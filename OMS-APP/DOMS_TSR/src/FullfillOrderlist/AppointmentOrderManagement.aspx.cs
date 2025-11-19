using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using SALEORDER.Common;

namespace DOMS_TSR.src.FullfillOrderlist
{
    public partial class AppointmentOrderManagement : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string Codelist = "";
        protected static int currentPageNumber_NoAnswerOrder;

        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";

        public Boolean check_NoAnswerOrder = false;
        public Boolean check_RequestForEditOrder = false;
        public Boolean check_RequestForRejectOrder = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                currentPageNumber_NoAnswerOrder = 1;

                MerchantInfo merchantInfo = new MerchantInfo();
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];
                hidMerchantCode.Value = merchantInfo.MerchantCode;
                EmpInfo empInfo = new EmpInfo();
               
                empInfo = (EmpInfo)Session["EmpInfo"];
             


                if (empInfo != null)
                {
                    APIUrl = empInfo.ConnectionAPI;
                    

                    hidEmpCode.Value = empInfo.EmpCode;

                    List<EmpBranchInfo> lb = new List<EmpBranchInfo>();

                    lb = ListEmpBranchByCriteria(empInfo.EmpCode);

                    if (lb.Count > 0)
                    {
                        hidBranchcode.Value = lb[0].BranchCode;
                        hiddisplayname.Value = lb[0].BranchCode;
                    }
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                Hide_Section();

                sectionLoad_NoAnswerOrder();

               
        
            }
        }

        #region Main
        #region Function (Main)

        public void Hide_Section()
        {
            show_NoAnswerOrder(false);

        }

        public void show_NoAnswerOrder(Boolean show)
        {
            btnSecondary();
            showSection_NoAnswerOrder.CssClass = "btn btn-primary";

            searchSection_NoAnswerOrder.Visible = show;
            Section_NoAnswerOrder.Visible = show;
        }



        public void reLoadAnySection()
        {
            LoadOrder_NoAnswerOrder();

        }

        public void sectionLoad_NoAnswerOrder()
        {

            showSection_NoAnswerOrder.CssClass = "btn btn-primary";
            show_NoAnswerOrder(true);

            
            BindddlSearchCamCate_NoAnswerOrder();
            BindDdlInventory();
            LoadOrder_NoAnswerOrder();
        }

    

     


        public void btnSecondary()
        {

            if (check_NoAnswerOrder == true) { }
            else { showSection_NoAnswerOrder.CssClass = "  btn-3bar-disable1"; }

        }

        public static List<EmpBranchInfo> ListEmpBranchByCriteria(string empcode)
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/ListEmpBranchByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empcode;
                data["rowOFFSet"] = "0";
                data["rowFetch"] = "10";

                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpBranchInfo> lEmpBranchInfo = JsonConvert.DeserializeObject<List<EmpBranchInfo>>(respstr);

            return lEmpBranchInfo;

        }

        #endregion Function (Main)

        #region Events (Main)

        protected void showSection_NoAnswerOrder_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_NoAnswerOrder();
        }



        public static int? CountOrderListByCriteria(string Branchcode, string orderstatus)
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/CountOrderListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["BranchCode"] = Branchcode;

                data["OrderStatusCode"] = orderstatus;

                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;

        }

        #endregion Events (Main)
        #endregion Main

        #region No Answer Order
        #region Function (No Answer Order)
        protected void LoadOrder_NoAnswerOrder()
        {
            List<OrderInfo> lOrderInfo_NoAnswerOrder = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_NoAnswerOrder();

            countSection_NoAnswerOrder.Text = totalRow.ToString();

            SetPageBar_NoAnswerOrder(Convert.ToDouble(totalRow));

            lOrderInfo_NoAnswerOrder = GetOrderMasterByCriteria_NoAnswerOrder();

            gvOrder_NoAnswerOrder.DataSource = lOrderInfo_NoAnswerOrder;
            gvOrder_NoAnswerOrder.DataBind();

        }

        public int? CountOrderMasterList_NoAnswerOrder()
        {
         
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderFullfillManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
            
                data["OrderCode"] = txtSearchOrderCode_NoAnswerOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NoAnswerOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NoAnswerOrder.Text;

           

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                

                data["CustomerContact"] = txtSearchContact_NoAnswerOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;

                

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                data["OrderStatusCode"] = StaticField.OrderStatus_01; 
                  data["OrderStateCode"] = StaticField.OrderState_01; 
                data["ConfirmNo"] = "NULL";
                data["InventoryCode"] = DDlInventory.SelectedValue == "-99" ? data["InventoryCode"] = "" : data["InventoryCode"] = DDlInventory.SelectedValue;
                data["MerchantMapCode"] = hidMerchantCode.Value;
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_NoAnswerOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderFullfillManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_NoAnswerOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NoAnswerOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NoAnswerOrder.Text;

            

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                

                data["CustomerContact"] = txtSearchContact_NoAnswerOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;

                

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                data["OrderStatusCode"] = StaticField.OrderStatus_01; 

              
                data["GroupOrderStateCode"] = StaticField.OrderState_01 + "," + StaticField.OrderState_16; 

                data["ConfirmNo"] = "NULL";
                data["InventoryCode"] = DDlInventory.SelectedValue == "-99" ? data["InventoryCode"] = "" : data["InventoryCode"] = DDlInventory.SelectedValue;
            
                data["MerchantMapCode"] = hidMerchantCode.Value;

                data["rowOFFSet"] = ((currentPageNumber_NoAnswerOrder - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_NoAnswerOrder()
        {
            txtSearchOrderCode_NoAnswerOrder.Text = "";
     
            txtSearchOrderDateFrom_NoAnswerOrder.Text = "";
            txtSearchOrderDateUntil_NoAnswerOrder.Text = "";
           
            txtSearchFName_NoAnswerOrder.Text = "";
            txtSearchLName_NoAnswerOrder.Text = "";
            
            txtSearchContact_NoAnswerOrder.Text = "";
            txtSearchDeliverDate_NoAnswerOrder.Text = "";
            txtSearchDeliverDateTo_NoAnswerOrder.Text = "";
            
            ddlSearchCamCate_NoAnswerOrder.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_NoAnswerOrder(string OrderCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/sumAmoutOrderDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = OrderCode;

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }
        public int? sumTotalPriceOrderDetail_NoAnswerOrder(string OrderCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/sumTotalPriceOrderDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = OrderCode;

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        protected Boolean Changestatus_NoAnswerOrder()
        {
            int conNo = CountRoundOfDay();
     
            for (int i = 0; i < gvOrder_NoAnswerOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_NoAnswerOrder.Rows[i].FindControl("chk_NoAnswerOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderId_NoAnswerOrder");  
                    HiddenField hidInventoryCode = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidInventoryCode_NoAnswerOrder");
                    HiddenField hidOrder = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderCode_NoAnswerOrder");
                    Label Lbordercode = (Label)gvOrder_NoAnswerOrder.Rows[i].FindControl("lblOrderCode_NoAnswerOrder");
                    
                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }

                    //UpdateInventoryDetailbyStock(hidOrder.Value, hidInventoryCode.Value);

                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = StaticField.OrderState_13, orderstatus= StaticField.OrderStatus_01, Confirmno =conNo.ToString() }); 
                }
            }

            if (CodelistApprove != "")
            {               
                APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                    var dataString = client.UploadString(APIpath, jsonObj);
           
                }                
            }
            else
            {
                return false;
            }
            return true;
        }
        protected Boolean Changestatus_NoAnswerOrderforCancel()
        {
            int conNo = CountRoundOfDay();

            for (int i = 0; i < gvOrder_NoAnswerOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_NoAnswerOrder.Rows[i].FindControl("chk_NoAnswerOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderId_NoAnswerOrder");
                    HiddenField hidOrder = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderCode_NoAnswerOrder");
                    Label Lbordercode = (Label)gvOrder_NoAnswerOrder.Rows[i].FindControl("lblOrderCode_NoAnswerOrder");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstatus = StaticField.OrderStatus_01, orderstate = StaticField.OrderState_14 }); 
                }
            }

            if (CodelistApprove != "")
            {
                APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                    var dataString = client.UploadString(APIpath, jsonObj);

                }
            }
            else
            {
                return false;
            }
            return true;
        }
        protected Boolean Changestatus_NoAnswerOrderforBackOrder()
        {
         

            for (int i = 0; i < gvOrder_NoAnswerOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_NoAnswerOrder.Rows[i].FindControl("chk_NoAnswerOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderId_NoAnswerOrder");
                    HiddenField hidOrder = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderCode_NoAnswerOrder");
                    Label Lbordercode = (Label)gvOrder_NoAnswerOrder.Rows[i].FindControl("lblOrderCode_NoAnswerOrder");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstatus = StaticField.OrderStatus_01, orderstate = StaticField.OrderState_12 }); 
                }
            }

            if (CodelistApprove != "")
            {
                APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                    var dataString = client.UploadString(APIpath, jsonObj);

                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public int CountRoundOfDay()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderByCriteriaOrderlistConfirmNo";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["OrderListDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<OrderListReturn> lInfo = JsonConvert.DeserializeObject<List<OrderListReturn>>(respstr);
            int cou = 0;
            if (lInfo.Count > 0)
            {
                cou = int.Parse(lInfo[0].ConfirmNo.ToString()) + 1;
            }

            return cou;


        }
        protected Boolean DeleteProduct()
        {

            for (int i = 0; i < gvOrder_NoAnswerOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_NoAnswerOrder.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidProductId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteProduct";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ProductCode"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            else
            {
               
                return false;
            }

         
            return true;

        }
        protected void gvOrder_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBalance = (Label)e.Row.FindControl("lblBackorder_NoAnswerOrder");

                if (lblBalance.Text != "" && lblBalance.Text != null)
                {
                    if (Convert.ToInt32(lblBalance.Text) > 0)
                    {
                        lblBalance.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else 
                {
                    lblBalance.Text = "0";
                    lblBalance.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion Function (No Answer Order)

        #region Events (No Answer Order)
        protected void btnSearch_Click_NoAnswerOrder(object sender, EventArgs e)
        {
            if (ValidateSearch())
            {
            LoadOrder_NoAnswerOrder();

            }
        }

        protected void btnClearSearch_Click_NoAnswerOrder(object sender, EventArgs e)
        {
            clearSearch_NoAnswerOrder();
        }


        protected void btnCancelRejectForRequest_NoAnswerOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject').modal('hide');", true);
        }

        protected void btnCancelOrder_NoAnswerOrder_Click(object sender, EventArgs e)
        {
            Changestatus_NoAnswerOrderforCancel();
            LoadOrder_NoAnswerOrder();
        }
        protected void btnBackOrder_NoAnswerOrder_Click(object sender, EventArgs e)
        {
            Changestatus_NoAnswerOrderforBackOrder();
            LoadOrder_NoAnswerOrder();
        }
        protected void btnAcceptOrder_NoAnswerOrder_Click(object sender, EventArgs e)
        {
            Changestatus_NoAnswerOrder();
            LoadOrder_NoAnswerOrder();
        }
        protected void chkAll_Change_NoAnswerOrder(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_NoAnswerOrder.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_NoAnswerOrder.HeaderRow.FindControl("chkAll_NoAnswerOrder");
                CheckBox chk_NewOrder = (CheckBox)gvOrder_NoAnswerOrder.Rows[i].FindControl("chk_NoAnswerOrder");

                if (chkall.Checked == true)
                {
                    chk_NewOrder.Checked = true;
                }
                else
                {
                    chk_NewOrder.Checked = false;
                }
            }

        }

        #endregion Events (No Answer Order)

        #region Binding (No Answer Order)

        protected void BindddlSearchOrderType_NoAnswerOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "SALEORDERTYPE";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            
        }

        protected void BindddlSearchBranch_NoAnswerOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/BranchListNoPagingCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["BranchCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<BranchInfo> bLookupInfo = JsonConvert.DeserializeObject<List<BranchInfo>>(respstr);

            
        }

        protected void BindddlSearchChannel_NoAnswerOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ChannelInfo> cLookupInfo = JsonConvert.DeserializeObject<List<ChannelInfo>>(respstr);

            
        }
        protected void BindDdlInventory()
        {
            List<InventoryInfo> linvInfo = new List<InventoryInfo>();
            linvInfo = GetListMasterInventoryByCriteria();

            DDlInventory.DataSource = linvInfo;
            DDlInventory.DataTextField = "InventoryName";
            DDlInventory.DataValueField = "InventoryCode";
            DDlInventory.DataBind();

            DDlInventory.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected List<InventoryInfo> GetListMasterInventoryByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = "";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> linventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);
            return linventoryInfo;
        }
        protected void BindddlSearchCamCate_NoAnswerOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchCamCate_NoAnswerOrder.DataSource = camLookupInfo;
            ddlSearchCamCate_NoAnswerOrder.DataTextField = "ProductBrandName";
            ddlSearchCamCate_NoAnswerOrder.DataValueField = "ProductBrandCode";
            ddlSearchCamCate_NoAnswerOrder.DataBind();
            ddlSearchCamCate_NoAnswerOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (No Answer Order)

        #region Paging (No Answer Order)

        protected void SetPageBar_NoAnswerOrder(double totalRow)
        {

            lblTotalPages_NoAnswerOrder.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_NoAnswerOrder.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_NoAnswerOrder.Text) + 1; i++)
            {
                ddlPage_NoAnswerOrder.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_NoAnswerOrder(ddlPage_NoAnswerOrder, currentPageNumber_NoAnswerOrder.ToString());
            

            
            if ((currentPageNumber_NoAnswerOrder == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_NoAnswerOrder.Enabled = false;
                lnkbtnPre_NoAnswerOrder.Enabled = false;
                lnkbtnNext_NoAnswerOrder.Enabled = true;
                lnkbtnLast_NoAnswerOrder.Enabled = true;
            }
            else if ((currentPageNumber_NoAnswerOrder.ToString() == lblTotalPages_NoAnswerOrder.Text) && (currentPageNumber_NoAnswerOrder == 1))
            {
                lnkbtnFirst_NoAnswerOrder.Enabled = false;
                lnkbtnPre_NoAnswerOrder.Enabled = false;
                lnkbtnNext_NoAnswerOrder.Enabled = false;
                lnkbtnLast_NoAnswerOrder.Enabled = false;
            }
            else if ((currentPageNumber_NoAnswerOrder.ToString() == lblTotalPages_NoAnswerOrder.Text) && (currentPageNumber_NoAnswerOrder > 1))
            {
                lnkbtnFirst_NoAnswerOrder.Enabled = true;
                lnkbtnPre_NoAnswerOrder.Enabled = true;
                lnkbtnNext_NoAnswerOrder.Enabled = false;
                lnkbtnLast_NoAnswerOrder.Enabled = false;
            }
            else
            {
                lnkbtnFirst_NoAnswerOrder.Enabled = true;
                lnkbtnPre_NoAnswerOrder.Enabled = true;
                lnkbtnNext_NoAnswerOrder.Enabled = true;
                lnkbtnLast_NoAnswerOrder.Enabled = true;
            }
            
        }

        private void setDDl_NoAnswerOrder(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        protected void GetPageIndex_NoAnswerOrder(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_NoAnswerOrder = 1;
                    break;

                case "Previous":
                    currentPageNumber_NoAnswerOrder = Int32.Parse(ddlPage_NoAnswerOrder.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_NoAnswerOrder = Int32.Parse(ddlPage_NoAnswerOrder.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_NoAnswerOrder = Int32.Parse(lblTotalPages_NoAnswerOrder.Text);
                    break;
            }


            LoadOrder_NoAnswerOrder();
        }

        protected void ddlPage_SelectedIndexChanged_NoAnswerOrder(object sender, EventArgs e)
        {
            currentPageNumber_NoAnswerOrder = Int32.Parse(ddlPage_NoAnswerOrder.SelectedValue);

            LoadOrder_NoAnswerOrder();
        }

        #endregion Paging (No Answer Order)
        #endregion No Answer Order


   

        protected string GetLink(object objCode, object CustomerCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            string StrCustomerCode = (CustomerCode != null) ? CustomerCode.ToString() : "";
            return "<a href=\"../FullfillOrderlist/AppointmentOrderManagementDetail.aspx?CustomerCode=" + StrCustomerCode + "&OrderCode=" + strCode + "\">" + strCode + "</a>";
        }
        protected Boolean ValidateSearch()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[ก-๙a-zA-Z0-9/ ]*$");
            var regexDate = new Regex("^[0-9/ ]*$");


            if (regexItem.IsMatch(txtSearchOrderCode_NoAnswerOrder.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchOrderCode_NoAnswerOrder.Text = "";
            }
            else
            {
                flag = false;
                lblSearchOrderCode_NoAnswerOrder.Text = MessageConst._MSG_PLEASEINSERT + " รหัสใบสั่งขายต้องไม่มีอักขระพิเศษ";
            }
            if (regexDate.IsMatch(txtSearchOrderDateFrom_NoAnswerOrder.Text) && regexDate.IsMatch(txtSearchOrderDateUntil_NoAnswerOrder.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchOrderDate_NoAnswerOrder.Text = "";
            }
            else
            {
                flag = false;
                lblSearchOrderDate_NoAnswerOrder.Text = MessageConst._MSG_PLEASEINSERT + " วันที่สั่งซื้อต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchFName_NoAnswerOrder.Text) && regexItem.IsMatch(txtSearchLName_NoAnswerOrder.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchName_NoAnswerOrder.Text = "";
            }
            else
            {
                flag = false;
                lblSearchName_NoAnswerOrder.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ - สกุลต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchContact_NoAnswerOrder.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchContact_NoAnswerOrder.Text = "";
            }
            else
            {
                flag = false;
                lblSearchContact_NoAnswerOrder.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์ติดต่อต้องไม่มีอักขระพิเศษ";
            }
            if (regexDate.IsMatch(txtSearchDeliverDate_NoAnswerOrder.Text) && regexDate.IsMatch(txtSearchDeliverDateTo_NoAnswerOrder.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchDeliverDate_NoAnswerOrder.Text = "";
            }
            else
            {
                flag = false;
                lblSearchDeliverDate_NoAnswerOrder.Text = MessageConst._MSG_PLEASEINSERT + " วันนัดส่งสินค้าต้องไม่มีอักขระพิเศษ";
            }
           
            return flag;
        }

        #region Function
        protected void UpdateInventoryDetailbyStock(string ordercode,string inventorycode)
        {
            string respstr1 = "";
            int? amount = 0;
            string productcode = "";
            string inventory = "";

            APIpath = APIUrl + "/api/support/ListOrderDetailNopagingbyOrderCode";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = ordercode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr1 = Encoding.UTF8.GetString(response);
            }

            List<OrderDetailInfo> lorderdataInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr1);

            if (lorderdataInfo.Count > 0)
            {
                foreach (var od in lorderdataInfo)
                {
                    amount = od.Amount;
                    productcode = od.ProductCode;
                                        
                    inventory = inventorycode;

                    string respstr2 = "";

                    APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        
                        data["InventoryCode"] = inventorycode;
                        data["ProductCode"] = od.ProductCode;

                        var response = wb.UploadValues(APIpath, "POST", data);
                        respstr2 = Encoding.UTF8.GetString(response);
                    }

                    List<OrderDetailInfo> linventroydetailinfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr2);
                    int? QTY = 0;
                    int? reserved = 0;
                    int? balance = 0;

                    try
                    {
                        if (linventroydetailinfo.Count > 0)
                        {
                            foreach (var oe in linventroydetailinfo)
                            {
                                QTY = oe.QTY - amount;
                                reserved = oe.Reserved - amount;
                                balance = oe.Balance - amount;

                                string respstr = "";
                                int? sum = 0;
                                APIpath = APIUrl + "/api/support/UpdateInvenDetailfromOrderChangeStatus";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["InventoryCode"] = inventory;
                                    data["ProductCode"] = productcode;
                                    data["QTY"] = QTY.ToString();
                                    data["Reserved"] = reserved.ToString();
                                    data["Balance"] = balance.ToString();
                                    data["UpdateBy"] = hidEmpCode.Value;

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstr = Encoding.UTF8.GetString(response);

                                    sum = JsonConvert.DeserializeObject<int?>(respstr);
                                }
                            }
                        }
                    }
                    catch (Exception ex) { }                    
                }
            }
        }
        #endregion
    }
}
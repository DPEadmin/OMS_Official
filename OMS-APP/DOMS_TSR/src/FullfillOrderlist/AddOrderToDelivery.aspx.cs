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
    public partial class AddOrderToDelivery : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string Codelist = "";
        protected static int currentPageNumber_NoAnswerOrder;

        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected static int currentPageNumber;
        public Boolean check_NoAnswerOrder = false;
        public Boolean check_RequestForEditOrder = false;
        public Boolean check_RequestForRejectOrder = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumber_NoAnswerOrder = 1;
           

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    
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

                LoadRouting();
                sectionLoad_NoAnswerOrder();

               
        
            }
        }

        #region Main
        #region Function (Main)
        protected void LoadRouting()
        {
            List<RoutingInfo> lRoutInfo = new List<RoutingInfo>();

            




            lRoutInfo = GetRoutingMasterByCriteria();

            ddlRouting.DataSource = lRoutInfo;
            ddlRouting.DataTextField = "Routing_name";
            ddlRouting.DataValueField = "Routing_code";
            ddlRouting.DataBind();
            ddlRouting.Items.Insert(0, new ListItem("กรุณาเลือกสายส่ง--------------------------", "-99"));



            

        }
        public List<RoutingInfo> GetRoutingMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = "";

                data["Routing_name"] ="";

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingInfo>>(respstr);


            return lRoutingInfo;

        }

        protected void LoadRoutingVehicle()
        {
            List<RoutingVehicleInfo> lRoutingVehicleInfo = new List<RoutingVehicleInfo>();




            lRoutingVehicleInfo = GetRoutingVehicleMasterByCriteria();


            ddlVechicle.DataSource = lRoutingVehicleInfo;
            ddlVechicle.DataTextField = "Vechicle_No";
            ddlVechicle.DataValueField = "RoutingVechicleId";
            ddlVechicle.DataBind();
            ddlVechicle.Items.Insert(0, new ListItem("กรุณาเลือกรถขนส่ง--------------------------", "-99"));

        }

        public List<RoutingVehicleInfo> GetRoutingVehicleMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingVehicleByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = ddlRouting.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingVehicleInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingVehicleInfo>>(respstr);


            return lRoutingInfo;

        }

        public void Hide_Section()
        {
            show_NoAnswerOrder(false);

        }

        public void show_NoAnswerOrder(Boolean show)
        {
            btnSecondary();
            

            searchSection_NoAnswerOrder.Visible = show;
            Section_NoAnswerOrder.Visible = show;
        }



        public void reLoadAnySection()
        {
            LoadOrder_NoAnswerOrder();

        }

        public void sectionLoad_NoAnswerOrder()
        {

            show_NoAnswerOrder(true);

 
            BindddlSearchCamCate_NoAnswerOrder();

            LoadOrder_NoAnswerOrder();
        }

    

     


        public void btnSecondary()
        {

            

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
                data["rowFetch"] = PAGE_SIZE.ToString();

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

                data["CustomerCode"] = txtSearchCustomerCode_NoAnswerOrder.Text;

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                

                data["CustomerContact"] = txtSearchContact_NoAnswerOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;

                

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["OrderStatusCode"] = StaticField.OrderStatus_01; 
                
                data["ConfirmNo"] = "NULL";

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

                data["CustomerCode"] = txtSearchCustomerCode_NoAnswerOrder.Text;

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                

                data["CustomerContact"] = txtSearchContact_NoAnswerOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;

                

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["OrderStatusCode"] = StaticField.OrderStatus_01; 
                
                data["ConfirmNo"] = "NULL";
                
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
            txtSearchCustomerCode_NoAnswerOrder.Text = "";
            txtSearchOrderDateFrom_NoAnswerOrder.Text = "";
            txtSearchOrderDateUntil_NoAnswerOrder.Text = "";
            txtSearchCustomerCode_NoAnswerOrder.Text = "";
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
                    
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo()
                    {
                        updateBy = hidEmpCode.Value.ToString(),
                        ordercode = Lbordercode.Text.Trim(),
                         
                        orderstate = StaticField.OrderState_05, 
                      
                        orderstatus = StaticField.OrderStatus_04, 
                        OrderRouting = ddlRouting.SelectedValue.ToString(),
                        OrderVechicle = ddlVechicle.SelectedValue.ToString()
                    });
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
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstatus = StaticField.OrderStatus_02, orderstate = StaticField.OrderState_06, Confirmno = conNo.ToString() }); 
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

        #endregion Function (No Answer Order)

        #region Events (No Answer Order)
        protected void btnSearch_Click_NoAnswerOrder(object sender, EventArgs e)
        {
            if (validateSearch())
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
        protected void ddlRouting_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRoutingVehicle();
        }
        #endregion Paging (No Answer Order)
        #endregion No Answer Order


        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"../OrderDetail/OrderDetail.aspx?OrderCode=" + strCode + "\">" + strCode + "</a>";
        }


        protected Boolean validateSearch()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[ก-๙a-zA-Z0-9/ ]*$");

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
            if (regexItem.IsMatch(txtSearchOrderDateFrom_NoAnswerOrder.Text) )
            {
                flag = (flag == false) ? false : true;
                lblSearchOrderDate_NoAnswerOrder.Text = "";
            }
            else
            {
                flag = false;
                lblSearchOrderDate_NoAnswerOrder.Text = MessageConst._MSG_PLEASEINSERT + " วันที่สั่งซื้อต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchCustomerCode_NoAnswerOrder.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchCustomerCode_NoAnswerOrder.Text = "";
            }
            else
            {
                flag = false;
                lblSearchCustomerCode_NoAnswerOrder.Text = MessageConst._MSG_PLEASEINSERT + " รหัสลูกค้าต้องไม่มีอักขระพิเศษ";
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
            return flag;
        }
    }
}
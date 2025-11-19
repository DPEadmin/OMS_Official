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
using SALEORDER.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace DOMS_TSR.src.Outbound
{
    public partial class ContactHistory_1 : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string return_result;
        string CodelistApprove = "";
        protected static string APIUrl;

        string Codelist = "";
        protected static int currentPageNumber_NoAnswerOrder;
        protected static string APIUrlx = ConfigurationManager.AppSettings["apiurl"];
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string OMSCORE_API = ConfigurationManager.AppSettings["OMSCORE_API"];
        string Merchant_Code = "";
        string APIpath = "";

        public Boolean check_NoAnswerOrder = false;
        public Boolean check_RequestForEditOrder = false;
        public Boolean check_RequestForRejectOrder = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                currentPageNumber_NoAnswerOrder = 1;


                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    //APIUrl = empInfo.ConnectionAPI;

                    APIUrl = APIUrlx;
                    hidEmpCode.Value = empInfo.EmpCode;
                    MerchantInfo mInfo = (MerchantInfo)Session["MerchantInfo"];
                    if (mInfo != null)
                    {
                        Merchant_Code = mInfo.MerchantCode;
                    }
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
                
                BindLeadStatus("LEADSTATUS");
                BindDDLContact_Status("CONTACTSTATUS");
                BindDDLOrderSituation();

                txtSearchFName_NoAnswerOrder.Visible = false;
                txtSearchLName_NoAnswerOrder.Visible = false;
                txtSearchContact_NoAnswerOrder.Visible = false;
              
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

            //BindddlSearchCamCate_NoAnswerOrder();
      //      BindDdlInventory();
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
            List<LeadManagementInfo> lOrderInfo_NoAnswerOrder = new List<LeadManagementInfo>();

            int? totalRow = CountOrderMasterList_NoAnswerOrder();

            countSection_NoAnswerOrder.Text = totalRow.ToString();

            SetPageBar_NoAnswerOrder(Convert.ToDouble(totalRow));

            lOrderInfo_NoAnswerOrder = GetOrderMasterByCriteria_NoAnswerOrder();

            gvOrder_NoAnswerOrder.DataSource = lOrderInfo_NoAnswerOrder;
            gvOrder_NoAnswerOrder.DataBind();

        }

        public int? CountOrderMasterList_NoAnswerOrder()
        {
            
            int? cou = 0;
            LeadManagementInfo leadInfo = new LeadManagementInfo();
            List<LeadManagementInfo> lMonInfo = new List<LeadManagementInfo>();
            leadInfo.AssignEmpCode = hidEmpCode.Value.ToString();
            leadInfo.CustomerCode = "";
            leadInfo.FIRSTNAME_TH = txtSearchFName_NoAnswerOrder.Text.Trim();
            leadInfo.LASTNAME_TH = txtSearchLName_NoAnswerOrder.Text.Trim();
            leadInfo.Name = txtSearchName.Text.Trim();
            leadInfo.Telephone = txtSearchTelephone.Text.Trim();

            leadInfo.CustomerPhone = txtSearchContact_NoAnswerOrder.Text.Trim();
            //leadInfo.AssignEmpName = txtSearchName.Text.Trim();
            leadInfo.MERCHANT_CODE = Merchant_Code;
            leadInfo.CreateDateFrom = txtSearchCreateDateFrom.Text;
            leadInfo.CreateDateTo = txtSearchCreateDateTo.Text;
            leadInfo.SeachStatus = ddlSearchLeadStatus.SelectedValue;

            APIpath = APIUrl + "/api/support/CountListClickToCallNoPagingByCriteria";
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    leadInfo.AssignEmpCode,
                    leadInfo.CustomerCode,
                    leadInfo.FIRSTNAME_TH,
                    leadInfo.LASTNAME_TH,
                    leadInfo.PREVIOUS_ORDER_DATE_FROM,
                    leadInfo.PREVIOUS_ORDER_DATE_TO,
                    leadInfo.BRAND_NO,
                    leadInfo.CustomerPhone,
                    leadInfo.Name,
                    leadInfo.Telephone,
                    leadInfo.Caryear,
                    leadInfo.Carmodel,
                    leadInfo.ProductName,
                    leadInfo.PromotionName,
                    leadInfo.Status,
                    leadInfo.SeachStatus,
                });

                var dataString = client.UploadString(APIpath, jsonObj);
                cou = JsonConvert.DeserializeObject<int?>(dataString.ToString());

            }
            return cou;
        }

        public List<LeadManagementInfo> GetOrderMasterByCriteria_NoAnswerOrder()
        {

            LeadManagementInfo leadInfo = new LeadManagementInfo();
            List<LeadManagementInfo> lMonInfo = new List<LeadManagementInfo>();
            leadInfo.AssignEmpCode = hidEmpCode.Value.ToString();
            leadInfo.CustomerCode = "";
            leadInfo.FIRSTNAME_TH = txtSearchFName_NoAnswerOrder.Text.Trim();
            leadInfo.LASTNAME_TH = txtSearchLName_NoAnswerOrder.Text.Trim();
        
            leadInfo.CustomerPhone = txtSearchContact_NoAnswerOrder.Text.Trim();
            leadInfo.Name = txtSearchName.Text.Trim();
            leadInfo.Telephone = txtSearchTelephone.Text.Trim();
    
            //leadInfo.AssignEmpName = txtSearchName.Text.Trim();
            leadInfo.MERCHANT_CODE = Merchant_Code;
            leadInfo.CreateDateFrom = txtSearchCreateDateFrom.Text;
            leadInfo.CreateDateTo = txtSearchCreateDateTo.Text;
            leadInfo.SeachStatus = ddlSearchLeadStatus.SelectedValue;

            leadInfo.rowOFFSet = ((currentPageNumber_NoAnswerOrder - 1) * PAGE_SIZE);
            leadInfo.rowFetch = PAGE_SIZE;

            APIpath = APIUrl + "/api/support/ListClickToCallNoPagingByCriteria";
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    leadInfo.AssignEmpCode,
                    leadInfo.CustomerCode,
                    leadInfo.FIRSTNAME_TH,
                    leadInfo.LASTNAME_TH,
                    leadInfo.PREVIOUS_ORDER_DATE_FROM,
                    leadInfo.PREVIOUS_ORDER_DATE_TO,
                    leadInfo.BRAND_NO,
                    leadInfo.CustomerPhone,
                    leadInfo.Name,
                    leadInfo.Telephone,
                    leadInfo.Caryear,
                    leadInfo.Carmodel,
                    leadInfo.ProductName,
                    leadInfo.PromotionName,
                    leadInfo.Status,
                    leadInfo.SeachStatus,
                    leadInfo.rowOFFSet,
                    leadInfo.rowFetch

                });

                var dataString = client.UploadString(APIpath, jsonObj);
                lMonInfo = JsonConvert.DeserializeObject<List<LeadManagementInfo>>(dataString.ToString());
                if (lMonInfo.Count > 0)
                {
                    return lMonInfo;
                }
                else
                {
                    return null;
                }


            }

        }

        public void clearSearch_NoAnswerOrder()
        {

            txtSearchCreateDateTo.Text = "";
            txtSearchCreateDateFrom.Text = "";
            txtSearchFName_NoAnswerOrder.Text = "";
            txtSearchLName_NoAnswerOrder.Text = "";
            txtSearchContact_NoAnswerOrder.Text = "";
            txtSearchName.Text = "";
            txtSearchTelephone.Text = "";
            ddlSearchLeadStatus.ClearSelection();
       
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
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = StaticField.OrderState_13, orderstatus = StaticField.OrderStatus_01, Confirmno = conNo.ToString() }); 
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
                //Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                //LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
     
                //Label lblProductName = (Label)e.Row.FindControl("lblProductName");
                //Label lblPromotionName = (Label)e.Row.FindControl("lblPromotionName");

                //if (lblStatus.Text == "Closed")
                //{
                //    btnEdit.Enabled = false;
                //}


                
            }
        }

        #endregion Function (No Answer Order)

        #region Events (No Answer Order)
        protected void btnSearch_Click_NoAnswerOrder(object sender, EventArgs e)
        {

            LoadOrder_NoAnswerOrder();
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

        protected void gvOrder_NoAnswerOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //EmpInfo empInfo = new EmpInfo();

            //empInfo = (EmpInfo)Session["EmpInfo"];

            //int index = Convert.ToInt32(e.CommandArgument);

            //GridViewRow row = gvOrder_NoAnswerOrder.Rows[index];
            //HiddenField hidCAMPAIGN_ID = (HiddenField)row.FindControl("hidCAMPAIGN_ID");
            //HiddenField hidLeadID = (HiddenField)row.FindControl("hidLeadID");
            //HiddenField hidMEDIA_PHONE = (HiddenField)row.FindControl("hidMEDIA_PHONE");
            //HiddenField hidEmail = (HiddenField)row.FindControl("hidEmail");
            //HiddenField hidLeadCode = (HiddenField)row.FindControl("hidLeadCode");

            //string merchantmapcode = StaticField.MerchantCode_ClickToCall_TIB; 
            //string merchantmapname = StaticField.MerchantName_ClickToCall_Toyota; 

            //Label lblmsg = (Label)row.FindControl("lblmsg");

            //if (e.CommandName == "ClickToCall")
            //{
            //    string respstr = "";

            //    APIpath = APIUrl + "/api/support/ListLeadManagementNoPagingByCriteria";

            //    using (var wb = new WebClient())
            //    {
            //        var data = new NameValueCollection();

            //        data["LeadID"] = hidLeadID.Value;

            //        var response = wb.UploadValues(APIpath, "POST", data);
            //        respstr = Encoding.UTF8.GetString(response);
            //    }

            //    List<LeadManagementInfo> leadinfo = JsonConvert.DeserializeObject<List<LeadManagementInfo>>(respstr);
            //    string email = "";
            //    string telephone = "";
            //    string name = "";

            //    if (leadinfo.Count > 0)
            //    {
            //        email = leadinfo[0].Email;
            //        telephone = leadinfo[0].Telephone;
            //        name = leadinfo[0].Name;
            //    }

                
            //    Response.Redirect("../TakeOrderRetail/TakeOrder.aspx?CustomerCode=" + StaticField.CustomerCode_Param_ClickToTakeOrder + "&CallInfoID=&MerchantCode=" + StaticField.MerchantCode_Param_ClickToTakeOrder +
            //                     "&Refusername=" + StaticField.Refusername_Param_ClickToTakeOrder + "&CalllnNumber=" + telephone + "&Firstname=" + StaticField.Firstname_Param_ClickToTakeOrder + "&Lastname=" + StaticField.Lastname_Param_ClickToTakeOrder + "&MerchantSession=" + StaticField.MerchantSession_Param_ClickToTakeOrder + "&MerchantSessionName=" + StaticField.MerchantSessionName_Param_ClickToTakeOrder + "&LeadID=" + hidLeadID.Value);


            //}

            //if (e.CommandName == "SaveContactHistory")
            //{
            //    hidLeadCodeforUpdate.Value = hidLeadCode.Value;
            //    hidLeadIdforUpdate.Value = hidLeadID.Value;
            //    hidleadIDfroloadPopup.Value = hidLeadID.Value;

            //    LoadModalLeadManagement();

            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);                
            //}
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

                data["LookupType"] = StaticField.LookupType_SALEORDERTYPE; 


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

            //ddlSearchCamCate_NoAnswerOrder.DataSource = camLookupInfo;
            //ddlSearchCamCate_NoAnswerOrder.DataTextField = "ProductBrandName";
            //ddlSearchCamCate_NoAnswerOrder.DataValueField = "ProductBrandCode";
            //ddlSearchCamCate_NoAnswerOrder.DataBind();
            //ddlSearchCamCate_NoAnswerOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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

        protected void GetLink(object objcode)
        {
            string strCode = (objcode != null) ? objcode.ToString() : "";
            
        }

        protected void LoadModalLeadManagement()
        {
            lblLeadCode.Text = hidLeadCodeforUpdate.Value;

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLeadManagementNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LeadID"] = hidLeadIdforUpdate.Value;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<LeadManagementInfo> lLeadManagementInfo = JsonConvert.DeserializeObject<List<LeadManagementInfo>>(respstr);
            
            if (lLeadManagementInfo.Count > 0)
            {                
                txtUpdateDate.Text = lLeadManagementInfo[0].CreateDate;
                DateTime dt1 = DateTime.Parse(txtUpdateDate.Text);
                txtUpdateDate.Text = dt1.ToString("dd'/'MM'/'yyyy");

                ddlStatus.SelectedValue = (lLeadManagementInfo[0].Status == "") ? "-" : lLeadManagementInfo[0].Status;
                txtName.Text = (lLeadManagementInfo[0].Name == "") ? "-" : lLeadManagementInfo[0].Name;
                txtTelephone.Text = (lLeadManagementInfo[0].Telephone == "") ? "-" : lLeadManagementInfo[0].Telephone;
                txtInsuranceType.Text = (lLeadManagementInfo[0].Insurancetype == "") ? "-" : lLeadManagementInfo[0].Insurancetype;
                txtCarYear.Text = (lLeadManagementInfo[0].Caryear == "") ? "-" : lLeadManagementInfo[0].Caryear;
                txtCarType.Text = (lLeadManagementInfo[0].Cartype == "") ? "-" : lLeadManagementInfo[0].Cartype;
                txtCarModel.Text = (lLeadManagementInfo[0].Carmodel == "") ? "-" : lLeadManagementInfo[0].Carmodel;
                txtCarsubmodel.Text = (lLeadManagementInfo[0].Carsubmodel == "") ? "-" : lLeadManagementInfo[0].Carsubmodel;
                txtInsurancedate.Text = (lLeadManagementInfo[0].Insurancedate == "") ? "-" : lLeadManagementInfo[0].Insurancedate;

                txtRecontactbackDate.Text = (DateTime.Parse(lLeadManagementInfo[0].RecontactbackDate).ToString("dd'/'MM'/'yyyy") == "01/01/1900") ? "-" : lLeadManagementInfo[0].RecontactbackDate;

                if(txtRecontactbackDate.Text != "-")
                {
                    DateTime dt2 = DateTime.Parse(txtRecontactbackDate.Text);
                    txtRecontactbackDate.Text = dt2.ToString("dd'/'MM'/'yyyy");
                }                

                ddlContact_Status.SelectedValue = (lLeadManagementInfo[0].CallStatus == "") ? "-99" : lLeadManagementInfo[0].CallStatus;
                bindddlOrderSituation(ddlContact_Status.SelectedValue);
                ddlOrderSituation.SelectedValue = (lLeadManagementInfo[0].CallSituation == "") ? "-99" : lLeadManagementInfo[0].CallSituation;
                ddlRecontactbactPeriodTime.SelectedValue = (lLeadManagementInfo[0].RecontactbactPeriodTime == "") ? "-99" : lLeadManagementInfo[0].RecontactbactPeriodTime;

                txtCallCommendHistory.Text = (lLeadManagementInfo[0].Description == "") ? "-" : lLeadManagementInfo[0].Description;
                txtSONumber.Text = (lLeadManagementInfo[0].OrderCode == "") ? "-" : lLeadManagementInfo[0].OrderCode;
                txtCusreason.Text = (lLeadManagementInfo[0].CusReason == "") ? "-" : lLeadManagementInfo[0].CusReason;
                txtCusReasonOther.Text = (lLeadManagementInfo[0].CusReasonOther == "") ? "-" : lLeadManagementInfo[0].CusReasonOther;
                txtTransactionTypeCode.Text = lLeadManagementInfo[0].TransactionTypeName;

                hidLeadTelephonePopUp.Value = lLeadManagementInfo[0].Telephone;

                Displaypopup(ddlStatus.SelectedValue);
            }

        }

        protected void btnSaveContactHistory_Click(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "btnSaveContactHistory")
            {

            }
        }

        protected void btnSubmit_Clicked(object sender, EventArgs e)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateTIBLeadfromClickToCall";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LeadID"] = hidleadIDfroloadPopup.Value;
                data["Status"] = ddlStatus.SelectedValue;
                data["Name"] = txtName.Text.Trim();
                data["Telephone"] = txtTelephone.Text.Trim();
                data["Insurancetype"] = txtInsuranceType.Text;
                data["Caryear"] = txtCarYear.Text;
                data["Cartype"] = txtCarType.Text;
                data["Carmodel"] = txtCarModel.Text;
                data["Carsubmodel"] = txtCarsubmodel.Text;
                data["Insurancedate"] = txtInsurancedate.Text;
                data["CallStatus"] = ddlContact_Status.SelectedValue;
                data["CallSituation"] = ddlOrderSituation.SelectedValue;
                data["RecontactbackDate"] = txtRecontactbackDate.Text;
                data["RecontactbactPeriodTime"] = ddlRecontactbactPeriodTime.SelectedValue;
                data["CusReason"] = txtCusreason.Text;
                data["CusReasonOther"] = txtCusReasonOther.Text;
                
                data["Description"] = txtCallCommendHistory.Text;
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int? sum = JsonConvert.DeserializeObject<int?>(respstr);
            if (sum > 0)
            {
                LoadOrder_NoAnswerOrder();

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + SALEORDER.Common.MessageConst._UPDATE_SUCCESS + "');$('#modal-Promotion').modal('hide');", true);
            }
        }

        protected void BindDDLContact_Status(string param)
        {
            List<LookupInfo> lInfo = new List<LookupInfo>();
            lInfo = GetListContact_StatusByDDL(param);
            ddlContact_Status.DataSource = lInfo;
            ddlContact_Status.DataTextField = "LookupValue";
            ddlContact_Status.DataValueField = "LookupCode";
            ddlContact_Status.DataBind();

            ddlContact_Status.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));            
        }

        protected void BindLeadStatus(string param)
        {
            List<LookupInfo> lInfo = new List<LookupInfo>();
            lInfo = GetListContact_StatusByDDL(param);
            ddlSearchLeadStatus.DataSource = lInfo;
            ddlSearchLeadStatus.DataTextField = "LookupValue";
            ddlSearchLeadStatus.DataValueField = "LookupCode";
            ddlSearchLeadStatus.DataBind();

            ddlSearchLeadStatus.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected List<LookupInfo> GetListContact_StatusByDDL(string TypeStatus)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = TypeStatus;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);
            return lLookupInfo;
        }

        protected void BindDDLOrderSituation()
        {
            List<LookupInfo> lInfo = new List<LookupInfo>();
            lInfo = GetListOrderSituationByDDL();
            ddlOrderSituation.DataSource = lInfo;
            ddlOrderSituation.DataTextField = "LookupValue";
            ddlOrderSituation.DataValueField = "LookupCode";
            ddlOrderSituation.DataBind();
            ddlOrderSituation.SelectedValue = StaticField.OrderSituation01;
            ddlOrderSituation.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected List<LookupInfo> GetListOrderSituationByDDL()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSITUATION";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);
            return lLookupInfo;
        }

        protected void ddlContact_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lookupcode = ddlContact_Status.SelectedValue;

            bindddlOrderSituation(lookupcode);            
        }

        protected void bindddlOrderSituation(string lookupcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupContactStatusMapOrderSituation";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupCode"] = lookupcode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlOrderSituation.DataSource = lLookupInfo;
            ddlOrderSituation.DataTextField = "LookupValue";
            ddlOrderSituation.DataValueField = "LookupCode";
            ddlOrderSituation.DataBind();
            ddlOrderSituation.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void Displaypopup(string status)
        {
            if (status == "Closed")
            {
                lblLeadCode.Enabled = false;
                LinkgotoTakeOrder.Enabled = false;
                txtUpdateDate.Enabled = false;
                ddlStatus.Enabled = false;
                txtName.Enabled = false;
                txtTelephone.Enabled = false;
                txtInsuranceType.Enabled = false;
                txtCarYear.Enabled = false;
                txtCarType.Enabled = false;
                txtCarModel.Enabled = false;
                txtCarsubmodel.Enabled = false;
                txtInsurancedate.Enabled = false;
                ddlContact_Status.Enabled = false;
                ddlOrderSituation.Enabled = false;
                txtCallCommendHistory.Enabled = false;
                txtSONumber.Enabled = false;
                txtRecontactbackDate.Enabled = false;
                ddlRecontactbactPeriodTime.Enabled = false;
                txtCusreason.Enabled = false;
                txtCusReasonOther.Enabled = false;
                txtTransactionTypeCode.Enabled = false;
            }
            else
            {
                lblLeadCode.Enabled = true;
                LinkgotoTakeOrder.Enabled = true;
                txtUpdateDate.Enabled = true;
                ddlStatus.Enabled = true;
                txtName.Enabled = true;
                txtTelephone.Enabled = true;
                txtInsuranceType.Enabled = true;
                txtCarYear.Enabled = true;
                txtCarType.Enabled = true;
                txtCarModel.Enabled = true;
                txtCarsubmodel.Enabled = true;
                txtInsurancedate.Enabled = true;
                ddlContact_Status.Enabled = true;
                ddlOrderSituation.Enabled = true;
                txtCallCommendHistory.Enabled = true;
                txtSONumber.Enabled = true;
                txtRecontactbackDate.Enabled = true;
                ddlRecontactbactPeriodTime.Enabled = true;
                txtCusreason.Enabled = true;
                txtCusReasonOther.Enabled = true;
                txtTransactionTypeCode.Enabled = true;
            }
        }

        protected void LinkgotoTakeOrder_Clicked(object sender, EventArgs e)
        {
            Response.Redirect("../TakeOrderRetail/TakeOrder.aspx?CustomerCode=" + "sukanya.koseanto@hotmail.com" + "&CallInfoID=&MerchantCode=ENT&Refusername=user&CalllnNumber=" + hidLeadTelephonePopUp.Value + "&Firstname=อรวา&Lastname=นราวรรณ&MerchantSession=TIB&MerchantSessionName=โตโยต้า ลีสซิ่ง (ประเทศไทย) จำกัด&LeadID=" + hidleadIDfroloadPopup.Value);
        }
    }

}
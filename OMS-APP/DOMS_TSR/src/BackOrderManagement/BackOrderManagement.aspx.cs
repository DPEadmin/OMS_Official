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

namespace DOMS_TSR.src.BackOrderManagement
{
    public partial class BackOrderManagement : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl;
        string Codelist_NoAnswerOrder = "";

        protected static int currentPageNumber_NoAnswerOrder;
        protected static int currentPageNumber_RequestForEditOrder;
        protected static int currentPageNumber_RequestForRejectOrder;
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
                currentPageNumber_RequestForEditOrder = 1;
                currentPageNumber_RequestForRejectOrder = 1;

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

                
                LoadOrder_RequestForEditOrder();
                LoadOrder_RequestForRejectOrder();
            }
        }

        #region Main
        #region Function (Main)

        public void Hide_Section()
        {
            show_NoAnswerOrder(false);
            show_RequestForEditOrder(false);
            show_RequestForRejectOrder(false);
        }

        public void show_NoAnswerOrder(Boolean show)
        {
            btnSecondary();
            showSection_NoAnswerOrder.CssClass = "btn btn-primary";

            searchSection_NoAnswerOrder.Visible = show;
            Section_NoAnswerOrder.Visible = show;
        }

        public void show_RequestForEditOrder(Boolean show)
        {
            btnSecondary();
            showSection_RequestForEditOrder.CssClass = "btn btn-primary";

            searchSection_RequestForEditOrder.Visible = show;
            Section_RequestForEditOrder.Visible = show;
        }

        public void show_RequestForRejectOrder(Boolean show)
        {
            btnSecondary();
            showSection_RequestForRejectOrder.CssClass = "btn btn-primary";

            searchSection_RequestForRejectOrder.Visible = show;
            Section_RequestForRejectOrder.Visible = show;
        }


        public void reLoadAnySection()
        {
            LoadOrder_NoAnswerOrder();
            LoadOrder_RequestForEditOrder();
            LoadOrder_RequestForRejectOrder();
        }

        public void sectionLoad_NoAnswerOrder()
        {

            showSection_NoAnswerOrder.CssClass = "btn btn-primary";
            show_NoAnswerOrder(true);

            BindddlSearchOrderType_NoAnswerOrder();
            BindddlSearchBranch_NoAnswerOrder();
            BindddlSearchChannel_NoAnswerOrder();
            BindddlSearchCamCate_NoAnswerOrder();

            LoadOrder_NoAnswerOrder();
        }

        public void sectionLoad_RequestForEditOrder()
        {

            showSection_RequestForEditOrder.CssClass = "btn btn-primary";
            show_RequestForEditOrder(true);

            BindddlSearchOrderType_RequestForEditOrder();
            BindddlSearchBranch_RequestForEditOrder();
            BindddlSearchChannel_RequestForEditOrder();
            BindddlSearchCamCate_RequestForEditOrder();

            LoadOrder_RequestForEditOrder();
        }

        public void sectionLoad_RequestForRejectOrder()
        {

            showSection_RequestForRejectOrder.CssClass = "btn btn-primary";
            show_RequestForRejectOrder(true);

            BindddlSearchOrderType_RequestForRejectOrder();
            BindddlSearchBranch_RequestForRejectOrder();
            BindddlSearchChannel_RequestForRejectOrder();
            BindddlSearchCamCate_RequestForRejectOrder();

            LoadOrder_RequestForRejectOrder();
        }


        public void btnSecondary()
        {

            if (check_NoAnswerOrder == true) { }
            else { showSection_NoAnswerOrder.CssClass = "  btn-3bar-disable1"; }

            if (check_RequestForEditOrder == true) { }
            else { showSection_RequestForEditOrder.CssClass = " btn-3bar-disable2"; }

            if (check_RequestForRejectOrder == true) { }
            else { showSection_RequestForRejectOrder.CssClass = "  btn-3bar-disable3"; }

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

        protected void showSection_RequestForEditOrder_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_RequestForEditOrder();
        }

        protected void showSection_RequestForRejectOrder_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_RequestForRejectOrder();
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

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_NoAnswerOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NoAnswerOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NoAnswerOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_NoAnswerOrder.Text;

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                data["OrderTypeCode"] = ddlSearchOrderType_NoAnswerOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_NoAnswerOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_NoAnswerOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;

                data["BranchCode"] = ddlSearchBranch_NoAnswerOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_NoAnswerOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                data["OrderStateCode"] = "07";

                data["CreateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_NoAnswerOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_NoAnswerOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NoAnswerOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NoAnswerOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_NoAnswerOrder.Text;

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                data["OrderTypeCode"] = ddlSearchOrderType_NoAnswerOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_NoAnswerOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_NoAnswerOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;

                data["BranchCode"] = ddlSearchBranch_NoAnswerOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_NoAnswerOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                data["OrderStateCode"] = "07";

                data["CreateBy"] = hidEmpCode.Value;

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
            ddlSearchOrderType_NoAnswerOrder.SelectedValue = "-99";
            txtSearchContact_NoAnswerOrder.Text = "";
            txtSearchDeliverDate_NoAnswerOrder.Text = "";
            txtSearchDeliverDateTo_NoAnswerOrder.Text = "";
            ddlSearchBranch_NoAnswerOrder.SelectedValue = "-99";
            ddlSearchChannel_NoAnswerOrder.SelectedValue = "-99";
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
            for (int i = 0; i < gvOrder_NoAnswerOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_NoAnswerOrder.Rows[i].FindControl("chk_NoAnswerOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderId_NoAnswerOrder");
                    HiddenField hidOrder = (HiddenField)gvOrder_NoAnswerOrder.Rows[i].FindControl("hidOrderCode_NoAnswerOrder");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = "06" });
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

            ddlSearchOrderType_NoAnswerOrder.DataSource = lLookupInfo;
            ddlSearchOrderType_NoAnswerOrder.DataTextField = "LookupValue";
            ddlSearchOrderType_NoAnswerOrder.DataValueField = "LookupCode";
            ddlSearchOrderType_NoAnswerOrder.DataBind();
            ddlSearchOrderType_NoAnswerOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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

            ddlSearchBranch_NoAnswerOrder.DataSource = bLookupInfo;
            ddlSearchBranch_NoAnswerOrder.DataTextField = "BranchName";
            ddlSearchBranch_NoAnswerOrder.DataValueField = "BranchCode";
            ddlSearchBranch_NoAnswerOrder.DataBind();
            ddlSearchBranch_NoAnswerOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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

            ddlSearchChannel_NoAnswerOrder.DataSource = cLookupInfo;
            ddlSearchChannel_NoAnswerOrder.DataTextField = "ChannelName";
            ddlSearchChannel_NoAnswerOrder.DataValueField = "ChannelCode";
            ddlSearchChannel_NoAnswerOrder.DataBind();
            ddlSearchChannel_NoAnswerOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_NoAnswerOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignCategoryInfo> camLookupInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

            ddlSearchCamCate_NoAnswerOrder.DataSource = camLookupInfo;
            ddlSearchCamCate_NoAnswerOrder.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_NoAnswerOrder.DataValueField = "CampaignCategoryCode";
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

        #region Request For Edit Order
        #region Function (Request For Edit Order)
        protected void LoadOrder_RequestForEditOrder()
        {
            List<OrderInfo> lOrderInfo_RequestForEditOrder = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_RequestForEditOrder();

            countSection_RequestForEditOrder.Text = totalRow.ToString();

            SetPageBar_RequestForEditOrder(Convert.ToDouble(totalRow));

            lOrderInfo_RequestForEditOrder = GetOrderMasterByCriteria_RequestForEditOrder();

            gvOrder_RequestForEditOrder.DataSource = lOrderInfo_RequestForEditOrder;
            gvOrder_RequestForEditOrder.DataBind();

        }

        public int? CountOrderMasterList_RequestForEditOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_RequestForEditOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_RequestForEditOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_RequestForEditOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_RequestForEditOrder.Text;

                data["CustomerFName"] = txtSearchFName_RequestForEditOrder.Text;

                data["CustomerLName"] = txtSearchLName_RequestForEditOrder.Text;

                data["OrderTypeCode"] = ddlSearchOrderType_RequestForEditOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_RequestForEditOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_RequestForEditOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_RequestForEditOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_RequestForEditOrder.Text;

                data["BranchCode"] = ddlSearchBranch_RequestForEditOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_RequestForEditOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_RequestForEditOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_RequestForEditOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForEditOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForEditOrder.SelectedValue;

                data["OrderStateCode"] = "08";

                data["CreateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_RequestForEditOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_RequestForEditOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_RequestForEditOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_RequestForEditOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_RequestForEditOrder.Text;

                data["CustomerFName"] = txtSearchFName_RequestForEditOrder.Text;

                data["CustomerLName"] = txtSearchLName_RequestForEditOrder.Text;

                data["OrderTypeCode"] = ddlSearchOrderType_RequestForEditOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_RequestForEditOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_RequestForEditOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_RequestForEditOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_RequestForEditOrder.Text;

                data["BranchCode"] = ddlSearchBranch_RequestForEditOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_RequestForEditOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_RequestForEditOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_RequestForEditOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForEditOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForEditOrder.SelectedValue;

                data["OrderStateCode"] = "08";

                data["CreateBy"] = hidEmpCode.Value;

                data["rowOFFSet"] = ((currentPageNumber_RequestForEditOrder - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_RequestForEditOrder()
        {
            txtSearchOrderCode_RequestForEditOrder.Text = "";
            txtSearchCustomerCode_RequestForEditOrder.Text = "";
            txtSearchOrderDateFrom_RequestForEditOrder.Text = "";
            txtSearchOrderDateUntil_RequestForEditOrder.Text = "";
            txtSearchCustomerCode_RequestForEditOrder.Text = "";
            txtSearchFName_RequestForEditOrder.Text = "";
            txtSearchLName_RequestForEditOrder.Text = "";
            ddlSearchOrderType_RequestForEditOrder.SelectedValue = "-99";
            txtSearchContact_RequestForEditOrder.Text = "";
            txtSearchDeliverDate_RequestForEditOrder.Text = "";
            txtSearchDeliverDateTo_RequestForEditOrder.Text = "";
            ddlSearchBranch_RequestForEditOrder.SelectedValue = "-99";
            ddlSearchChannel_RequestForEditOrder.SelectedValue = "-99";
            ddlSearchCamCate_RequestForEditOrder.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_RequestForEditOrder(string OrderCode)
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
        public int? sumTotalPriceOrderDetail_RequestForEditOrder(string OrderCode)
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

        protected Boolean Changestatus_RequestForEditOrder()
        {
            for (int i = 0; i < gvOrder_RequestForEditOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_RequestForEditOrder.Rows[i].FindControl("chk_RequestForEditOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_RequestForEditOrder.Rows[i].FindControl("hidOrderId_RequestForEditOrder");
                    HiddenField hidOrder = (HiddenField)gvOrder_RequestForEditOrder.Rows[i].FindControl("hidOrderCode_RequestForEditOrder");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = "06" });
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
        #endregion Function (Request For Edit Order)

        #region Events (Request For Edit Order)
        protected void btnSearch_Click_RequestForEditOrder(object sender, EventArgs e)
        {
            LoadOrder_RequestForEditOrder();
        }

        protected void btnClearSearch_Click_RequestForEditOrder(object sender, EventArgs e)
        {
            clearSearch_RequestForEditOrder();
        }

        protected void btnCancelRejectForRequest_RequestForEditOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject').modal('hide');", true);
        }

        protected void btnCancelOrder_RequestForEditOrder_Click(object sender, EventArgs e)
        {
            Changestatus_RequestForEditOrder();
            LoadOrder_RequestForEditOrder();
        }


        protected void chkAll_Change_RequestForEditOrder(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_RequestForEditOrder.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_RequestForEditOrder.HeaderRow.FindControl("chkAll_RequestForEditOrder");
                CheckBox chk_NewOrder = (CheckBox)gvOrder_RequestForEditOrder.Rows[i].FindControl("chk_RequestForEditOrder");

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

        #endregion Events (Request For Edit Order)

        #region Binding (Request For Edit Order)

        protected void BindddlSearchOrderType_RequestForEditOrder()
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

            ddlSearchOrderType_RequestForEditOrder.DataSource = lLookupInfo;
            ddlSearchOrderType_RequestForEditOrder.DataTextField = "LookupValue";
            ddlSearchOrderType_RequestForEditOrder.DataValueField = "LookupCode";
            ddlSearchOrderType_RequestForEditOrder.DataBind();
            ddlSearchOrderType_RequestForEditOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_RequestForEditOrder()
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

            ddlSearchBranch_RequestForEditOrder.DataSource = bLookupInfo;
            ddlSearchBranch_RequestForEditOrder.DataTextField = "BranchName";
            ddlSearchBranch_RequestForEditOrder.DataValueField = "BranchCode";
            ddlSearchBranch_RequestForEditOrder.DataBind();
            ddlSearchBranch_RequestForEditOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_RequestForEditOrder()
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

            ddlSearchChannel_RequestForEditOrder.DataSource = cLookupInfo;
            ddlSearchChannel_RequestForEditOrder.DataTextField = "ChannelName";
            ddlSearchChannel_RequestForEditOrder.DataValueField = "ChannelCode";
            ddlSearchChannel_RequestForEditOrder.DataBind();
            ddlSearchChannel_RequestForEditOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_RequestForEditOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignCategoryInfo> camLookupInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

            ddlSearchCamCate_RequestForEditOrder.DataSource = camLookupInfo;
            ddlSearchCamCate_RequestForEditOrder.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_RequestForEditOrder.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_RequestForEditOrder.DataBind();
            ddlSearchCamCate_RequestForEditOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (Request For Edit Order)

        #region Paging (Request For Edit Order)

        protected void SetPageBar_RequestForEditOrder(double totalRow)
        {

            lblTotalPages_RequestForEditOrder.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_RequestForEditOrder.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_RequestForEditOrder.Text) + 1; i++)
            {
                ddlPage_RequestForEditOrder.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_RequestForEditOrder(ddlPage_RequestForEditOrder, currentPageNumber_RequestForEditOrder.ToString());
            

            
            if ((currentPageNumber_RequestForEditOrder == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_RequestForEditOrder.Enabled = false;
                lnkbtnPre_RequestForEditOrder.Enabled = false;
                lnkbtnNext_RequestForEditOrder.Enabled = true;
                lnkbtnLast_RequestForEditOrder.Enabled = true;
            }
            else if ((currentPageNumber_RequestForEditOrder.ToString() == lblTotalPages_RequestForEditOrder.Text) && (currentPageNumber_RequestForEditOrder == 1))
            {
                lnkbtnFirst_RequestForEditOrder.Enabled = false;
                lnkbtnPre_RequestForEditOrder.Enabled = false;
                lnkbtnNext_RequestForEditOrder.Enabled = false;
                lnkbtnLast_RequestForEditOrder.Enabled = false;
            }
            else if ((currentPageNumber_RequestForEditOrder.ToString() == lblTotalPages_RequestForEditOrder.Text) && (currentPageNumber_RequestForEditOrder > 1))
            {
                lnkbtnFirst_RequestForEditOrder.Enabled = true;
                lnkbtnPre_RequestForEditOrder.Enabled = true;
                lnkbtnNext_RequestForEditOrder.Enabled = false;
                lnkbtnLast_RequestForEditOrder.Enabled = false;
            }
            else
            {
                lnkbtnFirst_RequestForEditOrder.Enabled = true;
                lnkbtnPre_RequestForEditOrder.Enabled = true;
                lnkbtnNext_RequestForEditOrder.Enabled = true;
                lnkbtnLast_RequestForEditOrder.Enabled = true;
            }
            
        }

        private void setDDl_RequestForEditOrder(DropDownList ddls, String val)
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

        protected void GetPageIndex_RequestForEditOrder(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_RequestForEditOrder = 1;
                    break;

                case "Previous":
                    currentPageNumber_RequestForEditOrder = Int32.Parse(ddlPage_RequestForEditOrder.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_RequestForEditOrder = Int32.Parse(ddlPage_RequestForEditOrder.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_RequestForEditOrder = Int32.Parse(lblTotalPages_RequestForEditOrder.Text);
                    break;
            }


            LoadOrder_RequestForEditOrder();
        }

        protected void ddlPage_SelectedIndexChanged_RequestForEditOrder(object sender, EventArgs e)
        {
            currentPageNumber_RequestForEditOrder = Int32.Parse(ddlPage_RequestForEditOrder.SelectedValue);

            LoadOrder_RequestForEditOrder();
        }

        #endregion Paging (Request For Edit Order)
        #endregion Request For Edit Order

        #region Request For Reject Order
        #region Function (Request For Edit Order)
        protected void LoadOrder_RequestForRejectOrder()
        {
            List<OrderInfo> lOrderInfo_RequestForRejectOrder = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_RequestForRejectOrder();

            countSection_RequestForRejectOrder.Text = totalRow.ToString();

            SetPageBar_RequestForRejectOrder(Convert.ToDouble(totalRow));

            lOrderInfo_RequestForRejectOrder = GetOrderMasterByCriteria_RequestForRejectOrder();

            gvOrder_RequestForRejectOrder.DataSource = lOrderInfo_RequestForRejectOrder;
            gvOrder_RequestForRejectOrder.DataBind();

        }

        public int? CountOrderMasterList_RequestForRejectOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_RequestForRejectOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_RequestForRejectOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_RequestForRejectOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_RequestForRejectOrder.Text;

                data["CustomerFName"] = txtSearchFName_RequestForRejectOrder.Text;

                data["CustomerLName"] = txtSearchLName_RequestForRejectOrder.Text;

                data["OrderTypeCode"] = ddlSearchOrderType_RequestForRejectOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_RequestForRejectOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_RequestForRejectOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_RequestForRejectOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_RequestForRejectOrder.Text;

                data["BranchCode"] = ddlSearchBranch_RequestForRejectOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_RequestForRejectOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_RequestForRejectOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_RequestForRejectOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForRejectOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForRejectOrder.SelectedValue;

                data["OrderStateCode"] = "09";

                data["CreateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_RequestForRejectOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_RequestForRejectOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_RequestForRejectOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_RequestForRejectOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_RequestForRejectOrder.Text;

                data["CustomerFName"] = txtSearchFName_RequestForRejectOrder.Text;

                data["CustomerLName"] = txtSearchLName_RequestForRejectOrder.Text;

                data["OrderTypeCode"] = ddlSearchOrderType_RequestForRejectOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_RequestForRejectOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_RequestForRejectOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_RequestForRejectOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_RequestForRejectOrder.Text;

                data["BranchCode"] = ddlSearchBranch_RequestForRejectOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_RequestForRejectOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_RequestForRejectOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_RequestForRejectOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForRejectOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_RequestForRejectOrder.SelectedValue;

                data["OrderStateCode"] = "09";

                data["CreateBy"] = hidEmpCode.Value;

                data["rowOFFSet"] = ((currentPageNumber_RequestForRejectOrder - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_RequestForRejectOrder()
        {
            txtSearchOrderCode_RequestForRejectOrder.Text = "";
            txtSearchCustomerCode_RequestForRejectOrder.Text = "";
            txtSearchOrderDateFrom_RequestForRejectOrder.Text = "";
            txtSearchOrderDateUntil_RequestForRejectOrder.Text = "";
            txtSearchCustomerCode_RequestForRejectOrder.Text = "";
            txtSearchFName_RequestForRejectOrder.Text = "";
            txtSearchLName_RequestForRejectOrder.Text = "";
            ddlSearchOrderType_RequestForRejectOrder.SelectedValue = "-99";
            txtSearchContact_RequestForRejectOrder.Text = "";
            txtSearchDeliverDate_RequestForRejectOrder.Text = "";
            txtSearchDeliverDateTo_RequestForRejectOrder.Text = "";
            ddlSearchBranch_RequestForRejectOrder.SelectedValue = "-99";
            ddlSearchChannel_RequestForRejectOrder.SelectedValue = "-99";
            ddlSearchCamCate_RequestForRejectOrder.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_RequestForRejectOrder(string OrderCode)
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
        public int? sumTotalPriceOrderDetail_RequestForRejectOrder(string OrderCode)
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

        protected Boolean Changestatus_RequestForRejectOrder()
        {
            for (int i = 0; i < gvOrder_RequestForRejectOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_RequestForRejectOrder.Rows[i].FindControl("chk_RequestForRejectOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_RequestForRejectOrder.Rows[i].FindControl("hidOrderId_RequestForRejectOrder");
                    HiddenField hidOrder = (HiddenField)gvOrder_RequestForRejectOrder.Rows[i].FindControl("hidOrderCode_RequestForRejectOrder");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = "06" });
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
        #endregion Function (Request For Edit Order)

        #region Events (Request For Edit Order)
        protected void btnSearch_Click_RequestForRejectOrder(object sender, EventArgs e)
        {
            LoadOrder_RequestForRejectOrder();
        }

        protected void btnClearSearch_Click_RequestForRejectOrder(object sender, EventArgs e)
        {
            clearSearch_RequestForRejectOrder();
        }

        protected void btnCancelRejectForRequest_RequestForRejectOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject').modal('hide');", true);
        }

        protected void btnCancelOrder_RequestForRejectOrder_Click(object sender, EventArgs e)
        {
            Changestatus_RequestForRejectOrder();
            LoadOrder_RequestForRejectOrder();
        }

        protected void chkAll_Change_RequestForRejectOrder(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_RequestForRejectOrder.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_RequestForRejectOrder.HeaderRow.FindControl("chkAll_RequestForRejectOrder");
                CheckBox chk_NewOrder = (CheckBox)gvOrder_RequestForRejectOrder.Rows[i].FindControl("chk_RequestForRejectOrder");

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

        #endregion Events (Request For Edit Order)

        #region Binding (Request For Edit Order)

        protected void BindddlSearchOrderType_RequestForRejectOrder()
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

            ddlSearchOrderType_RequestForRejectOrder.DataSource = lLookupInfo;
            ddlSearchOrderType_RequestForRejectOrder.DataTextField = "LookupValue";
            ddlSearchOrderType_RequestForRejectOrder.DataValueField = "LookupCode";
            ddlSearchOrderType_RequestForRejectOrder.DataBind();
            ddlSearchOrderType_RequestForRejectOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_RequestForRejectOrder()
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

            ddlSearchBranch_RequestForRejectOrder.DataSource = bLookupInfo;
            ddlSearchBranch_RequestForRejectOrder.DataTextField = "BranchName";
            ddlSearchBranch_RequestForRejectOrder.DataValueField = "BranchCode";
            ddlSearchBranch_RequestForRejectOrder.DataBind();
            ddlSearchBranch_RequestForRejectOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_RequestForRejectOrder()
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

            ddlSearchChannel_RequestForRejectOrder.DataSource = cLookupInfo;
            ddlSearchChannel_RequestForRejectOrder.DataTextField = "ChannelName";
            ddlSearchChannel_RequestForRejectOrder.DataValueField = "ChannelCode";
            ddlSearchChannel_RequestForRejectOrder.DataBind();
            ddlSearchChannel_RequestForRejectOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_RequestForRejectOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignCategoryInfo> camLookupInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

            ddlSearchCamCate_RequestForRejectOrder.DataSource = camLookupInfo;
            ddlSearchCamCate_RequestForRejectOrder.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_RequestForRejectOrder.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_RequestForRejectOrder.DataBind();
            ddlSearchCamCate_RequestForRejectOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (Request For Edit Order)

        #region Paging (Request For Edit Order)

        protected void SetPageBar_RequestForRejectOrder(double totalRow)
        {

            lblTotalPages_RequestForRejectOrder.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_RequestForRejectOrder.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_RequestForRejectOrder.Text) + 1; i++)
            {
                ddlPage_RequestForRejectOrder.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_RequestForRejectOrder(ddlPage_RequestForRejectOrder, currentPageNumber_RequestForRejectOrder.ToString());
            

            
            if ((currentPageNumber_RequestForRejectOrder == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_RequestForRejectOrder.Enabled = false;
                lnkbtnPre_RequestForRejectOrder.Enabled = false;
                lnkbtnNext_RequestForRejectOrder.Enabled = true;
                lnkbtnLast_RequestForRejectOrder.Enabled = true;
            }
            else if ((currentPageNumber_RequestForRejectOrder.ToString() == lblTotalPages_RequestForRejectOrder.Text) && (currentPageNumber_RequestForRejectOrder == 1))
            {
                lnkbtnFirst_RequestForRejectOrder.Enabled = false;
                lnkbtnPre_RequestForRejectOrder.Enabled = false;
                lnkbtnNext_RequestForRejectOrder.Enabled = false;
                lnkbtnLast_RequestForRejectOrder.Enabled = false;
            }
            else if ((currentPageNumber_RequestForRejectOrder.ToString() == lblTotalPages_RequestForRejectOrder.Text) && (currentPageNumber_RequestForRejectOrder > 1))
            {
                lnkbtnFirst_RequestForRejectOrder.Enabled = true;
                lnkbtnPre_RequestForRejectOrder.Enabled = true;
                lnkbtnNext_RequestForRejectOrder.Enabled = false;
                lnkbtnLast_RequestForRejectOrder.Enabled = false;
            }
            else
            {
                lnkbtnFirst_RequestForRejectOrder.Enabled = true;
                lnkbtnPre_RequestForRejectOrder.Enabled = true;
                lnkbtnNext_RequestForRejectOrder.Enabled = true;
                lnkbtnLast_RequestForRejectOrder.Enabled = true;
            }
            
        }

        private void setDDl_RequestForRejectOrder(DropDownList ddls, String val)
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

        protected void GetPageIndex_RequestForRejectOrder(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_RequestForRejectOrder = 1;
                    break;

                case "Previous":
                    currentPageNumber_RequestForRejectOrder = Int32.Parse(ddlPage_RequestForRejectOrder.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_RequestForRejectOrder = Int32.Parse(ddlPage_RequestForRejectOrder.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_RequestForRejectOrder = Int32.Parse(lblTotalPages_RequestForRejectOrder.Text);
                    break;
            }


            LoadOrder_RequestForRejectOrder();
        }

        protected void ddlPage_SelectedIndexChanged_RequestForRejectOrder(object sender, EventArgs e)
        {
            currentPageNumber_RequestForRejectOrder = Int32.Parse(ddlPage_RequestForRejectOrder.SelectedValue);

            LoadOrder_RequestForRejectOrder();
        }

        #endregion Paging (Request For Edit Order)
        #endregion Request For Reject Order

    }
}
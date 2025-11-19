using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using AjaxControlToolkit;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace DOMS_TSR.src.CallOrderManagementRetail
{
    public partial class CallOrderManagementRetail : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl;
        string Codelist_NewOrder = "";

        protected static int currentPageNumber;
        protected static int currentPageNumber_NewOrder;
        protected static int currentPageNumber_PreOrder;
        protected static int currentPageNumber_Cooking;
        protected static int currentPageNumber_Cooked;
        protected static int currentPageNumber_Delivering;
        protected static int currentPageNumber_Delivered;
        protected static int currentPageNumber_OrderCancelled;
        protected static int currentPageNumber_OrderChanged;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";

        protected static string NewOrder = ConfigurationManager.AppSettings["OrderStateCookNewOrder"].ToString();
        protected static string Cooking = ConfigurationManager.AppSettings["OrderStateCookCooking"].ToString();
        protected static string Cooked = ConfigurationManager.AppSettings["OrderStateCookCooked"].ToString();
        protected static string Delivering = ConfigurationManager.AppSettings["OrderStateCookDelivering"].ToString();
        protected static string Delivered = ConfigurationManager.AppSettings["OrderStateCookDelivered"].ToString();
        protected static string OrderCancelled = ConfigurationManager.AppSettings["OrderStateCookOrderCancelled"].ToString();
        protected static string OrderChanged = ConfigurationManager.AppSettings["OrderStateCookOrderChanged"].ToString();
        protected static string PreOrder = ConfigurationManager.AppSettings["OrderStateCookPreOrder"].ToString();

        public Boolean check_All = false;
        public Boolean check_NewOrder = false;
        public Boolean check_PreOrder = false;
        public Boolean check_Cooking = false;
        public Boolean check_Cooked = false;
        public Boolean check_Delivering = false;
        public Boolean check_Delivered = false;
        public Boolean check_OrderCancelled = false;
        public Boolean check_OrderChanged = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                currentPageNumber = 1;
                currentPageNumber_NewOrder = 1;
                currentPageNumber_Cooking = 1;
                currentPageNumber_Cooked = 1;
                currentPageNumber_Delivering = 1;
                currentPageNumber_Delivered = 1;
                currentPageNumber_OrderCancelled = 1;
                currentPageNumber_OrderChanged = 1;

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

                sectionLoad_All();

                
                LoadOrder_NewOrder();
                
                LoadOrder_Delivering();
                LoadOrder_Delivered();
                LoadOrder_OrderCancelled();
                LoadOrder_OrderChanged();

            }
        }

        #region Main
        #region Function (Main)

        public void Hide_Section()
        {
            
            showAll(false);
            showNewOrder(false);
            showCooking(false);
            showCooked(false);
            showDelivering(false);
            showDelivered(false);
            showOrderCancelled(false);
            showOrderChanged(false);
        }

        public void showAll(Boolean show)
        {
            btnSecondary();
            showSection_All.CssClass = "btn btn-primary";

            searchSection_All.Visible = show;
            Section_All.Visible = show;
        }

        public void showNewOrder(Boolean show)
        {
            btnSecondary();
            showSection_NewOrder.CssClass = "btn btn-primary";

            searchSection_NewOrder.Visible = show;
            Section_NewOrder.Visible = show;
        }


        public void showCooking(Boolean show)
        {
            btnSecondary();
            showSection_Cooking.CssClass = "btn btn-primary";

            searchSection_Cooking.Visible = show;
            Section_Cooking.Visible = show;
        }

        public void showCooked(Boolean show)
        {
            btnSecondary();
            showSection_Cooked.CssClass = "btn btn-primary";

            searchSection_Cooked.Visible = show;
            Section_Cooked.Visible = show;
        }

        public void showDelivering(Boolean show)
        {
            btnSecondary();
            showSection_Delivering.CssClass = "btn btn-primary";

            searchSection_Delivering.Visible = show;
            Section_Delivering.Visible = show;
        }
        public void showDelivered(Boolean show)
        {
            btnSecondary();
            showSection_Delivered.CssClass = "btn btn-primary";

            searchSection_Delivered.Visible = show;
            Section_Delivered.Visible = show;
        }
        public void showOrderCancelled(Boolean show)
        {
            btnSecondary();
            showSection_OrderCancelled.CssClass = "btn btn-primary";

            searchSection_OrderCancelled.Visible = show;
            Section_OrderCancelled.Visible = show;
        }
        public void showOrderChanged(Boolean show)
        {
            btnSecondary();
            showSection_OrderChanged.CssClass = "btn btn-primary";

            searchSection_OrderChanged.Visible = show;
            Section_OrderChanged.Visible = show;
        }

        public void reLoadAnySection()
        {
            LoadOrder_All();
            LoadOrder_NewOrder();
            LoadOrder_Cooking();
            LoadOrder_Cooked();
            LoadOrder_Delivering();
            LoadOrder_Delivered();
            LoadOrder_OrderCancelled();
            LoadOrder_OrderChanged();
        }

        public void sectionLoad_All()
        {

            showSection_All.CssClass = "btn btn-primary";
            showAll(true);

            BindddlSearchOrderStatus_All();
            BindddlSearchOrderType_All();
            BindddlSearchBranch_All();
            BindddlSearchChannel_All();
            BindddlSearchCamCate_All();
            LoadOrder_All();

        }

        public void sectionLoad_NewOrder()
        {

            showSection_NewOrder.CssClass = "btn btn-primary";
            showNewOrder(true);

            BindddlSearchOrderType_NewOrder();
            BindddlSearchOrderStatus_NewOrder();
            BindddlSearchBranch_NewOrder();
            BindddlSearchChannel_NewOrder();

            ddlSearchOrderStatus_NewOrder.SelectedValue = StaticField.OrderStatus_01; 
            ddlSearchOrderStatus_NewOrder.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_NewOrder();

            LoadOrder_NewOrder();

        }


        public void sectionLoad_Cooking()
        {

            showSection_Cooking.CssClass = "btn btn-primary";
            showCooking(true);

            BindddlSearchOrderType_Cooking();
            BindddlSearchOrderStatus_Cooking();
            BindddlSearchBranch_Cooking();
            BindddlSearchChannel_Cooking();

            ddlSearchOrderStatus_Cooking.SelectedValue = StaticField.OrderStatus_02; 
            ddlSearchOrderStatus_Cooking.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Cooking();

            LoadOrder_Cooking();


        }

        public void sectionLoad_Cooked()
        {
            showSection_Cooked.CssClass = "btn btn-primary";
            showCooked(true);

            BindddlSearchOrderType_Cooked();
            BindddlSearchOrderStatus_Cooked();
            BindddlSearchBranch_Cooked();
            BindddlSearchChannel_Cooked();

            ddlSearchOrderStatus_Cooked.SelectedValue = StaticField.OrderStatus_03; 
            ddlSearchOrderStatus_Cooked.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Cooked();

            LoadOrder_Cooked();
        }

        public void sectionLoad_Delivering()
        {
            showSection_Delivering.CssClass = "btn btn-primary";
            showDelivering(true);

            BindddlSearchOrderType_Delivering();
            BindddlSearchOrderStatus_Delivering();
            BindddlSearchBranch_Delivering();
            BindddlSearchChannel_Delivering();

            ddlSearchOrderStatus_Delivering.SelectedValue = StaticField.OrderStatus_04; 
            ddlSearchOrderStatus_Delivering.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Delivering();

            LoadOrder_Delivering();
        }

        public void sectionLoad_Delivered()
        {
            showSection_Delivered.CssClass = "btn btn-primary";
            showDelivered(true);

            BindddlSearchOrderType_Delivered();
            BindddlSearchOrderStatus_Delivered();
            BindddlSearchBranch_Delivered();
            BindddlSearchChannel_Delivered();

            ddlSearchOrderStatus_Delivered.SelectedValue = StaticField.OrderStatus_05; 
            ddlSearchOrderStatus_Delivered.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Delivered();

            LoadOrder_Delivered();
        }

        public void sectionLoad_OrderCancelled()
        {
            showSection_OrderCancelled.CssClass = "btn btn-primary";
            showOrderCancelled(true);

            BindddlSearchOrderType_OrderCancelled();
            BindddlSearchOrderStatus_OrderCancelled();
            BindddlSearchBranch_OrderCancelled();
            BindddlSearchChannel_OrderCancelled();

            ddlSearchOrderStatus_OrderCancelled.SelectedValue = StaticField.OrderStatus_06; 
            ddlSearchOrderStatus_OrderCancelled.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_OrderCancelled();

            LoadOrder_OrderCancelled();
        }

        public void sectionLoad_OrderChanged()
        {
            showSection_OrderChanged.CssClass = "btn btn-primary";
            showOrderChanged(true);

            BindddlSearchOrderType_OrderChanged();
            BindddlSearchOrderStatus_OrderChanged();
            BindddlSearchBranch_OrderChanged();
            BindddlSearchChannel_OrderChanged();

            ddlSearchOrderStatus_OrderChanged.SelectedValue = StaticField.OrderStatus_07; 
            ddlSearchOrderStatus_OrderChanged.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_OrderChanged();

            LoadOrder_OrderChanged();
        }

        public void btnSecondary()
        {
            if (check_All == true) { }
            else { showSection_All.CssClass = "btn-8bar-disable"; }

            if (check_NewOrder == true) { }
            else { showSection_NewOrder.CssClass = "  btn-8bar-disable2"; }

            if (check_Cooking == true) { }
            else { showSection_Cooking.CssClass = "btn-8bar-disable4"; }

            if (check_Cooked == true) { }
            else { showSection_Cooked.CssClass = "btn-8bar-disable5"; }

            if (check_Delivering == true) { }
            else { showSection_Delivering.CssClass = "btn-8bar-disable6"; }

            if (check_Delivered == true) { }
            else { showSection_Delivered.CssClass = "btn-8bar-disable7"; }

            if (check_OrderCancelled == true) { }
            else { showSection_OrderCancelled.CssClass = "btn-8bar-disable8"; }

            if (check_OrderChanged == true) { }
            else { showSection_OrderChanged.CssClass = "btn-8bar-disable9"; }

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

        protected void testsound_Click(object sender, EventArgs e)
        {
            LoadSound();

        }


        protected void LoadSound()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"EvalSound('audio1');");
            sb.Append(@"</script>");
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "JCall1", sb.ToString(), false);


            


        }

        

        #endregion Function (Main)

        #region Events (Main)
        protected void showSection_All_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_All();
        }

        protected void showSection_NewOrder_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_NewOrder();
        }


        protected void showSection_Cooking_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Cooking();
        }

        protected void showSection_Cooked_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Cooked();
        }

        protected void showSection_Delivering_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Delivering();
        }

        protected void showSection_Delivered_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Delivered();
        }

        protected void showSection_OrderCancelled_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_OrderCancelled();
        }

        protected void showSection_OrderChanged_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_OrderChanged();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string msg = hidordermsg.Value;

            LoadOrder_All();
            LoadOrder_NewOrder();
            LoadOrder_Cooking();
            LoadOrder_Cooked();
            LoadOrder_Delivering();
            LoadOrder_Delivered();
            LoadOrder_OrderCancelled();
            LoadOrder_OrderChanged();            

            if (msg == NewOrder) 
            {
                check_NewOrder = true;
                showSection_NewOrder.CssClass = "btn btn-danger";
            }
            else if (msg == Cooking) 
            {
                check_Cooking = true;
                showSection_Cooking.CssClass = "btn btn-danger";
            }
            else if (msg == Cooked) 
            {
                check_Cooked = true;
                showSection_Cooked.CssClass = "btn btn-danger";
            }
            else if (msg == Delivering) 
            {
                check_Delivering = true;
                showSection_Delivering.CssClass = "btn btn-danger";
            }
            else if (msg == Delivered) 
            {
                check_Delivered = true;
                showSection_Delivered.CssClass = "btn btn-danger";
            }
            else if (msg == OrderCancelled) 
            {
                check_OrderCancelled = true;
                showSection_OrderCancelled.CssClass = "btn btn-danger";
            }
            else if (msg == OrderChanged) 
            {
                check_OrderChanged = true;
                showSection_OrderChanged.CssClass = "btn btn-danger";
            }

            LoadSound();
            


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

        #region All
        #region Function (All)
        protected void LoadOrder_All()
        {
            List<OrderInfo> lOrderInfo_All = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_All();

            

            countSection_All.Text = totalRow.ToString();

            SetPageBar(Convert.ToDouble(totalRow));

            lOrderInfo_All = GetOrderMasterByCriteria_All();

            gvOrder_All.DataSource = lOrderInfo_All;
            gvOrder_All.DataBind();

        }

        public int? CountOrderMasterList_All()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode.Text;

                data["CreateDate"] = txtSearchOrderDateFrom.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil.Text;

                data["CustomerCode"] = txtSearchCustomerCode.Text;

                data["CustomerFName"] = txtSearchFName.Text;

                data["CustomerLName"] = txtSearchLName.Text;

                data["OrderStatusCode"] = ddlSearchOrderStatus.SelectedValue == "-99" ? data["OrderStatusCode"] = "" : data["OrderStatusCode"] = ddlSearchOrderStatus.SelectedValue;

                data["OrderTypeCode"] = ddlSearchOrderType.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType.SelectedValue;

                data["CustomerContact"] = txtSearchContact.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo.Text;

                data["BranchCode"] = ddlSearchBranch.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_All()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode.Text;

                data["CreateDate"] = txtSearchOrderDateFrom.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil.Text;

                data["CustomerCode"] = txtSearchCustomerCode.Text;

                data["CustomerFName"] = txtSearchFName.Text;

                data["CustomerLName"] = txtSearchLName.Text;

                data["OrderStatusCode"] = ddlSearchOrderStatus.SelectedValue == "-99" ? data["OrderStatusCode"] = "" : data["OrderStatusCode"] = ddlSearchOrderStatus.SelectedValue;

                data["OrderTypeCode"] = ddlSearchOrderType.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType.SelectedValue;

                data["CustomerContact"] = txtSearchContact.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo.Text;

                data["BranchCode"] = ddlSearchBranch.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_All()
        {
            txtSearchOrderCode.Text = "";
            txtSearchCustomerCode.Text = "";
            txtSearchOrderDateFrom.Text = "";
            txtSearchOrderDateUntil.Text = "";
            txtSearchCustomerCode.Text = "";
            txtSearchFName.Text = "";
            txtSearchLName.Text = "";
            ddlSearchOrderType.SelectedValue = "-99";
            ddlSearchOrderStatus.SelectedValue = "-99";
            txtSearchContact.Text = "";
            txtSearchDeliverDate.Text = "";
            txtSearchDeliverDateTo.Text = "";
            ddlSearchBranch.SelectedValue = "-99";
            ddlSearchChannel.SelectedValue = "-99";
            ddlSearchCamCate.SelectedValue = "-99";
        }


        #endregion Function (All)

        #region Events (All)
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadOrder_All();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            clearSearch_All();
        }

        protected void btntab_Click(object sender, EventArgs e)
        {
            string tabno = hidTabNo.Value;
        }

        #endregion Events (All)

        #region Binding (All)

        protected void BindddlSearchOrderType_All()
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

            ddlSearchOrderType.DataSource = lLookupInfo;
            ddlSearchOrderType.DataTextField = "LookupValue";
            ddlSearchOrderType.DataValueField = "LookupCode";
            ddlSearchOrderType.DataBind();
            ddlSearchOrderType.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_All()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus.DataSource = lLookupInfo;
            ddlSearchOrderStatus.DataTextField = "LookupValue";
            ddlSearchOrderStatus.DataValueField = "LookupCode";
            ddlSearchOrderStatus.DataBind();
            ddlSearchOrderStatus.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_All()
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

            ddlSearchBranch.DataSource = bLookupInfo;
            ddlSearchBranch.DataTextField = "BranchName";
            ddlSearchBranch.DataValueField = "BranchCode";
            ddlSearchBranch.DataBind();
            ddlSearchBranch.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_All()
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

            ddlSearchChannel.DataSource = cLookupInfo;
            ddlSearchChannel.DataTextField = "ChannelName";
            ddlSearchChannel.DataValueField = "ChannelCode";
            ddlSearchChannel.DataBind();
            ddlSearchChannel.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_All()
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

            ddlSearchCamCate.DataSource = camLookupInfo;
            ddlSearchCamCate.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate.DataBind();
            ddlSearchCamCate.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (All)

        #region Paging (All)

        protected void SetPageBar(double totalRow)
        {

            lblTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages.Text) + 1; i++)
            {
                ddlPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlPage, currentPageNumber.ToString());
            

            
            if ((currentPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber == 1))
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber > 1))
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            
        }

        private void setDDl(DropDownList ddls, String val)
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

        protected void GetPageIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber = 1;
                    break;

                case "Previous":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber = Int32.Parse(lblTotalPages.Text);
                    break;
            }


            LoadOrder_All();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadOrder_All();
        }




        #endregion Paging (All)
        #endregion All

        #region New Order
        #region Function (New Order)
        protected void LoadOrder_NewOrder()
        {
            List<OrderInfo> lOrderInfo_NewOrder = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_NewOrder();

            countSection_NewOrder.Text = totalRow.ToString();

            SetPageBar_NewOrder(Convert.ToDouble(totalRow));

            lOrderInfo_NewOrder = GetOrderMasterByCriteria_NewOrder();

            gvOrder_NewOrder.DataSource = lOrderInfo_NewOrder;
            gvOrder_NewOrder.DataBind();

        }

        public int? CountOrderMasterList_NewOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_NewOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NewOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NewOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_NewOrder.Text;

                data["CustomerFName"] = txtSearchFName_NewOrder.Text;

                data["CustomerLName"] = txtSearchLName_NewOrder.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_01; 

                data["OrderTypeCode"] = ddlSearchOrderType_NewOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_NewOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_NewOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NewOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NewOrder.Text;

                data["BranchCode"] = ddlSearchBranch_NewOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_NewOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_NewOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_NewOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_NewOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_NewOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NewOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NewOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_NewOrder.Text;

                data["CustomerFName"] = txtSearchFName_NewOrder.Text;

                data["CustomerLName"] = txtSearchLName_NewOrder.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_01; 

                data["OrderTypeCode"] = ddlSearchOrderType_NewOrder.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_NewOrder.SelectedValue;

                data["CustomerContact"] = txtSearchContact_NewOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NewOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NewOrder.Text;

                data["BranchCode"] = ddlSearchBranch_NewOrder.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_NewOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_NewOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_NewOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_NewOrder - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_NewOrder()
        {
            txtSearchOrderCode_NewOrder.Text = "";
            txtSearchCustomerCode_NewOrder.Text = "";
            txtSearchOrderDateFrom_NewOrder.Text = "";
            txtSearchOrderDateUntil_NewOrder.Text = "";
            txtSearchCustomerCode_NewOrder.Text = "";
            txtSearchFName_NewOrder.Text = "";
            txtSearchLName_NewOrder.Text = "";
            ddlSearchOrderType_NewOrder.SelectedValue = "-99";
            txtSearchContact_NewOrder.Text = "";
            txtSearchDeliverDate_NewOrder.Text = "";
            txtSearchDeliverDateTo_NewOrder.Text = "";
            ddlSearchBranch_NewOrder.SelectedValue = "-99";
            ddlSearchChannel_NewOrder.SelectedValue = "-99";
            ddlSearchCamCate_NewOrder.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_NewOrder(string OrderCode)
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
        public int? sumTotalPriceOrderDetail_NewOrder(string OrderCode)
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


        #endregion Function (New Order)

        #region Events (New Order)
        protected void btnSearch_Click_NewOrder(object sender, EventArgs e)
        {
            LoadOrder_NewOrder();
        }

        protected void btnClearSearch_Click_NewOrder(object sender, EventArgs e)
        {
            clearSearch_NewOrder();
        }


        protected void btnCancelRejectForRequest_NewOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject').modal('hide');", true);
        }

        #endregion Events (New Order)

        #region Binding (New Order)

        protected void BindddlSearchOrderType_NewOrder()
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

            ddlSearchOrderType_NewOrder.DataSource = lLookupInfo;
            ddlSearchOrderType_NewOrder.DataTextField = "LookupValue";
            ddlSearchOrderType_NewOrder.DataValueField = "LookupCode";
            ddlSearchOrderType_NewOrder.DataBind();
            ddlSearchOrderType_NewOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_NewOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus_NewOrder.DataSource = lLookupInfo;
            ddlSearchOrderStatus_NewOrder.DataTextField = "LookupValue";
            ddlSearchOrderStatus_NewOrder.DataValueField = "LookupCode";
            ddlSearchOrderStatus_NewOrder.DataBind();
            ddlSearchOrderStatus_NewOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_NewOrder()
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

            ddlSearchBranch_NewOrder.DataSource = bLookupInfo;
            ddlSearchBranch_NewOrder.DataTextField = "BranchName";
            ddlSearchBranch_NewOrder.DataValueField = "BranchCode";
            ddlSearchBranch_NewOrder.DataBind();
            ddlSearchBranch_NewOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_NewOrder()
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

            ddlSearchChannel_NewOrder.DataSource = cLookupInfo;
            ddlSearchChannel_NewOrder.DataTextField = "ChannelName";
            ddlSearchChannel_NewOrder.DataValueField = "ChannelCode";
            ddlSearchChannel_NewOrder.DataBind();
            ddlSearchChannel_NewOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_NewOrder()
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

            ddlSearchCamCate_NewOrder.DataSource = camLookupInfo;
            ddlSearchCamCate_NewOrder.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_NewOrder.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_NewOrder.DataBind();
            ddlSearchCamCate_NewOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (New Order)

        #region Paging (New Order)

        protected void SetPageBar_NewOrder(double totalRow)
        {

            lblTotalPages_NewOrder.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_NewOrder.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_NewOrder.Text) + 1; i++)
            {
                ddlPage_NewOrder.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_NewOrder(ddlPage_NewOrder, currentPageNumber_NewOrder.ToString());
            

            
            if ((currentPageNumber_NewOrder == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_NewOrder.Enabled = false;
                lnkbtnPre_NewOrder.Enabled = false;
                lnkbtnNext_NewOrder.Enabled = true;
                lnkbtnLast_NewOrder.Enabled = true;
            }
            else if ((currentPageNumber_NewOrder.ToString() == lblTotalPages_NewOrder.Text) && (currentPageNumber_NewOrder == 1))
            {
                lnkbtnFirst_NewOrder.Enabled = false;
                lnkbtnPre_NewOrder.Enabled = false;
                lnkbtnNext_NewOrder.Enabled = false;
                lnkbtnLast_NewOrder.Enabled = false;
            }
            else if ((currentPageNumber_NewOrder.ToString() == lblTotalPages_NewOrder.Text) && (currentPageNumber_NewOrder > 1))
            {
                lnkbtnFirst_NewOrder.Enabled = true;
                lnkbtnPre_NewOrder.Enabled = true;
                lnkbtnNext_NewOrder.Enabled = false;
                lnkbtnLast_NewOrder.Enabled = false;
            }
            else
            {
                lnkbtnFirst_NewOrder.Enabled = true;
                lnkbtnPre_NewOrder.Enabled = true;
                lnkbtnNext_NewOrder.Enabled = true;
                lnkbtnLast_NewOrder.Enabled = true;
            }
            
        }

        private void setDDl_NewOrder(DropDownList ddls, String val)
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

        protected void GetPageIndex_NewOrder(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_NewOrder = 1;
                    break;

                case "Previous":
                    currentPageNumber_NewOrder = Int32.Parse(ddlPage_NewOrder.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_NewOrder = Int32.Parse(ddlPage_NewOrder.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_NewOrder = Int32.Parse(lblTotalPages_NewOrder.Text);
                    break;
            }


            LoadOrder_NewOrder();
        }

        protected void ddlPage_SelectedIndexChanged_NewOrder(object sender, EventArgs e)
        {
            currentPageNumber_NewOrder = Int32.Parse(ddlPage_NewOrder.SelectedValue);

            LoadOrder_NewOrder();
        }

        #endregion Paging (New Order)
        #endregion New Order


        #region Cooking
        #region Function (Cooking)
        protected void LoadOrder_Cooking()
        {
            List<OrderInfo> lOrderInfo_Cooking = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_Cooking();

            countSection_Cooking.Text = totalRow.ToString();

            SetPageBar_Cooking(Convert.ToDouble(totalRow));

            lOrderInfo_Cooking = GetOrderMasterByCriteria_Cooking();

            gvOrder_Cooking.DataSource = lOrderInfo_Cooking;
            gvOrder_Cooking.DataBind();

        }

        public int? CountOrderMasterList_Cooking()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Cooking.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Cooking.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Cooking.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Cooking.Text;

                data["CustomerFName"] = txtSearchFName_Cooking.Text;

                data["CustomerLName"] = txtSearchLName_Cooking.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_02; 

                data["OrderTypeCode"] = ddlSearchOrderType_Cooking.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Cooking.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Cooking.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Cooking.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Cooking.Text;

                data["BranchCode"] = ddlSearchBranch_Cooking.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Cooking.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Cooking.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Cooking.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_Cooking()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Cooking.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Cooking.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Cooking.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Cooking.Text;

                data["CustomerFName"] = txtSearchFName_Cooking.Text;

                data["CustomerLName"] = txtSearchLName_Cooking.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_02; 

                data["OrderTypeCode"] = ddlSearchOrderType_Cooking.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Cooking.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Cooking.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Cooking.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Cooking.Text;

                data["BranchCode"] = ddlSearchBranch_Cooking.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Cooking.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Cooking.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Cooking.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Cooking - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_Cooking()
        {
            txtSearchOrderCode_Cooking.Text = "";
            txtSearchCustomerCode_Cooking.Text = "";
            txtSearchOrderDateFrom_Cooking.Text = "";
            txtSearchOrderDateUntil_Cooking.Text = "";
            txtSearchCustomerCode_Cooking.Text = "";
            txtSearchFName_Cooking.Text = "";
            txtSearchLName_Cooking.Text = "";
            ddlSearchOrderType_Cooking.SelectedValue = "-99";
            txtSearchContact_Cooking.Text = "";
            txtSearchDeliverDate_Cooking.Text = "";
            txtSearchDeliverDateTo_Cooking.Text = "";
            ddlSearchBranch_Cooking.SelectedValue = "-99";
            ddlSearchChannel_Cooking.SelectedValue = "-99";
            ddlSearchCamCate_Cooking.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_Cooking(string OrderCode)
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

        public int? sumTotalPriceOrderDetail_Cooking(string OrderCode)
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

        public void UpdateRequestForReject_Cooking(string OrderCode)
        {
            if (ddlOrderRejectStatus_Cooking.SelectedValue == "-99")
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._MSG_PLEASEINSERT + " เหตุผลปฏิเสธการสั่งซื้อ" + "')", true);
            }

            else
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateOrderInfo";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["OrderCode"] = OrderCode;

                    data["OrderRejectRemark"] = areaOrderRejectStatus_Cooking.Text;

                    data["OrderStatusCode"] = "07";

                    data["UpdateBy"] = hidEmpCode.Value;

                    data["ORDERREJECTSTATUS"] = ddlOrderRejectStatus_Cooking.SelectedValue == "-99" ? data["ORDERREJECTSTATUS"] = "" : data["ORDERREJECTSTATUS"] = ddlOrderRejectStatus_Cooking.SelectedValue;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }


            }
        }

        #endregion Function (Cooking)

        #region Events (Cooking)
        protected void btnSearch_Click_Cooking(object sender, EventArgs e)
        {
            LoadOrder_Cooking();
        }

        protected void btnClearSearch_Click_Cooking(object sender, EventArgs e)
        {
            clearSearch_Cooking();
        }

        protected void gvOrder_Cooking_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvOrder_Cooking.Rows[index];

            HiddenField hidOrderId_Cooking = (HiddenField)row.FindControl("hidOrderId_Cooking");
            HiddenField hidOrderCode_Cooking = (HiddenField)row.FindControl("hidOrderCode_Cooking");
            HiddenField hidSaleOrderTypeName_Cooking = (HiddenField)row.FindControl("hidSaleOrderTypeName_Cooking");
            HiddenField hidCustomerName_Cooking = (HiddenField)row.FindControl("hidCustomerName_Cooking");
            HiddenField hidCustomerContact_Cooking = (HiddenField)row.FindControl("hidCustomerContact_Cooking");
            HiddenField hidCreateDate_Cooking = (HiddenField)row.FindControl("hidCreateDate_Cooking");


            if (e.CommandName == "ShowRequestReject_Cooking")
            {
                areaOrderRejectStatus_Cooking.Text = "";

                BindddlOrderRejectStatus_Cooking();

                lblOrderCode_Cooking.Text = hidOrderCode_Cooking.Value;
                lblSaleOrderTypeName_Cooking.Text = hidSaleOrderTypeName_Cooking.Value;
                lblCustomerName_Cooking.Text = "คุณ " + hidCustomerName_Cooking.Value;
                lblCustomerContact_Cooking.Text = hidCustomerContact_Cooking.Value;
                lblOrderDate_Cooking.Text = hidCreateDate_Cooking.Value;

                int? sumAmout = sumAmoutOrderDetail_Cooking(hidOrderCode_Cooking.Value);
                lblsumAmount_Cooking.Text = sumAmout != null ? "ราคารวม " + "(" + sumAmout + " รายการ" + ")" : "";
                

                int? sumTotalPrice = sumTotalPriceOrderDetail_Cooking(hidOrderCode_Cooking.Value);
                lblsumTotalPrice_Cooking.Text = sumTotalPrice != null ? String.Format("{0:n}", sumTotalPriceOrderDetail_Cooking(hidOrderCode_Cooking.Value)) + " บาท" : "";
                

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_Cooking').modal();", true);
            }

        }

        protected void btnSubmitRejectForRequest_Cooking_Click(object sender, EventArgs e)
        {
            UpdateRequestForReject_Cooking(lblOrderCode_Cooking.Text);
            reLoadAnySection();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modalRequestForReject_Cooking').modal('hide');", true);
        }

        protected void btnCancelRejectForRequest_Cooking_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_Cooking').modal('hide');", true);
        }

        #endregion Events (Cooking)

        #region Binding (Cooking)


        protected void BindddlSearchOrderType_Cooking()
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

            ddlSearchOrderType_Cooking.DataSource = lLookupInfo;
            ddlSearchOrderType_Cooking.DataTextField = "LookupValue";
            ddlSearchOrderType_Cooking.DataValueField = "LookupCode";
            ddlSearchOrderType_Cooking.DataBind();
            ddlSearchOrderType_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_Cooking()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus_Cooking.DataSource = lLookupInfo;
            ddlSearchOrderStatus_Cooking.DataTextField = "LookupValue";
            ddlSearchOrderStatus_Cooking.DataValueField = "LookupCode";
            ddlSearchOrderStatus_Cooking.DataBind();
            ddlSearchOrderStatus_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_Cooking()
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

            ddlSearchBranch_Cooking.DataSource = bLookupInfo;
            ddlSearchBranch_Cooking.DataTextField = "BranchName";
            ddlSearchBranch_Cooking.DataValueField = "BranchCode";
            ddlSearchBranch_Cooking.DataBind();
            ddlSearchBranch_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_Cooking()
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

            ddlSearchChannel_Cooking.DataSource = cLookupInfo;
            ddlSearchChannel_Cooking.DataTextField = "ChannelName";
            ddlSearchChannel_Cooking.DataValueField = "ChannelCode";
            ddlSearchChannel_Cooking.DataBind();
            ddlSearchChannel_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Cooking()
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

            ddlSearchCamCate_Cooking.DataSource = camLookupInfo;
            ddlSearchCamCate_Cooking.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_Cooking.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_Cooking.DataBind();
            ddlSearchCamCate_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlOrderRejectStatus_Cooking()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERREJECTSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlOrderRejectStatus_Cooking.DataSource = lLookupInfo;
            ddlOrderRejectStatus_Cooking.DataTextField = "LookupValue";
            ddlOrderRejectStatus_Cooking.DataValueField = "LookupCode";
            ddlOrderRejectStatus_Cooking.DataBind();
            ddlOrderRejectStatus_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        #endregion Binding (Cooking)

        #region Paging (Cooking)

        protected void SetPageBar_Cooking(double totalRow)
        {

            lblTotalPages_Cooking.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_Cooking.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_Cooking.Text) + 1; i++)
            {
                ddlPage_Cooking.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Cooking(ddlPage_Cooking, currentPageNumber_Cooking.ToString());
            

            
            if ((currentPageNumber_Cooking == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_Cooking.Enabled = false;
                lnkbtnPre_Cooking.Enabled = false;
                lnkbtnNext_Cooking.Enabled = true;
                lnkbtnLast_Cooking.Enabled = true;
            }
            else if ((currentPageNumber_Cooking.ToString() == lblTotalPages_Cooking.Text) && (currentPageNumber_Cooking == 1))
            {
                lnkbtnFirst_Cooking.Enabled = false;
                lnkbtnPre_Cooking.Enabled = false;
                lnkbtnNext_Cooking.Enabled = false;
                lnkbtnLast_Cooking.Enabled = false;
            }
            else if ((currentPageNumber_Cooking.ToString() == lblTotalPages_Cooking.Text) && (currentPageNumber_Cooking > 1))
            {
                lnkbtnFirst_Cooking.Enabled = true;
                lnkbtnPre_Cooking.Enabled = true;
                lnkbtnNext_Cooking.Enabled = false;
                lnkbtnLast_Cooking.Enabled = false;
            }
            else
            {
                lnkbtnFirst_Cooking.Enabled = true;
                lnkbtnPre_Cooking.Enabled = true;
                lnkbtnNext_Cooking.Enabled = true;
                lnkbtnLast_Cooking.Enabled = true;
            }
            
        }

        private void setDDl_Cooking(DropDownList ddls, String val)
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

        protected void GetPageIndex_Cooking(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_Cooking = 1;
                    break;

                case "Previous":
                    currentPageNumber_Cooking = Int32.Parse(ddlPage_Cooking.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_Cooking = Int32.Parse(ddlPage_Cooking.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_Cooking = Int32.Parse(lblTotalPages_Cooking.Text);
                    break;
            }


            LoadOrder_Cooking();
        }

        protected void ddlPage_SelectedIndexChanged_Cooking(object sender, EventArgs e)
        {
            currentPageNumber_Cooking = Int32.Parse(ddlPage_Cooking.SelectedValue);

            LoadOrder_Cooking();
        }




        #endregion Paging (Cooking)
        #endregion Cooking

        #region Cooked
        #region Function (Cooked)
        protected void LoadOrder_Cooked()
        {
            List<OrderInfo> lOrderInfo_Cooked = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_Cooked();

            countSection_Cooked.Text = totalRow.ToString();

            SetPageBar_Cooked(Convert.ToDouble(totalRow));

            lOrderInfo_Cooked = GetOrderMasterByCriteria_Cooked();

            gvOrder_Cooked.DataSource = lOrderInfo_Cooked;
            gvOrder_Cooked.DataBind();

        }

        public int? CountOrderMasterList_Cooked()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Cooked.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Cooked.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Cooked.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Cooked.Text;

                data["CustomerFName"] = txtSearchFName_Cooked.Text;

                data["CustomerLName"] = txtSearchLName_Cooked.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_03; 

                data["OrderTypeCode"] = ddlSearchOrderType_Cooked.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Cooked.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Cooked.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Cooked.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Cooked.Text;

                data["BranchCode"] = ddlSearchBranch_Cooked.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Cooked.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Cooked.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Cooked.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_Cooked()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Cooked.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Cooked.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Cooked.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Cooked.Text;

                data["CustomerFName"] = txtSearchFName_Cooked.Text;

                data["CustomerLName"] = txtSearchLName_Cooked.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_03; 

                data["OrderTypeCode"] = ddlSearchOrderType_Cooked.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Cooked.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Cooked.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Cooked.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Cooked.Text;

                data["BranchCode"] = ddlSearchBranch_Cooked.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Cooked.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Cooked.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Cooked.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Cooked - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_Cooked()
        {
            txtSearchOrderCode_Cooked.Text = "";
            txtSearchCustomerCode_Cooked.Text = "";
            txtSearchOrderDateFrom_Cooked.Text = "";
            txtSearchOrderDateUntil_Cooked.Text = "";
            txtSearchCustomerCode_Cooked.Text = "";
            txtSearchFName_Cooked.Text = "";
            txtSearchLName_Cooked.Text = "";
            ddlSearchOrderType_Cooked.SelectedValue = "-99";
            txtSearchContact_Cooked.Text = "";
            txtSearchDeliverDate_Cooked.Text = "";
            txtSearchDeliverDateTo_Cooked.Text = "";
            ddlSearchBranch_Cooked.SelectedValue = "-99";
            ddlSearchChannel_Cooked.SelectedValue = "-99";
            ddlSearchCamCate_Cooked.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_Cooked(string OrderCode)
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

        public int? sumTotalPriceOrderDetail_Cooked(string OrderCode)
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

        public void UpdateRequestForReject_Cooked(string OrderCode)
        {
            if (ddlOrderRejectStatus_Cooked.SelectedValue == "-99")
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._MSG_PLEASEINSERT + " เหตุผลปฏิเสธการสั่งซื้อ" + "')", true);
            }

            else
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateOrderInfo";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["OrderCode"] = OrderCode;

                    data["OrderRejectRemark"] = areaOrderRejectStatus_Cooked.Text;

                    data["OrderStatusCode"] = StaticField.OrderStatus_07; 

                    data["UpdateBy"] = hidEmpCode.Value;

                    data["ORDERREJECTSTATUS"] = ddlOrderRejectStatus_Cooked.SelectedValue == "-99" ? data["ORDERREJECTSTATUS"] = "" : data["ORDERREJECTSTATUS"] = ddlOrderRejectStatus_Cooked.SelectedValue;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }


            }
        }

        #endregion Function (Cooked)

        #region Events (Cooked)
        protected void btnSearch_Click_Cooked(object sender, EventArgs e)
        {
            LoadOrder_Cooked();
        }

        protected void btnClearSearch_Click_Cooked(object sender, EventArgs e)
        {
            clearSearch_Cooked();
        }

        protected void gvOrder_Cooked_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvOrder_Cooked.Rows[index];

            HiddenField hidOrderId_Cooked = (HiddenField)row.FindControl("hidOrderId_Cooked");
            HiddenField hidOrderCode_Cooked = (HiddenField)row.FindControl("hidOrderCode_Cooked");
            HiddenField hidSaleOrderTypeName_Cooked = (HiddenField)row.FindControl("hidSaleOrderTypeName_Cooked");
            HiddenField hidCustomerName_Cooked = (HiddenField)row.FindControl("hidCustomerName_Cooked");
            HiddenField hidCustomerContact_Cooked = (HiddenField)row.FindControl("hidCustomerContact_Cooked");
            HiddenField hidCreateDate_Cooked = (HiddenField)row.FindControl("hidCreateDate_Cooked");


            if (e.CommandName == "ShowRequestReject_Cooked")
            {
                areaOrderRejectStatus_Cooked.Text = "";

                BindddlOrderRejectStatus_Cooked();

                lblOrderCode_Cooked.Text = hidOrderCode_Cooked.Value;
                lblSaleOrderTypeName_Cooked.Text = hidSaleOrderTypeName_Cooked.Value;
                lblCustomerName_Cooked.Text = "คุณ " + hidCustomerName_Cooked.Value;
                lblCustomerContact_Cooked.Text = hidCustomerContact_Cooked.Value;
                lblOrderDate_Cooked.Text = hidCreateDate_Cooked.Value;

                int? sumAmout = sumAmoutOrderDetail_Cooked(hidOrderCode_Cooked.Value);
                lblsumAmount_Cooked.Text = sumAmout != null ? "ราคารวม " + "(" + sumAmout + " รายการ" + ")" : "";
                

                int? sumTotalPrice = sumTotalPriceOrderDetail_Cooked(hidOrderCode_Cooked.Value);
                lblsumTotalPrice_Cooked.Text = sumTotalPrice != null ? String.Format("{0:n}", sumTotalPriceOrderDetail_Cooked(hidOrderCode_Cooked.Value)) + " บาท" : "";
                

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_Cooked').modal();", true);
            }

        }

        protected void btnSubmitRejectForRequest_Cooked_Click(object sender, EventArgs e)
        {
            UpdateRequestForReject_Cooked(lblOrderCode_Cooked.Text);
            reLoadAnySection();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modalRequestForReject_Cooked').modal('hide');", true);
        }

        protected void btnCancelRejectForRequest_Cooked_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_Cooked').modal('hide');", true);
        }

        #endregion Events (Cooked)

        #region Binding (Cooked)

        protected void BindddlSearchOrderType_Cooked()
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

            ddlSearchOrderType_Cooked.DataSource = lLookupInfo;
            ddlSearchOrderType_Cooked.DataTextField = "LookupValue";
            ddlSearchOrderType_Cooked.DataValueField = "LookupCode";
            ddlSearchOrderType_Cooked.DataBind();
            ddlSearchOrderType_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_Cooked()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus_Cooked.DataSource = lLookupInfo;
            ddlSearchOrderStatus_Cooked.DataTextField = "LookupValue";
            ddlSearchOrderStatus_Cooked.DataValueField = "LookupCode";
            ddlSearchOrderStatus_Cooked.DataBind();
            ddlSearchOrderStatus_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_Cooked()
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

            ddlSearchBranch_Cooked.DataSource = bLookupInfo;
            ddlSearchBranch_Cooked.DataTextField = "BranchName";
            ddlSearchBranch_Cooked.DataValueField = "BranchCode";
            ddlSearchBranch_Cooked.DataBind();
            ddlSearchBranch_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_Cooked()
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

            ddlSearchChannel_Cooked.DataSource = cLookupInfo;
            ddlSearchChannel_Cooked.DataTextField = "ChannelName";
            ddlSearchChannel_Cooked.DataValueField = "ChannelCode";
            ddlSearchChannel_Cooked.DataBind();
            ddlSearchChannel_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Cooked()
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

            ddlSearchCamCate_Cooked.DataSource = camLookupInfo;
            ddlSearchCamCate_Cooked.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_Cooked.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_Cooked.DataBind();
            ddlSearchCamCate_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlOrderRejectStatus_Cooked()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERREJECTSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlOrderRejectStatus_Cooked.DataSource = lLookupInfo;
            ddlOrderRejectStatus_Cooked.DataTextField = "LookupValue";
            ddlOrderRejectStatus_Cooked.DataValueField = "LookupCode";
            ddlOrderRejectStatus_Cooked.DataBind();
            ddlOrderRejectStatus_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        #endregion Binding (Cooked)

        #region Paging (Cooked)

        protected void SetPageBar_Cooked(double totalRow)
        {

            lblTotalPages_Cooked.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_Cooked.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_Cooked.Text) + 1; i++)
            {
                ddlPage_Cooked.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Cooked(ddlPage_Cooked, currentPageNumber_Cooked.ToString());
            

            
            if ((currentPageNumber_Cooked == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_Cooked.Enabled = false;
                lnkbtnPre_Cooked.Enabled = false;
                lnkbtnNext_Cooked.Enabled = true;
                lnkbtnLast_Cooked.Enabled = true;
            }
            else if ((currentPageNumber_Cooked.ToString() == lblTotalPages_Cooked.Text) && (currentPageNumber_Cooked == 1))
            {
                lnkbtnFirst_Cooked.Enabled = false;
                lnkbtnPre_Cooked.Enabled = false;
                lnkbtnNext_Cooked.Enabled = false;
                lnkbtnLast_Cooked.Enabled = false;
            }
            else if ((currentPageNumber_Cooked.ToString() == lblTotalPages_Cooked.Text) && (currentPageNumber_Cooked > 1))
            {
                lnkbtnFirst_Cooked.Enabled = true;
                lnkbtnPre_Cooked.Enabled = true;
                lnkbtnNext_Cooked.Enabled = false;
                lnkbtnLast_Cooked.Enabled = false;
            }
            else
            {
                lnkbtnFirst_Cooked.Enabled = true;
                lnkbtnPre_Cooked.Enabled = true;
                lnkbtnNext_Cooked.Enabled = true;
                lnkbtnLast_Cooked.Enabled = true;
            }
            
        }

        private void setDDl_Cooked(DropDownList ddls, String val)
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

        protected void GetPageIndex_Cooked(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_Cooked = 1;
                    break;

                case "Previous":
                    currentPageNumber_Cooked = Int32.Parse(ddlPage_Cooked.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_Cooked = Int32.Parse(ddlPage_Cooked.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_Cooked = Int32.Parse(lblTotalPages_Cooked.Text);
                    break;
            }


            LoadOrder_Cooked();
        }

        protected void ddlPage_SelectedIndexChanged_Cooked(object sender, EventArgs e)
        {
            currentPageNumber_Cooked = Int32.Parse(ddlPage_Cooked.SelectedValue);

            LoadOrder_Cooked();
        }




        #endregion Paging (Cooked)
        #endregion Cooked

        #region Delivering
        #region Function (Delivering)
        protected void LoadOrder_Delivering()
        {
            List<OrderInfo> lOrderInfo_Delivering = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_Delivering();

            countSection_Delivering.Text = totalRow.ToString();

            SetPageBar_Delivering(Convert.ToDouble(totalRow));

            lOrderInfo_Delivering = GetOrderMasterByCriteria_Delivering();

            gvOrder_Delivering.DataSource = lOrderInfo_Delivering;
            gvOrder_Delivering.DataBind();

        }

        public int? CountOrderMasterList_Delivering()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Delivering.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Delivering.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Delivering.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Delivering.Text;

                data["CustomerFName"] = txtSearchFName_Delivering.Text;

                data["CustomerLName"] = txtSearchLName_Delivering.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_04; 

                data["OrderTypeCode"] = ddlSearchOrderType_Delivering.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Delivering.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Delivering.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Delivering.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Delivering.Text;

                data["BranchCode"] = ddlSearchBranch_Delivering.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Delivering.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Delivering.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Delivering.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_Delivering()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Delivering.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Delivering.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Delivering.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Delivering.Text;

                data["CustomerFName"] = txtSearchFName_Delivering.Text;

                data["CustomerLName"] = txtSearchLName_Delivering.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_04; 

                data["OrderTypeCode"] = ddlSearchOrderType_Delivering.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Delivering.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Delivering.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Delivering.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Delivering.Text;

                data["BranchCode"] = ddlSearchBranch_Delivering.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Delivering.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Delivering.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Delivering.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Delivering - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_Delivering()
        {
            txtSearchOrderCode_Delivering.Text = "";
            txtSearchCustomerCode_Delivering.Text = "";
            txtSearchOrderDateFrom_Delivering.Text = "";
            txtSearchOrderDateUntil_Delivering.Text = "";
            txtSearchCustomerCode_Delivering.Text = "";
            txtSearchFName_Delivering.Text = "";
            txtSearchLName_Delivering.Text = "";
            ddlSearchOrderType_Delivering.SelectedValue = "-99";
            txtSearchContact_Delivering.Text = "";
            txtSearchDeliverDate_Delivering.Text = "";
            txtSearchDeliverDateTo_Delivering.Text = "";
            ddlSearchBranch_Delivering.SelectedValue = "-99";
            ddlSearchChannel_Delivering.SelectedValue = "-99";
            ddlSearchCamCate_Delivering.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_Delivering(string OrderCode)
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

        public int? sumTotalPriceOrderDetail_Delivering(string OrderCode)
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

        public void UpdateRequestForReject_Delivering(string OrderCode)
        {
            if (ddlOrderRejectStatus_Delivering.SelectedValue == "-99")
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._MSG_PLEASEINSERT + " เหตุผลปฏิเสธการสั่งซื้อ" + "')", true);
            }

            else
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateOrderInfo";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["OrderCode"] = OrderCode;

                    data["OrderRejectRemark"] = areaOrderRejectStatus_Delivering.Text;

                    data["OrderStatusCode"] = StaticField.OrderStatus_07; 

                    data["UpdateBy"] = hidEmpCode.Value;

                    data["ORDERREJECTSTATUS"] = ddlOrderRejectStatus_Delivering.SelectedValue == "-99" ? data["ORDERREJECTSTATUS"] = "" : data["ORDERREJECTSTATUS"] = ddlOrderRejectStatus_Delivering.SelectedValue;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }


            }
        }

        #endregion Function (Delivering)

        #region Events (Delivering)
        protected void btnSearch_Click_Delivering(object sender, EventArgs e)
        {
            LoadOrder_Delivering();
        }

        protected void btnClearSearch_Click_Delivering(object sender, EventArgs e)
        {
            clearSearch_Delivering();
        }

        protected void BindddlSearchCamCate_Delivering()
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

            ddlSearchCamCate_Delivering.DataSource = camLookupInfo;
            ddlSearchCamCate_Delivering.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_Delivering.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_Delivering.DataBind();
            ddlSearchCamCate_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void gvOrder_Delivering_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvOrder_Delivering.Rows[index];

            HiddenField hidOrderId_Delivering = (HiddenField)row.FindControl("hidOrderId_Delivering");
            HiddenField hidOrderCode_Delivering = (HiddenField)row.FindControl("hidOrderCode_Delivering");
            HiddenField hidSaleOrderTypeName_Delivering = (HiddenField)row.FindControl("hidSaleOrderTypeName_Delivering");
            HiddenField hidCustomerName_Delivering = (HiddenField)row.FindControl("hidCustomerName_Delivering");
            HiddenField hidCustomerContact_Delivering = (HiddenField)row.FindControl("hidCustomerContact_Delivering");
            HiddenField hidCreateDate_Delivering = (HiddenField)row.FindControl("hidCreateDate_Delivering");


            if (e.CommandName == "ShowRequestReject_Delivering")
            {
                areaOrderRejectStatus_Delivering.Text = "";

                BindddlOrderRejectStatus_Delivering();

                lblOrderCode_Delivering.Text = hidOrderCode_Delivering.Value;
                lblSaleOrderTypeName_Delivering.Text = hidSaleOrderTypeName_Delivering.Value;
                lblCustomerName_Delivering.Text = "คุณ " + hidCustomerName_Delivering.Value;
                lblCustomerContact_Delivering.Text = hidCustomerContact_Delivering.Value;
                lblOrderDate_Delivering.Text = hidCreateDate_Delivering.Value;

                int? sumAmout = sumAmoutOrderDetail_Delivering(hidOrderCode_Delivering.Value);
                lblsumAmount_Delivering.Text = sumAmout != null ? "ราคารวม " + "(" + sumAmout + " รายการ" + ")" : "";
                

                int? sumTotalPrice = sumTotalPriceOrderDetail_Delivering(hidOrderCode_Delivering.Value);
                lblsumTotalPrice_Delivering.Text = sumTotalPrice != null ? String.Format("{0:n}", sumTotalPriceOrderDetail_Delivering(hidOrderCode_Delivering.Value)) + " บาท" : "";
                

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_Delivering').modal();", true);
            }

        }

        protected void btnSubmitRejectForRequest_Delivering_Click(object sender, EventArgs e)
        {
            UpdateRequestForReject_Delivering(lblOrderCode_Delivering.Text);
            reLoadAnySection();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modalRequestForReject_Delivering').modal('hide');", true);
        }

        protected void btnCancelRejectForRequest_Delivering_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_Delivering').modal('hide');", true);
        }

        #endregion Events (Delivering)

        #region Binding (Delivering)

        protected void BindddlSearchOrderType_Delivering()
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

            ddlSearchOrderType_Delivering.DataSource = lLookupInfo;
            ddlSearchOrderType_Delivering.DataTextField = "LookupValue";
            ddlSearchOrderType_Delivering.DataValueField = "LookupCode";
            ddlSearchOrderType_Delivering.DataBind();
            ddlSearchOrderType_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_Delivering()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus_Delivering.DataSource = lLookupInfo;
            ddlSearchOrderStatus_Delivering.DataTextField = "LookupValue";
            ddlSearchOrderStatus_Delivering.DataValueField = "LookupCode";
            ddlSearchOrderStatus_Delivering.DataBind();
            ddlSearchOrderStatus_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_Delivering()
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

            ddlSearchBranch_Delivering.DataSource = bLookupInfo;
            ddlSearchBranch_Delivering.DataTextField = "BranchName";
            ddlSearchBranch_Delivering.DataValueField = "BranchCode";
            ddlSearchBranch_Delivering.DataBind();
            ddlSearchBranch_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_Delivering()
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

            ddlSearchChannel_Delivering.DataSource = cLookupInfo;
            ddlSearchChannel_Delivering.DataTextField = "ChannelName";
            ddlSearchChannel_Delivering.DataValueField = "ChannelCode";
            ddlSearchChannel_Delivering.DataBind();
            ddlSearchChannel_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlOrderRejectStatus_Delivering()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERREJECTSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlOrderRejectStatus_Delivering.DataSource = lLookupInfo;
            ddlOrderRejectStatus_Delivering.DataTextField = "LookupValue";
            ddlOrderRejectStatus_Delivering.DataValueField = "LookupCode";
            ddlOrderRejectStatus_Delivering.DataBind();
            ddlOrderRejectStatus_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (Delivering)

        #region Paging (Delivering)

        protected void SetPageBar_Delivering(double totalRow)
        {

            lblTotalPages_Delivering.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_Delivering.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_Delivering.Text) + 1; i++)
            {
                ddlPage_Delivering.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Delivering(ddlPage_Delivering, currentPageNumber_Delivering.ToString());
            

            
            if ((currentPageNumber_Delivering == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_Delivering.Enabled = false;
                lnkbtnPre_Delivering.Enabled = false;
                lnkbtnNext_Delivering.Enabled = true;
                lnkbtnLast_Delivering.Enabled = true;
            }
            else if ((currentPageNumber_Delivering.ToString() == lblTotalPages_Delivering.Text) && (currentPageNumber_Delivering == 1))
            {
                lnkbtnFirst_Delivering.Enabled = false;
                lnkbtnPre_Delivering.Enabled = false;
                lnkbtnNext_Delivering.Enabled = false;
                lnkbtnLast_Delivering.Enabled = false;
            }
            else if ((currentPageNumber_Delivering.ToString() == lblTotalPages_Delivering.Text) && (currentPageNumber_Delivering > 1))
            {
                lnkbtnFirst_Delivering.Enabled = true;
                lnkbtnPre_Delivering.Enabled = true;
                lnkbtnNext_Delivering.Enabled = false;
                lnkbtnLast_Delivering.Enabled = false;
            }
            else
            {
                lnkbtnFirst_Delivering.Enabled = true;
                lnkbtnPre_Delivering.Enabled = true;
                lnkbtnNext_Delivering.Enabled = true;
                lnkbtnLast_Delivering.Enabled = true;
            }
            
        }

        private void setDDl_Delivering(DropDownList ddls, String val)
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

        protected void GetPageIndex_Delivering(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_Delivering = 1;
                    break;

                case "Previous":
                    currentPageNumber_Delivering = Int32.Parse(ddlPage_Delivering.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_Delivering = Int32.Parse(ddlPage_Delivering.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_Delivering = Int32.Parse(lblTotalPages_Delivering.Text);
                    break;
            }


            LoadOrder_Delivering();
        }

        protected void ddlPage_SelectedIndexChanged_Delivering(object sender, EventArgs e)
        {
            currentPageNumber_Delivering = Int32.Parse(ddlPage_Delivering.SelectedValue);

            LoadOrder_Delivering();
        }




        #endregion Paging (Delivering)
        #endregion Delivering

        #region Delivered
        #region Function (Delivered)
        protected void LoadOrder_Delivered()
        {
            List<OrderInfo> lOrderInfo_Delivered = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_Delivered();

            countSection_Delivered.Text = totalRow.ToString();

            SetPageBar_Delivered(Convert.ToDouble(totalRow));

            lOrderInfo_Delivered = GetOrderMasterByCriteria_Delivered();

            gvOrder_Delivered.DataSource = lOrderInfo_Delivered;
            gvOrder_Delivered.DataBind();

        }

        public int? CountOrderMasterList_Delivered()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Delivered.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Delivered.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Delivered.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Delivered.Text;

                data["CustomerFName"] = txtSearchFName_Delivered.Text;

                data["CustomerLName"] = txtSearchLName_Delivered.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_05; 

                data["OrderTypeCode"] = ddlSearchOrderType_Delivered.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Delivered.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Delivered.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Delivered.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Delivered.Text;

                data["BranchCode"] = ddlSearchBranch_Delivered.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Delivered.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Delivered.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Delivered.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_Delivered()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Delivered.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Delivered.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Delivered.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Delivered.Text;

                data["CustomerFName"] = txtSearchFName_Delivered.Text;

                data["CustomerLName"] = txtSearchLName_Delivered.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_05; 

                data["OrderTypeCode"] = ddlSearchOrderType_Delivered.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_Delivered.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Delivered.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_Delivered.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_Delivered.Text;

                data["BranchCode"] = ddlSearchBranch_Delivered.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_Delivered.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_Delivered.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_Delivered.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Delivered - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_Delivered()
        {
            txtSearchOrderCode_Delivered.Text = "";
            txtSearchCustomerCode_Delivered.Text = "";
            txtSearchOrderDateFrom_Delivered.Text = "";
            txtSearchOrderDateUntil_Delivered.Text = "";
            txtSearchCustomerCode_Delivered.Text = "";
            txtSearchFName_Delivered.Text = "";
            txtSearchLName_Delivered.Text = "";
            ddlSearchOrderType_Delivered.SelectedValue = "-99";
            txtSearchContact_Delivered.Text = "";
            txtSearchDeliverDate_Delivered.Text = "";
            txtSearchDeliverDateTo_Delivered.Text = "";
            ddlSearchBranch_Delivered.SelectedValue = "-99";
            ddlSearchChannel_Delivered.SelectedValue = "-99";
            ddlSearchCamCate_Delivered.SelectedValue = "-99";
        }


        #endregion Function (Delivered)

        #region Events (Delivered)
        protected void btnSearch_Click_Delivered(object sender, EventArgs e)
        {
            LoadOrder_Delivered();
        }

        protected void btnClearSearch_Click_Delivered(object sender, EventArgs e)
        {
            clearSearch_Delivered();
        }

        #endregion Events (Delivered)

        #region Binding (Delivered)

        protected void BindddlSearchOrderType_Delivered()
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

            ddlSearchOrderType_Delivered.DataSource = lLookupInfo;
            ddlSearchOrderType_Delivered.DataTextField = "LookupValue";
            ddlSearchOrderType_Delivered.DataValueField = "LookupCode";
            ddlSearchOrderType_Delivered.DataBind();
            ddlSearchOrderType_Delivered.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_Delivered()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus_Delivered.DataSource = lLookupInfo;
            ddlSearchOrderStatus_Delivered.DataTextField = "LookupValue";
            ddlSearchOrderStatus_Delivered.DataValueField = "LookupCode";
            ddlSearchOrderStatus_Delivered.DataBind();
            ddlSearchOrderStatus_Delivered.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_Delivered()
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

            ddlSearchBranch_Delivered.DataSource = bLookupInfo;
            ddlSearchBranch_Delivered.DataTextField = "BranchName";
            ddlSearchBranch_Delivered.DataValueField = "BranchCode";
            ddlSearchBranch_Delivered.DataBind();
            ddlSearchBranch_Delivered.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_Delivered()
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

            ddlSearchChannel_Delivered.DataSource = cLookupInfo;
            ddlSearchChannel_Delivered.DataTextField = "ChannelName";
            ddlSearchChannel_Delivered.DataValueField = "ChannelCode";
            ddlSearchChannel_Delivered.DataBind();
            ddlSearchChannel_Delivered.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Delivered()
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

            ddlSearchCamCate_Delivered.DataSource = camLookupInfo;
            ddlSearchCamCate_Delivered.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_Delivered.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_Delivered.DataBind();
            ddlSearchCamCate_Delivered.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        #endregion Binding (Delivered)

        #region Paging (Delivered)

        protected void SetPageBar_Delivered(double totalRow)
        {

            lblTotalPages_Delivered.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_Delivered.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_Delivered.Text) + 1; i++)
            {
                ddlPage_Delivered.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Delivered(ddlPage_Delivered, currentPageNumber_Delivered.ToString());
            

            
            if ((currentPageNumber_Delivered == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_Delivered.Enabled = false;
                lnkbtnPre_Delivered.Enabled = false;
                lnkbtnNext_Delivered.Enabled = true;
                lnkbtnLast_Delivered.Enabled = true;
            }
            else if ((currentPageNumber_Delivered.ToString() == lblTotalPages_Delivered.Text) && (currentPageNumber_Delivered == 1))
            {
                lnkbtnFirst_Delivered.Enabled = false;
                lnkbtnPre_Delivered.Enabled = false;
                lnkbtnNext_Delivered.Enabled = false;
                lnkbtnLast_Delivered.Enabled = false;
            }
            else if ((currentPageNumber_Delivered.ToString() == lblTotalPages_Delivered.Text) && (currentPageNumber_Delivered > 1))
            {
                lnkbtnFirst_Delivered.Enabled = true;
                lnkbtnPre_Delivered.Enabled = true;
                lnkbtnNext_Delivered.Enabled = false;
                lnkbtnLast_Delivered.Enabled = false;
            }
            else
            {
                lnkbtnFirst_Delivered.Enabled = true;
                lnkbtnPre_Delivered.Enabled = true;
                lnkbtnNext_Delivered.Enabled = true;
                lnkbtnLast_Delivered.Enabled = true;
            }
            
        }

        private void setDDl_Delivered(DropDownList ddls, String val)
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

        protected void GetPageIndex_Delivered(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_Delivered = 1;
                    break;

                case "Previous":
                    currentPageNumber_Delivered = Int32.Parse(ddlPage_Delivered.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_Delivered = Int32.Parse(ddlPage_Delivered.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_Delivered = Int32.Parse(lblTotalPages_Delivered.Text);
                    break;
            }


            LoadOrder_Delivered();
        }

        protected void ddlPage_SelectedIndexChanged_Delivered(object sender, EventArgs e)
        {
            currentPageNumber_Delivered = Int32.Parse(ddlPage_Delivered.SelectedValue);

            LoadOrder_Delivered();
        }




        #endregion Paging (Delivered)
        #endregion Delivered

        #region OrderCancelled
        #region Function (OrderCancelled)
        protected void LoadOrder_OrderCancelled()
        {
            List<OrderInfo> lOrderInfo_OrderCancelled = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_OrderCancelled();

            countSection_OrderCancelled.Text = totalRow.ToString();

            SetPageBar_OrderCancelled(Convert.ToDouble(totalRow));

            lOrderInfo_OrderCancelled = GetOrderMasterByCriteria_OrderCancelled();

            gvOrder_OrderCancelled.DataSource = lOrderInfo_OrderCancelled;
            gvOrder_OrderCancelled.DataBind();

        }

        public int? CountOrderMasterList_OrderCancelled()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_OrderCancelled.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_OrderCancelled.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_OrderCancelled.Text;

                data["CustomerCode"] = txtSearchCustomerCode_OrderCancelled.Text;

                data["CustomerFName"] = txtSearchFName_OrderCancelled.Text;

                data["CustomerLName"] = txtSearchLName_OrderCancelled.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_06; 

                data["OrderTypeCode"] = ddlSearchOrderType_OrderCancelled.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_OrderCancelled.SelectedValue;

                data["CustomerContact"] = txtSearchContact_OrderCancelled.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_OrderCancelled.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_OrderCancelled.Text;

                data["BranchCode"] = ddlSearchBranch_OrderCancelled.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_OrderCancelled.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_OrderCancelled.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_OrderCancelled.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_OrderCancelled()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_OrderCancelled.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_OrderCancelled.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_OrderCancelled.Text;

                data["CustomerCode"] = txtSearchCustomerCode_OrderCancelled.Text;

                data["CustomerFName"] = txtSearchFName_OrderCancelled.Text;

                data["CustomerLName"] = txtSearchLName_OrderCancelled.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_06; 

                data["OrderTypeCode"] = ddlSearchOrderType_OrderCancelled.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_OrderCancelled.SelectedValue;

                data["CustomerContact"] = txtSearchContact_OrderCancelled.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_OrderCancelled.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_OrderCancelled.Text;

                data["BranchCode"] = ddlSearchBranch_OrderCancelled.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_OrderCancelled.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_OrderCancelled.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_OrderCancelled.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_OrderCancelled - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_OrderCancelled()
        {
            txtSearchOrderCode_OrderCancelled.Text = "";
            txtSearchCustomerCode_OrderCancelled.Text = "";
            txtSearchOrderDateFrom_OrderCancelled.Text = "";
            txtSearchOrderDateUntil_OrderCancelled.Text = "";
            txtSearchCustomerCode_OrderCancelled.Text = "";
            txtSearchFName_OrderCancelled.Text = "";
            txtSearchLName_OrderCancelled.Text = "";
            ddlSearchOrderType_OrderCancelled.SelectedValue = "-99";
            txtSearchContact_OrderCancelled.Text = "";
            txtSearchDeliverDate_OrderCancelled.Text = "";
            txtSearchDeliverDateTo_OrderCancelled.Text = "";
            ddlSearchBranch_OrderCancelled.SelectedValue = "-99";
            ddlSearchChannel_OrderCancelled.SelectedValue = "-99";
            ddlSearchCamCate_OrderCancelled.SelectedValue = "-99";
        }


        #endregion Function (OrderCancelled)

        #region Events (OrderCancelled)
        protected void btnSearch_Click_OrderCancelled(object sender, EventArgs e)
        {
            LoadOrder_OrderCancelled();
        }

        protected void btnClearSearch_Click_OrderCancelled(object sender, EventArgs e)
        {
            clearSearch_OrderCancelled();
        }

        #endregion Events (OrderCancelled)

        #region Binding (OrderCancelled)

        protected void BindddlSearchOrderType_OrderCancelled()
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

            ddlSearchOrderType_OrderCancelled.DataSource = lLookupInfo;
            ddlSearchOrderType_OrderCancelled.DataTextField = "LookupValue";
            ddlSearchOrderType_OrderCancelled.DataValueField = "LookupCode";
            ddlSearchOrderType_OrderCancelled.DataBind();
            ddlSearchOrderType_OrderCancelled.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_OrderCancelled()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus_OrderCancelled.DataSource = lLookupInfo;
            ddlSearchOrderStatus_OrderCancelled.DataTextField = "LookupValue";
            ddlSearchOrderStatus_OrderCancelled.DataValueField = "LookupCode";
            ddlSearchOrderStatus_OrderCancelled.DataBind();
            ddlSearchOrderStatus_OrderCancelled.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_OrderCancelled()
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

            ddlSearchBranch_OrderCancelled.DataSource = bLookupInfo;
            ddlSearchBranch_OrderCancelled.DataTextField = "BranchName";
            ddlSearchBranch_OrderCancelled.DataValueField = "BranchCode";
            ddlSearchBranch_OrderCancelled.DataBind();
            ddlSearchBranch_OrderCancelled.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_OrderCancelled()
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

            ddlSearchChannel_OrderCancelled.DataSource = cLookupInfo;
            ddlSearchChannel_OrderCancelled.DataTextField = "ChannelName";
            ddlSearchChannel_OrderCancelled.DataValueField = "ChannelCode";
            ddlSearchChannel_OrderCancelled.DataBind();
            ddlSearchChannel_OrderCancelled.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_OrderCancelled()
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

            ddlSearchCamCate_OrderCancelled.DataSource = camLookupInfo;
            ddlSearchCamCate_OrderCancelled.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_OrderCancelled.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_OrderCancelled.DataBind();
            ddlSearchCamCate_OrderCancelled.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        #endregion Binding (OrderCancelled)

        #region Paging (OrderCancelled)

        protected void SetPageBar_OrderCancelled(double totalRow)
        {

            lblTotalPages_OrderCancelled.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_OrderCancelled.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_OrderCancelled.Text) + 1; i++)
            {
                ddlPage_OrderCancelled.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_OrderCancelled(ddlPage_OrderCancelled, currentPageNumber_OrderCancelled.ToString());
            

            
            if ((currentPageNumber_OrderCancelled == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_OrderCancelled.Enabled = false;
                lnkbtnPre_OrderCancelled.Enabled = false;
                lnkbtnNext_OrderCancelled.Enabled = true;
                lnkbtnLast_OrderCancelled.Enabled = true;
            }
            else if ((currentPageNumber_OrderCancelled.ToString() == lblTotalPages_OrderCancelled.Text) && (currentPageNumber_OrderCancelled == 1))
            {
                lnkbtnFirst_OrderCancelled.Enabled = false;
                lnkbtnPre_OrderCancelled.Enabled = false;
                lnkbtnNext_OrderCancelled.Enabled = false;
                lnkbtnLast_OrderCancelled.Enabled = false;
            }
            else if ((currentPageNumber_OrderCancelled.ToString() == lblTotalPages_OrderCancelled.Text) && (currentPageNumber_OrderCancelled > 1))
            {
                lnkbtnFirst_OrderCancelled.Enabled = true;
                lnkbtnPre_OrderCancelled.Enabled = true;
                lnkbtnNext_OrderCancelled.Enabled = false;
                lnkbtnLast_OrderCancelled.Enabled = false;
            }
            else
            {
                lnkbtnFirst_OrderCancelled.Enabled = true;
                lnkbtnPre_OrderCancelled.Enabled = true;
                lnkbtnNext_OrderCancelled.Enabled = true;
                lnkbtnLast_OrderCancelled.Enabled = true;
            }
            
        }

        private void setDDl_OrderCancelled(DropDownList ddls, String val)
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

        protected void GetPageIndex_OrderCancelled(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_OrderCancelled = 1;
                    break;

                case "Previous":
                    currentPageNumber_OrderCancelled = Int32.Parse(ddlPage_OrderCancelled.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_OrderCancelled = Int32.Parse(ddlPage_OrderCancelled.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_OrderCancelled = Int32.Parse(lblTotalPages_OrderCancelled.Text);
                    break;
            }


            LoadOrder_OrderCancelled();
        }

        protected void ddlPage_SelectedIndexChanged_OrderCancelled(object sender, EventArgs e)
        {
            currentPageNumber_OrderCancelled = Int32.Parse(ddlPage_OrderCancelled.SelectedValue);

            LoadOrder_OrderCancelled();
        }




        #endregion Paging (OrderCancelled)
        #endregion OrderCancelled

        #region OrderCancelled
        #region Function (OrderCancelled)
        protected void LoadOrder_OrderChanged()
        {
            List<OrderInfo> lOrderInfo_OrderChanged = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_OrderChanged();

            countSection_OrderChanged.Text = totalRow.ToString();

            SetPageBar_OrderChanged(Convert.ToDouble(totalRow));

            lOrderInfo_OrderChanged = GetOrderMasterByCriteria_OrderChanged();

            gvOrder_OrderChanged.DataSource = lOrderInfo_OrderChanged;
            gvOrder_OrderChanged.DataBind();


        }

        public int? CountOrderMasterList_OrderChanged()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_OrderChanged.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_OrderChanged.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_OrderChanged.Text;

                data["CustomerCode"] = txtSearchCustomerCode_OrderChanged.Text;

                data["CustomerFName"] = txtSearchFName_OrderChanged.Text;

                data["CustomerLName"] = txtSearchLName_OrderChanged.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_07; 

                data["OrderTypeCode"] = ddlSearchOrderType_OrderChanged.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_OrderChanged.SelectedValue;

                data["CustomerContact"] = txtSearchContact_OrderChanged.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_OrderChanged.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_OrderChanged.Text;

                data["BranchCode"] = ddlSearchBranch_OrderChanged.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_OrderChanged.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_OrderChanged.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_OrderChanged.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_OrderChanged.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_OrderChanged.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_OrderChanged()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_OrderChanged.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_OrderChanged.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_OrderChanged.Text;

                data["CustomerCode"] = txtSearchCustomerCode_OrderChanged.Text;

                data["CustomerFName"] = txtSearchFName_OrderChanged.Text;

                data["CustomerLName"] = txtSearchLName_OrderChanged.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_07; 

                data["OrderTypeCode"] = ddlSearchOrderType_OrderChanged.SelectedValue == "-99" ? data["OrderTypeCode"] = "" : data["OrderTypeCode"] = ddlSearchOrderType_OrderChanged.SelectedValue;

                data["CustomerContact"] = txtSearchContact_OrderChanged.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_OrderChanged.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_OrderChanged.Text;

                data["BranchCode"] = ddlSearchBranch_OrderChanged.SelectedValue == "-99" ? data["BranchCode"] = "" : data["BranchCode"] = ddlSearchBranch_OrderChanged.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_OrderChanged.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_OrderChanged.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_OrderChanged.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_OrderChanged.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_OrderChanged - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_OrderChanged()
        {
            txtSearchOrderCode_OrderChanged.Text = "";
            txtSearchCustomerCode_OrderChanged.Text = "";
            txtSearchOrderDateFrom_OrderChanged.Text = "";
            txtSearchOrderDateUntil_OrderChanged.Text = "";
            txtSearchCustomerCode_OrderChanged.Text = "";
            txtSearchFName_OrderChanged.Text = "";
            txtSearchLName_OrderChanged.Text = "";
            ddlSearchOrderType_OrderChanged.SelectedValue = "-99";
            txtSearchContact_OrderChanged.Text = "";
            txtSearchDeliverDate_OrderChanged.Text = "";
            txtSearchDeliverDateTo_OrderChanged.Text = "";
            ddlSearchBranch_OrderChanged.SelectedValue = "-99";
            ddlSearchChannel_OrderChanged.SelectedValue = "-99";
            ddlSearchCamCate_OrderChanged.SelectedValue = "-99";
        }


        #endregion Function (OrderCancelled)

        #region Events (OrderCancelled)
        protected void btnSearch_Click_OrderChanged(object sender, EventArgs e)
        {
            LoadOrder_OrderChanged();
        }

        protected void btnClearSearch_Click_OrderChanged(object sender, EventArgs e)
        {
            clearSearch_OrderChanged();
        }

        #endregion Events (OrderCancelled)

        #region Binding (OrderCancelled)

        protected void BindddlSearchOrderType_OrderChanged()
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

            ddlSearchOrderType_OrderChanged.DataSource = lLookupInfo;
            ddlSearchOrderType_OrderChanged.DataTextField = "LookupValue";
            ddlSearchOrderType_OrderChanged.DataValueField = "LookupCode";
            ddlSearchOrderType_OrderChanged.DataBind();
            ddlSearchOrderType_OrderChanged.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchOrderStatus_OrderChanged()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "ORDERSTATUS";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderStatus_OrderChanged.DataSource = lLookupInfo;
            ddlSearchOrderStatus_OrderChanged.DataTextField = "LookupValue";
            ddlSearchOrderStatus_OrderChanged.DataValueField = "LookupCode";
            ddlSearchOrderStatus_OrderChanged.DataBind();
            ddlSearchOrderStatus_OrderChanged.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchBranch_OrderChanged()
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

            ddlSearchBranch_OrderChanged.DataSource = bLookupInfo;
            ddlSearchBranch_OrderChanged.DataTextField = "BranchName";
            ddlSearchBranch_OrderChanged.DataValueField = "BranchCode";
            ddlSearchBranch_OrderChanged.DataBind();
            ddlSearchBranch_OrderChanged.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchChannel_OrderChanged()
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

            ddlSearchChannel_OrderChanged.DataSource = cLookupInfo;
            ddlSearchChannel_OrderChanged.DataTextField = "ChannelName";
            ddlSearchChannel_OrderChanged.DataValueField = "ChannelCode";
            ddlSearchChannel_OrderChanged.DataBind();
            ddlSearchChannel_OrderChanged.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_OrderChanged()
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

            ddlSearchCamCate_OrderChanged.DataSource = camLookupInfo;
            ddlSearchCamCate_OrderChanged.DataTextField = "CampaignCategoryName";
            ddlSearchCamCate_OrderChanged.DataValueField = "CampaignCategoryCode";
            ddlSearchCamCate_OrderChanged.DataBind();
            ddlSearchCamCate_OrderChanged.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        #endregion Binding (OrderCancelled)

        #region Paging (OrderCancelled)

        protected void SetPageBar_OrderChanged(double totalRow)
        {

            lblTotalPages_OrderChanged.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_OrderChanged.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_OrderChanged.Text) + 1; i++)
            {
                ddlPage_OrderChanged.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_OrderChanged(ddlPage_OrderChanged, currentPageNumber_OrderChanged.ToString());
            

            
            if ((currentPageNumber_OrderChanged == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_OrderChanged.Enabled = false;
                lnkbtnPre_OrderChanged.Enabled = false;
                lnkbtnNext_OrderChanged.Enabled = true;
                lnkbtnLast_OrderChanged.Enabled = true;
            }
            else if ((currentPageNumber_OrderChanged.ToString() == lblTotalPages_OrderChanged.Text) && (currentPageNumber_OrderChanged == 1))
            {
                lnkbtnFirst_OrderChanged.Enabled = false;
                lnkbtnPre_OrderChanged.Enabled = false;
                lnkbtnNext_OrderChanged.Enabled = false;
                lnkbtnLast_OrderChanged.Enabled = false;
            }
            else if ((currentPageNumber_OrderChanged.ToString() == lblTotalPages_OrderChanged.Text) && (currentPageNumber_OrderChanged > 1))
            {
                lnkbtnFirst_OrderChanged.Enabled = true;
                lnkbtnPre_OrderChanged.Enabled = true;
                lnkbtnNext_OrderChanged.Enabled = false;
                lnkbtnLast_OrderChanged.Enabled = false;
            }
            else
            {
                lnkbtnFirst_OrderChanged.Enabled = true;
                lnkbtnPre_OrderChanged.Enabled = true;
                lnkbtnNext_OrderChanged.Enabled = true;
                lnkbtnLast_OrderChanged.Enabled = true;
            }
            
        }

        private void setDDl_OrderChanged(DropDownList ddls, String val)
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

        protected void GetPageIndex_OrderChanged(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_OrderChanged = 1;
                    break;

                case "Previous":
                    currentPageNumber_OrderChanged = Int32.Parse(ddlPage_OrderChanged.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_OrderChanged = Int32.Parse(ddlPage_OrderChanged.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_OrderChanged = Int32.Parse(lblTotalPages_OrderChanged.Text);
                    break;
            }


            LoadOrder_OrderChanged();
        }

        protected void ddlPage_SelectedIndexChanged_OrderChanged(object sender, EventArgs e)
        {
            currentPageNumber_OrderChanged = Int32.Parse(ddlPage_OrderChanged.SelectedValue);

            LoadOrder_OrderChanged();
        }




        #endregion Paging (OrderCancelled)

        #endregion OrderCancelled

        protected void btnCancelOrder_NewOrder_Click(object sender, EventArgs e)
        {
            Changestatus_NewOrder();
            LoadOrder_NewOrder();
            LoadOrder_OrderCancelled();
        }


        #region Change status New Order 
        protected void chkAll_Change_NewOrder(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_NewOrder.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_NewOrder.HeaderRow.FindControl("chkAll_NewOrder");
                CheckBox chkProduct = (CheckBox)gvOrder_NewOrder.Rows[i].FindControl("chk_NewOrder");

                if (chkall.Checked == true)
                {
                    chkProduct.Checked = true;
                }
                else
                {
                    chkProduct.Checked = false;
                }
            }

        }

        protected void chkAll_Change_OrderChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_OrderChanged.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_OrderChanged.HeaderRow.FindControl("chkAll_OrderChanged");
                CheckBox chkProduct = (CheckBox)gvOrder_OrderChanged.Rows[i].FindControl("chk_OrderChanged");

                if (chkall.Checked == true)
                {
                    chkProduct.Checked = true;
                }
                else
                {
                    chkProduct.Checked = false;
                }
            }

        }

        protected Boolean Changestatus_NewOrder()
        {
            for (int i = 0; i < gvOrder_NewOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_NewOrder.Rows[i].FindControl("chk_NewOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_NewOrder.Rows[i].FindControl("hidOrderId_NewOrder");
                    HiddenField hidOrder = (HiddenField)gvOrder_NewOrder.Rows[i].FindControl("hidOrderCode_NewOrder");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = StaticField.OrderStatus_06 }); 
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

        #endregion
        #region Change status Send Rider 
        protected Boolean Changestatus_SendRider()
        {


            for (int i = 0; i < gvOrder_Cooking.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_Cooking.Rows[i].FindControl("chk_Cooking");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_Cooking.Rows[i].FindControl("hidOrderId_Cooking");
                    HiddenField hidOrder = (HiddenField)gvOrder_Cooking.Rows[i].FindControl("hidOrderCode_Cooking");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = StaticField.OrderStatus_06 }); 
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

        protected Boolean Changestatus_Cancel_Cooked()
        {


            for (int i = 0; i < gvOrder_Cooked.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_Cooked.Rows[i].FindControl("chk_Cooked");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_Cooked.Rows[i].FindControl("hidOrderId_Cooked");
                    HiddenField hidOrder = (HiddenField)gvOrder_Cooked.Rows[i].FindControl("hidOrderCode_Cooked");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = StaticField.OrderStatus_06 }); 
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

        protected Boolean Changestatus_Cancel_Delivering()
        {


            for (int i = 0; i < gvOrder_Delivering.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_Delivering.Rows[i].FindControl("chk_Delivering");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_Delivering.Rows[i].FindControl("hidOrderId_Delivering");
                    HiddenField hidOrder = (HiddenField)gvOrder_Delivering.Rows[i].FindControl("hidOrderCode_Delivering");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = StaticField.OrderStatus_06 }); 
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

        protected Boolean Changestatus_Cancel_OrderChanged()
        {


            for (int i = 0; i < gvOrder_OrderChanged.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_OrderChanged.Rows[i].FindControl("chk_OrderChanged");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_OrderChanged.Rows[i].FindControl("hidOrderId_OrderChanged");
                    HiddenField hidOrder = (HiddenField)gvOrder_OrderChanged.Rows[i].FindControl("hidOrderCode_OrderChanged");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = hidOrder.Value.ToString(), orderstatus = StaticField.OrderStatus_06 }); 
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

        
        #endregion
        protected void btnSubmitToRider_Cooking_Click(object sender, EventArgs e)
        {
            Changestatus_SendRider();
            LoadOrder_Cooking();
            LoadOrder_OrderCancelled();
        }

        protected void btnOrderCancel_Cooked_Click(object sender, EventArgs e)
        {
            Changestatus_Cancel_Cooked();
            LoadOrder_Cooked();
            LoadOrder_OrderCancelled();
        }

        protected void btnOrderCancel_Delivering_Click(object sender, EventArgs e)
        {
            Changestatus_Cancel_Delivering();
            LoadOrder_Delivering();
            LoadOrder_OrderCancelled();
        }

        protected void btnOrderCancel_OrderChanged_Click(object sender, EventArgs e)
        {
            Changestatus_Cancel_OrderChanged();
            LoadOrder_OrderChanged();
            LoadOrder_OrderCancelled();
        }
    }
}
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
using System.Text.RegularExpressions;

namespace DOMS_TSR.src.OrderManagement
{
    public partial class OrderManagement_GP : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
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
                MerchantInfo merchantInfo = new MerchantInfo();
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];
                hidMerchantCode.Value = merchantInfo.MerchantCode;
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

                sectionLoad_All();

                LoadOrder_NewOrder();
                LoadOrder_Cooking();
                LoadOrder_Cooked();
                LoadOrder_Delivering();
                LoadOrder_Delivered();
                LoadOrder_OrderCancelled();

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
        }

        public void showAll(Boolean show)
        {
            btnSecondary();
            showSection_All.CssClass = "btn btn-primary h-120";

            searchSection_All.Visible = show;
            Section_All.Visible = show;
        }

        public void showNewOrder(Boolean show)
        {
            btnSecondary();
            showSection_NewOrder.CssClass = "btn btn-primary h-120";

            searchSection_NewOrder.Visible = show;
            Section_NewOrder.Visible = show;
        }


        public void showCooking(Boolean show)
        {
            btnSecondary();
            showSection_Cooking.CssClass = "btn btn-primary h-120";

            searchSection_Cooking.Visible = show;
            Section_Cooking.Visible = show;
        }

        public void showCooked(Boolean show)
        {
            btnSecondary();
            showSection_Cooked.CssClass = "btn btn-primary h-120";

            searchSection_Cooked.Visible = show;
            Section_Cooked.Visible = show;
        }

        public void showDelivering(Boolean show)
        {
            btnSecondary();
            showSection_Delivering.CssClass = "btn btn-primary h-120";

            searchSection_Delivering.Visible = show;
            Section_Delivering.Visible = show;
        }
        public void showDelivered(Boolean show)
        {
            btnSecondary();
            showSection_Delivered.CssClass = "btn btn-primary h-120";

            searchSection_Delivered.Visible = show;
            Section_Delivered.Visible = show;
        }
        public void showOrderCancelled(Boolean show)
        {
            btnSecondary();
            showSection_OrderCancelled.CssClass = "btn btn-primary h-120";

            searchSection_OrderCancelled.Visible = show;
            Section_OrderCancelled.Visible = show;
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
        }

        public void sectionLoad_All()
        {

            showSection_All.CssClass = "btn btn-primary h-120";
            showAll(true);

            BindddlSearchOrderStatus_All();
            BindddlSearchCamCate_All();
            LoadOrder_All();

        }

        public void sectionLoad_NewOrder()
        {

            showSection_NewOrder.CssClass = "btn btn-primary h-120";
            showNewOrder(true);

            BindddlSearchOrderStatus_NewOrder();

            ddlSearchOrderState_NewOrder.SelectedValue = StaticField.OrderStatus_01; 
            ddlSearchOrderState_NewOrder.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_NewOrder();

            LoadOrder_NewOrder();

        }


        public void sectionLoad_Cooking()
        {

            showSection_Cooking.CssClass = "btn btn-primary h-120";
            showCooking(true);

            BindddlSearchOrderStatus_Cooking();

            ddlSearchOrderState_Cooking.SelectedValue = StaticField.OrderStatus_02; 
            ddlSearchOrderState_Cooking.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Cooking();

            LoadOrder_Cooking();


        }

        public void sectionLoad_Cooked()
        {
            showSection_Cooked.CssClass = "btn btn-primary h-120";
            showCooked(true);

            BindddlSearchOrderStatus_Cooked();

            ddlSearchOrderState_Cooked.SelectedValue = StaticField.OrderStatus_03; 
            ddlSearchOrderState_Cooked.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Cooked();

            LoadOrder_Cooked();
        }

        public void sectionLoad_Delivering()
        {
            showSection_Delivering.CssClass = "btn btn-primary h-120";
            showDelivering(true);

            BindddlSearchOrderStatus_Delivering();

            ddlSearchOrderState_Delivering.SelectedValue = StaticField.OrderStatus_04; 
            ddlSearchOrderState_Delivering.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Delivering();

            LoadOrder_Delivering();
        }

        public void sectionLoad_Delivered()
        {
            showSection_Delivered.CssClass = "btn btn-primary h-120";
            showDelivered(true);

            BindddlSearchOrderStatus_Delivered();

            ddlSearchOrderState_Delivered.SelectedValue = StaticField.OrderStatus_05; 
            ddlSearchOrderState_Delivered.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Delivered();

            LoadOrder_Delivered();
        }

        public void sectionLoad_OrderCancelled()
        {
            showSection_OrderCancelled.CssClass = "btn btn-primary h-120";
            showOrderCancelled(true);

            BindddlSearchOrderStatus_OrderCancelled();

            ddlSearchOrderState_OrderCancelled.SelectedValue = StaticField.OrderStatus_06; 
            ddlSearchOrderState_OrderCancelled.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_OrderCancelled();

            LoadOrder_OrderCancelled();
        }

        public void btnSecondary()
        {
            if (check_All == true) { }
            else { showSection_All.CssClass = "btn-8bar-disable  h-120"; }

            if (check_NewOrder == true) { }
            else { showSection_NewOrder.CssClass = "  btn-8bar-disable2 h-120"; }

            if (check_Cooking == true) { }
            else { showSection_Cooking.CssClass = "btn-8bar-disable4 h-120"; }

            if (check_Cooked == true) { }
            else { showSection_Cooked.CssClass = "btn-8bar-disable5 h-120"; }

            if (check_Delivering == true) { }
            else { showSection_Delivering.CssClass = "btn-8bar-disable6 h-120"; }

            if (check_Delivered == true) { }
            else { showSection_Delivered.CssClass = "btn-8bar-disable7 h-120"; }

            if (check_OrderCancelled == true) { }
            else { showSection_OrderCancelled.CssClass = "btn-8bar-disable8 h-120"; }

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

            switch (msg)
            {

                case "01":
                    check_NewOrder = true;
                    showSection_NewOrder.CssClass = "btn btn-danger";
                    break;

                case "02":
                    check_Cooking = true;
                    showSection_Cooking.CssClass = "btn btn-danger";
                    break;

                case "03":
                    check_Cooked = true;
                    showSection_Cooked.CssClass = "btn btn-danger";
                    break;

                case "04":
                    check_Delivering = true;
                    showSection_Delivering.CssClass = "btn btn-danger";
                    break;

                case "05":
                    check_Delivered = true;
                    showSection_Delivered.CssClass = "btn btn-danger";
                    break;

                case "06":
                    check_OrderCancelled = true;
                    showSection_OrderCancelled.CssClass = "btn btn-danger";
                    break;

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

                data["OrderStatusCode"] = ddlSearchOrderState.SelectedValue == "-99" ? data["OrderStatusCode"] = "" : data["OrderStatusCode"] = ddlSearchOrderState.SelectedValue;

                data["CustomerContact"] = txtSearchContact.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue;
             
                data["MerchantMapCode"] = hidMerchantCode.Value;
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

                data["OrderStatusCode"] = ddlSearchOrderState.SelectedValue == "-99" ? data["OrderStatusCode"] = "" : data["OrderStatusCode"] = ddlSearchOrderState.SelectedValue;

                data["CustomerContact"] = txtSearchContact.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCode.Value;
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
            ddlSearchOrderState.SelectedValue = "-99";
            txtSearchContact.Text = "";
            ddlSearchCamCate.SelectedValue = "-99";
        }


        #endregion Function (All)

        #region Events (All)
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                currentPageNumber = 1;
                LoadOrder_All();
            }
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

        protected void BindddlSearchOrderStatus_All()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderState.DataSource = lLookupInfo;
            ddlSearchOrderState.DataTextField = "LookupValue";
            ddlSearchOrderState.DataValueField = "LookupCode";
            ddlSearchOrderState.DataBind();
            ddlSearchOrderState.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_All()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchCamCate.DataSource = camLookupInfo;
            ddlSearchCamCate.DataTextField = "ProductBrandName";
            ddlSearchCamCate.DataValueField = "ProductBrandCode";
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

                data["OrderStatusCode"]  = StaticField.OrderStatus_01 + "," + StaticField.OrderStatus_10; 

                data["CustomerContact"] = txtSearchContact_NewOrder.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue;
                data["MerchantMapCode"] = hidMerchantCode.Value;
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

                data["OrderStatusCode"] = StaticField.OrderStatus_01 + "," + StaticField.OrderStatus_10; 

                data["CustomerContact"] = txtSearchContact_NewOrder.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NewOrder.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_NewOrder - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCode.Value;
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
            txtSearchContact_NewOrder.Text = "";
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

        protected void BindddlSearchOrderStatus_NewOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderState_NewOrder.DataSource = lLookupInfo;
            ddlSearchOrderState_NewOrder.DataTextField = "LookupValue";
            ddlSearchOrderState_NewOrder.DataValueField = "LookupCode";
            ddlSearchOrderState_NewOrder.DataBind();
            ddlSearchOrderState_NewOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_NewOrder()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);


            ddlSearchCamCate_NewOrder.DataSource = camLookupInfo;
            ddlSearchCamCate_NewOrder.DataTextField = "ProductBrandName";
            ddlSearchCamCate_NewOrder.DataValueField = "ProductBrandCode";
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

                data["CustomerContact"] = txtSearchContact_Cooking.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue;
                data["MerchantMapCode"] = hidMerchantCode.Value;
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

                data["CustomerContact"] = txtSearchContact_Cooking.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooking.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Cooking - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCode.Value;
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
            txtSearchContact_Cooking.Text = "";
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


        protected void btnCancelRejectForRequest_Cooking_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_Cooking').modal('hide');", true);
        }

        #endregion Events (Cooking)

        #region Binding (Cooking)


        protected void BindddlSearchOrderStatus_Cooking()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderState_Cooking.DataSource = lLookupInfo;
            ddlSearchOrderState_Cooking.DataTextField = "LookupValue";
            ddlSearchOrderState_Cooking.DataValueField = "LookupCode";
            ddlSearchOrderState_Cooking.DataBind();
            ddlSearchOrderState_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Cooking()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchCamCate_Cooking.DataSource = camLookupInfo;
            ddlSearchCamCate_Cooking.DataTextField = "ProductBrandName";
            ddlSearchCamCate_Cooking.DataValueField = "ProductBrandCode";
            ddlSearchCamCate_Cooking.DataBind();
            ddlSearchCamCate_Cooking.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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

                data["CustomerContact"] = txtSearchContact_Cooked.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue;
                data["MerchantMapCode"] = hidMerchantCode.Value;
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

                data["CustomerContact"] = txtSearchContact_Cooked.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Cooked.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Cooked - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCode.Value;
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
            txtSearchContact_Cooked.Text = "";
            ddlSearchCamCate_Cooked.SelectedValue = "-99";
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

        #endregion Events (Cooked)

        #region Binding (Cooked)

        protected void BindddlSearchOrderStatus_Cooked()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderState_Cooked.DataSource = lLookupInfo;
            ddlSearchOrderState_Cooked.DataTextField = "LookupValue";
            ddlSearchOrderState_Cooked.DataValueField = "LookupCode";
            ddlSearchOrderState_Cooked.DataBind();
            ddlSearchOrderState_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Cooked()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchCamCate_Cooked.DataSource = camLookupInfo;
            ddlSearchCamCate_Cooked.DataTextField = "ProductBrandCode";
            ddlSearchCamCate_Cooked.DataValueField = "ProductBrandName";
            ddlSearchCamCate_Cooked.DataBind();
            ddlSearchCamCate_Cooked.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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

                data["CustomerContact"] = txtSearchContact_Delivering.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue;
                data["MerchantMapCode"] = hidMerchantCode.Value;
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

                data["CustomerContact"] = txtSearchContact_Delivering.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivering.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Delivering - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCode.Value;
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
            txtSearchContact_Delivering.Text = "";
            ddlSearchCamCate_Delivering.SelectedValue = "-99";
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

        #endregion Events (Delivering)

        #region Binding (Delivering)

        protected void BindddlSearchOrderStatus_Delivering()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderState_Delivering.DataSource = lLookupInfo;
            ddlSearchOrderState_Delivering.DataTextField = "LookupValue";
            ddlSearchOrderState_Delivering.DataValueField = "LookupCode";
            ddlSearchOrderState_Delivering.DataBind();
            ddlSearchOrderState_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Delivering()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchCamCate_Delivering.DataSource = camLookupInfo;
            ddlSearchCamCate_Delivering.DataValueField = "ProductBrandCode";
            ddlSearchCamCate_Delivering.DataTextField = "ProductBrandName";
            ddlSearchCamCate_Delivering.DataBind();
            ddlSearchCamCate_Delivering.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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

                data["CustomerContact"] = txtSearchContact_Delivered.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue;
                data["MerchantMapCode"] = hidMerchantCode.Value;
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

                data["CustomerContact"] = txtSearchContact_Delivered.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Delivered.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_Delivered - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCode.Value;
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
            txtSearchContact_Delivered.Text = "";
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

        protected void BindddlSearchOrderStatus_Delivered()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderState_Delivered.DataSource = lLookupInfo;
            ddlSearchOrderState_Delivered.DataTextField = "LookupValue";
            ddlSearchOrderState_Delivered.DataValueField = "LookupCode";
            ddlSearchOrderState_Delivered.DataBind();
            ddlSearchOrderState_Delivered.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Delivered()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchCamCate_Delivered.DataSource = camLookupInfo;
            ddlSearchCamCate_Delivered.DataValueField = "ProductBrandCode";
            ddlSearchCamCate_Delivered.DataTextField = "ProductBrandName";
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

                data["CustomerContact"] = txtSearchContact_OrderCancelled.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue;
                data["MerchantMapCode"] = hidMerchantCode.Value;
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

                data["CustomerContact"] = txtSearchContact_OrderCancelled.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber_OrderCancelled - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCode.Value;
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
            txtSearchContact_OrderCancelled.Text = "";
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

        protected void BindddlSearchOrderStatus_OrderCancelled()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_ORDERSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderState_OrderCancelled.DataSource = lLookupInfo;
            ddlSearchOrderState_OrderCancelled.DataTextField = "LookupValue";
            ddlSearchOrderState_OrderCancelled.DataValueField = "LookupCode";
            ddlSearchOrderState_OrderCancelled.DataBind();
            ddlSearchOrderState_OrderCancelled.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_OrderCancelled()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> camLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchCamCate_OrderCancelled.DataSource = camLookupInfo;
            ddlSearchCamCate_OrderCancelled.DataValueField = "ProductBrandCode";
            ddlSearchCamCate_OrderCancelled.DataTextField = "ProductBrandName";
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
        protected Boolean validateSearch()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (regexItem.IsMatch(txtSearchOrderCode.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchOrderCode.Text = "";
            }
            else
            {
                flag = false;
                lblSearchOrderCode.Text = MessageConst._MSG_PLEASEINSERT + " รหัสใบสั่งขายต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchCustomerCode.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchCustomerCode.Text = "";
            }
            else
            {
                flag = false;
                lblSearchCustomerCode.Text = MessageConst._MSG_PLEASEINSERT + " รหัสลูกค้าต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchFName.Text) && regexItem.IsMatch(txtSearchLName.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchFName.Text = "";
                lblSearchLName.Text = "";
            }
            else
            {
                flag = false;
                lblSearchLName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ - สกุลต้องไม่มีอักขระพิเศษ";
            }
           
            if (regexItem.IsMatch(txtSearchContact.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchContact.Text = "";
            }
            else
            {
                flag = false;
                lblSearchContact.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์ติดต่อไม่มีอักขระพิเศษ";
            }
            return flag;
        }
    }
}
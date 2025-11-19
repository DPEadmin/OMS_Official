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

namespace DOMS_TSR.src.FullfillmentOrderManagement
{
    public partial class FFChangeGroupOrderStatus : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist_Setup = "";

        protected static int currentPageNumber;
        protected static int currentPageNumber_Setup;
        protected static int currentPageNumber_PreOrder;
        protected static int currentPageNumber_ReadyDeli;
        protected static int currentPageNumber_FinishDeli;
        protected static int currentPageNumber_Delivering;
        protected static int currentPageNumber_Delivered;
        protected static int currentPageNumber_OrderCancelled;
        protected static int currentPageNumber_OrderChanged;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";

        public Boolean check_Distribute = false;
        public Boolean check_Setup = false;
        public Boolean check_PreOrder = false;
        public Boolean check_ReadyDeli = false;
        public Boolean check_FinishDeli = false;
        public Boolean check_Delivering = false;
        public Boolean check_Delivered = false;
        public Boolean check_OrderCancelled = false;
        public Boolean check_OrderChanged = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                currentPageNumber = 1;
                currentPageNumber_Setup = 1;
                currentPageNumber_ReadyDeli = 1;
                currentPageNumber_FinishDeli = 1;
                currentPageNumber_Delivering = 1;
                currentPageNumber_Delivered = 1;
                currentPageNumber_OrderCancelled = 1;
                currentPageNumber_OrderChanged = 1;

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

                sectionLoad_Distribute();

                LoadOrder_Setup();
                LoadOrder_ReadyDeli();
                LoadOrder_FinishDeli();
           
                LoadOrder_OrderCancelled();

            }
        }

        #region Main
        #region Function (Main)

        public void Hide_Section()
        {
            showAll(false);
            showSetup(false);
            showCooking(false);
            showCooked(false);
         
            showOrderCancelled(false);
        }

        public void showAll(Boolean show)
        {
            btnSecondary();
            showSection_Distribute.CssClass = "btn btn-primary";

            searchSection_Distribute.Visible = show;
            Section_Distribute.Visible = show;
        }

        public void showSetup(Boolean show)
        {
            btnSecondary();
            showSection_Setup.CssClass = "btn btn-primary";

            searchSection_Setup.Visible = show;
            Section_Setup.Visible = show;
        }


        public void showCooking(Boolean show)
        {
            btnSecondary();
            showSection_ReadyDeli.CssClass = "btn btn-primary";

            searchSection_ReadyDeli.Visible = show;
            Section_ReadyDeli.Visible = show;
        }

        public void showCooked(Boolean show)
        {
            btnSecondary();
            showSection_FinishDeli.CssClass = "btn btn-primary";

            searchSection_FinishDeli.Visible = show;
            Section_FinishDeli.Visible = show;
        }


        public void showOrderCancelled(Boolean show)
        {
            btnSecondary();
            showSection_OrderCancelled.CssClass = "btn btn-primary";

            searchSection_OrderCancelled.Visible = show;
            Section_OrderCancelled.Visible = show;
        }

        public void reLoadAnySection()
        {
            LoadOrder_Distribute();
            LoadOrder_Setup();
            LoadOrder_ReadyDeli();
            LoadOrder_FinishDeli();
      
            LoadOrder_OrderCancelled();
        }

        public void sectionLoad_Distribute()
        {

            showSection_Distribute.CssClass = "btn btn-primary";
            showAll(true);

            BindddlSearchOrderStatus_Distribute();
            BindddlSearchCamCate_Distribute();
            LoadOrder_Distribute();

        }

        public void sectionLoad_Setup()
        {

            showSection_Setup.CssClass = "btn btn-primary";
            showSetup(true);

            BindddlSearchOrderStatus_Setup();

            ddlSearchOrderState_Setup.SelectedValue = StaticField.OrderStatus_02; 
            ddlSearchOrderState_Setup.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_Setup();

            LoadOrder_Setup();

        }


        public void sectionLoad_ReadyDeli()
        {

            showSection_ReadyDeli.CssClass = "btn btn-primary";
            showCooking(true);

            BindddlSearchOrderStatus_ReadyDeli();

            ddlSearchOrderState_ReadyDeli.SelectedValue = StaticField.OrderStatus_02; 
            ddlSearchOrderState_ReadyDeli.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_ReadyDeli();

            LoadOrder_ReadyDeli();


        }

        public void sectionLoad_FinishDeli()
        {
            showSection_FinishDeli.CssClass = "btn btn-primary";
            showCooked(true);

            BindddlSearchOrderStatus_FinishDeli();

            ddlSearchOrderState_FinishDeli.SelectedValue = StaticField.OrderStatus_03; 
            ddlSearchOrderState_FinishDeli.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_FinishDeli();

            LoadOrder_FinishDeli();
        }



        public void sectionLoad_OrderCancelled()
        {
            showSection_OrderCancelled.CssClass = "btn btn-primary";
            showOrderCancelled(true);

            BindddlSearchOrderStatus_OrderCancelled();

            ddlSearchOrderState_OrderCancelled.SelectedValue = StaticField.OrderStatus_06; 
            ddlSearchOrderState_OrderCancelled.Attributes.Add("disabled", "true");

            BindddlSearchCamCate_OrderCancelled();

            LoadOrder_OrderCancelled();
        }

        public void btnSecondary()
        {
            if (check_Distribute == true) { }
            else { showSection_Distribute.CssClass = "btn-8bar-disable"; }

            if (check_Setup == true) { }
            else { showSection_Setup.CssClass = "  btn-8bar-disable2"; }

            if (check_ReadyDeli == true) { }
            else { showSection_ReadyDeli.CssClass = "btn-8bar-disable4"; }

            if (check_FinishDeli == true) { }
            else { showSection_FinishDeli.CssClass = "btn-8bar-disable5"; }

          

            if (check_OrderCancelled == true) { }
            else { showSection_OrderCancelled.CssClass = "btn-8bar-disable8"; }

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
        protected void showSection_Distribute_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Distribute();
        }

        protected void showSection_Setup_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Setup();
        }


        protected void showSection_ReadyDeli_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_ReadyDeli();
        }

        protected void showSection_FinishDeli_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_FinishDeli();
        }

     

        protected void showSection_OrderCancelled_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_OrderCancelled();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string msg = hidordermsg.Value;

            LoadOrder_Distribute();
            LoadOrder_Setup();
            LoadOrder_ReadyDeli();
            LoadOrder_FinishDeli();
      
         
            LoadOrder_OrderCancelled();

            if (msg == StaticField.OrderMsg_01) 
            {
                check_Setup = true;
                showSection_Setup.CssClass = "btn btn-danger";
            }
            else if (msg == StaticField.OrderMsg_02) 
            {
                check_ReadyDeli = true;
                showSection_ReadyDeli.CssClass = "btn btn-danger";
            }
            else if (msg == StaticField.OrderMsg_03) 
            {
                check_FinishDeli = true;
                showSection_FinishDeli.CssClass = "btn btn-danger";
            }
            else if (msg == StaticField.OrderMsg_06) 
            {
                check_OrderCancelled = true;
                showSection_OrderCancelled.CssClass = "btn btn-danger";
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
        protected void LoadOrder_Distribute()
        {
            List<OrderInfo> lOrderInfo_Distribute = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_Distribute();

            

            countSection_Distribute.Text = totalRow.ToString();

            SetPageBar(Convert.ToDouble(totalRow));

            lOrderInfo_Distribute = GetOrderMasterByCriteria_Distribute();

            gvOrder_Distribute.DataSource = lOrderInfo_Distribute;
            gvOrder_Distribute.DataBind();

        }

        public int? CountOrderMasterList_Distribute()
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

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();

                data["OrderStatusCode"] = StaticField.OrderStatus_10; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_Distribute()
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

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["OrderStatusCode"] = "10";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_Distribute()
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
            if (ValidateSearch())
            {
                currentPageNumber = 1;
                LoadOrder_Distribute();
            }
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            clearSearch_Distribute();
        }

        protected void btntab_Click(object sender, EventArgs e)
        {
            string tabno = hidTabNo.Value;
        }

        protected void chkAll_Change_Distribute(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_Distribute.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_Distribute.HeaderRow.FindControl("chkAll_Order");
                CheckBox chk_NewOrder = (CheckBox)gvOrder_Distribute.Rows[i].FindControl("chk_ByOrder");

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
        protected Boolean Changestatus_Distribute()
        {
          

            for (int i = 0; i < gvOrder_Distribute.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_Distribute.Rows[i].FindControl("chk_ByOrder");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_Distribute.Rows[i].FindControl("hidOrderCode_GvvDistribute");
             
                    Label Lbordercode = (Label)gvOrder_Distribute.Rows[i].FindControl("lblOrderCode");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = StaticField.OrderState_15, orderstatus= StaticField.OrderStatus_02 }); 
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

        protected void btnAcceptOrder_Distribute_Click(object sender, EventArgs e)
        {
            Changestatus_Distribute();
            LoadOrder_Distribute();
        }
        #endregion Events (All)

        #region Binding (All)

        protected void BindddlSearchOrderStatus_Distribute()
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

            ddlSearchOrderState.DataSource = lLookupInfo;
            ddlSearchOrderState.DataTextField = "LookupValue";
            ddlSearchOrderState.DataValueField = "LookupCode";
            ddlSearchOrderState.DataBind();
            ddlSearchOrderState.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Distribute()
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


            LoadOrder_Distribute();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadOrder_Distribute();
        }




        #endregion Paging (All)
        #endregion All

        #region SetUp
        #region Function (New Order)
        protected void LoadOrder_Setup()
        {
            List<OrderInfo> lOrderInfo_Setup = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_Setup();

            countSection_Setup.Text = totalRow.ToString();

            SetPageBar_Setup(Convert.ToDouble(totalRow));

            lOrderInfo_Setup = GetOrderMasterByCriteria_Setup();

            gvOrder_Setup.DataSource = lOrderInfo_Setup;
            gvOrder_Setup.DataBind();

        }

        public int? CountOrderMasterList_Setup()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Setup.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Setup.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Setup.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Setup.Text;

                data["CustomerFName"] = txtSearchFName_Setup.Text;

                data["CustomerLName"] = txtSearchLName_Setup.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_02; 

                data["CustomerContact"] = txtSearchContact_Setup.Text;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["CampaignCategoryCode"] = ddlSearchCamCate_Setup.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Setup.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_Setup()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_Setup.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_Setup.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_Setup.Text;

                data["CustomerCode"] = txtSearchCustomerCode_Setup.Text;

                data["CustomerFName"] = txtSearchFName_Setup.Text;

                data["CustomerLName"] = txtSearchLName_Setup.Text;

                data["OrderStatusCode"] = ddlSearchOrderState_Setup.SelectedValue;

                data["CustomerContact"] = txtSearchContact_Setup.Text;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["CampaignCategoryCode"] = ddlSearchCamCate_Setup.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_Setup.SelectedValue;

                data["OrderStatusCode"] = StaticField.OrderStatus_02; 
                data["rowOFFSet"] = ((currentPageNumber_Setup - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_Setup()
        {
            txtSearchOrderCode_Setup.Text = "";
            txtSearchCustomerCode_Setup.Text = "";
            txtSearchOrderDateFrom_Setup.Text = "";
            txtSearchOrderDateUntil_Setup.Text = "";
            txtSearchCustomerCode_Setup.Text = "";
            txtSearchFName_Setup.Text = "";
            txtSearchLName_Setup.Text = "";
            txtSearchContact_Setup.Text = "";
            ddlSearchCamCate_Setup.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_Setup(string OrderCode)
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
        public int? sumTotalPriceOrderDetail_Setup(string OrderCode)
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
        protected void chkAll_Change_Setup(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_Distribute.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_Distribute.HeaderRow.FindControl("chkAll_Setup");
                CheckBox chk_NewOrder = (CheckBox)gvOrder_Distribute.Rows[i].FindControl("chk_Setup");

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
        protected Boolean Changestatus_SetUp()
        {


            for (int i = 0; i < gvOrder_Setup.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_Setup.Rows[i].FindControl("chk_Setup");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_Setup.Rows[i].FindControl("hidOrderCode_Setup");

                    Label Lbordercode = (Label)gvOrder_Setup.Rows[i].FindControl("lblOrderCode_Setup");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = StaticField.OrderState_05, orderstatus = StaticField.OrderStatus_04 }); 
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

        protected void btnAcceptOrder_Setup_Click(object sender, EventArgs e)
        {
            Changestatus_SetUp();
            LoadOrder_Setup();
            LoadOrder_ReadyDeli();
        }

        #endregion Function (New Order)

        #region Events (New Order)
        protected void btnSearch_Click_Setup(object sender, EventArgs e)
        {
            LoadOrder_Setup();
        }

        protected void btnClearSearch_Click_Setup(object sender, EventArgs e)
        {
            clearSearch_Setup();
        }


        protected void btnCancelRejectForRequest_Setup_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject').modal('hide');", true);
        }

        #endregion Events (New Order)

        #region Binding (New Order)

        protected void BindddlSearchOrderStatus_Setup()
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

            ddlSearchOrderState_Setup.DataSource = lLookupInfo;
            ddlSearchOrderState_Setup.DataTextField = "LookupValue";
            ddlSearchOrderState_Setup.DataValueField = "LookupCode";
            ddlSearchOrderState_Setup.DataBind();
            ddlSearchOrderState_Setup.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_Setup()
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


            ddlSearchCamCate_Setup.DataSource = camLookupInfo;
            ddlSearchCamCate_Setup.DataTextField = "ProductBrandName";
            ddlSearchCamCate_Setup.DataValueField = "ProductBrandCode";
            ddlSearchCamCate_Setup.DataBind();
            ddlSearchCamCate_Setup.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (New Order)

        #region Paging (New Order)

        protected void SetPageBar_Setup(double totalRow)
        {

            lblTotalPages_Setup.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_Setup.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_Setup.Text) + 1; i++)
            {
                ddlPage_Setup.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Setup(ddlPage_Setup, currentPageNumber_Setup.ToString());
            

            
            if ((currentPageNumber_Setup == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_Setup.Enabled = false;
                lnkbtnPre_Setup.Enabled = false;
                lnkbtnNext_Setup.Enabled = true;
                lnkbtnLast_Setup.Enabled = true;
            }
            else if ((currentPageNumber_Setup.ToString() == lblTotalPages_Setup.Text) && (currentPageNumber_Setup == 1))
            {
                lnkbtnFirst_Setup.Enabled = false;
                lnkbtnPre_Setup.Enabled = false;
                lnkbtnNext_Setup.Enabled = false;
                lnkbtnLast_Setup.Enabled = false;
            }
            else if ((currentPageNumber_Setup.ToString() == lblTotalPages_Setup.Text) && (currentPageNumber_Setup > 1))
            {
                lnkbtnFirst_Setup.Enabled = true;
                lnkbtnPre_Setup.Enabled = true;
                lnkbtnNext_Setup.Enabled = false;
                lnkbtnLast_Setup.Enabled = false;
            }
            else
            {
                lnkbtnFirst_Setup.Enabled = true;
                lnkbtnPre_Setup.Enabled = true;
                lnkbtnNext_Setup.Enabled = true;
                lnkbtnLast_Setup.Enabled = true;
            }
            
        }

        private void setDDl_Setup(DropDownList ddls, String val)
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

        protected void GetPageIndex_Setup(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_Setup = 1;
                    break;

                case "Previous":
                    currentPageNumber_Setup = Int32.Parse(ddlPage_Setup.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_Setup = Int32.Parse(ddlPage_Setup.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_Setup = Int32.Parse(lblTotalPages_Setup.Text);
                    break;
            }


            LoadOrder_Setup();
        }

        protected void ddlPage_SelectedIndexChanged_Setup(object sender, EventArgs e)
        {
            currentPageNumber_Setup = Int32.Parse(ddlPage_Setup.SelectedValue);

            LoadOrder_Setup();
        }

        #endregion Paging (New Order)
        #endregion New Order

        #region ReadyDeli
        #region Function (ReadyDeli)
        protected void LoadOrder_ReadyDeli()
        {
            List<OrderInfo> lOrderInfo_ReadyDeli = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_ReadyDeli();

            countSection_ReadyDeli.Text = totalRow.ToString();

            SetPageBar_ReadyDeli(Convert.ToDouble(totalRow));

            lOrderInfo_ReadyDeli = GetOrderMasterByCriteria_ReadyDeli();

            gvOrder_ReadyDeli.DataSource = lOrderInfo_ReadyDeli;
            gvOrder_ReadyDeli.DataBind();

        }

        public int? CountOrderMasterList_ReadyDeli()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_ReadyDeli.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_ReadyDeli.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_ReadyDeli.Text;

                data["CustomerCode"] = txtSearchCustomerCode_ReadyDeli.Text;

                data["CustomerFName"] = txtSearchFName_ReadyDeli.Text;

                data["CustomerLName"] = txtSearchLName_ReadyDeli.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_04; 

                data["CustomerContact"] = txtSearchContact_ReadyDeli.Text;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["CampaignCategoryCode"] = ddlSearchCamCate_ReadyDeli.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_ReadyDeli.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_ReadyDeli()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_ReadyDeli.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_ReadyDeli.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_ReadyDeli.Text;

                data["CustomerCode"] = txtSearchCustomerCode_ReadyDeli.Text;

                data["CustomerFName"] = txtSearchFName_ReadyDeli.Text;

                data["CustomerLName"] = txtSearchLName_ReadyDeli.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_04; 

                data["CustomerContact"] = txtSearchContact_ReadyDeli.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_ReadyDeli.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_ReadyDeli.SelectedValue;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["rowOFFSet"] = ((currentPageNumber_ReadyDeli - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_ReadyDeli()
        {
            txtSearchOrderCode_ReadyDeli.Text = "";
            txtSearchCustomerCode_ReadyDeli.Text = "";
            txtSearchOrderDateFrom_ReadyDeli.Text = "";
            txtSearchOrderDateUntil_ReadyDeli.Text = "";
            txtSearchCustomerCode_ReadyDeli.Text = "";
            txtSearchFName_ReadyDeli.Text = "";
            txtSearchLName_ReadyDeli.Text = "";
            txtSearchContact_ReadyDeli.Text = "";
            ddlSearchCamCate_ReadyDeli.SelectedValue = "-99";
        }

        public int? sumAmoutOrderDetail_ReadyDeli(string OrderCode)
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

        public int? sumTotalPriceOrderDetail_ReadyDeli(string OrderCode)
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

        protected void chkAll_Change_ReadyDeli(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_ReadyDeli.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_ReadyDeli.HeaderRow.FindControl("chkAll_ReadyDeli");
                CheckBox chk_NewOrder = (CheckBox)gvOrder_ReadyDeli.Rows[i].FindControl("chk_ReadyDeli");

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
        protected Boolean Changestatus_ReadyDeli()
        {


            for (int i = 0; i < gvOrder_ReadyDeli.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_ReadyDeli.Rows[i].FindControl("chk_ReadyDeli");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_ReadyDeli.Rows[i].FindControl("hidOrderCode_ReadyDeli");

                    Label Lbordercode = (Label)gvOrder_ReadyDeli.Rows[i].FindControl("lblOrderCode_ReadyDeli");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = StaticField.OrderState_11, orderstatus = StaticField.OrderStatus_05 }); 
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

        protected void btnAcceptOrder_ReadyDeli_Click(object sender, EventArgs e)
        {
            UpdateInventoryDetailByOrder_ReadyDeli();
            Changestatus_ReadyDeli();
            LoadOrder_ReadyDeli();
            LoadOrder_FinishDeli();
        }

        #endregion Function (Cooking)

        #region Events (Cooking)
        protected void btnSearch_Click_ReadyDeli(object sender, EventArgs e)
        {
            LoadOrder_ReadyDeli();
        }

        protected void btnClearSearch_Click_ReadyDeli(object sender, EventArgs e)
        {
            clearSearch_ReadyDeli();
        }


        protected void btnCancelRejectForRequest_ReadyDeli_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "myModal", "$('#modalRequestForReject_ReadyDeli').modal('hide');", true);
        }

        #endregion Events (Cooking)

        #region Binding (Cooking)


        protected void BindddlSearchOrderStatus_ReadyDeli()
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

            ddlSearchOrderState_ReadyDeli.DataSource = lLookupInfo;
            ddlSearchOrderState_ReadyDeli.DataTextField = "LookupValue";
            ddlSearchOrderState_ReadyDeli.DataValueField = "LookupCode";
            ddlSearchOrderState_ReadyDeli.DataBind();
            ddlSearchOrderState_ReadyDeli.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_ReadyDeli()
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

            ddlSearchCamCate_ReadyDeli.DataSource = camLookupInfo;
            ddlSearchCamCate_ReadyDeli.DataTextField = "ProductBrandName";
            ddlSearchCamCate_ReadyDeli.DataValueField = "ProductBrandCode";
            ddlSearchCamCate_ReadyDeli.DataBind();
            ddlSearchCamCate_ReadyDeli.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (Cooking)

        #region Paging (Cooking)

        protected void SetPageBar_ReadyDeli(double totalRow)
        {

            lblTotalPages_ReadyDeli.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_ReadyDeli.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_ReadyDeli.Text) + 1; i++)
            {
                ddlPage_ReadyDeli.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_ReadyDeli(ddlPage_ReadyDeli, currentPageNumber_ReadyDeli.ToString());
            

            
            if ((currentPageNumber_ReadyDeli == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_ReadyDeli.Enabled = false;
                lnkbtnPre_ReadyDeli.Enabled = false;
                lnkbtnNext_ReadyDeli.Enabled = true;
                lnkbtnLast_ReadyDeli.Enabled = true;
            }
            else if ((currentPageNumber_ReadyDeli.ToString() == lblTotalPages_ReadyDeli.Text) && (currentPageNumber_ReadyDeli == 1))
            {
                lnkbtnFirst_ReadyDeli.Enabled = false;
                lnkbtnPre_ReadyDeli.Enabled = false;
                lnkbtnNext_ReadyDeli.Enabled = false;
                lnkbtnLast_ReadyDeli.Enabled = false;
            }
            else if ((currentPageNumber_ReadyDeli.ToString() == lblTotalPages_ReadyDeli.Text) && (currentPageNumber_ReadyDeli > 1))
            {
                lnkbtnFirst_ReadyDeli.Enabled = true;
                lnkbtnPre_ReadyDeli.Enabled = true;
                lnkbtnNext_ReadyDeli.Enabled = false;
                lnkbtnLast_ReadyDeli.Enabled = false;
            }
            else
            {
                lnkbtnFirst_ReadyDeli.Enabled = true;
                lnkbtnPre_ReadyDeli.Enabled = true;
                lnkbtnNext_ReadyDeli.Enabled = true;
                lnkbtnLast_ReadyDeli.Enabled = true;
            }
            
        }

        private void setDDl_ReadyDeli(DropDownList ddls, String val)
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

        protected void GetPageIndex_ReadyDeli(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_ReadyDeli = 1;
                    break;

                case "Previous":
                    currentPageNumber_ReadyDeli = Int32.Parse(ddlPage_ReadyDeli.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_ReadyDeli = Int32.Parse(ddlPage_ReadyDeli.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_ReadyDeli = Int32.Parse(lblTotalPages_ReadyDeli.Text);
                    break;
            }


            LoadOrder_ReadyDeli();
        }

        protected void ddlPage_SelectedIndexChanged_ReadyDeli(object sender, EventArgs e)
        {
            currentPageNumber_ReadyDeli = Int32.Parse(ddlPage_ReadyDeli.SelectedValue);

            LoadOrder_ReadyDeli();
        }




        #endregion Paging (Cooking)
        #endregion Cooking

        #region FinishDeli
        #region Function (FinishDeli)
        protected void LoadOrder_FinishDeli()
        {
            List<OrderInfo> lOrderInfo_FinishDeli = new List<OrderInfo>();

            int? totalRow = CountOrderMasterList_FinishDeli();

            countSection_FinishDeli.Text = totalRow.ToString();

            SetPageBar_FinishDeli(Convert.ToDouble(totalRow));

            lOrderInfo_FinishDeli = GetOrderMasterByCriteria_FinishDeli();

            gvOrder_FinishDeli.DataSource = lOrderInfo_FinishDeli;
            gvOrder_FinishDeli.DataBind();

        }

        public int? CountOrderMasterList_FinishDeli()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderManagementListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_FinishDeli.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_FinishDeli.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_FinishDeli.Text;

                data["CustomerCode"] = txtSearchCustomerCode_FinishDeli.Text;

                data["CustomerFName"] = txtSearchFName_FinishDeli.Text;

                data["CustomerLName"] = txtSearchLName_FinishDeli.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_05; 

                data["CustomerContact"] = txtSearchContact_FinishDeli.Text;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["CampaignCategoryCode"] = ddlSearchCamCate_FinishDeli.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_FinishDeli.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<OrderInfo> GetOrderMasterByCriteria_FinishDeli()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_FinishDeli.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_FinishDeli.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_FinishDeli.Text;

                data["CustomerCode"] = txtSearchCustomerCode_FinishDeli.Text;

                data["CustomerFName"] = txtSearchFName_FinishDeli.Text;

                data["CustomerLName"] = txtSearchLName_FinishDeli.Text;

                data["OrderStatusCode"] = StaticField.OrderStatus_05; 

                data["CustomerContact"] = txtSearchContact_FinishDeli.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_FinishDeli.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_FinishDeli.SelectedValue;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
                data["rowOFFSet"] = ((currentPageNumber_FinishDeli - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch_FinishDeli()
        {
            txtSearchOrderCode_FinishDeli.Text = "";
            txtSearchCustomerCode_FinishDeli.Text = "";
            txtSearchOrderDateFrom_FinishDeli.Text = "";
            txtSearchOrderDateUntil_FinishDeli.Text = "";
            txtSearchCustomerCode_FinishDeli.Text = "";
            txtSearchFName_FinishDeli.Text = "";
            txtSearchLName_FinishDeli.Text = "";
            txtSearchContact_FinishDeli.Text = "";
            ddlSearchCamCate_FinishDeli.SelectedValue = "-99";
        }


        protected void chkAll_Change_FinishDeli(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder_FinishDeli.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder_FinishDeli.HeaderRow.FindControl("chkAll_FinishDeli");
                CheckBox chk_NewOrder = (CheckBox)gvOrder_FinishDeli.Rows[i].FindControl("chk_FinishDeli");

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
        protected Boolean Changestatus_FinishDeli()
        {


            for (int i = 0; i < gvOrder_FinishDeli.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_FinishDeli.Rows[i].FindControl("chk_FinishDeli");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_FinishDeli.Rows[i].FindControl("hidOrderCode_FinishDeli");

                    Label Lbordercode = (Label)gvOrder_FinishDeli.Rows[i].FindControl("lblOrderCode_FinishDeli");

                    if (CodelistApprove != "")
                    {
                        CodelistApprove += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        CodelistApprove += "'" + hidCode.Value + "'";
                    }
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = StaticField.OrderState_05, orderstatus = StaticField.OrderStatus_04 }); 
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

        protected void btnAcceptOrder_FinishDeli_Click(object sender, EventArgs e)
        {
            Changestatus_FinishDeli();
            LoadOrder_FinishDeli();

        }


        #endregion Function (Cooked)

        #region Events (Cooked)
        protected void btnSearch_Click_FinishDeli(object sender, EventArgs e)
        {
            LoadOrder_FinishDeli();
        }

        protected void btnClearSearch_Click_FinishDeli(object sender, EventArgs e)
        {
            clearSearch_FinishDeli();
        }

        #endregion Events (Cooked)

        #region Binding (Cooked)

        protected void BindddlSearchOrderStatus_FinishDeli()
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

            ddlSearchOrderState_FinishDeli.DataSource = lLookupInfo;
            ddlSearchOrderState_FinishDeli.DataTextField = "LookupValue";
            ddlSearchOrderState_FinishDeli.DataValueField = "LookupCode";
            ddlSearchOrderState_FinishDeli.DataBind();
            ddlSearchOrderState_FinishDeli.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchCamCate_FinishDeli()
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

            ddlSearchCamCate_FinishDeli.DataSource = camLookupInfo;
            ddlSearchCamCate_FinishDeli.DataTextField = "ProductBrandCode";
            ddlSearchCamCate_FinishDeli.DataValueField = "ProductBrandName";
            ddlSearchCamCate_FinishDeli.DataBind();
            ddlSearchCamCate_FinishDeli.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }


        #endregion Binding (Cooked)

        #region Paging (Cooked)

        protected void SetPageBar_FinishDeli(double totalRow)
        {

            lblTotalPages_FinishDeli.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_FinishDeli.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_FinishDeli.Text) + 1; i++)
            {
                ddlPage_FinishDeli.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_FinishDeli(ddlPage_FinishDeli, currentPageNumber_FinishDeli.ToString());
            

            
            if ((currentPageNumber_FinishDeli == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_FinishDeli.Enabled = false;
                lnkbtnPre_FinishDeli.Enabled = false;
                lnkbtnNext_FinishDeli.Enabled = true;
                lnkbtnLast_FinishDeli.Enabled = true;
            }
            else if ((currentPageNumber_FinishDeli.ToString() == lblTotalPages_FinishDeli.Text) && (currentPageNumber_FinishDeli == 1))
            {
                lnkbtnFirst_FinishDeli.Enabled = false;
                lnkbtnPre_FinishDeli.Enabled = false;
                lnkbtnNext_FinishDeli.Enabled = false;
                lnkbtnLast_FinishDeli.Enabled = false;
            }
            else if ((currentPageNumber_FinishDeli.ToString() == lblTotalPages_FinishDeli.Text) && (currentPageNumber_FinishDeli > 1))
            {
                lnkbtnFirst_FinishDeli.Enabled = true;
                lnkbtnPre_FinishDeli.Enabled = true;
                lnkbtnNext_FinishDeli.Enabled = false;
                lnkbtnLast_FinishDeli.Enabled = false;
            }
            else
            {
                lnkbtnFirst_FinishDeli.Enabled = true;
                lnkbtnPre_FinishDeli.Enabled = true;
                lnkbtnNext_FinishDeli.Enabled = true;
                lnkbtnLast_FinishDeli.Enabled = true;
            }
            
        }

        private void setDDl_FinishDeli(DropDownList ddls, String val)
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

        protected void GetPageIndex_FinishDeli(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_FinishDeli = 1;
                    break;

                case "Previous":
                    currentPageNumber_FinishDeli = Int32.Parse(ddlPage_FinishDeli.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_FinishDeli = Int32.Parse(ddlPage_FinishDeli.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_FinishDeli = Int32.Parse(lblTotalPages_FinishDeli.Text);
                    break;
            }


            LoadOrder_FinishDeli();
        }

        protected void ddlPage_SelectedIndexChanged_FinishDeli(object sender, EventArgs e)
        {
            currentPageNumber_FinishDeli = Int32.Parse(ddlPage_FinishDeli.SelectedValue);

            LoadOrder_FinishDeli();
        }




        #endregion Paging (Cooked)
        #endregion Cooked

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

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
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

                data["CustomerContact"] = txtSearchContact_OrderCancelled.Text;

                data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_OrderCancelled.SelectedValue;

                data["MerchantMapCode"] = Session["MerchantCode"].ToString();
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

                data["LookupType"] = "ORDERSTATUS";


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
        protected Boolean ValidateSearch()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[ก-๙a-zA-Z0-9/ ]*$");
            var regexDate = new Regex("^[0-9/ ]*$");


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
            if (regexDate.IsMatch(txtSearchOrderDateFrom.Text) && regexDate.IsMatch(txtSearchOrderDateUntil.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchOrderDate.Text = "";
            }
            else
            {
                flag = false;
                lblSearchOrderDate.Text = MessageConst._MSG_PLEASEINSERT + " วันที่สั่งซื้อเท่านั้น";
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
                lblSearchName.Text = "";
            }
            else
            {
                flag = false;
                lblSearchName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ - สกุลต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(txtSearchContact.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchContact.Text = "";
            }
            else
            {
                flag = false;
                lblSearchContact.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์ติดต่อต้องไม่มีอักขระพิเศษ";
            }
            return flag;

        }

        protected Boolean UpdateInventoryDetailByOrder_ReadyDeli()
        {
            for (int i = 0; i < gvOrder_ReadyDeli.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder_ReadyDeli.Rows[i].FindControl("chk_ReadyDeli");                

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvOrder_ReadyDeli.Rows[i].FindControl("hidOrderCode_ReadyDeli");
                    HiddenField hidInventoryCode = (HiddenField)gvOrder_ReadyDeli.Rows[i].FindControl("hidInventoryCode");

                    Label Lbordercode = (Label)gvOrder_ReadyDeli.Rows[i].FindControl("lblOrderCode_ReadyDeli");

                    OrderDetailInfo odetailInfo = new OrderDetailInfo();
                    List<OrderDetailInfo> lodetailInfo = new List<OrderDetailInfo>();

                    lodetailInfo = GetOrderdetailNoPaging(hidCode.Value);

                    if (lodetailInfo.Count > 0)
                    {
                        foreach (var od in lodetailInfo)
                        {
                            InventoryDetailInfoNew invdInfo = new InventoryDetailInfoNew();
                            List<InventoryDetailInfoNew> linvdInfo = new List<InventoryDetailInfoNew>();

                            InventoryDetailInfoNew indtInfo = new InventoryDetailInfoNew();

                            linvdInfo = GetInventoryDetailNoPaging(hidInventoryCode.Value, od.ProductCode);

                            if (linvdInfo.Count > 0)
                            {
                                indtInfo.QTY = linvdInfo[0].QTY - od.Amount;
                                indtInfo.Reserved = linvdInfo[0].Reserved - od.Amount;
                                indtInfo.Current = linvdInfo[0].Current;
                                indtInfo.Balance = linvdInfo[0].Balance - od.Amount;

                                string respstr = "";
                                int? sum = 0;

                                APIpath = APIUrl + "/api/support/UpdateInventoryDetailfromTakeOrderRetail";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["InventoryCode"] = hidInventoryCode.Value;
                                    data["ProductCode"] = od.ProductCode;
                                    data["QTY"] = indtInfo.QTY.ToString();
                                    data["Reserved"] = indtInfo.Reserved.ToString();
                                    data["Current"] = indtInfo.Current.ToString();
                                    data["Balance"] = indtInfo.Balance.ToString();
                                    data["UpdateBy"] = hidEmpCode.Value;

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstr = Encoding.UTF8.GetString(response);

                                    sum = JsonConvert.DeserializeObject<int?>(respstr);
                                }
                            }
                        }
                    }

                }
            }

            return true;
        }

        protected List<OrderDetailInfo> GetOrderdetailNoPaging(string ordercode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderDetailMapProductNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = ordercode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderDetailInfo> lOrderDetailInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lOrderDetailInfo;
        }

        protected List<InventoryDetailInfoNew> GetInventoryDetailNoPaging(string inventoryCode, string productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = inventoryCode;
                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> linvdetailInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            return linvdetailInfo;
        }
    }
}
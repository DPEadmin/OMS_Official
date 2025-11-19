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
using System.Data;
using Microsoft.Reporting.WebForms;
using SALEORDER.Common;
namespace DOMS_TSR.src.Report
{
    public partial class Retest : System.Web.UI.Page
    {
        ReportSaleOrderInfo resultExport = new ReportSaleOrderInfo();
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
                BindddlSearchOrderStatus_NoAnswerOrder();

                BindddlSearchChannel_NoAnswerOrder();
                BindddlSearchCamCate_NoAnswerOrder();
                



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
            
        }



        public void reLoadAnySection()
        {
            LoadOrder_NoAnswerOrder();

        }

        public void sectionLoad_NoAnswerOrder()
        {

            
            show_NoAnswerOrder(true);

            BindddlSearchOrderStatus_NoAnswerOrder();

            BindddlSearchChannel_NoAnswerOrder();
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
            List<ReaportPerformanceSOSAInfo> lOrderInfo_NoAnswerOrder = new List<ReaportPerformanceSOSAInfo>();



            

            lOrderInfo_NoAnswerOrder = GetOrderMasterByCriteria_NoAnswerOrder();
            if (lOrderInfo_NoAnswerOrder.Count > 0)
            {
                ExportToExcel exx = new ExportToExcel();
             
                DataTable dts = exx.ConvertToDataTable(lOrderInfo_NoAnswerOrder);
                ReportDataSource datasource = new ReportDataSource("DataSet1", dts);

                rpsSale.LocalReport.DataSources.Clear();
                rpsSale.LocalReport.DataSources.Add(datasource);
                rpsSale.LocalReport.ReportPath = @"..\OMS_Report\Report\RDLCReportSaleOrder.rdlc";
                rpsSale.LocalReport.Refresh();

                
            }


        }

        static DataTable ConvertListToDataTable(List<string[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }
        protected void ExportOrder_NoAnswerOrder()
        {
            var dataExcel = new NameValueCollection();
            List<OrderOLInfo> olist = new List<OrderOLInfo>();
            olist.Add(new OrderOLInfo
            {
                OrderCode = txtSearchOrderCode_NoAnswerOrder.Text
                ,
                CreateDate = txtSearchOrderDateFrom_NoAnswerOrder.Text
                ,
                CreateDateTo = txtSearchOrderDateUntil_NoAnswerOrder.Text
                ,
                CustomerCode = txtSearchCustomerCode_NoAnswerOrder.Text,
                CustomerFName = txtSearchFName_NoAnswerOrder.Text,

                CustomerLName = txtSearchLName_NoAnswerOrder.Text
                ,
                DeliveryDate = txtSearchDeliverDate_NoAnswerOrder.Text,

                OrderStatusCode = ddlSearchOrderstatus_NoAnswerOrder.SelectedValue,
                ChannelCode = ddlSearchChannel_NoAnswerOrder.SelectedValue == "-99" ? dataExcel["ChannelCode"] = "" : dataExcel["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue
                ,
                CampaignCategoryCode = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? dataExcel["CampaignCategoryCode"] = "" : dataExcel["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue
                
            });


            Session["dataExportExcel"] = olist;
            string URL = "ReportPerformanceSOSA_Excel.aspx";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URL + "','_blank')", true);

        }
        public int? CountOrderMasterList_NoAnswerOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CounReaportPerformanceSOSA";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_NoAnswerOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NoAnswerOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NoAnswerOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_NoAnswerOrder.Text;

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;


                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;


                data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                data["OrderStatusCode"] = ddlSearchOrderstatus_NoAnswerOrder.SelectedValue;
                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<ReaportPerformanceSOSAInfo> GetOrderMasterByCriteria_NoAnswerOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ReaportPerformanceSOSA";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = txtSearchOrderCode_NoAnswerOrder.Text;

                data["CreateDate"] = txtSearchOrderDateFrom_NoAnswerOrder.Text;

                data["CreateDateTo"] = txtSearchOrderDateUntil_NoAnswerOrder.Text;

                data["CustomerCode"] = txtSearchCustomerCode_NoAnswerOrder.Text;

                data["CustomerFName"] = txtSearchFName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                data["CustomerLName"] = txtSearchLName_NoAnswerOrder.Text;

                data["DeliveryDateFrom"] = txtSearchDeliverDate_NoAnswerOrder.Text;

                data["DeliveryDateTo"] = txtSearchDeliverDateTo_NoAnswerOrder.Text;
                data["OrderStatusCode"] = ddlSearchOrderstatus_NoAnswerOrder.SelectedValue;

                data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue == "-99" ? data["ChannelCode"] = "" : data["ChannelCode"] = ddlSearchChannel_NoAnswerOrder.SelectedValue;

                data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue == "-99" ? data["CampaignCategoryCode"] = "" : data["CampaignCategoryCode"] = ddlSearchCamCate_NoAnswerOrder.SelectedValue;

                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);


            }

            List<ReaportPerformanceSOSAInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<ReaportPerformanceSOSAInfo>>(respstr);


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

            txtSearchDeliverDate_NoAnswerOrder.Text = "";
            txtSearchDeliverDateTo_NoAnswerOrder.Text = "";
            ddlSearchOrderstatus_NoAnswerOrder.SelectedValue = "-99";
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
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = StaticField.OrderState_02, Confirmno = conNo.ToString() }); 
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
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstatus = StaticField.OrderStatus_06, orderstate = StaticField.OrderState_04, Confirmno = conNo.ToString() }); 
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
            currentPageNumber_NoAnswerOrder = 1;
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
        protected void btnAcceptOrder_NoAnswerOrder_Click(object sender, EventArgs e)
        {

            ExportOrder_NoAnswerOrder();
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

        protected void BindddlSearchOrderStatus_NoAnswerOrder()
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

            List<LookupInfo> cLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchOrderstatus_NoAnswerOrder.DataSource = cLookupInfo;
            ddlSearchOrderstatus_NoAnswerOrder.DataTextField = "LookupValue";
            ddlSearchOrderstatus_NoAnswerOrder.DataValueField = "LookupCode";
            ddlSearchOrderstatus_NoAnswerOrder.DataBind();
            ddlSearchOrderstatus_NoAnswerOrder.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("close.html");
        }




        #endregion Paging (No Answer Order)

        #endregion No Answer Order
        

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"../OrderDetail/OrderDetail.aspx?OrderCode=" + strCode + "\">" + strCode + "</a>";
        }

    }
}
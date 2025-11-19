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
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using SALEORDER.Common;
using OfficeOpenXml.ConditionalFormatting;

namespace DOMS_TSR.src.LeadManagement
{
    public partial class ClickToCallProgress : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl;
        protected static string APIUrlx = ConfigurationManager.AppSettings["apiurl"];
        string Merchant_Code = "";
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
                    //Switch when push to GIT
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

                txtSearchFName_NoAnswerOrder.Visible = false;
                txtSearchLName_NoAnswerOrder.Visible = false;
                txtSearchContact_NoAnswerOrder.Visible = false;
                //ddlSearchCamCate_NoAnswerOrder.Visible = false;
                //txtSearchOrderDateFrom_NoAnswerOrder.Visible = false;
                //txtSearchOrderDateUntil_NoAnswerOrder.Visible = false;

                txtSearchASTFName_NoAnswerOrder.Visible = false;
                txtSearchASTLName_NoAnswerOrder.Visible = false;
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
            
            leadInfo.CustomerCode = "";
            leadInfo.FIRSTNAME_TH = txtSearchFName_NoAnswerOrder.Text.Trim();
            leadInfo.LASTNAME_TH = txtSearchLName_NoAnswerOrder.Text.Trim();
            //leadInfo.PREVIOUS_ORDER_DATE_FROM = txtSearchOrderDateFrom_NoAnswerOrder.Text.Trim();
            //leadInfo.PREVIOUS_ORDER_DATE_TO = txtSearchOrderDateUntil_NoAnswerOrder.Text.Trim();
            //leadInfo.BRAND_NO = ddlSearchCamCate_NoAnswerOrder.SelectedValue;
            leadInfo.CustomerPhone = txtSearchContact_NoAnswerOrder.Text.Trim();
            leadInfo.Name = txtSearchName.Text.Trim();
            leadInfo.Telephone = txtSearchTelephone.Text.Trim();
            //leadInfo.Caryear = txtSearchCarYear.Text.Trim();
            //leadInfo.Carmodel = txtSearchCarModel.Text.Trim();
            //leadInfo.ProductName = txtSearchProductName.Text.Trim();
            //leadInfo.PromotionName = txtSearchPromotionName.Text.Trim();
            leadInfo.MERCHANT_CODE = Merchant_Code;
            leadInfo.CreateDateFrom = txtSearchCreateDateFrom.Text;
            leadInfo.CreateDateTo = txtSearchCreateDateTo.Text;
            leadInfo.AssignEmpName = txtSearchEmpName.Text.ToString();

            APIpath = APIUrl + "/api/support/CountListClickToCallNoPagingByCriteria";
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    
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
                    leadInfo.AssignEmpName,
                    leadInfo.MERCHANT_CODE,
                    leadInfo.CreateDateFrom,
                    leadInfo.CreateDateTo,
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
            
            leadInfo.CustomerCode = "";
            leadInfo.AssignFNAME = txtSearchASTFName_NoAnswerOrder.Text.Trim();
            leadInfo.AssignLNAME = txtSearchASTLName_NoAnswerOrder.Text.Trim();
            leadInfo.FIRSTNAME_TH = txtSearchFName_NoAnswerOrder.Text.Trim();
            leadInfo.LASTNAME_TH = txtSearchLName_NoAnswerOrder.Text.Trim();
            //leadInfo.PREVIOUS_ORDER_DATE_FROM = txtSearchOrderDateFrom_NoAnswerOrder.Text.Trim();
            //leadInfo.PREVIOUS_ORDER_DATE_TO = txtSearchOrderDateUntil_NoAnswerOrder.Text.Trim();
            //leadInfo.BRAND_NO = ddlSearchCamCate_NoAnswerOrder.SelectedValue;
            leadInfo.CustomerPhone = txtSearchContact_NoAnswerOrder.Text.Trim();
            leadInfo.Name = txtSearchName.Text.Trim();
            leadInfo.Telephone = txtSearchTelephone.Text.Trim();
            //leadInfo.Caryear = txtSearchCarYear.Text.Trim();
            //leadInfo.Carmodel = txtSearchCarModel.Text.Trim();
            //leadInfo.ProductName = txtSearchProductName.Text.Trim();
            //leadInfo.PromotionName = txtSearchPromotionName.Text.Trim();
            leadInfo.AssignEmpName = txtSearchEmpName.Text.Trim();
            leadInfo.MERCHANT_CODE = Merchant_Code;
            leadInfo.CreateDateFrom = txtSearchCreateDateFrom.Text;
            leadInfo.CreateDateTo = txtSearchCreateDateTo.Text;
            leadInfo.rowOFFSet = ((currentPageNumber_NoAnswerOrder - 1) * PAGE_SIZE);
            leadInfo.rowFetch = PAGE_SIZE;


            APIpath = APIUrl + "/api/support/ListClickToCallNoPagingByCriteria";
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    
                    leadInfo.CustomerCode,
                    leadInfo.FIRSTNAME_TH,
                    leadInfo.LASTNAME_TH,
                    leadInfo.PREVIOUS_ORDER_DATE_FROM,
                    leadInfo.PREVIOUS_ORDER_DATE_TO,
                    leadInfo.BRAND_NO,
                    leadInfo.AssignFNAME,
                    leadInfo.AssignLNAME,
                    leadInfo.CustomerPhone,
                    leadInfo.Name,
                    leadInfo.Telephone,
                    leadInfo.Caryear,
                    leadInfo.Carmodel,
                    leadInfo.ProductName,
                    leadInfo.PromotionName,
                    leadInfo.AssignEmpName,
                    leadInfo.rowOFFSet,
                    leadInfo.rowFetch,
                    leadInfo.MERCHANT_CODE,
                    leadInfo.CreateDateFrom,
                    leadInfo.CreateDateTo,  

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
              
            //txtSearchOrderDateFrom_NoAnswerOrder.Text = "";
            //txtSearchOrderDateUntil_NoAnswerOrder.Text = "";

            txtSearchFName_NoAnswerOrder.Text = "";
            txtSearchLName_NoAnswerOrder.Text = "";

            txtSearchASTFName_NoAnswerOrder.Text = "";
            txtSearchASTLName_NoAnswerOrder.Text = "";

            txtSearchContact_NoAnswerOrder.Text = "";

            //ddlSearchCamCate_NoAnswerOrder.SelectedValue = "-99";

            txtSearchName.Text = "";
            txtSearchTelephone.Text = "";
            //txtSearchCarYear.Text = "";
            //txtSearchCarModel.Text = "";
            //txtSearchProductName.Text = "";
            //txtSearchPromotionName.Text = "";
            txtSearchCreateDateFrom.Text = "";
            txtSearchCreateDateTo.Text = "";
            txtSearchEmpName.Text = "";
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
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstate = "13", orderstatus = "01", Confirmno = conNo.ToString() });
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
                    result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Lbordercode.Text.Trim(), orderstatus = "01", orderstate = "14" });
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
        protected Boolean ValidateSearch()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[ก-๙a-zA-Z0-9/ ]*$");
            var regexDate = new Regex("^[0-9/ ]*$");


            if (regexItem.IsMatch(txtSearchName.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchName.Text = "";
            }
            else
            {
                flag = false;
                lblSearchName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ-สกุลต้องไม่มีอักขระพิเศษ";
            }
            if (regexDate.IsMatch(txtSearchTelephone.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchTelephone.Text = "";
            }
            else
            {
                flag = false;
                lblSearchTelephone.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์ติดต่อต้องไม่มีอักขระพิเศษ";
            }
            //if (regexItem.IsMatch(txtSearchCarYear.Text))
            //{
            //    flag = (flag == false) ? false : true;
            //    lblSearclblSearchCarYear.Text = "";
            //}
            //else
            //{
            //    flag = false;
            //    lblSearclblSearchCarYear.Text = MessageConst._MSG_PLEASEINSERT + " ปีรถยนต์ต้องไม่มีอักขระพิเศษ";
            //}
            //if (regexItem.IsMatch(txtSearchCarModel.Text) )
            //{
            //    flag = (flag == false) ? false : true;
            //    lblSearchCarModel.Text = "";
            //}
            //else
            //{
            //    flag = false;
            //    lblSearchCarModel.Text = MessageConst._MSG_PLEASEINSERT + " รุ่นรถยนต์ต้องไม่มีอักขระพิเศษ";
            //}
            //if (regexItem.IsMatch(txtSearchProductName.Text))
            //{
            //    flag = (flag == false) ? false : true;
            //    lblSearchProductName.Text = "";
            //}
            //else
            //{
            //    flag = false;
            //    lblSearchProductName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อสินค้าต้องไม่มีอักขระพิเศษ";
            //}
            //if (regexItem.IsMatch(txtSearchPromotionName.Text))
            //{
            //    flag = (flag == false) ? false : true;
            //    lblSearchPromotionName.Text = "";
            //}
            //else
            //{
            //    flag = false;
            //    lblSearchPromotionName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อโปรโมชันต้องไม่มีอักขระพิเศษ";
            //}
            return flag;
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
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvOrder_NoAnswerOrder.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            if (e.CommandName == "ClickToCall")
            {
                ClickToCallInfo ctcInfo = new ClickToCallInfo();
                List<calldata> lcdata = new List<calldata>();
                calldata cdata = new calldata();

                cdata.id = "id";
                cdata.campaignid = "campaignid";
                cdata.type = "type";
                cdata.callerid = "callerid";

                lcdata.Add(cdata);

                foreach (var test in lcdata.ToList())
                {
                    ctcInfo.calldata.Add(new calldata() { id = test.id, campaignid = test.campaignid, type = test.type, callerid = test.callerid });
                }

                ctcInfo.calldata = lcdata;
                ctcInfo.extension = "extension";
                ctcInfo.callnumber = "callnumber";

                Task.Run(() =>
                {
                    

                    ResultOneApp result = new ResultOneApp();
                    List<ResultOneApp> lresult = new List<ResultOneApp>();
                    int i = 0;

                    APIpath = "https://doublep.dlinkddns.com:8081/oms/api/customer/leadcall";

                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        client.Encoding = Encoding.UTF8;

                        var jsonObj = JsonConvert.SerializeObject(new
                        {
                            ctcInfo.calldata,
                            ctcInfo.extension,
                            ctcInfo.callnumber
                        });

                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        var dataString = client.UploadString(APIpath, jsonObj);
                        var data = JsonConvert.DeserializeObject<ResultOneApp>(dataString);
                        result = data;

                        
                    }


                    Thread.Sleep(1000);
                });
            }
        }
        protected void gvOrder_NoAnswerOrder_RowCreate(object sender, GridViewRowEventArgs e)
        {
            //gvOrder_NoAnswerOrder.Columns[1].Visible = false; //hide customercode column
            //gvOrder_NoAnswerOrder.Columns[2].Visible = false; //hide customername column
            //gvOrder_NoAnswerOrder.Columns[4].Visible = false; //hide customerphone column
            //gvOrder_NoAnswerOrder.Columns[5].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[6].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[7].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[8].Visible = false; 
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




        protected string GetLink(object objCode, object CustomerCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            string StrCustomerCode = (CustomerCode != null) ? CustomerCode.ToString() : "";
            return "<a href=\"../FullfillOrderlist/AppointmentOrderManagementDetail.aspx?CustomerCode=" + StrCustomerCode + "&OrderCode=" + strCode + "\">" + strCode + "</a>";
        }

    }

    public class ResultOneApp
    {
        public String resultCode { get; set; }
        public String resultMessage { get; set; }
        public List<resultData> resultData { get; set; } = new List<resultData>();

    }
}
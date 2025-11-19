using Newtonsoft.Json;
using SALEORDER.Common;
using SALEORDER.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DOMS_TSR.src.Outbound
{
    public partial class ContactHistory : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string return_result;
        string CodelistApprove = "";
        protected static string APIUrl;
        protected static string APIUrlx = ConfigurationManager.AppSettings["apiurl"];
        string Codelist = "";
        protected static int currentPageNumber_NoAnswerOrder;

        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string OMSCORE_API = ConfigurationManager.AppSettings["OMSCORE_API"];
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
                    // APIUrl = empInfo.ConnectionAPI;
                    //APIUrl = "http://localhost:54545";
                    APIUrl = APIUrlx;
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
                BindLeadStatus("LEADSTATUS");
                sectionLoad_NoAnswerOrder();

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

        protected void BindLeadStatus(string param)
        {
            List<LookupInfo> lInfo = new List<LookupInfo>();
            lInfo = GetListLookUp(param);
            ddlSearchLeadStatus.DataSource = lInfo;
            ddlSearchLeadStatus.DataTextField = "LookupValue";
            ddlSearchLeadStatus.DataValueField = "LookupCode";
            ddlSearchLeadStatus.DataBind();

            ddlSearchLeadStatus.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        protected List<LookupInfo> GetListLookUp(string TypeStatus)
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
        public void reLoadAnySection()
        {
            LoadOrder_NoAnswerOrder();

        }

        public void sectionLoad_NoAnswerOrder()
        {

            showSection_NoAnswerOrder.CssClass = "btn btn-primary";
            show_NoAnswerOrder(true);

          
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
            leadInfo.AssignEmpCode = hidEmpCode.Value.ToString();
            leadInfo.CustomerCode = "";
            leadInfo.FIRSTNAME_TH = txtSearchFName_NoAnswerOrder.Text.Trim();
            leadInfo.LASTNAME_TH = txtSearchLName_NoAnswerOrder.Text.Trim();

            leadInfo.Name = txtSearchName.Text.Trim();
            leadInfo.Telephone = txtSearchTelephone.Text.Trim();
            leadInfo.CreateDateFrom = txtSearchCreateDateFrom.Text.Trim();
            leadInfo.CreateDateTo = txtSearchCreateDateTo.Text.Trim();
            leadInfo.SeachStatus = ddlSearchLeadStatus.SelectedValue;
            leadInfo.CustomerPhone = txtSearchContact_NoAnswerOrder.Text.Trim();
            leadInfo.Status = "'OP','PD','CL'";//Lookup สถานะเกิดขึ้นหลัง Assign Lead
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

            leadInfo.CreateDateFrom =txtSearchCreateDateFrom.Text.Trim();
            leadInfo.CreateDateTo = txtSearchCreateDateTo.Text.Trim(); 
           leadInfo.Status = "'OP','PD','CL'";
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

            txtSearchCreateDateFrom.Text = "";
            txtSearchCreateDateTo.Text = "";

            txtSearchFName_NoAnswerOrder.Text = "";
            txtSearchLName_NoAnswerOrder.Text = "";

            txtSearchContact_NoAnswerOrder.Text = "";


            ddlSearchLeadStatus.ClearSelection();
            txtSearchName.Text = "";
            txtSearchTelephone.Text = "";
      
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
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvOrder_NoAnswerOrder.Rows[index];
            HiddenField hidCAMPAIGN_ID = (HiddenField)row.FindControl("hidCAMPAIGN_ID");
            HiddenField hidLeadID = (HiddenField)row.FindControl("hidLeadID");
            HiddenField hidMEDIA_PHONE = (HiddenField)row.FindControl("hidMEDIA_PHONE");
            Label lblCustomercode = (Label)row.FindControl("lblCustomercode");
            HiddenField hidEmail = (HiddenField)row.FindControl("hidEmail");
            Label lblmsg = (Label)row.FindControl("lblmsg");

            if (e.CommandName == "ClickToCall")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/ListLeadManagementNoPagingByCriteria";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["LeadID"] = hidLeadID.Value;

                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }

                List<LeadManagementInfo> leadinfo = JsonConvert.DeserializeObject<List<LeadManagementInfo>>(respstr);
                string email = "";
                string telephone = "";
                string name = "";
                string Fname = "";
                string Lname = "";
                string MerchantCode = "";
                string MerchantName = "";
                
                if (leadinfo.Count > 0)
                {
                    CustomerInfo Icus = new CustomerInfo();
                    if (leadinfo[0].MOBILE_1 != null && leadinfo[0].MOBILE_1 != "")
                    {
                        telephone = leadinfo[0].MOBILE_1;
                    }
                    else if (leadinfo[0].MOBILE_2 != null && leadinfo[0].MOBILE_2 != "")
                    {
                        telephone = leadinfo[0].MOBILE_2;
                    }
                    else if (leadinfo[0].MOBILE_3 != null && leadinfo[0].MOBILE_3 != "")
                    {
                        telephone = leadinfo[0].MOBILE_3;
                    }
                    else if (leadinfo[0].MOBILE_4 != null && leadinfo[0].MOBILE_4 != "")
                    {
                        telephone = leadinfo[0].MOBILE_4;
                    }
                    else if (leadinfo[0].MOBILE_5 != null && leadinfo[0].MOBILE_5 != "")
                    {
                        telephone = leadinfo[0].MOBILE_2;
                    }
                    else if (leadinfo[0].MOBILE_6 != null && leadinfo[0].MOBILE_6 != "")
                    {
                        telephone = leadinfo[0].MOBILE_6;
                    }
                    if (leadinfo[0].FIRSTNAME_TH != null && leadinfo[0].FIRSTNAME_TH != "") 
                    {
                        Fname = leadinfo[0].FIRSTNAME_TH==null?"-": leadinfo[0].FIRSTNAME_TH; ;
                    }
                    if (leadinfo[0].LASTNAME_TH != null && leadinfo[0].LASTNAME_TH != "")
                    {
                        Lname = leadinfo[0].LASTNAME_TH == null ? "-" : leadinfo[0].LASTNAME_TH; ;
                    }
                    if (leadinfo[0].MERCHANT_CODE != null && leadinfo[0].MERCHANT_CODE != "")
                    {
                        MerchantCode = leadinfo[0].MERCHANT_CODE == null ? "-" : leadinfo[0].MERCHANT_CODE; ;
                    }
                    if (leadinfo[0].MerchantName != null && leadinfo[0].MerchantName != "")
                    {
                        MerchantName = leadinfo[0].MerchantName == null ? "-" : leadinfo[0].MerchantName; ;
                    }
                  
                    email = leadinfo[0].Email==null?"-": leadinfo[0].Email;


                    //insert data to customer
                
                    if (lblCustomercode.Text != null && lblCustomercode.Text != "")
                    {
                        Response.Redirect("../TakeOrderRetail/TakeOrder.aspx?CustomerCode=" + lblCustomercode.Text.Trim() + "&CallInfoID=&MerchantCode=" + MerchantCode +
"&Refusername=&CalllnNumber=" + telephone + "&Firstname=" + Fname + "&Lastname=" + Lname + "&MerchantSession=" + MerchantCode + "&MerchantSessionName=" + MerchantName + "&LeadID=" + hidLeadID.Value);

                    }
                    else 
                    {
                        int? count = loadcountCustomer();
                        string CustomerCode = "C" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(5, '0');
                        Icus.CustomerCode = CustomerCode;
                        Icus.Mobile = telephone;
                        Icus.CustomerFName = Fname;
                        Icus.CustomerLName = Lname;
                        Icus.MerchantCode = MerchantCode;
                        int i = insertCustomer(Icus);

                        if (i > 0)
                        {
                            int? countCusPhone = loadcountCustomerPhone(CustomerCode, telephone);
                            if (countCusPhone == 0)
                            {
                                insertCustomerPhone(CustomerCode, telephone);
                            }
                            LeadManagementInfo lInfo = new LeadManagementInfo();
                            lInfo.LeadID = int.Parse(hidLeadID.Value);
                            lInfo.Status = "PD"; //Convert จาก Lead สู่ Customer ส่งไปที่ Takeorder สำเร็จ
                            lInfo.CustomerCode = CustomerCode;
                            int? upStatusLaed = UpdateLeadAssignment(lInfo);

                        }
                        Response.Redirect("../TakeOrderRetail/TakeOrder.aspx?CustomerCode=" + CustomerCode + "&CallInfoID=&MerchantCode=" + MerchantCode +
       "&Refusername=&CalllnNumber=" + telephone + "&Firstname=" + Fname + "&Lastname=" + Lname + "&MerchantSession=" + MerchantCode + "&MerchantSessionName=" + MerchantName + "&LeadID=" + hidLeadID.Value);
                    }
                   

                }
                else
                {
                    Response.Redirect("../TakeOrderRetail/TakeOrder.aspx?CustomerCode=" + StaticField.CustomerCode_Param_ClickToTakeOrder + "&CallInfoID=&MerchantCode=" + StaticField.MerchantCode_Param_ClickToTakeOrder +
                     "&Refusername=" + StaticField.Refusername_Param_ClickToTakeOrder + "&CalllnNumber=" + telephone + "&Firstname=" + StaticField.Firstname_Param_ClickToTakeOrder + "&Lastname=" + StaticField.Lastname_Param_ClickToTakeOrder + "&MerchantSession=" + StaticField.MerchantSession_Param_ClickToTakeOrder + "&MerchantSessionName=" + StaticField.MerchantSessionName_Param_ClickToTakeOrder + "&LeadID=" + hidLeadID.Value);

                }

            }
        }
        protected void gvOrder_NoAnswerOrder_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //gvOrder_NoAnswerOrder.Columns[2].Visible = false; //hide customername column
            //gvOrder_NoAnswerOrder.Columns[3].Visible = false; //hide customerphone column
            //gvOrder_NoAnswerOrder.Columns[4].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[5].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[6].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[7].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[8].Visible = false; //hide customername column
            //gvOrder_NoAnswerOrder.Columns[9].Visible = false; //hide customerphone column
            //gvOrder_NoAnswerOrder.Columns[10].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[11].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[12].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[13].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[14].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[15].Visible = false; 
            //gvOrder_NoAnswerOrder.Columns[16].Visible = false; 
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




        protected string GetLink(object phone_number)
        {
            string strCode = (phone_number != null) ? phone_number.ToString() : "";

            return "<a href='#'>" + strCode + "</a>";
        }

        protected int? loadcountCustomer()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCustomerListByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected int? loadcountCustomerPhone(string cusCode, string phoneNumber)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountCustomerPhone";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = cusCode;
                data["PhoneNumber"] = phoneNumber;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected void insertCustomerPhone(string cusCode, string phoneNumber)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/InsertCustomerPhoneByCustomerInfo";

            string phoneType = phoneNumber.Length == 10 ? phoneType = StaticField.PhoneType_01 : phoneType = StaticField.PhoneType_02;

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = cusCode;
                data["PhoneNumber"] = phoneNumber;
                data["PhoneType"] = phoneType;
                data["CreateBy"] = hidEmpCode.Value;
                data["UpdateBy"] = hidEmpCode.Value;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
        }
        protected int insertCustomer(CustomerInfo lcust)
        {
            string respstr = "";
            int i = 0;
            APIpath = APIUrl + "/api/support/InsertCustomerofOMS"; //Insert 

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = lcust.CustomerCode;
                data["CustomerFName"] = lcust.CustomerFName;
                data["CustomerLName"] = lcust.CustomerLName;
                data["MerchantCode"] = lcust.MerchantCode;

                data["ContactTel"] = lcust.Mobile;
                data["FlagDelete"] = "N";
                data["CreateBy"] = hidEmpCode.Value;
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);

                i=int.Parse(respstr);
                return i;

            }
        }
        protected int? UpdateLeadAssignment(LeadManagementInfo lInfo)
        {
            int? sum = 0;
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateAssignEmpLeadManagement";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LeadID"] = lInfo.LeadID.ToString();
                data["AssignEmpCode"] = lInfo.AssignEmpCode;
                data["CallOutStatus"] = lInfo.CallOutStatus;
                data["CustomerCode"] = lInfo.CustomerCode;
                data["UpdateBy"] = hidEmpCode.Value;
                data["Status"] = lInfo.Status;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);

                sum = JsonConvert.DeserializeObject<int?>(respstr);
            }

            return sum;
        }
    }

}
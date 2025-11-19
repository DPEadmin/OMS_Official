using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SALEORDER.DTO;
using System.Configuration;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using DOMS_TSR.Classes.DTO;
using SALEORDER.Common;

namespace DOMS_TSR.src.CallInfo
{
    public partial class CallInfo_Olds : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    HidRefUsername.Value = empInfo.Username;
                    HidCompanyCode.Value = empInfo.CompanyCode;
                    hidMerName.Value = merchantInfo.MerchantName;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                
                bindDdlSearchGender();
                bindDdlSearchMaritalStatus();
                bindDdlSearchOccupation();
                bindDDlTitle_Ins();
            }
        }
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"../OrderDetail/OrderDetail.aspx?OrderCode=" + strCode + "\">" + strCode + "</a>";
        }
        protected void gvCallInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lblCustomerCode = (LinkButton)e.Row.FindControl("lblCustomerCode");
                HiddenField hidOrderCode = (HiddenField)e.Row.FindControl("hidOrderCode");

                if (hidOrderCode.Value == "" || hidOrderCode.Value == null)
                {
                    lblCustomerCode.Enabled = true;
                }
                else
                {
                    lblCustomerCode.Enabled = false;
                }

            }
        }
            #region events
            protected void gvCallInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCallInfo.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCustomerId = (HiddenField)row.FindControl("hidCustomerId");
            HiddenField hidCustomerCode = (HiddenField)row.FindControl("hidCustomerCode");
            HiddenField hidCustomerFName = (HiddenField)row.FindControl("hidCustomerFName");
            HiddenField hidCustomerLName = (HiddenField)row.FindControl("hidCustomerLName");
            HiddenField hidTitle = (HiddenField)row.FindControl("hidTitle");
            HiddenField hidGender = (HiddenField)row.FindControl("hidGender");
            HiddenField hidBirthDate = (HiddenField)row.FindControl("hidBirthDate");
            HiddenField hidIdentification = (HiddenField)row.FindControl("hidIdentification");
            HiddenField hidMaritalStatusCode = (HiddenField)row.FindControl("hidMaritalStatusCode");
            HiddenField hidOccupation = (HiddenField)row.FindControl("hidOccupation");
            HiddenField hidHomePhone = (HiddenField)row.FindControl("hidHomePhone");
            HiddenField hidMail = (HiddenField)row.FindControl("hidMail");
            HiddenField hidIncome = (HiddenField)row.FindControl("hidIncome");
            HiddenField hidContactTel = (HiddenField)row.FindControl("hidContactTel");
            HiddenField hidAge = (HiddenField)row.FindControl("hidAge");
            HiddenField hidMobile = (HiddenField)row.FindControl("hidMobile");
            HiddenField hidCallInNumber = (HiddenField)row.FindControl("hidCallInNumber");

            int? count = loadcountCustomer();
            string CustomerCode = count.ToString().PadLeft(5, '0');

            if (hidMobile.Value == "")
            {
                hidMobile.Value = hidCallInNumber.Value;
            }
            if (e.CommandName == "ShowCustomer")
            {
                if (hidCustomerCode.Value == "NEW")
                {
                    lblCusCode_Ins.Style.Add("display", "none");
                    divTxtCus_Ins.Style.Add("display", "none");
                    lblCol1.Style.Add("display", "none");
                    txtFirstName_Ins.Enabled = false;
                    txtLastName_Ins.Enabled = false;
                    txtContactTel_Ins.Enabled = false;

                    txtCustomerCode_Ins.Text = "C" + DateTime.Now.ToString("yyyyMMdd") + CustomerCode;
                    txtCustomerCode_Ins.Enabled = false;
                    hidFlagInsert.Value = "True";
                    ddlTitle_Ins.SelectedValue = "-99";
                    txtFirstName_Ins.Text = hidCustomerFName.Value;
                    txtLastName_Ins.Text = hidCustomerLName.Value;
                    ddlGender_Ins.SelectedValue = "-99";
                    txtBirthDate_Ins.Text = "";
                    txtIdentificationNo_Ins.Text = "";
                    ddlMaritalStatus_Ins.SelectedValue = "-99";
                    ddlOccupation_Ins.SelectedValue = "-99";
                    txtIncome_Ins.Text = "";
                    txtContactTel_Ins.Text = hidMobile.Value;
                    txtHomePhone_Ins.Text = "";
                    txtEmail_Ins.Text = "";
                    txtAge_Ins.Text = "";
                }
                else
                {
                    string birthdate_show = "";
                    if (hidBirthDate.Value == "")
                    {
                        birthdate_show = "";
                    }
                    else
                    {
                        birthdate_show = DateTime.Parse(hidBirthDate.Value.ToString()).ToString("dd/MM/yyyy");
                    }


                    lblCusCode_Ins.Style.Add("display", "block");
                    divTxtCus_Ins.Style.Add("display", "block");
                    lblCol1.Style.Add("display", "block");
                    
                    txtCustomerCode_Ins.Text = hidCustomerCode.Value;
                    hidFlagInsert.Value = "False";
                    txtCustomerCode_Ins.Enabled = false; //disable textbox txtCustomerCode_Ins
                    txtFirstName_Ins.Enabled = false; //disable textbox txtFirstName_Ins
                    txtLastName_Ins.Enabled = false; //disable textbox txtLastName_Ins
                    txtContactTel_Ins.Enabled = false; //disable textbox txtContactTel_Ins

                    txtFirstName_Ins.Text = hidCustomerFName.Value;
                    txtLastName_Ins.Text = hidCustomerLName.Value;
                    ddlTitle_Ins.SelectedValue = hidTitle.Value;
                    txtFirstName_Ins.Text = hidCustomerFName.Value;
                    txtLastName_Ins.Text = hidCustomerLName.Value;
                    ddlGender_Ins.SelectedValue = hidGender.Value;

                    
                    txtBirthDate_Ins.Text = birthdate_show;
                    txtIdentificationNo_Ins.Text = hidIdentification.Value;
                    ddlMaritalStatus_Ins.SelectedValue = hidMaritalStatusCode.Value;
                    ddlOccupation_Ins.SelectedValue = hidOccupation.Value;
                    txtIncome_Ins.Text = hidIncome.Value;
                    txtContactTel_Ins.Text = hidMobile.Value; //หมายเลขโทรศัพท์ของลูกค้า
                    txtHomePhone_Ins.Text = hidHomePhone.Value;
                    txtEmail_Ins.Text = hidMail.Value;
                    txtAge_Ins.Text = hidAge.Value;
                }

                

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else
            {
                if (ValidateSave())
                {
                    cardGridviewCallInfo.Style.Add("display", "block");
                    Boolean isDuplicate = ValidateDuplicate();

                    if (isDuplicate)
                    {
                        List<CallInformationInfo> lcallinformationInfo = new List<CallInformationInfo>();
                        lcallinformationInfo = GetCallinformationInfoMasterByCriteria(txtFName.Text, txtLName.Text);
                        string CustomerCode = lcallinformationInfo[0].CustomerCode;
                        string CustomerPhone = lcallinformationInfo[0].CallInNumber;

                        Session["CustomerPhone"] = CustomerPhone;
                        updateCustomerCode(CustomerCode);
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/InsertCallInformation";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = CustomerCode;
                            data["CallInNumber"] = txtTel.Text;
                            data["CustomerFName"] = txtFName.Text;
                            data["CustomerLName"] = txtLName.Text;
                            data["MerchantCode"] = hidMerCode.Value;
                            data["CONTACTSTATUS"] = "07"; //สายดี
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);

                        }

                        hidCallInfoID.Value = respstr;
                    }

                    else
                    {
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/InsertCallInformation";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            
                            data["CustomerCode"] = "NEW";
                            data["CallInNumber"] = txtTel.Text;
                            data["CustomerFName"] = txtFName.Text;
                            data["CustomerLName"] = txtLName.Text;
                            data["MerchantCode"] = hidMerCode.Value;
                            data["CONTACTSTATUS"] = "07"; //สายดี
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }

                        hidCallInfoID.Value = respstr;
                    }
                    loadCallinfo();
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            MerchantInfo merchantInfo = new MerchantInfo();
            merchantInfo = (MerchantInfo)Session["MerchantInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (ValidateInsertandUpdate())
                    {
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/InsertCustomerofOMS"; //Insert 

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = txtCustomerCode_Ins.Text;
                            data["CustomerFName"] = txtFirstName_Ins.Text;
                            data["CustomerLName"] = txtLastName_Ins.Text;
                            data["MerchantCode"] = hidMerCode.Value;
                            data["TitleCode"] = ddlTitle_Ins.SelectedValue;
                            data["Gender"] = ddlGender_Ins.SelectedValue;
                            data["BirthDate"] = txtBirthDate_Ins.Text;
                            data["Identification"] = txtIdentificationNo_Ins.Text;
                            data["MaritalStatusCode"] = ddlMaritalStatus_Ins.SelectedValue;
                            data["OccupationCode"] = ddlOccupation_Ins.SelectedValue;
                            data["Income"] = txtIncome_Ins.Text;
                            data["HomePhone"] = txtHomePhone_Ins.Text;
                            data["Mail"] = txtEmail_Ins.Text;
                            data["ContactTel"] = txtContactTel_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if (sum > 0)
                        {
                            int? countCusPhone = loadcountCustomerPhone(txtCustomerCode_Ins.Text, txtContactTel_Ins.Text);
                            if (countCusPhone == 0)
                            {
                                insertCustomerPhone(txtCustomerCode_Ins.Text, txtContactTel_Ins.Text);
                            }

                            updateCustomerCode(txtCustomerCode_Ins.Text);
                            loadCallinfo(txtFName.Text, txtLastName_Ins.Text);

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-customer').modal('hide');", true);

                            Response.Redirect("~/src/TakeOrderRetail/TakeOrder.aspx?CustomerCode=" + txtCustomerCode_Ins.Text + "&CallInfoID=" + hidCallInfoID.Value + "&MerchantCode=" + HidCompanyCode.Value + "&Refusername=" + HidRefUsername.Value + "&CalllnNumber=" + txtContactTel_Ins.Text + "&Firstname=" + txtFirstName_Ins.Text + "&Lastname=" + txtLastName_Ins.Text + "&MerchantSession=" + hidMerCode.Value+ "&MerchantSessionName=" + hidMerName.Value); //ส่งค่าไปยังหน้า TakeOrder.aspx
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
                    }
                }

                else //Update
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateCustomer";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        
                        data["CustomerCode"] = txtCustomerCode_Ins.Text;
                        data["CustomerFName"] = txtFirstName_Ins.Text;
                        data["CustomerLName"] = txtLastName_Ins.Text;
                        data["TitleCode"] = ddlTitle_Ins.SelectedValue;
                        data["Gender"] = ddlGender_Ins.SelectedValue;
                        data["BirthDate"] = txtBirthDate_Ins.Text;
                        data["Identification"] = txtIdentificationNo_Ins.Text;
                        data["MaritalStatusCode"] = ddlMaritalStatus_Ins.SelectedValue;
                        data["OccupationCode"] = ddlOccupation_Ins.SelectedValue;
                        data["Income"] = txtIncome_Ins.Text;
                        data["HomePhone"] = txtHomePhone_Ins.Text;
                        data["Mail"] = txtEmail_Ins.Text;
                        data["ContactTel"] = txtContactTel_Ins.Text;
                        data["FlagDelete"] = "N";
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                    if (sum > 0)
                    {
                        int? countCusPhone = loadcountCustomerPhone(txtCustomerCode_Ins.Text, txtContactTel_Ins.Text);
                        if (countCusPhone == 0)
                        {
                            insertCustomerPhone(txtCustomerCode_Ins.Text, txtContactTel_Ins.Text);
                        }

                        Response.Redirect("~/src/TakeOrderRetail/TakeOrder.aspx?CustomerCode=" + txtCustomerCode_Ins.Text + "&CallInfoID=" + hidCallInfoID.Value + "&MerchantCode=" + HidCompanyCode.Value + "&Refusername=" + HidRefUsername.Value + "&CalllnNumber=" + txtContactTel_Ins.Text + "&Firstname=" + txtFirstName_Ins.Text + "&Lastname=" + txtLastName_Ins.Text+ "&MerchantSession=" + hidMerCode.Value + "&MerchantSessionName=" + hidMerName.Value); //ส่งค่าไปยัง TakeOrder.aspx.cs
                        loadCallinfo();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-inventory').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }
                }
            }
        }

        #endregion events

        #region functions
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            cardGridviewCallInfo.Style.Add("display", "block");
            loadCallinfo();
        }
        
        protected void loadCallinfo()
        {
            List<CallInformationInfo> lcallinformationInfo = new List<CallInformationInfo>();
            lcallinformationInfo = GetCallinformationInfoMasterByCriteria();
            gvCallInfo.DataSource = lcallinformationInfo;
            gvCallInfo.DataBind();
        }

        protected void loadCallinfo(string CustomerFName, string CustomerLName)
        {
            List<CallInformationInfo> lcallinformationInfo = new List<CallInformationInfo>();
            lcallinformationInfo = GetCallinformationInfoMasterByCriteria(CustomerFName, CustomerLName);
            gvCallInfo.DataSource = lcallinformationInfo;
            gvCallInfo.DataBind();
        }

        protected List<CallInformationInfo> GetCallinformationInfoMasterByCriteria(string CustomerFName, string CustomerLName)
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCallinformationInfoByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerFName"] = CustomerFName;
                data["CustomerLName"] = CustomerLName;
                data["MerchantCode"] = hidMerCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CallInformationInfo> listCallInformationInfo = JsonConvert.DeserializeObject<List<CallInformationInfo>>(respstr);


            return listCallInformationInfo;
        }

        

        protected List<CallInformationInfo> GetCallinformationInfoMasterByCriteria()
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCallinformationInfoByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = "";
                data["CustomerFName"] = txtFName.Text;
                data["CustomerLName"] = txtLName.Text;
                data["CallInNumber"] = txtTel.Text;
                data["MerchantCode"] = hidMerCode.Value;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CallInformationInfo> listCallInformationInfo = JsonConvert.DeserializeObject<List<CallInformationInfo>>(respstr);


            return listCallInformationInfo;
        }

        protected void bindDdlSearchGender()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListGenderNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["GenderCode"] = "";
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<GenderInfo> lGenderInfo = JsonConvert.DeserializeObject<List<GenderInfo>>(respstr);

            ddlGender_Ins.DataSource = lGenderInfo;
            ddlGender_Ins.DataTextField = "GenderName";
            ddlGender_Ins.DataValueField = "GenderCode";
            ddlGender_Ins.DataBind();
            ddlGender_Ins.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));
        }
        protected void bindDdlSearchMaritalStatus()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = "MARITALSTATUS";
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lMaritalStatusInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlMaritalStatus_Ins.DataSource = lMaritalStatusInfo;
            ddlMaritalStatus_Ins.DataTextField = "LookupValue";
            ddlMaritalStatus_Ins.DataValueField = "LookupCode";
            ddlMaritalStatus_Ins.DataBind();
            ddlMaritalStatus_Ins.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));
        }
        protected void bindDdlSearchOccupation()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOccupationNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["OccupationCode"] = "";
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<OccupationInfo> lMaritalStatusInfo = JsonConvert.DeserializeObject<List<OccupationInfo>>(respstr);

            ddlOccupation_Ins.DataSource = lMaritalStatusInfo;
            ddlOccupation_Ins.DataTextField = "OccupationName";
            ddlOccupation_Ins.DataValueField = "OccupationCode";
            ddlOccupation_Ins.DataBind();
            ddlOccupation_Ins.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));
        }
        protected void bindDDlTitle_Ins()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = "TITLE";
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lMaritalStatusInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlTitle_Ins.DataSource = lMaritalStatusInfo;
            ddlTitle_Ins.DataTextField = "LookupValue";
            ddlTitle_Ins.DataValueField = "LookupCode";
            ddlTitle_Ins.DataBind();
            ddlTitle_Ins.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));
        }

        protected Boolean ValidateInsertandUpdate()
        {
            Boolean flag = true;

            if (txtCustomerCode_Ins.Text == "" || txtCustomerCode_Ins.Text == null)
            {
                flag = false;
                lblCustomerCode_Ins.Text = "Please Insert Customer Code";
            }
            else
            {
                if (hidFlagInsert.Value == "True")
                {
                    if (ValidateCustomerCodeInsert())
                    {
                        flag = (!flag) ? false : true;
                        lblCustomerCode_Ins.Text = "";
                    }
                    else
                    {
                        flag = false;
                        lblCustomerCode_Ins.Text = "Customer Code Duplicate";
                    }
                }
            }
            

            return flag;
        }

        protected Boolean ValidateSave()
        {
            Boolean flag = true;

            if (txtFName.Text == "" || txtFName.Text == null)
            {
                flag = false;
                lbltxtFName.Text = "กรุณาระบุชื่อ";
            }
            else
            {
                flag = (!flag) ? false : true;
                lbltxtFName.Text = "";
            }

            if (txtLName.Text == "" || txtLName.Text == null)
            {
                flag = false;
                lbltxtLName.Text = "กรุณาระบุนามสกุล";
            }
            else
            {
                flag = (!flag) ? false : true;
                lbltxtLName.Text = "";
            }
            if (txtTel.Text == "" || txtTel.Text == null)
            {
                lbltxtTel.Text = "กรุณาระบุเบอร์โทรศัพท์";
                flag = false;
            }
            else
            {
                flag = (!flag) ? false : true;
                lbltxtTel.Text = "";
            }
            

            return flag;
        }

        protected Boolean ValidateCustomerCodeInsert()
        {
            Boolean flagcuscode = true;

            string respstr = "";

            APIpath = APIUrl + "/api/support/CustomerCodeValidation";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = txtCustomerCode_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerInfo> lCustomerInfo = JsonConvert.DeserializeObject<List<CustomerInfo>>(respstr);

            if (lCustomerInfo.Count > 0)
            {
                flagcuscode = false;
            }
            else
            {
                flagcuscode = true;
            }
            return flagcuscode;
        }

        protected void updateCustomerCode(string CustomerCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/UpdateCustomerCode";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = CustomerCode;
                data["CustomerFName"] = txtFirstName_Ins.Text;
                data["CustomerLName"] = txtLastName_Ins.Text;
                data["MerchantCode"] = hidMerCode.Value;



                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerCodeCallInValidate";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerFName"] = txtFName.Text;
                data["CustomerLName"] = txtLName.Text;
                data["MerchantCode"] = hidMerCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CallInformationInfo> lCallInformationInfo = JsonConvert.DeserializeObject<List<CallInformationInfo>>(respstr);

            if (lCallInformationInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

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

        #endregion functions
    }
}
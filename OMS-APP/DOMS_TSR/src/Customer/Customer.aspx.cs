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
using System.Globalization;
using System.IO;

namespace DOMS_TSR.src.Customer
{
    public partial class Customer : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl;
        string APIpath = "";
        string Codelist = "";
        string Idlist = "";
        Boolean isdelete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null && merchantInfo != null)
                {
                    APIUrl = empInfo.ConnectionAPI;
                    
                    hidEmpCode.Value = empInfo.EmpCode;
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
                loadCustomer();
            }
        }

        #region event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadCustomer();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchCustomerFirstName.Text = "";
            txtSearchCustomerLastName.Text = "";
            ddlSearchGender.SelectedValue = "-99";
            txtSearchAgeFrom.Text = "";
            txtSearchAgeTo.Text = "";
            ddlSearchMaritalStatus.SelectedValue = "-99";
            ddlSearchOccupation.SelectedValue = "-99";
            txtSearchIncomeFrom.Text = "";
            txtSearchIncomeTo.Text = "";
            txtSearchContactTel.Text = "";
        }
        protected void btnAddCustomer_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            MerchantInfo merchantInfo = new MerchantInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            merchantInfo = (MerchantInfo)Session["MerchantInfo"];

            if (empInfo != null && merchantInfo != null)
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
                        APIpath = APIUrl + "/api/support/InsertCustomerofOMS";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = txtCustomerCode_Ins.Text;
                            data["CustomerFName"] = txtFirstName_Ins.Text;
                            data["CustomerLName"] = txtLastName_Ins.Text;
                            data["MerchantCode"] = merchantInfo.MerchantCode;
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
                            btnCancel_Click(null, null);
                            loadCustomer();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-customer').modal('hide');", true);
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
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCustomerCode_Ins.Text = "";
            ddlTitle_Ins.SelectedValue = "-99";
            txtFirstName_Ins.Text = "";
            txtLastName_Ins.Text = "";
            ddlGender_Ins.SelectedValue = "-99";
            txtBirthDate_Ins.Text = "";
            txtAge_Ins.Text = "";
            txtIdentificationNo_Ins.Text = "";
            ddlMaritalStatus_Ins.SelectedValue = "-99";
            ddlOccupation_Ins.SelectedValue = "-99";
            txtIncome_Ins.Text = "";
            txtContactTel_Ins.Text = "";
            txtHomePhone_Ins.Text = "";
            txtEmail_Ins.Text = "";

            lblCustomerCode_Ins.Text = "";
            lblTitle_Ins.Text = "";
            lblFirstName_Ins.Text = "";
            lblLastName_Ins.Text = "";
            lblGender_Ins.Text = "";
            lblBirthDate_Ins.Text = "";
            lblAge_Ins.Text = "";
            lblIdentificationNo_Ins.Text = "";
            lblMaritalStatus_Ins.Text = "";
            lblOccupation_Ins.Text = "";
            lblIncome_Ins.Text = "";
            lblContactTel_Ins.Text = "";
            lblHomePhone_Ins.Text = "";
            lblEmail_Ins.Text = "";
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteCustomer();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
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
            loadCustomer();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadCustomer();
        }
        protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvCustomer.Rows[index];


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
            HiddenField hidOccupationCode = (HiddenField)row.FindControl("hidOccupationCode");
            HiddenField hidHomePhone = (HiddenField)row.FindControl("hidHomePhone");
            HiddenField hidMail = (HiddenField)row.FindControl("hidMail");
            HiddenField hidIncome = (HiddenField)row.FindControl("hidIncome");
            HiddenField hidContactTel = (HiddenField)row.FindControl("hidContactTel");

            if (e.CommandName == "ShowCustomer")
            {
                hidCustomer_Ins.Value = hidCustomerId.Value;
                txtCustomerCode_Ins.Text = hidCustomerCode.Value;
                ddlTitle_Ins.SelectedValue = hidTitle.Value;
                txtFirstName_Ins.Text = hidCustomerFName.Value;
                txtLastName_Ins.Text = hidCustomerLName.Value;
                ddlGender_Ins.SelectedValue = hidGender.Value;
                txtBirthDate_Ins.Text = hidBirthDate.Value;
                txtIdentificationNo_Ins.Text = hidIdentification.Value;
                ddlMaritalStatus_Ins.SelectedValue = hidMaritalStatusCode.Value;
                ddlOccupation_Ins.SelectedValue = hidOccupationCode.Value;
                txtIncome_Ins.Text = hidIncome.Value;
                txtContactTel_Ins.Text = hidContactTel.Value;
                txtHomePhone_Ins.Text = hidHomePhone.Value;
                txtEmail_Ins.Text = hidMail.Value;

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
            }
        }

        #endregion

        #region function

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

            ddlSearchGender.DataSource = lGenderInfo;
            ddlSearchGender.DataTextField = "GenderName";
            ddlSearchGender.DataValueField = "GenderCode";
            ddlSearchGender.DataBind();
            ddlSearchGender.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));

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

            ddlSearchMaritalStatus.DataSource = lMaritalStatusInfo;
            ddlSearchMaritalStatus.DataTextField = "LookupValue";
            ddlSearchMaritalStatus.DataValueField = "LookupCode";
            ddlSearchMaritalStatus.DataBind();
            ddlSearchMaritalStatus.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));

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

            ddlSearchOccupation.DataSource = lMaritalStatusInfo;
            ddlSearchOccupation.DataTextField = "OccupationName";
            ddlSearchOccupation.DataValueField = "OccupationCode";
            ddlSearchOccupation.DataBind();
            ddlSearchOccupation.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));

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
        protected void loadCustomer()
        {
            List<CustomerInfo> lcustomerInfo = new List<CustomerInfo>();
            int? totalRow = loadcountCustomer();
            SetPageBar(Convert.ToDouble(totalRow));
            lcustomerInfo = GetCustomerMasterByCriteria();
            gvCustomer.DataSource = lcustomerInfo;
            gvCustomer.DataBind();
        }
        protected List<CustomerInfo> GetCustomerMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = txtSearchCustomerCode.Text;
                data["CustomerFName"] = txtSearchCustomerFirstName.Text;
                data["CustomerLName"] = txtSearchCustomerLastName.Text;
                data["MerchantCode"] = hidMerCode.Value;
                data["Gender"] = ddlSearchGender.SelectedValue;
                data["AgeFrom"] = txtSearchAgeFrom.Text;
                data["AgeTo"] = txtSearchAgeTo.Text;
                data["MaritalStatusCode"] = ddlSearchMaritalStatus.SelectedValue;
                data["OccupationCode"] = ddlSearchOccupation.SelectedValue;
                data["IncomeFrom"] = txtSearchIncomeFrom.Text;
                data["IncomeTo"] = txtSearchIncomeTo.Text;
                data["ContactTel"] = txtSearchContactTel.Text;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerInfo> listCustomerInfo = JsonConvert.DeserializeObject<List<CustomerInfo>>(respstr);


            return listCustomerInfo;
        }
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
        protected int? loadcountCustomer()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCustomerListByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = txtSearchCustomerCode.Text;
                data["CustomerFName"] = txtSearchCustomerFirstName.Text;
                data["CustomerLName"] = txtSearchCustomerLastName.Text;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
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
            if (ddlTitle_Ins.SelectedValue == "" || ddlTitle_Ins.SelectedValue == null || ddlTitle_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblTitle_Ins.Text = "Please Select Customer Title";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblTitle_Ins.Text = "";
            }
            if (txtFirstName_Ins.Text == "" || txtFirstName_Ins.Text == null)
            {
                flag = false;
                lblFirstName_Ins.Text = "Please Insert First Name";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblFirstName_Ins.Text = "";
            }
            if (txtLastName_Ins.Text == "" || txtLastName_Ins.Text == null)
            {
                flag = false;
                lblLastName_Ins.Text = "Please Insert Last Name";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblLastName_Ins.Text = "";
            }
            if (ddlGender_Ins.SelectedValue == "" || ddlGender_Ins.SelectedValue == null)
            {
                flag = false;
                lblGender_Ins.Text = "Please Select Gender";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblGender_Ins.Text = "";
            }
            if (txtBirthDate_Ins.Text == "" || txtBirthDate_Ins.Text == null)
            {
                flag = false;
                lblBirthDate_Ins.Text = "Please Insert Date of Birth";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblBirthDate_Ins.Text = "";
            }
            if (txtAge_Ins.Text == "" || txtAge_Ins.Text == null)
            {
                flag = false;
                lblAge_Ins.Text = "Please Insert Date of Birth";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblAge_Ins.Text = "";
            }
            if (txtIdentificationNo_Ins.Text == "" || txtIdentificationNo_Ins.Text == null)
            {
                flag = false;
                lblIdentificationNo_Ins.Text = "Please Insert Identification No.";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblIdentificationNo_Ins.Text = "";
            }
            if (ddlMaritalStatus_Ins.SelectedValue == "" || ddlMaritalStatus_Ins.SelectedValue == null || ddlMaritalStatus_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblMaritalStatus_Ins.Text = "Please Select Marital Status";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblMaritalStatus_Ins.Text = "";
            }
            if (ddlOccupation_Ins.SelectedValue == "" || ddlOccupation_Ins.SelectedValue == null || ddlOccupation_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblOccupation_Ins.Text = "Please Select Occupation";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblOccupation_Ins.Text = "";
            }
            if (txtIncome_Ins.Text == "" || txtIncome_Ins.Text == null)
            {
                flag = false;
                lblIncome_Ins.Text = "Please Insert Income";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblIncome_Ins.Text = "";
            }
            if (txtContactTel_Ins.Text == "" || txtContactTel_Ins.Text == null)
            {
                flag = false;
                lblContactTel_Ins.Text = "Please Insert Contact Tel";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblContactTel_Ins.Text = "";
            }
            if (txtHomePhone_Ins.Text == "" || txtHomePhone_Ins.Text == null)
            {
                flag = false;
                lblHomePhone_Ins.Text = "Please Insert Home Phone";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblHomePhone_Ins.Text = "";
            }
            if (txtEmail_Ins.Text == "" || txtEmail_Ins.Text == null)
            {
                flag = false;
                lblEmail_Ins.Text = "Please Insert Email";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblEmail_Ins.Text = "";
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

        protected Boolean DeleteCustomer()
        {

            for (int i = 0; i < gvCustomer.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvCustomer.Rows[i].FindControl("chkCustomer");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvCustomer.Rows[i].FindControl("hidCustomerId");
                    HiddenField hidCode = (HiddenField)gvCustomer.Rows[i].FindControl("hidCustomerCode");

                    if (Idlist != "")
                    {
                        Idlist += "," + hidId.Value + "";
                    }
                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Idlist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePromotion";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Idlist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePromtoionDetailInfoByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            else
            {
                hidIdList.Value = "";

                return false;
            }

            hidIdList.Value = "";
            return true;

        }
    }
        #endregion
}
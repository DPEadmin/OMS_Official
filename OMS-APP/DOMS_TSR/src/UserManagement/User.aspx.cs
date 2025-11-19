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

namespace DOMS_TSR.src.UserManagement
{
    public partial class User : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string APIpath = "";
        string Codelist = "";
        string EmpCodelist = "";
        Boolean isdelete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                BindDDLBU(ddlBUIns);
                BindDDLBU(ddlSearchBU);
                loadEmp();


            }
        }
        #region event
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {

            txtEmpCodeIns.Enabled = true;
            hidFlagInsert.Value = "True";

            txtEmpCodeIns.Text = "";
            
            txtEmpFNameTHIns.Text = "";
            txtEmailIns.Text = "";
            txtEmpLNameTHIns.Text = "";
            ddlBUIns.ClearSelection();
            txtExtensionID_ins.Text = "";
            lblExtensionID_ins.Text = "";

            txtMobileIns.Text = "";
            lblEmpFNameTHIns.Text = "";
            lblEmpLNameTHIns.Text = "";
            lblMobileIns.Text = "";
            lblEmailIns.Text = "";
            lblEmpCodeIns.Text = "";
            lblBUIns.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emp').modal();", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            EmpInfo eInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (validateInsertUpdate())
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {


                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertEmployee";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();


                            data["EmpCode"] = txtEmpCodeIns.Text;
                            
                            data["EmpFName_TH"] = txtEmpFNameTHIns.Text;
                            
                            data["EmpLName_TH"] = txtEmpLNameTHIns.Text;
                            data["Mobile"] = txtMobileIns.Text;
                            data["Mail"] = txtEmailIns.Text;
                            data["BUCode"] = ddlBUIns.SelectedValue;
                            
                            data["ExtensionID"] = txtExtensionID_ins.Text;
                            data["FlagDelete"] = "N";
                            data["ActiveFlag"] = StaticField.ActiveFlag_Y;

                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            loadEmp();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-emp').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {


                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateEmployee";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["EmpId"] = hidEmpIdIns.Value;
                            data["EmpCode"] = txtEmpCodeIns.Text;
                            
                            data["EmpFName_TH"] = txtEmpFNameTHIns.Text;
                            
                            data["EmpLName_TH"] = txtEmpLNameTHIns.Text;
                            data["Mobile"] = txtMobileIns.Text;
                            data["Mail"] = txtEmailIns.Text;
                            data["BUCode"] = ddlBUIns.SelectedValue;
                            data["Updateby"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";
                            data["ActiveFlag"] = StaticField.ActiveFlag_Y;
                            data["ExtensionID"] = txtExtensionID_ins.Text;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            loadEmp();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-emp').modal('hide');", true);


                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteEmp();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
            txtEmpFNameTHIns.Text = "";
            
            txtEmpLNameTHIns.Text = "";
            txtMobileIns.Text = "";
            txtEmpCodeIns.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emp').modal('hide');", true);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadEmp();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchEmpCode.Text = "";
            txtSearchEmpFNameTH.Text = "";
            txtSearchEmpLNameTH.Text = "";
            ddlSearchBU.ClearSelection();
        }
        protected void chkEmpAll_Changed(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEmp.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvEmp.HeaderRow.FindControl("chkEmpAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvEmp.Rows[i].FindControl("hidEmpId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkEmp = (CheckBox)gvEmp.Rows[i].FindControl("chkEmp");

                    chkEmp.Checked = true;
                }
                else
                {
                    CheckBox chkEmp = (CheckBox)gvEmp.Rows[i].FindControl("chkEmp");

                    chkEmp.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected void gvEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvEmp.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");
            Label lblEmpCode = (Label)row.FindControl("lblEmpCode");
            Label lblMobile = (Label)row.FindControl("lblMobile");
            Label lblMail = (Label)row.FindControl("lblMail");

            HiddenField hidEmpId = (HiddenField)row.FindControl("hidEmpId");
            HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
            HiddenField hidTitleTH = (HiddenField)row.FindControl("hidTitleTH");
            HiddenField hidTitleEN = (HiddenField)row.FindControl("hidTitleEN");
            HiddenField hidEmpFNameTH = (HiddenField)row.FindControl("hidEmpFNameTH");
            HiddenField hidEmpLNameTH = (HiddenField)row.FindControl("hidEmpLNameTH");
            HiddenField hidEmpFNameEN = (HiddenField)row.FindControl("hidEmpFNameEN");
            HiddenField hidEmpLNameEN = (HiddenField)row.FindControl("hidEmpLNameEN");
            HiddenField hidBUCode = (HiddenField)row.FindControl("hidBUCode");
            HiddenField hidExtensionID = (HiddenField)row.FindControl("hidExtensionID");

            if (e.CommandName == "ShowEmp")
            {
                hidEmpIdIns.Value = hidEmpId.Value;
                
                txtEmpFNameTHIns.Text = hidEmpFNameTH.Value;
                txtEmpLNameTHIns.Text = hidEmpLNameTH.Value;
                

                BindDDLBU(ddlBUIns);
                if (hidBUCode.Value != "")
                {
                    ddlBUIns.SelectedValue = hidBUCode.Value;
                }
                txtMobileIns.Text = lblMobile.Text;
                txtEmailIns.Text = lblMail.Text;
                txtEmpCodeIns.Text = hidEmpCode.Value;
                txtEmpCodeIns.Enabled = false;
                txtExtensionID_ins.Text = hidExtensionID.Value;


                lblEmpFNameTHIns.Text = "";
                lblEmpLNameTHIns.Text = "";
                lblMobileIns.Text = "";
                lblEmailIns.Text = "";
                lblEmpCodeIns.Text = "";
                lblBUIns.Text = "";

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-emp').modal();", true);
            }
        }
        #endregion

        #region function
        protected Boolean validateInsertUpdate()
        {
            Boolean flag = true;



            if (txtEmpCodeIns.Text == "")
            {
                flag = false;
                lblEmpCodeIns.Text = MessageConst._MSG_PLEASEINSERT + " รหัสพนักงาน";
            }
            else
            {

                if (hidFlagInsert.Value == "True") //Insert
                {
                    List<EmpInfo> lem = new List<EmpInfo>();

                    lem = GetEmpValidate();

                    if (lem.Count > 0)
                    {
                        flag = false;
                        lblEmpCodeIns.Text = "ไม่สามารถระบุรหัสพนักงานซ้ำ ";
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblEmpCodeIns.Text = "";
                    }
                }
                else
                {

                    flag = (flag == false) ? false : true;
                    lblEmpCodeIns.Text = "";
                }
            }



            if (txtEmpFNameTHIns.Text == "")
            {
                flag = false;
                lblEmpFNameTHIns.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEmpFNameTHIns.Text = "";
            }

            if (txtEmpLNameTHIns.Text == "")
            {
                flag = false;
                lblEmpLNameTHIns.Text = MessageConst._MSG_PLEASEINSERT + " นามสกุล";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEmpLNameTHIns.Text = "";
            }

            if (ddlBUIns.SelectedValue == "-99")
            {
                flag = false;
                lblBUIns.Text = MessageConst._MSG_PLEASEINSERT + " BU";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblBUIns.Text = "";
            }

            return flag;
        }

        protected List<EmpInfo> GetEmpValidate()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCodeValidate"] = txtEmpCodeIns.Text;


                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            return lEmpInfo;
        }

        protected void loadEmp()
        {
            List<EmpInfo> lEmpInfo = new List<EmpInfo>();
            int? totalRow = CountEmpList();
            SetPageBar(Convert.ToDouble(totalRow));
            lEmpInfo = GetEmpMasterByCriteria();
            if (lEmpInfo.Count > 0)
            {
                foreach (var emp in lEmpInfo)
                {
                    if (emp.ActiveFlag == StaticField.ActiveFlag_Y)
                    {
                        emp.ActiveFlagName = StaticField.ActiveFlag_Y_NameValue_Active;
                    }
                    else
                    {
                        emp.ActiveFlagName = StaticField.ActiveFlag_N_NameValue_Inactive;
                    }
                }
            }
            gvEmp.DataSource = lEmpInfo;
            gvEmp.DataBind();
        }

        protected Boolean DeleteEmp()
        {

            for (int i = 0; i < gvEmp.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvEmp.Rows[i].FindControl("chkEmp");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvEmp.Rows[i].FindControl("hidEmpId");
                    HiddenField hidEmpCode = (HiddenField)gvEmp.Rows[i].FindControl("hidEmpCode");
                    
                    

                    if (EmpCodelist != "")
                    {
                        EmpCodelist += ",'" + hidEmpCode.Value + "'";
                    }
                    else
                    {
                        EmpCodelist += "'" + hidEmpCode.Value + "'";
                    }
                }
            }

            if (EmpCodelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteEmployeeList";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["EmpCode"] = EmpCodelist;


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

        protected int? CountEmpList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountEmployeeListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCode.Text;
                data["EmpFname_TH"] = txtSearchEmpFNameTH.Text;
                data["BUCode"] = ddlSearchBU.SelectedValue;
                data["ActiveFlag"] = StaticField.ActiveFlag_Y;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"UserDetail.aspx?EmpCode=" + strCode + "\">" + strCode + "</a>";
        }
        protected List<EmpInfo> GetEmpMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCode.Text;
                data["EmpFname_TH"] = txtSearchEmpFNameTH.Text;
                data["EmpLname_TH"] = txtSearchEmpLNameTH.Text;
                data["BUCode"] = ddlSearchBU.SelectedValue;
                data["ActiveFlag"] = StaticField.ActiveFlag_Y;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);
            return lEmpInfo;
        }
        #endregion

        #region binding
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
            loadEmp();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadEmp();
        }

        protected void BindDDLBU(DropDownList ddlBU)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_BU;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlBU.DataSource = lLookupInfo;

            ddlBU.DataTextField = "LookupValue";

            ddlBU.DataValueField = "LookupCode";

            ddlBU.DataBind();

            ddlBU.Items.Insert(0, new ListItem("---- Please select ----", "-99"));



        }

        #endregion


    }
}
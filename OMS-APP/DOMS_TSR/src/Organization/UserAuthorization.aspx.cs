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

namespace DOMS_TSR.src.Organization
{
    public partial class UserAuthorization : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl;
        string APIpath = "";
        string Codelist = "";
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
                    APIUrl = empInfo.ConnectionAPI;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                loadEmployee();
            }
        }
        #region event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadEmployee();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchEmpCode.Text = "";
            txtSearchEmpFname_TH.Text = "";
            txtSearchEmpLname_TH.Text = "";
            ddlSearchEmpActiveflag.SelectedValue = "";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if(hidFlagInsert.Value == "True")
            {
                txtEmpCodeIns.Enabled = true;
                txtEmpCodeIns.Text = "";
            }
            else
            {
                txtEmpCodeIns.Enabled = false;
            }
            txtEmpLname_THIns.Text = "";
            ddlEmpActiveflagIns.SelectedValue = "";

            lblEmpCodeIns.Text = "";
            lblEmpFname_THIns.Text = "";
            lblEmpLname_THIns.Text = "";
            lblEmpActiveflagIns.Text = "";
        }
        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";

            if (hidFlagInsert.Value == "True")
            {
                txtEmpCodeIns.Enabled = true;
            }

            txtEmpCodeIns.Text = "";
            txtEmpFname_THIns.Text = "";
            txtEmpLname_THIns.Text = "";
            ddlEmpActiveflagIns.SelectedValue = "";

            lblEmpCodeIns.Text = "";
            lblEmpFname_THIns.Text = "";
            lblEmpLname_THIns.Text = "";
            lblEmpActiveflagIns.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addemp').modal();", true);
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert Employee
                {
                    if (ValidateInsertandUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertEmployee";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["EmpCode"] = txtEmpCodeIns.Text;
                            data["EmpFname_TH"] = txtEmpFname_THIns.Text;
                            data["EmpLname_TH"] = txtEmpLname_THIns.Text;
                            data["ActiveFlag"] = ddlEmpActiveflagIns.SelectedValue;
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        String RefCode = JsonConvert.DeserializeObject<String>(respstr);
                        if (RefCode != "")
                        {
                            //PostCreateEmptoOneAppApi(RefCode); ***Open This code when need to sync api 3rd party emp system (Can applied another 3rd party API and use RefCode to sync) 
                            btnCancel_Click(null, null);
                            loadEmployee();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-addemp').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addemp').modal();", true);
                    }
                }
                else // Update Employee
                {
                    if (ValidateInsertandUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateEmployee";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["EmpId"] = hidEmpIdIns.Value;
                            data["EmpFname_TH"] = txtEmpFname_THIns.Text;
                            data["EmpLName_TH"] = txtEmpLname_THIns.Text;
                            data["ActiveFlag"] = ddlEmpActiveflagIns.SelectedValue;
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadEmployee();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-addemp').modal('hide');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addemp').modal();", true);
                    }
                }
            }
        }
        protected void gvEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvEmp.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidEmpId = (HiddenField)row.FindControl("hidEmpId");
            HiddenField hidEmpCode = (HiddenField)row.FindControl("hidEmpCode");
            HiddenField hidEmpFname_TH = (HiddenField)row.FindControl("hidEmpFname_TH");
            HiddenField hidEmpLname_TH = (HiddenField)row.FindControl("hidEmpLname_TH");
            HiddenField hidEmpName_TH = (HiddenField)row.FindControl("hidEmpName_TH");
            HiddenField hidActiveFlag = (HiddenField)row.FindControl("hidActiveFlag");

            if (e.CommandName == "ShowEmp")
            {
                hidEmpIdIns.Value = hidEmpId.Value;
                txtEmpCodeIns.Enabled = false;
                txtEmpCodeIns.Text = hidEmpCode.Value;
                txtEmpFname_THIns.Text = hidEmpFname_TH.Value;
                txtEmpLname_THIns.Text = hidEmpLname_TH.Value;
                ddlEmpActiveflagIns.SelectedValue = hidActiveFlag.Value;

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addemp').modal();", true);
            }
        }
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"UserDetail.aspx?EmpCodeTemp=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }
        #endregion

        #region function
        protected void loadEmployee()
        {
            List<EmpInfo> lempInfo = new List<EmpInfo>();
            int? totalRow = CountEmpMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lempInfo = GetEmpMasterByCriteria();
            if(lempInfo.Count > 0)
            {
                foreach(var empinfoV in lempInfo)
                {
                    if(empinfoV.ActiveFlag == "Y")
                    {
                        empinfoV.ActiveFlagName = "Active";
                    }
                    else
                    {
                        empinfoV.ActiveFlagName = "Inactive";
                    }
                }
            }            
            gvEmp.DataSource = lempInfo;
            gvEmp.DataBind();
        }
        public int? CountEmpMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountEmployeeListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCode.Text;
                data["EmpFname_TH"] = txtSearchEmpFname_TH.Text;
                data["EmpLname_TH"] = txtSearchEmpLname_TH.Text;
                data["ActiveFlag"] = ddlSearchEmpActiveflag.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
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
            loadEmployee();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadEmployee();
        }
        protected List<EmpInfo> GetEmpMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCode.Text;
                data["EmpFname_TH"] = txtSearchEmpFname_TH.Text;
                data["EmpLname_TH"] = txtSearchEmpLname_TH.Text;
                data["ActiveFlag"] = ddlSearchEmpActiveflag.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);
            return lEmpInfo;
        }
        protected Boolean ValidateInsertandUpdate()
        {
            Boolean flag = true;

            if(txtEmpCodeIns.Text == "" || txtEmpCodeIns.Text == null)
            {
                flag = false;
                lblEmpCodeIns.Text = "Please Insert Employee Code";
            }
            else
            {
                if (hidFlagInsert.Value == "True")
                {
                    if (ValidateCampaignCodeInsert())
                    {
                        flag = (!flag) ? false : true;
                        lblEmpCodeIns.Text = "";
                    }
                    else
                    {
                        flag = false;
                        lblEmpCodeIns.Text = "Employee Code Duplicate";
                    }
                }
            }
            if (txtEmpFname_THIns.Text == "" || txtEmpFname_THIns.Text == null)
            {
                flag = false;
                lblEmpFname_THIns.Text = "Please Insert First Name";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblEmpFname_THIns.Text = "";
            }
            if (txtEmpLname_THIns.Text == "" || txtEmpLname_THIns.Text == null)
            {
                flag = false;
                lblEmpLname_THIns.Text = "Please Insert Last Name";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblEmpLname_THIns.Text = "";
            }
            if (txtEmpLname_THIns.Text == "" || txtEmpLname_THIns.Text == null)
            {
                flag = false;
                lblEmpLname_THIns.Text = "Please Insert Last Name";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblEmpLname_THIns.Text = "";
            }
            if (ddlEmpActiveflagIns.SelectedValue == null || ddlEmpActiveflagIns.SelectedValue == "")
            {
                flag = false;
                lblEmpActiveflagIns.Text = "Please Select Employee Status";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblEmpActiveflagIns.Text = "";
            }

            return flag;
        }
        protected Boolean ValidateCampaignCodeInsert()
        {
            Boolean flagempcode = true;

            string respstr = "";

            APIpath = APIUrl + "/api/support/EmpCodeValidate";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCodeTemp"] = txtEmpCodeIns.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            if (lEmpInfo.Count > 0)
            {
                flagempcode = false;
            }
            else
            {
                flagempcode = true;
            }
            return flagempcode;
        }
        
        protected List<EmpInfo> GetEmpListbyRefCode(String rCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RefCode"] = rCode;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);
            return lEmpInfo;
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using System.Globalization;
using System.IO;

namespace DOMS_TSR.src.UserManagement
{
    public partial class Role : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string APIpath = "";
        string Codelist = "";
        string RoleCodelist = "";
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
             
                
                loadRole();
            }
        }
        #region event
        protected void btnAddRole_Click(object sender, EventArgs e)
        {

            txtRoleCodeIns.Enabled = true;
            hidFlagInsert.Value = "True";

            txtRoleCodeIns.Text = "";
            txtRoleNameIns.Text = "";
          
        

            lblRoleCodeIns.Text = "";
            lblRoleNameIns.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-role').modal();", true);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            RoleInfo eInfo = new RoleInfo();

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

                        APIpath = APIUrl + "/api/support/InsertRole";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                         
                            data["RoleCode"] = txtRoleCodeIns.Text;
                            data["RoleName"] = txtRoleNameIns.Text;
                         
                           
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

                            loadRole();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-role').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {
                     

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateRole";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["RoleId"] = hidRoleIdIns.Value;
                            data["RoleCode"] = txtRoleCodeIns.Text;
                            data["RoleName"] = txtRoleNameIns.Text;
                           
                          
                            data["Updateby"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            loadRole();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-role').modal('hide');", true);



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
            isdelete = DeleteRole();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtRoleCodeIns.Text = "";
            txtRoleNameIns.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-role').modal('hide');", true);
            

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadRole();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchRoleCode.Text = "";
            txtSearchRoleName.Text = "";
        }
        protected void chkRoleAll_Changed(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRole.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvRole.HeaderRow.FindControl("chkRoleAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRole.Rows[i].FindControl("hidRoleId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkRole = (CheckBox)gvRole.Rows[i].FindControl("chkRole");

                    chkRole.Checked = true;
                }
                else
                {
                    CheckBox chkRole = (CheckBox)gvRole.Rows[i].FindControl("chkRole");

                    chkRole.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected void gvRole_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvRole.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");
            HiddenField hidRoleCode = (HiddenField)row.FindControl("hidRoleCode");
            Label lblRoleName = (Label)row.FindControl("lblRoleName");
          
        
            HiddenField hidRoleId = (HiddenField)row.FindControl("hidRoleId");

           
            if (e.CommandName == "ShowRole")
            {
                hidRoleIdIns.Value = hidRoleId.Value;
              
                    txtRoleNameIns.Text = lblRoleName.Text;
                     txtRoleCodeIns.Text = hidRoleCode.Value;

                    txtRoleCodeIns.Enabled = false;

                


                lblRoleCodeIns.Text = "";
                lblRoleNameIns.Text = "";
              

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-role').modal();", true);
            }
        }
        #endregion

        #region function
        protected Boolean validateInsertUpdate()
        {
            Boolean flag = true;



            if (txtRoleCodeIns.Text == "")
            {
                flag = false;
                lblRoleCodeIns.Text = MessageConst._MSG_PLEASEINSERT + " Role Code";
            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {

                    List<RoleInfo> lem = new List<RoleInfo>();

                    lem = GetRoleValidate();

                    if (lem.Count > 0)
                    {
                        flag = false;
                        lblRoleCodeIns.Text = "ไม่สามารถระบุรหัส Role ซ้ำ ";
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblRoleCodeIns.Text = "";
                    }
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblRoleCodeIns.Text = "";
                }
            }

            



            if (txtRoleNameIns.Text == "")
            {
                flag = false;
                lblRoleNameIns.Text = MessageConst._MSG_PLEASEINSERT + " Role Name";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblRoleNameIns.Text = "";
            }

       

            return flag;
        }


        protected void loadRole()
        {
            List<RoleInfo> lRoleInfo = new List<RoleInfo>();
            int? totalRow = CountRoleList();
            SetPageBar(Convert.ToDouble(totalRow));
            lRoleInfo = GetRoleByCriteria();
           
            gvRole.DataSource = lRoleInfo;
            gvRole.DataBind();
        }

        protected Boolean DeleteRole()
        {

            for (int i = 0; i < gvRole.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvRole.Rows[i].FindControl("chkRole");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRole.Rows[i].FindControl("hidRoleId");
                    HiddenField hidRoleCode = (HiddenField)gvRole.Rows[i].FindControl("hidRoleCode");
                    
                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }


                    if (RoleCodelist != "")
                    {
                        RoleCodelist += ",'" + hidRoleCode.Value + "'";
                    }
                    else
                    {
                        RoleCodelist += "'" + hidRoleCode.Value + "'";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteRoleList";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["RoleIdList"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);

                if (RoleCodelist != "")
                {
                    //Delete Emp role by RoleCode
                    APIpath = APIUrl + "/api/support/DeleteEmpRoleByRoleCodeList";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["EmpRoleIdList"] = RoleCodelist;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    cou = JsonConvert.DeserializeObject<int?>(respstr);
                }

            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;

        }

        protected int? CountRoleList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountRoleByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RoleCode"] = txtSearchRoleCode.Text;
                data["RoleName"] = txtSearchRoleName.Text;
              
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"RoleDetail.aspx?RoleCode=" + strCode + "\">" + strCode + "</a>";
        }

        protected List<RoleInfo> GetRoleValidate()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoleValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RoleCodeValidate"] = txtRoleCodeIns.Text;


                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoleInfo> lRoleInfo = JsonConvert.DeserializeObject<List<RoleInfo>>(respstr);

            return lRoleInfo;
        }

        protected List<RoleInfo> GetRoleByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoleByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["RoleCode"] = txtSearchRoleCode.Text;
                data["RoleName"] = txtSearchRoleName.Text;

                data["FlagDelete"] = "N";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoleInfo> lRoleInfo = JsonConvert.DeserializeObject<List<RoleInfo>>(respstr);
            return lRoleInfo;
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
            loadRole();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadRole();
        }
       
   
        #endregion


    }
}
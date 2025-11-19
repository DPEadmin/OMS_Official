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
    public partial class UserDetail : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;

        public Boolean check_UserLogin = false;
        public Boolean check_Role = false;
        public Boolean check_Merchant = false;

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
               
                loadEmp();
                loadMermap();
                loadEmpRole();
                loadddlRole(ddlRole);
                loadddlMerchant(ddlMerchant);
                Hide_Section();
                showUserLogin(true);
            }
        }
        #region event

        protected void showSection_UserLogin_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_UserLogin();
        }

        protected void showSection_Role_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Role();
        }
        protected void showSection_Merchant_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_Merchant();
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            UserLoginInfo uid = new UserLoginInfo();

            string respstr = "";

             EmpInfo empInfo = new EmpInfo();

            EmpInfo eInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["UserLoginId"] = hidUserLoginId.Value;
                    data["Username"] = txtusernameIns.Text;
                    data["Password"] = txtPasswordIns.Text;
                    data["EmpCode"] = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";

                    data["Updateby"] = empInfo.EmpCode;
                    data["Createby"] = empInfo.EmpCode;

                    if (hidUserLoginId.Value != "")
                    {
                        APIpath = APIUrl + "/api/support/UpdateUserLogin";
                    }
                    else
                    {
                        APIpath = APIUrl + "/api/support/InsertUserLogin";
                    }
                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);


                if (cou > 0)
                {

                    if (hidUserLoginId.Value != "")
                    {
                        hidUserLoginId.Value = cou.ToString();
                    }

                    loadEmp();
                   
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);



                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }
        }
    
        protected void btnClearEditUser_Click(object sender, EventArgs e)
        {
            txtusernameIns.Text = "";
            txtPasswordIns.Text = "";
        }
        protected void chkEmpRoleAll_Changed(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEmpRole.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvEmpRole.HeaderRow.FindControl("chkEmpRoleAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvEmpRole.Rows[i].FindControl("hidEmpRoleId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkEmpRole = (CheckBox)gvEmpRole.Rows[i].FindControl("chkEmpRole");

                    chkEmpRole.Checked = true;
                }
                else
                {
                    CheckBox chkEmpRole = (CheckBox)gvEmpRole.Rows[i].FindControl("chkEmpRole");

                    chkEmpRole.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }

        protected void chkMerchantAll_Changed(object sender, EventArgs e) //Merchant
        {
            for (int i = 0; i < gvMerchant.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvMerchant.HeaderRow.FindControl("chkMerchantAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvMerchant.Rows[i].FindControl("hidMermapId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkMerchant = (CheckBox)gvMerchant.Rows[i].FindControl("chkMerchant");

                    chkMerchant.Checked = true;
                }
                else
                {
                    CheckBox chkMerchant = (CheckBox)gvMerchant.Rows[i].FindControl("chkMerchant");

                    chkMerchant.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteEmpRole();

            loadEmpRole();

            loadddlRole(ddlRole);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnDeleteMer_Click(object sender, EventArgs e)
        {
            isdelete = DeleteMapMerhant();

            loadddlMerchant(ddlMerchant);

            loadMermap();
            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnSubmitMerchant_Click(object sender, EventArgs e) //Merchant
        {
            MerchantInfo lMer = new MerchantInfo();

            string respstr = "";

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            

            if (validateInsertMapMer())
            {

                if (empInfo == null)
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                else
                {
                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();


                        data["UserName"] = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";
                        data["MerchantCode"] = ddlMerchant.SelectedValue;
                        data["FlagDelete"] = "N";

                        data["Updateby"] = empInfo.EmpCode;
                        data["Createby"] = empInfo.EmpCode;


                        APIpath = APIUrl + "/api/support/InsertMermap";

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? cou = JsonConvert.DeserializeObject<int?>(respstr);


                    if (cou > 0)
                    {

                        loadddlMerchant(ddlMerchant);

                        loadMermap();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ไม่สามารถเพิ่ม Merchant ได้เนื่องจากไม่มี Username และ Password ในระบบ');", true);
            }

        }

        protected void btnSubmitRole_Click(object sender, EventArgs e)
        {
            EmpRole uid = new EmpRole();

            string respstr = "";

            EmpInfo empInfo = new EmpInfo();

            EmpInfo eInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (validateInsertEmpRole())
            {

                if (empInfo == null)
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

                }
                else
                {

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();


                        data["EmpCode"] = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";
                        data["RoleCode"] = ddlRole.SelectedValue;
                        data["FlagDelete"] = "N";

                        data["Updateby"] = empInfo.EmpCode;
                        data["Createby"] = empInfo.EmpCode;


                        APIpath = APIUrl + "/api/support/InsertEmpRole";

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? cou = JsonConvert.DeserializeObject<int?>(respstr);


                    if (cou > 0)
                    {
                                                
                        loadddlRole(ddlRole);

                        loadEmpRole();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
            }
        }

        #endregion

        #region function
        protected Boolean DeleteMapMerhant() //Merchant
        {

            for (int i = 0; i < gvMerchant.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvMerchant.Rows[i].FindControl("chkMerchant");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvMerchant.Rows[i].FindControl("hidMermapId");

                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteMapMerchant";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["MerchantIdList"] = Codelist;
                    


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

        protected Boolean DeleteEmpRole()
        {

            for (int i = 0; i < gvEmpRole.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvEmpRole.Rows[i].FindControl("chkEmpRole");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvEmpRole.Rows[i].FindControl("hidEmpRoleId");

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

                APIpath = APIUrl + "/api/support/DeleteEmpRoleList";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["EmpRoleIdList"] = Codelist;


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
        public void Hide_Section()
        {
            showUserLogin(false);
            showRole(false);
            showMerchant(false);
        }
        public void sectionLoad_UserLogin ()
        {

            showSection_UserLogin.CssClass = "btn btn-primary";
            showUserLogin(true);

           

        }
        public void sectionLoad_Role()
        {

            showSection_Role.CssClass = "btn btn-primary";
            showRole(true);

            

        }
        public void sectionLoad_Merchant()
        {

            showSection_Merchant.CssClass = "btn btn-primary";
            showMerchant(true);

            

        }
        public void btnSecondary()
        {
            if (check_UserLogin == true) { }
            else { showSection_UserLogin.CssClass = "btn-8bar-disable"; }

            if (check_Role == true) { }
            else { showSection_Role.CssClass = "  btn-8bar-disable2"; }
            if (check_Merchant == true) { }
            else { showSection_Merchant.CssClass = "  btn-8bar-disable3"; }
        }


        public void showUserLogin(Boolean show)
        {
            btnSecondary();
            showSection_UserLogin.CssClass = "btn btn-primary";


            Section_UserLogin.Visible = show;
        }

        public void showRole(Boolean show)
        {
            btnSecondary();
            showSection_Role.CssClass = "btn btn-primary";

           
            Section_Role.Visible = show;
        }
        public void showMerchant(Boolean show)
        {
            btnSecondary();
            showSection_Merchant.CssClass = "btn btn-primary";


            Section_Merchant.Visible = show;
        }
        protected void loadMermap()
        {
            List<MerchantInfo> lMerInfo = new List<MerchantInfo>();

            lMerInfo = GetListMermap();

            gvMerchant.DataSource = lMerInfo;
            gvMerchant.DataBind();
        }

        protected void loadEmpRole()
        {
            List<EmpRole> lEmpInfo = new List<EmpRole>();

            lEmpInfo = GetEmpRoleByCriteria();

            gvEmpRole.DataSource = lEmpInfo;
            gvEmpRole.DataBind();

        }

        protected void loadddlMerchant(DropDownList ddlMerchant)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMerchantEnterprise";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantName"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lMerInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);

            ddlMerchant.DataSource = lMerInfo;

            ddlMerchant.DataTextField = "MerchantName";

            ddlMerchant.DataValueField = "MerchantCode";

            ddlMerchant.DataBind();

            ddlMerchant.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }
        
        protected void loadddlRole(DropDownList ddlRole)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoleNotInEmpRoleByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoleInfo> lRoleInfo = JsonConvert.DeserializeObject<List<RoleInfo>>(respstr);


            ddlRole.DataSource = lRoleInfo;

            ddlRole.DataTextField = "RoleName";

            ddlRole.DataValueField = "RoleCode";

            ddlRole.DataBind();

            ddlRole.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));



        }

        protected Boolean validateInsertMapMer()
        {
            Boolean flag = true;

            List<EmpInfo> lEmpInfo = new List<EmpInfo>();

            lEmpInfo = GetEmpMasterByCriteria();

            if (lEmpInfo[0].Username == "")
            {
                flag = false;
                lblusernameIns.Text = "";
                lblPasswordIns.Text = "";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblusernameIns.Text = "";
            }

            if (ddlMerchant.SelectedValue == "-99")
            {
                flag = false;
                lblMerchant.Text = MessageConst._MSG_PLEASEINSERT + " Merchant";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMerchant.Text = "";
            }

            return flag;
        }

        protected Boolean validateInsertEmpRole()
        {
            Boolean flag = true;


            if (ddlRole.SelectedValue == "-99")
            {
                flag = false;
                lblRole.Text = MessageConst._MSG_PLEASEINSERT + " Role";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblRole.Text = "";
            }

            return flag;
        }
        protected List<EmpRole> GetEmpRoleByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpRoleByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";
                data["rowOFFSet"] = "0";
                data["rowFetch"] = StaticField.UserDetail_rowfetch_100;


                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpRole> lEmpInfo = JsonConvert.DeserializeObject<List<EmpRole>>(respstr);
            return lEmpInfo;
        }


        protected void loadEmp()
        {
            List<EmpInfo> lEmpInfo = new List<EmpInfo>();
            string empcode = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";
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
                lblEmpCodeIns.Text = lEmpInfo[0].EmpCode;
               
                lblEmpFNameTHIns.Text = lEmpInfo[0].EmpFname_TH;
             
                lblEmpLNameTHIns.Text = lEmpInfo[0].EmpLname_TH;
                lblMobileIns.Text = lEmpInfo[0].Mobile;
                lblBUIns.Text = lEmpInfo[0].BuName;
                lblEmailIns.Text = lEmpInfo[0].Mail;
                lblExtensionid.Text = lEmpInfo[0].ExtensionID;
                hidUserLoginId.Value = lEmpInfo[0].UserLoginId;
                txtusernameIns.Text = lEmpInfo[0].Username;
                txtPasswordIns.Text = lEmpInfo[0].Password;
            }

        }
        protected List<MerchantInfo> GetListMermap()
        {
            

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMerchantEnterpriseForAuth";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                

                data["UserName"] = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lMerInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);
            return lMerInfo;

        }
     
        protected List<EmpInfo> GetEmpMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpByCriteriaUserDetail";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                 
                data["EmpCode"] = (Request.QueryString["EmpCode"] != null) ? Request.QueryString["EmpCode"].ToString() : "";
                data["FlagDelete"] = "N";
                
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);
            return lEmpInfo;
        }

        #endregion

        #region binding

        #endregion

       
    }
}
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
using System.Web.Services;
using System.Text.RegularExpressions;

namespace DOMS_TSR.src.SettingEmpBUMapLevel
{
    public partial class SettingEmpBUMapLevel : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        string BuCodeinit = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null)
                {
                    APIUrl = empInfo.ConnectionAPI;
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                GetEmpBUInit();
                BindddlEmp();
                LoadEmpMapBULevel();
            }
        }


        #region FUNCTION
        
        protected void GetEmpBUInit()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = hidEmpCode.Value;
                data["ActiveFlag"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lemployeeInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            if (lemployeeInfo.Count > 0)
            {
                BuCodeinit = lemployeeInfo[0].BuCode;
            }
        }
        protected void LoadEmpMapBULevel()
        {
            GetEmpBUInit();

            string a = "";
            List<EmpMapBu> lInventoryInfo = new List<EmpMapBu>();

            

            int? totalRow = CountEmpMapBULevelMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            lInventoryInfo = GetEemMapBULevelMasterByCriteria();

            gvEmpMapBu.DataSource = lInventoryInfo;

            gvEmpMapBu.DataBind();
        }

        public int? CountEmpMapBULevelMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountEmpMapBuPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCodeCode.Text.Trim();
                data["Bu"] = BuCodeinit;
                data["Role"] = txtSearchRole.Text.Trim();
                data["Levels"] = txtSearchLevels.Text.Trim();
                data["EmpCode"] = txtSearchRole.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        public List<EmpMapBu> GetEemMapBULevelMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpMapBuPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = txtSearchEmpCodeCode.Text.Trim();
                data["Bu"] = BuCodeinit;
                data["Role"] = txtSearchRole.Text.Trim();
                data["Levels"] = txtSearchLevels.Text.Trim();
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpMapBu> lEmpMapBUInfo = JsonConvert.DeserializeObject<List<EmpMapBu>>(respstr);

            return lEmpMapBUInfo;

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

            LoadEmpMapBULevel();
        }

        protected void ClearSearch()
        {
            txtSearchEmpCodeCode.Text = "";
            txtSearchRole.Text = "";
            txtSearchLevels.Text = "";

            lblSearchEmpCodeCode.Text = "";
            lblSearchRole.Text = "";
            lblSearchLevels.Text = "";
        }

        protected Boolean validateInsertUpdate()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[ก-๙a-zA-Z0-9/ ]*$");
            var regexNumber = new Regex("^[1-9 ]*$");

            if (ddlEmpCode_Ins.SelectedValue == "-99" || ddlEmpCode_Ins.SelectedValue == "")
            {
                flag = false;
                lblEmpCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสพนักงาน";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEmpCode_Ins.Text = "";
            }

            if (txtBu_Ins.Text == "")
            {
                flag = false;
                lblBu_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัส Bu";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblBu_Ins.Text = "";
            }

            if (ddlRole_Ins.SelectedValue == "-99" || ddlRole_Ins.SelectedValue == "")
            {
                flag = false;
                lblRole_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัส Role";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblRole_Ins.Text = "";
            }

            if (txtLevels_Ins.Text == "")
            {
                flag = false;
                lblLevels_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Level";
            }
            else if (regexNumber.IsMatch(txtLevels_Ins.Text))
            {
                flag = (flag == false) ? false : true;
                lblLevels_Ins.Text = "";
            }
            else
            {
                flag = false;
                lblLevels_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Level ให้ถูกต้อง";
            }

            if (hidFlagInsert.Value == "True")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/ListEmpMapRoleValidateDuplicateInsert";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["EmpCode"] = ddlEmpCode_Ins.SelectedValue;
                    data["Bu"] = txtBu_Ins.Text;
                    data["Role"] = ddlRole_Ins.SelectedValue;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                List<EmpMapBu> lemployeeInfo = JsonConvert.DeserializeObject<List<EmpMapBu>>(respstr);

                if (lemployeeInfo.Count > 0)
                {
                    flag = false;
                    lblEmpCode_Ins.Text = "EmpCode, Bu และ Role นี้มีข้อมูลในการ Mapping อยู่แล้ว";
                    lblBu_Ins.Text = "EmpCode, Bu และ Role นี้มีข้อมูลในการ Mapping อยู่แล้ว";
                    lblRole_Ins.Text = "EmpCode, Bu และ Role นี้มีข้อมูลในการ Mapping อยู่แล้ว";
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblLevels_Ins.Text = "";
                }
            }

            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-empmapbu').modal();", true);
            }            

            return flag;
        }

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryValidatePagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            if (lInventoryInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        protected Boolean DeleteEmpMapBu()
        {
            for (int i = 0; i < gvEmpMapBu.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvEmpMapBu.Rows[i].FindControl("chkEmpMapBu");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvEmpMapBu.Rows[i].FindControl("hidEmpMapBuId");

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

                APIpath = APIUrl + "/api/support/DeleteEmpMapBu";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["EmpCode"] = Codelist;

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

        protected List<ProductInfo> GetProductMaster()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = "";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }
        #endregion FUNCTION

        #region BINDING
        protected void BindddlEmp()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ActiveFlag"] = StaticField.ActiveFlag_Y; 
                data["BUCode"] = BuCodeinit;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lEmpInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);


            ddlEmpCode_Ins.DataSource = lEmpInfo;
            ddlEmpCode_Ins.DataTextField = "EmpName_TH";
            ddlEmpCode_Ins.DataValueField = "EmpCode";
            ddlEmpCode_Ins.DataBind();
            ddlEmpCode_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected void BindddlRole_Ins(string empcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpRoleNoPagingforDDLByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpRole> lemproleInfo = JsonConvert.DeserializeObject<List<EmpRole>>(respstr);

            if (lemproleInfo.Count > 0)
            {
                ddlRole_Ins.DataSource = lemproleInfo;
                ddlRole_Ins.DataTextField = "RoleName";
                ddlRole_Ins.DataValueField = "RoleCode";
                ddlRole_Ins.DataBind();
                ddlRole_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
            }
        }

        #endregion BINDING

        #region EVENT
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                currentPageNumber = 1;
                
                LoadEmpMapBULevel();
            }

        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }
        protected void btnAddEmpBUMapLevel_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";

            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-empmapbu').modal();", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteEmpMapBu();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ลบข้อมูลสำเร็จ');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            MerchantInfo merchantInfo = new MerchantInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            merchantInfo = (MerchantInfo)Session["MerchantInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (validateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertEmpMapBu";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["EmpCode"] = ddlEmpCode_Ins.SelectedValue;
                            data["Bu"] = txtBu_Ins.Text;
                            data["Role"] = ddlRole_Ins.SelectedValue;
                            data["Levels"] = txtLevels_Ins.Text.ToString();

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            LoadEmpMapBULevel();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-empmapbu').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else //Update
                {
                    if (validateInsertUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateEmpMapBu";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["Levels"] = txtLevels_Ins.Text.ToString();
                            data["EmpMapBuId"] = hidEmpMapRoleIdforUpdate.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            LoadEmpMapBULevel();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-empmapbu').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlEmpCode_Ins.SelectedValue = "-99";
            txtBu_Ins.Text = "";
            ddlRole_Ins.SelectedValue = "-99";
            txtLevels_Ins.Text = "";
            lblEmpCode_Ins.Text = "";
            lblBu_Ins.Text = "";
            lblRole_Ins.Text = "";
            lblLevels_Ins.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-empmapbu').modal('hide');", true);

            
        }

        protected void chkEmpMapBuAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEmpMapBu.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvEmpMapBu.HeaderRow.FindControl("chkEmpMapBuAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvEmpMapBu.Rows[i].FindControl("hidEmpMapBuId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkEmpMapBu = (CheckBox)gvEmpMapBu.Rows[i].FindControl("chkEmpMapBu");

                    chkEmpMapBu.Checked = true;
                }
                else
                {
                    CheckBox chkEmpMapBu = (CheckBox)gvEmpMapBu.Rows[i].FindControl("chkEmpMapBu");

                    chkEmpMapBu.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void gvEmpMapBu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvEmpMapBu.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidEmpMapBuId = (HiddenField)row.FindControl("hidEmpMapBuId");
            Label lblEmpCode = (Label)row.FindControl("lblEmpCode");
            Label lblBu = (Label)row.FindControl("lblBu");            
            Label lblRole = (Label)row.FindControl("lblRole");
            Label lblLevels = (Label)row.FindControl("lblLevels");

            if (e.CommandName == "ShowInventory")
            {
                try
                {
                    hidFlagInsert.Value = "False";

                    hidIdList.Value = hidEmpMapBuId.Value;
                    hidEmpMapRoleIdforUpdate.Value = hidEmpMapBuId.Value;

                    ddlEmpCode_Ins.SelectedValue = lblEmpCode.Text;
                    txtBu_Ins.Text = lblBu.Text;

                    BindddlRole_Ins(lblEmpCode.Text);

                    ddlRole_Ins.SelectedValue = lblRole.Text;
                    txtLevels_Ins.Text = lblLevels.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-empmapbu').modal();", true);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

            }
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadEmpMapBULevel();
        }

        protected void ddlEmpCode_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empcode = ddlEmpCode_Ins.SelectedValue.ToString();

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empcode;
                data["ActiveFlag"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> lempInfo = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            if (lempInfo.Count > 0)
            {
                txtBu_Ins.Text = lempInfo[0].BuCode;
            }

            BindddlRole_Ins(empcode);
        }

        private bool CheckSymbol(string value)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (value.Contains(item)) return true;
            }

            return false;
        }
        protected Boolean validateSearch()
        {
            Boolean flag = true;

            var regexItem = new Regex("^[ก-๙a-zA-Z0-9/ ]*$");
            var regexNumber = new Regex("^[1-9 ]*$");

            if (regexNumber.IsMatch(txtLevels_Ins.Text))
            {
                flag = (flag == false) ? false : true;
                lblLevels_Ins.Text = "";
            }
            else
            {
                flag = false;
                lblLevels_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Level ให้ถูกต้อง";
            }

            return flag;
        }

        #endregion EVENT

    }
}
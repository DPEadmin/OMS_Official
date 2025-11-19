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

namespace DOMS_TSR.src.Purchasing
{
    public partial class Supplier : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
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
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                BindddlProvince();
                loadSupplier();
            }
        }
        #region event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadSupplier();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchSupplierCode.Text = "";
            txtSearchSuplierName.Text = "";
            ddlSearchStatus.SelectedValue = "";
        }
        protected void btnAddSupplier_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";

            if (hidFlagInsert.Value == "True")
            {
                txtSupplierCodeIns.ReadOnly = false;
            }

            txtSupplierCodeIns.Text = "";
            txtSupplierNameIns.Text = "";
            txtTaxIdNoIns.Text = "";
            txtAddressIns.Text = "";
            ddlProvinceIns.SelectedValue = "";
            ddlDistrictIns.SelectedValue = null;
            ddlSubDistrictIns.SelectedValue = "";
            txtZipNoIns.Text = "";
            txtFaxNoIns.Text = "";
            txtEmailIns.Text = "";
            ddlStatusIns.SelectedValue = "";

            lblSupplierCodeIns.Text = "";
            lblSupplierNameIns.Text = "";
            lblTaxIdNoIns.Text = "";
            lblAddressIns.Text = "";
            lblProvinceIns.Text = "";
            lblDistrictIns.Text = "";
            lblSubDistrictIns.Text = "";
            lblZipNoIns.Text = "";
            lblFaxNoIns.Text = "";
            lblEmailIns.Text = "";
            lblStatusIns.Text = "";



            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal();", true);
        }
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDistrict(ddlProvinceIns.SelectedValue, ddlDistrictIns);
            ddlDistrictIns.SelectedIndex = 0;
        }
        protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvSupplier.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidSupplierId = (HiddenField)row.FindControl("hidSupplierId");
            HiddenField hidSupplierCode = (HiddenField)row.FindControl("hidSupplierCode");
            HiddenField hidSupplierName = (HiddenField)row.FindControl("hidSupplierName");
            HiddenField hidTaxIdNo = (HiddenField)row.FindControl("hidTaxIdNo");
            HiddenField hidAddress = (HiddenField)row.FindControl("hidAddress");
            HiddenField hidProvinceCode = (HiddenField)row.FindControl("hidProvinceCode");
            HiddenField hidDistrictCode = (HiddenField)row.FindControl("hidDistrictCode");
            HiddenField hidSubDistrictCode = (HiddenField)row.FindControl("hidSubDistrictCode");
            HiddenField hidZipNo = (HiddenField)row.FindControl("hidZipNo");
            HiddenField hidFaxNumber = (HiddenField)row.FindControl("hidFaxNumber");
            HiddenField hidPhoneNumber = (HiddenField)row.FindControl("hidPhoneNumber");
            HiddenField hidMail = (HiddenField)row.FindControl("hidMail");
            HiddenField hidContactor = (HiddenField)row.FindControl("hidContactor");
            HiddenField hidActiveFlag = (HiddenField)row.FindControl("hidActiveFlag");

            if (e.CommandName == "ShowSupplier")
            {
                hidSupplierIdIns.Value = hidSupplierId.Value;
                txtSupplierCodeIns.ReadOnly = true;
                txtSupplierCodeIns.Text = hidSupplierCode.Value;
                txtSupplierNameIns.Text = hidSupplierName.Value;
                txtTaxIdNoIns.Text = hidTaxIdNo.Value;
                txtAddressIns.Text = hidAddress.Value;
                ddlProvinceIns.SelectedValue = hidProvinceCode.Value;
                ddlDistrictIns.Items.Clear();
                ddlDistrictIns.SelectedValue = hidDistrictCode.Value;
                String a = "";
                BindddlSubDistrict(a);
                ddlSubDistrictIns.SelectedValue = hidSubDistrictCode.Value;
                BindddlDistrict(a, ddlDistrictIns);
                txtZipNoIns.Text = hidZipNo.Value;
                txtPhoneNumberIns.Text = hidPhoneNumber.Value;
                txtFaxNoIns.Text = hidFaxNumber.Value;
                txtEmailIns.Text = hidMail.Value;
                ddlStatusIns.SelectedValue = hidActiveFlag.Value;
                txtContactorIns.Text = hidContactor.Value;

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal();", true);
            }
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

                        APIpath = APIUrl + "/api/support/InsertSupplier";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["SupplierCode"] = txtSupplierCodeIns.Text;
                            data["SupplierName"] = txtSupplierNameIns.Text;
                            data["TaxIdNo"] = txtTaxIdNoIns.Text;
                            data["Address"] = txtAddressIns.Text;
                            data["ProvinceCode"] = ddlProvinceIns.SelectedValue;
                            data["DistrictCode"] = ddlDistrictIns.SelectedValue;
                            data["SubDistrictCode"] = ddlSubDistrictIns.SelectedValue;
                            data["ZipNo"] = txtZipNoIns.Text;
                            data["PhoneNumber"] = txtPhoneNumberIns.Text;
                            data["FaxNumber"] = txtFaxNoIns.Text;
                            data["Mail"] = txtEmailIns.Text;
                            data["Contactor"] = txtContactorIns.Text;
                            data["ActiveFlag"] = ddlStatusIns.SelectedValue;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int sum = JsonConvert.DeserializeObject<int>(respstr);
                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadSupplier();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-addsupplier').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal();", true);
                    }
                }
                else // Update Employee
                {
                    if (ValidateInsertandUpdate())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateSupplier";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["SupplierId"] = hidSupplierIdIns.Value;
                            data["SupplierCode"] = txtSupplierCodeIns.Text;
                            data["SupplierName"] = txtSupplierNameIns.Text;
                            data["TaxIdNo"] = txtTaxIdNoIns.Text;
                            data["Address"] = txtAddressIns.Text;
                            data["ProvinceCode"] = ddlProvinceIns.SelectedValue;
                            data["DistrictCode"] = ddlDistrictIns.SelectedValue;
                            data["SubDistrictCode"] = ddlSubDistrictIns.SelectedValue;
                            data["ZipNo"] = txtZipNoIns.Text;
                            data["PhoneNumber"] = txtPhoneNumberIns.Text;
                            data["FaxNumber"] = txtFaxNoIns.Text;
                            data["Mail"] = txtEmailIns.Text;
                            data["Contactor"] = txtContactorIns.Text;
                            data["ActiveFlag"] = ddlStatusIns.SelectedValue;
                            data["FlagDelete"] = "N";
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadSupplier();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-addsupplier').modal('hide');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-addsupplier').modal();", true);
                    }
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (hidFlagInsert.Value == "True")
            {
                txtSupplierCodeIns.ReadOnly = false;
                txtSupplierCodeIns.Text = "";
            }
            else
            {
                txtSupplierCodeIns.ReadOnly = true;
            }
            txtSupplierCodeIns.Text = "";
            txtSupplierNameIns.Text = "";
            txtTaxIdNoIns.Text = "";
            txtAddressIns.Text = "";
            ddlProvinceIns.SelectedValue = "";
            ddlDistrictIns.SelectedValue = "";
            ddlSubDistrictIns.SelectedValue = "";
            txtZipNoIns.Text = "";
            txtFaxNoIns.Text = "";
            txtEmailIns.Text = "";
            ddlStatusIns.SelectedValue = "";
            txtContactorIns.Text = "";

            lblSupplierCodeIns.Text = "";
            lblSupplierNameIns.Text = "";
            lblTaxIdNoIns.Text = "";
            lblAddressIns.Text = "";
            lblProvinceIns.Text = "";
            lblDistrictIns.Text = "";
            lblSubDistrictIns.Text = "";
            lblZipNoIns.Text = "";
            lblFaxNoIns.Text = "";
            lblEmailIns.Text = "";
            lblStatusIns.Text = "";
            lblContactorIns.Text = "";
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteSupplier();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }
        protected void chkSupplierAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvSupplier.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvSupplier.HeaderRow.FindControl("chkSupplierAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvSupplier.Rows[i].FindControl("hidSupplierId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkSupplier = (CheckBox)gvSupplier.Rows[i].FindControl("chkSupplier");

                    chkSupplier.Checked = true;
                }
                else
                {
                    CheckBox chkSupplier = (CheckBox)gvSupplier.Rows[i].FindControl("chkSupplier");

                    chkSupplier.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"SupplierDetail.aspx?SupplierCode=" + strCode + "\">" + strCode + "</a>";
        }
        #endregion

        #region function
        protected void loadSupplier()
        {
            List<SupplierInfo> lsupplierInfo = new List<SupplierInfo>();
            int? totalRow = CountSupplierMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lsupplierInfo = GetSupplierMasterByCriteria();
            if (lsupplierInfo.Count > 0)
            {
                foreach (var supplierinfoV in lsupplierInfo)
                {
                    if (supplierinfoV.ActiveFlag == StaticField.ActiveFlag_Y) 
                    {
                        supplierinfoV.ActiveFlagName = StaticField.ActiveFlag_Y_NameValue_Active; 
                    }
                    else
                    {
                        supplierinfoV.ActiveFlagName = StaticField.ActiveFlag_N_NameValue_Inactive; 
                    }
                }
            }
            gvSupplier.DataSource = lsupplierInfo;
            gvSupplier.DataBind();
        }
        protected int? CountSupplierMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountSupplierListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = "";
                data["CampaignName"] = "";
                data["Active"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected List<SupplierInfo> GetSupplierMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSupplierByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["SupplierCode"] = txtSearchSupplierCode.Text;
                data["SupplierName"] = txtSearchSuplierName.Text;
                data["ActiveFlag"] = ddlSearchStatus.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lEmpInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);
            return lEmpInfo;
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
        public List<ProvinceInfo> ListProvinceMasterInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);

            return lProvinceInfo;
        }
        public List<DistrictInfo> ListDistrictMasterInfo(string ProvinceCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ProvinceCode"] = ProvinceCode;
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);

            return lDistrictInfo;
        }
        protected void BindddlSubDistrict(string DistrictCode)
        {
            List<SubDistrictInfo> lSubDistrictInfo = new List<SubDistrictInfo>();

            lSubDistrictInfo = ListSubDistrictMasterInfo(DistrictCode);

            ddlSubDistrictIns.DataSource = lSubDistrictInfo;
            ddlSubDistrictIns.DataValueField = "SubDistrictCode";
            ddlSubDistrictIns.DataTextField = "SubDistrictName";
            ddlSubDistrictIns.DataBind();

            ddlSubDistrictIns.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", ""));
        }
        public List<SubDistrictInfo> ListSubDistrictMasterInfo(string DistrictCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["DistrictCode"] = DistrictCode;
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);

            return lSubDistrictInfo;
        }
        protected Boolean ValidateInsertandUpdate()
        {
            Boolean flag = true;

            if (txtSupplierCodeIns.Text == "" || txtSupplierCodeIns.Text == null)
            {
                flag = false;
                lblSupplierCodeIns.Text = "Please insert รหัสผู้ซื้อ";
            }
            else
            {
                if (hidFlagInsert.Value == "True")
                {
                    if (ValidateSupplierCodeInsert())
                    {
                        flag = (!flag) ? false : true;
                        lblSupplierCodeIns.Text = "";
                    }
                    else
                    {
                        flag = false;
                        lblSupplierCodeIns.Text = "รหัสผู้ผลิตซ้ำ";
                    }
                }
            }
            if (txtSupplierNameIns.Text == "" || txtSupplierNameIns.Text == null)
            {
                flag = false;
                lblSupplierNameIns.Text = "Please insert รหัสชื่อผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblSupplierNameIns.Text = "";
            }
            if (txtTaxIdNoIns.Text == "" || txtTaxIdNoIns.Text == null)
            {
                flag = false;
                lblTaxIdNoIns.Text = "Please insert รหัสประจำตัวผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblTaxIdNoIns.Text = "";
            }
            if (txtAddressIns.Text == "" || txtAddressIns == null)
            {
                flag = false;
                lblAddressIns.Text = "Please insert ที่อยู่ผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblAddressIns.Text = "";
            }
            if (ddlProvinceIns.SelectedValue == "" || ddlProvinceIns.SelectedValue == null)
            {
                flag = false;
                lblProvinceIns.Text = "Please insert จังหวัดของผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblProvinceIns.Text = "";
            }
            if (ddlDistrictIns.SelectedValue == "" || ddlDistrictIns.SelectedValue == null)
            {
                flag = false;
                lblDistrictIns.Text = "Please insert เขต/อำเภอของผู้ผลิต";
            }
            else;
            {
                flag = (!flag) ? false : true;
                lblDistrictIns.Text = "";
            }
            if (ddlSubDistrictIns.SelectedValue == "" || ddlSubDistrictIns.SelectedValue == null)
            {
                flag = false;
                lblSubDistrictIns.Text = "Please insert แขวง/ตำบลของผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblSubDistrictIns.Text = "";
            }
            if (txtZipNoIns.Text == "" || txtZipNoIns.Text == null)
            {
                flag = false;
                lblZipNoIns.Text = "Please insert รหัสไปรษณีย์ของผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblZipNoIns.Text = "";
            }
            if (txtPhoneNumberIns.Text == "" || txtPhoneNumberIns.Text == null)
            {
                flag = false;
                lblPhoneNumberIns.Text = "Please insert เบอร์โทรของผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblPhoneNumberIns.Text = "";
            }
            if (txtFaxNoIns.Text == "" || txtFaxNoIns.Text == null)
            {
                flag = false;
                lblPhoneNumberIns.Text = "Please insert แฟกซ์ของผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblFaxNoIns.Text = "";
            }
            if (txtEmailIns.Text == "" || txtEmailIns.Text == null)
            {
                flag = false;
                lblEmailIns.Text = "Please insert อีเมล์ของผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblEmailIns.Text = "";
            }
            if (ddlStatusIns.SelectedValue == "" || ddlStatusIns.SelectedValue == null)
            {
                flag = false;
                lblEmailIns.Text = "Please insert อีเมล์ของผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblEmailIns.Text = "";
            }
            if (txtContactorIns.Text == "" || txtContactorIns.Text == null)
            {
                flag = false;
                lblContactorIns.Text = "Please insert ตัวแทนผู้ผลิต";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblContactorIns.Text = "";
            }
            return flag;
        }
        protected Boolean ValidateSupplierCodeInsert()
        {
            Boolean flagsuppliercode = true;

            string respstr = "";

            APIpath = APIUrl + "/api/support/SupplierCodeValidate";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["SupplierCode"] = txtSupplierCodeIns.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lSupplierInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);

            if (lSupplierInfo.Count > 0)
            {
                flagsuppliercode = false;
            }
            else
            {
                flagsuppliercode = true;
            }
            return flagsuppliercode;
        }
        protected Boolean DeleteSupplier()
        {

            for (int i = 0; i < gvSupplier.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvSupplier.Rows[i].FindControl("chkSupplier");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvSupplier.Rows[i].FindControl("hidSupplierId");

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
                APIpath = APIUrl + "/api/support/DeleteSupplier";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["SupplierCode"] = Codelist;

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
        #endregion

        #region binding
        protected void BindddlProvince()
        {
            List<ProvinceInfo> lProvinceInfo = new List<ProvinceInfo>();
            lProvinceInfo = ListProvinceMasterInfo();

            ddlProvinceIns.DataSource = lProvinceInfo;
            ddlProvinceIns.DataValueField = "ProvinceCode";
            ddlProvinceIns.DataTextField = "ProvinceName";
            ddlProvinceIns.DataBind();

            ddlProvinceIns.Items.Insert(0, new ListItem("กรุณาเลือกจังหวัด", ""));
        }
        protected void BindddlDistrict(string ProvinceCode, DropDownList ddl)
        {
            List<DistrictInfo> lDistrictInfo = new List<DistrictInfo>();

            lDistrictInfo = ListDistrictMasterInfo(ProvinceCode);

            ddl.DataSource = lDistrictInfo;
            ddl.DataValueField = "DistrictCode";
            ddl.DataTextField = "DistrictName";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", ""));
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlSubDistrict(ddlDistrictIns.SelectedValue);
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
            loadSupplier();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadSupplier();
        }
        #endregion
    }
}
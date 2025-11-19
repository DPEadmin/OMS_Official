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

namespace DOMS_TSR.src.Voucher

{
    public partial class Voucher : System.Web.UI.Page
    {
        
        protected static string APIUrl;

        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";

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


                LoadVoucher();

                BindddlLookup(ddlVoucherType_Ins, StaticField.LookupType_VOUCHERTYPE);
                BindddlLookup(ddlStatus_Ins, StaticField.LookupType_VOUCHERSTATUS);
                BindddlLookup(ddlStatus, StaticField.LookupType_VOUCHERSTATUS);
                BindddlLookup(ddlVoucherType, StaticField.LookupType_VOUCHERTYPE);

                BindddlCampaignCategory(ddlCampaignCategory);
                BindddlCampaignCategory(ddlCampaignCategory_Ins);
            }
        }

        #region Function
        

        protected void LoadVoucher()
        {
            List<VoucherInfo> lVoucherInfo = new List<VoucherInfo>();

            

            int? totalRow = CountVoucherList();

            SetPageBar(Convert.ToDouble(totalRow));


            lVoucherInfo = GetVoucherByCriteria();

            gvVoucher.DataSource = lVoucherInfo;

            gvVoucher.DataBind();


            

        }

        protected void ClearSearch()
        {
            txtVoucherCode.Text = "";
            txtVoucherName.Text = "";
            ddlVoucherType.ClearSelection();
            ddlCampaignCategory.ClearSelection();
            txtPrice.Text = "";
            ddlStatus.Text = "";
            txtStartDateFrom.Text = "";
            txtStartDateFrom.Text = "";
            txtEndDateFrom.Text = "";
            txtEndDateTo.Text = "";
         
        }

        protected void ClearInsertSection()
        {
            txtStartDate_Ins.Text = "";
            txtEndDate_Ins.Text = "";
            ddlStatus_Ins.ClearSelection();
            txtRemark_Ins.Text = "";
            txtAmount_Ins.Text = "";
            txtVoucherCode_Ins.Text = "";
            txtVoucherName_Ins.Text = "";
            ddlVoucherType_Ins.ClearSelection();
            ddlCampaignCategory_Ins.ClearSelection();
            txtPrice_Ins.Text = "";
        }

        public List<VoucherInfo> GetVoucherByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListVoucherByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EndDateFrom"] = txtEndDateFrom.Text;
                data["EndDateTo"] = txtEndDateTo.Text;
                data["StartDateFrom"] = txtStartDateFrom.Text;
                data["StartDateTo"] = txtStartDateTo.Text;
                data["Price"] = txtPrice.Text;
                data["VoucherCode"] = txtVoucherCode.Text;
                data["VoucherName"] = txtVoucherName.Text;
                data["CampaignCategoryCode"] = ddlCampaignCategory.SelectedValue;
                data["VoucherTypeCode"] = ddlVoucherType.SelectedValue;
                data["StatusCode"] = ddlStatus.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<VoucherInfo> lVoucherInfo = JsonConvert.DeserializeObject<List<VoucherInfo>>(respstr);


            return lVoucherInfo;

        }

        public int? CountVoucherList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountVoucherListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EndDateFrom"] = txtEndDateFrom.Text;
                data["EndDateTo"] = txtEndDateTo.Text;
                data["StartDateFrom"] = txtStartDateFrom.Text;
                data["StartDateTo"] = txtStartDateTo.Text;
                data["Price"] = txtPrice.Text;
                data["VoucherCode"] = txtVoucherCode.Text;
                data["VoucherName"] = txtVoucherName.Text;
                data["CampaignCategoryCode"] = ddlCampaignCategory.SelectedValue;
                data["VoucherTypeCode"] = ddlVoucherType.SelectedValue;
                data["StatusCode"] = ddlStatus.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (txtVoucherCode_Ins.Text == "")
            {
                flag = false;
                txtVoucherCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " โค้ดบัตรกำนัล";
            }
            else
            {
                if (txtVoucherCode_Ins.Text != "")
                {


                    Boolean isDuplicate = ValidateDuplicate();


                    if (isDuplicate)
                    {
                        flag = false;
                        lblVoucherCode_Ins.Text = MessageConst._DATA_NComplete;

                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblVoucherCode_Ins.Text = "";

                    }
                }
            }

          

            if (txtVoucherName_Ins.Text == "")
            {
                flag = false;
                lblVoucherName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อบัตรกำนัล";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblVoucherName_Ins.Text = "";
            }    
            
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-driver').modal();", true);
            }

            return flag;
        }

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListVoucherNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["VoucherCode_Validate"] = txtVoucherCode_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<VoucherInfo> lVoucherInfo = JsonConvert.DeserializeObject<List<VoucherInfo>>(respstr);

            if (lVoucherInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        protected Boolean DeleteVoucher()
        {

            EmpInfo empInfo = new EmpInfo();

            VoucherInfo pInfo = new VoucherInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            for (int i = 0; i < gvVoucher.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvVoucher.Rows[i].FindControl("chkVoucher");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvVoucher.Rows[i].FindControl("hidVoucherId");

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

                APIpath = APIUrl + "/api/support/DeleteVoucher";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["VoucherIdList"] = Codelist;
                    data["UpdateBy"] = empInfo.EmpCode;

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

        #endregion Function

        #region Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadVoucher();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }
        protected void btnAddDriver_Click(object sender, EventArgs e)
        {
            ClearInsertSection();
            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-driver').modal();", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteVoucher();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            VoucherInfo pInfo = new VoucherInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (validateInsert())
                    {
                        HttpFileCollection uploadFiles = Request.Files;

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertVoucher";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["VoucherCode"] = txtVoucherCode_Ins.Text;
                            data["VoucherName"] = txtVoucherName_Ins.Text;
                            data["StatusCode"] = ddlStatus_Ins.SelectedValue;
                            data["VoucherTypeCode"] = ddlVoucherType_Ins.SelectedValue;
                            data["CampaignCategoryCode"] = ddlCampaignCategory_Ins.SelectedValue;
                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["Price"] = txtPrice_Ins.Text;
                            data["Remark"] = txtRemark_Ins.Text;
                            data["Quantity"] = txtAmount_Ins.Text;
                            data["Reserve"] = "0";
                            data["Balance"] = txtAmount_Ins.Text;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadVoucher();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-driver').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }

                }
                else //Update
                {

                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateVoucher";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();
                        data["VoucherId"] = hidIdList.Value;
                        data["VoucherCode"] = txtVoucherCode_Ins.Text;
                        data["VoucherName"] = txtVoucherName_Ins.Text;
                        data["StatusCode"] = ddlStatus_Ins.SelectedValue;
                        data["VoucherTypeCode"] = ddlVoucherType_Ins.SelectedValue;
                        data["CampaignCategoryCode"] = ddlCampaignCategory_Ins.SelectedValue;
                        data["StartDate"] = txtStartDate_Ins.Text;
                        data["EndDate"] = txtEndDate_Ins.Text;
                        data["Price"] = txtPrice_Ins.Text;
                        data["Remark"] = txtRemark_Ins.Text;
                        data["Quantity"] = txtAmount_Ins.Text;
                        data["Reserve"] = "0";
                        data["Balance"] = txtAmount_Ins.Text;
                        data["CreateBy"] = empInfo.EmpCode;
                        data["FlagDelete"] = "N";

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        LoadVoucher();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-driver').modal('hide');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }

                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearInsertSection();
        }

        protected void chkVoucherAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvVoucher.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvVoucher.HeaderRow.FindControl("chkVoucherAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvVoucher.Rows[i].FindControl("hidVoucherId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkVoucher = (CheckBox)gvVoucher.Rows[i].FindControl("chkVoucher");

                    chkVoucher.Checked = true;
                }
                else
                {

                    CheckBox chkVoucher = (CheckBox)gvVoucher.Rows[i].FindControl("chkVoucher");

                    chkVoucher.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void gvVoucher_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvVoucher.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");
            
            HiddenField hidVoucherId = (HiddenField)row.FindControl("hidVoucherId");
            HiddenField hidVoucherCode = (HiddenField)row.FindControl("hidVoucherCode");
            HiddenField hidVoucherName = (HiddenField)row.FindControl("hidVoucherName");
            HiddenField hidVoucherTypeCode = (HiddenField)row.FindControl("hidVoucherTypeCode");
            HiddenField hidVoucherStatusCode = (HiddenField)row.FindControl("hidVoucherStatusCode");
            HiddenField hidCampaignCategoryCode = (HiddenField)row.FindControl("hidCampaignCategoryCode");
            HiddenField hidPrice = (HiddenField)row.FindControl("hidPrice");
            HiddenField hidAmount = (HiddenField)row.FindControl("hidAmount");
            HiddenField hidStartDate = (HiddenField)row.FindControl("hidStartDate");
            HiddenField hidEndDate = (HiddenField)row.FindControl("hidEndDate");
            HiddenField hidRemark = (HiddenField)row.FindControl("hidRemark");
            
            if (e.CommandName == "ShowVoucher")
            {
                hidIdList.Value = hidVoucherId.Value;
                txtVoucherName_Ins.Text = hidVoucherCode.Value;
                txtVoucherCode_Ins.Text = hidVoucherName.Value;
                ddlVoucherType_Ins.SelectedValue = (hidVoucherTypeCode.Value == null || hidVoucherTypeCode.Value == "") ? hidVoucherTypeCode.Value = "-99" : hidVoucherTypeCode.Value;
                ddlStatus_Ins.SelectedValue = (hidVoucherStatusCode.Value == null || hidVoucherStatusCode.Value == "") ? hidVoucherStatusCode.Value = "-99" : hidVoucherStatusCode.Value;
                ddlCampaignCategory_Ins.SelectedValue = (hidCampaignCategoryCode.Value == null || hidCampaignCategoryCode.Value == "") ? hidCampaignCategoryCode.Value = "-99" : hidCampaignCategoryCode.Value;
                txtPrice_Ins.Text = hidPrice.Value;
                txtAmount_Ins.Text = hidAmount.Value;
                txtStartDate_Ins.Text = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(hidStartDate.Value));
                txtEndDate_Ins.Text = string.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(hidEndDate.Value));
                hidFlagInsert.Value = "False";
                txtRemark_Ins.Text = hidRemark.Value;
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-driver').modal();", true);

            }

        }

        #endregion Events

        #region Binding
    

        protected void BindddlLookup(DropDownList ddl,string lookuptype)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = lookuptype;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddl.DataSource = lLookupInfo;
            ddl.DataTextField = "LookupValue";
            ddl.DataValueField = "LookupCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", ""));
        }
    
        protected void BindddlCampaignCategory(DropDownList ddl)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignCategoryInfo> lCampaignCategoryInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

            ddl.DataSource = lCampaignCategoryInfo;
            ddl.DataTextField = "CampaignCategoryName";
            ddl.DataValueField = "CampaignCategoryCode";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", ""));
        }

        #endregion Binding

        #region Paging
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


            LoadVoucher();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadVoucher();
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

        #endregion Paging

    }
}
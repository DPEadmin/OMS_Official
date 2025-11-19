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

namespace DOMS_TSR.src.Driver

{
    public partial class Driver : System.Web.UI.Page
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
                    APIUrl = empInfo.ConnectionAPI;
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                LoadDriver();

                BindddlTitleName();
             
            }
        }

        #region Function
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProductDetail.aspx?ProductCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void LoadDriver()
        {
            List<DriverInfo> lDriverInfo = new List<DriverInfo>();

            

            int? totalRow = CountDriverMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lDriverInfo = GetDriverMasterByCriteria();

            gvDriver.DataSource = lDriverInfo;

            gvDriver.DataBind();


            

        }

        protected void ClearSearch()
        {
            txtSearchDriverCode.Text = "";
            txtSearchDriverFName.Text = "";
            txtSearchDriverLName.Text = "";
        }

        protected void ClearInsertSection()
        {
            txtDriverNo_Ins.Text = "";
            ddlTitleName_Ins.SelectedValue = "-99";
            txtFName_Ins.Text = "";
            txtLName_Ins.Text = "";
       
            txtTel_Ins.Text = "";
        }

        public List<DriverInfo> GetDriverMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDriverByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Driver_No"] = txtSearchDriverCode.Text;

                data["FName"] = txtSearchDriverFName.Text;

                data["LName"] = txtSearchDriverLName.Text;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DriverInfo> lDriverInfo = JsonConvert.DeserializeObject<List<DriverInfo>>(respstr);


            return lDriverInfo;

        }

        public int? CountDriverMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountDriverListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Driver_No"] = txtSearchDriverCode.Text;

                data["FName"] = txtSearchDriverFName.Text;

                data["LName"] = txtSearchDriverLName.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (txtDriverNo_Ins.Text == "")
            {
                flag = false;
                txtDriverNo_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสคนขบรถ";
            }
            else
            {
                if (txtDriverNo_Ins.Text != "")
                {


                    Boolean isDuplicate = ValidateDuplicate();


                    if (isDuplicate)
                    {
                        flag = false;
                        lblDriverNo_Ins.Text = MessageConst._DATA_NComplete;

                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblDriverNo_Ins.Text = "";

                    }
                }
            }

            if (ddlTitleName_Ins.SelectedValue == "-99" || ddlTitleName_Ins.SelectedValue == "")
            {
                flag = false;
                lblTitleName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " คำนำหน้าชื่อ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTitleName_Ins.Text = "";
            }

            if (txtFName_Ins.Text == "")
            {
                flag = false;
                lblFName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblFName_Ins.Text = "";
            }    

            if (txtLName_Ins.Text == "")
            {
                flag = false;
                lblLName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " นามสกุล";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblLName_Ins.Text = "";
            }


            

            if (txtTel_Ins.Text == "")
            {
                flag = false;
                lblTel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรศัพท์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTel_Ins.Text = "";
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

            APIpath = APIUrl + "/api/support/DriverCheck";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Driver_No"] = txtDriverNo_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lDriverInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            if (lDriverInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        protected Boolean DeleteDriver()
        {

            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            for (int i = 0; i < gvDriver.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvDriver.Rows[i].FindControl("chkDriver");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvDriver.Rows[i].FindControl("hidDriverId");

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
           
                APIpath = APIUrl + "/api/support/DeleteDriver";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["strDriverId"] = Codelist;
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
            LoadDriver();
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
            isdelete = DeleteDriver();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

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

                        APIpath = APIUrl + "/api/support/InsertDriver";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["Driver_No"] = txtDriverNo_Ins.Text;
                            data["TitleCode"] = ddlTitleName_Ins.SelectedValue;
                            data["FName"] = txtFName_Ins.Text;
                            data["LName"] = txtLName_Ins.Text;
                          
                            data["Mobile"] = txtTel_Ins.Text;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadDriver();

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

                    APIpath = APIUrl + "/api/support/UpdateDriver";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["DriverId"] = hidIdList.Value;
                        data["Driver_No"] = txtDriverNo_Ins.Text;
                        data["TitleCode"] = ddlTitleName_Ins.SelectedValue;
                        data["FName"] = txtFName_Ins.Text;
                        data["LName"] = txtLName_Ins.Text;
                     
                        data["Mobile"] = txtTel_Ins.Text;
                        data["UpdateBy"] = empInfo.EmpCode;
                        data["FlagDelete"] = "N";

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        LoadDriver();

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

        protected void chkDriverAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvDriver.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvDriver.HeaderRow.FindControl("chkDriverAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvDriver.Rows[i].FindControl("hidDriverId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkDriver = (CheckBox)gvDriver.Rows[i].FindControl("chkDriver");

                    chkDriver.Checked = true;
                }
                else
                {

                    CheckBox chkDriver = (CheckBox)gvDriver.Rows[i].FindControl("chkDriver");

                    chkDriver.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void gvDriver_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvDriver.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidDriverId = (HiddenField)row.FindControl("hidDriverId");
            HiddenField hidDriverNo = (HiddenField)row.FindControl("hidDriverNo");
            HiddenField hidTitle = (HiddenField)row.FindControl("hidTitle");
            HiddenField hidFName = (HiddenField)row.FindControl("hidFName");
            HiddenField hidLName = (HiddenField)row.FindControl("hidLName");
            HiddenField hidGender = (HiddenField)row.FindControl("hidGender");
            HiddenField hidMobile = (HiddenField)row.FindControl("hidMobile");

            if (e.CommandName == "ShowProduct")
            {
                hidIdList.Value = hidDriverId.Value;
                txtDriverNo_Ins.Text = hidDriverNo.Value;
                ddlTitleName_Ins.SelectedValue = (hidTitle.Value == null || hidTitle.Value == "") ? hidTitle.Value = "-99" : hidTitle.Value;
                txtFName_Ins.Text = hidFName.Value;
                txtLName_Ins.Text = hidLName.Value;
           
                txtTel_Ins.Text = hidMobile.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-driver').modal();", true);

            }

        }

        #endregion Events

        #region Binding

        protected void BindddlTitleName()
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

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlTitleName_Ins.DataSource = lLookupInfo;
            ddlTitleName_Ins.DataTextField = "LookupValue";
            ddlTitleName_Ins.DataValueField = "LookupCode";
            ddlTitleName_Ins.DataBind();
            ddlTitleName_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlGender()
        {
            
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


            LoadDriver();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadDriver();
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
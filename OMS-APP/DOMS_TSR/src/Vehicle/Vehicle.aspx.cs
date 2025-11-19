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
using System.Text.RegularExpressions;

namespace DOMS_TSR.src.Vehicle
{
    public partial class Vehicle : System.Web.UI.Page
    {
        

        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

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
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                
                BindddlVehicleType();
                BindStatusActive();
               
                BindddlSearchVehicleType();
                LoadVehicle();
            }

            
        }

        #region Function

        protected void LoadVehicle()
        {
            List<VechicleInfo> lVehicleInfo = new List<VechicleInfo>();

            

            int? totalRow = CountVehicleMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lVehicleInfo = GetVehicleMasterByCriteria();

            gvVehicle.DataSource = lVehicleInfo;

            gvVehicle.DataBind();


            

        }

        public int? CountVehicleMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountVechicleListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Vechicle_No"] = txtSearchLicensePlate.Text;

                data["Vechicle_Band"] = TxtSearchVehicleBrand.Text.Trim();

                data["Vechicle_Lookup"] = ddlSearchVehicleType.SelectedValue == "-99" ? data["Vechicle_Lookup"] = "" : data["Vechicle_Lookup"] = ddlSearchVehicleType.SelectedValue;

                data["Active"] = ddlSearchVehicleType.SelectedValue == "-99" ? data["Active"] = "" : data["Active"] = ddlSearchlActive.SelectedValue;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<VechicleInfo> GetVehicleMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListVechicleByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Vechicle_No"] = txtSearchLicensePlate.Text;

                data["Vechicle_Band"] = TxtSearchVehicleBrand.Text.Trim();

                data["Vechicle_Lookup"] = ddlSearchVehicleType.SelectedValue == "-99" ? data["Vechicle_Lookup"] = "" : data["Vechicle_Lookup"] = ddlSearchVehicleType.SelectedValue;

                data["Vechicle_Active"] = ddlSearchlActive.SelectedValue == "-99" ? data["Vechicle_Active"] = "" : data["Vechicle_Active"] = ddlSearchlActive.SelectedValue;


                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<VechicleInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<VechicleInfo>>(respstr);

            return lVehicleInfo;

            #region test connection string

            
            #endregion

        }

        protected void ClearSearch()
        {
            txtSearchLicensePlate.Text = "";
            ddlSearchVehicleBrand.SelectedValue = "-99";
            TxtSearchVehicleBrand.Text = "";
            ddlSearchVehicleType.SelectedValue = "-99";
            ddlSearchlActive.SelectedValue = "-99";
        }

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (txtLicensePlate_Ins.Text == "")
            {
                flag = false;
                lblLicensePlate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " หมายเลขทะเบียนรถ";
            }
            else if (CheckSymbol(txtLicensePlate_Ins.Text) == true)
            {
                flag = false;
                lblLicensePlate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " หมายเลขทะเบียนรถต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                if (txtLicensePlate_Ins.Text != "")
                {


                    Boolean isDuplicate = ValidateDuplicate();


                    if (isDuplicate)
                    {
                        flag = false;
                        lblLicensePlate_Ins.Text = MessageConst._DATA_NComplete;

                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblLicensePlate_Ins.Text = "";

                    }
                }
            }
            if (txtModel_Ins.Text == "")
            {
                flag = false;
                LbModel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รุ่นรถ";
            }
            else if (CheckSymbol(txtModel_Ins.Text) == true)
            {
                flag = false;
                LbModel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รุ่นรถต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbModel_Ins.Text = "";
            }

            if (txtVehicleBrand_Ins.Text.Trim() == "" || txtVehicleBrand_Ins.Text.Trim() == null)
            {
                flag = false;
                lblVehicleBrand_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ยี่ห้อรถ";
            }
            else if (CheckSymbol(txtVehicleBrand_Ins.Text) == true)
            {
                flag = false;
                lblVehicleBrand_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ยี่ห้อรถต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblVehicleBrand_Ins.Text = "";
            }

            if (ddlVehicleType_Ins.SelectedValue == "-99" || ddlVehicleType_Ins.SelectedValue == "")
            {
                flag = false;
                lblVehicleType_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ประเภทรถ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblVehicleType_Ins.Text = "";
            }
            if (ddlActive_Ins.SelectedValue == "-99" || ddlActive_Ins.SelectedValue == "")
            {
                flag = false;
                lbActive_Ins.Text = MessageConst._MSG_PLEASEINSERT + " สถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbActive_Ins.Text = "";
            }

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-vehicle').modal();", true);
            }

            return flag;
        }
        protected Boolean validateUpdate()
        {
            Boolean flag = true;

            if (txtLicensePlate_Ins.Text == "")
            {
                flag = false;
                lblLicensePlate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " หมายเลขทะเบียนรถ";
            }
            else
            {
                if (txtLicensePlate_Ins.Text != "")
                {
                        flag = (flag == false) ? false : true;
                        lblLicensePlate_Ins.Text = "";
                }
            }
            if (txtModel_Ins.Text == "")
            {
                flag = false;
                LbModel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รุ่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbModel_Ins.Text = "";
            }

            if (txtVehicleBrand_Ins.Text.Trim() == "" || txtVehicleBrand_Ins.Text.Trim() == null)
            {
                flag = false;
                lblVehicleBrand_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ยี่ห้อ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblVehicleBrand_Ins.Text = "";
            }

            if (ddlVehicleType_Ins.SelectedValue == "-99" || ddlVehicleType_Ins.SelectedValue == "")
            {
                flag = false;
                lblVehicleType_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ประเภทรถ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblVehicleType_Ins.Text = "";
            }
            if (ddlActive_Ins.SelectedValue == "-99" || ddlActive_Ins.SelectedValue == "")
            {
                flag = false;
                lbActive_Ins.Text = MessageConst._MSG_PLEASEINSERT + " สถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbActive_Ins.Text = "";
            }

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-vehicle').modal();", true);
            }

            return flag;
        }

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/VechicleCheck";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Vechicle_No"] = txtLicensePlate_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<VechicleInfo> lVehibleInfo = JsonConvert.DeserializeObject<List<VechicleInfo>>(respstr);

            if (lVehibleInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        protected Boolean DeleteVehicle()
        {

            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            for (int i = 0; i < gvVehicle.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvVehicle.Rows[i].FindControl("chkVehicle");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvVehicle.Rows[i].FindControl("hidVehicleId");

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

                APIpath = APIUrl + "/api/support/DeleteVechicle";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Vechicle_No"] = Codelist;
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

        protected void ClearInsertSection()
        {
            txtLicensePlate_Ins.Text = "";
            txtVehicleBrand_Ins.Text= "";
            ddlVehicleType_Ins.SelectedValue = "-99";
            ddlActive_Ins.SelectedValue = "-99";
            txtModel_Ins.Text = "";
            lbActive_Ins.Text = "";
            LbModel_Ins.Text = "";
            lblVehicleBrand_Ins.Text = "";
            lblVehicleType_Ins.Text = "";
            lblLicensePlate_Ins.Text = "";
        }

        protected void BindStatusActive()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("---- กรุณาเลือก ----", "-99"));
            items.Add(new ListItem(StaticField.ActiveFlag_Y_NameValue_Active, StaticField.ActiveFlag_Y));
            items.Add(new ListItem(StaticField.ActiveFlag_N_NameValue_Inactive, StaticField.ActiveFlag_N));
            
            ddlSearchlActive.Items.AddRange(items.ToArray());
            ddlActive_Ins.Items.AddRange(items.ToArray());
        }
        #endregion Function

        #region Events

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                currentPageNumber = 1;
                LoadVehicle();
            }
            
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        protected void btnAddVehicle_Click(object sender, EventArgs e)
        {
            ClearInsertSection();
            hidFlagInsert.Value = "True";
            txtLicensePlate_Ins.Attributes.Remove("disabled");
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-vehicle').modal();", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteVehicle();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void chkVehicleAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvVehicle.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvVehicle.HeaderRow.FindControl("chkVehicleAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvVehicle.Rows[i].FindControl("hidVehicleId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkVehicle = (CheckBox)gvVehicle.Rows[i].FindControl("chkVehicle");

                    chkVehicle.Checked = true;
                }
                else
                {

                    CheckBox chkVehicle = (CheckBox)gvVehicle.Rows[i].FindControl("chkVehicle");

                    chkVehicle.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void gvVehicle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvVehicle.Rows[index];

            txtLicensePlate_Ins.Attributes.Add("disabled", "disabled");

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidVehicleId = (HiddenField)row.FindControl("hidVehicleId");
            HiddenField hidVehicle_No = (HiddenField)row.FindControl("hidVehicle_No");
            HiddenField hidVehicle_Band = (HiddenField)row.FindControl("hidVehicle_Band");
            HiddenField hidVehicle_Type = (HiddenField)row.FindControl("hidVehicle_Type");
            HiddenField HidVechicle_Model = (HiddenField)row.FindControl("HidVechicle_Model");
            HiddenField HidActive = (HiddenField)row.FindControl("HidActive");
            if (e.CommandName == "ShowVehicle")
            {
                ClearInsertSection();
                hidIdList.Value = hidVehicleId.Value;
                txtLicensePlate_Ins.Text = hidVehicle_No.Value;
                txtVehicleBrand_Ins.Text = (hidVehicle_Band.Value == null || hidVehicle_Band.Value == "") ? hidVehicle_Band.Value = "" : hidVehicle_Band.Value;
                ddlVehicleType_Ins.SelectedValue = (hidVehicle_Type.Value == null || hidVehicle_Type.Value == "") ? hidVehicle_Type.Value = "-99" : hidVehicle_Type.Value;
                txtModel_Ins.Text = HidVechicle_Model.Value;
                ddlActive_Ins.SelectedValue = (HidActive.Value == null || HidActive.Value == "") ? HidActive.Value = "-99" : HidActive.Value;


                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-vehicle').modal();", true);

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

                        APIpath = APIUrl + "/api/support/InsertVechicle";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["Vechicle_No"] = txtLicensePlate_Ins.Text;
                     
                            data["Vechicle_Band"] = txtVehicleBrand_Ins.Text.Trim(); ;
                            data["Vechicle_Lookup"] = ddlVehicleType_Ins.SelectedValue;
                            data["Vechicle_Model"] = txtModel_Ins.Text;
                            data["Active"] = ddlActive_Ins.SelectedValue;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadVehicle();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-vehicle').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }

                }
                else //Update
                {
                    if (validateUpdate())
                    {

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateVechicle";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["VechicleId"] = hidIdList.Value;
                            data["Vechicle_No"] = txtLicensePlate_Ins.Text;
                            data["Vechicle_Band"] = txtVehicleBrand_Ins.Text.Trim();
                            data["Vechicle_Lookup"] = ddlVehicleType_Ins.SelectedValue;
                            data["Vechicle_Model"] = txtModel_Ins.Text;
                            data["Active"] = ddlActive_Ins.SelectedValue;

                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadVehicle();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-vehicle').modal('hide');", true);



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
            ClearInsertSection();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-vehicle').modal('hide');", true);
        }

        #endregion Events

        #region Binding

        protected void BindddlSearchVehicleBrand()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_CAR_BAND;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchVehicleBrand.DataSource = lLookupInfo;
            ddlSearchVehicleBrand.DataTextField = "LookupValue";
            ddlSearchVehicleBrand.DataValueField = "LookupCode";
            ddlSearchVehicleBrand.DataBind();
            ddlSearchVehicleBrand.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchVehicleType()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_CAR_TYPE;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchVehicleType.DataSource = lLookupInfo;
            ddlSearchVehicleType.DataTextField = "LookupValue";
            ddlSearchVehicleType.DataValueField = "LookupCode";
            ddlSearchVehicleType.DataBind();
            ddlSearchVehicleType.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlVehicleBrand()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_CAR_BAND;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            
        }

        protected void BindddlVehicleType()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_CAR_TYPE;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlVehicleType_Ins.DataSource = lLookupInfo;
            ddlVehicleType_Ins.DataTextField = "LookupValue";
            ddlVehicleType_Ins.DataValueField = "LookupCode";
            ddlVehicleType_Ins.DataBind();
            ddlVehicleType_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        #endregion Binding

        #region Paging

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


            LoadVehicle();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadVehicle();
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

            if (regexItem.IsMatch(txtSearchLicensePlate.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchLicensePlate.Text = "";
            }
            else
            {
                flag = false;
                lblSearchLicensePlate.Text = MessageConst._MSG_PLEASEINSERT + " ทะเบียนรถต้องไม่มีอักขระพิเศษ";
            }
            if (regexItem.IsMatch(TxtSearchVehicleBrand.Text))
            {
                flag = (flag == false) ? false : true;
                lblSearchVehicleBrand.Text = "";
            }
            else
            {
                flag = false;
                lblSearchVehicleBrand.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อคลังสินค้าต้องไม่มีอักขระพิเศษ";
            }
            return flag;
        }
        #endregion Paging
    }
}
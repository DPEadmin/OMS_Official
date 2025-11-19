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

namespace DOMS_TSR.src.RoutingManagement
{
    public partial class RoutingDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string ProductImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];

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
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                BindddlProvince();
                BindddlDistrict();
                BindddlSubDistrict();

                LoadRouting();
                LoadRoutingDetail();

            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadRouting();
        }

        #region Function



        protected void LoadRouting()
        {
            List<RoutingDetailInfo> lRoutingInfo = new List<RoutingDetailInfo>();


            int? totalRow = CountRoutingMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lRoutingInfo = GetRoutingMasterByCriteria();

            gvRouting.DataSource = lRoutingInfo;

            gvRouting.DataBind();


            

        }

        protected void LoadRoutingDetail()
        {
            List<RoutingInfo> lRoutingInfo = new List<RoutingInfo>();

            lRoutingInfo = LoadRoutingDetailNoPaging();

            lblRoutingCode.Text = lRoutingInfo[0].Routing_code;

            lblRoutingName.Text = lRoutingInfo[0].Routing_name;

            

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


            LoadRouting();
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

        protected Boolean DeleteRoutingDetail()
        {

            for (int i = 0; i < gvRouting.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvRouting.Rows[i].FindControl("chkRouting");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRouting.Rows[i].FindControl("hidRoutingDetailId");

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

                APIpath = APIUrl + "/api/support/DeleteRoutingDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Routing_code"] = Codelist;


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

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (ddlProvince_Ins.SelectedValue == "" || ddlProvince_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblProvince_Ins.Text = MessageConst._MSG_PLEASEINSERT + " จังหวัด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProvince_Ins.Text = "";
            }

            if (ddlDistrict_Ins.SelectedValue == "" || ddlDistrict_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เขต/อำเภอ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDistrict_Ins.Text = "";
            }

            if (ddlSubDistrict_Ins.SelectedValue == "" || ddlSubDistrict_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblSubDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แขวง/ตำบล";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSubDistrict_Ins.Text = "";
            }


            if (txtPostCode_Ins.Text.Length != 5|| txtPostCode_Ins.Text !=null|| txtPostCode_Ins.Text!="")
            {   
                if (txtPostCode_Ins.Text != "")
                {
                    if (txtPostCode_Ins.Text.Length < 5)
                    {
                        lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขไม่ครบ 5 ditgit";
                        flag = false;
                    }
                    else
                    {
                        if (txtPostCode_Ins.Text.Length > 5)
                        {
                            lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขเกิน 5 ditgit";
                            flag = false;
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblPostCode_Ins.Text = "";
                        }
                    }
                   
                }
                else 
                {
                    lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขไม่ครบ 5 ditgit";
                    flag = false;
                }
              
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPostCode_Ins.Text = "";
            }



          

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
            }
            else
            {
                //Validate Duplicate

                Boolean isDuplicate = ValidateDuplicate();


                if (isDuplicate)
                {
                    flag = false;
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('พบข้อมูลซ้ำ !');", true);

                }
                else
                {
                    flag = (flag == false) ? false : true;

                }
            }

            return flag;
        }
        protected Boolean validateUpdate()
        {
            Boolean flag = true;

            if (ddlProvince_Ins.SelectedValue == "" || ddlProvince_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblProvince_Ins.Text = MessageConst._MSG_PLEASEINSERT + " จังหวัด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProvince_Ins.Text = "";
            }

            if (ddlDistrict_Ins.SelectedValue == "" || ddlDistrict_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เขต/อำเภอ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDistrict_Ins.Text = "";
            }

            if (ddlSubDistrict_Ins.SelectedValue == "" || ddlSubDistrict_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblSubDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แขวง/ตำบล";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSubDistrict_Ins.Text = "";
            }


            if (txtPostCode_Ins.Text.Length != 5 || txtPostCode_Ins.Text != null || txtPostCode_Ins.Text != "")
            {
                
                if (txtPostCode_Ins.Text != "")
                {
                    if (txtPostCode_Ins.Text.Length < 5)
                    {
                        lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขไม่ครบ 5 ditgit";
                        flag = false;
                    }
                    else
                    {
                        if (txtPostCode_Ins.Text.Length > 5)
                        {
                            lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขเกิน 5 ditgit";
                            flag = false;
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblPostCode_Ins.Text = "";
                        }
                    }
                 
                }
                else
                {
                    lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระบุเลขไม่ครบ 5 ditgit";
                    flag = false;
                }

            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPostCode_Ins.Text = "";
            }





            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
            }
            else
            {
                
            }

            return flag;
        }
        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDistrict();
            BindddlSubDistrict();
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlSubDistrict();
        }

        #endregion

        #region DAO Function

        public List<RoutingDetailInfo> GetRoutingMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingDetailByCriteria_showgv";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];

                data["ProvinceCode"] = txtSearchProvince.Text;

                data["DistrictCode"] = txtSearchDistrict.Text;

                data["SubDistrictCode"] = txtSearchSubDistrict.Text;

                data["PostCode"] = txtSearchPostCode.Text;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingDetailInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingDetailInfo>>(respstr);


            return lRoutingInfo;

        }

        public List<RoutingInfo> LoadRoutingDetailNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingInfo> lRoutingInfo = JsonConvert.DeserializeObject<List<RoutingInfo>>(respstr);


            return lRoutingInfo;

        }


        public List<ProvinceInfo> ListProvinceNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceId"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);


            return lProvinceInfo;

        }


        public List<DistrictInfo> ListDistrictNoPaging(string pCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = pCode;
             

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);


            return lDistrictInfo;

        }


        public List<SubDistrictInfo> ListSubDistrictNoPaging(string dCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DistrictCode"] = dCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);


            return lSubDistrictInfo;

        }



        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/RoutingDetailCheck";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = Request.QueryString["RoutingCode"];
                data["ProviceCode"] = ddlProvince_Ins.SelectedValue;
                data["DistrictCode"] = ddlDistrict_Ins.SelectedValue;
                data["SubDistrictCode"] = ddlSubDistrict_Ins.SelectedValue;
                data["PostCode"] = txtPostCode_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            if (lProductInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }


        public int? CountRoutingMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountRoutingListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Routing_code"] = "";

                data["Routing_name"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        #endregion

        #region Event 

        protected void gvRouting_Change(object sender, GridViewPageEventArgs e)
        {
            gvRouting.PageIndex = e.NewPageIndex;

            List<RoutingDetailInfo> lRoutingInfo = new List<RoutingDetailInfo>();

            lRoutingInfo = GetRoutingMasterByCriteria();

            gvRouting.DataSource = lRoutingInfo;

            gvRouting.DataBind();

        }

        protected void chkRoutingAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRouting.Rows.Count; i++)
            {
                CheckBox chkRouting = (CheckBox)gvRouting.Rows[i].FindControl("chkRouting");
                CheckBox chkall = (CheckBox)gvRouting.HeaderRow.FindControl("chkRoutingAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRouting.Rows[i].FindControl("hidRoutingDetailId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }


                    chkRouting.Checked = true;
                }
                else
                {

                    chkRouting.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            LoadRouting();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteRoutingDetail();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
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
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (validateInsert())
                    {

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertRoutingDetail";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["Routing_code"] = Request.QueryString["RoutingCode"];
                            data["ProvinceCode"] = ddlProvince_Ins.SelectedValue;
                            data["DistrictCode"] = ddlDistrict_Ins.SelectedValue;
                            data["SubDistrictCode"] = ddlSubDistrict_Ins.SelectedValue;
                            data["PostCode"] = txtPostCode_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadRouting();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal('hide');", true);



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

                        APIpath = APIUrl + "/api/support/UpdateRoutingDetail";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();
                            data["Routing_code"] = Request.QueryString["RoutingCode"];
                            data["RoutingDetailId"] = hidIdList.Value;
                            data["ProvinceCode"] = ddlProvince_Ins.SelectedValue;
                            data["DistrictCode"] = ddlDistrict_Ins.SelectedValue;
                            data["SubDistrictCode"] = ddlSubDistrict_Ins.SelectedValue;
                            data["PostCode"] = txtPostCode_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["UpdateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadRouting();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-product').modal('hide');", true);



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
            ddlDistrict_Ins.ClearSelection();
            ddlSubDistrict_Ins.ClearSelection();
            ddlProvince_Ins.ClearSelection();
            txtPostCode_Ins.Text = "";
            lblSubDistrict_Ins.Text = "";
            lblDistrict_Ins.Text = "";
            lblProvince_Ins.Text = "";
            lblPostCode_Ins.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal('hide');", true);
            

        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchDistrict.Text = "";
            txtSearchSubDistrict.Text = "";
            txtSearchProvince.Text = "";
            txtSearchPostCode.Text = "";


        }

        protected void gvRouting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvRouting.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidRoutingDetailId = (HiddenField)row.FindControl("hidRoutingDetailId");
            HiddenField hidProvinceCode = (HiddenField)row.FindControl("hidProvinceCode");
            HiddenField hidDistrictCode = (HiddenField)row.FindControl("hidDistrictCode");
            HiddenField hidSubDistrictCode = (HiddenField)row.FindControl("hidSubDistrictCode");
            HiddenField hidPostCode = (HiddenField)row.FindControl("hidPostCode");


            if (e.CommandName == "ShowRouting")
            {
                BindddlProvince();
                ddlProvince_Ins.SelectedValue = hidProvinceCode.Value;
                BindddlDistrict();
                ddlDistrict_Ins.SelectedValue = hidDistrictCode.Value;
                BindddlSubDistrict();
                ddlSubDistrict_Ins.SelectedValue = hidSubDistrictCode.Value;
                txtPostCode_Ins.Text = hidPostCode.Value;

                lblProvince_Ins.Text = "";
                lblDistrict_Ins.Text = "";
                lblSubDistrict_Ins.Text = "";
                lblPostCode_Ins.Text = "";

                hidIdList.Value = hidRoutingDetailId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);

            }

        }

        protected void btnAddRouting_Click(object sender, EventArgs e)
        {
 
            hidFlagInsert.Value = "True";

            ddlProvince_Ins.ClearSelection();
            ddlDistrict_Ins.ClearSelection();
            ddlSubDistrict_Ins.ClearSelection();
            txtPostCode_Ins.Text = "";

            lblProvince_Ins.Text = "";
            lblDistrict_Ins.Text = "";
            lblSubDistrict_Ins.Text = "";
            lblPostCode_Ins.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"RoutingDetail.aspx?RoutingCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }


        protected void BindddlProvince()
        {
            List<ProvinceInfo> lProvinceInfo = new List<ProvinceInfo>();

            ProvinceInfo pinfo = new ProvinceInfo();

            lProvinceInfo = ListProvinceNoPaging();

            ddlProvince_Ins.DataSource = lProvinceInfo;
            ddlProvince_Ins.DataValueField = "ProvinceCode";
            ddlProvince_Ins.DataTextField = "ProvinceName";
            ddlProvince_Ins.DataBind();


            ddlProvince_Ins.Items.Insert(0, new ListItem("กรุณาเลือก", "-99"));


        }

        protected void BindddlDistrict()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = ddlProvince_Ins.SelectedValue;
                data["DistrictCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);


            ddlDistrict_Ins.DataSource = lDistrictInfo;

            ddlDistrict_Ins.DataTextField = "DistrictName";

            ddlDistrict_Ins.DataValueField = "DistrictCode";

            ddlDistrict_Ins.DataBind();

            ddlDistrict_Ins.Items.Insert(0, new ListItem("กรุณาเลือก", "-99"));

        }

        protected void BindddlSubDistrict()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DistrictCode"] = ddlDistrict_Ins.SelectedValue;
                data["SubDistrictCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);


            ddlSubDistrict_Ins.DataSource = lSubDistrictInfo;

            ddlSubDistrict_Ins.DataTextField = "SubDistrictName";

            ddlSubDistrict_Ins.DataValueField = "SubDistrictCode";

            ddlSubDistrict_Ins.DataBind();

            ddlSubDistrict_Ins.Items.Insert(0, new ListItem("กรุณาเลือก", "-99"));

        }
        #endregion


    }
}
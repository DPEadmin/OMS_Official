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
using System.IO;

namespace DOMS_TSR.src.Promotion
{
    public partial class Comboset : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];

        string Codelist = "";
        string CombosetIdlist = "";
        string EditFlag = "";
        Boolean isdelete;
        string RedeemFlag = "";
        string ComplementaryFlag = "";
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


                loadComboset();

                BindddlPromotionIns();
                BindddlPromotionSearch();
                Activestatus();

            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadComboset();
        }

        #region Function



        protected void loadComboset()
        {
            List<CombosetInfo> lPromotionInfo = new List<CombosetInfo>();

            

            int? totalRow = CountCombosetMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lPromotionInfo = GetCombosetMasterByCriteria();

            gvComboset.DataSource = lPromotionInfo;

            gvComboset.DataBind();


            

        }

        public List<CombosetInfo> GetCombosetMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCombosetByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = txtSearchCombosetCode.Text;

                data["CombosetName"] = txtSearchCombosetName.Text;

                data["PromotionCode"] = ddlSearchPromotion.SelectedValue;
                data["StartDate"] = txtSearchStartDateFrom.Text;
                data["EndDate"] = txtSearchEndDateFrom.Text;
                data["FlagActive"] = ddlActive_Status.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CombosetInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<CombosetInfo>>(respstr);


            return lPromotionInfo;

        }

        public int? CountCombosetMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCombosetByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetCode"] = txtSearchCombosetCode.Text;

                data["CombosetName"] = txtSearchCombosetName.Text;

                data["PromotionCode"] = ddlSearchPromotion.SelectedValue;

                data["StartDate"] = txtSearchStartDateFrom.Text;
                data["EndDate"] = txtSearchEndDateFrom.Text;
                data["FlagActive"] = ddlActive_Status.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


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


            loadComboset();
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

        protected Boolean DeletePromotion()
        {

            for (int i = 0; i < gvComboset.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvComboset.Rows[i].FindControl("chkComboset");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvComboset.Rows[i].FindControl("hidCombosetCode");
                    HiddenField hidCombosetId = (HiddenField)gvComboset.Rows[i].FindControl("hidCombosetId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                        CombosetIdlist += ",'" + hidCombosetId.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                        CombosetIdlist += "'" + hidCombosetId.Value + "'";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteCombosetDetailInfoByIdString";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Codelist;
                    data["SubMainIdDelete"] = Codelist;
                    

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);

                int? cou2 = 0;
                int? cou3 = 0;
                string respstr2 = "";

                string APIpath2 = APIUrl + "/api/support/DeleteMainPromotionDetailByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["SubMainIdDelete"] = CombosetIdlist;


                    var response2 = wb.UploadValues(APIpath2, "POST", data);

                    respstr2 = Encoding.UTF8.GetString(response2);
                }
                cou2 = JsonConvert.DeserializeObject<int?>(respstr2);

                string respstr3 = "";

                string APIpath3 = APIUrl + "/api/support/DeleteSubExchangePromotionDetailByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["SubMainIdDelete"] = CombosetIdlist;


                    var response3 = wb.UploadValues(APIpath2, "POST", data);

                    respstr3 = Encoding.UTF8.GetString(response3);
                }
                cou3 = JsonConvert.DeserializeObject<int?>(respstr3);


            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;
            loadComboset();
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

        public string GetPromotionBrand(string pCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = pCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lPromotionInfo.Count > 0 ? lPromotionInfo[0].ProductBrandCode : "";

        }


        #endregion

        #region Event 

        protected void gvPromotion_Change(object sender, GridViewPageEventArgs e)
        {
            gvComboset.PageIndex = e.NewPageIndex;

            List<CombosetInfo> lPromotionInfo = new List<CombosetInfo>();

            lPromotionInfo = GetCombosetMasterByCriteria();

            gvComboset.DataSource = lPromotionInfo;

            gvComboset.DataBind();

        }
        protected void gvComboset_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hidCombosetId = (HiddenField)e.Row.FindControl("hidCombosetId");
                LinkButton lCombosetCode = (LinkButton)e.Row.FindControl("lCombosetCode");
                HiddenField hidCombosetCode = (HiddenField)e.Row.FindControl("hidCombosetCode");
                lCombosetCode.PostBackUrl = "CombosetDetail.aspx?CombosetCode="+ hidCombosetCode.Value+ "&CombosetId=" + hidCombosetId.Value + "";
          

            }
        }
        protected void chkPromotionAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvComboset.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvComboset.HeaderRow.FindControl("chkPromotionAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvComboset.Rows[i].FindControl("hidPromotionId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkPromotion = (CheckBox)gvComboset.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkPromotion = (CheckBox)gvComboset.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadComboset();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeletePromotion();

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
                   

                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertPromotionDetailCombo";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CombosetCode"] = txtCombosetCode_Ins.Text;
                        
                        data["PromotionDetailName"] = txtCombosetName_Ins.Text;
                        
                        data["EndDatePromotionCombo"] = txtEndDate_INS.Text;
                        data["StartDatePromotionCombo"] = txtStartDate_INS.Text;
                        data["Price"] = txtCombosetPrice_Ins.Text;
                        data["FlagDelete"] = "N";
                        data["FlagActive"] = ddlActive_Status_INS.SelectedValue; 


                        data["CreateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        loadComboset();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Comboset').modal('hide');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }

                }
                else //Update
                {
                  

                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdatePromotionDetailCombo";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        
                        data["PromotionDeailInfoId"] = hidIdList.Value;

                        data["CombosetCode"] = txtCombosetCode_Ins.Text;
                        
                        data["PromotionDetailName"] = txtCombosetName_Ins.Text;
                        

                        data["EndDatePromotionCombo"] = txtEndDate_INS.Text;
                        data["StartDatePromotionCombo"] = txtStartDate_INS.Text;
                        data["FlagActive"] = ddlActive_Status_INS.SelectedValue;
                        data["Price"] = txtCombosetPrice_Ins.Text;

                        data["FlagDelete"] = "N";


                        data["UpdateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        loadComboset();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Comboset').modal('hide');", true);



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
            txtCombosetCode_Ins.Text = "";
            txtCombosetName_Ins.Text = "";
            txtEndDate_INS.Text = "";
            txtStartDate_INS.Text = "";
            txtCombosetPrice_Ins.Text = "";
            ddlActive_Status_INS.ClearSelection();
            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Comboset').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchCombosetCode.Text = "";
            txtSearchCombosetName.Text = "";
            ddlSearchPromotion.SelectedValue = "-99";
        }

        protected void gvComboset_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvComboset.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCombosetId = (HiddenField)row.FindControl("hidCombosetId");
            HiddenField hidPromotionCode = (HiddenField)row.FindControl("hidPromotionCode");
            HiddenField hidCombosetCode = (HiddenField)row.FindControl("hidCombosetCode");
            HiddenField hidCombosetName = (HiddenField)row.FindControl("hidCombosetName");
            HiddenField hidCombosetPrice = (HiddenField)row.FindControl("hidCombosetPrice");

            HiddenField HidStartDatePromotionCombo = (HiddenField)row.FindControl("HidStartDatePromotionCombo");
            HiddenField HidEndDatePromotionCombo = (HiddenField)row.FindControl("HidEndDatePromotionCombo");
            HiddenField HidFlagActive = (HiddenField)row.FindControl("HidFlagActive");


            if (e.CommandName == "ShowComboset")
            {
                txtCombosetCode_Ins.Text = hidCombosetCode.Value;
                hidPromotionCode_Ins.Value = hidPromotionCode.Value;
                txtCombosetName_Ins.Text = hidCombosetName.Value;
                txtCombosetPrice_Ins.Text = hidCombosetPrice.Value;
                txtStartDate_INS.Text = HidStartDatePromotionCombo.Value;
                txtEndDate_INS.Text = HidEndDatePromotionCombo.Value;
                ddlActive_Status_INS.SelectedValue = HidFlagActive.Value;
                

                hidIdList.Value = hidCombosetId.Value;
                hidFlagInsert.Value = "False";

                txtCombosetCode_Ins.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Comboset').modal();", true);

            }

        }

        protected void btnAddComboset_Click(object sender, EventArgs e)
        {
            txtCombosetCode_Ins.Enabled = true;
            txtCombosetCode_Ins.Text = "";
            txtCombosetName_Ins.Text = "";
            txtStartDate_INS.Text = "";
            txtEndDate_INS.Text = "";
            txtCombosetPrice_Ins.Text = "";
            ddlActive_Status_INS.SelectedValue = "-99";

            hidFlagInsert.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Comboset').modal();", true);
           
        }

       

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"CombosetDetail.aspx?CombosetCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindddlPromotionIns()
        {
            

        }

        protected void BindddlPromotionSearch()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CombosetFlag"] = "Y";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lLookupInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            ddlSearchPromotion.DataSource = lLookupInfo;

            ddlSearchPromotion.DataTextField = "PromotionName";

            ddlSearchPromotion.DataValueField = "PromotionCode";

            ddlSearchPromotion.DataBind();

            ddlSearchPromotion.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void Activestatus()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = StaticField.LookupType_ACTIVESTATUS; 
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lMaritalStatusInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlActive_Status_INS.DataSource = lMaritalStatusInfo;
            ddlActive_Status_INS.DataTextField = "LookupValue";
            ddlActive_Status_INS.DataValueField = "LookupCode";
            ddlActive_Status_INS.DataBind();
            ddlActive_Status_INS.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));

            ddlActive_Status.DataSource = lMaritalStatusInfo;
            ddlActive_Status.DataTextField = "LookupValue";
            ddlActive_Status.DataValueField = "LookupCode";
            ddlActive_Status.DataBind();
            ddlActive_Status.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));
        }
        #endregion
    }
}
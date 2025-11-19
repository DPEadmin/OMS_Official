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

namespace DOMS_TSR.src.Campaign
{
    public partial class CampaignPromotion : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static int currentPageNumber;
        protected static int currentPromotionPageNumber;
        protected static int currentPromotionDetailPageNumber;
        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;
        public object CampaignImg { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPromotionPageNumber = 1;
                currentPromotionDetailPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                empInfo = (EmpInfo)Session["EmpInfo"];
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCodeSess.Value = merchantinfo.MerchantCode;
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                BindDdlPromotionLevel();

                loadCampaignInformation();
                loadCampaignImage();
                loadCampaignPromotion();
                loadPromotion();
            }
        }
        #region Function

        protected void loadCampaignInformation()
        {
            CampaignInfo cInfo = new CampaignInfo();
            List<CampaignInfo> lcInfo = new List<CampaignInfo>();
            lcInfo = GetCampaignMasterNoPagingByCriteria();

            if (lcInfo.Count > 0)
            {lblbrand.Text= lcInfo[0].CampaignCategoryName;
                lblCampaignCode.Text = lcInfo[0].CampaignCode;
                lblCampaignName.Text = lcInfo[0].CampaignName;
                
                if (lcInfo[0].Active == "Y")
                {
                    lblActive.Text = "Active";
                }
                else
                {
                    lblActive.Text = "Inactive";
                }
                if (lcInfo[0].FlagComboSet == "N")
                {
                    lblCampaignFormat.Text = lcInfo[0].FlagShowProductPromotion;
                }
                else
                {
                    lblCampaignFormat.Text = "COMBO SET";
                }
                var s = lcInfo[0].StartDate;
                var n = lcInfo[0].NotifyDate;
                var p = lcInfo[0].ExpireDate;
                var startdate = s.IndexOf(" ") > -1 ? s.Substring(0, s.IndexOf(" ")) : s;
                var notifydate = n.IndexOf(" ") > -1 ? n.Substring(0, n.IndexOf(" ")) : n;
                var expiredate = p.IndexOf(" ") > -1 ? p.Substring(0, p.IndexOf(" ")) : p;

                lblFlagComboset.Text = lcInfo[0].FlagComboSet;
                camptype.Text =  lcInfo[0].CampaignType;
             
                
            }
        }
        protected List<CampaignInfo> GetCampaignMasterNoPagingByCriteria()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = Request.QueryString["CampaignCode"];

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<CampaignInfo> lProductInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

            return lProductInfo;
        }
        protected void loadCampaignImage()
        {
            CampaignInfo cInfo = new CampaignInfo();
            List<CampaignInfo> lcInfo = new List<CampaignInfo>();
            lcInfo = GetCampaignImage();

            CampaignPicIm.Src = lcInfo.Count > 0 ? APIUrl + lcInfo[0].PictureCampaignUrl : "";
        }
        protected List<CampaignInfo> GetCampaignImage()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = Request.QueryString["CampaignCode"];

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<CampaignInfo> lProductInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

            return lProductInfo;
        }
        protected void loadCampaignPromotion()
        {
            PromotionInfo pInfo = new PromotionInfo();
            List<PromotionInfo> lpInfo = new List<PromotionInfo>();
            int? totalRow = CountCampaignPromotionMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lpInfo = GetCampaignPromotionMasterByCriteria();
            gvCampaignPromotion.DataSource = lpInfo;
            gvCampaignPromotion.DataBind();
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
            loadCampaignPromotion();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadCampaignPromotion();
        }
        protected void ddlPromoPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPromotionPageNumber = Int32.Parse(ddlPromoPage.SelectedValue);
            loadPromotion();
        }
        protected void GetPromoPageIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "PromoFirst":
                    currentPromotionPageNumber = 1;
                    break;

                case "PromoPrevious":
                    currentPromotionPageNumber = Int32.Parse(ddlPromoPage.SelectedValue) - 1;
                    break;

                case "PromoNext":
                    currentPromotionPageNumber = Int32.Parse(ddlPromoPage.SelectedValue) + 1;
                    break;

                case "PromoLast":
                    currentPromotionPageNumber = Int32.Parse(lblPromoTotalPages.Text);
                    break;
            }
            loadPromotion();
        }

        protected int? CountCampaignPromotionMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountCampaignPromotionListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = lblCampaignCode.Text;
                data["PromotionCode"] = txtSearchCampaignPromotionCode.Text;
                data["PromotionName"] = txtSearchCampaignPromotionName.Text;
                data["PromotionLevel"] = ddlSearchPromotionLevel.SelectedValue;
                data["StartDate"] = txtPromotionStartDate.Text;
                data["EndDate"] = txtSearchPromotionEndDate.Text;
                data["FlagComboset"] = lblFlagComboset.Text;
                data["CampaignType"] = camptype.Text;
                data["MerchantMapCode"] = hidMerchantCodeSess.Value;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        protected List<PromotionInfo> GetCampaignPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignPromotionByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = lblCampaignCode.Text;
                data["PromotionCode"] = txtSearchCampaignPromotionCode.Text;
                data["PromotionName"] = txtSearchCampaignPromotionName.Text;
                data["PromotionLevel"] = ddlSearchPromotionLevel.SelectedValue;
                data["StartDate"] = txtPromotionStartDate.Text;
                data["EndDate"] = txtSearchPromotionEndDate.Text;
                data["FlagComboset"] = lblFlagComboset.Text;
                data["CampaignType"] = camptype.Text;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantMapCode"] = hidMerchantCodeSess.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lCampaignPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lCampaignPromotionInfo;
        }
        protected int InsertCampaignPromotion(String pCode)
        {
            int sum0 = 0;
            string respstring = "";
            string APIpath1 = APIUrl + "/api/support/InsertCampaignPromotion";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = lblCampaignCode.Text;
                data["PromotionCode"] = pCode;
                data["CreateBy"] = hidEmpCode.Value;
                data["UpdateBy"] = hidEmpCode.Value;
                data["Active"] = "Y";

                var response = wb.UploadValues(APIpath1, "POST", data);

                respstring = Encoding.UTF8.GetString(response);
            }
            int? sum = JsonConvert.DeserializeObject<int?>(respstring);
            if (sum > 0)
            {
                sum0 = 1;
            }
            else
            {
                sum0 = 0;
            }
            return sum0;
        }
        protected Boolean DeleteCampaignPromotion()
        {
            for (int i = 0; i < gvCampaignPromotion.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvCampaignPromotion.Rows[i].FindControl("chkCampaignPromotion");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCampaignPromotion.Rows[i].FindControl("hidCampaignPromotionId");

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

                APIpath = APIUrl + "/api/support/DeleteCampaignPromotion";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCode"] = Codelist;


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
        protected void loadPromotion()
        {
            List<PromotionInfo> lpromotionInfo = new List<PromotionInfo>();
            int? totalRow = CountPromotioninCampaignList();
            SetPromotionPageBar(Convert.ToDouble(totalRow));
            lpromotionInfo = GetPromotioninCampaignByCriteria();
            gvAddPromotion.DataSource = lpromotionInfo;
            gvAddPromotion.DataBind();
        }
        protected void SetPromotionPageBar(double totalRow)
        {
            lblPromoTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPromoPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblPromoTotalPages.Text) + 1; i++)
            {
                ddlPromoPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlPromoPage, currentPromotionPageNumber.ToString());
            

            
            if ((currentPromotionPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                linkbtnPromoFirst.Enabled = false;
                linkbtnPromoPre.Enabled = false;
                linkbtnPromoNext.Enabled = true;
                linkbtnPromoLast.Enabled = true;
            }
            else if ((currentPromotionPageNumber.ToString() == lblPromoTotalPages.Text) && (currentPromotionPageNumber == 1))
            {
                linkbtnPromoFirst.Enabled = false;
                linkbtnPromoPre.Enabled = false;
                linkbtnPromoNext.Enabled = false;
                linkbtnPromoLast.Enabled = false;
            }
            else if ((currentPromotionPageNumber.ToString() == lblPromoTotalPages.Text) && (currentPromotionPageNumber > 1))
            {
                linkbtnPromoFirst.Enabled = true;
                linkbtnPromoPre.Enabled = true;
                linkbtnPromoNext.Enabled = false;
                linkbtnPromoLast.Enabled = false;
            }
            else
            {
                linkbtnPromoFirst.Enabled = true;
                linkbtnPromoPre.Enabled = true;
                linkbtnPromoNext.Enabled = true;
                linkbtnPromoLast.Enabled = true;
            }
            
        }
        protected int? CountPromotioninCampaignList()
        {
         

            string respstr = "";
            APIpath = APIUrl + "/api/support/CountPromotionInCampaignListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = lblCampaignCode.Text;
                data["PromotionCode"] = txtPromotionCode_Search.Text;
                data["PromotionName"] = txtPromotionName_Search.Text;
                data["PromotionLevel"] = ddlSearchPromotionLevelgvAdd.SelectedValue;
                data["StartDate"] = txtSearchPromotionStartDategvAdd.Text;
                data["EndDate"] = txtSearchPromotionEndDategvAdd.Text;
                data["CombosetFlag"] = lblFlagComboset.Text;
                data["MerchantMapCode"] = hidMerchantCodeSess.Value;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        protected List<PromotionInfo> GetPromotioninCampaignByCriteria()
        {

           



            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionInCampaign";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = lblCampaignCode.Text;
                data["PromotionCode"] = txtPromotionCode_Search.Text;
                data["PromotionName"] = txtPromotionName_Search.Text;
                data["PromotionLevel"] = ddlSearchPromotionLevelgvAdd.SelectedValue;
                data["StartDate"] = txtSearchPromotionStartDategvAdd.Text;
                data["EndDate"] = txtSearchPromotionEndDategvAdd.Text;
                data["rowOFFSet"] = ((currentPromotionPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();
                data["CombosetFlag"] = lblFlagComboset.Text;
                data["CampaignType"] = camptype.Text;
                data["MerchantMapCode"] = hidMerchantCodeSess.Value;
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lPromotionInfo;
        }
        protected void gvCampaignPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvCampaignPromotion.Rows[index];

            HiddenField hidCampaignPromotionId = (HiddenField)row.FindControl("hidCampaignPromotionId");
            HiddenField hidPromotionCode = (HiddenField)row.FindControl("hidPromotionCode");

            String promotionCode = hidPromotionCode.Value;
            loadPromotionDetail(promotionCode);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-promotiondetail').modal();", true);
        }
        protected void BindDdlPromotionLevel()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = "PROMOTIONLEVEL";
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lPromotionLevelInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchPromotionLevel.DataSource = lPromotionLevelInfo;
            ddlSearchPromotionLevel.DataTextField = "LookupValue";
            ddlSearchPromotionLevel.DataValueField = "LookupCode";
            ddlSearchPromotionLevel.DataBind();
            ddlSearchPromotionLevel.Items.Insert(0, new ListItem("----------Please select promotion level----------", "-99"));

            ddlSearchPromotionLevelgvAdd.DataSource = lPromotionLevelInfo;
            ddlSearchPromotionLevelgvAdd.DataTextField = "LookupValue";
            ddlSearchPromotionLevelgvAdd.DataValueField = "LookupCode";
            ddlSearchPromotionLevelgvAdd.DataBind();
            ddlSearchPromotionLevelgvAdd.Items.Insert(0, new ListItem("----------Please select promotion level----------", "-99"));
        }

        #endregion

        #region Event

        protected void btnSearchCampaignPromotion_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadCampaignPromotion();
        }
        protected void btnClearSearchCampaignPromotion_Click(object sender, EventArgs e)
        {
            txtSearchCampaignPromotionCode.Text = "";
            txtSearchCampaignPromotionName.Text = "";
            txtPromotionStartDate.Text = "";
            txtSearchPromotionEndDate.Text = "";
            ddlSearchPromotionLevel.ClearSelection();
        }
        protected void btnAddPromotion_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-campaignpromotion').modal();", true);
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteCampaignPromotion();

            btnSearchCampaignPromotion_Click(null, null);
            loadPromotion();

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select order to delete');", true);
            }
        }
        protected void btnSearchAddPromotion_Click(object sender, EventArgs e)
        {
            loadPromotion();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-campaignpromotion').modal();", true);
        }
        protected void btnClearSearchAddPromotion_Click(object sender, EventArgs e)
        {
            txtPromotionCode_Search.Text = "";
            txtPromotionName_Search.Text = "";
            ddlSearchPromotionLevelgvAdd.SelectedValue = "-99";
            txtSearchPromotionStartDategvAdd.Text = "";
            txtSearchPromotionEndDategvAdd.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-campaignpromotion').modal();", true);
        }
        protected void chkCampaignPromotionAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvCampaignPromotion.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvCampaignPromotion.HeaderRow.FindControl("chkCampaignPromotionAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCampaignPromotion.Rows[i].FindControl("hidCampaignPromotionId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkCampaignPromotion = (CheckBox)gvCampaignPromotion.Rows[i].FindControl("chkCampaignPromotion");

                    chkCampaignPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkCampaignPromotion = (CheckBox)gvCampaignPromotion.Rows[i].FindControl("chkCampaignPromotion");

                    chkCampaignPromotion.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected void chkPromotionAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvAddPromotion.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvAddPromotion.HeaderRow.FindControl("chkPromotionAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvAddPromotion.Rows[i].FindControl("hidPromotionId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkPromotion = (CheckBox)gvAddPromotion.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkPromotion = (CheckBox)gvAddPromotion.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected void InsertCampaignPromotionfromSelect_Click(object sender, EventArgs e)
        {
            int sum = 0;
            for (int i = 0; i < gvAddPromotion.Rows.Count; i++)
            {
                CheckBox chkPromotion = (CheckBox)gvAddPromotion.Rows[i].FindControl("chkPromotion");

                if (chkPromotion.Checked == true)
                {
                    HiddenField hidPromotionId = (HiddenField)gvAddPromotion.Rows[i].FindControl("hidPromotionId");
                    HiddenField hidPromotionCode = (HiddenField)gvAddPromotion.Rows[i].FindControl("hidPromotionCode");

                    sum = InsertCampaignPromotion(hidPromotionCode.Value);
                }
            }
            if (sum > 0)
            {
                loadCampaignPromotion();
                loadPromotion();
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-campaignpromotion').modal('hide');", true);
            }
        }
        protected void InsertCampaignPromotionfromProductSelect_Click(object sender, EventArgs e)
        {

        }
        protected void loadPromotionDetail(String promotionCode)
        {
            List<PromotionDetailInfo> lpromotionInfo = new List<PromotionDetailInfo>();
            int? totalRow = CountPromotioninDetail(promotionCode);
            SetPromotionDetailPageBar(Convert.ToDouble(totalRow));
            lpromotionInfo = GetPromotionDetailByCriteria(promotionCode);
            gvPromotionDetail.DataSource = lpromotionInfo;
            gvPromotionDetail.DataBind();
        }
        protected void SetPromotionDetailPageBar(double totalRow)
        {
            lblDetailTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlDetailPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblDetailTotalPages.Text) + 1; i++)
            {
                ddlDetailPage.Items.Add(new ListItem(i.ToString()));
            }
            setDetailDDl(ddlDetailPage, currentPromotionDetailPageNumber.ToString());
            

            
            if ((currentPromotionDetailPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                linkbtnDetailFirst.Enabled = false;
                linkbtnDetailPrevious.Enabled = false;
                linkbtnDetailNext.Enabled = true;
                linkbtnDetailLast.Enabled = true;
            }
            else if ((currentPromotionDetailPageNumber.ToString() == lblDetailTotalPages.Text) && (currentPromotionDetailPageNumber == 1))
            {
                linkbtnDetailFirst.Enabled = false;
                linkbtnDetailPrevious.Enabled = false;
                linkbtnDetailNext.Enabled = false;
                linkbtnDetailLast.Enabled = false;
            }
            else if ((currentPromotionDetailPageNumber.ToString() == lblDetailTotalPages.Text) && (currentPromotionDetailPageNumber > 1))
            {
                linkbtnDetailFirst.Enabled = true;
                linkbtnDetailPrevious.Enabled = true;
                linkbtnDetailNext.Enabled = false;
                linkbtnDetailLast.Enabled = false;
            }
            else
            {
                linkbtnDetailFirst.Enabled = true;
                linkbtnDetailPrevious.Enabled = true;
                linkbtnDetailNext.Enabled = true;
                linkbtnDetailLast.Enabled = true;
            }
            
        }
        protected void ddlDetailPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPromotionDetailPageNumber = Int32.Parse(ddlDetailPage.SelectedValue);
            loadPromotion();
        }
        private void setDetailDDl(DropDownList ddls, String val)
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
        protected void GetDetailPageIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "DetailFirst":
                    currentPromotionDetailPageNumber = 1;
                    break;

                case "DetailPrevious":
                    currentPromotionDetailPageNumber = Int32.Parse(ddlDetailPage.SelectedValue) - 1;
                    break;

                case "DetailNext":
                    currentPromotionDetailPageNumber = Int32.Parse(ddlDetailPage.SelectedValue) + 1;
                    break;

                case "DetailLast":
                    currentPromotionDetailPageNumber = Int32.Parse(lblDetailTotalPages.Text);
                    break;
            }
        }
            protected int? CountPromotioninDetail(String pCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountPromotionDetailListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["PromotionCode"] = pCode;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        protected List<PromotionDetailInfo> GetPromotionDetailByCriteria(String pCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProducttionDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = pCode;
                data["rowOFFSet"] = ((currentPromotionDetailPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotionDetailInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);


            return lPromotionDetailInfo;
        }
        protected string btnBackLink_Click(object sender, EventArgs e)
        {
            string a = "<a href=\"Campaign.aspx" + "</a>";
            return a;
        }
        protected void gvAddPromotionSelect_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvAddPromotion.Rows[index];
            int? sum = 0;

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidPromotionId = (HiddenField)row.FindControl("hidPromotionId");
            HiddenField hidPromotionCode = (HiddenField)row.FindControl("hidPromotionCode");

            if (e.CommandName == "AddPromotion")
            {
                hidFlagInsert.Value = "True";
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertCampaignPromotion";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCode"] = lblCampaignCode.Text;
                    data["PromotionCode"] = hidPromotionCode.Value;
                    data["CreateBy"] = hidEmpCode.Value;
                    data["UpdateBy"] = hidEmpCode.Value;
                    data["Active"] = "Y";

                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }

                sum = JsonConvert.DeserializeObject<int?>(respstr);
                if (sum > 0)
                {
                    loadCampaignPromotion();
                    loadPromotion();
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-campaignpromotion').modal('hide');", true);
                }
            }
        }

        #endregion

        protected void gvPromotionDetail_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (lblFlagComboset.Text == "Y")
            {
                gvPromotionDetail.Columns[0].Visible = true;
                gvPromotionDetail.Columns[1].Visible = true;
                gvPromotionDetail.Columns[2].Visible = false;
                gvPromotionDetail.Columns[3].Visible = false;
                gvPromotionDetail.Columns[5].Visible = false;
                gvPromotionDetail.Columns[7].Visible = false;

                
            }
            else
            {
                gvPromotionDetail.Columns[0].Visible = false;
                gvPromotionDetail.Columns[1].Visible = false;
                gvPromotionDetail.Columns[2].Visible = true;
                gvPromotionDetail.Columns[3].Visible = true;
                gvPromotionDetail.Columns[5].Visible = true;
                gvPromotionDetail.Columns[7].Visible = true;
                
            }
        }
    }
}
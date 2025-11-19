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
using System.Globalization;

using System.IO;

namespace DOMS_TSR.src.Point
{
    public partial class ReportCustomerPoint : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];

        string Idlist = "";
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        string RedeemFlag = "";
        string ComplementaryFlag = "";
        string CustomerCode_req = "";
        protected static int currentPageNumber;
        protected static int currentPageNumber_Used;
        public Boolean check_GetPoint = false;
        public Boolean check_UsePoint = false;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumber_Used = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];
                
                if (empInfo != null && merchantinfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    CustomerCode_req = Request.QueryString["CustomerCode"].ToString();
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                
                lblTitle.Text = CustomerCode_req;
                lblTitle_Used.Text = CustomerCode_req;
                LoadEmpBuLevel(hidEmpCode.Value);
                loadPromotion();
                loadPromotion_Used();
                
                BindddlActionCode_Used();
                BindddlPropoint_Used();
                BindddlPointType_Used();
                BindddlActionCode_Used();
                ShowComboSection("N");
                Hide_Section();
                showGetPoint(true);
            }

        }
        protected void BindddlPropoint_Used()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_PROPOINT; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);




            ddlSearchPropoint_Used.DataSource = lLookupInfo;

            ddlSearchPropoint_Used.DataTextField = "LookupValue";

            ddlSearchPropoint_Used.DataValueField = "LookupCode";

            ddlSearchPropoint_Used.DataBind();

            ddlSearchPropoint_Used.Items.Insert(0, new ListItem("กรุณาเลือก-------------------------------", "-99"));

        }
        protected void BindddlActionCode_Used()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_POINTACTION; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);




            ddlSearchActionCode_Used.DataSource = lLookupInfo;

            ddlSearchActionCode_Used.DataTextField = "LookupValue";

            ddlSearchActionCode_Used.DataValueField = "LookupCode";

            ddlSearchActionCode_Used.DataBind();

            ddlSearchActionCode_Used.Items.Insert(0, new ListItem("กรุณาเลือก-------------------------------", "-99"));

        }
        protected void BindddlPointType_Used()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_POINTTYPE; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlSearchPointType_Used.DataSource = lLookupInfo;

            ddlSearchPointType_Used.DataTextField = "LookupValue";

            ddlSearchPointType_Used.DataValueField = "LookupCode";

            ddlSearchPointType_Used.DataBind();

            ddlSearchPointType_Used.Items.Insert(0, new ListItem("กรุณาเลือก-------------------------------", "-99"));

        }
        protected void LoadEmpBuLevel(string empcode)
        {
            List<EmpMapBu> lebuInfo = new List<EmpMapBu>();
            lebuInfo = GetEmpMapBuMaster(empcode);

            if (lebuInfo.Count > 0)
            {
                foreach (var le in lebuInfo)
                {
                    hidBu.Value = le.Bu;
                    hidLevels.Value = le.Levels;
                }
            }
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadPromotion();
        }
        protected void ddlPage_SelectedIndexChanged_Used(object sender, EventArgs e)
        {
            currentPageNumber_Used = Int32.Parse(ddlPage_Used.SelectedValue);

            loadPromotion_Used();
        }
        #region Function




        protected void loadPromotion()
        {
            List<ReportPointInfo> lReportPointInfo = new List<ReportPointInfo>();

            

            int? totalRow = CountReportPromotionPoint();

            SetPageBar(Convert.ToDouble(totalRow));


            lReportPointInfo = GetReportPromotionPoint();

            gvPromotion.DataSource = lReportPointInfo;

            gvPromotion.DataBind();


            

        }
        protected void loadPromotion_Used()
        {
            List<ReportPointInfo> lReportPointInfo = new List<ReportPointInfo>();

            int? totalRow = CountReportPromotionPoint_Used();

            SetPageBar_Used(Convert.ToDouble(totalRow));


            lReportPointInfo = GetReportPromotionPoint_Used();

            gvUsePoint.DataSource = lReportPointInfo;

            gvUsePoint.DataBind();


            

        }

        public List<ReportPointInfo> GetReportPromotionPoint()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListReportPromotionPoint";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = hidMerchantCode.Value;

                data["ActionCode"] = StaticField.PointAction03; 

                data["CustomerCode"] = Request.QueryString["CustomerCode"].ToString();

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ReportPointInfo> lReportPointInfo = JsonConvert.DeserializeObject<List<ReportPointInfo>>(respstr);


            return lReportPointInfo;

        }
        public List<ReportPointInfo> GetReportPromotionPoint_Used()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListReportPromotionPoint";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = hidMerchantCode.Value;

                data["PointTypeCode"] = ddlSearchPointType_Used.SelectedValue;

                data["ProPointCode"] = ddlSearchPropoint_Used.SelectedValue;

                data["ActionCode"] = ddlSearchActionCode_Used.SelectedValue;

                data["CustomerCode"] = Request.QueryString["CustomerCode"].ToString();

                data["rowOFFSet"] = ((currentPageNumber_Used - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ReportPointInfo> lReportPointInfo = JsonConvert.DeserializeObject<List<ReportPointInfo>>(respstr);


            return lReportPointInfo;

        }

        public int? CountReportPromotionPoint()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountReportPromotionPoint";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["MerchantCode"] = hidMerchantCode.Value;

                data["CustomerCode"] = Request.QueryString["CustomerCode"].ToString();  

                data["ActionCode"] = StaticField.PointAction03; 

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }
        public int? CountReportPromotionPoint_Used()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountReportPromotionPoint";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["MerchantCode"] = hidMerchantCode.Value;

                data["PointTypeCode"] = ddlSearchPointType_Used.SelectedValue;

                data["ProPointCode"] = ddlSearchPropoint_Used.SelectedValue;

                data["ActionCode"] = ddlSearchActionCode_Used.SelectedValue;

                data["CustomerCode"] = Request.QueryString["CustomerCode"].ToString();

                data["rowOFFSet"] = ((currentPageNumber_Used - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

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


            loadPromotion();
        }
        protected void GetPageIndex_Used(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber_Used = 1;
                    break;

                case "Previous":
                    currentPageNumber_Used = Int32.Parse(ddlPage_Used.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber_Used = Int32.Parse(ddlPage_Used.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber_Used = Int32.Parse(lblTotalPages_Used.Text);
                    break;
            }


            loadPromotion_Used();
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

            for (int i = 0; i < gvPromotion.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvPromotion.Rows[i].FindControl("chkPromotion");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionId");
                    HiddenField hidCode = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionCode");

                    if (Idlist != "")
                    {
                        Idlist += "," + hidId.Value + "";
                    }
                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Idlist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePromotion";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Idlist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePromtoionDetailInfoByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Codelist;


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
        protected void showSection_GetPoint_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_GetPoint();
        }

        protected void showSection_UsePoint_Click(object sender, EventArgs e)
        {
            Hide_Section();
            sectionLoad_UsePoint();
        }

        public void sectionLoad_GetPoint()
        {

            showSection_GetPoint.CssClass = "btn btn-primary";
            showGetPoint(true);

        }
        public void sectionLoad_UsePoint()
        {

            showSection_UsePoint.CssClass = "btn btn-primary";
            showUsePoint(true);

        }
        public void Hide_Section()
        {
            showGetPoint(false);
            showUsePoint(false);
        }
        public void showGetPoint(Boolean show)
        {
            btnSecondary();
            showSection_GetPoint.CssClass = "btn btn-primary";
            
            Section_GetPoint.Visible = show;
        }
        public void showUsePoint(Boolean show)
        {
            btnSecondary();
            showSection_UsePoint.CssClass = "btn btn-primary";
            
           
            Section_UsePoint.Visible = show;
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
        protected void SetPageBar_Used(double totalRow)
        {

            lblTotalPages_Used.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage_Used.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages_Used.Text) + 1; i++)
            {
                ddlPage_Used.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlPage_Used, currentPageNumber_Used.ToString());
            

            
            if ((currentPageNumber_Used == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst_Used.Enabled = false;
                lnkbtnPre_Used.Enabled = false;
                lnkbtnNext_Used.Enabled = true;
                lnkbtnLast_Used.Enabled = true;
            }
            else if ((currentPageNumber_Used.ToString() == lblTotalPages_Used.Text) && (currentPageNumber_Used == 1))
            {
                lnkbtnFirst_Used.Enabled = false;
                lnkbtnPre_Used.Enabled = false;
                lnkbtnNext_Used.Enabled = false;
                lnkbtnLast_Used.Enabled = false;
            }
            else if ((currentPageNumber_Used.ToString() == lblTotalPages_Used.Text) && (currentPageNumber_Used > 1))
            {
                lnkbtnFirst_Used.Enabled = true;
                lnkbtnPre_Used.Enabled = true;
                lnkbtnNext_Used.Enabled = false;
                lnkbtnLast_Used.Enabled = false;
            }
            else
            {
                lnkbtnFirst_Used.Enabled = true;
                lnkbtnPre_Used.Enabled = true;
                lnkbtnNext_Used.Enabled = true;
                lnkbtnLast_Used.Enabled = true;
            }
            
        }

        public string GetPromotionImgByCriteria(string ProductCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetPromotionImageUrl";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = ProductCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionImageInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionImageInfo>>(respstr);


            return lPromotionInfo.Count > 0 ? lPromotionInfo[0].PromotionImageId.ToString() : "";

        }

        protected List<PromotionDetailInfo> getPromoDetailList(string proCode)
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProducttionDetailNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = proCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);

            return lPromotionInfo;
        }

        protected List<EmpMapBu> GetEmpMapBuMaster(string empcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpMapBuNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpMapBu> lebuInfo = JsonConvert.DeserializeObject<List<EmpMapBu>>(respstr);

            return lebuInfo;
        }

        protected List<PromotionInfo> GetPromotionIDByCritreria(string promotioncode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = promotioncode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lpromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            return lpromotionInfo;
        }
        #endregion

        #region Event 

        protected void gvPromotion_Change(object sender, GridViewPageEventArgs e)
        {
            gvPromotion.PageIndex = e.NewPageIndex;

            List<ReportPointInfo> ReportPointInfo = new List<ReportPointInfo>();

            ReportPointInfo = GetReportPromotionPoint();

            gvPromotion.DataSource = ReportPointInfo;

            gvPromotion.DataBind();

        }

        protected void chkPromotionAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPromotion.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvPromotion.HeaderRow.FindControl("chkPromotionAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionId");
                    HiddenField hidCode = (HiddenField)gvPromotion.Rows[i].FindControl("hidPromotionCode");

                    if (Idlist != "")
                    {
                        Idlist += ",'" + hidId.Value + "'";
                    }
                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Idlist += "'" + hidId.Value + "'";
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkPromotion = (CheckBox)gvPromotion.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkPromotion = (CheckBox)gvPromotion.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = false;
                }

            }
            hidIdList.Value = Idlist;
            hidCodeList.Value = Codelist;
        }
        public void btnSecondary()
        {
            if (check_GetPoint == true) { }
            else { showSection_GetPoint.CssClass = "btn-8bar-disable"; }

            if (check_UsePoint == true) { }
            else { showSection_UsePoint.CssClass = "btn-8bar-disable2"; }
            
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadPromotion();

        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
          
            txtSearchStartDateFrom_Get.Text = "";
            txtSearchStartDateTo_Get.Text = "";
        }
        protected void btnSearch_Used_Click(object sender, EventArgs e)
        {
            currentPageNumber_Used = 1;
            loadPromotion_Used();

        }

        protected void btnClearSearch_Used_Click(object sender, EventArgs e)
        {
            ddlSearchPointType_Used.ClearSelection();
            ddlSearchPropoint_Used.ClearSelection();
            ddlSearchActionCode_Used.ClearSelection();
        }





        protected void ShowComboSection(string flag)
        {
            

        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"PromotionDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindddlSearchProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;
                data["MerchantMapCode"] = hidMerchantCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);


            

        }

        protected void BindddlProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = null;
                data["MerchantMapCode"] = hidMerchantCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);


            

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
        #endregion

        #region start k2 work flow
        #endregion
    }
}
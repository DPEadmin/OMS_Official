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
    public partial class ReportPromotionPoint : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];

        string Idlist = "";
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        string RedeemFlag = "";
        string ComplementaryFlag = "";
        string PromotionCode_req = "";
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];
                
                if (empInfo != null && merchantinfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    PromotionCode_req = Request.QueryString["PromotionCode"].ToString();
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                ReportPromotionPoint_title.Text = Request.QueryString["PromotionCode"].ToString()+ "-" + Request.QueryString["PromotionName"].ToString(); 
                
                LoadEmpBuLevel(hidEmpCode.Value);
                loadPromotion();

                BindddlPropoint();
                BindddlPointType();
                BindddlPointRange();
                BindddlCompany();
                
                ShowComboSection("N");
            }

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

        public List<ReportPointInfo> GetReportPromotionPoint()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListReportPromotionPoint";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = hidMerchantCode.Value;

                data["ProPointCode"] = ddlPropointSearch.SelectedValue;
                
                data["PointTypeCode"] = ddlPointTypeSearch.SelectedValue;  

                data["PointRangeCode"] = ddlPointRangeSearch.SelectedValue;
                
                data["CompanyCode"] = ddlCompanySearch.SelectedValue;

                data["PromotionCode"] = Request.QueryString["PromotionCode"].ToString(); 

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

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

                data["ProPointCode"] = ddlPropointSearch.SelectedValue;

                data["PointTypeCode"] = ddlPointTypeSearch.SelectedValue;

                data["PointRangeCode"] = ddlPointRangeSearch.SelectedValue;

                data["CompanyCode"] = ddlCompanySearch.SelectedValue;

                data["PromotionCode"] = Request.QueryString["PromotionCode"].ToString();

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadPromotion();


        }



        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchPromotionName.Text = "";
          
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
            ddlPropointSearch.ClearSelection();
            ddlPointTypeSearch.ClearSelection();
            ddlCompanySearch.ClearSelection();
            ddlPointRangeSearch.ClearSelection();
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



        protected void BindddlPropoint()
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


          

            ddlPropointSearch.DataSource = lLookupInfo;

            ddlPropointSearch.DataTextField = "LookupValue";

            ddlPropointSearch.DataValueField = "LookupCode";

            ddlPropointSearch.DataBind();

            ddlPropointSearch.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected void BindddlPointRange()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPointRangePagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantMapCode"] = hidMerchantCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PointInfo> lPointInfo = JsonConvert.DeserializeObject<List<PointInfo>>(respstr);


          

            ddlPointRangeSearch.DataSource = lPointInfo;

            ddlPointRangeSearch.DataTextField = "PointName";

            ddlPointRangeSearch.DataValueField = "PointCode";

            ddlPointRangeSearch.DataBind();

            ddlPointRangeSearch.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected void BindddlPointType()
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


            ddlPointTypeSearch.DataSource = lLookupInfo;

            ddlPointTypeSearch.DataTextField = "LookupValue";

            ddlPointTypeSearch.DataValueField = "LookupCode";

            ddlPointTypeSearch.DataBind();

            ddlPointTypeSearch.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        
        protected void BindddlCompany()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CompanyListNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CompanyCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CompanyInfo> lLookupInfo = JsonConvert.DeserializeObject<List<CompanyInfo>>(respstr);



            ddlCompanySearch.DataSource = lLookupInfo;

            ddlCompanySearch.DataTextField = "CompanyNameTH";

            ddlCompanySearch.DataValueField = "CompanyCode";

            ddlCompanySearch.DataBind();

            ddlCompanySearch.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

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
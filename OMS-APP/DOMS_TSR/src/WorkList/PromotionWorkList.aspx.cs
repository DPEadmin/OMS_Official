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

namespace DOMS_TSR.src.WorkList
{
    public partial class PromotionWorkList : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];

        string Idlist = "";
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        string RedeemFlag = "";
        string ComplementaryFlag = "";
        protected static int currentPageNumber;
        protected static int currentPageNumberCom;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumberCom = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null && merchantinfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                LoadEmpBuLevel(hidEmpCode.Value);
                loadPromotion();
                loadPromotionCompleted();
            }
        }

        #region function
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
        protected void loadPromotion()
        {
            List<PromotionInfo> lPromotionInfo = new List<PromotionInfo>();
            int? totalRow = CountPromotionMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lPromotionInfo = GetPromotionMasterByCriteria();    

            gvPromotion.DataSource = lPromotionInfo;
            gvPromotion.DataBind();
        }
        public List<PromotionInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionListGreenSprtWF";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if(hidLevels.Value == "1")
                {
                    data["RequestByEmpCode"] = hidEmpCode.Value;
                }

                data["Bu"] = hidBu.Value;

                if (hidLevels.Value == "1")
                {
                    
                }
                else
                {
                    data["Levels"] = hidLevels.Value;
                }

                data["MerchantCode"] = hidMerchantCode.Value;
                data["wfFinishFlag"] = "No";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            return lPromotionInfo;
        }
        public int? CountPromotionMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListPromotionGreenSpotWF";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if (hidLevels.Value == "1")
                {
                    data["RequestByEmpCode"] = hidEmpCode.Value;
                }

                data["Bu"] = hidBu.Value;

                if (hidLevels.Value == "1")
                {
                    
                }
                else
                {
                    data["Levels"] = hidLevels.Value;
                }

                data["wfFinishFlag"] = "No";
                data["MerchantCode"] = hidMerchantCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
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
        public void loadPromotionCompleted()
        {
            List<PromotionInfo> lPromotionInfo = new List<PromotionInfo>();
            int? totalRow = CountPromotionCompletedMasterList();
            SetPageBarCom(Convert.ToDouble(totalRow));
            lPromotionInfo = GetPromotionMasterCompletedByCriteria();

            gvPromotionCompleted.DataSource = lPromotionInfo;
            gvPromotionCompleted.DataBind();
        }
        public List<PromotionInfo> GetPromotionMasterCompletedByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionListGreenSprtWF";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if (hidLevels.Value == "1")
                {
                    data["RequestByEmpCode"] = hidEmpCode.Value;
                }

                data["MerchantCode"] = hidMerchantCode.Value;
                data["wfFinishFlag"] = "Yes";
                data["Bu"] = hidBu.Value;
                
                data["rowOFFSet"] = ((currentPageNumberCom - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            return lPromotionInfo;
        }
        public int? CountPromotionCompletedMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListPromotionGreenSpotWF";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if (hidLevels.Value == "1")
                {
                    data["RequestByEmpCode"] = hidEmpCode.Value;
                }

                data["MerchantCode"] = hidMerchantCode.Value;
                data["FinishFlag"] = StaticField.PromotionWorkList_FinishFlag_Yes;
                data["Bu"] = hidBu.Value;
                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        protected void SetPageBarCom(double totalRow)
        {
            lblTotalPagescom.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlcomPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPagescom.Text) + 1; i++)
            {
                ddlcomPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl(ddlcomPage, currentPageNumberCom.ToString());
            

            
            if ((currentPageNumberCom == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtncompFirst.Enabled = false;
                lnkbtcomnPre.Enabled = false;
                lnkbtncomNext.Enabled = true;
                lnkbtncomLast.Enabled = true;
            }
            else if ((currentPageNumberCom.ToString() == lblTotalPagescom.Text) && (currentPageNumberCom == 1))
            {
                lnkbtncompFirst.Enabled = false;
                lnkbtcomnPre.Enabled = false;
                lnkbtncomNext.Enabled = false;
                lnkbtncomLast.Enabled = false;
            }
            else if ((currentPageNumberCom.ToString() == lblTotalPagescom.Text) && (currentPageNumberCom > 1))
            {
                lnkbtncompFirst.Enabled = true;
                lnkbtcomnPre.Enabled = true;
                lnkbtncomNext.Enabled = false;
                lnkbtncomLast.Enabled = false;
            }
            else
            {
                lnkbtncompFirst.Enabled = true;
                lnkbtcomnPre.Enabled = true;
                lnkbtncomNext.Enabled = true;
                lnkbtncomLast.Enabled = true;
            }
            
        }
        #endregion

        #region event
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
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadPromotion();
        }
        protected void GetcomPageIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "First":
                    currentPageNumberCom = 1;
                    break;

                case "Previous":
                    currentPageNumberCom = Int32.Parse(ddlcomPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumberCom = Int32.Parse(ddlcomPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumberCom = Int32.Parse(lblTotalPagescom.Text);
                    break;
            }

            loadPromotionCompleted();
        }
        protected void ddlcomPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumberCom = Int32.Parse(ddlcomPage.SelectedValue);

            loadPromotionCompleted();
        }
        protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPromotion.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidPromotionId = (HiddenField)row.FindControl("hidPromotionId");
            HiddenField hidPromotionCode = (HiddenField)row.FindControl("hidPromotionCode");
            Label lblWfStatus = (Label)row.FindControl("lblWfStatus");

            if (e.CommandName == "ShowPromotion")
            {
                if (lblWfStatus.Text == StaticField.WfStatus_Revised || lblWfStatus.Text == StaticField.WfStatus_Savedraft)
                {
                    Response.Redirect("../Promotion/Promotion.aspx");
                }
                else
                {
                    Response.Redirect("PromotionWorkListDetail.aspx?PromotionCode=" + hidPromotionCode.Value);
                }
            }
        }
        protected void gvPromotionCompleted_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPromotionCompleted.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidPromotionId = (HiddenField)row.FindControl("hidPromotionId");
            HiddenField hidPromotionCode = (HiddenField)row.FindControl("hidPromotionCode");

            if (e.CommandName == "ShowPromotion")
            {
                Response.Redirect("PromotionWorkListDetail.aspx?PromotionCode=" + hidPromotionCode.Value + "&FlagWfComplete=Y");
            }
        }
        #endregion

        #region binding
        #endregion
    }
}
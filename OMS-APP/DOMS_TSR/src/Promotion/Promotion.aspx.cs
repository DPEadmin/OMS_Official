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
using DocumentFormat.OpenXml.Spreadsheet;

namespace DOMS_TSR.src.Promotion
{
    public partial class Promotion : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];

        string Idlist = "";
        string Codelist = "";
        protected static int TierCharge;
        string EditFlag = "";
        Boolean isdelete;
        string RedeemFlag = "";
        string ComplementaryFlag = "";
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string RoleCode;
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                TierCharge = 1;
                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null && merchantinfo != null)
                {
                    RoleCode = empInfo.RoleCode;
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    if (empInfo.RoleCode == "ADMIN")
                    {
                        btnApprovePromotion.Visible = true;
                    }
                    else 
                    {
                        btnApprovePromotion.Visible = false;
                    }

                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                btnSubmit.Visible = false;
                insertfalshsalesection.Visible = false;

                LoadEmpBuLevel(hidEmpCode.Value);
                loadPromotion();
                BindddlLazadaDisCountType();
                BindddlPromotionLevel();
                BindddlPromotionStatus();
                BindddlPromotionType();
             
                BindddlSearchPromotionLevel();
               
                BindddlSearchPromotionType();
                BindddlPromotionTag();
                BindddlMultiSelectPromoTag_Ins();
                BindchklistPromotionMapProductTag_InsAndddlSearchProductTag();
                ddlPromoType_SelectedIndexChanged(null, null);
                ddlCombosetFlag_Ins_SelectedIndexChanged(null, null);
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

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/PromotionCodeValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = txtPromotionCode_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lProductInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

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



        protected void loadPromotion()
        {
            List<PromotionInfo> lPromotionInfo = new List<PromotionInfo>();

            

            int? totalRow = CountPromotionMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lPromotionInfo = GetPromotionMasterByCriteria();

            // Load PromotionTag and ProductTag for (Multiple Value into one column)
            List<PromotionInfo> lPromotionCompletedInfo = LoadPromotionPromotionTagandProductTag(lPromotionInfo);                        

            gvPromotion.DataSource = lPromotionCompletedInfo;

            gvPromotion.DataBind();


            

        }

        public List<PromotionInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionList";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = "-99";

                data["PromotionTypeCode"] = ddlSearchPromotionType.SelectedValue;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["PromotionCode"] = txtSearchPromotionCode.Text;

                data["PromotionName"] = txtSearchPromotionName.Text;

                data["PromotionLevel"] = ddlSearchPromotionLevel.SelectedValue;

                data["PromotionTagCode"] = ddlSearchPromotionTag.SelectedValue;

                data["ProductTagCode"] = ddlSearchProductTag.SelectedValue;

                data["StartDate"] = txtSearchStartDateFrom.Text;

                data["StartDateTo"] = txtSearchStartDateTo.Text;

                data["EndDate"] = txtSearchEndDateFrom.Text;

                data["EndDateTo"] = txtSearchEndDateTo.Text;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                data["StatusWfforReq"] = "PromotionManagement";

                data["RequestByEmpCode"] = hidEmpCode.Value;
                EmpInfo empInfo = new EmpInfo();
                empInfo = (EmpInfo)Session["EmpInfo"];
                if (empInfo!=null)
                {
                    data["RoleCode"] = empInfo.RoleCode;
                }
               
              
             
                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lPromotionInfo;

        }

        public int? CountPromotionMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListPromotion";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] ="-99";

                data["PromotionTypeCode"] = ddlSearchPromotionType.SelectedValue;

                data["PromotionCode"] = txtSearchPromotionCode.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["PromotionName"] = txtSearchPromotionName.Text;

                data["PromotionLevel"] = ddlSearchPromotionLevel.SelectedValue;

                data["PromotionTagCode"] = ddlSearchPromotionTag.SelectedValue;

                data["ProductTagCode"] = ddlSearchProductTag.SelectedValue;

                data["StartDate"] = txtSearchStartDateFrom.Text;

                data["StartDateTo"] = txtSearchStartDateTo.Text;

                data["EndDate"] = txtSearchEndDateFrom.Text;

                data["EndDateTo"] = txtSearchEndDateTo.Text;

                data["StatusWfforReq"] = StaticField.StatusWfforReq_PromotionManagement; 

                data["RequestByEmpCode"] = hidEmpCode.Value;

                EmpInfo empInfo = new EmpInfo();
                empInfo = (EmpInfo)Session["EmpInfo"];
                if (empInfo != null)
                {
                    data["RoleCode"] = empInfo.RoleCode;
                }

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
        protected List<PromotionInfo> LoadPromotionPromotionTagandProductTag(List<PromotionInfo> pInfo)
        {
            string ptagcode = "";
            foreach (var og in pInfo)
            {
                ptagcode = og.PromotionTagCode;

                char[] separator = new char[1] { ',' };
                string result = "";
                string[] subResult = ptagcode.Split(separator);

                for (int i = 0; i <= subResult.Length - 1; i++)
                {
                    List<LookupInfo> llInfo = new List<LookupInfo>();

                    if (subResult[i].ToString() != "" && subResult[i].ToString() != null)
                    {
                        llInfo = ListPromotionTagName(subResult[i]);
                    }

                    if (llInfo.Count > 0)
                    {
                        result += llInfo[0].LookupValue + ",";
                    }
                }

                if (result != "" && result != null)
                {
                    result = result.Remove(result.Length - 1);
                }

                og.PromotionTagName = result;
            }

            string ptpcode = "";
            foreach (var oh in pInfo)
            {
                ptpcode = oh.ProductTagCode;

                char[] separator = new char[1] { ',' };
                string result = "";
                string[] subResult = ptpcode.Split(separator);

                for (int i = 0; i <= subResult.Length - 1; i++)
                {
                    List<LookupInfo> llookupInfo = new List<LookupInfo>();

                    if (subResult[i].ToString() != "" && subResult[i].ToString() != null)
                    {
                        llookupInfo = ListProductTagName(subResult[i]);
                    }

                    if (llookupInfo.Count > 0)
                    {
                        result += llookupInfo[0].LookupValue + ",";
                    }
                }

                if (result != "" && result != null)
                {
                    result = result.Remove(result.Length - 1);
                }

                oh.ProductTagName = result;
            }

            

            return pInfo;
        }
        protected List<PromotionInfo> GetPromotionMapPromotionTagbyPromotionCodeString(string pCodeStr)
        {
            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionMapPromotionTagNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCodeString"] = pCodeStr;
                data["MerchantCode"] = merchantinfo.MerchantCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lpromomappromotag = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lpromomappromotag;
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
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                        Codelist += "'" + hidCode.Value + "'";
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
        protected Boolean ApprovePromotion()
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
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                        Codelist += "'" + hidCode.Value + "'";
                    }

                }
            }

            if (Idlist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/ApprovePromotion";

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

            List<PromotionInfo> lPromotionInfo = new List<PromotionInfo>();

            lPromotionInfo = GetPromotionMasterByCriteria();

            gvPromotion.DataSource = lPromotionInfo;

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



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeletePromotion();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select item to delete');", true);
            }



        }
        protected void btnApprovePromotion_Click(object sender, EventArgs e)
        {
            isdelete = ApprovePromotion();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select item to Approve');", true);
            }



        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            hidFlagApprove.Value = "Y";
            hidFlagSavedraft.Value = "N";

            EmpInfo empInfo = new EmpInfo();

            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

            POInfo pInfo = new POInfo();

            string combosetName = (hidCombosetFlag_Ins.Value == "Y") ? txtPromotionName_Ins.Text : "";

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (validateInsert())
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {
                        HttpFileCollection uploadFiles = Request.Files;

                        for (int i = 0; i < uploadFiles.Count; i++)
                        {
                            HttpPostedFile postedFile = uploadFiles[i];
                            if (postedFile != null && postedFile.ContentLength > 0)
                            {
                                //Convert to Base64
                                Stream fs = postedFile.InputStream;
                                BinaryReader br = new BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                hidPicturePromotionUrl_Ins.Value = PromotionImgUrl + postedFile.FileName;

                                //Save Images
                                string respstring = "";

                                APIpath = APIUrl + "/api/support/InsertPromotionImage";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                    data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                    data["PromotionImageName"] = postedFile.FileName;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }

                                string APIpath1 = APIUrl + "/api/support/SavePromoPicfromjsonstring64/";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                    data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                    data["PromotionImageName"] = postedFile.FileName;
                                    data["PromotionImageBase64"] = base64String;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath1, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }

                            }


                        }

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertPromotion";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionCode"] = txtPromotionCode_Ins.Text;
                            data["PromotionName"] = txtPromotionName_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["PromotionDesc"] = txtPromotionDesc_Ins.Text;
                            data["PromotionLevel"] = ddlPromotionLevel_Ins.SelectedValue;

                            data["FreeShippingCode"] = ddlFreeShipFlag_Ins.SelectedValue;
                            
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = ddlPromotionType_Ins.SelectedValue;

                            data["DiscountPercent"] = txtDiscountPercent_Ins.Text == null || txtDiscountPercent_Ins.Text == "" ? data["DiscountPercent"] = "0" : data["DiscountPercent"] = txtDiscountPercent_Ins.Text;
                            data["DiscountAmount"] = txtDiscountAmount_Ins.Text == null || txtDiscountAmount_Ins.Text == "" ? data["DiscountAmount"] = "0" : data["DiscountAmount"] = txtDiscountAmount_Ins.Text;
                            data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text == null || txtProductDiscountPercent_Ins.Text == "" ? data["ProductDiscountPercent"] = "0" : data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                            data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text == null || txtProductDiscountAmount_Ins.Text == "" ? data["ProductDiscountAmount"] = "0" : data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;
                            data["DefaultAmount"] = "0";

                            data["MOQFlag"] = ddlMOQFlag_Ins.SelectedValue;
                            data["MinimumQty"] = txtMinimumQty_Ins.Text;

                            data["LockCheckbox"] = ddlLockCheckbox_Ins.SelectedValue;
                            data["LockAmountFlag"] = ddlLockAmountFlag_Ins.SelectedValue;

                            data["MinimumTotPrice"] = txtMinimumTotPrice_Ins.Text;
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                            data["ComplementaryChangeAble"] = ddl_ComplementaryStandardChangeAble_Ins.SelectedValue;
                            data["GroupPrice"] = txtGroupPrice_Ins.Text;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] = "-99";

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;

                            data["CreateBy"] = empInfo.EmpCode;
                            data["MerchantCode"] = merchantinfo.MerchantCode;

                            data["Bu"] = hidBu.Value;
                            data["levels"] = hidLevels.Value;
                            data["RequestByEmpCode"] = hidEmpCode.Value;

                            //LazadaFlexicombo
                            data["ApplyScope"] = rbtApplyScope.SelectedValue;
                            data["CriteriaType"] = rbtCriteriaType.SelectedValue;
                            data["DiscountType"] = ddlDisCountType.SelectedValue;
                            data["OrderNumbers"] = txtOrderNumbers_Ins.Text;
                            data["CriteriaValueTier1"] = txtCriteriaValueTier1_Ins.Text;
                            data["CriteriaValueTier2"] = txtCriteriaValueTier2_Ins.Text;
                            data["CriteriaValueTier3"] = txtCriteriaValueTier3_Ins.Text;
                            data["DiscountValueTier1"] = txtDiscountValueTier1_Ins.Text;
                            data["DiscountValueTier2"] = txtDiscountValueTier2_Ins.Text;
                            data["DiscountValueTier3"] = txtDiscountValueTier3_Ins.Text;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            if (hidFlagApprove.Value == "Y")
                            {
                                StartWorkFlow(txtPromotionCode_Ins.Text, "01", "01");
                            }

                            btnCancel_Click(null, null);

                            loadPromotion();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Promotion').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    else //Update
                    {
                        if (hidPromotionImgId.Value != "")
                        {
                            HttpFileCollection uploadFiles = Request.Files;

                            for (int i = 0; i < uploadFiles.Count; i++)
                            {
                                HttpPostedFile postedFile = uploadFiles[i];
                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Convert to Base64
                                    Stream fs = postedFile.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                    hidPicturePromotionUrl_Ins.Value = PromotionImgUrl + postedFile.FileName;

                                    //Save Images
                                    string respstring = "";

                                    APIpath = APIUrl + "/api/support/UpdatePromotionImage";

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                    string APIpath1 = APIUrl + "/api/support/SavePromoPicfromjsonstring64";
                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["PromotionImageBase64"] = base64String;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath1, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                }
                            }
                        }
                        else
                        {
                            HttpFileCollection uploadFiles = Request.Files;

                            for (int i = 0; i < uploadFiles.Count; i++)
                            {
                                HttpPostedFile postedFile = uploadFiles[i];
                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Convert to Base64
                                    Stream fs = postedFile.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                    hidPicturePromotionUrl_Ins.Value = PromotionImgUrl + postedFile.FileName;
                                    //Save Images
                                    string respstring = "";

                                    APIpath = APIUrl + "/api/support/InsertPromotionImage";

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                    string APIpath1 = APIUrl + "/api/support/SavePromoPicfromjsonstring64";
                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["PromotionImageBase64"] = base64String;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath1, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                }
                            }

                        }

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdatePromotion";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionId"] = hidIdList.Value;

                            data["PromotionCode"] = txtPromotionCode_Ins.Text;
                            data["PromotionName"] = txtPromotionName_Ins.Text;

                            data["FlagDelete"] = "N";
                            data["PromotionDesc"] = txtPromotionDesc_Ins.Text;
                            
                            data["FreeShippingCode"] = ddlFreeShipFlag_Ins.SelectedValue;
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = ddlPromotionType_Ins.SelectedValue;
                            data["PromotionLevel"] = ddlPromotionLevel_Ins.SelectedValue;


                            data["DiscountPercent"] = txtDiscountPercent_Ins.Text == null || txtDiscountPercent_Ins.Text == "" ? data["DiscountPercent"] = "0" : data["DiscountPercent"] = txtDiscountPercent_Ins.Text;
                            data["DiscountAmount"] = txtDiscountAmount_Ins.Text == null || txtDiscountAmount_Ins.Text == "" ? data["DiscountAmount"] = "0" : data["DiscountAmount"] = txtDiscountAmount_Ins.Text;
                            data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text == null || txtProductDiscountPercent_Ins.Text == "" ? data["ProductDiscountPercent"] = "0" : data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                            data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text == null || txtProductDiscountAmount_Ins.Text == "" ? data["ProductDiscountAmount"] = "0" : data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;

                            data["MOQFlag"] = ddlMOQFlag_Ins.SelectedValue;
                            data["MinimumQty"] = txtMinimumQty_Ins.Text;
                            data["MinimumQtyTier2"] = txtMinimumQtyTier2_Ins.Text;
                            data["DefaultAmount"] = "0";

                            data["LockCheckbox"] = ddlLockCheckbox_Ins.SelectedValue;
                            data["LockAmountFlag"] = ddlLockAmountFlag_Ins.SelectedValue;

                            data["MinimumTotPrice"] = txtMinimumTotPrice_Ins.Text;
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                            data["ComplementaryChangeAble"] = ddl_ComplementaryStandardChangeAble_Ins.SelectedValue;                            
                            data["GroupPrice"] = txtGroupPrice_Ins.Text;

                            data["CreateBy"] = empInfo.EmpCode;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] =  "-99";

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;

                            data["ApplyScope"] = rbtApplyScope.SelectedValue;
                            data["CriteriaType"] = rbtCriteriaType.SelectedValue;
                            data["DiscountType"] = ddlDisCountType.SelectedValue;
                            data["OrderNumbers"] = txtOrderNumbers_Ins.Text;
                            data["CriteriaValueTier1"] = txtCriteriaValueTier1_Ins.Text;
                            data["CriteriaValueTier2"] = txtCriteriaValueTier2_Ins.Text;
                            data["CriteriaValueTier3"] = txtCriteriaValueTier3_Ins.Text;
                            data["DiscountValueTier1"] = txtDiscountValueTier1_Ins.Text;
                            data["DiscountValueTier2"] = txtDiscountValueTier2_Ins.Text;
                            data["DiscountValueTier3"] = txtDiscountValueTier3_Ins.Text;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            List<PromotionDetailInfo> lPromoDetail = getPromoDetailList(txtPromotionCode_Ins.Text);

                            if (lPromoDetail.Count > 0)
                            {

                                string respstr2 = "";

                                APIpath = APIUrl + "/api/support/UpdatePromotionDetailDiscount";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["PromotionCode"] = txtPromotionCode_Ins.Text;


                                    data["DiscountPercent"] = txtProductDiscountPercent_Ins.Text == null || txtProductDiscountPercent_Ins.Text == "" ? data["ProductDiscountPercent"] = "0" : data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                                    data["DiscountAmount"] = txtProductDiscountAmount_Ins.Text == null || txtProductDiscountAmount_Ins.Text == "" ? data["ProductDiscountAmount"] = "0" : data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;


                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstr2 = Encoding.UTF8.GetString(response);
                                }

                                int? sum2 = JsonConvert.DeserializeObject<int?>(respstr2);


                                
                            }

                            if (hidFlagApprove.Value == "Y")
                            {
                                StartWorkFlow(txtPromotionCode_Ins.Text, "01", "01");
                            }

                            btnCancel_Click(null, null);

                            loadPromotion();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Promotion').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CancelTierSection();
            txtPromotionCode_Ins.Text = "";
            txtPromotionName_Ins.Text = "";
            txtPromotionDesc_Ins.Text = "";
            txtDiscountAmount_Ins.Text = "0";
            txtDiscountPercent_Ins.Text = "0";

            txtProductDiscountAmount_Ins.Text = "0";
            txtProductDiscountPercent_Ins.Text = "0";
            txtGroupPrice_Ins.Text = "0";
            txtMinimumQty_Ins.Text = "0";
            txtMinimumTotPrice_Ins.Text = "0";
            
            ddlPromotionStatus_Ins.SelectedValue = "-99";
            ddlPromotionType_Ins.SelectedValue = "-99";
            
            ddlLockAmountFlag_Ins.SelectedValue = "-99";
            ddlLockCheckbox_Ins.SelectedValue = "-99";
            ddlMOQFlag_Ins.SelectedValue = "-99";
            ddl_ComplementaryStandardChangeAble_Ins.SelectedValue = "-99";
            ddlMultiSelectPromoTag_Ins.ClearSelection();
            
            hidPicturePromotionUrl_Ins.Value = null;

            lblPromotionCode_Ins.Text = "";
            LbPromotionName_Ins.Text = "";
            lblPromotionLevel_Ins.Text = "";
            lblStartDate_Ins.Text = "";
            lblEndDate_Ins.Text = "";
            lblStartEnd_Ins.Text = "";
            lblPromotionStatus_Ins.Text = "";
            lblPromotionType_Ins.Text = "";
            lblPromotionTag_Ins.Text = "";
            lbllMultiSelectPromoTag_Ins.Text = "";

            txtCriteriaValueTier1_Ins.Text = "";
            txtDiscountValueTier1_Ins.Text = "";
            txtCriteriaValueTier2_Ins.Text = "";
            txtDiscountValueTier2_Ins.Text = "";
            txtCriteriaValueTier3_Ins.Text = "";
            txtDiscountValueTier3_Ins.Text = "";

            ddlPromoType_SelectedIndexChanged(null, null);
            ddlCombosetFlag_Ins_SelectedIndexChanged(null, null);

            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchPromotionCode.Text = "";
            txtSearchPromotionName.Text = "";
          
            ddlSearchPromotionLevel.SelectedValue = "-99";
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
            txtSearchEndDateFrom.Text = "";
            txtSearchEndDateTo.Text = "";
            ddlSearchPromotionType.SelectedValue = "-99";
            ddlSearchPromotionTag.SelectedValue = "-99";
            ddlSearchProductTag.SelectedValue = "-99";
        }
        protected void gvPromotion_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblWfStatus = (e.Row.FindControl("lblWfStatus") as Label);
                LinkButton btnEdit = (e.Row.FindControl("btnEdit") as LinkButton);
                CheckBox chkPromotion = (e.Row.FindControl("chkPromotion") as CheckBox);
             
             
                EmpInfo empInfo = new EmpInfo();
                empInfo = (EmpInfo)Session["EmpInfo"];
                if (empInfo.RoleCode == "ADMIN")
                {
                    if (lblWfStatus.Text == "Approved")
                    {
                        btnEdit.Visible = false;
                        chkPromotion.Enabled = false;
                    }
                }
                else 
                {
                    chkPromotion.Enabled = true;
                }

            }
        }
        protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPromotion.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            LbddlFreeShipFlag_Ins.Text = "";
            lblDiscountAmount_Ins.Text = "";
            lblDiscountPercent_Ins.Text = "";
            lblProductDiscountAmount_Ins.Text = "";
            lblProductDiscountPercent_Ins.Text = "";
            lblStartEnd_Ins.Text = "";

            lblPromotionCode_Ins.Text = "";
            LbPromotionName_Ins.Text = "";
            lblPromotionLevel_Ins.Text = "";
            lblStartDate_Ins.Text = "";
            lblEndDate_Ins.Text = "";
            lblStartEnd_Ins.Text = "";
            lblPromotionStatus_Ins.Text = "";
            lblPromotionType_Ins.Text = "";
            lbllMultiSelectPromoTag_Ins.Text = "";
            lblPromotionMapProductTag_Ins.Text = "";

            HiddenField hidPromotionId = (HiddenField)row.FindControl("hidPromotionId");
            HiddenField hidPromotionCode = (HiddenField)row.FindControl("hidPromotionCode");
            HiddenField hidPromotionName = (HiddenField)row.FindControl("hidPromotionName");
            HiddenField hidPromotionDesc = (HiddenField)row.FindControl("hidPromotionDesc");
            HiddenField hidPromotionLevel = (HiddenField)row.FindControl("hidPromotionLevel");

            HiddenField hidPromotionType = (HiddenField)row.FindControl("hidPromotionType");

            HiddenField hidFreeShipping = (HiddenField)row.FindControl("hidFreeShipping");
            HiddenField hidStatus = (HiddenField)row.FindControl("hidStatus");

            HiddenField hidDiscountPercent = (HiddenField)row.FindControl("hidDiscountPercent");
            HiddenField hidDiscountAmount = (HiddenField)row.FindControl("hidDiscountAmount");
            HiddenField hidProductDiscountPercent = (HiddenField)row.FindControl("hidProductDiscountPercent");
            HiddenField hidProductDiscountAmount = (HiddenField)row.FindControl("hidProductDiscountAmount");
            HiddenField hidProductDiscountPercentTier2 = (HiddenField)row.FindControl("hidProductDiscountPercentTier2");
            HiddenField hidProductDiscountAmountTier2 = (HiddenField)row.FindControl("hidProductDiscountAmountTier2");
            HiddenField hidMOQFlag = (HiddenField)row.FindControl("hidMOQFlag");
            HiddenField hidMinimumQty = (HiddenField)row.FindControl("hidMinimumQty");
            HiddenField hidMinimumQtyTier2 = (HiddenField)row.FindControl("hidMinimumQtyTier2");
            HiddenField hidGroupPrice = (HiddenField)row.FindControl("hidGroupPrice");

            HiddenField hidLockCheckbox = (HiddenField)row.FindControl("hidLockCheckbox");
            HiddenField hidLockAmountFlag = (HiddenField)row.FindControl("hidLockAmountFlag");
            HiddenField hidMinimumTotPrice = (HiddenField)row.FindControl("hidMinimumTotPrice");
            HiddenField hidRedeemFlag = (HiddenField)row.FindControl("hidRedeemFlag");
            HiddenField hidComplementaryFlag = (HiddenField)row.FindControl("hidComplementaryFlag");
            HiddenField hidComplementaryChangeAble = (HiddenField)row.FindControl("hidComplementaryChangeAble");
            HiddenField hidPicturePromotionUrl = (HiddenField)row.FindControl("hidPicturePromotionUrl");
            HiddenField hidCombosetFlag = (HiddenField)row.FindControl("hidCombosetFlag");
            HiddenField hidCombosetName = (HiddenField)row.FindControl("hidCombosetName");
            HiddenField hidStartDate = (HiddenField)row.FindControl("hidStartDate");
            HiddenField hidEndDate = (HiddenField)row.FindControl("hidEndDate");
            HiddenField hidProductBrandCode = (HiddenField)row.FindControl("hidProductBrandCode");
            HiddenField hidPromotionTagCode = (HiddenField)row.FindControl("hidPromotionTagCode");
            HiddenField hidProductTagCode = (HiddenField)row.FindControl("hidProductTagCode");

            HiddenField hidApplyScope = (HiddenField)row.FindControl("hidApplyScope");
            HiddenField hidCriteriaType = (HiddenField)row.FindControl("hidCriteriaType");
            HiddenField hidDiscountType = (HiddenField)row.FindControl("hidDiscountType");
            HiddenField hidOrderNumbers = (HiddenField)row.FindControl("hidOrderNumbers");
            HiddenField hidCriteriaValueTier1 = (HiddenField)row.FindControl("hidCriteriaValueTier1");
            HiddenField hidCriteriaValueTier2 = (HiddenField)row.FindControl("hidCriteriaValueTier2");
            HiddenField hidCriteriaValueTier3 = (HiddenField)row.FindControl("hidCriteriaValueTier3");
            HiddenField hidDiscountValueTier1 = (HiddenField)row.FindControl("hidDiscountValueTier1");
            HiddenField hidDiscountValueTier2 = (HiddenField)row.FindControl("hidDiscountValueTier2");
            HiddenField hidDiscountValueTier3 = (HiddenField)row.FindControl("hidDiscountValueTier3");

            if (e.CommandName == "ShowPromotion")
            {
                string lproducttag = hidProductTagCode.Value;

                char[] separator = new char[1] { ',' };
                string[] subResult = lproducttag.Split(separator);

                foreach (ListItem listItem in chklistPromotionMapProductTag_Ins.Items)
                {
                    listItem.Selected = false;

                    for (int i = 0; i <= subResult.Length - 1; i++)
                    {
                        if (listItem.Value == subResult[i])
                        {
                            listItem.Selected = true;
                        }
                    }
                }

                string lpromotag = hidPromotionTagCode.Value;

                char[] separator1 = new char[1] { ',' };
                string[] subResult1 = lpromotag.Split(separator);
                ddlMultiSelectPromoTag_Ins.ClearSelection();
                foreach (ListItem listItem1 in ddlMultiSelectPromoTag_Ins.Items)
                {
                    for (int i = 0; i <= subResult1.Length - 1; i++)
                    {
                        if (listItem1.Value == subResult1[i])
                        {
                            listItem1.Selected = true;
                        }
                    }
                }

                

                txtPromotionCode_Ins.Text = hidPromotionCode.Value;
                
                hidPromotionCode_Ins.Value = hidPromotionCode.Value;
                txtPromotionName_Ins.Text = hidPromotionName.Value;
                txtPromotionDesc_Ins.Text = hidPromotionDesc.Value;
                ddlPromotionLevel_Ins.SelectedValue = (hidPromotionLevel.Value == null || hidPromotionLevel.Value == "") ? hidPromotionLevel.Value = "-99" : hidPromotionLevel.Value.Trim();
                ddlPromotionStatus_Ins.SelectedValue = (hidStatus.Value == null || hidStatus.Value == "") ? hidStatus.Value = "-99" : hidStatus.Value;
                

                ddlPromotionType_Ins.SelectedValue = (hidPromotionType.Value == null || hidPromotionType.Value == "") ? hidPromotionType.Value = "-99" : hidPromotionType.Value;
                ddlPromoType_SelectedIndexChanged(ddlPromotionType_Ins.SelectedValue, null);
                ddlFreeShipFlag_Ins.SelectedValue = (hidFreeShipping.Value == null || hidFreeShipping.Value == "") ? hidFreeShipping.Value = "-99" : hidFreeShipping.Value.Trim();
                
                ddl_ComplementaryStandardChangeAble_Ins.SelectedValue = (hidComplementaryChangeAble.Value == null || hidComplementaryChangeAble.Value == "") ? hidComplementaryChangeAble.Value = "-99" : hidComplementaryChangeAble.Value.Trim();
                
                txtDiscountAmount_Ins.Text = hidDiscountAmount.Value;
                txtDiscountPercent_Ins.Text = hidDiscountPercent.Value;

                txtProductDiscountAmount_Ins.Text = hidProductDiscountAmount.Value;
                txtProductDiscountPercent_Ins.Text = hidProductDiscountPercent.Value;

                txtProductDiscountAmountTier2_Ins.Text = hidProductDiscountAmountTier2.Value;
                txtProductDiscountPercentTier2_Ins.Text = hidProductDiscountPercentTier2.Value;

                if (Convert.ToDouble(hidDiscountAmount.Value) != 0)
                {
                    txtDiscountAmount_Ins.Text = hidDiscountAmount.Value;
                    txtDiscountPercent_Ins.Enabled = false;
                }
                else
                {
                    txtDiscountPercent_Ins.Enabled = true;
                }

                if (Convert.ToDouble(hidDiscountPercent.Value) != 0)
                {
                    txtDiscountPercent_Ins.Text = hidDiscountPercent.Value;
                    txtDiscountAmount_Ins.Enabled = false;
                }
                else
                {
                    txtDiscountAmount_Ins.Enabled = true;
                }


                if (Convert.ToDouble(hidProductDiscountAmount.Value) != 0)
                {
                    txtProductDiscountAmount_Ins.Text = hidProductDiscountAmount.Value;
                    txtProductDiscountPercent_Ins.Enabled = false;
                }
                else
                {
                    txtProductDiscountPercent_Ins.Enabled = true;
                }

                if (Convert.ToDouble(hidProductDiscountPercent.Value) != 0)
                {
                    txtProductDiscountPercent_Ins.Text = hidProductDiscountPercent.Value;
                    txtProductDiscountAmount_Ins.Enabled = false;
                }
                else
                {
                    txtProductDiscountAmount_Ins.Enabled = true;
                }

                txtGroupPrice_Ins.Text = hidGroupPrice.Value;
                txtMinimumQty_Ins.Text = hidMinimumQty.Value;
                txtMinimumQtyTier2_Ins.Text = hidMinimumQtyTier2.Value;

                txtMinimumTotPrice_Ins.Text = hidMinimumTotPrice.Value;
                txtStartDate_Ins.Text = hidStartDate.Value;
                txtEndDate_Ins.Text = hidEndDate.Value;
                

                
                ddlLockAmountFlag_Ins.SelectedValue = (hidLockAmountFlag.Value == null || hidLockAmountFlag.Value == "") ? hidLockAmountFlag.Value = "-99" : hidLockAmountFlag.Value.Trim();
                ddlLockCheckbox_Ins.SelectedValue = (hidLockCheckbox.Value == null || hidLockCheckbox.Value == "") ? hidLockCheckbox.Value = "-99" : hidLockCheckbox.Value.Trim();
                
                if (ddlPromotionType_Ins.SelectedValue != StaticField.PromotionTypeCode16) 
                {
                    ddlMOQFlag_Ins.SelectedValue = (hidMOQFlag.Value == null || hidMOQFlag.Value == "") ? hidMOQFlag.Value = "-99" : hidMOQFlag.Value.Trim();
                }
                else
                {
                    ddlMOQFlag_Ins.SelectedValue = "N";
                }

                hidComplementaryFlag_Ins.Value = hidComplementaryFlag.Value;
                hidRedeemFlag_Ins.Value = hidRedeemFlag.Value;
                hidComplementaryChangeAble_Ins.Value = hidComplementaryChangeAble.Value;

                hidIdList.Value = hidPromotionId.Value;
                hidPromotionImgId.Value = GetPromotionImgByCriteria(hidPromotionCode.Value);
                hidFlagInsert.Value = "False";
                if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode21) 
                {
                   
                    rbtApplyScope.SelectedValue = hidApplyScope.Value;
                    rbtCriteriaType.SelectedValue = hidCriteriaType.Value;
                    ddlDisCountType.SelectedValue = hidDiscountType.Value;
                    txtOrderNumbers_Ins.Text = hidOrderNumbers.Value;
                    txtCriteriaValueTier1_Ins.Text = hidCriteriaValueTier1.Value;
                    txtCriteriaValueTier2_Ins.Text = hidCriteriaValueTier2.Value;
                    txtCriteriaValueTier3_Ins.Text = hidCriteriaValueTier3.Value;
                    txtDiscountValueTier1_Ins.Text = hidDiscountValueTier1.Value;
                    txtDiscountValueTier2_Ins.Text = hidDiscountValueTier2.Value;
                    txtDiscountValueTier3_Ins.Text = hidDiscountValueTier3.Value;
                    ddlDisCountType_SelectedIndexChanged(ddlDisCountType.SelectedValue, null);
                    rbtCriteriaType_SelectedIndexChanged(rbtCriteriaType.SelectedValue, null);
                    for (int i = 0; i < 2; i++)
                    {
                        btnAddTier_Click(null, null);
                    }
                }
               
                hidPicturePromotionUrl_Ins.Value = hidPicturePromotionUrl.Value;
                txtPromotionCode_Ins.Enabled = false;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);

            }

        }
        protected void btnTier2_Close(object sender, EventArgs e)
        {
            TierCharge = 1;
            Tier2Section.Visible = false;
            Tier3Section.Visible = false;
            txtCriteriaValueTier1_Ins.ReadOnly = false;
            txtDiscountValueTier1_Ins.ReadOnly = false;
            txtCriteriaValueTier2_Ins.Text = "";
            txtDiscountValueTier2_Ins.Text = "";
            txtCriteriaValueTier3_Ins.Text = "";
            txtDiscountValueTier3_Ins.Text = "";
        }
        protected void btnTier3_Close(object sender, EventArgs e)
        {
            TierCharge = 2;
            Tier3Section.Visible = false;
            txtCriteriaValueTier2_Ins.ReadOnly = false;
            txtDiscountValueTier2_Ins.ReadOnly = false;
            txtCriteriaValueTier3_Ins.Text = "";
            txtDiscountValueTier3_Ins.Text = "";
        }
       
        protected void btnAddTier_Click(object sender, EventArgs e)
        {
            
            if (txtCriteriaValueTier1_Ins.Text != "" && txtDiscountValueTier1_Ins.Text != "" && TierCharge < 2)
            {
                TierCharge++;
                txtCriteriaValueTier1_Ins.ReadOnly = true;
                txtDiscountValueTier1_Ins.ReadOnly = true;
                if (hidFlagInsert.Value == "True")
                {
                    double criteria_tier1 = double.Parse(txtCriteriaValueTier1_Ins.Text) + 1;
                    double discount_tier1 = double.Parse(txtDiscountValueTier1_Ins.Text) + 1;
                    txtCriteriaValueTier2_Ins.Text = criteria_tier1.ToString();
                    txtDiscountValueTier2_Ins.Text = discount_tier1.ToString();
                }
                
                Tier2Section.Visible = true;
            }
            else if (txtCriteriaValueTier2_Ins.Text != "" && txtDiscountValueTier2_Ins.Text != "" && TierCharge == 2)
            {
                TierCharge++;
                txtCriteriaValueTier1_Ins.ReadOnly = true;
                txtDiscountValueTier1_Ins.ReadOnly = true;
                txtCriteriaValueTier2_Ins.ReadOnly = true;
                txtDiscountValueTier2_Ins.ReadOnly = true;
                if (hidFlagInsert.Value == "True")
                {
                    double criteria_tier2 = double.Parse(txtCriteriaValueTier2_Ins.Text) + 1;
                    double discount_tier2 = double.Parse(txtDiscountValueTier2_Ins.Text) + 1;
                    txtCriteriaValueTier3_Ins.Text = criteria_tier2.ToString();
                    txtDiscountValueTier3_Ins.Text = discount_tier2.ToString();
                }
                Tier3Section.Visible = true;
            }
            else
            {
                bool flag = validateTier(true);
            }
        }
            protected void btnAddPromotion_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

            ddlFreeShipFlag_Ins.ClearSelection();
            ddlLockAmountFlag_Ins.ClearSelection();
            ddlLockCheckbox_Ins.ClearSelection();
            ddlMOQFlag_Ins.ClearSelection();
       
            ddlPromotionLevel_Ins.ClearSelection();
            ddlPromotionStatus_Ins.ClearSelection();
            ddlPromotionType_Ins.ClearSelection();
            ddlMultiSelectPromoTag_Ins.ClearSelection();
            chklistPromotionMapProductTag_Ins.ClearSelection();
            txtCombosetName_Ins.Text = "";
            txtDiscountAmount_Ins.Text = "";
            txtDiscountPercent_Ins.Text = "";
            txtEndDate_Ins.Text = "";
            txtGroupPrice_Ins.Text = "";
            txtMinimumQty_Ins.Text = "";
            txtMinimumTotPrice_Ins.Text = "";
            txtProductDiscountAmount_Ins.Text = "";
            txtProductDiscountPercent_Ins.Text = "";
            txtPromotionCode_Ins.Text = "";
            txtPromotionDesc_Ins.Text = "";
            txtPromotionName_Ins.Text = "";
            txtStartDate_Ins.Text = "";

            lblPromotionCode_Ins.Text = "";
            LbPromotionName_Ins.Text = "";
            lblPromotionLevel_Ins.Text = "";
            lblStartDate_Ins.Text = "";
            lblEndDate_Ins.Text = "";
            lblStartEnd_Ins.Text = "";
            lblPromotionStatus_Ins.Text = "";
            lblPromotionType_Ins.Text = "";
            lbllMultiSelectPromoTag_Ins.Text = "";
            lblPromotionMapProductTag_Ins.Text = "";

            ddlFreeShipFlag_Ins.Enabled = true;
            ddlLockCheckbox_Ins.Enabled = true;
            ddlMOQFlag_Ins.Enabled = true;
            txtPromotionCode_Ins.Enabled = true;
            txtProductDiscountAmount_Ins.Enabled = true;
            txtProductDiscountPercent_Ins.Enabled = true;
            PromotionDiscountSection.Visible = false;
            ProductDiscountSectionTier2.Visible = false;
            MOQSectionTier2.Visible = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);
        }
        protected void rbtCriteriaType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtCriteriaType.SelectedValue == StaticField.CriteriaType_AMOUNT) 
            {
                lblCriteriaValueHeader.InnerText = "หากมูลค่าการสั่งซื้อถึง(บาท)";
                
            }
            else if (rbtCriteriaType.SelectedValue == StaticField.CriteriaType_QUANTITY) 
            {
                lblCriteriaValueHeader.InnerText = "หากจำนวนรายการถึง";
                
            }
        }
        protected void ddlDisCountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDisCountType.SelectedValue == StaticField.DisCountType_money) 
            {
                lblDiscountValueHeader.InnerText = "ส่วนลดจะเป็น(บาท)";
            }
            else if (ddlDisCountType.SelectedValue == StaticField.DisCountType_discount) 
            {
                lblDiscountValueHeader.InnerText = "ส่วนลดจะเป็น(%)";
            }
        }
        protected void CancelTierSection()
        {
            TierCharge = 1;
            ddlDisCountType.ClearSelection();
            rbtCriteriaType.ClearSelection();
            rbtApplyScope.ClearSelection();
            FlexiComboSection.Visible = false;
            Tier1Section.Visible = false;
            txtCriteriaValueTier1_Ins.Text = "";
            txtDiscountValueTier1_Ins.Text = "";
            Tier2Section.Visible = false;
            txtCriteriaValueTier2_Ins.Text = "";
            txtDiscountValueTier2_Ins.Text = "";
            Tier3Section.Visible = false;
            txtCriteriaValueTier3_Ins.Text = "";
            txtDiscountValueTier3_Ins.Text = "";
        }
        protected void ddlPromoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPromotionType_Ins.SelectedValue != StaticField.PromotionTypeCode09) 
            {
                LbLowQty.Text = "Minimum unit of products (unit)";
            }
            else
            {

                LbLowQty.Text = "set number (item)";
            }

            

            // If Else replace for switch case section //
            if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode01) 
            {
                
                    //Free Shiping only
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.SelectedValue = "Y";
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = false;

                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    
                
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode02) 
            {
                
                    //individual discounted product price
                    CancelTierSection();


                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
                
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode03) 
            {
                
                    //product discount
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = true;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    
                
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode04) 
            {
                
                    //promotion discount
                    CancelTierSection();

                    GroupingSection.Visible = true;
                    PromotionDiscountSection.Visible = true;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";


                    ddlLockAmountFlag_Ins.SelectedValue = "Y";
                    ddlLockCheckbox_Ins.SelectedValue = "Y";
                    ddlMOQFlag_Ins.SelectedValue = "N";


                    ddlLockAmountFlag_Ins.Enabled = false;
                    ddlLockCheckbox_Ins.Enabled = false;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    
                
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode05) 
            {
                
                    //grouping
                    CancelTierSection();

                    GroupingSection.Visible = true;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";


                    ddlLockCheckbox_Ins.SelectedValue = "Y";
                    ddlMOQFlag_Ins.SelectedValue = "N";


                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = false;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    
                



            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode06) 
            {
                
                    //grouping with group price
                    CancelTierSection();

                    GroupingSection.Visible = true;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = true;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.ClearSelection();
                    ddlLockCheckbox_Ins.SelectedValue = "Y";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = false;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    
                


            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode07) 
            {
                
                    //MOQ + free shipping
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = true;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.SelectedValue = "Y";
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "Y";

                    ddlFreeShipFlag_Ins.Enabled = false;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    
                


            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode08) 
            {

                
                    //MOQ + product discount
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = true;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = true;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";


                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "Y";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    
                


            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode09) 
            {

                
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = true;
                    MOQSection.Visible = true;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";

                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "Y";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

                    

            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode10) 
            {
                
                    //minimum total price + promotion discount
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = true;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = true;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");

            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode11) 
            {
                    //minimum total price + redeem
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = true;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "Y";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");                

            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode12) 
            {
                    //minimum total price + giveaway
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = true;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "Y";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");


            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode13) 
            {
                    //normal price
                    CancelTierSection();
                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = false;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";


                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";


                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode14) 
            {
                    //combo set
                    CancelTierSection();
                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "Y";
                    ShowComboSection("Y");
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode15) 
            {
                    //จับกลุ่ม + ฟรีค่าขนส่ง
                    CancelTierSection();
                    GroupingSection.Visible = true;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.SelectedValue = "Y";
                    ddlLockAmountFlag_Ins.ClearSelection();
                    ddlLockCheckbox_Ins.SelectedValue = "Y";
                    ddlMOQFlag_Ins.SelectedValue = "N";
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlFreeShipFlag_Ins.Enabled = false;

                    ddlLockCheckbox_Ins.Enabled = false;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode16) 
            {
                    hidMOQFlagPromotion.Value = "N";//จับกลุ่ม + ลดราคา (ส่วนลดโปรโมชั่น)
                    CancelTierSection();


                    GroupingSection.Visible = true;
                    PromotionDiscountSection.Visible = true;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();

                    ddlLockCheckbox_Ins.SelectedValue = "Y";
                    ddlMOQFlag_Ins.SelectedValue = "N";
                    ddlLockAmountFlag_Ins.SelectedValue = "Y";
                    ddlLockAmountFlag_Ins.Enabled = false;
                    ddlFreeShipFlag_Ins.Enabled = true;

                    ddlLockCheckbox_Ins.Enabled = false;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode17) 
            {
                    //กำหนดยอดซื้อขั้นต่ำ + ฟรีค่าขนส่ง
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = true;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.SelectedValue = "Y";
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = false;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode18) 
            {
                    //Complementary Standard
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ComplementaryStandardSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtMinimumQtyTier2_Ins.Text = "0";
                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.SelectedValue = "Y";
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";
                    ddl_ComplementaryStandardChangeAble_Ins.SelectedValue = "N";


                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "Y";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode20) 
            {
                    //MOQ + product discount
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = true;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = true;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = true;
                    ProductDiscountSectionTier2.Visible = true;
                    MOQSectionTier2.Visible = true;

                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";


                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "Y";

                    ddlFreeShipFlag_Ins.Enabled = true;
                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
            }
            else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode21) 
            {
                    //MOQ + product discount
                    ddlDisCountType.SelectedValue = "money";
                    ddlDisCountType_SelectedIndexChanged(ddlDisCountType.SelectedValue, null);
                    rbtCriteriaType.SelectedValue = "AMOUNT";
                    rbtApplyScope.SelectedValue = "ENTIRE_STORE";
                    rbtCriteriaType_SelectedIndexChanged(rbtCriteriaType.SelectedValue, null);
                    TierCharge = 1;
                    FlexiComboSection.Visible = true;
                    Tier1Section.Visible = true;
                    txtCriteriaValueTier1_Ins.Text = "1";
                    txtDiscountValueTier1_Ins.Text = "5";
                    txtCriteriaValueTier1_Ins.ReadOnly = false;
                    txtDiscountValueTier1_Ins.ReadOnly = false;
                    txtCriteriaValueTier2_Ins.ReadOnly = false;
                    txtDiscountValueTier2_Ins.ReadOnly = false;
                    txtCriteriaValueTier3_Ins.ReadOnly = false;
                    txtDiscountValueTier3_Ins.ReadOnly = false;
                    Tier2Section.Visible = false;
                    Tier3Section.Visible = false;

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = false;
                    ProductDiscountSectionTier2.Visible = false;
                    MOQSectionTier2.Visible = false;

                    txtProductDiscountAmountTier2_Ins.Text = "0";
                    txtProductDiscountPercentTier2_Ins.Text = "0";
                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";

                    ddlFreeShipFlag_Ins.ClearSelection();
                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";

                    ddlFreeShipFlag_Ins.Enabled = false;
                    ddlLockAmountFlag_Ins.Enabled = false;
                    ddlLockCheckbox_Ins.Enabled = false;
                    ddlMOQFlag_Ins.Enabled = false;

                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
            }
            else //Other
            {
                    CancelTierSection();

                    GroupingSection.Visible = false;
                    PromotionDiscountSection.Visible = false;
                    ProductDiscountSection.Visible = false;
                    GroupPriceSection.Visible = false;
                    MOQSection.Visible = false;
                    MinimumTotPriceSection.Visible = false;
                    FreeShippingSection.Visible = false;
                    ComplementaryStandardSection.Visible = false;

                    txtDiscountAmount_Ins.Text = "0";
                    txtDiscountPercent_Ins.Text = "0";
                    txtProductDiscountAmount_Ins.Text = "0";
                    txtProductDiscountPercent_Ins.Text = "0";
                    txtGroupPrice_Ins.Text = "0";
                    txtMinimumQty_Ins.Text = "0";
                    txtMinimumTotPrice_Ins.Text = "0";


                    ddlLockAmountFlag_Ins.SelectedValue = "N";
                    ddlLockCheckbox_Ins.SelectedValue = "N";
                    ddlMOQFlag_Ins.SelectedValue = "N";


                    ddlLockAmountFlag_Ins.Enabled = true;
                    ddlLockCheckbox_Ins.Enabled = true;
                    ddlMOQFlag_Ins.Enabled = true;

                    hidComplementaryChangeAble_Ins.Value = "N";
                    hidRedeemFlag_Ins.Value = "N";
                    hidComplementaryFlag_Ins.Value = "N";
                    hidCombosetFlag_Ins.Value = "N";
                    ShowComboSection("N");
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChosenChange", "ChosenChangeSelectChanged()", true);
        }

        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        protected void ddlMultiSelectPromoTag_Ins_SelectChanged(object sender, EventArgs e)
        {
            Boolean flashsaleflag = false;

            foreach (ListItem listItem in ddlMultiSelectPromoTag_Ins.Items)
            {
                if (listItem.Selected)
                {
                    if (listItem.Value == StaticField.TagPromotion_03) 
                    {
                        flashsaleflag = true;
                    }
                    else
                    {
                        flashsaleflag = (flashsaleflag == false) ? false : true;
                    }
                }
            }

            if (flashsaleflag == true)
            {
                insertfalshsalesection.Visible = true;
            }
            else
            {
                insertfalshsalesection.Visible = false;
            }

            //Part Code conclude datetime to insert update flashsale
            string flashsalestartdate = "";
            string flashsalestarttime = "";
            string flashsaledatetime = "";
            flashsalestartdate = txtFlashSaleStartDate_Ins.Text;
            flashsalestarttime = txtFlashSaleStartTime_Ins.Text;

            flashsaledatetime = flashsalestartdate + " " + flashsalestarttime;
            //Part Code conclude datetime to insert update flashsale

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ChosenChange", "ChosenChangeSelectChanged()", true);
        }

        protected void ShowComboSection(string flag)
        {
            

        }

        protected void btnSavedraft_Click(object sender, EventArgs e)
        {
            hidFlagSavedraft.Value = "Y";
            hidFlagApprove.Value = "N";

            EmpInfo empInfo = new EmpInfo();

            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

            POInfo pInfo = new POInfo();

            string combosetName = (hidCombosetFlag_Ins.Value == "Y") ? txtPromotionName_Ins.Text : "";

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (validateInsert())
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {
                        HttpFileCollection uploadFiles = Request.Files;

                        for (int i = 0; i < uploadFiles.Count; i++)
                        {
                            HttpPostedFile postedFile = uploadFiles[i];
                            if (postedFile != null && postedFile.ContentLength > 0)
                            {
                                //Convert to Base64
                                Stream fs = postedFile.InputStream;
                                BinaryReader br = new BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                hidPicturePromotionUrl_Ins.Value = PromotionImgUrl + postedFile.FileName;

                                //Save Images
                                string respstring = "";

                                APIpath = APIUrl + "/api/support/InsertPromotionImage";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                    data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                    data["PromotionImageName"] = postedFile.FileName;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }

                                string APIpath1 = APIUrl + "/api/support/SavePromoPicfromjsonstring64/";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                    data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                    data["PromotionImageName"] = postedFile.FileName;
                                    data["PromotionImageBase64"] = base64String;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath1, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }
                            }
                        }

                        

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertPromotion";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionCode"] = txtPromotionCode_Ins.Text;
                            data["PromotionName"] = txtPromotionName_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["PromotionDesc"] = txtPromotionDesc_Ins.Text;
                            data["PromotionLevel"] = ddlPromotionLevel_Ins.SelectedValue;

                            data["FreeShippingCode"] = ddlFreeShipFlag_Ins.SelectedValue;
                            
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = ddlPromotionType_Ins.SelectedValue;

                            data["DiscountPercent"] = txtDiscountPercent_Ins.Text == null || txtDiscountPercent_Ins.Text == "" ? data["DiscountPercent"] = "0" : data["DiscountPercent"] = txtDiscountPercent_Ins.Text;
                            data["DiscountAmount"] = txtDiscountAmount_Ins.Text == null || txtDiscountAmount_Ins.Text == "" ? data["DiscountAmount"] = "0" : data["DiscountAmount"] = txtDiscountAmount_Ins.Text;
                            data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text == null || txtProductDiscountPercent_Ins.Text == "" ? data["ProductDiscountPercent"] = "0" : data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                            data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text == null || txtProductDiscountAmount_Ins.Text == "" ? data["ProductDiscountAmount"] = "0" : data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;
                            data["ProductDiscountPercentTier2"] = txtProductDiscountPercentTier2_Ins.Text == null || txtProductDiscountPercentTier2_Ins.Text == "" ? data["ProductDiscountPercentTier2"] = "0" : data["ProductDiscountPercentTier2"] = txtProductDiscountPercentTier2_Ins.Text;
                            data["ProductDiscountAmountTier2"] = txtProductDiscountAmountTier2_Ins.Text == null || txtProductDiscountAmountTier2_Ins.Text == "" ? data["ProductDiscountAmountTier2"] = "0" : data["ProductDiscountAmountTier2"] = txtProductDiscountAmountTier2_Ins.Text;
                            data["DefaultAmount"] = "0";

                            data["MOQFlag"] = ddlMOQFlag_Ins.SelectedValue;
                            data["MinimumQty"] = txtMinimumQty_Ins.Text;
                            data["MinimumQtyTier2"] = txtMinimumQtyTier2_Ins.Text;

                            data["LockCheckbox"] = ddlLockCheckbox_Ins.SelectedValue;
                            data["LockAmountFlag"] = ddlLockAmountFlag_Ins.SelectedValue;

                            data["MinimumTotPrice"] = txtMinimumTotPrice_Ins.Text;
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                            data["ComplementaryChangeAble"] = ddl_ComplementaryStandardChangeAble_Ins.SelectedValue;
                            data["GroupPrice"] = txtGroupPrice_Ins.Text;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] = "-99";

                            string lstrpromotiontagcode = "";
                            string lstrpromotiontagname = "";
                            foreach (ListItem listItem in ddlMultiSelectPromoTag_Ins.Items)
                            {
                                if (listItem.Selected)
                                {
                                    lstrpromotiontagcode += listItem.Value + ',';
                                    lstrpromotiontagname += listItem.Text + ',';
                                }
                            }
                            lstrpromotiontagcode = lstrpromotiontagcode.Remove(lstrpromotiontagcode.Length - 1);
                            lstrpromotiontagname = lstrpromotiontagname.Remove(lstrpromotiontagname.Length - 1);

                            data["PromotionTagCode"] = lstrpromotiontagcode;
                            data["PromotionTagName"] = lstrpromotiontagname;

                            string lstrproducttagcode = "";
                            string lstrproducttagname = "";
                            foreach (ListItem listItem in chklistPromotionMapProductTag_Ins.Items)
                            {
                                if (listItem.Selected)
                                {
                                    lstrproducttagcode += listItem.Value + ',';
                                    lstrproducttagname += listItem.Text + ',';
                                }
                            }

                            if (lstrproducttagcode.Length > 0)
                            {
                                lstrproducttagcode = lstrproducttagcode.Remove(lstrproducttagcode.Length - 1);
                            }

                            if (lstrproducttagname.Length > 0)
                            {
                                lstrproducttagname = lstrproducttagname.Remove(lstrproducttagname.Length - 1);
                            }

                            data["ProductTagCode"] = lstrproducttagcode;
                            data["ProductTagName"] = lstrproducttagname;

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;

                            data["CreateBy"] = empInfo.EmpCode;
                            data["MerchantCode"] = merchantinfo.MerchantCode;

                            data["Bu"] = hidBu.Value;
                            data["levels"] = hidLevels.Value;
                            data["RequestByEmpCode"] = hidEmpCode.Value;

                            //LazadaFlexicombo
                            data["ApplyScope"] = rbtApplyScope.SelectedValue; 
                            data["CriteriaType"] = rbtCriteriaType.SelectedValue;
                            data["DiscountType"] = ddlDisCountType.SelectedValue;
                            data["OrderNumbers"] = txtOrderNumbers_Ins.Text;
                            data["CriteriaValueTier1"] = txtCriteriaValueTier1_Ins.Text;
                            data["CriteriaValueTier2"] = txtCriteriaValueTier2_Ins.Text;
                            data["CriteriaValueTier3"] = txtCriteriaValueTier3_Ins.Text;
                            data["DiscountValueTier1"] = txtDiscountValueTier1_Ins.Text;
                            data["DiscountValueTier2"] = txtDiscountValueTier2_Ins.Text;
                            data["DiscountValueTier3"] = txtDiscountValueTier3_Ins.Text;                            

                            if (hidFlagSavedraft.Value == "Y")
                            {
                                data["wfStatus"] = "Savedraft";
                                data["wfFinishFlag"] = "No";
                            }

                            //Insert FlashSale StartDateTime and EndDateTime
                            string flashsalestartdate = "";
                            string flashsalestarttime = "";
                            string flashsalestartdatetime = "";
                            flashsalestartdate = txtFlashSaleStartDate_Ins.Text;
                            flashsalestarttime = txtFlashSaleStartTime_Ins.Text;

                            flashsalestartdatetime = flashsalestartdate + " " + flashsalestarttime;

                            data["PromotionFlashSaleStartDate"] = flashsalestartdatetime;

                            string flashsalesenddate = "";
                            string flashsaleendtime = "";
                            string flashsaleenddatetime = "";
                            flashsalesenddate = txtFlashSaleEndDate_Ins.Text;
                            flashsaleendtime = txtFlashSaleEndTime_Ins.Text;

                            flashsaleenddatetime = flashsalesenddate + " " + flashsaleendtime;

                            data["PromotionFlashSaleEndDate"] = flashsaleenddatetime;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            if (hidFlagSavedraft.Value == "Y")
                            {
                                
                            }

                            btnCancel_Click(null, null);

                            loadPromotion();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Promotion').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    else //Update
                    {
                        if (hidPromotionImgId.Value != "")
                        {
                            HttpFileCollection uploadFiles = Request.Files;

                            for (int i = 0; i < uploadFiles.Count; i++)
                            {
                                HttpPostedFile postedFile = uploadFiles[i];
                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Convert to Base64
                                    Stream fs = postedFile.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                    hidPicturePromotionUrl_Ins.Value = PromotionImgUrl + postedFile.FileName;

                                    //Save Images
                                    string respstring = "";

                                    APIpath = APIUrl + "/api/support/UpdatePromotionImage";

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                    string APIpath1 = APIUrl + "/api/support/SavePromoPicfromjsonstring64";
                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["PromotionImageBase64"] = base64String;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath1, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                }
                            }
                        }
                        else
                        {
                            HttpFileCollection uploadFiles = Request.Files;

                            for (int i = 0; i < uploadFiles.Count; i++)
                            {
                                HttpPostedFile postedFile = uploadFiles[i];
                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Convert to Base64
                                    Stream fs = postedFile.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                    hidPicturePromotionUrl_Ins.Value = PromotionImgUrl + postedFile.FileName;
                                    //Save Images
                                    string respstring = "";

                                    APIpath = APIUrl + "/api/support/InsertPromotionImage";

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                    string APIpath1 = APIUrl + "/api/support/SavePromoPicfromjsonstring64";
                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["PromotionCode"] = txtPromotionCode_Ins.Text;
                                        data["PromotionImageUrl"] = PromotionImgUrl + postedFile.FileName;
                                        data["PromotionImageName"] = postedFile.FileName;
                                        data["PromotionImageBase64"] = base64String;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath1, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                }
                            }

                        }

                          

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdatePromotion";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionId"] = hidIdList.Value;

                            data["PromotionCode"] = txtPromotionCode_Ins.Text;
                            data["PromotionName"] = txtPromotionName_Ins.Text;

                            data["FlagDelete"] = "N";
                            data["PromotionDesc"] = txtPromotionDesc_Ins.Text;
                            
                            data["FreeShippingCode"] = ddlFreeShipFlag_Ins.SelectedValue;
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = ddlPromotionType_Ins.SelectedValue;
                            data["PromotionLevel"] = ddlPromotionLevel_Ins.SelectedValue;

                            string lstrpromotiontagcode = "";
                            string lstrpromotiontagname = "";
                            foreach (ListItem listItem in ddlMultiSelectPromoTag_Ins.Items)
                            {
                                if (listItem.Selected)
                                {
                                    lstrpromotiontagcode += listItem.Value + ',';
                                    lstrpromotiontagname += listItem.Text + ',';
                                }
                            }
                            lstrpromotiontagcode = lstrpromotiontagcode.Remove(lstrpromotiontagcode.Length - 1);
                            lstrpromotiontagname = lstrpromotiontagname.Remove(lstrpromotiontagname.Length - 1);

                            data["PromotionTagCode"] = lstrpromotiontagcode;
                            data["PromotionTagName"] = lstrpromotiontagname;

                            string lstrproducttagcode = "";
                            string lstrproducttagname = "";
                            foreach (ListItem listItem in chklistPromotionMapProductTag_Ins.Items)
                            {
                                if (listItem.Selected)
                                {
                                    lstrproducttagcode += listItem.Value + ',';
                                    lstrproducttagname += listItem.Text + ',';
                                }
                            }

                            if (lstrproducttagcode.Length > 0)
                            {
                                lstrproducttagcode = lstrproducttagcode.Remove(lstrproducttagcode.Length - 1);
                            }

                            if (lstrproducttagname.Length > 0)
                            {
                                lstrproducttagname = lstrproducttagname.Remove(lstrproducttagname.Length - 1);
                            }

                            data["ProductTagCode"] = lstrproducttagcode;
                            data["ProductTagName"] = lstrproducttagname;

                            data["DiscountPercent"] = txtDiscountPercent_Ins.Text == null || txtDiscountPercent_Ins.Text == "" ? data["DiscountPercent"] = "0" : data["DiscountPercent"] = txtDiscountPercent_Ins.Text;
                            data["DiscountAmount"] = txtDiscountAmount_Ins.Text == null || txtDiscountAmount_Ins.Text == "" ? data["DiscountAmount"] = "0" : data["DiscountAmount"] = txtDiscountAmount_Ins.Text;
                            data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text == null || txtProductDiscountPercent_Ins.Text == "" ? data["ProductDiscountPercent"] = "0" : data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                            data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text == null || txtProductDiscountAmount_Ins.Text == "" ? data["ProductDiscountAmount"] = "0" : data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;
                            data["ProductDiscountPercentTier2"] = txtProductDiscountPercentTier2_Ins.Text == null || txtProductDiscountPercentTier2_Ins.Text == "" ? data["ProductDiscountPercentTier2"] = "0" : data["ProductDiscountPercentTier2"] = txtProductDiscountPercentTier2_Ins.Text;
                            data["ProductDiscountAmountTier2"] = txtProductDiscountAmountTier2_Ins.Text == null || txtProductDiscountAmountTier2_Ins.Text == "" ? data["ProductDiscountAmountTier2"] = "0" : data["ProductDiscountAmountTier2"] = txtProductDiscountAmountTier2_Ins.Text;

                            data["MOQFlag"] = ddlMOQFlag_Ins.SelectedValue;
                            data["MinimumQty"] = txtMinimumQty_Ins.Text;
                            data["MinimumQtyTier2"] = txtMinimumQtyTier2_Ins.Text;
                            data["DefaultAmount"] = "0";

                            data["LockCheckbox"] = ddlLockCheckbox_Ins.SelectedValue;
                            data["LockAmountFlag"] = ddlLockAmountFlag_Ins.SelectedValue;

                            data["MinimumTotPrice"] = txtMinimumTotPrice_Ins.Text;
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                            data["ComplementaryChangeAble"] = ddl_ComplementaryStandardChangeAble_Ins.SelectedValue;
                            data["GroupPrice"] = txtGroupPrice_Ins.Text;

                            data["CreateBy"] = empInfo.EmpCode;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] = "-99";

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;

                            data["ApplyScope"] = rbtApplyScope.SelectedValue;
                            data["CriteriaType"] = rbtCriteriaType.SelectedValue;
                            data["DiscountType"] = ddlDisCountType.SelectedValue;
                            data["OrderNumbers"] = txtOrderNumbers_Ins.Text;
                            data["CriteriaValueTier1"] = txtCriteriaValueTier1_Ins.Text;
                            data["CriteriaValueTier2"] = txtCriteriaValueTier2_Ins.Text;
                            data["CriteriaValueTier3"] = txtCriteriaValueTier3_Ins.Text;
                            data["DiscountValueTier1"] = txtDiscountValueTier1_Ins.Text;
                            data["DiscountValueTier2"] = txtDiscountValueTier2_Ins.Text;
                            data["DiscountValueTier3"] = txtDiscountValueTier3_Ins.Text;

                            //Insert FlashSale StartDateTime and EndDateTime
                            string flashsalestartdate = "";
                            string flashsalestarttime = "";
                            string flashsalestartdatetime = "";
                            flashsalestartdate = txtFlashSaleStartDate_Ins.Text;
                            flashsalestarttime = txtFlashSaleStartTime_Ins.Text;

                            flashsalestartdatetime = flashsalestartdate + " " + flashsalestarttime;

                            data["PromotionFlashSaleStartDate"] = flashsalestartdatetime;

                            string flashsalesenddate = "";
                            string flashsaleendtime = "";
                            string flashsaleenddatetime = "";
                            flashsalesenddate = txtFlashSaleEndDate_Ins.Text;
                            flashsaleendtime = txtFlashSaleEndTime_Ins.Text;

                            flashsaleenddatetime = flashsalesenddate + " " + flashsaleendtime;

                            data["PromotionFlashSaleEndDate"] = flashsaleenddatetime;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            List<PromotionDetailInfo> lPromoDetail = getPromoDetailList(txtPromotionCode_Ins.Text);

                            if (lPromoDetail.Count > 0)
                            {

                                string respstr2 = "";

                                APIpath = APIUrl + "/api/support/UpdatePromotionDetailDiscount";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["PromotionCode"] = txtPromotionCode_Ins.Text;


                                    data["DiscountPercent"] = txtProductDiscountPercent_Ins.Text == null || txtProductDiscountPercent_Ins.Text == "" ? data["ProductDiscountPercent"] = "0" : data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                                    data["DiscountAmount"] = txtProductDiscountAmount_Ins.Text == null || txtProductDiscountAmount_Ins.Text == "" ? data["ProductDiscountAmount"] = "0" : data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;


                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstr2 = Encoding.UTF8.GetString(response);
                                }

                                int? sum2 = JsonConvert.DeserializeObject<int?>(respstr2);


                                
                            }

                            btnCancel_Click(null, null);

                            loadPromotion();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Promotion').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }
        }        
        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"PromotionDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindddlPromotionLevel()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_PROMOTIONLEVEL; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlPromotionLevel_Ins.DataSource = lLookupInfo;

            ddlPromotionLevel_Ins.DataTextField = "LookupValue";

            ddlPromotionLevel_Ins.DataValueField = "LookupCode";

            ddlPromotionLevel_Ins.DataBind();

            ddlPromotionLevel_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlPromotionStatus()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_PROMOSTATUS; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlPromotionStatus_Ins.DataSource = lLookupInfo;

            ddlPromotionStatus_Ins.DataTextField = "LookupValue";

            ddlPromotionStatus_Ins.DataValueField = "LookupCode";

            ddlPromotionStatus_Ins.DataBind();

            ddlPromotionStatus_Ins.Items.Insert(0, new ListItem("---- Please select ----", "-99"));

        }

        protected void BindddlPromotionType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionType";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionTypeCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionTypeInfo> lLookupInfo = JsonConvert.DeserializeObject<List<PromotionTypeInfo>>(respstr);


            ddlPromotionType_Ins.DataSource = lLookupInfo;

            ddlPromotionType_Ins.DataTextField = "PromotionTypeName";

            ddlPromotionType_Ins.DataValueField = "PromotionTypeCode";

            ddlPromotionType_Ins.DataBind();

            ddlPromotionType_Ins.Items.Insert(0, new ListItem("---- Please select ----", "-99"));


        }

        protected void BindddlSearchPromotionLevel()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_PROMOTIONLEVEL; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlSearchPromotionLevel.DataSource = lLookupInfo;

            ddlSearchPromotionLevel.DataTextField = "LookupValue";

            ddlSearchPromotionLevel.DataValueField = "LookupCode";

            ddlSearchPromotionLevel.DataBind();

            ddlSearchPromotionLevel.Items.Insert(0, new ListItem("---- Please select ----", "-99"));

        }
        protected void BindddlLazadaDisCountType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_LAZADADISCOUNTTYPE; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlDisCountType.DataSource = lLookupInfo;

            ddlDisCountType.DataTextField = "LookupValue";

            ddlDisCountType.DataValueField = "LookupCode";

            ddlDisCountType.DataBind();

            ddlDisCountType.Items.Insert(0, new ListItem("---- Please select ----", "-99"));

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

        protected void BindddlSearchPromotionType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionType";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionTypeCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionTypeInfo> lLookupInfo = JsonConvert.DeserializeObject<List<PromotionTypeInfo>>(respstr);


            ddlSearchPromotionType.DataSource = lLookupInfo;

            ddlSearchPromotionType.DataTextField = "PromotionTypeName";

            ddlSearchPromotionType.DataValueField = "PromotionTypeCode";

            ddlSearchPromotionType.DataBind();

            ddlSearchPromotionType.Items.Insert(0, new ListItem("---- Please select ----", "-99"));


        }
        protected void BindddlPromotionTag()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_TAGPROMOTION; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlSearchPromotionTag.DataSource = lLookupInfo;
            ddlSearchPromotionTag.DataTextField = "LookupValue";
            ddlSearchPromotionTag.DataValueField = "LookupCode";
            ddlSearchPromotionTag.DataBind();
            ddlSearchPromotionTag.Items.Insert(0, new ListItem("---- Please select ----", "-99"));

            
        }
        protected List<PromotionInfo> ListPromotionMapPromotionTag(string pCode)
        {
            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionMapPromotionTagNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = pCode;
                data["MerchantCode"] = merchantinfo.MerchantCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lpromomappromotag = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lpromomappromotag;
        }
        protected List<LookupInfo> ListPromotionTagName(string lCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupCode"] = lCode;
                data["LookupType"] = StaticField.LookupType_TAGPROMOTION; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            return lLookupInfo;
        }
        protected List<LookupInfo> ListProductTagName(string lCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupCode"] = lCode;
                data["LookupType"] = StaticField.LookupType_TAGPRODUCT; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            return lLookupInfo;
        }
        protected void UpdateFlagDeletePromotionMapPromotionTag(string pCode, string mCode)
        {
            if (pCode != "" && mCode != "")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateFlagDeletePromotionMapPromotionTag";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["FlagDelete"] = "Y";
                    data["PromotionCode"] = pCode;
                    data["MerchantCode"] = mCode;
                    data["UpdateBy"] = hidEmpCode.Value;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            }
        }
        protected void BindddlMultiSelectPromoTag_Ins()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_TAGPROMOTION; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlMultiSelectPromoTag_Ins.DataSource = lLookupInfo;
            ddlMultiSelectPromoTag_Ins.DataTextField = "LookupValue";
            ddlMultiSelectPromoTag_Ins.DataValueField = "LookupCode";
            ddlMultiSelectPromoTag_Ins.DataBind();
        }
        protected void BindchklistPromotionMapProductTag_InsAndddlSearchProductTag()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_TAGPRODUCT; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            chklistPromotionMapProductTag_Ins.DataSource = lLookupInfo;
            chklistPromotionMapProductTag_Ins.DataTextField = "LookupValue";
            chklistPromotionMapProductTag_Ins.DataValueField = "LookupCode";
            chklistPromotionMapProductTag_Ins.DataBind();

            ddlSearchProductTag.DataSource = lLookupInfo;
            ddlSearchProductTag.DataTextField = "LookupValue";
            ddlSearchProductTag.DataValueField = "LookupCode";
            ddlSearchProductTag.DataBind();
            ddlSearchProductTag.Items.Insert(0, new ListItem("---- Please select ----", "-99"));
        }
        protected bool validateTier(bool flag)
        {
            if(TierCharge == 1 || TierCharge == 2 || TierCharge == 3)
            { 
                if (txtCriteriaValueTier1_Ins.Text == "")
                {
                    flag = false;
                    txtCriteriaValueTier1_Ins.Attributes.Add("placeholder", MessageConst._MSG_PLEASEINSERT + "Unit");
                    
                }
                if (txtDiscountValueTier1_Ins.Text == "")
                {
                    flag = false;
                    txtDiscountValueTier1_Ins.Attributes.Add("placeholder", MessageConst._MSG_PLEASEINSERT + "Discount");
                   
                }
            }
            if (TierCharge == 2 || TierCharge == 3)
            {
                if (txtCriteriaValueTier2_Ins.Text == "")
                {
                    flag = false;
                    txtCriteriaValueTier2_Ins.Attributes.Add("placeholder", MessageConst._MSG_PLEASEINSERT + "Unit");
                }
                else
                {
                    if (double.Parse(txtCriteriaValueTier2_Ins.Text) < double.Parse(txtCriteriaValueTier1_Ins.Text))
                    {
                        flag = false;
                        txtCriteriaValueTier2_Ins.Text = "";
                        txtCriteriaValueTier2_Ins.Attributes.Add("placeholder", "Unit Tier2 must not be less than Tier1");
                    }
                }
                if (txtDiscountValueTier2_Ins.Text == "")
                {
                    flag = false;
                    txtDiscountValueTier2_Ins.Attributes.Add("placeholder", MessageConst._MSG_PLEASEINSERT + "ส่วนลด");
                }
                else
                {
                    if (double.Parse(txtDiscountValueTier2_Ins.Text) < double.Parse(txtDiscountValueTier2_Ins.Text))
                    {
                        flag = false;
                        txtDiscountValueTier2_Ins.Text = "";
                        txtDiscountValueTier2_Ins.Attributes.Add("placeholder", "Discount Tier2 must not be less than Tier1");
                    }
                }
            }
            if (TierCharge == 3)
            {
                if (txtCriteriaValueTier3_Ins.Text == "")
                {
                    flag = false;
                    txtCriteriaValueTier3_Ins.Attributes.Add("placeholder", MessageConst._MSG_PLEASEINSERT + "Unit");
                }
                else
                {
                    if (double.Parse(txtCriteriaValueTier3_Ins.Text) < double.Parse(txtCriteriaValueTier2_Ins.Text) || double.Parse(txtCriteriaValueTier3_Ins.Text) < double.Parse(txtCriteriaValueTier1_Ins.Text))
                    {
                        flag = false;
                        txtCriteriaValueTier3_Ins.Text = "";
                        txtCriteriaValueTier3_Ins.Attributes.Add("placeholder", "Unit Tier3 must not be less than Tier2,Tier1");
                    }
                }
                if (txtDiscountValueTier3_Ins.Text == "")
                {
                    flag = false;
                    txtDiscountValueTier3_Ins.Attributes.Add("placeholder", MessageConst._MSG_PLEASEINSERT + "Discount");
                }
                else
                {
                    if (double.Parse(txtDiscountValueTier3_Ins.Text) < double.Parse(txtDiscountValueTier2_Ins.Text) || double.Parse(txtDiscountValueTier3_Ins.Text) < double.Parse(txtDiscountValueTier1_Ins.Text))
                    {
                        flag = false;
                        txtDiscountValueTier3_Ins.Text = "";
                        txtDiscountValueTier3_Ins.Attributes.Add("placeholder", "Discount Tier3 must not be less than Tier2,Tier1");
                    }
                }
            }
            return flag;
        }
            protected Boolean validateInsert()
        {
            Boolean flag = true;
            int? countitem = 0;
            int? counttagitem = 0;

            foreach (ListItem listItem in ddlMultiSelectPromoTag_Ins.Items)
            {
                if (listItem.Selected)
                {
                    countitem++;
                }
            }

            if (countitem > 1 && countitem <= 3)
            {
                flag = (flag == false) ? false : true;
                lbllMultiSelectPromoTag_Ins.Text = "";
            }
            if (countitem < 1)
            {
                flag = false;
                lbllMultiSelectPromoTag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " PromotionTag";
            }
            if (countitem > 3)
            {
                flag = false;
                lbllMultiSelectPromoTag_Ins.Text = "Imformation PromotionTag must not exceed 3 value";
            }

            

            if (txtPromotionCode_Ins.Text == "" || txtPromotionCode_Ins.Text == null)
            {
                flag = false;
                lblPromotionCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Promotion code";
            }
            else
            {

                if (CheckSymbol(txtPromotionCode_Ins.Text) == true)
                {
                    flag = false;
                    lblPromotionCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + "Promotion code Must not contain special characters";
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblPromotionCode_Ins.Text = "";
                }

                if (txtPromotionCode_Ins.Text != "")
                {
                    Boolean isDuplicate = ValidateDuplicate();

                    if (isDuplicate)
                    {
                        if (hidFlagInsert.Value == "True")
                        {
                            flag = false;
                            lblPromotionCode_Ins.Text = MessageConst._DATA_NComplete;
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                        }
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblPromotionCode_Ins.Text = "";
                    }
                }
            }




            if (txtPromotionName_Ins.Text == "")
            {
                flag = false;
                LbPromotionName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Promotion name";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbPromotionName_Ins.Text = "";
            }
            if (ddlPromotionLevel_Ins.SelectedValue == "-99" || ddlPromotionLevel_Ins.SelectedValue == "")
            {
                flag = false;
                lblPromotionLevel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Promotion level";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPromotionLevel_Ins.Text = "";
            }
            if (txtStartDate_Ins.Text == "")
            {
                flag = false;
                lblStartDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Promotion start date";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblStartDate_Ins.Text = "";
            }
            if (txtEndDate_Ins.Text == "")
            {
                flag = false;
                lblEndDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Promotion start date";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEndDate_Ins.Text = "";
            }

            if (ddlPromotionStatus_Ins.SelectedValue == "-99" || ddlPromotionStatus_Ins.SelectedValue == "")
            {
                flag = false;
                lblPromotionStatus_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Status";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPromotionStatus_Ins.Text = "";
            }
            if (ddlFreeShipFlag_Ins.SelectedValue == "-99" || ddlFreeShipFlag_Ins.SelectedValue == "")
            {
                if(ddlPromotionStatus_Ins.SelectedValue != "01")
                {
                    flag = false;
                    LbddlFreeShipFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Free shipping";
                }
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbddlFreeShipFlag_Ins.Text = "";
            }

            if (ddlPromotionType_Ins.SelectedValue == "-99" || ddlPromotionType_Ins.SelectedValue == "")
            {
                flag = false;
                lblPromotionType_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Promotion name";
            }
            else
            {
                if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode06 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode15 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode16) 
                {
                    if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode06) 
                    {
                        if (ddlLockAmountFlag_Ins.SelectedValue == "-99" || ddlLockAmountFlag_Ins.SelectedValue == "") 
                        {
                            flag = false;
                            LbddlLockAmountFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Edit the number of products";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            LbddlLockAmountFlag_Ins.Text = "";
                        }
                        if (txtGroupPrice_Ins.Text == "-99" || txtGroupPrice_Ins.Text == "")
                        {
                            flag = false;
                            lblGroupPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Edit the number of products";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblGroupPrice_Ins.Text = "";
                        }
                    }
                    if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode15) 
                    {

                        if (ddlLockAmountFlag_Ins.SelectedValue == "-99" || ddlLockAmountFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            LbddlLockAmountFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Edit the number of products";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            LbddlLockAmountFlag_Ins.Text = "";
                        }

                    }
                    if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode16) 
                    {

                        if (ddlLockAmountFlag_Ins.SelectedValue == "-99" || ddlLockAmountFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            LbddlLockAmountFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Edit the number of products";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            LbddlLockAmountFlag_Ins.Text = "";
                        }

                        if ((txtDiscountAmount_Ins.Text == "0" && txtDiscountPercent_Ins.Text == "0"))
                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (Bath)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (%)";

                        }
                        
                        else if ((txtDiscountAmount_Ins.Text == "0" || txtDiscountPercent_Ins.Text != "0")
                           || (txtDiscountAmount_Ins.Text != "0" || txtDiscountPercent_Ins.Text == "0"))
                        {
                            flag = (flag == false) ? false : true;
                            lblDiscountPercent_Ins.Text = "";
                            lblDiscountAmount_Ins.Text = "";
                        }

                        else
                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (บาท)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (%)";
                        }



                        

                    }
                }
                else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode07 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode08 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode09) 
                {
                    if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode07) 
                    {

                        if (ddlMOQFlag_Ins.SelectedValue == "-99" || ddlMOQFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            lblMOQFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Edit the number of products";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMOQFlag_Ins.Text = "";
                        }
                        if (txtMinimumQty_Ins.Text == "-99" || txtMinimumQty_Ins.Text == "")
                        {
                            flag = false;
                            lblMinimumQty_Ins.Text = MessageConst._MSG_PLEASEINSERT + " set minimum amount";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMinimumQty_Ins.Text = "";
                        }
                    }
                    else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode08) 
                    {
                        

                        if ((txtProductDiscountAmount_Ins.Text == "0" && txtProductDiscountPercent_Ins.Text == "0"))
                        {
                            flag = false;
                            lblProductDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (bath)";
                            lblProductDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (%)";

                        }
                        else if ((txtDiscountAmount_Ins.Text == "0" || txtDiscountPercent_Ins.Text != "0")
                           || (txtDiscountAmount_Ins.Text != "0" || txtDiscountPercent_Ins.Text == "0"))
                        {
                            flag = (flag == false) ? false : true;
                            lblProductDiscountPercent_Ins.Text = "";
                            lblProductDiscountAmount_Ins.Text = "";
                        }
                        else
                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + "  Select Discount(bath)/Discount(%)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Select Discount(bath)/Discount(%)";
                        }
                    }
                    else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode09) 
                    {
                        if (ddlMOQFlag_Ins.SelectedValue == "-99" || ddlMOQFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            lblMOQFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Edit the number of products";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMOQFlag_Ins.Text = "";
                        }
                        if (txtMinimumQty_Ins.Text == "0" || txtMinimumQty_Ins.Text == "")
                        {
                            flag = false;
                            lblMinimumQty_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Set a minimum purchase amount";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMinimumQty_Ins.Text = "";
                        }
                        if (txtGroupPrice_Ins.Text == "0" || txtGroupPrice_Ins.Text == "")
                        {
                            flag = false;
                            lblGroupPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Please enter price";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblGroupPrice_Ins.Text = "";
                        }
                        
                    }

                }

                else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode02 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode03) 
                {
                    if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode02) 
                    {
                        if (ddlFreeShipFlag_Ins.SelectedValue == "-99" || ddlFreeShipFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            LbddlFreeShipFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Free shipping";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            LbddlFreeShipFlag_Ins.Text = "";
                        }
                    }
                    else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode03) 
                    {
                        if ((txtProductDiscountAmount_Ins.Text == "0" && txtProductDiscountPercent_Ins.Text == "0"))

                        {
                            flag = false;
                            lblProductDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (บาท)";
                            lblProductDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (%)";

                        }
                        else if ((txtDiscountAmount_Ins.Text == "0" || txtDiscountPercent_Ins.Text != "0")
                           || (txtDiscountAmount_Ins.Text != "0" || txtDiscountPercent_Ins.Text == "0"))
                        {
                            flag = (flag == false) ? false : true;
                            lblProductDiscountPercent_Ins.Text = "";
                            lblProductDiscountAmount_Ins.Text = "";
                        }
                        else
                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + "  Select Discount(Bath)/Discount(%)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Select Discount(Bath)/Discount(%)";
                        }
                    }
                }
                else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode11 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode12 
                    || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode10 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode17) 
                {
                    if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode10) 
                    {
                        if ((txtDiscountAmount_Ins.Text == "0" && txtDiscountPercent_Ins.Text == "0"))

                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (Bath)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Discount (%)";

                        }
                        else if ((txtDiscountAmount_Ins.Text != "0" && txtDiscountPercent_Ins.Text != "0"))

                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + "  เลือก Discount(bath)/Discount(%)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือก Discount(bath)/Discount(%)";

                        }
                        else if ((txtDiscountAmount_Ins.Text == "0" || txtDiscountPercent_Ins.Text != "0")
                           || (txtDiscountAmount_Ins.Text != "0" || txtDiscountPercent_Ins.Text == "0"))
                        {
                            flag = (flag == false) ? false : true;
                            lblDiscountPercent_Ins.Text = "";
                            lblDiscountAmount_Ins.Text = "";
                        }


                        if (txtMinimumTotPrice_Ins.Text == "0" || txtMinimumTotPrice_Ins.Text == "")
                        {
                            flag = false;
                            lblMinimumTotPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Minimum purchase amount";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMinimumTotPrice_Ins.Text = "";
                        }
                    }
                    else
                    {
                        if (txtMinimumTotPrice_Ins.Text == "0" || txtMinimumTotPrice_Ins.Text == "")
                        {
                            flag = false;
                            lblMinimumTotPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Minimum purchase amount";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMinimumTotPrice_Ins.Text = "";
                        }
                    }
                }
                else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode00 || ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode01) //00||01
                {
                    flag = (flag == false) ? false : true;
                }
                else if (ddlPromotionType_Ins.SelectedValue == StaticField.PromotionTypeCode21) 
                {
                    if (rbtApplyScope.SelectedValue == "")
                    {
                        flag = false;
                        lblApplyScope_Ins.Text = MessageConst._MSG_PLEASESELECT + "scope";
                    }
                    if (rbtCriteriaType.SelectedValue == "")
                    {
                        flag = false;
                        lblCriteriaType_Ins.Text = MessageConst._MSG_PLEASESELECT + "Promotion conditions"; 
                    }
                    if (ddlDisCountType.SelectedValue == "" || ddlDisCountType.SelectedValue == "-99")
                    {
                        flag = false;
                        lblDisCountType.Text = MessageConst._MSG_PLEASESELECT + "Discount type";
                    }
                    if (txtOrderNumbers_Ins.Text == "" || txtOrderNumbers_Ins.Text == null)
                    {
                        flag = false;
                        lblOrderNumbers_Ins.Text = MessageConst._MSG_PLEASEINSERT + "number of orders";
                    }
                    flag = validateTier(flag);
                }


                flag = (flag == false) ? false : true;
                lblPromotionType_Ins.Text = "";
            }
            if (txtEndDate_Ins.Text != "" && txtStartDate_Ins.Text != "")
            {
                if (DateTime.ParseExact(txtEndDate_Ins.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.ParseExact(txtStartDate_Ins.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    flag = false;
                    lblStartEnd_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Correct start and end periods of the promotion.";
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblStartEnd_Ins.Text = "";
                }
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblStartEnd_Ins.Text = "";
            }

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);
            }

            return flag;
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
        public void StartWorkFlow(string promotioncode, string OStatus, string ostate)
        {
            EmpInfo empInfo = new EmpInfo();
            int? promotionid = 0;

            List<PromotionInfo> lpromoInfo = new List<PromotionInfo>();
            lpromoInfo = GetPromotionIDByCritreria(promotioncode);

            if(lpromoInfo.Count > 0)
            {
                promotionid = lpromoInfo[0].PromotionId;
            }

            empInfo = (EmpInfo)Session["EmpInfo"];
            APIpath = ConfigurationManager.AppSettings["K2API"];
            string userName = ConfigurationManager.AppSettings["K2User"];
            string passWord = ConfigurationManager.AppSettings["K2Password"];
            using (var client = new WebClient())
            {

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (send, certificate, chain, sslPolicyErrors) => { return true; };

                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(userName + ":" + passWord));
                client.Headers[HttpRequestHeader.Authorization] = string.Format(
                    "Basic {0}", credentials);

                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var olist = new k2Info();
                olist.folio = promotioncode;
                olist.expectedDuration = "86400";
                olist.priority = "1";
                
                olist.dataFields.PromotionID = promotionid;
                olist.dataFields.Event = "Submit";
                olist.dataFields.Actor = hidEmpCode.Value;
                olist.dataFields.Remark = txtPromotionDesc_Ins.Text.Trim();
                var jsonObj = JsonConvert.SerializeObject(olist);
                var dataString = client.UploadString(APIpath, jsonObj);

                
            }

        }

        public void InsertWorkFlow(string ordercode, string procInstId)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            APIpath = APIUrl + "/api/support/InsertK2Workflow";

            using (var client = new WebClient())
            {

                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var K2_OrderWF = new K2_OrderWFInfo();
                K2_OrderWF.OrderCode = ordercode;
                K2_OrderWF.procInstId = procInstId;
                K2_OrderWF.CreateBy = empInfo.EmpCode;
                var jsonObj = JsonConvert.SerializeObject(K2_OrderWF);
                var dataString = client.UploadString(APIpath, jsonObj);
            }
        }
        #endregion
    }
}
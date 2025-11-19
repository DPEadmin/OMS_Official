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
    public partial class PromotionPointManagement : System.Web.UI.Page
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
                   
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                btnSubmit.Visible = false;
                

                LoadEmpBuLevel(hidEmpCode.Value);
                loadPromotion();

                BindddlPromotionStatus();
                BindddlPropoint();
                BindddlPointType();
                BindddlPointRange();
                BindddlCompany();
                
                ddlCombosetFlag_Ins_SelectedIndexChanged(null, null);
                ddlPointType_Ins_SelectedIndexChanged(null, null);
                ddlPatent_Ins_SelectedIndexChanged(null, null);
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

            gvPromotion.DataSource = lPromotionInfo;

            gvPromotion.DataBind();


            

        }

        public List<PromotionInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            
            APIpath = APIUrl + "/api/support/ListPromotionPointList";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = "-99";

                data["PromotionTypeCode"] = "19";

                data["MerchantCode"] = hidMerchantCode.Value;

                data["PromotionCode"] = txtSearchPromotionCode.Text;

                data["PromotionName"] = txtSearchPromotionName.Text;

                data["StartDate"] = txtSearchStartDateFrom.Text;

                data["StartDateTo"] = txtSearchStartDateTo.Text;

                data["EndDate"] = txtSearchEndDateFrom.Text;

                data["EndDateTo"] = txtSearchEndDateTo.Text;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                data["Propoint"] = ddlPropointSearch.SelectedValue;

                data["PointType"] = ddlPointTypeSearch.SelectedValue;

                data["PointRangeCode"] = ddlPointRangeSearch.SelectedValue;

                data["CompanyCode"] = ddlCompanySearch.SelectedValue;

                data["FlagPointType"] = "Y";

                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            return lPromotionInfo;

        }

        public int? CountPromotionMasterList()
        {
            string respstr = "";

            
            APIpath = APIUrl + "/api/support/CountListPromotionPoint";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] ="-99";

                data["PromotionTypeCode"] = StaticField.PromotionTypeCode19; 

                data["PromotionCode"] = txtSearchPromotionCode.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["PromotionName"] = txtSearchPromotionName.Text;

                data["StartDate"] = txtSearchStartDateFrom.Text;

                data["StartDateTo"] = txtSearchStartDateTo.Text;

                data["EndDate"] = txtSearchEndDateFrom.Text;

                data["EndDateTo"] = txtSearchEndDateTo.Text;

                data["Propoint"] = ddlPropointSearch.SelectedValue;

                data["PointType"] = ddlPointTypeSearch.SelectedValue;

                data["PointRangeCode"] = ddlPointRangeSearch.SelectedValue;

                data["CompanyCode"] = ddlCompanySearch.SelectedValue;

                data["FlagPointType"] = "Y";
                

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
                        Codelist += ", '" + hidCode.Value + "' ";
                    }
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                        Codelist += " '" + hidCode.Value + "' ";
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
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
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

                            
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = StaticField.PromotionTypeCode19; 

                            data["PromotionLevel"] = "MAIN";
                          
                            data["DefaultAmount"] = "0";

                        

                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
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

                            data["Propoint"] = ddlPropoint.SelectedValue.Trim();
                            data["PointType"] = ddlPointType.SelectedValue.Trim();
                            data["CompanyCode"] = ddlCompany.SelectedValue.Trim();
                            data["FlagPatent"] = ddlPatent.SelectedValue.Trim();
                            data["PatentAmount"] = txtpatentnum.Text.Trim();
                            data["DiscountCode"] = txtDiscountCode.Text.Trim();
                            data["PointRangeCode"] = ddlPointRange.SelectedValue.Trim();
                            data["FlagPointType"] = "Y";

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
                            
                          
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = StaticField.PromotionTypeCode19; 


                            data["DefaultAmount"] = "0";

                       
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                          
                            data["CreateBy"] = empInfo.EmpCode;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] =  "-99";

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;

                            data["Propoint"] = ddlPropoint.SelectedValue.Trim();
                            data["PointType"] = ddlPointType.SelectedValue.Trim();
                            data["CompanyCode"] = ddlCompany.SelectedValue.Trim();
                            data["FlagPatent"] = ddlPatent.SelectedValue.Trim();
                            data["PatentAmount"] = txtpatentnum.Text.Trim();
                            data["DiscountCode"] = txtDiscountCode.Text.Trim();
                            data["PointRangeCode"] = ddlPointRange.SelectedValue.Trim();

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        
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
            txtPromotionCode_Ins.Text = "";
            txtPromotionName_Ins.Text = "";
            txtPromotionDesc_Ins.Text = "";
           
            
            ddlPromotionStatus_Ins.SelectedValue = "-99";
            
            hidPicturePromotionUrl_Ins.Value = null;

            ddlPropoint.ClearSelection();
            ddlPointType.ClearSelection();
            ddlCompany.ClearSelection();
            ddlPatent.ClearSelection();
            txtpatentnum.Text = "";
            txtDiscountCode.Text = "";
            ddlPointRange.ClearSelection();

            ddlPointType_Ins_SelectedIndexChanged(null, null);
            ddlPatent_Ins_SelectedIndexChanged(null, null);

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
          
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
            txtSearchEndDateFrom.Text = "";
            txtSearchEndDateTo.Text = "";
            ddlPropointSearch.ClearSelection();
            ddlPointTypeSearch.ClearSelection();
            ddlCompanySearch.ClearSelection();
            ddlPointRangeSearch.ClearSelection();
        }

        protected void gvPromotion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPromotion.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

          
            lblStartEnd_Ins.Text = "";

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

            HiddenField hidMOQFlag = (HiddenField)row.FindControl("hidMOQFlag");
            HiddenField hidMinimumQty = (HiddenField)row.FindControl("hidMinimumQty");
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

            HiddenField hidPropoint = (HiddenField)row.FindControl("hidPropoint");
            HiddenField hidPointType = (HiddenField)row.FindControl("hidPointType");
            HiddenField hidCompanyCode = (HiddenField)row.FindControl("hidCompanyCode");
            HiddenField hidFlagPatent = (HiddenField)row.FindControl("hidFlagPatent");
            HiddenField hidPatentAmount = (HiddenField)row.FindControl("hidPatentAmount");
            HiddenField hidDiscountCode = (HiddenField)row.FindControl("hidDiscountCode");
            HiddenField hidPointRangeCode = (HiddenField)row.FindControl("hidPointRangeCode");


            if (e.CommandName == "ShowPromotion")
            {
                ddlPropoint.SelectedValue = (hidPropoint.Value == null || hidPropoint.Value == "") ? hidPropoint.Value = "-99" : hidPropoint.Value;
                ddlPointType.SelectedValue = (hidPointType.Value == null || hidPointType.Value == "") ? hidPointType.Value = "-99" : hidPointType.Value;
                txtDiscountCode.Text = hidDiscountCode.Value.Trim();
                ddlCompany.SelectedValue = (hidCompanyCode.Value == null || hidCompanyCode.Value == "") ? hidCompanyCode.Value = "-99" : hidCompanyCode.Value;
                ddlPatent.SelectedValue = (hidFlagPatent.Value == null || hidFlagPatent.Value == "") ? hidFlagPatent.Value = "-99" : hidFlagPatent.Value;
                txtpatentnum.Text = hidPatentAmount.Value.Trim();
                ddlPointRange.SelectedValue = (hidPointRangeCode.Value == null || hidPointRangeCode.Value == "") ? hidPointRangeCode.Value = "-99" : hidPointRangeCode.Value;

                txtPromotionCode_Ins.Text = hidPromotionCode.Value;
                
                hidPromotionCode_Ins.Value = hidPromotionCode.Value;
                txtPromotionName_Ins.Text = hidPromotionName.Value;
                txtPromotionDesc_Ins.Text = hidPromotionDesc.Value;
                
                ddlPromotionStatus_Ins.SelectedValue = (hidStatus.Value == null || hidStatus.Value == "") ? hidStatus.Value = "-99" : hidStatus.Value;
          
                





              
                txtStartDate_Ins.Text = hidStartDate.Value;
                txtEndDate_Ins.Text = hidEndDate.Value;
                
                

                hidComplementaryFlag_Ins.Value = hidComplementaryFlag.Value;
                hidRedeemFlag_Ins.Value = hidRedeemFlag.Value;
                hidComplementaryChangeAble_Ins.Value = hidComplementaryChangeAble.Value;

                hidIdList.Value = hidPromotionId.Value;

                hidPromotionImgId.Value = GetPromotionImgByCriteria(hidPromotionCode.Value);
                hidFlagInsert.Value = "False";
                hidPicturePromotionUrl_Ins.Value = hidPicturePromotionUrl.Value;
                
                ddlPointType_Ins_SelectedIndexChanged(null, null);
                ddlPatent_Ins_SelectedIndexChanged(null, null);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);

            }

        }

        protected void btnAddPromotion_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

 
            ddlPromotionStatus_Ins.ClearSelection();
   
            txtEndDate_Ins.Text = "";
  
            txtPromotionCode_Ins.Text = "";
            txtPromotionDesc_Ins.Text = "";
            txtPromotionName_Ins.Text = "";
            txtStartDate_Ins.Text = "";

            ddlPropoint.ClearSelection();
            ddlPointType.ClearSelection();
            ddlCompany.ClearSelection();
            ddlPatent.ClearSelection();
            txtpatentnum.Text = "";
            txtDiscountCode.Text = "";
            ddlPointRange.ClearSelection();


     
            txtPromotionCode_Ins.Enabled = true;


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);
        }

        protected void ddlPatent_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPatent.SelectedValue == "01") //Bind static from aspx
            {
                txtpatentnum.Enabled = true;
            }
            else
            {
                txtpatentnum.Enabled = false;
            }

        }
        protected void ddlPointType_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPointType.SelectedValue != StaticField.PointType01 && ddlPointType.SelectedValue != "-99") 
            {
                DiscountCode_lbl.Visible = true;
                DiscountCode_div.Visible = true;
            
            }
            else
            {
                DiscountCode_lbl.Visible = false;
                DiscountCode_div.Visible = false;
            }

        }
        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
            

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

                         
                            
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = StaticField.PromotionTypeCode19; 

                           
                            data["DefaultAmount"] = "0";

                          
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                          
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

                            data["Propoint"] = ddlPropoint.SelectedValue.Trim();
                            data["PointType"] = ddlPointType.SelectedValue.Trim();
                            data["CompanyCode"] = ddlCompany.SelectedValue.Trim();
                            data["FlagPatent"] = ddlPatent.SelectedValue.Trim();
                            data["PatentAmount"] = txtpatentnum.Text.Trim();
                            data["DiscountCode"] = txtDiscountCode.Text.Trim(); 
                            data["PointRangeCode"] = ddlPointRange.SelectedValue.Trim();
                            data["FlagPointType"] = "Y";

                            if (hidFlagSavedraft.Value == "Y")
                            {
                                data["wfStatus"] = "Savedraft";
                                data["wfFinishFlag"] = "No";
                            }

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

                           

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Promotion').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                        loadPromotion();

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
                            
                         
                            data["PromotionStatusCode"] = ddlPromotionStatus_Ins.SelectedValue;
                            data["PromotionTypeCode"] = StaticField.PromotionTypeCode19; 


                           

                            
                            data["DefaultAmount"] = "0";

                          
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                           

                            data["CreateBy"] = empInfo.EmpCode;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] = "-99";

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;

                            data["Propoint"] = ddlPropoint.SelectedValue.Trim();
                            data["PointType"] = ddlPointType.SelectedValue.Trim();
                            data["CompanyCode"] = ddlCompany.SelectedValue.Trim();
                            data["FlagPatent"] = ddlPatent.SelectedValue.Trim();
                            data["PatentAmount"] = txtpatentnum.Text.Trim();
                            data["DiscountCode"] = txtDiscountCode.Text.Trim();
                            data["PointRangeCode"] = ddlPointRange.SelectedValue.Trim();

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            

                            btnCancel_Click(null, null);

                            

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

                loadPromotion();
            }
        }        
        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"PromotionPointDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }
        protected string GetBuyerDetail(object objCode,object objName)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            string strName = (objName != null) ? objName.ToString() : "";
            return "<a href=\"ReportPromotionPoint.aspx?PromotionCode=" + strCode + "&PromotionName=" + strName + "\">" + "รายละเอียดการขาย" + "</a>";
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

            ddlPromotionStatus_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

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


            ddlPropoint.DataSource = lLookupInfo;

            ddlPropoint.DataTextField = "LookupValue";

            ddlPropoint.DataValueField = "LookupCode";

            ddlPropoint.DataBind();

            ddlPropoint.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));


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


            ddlPointRange.DataSource = lPointInfo;

            ddlPointRange.DataTextField = "PointName";

            ddlPointRange.DataValueField = "PointCode";

            ddlPointRange.DataBind();

            ddlPointRange.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlPointRange.Items.Insert(1, new ListItem("ทุกระดับ", "ALL"));

            ddlPointRangeSearch.DataSource = lPointInfo;

            ddlPointRangeSearch.DataTextField = "PointName";

            ddlPointRangeSearch.DataValueField = "PointCode";

            ddlPointRangeSearch.DataBind();

            ddlPointRangeSearch.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlPointRangeSearch.Items.Insert(1, new ListItem("ทุกระดับ", "ALL"));

        }
        protected void BindddlPointType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "POINTTYPE";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlPointType.DataSource = lLookupInfo;

            ddlPointType.DataTextField = "LookupValue";

            ddlPointType.DataValueField = "LookupCode";

            ddlPointType.DataBind();

            ddlPointType.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

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


            ddlCompany.DataSource = lLookupInfo;

            ddlCompany.DataTextField = "CompanyNameTH";

            ddlCompany.DataValueField = "CompanyCode";

            ddlCompany.DataBind();

            ddlCompany.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

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

        protected Boolean validateInsert()
        {
            Boolean flag = true;


            

            if (txtPromotionCode_Ins.Text == "" || txtPromotionCode_Ins.Text == null)
            {
                flag = false;
                lblPromotionCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสโปรโมชั่น";
            }
            else
            {

                if (CheckSymbol(txtPromotionCode_Ins.Text) == true)
                {
                    flag = false;
                    lblPromotionCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสโปรโมชั่นต้องไม่มีอักขระพิเศษ";
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
                LbPromotionName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อโปรโมชั่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbPromotionName_Ins.Text = "";
            }
            if (ddlPropoint.SelectedValue == "-99")
            {
                flag = false;
                lblPropoint.Text = MessageConst._MSG_PLEASESELECT + " หมวดหมู่";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPropoint.Text = "";

            }
            if (ddlPointType.SelectedValue == "-99")
            {
                flag = false;
                lblPointType.Text = MessageConst._MSG_PLEASESELECT + " ประเภท";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPointType.Text = "";

            }
            if (ddlPointRange.SelectedValue == "-99")
            {
                flag = false;
                lblPointRange.Text = MessageConst._MSG_PLEASESELECT + " ระดับ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPointRange.Text = "";

            }
            if (ddlCompany.SelectedValue == "-99")
            {
                flag = false;
                lblCompany.Text = MessageConst._MSG_PLEASESELECT + " ร้านค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCompany.Text = "";

            }
            if (ddlPatent.SelectedValue == "-99")
            {
                flag = false;
                lblpatent.Text = MessageConst._MSG_PLEASESELECT + " จำนวนการเรียกใช้สิทธิ์";
            }
            else
            {

                if (ddlPatent.SelectedValue == "01")
                {
                    if (txtpatentnum.Text == "")
                    {
                        flag = false;
                        lblpatentnum.Text = MessageConst._MSG_PLEASEINSERT + " จำนวนสิทธิ์";
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblpatentnum.Text = "";
                    }
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblpatent.Text = "";
                    lblpatentnum.Text = "";
                }

            }

            if (txtStartDate_Ins.Text == "")
            {
                flag = false;
                lblStartDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " วันเริ่มโปรโมชั่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblStartDate_Ins.Text = "";
            }
            if (txtEndDate_Ins.Text == "")
            {
                flag = false;
                lblEndDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " วันเริ่มโปรโมชั่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEndDate_Ins.Text = "";
            }

            if (ddlPromotionStatus_Ins.SelectedValue == "-99" || ddlPromotionStatus_Ins.SelectedValue == "")
            {
                flag = false;
                lblPromotionStatus_Ins.Text = MessageConst._MSG_PLEASEINSERT + " สถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPromotionStatus_Ins.Text = "";
            }

            if (txtEndDate_Ins.Text != "" && txtStartDate_Ins.Text != "")
            {
                if (DateTime.ParseExact(txtEndDate_Ins.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture) < DateTime.ParseExact(txtStartDate_Ins.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                {
                    flag = false;
                    lblStartEnd_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ช่วงเวลาเริ่มและสิ้นสุดของโปรโมชั่นให้ถูกต้อง";
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
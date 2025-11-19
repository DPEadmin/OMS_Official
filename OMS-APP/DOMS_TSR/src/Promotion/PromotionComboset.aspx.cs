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
    public partial class PromotionComboset : System.Web.UI.Page
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

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                loadPromotion();

                BindddlPromotionLevel();
                BindddlPromotionStatus();
                BindddlPromotionType();
                BindddlSearchProductBrand();
                BindddlSearchPromotionLevel();
                BindddlProductBrand();
                BindddlSearchPromotionType();
                ddlPromoType_SelectedIndexChanged(null, null);
                ddlCombosetFlag_Ins_SelectedIndexChanged(null, null);
                ShowComboSection("N");

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

            APIpath = APIUrl + "/api/support/ListPromotionCombosetList";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = ddlSearchProductBrand.SelectedValue;

                data["PromotionTypeCode"] = ddlSearchPromotionType.SelectedValue;

                data["PromotionCode"] = txtSearchPromotionCode.Text;

                data["PromotionName"] = txtSearchPromotionName.Text;

                data["PromotionLevel"] = ddlSearchPromotionLevel.SelectedValue;

                data["StartDate"] = txtSearchStartDateFrom.Text;

                data["StartDateTo"] = txtSearchStartDateTo.Text;

                data["EndDate"] = txtSearchEndDateFrom.Text;

                data["EndDateTo"] = txtSearchEndDateTo.Text;

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

            APIpath = APIUrl + "/api/support/CountListPromotionComboset";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = ddlSearchProductBrand.SelectedValue;

                data["PromotionTypeCode"] = ddlSearchPromotionType.SelectedValue;

                data["PromotionCode"] = txtSearchPromotionCode.Text;

                data["PromotionName"] = txtSearchPromotionName.Text;

                data["PromotionLevel"] = ddlSearchPromotionLevel.SelectedValue;

                data["StartDate"] = txtSearchStartDateFrom.Text;

                data["StartDateTo"] = txtSearchStartDateTo.Text;

                data["EndDate"] = txtSearchEndDateFrom.Text;

                data["EndDateTo"] = txtSearchEndDateTo.Text;

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

            EmpInfo empInfo = new EmpInfo();

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

                            data["DiscountPercent"] = txtDiscountPercent_Ins.Text;
                            data["DiscountAmount"] = txtDiscountAmount_Ins.Text;
                            data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                            data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;
                            data["DefaultAmount"] = "0";

                            data["MOQFlag"] = ddlMOQFlag_Ins.SelectedValue;
                            data["MinimumQty"] = txtMinimumQty_Ins.Text;

                            data["LockCheckbox"] = ddlLockCheckbox_Ins.SelectedValue;
                            data["LockAmountFlag"] = ddlLockAmountFlag_Ins.SelectedValue;

                            data["MinimumTotPrice"] = txtMinimumTotPrice_Ins.Text;
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                            data["GroupPrice"] = txtGroupPrice_Ins.Text;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] = ddlProductBrand_Ins.SelectedValue;

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;


                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


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


                            data["DiscountPercent"] = txtDiscountPercent_Ins.Text;
                            data["DiscountAmount"] = txtDiscountAmount_Ins.Text;
                            data["ProductDiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                            data["ProductDiscountAmount"] = txtProductDiscountAmount_Ins.Text;

                            data["MOQFlag"] = ddlMOQFlag_Ins.SelectedValue;
                            data["MinimumQty"] = txtMinimumQty_Ins.Text;
                            data["DefaultAmount"] = "0";

                            data["LockCheckbox"] = ddlLockCheckbox_Ins.SelectedValue;
                            data["LockAmountFlag"] = ddlLockAmountFlag_Ins.SelectedValue;

                            data["MinimumTotPrice"] = txtMinimumTotPrice_Ins.Text;
                            data["RedeemFlag"] = hidRedeemFlag_Ins.Value;
                            data["ComplementaryFlag"] = hidComplementaryFlag_Ins.Value;
                            data["GroupPrice"] = txtGroupPrice_Ins.Text;

                            data["CreateBy"] = empInfo.EmpCode;
                            data["PicturePromotionUrl"] = hidPicturePromotionUrl_Ins.Value;

                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["ProductBrandCode"] = ddlProductBrand_Ins.SelectedValue;

                            data["CombosetFlag"] = hidCombosetFlag_Ins.Value;
                            data["CombosetName"] = combosetName;

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


                                    data["DiscountPercent"] = txtProductDiscountPercent_Ins.Text;
                                    data["DiscountAmount"] = txtProductDiscountAmount_Ins.Text;


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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
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
            
            hidPicturePromotionUrl_Ins.Value = null;

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
            ddlSearchProductBrand.SelectedValue = "-99";
            ddlSearchPromotionLevel.SelectedValue = "-99";
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
            txtSearchEndDateFrom.Text = "";
            txtSearchEndDateTo.Text = "";
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
            HiddenField hidPicturePromotionUrl = (HiddenField)row.FindControl("hidPicturePromotionUrl");
            HiddenField hidCombosetFlag = (HiddenField)row.FindControl("hidCombosetFlag");
            HiddenField hidCombosetName = (HiddenField)row.FindControl("hidCombosetName");
            HiddenField hidStartDate = (HiddenField)row.FindControl("hidStartDate");
            HiddenField hidEndDate = (HiddenField)row.FindControl("hidEndDate");
            HiddenField hidProductBrandCode = (HiddenField)row.FindControl("hidProductBrandCode");



            if (e.CommandName == "ShowPromotion")
            {


                txtPromotionCode_Ins.Text = hidPromotionCode.Value;
                
                hidPromotionCode_Ins.Value = hidPromotionCode.Value;
                txtPromotionName_Ins.Text = hidPromotionName.Value;
                txtPromotionDesc_Ins.Text = hidPromotionDesc.Value;
                ddlPromotionLevel_Ins.SelectedValue = (hidPromotionLevel.Value == null || hidPromotionLevel.Value == "") ? hidPromotionLevel.Value = "-99" : hidPromotionLevel.Value.Trim();
                ddlPromotionStatus_Ins.SelectedValue = (hidStatus.Value == null || hidStatus.Value == "") ? hidStatus.Value = "-99" : hidStatus.Value;
                ddlProductBrand_Ins.SelectedValue = (hidProductBrandCode.Value == null || hidProductBrandCode.Value == "") ? hidProductBrandCode.Value = "-99" : hidProductBrandCode.Value;

                ddlPromotionType_Ins.SelectedValue = (hidPromotionType.Value == null || hidPromotionType.Value == "") ? hidPromotionType.Value = "-99" : hidPromotionType.Value;
                ddlPromoType_SelectedIndexChanged(ddlPromotionType_Ins.SelectedValue, null);
                ddlFreeShipFlag_Ins.SelectedValue = (hidFreeShipping.Value == null || hidFreeShipping.Value == "") ? hidFreeShipping.Value = "-99" : hidFreeShipping.Value.Trim();
                

                txtDiscountAmount_Ins.Text = hidDiscountAmount.Value;
                txtDiscountPercent_Ins.Text = hidDiscountPercent.Value;
                txtProductDiscountAmount_Ins.Text = hidProductDiscountAmount.Value;
                txtProductDiscountPercent_Ins.Text = hidProductDiscountPercent.Value;
                txtGroupPrice_Ins.Text = hidGroupPrice.Value;
                txtMinimumQty_Ins.Text = hidMinimumQty.Value;
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

                hidIdList.Value = hidPromotionId.Value;

                hidPromotionImgId.Value = GetPromotionImgByCriteria(hidPromotionCode.Value);
                hidFlagInsert.Value = "False";
                hidPicturePromotionUrl_Ins.Value = hidPicturePromotionUrl.Value;
                txtPromotionCode_Ins.Enabled = false;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);

            }

        }

        protected void btnAddPromotion_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

            ddlFreeShipFlag_Ins.ClearSelection();
            ddlLockAmountFlag_Ins.ClearSelection();
            ddlLockCheckbox_Ins.ClearSelection();
            ddlMOQFlag_Ins.ClearSelection();
            ddlProductBrand_Ins.ClearSelection();
            ddlPromotionLevel_Ins.ClearSelection();
            ddlPromotionStatus_Ins.ClearSelection();
            ddlPromotionType_Ins.ClearSelection();
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

            ddlFreeShipFlag_Ins.Enabled = true;
            ddlLockCheckbox_Ins.Enabled = true;
            ddlMOQFlag_Ins.Enabled = true;
            txtPromotionCode_Ins.Enabled = true;
            PromotionDiscountSection.Visible = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);
        }

        protected void ddlPromoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPromotionType_Ins.SelectedValue != StaticField.PromotionTypeCode19) 
            {
                LbLowQty.Text = "จำนวนสินค้าขั้นต่ำ (หน่วย)";
            }
            else
            {

                LbLowQty.Text = "กำหนดจำนวน (ชิ้น)";
            }
            switch (ddlPromotionType_Ins.SelectedValue)
            {

                case "01":
                    {
                        //Free Shiping only

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "02":
                    {
                        //individual discounted product price

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "03":
                    {
                        //product discount

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = true;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "04":
                    {
                        //promotion discount

                        GroupingSection.Visible = true;
                        PromotionDiscountSection.Visible = true;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "05":
                    {
                        //grouping

                        GroupingSection.Visible = true;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "06":
                    {
                        //grouping with group price

                        GroupingSection.Visible = true;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = true;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "07":
                    {
                        //MOQ + free shipping

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = true;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "08":
                    {
                        //MOQ + product discount

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = true;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = true;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "09":
                    {
                        //MOQ + promotion discount

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = true;
                        MOQSection.Visible = true;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "10":
                    {
                        //minimum total price + promotion discount

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = true;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;


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

                        break;
                    }
                case "11":
                    {
                        //minimum total price + redeem

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "12":
                    {
                        //minimum total price + giveaway

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "13":
                    {
                        //normal price

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = false;

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

                        break;
                    }
                case "14":
                    {
                        //combo set

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

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

                        break;
                    }
                case "15":
                    {
                        //จับกลุ่ม + ฟรีค่าขนส่ง

                        GroupingSection.Visible = true;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

                        txtDiscountAmount_Ins.Text = "0";
                        txtDiscountPercent_Ins.Text = "0";
                        txtProductDiscountAmount_Ins.Text = "0";
                        txtProductDiscountPercent_Ins.Text = "0";
                        txtMinimumQty_Ins.Text = "0";
                        txtMinimumTotPrice_Ins.Text = "0";

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

                        break;
                    }
                case "16":
                    {
                        hidMOQFlagPromotion.Value = "N";//จับกลุ่ม + ลดราคา (ส่วนลดโปรโมชั่น)

                        

                        GroupingSection.Visible = true;
                        PromotionDiscountSection.Visible = true;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = true;

                        txtProductDiscountAmount_Ins.Text = "0";
                        txtProductDiscountPercent_Ins.Text = "0";
                        txtGroupPrice_Ins.Text = "0";
                        txtMinimumTotPrice_Ins.Text = "0";

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

                        break;
                    }
                case "17":
                    {
                        //กำหนดยอดซื้อขั้นต่ำ + ฟรีค่าขนส่ง

                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;


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

                        break;
                    }
                default:
                    {
                        GroupingSection.Visible = false;
                        PromotionDiscountSection.Visible = false;
                        ProductDiscountSection.Visible = false;
                        GroupPriceSection.Visible = false;
                        MOQSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = false;

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
                        ShowComboSection("N");

                        break;
                    }

            }

        }

        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        protected void ShowComboSection(string flag)
        {
            

        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"CombosetDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
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

            ddlPromotionStatus_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlPromotionType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionType";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionTypeInfo> lLookupInfo = JsonConvert.DeserializeObject<List<PromotionTypeInfo>>(respstr);


            ddlPromotionType_Ins.DataSource = lLookupInfo;

            ddlPromotionType_Ins.DataTextField = "PromotionTypeName";

            ddlPromotionType_Ins.DataValueField = "PromotionTypeCode";

            ddlPromotionType_Ins.DataBind();

            ddlPromotionType_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));


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

            ddlSearchPromotionLevel.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlSearchProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);


            ddlSearchProductBrand.DataSource = lLookupInfo;

            ddlSearchProductBrand.DataTextField = "ProductBrandName";

            ddlSearchProductBrand.DataValueField = "ProductBrandCode";

            ddlSearchProductBrand.DataBind();

            ddlSearchProductBrand.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);


            ddlProductBrand_Ins.DataSource = lLookupInfo;

            ddlProductBrand_Ins.DataTextField = "ProductBrandName";

            ddlProductBrand_Ins.DataValueField = "ProductBrandCode";

            ddlProductBrand_Ins.DataBind();

            ddlProductBrand_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlSearchPromotionType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionType";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionTypeInfo> lLookupInfo = JsonConvert.DeserializeObject<List<PromotionTypeInfo>>(respstr);


            ddlSearchPromotionType.DataSource = lLookupInfo;

            ddlSearchPromotionType.DataTextField = "PromotionTypeName";

            ddlSearchPromotionType.DataValueField = "PromotionTypeCode";

            ddlSearchPromotionType.DataBind();

            ddlSearchPromotionType.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));


        }
        protected Boolean validateInsert()
        {
            Boolean flag = true;


            if (ddlProductBrand_Ins.SelectedValue == "-99" || ddlProductBrand_Ins.SelectedValue == "")
            {
                flag = false;
                lblProductBrand_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แบรนด์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProductBrand_Ins.Text = "";
            }

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
                    lblPromotionCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสโปรโมชั่นต่้องไม่มีอักขระพิเศษ";
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
                            lblPromotionCode_Ins.Text = "";
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
            if (ddlPromotionLevel_Ins.SelectedValue == "-99" || ddlPromotionLevel_Ins.SelectedValue == "")
            {
                flag = false;
                lblPromotionLevel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระดับโปรโมชั่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPromotionLevel_Ins.Text = "";
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
            if (ddlFreeShipFlag_Ins.SelectedValue == "-99" || ddlFreeShipFlag_Ins.SelectedValue == "")
            {
                flag = false;
                LbddlFreeShipFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ฟรีค่าขนส่ง";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbddlFreeShipFlag_Ins.Text = "";
            }

            if (ddlPromotionType_Ins.SelectedValue == "-99" || ddlPromotionType_Ins.SelectedValue == "")
            {
                flag = false;
                lblPromotionType_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ประเภทโปรโมชั่น";
            }
            else
            {
                if (ddlPromotionType_Ins.SelectedValue == "06" || ddlPromotionType_Ins.SelectedValue == "15" || ddlPromotionType_Ins.SelectedValue == "16")
                {
                    if (ddlPromotionType_Ins.SelectedValue == "06")
                    {

                        if (ddlLockAmountFlag_Ins.SelectedValue == "-99" || ddlLockAmountFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            LbddlLockAmountFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แก้ไขจำนวนสินค้า";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            LbddlLockAmountFlag_Ins.Text = "";
                        }
                        if (txtGroupPrice_Ins.Text == "-99" || txtGroupPrice_Ins.Text == "")
                        {
                            flag = false;
                            lblGroupPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แก้ไขจำนวนสินค้า";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblGroupPrice_Ins.Text = "";
                        }
                    }
                    if (ddlPromotionType_Ins.SelectedValue == "15")
                    {

                        if (ddlLockAmountFlag_Ins.SelectedValue == "-99" || ddlLockAmountFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            LbddlLockAmountFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แก้ไขจำนวนสินค้า";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            LbddlLockAmountFlag_Ins.Text = "";
                        }

                    }
                    if (ddlPromotionType_Ins.SelectedValue == "16")
                    {

                        if (ddlLockAmountFlag_Ins.SelectedValue == "-99" || ddlLockAmountFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            LbddlLockAmountFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แก้ไขจำนวนสินค้า";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            LbddlLockAmountFlag_Ins.Text = "";
                        }
                        if ((txtDiscountAmount_Ins.Text == "0" && txtDiscountPercent_Ins.Text == "0"))

                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (บาท)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (%)";

                        }
                        else if ((txtDiscountPercent_Ins.Text != "0" && txtDiscountAmount_Ins.Text != "0"))
                        {
                            flag = false;
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือก ส่วนลด(บาท)/ส่วนลด(%)";
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือก ส่วนลด(บาท)/ส่วนลด(%)";
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
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (บาท)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (%)";
                        }

                    }
                }
                else if (ddlPromotionType_Ins.SelectedValue == "07" || ddlPromotionType_Ins.SelectedValue == "08" || ddlPromotionType_Ins.SelectedValue == "09")
                {
                    if (ddlPromotionType_Ins.SelectedValue == "07")
                    {

                        if (ddlMOQFlag_Ins.SelectedValue == "-99" || ddlMOQFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            lblMOQFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แก้ไขจำนวนสินค้า";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMOQFlag_Ins.Text = "";
                        }
                        if (txtMinimumQty_Ins.Text == "-99" || txtMinimumQty_Ins.Text == "")
                        {
                            flag = false;
                            lblMinimumQty_Ins.Text = MessageConst._MSG_PLEASEINSERT + " กำหนดยอดซื้อขั้นต่ำ";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMinimumQty_Ins.Text = "";
                        }
                    }
                    else if (ddlPromotionType_Ins.SelectedValue == "08")
                    {
                        if ((txtProductDiscountAmount_Ins.Text == "0" && txtProductDiscountPercent_Ins.Text == "0"))

                        {
                            flag = false;
                            lblProductDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (บาท)";
                            lblProductDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (%)";

                        }
                        else if ((txtProductDiscountPercent_Ins.Text != "0" && txtProductDiscountAmount_Ins.Text != "0"))
                        {
                            flag = false;
                            lblProductDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือก ส่วนลด(บาท)/ส่วนลด(%)";
                            lblProductDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือก ส่วนลด(บาท)/ส่วนลด(%)";
                        }
                        else if ((txtProductDiscountAmount_Ins.Text == "0" || txtProductDiscountPercent_Ins.Text != "0")
                           || (txtProductDiscountAmount_Ins.Text != "0" || txtProductDiscountPercent_Ins.Text == "0"))
                        {
                            flag = (flag == false) ? false : true;
                            lblProductDiscountPercent_Ins.Text = "";
                            lblProductDiscountAmount_Ins.Text = "";
                        }
                    }
                    else if (ddlPromotionType_Ins.SelectedValue == "09")
                    {
                        if (ddlMOQFlag_Ins.SelectedValue == "-99" || ddlMOQFlag_Ins.SelectedValue == "")
                        {
                            flag = false;
                            lblMOQFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แก้ไขจำนวนสินค้า";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMOQFlag_Ins.Text = "";
                        }
                        if (txtMinimumQty_Ins.Text == "0" || txtMinimumQty_Ins.Text == "")
                        {
                            flag = false;
                            lblMinimumQty_Ins.Text = MessageConst._MSG_PLEASEINSERT + " กำหนดยอดซื้อขั้นต่ำ";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMinimumQty_Ins.Text = "";
                        }
                        if (txtGroupPrice_Ins.Text == "0" || txtGroupPrice_Ins.Text == "")
                        {
                            flag = false;
                            lblGroupPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " กรุณากรอกราคา";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblGroupPrice_Ins.Text = "";
                        }
                        
                    }

                }
                else if (ddlPromotionType_Ins.SelectedValue == "02" || ddlPromotionType_Ins.SelectedValue == "03")
                {
                    if (ddlPromotionType_Ins.SelectedValue == "02")
                    {

                    }
                    else if (ddlPromotionType_Ins.SelectedValue == "03")
                    {
                        if ((txtProductDiscountAmount_Ins.Text == "0" && txtProductDiscountPercent_Ins.Text == "0"))

                        {
                            flag = false;
                            lblProductDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (บาท)";
                            lblProductDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (%)";

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
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + "  เลือก ส่วนลด(บาท)/ส่วนลด(%)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือก ส่วนลด(บาท)/ส่วนลด(%)";
                        }
                    }
                }
                else if (ddlPromotionType_Ins.SelectedValue == "11" || ddlPromotionType_Ins.SelectedValue == "12"
                    || ddlPromotionType_Ins.SelectedValue == "10" || ddlPromotionType_Ins.SelectedValue == "17")
                {
                    if (ddlPromotionType_Ins.SelectedValue == "10")
                    {
                        if ((txtDiscountAmount_Ins.Text == "0" && txtDiscountPercent_Ins.Text == "0"))

                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (บาท)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ส่วนลด (%)";

                        }
                        else if ((txtDiscountAmount_Ins.Text != "0" && txtDiscountPercent_Ins.Text != "0"))

                        {
                            flag = false;
                            lblDiscountAmount_Ins.Text = MessageConst._MSG_PLEASEINSERT + "  เลือก ส่วนลด(บาท)/ส่วนลด(%)";
                            lblDiscountPercent_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือก ส่วนลด(บาท)/ส่วนลด(%)";

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
                            lblMinimumTotPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ยอดซื้อขั้นต่ำ";
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
                            lblMinimumTotPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ยอดซื้อขั้นต่ำ";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblMinimumTotPrice_Ins.Text = "";
                        }
                    }
                }
                else if (ddlPromotionType_Ins.SelectedValue == "00")
                {
                    flag = true;
                }


                flag = (flag == false) ? false : true;
                lblPromotionType_Ins.Text = "";
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


    }
}
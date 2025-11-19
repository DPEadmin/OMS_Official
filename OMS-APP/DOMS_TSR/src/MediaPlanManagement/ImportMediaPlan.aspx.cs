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
using OfficeOpenXml;
namespace DOMS_TSR.src.MediaPlanManagement
{
    public partial class ImportMediaPlan : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string MediaPlanImgUrl = ConfigurationManager.AppSettings["MediaPlanImageUrl"];

        string Idlist = "";
        string Codelist = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        L_MediaPlan result = new L_MediaPlan();
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];



                if (empInfo != null && merchantInfo != null)
                {
                    

                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

            }
        }

        protected void btnShowImportFile_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-ImportMedia').modal();", true);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelPath = "";
            string FileName = "";

            try
            {
                //*** Check HasFile And Type of File is equals excel /**
                if (fiUpload.HasFile && (fiUpload.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    int fileSize = fiUpload.PostedFile.ContentLength;
                    if (fileSize > 5600000)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ขนาดไฟล์เกิน 5 MB');", true);
                        return;
                    }

                    else
                    {
                        string newFileName = "Upload_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                        fiUpload.SaveAs(Server.MapPath("~/Uploadfile/Xls/" + newFileName));


                        FileInfo excel = new FileInfo(Server.MapPath(@"~/Uploadfile/Xls/" + newFileName));
                        ExcelPath = excel.ToString();
                        FileName = newFileName;
                        ViewState["UpLoadFileName"] = fiUpload.FileName.ToString();
                        ViewState["FileName"] = newFileName;
                        ViewState["ExcelPath"] = excel.ToString();

                        using (var package = new ExcelPackage(excel))
                        {
                            var workbook = package.Workbook;
                            var worksheet = workbook.Worksheets[1];
                            //เช็คว่าไฟล์มีข้อมูล
                            if (worksheet.Cells[2, 2].Text.ToString().Trim() == "")
                            {
                                DivSubmitUpload.Visible = false;
                                File.Delete(ExcelPath);
                            }
                            else
                            {
                                DataTable dt = ConvertToDataTable(worksheet);
                                
                                dt = dt.DefaultView.ToTable();
                                gvMediaPlanImport.DataSource = dt;
                                gvMediaPlanImport.DataBind();
                                DivSubmitUpload.Visible = true;


                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }

        protected void btnSubmitImport_Click(object sender, EventArgs e)
        {
            if (ValidateImportInsert())
            {
                string Nulldata = "";
                int Countinsert = 0;
                Boolean flagNull = true;
                try
                {
                    for (int i = 0; i < gvMediaPlanImport.Rows.Count; i++)
                    {
                        Countinsert++;
                        HiddenField HidMEDIA_DATE = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMEDIA_DATE");
                        HiddenField HidMEDIA_ENDDATE = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMEDIA_ENDDATE");
                        HiddenField HidTIME_START = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidTIME_START");
                        HiddenField HidTIME_END = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidTIME_END");
                        HiddenField HidDuration = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidDuration");
                        HiddenField HidMEDIA_PHONE = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMEDIA_PHONE");
                        HiddenField HidSALE_CHANNEL = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidSALE_CHANNEL");
                        HiddenField HidMEDIA_CHANNEL = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMEDIA_CHANNEL");
                        HiddenField HidPROGRAM_NAME = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidPROGRAM_NAME");
                        HiddenField HidCAMPAIGN_CODE = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidCAMPAIGN_CODE");
                        HiddenField HidMERCHANT_NAME = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMERCHANT_NAME");

                        // Check data not null
                        int? countMePhone = CountMediaPhoneMasterList(HidMEDIA_PHONE.Value.ToString());
                        if (countMePhone > 0)
                        {

                        }
                        else
                        {
                            Nulldata += " MediaPhone Line " + Countinsert + ", ";
                            flagNull = false;
                        }
                        int? countMEDIA_CHANNEL = CountMediaChannelMasterList(HidMEDIA_CHANNEL.Value.ToString());
                        if (countMEDIA_CHANNEL > 0)
                        {

                        }
                        else
                        {
                            Nulldata += " MEDIA_CHANNEL Line " + Countinsert + ", ";
                            flagNull = false;
                        }

                        int? countSALE_CHANNEL = CountSaleChannelMasterList(HidSALE_CHANNEL.Value.ToString());
                        if (countSALE_CHANNEL > 0)
                        {

                        }
                        else
                        {
                            Nulldata += " Sale_CHANNEL Line " + Countinsert + ", ";
                            flagNull = false;
                        }

                        int? countCAMPAIGN_CODE = CountCampaignMasterList(HidCAMPAIGN_CODE.Value.ToString());
                        if (countCAMPAIGN_CODE > 0)
                        {

                        }
                        else
                        {
                            Nulldata += " CAMPAIGN_CODE Line " + Countinsert + ", ";
                            flagNull = false;
                        }

                        
                        result.L_MediaPlanInfo.Add(new MediaPlanInfo()
                        {
                            MediaPlanDate = HidMEDIA_DATE.Value.ToString().Trim(),
                            MediaPlanEndDate = HidMEDIA_ENDDATE.Value.ToString().Trim(),
                            MediaPlanTime = HidMEDIA_DATE.Value.ToString().Trim() + " " + HidTIME_START.Value.ToString().Trim(),
                            ProgramName = HidPROGRAM_NAME.Value.ToString().Trim(),
                            SALE_CHANNEL = HidSALE_CHANNEL.Value.ToString().Trim(),
                            Duration = HidDuration.Value.ToString().Trim(),
                            MediaPhone = HidMEDIA_PHONE.Value.ToString().Trim(),
                            CampaignCode = HidCAMPAIGN_CODE.Value.ToString().Trim(),
                            TIME_START = HidTIME_START.Value.ToString(),
                            TIME_END = HidTIME_END.Value.ToString(),
                            MEDIA_CHANNEL = HidMEDIA_CHANNEL.Value.ToString(),
                            MerchantCode = hidMerCode.Value.ToString(),


                            FlagDelete = "N"
                        }); ;

                        



                    }

                    if (flagNull)
                    {
                        if (flagNull)
                        {

                            string respstr = "";
                            APIpath = APIUrl + "/api/support/InsertMediaPlanList";
                            using (var client = new WebClient())
                            {
                                client.Encoding = Encoding.UTF8;
                                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                var jsonObj = JsonConvert.SerializeObject(new { result.L_MediaPlanInfo });
                                var dataString = client.UploadString(APIpath, jsonObj);

                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('บันทึกเรียบร้อย นำเข้าไฟล์สำเร็จ " + Countinsert + " แถว');", true);
                                gvMediaPlanImport.DataSource = null;
                                gvMediaPlanImport.DataBind();
                                DivSubmitUpload.Visible = false;

                            }
                        }
                        
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามรถ import ได้ : " + Nulldata + "');", true);
                        gvMediaPlanImport.DataSource = null;
                        gvMediaPlanImport.DataBind();
                        DivSubmitUpload.Visible = false;

                    }
                }

                catch (Exception ex)

                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ไม่สามรถ import ได้ format ไม่ถูกต้อง');", true);
                    gvMediaPlanImport.DataSource = null;
                    gvMediaPlanImport.DataBind();
                    DivSubmitUpload.Visible = false;
                }
                



                
            }
        }
        protected Boolean ValidateImportInsert()
        {
            Boolean flag = true;
            string error = "";
            int counterr = 0;
            List<MediaPhoneInfo> phoneInfo = new List<MediaPhoneInfo>();
            List<MediaChannelInfo> channelInfo = new List<MediaChannelInfo>();
            List<SaleChannelInfo> SaleInfo = new List<SaleChannelInfo>();
            List<CampaignInfo> CamInfo = new List<CampaignInfo>();
            List<MerchantInfo> MerInfo = new List<MerchantInfo>();

            foreach (GridViewRow row in gvMediaPlanImport.Rows)
            {
                Label lblMEDIA_PHONE = (Label)row.FindControl("lblMEDIA_PHONE");
                Label lblLINE_NO = (Label)row.FindControl("lblLINE_NO");
                error += "ในแถวที่ " + lblLINE_NO.Text + "\\n";
                phoneInfo = GetMediaPhoneValidateInsert(lblMEDIA_PHONE.Text);
                if (phoneInfo.Count == 0)
                {
                    flag = false;
                    counterr++;
                    error += "Mediaphone ไม่ถูกต้อง\\n";
                }


                Label lblCHANNEL = (Label)row.FindControl("lblCHANNEL");
                channelInfo = GetMediaChannelValidateInsert(lblCHANNEL.Text);
                if (channelInfo.Count == 0)
                {
                    flag = false;
                    counterr++;
                    error += "Mediachannel ไม่ถูกต้อง\\n";
                }

                Label lblCHANNEL_TYPE = (Label)row.FindControl("lblCHANNEL_TYPE");

                SaleInfo = GetSaleChannelValidateInsert(lblCHANNEL_TYPE.Text);
                if (SaleInfo.Count == 0)
                {
                    flag = false;
                    counterr++;
                    error += "Salechannel ไม่ถูกต้อง\\n";
                }

                Label lblMERCHANT_NAME = (Label)row.FindControl("lblMERCHANT_NAME");

                MerInfo = GetMerValidateInsert(lblMERCHANT_NAME.Text);
                if (MerInfo.Count == 0)
                {
                    flag = false;
                    counterr++;
                    error += "Mercahnt Name ไม่ตรงกับ Merchant ที่เลือก\\n";
                }


                Label lblCAMPAIGN_CODE = (Label)row.FindControl("lblCAMPAIGN_CODE");

                CamInfo = GetCamValidateInsert(lblCAMPAIGN_CODE.Text);
                if (CamInfo.Count == 0)
                {
                    flag = false;
                    counterr++;
                    error += "Campaign ไม่ถูกต้อง\\n";
                }


            }
            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }
            return flag;
        }

        protected List<MerchantInfo> GetMerValidateInsert(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMerchantForValByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["MerchantName"] = code;
                data["MerchantCode"] = hidMerCode.Value;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> MerchantInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);

            return MerchantInfo;
        }

        protected List<MediaPhoneInfo> GetMediaPhoneValidateInsert(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaPhoneNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["MediaPhoneCode"] = code;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MediaPhoneInfo> MediaPhoneInfo = JsonConvert.DeserializeObject<List<MediaPhoneInfo>>(respstr);

            return MediaPhoneInfo;
        }
        protected List<MediaChannelInfo> GetMediaChannelValidateInsert(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["Codeval"] = code;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MediaChannelInfo> MediaChannelInfo = JsonConvert.DeserializeObject<List<MediaChannelInfo>>(respstr);

            return MediaChannelInfo;
        }
        protected List<SaleChannelInfo> GetSaleChannelValidateInsert(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ChannelCode"] = code;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<SaleChannelInfo> SaleChannelInfo = JsonConvert.DeserializeObject<List<SaleChannelInfo>>(respstr);

            return SaleChannelInfo;
        }
        protected List<CampaignInfo> GetCamValidateInsert(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = code;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> CampaignInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

            return CampaignInfo;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"MediaPlanDetail.aspx?MediaPlanCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
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

        private DataTable ConvertToDataTable(ExcelWorksheet worksheet)
        {
            int totalRows = worksheet.Dimension.End.Row;
            DataTable dt = new DataTable(worksheet.Name);
            DataRow dr = null;

            //*** Column **/
            dt.Columns.Add("LINE_NO");
            dt.Columns.Add("MEDIA_DATE");
            dt.Columns.Add("MEDIA_ENDDATE");
            dt.Columns.Add("TIME_START");
            dt.Columns.Add("TIME_END");
            dt.Columns.Add("Duration");
            dt.Columns.Add("MEDIA_PHONE");
            dt.Columns.Add("SALE_CHANNEL");
            dt.Columns.Add("MEDIA_CHANNEL");
            dt.Columns.Add("PROGRAM_NAME");
            dt.Columns.Add("CAMPAIGN_CODE");
            dt.Columns.Add("MERCHANT_NAME");
            try
            {
                int j;
                for (int i = 2; i <= totalRows; i++)
                {
                    //*** Rows ***//
                    dr = dt.NewRow();
                    j = 1;

                    dr["LINE_NO"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_DATE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_ENDDATE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["TIME_START"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["TIME_END"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["Duration"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_PHONE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["SALE_CHANNEL"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_CHANNEL"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                    dr["PROGRAM_NAME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["CAMPAIGN_CODE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MERCHANT_NAME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        public int? CountMediaPhoneMasterList(string mediaphoneNo)
        {

            int? cou = 0;
            APIpath = APIUrl + "/api/support/CountListMediaPhone";
            MediaPhoneInfo mon = new MediaPhoneInfo();
            List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.MediaPhoneNo = mediaphoneNo;
                mon.Active = "";


                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.MediaPhoneNo,
                    mon.Active,

                });
                var dataString = client.UploadString(APIpath, jsonObj);
                cou = JsonConvert.DeserializeObject<int?>(dataString.ToString());

            }



            return cou;

        }

        public int? CountMediaChannelMasterList(string MediaChannel)
        {

            int? cou = 0;
            APIpath = APIUrl + "/api/support/CountListMediaChannel";
            MediaChannelInfo mon = new MediaChannelInfo();
            List<MediaChannelInfo> lMediaChannelInfo = new List<MediaChannelInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.Code = MediaChannel;


                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.Code,

                });
                var dataString = client.UploadString(APIpath, jsonObj);
                cou = JsonConvert.DeserializeObject<int?>(dataString.ToString());

            }



            return cou;

        }

        public int? CountSaleChannelMasterList(string code)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountChannelListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = code;

                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        public int? CountCampaignMasterList(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountCampaignListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = code;

                data["Active"] = "Y";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        #endregion
    }
}
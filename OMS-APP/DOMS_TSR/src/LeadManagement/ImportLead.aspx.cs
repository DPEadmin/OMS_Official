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
namespace DOMS_TSR.src.LeadManagement
{
    public partial class ImportLead : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string MediaPlanImgUrl = ConfigurationManager.AppSettings["MediaPlanImageUrl"];
        protected static string APIUrlx = ConfigurationManager.AppSettings["apiurl"];
        string Idlist = "";
        string Codelist = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        L_LeadManagementInfo result = new L_LeadManagementInfo();
        List<CampaignCategoryInfo> lcampaignCatCcode = new List<CampaignCategoryInfo>();
        List<MediaPhoneInfo> lMediaPhone = new List<MediaPhoneInfo>();
        List<ProvinceInfo> lProvice = new List<ProvinceInfo>();
        List<CampaignInfo> lCamp= new List<CampaignInfo>();
        
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
                    
                    APIUrl = empInfo.ConnectionAPI;
                    APIUrl = APIUrlx;
                    hidEmpCode.Value = empInfo.EmpCode;
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
                        string newFileName = "Upload_Lead" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
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
            string Nulldata = "";
            int Countinsert = 0;
            Boolean flagNull = true;
            
            lcampaignCatCcode = GetCampaignCategoryCode();
            lMediaPhone = GetMediaPhoneMasterByCriteria();

       
            for (int i = 0; i < gvMediaPlanImport.Rows.Count; i++)
            {
                Countinsert++;
                Label lblREF_CODE = (Label)gvMediaPlanImport.Rows[i].FindControl("lblREF_CODE");
                Label lblLOT_NAME = (Label)gvMediaPlanImport.Rows[i].FindControl("lblLOT_NAME");
                Label lblCHANNEL_FROM = (Label)gvMediaPlanImport.Rows[i].FindControl("lblCHANNEL_FROM");
                Label lblCHANNEL_TO = (Label)gvMediaPlanImport.Rows[i].FindControl("lblCHANNEL_TO");
                Label lblMERCHANT_CODE = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMERCHANT_CODE");

                Label lblBRAND_NO = (Label)gvMediaPlanImport.Rows[i].FindControl("lblBRAND_NO");
                Label lblMEDIA_PHONE = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMEDIA_PHONE");
                Label lblPREFIX_TH = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPREFIX_TH");
                Label lblFIRSTNAME_TH = (Label)gvMediaPlanImport.Rows[i].FindControl("lblFIRSTNAME_TH");
                Label lblLASTNAME_TH = (Label)gvMediaPlanImport.Rows[i].FindControl("lblLASTNAME_TH");

                Label lblFULL_NAME_TH = (Label)gvMediaPlanImport.Rows[i].FindControl("lblFULL_NAME_TH");
                Label lblMOBILE_1 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMOBILE_1");
                Label lblMOBILE_2 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMOBILE_2");
                Label lblMOBILE_3 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMOBILE_3");
                Label lblMOBILE_4 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMOBILE_4");

                Label lblMOBILE_5 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMOBILE_5");
                Label lblMOBILE_6 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblMOBILE_6");
                Label lblPHONE_1 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPHONE_1");
                Label lblPHONE_2 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPHONE_2");
                Label lblPHONE_3 = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPHONE_3");

                
                 Label lblADDR_NO = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_NO");
                Label lblPLACE = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPLACE");
                Label lblADDR_SUBDISTRICT = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_SUBDISTRICT");
                Label lblADDR_SUBDISTRICT_ID = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_SUBDISTRICT_ID");
                Label lblADDR_DISTRICT = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_DISTRICT");
                Label lblADDR_DISTRICT_ID = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_DISTRICT_ID");

                Label lblADDR_PROVINCE = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_PROVINCE");
                Label lblADDR_PROVINCE_ID = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_PROVINCE_ID");
                Label lblADDR_ZIPCODE = (Label)gvMediaPlanImport.Rows[i].FindControl("lblADDR_ZIPCODE");
                Label lblPREVIOUS_SALE_NAME = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPREVIOUS_SALE_NAME");
                Label lblPREVIOUS_ORDER_DATE = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPREVIOUS_ORDER_DATE");
                Label lblPREVIOUS_ORDER_BRAND = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPREVIOUS_ORDER_BRAND");
                Label lblPREVIOUS_PRODUCT = (Label)gvMediaPlanImport.Rows[i].FindControl("lblPREVIOUS_PRODUCT");
                Label lblCAMPAIGN_ID = (Label)gvMediaPlanImport.Rows[i].FindControl("lblCAMPAIGN_ID");

                if (lblREF_CODE.Text=="") 
                {
                    Nulldata += " REF_CODE Line " + Countinsert + ", ";
                    flagNull = false;
                }
                if (lblLOT_NAME.Text == "")
                {
                    Nulldata += " LOT_NAME Line " + Countinsert + ", ";
                    flagNull = false;
                }
                if (lblCHANNEL_FROM.Text == "")
                {
                    Nulldata += " CHANNEL_FROM Line " + Countinsert + ", ";
                    flagNull = false;
                }
                if (lblCHANNEL_TO.Text == "")
                {
                    Nulldata += " CHANNEL_TO Line " + Countinsert + ", ";
                    flagNull = false;
                }
                if (lblMERCHANT_CODE.Text == "")
                {
                    Nulldata += " MERCHANT_CODE Line " + Countinsert + ", ";
                    flagNull = false;
                }
                else 
                {
                    
                    //var CountMerchant= lcampaignCatCcode.Where(s => s.CampaignCategoryCode == lblMERCHANT_CODE.Text)                    
                    // .ToList();
                    //if (CountMerchant.Count >0)
                    
                    //{
                    //    //True
                    //}
                    //else 
                    //{
                    //    Nulldata += " MERCHANT_CODE Line " + Countinsert + ", ";
                    //    flagNull = false;
                    //}
                }
                if (lblBRAND_NO.Text == "")
                {
                    Nulldata += " BRAND_NO Line " + Countinsert + ", ";
                    flagNull = false;
                }
                else
                {
                    
                    //var CountMerchant = lcampaignCatCcode.Where(s => s.CampaignCategoryCode == lblBRAND_NO.Text)
                    // .ToList();
                    //if (CountMerchant.Count > 0)
                    //{
                    //    //True
                    //}
                    //else
                    //{
                    //    Nulldata += " BRAND_NO Line " + Countinsert + ", ";
                    //    flagNull = false;
                    //}
                }
                if (lblMEDIA_PHONE.Text == "")
                {
                    Nulldata += " MEDIA_PHONE Line " + Countinsert + ", ";
                    flagNull = false;
                }
                else 
                {
                    
                    
                    //var CountMerPhone = lMediaPhone.Where(s => s.MediaPhoneNo == lblMEDIA_PHONE.Text)
                    // .ToList();
                    //if (CountMerPhone.Count > 0)
                    //{
                    //    //True
                    //}
                    //else
                    //{
                    //    Nulldata += " MEDIA_PHONE Line " + Countinsert + ", ";
                    //    flagNull = false;
                    //}
                }
                if (lblFIRSTNAME_TH.Text == "")
                {
                    Nulldata += " FIRSTNAME_TH Line " + Countinsert + ", ";
                    flagNull = false;
                }
                if (lblLASTNAME_TH.Text == "")
                {
                    Nulldata += " LASTNAME_TH Line " + Countinsert + ", ";
                    flagNull = false;
                }
                if (lblFULL_NAME_TH.Text == "")
                {
                    Nulldata += " FULL_NAME_TH Line " + Countinsert + ", ";
                    flagNull = false;
                }
                if (lblMOBILE_1.Text == "")
                {
                    Nulldata += " MOBILE_1 Line " + Countinsert + ", ";
                    flagNull = false;
                }
                else 
                {
                    if (lblMOBILE_1.Text.Length < 10 || lblMOBILE_1.Text.Length>10)
                    {
                        Nulldata += " MOBILE_1 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if(lblMOBILE_2.Text != "")
                {
                    if (lblMOBILE_2.Text.Length < 10 || lblMOBILE_2.Text.Length > 10)
                    {
                        Nulldata += " MOBILE_2 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if (lblMOBILE_3.Text != "")
                {
                    if (lblMOBILE_3.Text.Length < 10 || lblMOBILE_3.Text.Length > 10)
                    {
                        Nulldata += " MOBILE_3 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if (lblMOBILE_4.Text != "" )
                {
                    if (lblMOBILE_4.Text.Length < 10 || lblMOBILE_4.Text.Length > 10)
                    {
                        Nulldata += " MOBILE_4 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if (lblMOBILE_5.Text != "")
                {
                    if (lblMOBILE_5.Text.Length < 10 || lblMOBILE_5.Text.Length > 10)
                    {
                        Nulldata += " MOBILE_5 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if (lblMOBILE_6.Text != "")
                {
                    if (lblMOBILE_6.Text.Length < 10 || lblMOBILE_6.Text.Length > 10)
                    {
                        Nulldata += " MOBILE_6 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if (lblPHONE_1.Text != "")
                {
                    if (lblPHONE_1.Text.Length < 9 || lblPHONE_1.Text.Length > 10)
                    {
                        Nulldata += " PHONE_1 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if (lblPHONE_2.Text != "")
                {
                    if (lblPHONE_2.Text.Length < 9 || lblPHONE_2.Text.Length > 10)
                    {
                        Nulldata += " PHONE_2 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }
                if (lblPHONE_3.Text != "")
                {
                    if (lblPHONE_3.Text.Length < 9 || lblPHONE_3.Text.Length > 10)
                    {
                        Nulldata += " PHONE_3 Line " + Countinsert + ", ";
                        flagNull = false;
                    }
                }

                if (lblCAMPAIGN_ID.Text == "")
                {
                    Nulldata += " CAMPAING_ID Line " + Countinsert + ", ";
                    flagNull = false;
                }
                else 
                {
                    
                }
                
                result.L_LeadManagement.Add(new LeadManagementInfo()
                {
                    REF_CODE = lblREF_CODE.Text,
                    LOT_NAME = lblLOT_NAME.Text,
                    CHANNEL_FROM = lblCHANNEL_FROM.Text,
                    CHANNEL_TO = lblCHANNEL_TO.Text,
                    MERCHANT_CODE = lblMERCHANT_CODE.Text,

                    BRAND_NO = lblBRAND_NO.Text.ToString().Trim(),
                    MEDIA_PHONE = lblMEDIA_PHONE.Text.ToString().Trim(),
                    PREFIX_TH = lblPREFIX_TH.Text.ToString().Trim(),
                    FIRSTNAME_TH = lblFIRSTNAME_TH.Text.ToString().Trim(),
                    LASTNAME_TH = lblLASTNAME_TH.Text.ToString().Trim(),

                    FULL_NAME_TH = lblFULL_NAME_TH.Text.ToString().Trim(),
                    MOBILE_1 = lblMOBILE_1.Text.ToString().Trim(),
                    MOBILE_2 = lblMOBILE_2.Text.ToString().Trim(),
                    MOBILE_3 = lblMOBILE_3.Text.ToString().Trim(),
                    MOBILE_4 = lblMOBILE_4.Text.ToString().Trim(),

                    MOBILE_5 = lblMOBILE_5.Text.ToString().Trim(),
                    MOBILE_6 = lblMOBILE_6.Text.ToString().Trim(),
                    PHONE_1 = lblPHONE_1.Text.ToString().Trim(),
                    PHONE_2 = lblPHONE_2.Text.ToString().Trim(),
                    PHONE_3 = lblPHONE_3.Text.ToString().Trim(),
                    ADDR_NO = lblADDR_NO.Text.ToString().Trim(),
                    PLACE = lblPLACE.Text.ToString().Trim(),
                    ADDR_SUBDISTRICT = lblADDR_SUBDISTRICT.Text.ToString().Trim(),
                    ADDR_SUBDISTRICT_ID = lblADDR_SUBDISTRICT_ID.Text.ToString().Trim(),
                    ADDR_DISTRICT = lblADDR_DISTRICT.Text.ToString().Trim(),
                    ADDR_DISTRICT_ID = lblADDR_DISTRICT_ID.Text.ToString().Trim(),

                    ADDR_PROVINCE = lblADDR_PROVINCE.Text.ToString().Trim(),
                    ADDR_PROVINCE_ID = lblADDR_PROVINCE_ID.Text.ToString().Trim(),
                    ADDR_ZIPCODE = lblADDR_ZIPCODE.Text.ToString().Trim(),
                    PREVIOUS_SALE_NAME = lblPREVIOUS_SALE_NAME.Text.ToString().Trim(),
                    PREVIOUS_ORDER_DATE = lblPREVIOUS_ORDER_DATE.Text.ToString().Trim(),

                    PREVIOUS_ORDER_BRAND = lblPREVIOUS_ORDER_BRAND.Text.ToString().Trim(),
                    PREVIOUS_PRODUCT = lblPREVIOUS_PRODUCT.Text.ToString().Trim(),
                    CAMPAIGN_ID = lblCAMPAIGN_ID.Text.ToString().Trim(),


                    UpdateBy = hidEmpCode.Value.ToString(),
                    CreateBy = hidEmpCode.Value.ToString(),
                    FlagDelete = "N"
                    //set upload lead 06/07/2023 lookupCode
                    ,Status = "UP", 
                });
            }
            if (flagNull)
            {

                APIpath = APIUrl + "/api/support/InsertLeadList";
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var jsonObj = JsonConvert.SerializeObject(new { result.L_LeadManagement });
                    var dataString = client.UploadString(APIpath, jsonObj);

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "นำเข้าไฟล์สำเร็จ : " + Countinsert + "');", true);
                    DivSubmitUpload.Visible = false;
                }
           
            }
            else 
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "ไม่สามรถ import ได้ : " + Nulldata + "');", true);
            }

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
            dt.Columns.Add("REF_CODE");
            dt.Columns.Add("LOT_NAME");
            dt.Columns.Add("CHANNEL_FROM");
            dt.Columns.Add("CHANNEL_TO");

            dt.Columns.Add("MERCHANT_CODE");
            dt.Columns.Add("BRAND_NO");
            dt.Columns.Add("MEDIA_PHONE");
            dt.Columns.Add("PREFIX_TH");
            dt.Columns.Add("FIRSTNAME_TH");

            dt.Columns.Add("LASTNAME_TH");
            dt.Columns.Add("FULL_NAME_TH");
            dt.Columns.Add("MOBILE_1");
            dt.Columns.Add("MOBILE_2");
            dt.Columns.Add("MOBILE_3");

            dt.Columns.Add("MOBILE_4");
            dt.Columns.Add("MOBILE_5");
            dt.Columns.Add("MOBILE_6");
            dt.Columns.Add("PHONE_1");
            dt.Columns.Add("PHONE_2");

            dt.Columns.Add("PHONE_3");
            dt.Columns.Add("ADDR_NO");
            dt.Columns.Add("PLACE");
            dt.Columns.Add("ADDR_SUBDISTRICT");
            dt.Columns.Add("ADDR_SUBDISTRICT_ID");
            dt.Columns.Add("ADDR_DISTRICT");

            dt.Columns.Add("ADDR_DISTRICT_ID");
            dt.Columns.Add("ADDR_PROVINCE");
            dt.Columns.Add("ADDR_PROVINCE_ID");
            dt.Columns.Add("ADDR_ZIPCODE");
            dt.Columns.Add("PREVIOUS_SALE_NAME");

            dt.Columns.Add("PREVIOUS_ORDER_DATE");
            dt.Columns.Add("PREVIOUS_ORDER_BRAND");
            dt.Columns.Add("PREVIOUS_PRODUCT");
            dt.Columns.Add("CAMPAIGN_ID");
            try
            {
                int j;
                for (int i = 2; i <= totalRows; i++)
                {
                    //*** Rows ***//
                    dr = dt.NewRow();
                    j = 1;

                    dr["LINE_NO"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["REF_CODE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["LOT_NAME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["CHANNEL_FROM"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["CHANNEL_TO"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;


                    dr["MERCHANT_CODE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["BRAND_NO"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_PHONE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PREFIX_TH"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["FIRSTNAME_TH"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                    dr["LASTNAME_TH"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["FULL_NAME_TH"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MOBILE_1"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MOBILE_2"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MOBILE_3"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                    dr["MOBILE_4"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MOBILE_5"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MOBILE_6"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PHONE_1"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PHONE_2"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                     dr["PHONE_3"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ADDR_NO"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PLACE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ADDR_SUBDISTRICT"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ADDR_SUBDISTRICT_ID"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ADDR_DISTRICT"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                    dr["ADDR_DISTRICT_ID"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ADDR_PROVINCE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ADDR_PROVINCE_ID"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ADDR_ZIPCODE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PREVIOUS_SALE_NAME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                    dr["PREVIOUS_ORDER_DATE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PREVIOUS_ORDER_BRAND"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PREVIOUS_PRODUCT"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["CAMPAIGN_ID"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        protected List<CampaignCategoryInfo> GetCampaignCategoryCode()
        {

            List<CampaignCategoryInfo> lMonInfo = new List<CampaignCategoryInfo>();
            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingByCriteria";
            CampaignCategoryInfo mon = new CampaignCategoryInfo();
            List<CampaignCategoryInfo> lMediaPhoneInfo = new List<CampaignCategoryInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

            
                mon.FlagDelete = "N";


                var jsonObj = JsonConvert.SerializeObject(new
                {
                 
                    mon.FlagDelete,

                });
                var dataString = client.UploadString(APIpath, jsonObj);
                lMonInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(dataString.ToString());

            }



            return lMonInfo;

        }


        public List<MediaPhoneInfo> GetMediaPhoneMasterByCriteria()
        {

            APIpath = APIUrl + "/api/support/ListMediaPhoneNoPagingByCriteria";
            MediaPhoneInfo mon = new MediaPhoneInfo();
            List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.MediaPhoneNo = "";
                mon.Active = "Y";
                
                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.MediaPhoneNo,
                    mon.Active,
                    
                });
                var dataString = client.UploadString(APIpath, jsonObj);
                lMediaPhoneInfo = JsonConvert.DeserializeObject<List<MediaPhoneInfo>>(dataString.ToString());
            }

            return lMediaPhoneInfo;

        }

        public List<CampaignInfo> GetCampaignMasterByCriteria()
        {

            APIpath = APIUrl + "/api/support/ListCampaignPagingByCriteria";
            CampaignInfo mon = new CampaignInfo();
            List<CampaignInfo> lMediaPhoneInfo = new List<CampaignInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.CampaignCode = "";
                mon.Active = "Y";

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.CampaignCode,
                    mon.Active,

                });
                var dataString = client.UploadString(APIpath, jsonObj);
                lMediaPhoneInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(dataString.ToString());
            }

            return lMediaPhoneInfo;

        }

        #endregion
    }
}
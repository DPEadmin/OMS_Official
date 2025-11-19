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
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data.SqlClient;

namespace DOMS_TSR.src.InventoryManagement
{
    public partial class ImportAPILazadaOrder : System.Web.UI.Page
    {
        protected static string APIUrl;
        public static string strConn = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        protected static string InventoryDetailImgUrl = ConfigurationManager.AppSettings["MediaPlanImageUrl"];
        public static DataTable dt = new DataTable();
        string Idlist = "";
        string Codelist = "";
        string stCustomerCode = string.Empty;
        string stOrderItemId = string.Empty;
        string stOrderType = string.Empty;
        string stOrderFlag = string.Empty;
        string stLazadaId = string.Empty;
        string stSellerSKU = string.Empty;
        string stLazadaSKU = string.Empty;
        string stCreatedAt = string.Empty;
        string stUpdatedAt = string.Empty;
        string stOrderNumber = string.Empty;
        string stInvoiceRequired = string.Empty;
        string stCustomerName = string.Empty;
        string stCustomerEmail = string.Empty;
        string stNationalRegistrationNumber = string.Empty;
        string stShippingName = string.Empty;
        string stShippingAddress = string.Empty;
        string stShippingAddress2 = string.Empty;
        string stShippingAddress3 = string.Empty;//จังหวัด
        string stShippingAddress4 = string.Empty;
        string stShippingAddress5 = string.Empty;
        string stShippingPhoneNumber = string.Empty;
        string stShippingPhoneNumber2 = string.Empty;
        string stShippingCity = string.Empty;
        string stShippingPostcode = string.Empty;
        string stShippingCountry = string.Empty;
        string stShippingRegion = string.Empty;
        string stBillingName = string.Empty;
        string stBillingAddress = string.Empty;
        string stBillingAddress2 = string.Empty;
        string stBillingAddress3 = string.Empty;
        string stBillingAddress4 = string.Empty;
        string stBillingAddress5 = string.Empty;
        string stBillingPhoneNumber = string.Empty;
        string stBillingPhoneNumber2 = string.Empty;
        string stBillingCity = string.Empty;
        string stBillingPostcode = string.Empty;
        string stBillingCountry = string.Empty;
        string stTaxCode = string.Empty;
        string stBranchNumber = string.Empty;
        string stTaxInvoiceRequested = string.Empty;
        string stPaymentMethod = string.Empty;
        string stPaidPrice = string.Empty;
        string stUnitPrice = string.Empty;
        decimal dPaidPrice = 0;
        decimal dUnitPrice = 0;
        decimal dShippingFee = 0;
        string stShippingFee = string.Empty;
        string stWalletCredits = string.Empty;
        string stItemName = string.Empty;
        string stVariation = string.Empty;
        string stCDShippingProvider = string.Empty;
        string stShippingProvider = string.Empty;
        string stShipmentTypeName = string.Empty;
        string stShippingProviderType = string.Empty;
        string stCDTrackingCode = string.Empty;
        string stTrackingCode = string.Empty;
        string stTrackingURL = string.Empty;
        string stShippingProviderfirstmile = string.Empty;
        string stTrackingCodefirstmile = string.Empty;
        string stTrackingURLfirstmile = string.Empty;
        string stPromisedshippingtime = string.Empty;
        string stPremium = string.Empty;
        string stStatus = string.Empty;
        string stCancel = string.Empty;
        string stReason = string.Empty;
        string stReasonDetail = string.Empty;
        string stEditor = string.Empty;
        string stBundleID = string.Empty;
        string stBundleDiscount = string.Empty;
        string stRefundAmount = string.Empty;
        string stPromotionCode = string.Empty;
        string stPromotionName = string.Empty;
        string stCampaignCode = string.Empty;
        string stProductCode = string.Empty;
        string stPromotionTypeCode = string.Empty;
        string stDiscountAmount = string.Empty;
        string stDiscountPercent = string.Empty;
        string stSumPrice = string.Empty;
        string stItemAmount = string.Empty;

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
                    APIUrl = empInfo.ConnectionAPI;
                    

                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                
                LoadOffsetInventoryDetail();
            }
        }
        #region function
        protected void LoadOffsetInventoryDetail()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = "-99";
                data["ProductCode"] = "-99";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lPOInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            DivSubmitUpload.Visible = true;
            btnSubmitImport.Visible = false;
            btnCancel.Visible = false;

            gvProductImport.DataSource = lPOInfo;
            gvProductImport.DataBind();
        }
        protected List<ProductCategoryInfo> GetProductCategory(string productcategorycode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductCategoryNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCategoryCode"] = productcategorycode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductCategoryInfo> lProductCategoryInfo = JsonConvert.DeserializeObject<List<ProductCategoryInfo>>(respstr);

            return lProductCategoryInfo;
        }
        protected List<ProductBrandInfo> GetProductBrand(string productbrand)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = productbrand;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lProductBrandInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            return lProductBrandInfo;
        }
        protected List<LookupInfo> GetUnitdetail(string unitcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupCode"] = unitcode;
                data["LookupType"] = "UNIT";
                data["FlagDelete"] = "N";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            return lookupInfo;
        }
        protected List<InventoryDetailInfoNew> GetInventorydetail(string inventorycode, string productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = inventorycode;
                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lInventoryDetailInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            return lInventoryDetailInfo;
        }
        private DataTable ConvertToDataTable(ExcelWorksheet worksheet)
        {
            int totalRows = worksheet.Dimension.End.Row;
            DataTable dt = new DataTable(worksheet.Name);
            DataRow dr = null;
           
            //*** Column **/
            dt.Columns.Add("OrderItemId");
            dt.Columns.Add("OrderType");
            dt.Columns.Add("OrderFlag");
            dt.Columns.Add("LazadaId");
            dt.Columns.Add("SellerSKU"); 
            dt.Columns.Add("LazadaSKU");
            dt.Columns.Add("CreatedAt");
            dt.Columns.Add("UpdatedAt");
            dt.Columns.Add("OrderNumber");
            dt.Columns.Add("InvoiceRequired");
            dt.Columns.Add("CustomerName");
            dt.Columns.Add("CustomerEmail");
            dt.Columns.Add("NationalRegistrationNumber");
            dt.Columns.Add("ShippingName");
            dt.Columns.Add("ShippingAddress");
            dt.Columns.Add("ShippingAddress2");
            dt.Columns.Add("ShippingAddress3");//จังหวัด
            dt.Columns.Add("ShippingAddress4");
            dt.Columns.Add("ShippingAddress5");
            dt.Columns.Add("ShippingPhoneNumber");
            dt.Columns.Add("ShippingPhoneNumber2");
            dt.Columns.Add("ShippingCity");
            dt.Columns.Add("ShippingPostcode");
            dt.Columns.Add("ShippingCountry");
            dt.Columns.Add("ShippingRegion");
            dt.Columns.Add("BillingName");
            dt.Columns.Add("BillingAddress");
            dt.Columns.Add("BillingAddress2");
            dt.Columns.Add("BillingAddress3");
            dt.Columns.Add("BillingAddress4");
            dt.Columns.Add("BillingAddress5");
            dt.Columns.Add("BillingPhoneNumber");
            dt.Columns.Add("BillingPhoneNumber2");
            dt.Columns.Add("BillingCity");
            dt.Columns.Add("BillingPostcode");
            dt.Columns.Add("BillingCountry");
            dt.Columns.Add("TaxCode");
            dt.Columns.Add("BranchNumber");
            dt.Columns.Add("TaxInvoiceRequested");
            dt.Columns.Add("PaymentMethod");
            dt.Columns.Add("PaidPrice", typeof(decimal));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("ShippingFee", typeof(decimal));
            dt.Columns.Add("WalletCredits");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("Variation");
            dt.Columns.Add("CDShippingProvider");
            dt.Columns.Add("ShippingProvider");
            dt.Columns.Add("ShipmentTypeName");
            dt.Columns.Add("ShippingProviderType");
            dt.Columns.Add("CDTrackingCode");
            dt.Columns.Add("TrackingCode");
            dt.Columns.Add("TrackingURL");
            dt.Columns.Add("ShippingProviderfirstmile");
            dt.Columns.Add("TrackingCodefirstmile");
            dt.Columns.Add("TrackingURLfirstmile");
            dt.Columns.Add("Promisedshippingtime");
            dt.Columns.Add("Premium");
            dt.Columns.Add("Status");
            dt.Columns.Add("Cancel / Return Initiator");
            dt.Columns.Add("Reason");
            dt.Columns.Add("ReasonDetail");
            dt.Columns.Add("Editor");
            dt.Columns.Add("BundleID");
            dt.Columns.Add("BundleDiscount");
            dt.Columns.Add("RefundAmount");
            dt.Columns.Add("ItemAmount");
            dt.Columns.Add("SumPrice");
            dt.Columns.Add("CampaignCode");
            dt.Columns.Add("PromotionCode");
            dt.Columns.Add("PromotionName");
            dt.Columns.Add("ProductCode");
            dt.Columns.Add("PromotionTypeCode");
            dt.Columns.Add("DiscountAmount");
            dt.Columns.Add("DiscountPercent");



            
            int j;
                for (int i = 2; i <= totalRows; i++)
                {
                //*** Rows ***//
                     List<ProductInfo> pinfo = new List<ProductInfo>();
                    j = 1;

                    stOrderItemId = worksheet.Cells[i, j].Text.ToString().Trim(); j++; 
                    stOrderType = worksheet.Cells[i, j].Text.ToString().Trim(); j++; 
                    stOrderFlag= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stLazadaId= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stSellerSKU= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stLazadaSKU= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stCreatedAt= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stUpdatedAt= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stOrderNumber= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stInvoiceRequired= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stCustomerName= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stCustomerEmail= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stNationalRegistrationNumber= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingName= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingAddress= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingAddress2= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingAddress3= worksheet.Cells[i, j].Text.ToString().Trim(); j++;//จังหวัด
                    stShippingAddress4= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingAddress5= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingPhoneNumber= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingPhoneNumber2= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingCity= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingPostcode= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingCountry= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingRegion= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingName= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingAddress= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingAddress2= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingAddress3= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingAddress4= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingAddress5= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingPhoneNumber= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingPhoneNumber2= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingCity= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingPostcode= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBillingCountry= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stTaxCode= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBranchNumber= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stTaxInvoiceRequested= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stPaymentMethod= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stPaidPrice= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    decimal.TryParse(stPaidPrice, out dPaidPrice);
                    stUnitPrice = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    decimal.TryParse(stUnitPrice, out dUnitPrice);
                    stShippingFee = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    decimal.TryParse(stShippingFee, out dShippingFee);
                    stWalletCredits = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stItemName= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stVariation= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stCDShippingProvider= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingProvider= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShipmentTypeName= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingProviderType= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stCDTrackingCode= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stTrackingCode= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stTrackingURL= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stShippingProviderfirstmile = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stTrackingCodefirstmile = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stTrackingURLfirstmile = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stPromisedshippingtime= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stPremium= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stStatus= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stCancel= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stReason= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stReasonDetail= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stEditor= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBundleID= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stBundleDiscount= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stRefundAmount= worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    stItemName = stItemName + " " + stVariation + " " + stCDShippingProvider;
                    pinfo = ListCampaignPromotion(stItemName.Trim(), stCreatedAt);

                    if (pinfo.Count() <= 0)
                    {
                    DivSubmitUpload.Visible = false;
                    btnSubmitImport.Visible = false;
                    btnCancel.Visible = false;

                    stProductCode = "";
                    stPromotionCode = "";
                    stPromotionName = "";
                    stCampaignCode = "";
                    stPromotionTypeCode = "";
                    stDiscountAmount = "";
                    stDiscountPercent = "";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                            "alert('ข้อมูลอาจจะชื่อสินค้า หรือ เวลาของสินค้าไม่อยู่ใน Range ทำให้ไม่สามารถค้าหาโปรโมชันได้');", true);
                    }
                    else
                    {
                        stProductCode = pinfo[0].ProductCode.ToString().Trim();
                        stPromotionCode = pinfo[0].PromotionCode.ToString().Trim();
                        stPromotionName = pinfo[0].PromotionName.ToString().Trim();
                        stCampaignCode = pinfo[0].CampaignCode.ToString().Trim();
                        stPromotionTypeCode = pinfo[0].PromotionTypeCode.ToString().Trim();
                        stDiscountAmount = pinfo[0].DiscountAmount.ToString().Trim();
                        stDiscountPercent = pinfo[0].DiscountPercent.ToString().Trim();
                    }
                   
                 


                dr = dt.NewRow();
                    dr["OrderItemId"] = stOrderItemId.Trim();
                    dr["OrderType"] = stOrderType.Trim();
                    dr["OrderFlag"] = stOrderFlag.Trim();
                    dr["LazadaId"] = stLazadaId.Trim();
                    dr["SellerSKU"] = stSellerSKU.Trim();
                    dr["LazadaSKU"] = stLazadaSKU.Trim();
                    dr["CreatedAt"] = stCreatedAt.Trim();
                    dr["UpdatedAt"] = stUpdatedAt.Trim();
                    dr["OrderNumber"] = stOrderNumber.Trim();
                    dr["InvoiceRequired"] = stInvoiceRequired.Trim();
                    dr["CustomerName"] = stCustomerName.Trim();
                    dr["CustomerEmail"] = stCustomerEmail.Trim();
                    dr["NationalRegistrationNumber"] = stNationalRegistrationNumber.Trim();
                    dr["ShippingName"] = stShippingName.Trim();
                    dr["ShippingAddress"] = stShippingAddress.Trim();
                    dr["ShippingAddress2"] = stShippingAddress2.Trim();
                    dr["ShippingAddress3"] = stShippingAddress3.Trim();//จังหวัด
                    dr["ShippingAddress4"] = stShippingAddress4.Trim();
                    dr["ShippingAddress5"] = stShippingAddress5.Trim();
                    dr["ShippingPhoneNumber"] = stShippingPhoneNumber.Trim();
                    dr["ShippingPhoneNumber2"] = stShippingPhoneNumber2.Trim();
                    dr["ShippingCity"] = stShippingCity.Trim();
                    dr["ShippingPostcode"] = stShippingPostcode.Trim();
                    dr["ShippingCountry"] = stShippingCountry.Trim();
                    dr["ShippingRegion"] = stShippingRegion.Trim();
                    dr["BillingName"] = stBillingName.Trim();
                    dr["BillingAddress"] = stBillingAddress.Trim();
                    dr["BillingAddress2"] = stBillingAddress2.Trim();
                    dr["BillingAddress3"] = stBillingAddress3.Trim();
                    dr["BillingAddress4"] = stBillingAddress4.Trim();
                    dr["BillingAddress5"] = stBillingAddress5.Trim();
                    dr["BillingPhoneNumber"] = stBillingPhoneNumber.Trim();
                    dr["BillingPhoneNumber2"] = stBillingPhoneNumber2.Trim();
                    dr["BillingCity"] = stBillingCity.Trim();
                    dr["BillingPostcode"] = stBillingPostcode.Trim();
                    dr["BillingCountry"] = stBillingCountry.Trim();
                    dr["TaxCode"] = stTaxCode.Trim();
                    dr["BranchNumber"] = stBranchNumber.Trim();
                    dr["TaxInvoiceRequested"] = stTaxInvoiceRequested.Trim();
                    dr["PaymentMethod"] = stPaymentMethod.Trim();
                    dr["PaidPrice"] = dPaidPrice;
                    dr["UnitPrice"] = dUnitPrice;
                    dr["ShippingFee"] = dShippingFee;
                    dr["WalletCredits"] = stWalletCredits.Trim();
                    dr["ItemName"] = stItemName.Trim();
                    dr["Variation"] = stVariation.Trim();
                    dr["CDShippingProvider"] = stCDShippingProvider.Trim();
                    dr["ShippingProvider"] = stShippingProvider.Trim();
                    dr["ShipmentTypeName"] = stShipmentTypeName.Trim();
                    dr["ShippingProviderType"] = stShippingProviderType.Trim();
                    dr["CDTrackingCode"] = stCDTrackingCode.Trim();
                    dr["TrackingCode"] = stTrackingCode.Trim();
                    dr["TrackingURL"] = stTrackingURL.Trim();
                    dr["ShippingProviderfirstmile"] = stShippingProviderfirstmile.Trim();
                    dr["TrackingCodefirstmile"] = stTrackingCodefirstmile.Trim();
                    dr["TrackingURLfirstmile"] = stTrackingURLfirstmile.Trim();
                    dr["Promisedshippingtime"] = stPromisedshippingtime.Trim();
                    dr["Premium"] = stPremium.Trim();
                    dr["Status"] = stStatus.Trim();
                    dr["Cancel / Return Initiator"] = stCancel.Trim();
                    dr["Reason"] = stReason.Trim();
                    dr["ReasonDetail"] = stReasonDetail.Trim();
                    dr["Editor"] = stEditor.Trim();
                    dr["BundleID"] = stBundleID.Trim();
                    dr["BundleDiscount"] = stBundleDiscount.Trim();
                    dr["RefundAmount"] = stRefundAmount.Trim();
                    dr["CampaignCode"] = stCampaignCode.Trim();
                    dr["PromotionCode"] = stPromotionCode.Trim();
                    dr["PromotionName"] = stPromotionName.Trim();
                    dr["ProductCode"] = stProductCode.Trim();
                    dr["PromotionTypeCode"] = stPromotionTypeCode.Trim();

                dt.Rows.Add(dr);
                }
            
            stReason = "";
            return dt;
        }
        protected Boolean ValidateImport()
        {
            Boolean flag = true;
            string error = "";
            int counterr = 0;

            

            if (counterr > 0)
            {
                btnSubmitImport.Visible = false;
                btnCancel.Visible = false;

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }
            else
            {
                btnSubmitImport.Visible = true;
                btnCancel.Visible = true;
            }

            return flag;
        }
        protected Boolean ValidateImportInsert()
        {
            Boolean flag = true;
            string error = "";
            int counterr = 0;
            int line = 0;
            int Countinsert = 0;

            
            List<ProductInfo> lpInfo = new List<ProductInfo>();
            foreach (GridViewRow row in gvProductImport.Rows)
            {

                Label lblProductCode = (Label)row.FindControl("lblProductCode");
                Label lblProductName = (Label)row.FindControl("lblProductName");
                Label lblUnit = (Label)row.FindControl("lblUnit");
                Label lblPrice = (Label)row.FindControl("lblPrice");
                Label lblMerchant = (Label)row.FindControl("lblMerchantName");
                HiddenField hidProductCategoryCode = (HiddenField)row.FindControl("hidProductCategoryCode");
                HiddenField hidProductBrandCode = (HiddenField)row.FindControl("hidProductBrandCode");
                List<MerchantInfo> lMerName = new List<MerchantInfo>();

                line++;
                var productcode = lblProductCode.Text;
                var productname = lblProductName.Text;
                var merchantName = lblMerchant.Text;
                var unit = lblUnit.Text;
                var price = lblPrice.Text;
                var HIDProductCategoryCode = hidProductCategoryCode.Value;
                var HIDProductBrandCode = hidProductBrandCode.Value;
                

                if (productcode == "")
                {
                    flag = false;
                    counterr++;
                    error += "ตรวจพบ productcode เป็นค่าว่างในแถวที่" + line + "โปรดตรวจสอบไฟล์\\n";
                }
                
                if (productname == "")
                {
                    flag = false;
                    counterr++;
                    error += "ตรวจพบ productname เป็นค่าว่างในแถวที่" + line + "โปรดตรวจสอบไฟล์\\n";
                }
                               
                if (HIDProductCategoryCode == "")
                {
                    flag = false;
                    counterr++;
                    error += "ตรวจพบ CategoryCode เป็นค่าว่างในแถวที่" + line + "โปรดตรวจสอบไฟล์\\n";
                }
                if (HIDProductBrandCode == "")
                {
                    flag = false;
                    counterr++;
                    error += "ตรวจพบ BrandCode เป็นค่าว่างในแถวที่" + line + "โปรดตรวจสอบไฟล์\\n";
                }
                if (unit == "")
                {
                    flag = false;
                    counterr++;
                    error += "ตรวจพบ unit เป็นค่าว่างในแถวที่" + line + "โปรดตรวจสอบไฟล์\\n";
                }
                if (price == "")
                {
                    flag = false;
                    counterr++;
                    error += "ตรวจพบ price เป็นค่าว่างในแถวที่" + line + "โปรดตรวจสอบไฟล์\\n";
                }
                if (merchantName == "")
                {
                    flag = false;
                    counterr++;
                    error += "ตรวจพบ Merchant Name เป็นค่าว่างในแถวที่" + line + "โปรดตรวจสอบไฟล์\\n";
                }
                

            }
                        
            List<ProductInfo> lidt = new List<ProductInfo>();

            foreach (GridViewRow row in gvProductImport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hidProductImportDup = (HiddenField)row.FindControl("hidProductImportDup");

                    ProductInfo idt = new ProductInfo();

                    idt.ProductCode = hidProductImportDup.Value;
                    lidt.Add(idt);
                }
            }

            var ProductCodeDupList = lidt.GroupBy(e => e.ProductCode).Select(g =>
            {
                var item = g.First();
                return new ProductInfo
                {
                    ProductCode = item.ProductCode,
                };
            }).ToList();

            string strshowproductdup = "";

            foreach (var oh in ProductCodeDupList.ToList())
            {
                if (oh.ProductCode != "")
                {
                    if (strshowproductdup != "")
                    {
                        strshowproductdup += "," + oh.ProductCode;
                    }
                    else
                    {
                        strshowproductdup += "" + oh.ProductCode + "";
                    }
                }
            }

            if (strshowproductdup == "")
            {
                flag = (!flag) ? false : true;
            }
            else
            {
                flag = false;
                error += "ProductCode ที่นำเข้าซ้ำดังนี้ " + strshowproductdup + "\\n";
                counterr++;
            }
            Boolean flagbalance = true;

            foreach (GridViewRow row in gvProductImport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblUnit = (Label)row.FindControl("lblUnit");


                    if (lblUnit.Text != "")
                    {
                        flagbalance = (!flagbalance) ? false : true;
                    }
                    else
                    {
                        flagbalance = false;
                    }
                }
            }

            if (flagbalance == false)
            {
                flag = false;
                error += "Unit ของสินค้่าในคลังต้องไม่ติดลบ \\n";
                counterr++;
            }

            Boolean flagMerchant = true;

            List<MerchantInfo> lMer = new List<MerchantInfo>();

            foreach (GridViewRow row in gvProductImport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblMerchantName = (Label)row.FindControl("lblMerchantName");

                    lMer = GetMerValidateInsert(lblMerchantName.Text);

                    if (lMer.Count == 0)
                    {
                        flagMerchant = (!flagMerchant) ? false : true;
                    }
                    else
                    {
                        flagMerchant = false;
                    }
                }

                if (flagMerchant == true)
                {
                    flag = false;
                    counterr++;
                    error += "Mercahnt Name ไม่ตรงกับ Merchant ที่เลือก\\n";
                }
            }

            Boolean flagproductcate = true;
            List<ProductCategoryInfo> lproductcat = new List<ProductCategoryInfo>();

            foreach (GridViewRow row in gvProductImport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hidProductCategoryCode = (HiddenField)row.FindControl("hidProductCategoryCode");

                    lproductcat = GetProductCategory(hidProductCategoryCode.Value);

                    if (lproductcat.Count > 0)
                    {
                        flagproductcate = (!flagproductcate) ? false : true;
                    }
                    else
                    {
                        flagproductcate = false;
                    }
                }
            }

            if (flagproductcate == false)
            {
                flag = false;
                error += "ProductCategory ไม่พบในระบบ \\n";
                counterr++;
            }

            Boolean flagproductbrand = true;
            List<ProductBrandInfo> lpbInfo = new List<ProductBrandInfo>();

            foreach (GridViewRow row in gvProductImport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hidProductBrandCode = (HiddenField)row.FindControl("hidProductBrandCode");

                    lpbInfo = GetProductBrand(hidProductBrandCode.Value);

                    if (lpbInfo.Count > 0)
                    {
                        flagproductbrand = (!flagproductbrand) ? false : true;
                    }
                    else
                    {
                        flagproductbrand = false;
                    }
                }
            }
            

            if (counterr > 0)
            {
                btnSubmitImport.Visible = false;
                btnCancel.Visible = false;

                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');", true);
            }
            else
            {
                btnSubmitImport.Visible = true;
                btnCancel.Visible = true;
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

        protected List<ProductInfo> GetProductCodeValidateInsert(string productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ProductCodeValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }
        protected List<ProductInfo> GetProductCodeValidateInventorydetail(string productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ProductCodeValidateInventorydetail";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }
        #endregion

        #region binding
        
        public int? runningNo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductRunningNumber";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }
        public int? getMaxOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/getMaximOrder";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Year"] = DateTime.Now.Year.ToString();
                data["Month"] = DateTime.Now.Month.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;

        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelPath = "";
            string FileName = "";
            int totalrow = 0;
            int? pluster = 0;
            string OrderCode = "";
            

            try
            {
                //*** Check HasFile And Type of File is equals excel /**
                if (ValidateImport())
                {
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
                                    dt = ConvertToDataTable(worksheet);
                                    
                                    dt = dt.DefaultView.ToTable();
                                    

                                }
                            }
                        }
                    }
                    else
                    {
                        btnSubmitImport.Visible = false;
                        btnCancel.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            //Classify Table

            DataTable tblOrder = dt.AsEnumerable().GroupBy(r => new { Col1 = r["OrderNumber"]} )
                                                  .Select(g => g.OrderBy(r => r["OrderNumber"]).First())
                                                  .CopyToDataTable();

         

            var listorder = dt.AsEnumerable().GroupBy(r => new { Col1 = r["OrderNumber"] })
                                                 .Select(a => new { PaidPrice = a.Sum(b => b.Field<decimal>("PaidPrice")), OrderNumber = a.Key})
                                                 .ToList();

            foreach (DataRow drOrder in tblOrder.Rows) //id ทั้งหมด
            {
                foreach (var Compute in listorder)
                {
                    stOrderNumber = drOrder["OrderNumber"].ToString();
                    if (stOrderNumber == Compute.OrderNumber.Col1.ToString())
                    {
                        
                        drOrder["SumPrice"] = Compute.PaidPrice;

                    }
                }
            }



            //Classify Table
            List<OrderInfo> Orderlist = tblOrder.AsEnumerable().Select(row => new OrderInfo 
            {
                OrderCode = row["OrderNumber"].ToString().Trim(),
                CreateDate = row["CreatedAt"].ToString().Trim(),
                UpdateDate = row["UpdatedAt"].ToString().Trim(),
                CustomerName = row["CustomerName"].ToString().Trim(),
                TotalPrice = row["SumPrice"].ToString().Trim(),

            }).ToList();


            gvProductImport.DataSource = Orderlist;
            gvProductImport.DataBind();
            DivSubmitUpload.Visible = true;
        }
        protected void btnSubmitImport_Clicked(object sender, EventArgs e)
        {
            int? running = 0;
            string strordercode = "";
            string ProductCode = StaticField.ProductCodeImportLazada_P00001932; 
            string CampaignCode = StaticField.CampaignCodeImportLazada_LZD001; 
            string PromotionCode = StaticField.PromotionCodeImportLazada_Prowf005; 
            string PromotionTypeCode = StaticField.PromotionTypeCode09; 
            string PromotionTypeName = StaticField.PromotionTypeName09; 
            string DiscountAmount = "";
            string DiscountPercent = "";
            string MinimumQty = "";
            string Unit = "";
            string[] Province ;
            string[] District ;
            string ProvinceCode = "";
            string DistrictCode = "";
            int runningdetail = 0;
            
            //Classify Table
            running = getMaxOrder();//เปลี่ยนเป็น Getlatest OrderCode
            String genLOTCode = "LOT" + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000}", running);
            DataTable dtOrderTmp = new DataTable();
                DataTable dtCustomerTmp = new DataTable();
               
                List<CustomerInfo> lcustomerInfo = new List<CustomerInfo>();

                DataTable tblOrder = dt.AsEnumerable().GroupBy(r => new { Col1 = r["OrderNumber"] })
                                                      .Select(g => g.OrderBy(r => r["OrderNumber"]).First())
                                                      .CopyToDataTable();

                lcustomerInfo = GetCustomerMasterByCriteria(null);


                //ทำ Order
                foreach (DataRow drOrder in tblOrder.Rows) //id ทั้งหมด
                {
                    runningdetail = 0;
                    List<CustomerInfo> newlc = new List<CustomerInfo>();
                    newlc = lcustomerInfo.Where(a => a.CustomerFName == drOrder["CustomerName"].ToString()).ToList();//เอาที่ดึงมาได้จาก Master เช็คกับของที่เข้ามา
                    if (newlc.Count > 0) //ถ้ามีให้เอา CustomerCode ยัด
                    {
                     stCustomerCode = newlc[0].CustomerCode.ToString();
                    }
                    else//ไม่มี Insert ละ Select ใหม่ละเอา CustomerCode ยัด
                    {

                        int? count = loadcountCustomer();
                        string CusCode = count.ToString().PadLeft(5, '0');
                        string CustomerCode = "C" + DateTime.Now.ToString("yyyyMMdd") + CusCode;

                    stCustomerName = drOrder["CustomerName"].ToString();
                    stBillingPhoneNumber = drOrder["BillingPhoneNumber"].ToString();
                    stShippingAddress = drOrder["ShippingAddress"].ToString();
                    stShippingAddress3 = drOrder["ShippingAddress3"].ToString();
                    stShippingAddress4 = drOrder["ShippingAddress4"].ToString();
                    stShippingAddress5 = drOrder["ShippingAddress5"].ToString();
                    stShipmentTypeName = drOrder["ShipmentTypeName"].ToString();
                    Province = stShippingAddress3.Split("/".ToCharArray());
                    District = stShippingAddress4.Split("/".ToCharArray());

                    ProvinceCode = ListProvince(Province[0]);
                    DistrictCode = ListDistrict(District[0]);

                    string respstr = "";
                        APIpath = APIUrl + "/api/support/InsertCustomerofOMS"; //Insert 

                        
                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = CustomerCode;
                            data["CustomerFName"] = stCustomerName;
                            
                            data["MerchantCode"] = hidMerCode.Value;
                            data["TitleCode"] = "-99";
                            data["Gender"] = "-99";
                            
                            data["Identification"] = "";
                            data["MaritalStatusCode"] = "-99";
                            data["OccupationCode"] = "-99";
                            data["Income"] = "0";
                            data["HomePhone"] = "";
                            data["Mail"] = "";
                            data["ContactTel"] = stBillingPhoneNumber;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        List<CustomerInfo> newcus = new List<CustomerInfo>();
                        newcus = GetCustomerMasterByCriteria("Y");
                        stCustomerCode = newcus[0].CustomerCode;


                    string respstrAddress = "";
                    APIpath = APIUrl + "/api/support/InsertCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = stCustomerCode;
                        data["AddressType"] = StaticField.AddressTypeCode01; 
                        data["Address"] = stShippingAddress;
                        data["Province"] = ProvinceCode;
                        data["District"] = DistrictCode;
                        data["ZipCode"] = stShippingAddress5;
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagActive"] = "Y";



                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstrAddress = Encoding.UTF8.GetString(response);
                    }

                }
                    var listorder = dt.AsEnumerable().GroupBy(r => new { Col1 = r["OrderNumber"] })
                                            .Select(a => new { PaidPrice = a.Sum(b => b.Field<decimal>("PaidPrice"))
                                                             , OrderNumber = a.Key 
                                                             , UnitPrice = a.Sum(b => b.Field<decimal>("UnitPrice"))
                                                             , transportPrice = a.Sum(b => b.Field<decimal>("ShippingFee"))
                                            })
                                            .ToList();

               
                    foreach (var Compute in listorder)
                    {
                        stOrderNumber = drOrder["OrderNumber"].ToString();
                        if (stOrderNumber == Compute.OrderNumber.Col1.ToString())
                        {
                            
                            drOrder["SumPrice"] = Compute.PaidPrice + Compute.transportPrice;
                            drOrder["PaidPrice"] = Compute.PaidPrice;
                            drOrder["UnitPrice"] = Compute.UnitPrice;
                            drOrder["ShippingFee"] = Compute.transportPrice;

                        }
                    }

                stPaidPrice = drOrder["PaidPrice"].ToString();
                stSumPrice = drOrder["SumPrice"].ToString();
                stPaymentMethod = drOrder["PaymentMethod"].ToString();
                stUpdatedAt = drOrder["UpdatedAt"].ToString();

                APIpath = APIUrl + "/api/support/InsertRetailOrderdata";
                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["OrderType"] = StaticField.OrderTypeCode01; 
                        data["OrderStatusCode"] = StaticField.OrderStatus_01; 
                        data["OrderStateCode"] = StaticField.OrderState_13; 
                        data["OrderSituation"] = StaticField.OrderSituation01; 
                        data["BranchCode"] = StaticField.BranchCodeImportLazada_01; 
                        data["Vat"] = "0";
                        data["PercentVat"] = "0";
                        data["SubTotalPrice"] = stPaidPrice;
                        data["TotalPrice"] = stSumPrice; 
                        data["CreateBy"] = hidEmpCode.Value;
                        data["DeliveryDate"] = stUpdatedAt;
                        data["CreateDate"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagDelete"] = "N";
                        data["CustomerCode"] = stCustomerCode;
                        data["ChannelCode"] = StaticField.ChannelCodeImportLazada_ECOM01; 
                        data["CampaignCategoryCode"] = "-99";
                        data["LotNo"] = genLOTCode; 
                        data["PlatformCode"] = StaticField.PlatformCodeImportLazada_LAZADA; 
                        data["MerchantMapCode"] = hidMerCode.Value;
                        data["MerchantMapName"] = StaticField.MerchantNameImportLazada_GP2021001; 


                    var response = wb.UploadValues(APIpath, "POST", data);

                        strordercode = Encoding.UTF8.GetString(response);
                        strordercode = strordercode.Replace("\"", "");
                    }

                string respstring = "";
                APIpath = APIUrl + "/api/support/L_paymentRetailInsert";
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["OrderCode"] = strordercode;
                    data["PaymentTypeCode"] = (stPaymentMethod != StaticField.PaymentMethod_COD) ? StaticField.PaymentMethod02 : StaticField.PaymentMethod01; 
                    data["Payamount"] = stSumPrice;
                    data["CreateBy"] = hidEmpCode.Value;
                    data["UpdateBy"] = hidEmpCode.Value;
                    data["FlagDelete"] = "N";
                    data["MerchantMapCode"] = hidMerCode.Value;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstring = Encoding.UTF8.GetString(response);
                }
                //Shipping
                stShippingFee = drOrder["ShippingFee"].ToString();

               

                string respstringShipping = "";
                APIpath = APIUrl + "/api/support/InsertRetailTransportdata";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["OrderCode"] = strordercode;
                    data["CustomerCode"] = stCustomerCode;
                    data["Address"] = stShippingAddress;
                    data["DistrictCode"] = DistrictCode;
                    data["ProvinceCode"] = ProvinceCode;
                    data["Zipcode"] = stShippingAddress5;
                    data["TransportType"] = stShipmentTypeName;
                    data["TransportPrice"] = stShippingFee;
                    data["AddressType"] = StaticField.AddressTypeCode01; 
                    data["InventoryCode"] = "-99";
                    data["UpdateBy"] = hidEmpCode.Value;
                    data["CreateBy"] = hidEmpCode.Value;
                    data["MerchantMapCode"] = hidMerCode.Value;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstringShipping = Encoding.UTF8.GetString(response);
                }
                //Billing

                stBillingAddress = drOrder["BillingAddress"].ToString();
                stBillingAddress3 = drOrder["BillingAddress3"].ToString();
                stBillingAddress4 = drOrder["BillingAddress4"].ToString();
                stBillingAddress5 = drOrder["BillingAddress5"].ToString();

                Province = stBillingAddress3.Split("/".ToCharArray());
                District = stBillingAddress4.Split("/".ToCharArray());
                ProvinceCode = ListProvince(Province[0]);
                DistrictCode = ListDistrict(District[0]);

                string respstringBilling = "";
                APIpath = APIUrl + "/api/support/InsertRetailTransportdata";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["OrderCode"] = strordercode;
                    data["CustomerCode"] = stCustomerCode;
                    data["Address"] = stBillingAddress;
                    data["DistrictCode"] = DistrictCode;
                    data["ProvinceCode"] = ProvinceCode;
                    data["Zipcode"] = stBillingAddress5;
                    data["TransportType"] = stShipmentTypeName;
                    data["TransportPrice"] = stShippingFee;
                    data["AddressType"] = StaticField.AddressTypeCode02; 
                    data["InventoryCode"] = "-99";
                    data["UpdateBy"] = hidEmpCode.Value;
                    data["CreateBy"] = hidEmpCode.Value;
                    data["MerchantMapCode"] = hidMerCode.Value;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstringBilling = Encoding.UTF8.GetString(response);
                }

                DataTable tblFilItem = dt.AsEnumerable().Where(row => row.Field<String>("OrderNumber") == drOrder["OrderNumber"].ToString())
                                                                 .GroupBy(r => new { Col1 = r["ItemName"] })
                                                                 .Select(g => g.OrderBy(r => r["OrderNumber"]).First())
                                                                 .CopyToDataTable(); //เหลือนับจำนวน กับ Sum ราคา

                DataTable listCompute = dt.AsEnumerable().Where(ro => ro.Field<String>("OrderNumber") == drOrder["OrderNumber"].ToString())
                                                         .GroupBy(r => new { Col1 = r["OrderNumber"], Col2 = r["ItemName"], Col3 = r["PaidPrice"], Col4 = r["ShippingFee"] , Col5 = r["UnitPrice"] })
                                                         .Select(g =>
                                                         {
                                                            var rows = dt.NewRow();
                                                            rows["OrderNumber"] = g.Key.Col1.ToString();
                                                            rows["ItemName"] = g.Key.Col2.ToString();
                                                            rows["UnitPrice"] = decimal.Parse(g.Key.Col5.ToString());
                                                            rows["PaidPrice"] = (decimal.Parse(g.Key.Col3.ToString()) * g.Count());
                                                            rows["SumPrice"] = (decimal.Parse(g.Key.Col3.ToString()) * g.Count()) + decimal.Parse(g.Key.Col4.ToString());
                                                            rows["ShippingFee"] = decimal.Parse(g.Key.Col4.ToString());
                                                            rows["ItemAmount"] = g.Count();
                                                            return rows;
                                                         }).CopyToDataTable(); //เหลือนับจำนวน กับ Sum ราคา

                foreach (DataRow drItem in tblFilItem.Rows)//DetailGroupby
                    {//findCampaignPromotion
                        foreach (DataRow Compute in listCompute.Rows)
                        {
                            if (drItem["OrderNumber"].ToString() == Compute["OrderNumber"].ToString() && drItem["ItemName"].ToString() == Compute["ItemName"].ToString())
                            {
                                drItem["UnitPrice"] = Compute["UnitPrice"];
                                drItem["SumPrice"] = Compute["SumPrice"];
                                drItem["ItemAmount"] = Compute["ItemAmount"];
                                drItem["PaidPrice"] = Compute["PaidPrice"];
                                drItem["ShippingFee"] = Compute["ShippingFee"];
                                
                            }
                        }
                        runningdetail++; 
                                    
                        ProductCode = drItem["ProductCode"].ToString(); 
                        PromotionCode = drItem["PromotionCode"].ToString(); 
                        CampaignCode = drItem["CampaignCode"].ToString(); 
                        stPaidPrice = drItem["PaidPrice"].ToString(); 
                        stUnitPrice = drItem["UnitPrice"].ToString(); 
                        stItemAmount = drItem["ItemAmount"].ToString(); 
                        stSumPrice = drItem["SumPrice"].ToString();
                        stShippingFee = drItem["ShippingFee"].ToString();
                        stPromotionTypeCode = drItem["PromotionTypeCode"].ToString();
                        stShipmentTypeName = drItem["ShipmentTypeName"].ToString();


                        string respstringorderdetail = "";
                        APIpath = APIUrl + "/api/support/InsertRetailOrderdetaildata";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductCode"] = ProductCode;
                            data["PromotionDetailId"] = "0";
                            data["Price"] = stPaidPrice;
                            data["ProductPrice"] = stUnitPrice;
                            data["CustomerCode"] = stCustomerCode;
                            data["NetPrice"] = stPaidPrice;
                            data["TotalPrice"] = stSumPrice;
                            data["Vat"] = "0";
                            data["Unit"] = Unit; 
                            data["Amount"] = stItemAmount;
                            data["DefaultAmount"] = "";
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["CreateBy"] = hidEmpCode.Value;
                            data["FlagDelete"] = "N";
                            data["OrderCode"] = strordercode;
                            data["ParentProductCode"] = "";
                            data["ParentPromotionCode"] = "";
                            data["FlagCombo"] = "";
                            data["ComboCode"] = ""; 
                            data["ComboName"] = "";
                            data["PromotionCode"] = PromotionCode;
                            data["CampaignCode"] = CampaignCode;
                            data["runningNo"] = runningdetail.ToString();
                            data["LockAmountFlag"] = "N";
                            data["LockCheckbox"] = "N";
                            data["FreeShipping"] = (stShippingFee != "0")? "Y" : "N";
                            data["FlagProSetHeader"] = "";
                            data["PromotionTypeCode"] = stPromotionTypeCode;
                            data["PromotionTypeName"] = "";
                            data["MOQFlag"] = "N";
                            data["MinimumQty"] = "0";
                            data["DiscountAmount"] = "0";
                            data["DiscountPercent"] = "0";
                            data["ProductDiscountPercent"] = "0";
                            data["ProductDiscountAmount"] = "0";
                            data["TransportPrice"] = stShippingFee;
                            data["SumPrice"] = stSumPrice;
                            data["InventoryCode"] = "-99";
                            data["CampaignCategoryCode"] = "-99";
                            data["ShippingBrand"] = stShipmentTypeName;
                            data["ChannelCode"] = StaticField.ChannelCodeImportLazada_ECOM01; 
                            data["MediaPlanFlag"] = "N";
                            data["CreateDate"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstringorderdetail = Encoding.UTF8.GetString(response);
                        }
                     }

                }
                string Lotstring = "";
                APIpath = APIUrl + "/api/support/InsertLotNo";
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["LotNo"] = genLOTCode;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    Lotstring = Encoding.UTF8.GetString(response);
                }

            DivSubmitUpload.Visible = false;
                btnSubmitImport.Visible = false;
                btnCancel.Visible = false;
            //Classify Table

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                    "alert('บันทึกข้อมูลเสร็จสิ้น');" , true);


            
        }
        protected string ListProvince(string ProvinceName)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceName"] = ProvinceName;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);

            return lProvinceInfo[0].ProvinceCode;
        }
        protected string ListDistrict(string DistrictName)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DistrictName"] = DistrictName;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);

            return lDistrictInfo[0].DistrictCode;
        }
        protected List<ProductInfo> ListCampaignPromotion(string ProductName , string StartDate)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductNopagingByEcommerce";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductName"] = ProductName;
                data["CreateDate"] = StartDate;
                data["MerchantCode"] = StaticField.MerchantCodeImportLazada_GP2021001; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> ProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return ProductInfo;
        }
        protected int? loadcountCustomer()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCustomerListByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        protected List<CustomerInfo> GetCustomerMasterByCriteria(string Currently)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Currently"] = Currently;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerInfo> listCustomerInfo = JsonConvert.DeserializeObject<List<CustomerInfo>>(respstr);


            return listCustomerInfo;
        }
        private string GetOMSLotNo(string sPropertyShortName)
        {
            string sLotNo = string.Empty;

            string month = DateTime.Now.Month.ToString("d2");
            string year = DateTime.Now.Year.ToString();
            SqlCommand objCmd = new SqlCommand();
            DataSet dsLotNo = new DataSet();
            using (SqlConnection objConn = new SqlConnection(strConn))
            {
                objConn.Open();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "sp_GeneratePeriodNo";

                objCmd.Parameters.AddWithValue("@pPrefix", SqlDbType.VarChar).Value = sPropertyShortName;
                objCmd.Parameters.AddWithValue("@pYear", SqlDbType.VarChar).Value = year;
                objCmd.Parameters.AddWithValue("@pMonth", SqlDbType.VarChar).Value = month;

                SqlDataAdapter da = new SqlDataAdapter(objCmd);
                da.Fill(dsLotNo);
                if (dsLotNo.Tables[0].Rows.Count > 0) sLotNo = dsLotNo.Tables[0].Rows[0]["PeriodNo"].ToString();

                da.Dispose();
                dsLotNo.Dispose();
                objCmd.Dispose();
                objConn.Close();

            }
            return sLotNo; ;
        }
        protected void gvImport_RowDataBound(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            string OdNum  = "";
            string ItNum = "";
            GridViewRow row = gvProductImport.Rows[index];
            Label lblOrderCode = (Label)row.FindControl("lblOrderCode");

            DataTable tblFilItem = dt.AsEnumerable().Where(ro => ro.Field<String>("OrderNumber") == lblOrderCode.Text)
                                                  .GroupBy(r => new { Col1 = r["OrderNumber"], Col2 = r["ItemName"] })
                                                  .Select(g => g.OrderBy(r => r["OrderNumber"]).First())
                                                  .CopyToDataTable();

            
            DataTable listCompute = dt.AsEnumerable().Where(ro => ro.Field<String>("OrderNumber") == lblOrderCode.Text)
                                                        .GroupBy(r => new { Col1 = r["OrderNumber"], Col2 = r["ItemName"], Col3 = r["PaidPrice"] ,Col4 = r["ShippingFee"] })
                                                        .Select(g =>
                                                        {
                                                         var rows = dt.NewRow();
                                                          rows["OrderNumber"] = g.Key.Col1.ToString();
                                                          rows["ItemName"] = g.Key.Col2.ToString();
                                                          rows["PaidPrice"] = (decimal.Parse(g.Key.Col3.ToString()) * g.Count());
                                                          rows["SumPrice"] = (decimal.Parse(g.Key.Col3.ToString()) * g.Count()) + decimal.Parse(g.Key.Col4.ToString());
                                                          rows["ShippingFee"] = decimal.Parse(g.Key.Col4.ToString());
                                                          rows["ItemAmount"] = g.Count();
                                                          return rows;
                                                        }).CopyToDataTable(); //เหลือนับจำนวน กับ Sum ราคา

            

                foreach (DataRow drItem in tblFilItem.Rows) //id ทั้งหมด
                {
                    foreach (DataRow Compute in listCompute.Rows)
                    {
                        if (drItem["OrderNumber"].ToString() == Compute["OrderNumber"].ToString() && drItem["ItemName"].ToString() == Compute["ItemName"].ToString())
                        {

                         drItem["SumPrice"] = Compute["PaidPrice"];
                         drItem["ItemAmount"] = Compute["ItemAmount"];


                        }
                    }
                }

            


            List<OrderInfo> Detaillist = tblFilItem.AsEnumerable().Select(raw => new OrderInfo 
                    {
                        OrderCode = raw["OrderNumber"].ToString().Trim(),
                        PromotionCode = raw["PromotionCode"].ToString().Trim(),
                        PromotionName = raw["PromotionName"].ToString().Trim(),
                        CreateDate = raw["CreatedAt"].ToString().Trim(),
                        UpdateDate = raw["UpdatedAt"].ToString().Trim(),
                        ProductName = raw["ItemName"].ToString().Trim(),
                        TotalPrice = raw["SumPrice"].ToString().Trim(),
                        Amount = raw["ItemAmount"].ToString().Trim(),

                    }).ToList();
           
            

            if (e.CommandName == "ShowProduct")
            {
                gvModalDetail.DataSource = Detaillist;
                gvModalDetail.DataBind();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
            }
        }
        #endregion

    }
}
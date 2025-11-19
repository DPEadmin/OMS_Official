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

namespace DOMS_TSR.src.InventoryManagement
{
    public partial class ImportProduct : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string InventoryDetailImgUrl = ConfigurationManager.AppSettings["MediaPlanImageUrl"];

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
            dt.Columns.Add("ProductCode");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductBrandCode");
            dt.Columns.Add("ProductCategoryCode");
            dt.Columns.Add("Price");
            dt.Columns.Add("Unit");
            dt.Columns.Add("SKU");
            dt.Columns.Add("Description");
            dt.Columns.Add("UpsellScript");
            dt.Columns.Add("MerchantName");

            try
            {
                int j;
                for (int i = 2; i <= totalRows; i++)
                {
                    //*** Rows ***//
                    dr = dt.NewRow();
                    j = 1;

                    dr["ProductCode"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ProductName"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ProductBrandCode"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ProductCategoryCode"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["Price"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["Unit"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["SKU"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["Description"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["UpsellScript"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MerchantName"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
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
        
        #endregion

        #region event
        
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
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelPath = "";
            string FileName = "";
            int totalrow = 0;
            int? running = 0;
            int? pluster = 0;
            string pdcode = "";
            DataTable dt = new DataTable();

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
            running = runningNo();
            List<ProductInfo> Productlist = dt.AsEnumerable().Select(row => new ProductInfo
            {
                ProductName = row["ProductName"].ToString().Trim(),
                ProductCategoryCode = row["ProductCategoryCode"].ToString().Trim(),
                ProductBrandCode = row["ProductBrandCode"].ToString().Trim(),
                Price = (row["Price"].ToString() != "") ? Convert.ToDouble(row["Price"]) : 0,
                Unit = row["Unit"].ToString().Trim(),
                Sku = row["SKU"].ToString().Trim(),
                Description = row["Description"].ToString().Trim(),
                UpsellScript = row["UpsellScript"].ToString().Trim(),
                MerchantName = row["MerchantName"].ToString().Trim(),


            }).ToList();

            List<ProductCategoryInfo> lpcgInfo = new List<ProductCategoryInfo>();
            List<ProductBrandInfo> lpbInfo = new List<ProductBrandInfo>();
            List<InventoryDetailInfoNew> lidtInfo = new List<InventoryDetailInfoNew>();
            List<LookupInfo> lkpInfo = new List<LookupInfo>(); //getunit
            List<MerchantInfo> lMerInfo = new List<MerchantInfo>();

            foreach (var od in Productlist.ToList())
            {
                pdcode = "P0000" + running.ToString();
                od.InventoryCode = hidInventorySelected.Value;
                od.CreateDate = DateTime.Now.ToString();
                od.CreateBy = hidEmpCode.Value;
                od.UpdateDate = DateTime.Now.ToString();
                od.UpdateBy = hidEmpCode.Value;
                od.ProductCode = pdcode;
                running++;
                

                lpcgInfo = GetProductCategory(od.ProductCategoryCode);
                lpbInfo = GetProductBrand(od.ProductBrandCode);
                lidtInfo = GetInventorydetail(hidInventorySelected.Value, od.ProductCode);
                lkpInfo = GetUnitdetail(od.Unit);


                if (lpcgInfo.Count > 0)
                {
                    od.ProductCategoryName = lpcgInfo[0].ProductCategoryName;
                }

                if (lpbInfo.Count > 0)
                {
                    od.ProductBrandName = lpbInfo[0].ProductBrandName;
                }
                if (lkpInfo.Count > 0)
                {
                    od.UnitName = lkpInfo[0].LookupValue;
                }

                
            }

            var ProductCodeDupList = Productlist.GroupBy(x => x.ProductCode)
                        .Where(g => g.Count() > 1)
                        .Select(y => y.Key)
                        .ToList();

            foreach (var oi in ProductCodeDupList.ToList())
            {
                foreach (var oj in Productlist.ToList())
                {
                    if (oi == oj.ProductCode)
                    {
                        oj.ProductCodeImportDup = oi;
                    }
                }
            }

            gvProductImport.DataSource = Productlist;
            gvProductImport.DataBind();
            DivSubmitUpload.Visible = true;
        }
        protected void btnSubmitImport_Clicked(object sender, EventArgs e)
        {
            if (ValidateImportInsert())
            {
                List<ProductInfo> lpInfo = new List<ProductInfo>();


                int? sum = 0;
                int? sum1 = 0;

                foreach (GridViewRow row in gvProductImport.Rows)
                {
                    string merchantcode = "";
                    Label lblProductCode = (Label)row.FindControl("lblProductCode");
                    Label lblProductName = (Label)row.FindControl("lblProductName");
                    Label lblUnit = (Label)row.FindControl("lblUnit");
                    Label lblPrice = (Label)row.FindControl("lblPrice");
                    Label lblSku = (Label)row.FindControl("lblSku");
                    Label lblDescription = (Label)row.FindControl("lblDescription");
                    Label lblUpsellScript = (Label)row.FindControl("lblUpsellScript");
                    HiddenField hidProductCategoryCode = (HiddenField)row.FindControl("hidProductCategoryCode");
                    HiddenField hidProductBrandCode = (HiddenField)row.FindControl("hidProductBrandCode");
                    HiddenField hidUnit = (HiddenField)row.FindControl("hidUnit");
                    
                    HiddenField hidProductID = (HiddenField)row.FindControl("hidProductID");

                    lpInfo = GetProductCodeValidateInsert(lblProductCode.Text);

                    if (lpInfo.Count > 0)
                    {
                        hidFlagInsertProduct.Value = "N";

                        string aaa = lpInfo[0].ProductId.ToString();
                        hidProductID.Value = lpInfo[0].ProductId.ToString();
                    }
                    else
                    {
                        hidFlagInsertProduct.Value = "Y";
                    }

                    lpInfo = GetProductCodeValidateInventorydetail(lblProductCode.Text);
                    if (lpInfo.Count > 0)
                    {

                        merchantcode = "";
                    }
                    else
                    {
                        merchantcode = "-99";
                    }


                    // Insert Update Product Master
                    if (hidFlagInsertProduct.Value == "Y")
                    {
                        sum = 0;
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertProductfromImportProduct";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductCode"] = lblProductCode.Text;
                            data["ProductName"] = lblProductName.Text;
                            data["ProductCategoryCode"] = hidProductCategoryCode.Value;
                            data["ProductBrandCode"] = hidProductBrandCode.Value;
                            data["MerchantCode"] = hidMerCode.Value;

                            data["Price"] = lblPrice.Text;
                            data["Unit"] = hidUnit.Value;
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagDelete"] = "N";
                            data["Sku"] = lblSku.Text;
                            data["Description"] = lblDescription.Text;
                            data["UpsellScript"] = lblUpsellScript.Text;

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);

                            sum = JsonConvert.DeserializeObject<int?>(respstr);
                        }
                    }
                    else
                    {
                        sum = 0;
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateProductfromImportProduct";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductId"] = hidProductID.Value;
                            data["ProductCode"] = lblProductCode.Text;
                            data["ProductName"] = lblProductName.Text;
                            data["ProductCategoryCode"] = hidProductCategoryCode.Value;
                            data["ProductBrandCode"] = hidProductBrandCode.Value;
                            data["MerchantCode"] = merchantcode;
                            data["Price"] = lblPrice.Text;
                            data["Unit"] = hidUnit.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["Sku"] = lblSku.Text;
                            data["Description"] = lblDescription.Text;
                            data["UpsellScript"] = lblUpsellScript.Text;

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);

                            sum = JsonConvert.DeserializeObject<int?>(respstr);
                        }
                    }

                    
                }

                if (sum > 0 || sum1 > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                        "alert('บันทึกข้อมูลเสร็จสิ้น'); window.location='" +
                        Request.ApplicationPath + "../src/Product/Product.aspx"+ "'", true);
                }
            }
        }
        protected void gvInventoryDetailImport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblProductCode = (Label)e.Row.FindControl("lblProductCode");
                Label lblSafetyStock = (Label)e.Row.FindControl("lblSafetyStock");
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                HiddenField hidBalance = (HiddenField)e.Row.FindControl("hidBalance");
                Label lblReOrder = (Label)e.Row.FindControl("lblReOrder");
                HiddenField hidProductImportDup = (HiddenField)e.Row.FindControl("hidProductImportDup");

                if (hidProductImportDup.Value == "" || hidProductImportDup.Value == null)
                {

                }
                else
                {
                    lblProductCode.ForeColor = System.Drawing.Color.Red;
                }

                
            }
        }
        #endregion

    }
}
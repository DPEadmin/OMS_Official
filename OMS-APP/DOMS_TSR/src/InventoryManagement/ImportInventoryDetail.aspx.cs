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
    public partial class ImportInventoryDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
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
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                EmpInfo empInfo = new EmpInfo();
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                BindddlInventory();
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

                data["InventoryCode"] = ddlInventory.SelectedValue;
                data["ProductCode"] = "-99";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lPOInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            DivSubmitUpload.Visible = true;
            btnSubmitImport.Visible = false;
            btnCancel.Visible = false;

            gvInventoryDetailImport.DataSource = lPOInfo;
            gvInventoryDetailImport.DataBind();
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
        protected List<InventoryDetailInfoNew> GetInventorydetail(string inventorycode, string productcode,string PoNo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = inventorycode;
                data["ProductCode"] = productcode;
                data["POCode"] = PoNo;
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
            dt.Columns.Add("ProductCategoryCode");
            dt.Columns.Add("ProductBrandCode");
            dt.Columns.Add("QTY");
            dt.Columns.Add("SafetyStock");
            dt.Columns.Add("Price");
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
                    dr["ProductCategoryCode"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ProductBrandCode"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["QTY"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["SafetyStock"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["Price"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
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

            if (hidInventorySelected.Value == "-99" || hidInventorySelected.Value == "" || hidInventorySelected.Value == null)
            {
                flag = false;
                error += "กรุณาระบุคลังสินค้า \\n";
                LbInvenvarlid.Text = "*";
                counterr++;
            }
            else
            {                
                flag = (!flag) ? false : true;
            }
            if (txtPoNo.Text == "-99" || txtPoNo.Text == "" || txtPoNo.Text == null)
            {
                flag = false;
                error += "กรุณาระบุเลข PO \\n";
                LbPoNovarlid.Text = "*";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }
            if  (counterr > 0)
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

            if (hidInventorySelected.Value == "-99" || hidInventorySelected.Value == "" || hidInventorySelected.Value == null)
            {
                flag = false;
                error += "กรุณาระบุคลังสินค้า \\n";
                LbInvenvarlid.Text = "*";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }
            if (txtPoNo.Text == "-99" || txtPoNo.Text == "" || txtPoNo.Text == null)
            {
                flag = false;
                error += "กรุณาระบุเลข PO \\n";
                LbPoNovarlid.Text = "*";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;
            }

            List<InventoryDetailInfoNew> lidt = new List<InventoryDetailInfoNew>();

            foreach (GridViewRow row in gvInventoryDetailImport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hidProductImportDup = (HiddenField)row.FindControl("hidProductImportDup");

                    InventoryDetailInfoNew idt = new InventoryDetailInfoNew();

                    idt.ProductCode = hidProductImportDup.Value;
                    lidt.Add(idt);
                }
            }

            var ProductCodeDupList = lidt.GroupBy(e => e.ProductCode).Select(g =>
            {
                var item = g.First();
                return new InventoryDetailInfoNew
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

            foreach (GridViewRow row in gvInventoryDetailImport.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hidBalance = (HiddenField)row.FindControl("hidBalance");

                    hidBalance.Value = (hidBalance.Value == "") ? "0" : hidBalance.Value;

                    if (Convert.ToInt32(hidBalance.Value) >= 0)
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
                error += "Balance ของสินค้่ในคลังต้องไม่ติดลบ \\n";
                counterr++;
            }

            Boolean flagproductcate = true;
            List<ProductCategoryInfo> lproductcat = new List<ProductCategoryInfo>();

            foreach (GridViewRow row in gvInventoryDetailImport.Rows)
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

            foreach (GridViewRow row in gvInventoryDetailImport.Rows)
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

            if (flagproductbrand == false)
            {
                flag = false;
                error += "ProductBrand ไม่พบในระบบ \\n";
                counterr++;
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
        protected List<ProductInfo> GetProductCodeValidateInsert(string productcode,string merchantcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ProductCodeValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = productcode;
                data["MerchantCode"] = merchantcode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);            

            return lProductInfo;
        }
        #endregion

        #region binding
        protected void BindddlInventory()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["MerchantCode"] = hidMerchantCode.Value;
                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lLnventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            ddlInventory.DataSource = lLnventoryInfo;
            ddlInventory.DataTextField = "InventoryName";
            ddlInventory.DataValueField = "InventoryCode";
            ddlInventory.DataBind();
            ddlInventory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("กรุณาเลือก", "-99"));
        }
        #endregion

        #region event
        protected void ddlInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidInventorySelected.Value = ddlInventory.SelectedValue;
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelPath = "";
            string FileName = "";
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

            List<InventoryDetailInfoNew> inventorydetaillist = dt.AsEnumerable().Select(row => new InventoryDetailInfoNew
            {
                ProductCode = row["ProductCode"].ToString().Trim(),
                ProductName = row["ProductName"].ToString().Trim(),
                ProductCategoryCode = row["ProductCategoryCode"].ToString().Trim(),
                ProductBrandCode = row["ProductBrandCode"].ToString().Trim(),
                QTY = (row["QTY"].ToString() != "") ? Convert.ToInt32(row["QTY"]) : 0,
                SafetyStock = (row["SafetyStock"].ToString() != "") ? Convert.ToInt32(row["SafetyStock"]) : 0,
                Price = (row["Price"].ToString() != "") ? Convert.ToInt32(row["Price"]) : 0,

            }).ToList();

            List<ProductCategoryInfo> lpcgInfo = new List<ProductCategoryInfo>();
            List<ProductBrandInfo> lpbInfo = new List<ProductBrandInfo>();
            List<InventoryDetailInfoNew> lidtInfo = new List<InventoryDetailInfoNew>();

            foreach (var od in inventorydetaillist.ToList())
            {
                od.InventoryCode = hidInventorySelected.Value;
                od.CreateDate = DateTime.Now.ToString();
                od.CreateBy = hidEmpCode.Value;
                od.UpdateDate = DateTime.Now.ToString();
                od.UpdateBy = hidEmpCode.Value;
                od.Reserved = 0;
                od.Current = od.QTY - od.Reserved;
                od.Balance = od.QTY; // Update Value 13/07/2020

                lpcgInfo = GetProductCategory(od.ProductCategoryCode);
                lpbInfo = GetProductBrand(od.ProductBrandCode);
                lidtInfo = GetInventorydetail(hidInventorySelected.Value, od.ProductCode,txtPoNo.Text);

                if (lpcgInfo.Count > 0)
                {
                    od.ProductCategoryName = lpcgInfo[0].ProductCategoryName;
                }

                if (lpbInfo.Count > 0)
                {
                    od.ProductBrandName = lpbInfo[0].ProductBrandName;
                }

                if (lidtInfo.Count > 0)
                {
                    od.InventoryDetailId = lidtInfo[0].InventoryDetailId;
                    od.QTY = od.QTY + lidtInfo[0].QTY;
                    od.Reserved = lidtInfo[0].Reserved;
                    od.Current = od.QTY - od.Reserved;
                    od.Balance = od.QTY;

                    if (od.Balance <= od.SafetyStock)
                    {
                        od.ReOrder = od.SafetyStock - od.Balance;
                        od.ReOrderCode = "Y";
                    }
                    else
                    {
                        od.ReOrder = 0;
                        od.ReOrderCode = "";
                    }
                }
                else
                {
                    if (od.Balance <= od.SafetyStock)
                    {
                        od.ReOrder = od.SafetyStock - od.Balance;
                        od.ReOrderCode = "Y";
                    }
                    else
                    {
                        od.ReOrder = 0;
                        od.ReOrderCode = "";
                    }
                }
            }

            var ProductCodeDupList = inventorydetaillist.GroupBy(x => x.ProductCode)
                        .Where(g => g.Count() > 1)
                        .Select(y => y.Key)
                        .ToList();

            foreach (var oi in ProductCodeDupList.ToList())
            {
                foreach (var oj in inventorydetaillist.ToList())
                {
                    if (oi == oj.ProductCode)
                    {
                        oj.ProductCodeImportDup = oi;
                    }
                }
            }

            gvInventoryDetailImport.DataSource = inventorydetaillist;
            gvInventoryDetailImport.DataBind();
            DivSubmitUpload.Visible = true;
        }
        protected void btnSubmitImport_Clicked(object sender, EventArgs e)
        {
            if (ValidateImportInsert())
            {
            List<ProductInfo> lpInfo = new List<ProductInfo>();
            List<InventoryDetailInfoNew> ldtInfo = new List<InventoryDetailInfoNew>();

            int? sum = 0;
            int? sum1 = 0;

            foreach (GridViewRow row in gvInventoryDetailImport.Rows)
            {
                Label lblProductCode = (Label)row.FindControl("lblProductCode");
                Label lblProductName = (Label)row.FindControl("lblProductName");
                HiddenField hidProductCategoryCode = (HiddenField)row.FindControl("hidProductCategoryCode");
                HiddenField hidProductBrandCode = (HiddenField)row.FindControl("hidProductBrandCode");
                HiddenField hidQTY = (HiddenField)row.FindControl("hidQTY");
                HiddenField hidReserved = (HiddenField)row.FindControl("hidReserved");
                HiddenField hidBalance = (HiddenField)row.FindControl("hidBalance");
                HiddenField hidCurrents = (HiddenField)row.FindControl("hidCurrents");
                Label lblSafetyStock = (Label)row.FindControl("lblSafetyStock");
                HiddenField hidReOrder = (HiddenField)row.FindControl("hidReOrder");
                HiddenField hidInventoryDetailID = (HiddenField)row.FindControl("hidInventoryDetailID");
                HiddenField hidProductID = (HiddenField)row.FindControl("hidProductID");
                    HiddenField hidPrice = (HiddenField)row.FindControl("hidPrice");
                    lpInfo = GetProductCodeValidateInsert(lblProductCode.Text, hidMerchantCode.Value);

                    if (lpInfo.Count > 0)
                    {
                        hidFlagInsertProduct.Value = "N";
                        hidProductID.Value = lpInfo[0].ProductId.ToString();
                    }
                    else
                    {
                        hidFlagInsertProduct.Value = "Y";
                    }

                    // Insert Update Product Master
                    if (hidFlagInsertProduct.Value == "Y")
                    {
                        sum = 0;
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertProductfromImportInventoryDetail";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductCode"] = lblProductCode.Text;
                            data["ProductName"] = lblProductName.Text;
                            data["ProductCategoryCode"] = hidProductCategoryCode.Value;
                            data["ProductBrandCode"] = hidProductBrandCode.Value;
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagDelete"] = "N";
                            data["Price"] = hidPrice.Value;
                            data["MerchantCode"] = hidMerchantCode.Value;
                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);

                            sum = JsonConvert.DeserializeObject<int?>(respstr);
                        }
                    }
                    else
                    {
                        sum = 0;
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateProductfromImportInventoryDetail";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductId"] = hidProductID.Value;
                            data["ProductCode"] = lblProductCode.Text;
                            data["ProductName"] = lblProductName.Text;
                            data["ProductCategoryCode"] = hidProductCategoryCode.Value;
                            data["ProductBrandCode"] = hidProductBrandCode.Value;
                            data["Price"] = hidPrice.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["MerchantCode"] = hidMerchantCode.Value;
                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);

                            sum = JsonConvert.DeserializeObject<int?>(respstr);
                        }
                    }

                    ldtInfo = GetInventorydetail(hidInventorySelected.Value, lblProductCode.Text,txtPoNo.Text);

                    if (ldtInfo.Count > 0)
                    {
                        hidFlagInsert.Value = "N";
                    }
                    else
                    {
                        hidFlagInsert.Value = "Y";
                    }

                    // Insert Update InventoryDetail
                    if (hidFlagInsert.Value == "Y")
                    {
                        sum1 = 0;
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertInventoryDetailfromUploadInvDetail";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["InventoryCode"] = hidInventorySelected.Value;
                            data["ProductCode"] = lblProductCode.Text;
                            data["QTY"] = (hidQTY.Value == "") ? "0" : hidQTY.Value;
                            data["Reserved"] = (hidReserved.Value == "") ? "0" : hidReserved.Value;
                            data["Current"] = (hidCurrents.Value == "") ? "0" : hidCurrents.Value;
                            data["Balance"] = (hidBalance.Value == "") ? "0" : hidBalance.Value;
                            data["SafetyStock"] = lblSafetyStock.Text;
                            data["ReOrder"] = hidReOrder.Value;
                            data["SafetyStock"] = lblSafetyStock.Text;
                          
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagDelete"] = "N";
                            data["POCode"] = txtPoNo.Text;
                            data["Price"] = hidPrice.Value;
                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);

                            sum1 = JsonConvert.DeserializeObject<int?>(respstr);
                        }
                    }
                    else
                    {
                        sum = 0;
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateInventoryDetailfromUploadFile";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["InventoryDetailId"] = hidInventoryDetailID.Value;
                            data["ProductCode"] = lblProductCode.Text;
                            data["QTY"] = (hidQTY.Value == "") ? "0" : hidQTY.Value;
                            data["Reserved"] = (hidReserved.Value == "") ? "0" : hidReserved.Value;
                            data["Current"] = (hidCurrents.Value == "") ? "0" : hidCurrents.Value;
                            data["Balance"] = (hidBalance.Value == "") ? "0" : hidBalance.Value;
                            data["SafetyStock"] = lblSafetyStock.Text;
                            data["ReOrder"] = hidReOrder.Value;
                           
                            data["SafetyStock"] = lblSafetyStock.Text;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["POCode"] = txtPoNo.Text;
                            data["Price"] = hidPrice.Value;
                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);

                            sum = JsonConvert.DeserializeObject<int?>(respstr);
                        }

                        if (sum > 0)
                        {
                            
                        }
                    }
                }

                if (sum > 0 || sum1 > 0)
                {
                    cleardata();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "redirect",
                        "alert('บันทึกข้อมูลเสร็จสิ้น'); window.location='" +
                        Request.ApplicationPath + "../src/InventoryManagement/InventoryDetail.aspx?InventoryCode=" + hidInventorySelected.Value + "'", true);
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

                if (Convert.ToInt32(hidBalance.Value) <= Convert.ToInt32(lblSafetyStock.Text))
                {
                    lblReOrder.ForeColor = System.Drawing.Color.Red;
                }

                if (Convert.ToInt32(hidBalance.Value) < 0)
                {
                    lblBalance.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
        #endregion

        private void cleardata() 
        {
            ddlInventory.ClearSelection();
            txtPoNo.Text = "";
            LbInvenvarlid.Text = "";
            LbPoNovarlid.Text = "";
        }
    }
}
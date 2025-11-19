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
using System.ComponentModel;

using System.IO;

namespace DOMS_TSR.src.Product
{
    public partial class ReportLazada_Excel : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string APIpath = "";
        string MerchantSession = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    
                    MerchantSession = merchantinfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                LoadDar("");
            }
        }
        protected void LoadDar(string StrDate)
        {
            DataTable dt = new DataTable();
            DataTable lorderdetailInfo = new DataTable();
            Util aaa = new Util();
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                List<ProductInfo> olist = new List<ProductInfo>();
                List<ProductInfo> resultExport = new List<ProductInfo>();
                resultExport = (List<ProductInfo>)Session["dataExportExcel"];

                var dataExcel = new NameValueCollection();

                dataExcel["MerchantCode"] = MerchantSession;
                dataExcel["ProductCode"] = Request.QueryString["pcode"];
                dataExcel["ProductName"] = Request.QueryString["pname"];
                dataExcel["ProductBrandCode"] = Request.QueryString["pbrand"];
                dataExcel["InventoryCode"] = Request.QueryString["pinventory"];


                





                var response = wb.UploadValues(APIpath, "POST", dataExcel);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lOrder = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ExportLazada.xlsx"));

            int CountNumber = 0;
            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "ProductCode");
            DataTable distinctTable;
            distinctTable = viewround.ToTable(true, "MerchantCode");
            //new datable for excel
            DataTable dtdatasale = new DataTable();
            dtdatasale.Columns.Add("Group No");
            dtdatasale.Columns.Add("catId");
            dtdatasale.Columns.Add("*ชื่อผลิตภัณฑ์");
            dtdatasale.Columns.Add("*รูปของสินค้า1");
            dtdatasale.Columns.Add("รูปของสินค้า2");
            dtdatasale.Columns.Add("รูปของสินค้า3");
            dtdatasale.Columns.Add("รูปของสินค้า4");
            dtdatasale.Columns.Add("รูปของสินค้า5");
            dtdatasale.Columns.Add("รูปของสินค้า6");
            dtdatasale.Columns.Add("รูปของสินค้า7");
            dtdatasale.Columns.Add("รูปของสินค้า8");
            dtdatasale.Columns.Add("Showcase_image1:1");
            dtdatasale.Columns.Add("Showcase_image4:3"); 
            dtdatasale.Columns.Add("originalLocalName"); 
            dtdatasale.Columns.Add("currencyCode"); 
            dtdatasale.Columns.Add("URL วิดีโอ");
            dtdatasale.Columns.Add("*ยี่ห้อ"); 
            dtdatasale.Columns.Add("ประเภทของกิจกรรม");
            dtdatasale.Columns.Add("วัสดุที่ใช้ผลิตเสื้อผ้า");
            dtdatasale.Columns.Add("ประเภทของเสื้อ");
            dtdatasale.Columns.Add("Long Description (Lorikeet)");
            dtdatasale.Columns.Add("รายละเอียดสินค้าภาษาอังกฤษแบบเต็ม (ไม่บังคับ)");
            dtdatasale.Columns.Add("สินค้าภายในกล่อง");
            dtdatasale.Columns.Add("เงื่อนไขการรับประกัน");
            dtdatasale.Columns.Add("การประกัน");
            dtdatasale.Columns.Add("ประเภทของการรับประกัน");
            dtdatasale.Columns.Add("*Package Weight (kg)");
            dtdatasale.Columns.Add("*แพคเกจขนาด (ซม.)-ความยาว (ซม.)");
            dtdatasale.Columns.Add("*แพคเกจขนาด (ซม.)-ความกว้าง (ซม.)");
            dtdatasale.Columns.Add("*แพคเกจขนาด (ซม.)-สูง (ซม.)");
            dtdatasale.Columns.Add("สินค้าอันตราย");
            dtdatasale.Columns.Add("Color Family");
            dtdatasale.Columns.Add("ไซส์");
            dtdatasale.Columns.Add("sku.skuId"); 
            dtdatasale.Columns.Add("props");
            dtdatasale.Columns.Add("ภาพ1");
            dtdatasale.Columns.Add("ภาพ2"); 
            dtdatasale.Columns.Add("ภาพ3");
            dtdatasale.Columns.Add("ภาพ4");
            dtdatasale.Columns.Add("ภาพ5");
            dtdatasale.Columns.Add("ภาพ6");
            dtdatasale.Columns.Add("ภาพ7");
            dtdatasale.Columns.Add("ภาพ8");
            dtdatasale.Columns.Add("สีรูปขนาดย่อ");
            dtdatasale.Columns.Add("*จำนวน");
            dtdatasale.Columns.Add("SpecialPrice");
            dtdatasale.Columns.Add("SpecialPrice Start");
            dtdatasale.Columns.Add("SpecialPrice End");
            dtdatasale.Columns.Add("*ราคา");
            dtdatasale.Columns.Add("SellerSKU");





            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                


                DataView ViewOrderinf_id = new DataView(dt);
                
                foreach (DataRow Detail in RoundTable.Rows)
                {
                    decimal amount = 0;
                    decimal price = 0;
                    decimal result = 0;
                    decimal OrderTotalpriceRE = 0;
                    decimal SumPrice = 0;
                    decimal SumTransPort = 0;
                    int NumRow = 1;
                    ViewOrderinf_id.RowFilter = string.Format("ProductCode ='{0}'", Detail["ProductCode"].ToString());


                    DataTable Productinfo = ViewOrderinf_id.ToTable();
                    foreach (DataRow Detailorder in Productinfo.Rows)
                    {
                            drsale = dtdatasale.NewRow(); // have new row on each iteration
                   

                        drsale["Group No"] = Detailorder["ProductCode"].ToString(); 
                        drsale["*ชื่อผลิตภัณฑ์"] = Detailorder["ProductName"].ToString();
                        drsale["*รูปของสินค้า1"] = (Detailorder["Product_img1"].ToString() == "") ? "" : APIUrl + Detailorder["Product_img1"].ToString();

                        
                        drsale["Showcase_image1:1"] = (Detailorder["Showcase_image11"].ToString() == "") ? "" : APIUrl + Detailorder["Showcase_image11"].ToString();
                        drsale["Showcase_image4:3"] = (Detailorder["Showcase_image43"].ToString() == "") ? "" : APIUrl + Detailorder["Showcase_image43"].ToString();
                        drsale["URL วิดีโอ"] = Detailorder["URLVideo"].ToString();
                        drsale["*ยี่ห้อ"] = Detailorder["ProductBrandName"].ToString();
                        
                        drsale["Long Description (Lorikeet)"] = Detailorder["ProductDesc"].ToString();
                        
                        drsale["สินค้าภายในกล่อง"] = Detailorder["ProdutAdditional"].ToString();
                        
                        drsale["*Package Weight (kg)"] = Detailorder["Weight"].ToString();
                        drsale["*แพคเกจขนาด (ซม.)-ความยาว (ซม.)"] = Detailorder["PackageLength"].ToString();
                        drsale["*แพคเกจขนาด (ซม.)-ความกว้าง (ซม.)"] = Detailorder["PackageWidth"].ToString();
                        drsale["*แพคเกจขนาด (ซม.)-สูง (ซม.)"] = Detailorder["PackageHeigth"].ToString();
                        
                        drsale["ภาพ1"] = (Detailorder["SKU_img1"].ToString() == "") ? "" : APIUrl + Detailorder["SKU_img1"].ToString();
                        
                        drsale["*จำนวน"] = Detailorder["QTY"].ToString();
                        
                        drsale["*ราคา"] = Detailorder["Price"].ToString();
                        drsale["SellerSKU"] = Detailorder["Sku"].ToString();

                        dtdatasale.Rows.Add(drsale);
                    }
                    
                }
                

            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportLazada_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
                this.Response.WriteFile(fileName);
                this.Response.End();
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('ไม่พบข้อมูล')", true);
                ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);

            }
        }


    }
}
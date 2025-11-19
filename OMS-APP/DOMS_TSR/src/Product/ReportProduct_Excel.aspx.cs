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
    public partial class ReportProduct_Excel : System.Web.UI.Page
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

                





                var response = wb.UploadValues(APIpath, "POST", dataExcel);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lOrder = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ExportProduct.xlsx"));

            int CountNumber = 0;
            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "ProductCode");
            DataTable distinctTable;
            distinctTable = viewround.ToTable(true, "MerchantCode");
            //new datable for excel
            DataTable dtdatasale = new DataTable();

            dtdatasale.Columns.Add("No.");
            dtdatasale.Columns.Add("รหัสสินค้า");
            dtdatasale.Columns.Add("MerchantName");
            dtdatasale.Columns.Add("SKU");
            dtdatasale.Columns.Add("ชื่อสินค้า");
            dtdatasale.Columns.Add("หน่วย");
            dtdatasale.Columns.Add("แบรนด์");
            dtdatasale.Columns.Add("หมวด");
            dtdatasale.Columns.Add("วันที่สร้าง");
            dtdatasale.Columns.Add("ราคา");

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
                       
                            CountNumber++;
                            drsale = dtdatasale.NewRow(); // have new row on each iteration
                            drsale["No."] = CountNumber.ToString();
                            drsale["รหัสสินค้า"] = Detailorder["ProductCode"].ToString();
                            drsale["MerchantName"] = Detailorder["MerchantName"].ToString();
                            drsale["SKU"] = Detailorder["Sku"].ToString();
                            drsale["ชื่อสินค้า"] = Detailorder["ProductName"].ToString();
                            drsale["หน่วย"] = Detailorder["UnitName"].ToString();
                            drsale["แบรนด์"] = Detailorder["ProductBrandName"].ToString();
                            drsale["หมวด"] = Detailorder["ProductCategoryName"].ToString();
                            drsale["ราคา"] = Detailorder["Price"].ToString();
                            drsale["วันที่สร้าง"] = Detailorder["CreateDate"].ToString();

                        dtdatasale.Rows.Add(drsale);
                    }
                    
                }
                

            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportProduct_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
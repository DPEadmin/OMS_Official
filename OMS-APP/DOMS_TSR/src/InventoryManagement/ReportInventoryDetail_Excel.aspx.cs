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

namespace DOMS_TSR.src.InventoryManagement
{
    public partial class ReportInventoryDetail_Excel : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                
                if (empInfo != null)
                {
                    
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
            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingExportExcelByCriteria";

            using (var wb = new WebClient())
            {
                List<InventoryDetailInfoNew> olist = new List<InventoryDetailInfoNew>();
                List<InventoryDetailInfoNew> resultExport = new List<InventoryDetailInfoNew>();
                resultExport = (List<InventoryDetailInfoNew>)Session["dataExportExcel"];

                var dataExcel = new NameValueCollection();

                dataExcel["InventoryCode"] = resultExport[0].InventoryCode;

                dataExcel["ProductCode"] = resultExport[0].ProductCode;

                dataExcel["ProductName"] = resultExport[0].ProductName;

                dataExcel["ProductCategoryName"] = resultExport[0].ProductCategoryName;

                dataExcel["ProductBrandName"] = resultExport[0].ProductBrandName;

                dataExcel["MerchantCode"] = resultExport[0].MerchantCode;

                var response = wb.UploadValues(APIpath, "POST", dataExcel);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryForReport> lOrder = JsonConvert.DeserializeObject<List<InventoryForReport>>(respstr); 
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ReportInventoryDetail.xlsx"));

            int CountNumber = 0;
            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);


            RoundTable = viewround.ToTable(true, "ProductCode");
            //new datable for excel
            DataTable dtdatasale = new DataTable();
            dtdatasale.Columns.Add("UpdateDate");
            dtdatasale.Columns.Add("ProductCode");
            dtdatasale.Columns.Add("ProductName");
            dtdatasale.Columns.Add("ProductCategoryName");
            dtdatasale.Columns.Add("ProductBrandName");
            dtdatasale.Columns.Add("QTY");

            dtdatasale.Columns.Add("Reserved");
            dtdatasale.Columns.Add("Current");
            dtdatasale.Columns.Add("Balance");
            dtdatasale.Columns.Add("SafetyStock");
            dtdatasale.Columns.Add("ReOrder");

            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                


                DataView ViewOrderinf_id = new DataView(dt);
                
                foreach (DataRow Detail in RoundTable.Rows)
                {

                    int NumRow = 1;
                    ViewOrderinf_id.RowFilter = string.Format("ProductCode ='{0}'", Detail["ProductCode"].ToString());


                    DataTable Orderinfo = ViewOrderinf_id.ToTable();
                    foreach (DataRow Detailorder in Orderinfo.Rows)
                    {

                        if (NumRow <= 1)
                        {
                            
                            CountNumber++;
                            drsale = dtdatasale.NewRow(); // have new row on each iteration
                            drsale["UpdateDate"] = Detailorder["UpdateDate"].ToString();
                            drsale["ProductCode"] = Detailorder["ProductCode"].ToString();
                            drsale["ProductName"] = Detailorder["ProductName"].ToString();
                            drsale["ProductCategoryName"] = Detailorder["ProductCategoryName"].ToString();
                            drsale["ProductBrandName"] = Detailorder["ProductBrandName"].ToString();

                            drsale["QTY"] = Detailorder["QTY"].ToString();
                            drsale["Reserved"] = Detailorder["Reserved"].ToString();
                            drsale["Current"] = Detailorder["Current"].ToString();
                            drsale["Balance"] = Detailorder["Balance"].ToString();
                            drsale["SafetyStock"] = Detailorder["SafetyStock"].ToString();
                            drsale["ReOrder"] = Detailorder["ReOrder"].ToString();

                            NumRow++;


                        }
                        else
                        {
                            drsale = dtdatasale.NewRow(); // have new row on each iteration

                            drsale["UpdateDate"] = Detailorder["UpdateDate"].ToString();
                            drsale["ProductCode"] = Detailorder["ProductCode"].ToString();
                            drsale["ProductName"] = Detailorder["ProductName"].ToString();
                            drsale["ProductCategoryName"] = Detailorder["ProductCategoryName"].ToString();
                            drsale["ProductBrandName"] = Detailorder["ProductBrandName"].ToString();

                            drsale["QTY"] = Detailorder["QTY"].ToString();
                            drsale["Reserved"] = Detailorder["Reserved"].ToString();
                            drsale["Current"] = Detailorder["Current"].ToString();
                            drsale["Balance"] = Detailorder["Balance"].ToString();
                            drsale["SafetyStock"] = Detailorder["SafetyStock"].ToString();
                            drsale["ReOrder"] = Detailorder["ReOrder"].ToString();
                            
                            NumRow++;

                        }

                        

                        dtdatasale.Rows.Add(drsale);
                    }

                }


            }


            if (dt.Rows.Count > 0)
            {
                exx.LoadData(dt, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportInventoryDetail_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
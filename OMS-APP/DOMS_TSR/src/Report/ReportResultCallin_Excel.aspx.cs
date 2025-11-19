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

namespace DOMS_TSR.src.Report
{
    public partial class ReportResultCallin_Excel : System.Web.UI.Page
    {
        protected static string APIUrl;
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    
                    APIUrl = empInfo.ConnectionAPI;
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
            APIpath = APIUrl + "/api/support/ReportResultCallin";

            using (var wb = new WebClient())
            {
                List<OrderOLInfo> olist = new List<OrderOLInfo>();
                List<OrderOLInfo> resultExport = new List<OrderOLInfo>();
                resultExport = (List<OrderOLInfo>)Session["dataExportExcel"];

                var dataExcel = new NameValueCollection();

                dataExcel["OrderCode"] = resultExport[0].OrderCode;

                dataExcel["CreateDate"] = resultExport[0].CreateDate;

                dataExcel["CreateDateTo"] = resultExport[0].CreateDateTo;

                dataExcel["SALE_CODE"] = resultExport[0].SALE_CODE; 

                dataExcel["SALE_FNAME"] = resultExport[0].SALE_FNAME; 

                dataExcel["SALE_LNAME"] = resultExport[0].SALE_LNAME; 

                dataExcel["DeliveryDateFrom"] = resultExport[0].DeliveryDate;

                dataExcel["DeliveryDateTo"] = resultExport[0].DeliveryDate;

                dataExcel["OrderStatusCode"] = resultExport[0].OrderStatusCode;

                dataExcel["OrderStateCode"] = resultExport[0].OrderStateCode;

                dataExcel["ChannelCode"] = resultExport[0].ChannelCode;

                

                dataExcel["ConfirmNo"] = "NULL";




                var response = wb.UploadValues(APIpath, "POST", dataExcel);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ReportResultCallinInfo> lOrder = JsonConvert.DeserializeObject<List<ReportResultCallinInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ReportResultCallin.xlsx"));

            int CountNumber = 0;
            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "SALE_CODE");
            //new datable for excel
            DataTable dtdatasale = new DataTable();
            

            dtdatasale.Columns.Add("วันที่สั่งซื้อ");
            dtdatasale.Columns.Add("รหัสพนักงาน");
            dtdatasale.Columns.Add("ชื่อพนักงาน");
            dtdatasale.Columns.Add("สถานะการโทร");
            dtdatasale.Columns.Add("รวมจำนวนการโทร");

            dtdatasale.Columns.Add("ช่องทางการขาย");
            dtdatasale.Columns.Add("ราคารวม");
            dtdatasale.Columns.Add("ราคา(กรุงเทพ)");
            dtdatasale.Columns.Add("ราคารวม(กรุงเทพ)");
            dtdatasale.Columns.Add("ราคา(ต่างจังหวัด)");
            dtdatasale.Columns.Add("ราคารวม(ต่างจังหวัด)");



            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                


                DataView ViewOrderinf_id = new DataView(dt);
                


                    DataTable Orderinfo = ViewOrderinf_id.ToTable();
                    foreach (DataRow Detailorder in Orderinfo.Rows)
                    {

                        
                            drsale = dtdatasale.NewRow(); // have new row on each iteration

                    
                            string calldate = Detailorder["ORDER_DATE"].ToString();
                            string convertdate = DateTime.Parse(calldate).ToString("dd/MM/yyyy");
                            drsale["วันที่สั่งซื้อ"] = convertdate;
                            drsale["รหัสพนักงาน"] = Detailorder["SALE_CODE"].ToString();
                            drsale["ชื่อพนักงาน"] = Detailorder["SALE_NAME"].ToString();
                            
                          
                            drsale["สถานะการโทร"] = Detailorder["CALL_STATUS"].ToString();
                            drsale["รวมจำนวนการโทร"] = Detailorder["COUNT_CALL"].ToString();

                            drsale["ช่องทางการขาย"] = Detailorder["CHANNEL"].ToString();
                            drsale["ราคารวม"] = Detailorder["TOTAL_ORDER"].ToString();
                            drsale["ราคา(กรุงเทพ)"] = Detailorder["BKK_ORDER"].ToString();
                            drsale["ราคารวม(กรุงเทพ)"] = Detailorder["BKK_REVENUE"].ToString();
                            drsale["ราคา(ต่างจังหวัด)"] = Detailorder["UPC_ORDER"].ToString();
                            drsale["ราคารวม(ต่างจังหวัด)"] = Detailorder["UPC_REVENUE"].ToString();
                            

                        

                        dtdatasale.Rows.Add(drsale);
                    }

                


            }


            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportResultCallin_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
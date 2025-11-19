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
    public partial class ReportOrderHistory_Excel : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    //Open this code for use API from server
                    //APIUrl = empInfo.ConnectionAPI;
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
            APIpath = APIUrl + "/api/support/ReportOrderHistory";

            using (var wb = new WebClient())
            {
                List<OrderHistory> olist = new List<OrderHistory>();
                

                var data = new NameValueCollection();

                data["OrderCode"] = "";

                data["CreateDate"] = "";

                data["CreateDateTo"] = "";

                data["CustomerFName"] = "";

                data["CustomerLName"] = "";

                data["ContactStatus"] = "";

                data["OrderSituation"] = "";

                data["OrderType"] = "";




                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderHistory> lOrder = JsonConvert.DeserializeObject<List<OrderHistory>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ReportOrderHistory_Excel.xlsx"));

            int CountNumber = 0;
            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "ordercode");
            //new datable for excel
            DataTable dtdatasale = new DataTable();
            

            dtdatasale.Columns.Add("วันที่ติดต่อ");
            dtdatasale.Columns.Add("เวลา");
            dtdatasale.Columns.Add("รหัสใบสั่งขาย");
            dtdatasale.Columns.Add("ชื่อผู้โทร");
            dtdatasale.Columns.Add("สถานะการติดต่อ");
            dtdatasale.Columns.Add("ผลการติดต่อ");
            dtdatasale.Columns.Add("ประเภทการขาย");

            dtdatasale.Columns.Add("รหัสสินค้า");
            dtdatasale.Columns.Add("สินค้า");
            dtdatasale.Columns.Add("ราคา");
            dtdatasale.Columns.Add("จำนวน");
            dtdatasale.Columns.Add("ค่าจัดส่ง");
            dtdatasale.Columns.Add("ราคาสินค้ารวม");
            dtdatasale.Columns.Add("ภาษี");
            dtdatasale.Columns.Add("ราคาที่ต้องชำระ");

            dtdatasale.Columns.Add("หมายเหตุ");



            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                

                decimal OrderTotalpriceRE = 0;
                string SumPrice = "0";
                decimal CountTotal = 0;
                decimal SumTransPort = 0;
                string Tax = "0";
                DataView ViewOrderinf_id = new DataView(dt);
                
                foreach (DataRow Detail in RoundTable.Rows)
                {

                    int NumRow = 1;
                    ViewOrderinf_id.RowFilter = string.Format("ordercode ='{0}'", Detail["orderCode"].ToString());


                    DataTable Orderinfo = ViewOrderinf_id.ToTable();
                    foreach (DataRow Detailorder in Orderinfo.Rows)
                    {

                        if (NumRow <= 1)
                        {
                            drsale = dtdatasale.NewRow(); // have new row on each iteration
                            
                            drsale["วันที่ติดต่อ"] = Detailorder["Date"].ToString();
                            drsale["เวลา"] = Detailorder["Time"].ToString();
                            drsale["รหัสใบสั่งขาย"] = Detailorder["OrderCode"].ToString();
                            drsale["ชื่อผู้โทร"] = Detailorder["CustomerName"].ToString();
                            drsale["สถานะการติดต่อ"] = Detailorder["ContactStatus"].ToString();
                            drsale["ผลการติดต่อ"] = Detailorder["OrderSituation"].ToString();
                            drsale["ประเภทการขาย"] = Detailorder["OrderType"].ToString();
                            drsale["รหัสสินค้า"] = Detailorder["ProductCode"].ToString();
                            drsale["สินค้า"] = Detailorder["ProductName"].ToString();
                            drsale["ราคา"] = Detailorder["NetPrice"].ToString();
                            drsale["จำนวน"] = Detailorder["Amount"].ToString();
                            drsale["ค่าจัดส่ง"] = "";
                            drsale["ราคาสินค้ารวม"] = Detailorder["Sumprice"].ToString();
                            drsale["ภาษี"] = "";
                            drsale["ราคาที่ต้องชำระ"] = "";
                            drsale["หมายเหตุ"] = Detailorder["OrderNote"].ToString();
                            NumRow++;
                            Tax = (Detailorder["Vat"].ToString()) != null ? (Detailorder["Vat"].ToString()) : "0";
                            SumTransPort += decimal.Parse(Detailorder["TransportPrice"].ToString()) != null ? decimal.Parse(Detailorder["TransportPrice"].ToString()) : 0;
                            CountTotal += decimal.Parse(Detailorder["Amount"].ToString()) != null ? decimal.Parse(Detailorder["Amount"].ToString()) : 0;
                            OrderTotalpriceRE = decimal.Parse(Detailorder["orderTotalPrice"].ToString()) != null ? decimal.Parse(Detailorder["orderTotalPrice"].ToString()) : 0;
                            SumPrice = (Detailorder["SumbyProductprice"].ToString()) != null ? (Detailorder["SumbyProductprice"].ToString()) : "0"; 

                        }
                        else
                        {
                            drsale = dtdatasale.NewRow(); // have new row on each iteration
                    
                            drsale["วันที่ติดต่อ"] = "";
                            drsale["เวลา"] = "";
                            drsale["รหัสใบสั่งขาย"] = "";
                            
                          
                            drsale["ชื่อผู้โทร"] = "";
                            drsale["สถานะการติดต่อ"] = "";

                            drsale["ผลการติดต่อ"] = "";
                            drsale["ประเภทการขาย"] = "";

                            drsale["รหัสสินค้า"] = Detailorder["ProductCode"].ToString();
                            drsale["สินค้า"] = Detailorder["ProductName"].ToString();
                            drsale["ราคา"] = Detailorder["NetPrice"].ToString();
                            drsale["จำนวน"] = Detailorder["Amount"].ToString();
                            drsale["ค่าจัดส่ง"] = "";
                            drsale["ราคาสินค้ารวม"] = Detailorder["Sumprice"].ToString();
                            drsale["ภาษี"] = "";
                            drsale["ราคาที่ต้องชำระ"] = "";
                            drsale["หมายเหตุ"] = "";

                            NumRow++;
                            SumTransPort += decimal.Parse(Detailorder["TransportPrice"].ToString()) != null ? decimal.Parse(Detailorder["TransportPrice"].ToString()) : 0;
                            CountTotal += decimal.Parse(Detailorder["Amount"].ToString()) != null ? decimal.Parse(Detailorder["Amount"].ToString()) : 0;
                            OrderTotalpriceRE = decimal.Parse(Detailorder["orderTotalPrice"].ToString()) != null ? decimal.Parse(Detailorder["orderTotalPrice"].ToString()) : 0;
                            SumPrice = (Detailorder["SumbyProductprice"].ToString()) != null ? (Detailorder["SumbyProductprice"].ToString()) : "0";
                        }

                        

                        dtdatasale.Rows.Add(drsale);
                    }
                    drsale = dtdatasale.NewRow(); // have new row on each iteration
                                                  
                    drsale["วันที่ติดต่อ"] = "";
                    drsale["เวลา"] = "";
                    drsale["รหัสใบสั่งขาย"] = "";
                    

                    drsale["ชื่อผู้โทร"] = "";
                    drsale["สถานะการติดต่อ"] = "";

                    drsale["ผลการติดต่อ"] = "";
                    drsale["ประเภทการขาย"] = "";

                    drsale["รหัสสินค้า"] = "";
                    drsale["สินค้า"] = "";
                    drsale["ราคา"] = "";
                    drsale["จำนวน"] = "";
                    drsale["ค่าจัดส่ง"] = String.Format("{0:##}", SumTransPort.ToString());
                    drsale["ราคาสินค้ารวม"] = String.Format("{0:##}", SumPrice.ToString());
                    drsale["ภาษี"] = String.Format("{0:##}", Tax.ToString());
                    drsale["ราคาที่ต้องชำระ"] = String.Format("{0:##}", OrderTotalpriceRE.ToString()); 
                    drsale["หมายเหตุ"] = "";
                    dtdatasale.Rows.Add(drsale);
                }


            }


            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportOrderHistory_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
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
    public partial class ReaportMediaDailySale_Excel : System.Web.UI.Page
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
            APIpath = APIUrl + "/api/support/ReaportMediaDailySale_Excel";

            using (var wb = new WebClient())
            {
                List<OrderOLInfo> olist = new List<OrderOLInfo>();
                List<OrderOLInfo> resultExport = new List<OrderOLInfo>();
                resultExport = (List<OrderOLInfo>)Session["dataExportExcel"];

                var dataExcel = new NameValueCollection();

                dataExcel["OrderCode"] = resultExport[0].OrderCode;

                dataExcel["CreateDate"] = resultExport[0].CreateDate;

                dataExcel["CreateDateTo"] = resultExport[0].CreateDateTo;

                dataExcel["CustomerCode"] = resultExport[0].CustomerCode;

                dataExcel["CustomerFName"] = resultExport[0].CustomerLName;

                dataExcel["CustomerLName"] = resultExport[0].CustomerLName;

                dataExcel["DeliveryDateFrom"] = resultExport[0].DeliveryDate;

                dataExcel["DeliveryDateTo"] = resultExport[0].DeliveryDate;
                dataExcel["OrderStatusCode"] = resultExport[0].OrderStatusCode;
                dataExcel["OrderStateCode"] = resultExport[0].OrderStateCode;
                dataExcel["ChannelCode"] = resultExport[0].ChannelCode;

                

                dataExcel["ConfirmNo"] = "NULL";




                var response = wb.UploadValues(APIpath, "POST", dataExcel);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ReaportMediaDailySaleInfo> lOrder = JsonConvert.DeserializeObject<List<ReaportMediaDailySaleInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ReaportMediaDailySale.xlsx"));

            int CountNumber = 0;
            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "ORDER_NO");
            DataTable distinctTable;
            distinctTable = viewround.ToTable(true, "ORDER_DATE");
            //new datable for excel
            DataTable dtdatasale = new DataTable();
            dtdatasale.Columns.Add("No.");
            dtdatasale.Columns.Add("วันที่ออกอากาศ");
            dtdatasale.Columns.Add("เวลาที่เริ่ม");
            dtdatasale.Columns.Add("เวลาที่สิ้นสุด");
            dtdatasale.Columns.Add("ระยะเวลา");
            dtdatasale.Columns.Add("เบอร์ติดต่อ");
            dtdatasale.Columns.Add("ช่องทางการชาย");

            dtdatasale.Columns.Add("ชื่อโปรแกรม");
            dtdatasale.Columns.Add("วันที่สั่งซื้อ");
            dtdatasale.Columns.Add("รหัสใบสั่งขาย");
            dtdatasale.Columns.Add("รหัสแคมเปญ");
            dtdatasale.Columns.Add("ชื่อแคมเปญ");
            dtdatasale.Columns.Add("รหัสโปรโมชัน");
            dtdatasale.Columns.Add("ชื่อโปรโมชัน");

            dtdatasale.Columns.Add("รหัสสินค้า");
            dtdatasale.Columns.Add("ชื่อสินค้า");
            dtdatasale.Columns.Add("จำนวน");
            dtdatasale.Columns.Add("ราคา");

            dtdatasale.Columns.Add("ค่าจัดส่ง");
            dtdatasale.Columns.Add("ราคารวม");
            dtdatasale.Columns.Add("ราคาทั้งหมด");

          
            
            dtdatasale.Columns.Add("วิธีการชำระ");
            dtdatasale.Columns.Add("รหัสลูกค้า");
            dtdatasale.Columns.Add("ชื่อลูกค้า");
            dtdatasale.Columns.Add("รหัสพนักงาน");
            dtdatasale.Columns.Add("ชื่อพนักงาน");



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
                    ViewOrderinf_id.RowFilter = string.Format("ORDER_NO ='{0}'", Detail["ORDER_NO"].ToString());


                    DataTable Orderinfo = ViewOrderinf_id.ToTable();
                    foreach (DataRow Detailorder in Orderinfo.Rows)
                    {

                        if (NumRow <= 1)
                        {
                            
                            CountNumber++;
                            drsale = dtdatasale.NewRow(); // have new row on each iteration
                            drsale["No."] = CountNumber.ToString();
                            drsale["วันที่ออกอากาศ"] = Detailorder["MEDIA_DATE"].ToString();
                            drsale["เวลาที่เริ่ม"] = Detailorder["TIME_START"].ToString();
                            drsale["เวลาที่สิ้นสุด"] = Detailorder["TIME_END"].ToString();
                            drsale["ระยะเวลา"] = Detailorder["DURATION"].ToString();
                            drsale["เบอร์ติดต่อ"] = Detailorder["MEDIA_PHONE"].ToString();
                            drsale["ช่องทางการชาย"] = Detailorder["CHANNEL"].ToString();


                            drsale["ชื่อโปรแกรม"] = Detailorder["PROGRAM_NAME"].ToString();
                            drsale["วันที่สั่งซื้อ"] = Detailorder["ORDER_DATE"].ToString();
                            drsale["รหัสใบสั่งขาย"] = Detailorder["ORDER_NO"].ToString();
                            drsale["รหัสแคมเปญ"] = Detailorder["CAMPAIGN_CODE"].ToString();
                            drsale["ชื่อแคมเปญ"] = Detailorder["CAMPAIGN_NAME"].ToString();
                            drsale["รหัสโปรโมชัน"] = Detailorder["PROMOTION_CODE"].ToString();
                            drsale["ชื่อโปรโมชัน"] = Detailorder["PROMOTION_NAME"].ToString();

                            drsale["รหัสสินค้า"] = Detailorder["PRODUCT_CODE"].ToString();
                            drsale["ชื่อสินค้า"] = Detailorder["PRODUCT_NAME"].ToString();
                            drsale["จำนวน"] = Detailorder["AMOUNT"].ToString();

                            amount = decimal.Parse(Detailorder["AMOUNT"].ToString()) != null ? decimal.Parse(Detailorder["AMOUNT"].ToString()) : 0;
                            price = decimal.Parse(Detailorder["PRICE"].ToString()) != null ? decimal.Parse(Detailorder["PRICE"].ToString()) : 0;
                            result = price / amount;
                            drsale["ราคา"] = String.Format("{0:0.00}", result.ToString());

                            drsale["ค่าจัดส่ง"] = String.Format("{0:0.00}", Detailorder["TransportPrice"].ToString());
                            drsale["ราคารวม"] = Detailorder["PRICE"].ToString();
                            drsale["ราคาทั้งหมด"] = "";

                            drsale["วิธีการชำระ"] = Detailorder["PAYMENT_TERM"].ToString();
                            drsale["รหัสลูกค้า"] = Detailorder["CUSTOMER_CODE"].ToString();
                            drsale["ชื่อลูกค้า"] = Detailorder["CUSTOMER_NAME"].ToString();
                            drsale["รหัสพนักงาน"] = Detailorder["SALE_CODE"].ToString();
                            drsale["ชื่อพนักงาน"] = Detailorder["SALE_NAME"].ToString();
                            NumRow++;
                            OrderTotalpriceRE = decimal.Parse(Detailorder["orderTotalPrice"].ToString()) != null ? decimal.Parse(Detailorder["orderTotalPrice"].ToString()) : 0;
                            SumTransPort += decimal.Parse(Detailorder["TransportPrice"].ToString()) != null ? decimal.Parse(Detailorder["TransportPrice"].ToString()) : 0;
                            SumPrice = decimal.Parse(Detailorder["SumbyProductprice"].ToString()) != null ? decimal.Parse(Detailorder["SumbyProductprice"].ToString()) : 0;

                        }
                        else
                        {
                            drsale = dtdatasale.NewRow(); // have new row on each iteration

                            drsale["No."] = "";
                            drsale["วันที่ออกอากาศ"] = Detailorder["MEDIA_DATE"].ToString();
                            drsale["เวลาที่เริ่ม"] = Detailorder["TIME_START"].ToString();
                            drsale["เวลาที่สิ้นสุด"] = Detailorder["TIME_END"].ToString();
                            drsale["ระยะเวลา"] = Detailorder["DURATION"].ToString();
                            drsale["เบอร์ติดต่อ"] = Detailorder["MEDIA_PHONE"].ToString();
                            drsale["ช่องทางการชาย"] = Detailorder["CHANNEL"].ToString();


                            drsale["ชื่อโปรแกรม"] = Detailorder["PROGRAM_NAME"].ToString();
                            drsale["วันที่สั่งซื้อ"] = "";
                            drsale["รหัสใบสั่งขาย"] = "";
                            drsale["รหัสแคมเปญ"] = Detailorder["CAMPAIGN_CODE"].ToString();
                            drsale["ชื่อแคมเปญ"] = Detailorder["CAMPAIGN_NAME"].ToString();
                            drsale["รหัสโปรโมชัน"] = Detailorder["PROMOTION_CODE"].ToString();
                            drsale["ชื่อโปรโมชัน"] = Detailorder["PROMOTION_NAME"].ToString();

                            drsale["รหัสสินค้า"] = Detailorder["PRODUCT_CODE"].ToString();
                            drsale["ชื่อสินค้า"] = Detailorder["PRODUCT_NAME"].ToString();
                            drsale["จำนวน"] = Detailorder["AMOUNT"].ToString();
                            amount = decimal.Parse(Detailorder["AMOUNT"].ToString()) != null ? decimal.Parse(Detailorder["AMOUNT"].ToString()) : 0;
                            price = decimal.Parse(Detailorder["PRICE"].ToString()) != null ? decimal.Parse(Detailorder["PRICE"].ToString()) : 0;
                            result = price / amount;
                            drsale["ราคา"] = String.Format("{0:0.00}", result.ToString());
                            drsale["ค่าจัดส่ง"] = String.Format("{0:0.00}", Detailorder["TransportPrice"].ToString());
                            drsale["ราคารวม"] = Detailorder["PRICE"].ToString();
                            drsale["ราคาทั้งหมด"] = "";
                            drsale["วิธีการชำระ"] = "";
                            drsale["รหัสลูกค้า"] = "";
                            drsale["ชื่อลูกค้า"] = "";
                            drsale["รหัสพนักงาน"] = "";
                            drsale["ชื่อพนักงาน"] = "";
                            NumRow++;
                            SumTransPort += decimal.Parse(Detailorder["TransportPrice"].ToString()) != null ? decimal.Parse(Detailorder["TransportPrice"].ToString()) : 0;
                           
                            OrderTotalpriceRE = decimal.Parse(Detailorder["orderTotalPrice"].ToString()) != null ? decimal.Parse(Detailorder["orderTotalPrice"].ToString()) : 0;
                            SumPrice = decimal.Parse(Detailorder["SumbyProductprice"].ToString()) != null ? decimal.Parse(Detailorder["SumbyProductprice"].ToString()) : 0;
                        }

                        

                        dtdatasale.Rows.Add(drsale);
                    }
                    drsale = dtdatasale.NewRow(); // have new row on each iteration

                    drsale["No."] = "";
                    drsale["วันที่ออกอากาศ"] = "";
                    drsale["เวลาที่เริ่ม"] = "";
                    drsale["เวลาที่สิ้นสุด"] = "";
                    drsale["ระยะเวลา"] = "";
                    drsale["เบอร์ติดต่อ"] = "";
                    drsale["ช่องทางการชาย"] = "";


                    drsale["ชื่อโปรแกรม"] = "";
                    drsale["วันที่สั่งซื้อ"] = "";
                    drsale["รหัสใบสั่งขาย"] = "";
                    drsale["รหัสแคมเปญ"] = "";
                    drsale["ชื่อแคมเปญ"] = "";
                    drsale["รหัสโปรโมชัน"] = "";
                    drsale["ชื่อโปรโมชัน"] = "";

                    drsale["รหัสสินค้า"] = "";
                    drsale["ชื่อสินค้า"] = "TOTAL";
                    drsale["จำนวน"] = ""; 
                    drsale["ราคา"] = "";
                    drsale["ค่าจัดส่ง"] = String.Format("{0:##}", SumTransPort.ToString());
                    drsale["ราคารวม"] = String.Format("{0:##}", SumPrice.ToString());
                    drsale["ราคาทั้งหมด"] = String.Format("{0:##}", OrderTotalpriceRE.ToString());
                    drsale["วิธีการชำระ"] = "";
                    drsale["รหัสลูกค้า"] = "";
                    drsale["ชื่อลูกค้า"] = "";
                    drsale["รหัสพนักงาน"] = "";
                    drsale["ชื่อพนักงาน"] = "";

                    dtdatasale.Rows.Add(drsale);
                }



                

            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReaportMediaDailySale_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
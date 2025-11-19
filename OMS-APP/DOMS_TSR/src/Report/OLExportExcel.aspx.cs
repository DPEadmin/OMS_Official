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
    public partial class OLExportExcel : System.Web.UI.Page
    {
        protected static string APIUrl;
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadDar("");
            }
        }
        protected void LoadDar(string StrDate)
        {
            EmpInfo empInfo = new EmpInfo();
            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];
            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo != null)
            {
              
              APIUrl = empInfo.ConnectionAPI;
              hidMerchantCode.Value = merchantinfo.MerchantCode;
            }
            else
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            DataTable dt = new DataTable();
            DataTable lorderdetailInfo = new DataTable();

            Util aaa = new Util();
            string respstr = "";
            APIpath = APIUrl + "/api/support/DtOlExcelReport";

            using (var wb = new WebClient())
            {
                List<OrderOLInfo> olist = new List<OrderOLInfo>();
                List<OrderOLInfo> resultExport = new List<OrderOLInfo>();
                resultExport = (List<OrderOLInfo>)Session["dataExportExcel"];

                var dataExcel = new NameValueCollection();

                dataExcel["OrderCode"] = resultExport[0].OrderCode;
                
                dataExcel["MerchantMapCode"] = hidMerchantCode.Value;

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

            List<OrderOLInfo> lOrder = JsonConvert.DeserializeObject<List<OrderOLInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ExportOL.xlsx"));

            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "ordercode");
            DataTable distinctTable;
            distinctTable = viewround.ToTable(true, "Createdate");
            //new datable for excel
            DataTable dtdatasale = new DataTable();
            dtdatasale.Columns.Add("Sale Date");
            dtdatasale.Columns.Add("พนักงานขาย");
            dtdatasale.Columns.Add("เพศ");
            dtdatasale.Columns.Add("Order No");
            dtdatasale.Columns.Add("Merchant");
            dtdatasale.Columns.Add("แบรนด์");
            dtdatasale.Columns.Add("ลูกค้า");

            dtdatasale.Columns.Add("ช่องทางการสั่งซื้อ");
            dtdatasale.Columns.Add("เบอร์");
            dtdatasale.Columns.Add("ที่อยู่");
            dtdatasale.Columns.Add("รหัสไปรษณีย์");
            dtdatasale.Columns.Add("หมายเหตุ");
            dtdatasale.Columns.Add("วันที่ลูกค้านัดรับสินค้า");

            dtdatasale.Columns.Add("รหัสสินค้า");
            dtdatasale.Columns.Add("รหัสสินค้าอ้างอิง");
            dtdatasale.Columns.Add("สินค้า");
            dtdatasale.Columns.Add("ราคา");
            dtdatasale.Columns.Add("จำนวน");
            dtdatasale.Columns.Add("ค่าจัดส่ง");

            dtdatasale.Columns.Add("ราคาสินค้ารวม");
            dtdatasale.Columns.Add("ราคาที่ต้องชำระ");
            dtdatasale.Columns.Add("สถานะจัดส่ง");
            dtdatasale.Columns.Add("เลขพัสดุ");
            dtdatasale.Columns.Add("สถานะออเดอร์");
            dtdatasale.Columns.Add("ช่องทางชำระเงิน");
            dtdatasale.Columns.Add("เลขที่ผู้เสียภาษี");

            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow orderinfo in distinctTable.Rows)
                {

                    decimal OrderTotalpriceRE = 0;
                    DataView ViewOrderinf_id = new DataView(dt);
                    DataView ViewOrderinf_Date = new DataView(dt);

                    ViewOrderinf_Date.RowFilter = string.Format("Createdate ='{0}'", orderinfo["Createdate"].ToString());
                    DataTable OrderDate = ViewOrderinf_Date.ToTable();
                    string OrderRepeat = "";
                    foreach (DataRow Detail in OrderDate.Rows)
                    {
                       
                        int NumRow = 1;
                        ViewOrderinf_id.RowFilter = string.Format("ordercode ='{0}'", Detail["orderCode"].ToString());
                       

                        DataTable Orderinfo = ViewOrderinf_id.ToTable();
                        foreach (DataRow Detailorder in Orderinfo.Rows)
                        {
                           
                            if (NumRow <= 1)
                            {
                                if (OrderRepeat == Detail["orderCode"].ToString().Trim())
                                {
                                    break;
                                }

                                drsale = dtdatasale.NewRow(); // have new row on each iteration
                                drsale["sale Date"] = Detailorder["CreateDate"].ToString();
                                drsale["พนักงานขาย"] = Detailorder["sellName"].ToString();
                                drsale["เพศ"] = Detailorder["Gender"].ToString();
                                drsale["Order No"] = Detailorder["OrderCode"].ToString();
                                drsale["Merchant"] = Detailorder["MerchantName"].ToString();
                                drsale["แบรนด์"] = Detailorder["CampaignCategoryName"].ToString();
                                drsale["ลูกค้า"] = Detailorder["CustomerFName"].ToString();
                                drsale["ช่องทางการสั่งซื้อ"] = Detailorder["ChannelName"].ToString();
                                drsale["เบอร์"] = Detailorder["ContactTel"].ToString();
                                drsale["ที่อยู่"] = Detailorder["addresscustomerdetail"].ToString();
                                drsale["รหัสไปรษณีย์"] = Detailorder["addresscustomerdetailzipcode"].ToString();
                                drsale["หมายเหตุ"] = Detailorder["OrderNote"].ToString();
                                drsale["วันที่ลูกค้านัดรับสินค้า"] = Detailorder["DeliveryDate"].ToString();

                                drsale["รหัสสินค้า"] = Detailorder["Productcode"].ToString();
                                drsale["รหัสสินค้าอ้างอิง"] = Detailorder["SKU"].ToString();
                                drsale["สินค้า"] = Detailorder["ProductName"].ToString();
                                drsale["ราคา"] = String.Format("{0:0.00}", Detailorder["NetPrice"].ToString());
                                drsale["จำนวน"] = Detailorder["Amount"].ToString();
                                drsale["ค่าจัดส่ง"] = Detailorder["TransportPrice"].ToString();

                                
                                drsale["ราคาสินค้ารวม"] = Detailorder["SumbyProductprice"].ToString();
                               
                                drsale["ราคาที่ต้องชำระ"] = Detailorder["orderTotalPrice"].ToString();

                                
                                drsale["สถานะจัดส่ง"] = Detailorder["OrderStatusName"].ToString();
                                
                                drsale["เลขพัสดุ"] = Detailorder["OrderTracking"].ToString();


                                
                                drsale["สถานะออเดอร์"] = Detailorder["OrderStateName"].ToString();
                                
                                drsale["ช่องทางชำระเงิน"] = Detailorder["PaymentTypeName"].ToString();
                                drsale["เลขที่ผู้เสียภาษี"] = Detailorder["TaxId"].ToString();
                                
                                NumRow++;
                                OrderTotalpriceRE += decimal.Parse(Detailorder["orderTotalPrice"].ToString()) != null ? decimal.Parse(Detailorder["orderTotalPrice"].ToString()) : 0;
                            }
                            else
                            {
                                drsale = dtdatasale.NewRow(); // have new row on each iteration
                                drsale["sale Date"] = "";
                                drsale["พนักงานขาย"] = "";
                                drsale["Order No"] = "";
                                drsale["แบรนด์"] = "";
                                drsale["ลูกค้า"] = "";
                                drsale["ช่องทางการสั่งซื้อ"] = Detailorder["ChannelName"].ToString();

                                drsale["เบอร์"] = "";
                                drsale["ที่อยู่"] = "";
                                drsale["รหัสไปรษณีย์"] = "";
                                drsale["หมายเหตุ"] = "";
                                drsale["วันที่ลูกค้านัดรับสินค้า"] = "";

                                drsale["รหัสสินค้า"] = Detailorder["Productcode"].ToString();
                                drsale["รหัสสินค้าอ้างอิง"] = Detailorder["SKU"].ToString();
                                drsale["สินค้า"] = Detailorder["ProductName"].ToString();
                               
                                drsale["ราคา"] = Detailorder["NetPrice"].ToString();
                                drsale["จำนวน"] = Detailorder["Amount"].ToString();
                                drsale["ค่าจัดส่ง"] = "";

                                drsale["ราคาสินค้ารวม"] = "";
                                drsale["ราคาที่ต้องชำระ"] = "";
                                drsale["สถานะจัดส่ง"] = "";
                                drsale["เลขพัสดุ"] = "";
                                drsale["สถานะออเดอร์"] = "";
                                drsale["ช่องทางชำระเงิน"] = "";
                                drsale["เลขที่ผู้เสียภาษี"] = "";
                                NumRow++;
                            }

                            OrderRepeat = Detail["orderCode"].ToString().Trim();

                            dtdatasale.Rows.Add(drsale);
                        }


                    }
                  
                    drsale = dtdatasale.NewRow(); // have new row on each iteration
                    drsale["sale Date"] = "Total";
                    drsale["พนักงานขาย"] = "";
                    drsale["Order No"] = "";
                    drsale["แบรนด์"] = "";
                    drsale["ลูกค้า"] = "";
                    drsale["ช่องทางการสั่งซื้อ"] = "";

                    drsale["เบอร์"] = "";
                    drsale["ที่อยู่"] = "";
                    drsale["รหัสไปรษณีย์"] = "";
                    drsale["หมายเหตุ"] = "";
                    drsale["วันที่ลูกค้านัดรับสินค้า"] = "";

                    drsale["รหัสสินค้า"] = "";
                    drsale["รหัสสินค้าอ้างอิง"] = "";
                    drsale["สินค้า"] = "";
                    drsale["ราคา"] = "";
                    drsale["จำนวน"] = "";
                    drsale["ค่าจัดส่ง"] = "";

                    drsale["ราคาสินค้ารวม"] = "";
                    drsale["ราคาที่ต้องชำระ"] = String.Format("{0:##}", OrderTotalpriceRE.ToString()); 
                    drsale["สถานะจัดส่ง"] = "";
                    drsale["เลขพัสดุ"] = "";
                    drsale["สถานะออเดอร์"] = "";
                    drsale["ช่องทางชำระเงิน"] = "";
                    drsale["เลขที่ผู้เสียภาษี"] = "";
                    dtdatasale.Rows.Add(drsale);

                }

            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= SaleOrder_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
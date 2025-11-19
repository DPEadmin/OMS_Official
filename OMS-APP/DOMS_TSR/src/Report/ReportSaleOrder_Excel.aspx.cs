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
    public partial class ReportSaleOrder_Excel : System.Web.UI.Page
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
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null)
                {
                    hidMerCode.Value = merchantInfo.MerchantCode;


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
            APIpath = APIUrl + "/api/support/Saleorder";

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
                dataExcel["SALE_CODE"] = resultExport[0].SALE_CODE;

                dataExcel["SALE_FNAME"] = resultExport[0].SALE_FNAME;
                dataExcel["SALE_LNAME"] = resultExport[0].SALE_LNAME;

                dataExcel["MerchantMapCode"] = hidMerCode.Value;

                dataExcel["ConfirmNo"] = "NULL";




                var response = wb.UploadValues(APIpath, "POST", dataExcel);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ReportSaleOrderInfo> lOrder = JsonConvert.DeserializeObject<List<ReportSaleOrderInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ReportResultSOSA.xlsx"));

            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "ORDER_NO");
            DataTable distinctTable;
            distinctTable = viewround.ToTable(true, "ORDER_DATE");
            //new datable for excel
            DataTable dtdatasale = new DataTable();

            dtdatasale.Columns.Add("วันที่สั่งซื้อ");
            dtdatasale.Columns.Add("พนักงานขาย");
            dtdatasale.Columns.Add("เลขที่ใบสั่งขาย");
            dtdatasale.Columns.Add("แบรนด์");
            dtdatasale.Columns.Add("ช่องทางการสั่งซื้อ");

            dtdatasale.Columns.Add("ลูกค้า");
            dtdatasale.Columns.Add("เบอร์");
            dtdatasale.Columns.Add("ที่อยู่จัดส่ง");
            dtdatasale.Columns.Add("รหัสไปรษณีย์");
            dtdatasale.Columns.Add("ที่อยู่ใบกำกับภาษี");
            dtdatasale.Columns.Add("รหัสไปรษณีย์(ใบกำกับภาษี)");
            dtdatasale.Columns.Add("เลขที่ผู้เสียภาษี");
            dtdatasale.Columns.Add("วันที่ลูกค้านัดรับสินค้า");
            dtdatasale.Columns.Add("หมายเหตุ");
            
            dtdatasale.Columns.Add("รหัสสินค้า");
            dtdatasale.Columns.Add("ชื่อสินค้า");
            dtdatasale.Columns.Add("จำนวน");
            dtdatasale.Columns.Add("ราคา");
            dtdatasale.Columns.Add("ส่วนลด(%)");
            dtdatasale.Columns.Add("ส่วนลด(บาท)");
            dtdatasale.Columns.Add("ราคาสินค้ารวม");
            dtdatasale.Columns.Add("อัตรา VAT");
            dtdatasale.Columns.Add("จำนวน VAT");
            dtdatasale.Columns.Add("ค่าจัดส่ง");
            dtdatasale.Columns.Add("ราคาที่ต้องชำระ");

            dtdatasale.Columns.Add("วิธีการจัดส่ง");
            dtdatasale.Columns.Add("ขั้นตอน");
            dtdatasale.Columns.Add("สถานะใบสั่งขาย");
            dtdatasale.Columns.Add("เลขพัสดุ");
            dtdatasale.Columns.Add("ช่องทางการชำระเงิน");



            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow orderinfo in distinctTable.Rows)
                {

                    decimal OrderTotalprice = 0;
                    DataView ViewOrderinf_id = new DataView(dt);
                    DataView ViewOrderinf_Date = new DataView(dt);

                    ViewOrderinf_Date.RowFilter = string.Format("ORDER_DATE ='{0}'", orderinfo["ORDER_DATE"].ToString());
                    DataTable OrderDate = ViewOrderinf_Date.ToTable();
                    string OrderRepeat = "";
                    foreach (DataRow Detail in OrderDate.Rows)
                    {

                        int NumRow = 1;
                        ViewOrderinf_id.RowFilter = string.Format("ORDER_NO ='{0}'", Detail["ORDER_NO"].ToString());


                        DataTable Orderinfo = ViewOrderinf_id.ToTable();
                        foreach (DataRow Detailorder in Orderinfo.Rows)
                        {

                            if (NumRow <= 1)
                            {
                                if (OrderRepeat == Detail["ORDER_NO"].ToString().Trim())
                                {
                                    break;
                                }

                                drsale = dtdatasale.NewRow(); // have new row on each iteration
                                drsale["วันที่สั่งซื้อ"] = Detailorder["ORDER_DATE"].ToString();
                                drsale["พนักงานขาย"] = Detailorder["SALE_NAME"].ToString();
                                drsale["เลขที่ใบสั่งขาย"] = Detailorder["ORDER_NO"].ToString();
                                drsale["แบรนด์"] = Detailorder["BRAND"].ToString();
                                drsale["ช่องทางการสั่งซื้อ"] = Detailorder["CHANNEL"].ToString();

                                drsale["ลูกค้า"] = Detailorder["CUS_NAME"].ToString();
                                drsale["เบอร์"] = Detailorder["CUS_MOBILE"].ToString();
                                drsale["ที่อยู่จัดส่ง"] = Detailorder["CUS_ADD"].ToString();
                                drsale["รหัสไปรษณีย์"] = Detailorder["CUS_POSTCODE"].ToString();
                                drsale["ที่อยู่ใบกำกับภาษี"] = Detailorder["CUS_ADD2"].ToString();
                                drsale["รหัสไปรษณีย์(ใบกำกับภาษี)"] = Detailorder["CUS_POSTCODE2"].ToString();
                                drsale["เลขที่ผู้เสียภาษี"] = Detailorder["CUS_TAXID"].ToString();
                                drsale["วันที่ลูกค้านัดรับสินค้า"] = Detailorder["DELIVER_DATE"].ToString();
                                drsale["หมายเหตุ"] = Detailorder["ORDER_NOTE"].ToString();

                                drsale["รหัสสินค้า"] = Detailorder["CODE_PRODUCT"].ToString();
                                drsale["ชื่อสินค้า"] = Detailorder["PRODUCT_NAME"].ToString();
                                drsale["จำนวน"] = Detailorder["AMOUNT"].ToString();
                                drsale["ราคา"] = Detailorder["PRICE"].ToString();
                                drsale["ส่วนลด(%)"] = Detailorder["DISC_PERCENT"].ToString();
                                drsale["ส่วนลด(บาท)"] = Detailorder["DISC_THB"].ToString();
                                drsale["ราคาสินค้ารวม"] = Detailorder["TOTAL_PRICE"].ToString();
                                drsale["อัตรา Vat"] = Detailorder["PER_VAT"].ToString();
                                drsale["จำนวน Vat"] = Detailorder["ORDER_VAT"].ToString();
                                drsale["ค่าจัดส่ง"] = Detailorder["ORDER_TRANSPRICE"].ToString();
                                drsale["ราคาที่ต้องชำระ"] = Detailorder["FINAL_PRICE"].ToString();

                                drsale["วิธีการจัดส่ง"] = Detailorder["ORDER_TRANS"].ToString();
                                drsale["ขั้นตอน"] = Detailorder["FULFILL_STATUS"].ToString();
                                drsale["สถานะใบสั่งขาย"] = Detailorder["ORDER_STATUS"].ToString();
                                drsale["เลขพัสดุ"] = Detailorder["ORDER_TRACK"].ToString();
                                drsale["ช่องทางการชำระเงิน"] = Detailorder["PAYMENT_TERM"].ToString();


                                NumRow++;
                                OrderTotalprice += decimal.Parse(Detailorder["FINAL_PRICE"].ToString()) != null ? decimal.Parse(Detailorder["FINAL_PRICE"].ToString()) : 0;

                              
                            }
                            else
                            {
                                drsale = dtdatasale.NewRow(); // have new row on each iteration
                                drsale["วันที่สั่งซื้อ"] = "";
                                drsale["พนักงานขาย"] = "";
                                drsale["เลขที่ใบสั่งขาย"] = "";
                                drsale["แบรนด์"] = "";
                                drsale["ช่องทางการสั่งซื้อ"] = "";

                                drsale["ลูกค้า"] = "";
                                drsale["เบอร์"] = "";
                                drsale["ที่อยู่จัดส่ง"] = "";
                                drsale["รหัสไปรษณีย์"] = "";
                                drsale["ที่อยู่ใบกำกับภาษี"] = "";
                                drsale["รหัสไปรษณีย์(ใบกำกับภาษี)"] = "";
                                drsale["เลขที่ผู้เสียภาษี"] = "";
                                drsale["วันที่ลูกค้านัดรับสินค้า"] = "";
                                drsale["หมายเหตุ"] = "";

                                drsale["รหัสสินค้า"] = Detailorder["CODE_PRODUCT"].ToString();
                                drsale["ชื่อสินค้า"] = Detailorder["PRODUCT_NAME"].ToString();
                                drsale["จำนวน"] = Detailorder["AMOUNT"].ToString();
                                drsale["ราคา"] = Detailorder["PRICE"].ToString();
                                drsale["ส่วนลด(%)"] = Detailorder["DISC_PERCENT"].ToString();
                                drsale["ส่วนลด(บาท)"] = Detailorder["DISC_THB"].ToString();
                                drsale["ราคาสินค้ารวม"] = Detailorder["TOTAL_PRICE"].ToString();
                                drsale["อัตรา Vat"] = Detailorder["PER_VAT"].ToString();
                                drsale["จำนวน Vat"] = Detailorder["ORDER_VAT"].ToString();
                                drsale["ค่าจัดส่ง"] = Detailorder["ORDER_TRANSPRICE"].ToString();
                                drsale["ราคาที่ต้องชำระ"] = Detailorder["FINAL_PRICE"].ToString();

                                drsale["วิธีการจัดส่ง"] = Detailorder["ORDER_TRANS"].ToString();
                                drsale["ขั้นตอน"] = Detailorder["FULFILL_STATUS"].ToString();
                                drsale["สถานะใบสั่งขาย"] = Detailorder["ORDER_STATUS"].ToString();
                                drsale["เลขพัสดุ"] = Detailorder["ORDER_TRACK"].ToString();
                                drsale["ช่องทางการชำระเงิน"] = Detailorder["PAYMENT_TERM"].ToString();
                                NumRow++;
                                OrderTotalprice += decimal.Parse(Detailorder["FINAL_PRICE"].ToString()) != null ? decimal.Parse(Detailorder["FINAL_PRICE"].ToString()) : 0;

                                
                            }

                            OrderRepeat = Detail["ORDER_NO"].ToString().Trim();

                            dtdatasale.Rows.Add(drsale);
                        }


                    }

                    drsale = dtdatasale.NewRow(); // have new row on each iteration
                    drsale["วันที่สั่งซื้อ"] = "";
                    drsale["พนักงานขาย"] = "";
                    drsale["เลขที่ใบสั่งขาย"] = "";
                    drsale["แบรนด์"] = "";
                    drsale["ช่องทางการสั่งซื้อ"] = "";

                    drsale["ลูกค้า"] = "";
                    drsale["เบอร์"] = "";
                    drsale["ที่อยู่จัดส่ง"] = "";
                    drsale["รหัสไปรษณีย์"] = "";
                    drsale["ที่อยู่ใบกำกับภาษี"] = "";
                    drsale["รหัสไปรษณีย์(ใบกำกับภาษี)"] = "";
                    drsale["เลขที่ผู้เสียภาษี"] = "";
                    drsale["วันที่ลูกค้านัดรับสินค้า"] = "";
                    drsale["หมายเหตุ"] = "";

                    drsale["รหัสสินค้า"] = "";
                    drsale["ชื่อสินค้า"] = "";
                    drsale["จำนวน"] = "";
                    drsale["ราคา"] = "";
                    drsale["ส่วนลด(%)"] = "";
                    drsale["ส่วนลด(บาท)"] = "";
                    drsale["ราคาสินค้ารวม"] = "";
                    drsale["อัตรา Vat"] = "";
                    drsale["จำนวน Vat"] = "";
                    drsale["ค่าจัดส่ง"] = "TOTAL";
                    drsale["ราคาที่ต้องชำระ"] = String.Format("{0:##}", OrderTotalprice.ToString()); ;

                    drsale["วิธีการจัดส่ง"] = "";
                    drsale["ขั้นตอน"] = "";
                    drsale["สถานะใบสั่งขาย"] = "";
                    drsale["เลขพัสดุ"] = "";
                    drsale["ช่องทางการชำระเงิน"] = "";
                    dtdatasale.Rows.Add(drsale);

                }

            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportSaleOrderResultSOSA_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
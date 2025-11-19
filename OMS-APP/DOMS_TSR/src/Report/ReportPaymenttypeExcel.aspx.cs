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
    public partial class ReportPaymenttypeExcel : System.Web.UI.Page
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

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo != null)
            {
               APIUrl = "http://localhost:54545";
              
            }
            else
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            DataTable dt = new DataTable();
            DataTable lorderdetailInfo = new DataTable();

            Util aaa = new Util();
            string respstr = "";
            APIpath = APIUrl + "/api/support/ReportPaymenttype";

            using (var wb = new WebClient())
            {
                List<OrderPaymenttypeInfo> olist = new List<OrderPaymenttypeInfo>();
                List<OrderPaymenttypeInfo> resultExport = new List<OrderPaymenttypeInfo>();
                resultExport = (List<OrderPaymenttypeInfo>)Session["dataExportExcel"];

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

            List<OrderPaymenttypeInfo> lOrder = JsonConvert.DeserializeObject<List<OrderPaymenttypeInfo>>(respstr);
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
            dtdatasale.Columns.Add("Order No");
            dtdatasale.Columns.Add("แบรนด์");
            dtdatasale.Columns.Add("ลูกค้า");

            

            dtdatasale.Columns.Add("รหัสสินค้า");
            dtdatasale.Columns.Add("สินค้า");
            dtdatasale.Columns.Add("ราคา");
            dtdatasale.Columns.Add("จำนวน");
            dtdatasale.Columns.Add("ค่าจัดส่ง");
            dtdatasale.Columns.Add("ราคาสินค้ารวม");
            dtdatasale.Columns.Add("ภาษี");
            dtdatasale.Columns.Add("ราคาที่ต้องชำระ");
            
            

            dtdatasale.Columns.Add("ช่องทางชำระเงิน");
            dtdatasale.Columns.Add("เลขที่ผู้เสียภาษี");

            dtdatasale.Columns.Add("จำนวนงวด");
            dtdatasale.Columns.Add("จ่ายงวดละ");
            dtdatasale.Columns.Add("งวดแรก");
            dtdatasale.Columns.Add("ประเภทบัตร");
            dtdatasale.Columns.Add("ธนาคารผู้ออกบัตร");
            dtdatasale.Columns.Add("หมายเลขบัตร");
            dtdatasale.Columns.Add("CVV");
            dtdatasale.Columns.Add("ชื่อผู้ถือบัตร");
            dtdatasale.Columns.Add("เดือน/ปีที่หมดอายุ");
            dtdatasale.Columns.Add("รหัสบัตรประชาชน");
            dtdatasale.Columns.Add("วัน/เดือน/ปีเกิด");



            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                

                    decimal OrderTotalpriceRE = 0;
                    decimal SumPrice = 0;
                    decimal CountTotal = 0;
                    string Tax = "0";
                    decimal SumTransPort = 0;
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
                                drsale["sale Date"] = Detailorder["CreateDate"].ToString();
                                drsale["พนักงานขาย"] = Detailorder["sellName"].ToString();
                                drsale["Order No"] = Detailorder["OrderCode"].ToString();
                                drsale["แบรนด์"] = Detailorder["CampaignCategoryCode"].ToString();
                                drsale["ลูกค้า"] = Detailorder["CustomerFName"].ToString();

                                

                                drsale["รหัสสินค้า"] = Detailorder["Productcode"].ToString();
                                
                                drsale["สินค้า"] = Detailorder["ProductName"].ToString();
                                drsale["ราคา"] = String.Format("{0:0.00}", Detailorder["NetPrice"].ToString());
                                drsale["จำนวน"] = Detailorder["Amount"].ToString();
                                drsale["ค่าจัดส่ง"] = Detailorder["TransportPrice"].ToString();

                                
                                drsale["ราคาสินค้ารวม"] = Detailorder["Sumprice"].ToString();
                                drsale["ภาษี"] = Detailorder["PercentVat"].ToString()+"%";
                                drsale["ราคาที่ต้องชำระ"] = "";

                                
                                drsale["ช่องทางชำระเงิน"] = Detailorder["PaymentTypeName"].ToString();
                                drsale["เลขที่ผู้เสียภาษี"] = Detailorder["TaxId"].ToString();

                                drsale["จำนวนงวด"] = Detailorder["Installment"].ToString();
                                drsale["จ่ายงวดละ"] = Detailorder["InstallmentPrice"].ToString();
                                drsale["งวดแรก"] = Detailorder["FirstInstallment"].ToString();
                                drsale["ประเภทบัตร"] = Detailorder["CardType"].ToString();
                                drsale["ธนาคารผู้ออกบัตร"] = Detailorder["CardIssuename"].ToString();
                                drsale["หมายเลขบัตร"] = Detailorder["CardNo"].ToString();
                                drsale["CVV"] = Detailorder["CVCNo"].ToString();
                                drsale["ชื่อผู้ถือบัตร"] = Detailorder["CardHolderName"].ToString() ;
                                drsale["เดือน/ปีที่หมดอายุ"] = Detailorder["CardExpMonth"].ToString()+"/"+Detailorder["CardExpYear"].ToString();
                                drsale["รหัสบัตรประชาชน"] = Detailorder["CitizenId"].ToString();
                                drsale["วัน/เดือน/ปีเกิด"] = Detailorder["BirthDate"].ToString();


                                NumRow++;
                                Tax = (Detailorder["Vat"].ToString()) != null ? (Detailorder["Vat"].ToString()) : "0";
                                SumTransPort += decimal.Parse(Detailorder["TransportPrice"].ToString()) != null ? decimal.Parse(Detailorder["TransportPrice"].ToString()) : 0;
                                CountTotal += decimal.Parse(Detailorder["Amount"].ToString()) != null ? decimal.Parse(Detailorder["Amount"].ToString()) : 0;
                                OrderTotalpriceRE = decimal.Parse(Detailorder["orderTotalPrice"].ToString()) != null ? decimal.Parse(Detailorder["orderTotalPrice"].ToString()) : 0;
                                SumPrice = decimal.Parse(Detailorder["SumbyProductprice"].ToString()) != null ? decimal.Parse(Detailorder["SumbyProductprice"].ToString()) : 0;
                            }
                            else
                            {
                                drsale = dtdatasale.NewRow(); // have new row on each iteration
                                drsale["sale Date"] = "";
                                drsale["พนักงานขาย"] = "";
                                drsale["Order No"] = "";
                                drsale["แบรนด์"] = "";
                                drsale["ลูกค้า"] = "";

                                

                                drsale["รหัสสินค้า"] = Detailorder["Productcode"].ToString();
                                
                                drsale["สินค้า"] = Detailorder["ProductName"].ToString();
                               
                                drsale["ราคา"] = Detailorder["NetPrice"].ToString();
                                drsale["จำนวน"] = Detailorder["Amount"].ToString();
                                drsale["ค่าจัดส่ง"] = "";

                                drsale["ราคาสินค้ารวม"] = Detailorder["Sumprice"].ToString();
                                drsale["ภาษี"] = "";
                                drsale["ราคาที่ต้องชำระ"] = "";
                                

                                drsale["จำนวนงวด"] = "";
                                drsale["จ่ายงวดละ"] = "";
                                drsale["งวดแรก"] = "";
                                drsale["ประเภทบัตร"] = "";
                                drsale["ธนาคารผู้ออกบัตร"] = "";
                                drsale["หมายเลขบัตร"] = "";
                                drsale["CVV"] = "";
                                drsale["ชื่อผู้ถือบัตร"] = "";
                                drsale["เดือน/ปีที่หมดอายุ"] = "";
                                drsale["รหัสบัตรประชาชน"] = "";
                                drsale["วัน/เดือน/ปีเกิด"] = "";
                                NumRow++;
                                SumTransPort += decimal.Parse(Detailorder["TransportPrice"].ToString()) != null ? decimal.Parse(Detailorder["TransportPrice"].ToString()) : 0;
                                CountTotal += decimal.Parse(Detailorder["Amount"].ToString()) != null ? decimal.Parse(Detailorder["Amount"].ToString()) : 0;
                                OrderTotalpriceRE = decimal.Parse(Detailorder["orderTotalPrice"].ToString()) != null ? decimal.Parse(Detailorder["orderTotalPrice"].ToString()) : 0;
                                SumPrice = decimal.Parse(Detailorder["SumbyProductprice"].ToString()) != null ? decimal.Parse(Detailorder["SumbyProductprice"].ToString()) : 0;
                            }

                            

                            dtdatasale.Rows.Add(drsale);
                        }
                    drsale = dtdatasale.NewRow(); // have new row on each iteration
                    drsale["sale Date"] = "";
                    drsale["พนักงานขาย"] = "";
                    drsale["Order No"] = "";
                    drsale["แบรนด์"] = "";
                    drsale["ลูกค้า"] = "";

                    

                    drsale["รหัสสินค้า"] = "";
                    
                    drsale["สินค้า"] = "";
                    drsale["ราคา"] = "Total";
                    drsale["จำนวน"] = "";
                    drsale["ค่าจัดส่ง"] = String.Format("{0:##}", SumTransPort.ToString());
                    drsale["ราคาสินค้ารวม"] = String.Format("{0:##}", SumPrice.ToString());
                    drsale["ภาษี"] = String.Format("{0:##}", Tax.ToString());
                    drsale["ราคาที่ต้องชำระ"] = String.Format("{0:##}", OrderTotalpriceRE.ToString()); 
                    
                    drsale["จำนวนงวด"] = "";
                    drsale["จ่ายงวดละ"] = "";
                    drsale["งวดแรก"] = "";
                    drsale["ประเภทบัตร"] = "";
                    drsale["ธนาคารผู้ออกบัตร"] = "";
                    drsale["หมายเลขบัตร"] = "";
                    drsale["CVV"] = "";
                    drsale["ชื่อผู้ถือบัตร"] = "";
                    drsale["เดือน/ปีที่หมดอายุ"] = "";
                    drsale["รหัสบัตรประชาชน"] = "";
                    drsale["วัน/เดือน/ปีเกิด"] = "";

                    dtdatasale.Rows.Add(drsale);

                }
                  
                 

                

            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportPaymenttype_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
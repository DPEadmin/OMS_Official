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
    public partial class ReportCampaignPromotionByProduct_Excel : System.Web.UI.Page
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
            APIpath = APIUrl + "/api/support/ReaportCampaignPromotionByProduct";

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

            List<ReaportCampaignPromotionByProductInfo> lOrder = JsonConvert.DeserializeObject<List<ReaportCampaignPromotionByProductInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ReportCampaignPromotionByProduct.xlsx"));

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
            dtdatasale.Columns.Add("Sale Code");
            dtdatasale.Columns.Add("Sale Name");
            dtdatasale.Columns.Add("Order Date");
            dtdatasale.Columns.Add("Order Number");
            dtdatasale.Columns.Add("Campaign Code");

            dtdatasale.Columns.Add("Campaign Name");
            dtdatasale.Columns.Add("Promotion Code");
            dtdatasale.Columns.Add("Promotion Name");
            dtdatasale.Columns.Add("Product Code");
            dtdatasale.Columns.Add("Product Name");
            dtdatasale.Columns.Add("QTY");
            dtdatasale.Columns.Add("Amount");

            dtdatasale.Columns.Add("Payment Term");
            dtdatasale.Columns.Add("Channel");
            dtdatasale.Columns.Add("Brand");
            dtdatasale.Columns.Add("OrderState");
            dtdatasale.Columns.Add("OrderStatus");

            dtdatasale.Columns.Add("Customer Code");
            dtdatasale.Columns.Add("Customer Name");


            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                

                    
                    DataView ViewOrderinf_id = new DataView(dt);
                    
                    foreach (DataRow Detail in RoundTable.Rows)
                    {
                    decimal CountTotal = 0;
                    decimal OrderTotalpriceRE = 0;
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
                                drsale["Sale Code"] = Detailorder["SALE_CODE"].ToString();
                                drsale["Sale Name"] = Detailorder["SALE_NAME"].ToString();
                                drsale["Order Date"] = Detailorder["ORDER_DATE"].ToString();
                                drsale["Order Number"] = Detailorder["ORDER_NO"].ToString();
                                drsale["Campaign Code"] = Detailorder["CAMPAIGN_CODE"].ToString();

                                drsale["Campaign Name"] = Detailorder["CAMPAIGN_NAME"].ToString();
                                drsale["Promotion Code"] = Detailorder["PROMOTION_CODE"].ToString();
                                drsale["Promotion Name"] = Detailorder["PROMOTION_NAME"].ToString();
                                drsale["Product Code"] = Detailorder["PRODUCT_CODE"].ToString();
                                drsale["Product Name"] = Detailorder["PRODUCT_NAME"].ToString();
                                drsale["QTY"] = Detailorder["AMOUNT"].ToString();
                                drsale["Amount"] = String.Format("{0:0.00}", Detailorder["PRICE"].ToString());
                                drsale["Payment Term"] = Detailorder["PAYMENT_TERM"].ToString();
                                drsale["Channel"] = Detailorder["CHANNEL"].ToString(); 
                                drsale["Brand"] = Detailorder["BRAND"].ToString();
                                drsale["OrderState"] = decimal.Parse(Detailorder["ORDER_STATE"].ToString()) == 1 ? Detailorder["ORDER_STATE"] = "กำลังจัดส่งสินค้า".ToString() : Detailorder["ORDER_STATE"] = "อะไรก็ไม่รู้" ;
                                drsale["OrderStatus"] = decimal.Parse(Detailorder["ORDER_STATUS"].ToString()) == 1 ? Detailorder["ORDER_STATUS"] = "สินค้าอยู่ระหว่างทางจัดส่ง".ToString() : Detailorder["ORDER_STATUS"] = "อะไรก็ไม่รู้";
                                drsale["Customer Code"] = Detailorder["CUSTOMER_CODE"].ToString(); 
                                drsale["Customer Name"] = Detailorder["CUSTOMER_NAME"].ToString();
                                NumRow++;
                                CountTotal += decimal.Parse(Detailorder["AMOUNT"].ToString()) != null ? decimal.Parse(Detailorder["AMOUNT"].ToString()) : 0;
                                OrderTotalpriceRE += decimal.Parse(Detailorder["PRICE"].ToString()) != null ? decimal.Parse(Detailorder["PRICE"].ToString()) : 0;
                                
                        }
                            else
                            {
                                drsale = dtdatasale.NewRow(); // have new row on each iteration
                 
                                drsale["Sale Code"] = "";
                                drsale["Sale Name"] = "";
                                drsale["Order Date"] = "";
                                drsale["Order Number"] = "";
                                drsale["Campaign Code"] = "";

                                drsale["Campaign Name"] = "";
                                drsale["Promotion Code"] = "";
                                drsale["Promotion Name"] = "";
                                drsale["Product Code"] = Detailorder["PRODUCT_CODE"].ToString();
                                drsale["Product Name"] = Detailorder["PRODUCT_NAME"].ToString();
                                drsale["QTY"] = Detailorder["AMOUNT"].ToString();
                                drsale["Amount"] = String.Format("{0:0.00}", Detailorder["PRICE"].ToString());
                                drsale["Payment Term"] = "";
                                drsale["Channel"] = "";
                                drsale["Brand"] = "";
                                drsale["OrderState"] = "";
                                drsale["OrderStatus"] = "";
                                drsale["Customer Code"] = "";
                                drsale["Customer Name"] = "";
                            NumRow++;
                                CountTotal += decimal.Parse(Detailorder["AMOUNT"].ToString()) != null ? decimal.Parse(Detailorder["AMOUNT"].ToString()) : 0;
                                OrderTotalpriceRE += decimal.Parse(Detailorder["PRICE"].ToString()) != null ? decimal.Parse(Detailorder["PRICE"].ToString()) : 0;
                        }

                            

                            dtdatasale.Rows.Add(drsale);
                        }
                    drsale = dtdatasale.NewRow(); // have new row on each iteration

                    drsale["Sale Code"] = "";
                    drsale["Sale Name"] = "";
                    drsale["Order Date"] = "";
                    drsale["Order Number"] = "";
                    drsale["Campaign Code"] = "";

                    drsale["Campaign Name"] = "";
                    drsale["Promotion Code"] = "";
                    drsale["Promotion Name"] = "";
                    drsale["Product Code"] = "";
                    drsale["Product Name"] = "ToTal";
                    drsale["QTY"] = CountTotal.ToString();
                    drsale["Amount"] = String.Format("{0:##}", OrderTotalpriceRE.ToString()); 
                    drsale["Payment Term"] = "";
                    drsale["Channel"] = "";
                    drsale["Brand"] = "";
                    drsale["OrderState"] = "";
                    drsale["OrderStatus"] = "";
                    drsale["Customer Code"] = "";
                    drsale["Customer Name"] = "";
                    dtdatasale.Rows.Add(drsale);
                }

                    

                

            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportCampaignPromotionByProduct_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
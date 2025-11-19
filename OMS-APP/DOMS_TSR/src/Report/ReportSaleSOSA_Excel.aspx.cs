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
    public partial class ReportSaleSOSA_Excel : System.Web.UI.Page
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
            APIpath = APIUrl + "/api/support/ReaportResultSOSA";

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

            List<ReportResultSOSAInfo> lOrder = JsonConvert.DeserializeObject<List<ReportResultSOSAInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
            
            dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ReportResultSOSA.xlsx"));

            DataTable RoundTable;
            DataView viewround;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "SALE_CODE");
            //new datable for excel
            DataTable dtdatasale = new DataTable();
            

            dtdatasale.Columns.Add("รหัสพนักงานขาย");
            dtdatasale.Columns.Add("ชื่อพนักงานขาย");
            dtdatasale.Columns.Add("วันที่สั่งซื้อ");
            dtdatasale.Columns.Add("ช่องทางการขาย");
            dtdatasale.Columns.Add("แบรนด์");

            dtdatasale.Columns.Add("สถานะใบสั่งขาย");
            dtdatasale.Columns.Add("สถานะออเดอร์");
            dtdatasale.Columns.Add("จำนวนใบสั่งขาย");
            dtdatasale.Columns.Add("จำนวนสินค้า");
            dtdatasale.Columns.Add("ราคารวม");



            DataRow drsale = null;

            
            if (dt.Rows.Count > 0)
            {

                


                DataView ViewOrderinf_id = new DataView(dt);
                


                DataTable Orderinfo = ViewOrderinf_id.ToTable();
                foreach (DataRow Detailorder in Orderinfo.Rows)
                {

                    
                    drsale = dtdatasale.NewRow(); // have new row on each iteration

                    
                    drsale["รหัสพนักงานขาย"] = Detailorder["SALE_CODE"].ToString();
                    drsale["ชื่อพนักงานขาย"] = Detailorder["SALE_NAME"].ToString();
                    drsale["วันที่สั่งซื้อ"] = Detailorder["ORDER_DATE"].ToString();

                    drsale["ช่องทางการขาย"] = Detailorder["CHANNEL"].ToString();
                    drsale["แบรนด์"] = Detailorder["BRAND"].ToString();

                    drsale["สถานะใบสั่งขาย"] = Detailorder["ORDER_STAGE"].ToString();
                    drsale["สถานะออเดอร์"] = Detailorder["ORDER_STATUS"].ToString();
                    drsale["จำนวนใบสั่งขาย"] = Detailorder["TOTAL_ORDER"].ToString();
                    drsale["จำนวนสินค้า"] = Detailorder["TOTAL_QTY"].ToString();
                    drsale["ราคารวม"] = Detailorder["TOTAL_AMOUNT"].ToString();
                    

                    dtdatasale.Rows.Add(drsale);
                }

                


            }

            if (dtdatasale.Rows.Count > 0)
            {
                exx.LoadData(dtdatasale, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= ReportSaleSOSA_" + DateTime.Now.ToString("ddmmyy") + ".xlsx");
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
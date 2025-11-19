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
using DOMS_TSR.Classes.DTO;
using System.IO;

namespace DOMS_TSR.src.Report
{
    public partial class ReportExcel_Aston : System.Web.UI.Page
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
            APIpath = APIUrl + "/api/support/ThemplateAston";
         
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

            List<ReportSaleAstonInfo> lOrder = JsonConvert.DeserializeObject<List<ReportSaleAstonInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
               
                 dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/Report/ExportExcel/{0}", "ExportSaleAston.xlsx"));
         
            if (dt.Rows.Count > 0)
            {
                exx.LoadData(dt, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename= SaleOrder_"+DateTime.Now.ToString("ddmmyy") +".xlsx");
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
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

namespace DOMS_TSR.src.FullfillOrderlist
{
    public partial class OLExportExcel : System.Web.UI.Page
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
                LoadDar((Request.QueryString["date"] != null) ? Request.QueryString["date"].ToString() : "");
            }
        }
        protected void LoadDar(string StrDate)
        {
            DataTable dt = new DataTable();
            
            DataTable lorderdetailInfo = new DataTable();
            Util aaa = new Util();
            string respstr = "";
            APIpath = APIUrl + "/api/support/OList";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderListDate"] = StrDate;



                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderOLExcelInfo> lOrder = JsonConvert.DeserializeObject<List<OrderOLExcelInfo>>(respstr);
            ExportToExcel exx = new ExportToExcel();
                 
                 dt = exx.ConvertToDataTable(lOrder);
            string fileName = Server.MapPath(string.Format("~/src/FullfillOrderlist/ExportExcel/{0}", "ExportOL.xlsx"));
         
            if (dt.Rows.Count > 0)
            {
                exx.LoadData(dt, fileName, "sheetname");
                this.Response.Clear();
                this.Response.ContentType = "application/vnd.ms-excel";
                this.Response.AddHeader("Content-Disposition", "attachment; filename=ExportOL.xlsx");
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;

using System.ComponentModel;
namespace DOMS_TSR.src.FullfillOrderlist
{
    public partial class OrderChart : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                loadDataBarchart();
                
            }
            
          
        }
        protected void loadDataBarchart()
        {
            List<OrderInfo> lPromotionInfo = new List<OrderInfo>();
            DataTable dt = new DataTable();
            lPromotionInfo = GetPromotionMasterByCriteria();
            string[] x = new string[lPromotionInfo.Count];
            decimal[] y = new decimal[lPromotionInfo.Count];

            int i = 0;
            foreach (var od in lPromotionInfo.ToList())
            {
                x[i] = od.shipmentdate;
                y[i] = Convert.ToDecimal(od.OrderTotalAmount);

                i++;
            }
            BarChart1.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, BarColor = "#ffb366",Name = "Sell of Day" });
         
            BarChart1.CategoriesAxis = string.Join(",", x);
            BarChart1.ChartTitle = "Sell (Baht)";
            if (x.Length > 3)
            {
                BarChart1.ChartWidth = (x.Length * 90).ToString();
                BarChart1.ChartHeight = (x.Length * 50).ToString();
            }
            else
            {
                BarChart1.ChartWidth = "400";
                BarChart1.ChartHeight = "350";
            }
            BarChart1.Visible = true;
        }
        protected void loadDataPieChart()
        {
            

         
        }
        public List<OrderInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/barchart";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CreateDate"] = txtSearchStartDateFrom.Text;

                data["StatusPage"] = "P";

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lPromotionInfo;

        }
        public List<OrderPieChartInfo> GetPiechartMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/PieChart";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Date"] = txtSearchStartDateFrom.Text;

                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderPieChartInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<OrderPieChartInfo>>(respstr);


            return lPromotionInfo;

        }
        public static string ValCurrent(Decimal? obj)
        {
            string ret = "0.00";
            if (obj != null)
            {
                ret = String.Format("{0:#,##0.00}", obj.Value);
            }
            return ret;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadDataBarchart();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchStartDateFrom.Text = "";


        }
    }
}
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
    public partial class OrderStatusChart : System.Web.UI.Page
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

                loadDataPieChart();
            }
    
        }
  
        protected void loadDataPieChart()
        {
            List<OrderPieChartInfo> lPromotionInfo = new List<OrderPieChartInfo>();
            DataTable dt = new DataTable();
            lPromotionInfo = GetPiechartMasterByCriteria();

            //Loop and add each datatable row to the Pie Chart Values
            int i = 0; int j = 0; string color = "";

            foreach (var od in lPromotionInfo.ToList())
            {
                string[] colors = { "#F9E79F", "#F5CBA7", "#85C1E9", "#ABEBC6", "#FAD7A0", "#E8DAEF", "brown", "black", "skyblue", "", "darkblue", "darkbrown" };


                if (i < 10)
                {
                    color = colors[j];
                    j++;
                }
                PieChart1.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                {
                    Category = od.OrderStatusCode,
                    Data = Convert.ToDecimal(od.Amount)
                 , PieChartValueColor = color
      
                });
                i++;
            }
            PieChart1.ChartTitle = "Order of Day";
            PieChart1.ChartWidth = "700";
            PieChart1.ChartHeight = "700";
            PieChart1.Visible = true;


        }
        public List<OrderInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/barchart";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["shipmentdate"] = "";

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
            loadDataPieChart();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchStartDateFrom.Text = "";


        }
    }
}
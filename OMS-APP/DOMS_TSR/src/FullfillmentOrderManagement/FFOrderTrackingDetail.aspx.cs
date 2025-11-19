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
namespace DOMS_TSR.src.FullfillmentOrderManagement
{
    public partial class FFOrderTrackingDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        L_OrderChangestatus result = new L_OrderChangestatus();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                
                ValidateOrderTracking();
            }
               
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            saveOrder();
            Response.Redirect("FFOrderTracking.aspx");
        }
        protected void ValidateOrderTracking()
        {
            List<OrderInfo> lOrderInfo = new List<OrderInfo>();
            lOrderInfo = GetOrderListByCriteria();
            var ordertrack = lOrderInfo[0].OrderTrackingNo.ToString();
            if (ordertrack == "")
            {
                txttrackingNo.Text = "";

            }
            else 
            {

                txttrackingNo.Text = ordertrack;
                
            }
        }
        public List<OrderInfo> GetOrderListByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderManagementNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                respstr = Encoding.UTF8.GetString(wb.UploadValues(APIpath, "POST", data));
            }

            List<OrderInfo> lOrderInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);

            return lOrderInfo;
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("FFOrderTracking.aspx");
        }
        protected void saveOrder()
        {
            result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Request.QueryString["OrderCode"].Trim(), OrderTracking = txttrackingNo.Text, OrderNote = txtDetailbackOrder.Text });

            APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                var dataString = client.UploadString(APIpath, jsonObj);

            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
          txttrackingNo.Text=  getTrackingNo();
        }
        protected string getTrackingNo()
        {
            ThaiPostInfo mon = new ThaiPostInfo();
            List<ThaiPostInfo> lThaiPostInfo = new List<ThaiPostInfo>();
            int no3 = 8, no4 = 6, no5 = 4, no6 = 2, no7 = 3, no8 = 5, no9 = 9, no10 = 7;
            string classname = "EB", CustomerCode = "21", runnigno ="", CheckDigit = "", Nation = "TH";

          

            runnigno = OrderTrackNo();
            char[] arrayCustomerCode = CustomerCode.ToCharArray();
            char[] arrayrunnigno = runnigno.ToCharArray();
            int r3 = no3 * int.Parse(arrayCustomerCode[0].ToString());
            int r4 = no4 * int.Parse(arrayCustomerCode[1].ToString());
            int r5 = no5 * int.Parse(arrayrunnigno[0].ToString());
            int r6 = no6 * int.Parse(arrayrunnigno[1].ToString());
            int r7 = no7 * int.Parse(arrayrunnigno[2].ToString());
            int r8 = no8 * int.Parse(arrayrunnigno[3].ToString());
            int r9 = no9 * int.Parse(arrayrunnigno[4].ToString());
            int r10 = no10 * int.Parse(arrayrunnigno[5].ToString());

            int SumCheckDigit = r3 + r4 + r5 + r6 + r7 + r8 + r9 + r10;

            int resultSumChecklist = SumCheckDigit / 11;


            string aaaa = string.Format("{0:0.0}", resultSumChecklist);
            string[] words = aaaa.ToString().Split('.');
            switch (int.Parse(words[1].ToString()))
            {
                case 0:
                    CheckDigit = "5";
                    break;
                case 1:
                    CheckDigit = "0";
                    break;

                default:
                    CheckDigit = (11 - int.Parse(words[1].ToString())).ToString();
                    break;
            }

            string result = classname + CustomerCode + runnigno + CheckDigit + Nation;
            return result;
        }

        public String OrderTrackNo()
        {
            ThaiPostInfo mon = new ThaiPostInfo();
            List<ThaiPostInfo> lThaiPostInfo = new List<ThaiPostInfo>();
            string runnigno;
            APIpath = APIUrl + "/api/support/OrderTrackingNo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { mon });
                var dataString = client.UploadString(APIpath, jsonObj);
                runnigno = JsonConvert.DeserializeObject<String>(dataString.ToString());
            }
            return runnigno;

        }
    }  
    
}
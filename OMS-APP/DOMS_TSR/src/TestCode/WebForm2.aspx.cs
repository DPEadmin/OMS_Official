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
using System.IO;
using OfficeOpenXml;
namespace DOMS_TSR.src.TestCode
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string token;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gettoken();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LThaiPostInfo mon = new LThaiPostInfo();
            List<LThaiPostInfo> lThaiPostInfo = new List<LThaiPostInfo>();

            string APIUrl = "https://trackapi.thailandpost.co.th/post/api/v1/track";
            string aaaa = "EG327058400TH";
            //string aaaa = "EG327058400TH,EG327057903TH,EG327057846TH";
            var testcode = aaaa.TrimStart('[').TrimEnd(']').Split(',');
            using (var client = new WebClient())
            {

                mon.status = "all";
                mon.language = "TH";
                mon.barcode = testcode;

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.status,
                    mon.language,
                    mon.barcode,
                });

                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Token " + Label1.Text;

                var dataString = client.UploadString(APIUrl, jsonObj);


                thaipost datalist = JsonConvert.DeserializeObject<thaipost>(dataString.ToString());
             
            }

        }
        protected void gettoken() 
        {
        ThaiPostInfo mon = new ThaiPostInfo();
        List<ThaiPostInfo> lThaiPostInfo = new List<ThaiPostInfo>();

        string APIUrl = "https://trackapi.thailandpost.co.th/post/api/v1/authenticate/token";
        using (var client = new WebClient())
        {
            mon.token = "";
            mon.expire = "";


            var jsonObj = JsonConvert.SerializeObject(new
            {
                mon.token,
                mon.expire,
            });

            client.Encoding = Encoding.UTF8;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Authorization] = "Token VuP.H;W2FNBXJgEoI?C.LtBrVBNzYzInZYHzExQtGaBJP/X-I@AOD_C3H+AsZ?ZkEeDiQRU#OTQvV.I1NaB&W!WKTCYnEfV1X7OU";

            var dataString = client.UploadString(APIUrl, jsonObj);
      
            ThaiPostInfo datalist = JsonConvert.DeserializeObject<ThaiPostInfo>(dataString.ToString());
            Label1.Text = datalist.token.ToString();

        }
        }

        protected void getTrackingNo()
        {
            ThaiPostInfo mon = new ThaiPostInfo();
            List<ThaiPostInfo> lThaiPostInfo = new List<ThaiPostInfo>();
            int no3 = 8, no4 = 6, no5 = 4, no6 = 2, no7 = 3, no8 = 5, no9 = 9, no10 = 7;
            string classname = "EB", CustomerCode = "21", runnigno = "000001", CheckDigit = "",Nation = "TH" ;

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

       
            string aaaa =   string.Format("{0:0.0}", resultSumChecklist);
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
                    CheckDigit = (11- int.Parse(words[1].ToString())).ToString();
                    break;
            }

            string result = classname + CustomerCode + runnigno + CheckDigit + Nation;
            lbltracking.Text = result;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            getTrackingNo();
        }
    }
}
using Microsoft.AspNet.SignalR;
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
using System.Web.Services;
using System.Media;
using System.Threading;
using System.Threading.Tasks;

namespace DOMS_TSR.src.TestCode
{
    public partial class OrderBranchTest : System.Web.UI.Page
    {
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        decimal? total = 0;
        string CustomerCode = "";
        string CustomerPhone = "";
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                  
                    List<EmpBranchInfo> lb = new List<EmpBranchInfo>();

                    lb = ListEmpBranchByCriteria(empInfo.EmpCode);

                    if(lb.Count >0)
                    {
                        hidBranchcode.Value = lb[0].BranchCode;
                        hiddisplayname.Value = lb[0].BranchCode;

                    }
                }

                btn01.BackColor = System.Drawing.Color.Blue;
                btn02.BackColor = System.Drawing.Color.Blue;
                btn03.BackColor = System.Drawing.Color.Blue;
                
            }
        }
      
 
        protected void btnSend_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<myChatHub>();

            hubContext.Clients.All.broadcasttest("Hello World");
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string msg = hidordermsg.Value;

            int? couorder = 0;

            couorder = CountOrderListByCriteria(hidBranchcode.Value, "01");

            btn01.Text = "สร้างใบสั่งขาย (" + couorder.ToString() + ")";

            couorder = CountOrderListByCriteria(hidBranchcode.Value, "02");

            btn02.Text = "อนุมัติใบสั่งขาย (" + couorder.ToString() + ")";

            couorder = CountOrderListByCriteria(hidBranchcode.Value, "03");

            btn03.Text = "จัดส่งสินค้า (" + couorder.ToString() + ")";

            switch (msg)
            {
                case "01":
                    btn01.BackColor = System.Drawing.Color.Red;
                    btn02.BackColor = System.Drawing.Color.Blue;
                    btn03.BackColor = System.Drawing.Color.Blue;
                    break;
                case "02":
                    btn01.BackColor = System.Drawing.Color.Blue;
                    btn02.BackColor = System.Drawing.Color.Red;
                    btn03.BackColor = System.Drawing.Color.Blue;
                    break;
                case "03":
                    btn01.BackColor = System.Drawing.Color.Blue;
                    btn02.BackColor = System.Drawing.Color.Blue;
                    btn03.BackColor = System.Drawing.Color.Red;
                    break;
            }

           for(int i = 0; i < 3; i++)
            {

                LoadSound();
         
            }
            


        }
        protected void LoadSound()
        {

            SoundPlayer player = new SoundPlayer(Server.MapPath("~/assets/call.wav")); 
            

            Task.Run(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    player.Play();
                    Thread.Sleep(5000);

                }
            });

           
        }
        public static int? CountOrderListByCriteria(string Branchcode,string orderstatus)
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/CountOrderListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["BranchCode"] = Branchcode;

                data["OrderStatusCode"] = orderstatus;

                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
          
            return cou;

        }

        public static List<EmpBranchInfo> ListEmpBranchByCriteria(string empcode)
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/ListEmpBranchByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empcode;
                data["rowOFFSet"] = "0";
                data["rowFetch"] = PAGE_SIZE.ToString();
                
                 var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpBranchInfo> lEmpBranchInfo = JsonConvert.DeserializeObject<List<EmpBranchInfo>>(respstr);

            return lEmpBranchInfo;

        }

    }
}
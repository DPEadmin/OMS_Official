using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using System.Globalization;
using System.IO;

namespace DOMS_TSR.src.OrderManagement
{
    public partial class OrderHistoryDetail : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl;

        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    APIUrl = empInfo.ConnectionAPI;
                }
                else
                {
                    Response.Redirect("..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                loadgvOrderHistoryDetail();
            }
        }
        #region event
        protected void btnBack_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region function
        protected void loadgvOrderHistoryDetail()
        {
            lblHeadOrderCode.Text = Request.QueryString["OrderCode"];

            List<OrderDetailInfo> lodInfo = new List<OrderDetailInfo>();
            lodInfo = GetOrderHistoryDetailMasterByCriteria(lblHeadOrderCode.Text);

            gvOrderHistoryDetail.DataSource = lodInfo;
            gvOrderHistoryDetail.DataBind();
        }
        protected List<OrderDetailInfo> GetOrderHistoryDetailMasterByCriteria(String ordercode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/GetOrderHistoryatSalesOrderPage";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = ordercode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<OrderDetailInfo> lOrderDetailInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lOrderDetailInfo;
        }
        #endregion
    }
}
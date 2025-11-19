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
using AjaxControlToolkit;
using System.IO;

namespace DOMS_TSR.src.UserControl
{
    public partial class UserControlOrderChangeStatus : System.Web.UI.UserControl
    {
        protected static string APIUrl;
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
                    APIUrl = empInfo.ConnectionAPI;
                    List<EmpBranchInfo> lb = new List<EmpBranchInfo>();

                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                LoadOrder();
                LoadOrderDetailMapProduct();
            }
        }
        #region Function

        protected void LoadOrder()
        {
            List<OrderInfo> lOrderInfo = new List<OrderInfo>();

            lOrderInfo = GetOrderListByCriteria();

            lblOrderCode.Text = lOrderInfo[0].OrderCode == "" ? lblOrderCode.Text = "-" : lblOrderCode.Text = lOrderInfo[0].OrderCode;

            lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd-MM-yyyy") == "" ? lblCreateDate.Text = "-" : lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd-MM-yyyy");

            

            lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd-MM-yyyy") == "" ? lblDeliveryDate.Text = "-" : lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd-MM-yyyy");

            lblOrderStatus.Text = lOrderInfo[0].OrderStatusName == "" ? lblOrderStatus.Text = "-" : lblOrderStatus.Text = lOrderInfo[0].OrderStatusName;

            


            lblBranchOrderID.Text = lOrderInfo[0].BranchOrderID == "" ? lblBranchOrderID.Text = "-" : lblBranchOrderID.Text = lOrderInfo[0].BranchOrderID;

            
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
        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void LoadOrderDetailMapProduct()
        {
            List<OrderDetailInfo> lProductInfo = new List<OrderDetailInfo>();

            lProductInfo = GetListOrderDetailMapProductNopagingByCriteria();

            gvProduct.DataSource = lProductInfo;

            gvProduct.DataBind();
        }
        public List<OrderDetailInfo> GetListOrderDetailMapProductNopagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderDetailMapProductNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderDetailInfo> lProductInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lProductInfo;

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            saveOrder();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtDetailbackOrder.Text = "";
        }
        protected void saveOrder()
        {
            result.L_OrderChangestatusInfo.Add(new OrderChangestatusInfo() { updateBy = hidEmpCode.Value.ToString(), ordercode = Request.QueryString["OrderCode"].Trim(), orderstate = StaticField.OrderState_12,OrderNote=txtDetailbackOrder.Text });
          
            APIpath = APIUrl + "/api/support/OrderChangeStatusInfo";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_OrderChangestatusInfo });
                var dataString = client.UploadString(APIpath, jsonObj);

            }
        }
        #endregion
    }
}
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

namespace DOMS_TSR.src
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";

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
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                LoadOrderMapCustomer();
                LoadOrder();
                LoadAddress();
                LoadOrderDetailMapProduct();
                LoadListOrderActivity();
            }
        }

        #region Function

        protected void LoadOrderMapCustomer()
        {
            List<OrderDetailInfo> lOrderDetailInfo = new List<OrderDetailInfo>();

            lOrderDetailInfo = GetOrderMapCustomerByCriteria();

            lblCustomerName.Text = lOrderDetailInfo[0].CustomerName;

            lblCustomerCode.Text = lOrderDetailInfo[0].CustomerCode;

            LoadCustomerPhoneSecond(lOrderDetailInfo[0].CustomerCode);
            GetCustomerPhone(lOrderDetailInfo[0].CustomerCode);
        }

        public List<OrderDetailInfo> GetOrderMapCustomerByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderMapCustomerNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                respstr = Encoding.UTF8.GetString(wb.UploadValues(APIpath, "POST", data));
            }

            List<OrderDetailInfo> lOrderDetailInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lOrderDetailInfo;
        }

        protected void LoadCustomerPhoneSecond(string CustomerCode)
        {
            List<OrderDetailInfo> lOrderDetailInfo = new List<OrderDetailInfo>();

            lOrderDetailInfo = GetCustomerPhoneByCriteria(CustomerCode);

            if (lOrderDetailInfo.Count > 1)
            {
                lblCustomerTel2.Text = lOrderDetailInfo[1].PhoneNumber;
            }
            
        }
        protected void GetCustomerPhone(String cuscode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerPhone";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = cuscode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerPhoneInfo> lCustomerPhoneInfo = JsonConvert.DeserializeObject<List<CustomerPhoneInfo>>(respstr);


            if (lCustomerPhoneInfo.Count >= 1)
            {
                lblCustomerTel1.Text = lCustomerPhoneInfo[0].CustomerContactTel;
            }
        }
        public List<OrderDetailInfo> GetCustomerPhoneByCriteria(string CustomerCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListGetCustomerPhoneNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["CustomerCode"] = CustomerCode
                };

                respstr = Encoding.UTF8.GetString(wb.UploadValues(APIpath, "POST", data));
            }

            List<OrderDetailInfo> lOrderDetailInfo = JsonConvert.DeserializeObject<List<OrderDetailInfo>>(respstr);

            return lOrderDetailInfo;
        }

        protected void LoadOrder()
        {
            List<OrderInfo> lOrderInfo = new List<OrderInfo>();
            try
            {    
                lOrderInfo = GetOrderListByCriteria();

                lblOrderCode.Text = lOrderInfo[0].OrderCode == "" ? lblOrderCode.Text = "-" : lblOrderCode.Text = lOrderInfo[0].OrderCode;

                lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd-MM-yyyy") == "" ? lblCreateDate.Text = "-" : lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd-MM-yyyy");

                lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd-MM-yyyy") == "" ? lblDeliveryDate.Text = "-" : lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd-MM-yyyy");

                lblOrderStatus.Text = lOrderInfo[0].OrderStatusName == "" ? lblOrderStatus.Text = "-" : lblOrderStatus.Text = lOrderInfo[0].OrderStatusName;
                lblBranchOrderID.Text = lOrderInfo[0].BranchOrderID == "" ? lblBranchOrderID.Text = "-" : lblBranchOrderID.Text = lOrderInfo[0].BranchOrderID;

                lblOrderNote.Text = lOrderInfo[0].OrderNote == "" ? lblOrderNote.Text = "-" : lblOrderNote.Text = lOrderInfo[0].OrderNote;
                lblvat.Text = lOrderInfo[0].PercentVat.ToString();
                lblvat.Text += " %:";
                // add ordertracking
                lblOrderTrackinNo.Text = lOrderInfo[0].OrderTrackingNo.ToString();

                int? totalRow = CountOrderDetailMapProductByCriteria();

                lblCountsumTotalPrice.Text = totalRow.ToString();

                decimal? TransportPrice = decimal.Parse( lOrderInfo[0].TransportPrice.ToString());
                lblTransportPrice.Text = string.Format("{0:N2}", TransportPrice);
                lblTransportPrice.Text += "฿";

                LoadsumOrderDetail(lOrderInfo[0].TransportPrice);

            }
            catch (Exception ex)
            {
            
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

        

        protected void LoadAddress()
        {
            List<CustomerAddressInfo> lOrderInfo = new List<CustomerAddressInfo>();

            lOrderInfo = GetAddressByCriteria("01");

            if(lOrderInfo.Count > 0)
            {
                lblDeliveryAddress.Text = lOrderInfo[0].Address
                                      + " "
                                      + lOrderInfo[0].SubdistrictName
                                      + " "
                                      + lOrderInfo[0].DistrictName
                                      + " "
                                      + lOrderInfo[0].ProvinceName
                                      + " "
                                      + lOrderInfo[0].ZipCode;
            }
            


            lOrderInfo = GetAddressByCriteria("02");

            if (lOrderInfo.Count > 0)
            {
                lblReceiptAddress.Text = lOrderInfo[0].Address
                                      + " "
                                      + lOrderInfo[0].SubdistrictName
                                      + " "
                                      + lOrderInfo[0].DistrictName
                                      + " "
                                      + lOrderInfo[0].ProvinceName
                                      + " "
                                      + lOrderInfo[0].ZipCode;
            }

                
        }

        public List<CustomerAddressInfo> GetAddressByCriteria(string AddressType)
        {
            List<OrderInfo> lOrderInfo = new List<OrderInfo>();

            lOrderInfo = GetOrderListByCriteria();
            List<CustomerAddressInfo> lCustomerAddressInfo = new List<CustomerAddressInfo>();
            if (lOrderInfo.Count > 0)
            {
                string respstr = "";

                
                APIpath = APIUrl + "/api/support/ListCustomerAddressDetailOrderByCriteria";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["OrderCode"] = Request.QueryString["OrderCode"],
                        ["CustomerCode"] = lOrderInfo[0].CustomerCode,
                        ["AddressType"] = AddressType
                    };

                    respstr = Encoding.UTF8.GetString(wb.UploadValues(APIpath, "POST", data));
                }

                lCustomerAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

                return lCustomerAddressInfo;
            }
            else 
            {
                return lCustomerAddressInfo;
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

            foreach (var i in lProductInfo.ToList())
            {
                if(i.ProductCode == null || i.ProductCode =="")
                {
                    i.ProductName = i.PromotionCode;
                }
            }

            return lProductInfo;

        }

        public List<OrderDetailInfo> GetsumOrderDetail()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/sumOrderDetail";

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

        protected void LoadsumOrderDetail(Decimal? TransportPrice)
        {
            List<OrderDetailInfo> lOrderDetailInfo = new List<OrderDetailInfo>();

            lOrderDetailInfo = GetsumOrderDetail();

            Decimal? sumTotalPrice = Convert.ToDecimal(lOrderDetailInfo[0].sumTotalPrice);
            lblsumTotalPrice.Text = string.Format("{0:N2}", sumTotalPrice);
            lblsumTotalPrice.Text += "฿";

            Decimal? sumVat = Convert.ToDecimal(lOrderDetailInfo[0].sumVat);
            lblsumVat.Text = string.Format("{0:N2}", sumVat);
            lblsumVat.Text += "฿";

            Decimal? sumAllPrice = TransportPrice + Convert.ToDecimal(lOrderDetailInfo[0].sumTotalPrice) + Convert.ToDecimal(lOrderDetailInfo[0].sumVat);
            lblsumAllPrice.Text = string.Format("{0:N2}", sumAllPrice);
            lblsumAllPrice.Text += "฿";
        }

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double amount = 0;
                double price = 0;
                double result = 0;
                double netprice = 0;
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lblNetPrice = (Label)e.Row.FindControl("lblNetPrice");
                amount = double.Parse(lblAmount.Text);
                price = double.Parse(lblPrice.Text);
                netprice = double.Parse(lblNetPrice.Text);
                result = price / amount;
                lblPrice.Text = result.ToString();
                lblNetPrice.Text = netprice.ToString();
                lblPrice.Text += "฿";
                lblNetPrice.Text += "฿";
                lblNetPrice.ForeColor = System.Drawing.Color.Red;
                if (lblPrice.Text == lblNetPrice.Text)
                {
                    lblPrice.Visible = false;
                    
                    lblNetPrice.Attributes.Add("style", "color:#6c757d;");
                }
            }
        }



        protected void gvOrderActivity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCreateDate = (Label)e.Row.FindControl("lblCreateDate");

                lblCreateDate.Text = DateTime.Parse(lblCreateDate.Text).ToString("dd-MM-yyyy") == "" ? lblCreateDate.Text = "-" : lblCreateDate.Text = DateTime.Parse(lblCreateDate.Text).ToString("dd-MM-yyyy HH:mm:ss");
            }
        }

        public int? CountOrderDetailMapProductByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountOrderDetailMapProductByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderCode"] = Request.QueryString["OrderCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected void LoadListOrderActivity()
        {
            List<OrderActivityInfo> lOrderActivityInfo = new List<OrderActivityInfo>();

            lOrderActivityInfo = GetListOrderActivityNopagingByCriteria();

            gvOrderActivity.DataSource = lOrderActivityInfo;

            gvOrderActivity.DataBind();
        }

        public List<OrderActivityInfo> GetListOrderActivityNopagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderActivityNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection
                {
                    ["OrderCode"] = Request.QueryString["OrderCode"]
                };

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderActivityInfo> lOrderActivityInfo = JsonConvert.DeserializeObject<List<OrderActivityInfo>>(respstr);

            return lOrderActivityInfo;

        }

        #endregion Function
    }
}
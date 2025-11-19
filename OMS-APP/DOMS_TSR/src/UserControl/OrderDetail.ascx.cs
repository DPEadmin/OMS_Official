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
using DocumentFormat.OpenXml.Drawing.Charts;

namespace DOMS_TSR.src.UserControl
{
    public partial class OrderDetail : System.Web.UI.UserControl
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

                LoadOrderMapCustomer();
                LoadOrder();
                LoadAddress();
                LoadOrderDetailMapProduct();
                LoadListOrderActivity();
                LoadOrderInventory();
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

            lOrderInfo = GetOrderListByCriteria();

            lblOrderCode.Text = lOrderInfo[0].OrderCode == "" ? lblOrderCode.Text = "-" : lblOrderCode.Text = lOrderInfo[0].OrderCode;

            lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd/MM/yyyy HH:mm") == "" ? lblCreateDate.Text = "-" : lblCreateDate.Text = DateTime.Parse(lOrderInfo[0].CreateDate).ToString("dd/MM/yyyy HH:mm");

            

            lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd/MM/yyyy") == "" ? lblDeliveryDate.Text = "-" : lblDeliveryDate.Text = DateTime.Parse(lOrderInfo[0].DeliveryDate).ToString("dd/MM/yyyy");

            lblOrderStatus.Text = lOrderInfo[0].OrderStatusName == "" ? lblOrderStatus.Text = "-" : lblOrderStatus.Text = lOrderInfo[0].OrderStatusName;
            lblOrderStatename.Text = lOrderInfo[0].OrderStateName == "" ? lblOrderStatename.Text = "-" : lblOrderStatename.Text = lOrderInfo[0].OrderStateName;

            


            lblChannel.Text = lOrderInfo[0].ChannelName == "" ? lblChannel.Text = "-" : lblChannel.Text = lOrderInfo[0].ChannelName;

            lbInventory.Text = lOrderInfo[0].InventoryName == "" ? lbInventory.Text = "-" : lbInventory.Text = lOrderInfo[0].InventoryName;

            lblOrderNote.Text = lOrderInfo[0].OrderNote == "" ? lblOrderNote.Text = "-" : lblOrderNote.Text = lOrderInfo[0].OrderNote;
            lbPay.Text = lOrderInfo[0].PaymentName == "" ? lbPay.Text = "-" : lbPay.Text = lOrderInfo[0].PaymentName;
            lbBrand.Text = lOrderInfo[0].CampaignCategoryName == "" ? lbBrand.Text = "-" : lbBrand.Text = lOrderInfo[0].CampaignCategoryName;

            lblvat.Text = lOrderInfo[0].PercentVat.ToString();
            lblvat.Text += " %:";
            int? totalRow = CountOrderDetailMapProductByCriteria();

            lblCountsumTotalPrice.Text = totalRow.ToString();

            decimal? TransportPrice = lOrderInfo[0].TransportPrice;
            lblTransportPrice.Text = string.Format("{0:N2}", TransportPrice);
            lblTransportPrice.Text += "฿";

            LoadsumOrderDetail(lOrderInfo[0].TransportPrice);
            lblOrderTrackinNo.Text = lOrderInfo[0].OrderTrackingNo.ToString();
            if (lOrderInfo[0].OrderTrackingNo != "" && lOrderInfo[0].OrderTrackingNo != null)
            {
                gettoken();
                getTokenKey(lOrderInfo[0].OrderTrackingNo.ToString());
                divgvtransport.Visible = true;
            }
            else 
            {
                divgvtransport.Visible = false;
            }
            HidtrackingtransportNo.Value = "";
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

            if (lOrderInfo.Count > 0)
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

            List<CustomerAddressInfo> lCustomerAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            return lCustomerAddressInfo;
        }

        protected void LoadOrderDetailMapProduct()
        {
            List<OrderDetailInfo> lProductInfo = new List<OrderDetailInfo>();

            lProductInfo = GetListOrderDetailMapProductAndPronotionNopagingByCriteria();
           if(lProductInfo.Count > 0) 
            {
                HidInventory.Value = lProductInfo[0].InventoryCode.ToString();
                gvProduct.DataSource = lProductInfo;

                gvProduct.DataBind();
            }
          
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
        public List<OrderDetailInfo> GetListOrderDetailMapProductAndPronotionNopagingByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderDetailMapProductAndPromotionNopagingByCriteria";

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

            double? sumTotalPrice = lOrderDetailInfo[0].sumTotalPrice;
            lblsumTotalPrice.Text = string.Format("{0:N2}", sumTotalPrice);
            lblsumTotalPrice.Text += "฿";


            double? sumVat = lOrderDetailInfo[0].sumVat;
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
                int discount = 0;
                double amount = 0;
                double price = 0;
                double result = 0;
                Label lbFlagProSetHeader = (Label)e.Row.FindControl("lbFlagProSetHeader");
                Label lblPrice = (Label)e.Row.FindControl("lblPrice");
                Label lblNetPrice = (Label)e.Row.FindControl("lblNetPrice");
                Label lblProductPrice = (Label)e.Row.FindControl("lblProductPrice");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lblTotalPrice = (Label)e.Row.FindControl("lblTotalPrice");
                Label lbplush = (Label)e.Row.FindControl("lbplush");
                Label lbbath = (Label)e.Row.FindControl("lbbath");
                HiddenField hidDiscount = (HiddenField)e.Row.FindControl("hidDiscount");
                discount = int.Parse(hidDiscount.Value);
                amount = double.Parse(lblAmount.Text);
                price = double.Parse(lblPrice.Text);
                result = price / amount;
                if (lbFlagProSetHeader.Text == "N")
                {
                    lblPrice.Text = result.ToString();
                    lblNetPrice.Text = "";
                    
                    lblTotalPrice.Text = "";
                    lbplush.Text = "";
                    lbbath.Text = "";
                }
                else
                {
                    lblPrice.Text = result.ToString();
                    
                }
                if (discount <= 0)
                {
                    lblProductPrice.Text = "";
                }



            }
        }



        protected void gvOrderActivity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCreateDate = (Label)e.Row.FindControl("lblCreateDate");

                lblCreateDate.Text = DateTime.Parse(lblCreateDate.Text).ToString("dd/MM/yyyy") == "" ? lblCreateDate.Text = "-" : lblCreateDate.Text = DateTime.Parse(lblCreateDate.Text).ToString("dd/MM/yyyy HH:mm:ss");
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

        protected void LoadOrderInventory()
        {
            if (HidInventory.Value.ToString() != "-99" && HidInventory.Value.ToString() != "" && HidInventory.Value != null)
            {
                divProductinventory.Visible = true;
                List<OrderDetailInfo> lProductInfo = new List<OrderDetailInfo>();
                lProductInfo = GetListOrderDetailMapProductNopagingByCriteria();

                if (lProductInfo.Count > 0)
                {
                    BindgvProductInventoryFromOrder(HidInventory.Value, lProductInfo);
                }
            }
            else
            {
                divProductinventory.Visible = false;


            }

        }
        protected void gvProductInventory_RowDatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBalance = (Label)e.Row.FindControl("lblBalance");
                Label lblCurrent = (Label)e.Row.FindControl("lblCurrent");

                if (Convert.ToInt32(lblBalance.Text) <= 0)
                {
                    lblBalance.ForeColor = System.Drawing.Color.Red;
                }

                if (Convert.ToInt32(lblCurrent.Text) <= 0)
                {
                    lblCurrent.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void BindgvProductInventoryFromOrder(string inventorycode, List<OrderDetailInfo> ldInfo)
        {
            InventoryInfo invdInfo = new InventoryInfo();
            List<InventoryDetailInfo> linvdInfo = new List<InventoryDetailInfo>();

            invdInfo.InventoryCode = inventorycode;
            linvdInfo = GetInventoryBalance(invdInfo);

            InventoryDetailInfoNew ivd = new InventoryDetailInfoNew();
            List<InventoryDetailInfoNew> lidvInfo = new List<InventoryDetailInfoNew>();

            foreach (var od in ldInfo.ToList()) // ProductCode ของ Order ที่อยู่ใน InventoryDetail
            {
                if (ldInfo.Count > 0)
                {
                    foreach (var ov in linvdInfo)
                    {
                        if (od.ProductCode == ov.ProductCode)
                        {
                            ivd = new InventoryDetailInfoNew();

                            ivd.ProductCode = od.ProductCode;
                            ivd.ProductName = od.ProductName;
                            ivd.Amount = od.Amount; // จำนวน
                            ivd.QTY = ov.QTY;
                            ivd.Reserved = ov.Reserved;
                            ivd.Current = ov.Current;
                            ivd.Balance = ov.Balance;
                            
                            lidvInfo.Add(ivd);

                            ldInfo.RemoveAll(s => s.ProductCode == od.ProductCode && s.PromotionCode == od.PromotionCode);
                        }
                    }
                }
            }

            if (ldInfo.Count > 0)
            {
                foreach (var og in ldInfo)
                {
                    ivd = new InventoryDetailInfoNew();

                    ivd.ProductCode = og.ProductCode;
                    ivd.ProductName = og.ProductName;
                    ivd.Amount = og.Amount; // จำนวน
                    ivd.QTY = 0;
                    ivd.Reserved = 0;
                    ivd.Current = 0;
                    ivd.Balance = 0;
                    

                    lidvInfo.Add(ivd);
                }
            }

            var ProductInvenList = lidvInfo.GroupBy(e => e.ProductCode).Select(g =>
            {
                var item = g.First();
                return new InventoryDetailInfoNew
                {
                    Amount = g.Sum(e => e.Amount),

                    ProductCode = item.ProductCode,
                    ProductName = item.ProductName,
                    QTY = item.QTY,
                    Reserved = item.Reserved,
                    Current = item.Current,
                    Balance = item.Balance
                };
            }).ToList();

            gvProductInventory.DataSource = ProductInvenList;
            gvProductInventory.DataBind();
        }
        protected List<InventoryDetailInfo> GetInventoryBalance(InventoryInfo invInfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = invInfo.InventoryCode;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfo> linventorydetailInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfo>>(respstr);
            return linventorydetailInfo;
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
                HidToken.Value = datalist.token.ToString();

            }
        }
        protected void getTokenKey(string trackNo)
        {
            LThaiPostInfo mon = new LThaiPostInfo();
            List<LThaiPostInfo> lThaiPostInfo = new List<LThaiPostInfo>();

            string APIUrl = "https://trackapi.thailandpost.co.th/post/api/v1/track";
        
           
            var code = trackNo.TrimStart('[').TrimEnd(']').Split(',');
            using (var client = new WebClient())
            {

                mon.status = "all";
                mon.language = "TH";
                mon.barcode = code;

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.status,
                    mon.language,
                    mon.barcode,
                });

                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Headers[HttpRequestHeader.Authorization] = "Token " + HidToken.Value.ToString();

                var dataString = client.UploadString(APIUrl, jsonObj);


                thaipost datalist = JsonConvert.DeserializeObject<thaipost>(dataString.ToString());
                GVStatusTransport.DataSource = datalist.response.items.EG327058400TH;
               
                GVStatusTransport.DataBind();

            }

        }

        protected void getDtetailtransport() 
        {
        
        }
        #endregion
    }
  
}
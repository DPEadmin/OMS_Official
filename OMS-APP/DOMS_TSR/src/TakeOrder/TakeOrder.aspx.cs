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

namespace DOMS_TSR.src.TakeOrderTSR
{
    public class OrderData
    {
        public string CampaignCategory { get; set; }
        public string CampaignCategoryName { get; set; }
        public string PromotionCode { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ParentProductCode { get; set; }
        public string ComboName { get; set; }
        public string ComboCode { get; set; }
        public string ProductOrderType { get; set; }
        public Double? Price { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public int? Amount { get; set; }
        public int? DefaultAmount { get; set; }
        public int? PromotionDetailId { get; set; }
        public Double? SumPrice { get; set; }

        public int? DiscountAmount { get; set; }
        public int? DiscountPercent { get; set; }
        public string OrderCode { get; set; }
        public string CustomerCode { get; set; }
        public string OrderStatusCode { get; set; }
        public string OrderStateCode { get; set; }
        public string BUCode { get; set; }
        public string FlagCombo { get; set; }
        public Double? NetPrice { get; set; }
        public Double? Vat { get; set; }
        public string UpdateBy { get; set; }
        public int? runningNo { get; set; }
        public Double TransportPrice { get; set; }
        public String BranchCode { get; set; }
        public string CampaignCode { get; set; }

    }

    public class InventoryData
    {
        public string InventoryCode { get; set; }
        public string ProductCode { get; set; }
        public int? Qty { get; set; }
        public int? Reserved { get; set; }
        public int? Reserving { get; set; }
        public int? Balance { get; set; }
    }

    public partial class TakeOrder : System.Web.UI.Page
    {

        string Codelist = "";
        string EditFlag = "";
        protected static int currentPageNumber;
        protected static int currentPageNumberProduct;
        protected static int currentPageNumberProductPopup;

        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        decimal? total = 0;
        string CustomerCode = "";
        string CustomerPhone = "";
        string APIpath = "";
        protected List<OrderData> L_orderdata
        {
            get
            {
                if (Session["l_orderdata"] == null)
                {
                    return new List<OrderData>();
                }
                else
                {
                    return (List<OrderData>)Session["l_orderdata"];
                }
            }
            set
            {
                Session["l_orderdata"] = value;
            }
        }
        protected List<transportdataInfo> L_transportdata
        {
            get
            {
                if (Session["L_transportdata"] == null)
                {
                    return new List<transportdataInfo>();
                }
                else
                {
                    return (List<transportdataInfo>)Session["L_transportdata"];
                }
            }
            set
            {
                Session["L_transportdata"] = value;
            }
        }
        protected List<InventoryData> L_inventorydata
        {
            get
            {
                if (Session["L_inventorydata"] == null)
                {
                    return new List<InventoryData>();
                }
                else
                {
                    return (List<InventoryData>)Session["L_inventorydata"];
                }
            }
            set
            {
                Session["L_inventorydata"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerCode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
            CustomerPhone = (Request.QueryString["CustomerPhone"] != null) ? Request.QueryString["CustomerPhone"].ToString() : "";
            Session["CustomerCode"] = CustomerCode;
            lblCustomerCode.Text = CustomerCode;

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumberProduct = 1;
                currentPageNumberProductPopup = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\Default.aspx?flaglogin=_EMPCODENULL");
                }



                

                L_orderdata = new List<OrderData>();
                L_transportdata = new List<transportdataInfo>();

                BindCampaign();
                BindCustomerLabel(CustomerCode);
                BindTop5Product(CustomerCode);
                BindCustomerAddressDelivery(CustomerCode);
                BindCustomerAddressReceipt(CustomerCode);
                BindddlProvince();

                InsertSectionDelivery.Visible = false;
                InsertCustomerAddressReceiptSection.Visible = false;
                

            }
        }

        #region Function
        public CustomerAddressInfo SetFormAddressPopup()
        {
            CustomerCode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";

            CustomerAddressInfo pinfo = new CustomerAddressInfo();

            pinfo.CustomerCode = (CustomerCode == "") ? "-99" : CustomerCode;
            pinfo.AddressType = StaticField.AddressTypeCode01; 

            return pinfo;
        }
        public CustomerAddressInfo SetFormAddressPopup2()
        {
            CustomerCode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";

            CustomerAddressInfo pinfo = new CustomerAddressInfo();

            pinfo.CustomerCode = (CustomerCode == "") ? "-99" : CustomerCode;
            pinfo.AddressType = StaticField.AddressTypeCode02; 

            return pinfo;
        }
        public List<CustomerAddressInfo> ListAddressByCriteria(CustomerAddressInfo pinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerAddressListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = pinfo.CustomerCode;
                data["AddressType"] = pinfo.AddressType;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCustomerAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);


            return lCustomerAddressInfo;

        }
        public ProductInfo SetFormProductPopup()
        {
            ProductInfo pinfo = new ProductInfo();

            pinfo.PromotionCode = lbPromotion.SelectedValue;
            pinfo.CampaignCode = lbCampaign.SelectedValue;

            return pinfo;
        }
        public List<ProductInfo> ListProductByCriteria(ProductInfo pinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = pinfo.PromotionCode;

                data["CampaignCode"] = pinfo.CampaignCode;

                data["rowOFFSet"] = ((currentPageNumberProductPopup - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }
        protected OrderData bindorderdata(OrderData item)
        {
            OrderData odata = new OrderData();
            
            odata.CampaignCode = item.CampaignCode;
            odata.PromotionCode = item.PromotionCode;
            odata.PromotionDetailId = item.PromotionDetailId;
            odata.ProductCode = item.ProductCode;
            odata.ProductName = item.ProductName;
            odata.UnitCode = item.UnitCode;
            odata.UnitName = item.UnitName;
            odata.DiscountAmount = item.DiscountAmount;
            odata.DiscountPercent = item.DiscountPercent;
            odata.Price = item.Price;
            
            odata.CustomerCode = item.CustomerCode;
            
            odata.runningNo = item.runningNo;

            return odata;
        }
        public int? CountProductListByCriteria(ProductInfo pinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = pinfo.PromotionCode;

                data["CampaignCode"] = pinfo.CampaignCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }


        private void setDDl(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

        
        protected void LoadAddressPopup(CustomerAddressInfo pinfo)
        {
            List<CustomerAddressInfo> lCustomerAddressInfo = new List<CustomerAddressInfo>();
            
            lCustomerAddressInfo = ListAddressByCriteria(pinfo);
                
            gvPopupCustomerAddress.DataSource = lCustomerAddressInfo;
            gvPopupCustomerAddress.DataBind();
        }

        protected void LoadAddressPopupReceipt(CustomerAddressInfo pinfo)
        {
            List<CustomerAddressInfo> lCustomerAddressInfo = new List<CustomerAddressInfo>();
            
            lCustomerAddressInfo = ListAddressByCriteria(pinfo);
                 
            gvCustomerAddressReceipt.DataSource = lCustomerAddressInfo;
            gvCustomerAddressReceipt.DataBind();
        }

        protected void LoadProductPopup(ProductInfo pinfo)
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            

            lProductInfo = ListProductByCriteria(pinfo);


            
            Session["LProductInfo"] = lProductInfo;
            gvProductPopup.DataSource = lProductInfo;
            gvProductPopup.DataBind();


        }
        protected string GetPrice(object objPrice, object objDiscountAmount, object objDiscountPercent)
        {
            decimal? strPrice = (objPrice != null) ? Convert.ToDecimal(objPrice) : 0;
            decimal? strDiscountAmount = (objDiscountAmount != null) ? Convert.ToDecimal(objDiscountAmount) : 0;
            decimal? strDiscountPercent = (objDiscountPercent != null) ? Convert.ToDecimal(objDiscountPercent) : 0;

            decimal? sumprice = strPrice - strDiscountAmount - ((strPrice * strDiscountPercent) / 100);

            return sumprice.ToString();
        }
        protected string GetTextPrice(object objParentProductCode, object objPrice, object objDiscountAmount, object objDiscountPercent)
        {
            string strParentProductCode = (objParentProductCode != null) ? objParentProductCode.ToString() : "";

            decimal? strPrice = (objPrice != null) ? Convert.ToDecimal(objPrice) : 0;
            decimal? strDiscountAmount = (objDiscountAmount != null) ? Convert.ToDecimal(objDiscountAmount) : 0;
            decimal? strDiscountPercent = (objDiscountPercent != null) ? Convert.ToDecimal(objDiscountPercent) : 0;

            decimal? sumprice = strPrice - strDiscountAmount - ((strPrice * strDiscountPercent) / 100);

            string strret = "";

            if (strParentProductCode == "")
            {
                if (strPrice > sumprice)
                {
                    strret = @"  <span style = ""  text-decoration: line-through;"">" + string.Format("{0:#.00}", strPrice) + "</span>  " +
                             "  &nbsp;" + string.Format("{0:n}", sumprice);
                }
                else
                {
                    strret = string.Format("{0:n}", strPrice);

                }
            }

            return strret;
        }
        protected string GetTextPriceProductPopup(object objPrice, object objDiscountAmount, object objDiscountPercent)
        {

            decimal? strPrice = (objPrice != null) ? Convert.ToDecimal(objPrice) : 0;
            decimal? strDiscountAmount = (objDiscountAmount != null) ? Convert.ToDecimal(objDiscountAmount) : 0;
            decimal? strDiscountPercent = (objDiscountPercent != null) ? Convert.ToDecimal(objDiscountPercent) : 0;

            decimal? sumprice = strPrice - strDiscountAmount - ((strPrice * strDiscountPercent) / 100);

            string strret = "";


            if (strPrice > sumprice)
            {
                strret = @"  <span style = ""  text-decoration: line-through;"">" + string.Format("{0:#.00}", strPrice) + "</span>  " +
                         "  &nbsp;" + string.Format("{0:n}", sumprice);
            }
            else
            {
                strret = string.Format("{0:n}", strPrice);

            }


            return strret;
        }
        protected List<ProductInfo> LoadOrderdataProductPopup(List<ProductInfo> LProductInfo, TextBox txtAmount, Label lbltotal, HiddenField hidPromotiondetailId)
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            foreach (var item in LProductInfo)
            {
                ProductInfo odata = new ProductInfo();

                odata.PromotionDetailId = Convert.ToInt32(item.PromotionDetailId.Value);

                if (odata.PromotionDetailId == Convert.ToInt32(hidPromotiondetailId.Value))
                {
                    odata.Amount = Convert.ToInt32(txtAmount.Text);
                }
                else
                {
                    odata.Amount = item.Amount;
                }

                odata.PromotionDetailId = item.PromotionDetailId;
                odata.CampaignCode = item.CampaignCode;
                odata.PromotionCode = item.PromotionCode;
                odata.ProductCode = item.ProductCode;
                odata.Unit = item.Unit;
                odata.UnitName = item.UnitName;

                odata.ProductName = item.ProductName;
                odata.DiscountAmount = (item.DiscountAmount != null) ? Convert.ToInt32(item.DiscountAmount) : 0;
                odata.DiscountPercent = (item.DiscountPercent != null) ? Convert.ToInt32(item.DiscountPercent) : 0;
                odata.Price = (item.Price != null) ? Convert.ToDouble(item.Price) : -99;
                
                lProductInfo.Add(odata);
            }

            return lProductInfo;
        }

        protected List<OrderData> LoadOrderdata(List<OrderData> Lorderdata, TextBox txtAmount, Label lbltotal, HiddenField hidRunning)
        {
            List<OrderData> lorderdata = new List<OrderData>();

            foreach (var item in Lorderdata)
            {
                OrderData odata = new OrderData();

                odata.runningNo = Convert.ToInt32(item.runningNo.Value);

                if (odata.runningNo == Convert.ToInt32(hidRunning.Value))
                {
                    odata.Amount = Convert.ToInt32(txtAmount.Text);
                }
                else
                {
                    odata.Amount = Convert.ToInt32(item.Amount);
                }
                odata.CampaignCategory = item.CampaignCategory;
                odata.CampaignCategoryName = item.CampaignCategoryName;
                odata.PromotionDetailId = item.PromotionDetailId;
                odata.CampaignCode = item.CampaignCode;
                odata.PromotionCode = item.PromotionCode;
                odata.ProductCode = item.ProductCode;
                odata.FlagCombo = item.FlagCombo;
                odata.ParentProductCode = item.ParentProductCode;
                odata.ComboCode = item.ComboCode;
                odata.ComboName = item.ComboName;
                odata.ProductName = item.ProductName;
                odata.DiscountAmount = (item.DiscountAmount != null) ? Convert.ToInt32(item.DiscountAmount) : 0;
                odata.DiscountPercent = (item.DiscountPercent != null) ? Convert.ToInt32(item.DiscountPercent) : 0;
                odata.Price = (item.Price != null) ? Convert.ToDouble(item.Price) : -99;
                odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;
                lbltotal.Text = string.Format("{0:n}", (Convert.ToDecimal(odata.SumPrice) * odata.Amount));
                lorderdata.Add(odata);
            }

            return lorderdata;
        }

        protected Boolean ValidateCustomerPhone()
        {
            Boolean flag = true;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ValidateCustomerPhone";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = CustomerCode;
                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerPhoneInfo> lCustomerPhoneInfo = JsonConvert.DeserializeObject<List<CustomerPhoneInfo>>(respstr);

            if (lCustomerPhoneInfo.Count > 0)
            {
                flag = false;
            }
            else
            {
                flag = true;
            }

            return flag;
        }

        protected void BindTop5Product(String customercode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListTop5ProductodOrderCustomerByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CustomerCode"] = customercode;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lTop5ProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            if (lTop5ProductInfo.Count > 0)
            {
                if (lTop5ProductInfo[0].ProductName != null)
                {
                    ProductNameTop1.Text = lTop5ProductInfo[0].ProductName;
                }
                else
                {
                    ProductNameTop1.Text = "";
                }
                if (lTop5ProductInfo[1].ProductName != null)
                {
                    ProductNameTop2.Text = lTop5ProductInfo[1].ProductName;
                }
                else
                {
                    ProductNameTop2.Text = "";
                }
                if (lTop5ProductInfo[2].ProductName != null)
                {
                    ProductNameTop3.Text = lTop5ProductInfo[2].ProductName;
                }
                else
                {
                    ProductNameTop3.Text = "";
                }
                if (lTop5ProductInfo[3].ProductName != null)
                {
                    ProductNameTop4.Text = lTop5ProductInfo[3].ProductName;
                }
                else
                {
                    ProductNameTop4.Text = "";
                }
                if (lTop5ProductInfo[4].ProductName != null)
                {
                    ProductNameTop5.Text = lTop5ProductInfo[4].ProductName;
                }
                else
                {
                    ProductNameTop5.Text = "";
                }
            }

        }

        protected List<CustomerAddressInfo> BindCustomerAddressDelivery(String customercode)
        {
            List<CustomerAddressInfo> lempInfo = new List<CustomerAddressInfo>();

            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLatestUpdatedCustomerAddress";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = customercode;
                data["AddressType"] = StaticField.AddressTypeCode01; 

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCusAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCusAddressInfo.Count > 0)
            {
                hidCustomerAddressIdShow01.Value = lCusAddressInfo[0].CustomerAddressId.ToString();
                lblAddress01.Text = lCusAddressInfo[0].Address;
                lblSubDistrictName01.Text = lCusAddressInfo[0].SubdistrictName;
                lblDistrictName01.Text = lCusAddressInfo[0].DistrictName;
                lblProvinceName01.Text = lCusAddressInfo[0].ProvinceName;
                lblZipCode01.Text = lCusAddressInfo[0].ZipCode;
                hidSubDistrictCodeShow01.Value = lCusAddressInfo[0].Subdistrict;
                hidDistrictCodeShow01.Value = lCusAddressInfo[0].District;
                hidProvinceCodeShow01.Value = lCusAddressInfo[0].Province;
            }

            return lCusAddressInfo;
        }

        protected List<CustomerAddressInfo> BindCustomerAddressReceipt(String customercode)
        {
            List<CustomerAddressInfo> lempInfo = new List<CustomerAddressInfo>();

            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLatestUpdatedCustomerAddress";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = customercode;
                data["AddressType"] = StaticField.AddressTypeCode02; 

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCusAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCusAddressInfo.Count > 0)
            {
                lblAddress02.Text = lCusAddressInfo[0].Address;
                lblSubDistrictName02.Text = lCusAddressInfo[0].SubdistrictName;
                lblDistrictName02.Text = lCusAddressInfo[0].DistrictName;
                lblProvinceName02.Text = lCusAddressInfo[0].ProvinceName;
                lblZipCode02.Text = lCusAddressInfo[0].ZipCode;
                hidSubDistrictCodeShow02.Value = lCusAddressInfo[0].Subdistrict;
                hidDistrictCodeShow02.Value = lCusAddressInfo[0].District;
                hidProvinceCodeShow02.Value = lCusAddressInfo[0].Province;
            }

            return lCusAddressInfo;
        }

        protected Boolean ValidateCustomerAddressReceiptInsert()
        {
            Boolean flag = true;

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListAddressValidate";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = CustomerCode;
                data["Address"] = lblAddress01.Text;
                data["Subdistrict"] = hidSubDistrictCodeShow01.Value;
                data["District"] = hidDistrictCodeShow01.Value;
                data["Province"] = hidProvinceCodeShow01.Value;
                data["ZipCode"] = lblZipCode01.Text;
                data["AddressType"] = StaticField.AddressTypeCode02; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerAddressInfo> lCustomerAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCustomerAddressInfo.Count > 0)
            {
                flag = false;
            }
            else
            {
                flag = true;
            }

            return flag;
        }

        protected Boolean ValidateCustomerAddressDeliveryInsert()
        {
            Boolean flag = true;

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListAddressValidate";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = CustomerCode;
                data["Address"] = txtAddress_Ins.Text;
                data["Subdistrict"] = ddlSubDistrict.SelectedValue;
                data["District"] = ddlDistrict.SelectedValue;
                data["Province"] = ddlProvince.SelectedValue;
                data["ZipCode"] = txtPostcode_Ins.Text;
                data["AddressType"] = StaticField.AddressTypeCode01; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerAddressInfo> lCustomerAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCustomerAddressInfo.Count > 0)
            {
                flag = false;
                hidCustomerAddressDeliveryIdUpd.Value = lCustomerAddressInfo[0].CustomerAddressId.ToString();
            }
            else
            {
                flag = true;
            }

            return flag;
        }

        protected List<CustomerAddressInfo> GetCustomerIdfromAddressReceipt(String customercode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLatestUpdatedCustomerAddress";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = CustomerCode;
                data["Address"] = lblAddress01.Text;
                data["Subdistrict"] = hidSubDistrictCodeShow01.Value;
                data["District"] = hidDistrictCodeShow01.Value;
                data["Province"] = hidProvinceCodeShow01.Value;
                data["ZipCode"] = lblZipCode01.Text;
                data["AddressType"] = StaticField.AddressTypeCode02; 
                data["FlagActive"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCusAddressInfo = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);
            return lCusAddressInfo;
        }

        protected void BindddlProvince()
        {
            List<ProvinceInfo> lProvinceInfo = new List<ProvinceInfo>();

            lProvinceInfo = ListProvinceMasterInfo();

            ddlProvince.DataSource = lProvinceInfo;
            ddlProvince.DataValueField = "ProvinceCode";
            ddlProvince.DataTextField = "ProvinceName";
            ddlProvince.DataBind();

            ddlCusProvinceReceiptIns.DataSource = lProvinceInfo;
            ddlCusProvinceReceiptIns.DataValueField = "ProvinceCode";
            ddlCusProvinceReceiptIns.DataTextField = "ProvinceName";
            ddlCusProvinceReceiptIns.DataBind();

            ddlProvince.Items.Insert(0, new ListItem("กรุณาเลือกจังหวัด", "-99"));
            ddlCusProvinceReceiptIns.Items.Insert(0, new ListItem("กรุณาเลือกจังหวัด", "-99"));
        }

        public List<ProvinceInfo> ListProvinceMasterInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);

            return lProvinceInfo;
        }

        protected void BindddlSubDistrict(string DistrictCode)
        {
            List<SubDistrictInfo> lSubDistrictInfo = new List<SubDistrictInfo>();

            lSubDistrictInfo = ListSubDistrictMasterInfo(DistrictCode);

            ddlSubDistrict.DataSource = lSubDistrictInfo;
            ddlSubDistrict.DataValueField = "SubDistrictCode";
            ddlSubDistrict.DataTextField = "SubDistrictName";
            ddlSubDistrict.DataBind();

            ddlCusSubdistrictReceiptIns.DataSource = lSubDistrictInfo;
            ddlCusSubdistrictReceiptIns.DataValueField = "SubDistrictCode";
            ddlCusSubdistrictReceiptIns.DataTextField = "SubDistrictName";
            ddlCusSubdistrictReceiptIns.DataBind();

            ddlSubDistrict.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
            ddlCusSubdistrictReceiptIns.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlDistrict(string ProvinceCode, DropDownList ddl)
        {
            List<DistrictInfo> lDistrictInfo = new List<DistrictInfo>();

            lDistrictInfo = ListDistrictMasterInfo(ProvinceCode);

            ddl.DataSource = lDistrictInfo;
            ddl.DataValueField = "DistrictCode";
            ddl.DataTextField = "DistrictName";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        public List<SubDistrictInfo> ListSubDistrictMasterInfo(string DistrictCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["DistrictCode"] = DistrictCode;
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);

            return lSubDistrictInfo;
        }

        public List<DistrictInfo> ListDistrictMasterInfo(string ProvinceCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ProvinceCode"] = ProvinceCode;
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);

            return lDistrictInfo;
        }
        #endregion

        #region Event 

        protected void gvPopupCustomerAddress_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvPopupCustomerAddress_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPopupCustomerAddress.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCustomerAddressId = (HiddenField)row.FindControl("hidCustomerAddressId");
            HiddenField hidCustomerCode = (HiddenField)row.FindControl("hidCustomerCode");
            HiddenField hidAddress = (HiddenField)row.FindControl("hidAddress");
            HiddenField hidSubdistrict = (HiddenField)row.FindControl("hidSubdistrict");
            HiddenField hidDistrict = (HiddenField)row.FindControl("hidDistrict");
            HiddenField hidProvince = (HiddenField)row.FindControl("hidProvince");
            HiddenField hidZipCode = (HiddenField)row.FindControl("hidZipCode");

            if (e.CommandName == "ShowCustomerAddressDelivery")
            {
                txtAddress_Ins.Text = hidAddress.Value;
                String a = "";
                BindddlSubDistrict(a);
                ddlSubDistrict.SelectedValue = hidSubdistrict.Value;
                ddlDistrict.SelectedValue = hidDistrict.Value;
                BindddlDistrict(a, ddlDistrict);
                ddlProvince.SelectedValue = hidProvince.Value;                
                txtPostcode_Ins.Text = hidZipCode.Value;
                hidCustomerAddressDeliveryIdUpd.Value = hidCustomerAddressId.Value;

                hidFlagInsert.Value = "False";
                InsertSectionDelivery.Visible = true;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address').modal();", true);
            }
        }

        protected void gvCustomerAddressReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvCustomerAddressReceipt.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCustomerAddressReceiptId = (HiddenField)row.FindControl("hidCustomerAddressReceiptId");
            HiddenField hidCustomerReceiptCode = (HiddenField)row.FindControl("hidCustomerReceiptCode");
            HiddenField hidAddressReceipt = (HiddenField)row.FindControl("hidAddressReceipt");
            HiddenField hidSubdistrictReceipt = (HiddenField)row.FindControl("hidSubdistrictReceipt");
            HiddenField hidDistrictReceipt = (HiddenField)row.FindControl("hidDistrictReceipt");
            HiddenField hidProvinceReceipt = (HiddenField)row.FindControl("hidProvinceReceipt");
            HiddenField hidZipCodeReceipt = (HiddenField)row.FindControl("hidZipCodeReceipt");

            if (e.CommandName == "ShowCustomerAddressReceipt")
            {
                txtCusAddressReceiptIns.Text = hidAddressReceipt.Value;
                String a = "";
                BindddlSubDistrict(a);
                ddlCusSubdistrictReceiptIns.SelectedValue = hidSubdistrictReceipt.Value;
                ddlCusDistrictReceiptIns.SelectedValue = hidDistrictReceipt.Value;
                BindddlDistrict(a, ddlCusDistrictReceiptIns);
                ddlCusProvinceReceiptIns.SelectedValue = hidProvinceReceipt.Value;
                txtCusPostCodeReceiptIns.Text = hidZipCodeReceipt.Value;
                hidCustomerAddressReceiptIdUpd.Value = hidCustomerAddressReceiptId.Value;

                hidFlagInsert.Value = "False";
                InsertCustomerAddressReceiptSection.Visible = true;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address2').modal();", true);
            }
        }

        protected void btnProductPopupClose_Click(object sender, EventArgs e)
        {

        }
        protected void txtAmountProductPopup_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;

            TextBox txtAmount = (TextBox)currentRow.FindControl("txtAmount");
            Label lbltotal = (Label)currentRow.FindControl("lbltotal");
            HiddenField hidPromotionDetailId = (HiddenField)currentRow.FindControl("hidPromotionDetailId");

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            List<ProductInfo> lProductInforeturn = new List<ProductInfo>();

            lProductInfo = (List<ProductInfo>)Session["LProductInfo"];

            lProductInforeturn = LoadOrderdataProductPopup(lProductInfo, txtAmount, lbltotal, hidPromotionDetailId);

            Session["LProductInfo"] = lProductInforeturn;

            gvProductPopup.DataSource = lProductInforeturn;
            gvProductPopup.DataBind();
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;

            TextBox txtAmount = (TextBox)currentRow.FindControl("txtAmount");
            Label lbltotal = (Label)currentRow.FindControl("lbltotal");
            HiddenField hidRunning = (HiddenField)currentRow.FindControl("hidRunning");

            List<OrderData> lorderdata = new List<OrderData>();



            lorderdata = LoadOrderdata(L_orderdata, txtAmount, lbltotal, hidRunning);

            L_orderdata = lorderdata;
            LoadgvOrder(L_orderdata);


            
        }
        protected void gvOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                HiddenField hidSumprice = (HiddenField)e.Row.FindControl("hidSumprice");
                Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                HiddenField hidParentProductCode = (HiddenField)e.Row.FindControl("hidParentProductCode");
                HiddenField hidFlagCombo = (HiddenField)e.Row.FindControl("hidFlagCombo");
                LinkButton btnClose = (LinkButton)e.Row.FindControl("btnClose");
                CheckBox chkOrder = (CheckBox)e.Row.FindControl("chkOrder");

                decimal? Sumprice = Decimal.Parse(hidSumprice.Value);

                lbltotal.Text = string.Format("{0:n}", (Sumprice * Convert.ToDecimal(txtAmount.Text)));

                if (hidFlagCombo.Value != "")
                {
                    if (hidParentProductCode.Value != "")
                    {
                        btnClose.Visible = false;
                        txtAmount.Visible = false;
                        chkOrder.Visible = false;
                        lbltotal.Visible = false;
                    }
                    else
                    {
                        txtAmount.Enabled = false;
                    }

                }


            }
        }
        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void chkOrderAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder.HeaderRow.FindControl("chkOrderAll");

                if (chkall.Checked == true)
                {

                    CheckBox chkOrder = (CheckBox)gvOrder.Rows[i].FindControl("chkOrder");

                    chkOrder.Checked = true;
                }
                else
                {

                    CheckBox chkOrder = (CheckBox)gvOrder.Rows[i].FindControl("chkOrder");

                    chkOrder.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }
        protected void chkPopupCustomerAddressAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPopupCustomerAddress.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvPopupCustomerAddress.HeaderRow.FindControl("chkPopupCustomerAddressAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidProductId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProductPopup = (CheckBox)gvProductPopup.Rows[i].FindControl("chkProductPopup");

                    chkProductPopup.Checked = true;
                }
                else
                {

                    CheckBox chkProductPopup = (CheckBox)gvProductPopup.Rows[i].FindControl("chkProductPopup");

                    chkProductPopup.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void chkProductPopupAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvProductPopup.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvProductPopup.HeaderRow.FindControl("chkProductPopupAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProductPopup.Rows[i].FindControl("hidProductId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProductPopup = (CheckBox)gvProductPopup.Rows[i].FindControl("chkProductPopup");

                    chkProductPopup.Checked = true;
                }
                else
                {

                    CheckBox chkProductPopup = (CheckBox)gvProductPopup.Rows[i].FindControl("chkProductPopup");

                    chkProductPopup.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        

        protected void gvProductPopup_Change(object sender, GridViewPageEventArgs e)
        {
            gvProductPopup.PageIndex = e.NewPageIndex;

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            lProductInfo = ListProductByCriteria(SetFormProductPopup());

            gvProductPopup.DataSource = lProductInfo;

            gvProductPopup.DataBind();

        }

        

        protected void lbCampaign_SelectedIndexChanged(object sender, EventArgs e)
        {
            PromotionInfo pinfo = new PromotionInfo();

            pinfo.CampaignCode = lbCampaign.SelectedValue;

            hidCampaigncode.Value = lbCampaign.SelectedValue;

            BindPromotion(pinfo);
        }

        protected void lbPromotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            hidPromotioncode.Value = lbPromotion.SelectedValue;
            hidCampaigncode.Value = lbCampaign.SelectedValue;


            LoadProductPopup(SetFormProductPopup());
        }

        protected void btneditaddress1_Click(object sender, EventArgs e)
        {
            LoadAddressPopup(SetFormAddressPopup());

            hidAddressType.Value = StaticField.AddressTypeCode01; 
            InsertSectionDelivery.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address').modal();", true);
        }

        protected void btneditaddress2_Click(object sender, EventArgs e)
        {
            LoadAddressPopupReceipt(SetFormAddressPopup2());

            hidAddressType.Value = StaticField.AddressTypeCode02; 
            InsertCustomerAddressReceiptSection.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address2').modal();", true);

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);

        }

        protected void gvProductPopup_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvProductPopup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                HiddenField hidSumprice = (HiddenField)e.Row.FindControl("hidSumprice");
                Label lbltotal = (Label)e.Row.FindControl("lbltotal");
                LinkButton btnClose = (LinkButton)e.Row.FindControl("btnClose");
                CheckBox chkOrder = (CheckBox)e.Row.FindControl("chkOrder");
                HiddenField hidPromotionDetailId = (HiddenField)e.Row.FindControl("hidPromotionDetailId");
                HiddenField hidAmount = (HiddenField)e.Row.FindControl("hidAmount");

                decimal? Sumprice = Decimal.Parse(hidSumprice.Value);

                txtAmount.Text = (txtAmount.Text == "") ? "1" : txtAmount.Text;
                hidAmount.Value = txtAmount.Text;
                lbltotal.Text = string.Format("{0:n}", (Sumprice * Convert.ToDecimal(txtAmount.Text)));


            }

        }

        protected void btnProductPopupSubmit_Click(object sender, EventArgs e)
        {

            List<OrderData> ldata = new List<OrderData>();

            ldata = L_orderdata;

            foreach (GridViewRow row in gvProductPopup.Rows)
            {
                Label lblProductCode = (Label)row.FindControl("lblProductCode");
                Label lblProductName = (Label)row.FindControl("lblProductName");
                HiddenField hidPrice = (HiddenField)row.FindControl("hidPrice");
                Label lblUNIT = (Label)row.FindControl("lblUNIT");
                
                HiddenField hidLockAmountFlag = (HiddenField)row.FindControl("hidLockAmountFlag");
                HiddenField hidDefaultAmount = (HiddenField)row.FindControl("hidDefaultAmount");
                HiddenField hidPromotionDetailId = (HiddenField)row.FindControl("hidPromotionDetailId");
                HiddenField hidDiscountAmount = (HiddenField)row.FindControl("hidDiscountAmount");
                HiddenField hidDiscountPercent = (HiddenField)row.FindControl("hidDiscountPercent");
                HiddenField hidUnitCode = (HiddenField)row.FindControl("hidUnitCode");
                HiddenField hidUnitName = (HiddenField)row.FindControl("hidUnitName");
                HiddenField hidCampaignCode = (HiddenField)row.FindControl("hidCampaignCode");
                HiddenField hidPromotionCode = (HiddenField)row.FindControl("hidPromotionCode");


                TextBox txtAmount = (TextBox)row.FindControl("txtAmount");

                CheckBox chkProductPopup = (CheckBox)row.FindControl("chkProductPopup");


                List<OrderData> lorderdatacheck = new List<OrderData>();


                List<OrderData> lneworderdata = new List<OrderData>();

                string flagDuplicate = "N";

                if (chkProductPopup.Checked == true)
                {
                    if ((txtAmount.Text != "") && (txtAmount.Text != "0"))
                    {

                        lorderdatacheck = L_orderdata;

                        OrderData odata;

                        foreach (var item in lorderdatacheck)
                        {
                            odata = new OrderData();

                            odata = bindorderdata(item);

                            if ((item.PromotionCode == hidPromotionCode.Value) && (item.CampaignCode == hidCampaignCode.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailId.Value))
                            {

                                odata.Amount = item.Amount + Convert.ToInt32(txtAmount.Text);
                                odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;


                                flagDuplicate = "Y";
                            }
                            else
                            {
                                odata.Amount = item.Amount;
                                odata.SumPrice = odata.SumPrice;
                            }

                            lneworderdata.Add(odata);

                        }


                        L_orderdata = lneworderdata;

                        ldata = L_orderdata;

                        if (flagDuplicate == "N")
                        {
                            odata = new OrderData();

                            odata.ProductCode = lblProductCode.Text;
                            odata.ProductName = lblProductName.Text;
                            odata.Price = (hidPrice.Value != "") ? Convert.ToDouble(hidPrice.Value) : -99;
                            odata.Amount = Convert.ToInt32(txtAmount.Text);
                            odata.DefaultAmount = (hidDefaultAmount.Value != "") ? Convert.ToInt32(hidDefaultAmount.Value) : -99;
                            odata.DiscountAmount = (hidDiscountAmount.Value != "") ? Convert.ToInt32(hidDiscountAmount.Value) : 0;
                            odata.DiscountPercent = (hidDiscountPercent.Value != "") ? Convert.ToInt32(hidDiscountPercent.Value) : 0;
                            odata.PromotionCode = hidPromotioncode.Value;
                            

                            odata.UnitCode = hidUnitCode.Value;
                            odata.UnitName = hidUnitName.Value;
                            odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;

                            odata.PromotionDetailId = (hidPromotionDetailId.Value != "") ? Convert.ToInt32(hidPromotionDetailId.Value) : -99;
                            odata.CampaignCode = hidCampaigncode.Value;
                            odata.runningNo = ldata.Count + 1;
                            hidCampCode.Value = odata.CampaignCode;
                            ldata.Add(odata);
                        }

                        
                    }


                }


            }

            L_orderdata = ldata;
            LoadgvOrder(L_orderdata);
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "$('#modal-product').modal('hide');", true);
            
        }

        public void LoadgvOrder(List<OrderData> lorderdata)
        {
            gvOrder.DataSource = lorderdata;
            gvOrder.DataBind();
        }

        public void btneditcustomer_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-customer').modal();", true);
        }

        protected void btnSubmit_EditCustomer(object sender, EventArgs e)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/UpdateCustomer";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = CustomerCode;
                data["CustomerFName"] = txtCustomerFName_Edit.Text;
                data["CustomerLName"] = txtCustomerLName_Edit.Text;
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int? sum = JsonConvert.DeserializeObject<int?>(respstr);

            int? sum1 = 0;

            if (ValidateCustomerPhone()) // insert CustomerPhone
            {
                string respstr01 = "";
                APIpath = APIUrl + "/api/support/InsertCustomerPhone";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CustomerCode"] = CustomerCode;
                    data["CustomerPhone"] = CustomerPhone;
                    data["CustomerPhoneType"] = StaticField.PhoneType_01; 
                    data["CreateBy"] = hidEmpCode.Value;
                    data["UpdateBy"] = hidEmpCode.Value;
                    data["FlagDelete"] = "N";

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr01 = Encoding.UTF8.GetString(response);
                }
                sum1 = JsonConvert.DeserializeObject<int?>(respstr);
            }
            else
            {
                string respstr01 = "";
                APIpath = APIUrl + "/api/support/UpdateCustomerPhoneTakeOrderMK";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CustomerCode"] = CustomerCode;
                    data["PhoneNumber"] = CustomerPhone;
                    data["UpdateBy"] = hidEmpCode.Value;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr01 = Encoding.UTF8.GetString(response);
                }
                sum1 = JsonConvert.DeserializeObject<int?>(respstr);
            }
            if (sum > 0 && sum1 > 0)
            {
                BindCustomerLabel(CustomerCode);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-address').modal('hide');", true);
            }
        }

        protected void btnSubmit_EditCustomerAddress_Click(object sender, EventArgs e)
        {
            Boolean checksumradioselect = false;

            for(int i = 0; i < gvPopupCustomerAddress.Rows.Count; i++)
            {
                RadioButton radCusAddressDelivery = (RadioButton)gvPopupCustomerAddress.Rows[i].FindControl("radCustomerAddress");

                if(radCusAddressDelivery.Checked == true)
                {
                    checksumradioselect = true;
                }
            }

            if (checksumradioselect == true)
            {
                int sum = 0;
                for (int i = 0; i < gvPopupCustomerAddress.Rows.Count; i++)
                {
                    RadioButton radCustomerAddress = (RadioButton)gvPopupCustomerAddress.Rows[i].FindControl("radCustomerAddress");

                    if (radCustomerAddress.Checked == true)
                    {
                        HiddenField hidCustomerAddressId = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidCustomerAddressId");
                        HiddenField hidCustomerCode = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidCustomerCode");
                        HiddenField hidAddress = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidAddress");
                        HiddenField hidSubdistrict = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidSubdistrict");
                        HiddenField hidDistrict = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidDistrict");
                        HiddenField hidProvince = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidProvince");
                        HiddenField hidZipCode = (HiddenField)gvPopupCustomerAddress.Rows[i].FindControl("hidZipCode");

                        hidCustomerCodeAddressType01.Value = hidCustomerCode.Value;

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCustomerAddress";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerAddressId"] = hidCustomerAddressId.Value;
                            data["Address"] = hidAddress.Value;
                            data["Subdistrict"] = hidSubdistrict.Value;
                            data["District"] = hidDistrict.Value;
                            data["Province"] = hidProvince.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["ZipCode"] = hidZipCode.Value;
                            data["FlagActive"] = StaticField.ActiveFlag_Y; 
                            data["AddressType"] = hidAddressType.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        sum = JsonConvert.DeserializeObject<int>(respstr);
                    }
                }
                if (sum > 0)
                {
                    List<CustomerAddressInfo> lempInfo = new List<CustomerAddressInfo>();
                    lempInfo = BindCustomerAddressDelivery(hidCustomerCodeAddressType01.Value);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-address').modal('hide');", true);
                }
                else
                {

                }
            }
            else
            {
                if (hidFlagInsert.Value == "True") // Insert
                {
                    if (ValidateInsert())
                    {
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/InsertCustomerAddress";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                            data["AddressType"] = hidAddressType.Value;
                            data["Address"] = txtAddress_Ins.Text;
                            data["Province"] = ddlProvince.SelectedValue;
                            data["District"] = ddlDistrict.SelectedValue;
                            data["Subdistrict"] = ddlSubDistrict.SelectedValue;
                            data["ZipCode"] = txtPostcode_Ins.Text;
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagActive"] = StaticField.ActiveFlag_Y; 

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if (sum > 0)
                        {
                            BindCustomerAddressDelivery(CustomerCode);
                            btnCancel_Click(null, null);

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-address').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else // Update
                {
                    if (ValidateInsert())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCustomerAddress";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                            data["CustomerAddressId"] = hidCustomerAddressDeliveryIdUpd.Value;
                            data["AddressType"] = hidAddressType.Value;
                            data["Address"] = txtAddress_Ins.Text;
                            data["Province"] = ddlProvince.SelectedValue;
                            data["District"] = ddlDistrict.SelectedValue;
                            data["Subdistrict"] = ddlSubDistrict.SelectedValue;
                            data["ZipCode"] = txtPostcode_Ins.Text;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagActive"] = StaticField.ActiveFlag_Y; 

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            BindCustomerAddressDelivery(CustomerCode);
                            btnCancel_Click(null, null);

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-address').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }
                    }
                }
            }
        }

        protected void btnSubmit_EditCustomerAddressReceipt_Click(object sender, EventArgs e)
        {
            Boolean checkinsertcusaddressreceipt = false;

            for (int i = 0; i < gvCustomerAddressReceipt.Rows.Count; i++)
            {
                RadioButton radcusaddressreceipt = (RadioButton)gvCustomerAddressReceipt.Rows[i].FindControl("radCustomerAddressReceipt");

                if(radcusaddressreceipt.Checked == true)
                {
                    checkinsertcusaddressreceipt = true;
                }
            }

            if (checkinsertcusaddressreceipt == true)
            {
                int sum = 0;
                for (int i = 0; i < gvCustomerAddressReceipt.Rows.Count; i++)
                {
                    RadioButton radCustomerAddressReceipt = (RadioButton)gvCustomerAddressReceipt.Rows[i].FindControl("radCustomerAddressReceipt");

                    if (radCustomerAddressReceipt.Checked == true)
                    {
                        HiddenField hidCustomerAddressReceiptId = (HiddenField)gvCustomerAddressReceipt.Rows[i].FindControl("hidCustomerAddressReceiptId");
                        HiddenField hidCustomerReceiptCode = (HiddenField)gvCustomerAddressReceipt.Rows[i].FindControl("hidCustomerReceiptCode");
                        HiddenField hidAddressReceipt = (HiddenField)gvCustomerAddressReceipt.Rows[i].FindControl("hidAddressReceipt");
                        HiddenField hidSubdistrictReceipt = (HiddenField)gvCustomerAddressReceipt.Rows[i].FindControl("hidSubdistrictReceipt");
                        HiddenField hidDistrictReceipt = (HiddenField)gvCustomerAddressReceipt.Rows[i].FindControl("hidDistrictReceipt");
                        HiddenField hidProvinceReceipt = (HiddenField)gvCustomerAddressReceipt.Rows[i].FindControl("hidProvinceReceipt");
                        HiddenField hidZipCodeReceipt = (HiddenField)gvCustomerAddressReceipt.Rows[i].FindControl("hidZipCodeReceipt");

                        hidCustomerCodeAddressType02.Value = hidCustomerReceiptCode.Value;

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCustomerAddress";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerAddressId"] = hidCustomerAddressReceiptId.Value;
                            data["Address"] = hidAddressReceipt.Value;
                            data["Subdistrict"] = hidSubdistrictReceipt.Value;
                            data["District"] = hidDistrictReceipt.Value;
                            data["Province"] = hidProvinceReceipt.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["ZipCode"] = hidZipCodeReceipt.Value;
                            data["FlagActive"] = StaticField.ActiveFlag_Y; 
                            data["AddressType"] = hidAddressType.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        sum = JsonConvert.DeserializeObject<int>(respstr);
                    }
                }
                if (sum > 0)
                {
                    List<CustomerAddressInfo> lempInfo = new List<CustomerAddressInfo>();
                    lempInfo = BindCustomerAddressReceipt(hidCustomerCodeAddressType02.Value);
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-address2').modal('hide');", true);
                }
            }
            else
            {
                if (hidFlagInsert.Value == "True") // Insert
                {
                    if (ValidateInsertCusAddressReceipt())
                    {
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/InsertCustomerAddress";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                            data["AddressType"] = hidAddressType.Value;
                            data["Address"] = txtCusAddressReceiptIns.Text;
                            data["Province"] = ddlCusProvinceReceiptIns.SelectedValue;
                            data["District"] = ddlCusDistrictReceiptIns.SelectedValue;
                            data["Subdistrict"] = ddlCusSubdistrictReceiptIns.SelectedValue;
                            data["ZipCode"] = txtCusPostCodeReceiptIns.Text;
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagActive"] = StaticField.ActiveFlag_Y; 

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if (sum > 0)
                        {
                            BindCustomerAddressReceipt(CustomerCode);
                            btnCancelCusAddressReceipt_Click(null, null);

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-address2').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else // Update
                {
                    if (ValidateInsertCusAddressReceipt())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCustomerAddress";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                            data["CustomerAddressId"] = hidCustomerAddressReceiptIdUpd.Value;
                            data["AddressType"] = hidAddressType.Value;
                            data["Address"] = txtCusAddressReceiptIns.Text;
                            data["Province"] = ddlCusProvinceReceiptIns.SelectedValue;
                            data["District"] = ddlCusDistrictReceiptIns.SelectedValue;
                            data["Subdistrict"] = ddlCusSubdistrictReceiptIns.SelectedValue;
                            data["ZipCode"] = txtCusPostCodeReceiptIns.Text;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["FlagActive"] = StaticField.ActiveFlag_Y; 

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            BindCustomerAddressReceipt(CustomerCode);
                            btnCancelCusAddressReceipt_Click(null, null);

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-address2').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }
                    }
                }
            }
        }

        public void BindCustomerLabel(String cuscode)
        {
            List<CustomerInfo> lcustomerInfo = new List<CustomerInfo>();
            lcustomerInfo = GetCustomerNoPaging(cuscode);
            if (lcustomerInfo.Count > 0)
            {
                lblCustomerName.Text = lcustomerInfo[0].CustomerName;
                txtCustomerFName_Edit.Text = lcustomerInfo[0].CustomerFName;
                txtCustomerLName_Edit.Text = lcustomerInfo[0].CustomerLName;
            }

            lblCustomerPhone1.Text = CustomerPhone;

            
        }

        protected List<CustomerPhoneInfo> GetCustomerPhone(String cuscode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerPhone";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = (cuscode == "") ? "-99" : cuscode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerPhoneInfo> lCustomerPhoneInfo = JsonConvert.DeserializeObject<List<CustomerPhoneInfo>>(respstr);

            return lCustomerPhoneInfo;
        }

        public List<CustomerInfo> GetCustomerNoPaging(String cuscode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = (cuscode == "") ? "-99" : cuscode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerInfo> lCustomerInfo = JsonConvert.DeserializeObject<List<CustomerInfo>>(respstr);

            return lCustomerInfo;
        }

        protected void chkCustomerAddressSameDelivery_Changed(object sender, EventArgs e)
        {
            List<CustomerAddressInfo> lcusaddressinfo = new List<CustomerAddressInfo>();
            if (chkCustomerAddressSameDelivery.Checked == true)
            {
                if(ValidateCustomerAddressReceiptInsert()) // Insert
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = CustomerCode;
                        data["Address"] = lblAddress01.Text;
                        data["Subdistrict"] = hidSubDistrictCodeShow01.Value;
                        data["District"] = hidDistrictCodeShow01.Value;
                        data["Province"] = hidProvinceCodeShow01.Value;
                        data["ZipCode"] = lblZipCode01.Text;
                        data["AddressType"] = StaticField.AddressTypeCode02; 
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagActive"] = StaticField.ActiveFlag_Y; 

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }
                    int sum = JsonConvert.DeserializeObject<int>(respstr);
                    if (sum > 0)
                    {
                        BindCustomerAddressReceipt(CustomerCode);
                        LoadAddressPopupReceipt(SetFormAddressPopup2());

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }            
                else // Update
                {
                    lcusaddressinfo = GetCustomerIdfromAddressReceipt(CustomerCode);
                    if(lcusaddressinfo.Count > 0)
                    {
                        hidCustomerAddressIdShow02.Value = lcusaddressinfo[0].CustomerAddressId.ToString();
                    }

                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerAddressId"] = hidCustomerAddressIdShow02.Value;
                        data["Address"] = lblAddress01.Text;
                        data["Subdistrict"] = hidSubDistrictCodeShow01.Value;
                        data["District"] = hidDistrictCodeShow01.Value;
                        data["Province"] = hidProvinceCodeShow01.Value;
                        data["ZipCode"] = lblZipCode01.Text;
                        data["AddressType"] = StaticField.AddressTypeCode02; 
                        data["FlagActive"] = StaticField.ActiveFlag_Y; 
                        data["UpdateBy"] = hidEmpCode.Value;

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }
                    int sum = JsonConvert.DeserializeObject<int>(respstr);
                    if (sum > 0)
                    {
                        BindCustomerAddressReceipt(CustomerCode);
                        LoadAddressPopupReceipt(SetFormAddressPopup2());

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }
                }
            }
        } 

        protected void LinkButtonCreateCustomerAddressDelivery_Click(object sender, EventArgs e)
        {
            hidTabSelecteventCusAddressDelivery.Value = "Insert";
            hidFlagInsert.Value = "True";
            txtAddress_Ins.Text = "";
            txtPostcode_Ins.Text = "";
            ddlProvince.ClearSelection();
            ddlDistrict.ClearSelection();
            ddlSubDistrict.ClearSelection();
            InsertSectionDelivery.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address').modal();", true);
        }

        protected void LinkBtnCreateCustomerAddressReceipt_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";
            txtCusAddressReceiptIns.Text = "";
            txtCusPostCodeReceiptIns.Text = "";
            ddlCusSubdistrictReceiptIns.ClearSelection();
            ddlCusDistrictReceiptIns.ClearSelection();
            ddlCusProvinceReceiptIns.ClearSelection();
            InsertCustomerAddressReceiptSection.Visible = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address2').modal();", true);
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDistrict(ddlProvince.SelectedValue, ddlDistrict);
            ddlSubDistrict.SelectedIndex = 0;
        }

        protected void ddlCusProvinceReceiptIns_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDistrict(ddlCusProvinceReceiptIns.SelectedValue, ddlCusDistrictReceiptIns);
            ddlCusSubdistrictReceiptIns.SelectedIndex = 0;
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlSubDistrict(ddlDistrict.SelectedValue);
        }

        protected void ddlCusDistrictReceiptIns_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlSubDistrict(ddlCusDistrictReceiptIns.SelectedValue);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtAddress_Ins.Text = "";
            txtPostcode_Ins.Text = "";
            ddlProvince.ClearSelection();
            ddlDistrict.ClearSelection();
            ddlSubDistrict.ClearSelection();

            lblAddress.Text = "";
            lblProvince_Ins.Text = "";
            lblDistrict_Ins.Text = "";
            lblSubDistrict_Ins.Text = "";
            txtPostcode_Ins.Text = "";

            InsertSectionDelivery.Visible = false;
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (hidFlagInsert.Value == "True") // Insert
            {
                if (ValidateInsert()) 
                {
                    string respstr = "";
                    APIpath = APIUrl + "/api/support/InsertCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                        data["AddressType"] = hidAddressType.Value;
                        data["Address"] = txtAddress_Ins.Text;
                        data["Province"] = ddlProvince.SelectedValue;
                        data["District"] = ddlDistrict.SelectedValue;
                        data["Subdistrict"] = ddlSubDistrict.SelectedValue;
                        data["ZipCode"] = txtPostcode_Ins.Text;
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagActive"] = StaticField.ActiveFlag_Y; 

                        var response = wb.UploadValues(APIpath, "POST", data);
                        respstr = Encoding.UTF8.GetString(response);
                    }
                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                    if (sum > 0)
                    {
                        BindCustomerAddressDelivery(CustomerCode);
                        btnCancel_Click(null, null);
                        
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-address').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                } 
            }
            else // Update
            {
                if (ValidateInsert())
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                        data["CustomerAddressId"] = hidCustomerAddressDeliveryIdUpd.Value;
                        data["AddressType"] = hidAddressType.Value;
                        data["Address"] = txtAddress_Ins.Text;
                        data["Province"] = ddlProvince.SelectedValue;
                        data["District"] = ddlDistrict.SelectedValue;
                        data["Subdistrict"] = ddlSubDistrict.SelectedValue;
                        data["ZipCode"] = txtPostcode_Ins.Text;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagActive"] = StaticField.ActiveFlag_Y; 

                        var response = wb.UploadValues(APIpath, "POST", data);
                        respstr = Encoding.UTF8.GetString(response);
                    }
                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                    if (sum > 0)
                    {
                        BindCustomerAddressDelivery(CustomerCode);
                        btnCancel_Click(null, null);

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-address').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }
                }
            }
        }

        protected Boolean ValidateInsert()
        {
            Boolean flag = true;

            if (txtAddress_Ins.Text == "")
            {
                flag = false;
                lblAddress.Text = MessageConst._MSG_PLEASEINSERT + " ที่อยู่";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblAddress.Text = "";
            }

            if (ddlSubDistrict.SelectedValue == "-99" || ddlSubDistrict.SelectedValue == "")
            {
                flag = false;
                lblSubDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ตำบล/แขวง";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSubDistrict_Ins.Text = "";
            }

            if (ddlDistrict.SelectedValue == "-99" || ddlDistrict.SelectedValue == "")
            {
                flag = false;
                lblDistrict_Ins.Text = MessageConst._MSG_PLEASEINSERT + " อำเภอ/เขต";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDistrict_Ins.Text = "";
            }
            if (ddlProvince.SelectedValue == "-99" || ddlProvince.SelectedValue == "")
            {
                flag = false;
                lblProvince_Ins.Text = MessageConst._MSG_PLEASEINSERT + " จังหวัด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProvince_Ins.Text = "";
            }


            if (txtPostcode_Ins.Text == "")
            {
                flag = false;
                lblPostCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสไปรษณีย์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPostCode_Ins.Text = "";
            }

            return flag;
        }

        protected Boolean ValidateUpdate()
        {
            Boolean flag = true;
            return flag;
        }

        protected void btnSubmitCusAddressReceipt_Click(object sender, EventArgs e)
        {
            if (hidFlagInsert.Value == "True") // Insert
            {
                if (ValidateInsertCusAddressReceipt())
                {
                    string respstr = "";
                    APIpath = APIUrl + "/api/support/InsertCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                        data["AddressType"] = hidAddressType.Value;
                        data["Address"] = txtCusAddressReceiptIns.Text;
                        data["Province"] = ddlCusProvinceReceiptIns.SelectedValue;
                        data["District"] = ddlCusDistrictReceiptIns.SelectedValue;
                        data["Subdistrict"] = ddlCusSubdistrictReceiptIns.SelectedValue;
                        data["ZipCode"] = txtCusPostCodeReceiptIns.Text;
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagActive"] = StaticField.ActiveFlag_Y; 

                        var response = wb.UploadValues(APIpath, "POST", data);
                        respstr = Encoding.UTF8.GetString(response);
                    }
                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                    if (sum > 0)
                    {
                        BindCustomerAddressReceipt(CustomerCode);
                        btnCancelCusAddressReceipt_Click(null, null);

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-address2').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
            }
            else // Update
            {
                if (ValidateInsertCusAddressReceipt())
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateCustomerAddress";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                        data["CustomerAddressId"] = hidCustomerAddressReceiptIdUpd.Value;
                        data["AddressType"] = hidAddressType.Value;
                        data["Address"] = txtCusAddressReceiptIns.Text;
                        data["Province"] = ddlCusProvinceReceiptIns.SelectedValue;
                        data["District"] = ddlCusDistrictReceiptIns.SelectedValue;
                        data["Subdistrict"] = ddlCusSubdistrictReceiptIns.SelectedValue;
                        data["ZipCode"] = txtCusPostCodeReceiptIns.Text;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagActive"] = StaticField.ActiveFlag_Y; 

                        var response = wb.UploadValues(APIpath, "POST", data);
                        respstr = Encoding.UTF8.GetString(response);
                    }
                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                    if (sum > 0)
                    {
                        BindCustomerAddressReceipt(CustomerCode);
                        btnCancelCusAddressReceipt_Click(null, null);

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-address2').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }
                }
            }
        }

        protected Boolean ValidateInsertCusAddressReceipt()
        {
            Boolean flag = true;

            if (txtCusAddressReceiptIns.Text == "")
            {
                flag = false;
                lblCusAddressReceiptIns.Text = MessageConst._MSG_PLEASEINSERT + " ที่อยู่";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCusAddressReceiptIns.Text = "";
            }

            if (ddlCusSubdistrictReceiptIns.SelectedValue == "-99" || ddlCusSubdistrictReceiptIns.SelectedValue == "")
            {
                flag = false;
                lblCusSubdistrictReceiptIns.Text = MessageConst._MSG_PLEASEINSERT + " ตำบล/แขวง";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCusSubdistrictReceiptIns.Text = "";
            }

            if (ddlCusDistrictReceiptIns.SelectedValue == "-99" || ddlCusDistrictReceiptIns.SelectedValue == "")
            {
                flag = false;
                lblCusDistrictReceiptIns.Text = MessageConst._MSG_PLEASEINSERT + " อำเภอ/เขต";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCusDistrictReceiptIns.Text = "";
            }
            if (ddlCusProvinceReceiptIns.SelectedValue == "-99" || ddlCusProvinceReceiptIns.SelectedValue == "")
            {
                flag = false;
                lblCusProvinceReceiptIns.Text = MessageConst._MSG_PLEASEINSERT + " จังหวัด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCusProvinceReceiptIns.Text = "";
            }


            if (txtCusPostCodeReceiptIns.Text == "")
            {
                flag = false;
                lblCusPostCodeReceiptIns.Text = MessageConst._MSG_PLEASEINSERT + " รหัสไปรษณีย์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCusPostCodeReceiptIns.Text = "";
            }

            return flag;
        }

        protected void btnCancelCusAddressReceipt_Click(object sender, EventArgs e)
        {
            txtCusAddressReceiptIns.Text = "";
            txtCusPostCodeReceiptIns.Text = "";
            ddlCusProvinceReceiptIns.ClearSelection();
            ddlCusDistrictReceiptIns.ClearSelection();
            ddlCusSubdistrictReceiptIns.ClearSelection();

            lblCusAddressReceiptIns.Text = "";
            lblCusProvinceReceiptIns.Text = "";
            lblCusDistrictReceiptIns.Text = "";
            lblCusSubdistrictReceiptIns.Text = "";
            txtCusPostCodeReceiptIns.Text = "";

            InsertCustomerAddressReceiptSection.Visible = false;
        }
        protected void btnSubmitOrder_Click(Object sender, EventArgs e)
        {
            CustomerCode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
            CustomerPhone = (Request.QueryString["Customerphone"] != null) ? Request.QueryString["Customerphone"].ToString() : "";

            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            string strordercode = "";
            double? sumvat = 0;
            double? sumtotal = 0;
            string APIpath = "";

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else
            {
                foreach (var lodt in L_orderdata)
                {
                    sumvat += (((((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount) * 7) / 100);
                    sumtotal += (((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount);
                }
                //Insert orderInfo
                APIpath = APIUrl + "/api/support/InsertOMSOrderdata";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CustomerCode"] = CustomerCode;
                    data["CustomerPhone"] = CustomerPhone;
                    data["SubTotalPrice"] = sumtotal.ToString();
                    data["Vat"] = sumvat.ToString();
                    data["TotalPrice"] = (sumtotal + sumvat).ToString(); 
                    data["OrderStatusCode"] = StaticField.OrderStatus_01; 
                    data["OrderStateCode"] = StaticField.OrderState_01;              
                    data["UpdateBy"] = empInfo.EmpCode;
                    data["CreateBy"] = empInfo.EmpCode;
                    data["FlagDelete"] = "N";
                    data["ChannelCode"] = StaticField.ChannelCode_Tel; 
                    data["SALEORDERTYPE"] = StaticField.SaleOrderType_01; 
                    data["TransportPrice"] = StaticField.TransportPrice_40; //40 Baht for transport price

                    var response = wb.UploadValues(APIpath, "POST", data);

                    strordercode = Encoding.UTF8.GetString(response);
                    strordercode = strordercode.Replace("\"", "");
                }
                //Insert Orderdetailinfo
                foreach (var lodt in L_orderdata)
                {
                    string respstringorderdetail = "";
                    APIpath = APIUrl + "/api/support/InsertOMSOrderdetaildata";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["ProductCode"] = lodt.ProductCode;
                        data["PromotionDetailId"] = lodt.PromotionDetailId.ToString();
                        data["Price"] = lodt.Price.ToString();
                        data["CustomerCode"] = CustomerCode;
                        data["NetPrice"] = ((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount).ToString();
                        data["TotalPrice"] = (((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount).ToString();
                        data["Vat"] = (((((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount) * 7) / 100).ToString();
                        data["Unit"] = lodt.UnitCode;
                        data["Amount"] = lodt.Amount.ToString();
                        data["UpdateBy"] = empInfo.EmpCode;
                        data["CreateBy"] = empInfo.EmpCode;
                        data["FlagDelete"] = "N";
                        data["OrderCode"] = strordercode;
                        data["ParentProductCode"] = lodt.ParentProductCode;
                        data["PromotionCode"] = lodt.PromotionCode;
                        data["CampaignCode"] = lodt.CampaignCode;

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstringorderdetail = Encoding.UTF8.GetString(response);
                    }
                }
                // insert paymentdata
                paymentdataInfo pdata = new paymentdataInfo();
                List<paymentdataInfo> lpdata = new List<paymentdataInfo>();

                pdata.OrderCode = strordercode;
                pdata.PaymentTypeCode = StaticField.PaymentType01; 
                
                pdata.CreateBy = hidEmpCode.Value;
                pdata.UpdateBy = hidEmpCode.Value;
                pdata.FlagDelete = "N";

                lpdata.Add(pdata);

                foreach (var paymentV in lpdata)
                {
                    string respstring = "";
                    APIpath = APIUrl + "/api/support/paymentOMSInsert";
                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["OrderCode"] = paymentV.OrderCode;
                        data["PaymentTypeCode"] = paymentV.PaymentTypeCode;
                        data["Payamount"] = (sumtotal + sumvat).ToString(); 
                        data["CreateBy"] = paymentV.CreateBy;
                        data["UpdateBy"] = paymentV.UpdateBy;
                        data["FlagDelete"] = "N";

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstring = Encoding.UTF8.GetString(response);
                    }
                }

                // insert transportdata
                transportdataInfo tInfo = new transportdataInfo();

                if(L_transportdata.Count == 0)
                {
                    if(lblAddress01.Text != "" && lblSubDistrictName01.Text != "" && lblDistrictName01.Text != "" && lblProvinceName01.Text != "" && lblZipCode01.Text != "")
                    {
                        tInfo.AddressId = hidCustomerAddressIdShow01.Value;
                        tInfo.Address = lblAddress01.Text;
                        tInfo.SubDistrictCode = hidSubDistrictCodeShow01.Value;
                        tInfo.SubDistrictName = lblSubDistrictName01.Text;
                        tInfo.DistrictCode = hidDistrictCodeShow01.Value;
                        tInfo.DistrictName = lblDistrictName01.Text;
                        tInfo.ProvinceCode = hidProvinceCodeShow01.Value;
                        tInfo.ProvinceName = lblProvinceName01.Text;
                        tInfo.Zipcode = lblZipCode01.Text;
                        tInfo.AddressType = StaticField.AddressTypeCode01; 

                        L_transportdata.Add(tInfo);
                    }
                    if(lblAddress02.Text != "" && lblSubDistrictName02.Text != "" && lblDistrictName02.Text != "" && lblProvinceName02.Text != "" && lblZipCode02.Text != "")
                    {
                        tInfo.AddressId = hidCustomerAddressIdShow02.Value;
                        tInfo.Address = lblAddress02.Text;
                        tInfo.SubDistrictCode = hidSubDistrictCodeShow02.Value;
                        tInfo.SubDistrictName = lblSubDistrictName02.Text;
                        tInfo.DistrictCode = hidDistrictCodeShow02.Value;
                        tInfo.DistrictName = lblDistrictName02.Text;
                        tInfo.ProvinceCode = hidProvinceCodeShow02.Value;
                        tInfo.ProvinceName = lblProvinceName02.Text;
                        tInfo.Zipcode = lblZipCode02.Text;
                        tInfo.AddressType = StaticField.AddressTypeCode02; 

                        L_transportdata.Add(tInfo);
                    }
                }

                foreach (var tdataV in L_transportdata)
                {
                    string respstringorderdetail = "";
                    APIpath = APIUrl + "/api/support/InsertOMSTransportdata";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["OrderCode"] = strordercode;
                        data["Address"] = tdataV.Address;
                        data["SubDistrictCode"] = tdataV.SubDistrictCode;
                        data["DistrictCode"] = tdataV.DistrictCode;
                        data["ProvinceCode"] = tdataV.ProvinceCode;
                        data["Zipcode"] = tdataV.Zipcode;
                        data["TransportPrice"] = (tInfo.TransportPrice == null || tInfo.TransportPrice == "") ? "0" : tInfo.TransportPrice;
                        data["AddressType"] = tdataV.AddressType;
                        
                        data["UpdateBy"] = empInfo.EmpCode;
                        data["CreateBy"] = empInfo.EmpCode;
                        

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstringorderdetail = Encoding.UTF8.GetString(response);
                    }
                }
            }
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "บันทึกข้อมูลเสร็จสิ้น เลขที่ใบสั่งขาย : " + strordercode + "');", true);
        }
        #endregion

        #region Binding

        protected void BindCampaign()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> lCampaignInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);


            lbCampaign.DataSource = lCampaignInfo;

            lbCampaign.DataTextField = "CampaignName";

            lbCampaign.DataValueField = "CampaignCode";

            lbCampaign.DataBind();

            lbCampaign.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected void BindPromotion(PromotionInfo pinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignPromotionNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = pinfo.CampaignCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);


            lbPromotion.DataSource = lPromotionInfo;

            lbPromotion.DataTextField = "PromotionName";

            lbPromotion.DataValueField = "PromotionCode";

            lbPromotion.DataBind();

            lbPromotion.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }





        #endregion

      
    }
}
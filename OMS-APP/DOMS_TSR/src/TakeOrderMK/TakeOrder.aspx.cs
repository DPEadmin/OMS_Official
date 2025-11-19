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
using System.Threading;
using System.IO;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using Microsoft.AspNet.SignalR;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace DOMS_TSR.src.TakeOrderMK
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
        public string ColorCode { get; set; }
        public string ProductOrderType { get; set; }
        public Double? Price { get; set; }
        public string Unit { get; set; }
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
        public string LandmarkLat { get; set; }
        public string LandmarkLng { get; set; }
        public string OrderNote { get; set; }
        public string DeliveryDate { get; set; }
        public string FlagApproved { get; set; }

    }

    public class FinishOrderReturn
    {
        public String unique_id { get; set; }
        public String customer_fname { get; set; }
        public String customer_lname { get; set; }
        public List<order_detail> order_detail { get; set; } = new List<order_detail>();
        public String channel { get; set; }
    }

    public class order_detail
    {
        public String order_code { get; set; }
        public String brand_name { get; set; }
        public Double total_amount { get; set; }
        public String branch_name { get; set; }
        public String order_status { get; set; }
    }

  
    public class ResultOneApp
    {
        public String resultCode { get; set; }
        public String resultMessage { get; set; }
        public List<resultData> resultData { get; set; } = new List<resultData>();
     
    }
    public class resultData
    {
        public String resultdata { get; set; }
    }
    public class ResultOneApp_retrun
    {
        public List<ResultOneApp> resultapp { get; set; } = new List<ResultOneApp>();


    }
    public partial class TakeOrder : System.Web.UI.Page
    {
        string Codelist = "";
        string EditFlag = "";
        protected static int currentPageNumber;
        protected static int currentPageNumberProduct;

        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        decimal? total = 0;
        string CustomerCode = "";
        string RefCode = "";
        string CustomerPhone = "";
        string APIpath = "";
        string EmpCode = "";
        protected List<OrderData> L_orderdata1
        {
            get
            {
                if (Session["l_orderdata1"] == null)
                {
                    return new List<OrderData>();
                }
                else
                {
                    return (List<OrderData>)Session["l_orderdata1"];
                }
            }
            set
            {
                Session["l_orderdata1"] = value;
            }
        }

        protected List<OrderData> L_orderdata2
        {
            get
            {
                if (Session["l_orderdata2"] == null)
                {
                    return new List<OrderData>();
                }
                else
                {
                    return (List<OrderData>)Session["l_orderdata2"];
                }
            }
            set
            {
                Session["l_orderdata2"] = value;
            }
        }

        protected List<OrderData> L_orderdata3
        {
            get
            {
                if (Session["l_orderdata3"] == null)
                {
                    return new List<OrderData>();
                }
                else
                {
                    return (List<OrderData>)Session["l_orderdata3"];
                }
            }
            set
            {
                Session["l_orderdata3"] = value;
            }
        }

        protected List<transportdataInfo> L_transportdata1
        {
            get
            {
                if (Session["L_transportdata1"] == null)
                {
                    return new List<transportdataInfo>();
                }
                else
                {
                    return (List<transportdataInfo>)Session["L_transportdata1"];
                }
            }
            set
            {
                Session["L_transportdata1"] = value;
            }
        }

        protected List<transportdataInfo> L_transportdata2
        {
            get
            {
                if (Session["L_transportdata2"] == null)
                {
                    return new List<transportdataInfo>();
                }
                else
                {
                    return (List<transportdataInfo>)Session["L_transportdata2"];
                }
            }
            set
            {
                Session["L_transportdata2"] = value;
            }
        }

        protected List<transportdataInfo> L_transportdata3
        {
            get
            {
                if (Session["L_transportdata3"] == null)
                {
                    return new List<transportdataInfo>();
                }
                else
                {
                    return (List<transportdataInfo>)Session["L_transportdata3"];
                }
            }
            set
            {
                Session["L_transportdata3"] = value;
            }
        }

        protected List<paymentdataInfo> L_paymentdata1
        {
            get
            {
                if (Session["L_paymentdata1"] == null)
                {
                    return new List<paymentdataInfo>();
                }
                else
                {
                    return (List<paymentdataInfo>)Session["L_paymentdata1"];
                }
            }
            set
            {
                Session["L_paymentdata1"] = value;
            }
        }



        protected List<paymentdataInfo> L_paymentdata2
        {
            get
            {
                if (Session["L_paymentdata2"] == null)
                {
                    return new List<paymentdataInfo>();
                }
                else
                {
                    return (List<paymentdataInfo>)Session["L_paymentdata2"];
                }
            }
            set
            {
                Session["L_paymentdata2"] = value;
            }
        }

        protected List<paymentdataInfo> L_paymentdata3
        {
            get
            {
                if (Session["L_paymentdata3"] == null)
                {
                    return new List<paymentdataInfo>();
                }
                else
                {
                    return (List<paymentdataInfo>)Session["L_paymentdata3"];
                }
            }
            set
            {
                Session["L_paymentdata3"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerCode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
            CustomerPhone = (Request.QueryString["CustomerPhone"] != null) ? Request.QueryString["CustomerPhone"].ToString() : "";
            
            Session["CustomerCode"] = CustomerCode;
            lblCustomerCode.Text = CustomerCode;

            lblProdTop01.Visible = false;
            lblProdTop02.Visible = false;
            lblProdTop03.Visible = false;
            lblProdTop04.Visible = false;
            lblProdTop05.Visible = false;

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumberProduct = 1;

                CreateSessionEmp();

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



                

                L_orderdata1 = new List<OrderData>();
                L_orderdata2 = new List<OrderData>();
                L_orderdata3 = new List<OrderData>();

                L_transportdata1 = new List<transportdataInfo>();
                L_transportdata2 = new List<transportdataInfo>();
                L_transportdata3 = new List<transportdataInfo>();

                BindCampaignCategory();
                BindCampaign();

                
                BindCustomerLabel(CustomerCode);
                
                btntab1_Click(null, null);
                LoadCustomerDeliveryAddressPageLoad();
                LoadNoteProfile(CustomerCode);
                LoadOrderNote(CustomerCode);
                LoadLatestOrderdata(CustomerCode);
            }
        }

        #region Function

        protected OrderData bindorderdata(OrderData item)
        {
            OrderData odata = new OrderData();
            odata.CampaignCategory = item.CampaignCategory;
            odata.CampaignCategoryName = item.CampaignCategoryName;
            odata.CampaignCode = item.CampaignCode;
            odata.PromotionCode = item.PromotionCode;
            odata.ColorCode = item.ColorCode;
            odata.PromotionDetailId = item.PromotionDetailId;
            odata.ProductCode = item.ProductCode;
            odata.ProductName = item.ProductName;
            odata.DiscountAmount = item.DiscountAmount;
            odata.DiscountPercent = item.DiscountPercent;
            odata.Price = item.Price;
            odata.FlagCombo = item.FlagCombo;
            odata.ParentProductCode = item.ParentProductCode;
            odata.CustomerCode = item.CustomerCode;
            odata.ComboCode = item.ComboCode;
            odata.ComboName = item.ComboName;
            odata.runningNo = item.runningNo;

            return odata;
        }

        public void LoadCustomerDeliveryAddress()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLatestUpdatedCustomerAddress";

            Label lblFullAddress = (Label)SelectBranch.FindControl("lblFullAddress");

            HiddenField currentDeliveryAddressId = (HiddenField)SelectBranch.FindControl("currentDeliveryAddressId");
            HiddenField currentDeliveryAddress = (HiddenField)SelectBranch.FindControl("currentDeliveryAddress");
            HiddenField currentDeliverySubDistrict = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrict");
            HiddenField currentDeliverySubDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrictName");
            HiddenField currentDeliveryDistrict = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrict");
            HiddenField currentDeliveryDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrictName");
            HiddenField currentDeliveryProvince = (HiddenField)SelectBranch.FindControl("currentDeliveryProvince");
            HiddenField currentDeliveryProvinceName = (HiddenField)SelectBranch.FindControl("currentDeliveryProvinceName");
            HiddenField currentDeliveryZipCode = (HiddenField)SelectBranch.FindControl("currentDeliveryZipCode");

            HiddenField currentReceiptAddressId = (HiddenField)SelectBranch.FindControl("currentReceiptAddressId");
            HiddenField currentReceiptAddress = (HiddenField)SelectBranch.FindControl("currentReceiptAddress");
            HiddenField currentReceiptSubDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrict");
            HiddenField currentReceiptSubDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrictName");
            HiddenField currentReceiptDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptDistrict");
            HiddenField currentReceiptDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptDistrictName");
            HiddenField currentReceiptProvince = (HiddenField)SelectBranch.FindControl("currentReceiptProvince");
            HiddenField currentReceiptProvinceName = (HiddenField)SelectBranch.FindControl("currentReceiptProvinceName");
            HiddenField currentReceiptZipCode = (HiddenField)SelectBranch.FindControl("currentReceiptZipCode");


            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if (currentDeliveryAddressId.Value != "")
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode01; 
                    data["CustomerAddressId"] = currentDeliveryAddressId.Value;

                }
                else
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode01; 
                }


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCustomerDeliveryAddress = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCustomerDeliveryAddress.Count > 0)
            {
                currentDeliveryAddress.Value = (lCustomerDeliveryAddress[0].Address != null) ? lCustomerDeliveryAddress[0].Address : "";
                currentDeliverySubDistrict.Value = lCustomerDeliveryAddress[0].Subdistrict;
                currentDeliverySubDistrictName.Value = lCustomerDeliveryAddress[0].SubdistrictName;
                currentDeliveryDistrict.Value = lCustomerDeliveryAddress[0].District;
                currentDeliveryDistrictName.Value = lCustomerDeliveryAddress[0].DistrictName;
                currentDeliveryProvince.Value = lCustomerDeliveryAddress[0].Province;
                currentDeliveryProvinceName.Value = lCustomerDeliveryAddress[0].ProvinceName;
                currentDeliveryZipCode.Value = lCustomerDeliveryAddress[0].ZipCode;

                lblFullAddress.Text = lCustomerDeliveryAddress[0].Address + " " + lCustomerDeliveryAddress[0].SubdistrictName + " " + lCustomerDeliveryAddress[0].DistrictName +
                " " + lCustomerDeliveryAddress[0].ProvinceName + " " + lCustomerDeliveryAddress[0].ZipCode;

                currentDeliveryAddressId.Value = lCustomerDeliveryAddress[0].CustomerAddressId.ToString();

            }
        }


        public void LoadCustomerDeliveryAddressPageLoad()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLatestUpdatedCustomerAddress";

            Label lblFullAddress = (Label)SelectBranch.FindControl("lblFullAddress");

            HiddenField currentDeliveryAddressId = (HiddenField)SelectBranch.FindControl("currentDeliveryAddressId");
            HiddenField currentDeliveryAddress = (HiddenField)SelectBranch.FindControl("currentDeliveryAddress");
            HiddenField currentDeliverySubDistrict = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrict");
            HiddenField currentDeliverySubDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrictName");
            HiddenField currentDeliveryDistrict = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrict");
            HiddenField currentDeliveryDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrictName");
            HiddenField currentDeliveryProvince = (HiddenField)SelectBranch.FindControl("currentDeliveryProvince");
            HiddenField currentDeliveryProvinceName = (HiddenField)SelectBranch.FindControl("currentDeliveryProvinceName");
            HiddenField currentDeliveryZipCode = (HiddenField)SelectBranch.FindControl("currentDeliveryZipCode");

            HiddenField currentReceiptAddressId = (HiddenField)SelectBranch.FindControl("currentReceiptAddressId");
            HiddenField currentReceiptAddress = (HiddenField)SelectBranch.FindControl("currentReceiptAddress");
            HiddenField currentReceiptSubDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrict");
            HiddenField currentReceiptSubDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrictName");
            HiddenField currentReceiptDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptDistrict");
            HiddenField currentReceiptDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptDistrictName");
            HiddenField currentReceiptProvince = (HiddenField)SelectBranch.FindControl("currentReceiptProvince");
            HiddenField currentReceiptProvinceName = (HiddenField)SelectBranch.FindControl("currentReceiptProvinceName");
            HiddenField currentReceiptZipCode = (HiddenField)SelectBranch.FindControl("currentReceiptZipCode");


            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if (currentDeliveryAddressId.Value != "")
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode01; 
                    data["CustomerAddressId"] = currentDeliveryAddressId.Value;

                }
                else
                {
                    data["CustomerCode"] = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
                    data["AddressType"] = StaticField.AddressTypeCode01; 
                }


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CustomerAddressInfo> lCustomerDeliveryAddress = JsonConvert.DeserializeObject<List<CustomerAddressInfo>>(respstr);

            if (lCustomerDeliveryAddress.Count > 0)
            {
                currentDeliveryAddress.Value = (lCustomerDeliveryAddress[0].Address != null) ? lCustomerDeliveryAddress[0].Address : "";
                currentDeliverySubDistrict.Value = lCustomerDeliveryAddress[0].Subdistrict;
                currentDeliverySubDistrictName.Value = lCustomerDeliveryAddress[0].SubdistrictName;
                currentDeliveryDistrict.Value = lCustomerDeliveryAddress[0].District;
                currentDeliveryDistrictName.Value = lCustomerDeliveryAddress[0].DistrictName;
                currentDeliveryProvince.Value = lCustomerDeliveryAddress[0].Province;
                currentDeliveryProvinceName.Value = lCustomerDeliveryAddress[0].ProvinceName;
                currentDeliveryZipCode.Value = lCustomerDeliveryAddress[0].ZipCode;

                lblFullAddress.Text = lCustomerDeliveryAddress[0].Address + " " + lCustomerDeliveryAddress[0].SubdistrictName + " " + lCustomerDeliveryAddress[0].DistrictName +
                " " + lCustomerDeliveryAddress[0].ProvinceName + " " + lCustomerDeliveryAddress[0].ZipCode;

                currentDeliveryAddressId.Value = lCustomerDeliveryAddress[0].CustomerAddressId.ToString();

                if (lCustomerDeliveryAddress[0].Lat.ToString() != "" && lCustomerDeliveryAddress[0].Lat.ToString() != null && lCustomerDeliveryAddress[0].Long.ToString() != "" && lCustomerDeliveryAddress[0].Long.ToString() != null)
                {
                    UsrCtrl_SelectBranch_Click(Convert.ToDouble(lCustomerDeliveryAddress[0].Lat), Convert.ToDouble(lCustomerDeliveryAddress[0].Long), lCustomerDeliveryAddress[0].AreaCode);
                }


            }
        }


        public void UpdateCustomerAddressLatLng(CustomerAddressInfo cInfo)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateCustomerAddressLatLng";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CustomerAddressId"] = cInfo.CustomerAddressId.ToString();
                    data["Lat"] = cInfo.Lat;
                    data["Long"] = cInfo.Long;
                    data["AreaCode"] = cInfo.AreaCode;
                    data["UpdateBy"] = empInfo.EmpCode;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            }


        }



        public static List<BranchInfo> LoadBranch()
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/ListBranchByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CompanyCode"] = "MK";


                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<BranchInfo> lBranchInfo = JsonConvert.DeserializeObject<List<BranchInfo>>(respstr);

            return lBranchInfo;

        }

        public static List<BranchInfo> LoadBranchList(string AreaCode)
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/ListBranchByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CompanyCode"] = StaticField.CompanyCode_MK; 
                data["AreaCode"] = AreaCode;


                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<BranchInfo> lBranchInfo = JsonConvert.DeserializeObject<List<BranchInfo>>(respstr);

            return lBranchInfo;

        }



        public static List<AreaInfo> LoadArea()
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/ListAreaByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<AreaInfo> lAreaInfo = JsonConvert.DeserializeObject<List<AreaInfo>>(respstr);

            return lAreaInfo;

        }


        public void LoadgvOrder(List<OrderData> lorderdata)
        {
            
            gvOrder.DataSource = lorderdata;
            gvOrder.DataBind();
        }

        public void LoadgvOrderfromTopProduct(List<OrderData> lorderdata, OrderData oadata)
        {
            if ((hidtab.Value == "1") || (hidtab.Value == ""))
            {
                if ((hidtab1CampaignCategory.Value == "") || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop01.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop02.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop03.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop04.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop05.Value))
                {
                    gvOrder.DataSource = lorderdata;
                    gvOrder.DataBind();
                }
                else
                {
                    oadata = new OrderData();
                }
            }
            else if (hidtab.Value == "2")
            {
                if ((hidtab2CampaignCategory.Value == "") || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop01.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop02.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop03.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop04.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop05.Value)) // check tab 2 เป็น campaign category เดียวกับที่เลือก  //แทป2ยังว่าง
                {
                    gvOrder.DataSource = lorderdata;
                    gvOrder.DataBind();
                }
            }
            else if (hidtab.Value == "3")
            {
                if ((hidtab3CampaignCategory.Value == "") || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop01.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop02.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop03.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop04.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop05.Value)) // check tab 2 เป็น campaign category เดียวกับที่เลือก  //แทป2ยังว่าง
                {
                    gvOrder.DataSource = lorderdata;
                    gvOrder.DataBind();
                }
            }
        }
        public ProductInfo SetFormProductPopup()
        {
            ProductInfo pinfo = new ProductInfo();

            

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

                data["rowOFFSet"] = ((currentPageNumberProduct - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }
        public void LoadOrderdata(OrderData odata)
        {

            List<OrderData> lorderdata = new List<OrderData>();

            if ((hidtab.Value == "1") || (hidtab.Value == ""))
            {
                if ((hidcampaigncategorycode.Value == hidtab1CampaignCategory.Value) || (hidtab1CampaignCategory.Value == "")) // check tab 1 เป็น campaign category เดียวกับที่เลือก  //แทป1ยังว่าง
                {
                    if (L_orderdata1.Count > 0) //ถ้ามีข้อมูลที่แทป 1 อยู่แล้ว
                    {
                        lorderdata = L_orderdata1;
                    }


                    odata.runningNo = lorderdata.Count + 1;
                    lorderdata.Add(odata);

                    L_orderdata1 = lorderdata;

                    btntab1.BackColor = System.Drawing.Color.CadetBlue;
                    btntab2.BackColor = System.Drawing.Color.Red;
                    btntab3.BackColor = System.Drawing.Color.Red;

                    hidtab1CampaignCategory.Value = hidcampaigncategorycode.Value;

                    hidtab.Value = "1";
                }

                LoadgvOrder(L_orderdata1);

                if (hidtab1CampaignCategoryname.Value == "")
                {
                    hidtab1CampaignCategoryname.Value = hidcampaigncategoryname.Value;
                }

                lblCampaignCategory.Text = hidtab1CampaignCategoryname.Value;
            }
            else if (hidtab.Value == "2")
            {
                if ((hidcampaigncategorycode.Value == hidtab2CampaignCategory.Value) || (hidtab2CampaignCategory.Value == "")) // check tab 2 เป็น campaign category เดียวกับที่เลือก  //แทป2ยังว่าง
                {
                    if (L_orderdata2.Count > 0) //ถ้ามีข้อมูลที่แทป 2 อยู่แล้ว
                    {
                        lorderdata = L_orderdata2;
                    }


                    odata.runningNo = lorderdata.Count + 1;
                    lorderdata.Add(odata);


                    L_orderdata2 = lorderdata;

                    btntab1.BackColor = System.Drawing.Color.Red;
                    btntab2.BackColor = System.Drawing.Color.CadetBlue;
                    btntab3.BackColor = System.Drawing.Color.Red;

                    hidtab2CampaignCategory.Value = hidcampaigncategorycode.Value;

                }

                LoadgvOrder(L_orderdata2);

                if (hidtab2CampaignCategoryname.Value == "")
                {
                    hidtab2CampaignCategoryname.Value = hidcampaigncategoryname.Value;
                }

                lblCampaignCategory.Text = hidtab2CampaignCategoryname.Value;
            }
            else if (hidtab.Value == "3")
            {
                if ((hidcampaigncategorycode.Value == hidtab3CampaignCategory.Value) || (hidtab3CampaignCategory.Value == "")) // check tab 3 เป็น campaign category เดียวกับที่เลือก  //แทป3ยังว่าง
                {
                    if (L_orderdata3.Count > 0) //ถ้ามีข้อมูลที่แทป 3 อยู่แล้ว
                    {
                        lorderdata = L_orderdata3;
                    }


                    odata.runningNo = lorderdata.Count + 1;
                    lorderdata.Add(odata);


                    L_orderdata3 = lorderdata;

                    btntab1.BackColor = System.Drawing.Color.Red;
                    btntab2.BackColor = System.Drawing.Color.Red;
                    btntab3.BackColor = System.Drawing.Color.CadetBlue;

                    hidtab3CampaignCategory.Value = hidcampaigncategorycode.Value;

                }

                LoadgvOrder(L_orderdata3);

                if (hidtab3CampaignCategoryname.Value == "")
                {
                    hidtab3CampaignCategoryname.Value = hidcampaigncategoryname.Value;
                }

                lblCampaignCategory.Text = hidtab3CampaignCategoryname.Value;
            }


        }

        public void LoadOrderdatafromTopProduct(OrderData odata)
        {

            List<OrderData> lorderdata = new List<OrderData>();

            if ((hidtab.Value == "1") || (hidtab.Value == ""))
            {
                if ((hidtab1CampaignCategory.Value == "") || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop01.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop02.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop03.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop04.Value) || (hidtab1CampaignCategory.Value == hidCampaignCategoryCodeProductTop05.Value)) // check tab 1 เป็น campaign category เดียวกับที่เลือก  //แทป1ยังว่าง
                {
                    if (L_orderdata1.Count > 0) //ถ้ามีข้อมูลที่แทป 1 อยู่แล้ว
                    {
                        lorderdata = L_orderdata1;
                    }


                    odata.runningNo = lorderdata.Count + 1;
                    lorderdata.Add(odata);

                    L_orderdata1 = lorderdata;

                    btntab1.BackColor = System.Drawing.Color.CadetBlue;
                    btntab2.BackColor = System.Drawing.Color.Red;
                    btntab3.BackColor = System.Drawing.Color.Red;

                    

                    hidtab.Value = "1";
                }

                LoadgvOrder(L_orderdata1);

                if (hidtab1CampaignCategoryname.Value == "")
                {
                    hidtab1CampaignCategoryname.Value = hidcampaigncategoryname.Value;
                }

                lblCampaignCategory.Text = hidtab1CampaignCategoryname.Value;
            }
            else if (hidtab.Value == "2")
            {
                if ((hidtab2CampaignCategory.Value == "") || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop01.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop02.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop03.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop04.Value) || (hidtab2CampaignCategory.Value == hidCampaignCategoryCodeProductTop05.Value)) // check tab 2 เป็น campaign category เดียวกับที่เลือก  //แทป2ยังว่าง
                {
                    if (L_orderdata2.Count > 0) //ถ้ามีข้อมูลที่แทป 2 อยู่แล้ว
                    {
                        lorderdata = L_orderdata2;
                    }


                    odata.runningNo = lorderdata.Count + 1;
                    lorderdata.Add(odata);


                    L_orderdata2 = lorderdata;

                    btntab1.BackColor = System.Drawing.Color.Red;
                    btntab2.BackColor = System.Drawing.Color.CadetBlue;
                    btntab3.BackColor = System.Drawing.Color.Red;

                    

                }

                LoadgvOrder(L_orderdata2);

                if (hidtab2CampaignCategoryname.Value == "")
                {
                    hidtab2CampaignCategoryname.Value = hidcampaigncategoryname.Value;
                }

                lblCampaignCategory.Text = hidtab2CampaignCategoryname.Value;
            }
            else if (hidtab.Value == "3")
            {
                if ((hidtab3CampaignCategory.Value == "") || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop01.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop02.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop03.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop04.Value) || (hidtab3CampaignCategory.Value == hidCampaignCategoryCodeProductTop05.Value)) // check tab 3 เป็น campaign category เดียวกับที่เลือก  //แทป3ยังว่าง
                {
                    if (L_orderdata3.Count > 0) //ถ้ามีข้อมูลที่แทป 3 อยู่แล้ว
                    {
                        lorderdata = L_orderdata3;
                    }


                    odata.runningNo = lorderdata.Count + 1;
                    lorderdata.Add(odata);


                    L_orderdata3 = lorderdata;

                    btntab1.BackColor = System.Drawing.Color.Red;
                    btntab2.BackColor = System.Drawing.Color.Red;
                    btntab3.BackColor = System.Drawing.Color.CadetBlue;

                    

                }

                LoadgvOrder(L_orderdata3);

                if (hidtab3CampaignCategoryname.Value == "")
                {
                    hidtab3CampaignCategoryname.Value = hidcampaigncategoryname.Value;
                }

                lblCampaignCategory.Text = hidtab3CampaignCategoryname.Value;
            }


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


        protected List<OrderData> LoadOrderbyList(List<OrderData> Lorderdata, Label lbltotal)
        {
            List<OrderData> lorderdata = new List<OrderData>();

            int i = 1;
            foreach (var item in Lorderdata)
            {
                OrderData odata = new OrderData();

                odata.runningNo = i;
                i++;

                odata.Amount = item.Amount;
                odata.CampaignCategory = item.CampaignCategory;
                odata.CampaignCategoryName = item.CampaignCategoryName;
                odata.CampaignCode = item.CampaignCode;
                odata.PromotionCode = item.PromotionCode;
                odata.ProductCode = item.ProductCode;
                odata.PromotionDetailId = item.PromotionDetailId;
                odata.ProductName = item.ProductName;
                odata.FlagCombo = item.FlagCombo;
                odata.ColorCode = item.ColorCode;
                odata.ParentProductCode = item.ParentProductCode;
                odata.ComboCode = item.ComboCode;
                odata.ComboName = item.ComboName;
                odata.DiscountAmount = (item.DiscountAmount != null) ? Convert.ToInt32(item.DiscountAmount) : 0;
                odata.DiscountPercent = (item.DiscountPercent != null) ? Convert.ToInt32(item.DiscountPercent) : 0;
                odata.Price = (item.Price != null) ? Convert.ToDouble(item.Price) : -99;
                odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;
                lbltotal.Text = string.Format("{0:n}", (Convert.ToDecimal(odata.SumPrice) * odata.Amount));
                lorderdata.Add(odata);
            }

            return lorderdata;
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
                odata.ColorCode = item.ColorCode;
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

        protected void Loadtotal(List<OrderData> lorderdata)
        {

            double? totalsumprice = 0;
            int? totalamount = 0;
            int? totalcou_discount = 0;
            double? totalsumdiscount = 0;
            double? totalbefore = 0;
            double? totalafter = 0;
            double? vatprice = 0;
            double? voucherprice = 0;
            double? transportprice = Convert.ToInt32(txtTransportPrice.Text);
            foreach (var item in lorderdata)
            {

                totalsumprice += ((item.Price - ((item.Price * item.DiscountPercent) / 100)) - item.DiscountAmount) * item.Amount;
                totalamount += item.Amount;

                
            }
            totalbefore = totalsumprice - voucherprice;
            vatprice = (totalbefore * 7) / 100;

            

            totalafter = totalbefore + vatprice + transportprice;

            lblTotalAmount.Text = totalamount.ToString();
            lblTotalPrice.Text = string.Format("{0:n}", (totalsumprice));
            lblTotalDiscount_Amount.Text = totalcou_discount.ToString();
            lblTotalDiscount_Price.Text = string.Format("{0:n}", (totalsumdiscount));
            
            lblBeforeTotal_Vat.Text = string.Format("{0:n}", (totalbefore));
            
            lbl_VatPrice.Text = string.Format("{0:n}", (vatprice));
            lblAfterTotal_Vat.Text = string.Format("{0:n}", (totalafter));

            txtTotalPrice.Text = lblAfterTotal_Vat.Text;

            
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


        protected void LoadSubMainPromotionDetail(string promotiondetailid)
        {
            List<SubPromotionDetailInfo> lsubpromotiondetailInfo = new List<SubPromotionDetailInfo>();

            lsubpromotiondetailInfo = GetSubMainPromotionDetailByCriteria(promotiondetailid);

            gvSubMainPromotionDetail.DataSource = lsubpromotiondetailInfo;

            gvSubMainPromotionDetail.DataBind();
        }
        protected void LoadSubExchangePromotionDetail(string promotiondetailid)
        {
            List<SubPromotionDetailInfo> lsubpromotiondetailInfo = new List<SubPromotionDetailInfo>();

            lsubpromotiondetailInfo = GetSubExchangePromotionDetailByCriteria(promotiondetailid);

            gvSubExchangePromotionDetail.DataSource = lsubpromotiondetailInfo;

            gvSubExchangePromotionDetail.DataBind();
        }

        protected List<SubPromotionDetailInfo> GetSubMainPromotionDetailByCriteria(string promotiondetailid)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubMainPromotionDetailbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                

                data["PromotionDetailId"] = promotiondetailid; // ** ต้องรับค่าจาก API ListProductByCriteria รอทำเพิ่ม

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubPromotionDetailInfo> lsubmainpromotiondetail = JsonConvert.DeserializeObject<List<SubPromotionDetailInfo>>(respstr); // List ของ Main Product Gridview


            return lsubmainpromotiondetail;
        }

        protected List<SubPromotionDetailInfo> GetSubExchangePromotionDetailByCriteria(string promotiondetailid)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubExchangePromotionDetailbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                

                data["PromotionDetailId"] = promotiondetailid; // ** ต้องรับค่าจาก API ListProductByCriteria รอทำเพิ่ม

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubPromotionDetailInfo> lsubmainpromotiondetail = JsonConvert.DeserializeObject<List<SubPromotionDetailInfo>>(respstr); // List ของ Main Product Gridview


            return lsubmainpromotiondetail;
        }


        protected List<BranchInfo> Get3NearestBranch(double addrLat, double addrLng, string AreaCode)
        {

            List<BranchInfo> lBranch = LoadBranchList(AreaCode);

            foreach (var i in lBranch)
            {
                var dis = GetDistanceFromLatLonInKm(addrLat, addrLng, Convert.ToDouble(i.Lat), Convert.ToDouble(i.Long));
                i.Distance = dis;
            }

            var sortedBranch = lBranch.OrderBy(x => x.Distance).ToList();
            List<BranchInfo> top3Branch = new List<BranchInfo>();

            for (int i = 0; i < 3; i++)
            {
                top3Branch.Add(sortedBranch[i]);
            }

            return top3Branch;
        }

        double GetDistanceFromLatLonInKm(double lat1,
                         double lon1,
                         double lat2,
                         double lon2)
        {
            var R = 6371d; // Radius of the earth in km
            var dLat = ConvertDeg2Rad(lat2 - lat1);  // deg2rad below
            var dLon = ConvertDeg2Rad(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2d) * Math.Sin(dLat / 2d) +
              Math.Cos(ConvertDeg2Rad(lat1)) * Math.Cos(ConvertDeg2Rad(lat2)) *
              Math.Sin(dLon / 2d) * Math.Sin(dLon / 2d);
            var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
            var distance = R * c; // Distance in km
            return distance;
        }

        double ConvertDeg2Rad(double deg)
        {
            return deg * (Math.PI / 180d);
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

            List<CustomerPhoneInfo> lcusphoneInfo = new List<CustomerPhoneInfo>();
            lcusphoneInfo = GetCustomerPhone(cuscode);
            if (lcusphoneInfo.Count > 0)
            {
                if (lcusphoneInfo[0].PhoneNumber != null || lcusphoneInfo[0].PhoneNumber != "")
                {
                    lblCustomerPhone1.Text = lcusphoneInfo[0].PhoneNumber;
                    
                }
                else
                {
                    lblCustomerPhone1.Text = "";
                }
            }
        }

        public List<CustomerInfo> GetCustomerNoPaging(String cuscode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = cuscode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerInfo> lCustomerInfo = JsonConvert.DeserializeObject<List<CustomerInfo>>(respstr);

            return lCustomerInfo;
        }

        protected List<CustomerPhoneInfo> GetCustomerPhone(String cuscode)
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

            return lCustomerPhoneInfo;
        }


        public bool IsDouble(string text)
        {
            Double num = 0;
            bool isDouble = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDouble = Double.TryParse(text, out num);

            return isDouble;
        }

        protected void UsrCtrl_SelectBranch_Click(object sender, EventArgs e)
        {
            Button btnClick = (Button)sender;

            CheckBox chkAddress = (CheckBox)SelectBranch.FindControl("chkAddress");

            HiddenField hidCustomerLat = (HiddenField)SelectBranch.FindControl("hidCustomerLat");
            HiddenField hidCustomerLng = (HiddenField)SelectBranch.FindControl("hidCustomerLng");
            HiddenField hidNearLat = (HiddenField)SelectBranch.FindControl("hidNearLat");
            HiddenField hidNearLng = (HiddenField)SelectBranch.FindControl("hidNearLng");
            HiddenField hidAreaCode = (HiddenField)SelectBranch.FindControl("hidAreaCode");

            HiddenField currentDeliveryAddressId = (HiddenField)SelectBranch.FindControl("currentDeliveryAddressId");
            HiddenField currentDeliveryAddress = (HiddenField)SelectBranch.FindControl("currentDeliveryAddress");
            HiddenField currentDeliverySubDistrict = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrict");
            HiddenField currentDeliverySubDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrictName");
            HiddenField currentDeliveryDistrict = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrict");
            HiddenField currentDeliveryDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrictName");
            HiddenField currentDeliveryProvince = (HiddenField)SelectBranch.FindControl("currentDeliveryProvince");
            HiddenField currentDeliveryProvinceName = (HiddenField)SelectBranch.FindControl("currentDeliveryProvinceName");
            HiddenField currentDeliveryZipCode = (HiddenField)SelectBranch.FindControl("currentDeliveryZipCode");

            HiddenField currentReceiptAddressId = (HiddenField)SelectBranch.FindControl("currentReceiptAddressId");
            HiddenField currentReceiptAddress = (HiddenField)SelectBranch.FindControl("currentReceiptAddress");
            HiddenField currentReceiptSubDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrict");
            HiddenField currentReceiptSubDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrictName");
            HiddenField currentReceiptDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptDistrict");
            HiddenField currentReceiptDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptDistrictName");
            HiddenField currentReceiptProvince = (HiddenField)SelectBranch.FindControl("currentReceiptProvince");
            HiddenField currentReceiptProvinceName = (HiddenField)SelectBranch.FindControl("currentReceiptProvinceName");
            HiddenField currentReceiptZipCode = (HiddenField)SelectBranch.FindControl("currentReceiptZipCode");

            List<BranchInfo> nearbyBranch1 = new List<BranchInfo>();
            List<BranchInfo> nearbyBranch2 = new List<BranchInfo>();
            List<BranchInfo> nearbyBranch3 = new List<BranchInfo>();

            if(hidNearLat.Value == "" || hidNearLng.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกพื้นที่ใกล้เคียง')", true);
            }
            else
            {
                CustomerAddressInfo cInfo = new CustomerAddressInfo();
                cInfo.CustomerAddressId = Convert.ToInt32(currentDeliveryAddressId.Value);
                cInfo.Lat = hidCustomerLat.Value;
                cInfo.Long = hidCustomerLng.Value;
                cInfo.AreaCode = hidAreaCode.Value;

                UpdateCustomerAddressLatLng(cInfo);

                if (hidtab.Value == "1")
                {
                    nearbyBranch1 = Get3NearestBranch(Convert.ToDouble(hidCustomerLat.Value), Convert.ToDouble(hidCustomerLng.Value),hidAreaCode.Value);

                    hidLandmarkLat1.Value = hidNearLat.Value;
                    hidLandmarkLng1.Value = hidNearLng.Value;

                    if (L_transportdata1.Count == 0)
                    {
                        L_transportdata1.Add(new transportdataInfo
                        {
                            AddressId = currentDeliveryAddressId.Value,
                            AddressType = StaticField.AddressTypeCode01, 
                            Address = currentDeliveryAddress.Value,
                            SubDistrictCode = currentDeliverySubDistrict.Value,
                            SubDistrictName = currentDeliverySubDistrictName.Value,
                            DistrictCode = currentDeliveryDistrict.Value,
                            DistrictName = currentDeliveryDistrictName.Value,
                            ProvinceCode = currentDeliveryProvince.Value,
                            ProvinceName = currentDeliveryProvinceName.Value,
                            Zipcode = currentDeliveryZipCode.Value,
                            NearestBranchList = nearbyBranch1,


                        });

                        if(chkAddress.Checked == true)
                        {
                            L_transportdata1.Add(new transportdataInfo
                            {
                                AddressId = currentDeliveryAddressId.Value,
                                AddressType = StaticField.AddressTypeCode02, 
                                Address = currentDeliveryAddress.Value,
                                SubDistrictCode = currentDeliverySubDistrict.Value,
                                SubDistrictName = currentDeliverySubDistrictName.Value,
                                DistrictCode = currentDeliveryDistrict.Value,
                                DistrictName = currentDeliveryDistrictName.Value,
                                ProvinceCode = currentDeliveryProvince.Value,
                                ProvinceName = currentDeliveryProvinceName.Value,
                                Zipcode = currentDeliveryZipCode.Value,
                                NearestBranchList = nearbyBranch1,
                            });
                        }
                        else
                        {
                            L_transportdata1.Add(new transportdataInfo
                            {
                                AddressId = currentReceiptAddressId.Value,
                                AddressType = StaticField.AddressTypeCode02, 
                                Address = currentReceiptAddress.Value,
                                SubDistrictCode = currentReceiptSubDistrict.Value,
                                SubDistrictName = currentReceiptSubDistrictName.Value,
                                DistrictCode = currentReceiptDistrict.Value,
                                DistrictName = currentReceiptDistrictName.Value,
                                ProvinceCode = currentReceiptProvince.Value,
                                ProvinceName = currentReceiptProvinceName.Value,
                                Zipcode = currentReceiptZipCode.Value,
                                NearestBranchList = nearbyBranch1,
                            });
                        }

                    }
                    else
                    {
                        L_transportdata1[0].AddressId = currentDeliveryAddressId.Value;
                        L_transportdata1[0].Address = currentDeliveryAddress.Value;
                        L_transportdata1[0].SubDistrictCode = currentDeliverySubDistrict.Value;
                        L_transportdata1[0].SubDistrictName = currentDeliverySubDistrictName.Value;
                        L_transportdata1[0].DistrictCode = currentDeliveryDistrict.Value;
                        L_transportdata1[0].DistrictName = currentDeliveryDistrictName.Value;
                        L_transportdata1[0].ProvinceCode = currentDeliveryProvince.Value;
                        L_transportdata1[0].ProvinceName = currentDeliveryProvinceName.Value;
                        L_transportdata1[0].Zipcode = currentDeliveryZipCode.Value;
                        L_transportdata1[0].NearestBranchList = nearbyBranch1;

                        if(chkAddress.Checked == true)
                        {
                            L_transportdata1[1].AddressId = currentDeliveryAddressId.Value;
                            L_transportdata1[1].Address = currentDeliveryAddress.Value;
                            L_transportdata1[1].SubDistrictCode = currentDeliverySubDistrict.Value;
                            L_transportdata1[1].SubDistrictName = currentDeliverySubDistrictName.Value;
                            L_transportdata1[1].DistrictCode = currentDeliveryDistrict.Value;
                            L_transportdata1[1].DistrictName = currentDeliveryDistrictName.Value;
                            L_transportdata1[1].ProvinceCode = currentDeliveryProvince.Value;
                            L_transportdata1[1].ProvinceName = currentDeliveryProvinceName.Value;
                            L_transportdata1[1].Zipcode = currentDeliveryZipCode.Value;
                            L_transportdata1[1].NearestBranchList = nearbyBranch1;
                        }
                        else
                        {
                            L_transportdata1[1].AddressId = currentReceiptAddressId.Value;
                            L_transportdata1[1].Address = currentReceiptAddress.Value;
                            L_transportdata1[1].SubDistrictCode = currentReceiptSubDistrict.Value;
                            L_transportdata1[1].SubDistrictName = currentReceiptSubDistrictName.Value;
                            L_transportdata1[1].DistrictCode = currentReceiptDistrict.Value;
                            L_transportdata1[1].DistrictName = currentReceiptDistrictName.Value;
                            L_transportdata1[1].ProvinceCode = currentReceiptProvince.Value;
                            L_transportdata1[1].ProvinceName = currentReceiptProvinceName.Value;
                            L_transportdata1[1].Zipcode = currentReceiptZipCode.Value;
                            L_transportdata1[1].NearestBranchList = nearbyBranch1;
                        }
   
                    }

                    lblCustomerAddress.Text = L_transportdata1[0].Address + " " + L_transportdata1[0].SubDistrictName
                    + " " + L_transportdata1[0].DistrictName + " " + L_transportdata1[0].ProvinceName + " " + L_transportdata1[0].Zipcode;


                    dtlNearestBranch.DataSource = nearbyBranch1;
                    dtlNearestBranch.DataBind();


                }
                else if (hidtab.Value == "2")
                {
                    nearbyBranch2 = Get3NearestBranch(Convert.ToDouble(hidCustomerLat.Value), Convert.ToDouble(hidCustomerLng.Value), hidAreaCode.Value);

                    hidLandmarkLat2.Value = hidNearLat.Value;
                    hidLandmarkLng2.Value = hidNearLng.Value;


                    if (L_transportdata2.Count == 0)
                    {
                        L_transportdata2.Add(new transportdataInfo
                        {
                            AddressId = currentDeliveryAddressId.Value,
                            AddressType = StaticField.AddressTypeCode01, 
                            Address = currentDeliveryAddress.Value,
                            SubDistrictCode = currentDeliverySubDistrict.Value,
                            SubDistrictName = currentDeliverySubDistrictName.Value,
                            DistrictCode = currentDeliveryDistrict.Value,
                            DistrictName = currentDeliveryDistrictName.Value,
                            ProvinceCode = currentDeliveryProvince.Value,
                            ProvinceName = currentDeliveryProvinceName.Value,
                            Zipcode = currentDeliveryZipCode.Value,
                            NearestBranchList = nearbyBranch2,


                        });

                        if (chkAddress.Checked == true)
                        {
                            L_transportdata2.Add(new transportdataInfo
                            {
                                AddressId = currentDeliveryAddressId.Value,
                                AddressType = StaticField.AddressTypeCode02, 
                                Address = currentDeliveryAddress.Value,
                                SubDistrictCode = currentDeliverySubDistrict.Value,
                                SubDistrictName = currentDeliverySubDistrictName.Value,
                                DistrictCode = currentDeliveryDistrict.Value,
                                DistrictName = currentDeliveryDistrictName.Value,
                                ProvinceCode = currentDeliveryProvince.Value,
                                ProvinceName = currentDeliveryProvinceName.Value,
                                Zipcode = currentDeliveryZipCode.Value,
                                NearestBranchList = nearbyBranch2,
                            });
                        }
                        else
                        {
                            L_transportdata2.Add(new transportdataInfo
                            {
                                AddressId = currentReceiptAddressId.Value,
                                AddressType = StaticField.AddressTypeCode02, 
                                Address = currentReceiptAddress.Value,
                                SubDistrictCode = currentReceiptSubDistrict.Value,
                                SubDistrictName = currentReceiptSubDistrictName.Value,
                                DistrictCode = currentReceiptDistrict.Value,
                                DistrictName = currentReceiptDistrictName.Value,
                                ProvinceCode = currentReceiptProvince.Value,
                                ProvinceName = currentReceiptProvinceName.Value,
                                Zipcode = currentReceiptZipCode.Value,
                                NearestBranchList = nearbyBranch2,
                            });
                        }

                    }
                    else
                    {
                        L_transportdata2[0].AddressId = currentDeliveryAddressId.Value;
                        L_transportdata2[0].Address = currentDeliveryAddress.Value;
                        L_transportdata2[0].SubDistrictCode = currentDeliverySubDistrict.Value;
                        L_transportdata2[0].SubDistrictName = currentDeliverySubDistrictName.Value;
                        L_transportdata2[0].DistrictCode = currentDeliveryDistrict.Value;
                        L_transportdata2[0].DistrictName = currentDeliveryDistrictName.Value;
                        L_transportdata2[0].ProvinceCode = currentDeliveryProvince.Value;
                        L_transportdata2[0].ProvinceName = currentDeliveryProvinceName.Value;
                        L_transportdata2[0].Zipcode = currentDeliveryZipCode.Value;
                        L_transportdata2[0].NearestBranchList = nearbyBranch2;

                        if (chkAddress.Checked == true)
                        {
                            L_transportdata2[1].AddressId = currentDeliveryAddressId.Value;
                            L_transportdata2[1].Address = currentDeliveryAddress.Value;
                            L_transportdata2[1].SubDistrictCode = currentDeliverySubDistrict.Value;
                            L_transportdata2[1].SubDistrictName = currentDeliverySubDistrictName.Value;
                            L_transportdata2[1].DistrictCode = currentDeliveryDistrict.Value;
                            L_transportdata2[1].DistrictName = currentDeliveryDistrictName.Value;
                            L_transportdata2[1].ProvinceCode = currentDeliveryProvince.Value;
                            L_transportdata2[1].ProvinceName = currentDeliveryProvinceName.Value;
                            L_transportdata2[1].Zipcode = currentDeliveryZipCode.Value;
                            L_transportdata2[1].NearestBranchList = nearbyBranch2;
                        }
                        else
                        {
                            L_transportdata2[1].AddressId = currentReceiptAddressId.Value;
                            L_transportdata2[1].Address = currentReceiptAddress.Value;
                            L_transportdata2[1].SubDistrictCode = currentReceiptSubDistrict.Value;
                            L_transportdata2[1].SubDistrictName = currentReceiptSubDistrictName.Value;
                            L_transportdata2[1].DistrictCode = currentReceiptDistrict.Value;
                            L_transportdata2[1].DistrictName = currentReceiptDistrictName.Value;
                            L_transportdata2[1].ProvinceCode = currentReceiptProvince.Value;
                            L_transportdata2[1].ProvinceName = currentReceiptProvinceName.Value;
                            L_transportdata2[1].Zipcode = currentReceiptZipCode.Value;
                            L_transportdata2[1].NearestBranchList = nearbyBranch2;
                        }

                    }

                    lblCustomerAddress.Text = L_transportdata2[0].Address + " " + L_transportdata2[0].SubDistrictName
                    + " " + L_transportdata2[0].DistrictName + " " + L_transportdata2[0].ProvinceName + " " + L_transportdata2[0].Zipcode;

                    dtlNearestBranch.DataSource = nearbyBranch2;
                    dtlNearestBranch.DataBind();
                }
                else if (hidtab.Value == "3")
                {
                    nearbyBranch3 = Get3NearestBranch(Convert.ToDouble(hidCustomerLat.Value), Convert.ToDouble(hidCustomerLng.Value), hidAreaCode.Value);

                    hidLandmarkLat3.Value = hidNearLat.Value;
                    hidLandmarkLng3.Value = hidNearLng.Value;

                    if (L_transportdata3.Count == 0)
                    {
                        L_transportdata3.Add(new transportdataInfo
                        {
                            AddressId = currentDeliveryAddressId.Value,
                            AddressType = StaticField.AddressTypeCode01, 
                            Address = currentDeliveryAddress.Value,
                            SubDistrictCode = currentDeliverySubDistrict.Value,
                            SubDistrictName = currentDeliverySubDistrictName.Value,
                            DistrictCode = currentDeliveryDistrict.Value,
                            DistrictName = currentDeliveryDistrictName.Value,
                            ProvinceCode = currentDeliveryProvince.Value,
                            ProvinceName = currentDeliveryProvinceName.Value,
                            Zipcode = currentDeliveryZipCode.Value,
                            NearestBranchList = nearbyBranch1,


                        });

                        if (chkAddress.Checked == true)
                        {
                            L_transportdata3.Add(new transportdataInfo
                            {
                                AddressId = currentDeliveryAddressId.Value,
                                AddressType = StaticField.AddressTypeCode02, 
                                Address = currentDeliveryAddress.Value,
                                SubDistrictCode = currentDeliverySubDistrict.Value,
                                SubDistrictName = currentDeliverySubDistrictName.Value,
                                DistrictCode = currentDeliveryDistrict.Value,
                                DistrictName = currentDeliveryDistrictName.Value,
                                ProvinceCode = currentDeliveryProvince.Value,
                                ProvinceName = currentDeliveryProvinceName.Value,
                                Zipcode = currentDeliveryZipCode.Value,
                                NearestBranchList = nearbyBranch1,
                            });
                        }
                        else
                        {
                            L_transportdata3.Add(new transportdataInfo
                            {
                                AddressId = currentReceiptAddressId.Value,
                                AddressType = "02",
                                Address = currentReceiptAddress.Value,
                                SubDistrictCode = currentReceiptSubDistrict.Value,
                                SubDistrictName = currentReceiptSubDistrictName.Value,
                                DistrictCode = currentReceiptDistrict.Value,
                                DistrictName = currentReceiptDistrictName.Value,
                                ProvinceCode = currentReceiptProvince.Value,
                                ProvinceName = currentReceiptProvinceName.Value,
                                Zipcode = currentReceiptZipCode.Value,
                                NearestBranchList = nearbyBranch1,
                            });
                        }

                    }
                    else
                    {
                        L_transportdata3[0].AddressId = currentDeliveryAddressId.Value;
                        L_transportdata3[0].Address = currentDeliveryAddress.Value;
                        L_transportdata3[0].SubDistrictCode = currentDeliverySubDistrict.Value;
                        L_transportdata3[0].SubDistrictName = currentDeliverySubDistrictName.Value;
                        L_transportdata3[0].DistrictCode = currentDeliveryDistrict.Value;
                        L_transportdata3[0].DistrictName = currentDeliveryDistrictName.Value;
                        L_transportdata3[0].ProvinceCode = currentDeliveryProvince.Value;
                        L_transportdata3[0].ProvinceName = currentDeliveryProvinceName.Value;
                        L_transportdata3[0].Zipcode = currentDeliveryZipCode.Value;
                        L_transportdata3[0].NearestBranchList = nearbyBranch3;

                        if (chkAddress.Checked == true)
                        {
                            L_transportdata3[1].AddressId = currentDeliveryAddressId.Value;
                            L_transportdata3[1].Address = currentDeliveryAddress.Value;
                            L_transportdata3[1].SubDistrictCode = currentDeliverySubDistrict.Value;
                            L_transportdata3[1].SubDistrictName = currentDeliverySubDistrictName.Value;
                            L_transportdata3[1].DistrictCode = currentDeliveryDistrict.Value;
                            L_transportdata3[1].DistrictName = currentDeliveryDistrictName.Value;
                            L_transportdata3[1].ProvinceCode = currentDeliveryProvince.Value;
                            L_transportdata3[1].ProvinceName = currentDeliveryProvinceName.Value;
                            L_transportdata3[1].Zipcode = currentDeliveryZipCode.Value;
                            L_transportdata3[1].NearestBranchList = nearbyBranch3;
                        }
                        else
                        {
                            L_transportdata3[1].AddressId = currentReceiptAddressId.Value;
                            L_transportdata3[1].Address = currentReceiptAddress.Value;
                            L_transportdata3[1].SubDistrictCode = currentReceiptSubDistrict.Value;
                            L_transportdata3[1].SubDistrictName = currentReceiptSubDistrictName.Value;
                            L_transportdata3[1].DistrictCode = currentReceiptDistrict.Value;
                            L_transportdata3[1].DistrictName = currentReceiptDistrictName.Value;
                            L_transportdata3[1].ProvinceCode = currentReceiptProvince.Value;
                            L_transportdata3[1].ProvinceName = currentReceiptProvinceName.Value;
                            L_transportdata3[1].Zipcode = currentReceiptZipCode.Value;
                            L_transportdata3[1].NearestBranchList = nearbyBranch3;
                        }

                    }

                    lblCustomerAddress.Text = L_transportdata3[0].Address + " " + L_transportdata3[0].SubDistrictName
                    + " " + L_transportdata3[0].DistrictName + " " + L_transportdata3[0].ProvinceName + " " + L_transportdata3[0].Zipcode;


                    dtlNearestBranch.DataSource = nearbyBranch3;
                    dtlNearestBranch.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, Page.GetType(), "function", "displayNonSelected()", true);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "$('#modal-address').modal('hide');", true);
            }

         

        }

        protected void CheckProductMapBranch(string BranchCode)
        {
            BranchMapProductInfo binfo = new BranchMapProductInfo();

            List<OrderData> lorderdata = new List<OrderData>();

            OrderData odata;

            string productcodelist = "";
            
            string parentproductcode = "";

            int? cou = 0;

            if (hidtab.Value == "1")
            {
                foreach(var item in L_orderdata1)
                {
                    binfo.ProductCode = item.ProductCode;
                    binfo.BranchCode = BranchCode;

                    cou =  LoadBranchMapProduct(binfo);

                    odata = new OrderData();

                    odata.ColorCode = (cou > 0) ? "#333333" : "red";

                    odata.runningNo = item.runningNo;

                        odata.Amount =item.Amount;
                        
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

                    lorderdata.Add(odata);

                    }

                L_orderdata1 = lorderdata;

               
            }
            else if (hidtab.Value == "2")
            {
                foreach (var item in L_orderdata2)
                {
                    binfo.ProductCode = item.ProductCode;
                    binfo.BranchCode = BranchCode;
                    cou = LoadBranchMapProduct(binfo);

                    odata = new OrderData();

                    odata.ColorCode = (cou > 0) ? "#333333" : "red";

                    odata.runningNo = item.runningNo;

                    odata.Amount = item.Amount;

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

                    lorderdata.Add(odata);

                }

                L_orderdata2 = lorderdata;

                
            }
            else if (hidtab.Value == "3")
            {
                foreach (var item in L_orderdata3)
                {
                    binfo.ProductCode = item.ProductCode;
                    binfo.BranchCode = BranchCode;
                    cou = LoadBranchMapProduct(binfo);

                    odata = new OrderData();

                    odata.ColorCode = (cou > 0) ? "#333333" : "red";

                    odata.runningNo = item.runningNo;

                    odata.Amount = item.Amount;

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

                    lorderdata.Add(odata);

                }

                L_orderdata3 = lorderdata;

              
            }

            LoadgvOrder(lorderdata);

            
        }

        protected void ClickRadioBranch()
        {
            int count = dtlNearestBranch.Items.Count;

            if (hidtab.Value == "1")
            {
                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    HiddenField bName = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchName");
                    if (hidSelectedBranchCode1.Value != "" && hidSelectedBranchCode1.Value == bCode.Value)
                    {
                        CheckProductMapBranch(bCode.Value);
                        hidSelectedBranchName1.Value = bName.Value;
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }
            }else if (hidtab.Value == "2")
            {
                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode2.Value != "" && hidSelectedBranchCode2.Value == bCode.Value)
                    {
                        CheckProductMapBranch(bCode.Value);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }
            }
            else if (hidtab.Value == "3")
            {
                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode3.Value != "" && hidSelectedBranchCode3.Value == bCode.Value)
                    {
                        CheckProductMapBranch(bCode.Value);
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }
            }
        }
        protected int? LoadBranchMapProduct(BranchMapProductInfo binfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListBranchMapProductByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = binfo.ProductCode;
                data["BranchCode"] = binfo.BranchCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? sum = JsonConvert.DeserializeObject<int?>(respstr);

            return sum;
        }

        protected void LoadNoteProfile(String customerCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = customerCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerInfo> lcInfo = JsonConvert.DeserializeObject<List<CustomerInfo>>(respstr);
            if(lcInfo.Count > 0)
            {
                txtNoteProfile.InnerText = lcInfo[0].Note;
            }
        }

        protected void LoadOrderNote(String customerCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCustomerOrderNoPagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = customerCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<CustomerOrderInfo> lcInfo = JsonConvert.DeserializeObject<List<CustomerOrderInfo>>(respstr);
            if (lcInfo.Count > 0)
            {
                txtOrderNote.InnerText = lcInfo[0].OrderNote;
            }
        }

        protected void UpdateCustomerNoteProfile(String customerCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateCustomerNoteProfile";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = customerCode;
                data["Note"] = txtNoteProfile.InnerText;
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int? sum = JsonConvert.DeserializeObject<int?>(respstr);
        }

        protected List<PromotionDetailInfo> GetProductRecipefromCampaignProduct(String campaigncode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionDetailRecipeinCampaignProductTakeOrderCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = campaigncode;
                data["RecipeName"] = txtSearchRecipe.Text.Trim();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lpdInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);
            return lpdInfo;
        }

        protected void BindTop5Product(string campaigncategorycode)
        {
            string customercode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";

            string respstr = "";
            APIpath = APIUrl + "/api/support/ListTop5ProductodOrderCustomerByCriteria";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["CustomerCode"] = customercode;
                    data["CampaignCategoryCode"] = campaigncategorycode;
                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }

                List<ProductInfo> lTop5ProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            if (lTop5ProductInfo.Count > 0)
            {
                if (lTop5ProductInfo[0].ProductName != null)
                {
                    lblProductNameTop1.Text = lTop5ProductInfo[0].ProductName;
                    lblProdTop01.Text = "1. " + lTop5ProductInfo[0].ProductName;

                    hidProductCodeProductTop01.Value = lTop5ProductInfo[0].ProductCode;
                    hidPromotionDetailIdProductTop01.Value = (lTop5ProductInfo[0].PromotionDetailId != null) ? lTop5ProductInfo[0].PromotionDetailId.ToString() : "0";
                    hidProductNameProductTop01.Value = lTop5ProductInfo[0].ProductName;
                    hidDiscountAmountProductTop01.Value = (lTop5ProductInfo[0].DiscountAmount != null) ? lTop5ProductInfo[0].DiscountAmount.ToString() : "0";
                    hidDiscountPercentProductTop01.Value = (lTop5ProductInfo[0].DiscountPercent != null) ? lTop5ProductInfo[0].DiscountPercent.ToString() : "0";
                    hidCampaignCodeProductTop01.Value = lTop5ProductInfo[0].CampaignCode;
                    hidPromotionCodeProductTop01.Value = lTop5ProductInfo[0].PromotionCode;
                    hidPriceProductTop01.Value = (lTop5ProductInfo[0].Price != null) ? lTop5ProductInfo[0].Price.ToString() : "0";
                    hidCampaignCategoryCodeProductTop01.Value = lTop5ProductInfo[0].CampaignCategoryCode;
                    hidCampaignCategoryNameProductTop01.Value = lTop5ProductInfo[0].CampaignCategoryName;
                    ico01.Visible = true;
                    
                }
                else
                {
                    lblProductNameTop1.Text = "";
                    hidProductCodeProductTop01.Value = "";
                    hidPromotionDetailIdProductTop01.Value = "";
                    hidProductNameProductTop01.Value = "";
                    hidDiscountAmountProductTop01.Value = "";
                    hidDiscountPercentProductTop01.Value = "";
                    hidCampaignCodeProductTop01.Value = "";
                    hidPromotionCodeProductTop01.Value = "";
                    hidPriceProductTop01.Value = "";
                    hidCampaignCategoryCodeProductTop01.Value = "";
                    hidCampaignCategoryNameProductTop01.Value = "";
                    ico01.Visible = false;
                }
            }
            if (lTop5ProductInfo.Count > 1)
            {
                if (lTop5ProductInfo[1].ProductName != null)
                {
                    lblProductNameTop2.Text = lTop5ProductInfo[1].ProductName;
                    lblProdTop02.Text = "2. " + lTop5ProductInfo[1].ProductName;
                    hidProductCodeProductTop02.Value = lTop5ProductInfo[1].ProductCode;
                    hidPromotionDetailIdProductTop02.Value = (lTop5ProductInfo[1].PromotionDetailId != null) ? lTop5ProductInfo[1].PromotionDetailId.ToString() : "0";
                    hidProductNameProductTop02.Value = lTop5ProductInfo[1].ProductName;
                    hidDiscountAmountProductTop02.Value = (lTop5ProductInfo[1].DiscountAmount != null) ? lTop5ProductInfo[1].DiscountAmount.ToString() : "0";
                    hidDiscountPercentProductTop02.Value = (lTop5ProductInfo[1].DiscountPercent != null) ? lTop5ProductInfo[1].DiscountPercent.ToString() : "0";
                    hidCampaignCodeProductTop02.Value = lTop5ProductInfo[1].CampaignCode;
                    hidPromotionCodeProductTop02.Value = lTop5ProductInfo[1].PromotionCode;
                    hidPriceProductTop02.Value = (lTop5ProductInfo[1].Price != null) ? lTop5ProductInfo[1].Price.ToString() : "0";
                    hidCampaignCategoryCodeProductTop02.Value = lTop5ProductInfo[1].CampaignCategoryCode;
                    hidCampaignCategoryNameProductTop02.Value = lTop5ProductInfo[1].CampaignCategoryName;
                    ico02.Visible = true;
                    
                }
                else
                {
                    lblProductNameTop2.Text = "";
                    hidProductCodeProductTop02.Value = "";
                    hidPromotionDetailIdProductTop02.Value = "";
                    hidProductNameProductTop02.Value = "";
                    hidDiscountAmountProductTop02.Value = "";
                    hidDiscountPercentProductTop02.Value = "";
                    hidCampaignCodeProductTop02.Value = "";
                    hidPromotionCodeProductTop02.Value = "";
                    hidPriceProductTop02.Value = "";
                    hidCampaignCategoryCodeProductTop02.Value = "";
                    hidCampaignCategoryNameProductTop02.Value = "";
                    ico02.Visible = false;
                }
            }
            if (lTop5ProductInfo.Count > 2)
            {
                if (lTop5ProductInfo[2].ProductName != null)
                {
                    lblProductNameTop3.Text = lTop5ProductInfo[2].ProductName;
                    lblProdTop03.Text = "3. " + lTop5ProductInfo[2].ProductName;
                    hidProductCodeProductTop03.Value = lTop5ProductInfo[2].ProductCode;
                    hidPromotionDetailIdProductTop03.Value = (lTop5ProductInfo[2].PromotionDetailId != null) ? lTop5ProductInfo[2].PromotionDetailId.ToString() : "0";
                    hidProductNameProductTop03.Value = lTop5ProductInfo[2].ProductName;
                    hidDiscountAmountProductTop03.Value = (lTop5ProductInfo[2].DiscountAmount != null) ? lTop5ProductInfo[2].DiscountAmount.ToString() : "0";
                    hidDiscountPercentProductTop03.Value = (lTop5ProductInfo[2].DiscountPercent != null) ? lTop5ProductInfo[2].DiscountPercent.ToString() : "0";
                    hidCampaignCodeProductTop03.Value = lTop5ProductInfo[2].CampaignCode;
                    hidPromotionCodeProductTop03.Value = lTop5ProductInfo[2].PromotionCode;
                    hidPriceProductTop03.Value = (lTop5ProductInfo[2].Price != null) ? lTop5ProductInfo[2].Price.ToString() : "0";
                    hidCampaignCategoryCodeProductTop03.Value = lTop5ProductInfo[2].CampaignCategoryCode;
                    hidCampaignCategoryNameProductTop03.Value = lTop5ProductInfo[2].CampaignCategoryName;
                    ico03.Visible = true;
                    
                }
                else
                {
                    lblProductNameTop3.Text = "";
                    hidProductCodeProductTop03.Value = "";
                    hidPromotionDetailIdProductTop03.Value = "";
                    hidProductNameProductTop03.Value = "";
                    hidDiscountAmountProductTop03.Value = "";
                    hidDiscountPercentProductTop03.Value = "";
                    hidCampaignCodeProductTop03.Value = "";
                    hidPromotionCodeProductTop03.Value = "";
                    hidPriceProductTop03.Value = "";
                    hidCampaignCategoryCodeProductTop03.Value = "";
                    hidCampaignCategoryNameProductTop03.Value = "";
                    ico03.Visible = false;
                }
            }
            if (lTop5ProductInfo.Count > 3)
            {
                if (lTop5ProductInfo[3].ProductName != null)
                {
                    lblProductNameTop4.Text = lTop5ProductInfo[3].ProductName;
                    lblProdTop04.Text = "4. " + lTop5ProductInfo[3].ProductName;
                    hidProductCodeProductTop04.Value = lTop5ProductInfo[3].ProductCode;
                    hidPromotionDetailIdProductTop04.Value = (lTop5ProductInfo[3].PromotionDetailId != null) ? lTop5ProductInfo[3].PromotionDetailId.ToString() : "0";
                    hidProductNameProductTop04.Value = lTop5ProductInfo[3].ProductName;
                    hidDiscountAmountProductTop04.Value = (lTop5ProductInfo[3].DiscountAmount != null) ? lTop5ProductInfo[3].DiscountAmount.ToString() : "0";
                    hidDiscountPercentProductTop04.Value = (lTop5ProductInfo[3].DiscountPercent != null) ? lTop5ProductInfo[3].DiscountPercent.ToString() : "0";
                    hidCampaignCodeProductTop04.Value = lTop5ProductInfo[3].CampaignCode;
                    hidPromotionCodeProductTop04.Value = lTop5ProductInfo[3].PromotionCode;
                    hidPriceProductTop04.Value = (lTop5ProductInfo[3].Price != null) ? lTop5ProductInfo[3].Price.ToString() : "0";
                    hidCampaignCategoryCodeProductTop04.Value = lTop5ProductInfo[3].CampaignCategoryCode;
                    hidCampaignCategoryNameProductTop04.Value = lTop5ProductInfo[3].CampaignCategoryName;
                    ico04.Visible = true;
                    
                }
                else
                {
                    lblProductNameTop4.Text = "";
                    hidProductCodeProductTop04.Value = "";
                    hidPromotionDetailIdProductTop04.Value = "";
                    hidProductNameProductTop04.Value = "";
                    hidDiscountAmountProductTop04.Value = "";
                    hidDiscountPercentProductTop04.Value = "";
                    hidCampaignCodeProductTop04.Value = "";
                    hidPromotionCodeProductTop04.Value = "";
                    hidPriceProductTop04.Value = "";
                    hidCampaignCategoryCodeProductTop04.Value = "";
                    hidCampaignCategoryNameProductTop04.Value = "";
                    ico04.Visible = false;
                }
            }
            if (lTop5ProductInfo.Count > 4)
            {
                if (lTop5ProductInfo[4].ProductName != null)
                {
                    lblProductNameTop5.Text = lTop5ProductInfo[4].ProductName;
                    lblProdTop05.Text = "5. " + lTop5ProductInfo[4].ProductName;
                    hidProductCodeProductTop05.Value = lTop5ProductInfo[4].ProductCode;
                    hidPromotionDetailIdProductTop05.Value = (lTop5ProductInfo[4].PromotionDetailId != null) ? lTop5ProductInfo[4].PromotionDetailId.ToString() : "0";
                    hidProductNameProductTop05.Value = lTop5ProductInfo[4].ProductName;
                    hidDiscountAmountProductTop05.Value = (lTop5ProductInfo[4].DiscountAmount != null) ? lTop5ProductInfo[4].DiscountAmount.ToString() : "0";
                    hidDiscountPercentProductTop05.Value = (lTop5ProductInfo[4].DiscountPercent != null) ? lTop5ProductInfo[4].DiscountPercent.ToString() : "0";
                    hidCampaignCodeProductTop05.Value = lTop5ProductInfo[4].CampaignCode;
                    hidPromotionCodeProductTop05.Value = lTop5ProductInfo[4].PromotionCode;
                    hidPriceProductTop05.Value = (lTop5ProductInfo[4].Price != null) ? lTop5ProductInfo[4].Price.ToString() : "0";
                    hidCampaignCategoryCodeProductTop05.Value = lTop5ProductInfo[4].CampaignCategoryCode;
                    hidCampaignCategoryNameProductTop05.Value = lTop5ProductInfo[4].CampaignCategoryName;
                    ico05.Visible = true;
                    
                }
                else
                {
                    lblProductNameTop5.Text = "";
                    hidProductCodeProductTop05.Value = "";
                    hidPromotionDetailIdProductTop05.Value = "";
                    hidProductNameProductTop05.Value = "";
                    hidDiscountAmountProductTop05.Value = "";
                    hidDiscountPercentProductTop05.Value = "";
                    hidCampaignCodeProductTop05.Value = "";
                    hidPromotionCodeProductTop05.Value = "";
                    hidPriceProductTop05.Value = "";
                    hidCampaignCategoryCodeProductTop05.Value = "";
                    hidCampaignCategoryNameProductTop05.Value = "";
                    ico05.Visible = false;
                }
            }

            if (lTop5ProductInfo.Count == 0)
            {
                lblProductNameTop1.Text = "";
                hidProductCodeProductTop01.Value = "";
                lblProductNameTop2.Text = "";
                hidProductCodeProductTop02.Value = "";
                lblProductNameTop3.Text = "";
                hidProductCodeProductTop03.Value = "";
                lblProductNameTop4.Text = "";
                hidProductCodeProductTop04.Value = "";
                lblProductNameTop5.Text = "";
                hidProductCodeProductTop05.Value = "";

                hidPromotionDetailIdProductTop01.Value = "";
                hidProductNameProductTop01.Value = "";
                hidDiscountAmountProductTop01.Value = "";
                hidDiscountPercentProductTop01.Value = "";
                hidCampaignCodeProductTop01.Value = "";
                hidPromotionCodeProductTop01.Value = "";
                hidPriceProductTop01.Value = "";
                hidCampaignCategoryCodeProductTop01.Value = "";
                hidCampaignCategoryNameProductTop01.Value = "";

                hidPromotionDetailIdProductTop02.Value = "";
                hidProductNameProductTop02.Value = "";
                hidDiscountAmountProductTop02.Value = "";
                hidDiscountPercentProductTop02.Value = "";
                hidCampaignCodeProductTop02.Value = "";
                hidPromotionCodeProductTop02.Value = "";
                hidPriceProductTop02.Value = "";
                hidCampaignCategoryCodeProductTop02.Value = "";
                hidCampaignCategoryNameProductTop02.Value = "";

                hidPromotionDetailIdProductTop03.Value = "";
                hidProductNameProductTop03.Value = "";
                hidDiscountAmountProductTop03.Value = "";
                hidDiscountPercentProductTop03.Value = "";
                hidCampaignCodeProductTop03.Value = "";
                hidPromotionCodeProductTop03.Value = "";
                hidPriceProductTop03.Value = "";
                hidCampaignCategoryCodeProductTop03.Value = "";
                hidCampaignCategoryNameProductTop03.Value = "";

                hidPromotionDetailIdProductTop04.Value = "";
                hidProductNameProductTop04.Value = "";
                hidDiscountAmountProductTop04.Value = "";
                hidDiscountPercentProductTop04.Value = "";
                hidCampaignCodeProductTop04.Value = "";
                hidPromotionCodeProductTop04.Value = "";
                hidPriceProductTop04.Value = "";
                hidCampaignCategoryCodeProductTop04.Value = "";
                hidCampaignCategoryNameProductTop04.Value = "";

                hidPromotionDetailIdProductTop05.Value = "";
                hidProductNameProductTop05.Value = "";
                hidDiscountAmountProductTop05.Value = "";
                hidDiscountPercentProductTop05.Value = "";
                hidCampaignCodeProductTop05.Value = "";
                hidPromotionCodeProductTop05.Value = "";
                hidPriceProductTop05.Value = "";
                hidCampaignCategoryCodeProductTop05.Value = "";
                hidCampaignCategoryNameProductTop05.Value = "";

                ico01.Visible = false;
                ico02.Visible = false;
                ico03.Visible = false;
                ico04.Visible = false;
                ico05.Visible = false;
            }
        }

        protected Boolean ValidateSubmitOrder()
        {
            Boolean flag = true;
            string error = "";
            int counterr = 0;

            if(hidSelectedBranchCode1.Value == "" || hidSelectedBranchCode1.Value == null)
            {
                flag = false;
                error += "กรุณาเลือกสาขา \\n";
                counterr++;
            }
            else
            {
                flag = (!flag) ? false : true;

            }
            if (txtcustomerpay.Text == "" || txtcustomerpay.Text == null)
            {
                flag = false;
                error += "กรุณาใส่เงินชำระ \\n";
                counterr++;
            }
            else
            {
                if (IsDouble(txtcustomerpay.Text))
                {
                    Double cashreturn = Convert.ToDouble(txtcustomerpay.Text) - Convert.ToDouble(txtTotalPrice.Text);
                    if (cashreturn >= 0)
                    {
                        flag = (!flag) ? false : true;
                    }
                    else
                    {
                        flag = false;
                        error += "จำนวนเงินชำระไม่เพียงพอ\\n";
                        counterr++;
                    }
                }
                else
                {
                    flag = false;
                    error += "กรุณาใส่เงินชำระ\\n";
                    counterr++;
                }
            }
            
            if (radpreorder.Checked == true)
            {
                if (txtdPreOrder.Value == "" || txtdPreOrder.Value == null)
                {
                    flag = false;
                    error += "กรุณาระบุวัน Pre Order\\n";
                    counterr++;
                }
                if(txtPreOrderHr.Value == "" || txtPreOrderHr.Value == null || txtPreOrderMin.Value == "" || txtPreOrderMin.Value == null)
                {
                    flag = false;
                    error += "กรุณาระบุเวลา Pre Order\\n";
                    counterr++;
                }
            }
            else
            {
                flag = (!flag) ? false : true;

            }

            if (counterr > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + error + "');displayPayment();", true);
                
            }

            return flag;
        }

        protected List<SubPromotionDetailInfo> LoadSubMainPromotionDetailforBindProductDesc(String promotiondetailid)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubMainPromotionDetailbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

               
                data["PromotionDetailId"] = promotiondetailid; // ** ต้องรับค่าจาก API ListProductByCriteria รอทำเพิ่ม

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubPromotionDetailInfo> lsubmainpromotiondetail = JsonConvert.DeserializeObject<List<SubPromotionDetailInfo>>(respstr); // List ของ Main Product Gridview


            return lsubmainpromotiondetail;
        }

        protected List<SubPromotionDetailInfo> LoadSubExchangePromotionDetailforBindProductDesc(String promotiondetailid)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubExchangePromotionDetailbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["PromotionDetailId"] = promotiondetailid; // ** ต้องรับค่าจาก API ListProductByCriteria รอทำเพิ่ม

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubPromotionDetailInfo> lsubmainpromotiondetail = JsonConvert.DeserializeObject<List<SubPromotionDetailInfo>>(respstr); // List ของ Main Product Gridview


            return lsubmainpromotiondetail;
        }

        protected void LoadLatestOrderdata(String customercode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/PromotionDetailfromLatestOrderbyCustomer";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CustomerCode"] = customercode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotionDetailInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);

            OrderData odata = new OrderData();
            List<OrderData> lindata = new List<OrderData>();
            String ordercode = "";
            int count = 0;

            if (lPromotionDetailInfo.Count > 0)
            {
                lblCampaignCategory.Text = lPromotionDetailInfo[0].CampaignCategoryName;
                hidtab.Value = "1";
                hidtab1CampaignCategory.Value = lPromotionDetailInfo[0].CampaignCategoryCode;
                hidcampaigncategorycode.Value = lPromotionDetailInfo[0].CampaignCategoryCode;
                hidtab1CampaignCategoryname.Value = lPromotionDetailInfo[0].CampaignCategoryName;
                BindTop5Product(hidcampaigncategorycode.Value);

                if (lPromotionDetailInfo[0].FlagCombo != "Y")
                {
                    foreach (var pd in lPromotionDetailInfo)
                    {
                        if (pd.OrderCode == odata.OrderCode)
                        {
                            odata.OrderCode = pd.OrderCode;
                            odata.CampaignCategory = pd.CampaignCategoryCode; 
                            odata.CampaignCategoryName = pd.CampaignCategoryName; 
                            odata.CampaignCode = pd.CampaignCode; 
                            odata.PromotionCode = pd.PromotionCode; 
                            odata.PromotionDetailId = pd.PromotionDetailInfoId; 
                            odata.ProductCode = pd.ProductCode; 
                            odata.ProductName = pd.ProductName; 
                            odata.DiscountAmount = (pd.DiscountAmount != null) ? Convert.ToInt32(pd.DiscountAmount) : 0; 
                            odata.DiscountPercent = (pd.DiscountPercent != null) ? Convert.ToInt32(pd.DiscountPercent) : 0; 
                            odata.Price = (pd.Price != null) ? Convert.ToInt32(pd.Price) : 0; 
                            odata.Amount = (pd.Amount != null) ? Convert.ToInt32(pd.Amount) : 0; 
                            odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount; 

                            count++;

                            
                            lindata.Add(new OrderData()
                            {
                                OrderCode = pd.OrderCode,
                                CampaignCategory = pd.CampaignCategoryCode, 
                                CampaignCategoryName = pd.CampaignCategoryName, 
                                CampaignCode = pd.CampaignCode, 
                                PromotionCode = pd.PromotionCode, 
                                PromotionDetailId = pd.PromotionDetailInfoId, 
                                ProductCode = pd.ProductCode, 
                                ProductName = pd.ProductName, 
                                DiscountAmount = (pd.DiscountAmount != null) ? Convert.ToInt32(pd.DiscountAmount) : 0, 
                                DiscountPercent = (pd.DiscountPercent != null) ? Convert.ToInt32(pd.DiscountPercent) : 0, 
                                Price = (pd.Price != null) ? Convert.ToInt32(pd.Price) : 0, 
                                Amount = (pd.Amount != null) ? Convert.ToInt32(pd.Amount) : 0, 
                                
                                
                                SumPrice = odata.SumPrice,
                                runningNo = count,

                            });
                        }
                        else if (odata.OrderCode == "" || odata.OrderCode == null)
                        {
                            odata.OrderCode = pd.OrderCode;
                            odata.CampaignCategory = pd.CampaignCategoryCode; 
                            odata.CampaignCategoryName = pd.CampaignCategoryName; 
                            odata.CampaignCode = pd.CampaignCode; 
                            odata.PromotionCode = pd.PromotionCode; 
                            odata.PromotionDetailId = pd.PromotionDetailInfoId; 
                            odata.ProductCode = pd.ProductCode; 
                            odata.ProductName = pd.ProductName; 
                            odata.DiscountAmount = (pd.DiscountAmount != null) ? Convert.ToInt32(pd.DiscountAmount) : 0; 
                            odata.DiscountPercent = (pd.DiscountPercent != null) ? Convert.ToInt32(pd.DiscountPercent) : 0; 
                            odata.Price = (pd.Price != null) ? Convert.ToInt32(pd.Price) : 0; 
                            odata.Amount = (pd.Amount != null) ? Convert.ToInt32(pd.Amount) : 0; 
                            odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount; 

                            count++;

                            
                            lindata.Add(new OrderData()
                            {
                                OrderCode = pd.OrderCode,
                                CampaignCategory = pd.CampaignCategoryCode, 
                                CampaignCategoryName = pd.CampaignCategoryName, 
                                CampaignCode = pd.CampaignCode, 
                                PromotionCode = pd.PromotionCode, 
                                PromotionDetailId = pd.PromotionDetailInfoId, 
                                ProductCode = pd.ProductCode, 
                                ProductName = pd.ProductName, 
                                DiscountAmount = (pd.DiscountAmount != null) ? Convert.ToInt32(pd.DiscountAmount) : 0, 
                                DiscountPercent = (pd.DiscountPercent != null) ? Convert.ToInt32(pd.DiscountPercent) : 0, 
                                Price = (pd.Price != null) ? Convert.ToInt32(pd.Price) : 0, 
                                Amount = (pd.Amount != null) ? Convert.ToInt32(pd.Amount) : 0, 
                                
                                
                                SumPrice = odata.SumPrice,
                                runningNo = count,
                            });
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            L_orderdata1 = lindata;
            LoadgvOrder(L_orderdata1);
            Loadtotal(L_orderdata1);
        }
        protected List<PromotionDetailInfo> GetPromotiondetailMasterByCriteria(ProductInfo pdInfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProducttionDetailNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = pdInfo.CampaignCode;
                data["PromotionCode"] = pdInfo.PromotionCode;
                data["Active"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotiondetailInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);
            return lPromotiondetailInfo;
        }
        protected void loadPromotionfromPromotionType(String campaigncode, String promotiontypecode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionbyPromotionTypeNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = campaigncode;
                data["PromotionTypeCode"] = promotiontypecode;
                data["Active"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            if (lPromotionInfo.Count > 0)
            {
                dtlPromotionbyPromotionType.DataSource = lPromotionInfo;
                dtlPromotionbyPromotionType.DataBind();
            }
        }

        #endregion

        #region Event 

        protected void btnMapAddress_Click(object sender, EventArgs e)
        {
            HiddenField currentDeliveryAddressId = (HiddenField)SelectBranch.FindControl("currentDeliveryAddressId");
            HiddenField currentReceiptAddressId = (HiddenField)SelectBranch.FindControl("currentReceiptAddressId");
  

            if (hidtab.Value == "1")
            {
                if (L_transportdata1.Count > 0)
                {
                    currentDeliveryAddressId.Value = L_transportdata1[0].AddressId != "" ? L_transportdata1[0].AddressId : currentDeliveryAddressId.Value;
                    currentReceiptAddressId.Value = L_transportdata1[1].AddressId != "" ? L_transportdata1[1].AddressId : currentReceiptAddressId.Value;
                }

            }


            if (hidtab.Value == "2")
            {
                if (L_transportdata2.Count > 0)
                {
                    currentDeliveryAddressId.Value = L_transportdata2[0].AddressId != "" ? L_transportdata2[0].AddressId : currentDeliveryAddressId.Value;
                    currentReceiptAddressId.Value = L_transportdata2[1].AddressId != "" ? L_transportdata2[1].AddressId : currentReceiptAddressId.Value;
                }

            }


            if (hidtab.Value == "3")
            {
                if (L_transportdata3.Count > 0)
                {
                    currentDeliveryAddressId.Value = L_transportdata3[0].AddressId != "" ? L_transportdata3[0].AddressId : currentDeliveryAddressId.Value;
                    currentReceiptAddressId.Value = L_transportdata3[1].AddressId != "" ? L_transportdata3[1].AddressId : currentReceiptAddressId.Value;
                }

            }

            LoadCustomerDeliveryAddress();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address').modal();", true);
        }


        protected void UsrCtrl_SelectBranch_Click(double currentLat, double currentLng,string AreaCode)
        {
            


            HiddenField hidCustomerLat = (HiddenField)SelectBranch.FindControl("hidCustomerLat");
            HiddenField hidCustomerLng = (HiddenField)SelectBranch.FindControl("hidCustomerLng");
         

            hidCustomerLat.Value = currentLat.ToString();
            hidCustomerLng.Value = currentLng.ToString();

            HiddenField currentDeliveryAddressId = (HiddenField)SelectBranch.FindControl("currentDeliveryAddressId");
            HiddenField currentDeliveryAddress = (HiddenField)SelectBranch.FindControl("currentDeliveryAddress");
            HiddenField currentDeliverySubDistrict = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrict");
            HiddenField currentDeliverySubDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliverySubDistrictName");
            HiddenField currentDeliveryDistrict = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrict");
            HiddenField currentDeliveryDistrictName = (HiddenField)SelectBranch.FindControl("currentDeliveryDistrictName");
            HiddenField currentDeliveryProvince = (HiddenField)SelectBranch.FindControl("currentDeliveryProvince");
            HiddenField currentDeliveryProvinceName = (HiddenField)SelectBranch.FindControl("currentDeliveryProvinceName");
            HiddenField currentDeliveryZipCode = (HiddenField)SelectBranch.FindControl("currentDeliveryZipCode");

            HiddenField currentReceiptAddressId = (HiddenField)SelectBranch.FindControl("currentReceiptAddressId");
            HiddenField currentReceiptAddress = (HiddenField)SelectBranch.FindControl("currentReceiptAddress");
            HiddenField currentReceiptSubDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrict");
            HiddenField currentReceiptSubDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptSubDistrictName");
            HiddenField currentReceiptDistrict = (HiddenField)SelectBranch.FindControl("currentReceiptDistrict");
            HiddenField currentReceiptDistrictName = (HiddenField)SelectBranch.FindControl("currentReceiptDistrictName");
            HiddenField currentReceiptProvince = (HiddenField)SelectBranch.FindControl("currentReceiptProvince");
            HiddenField currentReceiptProvinceName = (HiddenField)SelectBranch.FindControl("currentReceiptProvinceName");
            HiddenField currentReceiptZipCode = (HiddenField)SelectBranch.FindControl("currentReceiptZipCode");

            List<BranchInfo> nearbyBranch1 = new List<BranchInfo>();
            List<BranchInfo> nearbyBranch2 = new List<BranchInfo>();
            List<BranchInfo> nearbyBranch3 = new List<BranchInfo>();

            if (hidtab.Value == "1")
            {
                nearbyBranch1 = Get3NearestBranch(Convert.ToDouble(hidCustomerLat.Value), Convert.ToDouble(hidCustomerLng.Value), AreaCode);

                if (L_transportdata1.Count == 0)
                {
                    L_transportdata1.Add(new transportdataInfo
                    {
                        AddressId = currentDeliveryAddressId.Value,
                        AddressType = StaticField.AddressTypeCode01, 
                        Address = currentDeliveryAddress.Value,
                        SubDistrictCode = currentDeliverySubDistrict.Value,
                        SubDistrictName = currentDeliverySubDistrictName.Value,
                        DistrictCode = currentDeliveryDistrict.Value,
                        DistrictName = currentDeliveryDistrictName.Value,
                        ProvinceCode = currentDeliveryProvince.Value,
                        ProvinceName = currentDeliveryProvinceName.Value,
                        Zipcode = currentDeliveryZipCode.Value,
                        NearestBranchList = nearbyBranch1,


                    });

                    L_transportdata1.Add(new transportdataInfo
                    {
                        AddressId = currentReceiptAddressId.Value,
                        AddressType = StaticField.AddressTypeCode02, 
                        Address = currentReceiptAddress.Value,
                        SubDistrictCode = currentReceiptSubDistrict.Value,
                        SubDistrictName = currentReceiptSubDistrictName.Value,
                        DistrictCode = currentReceiptDistrict.Value,
                        DistrictName = currentReceiptDistrictName.Value,
                        ProvinceCode = currentReceiptProvince.Value,
                        ProvinceName = currentReceiptProvinceName.Value,
                        Zipcode = currentReceiptZipCode.Value,
                        NearestBranchList = nearbyBranch1,
                    });

                }
                else
                {
                    L_transportdata1[0].AddressId = currentDeliveryAddressId.Value;
                    L_transportdata1[0].Address = currentDeliveryAddress.Value;
                    L_transportdata1[0].SubDistrictCode = currentDeliverySubDistrict.Value;
                    L_transportdata1[0].SubDistrictName = currentDeliverySubDistrictName.Value;
                    L_transportdata1[0].DistrictCode = currentDeliveryDistrict.Value;
                    L_transportdata1[0].DistrictName = currentDeliveryDistrictName.Value;
                    L_transportdata1[0].ProvinceCode = currentDeliveryProvince.Value;
                    L_transportdata1[0].ProvinceName = currentDeliveryProvinceName.Value;
                    L_transportdata1[0].Zipcode = currentDeliveryZipCode.Value;
                    L_transportdata1[0].NearestBranchList = nearbyBranch1;


                    L_transportdata1[1].AddressId = currentReceiptAddressId.Value;
                    L_transportdata1[1].Address = currentReceiptAddress.Value;
                    L_transportdata1[1].SubDistrictCode = currentReceiptSubDistrict.Value;
                    L_transportdata1[1].SubDistrictName = currentReceiptSubDistrictName.Value;
                    L_transportdata1[1].DistrictCode = currentReceiptDistrict.Value;
                    L_transportdata1[1].DistrictName = currentReceiptDistrictName.Value;
                    L_transportdata1[1].ProvinceCode = currentReceiptProvince.Value;
                    L_transportdata1[1].ProvinceName = currentReceiptProvinceName.Value;
                    L_transportdata1[1].Zipcode = currentReceiptZipCode.Value;
                    L_transportdata1[1].NearestBranchList = nearbyBranch1;

                }

                lblCustomerAddress.Text = L_transportdata1[0].Address + " " + L_transportdata1[0].SubDistrictName
                + " " + L_transportdata1[0].DistrictName + " " + L_transportdata1[0].ProvinceName + " " + L_transportdata1[0].Zipcode;

                dtlNearestBranch.DataSource = nearbyBranch1;
                dtlNearestBranch.DataBind();


            }
            else if (hidtab.Value == "2")
            {
                nearbyBranch2 = Get3NearestBranch(Convert.ToDouble(hidCustomerLat.Value), Convert.ToDouble(hidCustomerLng.Value), AreaCode);

                if (L_transportdata2.Count == 0)
                {
                    L_transportdata2.Add(new transportdataInfo
                    {
                        AddressId = currentDeliveryAddressId.Value,
                        AddressType = StaticField.AddressTypeCode01, 
                        Address = currentDeliveryAddress.Value,
                        SubDistrictCode = currentDeliverySubDistrict.Value,
                        SubDistrictName = currentDeliverySubDistrictName.Value,
                        DistrictCode = currentDeliveryDistrict.Value,
                        DistrictName = currentDeliveryDistrictName.Value,
                        ProvinceCode = currentDeliveryProvince.Value,
                        ProvinceName = currentDeliveryProvinceName.Value,
                        Zipcode = currentDeliveryZipCode.Value,
                        NearestBranchList = nearbyBranch2,
                    });

                    L_transportdata2.Add(new transportdataInfo
                    {
                        AddressId = currentReceiptAddressId.Value,
                        AddressType = StaticField.AddressTypeCode02, 
                        Address = currentReceiptAddress.Value,
                        SubDistrictCode = currentReceiptSubDistrict.Value,
                        SubDistrictName = currentReceiptSubDistrictName.Value,
                        DistrictCode = currentReceiptDistrict.Value,
                        DistrictName = currentReceiptDistrictName.Value,
                        ProvinceCode = currentReceiptProvince.Value,
                        ProvinceName = currentReceiptProvinceName.Value,
                        Zipcode = currentReceiptZipCode.Value,
                        NearestBranchList = nearbyBranch2,
                    });

                }
                else
                {
                    L_transportdata2[0].AddressId = currentDeliveryAddressId.Value;
                    L_transportdata2[0].Address = currentDeliveryAddress.Value;
                    L_transportdata2[0].SubDistrictCode = currentDeliverySubDistrict.Value;
                    L_transportdata2[0].SubDistrictName = currentDeliverySubDistrictName.Value;
                    L_transportdata2[0].DistrictCode = currentDeliveryDistrict.Value;
                    L_transportdata2[0].DistrictName = currentDeliveryDistrictName.Value;
                    L_transportdata2[0].ProvinceCode = currentDeliveryProvince.Value;
                    L_transportdata2[0].ProvinceName = currentDeliveryProvinceName.Value;
                    L_transportdata2[0].Zipcode = currentDeliveryZipCode.Value;
                    L_transportdata1[0].NearestBranchList = nearbyBranch2;


                    L_transportdata2[1].AddressId = currentReceiptAddressId.Value;
                    L_transportdata2[1].Address = currentReceiptAddress.Value;
                    L_transportdata2[1].SubDistrictCode = currentReceiptSubDistrict.Value;
                    L_transportdata2[1].SubDistrictName = currentReceiptSubDistrictName.Value;
                    L_transportdata2[1].DistrictCode = currentReceiptDistrict.Value;
                    L_transportdata2[1].DistrictName = currentReceiptDistrictName.Value;
                    L_transportdata2[1].ProvinceCode = currentReceiptProvince.Value;
                    L_transportdata2[1].ProvinceName = currentReceiptProvinceName.Value;
                    L_transportdata2[1].Zipcode = currentReceiptZipCode.Value;
                    L_transportdata1[1].NearestBranchList = nearbyBranch2;

                }

                lblCustomerAddress.Text = L_transportdata2[0].Address + " " + L_transportdata2[0].SubDistrictName
                + " " + L_transportdata2[0].DistrictName + " " + L_transportdata2[0].ProvinceName + " " + L_transportdata2[0].Zipcode;

                dtlNearestBranch.DataSource = nearbyBranch2;
                dtlNearestBranch.DataBind();
            }
            else if (hidtab.Value == "3")
            {
                nearbyBranch3 = Get3NearestBranch(Convert.ToDouble(hidCustomerLat.Value), Convert.ToDouble(hidCustomerLng.Value), AreaCode);

                if (L_transportdata3.Count == 0)
                {
                    L_transportdata3.Add(new transportdataInfo
                    {
                        AddressId = currentDeliveryAddressId.Value,
                        AddressType = StaticField.AddressTypeCode01, 
                        Address = currentDeliveryAddress.Value,
                        SubDistrictCode = currentDeliverySubDistrict.Value,
                        SubDistrictName = currentDeliverySubDistrictName.Value,
                        DistrictCode = currentDeliveryDistrict.Value,
                        DistrictName = currentDeliveryDistrictName.Value,
                        ProvinceCode = currentDeliveryProvince.Value,
                        ProvinceName = currentDeliveryProvinceName.Value,
                        Zipcode = currentDeliveryZipCode.Value,
                        NearestBranchList = nearbyBranch3,

                    });

                    L_transportdata3.Add(new transportdataInfo
                    {
                        AddressId = currentReceiptAddressId.Value,
                        AddressType = StaticField.AddressTypeCode02, 
                        Address = currentReceiptAddress.Value,
                        SubDistrictCode = currentReceiptSubDistrict.Value,
                        SubDistrictName = currentReceiptSubDistrictName.Value,
                        DistrictCode = currentReceiptDistrict.Value,
                        DistrictName = currentReceiptDistrictName.Value,
                        ProvinceCode = currentReceiptProvince.Value,
                        ProvinceName = currentReceiptProvinceName.Value,
                        Zipcode = currentReceiptZipCode.Value,
                        NearestBranchList = nearbyBranch3,

                    });

                }
                else
                {
                    L_transportdata3[0].AddressId = currentDeliveryAddressId.Value;
                    L_transportdata3[0].Address = currentDeliveryAddress.Value;
                    L_transportdata3[0].SubDistrictCode = currentDeliverySubDistrict.Value;
                    L_transportdata3[0].SubDistrictName = currentDeliverySubDistrictName.Value;
                    L_transportdata3[0].DistrictCode = currentDeliveryDistrict.Value;
                    L_transportdata3[0].DistrictName = currentDeliveryDistrictName.Value;
                    L_transportdata3[0].ProvinceCode = currentDeliveryProvince.Value;
                    L_transportdata3[0].ProvinceName = currentDeliveryProvinceName.Value;
                    L_transportdata3[0].Zipcode = currentDeliveryZipCode.Value;
                    L_transportdata1[0].NearestBranchList = nearbyBranch3;

                    L_transportdata3[1].AddressId = currentReceiptAddressId.Value;
                    L_transportdata3[1].Address = currentReceiptAddress.Value;
                    L_transportdata3[1].SubDistrictCode = currentReceiptSubDistrict.Value;
                    L_transportdata3[1].SubDistrictName = currentReceiptSubDistrictName.Value;
                    L_transportdata3[1].DistrictCode = currentReceiptDistrict.Value;
                    L_transportdata3[1].DistrictName = currentReceiptDistrictName.Value;
                    L_transportdata3[1].ProvinceCode = currentReceiptProvince.Value;
                    L_transportdata3[1].ProvinceName = currentReceiptProvinceName.Value;
                    L_transportdata3[1].Zipcode = currentReceiptZipCode.Value;
                    L_transportdata1[1].NearestBranchList = nearbyBranch3;
                }

                lblCustomerAddress.Text = L_transportdata3[0].Address + " " + L_transportdata3[0].SubDistrictName
                + " " + L_transportdata3[0].DistrictName + " " + L_transportdata3[0].ProvinceName + " " + L_transportdata3[0].Zipcode;


                dtlNearestBranch.DataSource = nearbyBranch3;
                dtlNearestBranch.DataBind();
            }

            //*** Comment on redirect from OneApp Take Order ***//
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "$('#modal-address').modal('hide');", true);

        }



        protected void btnProductPopupClose_Click(object sender, EventArgs e)
        {

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

        protected void dtlPromotion_ItemCommand(object source, DataListCommandEventArgs e)
        {

            if (e.CommandName == "ShowProduct")
            {
                ClickRadioBranch();

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    HiddenField hidPromotionCode = e.Item.FindControl("hidPromotionCode") as HiddenField;                    
                    HiddenField hidCampaignCode = e.Item.FindControl("hidCampaignCode") as HiddenField;
                    HiddenField hidLockCheckbox = e.Item.FindControl("hidLockCheckbox") as HiddenField;

                    ProductInfo cinfo = new ProductInfo();

                    cinfo.PromotionCode = hidPromotionCode.Value;
                    hidpromotiontorecipe.Value = hidPromotionCode.Value;
                    cinfo.CampaignCode = hidCampaignCode.Value;
                    hidcampaigntorecipe.Value = hidCampaignCode.Value;
                    if (hidLockCheckbox.Value == null)
                    {
                        cinfo.LockCheckbox = "";
                    }
                    else
                    {
                        cinfo.LockCheckbox = hidLockCheckbox.Value;
                    }

                    

                    if (hidLockCheckbox.Value == "Y") // Promotion Set
                    {
                        List<PromotionDetailInfo> lpromotiondetailInfo = new List<PromotionDetailInfo>();
                        lpromotiondetailInfo = GetPromotiondetailMasterByCriteria(cinfo);

                        if (lpromotiondetailInfo[0].LockCheckbox == "Y") // Promotion Set (จับกลุ่มกำหนดราคา)
                        {
                            PromotionDetailInfo pdInfo = new PromotionDetailInfo();
                            List<PromotionDetailInfo> lpdInfo = new List<PromotionDetailInfo>();

                            

                            // Add PromotionSet Head
                            pdInfo.CampaignCode = lpromotiondetailInfo[0].CampaignCode;
                            pdInfo.PromotionCode = lpromotiondetailInfo[0].PromotionCode;
                            pdInfo.PromotionName = lpromotiondetailInfo[0].PromotionName;
                            pdInfo.ProductCode = "Pro Set : ";
                            pdInfo.ProductName = "Pro Set : ";
                            
                            pdInfo.GroupPrice = (lpromotiondetailInfo[0].GroupPrice != null) ? lpromotiondetailInfo[0].GroupPrice : 0;
                            pdInfo.Unit = StaticField.Unit_10; 
                            pdInfo.UnitName = StaticField.Unit_ชุด; 
                            pdInfo.MerchantCode = lpromotiondetailInfo[0].MerchantCode;
                            pdInfo.MerchantName = lpromotiondetailInfo[0].MerchantName;
                            pdInfo.FlagProSetHeader = "Y";
                            pdInfo.PromotionDetailInfoId = 0;

                            pdInfo.LockCheckbox = "Y"; //Promotion Set Type
                            pdInfo.LockAmountFlag = lpromotiondetailInfo[0].LockAmountFlag;
                            pdInfo.FreeShippingCode = lpromotiondetailInfo[0].FreeShippingCode;

                            pdInfo.PromotionDiscountAmount = lpromotiondetailInfo[0].PromotionDiscountAmount;
                            pdInfo.PromotionDiscountPercent = lpromotiondetailInfo[0].PromotionDiscountPercent;

                            if (lpromotiondetailInfo[0].GroupPrice != 0)
                            {
                                pdInfo.DiscountAmount = 0;
                                pdInfo.DiscountPercent = 0;
                                pdInfo.Price = (lpromotiondetailInfo[0].GroupPrice != null) ? lpromotiondetailInfo[0].GroupPrice : 0; // Promotion use GroupPrice replace than Normal SumPrice
                                pdInfo.Amount = 1;
                                pdInfo.SumPrice = pdInfo.Price * pdInfo.Amount;
                                pdInfo.ParentProductCode = "-x9"; // Hard Value of Set Header for use function GetTextPrice at gvProductView (Hide Show Price and SumPrice
                            }
                            else if (lpromotiondetailInfo[0].GroupPrice == 0)
                            {
                                if (lpromotiondetailInfo[0].PromotionDiscountAmount != 0 || lpromotiondetailInfo[0].PromotionDiscountPercent != 0)
                                {
                                    Double? sumprice = 0;
                                    Double? totalproprice = 0;

                                    foreach (var lprodetV in lpromotiondetailInfo.ToList())
                                    {
                                        sumprice += lprodetV.Price * lprodetV.DefaultAmount;
                                    }

                                    totalproprice = sumprice - lpromotiondetailInfo[0].PromotionDiscountAmount - ((sumprice * lpromotiondetailInfo[0].PromotionDiscountPercent) / 100);
                                    
                                    pdInfo.Price = sumprice;
                                    pdInfo.DiscountAmount = lpromotiondetailInfo[0].PromotionDiscountAmount;
                                    pdInfo.DiscountPercent = lpromotiondetailInfo[0].PromotionDiscountPercent;
                                    pdInfo.Amount = 1;
                                    pdInfo.SumPrice = pdInfo.Price * pdInfo.Amount;
                                    pdInfo.ParentProductCode = "-y9";
                                }
                                else
                                {
                                    Double? sumprice = 0;
                                    Double? totalproprice = 0;

                                    foreach (var lprodetV in lpromotiondetailInfo.ToList())
                                    {
                                        sumprice += lprodetV.Price * lprodetV.DefaultAmount;
                                    }

                                    totalproprice = sumprice - lpromotiondetailInfo[0].PromotionDiscountAmount - ((sumprice * lpromotiondetailInfo[0].PromotionDiscountPercent) / 100);
                                    pdInfo.Price = totalproprice;
                                    pdInfo.Amount = 1;
                                    pdInfo.SumPrice = pdInfo.Price * pdInfo.Amount;
                                }
                            }

                            lpdInfo.Add(pdInfo);

                            // Add PromotionSet Child
                            foreach (var lprodetailV in lpromotiondetailInfo.ToList())
                            {
                                pdInfo = new PromotionDetailInfo();

                                pdInfo.FlagCombo = lprodetailV.FlagCombo;
                                pdInfo.ProductCode = lprodetailV.ProductCode;
                                pdInfo.ProductName = lprodetailV.ProductName;
                                pdInfo.Unit = lprodetailV.Unit;
                                pdInfo.UnitName = lprodetailV.UnitName;
                                pdInfo.GroupPrice = (lpromotiondetailInfo[0].GroupPrice != null) ? lpromotiondetailInfo[0].GroupPrice : 0;
                                pdInfo.Price = lprodetailV.Price;
                                pdInfo.DefaultAmount = lprodetailV.DefaultAmount;

                                if (lpromotiondetailInfo[0].GroupPrice != 0)
                                {
                                    pdInfo.SumPrice = 0;
                                    pdInfo.ParentProductCode = "-x99"; // Hard Value of Set Child for use function GetTextPrice at gvProductView (Hide Show Price and SumPrice)
                                }
                                else if (lpromotiondetailInfo[0].GroupPrice == 0)
                                {
                                    pdInfo.SumPrice = pdInfo.Price * pdInfo.DefaultAmount;
                                    pdInfo.ParentProductCode = "-y99";
                                }

                                pdInfo.DiscountAmount = lprodetailV.DiscountAmount;
                                pdInfo.DiscountPercent = lprodetailV.DiscountPercent;
                                pdInfo.PromotionCode = lprodetailV.PromotionCode;
                                pdInfo.PromotionName = lprodetailV.PromotionName;
                                pdInfo.PromotionDetailInfoId = lprodetailV.PromotionDetailInfoId;
                                pdInfo.CampaignCode = lprodetailV.CampaignCode;
                                pdInfo.FreeShippingCode = lprodetailV.FreeShippingCode;
                                pdInfo.Amount = lprodetailV.DefaultAmount;
                                pdInfo.MerchantCode = lprodetailV.MerchantCode;
                                pdInfo.MerchantName = lprodetailV.MerchantName;
                                pdInfo.FlagProSetHeader = "N";
                                pdInfo.LockCheckbox = "Y"; //Promotion Set Type
                                pdInfo.LockAmountFlag = lprodetailV.LockAmountFlag;
                                pdInfo.FreeShippingCode = lprodetailV.FreeShippingCode;


                                lpdInfo.Add(pdInfo);
                            }

                            
                        }
                    }
                    else // Promotion by Product
                    {
                        BindProductByPromotion(cinfo);
                    }                    
                }

            }
        }
        protected void dtlPromotionTypebyCampaign_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }
        protected void dtlPromotionType_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ShowPromotionType")
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                    HiddenField hidCampaignCode = e.Item.FindControl("hidCampaignCode") as HiddenField;
                    HiddenField hidPromotionCode = e.Item.FindControl("hidPromotionCode") as HiddenField;
                    HiddenField hidPromotionTypeCode = e.Item.FindControl("hidPromotionTypeCode") as HiddenField;

                    loadPromotionfromPromotionType(hidCampaignCode.Value, hidPromotionTypeCode.Value);
                }
            }
        }
        protected void dtlPromotionbyCampaign_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }
        protected void dtlPromotionbyCampaign_ItemDataBound(object source, DataListItemEventArgs e)
        {

        }
        protected void dtlPromotionTypebyCampaign_ItemDataBound(object source, DataListItemEventArgs e)
        {

        }
        protected void dtlPromotionDetail_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "addtocart")
                {
                    ClickRadioBranch();

                    if (hidflagcombo.Value == "Y") // มีการ popup modal productset
                    {
                        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                        {

                            HiddenField hidPromotionDetailId = e.Item.FindControl("hidPromotionDetailId") as HiddenField;
                            HiddenField hidPromotionDetailName = e.Item.FindControl("hidPromotionDetailName") as HiddenField;
                            HiddenField hidPrice = e.Item.FindControl("hidPrice") as HiddenField;
                            HiddenField hidCampaignCode = e.Item.FindControl("hidCampaignCode") as HiddenField;
                            HiddenField hidPromotionCode = e.Item.FindControl("hidPromotionCode") as HiddenField;
                            hidCombosetPromotionCode.Value = hidPromotionCode.Value;
                            hidCombosetCampaignCode.Value = hidCampaignCode.Value;
                            lblCombosetCode.Text = hidPromotionDetailId.Value;
                            lblCombosetName.Text = hidPromotionDetailName.Value;
                            lblCombosetPrice.Text = string.Format("{0:n}", (hidPrice.Value));

                            LoadSubMainPromotionDetail(hidPromotionDetailId.Value);
                            LoadSubExchangePromotionDetail(hidPromotionDetailId.Value);

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-productset').modal();", true);

                        }
                    }
                    else // add to cart
                    {

                        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                        {

                            HiddenField hidPromotionDetailId = e.Item.FindControl("hidPromotionDetailId") as HiddenField;
                            HiddenField hidProductName = e.Item.FindControl("hidProductName") as HiddenField;
                            HiddenField hidProductCode = e.Item.FindControl("hidProductCode") as HiddenField;
                            HiddenField hidDiscountAmount = e.Item.FindControl("hidDiscountAmount") as HiddenField;
                            HiddenField hidDiscountPercent = e.Item.FindControl("hidDiscountPercent") as HiddenField;
                            HiddenField hidCampaignCode = e.Item.FindControl("hidCampaignCode") as HiddenField;
                            HiddenField hidPromotionCode = e.Item.FindControl("hidPromotionCode") as HiddenField;
                            HiddenField hidPrice = e.Item.FindControl("hidPrice") as HiddenField;

                            OrderData odata = new OrderData();
                            List<OrderData> lneworderdata = new List<OrderData>();

                            List<OrderData> lorderdatacheck = new List<OrderData>();
                            string flagDuplicate = "N";
                            if (hidtab.Value == "1")
                            {
                                lorderdatacheck = L_orderdata1;
                                foreach (var item in lorderdatacheck)
                                {
                                    odata = bindorderdata(item);

                                    if ((item.PromotionCode == hidPromotionCode.Value) && (item.CampaignCode == hidCampaignCode.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailId.Value))
                                    {

                                        odata.Amount = item.Amount + 1;
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
                                L_orderdata1 = lneworderdata;


                            }
                            else if (hidtab.Value == "2")
                            {
                                lorderdatacheck = L_orderdata2;
                                foreach (var item in lorderdatacheck)
                                {
                                    odata = bindorderdata(item);

                                    if ((item.PromotionCode == hidPromotionCode.Value) && (item.CampaignCode == hidCampaignCode.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailId.Value))
                                    {

                                        odata.Amount = item.Amount + 1;
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
                                L_orderdata2 = lneworderdata;

                            }
                            else if (hidtab.Value == "3")
                            {
                                lorderdatacheck = L_orderdata3;

                                foreach (var item in lorderdatacheck)
                                {
                                    odata = bindorderdata(item);

                                    if ((item.PromotionCode == hidPromotionCode.Value) && (item.CampaignCode == hidCampaignCode.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailId.Value))
                                    {

                                        odata.Amount = item.Amount + 1;
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

                                L_orderdata3 = lneworderdata;

                            }

                            if (flagDuplicate == "N")
                            {
                                odata = new OrderData();

                                odata.CampaignCategory = hidcampaigncategorycode.Value;
                                odata.CampaignCategoryName = hidcampaigncategoryname.Value;
                                odata.CampaignCode = hidCampaignCode.Value;
                                odata.PromotionCode = hidPromotionCode.Value;
                                odata.PromotionDetailId = (hidPromotionDetailId.Value != "") ? Convert.ToInt32(hidPromotionDetailId.Value) : 0;
                                odata.ProductCode = hidProductCode.Value;
                                odata.ProductName = hidProductName.Value;
                                odata.DiscountAmount = (hidDiscountAmount.Value != "") ? Convert.ToInt32(hidDiscountAmount.Value) : 0;
                                odata.DiscountPercent = (hidDiscountPercent.Value != "") ? Convert.ToInt32(hidDiscountPercent.Value) : 0;
                                odata.Price = (hidPrice.Value != "") ? Convert.ToDouble(hidPrice.Value) : -99;
                                odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;
                                odata.Amount = 1;

                                LoadOrderdata(odata);
                            }
                            else
                            {
                                LoadgvOrder(lneworderdata);
                            }
                        }
                        btnCalculateTotal_Click(null, null);
                    }

                }
            }


            catch (Exception ex)
            {
            }

        }
        protected void dtlPromotionbyPromotionType_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ShowPromotionbyType")
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                }
            }
        }
        protected void dtlPromotionbyPromotionType_ItemDataBound(object source, DataListItemEventArgs e)
        {

        }
        protected void dtlNearestBranch_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "radBranch")
            {
                ClickRadioBranch();
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "displayNonSelected();", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((LinkButton)sender).Parent.Parent;

            Label lbltotal = (Label)currentRow.FindControl("lbltotal");
            HiddenField hidRunning = (HiddenField)currentRow.FindControl("hidRunning");
            List<OrderData> lorderdata = new List<OrderData>();
            HiddenField hidComboCode = (HiddenField)currentRow.FindControl("hidComboCode");

            if (hidtab.Value == "1")
            {
              
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode1.Value != "" && hidSelectedBranchCode1.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }
           
                if (hidComboCode.Value != "")
                {
                    L_orderdata1.RemoveAll(x => x.ComboCode == hidComboCode.Value);
                    L_orderdata1.RemoveAll(x => x.ParentProductCode == hidComboCode.Value);
                    lorderdata = LoadOrderbyList(L_orderdata1, lbltotal);

                }
                else
                {
                    L_orderdata1.RemoveAll(x => x.runningNo == Convert.ToInt32(hidRunning.Value));
                    lorderdata = LoadOrderbyList(L_orderdata1, lbltotal);

                }
                L_orderdata1 = lorderdata;
                LoadgvOrder(lorderdata);
            }
            else
            if (hidtab.Value == "2")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode2.Value != "" && hidSelectedBranchCode2.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }

                if (hidComboCode.Value != "")
                {
                    L_orderdata2.RemoveAll(x => x.ComboCode == hidComboCode.Value);
                    L_orderdata2.RemoveAll(x => x.ParentProductCode == hidComboCode.Value);
                    lorderdata = LoadOrderbyList(L_orderdata2, lbltotal);

                }
                else
                {
                    L_orderdata2.RemoveAll(x => x.runningNo == Convert.ToInt32(hidRunning.Value));
                    lorderdata = LoadOrderbyList(L_orderdata2, lbltotal);

                }
                L_orderdata2 = lorderdata;
                LoadgvOrder(lorderdata);
            }
            else
            if (hidtab.Value == "3")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode3.Value != "" && hidSelectedBranchCode3.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }

                if (hidComboCode.Value != "")
                {
                    L_orderdata3.RemoveAll(x => x.ComboCode == hidComboCode.Value);
                    L_orderdata3.RemoveAll(x => x.ParentProductCode == hidComboCode.Value);
                    lorderdata = LoadOrderbyList(L_orderdata3, lbltotal);

                }
                else
                {
                    L_orderdata3.RemoveAll(x => x.runningNo == Convert.ToInt32(hidRunning.Value));
                    lorderdata = LoadOrderbyList(L_orderdata3, lbltotal);

                }
                L_orderdata3 = lorderdata;
                LoadgvOrder(lorderdata);
            }
            btnCalculateTotal_Click(null, null);
        }

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void dtlCampaign_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ShowPromotion")
                {
                    ClickRadioBranch();

                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {

                        HiddenField hidCampaignCode = e.Item.FindControl("hidCampaignCode") as HiddenField;
                        HiddenField hidFlagShowProductPromotion = e.Item.FindControl("hidFlagShowProductPromotion") as HiddenField;
                        HiddenField hidFlagComboSet = e.Item.FindControl("hidFlagComboSet") as HiddenField;
                        hidflagcombo.Value = hidFlagComboSet.Value;
                        if (hidFlagShowProductPromotion.Value == StaticField.FlagShowProductPromotion_PROMOTION) 
                        {
                            //Load Promotion

                            PromotionInfo cinfo = new PromotionInfo();

                            cinfo.CampaignCode = hidCampaignCode.Value;
                            hidcampaigncodetorecipe.Value = hidCampaignCode.Value;
                            hidflagshowproductpromotionrecipe.Value = hidFlagShowProductPromotion.Value;

                            BindPromotionByCampaign(cinfo, hidFlagShowProductPromotion.Value);
                            BindPromotionTypeByCampaign(cinfo, hidFlagShowProductPromotion.Value);

                            //Clear promotiondetail
                            PromotionDetailInfo pinfo = new PromotionDetailInfo();

                            pinfo.CampaignCode = "-99";

                            BindPromotionDetailByCampaign(pinfo);
                        }
                        else
                        {
                            

                            PromotionDetailInfo cinfo = new PromotionDetailInfo();
                            PromotionInfo pinfo = new PromotionInfo();

                            cinfo.CampaignCode = hidCampaignCode.Value;
                            pinfo.CampaignCode = hidCampaignCode.Value;
                            hidcampaigncodetorecipe.Value = hidCampaignCode.Value;
                            hidflagshowproductpromotionrecipe.Value = hidFlagShowProductPromotion.Value;

                            BindPromotionDetailByCampaign(cinfo); // Bind Promotion by Product (Old UI Style)
                            
                        }
                    }

                }
            }


            catch (Exception ex)
            {
            }
        }

        protected void dtlCampaignCategory_ItemCommand(object source, DataListCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ShowCampaign")
                {
                 
                    ClickRadioBranch();

                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {

                        HiddenField hidCampaignCategoryCodegv = e.Item.FindControl("hidCampaignCategoryCode") as HiddenField;
                        HiddenField hidCampaignCategoryNamegv = e.Item.FindControl("hidCampaignCategoryName") as HiddenField;

                        CampaignInfo cinfo = new CampaignInfo();


                        //get campaign category code for loadtop5
                        string strcampcatecode = "";
                        if ((hidtab.Value == "1")||  (hidtab.Value == ""))
                        {
                             strcampcatecode = (hidtab1CampaignCategory.Value != "") ? hidtab1CampaignCategory.Value : hidCampaignCategoryCodegv.Value;

                        }
                        else if (hidtab.Value == "2")
                        {
                            strcampcatecode = (hidtab2CampaignCategory.Value != "") ? hidtab2CampaignCategory.Value : hidCampaignCategoryCodegv.Value;
                        }
                        if (hidtab.Value == "3")
                        {
                            strcampcatecode = (hidtab3CampaignCategory.Value != "") ? hidtab3CampaignCategory.Value : hidCampaignCategoryCodegv.Value;
                        }
                       
                        BindTop5Product(strcampcatecode);

                        //end get campaign category code for loadtop5
                        

                        hidcampaigncategorycode.Value = hidCampaignCategoryCodegv.Value;
                        hidcampaigncategoryname.Value = hidCampaignCategoryNamegv.Value;
                        
                  
                        cinfo.CampaignCategory = hidCampaignCategoryCodegv.Value;

                        BindCampaign(cinfo);


                        //Clear promotiondetail
                        PromotionDetailInfo dinfo = new PromotionDetailInfo();

                        dinfo.CampaignCode = "-99";

                        BindPromotionDetailByCampaign(dinfo);                        

                        //Clear promotion
                        PromotionInfo pinfo = new PromotionInfo();

                        pinfo.CampaignCode = "-99";
                        String flgashowproductpromotion = "";

                        BindPromotionByCampaign(pinfo, flgashowproductpromotion);
                    }

                }
            }


            catch (Exception ex)
            {
            }
        }
        protected void gvPromotionDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPromotionDetail.Rows[index];

            try
            {
                if (e.CommandName == "addtocart")
                {
                    ClickRadioBranch();

                    if (hidflagcombo.Value == "Y") // มีการ popup modal productset
                    {


                        HiddenField hidPromotionDetailId = row.FindControl("hidPromotionDetailId") as HiddenField;
                        HiddenField hidPromotionDetailName = row.FindControl("hidPromotionDetailName") as HiddenField;
                        HiddenField hidPrice = row.FindControl("hidPrice") as HiddenField;
                        HiddenField hidCampaignCode = row.FindControl("hidCampaignCode") as HiddenField;
                        HiddenField hidPromotionCode = row.FindControl("hidPromotionCode") as HiddenField;
                        hidCombosetPromotionCode.Value = hidPromotionCode.Value;
                        hidCombosetCampaignCode.Value = hidCampaignCode.Value;
                        lblCombosetCode.Text = hidPromotionDetailId.Value;
                        lblCombosetName.Text = hidPromotionDetailName.Value;
                        lblCombosetPrice.Text = string.Format("{0:n}", (hidPrice.Value));

                        LoadSubMainPromotionDetail(hidPromotionDetailId.Value);
                        LoadSubExchangePromotionDetail(hidPromotionDetailId.Value);

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-productset').modal();", true);


                    }
                    else // add to cart
                    {


                        HiddenField hidPromotionDetailId = row.FindControl("hidPromotionDetailId") as HiddenField;
                        HiddenField hidProductName = row.FindControl("hidProductName") as HiddenField;
                        HiddenField hidProductCode = row.FindControl("hidProductCode") as HiddenField;
                        HiddenField hidDiscountAmount = row.FindControl("hidDiscountAmount") as HiddenField;
                        HiddenField hidDiscountPercent = row.FindControl("hidDiscountPercent") as HiddenField;
                        HiddenField hidCampaignCode = row.FindControl("hidCampaignCode") as HiddenField;
                        HiddenField hidPromotionCode = row.FindControl("hidPromotionCode") as HiddenField;
                        HiddenField hidPrice = row.FindControl("hidPrice") as HiddenField;

                        OrderData odata = new OrderData();
                        List<OrderData> lneworderdata = new List<OrderData>();

                        List<OrderData> lorderdatacheck = new List<OrderData>();
                        string flagDuplicate = "N";
                        if (hidtab.Value == "1")
                        {
                            lorderdatacheck = L_orderdata1;
                            foreach (var item in lorderdatacheck)
                            {
                                odata = bindorderdata(item);

                                if ((item.PromotionCode == hidPromotionCode.Value) && (item.CampaignCode == hidCampaignCode.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailId.Value))
                                {

                                    odata.Amount = item.Amount + 1;
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
                            L_orderdata1 = lneworderdata;


                        }
                        else if (hidtab.Value == "2")
                        {
                            lorderdatacheck = L_orderdata2;
                            foreach (var item in lorderdatacheck)
                            {
                                odata = bindorderdata(item);

                                if ((item.PromotionCode == hidPromotionCode.Value) && (item.CampaignCode == hidCampaignCode.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailId.Value))
                                {

                                    odata.Amount = item.Amount + 1;
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
                            L_orderdata2 = lneworderdata;

                        }
                        else if (hidtab.Value == "3")
                        {
                            lorderdatacheck = L_orderdata3;

                            foreach (var item in lorderdatacheck)
                            {
                                odata = bindorderdata(item);

                                if ((item.PromotionCode == hidPromotionCode.Value) && (item.CampaignCode == hidCampaignCode.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailId.Value))
                                {

                                    odata.Amount = item.Amount + 1;
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

                            L_orderdata3 = lneworderdata;

                        }

                        if (flagDuplicate == "N")
                        {
                            odata = new OrderData();

                            odata.CampaignCategory = hidcampaigncategorycode.Value;
                            odata.CampaignCategoryName = hidcampaigncategoryname.Value;
                            odata.CampaignCode = hidCampaignCode.Value;
                            odata.PromotionCode = hidPromotionCode.Value;
                            odata.PromotionDetailId = (hidPromotionDetailId.Value != "") ? Convert.ToInt32(hidPromotionDetailId.Value) : 0;
                            odata.ProductCode = hidProductCode.Value;
                            odata.ProductName = hidProductName.Value;
                            odata.DiscountAmount = (hidDiscountAmount.Value != "") ? Convert.ToInt32(hidDiscountAmount.Value) : 0;
                            odata.DiscountPercent = (hidDiscountPercent.Value != "") ? Convert.ToInt32(hidDiscountPercent.Value) : 0;
                            odata.Price = (hidPrice.Value != "") ? Convert.ToDouble(hidPrice.Value) : -99;
                            odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;
                            odata.Amount = 1;

                            LoadOrderdata(odata);
                        }
                        else
                        {
                            LoadgvOrder(lneworderdata);
                        }

                        btnCalculateTotal_Click(null, null);
                    }

                }
            }


            catch (Exception ex)
            {
            }

        }

        protected void AddtocartProductTop01(object sender, EventArgs e)
        {
            try
            {
                OrderData odata = new OrderData();
                List<OrderData> lneworderdata = new List<OrderData>();

                List<OrderData> lorderdatacheck = new List<OrderData>();
                string flagDuplicate = "N";

                ClickRadioBranch();

                if (hidtab.Value == "1")
                {
                    string a = hidtab1CampaignCategory.Value;
                    string b = hidcampaigncategorycode.Value;

                    hidtab1CampaignCategory.Value = hidCampaignCategoryCodeProductTop01.Value;
                    hidtab1CampaignCategoryname.Value = hidCampaignCategoryNameProductTop01.Value;

                    
                    lorderdatacheck = L_orderdata1;
                        foreach (var item in lorderdatacheck)
                        {
                            odata = bindorderdata(item);

                            if ((item.PromotionCode == hidPromotionCodeProductTop01.Value) && (item.CampaignCode == hidCampaignCodeProductTop01.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop01.Value))
                            {

                                odata.Amount = item.Amount + 1;
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
                        L_orderdata1 = lneworderdata;

                    
                }
                else if (hidtab.Value == "2")
                {
                    hidtab2CampaignCategory.Value = hidCampaignCategoryCodeProductTop01.Value;
                    hidtab2CampaignCategoryname.Value = hidCampaignCategoryNameProductTop01.Value;
                    
                    lorderdatacheck = L_orderdata2;
                        foreach (var item in lorderdatacheck)
                        {
                            odata = bindorderdata(item);

                            if ((item.PromotionCode == hidPromotionCodeProductTop01.Value) && (item.CampaignCode == hidCampaignCodeProductTop01.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop01.Value))
                            {

                                odata.Amount = item.Amount + 1;
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
                        L_orderdata2 = lneworderdata;
                    
                }
                else if (hidtab.Value == "3")
                {
                    hidtab3CampaignCategory.Value = hidCampaignCategoryCodeProductTop01.Value;
                    hidtab3CampaignCategoryname.Value = hidCampaignCategoryNameProductTop01.Value;
                    
                    lorderdatacheck = L_orderdata3;

                        foreach (var item in lorderdatacheck)
                        {
                            odata = bindorderdata(item);

                            if ((item.PromotionCode == hidPromotionCodeProductTop01.Value) && (item.CampaignCode == hidCampaignCodeProductTop01.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop01.Value))
                            {

                                odata.Amount = item.Amount + 1;
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

                        L_orderdata3 = lneworderdata;
                    
                }
                    if (flagDuplicate == "N")
                    {
                        odata = new OrderData();

                        odata.CampaignCategory = hidcampaigncategorycode.Value;
                        odata.CampaignCategoryName = hidcampaigncategoryname.Value;
                        odata.CampaignCode = hidCampaignCodeProductTop01.Value;
                        odata.PromotionCode = hidPromotionCodeProductTop01.Value;
                        odata.PromotionDetailId = (hidPromotionDetailIdProductTop01.Value != "") ? Convert.ToInt32(hidPromotionDetailIdProductTop01.Value) : 0;
                        odata.ProductCode = hidProductCodeProductTop01.Value;
                        odata.ProductName = hidProductNameProductTop01.Value;
                        odata.DiscountAmount = (hidDiscountAmountProductTop01.Value != "") ? Convert.ToInt32(hidDiscountAmountProductTop01.Value) : 0;
                        odata.DiscountPercent = (hidDiscountPercentProductTop01.Value != "") ? Convert.ToInt32(hidDiscountPercentProductTop01.Value) : 0;
                        odata.Price = (hidPriceProductTop01.Value != "") ? Convert.ToDouble(hidPriceProductTop01.Value) : -99;
                        odata.Amount = 1;
                        odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;

                    
                    LoadOrderdatafromTopProduct(odata);
                    }
                    else
                    {
                        LoadgvOrderfromTopProduct(lneworderdata, odata);
                        

                    }

                

                ScriptManager.RegisterStartupScript(this, Page.GetType(), "function", "displayNonSelected()", true);
                btnCalculateTotal_Click(null, null);                
            }
            catch (Exception ex)
            {

            }
        }

        protected void AddtocartProductTop02(object sender, EventArgs e)
        {
            OrderData odata = new OrderData();
            List<OrderData> lneworderdata = new List<OrderData>();

            List<OrderData> lorderdatacheck = new List<OrderData>();
            string flagDuplicate = "N";

            ClickRadioBranch();

            if (hidtab.Value == "1")
            {
                hidtab1CampaignCategory.Value = hidCampaignCategoryCodeProductTop02.Value;
                hidtab1CampaignCategoryname.Value = hidCampaignCategoryNameProductTop02.Value;
                
                lorderdatacheck = L_orderdata1;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop02.Value) && (item.CampaignCode == hidCampaignCodeProductTop02.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop02.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata1 = lneworderdata;
                
            }
            else if (hidtab.Value == "2")
            {
                hidtab2CampaignCategory.Value = hidCampaignCategoryCodeProductTop02.Value;
                hidtab2CampaignCategoryname.Value = hidCampaignCategoryNameProductTop02.Value;
                
                lorderdatacheck = L_orderdata2;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop02.Value) && (item.CampaignCode == hidCampaignCodeProductTop02.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop02.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata2 = lneworderdata;
                
            }
            else if (hidtab.Value == "3")
            {
                hidtab3CampaignCategory.Value = hidCampaignCategoryCodeProductTop02.Value;
                hidtab3CampaignCategoryname.Value = hidCampaignCategoryNameProductTop02.Value;
                
                lorderdatacheck = L_orderdata3;

                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop02.Value) && (item.CampaignCode == hidCampaignCodeProductTop02.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop02.Value))
                        {

                            odata.Amount = item.Amount + 1;
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

                    L_orderdata3 = lneworderdata;
                
            }
                if (flagDuplicate == "N")
                {
                    odata = new OrderData();

                    odata.CampaignCategory = hidcampaigncategorycode.Value;
                    odata.CampaignCategoryName = hidcampaigncategoryname.Value;
                    odata.CampaignCode = hidCampaignCodeProductTop02.Value;
                    odata.PromotionCode = hidPromotionCodeProductTop02.Value;
                    odata.PromotionDetailId = (hidPromotionDetailIdProductTop02.Value != "") ? Convert.ToInt32(hidPromotionDetailIdProductTop02.Value) : 0;
                    odata.ProductCode = hidProductCodeProductTop02.Value;
                    odata.ProductName = hidProductNameProductTop02.Value;
                    odata.DiscountAmount = (hidDiscountAmountProductTop02.Value != "") ? Convert.ToInt32(hidDiscountAmountProductTop02.Value) : 0;
                    odata.DiscountPercent = (hidDiscountPercentProductTop02.Value != "") ? Convert.ToInt32(hidDiscountPercentProductTop02.Value) : 0;
                    odata.Price = (hidPriceProductTop02.Value != "") ? Convert.ToDouble(hidPriceProductTop02.Value) : -99;
                    odata.Amount = 1;
                    odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;

                
                LoadOrderdatafromTopProduct(odata);
            }
                else
                {
                    LoadgvOrderfromTopProduct(lneworderdata, odata);
                    

                }
            
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "function", "displayNonSelected()", true);
            btnCalculateTotal_Click(null, null);
        }

        protected void AddtocartProductTop03(object sender, EventArgs e)
        {
            OrderData odata = new OrderData();
            List<OrderData> lneworderdata = new List<OrderData>();

            List<OrderData> lorderdatacheck = new List<OrderData>();
            string flagDuplicate = "N";

            ClickRadioBranch();

            if (hidtab.Value == "1")
            {
                hidtab1CampaignCategory.Value = hidCampaignCategoryCodeProductTop03.Value;
                hidtab1CampaignCategoryname.Value = hidCampaignCategoryNameProductTop03.Value;
                
                lorderdatacheck = L_orderdata1;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop03.Value) && (item.CampaignCode == hidCampaignCodeProductTop03.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop03.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata1 = lneworderdata;
                
            }
            else if (hidtab.Value == "2")
            {
                hidtab2CampaignCategory.Value = hidCampaignCategoryCodeProductTop03.Value;
                hidtab2CampaignCategoryname.Value = hidCampaignCategoryNameProductTop03.Value;
                
                lorderdatacheck = L_orderdata2;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop03.Value) && (item.CampaignCode == hidCampaignCodeProductTop03.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop03.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata2 = lneworderdata;
                
            }
            else if (hidtab.Value == "3")
            {
                hidtab3CampaignCategory.Value = hidCampaignCategoryCodeProductTop03.Value;
                hidtab3CampaignCategoryname.Value = hidCampaignCategoryNameProductTop03.Value;
                
                lorderdatacheck = L_orderdata3;

                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop03.Value) && (item.CampaignCode == hidCampaignCodeProductTop03.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop03.Value))
                        {

                            odata.Amount = item.Amount + 1;
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

                    L_orderdata3 = lneworderdata;
                
            }
            if (flagDuplicate == "N")
            {
                odata = new OrderData();

                odata.CampaignCategory = hidcampaigncategorycode.Value;
                odata.CampaignCategoryName = hidcampaigncategoryname.Value;
                odata.CampaignCode = hidCampaignCodeProductTop03.Value;
                odata.PromotionCode = hidPromotionCodeProductTop03.Value;
                odata.PromotionDetailId = (hidPromotionDetailIdProductTop03.Value != "") ? Convert.ToInt32(hidPromotionDetailIdProductTop03.Value) : 0;
                odata.ProductCode = hidProductCodeProductTop03.Value;
                odata.ProductName = hidProductNameProductTop03.Value;
                odata.DiscountAmount = (hidDiscountAmountProductTop03.Value != "") ? Convert.ToInt32(hidDiscountAmountProductTop03.Value) : 0;
                odata.DiscountPercent = (hidDiscountPercentProductTop03.Value != "") ? Convert.ToInt32(hidDiscountPercentProductTop03.Value) : 0;
                odata.Price = (hidPriceProductTop03.Value != "") ? Convert.ToDouble(hidPriceProductTop03.Value) : -99;
                odata.Amount = 1;
                odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;

                
                LoadOrderdatafromTopProduct(odata);
            }
            else
            {
                LoadgvOrderfromTopProduct(lneworderdata, odata);
                
            }
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "function", "displayNonSelected()", true);
            btnCalculateTotal_Click(null, null);
        }

        protected void AddtocartProductTop04(object sender, EventArgs e)
        {
            OrderData odata = new OrderData();
            List<OrderData> lneworderdata = new List<OrderData>();

            List<OrderData> lorderdatacheck = new List<OrderData>();
            string flagDuplicate = "N";

            ClickRadioBranch();

            if (hidtab.Value == "1")
            {
                hidtab1CampaignCategory.Value = hidCampaignCategoryCodeProductTop04.Value;
                hidtab1CampaignCategoryname.Value = hidCampaignCategoryNameProductTop04.Value;
                
                lorderdatacheck = L_orderdata1;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop04.Value) && (item.CampaignCode == hidCampaignCodeProductTop04.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop04.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata1 = lneworderdata;
                
            }
            else if (hidtab.Value == "2")
            {
                hidtab2CampaignCategory.Value = hidCampaignCategoryCodeProductTop04.Value;
                hidtab2CampaignCategoryname.Value = hidCampaignCategoryNameProductTop04.Value;
                
                lorderdatacheck = L_orderdata2;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop04.Value) && (item.CampaignCode == hidCampaignCodeProductTop04.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop04.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata2 = lneworderdata;
                
            }
            else if (hidtab.Value == "3")
            {
                hidtab3CampaignCategory.Value = hidCampaignCategoryCodeProductTop04.Value;
                hidtab3CampaignCategoryname.Value = hidCampaignCategoryNameProductTop04.Value;
                
                lorderdatacheck = L_orderdata3;

                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop04.Value) && (item.CampaignCode == hidCampaignCodeProductTop04.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop04.Value))
                        {

                            odata.Amount = item.Amount + 1;
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

                    L_orderdata3 = lneworderdata;
                
            }
            if (flagDuplicate == "N")
            {
                odata = new OrderData();

                odata.CampaignCategory = hidcampaigncategorycode.Value;
                odata.CampaignCategoryName = hidcampaigncategoryname.Value;
                odata.CampaignCode = hidCampaignCodeProductTop04.Value;
                odata.PromotionCode = hidPromotionCodeProductTop04.Value;
                odata.PromotionDetailId = (hidPromotionDetailIdProductTop04.Value != "") ? Convert.ToInt32(hidPromotionDetailIdProductTop04.Value) : 0;
                odata.ProductCode = hidProductCodeProductTop04.Value;
                odata.ProductName = hidProductNameProductTop04.Value;
                odata.DiscountAmount = (hidDiscountAmountProductTop04.Value != "") ? Convert.ToInt32(hidDiscountAmountProductTop04.Value) : 0;
                odata.DiscountPercent = (hidDiscountPercentProductTop04.Value != "") ? Convert.ToInt32(hidDiscountPercentProductTop04.Value) : 0;
                odata.Price = (hidPriceProductTop04.Value != "") ? Convert.ToDouble(hidPriceProductTop04.Value) : -99;
                odata.Amount = 1;
                odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;

                
                LoadOrderdatafromTopProduct(odata);
            }
            else
            {
                LoadgvOrderfromTopProduct(lneworderdata, odata);
                
            }
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "function", "displayNonSelected()", true);
            btnCalculateTotal_Click(null, null);
        }

        protected void AddtocartProductTop05(object sender, EventArgs e)
        {
            OrderData odata = new OrderData();
            List<OrderData> lneworderdata = new List<OrderData>();

            List<OrderData> lorderdatacheck = new List<OrderData>();
            string flagDuplicate = "N";

            ClickRadioBranch();


            if (hidtab.Value == "1")
            {
                hidtab1CampaignCategory.Value = hidCampaignCategoryCodeProductTop05.Value;
                hidtab1CampaignCategoryname.Value = hidCampaignCategoryNameProductTop05.Value;
                
                lorderdatacheck = L_orderdata1;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop05.Value) && (item.CampaignCode == hidCampaignCodeProductTop05.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop05.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata1 = lneworderdata;
                
            }
            else if (hidtab.Value == "2")
            {
                hidtab2CampaignCategory.Value = hidCampaignCategoryCodeProductTop05.Value;
                hidtab2CampaignCategoryname.Value = hidCampaignCategoryNameProductTop05.Value;
                
                lorderdatacheck = L_orderdata2;
                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop05.Value) && (item.CampaignCode == hidCampaignCodeProductTop05.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop05.Value))
                        {

                            odata.Amount = item.Amount + 1;
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
                    L_orderdata2 = lneworderdata;
                
            }
            else if (hidtab.Value == "3")
            {
                hidtab3CampaignCategory.Value = hidCampaignCategoryCodeProductTop05.Value;
                hidtab3CampaignCategoryname.Value = hidCampaignCategoryNameProductTop05.Value;
                
                lorderdatacheck = L_orderdata3;

                    foreach (var item in lorderdatacheck)
                    {
                        odata = bindorderdata(item);

                        if ((item.PromotionCode == hidPromotionCodeProductTop05.Value) && (item.CampaignCode == hidCampaignCodeProductTop05.Value) && (item.PromotionDetailId.ToString() == hidPromotionDetailIdProductTop05.Value))
                        {

                            odata.Amount = item.Amount + 1;
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

                    L_orderdata3 = lneworderdata;
                
            }
            if (flagDuplicate == "N")
            {
                odata = new OrderData();

                odata.CampaignCategory = hidcampaigncategorycode.Value;
                odata.CampaignCategoryName = hidcampaigncategoryname.Value;
                odata.CampaignCode = hidCampaignCodeProductTop05.Value;
                odata.PromotionCode = hidPromotionCodeProductTop05.Value;
                odata.PromotionDetailId = (hidPromotionDetailIdProductTop05.Value != "") ? Convert.ToInt32(hidPromotionDetailIdProductTop05.Value) : 0;
                odata.ProductCode = hidProductCodeProductTop05.Value;
                odata.ProductName = hidProductNameProductTop05.Value;
                odata.DiscountAmount = (hidDiscountAmountProductTop05.Value != "") ? Convert.ToInt32(hidDiscountAmountProductTop05.Value) : 0;
                odata.DiscountPercent = (hidDiscountPercentProductTop05.Value != "") ? Convert.ToInt32(hidDiscountPercentProductTop05.Value) : 0;
                odata.Price = (hidPriceProductTop05.Value != "") ? Convert.ToDouble(hidPriceProductTop05.Value) : -99;
                odata.Amount = 1;
                odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;

                
                LoadOrderdatafromTopProduct(odata);
            }
            else
            {
                LoadgvOrderfromTopProduct(lneworderdata, odata);
                
            }
            ScriptManager.RegisterStartupScript(this, Page.GetType(), "function", "displayNonSelected()", true);
            btnCalculateTotal_Click(null, null);
        }

        protected void btntab1_Click(object sender, EventArgs e)
        {
            //check top5 
            if (hidtab1CampaignCategory.Value == "")
            {
                BindTop5Product("-99");
            }
            else
            {
                BindTop5Product(hidtab1CampaignCategory.Value);
            }
            //end check top5

            hidtab.Value = "1";
            LoadgvOrder(L_orderdata1);

            lblCampaignCategory.Text = hidtab1CampaignCategoryname.Value;

            btntab1.BackColor = System.Drawing.Color.CadetBlue;
            btntab2.BackColor = System.Drawing.Color.Red;
            btntab3.BackColor = System.Drawing.Color.Red;

            if(hidtab1orderstatus.Value == "")
            {
                btncheckout.Visible = true;
                btnNewOrder.Visible = true;
                btnCloseTab.Visible = true;
                btnClear.Visible = true;

                LinkBtnProductTop01.Enabled = true;
                LinkBtnProductTop02.Enabled = true;
                LinkBtnProductTop03.Enabled = true;
                LinkBtnProductTop04.Enabled = true;
                LinkBtnProductTop05.Enabled = true;

                lblProdTop01.Visible = false;
                lblProdTop02.Visible = false;
                lblProdTop03.Visible = false;
                lblProdTop04.Visible = false;
                lblProdTop05.Visible = false;
            }
            else
            {
                btncheckout.Visible = false;
                btnNewOrder.Visible = false;
                btnCloseTab.Visible = false;
                btnClear.Visible = false;

                LinkBtnProductTop01.Enabled = false;
                LinkBtnProductTop02.Enabled = false;
                LinkBtnProductTop03.Enabled = false;
                LinkBtnProductTop04.Enabled = false;
                LinkBtnProductTop05.Enabled = false;

                LinkBtnProductTop01.Visible = false;
                LinkBtnProductTop02.Visible = false;
                LinkBtnProductTop03.Visible = false;
                LinkBtnProductTop04.Visible = false;
                LinkBtnProductTop05.Visible = false;

                lblProdTop01.Visible = true;
                lblProdTop02.Visible = true;
                lblProdTop03.Visible = true;
                lblProdTop04.Visible = true;
                lblProdTop05.Visible = true;
            }

            if (L_transportdata1.Count != 0)
            {
                lblCustomerAddress.Text = L_transportdata1[0].Address + " " + L_transportdata1[0].SubDistrictName
                + " " + L_transportdata1[0].DistrictName + " " + L_transportdata1[0].ProvinceName + " " + L_transportdata1[0].Zipcode;

                dtlNearestBranch.DataSource = L_transportdata1[0].NearestBranchList;
                dtlNearestBranch.DataBind();
            }
            else
            {
                lblCustomerAddress.Text = "";

                dtlNearestBranch.DataSource = null;
                dtlNearestBranch.DataBind();
            }

            ClickRadioBranch();

            lblorderstatus.Text = (hidtab1orderstatus.Value != "") ? hidtab1orderstatus.Value : "ใบสั่งขายใหม่";
            lblordercode.Text = hidtab1ordercode.Value;

            txtTransportPrice.Text = (hidtab1transportprice.Value != "") ? hidtab1transportprice.Value : "40";
            btnCalculateTotal_Click(null, null);
            
            txtcustomerpay.Text = (hidtab1customerpay.Value != "") ? hidtab1customerpay.Value : "";
            txtReturnCashAMount.Text = (hidtab1ReturnCashAMount.Value != "") ? hidtab1ReturnCashAMount.Value : "";

            txtOrderNoteLast.InnerText = hidtab1OrderNote.Value;
        }
        protected void btntab2_Click(object sender, EventArgs e)
        {
            //check top5 
            if(hidtab2CampaignCategory.Value == "")
            {
                BindTop5Product("-99");
            }
            else
            {
                BindTop5Product(hidtab2CampaignCategory.Value);
            }
            //end check top5

            hidtab.Value = "2";
            LoadgvOrder(L_orderdata2);
            lblCampaignCategory.Text = hidtab2CampaignCategoryname.Value;

            btntab1.BackColor = System.Drawing.Color.Red;
            btntab2.BackColor = System.Drawing.Color.CadetBlue;
            btntab3.BackColor = System.Drawing.Color.Red;

            if (hidtab2orderstatus.Value == "")
            {
                btncheckout.Visible = true;
                btnNewOrder.Visible = true;
                btnCloseTab.Visible = true;
                btnClear.Visible = true;
            }
            else
            {
                btncheckout.Visible = false;
                btnNewOrder.Visible = false;
                btnCloseTab.Visible = false;
                btnClear.Visible = false;
            }

            if (L_transportdata2.Count != 0)
            {
                lblCustomerAddress.Text = L_transportdata2[0].Address + " " + L_transportdata2[0].SubDistrictName
                + " " + L_transportdata2[0].DistrictName + " " + L_transportdata2[0].ProvinceName + " " + L_transportdata2[0].Zipcode;

                dtlNearestBranch.DataSource = L_transportdata2[0].NearestBranchList;
                dtlNearestBranch.DataBind();
            }
            else
            {
                lblCustomerAddress.Text = "";

                dtlNearestBranch.DataSource = null;
                dtlNearestBranch.DataBind();
            }

            ClickRadioBranch();

            lblorderstatus.Text = (hidtab2orderstatus.Value != "") ? hidtab2orderstatus.Value : "ใบสั่งขายใหม่";
            lblordercode.Text = hidtab2ordercode.Value;

            txtTransportPrice.Text = (hidtab2transportprice.Value != "") ? hidtab2transportprice.Value : "40";
            btnCalculateTotal_Click(null, null);

            txtcustomerpay.Text = (hidtab2customerpay.Value != "") ? hidtab2customerpay.Value : "";
            txtReturnCashAMount.Text = (hidtab2ReturnCashAMount.Value != "") ? hidtab2ReturnCashAMount.Value : "";

            txtOrderNoteLast.InnerText = hidtab2OrderNote.Value;

        }
        protected void btntab3_Click(object sender, EventArgs e)
        {
            //check top5 
            if (hidtab3CampaignCategory.Value == "")
            {
                BindTop5Product("-99");
            }
            else
            {
                BindTop5Product(hidtab3CampaignCategory.Value);
            }
            //end check top5

            hidtab.Value = "3";
            LoadgvOrder(L_orderdata3);
            lblCampaignCategory.Text = hidtab3CampaignCategoryname.Value;

            btntab1.BackColor = System.Drawing.Color.Red;
            btntab2.BackColor = System.Drawing.Color.Red;
            btntab3.BackColor = System.Drawing.Color.CadetBlue;

            if (hidtab3orderstatus.Value == "")
            {
                btncheckout.Visible = true;
                btnNewOrder.Visible = true;
                btnCloseTab.Visible = true;
                btnClear.Visible = true;
            }
            else
            {
                btncheckout.Visible = false;
                btnNewOrder.Visible = false;
                btnCloseTab.Visible = false;
                btnClear.Visible = false;
            }

            if (L_transportdata3.Count != 0)
            {
                lblCustomerAddress.Text = L_transportdata3[0].Address + " " + L_transportdata3[0].SubDistrictName
                + " " + L_transportdata3[0].DistrictName + " " + L_transportdata3[0].ProvinceName + " " + L_transportdata3[0].Zipcode;

                dtlNearestBranch.DataSource = L_transportdata3[0].NearestBranchList;
                dtlNearestBranch.DataBind();
            }
            else
            {
                lblCustomerAddress.Text = "";

                dtlNearestBranch.DataSource = null;
                dtlNearestBranch.DataBind();
            }

            ClickRadioBranch();

            lblorderstatus.Text = (hidtab3orderstatus.Value != "") ? hidtab3orderstatus.Value : "ใบสั่งขายใหม่";
            lblordercode.Text = hidtab3ordercode.Value;

            txtTransportPrice.Text = (hidtab3transportprice.Value != "") ? hidtab3transportprice.Value : "40";
            btnCalculateTotal_Click(null, null);

            txtcustomerpay.Text = (hidtab3customerpay.Value != "") ? hidtab3customerpay.Value : "";
            txtReturnCashAMount.Text = (hidtab3ReturnCashAMount.Value != "") ? hidtab3ReturnCashAMount.Value : "";

            txtOrderNoteLast.InnerText = hidtab3OrderNote.Value;
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;

            TextBox txtAmount = (TextBox)currentRow.FindControl("txtAmount");
            Label lbltotal = (Label)currentRow.FindControl("lbltotal");
            HiddenField hidRunning = (HiddenField)currentRow.FindControl("hidRunning");

            List<OrderData> lorderdata = new List<OrderData>();

            if (hidtab.Value == "1")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode1.Value != "" && hidSelectedBranchCode1.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }


                lorderdata = LoadOrderdata(L_orderdata1, txtAmount, lbltotal, hidRunning);

                L_orderdata1 = lorderdata;
                LoadgvOrder(L_orderdata1);
            }
            else
            if (hidtab.Value == "2")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode2.Value != "" && hidSelectedBranchCode2.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }

                lorderdata = LoadOrderdata(L_orderdata2, txtAmount, lbltotal, hidRunning);

                L_orderdata2 = lorderdata;
                LoadgvOrder(L_orderdata2);
            }
            else
            if (hidtab.Value == "3")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode3.Value != "" && hidSelectedBranchCode3.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }

                lorderdata = LoadOrderdata(L_orderdata3, txtAmount, lbltotal, hidRunning);

                L_orderdata3 = lorderdata;
                LoadgvOrder(L_orderdata3);
            }

            btnCalculateTotal_Click(null, null);
        }

        protected void btnCalculateTotal_Click(object sender, EventArgs e)
        {
            if (hidtab.Value == "1")
            {
                Loadtotal(L_orderdata1);
            }
            else if (hidtab.Value == "2")
            {
                Loadtotal(L_orderdata2);
            }
            else if (hidtab.Value == "3")
            {
                Loadtotal(L_orderdata3);
            }

        }

        protected void btnNewOrder_Click(object sender, EventArgs e)
        {
            List<OrderData> lorderdata = new List<OrderData>();

            if (L_orderdata1.Count == 0)
            {
                hidtab.Value = "1";

                if (L_orderdata1.Count > 0)
                {
                    lorderdata = L_orderdata1;
                }

                for (int i = 0; i < gvOrder.Rows.Count; i++)
                {
                    CheckBox checkbox = (CheckBox)gvOrder.Rows[i].FindControl("chkOrder");

                    HiddenField hidCampaignCategoryCode = gvOrder.Rows[i].FindControl("hidCampaignCategoryCode") as HiddenField;
                    HiddenField hidCampaignCategoryName = gvOrder.Rows[i].FindControl("hidCampaignCategoryName") as HiddenField;
                    HiddenField hidProductName = gvOrder.Rows[i].FindControl("hidProductName") as HiddenField;
                    HiddenField hidProductCode = gvOrder.Rows[i].FindControl("hidProductCode") as HiddenField;
                    HiddenField hidDiscountAmount = gvOrder.Rows[i].FindControl("hidDiscountAmount") as HiddenField;
                    HiddenField hidDiscountPercent = gvOrder.Rows[i].FindControl("hidDiscountPercent") as HiddenField;
                    HiddenField hidCampaignCode = gvOrder.Rows[i].FindControl("hidCampaignCode") as HiddenField;
                    HiddenField hidPromotionCode = gvOrder.Rows[i].FindControl("hidPromotionCode") as HiddenField;
                    HiddenField hidPrice = gvOrder.Rows[i].FindControl("hidPrice") as HiddenField;
                    HiddenField hidAmount = gvOrder.Rows[i].FindControl("hidAmount") as HiddenField;
                    HiddenField hidPromotionDetailId = gvOrder.Rows[i].FindControl("hidPromotionDetailId") as HiddenField;
                    HiddenField hidParentProductCode = gvOrder.Rows[i].FindControl("hidParentProductCode") as HiddenField;
                    HiddenField hidFlagCombo = gvOrder.Rows[i].FindControl("hidFlagCombo") as HiddenField;
                    HiddenField hidComboName = gvOrder.Rows[i].FindControl("hidComboName") as HiddenField;
                    HiddenField hidComboCode = gvOrder.Rows[i].FindControl("hidComboCode") as HiddenField;


                    if (checkbox.Checked == true)
                    {
                        OrderData odata = new OrderData();
                        odata.CampaignCategory = hidCampaignCategoryCode.Value;
                        odata.CampaignCategoryName = hidCampaignCategoryName.Value;
                        odata.PromotionDetailId = (hidPromotionDetailId.Value != "") ? Convert.ToInt32(hidPromotionDetailId.Value) : 0;
                        odata.CampaignCode = hidCampaignCode.Value;
                        odata.PromotionCode = hidPromotionCode.Value;
                        odata.ProductCode = hidProductCode.Value;
                        odata.ProductName = hidProductName.Value;
                        odata.ComboCode = hidComboCode.Value;
                        odata.ComboName = hidComboName.Value;
                        odata.ParentProductCode = hidParentProductCode.Value;
                        odata.FlagCombo = hidFlagCombo.Value;
                        odata.DiscountAmount = (hidDiscountAmount.Value != "") ? Convert.ToInt32(hidDiscountAmount.Value) : 0;
                        odata.DiscountPercent = (hidDiscountPercent.Value != "") ? Convert.ToInt32(hidDiscountPercent.Value) : 0;
                        odata.Price = (hidPrice.Value != "") ? Convert.ToDouble(hidPrice.Value) : -99;
                        odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;
                        odata.Amount = (hidAmount.Value != "") ? Convert.ToInt32(hidAmount.Value) : 0;
                        hidtab1CampaignCategory.Value = hidCampaignCategoryCode.Value;
                        hidtab1CampaignCategoryname.Value = hidCampaignCategoryName.Value;
                        odata.runningNo = lorderdata.Count + 1;
                        lorderdata.Add(odata);
                    }
                }

                L_orderdata1 = lorderdata;

                btntab1.BackColor = System.Drawing.Color.CadetBlue;
                btntab2.BackColor = System.Drawing.Color.Red;
                btntab3.BackColor = System.Drawing.Color.Red;

                LoadgvOrder(L_orderdata1);

                lblCampaignCategory.Text = hidtab1CampaignCategoryname.Value;

            }
            else if (L_orderdata2.Count == 0)
            {
                hidtab.Value = "2";

                if (L_orderdata2.Count > 0)
                {
                    lorderdata = L_orderdata1;
                }

                for (int i = 0; i < gvOrder.Rows.Count; i++)
                {
                    CheckBox checkbox = (CheckBox)gvOrder.Rows[i].FindControl("chkOrder");

                    HiddenField hidCampaignCategoryCode = gvOrder.Rows[i].FindControl("hidCampaignCategoryCode") as HiddenField;
                    HiddenField hidCampaignCategoryName = gvOrder.Rows[i].FindControl("hidCampaignCategoryName") as HiddenField;
                    HiddenField hidProductName = gvOrder.Rows[i].FindControl("hidProductName") as HiddenField;
                    HiddenField hidProductCode = gvOrder.Rows[i].FindControl("hidProductCode") as HiddenField;
                    HiddenField hidDiscountAmount = gvOrder.Rows[i].FindControl("hidDiscountAmount") as HiddenField;
                    HiddenField hidDiscountPercent = gvOrder.Rows[i].FindControl("hidDiscountPercent") as HiddenField;
                    HiddenField hidCampaignCode = gvOrder.Rows[i].FindControl("hidCampaignCode") as HiddenField;
                    HiddenField hidPromotionCode = gvOrder.Rows[i].FindControl("hidPromotionCode") as HiddenField;
                    HiddenField hidPrice = gvOrder.Rows[i].FindControl("hidPrice") as HiddenField;
                    HiddenField hidAmount = gvOrder.Rows[i].FindControl("hidAmount") as HiddenField;
                    HiddenField hidPromotionDetailId = gvOrder.Rows[i].FindControl("hidPromotionDetailId") as HiddenField;
                    HiddenField hidParentProductCode = gvOrder.Rows[i].FindControl("hidParentProductCode") as HiddenField;
                    HiddenField hidFlagCombo = gvOrder.Rows[i].FindControl("hidFlagCombo") as HiddenField;
                    HiddenField hidComboName = gvOrder.Rows[i].FindControl("hidComboName") as HiddenField;
                    HiddenField hidComboCode = gvOrder.Rows[i].FindControl("hidComboCode") as HiddenField;


                    if (checkbox.Checked == true)
                    {
                        OrderData odata = new OrderData();
                        odata.CampaignCategory = hidCampaignCategoryCode.Value;
                        odata.CampaignCategoryName = hidCampaignCategoryName.Value;
                        odata.CampaignCode = hidCampaignCode.Value;
                        odata.PromotionDetailId = (hidPromotionDetailId.Value != "") ? Convert.ToInt32(hidPromotionDetailId.Value) : 0;
                        odata.PromotionCode = hidPromotionCode.Value;
                        odata.ProductCode = hidProductCode.Value;
                        odata.ProductName = hidProductName.Value;
                        odata.ComboCode = hidComboCode.Value;
                        odata.ComboName = hidComboName.Value;
                        odata.ParentProductCode = hidParentProductCode.Value;
                        odata.FlagCombo = hidFlagCombo.Value;
                        odata.DiscountAmount = (hidDiscountAmount.Value != "") ? Convert.ToInt32(hidDiscountAmount.Value) : 0;
                        odata.DiscountPercent = (hidDiscountPercent.Value != "") ? Convert.ToInt32(hidDiscountPercent.Value) : 0;
                        odata.Price = (hidPrice.Value != "") ? Convert.ToDouble(hidPrice.Value) : -99;
                        odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;
                        odata.Amount = (hidAmount.Value != "") ? Convert.ToInt32(hidAmount.Value) : 0;

                        hidtab2CampaignCategory.Value = hidCampaignCategoryCode.Value;
                        hidtab2CampaignCategoryname.Value = hidCampaignCategoryName.Value;
                        odata.runningNo = lorderdata.Count + 1;
                        lorderdata.Add(odata);
                    }
                }

                L_orderdata2 = lorderdata;

                btntab1.BackColor = System.Drawing.Color.Red;
                btntab2.BackColor = System.Drawing.Color.CadetBlue;
                btntab3.BackColor = System.Drawing.Color.Red;


                LoadgvOrder(L_orderdata2);

                lblCampaignCategory.Text = hidtab2CampaignCategoryname.Value;
            }
            else if (L_orderdata3.Count == 0)
            {
                hidtab.Value = "3";

                if (L_orderdata1.Count > 0)
                {
                    lorderdata = L_orderdata3;
                }

                for (int i = 0; i < gvOrder.Rows.Count; i++)
                {
                    CheckBox checkbox = (CheckBox)gvOrder.Rows[i].FindControl("chkOrder");

                    HiddenField hidPromotionDetailId = gvOrder.Rows[i].FindControl("hidPromotionDetailId") as HiddenField;
                    HiddenField hidCampaignCategoryCode = gvOrder.Rows[i].FindControl("hidCampaignCategoryCode") as HiddenField;
                    HiddenField hidCampaignCategoryName = gvOrder.Rows[i].FindControl("hidCampaignCategoryName") as HiddenField;
                    HiddenField hidProductName = gvOrder.Rows[i].FindControl("hidProductName") as HiddenField;
                    HiddenField hidProductCode = gvOrder.Rows[i].FindControl("hidProductCode") as HiddenField;
                    HiddenField hidDiscountAmount = gvOrder.Rows[i].FindControl("hidDiscountAmount") as HiddenField;
                    HiddenField hidDiscountPercent = gvOrder.Rows[i].FindControl("hidDiscountPercent") as HiddenField;
                    HiddenField hidCampaignCode = gvOrder.Rows[i].FindControl("hidCampaignCode") as HiddenField;
                    HiddenField hidPromotionCode = gvOrder.Rows[i].FindControl("hidPromotionCode") as HiddenField;
                    HiddenField hidPrice = gvOrder.Rows[i].FindControl("hidPrice") as HiddenField;
                    HiddenField hidAmount = gvOrder.Rows[i].FindControl("hidAmount") as HiddenField;
                    HiddenField hidParentProductCode = gvOrder.Rows[i].FindControl("hidParentProductCode") as HiddenField;
                    HiddenField hidFlagCombo = gvOrder.Rows[i].FindControl("hidFlagCombo") as HiddenField;
                    HiddenField hidComboName = gvOrder.Rows[i].FindControl("hidComboName") as HiddenField;
                    HiddenField hidComboCode = gvOrder.Rows[i].FindControl("hidComboCode") as HiddenField;


                    if (checkbox.Checked == true)
                    {
                        OrderData odata = new OrderData();
                        odata.CampaignCategory = hidCampaignCategoryCode.Value;
                        odata.CampaignCategoryName = hidCampaignCategoryName.Value;
                        odata.CampaignCode = hidCampaignCode.Value;
                        odata.PromotionCode = hidPromotionCode.Value;
                        odata.ProductCode = hidProductCode.Value;
                        odata.ComboCode = hidComboCode.Value;
                        odata.ComboName = hidComboName.Value;
                        odata.ParentProductCode = hidParentProductCode.Value;
                        odata.FlagCombo = hidFlagCombo.Value;
                        odata.PromotionDetailId = (hidPromotionDetailId.Value != "") ? Convert.ToInt32(hidPromotionDetailId.Value) : 0;
                        odata.ProductName = hidProductName.Value;
                        odata.DiscountAmount = (hidDiscountAmount.Value != "") ? Convert.ToInt32(hidDiscountAmount.Value) : 0;
                        odata.DiscountPercent = (hidDiscountPercent.Value != "") ? Convert.ToInt32(hidDiscountPercent.Value) : 0;
                        odata.Price = (hidPrice.Value != "") ? Convert.ToDouble(hidPrice.Value) : -99;
                        odata.SumPrice = ((odata.Price - ((odata.Price * odata.DiscountPercent) / 100)) - odata.DiscountAmount) * odata.Amount;
                        odata.Amount = (hidAmount.Value != "") ? Convert.ToInt32(hidAmount.Value) : 0;

                        hidtab3CampaignCategory.Value = hidCampaignCategoryCode.Value;
                        hidtab3CampaignCategoryname.Value = hidCampaignCategoryName.Value;
                        odata.runningNo = lorderdata.Count + 1;
                        lorderdata.Add(odata);
                    }
                }

                L_orderdata3 = lorderdata;

                btntab1.BackColor = System.Drawing.Color.Red;
                btntab2.BackColor = System.Drawing.Color.Red;
                btntab3.BackColor = System.Drawing.Color.CadetBlue;

                LoadgvOrder(L_orderdata3);

                lblCampaignCategory.Text = hidtab3CampaignCategoryname.Value;
            }
        }

        protected void btnVoucher_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmitSelect_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelSelect_Click(object sender, EventArgs e)
        {

        }

        protected void txtCashOnChange(object sender, EventArgs e)
        {

        }

        protected void btnSelectComboset_Click(object sender, EventArgs e)
        {
            int cou = 0;
            string parentproductcode = "";
            if (hidtab.Value == "1")
            {
                cou = (hidtab1countcomboset.Value != "") ? Convert.ToInt32(hidtab1countcomboset.Value) + 1 : 1;
                hidtab1countcomboset.Value = cou.ToString();
                parentproductcode = cou.ToString() + lblCombosetCode.Text;
            }
            else if (hidtab.Value == "2")
            {
                cou = (hidtab2countcomboset.Value != "") ? Convert.ToInt32(hidtab2countcomboset.Value) + 1 : 1;
                hidtab2countcomboset.Value = cou.ToString();
                parentproductcode = cou.ToString() + lblCombosetCode.Text;
            }
            else if (hidtab.Value == "3")
            {
                cou = (hidtab3countcomboset.Value != "") ? Convert.ToInt32(hidtab3countcomboset.Value) + 1 : 1;
                hidtab3countcomboset.Value = cou.ToString();
                parentproductcode = cou.ToString() + lblCombosetCode.Text;
            }

            //Bind Comboset parent

            OrderData odata = new OrderData();

            odata.CampaignCode = hidCombosetCampaignCode.Value;
            odata.ComboCode = parentproductcode;
            odata.PromotionDetailId = (lblCombosetCode.Text != "") ? Convert.ToInt32(lblCombosetCode.Text) : 0;
            odata.ComboName = lblCombosetName.Text;
            odata.ProductName = lblCombosetName.Text;
            odata.DiscountAmount = 0;
            odata.DiscountPercent = 0;
            odata.Price = Convert.ToDouble(lblCombosetPrice.Text);
            odata.SumPrice = 0;
            odata.Amount = 1;
            odata.SumPrice = Convert.ToDouble(lblCombosetPrice.Text) * odata.Amount;
            odata.FlagCombo = "Y";


            LoadOrderdata(odata);

            //Bind Sub comboset
            for (int i = 0; i < gvSubMainPromotionDetail.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvSubMainPromotionDetail.Rows[i].FindControl("chkSubMainPromotionDetail");

                HiddenField hidCampaignCategoryCode = gvSubMainPromotionDetail.Rows[i].FindControl("hidCampaignCategoryCode") as HiddenField;
                HiddenField hidProductName = gvSubMainPromotionDetail.Rows[i].FindControl("hidProductName") as HiddenField;
                HiddenField hidProductCode = gvSubMainPromotionDetail.Rows[i].FindControl("hidProductCode") as HiddenField;
                HiddenField hidCampaignCode = gvSubMainPromotionDetail.Rows[i].FindControl("hidCampaignCode") as HiddenField;
                HiddenField hidPromotionCode = gvSubMainPromotionDetail.Rows[i].FindControl("hidPromotionCode") as HiddenField;
                Label lblAmount = gvSubMainPromotionDetail.Rows[i].FindControl("lblAmount") as Label;


                if (checkbox.Checked == true)
                {
                    odata = new OrderData();
                    odata.CampaignCategory = hidCampaignCategoryCode.Value;
                    odata.CampaignCode = hidCampaignCode.Value;
                    odata.ProductCode = hidProductCode.Value;
                    odata.ProductName = hidProductName.Value;
                    odata.DiscountAmount = 0;
                    odata.DiscountPercent = 0;
                    odata.Price = 0;
                    odata.SumPrice = 0;
                    odata.Amount = (lblAmount.Text != "") ? Convert.ToInt32(lblAmount.Text) : 0;
                    odata.SumPrice = 0;
                    odata.FlagCombo = "Y";
                    odata.ParentProductCode = parentproductcode;
                    odata.PromotionDetailId = 0;

                    LoadOrderdata(odata);

                }

            }
            for (int i = 0; i < gvSubExchangePromotionDetail.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvSubExchangePromotionDetail.Rows[i].FindControl("chkSubExchangePromotionDetail");

                HiddenField hidCampaignCategoryCode = gvSubExchangePromotionDetail.Rows[i].FindControl("hidCampaignCategoryCode") as HiddenField;
                HiddenField hidProductName = gvSubExchangePromotionDetail.Rows[i].FindControl("hidProductName") as HiddenField;
                HiddenField hidProductCode = gvSubExchangePromotionDetail.Rows[i].FindControl("hidProductCode") as HiddenField;
                HiddenField hidCampaignCode = gvSubExchangePromotionDetail.Rows[i].FindControl("hidCampaignCode") as HiddenField;
                HiddenField hidPromotionCode = gvSubExchangePromotionDetail.Rows[i].FindControl("hidPromotionCode") as HiddenField;
                Label lblAmount = gvSubExchangePromotionDetail.Rows[i].FindControl("lblAmount") as Label;


                if (checkbox.Checked == true)
                {
                    odata = new OrderData();
                    odata.CampaignCategory = hidCampaignCategoryCode.Value;
                    odata.CampaignCode = hidCampaignCode.Value;
                    odata.ProductCode = hidProductCode.Value;
                    odata.ProductName = hidProductName.Value;
                    odata.DiscountAmount = 0;
                    odata.DiscountPercent = 0;
                    odata.Price = 0;
                    odata.SumPrice = 0;
                    odata.Amount = (lblAmount.Text != "") ? Convert.ToInt32(lblAmount.Text) : 0;
                    odata.SumPrice = 0;
                    odata.FlagCombo = "Y";
                    odata.ParentProductCode = parentproductcode;
                    odata.PromotionDetailId = 0;

                    LoadOrderdata(odata);

                }

            }

            btnCalculateTotal_Click(null, null);
        }

        protected void btnCancelComboset_Click(object sender, EventArgs e)
        {

        }

        protected void chkSubMainPromotionDetailAll_Check(object sender, EventArgs e)
        {
            for (int i = 0; i < gvSubMainPromotionDetail.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvSubMainPromotionDetail.HeaderRow.FindControl("chkSubMainPromotionDetailAll");

                if (chkall.Checked == true)
                {


                    CheckBox chkSubMainPromotionDetail = (CheckBox)gvSubMainPromotionDetail.Rows[i].FindControl("chkSubMainPromotionDetail");

                    chkSubMainPromotionDetail.Checked = true;
                }
                else
                {

                    CheckBox chkSubMainPromotionDetail = (CheckBox)gvSubMainPromotionDetail.Rows[i].FindControl("chkSubMainPromotionDetail");

                    chkSubMainPromotionDetail.Checked = false;
                }

            }

        }

        protected void chkSubExchangePromotionDetailAll_Check(object sender, EventArgs e)
        {
            for (int i = 0; i < gvSubExchangePromotionDetail.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvSubExchangePromotionDetail.HeaderRow.FindControl("chkSubExchangePromotionDetailAll");

                if (chkall.Checked == true)
                {


                    CheckBox chkSubExchangePromotionDetail = (CheckBox)gvSubExchangePromotionDetail.Rows[i].FindControl("chkSubExchangePromotionDetail");

                    chkSubExchangePromotionDetail.Checked = true;
                }
                else
                {

                    CheckBox chkSubExchangePromotionDetail = (CheckBox)gvSubExchangePromotionDetail.Rows[i].FindControl("chkSubExchangePromotionDetail");

                    chkSubExchangePromotionDetail.Checked = false;
                }

            }

        }
        protected void gvPromotionDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                HiddenField hidProductDesc = (HiddenField)e.Row.FindControl("hidProductDesc");
                HiddenField hidPromotionDetailId = (HiddenField)e.Row.FindControl("hidPromotionDetailId");
                
                if (hidflagcombo.Value != "Y")
                {
                 

                }
                else
                {
               
                    List<SubPromotionDetailInfo> lsubmain= new List<SubPromotionDetailInfo>();

                    lsubmain = LoadSubMainPromotionDetailforBindProductDesc(hidPromotionDetailId.Value);


                    List<SubPromotionDetailInfo> lsubexchange = new List<SubPromotionDetailInfo>();

                    lsubexchange = LoadSubExchangePromotionDetailforBindProductDesc(hidPromotionDetailId.Value);

                    string strprd = "";

                    foreach(var item in lsubmain)
                    {
                        strprd += "-" + item.MainProductName + "<br>";
                    }
                    foreach (var item in lsubexchange)
                    {
                        strprd += "-" + item.ExchangeProductName + "<br>";
                    }

                 

                }
            }


        }
        protected void txtTransportPrice_TextChanged(object sender, EventArgs e)
        {
            if (hidtab.Value == "1")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode1.Value != "" && hidSelectedBranchCode1.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }

                hidtab1transportprice.Value = txtTransportPrice.Text;
            }
            else if (hidtab.Value == "2")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode2.Value != "" && hidSelectedBranchCode2.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }

                hidtab2transportprice.Value = txtTransportPrice.Text;
            }
            else if (hidtab.Value == "3")
            {
                int count = dtlNearestBranch.Items.Count;


                for (int i = 0; i < count; i++)
                {

                    RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                    HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                    if (hidSelectedBranchCode3.Value != "" && hidSelectedBranchCode3.Value == bCode.Value)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                    }
                }

                hidtab3transportprice.Value = txtTransportPrice.Text;
            }


            btnCalculateTotal_Click(null, null);
        }

        protected void txtcustomerpay_TextChanged(object sender, EventArgs e)
        {
            

            if (IsDouble(txtcustomerpay.Text))
            {              
                Double cashreturn = Convert.ToDouble(txtcustomerpay.Text) - Convert.ToDouble(txtTotalPrice.Text);

                txtcustomerpay.Text = string.Format("{0:n}", Convert.ToDouble(txtcustomerpay.Text));

                if (cashreturn >= 0)
                {
                    lblReturnCashAMount.Text = "";
                    
                    txtReturnCashAMount.Text = string.Format("{0:n}", cashreturn);


                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "displayPayment();", true);



                    if (hidtab.Value == "1")
                    {
                        int count = dtlNearestBranch.Items.Count;


                        for (int i = 0; i < count; i++)
                        {
                           
                            RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                            HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                            if (hidSelectedBranchCode1.Value != "" && hidSelectedBranchCode1.Value == bCode.Value)
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                            }
                        }

                        hidtab1customerpay.Value = txtcustomerpay.Text;
                        hidtab1ReturnCashAMount.Value = cashreturn.ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "displayPayment();", true);
                    }
                    else if (hidtab.Value == "2")
                    {
                        int count = dtlNearestBranch.Items.Count;


                        for (int i = 0; i < count; i++)
                        {

                            RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                            HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                            if (hidSelectedBranchCode2.Value != "" && hidSelectedBranchCode2.Value == bCode.Value)
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                            }
                        }

                        hidtab2customerpay.Value = txtcustomerpay.Text;
                        hidtab2ReturnCashAMount.Value = cashreturn.ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "displayPayment();", true);
                    }
                    else if (hidtab.Value == "3")
                    {
                        int count = dtlNearestBranch.Items.Count;


                        for (int i = 0; i < count; i++)
                        {

                            RadioButton r = (RadioButton)dtlNearestBranch.Items[i].FindControl("radBranch");
                            HiddenField bCode = (HiddenField)dtlNearestBranch.Items[i].FindControl("hidBranchCode");
                            if (hidSelectedBranchCode3.Value != "" && hidSelectedBranchCode3.Value == bCode.Value)
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "clicked('" + r.ClientID + "');", true);
                            }
                        }


                        hidtab3customerpay.Value = txtcustomerpay.Text;
                        hidtab3ReturnCashAMount.Value = cashreturn.ToString();
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "displayPayment();", true);
                    }
                }
                else
                {
                    lblReturnCashAMount.Text = "จำนวนเงินชำระไม่เพียงพอ";
                    txtcustomerpay.Text = "0";
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "displayPayment();", true);
                }
            }
            else
            {
                txtcustomerpay.Text = "0";
            }                      

        }

        protected void SubmitOrder_Click(object sender, EventArgs e)
        {
            ClickRadioBranch();
            EmpInfo empInfo = new EmpInfo();
            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            double? sumvat = 0;
            double? sumtotal = 0;
            string APIpath = "";

            if (IsDouble(txtcustomerpay.Text))
            {
                Double cashreturn = Convert.ToDouble(txtcustomerpay.Text) - Convert.ToDouble(txtTotalPrice.Text);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "Load", "alert('" + "กรุณาใส่เงินชำระ" + "');displayPayment();", true);
            }

            string branchcode = "";

           string strordercode = "";
            CustomerCode = (Request.QueryString["CustomerCode"] != null) ? Request.QueryString["CustomerCode"].ToString() : "";
            CustomerPhone = (Request.QueryString["Customerphone"] != null) ? Request.QueryString["Customerphone"].ToString() : "";

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                UpdateCustomerNoteProfile(CustomerCode);
                string x = txttimePreOrder.Value;
                if (hidtab.Value == "1")
                {
                    if (ValidateSubmitOrder())
                    {
                        hidtab1transportprice.Value = txtTransportPrice.Text;
                        hidtab1OrderNote.Value = txtOrderNoteLast.InnerText;
                        foreach (var lodt in L_orderdata1)
                        {
                            sumvat += (((((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount) * 7) / 100);
                            sumtotal += (((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount);
                        }

                        //Insert orderInfo
                        APIpath = APIUrl + "/api/support/InsertMKOrderdata";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerCode"] = CustomerCode;
                            data["CustomerPhone"] = CustomerPhone;
                            data["SubTotalPrice"] = sumtotal.ToString();
                            data["Vat"] = sumvat.ToString();
                            data["TransportPrice"] = hidtab1transportprice.Value;
                            data["Customerpay"] = hidtab1customerpay.Value;
                            if (radordernow.Checked == true)
                            {
                                data["SALEORDERTYPE"] = StaticField.SaleOrderType_01; 
                            }
                            else
                            {
                                data["SALEORDERTYPE"] = StaticField.SaleOrderType_02; 
                            }

                            data["ReturnCashAMount"] = hidtab1ReturnCashAMount.Value;
                            data["TotalPrice"] = (sumtotal + sumvat + Convert.ToDouble(hidtab1transportprice.Value)).ToString();
                            data["OrderStatusCode"] = StaticField.OrderStatus_01; 

                            if (radordernow.Checked == true)
                            {
                                data["OrderStateCode"] = StaticField.OrderState_01; 
                            }
                            else
                            {
                                data["OrderStateCode"] = StaticField.OrderState_10; 
                            }

                            data["UpdateBy"] = empInfo.EmpCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";
                            data["CampaignCategoryCode"] = hidtab1CampaignCategory.Value;
                            data["BranchCode"] = hidSelectedBranchCode1.Value;
                            data["LandmarkLat"] = hidLandmarkLat1.Value;
                            data["LandmarkLng"] = hidLandmarkLng1.Value;
                            data["OrderNote"] = hidtab1OrderNote.Value;
                            data["ChannelCode"] = StaticField.ChannelCode_Tel; 
                            data["FlagApproved"] = "N";

                            if (radordernow.Checked == true)
                            {
                                DateTime currentTime = DateTime.Now;
                                DateTime x45MinsLater = currentTime.AddMinutes(45);
                                data["DeliveryDate"] = x45MinsLater.ToString();
                            }
                            else
                            {
                                String Date = txtdPreOrder.Value;
                                
                                String TimeHr = txtPreOrderHr.Value;
                                String TimeMin = txtPreOrderMin.Value;
                                
                                data["DeliveryDate"] = Date + " " + TimeHr + ":" + TimeMin;
                            }

                            var response = wb.UploadValues(APIpath, "POST", data);

                            strordercode = Encoding.UTF8.GetString(response);
                            strordercode = strordercode.Replace("\"", "");
                        }

                        //Insert Orderdetailinfo


                        foreach (var lodt in L_orderdata1)
                        {
                            string respstringorderdetail = "";
                            APIpath = APIUrl + "/api/support/InsertMKOrderdetaildata";

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
                                data["Unit"] = lodt.Unit;
                                data["Amount"] = lodt.Amount.ToString();
                                data["UpdateBy"] = empInfo.EmpCode;
                                data["CreateBy"] = empInfo.EmpCode;
                                data["FlagDelete"] = "N";
                                data["OrderCode"] = strordercode;
                                data["ParentProductCode"] = lodt.ParentProductCode;
                                data["FlagCombo"] = lodt.FlagCombo;
                                data["ComboCode"] = lodt.ComboCode;
                                data["ComboName"] = lodt.ComboName;
                                data["PromotionCode"] = lodt.PromotionCode;
                                data["CampaignCode"] = lodt.CampaignCode;
                                data["CampaignCategory"] = lodt.CampaignCategory;

                                var response = wb.UploadValues(APIpath, "POST", data);

                                respstringorderdetail = Encoding.UTF8.GetString(response);
                            }
                        }

                        // insert paymentdata
                        paymentdataInfo pdata = new paymentdataInfo();
                        List<paymentdataInfo> lpdata = new List<paymentdataInfo>();

                        pdata.OrderCode = strordercode;
                        pdata.PaymentTypeCode = StaticField.PaymentType01; 
                        pdata.Payamount = Convert.ToDouble(txtTotalPrice.Text);
                        pdata.CreateBy = hidEmpCode.Value;
                        pdata.UpdateBy = hidEmpCode.Value;
                        pdata.FlagDelete = "N";

                        lpdata.Add(pdata);

                        foreach (var paymentV in lpdata)
                        {
                            string respstring = "";
                            APIpath = APIUrl + "/api/support/L_paymentMKInsert";
                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();

                                data["OrderCode"] = paymentV.OrderCode;
                                data["PaymentTypeCode"] = paymentV.PaymentTypeCode;
                                data["Payamount"] = paymentV.Payamount.ToString();
                                data["CreateBy"] = paymentV.CreateBy;
                                data["UpdateBy"] = paymentV.UpdateBy;
                                data["FlagDelete"] = "N";

                                var response = wb.UploadValues(APIpath, "POST", data);

                                respstring = Encoding.UTF8.GetString(response);
                            }
                        }

                        // insert transportdata
                        foreach (var tdataV in L_transportdata1)
                        {
                            string respstringorderdetail = "";
                            APIpath = APIUrl + "/api/support/InsertMKTransportdata";

                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();

                                data["OrderCode"] = strordercode;
                                data["Address"] = tdataV.Address;
                                data["SubDistrictCode"] = tdataV.SubDistrictCode;
                                data["DistrictCode"] = tdataV.DistrictCode;
                                data["ProvinceCode"] = tdataV.ProvinceCode;
                                data["Zipcode"] = tdataV.Zipcode;
                                data["TransportPrice"] = hidtab1transportprice.Value;
                                data["AddressType"] = tdataV.AddressType;
                                data["UpdateBy"] = empInfo.EmpCode;
                                data["CreateBy"] = empInfo.EmpCode;
                                data["BranchCode"] = hidSelectedBranchCode1.Value;

                                var response = wb.UploadValues(APIpath, "POST", data);

                                respstringorderdetail = Encoding.UTF8.GetString(response);
                            }
                        }

                        CallOneAppAPI1(strordercode);

                        hidtab1orderstatus.Value = "สร้างใบสั่งขาย";
                        hidtab1ordercode.Value = strordercode;
                        lblorderstatus.Text = hidtab1orderstatus.Value;
                        lblordercode.Text = hidtab1ordercode.Value;

                        
                        btntab1_Click(null, null);
                        LoadOrderNote(CustomerCode);

                        branchcode = hidSelectedBranchCode1.Value;
                    }
                    
                }
                else if (hidtab.Value == "2")
                {
                    hidtab2transportprice.Value = txtTransportPrice.Text;
                    hidtab2OrderNote.Value = txtOrderNoteLast.InnerText;
                    foreach (var lodt in L_orderdata2)
                    {
                        sumvat += (((((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount) * 7) / 100);
                        sumtotal += (((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount);
                    }

                    //Insert orderInfo
                    APIpath = APIUrl + "/api/support/InsertMKOrderdata";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = CustomerCode;
                        data["CustomerPhone"] = CustomerPhone;
                        data["SubTotalPrice"] = sumtotal.ToString();
                        data["Vat"] = sumvat.ToString();
                        data["TransportPrice"] = hidtab2transportprice.Value;
                        data["Customerpay"] = hidtab2customerpay.Value;
                        if (radordernow.Checked == true)
                        {
                            data["SALEORDERTYPE"] = StaticField.SaleOrderType_01; 
                        }
                        else
                        {
                            data["SALEORDERTYPE"] = StaticField.SaleOrderType_02; 
                        }
                        data["ReturnCashAMount"] = hidtab2ReturnCashAMount.Value;
                        data["TotalPrice"] = (sumtotal + sumvat + Convert.ToDouble(hidtab2transportprice.Value)).ToString();
                        data["OrderStatusCode"] = StaticField.OrderStatus_01; 

                        if (radordernow.Checked == true)
                        {
                            data["OrderStateCode"] = StaticField.OrderState_01; 
                        }
                        else
                        {
                            data["OrderStateCode"] = StaticField.OrderState_10; 
                        }

                        data["UpdateBy"] = empInfo.EmpCode;
                        data["CreateBy"] = empInfo.EmpCode;
                        data["FlagDelete"] = "N";
                        data["CampaignCategoryCode"] = hidtab2CampaignCategory.Value;
                        data["BranchCode"] = hidSelectedBranchCode2.Value;
                        data["LandmarkLat"] = hidLandmarkLat2.Value;
                        data["LandmarkLng"] = hidLandmarkLng2.Value;
                        data["OrderNote"] = hidtab2OrderNote.Value;
                        data["ChannelCode"] = StaticField.ChannelCode_Tel; 
                        data["FlagApproved"] = "N";

                        if (radordernow.Checked == true)
                        {
                            DateTime currentTime = DateTime.Now;
                            DateTime x45MinsLater = currentTime.AddMinutes(45);
                            data["DeliveryDate"] = x45MinsLater.ToString();
                        }
                        else
                        {
                            String Date = txtdPreOrder.Value;
                            String Time = txttimePreOrder.Value;
                            data["DeliveryDate"] = Date + " " + Time + ":00";
                        }

                        var response = wb.UploadValues(APIpath, "POST", data);

                        strordercode = Encoding.UTF8.GetString(response);
                        strordercode = strordercode.Replace("\"", "");
                    }

                    //Insert Orderdetailinfo


                    foreach (var lodt in L_orderdata2)
                    {
                        string respstringorderdetail = "";
                        APIpath = APIUrl + "/api/support/InsertMKOrderdetaildata";

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
                            data["Unit"] = lodt.Unit;
                            data["Amount"] = lodt.Amount.ToString();
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";
                            data["OrderCode"] = strordercode;
                            data["ParentProductCode"] = lodt.ParentProductCode;
                            data["FlagCombo"] = lodt.FlagCombo;
                            data["ComboCode"] = lodt.ComboCode;
                            data["ComboName"] = lodt.ComboName;
                            data["PromotionCode"] = lodt.PromotionCode;
                            data["CampaignCode"] = lodt.CampaignCode;
                            data["CampaignCategory"] = lodt.CampaignCategory;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstringorderdetail = Encoding.UTF8.GetString(response);
                        }
                    }


                    paymentdataInfo pdata = new paymentdataInfo();
                    List<paymentdataInfo> lpdata = new List<paymentdataInfo>();

                    pdata.OrderCode = strordercode;
                    pdata.PaymentTypeCode = StaticField.PaymentType01; 
                    pdata.Payamount = Convert.ToDouble(txtTotalPrice.Text);
                    pdata.CreateBy = hidEmpCode.Value;
                    pdata.UpdateBy = hidEmpCode.Value;
                    pdata.FlagDelete = "N";

                    lpdata.Add(pdata);

                    foreach (var paymentV in lpdata)
                    {
                        string respstring = "";
                        APIpath = APIUrl + "/api/support/L_paymentMKInsert";
                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["OrderCode"] = paymentV.OrderCode;
                            data["PaymentTypeCode"] = paymentV.PaymentTypeCode;
                            data["Payamount"] = paymentV.Payamount.ToString();
                            data["CreateBy"] = paymentV.CreateBy;
                            data["UpdateBy"] = paymentV.UpdateBy;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstring = Encoding.UTF8.GetString(response);
                        }
                    }

                    // insert transportdata
                    foreach (var tdataV in L_transportdata2)
                    {
                        string respstringorderdetail = "";
                        APIpath = APIUrl + "/api/support/InsertMKTransportdata";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["OrderCode"] = strordercode;
                            data["Address"] = tdataV.Address;
                            data["SubDistrictCode"] = tdataV.SubDistrictCode;
                            data["DistrictCode"] = tdataV.DistrictCode;
                            data["ProvinceCode"] = tdataV.ProvinceCode;
                            data["Zipcode"] = tdataV.Zipcode;
                            data["TransportPrice"] = hidtab2transportprice.Value;
                            data["AddressType"] = tdataV.AddressType;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["BranchCode"] = hidSelectedBranchCode2.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstringorderdetail = Encoding.UTF8.GetString(response);
                        }
                    }
                    CallOneAppAPI2(strordercode);

                    hidtab2orderstatus.Value = "สร้างใบสั่งขาย";
                    hidtab2ordercode.Value = strordercode;
                    lblorderstatus.Text = hidtab2orderstatus.Value;
                    lblordercode.Text = hidtab2ordercode.Value;

                    
                    btntab2_Click(null, null);
                    LoadOrderNote(CustomerCode);

                    branchcode = hidSelectedBranchCode2.Value;

                }
                else if (hidtab.Value == "3")
                {
                    hidtab3transportprice.Value = txtTransportPrice.Text;
                    hidtab3OrderNote.Value = txtOrderNoteLast.InnerText;
                    foreach (var lodt in L_orderdata3)
                    {
                        sumvat += (((((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount) * 7) / 100);
                        sumtotal += (((lodt.Price - ((lodt.Price * lodt.DiscountPercent) / 100)) - lodt.DiscountAmount) * lodt.Amount);
                    }

                    //Insert orderInfo
                    APIpath = APIUrl + "/api/support/InsertMKOrderdata";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["CustomerCode"] = CustomerCode;
                        data["CustomerPhone"] = CustomerPhone;
                        data["SubTotalPrice"] = sumtotal.ToString();
                        data["Vat"] = sumvat.ToString();
                        data["TransportPrice"] = hidtab3transportprice.Value;
                        data["Customerpay"] = hidtab3customerpay.Value;
                        if (radordernow.Checked == true)
                        {
                            data["SALEORDERTYPE"] = StaticField.SaleOrderType_01; 
                        }
                        else
                        {
                            data["SALEORDERTYPE"] = StaticField.SaleOrderType_02; 
                        }
                        data["ReturnCashAMount"] = hidtab3ReturnCashAMount.Value;
                        data["TotalPrice"] = (sumtotal + sumvat + Convert.ToDouble(hidtab3transportprice.Value)).ToString();
                        data["OrderStatusCode"] = StaticField.OrderStatus_01; 

                        if (radordernow.Checked == true)
                        {
                            data["OrderStateCode"] = StaticField.OrderState_01; 
                        }
                        else
                        {
                            data["OrderStateCode"] = StaticField.OrderState_10; 
                        }

                        data["UpdateBy"] = empInfo.EmpCode;
                        data["CreateBy"] = empInfo.EmpCode;
                        data["FlagDelete"] = "N";
                        data["CampaignCategoryCode"] = hidtab3CampaignCategory.Value;
                        data["BranchCode"] = hidSelectedBranchCode3.Value;
                        data["LandmarkLat"] = hidLandmarkLat3.Value;
                        data["LandmarkLng"] = hidLandmarkLng3.Value;
                        data["OrderNote"] = hidtab3OrderNote.Value;
                        data["ChannelCode"] = StaticField.ChannelCode_Tel; 
                        data["FlagApproved"] = "N";

                        if (radordernow.Checked == true)
                        {
                            DateTime currentTime = DateTime.Now;
                            DateTime x45MinsLater = currentTime.AddMinutes(45);
                            data["DeliveryDate"] = x45MinsLater.ToString();
                        }
                        else
                        {
                            String Date = txtdPreOrder.Value;
                            String Time = txttimePreOrder.Value;
                            data["DeliveryDate"] = Date + " " + Time + ":00";
                        }

                        var response = wb.UploadValues(APIpath, "POST", data);

                        strordercode = Encoding.UTF8.GetString(response);
                        strordercode = strordercode.Replace("\"", "");
                    }

                    //Insert Orderdetailinfo


                    foreach (var lodt in L_orderdata3)
                    {
                        string respstringorderdetail = "";
                        APIpath = APIUrl + "/api/support/InsertMKOrderdetaildata";

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
                            data["Unit"] = lodt.Unit;
                            data["Amount"] = lodt.Amount.ToString();
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";
                            data["OrderCode"] = strordercode;
                            data["ParentProductCode"] = lodt.ParentProductCode;
                            data["FlagCombo"] = lodt.FlagCombo;
                            data["ComboCode"] = lodt.ComboCode;
                            data["ComboName"] = lodt.ComboName;
                            data["PromotionCode"] = lodt.PromotionCode;
                            data["CampaignCode"] = lodt.CampaignCode;
                            data["CampaignCategory"] = lodt.CampaignCategory;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstringorderdetail = Encoding.UTF8.GetString(response);
                        }
                    }


                    paymentdataInfo pdata = new paymentdataInfo();
                    List<paymentdataInfo> lpdata = new List<paymentdataInfo>();

                    pdata.OrderCode = strordercode;
                    pdata.PaymentTypeCode = StaticField.PaymentType01; 
                    pdata.Payamount = Convert.ToDouble(txtTotalPrice.Text);
                    pdata.CreateBy = hidEmpCode.Value;
                    pdata.UpdateBy = hidEmpCode.Value;
                    pdata.FlagDelete = "N";

                    lpdata.Add(pdata);

                    foreach (var paymentV in lpdata)
                    {
                        string respstring = "";
                        APIpath = APIUrl + "/api/support/L_paymentMKInsert";
                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["OrderCode"] = paymentV.OrderCode;
                            data["PaymentTypeCode"] = paymentV.PaymentTypeCode;
                            data["Payamount"] = paymentV.Payamount.ToString();
                            data["CreateBy"] = paymentV.CreateBy;
                            data["UpdateBy"] = paymentV.UpdateBy;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstring = Encoding.UTF8.GetString(response);
                        }
                    }

                    // insert transportdata
                    foreach (var tdataV in L_transportdata3)
                    {
                        string respstringorderdetail = "";
                        APIpath = APIUrl + "/api/support/InsertMKTransportdata";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["OrderCode"] = strordercode;
                            data["Address"] = tdataV.Address;
                            data["SubDistrictCode"] = tdataV.SubDistrictCode;
                            data["DistrictCode"] = tdataV.DistrictCode;
                            data["ProvinceCode"] = tdataV.ProvinceCode;
                            data["Zipcode"] = tdataV.Zipcode;
                            data["TransportPrice"] = hidtab3transportprice.Value;
                            data["AddressType"] = tdataV.AddressType;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["BranchCode"] = hidSelectedBranchCode3.Value;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstringorderdetail = Encoding.UTF8.GetString(response);
                        }
                    }

                    

                    CallOneAppAPI3(strordercode);

                    hidtab3orderstatus.Value = "สร้างใบสั่งขาย";
                    hidtab3ordercode.Value = strordercode;
                    lblorderstatus.Text = hidtab3orderstatus.Value;
                    lblordercode.Text = hidtab3ordercode.Value;

                    
                    btntab3_Click(null, null);
                    LoadOrderNote(CustomerCode);

                    branchcode = hidSelectedBranchCode3.Value;

                }

                
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<myChatHub>();
                hubContext.Clients.All.broadcastMessage(hidEmpCode.Value, branchcode, StaticField.branchcode_01); 

                if (strordercode != "")
                {
                    
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "บันทึกข้อมูลเสร็จสิ้น เลขที่ใบสั่งขาย : " + strordercode + "');", true);
                }
                else
                {

                }
                
            }
        }

        protected void btneditcustomer_Click(object sender, EventArgs e)
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
                    
                    data["UpdateBy"] = hidEmpCode.Value;

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr01 = Encoding.UTF8.GetString(response);
                }
                sum1 = JsonConvert.DeserializeObject<int?>(respstr);
            }
            if (sum > 0 && sum1 > 0)
            {
                BindCustomerLabel(CustomerCode);
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-customer').modal('hide');", true);
            }
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

        protected void dtlPromotionDetail_ItemDataBound(object sender, DataListItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnShow = (Button)e.Item.FindControl("btnShow");
                HiddenField hidProductDesc = (HiddenField)e.Item.FindControl("hidProductDesc");
                HiddenField hidPromotionDetailId = (HiddenField)e.Item.FindControl("hidPromotionDetailId");

                if (hidflagcombo.Value != "Y")
                {
                    btnShow.Attributes.Add("onmouseover", string.Format("viewProductdesc('{0}')", hidProductDesc.Value));
                    btnShow.Attributes.Add("onmouseout", "this.style.textDecoration='none';");
                    
                }
                else
                {

                    List<SubPromotionDetailInfo> lsubmain = new List<SubPromotionDetailInfo>();

                    lsubmain = LoadSubMainPromotionDetailforBindProductDesc(hidPromotionDetailId.Value);


                    List<SubPromotionDetailInfo> lsubexchange = new List<SubPromotionDetailInfo>();

                    lsubexchange = LoadSubExchangePromotionDetailforBindProductDesc(hidPromotionDetailId.Value);

                    string strprd = "";

                    foreach (var item in lsubmain)
                    {
                        strprd += "-" + item.MainProductName + "<br>";
                    }
                    foreach (var item in lsubexchange)
                    {
                        strprd += "-" + item.ExchangeProductName + "<br>";
                    }

                    btnShow.Attributes.Add("onmouseover", string.Format("viewProductdesc('{0}')", strprd));
                    btnShow.Attributes.Add("onmouseout", "this.style.textDecoration='none';");

                }
            }


        }


        protected void dtlNearestBranch_ItemDataBound(object sender, DataListItemEventArgs e)
        {



            var item = e.Item.DataItem;


            RadioButton r = (RadioButton)e.Item.FindControl("radBranch");
            Button b = (Button)e.Item.FindControl("hidRadioBtn");
            if (r != null)
            {
                r.Attributes.Add("onclick", "resetname('" + r.ClientID + "','"+ b.ClientID + "');");
            }


            HiddenField stat = (HiddenField)e.Item.FindControl("hidOnlineStatus");
            var onlineCc = e.Item.FindControl("OnlineCircle");
            var offlineCc = e.Item.FindControl("OfflineCircle");
            var rider = e.Item.FindControl("RiderOk");
            var noRider = e.Item.FindControl("RiderNotOk");
            var gift = e.Item.FindControl("Gift");
            var noGift = e.Item.FindControl("NoGift");

            if (stat != null)
            {
                if (stat.Value == "Y")
                {
                    onlineCc.Visible = true;
                    offlineCc.Visible = false;

                    rider.Visible = true;
                    noRider.Visible = false;
                    gift.Visible = true;
                    noGift.Visible = false;
                }
                else
                {
                    onlineCc.Visible = false;
                    offlineCc.Visible = true;

                    rider.Visible = false;
                    noRider.Visible = true;
                    gift.Visible = false;
                    noGift.Visible = true;
                }
            }
        }

        protected void radClick_SelectBranch(object sender, EventArgs e)
        {
            HtmlInputRadioButton rad = (HtmlInputRadioButton)sender;
            var brCode = rad.Value;

            if (hidtab.Value == "1")
            {
                hidSelectedBranchCode1.Value = brCode;
                if (L_orderdata1.Count > 0)
                {
                    L_orderdata1[0].BranchCode = hidSelectedBranchCode1.Value;
                }

            }


            if (hidtab.Value == "2")
            {
                hidSelectedBranchCode2.Value = brCode;

                if (L_transportdata2.Count > 0)
                {
                    L_orderdata2[0].BranchCode = hidSelectedBranchCode2.Value;
                }

            }


            if (hidtab.Value == "3")
            {
                hidSelectedBranchCode3.Value = brCode;

                if (L_transportdata3.Count > 0)
                {
                    L_orderdata3[0].BranchCode = hidSelectedBranchCode3.Value;
                }

            }

        }

        protected void txtOrderNoteCopy_Click(object sender, EventArgs e)
        {
            LoadOrderNote(CustomerCode);
            txtOrderNoteLast.InnerText =  txtOrderNoteLast.InnerText + txtOrderNote.InnerText;

            ScriptManager.RegisterStartupScript(this, Page.GetType(), "function", "displayNonSelected()", true);
        }

        protected void ordernow_Changed(object sender, EventArgs e)
        {
            ClickRadioBranch();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "displayPayment();", true);           
        }

        protected void radpreorder_Changed(object sender, EventArgs e)
        {
            ClickRadioBranch();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "displayPayment();", true);
        }

        protected void BtnSearchRecipe_Click(object sender, EventArgs e)
        {

            if(hidflagshowproductpromotionrecipe.Value == StaticField.FlagShowProductPromotion_PRODUCT) 
            {
                PromotionDetailInfo pdInfo = new PromotionDetailInfo();
                List<PromotionDetailInfo> lpdInfo = new List<PromotionDetailInfo>();
                pdInfo.CampaignCode = hidcampaigncodetorecipe.Value;

                lpdInfo = GetProductRecipefromCampaignProduct(pdInfo.CampaignCode);
                
            }
            else // Search Product in Combo Set
            {
                string respstr = "";
                APIpath = APIUrl + "/api/support/ListProductMainByRecipeNameCriteria";

                ProductInfo pdcustomInfo = new ProductInfo();
                List<ProductInfo> lpdcustomInfo = new List<ProductInfo>();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["CampaignCode"] = hidcampaigntorecipe.Value;
                    data["PromotionCode"] = hidpromotiontorecipe.Value;
                    data["RecipeName"] = txtSearchRecipe.Text.Trim();
                    data["rowOFFSet"] = "0";
                    data["rowFetch"] = StaticField.rowFetch_100000; 

                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                List<ProductInfo> lProductMainInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

                string respstr1 = "";
                APIpath = APIUrl + "/api/support/ListProductExchangeByRecipeNameCriteria";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["CampaignCode"] = hidcampaigntorecipe.Value;
                    data["PromotionCode"] = hidpromotiontorecipe.Value;
                    data["RecipeName"] = txtSearchRecipe.Text.Trim();
                    data["rowOFFSet"] = "0";
                    data["rowFetch"] = StaticField.rowFetch_100000; 

                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr1 = Encoding.UTF8.GetString(response);
                }
                List<ProductInfo> lProductExchangeInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr1);

                lpdcustomInfo = lProductMainInfo.Union(lProductExchangeInfo).ToList();

                
            }
        }

        protected void fillText(object sender, EventArgs e)
        {
            String a = "";
        }

        #endregion

        #region Binding

        protected string GetPrice(object objPrice, object objDiscountAmount, object objDiscountPercent)
        {
            decimal? strPrice = (objPrice != null) ? Convert.ToDecimal(objPrice) : 0;
            decimal? strDiscountAmount = (objDiscountAmount != null) ? Convert.ToDecimal(objDiscountAmount) : 0;
            decimal? strDiscountPercent = (objDiscountPercent != null) ? Convert.ToDecimal(objDiscountPercent) : 0;

            decimal? sumprice = strPrice - strDiscountAmount - ((strPrice * strDiscountPercent) / 100);
            
            return sumprice.ToString();
        }

        protected string GetTextPrice(object objParentProductCode, object objPrice, object objDiscountAmount, object objDiscountPercent, object objcolorcode)
        {
            string strParentProductCode = (objParentProductCode != null) ? objParentProductCode.ToString() : "";
            string strcolor = (objcolorcode != null) ? objcolorcode.ToString() : "";
            decimal? strPrice = (objPrice != null) ? Convert.ToDecimal(objPrice) : 0;
            decimal? strDiscountAmount = (objDiscountAmount != null) ? Convert.ToDecimal(objDiscountAmount) : 0;
            decimal? strDiscountPercent = (objDiscountPercent != null) ? Convert.ToDecimal(objDiscountPercent) : 0;

            decimal? sumprice = strPrice - strDiscountAmount - ((strPrice * strDiscountPercent) / 100);

            string strret = "";

            strcolor = (strcolor != "") ? strcolor : "#333333";

            if (strParentProductCode == "")
            {
                if (strPrice > sumprice)
                {
                    strret = "  <span style = \"text-decoration: line-through;color:" + strcolor + "\" >" + string.Format("{0:n}", strPrice) + "</span>  " +
                             "  &nbsp; <span style = \"color:" + strcolor + "\" >" + string.Format("{0:n}", sumprice) + " X </span> ";
                }
                else
                {
                    strret = "<font color=\"" + strcolor + "\">" + string.Format("{0:n}", strPrice) + " X </font>";

                }
            }

            return strret;
        }
        protected string GetPricedtlPromotiondetail(object objPrice, object objDiscountAmount, object objDiscountPercent)
        {
            decimal? strPrice = (objPrice != null) ? Convert.ToDecimal(objPrice) : 0;
            decimal? strDiscountAmount = (objDiscountAmount != null) ? Convert.ToDecimal(objDiscountAmount) : 0;
            decimal? strDiscountPercent = (objDiscountPercent != null) ? Convert.ToDecimal(objDiscountPercent) : 0;

            decimal? sumprice = strPrice - strDiscountAmount - ((strPrice * strDiscountPercent) / 100);

            string strret = "";


            if (strPrice > sumprice)
            {
                strret = @"  <span style = ""text-decoration: line-through;"" >" + string.Format("{0:#.00}", strPrice) + "</span>  " +
                         "  &nbsp;" + string.Format("{0:n}", sumprice) + " ";
            }
            else
            {
                strret = string.Format("{0:n}", strPrice) + "";

            }


            return strret;
        }

        protected string GetProductName(object objProductName,object objPromotionDetailName)
        {
            string strProductName = (objProductName != null) ? objProductName.ToString() : "";
            string strPromotionDetailName = (objPromotionDetailName != null) ? objPromotionDetailName.ToString() : "";

            return (strPromotionDetailName != "") ? strPromotionDetailName : strProductName;
        }
        protected string GetPromotionName(object objPromotionCode, object objPromotionName)
        {
            string strPromotionCode = (objPromotionCode != null) ? objPromotionCode.ToString() : "";
            string strPromotionName = (objPromotionName != null) ? objPromotionName.ToString() : "";

            return strPromotionName;
        }
        protected string GetImage(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";

            return APIUrl + strCode;
        }
        protected void BindProductByPromotion(ProductInfo cinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductByCriteria";

            

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCode"] = cinfo.CampaignCode;
                    data["PromotionCode"] = cinfo.PromotionCode;
                    data["rowOFFSet"] = "0";
                    data["rowFetch"] = StaticField.rowFetch_100000; 


                var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            

        }

        protected void BindPromotionByCampaign(PromotionInfo cinfo, String flagshowproductpromotion)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignPromotionNopagingByCriteria";

            
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCode"] = cinfo.CampaignCode;
                    data["Active"] = StaticField.ActiveFlag_Y; 

                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            
            if (flagshowproductpromotion == StaticField.FlagShowProductPromotion_PROMOTION) 
            {
                dtlPromotion.DataSource = lPromotionInfo;

                dtlPromotion.DataBind();
            }
            else
            {
                
            }
            

        }
        protected void BindPromotionTypeByCampaign(PromotionInfo cinfo, String flagshowproductpromotion)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionTypeByCampaignNopagingByCriteria";

            
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = cinfo.CampaignCode;
                data["Active"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            
            if (flagshowproductpromotion == "PROMOTION")
            {
                dtlPromotionType.DataSource = lPromotionInfo;

                dtlPromotionType.DataBind();
            }
            else
            {
                
            }
            

        }
        protected void BindPromotionDetailByCampaign(PromotionDetailInfo cinfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionDetailByCampaign";

            

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCode"] = cinfo.CampaignCode;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                List<PromotionDetailInfo> lPromotionDetailInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);

            Cache["dtlPromotionDetail"] = lPromotionDetailInfo;

            gvPromotionDetail.DataSource = lPromotionDetailInfo;

            gvPromotionDetail.DataBind();
            

        }
        protected void BindCampaign(CampaignInfo cinfo)
        {
            string respstr = "";

            

            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCategory"] = cinfo.CampaignCategory;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                List<CampaignInfo> lCampaignInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

                

                dtlCampaign.DataSource = lCampaignInfo;

                dtlCampaign.DataBind();
            

        }
        protected void BindCampaignCategory()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingByCriteria";

            
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCategoryCode"] = "";


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                List<CampaignCategoryInfo> lCampaignCategoryInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

                

                dtlCampaignCategory.DataSource = lCampaignCategoryInfo;

                dtlCampaignCategory.DataBind();
            

        }
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


        
        }

        

        private void BindAddress(List<BranchInfo> branches)
        {
          
            dtlNearestBranch.DataSource = branches;
           
            dtlNearestBranch.DataBind();
        }


        #endregion

        

        #region GoogleMaps
        public class LatLngg
        {
            public Double longitude { get; set; }
            public Double latitude { get; set; }
        }

        public class LatLng
        {
            public double lat { get; set; }
            public double lng { get; set; }
            public string AreaCode { get; set; }
        }

        public class MAPS
        {
            public string LocationName;
            public string Latitude;
            public string Longitude;
        }

        public static Boolean isInside(List<LatLngg> area, LatLngg location)
        {

            var x = 0;
            var y = area.Count - 1;
            var result = false;

            while (x < area.Count)
            {
                if (area[x].longitude > location.longitude != area[y].longitude > location.longitude &&
                        location.latitude < (area[y].latitude - area[x].latitude) * (location.longitude - area[x].longitude) / (area[y].longitude - area[x].longitude) + area[x].latitude)
                {
                    result = !result;
                }
                y = x++;
            }
            return result;
        }

        [WebMethod]
        public static MAPS[] BindMapMarker(string AreaCode)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<BranchInfo> lstBranch = LoadBranchList(AreaCode);
            List<MAPS> lstMarkers = new List<MAPS>();
            try
            {
                foreach (BranchInfo bInfo in lstBranch)
                {
                    MAPS objMAPS = new MAPS();
                    objMAPS.LocationName = bInfo.BranchName;
                    objMAPS.Latitude = bInfo.Lat;
                    objMAPS.Longitude = bInfo.Long;
                    lstMarkers.Add(objMAPS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return lstMarkers.ToArray();
        }



        [WebMethod]
        public static LatLng[] BindPolygon(string currentLat, string currentLng)
        {
            LatLngg currentLatlng = new LatLngg();
            currentLatlng.latitude = Convert.ToDouble(currentLat);
            currentLatlng.longitude = Convert.ToDouble(currentLng);

            var ls = new List<LatLng>();

            List<AreaInfo> LArea = LoadArea();

            foreach (AreaInfo aInfo in LArea)
            {
                string areacode = aInfo.AreaCode;
                string polygon = aInfo.Polygon;
                var Polygonstr = polygon.Split(' ').ToList();

                List<LatLngg> PolygonCordinate = new List<LatLngg>();

                foreach (string latlng in Polygonstr)
                {
                    var lt = latlng.Split(',').ToList();
                    double lat = Convert.ToDouble(lt[0]);
                    double lng = Convert.ToDouble(lt[1]);

                    PolygonCordinate.Add(new LatLngg { latitude = lat, longitude = lng });

                }

                if (isInside(PolygonCordinate, currentLatlng))
                {
                    ls.Add(new LatLng { AreaCode = areacode });
                    foreach (LatLngg d in PolygonCordinate)
                    {
                        ls.Add(new LatLng { lat = d.latitude, lng = d.longitude });
                    }
                }
            }

            return ls.ToArray();
        }

        #endregion

        public void CallOneAppAPI1(String strordercode)
        {

            Task.Run(() =>
            {
               
                    
            FinishOrderReturn finishorderReturn = new FinishOrderReturn();
            List<order_detail> orderdetail = new List<order_detail>();

            order_detail ordetail = new order_detail();
            ordetail.order_code = strordercode;
            ordetail.brand_name = hidtab1CampaignCategoryname.Value;
            ordetail.total_amount = Convert.ToDouble(txtTotalPrice.Text);
            
            ordetail.branch_name = hidSelectedBranchName1.Value;
            ordetail.order_status = "Order Completed";

            orderdetail.Add(ordetail);

            finishorderReturn.unique_id = (Request.QueryString["UniqueId"] != null) ? Request.QueryString["UniqueId"].ToString() : "";
            finishorderReturn.customer_fname = txtCustomerFName_Edit.Text;
            finishorderReturn.customer_lname = txtCustomerLName_Edit.Text;

            foreach (var orderdetailV in orderdetail)
            {
                finishorderReturn.order_detail.Add(new order_detail() { order_code = orderdetailV.order_code, brand_name = orderdetailV.brand_name, total_amount = orderdetailV.total_amount, branch_name = orderdetailV.branch_name, order_status = orderdetailV.order_status });
            }

            finishorderReturn.channel = StaticField.MKChannelCode_02; 

            String wordingresult = "";
            List<ResultOneApp> lresult = new List<ResultOneApp>();

            
            APIpath = StaticField.APIpath_TRD_FinishOrder; //http://doublep.three-rd.com:3230/api/1.0.0/oms/order?access_token=X2LCUDLQoWlqpZoNDhijsUvp9ytvVUAl1YIUSCjqm2BSTUVbGitzHBIJGBvdS8DS
                using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                var jsonObj = JsonConvert.SerializeObject(new { finishorderReturn.order_detail, finishorderReturn.channel, finishorderReturn.customer_fname, finishorderReturn.customer_lname, finishorderReturn.unique_id, });
                var dataString = client.UploadString(APIpath, jsonObj);
                var data = JsonConvert.DeserializeObject<List<ResultOneApp>>(dataString);
                lresult = data;
            }
                hidtab1CampaignCategoryname.Value = "";

                foreach (var od in lresult)
            {
                String a = od.resultCode;
                List<resultData> b = od.resultData;
                String c = od.resultMessage;
            }
                

                Thread.Sleep(200);
                
            });
        }
        public void CallOneAppAPI2(String strordercode)
        {
            Task.Run(() =>
            {


                FinishOrderReturn finishorderReturn = new FinishOrderReturn();
                List<order_detail> orderdetail = new List<order_detail>();

                order_detail ordetail = new order_detail();
                ordetail.order_code = strordercode;
                ordetail.brand_name = hidtab2CampaignCategoryname.Value;
                ordetail.total_amount = Convert.ToDouble(txtTotalPrice.Text);
                
                ordetail.branch_name = hidSelectedBranchName2.Value;
                ordetail.order_status = "Order Completed";

                orderdetail.Add(ordetail);

                finishorderReturn.unique_id = (Request.QueryString["UniqueId"] != null) ? Request.QueryString["UniqueId"].ToString() : "";
                finishorderReturn.customer_fname = txtCustomerFName_Edit.Text;
                finishorderReturn.customer_lname = txtCustomerLName_Edit.Text;

                foreach (var orderdetailV in orderdetail)
                {
                    finishorderReturn.order_detail.Add(new order_detail() { order_code = orderdetailV.order_code, brand_name = orderdetailV.brand_name, total_amount = orderdetailV.total_amount, branch_name = orderdetailV.branch_name, order_status = orderdetailV.order_status });
                }

                finishorderReturn.channel = StaticField.MKChannelCode_02; 

                String wordingresult = "";
                List<ResultOneApp> lresult = new List<ResultOneApp>();

                //APIpath = APIUrl + "/api/support/FinishOrderAPI"; switch to this url for check result from internal server and switch to below for send api to 3rd party
                APIpath = StaticField.APIpath_TRD_FinishOrder; //http://doublep.three-rd.com:3230/api/1.0.0/oms/order?access_token=X2LCUDLQoWlqpZoNDhijsUvp9ytvVUAl1YIUSCjqm2BSTUVbGitzHBIJGBvdS8DS
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

                    var jsonObj = JsonConvert.SerializeObject(new { finishorderReturn.order_detail, finishorderReturn.channel, finishorderReturn.customer_fname, finishorderReturn.customer_lname, finishorderReturn.unique_id, });
                    var dataString = client.UploadString(APIpath, jsonObj);
                    var data = JsonConvert.DeserializeObject<List<ResultOneApp>>(dataString);
                    lresult = data;
                }
                hidtab2CampaignCategoryname.Value = "";

                foreach (var od in lresult)
                {
                    String a = od.resultCode;
                    List<resultData> b = od.resultData;
                    String c = od.resultMessage;
                }
                

                Thread.Sleep(1000);

            });
        }
        public void CallOneAppAPI3(String strordercode)
        {
            Task.Run(() =>
            {
                FinishOrderReturn finishorderReturn = new FinishOrderReturn();
                List<order_detail> orderdetail = new List<order_detail>();

                order_detail ordetail = new order_detail();
                ordetail.order_code = strordercode;
                ordetail.brand_name = hidtab3CampaignCategoryname.Value;
                ordetail.total_amount = Convert.ToDouble(txtTotalPrice.Text);
                
                ordetail.branch_name = hidSelectedBranchName3.Value;
                ordetail.order_status = "Order Completed";

                orderdetail.Add(ordetail);

                finishorderReturn.unique_id = (Request.QueryString["UniqueId"] != null) ? Request.QueryString["UniqueId"].ToString() : "";
                finishorderReturn.customer_fname = txtCustomerFName_Edit.Text;
                finishorderReturn.customer_lname = txtCustomerLName_Edit.Text;

                foreach (var orderdetailV in orderdetail)
                {
                    finishorderReturn.order_detail.Add(new order_detail() { order_code = orderdetailV.order_code, brand_name = orderdetailV.brand_name, total_amount = orderdetailV.total_amount, branch_name = orderdetailV.branch_name, order_status = orderdetailV.order_status });
                }

                finishorderReturn.channel = StaticField.MKChannelCode_02; 

                String wordingresult = "";
                List<ResultOneApp> lresult = new List<ResultOneApp>();

                //APIpath = APIUrl + "/api/support/FinishOrderAPI"; switch to this url for check result from internal server and switch to below for send api to 3rd party
                APIpath = StaticField.APIpath_TRD_FinishOrder; //http://doublep.three-rd.com:3230/api/1.0.0/oms/order?access_token=X2LCUDLQoWlqpZoNDhijsUvp9ytvVUAl1YIUSCjqm2BSTUVbGitzHBIJGBvdS8DS
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

                    var jsonObj = JsonConvert.SerializeObject(new { finishorderReturn.order_detail, finishorderReturn.channel, finishorderReturn.customer_fname, finishorderReturn.customer_lname, finishorderReturn.unique_id, });
                    var dataString = client.UploadString(APIpath, jsonObj);
                    var data = JsonConvert.DeserializeObject<List<ResultOneApp>>(dataString);
                    lresult = data;
                }
                hidtab3CampaignCategoryname.Value = "";

                foreach (var od in lresult)
                {
                    String a = od.resultCode;
                    List<resultData> b = od.resultData;
                    String c = od.resultMessage;
                }
                

                Thread.Sleep(200);

            });
        }
        public List<EmpInfo> GetLogin()
        {
            RefCode = (Request.QueryString["RefCode"] != null) ? Request.QueryString["RefCode"].ToString() : "";

            string respstr = "";

            APIpath = APIUrl + "/api/support/GetLogin";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["EmpCode"] = RefCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> emp_list = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            

            return emp_list;
        }

        public void CreateSessionEmp()
        {
            string respstr = "";
            
            List<EmpInfo> EmpObject = GetLogin();

            EmpInfo emp = new EmpInfo();


            if (EmpObject.Count > 0)
            {
                emp.Username = EmpObject[0].Username;

                emp.Password = EmpObject[0].Password;

                emp.ActiveFlag = StaticField.ActiveFlag_Y; 

                emp.EmpCode = EmpObject[0].EmpCode;

                emp.EmpName_TH = EmpObject[0].EmpName_TH;

                emp.PositionCode = EmpObject[0].PositionCode;

                emp.EmpFname_TH = EmpObject[0].EmpFname_TH;

                emp.EmpLname_TH = EmpObject[0].EmpLname_TH;

                Session["EmpInfo"] = emp;
                
            }

        }

        public List<EmpInfo> GetEmpCodefromRefCode(String refCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmp";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["RefCode"] = refCode;
                data["ActiveFlag"] = StaticField.ActiveFlag_Y; 

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpInfo> emp_list = JsonConvert.DeserializeObject<List<EmpInfo>>(respstr);

            return emp_list;
        }

        protected string GetSubString(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            

            if (strCode != "" && strCode.Length > 20)
            {
                strCode =  strCode.Substring(0,20) + "...";
            }
           


            return strCode;
        }

        protected void btnCloseTab_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            if (hidtab.Value == "1")
            {
                hidtab1CampaignCategory.Value = "";
                hidtab1CampaignCategoryname.Value = "";
                lblCampaignCategory.Text = "";

                Session.Remove("L_orderdata1");
                LoadgvOrder(L_orderdata1);
                Loadtotal(L_orderdata1);
            }
            if (hidtab.Value == "2")
            {
                hidtab2CampaignCategory.Value = "";
                hidtab2CampaignCategoryname.Value = "";
                lblCampaignCategory.Text = "";

                Session.Remove("L_orderdata2");
                LoadgvOrder(L_orderdata2);
                Loadtotal(L_orderdata2);
            }
            if (hidtab.Value == "3")
            {
                hidtab3CampaignCategory.Value = "";
                hidtab3CampaignCategoryname.Value = "";
                lblCampaignCategory.Text = "";

                Session.Remove("L_orderdata3");
                LoadgvOrder(L_orderdata3);
                Loadtotal(L_orderdata3);
            }
        }
    }
}

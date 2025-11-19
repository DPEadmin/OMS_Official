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
using System.Globalization;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Spire.Pdf.Graphics;
using Org.BouncyCastle.Security.Certificates;
using OfficeOpenXml.FormulaParsing.Utilities;
using System.Web.UI.WebControls;
using DOMS_TSR.src.UserControl;

namespace DOMS_TSR.src.InventoryManagement
{
    public partial class InventoryDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        protected static string ProductImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];
        protected static int currentPageNumber;
        protected static int currentPageNumbergvProduct;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
         float strlat;
         float strLong;
        string PolyLine;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPageNumbergvProduct = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null && merchantInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                BindddlProductCategry();
                //BindddlProductOrder();
                //BindddlSupplier();
                //BindddlSearchProductBrand();
                LoadInventory();
                LoadInventoryDetail();
                LoadRoutingMapInventory();
                LoadProduct();
                btnChoosePO.Visible = false;
                Drawpolygon.SetLatLong(PopmapLbLat.Text, PopmapLbLong.Text, hidInvenIDPopupMap.Value,PolyLine);



            }
        }





        #region Function
        protected void LoadInventory()
        {
            List<InventoryInfo> lInventoryInfo = new List<InventoryInfo>();

            lInventoryInfo = LoadInventoryNoPaging();

            lblInventoryCode.Text = lInventoryInfo[0].InventoryCode == "" ? lblInventoryCode.Text = "-" : lblInventoryCode.Text = lInventoryInfo[0].InventoryCode;

            lblInventoryName.Text = lInventoryInfo[0].InventoryName == "" ? lblInventoryName.Text = "-" : lblInventoryName.Text = lInventoryInfo[0].InventoryName;

            lblAddress.Text = lInventoryInfo[0].Address == "" ? lblAddress.Text = "-" : lblAddress.Text = lInventoryInfo[0].Address;

            lblProvince.Text = lInventoryInfo[0].Province == "" ? lblProvince.Text = "-" : lblProvince.Text = BindProvince(lInventoryInfo[0].Province);

            lblDistrict.Text = lInventoryInfo[0].District == "" ? lblDistrict.Text = "-" : lblDistrict.Text = BindDistrict(lInventoryInfo[0].District);

            lblSubDistrict.Text = lInventoryInfo[0].SubDistrict == "" ? lblSubDistrict.Text = "-" : lblSubDistrict.Text = BindSubDistrict(lInventoryInfo[0].SubDistrict);

            lblPostCode.Text = lInventoryInfo[0].PostCode == "" ? lblPostCode.Text = "-" : lblPostCode.Text = lInventoryInfo[0].PostCode;

            lblContactTel.Text = lInventoryInfo[0].ContactTel == "" ? lblContactTel.Text = "-" : lblContactTel.Text = lInventoryInfo[0].ContactTel;

            lblFax.Text = lInventoryInfo[0].Fax == "" ? lblFax.Text = "-" : lblFax.Text = lInventoryInfo[0].Fax;

            #region map
            string strlat;
            string strLong;
            strlat = lInventoryInfo[0].Lat == "" ? strlat = "-" : lInventoryInfo[0].Lat;
            strLong = lInventoryInfo[0].Long == "" ? strLong = "-" : lInventoryInfo[0].Long;
            LbLatLong.Text = strlat+","+strLong;
            PopmapLbLat.Text = strlat;
            PopmapLbLong.Text = strLong;
            hidInvenIDPopupMap.Value = lInventoryInfo[0].InventoryCode;
            PolyLine = lInventoryInfo[0].Polygon;

            txtareMap.Text = lInventoryInfo[0].Polygon == null?"": lInventoryInfo[0].Polygon;
            LbNameInventory.Text  = lInventoryInfo[0].InventoryName == "" ? lblInventoryName.Text = "-" : lblInventoryName.Text = lInventoryInfo[0].InventoryName;
            string script = $@"
    var strlat = '{PopmapLbLat.Text}';
    var strLong = '{PopmapLbLong.Text}';
    var idinven = '{hidInvenIDPopupMap.Value}';
    var polyline = `{PolyLine}`;"; // ใช้เครื่องหมายแบ็กทิก (backtick) สำหรับสตริงหลายบรรทัด

            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetLatLng", script, true);
            // ในฟังก์ชัน LoadInventory หรือที่คุณต้องการใช้ strlat และ strLong
            // ส่งค่า strlat และ strLong ไปยัง JavaScript โดยอัพเดตค่าของ asp:Label แบบอัตโนมัติ
            ScriptManager.RegisterStartupScript(this, GetType(), "UpdateLabels", $"UpdateLabels('{strlat}', '{strLong}');", true);

            #endregion
        }

        public List<InventoryInfo> LoadInventoryNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = Request.QueryString["InventoryCode"];

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            return lInventoryInfo;
        }

        protected void LoadInventoryDetail()
        {
            List<InventoryDetailInfo> lInventoryDetailInfo = new List<InventoryDetailInfo>();
            
            int? totalRow = CountInventoryDetailList();
            SetPageBar(Convert.ToDouble(totalRow));
            lInventoryDetailInfo = GetInventoryDetailListPaging();

            

            gvInventoryDetail.DataSource = lInventoryDetailInfo;
            gvInventoryDetail.DataBind();
            
            gvInventoryDetail.Columns[12].Visible = false;
        }

        public int? CountInventoryDetailList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListInventoryDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = Request.QueryString["InventoryCode"];
                data["ProductCode"] = txtSearchProductCode.Text;
                data["ProductName"] = txtSearchProductName.Text;
                data["MerchantCode"] = hidMerCode.Value;
                data["ProductCategoryCode"] = ddlSearchProductCategory.SelectedValue;
                data["ProductBrandCode"] = ddlSearchProductBrand.SelectedValue;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected void SetPageBar(double totalRow)
        {

            lblTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages.Text) + 1; i++)
            {
                ddlPage.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString()));
            }
            setDDl(ddlPage, currentPageNumber.ToString());
            

            
            if ((currentPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber == 1))
            {
                lnkbtnFirst.Enabled = false;
                lnkbtnPre.Enabled = false;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else if ((currentPageNumber.ToString() == lblTotalPages.Text) && (currentPageNumber > 1))
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = false;
                lnkbtnLast.Enabled = false;
            }
            else
            {
                lnkbtnFirst.Enabled = true;
                lnkbtnPre.Enabled = true;
                lnkbtnNext.Enabled = true;
                lnkbtnLast.Enabled = true;
            }
            
        }

        protected void SetPageBargvProduct(double totalRow)
        {

            lblgvProductTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlgvProductPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblgvProductTotalPages.Text) + 1; i++)
            {
                ddlgvProductPage.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString()));
            }
            setDDl(ddlgvProductPage, currentPageNumbergvProduct.ToString());
            

            
            if ((currentPageNumbergvProduct == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtngvProductFirst.Enabled = false;
                lnkbtngvProductPre.Enabled = false;
                lnkbtngvProductNext.Enabled = true;
                lnkbtngvProductLast.Enabled = true;
            }
            else if ((currentPageNumbergvProduct.ToString() == lblgvProductTotalPages.Text) && (currentPageNumbergvProduct == 1))
            {
                lnkbtngvProductFirst.Enabled = false;
                lnkbtngvProductPre.Enabled = false;
                lnkbtngvProductNext.Enabled = false;
                lnkbtngvProductLast.Enabled = false;
            }
            else if ((currentPageNumbergvProduct.ToString() == lblgvProductTotalPages.Text) && (currentPageNumbergvProduct > 1))
            {
                lnkbtngvProductFirst.Enabled = true;
                lnkbtngvProductPre.Enabled = true;
                lnkbtngvProductNext.Enabled = false;
                lnkbtngvProductLast.Enabled = false;
            }
            else
            {
                lnkbtngvProductFirst.Enabled = true;
                lnkbtngvProductPre.Enabled = true;
                lnkbtngvProductNext.Enabled = true;
                lnkbtngvProductLast.Enabled = true;
            }
            
        }

        private void setDDl(DropDownList ddls, String val)
        {
            System.Web.UI.WebControls.ListItem li;
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

        public List<InventoryDetailInfo> GetInventoryDetailListPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = Request.QueryString["InventoryCode"];
                data["ProductCode"] = txtSearchProductCode.Text;
                data["ProductName"] = txtSearchProductName.Text;
                data["MerchantCode"] = hidMerCode.Value;
                data["ProductCategoryCode"] = ddlSearchProductCategory.SelectedValue;
                data["ProductBrandCode"] = ddlSearchProductBrand.SelectedValue;
                data["FlagDelete"] = "N";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfo> lInventoryDetailInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfo>>(respstr);

            return lInventoryDetailInfo;
        }

        protected void LoadRoutingMapInventory()
        {
            List<RoutingMapInventoryDetailInfo> lroutingmapinvInfo = new List<RoutingMapInventoryDetailInfo>();
            lroutingmapinvInfo = GetRoutingmapInvenListNoPaging();

            myRepeater.DataSource = lroutingmapinvInfo;
            myRepeater.DataBind();         
        }

        public List<RoutingMapInventoryDetailInfo> GetRoutingmapInvenListNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRoutingInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Inventory_Code"] = Request.QueryString["InventoryCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RoutingMapInventoryDetailInfo> lRoutingmapInventoryInfo = JsonConvert.DeserializeObject<List<RoutingMapInventoryDetailInfo>>(respstr);

            return lRoutingmapInventoryInfo;
        }

        protected void ClearSearch()
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
            ddlSearchProductCategory.SelectedValue = "-99";
            txtBrand.Text = "";
        }

        protected void LoadProduct()
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();
            int? totalRow = CountProductMasterList();
            SetPageBargvProduct(Convert.ToDouble(totalRow));
            lProductInfo = GetProductMasterByCriteria();

            if (lProductInfo.Count > 0)
            {
                foreach (var lp in lProductInfo.ToList())
                {
                    lp.Amount = 0;
                }
            }

            gvProduct.DataSource = lProductInfo;

            gvProduct.DataBind();
        }

        public int? CountProductMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountProductListModalByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = txtSearchProductCode_ProductModal.Text;
                data["ProductName"] = txtSearchProductName_ProductModal.Text;
                
                data["ProductCategoryCode"] = ddlSearchCategory_ProductModal.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        public List<ProductInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = txtSearchProductCode_ProductModal.Text;
                data["ProductName"] = txtSearchProductName_ProductModal.Text;
                data["ProductCategoryCode"] = ddlSearchCategory_ProductModal.SelectedValue;
                data["MerchantCode"] = hidMerCode.Value;
                data["ProductBrandCode"] = (txtBrand.Text != "" ? txtBrand.Text : "-99");
                data["ProductCategoryCode"] = ddlSearchCategory_ProductModal.SelectedValue;
                data["MerchantCode"] = hidMerCode.Value;
                data["rowOFFSet"] = ((currentPageNumbergvProduct - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }

        protected void LoadPOinModal()
        {
            POInfo poInfo = new POInfo();
            List<POInfo> lPOInfo = new List<POInfo>();
            lPOInfo = GetPOinModalByCriteria();
            String codelist = "";

            if (lPOInfo.Count > 0)
            {
                foreach (var poininvmvm in lPOInfo)
                {
                    if (codelist != "")
                    {
                        codelist += ",'" + poininvmvm.POCode + "'";
                    }
                    else
                    {
                        codelist = poininvmvm.POCode + "'";
                    }
                }
            }
            codelist = codelist.Remove(codelist.Length - 1);

            poInfo.POCode = codelist;
            List<POInfo> lpomodalInfo = new List<POInfo>();
            lpomodalInfo = GetPOListModal(poInfo);

            gvPO.DataSource = lpomodalInfo;
            gvPO.DataBind();
        }

        protected void LoadPOinModalfromSearch()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOMOdalmapInventoryDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = txtSearchPOCode_POModal.Text;
                data["SupplierCode"] = ddlSearchSupplierName_POModal.SelectedValue;

                if (ddlSearchSupplierName_POModal.SelectedValue == "-99")
                {
                    data["SupplierName"] = "";
                }
                else
                {
                    data["SupplierName"] = ddlSearchSupplierName_POModal.SelectedItem.ToString();
                }

                data["MerchantCode"] = ddlSearchMerchantName_POModal.SelectedValue;

                if (ddlSearchMerchantName_POModal.SelectedValue == "-99")
                {
                    data["MerchantName"] = "";
                }
                else
                {
                    data["MerchantName"] = ddlSearchMerchantName_POModal.SelectedItem.ToString();
                }
                
                data["CreateDate"] = txtSearchCreateDateFrom.Text;
                data["CreateDateTo"] = txtSearchCreateDateTo.Text;
                data["RequestDate"] = txtSearchRequestDateFrom.Text;
                data["txtSearchRequestDateTo"] = txtSearchRequestDateTo.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<POInfo> lPOInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);

            gvPO.DataSource = lPOInfo;
            gvPO.DataBind();
        }

        protected void LoadPOItemMapProduct(string POCode)
        {
            List<POInfo> lPOInfo = new List<POInfo>();

            lPOInfo = GetPOItemMapProduct(POCode);

            gvPOItem.DataSource = lPOInfo;

            gvPOItem.DataBind();
        }

        public List<POInfo> GetPOinModalByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOmapInventoryDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = txtSearchPOCode_POModal.Text;

                data["ProductCode"] = txtSearchPOCode_POModal.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<POInfo> lPOInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);

            return lPOInfo;
        }

        public List<POInfo> GetPOListModal(POInfo poInfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOMOdalmapInventoryDetailByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCodeList"] = poInfo.POCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<POInfo> lPOInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);

            return lPOInfo;
        }

        public List<POInfo> GetPOItemMapProduct(string POCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOItemMapProductByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = POCode;

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<POInfo> lPOInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);

            return lPOInfo;
        }
        public List<InventoryDetailInfoNew> GetInventorydetail(InventoryInfo iInfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = Request.QueryString["InventoryCode"];
                data["ProductCode"] = iInfo.ProductCode;
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfoNew> lPOInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfoNew>>(respstr);

            return lPOInfo;
        }
        public int GetMaxSeqInvMovement(InventoryMovementInfo iInfo)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMaxSeqIdByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryDetailId"] = iInfo.InventoryDetailId.ToString();
                data["ProductCode"] = iInfo.ProductCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int sum = JsonConvert.DeserializeObject<int>(respstr);

            return sum;
        }
        public int? GetMaxSeqManual(int? inventorydetailid, string productcode)
        {
            int? sum = 0;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMaxSeqManualIdByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryDetailId"] = inventorydetailid.ToString();
                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);                
            }

            sum = JsonConvert.DeserializeObject<int?>(respstr);

            return sum;
        }
        public int? InsertInventoryMovement(InventoryMovementInfo imInfo)
        {
            int? sum = 0;
            string respstr = "";
            HiddenField hidSupplierCode = (HiddenField)FindControl("hidSupplierCode");
            APIpath = APIUrl + "/api/support/InsertInventoryMovement";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryDetailId"] = imInfo.InventoryDetailId.ToString();
                data["InventoryMovementCode"] = imInfo.InventoryMovementCode;
                data["InventoryManualLotCode"] = imInfo.InventoryManualLotCode;
                data["POCode"] = txtPurchaseOrder.Text; 
                data["SupplierCode"] = hidSupplierCode.Value;
                data["ProductCode"] = imInfo.ProductCode;
                data["SeqId"] = imInfo.SeqId.ToString();
                data["SeqManId"] = imInfo.SeqManId.ToString();
                data["CreateBy"] = imInfo.CreateBy;
                data["UpdateBy"] = imInfo.UpdateBy;
                data["ActiveFlag"] = imInfo.ActiveFlag;
                data["Remark"] = "Manual Update";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);

                sum = JsonConvert.DeserializeObject<int?>(respstr);                
            }

            return sum;
        }
        protected List<MerchantInfo> LoadMerchant()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/MerchantListNoPagingCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lMerchantInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);

            return lMerchantInfo;
        }

        protected List<SupplierInfo> LoadSupplier()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSupplierNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["SupplierCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lSupplierInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);

            return lSupplierInfo;
        }

        protected List<ProductCategoryInfo> LoadProductCategory()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductCategoryNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCategoryCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductCategoryInfo> lProductCategory = JsonConvert.DeserializeObject<List<ProductCategoryInfo>>(respstr);

            return lProductCategory;
        }

        protected void updateProduct(string POCode) 
        {
            List<POInfo> lPOInfo = new List<POInfo>();
            List<POInfo> lPOUpdInfo = new List<POInfo>(); // list for insert InventoryMovement when Update all Product in InventoryDetail

            lPOInfo = GetPOItemMapProduct(POCode); // List of PO Item going to insert or update into inventorydetail

            InventoryDetailInfoNew ivd = new InventoryDetailInfoNew();
            List<InventoryDetailInfoNew> livd = new List<InventoryDetailInfoNew>();
            List<InventoryDetailInfoNew> liup = new List<InventoryDetailInfoNew>();

            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            InventoryInfo ivinfo = new InventoryInfo();
            List<InventoryDetailInfoNew> linventorydetail = new List<InventoryDetailInfoNew>();
            linventorydetail = GetInventorydetail(ivinfo); // List Product from inventory

            Boolean flaginsupd = true;

            foreach (var lpoitem in lPOInfo.ToList())
            {
                if (lPOInfo.Count > 0)
                {
                    foreach (var linven in linventorydetail)
                    {
                        if (lpoitem.ProductCode == linven.ProductCode)
                        {
                            ivd = new InventoryDetailInfoNew();

                            linven.QTY = linven.QTY + lpoitem.QTY;
                            linven.Balance = linven.QTY - linven.Reserved;

                            ivd.InventoryDetailId = linven.InventoryDetailId;
                            ivd.POCode = lpoitem.POCode;
                            ivd.InventoryCode = linven.InventoryCode;
                            ivd.ProductCode = linven.ProductCode;
                            ivd.QTY = linven.QTY;
                            ivd.Reserved = linven.Reserved;
                            ivd.Current = linven.Balance;
                            ivd.Balance = linven.Balance;
                            ivd.UpdateBy = empInfo.EmpCode;

                            liup.Add(ivd); // Add List of Product with Duplicate prepare update
                            lPOInfo.RemoveAll(s => s.ProductCode == linven.ProductCode);
                            lPOUpdInfo.Add(lpoitem);

                            flaginsupd = (flaginsupd == false) ? false : true;
                        }
                    }
                }
            }
            
            if (lPOInfo.Count > 0) // List of Product Not Duplicate with InventoryDetail
            {                
                foreach (var lnew in lPOInfo.ToList())
                {
                    ivd = new InventoryDetailInfoNew();

                    ivd.InventoryCode = Request.QueryString["InventoryCode"];
                    ivd.ProductCode = lnew.ProductCode;
                    ivd.QTY = lnew.QTY;
                    ivd.Reserved = 0;
                    ivd.Current = ivd.QTY - ivd.Reserved;
                    ivd.Balance = ivd.QTY - ivd.Reserved;
                    ivd.POCode = lnew.POCode;
                    ivd.CreateBy = empInfo.EmpCode;
                    ivd.UpdateBy = empInfo.EmpCode;

                    livd.Add(ivd); // Add List of Product with Not Duplicate with InventoryDetail
                }
            }

            int? sum = 0;
            int? sum1 = 0;
            int? sum3 = 0;
            int? sum4 = 0;
            int? sum5 = 0;

            foreach (var linvins in livd.ToList()) // Insert InventoryDetail
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/InsertInventoryDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["InventoryCode"] = linvins.InventoryCode;
                    data["ProductCode"] = linvins.ProductCode;
                    data["POCode"] = linvins.POCode;
                    data["QTY"] = linvins.QTY.ToString();
                    data["Reserved"] = linvins.Reserved.ToString();
                    data["Current"] = linvins.Current.ToString();
                    data["Balance"] = linvins.Balance.ToString();
                    data["CreateBy"] = linvins.CreateBy;
                    data["UpdateBy"] = linvins.UpdateBy;
                    data["FlagDelete"] = "N";
                    
                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);

                    sum = JsonConvert.DeserializeObject<int?>(respstr);

                    flaginsupd = (flaginsupd == false) ? false : true;
                }
            }            

            if (liup.Count > 0) // Update InventoryDetail
            {
                foreach (var lupd in liup.ToList())
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateInventoryDetail";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["InventoryDetailId"] = lupd.InventoryDetailId.ToString();
                        data["POCode"] = lupd.POCode;
                        data["ProductCode"] = lupd.ProductCode;
                        data["QTY"] = lupd.QTY.ToString();
                        data["Reserved"] = lupd.Reserved.ToString();
                        data["Current"] = lupd.Current.ToString();
                        data["Balance"] = lupd.Balance.ToString();
                        data["UpdateBy"] = lupd.UpdateBy;

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);

                        sum3 = JsonConvert.DeserializeObject<int?>(respstr);

                        flaginsupd = (flaginsupd == false) ? false : true;
                    }
                }
            } 
            
            // Insert InventoryMovement from New Product in InventoryDetail
            foreach (var lpoitem in lPOInfo)
            {
                for (int i = 1; i <= lpoitem.QTY; i++)
                {
                    InventoryInfo iinfo = new InventoryInfo();
                    InventoryMovementInfo imvInfo = new InventoryMovementInfo();                    
                    List<InventoryDetailInfoNew> linfo = new List<InventoryDetailInfoNew>();

                    iinfo.ProductCode = lpoitem.ProductCode;
                    iinfo.InventoryCode = Request.QueryString["InventoryCode"];

                    linfo = GetInventorydetail(iinfo);
                    int? maxseq = 0;
                    String maxinventorymovementseq = "";

                    if (linfo.Count > 0)
                    {
                        imvInfo.InventoryDetailId = linfo[0].InventoryDetailId;
                        imvInfo.ProductCode = iinfo.ProductCode;

                        maxseq = GetMaxSeqInvMovement(imvInfo);
                        maxinventorymovementseq = imvInfo.InventoryDetailId + imvInfo.ProductCode + String.Format("{0:0000}", maxseq);
                    }

                    InventoryMovementInfo invInfo = new InventoryMovementInfo();

                    invInfo.InventoryDetailId = imvInfo.InventoryDetailId;
                    invInfo.POCode = lpoitem.POCode;
                    invInfo.ProductCode = lpoitem.ProductCode;
                    invInfo.SeqId = maxseq;
                    invInfo.InventoryMovementCode = maxinventorymovementseq;
                    invInfo.CreateBy = empInfo.EmpCode;
                    invInfo.UpdateBy = empInfo.EmpCode;
                    invInfo.ActiveFlag = "Y";

                    sum5 = InsertInventoryMovement(invInfo);
                }
                flaginsupd = (flaginsupd == false) ? false : true;
            }

            // Insert InventoryMovement from Update Product in InventoryDetail
            if (lPOUpdInfo.Count > 0)
            {
                foreach (var lpoitem in lPOUpdInfo)
                {
                    for (int i = 1; i <= lpoitem.QTY; i++)
                    {
                        InventoryInfo iinfo = new InventoryInfo();
                        InventoryMovementInfo imvInfo = new InventoryMovementInfo();
                        List<InventoryDetailInfoNew> linfo = new List<InventoryDetailInfoNew>();

                        iinfo.ProductCode = lpoitem.ProductCode;
                        iinfo.InventoryCode = Request.QueryString["InventoryCode"];

                        linfo = GetInventorydetail(iinfo);
                        int? maxseq = 0;
                        String maxinventorymovementseq = "";

                        if (linfo.Count > 0)
                        {
                            imvInfo.InventoryDetailId = linfo[0].InventoryDetailId;
                            imvInfo.ProductCode = iinfo.ProductCode;

                            maxseq = GetMaxSeqInvMovement(imvInfo);
                            maxinventorymovementseq = imvInfo.InventoryDetailId + imvInfo.ProductCode + String.Format("{0:0000}", maxseq);
                        }

                        InventoryMovementInfo invInfo = new InventoryMovementInfo();

                        invInfo.InventoryDetailId = imvInfo.InventoryDetailId;
                        invInfo.POCode = lpoitem.POCode;
                        invInfo.ProductCode = lpoitem.ProductCode;
                        invInfo.SeqId = maxseq;
                        invInfo.InventoryMovementCode = maxinventorymovementseq;
                        invInfo.CreateBy = empInfo.EmpCode;
                        invInfo.UpdateBy = empInfo.EmpCode;
                        invInfo.ActiveFlag = "Y";

                        sum5 = InsertInventoryMovement(invInfo);
                    }
                    flaginsupd = (flaginsupd == false) ? false : true;
                }
            }
            
            if (flaginsupd == true)
            {
                LoadInventoryDetail();
                LoadPOinModal();
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-show-product').modal('hide');", true);
            }
        }

        protected List<ProductInfo> GetProductList()
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ProductCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> MenuInfo_list = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return MenuInfo_list;

        }

        protected void InsertInventoryMovement()
        {

        }
        protected void ExportInventoryDetail()
        {
            var dataExcel = new NameValueCollection();
            List<InventoryDetailInfoNew> olist = new List<InventoryDetailInfoNew>();
            olist.Add(new InventoryDetailInfoNew
            {
                InventoryCode = Request.QueryString["InventoryCode"]
                ,
                ProductCode = txtSearchProductCode.Text
                ,
                ProductName = txtSearchProductName.Text
                ,
                ProductCategoryCode = ddlSearchProductCategory.SelectedValue,
                ProductBrandCode = ddlSearchProductBrand.SelectedValue,

                MerchantCode = hidMerCode.Value,

                FlagDelete = "N"               
            });


            Session["dataExportExcel"] = olist;
            string URL = "ReportInventoryDetail_Excel.aspx";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URL + "','_blank')", true);
        }
        #endregion Function





        #region Binding
        protected string BindProvince(string ProvinceCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = ProvinceCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);

            return lProvinceInfo[0].ProvinceName;
        }

        protected string BindDistrict(string DistrictCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DistrictCode"] = DistrictCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);

            return lDistrictInfo[0].DistrictName;
        }

        protected string BindSubDistrict(string SubDistrictCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["SubDistrictCode"] = SubDistrictCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);

            return lSubDistrictInfo[0].SubDistrictName;
        }

        protected void BindMerchant_POModal(List<MerchantInfo> mInfo)
        {
            ddlSearchMerchantName_POModal.DataSource = mInfo;
            ddlSearchMerchantName_POModal.DataTextField = "MerchantName";
            ddlSearchMerchantName_POModal.DataValueField = "MerchantCode";
            ddlSearchMerchantName_POModal.DataBind();
            ddlSearchMerchantName_POModal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindMerchant_ProductModal(List<MerchantInfo> mInfo)
        {
            
        }

        protected void BindSupplier_ProductModal(List<SupplierInfo> mInfo)
        {
            
        }

        protected void BindProductCategory_ProductModal(List<ProductCategoryInfo> mInfo)
        {
            ddlSearchCategory_ProductModal.DataSource = mInfo;
            ddlSearchCategory_ProductModal.DataTextField = "ProductCategoryName";
            ddlSearchCategory_ProductModal.DataValueField = "ProductCategoryCode";
            ddlSearchCategory_ProductModal.DataBind();
            ddlSearchCategory_ProductModal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindSupplier_POModal(List<SupplierInfo> mInfo)
        {
            ddlSearchSupplierName_POModal.DataSource = mInfo;
            ddlSearchSupplierName_POModal.DataTextField = "SupplierName";
            ddlSearchSupplierName_POModal.DataValueField = "SupplierCode";
            ddlSearchSupplierName_POModal.DataBind();

            ddlSearchSupplierName_POModal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("---- กรุณาเลือก ----", "-99"));
        }
        /*protected void BindddlProductOrder()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPONopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<POInfo> lInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);

            ddlProductOrder_Ins.DataSource = lInfo;
            ddlProductOrder_Ins.DataValueField = "POCode";
            ddlProductOrder_Ins.DataTextField = "POCode";
            ddlProductOrder_Ins.DataBind();


            ddlProductOrder_Ins.Items.Insert(0, new System.Web.UI.WebControls.ListItem("กรุณาเลือก", ""));
        }*/
        /*protected void BindddlSupplier()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSupplierNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["SupplierCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);

            ddlSupplier_Ins.DataSource = lInfo;
            ddlSupplier_Ins.DataValueField = "SupplierCode";
            ddlSupplier_Ins.DataTextField = "SupplierName";
            ddlSupplier_Ins.DataBind();


            ddlSupplier_Ins.Items.Insert(0, new System.Web.UI.WebControls.ListItem("กรุณาเลือก", ""));
        }*/
        protected void BindddlProductCategry()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductCategoryNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCategoryCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductCategoryInfo> lProductCategoryInfo = JsonConvert.DeserializeObject<List<ProductCategoryInfo>>(respstr);

            ddlSearchProductCategory.DataSource = lProductCategoryInfo;
            ddlSearchProductCategory.DataValueField = "ProductCategoryCode";
            ddlSearchProductCategory.DataTextField = "ProductCategoryName";
            ddlSearchProductCategory.DataBind();

            ddlSearchCategory_ProductModal.DataSource = lProductCategoryInfo;
            ddlSearchCategory_ProductModal.DataValueField = "ProductCategoryCode";
            ddlSearchCategory_ProductModal.DataTextField = "ProductCategoryName";
            ddlSearchCategory_ProductModal.DataBind();

            ddlSearchProductCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("กรุณาเลือก", "-99"));
            ddlSearchCategory_ProductModal.Items.Insert(0, new System.Web.UI.WebControls.ListItem("กรุณาเลือก", "-99"));
        }

        /*protected void BindddlSearchProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lProductBrandInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);

            ddlSearchProductBrand.DataSource = lProductBrandInfo;
            ddlSearchProductBrand.DataValueField = "ProductBrandCode";
            ddlSearchProductBrand.DataTextField = "ProductBrandName";
            ddlSearchProductBrand.DataBind();

            ddlSearchBrand_ProductModal1.DataSource = lProductBrandInfo;
            ddlSearchBrand_ProductModal1.DataValueField = "ProductBrandCode";
            ddlSearchBrand_ProductModal1.DataTextField = "ProductBrandName";
            ddlSearchBrand_ProductModal1.DataBind();

            ddlSearchProductBrand.Items.Insert(0, new System.Web.UI.WebControls.ListItem("กรุณาเลือก", "-99"));
            ddlSearchBrand_ProductModal1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("กรุณาเลือก", "-99"));
        }*/
        #endregion Binding





        #region Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadInventoryDetail();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        protected void btnClearSearch_ProductModal_Click(object sender, EventArgs e)
        {
            txtSearchProductCode_ProductModal.Text = "";
            txtSearchProductName_ProductModal.Text = "";
            ddlSearchCategory_ProductModal.SelectedValue = "-99";
            txtBrand.Text = "";
        }

        

        protected void GetPageIndex(object sender, CommandEventArgs e)
        {

            switch (e.CommandName)
            {
                case "First":
                    currentPageNumber = 1;
                    break;

                case "Previous":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPageNumber = Int32.Parse(ddlPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPageNumber = Int32.Parse(lblTotalPages.Text);
                    break;
            }

            LoadInventoryDetail();
        }        
        protected void GetPagegvProductIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "gvProductFirst":
                    currentPageNumbergvProduct = 1;
                    break;

                case "gvProductPrevious":
                    currentPageNumbergvProduct = Int32.Parse(ddlgvProductPage.SelectedValue) - 1;
                    break;

                case "gvProductNext":
                    currentPageNumbergvProduct = Int32.Parse(ddlgvProductPage.SelectedValue) + 1;
                    break;

                case "gvProductLast":
                    currentPageNumbergvProduct = Int32.Parse(lblgvProductTotalPages.Text);
                    break;
            }

            LoadProduct();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            LoadInventoryDetail();
        }
        protected void ddlgvProductPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumbergvProduct = Int32.Parse(ddlgvProductPage.SelectedValue);
            LoadProduct();
        }
        protected void btnChooseProduct_Click(object sender, EventArgs e)
        {
            BindMerchant_ProductModal(LoadMerchant());
            BindSupplier_ProductModal(LoadSupplier());
            BindProductCategory_ProductModal(LoadProductCategory());

            foreach (GridViewRow row in gvProduct.Rows)
            {
                TextBox txtAmountIns = (TextBox)row.FindControl("txtAmountIns");

                txtAmountIns.Text = "0";
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
        }

        protected void btnSearch_POModal_Click(object sender, EventArgs e)
        {
            LoadPOinModalfromSearch();
        }

        protected void btnClearSearch_POModal_Click(object sender, EventArgs e)
        {
            txtSearchPOCode_POModal.Text = "";
            ddlSearchSupplierName_POModal.SelectedValue = "-99";
            ddlSearchMerchantName_POModal.SelectedValue = "-99";
            txtSearchCreateDateFrom.Text = "";
            txtSearchCreateDateTo.Text = "";
            txtSearchRequestDateFrom.Text = "";
            txtSearchRequestDateTo.Text = "";
        }

        protected void btnChoosePO_Click(object sender, EventArgs e)
        {
            LoadPOinModal();
            BindMerchant_POModal(LoadMerchant());
            BindSupplier_POModal(LoadSupplier());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-po').modal();", true);
        }

        protected void btnAddInventoryDetail_Click(object sender, EventArgs e)
        {
            updateProduct(lblPOCodeTest.Text);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void gvPO_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPO.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidPOCode = (HiddenField)row.FindControl("hidPOCode");

            if (e.CommandName == "AddPO")
            {
                LoadPOItemMapProduct(hidPOCode.Value);
                lblPOCodeTest.Text = hidPOCode.Value;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-show-product').modal();", true);
            }
        }

        protected void gvInventoryDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvInventoryDetail.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidInventoryDetailId = (HiddenField)row.FindControl("hidInventoryDetailId");
            HiddenField hidInventoryCode = (HiddenField)row.FindControl("hidInventoryCode");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidQTY = (HiddenField)row.FindControl("hidQTY");
            HiddenField hidReserved = (HiddenField)row.FindControl("hidReserved");
            HiddenField hidBalance = (HiddenField)row.FindControl("hidBalance");
            HiddenField hidPickpack = (HiddenField)row.FindControl("hidPickpack");
            HiddenField hidReOrder = (HiddenField)row.FindControl("hidReOrder");
            Label lblOnhand = (Label)row.FindControl("lblOnhand");
            TextBox txtOnhand = (TextBox)row.FindControl("txtOnhand");
            Label lblSafetyStock = (Label)row.FindControl("lblSafetyStock");
            TextBox txtSafetyStock = (TextBox)row.FindControl("txtSafetyStock");
            LinkButton btnEdit = (LinkButton)row.FindControl("btnEdit");
            LinkButton btnSave = (LinkButton)row.FindControl("btnSave");
            LinkButton btnCancel = (LinkButton)row.FindControl("btnCancel");

            if (e.CommandName == "EditInventoryDetail")
            {
                lblOnhand.Visible = false;
                txtOnhand.Visible = true;
                lblSafetyStock.Visible = false;
                txtSafetyStock.Visible = true;

                txtOnhand.Enabled = true;
                txtSafetyStock.Enabled = true;
                btnEdit.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = false;
            }

            if (e.CommandName == "SaveInventoryDetail")
            {
                lblOnhand.Visible = true;
                txtOnhand.Visible = false;
                lblSafetyStock.Visible = true;
                txtSafetyStock.Visible = false;

                txtOnhand.Enabled = false;
                txtSafetyStock.Enabled = false;

                int? sum = 0;

                if (Convert.ToInt32(txtOnhand.Text) > 0 && Convert.ToInt32(lblSafetyStock.Text) > 0)
                {
                    int? current = Convert.ToInt32(txtOnhand.Text) - Convert.ToInt32(hidReserved.Value);
                    
                    int? diffOnhand = Convert.ToInt32(txtOnhand.Text) - Convert.ToInt32(lblOnhand.Text);
                    int? balance = Convert.ToInt32(hidBalance.Value) + diffOnhand;
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateInventoryDetail";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["InventoryDetailId"] = hidInventoryDetailId.Value;
                        data["ProductCode"] = hidProductCode.Value;
                        data["QTY"] = txtOnhand.Text;
                        data["Reserved"] = hidReserved.Value;
                        data["Current"] = current.ToString();
                        data["PickPack"] = hidPickpack.Value;
                        data["Balance"] = balance.ToString();
                        data["SafetyStock"] = lblSafetyStock.Text;
                        data["UpdateBy"] = empInfo.EmpCode;

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);

                        sum = JsonConvert.DeserializeObject<int?>(respstr);
                    }

                    if (sum > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-product').modal('hide');", true);
                        LoadInventoryDetail();
                        btnEdit.Visible = true;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('จำนวนสินค้าต้องมากกว่า 0');", true);
                    btnEdit.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                }
            }

            if (e.CommandName == "CancelInventoryDetail")
            {
                LoadInventoryDetail();
                btnEdit.Visible = true;
                btnSave.Visible = false;
                btnCancel.Visible = false;
            }
        }

        protected void gvInventoryDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblOnhand = (Label)e.Row.FindControl("lblOnhand");
                TextBox txtOnhand = (TextBox)e.Row.FindControl("txtOnhand");
                Label lblSafetyStock = (Label)e.Row.FindControl("lblSafetyStock");
                TextBox txtSafetyStock = (TextBox)e.Row.FindControl("txtSafetyStock");
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnEdit");
                LinkButton btnSave = (LinkButton)e.Row.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Row.FindControl("btnCancel");
                HiddenField hidReOrder = (HiddenField)e.Row.FindControl("hidReOrder");
                HiddenField hidBalance = (HiddenField)e.Row.FindControl("hidBalance");
                Label lblReOrder = (Label)e.Row.FindControl("lblReOrder");

                lblOnhand.Visible = true;
                txtOnhand.Visible = false;
                
                btnEdit.Visible = false;
                btnSave.Visible = false;
                btnCancel.Visible = false;

                if (Convert.ToInt32(hidBalance.Value) <= Convert.ToInt32(lblSafetyStock.Text))
                {
                    lblReOrder.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected String GetLink(object objCode, object objId)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";

            string strId = (objId != null) ? objId.ToString() : "";

            string invcode = (Request.QueryString["InventoryCode"] != null) ? Request.QueryString["InventoryCode"].ToString() : "";

            return "<a href=\"InventoryMovement.aspx?ProductCode=" + strCode + "&InventoryDetailId=" + strId + "&InventoryCode=" + invcode + "\">" + strCode + "</a>";
        }

        public void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProduct.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidProductId = (HiddenField)row.FindControl("hidProductId");
            HiddenField hidProductCodeIns = (HiddenField)row.FindControl("hidProductCodeIns");
            HiddenField hidProductNameIns = (HiddenField)row.FindControl("hidProductNameIns");
            HiddenField hidSupplierCodeIns = (HiddenField)row.FindControl("hidSupplierCodeIns");
            HiddenField hidSupplierNameIns = (HiddenField)row.FindControl("hidSupplierNameIns");
            HiddenField hidMerchantCodeIns = (HiddenField)row.FindControl("hidMerchantCodeIns");
            HiddenField hidMerchantNameIns = (HiddenField)row.FindControl("hidMerchantNameIns");
            HiddenField hidProductCategoryCodeIns = (HiddenField)row.FindControl("hidProductCategoryCodeIns");
            HiddenField hidProductCategoryNameIns = (HiddenField)row.FindControl("hidProductCategoryNameIns");
            TextBox txtAmountIns = (TextBox)row.FindControl("txtAmountIns");
            Label lblSafetyStock = (Label)row.FindControl("lblSafetyStock");

            if (e.CommandName == "AddProduct")
            {
                InventoryInfo dInfo = new InventoryInfo();
                InventoryDetailInfoNew dtInfo = new InventoryDetailInfoNew();
                List<InventoryDetailInfoNew> ldInfo = new List<InventoryDetailInfoNew>();
                HiddenField hidSupplierCode = (HiddenField)FindControl("hidSupplierCode");
                dInfo.ProductCode = hidProductCodeIns.Value;

                ldInfo = GetInventorydetail(dInfo);
                int? sum = 0;
                int? sum1 = 0;
                int? sum2 = 0;

                if (txtAmountIns.Text != "")
                {
                    if (Convert.ToInt32(txtAmountIns.Text) > 0)
                    {
                        if (ldInfo.Count > 0) // Duplicate in InventoryDetail
                        {
                            dtInfo = new InventoryDetailInfoNew();

                            dtInfo.QTY = ldInfo[0].QTY + Convert.ToInt32(txtAmountIns.Text);
                            dtInfo.Current = dtInfo.QTY - ldInfo[0].Reserved;
                            dtInfo.Balance = ldInfo[0].Balance + Convert.ToInt32(txtAmountIns.Text);
                            dtInfo.PickPack = ldInfo[0].Balance + Convert.ToInt32(txtAmountIns.Text);


                            string respstr = "";

                            APIpath = APIUrl + "/api/support/UpdateInventoryDetail";

                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();

                                data["InventoryDetailId"] = ldInfo[0].InventoryDetailId.ToString();
                                data["ProductCode"] = hidProductCodeIns.Value;
                                data["QTY"] = dtInfo.QTY.ToString();
                                data["Reserved"] = ldInfo[0].Reserved.ToString();
                                data["Current"] = dtInfo.Current.ToString();
                                data["Balance"] = dtInfo.Balance.ToString();
                                data["SafetyStock"] = (dtInfo.SafetyStock.ToString() == "") ? "1" : dtInfo.SafetyStock.ToString();
                                data["UpdateBy"] = empInfo.EmpCode;
                                data["SupplierCode"] = hidSupplierCode.Value;
                                data["POCode"] = txtPurchaseOrder.Text;


                                var response = wb.UploadValues(APIpath, "POST", data);
                                respstr = Encoding.UTF8.GetString(response);

                                sum = JsonConvert.DeserializeObject<int?>(respstr);
                            }

                            if (sum > 0)
                            {
                                
                            }
                        }
                        else // Insert into InventoryDetail
                        {
                            string respstr = "";

                            APIpath = APIUrl + "/api/support/InsertInventoryDetail";

                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();

                                data["InventoryCode"] = Request.QueryString["InventoryCode"];
                                data["ProductCode"] = hidProductCodeIns.Value;
                                data["QTY"] = txtAmountIns.Text;
                                data["Reserved"] = "0";
                                data["SafetyStock"] = "1";
                                data["Current"] = txtAmountIns.Text;
                                data["Balance"] = txtAmountIns.Text;
                                data["CreateBy"] = empInfo.EmpCode;
                                data["UpdateBy"] = empInfo.EmpCode;
                                data["FlagDelete"] = "N";
                                data["SupplierCode"] = hidSupplierCode.Value;
                                data["POCode"] = txtPurchaseOrder.Text;


                                var response = wb.UploadValues(APIpath, "POST", data);
                                respstr = Encoding.UTF8.GetString(response);

                                sum1 = JsonConvert.DeserializeObject<int?>(respstr);
                            }
                        }

                        if (sum > 0 || sum1 > 0)
                        {
                            LoadInventoryDetail();

                            InventoryInfo iinfo = new InventoryInfo();
                            List<InventoryDetailInfoNew> linfo = new List<InventoryDetailInfoNew>();
                            InventoryMovementInfo imvInfo = new InventoryMovementInfo();

                            iinfo.ProductCode = hidProductCodeIns.Value;
                            iinfo.InventoryCode = Request.QueryString["InventoryCode"];

                            linfo = GetInventorydetail(iinfo);

                            int productamount = (txtAmountIns.Text != "") ? Convert.ToInt32(txtAmountIns.Text) : 0;

                            string currentYear = DateTime.Now.Year.ToString();
                            string currentMonth = DateTime.Now.ToString("MM");
                            string currentDate = DateTime.Now.Date.ToString("dd");

                            string c1 = "";
                            string c2 = "";
                            string c3 = "";
                            string c4 = "";

                            for (int i = 0; i <= currentYear.Length - 1; i++)
                            {
                                if (i == 0)
                                {
                                    c1 = currentYear[i].ToString();
                                }
                                if (i == 1)
                                {
                                    c2 = currentYear[i].ToString();
                                }
                                if (i == 2)
                                {
                                    c3 = currentYear[i].ToString();
                                }
                                if (i == 3)
                                {
                                    c4 = currentYear[i].ToString();
                                }
                            }

                            currentYear = c3 + c4;

                            int? maxmanualseq = GetMaxSeqManual(linfo[0].InventoryDetailId, iinfo.ProductCode);
                            string maxmanualseqcode = "MID" + currentYear + currentMonth + currentDate + String.Format("{0:0000}", maxmanualseq);

                            //Looping InventoryMovement Insert

                            for (int i = 1; i <= productamount; i++)
                            {
                                int? maxseq = 0;
                                String maxinventorymovementseq = "";

                                if (linfo.Count > 0)
                                {
                                    imvInfo.InventoryDetailId = linfo[0].InventoryDetailId;
                                    imvInfo.ProductCode = iinfo.ProductCode;

                                    maxseq = GetMaxSeqInvMovement(imvInfo); //Movement each product or call Lot
                                                                            
                                    maxinventorymovementseq = iinfo.InventoryCode + imvInfo.ProductCode + currentYear + currentMonth + String.Format("{0:0000}", maxseq);
                                }

                                InventoryMovementInfo invInfo = new InventoryMovementInfo();

                                invInfo.InventoryDetailId = imvInfo.InventoryDetailId;
                                invInfo.ProductCode = hidProductCodeIns.Value;
                                invInfo.SeqId = maxseq;
                                invInfo.SeqManId = maxmanualseq;
                                invInfo.InventoryMovementCode = maxinventorymovementseq;
                                invInfo.InventoryManualLotCode = maxmanualseqcode;
                                invInfo.CreateBy = empInfo.EmpCode;
                                invInfo.ActiveFlag = "Y";

                                sum2 = InsertInventoryMovement(invInfo);
                            }

                            //End Looping InventoryMovement Insert
                            //above code
                        }

                        if (sum > 0 || sum1 > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal();", true);
                            LoadInventoryDetail();
                            LoadProduct();
                        }
                    }
                    else if (Convert.ToInt32(txtAmountIns.Text) <= 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('จำนวนสินค้าต้องมากกว่า 0');", true);
                    }                    
                }
                else if (txtAmountIns.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาระบุจำนวนสินค้า');", true);
                }
            }
        }

        protected void btnSearch_ProductModal_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            ExportInventoryDetail();
        }

        protected void btnImportProduct_Click(object sender, EventArgs e)
        {

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            HiddenField hidSupplierCode = (HiddenField)FindControl("hidSupplierCode");

            foreach (GridViewRow row in gvProduct.Rows)
            {
                
                    Label lblmsg = (Label)row.FindControl("lblmsg");
                    HiddenField hidProductId = (HiddenField)row.FindControl("hidProductId");
                    HiddenField hidProductCodeIns = (HiddenField)row.FindControl("hidProductCodeIns");
                    HiddenField hidProductNameIns = (HiddenField)row.FindControl("hidProductNameIns");
                    HiddenField hidSupplierCodeIns = (HiddenField)row.FindControl("hidSupplierCodeIns");
                    HiddenField hidSupplierNameIns = (HiddenField)row.FindControl("hidSupplierNameIns");
                    HiddenField hidMerchantCodeIns = (HiddenField)row.FindControl("hidMerchantCodeIns");
                    HiddenField hidMerchantNameIns = (HiddenField)row.FindControl("hidMerchantNameIns");
                    HiddenField hidProductCategoryCodeIns = (HiddenField)row.FindControl("hidProductCategoryCodeIns");
                    HiddenField hidProductCategoryNameIns = (HiddenField)row.FindControl("hidProductCategoryNameIns");
                    
                TextBox txtAmountIns = (TextBox)row.FindControl("txtAmountIns");
                    Label lblSafetyStock = (Label)row.FindControl("lblSafetyStock");

                    InventoryInfo dInfo = new InventoryInfo();
                    InventoryDetailInfoNew dtInfo = new InventoryDetailInfoNew();
                    List<InventoryDetailInfoNew> ldInfo = new List<InventoryDetailInfoNew>();

                    dInfo.ProductCode = hidProductCodeIns.Value;

                    ldInfo = GetInventorydetail(dInfo);
                    int? sum = 0;
                    int? sum1 = 0;
                    int? sum2 = 0;

                    if (txtAmountIns.Text != "" && txtAmountIns.Text != "0")
                    {
                        if (Convert.ToInt32(txtAmountIns.Text) > 0)
                        {
                            if (ldInfo.Count > 0) // Duplicate in InventoryDetail
                            {
                                dtInfo = new InventoryDetailInfoNew();

                                dtInfo.QTY = ldInfo[0].QTY + Convert.ToInt32(txtAmountIns.Text);
                                dtInfo.Current = dtInfo.QTY - ldInfo[0].Reserved;
                                dtInfo.Balance = ldInfo[0].Balance + Convert.ToInt32(txtAmountIns.Text);
                                dtInfo.PickPack = ldInfo[0].Balance + Convert.ToInt32(txtAmountIns.Text);


                                string respstr = "";

                                APIpath = APIUrl + "/api/support/UpdateInventoryDetail";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["InventoryDetailId"] = ldInfo[0].InventoryDetailId.ToString();
                                    data["ProductCode"] = hidProductCodeIns.Value;
                                    data["QTY"] = dtInfo.QTY.ToString();
                                    data["Reserved"] = ldInfo[0].Reserved.ToString();
                                    data["Current"] = dtInfo.Current.ToString();
                                    data["Balance"] = dtInfo.Balance.ToString();
                                    data["SafetyStock"] = (dtInfo.SafetyStock.ToString() == "") ? "1" : dtInfo.SafetyStock.ToString();
                                    data["UpdateBy"] = empInfo.EmpCode;
                                    data["SupplierCode"] = hidSupplierCode.Value;
                                    data["POCode"] = txtPurchaseOrder.Text;


                                    var response = wb.UploadValues(APIpath, "POST", data);
                                    respstr = Encoding.UTF8.GetString(response);

                                    sum = JsonConvert.DeserializeObject<int?>(respstr);
                                }

                                if (sum > 0)
                                {

                                }
                            }
                            else // Insert into InventoryDetail
                            {
                                string respstr = "";

                                APIpath = APIUrl + "/api/support/InsertInventoryDetail";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    //data["InventoryCode"] = Request.QueryString["InventoryCode"];
                                    data["InventoryCode"] = txtLotNumber.Text;
                                    data["ProductCode"] = hidProductCodeIns.Value;
                                    data["QTY"] = txtAmountIns.Text;
                                    data["Reserved"] = "0";
                                    data["SafetyStock"] = "1";
                                    data["Current"] = txtAmountIns.Text;
                                    data["Balance"] = txtAmountIns.Text;
                                    data["CreateBy"] = empInfo.EmpCode;
                                    data["UpdateBy"] = empInfo.EmpCode;
                                    data["FlagDelete"] = "N";
                                    data["SupplierCode"] = hidSupplierCode.Value;
                                    data["POCode"] = txtPurchaseOrder.Text;


                                    var response = wb.UploadValues(APIpath, "POST", data);
                                    respstr = Encoding.UTF8.GetString(response);

                                    sum1 = JsonConvert.DeserializeObject<int?>(respstr);
                                }
                            }

                            if (sum > 0 || sum1 > 0)
                            {
                                LoadInventoryDetail();

                                InventoryInfo iinfo = new InventoryInfo();
                                List<InventoryDetailInfoNew> linfo = new List<InventoryDetailInfoNew>();
                                InventoryMovementInfo imvInfo = new InventoryMovementInfo();

                                iinfo.ProductCode = hidProductCodeIns.Value;
                                iinfo.InventoryCode = Request.QueryString["InventoryCode"];

                                linfo = GetInventorydetail(iinfo);

                                int productamount = (txtAmountIns.Text != "") ? Convert.ToInt32(txtAmountIns.Text) : 0;

                                string currentYear = DateTime.Now.Year.ToString();
                                string currentMonth = DateTime.Now.ToString("MM");
                                string currentDate = DateTime.Now.Date.ToString("dd");

                                string c1 = "";
                                string c2 = "";
                                string c3 = "";
                                string c4 = "";

                                for (int i = 0; i <= currentYear.Length - 1; i++)
                                {
                                    if (i == 0)
                                    {
                                        c1 = currentYear[i].ToString();
                                    }
                                    if (i == 1)
                                    {
                                        c2 = currentYear[i].ToString();
                                    }
                                    if (i == 2)
                                    {
                                        c3 = currentYear[i].ToString();
                                    }
                                    if (i == 3)
                                    {
                                        c4 = currentYear[i].ToString();
                                    }
                                }

                                currentYear = c3 + c4;

                                int? maxmanualseq = GetMaxSeqManual(linfo[0].InventoryDetailId, iinfo.ProductCode);
                                string maxmanualseqcode = "MID" + currentYear + currentMonth + currentDate + String.Format("{0:0000}", maxmanualseq);

                                //Looping InventoryMovement Insert

                                for (int i = 1; i <= productamount; i++)
                                {
                                    int? maxseq = 0;
                                    String maxinventorymovementseq = "";

                                    if (linfo.Count > 0)
                                    {
                                        imvInfo.InventoryDetailId = linfo[0].InventoryDetailId;
                                        imvInfo.ProductCode = iinfo.ProductCode;

                                        maxseq = GetMaxSeqInvMovement(imvInfo); //Movement each product or call Lot

                                        maxinventorymovementseq = iinfo.InventoryCode + imvInfo.ProductCode + currentYear + currentMonth + String.Format("{0:0000}", maxseq);
                                    }

                                    InventoryMovementInfo invInfo = new InventoryMovementInfo();

                                    invInfo.InventoryDetailId = imvInfo.InventoryDetailId;
                                    invInfo.ProductCode = hidProductCodeIns.Value;
                                    invInfo.SeqId = maxseq;
                                    invInfo.SeqManId = maxmanualseq;
                                    invInfo.InventoryMovementCode = maxinventorymovementseq;
                                    invInfo.InventoryManualLotCode = maxmanualseqcode;
                                    invInfo.CreateBy = empInfo.EmpCode;
                                    invInfo.ActiveFlag = "Y";

                                    sum2 = InsertInventoryMovement(invInfo);
                                }

                                //End Looping InventoryMovement Insert
                                //above code
                            }

                            if (sum > 0 || sum1 > 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal();", true);
                                LoadInventoryDetail();
                                LoadProduct();
                            }
                        }
                        else if (Convert.ToInt32(txtAmountIns.Text) <= 0)
                        {
                            //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('จำนวนสินค้าต้องมากกว่า 0');", true);
                        }
                    }
                    else if (txtAmountIns.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาระบุจำนวนสินค้า');", true);
                    }
             }
           
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }

        protected List<SupplierInfo> ListAutoCompletePO(string POCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOMapSupplier";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = POCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lSupplier = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);

            return lSupplier;
        }

        protected void txtPOChanged(object sender, EventArgs e)
        {
            string poInput = txtPurchaseOrder.Text;
            List<SupplierInfo> matchingSuggestions = ListAutoCompletePO(poInput)
                .Where(item => String.Compare(item.POCode, poInput, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            ListBoxPurchaseOrder.Items.Clear();
            foreach (SupplierInfo suggestion in matchingSuggestions)
            {
                ListBoxPurchaseOrder.Items.Add(suggestion.POCode);
            }

            ListBoxPurchaseOrder.Visible = matchingSuggestions.Count > 0;
        }

        protected void ListBoxPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListBoxPurchaseOrder.SelectedIndex >= 0)
            {
                
                txtPurchaseOrder.Text = ListBoxPurchaseOrder.SelectedItem.Text;
                ListBoxPurchaseOrder.Visible = false;
                List<SupplierInfo> supplier = ListAutoCompletePO(txtPurchaseOrder.Text);

                if (supplier.Count > 0)
                {
                    txtSupplier.Text = supplier[0].SupplierName;
                }
                
            }
        }
        protected void btnPopMap_Click(object sender, EventArgs e)
        {
       
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address').modal();", true);
        }
        protected void btnPopupMapSave_Click(object sender, EventArgs e)
        {
            string respstr = "";
            int? sum;
            APIpath = APIUrl + "/api/support/UpdateInventoryPolygon";
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = hidInvenIDPopupMap.Value;
                data["Polygon"] = txtareMap.Text;
                data["UpdateBy"] = empInfo.EmpCode;


                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);

                sum = JsonConvert.DeserializeObject<int?>(respstr);

                if (sum > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address').modal();", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-address').modal();", true);
                }
            }

        }
        protected void btnMapReset_Click(object sender, EventArgs e)
        {
            txtareMap.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-show-Map').modal();", true);
        }
        #endregion Events
    }
}
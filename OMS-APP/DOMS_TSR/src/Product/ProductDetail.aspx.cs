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
using System.Xml;
using System.Net.Security;

namespace DOMS_TSR.src.Product
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string APIMKP = ConfigurationManager.AppSettings["APIMKP"];
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int currentPdPageNumber;
        protected static int currentProductBOMPageNumber;
        protected static int currentPdAddBOMPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPdPageNumber = 1;
                currentProductBOMPageNumber = 1;
                currentPdAddBOMPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                LoadPromotionComplementarySection();
                LoadProductBOMSection();
                
                LoadProduct();
                LoadProductImages();
                LoadComplementaryProduct();
                LoadProductMasterAddComprementary();
                LoadProductBOM();
                LoadProductMasterAddProductBOM();
                BindddlProductInventory_Search();
            }

        }


        #region Function
 
            protected void LoadPromotionComplementarySection()
        {
            List<PromotionDetailInfo> lpromotiondetailInfo = new List<PromotionDetailInfo>();

            lpromotiondetailInfo = CheckPromotionComplementarybyCriteria();

            if (lpromotiondetailInfo.Count > 0)
            {
                foreach(var lp in lpromotiondetailInfo)
                {
                    if (lp.ComplementaryFlag == "Y")
                    {
                        
                        complementarysection.Visible = true;
                    }
                    else
                    {
                        complementarysection.Visible = false;
                    }
                }
            }
        }
        protected void LoadProductBOMSection()
        {
            string ProductBOMFlag = Request.QueryString["ProductDetailBOM"];

            if (ProductBOMFlag == "Y")
            {
                ProdutBOMSection.Visible = true;
            }
            else
            {
                ProdutBOMSection.Visible = false;
            }
        }
        protected string LoadPathXML()
        {
            string FolderXML = Server.MapPath("~/XMLFile/");
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(FolderXML))
                {
                    Console.WriteLine("That path exists already.");
                    return FolderXML;
                }
                else
                {
                    // Try to create the directory.
                    DirectoryInfo di = Directory.CreateDirectory(FolderXML);
                    Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(FolderXML));
                    return FolderXML;
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
                return FolderXML;
            }

            
        }
        protected void LoadProduct()
        {
          

            sectionLazadaCreated.Visible = false;
            sectionLazadaNotCreate.Visible = false;


            List<ProductInfo> lProductInfo = new List<ProductInfo>();
  
            lProductInfo = GetProductMasterByCriteria();

            hidProductId.Value = lProductInfo[0].ProductId.ToString();

            txtProductCode.Text = lProductInfo[0].ProductCode;

            txtProductSku.Text = lProductInfo[0].Sku;

            txtProductName.Text = lProductInfo[0].ProductName;

            txtProductBrand.Text = lProductInfo[0].ProductBrandName;

            txtMerchantName.Text = lProductInfo[0].MerchantName;

            txtProductCategoryName.Text = lProductInfo[0].ProductCategoryName;

            hidLazadaCatagoryCode.Value = lProductInfo[0].LazadaCategoryCode;

            txtProductWidth.Text = lProductInfo[0].ProductWidth.ToString();
            txtProductLength.Text = lProductInfo[0].ProductLength.ToString();
            txtProductHeight.Text = lProductInfo[0].ProductHeigth.ToString();

            txtPackageWidth.Text = lProductInfo[0].PackageWidth.ToString();
            txtPackageLength.Text = lProductInfo[0].PackageLength.ToString();
            txtPackageHeight.Text = lProductInfo[0].PackageHeigth.ToString();
            txtProductWeight.Text = lProductInfo[0].Weight.ToString();

            txtPrice.Text = String.Format("{0:0.00}", lProductInfo[0].Price);

            txtUnit.Text = lProductInfo[0].UnitName;

            txtProductWeight.Text = lProductInfo[0].Weight.ToString();

            txtLogisticType.Text = lProductInfo[0].TransportationTypeName.ToString();

            txtDescription.InnerText = lProductInfo[0].Description;

            txtUpsellScript.InnerText = lProductInfo[0].UpsellScript;


            List<ProductMapRecipeInfo> ListPR = ListProductMapRecipe(lProductInfo[0].ProductCode);

            foreach(ProductMapRecipeInfo PR in ListPR)
            {
                if(txtProductRecipes.Text == "")
                {
                    txtProductRecipes.Text += PR.RecipeName;
                }
                else
                {
                    txtProductRecipes.Text += ", "+ PR.RecipeName;
                }
                
            }
           
            sectionLazadaCreated.Visible = false;
            sectionLazadaNotCreate.Visible = false;

            if (lProductInfo[0].Lazada_status == 1)
            {
                sectionLazadaCreated.Visible = true;
                txtLazada_ItemId.Text = lProductInfo[0].Lazada_ItemId;
                txtLazada_skuId.Text = lProductInfo[0].Lazada_skuId;
            }
            else
            {
                sectionLazadaNotCreate.Visible = true;

            }
        }
        protected void btnAddLazProduct_Click(object sender, EventArgs e)
        {
            int? sum = 0;
            string path = LoadPathXML();
            using (XmlWriter writer = XmlWriter.Create(@path+hidEmpCode.Value+"books.xml"))
            {
                writer.WriteStartElement("Request");
                writer.WriteStartElement("Product");
                writer.WriteElementString("PrimaryCategory", hidLazadaCatagoryCode.Value);

                writer.WriteStartElement("Images");
                
                writer.WriteElementString("Image", "https://i8.amplience.net/i/jpl/jd_907882-02_a");
                writer.WriteEndElement();

                writer.WriteStartElement("Attributes");
                writer.WriteElementString("name", txtProductName.Text); 
                writer.WriteElementString("short_description", txtDescription.InnerText); 
                writer.WriteElementString("brand", txtProductBrand.Text);  
                
                writer.WriteElementString("delivery_option_sof", "No");
                writer.WriteEndElement();

                writer.WriteStartElement("Skus");
                writer.WriteStartElement("Sku");
                writer.WriteElementString("SellerSku", txtProductSku.Text);
                writer.WriteElementString("color_family", "Green");
                writer.WriteElementString("size", "40");
                writer.WriteElementString("quantity", "10");
                writer.WriteElementString("price", txtPrice.Text);
                writer.WriteElementString("package_length", txtPackageLength.Text); 
                writer.WriteElementString("package_height", txtPackageHeight.Text); 
                writer.WriteElementString("package_weight", txtProductWeight.Text);
                writer.WriteElementString("package_width", txtPackageWidth.Text);
                
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            }
            XmlDocument doc = new XmlDocument();
            doc.Load(@path + hidEmpCode.Value + "books.xml");

           
            
                List<ProductInfo> lProductInfo = new List<ProductInfo>();
                lProductInfo = AddProductToLazada(doc.InnerXml);
                if (lProductInfo.Count() <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('เกิดข้อผิดพลาดไม่สามารถสร้างสินค้าใน Lazada ได้');", true);
                }
                else
                {
                    sum = UpdateProduct(lProductInfo);
                    if (sum > 0)
                    {
                      LoadProduct();
                      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }
                }
                
            
            
        }
        public int? UpdateProduct(List<ProductInfo> lProductInfo)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateLazadaProduct";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ProductId"] = hidProductId.Value;
                data["Lazada_ItemId"] = lProductInfo[0].Lazada_ItemId;
                data["Lazada_skuId"] = lProductInfo[0].Lazada_skuId;
                data["Lazada_status"] = StaticField.LazadaProduct_Lazada_status_1; 
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? sum = JsonConvert.DeserializeObject<int?>(respstr);
            return sum;
        }
        public List<ProductInfo> AddProductToLazada(string xml)
        {
            string respstring = "";

            
            APIMKP = APIUrl;
            string APIpath = APIMKP + "/LazCreateProduct";
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback
            (
               delegate { return true; }
            );
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["url"] = StaticField.LazadaProduct_url; 
                data["appkey"] = StaticField.LazadaProduct_appkey; 
                data["appSecret"] = StaticField.LazadaProduct_appSecret; 
                data["AccessToken"] = StaticField.LazadaProduct_AccessToken; 
                data["XML"] = xml;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstring = Encoding.UTF8.GetString(response);
            }
            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstring);
            return lProductInfo;
        }
        protected void LoadProductImages()
        {
            string Url = "";
            Url = "http://doublep-cloud.servehttp.com:2719";
            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            lProductInfo = GetProductImage();

            ProductImg.Src = lProductInfo.Count > 0 ? Url + lProductInfo[0].ProductImageUrl : "";
            hidimage1.Value = lProductInfo.Count > 0 ? Url + lProductInfo[0].ProductImageUrl : "";

        }
       
        protected void LoadComplementaryProduct()
        {
            List<ComplementaryInfo> lcomInfo = new List<ComplementaryInfo>();
            int? totalRow = CountComplementarybyPromotionDetailInfoIdList();
            SetPageBar(Convert.ToDouble(totalRow));
            lcomInfo = GetComplementaryProductByPromotionDetailInfoId();

            // For Complementary type show Price 
            foreach (var lc in lcomInfo)
            {
                lc.Price = 0;
            }

            gvProductComplementary.DataSource = lcomInfo;
            gvProductComplementary.DataBind();
        }
 
            public List<ProductInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = Request.QueryString["ProductCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }


        public List<ProductInfo> GetProductImage()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetProductImageUrl";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = Request.QueryString["ProductCode"];

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }

        public List<PromotionDetailInfo> CheckPromotionComplementarybyCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CheckPromotionComplementaryTypebyPromotionDetailInfoID";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionDetailInfoId"] = (Request.QueryString["PromotionDetailInfoID"] != "") ? Request.QueryString["PromotionDetailInfoID"] : "0";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotiondetailInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);


            return lPromotiondetailInfo;
        }

        public int? CountComplementarybyPromotionDetailInfoIdList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountComplementaryListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionDetailInfoId"] = (Request.QueryString["PromotionDetailInfoID"] != "") ? Request.QueryString["PromotionDetailInfoID"] : "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        public List<ComplementaryInfo> GetComplementaryProductByPromotionDetailInfoId()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListComplementaryByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionDetailInfoId"] = (Request.QueryString["PromotionDetailInfoID"] != "") ? Request.QueryString["PromotionDetailInfoID"] : "";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ComplementaryInfo> lComplementaryInfo = JsonConvert.DeserializeObject<List<ComplementaryInfo>>(respstr);

            return lComplementaryInfo;
        }

        protected void SetPageBar(double totalRow)
        {

            lblTotalPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPages.Text) + 1; i++)
            {
                ddlPage.Items.Add(new ListItem(i.ToString()));
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

        protected void LoadProductMasterAddComprementary()
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();
            int? totalRow = CountProductMasterAddComplementaryMasterList();
            SetPageBar_Product(Convert.ToDouble(totalRow));
            lProductInfo = GetProductMasterAddComplementaryMasterList();

            gvProduct.DataSource = lProductInfo;
            gvProduct.DataBind();
        }

        public List<ProductInfo> GetProductMasterAddComplementaryMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                
                data["ProductCodeNotInComplementary"] = "ProductCodeNotInComplementary";                
                data["PromotionDetailInfoId"] = (Request.QueryString["PromotionDetailInfoID"] != "") ? Request.QueryString["PromotionDetailInfoID"] : "0";
                data["ProductCode"] = txtSearchModalProductCode.Text;
                data["ProductName"] = txtSearchModalProductName.Text;
                data["MerchantCode"] = hidMerchantCode.Value;
                data["rowOFFSet"] = ((currentPdPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }
        public int? CountProductMasterAddComplementaryMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductMasterListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCodeNotInComplementary"] = "ProductCodeNotInComplementary";
                data["PromotionDetailInfoId"] = (Request.QueryString["PromotionDetailInfoID"] != "") ? Request.QueryString["PromotionDetailInfoID"] : "0";
                data["ProductCode"] = txtSearchModalProductCode.Text;
                data["ProductName"] = txtSearchModalProductName.Text;
                data["MerchantCode"] = hidMerchantCode.Value;
                data["rowOFFSet"] = ((currentPdPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected void SetPageBar_Product(double totalRow)
        {

            lblTotalPdPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPdPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPdPages.Text) + 1; i++)
            {
                ddlPdPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Product(ddlPdPage, currentPdPageNumber.ToString());
            

            
            if ((currentPdPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                btnPdFirst.Enabled = false;
                btnPdPre.Enabled = false;
                btnPdNext.Enabled = true;
                btnPdLast.Enabled = true;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalPdPages.Text) && (currentPdPageNumber == 1))
            {
                btnPdFirst.Enabled = false;
                btnPdPre.Enabled = false;
                btnPdNext.Enabled = false;
                btnPdLast.Enabled = false;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalPdPages.Text) && (currentPdPageNumber > 1))
            {
                btnPdFirst.Enabled = true;
                btnPdPre.Enabled = true;
                btnPdNext.Enabled = false;
                btnPdLast.Enabled = false;
            }
            else
            {
                btnPdFirst.Enabled = true;
                btnPdPre.Enabled = true;
                btnPdNext.Enabled = true;
                btnPdLast.Enabled = true;
            }
            
        }

        private void setDDl_Product(DropDownList ddls, String val)
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

        protected Boolean DeleteComplementaryProduct()
        {
            for (int i = 0; i < gvProductComplementary.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvProductComplementary.Rows[i].FindControl("chkProductComplementary");

                if (checkbox.Checked == true)
                {
                    HiddenField hidComplementaryId = (HiddenField)gvProductComplementary.Rows[i].FindControl("hidComplementaryId");
                    if (Codelist != "")
                    {                        
                        Codelist += "," + hidComplementaryId.Value + "";
                    }
                    else
                    {
                        Codelist += "" + hidComplementaryId.Value + "";
                    }

                }
            }

            if (Codelist != "")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteComplementary";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ComplementaryId_List"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;
        }

        protected void LoadProductBOM()
        {
            List<ProductBOMInfo> lProductBOMInfo = new List<ProductBOMInfo>();
            int? totalRow = CountProductBOMList();
            SetPageBar_ProductBOM(Convert.ToDouble(totalRow));
            lProductBOMInfo = GetProductBOMPagingList();

            gvProductBOM.DataSource = lProductBOMInfo;
            gvProductBOM.DataBind();
        }
        public int? CountProductBOMList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListProductBOMByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = Request.QueryString["ProductCode"];
                data["ProductBOM"] = txtProductCodeBOM_Search.Text.Trim();
                data["ProductBOMName"] = txtProductNameBOM_Search.Text.Trim();
                data["InventoryCode"] = ddlProductInventory_Search.SelectedValue;
                data["rowOFFSet"] = ((currentProductBOMPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }
        public List<ProductBOMInfo> GetProductBOMPagingList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBOMByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = Request.QueryString["ProductCode"]; ;
                data["ProductBOM"] = txtProductCodeBOM_Search.Text;
                data["ProductBOMName"] = txtProductNameBOM_Search.Text;
                data["InventoryCode"] = ddlProductInventory_Search.SelectedValue;
                data["rowOFFSet"] = ((currentProductBOMPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBOMInfo> lProductBOMInfo = JsonConvert.DeserializeObject<List<ProductBOMInfo>>(respstr);

            return lProductBOMInfo;
        }

        protected void SetPageBar_ProductBOM(double totalRow)
        {

            lblTotalProductBOMPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlProductBOMPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalProductBOMPages.Text) + 1; i++)
            {
                ddlProductBOMPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Product(ddlProductBOMPage, currentProductBOMPageNumber.ToString());
            

            
            if ((currentProductBOMPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                lnkbtnProductBOMFirst.Enabled = false;
                lnkbtnProductBOMPre.Enabled = false;
                lnkbtnProductBOMNext.Enabled = true;
                lnkbtnProductBOMLast.Enabled = true;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalProductBOMPages.Text) && (currentProductBOMPageNumber == 1))
            {
                lnkbtnProductBOMFirst.Enabled = false;
                lnkbtnProductBOMPre.Enabled = false;
                lnkbtnProductBOMNext.Enabled = false;
                lnkbtnProductBOMLast.Enabled = false;
            }
            else if ((currentPdPageNumber.ToString() == lblTotalProductBOMPages.Text) && (currentProductBOMPageNumber > 1))
            {
                lnkbtnProductBOMFirst.Enabled = true;
                lnkbtnProductBOMPre.Enabled = true;
                lnkbtnProductBOMNext.Enabled = false;
                lnkbtnProductBOMLast.Enabled = false;
            }
            else
            {
                lnkbtnProductBOMFirst.Enabled = true;
                lnkbtnProductBOMPre.Enabled = true;
                lnkbtnProductBOMNext.Enabled = true;
                lnkbtnProductBOMLast.Enabled = true;
            }
            
        }

        protected void LoadProductMasterAddProductBOM()
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();
            int? totalRow = CountProductMasterAddProductBOMMasterList();
            SetPageBar_AddProductBOM(Convert.ToDouble(totalRow));
            lProductInfo = GetProductMasterAddProductBOMMasterList();

            gvProductForAddBOM.DataSource = lProductInfo;
            gvProductForAddBOM.DataBind();
        }

        public int? CountProductMasterAddProductBOMMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductMasterListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCodeNotInProductBOM"] = Request.QueryString["ProductCode"];
                data["ProductCode"] = txtProductCode_InsBOMSearch.Text.Trim();
                data["ProductName"] = txtProductName_InsBOMSearch.Text.Trim();
                data["MerchantCode"] = hidMerchantCode.Value;
                data["rowOFFSet"] = ((currentPdAddBOMPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        public List<ProductInfo> GetProductMasterAddProductBOMMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCodeNotInProductBOM"] = Request.QueryString["ProductCode"];
                data["ProductCode"] = txtProductCode_InsBOMSearch.Text.Trim();
                data["ProductName"] = txtProductName_InsBOMSearch.Text.Trim();
                data["MerchantCode"] = hidMerchantCode.Value;
                data["rowOFFSet"] = ((currentPdAddBOMPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }

        protected void SetPageBar_AddProductBOM(double totalRow)
        {

            lblTotalPdBOMPages.Text = Math.Ceiling(totalRow / PAGE_SIZE).ToString(); 

            
            ddlPdBOMPage.Items.Clear();
            for (int i = 1; i < Convert.ToInt32(lblTotalPdBOMPages.Text) + 1; i++)
            {
                ddlPdBOMPage.Items.Add(new ListItem(i.ToString()));
            }
            setDDl_Product(ddlPdBOMPage, currentPdAddBOMPageNumber.ToString());
            

            
            if ((currentPdAddBOMPageNumber == 1) && (Math.Ceiling(totalRow / PAGE_SIZE)) > 1)
            {
                btnPdBOMFirst.Enabled = false;
                btnPdBOMPre.Enabled = false;
                btnPdBOMNext.Enabled = true;
                btnPdBOMLast.Enabled = true;
            }
            else if ((currentPdAddBOMPageNumber.ToString() == lblTotalPdBOMPages.Text) && (currentPdAddBOMPageNumber == 1))
            {
                btnPdBOMFirst.Enabled = false;
                btnPdBOMPre.Enabled = false;
                btnPdBOMNext.Enabled = false;
                btnPdBOMLast.Enabled = false;
            }
            else if ((currentPdAddBOMPageNumber.ToString() == lblTotalPdBOMPages.Text) && (currentPdAddBOMPageNumber > 1))
            {
                btnPdBOMFirst.Enabled = true;
                btnPdBOMPre.Enabled = true;
                btnPdBOMNext.Enabled = false;
                btnPdBOMLast.Enabled = false;
            }
            else
            {
                btnPdBOMFirst.Enabled = true;
                btnPdBOMPre.Enabled = true;
                btnPdBOMNext.Enabled = true;
                btnPdBOMLast.Enabled = true;
            }
            
        }

        protected Boolean DeleteProductBOM()
        {
            for (int i = 0; i < gvProductBOM.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvProductBOM.Rows[i].FindControl("chkProductBOM");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProductBOM.Rows[i].FindControl("hidProductBOMId");
                    HiddenField hidProductBOM = (HiddenField)gvProductBOM.Rows[i].FindControl("hidProductBOM");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }
                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteProductBOM";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ProductBOM"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;
        }

        protected void BindddlProductInventory_Search()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = hidMerchantCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            ddlProductInventory_Search.DataSource = lInventoryInfo;
            ddlProductInventory_Search.DataValueField = "InventoryCode";
            ddlProductInventory_Search.DataTextField = "InventoryName";
            ddlProductInventory_Search.DataBind();
            ddlProductInventory_Search.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        #endregion

        #region Event 
        protected void btnAddProductComplementary_Click(object sender, EventArgs e)
        {
            
            hidFlagInsertComplementary.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Product').modal();", true);
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

            LoadComplementaryProduct();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            LoadComplementaryProduct();
        }

        protected void chkProductComplementaryAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvProductComplementary.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvProductComplementary.HeaderRow.FindControl("chkProductComplementaryAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProductComplementary.Rows[i].FindControl("hidComplementaryId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProductComplementary = (CheckBox)gvProductComplementary.Rows[i].FindControl("chkProductComplementary");

                    chkProductComplementary.Checked = true;
                }
                else
                {

                    CheckBox chkProductComplementary = (CheckBox)gvProductComplementary.Rows[i].FindControl("chkProductComplementary");

                    chkProductComplementary.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;

            LoadComplementaryProduct();            
        }
        
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Promotion/PromotionComplementaryManagement.aspx");
        }

        protected void btnModalSearch_Click(object sender, EventArgs e)
        {
            LoadProductMasterAddComprementary();
        }

        protected void btnModalClear_Click(object sender, EventArgs e)
        {
            txtSearchModalProductCode.Text = "";
            txtSearchModalProductName.Text = "";
        }

        protected void gvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtqty = (TextBox)e.Row.FindControl("txtQty_Ins");

                txtqty.Text = "1";
            }
        }

        protected void gvProduct_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProduct.Rows[index];
            int? cou = 0;

            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            TextBox txtQty_Ins = (TextBox)row.FindControl("txtQty_Ins");

            if (e.CommandName == "AddProductComplementary")
            {
                if(hidFlagInsertComplementary.Value == "True")
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertComplementary";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["ProductCode"] = hidProductCode.Value;
                        data["Amount"] = txtQty_Ins.Text;                        
                        data["PromotionDetailInfoId"] = (Request.QueryString["PromotionDetailInfoID"] != "") ? Request.QueryString["PromotionDetailInfoID"] : "0";
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagDelete"] = "N";                        

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    cou = JsonConvert.DeserializeObject<int?>(respstr);

                    if (cou > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "เพิ่มรายการสำเร็จ" + "');$('#modal-Product').modal('hide');", true);
                        LoadComplementaryProduct();
                        LoadProductMasterAddComprementary();

                    }
                }
            }
        }

        protected void GetProductPageIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "First":
                    currentPdPageNumber = 1;
                    break;

                case "Previous":
                    currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPdPageNumber = Int32.Parse(lblTotalPdPages.Text);
                    break;
            }


            LoadProductMasterAddComprementary();
        }

        protected void GetProductBOMPageIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "First":
                    currentProductBOMPageNumber = 1;
                    break;

                case "Previous":
                    currentProductBOMPageNumber = Int32.Parse(ddlProductBOMPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentProductBOMPageNumber = Int32.Parse(ddlProductBOMPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentProductBOMPageNumber = Int32.Parse(ddlProductBOMPage.Text);
                    break;
            }

            LoadProductBOM();
        }

        protected void ddlProductPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue);

            LoadProductMasterAddComprementary();
        }

        protected void chkProductBOMAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvProductBOM.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvProductBOM.HeaderRow.FindControl("chkProductBOMAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProductBOM.Rows[i].FindControl("hidProductBOMId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProductBOM = (CheckBox)gvProductBOM.Rows[i].FindControl("chkProductBOM");

                    chkProductBOM.Checked = true;
                }
                else
                {

                    CheckBox chkProductBOM = (CheckBox)gvProductBOM.Rows[i].FindControl("chkProductBOM");

                    chkProductBOM.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteComplementaryProduct();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btn_ProductBOMSearch_Click(object sender, EventArgs e)
        {
            currentProductBOMPageNumber = 1;

            LoadProductBOM();
        }

        protected void btn_ProductBOMClearSearch_Click(object sender, EventArgs e)
        {
            txtProductCodeBOM_Search.Text = "";
            txtProductNameBOM_Search.Text = "";
        }

        protected void btnAddProductBOM_Click(object sender, EventArgs e)
        {
            hidFlagInsertProductBOM.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-AddProductBOM').modal();", true);
        }

        protected void btnDeleteProductBOM_Click(object sender, EventArgs e)
        {
            isdelete = DeleteProductBOM();
            btn_ProductBOMSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void GetAddPdBOMPageIndex(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "First":
                    currentPdAddBOMPageNumber = 1;
                    break;

                case "Previous":
                    currentPdAddBOMPageNumber = Int32.Parse(ddlPdBOMPage.SelectedValue) - 1;
                    break;

                case "Next":
                    currentPdAddBOMPageNumber = Int32.Parse(ddlPdBOMPage.SelectedValue) + 1;
                    break;

                case "Last":
                    currentPdAddBOMPageNumber = Int32.Parse(lblTotalPdBOMPages.Text);
                    break;
            }


            LoadProductMasterAddProductBOM();
        }

        protected void ddlPdBOMPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPdAddBOMPageNumber = Int32.Parse(ddlPdBOMPage.SelectedValue);
            LoadProductMasterAddProductBOM();
        }
        
        protected void btnProductAddBOMSearch_Click(object sender, EventArgs e)
        {
            currentPdAddBOMPageNumber = 1;

            LoadProductMasterAddProductBOM();
        }

        protected void btnProductAddBOMClearSearch_Click(object sender, EventArgs e)
        {
            txtProductCode_InsBOMSearch.Text = "";
            txtProductName_InsBOMSearch.Text = "";
        }

        protected void gvProductForAddBOM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtQty_Ins = (TextBox)e.Row.FindControl("txtQty_Ins");

                txtQty_Ins.Text = "1";
            }
        }

        protected void gvProductForAddBOM_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProductForAddBOM.Rows[index];
            int? cou = 0;

            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            TextBox txtQty_Ins = (TextBox)row.FindControl("txtQty_Ins");
            HiddenField hidUnit = (HiddenField)row.FindControl("hidUnit");

            if (e.CommandName == "AddProductBOM")
            {
                if (hidFlagInsertProductBOM.Value == "True")
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertProductBOM";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["ProductCode"] = Request.QueryString["ProductCode"];
                        data["ProductBOM"] = hidProductCode.Value;
                        data["QTY"] = txtQty_Ins.Text;
                        data["Unit"] = hidUnit.Value;                     
                        data["CreateBy"] = hidEmpCode.Value;
                        data["UpdateBy"] = hidEmpCode.Value;
                        data["FlagDelete"] = "N";

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    cou = JsonConvert.DeserializeObject<int?>(respstr);

                    if (cou > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + "เพิ่มรายการสำเร็จ" + "');$('#modal-AddProductBOM').modal('hide');", true);
                        LoadProductBOM();
                        LoadProductMasterAddProductBOM();
                    }
                }
            }
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProductDetail.aspx?ProductCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected List<ProductMapRecipeInfo> ListProductMapRecipe(string pCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMapRecipeNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = pCode;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductMapRecipeInfo> lRecipeInfo = JsonConvert.DeserializeObject<List<ProductMapRecipeInfo>>(respstr);


            return lRecipeInfo;

        }
        #endregion


    }
}
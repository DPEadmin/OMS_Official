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

namespace DOMS_TSR.src.Product
{
    public partial class Product : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string ProductImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];

        string Codelist = "";
        string Showcase_img11Upload_Name = "";
        string Showcase_img43Upload_Name = "";
        string SKUimg1Upload_Name = "";
        string Productimg1_Name = "";
       

        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];
               

                if (empInfo != null && merchantinfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                if (hidMerchantCode.Value == StaticField.MerchantCode_TIB) 
                {
                    TIBSection.Visible = true;
                }
                else
                {
                    TIBSection.Visible = false;
                }

                if (empInfo.BuCode == "LAZADA")
                {
                    lProductCategoryLazada.Visible = true;
                    divProductCategoryLazada.Visible = true;
                }
                else
                {
                    lProductCategoryLazada.Visible = false;
                    divProductCategoryLazada.Visible = false;
                }

                LoadProduct();
                
                BindWarrantyCondition();
                BindddlWarrantyType();
                BindddlProductBrand();
                BindddlUnit();
                BindddlCarType();
                BindddlMaintainType();
                BindddlProductCategory();
                BindddlProductCategoryLazada();
                BindddlProductInventory_Search();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadProduct();
        }
        protected void btnExportProduct_Click(object sender, EventArgs e)
        {
            ExportProduct_NoAnswerOrder();
        }
        
        #region Function
        protected void ExportProduct_NoAnswerOrder()
        {
            var dataExcel = new NameValueCollection();
            List<ProductInfo> olist = new List<ProductInfo>();
            olist.Add(new ProductInfo
            {
               
                CreateDate = ""
            
            });


            Session["dataExportExcel"] = olist;
            string URL = "ReportProduct_Excel.aspx?pname="+txtSearchProductName.Text+"&pcode="+ txtSearchProductCode.Text + "&pbrand="+ddlProductBrand_Search.SelectedValue+ "&pinventory=" + ddlProductInventory_Search.SelectedValue + "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URL + "','_blank')", true);

        }
        protected void btnExportLazada_Click(object sender, EventArgs e)
        {
            if (ddlProductInventory_Search.SelectedValue == "-99")
            {
                lblProProductInventory_Search.Text = "กรุณาระบุคลังสินค้าก่อน Export Lazada";
            }
            else
            { 
            ExportLazada_NoAnswerOrder();
            }
        }
        protected void ExportLazada_NoAnswerOrder()
        {
            var dataExcel = new NameValueCollection();
            List<ProductInfo> olist = new List<ProductInfo>();
            olist.Add(new ProductInfo
            {

                CreateDate = ""

            });


            Session["dataExportExcel"] = olist;
            string URL = "ReportLazada_Excel.aspx?pname=" + txtSearchProductName.Text + "&pcode=" + txtSearchProductCode.Text + "&pbrand=" + ddlProductBrand_Search.SelectedValue + "&pinventory=" + ddlProductInventory_Search.SelectedValue + "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URL + "','_blank')", true);
        }

        public int? runningNo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductRunningNumber";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }

        protected void LoadProduct()
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            

            int? totalRow = CountProductMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            
            lProductInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lProductInfo;

            gvProduct.DataBind();


            

        }


        public List<ProductInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["ProductBrandCode"] = ddlProductBrand_Search.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }


        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/ProductCodeValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = txtProductCode_Ins.Text;
             
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            if (lProductInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        public string GetProductImgByCriteria(string ProductCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetProductImageUrl";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = ProductCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

    
            return lProductInfo.Count>0?lProductInfo[0].ProductImageId:"";

        }

        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountProductMasterListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["ProductBrandName"] = txtSearchProductBrandName.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int?  cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


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


            LoadProduct();
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

        protected Boolean DeleteProduct()
        {
            InventoryDetailInfoNew ind = new InventoryDetailInfoNew();
            List<InventoryDetailInfoNew> lind = new List<InventoryDetailInfoNew>(); 

            for (int i = 0; i < gvProduct.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvProduct.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProduct.Rows[i].FindControl("hidProductId");
                    HiddenField hidProductCode = (HiddenField)gvProduct.Rows[i].FindControl("hidProductCode");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    ind = new InventoryDetailInfoNew();

                    ind.ProductCode = hidProductCode.Value;
                    lind.Add(ind);
                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteProduct";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ProductCode"] = Codelist;


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

            if (lind.Count > 0)
            {
                List<InventoryInfo> linv = new List<InventoryInfo>();
                linv = GetInventoryMaster();

                if (linv.Count > 0)
                {
                    foreach (var od in linv.ToList())
                    {
                        foreach (var og in lind.ToList())
                        {
                            List<InventoryDetailInfo> lvd = new List<InventoryDetailInfo>();
                            lvd = GetInventoryDetailIDMaster(od.InventoryCode, og.ProductCode);

                            if (lvd.Count > 0)
                            {
                                string respstr = "";

                                APIpath = APIUrl + "/api/support/DeleteInventoryDetail";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["InventoryDetailId"] = lvd[0].InventoryDetailId.ToString();

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstr = Encoding.UTF8.GetString(response);
                                }

                                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                            }

                        }
                    }
                }
            }

            hidIdList.Value = "";
            return true;

        }

        protected void DeleteProductById(string pId)
        {

            if (pId != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteProduct";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ProductCode"] = pId;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);

                if(cou > 0)
                {

                }
            }


        }

        public List<InventoryInfo> GetInventoryMaster()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = "";
                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            return lInventoryInfo;
        }
        public List<InventoryDetailInfo> GetInventoryDetailIDMaster(string inventorycode, string productcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryDetailInfoNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = inventorycode;
                data["ProductCode"] = productcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryDetailInfo> lInventoryDetailInfo = JsonConvert.DeserializeObject<List<InventoryDetailInfo>>(respstr);

            return lInventoryDetailInfo;
        }
        protected void UpdateProductMapRecipe(string pCode)
        {

            if (pCode != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateClearProductMapRecipe";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ProductCode"] = pCode;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            }
        }

        protected int? InsertInventoryDetail(string productcode)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            List<InventoryInfo> linv = new List<InventoryInfo>();
            linv = GetInventoryMaster();

            int? sum = 0;
            string respstr = "";

            if (linv.Count > 0)
            {
                foreach (var od in linv.ToList())
                {
                    List<InventoryDetailInfo> lvd = new List<InventoryDetailInfo>();
                    lvd = GetInventoryDetailIDMaster(od.InventoryCode, productcode);

                    if (lvd.Count == 0)
                    {                        
                        APIpath = APIUrl + "/api/support/InsertInventoryDetail";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["InventoryCode"] = od.InventoryCode;
                            data["ProductCode"] = productcode;
                            data["QTY"] = "0";
                            data["Reserved"] = "0";
                            data["SafetyStock"] = "1";
                            data["Current"] = "0";
                            data["Balance"] = "0";
                            data["PickPack"] = "0";
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);                            
                        }

                        sum = JsonConvert.DeserializeObject<int?>(respstr);
                    }
                }
            }
           
            return sum;
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

        protected Boolean validateInsertUpdate()
        {
            Boolean flag = true;

            HttpPostedFile Productimg1 = Request.Files["Productimg1"];
            HttpPostedFile Showcase_img11Upload_Ins = Request.Files["Showcase_img11Upload_Ins"];
            HttpPostedFile Showcase_img43Upload_Ins = Request.Files["Showcase_img43Upload_Ins"];
            HttpPostedFile SKUimg1Upload_Ins = Request.Files["SKUimg1Upload_Ins"];


            
            if (Showcase_img11Upload_Ins != null && Showcase_img11Upload_Ins.ContentLength > 0)
            {
                
                flag = (flag == false) ? false : true;
                Showcase_img11Upload_Name = ProductImgUrl + Showcase_img11Upload_Ins.FileName;
            }
            if (Showcase_img43Upload_Ins != null && Showcase_img43Upload_Ins.ContentLength > 0)
            {

                flag = (flag == false) ? false : true;
                Showcase_img43Upload_Name = ProductImgUrl + Showcase_img43Upload_Ins.FileName;
            }
            if (SKUimg1Upload_Ins != null && SKUimg1Upload_Ins.ContentLength > 0)
            {
                flag = (flag == false) ? false : true;
                SKUimg1Upload_Name = ProductImgUrl + SKUimg1Upload_Ins.FileName;
            }
            if (txtProductName_Ins.Text == "")
            {
                flag = false;
                lblProductName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Product name";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProductName_Ins.Text = "";
            }

            if (txtPrice_Ins.Text == "")
            {
                flag = false;
                lblPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Price";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPrice_Ins.Text = "";
            }

            if (ddlUnit_Ins.SelectedValue == "-99" || ddlUnit_Ins.SelectedValue == "")
            {
                flag = false;
                lblUnit_Ins.Text = MessageConst._MSG_PLEASESELECT + " unit";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblUnit_Ins.Text = "";
            }

            if (ddlProductBrand_Ins.SelectedValue == "-99" || ddlProductBrand_Ins.SelectedValue == "")
            {
                flag = false;
                lblProductBrand_Ins.Text = MessageConst._MSG_PLEASESELECT + " brand";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProductBrand_Ins.Text = "";
            }

            if (ddlProductCategory_Ins.SelectedValue == "-99" || ddlProductCategory_Ins.SelectedValue == "")
            {
                flag = false;
                lblProductCategory.Text = MessageConst._MSG_PLEASESELECT + " Product type";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProductCategory.Text = "";
            }
            if (ddlEcomSpec_Ins.SelectedValue == "-99" || ddlEcomSpec_Ins.SelectedValue == "")
            {
                flag = false;
                lblEcomSpec_Ins.Text = MessageConst._MSG_PLEASESELECT + " Sale on commerce";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEcomSpec_Ins.Text = "";
            }

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
            }

            return flag;
        }

        protected void UpdatePromotionDetail(string productcode, string productprice)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdatePromoDetailInfoByProductCode";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = productcode;
                data["Price"] = productprice;
                data["FlagDelete"] = "N";
                data["UpdateBy"] = hidEmpCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
        }

            #endregion

            #region Event 

            protected void gvProduct_Change(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;

            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            lProductInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lProductInfo;

            gvProduct.DataBind();

        }

        protected void chkProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvProduct.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvProduct.HeaderRow.FindControl("chkProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvProduct.Rows[i].FindControl("hidProductId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProduct = (CheckBox)gvProduct.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = true;
                }
                else
                {

                    CheckBox chkProduct = (CheckBox)gvProduct.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }
    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadProduct();

            
        }
  


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteProduct();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

          
            POInfo pInfo = new POInfo();
            MerchantInfo merchantinfo = new MerchantInfo();

            merchantinfo = (MerchantInfo)Session["MerchantInfo"];
            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (validateInsertUpdate()) 
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {
                        //Insert Allergy
                        string AllergyList = "";
                        foreach (ListItem listItemm in ddlAllergy.Items)
                        {
                            if (listItemm.Selected)
                            {
                                if(AllergyList == "")
                                {
                                    AllergyList += listItemm.Value; 
                                }
                                else
                                {
                                    AllergyList += "  "+listItemm.Value;
                                }
                            }
                        }

                        string ProductCode = "P0000" + runningNo().ToString();
                        //Insert Recipe
                        foreach (ListItem listItem in ddlMultiSelect.Items)
                        {
                            if (listItem.Selected)
                            {
                                var RecipeVal = listItem.Value;
                                var txt = listItem.Text;

                                //Save Recipe
                                string respstring = "";

                                APIpath = APIUrl + "/api/support/InsertProductMapRecipe";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["ProductCode"] = ProductCode;
                                    data["RecipeCode"] = RecipeVal;
                                    data["CreateBy"] = empInfo.EmpCode;
                                    data["UpdateBy"] = empInfo.EmpCode;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }
                            }
                        }

                        //Insert Img
                        HttpFileCollection uploadFiles = Request.Files;
                        HttpFileCollection uploads = HttpContext.Current.Request.Files;

                        for (int i = 0; i < uploadFiles.Count; i++)
                        {
                            HttpPostedFile upload = uploadFiles[i];
                            string UploadFileConrtrolName = uploads.AllKeys[i];

                            if (UploadFileConrtrolName == "Productimg1")
                            {
                                HttpPostedFile postedFile = uploadFiles[i];
                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Convert to Base64
                                    Stream fs = postedFile.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                    //Save Images
                                    string respstring = "";

                                    APIpath = APIUrl + "/api/support/InsertProductImage";

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["ProductCode"] = ProductCode;
                                        data["ProductImageUrl"] = ProductImgUrl + postedFile.FileName;
                                        data["ProductImageName"] = postedFile.FileName;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                    string APIpath1 = APIUrl + "/api/support/Savepicfromjsonstring64";
                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["ProductCode"] = ProductCode;
                                        data["ProductImageUrl"] = ProductImgUrl + postedFile.FileName;
                                        data["ProductImageName"] = postedFile.FileName;
                                        data["ProductImageBase64"] = base64String;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath1, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                }
                            }

                        }

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertProduct";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductCode"] = ProductCode;
                            data["Sku"] = txtProductSku_Ins.Text;
                            data["ProductName"] = txtProductName_Ins.Text;
                            data["ProductBrandCode"] = ddlProductBrand_Ins.SelectedValue;
                            data["CarType"] = ddlCarType_Ins.SelectedValue;
                            data["MaintainType"] = ddlMaintainType_Ins.SelectedValue;
                            data["InsureCost"] = (txtInsureCost_Ins.Text != "") ? txtInsureCost_Ins.Text : "0";
                            data["FirstDamages"] = (txtFirstDamages_Ins.Text != "") ? txtFirstDamages_Ins.Text : "0";
                            data["GarageQuan"] = (txtGarageQuan_Ins.Text != "") ? txtGarageQuan_Ins.Text : "0";
                            
                            data["Price"] = (txtPrice_Ins.Text != "") ? txtPrice_Ins.Text : "0";
                            data["TransportPrice"] = "0";
                            data["FlagDelete"] = "N";
                            data["Unit"] = ddlUnit_Ins.SelectedValue;
                            data["Description"] = txtDescription_Ins.Text;
                            data["UpsellScript"] = txtUpsellScript_Ins.Text;
                            data["AllergyRemark"] = AllergyList;                            
                            data["ProductCategoryCode"] = ddlProductCategory_Ins.SelectedValue; 
                            data["LazadaCategoryCode"] = ddlProductCategoryLazada_Ins.SelectedValue;
                            data["MerchantCode"] = merchantinfo.MerchantCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;

                            data["Product_img1"] = Productimg1_Name;
                            data["Showcase_image11"] = Showcase_img11Upload_Name;
                            data["Showcase_image43"] = Showcase_img43Upload_Name;
                            data["SKU_img1"] = SKUimg1Upload_Name;
                            data["URLvideo"] = txtURLvideo_Ins.Text;
                            data["ProdutAdditional"] = txtAdditional_Ins.Text;
                            data["WarrantyCondition"] = ddlWarrantyCondition_Ins.SelectedValue;
                            data["WarrantyType"] = ddlWarrantyType_Ins.SelectedValue;
                            data["WarrantyStartdate"] = txtWarrantyStartDate_Ins.Text;
                            data["WarrantyEnddate"] = txtWarrantyStartDate_Ins.Text;
                            data["PackageWidth"] = txtPackageWidth_Ins.Text;
                            data["PackageHeigth"] = txtPackageHeight_Ins.Text;
                            data["PackageLength"] = txtPackageLength_Ins.Text;
                            data["Weight"] = txtPackageWeight_Ins.Text;
                            data["EcomSpec"] = ddlEcomSpec_Ins.SelectedValue;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        int? sum1 = 0;

                        if (sum > 0)
                        {
                            sum1 = InsertInventoryDetail(ProductCode);

                            if (sum1 > 0)
                            {
                                btnCancel_Click(null, null);

                                LoadProduct();

                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal('hide');", true);
                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {
                        //Insert Allergy
                        string AllergyList = "";
                        foreach (ListItem listItemm in ddlAllergy.Items)
                        {
                            if (listItemm.Selected)
                            {
                                if (AllergyList == "")
                                {
                                    AllergyList += listItemm.Value;
                                }
                                else
                                {
                                    AllergyList += "  " + listItemm.Value;
                                }
                            }
                        }

                        //Update Flag อันเก่าเป็น y insert ใหม่
                        UpdateProductMapRecipe(txtProductCode_Ins.Text);
                        UpdatePromotionDetail(txtProductCode_Ins.Text, txtPrice_Ins.Text);
                        //Insert Recipe
                        foreach (ListItem listItem in ddlMultiSelect.Items)
                        {
                            if (listItem.Selected)
                            {
                                var RecipeVal = listItem.Value;
                                var txt = listItem.Text;

                                //Save Recipe
                                string respstring = "";

                                APIpath = APIUrl + "/api/support/InsertProductMapRecipe";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["ProductCode"] = txtProductCode_Ins.Text;
                                    data["RecipeCode"] = RecipeVal;
                                    data["CreateBy"] = empInfo.EmpCode;
                                    data["UpdateBy"] = empInfo.EmpCode;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }
                            }
                        }

                        if (hidProductImgId.Value != "")
                        {
                            HttpFileCollection uploadFiles = Request.Files;

                            for (int i = 0; i < uploadFiles.Count; i++)
                            {
                                HttpPostedFile postedFile = uploadFiles[i];
                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Convert to Base64
                                    Stream fs = postedFile.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                    //Save Images
                                    string respstring = "";

                                    APIpath = APIUrl + "/api/support/UpdateProductImage";

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["ProductCode"] = txtProductCode_Ins.Text;
                                        data["ProductImageUrl"] = ProductImgUrl + postedFile.FileName;
                                        data["ProductImageName"] = postedFile.FileName;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                    string APIpath1 = APIUrl + "/api/support/Savepicfromjsonstring64";
                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["ProductCode"] = txtProductCode_Ins.Text;
                                        data["ProductImageUrl"] = ProductImgUrl + postedFile.FileName;
                                        data["ProductImageName"] = postedFile.FileName;
                                        data["ProductImageBase64"] = base64String;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath1, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                }
                            }
                        }
                        else
                        {

                            HttpFileCollection uploadFiles = Request.Files;

                            for (int i = 0; i < uploadFiles.Count; i++)
                            {
                                HttpPostedFile postedFile = uploadFiles[i];
                                if (postedFile != null && postedFile.ContentLength > 0)
                                {
                                    //Convert to Base64
                                    Stream fs = postedFile.InputStream;
                                    BinaryReader br = new BinaryReader(fs);
                                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                    //Save Images
                                    string respstring = "";

                                    APIpath = APIUrl + "/api/support/InsertProductImage";

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["ProductCode"] = txtProductCode_Ins.Text;
                                        data["ProductImageUrl"] = ProductImgUrl + postedFile.FileName;
                                        data["ProductImageName"] = postedFile.FileName;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                    string APIpath1 = APIUrl + "/api/support/Savepicfromjsonstring64";
                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["ProductCode"] = txtProductCode_Ins.Text;
                                        data["ProductImageUrl"] = ProductImgUrl + postedFile.FileName;
                                        data["ProductImageName"] = postedFile.FileName;
                                        data["ProductImageBase64"] = base64String;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath1, "POST", data);

                                        respstring = Encoding.UTF8.GetString(response);
                                    }

                                }
                            }

                        }

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateProduct";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductId"] = hidIdList.Value;
                            data["Sku"] = txtProductSku_Ins.Text;
                            data["ProductCode"] = txtProductCode_Ins.Text;
                            data["ProductName"] = txtProductName_Ins.Text;
                            data["ProductBrandCode"] = ddlProductBrand_Ins.SelectedValue;
                            data["CarType"] = ddlCarType_Ins.SelectedValue;
                            data["MaintainType"] = ddlMaintainType_Ins.SelectedValue;
                            data["InsureCost"] = (txtInsureCost_Ins.Text != "") ? txtInsureCost_Ins.Text : "0"; 
                            data["FirstDamages"] = (txtFirstDamages_Ins.Text != "") ? txtFirstDamages_Ins.Text : "0"; 
                            data["GarageQuan"] = (txtGarageQuan_Ins.Text != "") ? txtGarageQuan_Ins.Text : "0";
                            data["Price"] = (txtPrice_Ins.Text != "") ? txtPrice_Ins.Text : "0";
                            data["TransportPrice"] = "0";
                            data["FlagDelete"] = "N";
                            data["Unit"] = ddlUnit_Ins.SelectedValue;
                            data["Description"] = txtDescription_Ins.Text;
                            data["UpsellScript"] = txtUpsellScript_Ins.Text;
                            data["ProductCategoryCode"] = ddlProductCategory_Ins.SelectedValue;
                            data["LazadaCategoryCode"] = ddlProductCategoryLazada_Ins.SelectedValue;
                            data["Updateby"] = empInfo.EmpCode;
                            data["PackageWidth"] = txtPackageWidth_Ins.Text;
                            data["PackageHeigth"] = txtPackageHeight_Ins.Text;
                            data["PackageLength"] = txtPackageLength_Ins.Text;
                            data["Weight"] = txtPackageWeight_Ins.Text;
                            data["EcomSpec"] = ddlEcomSpec_Ins.SelectedValue;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadProduct();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-product').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }

                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtProductCode_Ins.Text = "";
            txtProductSku_Ins.Text = "";
            txtProductName_Ins.Text = "";
            txtPrice_Ins.Text = "";
            ddlProductBrand_Ins.ClearSelection();
            ddlCarType_Ins.ClearSelection();
            ddlMaintainType_Ins.ClearSelection();
            txtInsureCost_Ins.Text = "";
            txtFirstDamages_Ins.Text = "";
            txtGarageQuan_Ins.Text = "";
            ddlUnit_Ins.ClearSelection();
            txtDescription_Ins.Text = "";

            lblProductCode_Ins.Text = "";
            lblProductName_Ins.Text = "";
            lblUnit_Ins.Text = "";
            lblPrice_Ins.Text = "";
            lblDescription_Ins.Text = "";

            txtPackageHeight_Ins.Text = "";
            txtPackageLength_Ins.Text = "";
            txtPackageWeight_Ins.Text = "";
            txtPackageWidth_Ins.Text = "";
            txtURLvideo_Ins.Text = "";
            txtAdditional_Ins.Text = "";
            ddlWarrantyCondition_Ins.ClearSelection();
            ddlWarrantyType_Ins.ClearSelection();
            txtWarrantyStartDate_Ins.Text = "";
            txtWarrantyEndDate_Ins.Text = "";
            ddlPackageDanger_Ins.ClearSelection();

            ddlMultiSelect.ClearSelection();


            
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
            txtSearchProductBrandName.Text = "";
            ddlProductBrand_Search.ClearSelection();

        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProduct.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidProductId = (HiddenField)row.FindControl("hidProductId");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidProductSku = (HiddenField)row.FindControl("hidProductSku");
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");
            HiddenField hidProductPrice = (HiddenField)row.FindControl("hidProductPrice");

            HiddenField hidCarType = (HiddenField)row.FindControl("hidCarType");
            HiddenField hidMaintainType = (HiddenField)row.FindControl("hidMaintainType");
            HiddenField hidInsureCost = (HiddenField)row.FindControl("hidInsureCost");
            HiddenField hidFirstDamages = (HiddenField)row.FindControl("hidFirstDamages"); 
            HiddenField hidGarageQuan = (HiddenField)row.FindControl("hidGarageQuan");

            HiddenField hidUpsellScript = (HiddenField)row.FindControl("hidUpsellScript");
          

            HiddenField hidDescription = (HiddenField)row.FindControl("hidDescription");
            HiddenField hidUnit = (HiddenField)row.FindControl("hidUnit");
            HiddenField hidQuantity = (HiddenField)row.FindControl("hidQuantity");
            HiddenField hidMerchant = (HiddenField)row.FindControl("hidMerchant");
            HiddenField hidProductCategory = (HiddenField)row.FindControl("hidProductCategory");
            HiddenField hidProductBrand = (HiddenField)row.FindControl("hidProductBrand");
           
            HiddenField hidProductHeigth = (HiddenField)row.FindControl("hidProductHeigth");
            HiddenField hidProductLength = (HiddenField)row.FindControl("hidProductLength");
            HiddenField hidProductWidth = (HiddenField)row.FindControl("hidProductWidth");
            
            HiddenField hidTransportationType = (HiddenField)row.FindControl("hidTransportationType");

            HiddenField hidProduct_img1 = (HiddenField)row.FindControl("hidProduct_img1");
            HiddenField hidShowcase_image11 = (HiddenField)row.FindControl("hidShowcase_image11");
            HiddenField hidShowcase_image43 = (HiddenField)row.FindControl("hidShowcase_image43");
            HiddenField hidSKU_img1 = (HiddenField)row.FindControl("hidSKU_img1");
            HiddenField hidURLvideo = (HiddenField)row.FindControl("hidURLvideo");
            HiddenField hidProdutAdditional = (HiddenField)row.FindControl("hidProdutAdditional");
            HiddenField hidWarrantyCondition = (HiddenField)row.FindControl("hidWarrantyCondition");
            HiddenField hidWarrantyType = (HiddenField)row.FindControl("hidWarrantyType");
            HiddenField hidWarrantyStartdate = (HiddenField)row.FindControl("hidWarrantyStartdate");
            HiddenField hidWarrantyEnddate = (HiddenField)row.FindControl("hidWarrantyEnddate");
            HiddenField hidWeight = (HiddenField)row.FindControl("hidWeight");
            HiddenField hidPackageHeigth = (HiddenField)row.FindControl("hidPackageHeigth");
            HiddenField hidPackageLength = (HiddenField)row.FindControl("hidPackageLength");
            HiddenField hidPackageWidth = (HiddenField)row.FindControl("hidPackageWidth");
            HiddenField hidPackageDanger = (HiddenField)row.FindControl("hidPackageDanger");
            HiddenField hidEcomSpec = (HiddenField)row.FindControl("hidEcomSpec");
            HiddenField hidLazadaCategoryCode = (HiddenField)row.FindControl("hidLazadaCategoryCode");

            if (e.CommandName == "ShowProduct")
            {
                ddlMultiSelect.ClearSelection();
                List<ProductMapRecipeInfo> listPR = ListProductMapRecipe(hidProductCode.Value);

                foreach (ListItem listItem in ddlMultiSelect.Items)
                {
                    foreach(ProductMapRecipeInfo pr in listPR)
                    {
                        if (listItem.Value == pr.RecipeCode)
                        {
                            listItem.Selected = true;
                        }
                    }

                }

                txtPackageHeight_Ins.Text = hidPackageHeigth.Value;
                txtPackageLength_Ins.Text = hidPackageLength.Value;
                txtPackageWeight_Ins.Text = hidWeight.Value;
                txtPackageWidth_Ins.Text = hidPackageWidth.Value;
                txtURLvideo_Ins.Text = hidURLvideo.Value;
                txtAdditional_Ins.Text = hidProdutAdditional.Value;
               
                txtWarrantyStartDate_Ins.Text = hidWarrantyStartdate.Value;
                txtWarrantyEndDate_Ins.Text = hidWarrantyEnddate.Value;
                

                txtProductSku_Ins.Text = hidProductSku.Value;
                txtProductCode_Ins.Text = hidProductCode.Value;
                txtProductCode_Ins.Enabled = false;
                hidProductCode_Ins.Value = hidProductCode.Value;
                txtDescription_Ins.Text = hidDescription.Value;
                
                txtPrice_Ins.Text = hidProductPrice.Value;
                txtInsureCost_Ins.Text = hidInsureCost.Value;
                txtFirstDamages_Ins.Text = hidFirstDamages.Value;
                txtGarageQuan_Ins.Text = hidGarageQuan.Value;
                txtProductName_Ins.Text = hidProductName.Value;
                ddlPackageDanger_Ins.SelectedValue = (hidPackageDanger.Value == null || hidPackageDanger.Value == "") ? hidPackageDanger.Value = "-99" : hidPackageDanger.Value; ; 
                ddlWarrantyCondition_Ins.SelectedValue = (hidWarrantyCondition.Value == null || hidWarrantyCondition.Value == "") ? hidWarrantyCondition.Value = "-99" : hidWarrantyCondition.Value; 
                ddlWarrantyType_Ins.SelectedValue = (hidWarrantyType.Value == null || hidWarrantyType.Value == "") ? hidWarrantyType.Value = "-99" : hidWarrantyType.Value;
                ddlCarType_Ins.SelectedValue = (hidCarType.Value == null || hidCarType.Value == "") ? hidCarType.Value = "-99" : hidCarType.Value;
                ddlEcomSpec_Ins.SelectedValue = (hidEcomSpec.Value == null || hidEcomSpec.Value == "") ? hidEcomSpec.Value = "-99" : hidEcomSpec.Value;
                ddlMaintainType_Ins.SelectedValue = (hidMaintainType.Value == null || hidMaintainType.Value == "") ? hidMaintainType.Value = "-99" : hidMaintainType.Value;
                ddlProductBrand_Ins.SelectedValue = (hidProductBrand.Value == null || hidProductBrand.Value == "") ? hidProductBrand.Value = "-99" : hidProductBrand.Value;
                ddlUnit_Ins.SelectedValue = (hidUnit.Value == null || hidUnit.Value == "") ? hidUnit.Value = "-99" : hidUnit.Value;
                ddlProductCategory_Ins.SelectedValue = (hidProductCategory.Value == null || hidProductCategory.Value == "") ? hidProductCategory.Value = "-99" : hidProductCategory.Value;
                ddlProductCategoryLazada_Ins.SelectedValue = (hidLazadaCategoryCode.Value == null || hidLazadaCategoryCode.Value == "") ? hidLazadaCategoryCode.Value = "-99" : hidLazadaCategoryCode.Value;
                txtUpsellScript_Ins.Text = hidUpsellScript.Value;

                hidProductImgId.Value = GetProductImgByCriteria(hidProductCode.Value);

                hidIdList.Value = hidProductId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);


            }

            if (e.CommandName == "DeleteProduct")
            {
                DeleteProductById(hidProductId.Value);
                btnSearch_Click(null, null);
            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            txtProductCode_Ins.Enabled = false;
            hidFlagInsert.Value = "True";

            txtProductCode_Ins.Text = "";
            txtProductSku_Ins.Text = "";
            txtProductName_Ins.Text = "";
            txtPrice_Ins.Text = "";
            ddlProductBrand_Ins.ClearSelection();
            ddlUnit_Ins.ClearSelection();

            ddlCarType_Ins.ClearSelection();
            ddlMaintainType_Ins.ClearSelection();
            txtInsureCost_Ins.Text = "";
            txtFirstDamages_Ins.Text = "";
            txtGarageQuan_Ins.Text = "";

            txtDescription_Ins.Text = "";
            txtUpsellScript_Ins.Text = "";


            lblProductCode_Ins.Text = "";
            lblProductName_Ins.Text = "";
            lblUnit_Ins.Text = "";
            lblPrice_Ins.Text = "";
            lblDescription_Ins.Text = "";

            ddlMultiSelect.ClearSelection();
            ddlProductBrand_Ins.ClearSelection();
            ddlProductCategory_Ins.ClearSelection(); 
            ddlProductCategoryLazada_Ins.ClearSelection();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProductDetail.aspx?ProductCode=" + strCode + "&MenuId=02&ProductDetailBOM=Y\">" + strCode + "</a>";
        }

        protected void BindddlRecipe()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRecipeNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RecipeCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RecipeInfo> lRecipeInfo = JsonConvert.DeserializeObject<List<RecipeInfo>>(respstr);


            ddlMultiSelect.DataSource = lRecipeInfo;

            ddlMultiSelect.DataTextField = "RecipeName";

            ddlMultiSelect.DataValueField = "RecipeCode";

            ddlMultiSelect.DataBind();


        }

        protected void BindddlAllergy()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListAllergyNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["AllergyCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<AllergyInfo> lRecipeInfo = JsonConvert.DeserializeObject<List<AllergyInfo>>(respstr);


            ddlAllergy.DataSource = lRecipeInfo;

            ddlAllergy.DataTextField = "AllergyName";

            ddlAllergy.DataValueField = "AllergyImageName";

            ddlAllergy.DataBind();


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
        protected void BindddlProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = "";
                data["MerchantMapCode"] = hidMerchantCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lProductCategoryInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);



            ddlProductBrand_Search.DataSource = lProductCategoryInfo;

            ddlProductBrand_Search.DataTextField = "ProductBrandName";

            ddlProductBrand_Search.DataValueField = "ProductBrandCode";

            ddlProductBrand_Search.DataBind();

            ddlProductBrand_Search.Items.Insert(0, new ListItem("---- Please Select ----", ""));


            ddlProductBrand_Ins.DataSource = lProductCategoryInfo;

            ddlProductBrand_Ins.DataTextField = "ProductBrandName";

            ddlProductBrand_Ins.DataValueField = "ProductBrandCode";

            ddlProductBrand_Ins.DataBind();

            ddlProductBrand_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));

        }
        protected void BindWarrantyCondition()
        {
            ddlWarrantyCondition_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        protected void BindddlWarrantyType()
        {
            ddlWarrantyType_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }

        protected void BindddlUnit()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_UNIT; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlUnit_Ins.DataSource = lLookupInfo;

            ddlUnit_Ins.DataTextField = "LookupValue";

            ddlUnit_Ins.DataValueField = "LookupCode";

            ddlUnit_Ins.DataBind();

            ddlUnit_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));

        }
        protected void BindddlCarType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_CARTYPE; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlCarType_Ins.DataSource = lLookupInfo;

            ddlCarType_Ins.DataTextField = "LookupValue";

            ddlCarType_Ins.DataValueField = "LookupCode";

            ddlCarType_Ins.DataBind();

            ddlCarType_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));

        }
        protected void BindddlMaintainType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_MAINTAINTYPE; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlMaintainType_Ins.DataSource = lLookupInfo;

            ddlMaintainType_Ins.DataTextField = "LookupValue";

            ddlMaintainType_Ins.DataValueField = "LookupCode";

            ddlMaintainType_Ins.DataBind();

            ddlMaintainType_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));

        }
        protected void BindddlProductCategoryLazada()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductCategoryLazadaNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCategoryCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductCategoryInfo> lProductCategoryInfo = JsonConvert.DeserializeObject<List<ProductCategoryInfo>>(respstr);

            ddlProductCategoryLazada_Ins.DataSource = lProductCategoryInfo;
            ddlProductCategoryLazada_Ins.DataValueField = "ProductCategoryId";
            ddlProductCategoryLazada_Ins.DataTextField = "ProductCategoryName";
            ddlProductCategoryLazada_Ins.DataBind();

            ddlProductCategoryLazada_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        protected void BindddlProductCategory()
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

            ddlProductCategory_Ins.DataSource = lProductCategoryInfo;
            ddlProductCategory_Ins.DataValueField = "ProductCategoryCode";
            ddlProductCategory_Ins.DataTextField = "ProductCategoryName";
            ddlProductCategory_Ins.DataBind();

            ddlProductCategory_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
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
    }
}
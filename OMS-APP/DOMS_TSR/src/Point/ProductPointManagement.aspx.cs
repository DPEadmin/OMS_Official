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
using System.Text.RegularExpressions;

namespace DOMS_TSR.src.Point
{
    public partial class ProductPointManagement : System.Web.UI.Page
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

                LoadProduct();
                
                BindddlUnit();
                BindddlCompany();
                BindddlPropoint();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadProduct();
        }
      
        
        #region Function

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

                data["Sku"] = txtSearchProductSku.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["FlagPointCoupon"] = "Y";

                data["Propoint"] = ddlPropoint_Search.SelectedValue;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["CompanyCode"] = ddlCompanySearch.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);


            return lProductInfo;

        }

        public List<ProductInfo> GetProductMasterByCriteria(string Sku)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Sku"] = Sku;

                data["FlagPointCoupon"] = "Y";

                data["MerchantCode"] = hidMerchantCode.Value;

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

                data["Sku"] = txtSearchProductSku.Text; 

                data["FlagPointCoupon"] = "Y";

                data["Propoint"] = ddlPropoint_Search.SelectedValue;

                data["ProductName"] = txtSearchProductName.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["CompanyCode"] = ddlCompanySearch.SelectedValue;

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
            List<ProductInfo> lProductInfo = new List<ProductInfo>();
            lProductInfo = GetProductMasterByCriteria(txtProductSku_Ins.Text);
            HttpPostedFile Productimg1 = Request.Files["Productimg1"];
            HttpPostedFile Showcase_img11Upload_Ins = Request.Files["Showcase_img11Upload_Ins"];
            HttpPostedFile Showcase_img43Upload_Ins = Request.Files["Showcase_img43Upload_Ins"];
            HttpPostedFile SKUimg1Upload_Ins = Request.Files["SKUimg1Upload_Ins"];
            var regexItem = new Regex("^[a-zA-Z0-9 ]*$");


            
            if (txtProductSku_Ins.Text == "")
            {
                flag = false;
                lblProductSku_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสอ้างอิงให้ครบถ้วน";
            }
            else
            {
                if (hidFlagInsert.Value == "true")
                {
                    if (lProductInfo.Count() > 0)
                    {
                        flag = false;
                        lblProductSku_Ins.Text = "รหัสอ้างอิงนี้มีอยู่แล้ว";
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblProductSku_Ins.Text = "";
                    }
                }
                else
                {
                    if (hidSKUCode.Value != txtProductSku_Ins.Text)
                    {
                        if (lProductInfo.Count() > 0)
                        {
                            flag = false;
                            lblProductSku_Ins.Text = "รหัสอ้างอิงนี้มีอยู่แล้ว";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true;
                            lblProductSku_Ins.Text = "";
                        }
                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblProductSku_Ins.Text = "";
                    }
                }
            }
        
            if (Productimg1 != null && Productimg1.ContentLength > 0 )
            {
                Showcase_img43Upload_Name = "";
                SKUimg1Upload_Name = "";
                flag = (flag == false) ? false : true;
                Productimg1_Name = ProductImgUrl + Productimg1.FileName;

            }
            
            if (txtExchangeAmount_Ins.Text == "" || txtExchangePoint_Ins.Text == "")
            {
                flag = false;
                lblExchange_Ins.Text = MessageConst._MSG_PLEASESELECT + "Exchange";
            }
            else 
            {
                flag = (flag == false) ? false : true;
                lblExchange_Ins.Text = "";
            }
            if (ddlUnit_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblUnit_Ins.Text = MessageConst._MSG_PLEASESELECT + " UNIT";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblUnit_Ins.Text = "";
            }
            if (ddlCompany_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblCompany_Ins.Text = MessageConst._MSG_PLEASESELECT + " ร้านค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCompany_Ins.Text = "";
            }
            if (ddlPropoint_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblProPoint_Ins.Text = MessageConst._MSG_PLEASESELECT + " หมวดหมู่";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProPoint_Ins.Text = "";
            }
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
                lblProductName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อสินค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProductName_Ins.Text = "";
            } 
            if (ddlCompany_Ins.Text == "-99")
            {
                flag = false;
                lblCompany_Ins.Text = MessageConst._MSG_PLEASESELECT + " ร้านค้า";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCompany_Ins.Text = "";
            }

            if (ddlUnit_Ins.SelectedValue == "-99" || ddlUnit_Ins.SelectedValue == "")
            {
                flag = false;
                lblUnit_Ins.Text = MessageConst._MSG_PLEASEINSERT + " หน่วย";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblUnit_Ins.Text = "";
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
                      

                        string ProductCode = "P0000" + runningNo().ToString();
                        //Insert Recipe
                       

                        //Insert Img
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

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertProduct";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ProductCode"] = ProductCode;
                            data["Sku"] = txtProductSku_Ins.Text;
                            data["ProductName"] = txtProductName_Ins.Text;
                            
                            data["FlagDelete"] = "N";
                            data["Unit"] = ddlUnit_Ins.SelectedValue;
                            data["CompanyCode"] = ddlCompany_Ins.SelectedValue;
                            data["Description"] = txtDescription_Ins.Text;
                            data["AllergyRemark"] = AllergyList;                            
                            data["MerchantCode"] = merchantinfo.MerchantCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagPointType"] = "Y";
                            


                            data["Propoint"] = ddlPropoint_Ins.SelectedValue ;
                            data["ExchangeAmount"] = txtExchangeAmount_Ins.Text;
                            data["ExchangePoint"] = txtExchangePoint_Ins.Text;

                            data["Product_img1"] = Productimg1_Name;


                           
                           

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
                     

                        //Update Flag อันเก่าเป็น y insert ใหม่
                        UpdateProductMapRecipe(txtProductCode_Ins.Text);
                       

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
                            data["CarType"] = "00";
                            data["MaintainType"] = "00";
                            data["InsureCost"] = "0";
                            data["FirstDamages"] = "0";
                            data["GarageQuan"] = "0";
                            data["TransportPrice"] = "0";
                            data["FlagDelete"] = "N";
                            data["Unit"] = ddlUnit_Ins.SelectedValue;
                            data["CompanyCode"] = ddlCompany_Ins.SelectedValue;
                            data["Description"] = txtDescription_Ins.Text;
                            data["Product_img1"] = Productimg1_Name;
                            data["Propoint"] = ddlPropoint_Ins.SelectedValue;
                            data["ExchangeAmount"] = txtExchangeAmount_Ins.Text;
                            data["ExchangePoint"] = txtExchangePoint_Ins.Text;

                            data["Updateby"] = empInfo.EmpCode;

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
            ddlPropoint_Ins.ClearSelection();
            ddlCompany_Ins.ClearSelection();
            txtExchangeAmount_Ins.Text = "";
            txtExchangePoint_Ins.Text = "";
            
            ddlUnit_Ins.ClearSelection();
            txtDescription_Ins.Text = "";
            lblCompany_Ins.Text = "";
            lblProductCode_Ins.Text = "";
            lblProductName_Ins.Text = "";
            lblUnit_Ins.Text = "";
            lblProductSku_Ins.Text = "";
            lblDescription_Ins.Text = "";

           


            
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchProductSku.Text = "";
            txtSearchProductName.Text = "";
            ddlPropoint_Search.ClearSelection();
            ddlCompanySearch.ClearSelection();
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
            HiddenField hidCompanyCode = (HiddenField)row.FindControl("hidCompanyCode");
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
            HiddenField hidProduct_img1 = (HiddenField)row.FindControl("hidProduct_img1");

            HiddenField hidExchangeAmount = (HiddenField)row.FindControl("hidExchangeAmount"); 
            HiddenField hidExchangePoint = (HiddenField)row.FindControl("hidExchangePoint");
            HiddenField hidPropoint = (HiddenField)row.FindControl("hidPropoint");


            if (e.CommandName == "ShowProduct")
            {
                List<ProductMapRecipeInfo> listPR = ListProductMapRecipe(hidProductCode.Value);

                Productimg1_Name = ProductImgUrl + hidProduct_img1.Value;
                ddlPropoint_Ins.SelectedValue = (hidPropoint.Value == null || hidPropoint.Value == "") ? hidPropoint.Value = "-99" : hidPropoint.Value;
                txtExchangeAmount_Ins.Text = hidExchangeAmount.Value;
                txtExchangePoint_Ins.Text = hidExchangePoint.Value;

                ddlCompany_Ins.SelectedValue = (hidCompanyCode.Value == null || hidCompanyCode.Value == "") ? hidCompanyCode.Value = "-99" : hidCompanyCode.Value;
                txtProductName_Ins.Text = hidProductName.Value;
                txtProductSku_Ins.Text = hidProductSku.Value;
                hidSKUCode.Value = hidProductSku.Value;
                txtProductCode_Ins.Text = hidProductCode.Value;
                txtProductCode_Ins.Enabled = false;
                hidProductCode_Ins.Value = hidProductCode.Value;
                txtDescription_Ins.Text = hidDescription.Value;
                
                ddlUnit_Ins.SelectedValue = (hidUnit.Value == null || hidUnit.Value == "") ? hidUnit.Value = "-99" : hidUnit.Value;
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
            ddlUnit_Ins.ClearSelection();
            ddlCompany_Ins.ClearSelection();
            ddlPropoint_Ins.ClearSelection();
            txtExchangeAmount_Ins.Text = "";
            txtExchangePoint_Ins.Text = "";
            

            txtDescription_Ins.Text = "";


            lblProductCode_Ins.Text = "";
            lblProductName_Ins.Text = "";
            lblUnit_Ins.Text = "";
            lblDescription_Ins.Text = "";
            lblProductSku_Ins.Text = "";


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProductPointDetail.aspx?ProductCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
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

        protected void BindddlPropoint()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = StaticField.LookupType_PROPOINT; 


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlPropoint_Ins.DataSource = lLookupInfo;

            ddlPropoint_Ins.DataTextField = "LookupValue";

            ddlPropoint_Ins.DataValueField = "LookupCode";

            ddlPropoint_Ins.DataBind();

            ddlPropoint_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlPropoint_Search.DataSource = lLookupInfo;

            ddlPropoint_Search.DataTextField = "LookupValue";

            ddlPropoint_Search.DataValueField = "LookupCode";

            ddlPropoint_Search.DataBind();

            ddlPropoint_Search.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

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

            ddlUnit_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected void BindddlCompany()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CompanyListNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CompanyCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CompanyInfo> lLookupInfo = JsonConvert.DeserializeObject<List<CompanyInfo>>(respstr);


            ddlCompany_Ins.DataSource = lLookupInfo;

            ddlCompany_Ins.DataTextField = "CompanyNameTH";

            ddlCompany_Ins.DataValueField = "CompanyCode";

            ddlCompany_Ins.DataBind();

            ddlCompany_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlCompanySearch.DataSource = lLookupInfo;

            ddlCompanySearch.DataTextField = "CompanyNameTH";

            ddlCompanySearch.DataValueField = "CompanyCode";

            ddlCompanySearch.DataBind();

            ddlCompanySearch.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }



        #endregion
    }
}
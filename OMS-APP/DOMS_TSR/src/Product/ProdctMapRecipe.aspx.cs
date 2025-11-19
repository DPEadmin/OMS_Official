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
    public partial class ProdctMapRecipe : System.Web.UI.Page
    {
        protected static string APIUrl;

        protected static string ProductImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];

        string Codelist = "";
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

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    APIUrl = empInfo.ConnectionAPI;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                LoadProduct();

                BindddlLogistic();
                BindddlMerchant();
                BindddlProductCategory();
                BindddlUnit();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadProduct();
        }

        #region Function

      

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

                data["MerchantCode"] = txtSearchMerchantCode.Text;

                data["MerchantName"] = txtSearchMerchantName.Text;

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

            for (int i = 0; i < gvProduct.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvProduct.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
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

            hidIdList.Value = "";
            return true;

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

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (txtProductCode_Ins.Text == "")
            {
                flag = false;
                lblProductCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสสินค้า";
            }
            else
            {
                if (txtProductCode_Ins.Text != "")
                {


                    Boolean isDuplicate = ValidateDuplicate();


                    if (isDuplicate)
                    {
                        flag = false;
                        lblProductCode_Ins.Text = MessageConst._DATA_NComplete;

                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblProductCode_Ins.Text = "";

                    }
                }
            }


            



            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
            }

            return flag;
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

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (validateInsert())
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

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertProduct";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadProduct();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-product').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }

                }
                else //Update
                {
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

                                    data["ProductCode"] = hidProductImgId.Value;
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

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
            txtSearchMerchantCode.Text = "";
            txtSearchMerchantName.Text = "";

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
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");
            HiddenField hidProductPrice = (HiddenField)row.FindControl("hidProductPrice");
          

            HiddenField hidDescription = (HiddenField)row.FindControl("hidDescription");
            HiddenField hidUnit = (HiddenField)row.FindControl("hidUnit");
            HiddenField hidQuantity = (HiddenField)row.FindControl("hidQuantity");
            HiddenField hidMerchant = (HiddenField)row.FindControl("hidMerchant");
            HiddenField hidProductCategory = (HiddenField)row.FindControl("hidProductCategory");
            HiddenField hidPackageHeigth = (HiddenField)row.FindControl("hidPackageHeigth");
            HiddenField hidPackageLength = (HiddenField)row.FindControl("hidPackageLength");
            HiddenField hidPackageWidth = (HiddenField)row.FindControl("hidPackageWidth");
            HiddenField hidProductHeigth = (HiddenField)row.FindControl("hidProductHeigth");
            HiddenField hidProductLength = (HiddenField)row.FindControl("hidProductLength");
            HiddenField hidProductWidth = (HiddenField)row.FindControl("hidProductWidth");
            HiddenField hidWeight = (HiddenField)row.FindControl("hidWeight");
            HiddenField hidTransportationType = (HiddenField)row.FindControl("hidTransportationType");
     


            if (e.CommandName == "ShowProduct")
            {

                
                ddlUnit_Ins.SelectedValue = (hidUnit.Value == null || hidUnit.Value == "") ? hidUnit.Value = "-99" : hidUnit.Value;

                hidProductImgId.Value = GetProductImgByCriteria(hidProductCode.Value);

                hidIdList.Value = hidProductId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);

            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            txtProductCode_Ins.Enabled = true;
            hidFlagInsert.Value = "True";

            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-product').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProdctMapRecipeDetail.aspx?ProductCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindddlMerchant()
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


            ddlMerChant_Ins.DataSource = lMerchantInfo;

            ddlMerChant_Ins.DataTextField = "MerchantName";

            ddlMerChant_Ins.DataValueField = "MerchantCode";

            ddlMerChant_Ins.DataBind();

            ddlMerChant_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

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

            ddlProductCategory_Ins.DataTextField = "ProductCategoryName";

            ddlProductCategory_Ins.DataValueField = "ProductCategoryCode";

            ddlProductCategory_Ins.DataBind();

            ddlProductCategory_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }


        protected void BindddlUnit()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "UNIT";


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
        protected void BindddlLogistic()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLogisticNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LogisticInfo> lLogisticInfo = JsonConvert.DeserializeObject<List<LogisticInfo>>(respstr);


            

        }
        #endregion


    }
}
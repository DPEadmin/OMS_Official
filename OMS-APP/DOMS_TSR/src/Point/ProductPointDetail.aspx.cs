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

namespace DOMS_TSR.src.Point
{
    public partial class ProductPointDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int currentPdPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;
                currentPdPageNumber = 1;

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

                LoadPromotionComplementarySection();
                LoadProduct();
                LoadProductImages();
                LoadComplementaryProduct();
                LoadProductMasterAddComprementary();
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

        protected void LoadProduct()
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();
  
            lProductInfo = GetProductMasterByCriteria();

            txtProductCode.Text = lProductInfo[0].ProductCode;

            txtProductSku.Text = lProductInfo[0].Sku;

            txtProductName.Text = lProductInfo[0].ProductName;

            txtCouponType.Text = lProductInfo[0].PropointName;

            txtProductWidth.Text = lProductInfo[0].ProductWidth.ToString();
            txtProductLength.Text = lProductInfo[0].ProductLength.ToString();
            txtProductHeight.Text = lProductInfo[0].ProductHeigth.ToString();

            txtPackageWidth.Text = lProductInfo[0].PackageWidth.ToString();
            txtPackageLength.Text = lProductInfo[0].PackageLength.ToString();
            txtPackageHeight.Text = lProductInfo[0].PackageHeigth.ToString();

            txtExchangeRate.Text = lProductInfo[0].ExchangeRate.ToString();

            txtUnit.Text = lProductInfo[0].UnitName;

            txtProductWeight.Text = lProductInfo[0].Weight.ToString();

            txtLogisticType.Text = lProductInfo[0].TransportationTypeName.ToString();

            txtDescription.InnerText = lProductInfo[0].Description;

          

            List<ProductMapRecipeInfo> ListPR = ListProductMapRecipe(lProductInfo[0].ProductCode);
        }

        protected void LoadProductImages()
        {
            List<ProductInfo> lProductInfo = new List<ProductInfo>();

            lProductInfo = GetProductImage();

            ProductImg.Src = lProductInfo.Count > 0 ? APIUrl + lProductInfo[0].ProductImageUrl : "";


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

        protected void ddlProductPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPdPageNumber = Int32.Parse(ddlPdPage.SelectedValue);

            LoadProductMasterAddComprementary();
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
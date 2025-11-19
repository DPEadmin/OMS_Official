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
    public partial class Recipe : System.Web.UI.Page
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
                    Response.Redirect("..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


              LoadRecipe();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadRecipe();
        }

        #region Function

      

        protected void LoadRecipe()
        {
            List<RecipeInfo> lRecipeInfo = new List<RecipeInfo>();

            

            int? totalRow = CountProductMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            
            lRecipeInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lRecipeInfo;

            gvProduct.DataBind();


            

        }


        public List<RecipeInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListRecipePagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RecipeCode"] = txtSearchRecipeCode.Text;

                data["RecipeName"] = txtSearchRecipeName.Text;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RecipeInfo> lProductInfo = JsonConvert.DeserializeObject<List<RecipeInfo>>(respstr);


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

                data["ProductCode"] = txtRecipeCode_Ins.Text;
             
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RecipeInfo> lRecipeInfo = JsonConvert.DeserializeObject<List<RecipeInfo>>(respstr);

            if (lRecipeInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }

        

        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountRecipeListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RecipeCode"] = txtSearchRecipeCode.Text;

                data["RecipeName"] = txtSearchRecipeName.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


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


            LoadRecipe();
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
                    HiddenField hidCode = (HiddenField)gvProduct.Rows[i].FindControl("hidRecipeId");

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

                APIpath = APIUrl + "/api/support/DeleteRecipe";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["RecipeIdDelete"] = Codelist;


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

            if (txtRecipeCode_Ins.Text == "")
            {
                flag = false;
                lblRecipeCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสสินค้า";
            }
            else
            {
                if (txtRecipeCode_Ins.Text != "")
                {


                    Boolean isDuplicate = ValidateDuplicate();


                    if (isDuplicate)
                    {
                        flag = false;
                        lblRecipeCode_Ins.Text = MessageConst._DATA_NComplete;

                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblRecipeCode_Ins.Text = "";

                    }
                }
            }


            if (txtRecipeName_Ins.Text == "")
            {
                flag = false;
                lblRecipeName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อส่วนประกอบ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblRecipeName_Ins.Text = "";
            }




            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Recipe').modal();", true);
            }

            return flag;
        }



        #endregion

        #region Event 

        protected void gvProduct_Change(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;

            List<RecipeInfo> lRecipeInfo = new List<RecipeInfo>();

            lRecipeInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lRecipeInfo;

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

            LoadRecipe();

            
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
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertRecipe";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["RecipeCode"] = txtRecipeCode_Ins.Text;
                        data["RecipeName"] = txtRecipeName_Ins.Text;

                        data["CreateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        LoadRecipe();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Recipe').modal('hide');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }

                }
                else //Update
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateRecipe";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["RecipeId"] = hidIdList.Value;

                        data["RecipeCode"] = txtRecipeCode_Ins.Text;
                        data["RecipeName"] = txtRecipeName_Ins.Text;
                        data["UpdateBy"] = empInfo.EmpCode;



                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        LoadRecipe();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Recipe').modal('hide');", true);



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
            txtRecipeCode_Ins.Text = "";
            txtRecipeName_Ins.Text = "";

            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Recipe').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchRecipeCode.Text = "";
            txtSearchRecipeName.Text = "";
        
       

        }

     protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProduct.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidRecipeId = (HiddenField)row.FindControl("hidRecipeId");
            HiddenField hidRecipeCode = (HiddenField)row.FindControl("hidRecipeCode");
            HiddenField hidRecipeName = (HiddenField)row.FindControl("hidRecipeName");

            if (e.CommandName == "ShowProduct")
            {
                txtRecipeCode_Ins.Text = hidRecipeCode.Value;
                txtRecipeName_Ins.Text = hidRecipeName.Value;
                hidIdList.Value = hidRecipeId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Recipe').modal();", true);

            }

        }


        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            txtRecipeCode_Ins.Enabled = true;
            hidFlagInsert.Value = "True";

            txtRecipeCode_Ins.Text = "";
            txtRecipeName_Ins.Text = "";


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Recipe').modal();", true);
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

     



        protected void BindddlProductBrand()
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

            List<ProductBrandInfo> lProductCategoryInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);



        }

        #endregion


    }
}
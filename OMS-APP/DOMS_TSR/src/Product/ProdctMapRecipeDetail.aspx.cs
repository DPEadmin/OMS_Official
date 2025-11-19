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
    public partial class ProdctMapRecipeDetail : System.Web.UI.Page
    {
        protected static string APIUrl;
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        int? totalRow;
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


                LoadProductMapRecipe();
                LoadRecipe();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadProductMapRecipe();
        
        }

        #region Function

        protected void LoadRecipe()
        {
            List<ProductMapRecipeInfo> lProductInfo = new List<ProductMapRecipeInfo>();

            lProductInfo = GetRecipeMasterByCriteria();

            CklRecipe.DataSource = lProductInfo;

            CklRecipe.DataTextField = "RecipeName";

            CklRecipe.DataValueField = "RecipeCode";

            CklRecipe.DataBind();

        }
        protected void myCheckBoxList_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in CklRecipe.Items)
            {
                List<ProductMapRecipeInfo> lProductInfo = new List<ProductMapRecipeInfo>();
                lProductInfo = GetRecipeMasterByCriteria();
                var list = lProductInfo.Where(f => f.RecipeCodeCheck == item.Value).ToList();
              
                if (list.Count > 0)
                {
                    item.Selected = true;
                }
            }
        }
       
        protected void LoadProductMapRecipe()
        {
            List<ProductMapRecipeInfo> lProductInfo = new List<ProductMapRecipeInfo>();

            

            totalRow = CountProductMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lProductInfo = GetProductMasterByCriteria();

            gvRecipe.DataSource = lProductInfo;

            gvRecipe.DataBind();


            

        }

        public List<ProductMapRecipeInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMapRecipeNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ProductCode"] = Request.QueryString["ProductCode"];
             

              
          
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductMapRecipeInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductMapRecipeInfo>>(respstr);


            return lProductInfo;

        }
        public List<ProductMapRecipeInfo> GetRecipeMasterByCriteria()
        {
            string respstr = "";
            if (totalRow > 0)
            {
                APIpath = APIUrl + "/api/support/ListRecipeByproductNoPagingByCriteria";
            }
            else
            {
                APIpath = APIUrl + "/api/support/ListRecipeNoPagingByCriteria";
            }

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                if (totalRow > 0)
                {
                    data["ProductCode"] = Request.QueryString["ProductCode"];
                }
                
                



                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductMapRecipeInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductMapRecipeInfo>>(respstr);


            return lProductInfo;

        }
        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCountProductMapRecipeNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = Request.QueryString["ProductCode"];
                


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


            LoadProductMapRecipe();
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

            for (int i = 0; i < gvRecipe.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvRecipe.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRecipe.Rows[i].FindControl("hidRecipeId");
                    HiddenField hidCodename = (HiddenField)gvRecipe.Rows[i].FindControl("hidRecipeName");
                    string AAA = hidCodename.Value.ToString();
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

                APIpath = APIUrl + "/api/support/DeleteProductMapRecipe";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ProductMapRecipeIdDelete"] = Codelist;


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

        #endregion

        #region Event 

        protected void gvProduct_Change(object sender, GridViewPageEventArgs e)
        {
            gvRecipe.PageIndex = e.NewPageIndex;

            List<ProductMapRecipeInfo> lProductInfo = new List<ProductMapRecipeInfo>();

            lProductInfo = GetProductMasterByCriteria();

            gvRecipe.DataSource = lProductInfo;

            gvRecipe.DataBind();

        }

        protected void chkProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvRecipe.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvRecipe.HeaderRow.FindControl("chkProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvRecipe.Rows[i].FindControl("hidRecipeId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProduct = (CheckBox)gvRecipe.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = true;
                }
                else
                {

                    CheckBox chkProduct = (CheckBox)gvRecipe.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadProductMapRecipe();


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
        int Asum;
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            
            int? sum;
            int? sumclear;
            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                string respstrclearOld = "";

                APIpath = APIUrl + "/api/support/UpdateClearProductMapRecipe";

                using (var wb = new WebClient())
                {
                    var dataclear = new NameValueCollection();

                    
                    dataclear["ProductCode"] = Request.QueryString["ProductCode"];
                    dataclear["UpdateBy"] = empInfo.EmpCode;



                    var responseclear = wb.UploadValues(APIpath, "POST", dataclear);

                    respstrclearOld = Encoding.UTF8.GetString(responseclear);
                }

                sumclear = JsonConvert.DeserializeObject<int?>(respstrclearOld);

                
                    foreach (ListItem li in CklRecipe.Items)
                    {
                        if (li.Selected == true)
                        {
                            string respstr = "";
                            APIpath = APIUrl + "/api/support/InsertProductMapRecipe";
                            using (var wb = new WebClient())
                            {
                                var data = new NameValueCollection();

                                data["RecipeCode"] = li.Value;
                                data["ProductCode"] = Request.QueryString["ProductCode"];
                                data["FlagDelete"] = "N";
                             
                                data["CreateBy"] = empInfo.EmpCode;


                                var response = wb.UploadValues(APIpath, "POST", data);

                                respstr = Encoding.UTF8.GetString(response);
                            }

                            sum = JsonConvert.DeserializeObject<int?>(respstr);
                            Asum++;
                        }
                    }



                    if (Asum > 0)
                    {
                        btnCancel_Click(null, null);

                        LoadProductMapRecipe();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Recipe').modal('hide');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
                    

            

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
            CklRecipe.DataSource = null;
            CklRecipe.DataBind();
          
            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Recipe').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvRecipe.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidRecipeId = (HiddenField)row.FindControl("hidRecipeId");
            HiddenField hidRecipeCode = (HiddenField)row.FindControl("hidRecipeCode");
            HiddenField hidRecipeName = (HiddenField)row.FindControl("hidRecipeName");

            if (e.CommandName == "ShowProduct")
            {
                
                 hidIdList.Value = hidRecipeId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Recipe').modal();", true);

            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Recipe').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProductDetail.aspx?ProductCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

       
        #endregion


    }
}
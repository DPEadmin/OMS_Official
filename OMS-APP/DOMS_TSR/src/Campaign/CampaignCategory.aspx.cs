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
using System.Text.RegularExpressions;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using AjaxControlToolkit;
using System.IO;
namespace DOMS_TSR.src.Campaign
{
    public partial class CampaignCategory : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string CampaignCategoryImageUrl = ConfigurationManager.AppSettings["CampaignCategoryImageUrl"];
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
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    APIUrl = empInfo.ConnectionAPI;
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                LoadCampaignCategory();

              }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadCampaignCategory();
        }

        #region Function



        protected void LoadCampaignCategory()
        {
            List<CampaignCategoryInfo> lProductInfo = new List<CampaignCategoryInfo>();

           

            int? totalRow = CountProductMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lProductInfo = GetProductMasterByCriteria();

            gvProduct.DataSource = lProductInfo;

            gvProduct.DataBind();


            

        }

        public List<CampaignCategoryInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignCategoryPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = txtSearchCampaignCategoryCode.Text;

                data["CampaignCategoryName"] = txtSearchCampaignCategoryName.Text;

                data["MerchantMapCode"] = hidMerchantCode.Value;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignCategoryInfo> lProductInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);


            return lProductInfo;

        }

        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCampaignCategoryListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = txtSearchCampaignCategoryCode.Text;

                data["MerchantMapCode"] = hidMerchantCode.Value;

                data["CamCate_name"] = txtSearchCampaignCategoryName.Text;

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


            LoadCampaignCategory();
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
                    HiddenField hidCode = (HiddenField)gvProduct.Rows[i].FindControl("hidCampaignCategorytId");

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

                APIpath = APIUrl + "/api/support/DeleteCampaignCategory";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCategoryIdDelete"] = Codelist;


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
            gvProduct.PageIndex = e.NewPageIndex;

            List<CampaignCategoryInfo> lProductInfo = new List<CampaignCategoryInfo>();

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
                    HiddenField hidCode = (HiddenField)gvProduct.Rows[i].FindControl("hidCampaignCategorytId");

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
            LoadCampaignCategory();

        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteProduct();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Please select item to delete');", true);
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
                if (hidFlagInsert.Value == "True")
                {
                    if (validateInsert()) //Insert
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
                                hidPicturePromotionUrl_Ins.Value = CampaignCategoryImageUrl + postedFile.FileName;
                                string respstring = "";
                                string APIpath1 = APIUrl + "/api/support/Savepicfromjsonstring64CampaignCategory";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["CampaignCategoryCode"] = txtCampaignCategoryCode_Ins.Text;
                                    data["CampaignCategoryImageUrl"] = CampaignCategoryImageUrl + postedFile.FileName;
                                    data["CampaignCategoryImageName"] = postedFile.FileName;
                                    data["CampaignCategoryImageBase64"] = base64String;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath1, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }
                                APIpath = APIUrl + "/api/support/InsertCampaignCategoryImage";

                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["CampaignCategoryCode"] = txtCampaignCategoryCode_Ins.Text;
                                    data["CampaignCategoryImageUrl"] = CampaignCategoryImageUrl + postedFile.FileName;
                                    data["CampaignCategoryImageBase64"] = postedFile.FileName;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }
                            }
                        }
                        
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertCampaignCategory";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CampaignCategoryCode"] = txtCampaignCategoryCode_Ins.Text;
                            data["CampaignCategoryName"] = txtCamCate_name_Ins.Text;
                            data["PictureCampaignUrl"] = hidPicturePromotionUrl_Ins.Value;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["MerchantMapCode"] = hidMerchantCode.Value;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadCampaignCategory();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Campaigncategory').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                }
              
                else //Update
                {
                    if (validateUpdate()) //Insert
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
                                hidPicturePromotionUrl_Ins.Value = CampaignCategoryImageUrl + postedFile.FileName;
                                string respstring = "";
                                string APIpath1 = APIUrl + "/api/support/Savepicfromjsonstring64CampaignCategory";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["CampaignCategoryCode"] = txtCampaignCategoryCode_Ins.Text;
                                    data["CampaignCategoryImageUrl"] = CampaignCategoryImageUrl + postedFile.FileName;
                                    data["CampaignCategoryImageName"] = postedFile.FileName;
                                    data["CampaignCategoryImageBase64"] = base64String;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath1, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }

                            }
                        }
                        
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCampaignCategory";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CampaignCategoryId"] = hidIdList.Value;
                            data["PictureCampaignUrl"] = hidPicturePromotionUrl_Ins.Value;
                            data["CampaignCategoryCode"] = txtCampaignCategoryCode_Ins.Text;
                            data["CampaignCategoryName"] = txtCamCate_name_Ins.Text;
                            data["UpdateBy"] = empInfo.EmpCode;



                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadCampaignCategory();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Campaigncategory').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                        }
                    }
                      

                }

            }

        }

        protected Boolean validateInsert()
        {
            Boolean flag = true;

            if (txtCampaignCategoryCode_Ins.Text == "")
            {
                flag = false;
                lblCampaignCategoryCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Brand code";
            }
            else
            {
                if (txtCampaignCategoryCode_Ins.Text != "")
                {


                    Boolean isDuplicate = ValidateDuplicate();


                    if (isDuplicate)
                    {
                        flag = false;
                        lblCampaignCategoryCode_Ins.Text = MessageConst._DATA_NComplete;

                    }
                    else
                    {
                        flag = (flag == false) ? false : true;
                        lblCampaignCategoryCode_Ins.Text = "";

                    }
                    if (Regex.IsMatch(txtCampaignCategoryCode_Ins.Text, @"^[a-zA-Z0-9\s]+$"))
                    {
                        flag = (!flag) ? false : true;
                        lblCampaignCategoryCode_Ins.Text = "";
                    }
                    else
                    {
                        flag = false;
                        lblCampaignCategoryCode_Ins.Text = "ไม่อนุญาติให้กรอกอักษรพิเศษ";
                    }
                }
            }


            if (txtCamCate_name_Ins.Text == "")
            {
                flag = false;
                lblCamCate_name_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Brand name";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCamCate_name_Ins.Text = "";
            }


            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Campaigncategory').modal();", true);
            }

            return flag;
        }

        protected Boolean validateUpdate()
        {
            Boolean flag = true;

            


            if (txtCamCate_name_Ins.Text == "")
            {
                flag = false;
                lblCamCate_name_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Brand name";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblCamCate_name_Ins.Text = "";
            }


            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Campaigncategory').modal();", true);
            }

            return flag;
        }

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/CampaignCategoryCodeValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = txtCampaignCategoryCode_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignCategoryInfo> lProductInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

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



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCampaignCategoryCode_Ins.Text = "";
            txtCamCate_name_Ins.Text = "";
           
            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Campaigncategory').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchCampaignCategoryCode.Text = "";
            txtSearchCampaignCategoryName.Text = "";
        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvProduct.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCampaignCategorytId = (HiddenField)row.FindControl("hidCampaignCategorytId");
            HiddenField hidCampaignCategoryCode = (HiddenField)row.FindControl("hidCampaignCategoryCode");
            HiddenField hidCampaignCategoryName = (HiddenField)row.FindControl("hidCampaignCategoryName");

            if (e.CommandName == "ShowProduct")
            {
                txtCampaignCategoryCode_Ins.Text = hidCampaignCategoryCode.Value;
                txtCamCate_name_Ins.Text = hidCampaignCategoryName.Value;
                txtCampaignCategoryCode_Ins.Enabled = false;
                lblCamCate_name_Ins.Text = "";
                lblCampaignCategoryCode_Ins.Text = "";
                hidIdList.Value = hidCampaignCategorytId.Value;
                hidFlagInsert.Value = "False";
                loadCampaignImage(hidCampaignCategoryCode.Value);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Campaigncategory').modal();", true);

            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {


            hidFlagInsert.Value = "True";

            txtCamCate_name_Ins.Text = "";
            txtCampaignCategoryCode_Ins.Text = "";
            lblCamCate_name_Ins.Text = "";
            lblCampaignCategoryCode_Ins.Text = "";

            txtCampaignCategoryCode_Ins.Enabled = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Campaigncategory').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProductDetail.aspx?ProductCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }


        #endregion

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

        protected void loadCampaignImage(string CampaignCategoryCode)
        {
            CampaignCategoryInfo cInfo = new CampaignCategoryInfo();
            List<CampaignCategoryInfo> lcInfo = new List<CampaignCategoryInfo>();
            lcInfo = GetCampaignImage(CampaignCategoryCode);

            CampaignPicIm.Src = lcInfo.Count > 0 ? lcInfo[0].PictureCampaignUrl : "";
        }

        protected List<CampaignCategoryInfo> GetCampaignImage(string CampaignCategoryCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCategoryCode"] = CampaignCategoryCode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<CampaignCategoryInfo> lCampaignCateInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

            return lCampaignCateInfo;
        }
    }
}
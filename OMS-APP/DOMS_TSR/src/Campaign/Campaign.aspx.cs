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
using System.Globalization;
using System.IO;

namespace DOMS_TSR.src.Campaign
{
    public partial class Campaign : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string CampaignImgUrl = ConfigurationManager.AppSettings["CampaignImageUrl"];
        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;
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
                    
                    hidMerchantCodeSess.Value = merchantinfo.MerchantCode;
                    HidMerchantCode.Value = empInfo.CompanyCode;
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
               
                loadCampaign();
                BindddlCampaignSpec();
            }
        }
        protected void BindddlCampaignSpec()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "CAMPAIGNSPEC";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlCampaingnSpec_Ins.DataSource = lLookupInfo;

            ddlCampaingnSpec_Ins.DataTextField = "LookupValue";

            ddlCampaingnSpec_Ins.DataValueField = "LookupCode";

            ddlCampaingnSpec_Ins.DataBind();

            ddlCampaingnSpec_Ins.Items.Insert(0, new ListItem("---- Please select ----", "-99"));

            ddlSearchCampaignSpec.DataSource = lLookupInfo;

            ddlSearchCampaignSpec.DataTextField = "LookupValue";

            ddlSearchCampaignSpec.DataValueField = "LookupCode";

            ddlSearchCampaignSpec.DataBind();

            ddlSearchCampaignSpec.Items.Insert(0, new ListItem("---- Please select ----", "-99"));

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadCampaign();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchCampaignCode.Text = "";
            txtSearchCampaignName.Text = "";
         
            ddlSearchCampaignStatus.SelectedValue = "-99";
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCampaignCode_Ins.Text = "";
            txtCampaignName_Ins.Text = "";
         

            lblCampaignCode_Ins.Text = "";
            lblCampaignName_Ins.Text = "";
       
            hidPictureCampaignUrl_Ins.Value = null;


            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblCampaignCode_Ins.Text = "";
            lblCampaignName_Ins.Text = "";
        

            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    if (ValidateInsertandUpdate())
                    {
                        String base64String = "";
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
                                base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                hidPictureCampaignUrl_Ins.Value = CampaignImgUrl + postedFile.FileName;
                                hidFileNane.Value = postedFile.FileName;

                                //Save Images
                                string respstring = "";
                                string APIpath1 = APIUrl + "/api/support/SaveCampaignpicfromjsonstring64";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["CampaignCodew"] = txtCampaignCode_Ins.Text;
                                    data["PictureCampaingUrl"] = CampaignImgUrl + postedFile.FileName;
                                    data["CampaignImageName"] = postedFile.FileName;
                                    data["CampaignImageBase64"] = base64String;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath1, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }
                            }
                        }
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertCampaign";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CampaignCode"] = txtCampaignCode_Ins.Text;
                            data["CampaignName"] = txtCampaignName_Ins.Text;
                            data["CampaignCategory"] = "-99";
                            data["PictureCampaignUrl"] = hidPictureCampaignUrl_Ins.Value;
                            data["CampaignImageName"] = hidFileNane.Value;
                            data["MerchantMapCode"] = merchantinfo.MerchantCode;
                            data["CampaignType"] = "PROMOTIONSET";
                            data["MerchantCode"] = HidMerchantCode.Value;
                            data["CampaignSpec"] = ddlCampaingnSpec_Ins.SelectedValue;
                            

                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["Active"] = ddlActive_Ins.SelectedValue;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadCampaign();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-campaign').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else // Update
                {
                    if (ValidateInsertandUpdate())
                    {
                        String base64String = "";
                        HttpFileCollection uploadFiles = Request.Files;
                        for (int i = 0; i < uploadFiles.Count; i++)
                        {
                            HttpPostedFile postedFile = uploadFiles[i];
                            if (postedFile.ContentLength > 0)
                            {
                                //Convert to Base64
                                Stream fs = postedFile.InputStream;
                                BinaryReader br = new BinaryReader(fs);
                                Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                                hidPictureCampaignUrl_Ins.Value = CampaignImgUrl + postedFile.FileName;
                                hidFileNane.Value = postedFile.FileName;

                                //Save Images
                                string respstring = "";
                                string APIpath1 = APIUrl + "/api/support/SaveCampaignpicfromjsonstring64";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["CampaignCodew"] = txtCampaignCode_Ins.Text;
                                    data["PictureCampaingUrl"] = CampaignImgUrl + postedFile.FileName;
                                    data["CampaignImageName"] = postedFile.FileName;
                                    data["CampaignImageBase64"] = base64String;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath1, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }
                            }

                        }
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCampaign";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CampaignId"] = hidCampaign_Ins.Value;
                            data["CampaignCode"] = txtCampaignCode_Ins.Text;
                            data["CampaignName"] = txtCampaignName_Ins.Text;
                            data["CampaignCategory"] = "-99";
                            data["CampaignSpec"] = ddlCampaingnSpec_Ins.SelectedValue;
                            data["PictureCampaignUrl"] = hidPictureCampaignUrl_Ins.Value;
                            data["CampaignImageName"] = hidFileNane.Value;

                            data["CampaignType"] = "PROMOTIONSET";

                            
                            data["CreateBy"] = hidEmpCode.Value;
                            data["UpdateBy"] = hidEmpCode.Value;
                            data["Active"] = ddlActive_Ins.SelectedValue;
                            data["FlagDelete"] = "N";

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                        if(sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadCampaign();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-campaign').modal('hide');", true);
                        }
                    }
                    else
                    {             
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);                      
                    }
                }
            }
        }
        protected Boolean ValidateInsertandUpdate()
        {
            Boolean flag = true;


            
            if (txtCampaignCode_Ins.Text == "" || txtCampaignCode_Ins.Text == null)
            {
                flag = false;
                lblCampaignCode_Ins.Text = "Please insert Campaign ID";
            }
            else
            {
                if (hidFlagInsert.Value == "True")
                {
                    if (ValidateCampaignCodeInsert())
                    {
                        flag = (!flag) ? false : true;
                        lblCampaignCode_Ins.Text = "";
                    }
                    else
                    {
                        flag = false;
                        lblCampaignCode_Ins.Text = "Campaign ID duplicate";
                    }
                    if (Regex.IsMatch(txtCampaignCode_Ins.Text, @"^[a-zA-Z0-9\s]+$"))
                    {
                        flag = (!flag) ? false : true;
                        lblCampaignCode_Ins.Text = "";
                    }
                    else
                    {
                        flag = false;
                        lblCampaignCode_Ins.Text = "Special characters are not allowed.";
                    }
                }
            }
            if (txtCampaignName_Ins.Text == "" || txtCampaignName_Ins.Text == null)
            {
                flag = false;
                lblCampaignName_Ins.Text = "Please insert Campaign Name";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblCampaignName_Ins.Text = "";
            }
            
            if(ddlCampaingnSpec_Ins.SelectedValue == "-99" || ddlCampaingnSpec_Ins.SelectedValue == null || ddlCampaingnSpec_Ins.SelectedValue == "")
            {
                flag = false;
                lblCampaignSpec_Ins.Text = "Please select status";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblActive_Ins.Text = "";
            }
            if (ddlCampaingnSpec_Ins.SelectedValue == "-99" || ddlCampaingnSpec_Ins.SelectedValue == null || ddlCampaingnSpec_Ins.SelectedValue == "")
            {
                flag = false;
                lblActive_Ins.Text = "Please select status";
            }
            else
            {
                flag = (!flag) ? false : true;
                lblActive_Ins.Text = "";
            }

            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-campaign').modal();", true);
            }
            return flag;
        }
        protected Boolean ValidateCampaignCodeInsert()
        {
            Boolean flagcampcode = true;

            string respstr = "";

            APIpath = APIUrl + "/api/support/CampaignCodeValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = txtCampaignCode_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> lCampaignInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

            if (lCampaignInfo.Count > 0)
            {
                flagcampcode = false;
            }
            else
            {
                flagcampcode = true;
            }
            return flagcampcode;
        }
        protected void bindDdlSearchCampaignCategory()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignCategoryNoPagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCategoryCode"] = "";
                data["MerchantMapCode"] = hidMerchantCodeSess.Value;
                var response = wb.UploadValues(APIpath, "POST", data);
                
                respstr = Encoding.UTF8.GetString(response);
            }
            List<CampaignCategoryInfo> lCampaignCategoryInfo = JsonConvert.DeserializeObject<List<CampaignCategoryInfo>>(respstr);

            
        }

        protected void loadCampaign()
        {
            List<CampaignInfo> lcampaignInfo = new List<CampaignInfo>();
            int? totalRow = CountCampaignMasterList();
            SetPageBar(Convert.ToDouble(totalRow));

            lcampaignInfo = GetCampaignMasterByCriteria();

            

            gvCampaign.DataSource = lcampaignInfo;
            gvCampaign.DataBind();
        }
        public List<CampaignInfo> GetCampaignMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListCampaignPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = txtSearchCampaignCode.Text;
                data["CampaignName"] = txtSearchCampaignName.Text;
              
                data["MerchantMapCode"] = hidMerchantCodeSess.Value;
                data["CampaignSpec"] = ddlSearchCampaignSpec.SelectedValue;
                //fix promotion
                data["CampaignType"] = "PROMOTIONSET";

                

                data["Active"] = ddlSearchCampaignStatus.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> lCampaignInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);
            return lCampaignInfo;
        }
        public int? CountCampaignMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountCampaignListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCode"] = txtSearchCampaignCode.Text;
                data["CampaignName"] = txtSearchCampaignName.Text;
               
                data["MerchantMapCode"] = hidMerchantCodeSess.Value;
                data["CampaignType"] = "PROMOTIONSET";

                

                data["Active"] = ddlSearchCampaignStatus.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected void btnAddCampaign_Click(object sender, EventArgs e)
        {
            hidFlagInsert.Value = "True";

            if(hidFlagInsert.Value == "True")
            {
                txtCampaignCode_Ins.Enabled = true;
            }

            txtCampaignCode_Ins.Text = "";
            txtCampaignName_Ins.Text = "";
          

            lblCampaignCode_Ins.Text = "";
            lblCampaignName_Ins.Text = "";
          

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-campaign').modal();", true);
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
            loadCampaign();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadCampaign();
        }
        protected void chkCampaignAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvCampaign.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvCampaign.HeaderRow.FindControl("chkCampaignAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCampaign.Rows[i].FindControl("hidCampaignId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkCampaign = (CheckBox)gvCampaign.Rows[i].FindControl("chkCampaign");

                    chkCampaign.Checked = true;
                }
                else
                {
                    CheckBox chkCampaign = (CheckBox)gvCampaign.Rows[i].FindControl("chkCampaign");

                    chkCampaign.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected void gvCampaign_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvCampaign.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidCampaignId = (HiddenField)row.FindControl("hidCampaignId");
            HiddenField hidCampaignCode = (HiddenField)row.FindControl("hidCampaignCode");
            HiddenField hidCampaignName = (HiddenField)row.FindControl("hidCampaignName");
            HiddenField hidCampaignCategory = (HiddenField)row.FindControl("hidCampaignCategory");
            HiddenField hidCampaignType = (HiddenField)row.FindControl("hidCampaignType");
            HiddenField hidCampaignSpec = (HiddenField)row.FindControl("hidCampaignSpec");
            HiddenField hidFlagComboSet = (HiddenField)row.FindControl("hidFlagComboSet");
            HiddenField hidFlagShowProductPromotion = (HiddenField)row.FindControl("hidFlagShowProductPromotion");
            HiddenField hidPictureCampaingUrl = (HiddenField)row.FindControl("hidPictureCampaingUrl");
            HiddenField hidActive = (HiddenField)row.FindControl("hidActive");
            HiddenField hidStartDate = (HiddenField)row.FindControl("hidStartDate");
            HiddenField hidNotifyDate = (HiddenField)row.FindControl("hidNotifyDate");
            HiddenField hidExpireDate = (HiddenField)row.FindControl("hidExpireDate");

            if (e.CommandName == "ShowCampaign")
            {
                hidCampaign_Ins.Value = hidCampaignId.Value;
                txtCampaignCode_Ins.Text = hidCampaignCode.Value;
                txtCampaignCode_Ins.Enabled = false;
                txtCampaignName_Ins.Text = hidCampaignName.Value;
                ddlCampaingnSpec_Ins.SelectedValue = hidCampaignSpec.Value;
            
                ddlActive_Ins.SelectedValue = hidActive.Value;
                hidPictureCampaignUrl_Ins.Value = hidPictureCampaingUrl.Value;

                

                var s = hidStartDate.Value;
                var n = hidNotifyDate.Value;
                var p = hidExpireDate.Value;
                var startdate = s.IndexOf(" ") > -1 ? s.Substring(0, s.IndexOf(" ")) : s;
                var notifydate = n.IndexOf(" ") > -1 ? n.Substring(0, n.IndexOf(" ")) : n;
                var expiredate = p.IndexOf(" ") > -1 ? p.Substring(0, p.IndexOf(" ")) : p;

                

                hidFlagInsert.Value = "False";

                loadCampaignImage(hidCampaignCode.Value);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-campaign').modal();", true);
            }
        }
        protected Boolean DeleteProduct()
        {

            for (int i = 0; i < gvCampaign.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvCampaign.Rows[i].FindControl("chkCampaign");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvCampaign.Rows[i].FindControl("hidCampaignId");

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
                APIpath = APIUrl + "/api/support/DeleteCampaign";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["CampaignCode"] = Codelist;

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
        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"CampaignPromotion.aspx?CampaignCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void loadCampaignImage(string CampaignCode)
        {
            CampaignInfo cInfo = new CampaignInfo();
            List<CampaignInfo> lcInfo = new List<CampaignInfo>();
            lcInfo = GetCampaignImage(CampaignCode);

            CampaignPicIm.Src = lcInfo.Count > 0 ? APIUrl + lcInfo[0].PictureCampaignUrl : "";
        }

        protected List<CampaignInfo> GetCampaignImage(string CampaignCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = CampaignCode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<CampaignInfo> lProductInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

            return lProductInfo;
        }
    }
}
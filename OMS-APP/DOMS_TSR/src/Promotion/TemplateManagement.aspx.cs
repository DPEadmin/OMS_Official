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

namespace DOMS_TSR.src.Promotion
{
    public partial class TemplateManagement : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string TemplateImgUrl = ConfigurationManager.AppSettings["UploadPicTemplatePath"];

        string Codelist = "";
        string Showcase_img11Upload_Name = "";
        string Showcase_img43Upload_Name = "";
        string SKUimg1Upload_Name = "";
       

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
               
                LoadTemplate();
                BindchkPlatform();
                BindchkTemplateParam();
                BindddlTemplateType();
               



            }

        }
        protected void BindddlTemplateType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "TEMPLATETYPE";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            ddlTemplateType_Ins.DataSource = lLookupInfo;

            ddlTemplateType_Ins.DataTextField = "LookupValue";

            ddlTemplateType_Ins.DataValueField = "LookupCode";

            ddlTemplateType_Ins.DataBind();

            ddlTemplateType_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            ddlTemplateType_Search.DataSource = lLookupInfo;

            ddlTemplateType_Search.DataTextField = "LookupValue";

            ddlTemplateType_Search.DataValueField = "LookupCode";

            ddlTemplateType_Search.DataBind();

            ddlTemplateType_Search.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            

        }
        protected void BindchkPlatform()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LookupType"] = "PLATFORMTEMPLATE";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LookupInfo> lLookupInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);


            chkPlatform_Search.DataSource = lLookupInfo;

            chkPlatform_Search.DataTextField = "LookupValue";

            chkPlatform_Search.DataValueField = "LookupCode";

            chkPlatform_Search.DataBind();

            chkPlatform_ins.DataSource = lLookupInfo;

            chkPlatform_ins.DataTextField = "LookupValue";

            chkPlatform_ins.DataValueField = "LookupCode";

            chkPlatform_ins.DataBind();



        }
        protected void BindchkTemplateParam()
        {
            List<TemplateParamInfo> lTemplateInfo = GetTemplateParamMasterByCriteria();


            chkTemplateParam.DataSource = lTemplateInfo;

            chkTemplateParam.DataTextField = "TemplateParamName";

            chkTemplateParam.DataValueField = "TemplateParamCode";

            chkTemplateParam.DataBind();


        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadTemplate();
        }


        #region Function
        protected int? loadcountCustomer()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountCustomerListByCriteriaMaster";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["FlagDelete"] = "N";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected void LoadTemplate()
        {
            List<TemplateInfo> lTemplateInfo = new List<TemplateInfo>();

            int? totalRow = CountTemplateMasterList();

            SetPageBar(Convert.ToDouble(totalRow));

            lTemplateInfo = GetTemplateMasterByCriteria();

            gvTemplate.DataSource = lTemplateInfo;

            gvTemplate.DataBind();


        }
        public List<TemplatePlatformInfo> GetTemplatePlatformNopagingByCriteria(string TemplateCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListTemplatePlatformNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["TemplateCode"] = TemplateCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<TemplatePlatformInfo> lTemplateInfo = JsonConvert.DeserializeObject<List<TemplatePlatformInfo>>(respstr);


            return lTemplateInfo;

        }
        public List<TemplateFieldInfo> GetTemplateFieldNopagingByCriteria(string TemplateCode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListTemplateFieldNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["TemplateCode"] = TemplateCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<TemplateFieldInfo> lTemplateInfo = JsonConvert.DeserializeObject<List<TemplateFieldInfo>>(respstr);


            return lTemplateInfo;

        }
        public List<TemplateInfo> GetTemplateMasterByCriteria()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListTemplateListPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["TemplateName"] = txtSearchTemplateName.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<TemplateInfo> lTemplateInfo = JsonConvert.DeserializeObject<List<TemplateInfo>>(respstr);


            return lTemplateInfo;

        }
        public List<TemplateParamInfo> GetTemplateParamMasterByCriteria()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListTemplateParamNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["TemplateParamName"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<TemplateParamInfo> lTemplateInfo = JsonConvert.DeserializeObject<List<TemplateParamInfo>>(respstr);


            return lTemplateInfo;

        }

        public int? CountTemplateMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountTemplateListPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();



                data["TemplateName"] = txtSearchTemplateName.Text;

                data["MerchantCode"] = hidMerchantCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int?  cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }
        public int? CountTemplateforInsert()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountTemplateListPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = hidMerchantCode.Value;

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


            LoadTemplate();
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

        protected Boolean DeleteCustomer()
        {
            InventoryDetailInfoNew ind = new InventoryDetailInfoNew();
            List<InventoryDetailInfoNew> lind = new List<InventoryDetailInfoNew>(); 

            for (int i = 0; i < gvTemplate.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvTemplate.Rows[i].FindControl("chkTemplate");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvTemplate.Rows[i].FindControl("hidTemplateId");
                    HiddenField hidCustomerCode = (HiddenField)gvTemplate.Rows[i].FindControl("hidTemplateCode");

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

                APIpath = APIUrl + "/api/support/DeleteTemplate";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["TempIdforDelete"] = Codelist;


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

        protected void DeleteTemplateById(string pId)
        {

            if (pId != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteTemplate";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["TemplateId"] = pId;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);

                if(cou > 0)
                {

                }
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

        protected Boolean validateInsertUpdate()
        {
            Boolean flag = true;

            List<string> Platform = new List<string>();
           
            foreach (ListItem ListPlatform in chkPlatform_ins.Items)
            {
                if (ListPlatform.Selected)
                {

                    Platform.Add(ListPlatform.Text);
                }
            }
            if (Platform.Count <= 0)
            {
                flag = false;
                lblPlatformTemplate_Ins.Text = MessageConst._MSG_PLEASESELECT + "Platform Template";
            }
            List<string> TemplateParam = new List<string>();
            foreach (ListItem ListParam in chkTemplateParam.Items)
            {
                if (ListParam.Selected)
                {

                    TemplateParam.Add(ListParam.Text);
                }
            }
            if (TemplateParam.Count <= 0)
            {
                flag = false;
                lblTemplateParam.Text = MessageConst._MSG_PLEASESELECT + "Body Attribute";
            }

            if (txtTemplateName_Ins.Text == "" || txtTemplateName_Ins.Text == null)
            {
                flag = false;
                lblTemplateName_Ins.Text = MessageConst._MSG_PLEASEINSERT + "ชื่อ Template";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTemplateType_Ins.Text = "";
            }

            if (ddlTemplateType_Ins.SelectedValue == "-99")
            {
                flag = false;
                lblTemplateType_Ins.Text = MessageConst._MSG_PLEASESELECT + "ชนิด Template";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTemplateType_Ins.Text = "";
            }
            if (ddlActive_ins.SelectedValue == "-99")
            {
                flag = false;
                lblTemplateActive.Text = MessageConst._MSG_PLEASESELECT + "Active";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTemplateActive.Text = "";
            } 
            if (example1.InnerText == "" || example1.InnerText == null)
            {
                flag = false;
                lblTemplateBody.Text = MessageConst._MSG_PLEASEINSERT + "รายละเอียด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTemplateBody.Text = "";
            }


            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Template').modal();", true);
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

            protected void gvCustomer_Change(object sender, GridViewPageEventArgs e)
        {
            gvTemplate.PageIndex = e.NewPageIndex;

            List<TemplateInfo> lTemplateInfo = new List<TemplateInfo>();

            lTemplateInfo = GetTemplateMasterByCriteria();

            gvTemplate.DataSource = lTemplateInfo;

            gvTemplate.DataBind();

        }

        protected void chkTemplateAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvTemplate.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvTemplate.HeaderRow.FindControl("chkTemplateAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvTemplate.Rows[i].FindControl("hidTemplateId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkTemplate = (CheckBox)gvTemplate.Rows[i].FindControl("chkTemplate");

                    chkTemplate.Checked = true;
                }
                else
                {

                    CheckBox chkTemplate = (CheckBox)gvTemplate.Rows[i].FindControl("chkTemplate");

                    chkTemplate.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }
    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadTemplate();
        }
  


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteCustomer();

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
                        int? count = CountTemplateforInsert();
                        count += 1;
                        string TemplateCode = "TEMP" + hidMerchantCode.Value.Substring(0, 3) + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(5, '0');
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

                                hidTemplatePicUrl_Ins.Value = TemplateImgUrl + postedFile.FileName;

                                //Save Images
                                string respstring = "";

                                string APIpath1 = APIUrl + "/api/support/SaveTempPicfromjsonstring64/";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["TemplateCode"] = TemplateCode;
                                    data["TemplateImageUrl"] = TemplateImgUrl + postedFile.FileName;
                                    data["TemplateImageName"] = postedFile.FileName;
                                    data["TemplateImageBase64"] = base64String;
                                    data["FlagDelete"] = "N";

                                    var response = wb.UploadValues(APIpath1, "POST", data);

                                    respstring = Encoding.UTF8.GetString(response);
                                }

                            }

                        }

                        string respstr = "";

                       APIpath = APIUrl + "/api/support/InsertTemplate"; //Insert 

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["TemplateCode"] = TemplateCode;
                            data["TemplateName"] = txtTemplateName_Ins.Text;
                            data["TemplateBody"] = example1.InnerText;
                            data["TemplateType"] = ddlTemplateType_Ins.SelectedValue;
                            data["TemplateImageURL"] = hidTemplatePicUrl_Ins.Value;
                            data["TemplateVideoURL"] = "";
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";
                            data["FlagActive"] = "Y";
                            data["MerchantCode"] = merchantinfo.MerchantCode;

                            var response = wb.UploadValues(APIpath, "POST", data);
                       

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            foreach (ListItem ListPlatform in chkPlatform_ins.Items)
                            {
                                if (ListPlatform.Selected)
                                {
                                    string respstrplatform = "";

                                    APIpath = APIUrl + "/api/support/InsertTemplatePlatform"; //Insert 

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["TemplateCode"] = TemplateCode;
                                        data["TemplatePlatformCode"] = ListPlatform.Value; 
                                        data["CreateBy"] = empInfo.EmpCode;
                                        data["UpdateBy"] = empInfo.EmpCode;
                                        data["FlagDelete"] = "N";

                                        var response = wb.UploadValues(APIpath, "POST", data);


                                        respstrplatform = Encoding.UTF8.GetString(response);
                                    }

                                    int? sumplatform = JsonConvert.DeserializeObject<int?>(respstrplatform);
                                    if (sumplatform == 0)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "TemplatePlatform" + "');", true);
                                    } 
                                   

                                }
                            }
                            int seqparam = 0;
                            foreach (ListItem ListParam in chkTemplateParam.Items)
                            {
                                seqparam++;
                                if (ListParam.Selected)
                                {
                                    string respstrparam = "";

                                    APIpath = APIUrl + "/api/support/InsertTemplateField"; //Insert 

                                    using (var wb = new WebClient())
                                    {
                                        var data = new NameValueCollection();

                                        data["TemplateCode"] = TemplateCode;
                                        data["TemplateParamCode"] = ListParam.Value;
                                        data["TemplateFieldParamSeq"] = seqparam.ToString();
                                       

                                        var response = wb.UploadValues(APIpath, "POST", data);


                                        respstrparam = Encoding.UTF8.GetString(response);
                                    }

                                    int? sumparam = JsonConvert.DeserializeObject<int?>(respstrparam);

                                    if (sumparam == 0)
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "TemplateParam" + "');", true);
                                    }
                                }
                            }
                            btnCancel_Click(null, null);

                                LoadTemplate();

                                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Template').modal('hide');", true);
                          

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateCustomer";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["CustomerId"] = hidIdList.Value;
                            data["MerchantCode"] = merchantinfo.MerchantCode;
                     
                            data["FlagDelete"] = "N";
                            data["UpdateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadTemplate();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Template').modal('hide');", true);



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
        protected void chkTemplateParam_Check(object sender, EventArgs e)
        {

            
            foreach (ListItem aListItem in chkTemplateParam.Items)
            {
                if (aListItem.Selected)
                {
                    example1.InnerText = example1.InnerText.Insert(0, "{"+aListItem.Text+"}");
                    
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myArea", "$('#example1').emojioneArea({ autoHideFilters: true });", true);
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chkPlatform_ins.Items.Count; i++)
            {
                chkPlatform_ins.Items[i].Selected = false;
            }
            for (int i = 0; i < chkTemplateParam.Items.Count; i++)
            {
                chkTemplateParam.Items[i].Selected = false;
            }
            
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
          
            txtSearchTemplateName.Text = "";
           

        }

        protected void gvTemplate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvTemplate.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidTemplateId = (HiddenField)row.FindControl("hidTemplateId");
            HiddenField hidTemplateCode = (HiddenField)row.FindControl("hidTemplateCode");
           
            HiddenField hidTemplateName = (HiddenField)row.FindControl("hidTemplateName");
            HiddenField hidTemplateType = (HiddenField)row.FindControl("hidTemplateType");
            HiddenField hidTemplateBody = (HiddenField)row.FindControl("hidTemplateBody"); 
            HiddenField hidTemplateImageURL = (HiddenField)row.FindControl("hidTemplateImageURL");
            HiddenField hidFlagActive = (HiddenField)row.FindControl("hidFlagActive");

            txtTemplateName_Ins.Text = hidTemplateName.Value;
            
            ddlActive_ins.SelectedValue = (hidFlagActive.Value.Trim() == null || hidFlagActive.Value.Trim() == "") ? hidFlagActive.Value = "-99" : hidFlagActive.Value.Trim();

            ddlTemplateType_Ins.SelectedValue = (hidTemplateType.Value == null || hidTemplateType.Value == "") ? hidTemplateType.Value = "-99" : hidTemplateType.Value; ;
            example1.InnerText = hidTemplateBody.Value;
            if (e.CommandName == "ShowTemplate")
            {
                List<TemplatePlatformInfo> lTemplateplatformInfo = new List<TemplatePlatformInfo>();
                lTemplateplatformInfo = GetTemplatePlatformNopagingByCriteria(hidTemplateCode.Value);
                for (int i = 0; i < chkPlatform_ins.Items.Count; i++)
                {
                    foreach (var platform in lTemplateplatformInfo)
                    {
                        if (chkPlatform_ins.Items[i].Value == platform.TemplatePlatformCode)
                        {
                            chkPlatform_ins.Items[i].Selected = true;
                        }
                    }
                }

                List<TemplateFieldInfo> lTemplatefieldInfo = new List<TemplateFieldInfo>();
                lTemplatefieldInfo = GetTemplateFieldNopagingByCriteria(hidTemplateCode.Value);
               
                for (int i = 0; i < chkTemplateParam.Items.Count; i++)
                {
                    foreach (var temp in lTemplatefieldInfo)
                    {
                        if (chkTemplateParam.Items[i].Value == temp.TemplateParamCode)
                        {
                            chkTemplateParam.Items[i].Selected = true;
                        }
                    }
                }
                chkTemplateParam.Items[1].Selected = true;
                hidIdList.Value = hidTemplateId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Template').modal();", true);


            }

            if (e.CommandName == "DeleteTemplate")
            {
                DeleteTemplateById(hidTemplateId.Value);
                btnSearch_Click(null, null);
            }

        }

        protected void btnAddTemplate_Click(object sender, EventArgs e)
        {
            btnCancel_Click(null, null);
            hidFlagInsert.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myArea", "$('#example1').emojioneArea({ autoHideFilters: true });", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Template').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ReportCustomerPoint.aspx?CustomerCode=" + strCode + "\">" + strCode + "</a>";
        }

        protected List<PointInfo> ListPointRangeNopaging()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListPointRangePagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantMapCode"] = hidMerchantCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PointInfo> lPointInfo = JsonConvert.DeserializeObject<List<PointInfo>>(respstr);
            return lPointInfo;
        }
      
        

       
        #endregion
    }
}
using Newtonsoft.Json;
using SALEORDER.Common;
using SALEORDER.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace DOMS_TSR.src.MerchantManagement
{
    public partial class MerchantManagement : System.Web.UI.Page
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
                MerchantInfo merInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                merInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null && merInfo != null)
                {

                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merInfo.MerchantCode;

                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                
                BindddlProvince();
                BindddlDistrict();
                BindddlSubDistrict();
                loadMerchant();

            }
        }

        #region event
        protected void btnAddMerchant_Click(object sender, EventArgs e) //ยังไม่ได้ add จังหวัด
        {

            txtMerCodeIns.Enabled = true;
            hidFlagInsert.Value = "True";

            txtMerCodeIns.Text = "";
            txtMerNameIns.Text = "";
            //ddlMerTIns.ClearSelection();    ปิดตามหน้าจอที่ไม่ส่งพารามิเตอร์มา
            ddlProvinceIns.ClearSelection();
            ddlDistrictIns.ClearSelection();
            ddlSubDistrictIns.ClearSelection();
            txtTaxIns.Text = "";
            txtAddressIns.Text = "";
            txtPostCodeIns.Text = "";
            txtMobileIns.Text = "";
            txtFaxIns.Text = "";
            txtEmailIns.Text = "";

            lblMerCodeIns.Text = "";
            lblAddressIns.Text = "";
            lblTaxIns.Text = "";
            lblProvinceIns.Text = "";
            lblDistrictIns.Text = "";
            lblSubDistrictIns.Text = "";
            lblPostCodeIns.Text = "";
            lblMerNameIns.Text = "";
            //lblMerTIns.Text = "";   ปิดตามหน้าจอที่ไม่ส่งพารามิเตอร์มา
            
            lblMobileIns.Text = "";
            lblFaxIns.Text = "";
            lblEmailIns.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-mer').modal({\r\nbackdrop: 'static',\r\nkeyboard: false\r\n});", true);
        }

       


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            MerchantInfo merchantInfo = new MerchantInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            merchantInfo = (MerchantInfo)Session["MerchantInfo"];

            if (empInfo == null && merchantInfo == null)
            {

                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (ValidateInsertUpdate())
                {
                    if (hidFlagInsert.Value == "True")
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

                                hidPictureMerchantURL_Ins.Value = CampaignImgUrl + postedFile.FileName;
                                hidFileName.Value = postedFile.FileName;

                                //Save Images
                                string respstring = "";
                                string APIpath1 = APIUrl + "/api/support/SaveCampaignpicfromjsonstring64";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["CampaignCodew"] = txtMerCodeIns.Text;
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

                        APIpath = APIUrl + "/api/support/InsertMerchant";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["MerchantCode"] = txtMerCodeIns.Text.Trim();
                            data["MerchantName"] = txtMerNameIns.Text.Trim();
                            data["MerchantType"] = "";//ddlMerTIns.SelectedValue;    ปิดตามหน้าจอที่ไม่ส่งพารามิเตอร์มา
                            data["CompanyCode"] = "ENTERPRISE";
                            data["TaxId"] = txtTaxIns.Text.Trim();
                            data["Address"] = txtAddressIns.Text.Trim();
                            data["ProvinceCode"] = ddlProvinceIns.SelectedValue;
                            data["DistrictCode"] = ddlDistrictIns.SelectedValue;
                            data["SubDistrictCode"] = ddlSubDistrictIns.SelectedValue;
                            data["ZipCode"] = txtPostCodeIns.Text.Trim();
                            data["ContactTel"] = txtMobileIns.Text.Trim();
                            data["FaxNum"] = txtFaxIns.Text.Trim();
                            data["Email"] = txtEmailIns.Text.Trim();
                            data["PictureMerchantURL"] = hidPictureMerchantURL_Ins.Value;

                            data["FlagDelete"] = "N";
                            data["ActiveFlag"] = "Y";

                            data["CreateBy"] = empInfo.EmpCode;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                                                        
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            loadMerchant();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-mer').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //update
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

                                hidPictureMerchantURL_Ins.Value = CampaignImgUrl + postedFile.FileName;
                                hidFileName.Value = postedFile.FileName;

                                //Save Images
                                string respstring = "";
                                string APIpath1 = APIUrl + "/api/support/SaveCampaignpicfromjsonstring64";
                                using (var wb = new WebClient())
                                {
                                    var data = new NameValueCollection();

                                    data["CampaignCodew"] = txtMerCodeIns.Text;
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

                        APIpath = APIUrl + "/api/support/UpdateMerchant";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["MerchantId"] = hidMerIdIns.Value;
                            data["MerchantCode"] = txtMerCodeIns.Text.Trim();
                            data["MerchantName"] = txtMerNameIns.Text.Trim();
                            data["CompanyCode"] = "ENTERPRISE";
                            data["TaxId"] = txtTaxIns.Text.Trim();
                            data["Address"] = txtAddressIns.Text.Trim();
                            data["ProvinceCode"] = ddlProvinceIns.SelectedValue;
                            data["DistrictCode"] = ddlDistrictIns.SelectedValue;
                            data["SubDistrictCode"] = ddlSubDistrictIns.SelectedValue;
                            data["ZipCode"] = txtPostCodeIns.Text.Trim();
                            data["ContactTel"] = txtMobileIns.Text.Trim();
                            data["FaxNum"] = txtFaxIns.Text.Trim();
                            data["Email"] = txtEmailIns.Text.Trim();

                            if (hidFileName.Value == null || hidFileName.Value == "")
                            {

                            }
                            else
                            {
                                data["PictureMerchantURL"] = hidPictureMerchantURL_Ins.Value;
                            }

                            data["FlagDelete"] = "N";
                            data["ActiveFlag"] = "Y";

                            data["Updateby"] = empInfo.EmpCode;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            loadMerchant();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-mer').modal('hide');", true);


                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteMerchant();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            
            txtMerNameIns.Text = "";
            
            txtMobileIns.Text = "";
            txtMerCodeIns.Text = "";
            txtTaxIns.Text = "";
            txtAddressIns.Text = "";
            ddlProvinceIns.ClearSelection();
            ddlDistrictIns.ClearSelection();
            ddlSubDistrictIns.ClearSelection();
            txtPostCodeIns.Text = "";
            txtMobileIns.Text = "";
            txtFaxIns.Text = "";
            txtEmailIns.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
                currentPageNumber = 1;
                loadMerchant();
            }
           
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchMerCode.Text = "";
            txtSearchMerName.Text = "";
        }

        protected void chkMerAll_Changed(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMer.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvMer.HeaderRow.FindControl("chkMerAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvMer.Rows[i].FindControl("hidMerId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkEmp = (CheckBox)gvMer.Rows[i].FindControl("chkMer");

                    chkEmp.Checked = true;
                }
                else
                {
                    CheckBox chkEmp = (CheckBox)gvMer.Rows[i].FindControl("chkMer");

                    chkEmp.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        
        }

        protected void gvMer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvMer.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");
            Label lblMerCode = (Label)row.FindControl("lblMerCode");
            Label lblPhone = (Label)row.FindControl("lblMobile");
            Label lblEMail = (Label)row.FindControl("lblMail");

            HiddenField hidMerId = (HiddenField)row.FindControl("hidMerId");
            HiddenField hidMerCode = (HiddenField)row.FindControl("hidMerCode");
            HiddenField hidMerName = (HiddenField)row.FindControl("hidMerName");
            HiddenField hidMerT = (HiddenField)row.FindControl("hidMerT");
            HiddenField hidComCode = (HiddenField)row.FindControl("hidComCode");
            HiddenField hidTaxId = (HiddenField)row.FindControl("hidTaxId");
            HiddenField hidAddress = (HiddenField)row.FindControl("hidAddress");
            HiddenField hidProvince = (HiddenField)row.FindControl("hidProvince");
            HiddenField hidDistrict = (HiddenField)row.FindControl("hidDistrict");
            HiddenField hidSubDistrict = (HiddenField)row.FindControl("hidSubDistrict");
            HiddenField hidPhone = (HiddenField)row.FindControl("hidPhone");
            HiddenField hidZipCode = (HiddenField)row.FindControl("hidZipCode");
            HiddenField hidFax = (HiddenField)row.FindControl("hidFax");
            HiddenField hidEmail = (HiddenField)row.FindControl("hidEmail");
            HiddenField hidPictureMerchantURL = (HiddenField)row.FindControl("hidPictureMerchantURL");

            if (e.CommandName == "ShowMer")
            {
                hidMerIdIns.Value = hidMerId.Value;
                txtMerCodeIns.Text = hidMerCode.Value;
                txtMerCodeIns.Enabled = false;
                txtMerNameIns.Text = hidMerName.Value;
                //BindddlSearchMerT(ddlSearchMerT);   ปิดตามหน้าจอที่ไม่ส่งพารามิเตอร์มา
                
                
                txtTaxIns.Text = hidTaxId.Value;
                txtAddressIns.Text = hidAddress.Value;
                BindddlProvince();
                ddlProvinceIns.SelectedValue = (hidProvince.Value == null || hidProvince.Value == "") ? hidProvince.Value = "-99" : hidProvince.Value;
                BindddlDistrict();
                ddlDistrictIns.SelectedValue = (hidDistrict.Value == null || hidDistrict.Value == "") ? hidDistrict.Value = "-99" : hidDistrict.Value;
                BindddlSubDistrict();
                ddlSubDistrictIns.SelectedValue = (hidSubDistrict.Value == null || hidSubDistrict.Value == "") ? hidSubDistrict.Value = "-99" : hidSubDistrict.Value;
                txtPostCodeIns.Text = hidZipCode.Value;
                txtFaxIns.Text = hidFax.Value;
                txtEmailIns.Text = lblEMail.Text;
                txtMobileIns.Text = lblPhone.Text;
                hidPictureMerchantURL_Ins.Value = hidPictureMerchantURL.Value;

                if (hidPictureMerchantURL.Value != null && hidPictureMerchantURL.Value != "")
                {
                    MerchantPicIm.Src = APIUrl + hidPictureMerchantURL.Value;
                }
                else
                {
                    MerchantPicIm.Src = "";
                }

                lblMerCodeIns.Text = "";
                lblMerNameIns.Text = "";
                //lblComCodeIns.Text = "";   ปิดตามหน้าจอที่ไม่ส่งพารามิเตอร์มา
                
                lblAddressIns.Text = "";
                lblTaxIns.Text = "";
                lblProvinceIns.Text = "";
                lblDistrictIns.Text = "";
                lblSubDistrictIns.Text = "";
                lblPostCodeIns.Text = "";
                lblMobileIns.Text = "";
                lblFaxIns.Text = "";

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-mer').modal({\r\nbackdrop: 'static',\r\nkeyboard: false\r\n});", true);
            }
        }
        #endregion

        #region Function

        protected Boolean ValidateInsertUpdate()
        {
            Boolean flag = true;

            var regexNumber = new Regex("^[0-9 ]*$");
            var regexMail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",RegexOptions.CultureInvariant | RegexOptions.Singleline);

            if (txtMerCodeIns.Text == "")
            {
                flag = false;
                lblMerCodeIns.Text = MessageConst._MSG_PLEASEINSERT + " รหัสร้านค้า";
            }
            else if (CheckSymbol(txtMerCodeIns.Text) == true)
            {
                flag = false;
                lblMerCodeIns.Text = MessageConst._MSG_PLEASEINSERT + " รหัสร้านค้าต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    
                        List<MerchantInfo> lmer = new List<MerchantInfo>();

                        lmer = GetMerValidate();

                        if (lmer.Count > 0)
                        {
                            flag = false;
                            lblMerCodeIns.Text = "ไม่สามารถระบุรหัสร้านค้าซ้ำได้";
                        }
                        else
                        {
                            flag = (flag == false) ? false : true; //ถาม
                            lblMerCodeIns.Text = "";
                        }
                    
                }
                else
                {

                    flag = (flag == false) ? false : true;
                    lblMerCodeIns.Text = "";
                }
                
            }



            if (txtMerNameIns.Text == "")
            {
                flag = false;
                lblMerNameIns.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อร้าน";
            }
            else if (CheckSymbol(txtMerNameIns.Text) == true)
            {
                flag = false;
                lblMerNameIns.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อร้านต้องไม่มีอักขระพิเศษ";
            }else
            {
                flag = (flag == false) ? false : true;
                lblMerNameIns.Text = "";
            }

            if (txtMobileIns.Text == "")
            {
                flag = false;
                lblMobileIns.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรศัพท์";
            }
            else if (regexNumber.IsMatch(txtMobileIns.Text) == false)
            {
                flag = false;
                lblMobileIns.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรศัพท์ให้ถูกต้อง";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMobileIns.Text = "";
            }
            if (CheckSymbol(txtTaxIns.Text) == true)
            {
                flag = false;
                lblTaxIns.Text = MessageConst._MSG_PLEASEINSERT + " เลขที่เสียภาษีต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTaxIns.Text = "";
            }
            if (CheckSymbol(txtFaxIns.Text) == true)
            {
                flag = false;
                lblFaxIns.Text = MessageConst._MSG_PLEASEINSERT + " แฟกซ์ต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblFaxIns.Text = "";
            }
            if (txtEmailIns.Text == "")
            {
                flag = (flag == false) ? false : true;
                lblEmailIns.Text = "";
            }
            else if (regexMail.IsMatch(txtEmailIns.Text) == false)
            {
                flag = false;
                lblEmailIns.Text = MessageConst._MSG_PLEASEINSERT + " อีเมลต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEmailIns.Text = "";
            }


            

            return flag;
        }

        protected List<MerchantInfo> GetMerValidate()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMerchantValidatePagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = txtMerCodeIns.Text;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lmerInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);
            List<MerchantInfo> MerVal = new List<MerchantInfo>();
            
            MerVal = lmerInfo.Where(x => x.MerchantCode == txtMerCodeIns.Text).ToList();

            return MerVal;
        }

        protected void loadMerchant()
        {
            List<MerchantInfo> lMerInfo = new List<MerchantInfo>();

            int? totalRow = CountMerList();

            SetPageBar(Convert.ToDouble(totalRow));

            lMerInfo = GetMerchantByCriteria();
            if (lMerInfo.Count > 0)
            {
                foreach (var mer in lMerInfo)
                {
                    if (mer.ActiveFlag == "Y")
                    {
                        mer.ActiveFlagName = "Active";
                    }
                    else
                    {
                        mer.ActiveFlagName = "Inactive";
                    }
                }
            }
            gvMer.DataSource = lMerInfo;
            gvMer.DataBind();
        }

        protected Boolean DeleteMerchant()
        {
            for (int i = 0; i < gvMer.Rows.Count; i++)
            {
                CheckBox checkBox = (CheckBox)gvMer.Rows[i].FindControl("chkMer");

                if (checkBox.Checked == true)
                {
                    HiddenField hidecode = (HiddenField)gvMer.Rows[i].FindControl("hidMerId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidecode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidecode.Value + "'";
                    }
                }
            }

            if (Codelist != "")
            {
                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteMerchant";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["MerchantCode"] = Codelist;

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

        protected int? CountMerList() //ต้องมาเพิ่ม companycode ที่หลัง
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountMerchantListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = txtSearchMerCode.Text;
                data["MerchantName"] = txtSearchMerName.Text;
                data["FlagDelete"] = "N";
                data["ActiveFlag"] = "Y";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"MerchantDetail.aspx?MerchantCode=" + strCode + "\">" + strCode + "</a>";
        }

        protected List<MerchantInfo> GetMerchantByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/MerchantListPagingCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = txtSearchMerCode.Text;
                data["MerchantName"] = txtSearchMerName.Text;
                data["MerchantType"] = "";//ddlSearchMerT.SelectedValue;  ปิดตามหน้าจอที่ไม่ส่งพารามิเตอร์มา
                data["CompanyCode"] = "";
                data["ActiveFlag"] = "Y";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lMerInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);
            return lMerInfo;
        }


        #endregion

        #region Bind

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
            loadMerchant();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadMerchant();
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlDistrict();
            BindddlSubDistrict();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindddlSubDistrict();
        }

        

        protected void BindddlSearchComCode(DropDownList ddlSearchComCode)
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

            List<CompanyInfo> lCompanyInfo = JsonConvert.DeserializeObject<List<CompanyInfo>>(respstr);

            ddlSearchComCode.DataSource = lCompanyInfo;

            ddlSearchComCode.DataTextField = "CompanyNameTH";
            ddlSearchComCode.DataValueField = "CompanyCode";
            ddlSearchComCode.DataBind();
            ddlSearchComCode.Items.Insert(0, new ListItem("Please Select", "-99"));
        }

        

        protected void BindddlProvince()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProvinceNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProvinceInfo> lProvinceInfo = JsonConvert.DeserializeObject<List<ProvinceInfo>>(respstr);


            ddlProvinceIns.DataSource = lProvinceInfo;

            ddlProvinceIns.DataTextField = "ProvinceName";

            ddlProvinceIns.DataValueField = "ProvinceCode";

            ddlProvinceIns.DataBind();

            ddlProvinceIns.Items.Insert(0, new ListItem("Please Select", "-99"));

        }

        protected void BindddlDistrict()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDistrictNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProvinceCode"] = ddlProvinceIns.SelectedValue;
                data["DistrictCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DistrictInfo> lDistrictInfo = JsonConvert.DeserializeObject<List<DistrictInfo>>(respstr);


            ddlDistrictIns.DataSource = lDistrictInfo;

            ddlDistrictIns.DataTextField = "DistrictName";

            ddlDistrictIns.DataValueField = "DistrictCode";

            ddlDistrictIns.DataBind();

            ddlDistrictIns.Items.Insert(0, new ListItem("Please Select", "-99"));

        }

        protected void BindddlSubDistrict()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSubDistrictNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["DistrictCode"] = ddlDistrictIns.SelectedValue;
                data["SubDistrictCode"] = "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SubDistrictInfo> lSubDistrictInfo = JsonConvert.DeserializeObject<List<SubDistrictInfo>>(respstr);


            ddlSubDistrictIns.DataSource = lSubDistrictInfo;

            ddlSubDistrictIns.DataTextField = "SubDistrictName";

            ddlSubDistrictIns.DataValueField = "SubDistrictCode";

            ddlSubDistrictIns.DataBind();

            ddlSubDistrictIns.Items.Insert(0, new ListItem("Please Select", "-99"));

        }

        #endregion
        private bool CheckSymbol(string value)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (value.Contains(item)) return true;
            }

            return false;
        }
        protected Boolean validateSearch()
        {
            Boolean flag = true;

            if (CheckSymbol(txtSearchMerCode.Text) )
            {
                flag = false;
                lblSearchMerCode.Text = MessageConst._MSG_PLEASEINSERT + " รหัสร้านค้าต้องไม่มีอักขระพิเศษ";
         
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSearchMerCode.Text = "";
            }
            if (CheckSymbol(txtSearchMerName.Text))
            {
                flag = false;
                lblSearchMerName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อร้านค้าต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSearchMerName.Text = "";
            }
            return flag;
        }

    }
}
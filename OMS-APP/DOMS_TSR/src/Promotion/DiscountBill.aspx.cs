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

using System.IO;

namespace DOMS_TSR.src.Promotion
{
    public partial class DiscountBill : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string DiscountBillImgUrl = ConfigurationManager.AppSettings["DiscountBillImageUrl"];

        string Idlist = "";
        string Codelist = "";
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
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                loadDiscountBill();
                BindddlSearchProductBrand();
                BindddlProductBrand();
                BindddlSearchDiscountBillType();
                BindddlSearchDiscountBillType_Ins();
                ddlCombosetFlag_Ins_SelectedIndexChanged(null, null);
                ShowComboSection("N");
                bindDDldiscountBill_Status();
                
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadDiscountBill();
        }

        #region Function



        protected void loadDiscountBill()
        {
            List<DiscountBillInfo> lDiscountBillInfo = new List<DiscountBillInfo>();

            

            int? totalRow = CountDiscountBillMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lDiscountBillInfo = GetDiscountBillMasterByCriteria();

            gvDiscountBill.DataSource = lDiscountBillInfo;

            gvDiscountBill.DataBind();


            

        }

        public List<DiscountBillInfo> GetDiscountBillMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDiscountBillList";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["BrandCode"] = ddlSearchProductBrand.SelectedValue;
                data["DiscountBillTypeCode"] = ddlSearchDiscountBillType.SelectedValue;
                data["DiscountBillCode"] = txtSearchDiscountBillCode.Text;
                data["DiscountBillName"] = txtSearchDiscountBillName.Text;
                data["StartDate"] = txtSearchStartDateFrom.Text;
                data["EndDate"] = txtSearchEndDateFrom.Text;
                data["Active"] = ddlDiscountBill_Status.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();
                data["MerchantCode"] = hidMerchantCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<DiscountBillInfo> lDiscountBillInfo = JsonConvert.DeserializeObject<List<DiscountBillInfo>>(respstr);


            return lDiscountBillInfo;

        }

        public int? CountDiscountBillMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListDiscountBill";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["BrandCode"] = ddlSearchProductBrand.SelectedValue;
                data["DiscountBillTypeCode"] = ddlSearchDiscountBillType.SelectedValue;
                data["DiscountBillCode"] = txtSearchDiscountBillCode.Text;
                data["DiscountBillName"] = txtSearchDiscountBillName.Text;
                data["StartDate"] = txtSearchStartDateFrom.Text;
                data["Active"] = ddlDiscountBill_Status.SelectedValue;
                data["EndDate"] = txtSearchEndDateFrom.Text;
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


            loadDiscountBill();
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

        protected Boolean DeleteDiscountBill()
        {

            for (int i = 0; i < gvDiscountBill.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvDiscountBill.Rows[i].FindControl("chkDiscountBill");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvDiscountBill.Rows[i].FindControl("hidDiscountBillId");
                    HiddenField hidCode = (HiddenField)gvDiscountBill.Rows[i].FindControl("hidDiscountBillCode");

                    if (Idlist != "")
                    {
                        Idlist += "," + hidId.Value + "";
                    }
                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Idlist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteDiscountBill";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["DiscountBillCode"] = Idlist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePromtoionDetailInfoByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["DiscountBillCode"] = Codelist;


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

        protected void gvDiscountBill_Change(object sender, GridViewPageEventArgs e)
        {
            gvDiscountBill.PageIndex = e.NewPageIndex;

            List<DiscountBillInfo> lDiscountBillInfo = new List<DiscountBillInfo>();

            lDiscountBillInfo = GetDiscountBillMasterByCriteria();

            gvDiscountBill.DataSource = lDiscountBillInfo;

            gvDiscountBill.DataBind();

        }
        protected void chkview_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox cb1 = (CheckBox)gvDiscountBill.Rows[index].FindControl("chkview");
            string yourvalue = cb1.Text;
            //here you can find your control and get value(Id).

        }
        protected void chkDiscountBillAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvDiscountBill.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvDiscountBill.HeaderRow.FindControl("chkDiscountBillAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvDiscountBill.Rows[i].FindControl("hidDiscountBillId");
                    HiddenField hidCode = (HiddenField)gvDiscountBill.Rows[i].FindControl("hidDiscountBillCode");

                    if (Idlist != "")
                    {
                        Idlist += ",'" + hidId.Value + "'";
                    }
                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Idlist += "'" + hidId.Value + "'";
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkDiscountBill = (CheckBox)gvDiscountBill.Rows[i].FindControl("chkDiscountBill");

                    chkDiscountBill.Checked = true;
                }
                else
                {

                    CheckBox chkDiscountBill = (CheckBox)gvDiscountBill.Rows[i].FindControl("chkDiscountBill");

                    chkDiscountBill.Checked = false;
                }

            }
            hidIdList.Value = Idlist;
            hidCodeList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadDiscountBill();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteDiscountBill();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            EmpInfo empInfo = new EmpInfo();
            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];
            POInfo pInfo = new POInfo();

         
            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (validateInsert())
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertDiscountBill";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["DiscountBillCode"] = txtDiscountBillCode_Ins.Text;
                            data["DiscountBillName"] = txtDiscountBillName_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["FreeShipping"] = ddlFreeShipFlag_Ins.SelectedValue;
                            data["DiscountPercent"] = (txtDiscountPercent_Ins.Text != "") ? txtDiscountPercent_Ins.Text : "0"; 
                            data["DiscountAmount"] = (txtDiscountAmount_Ins.Text != "") ? txtDiscountAmount_Ins.Text : "0";
                            data["MinimumTotPrice"] = (txtMinimumTotPrice_Ins.Text != "") ? txtMinimumTotPrice_Ins.Text : "0"; ;
                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["BrandCode"] = ddlProductBrand_Ins.SelectedValue;
                            data["MerchantCode"] = merchantinfo.MerchantCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["DiscountBillTypeCode"] = ddlDiscountBillType_Ins.SelectedValue;
                            data["Active"] = ddlDiscountBill_Status_Ins.SelectedValue;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            loadDiscountBill();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-DiscountBill').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    else //Update
                    {
                      

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateDiscountBill";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["DiscountBillId"] = hidIdList.Value;

                            data["DiscountBillCode"] = txtDiscountBillCode_Ins.Text;
                            data["DiscountBillName"] = txtDiscountBillName_Ins.Text;

                            data["FlagDelete"] = "N";
                            data["Active"] = ddlDiscountBill_Status_Ins.SelectedValue;
                            data["FreeShipping"] = ddlFreeShipFlag_Ins.SelectedValue;

                            data["DiscountPercent"] = (txtDiscountPercent_Ins.Text != "") ? txtDiscountPercent_Ins.Text : "0";
                            data["DiscountAmount"] = (txtDiscountAmount_Ins.Text != "") ? txtDiscountAmount_Ins.Text : "0";
                            data["MinimumTotPrice"] = (txtMinimumTotPrice_Ins.Text != "") ? txtMinimumTotPrice_Ins.Text : "0"; ;
                            data["CreateBy"] = empInfo.EmpCode;                         
                            data["StartDate"] = txtStartDate_Ins.Text;
                            data["EndDate"] = txtEndDate_Ins.Text;
                            data["BrandCode"] = ddlProductBrand_Ins.SelectedValue;
                            data["DiscountBillTypeCode"] = ddlDiscountBillType_Ins.SelectedValue;

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            

                            loadDiscountBill();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-DiscountBill').modal('hide');", true);
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtDiscountBillCode_Ins.Text = "";
            txtDiscountBillName_Ins.Text = "";
          
            txtDiscountAmount_Ins.Text = "0";
            txtDiscountPercent_Ins.Text = "0";
            txtMinimumTotPrice_Ins.Text = "0";
        
            ddlCombosetFlag_Ins_SelectedIndexChanged(null, null);

            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-DiscountBill').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchDiscountBillCode.Text = "";
            txtSearchDiscountBillName.Text = "";
            ddlSearchProductBrand.SelectedValue = "-99";

            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
            txtSearchEndDateFrom.Text = "";
            txtSearchEndDateTo.Text = "";
        }
        protected void gvDiscountBill_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                    HiddenField HidFlagAddProduct = (HiddenField)e.Row.FindControl("HidFlagAddProduct");
                    LinkButton lnkDownload = (LinkButton)e.Row.FindControl("lnkDownload");
                    HiddenField hidDiscountBillCode = (HiddenField)e.Row.FindControl("hidDiscountBillCode");
                    
                    
                
            }
        }
        protected void gvDiscountBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvDiscountBill.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidDiscountBillId = (HiddenField)row.FindControl("hidDiscountBillId");
            HiddenField hidDiscountBillCode = (HiddenField)row.FindControl("hidDiscountBillCode");
            HiddenField hidDiscountBillName = (HiddenField)row.FindControl("hidDiscountBillName");
            HiddenField hidDiscountBillDesc = (HiddenField)row.FindControl("hidDiscountBillDesc");
            HiddenField hidDiscountBillLevel = (HiddenField)row.FindControl("hidDiscountBillLevel");

            HiddenField hidDiscountBillType = (HiddenField)row.FindControl("hidDiscountBillType");

            HiddenField hidFreeShipping = (HiddenField)row.FindControl("hidFreeShipping");
            HiddenField hidStatus = (HiddenField)row.FindControl("hidStatus");

            HiddenField hidDiscountPercent = (HiddenField)row.FindControl("hidDiscountPercent");
            HiddenField hidDiscountAmount = (HiddenField)row.FindControl("hidDiscountAmount");
            HiddenField hidProductDiscountPercent = (HiddenField)row.FindControl("hidProductDiscountPercent");
            HiddenField hidProductDiscountAmount = (HiddenField)row.FindControl("hidProductDiscountAmount");

            HiddenField hidMOQFlag = (HiddenField)row.FindControl("hidMOQFlag");
            HiddenField hidMinimumQty = (HiddenField)row.FindControl("hidMinimumQty");
            HiddenField hidGroupPrice = (HiddenField)row.FindControl("hidGroupPrice");

            HiddenField hidLockCheckbox = (HiddenField)row.FindControl("hidLockCheckbox");
            HiddenField hidLockAmountFlag = (HiddenField)row.FindControl("hidLockAmountFlag");
            HiddenField hidMinimumTotPrice = (HiddenField)row.FindControl("hidMinimumTotPrice");
            HiddenField hidRedeemFlag = (HiddenField)row.FindControl("hidRedeemFlag");
            HiddenField hidComplementaryFlag = (HiddenField)row.FindControl("hidComplementaryFlag");
            HiddenField hidPictureDiscountBillUrl = (HiddenField)row.FindControl("hidPictureDiscountBillUrl");
            HiddenField hidCombosetFlag = (HiddenField)row.FindControl("hidCombosetFlag");
            HiddenField hidCombosetName = (HiddenField)row.FindControl("hidCombosetName");
            HiddenField hidStartDate = (HiddenField)row.FindControl("hidStartDate");
            HiddenField hidEndDate = (HiddenField)row.FindControl("hidEndDate");
            HiddenField hidProductBrandCode = (HiddenField)row.FindControl("hidProductBrandCode");
            HiddenField hidActive = (HiddenField)row.FindControl("hidActive");


            if (e.CommandName == "ShowDiscountBill")
            {
                bindDDldiscountBillStatus_Ins();
                txtDiscountBillCode_Ins.Text = hidDiscountBillCode.Value;
                txtDiscountBillCode_Ins.Enabled = true;
                hidDiscountBillCode_Ins.Value = hidDiscountBillCode.Value;
                txtDiscountBillName_Ins.Text = hidDiscountBillName.Value;
                ddlProductBrand_Ins.SelectedValue = (hidProductBrandCode.Value == null || hidProductBrandCode.Value == "") ? hidProductBrandCode.Value = "-99" : hidProductBrandCode.Value;
                ddlFreeShipFlag_Ins.SelectedValue = (hidFreeShipping.Value == null || hidFreeShipping.Value == "") ? hidFreeShipping.Value = "-99" : hidFreeShipping.Value.Trim();
                txtDiscountBillCode_Ins.Enabled = false;

                if ( Convert.ToInt32(hidDiscountAmount.Value) > 0 )
                {
                    txtDiscountAmount_Ins.Text = hidDiscountAmount.Value;
                    txtDiscountPercent_Ins.Enabled = false;
                }
                else
                {
                    txtDiscountPercent_Ins.Enabled = true;
                }

                if (Convert.ToInt32(hidDiscountPercent.Value) > 0)
                {
                    txtDiscountPercent_Ins.Text = hidDiscountAmount.Value;
                    txtDiscountAmount_Ins.Enabled = false;
                }
                else
                {
                    txtDiscountAmount_Ins.Enabled = true;
                }

                
                txtMinimumTotPrice_Ins.Text = hidMinimumTotPrice.Value;
                txtStartDate_Ins.Text = hidStartDate.Value;
                txtEndDate_Ins.Text = hidEndDate.Value;
                ddlDiscountBillType_Ins.SelectedValue = (hidDiscountBillType.Value == null || hidDiscountBillType.Value == "") ? hidDiscountBillType.Value = "-99" : hidDiscountBillType.Value.Trim();
                ddlDiscountBill_Status_Ins.SelectedValue = (hidActive.Value == null || hidActive.Value == "") ? hidActive.Value = "-99" : hidActive.Value.Trim();
                ddlDiscountBillTypeIns_SelectedIndexChanged(ddlDiscountBillType_Ins.SelectedValue,null);
                hidIdList.Value = hidDiscountBillId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-DiscountBill').modal();", true);

            }
            else if (e.CommandName == "Download") 
            {

            }

        }

        protected void btnAddDiscountBill_Click(object sender, EventArgs e)
        {
            bindDDldiscountBillStatus_Ins();
            hidFlagInsert.Value = "True";
            ddlProductBrand_Ins.ClearSelection();
            txtDiscountBillCode_Ins.Text = "";
            txtDiscountBillName_Ins.Text = "";
            txtStartDate_Ins.Text = "";
            txtEndDate_Ins.Text = "";
            ddlDiscountBillType_Ins.ClearSelection();
            ddlFreeShipFlag_Ins.ClearSelection();
            txtDiscountAmount_Ins.Text = "";
            txtDiscountPercent_Ins.Text = "";
            txtMinimumTotPrice_Ins.Text = "";

            txtDiscountBillCode_Ins.Enabled = true;
            txtDiscountAmount_Ins.Enabled = true;
            txtDiscountPercent_Ins.Enabled = true;
            ddlFreeShipFlag_Ins.Enabled = true;
            DiscountBillDiscountSection.Visible = false;
            MinimumTotPriceSection.Visible = false;
            FreeShippingSection.Visible = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-DiscountBill').modal();", true);
        }

        protected void ddlDiscountBillTypeIns_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            switch (ddlDiscountBillType_Ins.SelectedValue)
            {

                case "01":
                    {
                        FreeShippingSection.Visible = true;
                        ddlFreeShipFlag_Ins.Enabled = true;
                        
                        DiscountBillDiscountSection.Visible = true;
                        MinimumTotPriceSection.Visible = true;
                        
                     
                        break;
                    }
                case "02":
                    {
                        ddlFreeShipFlag_Ins.Enabled = true;
                        
                        DiscountBillDiscountSection.Visible = false;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;
                        
                      
                        break;
                    }
                case "03":
                    {
                        ddlFreeShipFlag_Ins.Enabled = true;
                        
                        DiscountBillDiscountSection.Visible = false;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;
                        
                     
                        break;
                    }
                case "04":
                    {
                        DiscountBillDiscountSection.Visible = false;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;
                        
                        ddlFreeShipFlag_Ins.Enabled = false;
                        ddlFreeShipFlag_Ins.SelectedValue = "Y";
                        break;
                    }

                case "-99":
                    {
                        DiscountBillDiscountSection.Visible = false;
                        MinimumTotPriceSection.Visible = false;
                        FreeShippingSection.Visible = false;
                        break;
                    }

                default:
                    {
                        DiscountBillDiscountSection.Visible = true;
                        MinimumTotPriceSection.Visible = true;
                        FreeShippingSection.Visible = true;
                        txtDiscountAmount_Ins.Text = "0";
                        txtDiscountPercent_Ins.Text = "0";
                        txtMinimumTotPrice_Ins.Text = "0";
                        
                        break;
                    }

            }

        }

        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        protected void ShowComboSection(string flag)
        {
            

        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"DiscountBillDetail.aspx?DiscountBillCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }


        protected void BindddlSearchProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductBrandCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);


            ddlSearchProductBrand.DataSource = lLookupInfo;

            ddlSearchProductBrand.DataTextField = "ProductBrandName";

            ddlSearchProductBrand.DataValueField = "ProductBrandCode";

            ddlSearchProductBrand.DataBind();

            ddlSearchProductBrand.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlProductBrand()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductBrandNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["CampaignCategoryCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductBrandInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ProductBrandInfo>>(respstr);


            ddlProductBrand_Ins.DataSource = lLookupInfo;

            ddlProductBrand_Ins.DataTextField = "ProductBrandName";

            ddlProductBrand_Ins.DataValueField = "ProductBrandCode";

            ddlProductBrand_Ins.DataBind();

            ddlProductBrand_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }

        protected void BindddlSearchDiscountBillType()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDiscountBillType";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DiscountBillTypeInfo> lLookupInfo = JsonConvert.DeserializeObject<List<DiscountBillTypeInfo>>(respstr);


            ddlSearchDiscountBillType.DataSource = lLookupInfo;

            ddlSearchDiscountBillType.DataTextField = "DiscountBillTypeName";

            ddlSearchDiscountBillType.DataValueField = "DiscountBillTypeCode";

            ddlSearchDiscountBillType.DataBind();

            ddlSearchDiscountBillType.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

            
        }
        protected void BindddlSearchDiscountBillType_Ins()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListDiscountBillType";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<DiscountBillTypeInfo> lLookupInfo = JsonConvert.DeserializeObject<List<DiscountBillTypeInfo>>(respstr);
            ddlDiscountBillType_Ins.DataSource = lLookupInfo;

            ddlDiscountBillType_Ins.DataTextField = "DiscountBillTypeName";

            ddlDiscountBillType_Ins.DataValueField = "DiscountBillTypeCode";

            ddlDiscountBillType_Ins.DataBind();

            ddlDiscountBillType_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));

        }
        protected Boolean validateInsert()
        {
            Boolean flag = true;


            if (ddlProductBrand_Ins.SelectedValue == "-99" || ddlProductBrand_Ins.SelectedValue == "")
            {
                flag = false;
                lblProductBrand_Ins.Text = MessageConst._MSG_PLEASEINSERT + " แบรนด์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProductBrand_Ins.Text = "";
            }

            if (txtDiscountBillCode_Ins.Text == "")
            {
                flag = false;
                lblDiscountBillCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสโปรโมชั่น";
            }
            else
            {
              
                if (CheckSymbol(txtDiscountBillCode_Ins.Text)==true)
                {
                    flag = false;
                    lblDiscountBillCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสโปรโมชั่นต่้องไม่มีอักขระพิเศษ";
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblDiscountBillCode_Ins.Text = "";
                }
            }




            if (txtDiscountBillName_Ins.Text == "")
            {
                flag = false;
                LbDiscountBillName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อโปรโมชั่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbDiscountBillName_Ins.Text = "";
            }
          
            if (txtStartDate_Ins.Text == "")
            {
                flag = false;
                lblStartDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " วันเริ่มโปรโมชั่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblStartDate_Ins.Text = "";
            }
            if (txtEndDate_Ins.Text == "")
            {
                flag = false;
                lblEndDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " วันเริ่มโปรโมชั่น";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEndDate_Ins.Text = "";
            }
          
            if (ddlFreeShipFlag_Ins.SelectedValue == "-99" || ddlFreeShipFlag_Ins.SelectedValue == "")
            {
                flag = false;
                LbddlFreeShipFlag_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ฟรีค่าขนส่ง";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbddlFreeShipFlag_Ins.Text = "";
            }
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-DiscountBill').modal();", true);
            }

            return flag;
        }
        private bool CheckSymbol(string value)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (value.Contains(item)) return true;
            }

            return false;
        }

        protected void bindDDldiscountBill_Status()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = StaticField.LookupType_DISCOUNTBILLSTATUS; 
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lMaritalStatusInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlDiscountBill_Status.DataSource = lMaritalStatusInfo;
            ddlDiscountBill_Status.DataTextField = "LookupValue";
            ddlDiscountBill_Status.DataValueField = "LookupCode";
            ddlDiscountBill_Status.DataBind();
            ddlDiscountBill_Status.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));
        }

        protected void bindDDldiscountBillStatus_Ins()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLookupNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["LookupType"] = StaticField.LookupType_DISCOUNTBILLSTATUS; 
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<LookupInfo> lMaritalStatusInfo = JsonConvert.DeserializeObject<List<LookupInfo>>(respstr);

            ddlDiscountBill_Status_Ins.DataSource = lMaritalStatusInfo;
            ddlDiscountBill_Status_Ins.DataTextField = "LookupValue";
            ddlDiscountBill_Status_Ins.DataValueField = "LookupCode";
            ddlDiscountBill_Status_Ins.DataBind();
            ddlDiscountBill_Status_Ins.Items.Insert(0, new ListItem("Please Select-------------------------------", "-99"));
        }

        #endregion


    }
}
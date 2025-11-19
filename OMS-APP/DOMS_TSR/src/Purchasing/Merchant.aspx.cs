using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using System.Globalization;
using System.IO;
using OfficeOpenXml;

namespace DOMS_TSR.src.Purchasing
{
    public partial class Merchant : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl;
        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null)
                {
                    APIUrl = empInfo.ConnectionAPI;
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                fileUpload.Visible = false;
                btnUpload.Visible = false;
                loadMerchant();
            }
        }
        #region event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadMerchant();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchMerchantCode.Text = "";
            txtSearchMerchantName.Text = "";
            ddlSearchStatus.SelectedValue = "";
        }
        protected void chkMerchantAll_Changed(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMerchant.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvMerchant.HeaderRow.FindControl("chkMerchantAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvMerchant.Rows[i].FindControl("hidMerchantId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkMerchant = (CheckBox)gvMerchant.Rows[i].FindControl("chkMerchant");

                    chkMerchant.Checked = true;
                }
                else
                {
                    CheckBox chkMerchant = (CheckBox)gvMerchant.Rows[i].FindControl("chkMerchant");

                    chkMerchant.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected void gvMerchant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvMerchant.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidSupplierId = (HiddenField)row.FindControl("hidSupplierId");
            HiddenField hidSupplierCode = (HiddenField)row.FindControl("hidSupplierCode");
            HiddenField hidSupplierName = (HiddenField)row.FindControl("hidSupplierName");
            HiddenField hidTaxIdNo = (HiddenField)row.FindControl("hidTaxIdNo");
            HiddenField hidAddress = (HiddenField)row.FindControl("hidAddress");
            HiddenField hidProvinceCode = (HiddenField)row.FindControl("hidProvinceCode");
            HiddenField hidDistrictCode = (HiddenField)row.FindControl("hidDistrictCode");
            HiddenField hidSubDistrictCode = (HiddenField)row.FindControl("hidSubDistrictCode");
            HiddenField hidZipNo = (HiddenField)row.FindControl("hidZipNo");
            HiddenField hidFaxNumber = (HiddenField)row.FindControl("hidFaxNumber");
            HiddenField hidMail = (HiddenField)row.FindControl("hidMail");
            HiddenField hidActiveFlag = (HiddenField)row.FindControl("hidActiveFlag");

            if (e.CommandName == "ShowMerchant")
            {
                hidSupplierIdIns.Value = hidSupplierId.Value;
                

                hidFlagInsert.Value = "False";

                
            }
        }
        #endregion

        #region function
        protected void loadMerchant()
        {
            List<MerchantInfo> lmerchantInfo = new List<MerchantInfo>();
            int? totalRow = CountMerchantMasterList();
            SetPageBar(Convert.ToDouble(totalRow));
            lmerchantInfo = GetMerchantMasterByCriteria();
            if (lmerchantInfo.Count > 0)
            {
                foreach (var merchantinfoV in lmerchantInfo)
                {
                    if (merchantinfoV.ActiveFlag == StaticField.ActiveFlag_Y) 
                    {
                        merchantinfoV.ActiveFlagName = StaticField.ActiveFlag_Y_NameValue_Active; 
                    }
                    else
                    {
                        merchantinfoV.ActiveFlagName = StaticField.ActiveFlag_N_NameValue_Inactive; 
                    }
                }
            }
            gvMerchant.DataSource = lmerchantInfo;
            gvMerchant.DataBind();
        }
        protected int? CountMerchantMasterList()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountMerchantListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = txtSearchMerchantCode.Text;
                data["MerchantName"] = txtSearchMerchantName.Text;
                data["ActiveFlag"] = ddlSearchStatus.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected List<MerchantInfo> GetMerchantMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/MerchantListPagingCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = txtSearchMerchantCode.Text;
                data["MerchantName"] = txtSearchMerchantName.Text;
                data["ActiveFlag"] = ddlSearchStatus.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lMerchantInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);
            return lMerchantInfo;
        }
        #endregion

        #region binding
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
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelPath = "";
            string FileName = "";

            DataTable dt = new DataTable();

            try
            {
                //*** Check HasFile And Type of File is equals excel /**
                if (fileUpload.HasFile && (fileUpload.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    int fileSize = fileUpload.PostedFile.ContentLength;
                    if (fileSize > StaticField.filesize_5600000) 
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ขนาดไฟล์เกิน 5 MB');", true);
                        return;
                    }

                    else
                    {
                        string newFileName = "Upload_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                        fileUpload.SaveAs(Server.MapPath("~/Uploadfile/Xls/" + newFileName));


                        FileInfo excel = new FileInfo(Server.MapPath(@"~/Uploadfile/Xls/" + newFileName));
                        ExcelPath = excel.ToString();
                        FileName = newFileName;
                        ViewState["UpLoadFileName"] = fileUpload.FileName.ToString();
                        ViewState["FileName"] = newFileName;
                        ViewState["ExcelPath"] = excel.ToString();

                        List<ProductInfo> lproductInfo = new List<ProductInfo>();

                        using (var package = new ExcelPackage(excel))
                        {
                            var workbook = package.Workbook;
                            var worksheet = workbook.Worksheets[1];
                            //เช็คว่าไฟล์มีข้อมูล
                            if (worksheet.Cells[2, 2].Text.ToString().Trim() == "")
                            {
                                
                                File.Delete(ExcelPath);
                            }
                            else
                            {
                                dt = ConvertToDataTable(worksheet);
                                
                                dt = dt.DefaultView.ToTable();
                                
                            }
                        }   

                                
                    }
                }

            }
            catch (Exception ex)
            {

            }

            List<ProductInfo> productlist = dt.AsEnumerable().Select(row => new ProductInfo
            {
                Price = (row["Price"].ToString() != "") ? Convert.ToDouble(row["Price"]) : 0,
                ProductCode = row["ProductCode"].ToString().Trim(),

            }).ToList();

            foreach (var od in productlist)
            {

            }
        }

        private DataTable ConvertToDataTable(ExcelWorksheet worksheet)
        {
            int totalRows = worksheet.Dimension.End.Row;
            DataTable dt = new DataTable(worksheet.Name);
            DataRow dr = null;

            //*** Column **/
            dt.Columns.Add("ProductCode");
            dt.Columns.Add("ProductName");
            dt.Columns.Add("ProductCategory");
            dt.Columns.Add("Price");

            try
            {
                int j;
                for (int i = 2; i <= totalRows; i++)
                {
                    //*** Rows ***//
                    dr = dt.NewRow();
                    j = 1;

                    dr["ProductCode"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ProductName"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["ProductCategory"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["Price"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;

                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        #endregion
    }
}
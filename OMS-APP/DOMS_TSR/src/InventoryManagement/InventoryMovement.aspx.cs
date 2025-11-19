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

namespace DOMS_TSR.src.InventoryManagement
{
    public partial class InventoryMovement : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string ProductImgUrl = ConfigurationManager.AppSettings["ProductImageUrl"];
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
                    APIUrl = empInfo.ConnectionAPI;
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                loadInventoryInformation();
                loadInventoryMovement();
            }
        }

        #region function
        protected void loadInventoryInformation()
        {
            List<InventoryInfo> liInfo = new List<InventoryInfo>();
            liInfo = GetInventoryMasterNoPaging();

            if (liInfo.Count > 0)
            {
                lblInventoryCode.Text = (Request.QueryString["InventoryCode"] != null) ? Request.QueryString["InventoryCode"].ToString() : "";
                lblInventoryName.Text = liInfo[0].InventoryName;                
            }

            lblProductCode.Text = (Request.QueryString["ProductCode"] != null) ? Request.QueryString["ProductCode"].ToString() : "";
            List<ProductInfo> lpInfo = new List<ProductInfo>();
            lpInfo = GetProductMasterNoPaging();

            if (lpInfo.Count > 0)
            {
                lblProductName.Text = lpInfo[0].ProductName;
                lblProductCategoryName.Text = lpInfo[0].ProductCategoryName;
                lblProductBrand.Text = lpInfo[0].ProductBrandName;
            }
        }
        protected void loadInventoryMovement()
        {
            List<InventoryMovementInfo> lmvInfo = new List<InventoryMovementInfo>();
            int? totalRow = CountInventoryMovement();
            SetPageBar(Convert.ToDouble(totalRow));
            lmvInfo = GetInventoryMovement();

            

            gvInventoryMovement.DataSource = lmvInfo;
            gvInventoryMovement.DataBind();
        }
        protected List<InventoryInfo> GetInventoryMasterNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["InventoryCode"] = (Request.QueryString["InventoryCode"] != null) ? Request.QueryString["InventoryCode"].ToString() : "";

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            return lInventoryInfo;
        }
        protected List<ProductInfo> GetProductMasterNoPaging()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProductMasterNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = lblProductCode.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ProductInfo> lProductInfo = JsonConvert.DeserializeObject<List<ProductInfo>>(respstr);

            return lProductInfo;
        }
        protected int? CountInventoryMovement()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountInventoryMovementByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = (Request.QueryString["ProductCode"] != null) ? Request.QueryString["ProductCode"].ToString() : "";
                data["InventoryDetailId"] = (Request.QueryString["InventoryDetailId"] != null) ? Request.QueryString["InventoryDetailId"].ToString() : "";
                data["ProductCode"] = (Request.QueryString["ProductCode"] != null) ? Request.QueryString["ProductCode"].ToString() : "";
                data["InventoryDetailId"] = (Request.QueryString["InventoryDetailId"] != null) ? Request.QueryString["InventoryDetailId"].ToString() : "";
                data["InventoryManualLotCode"] = txtSearchInventoryManualLotCode.Text.Trim();
                data["InventoryMovementCode"] = txtSearchInventoryMovementCode.Text.Trim();
                data["POCode"] = txtSearchPOCode.Text.Trim();
                data["GRCode"] = txtSearchGRCode.Text.Trim();
                data["SupplierName"] = txtSearchSupplierName.Text.Trim();
                data["OrderNo"] = txtSearchOrderNo.Text.Trim();
                data["EmpFNameTH"] = txtSearchEmpFNameTH.Text.Trim();
                data["EmpLNameTH"] = txtSearchEmpLNameTH.Text.Trim();
                data["EmpFNameTH"] = txtSearchEmpFNameTH.Text.Trim();
                data["EmpLNameTH"] = txtSearchEmpLNameTH.Text.Trim();
                data["CreateDateFrom"] = txtSearchCreateDateFrom.Text;
                data["CreateDateTo"] = txtSearchCreateDateTo.Text;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected List<InventoryMovementInfo> GetInventoryMovement()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryMovementInfoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ProductCode"] = (Request.QueryString["ProductCode"] != null) ? Request.QueryString["ProductCode"].ToString() : "";
                data["InventoryDetailId"] = (Request.QueryString["InventoryDetailId"] != null) ? Request.QueryString["InventoryDetailId"].ToString() : "";
                data["InventoryManualLotCode"] = txtSearchInventoryManualLotCode.Text.Trim();
                data["InventoryMovementCode"] = txtSearchInventoryMovementCode.Text.Trim();
                data["POCode"] = txtSearchPOCode.Text.Trim();
                data["GRCode"] = txtSearchGRCode.Text.Trim();
                data["SupplierName"] = txtSearchSupplierName.Text.Trim();
                data["OrderNo"] = txtSearchOrderNo.Text.Trim();
                data["EmpFNameTH"] = txtSearchEmpFNameTH.Text.Trim();
                data["EmpLNameTH"] = txtSearchEmpLNameTH.Text.Trim();
                data["EmpFNameTH"] = txtSearchEmpFNameTH.Text.Trim();
                data["EmpLNameTH"] = txtSearchEmpLNameTH.Text.Trim();
                data["CreateDateFrom"] = txtSearchCreateDateFrom.Text;
                data["CreateDateTo"] = txtSearchCreateDateTo.Text;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryMovementInfo> lInventoryMovementInfo = JsonConvert.DeserializeObject<List<InventoryMovementInfo>>(respstr);

            return lInventoryMovementInfo;
        }
        #endregion

        #region Binding
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

            loadInventoryMovement();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadInventoryMovement();
        }
        #endregion

        #region event
        protected void btnSearch_Clicked(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadInventoryMovement();
        }
        #endregion
    }
}
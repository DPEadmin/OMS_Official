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

namespace DOMS_TSR.src.Purchasing
{
    public partial class SupplierDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static int currentPageNumber;
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
                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                loadSupplierInformation();

                litLinkBack.Text = "<a href=\"Supplier.aspx" + "\" class=\"font11link\"> << Back</a>";
            }
        }
        #region function
        protected void loadSupplierInformation()
        {
            SupplierInfo sInfo = new SupplierInfo();
            List<SupplierInfo> lsinfo = new List<SupplierInfo>();
            String suppliercode = Request.QueryString["SupplierCode"]; ;
            lsinfo = GetSupplierMasterByCriteria(suppliercode);
            if (lsinfo.Count > 0)
            {
                lblSupplierCode.Text = lsinfo[0].SupplierCode;
                lblSupplierName.Text = lsinfo[0].SupplierName;
                lblIdNo.Text = lsinfo[0].TaxIdNo;
                lblAddress.Text = lsinfo[0].Address;
                lblProvinceName.Text = lsinfo[0].ProvinceName;
                lblDistrictName.Text = lsinfo[0].DistrictName;
                lblSubdistrictName.Text = lsinfo[0].SubDistrictName;
                lblZipNo.Text = lsinfo[0].ZipNo;
                lblPhoneNumber.Text = lsinfo[0].PhoneNumber;
                lblFaxNumber.Text = lsinfo[0].FaxNumber;
                lblEmail.Text = lsinfo[0].Mail;
                if (lsinfo[0].ActiveFlag == "Y")
                {
                    lblStatus.Text = "Active";
                }
                else
                {
                    lblStatus.Text = "Inactive";
                }
                hidSupplierId.Value = lsinfo[0].SupplierId.ToString();
            }
        }
        protected List<SupplierInfo> GetSupplierMasterByCriteria(String suppliercode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListSupplierNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["SupplierCode"] = suppliercode;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<SupplierInfo> lSupplierInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);

            return lSupplierInfo;
        }
        #endregion
    }
}
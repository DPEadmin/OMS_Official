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


namespace DOMS_TSR.src.Purchasing
{
    public partial class PODetail : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
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
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                String POCode = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";

                loadPOmapSupplier(POCode);
                loadPOmapInventory(POCode);
                loadPOInformation(POCode);
                loadPOItem(POCode);

                btnApprove.Visible = false;
                btnRevise.Visible = false;
                btnReject.Visible = false;
            }
            
        }
        #region function
        protected void loadPOmapSupplier(String pocode)
        {
            txtOrderCode.Text = pocode;

            List<SupplierInfo> lsupInfo = new List<SupplierInfo>();
            lsupInfo = GetPOMapSupplier();

            if (lsupInfo.Count > 0)
            {
                for (int i = 0; i < lsupInfo.Count; i++)
                {
                    txtSupplierName.Text = lsupInfo[0].SupplierName;
                    txtSupplierTaxIdNo.Text = lsupInfo[0].TaxIdNo;
                    txtSupplierAddress.Text = lsupInfo[0].Address + " " + lsupInfo[0].SubDistrictName + " " + lsupInfo[0].DistrictName + " " + lsupInfo[0].ProvinceName + " " + lsupInfo[0].ZipNo;
                    txtSupplierContactor.Text = lsupInfo[0].Contactor;
                    txtSupplierPhoneNumber.Text = lsupInfo[0].PhoneNumber;
                    txtSupplierFaxNumber.Text = lsupInfo[0].FaxNumber;
                    txtSupplierMail.Text = lsupInfo[0].Mail;
                }
            }
        }
        protected void loadPOmapInventory(String pocode)
        {
            List<InventoryInfo> linvInfo = new List<InventoryInfo>();
            linvInfo = GetPOmapInventory();

            if (linvInfo.Count > 0)
            {
                for (int i = 0; i < linvInfo.Count; i++)
                {
                    txtInventoryName.Text = linvInfo[0].InventoryName;
                    txtInventoryAddress.Text = linvInfo[0].Address + " " + linvInfo[0].SubDistrictName + " " + linvInfo[0].DistrictName + " " + linvInfo[0].ProvinceName + " " + linvInfo[0].PostCode;
                    txtInventoryContactTel.Text = linvInfo[0].ContactTel;
                    txtInventoryFax.Text = linvInfo[0].Fax;
                }
            }
        }
        protected void loadPOInformation(String pocode)
        {
            List<POInfo> lpoInfo = new List<POInfo>();
            lpoInfo = GetPOMaster();

            if (lpoInfo.Count > 0)
            {
                txtPODate.Text = lpoInfo[0].PODate;
                txtPOExpectDate.Text = lpoInfo[0].ExpectDate;
                txtPOCredit.Text = lpoInfo[0].Credit;
                txtPODescription.Text = lpoInfo[0].Description;
                lblPaymentMethod.Text = lpoInfo[0].PaymentMethodName;

                string[] s = txtPODate.Text.Split(' ');
                string[] v = txtPOExpectDate.Text.Split(' ');

                for (int i = 0; i < s.Length; i++)
                {
                    if (i == 0)
                    {
                        txtPODate.Text = s[0];
                    }
                    else if (i == 1)
                    {
                    }
                }

                for (int i = 0; i < v.Length; i++)
                {
                    if (i == 0)
                    {
                        txtPOExpectDate.Text = v[0];
                    }
                    else if (i == 1)
                    {
                    }
                }                
            }
        }
        protected void loadPOItem(String pocode)
        {
            List<POItemInfo> lpoitem = new List<POItemInfo>();
            lpoitem = GetPOItem();

            loadTotal(lpoitem);
            gvPOItem.DataSource = lpoitem;
            gvPOItem.DataBind();
        }
        protected List<SupplierInfo> GetPOMapSupplier()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOMapSupplier";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<SupplierInfo> lsupInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);
            return lsupInfo;
        }
        protected List<InventoryInfo> GetPOmapInventory()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/POMapInventoryNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<InventoryInfo> linvInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);
            return linvInfo;
        }
        protected List<POItemInfo> GetPOItem()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOItemNopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<POItemInfo> lpoitemInfo = JsonConvert.DeserializeObject<List<POItemInfo>>(respstr);
            return lpoitemInfo;
        }
        protected List<POInfo> GetPOMaster()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPONopagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = (Request.QueryString["POCode"] != null) ? Request.QueryString["POCode"].ToString() : "";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<POInfo> lpoInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);
            return lpoInfo;
        }
        protected void loadTotal(List<POItemInfo> lpoitemInfo)
        {
            Double? sumprice = 0;
            Double? billdiscount = 0;
            Double? netprice = 0;
            Double? vat = 0;
            Double? totalprice = 0;

            foreach (var lpV in lpoitemInfo.ToList())
            {
                sumprice += lpV.TotPrice;
                billdiscount = lpV.DiscountBill;
            }

            totaltext.Text = string.Format("{0:n}", (sumprice));
            

            netprice = sumprice - billdiscount;
            vat = netprice * 7 / 100;
            totalprice = netprice + vat;

            txtBillDiscount.Text = string.Format("{0:n}", (billdiscount));
            txtvat.Text = string.Format("{0:n}", (vat));
            txtTotalPrice.Text = string.Format("{0:n}", (totalprice));
        }
        #endregion
    }
}
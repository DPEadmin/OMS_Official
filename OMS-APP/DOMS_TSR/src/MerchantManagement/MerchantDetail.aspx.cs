using Newtonsoft.Json;
using SALEORDER.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SALEORDER.Common;

namespace DOMS_TSR.src.MerchantManagement
{

    public partial class MerchantDetail : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];

        string APIpath = "";
        string Codelist = "";
        Boolean isdelete;

        public Boolean check_UserLogin = false;
        public Boolean check_Role = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null && merchantInfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                    
                    ((DropDownList)Master.FindControl("ddlMerchant")).Enabled = false;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                loadMerchant();


            }

        }

        #region Function

        protected List<MerchantInfo> GetMerchantMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/MerchantListNoPagingCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantCode"] = (Request.QueryString["MerchantCode"] != null) ? Request.QueryString["MerchantCode"].ToString() : "";

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MerchantInfo> lMerInfo = JsonConvert.DeserializeObject<List<MerchantInfo>>(respstr);
            return lMerInfo;
        }

        protected void loadMerchant()
        {
            List<MerchantInfo> lMerInfo = new List<MerchantInfo>();
                        
            lMerInfo = GetMerchantMasterByCriteria();
            if (lMerInfo.Count > 0)
            {
                foreach (var mer in lMerInfo)
                {
                    if (mer.ActiveFlag == "Y")
                    {
                        mer.ActiveFlagName = StaticField.ActiveFlag_Y_NameValue_Active; 
                    }
                    else
                    {
                        mer.ActiveFlagName = StaticField.ActiveFlag_N_NameValue_Inactive; 
                    }
                }
                lblMerCodeIns.Text = lMerInfo[0].MerchantCode;

                lblMerNameIns.Text = lMerInfo[0].MerchantName;
                
                lblTaxIdIns.Text = lMerInfo[0].TaxId;
                
                lblMobileIns.Text = lMerInfo[0].ContactTel;
                lblFaxIns.Text = lMerInfo[0].FaxNum;
                lblEmailIns.Text = lMerInfo[0].Email;
            }
            
        }

        #endregion

    }
}
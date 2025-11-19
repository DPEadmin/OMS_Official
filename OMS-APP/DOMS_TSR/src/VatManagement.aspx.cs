using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SALEORDER.DTO;
using System.Configuration;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using Newtonsoft.Json;
using DOMS_TSR.Classes.DTO;
using SALEORDER.Common;

namespace DOMS_TSR.src
{
    public partial class VatManagement : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        
        protected static string APIUrl;
        string APIpath = "";
        string Idlist = "";
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
                    APIUrl = empInfo.ConnectionAPI;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                loadVat();
            }
        }

        #region function
        protected void loadVat()
        {
            List<VatInfo> lVatInfo = new List<VatInfo>();
            int? totalRow = loadcountVat();
            SetPageBar(Convert.ToDouble(totalRow));
            lVatInfo = GetVatMasterByCriteria();
            gvVat.DataSource = lVatInfo;
            gvVat.DataBind();
        }

        protected List<VatInfo> GetVatMasterByCriteria()
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListVatByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["VatCode"] = txtVatCode.Text;
                data["VatName"] = txtVatName.Text;
                data["VatValue"] = txtVatValue.Text;
                data["FlagActive"] = ddlActive.SelectedValue;
                data["FlagDelete"] = "N";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<VatInfo> listVatInfo = JsonConvert.DeserializeObject<List<VatInfo>>(respstr);

            return listVatInfo;
        }

        protected int? loadcountVat()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListVatByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["VatCode"] = txtVatCode.Text;
                data["VatName"] = txtVatName.Text;
                data["VatValue"] = txtVatValue.Text;
                data["FlagActive"] = ddlActive.SelectedValue;
                data["FlagDelete"] = "N";
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            return cou;
        }

        protected Boolean DeleteVat()
        {
            for (int i = 0; i < gvVat.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvVat.Rows[i].FindControl("chkVat");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvVat.Rows[i].FindControl("hidVatId");

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
                APIpath = APIUrl + "/api/support/DeleteVat";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["VatCode"] = Codelist;
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
            loadVat();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            loadVat();
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


        #endregion function

        #region event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadVat();
        }

        protected void chkVatAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvVat.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvVat.HeaderRow.FindControl("chkVatAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvVat.Rows[i].FindControl("hidVatId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkVat = (CheckBox)gvVat.Rows[i].FindControl("chkVat");

                    chkVat.Checked = true;
                }
                else
                {
                    CheckBox chkVat = (CheckBox)gvVat.Rows[i].FindControl("chkVat");
                    chkVat.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }

        protected void btnAddVat_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-vat').modal();", true);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteVat();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion event
    }
}
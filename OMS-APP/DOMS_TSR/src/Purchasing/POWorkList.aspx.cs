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
    public partial class POWorkList : System.Web.UI.Page
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
                BindddlSearchSupplier();
                BindddlSearchInventory();
                LoadPOWorkList();
            }
        }

        #region event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPOWorkList();
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchPOCode.Text = "";
            ddlSearchSupplier.SelectedValue = "-99";
            txtSearchPODateFrom.Text = "";
            txtSearchPODateTo.Text = "";
            ddlSearchInventory.SelectedValue = "-99";
            txtSearchCreateByNameTH.Text = "";
        }
        protected void gvPOWorkList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            hidApprover1.Value = "True";

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPOWorkList.Rows[index];

            HiddenField hidPOCode = (HiddenField)row.FindControl("hidPOCode");

            if (e.CommandName == "EditPOStatus")
            {
                Response.Redirect("/src/Purchasing/CreatePO.aspx?hidApprover1=" + hidApprover1.Value + "&POCode=" + hidPOCode.Value + "&hidFlagInsert=" + "False");
            }
        }
        protected void chkPOAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPOWorkList.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvPOWorkList.HeaderRow.FindControl("chPOAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPOWorkList.Rows[i].FindControl("hidPOId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkPO = (CheckBox)gvPOWorkList.Rows[i].FindControl("chkPO");

                    chkPO.Checked = true;
                }
                else
                {
                    CheckBox chkPO = (CheckBox)gvPOWorkList.Rows[i].FindControl("chkPO");

                    chkPO.Checked = false;
                }
            }
            hidIdList.Value = Codelist;
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            hidworkflowstatus.Value = StaticField.WfStatus_Approve; 

            if (hidworkflowstatus.Value == StaticField.WfStatus_Approve) 
            {
                hidwftaskliststatus.Value = StaticField.WfStatus_1200; 
            }

            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];

            for (int i = 0; i < gvPOWorkList.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvPOWorkList.Rows[i].FindControl("chkPO");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvPOWorkList.Rows[i].FindControl("hidPOId");

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
                APIpath = APIUrl + "/api/support/UpdateWFTaskList";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["OMSId"] = Codelist;
                    data["Status"] = hidwftaskliststatus.Value;
                    data["UpdateBy"] = empInfo.EmpCode;

                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            }
        }
        protected void btnRevise_Click(object sender, EventArgs e)
        {

        }
        protected void btnReject_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region function
        protected void LoadPOWorkList()
        {
            List<POInfo> lPOInfo = new List<POInfo>();
            int? totalRow = CountPOMaster();
            SetPageBar(Convert.ToDouble(totalRow));
            lPOInfo = GetPOMasterByCriteria();

            foreach (var lpV in lPOInfo.ToList())
            {
                string[] s = lpV.UpdateDate.Split(' ');

                for (int i = 0; i < s.Length; i++)
                {
                    if (i == 0)
                    {
                        lpV.UpdateDate = s[0];
                    }
                    else if (i == 1)
                    {
                    }
                }
            }

            gvPOWorkList.DataSource = lPOInfo;
            gvPOWorkList.DataBind();
        }
        protected int? CountPOMaster()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/CountPOListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["StatusCode"] = StaticField.WfStatus_200; 

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);
            return cou;
        }
        protected List<POInfo> GetPOMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPOByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["POCode"] = txtSearchPOCode.Text.Trim();
                data["SupplierCode"] = ddlSearchSupplier.SelectedValue;
                data["InventoryCode"] = ddlSearchInventory.SelectedValue;
                data["PODate"] = txtSearchPODateFrom.Text;
                data["PODateTo"] = txtSearchPODateTo.Text;
                data["StatusCode"] = StaticField.WfStatus_200; 
                data["CreateByNameTH"] = txtSearchCreateByNameTH.Text.Trim();
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<POInfo> lPOInfo = JsonConvert.DeserializeObject<List<POInfo>>(respstr);
            return lPOInfo;
        }
        public List<SupplierInfo> GetListSupplierInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSupplierNopagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<SupplierInfo> lSupplierInfo = JsonConvert.DeserializeObject<List<SupplierInfo>>(respstr);

            return lSupplierInfo;
        }
        public List<InventoryInfo> GetListInventoryInfo()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListInventoryNoPagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["FlagDelete"] = "N";
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<InventoryInfo> lInventoryInfo = JsonConvert.DeserializeObject<List<InventoryInfo>>(respstr);

            return lInventoryInfo;
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
            LoadPOWorkList();
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);
            LoadPOWorkList();
        }
        protected void BindddlSearchSupplier()
        {
            List<SupplierInfo> lSupplierInfo = new List<SupplierInfo>();
            lSupplierInfo = GetListSupplierInfo();

            ddlSearchSupplier.DataSource = lSupplierInfo;
            ddlSearchSupplier.DataValueField = "SupplierCode";
            ddlSearchSupplier.DataTextField = "SupplierName";
            ddlSearchSupplier.DataBind();

            ddlSearchSupplier.Items.Insert(0, new ListItem("กรุณาเลือก-------------------------------", "-99"));
        }
        protected void BindddlSearchInventory()
        {
            List<InventoryInfo> lInventoryInfo = new List<InventoryInfo>();
            lInventoryInfo = GetListInventoryInfo();

            ddlSearchInventory.DataSource = lInventoryInfo;
            ddlSearchInventory.DataValueField = "InventoryCode";
            ddlSearchInventory.DataTextField = "InventoryName";
            ddlSearchInventory.DataBind();

            ddlSearchInventory.Items.Insert(0, new ListItem("กรุณาเลือก-------------------------------", "-99"));
        }
        #endregion
    }
}
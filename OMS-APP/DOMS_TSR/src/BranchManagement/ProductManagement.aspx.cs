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
using System.Web.UI.HtmlControls;
using SALEORDER.Common;

namespace DOMS_TSR.src.BranchManagement
{
    public partial class ProductManagement : System.Web.UI.Page
    {
        L_OrderChangestatus result = new L_OrderChangestatus();
        string CodelistApprove = "";
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist = "";

        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";

        public Boolean check = false;
        public Boolean check_RequestForEditOrder = false;
        public Boolean check_RequestForRejectOrder = false;

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

                    List<EmpBranchInfo> lb = new List<EmpBranchInfo>();

                    lb = ListEmpBranchByCriteria(empInfo.EmpCode);

                    if (lb.Count > 0)
                    {
                        hidBranchcode.Value = lb[0].BranchCode;
                        hiddisplayname.Value = lb[0].BranchCode;
                    }
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                BindddlSearchRecipe();
                LoadOrder();
            }
        }

        #region Main
        #region Function (Main)

        public static List<EmpBranchInfo> ListEmpBranchByCriteria(string empcode)
        {
            string respstr = "";

            string APIpath1 = APIUrl + "/api/support/ListEmpBranchByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empcode;
                data["rowOFFSet"] = "0";
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath1, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpBranchInfo> lEmpBranchInfo = JsonConvert.DeserializeObject<List<EmpBranchInfo>>(respstr);

            return lEmpBranchInfo;

        }

        #endregion Function (Main)

        #region Events (Main)

        #endregion Events (Main)
        #endregion Main

        #region Function
        protected void LoadOrder()
        {
            List<BranchMapProductInfo> lBranchMapProductInfo = new List<BranchMapProductInfo>();

            int? totalRow = CountOrderManagementList();

            SetPageBar(Convert.ToDouble(totalRow));

            lBranchMapProductInfo = GetOrderMasterByCriteria();

            gvBranchMapProduct.DataSource = lBranchMapProductInfo;
            gvBranchMapProduct.DataBind();

        }

        public int? CountOrderManagementList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListBranchMapProductByCriteriaWithOneTxt";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["BranchCode"] = hidBranchcode.Value;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["RecipeCode"] = ddlSearchRecipe.SelectedValue == "-99" ? data["RecipeCode"] = "" : data["RecipeCode"] = ddlSearchRecipe.SelectedValue;

                data["Active"] = ddlSearchActive.SelectedValue == "-99" ? data["Active"] = "" : data["Active"] = ddlSearchActive.SelectedValue;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;
        }

        public List<BranchMapProductInfo> GetOrderMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListBranchMapProductByCriteriaWithOneTxt";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["BranchCode"] = hidBranchcode.Value;

                data["ProductCode"] = txtSearchProductCode.Text;

                data["ProductName"] = txtSearchProductName.Text;

                data["RecipeCode"] = ddlSearchRecipe.SelectedValue == "-99" ? data["RecipeCode"] = "" : data["RecipeCode"] = ddlSearchRecipe.SelectedValue;

                data["Active"] = ddlSearchActive.SelectedValue == "-99" ? data["Active"] = "" : data["Active"] = ddlSearchActive.SelectedValue;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<BranchMapProductInfo> lVehicleInfo = JsonConvert.DeserializeObject<List<BranchMapProductInfo>>(respstr);


            return lVehicleInfo;

        }

        public void clearSearch()
        {
            txtSearchProductCode.Text = "";
            txtSearchProductName.Text = "";
            ddlSearchRecipe.SelectedValue = "-99";
            ddlSearchActive.SelectedValue = "-99";
        }

        protected bool SetActiveFalse() {

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            for (int i = 0; i < gvBranchMapProduct.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvBranchMapProduct.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvBranchMapProduct.Rows[i].FindControl("hidBranchMapProductId");

                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateBranchMapProductActiveFlagWithString";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Active"] = "N";
                    data["UpdateBy"] = empInfo.EmpCode;
                    data["ProductCode"] = Codelist;



                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {
                    LoadOrder();
                }



            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;
        }

        protected bool SetActiveTrue()
        {

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            for (int i = 0; i < gvBranchMapProduct.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvBranchMapProduct.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvBranchMapProduct.Rows[i].FindControl("hidBranchMapProductId");

                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateBranchMapProductActiveFlagWithString";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Active"] = "Y";
                    data["UpdateBy"] = empInfo.EmpCode;
                    data["ProductCode"] = Codelist;



                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {
                    LoadOrder();
                }



            }
            else
            {
                hidIdList.Value = "";
                return false;
            }

            hidIdList.Value = "";
            return true;
        }

        #endregion Function

        #region Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadOrder();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            clearSearch();
        }

        protected void gvBranchMapProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvBranchMapProduct.Rows[index];

            HiddenField hidBranchMapProductId = (HiddenField)row.FindControl("hidBranchMapProductId");
            HiddenField hidProductCode = (HiddenField)row.FindControl("hidProductCode");
            HiddenField hidProductName = (HiddenField)row.FindControl("hidProductName");
            HiddenField hidActive = (HiddenField)row.FindControl("hidActive");
            HiddenField hidActiveCancelProduct = (HiddenField)row.FindControl("hidActiveCancelProduct");
            HtmlInputCheckBox hidSwitchChkBox = (HtmlInputCheckBox)row.FindControl("hidSwitchChkBox");

            String activeFlag = hidActive.Value.Trim() == "Y" ? "N" : "Y";

            if (e.CommandName == "SetActiveStatus")
            {
                


                string respstr = "";

                APIpath = APIUrl + "/api/support/UpdateBranchMapProductActiveFlag";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["Active"] = activeFlag;
                    data["UpdateBy"] = empInfo.EmpCode;
                    data["BranchMapProductId"] = hidBranchMapProductId.Value;



                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                int? cou = JsonConvert.DeserializeObject<int?>(respstr);
                if (cou > 0)
                {
                    LoadOrder();
                }
            }
             if (e.CommandName == "ShowProduct")
            {
                lblProductCode_Ins.Text = hidProductCode.Value;
                lblProductName_Ins.Text = hidProductName.Value;
                hidActiveCancelFlag_Ins.Value = hidActiveCancelProduct.Value;
                hidBranchMapProductId_Ins.Value = hidBranchMapProductId.Value;
                if (hidActiveCancelFlag_Ins.Value == "Y")
                {
                    hidSwitchChkBox_Ins.Checked = false;
                }
                else
                {
                    hidSwitchChkBox_Ins.Checked = true;

                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Product').modal();", true);

            }

        }

        protected void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];
        }

        protected void gvBranchMapProduct_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                HiddenField hidBranchMapProductId = (HiddenField)e.Row.FindControl("hidBranchMapProductId");
                HiddenField hidProductCode = (HiddenField)e.Row.FindControl("hidProductCode");
                HiddenField hidProductName = (HiddenField)e.Row.FindControl("hidProductName");
                HiddenField hidActive = (HiddenField)e.Row.FindControl("hidActive");
                HtmlInputCheckBox hidSwitchChkBox = (HtmlInputCheckBox)e.Row.FindControl("hidSwitchChkBox");
                HiddenField hidActiveCancelProduct = (HiddenField)e.Row.FindControl("hidActiveCancelProduct");

                Label lblUpdateTime = (Label)e.Row.FindControl("lblUpdateTime");
                Label lblActiveCancelStatus = (Label)e.Row.FindControl("lblActiveCancelStatus");
                Button hidChkbox = (Button)e.Row.FindControl("hidChkbox");

                CheckBox chkProduct = (CheckBox)e.Row.FindControl("chkProduct");

                if (hidActive.Value.Trim() == "Y")
                {
                    
                        hidSwitchChkBox.Checked = true;
                        lblUpdateTime.Visible = false;
                    
                }
                else
                {
                    if (hidActiveCancelProduct.Value.Trim() == "N")
                    {
                        hidSwitchChkBox.Checked = false;
                        lblUpdateTime.Visible = true;
                    }
                    else
                    {
                        hidSwitchChkBox.Checked = false;
                        lblUpdateTime.Visible = false;
                    }
                }

                if(hidActiveCancelProduct.Value.Trim() == "Y")
                {
                    chkProduct.Enabled = false;
                    hidChkbox.Enabled = false;
                    lblActiveCancelStatus.Text = "ปิดถาวร";
                }
                else
                {
                    chkProduct.Enabled = true;
                    hidChkbox.Enabled = true;
                    lblActiveCancelStatus.Text = "";
                }


            }
        }

        protected void hidBtnSetActiveClose_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateBranchMapProductCancelFlag";

            string activeCancelFlag = hidActiveCancelFlag_Ins.Value.Trim() == "Y" ? "N" : "Y";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ActiveCancelProduct"] = activeCancelFlag;
                data["UpdateBy"] = empInfo.EmpCode;
                data["BranchMapProductId"] = hidBranchMapProductId_Ins.Value;


                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            int? cou = JsonConvert.DeserializeObject<int?>(respstr);

            string respstr2 = "";

            string APIpath2 = APIUrl + "/api/support/UpdateBranchMapProductActiveFlag";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["Active"] = hidActiveCancelFlag_Ins.Value.Trim();
                data["UpdateBy"] = empInfo.EmpCode;
                data["BranchMapProductId"] = hidBranchMapProductId_Ins.Value;



                var response2 = wb.UploadValues(APIpath2, "POST", data);
                respstr2 = Encoding.UTF8.GetString(response2);
            }
            int? cou2 = JsonConvert.DeserializeObject<int?>(respstr);

            if (cou > 0)
            {
                hidSwitchChkBox_Ins.Checked = !hidSwitchChkBox_Ins.Checked;
                LoadOrder();

                if (activeCancelFlag == "Y")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ปิดถาวรสำเร็จ');$('#modal-Product').modal('hide');", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ยกเลิกการปิดถาวร');$('#modal-Product').modal('hide');", true);

                }
            }
        }

        protected void chkProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvBranchMapProduct.Rows.Count; i++)
            {
                HiddenField hidActiveCancelProduct = (HiddenField)gvBranchMapProduct.Rows[i].FindControl("hidActiveCancelProduct");

                CheckBox chkall = (CheckBox)gvBranchMapProduct.HeaderRow.FindControl("chkProductAll");

                if (chkall.Checked == true)
                {
                    if (hidActiveCancelProduct.Value.Trim() == "N")
                    {
                        HiddenField hidCode = (HiddenField)gvBranchMapProduct.Rows[i].FindControl("hidBranchMapProductId");

                        if (Codelist != "")
                        {
                            Codelist += ",'" + hidCode.Value + "'";
                        }
                        else
                        {
                            Codelist += "'" + hidCode.Value + "'";
                        }

                        CheckBox chkProduct = (CheckBox)gvBranchMapProduct.Rows[i].FindControl("chkProduct");

                        chkProduct.Checked = true;
                    }
                    else
                    {
                        CheckBox chkProduct = (CheckBox)gvBranchMapProduct.Rows[i].FindControl("chkProduct");

                        chkProduct.Checked = false;
                    }
                }
                else
                {

                    CheckBox chkProduct = (CheckBox)gvBranchMapProduct.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSetActive_Click(object sender, EventArgs e)
        {
            bool isactive = SetActiveTrue();

            btnSearch_Click(null, null);

            if (!isactive)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการเปิด');", true);
            }

        }

        protected void btnSetInActive_Click(object sender, EventArgs e)
        {
            bool isinactive = SetActiveFalse();

            btnSearch_Click(null, null);

            if (!isinactive)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการปิด');", true);
            }
        }

        #endregion Events

        #region Binding

        protected void BindddlSearchRecipe()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListRecipeNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["RecipeCode"] = "";


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<RecipeInfo> rInfo = JsonConvert.DeserializeObject<List<RecipeInfo>>(respstr);

            ddlSearchRecipe.DataSource = rInfo;
            ddlSearchRecipe.DataTextField = "RecipeName";
            ddlSearchRecipe.DataValueField = "RecipeCode";
            ddlSearchRecipe.DataBind();
            ddlSearchRecipe.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        

        #endregion Binding

        #region Paging

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


            LoadOrder();
        }

        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadOrder();
        }



        #endregion Paging

        
    }
}
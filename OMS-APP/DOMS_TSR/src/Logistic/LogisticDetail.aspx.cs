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
namespace DOMS_TSR.src.Logistic
{
    public partial class LogisticDetail : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        string APIpathLogisticDetail = "";
        string LogisticCode = "";
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

                LogisticCode = (Request.QueryString["LogisticCode"] != null) ? Request.QueryString["LogisticCode"].ToString() : "";
                LoadLogisticDetail(LogisticCode);

              }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadLogisticDetail(LogisticCode);
        }



        #region Function



        protected void LoadLogisticDetail(string Code)
        {
            List<LogisticDetailSelectInfo> lProductInfo = new List<LogisticDetailSelectInfo>();

            

            int? totalRow = CountProductMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lProductInfo = GetProductMasterByCriteria(Code);
            Lbname.Text = (Request.QueryString["LogisticCode"] != null) ? Request.QueryString["LogisticCode"].ToString() : "";
            gvLogistic.DataSource = lProductInfo;

            gvLogistic.DataBind();


            

        }

        public List<LogisticDetailSelectInfo> GetProductMasterByCriteria(string Code)
        {
            

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLogisticDetailPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCodeDetail"] = Code;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LogisticDetailSelectInfo> lProductInfo = JsonConvert.DeserializeObject<List<LogisticDetailSelectInfo>>(respstr);


            return lProductInfo;

        }

        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountLogisticListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCode"] =   (Request.QueryString["LogisticCode"] != null) ? Request.QueryString["LogisticCode"].ToString() : "";

                

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }
        public List<LogisticDetailSelectInfo> GetLogisticDetailByCriteria(string code)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListLogisticDetailPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["LogisticCodeDetail"] = code;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<LogisticDetailSelectInfo> lProductInfo = JsonConvert.DeserializeObject<List<LogisticDetailSelectInfo>>(respstr);


            return lProductInfo;

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


            LoadLogisticDetail(LogisticCode);
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

        protected Boolean DeleteProduct()
        {

            for (int i = 0; i < gvLogistic.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvLogistic.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvLogistic.Rows[i].FindControl("hidLogisticDetailId");

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

                APIpath = APIUrl + "/api/support/DeleteLogisticDetail";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["ChannelIdDelete"] = Codelist;


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

        protected void gvProduct_Change(object sender, GridViewPageEventArgs e)
        {
            

        }

        protected void chkProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvLogistic.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvLogistic.HeaderRow.FindControl("chkProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvLogistic.Rows[i].FindControl("hidChannelId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProduct = (CheckBox)gvLogistic.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = true;
                }
                else
                {

                    CheckBox chkProduct = (CheckBox)gvLogistic.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            LoadLogisticDetail(LogisticCode);

        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteProduct();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }



        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    string respstr = "";
                    string respstrDetail = "";
                    APIpath = APIUrl + "/api/support/InsertLogisticDetail";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();
                        data["LogisticCodeDetail"] = (Request.QueryString["LogisticCode"] != null) ? Request.QueryString["LogisticCode"].ToString() : "";
                        data["Fee"] = txtFee_Ins.Text;
                        data["PackageWidth"] = txtPackageWidth_Ins.Text;
                        data["PackageLength"] = txtPackageLength_Ins.Text;
                        data["PackageHeigth"] = txtPackageHeigth_Ins.Text;
                        data["PackageWLHFrom"] = txtPackageWLHFrom_Ins.Text;
                        data["PackageWLHTo"] = txtPackageWLHTo_Ins.Text;
                        data["WeightFrom"] = txtWeightFrom_Ins.Text;
                        data["WeightTo"] = txtWeightTo_Ins.Text;
             
                        data["CreateBy"] = empInfo.EmpCode;
                 

                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                    

                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        LoadLogisticDetail((Request.QueryString["LogisticCode"] != null) ? Request.QueryString["LogisticCode"].ToString() : "");

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Logistic').modal('hide');", true);



                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }

                }
                else //Update
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateLogisticDetail";

                    using (var wb = new WebClient())
                    {
                        var dataDetail = new NameValueCollection();
                        dataDetail["LogisticCodeDetail"] = (Request.QueryString["LogisticCode"] != null) ? Request.QueryString["LogisticCode"].ToString() : "";
                        dataDetail["LogisticDetailId"] = hidIdList.Value;

                        dataDetail["Fee"] = txtFee_Ins.Text;
                        dataDetail["PackageWidth"] = txtPackageWidth_Ins.Text;
                        dataDetail["PackageLength"] = txtPackageLength_Ins.Text;
                        dataDetail["PackageHeigth"] = txtPackageHeigth_Ins.Text;
                        dataDetail["PackageWLHFrom"] = txtPackageWLHFrom_Ins.Text;
                        dataDetail["PackageWLHTo"] = txtPackageWLHTo_Ins.Text;
                        dataDetail["WeightFrom"] = txtWeightFrom_Ins.Text;
                        dataDetail["WeightTo"] = txtWeightTo_Ins.Text;

                        dataDetail["UpdateBy"] = empInfo.EmpCode;



                        var response = wb.UploadValues(APIpath, "POST", dataDetail);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {
                        btnCancel_Click(null, null);

                        LoadLogisticDetail((Request.QueryString["LogisticCode"] != null) ? Request.QueryString["LogisticCode"].ToString() : "");

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Logistic').modal('hide');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }

                }

            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtFee_Ins.Text = "";
            lblPackageWidth_Ins.Text = "";
           
            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Logistic').modal('hide');", true);
        }
        protected void btnEditSubmit_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            POInfo pInfo = new POInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo == null)
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");

            }
            else
            {
                if (hidFlagInsert.Value == "True") //Insert
                {
                    string respstr = "";
                    string respstrDetail = "";
                    APIpath = APIUrl + "/api/support/InsertLogistic";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["LogisticCode"] = txtFee_Ins.Text;
                        data["LogisticName"] = txtPackageWidth_Ins.Text;

                        data["CreateBy"] = empInfo.EmpCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);
                }
                else //Update
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateLogistic";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["LogisticId"] = hidIdList.Value;

                        data["LogisticCode"] = txtEditLogisCode.Text;
                        data["LogisticName"] = txtEditLogisName.Text;
                        data["UpdateBy"] = empInfo.EmpCode;



                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {
                        btnCancel_Click(null, null);

                        LoadLogisticDetail(LogisticCode);

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Logistic').modal('hide');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_ERROR + "');", true);
                    }

                }

            }

        }
        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            txtFee_Ins.Text = "";
            lblPackageWidth_Ins.Text = "";

            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-EditLogistic').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {

        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvLogistic.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidLogisticDetailId = (HiddenField)row.FindControl("hidLogisticDetailId");
            HiddenField hidLogisticCodeDetail = (HiddenField)row.FindControl("hidLogisticCodeDetail");
            HiddenField hidFee = (HiddenField)row.FindControl("hidFee");
            HiddenField HidPackageWidth = (HiddenField)row.FindControl("HidPackageWidth");
            HiddenField HidPackageLength = (HiddenField)row.FindControl("HidPackageLength");
            HiddenField HidPackageHeigth = (HiddenField)row.FindControl("HidPackageHeigth");
            HiddenField HidPackageWLHFrom = (HiddenField)row.FindControl("HidPackageWLHFrom");
            HiddenField HidPackageWLHTo = (HiddenField)row.FindControl("HidPackageWLHTo");
            HiddenField HidWeightFrom = (HiddenField)row.FindControl("HidWeightFrom");
            HiddenField HidWeightTo = (HiddenField)row.FindControl("HidWeightTo");
            if (e.CommandName == "ShowProduct")
            {
                txtFee_Ins.Text = hidFee.Value;
                txtPackageWidth_Ins.Text= HidPackageWidth.Value;
                txtPackageLength_Ins.Text= HidPackageLength.Value;
                txtPackageHeigth_Ins.Text= HidPackageHeigth.Value;
                txtPackageWLHFrom_Ins.Text= HidPackageWLHFrom.Value;
                txtPackageWLHTo_Ins.Text = HidPackageWLHTo.Value;
                txtWeightFrom_Ins.Text= HidWeightFrom.Value;
                txtWeightTo_Ins.Text= HidWeightTo.Value;
                hidIdList.Value = hidLogisticDetailId.Value;

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Logistic').modal();", true);

            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Logistic').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"ProductDetail.aspx?ProductCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

       
        #endregion


    }
}
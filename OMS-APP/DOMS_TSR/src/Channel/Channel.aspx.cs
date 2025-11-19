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
namespace DOMS_TSR.src.Channel
{
    public partial class Channel : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        string Codelist = "";
        string EditFlag = "";
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
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null && merchantInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                LoadChannel();

              }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            LoadChannel();
        }

        #region Function



        protected void LoadChannel()
        {
            List<ChannelInfo> lProductInfo = new List<ChannelInfo>();

            

            int? totalRow = CountProductMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lProductInfo = GetProductMasterByCriteria();

            gvChannel.DataSource = lProductInfo;

            gvChannel.DataBind();


            

        }

        public List<ChannelInfo> GetProductMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListChannelPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = txtSearchChannelCode.Text;

                data["ChannelName"] = txtSearchChannelName.Text;

                data["MerchantCode"] = hidMerCode.Value;
          
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ChannelInfo> lProductInfo = JsonConvert.DeserializeObject<List<ChannelInfo>>(respstr);


            return lProductInfo;

        }

        public int? CountProductMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountChannelListByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = txtSearchChannelCode.Text;

                data["ChannelName"] = txtSearchChannelName.Text;

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


            LoadChannel();
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

            for (int i = 0; i < gvChannel.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvChannel.Rows[i].FindControl("chkProduct");

                if (checkbox.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvChannel.Rows[i].FindControl("hidChannelId");

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

                APIpath = APIUrl + "/api/support/DeleteChannel";

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
            gvChannel.PageIndex = e.NewPageIndex;

            List<ChannelInfo> lProductInfo = new List<ChannelInfo>();

            lProductInfo = GetProductMasterByCriteria();

            gvChannel.DataSource = lProductInfo;

            gvChannel.DataBind();

        }

        protected void chkProductAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChannel.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvChannel.HeaderRow.FindControl("chkProductAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidCode = (HiddenField)gvChannel.Rows[i].FindControl("hidChannelId");

                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkProduct = (CheckBox)gvChannel.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = true;
                }
                else
                {

                    CheckBox chkProduct = (CheckBox)gvChannel.Rows[i].FindControl("chkProduct");

                    chkProduct.Checked = false;
                }

            }
            hidIdList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
            currentPageNumber = 1;
            LoadChannel();
            }


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
        protected string ValidateChannelID(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListChannelPagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ChannelCode_val"] = code;
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            return respstr;
        }
        protected Boolean validateInsert()
        {
            Boolean flag = true;
            string channelcode = "";


            if (txtChannelCode_Ins.Text == "")
            {
                flag = false;
                lblChannelCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสช่องทางการขาย";
            }else if (CheckSymbol(txtChannelCode_Ins.Text) == true)
            {
                flag = false;
                lblChannelCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อช่องทางการขายต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                channelcode = ValidateChannelID(txtChannelCode_Ins.Text);
                if (channelcode == "[]" || channelcode == "" || channelcode == null)
                {
                    flag = (flag == false) ? false : true;
                    lblChannelCode_Ins.Text = "";
                }
                else
                {
                    flag = false;
                    lblChannelCode_Ins.Text = "แชนแนลนี้มีอยู่แล้ว";
                }
               
            }  
            if (txtChannelName_Ins.Text == "")
            {
                flag = false;
                lblChannelName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อช่องทางการขาย";
            }
            else if (CheckSymbol(txtChannelName_Ins.Text) == true)
            {
                flag = false;
                lblChannelName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อช่องทางการขายต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblChannelName_Ins.Text = "";
            }
           
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPhone').modal();", true);
            }

            return flag;
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
                    if (validateInsert())
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertChannel";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["ChannelCode"] = txtChannelCode_Ins.Text;
                            data["ChannelName"] = txtChannelName_Ins.Text;
                            data["MerchantCode"] = hidMerCode.Value;
                            data["CreateBy"] = empInfo.EmpCode;


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            LoadChannel();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-Channel').modal('hide');", true);



                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
                else //Update
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdateChannel";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["ChannelId"] = hidIdList.Value;

                        data["ChannelCode"] = txtChannelCode_Ins.Text;
                        data["ChannelName"] = txtChannelName_Ins.Text;
                        data["UpdateBy"] = empInfo.EmpCode;



                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {


                        btnCancel_Click(null, null);

                        LoadChannel();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._UPDATE_SUCCESS + "');$('#modal-Channel').modal('hide');", true);



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
            txtChannelCode_Ins.Text = "";
            txtChannelName_Ins.Text = "";
           
            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Channel').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchChannelCode.Text = "";
            txtSearchChannelName.Text = "";
        }

        protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvChannel.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField hidChannelId = (HiddenField)row.FindControl("hidChannelId");
            HiddenField hidChannelCode = (HiddenField)row.FindControl("hidChannelCode");
            HiddenField hidChannelName = (HiddenField)row.FindControl("hidChannelName");

            if (e.CommandName == "ShowProduct")
            {
                txtChannelCode_Ins.Text = hidChannelCode.Value;
                txtChannelName_Ins.Text = hidChannelName.Value;
                 hidIdList.Value = hidChannelId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Channel').modal();", true);

            }

        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            
            hidFlagInsert.Value = "True";
            txtChannelCode_Ins.Text = "";
            txtChannelName_Ins.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Channel').modal();", true);
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
        protected Boolean validateSearch()
        {
            Boolean flag = true;

            if (CheckSymbol(txtSearchChannelCode.Text))
            {
                flag = false;
                lblSearchChannelCode.Text = MessageConst._MSG_PLEASEINSERT + " รหัสช่องทางการขายต้องไม่มีอักขระพิเศษ";

            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSearchChannelCode.Text = "";
            }
            if (CheckSymbol(txtSearchChannelName.Text))
            {
                flag = false;
                lblSearchChannelName.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อช่องทางการขายต้องไม่มีอักขระพิเศษ";

            }
            else
            {
                flag = (flag == false) ? false : true;
                lblSearchChannelName.Text = "";
            }

            return flag;
        }
        #endregion

        #region Binding

        protected string GetLink(object objCode, object Active)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"../MediaPlanManagement/MediaChannel.aspx?Channel=" + strCode + "&ChannelName=" + Active + "\">" + strCode + "</a>";
        }

       
        #endregion


    }
}
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

using System.IO;

namespace DOMS_TSR.src.SaleChannel
{
    public partial class SaleChannel : System.Web.UI.Page
    {
        
        protected static string SaleChannelImgUrl = ConfigurationManager.AppSettings["SaleChannelImageUrl"];
        protected static string APIUrl;
        string Idlist = "";
        string Codelist = "";
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


                loadSaleChannel();
                BindddlSearchChannel();
                BindStatus();
           
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadSaleChannel();
        }

        #region Function



        protected void loadSaleChannel()
        {
            List<SaleChannelInfo> lSaleChannelInfo = new List<SaleChannelInfo>();

            

            int? totalRow = CountSaleChannelMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lSaleChannelInfo = GetSaleChannelMasterByCriteria();

            gvSaleChannel.DataSource = lSaleChannelInfo;

            gvSaleChannel.DataBind();


            

        }

        public List<SaleChannelInfo> GetSaleChannelMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListSaleChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = ddlSearchSaleChannelChannel.SelectedValue;

                data["StatusActive"] = ddlSearchSaleChannelActive.SelectedValue;

                data["StartTime"] = txtSearchStartDateFrom.Text;

                data["EndTime"] = txtSearchStartDateTo.Text;

                data["SaleChannelCode"] = txtSaleChannelCode.Text;
                data["SaleChannelName"] = txtname.Text;
                data["SaleChannelPhone"] = txtPhone.Text;


                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<SaleChannelInfo> lSaleChannelInfo = JsonConvert.DeserializeObject<List<SaleChannelInfo>>(respstr);


            return lSaleChannelInfo;

        }

        public int? CountSaleChannelMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListSaleChannel";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = ddlSearchSaleChannelChannel.SelectedValue;

                data["StatusActive"] = ddlSearchSaleChannelActive.SelectedValue;

                data["StartTime"] = txtSearchStartDateFrom.Text;

                data["EndTime"] = txtSearchStartDateTo.Text;
                data["SaleChannelCode"] = txtSaleChannelCode.Text;
                data["SaleChannelName"] = txtname.Text;
                data["SaleChannelPhone"] = txtPhone.Text;




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


            loadSaleChannel();
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

        protected Boolean DeleteSaleChannel()
        {

            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            for (int i = 0; i < gvSaleChannel.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvSaleChannel.Rows[i].FindControl("chkSaleChannel");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvSaleChannel.Rows[i].FindControl("hidSaleChannelid");
             

                    if (Idlist != "")
                    {
                        Idlist += "," + hidId.Value + "";
                    }
                
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                      
                    }

                }
            }

            if (Idlist != "")
            {
               
                string respstr = "";

                APIpath = APIUrl + "/api/support/DeleteSaleChannel";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["UpdateBy"] = empInfo.EmpCode;
                    data["SaleChannelidList"] = Idlist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




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

        protected void gvSaleChannel_Change(object sender, GridViewPageEventArgs e)
        {
            gvSaleChannel.PageIndex = e.NewPageIndex;

            List<SaleChannelInfo> lSaleChannelInfo = new List<SaleChannelInfo>();

            lSaleChannelInfo = GetSaleChannelMasterByCriteria();

            gvSaleChannel.DataSource = lSaleChannelInfo;

            gvSaleChannel.DataBind();

        }

        protected void chkSaleChannelAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvSaleChannel.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvSaleChannel.HeaderRow.FindControl("chkSaleChannelAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvSaleChannel.Rows[i].FindControl("hidSaleChannelid");
                    HiddenField hidCode = (HiddenField)gvSaleChannel.Rows[i].FindControl("hidSaleChannelCode");

                    if (Idlist != "")
                    {
                        Idlist += ",'" + hidId.Value + "'";
                    }
                    if (Codelist != "")
                    {
                        Codelist += ",'" + hidCode.Value + "'";
                    }
                    else
                    {
                        Idlist += "'" + hidId.Value + "'";
                        Codelist += "'" + hidCode.Value + "'";
                    }

                    CheckBox chkSaleChannel = (CheckBox)gvSaleChannel.Rows[i].FindControl("chkSaleChannel");

                    chkSaleChannel.Checked = true;
                }
                else
                {

                    CheckBox chkSaleChannel = (CheckBox)gvSaleChannel.Rows[i].FindControl("chkSaleChannel");

                    chkSaleChannel.Checked = false;
                }

            }
            hidIdList.Value = Idlist;
            hidCodeList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            loadSaleChannel();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteSaleChannel();

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
                if (validateInsert())
                {
                    if (hidFlagInsert.Value == "True") //Insert
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/InsertSaleChannel";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();
                            data["ChannelCode"] = ddlChannel_Ins.SelectedValue;                          
                            data["StatusActive"] = ddlStatusActive_Ins.SelectedValue;                      
                            data["StartTime"] = txtStartDate_Ins.Text;
                            data["EndTime"] = txtEndDate_Ins.Text;
                            data["SaleChannelCode"] = txtsalechannelcode_ins.Text;
                            data["SaleChannelName"] = txtsalechannelname_ins.Text;
                            data["SaleChannelPhone"] = txtsalechannelPhone_ins.Text;


                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;
                         

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            loadSaleChannel();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-SaleChannel').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    else //Update
                    {
                      

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateSaleChannel";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["SaleChannelid"] = hidIdList.Value;

                            data["ChannelCode"] = ddlChannel_Ins.SelectedValue;
                            data["StatusActive"] = ddlStatusActive_Ins.SelectedValue;
                            data["StartTime"] = txtStartDate_Ins.Text;
                            data["EndTime"] = txtEndDate_Ins.Text;
                            data["SaleChannelCode"] = txtsalechannelcode_ins.Text;
                            data["SaleChannelName"] = txtsalechannelname_ins.Text;
                            data["SaleChannelPhone"] = txtsalechannelPhone_ins.Text;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                        
                            loadSaleChannel();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-SaleChannel').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SaleChannel').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
          
            ddlSearchSaleChannelChannel.ClearSelection();
            ddlSearchSaleChannelActive.ClearSelection();
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
         
        }
        protected void gvSaleChannel_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                    
                
            }
        }
        protected void gvSaleChannel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvSaleChannel.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField HidSaleChannelid = (HiddenField)row.FindControl("HidSaleChannelid");
            HiddenField hidChannelCode = (HiddenField)row.FindControl("hidChannelCode");
            HiddenField hidStatusActive = (HiddenField)row.FindControl("hidStatusActive");
        
            HiddenField hidStartTime = (HiddenField)row.FindControl("hidStartTime");
            HiddenField hidEndTime = (HiddenField)row.FindControl("hidEndTime");


            HiddenField HidSaleChannelCode = (HiddenField)row.FindControl("HidSaleChannelCode");
            HiddenField HidSaleChannelName = (HiddenField)row.FindControl("HidSaleChannelName");
            HiddenField HidSaleChannelPhone = (HiddenField)row.FindControl("HidSaleChannelPhone");
            if (e.CommandName == "ShowSaleChannel")
            {


               
                ddlChannel_Ins.SelectedValue = hidChannelCode.Value;
                ddlStatusActive_Ins.SelectedValue = "";
                txtStartDate_Ins.Text = hidStartTime.Value;
                txtEndDate_Ins.Text = hidEndTime.Value;
                ddlStatusActive_Ins.SelectedValue = hidStatusActive.Value;
                hidIdList.Value = HidSaleChannelid.Value;
                txtsalechannelname_ins.Text = HidSaleChannelName.Value;
                txtsalechannelPhone_ins.Text = HidSaleChannelPhone.Value;
                txtsalechannelcode_ins.Text = HidSaleChannelCode.Value;

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SaleChannel').modal();", true);

            }
            else if (e.CommandName == "Download") 
            {

            }

        }

        protected void btnAddSaleChannel_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SaleChannel').modal();", true);
        }

        protected void ddlSaleChannelTypeIns_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }

        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        protected void ShowComboSection(string flag)
        {
          

        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"SaleChannelDetail.aspx?SaleChannelCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindStatus()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("---- กรุณาเลือก ----", ""));
            items.Add(new ListItem(StaticField.ActiveFlag_Y_NameValue_Active, StaticField.ActiveFlag_Y)); //"Active", "Y"
            items.Add(new ListItem(StaticField.ActiveFlag_N_NameValue_Inactive, StaticField.ActiveFlag_N)); //"Inactive", "N"

            ddlSearchSaleChannelActive.Items.AddRange(items.ToArray());
            ddlStatusActive_Ins.Items.AddRange(items.ToArray());
        }
        protected void BindddlSearchChannel()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<ChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ChannelInfo>>(respstr);

            
            ddlSearchSaleChannelChannel.DataSource = lLookupInfo;

            ddlSearchSaleChannelChannel.DataTextField = "ChannelName";

            ddlSearchSaleChannelChannel.DataValueField = "ChannelCode";

            ddlSearchSaleChannelChannel.DataBind();

            ddlSearchSaleChannelChannel.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));


            ddlChannel_Ins.DataSource = lLookupInfo;

            ddlChannel_Ins.DataTextField = "ChannelName";

            ddlChannel_Ins.DataValueField = "ChannelCode";

            ddlChannel_Ins.DataBind();

            ddlChannel_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

      

        
        protected Boolean validateInsert()
        {
            Boolean flag = true;


            if (ddlChannel_Ins.SelectedValue == "-99" || ddlChannel_Ins.SelectedValue == "")
            {
                flag = false;
                lblChannel_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Channel";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblChannel_Ins.Text = "";
            }

            if (txtStartDate_Ins.Text == "")
            {
                flag = false;
                lblStartDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " วันเริ่ม";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblStartDate_Ins.Text = "";
            }
            if (txtEndDate_Ins.Text == "")
            {
                flag = false;
                lblEndDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " วันสิ้นสุด";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblEndDate_Ins.Text = "";
            }
          
            if (ddlStatusActive_Ins.SelectedValue == "-99" || ddlStatusActive_Ins.SelectedValue == "")
            {
                flag = false;
                lbStatusActive_Ins.Text = MessageConst._MSG_PLEASEINSERT + " สถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbStatusActive_Ins.Text = "";
            }
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-SaleChannel').modal();", true);
            }

            return flag;
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
        #endregion


    }
}
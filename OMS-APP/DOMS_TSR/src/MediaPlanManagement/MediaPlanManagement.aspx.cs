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

namespace DOMS_TSR.src.MediaPlanManagement
{
    public partial class MediaPlanManagement : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string MediaOnAirImgUrl = ConfigurationManager.AppSettings["MediaOnAirImageUrl"];

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
                    APIUrl = empInfo.ConnectionAPI;

                    hidEmpCode.Value = empInfo.EmpCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                loadMediaOnAir();
                BindddlSearchChannel();
                BindStatus();
                BindddlSearchMediaSaleChannel();
                


            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadMediaOnAir();
        }

        #region Function



        protected void loadMediaOnAir()
        {
            List<MediaOnAirInfo> lMediaOnAirInfo = new List<MediaOnAirInfo>();

            

            int? totalRow = CountMediaOnAirMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lMediaOnAirInfo = GetMediaOnAirMasterByCriteria();

            gvMediaOnAir.DataSource = lMediaOnAirInfo;

            gvMediaOnAir.DataBind();


            

        }

        public List<MediaOnAirInfo> GetMediaOnAirMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMediaOnAirNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = ddlSearchMediaOnAirChannel.SelectedValue;

                data["StatusActive"] = ddlSearchMediaOnAirActive.SelectedValue;

                data["StartTime"] = txtSearchStartDateFrom.Text;

                data["EndTime"] = txtSearchStartDateTo.Text;

                
                

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MediaOnAirInfo> lMediaOnAirInfo = JsonConvert.DeserializeObject<List<MediaOnAirInfo>>(respstr);


            return lMediaOnAirInfo;

        }

        public int? CountMediaOnAirMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListMediaOnAir";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = ddlSearchMediaOnAirChannel.SelectedValue;

                data["StatusActive"] = ddlSearchMediaOnAirActive.SelectedValue;

                data["StartTime"] = txtSearchStartDateFrom.Text;

                data["EndTime"] = txtSearchStartDateTo.Text;
             

        

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


            loadMediaOnAir();
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

        protected Boolean DeleteMediaOnAir()
        {

            for (int i = 0; i < gvMediaOnAir.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvMediaOnAir.Rows[i].FindControl("chkMediaOnAir");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvMediaOnAir.Rows[i].FindControl("hidMediaOnAirId");
             

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

                APIpath = APIUrl + "/api/support/DeleteMediaOnAir";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["MediaOnAirId"] = Idlist;


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

        protected void gvMediaOnAir_Change(object sender, GridViewPageEventArgs e)
        {
            gvMediaOnAir.PageIndex = e.NewPageIndex;

            List<MediaOnAirInfo> lMediaOnAirInfo = new List<MediaOnAirInfo>();

            lMediaOnAirInfo = GetMediaOnAirMasterByCriteria();

            gvMediaOnAir.DataSource = lMediaOnAirInfo;

            gvMediaOnAir.DataBind();

        }

        protected void chkMediaOnAirAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMediaOnAir.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvMediaOnAir.HeaderRow.FindControl("chkMediaOnAirAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvMediaOnAir.Rows[i].FindControl("hidMediaOnAirId");
                    HiddenField hidCode = (HiddenField)gvMediaOnAir.Rows[i].FindControl("hidMediaOnAirCode");

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

                    CheckBox chkMediaOnAir = (CheckBox)gvMediaOnAir.Rows[i].FindControl("chkMediaOnAir");

                    chkMediaOnAir.Checked = true;
                }
                else
                {

                    CheckBox chkMediaOnAir = (CheckBox)gvMediaOnAir.Rows[i].FindControl("chkMediaOnAir");

                    chkMediaOnAir.Checked = false;
                }

            }
            hidIdList.Value = Idlist;
            hidCodeList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            loadMediaOnAir();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteMediaOnAir();

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

                        APIpath = APIUrl + "/api/support/InsertMediaOnAir";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();
                            data["ChannelCode"] = ddlChannel_Ins.SelectedValue;                          
                            data["StatusActive"] = ddlStatusActive_Ins.SelectedValue;                      
                            data["StartTime"] = txtStartDate_Ins.Text;
                            data["EndTime"] = txtEndDate_Ins.Text;
                            data["MediaPhone"] = txtMediaPhone_Ins.Text;
                            data["MediaSaleChannel"] = ddlMediSaleChannel_Ins.SelectedValue;
                            data["FlagDelete"] = "N";
                            data["CreateBy"] = empInfo.EmpCode;
                         

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {


                            btnCancel_Click(null, null);

                            loadMediaOnAir();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaOnAir').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    else //Update
                    {
                      

                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdateMediaOnAir";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["MediaOnAirId"] = hidIdList.Value;

                            data["ChannelCode"] = ddlChannel_Ins.SelectedValue;
                            data["StatusActive"] = ddlStatusActive_Ins.SelectedValue;
                            data["StartTime"] = txtStartDate_Ins.Text;
                            data["EndTime"] = txtEndDate_Ins.Text;
                            data["MediaPhone"] = txtMediaPhone_Ins.Text;
                            data["MediaSaleChannel"] = ddlMediSaleChannel_Ins.SelectedValue;
                            data["CreateBy"] = empInfo.EmpCode;                      
                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                        
                            loadMediaOnAir();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaOnAir').modal('hide');", true);
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
            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaOnAir').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
          
            ddlSearchMediaOnAirChannel.ClearSelection();
            ddlSearchMediaOnAirActive.ClearSelection();
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
         
        }
        protected void gvMediaOnAir_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                    
                
            }
        }
        protected void gvMediaOnAir_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvMediaOnAir.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField HidMediaOnAirId = (HiddenField)row.FindControl("HidMediaOnAirId");
            HiddenField hidChannelCode = (HiddenField)row.FindControl("hidChannelCode");
            HiddenField hidStatusActive = (HiddenField)row.FindControl("hidStatusActive");
        
            HiddenField hidStartTime = (HiddenField)row.FindControl("hidStartTime");
            HiddenField hidEndTime = (HiddenField)row.FindControl("hidEndTime");
            HiddenField HidMediaphone = (HiddenField)row.FindControl("HidMediaphone");
            HiddenField HidMediasalechannelcode = (HiddenField)row.FindControl("HidMediasalechannelcode");
            if (e.CommandName == "ShowMediaOnAir")
            {


               
                ddlChannel_Ins.SelectedValue = hidChannelCode.Value;
                ddlStatusActive_Ins.SelectedValue = "";
                txtStartDate_Ins.Text = hidStartTime.Value;
                txtEndDate_Ins.Text = hidEndTime.Value;
                ddlStatusActive_Ins.SelectedValue = hidStatusActive.Value;
                txtMediaPhone_Ins.Text= HidMediaphone.Value;
                ddlMediSaleChannel_Ins.SelectedValue = HidMediasalechannelcode.Value;
                hidIdList.Value = HidMediaOnAirId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaOnAir').modal();", true);

            }
            else if (e.CommandName == "Download") 
            {

            }

        }

        protected void btnAddMediaOnAir_Click(object sender, EventArgs e)
        {

            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaOnAir').modal();", true);
        }

        protected void ddlMediaOnAirTypeIns_SelectedIndexChanged(object sender, EventArgs e)
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
            return "<a href=\"MediaOnAirDetail.aspx?MediaOnAirCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

        protected void BindStatus()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("---- กรุณาเลือก ----", ""));
            items.Add(new ListItem("Active", "Y"));
            items.Add(new ListItem("Inactive", "N"));
           
            ddlSearchMediaOnAirActive.Items.AddRange(items.ToArray());
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

            
            ddlSearchMediaOnAirChannel.DataSource = lLookupInfo;

            ddlSearchMediaOnAirChannel.DataTextField = "ChannelName";

            ddlSearchMediaOnAirChannel.DataValueField = "ChannelCode";

            ddlSearchMediaOnAirChannel.DataBind();

            ddlSearchMediaOnAirChannel.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));


            ddlChannel_Ins.DataSource = lLookupInfo;

            ddlChannel_Ins.DataTextField = "ChannelName";

            ddlChannel_Ins.DataValueField = "ChannelCode";

            ddlChannel_Ins.DataBind();

            ddlChannel_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
        }

        protected void BindddlSearchMediaSaleChannel()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListMediaSaleChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["ChannelCode"] = null;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<MediaSaleChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<MediaSaleChannelInfo>>(respstr);
            ddlMediSaleChannel_Ins.DataSource = lLookupInfo;

            ddlMediSaleChannel_Ins.DataTextField = "NAME_TH";

            ddlMediSaleChannel_Ins.DataValueField = "CODE";

            ddlMediSaleChannel_Ins.DataBind();

            ddlMediSaleChannel_Ins.Items.Insert(0, new ListItem("---- กรุณาเลือก ----", "-99"));
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaOnAir').modal();", true);
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
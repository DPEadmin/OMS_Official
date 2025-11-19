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
using OfficeOpenXml;
using System.IO.Packaging;
using System.Globalization;

namespace DOMS_TSR.src.MediaPlanManagement
{
    public partial class MediaPlan : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string MediaPlanImgUrl = ConfigurationManager.AppSettings["MediaPlanImageUrl"];

        string Idlist = "";
        string Codelist = "";
        Boolean isdelete;
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        L_MediaPlan result = new L_MediaPlan();
        string APIpath = "";

        string calStartdate = "";
        string calEndDate=  "";
        string calstarttime = "";
        string calEndtime = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantInfo = new MerchantInfo();

                empInfo = (EmpInfo)Session["EmpInfo"];
                merchantInfo = (MerchantInfo)Session["MerchantInfo"];

                if (empInfo != null)
                {
                    
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerCode.Value = merchantInfo.MerchantCode;
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                
                BindddlCampaign();
                BindddlMediaphone();
                
                loadMediaPlan();
                BindddlSearchChannel();
                BindddlMediaProgram();
                
                ddlMediaChannel_ins.Enabled = false;
            }
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadMediaPlan();
        }
        protected void ddlCamp_Ins_select(object sender, EventArgs e)
        {
            
            BindddlCampaignByselect(ddlCamp_Ins.SelectedValue);
        }
        protected void ddlChannel_Ins_select(object sender, EventArgs e)
        {
            BindddlMediaChannel(ddlChannel_Ins.SelectedValue);
        }
        protected void ddlProgramName_Ins_select(object sender, EventArgs e)
        {
            BindddlMediaChannelByselect(ddlMediaChannel_ins.SelectedValue);
        }
        #region Function
        


        protected void loadMediaPlan()
        {
            List<MediaPlanInfo> lMediaPlanInfo = new List<MediaPlanInfo>();

            

            int? totalRow = CountMediaPlanMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lMediaPlanInfo = GetMediaPlanMasterByCriteria();

            gvlMediaPlan.DataSource = lMediaPlanInfo;

            gvlMediaPlan.DataBind();


            

        }

        public List<MediaPlanInfo> GetMediaPlanMasterByCriteria()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaPlanNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ProgramName"] = txtProgramName.Text;
                data["MediaPlanDateStart"] = txtSearchStartDateFrom.Text;
                data["MediaPlanDateEnd"] = txtSearchStartDateTo.Text;
                data["MerchantCode"] = hidMerCode.Value;
                data["SALE_CHANNEL"] = ddlSearchMediaPlanChannel.SelectedValue;
                if (txthhstart.Text!=""&& txtmmstart.Text!="")
                {
                    data["TIME_START"] = txthhstart.Text + ":" + txtmmstart.Text;
                }
                else 
                {
                    data["TIME_START"] = "";
                }
                if (txthhEnd.Text != "" && txtmmEnd.Text != "")
                {
                    data["TIME_END"] = txthhEnd.Text + ":" + txtmmEnd.Text;
                }
                else
                {
                    data["TIME_END"] = "";
                }
                if (ddlMediaphone.SelectedValue == "-99")
                {
                    data["MediaPhone"] = "";
                }
                else
                {
                    data["MediaPhone"] = ddlMediaphone.SelectedValue;
                }

                data["Duration"] = txtDuration.Text;
                data["Active"] = ddlactive.SelectedValue;
                data["CampaignCode"] = ddlCamp.SelectedValue;
                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();
                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<MediaPlanInfo> lMediaPlanInfo = JsonConvert.DeserializeObject<List<MediaPlanInfo>>(respstr);
            return lMediaPlanInfo;
        }

        public int? CountMediaPlanMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountListMediaPlan";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ProgramName"] = txtProgramName.Text;
                data["MediaPlanDateStart"] = txtSearchStartDateFrom.Text;
                data["MediaPlanDateEnd"] = txtSearchStartDateTo.Text;
                data["SALE_CHANNEL"] = ddlSearchMediaPlanChannel.SelectedValue;
                if (txthhstart.Text != "" && txtmmstart.Text != "")
                {
                    data["TIME_START"] = txthhstart.Text + ":" + txtmmstart.Text;
                }
                else
                {
                    data["TIME_START"] = "";
                }
                if (txthhEnd.Text != "" && txtmmEnd.Text != "")
                {
                    data["TIME_END"] = txthhEnd.Text + ":" + txtmmEnd.Text;
                }
                else
                {
                    data["TIME_END"] = "";
                }
                if (ddlMediaphone.SelectedValue == "-99")
                {
                    data["MediaPhone"] = "";
                }
                else
                {
                    data["MediaPhone"] = ddlMediaphone.SelectedValue;
                }
                data["Duration"] = txtDuration.Text;
                data["Active"] = ddlactive.SelectedValue;
                data["CampaignCode"] = ddlCamp.SelectedValue;

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
            loadMediaPlan();
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

        protected Boolean DeleteMediaPlan()
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            for (int i = 0; i < gvlMediaPlan.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvlMediaPlan.Rows[i].FindControl("chkMediaPlan");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvlMediaPlan.Rows[i].FindControl("hidMediaPlanId");


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
                APIpath = APIUrl + "/api/support/DeleteMediaPlan";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["UpdateBy"] = empInfo.EmpCode;
                    data["MediaPlanidList"] = Idlist;

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

        protected void gvlMediaPlan_Change(object sender, GridViewPageEventArgs e)
        {
            gvlMediaPlan.PageIndex = e.NewPageIndex;
            List<MediaPlanInfo> lMediaPlanInfo = new List<MediaPlanInfo>();
            lMediaPlanInfo = GetMediaPlanMasterByCriteria();
            gvlMediaPlan.DataSource = lMediaPlanInfo;
            gvlMediaPlan.DataBind();
        }

        protected void chkMediaPlanAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvlMediaPlan.Rows.Count; i++)
            {
                CheckBox chkall = (CheckBox)gvlMediaPlan.HeaderRow.FindControl("chkMediaPlanAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvlMediaPlan.Rows[i].FindControl("hidMediaPlanid");
                    

                    if (Idlist != "")
                    {
                        Idlist += ",'" + hidId.Value + "'";
                    }
                    
                    else
                    {
                        Idlist += "'" + hidId.Value + "'";
                        
                    }

                    CheckBox chkMediaPlan = (CheckBox)gvlMediaPlan.Rows[i].FindControl("chkMediaPlan");
                    chkMediaPlan.Checked = true;
                }
                else
                {
                    CheckBox chkMediaPlan = (CheckBox)gvlMediaPlan.Rows[i].FindControl("chkMediaPlan");
                    chkMediaPlan.Checked = false;
                }
            }
            hidIdList.Value = Idlist;
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadMediaPlan();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteMediaPlan();
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
            
            MerchantInfo merchantInfo = new MerchantInfo();
            merchantInfo = (MerchantInfo)Session["MerchantInfo"];
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
                        APIpath = APIUrl + "/api/support/InsertMediaPlan";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["MediaPlanDate"] = txtMediaPlanDate_Ins.Text;
                            data["MediaPlanEndDate"] = txtMediaPlanEndDate_Ins.Text;
                            data["MediaPlanTime"] = txtMediaPlanTimeHH_Ins.Text+":"+ txtMediaPlanTimeMM_Ins.Text;
                            data["MerchantCode"] = merchantInfo.MerchantCode;
                            
                            data["ProgramName"] = TxtProgramName_Ins.Text;
                            data["SALE_CHANNEL"] = ddlChannel_Ins.SelectedValue;
                            data["Duration"] = txtDuration_Ins.Text;
                            data["MediaPhone"] = ddlMediaPhone_Ins.SelectedValue;
                            data["CampaignCode"] = ddlCamp_Ins.SelectedValue;

                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";

                            data["TIME_START"] = txtMediaPlanTimeHH_Ins.Text +":"+ txtMediaPlanTimeMM_Ins.Text;
                            data["TIME_END"] = txtTimeEndHH_ins.Text + ":" + txtTimeEndmm_ins.Text;
                            data["Active"] = ddlActive_Ins.SelectedValue;
                            data["MEDIA_CHANNEL"] = ddlMediaChannel_ins.SelectedValue;
                            data["DelayStartTime"] = DelayStart.Text;
                            data["DelayEndTime"] = DelayEnd.Text;
                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);
                            loadMediaPlan();
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaPlan').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
                    else //Update
                    {
                        string respstr = "";
                        APIpath = APIUrl + "/api/support/UpdateMediaPlan";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();
                            data["MediaPlanId"] = hidIdList.Value;
                            data["MediaPlanDate"] = txtMediaPlanDate_Ins.Text;
                            data["MediaPlanEndDate"] = txtMediaPlanEndDate_Ins.Text;
                            data["MediaPlanTime"] = txtMediaPlanTimeHH_Ins.Text + ":" + txtMediaPlanTimeMM_Ins.Text;
                            
                            data["ProgramName"] = TxtProgramName_Ins.Text;
                            data["SALE_CHANNEL"] = ddlChannel_Ins.SelectedValue;
                            data["Duration"] = txtDuration_Ins.Text;
                            data["MediaPhone"] = ddlMediaPhone_Ins.SelectedValue;
                            data["MerchantCode"] = merchantInfo.MerchantCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["TIME_START"] = txtMediaPlanTimeHH_Ins.Text + ":" + txtMediaPlanTimeMM_Ins.Text;
                            data["TIME_END"] = txtTimeEndHH_ins.Text + ":" + txtTimeEndmm_ins.Text;
                            data["Active"] = ddlActive_Ins.SelectedValue;
                            data["MEDIA_CHANNEL"] = ddlMediaChannel_ins.SelectedValue;
                            data["CampaignCode"] = ddlCamp_Ins.SelectedValue;
                            var response = wb.UploadValues(APIpath, "POST", data);
                            respstr = Encoding.UTF8.GetString(response);
                        }
                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);

                        if (sum > 0)
                        {
                            loadMediaPlan();
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaPlan').modal('hide');", true);
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



        protected void Start_time(object sender, EventArgs e)
        {
           


        }




        protected void End_Time(object sender, EventArgs e)
        {
            if (txtMediaPlanTimeMM_Ins.Text != "" && txtMediaPlanTimeHH_Ins.Text != "" && txtTimeEndHH_ins.Text != "" && DelayStart.Text !="" && DelayEnd.Text !="" && txtTimeEndmm_ins.Text !="" && txtMediaPlanDate_Ins.Text !="" && txtMediaPlanEndDate_Ins.Text !="") {
                int finalcalculate;
                DateTime start_Date = DateTime.ParseExact(txtMediaPlanDate_Ins.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime End_Date = DateTime.ParseExact(txtMediaPlanEndDate_Ins.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
           
                TimeSpan diffResult = End_Date - start_Date;

              int StartTimeHH = Convert.ToInt32(txtMediaPlanTimeHH_Ins.Text)*60;
              int StartTimemm = Convert.ToInt32(txtMediaPlanTimeMM_Ins.Text) ;
              int EndTimeHH = Convert.ToInt32(txtTimeEndHH_ins.Text) * 60;
              int EndTimemm = Convert.ToInt32(txtTimeEndmm_ins.Text) ;
              int DelayStartMM = Convert.ToInt32(DelayStart.Text);
                int DelayEndMM = Convert.ToInt32(DelayEnd.Text);
                int days = (int)diffResult.TotalDays;

                if (days != 0)
                {

                    int total = (days * 24) * 60;
                    finalcalculate = (EndTimeHH + EndTimemm) - (StartTimeHH + StartTimemm) + total   + DelayStartMM  + DelayEndMM;
                    txtDuration_Ins.Text = finalcalculate.ToString();
                }

                else
                {

                    finalcalculate = (EndTimeHH + EndTimemm) - (StartTimeHH + StartTimemm) + DelayStartMM + DelayEndMM;
                    txtDuration_Ins.Text = finalcalculate.ToString();

                }

            }



        }






        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlCamp_Ins.ClearSelection();
            txtMediaPlanDate_Ins.Text = "";
            DelayStart.Text = "";
            DelayEnd.Text = "";
            txtMediaPlanTimeHH_Ins.Text = "";
            txtMediaPlanTimeMM_Ins.Text = "";
            ddlChannel_Ins.ClearSelection();
            txtDuration_Ins.Text = "";
            ddlMediaPhone_Ins.ClearSelection();
            txtCampName_ins.Text = "";
            TxtProgramName_Ins.Text = "";
            txtMediaPlanTimeHH_Ins.Text = "";
            txtMediaPlanTimeMM_Ins.Text = "";
            txtTimeEndHH_ins.Text = "";
            txtTimeEndmm_ins.Text = "";
            ddlActive_Ins.ClearSelection();
            ddlMediaChannel_ins.ClearSelection();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            ddlCamp.ClearSelection();
            txtProgramName.Text = "";
            txtSearchStartDateFrom.Text = "";
            txtSearchStartDateTo.Text = "";
            ddlSearchMediaPlanChannel.ClearSelection();
            txthhstart.Text = "";
            txtmmstart.Text = "";
            txthhEnd.Text = "";
            txtmmEnd.Text = "";
            ddlMediaphone.ClearSelection();
            txtDuration.Text = "";
            ddlactive.ClearSelection();
            DelayStart.Text = "";
            DelayEnd.Text = "";
        }
        protected void btnClearbeforeEdit()
        {

            ddlCamp_Ins.ClearSelection();
            txtMediaPlanDate_Ins.Text = "";
            txtMediaPlanTimeHH_Ins.Text = "";
            txtMediaPlanTimeMM_Ins.Text = "";
            ddlChannel_Ins.ClearSelection();
            txtDuration_Ins.Text = "";
            ddlMediaPhone_Ins.ClearSelection();
            txtCampName_ins.Text = "";
            TxtProgramName_Ins.Text = "";
            txtMediaPlanTimeHH_Ins.Text = "";
            txtMediaPlanTimeMM_Ins.Text = "";
            txtTimeEndHH_ins.Text = "";
            txtTimeEndmm_ins.Text = "";
            ddlActive_Ins.ClearSelection();
            ddlMediaChannel_ins.ClearSelection();
            ddlMediaChannel_ins.Items.Clear();
            lblMediaPlanDate_Ins.Text = "";
            lblMediaPlanTimeHH_Ins.Text = "";
            lblMediaPlanTimeMM_Ins.Text = "";
            lblTimeEndHH_ins.Text = "";
            lblTimeEndmm_ins.Text = "";
            lblDuration_Ins.Text = "";
            lblMediaPhone_Ins.Text = "";
            lblChannel_Ins.Text = "";
            lblMediaChannel_ins.Text = "";
            lblProgramName_Ins.Text = "";
            lblddlCamp_Ins.Text = "";
            Label2.Text = "";
            Label1.Text = "";
            lblDelayStart.Text = "";
            lblDelayEnd.Text = "";
            DelayStart.Text = "";
            DelayEnd.Text = "";
        }
        protected void gvlMediaPlan_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                

            }
        }
        protected void gvlMediaPlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            empInfo = (EmpInfo)Session["EmpInfo"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvlMediaPlan.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField HidMediaPlanid = (HiddenField)row.FindControl("HidMediaPlanid");
            HiddenField HidProgramName = (HiddenField)row.FindControl("HidProgramName");
            HiddenField HidTimeStart = (HiddenField)row.FindControl("HidTimeStart");
            HiddenField HidTimeEnd = (HiddenField)row.FindControl("HidTimeEnd");
            HiddenField HidMediaPlanDate = (HiddenField)row.FindControl("HidMediaPlanDate");
            HiddenField HidMediaPlanEndDate = (HiddenField)row.FindControl("HidMediaPlanEndDate");
            HiddenField HidMEDIA_CHANNEL = (HiddenField)row.FindControl("HidMEDIA_CHANNEL");
            Label lblMediaPhone = (Label)row.FindControl("lblMediaPhone");
            
            HiddenField HidMediaPlanTimeHH = (HiddenField)row.FindControl("HidMediaPlanTimeHH");
            HiddenField HidMERCHANT_NAME = (HiddenField)row.FindControl("HidMERCHANT_NAME");
            HiddenField HidDuration = (HiddenField)row.FindControl("HidDuration");
            HiddenField HidMediaPhone = (HiddenField)row.FindControl("HidMediaPhone");

            HiddenField hidChannelCode = (HiddenField)row.FindControl("hidChannelCode");
            HiddenField hidStatusActive = (HiddenField)row.FindControl("hidStatusActive");
            HiddenField HidMediaPlanCode = (HiddenField)row.FindControl("HidMediaPlanCode");
            HiddenField HidMediaPlanName = (HiddenField)row.FindControl("HidMediaPlanName");
            HiddenField HidMediaPlanDuration = (HiddenField)row.FindControl("HidMediaPlanDuration");
            HiddenField HidCampaignCode = (HiddenField)row.FindControl("HidCampaignCode");
            HiddenField HidPromotionCode = (HiddenField)row.FindControl("HidPromotionCode");
            HiddenField HidSaleChannelCode = (HiddenField)row.FindControl("HidSaleChannelCode");
            HiddenField HidActive = (HiddenField)row.FindControl("HidActive");
            HiddenField HidMerCode = (HiddenField)row.FindControl("HidMerCode");


            HiddenField HiddenDelayStarttime = (HiddenField)row.FindControl("HiddenDelayStarttime");
            HiddenField HiddenDelayEndtime = (HiddenField)row.FindControl("HiddenDelayEndtime");


            if (e.CommandName == "ShowMediaPlan")
            {
                btnClearbeforeEdit();
                hidIdList.Value = HidMediaPlanid.Value;
                txtMediaPlanDate_Ins.Text = HidMediaPlanDate.Value;
                txtMediaPlanEndDate_Ins.Text = HidMediaPlanEndDate.Value;
             
                txtDuration_Ins.Text = HidDuration.Value;

                DelayStart.Text = HiddenDelayStarttime.Value;
                DelayEnd.Text = HiddenDelayEndtime.Value;
                string mediaphone = "";
                string mediachannel = "";
                mediaphone = BindddlMediaphoneByselect(HidMediaPhone.Value);
                if (mediaphone == "[]" || mediaphone == "" || mediaphone == null)
                {
                   ddlMediaPhone_Ins.SelectedValue = "-99";
                }
                else
                { 
                
                   ddlMediaPhone_Ins.SelectedValue = HidMediaPhone.Value;
                }
                string[] HourStarttime = HidTimeStart.Value.Split(':');
                txtMediaPlanTimeHH_Ins.Text = HourStarttime[0].ToString();
                txtMediaPlanTimeMM_Ins.Text = HourStarttime[1].ToString();
                string[] HourEndtime = HidTimeEnd.Value.Split(':');
                txtTimeEndHH_ins.Text = HourEndtime[0].ToString();
                txtTimeEndmm_ins.Text = HourEndtime[1].ToString();

                ddlChannel_Ins.SelectedValue = hidChannelCode.Value;

                mediachannel = BindddlMediachannelByselect(hidChannelCode.Value, HidMEDIA_CHANNEL.Value);
                if (mediachannel == "[]" || mediachannel == "" || mediachannel == null)
                {
                    ddlMediaChannel_ins.Enabled = true;
                    BindddlMediaChannel(ddlChannel_Ins.SelectedValue);
                    ddlMediaChannel_ins.SelectedValue = "-99";
                }
                else
                {
                    ddlMediaChannel_ins.Enabled = true;
                    ddlMediaChannel_ins.SelectedValue = HidMEDIA_CHANNEL.Value;
                }


                ddlCamp_Ins.SelectedValue = HidCampaignCode.Value;
                ddlCamp_Ins_select(ddlCamp_Ins.SelectedValue, null);
            
                TxtProgramName_Ins.Text = HidProgramName.Value.ToString();
                ddlActive_Ins.SelectedValue= HidActive.Value.ToString();
                hidFlagInsert.Value = "False";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal();", true);

            }
            else if (e.CommandName == "Download")
            {

            }
        }

        protected void btnAddMediaPlan_Click(object sender, EventArgs e)
        {
            ddlCamp_Ins.ClearSelection();
            txtMediaPlanDate_Ins.Text = "";
            txtMediaPlanTimeHH_Ins.Text = "";
            txtMediaPlanTimeMM_Ins.Text = "";
            ddlChannel_Ins.ClearSelection();
            txtDuration_Ins.Text = "";
            ddlMediaPhone_Ins.ClearSelection();


            TxtProgramName_Ins.Text = "";
            txtMediaPlanTimeHH_Ins.Text = "";
            txtMediaPlanTimeMM_Ins.Text = "";
            txtTimeEndHH_ins.Text = "";
            txtTimeEndmm_ins.Text = "";
            ddlActive_Ins.ClearSelection();
            ddlMediaChannel_ins.ClearSelection();
            txtCampName_ins.Text = "";
            lblMediaPlanDate_Ins.Text = "";
            lblMediaPlanTimeHH_Ins.Text = "";
            lblMediaPlanTimeMM_Ins.Text = "";
            lblTimeEndHH_ins.Text = "";
            lblTimeEndmm_ins.Text = "";
            lblDuration_Ins.Text = "";
            lblMediaPhone_Ins.Text = "";
            lblChannel_Ins.Text = "";
            lblMediaChannel_ins.Text = "";
            lblProgramName_Ins.Text = "";
            lblddlCamp_Ins.Text = "";
            Label1.Text = "";
            Label2.Text = "";
            hidFlagInsert.Value = "True";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal();", true);
        }

        protected void ddlMediaPlanTypeIns_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCombosetFlag_Ins_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ShowComboSection(string flag)
        {


        }
        protected void btnShowImportFile_Click(object sender, EventArgs e)
        {
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-ImportMedia').modal();", true);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string ExcelPath = "";
            string FileName = "";

            try
            {
                //*** Check HasFile And Type of File is equals excel /**
                if (fiUpload.HasFile && (fiUpload.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"))
                {
                    int fileSize = fiUpload.PostedFile.ContentLength;
                    if (fileSize > 5600000)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('ขนาดไฟล์เกิน 5 MB');", true);
                        return;
                    }

                    else
                    {
                        string newFileName = "Upload_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";
                        fiUpload.SaveAs(Server.MapPath("~/Uploadfile/Xls/" + newFileName));
                    

                        FileInfo excel = new FileInfo(Server.MapPath(@"~/Uploadfile/Xls/" + newFileName));
                        ExcelPath = excel.ToString();
                        FileName = newFileName;
                        ViewState["UpLoadFileName"] = fiUpload.FileName.ToString();
                        ViewState["FileName"] = newFileName;
                        ViewState["ExcelPath"] = excel.ToString();

                        using (var package = new ExcelPackage(excel))
                        {
                            var workbook = package.Workbook;
                            var worksheet = workbook.Worksheets[1];
                            //เช็คว่าไฟล์มีข้อมูล
                            if (worksheet.Cells[2, 2].Text.ToString().Trim() == "")
                            {
                                DivSubmitUpload.Visible = false;
                                File.Delete(ExcelPath);
                            }
                            else
                            {
                                DataTable dt = ConvertToDataTable(worksheet);
                                
                                dt = dt.DefaultView.ToTable();
                                gvMediaPlanImport.DataSource = dt;
                                gvMediaPlanImport.DataBind();
                                DivSubmitUpload.Visible = true;
                            
                            }
                        }
                    }
                }
            
            }
            catch (Exception ex)
            {

            }

        }

        protected void btnSubmitImport_Click(object sender, EventArgs e)
        {
            
            for (int i = 0; i < gvMediaPlanImport.Rows.Count; i++)
            {
                HiddenField HidMEDIA_DATE = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMEDIA_DATE");
                HiddenField HidTIME_START = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidTIME_START");
                HiddenField HidTIME_END = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidTIME_END");
                HiddenField HidDuration = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidDuration");
                HiddenField HidMEDIA_PHONE = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMEDIA_PHONE");
                HiddenField HidSALE_CHANNEL = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidSALE_CHANNEL");
                HiddenField HidMEDIA_CHANNEL = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMEDIA_CHANNEL");
                HiddenField HidPROGRAM_NAME = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidPROGRAM_NAME");
                HiddenField HidCAMPAIGN_CODE = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidCAMPAIGN_CODE");
                HiddenField HidMerchantCode = (HiddenField)gvMediaPlanImport.Rows[i].FindControl("HidMERCHANT_NAME");

                result.L_MediaPlanInfo.Add(new MediaPlanInfo()
                {
                    MediaPlanDate = HidMEDIA_DATE.Value.ToString().Trim(),
                    MediaPlanTime = HidMEDIA_DATE.Value.ToString().Trim() + " " + HidTIME_START.Value.ToString().Trim(),
                    ProgramName = HidPROGRAM_NAME.Value.ToString().Trim(),
                    SALE_CHANNEL = HidSALE_CHANNEL.Value.ToString().Trim(),
                    Duration = HidDuration.Value.ToString().Trim(),
                    MediaPhone = HidMEDIA_PHONE.Value.ToString().Trim(),
                    CampaignCode = HidCAMPAIGN_CODE.Value.ToString().Trim(),
                    TIME_START = HidTIME_START.Value.ToString(),
                    TIME_END = HidTIME_END.Value.ToString(),
                    MEDIA_CHANNEL = HidMEDIA_CHANNEL.Value.ToString(),
                    MerchantCode = hidMerCode.Value.ToString(),
                    FlagDelete = "N"
                });
            }

            string respstr = "";
            APIpath = APIUrl + "/api/support/InsertMediaPlanList";
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new { result.L_MediaPlanInfo });
                var dataString = client.UploadString(APIpath, jsonObj);
            }

            loadMediaPlan();
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"MediaPlanDetail.aspx?MediaPlanCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }

      

        protected void BindddlSearchChannel()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ChannelCode"] = null;
                data["MerchantCode"] = hidMerCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<ChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<ChannelInfo>>(respstr);

            ddlSearchMediaPlanChannel.DataSource = lLookupInfo;
            ddlSearchMediaPlanChannel.DataTextField = "ChannelName";
            ddlSearchMediaPlanChannel.DataValueField = "ChannelCode";
            ddlSearchMediaPlanChannel.DataBind();
            ddlSearchMediaPlanChannel.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
            ddlChannel_Ins.DataSource = lLookupInfo;
            ddlChannel_Ins.DataTextField = "ChannelName";
            ddlChannel_Ins.DataValueField = "ChannelCode";
            ddlChannel_Ins.DataBind();
            ddlChannel_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        protected void BindddlMediaphone()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaPhoneNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["MediaPhoneNo"] = null;
                data["MerchantCode"] = hidMerCode.Value;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            List<MediaPhoneInfo> lMediaPhoneInfo = JsonConvert.DeserializeObject<List<MediaPhoneInfo>>(respstr);

            ddlMediaphone.DataSource = lMediaPhoneInfo;
            ddlMediaphone.DataTextField = "MediaPhoneNo";
            ddlMediaphone.DataValueField = "MediaPhoneNo";
            ddlMediaphone.DataBind();
            ddlMediaphone.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));

            ddlMediaPhone_Ins.DataSource = lMediaPhoneInfo;
            ddlMediaPhone_Ins.DataTextField = "MediaPhoneNo";
            ddlMediaPhone_Ins.DataValueField = "MediaPhoneNo";
            ddlMediaPhone_Ins.DataBind();
            ddlMediaPhone_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
            protected void BindddlCampaign()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = null;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> lLookupInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);

            ddlCamp.DataSource = lLookupInfo;
            ddlCamp.DataTextField = "CampaignCode";
            ddlCamp.DataValueField = "CampaignCode";
            ddlCamp.DataBind();
            ddlCamp.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));

            ddlCamp_Ins.DataSource = lLookupInfo;
            ddlCamp_Ins.DataTextField = "CampaignCode";
            ddlCamp_Ins.DataValueField = "CampaignCode";
            ddlCamp_Ins.DataBind();
            ddlCamp_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        protected void BindddlSalechannel()
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListSaleChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["SaleChannelCode"] = null;
                

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<SaleChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<SaleChannelInfo>>(respstr);

            ddlChannel_Ins.DataSource = lLookupInfo;
            ddlChannel_Ins.DataTextField = "SaleChannelName";
            ddlChannel_Ins.DataValueField = "SaleChannelCode";
            ddlChannel_Ins.DataBind();
            ddlChannel_Ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));

            ddlSearchMediaPlanChannel.DataSource = lLookupInfo;
            ddlSearchMediaPlanChannel.DataTextField = "SaleChannelName";
            ddlSearchMediaPlanChannel.DataValueField = "SaleChannelCode";
            ddlSearchMediaPlanChannel.DataBind();
            ddlSearchMediaPlanChannel.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
        }
        protected void BindddlMediaChannel(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaChannelNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["SaleChannelCode"] = code;
                

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<MediaChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<MediaChannelInfo>>(respstr);

            ddlMediaChannel_ins.DataSource = lLookupInfo;
            ddlMediaChannel_ins.DataTextField = "name_en";
            ddlMediaChannel_ins.DataValueField = "Code";
            ddlMediaChannel_ins.DataBind();
            ddlMediaChannel_ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
            ddlMediaChannel_ins.Enabled = true;
        
        }
        protected void BindddlMediaProgram()
        {
            


        }
        protected void BindddlMediaChannelByselect(string Code)
        {
            


        }

        protected void BindddlCampaignByselect(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListCampaignNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["CampaignCode"] = code;

                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }

            List<CampaignInfo> lLookupInfo = JsonConvert.DeserializeObject<List<CampaignInfo>>(respstr);
            txtCampName_ins.Text = lLookupInfo[0].CampaignName.ToString();
        }
        protected string BindddlMediaphoneByselect(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaPhoneNoPagingByCriteria";
            if (code != "")
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["MediaPhoneCode"] = code;
                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                return respstr;
            }
            else 
            {
                return respstr;
            }
            
        }
        protected string BindddlMediachannelByselect(string channelCode , string mediacode)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaChannelNoPagingByCriteria";
            if (mediacode != "" && channelCode != "")
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["Codeval"] = mediacode;
                    data["SaleChannelCode"] = channelCode;
                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                List<MediaChannelInfo> lLookupInfo = JsonConvert.DeserializeObject<List<MediaChannelInfo>>(respstr);

                ddlMediaChannel_ins.DataSource = lLookupInfo;
                ddlMediaChannel_ins.DataTextField = "name_en";
                ddlMediaChannel_ins.DataValueField = "Code";
                ddlMediaChannel_ins.DataBind();
                ddlMediaChannel_ins.Items.Insert(0, new ListItem("---- Please Select ----", "-99"));
                ddlMediaChannel_ins.Enabled = true;
                return respstr;

            }
            else
            {
                return respstr;
            }

        }
        protected Boolean validateInsert()
        {
            Boolean flag = true;
            if (txtMediaPlanDate_Ins.Text == "")
            {
                flag = false;
                lblMediaPlanDate_Ins.Text = MessageConst._MSG_PLEASEINSERT + " Media Plan Date";
            }
            
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaPlanDate_Ins.Text = "";
            }

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
            
            if (txtMediaPlanTimeHH_Ins.Text == "")
            {
                flag = false;
                lblMediaPlanTimeHH_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาเริ่ม";
            }
            else if (CheckSymbol(txtMediaPlanTimeHH_Ins.Text) == true)
            {
                flag = false;
                lblMediaPlanTimeHH_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาเริ่มต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaPlanTimeHH_Ins.Text = "";
            }
            if (txtMediaPlanTimeMM_Ins.Text == "")
            {
                flag = false;
                lblMediaPlanTimeMM_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาเริ่ม";
            }
            else if (CheckSymbol(txtMediaPlanTimeMM_Ins.Text) == true)
            {
                flag = false;
                lblMediaPlanTimeMM_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาเริ่มต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaPlanTimeMM_Ins.Text = "";
            }

            if (txtTimeEndHH_ins.Text == "")
            {
                flag = false;
                lblTimeEndHH_ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาสิ้นสุด";
            }
            else if (CheckSymbol(txtTimeEndHH_ins.Text) == true)
            {
                flag = false;
                lblTimeEndHH_ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาสิ้นสุดต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTimeEndHH_ins.Text = "";
            }


            if (txtTimeEndmm_ins.Text == "")
            {
                flag = false;
                lblTimeEndmm_ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาสิ้นสุด";
            }
            else if (CheckSymbol(txtTimeEndmm_ins.Text) == true)
            {
                flag = false;
                lblTimeEndHH_ins.Text = MessageConst._MSG_PLEASEINSERT + " เวลาสิ้นสุดต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblTimeEndmm_ins.Text = "";
            }




            if (DelayStart.Text == "")
            {
                flag = false;
                lblDelayStart.Text = MessageConst._MSG_PLEASEINSERT + " เวลาเริ่มหน่วง";
            }
            else if (CheckSymbol(DelayStart.Text) == true)
            {
                flag = false;
                lblDelayStart.Text = MessageConst._MSG_PLEASEINSERT + " เวลาหน่วงเริ่มต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDelayStart.Text = "";
            }



            if (DelayEnd.Text == "")
            {
                flag = false;
                lblDelayEnd.Text = MessageConst._MSG_PLEASEINSERT + " เวลาสิ้นสุดหน่วง";
            }
            else if (CheckSymbol(DelayStart.Text) == true)
            {
                flag = false;
                lblDelayEnd.Text = MessageConst._MSG_PLEASEINSERT + " เวลาหน่วงเริ่มต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDelayEnd.Text = "";
            }






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

            if (ddlCamp_Ins.SelectedValue == "-99" || ddlCamp_Ins.SelectedValue == "")
            {
                flag = false;
                lblddlCamp_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสแคมเปญ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblddlCamp_Ins.Text = "";
            }

            if (ddlActive_Ins.SelectedValue == "-99" || ddlActive_Ins.SelectedValue == "")
            {
                flag = false;
                Label1.Text = MessageConst._MSG_PLEASEINSERT + " สถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                Label1.Text = "";
            }

            if (txtDuration_Ins.Text == "")
            {
                flag = false;
                lblDuration_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระยะเวลา";
            } 
            else if (CheckSymbol(txtDuration_Ins.Text) == true)
            {
                flag = false;
                lblDuration_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระยะเวลาต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblDuration_Ins.Text = "";
            }

            if (txtCampName_ins.Text == "")
            {
                flag = false;
                Label2.Text = MessageConst._MSG_PLEASEINSERT + "ชื่อแคมเปญ";
            }
            

            if (TxtProgramName_Ins.Text == "")
            {
                flag = false;
                lblProgramName_Ins.Text = MessageConst._MSG_PLEASEINSERT + "ชื่อโปรแกรม";
            }
            else if (CheckSymbol(TxtProgramName_Ins.Text) == true)
            {
                flag = false;
                lblProgramName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อโปรแกรมต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblProgramName_Ins.Text = "";
            }
            if (ddlMediaPhone_Ins.SelectedValue == "-99" || ddlMediaPhone_Ins.SelectedValue == "")
            {
                flag = false;
                lblMediaPhone_Ins.Text = MessageConst._MSG_PLEASEINSERT + "เบอร์โทรศัพท์";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaPhone_Ins.Text = "";
            }
            
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPlan').modal();", true);
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

        private DataTable ConvertToDataTable(ExcelWorksheet worksheet)
        {
            int totalRows = worksheet.Dimension.End.Row;
            DataTable dt = new DataTable(worksheet.Name);
            DataRow dr = null;

            //*** Column **/
            dt.Columns.Add("LINE_NO");
            dt.Columns.Add("MEDIA_DATE");
            dt.Columns.Add("TIME_START");
            dt.Columns.Add("TIME_END");
            dt.Columns.Add("Duration");
            dt.Columns.Add("MEDIA_PHONE");
            dt.Columns.Add("MEDIA_CHANNEL");
            dt.Columns.Add("SALE_CHANNEL");
            dt.Columns.Add("PROGRAM_NAME");
            dt.Columns.Add("CAMPAIGN_CODE");
            try
            {
                int j;
                for (int i = 2; i <= totalRows; i++)
                {
                    //*** Rows ***//
                    dr = dt.NewRow();
                    j = 1;

                    dr["LINE_NO"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_DATE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["TIME_START"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["TIME_END"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["Duration"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_PHONE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["MEDIA_CHANNEL"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["SALE_CHANNEL"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["PROGRAM_NAME"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dr["CAMPAIGN_CODE"] = worksheet.Cells[i, j].Text.ToString().Trim(); j++;
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

            }
            return dt;
        }
        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            loadMediaPlan();
        }

    }
}
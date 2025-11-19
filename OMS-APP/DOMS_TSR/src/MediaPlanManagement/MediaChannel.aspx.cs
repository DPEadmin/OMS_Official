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
    public partial class MediaChannel : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string MediaChannelImgUrl = ConfigurationManager.AppSettings["MediaChannelImageUrl"];

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
                lblChannelDis.Text = Request.QueryString["Channel"].ToString().Trim();
                lblActiveDis.Text = Request.QueryString["ChannelName"].ToString().Trim();


                BindStatus();

                loadMediaChannel();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadMediaChannel();
        }

        #region Function



        protected void loadMediaChannel()
        {
            List<MediaChannelInfo> lMediaChannelInfo = new List<MediaChannelInfo>();

            

            int? totalRow = CountMediaChannelMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lMediaChannelInfo = GetMediaChannelMasterByCriteria();

            gvMediaChannel.DataSource = lMediaChannelInfo;

            gvMediaChannel.DataBind();


            

        }

        public List<MediaChannelInfo> GetMediaChannelMasterByCriteria()
        {
           
            APIpath = APIUrl + "/api/support/ListMediaChannelNoPagingByCriteria";
            MediaChannelInfo mon = new MediaChannelInfo();
            List<MediaChannelInfo> lMediaChannelInfo = new List<MediaChannelInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.Code = txtMediaChannelCode_Search.Text;
                mon.name_en = txtnameEn_search.Text;
                mon.name_th = txtnameTh_search.Text;
                mon.MerchantCode = hidMerCode.Value;
                mon.Active = ddlSearchMediaChannelActive.SelectedValue;
                mon.rowOFFSet = (currentPageNumber - 1) * PAGE_SIZE;
                mon.rowFetch = PAGE_SIZE;
                mon.SaleChannelCode = Request.QueryString["Channel"].ToString().Trim();

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.Code,
                    mon.name_en,
                    mon.name_th,
                    mon.Active,
                    mon.MerchantCode,
                    mon.rowOFFSet,
                    mon.rowFetch,
                    mon.SaleChannelCode
                });
                var dataString = client.UploadString(APIpath, jsonObj);
                lMediaChannelInfo = JsonConvert.DeserializeObject<List<MediaChannelInfo>>(dataString.ToString());
            }

            return lMediaChannelInfo;

        }

        public int? CountMediaChannelMasterList()
        {
           
            int? cou = 0;
            APIpath = APIUrl + "/api/support/CountListMediaChannel";
            MediaChannelInfo mon = new MediaChannelInfo();
            List<MediaChannelInfo> lMediaChannelInfo = new List<MediaChannelInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.Code = txtMediaChannelCode_Search.Text;
                mon.name_en = txtnameEn_search.Text;
                mon.name_th = txtnameTh_search.Text;
                mon.Active = ddlSearchMediaChannelActive.SelectedValue;
                mon.SaleChannelCode = Request.QueryString["Channel"].ToString().Trim();

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.Code,
                    mon.name_en,
                    mon.name_th,
                    mon.Active,
                    mon.SaleChannelCode,
                });
                var dataString = client.UploadString(APIpath, jsonObj);
                cou = JsonConvert.DeserializeObject<int?>(dataString.ToString());
               
            }
           


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


            loadMediaChannel();
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

        protected Boolean DeleteMediaChannel()
        {

            for (int i = 0; i < gvMediaChannel.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvMediaChannel.Rows[i].FindControl("chkMediaChannel");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvMediaChannel.Rows[i].FindControl("HidMediaChannelId");
             

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

                APIpath = APIUrl + "/api/support/DeleteMediaChannel";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["MediaChannelIdlist"] = Idlist;


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

        protected void gvMediaChannel_Change(object sender, GridViewPageEventArgs e)
        {
            gvMediaChannel.PageIndex = e.NewPageIndex;

            List<MediaChannelInfo> lMediaChannelInfo = new List<MediaChannelInfo>();

            lMediaChannelInfo = GetMediaChannelMasterByCriteria();

            gvMediaChannel.DataSource = lMediaChannelInfo;

            gvMediaChannel.DataBind();

        }

        protected void chkMediaChannelAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMediaChannel.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvMediaChannel.HeaderRow.FindControl("chkMediaChannelAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvMediaChannel.Rows[i].FindControl("hidMediaChannelId");
                    HiddenField hidCode = (HiddenField)gvMediaChannel.Rows[i].FindControl("hidMediaChannelNo");

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

                    CheckBox chkMediaChannel = (CheckBox)gvMediaChannel.Rows[i].FindControl("chkMediaChannel");

                    chkMediaChannel.Checked = true;
                }
                else
                {

                    CheckBox chkMediaChannel = (CheckBox)gvMediaChannel.Rows[i].FindControl("chkMediaChannel");

                    chkMediaChannel.Checked = false;
                }

            }
            hidIdList.Value = Idlist;
            hidCodeList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            loadMediaChannel();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteMediaChannel();

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
                        if (validateInsert())
                        {
                        int? sum = 0;
                        APIpath = APIUrl + "/api/support/InsertMediaChannel";
                        MediaChannelInfo mon = new MediaChannelInfo();
                        List<MediaChannelInfo> lMediaChannelInfo = new List<MediaChannelInfo>();
                        using (var client = new WebClient())
                        {
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            client.Encoding = Encoding.UTF8;

                            mon.Code = txtMediaChannelCode_Ins.Text.Trim();
                            mon.name_th = txtMediaChannelNameth_ins.Text.Trim();
                            mon.name_en = txtMediaChannelNameEN_ins.Text.Trim();
                            mon.Active = ddlStatusActive_Ins.SelectedValue;
                            mon.CreateBy = empInfo.EmpCode;
                            mon.UpdateBy = empInfo.EmpCode;
                            mon.MerchantCode = hidMerCode.Value;
                            mon.SaleChannelCode = Request.QueryString["Channel"].ToString().Trim();
                            var jsonObj = JsonConvert.SerializeObject(new
                            {
                                mon.Code,
                                mon.name_th,
                                mon.name_en,
                                mon.Active,
                                mon.CreateBy,
                                mon.MerchantCode,
                                mon.SaleChannelCode
                            });
                            var dataString = client.UploadString(APIpath, jsonObj);
                            sum = JsonConvert.DeserializeObject<int?>(dataString.ToString());

                        }
                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            loadMediaChannel();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaChannel').modal('hide');", true);

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
                        int? sumupdate = 0;
                        APIpath = APIUrl + "/api/support/UpdateMediaChannel";

                        MediaChannelInfo mon = new MediaChannelInfo();
                        List<MediaChannelInfo> lMediaChannelInfo = new List<MediaChannelInfo>();
                        using (var client = new WebClient())
                        {
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            client.Encoding = Encoding.UTF8;

                            mon.Code = txtMediaChannelCode_Ins.Text.Trim();
                            mon.name_th = txtMediaChannelNameth_ins.Text.Trim();
                            mon.name_en = txtMediaChannelNameEN_ins.Text.Trim();
                            mon.Active = ddlStatusActive_Ins.SelectedValue;
                         
                            mon.UpdateBy = empInfo.EmpCode;
                            mon.MediaChannelId = int.Parse(hidIdList.Value.ToString());
                            var jsonObj = JsonConvert.SerializeObject(new
                            {
                                mon.Code,
                                mon.name_th,
                                mon.name_en,
                              
                                mon.Active,
                                mon.MediaChannelId,
                                mon.UpdateBy
                            });
                            var dataString = client.UploadString(APIpath, jsonObj);
                            sumupdate = JsonConvert.DeserializeObject<int?>(dataString.ToString());

                        }



                        if (sumupdate > 0)
                        {

                            loadMediaChannel();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaChannel').modal('hide');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }
                    }
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtMediaChannelCode_Ins.Text = "";
            txtMediaChannelNameth_ins.Text = "";
            txtMediaChannelNameEN_ins.Text = "";
            ddlStatusActive_Ins.ClearSelection();



            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaChannel').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {

            txtnameEn_search.Text = "";
            ddlSearchMediaChannelActive.ClearSelection();
            txtMediaChannelCode_Search.Text = "";
            txtnameTh_search.Text = "";

        }
        protected void gvMediaChannel_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                    
                
            }
        }
        protected void gvMediaChannel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvMediaChannel.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField HidMediaChannelId = (HiddenField)row.FindControl("HidMediaChannelId");
            HiddenField HidCode = (HiddenField)row.FindControl("HidCode");
            HiddenField Hidname_en = (HiddenField)row.FindControl("Hidname_en");
            HiddenField Hidname_th = (HiddenField)row.FindControl("Hidname_th");
            HiddenField hidActive = (HiddenField)row.FindControl("hidActive");

         
            if (e.CommandName == "ShowMediaChannel")
            {
                ddlStatusActive_Ins.SelectedValue = hidActive.Value;
                txtMediaChannelCode_Ins.Text = HidCode.Value;
                txtMediaChannelNameth_ins.Text = Hidname_th.Value;
                txtMediaChannelNameEN_ins.Text = Hidname_en.Value;

                hidIdList.Value = HidMediaChannelId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaChannel').modal();", true);

            }
            else if (e.CommandName == "Download")
            {

            }

        }

        protected void btnAddMediaChannel_Click(object sender, EventArgs e)
        {
            lblMediaChannelNameTh_Ins.Text = "";
            txtMediaChannelNameth_ins.Text = "";
            lblMediaChannelNameEN_Ins.Text = "";
            txtMediaChannelNameEN_ins.Text = "";
            txtMediaChannelCode_Ins.Text = "";
            lblMediaChannelCode_Ins.Text = "";
            ddlStatusActive_Ins.ClearSelection();
            lbStatusActive_Ins.Text = "";
            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaChannel').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode, object Active)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            string stcActive = (Active != null) ? Active.ToString() : "";
            return "<a href=\"MediaChannelSub.aspx?MediaChannelCode=" + strCode + "&Active="+ Active + "\">" + strCode + "</a>";
        }

        protected void BindStatus()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("---- กรุณาเลือก ----", ""));
            items.Add(new ListItem("Active", "Y"));
            items.Add(new ListItem("Inactive", "N"));
            
            ddlSearchMediaChannelActive.Items.AddRange(items.ToArray());
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

            
            
        }
        protected string ValidateMediaChannelID(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaChannelNoPagingByCriteria";
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["Codeval"] = code;
                data["SaleChannelCode"] = Request.QueryString["Channel"].ToString().Trim();
                var response = wb.UploadValues(APIpath, "POST", data);
                respstr = Encoding.UTF8.GetString(response);
            }
            return respstr;
        }



        protected Boolean validateInsert()
        {
            Boolean flag = true;
            string mediachannel = "";

            if (ddlStatusActive_Ins.SelectedValue == "-99" || ddlStatusActive_Ins.SelectedValue == "")
            {
                flag = false;
                lbStatusActive_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เลือกสถานะ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lbStatusActive_Ins.Text = "";
            }

            if (txtMediaChannelCode_Ins.Text == "")
            {
                flag = false;
                lblMediaChannelCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสช่องมีเดีย";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaChannelCode_Ins.Text = "";
            }
            if (txtMediaChannelNameth_ins.Text == "")
            {
                flag = false;
                lblMediaChannelNameTh_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อภาษาไทย";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaChannelNameTh_Ins.Text = "";
            }
            if (txtMediaChannelNameEN_ins.Text == "")
            {
                flag = false;
                lblMediaChannelNameEN_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อภาษาอังกฤษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaChannelNameEN_Ins.Text = "";
            }
            if (txtMediaChannelNameEN_ins.Text == "")
            {
                flag = false;
                lblMediaChannelNameEN_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อภาษาอังกฤษ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaChannelNameEN_Ins.Text = "";
            }
            mediachannel = ValidateMediaChannelID(txtMediaChannelCode_Ins.Text);
            if (mediachannel == "[]" || mediachannel == "" || mediachannel == null)
            {
                flag = (flag == false) ? false : true;
                lblMediaChannelCode_Ins.Text = "";
            }
            else
            {
                flag = false;
                lblMediaChannelCode_Ins.Text = "MediaChannel นี้มีอยู่แล้ว";
            }
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaChannel').modal();", true);
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
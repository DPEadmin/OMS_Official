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
    public partial class MediaPhone : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string MediaPhoneImgUrl = ConfigurationManager.AppSettings["MediaPhoneImageUrl"];

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

                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }
                BindStatus();

                loadMediaPhone();
            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadMediaPhone();
        }

        #region Function



        protected void loadMediaPhone()
        {
            List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();

            

            int? totalRow = CountMediaPhoneMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lMediaPhoneInfo = GetMediaPhoneMasterByCriteria();

            gvMediaPhone.DataSource = lMediaPhoneInfo;

            gvMediaPhone.DataBind();


            

        }

        public List<MediaPhoneInfo> GetMediaPhoneMasterByCriteria()
        {
           
            APIpath = APIUrl + "/api/support/ListMediaPhoneNoPagingByCriteria";
            MediaPhoneInfo mon = new MediaPhoneInfo();
            List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.MediaPhoneNo = txtMediaphoneNo.Text;
                mon.Active = ddlSearchMediaPhoneActive.SelectedValue;
                mon.MerchantCode = hidMerCode.Value;
                mon.rowOFFSet = (currentPageNumber - 1) * PAGE_SIZE;
                mon.rowFetch = PAGE_SIZE;
                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.MediaPhoneNo,
                    mon.MerchantCode,
                    mon.Active,
                    mon.rowOFFSet,
                    mon.rowFetch
                });
                var dataString = client.UploadString(APIpath, jsonObj);
                lMediaPhoneInfo = JsonConvert.DeserializeObject<List<MediaPhoneInfo>>(dataString.ToString());
            }

            return lMediaPhoneInfo;

        }

        public int? CountMediaPhoneMasterList()
        {
           
            int? cou = 0;
            APIpath = APIUrl + "/api/support/CountListMediaPhone";
            MediaPhoneInfo mon = new MediaPhoneInfo();
            List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                mon.MediaPhoneNo = txtMediaphoneNo.Text;
                mon.MerchantCode = hidMerCode.Value;
                mon.Active = ddlSearchMediaPhoneActive.SelectedValue;
             

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.MediaPhoneNo,
                    mon.MerchantCode,
                    mon.Active,
                 
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


            loadMediaPhone();
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

        protected Boolean DeleteMediaPhone()
        {

            for (int i = 0; i < gvMediaPhone.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvMediaPhone.Rows[i].FindControl("chkMediaPhone");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvMediaPhone.Rows[i].FindControl("hidMediaPhoneId");
             

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

                APIpath = APIUrl + "/api/support/DeleteMediaPhone";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["MediaPhoneIdlist"] = Idlist;


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

        protected void gvMediaPhone_Change(object sender, GridViewPageEventArgs e)
        {
            gvMediaPhone.PageIndex = e.NewPageIndex;

            List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();

            lMediaPhoneInfo = GetMediaPhoneMasterByCriteria();

            gvMediaPhone.DataSource = lMediaPhoneInfo;

            gvMediaPhone.DataBind();

        }

        protected void chkMediaPhoneAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvMediaPhone.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvMediaPhone.HeaderRow.FindControl("chkMediaPhoneAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvMediaPhone.Rows[i].FindControl("hidMediaPhoneId");
                    HiddenField hidCode = (HiddenField)gvMediaPhone.Rows[i].FindControl("hidMediaPhoneNo");

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

                    CheckBox chkMediaPhone = (CheckBox)gvMediaPhone.Rows[i].FindControl("chkMediaPhone");

                    chkMediaPhone.Checked = true;
                }
                else
                {

                    CheckBox chkMediaPhone = (CheckBox)gvMediaPhone.Rows[i].FindControl("chkMediaPhone");

                    chkMediaPhone.Checked = false;
                }

            }
            hidIdList.Value = Idlist;
            hidCodeList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (validateSearch())
            {
            loadMediaPhone();
            }


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeleteMediaPhone();

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
                        APIpath = APIUrl + "/api/support/InsertMediaPhone";
                        MediaPhoneInfo mon = new MediaPhoneInfo();
                        List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();
                        using (var client = new WebClient())
                        {
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            client.Encoding = Encoding.UTF8;

                            mon.MediaPhoneNo = txtMediaPhone_Ins.Text.Trim();
                            mon.Active = ddlStatusActive_Ins.SelectedValue;
                            mon.MerchantCode = hidMerCode.Value;
                            mon.CreateBy = empInfo.EmpCode;
                            mon.UpdateBy = empInfo.EmpCode;
                            var jsonObj = JsonConvert.SerializeObject(new
                            {
                                mon.MediaPhoneNo,
                                mon.MerchantCode,
                                mon.Active,
                                mon.CreateBy,
                            });
                            var dataString = client.UploadString(APIpath, jsonObj);
                            sum = JsonConvert.DeserializeObject<int?>(dataString.ToString());

                        }
                        if (sum > 0)
                        {
                            btnCancel_Click(null, null);

                            loadMediaPhone();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaPhone').modal('hide');", true);

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
                        APIpath = APIUrl + "/api/support/UpdateMediaPhone";

                        MediaPhoneInfo mon = new MediaPhoneInfo();
                        List<MediaPhoneInfo> lMediaPhoneInfo = new List<MediaPhoneInfo>();
                        using (var client = new WebClient())
                        {
                            client.Headers[HttpRequestHeader.ContentType] = "application/json";
                            client.Encoding = Encoding.UTF8;

                            mon.MediaPhoneNo = txtMediaPhone_Ins.Text.Trim();
                            mon.Active = ddlStatusActive_Ins.SelectedValue;
                         
                            mon.UpdateBy = empInfo.EmpCode;
                            mon.MediaPhoneId = int.Parse(hidIdList.Value.ToString());
                            var jsonObj = JsonConvert.SerializeObject(new
                            {
                                mon.MediaPhoneNo,
                                mon.Active,
                                mon.MediaPhoneId,
                                mon.UpdateBy
                            });
                            var dataString = client.UploadString(APIpath, jsonObj);
                            sumupdate = JsonConvert.DeserializeObject<int?>(dataString.ToString());

                        }



                        if (sumupdate > 0)
                        {

                            loadMediaPhone();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-MediaPhone').modal('hide');", true);
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
            txtMediaPhone_Ins.Text = "";
            ddlStatusActive_Ins.ClearSelection();



            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPhone').modal('hide');", true);
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
          
            
            ddlSearchMediaPhoneActive.ClearSelection();
           txtMediaphoneNo.Text = "";
            

        }
        protected void gvMediaPhone_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                    
                
            }
        }
        protected void gvMediaPhone_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvMediaPhone.Rows[index];


            Label lblmsg = (Label)row.FindControl("lblmsg");

            HiddenField HidMediaPhoneId = (HiddenField)row.FindControl("HidMediaPhoneId");
            HiddenField HidMediaPhoneNo = (HiddenField)row.FindControl("HidMediaPhoneNo");
            HiddenField hidActive = (HiddenField)row.FindControl("hidActive");

         
            if (e.CommandName == "ShowMediaPhone")
            {
                ddlStatusActive_Ins.SelectedValue = hidActive.Value;
                txtMediaPhone_Ins.Text = HidMediaPhoneNo.Value;           
                hidIdList.Value = HidMediaPhoneId.Value;
                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPhone').modal();", true);

            }
            else if (e.CommandName == "Download")
            {

            }

        }

        protected void btnAddMediaPhone_Click(object sender, EventArgs e)
        {
            txtMediaPhone_Ins.Text = "";
            ddlStatusActive_Ins.ClearSelection();
            hidFlagInsert.Value = "True";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPhone').modal();", true);
        }

        #endregion

        #region Binding

        protected string GetLink(object objCode, object Active)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            string stcActive = (Active != null) ? Active.ToString() : "";
            return "<a href=\"MediaPhoneSub.aspx?MediaPhoneCode=" + strCode + "&Active="+ Active + "\">" + strCode + "</a>";
        }

        protected void BindStatus()
        {
            List<ListItem> items = new List<ListItem>();
            items.Add(new ListItem("---- Please Select ----", ""));
            items.Add(new ListItem("Active", "Y"));
            items.Add(new ListItem("Inactive", "N"));
           
            ddlSearchMediaPhoneActive.Items.AddRange(items.ToArray());
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

            
        }


        protected Boolean validateInsert()
        {
            Boolean flag = true;
            string mediaphone = "";

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

            if (txtMediaPhone_Ins.Text == "")
            {
                flag = false;
                lblMediaPhone_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรศัทพ์";
            }
            else if (CheckSymbol(txtMediaPhone_Ins.Text) == true)
            {
                flag = false;
                lblMediaPhone_Ins.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรศัทพ์ต้องไม่มีอักขระพิเศษ";
            }
            else
            {
                mediaphone = ValidateMediaID(txtMediaPhone_Ins.Text);
                if (mediaphone == "[]" || mediaphone == "" || mediaphone == null)
                {
                    flag = (flag == false) ? false : true;
                    lblMediaPhone_Ins.Text = "";
                }
                else
                {
                    flag = false;
                    lblMediaPhone_Ins.Text = "เบอร์โทรศัทพ์นี้มีอยู่แล้ว";
                }
            }
            
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-MediaPhone').modal();", true);
            }

            return flag;
        }
        protected string ValidateMediaID(string code)
        {
            string respstr = "";
            APIpath = APIUrl + "/api/support/ListMediaPhoneNoPagingByCriteria";
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();
                    data["MediaPhoneCode"] = code;
                    var response = wb.UploadValues(APIpath, "POST", data);
                    respstr = Encoding.UTF8.GetString(response);
                }
                return respstr;
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
        protected Boolean validateSearch()
        {
            Boolean flag = true;

            if (CheckSymbol(txtMediaphoneNo.Text))
            {
                flag = false;
                lblMediaphoneNo.Text = MessageConst._MSG_PLEASEINSERT + " เบอร์โทรต้องไม่มีอักขระพิเศษ";

            }
            else
            {
                flag = (flag == false) ? false : true;
                lblMediaphoneNo.Text = "";
            }
           
            return flag;
        }

    }
}
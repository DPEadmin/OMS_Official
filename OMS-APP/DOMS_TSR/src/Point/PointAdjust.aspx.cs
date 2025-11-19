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
using System.Globalization;

using System.IO;

namespace DOMS_TSR.src.Point
{
    public partial class PointAdjust : System.Web.UI.Page
    {
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];

        string Idlist = "";
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
        string RedeemFlag = "";
        string ComplementaryFlag = "";
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        string APIpath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                currentPageNumber = 1;

                EmpInfo empInfo = new EmpInfo();
                MerchantInfo merchantinfo = new MerchantInfo();
                merchantinfo = (MerchantInfo)Session["MerchantInfo"];
                empInfo = (EmpInfo)Session["EmpInfo"];

                if (empInfo != null && merchantinfo != null)
                {
                    hidEmpCode.Value = empInfo.EmpCode;
                    hidMerchantCode.Value = merchantinfo.MerchantCode;
                    
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }

                btnSubmit.Visible = true;
                btnSavedraft.Visible = false;
                
                LoadEmpBuLevel(hidEmpCode.Value);
                loadPointRange();
                loadPointRate();

            }

        }

        protected void LoadEmpBuLevel(string empcode)
        {
            List<EmpMapBu> lebuInfo = new List<EmpMapBu>();
            lebuInfo = GetEmpMapBuMaster(empcode);

            if (lebuInfo.Count > 0)
            {
                foreach (var le in lebuInfo)
                {
                    hidBu.Value = le.Bu;
                    hidLevels.Value = le.Levels;
                }
            }
        }
        protected void txtPointSeqOnClick(object sender, EventArgs e)
        {
            int count = 0;
            int previous = 0;
            int near = 0;
            List<PointInfo> pointinfo = new List<PointInfo>();
            bool flagpoint = false;
            pointinfo = BindtxtPointRange(null);
            int lastpoint = pointinfo.Count();
            int? lastsequence = pointinfo[lastpoint - 1].PointSequence;
            int txtseq = int.Parse(txtPointSeq.Text);

            foreach (var item in pointinfo)
            {
                
                if (item.PointSequence == txtseq || txtseq > lastsequence)
                {
                    flagpoint = false;
                    break;
                }
                else
                {
                    if (item.PointSequence - txtseq == 1)
                    {
                        near = count ;
                        flagpoint = true;
                        
                    }
                    else if (item.PointSequence - txtseq == -1)
                    {
                        
                        previous = count ;
                        flagpoint = true;
                        
                    }
                }
                count++;
            }

            if (flagpoint == true)
            {
                lblPointSeq_Ins.Text = "";
                txtPointBegin_Ins.Text = (pointinfo[previous].PointEnd + 1).ToString();
                txtPointEnd_Ins.Text = (pointinfo[near].PointBegin - 1).ToString();
            }
            else if (txtseq > lastsequence)
            {
                lblPointSeq_Ins.Text = "ไม่สามารถใช้เลขนี้ได้เนื่องจากมากกว่า อันดับที่มีอยู่";
                txtPointSeq.Text = hidSeq_Spare.Value;
            }
            else
            {
                lblPointSeq_Ins.Text = "ไม่สามารถใช้เลขนี้ได้เนื่องจากซ้ำกับอันดับที่มีอยู่";
                txtPointSeq.Text = hidSeq_Spare.Value;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-PointRange').modal();", true);
        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadPointRange();
        }
     

        #region Function

        public bool ValidateDuplicate()
        {
            bool isDuplicate;
            string respstr = "";

            APIpath = APIUrl + "/api/support/PromotionCodeValidateInsert";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = txtPromotionCode_Ins.Text;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lProductInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            if (lProductInfo.Count > 0)
            {
                isDuplicate = true;
            }
            else
            {
                isDuplicate = false;
            }

            return isDuplicate;

        }


        protected void loadPointRate()
        {
            List<PointInfo> lPointInfo = new List<PointInfo>();
            lPointInfo = GetPointRateMasterByCriteria();
            if (lPointInfo.Count() > 0)
            {
                txtPrice.Text = lPointInfo[0].Price.ToString();
                txtGetPoint.Text = lPointInfo[0].GetPoint.ToString();
                hidPointRateID.Value = lPointInfo[0].PointId.ToString();
            }
            else
            {
                txtPrice.Text = "";
                txtGetPoint.Text = "";
                hidPointRateID.Value = "";
            }
        }

        protected void loadPointRange()
        {
            List<PointInfo> lPointInfo = new List<PointInfo>();

            int? totalRow = CountPointRangeMasterList();

            SetPageBar(Convert.ToDouble(totalRow));


            lPointInfo = GetPointRangeMasterByCriteria();

            gvPointRange.DataSource = lPointInfo;

            gvPointRange.DataBind();

        }
        public List<PointInfo> GetPointRateMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPointRateNoPagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantMapCode"] = hidMerchantCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PointInfo> lPointInfo = JsonConvert.DeserializeObject<List<PointInfo>>(respstr);


            return lPointInfo;

        }
        public List<PointInfo> GetPointRangeMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPointRangePagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantMapCode"] = hidMerchantCode.Value;

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();
     

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PointInfo> lPointInfo = JsonConvert.DeserializeObject<List<PointInfo>>(respstr);


            return lPointInfo;

        }

        public int? CountPointRangeMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/CountPointRangePagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["MerchantMapCode"] = hidMerchantCode.Value;

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


            loadPointRange();
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

        protected Boolean DeletePointRange()
        {

            for (int i = 0; i < gvPointRange.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvPointRange.Rows[i].FindControl("chkPointRange");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvPointRange.Rows[i].FindControl("hidPointId");
                    HiddenField hidCode = (HiddenField)gvPointRange.Rows[i].FindControl("hidPointCode");

                    if (Idlist != "")
                    {
                        Idlist += "," + hidId.Value + "";
                    }
                    if (Codelist != "")
                    {
                        Codelist += "," + hidCode.Value + "";
                    }
                    else
                    {
                        Idlist += "" + hidId.Value + "";
                        Codelist += "" + hidCode.Value + "";
                    }

                }
            }

            if (Idlist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePointRange";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PointId"] = Idlist;


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

        public string GetPromotionImgByCriteria(string ProductCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/GetPromotionImageUrl";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = ProductCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionImageInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionImageInfo>>(respstr);


            return lPromotionInfo.Count > 0 ? lPromotionInfo[0].PromotionImageId.ToString() : "";

        }

        protected List<PromotionDetailInfo> getPromoDetailList(string proCode)
        {

            string respstr = "";

            APIpath = APIUrl + "/api/support/ListProducttionDetailNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = proCode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionDetailInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<PromotionDetailInfo>>(respstr);

            return lPromotionInfo;
        }

        protected List<EmpMapBu> GetEmpMapBuMaster(string empcode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListEmpMapBuNoPaging";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["EmpCode"] = empcode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<EmpMapBu> lebuInfo = JsonConvert.DeserializeObject<List<EmpMapBu>>(respstr);

            return lebuInfo;
        }

        protected List<PromotionInfo> GetPromotionIDByCritreria(string promotioncode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPromotionNoPagingByCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PromotionCode"] = promotioncode;

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PromotionInfo> lpromotionInfo = JsonConvert.DeserializeObject<List<PromotionInfo>>(respstr);

            return lpromotionInfo;
        }
        #endregion

        #region Event 

        protected void gvPromotion_Change(object sender, GridViewPageEventArgs e)
        {
            gvPointRange.PageIndex = e.NewPageIndex;

            List<PointInfo> lPointInfo = new List<PointInfo>();

            lPointInfo = GetPointRangeMasterByCriteria();

            gvPointRange.DataSource = lPointInfo;

            gvPointRange.DataBind();

        }
        protected List<PointInfo> BindtxtPointRange(string PointCode)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListPointRangePagingbyCriteria";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["PointCode"] = PointCode;

                data["MerchantMapCode"] = hidMerchantCode.Value;


                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<PointInfo> lPointInfo = JsonConvert.DeserializeObject<List<PointInfo>>(respstr);

            return lPointInfo;



        }
        protected void chkPointRange_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvPointRange.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvPointRange.HeaderRow.FindControl("chkPointRangeAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvPointRange.Rows[i].FindControl("hidPointId");
                    HiddenField hidCode = (HiddenField)gvPointRange.Rows[i].FindControl("hidPointCode");

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

                    CheckBox chkPromotion = (CheckBox)gvPointRange.Rows[i].FindControl("chkPointRange");

                    chkPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkPromotion = (CheckBox)gvPointRange.Rows[i].FindControl("chkPointRange");

                    chkPromotion.Checked = false;
                }

            }
            hidIdList.Value = Idlist;
            hidCodeList.Value = Codelist;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            currentPageNumber = 1;
            loadPointRange();


        }



        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeletePointRange();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }



        }
        protected void btnSavePrice_Click(object sender, EventArgs e)
        {
            EmpInfo empInfo = new EmpInfo();
            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

            List<PointInfo> lPointInfo = new List<PointInfo>();
            lPointInfo = GetPointRateMasterByCriteria();
            if (lPointInfo.Count() > 0)//Update
            {
                if (txtPrice.Text != "" && txtGetPoint.Text != "")
                {
                    empInfo = (EmpInfo)Session["EmpInfo"];
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/UpdatePointRate";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["PointId"] = hidPointRateID.Value;
                        data["Price"] = txtPrice.Text;
                        data["GetPoint"] = txtGetPoint.Text;
                        data["FlagDelete"] = "N";
                        data["UpdateBy"] = empInfo.EmpCode;



                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {

                        btnCancel_Click(null, null);

                        loadPointRate();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-PointRange').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
                else
                {
                    lblPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ราคา";
                    lblGetPoint_Ins.Text = MessageConst._MSG_PLEASEINSERT + " พอยต์ที่ได้";
                }
            }
            else//Insert
            {
                if (txtPrice.Text != "" && txtGetPoint.Text != "")
                {
                    string respstr = "";

                    APIpath = APIUrl + "/api/support/InsertPointRate";

                    using (var wb = new WebClient())
                    {
                        var data = new NameValueCollection();

                        data["Price"] = txtPrice.Text;
                        data["GetPoint"] = txtGetPoint.Text;
                        data["FlagDelete"] = "N";
                        data["UpdateBy"] = empInfo.EmpCode;
                        data["CreateBy"] = empInfo.EmpCode;
                        data["MerchantMapCode"] = merchantinfo.MerchantCode;


                        var response = wb.UploadValues(APIpath, "POST", data);

                        respstr = Encoding.UTF8.GetString(response);
                    }

                    int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                    if (sum > 0)
                    {

                        btnCancel_Click(null, null);

                        loadPointRate();

                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-PointRange').modal('hide');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                    }
                }
                else
                {
                    lblPrice_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ราคา";
                    lblGetPoint_Ins.Text = MessageConst._MSG_PLEASEINSERT + " พอยต์ที่ได้";
                }
            }

           
        }
            protected void btnSubmit_Click(object sender, EventArgs e)
        {
            hidFlagApprove.Value = "Y";
            hidFlagSavedraft.Value = "N";

            EmpInfo empInfo = new EmpInfo();

            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

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

                        APIpath = APIUrl + "/api/support/InsertPointRange";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PointCode"] = txtPromotionCode_Ins.Text;
                            data["PointName"] = txtPromotionName_Ins.Text;
                            data["PointSequence"] = txtPointSeq.Text;
                            data["PointBegin"] = txtPointBegin_Ins.Text;
                            data["PointEnd"] = txtPointEnd_Ins.Text;
                            data["UpdateBy"] = empInfo.EmpCode;
                            data["CreateBy"] = empInfo.EmpCode;
                            data["FlagDelete"] = "N";
                            data["MerchantMapCode"] = merchantinfo.MerchantCode;
                            

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                           
                            btnCancel_Click(null, null);

                            loadPointRange();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-PointRange').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    else //Update
                    {
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdatePointRange";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PointId"] = hidPointRangeID_Ins.Value;
                            data["PointCode"] = txtPromotionCode_Ins.Text;
                            data["PointName"] = txtPromotionName_Ins.Text;
                            data["PointSequence"] = txtPointSeq.Text;
                            data["PointBegin"] = txtPointBegin_Ins.Text;
                            data["PointEnd"] = txtPointEnd_Ins.Text;
                            data["FlagDelete"] = "N";
                            data["UpdateBy"] = empInfo.EmpCode;
                      


                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                          
                            btnCancel_Click(null, null);

                            loadPointRange();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-PointRange').modal('hide');", true);
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
            txtPromotionCode_Ins.Text = "";
            txtPromotionName_Ins.Text = "";


            HttpFileCollection uploadFiles = Request.Files;
            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile postedFile = uploadFiles[i];
                string x = postedFile.FileName;
                int y = postedFile.ContentLength;

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-PointRange').modal('hide');", true);
        }
        protected void gvPointRange_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            btnCheckSeq.Visible = true;
            txtPointBegin_Ins.Enabled = true;
            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvPointRange.Rows[index];

            Label lblmsg = (Label)row.FindControl("lblmsg");


            HiddenField hidPointId = (HiddenField)row.FindControl("hidPointId");
            HiddenField hidPointCode = (HiddenField)row.FindControl("hidPointCode");
            HiddenField hidPointName = (HiddenField)row.FindControl("hidPointName");
            HiddenField hidPointBegin = (HiddenField)row.FindControl("hidPointBegin");
            HiddenField hidPointEnd = (HiddenField)row.FindControl("hidPointEnd");  
            HiddenField hidPointSequence = (HiddenField)row.FindControl("hidPointSequence");
         

            if (e.CommandName == "ShowPointRange")
            {
                txtPointSeq.Enabled = true;

                txtPromotionCode_Ins.Text = hidPointCode.Value;
                txtPromotionName_Ins.Text = hidPointName.Value;
                txtPointBegin_Ins.Text = hidPointBegin.Value;
                txtPointEnd_Ins.Text = hidPointEnd.Value;
                hidPointRangeID_Ins.Value = hidPointId.Value;
                txtPointSeq.Text = hidPointSequence.Value;
                hidSeq_Spare.Value = hidPointSequence.Value;

                hidFlagInsert.Value = "False";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-PointRange').modal();", true);

            }

        }
        protected void ClearPointRangeModal()
        {
            txtPromotionCode_Ins.Text = "";
            lblPromotionCode_Ins.Text = "";
            txtPromotionName_Ins.Text = "";
            LbPromotionName_Ins.Text = "";
            txtPointSeq.Text = "";
            lblPointSeq_Ins.Text = "";
            txtPointBegin_Ins.Text = "";
            txtPointEnd_Ins.Text = "";
            lblPointRange_Ins.Text = "";


        }
        protected void btnAddPointRange_Click(object sender, EventArgs e)
        {
            ClearPointRangeModal();
            btnCheckSeq.Visible = false;
            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];
            string merchantname = merchantinfo.MerchantCode.Substring(0, 3);
            
            int lastcount = 0;
            int? lastPointId = 0;
            
            int? lastPointSequence = 0;
            int? lastPointEnd = 0;
            List<PointInfo> pointinfo = new List<PointInfo>();
            
            pointinfo = BindtxtPointRange(null);//Select where Merchant
            lastcount = pointinfo.Count();

            if (lastcount > 0)
            {
                lastPointId = pointinfo[lastcount - 1].PointId;
                lastPointSequence = pointinfo[lastcount - 1].PointSequence;
                lastPointEnd = pointinfo[lastcount - 1].PointEnd;
                
                txtPointBegin_Ins.Enabled = false;

            }
            else
            {
                txtPointBegin_Ins.Enabled = true;
            }
            
            hidFlagInsert.Value = "True";
            
            txtPointSeq.Text = (lastPointSequence + 1).ToString();
            txtPointBegin_Ins.Text = (lastPointEnd + 1).ToString();
            txtPointSeq.Enabled = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-PointRange').modal();", true);
        }

       
        

        protected void btnSavedraft_Click(object sender, EventArgs e)
        {
            hidFlagSavedraft.Value = "Y";
            hidFlagApprove.Value = "N";

            EmpInfo empInfo = new EmpInfo();

            MerchantInfo merchantinfo = new MerchantInfo();
            merchantinfo = (MerchantInfo)Session["MerchantInfo"];

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

                        APIpath = APIUrl + "/api/support/InsertPromotion";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionCode"] = txtPromotionCode_Ins.Text;
                            data["PromotionName"] = txtPromotionName_Ins.Text;
                            data["FlagDelete"] = "N";

                            data["CreateBy"] = empInfo.EmpCode;
                            data["MerchantCode"] = merchantinfo.MerchantCode;

                            data["Bu"] = hidBu.Value;
                            data["levels"] = hidLevels.Value;
                            data["RequestByEmpCode"] = hidEmpCode.Value;

                            if (hidFlagSavedraft.Value == "Y")
                            {
                                data["wfStatus"] = "Savedraft";
                                data["wfFinishFlag"] = "No";
                            }

                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            if (hidFlagSavedraft.Value == "Y")
                            {

                            }

                            btnCancel_Click(null, null);

                            loadPointRange();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-PointRange').modal('hide');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_ERROR + "');", true);
                        }

                    }
                    else //Update
                    {
                        
                        string respstr = "";

                        APIpath = APIUrl + "/api/support/UpdatePromotion";

                        using (var wb = new WebClient())
                        {
                            var data = new NameValueCollection();

                            data["PromotionId"] = hidIdList.Value;

                            data["PromotionCode"] = txtPromotionCode_Ins.Text;
                            data["PromotionName"] = txtPromotionName_Ins.Text;

                            data["FlagDelete"] = "N";
                           
                            var response = wb.UploadValues(APIpath, "POST", data);

                            respstr = Encoding.UTF8.GetString(response);
                        }

                        int? sum = JsonConvert.DeserializeObject<int?>(respstr);


                        if (sum > 0)
                        {
                            
                            btnCancel_Click(null, null);

                            loadPointRange();

                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + MessageConst._INSERT_SUCCESS + "');$('#modal-PointRange').modal('hide');", true);
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
        #endregion

        #region Binding

        protected string GetLink(object objCode)
        {
            string strCode = (objCode != null) ? objCode.ToString() : "";
            return "<a href=\"PromotionDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
        }



        
        protected Boolean validateInsert()
        {
            Boolean flag = true;
           
            if (txtPromotionCode_Ins.Text == "" || txtPromotionCode_Ins.Text == null)
            {
                flag = false;
                lblPromotionCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสระดับ";
            }
            else
            {
                List<PointInfo> pointinfo = new List<PointInfo>();
                pointinfo = BindtxtPointRange(txtPromotionCode_Ins.Text);
                if (CheckSymbol(txtPromotionCode_Ins.Text) == true)
                {
                    flag = false;
                    lblPromotionCode_Ins.Text = MessageConst._MSG_PLEASEINSERT + " รหัสระดับ";
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblPromotionCode_Ins.Text = "";
                }
                if (hidFlagInsert.Value == "true")
                {
                    if (pointinfo.Count > 0)
                    {
                        flag = false;
                        lblPromotionCode_Ins.Text = "รหัสPointRangeนี้มีอยู่แล้ว";
                    }
                }
                else
                {
                    flag = (flag == false) ? false : true;
                    lblPromotionCode_Ins.Text = "";
                }
            }

            if (txtPromotionName_Ins.Text == "")
            {
                flag = false;
                LbPromotionName_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ชื่อระดับ";
            }
            else
            {
                flag = (flag == false) ? false : true;
                LbPromotionName_Ins.Text = "";
            }
            if (txtPointBegin_Ins.Text == "" || txtPointEnd_Ins.Text == "")
            {
                flag = false;
                lblPointRange_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ระยะ Point ต้องไม่เป็นช่องว่าง";
            }
            else if (int.Parse(txtPointBegin_Ins.Text) >= int.Parse(txtPointEnd_Ins.Text))
            {
                flag = false;
                lblPointRange_Ins.Text = MessageConst._MSG_PLEASEINSERT + " PointBegin ต้องไม่มากกว่า หรือ เท่ากับ PointEnd";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPointRange_Ins.Text = "";
            }
            if (txtPointSeq.Text == "")
            {
                flag = false;
                lblPointSeq_Ins.Text = MessageConst._MSG_PLEASEINSERT + " ลำดับ Point ต้องไม่เป็นช่องว่าง";
            }
            else
            {
                flag = (flag == false) ? false : true;
                lblPointSeq_Ins.Text = "";
            }
            //open modal show error
            if (flag == false)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-PointRange').modal();", true);
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
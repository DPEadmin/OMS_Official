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

namespace DOMS_TSR.src.FullfillOrderlist
{
    public partial class ProgressOrderPLOL : System.Web.UI.Page
    {
        protected static string APIUrl;
        protected static string PromotionImgUrl = ConfigurationManager.AppSettings["PromotionImageUrl"];

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
                
                }
                else
                {
                    Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
                }


                loadData();

                

            }

        }
        protected void ddlPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPageNumber = Int32.Parse(ddlPage.SelectedValue);

            loadData();
        }

        #region Function



        protected void loadData()
        {
            List<OrderInfo> lPromotionInfo = new List<OrderInfo>();

            


            lPromotionInfo = GetPromotionMasterByCriteria();

            gvOrder.DataSource = lPromotionInfo;

            gvOrder.DataBind();


            

        }

        public List<OrderInfo> GetPromotionMasterByCriteria()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderByCriteriaOLPL";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["shipmentdate"] = txtSearchStartDateFrom.Text;

                data["StatusPage"] = "P";

                data["rowOFFSet"] = ((currentPageNumber - 1) * PAGE_SIZE).ToString();

                data["rowFetch"] = PAGE_SIZE.ToString();

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderInfo> lPromotionInfo = JsonConvert.DeserializeObject<List<OrderInfo>>(respstr);


            return lPromotionInfo;

        }

        public int? CountPromotionMasterList()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderByCriteriaOLPL";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["shipmentdate"] = txtSearchStartDateFrom.Text;

                data["ShipmentStatus"] = "P";

           

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            int? cou = JsonConvert.DeserializeObject<int?>(respstr);


            return cou;


        }
        public int CountRoundOfDay()
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/ListOrderByCriteriaOrderlistConfirmNo";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();


                data["OrderListDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            List<OrderListReturn> lInfo = JsonConvert.DeserializeObject<List<OrderListReturn>>(respstr);
            int cou = 0;
            if (lInfo.Count>0)
            {
                cou= int.Parse(lInfo[0].ConfirmNo.ToString()) + 1;
            }

            return cou;


        }
        public int UpdateConfirmNo(string ConfirmNo,string  DateShip)
        {
            string respstr = "";

            APIpath = APIUrl + "/api/support/UpdateOrderInfoOrderlist";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["ConfirmNo"] = ConfirmNo;
                data["OrderListDate"] = DateShip;
                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }
            int cou = JsonConvert.DeserializeObject<int>(respstr);
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


            loadData();
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

        protected Boolean DeletePromotion()
        {

            for (int i = 0; i < gvOrder.Rows.Count; i++)
            {
                CheckBox checkbox = (CheckBox)gvOrder.Rows[i].FindControl("chkPromotion");

                if (checkbox.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvOrder.Rows[i].FindControl("hidPromotionId");
                    HiddenField hidCode = (HiddenField)gvOrder.Rows[i].FindControl("hidPromotionCode");

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

                APIpath = APIUrl + "/api/support/DeletePromotion";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Idlist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            if (Codelist != "")
            {

                string respstr = "";

                APIpath = APIUrl + "/api/support/DeletePromtoionDetailInfoByCode";

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    data["PromotionCode"] = Codelist;


                    var response = wb.UploadValues(APIpath, "POST", data);

                    respstr = Encoding.UTF8.GetString(response);
                }

                int? cou = JsonConvert.DeserializeObject<int?>(respstr);




            }
            else
            {
            

                return false;
            }

          
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


        #endregion

        #region Event 

        protected void gvOrder_Change(object sender, GridViewPageEventArgs e)
        {
            gvOrder.PageIndex = e.NewPageIndex;

            List<OrderInfo> lPromotionInfo = new List<OrderInfo>();

            lPromotionInfo = GetPromotionMasterByCriteria();

            gvOrder.DataSource = lPromotionInfo;

            gvOrder.DataBind();

        }

        protected void chkPromotionAll_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOrder.Rows.Count; i++)
            {

                CheckBox chkall = (CheckBox)gvOrder.HeaderRow.FindControl("chkPromotionAll");

                if (chkall.Checked == true)
                {
                    HiddenField hidId = (HiddenField)gvOrder.Rows[i].FindControl("hidPromotionId");
                    HiddenField hidCode = (HiddenField)gvOrder.Rows[i].FindControl("hidPromotionCode");

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

                    CheckBox chkPromotion = (CheckBox)gvOrder.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = true;
                }
                else
                {

                    CheckBox chkPromotion = (CheckBox)gvOrder.Rows[i].FindControl("chkPromotion");

                    chkPromotion.Checked = false;
                }

            }
     
        }

        protected void gvOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

            HiddenField hidshipmentdate = (HiddenField)row.FindControl("hidshipmentdate");
            
            if (e.CommandName == "ShowOL")
            {
                int conNo = CountRoundOfDay();
                
                string pageurl = "Orderlist.ashx?date=" + DateTime.Parse(hidshipmentdate.Value.ToString()).ToString("yyyy-MM-dd") + "";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pageurl + "', '_blank');", true);
                

            }
            else if (e.CommandName == "ShowExcel")
            {
                
                string URL = "OLExportExcel.aspx?date=" + DateTime.Parse(hidshipmentdate.Value.ToString()).ToString("yyyy-MM-dd") + ""; ;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URL + "','_blank')", true);
            }
            else
            {
                string pageurl = "PLList.ashx?date=" + DateTime.Parse(hidshipmentdate.Value.ToString()).ToString("yyyy-MM-dd") + "";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + pageurl + "', '_blank');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            isdelete = DeletePromotion();

            btnSearch_Click(null, null);

            if (!isdelete)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('กรุณาเลือกรายการที่ต้องการลบ');", true);
            }
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearchStartDateFrom.Text = "";
        
     
        }
        
        protected void btnAddPromotion_Click(object sender, EventArgs e)
        {

            

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modal-Promotion').modal();", true);
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
            return "<a href=\"PromotionDetail.aspx?PromotionCode=" + strCode + "&MenuId=02\">" + strCode + "</a>";
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
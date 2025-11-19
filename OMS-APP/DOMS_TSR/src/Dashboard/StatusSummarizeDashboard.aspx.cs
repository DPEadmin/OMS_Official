using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Configuration;
using SALEORDER.DTO;
using Newtonsoft.Json;
using SALEORDER.Common;
using AjaxControlToolkit;
using System.IO;
using System.Globalization;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using System.Web.Http.Controllers;

namespace DOMS_TSR.src.Dashboard
{
    public partial class StatusSummarizeDashboard : System.Web.UI.Page
    {
        protected static int currentPageNumber;
        protected static int PAGE_SIZE = Convert.ToInt32(ConfigurationManager.AppSettings["PAGE_SIZE"]);
        protected static string APIUrl = "";
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

                bindDDLViewData();

              
                if (this.SelectedDate == DateTime.MinValue)
                {
                    this.PopulateYear();
                    this.PopulateMonth();
                    this.PopulateDay();
                }

                LoadDashBoard();
            }
            else {
                if (Request.Form[ddlDay.UniqueID] != null)
                {
                    this.PopulateDay();
                    ddlDay.ClearSelection();
                    ddlDay.Items.FindByValue(Request.Form[ddlDay.UniqueID]).Selected = true;
                }
            }
        }

        #region event
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDashBoard();
        }

        int[] sumStatus = new int[] { 0, 0, 0, 0, 0,0,0,0,0,0,0 };

        protected void gvMonthly_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus01 = (Label)e.Row.FindControl("lblStatus01");
                Label lblStatus02 = (Label)e.Row.FindControl("lblStatus02");
                Label lblStatus03 = (Label)e.Row.FindControl("lblStatus03");
                Label lblStatus04 = (Label)e.Row.FindControl("lblStatus04");
                Label lblStatus05 = (Label)e.Row.FindControl("lblStatus05");
                Label lblStatus06 = (Label)e.Row.FindControl("lblStatus06");
                Label lblStatus07 = (Label)e.Row.FindControl("lblStatus07");
                Label lblStatus08 = (Label)e.Row.FindControl("lblStatus08");
                Label lblStatus09 = (Label)e.Row.FindControl("lblStatus09");
                Label lblStatus10 = (Label)e.Row.FindControl("lblStatus10");
                Label lblStatus11 = (Label)e.Row.FindControl("lblStatus11");

                sumStatus[0] += Convert.ToInt32(lblStatus01.Text);
                sumStatus[1] += Convert.ToInt32(lblStatus02.Text);
                sumStatus[2] += Convert.ToInt32(lblStatus03.Text);
                sumStatus[3] += Convert.ToInt32(lblStatus04.Text);
                sumStatus[4] += Convert.ToInt32(lblStatus05.Text);
                sumStatus[5] += Convert.ToInt32(lblStatus06.Text);
                sumStatus[6] += Convert.ToInt32(lblStatus07.Text);
                sumStatus[7] += Convert.ToInt32(lblStatus08.Text);
                sumStatus[8] += Convert.ToInt32(lblStatus09.Text);
                sumStatus[9] += Convert.ToInt32(lblStatus10.Text);
                sumStatus[10] += Convert.ToInt32(lblStatus11.Text);
            }
            if(e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblSum01 = (Label)e.Row.FindControl("lblSum01");
                Label lblSum02 = (Label)e.Row.FindControl("lblSum02");
                Label lblSum03 = (Label)e.Row.FindControl("lblSum03");
                Label lblSum04 = (Label)e.Row.FindControl("lblSum04");
                Label lblSum05 = (Label)e.Row.FindControl("lblSum05");
                Label lblSum06 = (Label)e.Row.FindControl("lblSum06");
                Label lblSum07 = (Label)e.Row.FindControl("lblSum07");
                Label lblSum08 = (Label)e.Row.FindControl("lblSum08");
                Label lblSum09 = (Label)e.Row.FindControl("lblSum09");
                Label lblSum10 = (Label)e.Row.FindControl("lblSum10");
                Label lblSum11 = (Label)e.Row.FindControl("lblSum11");

                lblSum01.Text = sumStatus[0].ToString();
                lblSum02.Text = sumStatus[1].ToString();
                lblSum03.Text = sumStatus[2].ToString();
                lblSum04.Text = sumStatus[3].ToString();
                lblSum05.Text = sumStatus[4].ToString();
                lblSum06.Text = sumStatus[5].ToString();
                lblSum07.Text = sumStatus[6].ToString();
                lblSum08.Text = sumStatus[7].ToString();
                lblSum09.Text = sumStatus[8].ToString();
                lblSum10.Text = sumStatus[9].ToString();
                lblSum11.Text = sumStatus[10].ToString();
            }
        }
        #endregion event

        #region function
        protected List<StatusInfo> LoadStatus()
        {
            List<StatusInfo> lmon = new List<StatusInfo>();

            StatusInfo mon = new StatusInfo();

            APIpath = APIUrl + "/api/support/ListDashBoardStatus";
            List<StatusInfo> lStatusInfo = new List<StatusInfo>();

            var startDate = new DateTime(int.Parse(ddlYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

            string DMY = "" + ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue + "";

            using (var client = new WebClient())
            {
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "0")
                {
                    mon.DayStartMonth = DMY;
                    mon.DayEndMonth = DMY;
                }
                else
                {
                    switch (ddlMonth.SelectedValue)
                    {
                        case "1":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "2":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "3":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "4":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "5":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "6":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "7":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "8":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "9":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "10":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "11":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        case "12":
                            mon.DayStartMonth = startDate.ToString("dd/MM/yyyy");
                            mon.DayEndMonth = endDate.ToString();
                            break;
                        default:
                            // code block
                            break;
                    }
                }

                mon.ViewDataType = ddlViewData.SelectedValue;

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.DayStartMonth,
                    mon.DayEndMonth,
                });
                var dataString = client.UploadString(APIpath, jsonObj);
                lStatusInfo = JsonConvert.DeserializeObject<List<StatusInfo>>(dataString.ToString());

            }

            return lStatusInfo;
        }
        #endregion function

        #region binding
        protected void LoadDashBoard()
        {
            List<StatusInfo> lstatus = new List<StatusInfo>();
            lblDashboard.Text = ddlViewData.SelectedItem.ToString() + " in " + ddlMonth.SelectedItem.Text.ToString() + " " + ddlYear.SelectedItem.Text.ToString();
            lstatus = LoadStatus();
            gvMonthly.Visible = true;
            gvMonthly.DataSource = lstatus;
            gvMonthly.DataBind();
        }

        protected void bindDDLViewData()
        {
            ddlViewData.Items.Insert(0, new ListItem("Total of Status", "1"));
        }
        private void PopulateDay()
        {
            ddlDay.Items.Clear();
            ListItem lt = new ListItem();
            
            int days = DateTime.DaysInMonth(this.Year, this.Month);
            for (int i = 1; i <= days; i++)
            {
                lt = new ListItem();
                lt.Text = i.ToString();
                lt.Value = i.ToString();
                ddlDay.Items.Add(lt);
            }
            ddlDay.Items.Insert(0, new ListItem("ALL", "0"));
            ddlDay.Items.FindByValue(DateTime.Now.Day.ToString()).Selected = true;
        }

        private void PopulateMonth()
        {
            ddlMonth.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "MM";
            lt.Value = "0";
            ddlMonth.Items.Add(lt);
            for (int i = 1; i <= 12; i++)
            {
                lt = new ListItem();
                lt.Text = Convert.ToDateTime(i.ToString() + "/1/1900").ToString("MMMM");
                lt.Value = i.ToString();
                ddlMonth.Items.Add(lt);
            }

            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
        }

        private void PopulateYear()
        {
            ddlYear.Items.Clear();
            ListItem lt = new ListItem();
            lt.Text = "YYYY";
            lt.Value = "0";
            ddlYear.Items.Add(lt);
            for (int i = DateTime.Now.Year; i >= 2019; i--)
            {
                lt = new ListItem();
                lt.Text = i.ToString();
                lt.Value = i.ToString();
                ddlYear.Items.Add(lt);
            }
            ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
        }
        private int Day
        {
            get
            {
                if (Request.Form[ddlDay.UniqueID] != null)
                {
                    return int.Parse(Request.Form[ddlDay.UniqueID]);
                }
                else
                {
                    return int.Parse(DateTime.Now.Date.ToString());
                }
            }
            set
            {
                this.PopulateDay();
                ddlDay.ClearSelection();
                ddlDay.Items.FindByValue(value.ToString()).Selected = true;
            }
        }
        private int Month
        {
            get
            {
                return int.Parse(DateTime.Now.Month.ToString());
            }
            set
            {
                this.PopulateMonth();
                ddlMonth.ClearSelection();
                ddlMonth.Items.FindByValue(value.ToString()).Selected = true;
            }
        }
        private int Year
        {
            get
            {
                return int.Parse(DateTime.Now.Year.ToString());
            }
            set
            {
                this.PopulateYear();
                ddlYear.ClearSelection();
                ddlYear.Items.FindByValue(value.ToString()).Selected = true;
            }
        }
        public DateTime SelectedDate
        {
            get
            {
                try
                {
                    return DateTime.Parse(this.Month + "/" + this.Day + "/" + this.Year);
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set
            {
                if (!value.Equals(DateTime.MinValue))
                {
                    this.Year = value.Year;
                    this.Month = value.Month;
                    this.Day = value.Day;
                }
            }
        }
        
        #endregion binding

    }
}
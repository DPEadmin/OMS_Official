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
using System.Globalization;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using System.Web.Http.Controllers;

namespace DOMS_TSR.src.Dashboard
{
    public partial class ProductSummarizeDashboard : System.Web.UI.Page
    {
        string Codelist = "";
        string EditFlag = "";
        Boolean isdelete;
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
            else
            {
                if (Request.Form[ddlDay.UniqueID] != null)
                {
                    this.PopulateDay();
                    ddlDay.ClearSelection();
                    ddlDay.Items.FindByValue(Request.Form[ddlDay.UniqueID]).Selected = true;
                }
            }
        }
        #region Function

        protected void LoadDashBoard()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();
            List<ProductAmountInfo> lPmon = new List<ProductAmountInfo>();
            List<PromotionAmountInfo> lPromotion = new List<PromotionAmountInfo>();

            lblDashboard.Text = ddlViewData.SelectedItem.ToString() + " in " + ddlMonth.SelectedItem.Text.ToString() + " " + ddlYear.SelectedItem.Text.ToString();
           
            
            LoadtotalTotalHeader();

            switch (ddlViewData.SelectedValue)
            {
                case "1":

                    lmon = LoadtotalCallIn();
                    gvMonthly.Visible = true;
                    gvMonthly.DataSource = lmon;

                    gvMonthly.DataBind();

                    break;

                case "2":

                    lmon = LoadtotalAmount();
                    gvMonthly.Visible = true;
                    gvMonthly.DataSource = lmon;

                    gvMonthly.DataBind();

                    break;

                case "3":

                    lmon = LoadtotalAmountOrderandPrice();
                    gvMonthly.Visible = true;
                    gvMonthly.DataSource = lmon;

                    gvMonthly.DataBind();

                    break;
                case "4":

                    lmon = LoadtotalPercentOrder();
                    gvMonthly.Visible = true;
                    gvMonthly.DataSource = lmon;

                    gvMonthly.DataBind();

                    break;
                case "5":
                   
                    lmon = LoadtotalAverCallIn();
                    gvMonthly.Visible = true;
                    gvMonthly.DataSource = lmon;

                    gvMonthly.DataBind();

                    break;

                case "6":

                    lmon = LoadtotalAverAmountPerHour();
                    gvMonthly.Visible = true;
                    gvMonthly.DataSource = lmon;

                    gvMonthly.DataBind();

                    break;
                case "7":
                    lmon = LoadtotalAmount();
                    if (lmon.Count > 0)
                    {
                        gvMonthly.DataSource = lmon;
                        gvMonthly.DataBind();
                    }
                    else
                    {
                        gvMonthly.DataSource = null;
                        gvMonthly.DataBind();
                    }
                    lPmon = LoadproductAmount();
                   
                    if(lPmon.Count>0)
                    {
                        GvproductAmount.DataSource = lPmon;
                        GvproductAmount.DataBind();

                        gvPromotionAmount.DataSource = lPromotion;
                        gvPromotionAmount.DataBind();
                    }
                   
                       else
                    {
                        GvproductAmount.DataSource = null;
                        GvproductAmount.DataBind();

                        
                    }
                    break;
                default:

                    lmon = LoadtotalCall();

                    gvMonthly.DataSource = lmon;

                    gvMonthly.DataBind();

                    break;
            }
        }
      
        protected List<MonthlyInfo> LoadtotalCall()
        {
            lblDashboard.Text = ddlViewData.SelectedItem.ToString() + " in " + ddlMonth.SelectedItem.Text.ToString() + " " + ddlYear.SelectedItem.Text.ToString();
            string nowdate;
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();

            MonthlyInfo mon = new MonthlyInfo();
            DateTime now = DateTime.Now;
            var startDate = new DateTime(int.Parse(ddlYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

            if (now.Month.ToString() == ddlMonth.SelectedValue)
            {
                nowdate = now.Day.ToString();
                
            }
            else
            {
                nowdate = DateTime.DaysInMonth(int.Parse(ddlYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString())).ToString();
            }


            if (ddlDay.SelectedValue != "")
            {
                lmon.Add(new MonthlyInfo()
                {
                    Day = "" + ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue.ToString() + "" + ddlYear.SelectedValue + "",
                    Hour0 = "8",
                    Hour1 = "15",
                    Hour2 = "30",
                    Hour3 = "89",
                    Hour4 = "35",
                    Hour5 = "50",
                    Hour6 = "43",
                    Hour7 = "67",
                    Hour8 = "21",
                    Hour9 = "68",
                    Hour10 = "95",
                    Hour11 = "75",
                    Hour12 = "32",
                    Hour13 = "12",
                    Hour14 = "21",
                    Hour15 = "77",
                    Hour16 = "5",
                    Hour17 = "89",
                    Hour18 = "45",
                    Hour19 = "54",
                    Hour20 = "32",
                    Hour21 = "47",
                    Hour22 = "42",
                    Hour23 = "87"
                     ,
                    TotalAmount = "135",
                    OAllOrdercount = "120"
                });
            }
            else 
            {
                for (int i = 1; i <= Convert.ToInt32(nowdate); i++)
                {
                    if (IsOdd(i))
                    {
                        lmon.Add(new MonthlyInfo()
                        {
                            Day = "" + i.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "" + int.Parse(ddlYear.SelectedValue.ToString()) + "",
                            Hour0 = "15",
                            Hour1 = "200",
                            Hour2 = "153",
                            Hour3 = "89",
                            Hour4 = "35",
                            Hour5 = "500",
                            Hour6 = "240",
                            Hour7 = "67",
                            Hour8 = "65",
                            Hour9 = "89",
                            Hour10 = "95",
                            Hour11 = "145",
                            Hour12 = "150",
                            Hour13 = "152",
                            Hour14 = "65",
                            Hour15 = "77",
                            Hour16 = "78",
                            Hour17 = "89",
                            Hour18 = "45",
                            Hour19 = "54",
                            Hour20 = "460",
                            Hour21 = "160",
                            Hour22 = "300",
                            Hour23 = "154"
                             ,
                            TotalAmount = "135",
                            OAllOrdercount = "120"
                        });
                    }
                    else
                    {

                        lmon.Add(new MonthlyInfo()
                        {
                            Day = "" + i.ToString() + "/" + ddlMonth.SelectedValue.ToString() + "" + int.Parse(ddlYear.SelectedValue.ToString()) + "",
                            Hour0 = "8",
                            Hour1 = "15",
                            Hour2 = "30",
                            Hour3 = "89",
                            Hour4 = "35",
                            Hour5 = "50",
                            Hour6 = "43",
                            Hour7 = "67",
                            Hour8 = "21",
                            Hour9 = "68",
                            Hour10 = "95",
                            Hour11 = "75",
                            Hour12 = "32",
                            Hour13 = "12",
                            Hour14 = "21",
                            Hour15 = "77",
                            Hour16 = "5",
                            Hour17 = "89",
                            Hour18 = "45",
                            Hour19 = "54",
                            Hour20 = "32",
                            Hour21 = "47",
                            Hour22 = "42",
                            Hour23 = "87"
                            ,TotalAmount ="135",OAllOrdercount = "120"                 });
                    }

                }
            }
   
           
            
            return (lmon);
        }

        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
        protected List<MonthlyInfo> LoadtotalAmount()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();

            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashBoardTotalAmountOrder";
            
            List<MonthlyInfo> lMonInfo = new List<MonthlyInfo>();

            DateTime now = DateTime.Now;
            var startDate = new DateTime(int.Parse(ddlYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

            string DMY = "" + ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue + "";
            using (var client = new WebClient())
            {
                if (ddlYear.SelectedValue!="" && ddlDay.SelectedValue!="0") 
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
                lMonInfo = JsonConvert.DeserializeObject<List<MonthlyInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }

        protected List<MonthlyInfo> LoadtotalPercentOrder()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();

            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashBoardPercentOrder";
            List<MonthlyInfo> lMonInfo = new List<MonthlyInfo>();

            DateTime now = DateTime.Now;
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
                lMonInfo = JsonConvert.DeserializeObject<List<MonthlyInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }
        protected List<MonthlyInfo> LoadtotalAmountOrderandPrice()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();

            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashAmountOrderandPrice";
            List<MonthlyInfo> lMonInfo = new List<MonthlyInfo>();

            DateTime now = DateTime.Now;
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
                lMonInfo = JsonConvert.DeserializeObject<List<MonthlyInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }

        protected List<MonthlyInfo> LoadtotalCallIn()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();

            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashBoardCallIn";
            List<MonthlyInfo> lMonInfo = new List<MonthlyInfo>();

            DateTime now = DateTime.Now;
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
                lMonInfo = JsonConvert.DeserializeObject<List<MonthlyInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }

        
        protected List<MonthlyInfo> LoadtotalAverCallIn()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();

            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashBoardAverCallIn";
            List<MonthlyInfo> lMonInfo = new List<MonthlyInfo>();

            DateTime now = DateTime.Now;
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
                lMonInfo = JsonConvert.DeserializeObject<List<MonthlyInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }
        protected List<MonthlyInfo> LoadtotalAverAmountPerHour()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();

            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashBoardAverAmountPerHour";
            List<MonthlyInfo> lMonInfo = new List<MonthlyInfo>();

            DateTime now = DateTime.Now;
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
                lMonInfo = JsonConvert.DeserializeObject<List<MonthlyInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }
        protected void LoadtotalTotalHeader()
        {
            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashTotalHeader";
            
            List<TotalMonthlyInfo> lMonInfo = new List<TotalMonthlyInfo>();

            DateTime now = DateTime.Now;
            var startDate = new DateTime(int.Parse(ddlYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

            string DMY = "" + ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue + "";
            using (var client = new WebClient())
            {
                if (ddlYear.SelectedValue != "" &&  ddlDay.SelectedValue != "0")
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
                lMonInfo = JsonConvert.DeserializeObject<List<TotalMonthlyInfo>>(dataString.ToString());
                if (lMonInfo[0].totalcall.ToString() == "0"&& lMonInfo[0].totalorderprice.ToString() == "0"&& lMonInfo[0].totalcall.ToString()=="0")
                {
                    lblsumtotalcall.Text = lMonInfo[0].totalcall;
                    lblsumtotalorder.Text = lMonInfo[0].totalorder;
                    lblsumtotalamount.Text = "0";
                    lblpercentorder.Text = "0";
                    lblavgcall.Text = "0";
                    lblavgamounthrs.Text = "0";
                }
                else 
                {
                    lblsumtotalcall.Text = lMonInfo[0].totalcall;
                    lblsumtotalorder.Text = lMonInfo[0].totalorder;
                    lblsumtotalamount.Text = string.Format("{0:n}", decimal.Parse(lMonInfo[0].totalorderprice.ToString()));
                    lblpercentorder.Text = string.Format("{0:n}", (decimal.Parse(lMonInfo[0].totalorder.ToString()) * 100 / decimal.Parse(lMonInfo[0].totalcall.ToString())));
                    lblavgcall.Text = string.Format("{0:n}", decimal.Parse(lMonInfo[0].totalcall.ToString()) / 24);
                    lblavgamounthrs.Text = string.Format("{0:n}", decimal.Parse(lMonInfo[0].totalorderprice.ToString()) / 24);
                }
              
            }

       

        }

        protected List<ProductAmountInfo> LoadproductAmount()
        {
            MonthlyInfo mon = new MonthlyInfo();
            APIpath = APIUrl + "/api/support/ListDashBoardProductAmount";
            
            List<ProductAmountInfo> lMonInfo = new List<ProductAmountInfo>();
            string respstr = "";
            DateTime now = DateTime.Now;
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
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;
           
                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.DayStartMonth,
                    mon.DayEndMonth,
                    
                });
                var dataString = client.UploadString(APIpath, jsonObj);           
                lMonInfo = JsonConvert.DeserializeObject<List<ProductAmountInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }

        protected List<PromotionAmountInfo> LoadPromotionAmount()
        {
            MonthlyInfo mon = new MonthlyInfo();
            
            APIpath = "http://localhost:54545" + "/api/support/ListDashBoardPromotionAmount";
            List<PromotionAmountInfo> lMonInfo = new List<PromotionAmountInfo>();
            string respstr = "";
            DateTime now = DateTime.Now;
            var startDate = new DateTime(int.Parse(ddlYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
            string DMY="";
            
            
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
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                client.Encoding = Encoding.UTF8;

                var jsonObj = JsonConvert.SerializeObject(new
                {
                    mon.DayStartMonth,
                    mon.DayEndMonth,
                    
                });
                var dataString = client.UploadString(APIpath, jsonObj);
                lMonInfo = JsonConvert.DeserializeObject<List<PromotionAmountInfo>>(dataString.ToString());

            }

            return lMonInfo;

        }

        #endregion


        #region Event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
              LoadDashBoard();
        }

        #endregion

        #region Binding
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
        protected void bindDDLViewData()
        {
          

        
           
            ddlViewData.Items.Insert(0, new ListItem("Product Summary", "7"));
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
        

        decimal H0 = 0, H1 = 0, H2 = 0, H3 = 0, H4 = 0, H5 = 0, H6 = 0, H7 = 0, H8 = 0, H9 = 0, H10 = 0, H11 = 0, H12 = 0, H13 = 0, H14 = 0, H15 = 0, H16 = 0,
            H17 = 0, H18 = 0, H19 = 0, H20 = 0, H21 = 0, H22 = 0, H23 = 0;
        decimal GHT =0;
        decimal TAmount =0;
        decimal OAllOrder = 0;
        protected void gvMonthly_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblHour0 = (Label)e.Row.FindControl("lblHour0");
                Label lblHour1 = (Label)e.Row.FindControl("lblHour1");
                Label lblHour2 = (Label)e.Row.FindControl("lblHour2");
                Label lblHour3 = (Label)e.Row.FindControl("lblHour3");
                Label lblHour4 = (Label)e.Row.FindControl("lblHour4");

                Label lblHour5 = (Label)e.Row.FindControl("lblHour5");
                Label lblHour6 = (Label)e.Row.FindControl("lblHour6");
                Label lblHour7 = (Label)e.Row.FindControl("lblHour7");
                Label lblHour8 = (Label)e.Row.FindControl("lblHour8");
                Label lblHour9 = (Label)e.Row.FindControl("lblHour9");

                Label lblHour10 = (Label)e.Row.FindControl("lblHour10");
                Label lblHour11 = (Label)e.Row.FindControl("lblHour11");
                Label lblHour12 = (Label)e.Row.FindControl("lblHour12");
                Label lblHour13 = (Label)e.Row.FindControl("lblHour13");
                Label lblHour14= (Label)e.Row.FindControl("lblHour14");

                Label lblHour15 = (Label)e.Row.FindControl("lblHour15");
                Label lblHour16 = (Label)e.Row.FindControl("lblHour16");
                Label lblHour17 = (Label)e.Row.FindControl("lblHour17");
                Label lblHour18 = (Label)e.Row.FindControl("lblHour18");
                Label lblHour19 = (Label)e.Row.FindControl("lblHour19");

                Label lblHour20 = (Label)e.Row.FindControl("lblHour20");
                Label lblHour21 = (Label)e.Row.FindControl("lblHour21");
                Label lblHour22 = (Label)e.Row.FindControl("lblHour22");
                Label lblHour23 = (Label)e.Row.FindControl("lblHour23");

                lblHour0.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour0.Text)));
                lblHour1.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour1.Text)));
                lblHour2.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour2.Text)));
                lblHour3.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour3.Text)));
                lblHour4.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour4.Text)));
                lblHour5.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour5.Text)));
                lblHour6.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour6.Text)));
                lblHour7.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour7.Text)));
                lblHour8.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour8.Text)));
                lblHour9.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour9.Text)));
                lblHour10.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour10.Text)));
                lblHour11.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour11.Text)));
                lblHour12.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour12.Text)));
                lblHour13.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour13.Text)));
                lblHour14.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour14.Text)));
                lblHour15.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour15.Text)));
                lblHour16.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour16.Text)));
                lblHour17.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour17.Text)));
                lblHour18.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour18.Text)));
                lblHour19.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour19.Text)));
                lblHour20.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour20.Text)));
                lblHour21.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour21.Text)));
                lblHour22.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour22.Text)));
                lblHour23.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblHour23.Text)));
                

            }

        }

        Int64 pQuanlity; decimal pAmont;
            protected void GvproductAmount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblQuanlity = (Label)e.Row.FindControl("lblQuanlity");
                Label lblAmount = (Label)e.Row.FindControl("lblAmount");
                Label lblAmount1 = (Label)e.Row.FindControl("lblAmount1");
                HiddenField HidAmount = (HiddenField)e.Row.FindControl("HidAmount");
                
                pQuanlity += Convert.ToInt64(lblQuanlity.Text);
                pAmont += Convert.ToDecimal(lblAmount.Text);
                lblAmount.Text = string.Format("{0:N2}", ( Convert.ToDecimal( lblAmount.Text)));
            
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFQuanlity = (Label)e.Row.FindControl("lblFQuanlity");
                Label lblFAmount = (Label)e.Row.FindControl("lblFAmount");
              
            
                Label lblFOAllOrdercount = (Label)e.Row.FindControl("lblFOAllOrdercount");


                lblFQuanlity.Text = string.Format("{0:#,0}", (pQuanlity));
                lblFAmount.Text = string.Format("{0:N2}", (pAmont));
                 
                 
               
            }
        }

        Int64 PromotionQuanlity; decimal PromotionAmont;
        protected void gvPromotionAmount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblPromotionQuanlity = (Label)e.Row.FindControl("lblPromotionQuanlity");
                Label lblPromotionAmount = (Label)e.Row.FindControl("lblPromotionAmount");
                Label lblAmount1 = (Label)e.Row.FindControl("lblAmount1");
                HiddenField HidAmount = (HiddenField)e.Row.FindControl("HidAmount");

                PromotionQuanlity += Convert.ToInt64(lblPromotionQuanlity.Text);
                PromotionAmont += Convert.ToDecimal(lblPromotionAmount.Text);
                lblPromotionAmount.Text = string.Format("{0:N2}", (Convert.ToDecimal(lblPromotionAmount.Text)));
                
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFPromotionQuanlity = (Label)e.Row.FindControl("lblFPromotionQuanlity");
                Label lblFPromotionAmount = (Label)e.Row.FindControl("lblFPromotionAmount");


                Label lblFOAllOrdercount = (Label)e.Row.FindControl("lblFOAllOrdercount");


                lblFPromotionQuanlity.Text = string.Format("{0:#,0}", (PromotionQuanlity));
                lblFPromotionAmount.Text = string.Format("{0:N2}", (PromotionAmont));



            }
        }
        #endregion

    }
}
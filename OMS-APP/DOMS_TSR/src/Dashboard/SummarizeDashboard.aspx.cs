using Newtonsoft.Json;
using SALEORDER.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DOMS_TSR.src.Dashboard
{
    public partial class SummarizeDashboard : System.Web.UI.Page
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

                bindDDLYear();

                bindDDLMonth();

                bindDDLDay();

                LoadDashBoard();

            }
        }
        #region Function

        protected void LoadDashBoard()
        {
            List<MonthlyInfo> lmon = new List<MonthlyInfo>();
            List<ProductAmountInfo> lPmon = new List<ProductAmountInfo>();

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
                            ,
                            TotalAmount = "135",
                            OAllOrdercount = "120"
                        });
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

            APIpath = APIUrl + "/api/support/ListDashBoardAmountOrder";
            List<MonthlyInfo> lMonInfo = new List<MonthlyInfo>();

            DateTime now = DateTime.Now;
            var startDate = new DateTime(int.Parse(ddlYear.SelectedValue.ToString()), int.Parse(ddlMonth.SelectedValue.ToString()), 1);
            var endDate = startDate.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

            string DMY = "" + ddlDay.SelectedValue + "/" + ddlMonth.SelectedValue.ToString() + "/" + ddlYear.SelectedValue + "";
            using (var client = new WebClient())
            {
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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
                if (lMonInfo[0].totalcall.ToString() == "0" && lMonInfo[0].totalorderprice.ToString() == "0" && lMonInfo[0].totalcall.ToString() == "0")
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
                    lblsumtotalamount.Text = string.Format("{0:0.00}", decimal.Parse(lMonInfo[0].totalorderprice.ToString()));
                    lblpercentorder.Text = string.Format("{0:0.00}", (decimal.Parse(lMonInfo[0].totalorder.ToString()) * 100 / decimal.Parse(lMonInfo[0].totalcall.ToString())));
                    lblavgcall.Text = string.Format("{0:0.00}", decimal.Parse(lMonInfo[0].totalcall.ToString()) / 24);
                    lblavgamounthrs.Text = string.Format("{0:0.00}", decimal.Parse(lMonInfo[0].totalorderprice.ToString()) / 24);
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
                if (ddlYear.SelectedValue != "" && ddlDay.SelectedValue != "")
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

        #endregion


        #region Event

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDashBoard();
        }

        #endregion

        #region Binding

        protected void bindDDLViewData()
        {


            ddlViewData.Items.Insert(0, new ListItem("Total of Call", "1"));
            ddlViewData.Items.Insert(1, new ListItem("Total Order", "2"));
            ddlViewData.Items.Insert(2, new ListItem("Total Amount", "3"));


        }
        protected void bindDDLYear()
        {


            ddlYear.Items.Insert(0, new ListItem("2020", "2020"));
            ddlYear.Items.Insert(1, new ListItem("2019", "2019"));
            ddlYear.Items.Insert(2, new ListItem("2018", "2018"));
            ddlYear.Items.Insert(3, new ListItem("2017", "2017"));
            ddlYear.Items.Insert(4, new ListItem("2016", "2016"));
            ddlYear.Items.Insert(5, new ListItem("2015", "2015"));
            ddlYear.Items.Insert(6, new ListItem("2014", "2014"));


            ddlYear.SelectedValue = DateTime.Now.Year.ToString();

        }
        protected void bindDDLDay()
        {


            ddlDay.Items.Insert(0, new ListItem("[All]", ""));
            ddlDay.Items.Insert(1, new ListItem("1", "1"));
            ddlDay.Items.Insert(2, new ListItem("2", "2"));
            ddlDay.Items.Insert(3, new ListItem("3", "3"));
            ddlDay.Items.Insert(4, new ListItem("4", "4"));
            ddlDay.Items.Insert(5, new ListItem("5", "5"));
            ddlDay.Items.Insert(6, new ListItem("6", "6"));
            ddlDay.Items.Insert(7, new ListItem("7", "7"));
            ddlDay.Items.Insert(8, new ListItem("8", "8"));
            ddlDay.Items.Insert(9, new ListItem("9", "9"));
            ddlDay.Items.Insert(10, new ListItem("10", "10"));
            ddlDay.Items.Insert(11, new ListItem("11", "11"));
            ddlDay.Items.Insert(12, new ListItem("12", "12"));
            ddlDay.Items.Insert(13, new ListItem("13", "13"));
            ddlDay.Items.Insert(14, new ListItem("14", "14"));
            ddlDay.Items.Insert(15, new ListItem("15", "15"));
            ddlDay.Items.Insert(16, new ListItem("16", "16"));
            ddlDay.Items.Insert(17, new ListItem("17", "17"));
            ddlDay.Items.Insert(18, new ListItem("18", "18"));
            ddlDay.Items.Insert(19, new ListItem("19", "19"));
            ddlDay.Items.Insert(20, new ListItem("20", "20"));
            ddlDay.Items.Insert(21, new ListItem("21", "21"));
            ddlDay.Items.Insert(22, new ListItem("22", "22"));
            ddlDay.Items.Insert(23, new ListItem("23", "23"));
            ddlDay.Items.Insert(24, new ListItem("24", "24"));
            ddlDay.Items.Insert(25, new ListItem("25", "25"));
            ddlDay.Items.Insert(26, new ListItem("26", "26"));
            ddlDay.Items.Insert(27, new ListItem("27", "27"));
            ddlDay.Items.Insert(28, new ListItem("28", "28"));
            ddlDay.Items.Insert(29, new ListItem("29", "29"));
            ddlDay.Items.Insert(30, new ListItem("30", "30"));
            ddlDay.Items.Insert(31, new ListItem("31", "31"));

            string aaaa = DateTime.Now.Month.ToString();
            ddlDay.SelectedValue = DateTime.Now.Date.ToString();

        }

        protected void bindDDLMonth()
        {


            ddlMonth.Items.Insert(0, new ListItem("January", "1"));
            ddlMonth.Items.Insert(1, new ListItem("Febuary", "2"));
            ddlMonth.Items.Insert(2, new ListItem("March", "3"));
            ddlMonth.Items.Insert(3, new ListItem("April", "4"));
            ddlMonth.Items.Insert(4, new ListItem("May", "5"));
            ddlMonth.Items.Insert(5, new ListItem("June", "6"));
            ddlMonth.Items.Insert(6, new ListItem("July", "7"));
            ddlMonth.Items.Insert(7, new ListItem("August", "8"));
            ddlMonth.Items.Insert(8, new ListItem("September", "9"));
            ddlMonth.Items.Insert(9, new ListItem("October", "10"));
            ddlMonth.Items.Insert(10, new ListItem("November", "11"));
            ddlMonth.Items.Insert(11, new ListItem("December", "12"));


            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();

        }

        decimal H0 = 0, H1 = 0, H2 = 0, H3 = 0, H4 = 0, H5 = 0, H6 = 0, H7 = 0, H8 = 0, H9 = 0, H10 = 0, H11 = 0, H12 = 0, H13 = 0, H14 = 0, H15 = 0, H16 = 0,
            H17 = 0, H18 = 0, H19 = 0, H20 = 0, H21 = 0, H22 = 0, H23 = 0;
        decimal GHT = 0;
        decimal TAmount = 0;
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
                Label lblHour14 = (Label)e.Row.FindControl("lblHour14");

                Label lblHour15 = (Label)e.Row.FindControl("lblHour15");
                Label lblHour16 = (Label)e.Row.FindControl("lblHour16");
                Label lblHour17 = (Label)e.Row.FindControl("lblHour17");
                Label lblHour18 = (Label)e.Row.FindControl("lblHour18");
                Label lblHour19 = (Label)e.Row.FindControl("lblHour19");

                Label lblHour20 = (Label)e.Row.FindControl("lblHour20");
                Label lblHour21 = (Label)e.Row.FindControl("lblHour21");
                Label lblHour22 = (Label)e.Row.FindControl("lblHour22");
                Label lblHour23 = (Label)e.Row.FindControl("lblHour23");
                Label LblHDayTotal = (Label)e.Row.FindControl("LblHDayTotal");
                HiddenField hidTotalAmount = (HiddenField)e.Row.FindControl("hidTotalAmount");
                Label lbloTotal = (Label)e.Row.FindControl("lbloTotal");
                Label lblOAllOrdercount = (Label)e.Row.FindControl("lblOAllOrdercount");
                OAllOrder = Decimal.Parse(lblOAllOrdercount.Text);
                H0 += Convert.ToDecimal(lblHour0.Text);
                H1 += Convert.ToDecimal(lblHour1.Text);
                H2 += Convert.ToDecimal(lblHour2.Text);
                H3 += Convert.ToDecimal(lblHour3.Text);
                H4 += Convert.ToDecimal(lblHour4.Text);
                H5 += Convert.ToDecimal(lblHour5.Text);
                H6 += Convert.ToDecimal(lblHour6.Text);
                H7 += Convert.ToDecimal(lblHour7.Text);
                H8 += Convert.ToDecimal(lblHour8.Text);
                H9 += Convert.ToDecimal(lblHour9.Text);
                H10 += Convert.ToDecimal(lblHour10.Text);
                H11 += Convert.ToDecimal(lblHour11.Text);
                H12 += Convert.ToDecimal(lblHour12.Text);
                H13 += Convert.ToDecimal(lblHour13.Text);
                H14 += Convert.ToDecimal(lblHour14.Text);
                H15 += Convert.ToDecimal(lblHour15.Text);
                H16 += Convert.ToDecimal(lblHour16.Text);
                H17 += Convert.ToDecimal(lblHour17.Text);
                H18 += Convert.ToDecimal(lblHour18.Text);
                H19 += Convert.ToDecimal(lblHour19.Text);
                H20 += Convert.ToDecimal(lblHour20.Text);
                H21 += Convert.ToDecimal(lblHour21.Text);
                H22 += Convert.ToDecimal(lblHour22.Text);
                H23 += Convert.ToDecimal(lblHour23.Text);
                TAmount += Convert.ToDecimal(lbloTotal.Text);


                string totalofDAY = Convert.ToString(Convert.ToDecimal(lblHour0.Text) + Convert.ToDecimal(lblHour1.Text) +
                 Convert.ToDecimal(lblHour2.Text) +
                  Convert.ToDecimal(lblHour3.Text) +
                Convert.ToDecimal(lblHour4.Text) +
                 Convert.ToDecimal(lblHour5.Text) +
                 Convert.ToDecimal(lblHour6.Text) +
                 Convert.ToDecimal(lblHour7.Text) +
                 Convert.ToDecimal(lblHour8.Text) +
                Convert.ToDecimal(lblHour9.Text) +
                 Convert.ToDecimal(lblHour10.Text) +
                  Convert.ToDecimal(lblHour11.Text) +
                 Convert.ToDecimal(lblHour12.Text) +
                 Convert.ToDecimal(lblHour13.Text) +
                 Convert.ToDecimal(lblHour14.Text) +
                  Convert.ToDecimal(lblHour15.Text) +
                 Convert.ToDecimal(lblHour16.Text) +
                  Convert.ToDecimal(lblHour17.Text) +
                 Convert.ToDecimal(lblHour18.Text) +
                 Convert.ToDecimal(lblHour19.Text) +
                 Convert.ToDecimal(lblHour20.Text) +
                Convert.ToDecimal(lblHour21.Text) +
                 Convert.ToDecimal(lblHour22.Text) +
                  Convert.ToDecimal(lblHour23.Text));
                if (ddlViewData.SelectedValue == "3")
                {
                    LblHDayTotal.Text = string.Format("{0:N2}", (Convert.ToDecimal(totalofDAY)));
                }
                else
                {
                    LblHDayTotal.Text = string.Format("{0:0}", (Convert.ToDecimal(totalofDAY)));
                }


                GHT += Convert.ToDecimal(totalofDAY);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblFCal0 = (Label)e.Row.FindControl("lblFCal0");
                Label lblFCal1 = (Label)e.Row.FindControl("lblFCal1");
                Label lblFCal2 = (Label)e.Row.FindControl("lblFCal2");
                Label lblFCal3 = (Label)e.Row.FindControl("lblFCal3");
                Label lblFCal4 = (Label)e.Row.FindControl("lblFCal4");
                Label lblFCal5 = (Label)e.Row.FindControl("lblFCal5");
                Label lblFCal6 = (Label)e.Row.FindControl("lblFCal6");
                Label lblFCal7 = (Label)e.Row.FindControl("lblFCal7");
                Label lblFCal8 = (Label)e.Row.FindControl("lblFCal8");
                Label lblFCal9 = (Label)e.Row.FindControl("lblFCal9");
                Label lblFCal10 = (Label)e.Row.FindControl("lblFCal10");
                Label lblFCal11 = (Label)e.Row.FindControl("lblFCal11");
                Label lblFCal12 = (Label)e.Row.FindControl("lblFCal12");
                Label lblFCal13 = (Label)e.Row.FindControl("lblFCal13");
                Label lblFCal14 = (Label)e.Row.FindControl("lblFCal14");
                Label lblFCal15 = (Label)e.Row.FindControl("lblFCal15");
                Label lblFCal16 = (Label)e.Row.FindControl("lblFCal16");
                Label lblFCal17 = (Label)e.Row.FindControl("lblFCal17");
                Label lblFCal18 = (Label)e.Row.FindControl("lblFCal18");
                Label lblFCal19 = (Label)e.Row.FindControl("lblFCal19");
                Label lblFCal20 = (Label)e.Row.FindControl("lblFCal20");
                Label lblFCal21 = (Label)e.Row.FindControl("lblFCal21");
                Label lblFCal22 = (Label)e.Row.FindControl("lblFCal22");
                Label lblFCal23 = (Label)e.Row.FindControl("lblFCal23");
                Label lblFDayTotal = (Label)e.Row.FindControl("lblFDayTotal");
                Label lblFOAllOrdercount = (Label)e.Row.FindControl("lblFOAllOrdercount");

                if (ddlViewData.SelectedValue == "3")
                {
                    lblFCal0.Text = string.Format("{0:N2}", (H0));
                    lblFCal1.Text = string.Format("{0:N2}", (H1));
                    lblFCal2.Text = string.Format("{0:N2}", (H2));
                    lblFCal3.Text = string.Format("{0:N2}", (H3));
                    lblFCal4.Text = string.Format("{0:N2}", (H4));
                    lblFCal5.Text = string.Format("{0:N2}", (H5));
                    lblFCal6.Text = string.Format("{0:N2}", (H6));
                    lblFCal7.Text = string.Format("{0:N2}", (H7));
                    lblFCal8.Text = string.Format("{0:N2}", (H8));
                    lblFCal9.Text = string.Format("{0:N2}", (H9));
                    lblFCal10.Text = string.Format("{0:N2}", (H10));
                    lblFCal11.Text = string.Format("{0:N2}", (H11));
                    lblFCal12.Text = string.Format("{0:N2}", (H12));
                    lblFCal13.Text = string.Format("{0:N2}", (H13));
                    lblFCal14.Text = string.Format("{0:N2}", (H14));
                    lblFCal15.Text = string.Format("{0:N2}", (H15));
                    lblFCal16.Text = string.Format("{0:N2}", (H16));
                    lblFCal17.Text = string.Format("{0:N2}", (H17));
                    lblFCal18.Text = string.Format("{0:N2}", (H18));
                    lblFCal19.Text = string.Format("{0:N2}", (H19));
                    lblFCal20.Text = string.Format("{0:N2}", (H20));
                    lblFCal21.Text = string.Format("{0:N2}", (H21));
                    lblFCal22.Text = string.Format("{0:N2}", (H22));
                    lblFCal23.Text = string.Format("{0:N2}", (H23));
                    lblFDayTotal.Text = string.Format("{0:N2}", (GHT));

                }
                else
                {
                    lblFCal0.Text = string.Format("{0:0}", (H0));
                    lblFCal1.Text = string.Format("{0:0}", (H1));
                    lblFCal2.Text = string.Format("{0:0}", (H2));
                    lblFCal3.Text = string.Format("{0:0}", (H3));
                    lblFCal4.Text = string.Format("{0:0}", (H4));
                    lblFCal5.Text = string.Format("{0:0}", (H5));
                    lblFCal6.Text = string.Format("{0:0}", (H6));
                    lblFCal7.Text = string.Format("{0:0}", (H7));
                    lblFCal8.Text = string.Format("{0:0}", (H8));
                    lblFCal9.Text = string.Format("{0:0}", (H9));
                    lblFCal10.Text = string.Format("{0:0}", (H10));
                    lblFCal11.Text = string.Format("{0:0}", (H11));
                    lblFCal12.Text = string.Format("{0:0}", (H12));
                    lblFCal13.Text = string.Format("{0:0}", (H13));
                    lblFCal14.Text = string.Format("{0:0}", (H14));
                    lblFCal15.Text = string.Format("{0:0}", (H15));
                    lblFCal16.Text = string.Format("{0:0}", (H16));
                    lblFCal17.Text = string.Format("{0:0}", (H17));
                    lblFCal18.Text = string.Format("{0:0}", (H18));
                    lblFCal19.Text = string.Format("{0:0}", (H19));
                    lblFCal20.Text = string.Format("{0:0}", (H20));
                    lblFCal21.Text = string.Format("{0:0}", (H21));
                    lblFCal22.Text = string.Format("{0:0}", (H22));
                    lblFCal23.Text = string.Format("{0:0}", (H23));
                    lblFDayTotal.Text = string.Format("{0:0}", (GHT));
                }
            }
        }



        #endregion

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SALEORDER.DTO;
using Microsoft.Reporting.WebForms;

namespace KMISRWebApplication.Webcontent.Report
{
    
    public partial class REPS00045 : System.Web.UI.Page
    {
        string dateFreeForm, dateFreeTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                dateFreeForm = Request.QueryString["dateFreeForm"];
                dateFreeTo = Request.QueryString["dateFreeTo"];
                dateFreeForm = Request.QueryString["dateFreeForm"] == null ? "" : Request.QueryString["dateFreeForm"].ToString();
                dateFreeTo = Request.QueryString["dateFreeTo"] == null ? "" : Request.QueryString["dateFreeTo"].ToString();
                LoadDataPDF();
            }
        }
      
        private void LoadDataPDF()
        {
            try
            {
                MediaOnAirInfo com = new MediaOnAirInfo();
                DataTable DataSet1 = new DataTable();
                DataSet1.Columns.Add("Line_No");
                DataSet1.Columns.Add("Date");
                DataSet1.Columns.Add("Time");
                DataSet1.Columns.Add("Program_Name");
                DataSet1.Columns.Add("CHANNEL");
                DataSet1.Columns.Add("MEDIA_TYPE");
                DataSet1.Columns.Add("SHOW_TYPE");
                DataSet1.Columns.Add("DURATION");
                DataSet1.Columns.Add("MEDIA_PHONE");
                DataSet1.Columns.Add("CAMPAIGN_PRODUCT_NO");
                DataSet1.Columns.Add("CAMPAIGN_NAME");
                DataSet1.Columns.Add("CAMPAIGN_VERSION");
                DataSet1.Columns.Add("PLANNER_CODE");
                DataSet1.Columns.Add("PLANNER_NAME");
                DataSet1.Columns.Add("CPM");
          


                for (int i= 0;i < 100; i++)
                {
                    DataRow row = DataSet1.NewRow();
                    row["Line_No"] = i;
                    row["Date"] = "01/07/2011";
                    row["Time"] = "00.00";
                    row["Program_Name"] = "DIRECT 2 HOME";
                    row["CHANNEL"] = "MEDIA NEWS ";
                    row["MEDIA_TYPE"] = "SATELLITE";
                    row["SHOW_TYPE"] = "SPOT";
                    row["DURATION"] = "5";
                    row["MEDIA_PHONE"] = "02.666.0799";
                    row["CAMPAIGN_PRODUCT_NO"] = "0121300300567";
                    row["CAMPAIGN_NAME"] = "DIRT BULLET";
                    row["CAMPAIGN_VERSION"] = "V.1";
                    row["PLANNER_CODE"] = "MESAYA_J";
                    row["PLANNER_NAME"] = "MESAYA JOYRUNG";
                    row["CPM"] = "96.62";

                    DataSet1.Rows.Add(row);
                }



                
                if (DataSet1.Rows.Count>0)
                {
                    
                    if (DataSet1.Rows.Count > 0)
                    {
                        ReportParameter DateFrom = new ReportParameter("DateFrom", dateFreeForm);
                        ReportParameter DateTo = new ReportParameter("DateTo", dateFreeTo);
                        ReportParameter MediaName = new ReportParameter("MediaName", "ALL");
                        DataTable dts = DataSet1;
                        ReportDataSource datasource = new ReportDataSource("DataSet1", dts);

                        REP00045.LocalReport.DataSources.Clear();
                        REP00045.LocalReport.DataSources.Add(datasource);
                        

                        REP00045.LocalReport.ReportPath = @"ReportFile\Report\Report1.rdlc";
                        REP00045.LocalReport.SetParameters(DateTo);
                        REP00045.LocalReport.SetParameters(DateFrom);
                        REP00045.LocalReport.SetParameters(MediaName);
                        
                        REP00045.LocalReport.Refresh();

                    }
                    else 
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alertxxx", "alert('ไม่มีข้อมูล')", true);
                        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);


                    }
            
               
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "loadreport", "ShowMessage();", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Report 45 Error :" + ex.Message);
            }

        }
        private void ShowReports(ReportDataSource drsource)
        {

            REP00045.LocalReport.DataSources.Clear();
            REP00045.LocalReport.DataSources.Add(drsource);
            

            REP00045.LocalReport.ReportPath = @"REP00045.rdlc";
         

          
        }

    }
}
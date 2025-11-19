using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using SALEORDER.Common;


namespace DOMS_TSR.src.Report
{
    public partial class REP00045 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
            if (!IsPostBack)
            {
                InitPage();
            }
        }
        private void InitPage()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
        private void LoadData()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Response.Write("Report 45 Error :" + ex.Message);
            }

        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            


        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            InitPage();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnSearch_Click_NoAnswerOrder(object sender, EventArgs e)
        {
            
            string URL = (@"REPS00045.aspx?dateFreeForm=" + txtSearchOrderDateFrom_NoAnswerOrder.Text + "&dateFreeTo=" + txtSearchOrderDateUntil_NoAnswerOrder.Text);

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + URL + "','_blank')", true);
        }

        protected void btnClearSearch_Click_NoAnswerOrder(object sender, EventArgs e)
        {
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string dateformat = "yyyy-MM-dd";
            
        }
        private void LoadDataPDF()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Response.Write("Report 45 Error :" + ex.Message);
            }

        }

        public string Text_Textbox(TextBox recieve_param)
        {
            string result = "";
            result = (recieve_param.Text == "") ? "" : recieve_param.Text;
            return result;
        }

        public string Text_selectedVal(DropDownList recieve_param)
        {
            string result = "";
            result = (recieve_param.SelectedValue.ToString() == "") ? "" : recieve_param.SelectedValue.ToString();
            return result;
        }



        protected void Button2_Click1(object sender, EventArgs e)
        {
            string dateformat = "yyyy-MM-dd";
            
        }
    }
}
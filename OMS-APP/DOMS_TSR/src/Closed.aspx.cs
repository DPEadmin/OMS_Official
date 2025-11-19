using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;

namespace DOMS_TSR.src
{
    public partial class Closed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowMessageBox();
           
            }
              
        }
        private void ShowMessageBox()
        {
            string sJavaScript = "<script language=javascript>\n";
            sJavaScript += "var agree;\n";
            sJavaScript += "agree = confirm('กด ตกลง เพื่อเข้าสู่ระบบ');\n";
            sJavaScript += "if(agree)\n";
            sJavaScript += "window.location = \"../../Default.aspx\";\n";
            sJavaScript += "</script>";
            Response.Write(sJavaScript);
        }
    }
}
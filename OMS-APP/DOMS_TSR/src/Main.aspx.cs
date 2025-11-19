using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using SALEORDER.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Configuration;
namespace DOMS_TSR.src
{
    public partial class Main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            EmpInfo empInfo = new EmpInfo();

            empInfo = (EmpInfo)Session["EmpInfo"];

            if (empInfo != null)
            {
                string empcode = empInfo.EmpCode;
            }
            else
            {
                Response.Redirect("..\\..\\Default.aspx?flaglogin=_EMPCODENULL");
            }

        }
    }
}
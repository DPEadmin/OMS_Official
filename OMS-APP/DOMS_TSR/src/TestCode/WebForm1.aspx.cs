using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DOMS_TSR.src.TestCode
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            double lat1 = 12.916933d,
          lon1 = 77.562658d,
          lat2 = 12.930140d,
          lon2 = 77.587732d;
            double dist = GetDistanceFromLatLonInKm(lat1, lon1, lat2, lon2);
            TextBox1.Text = dist.ToString();
        }
        double GetDistanceFromLatLonInKm(double lat1,
                                 double lon1,
                                 double lat2,
                                 double lon2)
        {
            var R = 6371d; // Radius of the earth in km
            var dLat = Deg2Rad(lat2 - lat1);  // deg2rad below
            var dLon = Deg2Rad(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2d) * Math.Sin(dLat / 2d) +
              Math.Cos(Deg2Rad(lat1)) * Math.Cos(Deg2Rad(lat2)) *
              Math.Sin(dLon / 2d) * Math.Sin(dLon / 2d);
            var c = 2d * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1d - a));
            var d = R * c; // Distance in km
            return d;
        }

        double Deg2Rad(double deg)
        {

            return deg * (Math.PI / 180d);
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<myChatHub>();

            hubContext.Clients.All.broadcasttest("Hello World");
        }

        public void CreateSound()
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script language='javascript'>");
            sb.Append(@"EvalSound('audio1');");
            sb.Append(@"</script>");
            System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "JCall1", sb.ToString(), false);

        }

        protected void testsound_Click(object sender, EventArgs e)
        {
            CreateSound();

        }
    }
}
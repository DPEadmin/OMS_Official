using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
namespace DOMS_TSR
{
    public partial class testmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SendMail();
        }
        protected void SendMail()
        {
         string SMTP_SERVER = "smtp.gmail.com";
        string SMTP_PORT = "587";
        int Port = Int32.Parse(SMTP_PORT);
            string mailFrom = "achawin@gmail.com";
            string mailTo = "ocelot032@gmail.com";
            MailMessage objMailMessage = new MailMessage();
            System.Net.NetworkCredential objSMTPUserInfo = new System.Net.NetworkCredential();
            SmtpClient objSmtpClient = new SmtpClient();

            try
            {
                objMailMessage.From = new MailAddress(mailFrom);
                string[] mailTos = mailTo.Split(';');
                foreach (string s in mailTos)
                {
                    if (s != "")
                    {
                        objMailMessage.To.Add(s);

                    }
                }
                
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Subject = "ทดสอบ";
                objMailMessage.Body = "ทดสอบส่งเมล";
                objSmtpClient = new SmtpClient(SMTP_SERVER, Port); /// Server IP
                objSMTPUserInfo = new System.Net.NetworkCredential("palmmylittle@gmail.com", "s4635735", "palmmylittle.gmail.com");
                objSmtpClient.EnableSsl = true;
                objSmtpClient.Credentials = objSMTPUserInfo;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
   
                objSmtpClient.Send(objMailMessage);
                

            }
            catch (Exception ex)
            { }

            finally
            {
                objMailMessage = null;
                objSMTPUserInfo = null;
                objSmtpClient = null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;
using System.Text;
using System.Configuration;
using SALEORDER.DTO;
using SALEORDER.Common;
using System.Net;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Newtonsoft.Json;

namespace WebApplication1.FullfilllManagement.Orderlist
{
    /// <summary>
    /// Summary description for Orderlist
    /// </summary>
    public class Orderlist : IHttpHandler
    {
        string APIpath = "";
        string OL;
        protected static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];


        public void ProcessRequest(HttpContext Tag)
        {
           
            if (!(FontFactory.FontImp is iDevFont))
            {
                FontFactory.FontImp = new iDevFont();
            }

         
            string Edate = Tag.Request["date"];
            string ConfirmNo = Tag.Request["ConfirmNo"];
      
            

            Tag.Response.ContentType = "application/pdf";

            Tag.Response.ContentType = "application/pdf";
            Tag.Response.Cache.SetCacheability(HttpCacheability.NoCache);



            Document pdfDoc = new Document(PageSize.A4.Rotate(), 30f, 30f, 30f, 30f);


            PdfWriter Writer = PdfWriter.GetInstance(pdfDoc, Tag.Response.OutputStream);

            HTMLWorkerExtended htmlparser = new HTMLWorkerExtended(pdfDoc); // verwrite class from PrintPDFpl

            pdfDoc.Open();
            htmlparser.StartDocument();
         

            DataTable dt = new DataTable();
       
             DataTable lorderdetailInfo = new DataTable();
            Util aaa = new Util();
            string respstr = "";
            APIpath = APIUrl + "/api/support/OList";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                data["OrderListDate"] = Edate;

           

                var response = wb.UploadValues(APIpath, "POST", data);

                respstr = Encoding.UTF8.GetString(response);
            }

            List<OrderOLInfo> lOrder = JsonConvert.DeserializeObject<List<OrderOLInfo>>(respstr);

            
            dt = ConvertToDataTable(lOrder);
            


            //DataView view;
            DataTable distinctTable;

            string shipmenttype = "";
            




            StringBuilder Sb = new StringBuilder();

            #region header
            pdfDoc.NewPage();
            DataView viewround;
            DataTable RoundTable;
            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "confirmno");
         

            int i = 0;
            foreach (DataRow DetailRound in RoundTable.Rows)
            {
                bool check_last = (RoundTable.Rows.Count == i + 1) ? check_last = true : check_last = false;
                DataView ViewRound = new DataView(dt);
                ViewRound.RowFilter = string.Format("confirmno ='" + DetailRound["confirmno"].ToString() + "' ");
               distinctTable = ViewRound.ToTable(true, "OrderCode");
                Sb.AppendLine("<div>");
                Sb.AppendLine("<table style=\"width: 100%;\">");

                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"right\">");
                Sb.AppendLine(string.Format("{0}", OL));
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");

                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"center\">");
                Sb.AppendLine("ใบสรุป Order List");
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");

                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"center\">");
                //Lhoang อาจจะมีการเปลี่ยนแปลงแก้ไขภายหลัง
                Sb.AppendLine(shipmenttype);
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");

                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"right\" class=\"style1\">");
                Sb.AppendLine(string.Format("วันที่นัดส่งสินค้า {0} รอบที่{1}", Edate, DetailRound["confirmno"].ToString()));
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");


                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"left\">");
                Sb.AppendLine(string.Format("{0}", "คลังหลัก"));
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");
                Sb.AppendLine("</table>");
                Sb.AppendLine("</div>");
                #endregion

                #region body

                Sb.AppendLine("<div>");
                Sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0.5\" style=\"border-color: #000000; width: 100%; height: 100%; font-size: 12;\"  align=\"center\">");
                Sb.AppendLine("<tr>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"35\">NO</th>");
                
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"75\">ชำระ</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"150\">เลขที่</th>");
                
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"95\">วันที่</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"130\">ชื่อ-สกุล</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"130\">รหัสสินค้า</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"480\">รายละเอียดสินค้า</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"135\">จน.</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 12px; font-weight: 700;\" valign=\"top\" width=\"135\">หน่วย</th>");
                Sb.AppendLine("</tr>");



                decimal GrandTotalAmount = 0;
                decimal GrandTotalAmountQTY = 0;
                int OrderCount = 1;
                foreach (DataRow orderinfo in distinctTable.Rows)
                {
                    DataView ViewOrderinf_id = new DataView(dt);
                    ViewOrderinf_id.RowFilter = string.Format("OrderCode ='{0}'", orderinfo["OrderCode"].ToString());

                    DataTable Orderinfo = ViewOrderinf_id.ToTable();
               

                    int NumRow = 1;
                    Decimal ProductTotalAmount = 0;
                    decimal ProductTotalAmountQty = 0;

                    foreach (DataRow Detail in Orderinfo.Rows)
                    {
                        if (NumRow <= 1)
                        {
                            Sb.AppendLine("<tr>");
                            Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 12px\" rowspan=\"{0}\">{1}</td>", Orderinfo.Rows.Count, OrderCount));
                            
                            Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 10px\" rowspan=\"{0}\">{1}</td>", Orderinfo.Rows.Count, Detail["PaymentTypeName"].ToString()));
                            Sb.AppendLine(string.Format("<td align=\"left\"   valign=\"top\" style=\"font-size: 10px\" rowspan=\"{0}\">{1}</td>", Orderinfo.Rows.Count, Detail["OrderCode"].ToString()));
                            
                            Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 10px\" rowspan=\"{0}\">{1}</td>", Orderinfo.Rows.Count, Detail["CreateDate"].ToString()));
                            Sb.AppendLine(string.Format("<td align=\"left\" valign=\"top\" style=\"font-size: 10px\" rowspan=\"{0}\">{1}</td>", Orderinfo.Rows.Count, Detail["CustomerFName"].ToString()+"  " + Detail["CustomerLName"].ToString()));
                            Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 10px\">{0}</td>", Detail["productcode"].ToString() + "&nbsp;"));
                            Sb.AppendLine(string.Format("<td align=\"left\"  style=\"font-size: 10px\"; vertical-align:top\"> {0}</td>", NumRow + ") " + Detail["PRODUCTNAME"].ToString()));

                            Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 10px\">{0}</td>", Detail["Amount"].ToString() + "&nbsp;"));
                            Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 10px\">{0}</td>", Detail["UnitName"].ToString() + "&nbsp;"));
                            Sb.AppendLine("</tr>");

                        }
                        else
                        {
                            Sb.AppendLine("<tr>");
                            Sb.AppendLine("<tr>");
                            Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 10px\">{0}</td>", Detail["productcode"].ToString() + "&nbsp;"));
                            if (Detail["PRODUCTNAME"].ToString().Length > 35)
                            {

                                Sb.AppendLine(string.Format("<td align=\"left\"  style=\"font-size: 10px\"; vertical-align:top\"> {0}</td>", NumRow + ") " + Detail["PRODUCTNAME"].ToString().Substring(0, 20)));
                            }
                            else
                            {

                                Sb.AppendLine(string.Format("<td align=\"left\"  style=\"font-size: 10px\"; vertical-align:top\"> {0}</td>", NumRow + ") " + Detail["PRODUCTNAME"].ToString()));
                            }



                            Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 10px\">{0}</td>", Detail["Amount"].ToString() + "&nbsp;"));
                            Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 10px\">{0}</td>", Detail["UnitName"].ToString() + "&nbsp;"));


                            Sb.AppendLine("</tr>");
                        }
                        ProductTotalAmount = ProductTotalAmount + (Convert.ToDecimal(Detail["NetPrice"]));
                        ProductTotalAmountQty = ProductTotalAmountQty + (Convert.ToDecimal(Detail["AMOUNT"]));
                        NumRow++;
                        
                    }
                    Sb.AppendLine("<tr>");
                    Sb.AppendLine("<td align=\"center\" valign=\"top\" style=\"font-size: 14; font-weight: 500\" colspan=\"6\">รวม</td>");
                    Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 12; font-weight: 500\"></td>&nbsp;"));
                    Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 12; font-weight: 200\" colspan=\"2\">{0}</td>", ProductTotalAmountQty + "&nbsp;"));

                    Sb.AppendLine("</tr>");
                    GrandTotalAmount = GrandTotalAmount + ProductTotalAmount;
                    GrandTotalAmountQTY = GrandTotalAmountQTY + ProductTotalAmountQty;

                    OrderCount++;
                }

                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"center\" valign=\"top\" style=\"font-size: 14; font-weight: 700\" colspan=\"7\">รวมทั้งหมด</td>");
                Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 12; font-weight: 700\">{0}</td>", GrandTotalAmountQTY + "&nbsp;"));
                Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 12; font-weight: 700\">{0}</td>", GrandTotalAmount + "&nbsp;"));
                Sb.AppendLine("</tr>");
                Sb.AppendLine("</table>");
                Sb.AppendLine("</div>");

                if (check_last)
                { Sb.Append(""); }
                else
                { Sb.Append("<newpage/>"); }
                i++;

            }
            #endregion

           
            #region footer
            StringReader Reader = new StringReader(Sb.ToString());
            htmlparser.Parse(Reader);

            Font contentFont = FontFactory.GetFont(@"C:\WINDOWS\Fonts\tahoma.ttf", 10);

            Paragraph Sign = new Paragraph("....................................    ...................................", contentFont);
            Sign.Alignment = Element.ALIGN_RIGHT;

            PdfPCell cellSign = new PdfPCell(Sign);
            cellSign.Border = 0;
            cellSign.PaddingLeft = 10;

            Paragraph SignDate = new Paragraph("วันที่........./........./.........    วันที่........./........./.........", contentFont);
            SignDate.Alignment = Element.ALIGN_RIGHT;

            PdfPCell cellSignDate = new PdfPCell(SignDate);
            cellSignDate.Border = 0;
            cellSignDate.PaddingLeft = 10;


            Paragraph WarehouseAndShipment = new Paragraph("            (คลังสินค้า)                          (ขนส่ง)", contentFont);
            WarehouseAndShipment.Alignment = Element.ALIGN_RIGHT;


            PdfPCell cellWarehouseAndShipment = new PdfPCell(WarehouseAndShipment);
            cellWarehouseAndShipment.Border = 0;
            cellWarehouseAndShipment.PaddingLeft = 10;




            PdfPTable footerTbl = new PdfPTable(1);
            footerTbl.TotalWidth = 300;
            footerTbl.HorizontalAlignment = Element.ALIGN_CENTER;


            footerTbl.AddCell(cellSign);
            footerTbl.AddCell(cellSignDate);
            footerTbl.AddCell(cellWarehouseAndShipment);

            
            footerTbl.WriteSelectedRows(0, -1, 570, 60, Writer.DirectContent);
            htmlparser.EndDocument();

            #endregion

            htmlparser.Close();
            pdfDoc.Close();

            Tag.Response.Write(pdfDoc);
            Tag.Response.Flush();
            Tag.Response.End();
        }


        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
  
    public class iDevFont : iTextSharp.text.FontFactoryImp
    {
        iTextSharp.text.Font defaultFont;
        public iDevFont()
        {

            BaseFont tahoma = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\tahoma.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            
            defaultFont = new iTextSharp.text.Font(tahoma);

        }
        public override iTextSharp.text.Font GetFont(string fontname, string encoding, Boolean embedded, float size, int style, BaseColor color, Boolean cached)
        {

            BaseFont tahoma = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\tahoma.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            return new iTextSharp.text.Font(tahoma, size, style, color);
        }
    }

    public class HTMLWorkerExtended : HTMLWorker
    {
        public HTMLWorkerExtended(IDocListener document)
            : base(document)
        {

        }
        public override void StartElement(string tag, IDictionary<string, string> str)
        {
            if (tag.Equals("newpage"))
                document.Add(Chunk.NEXTPAGE);
            else
                base.StartElement(tag, str);
        }

    }
}
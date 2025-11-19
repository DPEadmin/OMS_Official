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

using SALEORDER.DTO;
using SALEORDER.Common;

namespace WebApplication1.FullfilllManagement.Orderlist
{
    /// <summary>
    /// Summary description for PLList
    /// </summary>
    public class PLList : IHttpHandler
    
    {
        string PLNO = "123456789";

        public void ProcessRequest(HttpContext Tag)
        {
            if (!(FontFactory.FontImp is iDevFont))
            {
                FontFactory.FontImp = new iDevFont();
            }
            string Date = Tag.Request["date"];
            string ConfirmNo = Tag.Request["ConfirmNo"];


            Tag.Response.ContentType = "application/pdf";

            
            Tag.Response.ContentEncoding = System.Text.Encoding.UTF8;
            Tag.Response.Cache.SetCacheability(HttpCacheability.NoCache);



            Document pdfDoc = new Document(PageSize.A4, 30f, 30f, 30f, 30f);


            PdfWriter Writer = PdfWriter.GetInstance(pdfDoc, Tag.Response.OutputStream);

            HTMLWorkerExtended htmlparser = new HTMLWorkerExtended(pdfDoc);

            pdfDoc.Open();
            htmlparser.StartDocument();


            DataTable dt = new DataTable();
            DataTable dtRound = new DataTable();
         
            DataTable lorderdetailInfo = new DataTable();
        

            dt = lorderdetailInfo;






            StringBuilder Sb = new StringBuilder();
            DataView viewround;
            DataTable RoundTable;

            viewround = new DataView(dt);
            RoundTable = viewround.ToTable(true, "confirmno");
            #region header

            

            int i = 0;
            foreach (DataRow DetailRound in RoundTable.Rows)
            {
                //Header PDF
                bool check_last = (RoundTable.Rows.Count == i + 1) ? check_last = true : check_last = false;
                Sb.AppendLine("<div>");
                Sb.AppendLine("<table style=\"width: 100%;\">");

                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"right\">");
                Sb.AppendLine(string.Format("{0} - {1}", PLNO, "Center"));
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");

                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"center\">");
                Sb.AppendLine("ใบสรุป Picking List");
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");


                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"center\" style=\"width: 50%;\">");
                //Lhoang อาจจะมีการเปลี่ยนแปลงแก้ไขภายหลัง
                Sb.AppendLine(string.Format("{0}", "DOMS"));
                Sb.AppendLine("</td>");
                Sb.AppendLine("<td align=\"right\" style=\"width: 50%;\">");
                Sb.AppendLine(string.Format("วันที่นัดส่งสินค้า {0} รอบที่ {1}", Date, DetailRound["confirmno"].ToString()));
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");


                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"left\">");
                Sb.AppendLine(string.Format("{0} {1}", "รอบที่", DetailRound["confirmno"].ToString()));
                Sb.AppendLine("</td>");
                Sb.AppendLine("</tr>");

                Sb.AppendLine("</table>");
                Sb.AppendLine("</div>");
                #endregion

                #region body

                int GrandTotalQuantity = 0;
                decimal GrandTotalAmount = 0;


                Sb.AppendLine("<div>");

                int NumProduct = 1;
                int ProductTotalQuanTity = 0;
                Decimal ProductTotalAmount = 0;
                string aaaa = DetailRound["confirmno"].ToString();
                // รายการปกติ
                DataView ViewWarehouseRouteTypeID = new DataView(dt);
                ViewWarehouseRouteTypeID.RowFilter = string.Format("ProductCode <> '9999999' and confirmno ='" + DetailRound["confirmno"].ToString() + "'");
                DataTable DtproViewWarehouseRouteTypeID;
                DtproViewWarehouseRouteTypeID = ViewWarehouseRouteTypeID.ToTable();
                // รายกาค่าขนส่ง
                DataView ViewWarehouseRouteTypeID2 = new DataView(dt);
                ViewWarehouseRouteTypeID2.RowFilter = string.Format("ProductCode = '9999999' and confirmno ='" + DetailRound["confirmno"].ToString() + "'");
                DataTable DTViewWarehouseRouteTypeID2 = ViewWarehouseRouteTypeID2.ToTable();
                DataView ViewItemId = ViewWarehouseRouteTypeID;
                DataTable ProductList = ViewItemId.ToTable();

                Sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0.5\" style=\"border-color: #000000; width: 100%; height: 100%; font-size: 10;\"  align=\"center\">");
                Sb.AppendLine("<tr>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"40\">NO</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"150\">รหัสสินค้า</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"400\">ชื่อสินค้า</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"50\">จน.</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"160\">ราคารวม</th>");
                Sb.AppendLine("</tr>");

                foreach (DataRow Detail in ProductList.Rows)
                {
                    
                    Decimal Proprice;
                    if (Detail["SUM_AMOUNT"] != null && Detail["SUM_AMOUNT"].ToString() != "")
                    {
                        Proprice = decimal.Parse(Detail["SUM_AMOUNT"].ToString());
                    }
                    else
                    {
                        Proprice = 0;
                    }
                    Sb.AppendLine("<tr>");
                        Sb.AppendLine(string.Format("<td align=\"center\" style=\"font-size: 8px; valign=\"top\">{0}</td>", NumProduct));
                        Sb.AppendLine(string.Format("<td align=\"center\" style=\"font-size: 8px; valign=\"top\">{0}</td>", Detail["ProductCode"]).ToString());
                        Sb.AppendLine(string.Format("<td align=\"left\" style=\"font-size: 8px; valign=\"top\">{0}</td>&nbsp;&nbsp;&nbsp;&nbsp", Detail["ProductName"]).ToString());
                        Sb.AppendLine(string.Format("<td align=\"center\" style=\"font-size: 8px; valign=\"top\">{0}</td>", Detail["SUM_QUANTITY"]).ToString());
                        Sb.AppendLine(string.Format("<td align=\"right\" style=\"font-size: 8px; valign=\"top\">{0}</td>", Util.DecimalToString(Proprice, "#,##0.#0") + "&nbsp;"));
                        Sb.AppendLine("</tr>");
                        NumProduct++;
                        ProductTotalQuanTity = ProductTotalQuanTity + (Convert.ToInt32(Detail["SUM_QUANTITY"]));
                        ProductTotalAmount = ProductTotalAmount + (Convert.ToDecimal(Proprice));
                    
                }
                GrandTotalQuantity = GrandTotalQuantity + ProductTotalQuanTity;
                GrandTotalAmount = GrandTotalAmount + ProductTotalAmount;


                Sb.AppendLine("<tr>");
                Sb.AppendLine("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700\" colspan=\"3\">รวม</td>");
                Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700\">{0}</td>", ProductTotalQuanTity.ToString()));
                Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 9px; font-weight: 700\">{0}</td>", Util.DecimalToString(ProductTotalAmount, "#,##0.#0") + "&nbsp;"));
                Sb.AppendLine("</tr>");
                Sb.AppendLine("</table>");
                Sb.AppendLine("<br />");
                ProductTotalQuanTity = 0;
                ProductTotalAmount = 0;
                              

                //รายการเงินมัดจำ
                DataView ViewWarehouseRouteTypeID3 = new DataView(dt);
                ViewWarehouseRouteTypeID3.RowFilter = string.Format("ProductCode ='50216510' and confirmno ='" + DetailRound["confirmno"].ToString() + "'");
                DataTable DTViewWarehouseRouteTypeID3 = ViewWarehouseRouteTypeID3.ToTable();
                
                DataTable ItemIdDistinct = ViewWarehouseRouteTypeID3.ToTable(true);
                if (ItemIdDistinct.Rows.Count == 0)
                {
                    ItemIdDistinct = ViewWarehouseRouteTypeID3.ToTable(true);
                }
                DataView ViewItemIdMoney = ViewWarehouseRouteTypeID3;
                DataTable ProductListMoney = ViewItemIdMoney.ToTable();
                int NumProductMoney = 1;
                int ProductTotalQuanTityMoney = 0;
                Decimal ProductTotalAmountMoney = 0;

                

                int GrandTotalQuantityTran = 0;
                decimal GrandTotalAmountTran = 0;

                int RowsCount = 0;

                
                

                //รายงานค่าขนส่ง
                DataView ViewItemIdTran = ViewWarehouseRouteTypeID2;
                DataTable ProductListTran = ViewItemIdTran.ToTable();
                int NumProductTran = 1;
                int ProductTotalQuanTityTran = 0;
                Decimal ProductTotalAmountTran = 0;

                Sb.AppendLine("<table cellpadding=\"0\" cellspacing=\"0\" border=\"0.5\" style=\"border-color: #000000; width: 100%; height: 100%; font-size: 12;\"  align=\"center\">");
                Sb.AppendLine("<tr>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"40\">NO</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"150\">รหัสสินค้า</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"400\">ชื่อสินค้า</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"50\">จำนวน</th>");
                Sb.AppendLine("<th align=\"center\" style=\"font-size: 10px; font-weight: 700;\" valign=\"top\" width=\"160\">ราคารวม</th>");
                Sb.AppendLine("</tr>");

                foreach (DataRow DetailTran in ProductListTran.Rows)
                {
                    Sb.AppendLine("<tr>");
                    Sb.AppendLine(string.Format("<td align=\"center\" style=\"font-size: 8px;\"valign=\"top\"> {0}</td>", NumProductTran));
                    Sb.AppendLine(string.Format("<td align=\"center\" style=\"font-size: 8px;\"valign=\"top\"> {0}</td>", DetailTran["ProductCode"]).ToString());
                    Sb.AppendLine(string.Format("<td align=\"left\" style=\"font-size: 8px;\"valign=\"top\"> {0}</td>&nbsp;&nbsp;&nbsp;", DetailTran["ProductName"]).ToString());
                    Sb.AppendLine(string.Format("<td align=\"center\" style=\"font-size: 8px;\"valign=\"top\"> {0}</td>", DetailTran["SUM_QUANTITY"]).ToString());
                    Decimal sumamout;
                    if (DetailTran["SUM_AMOUNT"] != null && DetailTran["SUM_AMOUNT"].ToString()!="")
                    {
                        sumamout =decimal.Parse( DetailTran["SUM_AMOUNT"].ToString());
                    }
                    else
                    {
                        sumamout = 0;
                    }
                    

                    Sb.AppendLine(string.Format("<td align=\"right\" style=\"font-size: 8px; margin-right:2px;\"valign=\"top;\" >{0} </td>", Util.DecimalToString(sumamout, "#,##0.#0") + "&nbsp;"));
                    Sb.AppendLine("</tr>");

                    NumProductTran++;
                    ProductTotalQuanTityTran = ProductTotalQuanTityTran + (Convert.ToInt32(DetailTran["SUM_QUANTITY"]));
                    ProductTotalAmountTran = ProductTotalAmountTran + (sumamout);


                }

                GrandTotalQuantityTran = ProductTotalQuanTityTran + ProductTotalQuanTity + ProductTotalQuanTityMoney;
                GrandTotalAmountTran = GrandTotalAmount + ProductTotalAmountTran + ProductTotalAmountMoney;

                if (RowsCount < ItemIdDistinct.Rows.Count)
                {
                    Sb.AppendLine("<tr>");
                    Sb.AppendLine("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700;\" colspan=\"3\">รวม</td>");
                    Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700;\">{0}</td>", ProductTotalQuanTityTran.ToString()));
                    Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 9px; font-weight: 700;\">{0} </td>", Util.DecimalToString(ProductTotalAmountTran, "#,##0.#0") + "&nbsp;"));
                    Sb.AppendLine("</tr>");
                    Sb.AppendLine("</table>");
                    Sb.AppendLine("<br />");
                }
                else
                {
                    Sb.AppendLine("<tr>");
                    Sb.AppendLine("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700\" colspan=\"3\">รวม</td>");
                    Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700;\">{0}</td>", ProductTotalQuanTityTran.ToString()));
                    Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 9px; font-weight: 700;\">{0} </td>", Util.DecimalToString(ProductTotalAmountTran, "#,##0.#0") + "&nbsp;"));
                    Sb.AppendLine("</tr>");

                    Sb.AppendLine("<tr>");
                    Sb.AppendLine("<td colspan=\"5\"></td>");
                    Sb.AppendLine("</tr>");

                    //GrandTotal
                    Sb.AppendLine("<tr>");
                    Sb.AppendLine("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700\" colspan=\"3\">รวมทั้งหมด</td>");
                    Sb.AppendLine(string.Format("<td align=\"center\" valign=\"top\" style=\"font-size: 9px; font-weight: 700;\">{0}</td>", GrandTotalQuantityTran.ToString()));
                    Sb.AppendLine(string.Format("<td align=\"right\" valign=\"top\" style=\"font-size: 9px; font-weight: 700;\">{0} </td>", Util.DecimalToString(GrandTotalAmountTran, "#,##0.#0") + "&nbsp;"));
                    Sb.AppendLine("</tr>");
                    Sb.AppendLine("</table>");
                    Sb.AppendLine("<br />");
                    Sb.AppendLine("</div>");
                }
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
            footerTbl.WriteSelectedRows(0, -1, 330, 60, Writer.DirectContent);
            #endregion

            htmlparser.EndDocument();
            htmlparser.Close();
            pdfDoc.Close();

            Tag.Response.Write(pdfDoc);
            Tag.Response.Flush();
            Tag.Response.End();
        }


        public bool IsReusable
        {
            get
            {
                return false;
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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading;
using System.Data;
using System.IO;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data.SqlClient;
using System.ComponentModel;

using System.Configuration;
namespace SALEORDER.Common
{
    public class ExportToExcel
    {

        public void epp_gen(DataTable inbound, string filename)
        {
            ExcelPackage pck = new ExcelPackage();

            int count = 0;
            //var fi = new FileInfo(outputDir.FullName + "\\REP00049.xlsx");
            var wsDt = pck.Workbook.Worksheets.Add(filename);
            //wsDt
            //Add first row first column
            //wsDt.Cells[]
            wsDt.Cells[1, 1].Value = filename;
            wsDt.Cells[1, 1, 1, inbound.Columns.Count].Merge = true;

            //Initvalue.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            var first = inbound.AsEnumerable().CopyToDataTable();
            wsDt.Cells["A2"].LoadFromDataTable(first, true);


            //Border Declaration
            int a = inbound.Rows.Count + 2;
            int b = inbound.Columns.Count;

            wsDt.Cells[1, 1, a, b].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            wsDt.Cells[1, 1, a, b].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            wsDt.Cells[1, 1, a, b].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            wsDt.Cells[1, 1, a, b].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            wsDt.Cells[wsDt.Dimension.Address].AutoFitColumns();

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=" + filename + ".xlsx");
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.BinaryWrite(pck.GetAsByteArray());
            HttpContext.Current.Response.End();
        }
     
        public void LoadData(DataTable inbound, string filename, string sheetname)
        {
            int rowsPerSheet = 50000;
            int c = 0;
            bool firstTime = true;
            var ResultsData = new DataTable();
            ResultsData = inbound.Clone();
            for (int i = 0; i < inbound.Rows.Count; i++)
            {
                ResultsData.ImportRow(inbound.Rows[i]);
                c++;
                if (c == rowsPerSheet)
                {
                    c = 0;
                    ExportToOxml(firstTime, sheetname, filename, ResultsData);
                    ResultsData.Clear();
                    firstTime = false;
                }
            }
            if (ResultsData.Rows.Count > 0)
            {
                ExportToOxml(firstTime, sheetname, filename, ResultsData);
                ResultsData.Clear();
            }
        }
        private void ExportToOxml(bool firstTime, string sheetname, string filename, DataTable ResultsData)
        {
            //Delete the file if it exists.
            if (firstTime && File.Exists(filename))
            {
                File.Delete(filename);
            }

            uint sheetId = 1; //Start at the first sheet in the Excel workbook.
            if (firstTime)
            {
                //This is the first time of creating the excel file and the first sheet.
                // Create a spreadsheet document by supplying the filepath.
                // By default, AutoSave = true, Editable = true, and Type = xlsx.
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filename, SpreadsheetDocumentType.Workbook);

                // Add a WorkbookPart to the document.
                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                // Add a WorksheetPart to the WorkbookPart.
                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                var bold1 = new Bold();
                CellFormat cf = new CellFormat();

                // Add Sheets to the Workbook.
                Sheets sheets;
                sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());

                // Append a new worksheet and associate it with the workbook.
                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = sheetname + sheetId
                };
                sheets.Append(sheet);

                //Add Header Row.
                var headerRow = new Row();
                foreach (DataColumn column in ResultsData.Columns)
                {
                    var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                foreach (DataRow row in ResultsData.Rows)
                {
                    var newRow = new Row();
                    foreach (DataColumn col in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }
                //wsDt.Cells[wsDt.Dimension.Address].AutoFitColumns();

                workbookpart.Workbook.Save();

                spreadsheetDocument.Close();
            }
            else
            {
                // Open the Excel file that we created before, and start to add sheets to it.
                var spreadsheetDocument = SpreadsheetDocument.Open(filename, true);

                var workbookpart = spreadsheetDocument.WorkbookPart;
                if (workbookpart.Workbook == null)
                    workbookpart.Workbook = new Workbook();

                var worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                var sheets = spreadsheetDocument.WorkbookPart.Workbook.Sheets;

                if (sheets.Elements<Sheet>().Any())
                {
                    //Set the new sheet id
                    sheetId = sheets.Elements<Sheet>().Max(s => s.SheetId.Value) + 1;
                }
                else
                {
                    sheetId = 1;
                }

                // Append a new worksheet and associate it with the workbook.
                var sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                        GetIdOfPart(worksheetPart),
                    SheetId = sheetId,
                    Name = sheetname + sheetId
                };
                sheets.Append(sheet);

                //Add the header row here.
                var headerRow = new Row();

                foreach (DataColumn column in ResultsData.Columns)
                {
                    var cell = new Cell { DataType = CellValues.String, CellValue = new CellValue(column.ColumnName) };
                    headerRow.AppendChild(cell);
                }
                sheetData.AppendChild(headerRow);

                foreach (DataRow row in ResultsData.Rows)
                {
                    var newRow = new Row();

                    foreach (DataColumn col in ResultsData.Columns)
                    {
                        var cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(row[col].ToString())
                        };
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookpart.Workbook.Save();

                // Close the document.
                spreadsheetDocument.Close();
            }
        }
        public string getport()
        {
            string assign = "94";
            string Port = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];
            string port_bind = (Port == assign || Port == "64092") ? port_bind = "T" : port_bind = "F";
            return port_bind;
        }
        public static string GetUserIP()
        {
            var ip = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null
                      && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                     ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]
                     : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (ip.Contains(","))
                ip = ip.Split(',').First().Trim();
            return ip;
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
    }
}
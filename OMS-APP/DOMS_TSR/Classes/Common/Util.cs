using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using System.Data;
using System.ComponentModel;
using System.Web.UI.WebControls;
namespace SALEORDER.Common
{
    public class Util
    {
        public static string paging(String sql, int start, int end)
        {
           
            return @"   select * from (
                        SELECT a.*,rownum r
                                FROM ( " + sql +
                            @") a) x
                        WHERE R >= " + start.ToString() + "  AND R <= " + end.ToString();

        }

        public static DataTable ConvertToDataTable<T>(IEnumerable<T> data)
        {
            List<Object> list = data.Cast<Object>().ToList();

            PropertyDescriptorCollection props = null;
            DataTable table = new DataTable();
            if (list != null && list.Count > 0)
            {
                props = TypeDescriptor.GetProperties(list[0]);
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }
            if (props != null)
            {
                object[] values = new object[props.Count];
                foreach (T item in data)
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item) ?? DBNull.Value;
                    }
                    table.Rows.Add(values);
                }
            }
            return table;
        }
        public static string getStatusColor(string status)
        {
            string strcolor = "";
            if (status == "Rush")
            {
                strcolor = "yellow";
            }
            else if (status == "Confiscate")
            {
                strcolor = "orange";
            }
            else if (status == "Auction")
            {
                strcolor = "red";
            }
            else if (status == "Legal")
            {
                strcolor = "red";
            }
            else if (status  == "Close")
            {
                strcolor = "green";
            }
            else
            {
                strcolor = "white";
            }
            return strcolor;
        }

        public static Decimal[] ConvertArrStringToDecimal(String[] arr)
        {
            Decimal[] d1 = new Decimal[] { };
            Decimal d;
            if (arr.All(number => Decimal.TryParse(number, out d)))
            {
                d1 = Array.ConvertAll<String, Decimal>(arr, Convert.ToDecimal);
            }
            return d1;
        }

        public static string ValToString(object obj)
        {
            string ret = "";
            if (obj != null)
            {
                ret = obj.ToString();
            }
            return ret;
        }

        public static string ValToStringFormat(DateTime? obj, String format)
        {
            string ret = "";
            if (obj != null)
            {
                ret = obj.Value.ToString(format);
            }
            return ret;
        }

        public static string ThaiDate(String obj)
        {
            string ret = "";

            String[] dd = obj.Split('/');
            try
            {
                if (dd.Length > 0)
                {
                    String d = dd[0];
                    String m = dd[1];
                    String y = dd[2];
                    String month = "";
                    switch (m)
                    {
                        case "01": month = "ม.ค.";
                            break;
                        case "1": month = "ม.ค.";
                            break;
                        case "02": month = "ก.พ.";
                            break;
                        case "2": month = "ก.พ.";
                            break;
                        case "03": month = "มี.ค.";
                            break;
                        case "3": month = "มี.ค.";
                            break;
                        case "04": month = "เม.ย.";
                            break;
                        case "05": month = "พ.ค.";
                            break;
                        case "06": month = "มิ.ย.";
                            break;
                        case "07": month = "ก.ค.";
                            break;
                        case "08": month = "ส.ค.";
                            break;
                        case "09": month = "ก.ย.";
                            break;
                        case "10": month = "ต.ค.";
                            break;
                        case "11": month = "พ.ย.";
                            break;
                        case "12": month = "ธ.ค.";
                            break;
                        case "4": month = "เม.ย.";
                            break;
                        case "5": month = "พ.ค.";
                            break;
                        case "6": month = "มิ.ย.";
                            break;
                        case "7": month = "ก.ค.";
                            break;
                        case "8": month = "ส.ค.";
                            break;
                        case "9": month = "ก.ย.";
                            break;

                    }
                    Decimal year = Decimal.Parse(y) + 543;
                    ret = d + " " + month + " " + year.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return ret;
        }
        public static string ThaiShortDate(String obj)
        {
            string ret = "";

            String[] dd = obj.Split('/');
            try
            {
                if (dd.Length > 0)
                {
                    String d = dd[0];
                    String m = dd[1];
                    String y = dd[2];

                    Decimal year = Decimal.Parse(y) + 543;
                    ret = d + "/" + m + "/" + year.ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return ret;
        }

        public static string ValCurrent(Decimal? obj)
        {
            string ret = "0.00";
            if (obj != null)
            {
                ret = String.Format("{0:#,##0.00}", obj.Value);
            }
            return ret;
        }

        public static string ValCurrentDouble(Double obj)
        {
            string ret = "0.00";
            if (obj != null)
            {
                ret = String.Format("{0:#,##0.00}", obj);
            }
            return ret;
        }

        public static string ValToString(object obj, string dateFormat)
        {
            string ret = "";
            if (obj != null)
            {
                ret = ((DateTime)obj).ToString(dateFormat);
            }
            return ret;
        }
        public static string DecimalToString(object obj, string format)
        {
            string ret = "";
            if (obj != null)
            {
                ret = ((decimal)obj).ToString(format);
            }
            return ret;
        }
        public static string ThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }

        public static string BooleanToString(bool obj)
        {
            return obj ? "Y" : "F";
        }

        public static DateTime? StringToDateTime(string obj)
        {
            DateTime result;
            if (DateTime.TryParseExact(obj, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;
        }
        public static DateTime? StringToDateTime(string obj, String format)
        {
            DateTime result;
            if (DateTime.TryParseExact(obj, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            return null;
        }

        public static Decimal? StringToDecimal(string obj)
        {
            Decimal result;
            if (Decimal.TryParse(obj, out result))
            {
                return result;
            }
            return null;
        }
        public static Decimal StringToDec(string obj)
        {
            Decimal result = 0;
            if (Decimal.TryParse(obj, out result))
            {
                return result;
            }
            return result;
        }
        public static int StringToint(string obj)
        {
            int result = 0;
            if (int.TryParse(obj, out result))
            {
                return result;
            }
            return result;
        }
        public static Decimal DoubleToDec(Double obj)
        {
            Decimal result = 0;
            if (Decimal.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return result;
        }
        public static Double? StringToDouble(string obj)
        {
            Double result;
            if (Double.TryParse(obj, out result))
            {
                return result;
            }
            return 0;
        }

        public static String nullString(String val)
        {
            try
            {
                return (!String.IsNullOrEmpty(val) ? val : "");
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static String nullDecimal(String val)
        {
            try
            {
                return (!String.IsNullOrEmpty(val) ? val : "0");
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static Decimal? nullDecimalValue(Decimal? val)
        {
            try
            {
                return ((val != null) ? val : 0);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static String getTumbon(String tumbon, String province)
        {
            String rev = "";

            try
            {
                if (!String.IsNullOrEmpty(tumbon))
                {
                    if ("กรุงเทพมหานคร".Equals(province))
                    {
                        rev = " แขวง " + tumbon;
                    }
                    else
                    {
                        rev = " ต. " + tumbon;
                    }
                }


            }
            catch (Exception ex)
            {
                return "";
            }

            return rev;
        }

        public static String getAumphur(String amphur, String province)
        {
            String rev = "";

            try
            {
                if (!String.IsNullOrEmpty(amphur))
                {
                    if ("กรุงเทพมหานคร".Equals(province))
                    {
                        rev = "เขต " + amphur;
                    }
                    else
                    {
                        rev = "อ. " + amphur;
                    }
                }


            }
            catch (Exception ex)
            {
                return "";
            }

            return rev;
        }

        public static Decimal getDecimal(Decimal val)
        {
            Decimal rev = 0;
            try
            {
                if (val != null)

                    rev = Decimal.Parse((!String.IsNullOrEmpty(val.ToString()) ? val.ToString() : "0"));
            }
            catch (Exception ex)
            {

            }
            return rev;
        }

        public static string EncryptString(string inputString, int dwKeySize, string xmlString)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider =
                                      new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int keySize = dwKeySize / 8;
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            RSACryptoServiceProvider here;
            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[
                        (dataLength - maxLength * i > maxLength) ? maxLength :
                                                      dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                  tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                          true);
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }
        public static string DecryptString(string inputString, int dwKeySize, string xmlString)
        {
            RSACryptoServiceProvider rsaCryptoServiceProvider
                                         = new RSACryptoServiceProvider(dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(xmlString);
            int base64BlockSize = ((dwKeySize / 8) % 3 != 0) ?
              (((dwKeySize / 8) / 3) * 4) + 4 : ((dwKeySize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(
                     inputString.Substring(base64BlockSize * i, base64BlockSize));
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                                    encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(
                                      Type.GetType("System.Byte")) as byte[]);
        }

        public static String formatCurrency(Decimal val)
        {

            String rev = "0";

            try
            {
                rev = String.Format("{0.#,###.00}", val);
            }
            catch (Exception e)
            {

            }
            return rev;

        }

        public static String splitCardNo(String val, int pos)
        {

            String rev = "";

            try
            {
                rev = val.Substring(pos - 1, 1);
            }
            catch (Exception e)
            {

            }
            return rev;

        }

        public static String splitCardExp(String val, int pos)
        {

            String rev = "";

            try
            {
                rev = val.Split('/')[pos];
            }
            catch (Exception e)
            {

            }
            return rev;

        }
        public static string ThaiLongDate(DateTime date)
        {
            string ret = "";
            try
            {
                ret = date.Day + " ";
                ret += (new ReturnMonth()).ReturnMonths(date.Month, "th") + " ";
                int year = date.Year;
                if (year < 2300)
                {
                    year += 543;
                }
                ret += year.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public static string ThaiDate(DateTime date)
        {
            string ret = "";
            try
            {
                ret = date.Day + " ";
                ret += (new ReturnMonth()).ReturnMonths(date.Month, "th2") + " ";
                int year = date.Year;
                if (year < 2300)
                {
                    year += 543;
                }
                ret += year.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        public static DateTime FirstDayOfMonthFromDateTime(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        public static DateTime SetCultureEN(string _datetime)
        {
            DateTime result;
            try
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");

                if (DateTime.TryParse(_datetime, out result))
                {
                    result = DateTime.ParseExact(_datetime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

            }
            catch (Exception ex)
            {
                throw ex;
                //txtDateFrom.Text = value != null ? value.Value.ToString("dd/MM/yyyy") : "";
            }
            return result;
        }

        public static string SetDateDMY(DateTime _datetime)
        {

            try
            {
                System.IFormatProvider engformat = new System.Globalization.CultureInfo("en-GB");

                return _datetime.ToString("dd/MM/yyyy", engformat);
                //if (DateTime.TryParse(_datetime, out result))
                //{
                //    result = DateTime.ParseExact(_datetime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public static string SetPhone(String phone1, String phone2, String phone3)
        {
            String rev = "";
            try
            {
                if (!String.IsNullOrEmpty(phone1))
                {
                    rev = rev + phone1;
                }
                if (!String.IsNullOrEmpty(phone2))
                {
                    rev = rev + "," + phone2;
                }
                if (!String.IsNullOrEmpty(phone3))
                {
                    rev = rev + "," + phone3;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rev;
        }

        public static string SetSqlDate(String param)
        {
            String rev = "";
            try
            {
                if (!String.IsNullOrEmpty(param))
                {
                    rev = "convert(datetime,'"+param+"', 103)";
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rev;
        }

        public static void setDDL(DropDownList ddls, String val)
        {
            ListItem li;
            for (int i = 0; i < ddls.Items.Count; i++)
            {
                li = ddls.Items[i];
                if (val.Equals(li.Value))
                {
                    ddls.SelectedIndex = i;
                    break;
                }
            }

        }

    }
}

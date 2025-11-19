using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APPCOREMODEL.Datas;
using APPCOREMODEL.DAO;
using System.Data.SqlClient;
using System.Data;
using APPHELPPERS;
using APPCOREMODEL.Datas.OMSDTO;
using System.Configuration;

namespace APPCOREMODEL.OMSDAO
{
    public class DashBoardDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<MonthlyInfo> ListDashBoardByCriteria(MonthlyInfo suminfo)
        {
            string strcond = " and o.OrderSituation = '01' ";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
          
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select CONVERT(varchar, o.CreateDate,103) as datadate," +
                    " case when o00.count_H00 is null then 0 else o00.count_H00 end count_H00," +
                    "case when count_H01 is null then 0 else count_H01 end count_H01," +
                    "case when count_H02 is null then 0 else count_H02 end count_H02," +
                    "case when count_H03 is null then 0 else count_H03 end count_H03," +
                    "case when count_H04 is null then 0 else count_H04 end count_H04," +
                    "case when count_H05 is null then 0 else count_H05 end count_H05," +
                    "case when count_H06 is null then 0 else count_H06 end count_H06," +
                    "case when count_H07 is null then 0 else count_H07 end count_H07," +
                    "case when count_H08 is null then 0 else count_H08 end count_H08," +
                    "case when count_H09 is null then 0 else count_H09 end count_H09," +
                    "case when count_H10 is null then 0 else count_H10 end count_H10," +
                    "case when count_H11 is null then 0 else count_H11 end count_H11," +
                    "case when count_H12 is null then 0 else count_H12 end count_H12," +
                    "case when count_H13 is null then 0 else count_H13 end count_H13," +
                    "case when count_H14 is null then 0 else count_H14 end count_H14," +
                    "case when count_H15 is null then 0 else count_H15 end count_H15," +
                    "case when count_H16 is null then 0 else count_H16 end count_H16," +
                    "case when count_H17 is null then 0 else count_H17 end count_H17," +
                    "case when count_H18 is null then 0 else count_H18 end count_H18," +
                    "case when count_H19 is null then 0 else count_H19 end count_H19," +
                    "case when count_H20 is null then 0 else count_H20 end count_H20," +
                    "case when count_H21 is null then 0 else count_H21 end count_H21," +
                    "case when count_H22 is null then 0 else count_H22 end count_H22," +
                    "case when count_H23 is null then 0 else count_H23 end count_H23" +
                    ", CASE WHEN OAmount IS NULL THEN 0 ELSE OAmount END AS oTotal" +
                    ",(select COUNT(ocount.id) from OrderInfo ocount where ocount.OrderSituation = '01') as OAllOrdercount" +

                    " from " + dbName + ".dbo.orderinfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H00 from orderinfo o0" +
                    " where o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H01 from orderinfo o0" +
                    " where o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H02 from orderinfo o0" +
                    " where o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H03 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H04 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H05 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, COUNT(OrderCode) count_H06 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H07 from orderinfo o0" +
                    " where" +
                     " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H08 from orderinfo o0" +
                    " where" +
                    "  o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H09 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H10 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H11 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H12 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H13 from orderinfo o0" +
                    " where" +
                     " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H14 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H15 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H16 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H17 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H18 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H19 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H20 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H21 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H22 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H23 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    "  LEFT OUTER JOIN(	 " +
                    " SELECT CONVERT(varchar, o0.CreateDate, 103) AS createdate, sum(ol.TotalPrice) AS OAmount" +
                    " FROM    OrderInfo AS o0 inner join OrderDetail ol on ol.OrderCode = o0.OrderCode where o0.OrderSituation = '01'" +
                    " GROUP BY CONVERT(varchar, o0.CreateDate, 103)) AS oTotal ON oTotal.createdate = CONVERT(varchar, o.CreateDate, 103)" +


                    "" +
     
                                " where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23,OAmount" +
                    " order by CONVERT(varchar, o.CreateDate,103) ";
          

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                             select new MonthlyInfo()
                             {
                                 Day = dr["datadate"].ToString().Trim(),
                                 Hour0 = dr["count_H00"].ToString().Trim(),
                                 Hour1 = dr["count_H01"].ToString().Trim(),
                                 Hour2 = dr["count_H02"].ToString().Trim(),
                                 Hour3 = dr["count_H03"].ToString().Trim(),
                                 Hour4 = dr["count_H04"].ToString().Trim(),
                                 Hour5 = dr["count_H05"].ToString().Trim(),
                                 Hour6 = dr["count_H06"].ToString().Trim(),
                                 Hour7 = dr["count_H07"].ToString().Trim(),
                                 Hour8 = dr["count_H08"].ToString().Trim(),
                                 Hour9 = dr["count_H09"].ToString().Trim(),
                                 Hour10 = dr["count_H10"].ToString().Trim(),
                                 Hour11 = dr["count_H11"].ToString().Trim(),
                                 Hour12 = dr["count_H12"].ToString().Trim(),
                                 Hour13 = dr["count_H13"].ToString().Trim(),
                                 Hour14 = dr["count_H14"].ToString().Trim(),
                                 Hour15 = dr["count_H15"].ToString().Trim(),
                                 Hour16 = dr["count_H16"].ToString().Trim(),
                                 Hour17 = dr["count_H17"].ToString().Trim(),
                                 Hour18 = dr["count_H18"].ToString().Trim(),
                                 Hour19 = dr["count_H19"].ToString().Trim(),
                                 Hour20 = dr["count_H20"].ToString().Trim(),
                                 Hour21 = dr["count_H21"].ToString().Trim(),
                                 Hour22 = dr["count_H22"].ToString().Trim(),
                                 Hour23 = dr["count_H23"].ToString().Trim(),
                                 TotalAmount = dr["oTotal"].ToString().Trim(),
                                 OAllOrdercount= dr["OAllOrdercount"].ToString().Trim(),
                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
        public List<MonthlyInfo> ListDashBoardPercentOrderByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select CONVERT(varchar, o.CreateDate,103) as datadate," +
                    " case when o00.count_H00 is null then 0 ELSE  CAST(count_H00 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H00," +
                    "case when count_H01 is null then 0 ELSE  CAST(count_H01 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H01," +
                    "case when count_H02 is null then 0 ELSE  CAST(count_H02 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H02," +
                    "case when count_H03 is null then 0 ELSE  CAST(count_H03 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H03," +
                    "case when count_H04 is null then 0 ELSE  CAST(count_H04 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H04," +
                    "case when count_H05 is null then 0 ELSE  CAST(count_H05 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H05," +
                    "case when count_H06 is null then 0 ELSE  CAST(count_H06 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H06," +
                    "case when count_H07 is null then 0 ELSE  CAST(count_H07 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H07," +
                    "case when count_H08 is null then 0 ELSE  CAST(count_H08 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H08," +
                    "case when count_H09 is null then 0 ELSE  CAST(count_H09 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H09," +
                    "case when count_H10 is null then 0 ELSE  CAST(count_H10 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H10," +
                    "case when count_H11 is null then 0 ELSE  CAST(count_H11 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H11," +
                    "case when count_H12 is null then 0 ELSE  CAST(count_H12 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H12," +
                    "case when count_H13 is null then 0 ELSE  CAST(count_H13 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H13," +
                    "case when count_H14 is null then 0 ELSE  CAST(count_H14 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H14," +
                    "case when count_H15 is null then 0 ELSE  CAST(count_H15 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H15," +
                    "case when count_H16 is null then 0 ELSE  CAST(count_H16 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H16," +
                    "case when count_H17 is null then 0 ELSE  CAST(count_H17 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H17," +
                    "case when count_H18 is null then 0 ELSE  CAST(count_H18 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H18," +
                    "case when count_H19 is null then 0 ELSE  CAST(count_H19 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H19," +
                    "case when count_H20 is null then 0 ELSE  CAST(count_H20 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H20," +
                    "case when count_H21 is null then 0 ELSE  CAST(count_H21 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H21," +
                    "case when count_H22 is null then 0 ELSE  CAST(count_H22 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H22," +
                    "case when count_H23 is null then 0 ELSE  CAST(count_H23 * 100.0  / COUNT(o.OrderCode) AS DECIMAL(18, 2)) end count_H23" +
                    ", CASE WHEN OAmount IS NULL THEN 0 ELSE OAmount END AS oTotal" +
                    ",(select COUNT(ocount.id) from OrderInfo ocount) as OAllOrdercount" +

                    " from " + dbName + ".dbo.orderinfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H00 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H01 from orderinfo o0" +
                    " where  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H02 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H03 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H04 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H05 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H06 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H07 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H08 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H09 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H10 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H11 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H12 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H13 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H14 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H15 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H16 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H17 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H18 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H19 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H20 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H21 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H22 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.ordercode) count_H23 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    "  LEFT OUTER JOIN(	 " +
                    " SELECT CONVERT(varchar, o0.CreateDate, 103) AS createdate, sum(ol.TotalPrice) AS OAmount" +
                    " FROM    OrderInfo AS o0 inner join OrderDetail ol on ol.OrderCode = o0.OrderCode" +
                    " GROUP BY CONVERT(varchar, o0.CreateDate, 103)) AS oTotal ON oTotal.createdate = CONVERT(varchar, o.CreateDate, 103)" +


                    "" +

                                " where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23,OAmount" +
                    " order by CONVERT(varchar, o.CreateDate,103) ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new MonthlyInfo()
                           {
                               Day = dr["datadate"].ToString().Trim(),
                               Hour0 = dr["count_H00"].ToString().Trim(),
                               Hour1 = dr["count_H01"].ToString().Trim(),
                               Hour2 = dr["count_H02"].ToString().Trim(),
                               Hour3 = dr["count_H03"].ToString().Trim(),
                               Hour4 = dr["count_H04"].ToString().Trim(),
                               Hour5 = dr["count_H05"].ToString().Trim(),
                               Hour6 = dr["count_H06"].ToString().Trim(),
                               Hour7 = dr["count_H07"].ToString().Trim(),
                               Hour8 = dr["count_H08"].ToString().Trim(),
                               Hour9 = dr["count_H09"].ToString().Trim(),
                               Hour10 = dr["count_H10"].ToString().Trim(),
                               Hour11 = dr["count_H11"].ToString().Trim(),
                               Hour12 = dr["count_H12"].ToString().Trim(),
                               Hour13 = dr["count_H13"].ToString().Trim(),
                               Hour14 = dr["count_H14"].ToString().Trim(),
                               Hour15 = dr["count_H15"].ToString().Trim(),
                               Hour16 = dr["count_H16"].ToString().Trim(),
                               Hour17 = dr["count_H17"].ToString().Trim(),
                               Hour18 = dr["count_H18"].ToString().Trim(),
                               Hour19 = dr["count_H19"].ToString().Trim(),
                               Hour20 = dr["count_H20"].ToString().Trim(),
                               Hour21 = dr["count_H21"].ToString().Trim(),
                               Hour22 = dr["count_H22"].ToString().Trim(),
                               Hour23 = dr["count_H23"].ToString().Trim(),
                               TotalAmount = dr["oTotal"].ToString().Trim(),
                               OAllOrdercount = dr["OAllOrdercount"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
        public List<MonthlyInfo> ListDashBoardAmountOrderandPriceByCriteria(MonthlyInfo suminfo)
        {
            string strcond = " and o.OrderSituation = '01' ";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select CONVERT(varchar, o.CreateDate,103) as datadate," +
                    " case when o00.count_H00 is null then 0 else o00.count_H00 end count_H00," +
                    "case when count_H01 is null then 0 else count_H01 end count_H01," +
                    "case when count_H02 is null then 0 else count_H02 end count_H02," +
                    "case when count_H03 is null then 0 else count_H03 end count_H03," +
                    "case when count_H04 is null then 0 else count_H04 end count_H04," +
                    "case when count_H05 is null then 0 else count_H05 end count_H05," +
                    "case when count_H06 is null then 0 else count_H06 end count_H06," +
                    "case when count_H07 is null then 0 else count_H07 end count_H07," +
                    "case when count_H08 is null then 0 else count_H08 end count_H08," +
                    "case when count_H09 is null then 0 else count_H09 end count_H09," +
                    "case when count_H10 is null then 0 else count_H10 end count_H10," +
                    "case when count_H11 is null then 0 else count_H11 end count_H11," +
                    "case when count_H12 is null then 0 else count_H12 end count_H12," +
                    "case when count_H13 is null then 0 else count_H13 end count_H13," +
                    "case when count_H14 is null then 0 else count_H14 end count_H14," +
                    "case when count_H15 is null then 0 else count_H15 end count_H15," +
                    "case when count_H16 is null then 0 else count_H16 end count_H16," +
                    "case when count_H17 is null then 0 else count_H17 end count_H17," +
                    "case when count_H18 is null then 0 else count_H18 end count_H18," +
                    "case when count_H19 is null then 0 else count_H19 end count_H19," +
                    "case when count_H20 is null then 0 else count_H20 end count_H20," +
                    "case when count_H21 is null then 0 else count_H21 end count_H21," +
                    "case when count_H22 is null then 0 else count_H22 end count_H22," +
                    "case when count_H23 is null then 0 else count_H23 end count_H23" +
                    ", CASE WHEN OAmount IS NULL THEN 0 ELSE OAmount END AS oTotal" +
                    ",(select COUNT(ocount.id) from OrderInfo ocount where ocount.OrderSituation = '01') as OAllOrdercount" +

                    " from " + dbName + ".dbo.orderinfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H00 from orderinfo o0" +
                    " where o0.OrderSituation = '01'  and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H01 from orderinfo o0" +
                    " where o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H02 from orderinfo o0" +
                    " where o0.OrderSituation = '01'  and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H03 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H04 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H05 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H06 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H07 from orderinfo o0" +
                    " where" +
                     " o0.OrderSituation = '01'  and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H08 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H09 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H10 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H11 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H12 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H13 from orderinfo o0" +
                    " where" +
                     " o0.OrderSituation = '01'  and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H14 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H15 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H16 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H17 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H18 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H19 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H20 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H21 from orderinfo o0" +
                    " where" +
                    "  o0.OrderSituation = '01'  and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H22 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H23 from orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01'  and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    "  LEFT OUTER JOIN(	 " +
                    " SELECT CONVERT(varchar, o0.CreateDate, 103) AS createdate, sum(ol.TotalPrice) AS OAmount" +
                    " FROM    OrderInfo AS o0 inner join OrderDetail ol on ol.OrderCode = o0.OrderCode where o0.OrderSituation = '01' " +
                    " GROUP BY CONVERT(varchar, o0.CreateDate, 103)) AS oTotal ON oTotal.createdate = CONVERT(varchar, o.CreateDate, 103)" +


                    "" +

                                " where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23,OAmount" +
                    " order by CONVERT(varchar, o.CreateDate,103) ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new MonthlyInfo()
                           {
                               Day = dr["datadate"].ToString().Trim(),
                               Hour0 = dr["count_H00"].ToString().Trim(),
                               Hour1 = dr["count_H01"].ToString().Trim(),
                               Hour2 = dr["count_H02"].ToString().Trim(),
                               Hour3 = dr["count_H03"].ToString().Trim(),
                               Hour4 = dr["count_H04"].ToString().Trim(),
                               Hour5 = dr["count_H05"].ToString().Trim(),
                               Hour6 = dr["count_H06"].ToString().Trim(),
                               Hour7 = dr["count_H07"].ToString().Trim(),
                               Hour8 = dr["count_H08"].ToString().Trim(),
                               Hour9 = dr["count_H09"].ToString().Trim(),
                               Hour10 = dr["count_H10"].ToString().Trim(),
                               Hour11 = dr["count_H11"].ToString().Trim(),
                               Hour12 = dr["count_H12"].ToString().Trim(),
                               Hour13 = dr["count_H13"].ToString().Trim(),
                               Hour14 = dr["count_H14"].ToString().Trim(),
                               Hour15 = dr["count_H15"].ToString().Trim(),
                               Hour16 = dr["count_H16"].ToString().Trim(),
                               Hour17 = dr["count_H17"].ToString().Trim(),
                               Hour18 = dr["count_H18"].ToString().Trim(),
                               Hour19 = dr["count_H19"].ToString().Trim(),
                               Hour20 = dr["count_H20"].ToString().Trim(),
                               Hour21 = dr["count_H21"].ToString().Trim(),
                               Hour22 = dr["count_H22"].ToString().Trim(),
                               Hour23 = dr["count_H23"].ToString().Trim(),
                               TotalAmount = dr["oTotal"].ToString().Trim(),
                               OAllOrdercount = dr["OAllOrdercount"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
        public List<MonthlyInfo> ListDashBoardCallInByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select CONVERT(varchar, o.CreateDate,103) as datadate," +
                    " case when o00.count_H00 is null then 0 else o00.count_H00 end count_H00," +
                    "case when count_H01 is null then 0 else count_H01 end count_H01," +
                    "case when count_H02 is null then 0 else count_H02 end count_H02," +
                    "case when count_H03 is null then 0 else count_H03 end count_H03," +
                    "case when count_H04 is null then 0 else count_H04 end count_H04," +
                    "case when count_H05 is null then 0 else count_H05 end count_H05," +
                    "case when count_H06 is null then 0 else count_H06 end count_H06," +
                    "case when count_H07 is null then 0 else count_H07 end count_H07," +
                    "case when count_H08 is null then 0 else count_H08 end count_H08," +
                    "case when count_H09 is null then 0 else count_H09 end count_H09," +
                    "case when count_H10 is null then 0 else count_H10 end count_H10," +
                    "case when count_H11 is null then 0 else count_H11 end count_H11," +
                    "case when count_H12 is null then 0 else count_H12 end count_H12," +
                    "case when count_H13 is null then 0 else count_H13 end count_H13," +
                    "case when count_H14 is null then 0 else count_H14 end count_H14," +
                    "case when count_H15 is null then 0 else count_H15 end count_H15," +
                    "case when count_H16 is null then 0 else count_H16 end count_H16," +
                    "case when count_H17 is null then 0 else count_H17 end count_H17," +
                    "case when count_H18 is null then 0 else count_H18 end count_H18," +
                    "case when count_H19 is null then 0 else count_H19 end count_H19," +
                    "case when count_H20 is null then 0 else count_H20 end count_H20," +
                    "case when count_H21 is null then 0 else count_H21 end count_H21," +
                    "case when count_H22 is null then 0 else count_H22 end count_H22," +
                    "case when count_H23 is null then 0 else count_H23 end count_H23" +
                    ", CASE WHEN OAmount IS NULL THEN 0 ELSE OAmount END AS oTotal" +
                    ",(select COUNT(ocount.id) from " + dbName + ".dbo.CallInfo ocount) as OAllOrdercount" +

                    " from " + dbName + ".dbo.CallInfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H00 from " + dbName + ".dbo.CallInfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H01 from " + dbName + ".dbo.CallInfo o0" +
                    " where  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H02 from " + dbName + ".dbo.CallInfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H03 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H04 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H05 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H06 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H07 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H08 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H09 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H10 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H11 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H12 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H13 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H14 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H15 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H16 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H17 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H18 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H19 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H20 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H21 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H22 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H23 from " + dbName + ".dbo.CallInfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    "  LEFT OUTER JOIN(	 " +
                    " SELECT CONVERT(varchar, o0.CreateDate, 103) AS createdate, count(o0.id) AS OAmount" +
                    " FROM "+dbName+".dbo.OrderInfo AS o0 inner join OrderDetail ol on ol.OrderCode = o0.OrderCode" +
                    " GROUP BY CONVERT(varchar, o0.CreateDate, 103)) AS oTotal ON oTotal.createdate = CONVERT(varchar, o.CreateDate, 103)" +


                    "" +

                                " where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23,OAmount" +
                    " order by CONVERT(varchar, o.CreateDate,103) ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                com.CommandTimeout = 0;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new MonthlyInfo()
                           {
                               Day = dr["datadate"].ToString().Trim(),
                               Hour0 = dr["count_H00"].ToString().Trim(),
                               Hour1 = dr["count_H01"].ToString().Trim(),
                               Hour2 = dr["count_H02"].ToString().Trim(),
                               Hour3 = dr["count_H03"].ToString().Trim(),
                               Hour4 = dr["count_H04"].ToString().Trim(),
                               Hour5 = dr["count_H05"].ToString().Trim(),
                               Hour6 = dr["count_H06"].ToString().Trim(),
                               Hour7 = dr["count_H07"].ToString().Trim(),
                               Hour8 = dr["count_H08"].ToString().Trim(),
                               Hour9 = dr["count_H09"].ToString().Trim(),
                               Hour10 = dr["count_H10"].ToString().Trim(),
                               Hour11 = dr["count_H11"].ToString().Trim(),
                               Hour12 = dr["count_H12"].ToString().Trim(),
                               Hour13 = dr["count_H13"].ToString().Trim(),
                               Hour14 = dr["count_H14"].ToString().Trim(),
                               Hour15 = dr["count_H15"].ToString().Trim(),
                               Hour16 = dr["count_H16"].ToString().Trim(),
                               Hour17 = dr["count_H17"].ToString().Trim(),
                               Hour18 = dr["count_H18"].ToString().Trim(),
                               Hour19 = dr["count_H19"].ToString().Trim(),
                               Hour20 = dr["count_H20"].ToString().Trim(),
                               Hour21 = dr["count_H21"].ToString().Trim(),
                               Hour22 = dr["count_H22"].ToString().Trim(),
                               Hour23 = dr["count_H23"].ToString().Trim(),
                               TotalAmount = dr["oTotal"].ToString().Trim(),
                               OAllOrdercount = dr["OAllOrdercount"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
        public List<MonthlyInfo> ListDashBoardAVerCallInByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select CONVERT(varchar, o.CreateDate,103) as datadate," +
                    " CASE WHEN o00.count_H00 IS NULL THEN 0 ELSE CAST(count_H00 * 100.0 / COUNT(o.id) AS DECIMAL(18, 2)) END AS count_H00," +
                    "CASE WHEN count_H01 IS NULL THEN 0 ELSE CAST(count_H01 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H01, " +
                    "    CASE WHEN count_H02 IS NULL THEN 0 ELSE CAST(count_H02 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H02, " +
                    " CASE WHEN count_H03 IS NULL THEN 0 ELSE CAST(count_H03 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H03," +
                    " CASE WHEN count_H04 IS NULL THEN 0 ELSE CAST(count_H04 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H04," +
                    " CASE WHEN count_H05 IS NULL THEN 0 ELSE CAST(count_H05 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H05," +
                    " CASE WHEN count_H06 IS NULL THEN 0 ELSE CAST(count_H06 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H06," +
                    " CASE WHEN count_H07 IS NULL THEN 0 ELSE CAST(count_H07 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H07," +
                    " CASE WHEN count_H08 IS NULL THEN 0 ELSE CAST(count_H08 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H08, " +
                    " CASE WHEN count_H09 IS NULL THEN 0 ELSE CAST(count_H09 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H09," +
                    "CASE WHEN count_H10 IS NULL THEN 0 ELSE CAST(count_H10 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H10," +
                    " CASE WHEN count_H11 IS NULL THEN 0 ELSE CAST(count_H11 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H11," +
                    "CASE WHEN count_H12 IS NULL THEN 0 ELSE CAST(count_H12 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H12," +
                    "CASE WHEN count_H13 IS NULL THEN 0 ELSE CAST(count_H13 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H13, " +
                    " CASE WHEN count_H14 IS NULL THEN 0 ELSE CAST(count_H14 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H14," +
                    "CASE WHEN count_H15 IS NULL THEN 0 ELSE CAST(count_H15 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H15, " +
                    "CASE WHEN count_H16 IS NULL THEN 0 ELSE CAST(count_H16 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H16," +
                    "CASE WHEN count_H17 IS NULL THEN 0 ELSE CAST(count_H17 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H17," +
                    "CASE WHEN count_H18 IS NULL THEN 0 ELSE CAST(count_H18 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H18, " +
                    "CASE WHEN count_H19 IS NULL THEN 0 ELSE CAST(count_H19 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H19," +
                    "CASE WHEN count_H20 IS NULL THEN 0 ELSE CAST(count_H20 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H20," +
                    "CASE WHEN count_H21 IS NULL THEN 0 ELSE CAST(count_H21 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H21," +
                    " CASE WHEN count_H22 IS NULL THEN 0 ELSE CAST(count_H22 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H22, " +
                    "CASE WHEN count_H23 IS NULL THEN 0 ELSE CAST(count_H23 * 100.0 / count(o.id) AS DECIMAL(18, 2)) END AS count_H23, " +
                    " CASE WHEN OAmount IS NULL THEN 0 ELSE OAmount END AS oTotal" +
                    ",(select COUNT(ocount.id) from CallInfo ocount) as OAllOrdercount" +

                    " from " + dbName + ".dbo.CallInfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H00 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H01 from orderinfo o0" +
                    " where  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H02 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H03 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H04 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H05 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H06 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H07 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H08 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H09 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H10 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H11 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H12 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H13 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H14 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H15 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H16 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H17 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H18 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H19 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H20 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H21 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H22 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,count(o0.id) count_H23 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    "  LEFT OUTER JOIN(	 " +
                    " SELECT CONVERT(varchar, o0.CreateDate, 103) AS createdate, count(o0.id) AS OAmount" +
                    " FROM    callinfo AS o0 " +
                    " GROUP BY CONVERT(varchar, o0.CreateDate, 103)) AS oTotal ON oTotal.createdate = CONVERT(varchar, o.CreateDate, 103)" +


                    "" +

                                " where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23,OAmount" +
                    " order by CONVERT(varchar, o.CreateDate,103) ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new MonthlyInfo()
                           {
                               Day = dr["datadate"].ToString().Trim(),
                               Hour0 = dr["count_H00"].ToString().Trim(),
                               Hour1 = dr["count_H01"].ToString().Trim(),
                               Hour2 = dr["count_H02"].ToString().Trim(),
                               Hour3 = dr["count_H03"].ToString().Trim(),
                               Hour4 = dr["count_H04"].ToString().Trim(),
                               Hour5 = dr["count_H05"].ToString().Trim(),
                               Hour6 = dr["count_H06"].ToString().Trim(),
                               Hour7 = dr["count_H07"].ToString().Trim(),
                               Hour8 = dr["count_H08"].ToString().Trim(),
                               Hour9 = dr["count_H09"].ToString().Trim(),
                               Hour10 = dr["count_H10"].ToString().Trim(),
                               Hour11 = dr["count_H11"].ToString().Trim(),
                               Hour12 = dr["count_H12"].ToString().Trim(),
                               Hour13 = dr["count_H13"].ToString().Trim(),
                               Hour14 = dr["count_H14"].ToString().Trim(),
                               Hour15 = dr["count_H15"].ToString().Trim(),
                               Hour16 = dr["count_H16"].ToString().Trim(),
                               Hour17 = dr["count_H17"].ToString().Trim(),
                               Hour18 = dr["count_H18"].ToString().Trim(),
                               Hour19 = dr["count_H19"].ToString().Trim(),
                               Hour20 = dr["count_H20"].ToString().Trim(),
                               Hour21 = dr["count_H21"].ToString().Trim(),
                               Hour22 = dr["count_H22"].ToString().Trim(),
                               Hour23 = dr["count_H23"].ToString().Trim(),
                               TotalAmount = dr["oTotal"].ToString().Trim(),
                               OAllOrdercount = dr["OAllOrdercount"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }

        public List<MonthlyInfo> ListDashBoardAverAmountOrderandPriceByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select CONVERT(varchar, o.CreateDate,103) as datadate," +
                    " case when o00.count_H00 is null then 0 else o00.count_H00 end count_H00," +
                    "case when count_H01 is null then 0 else count_H01 end count_H01," +
                    "case when count_H02 is null then 0 else count_H02 end count_H02," +
                    "case when count_H03 is null then 0 else count_H03 end count_H03," +
                    "case when count_H04 is null then 0 else count_H04 end count_H04," +
                    "case when count_H05 is null then 0 else count_H05 end count_H05," +
                    "case when count_H06 is null then 0 else count_H06 end count_H06," +
                    "case when count_H07 is null then 0 else count_H07 end count_H07," +
                    "case when count_H08 is null then 0 else count_H08 end count_H08," +
                    "case when count_H09 is null then 0 else count_H09 end count_H09," +
                    "case when count_H10 is null then 0 else count_H10 end count_H10," +
                    "case when count_H11 is null then 0 else count_H11 end count_H11," +
                    "case when count_H12 is null then 0 else count_H12 end count_H12," +
                    "case when count_H13 is null then 0 else count_H13 end count_H13," +
                    "case when count_H14 is null then 0 else count_H14 end count_H14," +
                    "case when count_H15 is null then 0 else count_H15 end count_H15," +
                    "case when count_H16 is null then 0 else count_H16 end count_H16," +
                    "case when count_H17 is null then 0 else count_H17 end count_H17," +
                    "case when count_H18 is null then 0 else count_H18 end count_H18," +
                    "case when count_H19 is null then 0 else count_H19 end count_H19," +
                    "case when count_H20 is null then 0 else count_H20 end count_H20," +
                    "case when count_H21 is null then 0 else count_H21 end count_H21," +
                    "case when count_H22 is null then 0 else count_H22 end count_H22," +
                    "case when count_H23 is null then 0 else count_H23 end count_H23" +
                    ", CASE WHEN OAmount IS NULL THEN 0 ELSE OAmount END AS oTotal" +
                    ",(select COUNT(ocount.id) from OrderInfo ocount) as OAllOrdercount" +

                    " from " + dbName + ".dbo.orderinfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H00 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H01 from orderinfo o0" +
                    " where  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H02 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H03 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H04 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H05 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H06 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H07 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H08 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H09 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H10 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H11 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H12 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H13 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H14 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H15 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H16 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H17 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H18 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H19 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H20 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H21 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H22 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H23 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    "  LEFT OUTER JOIN(	 " +
                    " SELECT CONVERT(varchar, o0.CreateDate, 103) AS createdate, sum(ol.TotalPrice) AS OAmount" +
                    " FROM    OrderInfo AS o0 inner join OrderDetail ol on ol.OrderCode = o0.OrderCode" +
                    " GROUP BY CONVERT(varchar, o0.CreateDate, 103)) AS oTotal ON oTotal.createdate = CONVERT(varchar, o.CreateDate, 103)" +


                    "" +

                                " where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23,OAmount" +
                    " order by CONVERT(varchar, o.CreateDate,103) ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new MonthlyInfo()
                           {
                               Day = dr["datadate"].ToString().Trim(),
                               Hour0 = dr["count_H00"].ToString().Trim(),
                               Hour1 = dr["count_H01"].ToString().Trim(),
                               Hour2 = dr["count_H02"].ToString().Trim(),
                               Hour3 = dr["count_H03"].ToString().Trim(),
                               Hour4 = dr["count_H04"].ToString().Trim(),
                               Hour5 = dr["count_H05"].ToString().Trim(),
                               Hour6 = dr["count_H06"].ToString().Trim(),
                               Hour7 = dr["count_H07"].ToString().Trim(),
                               Hour8 = dr["count_H08"].ToString().Trim(),
                               Hour9 = dr["count_H09"].ToString().Trim(),
                               Hour10 = dr["count_H10"].ToString().Trim(),
                               Hour11 = dr["count_H11"].ToString().Trim(),
                               Hour12 = dr["count_H12"].ToString().Trim(),
                               Hour13 = dr["count_H13"].ToString().Trim(),
                               Hour14 = dr["count_H14"].ToString().Trim(),
                               Hour15 = dr["count_H15"].ToString().Trim(),
                               Hour16 = dr["count_H16"].ToString().Trim(),
                               Hour17 = dr["count_H17"].ToString().Trim(),
                               Hour18 = dr["count_H18"].ToString().Trim(),
                               Hour19 = dr["count_H19"].ToString().Trim(),
                               Hour20 = dr["count_H20"].ToString().Trim(),
                               Hour21 = dr["count_H21"].ToString().Trim(),
                               Hour22 = dr["count_H22"].ToString().Trim(),
                               Hour23 = dr["count_H23"].ToString().Trim(),
                               TotalAmount = dr["oTotal"].ToString().Trim(),
                               OAllOrdercount = dr["OAllOrdercount"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }

        public List<TotalMonthlyInfo> ListDashBoardTotalHeaderByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and c.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LCallin = new List<TotalMonthlyInfo>();

            try
            {
                string strsql = " SELECT count(c.id) AS totalcall" +
                    " ,(select count(o.id) from "+ dbName +".dbo.OrderInfo o " +
                    "inner join " + dbName + ".dbo.CallInfo c0 on c0.id=o.Callinfo_id" +
                    " where o.OrderSituation = '01' and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')" +
                     ") As totalorder" +
                    ",(select sum(od.price) from " + dbName + ".dbo.OrderDetail od " +
                    " left join " + dbName + ".dbo.OrderInfo o on o.OrderCode = od.OrderCode " + 
                    "inner join " + dbName + ".dbo.CallInfo c0 on c0.id=o.Callinfo_id" +
                    " where o.OrderSituation = '01' and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')" +
                    ") As totalorderprice" +

                    " from " + dbName + ".dbo.CallInfo c " +
                    "" +

                                " where 1=1 " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new TotalMonthlyInfo()
                           {
                               totalcall = (dr["totalcall"].ToString().Trim() != "") ? dr["totalcall"].ToString().Trim() : "0",
                          
                               totalorder = (dr["totalorder"].ToString().Trim() != "") ? dr["totalorder"].ToString().Trim() : "0",
                             
                               totalorderprice = (dr["totalorderprice"].ToString().Trim() != "") ? dr["totalorderprice"].ToString().Trim() : "0",
                             

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
        public List<MonthlyInfo> ListDashBoardAverAmountPerHourByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                //      strcond += " and  c.Type = '" + suminfo.ViewDataType + "'";
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select CONVERT(varchar, o.CreateDate,103) as datadate," +
                   " case when o00.count_H00 is null then 0 ELSE cast(o00.count_H00 AS DECIMAL(18, 2)) END count_H00," +
                   "case when count_H01 is null then 0 ELSE cast(count_H01 AS DECIMAL(18, 2)) END count_H01," +
                   "case when count_H02 is null then 0 ELSE cast(count_H02 AS DECIMAL(18, 2)) END count_H02," +
                   "case when count_H03 is null then 0 ELSE cast(count_H03 AS DECIMAL(18, 2)) END count_H03," +
                   "case when count_H04 is null then 0 ELSE cast(count_H04 AS DECIMAL(18, 2)) END count_H04," +
                   "case when count_H05 is null then 0 ELSE cast(count_H05 AS DECIMAL(18, 2)) END count_H05," +
                   "case when count_H06 is null then 0 ELSE cast(count_H06 AS DECIMAL(18, 2)) END count_H06," +
                   "case when count_H07 is null then 0 ELSE cast(count_H07 AS DECIMAL(18, 2)) END count_H07," +
                   "case when count_H08 is null then 0 ELSE cast(count_H08 AS DECIMAL(18, 2)) END count_H08," +
                   "case when count_H09 is null then 0 ELSE cast(count_H09 AS DECIMAL(18, 2)) END count_H09," +
                   "case when count_H10 is null then 0 ELSE cast(count_H10 AS DECIMAL(18, 2)) END count_H10," +
                   "case when count_H11 is null then 0 ELSE cast(count_H11 AS DECIMAL(18, 2)) END count_H11," +
                   "case when count_H12 is null then 0 ELSE cast(count_H12 AS DECIMAL(18, 2)) END count_H12," +
                   "case when count_H13 is null then 0 ELSE cast(count_H13 AS DECIMAL(18, 2)) END count_H13," +
                   "case when count_H14 is null then 0 ELSE cast(count_H14 AS DECIMAL(18, 2)) END count_H14," +
                   "case when count_H15 is null then 0 ELSE cast(count_H15 AS DECIMAL(18, 2)) END count_H15," +
                   "case when count_H16 is null then 0 ELSE cast(count_H16 AS DECIMAL(18, 2)) END count_H16," +
                   "case when count_H17 is null then 0 ELSE cast(count_H17 AS DECIMAL(18, 2)) END count_H17," +
                   "case when count_H18 is null then 0 ELSE cast(count_H18 AS DECIMAL(18, 2)) END count_H18," +
                   "case when count_H19 is null then 0 ELSE cast(count_H19 AS DECIMAL(18, 2)) END count_H19," +
                   "case when count_H20 is null then 0 ELSE cast(count_H20 AS DECIMAL(18, 2)) END count_H20," +
                   "case when count_H21 is null then 0 ELSE cast(count_H21 AS DECIMAL(18, 2)) END count_H21," +
                   "case when count_H22 is null then 0 ELSE cast(count_H22 AS DECIMAL(18, 2)) END count_H22," +
                   "case when count_H23 is null then 0 ELSE cast(count_H23 AS DECIMAL(18, 2)) END count_H23" +
                   ", CASE WHEN OAmount IS NULL THEN 0 ELSE OAmount END AS oTotal" +
                   ",(select COUNT(ocount.id) from CallInfo ocount) as OAllOrdercount" +

                   " from " + dbName + ".dbo.CallInfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H00 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H01 from orderinfo o0" +
                    " where  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H02 from orderinfo o0" +
                    " where o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H03 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H04 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H05 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H06 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H07 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H08 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H09 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H10 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H11 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H12 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H13 from orderinfo o0" +
                    " where" +
                     " o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H14 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H15 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H16 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H17 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H18 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H19 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H20 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H21 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H22 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate, avg(TotalPrice) count_H23 from orderinfo o0" +
                    " where" +
                    "  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    "  LEFT OUTER JOIN(	 " +
                    " SELECT CONVERT(varchar, o0.CreateDate, 103) AS createdate, count(o0.id) AS OAmount" +
                    " FROM    callinfo AS o0 " +
                    " GROUP BY CONVERT(varchar, o0.CreateDate, 103)) AS oTotal ON oTotal.createdate = CONVERT(varchar, o.CreateDate, 103)" +


                    "" +

                                " where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23,OAmount" +
                    " order by CONVERT(varchar, o.CreateDate,103) ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new MonthlyInfo()
                           {
                               Day = dr["datadate"].ToString().Trim(),
                               Hour0 = dr["count_H00"].ToString().Trim(),
                               Hour1 = dr["count_H01"].ToString().Trim(),
                               Hour2 = dr["count_H02"].ToString().Trim(),
                               Hour3 = dr["count_H03"].ToString().Trim(),
                               Hour4 = dr["count_H04"].ToString().Trim(),
                               Hour5 = dr["count_H05"].ToString().Trim(),
                               Hour6 = dr["count_H06"].ToString().Trim(),
                               Hour7 = dr["count_H07"].ToString().Trim(),
                               Hour8 = dr["count_H08"].ToString().Trim(),
                               Hour9 = dr["count_H09"].ToString().Trim(),
                               Hour10 = dr["count_H10"].ToString().Trim(),
                               Hour11 = dr["count_H11"].ToString().Trim(),
                               Hour12 = dr["count_H12"].ToString().Trim(),
                               Hour13 = dr["count_H13"].ToString().Trim(),
                               Hour14 = dr["count_H14"].ToString().Trim(),
                               Hour15 = dr["count_H15"].ToString().Trim(),
                               Hour16 = dr["count_H16"].ToString().Trim(),
                               Hour17 = dr["count_H17"].ToString().Trim(),
                               Hour18 = dr["count_H18"].ToString().Trim(),
                               Hour19 = dr["count_H19"].ToString().Trim(),
                               Hour20 = dr["count_H20"].ToString().Trim(),
                               Hour21 = dr["count_H21"].ToString().Trim(),
                               Hour22 = dr["count_H22"].ToString().Trim(),
                               Hour23 = dr["count_H23"].ToString().Trim(),
                               TotalAmount = dr["oTotal"].ToString().Trim(),
                               OAllOrdercount = dr["OAllOrdercount"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
        public List<ProductAmountInfo> ListDashBoardProductAmountByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";
            string Substrcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
                Substrcond+= "  (o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)), '00:00:00') AND DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)), '23:59:59'))";


            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LProduuct= new List<ProductAmountInfo>();

            try
            {
                string strsql = "SELECT d.ProductCode, p.ProductName ,o00.sumprice,o00.sumqty" +
                   
                   " from " + dbName + ".dbo.OrderDetail AS d LEFT OUTER JOIN" +
                   " " + dbName + ".dbo.OrderInfo AS o ON o.OrderCode = d.OrderCode and o.OrderSituation = '01' LEFT OUTER JOIN" +
                   " " + dbName + ".dbo.Product AS p ON p.ProductCode = d.ProductCode and p.FlagDelete='N' LEFT OUTER JOIN" +
                   "  Promotion AS pr ON pr.PromotionCode = d.PromotionCode and pr.FlagDelete='N'" +
                   "  inner  join (SELECT  p0.ProductCode,sum(od0.Price) sumprice,sum(od0.Amount) sumqty" +
                   "  FROM    Product AS p0" +
                   "   inner join OrderDetail od0 on od0.ProductCode =p0.ProductCode" +
                   " inner join orderinfo o0 on od0.OrderCode = o0.OrderCode and o0.OrderSituation = '01' " +
                   " WHERE "+ Substrcond + "" +
                   "      GROUP BY p0.ProductCode ) AS o00 ON o00.ProductCode =d.ProductCode" +
                   " " +
                   " " +



                                " where d.FlagProSetHeader !='Y' " + strcond;


                strsql += " GROUP BY  d.ProductCode, p.ProductName,o00.sumprice,o00.sumqty  ORDER BY o00.sumprice desc";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduuct = (from DataRow dr in dt.Rows

                           select new ProductAmountInfo()
                           {
                               ProductCode = dr["ProductCode"].ToString().Trim(),
                               ProductName = dr["ProductName"].ToString().Trim(),
                               Quanlity = dr["sumqty"].ToString().Trim(),
                               Amount = dr["sumprice"].ToString().Trim(),
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduuct;
        }
        public List<PromotionAmountInfo> ListDashBoardPromotionAmountByCriteria(MonthlyInfo suminfo)
        {
            string strcond = "";
            string Substrcond = "";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
                Substrcond += "  (od.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)), '00:00:00') AND DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)), '23:59:59'))";


            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LProduuct = new List<PromotionAmountInfo>();

            try
            {
                string strsql = "SELECT od.ProductCode,p.PromotionName,SUM(od.Amount) AS sumqty,SUM(Price) AS sumprice " +
                    " FROM " + dbName + ".dbo.OrderDetail AS od " +
                    " LEFT JOIN " + dbName + ".dbo.OrderInfo AS o ON o.OrderCode = od.OrderCode" +
                    " LEFT JOIN " + dbName + ".dbo.Promotion AS p ON CONCAT('PRO SET : ', p.PromotionCode) = od.ProductCode " +
                    " WHERE " + Substrcond + " AND ProductCode LIKE 'PRO SET%' and o.OrderSituation = '01'";


                strsql += " GROUP BY ProductCode,p.PromotionName ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduuct = (from DataRow dr in dt.Rows

                             select new PromotionAmountInfo()
                             {
                                 PromotionCode = dr["ProductCode"].ToString().Trim(),
                                 PromotionName = dr["PromotionName"].ToString().Trim(),
                                 Quanlity = dr["sumqty"].ToString().Trim(),
                                 Amount = dr["sumprice"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduuct;
        }
        public List<MonthlyInfo> ListDashBoardTotalAmountByCriteria(MonthlyInfo suminfo)
        {
            string strcond = " and o.OrderSituation = '01'";

            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LCallin = new List<MonthlyInfo>();

            try
            {
                string strsql = " select sum(count_H00) count_H00, sum(count_H01) count_H01, sum(count_H02) count_H02, sum(count_H03) count_H03, sum(count_H04)  count_H04" +
                    ", sum(count_H05)count_H05, sum(count_H06)count_H06, sum(count_H07)count_H07, sum(count_H08)count_H08, sum(count_H09)count_H09" +
                    ", sum(count_H10)count_H10, sum(count_H11)count_H11, sum(count_H12)count_H12, sum(count_H13)count_H13, sum(count_H14)count_H14" +
                    ", sum(count_H15)count_H15, sum(count_H16)count_H16, sum(count_H17)count_H17, sum(count_H18)count_H18, sum(count_H19)count_H19" +
                    ", sum(count_H20)count_H20, sum(count_H21)count_H21, sum(count_H22)count_H22, sum(count_H23) count_H23" +
                    " from (" +
                    "" +
                    "" +
                    "select CONVERT(varchar, o.CreateDate,103) as datadate," +
                    " case when o00.count_H00 is null then 0 else o00.count_H00 end count_H00," +
                    "case when count_H01 is null then 0 else count_H01 end count_H01," +
                    "case when count_H02 is null then 0 else count_H02 end count_H02," +
                    "case when count_H03 is null then 0 else count_H03 end count_H03," +
                    "case when count_H04 is null then 0 else count_H04 end count_H04," +
                    "case when count_H05 is null then 0 else count_H05 end count_H05," +
                    "case when count_H06 is null then 0 else count_H06 end count_H06," +
                    "case when count_H07 is null then 0 else count_H07 end count_H07," +
                    "case when count_H08 is null then 0 else count_H08 end count_H08," +
                    "case when count_H09 is null then 0 else count_H09 end count_H09," +
                    "case when count_H10 is null then 0 else count_H10 end count_H10," +
                    "case when count_H11 is null then 0 else count_H11 end count_H11," +
                    "case when count_H12 is null then 0 else count_H12 end count_H12," +
                    "case when count_H13 is null then 0 else count_H13 end count_H13," +
                    "case when count_H14 is null then 0 else count_H14 end count_H14," +
                    "case when count_H15 is null then 0 else count_H15 end count_H15," +
                    "case when count_H16 is null then 0 else count_H16 end count_H16," +
                    "case when count_H17 is null then 0 else count_H17 end count_H17," +
                    "case when count_H18 is null then 0 else count_H18 end count_H18," +
                    "case when count_H19 is null then 0 else count_H19 end count_H19," +
                    "case when count_H20 is null then 0 else count_H20 end count_H20," +
                    "case when count_H21 is null then 0 else count_H21 end count_H21," +
                    "case when count_H22 is null then 0 else count_H22 end count_H22," +
                    "case when count_H23 is null then 0 else count_H23 end count_H23" +
                    
                    " from " + dbName + ".dbo.orderinfo o " +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H00 from "+ dbName +".dbo.orderinfo o0" +
                    " where o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '00:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'00:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o00 on o00.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H01 from "+ dbName +".dbo.orderinfo o0" +
                    " where o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '01:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'01:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o01 on o01.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H02 from "+ dbName +".dbo.orderinfo o0" +
                    " where o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '02:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'02:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o02 on o02.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H03 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '03:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'03:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o03 on o03.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H04 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '04:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'04:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o04 on o04.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H05 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '05:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'05:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o05 on o05.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H06 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '06:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'06:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o06 on o06.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H07 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                     " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '07:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'07:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o07 on o07.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H08 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '08:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'08:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o08 on o08.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H09 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '09:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'09:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o09 on o09.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H10 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '10:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'10:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o10 on o10.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H11 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '11:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'11:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o11 on o11.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H12 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '12:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'12:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o12 on o12.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H13 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                     " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '13:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'13:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o13 on o13.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H14 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '14:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'14:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o14 on o14.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H15 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '15:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'15:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o15 on o15.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H16 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '16:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'16:59:59')" +

                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o16 on o16.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H17 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '17:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'17:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o17 on o17.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H18 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '18:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'18:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o18 on o18.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                     " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H19 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '19:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'19:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o19 on o19.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H20 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '20:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'20:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o20 on o20.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H21 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '21:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'21:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o21 on o21.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H22 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and  o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '22:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'22:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o22 on o22.createdate = CONVERT(varchar, o.CreateDate, 103)" +

                    " left join(select CONVERT(varchar, o0.CreateDate,103) as createdate,sum(o0.TotalPrice) count_H23 from "+ dbName +".dbo.orderinfo o0" +
                    " where" +
                    " o0.OrderSituation = '01' and o0.CreateDate BETWEEN DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, o0.CreateDate, 103)), '23:00:00')" +
                    " AND DATEADD(day, DATEDIFF (day, 0, CONVERT(datetime2, o0.CreateDate, 103)),'23:59:59')" +
                    " group by CONVERT(varchar, o0.CreateDate, 103)" +
                    " ) as o23 on o23.createdate = CONVERT(varchar, o.CreateDate, 103)" +
                
                    "" +" where 1=1 " + strcond;


                strsql += " group by CONVERT(varchar, o.CreateDate,103)," +
                    "o00.count_H00,count_H01,count_H02,count_H03,count_H04,count_H05,count_H06,count_H07,count_H08,count_H09" +
                    ",count_H10,count_H11,count_H12,count_H13,count_H14,count_H15,count_H16,count_H17,count_H18,count_H19" +
                    ",count_H20,count_H21,count_H22,count_H23 " +
                    " ) TbAmontTotal " +
                    "";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                           select new MonthlyInfo()
                           {
                           
                               Hour0 = (dr["count_H00"].ToString().Trim() != "") ? dr["count_H00"].ToString().Trim() : "0",                        
                               Hour1 = (dr["count_H01"].ToString().Trim() != "") ? dr["count_H01"].ToString().Trim() : "0",                    
                               Hour2 = (dr["count_H02"].ToString().Trim() != "") ? dr["count_H02"].ToString().Trim() : "0",                           
                               Hour3 = (dr["count_H03"].ToString().Trim() != "") ? dr["count_H03"].ToString().Trim() : "0",                              
                               Hour4 = (dr["count_H04"].ToString().Trim() != "") ? dr["count_H04"].ToString().Trim() : "0",                   
                               Hour5 = (dr["count_H05"].ToString().Trim() != "") ? dr["count_H05"].ToString().Trim() : "0",                         
                               Hour6 = (dr["count_H06"].ToString().Trim() != "") ? dr["count_H06"].ToString().Trim() : "0",                             
                               Hour7 = (dr["count_H07"].ToString().Trim() != "") ? dr["count_H07"].ToString().Trim() : "0",                        
                               Hour8 = (dr["count_H08"].ToString().Trim() != "") ? dr["count_H08"].ToString().Trim() : "0",                       
                               Hour9 = (dr["count_H09"].ToString().Trim() != "") ? dr["count_H09"].ToString().Trim() : "0",                            
                               Hour10 = (dr["count_H10"].ToString().Trim() != "") ? dr["count_H10"].ToString().Trim() : "0",                              
                               Hour11 = (dr["count_H11"].ToString().Trim() != "") ? dr["count_H11"].ToString().Trim() : "0",                              
                               Hour12 = (dr["count_H12"].ToString().Trim() != "") ? dr["count_H12"].ToString().Trim() : "0",                              
                               Hour13 = (dr["count_H13"].ToString().Trim() != "") ? dr["count_H13"].ToString().Trim() : "0",                           
                               Hour14 = (dr["count_H14"].ToString().Trim() != "") ? dr["count_H14"].ToString().Trim() : "0",                     
                               Hour15 = (dr["count_H15"].ToString().Trim() != "") ? dr["count_H15"].ToString().Trim() : "0",                           
                               Hour16 = (dr["count_H16"].ToString().Trim() != "") ? dr["count_H16"].ToString().Trim() : "0",
                                Hour17 = (dr["count_H17"].ToString().Trim() != "") ? dr["count_H17"].ToString().Trim() : "0",                           
                               Hour18 = (dr["count_H18"].ToString().Trim() != "") ? dr["count_H18"].ToString().Trim() : "0",                             
                               Hour19 = (dr["count_H19"].ToString().Trim() != "") ? dr["count_H19"].ToString().Trim() : "0",                               
                               Hour20 = (dr["count_H20"].ToString().Trim() != "") ? dr["count_H20"].ToString().Trim() : "0",
                               Hour21 = (dr["count_H21"].ToString().Trim() != "") ? dr["count_H21"].ToString().Trim() : "0",                           
                               Hour22 = (dr["count_H22"].ToString().Trim() != "") ? dr["count_H22"].ToString().Trim() : "0",                              
                               Hour23 = (dr["count_H23"].ToString().Trim() != "") ? dr["count_H23"].ToString().Trim() : "0",
                           
                           
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }

        public List<StatusInfo> ListDashBoardStatusByCriteria(StatusInfo suminfo)
        {
            string strcond = " and o.OrderSituation = '01' ";
            string strsql;
            string[] statusCode = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11" };


            if ((suminfo.DayStartMonth != null) && (suminfo.DayStartMonth != "") && (suminfo.DayEndMonth != "") && (suminfo.DayEndMonth != ""))
            {
                strcond += " and o.CreateDate BETWEEN CONVERT(datetime, '" + suminfo.DayStartMonth + " 00:00:00.000', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (datetime, '" + suminfo.DayEndMonth + " 00:00:00.000', 103)),'23:59:59')";
            }

            if ((suminfo.ViewDataType != null) && (suminfo.ViewDataType != ""))
            {
                
            }

            DataTable dt = new DataTable();
            var LStatus= new List<StatusInfo>();


            try
            {
                strsql = "SELECT CONVERT(varchar, o.CreateDate,103) as datadate,";

                foreach(var item in statusCode)
                {
                    strsql += "(CASE WHEN count_status" + item + " IS NULL THEN 0 ELSE count_status"+ item + " END) AS count_status"+ item + ",";
                }
                strsql = strsql.Remove(strsql.Length - 1);
                strsql += " FROM "+ dbName + ".dbo.OrderInfo o";

                foreach (var item in statusCode)
                {
                    strsql += " LEFT JOIN(SELECT CONVERT(VARCHAR, status"+ item + ".CreateDate,103) AS createdate,COUNT(status"+ item + ".ordercode) AS count_status"+ item + " FROM Orderinfo status"+ item + " " +
                              " WHERE status"+ item + ".OrderStatusCode = '"+ item + "'" +" and status"+item+".OrderSituation = '01' " +
                              " GROUP BY CONVERT(VARCHAR, status"+ item + ".CreateDate, 103) ) AS s"+ item + " on s"+ item + ".createdate = CONVERT(VARCHAR, o.CreateDate, 103)";
                }

                strsql += " WHERE 1=1 " + strcond;

                strsql += " GROUP BY CONVERT(varchar, o.CreateDate,103),";

                foreach(var item in statusCode)
                {
                    strsql += "count_status" + item + ",";
                }
                strsql = strsql.Remove(strsql.Length - 1);

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LStatus = (from DataRow dr in dt.Rows

                           select new StatusInfo()
                           {
                               Day = dr["datadate"].ToString().Trim(),
                               status01 = (dr["count_status01"].ToString().Trim() != "") ? dr["count_status01"].ToString().Trim() : "0",
                               status02 = (dr["count_status02"].ToString().Trim() != "") ? dr["count_status02"].ToString().Trim() : "0",
                               status03 = (dr["count_status03"].ToString().Trim() != "") ? dr["count_status03"].ToString().Trim() : "0",
                               status04 = (dr["count_status04"].ToString().Trim() != "") ? dr["count_status04"].ToString().Trim() : "0",
                               status05 = (dr["count_status05"].ToString().Trim() != "") ? dr["count_status05"].ToString().Trim() : "0",
                               status06 = (dr["count_status06"].ToString().Trim() != "") ? dr["count_status06"].ToString().Trim() : "0",
                               status07 = (dr["count_status07"].ToString().Trim() != "") ? dr["count_status07"].ToString().Trim() : "0",
                               status08 = (dr["count_status08"].ToString().Trim() != "") ? dr["count_status08"].ToString().Trim() : "0",
                               status09 = (dr["count_status09"].ToString().Trim() != "") ? dr["count_status09"].ToString().Trim() : "0",
                               status10 = (dr["count_status10"].ToString().Trim() != "") ? dr["count_status10"].ToString().Trim() : "0",
                               status11 = (dr["count_status11"].ToString().Trim() != "") ? dr["count_status11"].ToString().Trim() : "0",

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LStatus;
        }
    }
}

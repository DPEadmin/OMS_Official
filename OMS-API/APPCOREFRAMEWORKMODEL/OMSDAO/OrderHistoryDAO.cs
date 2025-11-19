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
    public class OrderHistoryDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<OrderHistoryListReturn> GetOrderHistory(OrderHistoryInfo ohInfo)
        {
            string strcond = "";

            if ((ohInfo.OrderCode != null) && (ohInfo.OrderCode != ""))
            {
                strcond += " and  od.OrderCode like '%" + ohInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderHistory = new List<OrderHistoryListReturn>();

            try
            {
                string strsql = " select distinct od.OrderCode,ord.ProductCode,p.ProductName,od.Price,od.Amount,oi.OrderStatusCode,l.LookupValue as OrderStatusName  from " + dbName + ".dbo.OrderDetail od" +
                                " join " + dbName + ".dbo.OrderInfo oi on od.OrderCode=oi.OrderCode " +
                                " join " + dbName + ".dbo.Product p on od.ProductCode=p.ProductCode " +
                                " join " + dbName + ".dbo.Lookup l on l.LookupCode= oi.OrderStatusCode and l.LookupType= 'ORDERSTATUS'" +
                                "inner join " + dbName + ".dbo.OrderDetail ord on ord.ProductCode = p.ProductCode AND oi.OrderCode = ord.OrderCode " +
                                 "where CustomerCode ='" + ohInfo.CustomerCode + "'" + strcond;
                strsql += " Order by od.OrderCode ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderHistory = (from DataRow dr in dt.Rows

                             select new OrderHistoryListReturn()
                             {
                                 OrderCode = dr["OrderCode"].ToString().Trim(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 OrderStatusName = dr["OrderStatusName"].ToString().Trim(),


                                 Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,

                                 Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderHistory;
        }

        public List<OrderDetailListReturn> GetOrderHistoryatSalesOrderPage(OrderDetailInfo ohInfo)
        {
            string strcond = "";

            if ((ohInfo.OrderCode != null) && (ohInfo.OrderCode != ""))
            {
                strcond += " and  od.OrderCode = '" + ohInfo.OrderCode + "'";
            }

            DataTable dt = new DataTable();
            var LOrderHistory = new List<OrderDetailListReturn>();

            try
            {
                string strsql = " SELECT od.OrderCode, od.ProductCode, p.ProductName, p.ProductCategoryCode, pg.ProductCategoryName, p.MerchantCode, m.MerchantName, od.Price, od.DiscountAmount, od.DiscountPercent, od.NetPrice, od.Amount, od.TotalPrice, u.LookupValue AS UnitName FROM " +
                                dbName + ".dbo.OrderDetail od LEFT OUTER JOIN " +
                                dbName + ".dbo.Product AS p ON p.ProductCode = od.ProductCode LEFT OUTER JOIN " +
                                dbName + ".dbo.Merchant AS m ON m.MerchantCode = p.MerchantCode LEFT OUTER JOIN " +
                                dbName + ".dbo.ProductCategory AS pg ON pg.ProductCategoryCode = p.ProductCategoryCode LEFT OUTER JOIN " +
                                dbName + ".dbo.Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                                " where 1=1 " + strcond;
                strsql += " Order by od.OrderCode ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderHistory = (from DataRow dr in dt.Rows

                                 select new OrderDetailListReturn()
                                 {
                                     OrderCode = dr["OrderCode"].ToString().Trim(),
                                     ProductCode = dr["ProductCode"].ToString().Trim(),
                                     ProductName = dr["ProductName"].ToString().Trim(),
                                     MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                     MerchantName = dr["MerchantName"].ToString().Trim(),
                                     ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                     ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                     UnitName = dr["UnitName"].ToString().Trim(),
                                     Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                     DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                     DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                     NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                                     Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                     TotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDouble(dr["TotalPrice"]) : 0,
                                 }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderHistory;
        }

        public int InsertOrderHistory(OrderHistoryInfo ohInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.OrderDetail  (ProductCode,Amount,Price,PromotionCode,OrderCode,PromotionDetailId)" +
                            "VALUES (" +

                           "'" + ohInfo.ProductCode + "'," +
                           "'" + ohInfo.Amount + "'," +
                           "'" + ohInfo.Price + "'," +
                           "'" + ohInfo.PromotionCode + "'," +
                             "'" + ohInfo.OrderCode + "'," +
                           "'" + ohInfo.PromotionDetailId + "'" +

                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<OrderHistoryListReturn> GetOrderHistoryWithoutProductList(OrderHistoryInfo ohInfo)
        {
            string strcond = "";


            if ((ohInfo.OrderCode != null) && (ohInfo.OrderCode != ""))
            {
                strcond += " and  oi.OrderCode like '%" + ohInfo.OrderCode + "%'";
            }
           
            if ((ohInfo.OrderStatusCode != null) && (ohInfo.OrderStatusCode != ""))
            {
                strcond += " and  oi.OrderStatusCode = '" + ohInfo.OrderStatusCode + "'";
            }

            if ((ohInfo.OrderTypeCode != null) && (ohInfo.OrderTypeCode != ""))
            {
                strcond += " and  oi.OrderType = '" + ohInfo.OrderTypeCode + "'";
            }
            if (((ohInfo.CreateDate != "") && (ohInfo.CreateDate != null)))
            {
                strcond += " and  oi.CreateDate between CONVERT(VARCHAR, '" + ohInfo.CreateDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + ohInfo.CreateDate + "', 103)),'23:59:59')";
            }
            if (((ohInfo.DeliveryDate != "") && (ohInfo.DeliveryDate != null)))
            {
                strcond += " and  oi.shipmentdate between CONVERT(VARCHAR, '" + ohInfo.DeliveryDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + ohInfo.DeliveryDate + "', 103)),'23:59:59')";
            }
            if ((ohInfo.CustomerCode != null) && (ohInfo.CustomerCode != ""))
            {
                strcond += " and  oi.CustomerCode = '" + ohInfo.CustomerCode + "'";
            }
            if ((ohInfo.CustomerFName != null) && (ohInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + ohInfo.CustomerFName + "%'";
            }
            if ((ohInfo.CustomerLName != null) && (ohInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + ohInfo.CustomerLName + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderHistory = new List<OrderHistoryListReturn>();

            try
            {
                string strsql = "select oi.CustomerCode,c.CustomerFName,c.CustomerLName,oi.OrderCode, oi.OrderStatusCode, l.LookupValue as OrderStatusName," +
                                "lt.LookupValue as OrderTypeName,oi.TotalPrice,lst.LookupValue as ORDERSTATEName, " +
                                "oi.CreateDate, oi.shipmentdate from " + dbName +".dbo.OrderInfo oi " +
                                "left join " + dbName + ".dbo.Lookup l on(l.LookupCode = oi.OrderStatusCode and l.LookupType = 'ORDERSTATUS')" +
                                "left join " + dbName + ".dbo.Lookup lt on(lt.LookupCode = oi.OrderType and lt.LookupType = 'ORDERTYPE')" +
                                "left join " + dbName + ".dbo.Lookup lst on(lst.LookupCode = oi.OrderType and lst.LookupType = 'ORDERSTATE')" +
                                "left join " + dbName + ".dbo.Customer c on(c.CustomerCode = oi.CustomerCode) " +
                                "where 1=1 " + strcond;

                strsql += " Order by oi.OrderCode ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderHistory = (from DataRow dr in dt.Rows

                                 select new OrderHistoryListReturn()
                                 {
                                     OrderCode = dr["OrderCode"].ToString().Trim(),
                                   //  ProductName = dr["ProductName"].ToString().Trim(),
                                     OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                                     OrderTypeName = dr["OrderTypeName"].ToString().Trim(),
                                   //  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                     Price = (dr["TotalPrice"].ToString() != "") ? Convert.ToInt32(dr["TotalPrice"]) : 0,
                                     CreateDate = dr["CreateDate"].ToString().Trim(),
                                     DeliveryDate = dr["shipmentdate"].ToString().Trim(),

                                 }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderHistory;
        }

        public List<OrderHistoryListReturn> GetOrderHistoryWithoutProductListPaging(OrderHistoryInfo ohInfo)
        {
            string strcond = "";


            if ((ohInfo.OrderCode != null) && (ohInfo.OrderCode != ""))
            {
                strcond += " and  oi.OrderCode like '%" + ohInfo.OrderCode + "%'";
            }
            if ((ohInfo.OrderStatusCode != null) && (ohInfo.OrderStatusCode != ""))
            {
                strcond += " and  oi.OrderStatusCode = '" + ohInfo.OrderStatusCode + "'";
            }

            if ((ohInfo.OrderTypeCode != null) && (ohInfo.OrderTypeCode != ""))
            {
                strcond += " and  oi.OrderType = '" + ohInfo.OrderTypeCode + "'";
            }
            if (((ohInfo.CreateDate != "") && (ohInfo.CreateDate != null)))
            {
                strcond += " and  oi.CreateDate between CONVERT(VARCHAR, '" + ohInfo.CreateDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + ohInfo.CreateDate + "', 103)),'23:59:59')";
            }
            if (((ohInfo.DeliveryDate != "") && (ohInfo.DeliveryDate != null)))
            {
                strcond += " and  oi.shipmentdate between CONVERT(VARCHAR, '" + ohInfo.DeliveryDate + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + ohInfo.DeliveryDate + "', 103)),'23:59:59')";
            }
            if ((ohInfo.CustomerCode != null) && (ohInfo.CustomerCode != ""))
            {
                strcond += " and  oi.CustomerCode = '" + ohInfo.CustomerCode + "'";
            }
            if ((ohInfo.CustomerFName != null) && (ohInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + ohInfo.CustomerFName + "%'";
            }
            if ((ohInfo.CustomerLName != null) && (ohInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + ohInfo.CustomerLName + "%'";
            }
            if (((ohInfo.CreateDateFrom != "") && (ohInfo.CreateDateFrom != null)) && ((ohInfo.CreateDateTo != "") && (ohInfo.CreateDateTo != null)))
            {
                strcond += " and  oi.CreateDate BETWEEN CONVERT(VARCHAR, '" + ohInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + ohInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            if (((ohInfo.DeliveryDateFrom != "") && (ohInfo.DeliveryDateFrom != null)) && ((ohInfo.DeliveryDateTo != "") && (ohInfo.DeliveryDateTo != null)))
            {
                strcond += " and  oi.DeliveryDate BETWEEN CONVERT(VARCHAR, '" + ohInfo.DeliveryDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + ohInfo.DeliveryDateTo + "', 103)),'23:59:59')";
            }

            if (((ohInfo.ReceivedDateFrom != "") && (ohInfo.ReceivedDateFrom != null)) && ((ohInfo.ReceivedDateTo != "") && (ohInfo.ReceivedDateTo != null)))
            {
                strcond += " and  oi.ReceiveDate BETWEEN CONVERT(VARCHAR, '" + ohInfo.ReceivedDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + ohInfo.ReceivedDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrderHistory = new List<OrderHistoryListReturn>();

            try
            {
                string strsql = "select oi.CustomerCode,c.CustomerFName,c.CustomerLName,oi.OrderCode, oi.OrderStatusCode, l.LookupValue as OrderStatusName," +
                                "lt.LookupValue as OrderTypeName,oi.TotalPrice,lst.LookupValue as ORDERSTATEName, " +
                                "oi.CreateDate, oi.shipmentdate, oi.ReceiveDate, oi.DeliveryDate from " + dbName + ".dbo.OrderInfo oi " +
                                "left join " + dbName + ".dbo.Lookup l on(l.LookupCode = oi.OrderStatusCode and l.LookupType = 'ORDERSTATUS')" +
                                "left join " + dbName + ".dbo.Lookup lt on(lt.LookupCode = oi.OrderType and lt.LookupType = 'ORDERTYPE')" +
                                "left join " + dbName + ".dbo.Lookup lst on(lst.LookupCode = oi.OrderType and lst.LookupType = 'ORDERSTATE')" +
                                "left join " + dbName + ".dbo.Customer c on(c.CustomerCode = oi.CustomerCode) " +
                                " where 1=1 " + strcond;

                strsql += " Order by oi.OrderCode OFFSET " + ohInfo.rowOFFSet + " ROWS FETCH NEXT " + ohInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderHistory = (from DataRow dr in dt.Rows

                                 select new OrderHistoryListReturn()
                                 {
                                     OrderCode = dr["OrderCode"].ToString().Trim(),
                                     //  ProductName = dr["ProductName"].ToString().Trim(),
                                     OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                                     OrderTypeName = dr["OrderTypeName"].ToString().Trim(),
                                     //  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                     Price = (dr["TotalPrice"].ToString() != "") ? Convert.ToInt32(dr["TotalPrice"]) : 0,
                                     CreateDate = dr["CreateDate"].ToString().Trim(),
                                     DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                                     ReceivedDate = dr["ReceiveDate"].ToString().Trim(),

                                 }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderHistory;
        }

    }
}

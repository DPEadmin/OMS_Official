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
    public class OrderDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountOrderListByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.BranchCode != null) && (oInfo.BranchCode != ""))
            {
                strcond += " and  o.Branch = '" + oInfo.BranchCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }
            if ((oInfo.TransportCode != null) && (oInfo.TransportCode != ""))
            {
                strcond += " and  o.Transport = '" + oInfo.TransportCode + "'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.shipmentdate != null) && (oInfo.shipmentdate != ""))
            {
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + oInfo.shipmentdate + "')";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond += " and  o.OrderType = '" + oInfo.OrderTypeCode + "'";
            }
            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((oInfo.DeliveryDateFrom != "") && (oInfo.DeliveryDateFrom != null)) && ((oInfo.DeliveryDateTo != "") && (oInfo.DeliveryDateTo != null)))
            {
                strcond += " and  o.DeliveryDate BETWEEN CONVERT(VARCHAR, '" + oInfo.DeliveryDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.DeliveryDateTo + "', 103)),'23:59:59')";
            }

            if (((oInfo.ReceivedDateFrom != "") && (oInfo.ReceivedDateFrom != null)) && ((oInfo.ReceivedDateTo != "") && (oInfo.ReceivedDateTo != null)))
            {
                strcond += " and  o.ReceiveDate BETWEEN CONVERT(VARCHAR, '" + oInfo.ReceivedDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.ReceivedDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = "select count(o.Id) as countOrder from " + dbName + ".dbo.OrderInfo o " +
                              " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left join Lookup ot on o.OrderType = ot.LookupCode and ot.LookupType = 'ORDERTYPE'" +
                                " left join " + dbName + ".dbo.Lookup ts on o.Transport = ts.LookupCode and ts.LookupType = 'TRANSPORT'" +
                                     " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }

        public int? CountOrderApproveListByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.shipmentdate != null) && (oInfo.shipmentdate != ""))
            {
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + oInfo.shipmentdate + "')";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond += " and  o.OrderType = '" + oInfo.OrderTypeCode + "'";
            }
            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = "select count(o.Id) as countOrder from " + dbName + ".dbo.OrderInfo o " +
                                " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left join Lookup ot on o.OrderType = ot.LookupCode and ot.LookupType = 'ORDERTYPE'" +
                                " where o.OrderStatusCode = '01' and o.OrderStateCode = '01' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }

        public List<OrderListReturn> ListOrderNopagingByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName from " + dbName + ".dbo.OrderInfo o " +
                               " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY o.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              TotalPrice = dr["TotalPrice"].ToString().Trim(),
                              OrderNote = dr["OrderNote"].ToString().Trim(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListLatestOrderNopagingByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName from " + dbName + ".dbo.OrderInfo o " +
                               " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY o.UpdateDate Desc ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              InventoryCode = dr["InventoryCode"].ToString().Trim(),
                              TotalPrice = dr["TotalPrice"].ToString().Trim(),
                              DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                              ChannelCode = dr["ChannelCode"].ToString().Trim(),
                              CallInfoId = dr["Callinfo_id"].ToString().Trim(),
                              PercentVat = (dr["PercentVat"].ToString() != "") ? Convert.ToInt32(dr["PercentVat"]) : 0,
                              OrderNote = dr["OrderNote"].ToString().Trim(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                              BranchOrderID = dr["BranchOrderID"].ToString(),
                              MediaPhone = dr["MediaPhone"].ToString(),
                              OrderSituation = dr["OrderSituation"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderDetailListReturn> OrderReportByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LOrder = new List<OrderDetailListReturn>();

            try
            {
                string strsql = " SELECT o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate, d.ProductCode, p.ProductName, d.Amount, u.LookupValue AS UnitName, d.NetPrice, cd.address, cd.subdistrict, cd.district, cd.province, cd.zipcode from " + dbName + ".dbo.OrderDetail d" +
                            " left join OrderInfo AS o ON o.OrderCode = d.OrderCode " +
                            " left join Product AS p ON p.ProductCode = d.ProductCode " +
                            " left join Promotion AS pr ON pr.PromotionCode = d.PromotionCode " +
                            " left join Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            " left join CustomerAddressDetail AS cd ON cd.CustomerCode = c.CustomerCode" +
                            " left join OrderTransport AS ot ON ot.OrderCode = o.OrderCode AND ot.AddressType = 'T'" +
                            " where o.OrderCode in('" + oInfo.OrderCode + "') " +
                            " ORDER BY d.OrderCode DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderDetailListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                              UnitName = dr["UnitName"].ToString().Trim(),
                              NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                              Address = dr["Address"].ToString(),
                              Subdistrict = dr["Subdistrict"].ToString(),
                              District = dr["District"].ToString(),
                              Province = dr["Province"].ToString(),
                              Zipcode = dr["Zipcode"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderDetailListReturn> OrderApproveReport(OrderInfo oInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LOrder = new List<OrderDetailListReturn>();

            try
            {
                string strsql = " SELECT o.OrderCode, o.OrderStatusCode, s.LookupValue as OrderStatusName, t.LookupValue as OrderTypeName, o.OrderType, o.CreateDate, od.ProductCode, p.ProductName, m.MerchantName, pg.ProductCategoryName, od.Price, u.LookupValue as UnitName, od.DiscountPercent, od.DiscountAmount, od.NetPrice, od.Amount, od.TotalPrice from " + dbName + ".dbo.OrderInfo AS o" +
                            " LEFT OUTER JOIN OrderDetail AS od ON od.OrderCode = o.OrderCode " +
                            " LEFT OUTER JOIN Product AS p ON p.ProductCode = od.ProductCode " +
                            " LEFT OUTER JOIN ProductCategory AS pg ON pg.ProductCategoryCode = p.ProductCategoryCode  " +
                            " LEFT OUTER JOIN Lookup AS s ON s.LookupCode = o.OrderStatusCode AND s.LookupType = 'ORDERSTATUS' " +
                            " LEFT OUTER JOIN Lookup AS t ON t.LookupCode = o.OrderType AND t.LookupType = 'ORDERTYPE' " +
                            " LEFT OUTER JOIN Lookup AS u ON u.LookupCode = od.Unit AND u.LookupType = 'UNIT' " +
                            " LEFT OUTER JOIN Merchant AS m ON m.MerchantCode = od.MerchantCode" +
                            " where o.OrderCode in('" + oInfo.OrderCode + "') " +
                            " ORDER BY o.OrderCode DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderDetailListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderTypeName = dr["OrderTypeName"].ToString().Trim(),
                              UnitName = dr["UnitName"].ToString().Trim(),
                              NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderDetailListReturn> OrderDetailApproveReport(OrderInfo oInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LOrder = new List<OrderDetailListReturn>();

            try
            {
                string strsql = " SELECT od.ProductCode, p.ProductName, m.MerchantName, pg.ProductCategoryName, od.Price, u.LookupValue AS UnitName, od.DiscountPercent, od.DiscountAmount, od.NetPrice, od.Amount, od.TotalPrice, od.Vat from " + dbName + ".dbo.OrderDetail AS od" +
                            " LEFT OUTER JOIN Product AS p ON p.ProductCode = od.ProductCode " +
                            " LEFT OUTER JOIN Merchant AS m ON m.MerchantCode = od.MerchantCode " +
                            " LEFT OUTER JOIN ProductCategory AS pg ON pg.ProductCategoryCode = p.ProductCategoryCode " +
                            " LEFT OUTER JOIN Lookup AS u ON u.LookupCode = od.Unit AND u.LookupType = 'UNIT' " +
                            " where od.OrderCode in('" + oInfo.OrderCode + "') " +
                            " ORDER BY od.OrderCode DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderDetailListReturn()
                          {
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                              MerchantName = dr["MerchantName"].ToString().Trim(),
                              Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                              DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                              DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                              NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                              Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                              TotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDouble(dr["TotalPrice"]) : 0,
                              Vat = (dr["Vat"].ToString() != "") ? Convert.ToDouble(dr["Vat"]) : 0,
                              UnitName = dr["UnitName"].ToString().Trim(),

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public DataSet InterfaceByCriteria(OrderInfo oInfo)
        {
            string strcond = "";


            DataTable dt = new DataTable();
            var OrderDetailDs = new DataSet("DataSet1");

            try
            {
                string strsql = " SELECT o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate, d.ProductCode, p.ProductName, d.Amount, u.LookupValue AS UnitName, d.NetPrice, cd.address, cd.subdistrict, cd.district, cd.province, cd.zipcode from " + dbName + ".dbo.OrderDetail d" +
                            " left join OrderInfo AS o ON o.OrderCode = d.OrderCode " +
                            " left join Product AS p ON p.ProductCode = d.ProductCode " +
                            " left join Promotion AS pr ON pr.PromotionCode = d.PromotionCode " +
                            " left join Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            " left join CustomerAddressDetail AS cd ON cd.CustomerCode = c.CustomerCode" +
                            " left join OrderTransport AS ot ON ot.OrderCode = o.OrderCode AND ot.AddressType = 'T'" +
                            " where o.OrderCode in('" + oInfo.OrderCode + "') " +
                            " ORDER BY d.OrderCode DESC";

                strsql += ", o.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(OrderDetailDs, "DataTable1");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return OrderDetailDs;
        }

        public List<OrderListReturn> ListOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond += " and  o.OrderType = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.TransportCode != null) && (oInfo.TransportCode != ""))
            {
                strcond += " and  o.Transport = '" + oInfo.TransportCode + "'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";

                
            }

            if (((oInfo.DeliveryDateFrom != "") && (oInfo.DeliveryDateFrom != null)) && ((oInfo.DeliveryDateTo != "") && (oInfo.DeliveryDateTo != null)))
            {
                strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";

                
            }

            if (((oInfo.ReceivedDateFrom != "") && (oInfo.ReceivedDateFrom != null)) && ((oInfo.ReceivedDateTo != "") && (oInfo.ReceivedDateTo != null)))
            {
                strcond += " AND o.ReceiveDate BETWEEN CONVERT(DATETIME, '" + oInfo.ReceivedDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.ReceivedDateTo + " 23:59:59:999',103)";

                
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }
            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.id,o.OrderCode,o.OrderStateCode,o.OrderStatusCode," +
                    " o.CustomerCode,o.TotalPrice,o.CreateBy,o.CreateDate,o.UpdateBy,o.UpdateDate,o.FlagDelete" +
                    " , CASE WHEN o.ReceiveDate IS NULL THEN '' else	convert(varchar,o.ReceiveDate , 103)  END AS ReceiveDate" +
                    ",CASE WHEN o.DeliveryDate IS NULL THEN ''  else convert(varchar,o.DeliveryDate , 103)	END AS DeliveryDate," +
                    "s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName,ot.LookupValue as OrderTypeName,ts.LookupValue as TransportName from " + dbName + ".dbo.OrderInfo o " +
                               " left join " + dbName + ".dbo.Customer c on  o.CustomerCode = c.CustomerCode" +
                                 " left join " + dbName + ".dbo.Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join " + dbName + ".dbo.Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left join " + dbName + ".dbo.Lookup ot on o.OrderType = ot.LookupCode and ot.LookupType = 'ORDERTYPE'" +
                                " left join " + dbName + ".dbo.Lookup ts on o.Transport = ts.LookupCode and ts.LookupType = 'TRANSPORT'" +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY o.Id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderTypeName = dr["OrderTypeName"].ToString().Trim(),
                              TotalPrice = dr["TotalPrice"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                              ReceivedDate = dr["ReceiveDate"].ToString().Trim(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                              TransportName = dr["TransportName"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListOrderApprovedByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond += " and  o.OrderType = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName,ot.LookupValue as OrderTypeName from " + dbName + ".dbo.OrderInfo o " +
                               " left join " + dbName + ".dbo.Customer c on  o.CustomerCode = c.CustomerCode" +
                                 " left join " + dbName + ".dbo.Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join " + dbName + ".dbo.Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left join " + dbName + ".dbo.Lookup ot on o.OrderType = ot.LookupCode and ot.LookupType = 'ORDERTYPE'" +
                                " where o.OrderStatusCode = '01' and o.OrderStateCode = '01' " + strcond;

                strsql += " ORDER BY o.Id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderTypeName = dr["OrderTypeName"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int UpdateOrderInfo(OrderInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " OrderStatusCode = '" + oInfo.OrderStatusCode + "',";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " OrderStateCode = '" + oInfo.OrderStateCode + "',";
            }
            if ((oInfo.OrderNote != null) && (oInfo.OrderNote != ""))
            {
                strcond += " OrderNote = '" + oInfo.OrderNote + "',";
            }
            if ((oInfo.shipmentdate != null) && (oInfo.shipmentdate != ""))
            {
                strcond += " shipmentdate = '" + oInfo.shipmentdate + "',";
            }
            if ((oInfo.ORDERREJECTSTATUS != null) && (oInfo.ORDERREJECTSTATUS != ""))
            {
                strcond += " ORDERREJECTSTATUS = '" + oInfo.ORDERREJECTSTATUS + "',";
            }
            if ((oInfo.OrderRejectRemark != null) && (oInfo.OrderRejectRemark != ""))
            {
                strcond += " OrderRejectRemark = '" + oInfo.OrderRejectRemark + "',";
            }

            string strsql = " Update " + dbName + ".dbo.OrderInfo set " + strcond + " UpdateBy = '" + oInfo.UpdateBy + "'," +
                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                      " where OrderCode ='" + oInfo.OrderCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int getMaxOrder(string year, string month)
        {
            int maxOrder = 1;

            string strcond = "";
            DataTable dt = new DataTable();

            try
            {
                string strsql = @" select isnull(max(isnull(runningno,0)),0) + 1 max_no from " + dbName + @".dbo.OrderInfo
                                    where month(createdate) = " + month + " and year(createdate) = " + year + " and FlagDelete ='N' ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (dt.Rows.Count > 0)
            {
                maxOrder = (dt.Rows[0]["max_no"] != null) ? int.Parse(dt.Rows[0]["max_no"].ToString()) : 1;
            }

            return maxOrder;

        }

        public String ManageOrder(List<OrderData> cInfo, List<PaymentData> pInfo, String customerCode, List<TransportData> tInfo, String emp, List<InventoryData> iInfo, String inventoryCode)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            Double? netPrice = 0;
            Double? price = 0;
            Double? vat = 0; Double? sumPrice = 0;

            Boolean rev = false;

            int orderCode = getMaxOrder(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

            String genOrderCode = "SO" + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000}", orderCode);




            foreach (var info in cInfo)
            {
                info.Vat = (((info.Price == null) ? 0 : info.Price) * info.Amount) * .07;


                //info.NetPrice = ((info.Price == null) ? 0 : info.Price);

                strsql = @"INSERT INTO " + dbName + @".[dbo].[OrderDetail]
                                   ([OrderCode]
                                   ,[PromotionCode]
                                   ,[Price]
                                   ,[ProductCode]
                                   ,[PromotionDetailId]
                                   ,[Unit]
                                   ,[DiscountAmount]
                                   ,[DiscountPercent]
                                   ,[Vat]
                                   ,[CreateDate]
                                   ,[CreateBy]
                                   ,[UpdateDate]
                                   ,[UpdateBy]
                                   ,[FlagDelete],[Amount],[NetPrice]
                                   ,[InventoryCode])
                             VALUES
                                   ('" + genOrderCode + @"'
                                   ,'" + info.PromotionCode + @"'
                                   ," + ((info.Price == null) ? 0 : info.Price) + @"
                                   ,'" + info.ProductCode + @"'
                                   ,'" + info.PromotionDetailId + @"'
                                   ,'" + info.Unit + @"'  
                                   ," + ((info.DiscountAmount == null) ? 0 : info.DiscountAmount) + @"
                                   ," + ((info.DiscountPercent == null) ? 0 : info.DiscountPercent) + @"
                                   ," + ((info.Vat == null) ? 0 : info.Vat) + @"
                                   ,getdate()
                                   ,'" + emp + @"'
                                   ,getdate()
                                   ,'" + emp + @"'
                                   ,'N'
                                    ," + ((info.Amount == null) ? 0 : info.Amount) + @"
                                    ," + ((info.SumPrice == null) ? 0 : info.SumPrice) + @"
                                    ,'" + inventoryCode + @"')";

                vat += info.Vat;
                netPrice += info.NetPrice;
                price += info.Price;
                sumPrice += info.SumPrice;
                lSQL.Add(strsql);
            }

            strsql = @"INSERT INTO " + dbName + @".[dbo].[OrderInfo]
                                   ([OrderCode]
                                   ,[OrderStatusCode]
                                   ,[OrderStateCode]
                                   ,[BUCode]
                                   ,[NetPrice]
                                   ,[TotalPrice]
                                   ,[Vat]
                                   ,[CreateDate]
                                   ,[CreateBy]
                                   ,[UpdateDate]
                                   ,[UpdateBy]
                                   ,[FlagDelete]
                                   ,[CustomerCode]
                                   ,[RunningNo])
                             VALUES
                                   ('" + genOrderCode + @"'
                                   ,'01'
                                   ,'01'
                                   ,'" + cInfo[0].BUCode + @"'
                                   ," + ((netPrice == null) ? 0 : netPrice) + @"
                                   ," + ((sumPrice == null) ? 0 : sumPrice) + @"
                                   ," + ((vat == null) ? 0 : vat) + @"
                                   ,getdate()
                                   ,'" + emp + @"'
                                   ,getdate()
                                   ,'" + emp + @"'
                                   ,'N'
                                   ,'" + customerCode + @"'
                                   ," + orderCode + @")";

            lSQL.Add(strsql);


            foreach (var p in pInfo)
            {
                strsql = @"INSERT INTO " + dbName + @".[dbo].[OrderPayment]
                                       ([OrderCode]
                                       ,[PaymentTypeCode]
                                       ,[PayAmount]
                                       ,[Installment]
                                       ,[InstallmentPrice]
                                       ,[FirstInstallment]
                                       ,[CardIssuename]
                                       ,[CardNo]
                                       ,[CardType]
                                       ,[CVCNo]
                                       ,[CardOwnerName]
                                       ,[CardExpMonth]
                                       ,[CardExpYear]
                                       ,[CitizenId]
                                       ,[BirthDate]
                                       ,[BankCode]
                                       ,[BankBranch]
                                       ,[AccountName]
                                       ,[AccountType]
                                       ,[AccountNo]
                                       ,[PaymentOtherDetail],[CreateDate]
                                       ,[CreateBy]
                                       ,[UpdateDate]
                                       ,[UpdateBy]
                                       ,[FlagDelete])
                                 VALUES
                                       ('" + genOrderCode + @"'
                                       ,'" + p.PaymentTypeCode + @"'
                                       ," + ((p.PayAmount == null) ? 0 : p.PayAmount) + @"
                                       ," + ((p.Installment == null) ? 0 : p.Installment) + @"
                                       ," + ((p.InstallmentPrice == null) ? 0 : p.InstallmentPrice) + @"
                                       ," + ((p.FirstInstallment == null) ? 0 : p.FirstInstallment) + @"
                                       ,'" + p.CardIssuename + @"'
                                       ,'" + p.CardNo + @"'
                                       ,'" + p.CardType + @"'
                                       ,'" + p.CVCNo + @"'
                                       ,'" + p.CardOwnerName + @"'
                                       ,'" + p.CardExpMonth + @"'
                                       ,'" + p.CardExpYear + @"'
                                       ,'" + p.CitizenId + @"'
                                       ,'" + p.BirthDate + @"'
                                       ,'" + p.BankCode + @"'
                                       ,'" + p.BankBranch + @"'
                                       ,'" + p.AccountName + @"'
                                       ,'" + p.AccountType + @"'
                                       ,'" + p.AccountNo + @"'
                                       ,'" + p.PaymentOtherDetail + @"'
                                        ,getdate()
                                        ,'" + emp + @"'
                                        ,getdate()
                                        ,'" + emp + @"'
                                        ,'N')";

                lSQL.Add(strsql);
            }


            foreach (var t in tInfo)
            {
                strsql = @"INSERT INTO " + dbName + @".[dbo].[OrderTransport]
                                   ([OrderCode]
                                   ,[Address]
                                   ,[Subdistrict]
                                   ,[District]
                                   ,[Province]
                                   ,[Zipcode]
                                   ,[TransportPrice]
                                   ,[TransportType]
                                   ,[CreateDate]
                                   ,[CreateBy]
                                   ,[UpdateDate]
                                   ,[UpdateBy]
                                   ,[AddressType],[TransportTypeOther])
                             VALUES
                                   ('" + genOrderCode + @"'
                                   ,'" + t.Address + @"'
                                   ,'" + t.SubDistrictCode + @"'
                                   ,'" + t.DistrictCode + @"'
                                   ,'" + t.ProvinceCode + @"'
                                   ,'" + t.Zipcode + @"'
                                   ," + ((t.TransportPrice == null) ? 0 : t.TransportPrice) + @"
                                   ,'" + t.TransportType + @"'
                                   ,getdate()
                                   ,'" + emp + @"'
                                   ,getdate()
                                   ,'" + emp + @"'
                                   ,'" + t.AddressType + @"','" + t.TransportOther + @"')";

                lSQL.Add(strsql);
            }

            foreach (var info in iInfo) //Update by Yim
            {

                if ((info.Reserved != null) && (info.Balance != null))
                {
                    string strsqlupdate = "Update InventoryDetail set ";

                    if (info.Reserved != null)
                    {
                        strsqlupdate += " Reserved = '" + info.Reserved + "', ";
                    }
                    if (info.Balance != null)
                    {
                        strsqlupdate += "Balance = '" + info.Balance + "'";
                    }

                    strsqlupdate += "where (ProductCode = '" + info.ProductCode + "') and (InventoryCode = '" + info.InventoryCode + "')";

                    lSQL.Add(strsqlupdate);

                    //Update Inventory  Movement 

                    List<InventoryListReturn> linfo = new List<InventoryListReturn>();
                    InventoryInfo iinfo = new InventoryInfo();
                    InventoryDetailDAO idao = new InventoryDetailDAO();

                    iinfo.ProductCode = info.ProductCode;
                    iinfo.InventoryCode = info.InventoryCode;

                    linfo = idao.ListInventoryDetailInfoNoPagingByCriteria(iinfo);

                    string sqlMovement = "";

                    if (linfo.Count > 0)
                    {
                        for (int x = 0; x < info.Reserving; x++)
                        {
                            //FIFO คือ Order by CreateDate ASC
                            sqlMovement = "UPDATE       InventoryMovement " +
                                          "  SET        OrderNo = '" + genOrderCode + "'" +
                                          "   WHERE        (Id = " +
                                          "                               (SELECT        TOP (1) Id " +
                                          "                                  FROM            InventoryMovement AS InventoryMovement_1 " +
                                          "                                  WHERE        (InventoryDetailId = " + linfo[0].InventoryDetailId + ") AND (OrderNo = '') " +
                                          "                                   ORDER BY CreateDate))";
                            lSQL.Add(sqlMovement);
                        }
                    }
                    //End Update Inventory  Movement
                }

                
            }


            try
            {
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);

            }
            catch (Exception ex)
            {

            }


            return (rev) ? genOrderCode : "";
        }

        public int InsertOrder(OrderDetailInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            string strsql = @"INSERT INTO " + dbName + @".[dbo].[OrderInfo]
                                   ([OrderCode]
                                   ,[OrderStatusCode]
                                   ,[OrderStateCode]
                                   ,[BUCode]
                                   ,[NetPrice]
                                   ,[TotalPrice]
                                   ,[Vat]
                                   ,[CreateDate]
                                   ,[CreateBy]
                                   ,[UpdateDate]
                                   ,[UpdateBy]
                                   ,[FlagDelete]
                                   ,[CustomerCode]
                                   ,[RunningNo])
                             VALUES
                                   ('" + oInfo.OrderCode + @"'
                                   ,'" + oInfo.OrderStatusCode + @"'
                                   ,'" + oInfo.OrderStateCode + @"'
                                   ,'" + oInfo.BUCode + @"'
                                   ," + oInfo.NetPrice + @"
                                   ," + oInfo.Price + @"
                                   ," + oInfo.Vat + @"
                                   ,getdate()
                                   ,'" + oInfo.UpdateBy + @"'
                                   ,getdate()
                                   ,'" + oInfo.UpdateBy + @"'
                                   ,'N'
                                   ,'" + oInfo.CustomerCode + @"'
                                   ,'" + oInfo.runningNo + @"')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertOrderDetail(OrderDetailInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            string strsql = @"INSERT INTO [dbo].[OrderDetail]
                                   ([OrderCode]
                                   ,[PromotionCode]
                                   ,[Price]
                                   ,[ProductCode]
                                   ,[PromotionDetailId]
                                   ,[Unit]
                                   ,[Vat]
                                   ,[CreateDate]
                                   ,[CreateBy]
                                   ,[UpdateDate]
                                   ,[UpdateBy]
                                   ,[FlagDelete])
                             VALUES
                                   ('" + oInfo.OrderCode + @"'
                                   ,'" + oInfo.PromotionCode + @"'
                                   ,'" + oInfo.Price + @"'
                                   ,'" + oInfo.ProductCode + @"'
                                   ,'" + oInfo.PromotionDetailId + @"'
                                   ,'" + oInfo.Unit + @"'
                                   ,'" + oInfo.Vat + @"'
                                   ,getdate()
                                   ,'" + oInfo.UpdateBy + @"'
                                   ,getdate()
                                   ,'" + oInfo.UpdateBy + @"'
                                   ,'N')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //old name(ListCustomerByCriteria) was the same of customerDAO's method
        public List<CustomerListReturn> ListCustomerByCriteria_fromOrder(CustomerInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Customer c " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),

                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }

        

        public List<OrderPaymentListReturn> ListOrderPaymentNoPagingByCriteria(OrderPaymentInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  p.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderPaymentListReturn>();

            try
            {
                string strsql = " select b.LookupValue as BankName,c.LookupValue as CardTypeName,m.LookupValue AS PaymentTypeName,p.* from " + dbName + ".dbo.OrderPayment p " +
                                " left join Lookup c on c.LookupCode = p.CardType and c.LookupType = 'CARDTYPE'" +
                                 " left join Lookup b on b.LookupCode = p.CardType and b.LookupType = 'CARDTYPE'" +
                                 " left join Lookup AS m ON m.LookupCode = p.PaymentTypeCode AND m.LookupType = 'PAYMENTMETHOD'" +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY p.Id  ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderPaymentListReturn()
                          {
                              OrderPaymentId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              PaymentTypeCode = dr["PaymentTypeCode"].ToString().Trim(),
                              PaymentTypeName = dr["PaymentTypeName"].ToString().Trim(),
                              PayAmount = (dr["PayAmount"].ToString() != "") ? Convert.ToDouble(dr["PayAmount"]) : 0,
                              Installment = (dr["Installment"].ToString() != "") ? Convert.ToInt32(dr["Installment"]) : 0,
                              InstallmentPrice = (dr["InstallmentPrice"].ToString() != "") ? Convert.ToDouble(dr["InstallmentPrice"]) : 0,
                              FirstInstallment = (dr["FirstInstallment"].ToString() != "") ? Convert.ToDouble(dr["FirstInstallment"]) : 0,
                              CardIssuename = dr["CardIssuename"].ToString().Trim(),
                              CardNo = dr["CardNo"].ToString().Trim(),
                              CardType = dr["CardType"].ToString().Trim(),
                              CardTypeName = dr["CardTypeName"].ToString().Trim(),
                              CVCNo = dr["CVCNo"].ToString().Trim(),
                              CardOwnerName = dr["CardOwnerName"].ToString().Trim(),
                              CardExpMonth = dr["CardExpMonth"].ToString().Trim(),
                              CardExpYear = dr["CardExpYear"].ToString().Trim(),
                              CitizenId = dr["CitizenId"].ToString().Trim(),
                              BirthDate = dr["BirthDate"].ToString().Trim(),
                              BankCode = dr["BankCode"].ToString().Trim(),
                              BankName = dr["BankName"].ToString().Trim(),
                              BankBranch = dr["BankBranch"].ToString().Trim(),
                              AccountName = dr["AccountName"].ToString().Trim(),
                              AccountType = dr["AccountType"].ToString().Trim(),
                              AccountNo = dr["AccountNo"].ToString().Trim(),
                              PaymentOtherDetail = dr["PaymentOtherDetail"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public DataSet GetDatasetOrderDetailByCriteria(OrderDetailInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  d.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var OrderDetailDs = new DataSet("DataSet1");

            try
            {
                string strsql = " select u.LookupValue as UnitName,d.*,p.ProductName,pr.PromotionName from " + dbName + ".dbo.OrderDetail d " +
                                " left join Product p on p.ProductCode = d.ProductCode " +
                                " left join Promotion pr on pr.PromotionCode = d.PromotionCode " +
                                 " left join Lookup u on u.LookupCode = d.Unit and u.LookupType='UNIT'" +
                               " where 1=1 " + strcond;

                strsql += " ORDER BY d.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(OrderDetailDs, "DataTable1");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return OrderDetailDs;
        }

        //similar result as above method
        public List<OrderDetailListReturn> GetDatasetOrderDetailByCriteria1(OrderDetailInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  d.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderDetailListReturn>();

            try
            {
                string strsql = " select u.LookupValue as UnitName,d.*,p.ProductName,pr.PromotionName, d.Amount, d.NetPrice, c.CustomerFName, c.CustomerLName, dt.address, dt.subdistrict, dt.district, dt.province, dt.zipcode from " + dbName + ".dbo.OrderDetail d " +
                                " left join " + dbName + ".dbo.Product p on p.ProductCode = d.ProductCode " +
                                " left join " + dbName + ".dbo.Promotion pr on pr.PromotionCode = d.PromotionCode " +
                                " left join " + dbName + ".dbo.Lookup u on u.LookupCode = p.Unit and u.LookupType='UNIT'" +
                                " left join " + dbName + ".dbo.OrderInfo o on o.OrderCode=d.OrderCode" +
                                " left join " + dbName + ".dbo.Customer c on c.CustomerCode=o.CustomerCode" +
                                " left join " + dbName + ".dbo.CustomerAddressDetail dt on dt.CustomerCode=c.CustomerCode" +
                               " where 1=1 " + strcond;

                strsql += " ORDER BY d.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderDetailListReturn()
                          {
                              OrderDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              PromotionCode = dr["PromotionCode"].ToString().Trim(),
                              PromotionName = dr["PromotionName"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              Address = dr["Address"].ToString().Trim(),
                              Subdistrict = dr["Subdistrict"].ToString().Trim(),
                              District = dr["District"].ToString().Trim(),
                              Province = dr["Province"].ToString().Trim(),
                              Zipcode = dr["Zipcode"].ToString().Trim(),
                              UnitName = dr["UnitName"].ToString().Trim(),
                              Unit = dr["Unit"].ToString().Trim(),
                              Vat = (dr["Vat"].ToString() != "") ? Convert.ToDouble(dr["Vat"]) : 0,
                              PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),
                              Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                              Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                              DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                              DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                              NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString()
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderDetailListReturn> ListOrderDetailNoPagingByCriteria(OrderDetailInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  d.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderDetailListReturn>();

            try
            {
                string strsql = " select u.LookupValue as UnitName,d.*,p.ProductName,pr.PromotionName, m.MerchantName, p.ProductCategoryCode, pc.ProductCategoryName from " + dbName + ".dbo.OrderDetail d " +
                                " left join Product p on p.ProductCode = d.ProductCode " +
                                " left join Promotion pr on pr.PromotionCode = d.PromotionCode " +
                                 " left join Lookup u on u.LookupCode = d.Unit and u.LookupType='UNIT'" +
                                   " left join Merchant m on m.MerchantCode = d.MerchantCode" +
                                   " left join ProductCategory pc on pc.ProductCategoryCode = p.ProductCategoryCode" +
                               " where 1=1 " + strcond + " " +
                               " group by u.LookupValue, d.Id, d.OrderCode, d.PromotionCode, d.Price, d.ProductCode, " +
                               " d.PromotionDetailId, d.Unit, d.Vat, d.CreateDate, d.CreateBy, d.UpdateDate, d.UpdateBy, d.FlagDelete, d.Amount, d.NetPrice, " +
                               " d.DiscountPercent, d.DiscountAmount, d.InventoryCode, p.ProductName, pr.PromotionName, d.MerchantCode, d.TotalPrice, m.MerchantName, p.ProductCategoryCode, pc.ProductCategoryName";
                strsql += " ORDER BY d.Id  ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderDetailListReturn()
                          {
                              OrderDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              PromotionCode = dr["PromotionCode"].ToString().Trim(),
                              PromotionName = dr["PromotionName"].ToString().Trim(),
                              ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                              ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                              MerchantCode = dr["MerchantCode"].ToString().Trim(),
                              MerchantName = dr["MerchantName"].ToString().Trim(),
                              UnitName = dr["UnitName"].ToString().Trim(),
                              PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),
                              Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                              Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                              DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                              DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                              NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                              TotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDouble(dr["TotalPrice"]) : 0,
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString()
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderTransportListReturn> ListOrderTransport(OrderTransportInfo oInfo)
        {
            string strcond = "";


            DataTable dt = new DataTable();
            var LOrder = new List<OrderTransportListReturn>();

            try
            {
                string strsql = @" SELECT [OrderCode]
                                          ,[Address]
                                          ,[Subdistrict]
                                          ,[District]
                                          ,[Province]
                                          ,[Zipcode]
                                          ,[TransportPrice]
                                          ,[TransportType]
                                          ,d.[DistrictName]
                                          ,p.ProvinceName
                                          ,sd.SubDistrictName
                                          ,[AddressType],l.LookupValue [AddressTypeName]
                                          ,[TransportTypeOther]
                                      FROM " + dbName + @".[dbo].[OrderTransport] o
                                      left join  " + dbName + @".[dbo].Province p on o.Province= p.ProvinceCode
                                      left join  " + dbName + @".[dbo].District d on o.District= d.DistrictCode
                                      left join  " + dbName + @".[dbo].SubDistrict sd on o.Subdistrict= sd.SubDistrictCode
                                      left join (select * from  " + dbName + @".[dbo].[Lookup] where [LookupType] = 'ADDRESSTYPE') l on o.AddressType= l.LookupCode

                                      where o.OrderCode = '" + oInfo.OrderCode + "'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderTransportListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              ProvinceCode = dr["Province"].ToString().Trim(),
                              ProvinceName = dr["ProvinceName"].ToString().Trim(),
                              SubDistrictCode = dr["SubDistrict"].ToString().Trim(),
                              SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                              DistrictCode = dr["District"].ToString().Trim(),
                              DistrictName = dr["DistrictName"].ToString().Trim(),
                              TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToDouble(dr["TransportPrice"]) : 0,
                              TransportType = dr["TransportType"].ToString().Trim(),
                              TransportTypeOther = dr["TransportTypeOther"].ToString().Trim(),
                              AddressType = dr["AddressType"].ToString().Trim(),
                              AddressTypeName = dr["AddressTypeName"].ToString().Trim(),
                              Zipcode = dr["Zipcode"].ToString().Trim(),
                              Address = dr["Address"].ToString().Trim()
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        

        public DataTable DtPl(string confirmNo, string date)
        {
            string strcond = "";

            if ((confirmNo != null) && (confirmNo != ""))
            {
                strcond += " and o.confirmno ='" + confirmNo + "'";
            }

            if ((date != null) && (date != ""))
            {
                strcond += " AND (CONVERT(Date, o.OrderListDate) = '" + date + "')";
            }

            DataTable dt = new DataTable();

            try
            {
                string strsql = "SELECT   d.ProductCode, p.ProductName,o.confirmno" +
                                        ",sum(CASE p.Price  " +
                                        " WHEN null THEN 0  " +
                                       "  ElSE  " +
                                      "   p.Price  " +
                                    " END) as SUM_AMOUNT  " +
                                    " , SUM(CASE d.Amount  " +
                                     "    WHEN null THEN 0  " +
                                     "    ElSE  " +
                                     "     d.Amount  " +
                                    "  END) AS SUM_QUANTITY " +
                                          "  FROM " + dbName + ".dbo.OrderDetail AS d LEFT OUTER JOIN  " +
                                          "    OrderInfo AS o ON o.OrderCode = d.OrderCode LEFT OUTER JOIN  " +
                                          "     Product AS p ON p.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                          "    Promotion AS pr ON pr.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                          "   Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                                          "   Customer AS c ON c.CustomerCode = o.CustomerCode LEFT OUTER JOIN " +
                                          "   CustomerAddressDetail AS cd ON cd.CustomerCode = c.CustomerCode LEFT OUTER JOIN " +
                                          "     OrderTransport AS ot ON ot.OrderCode = o.OrderCode AND ot.AddressType = 'T' INNER JOIN " +
                                          "          OrderPayment AS op ON op.OrderCode = o.OrderCode INNER JOIN " +
                                          "        PaymentType AS pay ON op.PaymentTypeCode = pay.Id  " +
                                          "    WHERE (1 = 1) " +
                                             strcond +
                                          "    GROUP BY d.ProductCode, p.ProductName,o.confirmno";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return dt;
        }

        //duplicated method name(CountOrderListByCriteriaOrderlist), changed into CountOrderListByCriteriaOrderInfo
        public int? CountOrderListByCriteriaOrderInfo(OrderInfo oInfo, string StatusPage)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.shipmentdate != null) && (oInfo.shipmentdate != ""))
            {
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + oInfo.shipmentdate + "')";
            }
            if ((StatusPage != null) && (StatusPage != ""))
            {
                strcond += "  and o.confirmno is null ";
            }
            else
            {
                strcond += "  and o.confirmno is not null ";
            }
            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = "select count(o.Id) as countOrder from " + dbName + ".dbo.OrderInfo o " +
                              " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                     " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }

        //duplicated method name(ListOrderByCriteriaOrderlist), changed into ListOrderByCriteriaOrderInfo
        public List<OrderListReturn> ListOrderByCriteriaOrderInfo(OrderInfo oInfo, string StatusPage)
        {
            string strcond = "";

            if ((oInfo.OrderId != null) && (oInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + oInfo.OrderId;
            }

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }
            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.shipmentdate != null) && (oInfo.shipmentdate != ""))
            {
                
                strcond += " AND o.shipmentdate BETWEEN CONVERT(DATETIME, '" + oInfo.shipmentdate + "',103) AND CONVERT(DATETIME,'" + oInfo.shipmentdate + " 23:59:59:999',103)";
            }

            if ((StatusPage != null) && (StatusPage != ""))
            {
                strcond += "  and o.confirmno is not null ";
            }
            else
            {
                strcond += "  and o.confirmno is null ";
            }
            if (((oInfo.DeliveryDateFrom != "") && (oInfo.DeliveryDateTo != null)) && ((oInfo.DeliveryDateTo != "") && (oInfo.DeliveryDateTo != null)))
            {
                strcond += " and  o.DeliveryDate BETWEEN CONVERT(VARCHAR, '" + oInfo.DeliveryDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.DeliveryDateTo + "', 103)),'23:59:59')";
            }
            
            if (((oInfo.ReceivedDateFrom != "") && (oInfo.ReceivedDateFrom != null)) && ((oInfo.ReceivedDateTo != "") && (oInfo.ReceivedDateTo != null)))
            {
                strcond += " AND o.ReceiveDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.ReceivedDateTo + " 23:59:59:999',103)";

                
            }
            

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName ,o.shipmentdate from " + dbName + ".dbo.OrderInfo o " +
                               " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where 1=1" + strcond;

                strsql += " ORDER BY o.Id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                              shipmentdate = dr["shipmentdate"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int UpdateOrderInfoOrderlist(OrderInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            string strsql = " Update " + dbName + ".dbo.OrderInfo set ";

            if ((oInfo.ConfirmNo != null) && (oInfo.ConfirmNo != ""))
            {
                strsql += " ConfirmNo = '" + oInfo.ConfirmNo + "',";
            }

            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                   " shipmentdate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                     " where ConfirmNo is null and (CONVERT(Date, CreateDate) = '" + oInfo.CreateDate + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<OrderListReturn> ListOrderByCriteriaOrderlistConfirmNo(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderListDate != null) && (oInfo.OrderListDate != ""))
            {
                strcond += " AND (CONVERT(Date, o.OrderListDate) = '" + oInfo.OrderListDate + "')";

            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select max(o.confirmno) as confirmno from " + dbName + ".dbo.OrderInfo o " +

                                " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              ConfirmNo = (dr["confirmno"].ToString() != "") ? Convert.ToString(dr["confirmno"]) : "0",
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> GetCampaignName(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.CampaignCode != null) && (oInfo.CampaignCode != ""))
            {
                strcond += " and  (CampaignCode = '" + oInfo.CampaignCode + "')";
            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " SELECT CampaignName from " + dbName + ".dbo.Campaign " +
                                " where (1=1) " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              CampaignName = dr["CampaignName"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderTransportListReturn> getlogisticbyorder(OrderTransportInfo oInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LOrder = new List<OrderTransportListReturn>();

            try
            {
                string strsql = " SELECT  l.LogisticName AS TransportTypeName, t.TransportPrice from " + dbName + ".dbo.OrderTransport AS t" +
                            " LEFT OUTER JOIN Logistic AS l ON l.LogisticCode = t.TransportType " +
                            " where t.OrderCode in('" + oInfo.OrderCode + "') " +
                            " ORDER BY t.OrderCode DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderTransportListReturn()
                          {
                              TransportTypeName = dr["TransportTypeName"].ToString().Trim(),
                              TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToDouble(dr["TransportPrice"]) : 0,

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderTransportListReturn> GetAddressbyCustomerOrder(OrderTransportInfo oInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LOrder = new List<OrderTransportListReturn>();

            try
            {
                string strsql = " SELECT c.address, s.SubDistrictName, d.DistrictName, p.ProvinceName, c.zipcode from " + dbName + ".dbo.CustomerAddressDetail  AS c " +
                            " LEFT OUTER JOIN SubDistrict AS s ON s.SubDistrictCode = c.subdistrict " +
                            " LEFT OUTER JOIN District AS d ON d.DistrictCode = c.district " +
                            " LEFT OUTER JOIN Province AS p ON p.ProvinceCode = c.province " +
                            " where  (c.UpdateDate = " +
                            "  (SELECT        MAX(UpdateDate) AS updatedate FROM CustomerAddressDetail AS CustomerAddressDetail_1 " +
                            " WHERE (CustomerCode IN ('" + oInfo.CustomerCode + "')) AND " +
                            " (AddressType = '" + oInfo.AddressType + "') AND (FlagActive = 'Y')))";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderTransportListReturn()
                          {
                              Address = dr["Address"].ToString().Trim(),
                              SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                              DistrictName = dr["DistrictName"].ToString().Trim(),
                              ProvinceName = dr["ProvinceName"].ToString().Trim(),
                              Zipcode = dr["Zipcode"].ToString().Trim(),

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }


        //โค้ดนี้ยังไม่ได้เขียน API ใหม่จึงมายืมใช้ตรงนี้ก่อน
        public int? CountRiderOrderListByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.RiderCode != null) && (oInfo.RiderCode != ""))
            {
                strcond += " and  rd.Rider_code like '%" + oInfo.RiderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " select count (distinct o.Id) as countOrder " +
                                " from OrderInfo o " +
                                " inner join CustomerAddressDetail cd on o.CustomerCode = cd.CustomerCode and cd.AddressType = '01' " +
                                " inner join RiderManagement rd on rd.SubDistrictCode = cd.subdistrict " +
                                " inner join Merchant m on cd.subdistrict = m.SubDistrictCode " +
                                " left join " + dbName + ".dbo.Customer c on  o.CustomerCode = c.CustomerCode" +
                                " left join " + dbName + ".dbo.Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join " + dbName + ".dbo.Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left join " + dbName + ".dbo.Lookup ot on o.OrderType = ot.LookupCode and ot.LookupType = 'ORDERTYPE'" +
                                " left join District dt on dt.DistrictCode = cd.district " +
                                " left join SubDistrict sdt on sdt.SubDistrictCode = cd.subdistrict " +
                                " where o.OrderStatusCode = '01' and o.OrderStatusCode = '01'" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }
        public List<OrderListReturn> ListRiderOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.RiderCode != null) && (oInfo.RiderCode != ""))
            {
                strcond += " and  rd.Rider_code like '%" + oInfo.RiderCode + "%'";
            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = "select distinct o.*,c.CustomerFName,c.CustomerLName,rd.Rider_code,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName,ot.LookupValue as OrderTypeName," +
                                "dt.DistrictName,sdt.SubDistrictName " +
                                " from OrderInfo o " +
                                " inner join " + dbName + ".dbo.CustomerAddressDetail cd on o.CustomerCode = cd.CustomerCode and cd.AddressType = '01' " +
                                " inner join " + dbName + ".dbo.RiderManagement rd on rd.SubDistrictCode = cd.subdistrict " +
                                " inner join " + dbName + ".dbo.Merchant m on cd.subdistrict = m.SubDistrictCode " +
                                " left join " + dbName + ".dbo.Customer c on  o.CustomerCode = c.CustomerCode" +
                                " left join " + dbName + ".dbo.Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join " + dbName + ".dbo.Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left join " + dbName + ".dbo.Lookup ot on o.OrderType = ot.LookupCode and ot.LookupType = 'ORDERTYPE'" +
                                " left join " + dbName + ".dbo.District dt on dt.DistrictCode = cd.district " +
                                " left join " + dbName + ".dbo.SubDistrict sdt on sdt.SubDistrictCode = cd.subdistrict " +
                                " where o.OrderStatusCode = '01' and o.OrderStatusCode = '01'" + strcond;

                strsql += " ORDER BY o.Id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderTypeName = dr["OrderTypeName"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                              RiderCode = dr["Rider_code"].ToString(),
                              DistrictName = dr["DistrictName"].ToString(),
                              SubDistrictName = dr["SubDistrictName"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderListReturn> ListRiderNoPagingByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.RiderCode != null) && (oInfo.RiderCode != ""))
            {
                strcond += " and  rm.Rider_code like '%" + oInfo.RiderCode + "%'";
            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = " select rm.Rider_code,rm.Rider_name,m.MerchantName,m.Lat,m.Long from RiderManagement rm " +
                                " inner join Merchant m on rm.SubDistrictCode = m.SubDistrictCode " +
                                " where rm.FlagDelete = 'N' and m.FlagDelete = 'N'" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              RiderCode = dr["Rider_code"].ToString(),
                              Lat = double.Parse(dr["Lat"].ToString()),
                              Long = double.Parse(dr["Long"].ToString()),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListFulfilOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.RiderCode != null) && (oInfo.RiderCode != ""))
            {
                strcond += " and  rd.Rider_code like '%" + oInfo.RiderCode + "%'";
            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = "select distinct o.*,c.CustomerFName,c.CustomerLName,rd.Rider_code,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName,ot.LookupValue as OrderTypeName," +
                                "dt.DistrictName,sdt.SubDistrictName " +
                                " from OrderInfo o " +
                                " inner join " + dbName + ".dbo.CustomerAddressDetail cd on o.CustomerCode = cd.CustomerCode and cd.AddressType = '01' " +
                                " inner join " + dbName + ".dbo.RiderManagement rd on rd.SubDistrictCode = cd.subdistrict " +
                                " inner join " + dbName + ".dbo.Merchant m on cd.subdistrict = m.SubDistrictCode " +
                                " left join " + dbName + ".dbo.Customer c on  o.CustomerCode = c.CustomerCode" +
                                " left join " + dbName + ".dbo.Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join " + dbName + ".dbo.Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left join " + dbName + ".dbo.Lookup ot on o.OrderType = ot.LookupCode and ot.LookupType = 'ORDERTYPE'" +
                                " left join " + dbName + ".dbo.District dt on dt.DistrictCode = cd.district " +
                                " left join " + dbName + ".dbo.SubDistrict sdt on sdt.SubDistrictCode = cd.subdistrict " +
                                " where o.OrderStatusCode = '01' and o.OrderStatusCode = '01'" + strcond;

                strsql += " ORDER BY o.Id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderTypeName = dr["OrderTypeName"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                              RiderCode = dr["Rider_code"].ToString(),
                              DistrictName = dr["DistrictName"].ToString(),
                              SubDistrictName = dr["SubDistrictName"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListMerchantAroundRiderMapNoPagingByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.RiderCode != null) && (oInfo.RiderCode != ""))
            {
                strcond += "where rd.Rider_code like '%" + oInfo.RiderCode + "%'";
            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                

                string strsql = " select rd.Rider_code,rd.Rider_name,mc.MerchantName,dt.DistrictName,sdt.SubDistrictName,mc.Lat,mc.Long from RiderManagement rd " +
                                " left join Merchant mc on mc.DistrictCode = rd.DistrictCode " +
                                " left join District dt on dt.DistrictCode = rd.DistrictCode " +
                                " left join SubDistrict sdt on sdt.SubDistrictCode = rd.SubDistrictCode " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              RiderCode = dr["Rider_code"].ToString(),
                              MerchantName = dr["MerchantName"].ToString().Trim(),
                              Lat = (dr["Lat"].ToString().Trim() != "") ? Convert.ToDouble(dr["Lat"]) : 0,
                              Long = (dr["Long"].ToString().Trim() != "") ? Convert.ToDouble(dr["Long"]) : 0,
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListFulfilOrderDetailByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            if ((oInfo.Month != null) && (oInfo.Month != ""))
            {
                strcond += " where MONTH(o.CreateDate) like '" + int.Parse(oInfo.Month) + "'";
            }
            if ((oInfo.Year != null) && (oInfo.Year != ""))
            {
                strcond += " and YEAR(o.CreateDate) like '" + int.Parse(oInfo.Year) + "'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = "select count(o.id) as Amount,CONVERT(VARCHAR(10),CONVERT(DATE, o.CreateDate, 103),103) as Date,MONTH(o.CreateDate) as month, YEAR(o.CreateDate) as year from OrderInfo o " + strcond +
                                "group by CONVERT(DATE, o.CreateDate, 103),MONTH(o.CreateDate),YEAR(o.CreateDate) ";

                strsql += " ORDER BY CONVERT(DATE, o.CreateDate, 103) DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderDate = dr["Date"].ToString(),
                              Amount = dr["Amount"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListPickingByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = " select  o.OrderCode,od.ProductCode,p.ProductName,od.Amount,od.TotalPrice,m.MerchantCode,m.MerchantName from OrderInfo o " +
                                " inner join orderdetail od on od.OrderCode = o.OrderCode " +
                                " inner join Product p on od.ProductCode = p.ProductCode " +
                                " inner join Merchant m on o.MerchantCode = m.MerchantCode  " +
                                " where CONVERT(VARCHAR(10), o.CreateDate, 103) = '" + oInfo.OrderDate + "'";
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString(),
                              Amount = dr["Amount"].ToString(),
                              ProductCode = dr["ProductCode"].ToString(),
                              ProductName = dr["ProductName"].ToString(),
                              MerchantCode = dr["MerchantCode"].ToString(),
                              MerchantName = dr["MerchantName"].ToString(),
                              TotalPrice = dr["TotalPrice"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListOrderFulfillByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select distinct o.OrderCode,m.MerchantCode,m.MerchantName from OrderInfo o " +
                                " inner join Merchant m on o.MerchantCode = m.MerchantCode " +
                                " where CONVERT(VARCHAR(10), o.CreateDate, 103) = '" + oInfo.OrderDate + "'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString(),
                              MerchantCode = dr["MerchantCode"].ToString(),
                              MerchantName = dr["MerchantName"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int InsertOrderMapRider(OrderInfo oInfo)
        {
            int i = 0;

            string strsql = " insert into OrderMapRider(Orderinfo,Rider_code,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                                " values (" +
                                "'" + oInfo.OrderCode + "'," +
                                "'" + oInfo.RiderCode + "'," +
                                "GETDATE()," +
                                "'" + oInfo.CreateBy + "'," +
                                "GETDATE()," +
                                "'" + oInfo.UpdateBy + "'," +
                                "'N')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<OrderListReturn> ListCancelOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.shipmentdate != null) && (oInfo.shipmentdate != ""))
            {
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + oInfo.shipmentdate + "')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName ,o.shipmentdate,o.orderlistdate from " + dbName + ".dbo.OrderInfo o " +
                                " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where (1=1)" + strcond;

                strsql += " ORDER BY o.OrderCode DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              shipmentdate = dr["shipmentdate"].ToString().Trim(),
                              OrderListDate = dr["OrderListDate"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? countListCancelOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.shipmentdate != null) && (oInfo.shipmentdate != ""))
            {
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + oInfo.shipmentdate + "')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " select count(o.Id) as countOrder from " + dbName + ".dbo.OrderInfo o " +
                                " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where (1=1)" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }

        public List<OrderListReturn> ListAppointmentOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName ,o.shipmentdate,o.orderlistdate from " + dbName + ".dbo.OrderInfo o " +
                                " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where o.OrderStatusCode = '02' and o.OrderStateCode = '01' " + strcond;

                strsql += " ORDER BY o.OrderCode DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              OrderListDate = dr["OrderListDate"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? countListAppointmentOrderByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " select count(o.Id) as countOrder from " + dbName + ".dbo.OrderInfo o " +
                                " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where o.OrderStatusCode = '02' and o.OrderStateCode = '01' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }
        public int UpdateOrderApprove(OrderInfo oInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.OrderInfo set OrderStatusCode = '02' where OrderCode in ('" + oInfo.OrderCode + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateOrderReject(OrderInfo oInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.OrderInfo set OrderStatusCode = '03' where OrderCode in ('" + oInfo.OrderCode + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateOrderTrackinginOrderInfo(String OrderTracking, String oCode)
        {
            int i = 0;

            string strcond = "";



            string strsql = " Update " + dbName + ".dbo.OrderInfo set " + " OrderTracking = '" + OrderTracking + "'" +
                     " where OrderCode ='" + oCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<OrderListReturn> ListStampPeriodByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((oInfo.ShipmentDateFrom != "") && (oInfo.ShipmentDateFrom != null)) && ((oInfo.ShipmentDateTo != "") && (oInfo.ShipmentDateTo != null)))
            {
                strcond += " and  o.shipmentdate BETWEEN CONVERT(VARCHAR, '" + oInfo.ShipmentDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.ShipmentDateFrom + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql = " SELECT o.Id,o.OrderCode,l.LookupValue as OrderStatusName,c.CustomerFName,c.CustomerLName,o.CreateDate,o.shipmentdate FROM OrderInfo as o  " +
                                " LEFT JOIN Lookup as l on l.LookupCode = o.OrderStatusCode and l.LookupType = 'ORDERSTATUS' " +
                                " LEFT JOIN Customer as c on c.CustomerCode = o.CustomerCode " +
                                " where o.OrderStatusCode = '04' and o.OrderStateCode = '02' and o.confirmno is null " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              shipmentdate = dr["shipmentdate"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? CountListStampPeriodByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if (((oInfo.CreateDateFrom != "") && (oInfo.CreateDateFrom != null)) && ((oInfo.CreateDateTo != "") && (oInfo.CreateDateTo != null)))
            {
                strcond += " and  o.CreateDate BETWEEN CONVERT(VARCHAR, '" + oInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.CreateDateTo + "', 103)),'23:59:59')";
            }
            if (((oInfo.ShipmentDateFrom != "") && (oInfo.ShipmentDateFrom != null)) && ((oInfo.ShipmentDateTo != "") && (oInfo.ShipmentDateTo != null)))
            {
                strcond += " and  o.shipmentdate BETWEEN CONVERT(VARCHAR, '" + oInfo.ShipmentDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + oInfo.ShipmentDateFrom + "', 103)),'23:59:59')";
            }
            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " SELECT count(o.Id) as countOrder FROM OrderInfo as o  " +
                                " LEFT JOIN Lookup as l on l.LookupCode = o.OrderStatusCode and l.LookupType = 'ORDERSTATUS' " +
                                " LEFT JOIN Customer as c on c.CustomerCode = o.CustomerCode " +
                                " where o.OrderStatusCode = '04' and o.OrderStateCode = '02' and o.confirmno is null " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }

        public int? MaxIndexPeriodDay(OrderInfo oInfo)
        {
            string strcond = "";
            int? periodday = 0;

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                

                string strsql = " SELECT MAX(o.confirmno) AS confirmno FROM OrderInfo o " +
                                " WHERE o.OrderListDate BETWEEN CONVERT(DATE,GETDATE()) AND GETDATE() ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              IndexPeriodDay = (dr["confirmno"].ToString() != "") ? Convert.ToInt32(dr["confirmno"]) : 0,
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                periodday = LOrder[0].IndexPeriodDay;
            }

            return periodday;
        }

        public int UpdateConfirmNo(OrderInfo oInfo)
        {
            int i = 0;

            

            string strsql = " UPDATE o SET " +
                            " o.confirmno = " + oInfo.IndexPeriodDay + "," +
                            " o.OrderListDate = " + "GETDATE() " + "," +
                            " o.UpdateDate = " + "GETDATE()" + "" +
                            " FROM OrderInfo AS o  " +
                            " LEFT OUTER JOIN Lookup as l on l.LookupCode = o.OrderStatusCode and l.LookupType = 'ORDERSTATUS' " +
                            " LEFT JOIN Customer as c on c.CustomerCode = o.CustomerCode  where o.OrderStatusCode = '04' and o.OrderStateCode = '02' and o.confirmno is null and o.Id = " + oInfo.OrderId;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountOrderManagementListByCriteria(OrderInfo oInfo)
        {
            string strcond = " WHERE o.OrderSituation = '01' ";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderSituation = '01' and o.OrderStatusCode in (" + oInfo.OrderStatusCode + ")" : strcond += " and  o.OrderStatusCode in (" + oInfo.OrderStatusCode + ")";
            }

            if ((oInfo.CreateDate != null) && (oInfo.CreateDate != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.ContactTel like '%" + oInfo.CustomerContact + "%'" : strcond += " and  c.ContactTel like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.BranchCode != null) && (oInfo.BranchCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.BranchCode = '" + oInfo.BranchCode + "'" : strcond += " and  o.BranchCode = '" + oInfo.BranchCode + "'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.ConfirmNo == "NULL") && (oInfo.ConfirmNo != ""))
            {
             
            }

            if ((oInfo.GroupOrderStatusCode != null) && (oInfo.GroupOrderStatusCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode In (" + oInfo.GroupOrderStatusCode + ")" : strcond += " and  o.OrderStatusCode In (" + oInfo.GroupOrderStatusCode + ")";
            }
            if ((oInfo.GroupOrderStateCode != null) && (oInfo.GroupOrderStateCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode In (" + oInfo.GroupOrderStateCode + ")" : strcond += " and  o.OrderStateCode In (" + oInfo.GroupOrderStateCode + ")";
            }
            if ((oInfo.InventoryCode != null) && (oInfo.InventoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.InventoryCode = '" + oInfo.InventoryCode + "'" : strcond += " and  o.InventoryCode = '" + oInfo.InventoryCode + "'";
            }
            if ((oInfo.MerchantMapCode != null) && (oInfo.MerchantMapCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'" : strcond += " and  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'";
            }

            DataTable dt = new DataTable();
            var LVehicle = new List<OrderListReturn>();


            try
            {
                string strsql =
                    "select count(o.Id) as countOrder " +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT OUTER JOIN Customer AS c ON o.CustomerCode = c.CustomerCode " +
                    " LEFT OUTER JOIN Lookup AS s ON o.OrderStatusCode = s.LookupCode AND s.LookupType = 'ORDERSTATUS' " +
                    " LEFT OUTER JOIN Lookup AS SRT ON o.SALEORDERTYPE = s.LookupCode AND s.LookupType = 'SALEORDERTYPE' " +
                    " LEFT OUTER JOIN Lookup AS sos ON o.OrderStateCode = sos.LookupCode AND sos.LookupType = 'ORDERSTATE' " +
                    " LEFT OUTER JOIN Channel AS cn ON o.ChannelCode = cn.ChannelCode AND cn.FlagDelete = 'N' " +
                    " LEFT OUTER JOIN Branch AS b ON o.BranchCode = b.BranchCode " +
                    " LEFT OUTER JOIN CampaignCategory AS cc ON o.CampaignCategoryCode = cc.CampaignCategoryCode " +
                    " LEFT OUTER JOIN CustomerPhone AS cp ON o.CustomerCode  = cp.CustomerCode and cp.PhoneNumber = o.CustomerPhone" +
                 " inner join OrderTransport odt on  o.OrderCode =odt.OrderCode and odt.AddressType ='01' " +
                    " left join Inventory i on odt.InventoryCode = i.InventoryCode" +
                      "  left join ( select count (odback.id) AmountBackOrder,odback.OrderCode from OrderDetail odback" +
                    " left join InventoryDetail ivdback on ivdback.InventoryCode = odback.InventoryCode and odback.ProductCode=ivdback.ProductCode" +
                            

                    " where  odback.Amount>ivdback.QTY group by odback.OrderCode ) orderback on orderback.OrderCode =o.OrderCode" +
                     "  inner join Merchant mer on mer.MerchantCode = o.MerchantMapCode " +
                    " " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LVehicle = (from DataRow dr in dt.Rows

                            select new OrderListReturn()
                            {
                                countOrder = Convert.ToInt32(dr["countOrder"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LVehicle.Count > 0)
            {
                count = LVehicle[0].countOrder;
            }

            return count;
        }

        public List<OrderListReturn> ListOrderManagementByCriteria_showgv(OrderInfo oInfo)
        {
            string strcond = " WHERE o.OrderSituation = '01' ";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
              

                strcond = strcond == "" ? strcond += " WHERE o.OrderSituation = '01' and o.OrderStatusCode in (" + oInfo.OrderStatusCode + ")" : strcond += " and  o.OrderStatusCode in (" + oInfo.OrderStatusCode + ")";
            }

            if ((oInfo.CreateDate != null) && (oInfo.CreateDate != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.ContactTel like '%" + oInfo.CustomerContact + "%'" : strcond += " and  c.ContactTel like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.BranchCode != null) && (oInfo.BranchCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.BranchCode = '" + oInfo.BranchCode + "'" : strcond += " and  o.BranchCode = '" + oInfo.BranchCode + "'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                //strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";

                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode in ( " + oInfo.OrderStateCode + ")" : strcond += " and  o.OrderStateCode in (" + oInfo.OrderStateCode + ")";

            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.ConfirmNo == "NULL") && (oInfo.ConfirmNo != ""))
            {
        //        strcond = strcond == "" ? strcond += " WHERE  o.ConfirmNo is null" : strcond += " and  o.ConfirmNo is null";
            }
            if ((oInfo.GroupOrderStatusCode != null) && (oInfo.GroupOrderStatusCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode In (" + oInfo.GroupOrderStatusCode + ")" : strcond += " and  o.OrderStatusCode In (" + oInfo.GroupOrderStatusCode + ")";
            }
            if ((oInfo.GroupOrderStateCode != null) && (oInfo.GroupOrderStateCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode In (" + oInfo.GroupOrderStateCode + ")" : strcond += " and  o.OrderStateCode In (" + oInfo.GroupOrderStateCode + ")";
            }
            if ((oInfo.InventoryCode != null) && (oInfo.InventoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.InventoryCode = '" + oInfo.InventoryCode + "'" : strcond += " and  o.InventoryCode = '" + oInfo.InventoryCode + "'";
            }

            if ((oInfo.MerchantMapCode != null) && (oInfo.MerchantMapCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'" : strcond += " and  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'";
            }
            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql =
                    "SELECT  DISTINCT c.CustomerFName, c.CustomerLName,cp.PhoneNumber, o.Id, o.OrderCode, o.OrderType, o.OrderStatusCode, o.OrderStateCode, o.BUCode, o.SubTotalPrice, o.TotalPrice, o.Vat, o.CreateDate, o.CreateBy, o.UpdateDate, o.UpdateBy, o.FlagDelete," +
                    "o.CustomerCode, o.RunningNo, o.CompanyFrom, o.CompanyTo, o.Transport, o.Certificate, o.confirmno, o.shipmentdate, o.OrderNote, o.OrderListDate, o.BranchCode,b.BranchName, o.OrderRef, o.ChannelCode,cn.ChannelName, o.ReferalRequestCode," +
                    "o.DeliveryDate, o.ReceiveDate, o.OrderTracking, o.AssignTo, s.LookupValue AS OrderStatusName, o.shipmentdate AS Expr1,o.SALEORDERTYPE,sot.LookupValue as SaleOrderTypeName,sos.LookupValue AS OrderStateName, cc.CamCate_name AS CamCateName, o.BranchOrderID," +
                    " o.OrderRejectRemark,c.ContactTel ,i.InventoryName,i.InventoryCode,case when orderback.AmountBackOrder is not null then orderback.AmountBackOrder else 0 end as backorder,o.ordertracking,mer.MerchantName" +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT OUTER JOIN Customer AS c ON o.CustomerCode = c.CustomerCode " +
                    " LEFT OUTER JOIN Lookup AS s ON o.OrderStatusCode = s.LookupCode AND s.LookupType = 'ORDERSTATUS' " +
                    " LEFT OUTER JOIN Lookup AS sot ON o.SALEORDERTYPE = sot.LookupCode AND sot.LookupType = 'SALEORDERTYPE' " +
                    " LEFT OUTER JOIN Lookup AS sos ON o.OrderStateCode = sos.LookupCode AND sos.LookupType = 'ORDERSTATE' " +
                    " LEFT OUTER JOIN Channel AS cn ON o.ChannelCode = cn.ChannelCode AND cn.FlagDelete = 'N' " +
                    " LEFT OUTER JOIN Branch AS b ON o.BranchCode = b.BranchCode " +
                    " LEFT OUTER JOIN CampaignCategory AS cc ON o.CampaignCategoryCode = cc.CampaignCategoryCode " +
                    " LEFT OUTER JOIN CustomerPhone AS cp ON o.CustomerCode  = cp.CustomerCode and cp.PhoneNumber = o.CustomerPhone" +
                    " inner join OrderTransport odt on  o.OrderCode =odt.OrderCode " +
                    " left join Inventory i on o.InventoryCode = i.InventoryCode " +
                    "  left join ( select count (odback.id) AmountBackOrder,odback.OrderCode from OrderDetail odback" +
                    " inner join InventoryDetail ivdback on ivdback.InventoryCode = odback.InventoryCode and odback.ProductCode=ivdback.ProductCode" +
                  
                    " where  odback.Amount>ivdback.QTY group by odback.OrderCode ) orderback on orderback.OrderCode =o.OrderCode" +
                      "  inner join Merchant mer on mer.MerchantCode = o.MerchantMapCode " +

                    " " +
                    "  " + strcond;

                strsql += " ORDER BY o.id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString(),
                              CustomerCode = dr["CustomerCode"].ToString(),
                              CustomerFName = dr["CustomerFName"].ToString(),
                              CustomerLName = dr["CustomerLName"].ToString(),
                              CustomerName = dr["CustomerFName"].ToString() + " " + dr["CustomerLName"].ToString(),
                              CustomerContact = (dr["ContactTel"].ToString() != null) ? dr["ContactTel"].ToString() : dr["PhoneNumber"].ToString(),
                              OrderStatusName = dr["OrderStatusName"].ToString(),
                              OrderNote = dr["OrderNote"].ToString(),
                              ChannelCode = dr["ChannelCode"].ToString(),
                              ChannelName = dr["ChannelName"].ToString(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                              BranchCode = dr["BranchCode"].ToString(),
                              BranchName = dr["BranchName"].ToString(),
                              SALEORDERTYPE = dr["SALEORDERTYPE"].ToString(),
                              SaleOrderTypeName = dr["SaleOrderTypeName"].ToString(),
                              DeliveryDate = dr["DeliveryDate"].ToString(),
                              CampaignCategoryName = dr["CamCateName"].ToString(),
                              BranchOrderID = dr["BranchOrderID"].ToString(),
                              OrderRejectRemark = dr["OrderRejectRemark"].ToString(),
                              OrderStateName = dr["OrderStateName"].ToString(),
                              InventoryName = dr["InventoryName"].ToString(),
                              InventoryCode = dr["InventoryCode"].ToString(),
                              BackOrder = dr["backorder"].ToString(),
                              OrderTrackingNo = dr["ordertracking"].ToString(),
                              MerchantName = dr["MerchantName"].ToString(),
                              OrderStatusCode= dr["OrderStatusCode"].ToString(),
                              

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListOrderManagementNoPagingByCriteria(OrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CreateDate != null) && (oInfo.CreateDate != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }

            if ((oInfo.BranchCode != null) && (oInfo.BranchCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.BranchCode = '" + oInfo.BranchCode + "'" : strcond += " and  o.BranchCode = '" + oInfo.BranchCode + "'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql =
                    "SELECT distinct c.CustomerFName, c.CustomerLName,cp.PhoneNumber, o.Id, o.OrderCode, o.OrderType, o.OrderStatusCode, o.OrderStateCode, o.BUCode, o.SubTotalPrice, o.TotalPrice, o.Vat, o.CreateDate, o.CreateBy, o.UpdateDate, o.UpdateBy, o.FlagDelete," +
                    "o.CustomerCode, o.RunningNo, o.CompanyFrom, o.CompanyTo, o.Transport, o.Certificate, o.confirmno, o.shipmentdate, o.OrderNote, o.OrderListDate, o.BranchCode,b.BranchName, o.OrderRef, o.ChannelCode,cn.ChannelName, o.ReferalRequestCode," +
                    "o.DeliveryDate, o.ReceiveDate, o.OrderNote,o.OrderTracking, o.AssignTo, s.LookupValue AS OrderStatusName, " +
                    "o.shipmentdate AS Expr1,o.SALEORDERTYPE,sot.LookupValue as SaleOrderTypeName,sos.LookupValue AS ORDERSTATE," +
                    " cc.CamCate_name AS CamCateName, o.BranchOrderID, o.OrderRejectRemark, ots.TransportPrice,odp.PaymentTypeCode,OrderTracking, " +
                    "opay.LookupValue as paymenttype ,i.InventoryName,i.InventoryCode,o.vat,o.PercentVat" +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT OUTER JOIN Customer AS c ON o.CustomerCode = c.CustomerCode " +
                    " LEFT OUTER JOIN Lookup AS s ON o.OrderStatusCode = s.LookupCode AND s.LookupType = 'ORDERSTATUS' " +
                    " LEFT OUTER JOIN Lookup AS sot ON o.SALEORDERTYPE = sot.LookupCode AND sot.LookupType = 'SALEORDERTYPE' " +
                    " LEFT OUTER JOIN Lookup AS sos ON o.OrderStateCode = sos.LookupCode AND sos.LookupType = 'ORDERSTATE' " +
                    " LEFT OUTER JOIN Channel AS cn ON o.ChannelCode = cn.ChannelCode AND cn.FlagDelete = 'N' " +
                    " LEFT OUTER JOIN Branch AS b ON o.BranchCode = b.BranchCode " +
                    " LEFT OUTER JOIN CampaignCategory AS cc ON o.CampaignCategoryCode = cc.CampaignCategoryCode " +
                    " LEFT OUTER JOIN CustomerPhone AS cp ON o.CustomerCode  = cp.CustomerCode and cp.PhoneNumber = o.CustomerPhone " +
                    "  LEFT OUTER JOIN OrderPayment odp on odp.OrderCode = o.OrderCode  " +
                    " LEFT OUTER JOIN Lookup AS opay ON opay.LookupCode = odp.PaymentTypeCode aND opay.LookupType = 'PAYMENTMETHOD'  " +
                    " inner join OrderTransport odt on  o.OrderCode =odt.OrderCode " +
                    " left join Inventory i on odt.InventoryCode = i.InventoryCode " +
                    " inner join OrderTransport ots on ots.OrderCode =o.OrderCode " +
                
                    "" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              OrderCode = dr["OrderCode"].ToString(),
                              CustomerCode = dr["CustomerCode"].ToString(),
                              CustomerFName = dr["CustomerFName"].ToString(),
                              CustomerLName = dr["CustomerLName"].ToString(),
                              CustomerName = dr["CustomerFName"].ToString() + " " + dr["CustomerLName"].ToString(),
                              CustomerContact = dr["PhoneNumber"].ToString(),
                              OrderStatusName = dr["OrderStatusName"].ToString(),
                              OrderStateName = dr["ORDERSTATE"].ToString(),
                              OrderNote = dr["OrderNote"].ToString(),
                              ChannelCode = dr["ChannelCode"].ToString(),
                              ChannelName = dr["ChannelName"].ToString(),
                              CreateBy = dr["CreateBy"].ToString(),
                              CreateDate = dr["CreateDate"].ToString(),
                              UpdateBy = dr["UpdateBy"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                              FlagDelete = dr["FlagDelete"].ToString(),
                              BranchCode = dr["BranchCode"].ToString(),
                              BranchName = dr["BranchName"].ToString(),
                              SALEORDERTYPE = dr["SALEORDERTYPE"].ToString(),
                              SaleOrderTypeName = dr["SaleOrderTypeName"].ToString(),
                              DeliveryDate = dr["DeliveryDate"].ToString(),
                              CampaignCategoryName = dr["CamCateName"].ToString(),
                              BranchOrderID = dr["BranchOrderID"].ToString(),
                              OrderRejectRemark = dr["OrderRejectRemark"].ToString(),
                              TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToDecimal(dr["TransportPrice"]) : 0,
                              PaymentName=dr["paymenttype"].ToString(),
                              InventoryName = dr["InventoryName"].ToString(),
                              vat = dr["vat"].ToString(),
                              PercentVat = (dr["PercentVat"].ToString() != "") ? Convert.ToInt16(dr["PercentVat"]) : 0,
                              OrderTrackingNo = dr["OrderTracking"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? sumAmoutOrderDetailByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE od.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  od.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.FlagDelete != null) && (oInfo.FlagDelete != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE od.FlagDelete = '" + oInfo.FlagDelete + "'" : strcond += " AND od.FlagDelete = '" + oInfo.FlagDelete + "'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " SELECT sum(Amount) as sumAmount " +
                                " FROM " + dbName + ".dbo.OrderDetail AS od " + strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              sumAmount = Convert.ToInt32(dr["sumAmount"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].sumAmount;
            }

            return count;
        }

        public int? sumTotalPriceOrderDetailByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE od.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  od.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.FlagDelete != null) && (oInfo.FlagDelete != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE od.FlagDelete = '" + oInfo.FlagDelete + "'" : strcond += " AND od.FlagDelete = '" + oInfo.FlagDelete + "'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " SELECT sum(TotalPrice) as sumTotalPrice " +
                                " FROM " + dbName + ".dbo.OrderDetail AS od " + strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              sumTotalPrice = Convert.ToInt32(dr["sumTotalPrice"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].sumTotalPrice;
            }

            return count;
        }
        public List<OrderOLInfo> DtOl(OrderInfo Oinfo)
        {
            string strcond = "";

            if ((Oinfo.ConfirmNo != null) && (Oinfo.ConfirmNo != ""))
            {
                strcond += " and o.confirmno ='" + Oinfo.ConfirmNo + "'";
            }

            if ((Oinfo.OrderListDate != null) && (Oinfo.OrderListDate != ""))
            {
                strcond += " AND (CONVERT(Date, o.OrderListDate) = '" + Oinfo.OrderListDate + "')";
            }
            var LOrder = new List<OrderOLInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT        o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate, d.ProductCode, p.ProductName, " +
                            "d.Amount, u.LookupValue AS UnitName, d.NetPrice,  o.confirmno as confirmno" +
                            " from " + dbName + ".dbo.OrderDetail d" +
                            " left join OrderInfo AS o ON o.OrderCode = d.OrderCode " +
                            " left join Product AS p ON p.ProductCode = d.ProductCode " +
                            " left join Promotion AS pr ON pr.PromotionCode = d.PromotionCode " +
                            " left join Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            " left join CustomerAddressDetail AS cd ON cd.CustomerCode = c.CustomerCode" +
                                     
                                     " where  1=1 " +
                                     strcond +
                                     " group by  o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate, d.ProductCode, p.ProductName, " +
                                        " d.Amount, u.LookupValue, d.NetPrice, o.confirmno --,pay.PaymentTypeName ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderOLInfo()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              Amount = dr["Amount"].ToString().Trim(),
                              UnitName = dr["UnitName"].ToString().Trim(),
                              NetPrice = dr["NetPrice"].ToString().Trim(),
                              confirmno = dr["confirmno"].ToString().Trim(),
                              
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderOLInfo> DtOlExcel(OrderInfo Oinfo)
        {
            string strcond = "";

            if ((Oinfo.ConfirmNo != null) && (Oinfo.ConfirmNo != ""))
            {
                strcond += " and o.confirmno ='" + Oinfo.ConfirmNo + "'";
            }

            if ((Oinfo.OrderListDate != null) && (Oinfo.OrderListDate != ""))
            {
                strcond += " AND (CONVERT(Date, o.OrderListDate) = '" + Oinfo.OrderListDate + "')";
            }
            var LOrder = new List<OrderOLInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT        o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate, d.ProductCode, p.ProductName, " +
                            "d.Amount, u.LookupValue AS UnitName, d.NetPrice,  o.confirmno as confirmno" +
                            " from " + dbName + ".dbo.OrderDetail d" +
                            " left join OrderInfo AS o ON o.OrderCode = d.OrderCode " +
                            " left join Product AS p ON p.ProductCode = d.ProductCode " +
                            " left join Promotion AS pr ON pr.PromotionCode = d.PromotionCode " +
                            " left join Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            " left join CustomerAddressDetail AS cd ON cd.CustomerCode = c.CustomerCode" +

                                     " where  1=1 " +
                                     strcond +
                                     " group by  o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate, d.ProductCode, p.ProductName, " +
                                        " d.Amount, u.LookupValue, d.NetPrice, o.confirmno";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderOLInfo()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              Amount = dr["Amount"].ToString().Trim(),
                              
                              NetPrice = dr["NetPrice"].ToString().Trim(),
                              confirmno = dr["confirmno"].ToString().Trim(),
                              
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderOLInfo> LogfileExcelReport(OrderOLInfo oInfo)
        {
            string strcond = "";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CreateDate != null) && (oInfo.CreateDate != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }



            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderOLInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT  distinct  d.id,o.OrderCode,o.OrderStatusCode,o.OrderStateCode, c.CustomerFName+' '+ c.CustomerLName as customername " +
                    ",c.Gender,FORMAT (c.BirthDate, 'dd/MM/yyyy') as CustomerBirthdate" +
                    ",(select DATEDIFF(year,convert(varchar, c.BirthDate, 121),convert(varchar, getdate(), 121))) as Age" +
                    ",e.EmpFname_TH + ' ' + e.EmpLName_TH as Sale_Name, FORMAT (o.CreateDate, 'dd/MM/yyyy') as Saledate , d.ProductCode,p.sku, p.ProductName, d.Amount, " +
                    " u.LookupValue AS UnitName" +
                    ",cast(d.NetPrice as decimal(10,2))  NetPrice" +
                    ", o.confirmno, c.ContactTel, c.CustomerCode, FORMAT ( o.ReceiveDate, 'dd/MM/yyyy') as ReceiveDate,FORMAT ( o.DeliveryDate, 'dd/MM/yyyy') as DeliveryDate " +
                    " ,(select top(1) cad.address from OrderTransport cad where cad.CustomerCode=c.CustomerCode and cad.OrderCode = o.OrderCode ) as addressDatial," +
                    " (select top(1) sd.SubDistrictName from SubDistrict as sd left join OrderTransport as odt on odt.subdistrict = sd.SubDistrictCode where odt.CustomerCode = c.CustomerCode and odt.OrderCode = o.OrderCode ) as SubDistrictName," +
                    " (select top(1) d.DistrictName from District as d left join OrderTransport as odt on odt.district = d.DistrictCode where odt.CustomerCode = c.CustomerCode and odt.OrderCode = o.OrderCode) as DistrictName, " +
                    " (select top(1) p.ProvinceName from Province as p left join OrderTransport as odt on odt.province = p.ProvinceCode where odt.CustomerCode = c.CustomerCode and odt.OrderCode = o.OrderCode) as ProvinceName, " +
                    " (select top(1) odt.zipcode from OrderTransport odt where odt.CustomerCode=c.CustomerCode and odt.OrderCode = o.OrderCode) as addressZipcode,ccr.CamCate_name,o.OrderNote" +
                    ",cast(otp.TransportPrice as decimal(10,2)) TransportPrice" +
                    " ,cast(o.TotalPrice as decimal(10,2)) TotalPrice, cast( o.TotalPrice -  otp.TransportPrice as decimal(10,2)) priceAmountproduct" +
                    "" +
                    ",LStatus.LookupValue as orderstatusname,LState.LookupValue as orderstatename,o.OrderTracking,pt.PaymentTypeName,c.taxid" +

                    "  ,(SELECT SUM(CAST(NetPrice AS decimal(10, 2) )*ol0.Amount) from OrderDetail ol0 where ol0.OrderCode =o.OrderCode ) as SumbyProductprice" +

                            " from " + dbName + ".dbo.OrderDetail d" +
                            " left join OrderInfo AS o ON o.OrderCode = d.OrderCode " +
                            " left join Product AS p ON p.ProductCode = d.ProductCode " +
                            " left join Promotion AS pr ON pr.PromotionCode = d.PromotionCode " +
                            " left join Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            "  LEFT OUTER JOIN      CampaignCategory AS ccr ON ccr.CampaignCategoryCode =  o.CampaignCategoryCode " +
                            "  inner join Lookup LStatus on LStatus.LookupCode = o.OrderStatusCode and LStatus.LookupType = 'ORDERSTATUS'  " +
                            " inner join Lookup LState on LState.LookupCode = o.OrderStateCode and LState.LookupType = 'ORDERSTATE'  " +
                            " left join Emp e on o.CreateBy = e.EmpCode" +
                            " inner join OrderPayment op on op.OrderCode=o.OrderCode " +
                            "  inner join PaymentType pt on op.PaymentTypeCode = pt.PaymentTypeCode " +
                            "  inner join OrderTransport otp on otp.OrderCode = o.OrderCode and otp.AddressType='01' " +
                            " " +

                                     
                                     strcond +
                                     " GROUP BY d.id,o.OrderCode, c.CustomerFName, c.CustomerLName,c.Gender,c.BirthDate, o.CreateDate, d.ProductCode, p.ProductName, " +
                                        " d.Amount, u.LookupValue, d.NetPrice, o.confirmno, c.ContactTel, c.CustomerCode, o.ReceiveDate, o.DeliveryDate,ccr.CamCate_name" +
                                        " ,e.EmpFname_TH , e.EmpLName_TH,o.OrderNote,otp.TransportPrice,o.TotalPrice,LStatus.LookupValue,LState.LookupValue ,o.OrderTracking,o.OrderStatusCode,o.OrderStateCode,pt.PaymentTypeName,c.taxid,p.sku" +
                                        "  ORDER BY o.OrderCode desc,d.id  " +
                                        "  " + strnumpage + " ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderOLInfo()
                          {
                              CreateDate = dr["Saledate"].ToString().Trim(),
                              sellName = dr["Sale_Name"].ToString().Trim(),
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CampaignCategoryName = dr["CamCate_name"].ToString().Trim(),
                              CustomerFName = dr["customername"].ToString().Trim(),
                              ContactTel = dr["ContactTel"].ToString().Trim(),
                              addresscustomerdetail = dr["addressDatial"].ToString().Trim() + " " + dr["SubDistrictName"].ToString().Trim() + " " + dr["DistrictName"].ToString().Trim() + " " + dr["ProvinceName"].ToString().Trim(),
                              addresscustomerdetailzipcode = dr["addressZipcode"].ToString().Trim(),
                              OrderNote = dr["OrderNote"].ToString().Trim(),
                              DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              SKU = (dr["sku"].ToString().Trim() != "") ? dr["sku"].ToString().Trim() : dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              NetPrice = (dr["NetPrice"].ToString().Trim() != "") ? dr["NetPrice"].ToString().Trim() : "0",
                              Amount = (dr["Amount"].ToString().Trim() != "") ? dr["Amount"].ToString().Trim() : "0",
                              TransportPrice = (dr["TransportPrice"].ToString().Trim() != "") ? dr["TransportPrice"].ToString().Trim() : "0",
                              orderTotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDecimal(dr["TotalPrice"].ToString().Trim()) : 0,
                              priceAmountproduct = dr["priceAmountproduct"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderTracking = dr["OrderTracking"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              ReceiveDate = dr["ReceiveDate"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              PaymentTypeName = dr["PaymentTypeName"].ToString().Trim(),
                              TaxId = dr["TaxId"].ToString().Trim(),
                              SumbyProductprice = dr["SumbyProductprice"].ToString().Trim(),
                              Gender = dr["Gender"].ToString().Trim(),
                              CustomerBirthdate = dr["CustomerBirthdate"].ToString().Trim(),
                              Age = dr["Age"].ToString().Trim(),


                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderPaymenttypeInfo> ReportPaymenttype(OrderPaymenttypeInfo oInfo)
        {
            string strcond = "where op.PaymentTypeCode = '02' and o.OrderSituation = '01' ";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CreateDate != null) && (oInfo.CreateDate != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }



            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderPaymenttypeInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select o.OrderCode,CONVERT(varchar, o.CreateDate,103) order_date , CONVERT(varchar, o.DeliveryDate,103) DeliveryDate"
                      + ",od.ProductCode,p.ProductName,od.CampaignCategoryCode" +
                      " ,od.Amount,od.Price,pm.PaymentTypeName,od.NetPrice"
                      + ",o.UpdateDate,  e.EmpCode as sale_Code,e.EmpFname_TH+' '+e.EmpLName_TH as Sale_Name ,od.SumPrice "
                      + ",cust.CustomerCode ,cust.CustomerFName+' '+cust.CustomerLName as customername "
                      + ",op.Installment,op.InstallmentPrice,op.FirstInstallment,luci.LookupValue as CardIssuename,op.CardNo,o.PercentVat,o.Vat"
                      + ",luct.LookupValue as CardType,op.CVCNo,op.CardHolderName,op.CardExpMonth,op.CardExpYear,op.CitizenId,op.BirthDate"
                      + ",(SELECT SUM(CAST(NetPrice AS decimal(10, 2) )*ol0.Amount) from OrderDetail ol0 where ol0.OrderCode =o.OrderCode ) as SumbyProductprice"
                      + ",cast(o.TotalPrice as decimal(10,2)) TotalPrice ,od.TransportPrice"

                      + " from OrderDetail od " +
                      " inner join OrderInfo o on o.OrderCode = od.OrderCode" +
                      " inner join emp e on e.EmpCode =o.CreateBy " +
                      " inner join emp eupdate on eupdate.EmpCode =o.UpdateBy" +
                      " left join Product p on od.ProductCode = p.ProductCode" +
                      " inner join Customer cust on o.CustomerCode = cust.CustomerCode" +
                      " inner join OrderPayment op on op.OrderCode = o.OrderCode" +
                      " inner join PaymentType pm on pm.PaymentTypeCode=op.PaymentTypeCode" +
                      " left join Lookup AS luci ON op.CardIssuename = luci.LookupCode and luci.LookupType = 'BANK'" +
                      " left join Lookup AS luct ON op.CardIssuename = luct.LookupCode and luct.LookupType = 'CARDTYPE' " 
                      + strcond
                      + " order by o.CreateDate desc " +
                      " ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderPaymenttypeInfo()
                          {
                              CreateDate = dr["order_date"].ToString().Trim(),
                              sellName = dr["Sale_Name"].ToString().Trim(),
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["customername"].ToString().Trim(),
                              DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              //SKU = (dr["sku"].ToString().Trim() != "") ? dr["sku"].ToString().Trim() : dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              NetPrice = (dr["NetPrice"].ToString().Trim() != "") ? dr["NetPrice"].ToString().Trim() : "0",
                              Amount = (dr["Amount"].ToString().Trim() != "") ? dr["Amount"].ToString().Trim() : "0",
                              Sumprice = (dr["SumPrice"].ToString().Trim() != "") ? dr["SumPrice"].ToString().Trim() : "0",
                              TransportPrice = (dr["TransportPrice"].ToString().Trim() != "") ? dr["TransportPrice"].ToString().Trim() : "0",
                              orderTotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDecimal(dr["TotalPrice"].ToString().Trim()) : 0,
                              SumbyProductprice = dr["SumbyProductprice"].ToString().Trim(),
                              
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              
                              PaymentTypeName = dr["PaymentTypeName"].ToString().Trim(),
                              
                              
                              CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                              //paymentmethod
                              Installment = (dr["Installment"].ToString().Trim() != "-99") ? dr["Installment"].ToString().Trim() : "",
                              InstallmentPrice = dr["InstallmentPrice"].ToString().Trim(),
                              FirstInstallment = dr["FirstInstallment"].ToString().Trim(),
                              CardIssuename = dr["CardIssuename"].ToString().Trim(),
                              CardNo = dr["CardNo"].ToString().Trim(),
                              CardType = dr["CardType"].ToString().Trim(),
                              CVCNo = dr["CVCNo"].ToString().Trim(),
                              CardHolderName = dr["CardHolderName"].ToString().Trim(),
                              CardExpMonth = dr["CardExpMonth"].ToString().Trim(),
                              CardExpYear = dr["CardExpYear"].ToString().Trim(),
                              CitizenId = dr["CitizenId"].ToString().Trim(),
                              BirthDate = dr["BirthDate"].ToString().Trim(),
                              PercentVat = dr["PercentVat"].ToString().Trim(),
                              Vat = dr["Vat"].ToString().Trim(),



                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderOLInfo> DtOlExcelReport(OrderOLInfo oInfo)
        {
            string strcond = " WHERE  o.OrderSituation = '01' ";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.MerchantMapCode != null) && (oInfo.MerchantMapCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.MerchantMapCode  = '" + oInfo.MerchantMapCode + "'" : strcond += " AND  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if ((oInfo.CreateDate != null) && (oInfo.CreateDate != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }



            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderOLInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT  distinct  d.id,o.OrderCode,o.OrderStatusCode,mc.MerchantName,o.OrderNote,e.Gender,o.OrderStateCode, c.CustomerFName+' '+ c.CustomerLName as customername ,e.EmpFname_TH + ' ' + e.EmpLName_TH as Sale_Name, FORMAT (o.CreateDate, 'dd/MM/yyyy') as Saledate , d.ProductCode,p.sku, p.ProductName, d.Amount, " +
                    " u.LookupValue AS UnitName,o.CreateDate,ch.ChannelName " +
                    ",cast(d.NetPrice as decimal(10,2))  NetPrice" +
                    ", o.confirmno, c.ContactTel, c.CustomerCode, FORMAT ( o.ReceiveDate, 'dd/MM/yyyy') as ReceiveDate,FORMAT ( o.DeliveryDate, 'dd/MM/yyyy') as DeliveryDate " +
                    " ,(select top(1) cad.address from OrderTransport cad where cad.CustomerCode=c.CustomerCode and cad.OrderCode = o.OrderCode ) as addressDatial," +
                    " (select top(1) sd.SubDistrictName from SubDistrict as sd left join OrderTransport as odt on odt.subdistrict = sd.SubDistrictCode where odt.CustomerCode = c.CustomerCode and odt.OrderCode = o.OrderCode ) as SubDistrictName," +
                    " (select top(1) d.DistrictName from District as d left join OrderTransport as odt on odt.district = d.DistrictCode where odt.CustomerCode = c.CustomerCode and odt.OrderCode = o.OrderCode) as DistrictName, " +
                    " (select top(1) p.ProvinceName from Province as p left join OrderTransport as odt on odt.province = p.ProvinceCode where odt.CustomerCode = c.CustomerCode and odt.OrderCode = o.OrderCode) as ProvinceName, " +
                    " (select top(1) odt.zipcode from OrderTransport odt where odt.CustomerCode=c.CustomerCode and odt.OrderCode = o.OrderCode) as addressZipcode,ccr.CamCate_name,o.OrderNote" +
                    ",cast(otp.TransportPrice as decimal(10,2)) TransportPrice" +
                    " ,cast(o.TotalPrice as decimal(10,2)) TotalPrice, cast( o.TotalPrice -  otp.TransportPrice as decimal(10,2)) priceAmountproduct" +
                    "" +
                    ",LStatus.LookupValue as orderstatusname,LState.LookupValue as orderstatename,o.OrderTracking,pt.PaymentTypeName,c.taxid" +

                    "  ,(SELECT SUM(CAST(NetPrice AS decimal(10, 2) )*ol0.Amount) from OrderDetail ol0 where ol0.OrderCode =o.OrderCode ) as SumbyProductprice" +

                            " from " + dbName + ".dbo.OrderDetail d" +
                            " left join OrderInfo AS o ON o.OrderCode = d.OrderCode " +
                            " left join Product AS p ON p.ProductCode = d.ProductCode " +
                            " left join Promotion AS pr ON pr.PromotionCode = d.PromotionCode " +
                            " left join Channel AS ch ON ch.ChannelCode = d.ChannelCode " +
                            " left join Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            "  LEFT OUTER JOIN      CampaignCategory AS ccr ON ccr.CampaignCategoryCode =  o.CampaignCategoryCode " +
                            "  inner join Lookup LStatus on LStatus.LookupCode = o.OrderStatusCode and LStatus.LookupType = 'ORDERSTATUS'  " +
                            " inner join Lookup LState on LState.LookupCode = o.OrderStateCode and LState.LookupType = 'ORDERSTATE'  " +
                            " left join Emp e on o.CreateBy = e.EmpCode" +
                            " inner join OrderPayment op on op.OrderCode=o.OrderCode " +
                            "  inner join PaymentType pt on op.PaymentTypeCode = pt.PaymentTypeCode " +
                            "  inner join OrderTransport otp on otp.OrderCode = o.OrderCode and otp.AddressType='01' " +
                            "  inner join Merchant mc on mc.MerchantCode = o.MerchantMapCode "+
                            " " +

                                     
                                     strcond +
                                     " GROUP BY d.id,o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate, d.ProductCode, p.ProductName,ch.ChannelName, " +
                                        " d.Amount, u.LookupValue, d.NetPrice, o.confirmno, c.ContactTel, c.CustomerCode, o.ReceiveDate, o.DeliveryDate,mc.MerchantName,e.Gender,ccr.CamCate_name" +
                                        " ,e.EmpFname_TH , e.EmpLName_TH,o.OrderNote,otp.TransportPrice,o.TotalPrice,LStatus.LookupValue,LState.LookupValue ,o.OrderTracking,o.OrderStatusCode,o.OrderStateCode,pt.PaymentTypeName,c.taxid,p.sku" +
                                        "  ORDER BY o.CreateDate desc " +
                                        "  " + strnumpage + " ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderOLInfo()
                          {
                              CreateDate = dr["Saledate"].ToString().Trim(),
                              sellName = dr["Sale_Name"].ToString().Trim(),
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CampaignCategoryName = dr["CamCate_name"].ToString().Trim(),
                              CustomerFName = dr["customername"].ToString().Trim(),
                              ContactTel = dr["ContactTel"].ToString().Trim(),
                              addresscustomerdetail = dr["addressDatial"].ToString().Trim() + " " + dr["SubDistrictName"].ToString().Trim() + " " + dr["DistrictName"].ToString().Trim() + " " + dr["ProvinceName"].ToString().Trim(),
                              addresscustomerdetailzipcode = dr["addressZipcode"].ToString().Trim(),
                              
                              DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                              ProductCode =  dr["ProductCode"].ToString().Trim(),
                              SKU = (dr["sku"].ToString().Trim() != "") ? dr["sku"].ToString().Trim() : dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              NetPrice = (dr["NetPrice"].ToString().Trim() != "") ? dr["NetPrice"].ToString().Trim() : "0",
                              Amount = (dr["Amount"].ToString().Trim() != "") ? dr["Amount"].ToString().Trim() : "0",
                              TransportPrice = (dr["TransportPrice"].ToString().Trim() != "") ? dr["TransportPrice"].ToString().Trim() : "0",
                              orderTotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDecimal(dr["TotalPrice"].ToString().Trim()) : 0,
                              priceAmountproduct = dr["priceAmountproduct"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              OrderTracking = dr["OrderTracking"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              ReceiveDate = dr["ReceiveDate"].ToString().Trim(),
                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                              OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                              PaymentTypeName = dr["PaymentTypeName"].ToString().Trim(),
                              TaxId = dr["TaxId"].ToString().Trim(),
                              SumbyProductprice = dr["SumbyProductprice"].ToString().Trim(),
                              ChannelName = dr["ChannelName"].ToString().Trim(),
                              Gender = dr["Gender"].ToString().Trim(),
                              OrderNote = dr["OrderNote"].ToString().Trim(),
                              MerchantName = dr["MerchantName"].ToString().Trim(),



                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderOLInfo> LotLazadaReviewSearch(OrderOLInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.LotNo != null) && (oInfo.LotNo != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.LotNo like '%" + oInfo.LotNo + "%'" : strcond += " AND  o.LotNo like '%" + oInfo.LotNo + "%'";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }
           

            var LOrder = new List<OrderOLInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " select o.id, o.LotNo , o.CreateDate from LazadaLotNo o " + strcond  +
                    " order by o.id desc";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderOLInfo()
                          {
                              LotNo = dr["Lotno"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),


                            
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? CountLotLazadaReviewSearch(OrderInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.LotNo != null) && (oInfo.LotNo != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.LotNo like '%" + oInfo.LotNo + "%'" : strcond += " AND  o.LotNo like '%" + oInfo.LotNo + "%'";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();


            try
            {
                string strsql = " select count(o.id) as countOrder from LazadaLotNo o " 
                               + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              countOrder = Convert.ToInt32(dr["countOrder"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOrder.Count > 0)
            {
                count = LOrder[0].countOrder;
            }

            return count;
        }
        public List<OrderOLInfo> DtOlExcelReportSearch(OrderOLInfo oInfo)
        {
            string strcond = " WHERE  o.OrderSituation = '01'";
            string strnumpage = "";
            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.MerchantMapCode != null) && (oInfo.MerchantMapCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.MerchantMapCode  = '" + oInfo.MerchantMapCode + "'" : strcond += " AND  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }
            if ((oInfo.LotNo != null) && (oInfo.LotNo != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.LotNo = '" + oInfo.LotNo + "'" : strcond += " and  o.LotNo = '" + oInfo.LotNo + "'";
            }

            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderOLInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT distinct       o.OrderCode, c.CustomerFName, c.CustomerLName,  o.CreateDate," +
                            "c.ContactTel,c.CustomerCode,o.ReceiveDate,o.DeliveryDate,o.TotalPrice,o.OrderStatusCode  " +
                            " from " + dbName + ".dbo.OrderInfo o" +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            " left join CustomerAddressDetail AS cd ON cd.CustomerCode = c.CustomerCode" +

                                     // " where  1=1 " +
                                     strcond +
                                     " group by  o.OrderCode, c.CustomerFName, c.CustomerLName, o.CreateDate " +
                                        " ,o.confirmno,c.ContactTel,c.CustomerCode,o.ReceiveDate,o.DeliveryDate,o.TotalPrice,o.OrderStatusCode  ORDER BY o.OrderCode DESC " +
                                        "  " + strnumpage + " ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderOLInfo()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),
                            

                              ContactTel = dr["ContactTel"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              ReceiveDate = dr["ReceiveDate"].ToString().Trim(),
                              FullName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                              orderTotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDecimal( dr["TotalPrice"].ToString()) : 0,

                              OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? DtOlExcelReportSearchCount(OrderOLInfo oInfo)
        {
            string strcond = " WHERE  o.OrderSituation = '01'";
            string strnumpage = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if ((oInfo.MerchantMapCode != null) && (oInfo.MerchantMapCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.MerchantMapCode  = '" + oInfo.MerchantMapCode + "'" : strcond += " AND  o.MerchantMapCode = '" + oInfo.MerchantMapCode + "'";
            }
            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'" : strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)" : strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)" : strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)" : strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CustomerCode like '%" + oInfo.CustomerCode + "%'" : strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.CustomerContact != null) && (oInfo.CustomerContact != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'" : strcond += " and  cp.PhoneNumber like '%" + oInfo.CustomerContact + "%'";
            }
            if ((oInfo.LotNo != null) && (oInfo.LotNo != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.LotNo = '" + oInfo.LotNo + "'" : strcond += " and  o.LotNo = '" + oInfo.LotNo + "'";
            }
            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.ChannelCode = '" + oInfo.ChannelCode + "'" : strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'" : strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'" : strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderStateCode = '" + oInfo.OrderStateCode + "'" : strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.CreateBy = '" + oInfo.CreateBy + "'" : strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagApproved = '" + oInfo.FlagApproved + "'" : strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderOLInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT count (distinct o.id) as CountReport " +

                            " from " + dbName + ".dbo.OrderInfo o " +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            " left join CustomerAddressDetail AS cd ON cd.CustomerCode = c.CustomerCode" +


                                     
                                     "  " + strcond + " ";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderOLInfo()
                          {
                              CountReport = int.Parse(dr["CountReport"].ToString().Trim()),

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReport;
            }

            return count;
        }
        public List<OrderHistory> ReportOrderHistorySearch(OrderHistory oInfo)
        {
            string strcond = "";
            string strnumpage = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND c.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }
            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND c.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " AND  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " AND  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.ContactStatus != null) && (oInfo.ContactStatus != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  loc.LookupCode = '" + oInfo.ContactStatus + "'" : strcond += " AND  loc.LookupCode = '" + oInfo.ContactStatus + "'";
            }
            if ((oInfo.OrderSituation != null) && (oInfo.OrderSituation != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  loo.LookupCode = '" + oInfo.OrderSituation + "'" : strcond += " AND  loo.LookupCode = '" + oInfo.OrderSituation + "'";
            }
            if ((oInfo.OrderType != null) && (oInfo.OrderType != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagMediaPlan = '" + oInfo.OrderType + "'" : strcond += " AND  o.FlagMediaPlan = '" + oInfo.OrderType + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderHistory>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " Select o.id,FORMAT(c.CreateDate, 'dd/MM/yyyy ') as Createdate,FORMAT(c.CreateDate, 'HH:mm') as Createtime" +
                                " ,o.OrderCode,c.CustomerFName+' '+c.CustomerLName as CustomerName" +
                                " ,loc.LookupValue as CONTACTSTATUS,loo.LookupValue as ORDERSITUATION" +
                                " ,(case when o.FlagMediaPlan = 'Y' then 'Mediaplan' else 'Standard' end) as Ordertype" +
                                " ,o.OrderNote from  " + dbName + ".dbo.OrderInfo o " +
                                "left join CallInfo as c on o.Callinfo_id = c.id " +
                                "left join Lookup AS loc ON c.CONTACTSTATUS = loc.LookupCode and loc.LookupType = 'CONTACTSTATUS' " +
                                "left join Lookup AS loo ON o.OrderSituation = loo.LookupCode and loo.LookupType = 'ORDERSITUATION' " +
                                     
                                     strcond +
                                        " order by c.CreateDate desc " +
                                        "  " + strnumpage + " ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderHistory()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              Date = dr["Createdate"].ToString().Trim(),
                              Time = dr["Createtime"].ToString().Trim(),
                              CustomerName = dr["CustomerName"].ToString().Trim(),
                              ContactStatus = dr["CONTACTSTATUS"].ToString().Trim(),
                              OrderSituation = dr["ORDERSITUATION"].ToString().Trim(),
                              OrderType = dr["Ordertype"].ToString().Trim(),
                              OrderNote = dr["OrderNote"].ToString().Trim(),
                     



                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderHistory> ReportOrderHistory(OrderHistory oInfo)
        {
            string strcond = "";
            string strnumpage = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND c.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }
            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND c.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " AND  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " AND  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.ContactStatus != null) && (oInfo.ContactStatus != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  loc.LookupCode = '" + oInfo.ContactStatus + "'" : strcond += " AND  loc.LookupCode = '" + oInfo.ContactStatus + "'";
            }
            if ((oInfo.OrderSituation != null) && (oInfo.OrderSituation != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  loo.LookupCode = '" + oInfo.OrderSituation + "'" : strcond += " AND  loo.LookupCode = '" + oInfo.OrderSituation + "'";
            }
            if ((oInfo.OrderType != null) && (oInfo.OrderType != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagMediaPlan = '" + oInfo.OrderType + "'" : strcond += " AND  o.FlagMediaPlan = '" + oInfo.OrderType + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderHistory>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " Select o.id,FORMAT(c.CreateDate, 'dd/MM/yyyy ') as Createdate,FORMAT(c.CreateDate, 'HH:mm') as Createtime" +
                                " ,o.OrderCode,c.CustomerFName+' '+c.CustomerLName as CustomerName,od.ProductCode,p.ProductName,od.Amount,od.Price,od.NetPrice" +
                                " ,od.SumPrice,o.PercentVat,o.Vat,cast(o.TotalPrice as decimal(10,2)) TotalPrice,od.TransportPrice  " +
                                " ,(SELECT SUM(CAST(NetPrice AS decimal(10, 2) )*ol0.Amount) from OrderDetail ol0 where ol0.OrderCode =o.OrderCode ) as SumbyProductprice " +
                                " ,loc.LookupValue as CONTACTSTATUS,loo.LookupValue as ORDERSITUATION" +
                                " ,(case when o.FlagMediaPlan = 'Y' then 'Mediaplan' else 'Standard' end) as Ordertype" +
                                " ,o.OrderNote from  " + dbName + ".dbo.OrderInfo o " +
                                "left join CallInfo as c on o.Callinfo_id = c.id " +
                                "left join Lookup AS loc ON c.CONTACTSTATUS = loc.LookupCode and loc.LookupType = 'CONTACTSTATUS' " +
                                "left join Lookup AS loo ON o.OrderSituation = loo.LookupCode and loo.LookupType = 'ORDERSITUATION' " +
                                "left join OrderDetail as od ON od.OrderCode = o.OrderCode "+
                                "left join Product p on od.ProductCode = p.ProductCode " +
                                     
                                     strcond +
                                        " order by c.CreateDate desc " + "";
                                        


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderHistory()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              Date = dr["Createdate"].ToString().Trim(),
                              Time = dr["Createtime"].ToString().Trim(),
                              CustomerName = dr["CustomerName"].ToString().Trim(),
                              ContactStatus = dr["CONTACTSTATUS"].ToString().Trim(),
                              OrderSituation = dr["ORDERSITUATION"].ToString().Trim(),
                              OrderType = dr["Ordertype"].ToString().Trim(),
                              OrderNote = dr["OrderNote"].ToString().Trim(),
                              ProductCode = dr["ProductCode"].ToString().Trim(),
                              ProductName = dr["ProductName"].ToString().Trim(),
                              NetPrice = (dr["NetPrice"].ToString().Trim() != "") ? dr["NetPrice"].ToString().Trim() : "0",
                              Amount = (dr["Amount"].ToString().Trim() != "") ? dr["Amount"].ToString().Trim() : "0",
                              Sumprice = (dr["SumPrice"].ToString().Trim() != "") ? dr["SumPrice"].ToString().Trim() : "0",
                              TransportPrice = (dr["TransportPrice"].ToString().Trim() != "") ? dr["TransportPrice"].ToString().Trim() : "0",
                              orderTotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDecimal(dr["TotalPrice"].ToString().Trim()) : 0,
                              SumbyProductprice = dr["SumbyProductprice"].ToString().Trim(),
                              PercentVat = dr["PercentVat"].ToString().Trim(),
                              Vat = dr["Vat"].ToString().Trim(),



                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? ReportOrderHistoryCount(OrderHistory oInfo)
        {
            string strcond = "";
            string strnumpage = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + oInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)" : strcond += " AND c.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)" : strcond += " AND c.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }
            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond = strcond == "" ? strcond += " WHERE c.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)" : strcond += " AND c.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }
            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerFName like '%" + oInfo.CustomerFName + "%'" : strcond += " AND  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }
            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  c.CustomerLName like '%" + oInfo.CustomerLName + "%'" : strcond += " AND  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }
            if ((oInfo.ContactStatus != null) && (oInfo.ContactStatus != "") && (oInfo.ContactStatus != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  loc.LookupCode = '" + oInfo.ContactStatus + "'" : strcond += " AND  loc.LookupCode = '" + oInfo.ContactStatus + "'";
            }
            if ((oInfo.OrderSituation != null) && (oInfo.OrderSituation != "") && (oInfo.OrderSituation != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  loo.LookupCode = '" + oInfo.OrderSituation + "'" : strcond += " AND  loo.LookupCode = '" + oInfo.OrderSituation + "'";
            }
            if ((oInfo.OrderType != null) && (oInfo.OrderType != "") && (oInfo.OrderType != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.FlagMediaPlan = '" + oInfo.OrderType + "'" : strcond += " AND  o.FlagMediaPlan = '" + oInfo.OrderType + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderHistory>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT count(o.id) as CountReport " +

                                "from  " + dbName + ".dbo.OrderInfo o " +
                                "left join CallInfo as c on o.Callinfo_id = c.id " +
                                "left join Lookup AS loc ON c.CONTACTSTATUS = loc.LookupCode and loc.LookupType = 'CONTACTSTATUS' " +
                                "left join Lookup AS loo ON o.OrderSituation = loo.LookupCode and loo.LookupType = 'ORDERSITUATION' " +

                                     
                                     "  " + strcond + " ";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderHistory()
                          {
                              CountReport = int.Parse(dr["CountReport"].ToString().Trim()),

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReport;
            }

            return count;
        }
        public List<OrderPaymenttypeInfo> ReportPaymenttypeSearch(OrderPaymenttypeInfo oInfo)
        {
            string strcond = "";
            string strnumpage = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }

            if ((oInfo.ContactTel != null) && (oInfo.ContactTel != ""))
            {
                strcond += " and  c.ContactTel like '%" + oInfo.ContactTel + "%'";
            }


            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderPaymenttypeInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT o.OrderCode, c.CustomerFName, c.CustomerLName,  o.CreateDate, c.ContactTel" +
                                ",c.CustomerCode,o.ReceiveDate,o.DeliveryDate,o.TotalPrice,op.Installment,op.InstallmentPrice" +
                                ",op.FirstInstallment,luci.LookupValue as CardIssuename,op.CardNo" +
                                ",luct.LookupValue as CardType,op.CVCNo,op.CardHolderName,op.CardExpMonth" +
                                ",op.CardExpYear,op.CitizenId,op.BirthDate from " + dbName + ".dbo.OrderInfo o " +
                                "left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                                "left join OrderPayment AS op ON op.OrderCode = o.OrderCode " +
                                "left join Lookup AS luci ON op.CardIssuename = luci.LookupCode and luci.LookupType = 'BANK'" +
                                "left join Lookup AS luct ON op.CardIssuename = luct.LookupCode and luct.LookupType = 'CARDTYPE'" +
                                " where op.PaymentTypeCode = '02' and o.OrderSituation = '01'" +
                                     
                                     strcond +
                                        "ORDER BY o.CreateDate DESC " +
                                        "  " + strnumpage + " ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderPaymenttypeInfo()
                          {
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              CustomerFName = dr["CustomerFName"].ToString().Trim(),
                              CustomerLName = dr["CustomerLName"].ToString().Trim(),
                              CreateDate = dr["CreateDate"].ToString().Trim(),
                              ContactTel = dr["ContactTel"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              ReceiveDate = dr["ReceiveDate"].ToString().Trim(),
                              FullName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                              DeliveryDate = dr["DeliveryDate"].ToString().Trim(),
                              orderTotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDecimal(dr["TotalPrice"].ToString()) : 0,
                              Installment = (dr["Installment"].ToString().Trim() != "-99") ? dr["Installment"].ToString().Trim() : "",
                              InstallmentPrice = dr["InstallmentPrice"].ToString().Trim(),
                              FirstInstallment = dr["FirstInstallment"].ToString().Trim(),
                              CardIssuename = dr["CardIssuename"].ToString().Trim(),
                              CardNo = dr["CardNo"].ToString().Trim(),
                              CardType = dr["CardType"].ToString().Trim(),
                              CVCNo = dr["CVCNo"].ToString().Trim(),
                              CardHolderName = dr["CardHolderName"].ToString().Trim(),
                              CardExpMonth = dr["CardExpMonth"].ToString().Trim(),
                              CardExpYear = dr["CardExpMonth"].ToString().Trim()+"/"+dr["CardExpYear"].ToString().Trim(),
                              CitizenId = dr["CitizenId"].ToString().Trim(),
                              BirthDate = dr["BirthDate"].ToString().Trim(),
                         


                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public int? ReportPaymenttypeCount(OrderPaymenttypeInfo oInfo)
        {
            string strcond = " and o.OrderSituation = '01' ";
            string strnumpage = "";
            int? count = 0;

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " AND  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            if ((oInfo.OrderStatusCode != null) && (oInfo.OrderStatusCode != "") && (oInfo.OrderStatusCode != "-99"))
            {
                strcond += " and  o.OrderStatusCode = '" + oInfo.OrderStatusCode + "'";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")))
            {
                strcond += " AND o.CreateDate BETWEEN CONVERT(DATETIME, '" + oInfo.CreateDate + "',103) AND CONVERT(DATETIME,'" + oInfo.CreateDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.CreateDate != null) && (oInfo.CreateDate != "")) && ((oInfo.CreateDateTo == null) || (oInfo.CreateDateTo == "")))
            {
                strcond += " AND o.CreateDate >= CONVERT(DATETIME, '" + oInfo.CreateDate + "',103)";
            }

            if (((oInfo.CreateDateTo != null) && (oInfo.CreateDateTo != "")) && ((oInfo.CreateDate == null) || (oInfo.CreateDate == "")))
            {
                strcond += " AND o.CreateDate <= CONVERT(DATETIME, '" + oInfo.CreateDateTo + "',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")))
            {
                strcond += " AND o.DeliveryDate BETWEEN CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103) AND CONVERT(DATETIME,'" + oInfo.DeliveryDateTo + " 23:59:59:999',103)";
            }

            if (((oInfo.DeliveryDateFrom != null) && (oInfo.DeliveryDateFrom != "")) && ((oInfo.DeliveryDateTo == null) || (oInfo.DeliveryDateTo == "")))
            {
                strcond += " AND o.DeliveryDate >= CONVERT(DATETIME, '" + oInfo.DeliveryDateFrom + "',103)";
            }

            if (((oInfo.DeliveryDateTo != null) && (oInfo.DeliveryDateTo != "")) && ((oInfo.DeliveryDate == null) || (oInfo.DeliveryDate == "")))
            {
                strcond += " AND o.DeliveryDate <= CONVERT(DATETIME, '" + oInfo.DeliveryDateTo + "',103)";
            }

            if ((oInfo.CustomerCode != null) && (oInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode like '%" + oInfo.CustomerCode + "%'";
            }

            if ((oInfo.CustomerFName != null) && (oInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + oInfo.CustomerFName + "%'";
            }

            if ((oInfo.CustomerLName != null) && (oInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + oInfo.CustomerLName + "%'";
            }


            if ((oInfo.ContactTel != null) && (oInfo.ContactTel != ""))
            {
                strcond += " and  c.ContactTel like '%" + oInfo.ContactTel + "%'";
            }



            if ((oInfo.ChannelCode != null) && (oInfo.ChannelCode != ""))
            {
                strcond += " and  o.ChannelCode = '" + oInfo.ChannelCode + "'";
            }

            if ((oInfo.OrderTypeCode != null) && (oInfo.OrderTypeCode != ""))
            {
                strcond += " and  o.SALEORDERTYPE = '" + oInfo.OrderTypeCode + "'";
            }

            if ((oInfo.CampaignCategoryCode != null) && (oInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  o.CampaignCategoryCode = '" + oInfo.CampaignCategoryCode + "'";
            }

            if ((oInfo.OrderStateCode != null) && (oInfo.OrderStateCode != "") && (oInfo.OrderStateCode != "-99"))
            {
                strcond += " and  o.OrderStateCode = '" + oInfo.OrderStateCode + "'";
            }

            if ((oInfo.CreateBy != null) && (oInfo.CreateBy != ""))
            {
                strcond += " and  o.CreateBy = '" + oInfo.CreateBy + "'";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond += " and  o.FlagApproved = '" + oInfo.FlagApproved + "'";
            }
            if ((oInfo.rowFetch != 0))
            {
                strnumpage = strnumpage = "OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";
            }
            var LOrder = new List<OrderPaymenttypeInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT count (distinct o.id) as CountReport " +

                            " from " + dbName + ".dbo.OrderInfo o" +
                            " left join Customer AS c ON c.CustomerCode = o.CustomerCode " +
                            " left join OrderPayment AS op ON op.OrderCode = o.OrderCode where op.PaymentTypeCode = '02'"+

                                     
                                     "  " + strcond + " ";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new OrderPaymenttypeInfo()
                          {
                              CountReport = int.Parse(dr["CountReport"].ToString().Trim()),

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LOrder.Count > 0)
            {
                count = LOrder[0].CountReport;
            }

            return count;
        }
        public List<InvoiceOrderInfo> InvoiceOrder(InvoiceOrderInfo oInfo)
        {
            string strcond = "";
            string strnumpage = "";

            if ((oInfo.ordercode != null) && (oInfo.ordercode != ""))
            {
                strcond += " AND  o.OrderCode like '%" + oInfo.ordercode + "%'";
            }

       
            var LOrder = new List<InvoiceOrderInfo>();
            DataTable dt = new DataTable();


            try
            {
                string strsql = " SELECT o.OrderCode,od.Amount as qty,pd.ProductCode,pd.ProductName,c.CustomerFName +' '+ c.CustomerLName as customername,od.SumPrice as productprice ," +
                                "od.DiscountAmount as discount,od.ProductPrice as unit,od.SumPrice, ISNULL(od.Amount, 0)* ISNULL(od.ProductPrice, 0) as amount," +
                                "ISNULL(ca.address, '')+' '+ISNULL(ca.address2, '')+' '+ds.DistrictName+''+ISNULL(sds.SubDistrictName, '')+' '+pdn.ProvinceName+' '+ca.zipcode as customeraddress" +
                                ",c.CustomerCode ,o.CreateDate,CONVERT(varchar,o.CreateDate,106) as orderdate,o.MerchantMapCode" +
                                " FROM   OrderDetail AS od INNER JOIN" +
                                "  Promotion AS pro ON od.PromotionCode = pro.PromotionCode AND pro.FlagDelete = 'N' LEFT OUTER JOIN " +
                                " Product AS pd ON pd.ProductCode = od.ProductCode LEFT OUTER JOIN " +
                                " Lookup AS l ON l.LookupCode = pd.Unit AND l.LookupType = 'UNIT'" +
                                " inner join OrderInfo o on o.OrderCode =od.OrderCode " +
                                " inner join Customer c on o.CustomerCode =c.CustomerCode" +
                                " inner join CustomerAddressDetail ca on ca.CustomerCode = c.CustomerCode" +
                                " inner join District ds on ds.DistrictCode = ca.district" +
                                " left join SubDistrict sds on sds.SubDistrictCode =ca.subdistrict" +
                                " inner join Province pdn on pdn.ProvinceCode =ca.province" +
                                " inner join Merchant as mc on mc.MerchantCode = o.MerchantMapCode" +
                                " where o.ordercode is not null" +
                                  
                                     strcond +
                                     
                                        "  ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                LOrder = (from DataRow dr in dt.Rows

                          select new InvoiceOrderInfo()
                          {
                              ordercode = dr["ordercode"].ToString().Trim(),
                              qty = dr["qty"].ToString().Trim(),
                              productcode = dr["ProductCode"].ToString().Trim(),
                              productname = dr["productname"].ToString().Trim(),
                              customername = dr["customername"].ToString().Trim(),
                              CustomerCode = dr["CustomerCode"].ToString().Trim(),
                              customeraddress = dr["customeraddress"].ToString().Trim(),
                      
                              orderdate = dr["orderdate"].ToString().Trim(),
                              discount = dr["discount"].ToString().Trim(),
                              unit = dr["unit"].ToString().Trim(),
                              Amount = dr["Amount"].ToString().Trim(),
                              MerchantMapCode = dr["MerchantMapCode"].ToString().Trim(),


                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
    }
}

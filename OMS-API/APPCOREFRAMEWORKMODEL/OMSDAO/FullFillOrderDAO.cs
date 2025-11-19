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
    public class FullFillOrderDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        

        public int UpdateOrderInfoUpdateStatus(OrderInfo oinfo) {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.OrderInfo set ";


            if ((oinfo.OrderStatusCode != null) && (oinfo.OrderStatusCode != ""))
            {
                strsql += " OrderStatusCode = '" + oinfo.OrderStatusCode + "',";
            }
            if ((oinfo.OrderStateCode != null) && (oinfo.OrderStateCode != ""))
            {
                strsql += " OrderStateCode = '" + oinfo.OrderStateCode + "',";
            }
            if ((oinfo.OrderNote != null) && (oinfo.OrderNote != ""))
            {
                strsql += " OrderNote = '" + oinfo.OrderNote + "',";
            }
            if ((oinfo.shipmentdate != null) && (oinfo.shipmentdate != ""))
            {
                strsql += " shipmentdate = CONVERT(datetime,'" + oinfo.shipmentdate + "',101) ,";
            }

            strsql += " UpdateBy = '" + oinfo.UpdateBy + "'," +
                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                      " where OrderCode ='" + oinfo.OrderCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        //CountOrderListByCriteria_credit(): seems its query is the same as OrderDAO's CountOrderListByCriteria()
        //an additional condition of this method was added in OrderDAO's method instead

        public List<OrderListReturn> ListOrderByCriteriaOrderlist_credit(OrderInfo oInfo)
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
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + oInfo.shipmentdate + "')";
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

        public List<OrderInfoActivityListReturn> ListOrderByCriteriaOrderActivity(OrderInfoActivity oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderInfoActivityListReturn>();

            try
            {
                string strsql = " SELECT    oa.ordercode, oa.id,CONVERT(VARCHAR(19), oa.CreateDate, 120)	as CreateDate ,o.CreateBy,s.LookupValue as OrderStatusName," +
                                 "t.LookupValue  as OrderStateName, e.EmpFname_TH + SPACE(2) + e.Emplname_th as name,oa.Orderstatus,oa.Orderstate,Oa.note " +
                                 " FROM OMS.dbo.OrderInfo AS o " +
                                 " inner join OrderActivity oa on o.OrderCode = oa.OrderCode LEFT OUTER JOIN " +

                                  " Lookup AS s ON oa.Orderstatus = s.LookupCode AND s.LookupType = 'ORDERSTATUS' LEFT OUTER JOIN " +
                                  " Lookup AS t ON oa.OrderState = t.LookupCode AND t.LookupType = 'ORDERSTATE' " +

                         " inner join Emp e on e.empcode = o.createby " +
                                " where 1=1" + strcond;

                strsql += " ORDER BY o.Id DESC  ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderInfoActivityListReturn()
                          {
                              OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              OrderCode = dr["OrderCode"].ToString().Trim(),
                              OrderStatusCode = dr["Orderstatus"].ToString().Trim(),
                              OrderStateCode = dr["Orderstate"].ToString().Trim(),
                              OrderNote = dr["Note"].ToString().Trim(),
                              OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                              OrderStateName = dr["OrderStateName"].ToString().Trim(),
                              CreateBy = dr["name"].ToString().Trim(),
                              Createdate = dr["createdate"].ToString().Trim(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        //old name(ListOrderByCriteriaOrderlist_ReceiptReturn) was the same of customerOrderDAO's method
        public List<ReceiptReturnOrderListReturn> ListOrderByCriteriaOrderlist_ReceiptReturn1(ReceiptReturnOrderInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + oInfo.OrderCode + "%'";
            }

        

            DataTable dt = new DataTable();
            var LOrderReceipt = new List<ReceiptReturnOrderListReturn>();

            try
            {
                string strsql = " SELECT        o.Id, o.OrderCode,inm.ProductCode,p.ProductName,inm.POCode,inm.id as idinm " +
                              "FROM OrderInfo AS o inner join " +
                               "              InventoryMovement inm on o.OrderCode = inm.OrderNo " +

                                "             inner join Product p on inm.ProductCode = p.ProductCode " +
                            "  WHERE(1 = 1) and p.FlagDelete = 'N' " +

                          "" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderReceipt = (from DataRow dr in dt.Rows

                                  select new ReceiptReturnOrderListReturn()
                                  {
                                      OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                      OrderCode = dr["OrderCode"].ToString().Trim(),
                                      ProductCode = dr["ProductCode"].ToString().Trim(),
                                      ProductName = dr["ProductName"].ToString().Trim(),
                                      POCode = dr["POCode"].ToString().Trim(),
                                      InventoryMovement_id = dr["idinm"].ToString().Trim(),
                                  }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderReceipt;
        }

        public List<OrderListReturn> ListOrderByCriteriaBookOrder(OrderInfo oInfo, string StatusPage)
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
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + oInfo.shipmentdate + "')";
            }
            if ((StatusPage != null) && (StatusPage != ""))
            {
                strcond += "  and o.confirmno is not null ";
            }
            else
            {
                strcond += "  and o.confirmno is null ";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName ,o.shipmentdate,o.OrderListDate from " + dbName + ".dbo.OrderInfo o " +
                               " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                 "left outer join BookingDetail bkd on o.OrderCode != bkd.Ordercode " +
                                  "inner join OrderTransport ot on o.OrderCode = ot.OrderCode " +
                                " where 1=1 and o.shipmentdate is not null  and ot.TransportType = '03' " + strcond;
                strsql += "  group by c.CustomerFName, c.CustomerLName, o.Id, o.OrderCode, o.OrderStatusCode, o.OrderStateCode, o.BUCode, o.NetPrice, o.TotalPrice, o.Vat, o.CreateDate, o.CreateBy, o.UpdateDate, o.UpdateBy, o.FlagDelete, o.CustomerCode, " +
               "    o.RunningNo, o.CompanyFrom, o.CompanyTo, o.Transport, o.Certificate, o.confirmno, o.shipmentdate, o.OrderNote, s.LookupValue, t.LookupValue , o.shipmentdate,o.OrderListDate";
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
                              //TransportName = dr["TransportName"].ToString(),
                              //TransportCode = dr["TransportCode"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public int? CountOrderListByCriteriaBookOrderCar(OrderInfo oInfo, string StatusPage)
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
                                 "left outer join BookingDetail bkd on o.OrderCode != bkd.Ordercode " +
                                     " where 1=1 and o.shipmentdate is not null  " + strcond;

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

        public List<OrderListReturn> ListOrderByCriteriaOLPL(OrderInfo oInfo)
        {
            string strcond = "";
            if ((oInfo.OrderListDate != null) && (oInfo.OrderListDate != ""))
            {
                strcond += " AND (CONVERT(Date, o.CreateDate) = '" + oInfo.OrderListDate + "')";
            }
          

            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = "  SELECT        CONVERT(Date, o.OrderListDate) OrderListDate  ,count(id) as amonut " +
                                ", (SELECT        COUNT(o1.Id) " +
                                " FROM  " + dbName + ".dbo.OrderInfo AS o1 " +
                                " WHERE(1 = 1) and CONVERT(Date, o.OrderListDate) = CONVERT(Date, o1.OrderListDate) and(o1.confirmno is null) " +
                                " ) as NotSent " +
                                " , " +
                                " (SELECT        COUNT(o2.Id) " +
                                " FROM  " + dbName + ".dbo.OrderInfo AS o2 " +
                                " WHERE(1 = 1) and CONVERT(Date, o.OrderListDate) = CONVERT(Date, o2.OrderListDate) and(o2.confirmno is not null) " +
                                " ) as Sented " +

                                   " FROM  " + dbName + ".dbo.OrderInfo AS o " +
                                    "  WHERE(1 = 1)  " +
                                   "   group by   CONVERT(Date, o.OrderListDate)  " +
                                  "    ORDER BY OrderListDate desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {
                              
                              shipmentdate = dr["OrderListDate"].ToString(),
                              OrderTotalAmount = dr["amonut"].ToString(),
                              NotSent = dr["NotSent"].ToString(),
                              Sented = dr["Sented"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderPieChartInfo> ListOrderByCriteriaPiechart(OrderPieChartInfo oInfo)
        {
            string strcond = "";
            
            if ((oInfo.Date != null) && (oInfo.Date != ""))
            {

                
                strcond += " AND (CONVERT(Date, o.CreateDate) =  (CONVERT(Date, '" + oInfo.Date + "',103)))";
            }
            else
            {
                strcond += " AND ( MONTH(CONVERT(Date, o.CreateDate)) = MONTH(CONVERT(Date,getdate())))";
            }

            DataTable dt = new DataTable();
            var LOrder = new List<OrderPieChartInfo>();

            try
            {
                string strsql = " SELECT        o.OrderStatusCode,l.LookupValue, COUNT(o.Id) AS amonut " +
                                   
                                   " FROM OrderInfo AS o " +
                                    " inner join Lookup l on o.OrderStatusCode = l.LookupCode and l.LookupType='ORDERSTATUS' " +
                                    "  WHERE(1 = 1) "+strcond+" " +
                                //   "   and  MONTH(o.CreateDate) = MONTH(getdate())  and   YEAR(o.CreateDate) = YEAR(getdate())  " +
                                  "  GROUP BY o.OrderStatusCode,l.LookupValue ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderPieChartInfo()
                          {

                              OrderStatusCode = dr["LookupValue"].ToString(),
                              Amount = dr["amonut"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderListReturn> ListOrderBarChartByCriteria(OrderInfo oInfo)
        {
            string strcond = "";
            if ((oInfo.CreateDate != null) && (oInfo.CreateDate != ""))
            {

                
                strcond += " AND ( MONTH(CONVERT(Date, o.CreateDate)) = MONTH(CONVERT(Date, '" + oInfo.CreateDate + "',103)))";
            }
            else
            {
                strcond += " AND ( MONTH(CONVERT(Date, o.CreateDate)) = MONTH(CONVERT(Date,getdate())))";
            }


            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {
                string strsql = "  SELECT        CONVERT(varchar, CreateDate, 103) OrderListDate  ,SUM(o.TotalPrice)  as amonut " +
                                   " FROM OrderInfo AS o " +
                                    "  WHERE(1 = 1)  "+ strcond + "" +
                                   "   group by   CONVERT(varchar, CreateDate, 103) " +
                                  "    ORDER BY OrderListDate desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrder = (from DataRow dr in dt.Rows

                          select new OrderListReturn()
                          {

                              shipmentdate = dr["OrderListDate"].ToString(),
                              OrderTotalAmount = dr["amonut"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }
        public List<OrderListReturn> ListOrderFullfillManagementByCriteria_showgv(OrderInfo oInfo)
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

            if ((oInfo.MerchantCode != null) && (oInfo.MerchantCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.MerchantCode = '" + oInfo.MerchantCode + "'" : strcond += " and  o.MerchantCode = '" + oInfo.MerchantCode + "'";
            }
            DataTable dt = new DataTable();
            var LOrder = new List<OrderListReturn>();

            try
            {

                string strsql =
                    "SELECT c.CustomerFName, c.CustomerLName,cp.PhoneNumber, o.Id, o.OrderCode, o.OrderType, o.OrderStatusCode, o.OrderStateCode, o.BUCode, o.SubTotalPrice, o.TotalPrice, o.Vat, o.CreateDate, o.CreateBy, o.UpdateDate, o.UpdateBy, o.FlagDelete," +
                    "o.CustomerCode, o.RunningNo, o.CompanyFrom, o.CompanyTo, o.Transport, o.Certificate, o.confirmno, o.shipmentdate, o.OrderNote, o.OrderListDate, o.BranchCode,b.BranchName, o.OrderRef, o.ChannelCode,cn.ChannelName, o.ReferalRequestCode," +
                    "o.DeliveryDate, o.ReceiveDate, o.OrderTracking, o.AssignTo, s.LookupValue AS OrderStatusName, o.shipmentdate AS Expr1,o.SALEORDERTYPE,sot.LookupValue as SaleOrderTypeName,sos.LookupValue AS ORDERSTATE, cc.CamCate_name AS CamCateName, o.BranchOrderID, o.OrderRejectRemark,o.InventoryCode " +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT OUTER JOIN Customer AS c ON o.CustomerCode = c.CustomerCode " +
                    " LEFT OUTER JOIN Lookup AS s ON o.OrderStatusCode = s.LookupCode AND s.LookupType = 'ORDERSTATUS' " +
                    " LEFT OUTER JOIN Lookup AS sot ON o.SALEORDERTYPE = sot.LookupCode AND sot.LookupType = 'SALEORDERTYPE' " +
                    " LEFT OUTER JOIN Lookup AS sos ON o.SALEORDERTYPE = sos.LookupCode AND sos.LookupType = 'ORDERSTATE' " +
                    " LEFT OUTER JOIN Channel AS cn ON o.ChannelCode = cn.ChannelCode AND cn.FlagDelete = 'N' " +
                    " LEFT OUTER JOIN Branch AS b ON o.BranchCode = b.BranchCode " +
                    " LEFT OUTER JOIN CampaignCategory AS cc ON o.CampaignCategoryCode = cc.CampaignCategoryCode " +
                    " LEFT OUTER JOIN CustomerPhone AS cp ON o.CustomerCode  = cp.CustomerCode " + strcond;

                strsql += " ORDER BY o.CreateDate DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";


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
                              InventoryCode = dr["InventoryCode"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrder;
        }

        public List<OrderListReturn> ListFullfillOrderManagementNoPagingByCriteria(OrderInfo oInfo)
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
                    "SELECT c.CustomerFName, c.CustomerLName,cp.PhoneNumber, o.Id, o.OrderCode, o.OrderType, o.OrderStatusCode, o.OrderStateCode, o.BUCode, o.SubTotalPrice, o.TotalPrice, o.Vat, o.CreateDate, o.CreateBy, o.UpdateDate, o.UpdateBy, o.FlagDelete," +
                    "o.CustomerCode, o.RunningNo, o.CompanyFrom, o.CompanyTo, o.Transport, o.Certificate, o.confirmno, o.shipmentdate, o.OrderNote, o.OrderListDate, o.BranchCode,b.BranchName, o.OrderRef, o.ChannelCode,cn.ChannelName, o.ReferalRequestCode," +
                    "o.DeliveryDate, o.ReceiveDate, o.OrderTracking, o.AssignTo, s.LookupValue AS OrderStatusName, o.shipmentdate AS Expr1,o.SALEORDERTYPE,sot.LookupValue as SaleOrderTypeName,sos.LookupValue AS ORDERSTATE, cc.CamCate_name AS CamCateName, o.BranchOrderID, o.OrderRejectRemark, o.TransportPrice " +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT OUTER JOIN Customer AS c ON o.CustomerCode = c.CustomerCode " +
                    " LEFT OUTER JOIN Lookup AS s ON o.OrderStatusCode = s.LookupCode AND s.LookupType = 'ORDERSTATUS' " +
                    " LEFT OUTER JOIN Lookup AS sot ON o.SALEORDERTYPE = sot.LookupCode AND sot.LookupType = 'SALEORDERTYPE' " +
                    " LEFT OUTER JOIN Lookup AS sos ON o.SALEORDERTYPE = sos.LookupCode AND sos.LookupType = 'ORDERSTATE' " +
                    " LEFT OUTER JOIN Channel AS cn ON o.ChannelCode = cn.ChannelCode AND cn.FlagDelete = 'N' " +
                    " LEFT OUTER JOIN Branch AS b ON o.BranchCode = b.BranchCode " +
                    " LEFT OUTER JOIN CampaignCategory AS cc ON o.CampaignCategoryCode = cc.CampaignCategoryCode " +
                    " LEFT OUTER JOIN CustomerPhone AS cp ON o.CustomerCode  = cp.CustomerCode " + strcond;

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
                              TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToInt32(dr["TransportPrice"]) : 0,
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

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
    public class BookOrderCarDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? InsertBookOrderCar(BookOrderCarInfo bocInfo) {
            int? i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.BookingDetail   (Booking_No, Ordercode, Driver_no,Vechicle_id, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete)" +
                            "VALUES (" +
                           "'" + bocInfo.Booking_No + "'," +
                           "'" + bocInfo.Ordercode + "'," +
                           "'" + bocInfo.Driver_no + "'," +
                             "'" + bocInfo.Vechicle_id + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + bocInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + bocInfo.UpdateBy + "'," +
                           "'" + bocInfo.FlagDelete + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdateCustomerBookOrderCar(BookOrderCarInfo bocInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.BookingDetail set " +
                            " Booking_No = '" + bocInfo.Booking_No + "'," +
                            " Ordercode = '" + bocInfo.Ordercode + "'," +
                            " Driver_no = '" + bocInfo.Driver_no + "'," +
                            " UpdateBy = '" + bocInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where id ='" + bocInfo.BookId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? DeleteCustomerBookOrderCar(BookOrderCarInfo bocInfo)
        {
            int? i = 0;

            string strsql = "Update " + dbName + ".dbo.BookingDetail set FlagDelete = 'Y' where Id in (" + bocInfo.BookId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<OrderListReturn> ListOrderByCriteriaBookOrder(OrderInfo oInfo)
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
            var OrderList = new List<OrderListReturn>();

            try
            {
                string strsql = " select r.Routing_name,rm.Routing_code,c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName ,o.shipmentdate,o.OrderListDate from " + dbName + ".dbo.OrderInfo o " +
                                " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " left outer join BookingDetail bkd on o.OrderCode != bkd.Ordercode " +
                                " inner join OrderTransport ot on o.OrderCode = ot.OrderCode " +
                                " left join RoutingManagement rm on rm.ProvinceCode = ot.Province and rm.DistrictCode = ot.District and rm.SubDistrictCode = ot.Subdistrict "+
                                " left join Routing r on r.Routing_code = rm.routing_code" +
                                " where 1=1 and o.shipmentdate is not null  and ot.TransportType = '03'  and  o.OrderStatusCode = '04' and  o.OrderStateCode = '02'" + strcond;
                strsql += "   group by r.Routing_name,rm.Routing_code,c.CustomerFName, c.CustomerLName,o.Id, o.OrderCode, o.OrderStatusCode, o.OrderStateCode, o.BUCode,o.ordertype,o.SubTotalPrice, o.TotalPrice, o.Vat, o.CreateDate, o.CreateBy, o.UpdateDate, o.UpdateBy, o.FlagDelete,o.confirmno,o.MerchantCode,o.OrderRef,o.ChannelCode,o.ReferalRequestCode,o.DeliveryDate,o.ReceiveDate,o.OrderTracking,o.AssignTo, o.CustomerCode, o.RunningNo, o.CompanyFrom, o.CompanyTo, o.Transport, o.Certificate, o.shipmentdate, o.OrderNote, s.LookupValue, t.LookupValue , o.shipmentdate,o.OrderListDate   ";
         
                       strsql += " ORDER BY o.Id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                OrderList = (from DataRow dr in dt.Rows

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

            return OrderList;
        }

        public int? CountOrderListByCriteriaBookOrderCar(OrderInfo oInfo)
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


            DataTable dt = new DataTable();
            var OrderList = new List<OrderListReturn>();


            try
            {
                string strsql = " select count(distinct o.id) as countOrder from " + dbName + ".dbo.OrderInfo o " +
                                 "left join Customer c on o.CustomerCode = c.CustomerCode " +
                                "left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS' " +
                                "left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE' " +
                                "left outer join BookingDetail bkd on o.OrderCode != bkd.Ordercode " +
                                "inner join OrderTransport ot on o.OrderCode = ot.OrderCode " +
                                "where 1 = 1 and o.shipmentdate is not null  and ot.TransportType = '03' and o.OrderStatusCode = '04' and o.OrderStateCode = '02'" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                OrderList = (from DataRow dr in dt.Rows

                             select new OrderListReturn()
                             {
                                 countOrder = Convert.ToInt32(dr["countOrder"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (OrderList.Count > 0)
            {
                count = OrderList[0].countOrder;
            }

            return count;
        }

    }
}
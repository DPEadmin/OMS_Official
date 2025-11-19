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
    public class CustomerOrderDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountOrderListByCriteriaOrderlist(CustomerOrderInfo cInfo, string statusPage)
        {
            string strcond = "";
            int? count = 0;
            if ((cInfo.OrderId != null) && (cInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + cInfo.OrderId;
            }

            if ((cInfo.OrderCode != null) && (cInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + cInfo.OrderCode + "%'";
            }
            if ((cInfo.OrderStatusCode != null) && (cInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + cInfo.OrderStatusCode + "'";
            }
            if ((cInfo.OrderStateCode != null) && (cInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + cInfo.OrderStateCode + "'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }
            

            DataTable dt = new DataTable();
            var LCustomerOrder = new List<CustomerOrderListReturn>();


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
                LCustomerOrder = (from DataRow dr in dt.Rows

                             select new CustomerOrderListReturn()
                             {
                                 countOrder = Convert.ToInt32(dr["countOrder"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCustomerOrder.Count > 0)
            {
                count = LCustomerOrder[0].countOrder;
            }

            return count;
        }

        public List<CustomerOrderListReturn> ListOrderByCriteriaOrderlist(CustomerOrderInfo cInfo, string statusPage)
        {
            string strcond = "";

            if ((cInfo.OrderId != null) && (cInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + cInfo.OrderId;
            }

            if ((cInfo.OrderCode != null) && (cInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + cInfo.OrderCode + "%'";
            }
            if ((cInfo.OrderStatusCode != null) && (cInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + cInfo.OrderStatusCode + "'";
            }
            if ((cInfo.OrderStateCode != null) && (cInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + cInfo.OrderStateCode + "'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }
            if ((cInfo.shipmentdate != null) && (cInfo.shipmentdate != ""))
            {
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + cInfo.shipmentdate + "')";
            }
            if ((statusPage != null) && (statusPage != ""))
            {
                strcond += "  and o.confirmno is not null ";
            }
            else
            {
                strcond += "  and o.confirmno is null ";
            }

            DataTable dt = new DataTable();
            var LCustomerOrder = new List<CustomerOrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName ,o.shipmentdate,c.Mobile as customerphone from " + dbName + ".dbo.OrderInfo o " +
                               " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where 1=1" + strcond;

                strsql += " ORDER BY o.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerOrder = (from DataRow dr in dt.Rows

                             select new CustomerOrderListReturn()
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
                                 CustomerPhone = dr["CustomerPhone"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerOrder;
        }

        public List<CustomerOrderListReturn> ListCustomerOrderNoPagingbyCriteria(CustomerOrderInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.OrderId != null) && (cInfo.OrderId != 0))
            {
                strcond += " and  o.Id =" + cInfo.OrderId;
            }

            if ((cInfo.OrderCode != null) && (cInfo.OrderCode != ""))
            {
                strcond += " and  o.OrderCode like '%" + cInfo.OrderCode + "%'";
            }
            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode = '" + cInfo.CustomerCode + "'";
            }
            if ((cInfo.OrderStatusCode != null) && (cInfo.OrderStatusCode != ""))
            {
                strcond += " and  o.OrderStatusCode = '" + cInfo.OrderStatusCode + "'";
            }
            if ((cInfo.OrderStateCode != null) && (cInfo.OrderStateCode != ""))
            {
                strcond += " and  o.OrderStateCode = '" + cInfo.OrderStateCode + "'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }
            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }
            if ((cInfo.shipmentdate != null) && (cInfo.shipmentdate != ""))
            {
                strcond += " AND (CONVERT(Date, o.shipmentdate) = '" + cInfo.shipmentdate + "')";
            }

            DataTable dt = new DataTable();
            var LCustomerOrder = new List<CustomerOrderListReturn>();

            try
            {
                string strsql = " select c.CustomerFName,c.CustomerLName,o.*,s.LookupValue as OrderStatusName,t.LookupValue  as OrderStateName ,o.shipmentdate from " + dbName + ".dbo.OrderInfo o " +
                               " left join Customer c on o.CustomerCode = c.CustomerCode" +
                                 " left join Lookup s on o.OrderStatusCode = s.LookupCode and s.LookupType = 'ORDERSTATUS'" +
                                " left join Lookup t on o.OrderStateCode = t.LookupCode and t.LookupType = 'ORDERSTATE'" +
                                " where 1=1" + strcond;

                strsql += " ORDER BY o.UpdateDate DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerOrder = (from DataRow dr in dt.Rows

                                  select new CustomerOrderListReturn()
                                  {
                                      OrderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                      OrderCode = dr["OrderCode"].ToString().Trim(),
                                      OrderStateCode = dr["OrderStateCode"].ToString().Trim(),
                                      OrderStateName = dr["OrderStateName"].ToString().Trim(),
                                      OrderStatusCode = dr["OrderStatusCode"].ToString().Trim(),
                                      OrderStatusName = dr["OrderStatusName"].ToString().Trim(),
                                      OrderNote = dr["OrderNote"].ToString().Trim(),
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

            return LCustomerOrder;
        }

        public List<ReceiptReturnOrderListReturn> ListOrderByCriteriaOrderlist_ReceiptReturn(ReceiptReturnOrderInfo oInfo)
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
                string strsql = " SELECT        o.Id, o.OrderCode,inm.ProductCode,p.ProductName,inm.POCode,inm.id as idinm ,od.Amount " +
                                    " FROM OrderInfo AS o " +
                                    " inner join OrderDetail as od on o.OrderCode = od.OrderCode " +
                                    "inner join " +
                                     "              InventoryMovement inm on o.OrderCode = inm.OrderNo " +

                                      "             inner join Product p on inm.ProductCode = p.ProductCode " +
                                  "  WHERE(1 = 1) and p.FlagDelete = 'N' and od.ProductCode !='9999999' " +

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

    }
}

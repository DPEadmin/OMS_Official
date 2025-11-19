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
    public class OrderChangestatusDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

     

        public int UpdateOrderChangeStatusInfo(OrderChangestatusInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            
            if ((oInfo.orderstatus != null) && (oInfo.orderstatus != ""))
            {
                strcond += " OrderStatusCode = '" + oInfo.orderstatus + "',";
            }

            if ((oInfo.FlagApproved != null) && (oInfo.FlagApproved != ""))
            {
                strcond += " FlagApproved = '" + oInfo.FlagApproved + "',";
            }

            if ((oInfo.orderstate != null) && (oInfo.orderstate != ""))
            {
                strcond += " OrderStateCode = '" + oInfo.orderstate + "',";
            }
            if ((oInfo.Confirmno!= null) && (oInfo.Confirmno != ""))
            {
                strcond += " Confirmno = '" + oInfo.Confirmno + "', OrderListDate = getdate(),";
            }
            if ((oInfo.OrderTracking != null) && (oInfo.OrderTracking != ""))
            {
                strcond += " OrderTracking = '" + oInfo.OrderTracking + "',";
            }
            if ((oInfo.MerchantMapCode != null) && (oInfo.MerchantMapCode != ""))
            {
                strcond += " MerchantMapCode = '" + oInfo.MerchantMapCode + "',";
            }
            string strsql = " Update " + dbName + ".dbo.OrderInfo set " + strcond + " UpdateBy = '" + oInfo.updateBy + "'," +
                       
                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                     

                      " where OrderCode ='" + oInfo.ordercode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateOrderinventoryInfo(OrderChangeInventoryInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            
          

            if ((oInfo.Inventorycode != null) && (oInfo.Inventorycode != ""))
            {
                strcond += " Inventorycode = '" + oInfo.Inventorycode + "',";
            }
            if ((oInfo.orderstatus != null) && (oInfo.orderstatus != ""))
            {
                strcond += "orderstatusCode = '" + oInfo.orderstatus + "',";
            }
            if ((oInfo.orderstate != null) && (oInfo.orderstate != ""))
            {
                strcond += " OrderStateCode = '" + oInfo.orderstate + "',";
            }
            if ((oInfo.OrderNote != null) && (oInfo.OrderNote != ""))
            {
                strcond +=  " OrderNote ='" +  oInfo.OrderNote + "/" + "Change inventory from " + oInfo.Inventorycode_Old + " to "+ oInfo.Inventorycode+"',";
            }
            string strsql = " Update " + dbName + ".dbo.OrderInfo set " + strcond + " UpdateBy = '" + oInfo.updateBy + "'," +

                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +


                      " where OrderCode ='" + oInfo.ordercode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateOrderTransportinventoryInfo(OrderChangeInventoryInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            
          

            if ((oInfo.Inventorycode != null) && (oInfo.Inventorycode != ""))
            {
                strcond += " Inventorycode = '" + oInfo.Inventorycode + "',";
            }
         
            string strsql = " Update " + dbName + ".dbo.ordertransport set " + strcond + " UpdateBy = '" + oInfo.updateBy + "'," +

                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +


                      " where OrderCode ='" + oInfo.ordercode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateOrderDetailinventoryInfo(OrderChangeInventoryInfo oInfo)
        {
            int i = 0;

            string strcond = "";

            


            if ((oInfo.Inventorycode != null) && (oInfo.Inventorycode != ""))
            {
                strcond += " Inventorycode = '" + oInfo.Inventorycode + "',";
            }

            string strsql = " Update " + dbName + ".dbo.orderdetail set " + strcond + " UpdateBy = '" + oInfo.updateBy + "'," +

                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +


                      " where OrderCode ='" + oInfo.ordercode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}

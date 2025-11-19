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
    public class InventoryBalanceDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<InventoryBalanceListReturn> ListInventoryBalanceNopagingByCriteria(InventoryBalanceInfo ibInfo)
        {
            string strcond = "";

            if ((ibInfo.InventoryCode != null) && (ibInfo.InventoryCode != ""))
            {
                strcond += " and  ib.InventoryCode = '" + ibInfo.InventoryCode + "'";
            }

            if ((ibInfo.ProductCode != null) && (ibInfo.ProductCode != ""))
            {
                strcond += " and  ib.ProductCode like '%" + ibInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LInventoryBalance = new List<InventoryBalanceListReturn>();

            try
            {
                string strsql = " select ib.*, i.InventoryName as InventoryName, p.ProductName as ProductName, s.SupplierName, s.SupplierCode from " + dbName + ".dbo.InventoryBalance ib " +
                                " left join " + dbName + ".dbo.Inventory i on ib.InventoryCode = i.InventoryCode " +
                                " left join " + dbName + ".dbo.Product p on p.ProductCode = ib.ProductCode  and p.FlagDelete = 'N'" +
                                " left join " + dbName + ".dbo.Supplier s on s.SupplierCode = p.SupplierCode and s.FlagDelete = 'N'" +
                               " where 1=1 " + strcond;

                strsql += "order by ib.Id ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryBalance = (from DataRow dr in dt.Rows

                               select new InventoryBalanceListReturn()
                               {
                                   InventoryBalanceId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                   InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                   InventoryName = dr["InventoryName"].ToString().Trim(),
                                   ProductCode = dr["ProductCode"].ToString().Trim(),
                                   ProductName = dr["ProductName"].ToString().Trim(),
                                   SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                   SupplierName = dr["SupplierName"].ToString().Trim(),
                                   QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                   Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                   Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,

                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventoryBalance;
        }

        public List<InventoryBalanceListReturn> ListInventoryBalanceByCriteria(InventoryBalanceInfo ibInfo)
        {
            string strcond = "";

            if ((ibInfo.InventoryCode != null) && (ibInfo.InventoryCode != ""))
            {
                strcond += " and  ib.InventoryCode = '" + ibInfo.InventoryCode + "'";
            }

            if ((ibInfo.ProductCode != null) && (ibInfo.ProductCode != ""))
            {
                strcond += " and  ib.ProductCode like '%" + ibInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LInventoryBalance = new List<InventoryBalanceListReturn>();

            try
            {
                string strsql = " select ib.*, i.InventoryName as InventoryName, p.ProductName as ProductName, s.SupplierName, s.SupplierCode from " + dbName + ".dbo.InventoryBalance ib " +
                                " left join " + dbName + ".dbo.Inventory i on ib.InventoryCode = i.InventoryCode " +
                                " left join " + dbName + ".dbo.Product p on p.ProductCode = ib.ProductCode  and p.FlagDelete = 'N'" +
                                " left join " + dbName + ".dbo.Supplier s on s.SupplierCode = p.SupplierCode and s.FlagDelete = 'N'" +
                               " where 1=1 " + strcond;

                strsql += "order by ib.Id DESC OFFSET " + ibInfo.rowOFFSet + " ROWS FETCH NEXT " + ibInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryBalance = (from DataRow dr in dt.Rows

                                     select new InventoryBalanceListReturn()
                                     {
                                         InventoryBalanceId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                         InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                         InventoryName = dr["InventoryName"].ToString().Trim(),
                                         ProductCode = dr["ProductCode"].ToString().Trim(),
                                         ProductName = dr["ProductName"].ToString().Trim(),
                                         SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                         SupplierName = dr["SupplierName"].ToString().Trim(),
                                         QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                         Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                         Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,

                                     }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventoryBalance;
        }

        public int? CountInventoryBalanceByCriteria(InventoryBalanceInfo ibInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((ibInfo.InventoryCode != null) && (ibInfo.InventoryCode != ""))
            {
                strcond += " and  ib.InventoryCode = '" + ibInfo.InventoryCode + "'";
            }

            if ((ibInfo.ProductCode != null) && (ibInfo.ProductCode != ""))
            {
                strcond += " and  ib.ProductCode like '%" + ibInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LInventoryBalance = new List<InventoryBalanceListReturn>();

            try
            {
                string strsql = "select count(ib.Id) as countInventoryBalance from " + dbName + ".dbo.InventoryBalance ib " +
                                " left join " + dbName + ".dbo.Inventory i on ib.InventoryCode = i.InventoryCode " +
                                " left join " + dbName + ".dbo.Product p on p.ProductCode = ib.ProductCode  and p.FlagDelete = 'N'" +
                                " left join " + dbName + ".dbo.Supplier s on s.SupplierCode = p.SupplierCode and s.FlagDelete = 'N'" +
                               " where 1=1 " + strcond;

               // strsql += "order by ib.Id ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventoryBalance = (from DataRow dr in dt.Rows

                                     select new InventoryBalanceListReturn()
                                     {
                                         countInventoryBalance = (dr["countInventoryBalance"].ToString() != "") ? Convert.ToInt32(dr["countInventoryBalance"]) : 0,
       
                                     }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LInventoryBalance.Count > 0)
            {
                count = LInventoryBalance[0].countInventoryBalance;
            }

            return count;

        }

        public int InsertInventoryBalance(InventoryBalanceInfo iInfo)
        {
            int i = 0;

            string strsql = "insert into InventoryBalance (InventoryCode, ProductCode, Balance , QTY, Reserved, CreateDate, FlagActive) values (" +
                             "'" + iInfo.InventoryCode + "', " +
                             "'" + iInfo.ProductCode + "', " +
                             "'" + iInfo.Balance + "', " +
                                   iInfo.QTY + ", " +
                                 +iInfo.Reserved + ", " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                        //     "'" + iInfo.CreateBy + "', " +
                             "'" + iInfo.FlagActive + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteInventoryBalance(InventoryBalanceInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryBalance set " +

                         " FlagActive = 'N'" +

                         " where Id in(" + iInfo.InventoryBalanceId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInventoryBalance(InventoryBalanceInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryBalance set " +

                            " Balance = " + iInfo.Balance + "," +
                            " QTY = " + iInfo.QTY + "," +
                            " Reserved = " + iInfo.Reserved + "," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'" +
                            " where Id =" + iInfo.InventoryBalanceId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteInventoryBalanceFromInventoryCode(InventoryBalanceInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryBalance set " +

                         " FlagActive = 'N'" +

                         " where InventoryCode ='" + iInfo.InventoryCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}

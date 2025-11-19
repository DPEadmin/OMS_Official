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
    public class ProductStockDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<ProductStockListReturn> ListProductStockByCriteria(ProductStockInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductStockId != null) && (pInfo.ProductStockId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductStockId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode = '" + pInfo.ProductCode + "'";
            }

            DataTable dt = new DataTable();
            var LProductStock = new List<ProductStockListReturn>();

            try
            {
                string strsql = " select p.*,s.ProductName from " + dbName + ".dbo.ProductStock p " +

                                " left join Product s on s.ProductCode =  p.ProductCode" +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProductStock = (from DataRow dr in dt.Rows

                             select new ProductStockListReturn()
                             {
                                 ProductStockId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                 Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                 Intransit = (dr["Intransit"].ToString() != "") ? Convert.ToInt32(dr["Intransit"]) : 0,
                                 Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
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

            return LProductStock;
        }

        public int? CountProductStockListByCriteria(ProductStockInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.ProductStockId != null) && (pInfo.ProductStockId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductStockId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LProductStock = new List<ProductStockListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countProductStock from " + dbName + ".dbo.Product p " +

                                " left join Product s on s.ProductCode =  p.ProductCode" +
                                " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProductStock = (from DataRow dr in dt.Rows

                             select new ProductStockListReturn()
                             {
                                 countProductStock = Convert.ToInt32(dr["countProductStock"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LProductStock.Count > 0)
            {
                count = LProductStock[0].countProductStock;
            }

            return count;
        }

        public int DeleteProductstock(ProductStockInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.ProductStock set FlagDelete = 'Y' where Id in (" + pInfo.ProductStockId + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertProductStock(ProductStockInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO ProductStock  (ProductCode,Balance,QTY,Reserved,Intransit,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.ProductCode + "'," +
                           "'" + pInfo.Balance + "'," +
                           "'" + pInfo.QTY + "'," +
                           "'" + pInfo.Reserved + "'," +
                           "'" + pInfo.Intransit + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "',"  +
                           "'" + pInfo.CreateBy + "'," +
                           "'N'" +
                            ")";
                       //found wrong insert position between createBy and flagDelete, switched them already
                        

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateProductStock(ProductStockInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.ProductStock set ";

            if ((pInfo.QTY != null) && (pInfo.QTY != 0))
            {
                strsql += " QTY = '" + pInfo.QTY + "',";
            }
            if ((pInfo.Reserved != null) && (pInfo.Reserved != 0))
            {
                strsql += " Reserved = '" + pInfo.Reserved + "',";
            }
            if ((pInfo.Intransit != null) && (pInfo.Intransit != 0))
            {
                strsql += " Intransit = '" + pInfo.Intransit + "',";
            }
            if ((pInfo.Balance != null) && (pInfo.Balance != 0))
            {
                strsql += " Balance = '" + pInfo.Balance + "',";
            }
            strsql += " UpdateDate =  '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                      " UpdateBy = '" + pInfo.UpdateBy + "'" +
                      " where Id =" + pInfo.ProductStockId + "";



            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}

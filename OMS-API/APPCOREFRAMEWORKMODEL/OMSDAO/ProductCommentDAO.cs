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
    public class ProductCommentDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<ProductCommentInfo> ProductCommentListNoPagingCriteria(ProductCommentInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  m.ProductCode =" + "'" + pInfo.ProductCode + "'";
            }
            DataTable dt = new DataTable();
            var ProductCommentR = new List<ProductCommentInfo>();

            try
            {
                string strsql = " select m.*,r.Rating from " + dbName + ".dbo.ProductComment m  LEFT JOIN "  + dbName + ".dbo.ProductRating r  ON m.ProductCode = r.ProductCode and m.CustomerCode = r.CustomerCode " + " where m.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY m.Id DESC ";



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ProductCommentR = (from DataRow dr in dt.Rows

                             select new ProductCommentInfo()
                             {
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 ProductComment = dr["ProductComment"].ToString().Trim(), 
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim(), 
                                 Rating = Convert.ToInt32(dr["Rating"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ProductCommentR;
        }
        //INSERT DATATO BASE
        public int InsertProductComment(ProductCommentInfo PfInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO ProductComment  (ProductCode,ProductComment,CustomerCode,FlagDelete,CreateDate,CreateBy,UpdateDate,UpdateBy )" +
                           " OUTPUT inserted.Id VALUES (" +
                           "'" + PfInfo.ProductCode + "'," +
                           "'" + PfInfo.ProductComment + "'," +
                           "'" + PfInfo.CustomerCode + "'," + 
                           "'" + PfInfo.FlagDelete + "'," + 
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + PfInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + PfInfo.UpdateBy + "')"
                           ;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertProductCommentR(ProductCommentRatingInfo RfInfo) {
            int i = 0;
            string strsql = "INSERT INTO ProductRating  (ProductCode,Rating,Active,CreateDate,CreateBy,UpdateDate,UpdateBy )" +
                           " OUTPUT inserted.Id VALUES (" +
                           "'" + RfInfo.ProductCode + "'," +
                           "'" + RfInfo.Rating + "'," +
                           "'" + RfInfo.Active + "'," + 
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + RfInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + RfInfo.UpdateBy + "')"
                           ;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}

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
using System.IO;

namespace APPCOREMODEL.OMSDAO
{
    public class PromotionImageDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<PromotionImageListReturn> GetPromotionImageUrl(PromotionImageInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  (p.PromotionCode = '" + pInfo.PromotionCode + "')";
            }

            DataTable dt = new DataTable();
            var LPromotionImage = new List<PromotionImageListReturn>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.PromotionImage p " +
                                " where (p.FlagDelete ='N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionImage = (from DataRow dr in dt.Rows

                                 select new PromotionImageListReturn()
                                 {
                                     PromotionImageId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                     PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                     PromotionImageUrl = dr["PromotionImageUrl"].ToString().Trim(),
                                     PromotionImageName = dr["PromotionImageName"].ToString().Trim(),
                                 }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return LPromotionImage;
        }
        public List<PromotionImageListReturn> ListPromotionImageValidate(PromotionImageInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  (p.PromotionCode = '" + pInfo.PromotionCode + "')";
            }

            DataTable dt = new DataTable();
            var LPromotionImage = new List<PromotionImageListReturn>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.PromotionImage p " +
                                " where (p.FlagDelete ='N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionImage = (from DataRow dr in dt.Rows

                                 select new PromotionImageListReturn()
                                 {
                                     
                                     PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                     PromotionImageUrl = dr["PromotionImageUrl"].ToString().Trim(),
                                     
                                 }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return LPromotionImage;
        }
        public int? InsertPromotionImage(PromotionImageInfo pInfo)
        {
            int? i = 0;

            string strsql = "INSERT INTO " + dbName + " .dbo.PromotionImage  (PromotionCode,PromotionImageUrl,PromotionImageName,FlagDelete)" +
                           "VALUES (" +
                          "'" + pInfo.PromotionCode + "'," +
                          "'" + pInfo.PromotionImageUrl + "'," +
                          "'" + pInfo.PromotionImageName + "'," +
                          "'" + pInfo.FlagDelete + "')";

            DataTable dt = new DataTable();
            var LPromotionImage = new List<PromotionImageInfo>();

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdatePromotionImage(PromotionImageInfo pInfo)
        {
            int? i = 0;
            string strsql = "";

            if ((pInfo.PromotionImageName != "") && (pInfo.PromotionImageName != null))
            {
                if ((pInfo.PromotionImageUrl != "") && (pInfo.PromotionImageUrl != null))
                {
                    strsql = " Update " + dbName + ".dbo.PromotionImage set " +
                                    " PromotionImageUrl = '" + pInfo.PromotionImageUrl + "'," +
                                    " PromotionImageName = '" + pInfo.PromotionImageName + "'," +
                                    " FlagDelete = '" + pInfo.FlagDelete + "'" +
                                    " where PromotionCode ='" + pInfo.PromotionCode + "'";
                }
            }
            else
            {
                strsql = " Update " + dbName + ".dbo.PromotionImage set " +
                                
                                " FlagDelete = '" + pInfo.FlagDelete + "'" +
                                " where PromotionCode ='" + pInfo.PromotionCode + "'";
            }

            DataTable dt = new DataTable();
            var LPromotionImage = new List<PromotionImageInfo>();

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
   
    }
}

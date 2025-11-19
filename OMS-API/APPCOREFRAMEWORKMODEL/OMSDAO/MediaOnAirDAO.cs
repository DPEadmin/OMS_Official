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
    public class MediaOnAirDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateMediaOnAir(MediaOnAirInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.MediaOnAir set " +
                            " ChannelCode = '" + pInfo.ChannelCode + "'," +
                            " StatusActive = '" + pInfo.StatusActive + "'," +
                             " StartTime = CONVERT(DATETIME, '" + pInfo.StartTime + "', 103)," +
                            " EndTime = CONVERT(DATETIME, '" + pInfo.EndTime + "', 103)," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where MediaOnAirId =" + pInfo.MediaOnAirId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteMediaOnAir(MediaOnAirInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.MediaOnAir set FlagDelete = 'Y' where Id in (" + pInfo.MediaOnAirId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertMediaOnAir(MediaOnAirInfo pInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO MediaOnAir  (ChannelCode,StatusActive,StartTime,EndTime,MediaPhone,MediaSaleChannel,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.ChannelCode + "'," +
                           "'" + pInfo.StatusActive + "'," +
                            "  CONVERT(DATETIME, '" + pInfo.StartTime + "', 103),"+
                             "  CONVERT(DATETIME, '" + pInfo.EndTime + "', 103)," +
                               "'" + pInfo.MediaPhone + "'," +
                           "'" + pInfo.MediaSaleChannel + "'," +

                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                
                           "'" + pInfo.FlagDelete + "'" +
               

                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<MediaOnAirListReturn> ListMediaOnAirNoPagingByCriteria(MediaOnAirInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.MediaOnAirId != null) && (pInfo.MediaOnAirId != 0))
            {
                strcond += " and  m.MediaOnAirId =" + pInfo.MediaOnAirId;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  m.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
        
     
            if (((pInfo.StartTime != "") && (pInfo.StartTime != null)) && ((pInfo.EndTime != "") && (pInfo.EndTime != null)))
            {
                strcond += " AND m.StartTime BETWEEN CONVERT(DATETIME, '" + pInfo.StartTime + "',103) AND CONVERT(DATETIME,'" + pInfo.EndTime + " 23:59:59:999',103)";
            }


            if ((pInfo.StatusActive != null) && (pInfo.StatusActive != ""))
            {
                strcond += " and  m.StatusActive like '%" + pInfo.StatusActive + "%'";
            }


            DataTable dt = new DataTable();
            var LMediaOnAir = new List<MediaOnAirListReturn>();

            try
            {
                string strsql = " select  m.MediaOnAirId,m.ChannelCode,m.FlagDelete,m.CreateBy,m.createdate,m.EndTime,m.StartTime,m.StatusActive,c.ChannelName,mc.name_th as MediaSaleChannel,m.MediaPhone" +
                    ",mc.code as  MediaSaleChannelcode  " +
                    "from " + dbName + ".dbo.MediaOnAir m " +
                                " inner join Channel c on  c.ChannelCode=m.ChannelCode AND c.FlagDelete = 'N'  " +
                                  "  inner join MediaSaleChannel AS mc ON mc.code = m.MediaSaleChannel and mc.active = 'Y' "  +


                               " where m.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY m.MediaOnAirId DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaOnAir = (from DataRow dr in dt.Rows

                             select new MediaOnAirListReturn()
                             {
                                 MediaOnAirId = (dr["MediaOnAirId"].ToString() != "") ? Convert.ToInt32(dr["MediaOnAirId"]) : 0,
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),                           
                                 CreateBy = dr["CreateBy"].ToString(),                               
                                 //UpdateBy = dr["UpdateBy"].ToString(),                             
                                 FlagDelete = dr["FlagDelete"].ToString(),             
                                 StatusActive = dr["StatusActive"].ToString(),
                                 StartTime = dr["StartTime"].ToString(),
                                 EndTime = dr["EndTime"].ToString(),
                                 MediaPhone = dr["MediaPhone"].ToString().Trim(),
                                 MediaSaleChannel = dr["MediaSaleChannel"].ToString(),
                                 MediaSaleChannelcode = dr["MediaSaleChannel"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMediaOnAir;
        }

        public List<MediaOnAirListReturn> GetMediaOnAirTakeOrder(MediaOnAirInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.MediaOnAirId != null) && (pInfo.MediaOnAirId != 0))
            {
                strcond += " and  m.MediaOnAirId =" + pInfo.MediaOnAirId;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  ma.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }


            if (((pInfo.StartTime != "") && (pInfo.StartTime != null)) && ((pInfo.EndTime != "") && (pInfo.EndTime != null)))
            {
                strcond += " AND ma.StartTime BETWEEN CONVERT(DATETIME, '" + pInfo.StartTime + "',103) AND CONVERT(DATETIME,'" + pInfo.EndTime + " 23:59:59:999',103)";
            }


            if ((pInfo.StatusActive != null) && (pInfo.StatusActive != ""))
            {
                strcond += " and  ma.StatusActive like '%" + pInfo.StatusActive + "%'";
            }

            if ((pInfo.StartTime != null) && (pInfo.StartTime != ""))
            {
                strcond += " AND (ma.StartTime <= CONVERT(VARCHAR(10), CONVERT(DATE, '" + pInfo.StartTime + "', 103), 102)) AND (ma.EndTime >= CONVERT(VARCHAR(10), CONVERT(DATE, '" + pInfo.StartTime + "', 103), 102))";
            }
            if ((pInfo.MediaPhone != null) && (pInfo.MediaPhone != "") && (pInfo.MediaPhone != "-99"))
            {
                strcond += " and  m.MediaPhone ='" + pInfo.MediaPhone + "'";
            }


            DataTable dt = new DataTable();
            var LMediaOnAir = new List<MediaOnAirListReturn>();

            try
            {
                string strsql = "SELECT m.MediaPhone, m.CampaignCode, m.PromotionCode, CONVERT(varchar(10), c.StartDate, 103) AS CampaignMediaStartDate, CONVERT(varchar(10), c.ExpireDate, 103) AS CampaignMediaExpireDate, " +
                                "CONVERT(char(5), m.MediaPlanTime, 108) AS MediaPlanTime " +
                                "FROM " + dbName + ".dbo.MediaPlan AS m LEFT OUTER JOIN " +
                                dbName + ".dbo.Campaign AS c ON c.CampaignCode = m.CampaignCode  " +
                             
                                " where m.FlagDelete = 'N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaOnAir = (from DataRow dr in dt.Rows

                               select new MediaOnAirListReturn()
                               {
                                   
                                   CampaignMediaStartDate = dr["CampaignMediaStartDate"].ToString(),
                                   CampaignMediaExpireDate = dr["CampaignMediaExpireDate"].ToString(),
                                   MediaPlanTime = dr["MediaPlanTime"].ToString(),
                                   CampaignCode = dr["CampaignCode"].ToString(),
                                   PromotionCode = dr["PromotionCode"].ToString(),
                                   MediaPhone = dr["MediaPhone"].ToString(),

                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMediaOnAir;
        }


        public int? CountMediaOnAirListByCriteria(MediaOnAirInfo pInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((pInfo.MediaOnAirId != null) && (pInfo.MediaOnAirId != 0))
            {
                strcond += " and  m.MediaOnAirId =" + pInfo.MediaOnAirId;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != ""))
            {
                strcond += " and  m.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }


            if (((pInfo.StartTime != "") && (pInfo.StartTime != null)) && ((pInfo.EndTime != "") && (pInfo.EndTime != null)))
            {
                strcond += " AND m.StartTime BETWEEN CONVERT(DATETIME, '" + pInfo.StartTime + "',103) AND CONVERT(DATETIME,'" + pInfo.EndTime + " 23:59:59:999',103)";
            }


            if ((pInfo.StatusActive != null) && (pInfo.StatusActive != ""))
            {
                strcond += " and  m.StatusActive like '%" + pInfo.StatusActive + "%'";
            }

            DataTable dt = new DataTable();
            var LMediaOnAir = new List<MediaOnAirListReturn>();


            try
            {
                string strsql = "select count(m.MediaOnAirId) as countMediaOnAir" +
                                " from " + dbName + ".dbo.MediaOnAir m " +
                                " inner join Channel c on  c.ChannelCode=m.ChannelCode AND c.FlagDelete = 'N'  " +
                                " where m.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaOnAir = (from DataRow dr in dt.Rows

                               select new MediaOnAirListReturn()
                               {
                                   countMediaOnAir = Convert.ToInt32(dr["countMediaOnAir"])
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LMediaOnAir.Count > 0)
            {
                count = LMediaOnAir[0].countMediaOnAir;
            }

            return count;
        }

        

        public int? CountListMediaOnAir(MediaOnAirInfo pInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((pInfo.MediaOnAirId != null) && (pInfo.MediaOnAirId != 0))
            {
                strcond += " and  m.MediaOnAirId =" + pInfo.MediaOnAirId;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != ""))
            {
                strcond += " and  m.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }


            if (((pInfo.StartTime != "") && (pInfo.StartTime != null)) && ((pInfo.EndTime != "") && (pInfo.EndTime != null)))
            {
                strcond += " AND m.StartTime BETWEEN CONVERT(DATETIME, '" + pInfo.StartTime + "',103) AND CONVERT(DATETIME,'" + pInfo.EndTime + " 23:59:59:999',103)";
            }


            if ((pInfo.StatusActive != null) && (pInfo.StatusActive != ""))
            {
                strcond += " and  m.StatusActive like '%" + pInfo.StatusActive + "%'";
            }

            DataTable dt = new DataTable();
            var LMediaOnAir = new List<MediaOnAirListReturn>();


            try
            {
                string strsql = "select count(m.MediaOnAirId) as countMediaOnAir" +
                                " from " + dbName + ".dbo.MediaOnAir m " +
                                " inner join Channel c on  c.ChannelCode=m.ChannelCode AND c.FlagDelete = 'N'  " +
                                " where m.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaOnAir = (from DataRow dr in dt.Rows

                               select new MediaOnAirListReturn()
                               {
                                   countMediaOnAir = Convert.ToInt32(dr["countMediaOnAir"])
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LMediaOnAir.Count > 0)
            {
                count = LMediaOnAir[0].countMediaOnAir;
            }

            return count;
        }

        
        

        public int DeleteMediaOnAirList(MediaOnAirInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.MediaOnAir set FlagDelete = 'Y' where MediaOnAirId in (" + pInfo.MediaOnAirId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<MediaSaleChannelReturnInfo> ListMediaSaleChannelByCriteria(MediaSaleChannelInfo pInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LMediaOnAir = new List<MediaSaleChannelReturnInfo>();

            try
            {
                string strsql = " select  m.id,m.CODE,m.NAME_TH,m.NAME_EN " +
                               "from " + dbName + ".dbo.MediaSaleChannel m " +

                               " where m.ACTIVE ='Y' " + strcond;

                strsql += " ORDER BY m.NAME_TH DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaOnAir = (from DataRow dr in dt.Rows

                               select new MediaSaleChannelReturnInfo()
                               {
                                   ID = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                   CODE = dr["CODE"].ToString().Trim(),
                                   NAME_TH = dr["NAME_TH"].ToString().Trim(),
                                   NAME_EN = dr["NAME_EN"].ToString(),
                               
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMediaOnAir;
        }
    }
}

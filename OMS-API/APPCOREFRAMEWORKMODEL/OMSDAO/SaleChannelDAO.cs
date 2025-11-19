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
    public class SaleChannelDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateSaleChannel(SaleChannelInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.SaleChannel set " +
                            " ChannelCode = '" + pInfo.ChannelCode + "'," +
                            " StatusActive = '" + pInfo.StatusActive + "'," +
                             " StartTime = CONVERT(DATETIME, '" + pInfo.StartTime + "', 103)," +
                            " EndTime = CONVERT(DATETIME, '" + pInfo.EndTime + "', 103)," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                            " SaleChannelCode = '" + pInfo.SaleChannelCode + "'," +
                            " SaleChannelName = '" + pInfo.SaleChannelName + "'," +
                             " SaleChannelPhone = '" + pInfo.SaleChannelPhone + "'," +
                        
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where SaleChannelid =" + pInfo.SaleChannelid + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSaleChannel(SaleChannelInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SaleChannel set FlagDelete = 'Y' where Id in (" + pInfo.SaleChannelid + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertSaleChannel(SaleChannelInfo pInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO SaleChannel  (ChannelCode,StatusActive,StartTime,EndTime,SaleChannelCode,SaleChannelName,SaleChannelPhone,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.ChannelCode + "'," +
                           "'" + pInfo.StatusActive + "'," +
                            "  CONVERT(DATETIME, '" + pInfo.StartTime + "', 103),"+
                             "  CONVERT(DATETIME, '" + pInfo.EndTime + "', 103)," +
                                 "'" + pInfo.SaleChannelCode + "'," +
                           "'" + pInfo.SaleChannelName + "'," +
                             "'" + pInfo.SaleChannelPhone + "'," +
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

        public List<SaleChannelListReturn> ListSaleChannelNoPagingByCriteria(SaleChannelInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.SaleChannelid != null) && (pInfo.SaleChannelid != 0))
            {
                strcond += " and  m.SaleChannelid =" + pInfo.SaleChannelid;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  m.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
            if ((pInfo.SaleChannelCode != null) && (pInfo.SaleChannelCode != "") )
            {
                strcond += " and  m.SaleChannelCode like '%" + pInfo.SaleChannelCode + "%'";
            }

            if ((pInfo.SaleChannelName != null) && (pInfo.SaleChannelName != ""))
            {
                strcond += " and  m.SaleChannelName like '%" + pInfo.SaleChannelName + "%'";
            }

            if ((pInfo.SaleChannelPhone != null) && (pInfo.SaleChannelPhone != ""))
            {
                strcond += " and  m.SaleChannelPhone like '%" + pInfo.SaleChannelPhone + "%'";
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
            var LSaleChannel = new List<SaleChannelListReturn>();

            try
            {
                string strsql = " select  m.SaleChannelid,m.ChannelCode,m.FlagDelete,m.CreateBy,m.createdate,m.EndTime,m.StartTime,m.StatusActive,c.ChannelName,m.SaleChannelCode," +
                    " m.SaleChannelName,m.SaleChannelPhone  " +
                    "from " + dbName + ".dbo.SaleChannel m " +
                                " inner join Channel c on  c.ChannelCode=m.ChannelCode AND c.FlagDelete = 'N'  " +
                    
                             
                               " where m.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY m.SaleChannelid DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSaleChannel = (from DataRow dr in dt.Rows

                             select new SaleChannelListReturn()
                             {
                                 SaleChannelid = (dr["SaleChannelid"].ToString() != "") ? Convert.ToInt32(dr["SaleChannelid"]) : 0,
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),                           
                                 CreateBy = dr["CreateBy"].ToString(),
                                 
                                 FlagDelete = dr["FlagDelete"].ToString(),             
                                 StatusActive = dr["StatusActive"].ToString(),
                                 StartTime = dr["StartTime"].ToString(),
                                 EndTime = dr["EndTime"].ToString(),

                                 SaleChannelCode = dr["SaleChannelCode"].ToString(),
                                 SaleChannelName = dr["SaleChannelName"].ToString(),
                                 SaleChannelPhone = dr["SaleChannelPhone"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSaleChannel;
        }



        public int? CountSaleChannelListByCriteria(SaleChannelInfo pInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((pInfo.SaleChannelid != null) && (pInfo.SaleChannelid != 0))
            {
                strcond += " and  m.SaleChannelid =" + pInfo.SaleChannelid;
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
            var LSaleChannel = new List<SaleChannelListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countSaleChannel" +
                                "from " + dbName + ".dbo.SaleChannel m " +
                                " inner join Channel c on  c.ChannelCode=m.ChannelCode AND c.FlagDelete = 'N'  " +
                                " where m.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSaleChannel = (from DataRow dr in dt.Rows

                               select new SaleChannelListReturn()
                               {
                                   countSaleChannel = Convert.ToInt32(dr["countSaleChannel"])
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LSaleChannel.Count > 0)
            {
                count = LSaleChannel[0].countSaleChannel;
            }

            return count;
        }
        public int DeleteSaleChannelList(SaleChannelInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SaleChannel set FlagDelete = 'Y' where SaleChannelid in (" + pInfo.SaleChannelidList + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountListSaleChannel(SaleChannelInfo pInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((pInfo.SaleChannelid != null) && (pInfo.SaleChannelid != 0))
            {
                strcond += " and  m.SaleChannelId =" + pInfo.SaleChannelid;
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
            var LSaleChannel = new List<SaleChannelListReturn>();


            try
            {
                string strsql = "select count(m.SaleChannelId) as countSaleChannel" +
                                " from " + dbName + ".dbo.SaleChannel m " +
                                " inner join Channel c on  c.ChannelCode=m.ChannelCode AND c.FlagDelete = 'N'  " +
                                " where m.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSaleChannel = (from DataRow dr in dt.Rows

                               select new SaleChannelListReturn()
                               {
                                   countSaleChannel = Convert.ToInt32(dr["countSaleChannel"])
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LSaleChannel.Count > 0)
            {
                count = LSaleChannel[0].countSaleChannel;
            }

            return count;
        }
    }
}

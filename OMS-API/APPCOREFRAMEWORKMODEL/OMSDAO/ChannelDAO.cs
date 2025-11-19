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
    public class ChannelDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateChannel(ChannelInfo cInfo)
        {
            int i = 0;
            
            string strsql = " Update " + dbName + ".dbo.Channel set " +
                           " ChannelCode = '" + cInfo.ChannelCode + "'," +
                           " ChannelName = '" + cInfo.ChannelName + "'," +
                          
                          
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.ChannelId + "";
            

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteChannel(ChannelInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Channel set FlagDelete = 'Y' where Id in (" + cInfo.ChannelIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertChannel(ChannelInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Channel  (ChannelCode,ChannelName,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,MerchantCode)" +
                            "VALUES (" +
                           "'" + cInfo.ChannelCode + "'," +
                           "'" + cInfo.ChannelName + "'," +
                          
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'N'," +
                           "'" + cInfo.MerchantCode + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<ChannelListReturn> ListChannelNoPagingByCriteria(ChannelInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.ChannelId != null) && (cInfo.ChannelId != 0))
            {
                strcond += " and  c.Id =" + cInfo.ChannelId;
            }

            if ((cInfo.ChannelCode != null) && (cInfo.ChannelCode != ""))
            {
                strcond += " and  c.ChannelCode like '%" + cInfo.ChannelCode + "%'";
            }
            if ((cInfo.ChannelName != null) && (cInfo.ChannelName != ""))
            {
                strcond += " and  c.ChannelName like '%" + cInfo.ChannelName + "%'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  me.MerchantCode like '%" + cInfo.MerchantCode + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<ChannelListReturn>();

            try
            {
                string strsql = //" select c.* from " + dbName + ".dbo.Channel c " +

                                //" where c.FlagDelete ='N' " + strcond
                                " select c.*,me.MerchantName from " + dbName + ".dbo.Channel c " +
                                
                                " inner join Merchant as me on me.MerchantCode = c.MerchantCode " +


                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                            select new ChannelListReturn()
                            {
                                ChannelId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                ChannelName = dr["ChannelName"].ToString().Trim(),
                              
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                              
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<ChannelListReturn> ListChannelPagingByCriteria(ChannelInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.ChannelId != null) && (cInfo.ChannelId != 0))
            {
                strcond += " and  c.Id =" + cInfo.ChannelId;
            }
            if ((cInfo.ChannelCode_val != null) && (cInfo.ChannelCode_val != ""))
            {
                strcond += " and  c.ChannelCode = '" + cInfo.ChannelCode_val + "'";
            }
            if ((cInfo.ChannelCode != null) && (cInfo.ChannelCode != ""))
            {
                strcond += " and  c.ChannelCode like '%" + cInfo.ChannelCode + "%'";
            }
            if ((cInfo.ChannelName != null) && (cInfo.ChannelName != ""))
            {
                strcond += " and  c.ChannelName like '%" + cInfo.ChannelName + "%'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  me.MerchantCode like '%" + cInfo.MerchantCode + "%'";
            }



            DataTable dt = new DataTable();
            var LCampaign = new List<ChannelListReturn>();

            try
            {
                string strsql = " select c.*,me.MerchantName from " + dbName + ".dbo.Channel c " +
                                //" inner join  MediaPlan as mp on mp.SALE_CHANNEL = c.ChannelCode " +
                                " inner join Merchant as me on me.MerchantCode = c.MerchantCode " +


                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                if (cInfo.rowOFFSet != 0)
                {
                    strsql += "OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";
                }
                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ChannelListReturn()
                             {
                                 ChannelId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int? CountChannelListByCriteria(ChannelInfo cInfo) {
            string strcond = "";
            int? count = 0;

            if ((cInfo.ChannelId != null) && (cInfo.ChannelId != 0))
            {
                strcond += " and  c.Id =" + cInfo.ChannelId;
            }

            if ((cInfo.ChannelCode != null) && (cInfo.ChannelCode != ""))
            {
                strcond += " and  c.ChannelCode collate SQL_Latin1_General_CP1_CS_AS like '%" + cInfo.ChannelCode + "%'";
            }
            if ((cInfo.ChannelName != null) && (cInfo.ChannelName != ""))
            {
                strcond += " and  c.ChannelName like '%" + cInfo.ChannelName + "%'";
            }

          

            DataTable dt = new DataTable();
            var LCampaign = new List<ChannelListReturn>();


            try
            {
                string strsql = "select count(c.Id) as countChannel from " + dbName + ".dbo.Channel c " +

                         " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ChannelListReturn()
                             {
                                 countChannel = Convert.ToInt32(dr["countChannel"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countChannel;
            }

            return count;
        }

     
        public List<ChannelListReturn> ChannelCodeValidateInsert(ChannelInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.ChannelCode != null) && (cInfo.ChannelCode != ""))
            {
                strcond += " and  c.ChannelCode = '" + cInfo.ChannelCode + "'";
            }
            if ((cInfo.ChannelName != null) && (cInfo.ChannelName != ""))
            {
                strcond += " and  c.ChannelName = '" + cInfo.ChannelName + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<ChannelListReturn>();

            try
            {
                string strsql = " select c.ChannelCode from " + dbName + ".dbo.Channel c " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ChannelListReturn()
                             {
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int InsertChannelImport(List<ChannelInfo> lcinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in lcinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.Channel (ChannelCode,CamCate_name,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete) values (" +
                             "'" + info.ChannelCode + "', " +
                             "'" + info.ChannelName + "', " +
                                                  
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.UpdateBy + "', " +
                             "'" + info.FlagDelete + "')";
                lSQL.Add(strsql);
            }

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            foreach (string strq in lSQL)
            {
                com.CommandText = strq;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            }
            return i;
        }

        public List<ChannelListReturn> ChannelListNoPagingCriteria(ChannelInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.ChannelId != null) && (cInfo.ChannelId != 0))
            {
                strcond += " and  c.Id =" + cInfo.ChannelId;
            }

            if ((cInfo.ChannelCode != null) && (cInfo.ChannelCode != ""))
            {
                strcond += " and  c.ChannelCode like '%" + cInfo.ChannelCode + "%'";
            }
            if ((cInfo.ChannelName != null) && (cInfo.ChannelName != ""))
            {
                strcond += " and  c.ChannelName like '%" + cInfo.ChannelName + "%'";
            }

            DataTable dt = new DataTable();
            var LMerchant = new List<ChannelListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Channel c " +
                                    " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new ChannelListReturn()
                             {
                                 ChannelId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }

        public List<swaggerInfo> ChannelListNoPagingCriteriaSwagger()
        {
            string strcond = "";

            

            DataTable dt = new DataTable();
            var LMerchant = new List<swaggerInfo>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Channel c " +
                                    " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new swaggerInfo()
                             {
                                 ChannelId = dr["id"].ToString().Trim(),
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),
                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;
        }
    }
   
}

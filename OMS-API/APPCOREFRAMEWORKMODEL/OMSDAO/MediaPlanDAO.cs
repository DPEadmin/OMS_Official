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
    public class MediaPlanDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int InsertMediaPlan(MediaPlanInfo mInfo)
        {
            int i = 0;
            

            string strsql = "INSERT INTO " + dbName + ".dbo.MediaPlan  " +
                "(MediaPlanDate,MediaPlanTime,ProgramName,SALE_CHANNEL," +
                "Duration,MediaPhone,CampaignCode,CreateBy,UpdateDate,UpdateBy,FlagDelete,CreateDate,Active," +
                "TimeStart,TimeEnd,DelayStartTime,DelayEndTime,MEDIA_CHANNEL,FlagApprove,MediaPlanEndDate,MerchantCode)" +
                               "VALUES (" +
                              "CONVERT(DATETIME,'" + mInfo.MediaPlanDate + "',103), " +
                              "CONVERT(DATETIME,'" + mInfo.MediaPlanDate + " " + mInfo.TIME_START + "',103)," +
                              "'" + mInfo.ProgramName + "'," +            
                              "'" + mInfo.SALE_CHANNEL + "'," +
                              "'" + mInfo.Duration + "'," +
                              "'" + mInfo.MediaPhone + "'," +
                              "'" + mInfo.CampaignCode + "'," +
                              "'" + mInfo.CreateBy + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.UpdateBy + "'," +
                              "'" + "N" + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                                  "'" + mInfo.Active + "'," +
                                   "'" + mInfo.TIME_START + "'," +
                                    "'" + mInfo.TIME_END + "'," +
                                     "'" + mInfo.DelayStartTime + "'," +
                                      "'" + mInfo.DelayEndTime + "'," +
                                       "'" + mInfo.MEDIA_CHANNEL + "'," +
                                             "'" + "N" + "'," +
                              "CONVERT(DATETIME,'" + mInfo.MediaPlanEndDate + "',103), " +
                                             "'" + mInfo.MerchantCode + "'" + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertMediaPlanList(MediaPlanInfo mInfo)
        {

            try
            {
                int i = 0;

                string strsql = "INSERT INTO " + dbName + ".dbo.MediaPlan  " +
                "(MediaPlanDate,MediaPlanTime,ProgramName,SALE_CHANNEL," +
                "Duration,MediaPhone,CampaignCode,CreateBy,UpdateDate,UpdateBy,FlagDelete,CreateDate,Active," +
                "TimeStart,TimeEnd,MEDIA_CHANNEL,FlagApprove,MediaPlanEndDate,MerchantCode)" +
                               "VALUES (" +
                              "CONVERT(DATETIME,'" + mInfo.MediaPlanDate + "',103), " +
                              "CONVERT(DATETIME,'" + mInfo.MediaPlanDate + " " + mInfo.TIME_START + "',103)," +
                              "'" + mInfo.ProgramName + "'," +
                              "'" + mInfo.SALE_CHANNEL + "'," +
                              "'" + mInfo.Duration + "'," +
                              "'" + mInfo.MediaPhone + "'," +
                              "'" + mInfo.CampaignCode + "'," +
                              "'" + mInfo.CreateBy + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.UpdateBy + "'," +
                              "'" + "N" + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                                  "'" + mInfo.Active + "'," +
                                   "'" + mInfo.TIME_START + "'," +
                                    "'" + mInfo.TIME_END + "'," +
                                       "'" + mInfo.MEDIA_CHANNEL + "'," +
                                             "'" + "N" + "'," +
                              "CONVERT(DATETIME,'" + mInfo.MediaPlanEndDate + "',103), " +
                                             "'" + mInfo.MerchantCode + "'" + ")";

                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);

                return i;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public List<MediaPlanListReturn> ListMediaPlanNoPagingByCriteria(MediaPlanInfo mInfo)
        {
            string strcond = "";

            

            if (((mInfo.MediaPlanDate != "") && (mInfo.MediaPlanDate != null)) && ((mInfo.MediaPlanDate != "") && (mInfo.MediaPlanDate != null)))
            {
                strcond += " AND m.MediaPlanDate BETWEEN CONVERT(DATETIME, '" + mInfo.MediaPlanDate + "',103) AND CONVERT(DATETIME,'" + mInfo.MediaPlanDate + " 23:59:59:999',103)";
            }

            if ((mInfo.MediaPlanTime != null) && (mInfo.MediaPlanTime != ""))
            {
                strcond += " and  m.MediaPlanTime like '%" + mInfo.MediaPlanTime + "%'";
            }

            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode = '" + mInfo.MerchantCode + "'";
            }

            if ((mInfo.ProgramName != null) && (mInfo.ProgramName != ""))
            {
                strcond += " and  m.ProgramName like '%" + mInfo.ProgramName + "%'";
            }
         
            if ((mInfo.Duration != null) && (mInfo.Duration != ""))
            {
                strcond += " and  m.Duration like '%" + mInfo.Duration + "%'";
            }
            
            if ((mInfo.MediaPhone != null) && (mInfo.MediaPhone != ""))
            {
                strcond += " and  m.MediaPhone like '%" + mInfo.MediaPhone + "%'";
            }
            if (((mInfo.MediaPlanDateStart != "") && (mInfo.MediaPlanDateStart != null)) && ((mInfo.MediaPlanDateEnd != "") && (mInfo.MediaPlanDateEnd != null)))
            {
                strcond += " AND m.MediaPlanDate BETWEEN CONVERT(DATETIME, '" + mInfo.MediaPlanDateStart + "',103) AND CONVERT(DATETIME,'" + mInfo.MediaPlanDateEnd + " 23:59:59:999',103)";
            }
            if ((mInfo.TIME_START != null) && (mInfo.TIME_START != ""))
            {
                strcond += " and  m.TIMESTART = '" + mInfo.TIME_START + "'";
            }
            if ((mInfo.TIME_END != null) && (mInfo.TIME_END != ""))
            {
                strcond += " and  m.TIMEEND = '" + mInfo.TIME_END + "'";
            }
            if ((mInfo.Active != null) && (mInfo.Active != "") && (mInfo.Active != "-99"))
            {
                strcond += " and  m.Active = '" + mInfo.Active + "'";
            }
            if ((mInfo.CampaignCode != null) && (mInfo.CampaignCode != "") && (mInfo.CampaignCode != "-99"))
            {
                strcond += " and  c.CampaignCode = '" + mInfo.CampaignCode + "'";
            }
            if ((mInfo.SALE_CHANNEL != null) && (mInfo.SALE_CHANNEL != "") && (mInfo.SALE_CHANNEL != "-99"))
            {
                strcond += " and  m.SALE_CHANNEL like '%" + mInfo.SALE_CHANNEL + "%'";
            }
            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaPlanListReturn>();

            try
            {
                string strsql = "SELECT m.id, m.MediaPlanDate, m.MediaPlanEndDate, m.MediaPlanTime, m.ProgramName, ch.ChannelName, " +
                    "m.SALE_CHANNEL, m.MediaType, m.Duration, p.MediaPhoneNo,m.MediaPhone, m.CampaignCode, m.PromotionCode, m.ChannelMedia," +
                    " m.ProductCode, m.BU, m.CreateBy, m.UpdateDate, m.UpdateBy, m.FlagDelete, m.Createdate,m.Active," +
                    "m.TIMESTART,m.TIMEEND,m.MEDIA_CHANNEL,mc.NAME_TH,c.CampaignName ,m.flagapprove ,me.MerchantName, m.MerchantCode ,m.DelayStartTime,m.DelayEndTime " +
                                "FROM " + dbName + ".dbo.MediaPlan AS m " +
                                " left join " +dbName + ".dbo.campaign c on c.CampaignCode = m.CampaignCode" +
                                " left join " +dbName + ".dbo.Merchant AS me on me.MerchantCode = m.MerchantCode" +
                                " left join " +dbName + ".dbo.Channel ch on ch.ChannelCode = m.SALE_CHANNEL" +
                                " left join " +dbName + ".dbo.MediaPhone p on p.MediaPhoneNo = m.MediaPhone " +
                                " left join " +dbName + ".dbo.MediaChannel mc on mc.CODE = m.MEDIA_CHANNEL" +
                                " WHERE(m.FlagDelete = 'N') AND mc.Active = 'Y' " + strcond;

                strsql += " ORDER BY m.id desc, m.active asc, m.ProgramName desc OFFSET " + mInfo.rowOFFSet + " ROWS FETCH NEXT " + mInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows
                            select new MediaPlanListReturn()
                            {
                                MediaPlanId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                MediaPlanDate = dr["MediaPlanDate"].ToString().Trim(),
                                MediaPlanEndDate = dr["MediaPlanEndDate"].ToString().Trim(),
                                MediaPlanTime = dr["MediaPlanTime"].ToString().Trim(),
                                ProgramName = dr["ProgramName"].ToString().Trim(),
                                SALE_CHANNEL = dr["ChannelName"].ToString(),
                                SALE_CHANNEL_CODE = dr["SALE_CHANNEL"].ToString(),
                                Duration = (dr["Duration"].ToString() != "") ? Convert.ToInt32(dr["Duration"]) : 0,
                                MediaPhone = dr["MediaPhone"].ToString(),
                                //MediaPhoneCode = dr["MediaPhone"].ToString(),
                                CampaignCode = dr["CampaignCode"].ToString(),
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["Createdate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                Active = dr["Active"].ToString(),
                                TIME_START = dr["TIMESTART"].ToString(),
                                TIME_END = dr["TIMEEND"].ToString(),
                                MEDIA_CHANNEL = dr["MEDIA_CHANNEL"].ToString(),
                                MEDIA_CHANNEL_NAME = dr["NAME_TH"].ToString(),
                                CampaignName = dr["CampaignName"].ToString(),
                                FlagApprove = dr["FlagApprove"].ToString(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                DelayStartTime = dr["DelayStartTime"].ToString(),
                                DelayEndTime = dr["DelayEndTime"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMediaPlan;
        }

        public int? CountListMediaPlan(MediaPlanInfo mInfo)
        {
            string strcond = "";
            int? count = 0;

            if (((mInfo.MediaPlanDate != "") && (mInfo.MediaPlanDate != null)) && ((mInfo.MediaPlanDate != "") && (mInfo.MediaPlanDate != null)))
            {
                strcond += " AND m.MediaPlanDate BETWEEN CONVERT(DATETIME, '" + mInfo.MediaPlanDate + "',103) AND CONVERT(DATETIME,'" + mInfo.MediaPlanDate + " 23:59:59:999',103)";
            }

            if ((mInfo.MediaPlanTime != null) && (mInfo.MediaPlanTime != ""))
            {
                strcond += " and  m.MediaPlanTime like '%" + mInfo.MediaPlanTime + "%'";
            }

            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode = '" + mInfo.MerchantCode + "'";
            }

            if ((mInfo.ProgramName != null) && (mInfo.ProgramName != ""))
            {
                strcond += " and  m.ProgramName like '%" + mInfo.ProgramName + "%'";
            }

            if ((mInfo.Duration != null) && (mInfo.Duration != ""))
            {
                strcond += " and  m.Duration like '%" + mInfo.Duration + "%'";
            }

            if ((mInfo.MediaPhone != null) && (mInfo.MediaPhone != ""))
            {
                strcond += " and  m.MediaPhone like '%" + mInfo.MediaPhone + "%'";
            }
            if (((mInfo.MediaPlanDateStart != "") && (mInfo.MediaPlanDateStart != null)) && ((mInfo.MediaPlanDateEnd != "") && (mInfo.MediaPlanDateEnd != null)))
            {
                strcond += " AND m.MediaPlanDate BETWEEN CONVERT(DATETIME, '" + mInfo.MediaPlanDateStart + "',103) AND CONVERT(DATETIME,'" + mInfo.MediaPlanDateEnd + " 23:59:59:999',103)";
            }
            if ((mInfo.TIME_START != null) && (mInfo.TIME_START != ""))
            {
                strcond += " and  m.TIMESTART = '" + mInfo.TIME_START + "'";
            }
            if ((mInfo.TIME_END != null) && (mInfo.TIME_END != ""))
            {
                strcond += " and  m.TIMEEND = '" + mInfo.TIME_END + "'";
            }
            if ((mInfo.Active != null) && (mInfo.Active != "") && (mInfo.Active != "-99"))
            {
                strcond += " and  m.Active = '" + mInfo.Active + "'";
            }
            if ((mInfo.CampaignCode != null) && (mInfo.CampaignCode != "") && (mInfo.CampaignCode != "-99"))
            {
                strcond += " and  c.CampaignCode = '" + mInfo.CampaignCode + "'";
            }

            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaPlanListReturn>();

            try
            {
                string strsql = " SELECT COUNT(m.id) as countMediaPlan " +
                                " FROM " + dbName + ".dbo.MediaPlan AS m " +
                                   " " +
                                " left join " + dbName + ".dbo.campaign c on c.CampaignCode = m.CampaignCode" +
                                " WHERE(m.FlagDelete = 'N') " + strcond;

           

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows

                            select new MediaPlanListReturn()
                            {
                                countMediaPlan = Convert.ToInt32(dr["countMediaPlan"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LMediaPlan.Count > 0)
            {
                count = LMediaPlan[0].countMediaPlan;
            }

            return count;
        }

        public int DeleteMediaPlanList(MediaPlanInfo mInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.MediaPlan set FlagDelete = 'Y' where Id in (" + mInfo.MediaPlanidList + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int ActiveMediaPlanList(MediaPlanInfo mInfo)
        {
            string strcond = "";
            if ((mInfo.Active != null) && (mInfo.Active != ""))
            {
                strcond += " Active = '" + mInfo.Active + "',";
            }
            if ((mInfo.UpdateBy != null) && (mInfo.UpdateBy != ""))
            {
                strcond += " UpdateBy = '" + mInfo.UpdateBy + "',";
            }

            if ((mInfo.FlagApprove != null) && (mInfo.FlagApprove != ""))
            {
                strcond += " FlagApprove = '" + mInfo.FlagApprove + "',";
            }
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.MediaPlan set  " + strcond + "  " +
                     " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                
                "where Id in (" + mInfo.MediaPlanidList + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateMediaPlan(MediaPlanInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.MediaPlan set " +
                            " MediaPlanDate = CONVERT(DATETIME,'" + pInfo.MediaPlanDate + "',103)," +
                            " MediaPlanEndDate = CONVERT(DATETIME,'" + pInfo.MediaPlanEndDate + "',103)," +
                            " MediaPlanTime = CONVERT(varchar,'" + pInfo.MediaPlanTime + "',113)," +
                             " ProgramName ='" + pInfo.ProgramName + "'," +
                            " SALE_CHANNEL = '" + pInfo.SALE_CHANNEL + "'," +
                              " Duration = '" + pInfo.Duration + "'," +
                                " MediaPhone = '" + pInfo.MediaPhone + "'," +
                                  " CampaignCode = '" + pInfo.CampaignCode + "'," +
                                   " TimeEnd = '" + pInfo.TIME_END + "'," +
                                     " TimeStart = '" + pInfo.TIME_START + "'," +
                                     " Active = '" + pInfo.Active + "'," +
                                       " MEDIA_CHANNEL = '" + pInfo.MEDIA_CHANNEL + "'," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                            " FlagApprove = '" + pInfo.FlagApprove + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where id =" + pInfo.MediaPlanId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertMediaPhone(MediaPhoneInfo mInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.MediaPhone  (MediaphoneNo,Active,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,MerchantCode)" +
                               "VALUES (" +
                           
                              "'" + mInfo.MediaPhoneNo + "'," +
                                "'" + mInfo.Active + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.CreateBy + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.UpdateBy + "'," +
                              "'" + "N" + "'," +
                              "'" + mInfo.MerchantCode + "'" +
                               ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<MediaPhoneInfo> ListMediaPhoneNoPagingByCriteria_Val(MediaPhoneInfo mInfo)
        {
            string strcond = "";

            if ((mInfo.MediaPhoneCode != null) && (mInfo.MediaPhoneId != 0))
            {
                strcond += " or  m.MediaPhoneNo = '" + mInfo.MediaPhoneCode + "'";
                
            }
            if ((mInfo.MediaPhoneId != null) && (mInfo.MediaPhoneId != 0))
            {
                strcond += " and  m.id like '%" + mInfo.MediaPhoneId + "%'";
            }
            if ((mInfo.MediaPhoneNo != null) && (mInfo.MediaPhoneNo != ""))
            {
                strcond += " and  m.MediaPhoneNo like '%" + mInfo.MediaPhoneNo + "%'";
            }
            if ((mInfo.Active != null) && (mInfo.Active != ""))
            {
                strcond += " and  m.Active like '%" + mInfo.Active + "%'";
            }
            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaPhoneInfo>();

            try
            {
                string strsql = "SELECT m.id, m.MediaPhoneNo, m.CreateBy, m.UpdateDate, m.UpdateBy, m.FlagDelete, m.Createdate,m.Active " +
                                "FROM " + dbName + ".dbo.MediaPhone AS m " +
                                " WHERE NOT m.MediaPhoneNo IN(SELECT mp.mediaphone from mediaplan mp) and m.FlagDelete = 'N'" + strcond;
                if(mInfo.rowOFFSet != 0) 
                {
                    strsql += " ORDER BY m.id desc OFFSET " + mInfo.rowOFFSet + " ROWS FETCH NEXT " + mInfo.rowFetch + " ROWS ONLY";
                }
              

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows
                              select new MediaPhoneInfo()
                              {
                                  MediaPhoneId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                  MediaPhoneNo = dr["MediaPhoneNo"].ToString().Trim(),   
                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["Createdate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  Active = dr["Active"].ToString(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMediaPlan;
        }
        public List<MediaPhoneInfo> ListMediaPhoneNoPagingByCriteria(MediaPhoneInfo mInfo)
        {
            string strcond = "";

            if ((mInfo.MediaPhoneCode != null) && (mInfo.MediaPhoneId != 0))
            {
                strcond += " and  m.MediaPhoneNo = '" + mInfo.MediaPhoneCode + "'";
            }
            if ((mInfo.MediaPhoneId != null) && (mInfo.MediaPhoneId != 0))
            {
                strcond += " and  m.id like '%" + mInfo.MediaPhoneId + "%'";
            }
            if ((mInfo.MediaPhoneNo != null) && (mInfo.MediaPhoneNo != ""))
            {
                strcond += " and  m.MediaPhoneNo like '%" + mInfo.MediaPhoneNo + "%'";
            }
            if ((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode like '%" + mInfo.MerchantCode + "%'";
            }
            if ((mInfo.Active != null) && (mInfo.Active != ""))
            {
                strcond += " and  m.Active like '%" + mInfo.Active + "%'";
            }
            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaPhoneInfo>();

            try
            {
                string strsql = "SELECT m.id, m.MediaPhoneNo, m.CreateBy, m.UpdateDate, m.UpdateBy, m.FlagDelete, m.Createdate,m.Active,m.MerchantCode " +
                                "FROM " + dbName + ".dbo.MediaPhone AS m " +
                                
                                " WHERE(m.FlagDelete = 'N') " + strcond;
                if (mInfo.rowOFFSet != 0)
                {
                    strsql += " ORDER BY m.id desc OFFSET " + mInfo.rowOFFSet + " ROWS FETCH NEXT " + mInfo.rowFetch + " ROWS ONLY";
                }


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows
                              select new MediaPhoneInfo()
                              {
                                  MediaPhoneId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                  MediaPhoneNo = dr["MediaPhoneNo"].ToString().Trim(),
                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["Createdate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  Active = dr["Active"].ToString(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMediaPlan;
        }
        public int? CountListMediaPhone(MediaPhoneInfo mInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((mInfo.MediaPhoneNo != null) && (mInfo.MediaPhoneNo != ""))
            {
                strcond += " and  m.MediaPhoneNo like '%" + mInfo.MediaPhoneNo + "%'";
            }
            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaPhoneInfo>();

            try
            {
                string strsql = " SELECT COUNT(m.id) as countMediaPhone " +
                                    "FROM " + dbName + ".dbo.MediaPhone AS m " +
                                " WHERE(m.FlagDelete = 'N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows

                              select new MediaPhoneInfo()
                              {
                                  countMediaPhone = Convert.ToInt32(dr["countMediaPhone"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LMediaPlan.Count > 0)
            {
                count = LMediaPlan[0].countMediaPhone;
            }

            return count;
        }
        public int UpdateMediaPhone(MediaPhoneInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.MediaPhone set " +

                            " MediaPhoneNo = '" + pInfo.MediaPhoneNo + "'," +
                              " Active = '" + pInfo.Active + "'," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where id =" + pInfo.MediaPhoneId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteMediaPhone(MediaPhoneInfo mInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.MediaPhone set FlagDelete = 'Y' where Id in (" + mInfo.MediaPhoneIdlist + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<MerchantInfo> ListMerchantForValByCriteria(MerchantInfo mInfo)
        {
            string strcond = "";

            if((mInfo.MerchantCode != null) && (mInfo.MerchantCode != ""))
            {
                strcond += " and m.MerchantCode = '" + mInfo.MerchantCode + "'";
            }
            if ((mInfo.MerchantName != null) && (mInfo.MerchantName != ""))
            {
                //strcond += " and m.MerchantCode = '" + mInfo.MerchantCode + "'";
                strcond = strcond == "" ? strcond += "where m.MerchantName = '" + mInfo.MerchantName.Trim() + "'" : strcond += "AND m.MerchantName = '" + mInfo.MerchantName.Trim() + "'";
            }
            DataTable dt = new DataTable();
            var LMerchant = new List<MerchantInfo>();

            try
            {
                string strsql = " select m.MerchantName, m.MerchantCode " +
                    " from " + dbName + " .dbo.Merchant AS m " +
                    " where m.FlagDelete = 'N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMerchant = (from DataRow dr in dt.Rows

                             select new MerchantInfo()
                             {
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 MerchantName = dr["MerchantName"].ToString().Trim(),
                                 
                             }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMerchant;

        }

        public List<MediaChannelInfo> ListMediaChannelNoPagingByCriteria(MediaChannelInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.Codeval != null) && (pInfo.Codeval != ""))
            {
                strcond += " and  m.Code = '" + pInfo.Codeval + "'";
            }
            if ((pInfo.Code != null) && (pInfo.Code != ""))
            {
                strcond += " and  m.Code like '%" + pInfo.Code + "%'";
            }
            if ((pInfo.name_en != null) && (pInfo.name_en != ""))
            {
                strcond += " and  m.name_en like '%" + pInfo.name_en + "%'";
            }
            if ((pInfo.name_th != null) && (pInfo.name_th != ""))
            {
                strcond += " and  m.name_th like '%" + pInfo.name_th + "%'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != "") && (pInfo.Active != "-99"))
            {
                strcond += " and  m.Active = '" + pInfo.Active + "'";
            }
            if ((pInfo.SaleChannelCode != null) && (pInfo.SaleChannelCode != ""))
            {
                strcond += " and  m.SaleChannelCode = '" + pInfo.SaleChannelCode + "'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  m.MerchantCode = '" + pInfo.MerchantCode + "'";
            }

            DataTable dt = new DataTable();
            var LSaleChannel = new List<MediaChannelInfo>();

            try
            {
                string strsql = " select m.id, m.code,m.name_th,m.name_en ,m.Active,m.MerchantCode,me.MerchantName " +
                
                    "from " + dbName + ".dbo.mediachannel m " +
                    " inner join Channel c on c.ChannelCode = m.SaleChannelCode " +
                    " inner join Merchant me on me.MerchantCode = m.MerchantCode " +
                              


                               " where m.FlagDelete ='N' " + strcond;
                if (pInfo.rowFetch > 0)
                {
                    strsql += " ORDER BY m.name_th desc OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";
                }
                else 
                {
                    strsql += " ORDER BY m.name_th DESC ";
                }
                          

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSaleChannel = (from DataRow dr in dt.Rows

                                select new MediaChannelInfo()
                                {
                                    MediaChannelId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                    MerchantCode = dr["MerchantCode"].ToString(),
                                    Code = dr["Code"].ToString().Trim(),
                                    name_en = dr["name_en"].ToString().Trim(),
                                    name_th = dr["name_th"].ToString(),
                                    Active = dr["Active"].ToString(),
                                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSaleChannel;
        }
        public List<MediaProgramInfo> ListMediaProgramNoPagingByCriteria(MediaProgramInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.MediaProgramName != null) && (pInfo.MediaProgramName != ""))
            {
                strcond += " and  m.MediaProgramName ='" + pInfo.MediaProgramName + "'";
            }

            if ((pInfo.MediaChannelCode != null) && (pInfo.MediaChannelCode != ""))
            {
                strcond += " and  m.MediaChannelCode ='" + pInfo.MediaChannelCode+"'";
            }
       


            DataTable dt = new DataTable();
            var LSaleChannel = new List<MediaProgramInfo>();

            try
            {
                string strsql = " select  m.MediaProgramName,m.MediaChannelCode " +

                    "from " + dbName + ".dbo.MediaProgram m " +



                               " where m.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY m.MediaProgramName DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSaleChannel = (from DataRow dr in dt.Rows

                                select new MediaProgramInfo()
                                {

                                    MediaChannelCode = dr["MediaChannelCode"].ToString().Trim(),
                                    MediaProgramName = dr["MediaProgramName"].ToString().Trim(),
                                   // name_th = dr["name_th"].ToString(),

                                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSaleChannel;
        }

        public int InsertMediaPhoneSub(MediaPhoneSubInfo mInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.MediaPhoneSub  (MediaPhoneSubNo,MediaphoneNo,Active,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                               "VALUES (" +
                                "'" + mInfo.MediaPhoneSubNo + "'," +
                              "'" + mInfo.MediaPhoneNo + "'," +
                                "'" + mInfo.Active + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.CreateBy + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.UpdateBy + "'," +
                              "'" + "N" + "'" +
                               ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<MediaPhoneSubInfo> ListMediaPhoneSubNoPagingByCriteria(MediaPhoneSubInfo mInfo)
        {
            string strcond = "";
            if ((mInfo.MediaPhoneSubNo != null) && (mInfo.MediaPhoneSubNo != ""))
            {
                strcond += " and  m.MediaPhoneSubNo = '" + mInfo.MediaPhoneSubNo + "'";
                
            }
            if ((mInfo.MediaPhoneNo != null) && (mInfo.MediaPhoneNo != ""))
            {
                strcond += " and  m.MediaPhoneNo like '%" + mInfo.MediaPhoneNo + "%'";
            }
            if ((mInfo.Active != null) && (mInfo.Active != ""))
            {
                strcond += " and  m.Active like '%" + mInfo.Active + "%'";
            }
            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaPhoneSubInfo>();

            try
            {
                string strsql = "SELECT m.id,m.MediaPhoneSubNo, m.MediaPhoneNo, m.CreateBy, m.UpdateDate, m.UpdateBy, m.FlagDelete, m.Createdate,m.Active " +
                                "FROM " + dbName + ".dbo.MediaPhoneSub AS m " +
                                " WHERE(m.FlagDelete = 'N') " + strcond;

                if (mInfo.rowOFFSet != 0)
                {
                    strsql += " ORDER BY m.id desc OFFSET " + mInfo.rowOFFSet + " ROWS FETCH NEXT " + mInfo.rowFetch + " ROWS ONLY";
                }
       
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows
                              select new MediaPhoneSubInfo()
                              {
                                  MediaPhoneSubId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                  MediaPhoneNo = dr["MediaPhoneNo"].ToString().Trim(),
                                  MediaPhoneSubNo = dr["MediaPhoneSubNo"].ToString().Trim(),
                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["Createdate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  Active = dr["Active"].ToString(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMediaPlan;
        }

        public int? CountListMediaPhoneSub(MediaPhoneSubInfo mInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((mInfo.MediaPhoneSubCode != null) && (mInfo.MediaPhoneSubCode != ""))
            {
                strcond += " and  m.MediaPhoneSubNo = '" + mInfo.MediaPhoneSubCode + "'";
                
            }
            if ((mInfo.MediaPhoneNo != null) && (mInfo.MediaPhoneNo != ""))
            {
                strcond += " and  m.MediaPhoneNo like '%" + mInfo.MediaPhoneNo + "%'";
            }
            if ((mInfo.MediaPhoneSubNo != null) && (mInfo.MediaPhoneSubNo != ""))
            {
                strcond += " and  m.MediaPhoneSubNo like '%" + mInfo.MediaPhoneSubNo + "%'";
            }
            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaPhoneSubInfo>();

            try
            {
                string strsql = " SELECT COUNT(m.id) as countMediaPhoneSub " +
                                    "FROM " + dbName + ".dbo.MediaPhoneSub AS m " +
                                " WHERE(m.FlagDelete = 'N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows

                              select new MediaPhoneSubInfo()
                              {
                                  countMediaPhoneSub = Convert.ToInt32(dr["countMediaPhoneSub"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LMediaPlan.Count > 0)
            {
                count = LMediaPlan[0].countMediaPhoneSub;
            }

            return count;
        }
        public int UpdateMediaPhoneSub(MediaPhoneSubInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.MediaPhoneSub set " +

                            " MediaPhoneNo = '" + pInfo.MediaPhoneNo + "'," +
                               " MediaPhoneSubNo = '" + pInfo.MediaPhoneSubNo + "'," +
                              " Active = '" + pInfo.Active + "'," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where id =" + pInfo.MediaPhoneSubId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteMediaPhoneSub(MediaPhoneSubInfo mInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.MediaPhoneSub set FlagDelete = 'Y' where Id in (" + mInfo.MediaPhoneSubIdlist + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }


        public int InsertMediaChannel(MediaChannelInfo mInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.MediaChannel  (code,name_th,name_en,Active,SaleChannelCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,MerchantCode)" +
                               "VALUES (" +
                                "'" + mInfo.Code + "'," +
                              "'" + mInfo.name_th + "'," +
                                   "'" + mInfo.name_en + "'," +
                                "'" + mInfo.Active + "'," +
                                   "'" + mInfo.SaleChannelCode + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.CreateBy + "'," +
                              "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                              "'" + mInfo.UpdateBy + "'," +
                              "'" + "N" + "'," +
                              "'" + mInfo.MerchantCode +"'" +
                               ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
       

        public int? CountListMediaChannel(MediaChannelInfo mInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((mInfo.Code != null) && (mInfo.Code != ""))
            {
                strcond += " and  m.Code like '%" + mInfo.Code + "%'";
            }
            if ((mInfo.name_en != null) && (mInfo.name_en != ""))
            {
                strcond += " and  m.name_en like '%" + mInfo.name_en + "%'";
            }
            if ((mInfo.name_th != null) && (mInfo.name_th != ""))
            {
                strcond += " and  m.name_th like '%" + mInfo.name_th + "%'";
            }
            if ((mInfo.Active != null) && (mInfo.Active != "") && (mInfo.Active != "-99"))
            {
                strcond += " and  m.Active = '" + mInfo.Active + "'";
            }
            DataTable dt = new DataTable();
            var LMediaPlan = new List<MediaChannelInfo>();

            try
            {
                string strsql = " SELECT COUNT(m.id) as countMediaChannel " +
                                    "FROM " + dbName + ".dbo.MediaChannel AS m " +
                                " WHERE(m.FlagDelete = 'N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMediaPlan = (from DataRow dr in dt.Rows

                              select new MediaChannelInfo()
                              {
                                  countMediaChannel = Convert.ToInt32(dr["countMediaChannel"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LMediaPlan.Count > 0)
            {
                count = LMediaPlan[0].countMediaChannel;
            }

            return count;
        }
        public int UpdateMediaChannel(MediaChannelInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.MediaChannel set " +

                            " code = '" + pInfo.Code + "'," +
                               " name_th = '" + pInfo.name_th + "'," +
                                   " name_en = '" + pInfo.name_en + "'," +
                              " Active = '" + pInfo.Active + "'," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where id =" + pInfo.MediaChannelId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteMediaChannel(MediaChannelInfo mInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.MediaChannel set FlagDelete = 'Y' where Id in (" + mInfo.MediaChannelIdlist + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}
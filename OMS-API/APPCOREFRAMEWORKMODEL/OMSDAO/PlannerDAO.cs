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
    public class PlannerDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdatePlanner(PlannerInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Planner set " +
                            " ChannelCode = '" + pInfo.ChannelCode + "'," +
                            " StatusActive = '" + pInfo.StatusActive + "'," +
                             " StartTime = CONVERT(DATETIME, '" + pInfo.StartTime + "', 103)," +
                            " EndTime = CONVERT(DATETIME, '" + pInfo.EndTime + "', 103)," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                            " PlannerCode = '" + pInfo.PlannerCode + "'," +
                            " PlannerName = '" + pInfo.PlannerName + "'," +
                             " PlannerDate =CONVERT(DATETIME, '" + pInfo.PlannerDate + "', 103)," +
                             " PlannerTime =CONVERT(DATETIME, '" + pInfo.PlannerTime + "', 103)," +
                              " PlannerDuration = '" + pInfo.PlannerDuration + "'," +
                            " CampaignCode = '" + pInfo.CampaignCode + "'," +
                             " PromotionCode = '" + pInfo.PromotionCode + "'," +
                            " PlannerProgramName = '" + pInfo.PlannerProgramName + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Plannerid =" + pInfo.Plannerid + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePlanner(PlannerInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Planner set FlagDelete = 'Y' where Id in (" + pInfo.Plannerid + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertPlanner(PlannerInfo pInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO Planner  (ChannelCode,StatusActive,PlannerDate,PlannerTime,PlannerCode," +
                "PlannerName,PlannerDuration,CampaignCode,Promotioncode,PlannerProgramName,SaleChannelCode," +

                "CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.ChannelCode + "'," +
                           "'" + pInfo.StatusActive + "'," +
                            "  CONVERT(DATETIME, '" + pInfo.PlannerDate + "', 103)," +
                         "'" + pInfo.PlannerTime + "'," +
                                 "'" + pInfo.PlannerCode + "'," +

                           "'" + pInfo.PlannerName + "'," +
                             "'" + pInfo.PlannerDuration + "'," +
                            "'" + pInfo.CampaignCode + "'," +
                           "'" + pInfo.PromotionCode + "'," +
                             "'" + pInfo.PlannerProgramName + "'," +
                               "'" + pInfo.SaleChannelCode + "'," +

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

        public List<PlannerListReturn> ListPlannerNoPagingByCriteria(PlannerInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.Plannerid != null) && (pInfo.Plannerid != 0))
            {
                strcond += " and  p.Plannerid =" + pInfo.Plannerid;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
            if ((pInfo.PlannerCode != null) && (pInfo.PlannerCode != "") )
            {
                strcond += " and  p.PlannerCode like '%" + pInfo.PlannerCode + "%'";
            }

            if ((pInfo.PlannerName != null) && (pInfo.PlannerName != ""))
            {
                strcond += " and  p.PlannerName like '%" + pInfo.PlannerName + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != "") && (pInfo.CampaignCode != "-99"))
            {
                strcond += " and  p.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != "") && (pInfo.PromotionCode != "-99"))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }

            if (((pInfo.StartTime != "") && (pInfo.StartTime != null)) && ((pInfo.EndTime != "") && (pInfo.EndTime != null)))
            {
                strcond += " AND p.PlannerDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartTime + "',103) AND CONVERT(DATETIME,'" + pInfo.EndTime + " 23:59:59:999',103)";
            }


            if ((pInfo.StatusActive != null) && (pInfo.StatusActive != ""))
            {
                strcond += " and  p.StatusActive like '%" + pInfo.StatusActive + "%'";
            }


            DataTable dt = new DataTable();
            var LPlanner = new List<PlannerListReturn>();

            try
            {
                string strsql = " SELECT        p.PlannerID, p.ChannelCode, p.FlagDelete, p.CreateBy, p.createdate, p.EndTime, p.StartTime, " +
                    " p.StatusActive, c.ChannelName, p.PlannerCode, p.PlannerName,cp.CampaignCode,p.PlannerDate,p.PlannerTime," +
                    "cp.CampaignName,prom.PromotionName,prom.PromotionCode,p.PlannerDuration,p.PlannerProgramName," +
                    "s.SaleChannelName,s.SaleChannelCode " +
                    "from " + dbName + ".dbo.Planner p " +
                                " inner join Channel c on  c.ChannelCode=p.ChannelCode AND c.FlagDelete = 'N' " +
                                " inner join Campaign cp on p.CampaignCode=cp.CampaignCode and cp.FlagDelete='N'" +
                                "  inner join Promotion prom on prom.PromotionCode = p.promotioncode and prom.FlagDelete='N'" +
                                "  inner join SaleChannel s on s.SaleChannelCode = p.SaleChannelCode and s.FlagDelete ='N' " +


                               " where  (p.FlagDelete = 'N') " + strcond;

                strsql += "ORDER BY p.PlannerID DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPlanner = (from DataRow dr in dt.Rows

                             select new PlannerListReturn()
                             {
                                 Plannerid = (dr["Plannerid"].ToString() != "") ? Convert.ToInt32(dr["Plannerid"]) : 0,
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),                           
                                 CreateBy = dr["CreateBy"].ToString(),
                                 
                                 FlagDelete = dr["FlagDelete"].ToString(),             
                                 StatusActive = dr["StatusActive"].ToString(),
                                 StartTime = dr["PlannerDate"].ToString(),
                                 EndTime = dr["PlannerTime"].ToString(),

                                 PlannerCode = dr["PlannerCode"].ToString(),
                                 PlannerName = dr["PlannerName"].ToString(),
                                 PlannerDuration = dr["PlannerDuration"].ToString(),
                                 PlannerProgramName = dr["PlannerProgramName"].ToString(),
                                 PlannerDate = dr["PlannerDate"].ToString(),
                                 PlannerTime = dr["PlannerTime"].ToString(),
                                 CampaignCode = dr["CampaignCode"].ToString(),
                                 PromotionCode = dr["PromotionCode"].ToString(),
                                 PromotionName = dr["PromotionName"].ToString(),
                                 CampaignName = dr["CampaignName"].ToString(),
                                 SaleChannelName = dr["SaleChannelName"].ToString(),
                                 SaleChannelCode = dr["SaleChannelCode"].ToString(),


                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPlanner;
        }



        public int? CountPlannerListByCriteria(PlannerInfo pInfo)
        {
            string strcond = "";
            int? count = 0;



            if ((pInfo.Plannerid != null) && (pInfo.Plannerid != 0))
            {
                strcond += " and  p.Plannerid =" + pInfo.Plannerid;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
            if ((pInfo.PlannerCode != null) && (pInfo.PlannerCode != ""))
            {
                strcond += " and  p.PlannerCode like '%" + pInfo.PlannerCode + "%'";
            }

            if ((pInfo.PlannerName != null) && (pInfo.PlannerName != ""))
            {
                strcond += " and  p.PlannerName like '%" + pInfo.PlannerName + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != "") && (pInfo.CampaignCode != "-99"))
            {
                strcond += " and  p.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != "") && (pInfo.PromotionCode != "-99"))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }

            if (((pInfo.StartTime != "") && (pInfo.StartTime != null)) && ((pInfo.EndTime != "") && (pInfo.EndTime != null)))
            {
                strcond += " AND p.PlannerDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartTime + "',103) AND CONVERT(DATETIME,'" + pInfo.EndTime + " 23:59:59:999',103)";
            }


            if ((pInfo.StatusActive != null) && (pInfo.StatusActive != ""))
            {
                strcond += " and  p.StatusActive like '%" + pInfo.StatusActive + "%'";
            }
            
            DataTable dt = new DataTable();
            var LPlanner = new List<PlannerListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countPlanner" +
                                "from " + dbName + ".dbo.Planner p " +
                            " inner join Channel c on  c.ChannelCode=m.ChannelCode AND c.FlagDelete = 'N' " +
                                " inner join Campaign cp on p.CampaignCode=cp.CampaignCode and cp.FlagDelete='N'" +
                                "  inner join Promotion prom on prom.PromotionCode = p.promotioncode and prom.FlagDelete='N'" +
                                    "  inner join SaleChannel s on s.SaleChannelCode = p.SaleChannelCode and s.FlagDelete ='N' " +

                               " where  (p.FlagDelete = 'N') " + strcond;
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPlanner = (from DataRow dr in dt.Rows

                               select new PlannerListReturn()
                               {
                                   countPlanner = Convert.ToInt32(dr["countPlanner"])
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPlanner.Count > 0)
            {
                count = LPlanner[0].countPlanner;
            }

            return count;
        }
        public int DeletePlannerList(PlannerInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Planner set FlagDelete = 'Y' where Plannerid in (" + pInfo.PlanneridList + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountListPlanner(PlannerInfo pInfo)
        {
            string strcond = "";
            int? count = 0;



            if ((pInfo.Plannerid != null) && (pInfo.Plannerid != 0))
            {
                strcond += " and  p.Plannerid =" + pInfo.Plannerid;
            }

            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
            if ((pInfo.PlannerCode != null) && (pInfo.PlannerCode != ""))
            {
                strcond += " and  p.PlannerCode like '%" + pInfo.PlannerCode + "%'";
            }

            if ((pInfo.PlannerName != null) && (pInfo.PlannerName != ""))
            {
                strcond += " and  p.PlannerName like '%" + pInfo.PlannerName + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != "") && (pInfo.CampaignCode != "-99"))
            {
                strcond += " and  p.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != "") && (pInfo.PromotionCode != "-99"))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }

            if (((pInfo.StartTime != "") && (pInfo.StartTime != null)) && ((pInfo.EndTime != "") && (pInfo.EndTime != null)))
            {
                strcond += " AND p.PlannerDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartTime + "',103) AND CONVERT(DATETIME,'" + pInfo.EndTime + " 23:59:59:999',103)";
            }


            if ((pInfo.StatusActive != null) && (pInfo.StatusActive != ""))
            {
                strcond += " and  p.StatusActive like '%" + pInfo.StatusActive + "%'";
            }


            DataTable dt = new DataTable();
            var LPlanner = new List<PlannerListReturn>();


            try
            {
                string strsql = "select count(p.PlannerId) as countPlanner" +
                                " from " + dbName + ".dbo.Planner p " +
                                  " inner join Channel c on  c.ChannelCode=p.ChannelCode AND c.FlagDelete = 'N' " +
                                " inner join Campaign cp on p.CampaignCode=cp.CampaignCode and cp.FlagDelete='N'" +
                                "  inner join Promotion prom on prom.PromotionCode = p.promotioncode and prom.FlagDelete='N'" +
                                    "  inner join SaleChannel s on s.SaleChannelCode = p.SaleChannelCode and s.FlagDelete ='N' " +
                                " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPlanner = (from DataRow dr in dt.Rows

                               select new PlannerListReturn()
                               {
                                   countPlanner = Convert.ToInt32(dr["countPlanner"])
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPlanner.Count > 0)
            {
                count = LPlanner[0].countPlanner;
            }

            return count;
        }
    }
}

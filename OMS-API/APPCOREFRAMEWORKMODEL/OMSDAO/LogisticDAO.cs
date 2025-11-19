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
    public class LogisticDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<LogisticListReturn> ListLogisticNopagingByCriteria(LogisticInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.LogisticCode != null) && (cInfo.LogisticCode != ""))
            {
                strcond += " and  c.LogisticCode like '%" + cInfo.LogisticCode + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LogisticListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Logistic c " +
                           
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LogisticListReturn()
                             {
                                 LogisticId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 LogisticCode = dr["LogisticCode"].ToString().Trim(),
                                 LogisticName = dr["LogisticName"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 Fee = (dr["Fee"].ToString() != "") ? Convert.ToInt32(dr["Fee"]) : 0,
                                 WorkingDay = dr["WorkingDay"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }
        public List<LogisticListReturn> LogisticFeeCalculate(LogisticInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.LogisticCode != null) && (cInfo.LogisticCode != ""))
            {
                strcond += " and  l.LogisticCode like '%" + cInfo.LogisticCode + "%'";
            }
            if (cInfo.Weight != 0)
            {
                strcond += " and  l.Weight like '%" + cInfo.Weight + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LogisticListReturn>();

            try
            {
                string strsql = " select c.Id,c.LogisticCode,LogisticName,FlagDelete,Fee,Weight from " + dbName + ".dbo.Logistic c " +
                                " where c.FlagDelete ='N' " + strcond;

                //strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LogisticListReturn()
                             {
                                 LogisticId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 LogisticCode = dr["LogisticCode"].ToString().Trim(),
                                 LogisticName = dr["LogisticName"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 Fee = (dr["Fee"].ToString() != "") ? Convert.ToInt32(dr["Fee"]) : 0,
                                 Weight = (dr["Weight"].ToString() != "") ? Convert.ToDouble(dr["Weight"]) : 0,

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }


        public int UpdateLogistic(LogisticInfo cInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Logistic set " +
                           " LogisticCode = '" + cInfo.LogisticCode + "'," +
                           " LogisticName = '" + cInfo.LogisticName + "'," +

                            " EstimatedTime = '" + cInfo.EstimatedTime + "'," +
                           " Status = '" + cInfo.status + "'," +
                            " LogisticType = '" + cInfo.LogisticType + "'," +
                            " Fee = '" + cInfo.Fee + "'," +
                           " TypeCalWeight = '" + cInfo.TypeCalWeight + "'," +
                            " TypeCalSize = '" + cInfo.TypeCalSize + "'," +
                           " TypeCalWeightSize = '" + cInfo.TypeCalWeightSize + "'," +

                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.LogisticId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteLogistic(LogisticInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Logistic set FlagDelete = 'Y' where Id in (" + cInfo.LogisticIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertLogistic(LogisticInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Logistic  (LogisticCode,LogisticName," +
                "EstimatedTime,Status,LogisticType,Fee," +
                "TypeCalWeight,TypeCalSize,TypeCalWeightSize" +
                ",CreateDate,CreateBy," +
                "UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.LogisticCode + "'," +
                           "'" + cInfo.LogisticName + "'," +
                              "'" + cInfo.EstimatedTime + "'," +
                           "'" + cInfo.status + "'," +
                               "'" + cInfo.LogisticType + "'," +
                               "'" + cInfo.Fee + "'," +
                             "'" + cInfo.TypeCalWeight + "'," +
                           "'" + cInfo.TypeCalSize + "'," +
                             "'" + cInfo.TypeCalWeightSize + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +

                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertLogisticDetail(LogisticDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO LogisticDetail  (LogisticCodeDetail,Fee,PackageWLHFrom,PackageWLHTo,PackageWidth,PackageLength," +
                "PackageHeigth,WeightTo,WeightFrom," +
                "CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.LogisticCodeDetail + "'," +
                           "'" + cInfo.Fee + "'," +
                           "'" + cInfo.PackageWLHFrom + "'," +
                               "'" + cInfo.PackageWLHTo + "'," +
                           "'" + cInfo.PackageWidth + "'," +
                           "'" + cInfo.PackageLength + "'," +

                           "'" + cInfo.PackageHeigth + "'," +
                           "'" + cInfo.WeightTo + "'," +
                           "'" + cInfo.WeightFrom + "'," +
                    

                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<LogisticListReturn> ListLogisticNoPagingByCriteria(LogisticInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.LogisticId != null) && (cInfo.LogisticId != 0))
            {
                strcond += " and  c.Id =" + cInfo.LogisticId;
            }

            if ((cInfo.LogisticCode != null) && (cInfo.LogisticCode != ""))
            {
                strcond += " and  c.LogisticCode like '%" + cInfo.LogisticCode + "%'";
            }
            if ((cInfo.LogisticName != null) && (cInfo.LogisticName != ""))
            {
                strcond += " and  c.LogisticName like '%" + cInfo.LogisticName + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LogisticListReturn>();

            try
            {
                string strsql = " select c.Id,c.LogisticCode,LogisticName,FlagDelete,Fee,Weight,CreateBy,CreateDate," +
                    "UpdateBy,UpdateDate,FlagDelete from " + dbName + ".dbo.Logistic c " +

                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LogisticListReturn()
                             {
                                 LogisticId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 LogisticCode = dr["LogisticCode"].ToString().Trim(),
                                 LogisticName = dr["LogisticName"].ToString().Trim(),

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

        public List<LogisticListReturn> ListLogisticPagingByCriteria(LogisticInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.LogisticId != null) && (cInfo.LogisticId != 0))
            {
                strcond += " and  c.Id =" + cInfo.LogisticId;
            }

            if ((cInfo.LogisticCode != null) && (cInfo.LogisticCode != ""))
            {
                strcond += " and  c.LogisticCode like '%" + cInfo.LogisticCode + "%'";
            }
            if ((cInfo.LogisticName != null) && (cInfo.LogisticName != ""))
            {
                strcond += " and  c.LogisticName like '%" + cInfo.LogisticName + "%'";
            }
            if ((cInfo.LogisticType != null) && (cInfo.LogisticType != "-99") && (cInfo.LogisticType != ""))
            {
                strcond += " and  c.LogisticType ='" + cInfo.LogisticType + "'";
            }


            DataTable dt = new DataTable();
            var LCampaign = new List<LogisticListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Logistic c " +

                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LogisticListReturn()
                             {
                                 LogisticId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 LogisticCode = dr["LogisticCode"].ToString().Trim(),
                                 LogisticName = dr["LogisticName"].ToString().Trim(),
                                 LogisticType = dr["LogisticType"].ToString().Trim(),
                                 status = dr["status"].ToString().Trim(),
                                 EstimatedTime = dr["EstimatedTime"].ToString().Trim(),
                                 TypeCalWeight = dr["TypeCalWeight"].ToString().Trim(),
                                 TypeCalSize = dr["TypeCalSize"].ToString().Trim(),
                                 TypeCalWeightSize = dr["TypeCalWeightSize"].ToString().Trim(),
                                 Fee = (dr["Fee"].ToString() != "") ? Convert.ToInt32(dr["Fee"]) : 0,
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

        public int? CountLogisticListByCriteria(LogisticInfo cInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((cInfo.LogisticId != null) && (cInfo.LogisticId != 0))
            {
                strcond += " and  c.Id =" + cInfo.LogisticId;
            }

            if ((cInfo.LogisticCode != null) && (cInfo.LogisticCode != ""))
            {
                strcond += " and  c.LogisticCode like '%" + cInfo.LogisticCode + "%'";
            }
            if ((cInfo.LogisticName != null) && (cInfo.LogisticName != ""))
            {
                strcond += " and  c.LogisticName like '%" + cInfo.LogisticName + "%'";
            }



            DataTable dt = new DataTable();
            var LCampaign = new List<LogisticListReturn>();


            try
            {
                string strsql = "select count(c.Id) as countChannel from " + dbName + ".dbo.Logistic c " +

                         " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LogisticListReturn()
                             {
                                 countLogistic = Convert.ToInt32(dr["countChannel"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countLogistic;
            }

            return count;
        }
        public List<LogisticListDetailReturn> ListLogisticDetailPagingByCriteria(LogisticDetailInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.LogisticCodeDetail != null) && (cInfo.LogisticCodeDetail != ""))
            {
                strcond += " and  c.LogisticCodeDetail ='" + cInfo.LogisticCodeDetail+"'";
            }

     


            DataTable dt = new DataTable();
            var LCampaign = new List<LogisticListDetailReturn>();

            try
            {
                string strsql = " select c.id,LogisticCodeDetail,Fee,PackageWidth,PackageLength,PackageHeigth,PackageWLHFrom,PackageWLHTo,WeightFrom" +
                    ",WeightTo,CreateBy,UpdateBy,CreateDate,UpdateDate,FlagDelete from " + dbName + ".dbo.LogisticDetail c " +

                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LogisticListDetailReturn()
                             {
                                 LogisticDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 LogisticCodeDetail = dr["LogisticCodeDetail"].ToString().Trim(),
                                 Fee = (dr["Fee"].ToString() != "") ? Convert.ToInt32(dr["Fee"]) : 0,
                                 PackageWidth = (dr["PackageWidth"].ToString() != "") ? Convert.ToDouble(dr["PackageWidth"]) : 0.00,

                                 PackageLength = (dr["PackageLength"].ToString() != "") ? Convert.ToDouble(dr["PackageLength"]) : 0.00,
                            
                                 PackageHeigth = (dr["PackageHeigth"].ToString() != "") ? Convert.ToDouble(dr["PackageHeigth"]) : 0.00,
                             
                                 PackageWLHFrom = (dr["PackageWLHFrom"].ToString() != "") ? Convert.ToDouble(dr["PackageWLHFrom"]) : 0.00,
                                 PackageWLHTo = (dr["PackageWLHTo"].ToString() != "") ? Convert.ToDouble(dr["PackageWLHTo"]) : 0.00,


                                 WeightFrom = (dr["WeightFrom"].ToString() != "") ? Convert.ToDouble(dr["WeightFrom"]) : 0.00,
                               
                                 WeightTo = (dr["WeightTo"].ToString() != "") ? Convert.ToDouble(dr["WeightTo"]) : 0.00,
                               
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


        public int UpdateLogisticDetail(LogisticDetailInfo cInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.LogisticDetail set " +
                           " LogisticCodeDetail = '" + cInfo.LogisticCodeDetail + "'," +
                           " Fee = '" + cInfo.Fee + "'," +
                            " PackageWidth = '" + cInfo.PackageWidth + "'," +
                           " PackageLength = '" + cInfo.PackageLength + "'," +
                            " PackageHeigth = '" + cInfo.PackageHeigth + "'," +
                           
                              " PackageWLHFrom = '" + cInfo.PackageWLHFrom + "'," +
                                  " PackageWLHTo = '" + cInfo.PackageWLHTo + "'," +
                            " WeightFrom = '" + cInfo.WeightFrom + "'," +
                           " WeightTo = '" + cInfo.WeightTo + "'," +

                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.LogisticDetailId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteLogisticDetail(LogisticDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.LogisticDetail set FlagDelete = 'Y' where Id in (" + cInfo.LogisticDetailId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<LogisticListReturn> ListLogisticCodeValidateCriteria(LogisticInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.LogisticCode != null) && (cInfo.LogisticCode != ""))
            {
                strcond += " and  c.LogisticCode like '%" + cInfo.LogisticCode + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<LogisticListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Logistic c " +

                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new LogisticListReturn()
                             {
                                 LogisticId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 LogisticCode = dr["LogisticCode"].ToString().Trim(),
                                 LogisticName = dr["LogisticName"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 Fee = (dr["Fee"].ToString() != "") ? Convert.ToInt32(dr["Fee"]) : 0,
                                 WorkingDay = dr["WorkingDay"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }
    }
}

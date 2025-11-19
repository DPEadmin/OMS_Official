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
    public class IntegrationEmpDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateChannel(IntegrationEmpInfo cInfo)
        {
            int i = 0;
            
            string strsql = " Update " + dbName + ".dbo.Emp set " +
                           " EmpCode = '" + cInfo.EmpCode + "'," +
                           " EmpFname_TH = '" + cInfo.EmpFName + "'," +
                            " EmpLname_TH = '" + cInfo.EmpLName + "'," +
                               " RefCode = '" + cInfo.RefCode + "'," +
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.EmpId + "";
            

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteChannel(IntegrationEmpInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Emp set FlagDelete = 'Y' where Id in (" + cInfo.EmpId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertChannel(IntegrationEmpInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Emp  (EmpCode,EmpFname_TH,EmpLName_TH,RefCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.EmpCode + "'," +
                           "'" + cInfo.EmpFName + "'," +
                            "'" + cInfo.EmpLName + "'," +
                            "'" + cInfo.RefCode + "'," +
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

        public List<IntegrationEmpListReturn> ListChannelNoPagingByCriteria(IntegrationEmpInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.EmpId != null) && (cInfo.EmpId != 0))
            {
                strcond += " and  c.Id =" + cInfo.EmpId;
            }

            if ((cInfo.EmpCode != null) && (cInfo.EmpCode != ""))
            {
                strcond += " and  c.EmpCode like '%" + cInfo.EmpCode + "%'";
            }
            if ((cInfo.RefCode != null) && (cInfo.RefCode != ""))
            {
                strcond += " and  c.RefCode like '%" + cInfo.RefCode + "%'";
            }
            


            DataTable dt = new DataTable();
            var LCampaign = new List<IntegrationEmpListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Emp c " +
                              
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                            select new IntegrationEmpListReturn()
                            {
                                EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                EmpCode = dr["EmpCode"].ToString().Trim(),
                                EmpFName = dr["EmpFname_TH"].ToString().Trim(),
                                EmpLName = dr["EmpLName"].ToString().Trim(),

                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                              
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<IntegrationEmpListReturn> ListChannelPagingByCriteria(IntegrationEmpInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.EmpId != null) && (cInfo.EmpId != 0))
            {
                strcond += " and  c.Id =" + cInfo.EmpId;
            }

            if ((cInfo.EmpCode != null) && (cInfo.EmpCode != ""))
            {
                strcond += " and  c.EmpCode like '%" + cInfo.EmpCode + "%'";
            }
            if ((cInfo.EmpFName != null) && (cInfo.EmpFName != ""))
            {
                strcond += " and  c.EmpFname_TH like '%" + cInfo.EmpFName + "%'";
            }
            if ((cInfo.EmpLName != null) && (cInfo.EmpLName != ""))
            {
                strcond += " and  c.EmpLname_TH like '%" + cInfo.EmpLName + "%'";
            }
            if ((cInfo.RefCode != null) && (cInfo.RefCode != ""))
            {
                strcond += " and  c.RefCode like '%" + cInfo.RefCode + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<IntegrationEmpListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Emp c " +
                            
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new IntegrationEmpListReturn()
                             {
                                 EmpId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString().Trim(),
                                 EmpFName = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLName = dr["EmpLname_TH"].ToString().Trim(),
                                 RefCode = dr["RefCode"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                               
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int? CountChannelListByCriteria(IntegrationEmpInfo cInfo) {
            string strcond = "";
            int? count = 0;


            if ((cInfo.EmpId != null) && (cInfo.EmpId != 0))
            {
                strcond += " and  c.Id =" + cInfo.EmpId;
            }

            if ((cInfo.EmpCode != null) && (cInfo.EmpCode != ""))
            {
                strcond += " and  c.EmpCode like '%" + cInfo.EmpCode + "%'";
            }
            if ((cInfo.EmpFName != null) && (cInfo.EmpFName != ""))
            {
                strcond += " and  c.EmpFname_TH like '%" + cInfo.EmpFName + "%'";
            }
            if ((cInfo.EmpLName != null) && (cInfo.EmpLName != ""))
            {
                strcond += " and  c.EmpLname_TH like '%" + cInfo.EmpLName + "%'";
            }
            if ((cInfo.RefCode != null) && (cInfo.RefCode != ""))
            {
                strcond += " and  c.RefCode like '%" + cInfo.RefCode + "%'";
            }
            DataTable dt = new DataTable();
            var LCampaign = new List<IntegrationEmpListReturn>();


            try
            {
                string strsql = "select count(c.Id) as CountEmp from " + dbName + ".dbo.Emp c " +

                         " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new IntegrationEmpListReturn()
                             {
                                 CountEmp = Convert.ToInt32(dr["CountEmp"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].CountEmp;
            }

            return count;
        }

     
        public List<IntegrationEmpListReturn> EmpCodeValidateInsert(IntegrationEmpInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.EmpCode != null) && (cInfo.EmpCode != ""))
            {
                strcond += " and  c.EmpCode = '" + cInfo.EmpCode + "'";
            }
            if ((cInfo.RefCode != null) && (cInfo.RefCode != ""))
            {
                strcond += " and  c.RefCode = '" + cInfo.RefCode + "'";
            }
     
            DataTable dt = new DataTable();
            var LCampaign = new List<IntegrationEmpListReturn>();

            try
            {
                string strsql = " select c.EmpCode from " + dbName + ".dbo.Emp c " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new IntegrationEmpListReturn()
                             {
                                 EmpCode = dr["EmpCode"].ToString().Trim(),

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

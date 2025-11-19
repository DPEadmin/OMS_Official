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
    public class EmpMapBUDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<EmpMapBuListReturn> ListEmpMapBuNoPaging(EmpMapBuInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += " and  e.EmpCode = '" + eInfo.EmpCode + "'";
            }

            DataTable dt = new DataTable();
            var LEmpMapBu = new List<EmpMapBuListReturn>();

            try
            {
                string strsql = " select Id, e.EmpCode, e.Bu, e.Role, e.Levels " +
                            " from " + dbName + ".dbo.EmpMapBu e " +
                            " where 1 = 1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmpMapBu = (from DataRow dr in dt.Rows

                              select new EmpMapBuListReturn()
                              {
                                  EmpMapBuId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  EmpCode = dr["EmpCode"].ToString(),
                                  Bu = dr["Bu"].ToString().Trim(),
                                  Role = dr["Role"].ToString().Trim(),
                                  Levels = dr["Levels"].ToString().Trim(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmpMapBu;
        }

        public List<EmpMapBuListReturn> GetMaxLevelsEmpMapBu(EmpMapBuInfo eInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LEmpMapBu = new List<EmpMapBuListReturn>();

            try
            {
                string strsql = " SELECT MAX(Levels) AS Levels " +
                                " from " + dbName + ".dbo.EmpMapBu e ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmpMapBu = (from DataRow dr in dt.Rows

                             select new EmpMapBuListReturn()
                             {
                                 Levels = dr["Levels"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmpMapBu;
        }

        public List<EmpMapBuListReturn> ListEmpMapBuPaging(EmpMapBuInfo eInfo)
        {
            string strcond = "";
            string stroffset = "";

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += " and  e.EmpCode = '" + eInfo.EmpCode + "'";
            }

            if ((eInfo.Bu != null) && (eInfo.Bu != ""))
            {
                strcond += " and  e.Bu = '" + eInfo.Bu + "'";
            }

            if ((eInfo.Role != null) && (eInfo.Role != ""))
            {
                strcond += " and  e.Role = '" + eInfo.Role + "'";
            }

            if ((eInfo.Levels != null) && (eInfo.Levels != ""))
            {
                strcond += " and  e.Levels = '" + eInfo.Levels + "'";
            }

            if ((eInfo.rowOFFSet != null) && (eInfo.rowOFFSet != 0))
            {
                stroffset += " OFFSET " + eInfo.rowOFFSet + " ROWS FETCH NEXT " + eInfo.rowFetch + " ROWS ONLY ";
            }

            DataTable dt = new DataTable();
            var LEmpMapBu = new List<EmpMapBuListReturn>();

            try
            {
                string strsql = "SELECT e.Id, e.EmpCode, e.Bu, e.Role, e.Levels, em.EmpFname_TH, em.EmpLName_TH " +
                                " FROM "+ dbName + ".dbo.EmpMapBu AS e LEFT OUTER JOIN " +
                                dbName + ".dbo.Emp AS em ON em.EmpCode = e.EmpCode" +
                                " where (em.FlagDelete = 'N') AND (em.ActiveFlag = 'Y') " + strcond;

                strsql += stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmpMapBu = (from DataRow dr in dt.Rows

                             select new EmpMapBuListReturn()
                             {
                                 EmpMapBuId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString(),
                                 Bu = dr["Bu"].ToString().Trim(),
                                 Role = dr["Role"].ToString().Trim(),
                                 Levels = dr["Levels"].ToString().Trim(),
                                 EmpFname_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLname_TH = dr["EmpLName_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString() + "  " + dr["EmpLname_TH"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmpMapBu;
        }

        public int? CountEmpMapBuPaging(EmpMapBuInfo eInfo)
        {
            string strcond = "";
            string stroffset = "";
            int? count = 0;

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += " and  e.EmpCode = '" + eInfo.EmpCode + "'";
            }

            if ((eInfo.Bu != null) && (eInfo.Bu != ""))
            {
                strcond += " and  e.Bu = '" + eInfo.Bu + "'";
            }

            if ((eInfo.Role != null) && (eInfo.Role != ""))
            {
                strcond += " and  e.Role = '" + eInfo.Role + "'";
            }

            if ((eInfo.Levels != null) && (eInfo.Levels != ""))
            {
                strcond += " and  e.Levels = '" + eInfo.Levels + "'";
            }

            if ((eInfo.rowOFFSet != null) && (eInfo.rowOFFSet != 0))
            {
                stroffset += " OFFSET " + eInfo.rowOFFSet + " ROWS FETCH NEXT " + eInfo.rowFetch + " ROWS ONLY ";
            }

            DataTable dt = new DataTable();
            var LEmpMapBu = new List<EmpMapBuListReturn>();

            try
            {
                string strsql = " select count(Id) as countEmpMapBU " +
                            " from " + dbName + ".dbo.EmpMapBu e " +
                            " where 1 = 1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmpMapBu = (from DataRow dr in dt.Rows

                              select new EmpMapBuListReturn()
                              {
                                  countEmpMapBU = Convert.ToInt32(dr["countEmpMapBU"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LEmpMapBu.Count > 0)
            {
                count = LEmpMapBu[0].countEmpMapBU;
            }

            return count;
        }

        public int InsertEmpMapBu(EmpMapBuInfo eInfo)
        {
            int i = 0;

            string strsql = "insert into EmpMapBu (EmpCode, Bu, Role, Levels) values (" +
                             "'" + eInfo.EmpCode + "', " +
                             "'" + eInfo.Bu + "', " +
                             "'" + eInfo.Role + "', " +
                             "'" + eInfo.Levels + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateEmpMapBu(EmpMapBuInfo eInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.EmpMapBu set " +

                            " Levels = '" + eInfo.Levels + "'" +
                            " where Id =" + eInfo.EmpMapBuId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteEmpMapBu(EmpMapBuInfo eInfo)
        {
            int i = 0;

            string strsql = " Delete from " + dbName + ".dbo.EmpMapBu " +
                         " where Id in(" + eInfo.EmpCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<EmpMapBuListReturn> ListEmpMapRoleValidateDuplicateInsert(EmpMapBuInfo eInfo)
        {
            string strcond = "";

            if ((eInfo.EmpCode != null) && (eInfo.EmpCode != ""))
            {
                strcond += (strcond == "") ? " where  e.EmpCode = '" + eInfo.EmpCode.Trim() + "'" : " and e.EmpCode = '" + eInfo.EmpCode.Trim() + "'";
            }
            if ((eInfo.Bu != null) && (eInfo.Bu != ""))
            {
                strcond += (strcond == "") ? " where  e.Bu = '" + eInfo.Bu.Trim() + "'" : " and e.Bu = '" + eInfo.Bu.Trim() + "'";
            }
            if ((eInfo.Role != null) && (eInfo.Role != ""))
            {
                strcond += (strcond == "") ? " where  e.Role = '" + eInfo.Role.Trim() + "'" : " and e.Role = '" + eInfo.Role.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LEmpMapBu = new List<EmpMapBuListReturn>();

            try
            {
                string strsql = "select e.*" +
                                " from " + dbName + ".dbo.EmpMapBu e " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LEmpMapBu = (from DataRow dr in dt.Rows

                             select new EmpMapBuListReturn()
                             {
                                 EmpMapBuId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 EmpCode = dr["EmpCode"].ToString(),
                                 Bu = dr["Bu"].ToString().Trim(),
                                 Role = dr["Role"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LEmpMapBu;
        }
    }
}

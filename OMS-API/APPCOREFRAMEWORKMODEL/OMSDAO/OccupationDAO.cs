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
    public class OccupationDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public List<OccupationListReturn> ListOccupationNopagingByCriteria(OccupationInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OccupationCode != null) && (oInfo.OccupationCode != ""))
            {
                strcond += " and  o.OccupationCode = '" + oInfo.OccupationCode + "'";
            }

            if ((oInfo.OccupationName != null) && (oInfo.OccupationName != ""))
            {
                strcond += " and  o.OccupationName like '%" + oInfo.OccupationName + "%'";
            }

            DataTable dt = new DataTable();
            var LOccupation = new List<OccupationListReturn>();

            try
            {
                string strsql = " select o.* from " + dbName + ".dbo.Occupation o " +

                               " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOccupation = (from DataRow dr in dt.Rows

                             select new OccupationListReturn()
                             {
                                 OccupationId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 OccupationCode = dr["OccupationCode"].ToString().Trim(),
                                 OccupationName = dr["OccupationName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOccupation;
        }
        public List<OccupationListReturn> ListOccupationPagingByCriteria(OccupationInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OccupationCode.Trim() != null) && (oInfo.OccupationCode.Trim() != ""))
            {
                strcond += " and  o.OccupationCode = '" + oInfo.OccupationCode.Trim() + "' ";
            }

            if ((oInfo.OccupationName.Trim() != null) && (oInfo.OccupationName.Trim() != ""))
            {
                strcond += " and  o.OccupationName like '%" + oInfo.OccupationName.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LOccupation = new List<OccupationListReturn>();

            try
            {
                string strsql = " select o.* from " + dbName + ".dbo.Occupation o " +
                                " where o.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY o.Id DESC OFFSET " + oInfo.rowOFFSet + " ROWS FETCH NEXT " + oInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOccupation = (from DataRow dr in dt.Rows

                            select new OccupationListReturn()
                            {
                                OccupationId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                OccupationCode = dr["OccupationCode"].ToString().Trim(),
                                OccupationName = dr["OccupationName"].ToString().Trim(),
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

            return LOccupation;
        }

        public int? CountOccupationMasterListByCriteria(OccupationInfo oInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((oInfo.OccupationCode.Trim() != null) && (oInfo.OccupationCode.Trim() != ""))
            {
                strcond += " and  o.OccupationCode = '" + oInfo.OccupationCode.Trim() + "'";
            }
            if ((oInfo.OccupationName.Trim() != null) && (oInfo.OccupationName.Trim() != ""))
            {
                strcond += " and  o.OccupationName like '%" + oInfo.OccupationName.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LOccupation = new List<OccupationListReturn>();

            try
            {
                string strsql = " select count(o.Id) as countOccupation from " + dbName + ".dbo.Occupation o " +
                                " where o.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOccupation = (from DataRow dr in dt.Rows

                            select new OccupationListReturn()
                            {
                                countOccupation = Convert.ToInt32(dr["countOccupation"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LOccupation.Count > 0)
            {
                count = LOccupation[0].countOccupation;
            }

            return count;

        }

        public List<OccupationListReturn> OccupationCodeValidateInsert(OccupationInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OccupationCode != null) && (oInfo.OccupationCode != ""))
            {
                strcond += " and  o.OccupationCode = '" + oInfo.OccupationCode + "'";
            }

            DataTable dt = new DataTable();
            var LOccupation = new List<OccupationListReturn>();

            try
            {
                string strsql = " select o.OccupationCode from " + dbName + ".dbo.Occupation o " +
                               " where o.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOccupation = (from DataRow dr in dt.Rows

                            select new OccupationListReturn()
                            {
                                OccupationCode = dr["OccupationCode"].ToString().Trim(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOccupation;
        }

        public int? InsertOccupation(OccupationInfo oInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO Occupation (OccupationCode,OccupationName,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete) " +
                            "VALUES (" +
                           "'" + oInfo.OccupationCode + "'," +
                           "'" + oInfo.OccupationName + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + oInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + oInfo.UpdateBy + "'," +
                           "'" + oInfo.FlagDelete + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateOccupation(OccupationInfo oInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Occupation set " +
                            " OccupationCode = '" + oInfo.OccupationCode + "'," +
                            " OccupationName = '" + oInfo.OccupationName + "'," +
                            " UpdateBy = '" + oInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id =" + oInfo.OccupationId + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteOccupation(OccupationInfo oInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Occupation set FlagDelete = 'Y' where Id in (" + oInfo.OccupationId + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}

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
    public class DepartmentDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountDepartmentByCriteria(DepartmentInfo dInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((dInfo.DepartmentCode != null) && (dInfo.DepartmentCode != ""))
            {
                strcond += (strcond == "") ? " where  d.DepartmentCode like '%" + dInfo.DepartmentCode.Trim() + "%'" : " and d.DepartmentCode like '%" + dInfo.DepartmentCode.Trim() + "%'";
            }

            if ((dInfo.DepartmentName != null) && (dInfo.DepartmentName != ""))
            {
                strcond += (strcond == "") ? " where  d.DepartmentName like '%" + dInfo.DepartmentName + "%'" : " and   d.DepartmentName like '%" + dInfo.DepartmentName + "%'";
            }
            if ((dInfo.FlagDelete != null) && (dInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  d.FlagDelete = '" + dInfo.FlagDelete + "'" : " and   d.FlagDelete = '" + dInfo.FlagDelete + "'";
            }
            if ((dInfo.rowOFFSet != 0) && (dInfo.rowFetch != 0))
            {
                strcond += " ORDER BY d.Id DESC OFFSET " + dInfo.rowOFFSet + " ROWS FETCH NEXT " + dInfo.rowFetch + " ROWS ONLY";

            }

            DataTable dt = new DataTable();
            var LDepartment = new List<DepartmentListReturn>();


            try
            {
                string strsql = " select count (d.Id) as countDepartment " +
                                " from " + dbName + ".dbo.Department d " +
                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDepartment = (from DataRow dr in dt.Rows

                             select new DepartmentListReturn()
                             {
                                 countDepartment = Convert.ToInt32(dr["countDepartment"])

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LDepartment.Count > 0)
            {
                count = LDepartment[0].countDepartment;
            }

            return count;
        }

        public List<DepartmentListReturn> ListDepartmentByCriteria(DepartmentInfo dInfo)
        {
            string strcond = "";

            if ((dInfo.DepartmentCode != null) && (dInfo.DepartmentCode != ""))
            {
                strcond += (strcond == "") ? " where  d.DepartmentCode like '%" + dInfo.DepartmentCode.Trim() + "%'" : " and d.DepartmentCode like '%" + dInfo.DepartmentCode.Trim() + "%'";
            }
            if ((dInfo.DepartmentCodeValidate != null) && (dInfo.DepartmentCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  d.DepartmentCode = '" + dInfo.DepartmentCodeValidate.Trim() + "'" : " and d.DepartmentCode = '" + dInfo.DepartmentCodeValidate.Trim() + "'";
            }
            if ((dInfo.DepartmentName != null) && (dInfo.DepartmentName != ""))
            {
                strcond += (strcond == "") ? " where  d.DepartmentName like '%" + dInfo.DepartmentName + "%'" : " and   d.DepartmentName like '%" + dInfo.DepartmentName + "%'";
            }
            if ((dInfo.FlagDelete != null) && (dInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  d.FlagDelete = '" + dInfo.FlagDelete + "'" : " and   d.FlagDelete = '" + dInfo.FlagDelete + "'";
            }
            if ((dInfo.rowOFFSet != 0) && (dInfo.rowFetch != 0))
            {
                strcond += " ORDER BY d.Id DESC OFFSET " + dInfo.rowOFFSet + " ROWS FETCH NEXT " + dInfo.rowFetch + " ROWS ONLY";

            }

            DataTable dt = new DataTable();
            var LDepartment = new List<DepartmentListReturn>();

            try
            {
                string strsql = " select d.*" +
                                " from " + dbName + ".dbo.Department d " +
                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDepartment = (from DataRow dr in dt.Rows

                             select new DepartmentListReturn()
                             {
                                 DepartmentId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 DepartmentCode = dr["DepartmentCode"].ToString().Trim(),
                                 DepartmentName = dr["DepartmentName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDepartment;
        }

        public int InsertDepartment(DepartmentInfo dInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Department  (DepartmentCode,DepartmentName,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + dInfo.DepartmentCode + "'," +
                           "'" + dInfo.DepartmentName + "'," +

                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + dInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + dInfo.UpdateBy + "'," +

                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteDepartment(DepartmentInfo dInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Department set " +

                           " FlagDelete = 'Y'" +

                           " where Id in(" + dInfo.DepartmentId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateDepartment(DepartmentInfo dInfo)
        {
            int i = 0;

            string strsql = "Update Department  set " +
                            " DepartmentCode = '" + dInfo.DepartmentCode + "'," +
                           " DepartmentName = '" + dInfo.DepartmentName + "'," +
                           " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           " UpdateBy ='" + dInfo.UpdateBy + "'" +
                           " where Id = " + dInfo.DepartmentId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<DepartmentListReturn> ddlDepartment(DepartmentInfo dInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LDepartment = new List<DepartmentListReturn>();

            try
            {
                string strsql = " select *" +
                                " from " + dbName + ".dbo.Department d where FlagDelete='N' " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDepartment = (from DataRow dr in dt.Rows

                               select new DepartmentListReturn()
                               {
                                   DepartmentId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                                   DepartmentCode = dr["DepartmentCode"].ToString(),
                                   DepartmentName = dr["DepartmentName"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDepartment;
        }
    }
}

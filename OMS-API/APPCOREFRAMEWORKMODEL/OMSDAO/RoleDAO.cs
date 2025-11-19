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
    public class RoleDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountRoleByCriteria(RoleInfo rInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((rInfo.RoleCode != null) && (rInfo.RoleCode != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'" : " and r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'";
            }
            if ((rInfo.RoleCodeValidate != null) && (rInfo.RoleCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'" : " and r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'";
            }
            if ((rInfo.RoleName != null) && (rInfo.RoleName != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleName like '%" + rInfo.RoleName + "%'" : " and   r.RoleName like '%" + rInfo.RoleName + "%'";
            }
            if ((rInfo.FlagDelete != null) && (rInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  r.FlagDelete = '" + rInfo.FlagDelete + "'" : " and   r.FlagDelete = '" + rInfo.FlagDelete + "'";
            }
            if ((rInfo.DepartmentCode != null) && (rInfo.DepartmentCode != ""))
            {
                strcond += (strcond == "") ? " where  r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'" : " and   r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'";
            }
            if ((rInfo.rowOFFSet != 0) || (rInfo.rowFetch != 0))
            {
                strcond += " ORDER BY r.Id DESC OFFSET " + rInfo.rowOFFSet + " ROWS FETCH NEXT " + rInfo.rowFetch + " ROWS ONLY";

            }

            DataTable dt = new DataTable();
            var LRole = new List<RoleListReturn>();


            try
            {
                string strsql = " select count (r.Id) as countRole " +
                                " from " + dbName + ".dbo.Role r " +
                               " left join " + dbName + ".dbo.Department d on r.DepartmentCode = d.DepartmentCode " +
                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LRole = (from DataRow dr in dt.Rows

                             select new RoleListReturn()
                             {
                                 countRole = Convert.ToInt32(dr["countRole"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LRole.Count > 0)
            {
                count = LRole[0].countRole;
            }

            return count;
        }

        public List<RoleListReturn> ListRoleByCriteria(RoleInfo rInfo)
        {
            string strcond = "";

            if ((rInfo.RoleCode != null) && (rInfo.RoleCode != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'" : " and r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'";
            }
            if ((rInfo.RoleCodeValidate != null) && (rInfo.RoleCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'" : " and r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'";
            }
            if ((rInfo.RoleName != null) && (rInfo.RoleName != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleName like '%" + rInfo.RoleName + "%'" : " and   r.RoleName like '%" + rInfo.RoleName + "%'";
            }
            if ((rInfo.FlagDelete != null) && (rInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  r.FlagDelete = '" + rInfo.FlagDelete + "'" : " and   r.FlagDelete = '" + rInfo.FlagDelete + "'";
            }
            if ((rInfo.DepartmentCode != null) && (rInfo.DepartmentCode != ""))
            {
                strcond += (strcond == "") ? " where  r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'" : " and   r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'";
            }
            if ((rInfo.rowOFFSet != 0) || (rInfo.rowFetch != 0))
            {
                strcond += " ORDER BY r.Id DESC OFFSET " + rInfo.rowOFFSet + " ROWS FETCH NEXT " + rInfo.rowFetch + " ROWS ONLY";

            }

            DataTable dt = new DataTable();
            var LCampaign = new List<RoleListReturn>();

            try
            {
                string strsql = " select r.*,d.DepartmentName" +
                                " from " + dbName + ".dbo.Role r " +
                                " left join " + dbName + ".dbo.Department d on r.DepartmentCode = d.DepartmentCode " +
                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RoleListReturn()
                             {
                                 RoleId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                                 DepartmentCode = dr["DepartmentCode"].ToString().Trim(),
                                 DepartmentName = dr["DepartmentName"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<RoleListReturn> ListRoleValidateInsert(RoleInfo rInfo)
        {
            string strcond = "";

            if ((rInfo.RoleCode != null) && (rInfo.RoleCode != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'" : " and r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'";
            }
            if ((rInfo.RoleCodeValidate != null) && (rInfo.RoleCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'" : " and r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'";
            }
            if ((rInfo.RoleName != null) && (rInfo.RoleName != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleName like '%" + rInfo.RoleName + "%'" : " and   r.RoleName like '%" + rInfo.RoleName + "%'";
            }
            if ((rInfo.FlagDelete != null) && (rInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  r.FlagDelete = '" + rInfo.FlagDelete + "'" : " and   r.FlagDelete = '" + rInfo.FlagDelete + "'";
            }
            if ((rInfo.DepartmentCode != null) && (rInfo.DepartmentCode != ""))
            {
                strcond += (strcond == "") ? " where  r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'" : " and   r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'";
            }
            if ((rInfo.rowOFFSet != 0) || (rInfo.rowFetch != 0))
            {
                strcond += " ORDER BY r.Id DESC OFFSET " + rInfo.rowOFFSet + " ROWS FETCH NEXT " + rInfo.rowFetch + " ROWS ONLY";

            }

            DataTable dt = new DataTable();
            var LCampaign = new List<RoleListReturn>();

            try
            {
                string strsql = " select r.* " +
                                " from " + dbName + ".dbo.Role r " +
                                 strcond + " and r.FlagDelete = 'N'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RoleListReturn()
                             {
                                 RoleId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                                 DepartmentCode = dr["DepartmentCode"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int InsertRole(RoleInfo rInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Role  (RoleCode,RoleName,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,DepartmentCode)" +
                            "VALUES (" +
                           "'" + rInfo.RoleCode + "'," +
                           "'" + rInfo.RoleName + "'," +

                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + rInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + rInfo.UpdateBy + "'," +

                           "'N'" + "," +
                           "'" + rInfo.DepartmentCode + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateRole(RoleInfo rInfo)
        {
            int i = 0;

            string strsql = "Update Role  set " +
                            " RoleCode = '" + rInfo.RoleCode + "'," +
                           " RoleName = '" + rInfo.RoleName + "'," +
                           " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           " DepartmentCode = '" + rInfo.DepartmentCode + "'," +
                      
                           " UpdateBy ='" + rInfo.UpdateBy + "'" +
                           " where Id = " + rInfo.RoleId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteRole(RoleInfo rInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Role set " +

                           " FlagDelete = 'Y'" +

                           " where Id in(" + rInfo.RoleId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteRoleList(RoleInfo rInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Role set " +

                           " FlagDelete = 'Y'" +

                           " where Id in(" + rInfo.RoleIdList + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<RoleListReturn> ddlRole()
        {
            string strcond = "";

          
            DataTable dt = new DataTable();
            var LCampaign = new List<RoleListReturn>();

            try
            {
                string strsql = " select *" +
                                " from " + dbName + ".dbo.Role r where FlagDelete='N' " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RoleListReturn()
                             {
                                 RoleId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                                 RoleCode = dr["RoleCode"].ToString(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                                 DepartmentCode = dr["DepartmentCode"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<RoleListReturn> ddlRoleByDepartmentCode(RoleInfo rInfo)
        {
            string strcond = "";

            if ((rInfo.DepartmentCode != null) && (rInfo.DepartmentCode != ""))
            {
                strcond += (strcond == "") ? " where d.DepartmentCode like '%" + rInfo.DepartmentCode.Trim() + "%'" : " and d.DepartmentCode like '%" + rInfo.DepartmentCode.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<RoleListReturn>();

            try
            {
                string strsql = " select *" +
                                " from " + dbName + ".dbo.Department d left join " + dbName + ".dbo.Role r on d.DepartmentCode = r.DepartmentCode and r.FlagDelete ='N' " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RoleListReturn()
                             {
                                 RoleId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                                 RoleCode = dr["RoleCode"].ToString(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                                 DepartmentCode = dr["DepartmentCode"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }
        public List<RoleListReturn> ListRoleFullfillByCriteria(RoleInfo rInfo)
        {
            string strcond = "";

            strcond = " where r.FlagDelete ='N'";
            if ((rInfo.RoleCode != null) && (rInfo.RoleCode != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'" : " and r.RoleCode like '%" + rInfo.RoleCode.Trim() + "%'";
            }
            if ((rInfo.RoleCodeValidate != null) && (rInfo.RoleCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'" : " and r.RoleCode = '" + rInfo.RoleCodeValidate.Trim() + "'";
            }
            if ((rInfo.RoleName != null) && (rInfo.RoleName != ""))
            {
                strcond += (strcond == "") ? " where  r.RoleName like '%" + rInfo.RoleName + "%'" : " and   r.RoleName like '%" + rInfo.RoleName + "%'";
            }
            if ((rInfo.FlagDelete != null) && (rInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  r.FlagDelete = '" + rInfo.FlagDelete + "'" : " and   r.FlagDelete = '" + rInfo.FlagDelete + "'";
            }
            if ((rInfo.DepartmentCode != null) && (rInfo.DepartmentCode != ""))
            {
                strcond += (strcond == "") ? " where  r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'" : " and   r.DepartmentCode like '%" + rInfo.DepartmentCode + "%'";
            }
            if ((rInfo.rowOFFSet != 0) || (rInfo.rowFetch != 0))
            {
                strcond += " ORDER BY r.Id DESC OFFSET " + rInfo.rowOFFSet + " ROWS FETCH NEXT " + rInfo.rowFetch + " ROWS ONLY";

            }

            DataTable dt = new DataTable();
            var LCampaign = new List<RoleListReturn>();

            try
            {
                string strsql = " select r.*,d.DepartmentName" +
                                " from " + dbName + ".dbo.Role r  " +
                                " left join " + dbName + ".dbo.Department d on r.DepartmentCode = d.DepartmentCode  " +
                                 strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RoleListReturn()
                             {
                                 RoleId = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                                 RoleCode = dr["RoleCode"].ToString().Trim(),
                                 RoleName = dr["RoleName"].ToString().Trim(),
                                 DepartmentCode = dr["DepartmentCode"].ToString().Trim(),
                                 DepartmentName = dr["DepartmentName"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int DeleteEmpRole(EmpInfo rInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.EmpRole set " +

                           " FlagDelete = 'Y'" +

                           " where Empcode in(" + rInfo.EmpCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteUserlogin(EmpInfo rInfo)
        {
            int i = 0;

            string strsql = " delete " + dbName + ".dbo.userlogin " +
                           " where Empcode in(" + rInfo.EmpCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteMerchantMapUser(EmpInfo rInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.MerchantMapping set " +

                           " FlagDelete = 'Y'" +

                           " where UserName in(" + rInfo.EmpCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}

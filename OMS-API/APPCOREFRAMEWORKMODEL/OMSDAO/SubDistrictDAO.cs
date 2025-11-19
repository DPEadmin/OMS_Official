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
    public class SubDistrictDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountSubDistrictListByCriteria(SubDistrictInfo sInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((sInfo.SubDistrictId != null) && (sInfo.SubDistrictId != 0))
            {
                strcond += " and  p.Id =" + sInfo.SubDistrictId;
            }

            if ((sInfo.SubDistrictCode != null) && (sInfo.SubDistrictCode != ""))
            {
                strcond += " and  p.SubDistrictCode = '" + sInfo.SubDistrictCode + "'";
            }
            if ((sInfo.SubDistrictName != null) && (sInfo.SubDistrictName != ""))
            {
                strcond += " and  p.SubDistrictName like '%" + sInfo.SubDistrictName + "%'";
            }
            if ((sInfo.DistrictCode != null) && (sInfo.DistrictCode != ""))
            {
                strcond += " and  p.DistrictCode = '" + sInfo.DistrictCode + "'";
            }

            DataTable dt = new DataTable();
            var LSubDistrict = new List<SubDistrictListReturn>();

            try
            {
                string strsql = "select count(p.Id) as countSubDistrict from " + dbName + ".dbo.SubDistrict p " +
                                " where p.FlagDelete ='N' " + strcond;
                strcond += " order by p.SubDistrictName ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSubDistrict = (from DataRow dr in dt.Rows

                             select new SubDistrictListReturn()
                             {
                                 countSubDistrict = Convert.ToInt32(dr["countSubDistrict"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LSubDistrict.Count > 0)
            {
                count = LSubDistrict[0].countSubDistrict;
            }

            return count;
        }

        public int? UpdateSubDistrict(SubDistrictInfo sInfo)
        {
            int? i = 0;
            
            string strsql = " Update " + dbName + ".dbo.SubDistrict set " +
                            " SubDistrictCode = '" + sInfo.SubDistrictCode + "'," +
                            " SubDistrictName = '" + sInfo.SubDistrictName + "'," +
                            " UpdateBy = '" + sInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + sInfo.SubDistrictId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSubDistrict(SubDistrictInfo sInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SubDistrict set FlagDelete = 'Y' where Id in (" + sInfo.SubDistrictId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? InsertSubDistrict(SubDistrictInfo sInfo)
        {
            int? i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.SubDistrict  (SubDistrictCode,SubDistrictName,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + sInfo.SubDistrictCode + "'," +
                           "'" + sInfo.SubDistrictName + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + sInfo.CreateBy + "'," +
                           "'" + sInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<SubDistrictListReturn> ListSubDistrictByCriteria(SubDistrictInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.SubDistrictId != null) && (cInfo.SubDistrictId != 0))
            {
                strcond += " and  c.Id =" + cInfo.SubDistrictId;
            }

            if ((cInfo.SubDistrictCode != null) && (cInfo.SubDistrictCode != ""))
            {
                strcond += " and  c.SubDistrictCode = '" + cInfo.SubDistrictCode + "'";
            }
            if ((cInfo.SubDistrictName != null) && (cInfo.SubDistrictName != ""))
            {
                strcond += " and  c.SubDistrictName like '%" + cInfo.SubDistrictName + "%'";
            }
            if ((cInfo.DistrictCode != null) && (cInfo.DistrictCode != ""))
            {
                strcond += " and  c.DistrictCode = '" + cInfo.DistrictCode + "'";
            }

            DataTable dt = new DataTable();
            var LSubDistrict = new List<SubDistrictListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.SubDistrict c " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSubDistrict = (from DataRow dr in dt.Rows

                             select new SubDistrictListReturn()
                             {
                                 SubDistrictId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 SubDistrictName = dr["SubDistrictName"].ToString().Trim(),

                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),

                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSubDistrict;
        }

        public List<SubDistrictListReturn> ListMaxSubDistrict(SubDistrictInfo cInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LSubDistrict = new List<SubDistrictListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.SubDistrict c " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSubDistrict = (from DataRow dr in dt.Rows

                                select new SubDistrictListReturn()
                                {
                                    SubDistrictId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                    SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                    SubDistrictName = dr["SubDistrictName"].ToString().Trim(),

                                    CreateBy = dr["CreateBy"].ToString(),
                                    CreateDate = dr["CreateDate"].ToString(),
                                    UpdateBy = dr["UpdateBy"].ToString(),

                                    FlagDelete = dr["FlagDelete"].ToString(),
                                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSubDistrict;
        }

        

        public List<SubDistrictListReturn> ListTumbonNopagingByCriteria(SubDistrictInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.Prov_Code != null) && (cInfo.Prov_Code != ""))
            {
                strcond += " and  c.Prov_Code =" + ((Convert.ToInt32(cInfo.Prov_Code)) + 9);
            }

            if ((cInfo.Aump_Code != null) && (cInfo.Aump_Code != ""))
            {
                strcond += " and  c.Aump_Code = '" + cInfo.Aump_Code + "'";
            }

            DataTable dt = new DataTable();
            var LSubDistrict = new List<SubDistrictListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Tumbon c " +

                               " where 1=1 " + strcond;

                strsql += " ORDER BY c.TUMB_NAME ASC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSubDistrict = (from DataRow dr in dt.Rows

                                select new SubDistrictListReturn()
                                {
                                  
                                    Tumb_Code = dr["TUMB_CODE"].ToString().Trim(),
                                    Aump_Code = dr["AUMP_CODE"].ToString().Trim(),
                                    Prov_Code = dr["PROV_CODE"].ToString().Trim(),
                                    Zipcode = dr["TUMB_ZIPCODE"].ToString().Trim(),
                                    
                                    SubDistrictName = dr["TUMB_NAME"].ToString().Trim(),

                                    
                                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSubDistrict;
        }
        public List<SubDistrictListReturn> ListSubDistrictNoPagingByCriteria(SubDistrictInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.ProvinceCode != null) && (cInfo.ProvinceCode != ""))
            {
                strcond += " and  c.ProvinceCode ='" + cInfo.ProvinceCode + "'";
            }

            if ((cInfo.DistrictCode != null) && (cInfo.DistrictCode != ""))
            {
                strcond += " and  c.DistrictCode = '" + cInfo.DistrictCode + "'";
            }

            if ((cInfo.SubDistrictCode != null) && (cInfo.SubDistrictCode != ""))
            {
                strcond += " and  c.SubDistrictCode = '" + cInfo.SubDistrictCode + "'";
            }

            DataTable dt = new DataTable();
            var LSubDistrict = new List<SubDistrictListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.SubDistrict c " +

                               " where 1=1 " + strcond;

                strsql += " ORDER BY c.SubDistrictName ASC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSubDistrict = (from DataRow dr in dt.Rows

                                select new SubDistrictListReturn()
                                {
                                    SubDistrictId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                    SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                    SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                    ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                    DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                    Zipcode = dr["zipcode"].ToString().Trim(),

                                }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSubDistrict;
        }

    }
}

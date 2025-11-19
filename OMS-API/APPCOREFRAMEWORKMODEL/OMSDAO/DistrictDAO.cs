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
    public class DistrictDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<DistrictListReturn> ListDistrictNopagingByCriteria(DistrictInfo dInfo)
        {
            string strcond = "";
            if ((dInfo.DistrictId != null) && (dInfo.DistrictId != 0))
            {
                strcond += " and  p.Id =" + dInfo.DistrictId;
            }
            if ((dInfo.DistrictCode != null) && (dInfo.DistrictCode != ""))
            {
                strcond += " and  p.DistrictCode = '" + dInfo.DistrictCode + "'";
            }
            if ((dInfo.DistrictName != null) && (dInfo.DistrictName != ""))
            {
                strcond += " and  p.DistrictName like '%" + dInfo.DistrictName + "%'";
            }
            if ((dInfo.ProvinceCode != null) && (dInfo.ProvinceCode != ""))
            {
                strcond += " and  p.ProvinceCode = '" + dInfo.ProvinceCode + "'";
            }

            DataTable dt = new DataTable();
            var LDistrict = new List<DistrictListReturn>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.District p " +

                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.DistrictName ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDistrict = (from DataRow dr in dt.Rows

                             select new DistrictListReturn()
                             {
                                 DistrictId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 DistrictName = dr["DistrictName"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 Amphur_Id = (dr["AMPHUR_ID"].ToString() != "") ? Convert.ToInt32(dr["AMPHUR_ID"]) : 0,
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

            return LDistrict;
        }

        public int? CountDistrictListByCriteria(DistrictInfo dInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((dInfo.DistrictId != null) && (dInfo.DistrictId != 0))
            {
                strcond += " and  p.Id =" + dInfo.DistrictId;
            }

            if ((dInfo.DistrictCode != null) && (dInfo.DistrictCode != ""))
            {
                strcond += " and  p.DistrictCode = '" + dInfo.DistrictCode + "'";
            }
            if ((dInfo.DistrictName != null) && (dInfo.DistrictName != ""))
            {
                strcond += " and  p.DistrictName like '%" + dInfo.DistrictName + "%'";
            }
            if ((dInfo.ProvinceCode != null) && (dInfo.ProvinceCode != ""))
            {
                strcond += " and  p.ProvinceCode = '" + dInfo.ProvinceCode + "'";
            }

            DataTable dt = new DataTable();
            var LDistrict = new List<DistrictListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countDistrict from " + dbName + ".dbo.District p " +

                                 " left join " + dbName + ".dbo.PromotionDetailInfo d on d.PromotionCode =  p.DistrictCode " +
                                 " left join " + dbName + ".dbo.Promotion t on t.PromotionCode =  d.PromotionCode " +
                                 " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDistrict = (from DataRow dr in dt.Rows

                             select new DistrictListReturn()
                             {
                                 countDistrict = Convert.ToInt32(dr["countDistrict"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LDistrict.Count > 0)
            {
                count = LDistrict[0].countDistrict;
            }

            return count;
        }

        public int InsertDistrict(DistrictInfo dInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.District  (ProvinceCode,DistrictCode,DistrictName,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +

                            "'" + dInfo.ProvinceCode + "'," +
                           "'" + dInfo.DistrictCode + "'," +
                           "'" + dInfo.DistrictName + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + dInfo.CreateBy + "'," +

                           "'" + dInfo.FlagDelete + "'" +
                            ")";



            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateDistrict(DistrictInfo dInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.District set " +
                            " DistrictCode = '" + dInfo.DistrictCode + "'," +
                            " DistrictName = '" + dInfo.DistrictName + "'," +
                            " UpdateBy = '" + dInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + dInfo.DistrictId + "";



            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteDistrict(DistrictInfo dInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.District set FlagDelete = 'Y' where Id in (" + dInfo.DistrictId + ")";



            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<DistrictListReturn> ListDistrictByCriteria(DistrictInfo dInfo)
        {
            string strcond = "";
            if ((dInfo.DistrictId != null) && (dInfo.DistrictId != 0))
            {
                strcond += " and  c.Id =" + dInfo.DistrictId;
            }

            if ((dInfo.DistrictCode != null) && (dInfo.DistrictCode != ""))
            {
                strcond += " and  c.DistrictCode = '" + dInfo.DistrictCode + "'";
            }
            if ((dInfo.DistrictName != null) && (dInfo.DistrictName != ""))
            {
                strcond += " and  c.DistrictName like '%" + dInfo.DistrictName + "%'";
            }
            if ((dInfo.ProvinceCode != null) && (dInfo.ProvinceCode != ""))
            {
                strcond += " and  c.ProvinceCode = '" + dInfo.ProvinceCode + "'";
            }

            DataTable dt = new DataTable();
            var LDistrict = new List<DistrictListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.District c  JOIN " + dbName + ".dbo.Province p ON C.ProvinceCode=p.ProvinceCode " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + dInfo.rowOFFSet + " ROWS FETCH NEXT " + dInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDistrict = (from DataRow dr in dt.Rows

                             select new DistrictListReturn()
                             {
                                 DistrictId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 DistrictName = dr["DistrictName"].ToString().Trim(),

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

            return LDistrict;
        }

        public List<DistrictListReturn> ListMaxDistrictByCriteria(DistrictInfo dInfo)
        {
            string strcond = "";
           
            DataTable dt = new DataTable();
            var LDistrict = new List<DistrictListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.District c  JOIN " + dbName + ".dbo.Province p ON C.ProvinceCode=p.ProvinceCode " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDistrict = (from DataRow dr in dt.Rows

                             select new DistrictListReturn()
                             {
                                 DistrictId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 DistrictName = dr["DistrictName"].ToString().Trim(),

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

            return LDistrict;
        }

    }
}

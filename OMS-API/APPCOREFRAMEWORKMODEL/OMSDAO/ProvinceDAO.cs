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
    public class ProvinceDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountProvinceListByCriteria(ProvinceInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.ProvinceId != null) && (pInfo.ProvinceId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProvinceId;
            }

            if ((pInfo.ProvinceCode != null) && (pInfo.ProvinceCode != ""))
            {
                strcond += " and  p.ProvinceCode like '%" + pInfo.ProvinceCode + "%'";
            }
            if ((pInfo.ProvinceName != null) && (pInfo.ProvinceName != ""))
            {
                strcond += " and  p.ProvinceName like '%" + pInfo.ProvinceName + "%'";
            }

            DataTable dt = new DataTable();
            var LProvince = new List<ProvinceListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countProvince from " + dbName + ".dbo.Province p " +
                                 " left join " + dbName + ".dbo.PromotionDetailInfo d on d.ProductCode =  p.ProvinceCode" +
                                 " left join " + dbName + ".dbo.Promotion t on t.PromotionCode =  d.PromotionCode " +
                                 " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProvince = (from DataRow dr in dt.Rows

                             select new ProvinceListReturn()
                             {
                                 countProvince = Convert.ToInt32(dr["countProvince"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LProvince.Count > 0)
            {
                count = LProvince[0].countProvince;
            }

            return count;
        }

        public int InsertProvince(ProvinceInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Province  (ProvinceCode,ProvinceName,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.ProvinceCode + "'," +
                           "'" + pInfo.ProvinceName + "'," +
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

        public int? UpdateProvince(ProvinceInfo pInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.Province set " +
                            " ProvinceCode = '" + pInfo.ProvinceCode + "'," +
                            " ProvinceName = '" + pInfo.ProvinceName + "'," +
                             " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pInfo.ProvinceId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteProvince(ProvinceInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Province set FlagDelete = 'Y' where Id in (" + pInfo.ProvinceId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<ProvinceListReturn> ListProvinceNopagingByCriteria(ProvinceInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProvinceId != null) && (pInfo.ProvinceId != 0))
            {
                strcond += " and  c.Id =" + pInfo.ProvinceId;
            }

            if ((pInfo.ProvinceCode != null) && (pInfo.ProvinceCode != ""))
            {
                strcond += " and  c.ProvinceCode like '%" + pInfo.ProvinceCode + "%'";
            }
            if ((pInfo.ProvinceName != null) && (pInfo.ProvinceName != ""))
            {
                strcond += " and  c.ProvinceName like '%" + pInfo.ProvinceName + "%'";
            }

            DataTable dt = new DataTable();
            var LProvince = new List<ProvinceListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Province c " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.ProvinceName ASC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProvince = (from DataRow dr in dt.Rows

                             select new ProvinceListReturn()
                             {
                                 ProvinceId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ProvinceName = dr["ProvinceName"].ToString().Trim(),

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

            return LProvince;
        }

        public List<ProvinceListReturn> ListProvinceByCriteria(ProvinceInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProvinceId != null) && (pInfo.ProvinceId != 0))
            {
                strcond += " and  c.Id =" + pInfo.ProvinceId;
            }

            if ((pInfo.ProvinceCode != null) && (pInfo.ProvinceCode != ""))
            {
                strcond += " and  c.ProvinceCode like '%" + pInfo.ProvinceCode + "%'";
            }
            if ((pInfo.ProvinceName != null) && (pInfo.ProvinceName != ""))
            {
                strcond += " and  c.ProvinceName like '%" + pInfo.ProvinceName + "%'";
            }
            if ((pInfo.ProvinceName_Ins != null) && (pInfo.ProvinceName_Ins != ""))
            {
                strcond += " and  c.ProvinceName like '%" + pInfo.ProvinceName_Ins + "%'";
            }

            DataTable dt = new DataTable();
            var LProvince = new List<ProvinceListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Province c " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProvince = (from DataRow dr in dt.Rows

                             select new ProvinceListReturn()
                             {
                                 ProvinceId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ProvinceName = dr["ProvinceName"].ToString().Trim(),

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

            return LProvince;
        }

        public List<ProvinceListReturn> ListMaxProvince(ProvinceInfo pInfo)
        {
            string strcond = "";

           
            DataTable dt = new DataTable();
            var LProvince = new List<ProvinceListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Province c " +

                       " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProvince = (from DataRow dr in dt.Rows

                             select new ProvinceListReturn()
                             {
                                 ProvinceId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ProvinceName = dr["ProvinceName"].ToString().Trim(),
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

            return LProvince;
        }

    }
}

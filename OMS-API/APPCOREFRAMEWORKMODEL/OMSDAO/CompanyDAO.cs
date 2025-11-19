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
    public class CompanyDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateCompany(CompanyInfo comInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Company set " +
                            " CompanyCode = '" + comInfo.CompanyCode + "'," +
                            " CompanyNameTH = '" + comInfo.CompanyNameTH + "'," +
                            " CompanyNameEN = '" + comInfo.CompanyNameEN + "'," +
                            " CompanyType = '" + comInfo.CompanyKind + "'," +
                            " AddressTH = '" + comInfo.AddressTH + "'," +
                            " AddressEN = '" + comInfo.AddressEN + "'," +
                            " Telephone = '" + comInfo.Telephone + "'," +
                           " Fax = '" + comInfo.Fax + "'," +
                           " TechnicianCode = '" + comInfo.TechnicianCode + "'," +
                           " UpdateBy = '" + comInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id = " + comInfo.CompanyId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeleteCompany(CompanyInfo comInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Company set FlagDelete = 'Y' where Id in (" + comInfo.CompanyId_str + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertCompany(CompanyInfo comInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Company  (CompanyCode,CompanyNameTH,CompanyNameEN,CompanyType,AddressTH,AddressEN,Telephone,Fax,TechnicianCode,CreateDate,CreateBy,UpdateDate,MerchantMapCode,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + comInfo.CompanyCode + "'," +
                           "'" + comInfo.CompanyNameTH + "'," +
                           "'" + comInfo.CompanyNameEN + "'," +
                           "'" + comInfo.CompanyKind + "'," +
                           "'" + comInfo.AddressTH + "'," +
                           "'" + comInfo.AddressEN + "'," +
                           "'" + comInfo.Telephone + "'," +
                           "'" + comInfo.Fax + "'," +
                           "'" + comInfo.TechnicianCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + comInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + comInfo.MerchantMapCode + "'," +
                           "'" + comInfo.UpdateBy + "'," +
                           "'" + comInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<CompanyListReturn> CompanyListNoPaging(CompanyInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CompanyId != null) && (cInfo.CompanyId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CompanyId;
            }

            if ((cInfo.CompanyCode != null) && (cInfo.CompanyCode != ""))
            {
                strcond += " and  c.CompanyCode like '%" + cInfo.CompanyCode + "%'";
            }
            if ((cInfo.CompanyNameEN != null) && (cInfo.CompanyNameEN != ""))
            {
                strcond += " and  c.CompanyNameEN like '%" + cInfo.CompanyNameEN + "%'";
            }

            DataTable dt = new DataTable();
            var LCompany = new List<CompanyListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Company c " +
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCompany = (from DataRow dr in dt.Rows

                             select new CompanyListReturn()
                             {
                                 CompanyId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CompanyCode = dr["CompanyCode"].ToString().Trim(),
                                 CompanyNameEN = dr["CompanyNameEN"].ToString().Trim(),
                                 CompanyNameTH = dr["CompanyNameTH"].ToString().Trim(),
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

            return LCompany;
        }
        public List<CompanyListReturn> ListCompanyPagingByCriteria(CompanyInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CompanyCode != null) && (cInfo.CompanyCode != "") && (cInfo.TechnicianCode == "Y"))
            {
                strcond += " and  c.CompanyCode = '" + cInfo.CompanyCode + "'";
            }
            else if((cInfo.CompanyCode != null) && (cInfo.CompanyCode != "") && (cInfo.TechnicianCode != "Y"))
            {
                strcond += " and  c.CompanyCode like '%" + cInfo.CompanyCode + "%'";
            }

            if ((cInfo.CompanyNameTH != null) && (cInfo.CompanyNameTH != ""))
            {
                strcond += " and  c.CompanyNameTH like '%" + cInfo.CompanyNameTH + "%'";
            }

            if ((cInfo.CompanyNameEN != null) && (cInfo.CompanyNameEN != ""))
            {
                strcond += " and  c.CompanyNameEN like '%" + cInfo.CompanyNameEN + "%'";
            }
           
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode + "'";
            }
           

            var LCompany = new List<CompanyListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Company c " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCompany = (from DataRow dr in dt.Rows

                             select new CompanyListReturn()
                             {
                                 CompanyId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CompanyCode = dr["CompanyCode"].ToString().Trim(),
                                 CompanyNameTH = dr["CompanyNameTH"].ToString().Trim(),
                                 CompanyNameEN = dr["CompanyNameEN"].ToString().Trim(),
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

            return LCompany;
        }
        public int? CountCompanyListPagingByCriteria(CompanyInfo cInfo)
        {

            string strcond = "";
            int? count = 0;


            if ((cInfo.CompanyCode != null) && (cInfo.CompanyCode != "") && (cInfo.TechnicianCode == "Y"))
            {
                strcond += " and  c.CompanyCode = '" + cInfo.CompanyCode + "'";
            }
            else if ((cInfo.CompanyCode != null) && (cInfo.CompanyCode != "") && (cInfo.TechnicianCode != "Y"))
            {
                strcond += " and  c.CompanyCode like '%" + cInfo.CompanyCode + "%'";
            }

            if ((cInfo.CompanyNameTH != null) && (cInfo.CompanyNameTH != ""))
            {
                strcond += " and  c.CompanyNameTH like '%" + cInfo.CompanyNameTH + "%'";
            }

            if ((cInfo.CompanyNameEN != null) && (cInfo.CompanyNameEN != ""))
            {
                strcond += " and  c.CompanyNameEN like '%" + cInfo.CompanyNameEN + "%'";
            }

            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode + "'";
            }

            var LCompany = new List<CompanyListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select count(c.Id) as countCompany from " + dbName + ".dbo.Company c " +
                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCompany = (from DataRow dr in dt.Rows

                             select new CompanyListReturn()
                             {
                                 countCompany = Convert.ToInt32(dr["countCompany"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LCompany.Count > 0)
            {
                count = LCompany[0].countCompany;
            }

            return count;
        }
    }
}

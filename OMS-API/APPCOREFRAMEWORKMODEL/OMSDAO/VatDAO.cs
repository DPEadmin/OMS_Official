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
    public class VatDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<VatListReturn> ListVatByCriteria(VatInfo vInfo)
        {
            string strcond = "";

            if ((vInfo.VatId != null) && (vInfo.VatId != 0))
            {
                strcond += "AND v.Id = " + vInfo.VatId;
            }
            if ((vInfo.VatCode != null) && (vInfo.VatCode != ""))
            {
                strcond = strcond == "" ? strcond += "where v.VatCode LIKE '%" + vInfo.VatCode.Trim() + "%'" : strcond += "AND v.VatCode LIKE '%" + vInfo.VatCode.Trim() + "%'";
            }
            if ((vInfo.VatName != null) && (vInfo.VatName != ""))
            {
                strcond = strcond == "" ? strcond += "where v.VatName LIKE '%" + vInfo.VatName.Trim() + "%'" : strcond += "AND v.VatName LIKE '%" + vInfo.VatName.Trim() + "%'";
            }
            if ((vInfo.VatValue != null) && (vInfo.VatValue != 0))
            {
                strcond = strcond == "" ? strcond += "where v.VatValue = " + vInfo.VatValue + "" : strcond += "AND v.VatValue " + vInfo.VatValue + "";
            }
            if ((vInfo.FlagActive != null) && (vInfo.FlagActive != "") && (vInfo.FlagActive != "-99"))
            {
                strcond = strcond == "" ? strcond += "where v.FlagActive LIKE '%" + vInfo.FlagActive.Trim() + "%'" : strcond += "AND v.FlagActive LIKE '%" + vInfo.FlagActive.Trim() + "%'";
            }
            if ((vInfo.FlagDelete != null) && (vInfo.FlagDelete != ""))
            {
                strcond = strcond == "" ? strcond += "where v.FlagDelete LIKE '%" + vInfo.FlagDelete.Trim() + "%'" : strcond += "AND v.FlagDelete LIKE '%" + vInfo.FlagDelete.Trim() + "%'";
            }
            var LVat = new List<VatListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = "SELECT v.Id, v.VatCode, v.VatName, v.VatValue, v.FlagActive, v.FlagDelete " +
                                "FROM " + dbName + ".dbo.Vat AS v " + strcond;

                strsql += " ORDER BY v.Id DESC OFFSET " + vInfo.rowOFFSet + " ROWS FETCH NEXT " + vInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LVat = (from DataRow dr in dt.Rows

                        select new VatListReturn()
                        {
                            VatId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                            VatCode = dr["VatCode"].ToString().Trim(),
                            VatName = dr["VatName"].ToString().Trim(),
                            VatValue = (dr["VatValue"].ToString() != "") ? Convert.ToDouble(dr["VatValue"]) : 0,
                            FlagActive = dr["FlagActive"].ToString().Trim(),
                            FlagDelete = dr["FlagDelete"].ToString().Trim(),

                        }).ToList();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LVat;
        }

        public int? CountListVatByCriteria(VatInfo vInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((vInfo.VatId != null) && (vInfo.VatId != 0))
            {
                strcond += "AND v.Id = " + vInfo.VatId;
            }
            if ((vInfo.VatCode != null) && (vInfo.VatCode != ""))
            {
                strcond = strcond == "" ? strcond += "where v.VatCode LIKE '%" + vInfo.VatCode.Trim() + "%'" : strcond += "AND v.VatCode LIKE '%" + vInfo.VatCode.Trim() + "%'";
            }
            if ((vInfo.VatName != null) && (vInfo.VatName != ""))
            {
                strcond = strcond == "" ? strcond += "where v.VatName LIKE '%" + vInfo.VatName.Trim() + "%'" : strcond += "AND v.VatName LIKE '%" + vInfo.VatName.Trim() + "%'";
            }
            if ((vInfo.VatValue != null) && (vInfo.VatValue != 0))
            {
                strcond = strcond == "" ? strcond += "where v.VatValue = " + vInfo.VatValue + "" : strcond += "AND v.VatValue " + vInfo.VatValue + "";
            }
            if ((vInfo.FlagActive != null) && (vInfo.FlagActive != "") && (vInfo.FlagActive != "-99"))
            {
                strcond = strcond == "" ? strcond += "where v.FlagActive LIKE '%" + vInfo.FlagActive.Trim() + "%'" : strcond += "AND v.FlagActive LIKE '%" + vInfo.FlagActive.Trim() + "%'";
            }
            if ((vInfo.FlagDelete != null) && (vInfo.FlagDelete != ""))
            {
                strcond = strcond == "" ? strcond += "where v.FlagDelete LIKE '%" + vInfo.FlagDelete.Trim() + "%'" : strcond += "AND v.FlagDelete LIKE '%" + vInfo.FlagDelete.Trim() + "%'";
            }
            var LVat = new List<VatListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = "SELECT count(v.Id) as countVat " +
                                "FROM " + dbName + ".dbo.Vat AS v " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LVat = (from DataRow dr in dt.Rows

                        select new VatListReturn()
                        {
                            countVat = Convert.ToInt32(dr["countVat"])

                        }).ToList();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LVat.Count > 0)
            {
                count = LVat[0].countVat;
            }

            return count;
        }
    }
}

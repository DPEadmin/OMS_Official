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
    public class LookupDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<LookupListReturn> ListLookupNopagingByCriteria(LookupInfo lInfo)
        {
            string strcond = "";

            if ((lInfo.LookupId != null) && (lInfo.LookupId != 0))
            {
                strcond += " and  l.Id =" + lInfo.LookupId;
            }

            if ((lInfo.LookupCode != null) && (lInfo.LookupCode != ""))
            {
                strcond += " and  l.LookupCode = '" + lInfo.LookupCode + "'";
            }
            if ((lInfo.LookupType != null) && (lInfo.LookupType != ""))
            {
                strcond += " and  l.LookupType = '" + lInfo.LookupType + "'";
            }

            if ((lInfo.LookupValue != null) && (lInfo.LookupValue != ""))
            {
                strcond += " and  l.LookupValue in ('" + lInfo.LookupValue + "')";
            }

            if ((lInfo.GroupLookupCode != null) && (lInfo.GroupLookupCode != ""))
            {
                strcond += " and  l.LookupCode in (" + lInfo.GroupLookupCode + ")";
            }
            DataTable dt = new DataTable();
            var LLookUp = new List<LookupListReturn>();

            try
            {
                string strsql = " select l.* from " + dbName + ".dbo.Lookup l " +
                               " where l.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY l.Id";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLookUp = (from DataRow dr in dt.Rows

                             select new LookupListReturn()
                             {
                                 LookupId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 LookupCode = dr["LookupCode"].ToString().Trim(),
                                 LookupType = dr["LookupType"].ToString().Trim(),
                                 LookupValue = dr["LookupValue"].ToString().Trim(),
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

            return LLookUp;
        }

        public List<LookupListReturn> GetBranch(LookupInfo lInfo)
        {
            string strcond = "";

            if ((lInfo.LookupId != null) && (lInfo.LookupId != 0))
            {
                strcond += " and  l.Id =" + lInfo.LookupId;
            }

            if ((lInfo.LookupCode != null) && (lInfo.LookupCode != ""))
            {
                strcond += " and  l.LookupCode = '" + lInfo.LookupCode + "'";
            }
            if ((lInfo.LookupType != null) && (lInfo.LookupType != ""))
            {
                strcond += " and  l.LookupType = '" + lInfo.LookupType + "'";
            }

            DataTable dt = new DataTable();
            var LLookUp = new List<LookupListReturn>();

            try
            {
                string strsql = " SELECT        a.Id, a.Branch, a.AccountName" +
                               " FROM " + dbName + ".dbo.AccountPayment AS a LEFT OUTER JOIN " + dbName +
                               " .dbo.Lookup AS l ON a.BankCode = l.LookupCode AND l.LookupType = 'BANK'" +
                               " WHERE a.BankCode = '" + lInfo.LookupCode + "'" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLookUp = (from DataRow dr in dt.Rows

                             select new LookupListReturn()
                             {
                                 AccountId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Branch = dr["Branch"].ToString().Trim(),
                                 AccountName = dr["AccountName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LLookUp;
        }

        public List<LookupListReturn> GetAccountType(LookupInfo lInfo)
        {
            string strcond = "";

            if ((lInfo.LookupId != null) && (lInfo.LookupId != 0))
            {
                strcond += " and  l.Id =" + lInfo.LookupId;
            }

            if ((lInfo.LookupCode != null) && (lInfo.LookupCode != ""))
            {
                strcond += " and  a.BankCode = '" + lInfo.LookupCode + "'";
            }
            if ((lInfo.LookupType != null) && (lInfo.LookupType != ""))
            {
                strcond += " and  l.LookupType = '" + lInfo.LookupType + "'";
            }

            DataTable dt = new DataTable();
            var LLookUp = new List<LookupListReturn>();

            try
            {
                string strsql = " SELECT        a.Id, l.LookUpCode, a.AccountName" +
                               " FROM " + dbName + ".dbo.AccountPayment AS a LEFT OUTER JOIN " + dbName +
                               " .dbo.Lookup AS l ON a.AccountType = l.LookupCode AND l.LookupType = 'ACCOUNTTYPE'" +
                               " WHERE 1=1" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLookUp = (from DataRow dr in dt.Rows

                           select new LookupListReturn()
                           {
                               AccountId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               AccountName = dr["AccountName"].ToString().Trim(),
                               AccountType = dr["LookUpCode"].ToString().Trim(),
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LLookUp;
        }

        public List<LookupListReturn> GetAccountNumber(LookupInfo lInfo)
        {
            string strcond = "";

            if ((lInfo.LookupId != null) && (lInfo.LookupId != 0))
            {
                strcond += " and  l.Id =" + lInfo.LookupId;
            }

            if ((lInfo.LookupCode != null) && (lInfo.LookupCode != ""))
            {
                strcond += " and  a.BankCode = '" + lInfo.LookupCode + "'";
            }
            if ((lInfo.LookupType != null) && (lInfo.LookupType != ""))
            {
                strcond += " and  l.LookupType = '" + lInfo.LookupType + "'";
            }

            if ((lInfo.AccountNumber != null) && (lInfo.AccountNumber != ""))
            {
                strcond += " and  l.AccountNumber = '" + lInfo.AccountNumber + "'";
            }

            DataTable dt = new DataTable();
            var LLookUp = new List<LookupListReturn>();

            try
            {
                string strsql = " SELECT a.Id, a.AccountNumber" +
                               " FROM " + dbName + ".dbo.AccountPayment AS a " +
                               " WHERE 1=1" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLookUp = (from DataRow dr in dt.Rows

                           select new LookupListReturn()
                           {
                               AccountId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               AccountNumber = dr["AccountNumber"].ToString().Trim(),
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LLookUp;
        }

        public List<LookupListReturn> GetLockAmountFlag()
        {
            string strcond = "";

          

            DataTable dt = new DataTable();
            var LLookUp = new List<LookupListReturn>();

            try
            {
                string strsql = " SELECT        a.LookupCode, a.LookupValue" +
                               " FROM " + dbName + ".dbo.Lookup AS a " +
                               " WHERE a.LookupType = 'LOCKAMOUNTFLAG'" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLookUp = (from DataRow dr in dt.Rows

                           select new LookupListReturn()
                           {
                               LookupCode = dr["LookupCode"].ToString(),
                               LookupValue = dr["LookupValue"].ToString().Trim(),
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LLookUp;
        }

        public List<LookupListReturn> ListLookupContactStatusMapOrderSituation(LookupInfo lInfo)
        {
            string strcond = "";

            if ((lInfo.LookupCode != null) && (lInfo.LookupCode != ""))
            {
                strcond += " and  co.CONTACTSTATUS = '" + lInfo.LookupCode + "'";
            }

            DataTable dt = new DataTable();
            var LLookUp = new List<LookupListReturn>();

            try
            {
                string strsql = " SELECT co.ORDERSITUATION AS LookupCode, l.LookupValue FROM " + dbName + ".dbo.ContactStatusMapOrderSituation AS co " +
                                " LEFT OUTER JOIN " + dbName + ".dbo.Lookup AS l ON l.LookupCode = co.ORDERSITUATION AND l.LookupType = 'ORDERSITUATION'" +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY l.Id";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLookUp = (from DataRow dr in dt.Rows

                           select new LookupListReturn()
                           {
                               LookupCode = dr["LookupCode"].ToString().Trim(),
                               LookupValue = dr["LookupValue"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LLookUp;
        }

    }
}

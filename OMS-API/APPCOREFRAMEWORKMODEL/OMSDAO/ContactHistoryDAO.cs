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
    public class ContactHistoryDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<ContactHistoryListReturn> ListContactByCriteria(ContactHistoryInfo conHisInfo)
        {
            string strcond = "";

            if ((conHisInfo.ContactId != null) && (conHisInfo.ContactId != 0))
            {
                strcond += " and  c.Id =" + conHisInfo.ContactId;
            }

            if ((conHisInfo.CustomerCode != null) && (conHisInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode = '" + conHisInfo.CustomerCode + "'";
            }

            DataTable dt = new DataTable();
            var LContactHistory = new List<ContactHistoryListReturn>();

            try
            {
                string strsql = " select c.*,cus.CustomerFName,cus.CustomerLName,lcs.LookupValue as ContactStatusName,e.EmpFname_TH,e.EmpLName_TH from " + dbName + ".dbo.ContactHistory c " +
                                " left join " + dbName + ".dbo.Customer cus on cus.CustomerCode = c.CustomerCode " +
                                " left join " + dbName + ".dbo.Lookup lcs on c.ContactStatus = lcs.LookupCode and lcs.LookupType = 'CONTACTSTATUS' " +
                                " left join " + dbName + ".dbo.Emp e on c.CreateBy = e.EmpCode " + 
                                " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LContactHistory = (from DataRow dr in dt.Rows

                             select new ContactHistoryListReturn()
                             {
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 ContactStatus = dr["ContactStatus"].ToString().Trim(),
                                 ContactStatusName = dr["ContactStatusName"].ToString().Trim(),
                                 ContactDesc = dr["ContactDesc"].ToString().Trim(),
                                 ContactName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 EmpFName_TH = dr["EmpFname_TH"].ToString().Trim(),
                                 EmpLName_TH = dr["EmpLName_TH"].ToString().Trim(),
                                 EmpName_TH = dr["EmpFname_TH"].ToString().Trim() + " " + dr["EmpLName_TH"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LContactHistory;
        }

        public int? CountListContactPagingByCriteria(ContactHistoryInfo conHisInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((conHisInfo.ContactId != null) && (conHisInfo.ContactId != 0))
            {
                strcond += " and  c.Id =" + conHisInfo.ContactId;
            }

            if ((conHisInfo.CustomerCode != null) && (conHisInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode = '" + conHisInfo.CustomerCode + "'";
            }

            DataTable dt = new DataTable();
            var LContactHistory = new List<ContactHistoryListReturn>();


            try
            {
                string strsql = " select count(c.Id) as countCallHistory from " + dbName + ".dbo.ContactHistory c " +
                                " left join " + dbName + ".dbo.Customer cus on cus.CustomerCode = c.CustomerCode " +
                                " left join " + dbName + ".dbo.Lookup lcs on c.ContactStatus = lcs.LookupCode and lcs.LookupType = 'CONTACTSTATUS' " +
                                " left join " + dbName + ".dbo.Emp e on c.CreateBy = e.EmpCode " +
                                " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LContactHistory = (from DataRow dr in dt.Rows

                              select new ContactHistoryListReturn()
                              {
                                  countCallHistory = Convert.ToInt32(dr["countCallHistory"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LContactHistory.Count > 0)
            {
                count = LContactHistory[0].countCallHistory;
            }

            return count;
        }

        public List<ContactHistoryListReturn> ListContactPagingByCriteria(ContactHistoryInfo conHisInfo)
        {
            string strcond = "";

            if ((conHisInfo.ContactId != null) && (conHisInfo.ContactId != 0))
            {
                strcond += " and  c.Id =" + conHisInfo.ContactId;
            }

            if ((conHisInfo.CustomerCode != null) && (conHisInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode = '" + conHisInfo.CustomerCode + "'";
            }

            DataTable dt = new DataTable();
            var LContactHistory = new List<ContactHistoryListReturn>();

            try
            {
                string strsql = " select c.*,cus.CustomerFName,cus.CustomerLName,lcs.LookupValue as ContactStatusName,e.EmpFname_TH,e.EmpLName_TH from " + dbName + ".dbo.ContactHistory c " +
                                " left join " + dbName + ".dbo.Customer cus on cus.CustomerCode = c.CustomerCode " +
                                " left join " + dbName + ".dbo.Lookup lcs on c.ContactStatus = lcs.LookupCode and lcs.LookupType = 'CONTACTSTATUS' " +
                                " left join " + dbName + ".dbo.Emp e on c.CreateBy = e.EmpCode " +
                                " where 1=1 " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + conHisInfo.rowOFFSet + " ROWS FETCH NEXT " + conHisInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LContactHistory = (from DataRow dr in dt.Rows

                                   select new ContactHistoryListReturn()
                                   {
                                       CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                       ContactStatus = dr["ContactStatus"].ToString().Trim(),
                                       ContactStatusName = dr["ContactStatusName"].ToString().Trim(),
                                       ContactDesc = dr["ContactDesc"].ToString().Trim(),
                                       ContactName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                       CreateDate = dr["CreateDate"].ToString(),
                                       CreateBy = dr["CreateBy"].ToString(),
                                       EmpFName_TH = dr["EmpFname_TH"].ToString().Trim(),
                                       EmpLName_TH = dr["EmpLName_TH"].ToString().Trim(),
                                       EmpName_TH = dr["EmpFname_TH"].ToString().Trim() + " " + dr["EmpLName_TH"].ToString().Trim(),
                                       UpdateBy = dr["UpdateBy"].ToString(),
                                       UpdateDate = dr["UpdateDate"].ToString(),
                                       FlagDelete = dr["FlagDelete"].ToString(),
                                   }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LContactHistory;
        }

        public int InsertContactHis(ContactHistoryInfo conHisInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.ContactHistory  (CustomerCode,ContactStatus,ContactResult,ContactDesc,OrderCode,Emp,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                             "VALUES (" +
                             "'" + conHisInfo.CustomerCode + "'," +
                            "'" + conHisInfo.ContactStatus + "'," +
                            "'" + conHisInfo.ContactResult + "'," +
                            "'" + conHisInfo.ContactDesc + "'," +
                            "'" + conHisInfo.OrderCode + "'," +
                            "'" + conHisInfo.Emp + "'," +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + conHisInfo.CreateBy + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + conHisInfo.CreateBy + "'," +
                            "'" + conHisInfo.FlagDelete + "'" +

                             ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}

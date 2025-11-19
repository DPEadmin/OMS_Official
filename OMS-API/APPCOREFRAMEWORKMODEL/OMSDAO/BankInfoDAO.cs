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
    public class BankInfoDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int InsertBankInfo(BankInfo bInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO BankInfo  (BankName, BankAccountNumber, BankAccountType, AccountName, BranchName, MerchantCode, CreateDate,CreateBy,UpdateDate,UpdateBy, FlagDelete, Active )" +
                           " OUTPUT inserted.Id VALUES (" +
                           "'" + bInfo.BankName + "'," +
                           "'" + bInfo.BankAccountNumber + "'," +
                           "'" + bInfo.BankAccountType + "'," +
                           "'" + bInfo.AccountName + "'," +
                           "'" + bInfo.BranchName + "'," +
                           "'" + bInfo.MerchantCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + bInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + bInfo.UpdateBy + "'," +
                           "'" + bInfo.FlagDelete + "'," +
                           "'" + bInfo.Active + "')"
                           ;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateBankInfo(BankInfo bInfo)
        {
            {
                int i = 0;

                string strsql = " Update " + dbName + ".dbo.BankInfo set " + "(BankName, BankAccountNumber, BankAccountType, AccountName, BranchName, MerchantCode, CreateDate,CreateBy,UpdateDate,UpdateBy, FlagDelete, Active ) values (" +

                           "'" + bInfo.BankName + "'," +
                           "'" + bInfo.BankAccountNumber + "'," +
                           "'" + bInfo.BankAccountType + "'," +
                           "'" + bInfo.AccountName + "'," +
                           "'" + bInfo.BranchName + "'," +
                           "'" + bInfo.MerchantCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + bInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + bInfo.UpdateBy + "'," +
                           "'" + bInfo.FlagDelete + "'," +
                           "'" + bInfo.Active + "')" +
                             " where Id in(" + bInfo.MerchantCode + ")";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);

                return i;
            }
        }
    }
}

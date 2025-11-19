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
    public class CustomerOrderSlipDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int InsertOrderFile(CustomerOrderSlipInfo opfInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO CustomerOrderSlip  (CustomerCode,Ordercode,MerchantCode,OrderSlipImageUrl,FlagDelete,Active,CreateDate,CreateBy,UpdateDate,UpdateBy )" +
                           " OUTPUT inserted.Id VALUES (" +
                           "'" + opfInfo.CustomerCode + "'," +
                           "'" + opfInfo.Ordercode + "'," +
                           "'" + opfInfo.MerchantCode + "'," +
                           "'" + opfInfo.OrderSlipImageUrl + "'," +
                           "'" + opfInfo.FlagDelete + "'," +
                           "'" + opfInfo.Active + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + opfInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + opfInfo.UpdateBy + "')"
                           ;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}

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
    public class PaymentTypeDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<PaymentTypeListReturn> ListPaymentTypeNopagingByCriteria(PaymentTypeInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PaymentTypeId != null) && (pInfo.PaymentTypeId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PaymentTypeId;
            }

            if ((pInfo.PaymentTypeCode != null) && (pInfo.PaymentTypeCode != ""))
            {
                strcond += " and  p.PaymentTypeCode like '%" + pInfo.PaymentTypeCode + "%'";
            }
            if ((pInfo.PaymentTypeName != null) && (pInfo.PaymentTypeName != ""))
            {
                strcond += " and  p.PaymentTypeName like '%" + pInfo.PaymentTypeName + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<PaymentTypeListReturn>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.PaymentType p " +

                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new PaymentTypeListReturn()
                             {
                                 PaymentTypeId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 PaymentTypeCode = dr["PaymentTypeCode"].ToString().Trim(),
                                 PaymentTypeName = dr["PaymentTypeName"].ToString().Trim(),
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

            return LCampaign;
        }

    }
}

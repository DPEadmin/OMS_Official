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
    public class CallInDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<CallInListReturn> ListCallInNoPagingByCriteria(CallInInfo callInfo)
        {
            string strcond = "";

            if ((callInfo.AgentId != null) && (callInfo.AgentId != ""))
            {
                strcond += " and  c.AgentId = '" + callInfo.AgentId + "'";
            }

            if ((callInfo.Type != null) && (callInfo.Type != ""))
            {
                strcond += " and  c.Type = '" + callInfo.Type + "'";
            }

            DataTable dt = new DataTable();
            var LCallin = new List<CallInListReturn>();

            try
            {
                string strsql = " select ch.LookupValue AS ChannelName, c.* from " + dbName + ".dbo.CallIn c " +
                                " left join Lookup ch on c.ChannelCode = ch.LookupCode and ch.LookupType='CHANNEL' " +
                                " where 1=1 " + strcond;
                strsql += " ORDER BY c.Id Desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                             select new CallInListReturn()
                             {
                                 CallInId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CallInNumber = dr["CallInNumber"].ToString().Trim(),
                                 DNIS = dr["DNIS"].ToString().Trim(),
                                 AgentId = dr["AgentId"].ToString().Trim(),
                                 BUCode = dr["BUCode"].ToString().Trim(),
                                 ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
    }
}

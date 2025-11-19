using APPCOREMODEL.Datas.OMSDTO;
using APPHELPPERS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPCOREMODEL.OMSDAO
{
    public class LCCDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<LCCInfo> GetLCC(LCCInfo lccInfo)
        {
            string strcond = "";

            if ((lccInfo.LCCID != null) && (lccInfo.LCCID != 0))
            {
                strcond += strcond == "" ? strcond += " WHERE lcc.Id =" + lccInfo.LCCID : strcond += " AND lcc.Id =" + lccInfo.LCCID;
            }

            if ((lccInfo.Limit != null) && (lccInfo.Limit != 0))
            {
                strcond += strcond == "" ? strcond += " WHERE lcc.Limit =" + lccInfo.Limit : strcond += " AND lcc.Limit =" + lccInfo.Limit;
            }

            if ((lccInfo.CountRun != null) && (lccInfo.CountRun != 0))
            {
                strcond += strcond == "" ? strcond += " WHERE lcc.CountRun =" + lccInfo.CountRun : strcond += " AND lcc.CountRun =" + lccInfo.CountRun;
            }

            if ((lccInfo.X != null) && (lccInfo.X != 0))
            {
                strcond += strcond == "" ? strcond += " WHERE lcc.X =" + lccInfo.X : strcond += " AND lcc.X =" + lccInfo.X;
            }

            if ((lccInfo.Account_Name != null) && (lccInfo.Account_Name != ""))
            {
                strcond += strcond == "" ? strcond += " WHERE lcc.Account_Name =" + "'" + lccInfo.Account_Name + "'" : strcond += " AND lcc.Account_Name = " + "'" + lccInfo.Account_Name + "'";
            }

            var listLCC = new List<LCCInfo>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = "SELECT lcc.ID,lcc.X,lcc.Account_Name,lcc.Limit,LCC.CountRun,LCC.X ,(LCC.Limit - LCC.CountRun) as Expire " +
                    "FROM " + dbName + ".dbo.LCC AS lcc " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                listLCC = (from DataRow dr in dt.Rows
                           select new LCCInfo()
                           {
                               LCCID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               Limit = (dr["Limit"].ToString() != "") ? Convert.ToInt32(dr["Limit"]) : 0,
                               CountRun = (dr["CountRun"].ToString() != "") ? Convert.ToInt32(dr["CountRun"]) : 0,
                               X = (dr["X"].ToString() != "") ? Convert.ToInt32(dr["X"]) : 0,
                               Expire = (dr["Expire"].ToString() != "") ? Convert.ToInt32(dr["Expire"]) : 0,
                               Account_Name = dr["Account_Name"].ToString().Trim(),
                           }
                           ).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return listLCC;
        }
    }
}

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
    public class GenderDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<GenderListReturn> ListGenderNopagingByCriteria(GenderInfo gInfo)
        {
            string strcond = "";

            if ((gInfo.GenderCode != null) && (gInfo.GenderCode != ""))
            {
                strcond += " and  g.GenderCode = '" + gInfo.GenderCode +"'";
            }

            if ((gInfo.GenderName != null) && (gInfo.GenderName != ""))
            {
                strcond += " and  g.GenderName like '%" + gInfo.GenderName + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<GenderListReturn>();

            try
            {
                string strsql = " select g.* from " + dbName + ".dbo.Gender g " +

                               " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new GenderListReturn()
                             {
                                 GenderId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 GenderCode = dr["GenderCode"].ToString().Trim(),
                                 GenderName = dr["GenderName"].ToString().Trim(),
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

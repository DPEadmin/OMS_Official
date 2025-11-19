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
    public class ModuleDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateModule(ModuleInfo mInfo) {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Module set " +
                            " ActiveFlag = '" + mInfo.ActiveFlag + "'," +
                            " UpdateBy = '" + mInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where ModuleCode =" + mInfo.ModuleCode + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<ModuleListReturn> ListModuleNopagingByCriteria(ModuleInfo mInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LCampaign = new List<ModuleListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Module c " +
                                " ORDER BY c.Id ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ModuleListReturn()
                             {
                                 ModuleId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ModuleCode = dr["ModuleCode"].ToString().Trim(),
                                 ModuleName = dr["ModuleName"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 ActiveFlag = dr["ActiveFlag"].ToString(),
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

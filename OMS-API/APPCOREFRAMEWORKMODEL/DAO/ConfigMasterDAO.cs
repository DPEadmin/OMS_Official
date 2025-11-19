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
namespace APPCOREMODEL.DAO
{
public	class ConfigMasterDAO
    {
		public List<ConfigMaster> GetConfigMaster()
		{

			string strcond = "";
			DataTable dt = new DataTable();
			var LConfig = new List<ConfigMaster>();

			try
			{
				string strsql = "        SELECT [id]      ,[NameTB]      ,[Version]      ,[Vendor]      ,[Role]  FROM[dbo].[ConfigMaster] " +
								
								strcond;
				Database db = new Database(APPHELPPERS.Driver.ConntectionString());        
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LConfig = (from DataRow dr in dt.Rows

                               select new ConfigMaster()
                               {
                                   NameTB = dr["NameTB"].ToString().Trim(),
                                   Version = dr["Version"].ToString().Trim(),
                                   Vendor  = dr["Vendor"].ToString().Trim(),
                                   Role = dr["Role"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

			return LConfig; //Return LConfig
		}

	}
}

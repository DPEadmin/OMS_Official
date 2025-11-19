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
public	class LookUpMobileDAO
    {
		public List<MasterLookup> GetLookUp()
		{

			string strcond = "";
			DataTable dt = new DataTable();
			var LLookupMobile = new List<MasterLookup>();

			try
			{
                string strsql = " select id,LookupCode,LookupType,LookupValue,RefferenceJobstatus  from Lookup lu " +
                                "   where LookupType in ('JOBSTATUS', 'JOBITEMSTATUS', 'JOBPROBLEMSTATUS','JOBCLASS','JOBBOISTATUS') and lu.FlagDelete = 'N' " +
                                "  order by   CASE LookupType  " +
                                "    WHEN 'JOBSTATUS' THEN 1 " +
                                "     WHEN 'JOBITEMSTATUS' THEN 2 " +
                                "    WHEN 'JOBPROBLEMSTATUS' THEN 3 " +
                                "   WHEN 'JOBCLASS' THEN 4 " +
                                "  WHEN 'JOBBOISTATUS' THEN 5 " +
                                " END,id ";
							
				Database db = new Database(APPHELPPERS.Driver.ConntectionString());        
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLookupMobile = (from DataRow dr in dt.Rows

                               select new MasterLookup()
                               {
                                   lookup_id = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,                                
                                   LookupCode = dr["LookupCode"].ToString().Trim(),
                                   LookupType = dr["LookupType"].ToString().Trim(),
                                   LookupValue = dr["LookupValue"].ToString().Trim(),
                                   RefferenceJobstatus = dr["RefferenceJobstatus"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

			return LLookupMobile;
		}

	}
}

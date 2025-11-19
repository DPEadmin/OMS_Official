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
public	class LogoMobileDAO
    {
		public List<LogoVendor> GetLogo(EmpLogin empInfo)
		{

			string strcond = "";
			DataTable dt = new DataTable();
			var LLogoVendor = new List<LogoVendor>();

			try
			{
				string strsql = " SELECT        mmv.VendorCode, mmv.Id,mmv.MediaBase64 " +
                                         "   FROM MediaFile AS mmv " +
                                          "  WHERE mmv.VendorCode IN " +
								"	('"+empInfo.VendorCode+"')" +
								
								strcond;
				Database db = new Database(APPHELPPERS.Driver.ConntectionString());        
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LLogoVendor = (from DataRow dr in dt.Rows

                               select new LogoVendor()
                               {
                                   id = dr["Id"].ToString().Trim(),
                                   MediaBase64 = dr["MediaBase64"].ToString().Trim(),
                                   VendorCode = dr["VendorCode"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

			return LLogoVendor;
		}

	}
}

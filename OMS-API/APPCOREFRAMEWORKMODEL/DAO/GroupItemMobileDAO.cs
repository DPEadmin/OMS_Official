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
public	class GroupItemMobileDAO
    {
		public List<GroupItemDetail> GetItem()
		{

			string strcond = "";
			DataTable dt = new DataTable();
			var LItemDetail = new List<GroupItemDetail>();

			try
			{
                string strsql = "select gi.GroupCode,gi.GroupName,i.Id,i.ItemName,gid.Amount from GroupItem gi "+
                                " inner join GroupItemDetail gid on gid.GroupCode = gi.GroupCode "+
                                " inner join Item i on gid.ItemID = i.Id "+
                                "  where gi.FlagDelete = 'N' and gid.FlagDelete = 'N' and i.FlagDelete = 'N' ";
							
				Database db = new Database(APPHELPPERS.Driver.ConntectionString());        
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LItemDetail = (from DataRow dr in dt.Rows

                               select new GroupItemDetail()
                               {
                                   GroupCode = dr["GroupCode"].ToString().Trim(),
                                   GroupName = dr["GroupName"].ToString().Trim(),
                                   ItemName = dr["ItemName"].ToString().Trim(),
                                   Amount = dr["Amount"].ToString().Trim(),
                                   ItemID = dr["Id"].ToString().Trim()
                               }).ToList();

            }
            catch (Exception ex)
			{
				throw new Exception(ex.Message, ex);
			}

			return LItemDetail;
		}

	}
}

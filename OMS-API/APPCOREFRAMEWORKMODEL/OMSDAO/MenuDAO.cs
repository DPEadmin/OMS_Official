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
    public class MenuDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

    

        public List<MenuListReturn> ListMenuByCriteria(MenuInfo mInfo)
        {
            string strcond = "";

            if ((mInfo.EmpCode != null) && (mInfo.EmpCode != ""))
            {
                strcond += " and  e.EmpCode = '" + mInfo.EmpCode + "'";
            }
            if ((mInfo.ModuleNo != null) && (mInfo.ModuleNo != ""))
            {
                strcond += " and  m.ModuleNo = '" + mInfo.ModuleNo + "'";
            }

            DataTable dt = new DataTable();
            var LMenu = new List<MenuListReturn>();

            try
            {
                string strsql = "SELECT        m.Id, m.MenuName, m.MenuURL, m.ParentId,m.Style, m.ranking     " +
                                " FROM            " + dbName + ".dbo.Menu AS m LEFT OUTER JOIN       " +
                                "                         " + dbName + ".dbo.RoleMenu AS rm ON m.Id = rm.MenuId LEFT OUTER JOIN   " +
                                "                         " + dbName + ".dbo.Role AS r ON r.RoleCode = rm.RoleCode LEFT OUTER JOIN   " +
                                "                         " + dbName + ".dbo.EmpRole AS er ON er.RoleCode = r.RoleCode LEFT OUTER JOIN   " +
                                "                         " + dbName + ".dbo.Emp AS e ON e.EmpCode = er.EmpCode    "
                                + " WHERE 1=1 and  rm.FlagDelete='N'  " + strcond +
                                " GROUP BY m.Id, m.MenuName, m.MenuURL, m.ParentId,m.Style, m.ranking   " +
                                " ORDER BY m.ranking      ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMenu = (from DataRow dr in dt.Rows

                             select new MenuListReturn()
                             {
                                 Id = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 MenuName = dr["MenuName"].ToString().Trim(),
                                 MenuUrl = dr["MenuUrl"].ToString().Trim(),
                                 ParentId = (dr["ParentId"].ToString() != "") ? Convert.ToInt32(dr["ParentId"]) : 0,
                                 Style = dr["Style"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMenu;
        }

        public List<MenuListReturn> ListMenuNull(MenuInfo mInfo)
        {
            string strcond = "";

           
            DataTable dt = new DataTable();
            var LMenu = new List<MenuListReturn>();

            try
            {
                string strsql = "SELECT        m.Id, m.MenuName, m.MenuURL, m.ParentId,m.Style     " +
                                " FROM            " + dbName + ".dbo.Menu AS m " +
                                " LEFT JOIN " + dbName + ".dbo.RoleMenu AS r on r.MenuId  = m.Id  " +
                                " WHERE  r.RoleCode IS NULL " +
                                " ORDER BY m.Id      ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMenu = (from DataRow dr in dt.Rows

                         select new MenuListReturn()
                         {
                             Id = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                             MenuName = dr["MenuName"].ToString().Trim(),
                             MenuUrl = dr["MenuUrl"].ToString().Trim(),
                             ParentId = (dr["ParentId"].ToString() != "") ? Convert.ToInt32(dr["ParentId"]) : 0,
                             Style = dr["Style"].ToString().Trim(),
                         }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMenu;
        }

        public int? CountMenubyCriteria()
        {
            string strcond = "";
            int? count = 0;

            DataTable dt = new DataTable();
            var LMenu = new List<MenuListReturn>();


            try
            {
                string strsql = "SELECT        count(m.MenuName) as countMenu FROM   " + dbName + ".dbo.Menu m " +

                            " where m.Main ='Y' ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMenu = (from DataRow dr in dt.Rows

                             select new MenuListReturn()
                             {
                                 countMenu = Convert.ToInt32(dr["countMenu"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LMenu.Count > 0)
            {
                count = LMenu[0].countMenu;
            }

            return count;
        }

        public List<MenuListReturn> ListMenuPageLoadPageEditRanking(MenuInfo mInfo)
        {
            string strcond = "";


            DataTable dt = new DataTable();
            var LMenu = new List<MenuListReturn>();

            try
            {
                string strsql = "SELECT       m.Id, m.MenuName, m.Ranking     " +
                                " FROM            " + dbName + ".dbo.Menu AS m WHERE        (Main = 'Y') " +
                                " ORDER BY m.ranking OFFSET " + mInfo.rowOFFSet + " ROWS FETCH NEXT " + mInfo.rowFetch + " ROWS ONLY     ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LMenu = (from DataRow dr in dt.Rows

                         select new MenuListReturn()
                         {
                             Id = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                             MenuName = dr["MenuName"].ToString().Trim(),
                             Ranking = dr["Ranking"].ToString().Trim(),
                         }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LMenu;
        }

        public int? UpdateMenuRanking(MenuInfo mInfo)
        {
            int? i = 0;

            string strsql = " Update " + dbName + ".dbo.Menu set " +
                            " Ranking = '" + mInfo.Ranking + "', " +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                            " UpdateBy = '" + mInfo.UpdateBy + "'" +
                            " where Id = '" + mInfo.Id + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}

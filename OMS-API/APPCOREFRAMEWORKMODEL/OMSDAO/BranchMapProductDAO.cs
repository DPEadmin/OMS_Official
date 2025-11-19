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
    public class BranchMapProductDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountListBranchMapProductByCriteria(BranchMapProductInfo bmpInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((bmpInfo.BranchMapProductId != null) && (bmpInfo.BranchMapProductId != 0))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.Id = " + bmpInfo.BranchMapProductId + "" : strcond += " AND  bmp.Id = " + bmpInfo.BranchMapProductId + "";
            }

            if ((bmpInfo.ProductCode != null) && (bmpInfo.ProductCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'" : strcond += " AND  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'";
            }

            if ((bmpInfo.BranchCode != null) && (bmpInfo.BranchCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'" : strcond += " AND  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'";
            }

            if ((bmpInfo.ProductCodeList != null) && (bmpInfo.ProductCodeList != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')" : strcond += " AND bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')";
            }

            if ((bmpInfo.ProductName != null) && (bmpInfo.ProductName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  p.ProductName like '%" + bmpInfo.ProductName + "%'" : strcond += " and  p.ProductName like '%" + bmpInfo.ProductName + "%'";
            }

            if ((bmpInfo.RecipeCode != null) && (bmpInfo.RecipeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  pmr.RecipeCode = '" + bmpInfo.RecipeCode + "'" : strcond += " and  pmr.RecipeCode = '" + bmpInfo.RecipeCode + "'";
            }

            if ((bmpInfo.Active != null) && (bmpInfo.Active != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE bmp.Active = '" + bmpInfo.Active + "'" : strcond += " AND  bmp.Active = '" + bmpInfo.Active + "'";
            }

            DataTable dt = new DataTable();
            var bmpList = new List<BranchMapProductList>();


            try
            {
                string strsql =
                    " SELECT count(DISTINCT bmp.ProductCode) AS countOrder " +
                    " FROM " + dbName + ".dbo.BranchMapProduct AS bmp " +
                    " LEFT JOIN product AS p ON p.ProductCode = bmp.ProductCode " +
                    " LEFT JOIN ProductMapRecipe AS pmr ON p.ProductCode = pmr.ProductCode " +
                    " LEFT JOIN Recipe AS r ON pmr.RecipeCode = r.RecipeCode " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                bmpList = (from DataRow dr in dt.Rows

                            select new BranchMapProductList()
                            {
                                countOrder = Convert.ToInt32(dr["countOrder"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (bmpList.Count > 0)
            {
                count = bmpList[0].countOrder;
            }

            return count;
        }

        public List<BranchMapProductList> ListBranchMapProductByCriteria(BranchMapProductInfo bmpInfo)
        {
            string strcond = "";

            if ((bmpInfo.BranchMapProductId != null) && (bmpInfo.BranchMapProductId != 0))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.Id = " + bmpInfo.BranchMapProductId + "" : strcond += " AND  bmp.Id = " + bmpInfo.BranchMapProductId + "";
            }

            if ((bmpInfo.ProductCode != null) && (bmpInfo.ProductCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'" : strcond += " AND  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'";
            }

            if ((bmpInfo.BranchCode != null) && (bmpInfo.BranchCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'" : strcond += " AND  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'";
            }

            if ((bmpInfo.ProductCodeList != null) && (bmpInfo.ProductCodeList != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')" : strcond += " AND bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')";
            }
   
            if ((bmpInfo.ProductName != null) && (bmpInfo.ProductName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  p.ProductName like '%" + bmpInfo.ProductName + "%'" : strcond += " and  p.ProductName like '%" + bmpInfo.ProductName + "%'";
            }

            if ((bmpInfo.RecipeCode != null) && (bmpInfo.RecipeCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  pmr.RecipeCode = '" + bmpInfo.RecipeCode + "'" : strcond += " and  pmr.RecipeCode = '" + bmpInfo.RecipeCode + "'";
            }

            if ((bmpInfo.Active != null) && (bmpInfo.Active != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE bmp.Active = '" + bmpInfo.Active + "'" : strcond += " AND  bmp.Active = '" + bmpInfo.Active + "'";
            }

            DataTable dt = new DataTable();
            var LBranchMapProduct = new List<BranchMapProductList>();

            try
            {

                string strsql =
                    " SELECT DISTINCT bmp.Id,bmp.ProductCode,p.ProductName,bmp.Active, bmp.ActiveCancelProduct, bmp.UpdateDate " +
                    " FROM " + dbName + ".dbo.BranchMapProduct AS bmp " +
                    " LEFT JOIN product AS p ON p.ProductCode = bmp.ProductCode " +
                    " LEFT JOIN ProductMapRecipe AS pmr ON p.ProductCode = pmr.ProductCode " +
                    " LEFT JOIN Recipe AS r ON pmr.RecipeCode = r.RecipeCode " + strcond;

                strsql += " ORDER BY bmp.ProductCode DESC OFFSET " + bmpInfo.rowOFFSet + " ROWS FETCH NEXT " + bmpInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LBranchMapProduct = (from DataRow dr in dt.Rows

                          select new BranchMapProductList()
                          {
                              BranchMapProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              ProductCode = dr["ProductCode"].ToString(),
                              ProductName = dr["ProductName"].ToString(),
                              Active = dr["Active"].ToString(),
                              ActiveCancelProduct = dr["ActiveCancelProduct"].ToString(),
                              UpdateDate = dr["UpdateDate"].ToString(),
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LBranchMapProduct;
        }

        public int? CountListBranchMapProductByCriteriaWithOneTxtbox(BranchMapProductInfo bmpInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((bmpInfo.BranchMapProductId != null) && (bmpInfo.BranchMapProductId != 0))
            {
                strcond += " AND  bmp.Id = " + bmpInfo.BranchMapProductId + "";
            }

            if ((bmpInfo.ProductCode != null) && (bmpInfo.ProductCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'" : strcond += " AND  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'";
            }

            if ((bmpInfo.BranchCode != null) && (bmpInfo.BranchCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'" : strcond += " AND  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'";
            }

            if ((bmpInfo.ProductCodeList != null) && (bmpInfo.ProductCodeList != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')" : strcond += " AND bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')";
            }


            if ((bmpInfo.ProductName != null) && (bmpInfo.ProductName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  (r.RecipeName like '%" + bmpInfo.ProductName + "%' OR p.ProductName like '%" + bmpInfo.ProductName + "%') " : strcond += " and  (r.RecipeName like '%" + bmpInfo.ProductName + "%' OR p.ProductName like '%" + bmpInfo.ProductName + "%') ";
            }

            if ((bmpInfo.Active != null) && (bmpInfo.Active != "") && (bmpInfo.Active != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE bmp.Active = '" + bmpInfo.Active + "'" : strcond += " AND  bmp.Active = '" + bmpInfo.Active + "'";
            }

            DataTable dt = new DataTable();
            var bmpList = new List<BranchMapProductList>();


            try
            {
                string strsql =
                    " SELECT count(DISTINCT bmp.ProductCode) AS countOrder " +
                    " FROM " + dbName + ".dbo.BranchMapProduct AS bmp " +
                    " LEFT JOIN product AS p ON p.ProductCode = bmp.ProductCode " +
                    " LEFT JOIN ProductMapRecipe AS pmr ON p.ProductCode = pmr.ProductCode " +
                    " LEFT JOIN Recipe AS r ON pmr.RecipeCode = r.RecipeCode " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                bmpList = (from DataRow dr in dt.Rows

                           select new BranchMapProductList()
                           {
                               countOrder = Convert.ToInt32(dr["countOrder"])
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (bmpList.Count > 0)
            {
                count = bmpList[0].countOrder;
            }

            return count;
        }

        public List<BranchMapProductList> ListBranchMapProductByCriteriaWithOneTxtbox(BranchMapProductInfo bmpInfo)
        {
            string strcond = "";

            if ((bmpInfo.BranchMapProductId != null) && (bmpInfo.BranchMapProductId != 0))
            {
                strcond += " AND  bmp.Id = " + bmpInfo.BranchMapProductId + "";
            }

            if ((bmpInfo.ProductCode != null) && (bmpInfo.ProductCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'" : strcond += " AND  bmp.ProductCode like '%" + bmpInfo.ProductCode + "%'";
            }

            if ((bmpInfo.BranchCode != null) && (bmpInfo.BranchCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'" : strcond += " AND  bmp.BranchCode like '%" + bmpInfo.BranchCode + "%'";
            }

            if ((bmpInfo.ProductCodeList != null) && (bmpInfo.ProductCodeList != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')" : strcond += " AND bmp.ProductCode in ('" + bmpInfo.ProductCodeList + "')";
            }


            if ((bmpInfo.ProductName != null) && (bmpInfo.ProductName != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  (r.RecipeName like '%" + bmpInfo.ProductName + "%' OR p.ProductName like '%" + bmpInfo.ProductName + "%') " : strcond += " and  (r.RecipeName like '%" + bmpInfo.ProductName + "%' OR p.ProductName like '%" + bmpInfo.ProductName + "%') ";
            }

            if ((bmpInfo.Active != null) && (bmpInfo.Active != "") && (bmpInfo.Active != "-99"))
            {
                strcond = strcond == "" ? strcond += " WHERE bmp.Active = '" + bmpInfo.Active + "'" : strcond += " AND  bmp.Active = '" + bmpInfo.Active + "'";
            }

            DataTable dt = new DataTable();
            var LBranchMapProduct = new List<BranchMapProductList>();

            try
            {

                string strsql =
                    " SELECT DISTINCT bmp.Id,bmp.ProductCode,p.ProductName,bmp.Active, bmp.ActiveCancelProduct, bmp.UpdateDate " +
                    " FROM " + dbName + ".dbo.BranchMapProduct AS bmp " +
                    " LEFT JOIN product AS p ON p.ProductCode = bmp.ProductCode " +
                    " LEFT JOIN ProductMapRecipe AS pmr ON p.ProductCode = pmr.ProductCode " +
                    " LEFT JOIN Recipe AS r ON pmr.RecipeCode = r.RecipeCode "
                    + strcond;

                strsql += " ORDER BY bmp.ProductCode DESC OFFSET " + bmpInfo.rowOFFSet + " ROWS FETCH NEXT " + bmpInfo.rowFetch + " ROWS ONLY";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LBranchMapProduct = (from DataRow dr in dt.Rows

                                     select new BranchMapProductList()
                                     {
                                         BranchMapProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                         ProductCode = dr["ProductCode"].ToString(),
                                         ProductName = dr["ProductName"].ToString(),
                                         Active = dr["Active"].ToString(),
                                         ActiveCancelProduct = dr["ActiveCancelProduct"].ToString(),
                                         UpdateDate = dr["UpdateDate"].ToString(),

                                     }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LBranchMapProduct;
        }

        public int UpdateBranchMapProductActiveFlag(BranchMapProductInfo eInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.BranchMapProduct set " +
                            " Active = '" + eInfo.Active + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +                           
                            " UpdateBy ='" + eInfo.UpdateBy + "'" +
                            " where Id = " + eInfo.BranchMapProductId;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateBranchMapProductCancelFlag(BranchMapProductInfo eInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.BranchMapProduct set " +
                            " ActiveCancelProduct = '" + eInfo.ActiveCancelProduct + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + eInfo.UpdateBy + "'" +
                            " where Id = " + eInfo.BranchMapProductId;

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateBranchMapProductActiveFlagWithString(BranchMapProductInfo eInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.BranchMapProduct set " +
                            " Active = '" + eInfo.Active + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + eInfo.UpdateBy + "'" +
                            " where Id in (" + eInfo.ProductCode + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}

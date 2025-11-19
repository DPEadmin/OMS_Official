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
    public class RecipeDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateRecipe(RecipeInfo cInfo)
        {
            int i = 0;
            
            string strsql = " Update " + dbName + ".dbo.Recipe set " +
                           " RecipeCode = '" + cInfo.RecipeCode + "'," +
                           " RecipeName = '" + cInfo.RecipeName + "'," +
                          
                          
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.RecipeId + "";
            

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteRecipe(RecipeInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Recipe set FlagDelete = 'Y' where Id in (" + cInfo.RecipeIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertRecipe(RecipeInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Recipe  (RecipeCode,RecipeName,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.RecipeCode + "'," +
                           "'" + cInfo.RecipeName + "'," +
                          
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<RecipeListReturn> ListRecipeNoPagingByCriteria(RecipeInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.RecipeId != null) && (cInfo.RecipeId != 0))
            {
                strcond += " and  c.Id =" + cInfo.RecipeId;
            }

            if ((cInfo.RecipeCode != null) && (cInfo.RecipeCode != ""))
            {
                strcond += " and  c.RecipeCode like '%" + cInfo.RecipeCode + "%'";
            }
            if ((cInfo.RecipeName != null) && (cInfo.RecipeName != ""))
            {
                strcond += " and  c.RecipeName like '%" + cInfo.RecipeName + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<RecipeListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Recipe c " +
                              
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                            select new RecipeListReturn()
                            {
                                RecipeId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                RecipeCode = dr["RecipeCode"].ToString().Trim(),
                                RecipeName = dr["RecipeName"].ToString().Trim(),
                              
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<RecipeListReturn> ListRecipePagingByCriteria(RecipeInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.RecipeId != null) && (cInfo.RecipeId != 0))
            {
                strcond += " and  c.Id =" + cInfo.RecipeId;
            }

            if ((cInfo.RecipeCode != null) && (cInfo.RecipeCode != ""))
            {
                strcond += " and  c.RecipeCode like '%" + cInfo.RecipeCode + "%'";
            }
            if ((cInfo.RecipeName != null) && (cInfo.RecipeName != ""))
            {
                strcond += " and  c.RecipeName like '%" + cInfo.RecipeName + "%'";
            }

           

            DataTable dt = new DataTable();
            var LCampaign = new List<RecipeListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Recipe c " +
                            
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RecipeListReturn()
                             {
                                 RecipeId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 RecipeCode = dr["RecipeCode"].ToString().Trim(),
                                 RecipeName = dr["RecipeName"].ToString().Trim(),

                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int? CountRecipeListByCriteria(RecipeInfo cInfo) {
            string strcond = "";
            int? count = 0;

            if ((cInfo.RecipeId != null) && (cInfo.RecipeId != 0))
            {
                strcond += " and  c.Id =" + cInfo.RecipeId;
            }

            if ((cInfo.RecipeCode != null) && (cInfo.RecipeCode != ""))
            {
                strcond += " and  c.RecipeCode like '%" + cInfo.RecipeCode + "%'";
            }
            if ((cInfo.RecipeName != null) && (cInfo.RecipeName != ""))
            {
                strcond += " and  c.RecipeName like '%" + cInfo.RecipeName + "%'";
            }

          

            DataTable dt = new DataTable();
            var LCampaign = new List<RecipeListReturn>();


            try
            {
                string strsql = "select count(c.Id) as countRecipe from " + dbName + ".dbo.Recipe c " +

                         " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RecipeListReturn()
                             {
                                 countRecipe = Convert.ToInt32(dr["countRecipe"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countRecipe;
            }

            return count;
        }

     
        public List<RecipeListReturn> RecipeCodeValidateInsert(RecipeInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.RecipeCode != null) && (cInfo.RecipeCode != ""))
            {
                strcond += " and  c.RecipeCode = '" + cInfo.RecipeCode + "'";
            }
            if ((cInfo.RecipeName != null) && (cInfo.RecipeName != ""))
            {
                strcond += " and  c.RecipeName = '" + cInfo.RecipeName + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<RecipeListReturn>();

            try
            {
                string strsql = " select c.RecipeCode from " + dbName + ".dbo.Recipe c " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RecipeListReturn()
                             {
                                 RecipeCode = dr["RecipeCode"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int InsertRecipeImport(List<RecipeInfo> lcinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in lcinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.Recipe (RecipeCode,CamCate_name,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete) values (" +
                             "'" + info.RecipeCode + "', " +
                             "'" + info.RecipeName + "', " +
                                                  
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.UpdateBy + "', " +
                             "'" + info.FlagDelete + "')";
                lSQL.Add(strsql);
            }

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            foreach (string strq in lSQL)
            {
                com.CommandText = strq;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            }
            return i;
        }

        public List<ProductMapRecipeReturnInfo> ListProductMapRecipeNoPagingByCriteria(ProductMapRecipeInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.ProductMapRecipeId != null) && (cInfo.ProductMapRecipeId != 0))
            {
                strcond += " and  pmr.Id =" + cInfo.ProductMapRecipeId;
            }

            if ((cInfo.RecipeCode != null) && (cInfo.RecipeCode != ""))
            {
                strcond += " and  pmr.RecipeCode like '%" + cInfo.RecipeCode + "%'";
            }
            if ((cInfo.ProductCode != null) && (cInfo.ProductCode != ""))
            {
                strcond += " and  pmr.ProductCode like '" + cInfo.ProductCode + "'";
            }




            DataTable dt = new DataTable();
            var LCampaign = new List<ProductMapRecipeReturnInfo>();

            try
            {
                string strsql = " select pmr.id,p.ProductCode,p.ProductName,r.RecipeCode,r.RecipeName,pmr.FlagDelete,pmr.CreateBy,pmr.CreateDate,pmr.UpdateBy,pmr.UpdateDate from product p " +
                    " inner join productmaprecipe pmr on pmr.productcode = p.ProductCode " +
                    " inner join recipe r on r.recipecode = pmr.recipecode " +
         

                                " where pmr.FlagDelete ='N' " + strcond;

             //   strsql += " ORDER BY pmr.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ProductMapRecipeReturnInfo()
                             {
                                 ProductMapRecipeId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 RecipeCode = dr["RecipeCode"].ToString().Trim(),
                                 RecipeName = dr["RecipeName"].ToString().Trim(),
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int? CountProductMapRecipeListByCriteria(ProductMapRecipeInfo cInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((cInfo.ProductMapRecipeId != null) && (cInfo.ProductMapRecipeId != 0))
            {
                strcond += " and  c.Id =" + cInfo.ProductMapRecipeId;
            }

            if ((cInfo.RecipeCode != null) && (cInfo.RecipeCode != ""))
            {
                strcond += " and  c.RecipeCode like '%" + cInfo.RecipeCode + "%'";
            }
            if ((cInfo.ProductCode != null) && (cInfo.ProductCode != ""))
            {
                strcond += " and  pmr.ProductCode like '" + cInfo.ProductCode + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<ProductMapRecipeReturnInfo>();


            try
            {
                string strsql = "select count(pmr.Id) as countProductMapRecipe from " + dbName + ".dbo.product p  " +
                            " inner join productmaprecipe pmr on pmr.productcode = p.ProductCode " +
                            " inner join recipe r on r.recipecode = pmr.recipecode " +
                         " where pmr.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ProductMapRecipeReturnInfo()
                             {
                                 countProductMapRecipe = Convert.ToInt32(dr["countProductMapRecipe"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countProductMapRecipe;
            }

            return count;
        }
        public List<RecipeListReturn> ListRecipeProductDetailNoPagingByCriteria(ProductMapRecipeInfo cInfo)
        {
            string strcond = "";

            

            if ((cInfo.ProductCode != null) && (cInfo.ProductCode != ""))
            {
                strcond += " and  pmr.ProductCode = '" + cInfo.ProductCode + "'";
            }
        

            DataTable dt = new DataTable();
            var LCampaign = new List<RecipeListReturn>();

            try
            {
                string strsql = " select c.*,pmr.recipecode as RecipeCodeCheck from " + dbName + ".dbo.Recipe c " +
                    " left OUTER  JOIN productmaprecipe pmr on c.recipecode = pmr.recipecode  " + strcond +

                                " where c.FlagDelete ='N' ";

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new RecipeListReturn()
                             {
                                 RecipeId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 RecipeCode = dr["RecipeCode"].ToString().Trim(),
                                 RecipeName = dr["RecipeName"].ToString().Trim(),
                                 RecipeCodeCheck = dr["RecipeCodeCheck"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }
        public int UpdateProductMapRecipe(ProductMapRecipeInfo cInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.ProductMapRecipe set " +
                           " RecipeCode = '" + cInfo.RecipeCode + "'," +
                           " ProductCode = '" + cInfo.ProductCode + "'," +


                          " FlagDelete = '" + cInfo.FlagDelete + "'," +
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +

                          " where Id =" + cInfo.ProductMapRecipeId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteProductMapRecipe(ProductMapRecipeInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.ProductMapRecipe set FlagDelete = 'Y' where Id in (" + cInfo.ProductMapRecipeIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertProductMapRecipe(ProductMapRecipeInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO ProductMapRecipe  (RecipeCode,ProductCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.RecipeCode + "'," +
                           "'" + cInfo.ProductCode + "'," +

                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'"+ cInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteClearProductMapRecipe(ProductMapRecipeInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.ProductMapRecipe set FlagDelete = 'Y' where ProductCode ='" + cInfo.ProductCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }

}

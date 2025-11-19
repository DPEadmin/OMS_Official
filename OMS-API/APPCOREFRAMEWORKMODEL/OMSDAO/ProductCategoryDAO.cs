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
    public class ProductCategoryDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<ProductCategoryListReturn> ListProductCategoryNopagingByCriteria(ProductCategoryInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.ProductCategoryCode != null) && (cInfo.ProductCategoryCode != ""))
            {
                strcond += " and  c.ProductCategoryCode like '%" + cInfo.ProductCategoryCode + "%'";
            }


            if ((cInfo.ProductCategoryName != null) && (cInfo.ProductCategoryName != ""))
            {
                strcond += " and  c.ProductCategoryName = '" + cInfo.ProductCategoryName + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<ProductCategoryListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.ProductCategory c " +
                                
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ProductCategoryListReturn()
                             {
                                 ProductCategoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                 ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }
        public List<ProductCategoryListReturn> ListProductCategoryLazadaNopagingByCriteria(ProductCategoryInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.ProductCategoryCode != null) && (cInfo.ProductCategoryCode != ""))
            {
                strcond += " and  c.ProductCategoryCode like '%" + cInfo.ProductCategoryCode + "%'";
            }


            if ((cInfo.ProductCategoryName != null) && (cInfo.ProductCategoryName != ""))
            {
                strcond += " and  c.ProductCategoryName = '" + cInfo.ProductCategoryName + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<ProductCategoryListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.LazadaTier2Catagory c " +
                                
                                " where c.Leaf = '1' " + strcond;

                strsql += " ORDER BY c.LazadaCatagoryId ASC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ProductCategoryListReturn()
                             {
                                 ProductCategoryId = (dr["LazadaCatagoryId"].ToString() != "") ? Convert.ToInt32(dr["LazadaCatagoryId"]) : 0,
                                 ProductCategoryName = dr["CatagoryName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }



        public List<ProductCategoryListReturn> ListProductCategory(string cInfo)
        {
            string strcond = "";


            if ((cInfo != null) && (cInfo != ""))
            {
                strcond += " and  c.ProductCategoryCode like '%" + cInfo + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<ProductCategoryListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.ProductCategory c " +
                                
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new ProductCategoryListReturn()
                             {
                                 ProductCategoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                 ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }



        public List<ProductListReturn> ListProduct(string cInfo)
        {
            string strcond = "";


            if ((cInfo != null) && (cInfo != ""))
            {
                strcond += " and  ProductCategoryCode = '" + cInfo + "'";
            }

            DataTable dt = new DataTable();
            var Lproduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.Product " +
                                " where FlagDelete ='N' " + strcond;

                strsql += " ORDER BY Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                Lproduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                ProductDesc = dr["ProductDesc"].ToString().Trim(),
                                ProductImageUrl = dr["ProductImageUrl"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                Unit = dr["Unit"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString().Trim()
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Lproduct;
        }



        public List<ProductImageListReturn> ListProductImage(string cInfo)
        {
            string strcond = "";


            if ((cInfo != null) && (cInfo != ""))
            {
                strcond += " and  ProductCode = '" + cInfo + "'";
            }

            DataTable dt = new DataTable();
            var Lproduct = new List<ProductImageListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.ProductImage " +
                                " where FlagDelete ='N' " + strcond;

                strsql += " ORDER BY Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                Lproduct = (from DataRow dr in dt.Rows

                            select new ProductImageListReturn()
                            {
                                ProductImageId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString(),
                                ProductImageName = dr["ProductImageName"].ToString(),
                                ProductImageUrl = dr["ProductImageUrl"].ToString().Trim(),
                                FlagDelete = dr["FlagDelete"].ToString().Trim()
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Lproduct;
        }

    }
}

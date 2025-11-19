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
using System.IO;

namespace APPCOREMODEL.OMSDAO
{
    
    public class ProductDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public List<ProductListReturn> ListComplementaryPromotionDetailByCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionDetailId != null) && (pInfo.PromotionDetailId != 0))
            {
                strcond += " and  c.PromotionDetailInfoId = " + pInfo.PromotionDetailId;
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select c.* ,p.Unit, p.ProductCode,p.ProductName from " + dbName + ".dbo.Complementary c " +

                                " left join Product p on c.ProductCode =  p.ProductCode" +
                                " left join PromotionDetailInfo t on t.Id =  c.PromotionDetailInfoId " +
                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                PromotionDetailId = (dr["PromotionDetailInfoId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailInfoId"]) : 0,
                                ComplementaryAmount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                Unit = dr["Unit"].ToString(),
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

            return LProduct;
        }
        public List<ProductListReturn> ListProductNopagingByCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  t.PromotionCode = '" + pInfo.PromotionCode + "'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select d.Id As PromotionDetailId,d.Price as PromotionDetailPrice,d.DiscountAmount,d.DiscountPercent,d.ComplementaryAmount," +
                                  "d.LockAmountFlag,d.DefaultAmount,t.PromotionCode,t.PromotionName,p.*,pdi.ProductImageUrl as ProductIMG ,p.MerchantCode from " + dbName + ".dbo.Product p " +

                                " left join PromotionDetailInfo d on d.ProductCode =  p.ProductCode " +
                                " left join Promotion t on t.PromotionCode =  d.PromotionCode " +
                                " left join ProductImage AS pdi on pdi.ProductCode = p.ProductCode" +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                PromotionName = dr["PromotionName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                PromotionDetailPrice = (dr["PromotionDetailPrice"].ToString() != "") ? Convert.ToDouble(dr["PromotionDetailPrice"]) : 0,
                                DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                ProductImageUrl = dr["ProductIMG"].ToString().Trim(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public List<ProductListReturn> ProductList(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.Product p " +
                                " where p.FlagDelete ='N' " + strcond;
                              
                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToDouble(dr["TransportPrice"]) : 0,
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public List<ProductListReturn> ListTopReservedProductStockByCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select p.*,s.QTY,s.Reserved,s.Balance,s.Intransit from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY s.Reserved DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                Intransit = (dr["Intransit"].ToString() != "") ? Convert.ToInt32(dr["Intransit"]) : 0,
                                Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
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

            return LProduct;
        }
        public List<ProductListReturn> ListTopReservedProductInventoryDetailByCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " Select top(5) i.ProductCode, SUM(i.Reserved) AS All_Reserved, i.FlagDelete from " + dbName + ".dbo.InventoryDetail AS i " +
                                " WHERE        (i.FlagDelete = 'N') GROUP BY i.ProductCode, i.FlagDelete " +
                                strcond;

                strsql += " ORDER BY SUM(i.Reserved)  DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                
                                Reserved = (dr["All_Reserved"].ToString() != "") ? Convert.ToInt32(dr["All_Reserved"]) : 0,
                                
                                FlagDelete = dr["FlagDelete"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public List<ProductListReturn> ListProductMasterNopagingByCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }
            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != ""))
            {
                strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select pb.ProductBrandName,p.*,s.QTY,s.Reserved,s.Balance, cp.lookupvalue AS propointname,s.Intransit, m.MerchantName,Cast(p.exchangeamount AS VARCHAR(10) ) +':'+ Cast(p.exchangepoint AS VARCHAR(10) ) AS exchangerate, l.LogisticName, pg.ProductCategoryName, u.LookUpValue as UnitName, sp.SupplierName, cn.ChannelName from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " left join Merchant m on m.MerchantCode = p.MerchantCode and  m.FlagDelete ='N'" +
                                " left join Supplier sp on sp.SupplierCode = p.SupplierCode and  sp.FlagDelete ='N'" +
                                " left join ProductBrand pb on pb.ProductBrandCode = p.ProductBrandCode and  pb.FlagDelete ='N'" +
                                " left join Logistic l on l.LogisticCode = p.TransportationTypeCode and  l.FlagDelete ='N'" +
                                " left join ProductCategory pg on pg.ProductCategoryCode = p.ProductCategoryCode and pg.FlagDelete ='N'" +
                                " left join LookUp u on u.LookUpCode = p.Unit and  u.LookUpType ='UNIT'" +
                                " left join LookUp cp on cp.LookUpCode = p.ProPoint and  cp.LookUpType ='PROPOINT'" +
                                " left join Channel cn on cn.ChannelCode = p.ChannelCode " +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                ProductBrandCode = dr["ProductBrandCode"].ToString().Trim(),
                                ProductBrandName = dr["ProductBrandName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                SupplierName = dr["SupplierName"].ToString().Trim(),
                                TransportationTypeCode = dr["TransportationTypeCode"].ToString().Trim(),
                                TransportationTypeName = dr["LogisticName"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                Description = dr["ProductDesc"].ToString().Trim(),
                                QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                Intransit = (dr["Intransit"].ToString() != "") ? Convert.ToInt32(dr["Intransit"]) : 0,
                                Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                ProductWidth = (dr["ProductWidth"].ToString() != "") ? Convert.ToDouble(dr["ProductWidth"]) : 0,
                                ProductHeigth = (dr["ProductHeigth"].ToString() != "") ? Convert.ToDouble(dr["ProductHeigth"]) : 0,
                                ProductLength = (dr["ProductLength"].ToString() != "") ? Convert.ToDouble(dr["ProductLength"]) : 0,
                                PackageWidth = (dr["PackageWidth"].ToString() != "") ? Convert.ToDouble(dr["PackageWidth"]) : 0,
                                PackageHeigth = (dr["PackageHeigth"].ToString() != "") ? Convert.ToDouble(dr["PackageHeigth"]) : 0,
                                PackageLength = (dr["PackageLength"].ToString() != "") ? Convert.ToDouble(dr["PackageLength"]) : 0,
                                Weight = (dr["Weight"].ToString() != "") ? Convert.ToDouble(dr["Weight"]) : 0,
                                ChannelCode = dr["ChannelCode"].ToString(),
                                ChannelName = dr["ChannelName"].ToString(),
                                ExchangeRate = dr["exchangerate"].ToString(),
                                PropointName = dr["propointname"].ToString(),
                                Sku = dr["Sku"].ToString(),
                                UpsellScript = dr["UpsellScript"].ToString(),
                                Lazada_ItemId = dr["Lazada_ItemId"].ToString(),
                                Lazada_skuId = dr["Lazada_skuId"].ToString(),
                                Lazada_status = (dr["Lazada_status"].ToString() != "") ? Convert.ToInt32(dr["Lazada_status"]) : 0,
                                LazadaCategoryCode = dr["LazadaCategoryCode"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public int? CountProductListModalByCriteria(ProductInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }
            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName.Trim() + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != "") && (pInfo.MerchantCode != "-99"))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            if ((pInfo.MerchantName != null) && (pInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + pInfo.MerchantName + "%'";
            }
            if ((pInfo.SupplierCode != null) && (pInfo.SupplierCode != "") && (pInfo.SupplierCode != "-99"))
            {
                strcond += " and  p.SupplierCode like '%" + pInfo.SupplierCode + "%'";
            }
            if ((pInfo.SupplierName != null) && (pInfo.SupplierName != ""))
            {
                strcond += " and  sp.SupplierName like '%" + pInfo.SupplierName + "%'";
            }
            if ((pInfo.TransportationTypeCode != null) && (pInfo.TransportationTypeCode != ""))
            {
                strcond += " and  p.TransportationTypeCode like '%" + pInfo.TransportationTypeCode + "%'";
            }
            if ((pInfo.TransportationTypeName != null) && (pInfo.TransportationTypeName != ""))
            {
                strcond += " and  p.TransportationTypeName like '%" + pInfo.TransportationTypeName + "%'";
            }
            if ((pInfo.ProductCategoryCode != null) && (pInfo.ProductCategoryCode != "") && (pInfo.ProductCategoryCode != "-99"))
            {
                strcond += " and  pg.ProductCategoryCode like '%" + pInfo.ProductCategoryCode + "%'";
            }
            if ((pInfo.ProductCategoryName != null) && (pInfo.ProductCategoryName != ""))
            {
                strcond += " and  pg.ProductCategoryName like '%" + pInfo.ProductCategoryName + "%'";
            }
            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != ""))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.ProductBrandName != null) && (pInfo.ProductBrandName != ""))
            {
                strcond += " and  pb.ProductBrandName like '%" + pInfo.ProductBrandName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select count(p.Id) as countProduct from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " left join Merchant m on m.MerchantCode = p.MerchantCode and  m.FlagDelete ='N'" +
                                " left join lookup u on u.LookupCode = p.Unit and u.LookupType = 'UNIT' " +
                                " left join Supplier AS sp ON sp.SupplierCode = p.SupplierCode AND sp.FlagDelete = 'N' " +
                                " left join Logistic l on l.LogisticCode = p.TransportationTypeCode and  l.FlagDelete ='N'" +
                                " left join ProductBrand pb on pb.ProductBrandCode = p.ProductBrandCode and  pb.FlagDelete ='N'" +
                                " left join Channel cn on cn.ChannelCode = p.ChannelCode " +
                                " LEFT OUTER JOIN ProductCategory AS pg ON pg.ProductCategoryCode = p.ProductCategoryCode " +
                                " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                countProduct = Convert.ToInt32(dr["countProduct"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProduct;
            }

            return count;
        }
            public List<ProductListReturn> ListProductMasterByCriteria(ProductInfo pInfo)
            {
                string strcond = "";
                string stroffset = "";
                string strinv_join = "";
                string strsel = "";
                string strNotin = "";

                if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
                {
                    strcond += " and  p.Id =" + pInfo.ProductId;
                }
                if ((pInfo.FlagPointCoupon != null) && (pInfo.FlagPointCoupon != ""))
                {
                    strcond += " and p.ProPoint is not null ";
                }
                else
                {
                    strcond += " and p.ProPoint = '' ";
                }
                if ((pInfo.Sku != null) && (pInfo.Sku != "") )
                {
                    strcond += " and p.Sku = '" + pInfo.Sku + "'";
                }
                if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
                {
                    strcond += " and p.ProPoint = '" + pInfo.Propoint + "'";
                }
                if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
                {
                    strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
                }
                if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
                {
                    strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";

                }
                if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
                {
                    strcond += " and  p.ProductName like '%" + pInfo.ProductName.Trim() + "%'";
                }
                if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != "") && (pInfo.MerchantCode != "-99"))
                {
                    strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
                }
                if ((pInfo.MerchantName != null) && (pInfo.MerchantName != ""))
                {
                    strcond += " and  m.MerchantName like '%" + pInfo.MerchantName + "%'";
                }
                if ((pInfo.SupplierCode != null) && (pInfo.SupplierCode != "") && (pInfo.SupplierCode != "-99"))
                {
                    strcond += " and  p.SupplierCode like '%" + pInfo.SupplierCode + "%'";
                }
                if ((pInfo.SupplierName != null) && (pInfo.SupplierName != ""))
                {
                    strcond += " and  sp.SupplierName like '%" + pInfo.SupplierName + "%'";
                }
                if ((pInfo.TransportationTypeCode != null) && (pInfo.TransportationTypeCode != ""))
                {
                    strcond += " and  p.TransportationTypeCode like '%" + pInfo.TransportationTypeCode + "%'";
                }
                if ((pInfo.TransportationTypeName != null) && (pInfo.TransportationTypeName != ""))
                {
                    strcond += " and  p.TransportationTypeName like '%" + pInfo.TransportationTypeName + "%'";
                }
                if ((pInfo.ProductCategoryCode != null) && (pInfo.ProductCategoryCode != "") && (pInfo.ProductCategoryCode != "-99"))
                {
                    strcond += " and  pg.ProductCategoryCode like '%" + pInfo.ProductCategoryCode + "%'";
                }
                if ((pInfo.ProductCategoryName != null) && (pInfo.ProductCategoryName != ""))
                {
                    strcond += " and  pg.ProductCategoryName like '%" + pInfo.ProductCategoryName + "%'";
                }
                if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
                {
                    strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
                }
                if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != "") && (pInfo.ProductBrandCode != "-99"))
                {
                    strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
                }
                if ((pInfo.ProductBrandName != null) && (pInfo.ProductBrandName != ""))
                {
                    strcond += " and  pb.ProductBrandName like '%" + pInfo.ProductBrandName + "%'";
                }
                if ((pInfo.Lazada_ItemId != null) && (pInfo.Lazada_ItemId != ""))
                {
                    strcond += " and  p.Lazada_status = 1";
                
                }
                else
                {
                    strNotin = " AND(c.PromotionCode = '" + pInfo.ProductNotInPromotionCode + "')";
                }
                if ((pInfo.InventoryCode != null) && (pInfo.InventoryCode != ""))
                {
                    strinv_join += " left join InventoryDetail as id on p.ProductCode = id.ProductCode and id.InventoryCode = '" + pInfo.InventoryCode + "' ";
                    strsel = ",id.QTY";
                }
                else
                {
                    strsel = ",s.QTY";
                }
                if ((pInfo.rowOFFSet != null) && (pInfo.rowFetch != 0) && (pInfo.rowOFFSet != null) && (pInfo.rowFetch != 0))
                {
                    stroffset += " OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY ";
                }
    
                //product not in promotionDetail
                if ((pInfo.ProductNotInPromotionCode != null) && (pInfo.ProductNotInPromotionCode != ""))
                {
                    strcond += "and (p.ProductCode not in(SELECT c.ProductCode"+
                               " FROM  PromotionDetailInfo AS c LEFT OUTER JOIN " +
                               "  Product AS pro ON pro.ProductCode = c.ProductCode LEFT OUTER JOIN " +
                               "  Lookup AS u ON u.LookupCode = pro.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                               "  Merchant AS mech ON mech.MerchantCode = pro.MerchantCode LEFT OUTER JOIN " +
                               "  Promotion AS promo ON promo.PromotionCode = c.PromotionCode AND promo.FlagDelete = 'N' AND c.FlagDelete = 'N' LEFT OUTER JOIN " +
                               "  ProductCategory AS procat ON pro.ProductCategoryCode = procat.ProductCategoryCode LEFT OUTER JOIN " +
                               "  ProductBrand AS pb ON pb.ProductBrandCode = pro.ProductBrandCode LEFT OUTER JOIN " +
                               "  Channel AS cn ON cn.ChannelCode = pro.ChannelCode " +
                               "  WHERE(c.FlagDelete = 'N')" + strNotin +
                               " ))";
                }

                //product not in complementary
                if ((pInfo.ProductCodeNotInComplementary != null) && (pInfo.ProductNotInPromotionCode != ""))
                {
                    strcond += " AND (p.ProductCode NOT IN (SELECT com.ProductCode FROM Complementary com " +
                               " WHERE (com.PromotionDetailInfoId = '" + pInfo.PromotionDetailInfoId + "') and (com.flagdelete = 'N')))";
                }

            //product not in productBOM
            if ((pInfo.ProductCodeNotInProductBOM != null) && (pInfo.ProductCodeNotInProductBOM != ""))
            {
                strcond += " AND (p.ProductCode NOT IN (SELECT b.ProductBOM FROM ProductBOM b " +
                           " WHERE (b.ProductCode = '" + pInfo.ProductCodeNotInProductBOM + "') and (b.flagdelete = 'N'))) " +
                           " AND (p.ProductCode NOT IN ('" + pInfo.ProductCodeNotInProductBOM + "')) ";
            }

            DataTable dt = new DataTable();
                var LProduct = new List<ProductListReturn>();

                try
                {
                    string strsql = " select p.*,s.Reserved,s.Balance,s.Intransit,CAST(p.ExchangeAmount AS VARCHAR(10) ) +':'+ CAST(p.ExchangePoint AS VARCHAR(10) ) as ExchangeRate , m.MerchantName,sp.SupplierName,c.CompanyNameTH, l.LogisticName, u.LookUpValue as UnitName, cp.LookUpValue as ProPointName,ls.LookupValue as Lazada_status_Name, cn.ChannelName, pb.ProductBrandName, pg.ProductCategoryName" + strsel+
                                    " from " + dbName + ".dbo.Product p " +
                                    " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                    " left join Merchant m on m.MerchantCode = p.MerchantCode and  m.FlagDelete ='N'" +
                                    " left join lookup u on u.LookupCode = p.Unit and u.LookupType = 'UNIT' " +
                                    " left join lookup cp on cp.LookupCode = p.ProPoint and cp.LookupType = 'PROPOINT' " +
                                    " left join lookup ls on ls.LookupCode = p.Lazada_status and ls.LookupType = 'LAZSTAT' " +
                                    " left join Supplier AS sp ON sp.SupplierCode = p.SupplierCode AND sp.FlagDelete = 'N' " +
                                    " left join Logistic l on l.LogisticCode = p.TransportationTypeCode and  l.FlagDelete ='N'" +
                                    " left join ProductBrand pb on pb.ProductBrandCode = p.ProductBrandCode and  pb.FlagDelete ='N'" +
                                    " left join Channel cn on cn.ChannelCode = p.ChannelCode " +
                                    " left join Company c on c.CompanyCode = p.CompanyCode and c.MerchantMapCode = '" + pInfo.MerchantCode + "'" +
                                    " left join ProductCategory AS pg ON pg.ProductCategoryCode = p.ProductCategoryCode " + strinv_join +
                                    " where p.FlagDelete ='N' " + strcond;

                    strsql += " ORDER BY p.ProductCode DESC" + stroffset;

                    Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                    SqlCommand com = new SqlCommand();
                    com.CommandText = strsql;
                    com.CommandType = System.Data.CommandType.Text;
                    dt = db.ExcuteDataReaderText(com);
                    LProduct = (from DataRow dr in dt.Rows

                                select new ProductListReturn()
                                {
                                    ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                    ProductCode = dr["ProductCode"].ToString().Trim(),
                                    ProductName = dr["ProductName"].ToString().Trim(),
                                    ProductBrandCode = dr["ProductBrandCode"].ToString().Trim(),
                                    ProductBrandName = dr["ProductBrandName"].ToString().Trim(),
                                    MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                    MerchantName = dr["MerchantName"].ToString().Trim(),
                                    SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                    SupplierName = dr["SupplierName"].ToString().Trim(),
                                    CompanyCode = dr["CompanyCode"].ToString().Trim(),
                                    CompanyNameTH = dr["CompanyNameTH"].ToString().Trim(),
                                    TransportationTypeCode = dr["TransportationTypeCode"].ToString().Trim(),
                                    TransportationTypeName = dr["LogisticName"].ToString().Trim(),
                                    ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                    ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                    Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                    CarType = dr["CarType"].ToString().Trim(),
                                    MaintainType = dr["MaintainType"].ToString().Trim(),
                                    InsureCost = (dr["InsureCost"].ToString() != "") ? Convert.ToDouble(dr["InsureCost"]) : 0,
                                    FirstDamages = (dr["FirstDamages"].ToString() != "") ? Convert.ToDouble(dr["FirstDamages"]) : 0,
                                    GarageQuan = (dr["GarageQuan"].ToString() != "") ? Convert.ToInt32(dr["GarageQuan"]) : 0,
                                    Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                    Unit = dr["Unit"].ToString().Trim(),
                                    Description = dr["ProductDesc"].ToString().Trim(),
                                    QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                    Intransit = (dr["Intransit"].ToString() != "") ? Convert.ToInt32(dr["Intransit"]) : 0,
                                    Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                    Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                    CreateBy = dr["CreateBy"].ToString(),
                                    CreateDate = dr["CreateDate"].ToString(),
                                    UpdateBy = dr["UpdateBy"].ToString(),
                                    UpdateDate = dr["UpdateDate"].ToString(),
                                    FlagDelete = dr["FlagDelete"].ToString(),
                                    ProductWidth = (dr["ProductWidth"].ToString() != "") ? Convert.ToDouble(dr["ProductWidth"]) : 0,
                                    ProductHeigth = (dr["ProductHeigth"].ToString() != "") ? Convert.ToDouble(dr["ProductHeigth"]) : 0,
                                    ProductLength = (dr["ProductLength"].ToString() != "") ? Convert.ToDouble(dr["ProductLength"]) : 0,
                                    PackageWidth = (dr["PackageWidth"].ToString() != "") ? Convert.ToDouble(dr["PackageWidth"]) : 0,
                                    PackageHeigth = (dr["PackageHeigth"].ToString() != "") ? Convert.ToDouble(dr["PackageHeigth"]) : 0,
                                    PackageLength = (dr["PackageLength"].ToString() != "") ? Convert.ToDouble(dr["PackageLength"]) : 0,
                                    Weight = (dr["Weight"].ToString() != "") ? Convert.ToDouble(dr["Weight"]) : 0,
                                    UnitName = dr["UnitName"].ToString().Trim(),
                                    ChannelCode = dr["ChannelCode"].ToString(),
                                    ChannelName = dr["ChannelName"].ToString(),
                                    Sku = dr["Sku"].ToString(),
                                    Product_img1 = dr["ProductImage1"].ToString(),
                                    Showcase_image11 = dr["Showcase_image11"].ToString(),
                                    Showcase_image43 = dr["Showcase_image43"].ToString(),
                                    SKU_img1 = dr["SKUImage1"].ToString(),
                                    URLvideo = dr["URLVideo"].ToString(),
                                    ProdutAdditional = dr["Additional"].ToString(),
                                    WarrantyCondition = dr["WarrantyCondition"].ToString(),
                                    WarrantyType = dr["WarrantyType"].ToString(),
                                    WarrantyStartdate = dr["WarrantyStartdate"].ToString(),
                                    WarrantyEnddate = dr["WarrantyEnddate"].ToString(),
                                    PackageDanger = dr["FlagDanger"].ToString(),
                                    ExchangeAmount = (dr["ExchangeAmount"].ToString() != "") ? Convert.ToInt32(dr["ExchangeAmount"]) : 0,
                                    ExchangePoint = (dr["ExchangePoint"].ToString() != "") ? Convert.ToInt32(dr["ExchangePoint"]) : 0,
                                    Propoint = dr["ProPoint"].ToString(),
                                    ExchangeRate = dr["ExchangeRate"].ToString(),
                                    PropointName = dr["ProPointName"].ToString(),
                                    EcomSpec = dr["EcomSpec"].ToString(),
                                    UpsellScript = dr["UpsellScript"].ToString(),
                                    Lazada_ItemId = dr["Lazada_ItemId"].ToString(),
                                    Lazada_skuId = dr["Lazada_skuId"].ToString(),
                                    Lazada_status_Name = (dr["Lazada_status_Name"].ToString() == "") ? "ยังไม่ถูกสร้าง" : dr["Lazada_status_Name"].ToString(),
                                    Lazada_status = (dr["Lazada_status"].ToString() != "") ? Convert.ToInt32(dr["Lazada_status"]) : 0,
                                    LazadaCategoryCode = dr["LazadaCategoryCode"].ToString(),
                                }).ToList();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                return LProduct;
            }

        public List<ProductListReturn> ProductCheck(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select p.*,s.QTY,s.Reserved,s.Balance,s.Intransit from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }

        public int? CountProductMasterListByCriteria(ProductInfo pInfo)
        {
            string strcond = "";
            int? count = 0;
            string strinv_join = "";


            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }
            if ((pInfo.FlagPointCoupon != null) && (pInfo.FlagPointCoupon != ""))
            {
                strcond += " and p.ProPoint is not null ";
            }
            else
            {
                strcond += " and p.ProPoint = '' ";
            }
            if ((pInfo.Sku != null) && (pInfo.Sku != ""))
            {
                strcond += " and p.Sku = '" + pInfo.Sku + "'";
            }
            if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
            {
                strcond += " and p.ProPoint = '" + pInfo.Propoint + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != "") && (pInfo.MerchantCode != "-99"))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }
            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != ""))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.InventoryCode != null) && (pInfo.InventoryCode != ""))
            {
                strinv_join += " left join InventoryDetail as id on p.ProductCode = id.ProductCode and id.InventoryCode = '" + pInfo.InventoryCode + "' ";
            }
            if ((pInfo.ProductBrandName != null) && (pInfo.ProductBrandName != ""))
            {
                strcond += " and  pb.ProductBrandName like '%" + pInfo.ProductBrandName + "%'";
            }
            if ((pInfo.Lazada_ItemId != null) && (pInfo.Lazada_ItemId != ""))
            {
                strcond += " and  p.Lazada_status = 1";
            }

            //product not in promotionDetail
            if ((pInfo.ProductNotInPromotionCode != null) && (pInfo.ProductNotInPromotionCode != ""))
            {
                strcond += "and (p.ProductCode not in(SELECT c.ProductCode" +
                           " FROM  PromotionDetailInfo AS c LEFT OUTER JOIN " +
                           "  Product AS pro ON pro.ProductCode = c.ProductCode LEFT OUTER JOIN " +
                           "  Lookup AS u ON u.LookupCode = pro.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                            " lookup cp on cp.LookupCode = pro.ProPoint and cp.LookupType = 'PROPOINT' LEFT OUTER JOIN " +
                           "  Merchant AS mech ON mech.MerchantCode = pro.MerchantCode LEFT OUTER JOIN " +
                           "  Promotion AS promo ON promo.PromotionCode = c.PromotionCode AND promo.FlagDelete = 'N' AND c.FlagDelete = 'N' LEFT OUTER JOIN " +
                           "  ProductCategory AS procat ON pro.ProductCategoryCode = procat.ProductCategoryCode LEFT OUTER JOIN " +
                           "  ProductBrand AS pb ON pb.ProductBrandCode = pro.ProductBrandCode LEFT OUTER JOIN " +
                           "  Channel AS cn ON cn.ChannelCode = pro.ChannelCode " + strinv_join +
                           "  WHERE(c.FlagDelete = 'N') AND(c.PromotionCode = '" + pInfo.ProductNotInPromotionCode + "')))";
            }

            //product not in complementary
            if ((pInfo.ProductCodeNotInComplementary != null) && (pInfo.ProductNotInPromotionCode != ""))
            {
                strcond += "AND (p.ProductCode NOT IN (SELECT com.ProductCode FROM Complementary com " +
                           " WHERE (com.PromotionDetailInfoId = '" + pInfo.PromotionDetailInfoId + "') and (com.flagdelete = 'N')))";
            }

            //product not in productBOM
            if ((pInfo.ProductCodeNotInProductBOM != null) && (pInfo.ProductCodeNotInProductBOM != ""))
            {
                strcond += " AND (p.ProductCode NOT IN (SELECT b.ProductBOM FROM ProductBOM b " +
                           " WHERE (b.ProductCode = '" + pInfo.ProductCodeNotInProductBOM + "') and (b.flagdelete = 'N'))) " +
                           " AND (p.ProductCode NOT IN ('" + pInfo.ProductCodeNotInProductBOM + "')) ";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select count(p.Id) as countProduct from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " left join Merchant m on m.MerchantCode = p.MerchantCode and  m.FlagDelete ='N'" +
                                " left join lookup u on u.LookupCode = p.Unit and u.LookupType = 'UNIT' " +
                                " left join Supplier AS sp ON sp.SupplierCode = p.SupplierCode AND sp.FlagDelete = 'N' " +
                                " left join Logistic l on l.LogisticCode = p.TransportationTypeCode and  l.FlagDelete ='N'" +
                                " left join ProductCategory pg on pg.ProductCategoryCode = p.ProductCategoryCode and  l.FlagDelete ='N'" +
                                " left join ProductBrand pb on pb.ProductBrandCode = p.ProductBrandCode and  pb.FlagDelete ='N'" +
                                " left join Channel cn on cn.ChannelCode = p.ChannelCode " +
                                " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                countProduct = Convert.ToInt32(dr["countProduct"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProduct;
            }

            return count;

        }
        public int? CountProduct(ProductInfo pInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = "select count(p.Id) as countProduct from " + dbName + ".dbo.Product p " +
                                 " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                countProduct = Convert.ToInt32(dr["countProduct"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProduct;
            }

            return count;
        }


        public int? CountProductRunningNumber(ProductInfo pInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = "SELECT max(p.id) as countProduct from " + dbName + ".dbo.Product p " +
                                 " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                countProduct = Convert.ToInt32(dr["countProduct"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProduct;
            }

            return count;
        }

        public List<ProductListReturn> ListProductByCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  d.PromotionCode = '" + pInfo.PromotionCode + "'";
            }

            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode = '" + pInfo.CampaignCode + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select u.LookupValue as UnitName,d.PromotionDetailName, d.Id As PromotionDetailId,d.Price as PromotionDetailPrice,d.DiscountAmount,d.DiscountPercent,d.ComplementaryAmount,cp.CampaignCode,cp.PromotionCode, " +
                                " d.LockAmountFlag,d.LockCheckbox,d.DefaultAmount,t.PromotionName,pc.ProductCategoryName,m.MerchantName,p.* from " + dbName + ".dbo.Product p " +
                                 " left join Lookup u on u.LookupCode  =  p.Unit and u.LookupType = 'UNIT'" +

                                " left join PromotionDetailInfo d on d.ProductCode =  p.ProductCode" +
                                " left join Promotion t on t.PromotionCode =  d.PromotionCode " +
                                " LEFT OUTER JOIN CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode" +
                                " left join ProductCategory pc on pc.ProductCategoryCode =  p.ProductCategoryCode" +
                                " left join Merchant m on m.MerchantCode =  p.MerchantCode and m.FlagDelete = 'N'" +
                                " where p.FlagDelete ='N' and  d.FlagDelete ='N' and t.FlagDelete = 'N'" + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                PromotionName = dr["PromotionName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                PromotionDetailPrice = (dr["PromotionDetailPrice"].ToString() != "") ? Convert.ToDouble(dr["PromotionDetailPrice"]) : 0,
                                PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                AllergyRemark = dr["AllergyRemark"].ToString().Trim(),
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

            return LProduct;
        }

        public List<ProductListReturn> ListProductMainByRecipeNameCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  d.PromotionCode = '" + pInfo.PromotionCode + "'";
            }

            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode = '" + pInfo.CampaignCode + "'";
            }

            if ((pInfo.RecipeName != null) && (pInfo.RecipeName != ""))
            {
                strcond += " and  r.RecipeName like '%" + pInfo.RecipeName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select u.LookupValue as UnitName,d.PromotionDetailName, d.Id As PromotionDetailId,d.Price as PromotionDetailPrice,d.DiscountAmount,d.DiscountPercent,d.ComplementaryAmount,cp.CampaignCode,cp.PromotionCode, " +
                                " d.LockAmountFlag,d.LockCheckbox,d.DefaultAmount,t.PromotionName,pc.ProductCategoryName,m.MerchantName,pm.RecipeCode,r.RecipeName,p.* from " + dbName + ".dbo.Product p " +
                                " left join Lookup u on u.LookupCode  =  p.Unit and u.LookupType = 'UNIT'" +
                                " left join PromotionDetailInfo d on d.ProductCode =  p.ProductCode" +
                                " left join Promotion t on t.PromotionCode =  d.PromotionCode " +
                                " LEFT OUTER JOIN CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode" +
                                " left join ProductCategory pc on pc.ProductCategoryCode =  p.ProductCategoryCode" +
                                " left join Merchant m on m.MerchantCode =  p.MerchantCode and m.FlagDelete = 'N'" +
                                " left join SubMainPromotionDetailInfo AS sm ON sm.PromotionDetailId = d.Id " +
                                " left join ProductMapRecipe AS pm ON pm.ProductCode = sm.ProductCode " +
                                " left join Recipe AS r ON r.RecipeCode = pm.RecipeCode " +
                                " where p.FlagDelete ='N' and  d.FlagDelete ='N' and t.FlagDelete = 'N' and pm.FlagDelete = 'N' and r.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                PromotionName = dr["PromotionName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                PromotionDetailPrice = (dr["PromotionDetailPrice"].ToString() != "") ? Convert.ToDouble(dr["PromotionDetailPrice"]) : 0,
                                PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                CampaignCode = dr["CampaignCode"].ToString().Trim(),
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

            return LProduct;
        }

        public List<ProductListReturn> ListProductExchangeByRecipeNameCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  d.PromotionCode = '" + pInfo.PromotionCode + "'";
            }

            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode = '" + pInfo.CampaignCode + "'";
            }

            if ((pInfo.RecipeName != null) && (pInfo.RecipeName != ""))
            {
                strcond += " and  r.RecipeName like '%" + pInfo.RecipeName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select u.LookupValue as UnitName,d.PromotionDetailName, d.Id As PromotionDetailId,d.Price as PromotionDetailPrice,d.DiscountAmount,d.DiscountPercent,d.ComplementaryAmount,cp.CampaignCode,cp.PromotionCode, " +
                                " d.LockAmountFlag,d.LockCheckbox,d.DefaultAmount,t.PromotionName,pc.ProductCategoryName,m.MerchantName,pm.RecipeCode,r.RecipeName,p.* from " + dbName + ".dbo.Product p " +
                                " left join Lookup u on u.LookupCode  =  p.Unit and u.LookupType = 'UNIT'" +
                                " left join PromotionDetailInfo d on d.ProductCode =  p.ProductCode" +
                                " left join Promotion t on t.PromotionCode =  d.PromotionCode " +
                                " LEFT OUTER JOIN CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode" +
                                " left join ProductCategory pc on pc.ProductCategoryCode =  p.ProductCategoryCode" +
                                " left join Merchant m on m.MerchantCode =  p.MerchantCode and m.FlagDelete = 'N'" +
                                " left join SubExchangePromotionDetailInfo AS sm ON sm.PromotionDetailId = d.Id " +
                                " left join ProductMapRecipe AS pm ON pm.ProductCode = sm.ProductCode " +
                                " left join Recipe AS r ON r.RecipeCode = pm.RecipeCode " +
                                " where p.FlagDelete ='N' and  d.FlagDelete ='N' and t.FlagDelete = 'N' and pm.FlagDelete = 'N' and r.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                PromotionName = dr["PromotionName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                PromotionDetailPrice = (dr["PromotionDetailPrice"].ToString() != "") ? Convert.ToDouble(dr["PromotionDetailPrice"]) : 0,
                                PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                CampaignCode = dr["CampaignCode"].ToString().Trim(),
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

            return LProduct;
        }

        public int? CountProductListByCriteria(ProductInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }
            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName.Trim() + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != "") && (pInfo.MerchantCode != "-99"))
            {
                strcond += " and  p.MerchantCode like '%" + pInfo.MerchantCode + "%'";
            }
            if ((pInfo.MerchantName != null) && (pInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + pInfo.MerchantName + "%'";
            }
            if ((pInfo.SupplierCode != null) && (pInfo.SupplierCode != "") && (pInfo.SupplierCode != "-99"))
            {
                strcond += " and  p.SupplierCode like '%" + pInfo.SupplierCode + "%'";
            }
            if ((pInfo.SupplierName != null) && (pInfo.SupplierName != ""))
            {
                strcond += " and  sp.SupplierName like '%" + pInfo.SupplierName + "%'";
            }
            if ((pInfo.TransportationTypeCode != null) && (pInfo.TransportationTypeCode != ""))
            {
                strcond += " and  p.TransportationTypeCode like '%" + pInfo.TransportationTypeCode + "%'";
            }
            if ((pInfo.TransportationTypeName != null) && (pInfo.TransportationTypeName != ""))
            {
                strcond += " and  p.TransportationTypeName like '%" + pInfo.TransportationTypeName + "%'";
            }
            if ((pInfo.ProductCategoryCode != null) && (pInfo.ProductCategoryCode != "") && (pInfo.ProductCategoryCode != "-99"))
            {
                strcond += " and  pg.ProductCategoryCode like '%" + pInfo.ProductCategoryCode + "%'";
            }
            if ((pInfo.ProductCategoryName != null) && (pInfo.ProductCategoryName != ""))
            {
                strcond += " and  pg.ProductCategoryName like '%" + pInfo.ProductCategoryName + "%'";
            }
            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != "") && (pInfo.ChannelCode != "-99"))
            {
                strcond += " and  p.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != ""))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.ProductBrandName != null) && (pInfo.ProductBrandName != ""))
            {
                strcond += " and  pb.ProductBrandName like '%" + pInfo.ProductBrandName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = "select count(p.Id) as countProduct from " + dbName + ".dbo.Product p " +

                                " left join PromotionDetailInfo d on d.ProductCode =  p.ProductCode" +
                                " left join Promotion t on t.PromotionCode =  d.PromotionCode " +
                                " LEFT OUTER JOIN CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode " +
                                " left join ProductCategory pc on pc.ProductCategoryCode = p.ProductCategoryCode " +
                                " left join Merchant m on m.MerchantCode =  p.MerchantCode " +
                                " left join Supplier AS sp ON sp.SupplierCode = p.SupplierCode " +
                                " left join ProductCategory AS pg ON pg.ProductCategoryCode = p.ProductCategoryCode  " +
                                " where p.FlagDelete ='N' and  d.FlagDelete ='N' and t.FlagDelete = 'N'" + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                countProduct = Convert.ToInt32(dr["countProduct"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProduct;
            }

            return count;
        }
        public int? DeleteProduct(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = "Update " + dbName + ".dbo.Product set FlagDelete = 'Y' where Id in (" + pInfo.ProductId + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? DeleteProductList(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = "Update " + dbName + ".dbo.Product set FlagDelete = 'Y' where Id in (" + pInfo.ProductCode + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? InsertProduct(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO Product  (ProductCode,ProductName,MerchantCode,SupplierCode,CarType,MaintainType,InsureCost,FirstDamages,GarageQuan,Price,CompanyCode,Unit,FlagDelete,Transportprice,TransportationTypeCode," +
                             "ProductWidth,ProductLength,ProductHeigth,Weight,ProductCategoryCode,OtherChoice,PackageWidth,PackageLength," +
                             "PackageHeigth,ProductBrandCode,ProductDesc,Sku,AllergyRemark,CreateDate,CreateBy,UpdateDate,UpdateBy,ProductImage1,Showcase_image11,Showcase_image43,SKUImage1,URLVideo,Additional,WarrantyCondition,WarrantyType,WarrantyStartdate,WarrantyEnddate,FlagDanger,ExchangeAmount,ExchangePoint,FlagPointType,ProPoint,EcomSpec,LazadaCategoryCode,UpsellScript)" +
                            "VALUES (" +
                           "'" + pInfo.ProductCode + "'," +
                           "N'" + pInfo.ProductName + "'," +
                           "'" + pInfo.MerchantCode + "'," +
                           "'" + pInfo.SupplierCode + "'," +
                           "'" + pInfo.CarType + "'," +
                           "'" + pInfo.MaintainType + "'," +
                           "'" + ((pInfo.InsureCost.ToString() == "") ? 0 : pInfo.InsureCost) + "'," +
                           "'" + ((pInfo.FirstDamages.ToString() == "") ? 0 : pInfo.FirstDamages) + "'," +
                           "'" + ((pInfo.GarageQuan.ToString() == "") ? 0 : pInfo.GarageQuan) + "'," +
                           "'" +((pInfo.Price.ToString() == "") ? 0 : pInfo.Price) +"',"+
                           "'" + pInfo.CompanyCode + "'," +
                           "'" + pInfo.Unit + "'," +
                           "'" + pInfo.FlagDelete + "'," +
                           "'" + ((pInfo.TransportPrice.ToString() == "") ? 0 : pInfo.TransportPrice) + "'," +
                           "'" + pInfo.TransportationTypeCode + "'," +
                           "'" + ((pInfo.ProductWidth.ToString() == "") ? 0 : pInfo.ProductWidth) + "'," +
                           "'" + ((pInfo.ProductLength.ToString() == "") ? 0 : pInfo.ProductLength) + "'," +
                           "'" + ((pInfo.ProductHeigth.ToString() == "") ? 0 : pInfo.ProductHeigth) + "'," +
                           "'" + ((pInfo.Weight.ToString() == "") ? 0 : pInfo.Weight) + "'," +
                           "'" + pInfo.ProductCategoryCode + "'," +
                           "'" + pInfo.OtherChoice + "'," +
                           "'" + ((pInfo.PackageWidth.ToString() == "") ? 0 : pInfo.PackageWidth) + "'," +
                           "'" + ((pInfo.PackageLength.ToString() == "") ? 0 : pInfo.PackageLength) + "'," +
                           "'" + ((pInfo.PackageHeigth.ToString() == "") ? 0 : pInfo.PackageHeigth) + "'," +
                           "'" + pInfo.ProductBrandCode + "'," +
                           "'" + pInfo.Description + "'," +
                           "'" + pInfo.Sku + "'," +
                           "'" + pInfo.AllergyRemark + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.UpdateBy + "'," + 
                           "'" + pInfo.Product_img1 + "'," +
                           "'" + pInfo.Showcase_image11 + "'," +
                           "'" + pInfo.Showcase_image43 + "'," +
                           "'" + pInfo.SKU_img1 + "'," +
                           "'" + pInfo.URLvideo + "'," +
                           "'" + pInfo.ProdutAdditional + "'," +
                           "'" + pInfo.WarrantyCondition + "'," +
                           "'" + pInfo.WarrantyType + "'," +
                           "'" + pInfo.WarrantyStartdate + "'," +
                           "'" + pInfo.WarrantyEnddate + "'," +
                           "'" + pInfo.PackageDanger + "'," +
                           "'" + pInfo.ExchangeAmount + "'," +
                           "'" + pInfo.ExchangePoint + "'," +
                           "'" + pInfo.FlagPointType + "'," +
                           "'" + pInfo.Propoint + "'," +
                           "'" + pInfo.EcomSpec + "'," +
                           "'" + pInfo.LazadaCategoryCode + "'," +
                           "'" + pInfo.UpsellScript + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdateProduct(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.Product set " +
                            " ProductCode = '" + pInfo.ProductCode + "'," +
                            " ProductName = N'" + pInfo.ProductName + "'," +
                            " ProductBrandCode = '" + pInfo.ProductBrandCode + "'," +
                            " TransportationTypeCode = '" + pInfo.TransportationTypeCode + "'," +
                            " ProductCategoryCode = '" + pInfo.ProductCategoryCode + "'," +
                            " ProductWidth = '" +  ((pInfo.ProductWidth.ToString() == "") ? 0 : pInfo.ProductWidth) + "'," +
                            " ProductLength = '" + ((pInfo.ProductLength.ToString() == "") ? 0 : pInfo.ProductLength) + "'," +
                            " ProductHeigth = '" + ((pInfo.ProductHeigth.ToString() == "") ? 0 : pInfo.ProductHeigth) + "'," +
                            " PackageWidth = '" +  ((pInfo.PackageWidth.ToString() == "") ? 0 : pInfo.PackageWidth) + "'," +
                            " PackageLength = '" + ((pInfo.PackageLength.ToString() == "") ? 0 : pInfo.PackageLength) + "'," +
                            " PackageHeigth = '" + ((pInfo.PackageHeigth.ToString() == "") ? 0 : pInfo.PackageHeigth) + "'," +
                            " Weight = '" + ((pInfo.Weight.ToString() == "") ? 0 : pInfo.Weight) + "'," +
                            " Price = '" + ((pInfo.Price.ToString() == "") ? 0 : pInfo.Price) + "'," +
                            " CarType = '" + pInfo.CarType + "'," +
                            " MaintainType = '" + pInfo.MaintainType + "'," +
                            " InsureCost = '" + ((pInfo.InsureCost.ToString() == "") ? 0 : pInfo.InsureCost) + "'," +
                            " FirstDamages = '" + ((pInfo.FirstDamages.ToString() == "") ? 0 : pInfo.FirstDamages) + "'," +
                            " GarageQuan = '" + ((pInfo.GarageQuan.ToString() == "") ? 0 : pInfo.GarageQuan) + "'," +
                            " Unit = '" + pInfo.Unit + "'," +
                            " CompanyCode = '" + pInfo.CompanyCode + "'," +
                            " SupplierCode = '" + pInfo.SupplierCode + "'," +
                            " ProductDesc = '" + pInfo.Description + "'," +
                            " Sku ='" + pInfo.Sku + "'," +
                            " AllergyRemark ='" + pInfo.AllergyRemark + "'," +
                            " Transportprice ='" + ((pInfo.TransportPrice.ToString() == "") ? 0 : pInfo.TransportPrice) + "'," +
                            " ProductImage1='" + pInfo.Product_img1 + "'," +
                            " Showcase_image11='" + pInfo.Showcase_image11 + "'," +
                            " Showcase_image43='" + pInfo.Showcase_image43 + "'," +
                            " SKUImage1='" + pInfo.SKU_img1 + "'," +
                            " URLVideo='" + pInfo.URLvideo + "'," +
                            " Additional='" + pInfo.ProdutAdditional + "'," +
                            " WarrantyCondition='" + pInfo.WarrantyCondition + "'," +
                            " WarrantyType='" + pInfo.WarrantyType + "'," +
                            " WarrantyStartdate='" + pInfo.WarrantyStartdate + "'," +
                            " WarrantyEnddate='" + pInfo.WarrantyEnddate + "'," +
                            " FlagDanger='" + pInfo.PackageDanger + "'," +
                            " UpsellScript ='" + pInfo.UpsellScript + "'," +

                            " ExchangeAmount ='" + pInfo.ExchangeAmount + "'," +
                            " ExchangePoint ='" + pInfo.ExchangePoint + "'," +
                            " ProPoint ='" + pInfo.Propoint + "'," +
                            " EcomSpec ='" + pInfo.EcomSpec + "'," +
                            " LazadaCategoryCode ='" + pInfo.LazadaCategoryCode + "'," +
                            " Lazada_ItemId ='" + pInfo.Lazada_ItemId + "'," +
                            " Lazada_skuId ='" + pInfo.Lazada_skuId + "'," +
                            " Lazada_status ='" + ((pInfo.Lazada_status.ToString() == "") ? 0 : pInfo.Lazada_status) + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + pInfo.UpdateBy + "'" +

                           " where Id ='" + pInfo.ProductId + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdateLazadaProduct(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.Product set " +
                            " Lazada_ItemId ='" + pInfo.Lazada_ItemId + "'," +
                            " Lazada_skuId ='" + pInfo.Lazada_skuId + "'," +
                            " Lazada_status ='" + ((pInfo.Lazada_status.ToString() == "") ? 0 : pInfo.Lazada_status) + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + pInfo.UpdateBy + "'" +
                            " where Id ='" + pInfo.ProductId + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<ProductListReturn> ListProductShowAll(ProductInfo pInfo)
        {
          

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = "select * from product";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                ProductDesc = dr["ProductCode"].ToString(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToInt32(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString(),
                                CreateDate = dr["CreateDate"].ToString().Trim(),
                                CreateBy = dr["CreateBy"].ToString(),
                                UpdateDate = dr["ProductName"].ToString().Trim(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString().Trim()
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public List<ProductListReturn> GetProductName(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  (p.ProductCode = '" + pInfo.ProductCode + "')";
            }


            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();


            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.Product p " +
                                " where (p.FlagDelete ='N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return LProduct;
        }
        public List<ProductImageListReturn> GetProductImageUrl(ProductImageInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  (p.ProductCode = '" + pInfo.ProductCode + "')";
            }

            DataTable dt = new DataTable();
            var LProductImage = new List<ProductImageListReturn>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.ProductImage p " +
                                " where (p.FlagDelete ='N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProductImage = (from DataRow dr in dt.Rows

                            select new ProductImageListReturn()
                            {
                                ProductImageId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductImageUrl = dr["ProductImageUrl"].ToString().Trim(),
                                ProductImageName = dr["ProductImageName"].ToString().Trim(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return LProductImage;
        }
        public List<ProductImageListReturn> ListProductImageValidate(ProductImageInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  (p.ProductCode = '" + pInfo.ProductCode + "')";
            }

            DataTable dt = new DataTable();
            var LProductImage = new List<ProductImageListReturn>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.ProductImage p " +
                                " where (p.FlagDelete ='N') " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProductImage = (from DataRow dr in dt.Rows

                                 select new ProductImageListReturn()
                                 {
                                     
                                     ProductCode = dr["ProductCode"].ToString().Trim(),
                                     ProductImageUrl = dr["ProductImageUrl"].ToString().Trim(),
                                     
                                 }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return LProductImage;
        }
        public int? InsertProductImage(ProductImageInfo pInfo)
        {
            int? i = 0;

            string strsql = "INSERT INTO " + dbName + " .dbo.ProductImage  (ProductCode,ProductImageUrl,ProductImageName,FlagDelete)" +
                           "VALUES (" +
                          "'" + pInfo.ProductCode + "'," +
                          "'" + pInfo.ProductImageUrl + "'," +
                          "'" + pInfo.ProductImageName + "'," +
                          "'" + pInfo.FlagDelete + "')";

            DataTable dt = new DataTable();
            var LProductImage = new List<ProductImageInfo>();

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdateProductImage(ProductImageInfo pInfo)
        {
            int? i = 0;
            string strsql = "";

            if ((pInfo.ProductImageName != "") && (pInfo.ProductImageName != null))
            {
                if ((pInfo.ProductImageUrl != "") && (pInfo.ProductImageUrl != null))
                {
                    strsql = " Update " + dbName + ".dbo.ProductImage set " +
                                    " ProductImageUrl = '" + pInfo.ProductImageUrl + "'," +
                                    " ProductImageName = '" + pInfo.ProductImageName + "'," +
                                    " FlagDelete = '" + pInfo.FlagDelete + "'" +
                                    " where ProductCode ='" + pInfo.ProductCode + "'";
                }
            }
            else
            {
                strsql = " Update " + dbName + ".dbo.ProductImage set " +
                                
                                " FlagDelete = '" + pInfo.FlagDelete + "'" +
                                " where ProductCode ='" + pInfo.ProductCode + "'";
            }

            DataTable dt = new DataTable();
            var LProductImage = new List<ProductImageInfo>();

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? CountListProductBOMByCriteria(ProductBOMInfo pInfo) {

            string strcond = "";
            int? count = 0;

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  (p.ProductCode = '" + pInfo.ProductCode + "')";
            }

            if ((pInfo.ProductBOM != null) && (pInfo.ProductBOM != ""))
            {
                strcond += " and  (p.ProductBOM = '" + pInfo.ProductBOM + "')";
            }

            if ((pInfo.ProductBOMName != null) && (pInfo.ProductBOMName != ""))
            {
                strcond += " and  (pd.ProductName like '%" + pInfo.ProductBOMName + "%')";
            }
            if ((pInfo.InventoryCode != null) && (pInfo.InventoryCode != "") && (pInfo.InventoryCode != "-99"))
            {
                strcond += " and  (i.InventoryCode like '%" + pInfo.InventoryCode + "%')";
            }
            DataTable dt = new DataTable();
            var LProduct = new List<ProductBOMListReturn>();

            try
            {
                string strsql = "select count(p.Id) as countProductBOM from " + dbName + ".dbo.ProductBOM p " +
                                " left join Product pd on pd.ProductCode = p.ProductBOM " +
                                " left join Lookup l on l.LookupCode = p.Unit and l.LookupType = 'UNIT' and l.FlagDelete = 'N' " +
                                   "  inner join InventoryDetail as i on i.ProductCode = pd.ProductCode" +
                                " where p.FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductBOMListReturn()
                            {
                                countProductBOM = Convert.ToInt32(dr["countProductBOM"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProductBOM;
            }


            return count;
        }
        public List<ProductBOMListReturn> ListProductBOMByCriteria(ProductBOMInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }

            if ((pInfo.ProductBOM != null) && (pInfo.ProductBOM != ""))
            {
                strcond += " and  (p.ProductBOM = '" + pInfo.ProductBOM + "')";
            }

            if ((pInfo.ProductBOMName != null) && (pInfo.ProductBOMName != ""))
            {
                strcond += " and  (pd.ProductName like '%" + pInfo.ProductBOMName + "%')";
            }
            if ((pInfo.InventoryCode != null) && (pInfo.InventoryCode != "") && (pInfo.InventoryCode != "-99"))
            {
                strcond += " and  (i.InventoryCode like '%" + pInfo.InventoryCode + "%')";
            }
            DataTable dt = new DataTable();
            var LProduct = new List<ProductBOMListReturn>();

            try
            {
                string strsql = " Select distinct p.*, pd.ProductName as ProductBOMName, l.LookupValue as UnitName from " + dbName + ".dbo.ProductBOM p " +
                               " left join Product pd on pd.ProductCode = p.ProductBOM " +
                               " left join Lookup l on l.LookupCode = p.Unit and l.LookupType = 'UNIT' and l.FlagDelete = 'N' " +
                                  "  inner join InventoryDetail as i on i.ProductCode = pd.ProductCode" +
                               " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductBOMListReturn()
                            {
                                ProductBOMId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductBOM = dr["ProductBOM"].ToString().Trim(),
                                ProductBOMName = dr["ProductBOMName"].ToString().Trim(),
                                QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                ProductDetail = dr["ProductDetail"].ToString().Trim(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public int? InsertProductBOM(ProductBOMInfo pInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO ProductBOM  (ProductCode,ProductBOM,QTY,Unit,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete) " +
                            "VALUES (" +
                            "'" + pInfo.ProductCode + "'," +
                            "'" + pInfo.ProductBOM + "'," +
                            "'" + pInfo.QTY + "'," +
                            "'" + pInfo.Unit + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + pInfo.CreateBy + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + pInfo.UpdateBy + "'," +
                            "'" + pInfo.FlagDelete + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? DeleteProductBOM(ProductBOMInfo pInfo)
        {
            int i = 0;
            string strsql = "Update " + dbName + ".dbo.ProductBOM set FlagDelete = 'Y' where Id in (" + pInfo.ProductBOM + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<ProductListReturn> ProductCodeValidateInsert(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode = '" + pInfo.ProductCode + "'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName = '" + pInfo.ProductName + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select p.Id,p.ProductCode from " + dbName + ".dbo.Product p " +
                               " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                             select new ProductListReturn()
                             {
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public List<ProductListReturn> ProductCodeValidateInventorydetail(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode = '" + pInfo.ProductCode + "'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName = '" + pInfo.ProductName + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select p.ProductCode from " + dbName + ".dbo.InventoryDetail p " +
                               " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductCode = dr["ProductCode"].ToString().Trim(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }
        public int? InsertProductfromImportProduct(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO Product  (ProductCode,ProductName,ProductCategoryCode,ProductBrandCode," +
                "MerchantCode,Price,Unit,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,Sku,ProductDesc,UpsellScript)" +
                            "VALUES (" +
                           "'" + pInfo.ProductCode + "'," +
                           "'" + pInfo.ProductName + "'," +
                           "'" + pInfo.ProductCategoryCode + "'," +
                           "'" + pInfo.ProductBrandCode + "'," +
                           "'" + pInfo.MerchantCode + "'," +
                           "'" + pInfo.Price + "'," +
                           "'" + pInfo.Unit + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.UpdateBy + "'," +
                           "'" + pInfo.FlagDelete + "'," +
                           "'" + pInfo.Sku + "',"+
                           "'" + pInfo.Description + "'," +
                           "'" + pInfo.UpsellScript + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateProductfromImportProduct(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.Product set " +
                            " ProductCode = '" + pInfo.ProductCode + "'," +
                            " ProductName = '" + pInfo.ProductName + "'," +
                            " ProductBrandCode = '" + pInfo.ProductBrandCode + "'," +
                            " ProductCategoryCode = '" + pInfo.ProductCategoryCode + "'," +
                            " MerchantCode = '" + pInfo.MerchantCode + "'," +
                            " Price = '" + pInfo.Price + "'," +
                            " Unit = '" + pInfo.Unit + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " Sku = '" + pInfo.Sku + "'," +
                            " ProductDesc = '" + pInfo.Description + "'," +
                            " UpsellScript = '" + pInfo.UpsellScript + "'," +
                            " UpdateBy ='" + pInfo.UpdateBy + "'" +

                           " where Id ='" + pInfo.ProductId + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }


        public List<ProductListReturn> GetProductBySlug(ProductInfo pInfo)
        {
            string strcond = "";


            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName = '" + pInfo.ProductName + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select pc.ProductCategoryCode,pc.ProductCategoryName,d.Id As PromotionDetailId,d.Price as PromotionDetailPrice,d.DiscountAmount,d.DiscountPercent,d.ComplementaryAmount," +
                                  "d.LockAmountFlag,d.DefaultAmount,t.PromotionCode,t.PromotionName,p.* from " + dbName + ".dbo.Product p " +
                                " left join ProductCategory pc on pc.ProductCategoryCode = p.ProductCategoryCode " +
                                " left join PromotionDetailInfo d on d.ProductCode =  p.ProductCode" +
                                " left join Promotion t on t.PromotionCode =  d.PromotionCode " +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                PromotionName = dr["PromotionName"].ToString().Trim(),
                                ProductDesc = dr["ProductDesc"].ToString().Trim(),
                                ProductImageUrl = dr["ProductImageUrl"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                PromotionDetailPrice = (dr["PromotionDetailPrice"].ToString() != "") ? Convert.ToDouble(dr["PromotionDetailPrice"]) : 0,
                                DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
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

            return LProduct;
        }
        

        public List<ProductListReturn> ListProductInPromotion(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode like '%" + pInfo.MerchantCode + "%'";
            }
            if ((pInfo.MerchantName != null) && (pInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + pInfo.MerchantName + "%'";
            }
            if ((pInfo.TransportationTypeCode != null) && (pInfo.TransportationTypeCode != ""))
            {
                strcond += " and  p.TransportationTypeCode like '%" + pInfo.TransportationTypeCode + "%'";
            }
            if ((pInfo.TransportationTypeName != null) && (pInfo.TransportationTypeName != ""))
            {
                strcond += " and  p.TransportationTypeName like '%" + pInfo.TransportationTypeName + "%'";
            }
            if ((pInfo.ProductCategoryCode != null) && (pInfo.ProductCategoryCode != ""))
            {
                strcond += " and  pg.ProductCategoryCode like '%" + pInfo.ProductCategoryCode + "%'";
            }
            if ((pInfo.ProductCategoryName != null) && (pInfo.ProductCategoryName != ""))
            {
                strcond += " and  pg.ProductCategoryName like '%" + pInfo.ProductCategoryName + "%'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != ""))
            {
                strcond += " and  pb.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select p.*, pd.*,s.QTY,s.Reserved,s.Balance,s.Intransit, m.MerchantName, l.LogisticName, pg.ProductCategoryName, u.LookUpValue as UnitName, pb.ProductBrandName from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " left join Merchant m on m.MerchantCode = p.MerchantCode and  m.FlagDelete ='N'" +
                                " left join Logistic l on l.LogisticCode = p.TransportationTypeCode and  l.FlagDelete ='N'" +
                                " left join ProductCategory pg on pg.ProductCategoryCode = p.ProductCategoryCode and  l.FlagDelete ='N'" +
                                " left join LookUp u on u.LookUpCode = p.Unit and  u.LookUpType ='UNIT'" +
                                " left join PromotionDetailInfo pd on pd.ProductCode = p.ProductCode and pd.PromotionCode like '%"+ pInfo.PromotionCode + "%' and pd.FlagDelete ='N'" +
                                " left join ProductBrand pb on pb.ProductBrandCode = p.ProductBrandCode and  pb.FlagDelete ='N'" +
                                " where p.FlagDelete ='N' and pd.PromotionCode is null" + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                TransportationTypeCode = dr["TransportationTypeCode"].ToString().Trim(),
                                TransportationTypeName = dr["LogisticName"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                Description = dr["ProductDesc"].ToString().Trim(),
                                QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                Intransit = (dr["Intransit"].ToString() != "") ? Convert.ToInt32(dr["Intransit"]) : 0,
                                Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                ProductWidth = (dr["ProductWidth"].ToString() != "") ? Convert.ToDouble(dr["ProductWidth"]) : 0,
                                ProductHeigth = (dr["ProductHeigth"].ToString() != "") ? Convert.ToDouble(dr["ProductHeigth"]) : 0,
                                ProductLength = (dr["ProductLength"].ToString() != "") ? Convert.ToDouble(dr["ProductLength"]) : 0,
                                PackageWidth = (dr["PackageWidth"].ToString() != "") ? Convert.ToDouble(dr["PackageWidth"]) : 0,
                                PackageHeigth = (dr["PackageHeigth"].ToString() != "") ? Convert.ToDouble(dr["PackageHeigth"]) : 0,
                                PackageLength = (dr["PackageLength"].ToString() != "") ? Convert.ToDouble(dr["PackageLength"]) : 0,
                                Weight = (dr["Weight"].ToString() != "") ? Convert.ToDouble(dr["Weight"]) : 0,
                                ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                ProductBrandName = dr["ProductBrandName"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }

        public List<ProductListReturn> ListProductInPromotionNoPaging(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode like '%" + pInfo.MerchantCode + "%'";
            }
            if ((pInfo.MerchantName != null) && (pInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + pInfo.MerchantName + "%'";
            }
            if ((pInfo.TransportationTypeCode != null) && (pInfo.TransportationTypeCode != ""))
            {
                strcond += " and  p.TransportationTypeCode like '%" + pInfo.TransportationTypeCode + "%'";
            }
            if ((pInfo.TransportationTypeName != null) && (pInfo.TransportationTypeName != ""))
            {
                strcond += " and  p.TransportationTypeName like '%" + pInfo.TransportationTypeName + "%'";
            }
            if ((pInfo.ProductCategoryCode != null) && (pInfo.ProductCategoryCode != ""))
            {
                strcond += " and  pg.ProductCategoryCode like '%" + pInfo.ProductCategoryCode + "%'";
            }
            if ((pInfo.ProductCategoryName != null) && (pInfo.ProductCategoryName != ""))
            {
                strcond += " and  pg.ProductCategoryName like '%" + pInfo.ProductCategoryName + "%'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != ""))
            {
                strcond += " and  pb.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " select p.*, pd.*,s.QTY,s.Reserved,s.Balance,s.Intransit, m.MerchantName, l.LogisticName, pg.ProductCategoryName, u.LookUpValue as UnitName, pb.ProductBrandName from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " left join Merchant m on m.MerchantCode = p.MerchantCode and  m.FlagDelete ='N'" +
                                " left join Logistic l on l.LogisticCode = p.TransportationTypeCode and  l.FlagDelete ='N'" +
                                " left join ProductCategory pg on pg.ProductCategoryCode = p.ProductCategoryCode and  l.FlagDelete ='N'" +
                                " left join LookUp u on u.LookUpCode = p.Unit and  u.LookUpType ='UNIT'" +
                                " left join PromotionDetailInfo pd on pd.ProductCode = p.ProductCode and pd.PromotionCode like '%" + pInfo.PromotionCode + "%' and pd.FlagDelete ='N'" +
                                " left join ProductBrand pb on pb.ProductBrandCode = p.ProductBrandCode and  pb.FlagDelete ='N'" +
                                " where p.FlagDelete ='N' and pd.PromotionCode is null" + strcond;

                strsql += " ORDER BY p.Id";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                TransportationTypeCode = dr["TransportationTypeCode"].ToString().Trim(),
                                TransportationTypeName = dr["LogisticName"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                Description = dr["ProductDesc"].ToString().Trim(),
                                QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                Intransit = (dr["Intransit"].ToString() != "") ? Convert.ToInt32(dr["Intransit"]) : 0,
                                Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                ProductWidth = (dr["ProductWidth"].ToString() != "") ? Convert.ToDouble(dr["ProductWidth"]) : 0,
                                ProductHeigth = (dr["ProductHeigth"].ToString() != "") ? Convert.ToDouble(dr["ProductHeigth"]) : 0,
                                ProductLength = (dr["ProductLength"].ToString() != "") ? Convert.ToDouble(dr["ProductLength"]) : 0,
                                PackageWidth = (dr["PackageWidth"].ToString() != "") ? Convert.ToDouble(dr["PackageWidth"]) : 0,
                                PackageHeigth = (dr["PackageHeigth"].ToString() != "") ? Convert.ToDouble(dr["PackageHeigth"]) : 0,
                                PackageLength = (dr["PackageLength"].ToString() != "") ? Convert.ToDouble(dr["PackageLength"]) : 0,
                                Weight = (dr["Weight"].ToString() != "") ? Convert.ToDouble(dr["Weight"]) : 0,
                                ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                ProductBrandName = dr["ProductBrandName"].ToString(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }

        public int? CountProductListInPromotion(ProductInfo pInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((pInfo.ProductId != null) && (pInfo.ProductId != 0))
            {
                strcond += " and  p.Id =" + pInfo.ProductId;
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pInfo.ProductName + "%'";
            }

            if ((pInfo.ProductCategoryCode != null) && (pInfo.ProductCategoryCode != ""))
            {
                strcond += " and  pg.ProductCategoryCode like '%" + pInfo.ProductCategoryCode + "%'";
            }

            if ((pInfo.MerchantName != null) && (pInfo.MerchantName != ""))
            {
                strcond += " and  m.MerchantName like '%" + pInfo.MerchantName + "%'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != ""))
            {
                strcond += " and  pb.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = "select count(p.Id) as countProduct from " + dbName + ".dbo.Product p " +
                                " left join ProductStock s on p.ProductCode = s.ProductCode and  s.FlagDelete ='N'" +
                                " left join Merchant m on m.MerchantCode = p.MerchantCode and  m.FlagDelete ='N'" +
                                " left join Logistic l on l.LogisticCode = p.TransportationTypeCode and  l.FlagDelete ='N'" +
                                " left join ProductCategory pg on pg.ProductCategoryCode = p.ProductCategoryCode and  l.FlagDelete ='N'" +
                                " left join LookUp u on u.LookUpCode = p.Unit and  u.LookUpType ='UNIT'" +
                                " left join PromotionDetailInfo pd on pd.ProductCode = p.ProductCode and pd.PromotionCode like '%" + pInfo.PromotionCode + "%' and pd.FlagDelete ='N'" +
                                " left join ProductBrand pb on pb.ProductBrandCode = p.ProductBrandCode and  pb.FlagDelete ='N'" +
                                " where p.FlagDelete ='N' and pd.PromotionCode is null" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                countProduct = Convert.ToInt32(dr["countProduct"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProduct;
            }

            return count;
        }

        public List<ProductListReturn> ListPromotionDetailforTakeOrder(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  d.PromotionCode = '" + pInfo.PromotionCode + "'";
            }

            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode = '" + pInfo.CampaignCode + "'";
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  d.ProductCode = '" + pInfo.ProductCode + "'";
            }

            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  pro.ProductName like '%" + pInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " SELECT d.Id AS PromotionDetailId, d.ProductCode, d.PromotionCode, d.Price AS PromotionDetailPrice, pro.Price, pro.Transportprice, d.DiscountPercent, d.DiscountAmount, d.LockAmountFlag, d.LockCheckbox, ComplementaryFlag, d.DefaultAmount, d.ComplementaryAmount, d.CreateDate, d.CreateBy, d.UpdateDate, d.UpdateBy, d.FlagDelete, p.PromotionName, " +
                                " c.CampaignCode, c.CampaignName, pro.ProductName, pc.ProductCategoryCode, pc.ProductCategoryName,m.MerchantCode, m.MerchantName, pro.Unit, u.LookupValue " + 
                                " FROM PromotionDetailInfo AS d LEFT OUTER JOIN " +
                                " Promotion AS p ON p.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                " CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                " Campaign AS c ON c.CampaignCode = cp.CampaignCode LEFT OUTER JOIN " +
                                " Product AS pro ON pro.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                " ProductCategory AS pc ON pc.ProductCategoryCode = pro.ProductCategoryCode LEFT OUTER JOIN " +
                                " Merchant AS m ON m.MerchantCode = pro.MerchantCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = pro.Unit AND u.LookupType = 'UNIT' " +
                                " WHERE (d.FlagDelete = 'N') AND (p.FlagDelete = 'N') AND (cp.Active = 'Y') AND (c.FlagDelete = 'N') AND (pro.FlagDelete = 'N') AND (pc.FlagDelete = 'N') " +
                                strcond;

                strsql += " ORDER BY PromotionDetailId DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                ProductCategoryCode = dr["ProductCategoryCode"].ToString().Trim(),
                                ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                MerchantName = dr["MerchantName"].ToString().Trim(),
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                PromotionName = dr["PromotionName"].ToString().Trim(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                Transportprice = (dr["Transportprice"].ToString() != "") ? Convert.ToDouble(dr["Transportprice"]) : 0,
                                Unit = dr["Unit"].ToString().Trim(),
                                PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                PromotionDetailPrice = (dr["PromotionDetailPrice"].ToString() != "") ? Convert.ToDouble(dr["PromotionDetailPrice"]) : 0,
                                DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                ComplementaryFlag = dr["ComplementaryFlag"].ToString().Trim(),
                                DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                CampaignCode = dr["CampaignCode"].ToString().Trim(),
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

            return LProduct;
        }

        public int? CountPromotionDetailTakeOrderByCriteria(ProductInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  d.PromotionCode = '" + pInfo.PromotionCode + "'";
            }

            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode = '" + pInfo.CampaignCode + "'";
            }

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  d.ProductCode = '" + pInfo.ProductCode + "'";
            }

            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  pro.ProductName like '%" + pInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " SELECT count(d.Id) as countProduct " +
                                " FROM PromotionDetailInfo AS d LEFT OUTER JOIN " +
                                " Promotion AS p ON p.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                " CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                " Campaign AS c ON c.CampaignCode = cp.CampaignCode LEFT OUTER JOIN " +
                                " Product AS pro ON pro.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                " ProductCategory AS pc ON pc.ProductCategoryCode = pro.ProductCategoryCode LEFT OUTER JOIN " +
                                " Merchant AS m ON m.MerchantCode = pro.MerchantCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = pro.Unit AND u.LookupType = 'UNIT' " +
                                " WHERE (d.FlagDelete = 'N') AND (p.FlagDelete = 'N') AND (cp.Active = 'Y') AND (c.FlagDelete = 'N') AND (pro.FlagDelete = 'N') AND (pc.FlagDelete = 'N') AND " +
                                " (d.PromotionCode = '" + pInfo.PromotionCode + "') AND (cp.CampaignCode = '" + pInfo.CampaignCode + "') " +
                                strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                countProduct = Convert.ToInt32(dr["countProduct"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LProduct.Count > 0)
            {
                count = LProduct[0].countProduct;
            }

            return count;
        }

        public int InsertProductImport(List<ProductListReturn> pinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in pinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.Product (ProductCode, ProductName, MerchantCode, SupplierCode, ProductCategoryCode, Price, Unit, ProductWidth, ProductLength, ProductHeigth, PackageWidth, PackageLength, PackageHeigth, Weight, TransportationTypeCode, ProductDesc, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete) values (" +
                             "'" + info.ProductCode + "', " +
                             "'" + info.ProductName + "', " +
                             "'" + info.MerchantCode + "', " +
                             "'" + info.SupplierCode + "', " +
                             "'" + info.ProductCategoryCode + "', " +
                             "'" + info.Price + "', " +
                             "'" + info.Unit + "', " +
                             "" + info.ProductWidth + ", " +
                             "" + info.ProductLength + ", " +
                             "" + info.ProductHeigth + ", " +
                             "" + info.PackageWidth + ", " +
                             "" + info.PackageLength + ", " +
                             "" + info.PackageHeigth + ", " +
                             "" + info.Weight + ", " +
                             "'" + info.TransportationTypeCode + "', " +
                             "'" + info.Description + "', " +
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

        public List<ProductListReturn> ListTop5ProductodOrderCustomerByCriteria(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.CustomerCode != null) && (pInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode = '" + pInfo.CustomerCode + "'";
            }

            

            if ((pInfo.CampaignCategoryCode != null) && (pInfo.CampaignCategoryCode != ""))
            {
                //strcond += " and  o.CampaignCategoryCode = '" + pInfo.CampaignCategoryCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = " SELECT TOP (5) SUM(dt.Amount) AS SumAmount, dt.ProductCode, p.ProductName, o.CampaignCategoryCode, ct.CamCate_name, dt.CampaignCode, dt.PromotionCode, dt.PromotionDetailId, pd.Price, pd.DiscountAmount, pd.DiscountPercent, p.Unit, u.LookupValue AS UnitName " +
                                " FROM            OrderDetail AS dt LEFT OUTER JOIN " +
                                " OrderInfo AS o ON o.OrderCode = dt.OrderCode LEFT OUTER JOIN " +
                                " Product AS p ON p.ProductCode = dt.ProductCode LEFT OUTER JOIN " +
                                " CampaignCategory AS ct ON ct.CampaignCategoryCode = o.CampaignCategoryCode LEFT OUTER JOIN " +
                                " PromotionDetailInfo AS pd ON pd.Id = dt.PromotionDetailId LEFT OUTER JOIN " +
                                " CampaignPromotion AS cp ON cp.PromotionCode = dt.PromotionCode AND cp.PromotionCode = dt.PromotionCode AND cp.CampaignCode = dt.CampaignCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' " +
                                " where (p.FlagDelete = 'N') " + strcond;

                strsql += " GROUP BY dt.ProductCode, o.CampaignCategoryCode, dt.PromotionCode, dt.PromotionDetailId, dt.CampaignCode, p.ProductName, ct.CamCate_name, pd.Price, pd.DiscountAmount, pd.DiscountPercent, p.Unit, u.LookupValue " +
                          " ORDER BY SumAmount DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                
                                ProductCode = dr["ProductCode"].ToString().Trim(),
                                ProductName = dr["ProductName"].ToString().Trim(),
                                CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                CampaignCategoryName = dr["CamCate_name"].ToString().Trim(),
                                CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                Unit = dr["Unit"].ToString().Trim(),
                                UnitName = dr["UnitName"].ToString().Trim(),
                                SumAmount = (dr["SumAmount"].ToString() != "") ? Convert.ToInt32(dr["SumAmount"]) : 0,
                                PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }

        public List<ProductBrandListReturn> ListProductBrandNopagingByCriteria(ProductBrandInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != ""))
            {
                strcond += " and  p.ProductBrandCode = '" + pInfo.ProductBrandCode + "'";
            }
            
            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + pInfo.MerchantMapCode + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductBrandListReturn>();

            try
            {
                
                string strsql = " select * from " + dbName + ".dbo.ProductBrand p " +
                                " inner join CampaignCategory as c on c.CampaignCategoryCode = p.ProductBrandCode " +
                                " where p.FlagDelete ='N' " + strcond;

                

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductBrandListReturn()
                            {
                                

                      
                                ProductBrandId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                ProductBrandName = dr["ProductBrandName"].ToString(),
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

            return LProduct;
        }

        public List<AllergyListReturn> ListAllergyNopagingByCriteria(AllergyInfo pInfo)
        {
            string strcond = "";


            DataTable dt = new DataTable();
            var LProduct = new List<AllergyListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.Allergy p " +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new AllergyListReturn()
                            {
                                AllergyId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                AllergyCode = dr["ProductBrandCode"].ToString(),
                                AllergyName = dr["ProductBrandName"].ToString(),
                                AllergyImageUrl = dr["AllergyImageUrl"].ToString(),
                                AllergyImageName = dr["AllergyImageName"].ToString(),
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

            return LProduct;
        }

        public int? InsertProductfromImportInventoryDetail(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = "INSERT INTO Product  (ProductCode,ProductName,ProductCategoryCode,ProductBrandCode,CreateDate,CreateBy,UpdateDate,UpdateBy,MerchantCode,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.ProductCode + "'," +
                           "'" + pInfo.ProductName + "'," +
                           "'" + pInfo.ProductCategoryCode + "'," +
                           "'" + pInfo.ProductBrandCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.UpdateBy + "'," +
                            "'" + pInfo.MerchantCode + "'," +
                           "'" + pInfo.FlagDelete + "')";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateProductfromImportInventoryDetail(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.Product set " +
                            " ProductCode = '" + pInfo.ProductCode + "'," +
                            " ProductName = '" + pInfo.ProductName + "'," +
                            " ProductBrandCode = '" + pInfo.ProductBrandCode + "'," +
                            " ProductCategoryCode = '" + pInfo.ProductCategoryCode + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                               " MerchantCode = '" + pInfo.MerchantCode + "'," +
                            " UpdateBy ='" + pInfo.UpdateBy + "'" +

                           " where Id ='" + pInfo.ProductId + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<ProductListReturn> ListProductNopagingByEcommerce(ProductInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != ""))
            {
                strcond += " and  pr.PromotionCode = '" + cInfo.PromotionCode + "'";
            }
            if ((cInfo.PromotionTypeCode != null) && (cInfo.PromotionTypeCode != ""))
            {
                strcond += " and pr.PromotionTypeCode = '" + cInfo.PromotionTypeCode + "' and pr.PromotionTypeCode <> '18' ";
            }
            else
            {
                strcond += " and pr.PromotionTypeCode <> '18' ";
            }
            if ((cInfo.ProductCode != null) && (cInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode = '" + cInfo.ProductCode + "'";
            }
            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + cInfo.CampaignCode + "'";
            }
            if ((cInfo.ProductId != null) && (cInfo.ProductId != 0))
            {
                strcond += " and  p.id = '" + cInfo.ProductId + "'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + cInfo.MerchantCode + "'";
            }
            if ((cInfo.ProductName != null) && (cInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName = '" + cInfo.ProductName + "'";
            }
            

            //Loop string search PromotionTagCode multivalue
            if ((cInfo.PromotionTagCode != null) && (cInfo.PromotionTagCode != ""))
            {
                char[] separator = new char[1] { ',' };
                string[] subResult = cInfo.PromotionTagCode.Split(separator);

                for (int i = 0; i <= subResult.Length - 1; i++)
                {
                    strcond += " and  pr.PromotionTagCode like '%" + subResult[i] + "%'";
                }
            }
            // End string search PromotionTagCode multivalue

            

            //Loop string search ProductTagCode multivalue
            if ((cInfo.ProductTagCode != null) && (cInfo.ProductTagCode != ""))
            {
                char[] separator1 = new char[1] { ',' };
                string[] subResult1 = cInfo.ProductTagCode.Split(separator1);

                for (int i = 0; i <= subResult1.Length - 1; i++)
                {
                    strcond += " and  pr.ProductTagCode like '%" + subResult1[i] + "%'";
                }
            }
            // End string search ProductTagCode multivalue

            if ((cInfo.CreateDate != null) && (cInfo.CreateDate != ""))
            {
                strcond += " and  '" + cInfo.CreateDate + "'" + " between pr.StartDate and pr.EndDate ";
            }
            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();
            try
            {

                string strsql = " select p.id as ProductID,p.EcomSpec ,pd.id as PromotionDetailID , pd.ProductCode, p.ProductName ,c.CampaignCode,c.CampaignName,pr.PromotionCode,pr.PromotionTypeCode,pr.PromotionName, pd.Price, pd.DiscountPercent AS ProductDiscountPercent, pd.DiscountAmount AS ProductDiscountAmount, pr.FreeShipping ,pdi.ProductImageUrl ,p.MerchantCode ,mc.MerchantName, pr.PromotionTagCode, pr.PromotionFlashSaleStartDate, PromotionFlashSaleEndDate, " +

                                " (select ',' + t.LookupValue from Lookup    as t  where ',' + pr.PromotionTagCode + ',' like '%,' + t.LookupCode + ',%'  and t.LookupType = 'TAGPromotion' and t.flagdelete = 'N'for xml path(''), type).value('substring(text()[1], 2)', 'varchar(max)') as PromotionTagName,  " +

                                " pr.ProductTagCode, " +

                                " (select ','+CM.LookupValue from   Lookup    as CM where ',' + pr.ProductTagCode + ',' like '%,' + CM.LookupCode + ',%'  and CM.LookupType = 'TAGPRODUCT' and CM.flagdelete = 'N'for xml path(''), type).value('substring(text()[1], 2)', 'varchar(max)') as ProductTagName " +
                                
                                " from " + dbName + ".dbo.PromotionDetailInfo pd " +
                                " LEFT OUTER JOIN  Product AS p ON p.ProductCode = pd.ProductCode  " +
                                " LEFT OUTER JOIN  CampaignPromotion AS cp ON cp.PromotionCode = pd.PromotionCode " +
                                " LEFT OUTER JOIN  Promotion AS pr ON pr.PromotionCode = pd.PromotionCode " +
                                " LEFT OUTER JOIN  ProductImage AS pdi on pdi.ProductCode = p.ProductCode " +
                                " LEFT OUTER JOIN  Campaign AS c ON c.CampaignCode = cp.CampaignCode " +
                                " LEFT OUTER JOIN  Merchant AS mc ON mc.MerchantCode = p.MerchantCode " +
                                " LEFT OUTER JOIN  Lookup AS t ON t.LookupCode = pr.PromotionTagCode AND t.LookupType = 'TAGPROMOTION' AND t.FlagDelete = 'N' " +
                                " where p.FlagDelete = 'N' and c.CampaignSpec = '02' and pr.EndDate >= GETDATE() and pd.FlagDelete = 'N' " + strcond;

                //string strsql = " select * from " + dbName + ".dbo.CampaignCategory p " +
                //                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["ProductID"].ToString() != "") ? Convert.ToInt32(dr["ProductID"]) : 0,
                                ProductCode = dr["ProductCode"].ToString(),
                                ProductName = dr["ProductName"].ToString(),
                                PromotionCode = dr["PromotionCode"].ToString(),
                                PromotionDetailId = (dr["PromotionDetailID"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailID"]) : 0,
                                PromotionName = dr["PromotionName"].ToString(),
                                CampaignCode = dr["CampaignCode"].ToString(),
                                CampaignName = dr["CampaignName"].ToString(),
                                PromotionTypeCode = dr["PromotionTypeCode"].ToString(),
                                Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                //DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                //DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountAmount"]) : 0,
                                ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountPercent"]) : 0,
                                ProductImageUrl = dr["ProductImageUrl"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString(),
                                MerchantName = dr["MerchantName"].ToString(),
                                EcomSpec = dr["EcomSpec"].ToString(),
                                PromotionTagCode = dr["PromotionTagCode"].ToString(),
                                PromotionTagName = dr["PromotionTagName"].ToString(),
                                ProductTagCode = dr["ProductTagCode"].ToString(),
                                ProductTagName = dr["ProductTagName"].ToString(),
                                PromotionFlashSaleStartDate = dr["PromotionFlashSaleStartDate"].ToString(),
                                PromotionFlashSaleEndDate = dr["PromotionFlashSaleEndDate"].ToString(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;

        }
        public List<ProductListReturn> ListProductDetailNopagingByEcommerce(ProductInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != ""))
            {
                strcond += " and  pr.PromotionCode = '" + cInfo.PromotionCode + "'";
            }
            if ((cInfo.ProductCode != null) && (cInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode = '" + cInfo.ProductCode + "'";
            }
            if ((cInfo.ProductId != null) && (cInfo.ProductId != 0))
            {
                strcond += " and  p.id = '" + cInfo.ProductId + "'";
            }
            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + cInfo.CampaignCode + "'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + cInfo.MerchantCode + "'";
            }

            //Loop string search PromotionTagCode multivalue
            if ((cInfo.PromotionTagCode != null) && (cInfo.PromotionTagCode != ""))
            {
                char[] separator = new char[1] { ',' };
                string[] subResult = cInfo.PromotionTagCode.Split(separator);

                for (int i = 0; i <= subResult.Length - 1; i++)
                {
                    strcond += " and  pr.PromotionTagCode like '%" + subResult[i] + "%'";
                }
            }
            // End string search PromotionTagCode multivalue

            //Loop string search ProductTagCode multivalue
            if ((cInfo.ProductTagCode != null) && (cInfo.ProductTagCode != ""))
            {
                char[] separator1 = new char[1] { ',' };
                string[] subResult1 = cInfo.ProductTagCode.Split(separator1);

                for (int i = 0; i <= subResult1.Length - 1; i++)
                {
                    strcond += " and  pr.ProductTagCode like '%" + subResult1[i] + "%'";
                }
            }
            // End string search ProductTagCode multivalue

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();
            try
            {

                string strsql = " select p.id as ProductID ,pd.id as PromotionDetailID, pd.ProductCode ,c.CampaignCode,c.CampaignName,c.CampaignCategory,pr.PromotionTypeCode,pr.PromotionCode,pr.PromotionName, p.ProductName,p.ProductDesc " +
                                " ,pd.Price, pd.DiscountPercent AS ProductDiscountPercent, pd.DiscountAmount AS ProductDiscountAmount, pr.FreeShipping ,pdi.ProductImageUrl ,p.MerchantCode ,mc.MerchantName " +
                                " ,p.ProductImage1,p.CarType,p.MaintainType,p.InsureCost,p.FirstDamages,p.GarageQuan,p.EcomSpec, pd.QuotaOnHand, pd.QuotaReserved, pd.QuotaBalance, pr.PromotionTagCode, pr.PromotionTagName, pr.ProductTagCode, pr.ProductTagName, pr.PromotionFlashSaleStartDate, pr.PromotionFlashSaleEndDate " +
                                " from " + dbName + ".dbo.PromotionDetailInfo pd " +
                                " LEFT OUTER JOIN  Product AS p ON p.ProductCode = pd.ProductCode  " +
                                " LEFT OUTER JOIN  CampaignPromotion AS cp ON cp.PromotionCode = pd.PromotionCode " +
                                " LEFT OUTER JOIN  Promotion AS pr ON pr.PromotionCode = pd.PromotionCode " +
                                " LEFT OUTER JOIN  ProductImage AS pdi on pdi.ProductCode = p.ProductCode " +
                                " LEFT OUTER JOIN  Campaign AS c ON c.CampaignCode = cp.CampaignCode  " +
                                " LEFT OUTER JOIN  Merchant AS mc ON mc.MerchantCode = p.MerchantCode " +
                                " where p.FlagDelete = 'N' and c.CampaignSpec = '02' AND (pr.EndDate >= GETDATE()) AND (pd.FlagDelete = 'N') " + strcond;

                

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                ProductId = (dr["ProductID"].ToString() != "") ? Convert.ToInt32(dr["ProductID"]) : 0,
                                ProductCode = dr["ProductCode"].ToString(),
                                ProductName = dr["ProductName"].ToString(),
                                ProductDesc = dr["ProductDesc"].ToString(),
                                PromotionCode = dr["PromotionCode"].ToString(),
                                PromotionTypeCode = dr["PromotionTypeCode"].ToString(),
                                PromotionDetailId = (dr["PromotionDetailID"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailID"]) : 0,
                                PromotionName = dr["PromotionName"].ToString(),
                                CampaignCode = dr["CampaignCode"].ToString(),
                                CampaignName = dr["CampaignName"].ToString(),
                                CampaignCategoryCode = dr["CampaignCategory"].ToString(),
                                Product_img1 = dr["ProductImage1"].ToString(),
                                CarType = dr["CarType"].ToString(),
                                MaintainType = dr["MaintainType"].ToString(),
                                InsureCost = (dr["InsureCost"].ToString() != "") ? Convert.ToInt32(dr["InsureCost"]) : 0,
                                FirstDamages = (dr["FirstDamages"].ToString() != "") ? Convert.ToInt32(dr["FirstDamages"]) : 0,
                                GarageQuan = (dr["GarageQuan"].ToString() != "") ? Convert.ToInt32(dr["GarageQuan"]) : 0,
                                Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                
                                ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountAmount"]) : 0,
                                ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountPercent"]) : 0,
                                FreeShipping = dr["FreeShipping"].ToString(),
                                ProductImageUrl = dr["ProductImageUrl"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString(),
                                MerchantName = dr["MerchantName"].ToString(),
                                EcomSpec = dr["EcomSpec"].ToString(),
                                QuotaOnHand = (dr["QuotaOnHand"].ToString() != "") ? Convert.ToInt32(dr["QuotaOnHand"]) : 0,
                                QuotaReserved = (dr["QuotaReserved"].ToString() != "") ? Convert.ToInt32(dr["QuotaReserved"]) : 0,
                                QuotaBalance = (dr["QuotaBalance"].ToString() != "") ? Convert.ToInt32(dr["QuotaBalance"]) : 0,
                                PromotionTagCode = dr["PromotionTagCode"].ToString().Trim(),
                                PromotionTagName = dr["PromotionTagName"].ToString(),
                                ProductTagCode = dr["ProductTagCode"].ToString(),
                                ProductTagName = dr["ProductTagName"].ToString(),
                                PromotionFlashSaleStartDate = dr["PromotionFlashSaleStartDate"].ToString(),
                                PromotionFlashSaleEndDate = dr["PromotionFlashSaleEndDate"].ToString(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;

        }
        public List<ProductListReturn> ValidateProductinPromotion(ProductInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  pd.ProductCode = '" + pInfo.ProductCode + "'";
            }

            DataTable dt = new DataTable();
            var LProduct = new List<ProductListReturn>();

            try
            {
                string strsql = "SELECT pd.PromotionCode, pd.ProductCode, pr.StartDate, pr.EndDate, GETDATE() AS todate from " + dbName + ".dbo.PromotionDetailInfo pd " +
                                " LEFT JOIN " + dbName + ".dbo.Promotion AS pr ON pr.PromotionCode = pd.PromotionCode " +
                                " where pd.FlagDelete ='N' AND pr.EndDate >= GETDATE() " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LProduct = (from DataRow dr in dt.Rows

                            select new ProductListReturn()
                            {
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                ProductCode = dr["ProductCode"].ToString().Trim(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LProduct;
        }

        public int? UpdateProductDetailQuotaBalancebyEcommerce(ProductInfo pInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " QuotaReserved ='" + pInfo.QuotaReserved + "'," +
                            " QuotaBalance ='" + pInfo.QuotaBalance + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + pInfo.UpdateBy + "'" +
                            " where Id ='" + pInfo.PromotionDetailId + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }

}

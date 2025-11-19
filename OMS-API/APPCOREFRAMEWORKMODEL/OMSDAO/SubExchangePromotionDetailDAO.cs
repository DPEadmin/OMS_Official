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
    public class SubExchangePromotionDetailDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        

        public int UpdateSubExchangePromotonDetail(SubExchangePromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.SubExchangePromotionDetailInfo set " +
                           " PromotionCode = '" + cInfo.PromotionCode + "'," +
                           " PromotionDetailId = '" + cInfo.PromotionDetailId + "'," +
                          " ProductCode = '" + cInfo.PromotionDetailId + "'," +
                          " Amount = '" + cInfo.PromotionDetailId + "'," +
                          " FlagSubPromotionDetailExchange = '" + cInfo.FlagSubPromotionDetailExchange + "'," +
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " CombosetCode = '" + cInfo.CombosetCode + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.SubMainId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSubExchangePromotonDetail(SubExchangePromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SubExchangePromotionDetailInfo set FlagDelete = 'Y' where Id in (" + cInfo.SubMainIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSubExchangePromotonDetailByCode(SubExchangePromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SubExchangePromotionDetailInfo set FlagDelete = 'Y' where PromotionDetailId in (" + cInfo.SubMainIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertSubExchangePromotionDetail(SubExchangePromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO SubExchangePromotionDetailInfo  (PromotionCode,PromotionDetailId,ProductCode,Amount,FlagSubPromotionDetailExchange,CreateDate,CreateBy,UpdateDate,UpdateBy,CombosetCode,SubMainExchangeID,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.PromotionCode + "'," +
                           "'" + cInfo.PromotionDetailId + "'," +
                              "'" + cInfo.ProductCode + "'," +
                           "'" + cInfo.Amount + "'," +
                              "'" + cInfo.FlagSubPromotionDetailExchange + "'," +

                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'" + cInfo.CombosetCode + "'," +
                           "'" + cInfo.SubMainExchangeID + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionDetailListReturn> ListProducttionDetailByCriteria(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + pdInfo.ProductCode + "%'";
            }
            if ((pdInfo.ProductName != null) && (pdInfo.ProductName != ""))
            {
                strcond += " and  pro.ProductName like '%" + pdInfo.ProductName + "%'";
            }
            if ((pdInfo.PromotionCode != null) && (pdInfo.PromotionCode != ""))
            {
                strcond += " and  c.PromotionCode like '%" + pdInfo.PromotionCode + "%'";
            }

            if ((pdInfo.Price != null) && (pdInfo.Price != 0))
            {
                strcond += " and  c.Price like '%" + pdInfo.Price + "%'";
            }

            if ((pdInfo.DiscountPercent != null) && (pdInfo.DiscountPercent != 0))
            {
                strcond += " and  c.DiscountPercent like '%" + pdInfo.DiscountPercent + "%'";
            }
            if ((pdInfo.DiscountAmount != null) && (pdInfo.DiscountAmount != 0))
            {
                strcond += " and  c.DiscountAmount like '%" + pdInfo.DiscountAmount + "%'";
            }
            if ((pdInfo.LockAmountFlag != null) && (pdInfo.LockAmountFlag != ""))
            {
                strcond += " and  c.LockAmountFlag like '%" + pdInfo.LockAmountFlag + "%'";
            }
            if ((pdInfo.MerchantCode != null) && (pdInfo.MerchantCode != ""))
            {
                strcond += " and  pro.MerchantCode like '%" + pdInfo.MerchantCode + "%'";
            }
            if ((pdInfo.MerchantName != null) && (pdInfo.MerchantName != ""))
            {
                strcond += " and  pro.MerchantName like '%" + pdInfo.MerchantName + "%'";
            }
            if ((pdInfo.ProductCategoryName != null) && (pdInfo.ProductCategoryName != ""))
            {
                strcond += " and  procat.ProductCategoryName like '%" + pdInfo.ProductCategoryName + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " select c.Id, c.ProductCode, c.PromotionCode, pro.Price, c.DiscountPercent, c.DiscountAmount, c.LockAmountFlag, c.LockCheckbox, c.DefaultAmount, c.ComplementaryAmount, " +
                                " c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, pro.ProductName, pro.MerchantCode, pro.SupplierCode, pro.ProductCategoryCode, pro.ProductBrandCode, " +
                                " pb.ProductBrandName, promo.PromotionName, mech.MerchantName, procat.ProductCategoryName, " +
                                " u.LookupValue AS UnitName, c.ComplementaryFlag from " + dbName + ".dbo.PromotionDetailInfo c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                " left join Merchant mech on mech.MerchantCode = pro.MerchantCode " +
                                " left join Promotion promo on promo.PromotionCode = c.PromotionCode and promo.FlagDelete = 'N' and c.FlagDelete = 'N' " +
                                " left join ProductCategory as procat on pro.ProductCategoryCode = procat.ProductCategoryCode " +
                                " left join ProductBrand pb on pb.ProductBrandCode = pro.ProductBrandCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + pdInfo.rowOFFSet + " ROWS FETCH NEXT " + pdInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionDetail = (from DataRow dr in dt.Rows

                                    select new PromotionDetailListReturn()
                                    {
                                        PromotionDetailInfoId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                        PromotionName = dr["PromotionName"].ToString().Trim(),
                                        ProductName = dr["ProductName"].ToString().Trim(),
                                        UnitName = dr["UnitName"].ToString().Trim(),
                                        Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                        DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                        DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                        LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                        DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                        ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                        CreateBy = dr["CreateBy"].ToString(),
                                        CreateDate = dr["CreateDate"].ToString(),
                                        UpdateBy = dr["UpdateBy"].ToString(),
                                        UpdateDate = dr["UpdateDate"].ToString(),
                                        FlagDelete = dr["FlagDelete"].ToString(),
                                        MerchantName = dr["MerchantName"].ToString(),
                                        MerchantCode = dr["MerchantCode"].ToString(),
                                        ProductCategoryCode = dr["ProductCategoryCode"].ToString(),
                                        ProductCategoryName = dr["ProductCategoryName"].ToString(),
                                        ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                        ProductBrandName = dr["ProductBrandName"].ToString(),
                                        ComplementaryFlag = dr["ComplementaryFlag"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }
    }
    
}

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
    public class PromotionDetailDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdatePromotionDetailInfo(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " ProductCode = '" + pdInfo.ProductCode + "'," +
                            " PromotionCode = '" + pdInfo.PromotionCode + "'," +
                            " Price  = " + pdInfo.Price + "," +
                            " DiscountPercent = " + pdInfo.DiscountPercent + "," +
                            " DiscountAmount = " + pdInfo.DiscountAmount + "," +
                            " LockAmountFlag = '" + pdInfo.LockAmountFlag + "'," +
                            " DefaultAmount = " + pdInfo.DefaultAmount + "," +
                           " ComplementaryAmount = " + pdInfo.ComplementaryAmount + "," +
                           " UpdateBy = '" + pdInfo.UpdateBy + "'," +
                           " PromotionDetailName = '" + pdInfo.PromotionDetailName + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pdInfo.PromotionDetailInfoId + "";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertPromotionDetail(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO PromotionDetailInfo  (ProductCode,PromotionCode, PromotionDetailName, Price,DiscountPercent,DiscountAmount,LockAmountFlag,DefaultAmount,ComplementaryAmount,CreateDate,CreateBy,PointNum,QuotaOnHand,QuotaReserved,QuotaBalance,FlagDelete)" +
                            "VALUES (" +
                           "'" + pdInfo.ProductCode + "'," +
                           "'" + pdInfo.PromotionCode + "'," +
                           "'" + pdInfo.PromotionDetailName + "'," +
                           "'" + pdInfo.Price + "'," +
                           "'" + pdInfo.DiscountPercent + "'," +
                           "'" + pdInfo.DiscountAmount + "'," +
                           "'" + pdInfo.LockAmountFlag + "'," +
                           "'" + pdInfo.DefaultAmount + "'," +
                           "'" + pdInfo.ComplementaryAmount + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pdInfo.CreateBy + "'," + 
                           "'" + pdInfo.PointNum + "'," +
                           pdInfo.QuotaOnHand + "," +
                           pdInfo.QuotaReserved + "," +
                           pdInfo.QuotaBalance + "," +
                           "'" + pdInfo.FlagDelete + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePromtoionDetailInfo(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.PromotionDetailInfo set FlagDelete = 'Y' where Id in (" + pdInfo.PromotionDetailInfoId + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountPromotionDetailByCampaign(PromotionDetailInfo pdInfo)
        {
            string strcond = "";
            int? count = 0;
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
            if ((pdInfo.CampaignCode != null) && (pdInfo.CampaignCode != ""))
            {
                strcond += " and  ca.CampaignCode like '%" + pdInfo.CampaignCode + "%'";
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


            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " select count(c.Id) as countPromotionDetail from " + dbName + ".dbo.PromotionDetailInfo c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                 " left join Promotion promo on promo.PromotionCode = c.PromotionCode and promo.FlagDelete = 'N'" +
                                " left join CampaignPromotion as cp on cp.PromotionCode = promo.PromotionCode " +
                               " left join Campaign as ca on ca.CampaignCode  = cp.CampaignCode and ca.flagdelete = 'N' and ca.Active = 'Y' " +
                               " where c.FlagDelete ='N' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionDetail = (from DataRow dr in dt.Rows

                                    select new PromotionDetailListReturn()
                                    {

                                        countPromotionDetail = Convert.ToInt32(dr["countPromotionDetail"])

                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPromotionDetail.Count > 0)
            {
                count = LPromotionDetail[0].countPromotionDetail;
            }

            return count;
        }

        public List<PromotionDetailListReturn> ListPromotionDetailByCampaign(PromotionDetailInfo pdInfo)
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
            if ((pdInfo.CampaignCode != null) && (pdInfo.CampaignCode != ""))
            {
                strcond += " and  ca.CampaignCode like '%" + pdInfo.CampaignCode + "%'";
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


            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " select ca.CampaignCode,ca.CampaignName,c.Id, c.ProductCode, c.PromotionCode,c.PromotionDetailName, pro.Price, c.DiscountPercent, c.DiscountAmount, c.LockAmountFlag, c.LockCheckbox, c.DefaultAmount, " +
                                 "c.ComplementaryAmount, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, pro.ProductName,pro.ProductDesc,pro.AllergyRemark, promo.PromotionName, " +
                                "  u.LookupValue AS UnitName, c.ComplementaryFlag from " + dbName + ".dbo.PromotionDetailInfo c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                 " left join Promotion promo on promo.PromotionCode = c.PromotionCode and promo.FlagDelete = 'N'" +
                                " left join CampaignPromotion as cp on cp.PromotionCode = promo.PromotionCode " +
                               " left join Campaign as ca on ca.CampaignCode  = cp.CampaignCode and ca.flagdelete = 'N' and ca.Active = 'Y' " +
                               " where c.FlagDelete ='N' and cp.Active='Y' " + strcond;

                strsql += " ORDER BY c.Id ";
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionDetail = (from DataRow dr in dt.Rows

                                    select new PromotionDetailListReturn()
                                    {
                                        PromotionDetailInfoId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        PromotionDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                        PromotionName = dr["PromotionName"].ToString().Trim(),
                                        ProductName = dr["ProductName"].ToString().Trim(),
                                        ProductDesc = dr["ProductDesc"].ToString().Trim(),
                                        AllergyRemark = dr["AllergyRemark"].ToString().Trim(),
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
                                        CampaignCode = dr["CampaignCode"].ToString(),
                                        CampaignName = dr["CampaignName"].ToString(),
                                        ComplementaryFlag = dr["ComplementaryFlag"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
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
                strcond += " and  c.PromotionCode = '" + pdInfo.PromotionCode + "'";
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
            if ((pdInfo.ChannelCode != null) && (pdInfo.ChannelCode != "") && (pdInfo.ChannelCode != "-99"))
            {
                strcond += " and  pro.ChannelCode like '%" + pdInfo.ChannelCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " select c.Id, c.ProductCode, c.PromotionCode, c.Price, c.DiscountPercent, c.DiscountAmount, c.LockAmountFlag, c.LockCheckbox, c.DefaultAmount, c.ComplementaryAmount, " +
                                " c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, pro.ProductName, pro.MerchantCode, pro.SupplierCode, pro.ProductCategoryCode, pro.ProductBrandCode,pro.Lazada_skuId,pro.Lazada_ItemId, " +
                                " pb.ProductBrandName, promo.PromotionName, mech.MerchantName, procat.ProductCategoryName, c.CombosetCode, c.PromotionDetailName, " +
                                " u.LookupValue AS UnitName, c.ComplementaryFlag, c.ChannelCode, cn.ChannelName as ChannelName,c.itemtype,c.PointNum,c.QuotaOnHand,c.QuotaBalance from " + dbName + ".dbo.PromotionDetailInfo c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                " left join Merchant mech on mech.MerchantCode = pro.MerchantCode " +
                                " left join Promotion promo on promo.PromotionCode = c.PromotionCode and promo.FlagDelete = 'N' and c.FlagDelete = 'N' " +
                                " left join ProductCategory as procat on pro.ProductCategoryCode = procat.ProductCategoryCode " +
                                " left join ProductBrand pb on pb.ProductBrandCode = pro.ProductBrandCode " +
                                " left join Channel cn on cn.ChannelCode = pro.ChannelCode " +
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
                                        Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
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
                                        ChannelCode = dr["ChannelCode"].ToString(),
                                        ChannelName = dr["ChannelName"].ToString(),
                                        CombosetCode = dr["CombosetCode"].ToString(),
                                        PromotionDetailName = dr["PromotionDetailName"].ToString(),
                                        PointNum = (dr["PointNum"].ToString() != "") ? Convert.ToInt32(dr["PointNum"]) : 0,
                                        ItemType = dr["ItemType"].ToString(),
                                        Lazada_skuId = dr["Lazada_skuId"].ToString(),
                                        Lazada_ItemId = dr["Lazada_ItemId"].ToString(),
                                        QuotaOnHand = (dr["QuotaOnHand"].ToString() != "") ? Convert.ToInt32(dr["QuotaOnHand"]) : 0,
                                        QuotaBalance = (dr["QuotaBalance"].ToString() != "") ? Convert.ToInt32(dr["QuotaBalance"]) : 0,
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public int? CountPromotionDetailListByCriteria(PromotionDetailInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.ProductCode != null) && (pInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + pInfo.ProductCode + "%'";
            }
            if ((pInfo.ProductName != null) && (pInfo.ProductName != ""))
            {
                strcond += " and  pro.ProductName like '%" + pInfo.ProductName + "%'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  c.PromotionCode = '" + pInfo.PromotionCode + "'";

            }

            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  c.PromotionName like '%" + pInfo.PromotionName + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.CampaignName != null) && (pInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + pInfo.CampaignName + "%'";
            }
            if ((pInfo.LockAmountFlag != null) && (pInfo.LockAmountFlag != ""))
            {
                strcond += " and  c.LockAmountFlag like '%" + pInfo.LockAmountFlag + "%'";
            }
            if ((pInfo.Amount != null) && (pInfo.Amount != 0))
            {
                strcond += " and  c.Amount like '%" + pInfo.Amount + "%'";
            }
            if ((pInfo.DiscountPercent != null) && (pInfo.DiscountPercent != 0))
            {
                strcond += " and  c.DiscountPercent like '%" + pInfo.DiscountPercent + "%'";
            }
            if ((pInfo.DiscountAmount != null) && (pInfo.DiscountAmount != 0))
            {
                strcond += " and  c.DiscountAmount like '%" + pInfo.DiscountAmount + "%'";
            }
            if ((pInfo.ComplementaryAmount != null) && (pInfo.ComplementaryAmount != 0))
            {
                strcond += " and  c.ComplementaryAmount like '%" + pInfo.ComplementaryAmount + "%'";
            }
            if ((pInfo.ChannelCode != null) && (pInfo.ChannelCode != ""))
            {
                strcond += " and  c.ChannelCode like '%" + pInfo.ChannelCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countPromotionDetail from " + dbName + ".dbo.PromotionDetailInfo c " +

                             " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join Promotion promo on promo.PromotionCode = c.PromotionCode " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionDetail = (from DataRow dr in dt.Rows

                                    select new PromotionDetailListReturn()
                                    {
                                        countPromotionDetail = Convert.ToInt32(dr["countPromotionDetail"])

                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LPromotionDetail.Count > 0)
            {
                count = LPromotionDetail[0].countPromotionDetail;
            }

            return count;
        }

        public List<PromotionDetailListReturn> GetPromotiondetailDropDownList(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + pdInfo.ProductCode + "%'";
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
            if ((pdInfo.ProductName != null) && (pdInfo.ProductName != ""))
            {
                strcond += " and  c.ProductName like '%" + pdInfo.ProductName + "%'";
            }
            if ((pdInfo.PromotionName != null) && (pdInfo.PromotionName != ""))
            {
                strcond += " and  c.PromotionName like '%" + pdInfo.PromotionName + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " select c.*, pro.ProductName, promo.PromotionName from " + dbName + ".dbo.PromotionDetailInfo c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join Promotion promo on promo.PromotionCode = c.PromotionCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

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
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public List<PromotionDetailListReturn> GetPromotionCodeDistinctdetailList(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + pdInfo.ProductCode + "%'";
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
            if ((pdInfo.ProductName != null) && (pdInfo.ProductName != ""))
            {
                strcond += " and  c.ProductName like '%" + pdInfo.ProductName + "%'";
            }
            if ((pdInfo.PromotionName != null) && (pdInfo.PromotionName != ""))
            {
                strcond += " and  c.PromotionName like '%" + pdInfo.PromotionName + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT DISTINCT c.PromotionCode, promo.PromotionName from " + dbName + ".dbo.PromotionDetailInfo c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join Promotion promo on promo.PromotionCode = c.PromotionCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.PromotionCode DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionDetail = (from DataRow dr in dt.Rows

                                    select new PromotionDetailListReturn()
                                    {
                                        PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                        PromotionName = dr["PromotionName"].ToString().Trim(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public int UpdatePromotionDetailAmount(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " DefaultAmount = " + pdInfo.DefaultAmount + "," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pdInfo.PromotionDetailInfoId + "";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePromtoionDetailInfoByCode(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.PromotionDetailInfo set FlagDelete = 'Y' where PromotionCode in (" + pdInfo.PromotionCode + ") and FlagDelete = 'N'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateComplementaryFlag(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " ComplementaryFlag = '" + pdInfo.ComplementaryFlag + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pdInfo.PromotionDetailInfoId + "";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdatePromotionDetailDiscount(PromotionDetailInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " DiscountAmount = " + pInfo.DiscountAmount + "," +
                            " DiscountPercent = " + pInfo.DiscountPercent + "," +
                            " LockAmountFlag = '" + pInfo.LockAmountFlag + "'," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where PromotionCode = '" + pInfo.PromotionCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionDetailListReturn> ListProducttionDetailNoPagingByCriteria(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + pdInfo.ProductCode + "%'";
            }
            if ((pdInfo.CampaignCode != null) && (pdInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode like '%" + pdInfo.CampaignCode + "%'";
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
            if ((pdInfo.ChannelCode != null) && (pdInfo.ChannelCode != ""))
            {
                strcond += " and  procat.ChannelCode like '%" + pdInfo.ChannelCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " select c.Id, c.ProductCode, cp.CampaignCode, c.PromotionCode, c.Price, pro.Price AS ProductPrice, c.DiscountPercent, c.DiscountAmount, promo.DiscountPercent as PromotionDiscountPercent, promo.DiscountAmount as PromotionDiscountAmount, promo.LockAmountFlag, promo.LockCheckbox, c.DefaultAmount, c.ComplementaryAmount, " +
                                " c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, pro.ProductName, pro.Unit, pro.MerchantCode, pro.SupplierCode, pro.ProductCategoryCode, pro.ProductBrandCode, pro.ProductWidth, pro.ProductLength, pro.ProductHeigth, pro.PackageWidth, pro.PackageLength, pro.PackageHeigth, pro.Weight," +
                                " pb.ProductBrandName, promo.PromotionName, mech.MerchantName, procat.ProductCategoryName, c.CombosetCode, c.PromotionDetailName, promo.PromotionTypeCode, pt.PromotionTypeName, promo.FreeShipping, promo.GroupPrice, promo.MOQFlag,promo.MinimumQty,promo.MinimumQtyTier2,promo.ProductDiscountPercent,promo.ProductDiscountAmount,promo.ProductDiscountPercentTier2,promo.ProductDiscountAmountTier2, " +
                                " u.LookupValue AS UnitName, c.ComplementaryFlag, c.ChannelCode, cn.ChannelName as ChannelName,c.itemtype from " + dbName + ".dbo.PromotionDetailInfo c " +
                                " inner join Product pro on pro.ProductCode = c.ProductCode and pro.FlagDelete = 'N'" +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                " left join Merchant mech on mech.MerchantCode = pro.MerchantCode " +
                                " left join Promotion promo on promo.PromotionCode = c.PromotionCode and promo.FlagDelete = 'N' and c.FlagDelete = 'N' " +
                                " left join ProductCategory as procat on pro.ProductCategoryCode = procat.ProductCategoryCode " +
                                " left join ProductBrand pb on pb.ProductBrandCode = pro.ProductBrandCode " +
                                " left join Channel cn on cn.ChannelCode = pro.ChannelCode " +
                                " left join CampaignPromotion cp on cp.PromotionCode = promo.PromotionCode " +
                                " left join PromotionType AS pt ON pt.PromotionTypeCode = promo.PromotionTypeCode " +
                                " where c.FlagDelete ='N' and cp.Active ='Y' AND c.QuotaBalance > 0 " + strcond;

                strsql += " ORDER BY c.Id";

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
                                        CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                        PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                        PromotionName = dr["PromotionName"].ToString().Trim(),
                                        ProductName = dr["ProductName"].ToString().Trim(),
                                        Unit = dr["Unit"].ToString().Trim(),
                                        UnitName = dr["UnitName"].ToString().Trim(),
                                        Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                        ProductPrice = (dr["ProductPrice"].ToString() != "") ? Convert.ToDouble(dr["ProductPrice"]) : 0,
                                        DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["DiscountPercent"]) : 0,
                                        DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                        PromotionDiscountPercent = (dr["PromotionDiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["PromotionDiscountPercent"]) : 0,
                                        PromotionDiscountAmount = (dr["PromotionDiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["PromotionDiscountAmount"]) : 0,
                                        LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                        LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                        DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                        ComplementaryAmount = (dr["ComplementaryAmount"].ToString() != "") ? Convert.ToInt32(dr["ComplementaryAmount"]) : 0,
                                        CreateBy = dr["CreateBy"].ToString(),
                                        CreateDate = dr["CreateDate"].ToString(),
                                        UpdateBy = dr["UpdateBy"].ToString(),
                                        UpdateDate = dr["UpdateDate"].ToString(),
                                        FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                        MerchantName = dr["MerchantName"].ToString(),
                                        MerchantCode = dr["MerchantCode"].ToString(),
                                        ProductCategoryCode = dr["ProductCategoryCode"].ToString(),
                                        ProductCategoryName = dr["ProductCategoryName"].ToString(),
                                        ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                        ProductBrandName = dr["ProductBrandName"].ToString(),
                                        ComplementaryFlag = dr["ComplementaryFlag"].ToString(),
                                        ChannelCode = dr["ChannelCode"].ToString(),
                                        ChannelName = dr["ChannelName"].ToString(),
                                        CombosetCode = dr["CombosetCode"].ToString(),
                                        PromotionDetailName = dr["PromotionDetailName"].ToString(),
                                        PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                        PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                        FreeShippingCode = dr["FreeShipping"].ToString().Trim(),
                                        MOQFlag = dr["MOQFlag"].ToString().Trim(),
                                        GroupPrice = (dr["GroupPrice"].ToString() != "") ? Convert.ToDouble(dr["GroupPrice"]) : 0,
                                        MinimumQty = (dr["MinimumQty"].ToString() != "") ? Convert.ToInt32(dr["MinimumQty"]) : 0,
                                        MinimumQtyTier2 = (dr["MinimumQtyTier2"].ToString() != "") ? Convert.ToInt32(dr["MinimumQtyTier2"]) : 0,
                                        ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountPercent"]) : 0,
                                        ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountAmount"]) : 0,
                                        ProductDiscountPercentTier2 = (dr["ProductDiscountPercentTier2"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountPercentTier2"]) : 0,
                                        ProductDiscountAmountTier2 = (dr["ProductDiscountAmountTier2"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountAmountTier2"]) : 0,
                                        ProductWidth = (dr["ProductWidth"].ToString() != "") ? float.Parse(dr["ProductWidth"].ToString()) : 0,
                                        ProductLength = (dr["ProductLength"].ToString() != "") ? float.Parse(dr["ProductLength"].ToString()) : 0,
                                        ProductHeigth = (dr["ProductHeigth"].ToString() != "") ? float.Parse(dr["ProductHeigth"].ToString()) : 0,
                                        PackageWidth = (dr["PackageWidth"].ToString() != "") ? float.Parse(dr["PackageWidth"].ToString()) : 0,
                                        PackageLength = (dr["PackageLength"].ToString() != "") ? float.Parse(dr["PackageLength"].ToString()) : 0,
                                        PackageHeigth = (dr["PackageHeigth"].ToString() != "") ? float.Parse(dr["PackageHeigth"].ToString()) : 0,
                                        Weight = (dr["Weight"].ToString() != "") ? float.Parse(dr["Weight"].ToString()) : 0,
                                        ItemType= dr["ItemType"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public int UpdatePromotionDetailName(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " PromotionDetailName = '" + pdInfo.PromotionDetailName + "'," +
                           " UpdateBy = '" + pdInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where PromotionCode = '" + pdInfo.PromotionCode + "'";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePromtoionDetailInfoByIdString(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.PromotionDetailInfo set FlagDelete = 'Y' where Id in (" + pdInfo.PromotionCode + ") ";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionDetailListReturn> ListPromotionDetailRecipeinCampaignProductTakeOrderCriteria(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.CampaignCode != null) && (pdInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode like '%" + pdInfo.CampaignCode + "%'";
            }
            

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT distinct p.ProductCode, cp.CampaignCode, c.CampaignName, d.Id, cp.PromotionCode, pr.PromotionName, d.Price, d.DiscountPercent, " +
                                " d.DiscountAmount, d.LockAmountFlag, d.LockCheckbox, d.DefaultAmount, d.ComplementaryAmount, p.ProductName, " +
                                " d.CreateDate, d.CreateBy, d.UpdateDate, d.UpdateBy, d.FlagDelete, u.LookupValue AS UnitName, d.ComplementaryFlag " +
                                " FROM            Product AS p LEFT OUTER JOIN " +
                                " PromotionDetailInfo AS d ON d.ProductCode = p.ProductCode LEFT OUTER JOIN " +
                                " CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                " Campaign AS c ON c.CampaignCode = cp.CampaignCode LEFT OUTER JOIN " +
                                " Promotion AS pr ON pr.PromotionCode = cp.PromotionCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = p.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                                " ProductMapRecipe AS pm ON pm.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                " Recipe AS r ON r.RecipeCode = pm.RecipeCode " +
                                " WHERE (p.FlagDelete = 'N') and (cp.Active = 'Y') AND (c.FlagDelete = 'N') ";
                strsql = strsql + strcond;

                strsql = strsql + " and ((r.RecipeName LIKE '% " + pdInfo.RecipeName + "%') OR (p.ProductName LIKE '%" + pdInfo.RecipeName + "%') OR (p.ProductCode LIKE '%" + pdInfo.RecipeName + "%'))";

                

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
                                        

                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public List<PromotionDetailListReturn> ListPromotionDetailbyProductinRecipeCampaignProductTakeOrderCriteria(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.CampaignCode != null) && (pdInfo.CampaignCode != ""))
            {
                strcond += " and  ca.CampaignCode like '%" + pdInfo.CampaignCode + "%'";
            }
            if ((pdInfo.ProductName != null) && (pdInfo.ProductName != ""))
            {
                strcond += " and  pro.ProductName like '%" + pdInfo.ProductName + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT ca.CampaignCode, ca.CampaignName, c.Id, c.ProductCode, c.PromotionCode, c.PromotionDetailName, pro.Price, c.DiscountPercent, " +
                                " c.DiscountAmount, c.LockAmountFlag, c.LockCheckbox, c.DefaultAmount, c.ComplementaryAmount, c.CreateDate, c.CreateBy, c.UpdateDate, " +
                                " c.UpdateBy, c.FlagDelete, pro.ProductName, promo.PromotionName, u.LookupValue AS UnitName, c.ComplementaryFlag " +
                                " FROM            PromotionDetailInfo AS c LEFT OUTER JOIN " +
                                " Product AS pro ON pro.ProductCode = c.ProductCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = pro.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                                " Promotion AS promo ON promo.PromotionCode = c.PromotionCode AND promo.FlagDelete = 'N' LEFT OUTER JOIN " +
                                " CampaignPromotion AS cp ON cp.PromotionCode = promo.PromotionCode LEFT OUTER JOIN " +
                                " Campaign AS ca ON ca.CampaignCode = cp.CampaignCode AND ca.FlagDelete = 'N' AND ca.Active = 'Y' " +
                                " WHERE (c.FlagDelete = 'N') AND (c.FlagDelete = 'N') ";

                strsql = strsql + strcond;

                strsql = strsql + " ORDER BY c.Id desc";

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

                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public int InsertPromotionDetailCombo(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO dbo.PromotionDetailInfo  (PromotionCode, PromotionDetailName, Price, CombosetCode, ProductCode,StartDatePromotionCombo,EndDatePromotionCombo,FlagActive, CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pdInfo.PromotionCode + "'," +
                           "'" + pdInfo.PromotionDetailName + "'," +
                           "" + pdInfo.Price + "," +
                           "'" + pdInfo.CombosetCode + "'," +
                           "'" + pdInfo.ProductCode + "'," +
                            "'" + ((pdInfo.StartDatePromotionCombo != null) ? ("convert(datetime, '" + pdInfo.StartDatePromotionCombo + "', 103)") : null) + "'," +
                             "'" + ((pdInfo.EndDatePromotionCombo != null) ? ("convert(datetime, '" + pdInfo.EndDatePromotionCombo + "', 103)") : null) + "'," +
                            "'" + pdInfo.FlagActive + "'," +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pdInfo.CreateBy + "'," +
                           "'" + pdInfo.FlagDelete + "'" +

                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdatePromotionDetailCombo(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " PromotionCode = '" + pdInfo.PromotionCode + "'," +
                            " ProductCode = '" + pdInfo.ProductCode + "'," +
                           " PromotionDetailName = '" + pdInfo.PromotionDetailName + "'," +
                            " Price  = " + pdInfo.Price + "," +
                            " CombosetCode = '" + pdInfo.CombosetCode + "'," +
                             " StartDatePromotionCombo = '" + ((pdInfo.StartDatePromotionCombo != null) ? DateTime.Parse(pdInfo.StartDatePromotionCombo).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +

                            " EndDatePromotionCombo = '" + ((pdInfo.EndDatePromotionCombo != null) ? DateTime.Parse(pdInfo.EndDatePromotionCombo).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +
                            " UpdateBy = '" + pdInfo.UpdateBy + "'," +
                           " FlagActive = '" + pdInfo.FlagActive + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pdInfo.PromotionDetailInfoId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionDetailListReturn> PromotionDetailfromProductTop5(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  d.ProductCode = '" + pdInfo.ProductCode + "'";
            }

            if ((pdInfo.CampaignCategoryCode != null) && (pdInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  c.CampaignCategory = '" + pdInfo.CampaignCategoryCode + "'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT d.Id,d.PromotionCode,d.ProductCode,p.ProductName,d.DiscountAmount,d.DiscountPercent,d.Price,cp.CampaignCode,l.LookupValue AS UnitName, " +
                                " c.CampaignName,c.CampaignCategory,ct.CamCate_name,pr.PromotionName,d.LockCheckbox,d.LockAmountFlag,p.ProductDesc FROM PromotionDetailInfo AS d LEFT OUTER JOIN " +
                                " Product AS p ON p.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                " CampaignPromotion AS cp ON cp.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                " Campaign AS c ON c.CampaignCode = cp.CampaignCode LEFT OUTER JOIN " +
                                " Promotion AS pr ON pr.PromotionCode = cp.PromotionCode LEFT OUTER JOIN " +
                                " CampaignCategory AS ct ON ct.CampaignCategoryCode = c.CampaignCategory LEFT OUTER JOIN " +
                                " Lookup AS l ON l.LookupCode = p.Unit AND l.LookupType = 'UNIT' " +
                                " WHERE (d.FlagDelete = 'N') AND (p.FlagDelete = 'N') AND (cp.Active = 'Y') AND (c.FlagDelete = 'N') AND (c.Active = 'Y') AND (p.FlagDelete = 'N') " +
                                strcond;

                strsql += " ORDER BY d.Id ";

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
                                        ProductDesc = dr["ProductDesc"].ToString().Trim(),
                                        UnitName = dr["UnitName"].ToString().Trim(),
                                        Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                        DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                        DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                        LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                        LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                        CampaignCode = dr["CampaignCode"].ToString(),
                                        CampaignName = dr["CampaignName"].ToString(),
                                        CampaignCategoryCode = dr["CampaignCategory"].ToString(),
                                        CampaignCategoryName = dr["CamCate_name"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public List<PromotionDetailListReturn> PromotionDetailfromLatestOrderbyCustomer(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.CustomerCode != null) && (pdInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode = '" + pdInfo.CustomerCode + "'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT o.Id, d.PromotionDetailId, o.CustomerCode, d.OrderCode, d.ProductCode, pd.ProductName, pd.ProductDesc, d.PromotionCode, pm.PromotionName, pd.Unit, u.LookupValue AS UnitName, CustomerCode, d.FlagCombo," +
                                " d.Price, d.Amount, pt.DiscountPercent, pt.DiscountAmount, pt.LockAmountFlag, pt.LockCheckbox, d.CampaignCode, c.CampaignName, o.CampaignCategoryCode, cg.CamCate_name, pt.CombosetCode, pt.PromotionDetailName, o.RunningNo FROM OrderDetail AS d LEFT OUTER JOIN " +
                                " OrderInfo AS o ON o.OrderCode = d.OrderCode LEFT OUTER JOIN " +
                                " Promotion AS pm ON pm.PromotionCode = d.PromotionCode LEFT OUTER JOIN " +
                                " Product AS pd ON pd.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = pd.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                                " PromotionDetailInfo AS pt ON pt.Id = d.Id LEFT OUTER JOIN " +
                                " Campaign AS c ON c.CampaignCode = d.CampaignCode LEFT OUTER JOIN " +
                                " CampaignCategory AS cg ON cg.CampaignCategoryCode = o.CampaignCategoryCode" +
                                " WHERE (pm.FlagDelete = 'N') AND (pd.FlagDelete = 'N') AND (c.FlagDelete = 'N') AND (c.Active = 'Y')  AND (cg.FlagDelete = 'N')" +
                                strcond;

                strsql += " ORDER BY d.Id desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotionDetail = (from DataRow dr in dt.Rows

                                    select new PromotionDetailListReturn()
                                    {
                                        PromotionDetailInfoId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                        PromotionName = dr["PromotionName"].ToString().Trim(),
                                        ProductName = dr["ProductName"].ToString().Trim(),
                                        ProductDesc = dr["ProductDesc"].ToString().Trim(),
                                        Unit = dr["Unit"].ToString().Trim(),
                                        UnitName = dr["UnitName"].ToString().Trim(),
                                        Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                        Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                        DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                        DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                        LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                        LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                        CampaignCode = dr["CampaignCode"].ToString(),
                                        CampaignName = dr["CampaignName"].ToString(),
                                        CampaignCategoryCode = dr["CampaignCategoryCode"].ToString(),
                                        CampaignCategoryName = dr["CamCate_name"].ToString(),
                                        CombosetCode = dr["CombosetCode"].ToString(),
                                        ComboName = dr["PromotionDetailName"].ToString(),
                                        RunningNo = dr["RunningNo"].ToString(),
                                        OrderCode = dr["OrderCode"].ToString(),
                                        CustomerCode = dr["CustomerCode"].ToString(),
                                        FlagCombo = dr["FlagCombo"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }
        public int DeleteCombosetDetailInfoByIdString(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.PromotionDetailInfo set FlagDelete = 'Y' where Combosetcode in (" + pdInfo.PromotionCode + ") ";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateItemTypePromotionDetailInfoByIdString(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.PromotionDetailInfo set ItemType = '1' where id in (" + pdInfo.PromotionDetailIDList + ") and promotioncode='" + pdInfo.PromotionCode + "' ";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

        

            string strsqlsub = "Update " + dbName + ".dbo.PromotionDetailInfo set ItemType = '2' where id not in (" + pdInfo.PromotionDetailIDList + ") and promotioncode='" + pdInfo.PromotionCode + "' ";

            com.CommandText = strsqlsub;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdatePromoDetailInfoByProductCode(PromotionDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.PromotionDetailInfo set " +
                            " ProductCode = '" + pdInfo.ProductCode + "'," +
                            " Price  = " + pdInfo.Price + "," +
                           " UpdateBy = '" + pdInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where ProductCode =" + "'" + pdInfo.ProductCode + "'" + "and FlagDelete = 'N'";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionDetailListReturn> CheckPromotionComplementaryTypebyPromotionDetailInfoID(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.PromotionDetailInfoId != null) && (pdInfo.PromotionDetailInfoId != 0))
            {
                strcond += " and  pd.Id = " + pdInfo.PromotionDetailInfoId;
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT pd.Id, pd.PromotionCode, pd.ProductCode, p.ComplementaryFlag from " + dbName + ".dbo.PromotionDetailInfo pd " +
                                " left join Promotion p on p.PromotionCode = pd.PromotionCode " +
                                " where pd.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY pd.Id DESC ";

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
                                        ComplementaryFlag = dr["ComplementaryFlag"].ToString().Trim(),

                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotionDetail;
        }

        public List<PromotionDetailListReturn> GetPromotiondetailInfoNoPagingbyCriteria(PromotionDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  pd.ProductCode like '%" + pdInfo.ProductCode + "%'";
            }
            if ((pdInfo.ProductName != null) && (pdInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + pdInfo.ProductName + "%'";
            }
            if ((pdInfo.PromotionCode != null) && (pdInfo.PromotionCode != ""))
            {
                strcond += " and  pd.PromotionCode like '%" + pdInfo.PromotionCode + "%'";
            }

            DataTable dt = new DataTable();
            var LPromotionDetail = new List<PromotionDetailListReturn>();

            try
            {
                string strsql = " select " +
                                " pd.Id, pd.ProductCode, pd.PromotionCode, pd.PromotionDetailName, pd.CombosetCode, pd.Price, pd.DiscountPercent, pd.DiscountAmount, pd.LockAmountFlag, " +
                                " pd.ComplementaryFlag, pd.LockCheckbox, pd.DefaultAmount, pd.ComplementaryAmount, pd.CreateDate, pd.CreateBy, " +
                                " pd.UpdateDate, pd.UpdateBy, pd.FlagDelete, pd.ChannelCode, pd.StartDatePromotionCombo, pd.EndDatePromotionCombo, pd.FlagActive, pd.ItemType, p.ProductName" +
                                " from PromotionDetailInfo AS pd" +
                                " LEFT OUTER JOIN Product AS p ON p.ProductCode = pd.ProductCode " +
                                " where pd.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY pd.Id";

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
                                        ProductName = dr["ProductName"].ToString().Trim(),
                                        DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                        DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["DiscountPercent"]) : 0,
                                        DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
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

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
    public class PromotionDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdatePromotion(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Promotion set " +
                            " PromotionCode = '" + pInfo.PromotionCode + "'," +
                            " PromotionName = '" + pInfo.PromotionName + "'," +
                            " PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'," +
                            " PromotionDesc = '" + pInfo.PromotionDesc + "'," +
                            " FreeShipping = '" + pInfo.FreeShippingCode + "'," +
                            " PromotionStatus = '" + pInfo.PromotionStatusCode + "'," +
                            " MOQFlag = '" + pInfo.MOQFlag + "'," +
                            " MinimumQtyTier2 = " + ((pInfo.MinimumQtyTier2.ToString() == "") ? 0 : pInfo.MinimumQtyTier2) + "," +
                            " MinimumQty = " + ((pInfo.MinimumQty.ToString() == "") ? 0 : pInfo.MinimumQty) + "," +
                            " DiscountAmount = " +((pInfo.DiscountAmount.ToString() == "") ? 0 : pInfo.DiscountAmount) + "," +
                            " ProductDiscountPercent = " +((pInfo.ProductDiscountPercent.ToString() == "") ? 0 : pInfo.ProductDiscountPercent) + "," +
                            " ProductDiscountPercentTier2 = " + ((pInfo.ProductDiscountPercentTier2.ToString() == "") ? 0 : pInfo.ProductDiscountPercentTier2) + "," +
                            " ProductDiscountAmount = " + ((pInfo.ProductDiscountAmount.ToString() == "") ? 0 : pInfo.ProductDiscountAmount) + "," +
                            " ProductDiscountAmountTier2 = " + ((pInfo.ProductDiscountAmountTier2.ToString() == "") ? 0 : pInfo.ProductDiscountAmountTier2) + "," +
                            " DiscountPercent = " +((pInfo.DiscountPercent.ToString() == "") ? 0 : pInfo.DiscountPercent) + "," +
                            " LockAmountFlag = '" + pInfo.LockAmountFlag + "'," +
                            " LockCheckbox = '" + pInfo.LockCheckbox + "'," +
                            " DefaultAmount = " +((pInfo.DefaultAmount.ToString() == "") ? 0 : pInfo.DefaultAmount) + "," +
                            " MinimumTotPrice = " +((pInfo.MinimumTotPrice.ToString() == "") ? 0 : pInfo.MinimumTotPrice) + "," +
                            " RedeemFlag = '" + pInfo.RedeemFlag + "'," +
                            " ComplementaryFlag = '" + pInfo.ComplementaryFlag + "'," +
                            " ComplementaryChangeAble = '" + pInfo.ComplementaryChangeAble + "'," +
                            " PromotionLevel = '" + pInfo.PromotionLevel + "'," +
                            " GroupPrice = " + ((pInfo.GroupPrice.ToString() == "") ? 0 : pInfo.GroupPrice) + "," +
                            " PicturePromotionUrl = '" + pInfo.PicturePromotionUrl + "'," +
                            " CombosetFlag = '" + pInfo.CombosetFlag + "'," +
                            " StartDate = CONVERT(DATETIME, '" + pInfo.StartDate + "', 103)," +
                            " EndDate = CONVERT(DATETIME, '" + pInfo.EndDate + "', 103)," +
                            " ProductBrandCode = '" + pInfo.ProductBrandCode + "'," +
                            " CombosetName = '" + pInfo.CombosetName + "'," +

                            " ProPoint = '" + pInfo.Propoint + "'," +
                            " PointType = '" + pInfo.PointType + "'," +
                            " CompanyCode = '" + pInfo.CompanyCode + "'," +
                            " FlagPatent = '" + pInfo.FlagPatent + "'," +
                            " PatentAmount = '" + pInfo.PatentAmount + "'," +
                            " DiscountCode = '" + pInfo.DiscountCode + "'," +
                            " PointRangeCode = '" + pInfo.PointRangeCode + "'," +

                            " PromotionTagCode = '" + pInfo.PromotionTagCode + "'," +
                            " PromotionTagName = '" + pInfo.PromotionTagName + "'," +
                            " ProductTagCode = '" + pInfo.ProductTagCode + "'," +
                            " ProductTagName = '" + pInfo.ProductTagName + "'," +

                            " ApplyScope = '" + pInfo.ApplyScope + "'," +
                            " CriteriaType = '" + pInfo.CriteriaType + "'," +
                            " DiscountType = '" + pInfo.DiscountType + "'," +
                            " OrderNumbers = '" + pInfo.OrderNumbers + "'," +
                            " CriteriaValueTier1 = '" + pInfo.CriteriaValueTier1 + "'," +
                            " CriteriaValueTier2 = '" + pInfo.CriteriaValueTier2 + "'," +
                            " CriteriaValueTier3 = '" + pInfo.CriteriaValueTier3 + "'," +
                            " DiscountValueTier1 = '" + pInfo.DiscountValueTier1 + "'," +
                            " DiscountValueTier2 = '" + pInfo.DiscountValueTier2 + "'," +
                            " DiscountValueTier3 = '" + pInfo.DiscountValueTier3 + "'," +

                            " PromotionFlashSaleStartDate = CONVERT(DATETIME, '" + pInfo.PromotionFlashSaleStartDate + "', 103)," +
                            " PromotionFlashSaleEndDate = CONVERT(DATETIME, '" + pInfo.PromotionFlashSaleEndDate + "', 103)," +

                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pInfo.PromotionId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateLazadaPromotion(PromotionInfo pInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.Promotion set " +
                            " LazadaPromotionId ='" + pInfo.LazadaPromotionId + "'," +
                            " LazadaPromotionStatus = " + ((pInfo.LazadaPromotionStatus.ToString() == "") ? 0 : pInfo.LazadaPromotionStatus) + "," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + pInfo.UpdateBy + "'" +
                            " where Id ='" + pInfo.PromotionId + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int DeletePromotion(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Promotion set FlagDelete = 'Y' where Id in (" + pInfo.PromotionId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertPromotion(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Promotion  (MerchantCode,PromotionCode,PromotionName,PromotionDesc,PromotionTypeCode,CreateDate,CreateBy,FlagDelete, FreeShipping, PromotionStatus," +
                            " MOQFlag, MinimumQty,MinimumQtyTier2, DiscountAmount, DiscountPercent, LockAmountFlag, LockCheckbox, DefaultAmount, ProductDiscountAmount,ProductDiscountAmountTier2, ProductDiscountPercent,ProductDiscountPercentTier2, MinimumTotPrice," + 
                            " RedeemFlag, ComplementaryFlag, PromotionLevel, GroupPrice, PicturePromotionUrl, CombosetName, CombosetFlag, StartDate, EndDate, ComplementaryChangeAble, Bu, Levels," +
                            " RequestByEmpCode, RequestDate, Status, FinishFlag,ProPoint,PointType,CompanyCode,FlagPatent,PatentAmount,DiscountCode,PointRangeCode,FlagPointType, " +
                            " ApplyScope,CriteriaType,DiscountType,OrderNumbers,CriteriaValueTier1,CriteriaValueTier2,CriteriaValueTier3,DiscountValueTier1,DiscountValueTier2,DiscountValueTier3, ProductBrandCode,PromotionTagCode,PromotionTagName,ProductTagCode,ProductTagName,PromotionFlashSaleStartDate,PromotionFlashSaleEndDate,LazadaPromotionStatus)" +
                            "VALUES (" +
                           "'" + pInfo.MerchantCode + "'," +
                           "'" + pInfo.PromotionCode + "'," +
                           "'" + pInfo.PromotionName + "'," +
                           "'" + pInfo.PromotionDesc + "'," +
                           "'" + pInfo.PromotionTypeCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           
                           "'" + pInfo.FlagDelete + "'," +
                           "'" + pInfo.FreeShippingCode + "'," +
                           "'" + pInfo.PromotionStatusCode + "'," +
                           "'" + pInfo.MOQFlag + "'," +
                           ((pInfo.MinimumQty.ToString() == "") ? 0 : pInfo.MinimumQty) + "," +
                           ((pInfo.MinimumQtyTier2.ToString() == "") ? 0 : pInfo.MinimumQtyTier2) + "," +
                           ((pInfo.DiscountAmount.ToString() == "") ? 0 : pInfo.DiscountAmount) + "," +
                           ((pInfo.DiscountPercent.ToString() == "") ? 0 : pInfo.DiscountPercent) + "," +
                           "'" + pInfo.LockAmountFlag + "'," +
                           "'" + pInfo.LockCheckbox + "'," +
                           ((pInfo.DefaultAmount.ToString() == "") ? 0 : pInfo.DefaultAmount) + "," +
                           ((pInfo.ProductDiscountAmount.ToString() == "") ? 0 : pInfo.ProductDiscountAmount) + "," +
                           ((pInfo.ProductDiscountAmountTier2.ToString() == "") ? 0 : pInfo.ProductDiscountAmountTier2) + "," +
                           ((pInfo.ProductDiscountPercent.ToString() == "") ? 0 : pInfo.ProductDiscountPercent) + "," +
                            ((pInfo.ProductDiscountPercentTier2.ToString() == "") ? 0 : pInfo.ProductDiscountPercentTier2) + "," +
                           ((pInfo.MinimumTotPrice.ToString() == "") ? 0 : pInfo.MinimumTotPrice) + "," +
                            "'" + pInfo.RedeemFlag + "'," +
                            "'" + pInfo.ComplementaryFlag + "'," +
                            "'" + pInfo.PromotionLevel + "'," +
                           ((pInfo.GroupPrice.ToString() == "") ? 0 : pInfo.GroupPrice) + "," +
                           "'" + pInfo.PicturePromotionUrl + "'," +
                           "'" + pInfo.CombosetName + "'," +
                           "'" + pInfo.CombosetFlag + "', " +
                           "CONVERT(DATETIME, '" + pInfo.StartDate + "',103), " +
                           "CONVERT(DATETIME, '" + pInfo.EndDate + "',103), " +
                            "'" + pInfo.ComplementaryChangeAble + "'," +
                            "'" + pInfo.Bu + "'," +
                            "'" + pInfo.levels + "'," +
                            "'" + pInfo.RequestByEmpCode + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            "'" + pInfo.wfStatus + "'," +
                            "'" + pInfo.wfFinishFlag + "'," +
                            "'" + pInfo.Propoint + "'," +
                            "'" + pInfo.PointType + "'," +
                            "'" + pInfo.CompanyCode + "'," +
                            "'" + pInfo.FlagPatent + "'," +
                            "'" + pInfo.PatentAmount + "'," +
                            "'" + pInfo.DiscountCode + "'," +
                            "'" + pInfo.PointRangeCode + "'," +
                            "'" + pInfo.FlagPointType + "'," +

                            "'" + pInfo.ApplyScope + "'," +
                            "'" + pInfo.CriteriaType + "'," +
                            "'" + pInfo.DiscountType + "'," +
                            ((pInfo.OrderNumbers.ToString() == "") ? 0 : pInfo.OrderNumbers) + "," +
                            "'" + pInfo.CriteriaValueTier1 + "'," +
                            "'" + pInfo.CriteriaValueTier2 + "'," +
                            "'" + pInfo.CriteriaValueTier3 + "'," +
                            "'" + pInfo.DiscountValueTier1 + "'," +
                            "'" + pInfo.DiscountValueTier2 + "'," +
                            "'" + pInfo.DiscountValueTier3 + "'," +
                           "'" + pInfo.ProductBrandCode + "'," +
                           "'" + pInfo.PromotionTagCode + "'," +
                           "'" + pInfo.PromotionTagName + "'," +
                           "'" + pInfo.ProductTagCode + "'," +
                           "'" + pInfo.ProductTagName + "'," +
                            "CONVERT(DATETIME, '" + pInfo.PromotionFlashSaleStartDate + "',103), " +
                            "CONVERT(DATETIME, '" + pInfo.PromotionFlashSaleEndDate + "',103), " +
                           ((pInfo.LazadaPromotionStatus.ToString() == "") ? 0 : pInfo.LazadaPromotionStatus) + "" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionListReturn> ListPromotionNoPagingByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode = '" + pInfo.PromotionCode + "'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }

            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != "") && (pInfo.PromotionTypeCode != "-99"))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != "") && (pInfo.ProductBrandCode != "-99"))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select pb.ProductBrandName, t.PromotionTypeName,p.*,c.CompanyNameEN, fs.LookupValue as FreeShip, st.LookupValue as PromotionStatusName" +
                                ", pl.LookupValue as PromotionLevelName " +
                                ", ld.LookupValue as DiscountTypeName , ct.LookupValue as CriteriaTypeName , ap.LookupValue as ApplyScopeName " +
                                " from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode and t.FlagDelete='N' " +
                                " left join LookUp fs on fs.LookupCode =  p.FreeShipping and fs.LookupType='FREESHIPPING' " +
                                " left join Company c on c.CompanyCode =  p.CompanyCode " +
                                " left join LookUp st on st.LookupCode =  p.PromotionStatus  and st.LookupType='PROMOSTATUS' " +
                                " left join LookUp pl on pl.LookupCode =  p.PromotionLevel  and pl.LookupType='PROMOTIONLEVEL' " +
                                " left join LookUp ld on ld.LookupCode =  p.DiscountType  and ld.LookupType='LAZADADISCOUNTTYPE' " +
                                " left join LookUp ct on ct.LookupCode =  p.CriteriaType  and ct.LookupType='CRITERIATYPE' " +
                                " left join LookUp ap on ap.LookupCode =  p.ApplyScope  and ap.LookupType='APPLYSCOPE' " +
                                " left join ProductBrand pb on pb.ProductBrandCode =  p.ProductBrandCode " +
                               " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  PromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                  PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["CreateDate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  FreeShippingCode = dr["FreeShipping"].ToString(),
                                  FreeShippingName = dr["FreeShip"].ToString(),
                                  PromotionStatusCode = dr["PromotionStatus"].ToString(),
                                  PromotionStatusName = dr["PromotionStatusName"].ToString(),
                                  MOQFlag = dr["MOQFlag"].ToString(),
                                  MinimumQty = (dr["MinimumQty"].ToString() != "") ? Convert.ToInt32(dr["MinimumQty"]) : 0,
                                  DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                  DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["DiscountPercent"]) : 0,
                                  LockAmountFlag = dr["LockAmountFlag"].ToString(),
                                  LockCheckbox = dr["LockCheckbox"].ToString(),
                                  DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                  ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountAmount"]) : 0,
                                  ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountPercent"]) : 0,
                                  MinimumTotPrice = (dr["MinimumTotPrice"].ToString() != "") ? Convert.ToInt32(dr["MinimumTotPrice"]) : 0,
                                  GroupPrice = (dr["GroupPrice"].ToString() != "") ? Convert.ToInt32(dr["GroupPrice"]) : 0,
                                  PromotionLevel = dr["PromotionLevel"].ToString(),
                                  ComplementaryFlag = dr["ComplementaryFlag"].ToString(),
                                  RedeemFlag = dr["RedeemFlag"].ToString(),
                                  CombosetFlag = dr["CombosetFlag"].ToString(),
                                  CombosetName = dr["CombosetName"].ToString(),
                                  CompanyCode = dr["CompanyCode"].ToString(),
                                  CompanyNameEN = dr["CompanyNameEN"].ToString(),
                                  
                                  ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                  ProductBrandName = dr["ProductBrandName"].ToString(),
                                  PromotionLevelName = dr["PromotionLevelName"].ToString(),
                                  PromotionTagCode = dr["PromotionTagCode"].ToString(),
                                  ProductTagCode = dr["ProductTagCode"].ToString(),
                                  StartDate = dr["StartDate"].ToString(),
                                  EndDate = dr["EndDate"].ToString(),

                                  ApplyScope = dr["ApplyScope"].ToString(),
                                  ApplyScopeName = dr["ApplyScopeName"].ToString(),
                                  CriteriaType = dr["CriteriaType"].ToString(),
                                  CriteriaTypeName = dr["CriteriaTypeName"].ToString(),
                                  DiscountType = dr["DiscountType"].ToString(),
                                  DiscountTypeName = dr["DiscountTypeName"].ToString(),
                                  OrderNumbers = (dr["OrderNumbers"].ToString() != "") ? Convert.ToInt32(dr["OrderNumbers"]) : (int?)null,
                                  CriteriaValueTier1 = dr["CriteriaValueTier1"].ToString(),
                                  CriteriaValueTier2 = dr["CriteriaValueTier2"].ToString(),
                                  CriteriaValueTier3 = dr["CriteriaValueTier3"].ToString(),
                                  DiscountValueTier1 = dr["DiscountValueTier1"].ToString(),
                                  DiscountValueTier2 = dr["DiscountValueTier2"].ToString(),
                                  DiscountValueTier3 = dr["DiscountValueTier3"].ToString(),
                                  LazadaPromotionId = dr["LazadaPromotionId"].ToString(),
                                  LazadaPromotionStatus = (dr["LazadaPromotionStatus"].ToString() != "") ? Convert.ToInt32(dr["LazadaPromotionStatus"]) : 0,

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public List<PromotionListReturn> ListPromotionList(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            
            
            //trimmed string make code cannot pass this condition
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
            }
            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != "") && (pInfo.PromotionTypeCode != "-99"))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo == "") || (pInfo.StartDateTo == null)))
            {
                strcond += " AND p.StartDate >= CONVERT(DATETIME, '" + pInfo.StartDate + "',103)";
            }
            if (((pInfo.StartDate == "") && (pInfo.StartDate == null)) && ((pInfo.StartDateTo != "") || (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate <= CONVERT(DATETIME, '" + pInfo.StartDateTo + "',103)";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo == "") || (pInfo.EndDateTo == null)))
            {
                strcond += " AND p.EndDate >= CONVERT(DATETIME, '" + pInfo.EndDate + "',103)";
            }
            if (((pInfo.EndDate == "") && (pInfo.EndDate == null)) && ((pInfo.EndDateTo != "") || (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate <= CONVERT(DATETIME, '" + pInfo.EndDateTo + "',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != "") && (pInfo.ProductBrandCode != "-99"))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.ComplementaryFlag != null) && (pInfo.ComplementaryFlag != ""))
            {
                strcond += " and  p.ComplementaryFlag = '" + pInfo.ComplementaryFlag + "'";
            }
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }
            if ((pInfo.Bu != null) && (pInfo.Bu != ""))
            {
                strcond += " and  p.Bu = '" + pInfo.Bu + "'";
            }
            if ((pInfo.levels != null) && (pInfo.levels != 0))
            {
                strcond += " and  p.levels = '" + pInfo.levels + "'";
            }
            //View by Admin
            if ((pInfo.RoleCode != "ADMIN"))
            {
                if ((pInfo.RequestByEmpCode != null) && (pInfo.RequestByEmpCode != ""))
                {
                    strcond += " and  p.RequestByEmpCode = '" + pInfo.RequestByEmpCode + "'";
                }
            }
            


            if ((pInfo.wfFinishFlag != null) && (pInfo.wfFinishFlag != ""))
            {
                strcond += " and  p.FinishFlag = '" + pInfo.wfFinishFlag + "'";
            }
            if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
            {
                strcond += " and  p.ProPoint = '" + pInfo.Propoint + "'";
            }
            if ((pInfo.PointType != null) && (pInfo.PointType != "") && (pInfo.PointType != "-99"))
            {
                strcond += " and  p.PointType = '" + pInfo.PointType + "'";
            }
            if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
            {
                strcond += " and  p.PointRangeCode = '" + pInfo.PointRangeCode + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }
            if ((pInfo.FlagPointType != null) && (pInfo.FlagPointType != ""))
            {
                strcond += " and  p.FlagPointType = '" + pInfo.FlagPointType + "'";
            }
            else
            {
                strcond += "  AND (p.FlagPointType IS NULL OR p.FlagPointType = '')  ";
            }
            if ((pInfo.StatusWfforReq != null) && (pInfo.StatusWfforReq != ""))
            {
                strcond += " and (p.Status IN ('Revised', 'Savedraft','Approved')) ";
            }
            if ((pInfo.PromotionTagCode != null) && (pInfo.PromotionTagCode != "") && (pInfo.PromotionTagCode != "-99"))
            {
                strcond += " and  p.PromotionTagCode like '%" + pInfo.PromotionTagCode + "%'";
            }
            if ((pInfo.ProductTagCode != null) && (pInfo.ProductTagCode != "") && (pInfo.ProductTagCode != "-99"))
            {
                strcond += " and  p.ProductTagCode like '%" + pInfo.ProductTagCode + "%'";
            }
            if ((pInfo.PromotionQuotaFlag != null) && (pInfo.PromotionQuotaFlag != "") && (pInfo.PromotionQuotaFlag != "-99"))
            {
                strcond += " and  p.PromotionQuotaFlag like '%" + pInfo.PromotionQuotaFlag + "%'";
            }
          

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select pb.ProductBrandName, t.PromotionTypeName,p.*, fs.LookupValue as FreeShip, st.LookupValue as PromotionStatusName, pl.LookupValue as PromotionLevelName," +
                    "pt.LookupValue as PointTypeName ,pp.LookupValue as ProPointName ,pr.PointName,cn.CompanyNameEN, " +
                    "lt.LookupValue as LazadaPromotionStatusName, ptc.LookupValue AS PromotionTagName, pc.LookupValue AS ProductTagName " +
                    " from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode  and t.FlagDelete='N' " +
                                " left join LookUp fs on fs.LookupCode =  p.FreeShipping and fs.LookupType='FREESHIPPING' " +
                                " left join LookUp st on st.LookupCode =  p.PromotionStatus  and st.LookupType='PROMOSTATUS' " +
                                " left join LookUp pl on pl.LookupCode =  p.PromotionLevel  and pl.LookupType='PROMOTIONLEVEL' " +
                                " left join LookUp pt on pt.LookupCode =  p.PointType  and pt.LookupType='POINTTYPE' " +
                                " left join LookUp pp on pp.LookupCode =  p.ProPoint  and pp.LookupType='PROPOINT' " +
                                " left join LookUp lt on lt.LookupCode =  p.LazadaPromotionStatus  and lt.LookupType='LAZADAPROMOTION' " +
                                " left join LookUp ptc on ptc.LookupCode =  p.PromotionTagCode  and ptc.LookupType='TAGPROMOTION' " +
                                " left join LookUp pc on pc.LookupCode =  p.ProductTagCode   and pc.LookupType='TAGPRODUCT' " +
                                " left join PointRange pr on pr.PointCode =  p.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join Company cn on cn.CompanyCode =  p.CompanyCode and cn.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join ProductBrand pb on pb.ProductBrandCode =  p.ProductBrandCode " +
                               " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  PromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                  PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),

                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["CreateDate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  FreeShippingCode = dr["FreeShipping"].ToString(),
                                  FreeShippingName = dr["FreeShip"].ToString(),
                                  PromotionStatusCode = dr["PromotionStatus"].ToString(),
                                  PromotionStatusName = dr["PromotionStatusName"].ToString(),
                                  MOQFlag = dr["MOQFlag"].ToString(),
                                  MinimumQty = (dr["MinimumQty"].ToString() != "") ? Convert.ToInt32(dr["MinimumQty"]) : 0,
                                  MinimumQtyTier2 = (dr["MinimumQtyTier2"].ToString() != "") ? Convert.ToInt32(dr["MinimumQtyTier2"]) : 0,
                                  DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                  DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["DiscountPercent"]) : 0,
                                  LockAmountFlag = dr["LockAmountFlag"].ToString(),
                                  LockCheckbox = dr["LockCheckbox"].ToString(),
                                  DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                  ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountAmount"]) : 0,
                                  ProductDiscountAmountTier2 = (dr["ProductDiscountAmountTier2"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountAmountTier2"]) : 0,
                                  ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountPercent"]) : 0,
                                  ProductDiscountPercentTier2 = (dr["ProductDiscountPercentTier2"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountPercentTier2"]) : 0,
                                  MinimumTotPrice = (dr["MinimumTotPrice"].ToString() != "") ? Convert.ToInt32(dr["MinimumTotPrice"]) : 0,
                                  GroupPrice = (dr["GroupPrice"].ToString() != "") ? Convert.ToDouble(dr["GroupPrice"]) : 0,
                                  PromotionLevel = dr["PromotionLevel"].ToString(),
                                  ComplementaryFlag = dr["ComplementaryFlag"].ToString(),
                                  ComplementaryChangeAble = dr["ComplementaryChangeAble"].ToString(),
                                  RedeemFlag = dr["RedeemFlag"].ToString(),
                                  CombosetFlag = dr["CombosetFlag"].ToString(),
                                  CombosetName = dr["CombosetName"].ToString(),
                                  StartDate = dr["StartDate"].ToString(),
                                  EndDate = dr["EndDate"].ToString(),
                                  ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                  ProductBrandName = dr["ProductBrandName"].ToString(),
                                  PromotionLevelName = dr["PromotionLevelName"].ToString(),
                                  wfStatus = dr["Status"].ToString(),
                                  wfFinishFlag = dr["FinishFlag"].ToString(),
                                  RequestByEmpCode = dr["RequestByEmpCode"].ToString(),

                                  Propoint =  dr["ProPoint"].ToString(),
                                  PropointName =  dr["ProPointName"].ToString(),
                                  PointType = dr["PointType"].ToString(),
                                  PointTypeName = dr["PointTypeName"].ToString(),
                                  CompanyCode = dr["CompanyCode"].ToString(),
                                  CompanyNameEN = dr["CompanyNameEN"].ToString(),
                                  FlagPatent = dr["FlagPatent"].ToString(),
                                  PatentAmount = (dr["PatentAmount"].ToString() != "") ? Convert.ToInt32(dr["PatentAmount"]) : 0,
                                  DiscountCode = dr["DiscountCode"].ToString(),
                                  PointRangeCode = dr["PointRangeCode"].ToString(),
                                  PointRangeName = dr["PointName"].ToString(),

                                  ApplyScope = dr["ApplyScope"].ToString(),
                                  CriteriaType = dr["CriteriaType"].ToString(),
                                  DiscountType = dr["DiscountType"].ToString(),
                                  OrderNumbers = (dr["OrderNumbers"].ToString() != "") ? Convert.ToInt32(dr["OrderNumbers"]) : (int?)null,
                                  CriteriaValueTier1 = dr["CriteriaValueTier1"].ToString(),
                                  CriteriaValueTier2 = dr["CriteriaValueTier2"].ToString(),
                                  CriteriaValueTier3 = dr["CriteriaValueTier3"].ToString(),
                                  DiscountValueTier1 = dr["DiscountValueTier1"].ToString(),
                                  DiscountValueTier2 = dr["DiscountValueTier2"].ToString(),
                                  DiscountValueTier3 = dr["DiscountValueTier3"].ToString(),
                                  LazadaPromotionStatusName = dr["LazadaPromotionStatusName"].ToString(),
                                  LazadaPromotionStatus = (dr["PatentAmount"].ToString() != "") ? Convert.ToInt32(dr["PatentAmount"]) : 0,
                                  PromotionTagCode = dr["PromotionTagCode"].ToString(),
                                  PromotionTagName = dr["PromotionTagName"].ToString(),
                                  ProductTagCode = dr["ProductTagCode"].ToString(),
                                  ProductTagName = dr["ProductTagName"].ToString(),
                                  PromotionQuotaFlag = dr["PromotionQuotaFlag"].ToString(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public List<PromotionListReturn> ListPromotionInCampaign(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            
            //trimmed string make code cannot pass this condition
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
            }

            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != ""))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }

            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.StartDate != "") && (pInfo.StartDate != null))
            {
                strcond += " and  p.StartDate >= CONVERT(VARCHAR, '" + pInfo.StartDate + "', 103)";
            }
            if ((pInfo.EndDate != "") && (pInfo.EndDate != null))
            {
                strcond += " and  p.EndDate >= CONVERT(VARCHAR, '" + pInfo.EndDate + "', 103)";
            }
            
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and p.PromotionCode not in (select PromotionCode from " + dbName + ".dbo.CampaignPromotion cp where cp.CampaignCode = '" + pInfo.CampaignCode + "' and cp.Active = 'Y')";
            }
            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantMapCode + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select t.PromotionTypeName,p.* from " + dbName + ".dbo.Promotion p " +
                                " inner join " + dbName + ".dbo.PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode and t.FlagDelete = 'N' " +
                                
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  PromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                  PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                  PromotionLevel = dr["PromotionLevel"].ToString().Trim(),
                                  StartDate = dr["StartDate"].ToString(),
                                  EndDate = dr["EndDate"].ToString(),
                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["CreateDate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  CombosetFlag = dr["CombosetFlag"].ToString(),
                                  CombosetName = dr["CombosetName"].ToString(),


                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public int? CountPromotionListByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }

            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != "") && (pInfo.PromotionTypeCode != "-99"))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != ""))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countPromotion from " + dbName + ".dbo.Promotion p " +
                           " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode " +
                           " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  countPromotion = Convert.ToInt32(dr["countPromotion"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countPromotion;
            }

            return count;
        }

        public int? CountPromotionInCampaignListByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }

            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != ""))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }

            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.CampaignCategoryCode != null) && (pInfo.CampaignCategoryCode != "") && (pInfo.CampaignCategoryCode != "-99"))
            {
                strcond += " and  p.CampaignCategoryCode = '" + pInfo.CampaignCategoryCode + "'";
            }
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }
            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantMapCode + "'";
            }
            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countPromotion from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode and t.FlagDelete = 'N' " +
                                " left join CampaignPromotion cp on cp.PromotionCode = p.PromotionCode and cp.CampaignCode = '" + pInfo.CampaignCode + "' and cp.Active = 'Y' " +
                                " where cp.PromotionCode is NULL and p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  countPromotion = Convert.ToInt32(dr["countPromotion"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countPromotion;
            }

            return count;
        }

        public int? CountListPromotion(PromotionInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != "") && (pInfo.PromotionTypeCode != "-99"))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo == "") || (pInfo.StartDateTo == null)))
            {
                strcond += " AND p.StartDate >= CONVERT(DATETIME, '" + pInfo.StartDate + "',103)";
            }
            if (((pInfo.StartDate == "") && (pInfo.StartDate == null)) && ((pInfo.StartDateTo != "") || (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate <= CONVERT(DATETIME, '" + pInfo.StartDateTo + "',103)";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo == "") || (pInfo.EndDateTo == null)))
            {
                strcond += " AND p.EndDate >= CONVERT(DATETIME, '" + pInfo.EndDate + "',103)";
            }
            if (((pInfo.EndDate == "") && (pInfo.EndDate == null)) && ((pInfo.EndDateTo != "") || (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate <= CONVERT(DATETIME, '" + pInfo.EndDateTo + "',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != "") && (pInfo.ProductBrandCode != "-99"))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.ComplementaryFlag != null) && (pInfo.ComplementaryFlag != ""))
            {
                strcond += " and  p.ComplementaryFlag = '" + pInfo.ComplementaryFlag + "'";
            }
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }
            if ((pInfo.wfStatus != null) && (pInfo.wfStatus != ""))
            {
                strcond += " and  p.Status = '" + pInfo.wfStatus + "'";
            }
            if ((pInfo.levels != null) && (pInfo.levels != 0))
            {
                strcond += " and  p.Levels = '" + pInfo.levels + "'";
            }
            // View by Admin
            if (pInfo.RoleCode !="ADMIN")
            {
                if ((pInfo.RequestByEmpCode != null) && (pInfo.RequestByEmpCode != ""))
                {
                    strcond += " and  p.RequestByEmpCode = '" + pInfo.RequestByEmpCode + "'";
                }

            }
         
            if ((pInfo.wfFinishFlag != null) && (pInfo.wfFinishFlag != ""))
            {
                strcond += " and  p.FinishFlag = '" + pInfo.wfFinishFlag + "'";
            }
            if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
            {
                strcond += " and  p.ProPoint = '" + pInfo.Propoint + "'";
            }
            if ((pInfo.PointType != null) && (pInfo.PointType != "") && (pInfo.PointType != "-99"))
            {
                strcond += " and  p.PointType = '" + pInfo.PointType + "'";
            }
            if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
            {
                strcond += " and  p.PointRangeCode = '" + pInfo.PointRangeCode + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }
            if ((pInfo.PromotionTagCode != null) && (pInfo.PromotionTagCode != "") && (pInfo.PromotionTagCode != "-99"))
            {
                strcond += " and  p.PromotionTagCode like '%" + pInfo.PromotionTagCode + "%'";
            }
            if ((pInfo.ProductTagCode != null) && (pInfo.ProductTagCode != "") && (pInfo.ProductTagCode != "-99"))
            {
                strcond += " and  p.ProductTagCode like '%" + pInfo.ProductTagCode + "%'";
            }
            if ((pInfo.PromotionQuotaFlag != null) && (pInfo.PromotionQuotaFlag != "") && (pInfo.PromotionQuotaFlag != "-99"))
            {
                strcond += " and  p.PromotionQuotaFlag like '%" + pInfo.PromotionQuotaFlag + "%'";
            }
            if ((pInfo.FlagPointType != null) && (pInfo.FlagPointType != ""))
            {
                strcond += " and  p.FlagPointType = '" + pInfo.FlagPointType + "'";
            }
            else
            {
                strcond += " AND (p.FlagPointType IS NULL OR p.FlagPointType = '') ";
            }
            if ((pInfo.StatusWfforReq != null) && (pInfo.StatusWfforReq != ""))
            {
                strcond += " and (p.Status IN ('Revised', 'Savedraft','Approved')) ";
            }
            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();


            try
            {
                string strsql = "SELECT        COUNT(p.Id) AS countPromotion from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode  and t.FlagDelete='N' " +
                                " left join LookUp fs on fs.LookupCode =  p.FreeShipping and fs.LookupType='FREESHIPPING' " +
                                " left join LookUp st on st.LookupCode =  p.PromotionStatus  and st.LookupType='PROMOSTATUS' " +
                                " left join LookUp pl on pl.LookupCode =  p.PromotionLevel  and pl.LookupType='PROMOTIONLEVEL' " +
                                " left join LookUp pt on pt.LookupCode =  p.PointType  and pl.LookupType='POINTTYPE' " +
                                " left join LookUp pp on pp.LookupCode =  p.ProPoint  and pl.LookupType='PROPOINT' " +
                                " left join LookUp ptc on ptc.LookupCode =  p.PromotionTagCode  and ptc.LookupType='TAGPROMOTION' " +
                                " left join LookUp pc on pc.LookupCode =  p.ProductTagCode and pc.LookupType='TAGPRODUCT' " +
                                " left join PointRange pr on pr.PointCode =  p.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join Company cn on cn.CompanyCode =  p.CompanyCode and cn.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join ProductBrand pb on pb.ProductBrandCode =  p.ProductBrandCode " +
                                " where p.FlagDelete ='N' " + strcond;
                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  countPromotion = Convert.ToInt32(dr["countPromotion"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countPromotion;
            }

            return count;
        }

        public List<PromotionTypeListReturn> ListPromotionType(PromotionTypeInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != ""))
            {
                strcond += " and t.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionTypeListReturn>();

            try
            {
                string strsql = " select t.Id, t.PromotionTypeCode, t.PromotionTypeName from " + dbName + ".dbo.PromotionType t " +
                               " where t.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY t.Id";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionTypeListReturn()
                              {
                                  PromotionTypeId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public List<PromotionListReturn> PromotionCodeValidateInsert(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode = '" + pInfo.PromotionCode + "'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName = '" + pInfo.PromotionName + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select p.PromotionCode from " + dbName + ".dbo.Promotion p " +
                               " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }
        public List<PromotionListReturn> ListPromotionListEliminate(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            
            //trimmed string make code cannot pass this condition
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
            }

            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != ""))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }

            

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select t.PromotionTypeName,p.* from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode " +
                               " where p.FlagDelete ='N' and " +
                               " (PromotionCode NOT IN ('" + pInfo.PromotionCodeEliminate + "')) " +
                               strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  PromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                  PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),

                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["CreateDate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  CombosetFlag = dr["CombosetFlag"].ToString(),
                                  CombosetName = dr["CombosetName"].ToString(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public int InsertPromotionImport(List<PromotionInfo> lcinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var pInfo in lcinfo.ToList())
            {
                strsql = "INSERT INTO Promotion  (PromotionCode,PromotionName,PromotionDesc,PromotionTypeCode,CreateDate,CreateBy,FlagDelete, FreeShipping, PromotionStatus," +
                           " MOQFlag, MinimumQty, DiscountAmount, DiscountPercent, LockAmountFlag, LockCheckbox, DefaultAmount, CombosetFlag, CombosetName)" +
                           "VALUES (" +
                          "'" + pInfo.PromotionCode + "'," +
                          "'" + pInfo.PromotionName + "'," +
                          "'" + pInfo.PromotionDesc + "'," +
                          "'" + pInfo.PromotionTypeCode + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.CreateBy + "'," +
                          
                          "'" + pInfo.FlagDelete + "'," +
                          "'" + pInfo.FreeShippingCode + "'," +
                          "'" + pInfo.PromotionStatusCode + "'," +
                          "'" + pInfo.MOQFlag + "'," +
                           pInfo.MinimumQty + "," +
                           pInfo.DiscountAmount + "," +
                           pInfo.DiscountPercent + "," +
                          "'" + pInfo.LockAmountFlag + "'," +
                          "'" + pInfo.LockCheckbox + "'," +
                          pInfo.DefaultAmount + "," +
                          "'" + pInfo.CombosetFlag + "'," +
                          "'" + pInfo.CombosetName + "'" +
                           ")";
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

        public int DeletePromotionList(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Promotion set FlagDelete = 'Y' where Id in (" + pInfo.PromotionCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionListReturn> ListPromotionbyPromotionTypeNoPagingByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }

            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != "") && (pInfo.PromotionTypeCode != "-99"))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != "") && (pInfo.ProductBrandCode != "-99"))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += " and  cp.Active = '" + pInfo.Active + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " SELECT cp.CampaignCode, cp.PromotionCode, p.PromotionName, p.PromotionTypeCode, pt.PromotionTypeName, pm.PromotionImageUrl from " + dbName + ".dbo.CampaignPromotion AS cp " +
                                " left join " + dbName + ".dbo.Promotion AS p ON p.PromotionCode = cp.PromotionCode " +
                                " left join " + dbName + ".dbo.PromotionType AS pt ON pt.PromotionTypeCode = p.PromotionTypeCode " +
                                " left join " + dbName + ".dbo.PromotionImage AS pm ON pm.PromotionCode = p.PromotionCode " +
                                " where (cp.Active = 'Y') AND (p.FlagDelete = 'N') " + strcond;

                strsql += " ORDER BY cp.CampaignCode DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                  PicturePromotionUrl = dr["PromotionImageUrl"].ToString().Trim(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public int UpdatePromotionfromApproverWF(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Promotion set " +
                            " PromotionDesc = '" + pInfo.PromotionDesc + "'," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pInfo.PromotionId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountListPromotionGreenSpotWF(PromotionInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != "") && (pInfo.PromotionTypeCode != "-99"))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo == "") || (pInfo.StartDateTo == null)))
            {
                strcond += " AND p.StartDate >= CONVERT(DATETIME, '" + pInfo.StartDate + "',103)";
            }
            if (((pInfo.StartDate == "") && (pInfo.StartDate == null)) && ((pInfo.StartDateTo != "") || (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate <= CONVERT(DATETIME, '" + pInfo.StartDateTo + "',103)";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo == "") || (pInfo.EndDateTo == null)))
            {
                strcond += " AND p.EndDate >= CONVERT(DATETIME, '" + pInfo.EndDate + "',103)";
            }
            if (((pInfo.EndDate == "") && (pInfo.EndDate == null)) && ((pInfo.EndDateTo != "") || (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate <= CONVERT(DATETIME, '" + pInfo.EndDateTo + "',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != "") && (pInfo.ProductBrandCode != "-99"))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.ComplementaryFlag != null) && (pInfo.ComplementaryFlag != ""))
            {
                strcond += " and  p.ComplementaryFlag = '" + pInfo.ComplementaryFlag + "'";
            }
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }
            if ((pInfo.wfStatus != null) && (pInfo.wfStatus != ""))
            {
                strcond += " and  p.Status = '" + pInfo.wfStatus + "'";
            }
            if ((pInfo.levels != null) && (pInfo.levels != 0))
            {
                strcond += " and  p.Levels = '" + pInfo.levels + "'";
            }
            if ((pInfo.RequestByEmpCode != null) && (pInfo.RequestByEmpCode != ""))
            {
                strcond += " and  p.RequestByEmpCode = '" + pInfo.RequestByEmpCode + "'";
            }
            if ((pInfo.wfFinishFlag != null) && (pInfo.wfFinishFlag != ""))
            {
                strcond += " and  p.FinishFlag = '" + pInfo.wfFinishFlag + "'";
            }
            if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
            {
                strcond += " and  p.ProPoint = '" + pInfo.Propoint + "'";
            }
            if ((pInfo.PointType != null) && (pInfo.PointType != "") && (pInfo.PointType != "-99"))
            {
                strcond += " and  p.PointType = '" + pInfo.PointType + "'";
            }
            if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
            {
                strcond += " and  p.PointRangeCode = '" + pInfo.PointRangeCode + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }
            if ((pInfo.StatusWfforReq != null) && (pInfo.StatusWfforReq != ""))
            {
                strcond += " and (p.Status IN ('Revised', 'Savedraft')) ";
            }
            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();


            try
            {
                string strsql = "SELECT        COUNT(p.Id) AS countPromotion from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode  and t.FlagDelete='N' " +
                                " left join LookUp fs on fs.LookupCode =  p.FreeShipping and fs.LookupType='FREESHIPPING' " +
                                " left join LookUp st on st.LookupCode =  p.PromotionStatus  and st.LookupType='PROMOSTATUS' " +
                                " left join LookUp pl on pl.LookupCode =  p.PromotionLevel  and pl.LookupType='PROMOTIONLEVEL' " +
                                " left join LookUp pt on pt.LookupCode =  p.PointType  and pl.LookupType='POINTTYPE' " +
                                " left join LookUp pp on pp.LookupCode =  p.ProPoint  and pl.LookupType='PROPOINT' " +
                                " left join PointRange pr on pr.PointCode =  p.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + " '" +
                                " left join Company cn on cn.CompanyCode =  p.CompanyCode and cn.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join ProductBrand pb on pb.ProductBrandCode =  p.ProductBrandCode " +
                                " where p.FlagDelete ='N' " + strcond;
                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  countPromotion = Convert.ToInt32(dr["countPromotion"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countPromotion;
            }

            return count;
        }

        public List<PromotionListReturn> ListPromotionListGreenSprtWF(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  p.Id =" + pInfo.PromotionId;
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }

            
            //trimmed string make code cannot pass this condition
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
            }
            if ((pInfo.PromotionTypeCode != null) && (pInfo.PromotionTypeCode != "") && (pInfo.PromotionTypeCode != "-99"))
            {
                strcond += " and  p.PromotionTypeCode = '" + pInfo.PromotionTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo == "") || (pInfo.StartDateTo == null)))
            {
                strcond += " AND p.StartDate >= CONVERT(DATETIME, '" + pInfo.StartDate + "',103)";
            }
            if (((pInfo.StartDate == "") && (pInfo.StartDate == null)) && ((pInfo.StartDateTo != "") || (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate <= CONVERT(DATETIME, '" + pInfo.StartDateTo + "',103)";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.StartDateTo != "") && (pInfo.StartDateTo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.StartDateTo + " 23:59:59:999',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo == "") || (pInfo.EndDateTo == null)))
            {
                strcond += " AND p.EndDate >= CONVERT(DATETIME, '" + pInfo.EndDate + "',103)";
            }
            if (((pInfo.EndDate == "") && (pInfo.EndDate == null)) && ((pInfo.EndDateTo != "") || (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate <= CONVERT(DATETIME, '" + pInfo.EndDateTo + "',103)";
            }
            if (((pInfo.EndDate != "") && (pInfo.EndDate != null)) && ((pInfo.EndDateTo != "") && (pInfo.EndDateTo != null)))
            {
                strcond += " AND p.EndDate BETWEEN CONVERT(DATETIME, '" + pInfo.EndDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDateTo + " 23:59:59:999',103)";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.ProductBrandCode != null) && (pInfo.ProductBrandCode != "") && (pInfo.ProductBrandCode != "-99"))
            {
                strcond += " and  p.ProductBrandCode like '%" + pInfo.ProductBrandCode + "%'";
            }
            if ((pInfo.ComplementaryFlag != null) && (pInfo.ComplementaryFlag != ""))
            {
                strcond += " and  p.ComplementaryFlag = '" + pInfo.ComplementaryFlag + "'";
            }
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }
            if ((pInfo.Bu != null) && (pInfo.Bu != ""))
            {
                strcond += " and  p.Bu = '" + pInfo.Bu + "'";
            }
            if ((pInfo.levels != null) && (pInfo.levels != 0))
            {
                strcond += " and  p.levels = '" + pInfo.levels + "'";
            }
            if ((pInfo.RequestByEmpCode != null) && (pInfo.RequestByEmpCode != ""))
            {
                strcond += " and  p.RequestByEmpCode = '" + pInfo.RequestByEmpCode + "'";
            }
            if ((pInfo.wfFinishFlag != null) && (pInfo.wfFinishFlag != ""))
            {
                strcond += " and  p.FinishFlag = '" + pInfo.wfFinishFlag + "'";
            }
            if ((pInfo.Propoint != null) && (pInfo.Propoint != "") && (pInfo.Propoint != "-99"))
            {
                strcond += " and  p.ProPoint = '" + pInfo.Propoint + "'";
            }
            if ((pInfo.PointType != null) && (pInfo.PointType != "") && (pInfo.PointType != "-99"))
            {
                strcond += " and  p.PointType = '" + pInfo.PointType + "'";
            }
            if ((pInfo.PointRangeCode != null) && (pInfo.PointRangeCode != "") && (pInfo.PointRangeCode != "-99"))
            {
                strcond += " and  p.PointRangeCode = '" + pInfo.PointRangeCode + "'";
            }
            if ((pInfo.CompanyCode != null) && (pInfo.CompanyCode != "") && (pInfo.CompanyCode != "-99"))
            {
                strcond += " and  p.CompanyCode = '" + pInfo.CompanyCode + "'";
            }           
            if ((pInfo.StatusWfforReq != null) && (pInfo.StatusWfforReq != ""))
            {
                strcond += " and (p.Status IN ('Revised', 'Savedraft')) ";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select pb.ProductBrandName, t.PromotionTypeName,p.*, fs.LookupValue as FreeShip, st.LookupValue as PromotionStatusName, pl.LookupValue as PromotionLevelName," +
                    "pt.LookupValue as PointTypeName ,pp.LookupValue as ProPointName ,pr.PointName,cn.CompanyNameEN from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode  and t.FlagDelete='N' " +
                                " left join LookUp fs on fs.LookupCode =  p.FreeShipping and fs.LookupType='FREESHIPPING' " +
                                " left join LookUp st on st.LookupCode =  p.PromotionStatus  and st.LookupType='PROMOSTATUS' " +
                                " left join LookUp pl on pl.LookupCode =  p.PromotionLevel  and pl.LookupType='PROMOTIONLEVEL' " +
                                " left join LookUp pt on pt.LookupCode =  p.PointType  and pt.LookupType='POINTTYPE' " +
                                " left join LookUp pp on pp.LookupCode =  p.ProPoint  and pp.LookupType='PROPOINT' " +
                                " left join PointRange pr on pr.PointCode =  p.PointRangeCode and pr.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join Company cn on cn.CompanyCode =  p.CompanyCode and cn.MerchantMapCode = '" + pInfo.MerchantCode + "' " +
                                " left join ProductBrand pb on pb.ProductBrandCode =  p.ProductBrandCode " +
                               " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  PromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                  PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),

                                  CreateBy = dr["CreateBy"].ToString(),
                                  CreateDate = dr["CreateDate"].ToString(),
                                  UpdateBy = dr["UpdateBy"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  FreeShippingCode = dr["FreeShipping"].ToString(),
                                  FreeShippingName = dr["FreeShip"].ToString(),
                                  PromotionStatusCode = dr["PromotionStatus"].ToString(),
                                  PromotionStatusName = dr["PromotionStatusName"].ToString(),
                                  MOQFlag = dr["MOQFlag"].ToString(),
                                  MinimumQty = (dr["MinimumQty"].ToString() != "") ? Convert.ToInt32(dr["MinimumQty"]) : 0,
                                  DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["DiscountAmount"]) : 0,
                                  DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["DiscountPercent"]) : 0,
                                  LockAmountFlag = dr["LockAmountFlag"].ToString(),
                                  LockCheckbox = dr["LockCheckbox"].ToString(),
                                  DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                  ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountAmount"]) : 0,
                                  ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToDouble(dr["ProductDiscountPercent"]) : 0,
                                  MinimumTotPrice = (dr["MinimumTotPrice"].ToString() != "") ? Convert.ToInt32(dr["MinimumTotPrice"]) : 0,
                                  GroupPrice = (dr["GroupPrice"].ToString() != "") ? Convert.ToDouble(dr["GroupPrice"]) : 0,
                                  PromotionLevel = dr["PromotionLevel"].ToString(),
                                  ComplementaryFlag = dr["ComplementaryFlag"].ToString(),
                                  ComplementaryChangeAble = dr["ComplementaryChangeAble"].ToString(),
                                  RedeemFlag = dr["RedeemFlag"].ToString(),
                                  CombosetFlag = dr["CombosetFlag"].ToString(),
                                  CombosetName = dr["CombosetName"].ToString(),
                                  StartDate = dr["StartDate"].ToString(),
                                  EndDate = dr["EndDate"].ToString(),
                                  ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                  ProductBrandName = dr["ProductBrandName"].ToString(),
                                  PromotionLevelName = dr["PromotionLevelName"].ToString(),
                                  wfStatus = dr["Status"].ToString(),
                                  wfFinishFlag = dr["FinishFlag"].ToString(),
                                  RequestByEmpCode = dr["RequestByEmpCode"].ToString(),

                                  Propoint = dr["ProPoint"].ToString(),
                                  PropointName = dr["ProPointName"].ToString(),
                                  PointType = dr["PointType"].ToString(),
                                  PointTypeName = dr["PointTypeName"].ToString(),
                                  CompanyCode = dr["CompanyCode"].ToString(),
                                  CompanyNameEN = dr["CompanyNameEN"].ToString(),
                                  FlagPatent = dr["FlagPatent"].ToString(),
                                  PatentAmount = (dr["PatentAmount"].ToString() != "") ? Convert.ToInt32(dr["PatentAmount"]) : 0,
                                  DiscountCode = dr["DiscountCode"].ToString(),
                                  PointRangeCode = dr["PointRangeCode"].ToString(),
                                  PointRangeName = dr["PointName"].ToString(),



                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public int InsertPromotionMapPromotionTag(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO PromotionMapPromotionTag (PromotionCode,PromotionTagCode,MerchantCode,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.PromotionCode + "'," +
                           "'" + pInfo.PromotionTagCode + "'," +
                           "'" + pInfo.MerchantCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.UpdateBy + "'," +
                           "'" + pInfo.FlagDelete + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionListReturn> ListPromotionMapPromotionTagNoPagingByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  p.PromotionCode = '" + pInfo.PromotionCode + "'";
            }

            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }

            if ((pInfo.PromotionCodeString != null) && (pInfo.PromotionCodeString != ""))
            {
                strcond += " and  p.PromotionCode in (" + pInfo.PromotionCodeString + ")";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = "SELECT p.Id, p.PromotionCode, pr.PromotionName, p.PromotionTagCode, p.MerchantCode, p.CreateDate, p.CreateBy, p.UpdateDate, " +
                                " p.UpdateBy, p.FlagDelete, t.LookupValue AS PromotionTagName " + 
                                " FROM PromotionMapPromotionTag p" +
                                " LEFT JOIN Lookup AS t ON t.LookupCode = p.PromotionTagCode AND t.LookupType = 'TAGPROMOTION' " +
                                " LEFT JOIN Promotion AS pr ON pr.PromotionCode = p.PromotionCode" +
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  PromotionMapPromotionTagId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                  PromotionTagCode = dr["PromotionTagCode"].ToString().Trim(),
                                  PromotionTagName = dr["PromotionTagName"].ToString().Trim(),
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

            return LPromotion;
        }

        public int UpdateFlagDeletePromotionMapPromotionTag(PromotionInfo pInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.PromotionMapPromotionTag set " +
                            " FlagDelete ='" + pInfo.FlagDelete + "'," +
                            " UpdateDate ='" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy ='" + pInfo.UpdateBy + "'" +
                            " where PromotionCode ='" + pInfo.PromotionCode + "'" +
                            " and MerchantCode = '" + pInfo.MerchantCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int ApprovePromotionList(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Promotion set Status = 'Approved' where Id in (" + pInfo.PromotionCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}

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
    public class PromotionCombosetDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public List<PromotionCombosetListReturn> ListPromotionCombosetList(PromotionCombosetInfo pInfo)
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
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionCombosetListReturn>();

            try
            {
                string strsql = " select pb.ProductBrandName, t.PromotionTypeName,p.*, fs.LookupValue as FreeShip, st.LookupValue as PromotionStatusName, pl.LookupValue as PromotionLevelName from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode  and t.FlagDelete='N' " +
                                " left join LookUp fs on fs.LookupCode =  p.FreeShipping and fs.LookupType='FREESHIPPING' " +
                                " left join LookUp st on st.LookupCode =  p.PromotionStatus  and st.LookupType='PROMOSTATUS' " +
                                " left join LookUp pl on pl.LookupCode =  p.PromotionLevel  and pl.LookupType='PROMOTIONLEVEL' " +
                                " left join ProductBrand pb on pb.ProductBrandCode =  p.ProductBrandCode " +
                               " where p.FlagDelete ='N'  and p.CombosetFlag = 'Y' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionCombosetListReturn()
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
                                  DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                  DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                  LockAmountFlag = dr["LockAmountFlag"].ToString(),
                                  LockCheckbox = dr["LockCheckbox"].ToString(),
                                  DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                  ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountAmount"]) : 0,
                                  ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountPercent"]) : 0,
                                  MinimumTotPrice = (dr["MinimumTotPrice"].ToString() != "") ? Convert.ToInt32(dr["MinimumTotPrice"]) : 0,
                                  GroupPrice = (dr["GroupPrice"].ToString() != "") ? Convert.ToInt32(dr["GroupPrice"]) : 0,
                                  PromotionLevel = dr["PromotionLevel"].ToString(),
                                  ComplementaryFlag = dr["ComplementaryFlag"].ToString(),
                                  RedeemFlag = dr["RedeemFlag"].ToString(),
                                  CombosetFlag = dr["CombosetFlag"].ToString(),
                                  CombosetName = dr["CombosetName"].ToString(),
                                  StartDate = dr["StartDate"].ToString(),
                                  EndDate = dr["EndDate"].ToString(),
                                  ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                  ProductBrandName = dr["ProductBrandName"].ToString(),
                                  PromotionLevelName = dr["PromotionLevelName"].ToString(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPromotion;
        }

        public int? CountListPromotionComboset(PromotionCombosetInfo pInfo)
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
            if ((pInfo.CombosetFlag != null) && (pInfo.CombosetFlag != ""))
            {
                strcond += " and  p.CombosetFlag = '" + pInfo.CombosetFlag + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionCombosetListReturn>();


            try
            {
                string strsql = "SELECT        COUNT(p.Id) AS countPromotion from " + dbName + ".dbo.Promotion p " +
                                " left join PromotionType t on t.PromotionTypeCode =  p.PromotionTypeCode  and t.FlagDelete='N' " +
                                " left join LookUp fs on fs.LookupCode =  p.FreeShipping and fs.LookupType='FREESHIPPING' " +
                                " left join LookUp st on st.LookupCode =  p.PromotionStatus  and st.LookupType='PROMOSTATUS' " +
                                " left join LookUp pl on pl.LookupCode =  p.PromotionLevel  and pl.LookupType='PROMOTIONLEVEL' " +
                                " left join ProductBrand pb on pb.ProductBrandCode =  p.ProductBrandCode " +
                                " where p.FlagDelete ='N' and p.CombosetFlag = 'Y' " + strcond;
                

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionCombosetListReturn()
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
    }
}
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
    public class SubPromotonDetailInfoDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public List<SubPromotionDetailListReturn> ListSubMainPromotionDetailNoPagingbyCriteria(SubPromotionDetailInfo sInfo)
        {
            string strcond = "";

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != 0))
            {
                strcond += " and  PromotionDetailId =" + sInfo.PromotionDetailId;
            }
            if ((sInfo.CampaignCategoryCode != null) && (sInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  ct.CampaignCategoryCode ='" + sInfo.CampaignCategoryCode.Trim() + "'";
            }

            if ((sInfo.CampaignCategoryName != null) && (sInfo.CampaignCategoryName != ""))
            {
                strcond += " and  c.CampaignCategoryName like '%" + sInfo.CampaignCategoryName.Trim() + "%'";
            }
            if ((sInfo.CampaignCode != null) && (sInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + sInfo.CampaignCode.Trim() + "'";
            }
            if ((sInfo.CampaignName != null) && (sInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + sInfo.CampaignName.Trim() + "%'";
            }
            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  cp.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }
            if ((sInfo.PromotionName != null) && (sInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + sInfo.PromotionName.Trim() + "%'";
            }
            if ((sInfo.CombosetCode != null) && (sInfo.CombosetCode != ""))
            {
                strcond += " and  pd.CombosetCode like '%" + sInfo.CombosetCode.Trim() + "%'";
            }
            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubPromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT sm.Id as SubMainPromotionDetailInfoId, sm.Amount, ct.CampaignCategoryCode, ct.CamCate_name, c.CampaignCode, c.CampaignName, cp.PromotionCode, p.PromotionName, pd.Id AS PromotionDetailId, " +
                                " pd.PromotionDetailName, c.FlagShowProductPromotion, c.FlagComboset, sm.FlagSubPromotionDetailMain, sm.ProductCode as MainProductCode, pr.ProductName as MainProductName, pr.Price,pr.UNIT,p.FreeShipping, " +
                                " u.LookupValue AS UNITName,pd.combosetcode FROM CampaignCategory AS ct " + 
                                " LEFT OUTER JOIN Campaign AS c ON c.CampaignCategory = ct.CampaignCategoryCode AND c.FlagDelete = 'N' and c.Active = 'Y' " + 
                                " LEFT OUTER JOIN CampaignPromotion AS cp ON cp.CampaignCode = c.CampaignCode AND cp.Active = 'Y' " + 
                                " LEFT OUTER JOIN Promotion AS p ON p.PromotionCode = cp.PromotionCode AND p.FlagDelete = 'N' " + 
                                " LEFT OUTER JOIN PromotionDetailInfo AS pd ON pd.PromotionCode = cp.PromotionCode AND pd.FlagDelete = 'N' " +
                                " LEFT OUTER JOIN SubMainPromotionDetailInfo AS sm ON sm.CombosetCode = pd.CombosetCode AND sm.FlagDelete = 'N' " +
                                " LEFT OUTER JOIN Product AS pr ON pr.ProductCode = sm.ProductCode AND pr.FlagDelete = 'N' " + 
                                " LEFT OUTER JOIN Lookup AS u ON u.LookupCode = pr.Unit AND u.LookupType = 'UNIT' " +
                                " where ct.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY PromotionDetailId DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                             select new SubPromotionDetailListReturn()
                             {
                                 SubMainPromotionDetailInfoId = (dr["SubMainPromotionDetailInfoId"].ToString() != "") ? Convert.ToInt32(dr["SubMainPromotionDetailInfoId"]) : 0,
                                 PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                 CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                 CampaignCategoryName = dr["CamCate_name"].ToString().Trim(),
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 CampaignName = dr["CampaignName"].ToString().Trim(),
                                 PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                 PromotionName = dr["PromotionName"].ToString().Trim(),
                                 PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                 FlagShowProductPromotion = dr["FlagShowProductPromotion"].ToString().Trim(),
                                 FlagComboSet = dr["FlagComboSet"].ToString().Trim(),
                                 FlagSubPromotionDetailMain = dr["FlagSubPromotionDetailMain"].ToString().Trim(),
                                 MainProductCode = dr["MainProductCode"].ToString().Trim(),
                                 MainProductName = dr["MainProductName"].ToString().Trim(),
                                 Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                 Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                 UNITName = dr["UNITName"].ToString().Trim(),
                                 UNIT = dr["UNIT"].ToString().Trim(),
                                 FreeShipping = dr["FreeShipping"].ToString().Trim(),
                                 CombosetCode = dr["CombosetCode"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ListSubPromotionDetailInfo;
        }
        public List<SubPromotionDetailListReturn> ListSubExchangePromotionDetailNoPagingbyCriteria(SubPromotionDetailInfo sInfo)
        {
            string strcond = "";

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != 0))
            {
                strcond += " and  PromotionDetailId =" + sInfo.PromotionDetailId;
            }
            if ((sInfo.CampaignCategoryCode != null) && (sInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  ct.CampaignCategoryCode ='" + sInfo.CampaignCategoryCode.Trim() + "'";
            }

            if ((sInfo.CampaignCategoryName != null) && (sInfo.CampaignCategoryName != ""))
            {
                strcond += " and  c.CampaignCategoryName like '%" + sInfo.CampaignCategoryName.Trim() + "%'";
            }
            if ((sInfo.CampaignCode != null) && (sInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + sInfo.CampaignCode.Trim() + "'";
            }
            if ((sInfo.CampaignName != null) && (sInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + sInfo.CampaignName.Trim() + "%'";
            }
            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  cp.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }
            if ((sInfo.PromotionName != null) && (sInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + sInfo.PromotionName.Trim() + "%'";
            }

            if ((sInfo.SubMainExchangeID != null) && (sInfo.SubMainExchangeID != ""))
            {
                strcond += " and  se.SubMainExchangeID ='" + sInfo.SubMainExchangeID + "'";
            }

            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubPromotionDetailListReturn>();

            try
            {
                string strsql = " SELECT   se.SubMainExchangeID,     ct.CampaignCategoryCode, ct.CamCate_name, c.CampaignCode, c.CampaignName, cp.PromotionCode, p.PromotionName, pd.Id AS PromotionDetailId, " +
                                " pd.PromotionDetailName, c.FlagShowProductPromotion, c.FlagComboset,se.Amount, se.FlagSubPromotionDetailExchange, se.ProductCode as ExchangeProductCode, pr.ProductName as ExchangeProductName, pr.Price,pr.UNIT, " +
                                " u.LookupValue AS UNITName FROM CampaignCategory AS ct " +
                                " LEFT OUTER JOIN Campaign AS c ON c.CampaignCategory = ct.CampaignCategoryCode AND c.FlagDelete = 'N' and c.Active = 'Y' " +
                                " LEFT OUTER JOIN CampaignPromotion AS cp ON cp.CampaignCode = c.CampaignCode AND cp.Active = 'Y' " +
                                " LEFT OUTER JOIN Promotion AS p ON p.PromotionCode = cp.PromotionCode AND p.FlagDelete = 'N' " +
                                " LEFT OUTER JOIN PromotionDetailInfo AS pd ON pd.PromotionCode = cp.PromotionCode AND pd.FlagDelete = 'N' " +
                                " LEFT OUTER JOIN SubExchangePromotionDetailInfo AS se ON se.CombosetCode = pd.CombosetCode AND se.FlagDelete = 'N' " +
                                " LEFT OUTER JOIN Product AS pr ON pr.ProductCode = se.ProductCode AND pr.FlagDelete = 'N' " +
                                " LEFT OUTER JOIN Lookup AS u ON u.LookupCode = pr.Unit AND u.LookupType = 'UNIT' " +
                                " where ct.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY PromotionDetailId DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                                              select new SubPromotionDetailListReturn()
                                              {
                                                  PromotionDetailId = (dr["PromotionDetailId"].ToString() != "") ? Convert.ToInt32(dr["PromotionDetailId"]) : 0,
                                                  SubMainExchangeID = dr["SubMainExchangeID"].ToString().Trim(),
                                                  CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                                  CampaignCategoryName = dr["CamCate_name"].ToString().Trim(),
                                                  CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                                  CampaignName = dr["CampaignName"].ToString().Trim(),
                                                  PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                                  PromotionName = dr["PromotionName"].ToString().Trim(),
                                                  PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                                  FlagShowProductPromotion = dr["FlagShowProductPromotion"].ToString().Trim(),
                                                  FlagComboSet = dr["FlagComboSet"].ToString().Trim(),
                                                  FlagSubPromotionDetailExchange = dr["FlagSubPromotionDetailExchange"].ToString().Trim(),
                                                  ExchangeProductCode = dr["ExchangeProductCode"].ToString().Trim(),
                                                  ExchangeProductName = dr["ExchangeProductName"].ToString().Trim(),
                                                  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                                  Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                                  UNITName = dr["UNITName"].ToString().Trim(),
                                                  UNIT = dr["UNIT"].ToString().Trim(),
                                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ListSubPromotionDetailInfo;
        }
    }
    
}

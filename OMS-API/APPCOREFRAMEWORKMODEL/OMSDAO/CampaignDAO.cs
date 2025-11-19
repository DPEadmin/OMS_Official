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
using System.Globalization;

namespace APPCOREMODEL.OMSDAO
{
    public class CampaignDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateCampaign(CampaignInfo cInfo)
        {
            int i = 0;
            string strsql;
            if (cInfo.CampaignImageName == "" || cInfo.CampaignImageName == null)
            {
                strsql = " Update " + dbName + ".dbo.Campaign set " +
                               " CampaignCode = '" + cInfo.CampaignCode + "'," +
                               " CampaignName = '" + cInfo.CampaignName + "'," +
                               " CampaignCategory = '" + cInfo.CampaignCategory + "'," +
                               " CampaignType = '" + cInfo.CampaignType + "'," +
                               " CampaignSpec = '" + cInfo.CampaignSpec + "'," +
                               " FlagComboset = '" + cInfo.FlagComboSet + "'," +
                               " FlagShowProductPromotion = '" + cInfo.FlagShowProductPromotion + "'," +
                               
                               " StartDate = '" + ((cInfo.StartDate != null) ? DateTime.Parse(cInfo.StartDate).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +
                               " NotifyDate = '" + ((cInfo.NotifyDate != null) ? DateTime.Parse(cInfo.NotifyDate).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +
                               " ExpireDate = '" + ((cInfo.ExpireDate != null) ? DateTime.Parse(cInfo.ExpireDate).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +
                               " UpdateBy = '" + cInfo.UpdateBy + "'," +
                               " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                               " Active = '" + cInfo.Active + "'" +
                               " where Id =" + cInfo.CampaignId + "";
            }
            else
            {
                strsql = " Update " + dbName + ".dbo.Campaign set " +
                               " CampaignCode = '" + cInfo.CampaignCode + "'," +
                               " CampaignName = '" + cInfo.CampaignName + "'," +
                               " CampaignCategory = '" + cInfo.CampaignCategory + "'," +
                               " CampaignType = '" + cInfo.CampaignType + "'," +
                               " CampaignSpec = '" + cInfo.CampaignSpec + "'," +
                               " FlagComboset = '" + cInfo.FlagComboSet + "'," +
                               " FlagShowProductPromotion = '" + cInfo.FlagShowProductPromotion + "'," +
                               " PictureCampaignUrl = '" + cInfo.PictureCampaignUrl + "'," +
                               " StartDate = '" + ((cInfo.StartDate != null) ? DateTime.Parse(cInfo.StartDate).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +
                               " NotifyDate = '" + ((cInfo.NotifyDate != null) ? DateTime.Parse(cInfo.NotifyDate).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +
                               " ExpireDate = '" + ((cInfo.ExpireDate != null) ? DateTime.Parse(cInfo.ExpireDate).ToString("MM/dd/yyyy HH:mm:ss") : "") + @"'," +
                               " UpdateBy = '" + cInfo.UpdateBy + "'," +
                               " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                               " Active = '" + cInfo.Active + "'" +
                               " where Id =" + cInfo.CampaignId + "";
            }
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteCampaign(CampaignInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Campaign set FlagDelete = 'Y' where Id in (" + cInfo.CampaignCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertCampaign(CampaignInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.Campaign  (MerchantMapCode,CampaignCode,CampaignName,CampaignCategory,CampaignType,FlagComboset,FlagShowProductPromotion,PictureCampaignUrl,StartDate,NotifyDate,ExpireDate,CreateDate,CreateBy,UpdateDate,UpdateBy,Active,FlagDelete,CampaignSpec,MerchantCode)" +
                            "VALUES (" +
                           "'" + cInfo.MerchantMapCode + "'," +
                           "'" + cInfo.CampaignCode + "'," +
                           "'" + cInfo.CampaignName + "'," +
                           "'" + cInfo.CampaignCategory + "'," +
                           "'" + cInfo.CampaignType + "'," +
                           "'" + cInfo.FlagComboSet + "'," +
                           "'" + cInfo.FlagShowProductPromotion + "'," +
                           "'" + cInfo.PictureCampaignUrl + "'," +
                           "'" + ((cInfo.StartDate != null) ? (DateTime.Parse(cInfo.StartDate).ToString("MM/dd/yyyy HH:mm:ss")) : "") + @"'," +
                           "'" + ((cInfo.NotifyDate != null) ? (DateTime.Parse(cInfo.NotifyDate).ToString("MM/dd/yyyy HH:mm:ss")) : "") + @"'," +
                           "'" + ((cInfo.ExpireDate != null) ? (DateTime.Parse(cInfo.ExpireDate).ToString("MM/dd/yyyy HH:mm:ss")) : "") + @"'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'" + cInfo.Active + "'," +
                           "'" + cInfo.FlagDelete + "'," +
                           "'" + cInfo.CampaignSpec + "'," +
                            "'" + cInfo.MerchantCode + "'" +
                            ")";
            strsql = strsql + "";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<CampaignListReturn> ListCampaignNoPagingByCriteria(CampaignInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CampaignId != null) && (cInfo.CampaignId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CampaignId;
            }
            //if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != "") && (cInfo.MerchantCode != "-99"))
            //{
            //    strcond += " and  c.MerchantCode = '" + cInfo.MerchantCode + "'";
            //}
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != "") && (cInfo.MerchantMapCode != "-99"))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode + "'";
            }
            if ((cInfo.CampaignCategory != null) && (cInfo.CampaignCategory != ""))
            {
                strcond += " and  c.CampaignCategory like '%" + cInfo.CampaignCategory + "%'";
            }
            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + cInfo.CampaignCode + "'";
            }
            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }
            if ((cInfo.CompanyCode != null) && (cInfo.CompanyCode != ""))
            {
                strcond += " and  c.CompanyCode = '" + cInfo.CompanyCode + "'";
            }

            if ((cInfo.NotifyDate != null) && (cInfo.NotifyDate != ""))
            {
                strcond += " and c.NotifyDate = '" + DateTime.Now.ToString() + @"'";
                

            }

            if ((cInfo.ExpireDate != null) && (cInfo.ExpireDate != ""))
            {
                strcond += " and c.ExpireDate = '" + DateTime.Now.ToString() + @"'";
                
            }
            if ((cInfo.CampaignSpec != null) && (cInfo.CampaignSpec != ""))
            {
                strcond += " and  c.CampaignSpec = '" + cInfo.CampaignSpec + "'";
            }
            if ((cInfo.Active != null) && (cInfo.Active != "") && (cInfo.Active != "-99"))
            {
                strcond += " and  c.Active = '" + cInfo.Active + "'";
            }
            if ((cInfo.FlagComboSet != null) && (cInfo.FlagComboSet != "") && (cInfo.FlagComboSet != "-99"))
            {
                strcond += " and  c.FlagComboSet = '" + cInfo.FlagComboSet + "'";
            }
            if ((cInfo.FlagShowProductPromotion != null) && (cInfo.FlagShowProductPromotion != "") && (cInfo.FlagShowProductPromotion != "-99"))
            {
                strcond += " and  c.FlagShowProductPromotion = '" + cInfo.FlagShowProductPromotion + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignListReturn>();

            try
            {
                string strsql = " select c.*, cg.camcate_name from " + dbName + ".dbo.Campaign c left join " + dbName +
                                ".dbo.CampaignCategory cg on cg.CampaignCategoryCode = c.CampaignCategory AND cg.flagdelete = 'N'" +
                                
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                            select new CampaignListReturn()
                            {
                                CampaignId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                CampaignCategory = dr["CampaignCategory"].ToString().Trim(),
                                CampaignName = dr["CampaignName"].ToString().Trim(),
                                CampaignCategoryName = dr["camcate_name"].ToString().Trim(),
                                PictureCampaignUrl = dr["PictureCampaignUrl"].ToString().Trim(),
                                FlagComboSet = dr["FlagComboset"].ToString().Trim(),
                                FlagShowProductPromotion = dr["FlagShowProductPromotion"].ToString().Trim(),
                                StartDate = dr["StartDate"].ToString().Trim(),
                                NotifyDate = dr["NotifyDate"].ToString().Trim(),
                                ExpireDate = dr["ExpireDate"].ToString().Trim(),
                                CreateBy = dr["CreateBy"].ToString(),
                                CreateDate = dr["CreateDate"].ToString(),
                                UpdateBy = dr["UpdateBy"].ToString(),
                                UpdateDate = dr["UpdateDate"].ToString(),
                                Active = dr["Active"].ToString(),
                                FlagDelete = dr["FlagDelete"].ToString(),
                                CampaignType = dr["CampaignType"].ToString().Trim(),
                                MerchantCode = dr["MerchantCode"].ToString().Trim(),
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<CampaignListReturn> ListCampaignMediaNoPagingByCriteria(CampaignInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + cInfo.CampaignCode + "'";
            }
            if ((cInfo.MediaPhone != null) && (cInfo.MediaPhone != ""))
            {
                strcond += " and  m.Mediaphone = '" + cInfo.MediaPhone + "'";
            }
            if ((cInfo.TIME_START_Mediaplan != null) && (cInfo.TIME_START_Mediaplan != ""))
            {
                
                DateTime aaa = DateTime.ParseExact(cInfo.MediaPlanDate + " " + cInfo.TIME_START_Mediaplan, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

             strcond += " and   ( CAST(CONVERT(datetime2, CONVERT(varchar, m.MediaPlanDate) +' ' + CONVERT(varchar, m.TimeStart)) as time)  BETWEEN cast(DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '"+ cInfo.MediaPlanDate+ "', 103)), '"+cInfo.TIME_START_Mediaplan+ "') as time ) AND cast(DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '" + cInfo.MediaPlanDateEnd + "', 103)), '" + cInfo.TIME_END_Mediaplan + "') as time) " +
                    " or " +
                    " CAST(CONVERT(datetime2, CONVERT(varchar, m.MediaPlanDate) +' ' + CONVERT(varchar, m.TimeEnd)) as time)  BETWEEN cast(DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '" + cInfo.MediaPlanDate + "', 103)), '" + cInfo.TIME_START_Mediaplan + "') as time ) AND cast(DATEADD(day, DATEDIFF(day, 0, CONVERT(datetime2, '" + cInfo.MediaPlanDateEnd + "', 103)), '" + cInfo.TIME_END_Mediaplan + "') as time" +
                    "))and CONVERT(date,m.MediaPlanEndDate) >= CONVERT(date, GETDATE())";
           
            
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignListReturn>();

            try
            {
                string strsql = " select distinct c.id,c.CampaignCode,c.FlagComboset,c.PictureCampaignUrl" +
                    " ,c.NotifyDate,c.CampaignName,c.CampaignCategory,c.FlagShowProductPromotion,c.CreateBy,c.CreateDate,c.UpdateBy,c.Active," +
                    "c.FlagDelete,c.CampaignType,c.UpdateDate" +
                    " from " + dbName + ".dbo.Campaign c " +
                                " inner join " + dbName + ".dbo.MediaPlan m on m.CampaignCode = c.CampaignCode " +
                                "  inner join " + dbName + ".dbo.CampaignPromotion cp on cp.CampaignCode =c.CampaignCode " +
                                "  inner join " + dbName + ".dbo.Promotion p on cp.PromotionCode = p.PromotionCode" +
                                " where c.FlagDelete ='N' and c.Active = 'Y'  and m.Active='Y' and m.FlagApprove ='Y'  " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignListReturn()
                             {
                                 CampaignId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 CampaignCategory = dr["CampaignCategory"].ToString().Trim(),
                                 CampaignName = dr["CampaignName"].ToString().Trim(),
                                 PictureCampaignUrl = dr["PictureCampaignUrl"].ToString().Trim(),
                                 FlagComboSet = dr["FlagComboset"].ToString().Trim(),
                                 FlagShowProductPromotion = dr["FlagShowProductPromotion"].ToString().Trim(),
                             
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 Active = dr["Active"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 CampaignType = dr["CampaignType"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }
        public List<CampaignListReturn> ListCampaignMediaNoPagingByCriteria2(CampaignInfo cInfo)
        {
            string strcond = "";
            string strjoincond = "";
            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + cInfo.CampaignCode + "'";
            }
            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }
            if ((cInfo.MediaPhone != null) && (cInfo.MediaPhone != ""))
            {
                strcond += " and  m.Mediaphone = '" + cInfo.MediaPhone + "'";
            }
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))

            {
                strjoincond += " and  m.MerchantCode = '" + cInfo.MerchantMapCode + "'";
            }
            if ((cInfo.TIME_START_Mediaplan != null) && (cInfo.TIME_START_Mediaplan != ""))
            {
                
                DateTime aaa = DateTime.ParseExact(cInfo.MediaPlanDate + " " + cInfo.TIME_START_Mediaplan, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                
                strcond += " AND((CONVERT(date, m.MediaPlanDate) <= CONVERT(date, GETDATE()))" +
                         "or(CONVERT(date, m.MediaPlanEndDate) >= CONVERT(date, GETDATE())))" +
                         "and (CONVERT(time, GETDATE()) >= DATEADD(MINUTE, -30, CONVERT(time, m.TimeStart)) " +
                         "and CONVERT(time, GETDATE()) <= DATEADD(HOUR, 1.30, CONVERT(time, m.TimeEnd)))";

             
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignListReturn>();

            try
            {
                string strsql = " select distinct c.id,c.CampaignCode,c.FlagComboset,c.PictureCampaignUrl" +
                    " ,c.NotifyDate,c.CampaignName,c.CampaignCategory,c.FlagShowProductPromotion,c.CreateBy,c.CreateDate,c.UpdateBy,c.Active," +
                    "c.FlagDelete,c.CampaignType,c.UpdateDate,m.SALE_CHANNEL,ch.ChannelName" +
                    " from " + dbName + ".dbo.Campaign c " +
                                " inner join " + dbName + ".dbo.MediaPlan m on m.CampaignCode = c.CampaignCode and m.FlagDelete = 'N' and m.Active='Y' and m.FlagApprove ='Y' " + strjoincond +
                                "  inner join " + dbName + ".dbo.CampaignPromotion cp on cp.CampaignCode =c.CampaignCode " +
                                "  inner join " + dbName + ".dbo.Promotion p on cp.PromotionCode = p.PromotionCode" +
                                " inner join "+ dbName + ".dbo.Channel ch on ch.ChannelCode = m.SALE_CHANNEL" +
                                " where c.FlagDelete ='N' and c.Active = 'Y'" + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignListReturn()
                             {
                                 CampaignId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 CampaignCategory = dr["CampaignCategory"].ToString().Trim(),
                                 CampaignName = dr["CampaignName"].ToString().Trim(),
                                 PictureCampaignUrl = dr["PictureCampaignUrl"].ToString().Trim(),
                                 FlagComboSet = dr["FlagComboset"].ToString().Trim(),
                                 FlagShowProductPromotion = dr["FlagShowProductPromotion"].ToString().Trim(),
                                 
                                 Sale_Channel = dr["SALE_CHANNEL"].ToString(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 Active = dr["Active"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 CampaignType = dr["CampaignType"].ToString().Trim(),
                                 ChannelName = dr["ChannelName"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }
        public List<CampaignListReturn> ListCampaignPagingByCriteria(CampaignInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CampaignId != null) && (cInfo.CampaignId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CampaignId;
            }
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode + "'";
            }
            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != "") && (cInfo.CampaignCode != "-99"))
            {
                strcond += " and  c.CampaignCode like '%" + cInfo.CampaignCode.Trim() + "%'";
            }
            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName.Trim() + "%'";
            }
            if ((cInfo.CampaignSpec != null) && (cInfo.CampaignSpec != "") && (cInfo.CampaignSpec != "-99"))
            {
                strcond += " and sp.LookupCode = '" + cInfo.CampaignSpec.Trim() + "'";
            }

            if ((cInfo.CampaignCategory != null) && (cInfo.CampaignCategory != "") && (cInfo.CampaignCategory != "-99"))
            {
                strcond += " and  c.CampaignCategory like '%" + cInfo.CampaignCategory + "%'";
            }

            if ((cInfo.NotifyDate != null) && (cInfo.NotifyDate != ""))
            {
                strcond += " and c.NotifyDate = '" + DateTime.Now.ToString() + @"'";
                

            }

            if ((cInfo.ExpireDate != null) && (cInfo.ExpireDate != ""))
            {
                strcond += " and c.ExpireDate = '" + DateTime.Now.ToString() + @"'";
                

            }
            if ((cInfo.Active != null) && (cInfo.Active != "") && (cInfo.Active != "-99"))
            {
                strcond += " and  c.Active like '" + cInfo.Active + "'";
            }
            if ((cInfo.FlagComboSet != null) && (cInfo.FlagComboSet != "") && (cInfo.FlagComboSet != "-99"))
            {
                strcond += " and  c.FlagComboSet = '" + cInfo.FlagComboSet + "'";
            }
            if ((cInfo.FlagShowProductPromotion != null) && (cInfo.FlagShowProductPromotion != "") && (cInfo.FlagShowProductPromotion != "-99"))
            {
                strcond += " and  c.FlagShowProductPromotion = '" + cInfo.FlagShowProductPromotion + "'";
            }
            if ((cInfo.CampaignType != null) && (cInfo.CampaignType != ""))
            {
                strcond += " and  c.CampaignType = '" + cInfo.CampaignType + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignListReturn>();

            try
            {
                string strsql = " select c.*,l.LookupValue as CAMPAIGNSTATUS,sp.LookupValue as CampaignSpec_Value ,cg.camcate_name from " + dbName + ".dbo.Campaign c " + 
                                " left join " + dbName + ".dbo.CampaignCategory cg on cg.CampaignCategoryCode = c.CampaignCategory AND cg.flagdelete = 'N'" +
                                " left join " + dbName + ".dbo.Lookup as l on l.LookupCode = c.Active and l.LookupType = 'CAMPAIGNSTATUS' " +
                                " left join " + dbName + ".dbo.Lookup as sp on Sp.LookupCode = c.CampaignSpec and Sp.LookupType = 'CAMPAIGNSPEC' " +
                                
                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignListReturn()
                             {
                                 CampaignId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 CampaignCategory = dr["CampaignCategory"].ToString().Trim(),
                                 CampaignName = dr["CampaignName"].ToString().Trim(),
                                 CampaignCategoryName = dr["camcate_name"].ToString().Trim(),
                                 CampaignType = dr["CampaignType"].ToString().Trim(),
                                 FlagComboSet = dr["FlagComboset"].ToString().Trim(),
                                 FlagShowProductPromotion = dr["FlagShowProductPromotion"].ToString().Trim(),
                                 StartDate = dr["StartDate"].ToString().Trim(),
                                 NotifyDate = dr["NotifyDate"].ToString().Trim(),
                                 ExpireDate = dr["ExpireDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 MerchantMapCode = dr["MerchantMapCode"].ToString(),
                                 Active = dr["Active"].ToString(),
                                 CAMPAIGNSTATUS = dr["CAMPAIGNSTATUS"].ToString(),
                                 CampaignSpec = dr["CampaignSpec"].ToString(),
                                 CampaignSpecValue = dr["CampaignSpec_Value"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int? CountCampaignListByCriteria(CampaignInfo cInfo) 
        {
            string strcond = "";
            int? count = 0;

            if ((cInfo.CampaignId != null) && (cInfo.CampaignId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CampaignId;
            }
            if ((cInfo.CampaignCategory != null) && (cInfo.CampaignCategory != "") && (cInfo.CampaignCategory != "-99"))
            {
                strcond += " and  c.CampaignCategory like '%" + cInfo.CampaignCategory + "%'";
            }
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode + "'";
            }
            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode like '%" + cInfo.CampaignCode + "%'";
            }
            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }

            if ((cInfo.NotifyDate != null) && (cInfo.NotifyDate != ""))
            {
                strcond += " and c.NotifyDate = '" + DateTime.Now.ToString() + @"'";
                

            }

            if ((cInfo.ExpireDate != null) && (cInfo.ExpireDate != ""))
            {
                strcond += " and c.ExpireDate = '" + DateTime.Now.ToString() + @"'";
                
            }

            if ((cInfo.Active != null) && (cInfo.Active != "") && (cInfo.Active != "-99"))
            {
                strcond += " and  c.Active = '" + cInfo.Active + "'";
            }
            if ((cInfo.FlagComboSet != null) && (cInfo.FlagComboSet != "") && (cInfo.FlagComboSet != "-99"))
            {
                strcond += " and  c.FlagComboSet = '" + cInfo.FlagComboSet + "'";
            }
            if ((cInfo.FlagShowProductPromotion != null) && (cInfo.FlagShowProductPromotion != "") && (cInfo.FlagShowProductPromotion != "-99"))
            {
                strcond += " and  c.FlagShowProductPromotion = '" + cInfo.FlagShowProductPromotion + "'";
            }
            if ((cInfo.CampaignType != null) && (cInfo.CampaignType != ""))
            {
                strcond += " and  c.CampaignType = '" + cInfo.CampaignType + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignListReturn>();


            try
            {
                string strsql = "select count(c.Id) as countCampaign from " + dbName + ".dbo.Campaign c " +
                         " left join " + dbName + ".dbo.CampaignCategory cg on cg.CampaignCategoryCode = c.CampaignCategory AND cg.flagdelete = 'N'" +
                         " left join " + dbName + ".dbo.Lookup as l on l.LookupCode = c.Active and l.LookupType = 'CAMPAIGNSTATUS' " +
                         " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignListReturn()
                             {
                                 countCampaign = Convert.ToInt32(dr["countCampaign"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countCampaign;
            }

            return count;
        }

        public int? InsertOrderCampaignMovement(CampaignInfo cInfo)
        {
            int? i = 0;

            string strsql = "INSERT INTO OrderCampaignMovement  (CampaignCode,OrderCode,CreateDate,CreateBy)" +
                           "VALUES (" +
                          "'" + cInfo.CampaignCode + "'," +
                          "'" + cInfo.OrderCode + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'" +
                           ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;

        }

        public List<CampaignListReturn> GetTopFiveCampagin(CampaignInfo cInfo)
        {
          

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignListReturn>();

            try
            {
                string strsql = " SELECT TOP (5) CampaignCode, COUNT(CampaignCode) AS SumCampaign FROM " + dbName + ".dbo.OrderCampaignMovement c " +
                                " GROUP BY CampaignCode ";// + strcond;

                strsql += " ORDER BY SumCampaign DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignListReturn()
                             {
                                 SumCampaign = (dr["SumCampaign"].ToString() != "") ? Convert.ToInt32(dr["SumCampaign"]) : 0,
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public List<CampaignListReturn> CampaignCodeValidateInsert(CampaignInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CampaignCode != null) && (cInfo.CampaignCode != ""))
            {
                strcond += " and  c.CampaignCode = '" + cInfo.CampaignCode.Trim() + "'";
            }
            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName = '" + cInfo.CampaignName.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignListReturn>();

            try
            {
                string strsql = " select c.CampaignCode from " + dbName + ".dbo.Campaign c " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignListReturn()
                             {
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int InsertCampaignImport(List<CampaignInfo> lcinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in lcinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.Campaign (CampaignCode,CampaignName,StartDate,NotifyDate,ExpireDate,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete) values (" +
                             "'" + info.CampaignCode + "', " +
                             "'" + info.CampaignName + "', " +
                             "'" + info.StartDate + "', " +
                             "'" + info.NotifyDate + "', " +
                             "'" + info.ExpireDate + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                             "'" + info.UpdateBy + "', " +
                             "'" + "N" + "')";
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

    }

}

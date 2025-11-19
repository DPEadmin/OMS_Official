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
    public class CampaignPromotionDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountCampaignPromotionListByCriteria(PromotionInfo pInfo) {
            string strcond = "";
            string strjoincond = "";
            int? count = 0;

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += " and  cp.Active = '" + pInfo.Active + "'";
            }
            if ((pInfo.FlagDelete != null) && (pInfo.FlagDelete != ""))
            {
                strcond += " and  c.FlagDelete = '" + pInfo.FlagDelete + "'";
            }
            if ((pInfo.StartDate != "") && (pInfo.StartDate != null))
            {
                strcond += " and  p.StartDate >= CONVERT(VARCHAR, '" + pInfo.StartDate + "', 103)";
            }
            if ((pInfo.EndDate != "") && (pInfo.EndDate != null))
            {
                strcond += " and  p.EndDate >= CONVERT(VARCHAR, '" + pInfo.EndDate + "', 103)";
            }
            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strjoincond += " and c.MerchantMapCode = '" + pInfo.MerchantMapCode + "' ";
            }
            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();


            try
            {
                string strsql = "select count(cp.Id) as countCampaignPromotion from " + dbName + ".dbo.CampaignPromotion cp LEFT OUTER JOIN " +
                            dbName + ".dbo.Campaign c ON c.CampaignCode = cp.CampaignCode"+ strjoincond + " LEFT OUTER JOIN " +
                            dbName + ".dbo.Promotion p on p.PromotionCode = cp.PromotionCode LEFT OUTER JOIN " +
                            dbName + ".dbo.PromotionType t on t.PromotionTypeCode = p.PromotionTypeCode and t.FlagDelete = 'N' LEFT OUTER JOIN " +
                            dbName + ".dbo.Emp e On cp.CreateBy = e.EmpCode " +
                            " where 1=1 and c.FlagDelete = 'N' and p.FlagDelete = 'N' and cp.Active = 'Y' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                             select new PromotionListReturn()
                             {
                                 countCampaignPromotion = Convert.ToInt32(dr["countCampaignPromotion"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countCampaignPromotion;
            }

            return count;
        }

        public int InsertCampaignPromotion(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO CampaignPromotion(CampaignCode,PromotionCode,CreateDate,CreateBy,UpdateDate,Active)" +
                            " OUTPUT inserted.Id VALUES (" +
                           "'" + pInfo.CampaignCode + "'," +
                           "'" + pInfo.PromotionCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.Active + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateCampaignPromotion(PromotionInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.CampaignPromotion set " +
                            " Active = '" + pInfo.Active + "'," +
                            " UpdateBy = '" + pInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id ='" + pInfo.CampaignPromotionId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteCampaignPromotion(PromotionInfo pInfo)
        {
            int i = 0;

            //string strsql = "delete from " + dbName + ".dbo.CampaignPromotion  where Id in (" + pInfo.CampaignPromotionId + ")";
            string strsql = "update " + dbName + ".dbo.CampaignPromotion set Active = 'N' where Id in (" + pInfo.CampaignCode + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<PromotionListReturn> ListCampaignPromotionByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";
            string strjoincond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }
            if ((pInfo.CampaignPromotionId != null) && (pInfo.CampaignPromotionId != 0))
            {
                strcond += " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += " and  cp.PromotionCode like '%" + pInfo.PromotionCode.Trim() + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += " and  p.PromotionName like '%" + pInfo.PromotionName.Trim() + "%'";
            }
            if ((pInfo.PromotionLevel != null) && (pInfo.PromotionLevel != "") && (pInfo.PromotionLevel != "-99"))
            {
                strcond += " and  p.PromotionLevel = '" + pInfo.PromotionLevel + "'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += " and  cp.Active = '" + pInfo.Active + "'";
            }
            if ((pInfo.StartDate != "") && (pInfo.StartDate != null))
            {
                strcond += " and  p.StartDate = CONVERT(VARCHAR, '" + pInfo.StartDate + "', 103)";
            }
            if ((pInfo.EndDate != "") && (pInfo.EndDate != null))
            {
                strcond += " and  p.EndDate = CONVERT(VARCHAR, '" + pInfo.EndDate + "', 103)";
            }
            if ((pInfo.FlagDelete != null) && (pInfo.FlagDelete != ""))
            {
                strcond += " and  c.FlagDelete = '" + pInfo.FlagDelete + "'";
            }
            if ((pInfo.MerchantMapCode != null) && (pInfo.MerchantMapCode != ""))
            {
                strjoincond += " and c.MerchantMapCode = '" + pInfo.MerchantMapCode + "' ";
            }




            DataTable dt = new DataTable();
            var LCampaignPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select cp.* ,p.PromotionName, p.PromotionDesc,p.PromotionTypeCode, c.CampaignName, t.PromotionTypeName, e.EmpFname_TH as name, e.EmpLName_TH as lastname, c.NotifyDate, c.ExpireDate, p.PromotionLevel, p.StartDate, p.EndDate,cc.CamCate_name from " + dbName + ".dbo.CampaignPromotion cp " +


                                 " left join " + dbName + ".dbo.Campaign c on c.CampaignCode = cp.CampaignCode " + strjoincond +

                                 " left join " + dbName + ".dbo.CampaignCategory AS cc ON cc.CampaignCategoryCode = c.CampaignCategory " +

                                 " left join " + dbName + ".dbo.Promotion p on p.PromotionCode = cp.PromotionCode " +

                                 " left join " + dbName + ".dbo.PromotionType t on t.PromotionTypeCode = p.PromotionTypeCode AND t.FlagDelete = 'N' " +

                                 " left join " + dbName + ".dbo.Emp e On cp.CreateBy = e.EmpCode " +

                                 " where 1=1 and c.FlagDelete = 'N' and p.FlagDelete = 'N' and cp.Active = 'Y' " + strcond;


                strsql += " ORDER BY cp.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaignPromotion = (from DataRow dr in dt.Rows

                             select new PromotionListReturn()
                             {
                                 CampaignPromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                 CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                 PromotionName = dr["PromotionName"].ToString().Trim(),
                                 CampaignName = dr["CampaignName"].ToString().Trim(),
                                 PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                 PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                 PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                 PromotionLevel = dr["PromotionLevel"].ToString().Trim(),
                                 NotifyDate = dr["NotifyDate"].ToString(),
                                 ExpireDate =  (dr["ExpireDate"].ToString() != "") ? dr["ExpireDate"].ToString() : "",
                                 StartDate = (dr["StartDate"].ToString() != "") ? dr["StartDate"].ToString() : "",
                                 EndDate = (dr["EndDate"].ToString() != "") ? dr["EndDate"].ToString() : "",
                                 CreateDate = dr["CreateDate"].ToString(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 Name = dr["name"].ToString(),
                                 CampaignCategoryName = dr["CamCate_name"].ToString(),
                                 Lastname = dr["lastname"].ToString()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaignPromotion;
        }

        public List<PromotionListReturn> ListCampaignPromotionNopagingByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += (strcond == "") ? " where  cp.Id = '" + pInfo.CampaignPromotionId + "'" : " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'" : " and  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += (strcond == "") ? " where  p.PromotionName like '%" + pInfo.PromotionName + "%'" : " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'" : " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += (strcond == "") ? " where  cp.Active = '" + pInfo.Active + "'" : " and  cp.Active = '" + pInfo.Active + "'";
            }
            

            DataTable dt = new DataTable();
            var LCampaignPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select cp.* ,p.PicturePromotionUrl,p.PromotionName,p.PromotionDesc,p.PromotionTypeCode,p.LockAmountFlag,p.LockCheckbox,p.FreeShipping,p.MOQFlag,p.GroupPrice from " + dbName + ".dbo.CampaignPromotion cp " +


                                 " left join " + dbName + ".dbo.Campaign c on c.CampaignCode = cp.CampaignCode AND c.FlagDelete = 'N' and c.Active = 'Y' " +

                                 " inner join " + dbName + ".dbo.Promotion p on p.PromotionCode = cp.PromotionCode AND p.FlagDelete = 'N'  and p.EndDate > GETDATE() " + strcond;


                strsql += " ORDER BY cp.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaignPromotion = (from DataRow dr in dt.Rows

                                      select new PromotionListReturn()
                                      {
                                          CampaignPromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                          PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                          CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                          PromotionName = dr["PromotionName"].ToString().Trim(),
                                          PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                          PicturePromotionUrl = dr["PicturePromotionUrl"].ToString().Trim(),
                                          PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                          LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                          LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                          FreeShippingCode = dr["FreeShipping"].ToString().Trim(),
                                          MOQFlag = dr["MOQFlag"].ToString().Trim(),
                                          GroupPrice = (dr["GroupPrice"].ToString() != "") ? Convert.ToDouble(dr["GroupPrice"]) : 0,
                                          CreateBy = dr["CreateBy"].ToString(),
                                          CreateDate = dr["CreateDate"].ToString(),
                                          UpdateBy = dr["UpdateBy"].ToString(),
                                          UpdateDate = dr["UpdateDate"].ToString(),
                                      }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaignPromotion;
        }

        public List<PromotionListReturn> ListWFCampaignPromotionByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += (strcond == "") ? " where  cp.Id = '" + pInfo.CampaignPromotionId + "'" : " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }
            if ((pInfo.CampaignPromotionId != null) && (pInfo.CampaignPromotionId != 0))
            {
                strcond += (strcond == "") ? " where  cp.Id = '" + pInfo.CampaignPromotionId + "'" : " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'" : " and  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'" : " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += (strcond == "") ? " where  cp.Active = '" + pInfo.Active + "'" : " and  cp.CampaignCode = '" + pInfo.Active + "'";
            }
            if ((pInfo.Status != null) && (pInfo.Status != 0))
            {
                strcond += (strcond == "") ? " where  w.Status = '" + pInfo.Status + "'" : " and  w.Status = '" + pInfo.Status + "'";
            }

            DataTable dt = new DataTable();
            var LCampaignPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select cp.* ,p.PromotionName, p.PromotionDesc,p.PromotionTypeCode, c.CampaignName, t.PromotionTypeName, e.EmpFname_TH as name, e.EmpLName_TH as lastname  from " + dbName + ".dbo.CampaignPromotion cp " +


                                 " left join " + dbName + ".dbo.Campaign c on c.CampaignCode = cp.CampaignCode " +

                                 " left join " + dbName + ".dbo.Promotion p on p.PromotionCode = cp.PromotionCode " +

                                 " left join " + dbName + ".dbo.PromotionType t on t.PromotionTypeCode = p.PromotionTypeCode " +

                                 " left join " + dbName + ".dbo.Emp e On cp.CreateBy = e.EmpCode " +

                                 " left join " + dbName + ".dbo.WF_Task_List w On cp.Id = w.OMS_Id " +

                                 strcond;


                strsql += " ORDER BY cp.Id DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaignPromotion = (from DataRow dr in dt.Rows

                                      select new PromotionListReturn()
                                      {
                                          CampaignPromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                          PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                          CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                          PromotionName = dr["PromotionName"].ToString().Trim(),
                                          CampaignName = dr["CampaignName"].ToString().Trim(),
                                          PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                          PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                          PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                          CreateBy = dr["CreateBy"].ToString(),
                                          CreateDate = dr["CreateDate"].ToString(),
                                          UpdateBy = dr["UpdateBy"].ToString(),
                                          UpdateDate = dr["UpdateDate"].ToString(),
                                          Name = dr["name"].ToString(),
                                          Lastname = dr["lastname"].ToString()
                                      }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaignPromotion;
        }

        public List<PromotionListReturn> ListDeatilCampaignPromotionByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += (strcond == "") ? " where  cp.Id = '" + pInfo.CampaignPromotionId + "'" : " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }
            if ((pInfo.CampaignPromotionId != null) && (pInfo.CampaignPromotionId != 0))
            {
                strcond += (strcond == "") ? " where  cp.Id = '" + pInfo.CampaignPromotionId + "'" : " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }
            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'" : " and  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'" : " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += (strcond == "") ? " where  cp.Active = '" + pInfo.Active + "'" : " and  cp.Active = '" + pInfo.Active + "'";
            }

            DataTable dt = new DataTable();
            var LCampaignPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " select cp.* ,p.PromotionName, p.PromotionDesc,p.PromotionTypeCode, c.CampaignName, t.PromotionTypeName, e.EmpFname_TH as name, e.EmpLName_TH as lastname, c.NotifyDate, c.ExpireDate from " + dbName + ".dbo.CampaignPromotion cp " +


                                 " left join " + dbName + ".dbo.Campaign c on c.CampaignCode = cp.CampaignCode " +

                                 " left join " + dbName + ".dbo.Promotion p on p.PromotionCode = cp.PromotionCode " +

                                 " left join " + dbName + ".dbo.PromotionType t on t.PromotionTypeCode = p.PromotionTypeCode " +

                                 " left join " + dbName + ".dbo.Emp e On cp.CreateBy = e.EmpCode " +

                                 strcond;


                strsql += " ORDER BY cp.Id DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaignPromotion = (from DataRow dr in dt.Rows

                                      select new PromotionListReturn()
                                      {
                                          CampaignPromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                          PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                          CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                          PromotionName = dr["PromotionName"].ToString().Trim(),
                                          CampaignName = dr["CampaignName"].ToString().Trim(),
                                          PromotionDesc = dr["PromotionDesc"].ToString().Trim(),
                                          PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                          PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                          NotifyDate = dr["NotifyDate"].ToString(),
                                          ExpireDate = dr["ExpireDate"].ToString(),
                                          CreateBy = dr["CreateBy"].ToString(),
                                          CreateDate = dr["CreateDate"].ToString(),
                                          UpdateBy = dr["UpdateBy"].ToString(),
                                          UpdateDate = dr["UpdateDate"].ToString(),
                                          Name = dr["name"].ToString(),
                                          Lastname = dr["lastname"].ToString()
                                      }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaignPromotion;
        }

        public int? CountWFCampaignPromotionListByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += (strcond == "") ? " where  cp.Id = '" + pInfo.CampaignPromotionId + "'" : " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'" : " and  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'" : " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += (strcond == "") ? " where  cp.Active = '" + pInfo.Active + "'" : " and  cp.Active = '" + pInfo.Active + "'";
            }
            if ((pInfo.Status != null) && (pInfo.Status != 0))
            {
                strcond += (strcond == "") ? " where  w.Status = '" + pInfo.Status + "'" : " and  w.Status = '" + pInfo.Status + "'";
            }

            DataTable dt = new DataTable();
            var LPromotion = new List<PromotionListReturn>();


            try
            {
                string strsql = "select count(cp.Id) as countCampaignPromotion from " + dbName + ".dbo.CampaignPromotion cp " +

                                 " left join " + dbName + ".dbo.Campaign c on c.CampaignCode = cp.CampaignCode " +

                                 " left join " + dbName + ".dbo.Promotion p on p.PromotionCode = cp.PromotionCode " +

                                 " left join " + dbName + ".dbo.WF_Task_List w On cp.Id = w.OMS_Id " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPromotion = (from DataRow dr in dt.Rows

                              select new PromotionListReturn()
                              {
                                  countCampaignPromotion = Convert.ToInt32(dr["countCampaignPromotion"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LPromotion.Count > 0)
            {
                count = LPromotion[0].countCampaignPromotion;
            }

            return count;
        }
        public int InsertCampProVer02(PromotionInfo mInfo)
        {
            int i = 0;

            string strsql = "insert into campaignpromotion (CampaignCode, PromotionCode, CreateDate, CreateBy, UpdateDate, UpDateBy, Active) values (" +
                             "'" + mInfo.CampaignCode + "', " +
                             "(SELECT PromotionCode FROM Promotion WHERE (Id = " + mInfo.PromotionId + ")), " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + mInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + mInfo.UpdateBy + "', " +
                             "'" + mInfo.Active + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;

        }

        public List<PromotionListReturn> ListNopagingDistinctCampaignPromotion(PromotionInfo pInfo)
        {
            string strcond = "";

            
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += (strcond == "") ? " where  CampaignCode like '%" + pInfo.CampaignCode + "%'" : " and  CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
    
            DataTable dt = new DataTable();
            var LCampaignPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " SELECT DISTINCT PromotionCode  from " + dbName + ".dbo.CampaignPromotion  " +
                                strcond;

                //strsql += " ORDER BY cp.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaignPromotion = (from DataRow dr in dt.Rows

                                      select new PromotionListReturn()
                                      {
                                          //CampaignPromotionId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                          PromotionCode = dr["PromotionCode"].ToString().Trim(),

                                      }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaignPromotion;
        }
        public List<PromotionListReturn> ListPromotionTypeByCampaignNopagingByCriteria(PromotionInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.PromotionId != null) && (pInfo.PromotionId != 0))
            {
                strcond += (strcond == "") ? " where  cp.Id = '" + pInfo.CampaignPromotionId + "'" : " and  cp.Id = '" + pInfo.CampaignPromotionId + "'";
            }

            if ((pInfo.PromotionCode != null) && (pInfo.PromotionCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'" : " and  cp.PromotionCode like '%" + pInfo.PromotionCode + "%'";
            }
            if ((pInfo.PromotionName != null) && (pInfo.PromotionName != ""))
            {
                strcond += (strcond == "") ? " where  p.PromotionName like '%" + pInfo.PromotionName + "%'" : " and  p.PromotionName like '%" + pInfo.PromotionName + "%'";
            }
            if ((pInfo.CampaignCode != null) && (pInfo.CampaignCode != ""))
            {
                strcond += (strcond == "") ? " where  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'" : " and  cp.CampaignCode like '%" + pInfo.CampaignCode + "%'";
            }
            if ((pInfo.Active != null) && (pInfo.Active != ""))
            {
                strcond += (strcond == "") ? " where  cp.Active = '" + pInfo.Active + "'" : " and  cp.Active = '" + pInfo.Active + "'";
            }
            DataTable dt = new DataTable();
            var LCampaignPromotion = new List<PromotionListReturn>();

            try
            {
                string strsql = " SELECT DISTINCT cp.CampaignCode, c.CampaignName, p.PromotionTypeCode, pt.PromotionTypeName, pt.PromotionTypeImageUrl FROM " + dbName + ".dbo.CampaignPromotion AS cp " +
                                " left join " + dbName + ".dbo.Promotion AS p ON p.PromotionCode = cp.PromotionCode " +
                                " left join " + dbName + ".dbo.Campaign AS c ON c.CampaignCode = cp.CampaignCode " +
                                " left join " + dbName + ".dbo.PromotionType AS pt ON pt.PromotionTypeCode = p.PromotionTypeCode " + strcond;
                                
                if (strcond == "")
                {
                    strsql = " where (cp.Active = 'Y') AND (c.FlagDelete = 'N') AND (p.FlagDelete = 'N')";
                }
                else
                {
                    strsql = strsql + " and (cp.Active = 'Y') AND (c.FlagDelete = 'N') AND (p.FlagDelete = 'N')";
                }

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaignPromotion = (from DataRow dr in dt.Rows

                                      select new PromotionListReturn()
                                      {
                                          CampaignCode = dr["CampaignCode"].ToString().Trim(),
                                          CampaignName = dr["CampaignName"].ToString().Trim(),
                                          PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                          PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                          PicturePromotionTypeUrl = dr["PromotionTypeImageUrl"].ToString().Trim(),

                                      }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaignPromotion;
        }

    }
}

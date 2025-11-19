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
    public class CampaignCategoryDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        String ServerpathPic = ConfigurationManager.AppSettings["ServerpathPic"];
        public int UpdateCampaignCategory(CampaignCategoryInfo cInfo)
        {
            int i = 0;
            
            string strsql = " Update " + dbName + ".dbo.CampaignCategory set " +
                           " CampaignCategoryCode = '" + cInfo.CampaignCategoryCode + "'," +
                           " CamCate_name = '" + cInfo.CampaignCategoryName + "'," +
                           " PictureCampaignUrl = '" + cInfo.PictureCampaignUrl + "'," +
                          
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.CampaignCategoryId + "";
            

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteCampaignCategory(CampaignCategoryInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.CampaignCategory set FlagDelete = 'Y' where Id in (" + cInfo.CampaignCategoryIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertCampaignCategory(CampaignCategoryInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO CampaignCategory  (MerchantMapCode,CampaignCategoryCode,CamCate_name,PictureCampaignUrl,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.MerchantMapCode + "'," +
                           "'" + cInfo.CampaignCategoryCode + "'," +
                           "'" + cInfo.CampaignCategoryName + "'," +
                           "'" + cInfo.PictureCampaignUrl + "'," +
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

        public List<CampaignCategoryReturn> ListCampaignCategoryNoPagingByCriteria(CampaignCategoryInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CampaignCategoryId != null) && (cInfo.CampaignCategoryId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CampaignCategoryId;
            }
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode +"' ";
            }
            if ((cInfo.CampaignCategoryCode != null) && (cInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  c.CampaignCategoryCode like '%" + cInfo.CampaignCategoryCode + "%'";
            }
            if ((cInfo.CampaignCategoryName != null) && (cInfo.CampaignCategoryName != ""))
            {
                strcond += " and  c.CamCate_name like '%" + cInfo.CampaignCategoryName + "%'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignCategoryReturn>();

            try
            {
                string strsql = "  select c.id,c.CamCate_name,c.CampaignCategoryCode,c.PictureCampaignUrl,c.CreateBy,c.CreateDate,c.UpdateBy,c.UpdateDate,c.FlagDelete from " + dbName + ".dbo.CampaignCategory c " +
                    "  inner join " + dbName + ".dbo.ProductBrand p on p.ProductBrandCode = c.CampaignCategoryCode and p.FlagDelete = 'N' " +
                    "  left join " + dbName + ".dbo.CampaignCategoryImage ci on ci.CampaignCategoryCode =c.CampaignCategoryCode" +
                              
                                "  where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                            select new CampaignCategoryReturn()
                            {
                                CampaignCategoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                CampaignCategoryName = dr["CamCate_name"].ToString().Trim(),
                                PictureCampaignUrl = ServerpathPic+dr["PictureCampaignUrl"].ToString().Trim(),
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

        public List<CampaignCategoryReturn> ListCampaignCategoryPagingByCriteria(CampaignCategoryInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CampaignCategoryId != null) && (cInfo.CampaignCategoryId != 0))
            {
                strcond += " and  c.Id = '" + cInfo.CampaignCategoryId + "'";
            }
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode+"'";
            }
            if ((cInfo.CampaignCategoryCode != null) && (cInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  c.CampaignCategoryCode like '%" + cInfo.CampaignCategoryCode + "%'";
            }
            if ((cInfo.CampaignCategoryName != null) && (cInfo.CampaignCategoryName != ""))
            {
                strcond += " and  c.CamCate_name like '%" + cInfo.CampaignCategoryName + "%'";
            }

           

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignCategoryReturn>();

            try
            {
                string strsql = "  select c.id,c.CamCate_name,c.CampaignCategoryCode,c.CreateBy,c.CreateDate,c.UpdateBy,c.UpdateDate,c.FlagDelete from " + dbName + ".dbo.CampaignCategory c " +
                   "  inner join " + dbName + ".dbo.ProductBrand p on p.ProductBrandCode = c.CampaignCategoryCode and p.FlagDelete = 'N' " +
                    " left join OMS_ASTON_DEV.dbo.CampaignCategoryImage ci on ci.CampaignCategoryCode =c.CampaignCategoryCode " +
                               "  where c.FlagDelete ='N' " + strcond;

         

                strsql += " ORDER BY c.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignCategoryReturn()
                             {
                                 CampaignCategoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                 CampaignCategoryName = dr["CamCate_name"].ToString().Trim(),
                               //  PictureCampaignUrl = ServerpathPic + dr["CampaignCategoryImageUrl"].ToString().Trim(),
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

        public int? CountCampaignCategoryListByCriteria(CampaignCategoryInfo cInfo) {
            string strcond = "";
            int? count = 0;

            if ((cInfo.CampaignCategoryId != null) && (cInfo.CampaignCategoryId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CampaignCategoryId;
            }
            if ((cInfo.MerchantMapCode != null) && (cInfo.MerchantMapCode != ""))
            {
                strcond += " and  c.MerchantMapCode = '" + cInfo.MerchantMapCode + "'";
            }
            if ((cInfo.CampaignCategoryCode != null) && (cInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  c.CampaignCategoryCode like '%" + cInfo.CampaignCategoryCode + "%'";
            }
            if ((cInfo.CampaignCategoryName != null) && (cInfo.CampaignCategoryName != ""))
            {
                strcond += " and  c.CamCate_name like '%" + cInfo.CampaignCategoryName + "%'";
            }

          

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignCategoryReturn>();


            try
            {
                string strsql = "select count(c.Id) as countCampaignCategory from " + dbName + ".dbo.CampaignCategory c " +
                   "  inner join " + dbName + ".dbo.ProductBrand p on p.ProductBrandCode = c.CampaignCategoryCode and p.FlagDelete = 'N' " +
                    " left join OMS_ASTON_DEV.dbo.CampaignCategoryImage ci on ci.CampaignCategoryCode =c.CampaignCategoryCode  " +
                               "  where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignCategoryReturn()
                             {
                                 countCampaignCategory = Convert.ToInt32(dr["countCampaignCategory"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCampaign.Count > 0)
            {
                count = LCampaign[0].countCampaignCategory;
            }

            return count;
        }



     
        public List<CampaignCategoryReturn> CampaignCategoryCodeValidateInsert(CampaignCategoryInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CampaignCategoryCode != null) && (cInfo.CampaignCategoryCode != ""))
            {
                strcond += " and  c.CampaignCategoryCode = '" + cInfo.CampaignCategoryCode + "'";
            }
            if ((cInfo.CampaignCategoryName != null) && (cInfo.CampaignCategoryName != ""))
            {
                strcond += " and  c.CamCate_name = '" + cInfo.CampaignCategoryName + "'";
            }

            DataTable dt = new DataTable();
            var LCampaign = new List<CampaignCategoryReturn>();

            try
            {
                string strsql = " select c.CampaignCategoryCode from " + dbName + ".dbo.CampaignCategory c " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCampaign = (from DataRow dr in dt.Rows

                             select new CampaignCategoryReturn()
                             {
                                 CampaignCategoryCode = dr["CampaignCategoryCode"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCampaign;
        }

        public int InsertCampaignCategoryImport(List<CampaignCategoryInfo> lcinfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;

            foreach (var info in lcinfo.ToList())
            {
                strsql = "insert into " + dbName + ".dbo.CampaignCategory (CampaignCategoryCode,CamCate_name,PictureCampaignUrl,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete) values (" +
                             "'" + info.CampaignCategoryCode + "', " +
                             "'" + info.CampaignCategoryName + "', " +
                             "'" + info.PictureCampaignUrl + "', " +                             
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

        public int? InsertCampaignCategoryImage(CampaignCategoryImageListInfo pInfo)
        {
            int? i = 0;

            string strsql = "INSERT INTO " + dbName + " .dbo.CampaignCategoryImage  (CampaignCategoryCode,CampaignCategoryImageUrl,CampaignCategoryImageName,FlagDelete)" +
                           "VALUES (" +
                          "'" + pInfo.CampaignCategoryCode + "'," +
                          "'" + pInfo.CampaignCategoryImageUrl + "'," +
                          "'" + pInfo.CampaignCategoryImageName + "'," +
                          "'" + pInfo.FlagDelete + "')";

            DataTable dt = new DataTable();
            var LCampaignCategoryImage = new List<CampaignCategoryImageListInfo>();

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdateCampaignCategoryImage(CampaignCategoryImageListInfo pInfo)
        {
            int? i = 0;
            string strsql = "";

            if ((pInfo.CampaignCategoryImageName != "") && (pInfo.CampaignCategoryImageName != null))
            {
                if ((pInfo.CampaignCategoryImageUrl != "") && (pInfo.CampaignCategoryImageUrl != null))
                {
                    strsql = " Update " + dbName + ".dbo.CampaignCategoryImage set " +
                                    " CampaignCategoryImageUrl = '" + pInfo.CampaignCategoryImageUrl + "'," +
                                    " CampaignCategoryImageName = '" + pInfo.CampaignCategoryImageName + "'," +
                                    " FlagDelete = '" + pInfo.FlagDelete + "'" +
                                    " where CampaignCategoryCode ='" + pInfo.CampaignCategoryCode + "'";
                }
            }
            else
            {
                strsql = " Update " + dbName + ".dbo.CampaignCategoryImage set " +
                                //" CampaignCategoryImageUrl = '" + pInfo.CampaignCategoryImageUrl + "'," +
                                //" CampaignCategoryImageName = '" + pInfo.CampaignCategoryImageName + "'" +
                                " FlagDelete = '" + pInfo.FlagDelete + "'" +
                                " where CampaignCategoryCode ='" + pInfo.CampaignCategoryCode + "'";
            }

            DataTable dt = new DataTable();
            var LCampaignCategoryImage = new List<CampaignCategoryImageListInfo>();

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }

}

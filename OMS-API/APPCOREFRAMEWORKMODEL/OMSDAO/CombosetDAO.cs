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
    public class CombosetDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<CombosetReturn> ListCombosetByCriteria(CombosetInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PromotionDetailName != null) && (cInfo.PromotionDetailName != ""))
            {
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.PromotionDetailName + "%'";
            }

            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }

            if ((cInfo.CombosetCode != null) && (cInfo.CombosetCode != ""))
            {
                
                strcond += " and  pmd.CombosetCode like '%" + cInfo.CombosetCode + "%'";
            }

            if ((cInfo.CombosetName != null) && (cInfo.CombosetName != ""))
            {
                
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.CombosetName + "%'";
            }

            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != "") && (cInfo.PromotionCode != "-99"))
            {
                strcond += " and  cb.PromotionCode = '" + cInfo.PromotionCode + "'";
            }

            DataTable dt = new DataTable();
            var LAddress = new List<CombosetReturn>();

            try
            {
                string strsql = " select pmd.CombosetCode, pmd.FlagDelete, pmd.PromotionDetailName,pmd.Price,c.CampaignName,c.CampaignCode,pmd.id,cp.PromotionCode, pm.ProductBrandCode, pm.PromotionCode, pm.PromotionName " +
                    " from Campaign  c inner join CampaignPromotion cp on cp.CampaignCode = c.CampaignCode " +
                    "inner join PromotionDetailInfo pmd on pmd.PromotionCode = cp.PromotionCode  " +
                    "left join Promotion pm on pm.PromotionCode = cp.PromotionCode  " +
                                " where c.FlagComboset ='Y' and c.FlagDelete='N' and cp.Active = 'Y' and pmd.PromotionDetailName is not null and pmd.PromotionDetailName != '' and pmd.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY pmd.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";
                
                


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LAddress = (from DataRow dr in dt.Rows

                             select new CombosetReturn()
                             {
                                 
                                 PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                 
                                 CombosetId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                 PromotionName = dr["PromotionName"].ToString().Trim(),
                                 CombosetCode = dr["CombosetCode"].ToString().Trim(),
                                                                 
                                 CombosetName = dr["PromotionDetailName"].ToString().Trim(),
                                 CombosetPrice = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                 
                                 FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                 ProductBrandCode = dr["ProductBrandCode"].ToString().Trim(),
                                 

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LAddress;
        }

        public List<CombosetReturn> ListCombosetNopagingByCriteria(CombosetInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PromotionDetailName != null) && (cInfo.PromotionDetailName != ""))
            {
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.PromotionDetailName + "%'";
            }

            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }

            if ((cInfo.CombosetCode != null) && (cInfo.CombosetCode != ""))
            {
                
                strcond += " and  pmd.CombosetCode like '%" + cInfo.CombosetCode + "%'";
            }

            if ((cInfo.CombosetName != null) && (cInfo.CombosetName != ""))
            {
                
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.CombosetName + "%'";
            }

            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != "") && (cInfo.PromotionCode != "-99"))
            {
                strcond += " and  pmd.PromotionCode = '" + cInfo.PromotionCode + "'";
            }

            DataTable dt = new DataTable();
            var LAddress = new List<CombosetReturn>();

            try
            {
                string strsql = " select pmd.CombosetCode, pmd.FlagDelete, pmd.PromotionDetailName,pmd.Price,pmd.id " +
                    " from  " +
                    " PromotionDetailInfo pmd   " +

                                " where  pmd.FlagDelete='N'     " + strcond;

                strsql += " ORDER BY pmd.Id";

                


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LAddress = (from DataRow dr in dt.Rows

                            select new CombosetReturn()
                            {
                                
                                PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                
                                CombosetId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                
                                CombosetCode = dr["CombosetCode"].ToString().Trim(),
                                                                 
                                CombosetName = dr["PromotionDetailName"].ToString().Trim(),
                                CombosetPrice = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                
                                FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LAddress;
        }

        public int? CountCombosetByCriteria(CombosetInfo cInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((cInfo.PromotionDetailName != null) && (cInfo.PromotionDetailName != ""))
            {
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.PromotionDetailName + "%'";
            }

            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }

            if ((cInfo.CombosetCode != null) && (cInfo.CombosetCode != ""))
            {
                
                strcond += " and  pmd.CombosetCode like '%" + cInfo.CombosetCode + "%'";
            }

            if ((cInfo.CombosetName != null) && (cInfo.CombosetName != ""))
            {
                
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.CombosetName + "%'";
            }

            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != "") && (cInfo.PromotionCode != "-99"))
            {
                strcond += " and  p.PromotionCode = '" + cInfo.PromotionCode + "'";
            }

            DataTable dt = new DataTable();
            var LAddress = new List<CombosetReturn>();

            try
            {
                string strsql = " select count(pmd.Id) as countComboset " +
                     " from Campaign  c inner join CampaignPromotion cp on cp.CampaignCode = c.CampaignCode " +
                     "inner join PromotionDetailInfo pmd on pmd.PromotionCode = cp.PromotionCode  " +
                     "left join Promotion pm on pm.PromotionCode = cp.PromotionCode  " +
                                 " where c.FlagComboset ='Y' and c.FlagDelete='N' and cp.Active = 'Y' and pmd.PromotionDetailName is not null and pmd.PromotionDetailName != ''  and pmd.FlagDelete = 'N' " + strcond;

                

              


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LAddress = (from DataRow dr in dt.Rows

                            select new CombosetReturn()
                            {

                                countComboset = Convert.ToInt32(dr["countComboset"])


                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }



            if (LAddress.Count > 0)
            {
                count = LAddress[0].countComboset;
            }

            return count;
        }

        public int InsertComboset(CombosetInfo cInfo)
        {
            int i;

            string strsql = "INSERT INTO Comboset  (PromotionCode,CombosetCode,CombosetName,CombosetPrice,CreateDate,CreateBy,UpdateDate,FlagDelete)" +
                           "VALUES (" +
                          "'" + cInfo.PromotionCode + "'," +
                          "'" + cInfo.CombosetCode + "'," +
                             "'" + cInfo.CombosetName + "'," +
                          "" + cInfo.CombosetPrice + "," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.FlagDelete + "'" +
                           ")";




            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateComboset(CombosetInfo cInfo)
        {
            int i;

            string strsql = " Update " + dbName + ".dbo.Comboset set " +
                            " PromotionCode = '" + cInfo.PromotionCode + "'," +
                            " CombosetName = '" + cInfo.CombosetName + "'," +
                            " CombosetCode = '" + cInfo.CombosetCode + "'," +
                            " CombosetPrice = " + cInfo.CombosetPrice + "," +                          
                           " UpdateBy = '" + cInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + cInfo.CombosetId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteComboset(CombosetInfo cInfo)
        {
            int i;

            string strsql = " Update " + dbName + ".dbo.Comboset set " +
                            " Flagdelete = 'Y'" +
                           " where Id =" + cInfo.CombosetId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteCombosetByCode(CombosetInfo cInfo)
        {
            int i;

            string strsql = " Update " + dbName + ".dbo.Comboset set " +
                            " Flagdelete = 'Y'" +
                           " where Id in (" + cInfo.CombosetCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<CombosetReturn> ListPromotionCombosetNopagingByCriteria(CombosetInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PromotionDetailName != null) && (cInfo.PromotionDetailName != ""))
            {
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.PromotionDetailName + "%'";
            }

            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }

            if ((cInfo.CombosetCode != null) && (cInfo.CombosetCode != ""))
            {
                
                strcond += " and  pmd.CombosetCode like '%" + cInfo.CombosetCode + "%'";
            }

            if ((cInfo.CombosetName != null) && (cInfo.CombosetName != ""))
            {
                
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.CombosetName + "%'";
            }

            if ((cInfo.PromotionCode != null) && (cInfo.PromotionCode != "") && (cInfo.PromotionCode != "-99"))
            {
                strcond += " and  p.PromotionCode = '" + cInfo.PromotionCode + "'";
            }

            DataTable dt = new DataTable();
            var LAddress = new List<CombosetReturn>();

            try
            {
                
                
                
                
                

                

                string strsql = "select cb.*, pm.PromotionName, pm.ProductBrandCode from " + dbName + ".dbo.Comboset cb " +
                                "left join Promotion pm on pm.PromotionCode = cb.PromotionCode  " +
                                "where cb.FlagDelete = 'N'" + strcond;

                strsql += " ORDER BY cb.Id ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LAddress = (from DataRow dr in dt.Rows

                            select new CombosetReturn()
                            {
                                
                                PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),
                                
                                
                                CombosetId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                PromotionName = dr["PromotionName"].ToString().Trim(),
                                CombosetCode = dr["CombosetCode"].ToString().Trim(),
                                                               
                                CombosetName = dr["PromotionDetailName"].ToString().Trim(),
                                CombosetPrice = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                
                                FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                ProductBrandCode = dr["ProductBrandCode"].ToString().Trim(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LAddress;
        }
        public List<CombosetReturn> ListCombosetByCriteriaMaster(CombosetInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PromotionDetailName != null) && (cInfo.PromotionDetailName != ""))
            {
                strcond += " and  pmd.PromotionDetailName like '%" + cInfo.PromotionDetailName + "%'";
            }

            if ((cInfo.CampaignName != null) && (cInfo.CampaignName != ""))
            {
                strcond += " and  c.CampaignName like '%" + cInfo.CampaignName + "%'";
            }

            if ((cInfo.CombosetCode != null) && (cInfo.CombosetCode != ""))
            {
                strcond += " and  pmd.CombosetCode like '%" + cInfo.CombosetCode + "%'";
            }

            if ((cInfo.CombosetName != null) && (cInfo.CombosetName != ""))
            {
                 strcond += " and  pmd.PromotionDetailName like '%" + cInfo.CombosetName + "%'";
            }
            if (((cInfo.StartDatePromotionCombo != "") && (cInfo.StartDatePromotionCombo != null)) && ((cInfo.EndDatePromotionCombo != "") && (cInfo.EndDatePromotionCombo != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + cInfo.StartDatePromotionCombo + "',103) AND CONVERT(DATETIME,'" + cInfo.StartDatePromotionCombo + " 23:59:59:999',103)";
            }
          
            if ((cInfo.FlagActive != null) && (cInfo.FlagActive != "") && (cInfo.FlagActive != "-99"))
            {
                strcond += " and  p.flagActive like '%" + cInfo.FlagActive + "%'";
            }

            DataTable dt = new DataTable();
            var LAddress = new List<CombosetReturn>();

            try
            {
                string strsql = " select   pmd.CombosetCode, pmd.FlagDelete," +
                    "  pmd.PromotionDetailName, pmd.Price,pmd.Id,convert(varchar, pmd.StartDatePromotionCombo, 103) as StartDatePromotionCombo,convert(varchar,pmd.EndDatePromotionCombo, 103) as  EndDatePromotionCombo, pmd.flagactive" +
                    "    from " + dbName + ".dbo.PromotionDetailInfo pmd  " +

                                " where  pmd.FlagDelete = 'N' and (pmd.PromotionCode ='' or pmd.PromotionCode is null)" + strcond;

                strsql += " ORDER BY pmd.Id DESC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LAddress = (from DataRow dr in dt.Rows

                            select new CombosetReturn()
                            {
                                PromotionDetailName = dr["PromotionDetailName"].ToString().Trim(),                           
                                CombosetId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,                         
                                CombosetCode = dr["CombosetCode"].ToString().Trim(),                                                      
                                CombosetName = dr["PromotionDetailName"].ToString().Trim(),
                                CombosetPrice = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,    
                                StartDatePromotionCombo = (dr["StartDatePromotionCombo"].ToString() != "") ?(dr["StartDatePromotionCombo"].ToString()) : "",
                                EndDatePromotionCombo = (dr["EndDatePromotionCombo"].ToString() != "") ? (dr["EndDatePromotionCombo"].ToString()) : "",
                                FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                FlagActive = (dr["FlagActive"].ToString() != "") ? (dr["FlagActive"].ToString()) : "",

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LAddress;
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
    }
}

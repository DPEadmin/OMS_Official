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
    public class DiscountBillDetailDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateDiscountBillDetailInfo(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.DiscountBillDetail set " +
                            " ProductCode = '" + pdInfo.ProductCode + "'," +
                            " DiscountBillCode = '" + pdInfo.DiscountBillCode + "'," +
                            " Price  = " + pdInfo.Price + "," +
                            " DiscountPercent = " + pdInfo.DiscountPercent + "," +
                            " DiscountAmount = " + pdInfo.DiscountAmount + "," +
                           
                            " DefaultAmount = " + pdInfo.DefaultAmount + "," +
                           " ComplementaryAmount = " + pdInfo.ComplementaryAmount + "," +
                           " UpdateBy = '" + pdInfo.UpdateBy + "'," +
                        
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + pdInfo.DiscountBillDetailId + "";
          

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertDiscountBillDetail(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO DiscountBillDetail  (ProductCode,DiscountBillCode, Price,DiscountPercent,DiscountAmount,DefaultAmount,ComplementaryAmount,CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pdInfo.ProductCode + "'," +
                           "'" + pdInfo.DiscountBillCode + "'," +
                     
                           "'" + pdInfo.Price + "'," +
                           "'" + pdInfo.DiscountPercent + "'," +
                           "'" + pdInfo.DiscountAmount + "'," +
                     
                           "'" + pdInfo.DefaultAmount + "'," +
                           "'" + pdInfo.ComplementaryAmount + "'," +
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

        public int DeleteDiscountBillDetailInfo(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.DiscountBillDetail set FlagDelete = 'Y' where Id in (" + pdInfo.DiscountBillDetailId + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountDiscountBillDetailByCampaign(DiscountBillDetailInfo pdInfo)
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
            if ((pdInfo.DiscountBillCode != null) && (pdInfo.DiscountBillCode != ""))
            {
                strcond += " and  c.DiscountBillCode like '%" + pdInfo.DiscountBillCode + "%'";
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
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " select count(c.Id) as countDiscountBillDetail from " + dbName + ".dbo.DiscountBillDetail c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                 " left join DiscountBill promo on promo.DiscountBillCode = c.DiscountBillCode and promo.FlagDelete = 'N'" +
                                " left join CampaignDiscountBill as cp on cp.DiscountBillCode = promo.DiscountBillCode " +
                               " left join Campaign as ca on ca.CampaignCode  = cp.CampaignCode and ca.flagdelete = 'N' and ca.Active = 'Y' " +
                               " where c.FlagDelete ='N' " + strcond;

             
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                         
                                              countDiscountBillDetail = Convert.ToInt32(dr["countDiscountBillDetail"])

                                   }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LDiscountBillDetail.Count > 0)
            {
                count = LDiscountBillDetail[0].countDiscountBillDetail;
            }

            return count;
        }

        public List<DiscountBillDetailListReturn> ListDiscountBillDetailByCampaign(DiscountBillDetailListReturn pdInfo)
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
            if ((pdInfo.DiscountBillCode != null) && (pdInfo.DiscountBillCode != ""))
            {
                strcond += " and  c.DiscountBillCode like '%" + pdInfo.DiscountBillCode + "%'";
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
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " select ca.CampaignCode,ca.CampaignName,c.Id, c.ProductCode, c.DiscountBillCode,c.DiscountBillDetailName, pro.Price, c.DiscountPercent, c.DiscountAmount, c.LockAmountFlag, c.LockCheckbox, c.DefaultAmount, " +
                                 "c.ComplementaryAmount, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, pro.ProductName,pro.ProductDesc,pro.AllergyRemark, promo.DiscountBillName, " +
                                "  u.LookupValue AS UnitName, c.ComplementaryFlag from " + dbName + ".dbo.DiscountBillDetail c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                 " left join DiscountBill promo on promo.DiscountBillCode = c.DiscountBillCode and promo.FlagDelete = 'N'" +
                                " left join CampaignDiscountBill as cp on cp.DiscountBillCode = promo.DiscountBillCode " +
                               " left join Campaign as ca on ca.CampaignCode  = cp.CampaignCode and ca.flagdelete = 'N' and ca.Active = 'Y' " +
                               " where c.FlagDelete ='N' and cp.Active='Y' " + strcond;

                strsql += " ORDER BY c.Id ";
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                              
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
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

            return LDiscountBillDetail;
        }
               
        public List<DiscountBillDetailListReturn> ListProducttionDetailByCriteria(DiscountBillDetailInfo pdInfo)
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
            if ((pdInfo.DiscountBillCode != null) && (pdInfo.DiscountBillCode != ""))
            {
                strcond += " and  c.DiscountBillCode like '%" + pdInfo.DiscountBillCode + "%'";
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
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " SELECT        c.Id, c.ProductCode, c.DiscountBillCode, c.Price, c.DiscountPercent, c.DiscountAmount " +
                                " , c.DefaultAmount, c.ComplementaryAmount, c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy,  " +
                                " c.FlagDelete, pro.ProductName, pro.MerchantCode, pro.SupplierCode,pro.ProductCategoryCode, pro.ProductBrandCode," +
                                "  pb.ProductBrandName,promo.DiscountBillName, mech.MerchantName, procat.ProductCategoryName,u.LookupValue AS UnitName  from " + dbName + ".dbo.DiscountBillDetail c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
                                " left join Merchant mech on mech.MerchantCode = pro.MerchantCode " +
                                " left join DiscountBill promo on promo.DiscountBillCode = c.DiscountBillCode and promo.FlagDelete = 'N' and c.FlagDelete = 'N' " +
                                " left join ProductCategory as procat on pro.ProductCategoryCode = procat.ProductCategoryCode " +
                                " left join ProductBrand pb on pb.ProductBrandCode = pro.ProductBrandCode " +
                             
                                " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + pdInfo.rowOFFSet + " ROWS FETCH NEXT " + pdInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                             select new DiscountBillDetailListReturn()
                             {
                                 DiscountBillDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                 DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 UnitName = dr["UnitName"].ToString().Trim(),
                                 Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                 DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                 DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                 DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 ProductCategoryCode = dr["ProductCategoryCode"].ToString(),
                                 ProductCategoryName = dr["ProductCategoryName"].ToString(),
                                 ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                 ProductBrandName = dr["ProductBrandName"].ToString(),
                                 
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDiscountBillDetail;
        }

        public int? CountDiscountBillDetailListByCriteria(DiscountBillDetailInfo pInfo)
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

            if ((pInfo.DiscountBillCode != null) && (pInfo.DiscountBillCode != ""))
            {
                strcond += " and  c.DiscountBillCode like '%" + pInfo.DiscountBillCode + "%'";

            }

            if ((pInfo.DiscountBillName != null) && (pInfo.DiscountBillName != ""))
            {
                strcond += " and  c.DiscountBillName like '%" + pInfo.DiscountBillName + "%'";
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
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countDiscountBillDetail from " + dbName + ".dbo.DiscountBillDetail c " +

                             " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join DiscountBill promo on promo.DiscountBillCode = c.DiscountBillCode " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        countDiscountBillDetail = Convert.ToInt32(dr["countDiscountBillDetail"])

                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LDiscountBillDetail.Count > 0)
            {
                count = LDiscountBillDetail[0].countDiscountBillDetail;
            }

            return count;
        }

        public List<DiscountBillDetailListReturn> GetDiscountBilldetailDropDownList(DiscountBillDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + pdInfo.ProductCode + "%'";
            }
            if ((pdInfo.DiscountBillCode != null) && (pdInfo.DiscountBillCode != ""))
            {
                strcond += " and  c.DiscountBillCode like '%" + pdInfo.DiscountBillCode + "%'";
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
            if ((pdInfo.DiscountBillName != null) && (pdInfo.DiscountBillName != ""))
            {
                strcond += " and  c.DiscountBillName like '%" + pdInfo.DiscountBillName + "%'";
            }

            DataTable dt = new DataTable();
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " select c.*, pro.ProductName, promo.DiscountBillName from " + dbName + ".dbo.DiscountBillDetail c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join DiscountBill promo on promo.DiscountBillCode = c.DiscountBillCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
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

            return LDiscountBillDetail;
        }

        public List<DiscountBillDetailListReturn> GetDiscountBillCodeDistinctdetailList(DiscountBillDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.ProductCode != null) && (pdInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + pdInfo.ProductCode + "%'";
            }
            if ((pdInfo.DiscountBillCode != null) && (pdInfo.DiscountBillCode != ""))
            {
                strcond += " and  c.DiscountBillCode like '%" + pdInfo.DiscountBillCode + "%'";
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
            if ((pdInfo.DiscountBillName != null) && (pdInfo.DiscountBillName != ""))
            {
                strcond += " and  c.DiscountBillName like '%" + pdInfo.DiscountBillName + "%'";
            }

            DataTable dt = new DataTable();
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " SELECT DISTINCT c.DiscountBillCode, promo.DiscountBillName from " + dbName + ".dbo.DiscountBillDetail c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join DiscountBill promo on promo.DiscountBillCode = c.DiscountBillCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.DiscountBillCode DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDiscountBillDetail;
        }

        public int UpdateDiscountBillDetailAmount(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.DiscountBillDetail set " +
                            " DefaultAmount = " + pdInfo.DefaultAmount + "," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pdInfo.DiscountBillDetailId + "";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteDiscountBillDetailInfoByCode(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.DiscountBillDetail set FlagDelete = 'Y' where DiscountBillCode ='" + pdInfo.DiscountBillCode + "' and FlagDelete = 'N'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateComplementaryFlag(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.DiscountBillDetail set " +
                            " ComplementaryFlag = '" + pdInfo.ComplementaryFlag + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pdInfo.DiscountBillDetailId + "";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateDiscountBillDetailDiscount(DiscountBillDetailInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.DiscountBillDetail set " +
                            " DiscountAmount = " + pInfo.DiscountAmount + "," +
                            " DiscountPercent = " + pInfo.DiscountPercent + "," +
                            " LockAmountFlag = '" + pInfo.LockAmountFlag + "'," +
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where DiscountBillCode = '" + pInfo.DiscountBillCode + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<DiscountBillDetailListReturn> ListProducttionDetailNoPagingByCriteria(DiscountBillDetailInfo pdInfo)
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
            if ((pdInfo.DiscountBillCode != null) && (pdInfo.DiscountBillCode != ""))
            {
                strcond += " and  c.DiscountBillCode like '%" + pdInfo.DiscountBillCode + "%'";
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
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " SELECT        c.Id, c.ProductCode, c.DiscountBillCode, c.Price, c.DiscountPercent, c.DiscountAmount, " +
                                "promo.DiscountPercent AS DiscountBillDiscountPercent, promo.DiscountAmount AS DiscountBillDiscountAmount,  " +
                                " c.DefaultAmount, c.ComplementaryAmount,c.CreateDate, c.CreateBy, c.UpdateDate, c.UpdateBy, c.FlagDelete, pro.ProductName, " +
                                " pro.Unit, pro.MerchantCode, pro.SupplierCode,pro.ProductCategoryCode, pro.ProductBrandCode, pb.ProductBrandName, promo.DiscountBillName, " +
                                "procat.ProductCategoryName,promo.DiscountBillTypeCode, pt.DisCountBillTypeName, promo.FreeShipping,u.LookupValue AS UnitName, c.ComplementaryFlag" +
                                " from " + dbName + ".dbo.DiscountBillDetail c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join lookup u on u.LookupCode = pro.Unit and u.LookupType = 'UNIT' " +
         
                                " left join DiscountBill promo on promo.DiscountBillCode = c.DiscountBillCode and promo.FlagDelete = 'N' and c.FlagDelete = 'N' " +
                                " left join ProductCategory as procat on pro.ProductCategoryCode = procat.ProductCategoryCode " +
                                " left join ProductBrand pb on pb.ProductBrandCode = pro.ProductBrandCode " +
                                " left join Channel cn on cn.ChannelCode = pro.ChannelCode " +

                                " left join DiscountBillType AS pt ON pt.DiscountBillTypeCode = promo.DiscountBillTypeCode " +
                                " where c.FlagDelete ='N'  " + strcond;

                strsql += " ORDER BY c.Id";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
                                        ProductName = dr["ProductName"].ToString().Trim(),
                                        Unit = dr["Unit"].ToString().Trim(),
                                        UnitName = dr["UnitName"].ToString().Trim(),                                        
                                        Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                        DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                        DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,                                  
                                        DefaultAmount = (dr["DefaultAmount"].ToString() != "") ? Convert.ToInt32(dr["DefaultAmount"]) : 0,                                    
                                        CreateBy = dr["CreateBy"].ToString(),
                                        CreateDate = dr["CreateDate"].ToString(),
                                        UpdateBy = dr["UpdateBy"].ToString(),
                                        UpdateDate = dr["UpdateDate"].ToString(),
                                        FlagDelete = dr["FlagDelete"].ToString().Trim(),                                     
                                        ProductCategoryCode = dr["ProductCategoryCode"].ToString(),
                                        ProductCategoryName = dr["ProductCategoryName"].ToString(),
                                        ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                        ProductBrandName = dr["ProductBrandName"].ToString(),
                                        ComplementaryFlag = dr["ComplementaryFlag"].ToString(),                                  
                                        DiscountBillTypeCode = dr["DiscountBillTypeCode"].ToString().Trim(),
                                        DiscountBillTypeName = dr["DiscountBillTypeName"].ToString().Trim(),
                                        FreeShippingCode = dr["FreeShipping"].ToString().Trim(),
                                
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDiscountBillDetail;
        }

        public int UpdateDiscountBillDetailName(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.DiscountBillDetail set " +
                           
                           " UpdateBy = '" + pdInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where DiscountBillCode = '" + pdInfo.DiscountBillCode + "'";
            //removed single quotes on decimal param query strings

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteDiscountBillDetailInfoByIdString(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.DiscountBillDetail set FlagDelete = 'Y' where Id in (" + pdInfo.DiscountBillCode + ") ";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<DiscountBillDetailListReturn> ListDiscountBillDetailRecipeinCampaignProductTakeOrderCriteria(DiscountBillDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.CampaignCode != null) && (pdInfo.CampaignCode != ""))
            {
                strcond += " and  cp.CampaignCode like '%" + pdInfo.CampaignCode + "%'";
            }
            

            DataTable dt = new DataTable();
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " SELECT distinct p.ProductCode, cp.CampaignCode, c.CampaignName, d.Id, cp.DiscountBillCode, pr.DiscountBillName, d.Price, d.DiscountPercent, " +
                                " d.DiscountAmount, d.LockAmountFlag, d.LockCheckbox, d.DefaultAmount, d.ComplementaryAmount, p.ProductName, " +
                                " d.CreateDate, d.CreateBy, d.UpdateDate, d.UpdateBy, d.FlagDelete, u.LookupValue AS UnitName, d.ComplementaryFlag " +
                                " FROM            Product AS p LEFT OUTER JOIN " +
                                " DiscountBillDetail AS d ON d.ProductCode = p.ProductCode LEFT OUTER JOIN " +
                                " CampaignDiscountBill AS cp ON cp.DiscountBillCode = d.DiscountBillCode LEFT OUTER JOIN " +
                                " Campaign AS c ON c.CampaignCode = cp.CampaignCode LEFT OUTER JOIN " +
                                " DiscountBill AS pr ON pr.DiscountBillCode = cp.DiscountBillCode LEFT OUTER JOIN " +
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
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
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

            return LDiscountBillDetail;
        }

        public List<DiscountBillDetailListReturn> ListDiscountBillDetailbyProductinRecipeCampaignProductTakeOrderCriteria(DiscountBillDetailInfo pdInfo)
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
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " SELECT ca.CampaignCode, ca.CampaignName, c.Id, c.ProductCode, c.DiscountBillCode, c.DiscountBillDetailName, pro.Price, c.DiscountPercent, " +
                                " c.DiscountAmount, c.LockAmountFlag, c.LockCheckbox, c.DefaultAmount, c.ComplementaryAmount, c.CreateDate, c.CreateBy, c.UpdateDate, " +
                                " c.UpdateBy, c.FlagDelete, pro.ProductName, promo.DiscountBillName, u.LookupValue AS UnitName, c.ComplementaryFlag " +
                                " FROM            DiscountBillDetail AS c LEFT OUTER JOIN " +
                                " Product AS pro ON pro.ProductCode = c.ProductCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = pro.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                                " DiscountBill AS promo ON promo.DiscountBillCode = c.DiscountBillCode AND promo.FlagDelete = 'N' LEFT OUTER JOIN " +
                                " CampaignDiscountBill AS cp ON cp.DiscountBillCode = promo.DiscountBillCode LEFT OUTER JOIN " +
                                " Campaign AS ca ON ca.CampaignCode = cp.CampaignCode AND ca.FlagDelete = 'N' AND ca.Active = 'Y' " +
                                " WHERE (c.FlagDelete = 'N') AND (c.FlagDelete = 'N') ";

                strsql = strsql + strcond;

                strsql = strsql + " ORDER BY c.Id desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
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

            return LDiscountBillDetail;
        }

        public int InsertDiscountBillDetailCombo(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO DiscountBillDetail  (DiscountBillCode, DiscountBillDetailName, Price, CombosetCode, ProductCode, CreateDate,CreateBy,FlagDelete)" +
                            "VALUES (" +                           
                           "'" + pdInfo.DiscountBillCode + "'," +
                          
                           "" + pdInfo.Price + "," +
                           "'" + pdInfo.CombosetCode + "'," +
                           "'" + pdInfo.ProductCode + "'," +
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

        public int UpdateDiscountBillDetailCombo(DiscountBillDetailInfo pdInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.DiscountBillDetail set " +                        
                            " DiscountBillCode = '" + pdInfo.DiscountBillCode + "'," +
                            " ProductCode = '" + pdInfo.ProductCode + "'," +
                           
                            " Price  = " + pdInfo.Price + "," +
                            " CombosetCode = '" + pdInfo.CombosetCode + "'," +                            
                           " UpdateBy = '" + pdInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pdInfo.DiscountBillDetailId + "";
            

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<DiscountBillDetailListReturn> DiscountBillDetailfromProductTop5(DiscountBillDetailInfo pdInfo)
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
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " SELECT d.Id,d.DiscountBillCode,d.ProductCode,p.ProductName,d.DiscountAmount,d.DiscountPercent,d.Price,cp.CampaignCode,l.LookupValue AS UnitName, " +
                                " c.CampaignName,c.CampaignCategory,ct.CamCate_name,pr.DiscountBillName,d.LockCheckbox,d.LockAmountFlag,p.ProductDesc FROM DiscountBillDetail AS d LEFT OUTER JOIN " +
                                " Product AS p ON p.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                " CampaignDiscountBill AS cp ON cp.DiscountBillCode = d.DiscountBillCode LEFT OUTER JOIN " +
                                " Campaign AS c ON c.CampaignCode = cp.CampaignCode LEFT OUTER JOIN " +
                                " DiscountBill AS pr ON pr.DiscountBillCode = cp.DiscountBillCode LEFT OUTER JOIN " +
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
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
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

            return LDiscountBillDetail;
        }

        public List<DiscountBillDetailListReturn> DiscountBillDetailfromLatestOrderbyCustomer(DiscountBillDetailInfo pdInfo)
        {
            string strcond = "";

            if ((pdInfo.CustomerCode != null) && (pdInfo.CustomerCode != ""))
            {
                strcond += " and  o.CustomerCode = '" + pdInfo.CustomerCode + "'";
            }

            DataTable dt = new DataTable();
            var LDiscountBillDetail = new List<DiscountBillDetailListReturn>();

            try
            {
                string strsql = " SELECT o.Id, d.DiscountBillDetailId, o.CustomerCode, d.OrderCode, d.ProductCode, pd.ProductName, pd.ProductDesc, d.DiscountBillCode, pm.DiscountBillName, pd.Unit, u.LookupValue AS UnitName, CustomerCode, d.FlagCombo," +
                                " d.Price, d.Amount, pt.DiscountPercent, pt.DiscountAmount, pt.LockAmountFlag, pt.LockCheckbox, d.CampaignCode, c.CampaignName, o.CampaignCategoryCode, cg.CamCate_name, pt.CombosetCode, pt.DiscountBillDetailName, o.RunningNo FROM OrderDetail AS d LEFT OUTER JOIN " +
                                " OrderInfo AS o ON o.OrderCode = d.OrderCode LEFT OUTER JOIN " +
                                " DiscountBill AS pm ON pm.DiscountBillCode = d.DiscountBillCode LEFT OUTER JOIN " +
                                " Product AS pd ON pd.ProductCode = d.ProductCode LEFT OUTER JOIN " +
                                " Lookup AS u ON u.LookupCode = pd.Unit AND u.LookupType = 'UNIT' LEFT OUTER JOIN " +
                                " DiscountBillDetail AS pt ON pt.Id = d.Id LEFT OUTER JOIN " +
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
                LDiscountBillDetail = (from DataRow dr in dt.Rows

                                    select new DiscountBillDetailListReturn()
                                    {
                                        DiscountBillDetailId = (dr["DiscountBillDetailId"].ToString() != "") ? Convert.ToInt32(dr["DiscountBillDetailId"]) : 0,
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                        DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
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
                                        ComboName = dr["DiscountBillDetailName"].ToString(),
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

            return LDiscountBillDetail;
        }
    }
}

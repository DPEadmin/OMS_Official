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
    public class ComplementaryDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int InsertComplementary(ComplementaryInfo cpmInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Complementary  (ProductCode,Amount,PromotionDetailInfoId,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cpmInfo.ProductCode + "'," +
                           "'" + cpmInfo.Amount + "'," +
                           "'" + cpmInfo.PromotionDetailInfoId + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cpmInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cpmInfo.UpdateBy + "'," +
                           "'" + cpmInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<ComplementaryListReturn> ListComplementaryByCriteria(ComplementaryInfo cpmInfo) {
            string strcond = "";

            if ((cpmInfo.ProductCode != null) && (cpmInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + cpmInfo.ProductCode + "%'";
            }
            if ((cpmInfo.Amount != null) && (cpmInfo.Amount != ""))
            {
                strcond += " and  c.Amount like '%" + cpmInfo.Amount + "%'";
            }

            if ((cpmInfo.PromotionDetailInfoId != null) && (cpmInfo.PromotionDetailInfoId != ""))
            {
                strcond += " and  c.PromotionDetailInfoId = '" + cpmInfo.PromotionDetailInfoId + "'";

            }

            DataTable dt = new DataTable();
            var LComplementary = new List<ComplementaryListReturn>();

            try
            {
                //string strsql = " select * from " + dbName + ".dbo.Complementary c " +
                //                " left join Product pro on pro.ProductCode = c.ProductCode " +
                //               " where c.FlagDelete ='N' " + strcond;
                string strsql = " select c.*,pro.ProductName, pro.MerchantCode, m.MerchantName, pro.Unit, pro.Price, u.LookupValue as UnitName, pc.ProductCategoryName, pro.ProductBrandCode, pb.ProductBrandName from " + dbName + ".dbo.Complementary c " +
                               " left join Product pro on pro.ProductCode = c.ProductCode " +
                               " left join Merchant m on m.MerchantCode = pro.MerchantCode " +
                               " left join Lookup u on u.LookupCode = pro.Unit and LookupType = 'UNIT' " +
                               " left join ProductCategory pc on pc.ProductCategoryCode = pro.ProductCategoryCode " +
                               " left join ProductBrand pb on pb.ProductBrandCode = pro.ProductBrandCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + cpmInfo.rowOFFSet + " ROWS FETCH NEXT " + cpmInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LComplementary = (from DataRow dr in dt.Rows

                             select new ComplementaryListReturn()
                             {
                                 ComplementaryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 ProductCode = dr["ProductCode"].ToString().Trim(),
                                 ProductName = dr["ProductName"].ToString().Trim(),
                                 Amount = dr["Amount"].ToString().Trim(),
                                 PromotionDetailInfoId = dr["PromotionDetailInfoId"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                 ProductCategoryName = dr["ProductCategoryName"].ToString(),
                                 UnitName = dr["UnitName"].ToString(),
                                 ProductBrandCode = dr["ProductBrandCode"].ToString(),
                                 ProductBrandName = dr["ProductBrandName"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LComplementary;
        }

        public int? CountComplementaryListByCriteria(ComplementaryInfo cpmInfo)
        {
            string strcond = "";
            int? count = 0;


            if ((cpmInfo.ProductCode != null) && (cpmInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + cpmInfo.ProductCode + "%'";
            }
            if ((cpmInfo.Amount != null) && (cpmInfo.Amount != ""))
            {
                strcond += " and  c.Amount like '%" + cpmInfo.Amount + "%'";
            }

            if ((cpmInfo.PromotionDetailInfoId != null) && (cpmInfo.PromotionDetailInfoId != ""))
            {
                strcond += " and  c.PromotionDetailInfoId like '%" + cpmInfo.PromotionDetailInfoId + "%'";

            }

            DataTable dt = new DataTable();
            var LComplementary = new List<ComplementaryListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countComplementaryInfo from " + dbName + ".dbo.Complementary c " +

                            " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LComplementary = (from DataRow dr in dt.Rows

                             select new ComplementaryListReturn()
                             {
                                 countComplementaryInfo = Convert.ToInt32(dr["countComplementaryInfo"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LComplementary.Count > 0)
            {
                count = LComplementary[0].countComplementaryInfo;
            }

            return count;
        }

        public List<ComplementaryListReturn> ListComplementarynopagingByCriteria(ComplementaryInfo cpmInfo)
        {
            string strcond = "";

            if ((cpmInfo.ProductCode != null) && (cpmInfo.ProductCode != ""))
            {
                strcond += " and  c.ProductCode like '%" + cpmInfo.ProductCode + "%'";
            }
            if ((cpmInfo.Amount != null) && (cpmInfo.Amount != ""))
            {
                strcond += " and  c.Amount like '%" + cpmInfo.Amount + "%'";
            }

            if ((cpmInfo.PromotionDetailInfoId != null) && (cpmInfo.PromotionDetailInfoId != ""))
            {
                strcond += " and  c.PromotionDetailInfoId = '" + cpmInfo.PromotionDetailInfoId + "'";

            }

            DataTable dt = new DataTable();
            var LComplementary = new List<ComplementaryListReturn>();

            try
            {
                string strsql = " select c.*,pro.ProductName, pro.MerchantCode, m.MerchantName, pro.Unit, pro.Price, u.LookupValue as UnitName, pc.ProductCategoryName, p.PromotionCode from " + dbName + ".dbo.Complementary c " +
                                " left join Product pro on pro.ProductCode = c.ProductCode " +
                                " left join Merchant m on m.MerchantCode = pro.MerchantCode " +
                                " left join Lookup u on u.LookupCode = pro.Unit and LookupType = 'UNIT' " +
                                " left join ProductCategory pc on pc.ProductCategoryCode = pro.ProductCategoryCode " +
                                " left join PromotionDetailInfo d on d.Id = c.PromotionDetailInfoId " +
                                " left join Promotion p on p.PromotionCode = d.PromotionCode " +
                                " where c.FlagDelete ='N' and p.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY c.Id DESC";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LComplementary = (from DataRow dr in dt.Rows

                                  select new ComplementaryListReturn()
                                  {
                                      ComplementaryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                      ProductCode = dr["ProductCode"].ToString().Trim(),
                                      ProductName = dr["ProductName"].ToString().Trim(),
                                      MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                      MerchantName = dr["MerchantName"].ToString().Trim(),
                                      Unit = dr["Unit"].ToString().Trim(),
                                      UnitName = dr["UnitName"].ToString().Trim(),
                                      Amount = dr["Amount"].ToString().Trim(),
                                      PromotionDetailInfoId = dr["PromotionDetailInfoId"].ToString().Trim(),
                                      PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                      CreateBy = dr["CreateBy"].ToString(),
                                      CreateDate = dr["CreateDate"].ToString(),
                                      UpdateBy = dr["UpdateBy"].ToString(),
                                      UpdateDate = dr["UpdateDate"].ToString(),
                                      FlagDelete = dr["FlagDelete"].ToString(),
                                      Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                      ProductCategoryName = dr["ProductCategoryName"].ToString(),

                                  }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LComplementary;
        }

        public int UpdateComplementary(ComplementaryInfo cpmInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Complementary set " +
                            " ProductCode = '" + cpmInfo.ProductCode + "'," +
                            " Amount = '" + cpmInfo.Amount + "'," +
                            " PromotionDetailInfoId = '" + cpmInfo.PromotionDetailInfoId + "'," +
                           " UpdateBy = '" + cpmInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + cpmInfo.ComplementaryId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteComplementary(ComplementaryInfo cpmInfo)
        {
            string strsql = "";
            int i = 0;

            if ((cpmInfo.ComplementaryId_List != null) && (cpmInfo.ComplementaryId_List != ""))
            {
                strsql = "Update " + dbName + ".dbo.Complementary set FlagDelete = 'Y' where Id in (" + cpmInfo.ComplementaryId_List + ")";
            }
            else
            {
                strsql = "Update " + dbName + ".dbo.Complementary set FlagDelete = 'Y' where Id in (" + cpmInfo.ComplementaryId + ")";
            }

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}

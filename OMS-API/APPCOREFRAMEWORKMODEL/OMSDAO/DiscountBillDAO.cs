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
    public class DiscountBillDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateDiscountBill(DiscountBillInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.DiscountBill set " +
                            " DiscountBillCode = '" + pInfo.DiscountBillCode + "'," +
                            " DiscountBillName = '" + pInfo.DiscountBillName + "'," +
                            " DiscountBillTypeCode = '" + pInfo.DiscountBillTypeCode + "'," +                     
                            " FreeShipping = '" + pInfo.FreeShipping + "'," +                  
                            " Active = '" + pInfo.Active + "'," +                  
                            " DiscountAmount = " + pInfo.DiscountAmount + "," +                      
                            " DiscountPercent = " + pInfo.DiscountPercent + "," +
                            " MinimumTotPrice = " + pInfo.MinimumTotPrice + "," +
                            
                            " StartDate = CONVERT(DATETIME, '" + pInfo.StartDate + "', 103)," +
                            " EndDate = CONVERT(DATETIME, '" + pInfo.EndDate + "', 103)," +

                            " BrandCode = '" + pInfo.BrandCode + "'," +
                           
                           " UpdateBy = '" + pInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where Id =" + pInfo.DiscountBillId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteDiscountBill(DiscountBillInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.DiscountBill set FlagDelete = 'Y' where Id in (" + pInfo.DiscountBillId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertDiscountBill(DiscountBillInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO DiscountBill  (MerchantCode,DiscountBillCode,DiscountBillName,DiscountBillTypeCode,CreateDate,CreateBy,FlagDelete, FreeShipping," +
                            " DiscountAmount, DiscountPercent," +
                            " MinimumTotPrice, StartDate, EndDate,BrandCode,Active)" +
                            "VALUES (" +
                           "'" + pInfo.MerchantCode + "'," +
                           "'" + pInfo.DiscountBillCode + "'," +
                           "'" + pInfo.DiscountBillName + "'," +
                      
                           "'" + pInfo.DiscountBillTypeCode + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                
                           "'" + pInfo.FlagDelete + "'," +
                           "'" + pInfo.FreeShipping + "'," +
                          
                         
                            pInfo.DiscountAmount + "," +
                            pInfo.DiscountPercent + "," +
                            pInfo.MinimumTotPrice + "," +
                    
                           "CONVERT(DATETIME, '" + pInfo.StartDate + "',103), " +
                           "CONVERT(DATETIME, '" + pInfo.EndDate + "',103), " +
                           "'" + pInfo.BrandCode + "'," +
                           "'" + pInfo.Active + "'" +

                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<DiscountBillListReturn> ListDiscountBillNoPagingByCriteria(DiscountBillInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.DiscountBillId != null) && (pInfo.DiscountBillId != 0))
            {
                strcond += " and  p.Id =" + pInfo.DiscountBillId;
            }

            if ((pInfo.DiscountBillCode != null) && (pInfo.DiscountBillCode != ""))
            {
                strcond += " and  p.DiscountBillCode like '%" + pInfo.DiscountBillCode + "%'";
            }
            if ((pInfo.DiscountBillName != null) && (pInfo.DiscountBillName != ""))
            {
                strcond += " and  p.DiscountBillName like '%" + pInfo.DiscountBillName + "%'";
            }

            if ((pInfo.DiscountBillTypeCode != null) && (pInfo.DiscountBillTypeCode != "") && (pInfo.DiscountBillTypeCode != "-99"))
            {
                strcond += " and  p.DiscountBillTypeCode = '" + pInfo.DiscountBillTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.EndDate != "") && (pInfo.EndDate != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDate + " 23:59:59:999',103)";
            }


            if ((pInfo.BrandCode != null) && (pInfo.BrandCode != "") && (pInfo.BrandCode != "-99"))
            {
                strcond += " and  p.BrandCode like '%" + pInfo.BrandCode + "%'";
            }


            DataTable dt = new DataTable();
            var LDiscountBill = new List<DiscountBillListReturn>();

            try
            {
                string strsql = " select pb.ProductBrandName, t.DisCountBillTypeName, p.id, p.DiscountBillCode, p.CreateBy, p.UpdateDate, " +
                    "p.UpdateBy, p.FlagDelete, p.createdate, p.DiscountBillName, p.DiscountPercent, p.DiscountAmount, p.MinimumTotPrice," +
                    "  p.Brandcode, p.StartDate, p.EndDate, p.FreeShipping, p.DiscountBillTypeCode, p.Active " +
                    "from " + dbName + ".dbo.DiscountBill p " +
                                " inner join  DiscountBillType AS t ON t.DisCountBillTypeCode = p.DiscountBillTypeCode AND t.FlagDelete = 'N'  " +
                                 "  inner join  ProductBrand AS pb ON pb.ProductBrandCode = p.Brandcode" +
                             
                               " where p.FlagDelete ='N' and p.Active = 'Y'" + strcond;

                strsql += " ORDER BY p.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBill = (from DataRow dr in dt.Rows

                             select new DiscountBillListReturn()
                             {
                                 DiscountBillId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                 DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
                                 DiscountBillTypeCode = dr["DiscountBillTypeCode"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),                               
                                 UpdateBy = dr["UpdateBy"].ToString(),                             
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 FreeShipping = dr["FreeShipping"].ToString(),                                                      
                                 DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                 DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,                             
                                 MinimumTotPrice = (dr["MinimumTotPrice"].ToString() != "") ? Convert.ToInt32(dr["MinimumTotPrice"]) : 0,                                
                                                             
                                 StartDate = dr["StartDate"].ToString(),
                                 EndDate = dr["EndDate"].ToString(),
                           

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDiscountBill;
        }

        public List<DiscountBillListReturn> ListDiscountBillList(DiscountBillInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.DiscountBillId != null) && (pInfo.DiscountBillId != 0))
            {
                strcond += " and  p.Id =" + pInfo.DiscountBillId;
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            if ((pInfo.DiscountBillCode != null) && (pInfo.DiscountBillCode != ""))
            {
                strcond += " and  p.DiscountBillCode like '%" + pInfo.DiscountBillCode + "%'";
            }
          
          //trimmed string make code cannot pass this condition
            if ((pInfo.DiscountBillName != null) && (pInfo.DiscountBillName != ""))
            {
                strcond += " and  p.DiscountBillName like '%" + pInfo.DiscountBillName.Trim() + "%'";
            }

            if ((pInfo.DiscountBillTypeCode != null) && (pInfo.DiscountBillTypeCode != "") && (pInfo.DiscountBillTypeCode != "-99"))
            {
                strcond += " and  p.DiscountBillTypeCode = '" + pInfo.DiscountBillTypeCode + "'";
            }
         
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.EndDate != "") && (pInfo.EndDate != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDate + " 23:59:59:999',103)";
            }


            if ((pInfo.BrandCode != null) && (pInfo.BrandCode != "") && (pInfo.BrandCode != "-99"))
            {
                strcond += " and  p.BrandCode like '%" + pInfo.BrandCode + "%'";
            }
            
            if ((pInfo.Active != null) && (pInfo.Active != "") && (pInfo.Active != "-99"))
            {
                strcond += " and  p.Active like '%" + pInfo.Active + "%'";
            }

            DataTable dt = new DataTable();
            var LDiscountBill = new List<DiscountBillListReturn>();

            try
            {
                string strsql = " select p.id, p.DiscountBillCode,t.DisCountBillTypeCode, t.DiscountBillTypeName,p.DiscountBillName" +
                                ",p.MinimumTotPrice,p.DiscountAmount,p.DiscountPercent,p.StartDate,p.EndDate ,p.FreeShipping,p.FlagDelete ,pb.ProductBrandName,p.BrandCode,t.flagaddproduct,p.Active,l.LookupValue as DISCOUNTBILLSTATUS " +
                                " from " + dbName + ".dbo.DiscountBill p " +
                                " left join DiscountBillType t on t.DiscountBillTypeCode =  p.DiscountBillTypeCode  and t.FlagDelete='N'" +
                                " left join Lookup as l on p.Active = l.LookupCode and l.LookupType like 'DISCOUNTBILLSTATUS' " +
                                " inner join ProductBrand pb on p.BrandCode =pb.ProductBrandCode " +
                     
                                " where p.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY p.Id DESC OFFSET " + pInfo.rowOFFSet + " ROWS FETCH NEXT " + pInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBill = (from DataRow dr in dt.Rows

                              select new DiscountBillListReturn()
                              {
                                  DiscountBillId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                  DiscountBillName = dr["DiscountBillName"].ToString().Trim(),
                                  DiscountBillTypeCode = dr["DiscountBillTypeCode"].ToString().Trim(),
                                  DiscountBillTypeName = dr["DiscountBillTypeName"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString(),
                                  FreeShipping = dr["FreeShipping"].ToString(),
                                  DiscountAmount = (dr["DiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["DiscountAmount"]) : 0,
                                  DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,
                                  MinimumTotPrice = (dr["MinimumTotPrice"].ToString() != "") ? Convert.ToInt32(dr["MinimumTotPrice"]) : 0,
                                  StartDate = dr["StartDate"].ToString(),
                                  EndDate = dr["EndDate"].ToString(),
                                  ProductBrandName = dr["ProductBrandName"].ToString(),
                                  BrandCode = dr["BrandCode"].ToString(),
                                  FlagAddProduct = dr["FlagAddProduct"].ToString(),
                                  DISCOUNTBILLSTATUS = dr["DISCOUNTBILLSTATUS"].ToString(),
                                  Active = dr["Active"].ToString(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDiscountBill;
        }

        

        public int? CountDiscountBillListByCriteria(DiscountBillInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.DiscountBillId != null) && (pInfo.DiscountBillId != 0))
            {
                strcond += " and  p.Id =" + pInfo.DiscountBillId;
            }

            if ((pInfo.DiscountBillCode != null) && (pInfo.DiscountBillCode != ""))
            {
                strcond += " and  p.DiscountBillCode like '%" + pInfo.DiscountBillCode + "%'";
            }
            if ((pInfo.DiscountBillName != null) && (pInfo.DiscountBillName != ""))
            {
                strcond += " and  p.DiscountBillName like '%" + pInfo.DiscountBillName + "%'";
            }

            if ((pInfo.DiscountBillTypeCode != null) && (pInfo.DiscountBillTypeCode != "") && (pInfo.DiscountBillTypeCode != "-99"))
            {
                strcond += " and  p.DiscountBillTypeCode = '" + pInfo.DiscountBillTypeCode + "'";
            }
            if ((pInfo.DiscountBillTypeCode != null) && (pInfo.DiscountBillTypeCode != ""))
            {
                strcond += " and  p.DiscountBillTypeCode = '" + pInfo.DiscountBillTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.EndDate != "") && (pInfo.EndDate != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDate + " 23:59:59:999',103)";
            }


            if ((pInfo.BrandCode != null) && (pInfo.BrandCode != "") && (pInfo.BrandCode != "-99"))
            {
                strcond += " and  p.BrandCode like '%" + pInfo.BrandCode + "%'";
            }


            DataTable dt = new DataTable();
            var LDiscountBill = new List<DiscountBillListReturn>();


            try
            {
                string strsql = "select count(p.Id) as countDiscountBill from " + dbName + ".dbo.DiscountBill p " +
                           " left join DiscountBillType t on t.DiscountBillTypeCode =  p.DiscountBillTypeCode " +
                           " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBill = (from DataRow dr in dt.Rows

                             select new DiscountBillListReturn()
                             {
                                 countDiscountBill = Convert.ToInt32(dr["countDiscountBill"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LDiscountBill.Count > 0)
            {
                count = LDiscountBill[0].countDiscountBill;
            }

            return count;
        }

        

        public int? CountListDiscountBill(DiscountBillInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.DiscountBillId != null) && (pInfo.DiscountBillId != 0))
            {
                strcond += " and  p.Id =" + pInfo.DiscountBillId;
            }
            if ((pInfo.MerchantCode != null) && (pInfo.MerchantCode != ""))
            {
                strcond += " and  p.MerchantCode = '" + pInfo.MerchantCode + "'";
            }
            if ((pInfo.DiscountBillCode != null) && (pInfo.DiscountBillCode != ""))
            {
                strcond += " and  p.DiscountBillCode like '%" + pInfo.DiscountBillCode + "%'";
            }
            if ((pInfo.DiscountBillName != null) && (pInfo.DiscountBillName != ""))
            {
                strcond += " and  p.DiscountBillName like '%" + pInfo.DiscountBillName + "%'";
            }

            if ((pInfo.DiscountBillTypeCode != null) && (pInfo.DiscountBillTypeCode != "") && (pInfo.DiscountBillTypeCode != "-99"))
            {
                strcond += " and  p.DiscountBillTypeCode = '" + pInfo.DiscountBillTypeCode + "'";
            }
            if (((pInfo.StartDate != "") && (pInfo.StartDate != null)) && ((pInfo.EndDate != "") && (pInfo.EndDate != null)))
            {
                strcond += " AND p.StartDate BETWEEN CONVERT(DATETIME, '" + pInfo.StartDate + "',103) AND CONVERT(DATETIME,'" + pInfo.EndDate + " 23:59:59:999',103)";
            }
            if ((pInfo.Active != null) && (pInfo.Active != "") && (pInfo.Active != "-99"))
            {
                strcond += " and  p.Active like '%" + pInfo.Active + "%'";
            }


            if ((pInfo.BrandCode != null) && (pInfo.BrandCode != "") && (pInfo.BrandCode != "-99"))
            {
                strcond += " and  p.BrandCode like '%" + pInfo.BrandCode + "%'";
            }
          

            DataTable dt = new DataTable();
            var LDiscountBill = new List<DiscountBillListReturn>();


            try
            {
                string strsql = "SELECT        COUNT(p.Id) AS countDiscountBill from " + dbName + ".dbo.DiscountBill p " +
                                " left join DiscountBillType t on t.DiscountBillTypeCode =  p.DiscountBillTypeCode  and t.FlagDelete='N' " +
                                " left join Lookup as l on p.Active = l.LookupCode and l.LookupType like 'DISCOUNTBILLSTATUS' " +
                                " where p.FlagDelete ='N' " + strcond;
       
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBill = (from DataRow dr in dt.Rows

                              select new DiscountBillListReturn()
                              {
                                  countDiscountBill = Convert.ToInt32(dr["countDiscountBill"])
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LDiscountBill.Count > 0)
            {
                count = LDiscountBill[0].countDiscountBill;
            }

            return count;
        }

        public List<DiscountBillTypeListReturn> ListDiscountBillType()
        {
            string strcond = "";

        
            DataTable dt = new DataTable();
            var LDiscountBill = new List<DiscountBillTypeListReturn>();

            try
            {
                string strsql = " select t.Id, t.DiscountBillTypeCode, t.DiscountBillTypeName from " + dbName + ".dbo.DiscountBillType t " +
                               " where t.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY t.Id";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBill = (from DataRow dr in dt.Rows

                              select new DiscountBillTypeListReturn()
                              {
                                  DiscountBillTypeId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  DiscountBillTypeCode = dr["DiscountBillTypeCode"].ToString().Trim(),
                                  DiscountBillTypeName = dr["DiscountBillTypeName"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDiscountBill;
        }

        public List<DiscountBillListReturn> DiscountBillCodeValidateInsert(DiscountBillInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.DiscountBillCode != null) && (pInfo.DiscountBillCode != ""))
            {
                strcond += " and  p.DiscountBillCode = '" + pInfo.DiscountBillCode + "'";
            }
            if ((pInfo.DiscountBillName != null) && (pInfo.DiscountBillName != ""))
            {
                strcond += " and  p.DiscountBillName = '" + pInfo.DiscountBillName + "'";
            }

            DataTable dt = new DataTable();
            var LDiscountBill = new List<DiscountBillListReturn>();

            try
            {
                string strsql = " select p.DiscountBillCode from " + dbName + ".dbo.DiscountBill p " +
                               " where p.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDiscountBill = (from DataRow dr in dt.Rows

                              select new DiscountBillListReturn()
                              {
                                  
                                  DiscountBillCode = dr["DiscountBillCode"].ToString().Trim(),
                                  
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDiscountBill;
        }
        

        public int DeleteDiscountBillList(DiscountBillInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.DiscountBill set FlagDelete = 'Y' where Id in (" + pInfo.DiscountBillCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }


    }
}

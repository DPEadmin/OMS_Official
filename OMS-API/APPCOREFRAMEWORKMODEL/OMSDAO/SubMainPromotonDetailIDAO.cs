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
    public class SubMainPromotonDetailIDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateSubMainPromotonDetail(SubMainPromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.SubMainPromotionDetailInfo set " +
                           " PromotionCode = '" + cInfo.PromotionCode + "'," +
                           " PromotionDetailId = '" + cInfo.PromotionDetailId + "'," +
                          " ProductCode = '" + cInfo.PromotionDetailId + "'," +
                          " Amount = '" + cInfo.PromotionDetailId + "'," +
                          " FlagSubPromotionDetailMain = '" + cInfo.FlagSubPromotionDetailMain + "'," +
                          " UpdateBy = '" + cInfo.UpdateBy + "'," +
                          " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                          " where Id =" + cInfo.SubMainId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSubMainPromotonDetail(SubMainPromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SubMainPromotionDetailInfo set FlagDelete = 'Y' where Id in (" + cInfo.SubMainIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSubMainPromotonDetailByCode(SubMainPromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SubMainPromotionDetailInfo set FlagDelete = 'Y' where PromotionDetailId in (" + cInfo.SubMainIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertSubMainPromotonDetail(SubMainPromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO SubMainPromotionDetailInfo  (PromotionCode,PromotionDetailId,ProductCode,Amount,FlagSubPromotionDetailMain,CreateDate,CreateBy,UpdateDate,UpdateBy,CombosetCode,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.PromotionCode + "'," +
                           "'" + cInfo.PromotionDetailId + "'," +
                              "'" + cInfo.ProductCode + "'," +
                           "'" + cInfo.Amount + "'," +
                              "'" + cInfo.FlagSubPromotionDetailMain + "'," +
          
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'" + cInfo.CombosetCode + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertSubExchangePromotionDetail(SubMainPromotionDetailInfo cInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO SubExchangePromotionDetailInfo  (PromotionCode,PromotionDetailId,ProductCode,Amount,FlagSubPromotionDetailMain,CreateDate,CreateBy,UpdateDate,UpdateBy,CombosetCode,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.PromotionCode + "'," +
                           "'" + cInfo.PromotionDetailId + "'," +
                              "'" + cInfo.ProductCode + "'," +
                           "'" + cInfo.Amount + "'," +
                              "'" + cInfo.FlagSubPromotionDetailMain + "'," +

                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'" + cInfo.CombosetCode + "'," +
                           "'N'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<SubMainPromotionDetailListReturn> ListMainExchangePromotionDetailNoPagingbyCriteria(SubMainPromotionDetailListReturn sInfo)
        {
            string strcond = "";

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != ""))
            {
                strcond += " and  pc.PromotionDetailId ='" + sInfo.PromotionDetailId + "'";
            }
           
            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  pc.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }

            if ((sInfo.CombosetCode != null) && (sInfo.CombosetCode != ""))
            {
                strcond += " and  pc.CombosetCode = '" + sInfo.CombosetCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubMainPromotionDetailListReturn>();

            try
            {
                string strsql = " select pc.id,pc.ProductCode,pc.Amount,pc.PromotionDetailId,p.ProductName from SubMainPromotionDetailInfo pc" +
                    " inner join Product p on p.ProductCode = pc.ProductCode " +
                  
                                " where pc.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY PromotionDetailId DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                                              select new SubMainPromotionDetailListReturn()
                                              {
                                                 
                                                  SubMainId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                                  PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),

                                                  //   PromotionCode = dr["PromotionCode"].ToString().Trim(),
                                                  ProductCode = dr["ProductCode"].ToString().Trim(),
                                                  ProductName = dr["Productname"].ToString().Trim(),
                                                  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]).ToString() : "0",

                                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ListSubPromotionDetailInfo;
        }

        public List<SubMainPromotionDetailListReturn> ListMainExchangePromotionDetailbyCriteria(SubMainPromotionDetailListReturn sInfo)
        {
            string strcond = "";

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != ""))
            {
                strcond += " and  pc.PromotionDetailId ='" + sInfo.PromotionDetailId + "'";
            }

            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  pc.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }

            if ((sInfo.CombosetCode != null) && (sInfo.CombosetCode != ""))
            {
                strcond += " and  pc.CombosetCode = '" + sInfo.CombosetCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubMainPromotionDetailListReturn>();

            try
            {
                string strsql = " select pc.id,pc.ProductCode,pc.Amount,pc.PromotionDetailId,p.ProductName from SubMainPromotionDetailInfo pc" +
                    " inner join Product p on p.ProductCode = pc.ProductCode " +

                                " where pc.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY PromotionDetailId DESC OFFSET " + sInfo.rowOFFSet + " ROWS FETCH NEXT " + sInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                                              select new SubMainPromotionDetailListReturn()
                                              {

                                                  SubMainId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                                  PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),

                                                  
                                                  ProductCode = dr["ProductCode"].ToString().Trim(),
                                                  ProductName = dr["Productname"].ToString().Trim(),
                                                  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]).ToString() : "0",

                                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ListSubPromotionDetailInfo;
        }

        public int? CountMainExchangePromotionDetailbyCriteria(SubMainPromotionDetailListReturn sInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != ""))
            {
                strcond += " and  pc.PromotionDetailId ='" + sInfo.PromotionDetailId + "'";
            }

            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  pc.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }

            if ((sInfo.CombosetCode != null) && (sInfo.CombosetCode != ""))
            {
                strcond += " and  pc.CombosetCode = '" + sInfo.CombosetCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubMainPromotionDetailListReturn>();

            try
            {
                string strsql = " select count(pc.id) as countSubMain  from SubMainPromotionDetailInfo pc" +
                    " inner join Product p on p.ProductCode = pc.ProductCode " +

                                " where pc.FlagDelete ='N' " + strcond;

               

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                                              select new SubMainPromotionDetailListReturn()
                                              {
                                                  countSubMain = (dr["countSubMain"].ToString() != "") ? Convert.ToInt32(dr["countSubMain"]) : 0,

                                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (ListSubPromotionDetailInfo.Count > 0) {
                count = ListSubPromotionDetailInfo[0].countSubMain;
            }

            return count;
        }

        public List<SubExchangePromotionDetailInfoListReturn> ListSubExchangePromotionDetailNoPagingbyCriteria(SubExchangePromotionDetailInfoListReturn sInfo)
        {
            string strcond = "";

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != ""))
            {
                strcond += " and  pc.PromotionDetailId ='" + sInfo.PromotionDetailId + "'";
            }

            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  pc.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }

            if ((sInfo.CombosetCode != null) && (sInfo.CombosetCode != ""))
            {
                strcond += " and  pc.CombosetCode = '" + sInfo.CombosetCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubExchangePromotionDetailInfoListReturn>();

            try
            {
                string strsql = " select pc.id,pc.ProductCode,pc.Amount,pc.PromotionDetailId,p.ProductName from SubExchangePromotionDetailInfo pc" +
                    " inner join Product p on p.ProductCode = pc.ProductCode " +

                                " where pc.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY PromotionDetailId DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                                              select new SubExchangePromotionDetailInfoListReturn()
                                              {
                                                  SubMainId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                                  PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),

                                                  
                                                  ProductCode = dr["ProductCode"].ToString().Trim(),
                                                  ProductName = dr["Productname"].ToString().Trim(),
                                                  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]).ToString() : "0",

                                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ListSubPromotionDetailInfo;
        }

        public List<SubExchangePromotionDetailInfoListReturn> ListSubExchangePromotionDetailbyCriteria(SubExchangePromotionDetailInfoListReturn sInfo)
        {
            string strcond = "";

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != ""))
            {
                strcond += " and  pc.PromotionDetailId ='" + sInfo.PromotionDetailId + "'";
            }

            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  pc.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }

            if ((sInfo.CombosetCode != null) && (sInfo.CombosetCode != ""))
            {
                strcond += " and  pc.CombosetCode = '" + sInfo.CombosetCode.Trim() + "'";
            }
            if ((sInfo.SubMainExchangeID != null) && (sInfo.SubMainExchangeID != ""))
            {
                strcond += " and  pc.SubMainExchangeID = '" + sInfo.SubMainExchangeID.Trim() + "'";
            }


            if ((sInfo.ProductCode != null) && (sInfo.ProductCode != ""))
            {
                strcond += " and  p.ProductCode ='" + sInfo.ProductCode + "'";
            }

            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubExchangePromotionDetailInfoListReturn>();

            try
            {
                string strsql = " select pc.id,pc.ProductCode,pc.Amount,pc.PromotionDetailId,p.ProductName from SubExchangePromotionDetailInfo pc" +
                    " inner join Product p on p.ProductCode = pc.ProductCode " +

                                " where pc.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY PromotionDetailId DESC OFFSET " + sInfo.rowOFFSet + " ROWS FETCH NEXT " + sInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                                              select new SubExchangePromotionDetailInfoListReturn()
                                              {
                                                  SubMainId = (dr["id"].ToString() != "") ? Convert.ToInt32(dr["id"]) : 0,
                                                  PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),

                                                  
                                                  ProductCode = dr["ProductCode"].ToString().Trim(),
                                                  ProductName = dr["Productname"].ToString().Trim(),
                                                  Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]).ToString() : "0",

                                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ListSubPromotionDetailInfo;
        }

        public int? CountSubExchangePromotionDetailbyCriteria(SubExchangePromotionDetailInfoListReturn sInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((sInfo.PromotionDetailId != null) && (sInfo.PromotionDetailId != ""))
            {
                strcond += " and  pc.PromotionDetailId ='" + sInfo.PromotionDetailId + "'";
            }

            if ((sInfo.PromotionCode != null) && (sInfo.PromotionCode != ""))
            {
                strcond += " and  pc.PromotionCode = '" + sInfo.PromotionCode.Trim() + "'";
            }

            if ((sInfo.CombosetCode != null) && (sInfo.CombosetCode != ""))
            {
                strcond += " and  pc.CombosetCode = '" + sInfo.CombosetCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var ListSubPromotionDetailInfo = new List<SubExchangePromotionDetailInfoListReturn>();

            try
            {
                string strsql = " select count(pc.id) as countSubMain from SubExchangePromotionDetailInfo pc" +
                    " inner join Product p on p.ProductCode = pc.ProductCode " +

                                " where pc.FlagDelete ='N' " + strcond;

             

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                ListSubPromotionDetailInfo = (from DataRow dr in dt.Rows

                                              select new SubExchangePromotionDetailInfoListReturn()
                                              {
                                                  countSubMain = (dr["countSubMain"].ToString() != "") ? Convert.ToInt32(dr["countSubMain"]) : 0,
                                                  

                                              }).ToList();

            }
            catch (Exception ex)
            {
                 throw new Exception(ex.Message, ex);
            }

            if (ListSubPromotionDetailInfo.Count > 0)
            {
                count = ListSubPromotionDetailInfo[0].countSubMain;
            }

            return count;
        }

        public int DeleteSubExchangePromotonDetail(SubExchangePromotionDetailInfoListReturn cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SubExchangePromotionDetailInfo set FlagDelete = 'Y' where Id in (" + cInfo.SubMainIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSubExchangePromotonDetailByCode(SubExchangePromotionDetailInfoListReturn cInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.SubExchangePromotionDetailInfo set FlagDelete = 'Y' where PromotionDetailId in (" + cInfo.SubMainIdDelete + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
    
}

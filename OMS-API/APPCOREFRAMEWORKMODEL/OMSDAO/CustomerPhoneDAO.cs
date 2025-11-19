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
    public class CustomerPhoneDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountCustomerPhoneListByCriteria(CustomerPhoneInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.CustomerCode != null) && (pInfo.CustomerCode != ""))
            {
                strcond += " and  cp.CustomerCode ='" + pInfo.CustomerCode + "'";
            }

            if ((pInfo.PhoneNumber != null) && (pInfo.PhoneNumber != ""))
            {
                strcond += " and  cp.PhoneNumber ='" + pInfo.PhoneNumber + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerPhone = new List<CustomerPhoneListReturn>();


            try
            {
                string strsql = " select count(cp.PhoneId) as countCustomerPhone    from " + dbName + ".dbo.CustomerPhone cp " +

                           "  join " + dbName + ".dbo.Customer c on c.CustomerCode =  cp.CustomerCode" +
                           "  join " + dbName + ".dbo.Lookup lu on cp.PhoneType=lu.LookupCode" +
                           " where cp.FlagDelete ='N'  and lu.LookupType='PhoneType'" + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerPhone = (from DataRow dr in dt.Rows

                             select new CustomerPhoneListReturn()
                             {
                                 countCustomerPhone = Convert.ToInt32(dr["countCustomerPhone"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCustomerPhone.Count > 0)
            {
                count = LCustomerPhone[0].countCustomerPhone;
            }

            return count;
        }

        public int? CountCustomerPhoneCallListByCriteria(CustomerPhoneInfo pInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((pInfo.CustomerCode != null) && (pInfo.CustomerCode != ""))
            {
                strcond += " and  cp.CustomerCode ='" + pInfo.CustomerCode + "'";
            }

            if ((pInfo.PhoneNumber != null) && (pInfo.PhoneNumber != ""))
            {
                strcond += " and  cp.PhoneNumber ='" + pInfo.PhoneNumber + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerPhone = new List<CustomerPhoneListReturn>();


            try
            {
                string strsql = " select count (distinct cp.CustomerCode) as countCustomerPhone    from " + dbName + ".dbo.CustomerPhone cp " +

                            "  join " + dbName + ".dbo.Customer c on c.CustomerCode =  cp.CustomerCode" +
                            "  join " + dbName + ".dbo.Lookup lu on cp.PhoneType=lu.LookupCode" +
                            
                            " where c.FlagDelete ='N'  and lu.LookupType='PhoneType' " + strcond ;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerPhone = (from DataRow dr in dt.Rows

                                  select new CustomerPhoneListReturn()
                                  {
                                      countCustomerPhone = Convert.ToInt32(dr["countCustomerPhone"])
                                  }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCustomerPhone.Count > 0)
            {
                count = LCustomerPhone[0].countCustomerPhone;
            }

            return count;
        }

        public List<CustomerPhoneListReturn> ListCustomerPhone(CustomerPhoneInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.CustomerFName != null) && (pInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + pInfo.CustomerFName + "%'";
            }

            if ((pInfo.CustomerLName != null) && (pInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + pInfo.CustomerLName + "%'";
            }

            if ((pInfo.CustomerCode != null) && (pInfo.CustomerCode != ""))
            {
                strcond += " and  cp.CustomerCode ='" + pInfo.CustomerCode + "'";
            }

            if ((pInfo.PhoneNumber != null) && (pInfo.PhoneNumber != ""))
            {
                strcond += " and  cp.PhoneNumber ='" + pInfo.PhoneNumber + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerPhone = new List<CustomerPhoneListReturn>();

            try
            {
                string strsql = " select cp.*,lu.LookupValue,c.CustomerFName,c.CustomerLName,c.ContactTel   from " + dbName + ".dbo.CustomerPhone cp " +

                                "  join " + dbName + ".dbo.Customer c on c.CustomerCode =  cp.CustomerCode" +
                                "  join " + dbName + ".dbo.Lookup lu on cp.PhoneType=lu.LookupCode" +
                                " where cp.FlagDelete ='N'  and lu.LookupType='PhoneType'" + strcond ;
                       strsql += " order by cp.UpdateDate desc";

         Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerPhone = (from DataRow dr in dt.Rows

                             select new CustomerPhoneListReturn()
                             {
                                 PhoneId = (dr["PhoneId"].ToString() != "") ? Convert.ToInt32(dr["PhoneId"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 PhoneNumber = dr["PhoneNumber"].ToString().Trim(),
                                 LookupValue = dr["LookupValue"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 CustomerContactTel  = dr["ContactTel"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerPhone;
        }

        public List<CustomerPhoneListReturn> ListCustomerPhoneCall(CustomerPhoneInfo pInfo)
        {
            string strcond = "";

            if ((pInfo.CustomerFName != null) && (pInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + pInfo.CustomerFName + "%'";
            }

            if ((pInfo.CustomerLName != null) && (pInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + pInfo.CustomerLName + "%'";
            }

            if ((pInfo.CustomerCode != null) && (pInfo.CustomerCode != ""))
            {
                strcond += " and  cp.CustomerCode ='" + pInfo.CustomerCode + "'";
            }

            if ((pInfo.PhoneNumber != null) && (pInfo.PhoneNumber != ""))
            {
                strcond += " and  cp.PhoneNumber ='" + pInfo.PhoneNumber + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerPhone = new List<CustomerPhoneListReturn>();

            try
            {
                string strsql = " select distinct cp.CustomerCode,cp.PhoneNumber,cp.Phonetype,lu.LookupValue,c.CustomerFName,c.CustomerLName " +
                    
                    " from " + dbName + ".dbo.CustomerPhone cp " +
                

                                "  join " + dbName + ".dbo.Customer c on c.CustomerCode =  cp.CustomerCode" +
                                "  join " + dbName + ".dbo.Lookup lu on cp.PhoneType=lu.LookupCode" +
                                
                                " where c.FlagDelete ='N'  and lu.LookupType='PhoneType' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerPhone = (from DataRow dr in dt.Rows

                                  select new CustomerPhoneListReturn()
                                  {
                                      CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                      CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                      PhoneNumber = dr["PhoneNumber"].ToString().Trim(),
                                      PhoneType = dr["PhoneType"].ToString().Trim(),
                                      LookupValue = dr["LookupValue"].ToString().Trim(),
                                  }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerPhone;
        }

        public int DeleteCustomerPhone(CustomerPhoneInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.CustomerPhone set FlagDelete = 'Y' where PhoneId in (" + pInfo.PhoneId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int InsertCustomerPhone(CustomerPhoneInfo pInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.CustomerPhone  (CustomerCode,PhoneNumber,PhoneType,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + pInfo.CustomerCode + "'," +
                           "'" + pInfo.CustomerPhone + "'," +
                           "'" + pInfo.CustomerPhoneType + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + pInfo.UpdateBy + "'," +
                           "'" + pInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateCustomerPhone(CustomerPhoneInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.CustomerPhone set " +
                            " PhoneNumber = '" + pInfo.CustomerPhone + "'," +
                            " PhoneType = '" + pInfo.CustomerPhoneType + "'" +
                           " where PhoneId =" + pInfo.PhoneId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<CustomerPhoneListReturn> ValidateCusPhoneInfo(String cusPhone)
        {
            string strcond = "";

            if ((cusPhone != null) && (cusPhone != ""))
            {
                strcond += " and  cp.PhoneNumber = '" + cusPhone + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerPhone = new List<CustomerPhoneListReturn>();

            try
            {
                string strsql = " select cp.* from " + dbName + ".dbo.CustomerPhone cp " +
                                " where cp.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerPhone = (from DataRow dr in dt.Rows

                                  select new CustomerPhoneListReturn()
                                  {
                                      PhoneId = (dr["PhoneId"].ToString() != "") ? Convert.ToInt32(dr["PhoneId"]) : 0,
                                      CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                      //CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                      PhoneNumber = dr["PhoneNumber"].ToString().Trim(),
                                      //LookupValue = dr["LookupValue"].ToString().Trim(),
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

            return LCustomerPhone;
        }
        public int UpdateCustomerPhonefromOneApp(CustomerPhoneInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.CustomerPhone set " +
                            " CustomerCode = '" + pInfo.CustomerCode + "', " +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where PhoneNumber = '" + pInfo.CustomerPhone + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateCustomerPhoneTakeOrderMK(CustomerPhoneInfo cInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.CustomerPhone set " +
                            " PhoneNumber = '" + cInfo.PhoneNumber + "', " +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + cInfo.UpdateBy + "' " +
                            " where (UpdateDate = " +
                            " (SELECT        MAX(UpdateDate) AS LastUpdate" +
                            " FROM            CustomerPhone AS CustomerPhone_1" +
                            " WHERE        (CustomerCode = '" + cInfo.CustomerCode + "')))";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public List<CustomerPhoneListReturn> ValidateCustomerPhone(CustomerPhoneInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and c.CustomerCode = '" + cInfo.CustomerCode.Trim() + "'";
            }
            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and c.PhoneNumber = '" + cInfo.PhoneNumber.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LCustomer = new List<CustomerPhoneListReturn>();

            try
            {
                string strsql = "select c.*" +
                                " from " + dbName + ".dbo.CustomerPhone c where c.FlagDelete = 'N' " +
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerPhoneListReturn()
                             {
                                 CustomerCode = dr["CustomerCode"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }
    }
}

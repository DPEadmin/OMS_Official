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
    public class CustomerDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<CustomerListReturn> ListCustomerByCriteria(CustomerInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";

            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode.Trim() + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName.Trim() + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName.Trim() + "%'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and  c.Mobile = '" + cInfo.PhoneNumber + "'";
            }
            if ((cInfo.AgeFrom != null) && (cInfo.AgeFrom != 0) && (cInfo.AgeTo != null) && (cInfo.AgeTo != 0))
            {
                strcond += " and (DATEDIFF(year, c.BirthDate, GETDATE()) BETWEEN " + cInfo.AgeFrom + " AND " + cInfo.AgeTo + ")";
            }
            if ((cInfo.Gender != null) && (cInfo.Gender != "") && (cInfo.Gender != "-99"))
            {
                strcond += " and  c.Gender = '" + cInfo.Gender + "'";
            }
            if ((cInfo.MaritalStatusCode != null) && (cInfo.MaritalStatusCode != "") && (cInfo.MaritalStatusCode != "-99"))
            {
                strcond += " and  c.MaritalStatus = '" + cInfo.MaritalStatusCode + "'";
            }
            if ((cInfo.IncomeFrom != null) && (cInfo.IncomeFrom != 0) && (cInfo.IncomeTo != null) && (cInfo.IncomeTo != 0))
            {
                strcond += " and   (Income BETWEEN " + cInfo.IncomeFrom + " AND " + cInfo.IncomeTo + ")";
            }
            if ((cInfo.OccupationCode != null) && (cInfo.OccupationCode != "") && (cInfo.OccupationCode != "-99"))
            {
                strcond += " and  c.Occupation = '" + cInfo.OccupationCode + "'";
            }
            if ((cInfo.ContactTel != null) && (cInfo.ContactTel != ""))
            {
                strcond += " and  c.ContactTel = '" + cInfo.ContactTel.Trim() + "'";
            }
            if ((cInfo.rowOFFSet != null) && (cInfo.rowFetch != null) && (cInfo.rowFetch != 0))
            {
                stroffset += " OFFSET " +cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";
            }

                var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.*, DATEDIFF(year, c.BirthDate, GETDATE()) AS Age, p.PhoneNumber,p.PhoneType, g.GenderName, m.LookupValue as MaritalStatusName, o.OccupationName from " + dbName + ".dbo.Customer c " +
                                "left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +
                                "left join  " + dbName + ".dbo.Gender g on g.GenderCode = c.Gender " +
                                "left join  " + dbName + ".dbo.Occupation o on o.OccupationCode = c.Occupation " +
                                "left join  " + dbName + ".dbo.Lookup m on m.LookupCode = c.MaritalStatus and m.LookupType = 'MARITALSTATUS' " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),
                                 Title = dr["Title"].ToString().Trim(),
                                 Gender = dr["Gender"].ToString().Trim(),
                                 GenderName = dr["GenderName"].ToString().Trim(),
                                 OccupationCode = dr["Occupation"].ToString().Trim(),
                                 OccupationName = dr["OccupationName"].ToString().Trim(),
                                 MaritalStatusCode = dr["MaritalStatus"].ToString().Trim(),
                                 MaritalStatusName = dr["MaritalStatusName"].ToString().Trim(),
                                 BirthDate = dr["BirthDate"].ToString().Trim(),
                                 Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                                 Income = (dr["Income"].ToString() != "") ? Convert.ToInt32(dr["Income"]) : 0,
                                 Identification = dr["Identification_Number"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 HomePhone = dr["HomePhone"].ToString().Trim(),
                                 PhoneNumber = dr["PhoneNumber"].ToString().Trim(),
                                 PhoneType = dr["PhoneType"].ToString().Trim(),
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

            return LCustomer;
        }

        public List<CustomerListReturn> ListCustomerMapCusPhoneByCriteria(CustomerInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and  c.Mobile = '" + cInfo.PhoneNumber + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select convert(varchar,c.BirthDate , 103) as BirthDate,c.*, DATEDIFF(year, c.BirthDate, GETDATE()) AS Age, p.PhoneNumber,p.PhoneType, g.GenderName, m.LookupValue as MaritalStatusName, o.OccupationName, t.LookupValue as TitleName from " + dbName + ".dbo.Customer c " +
                                "left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +
                                "left join  " + dbName + ".dbo.Gender g on g.GenderCode = c.Gender " +
                                "left join  " + dbName + ".dbo.Occupation o on o.OccupationCode = c.Occupation " +
                                "left join  " + dbName + ".dbo.Lookup m on m.LookupCode = c.MaritalStatus and m.LookupType = 'MARITALSTATUS' " +
                                "left join  " + dbName + ".dbo.Lookup t on t.LookupCode = c.title and t.LookupType = 'TITLE' " +
                               " where c.FlagDelete ='N' " + strcond;


                strsql += " ORDER BY c.UpdateDate Desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 HomePhone = dr["HomePhone"].ToString().Trim(),
                                 Mobile = dr["Mobile"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 Title = dr["Title"].ToString().Trim(),
                                 TitleName = dr["TitleName"].ToString().Trim(),
                                 Gender = dr["gender"].ToString().Trim(),
                                 GenderName = dr["GenderName"].ToString().Trim(),
                                 MaritalStatusCode = dr["MaritalStatus"].ToString().Trim(),
                                 MaritalStatusName = dr["MaritalStatusName"].ToString().Trim(),
                                 OccupationCode = dr["Occupation"].ToString().Trim(),
                                 OccupationName = dr["OccupationName"].ToString().Trim(),
                                 Identification = dr["Identification_Number"].ToString().Trim(),
                                 Income = (dr["Income"].ToString() != "") ? Convert.ToInt32(dr["Income"]) : 0,
                                 BirthDate = dr["BirthDate"].ToString(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                                 Note = dr["Note"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }

        public List<CustomerListReturn> ListCustomerNoPagingByCriteria(CustomerInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.Username != null) && (cInfo.Username != ""))
            {
                strcond += " and  c.Username = '" + cInfo.Username + "'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and  c.Mobile = '" + cInfo.PhoneNumber + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select convert(varchar,c.BirthDate , 103) as BirthDate,c.*, DATEDIFF(year, c.BirthDate, GETDATE()) AS Age, g.GenderName, m.LookupValue as MaritalStatusName, o.OccupationName, t.LookupValue as TitleName from " + dbName + ".dbo.Customer c " +
                                "left join  " + dbName + ".dbo.Gender g on g.GenderCode = c.Gender " +
                                "left join  " + dbName + ".dbo.Occupation o on o.OccupationCode = c.Occupation " +
                                "left join  " + dbName + ".dbo.Lookup m on m.LookupCode = c.MaritalStatus and m.LookupType = 'MARITALSTATUS' " +
                                "left join  " + dbName + ".dbo.Lookup t on t.LookupCode = c.title and t.LookupType = 'TITLE' " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 HomePhone = dr["HomePhone"].ToString().Trim(),
                                 Mobile = dr["Mobile"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 Title = dr["Title"].ToString().Trim(),
                                 TitleName = dr["TitleName"].ToString().Trim(),
                                 Gender = dr["gender"].ToString().Trim(),
                                 GenderName = dr["GenderName"].ToString().Trim(),
                                 MaritalStatusCode = dr["MaritalStatus"].ToString().Trim(),
                                 MaritalStatusName = dr["MaritalStatusName"].ToString().Trim(),
                                 OccupationCode = dr["Occupation"].ToString().Trim(),
                                 OccupationName = dr["OccupationName"].ToString().Trim(),
                                 Identification = dr["Identification_Number"].ToString().Trim(),
                                 Income = (dr["Income"].ToString() != "") ? Convert.ToInt32(dr["Income"]) : 0,
                                 BirthDate = dr["BirthDate"].ToString(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                                 Note = dr["Note"].ToString(),
                                 TaxId = dr["TaxId"].ToString(),
                                 Username = dr["Username"].ToString(), 
                                 Password = dr["Password"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }

        public List<CustomerListReturn> ListCustomerDetailByCriteria(CustomerInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and  p.PhoneNumber ='" + cInfo.PhoneNumber + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.*,p.PhoneNumber,p.PhoneType  from " + dbName + ".dbo.Customer c left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),
                                 Gender = dr["Gender"].ToString().Trim(),
                                 Identification = dr["Identification_Number"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 PhoneNumber = dr["PhoneNumber"].ToString().Trim(),
                                 PhoneType = dr["PhoneType"].ToString().Trim(),
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

            return LCustomer;
        }

        public List<CustomerListReturn> ListCustomerByCriteria_showgv(CustomerInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode.Trim() != null) && (cInfo.CustomerCode.Trim() != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode.Trim() + "%'";
            }
            if ((cInfo.CustomerFName.Trim() != null) && (cInfo.CustomerFName.Trim() != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName.Trim() + "%'";
            }

            if ((cInfo.CustomerLName.Trim() != null) && (cInfo.CustomerLName.Trim() != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName.Trim() + "%'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            if ((cInfo.Mobile != null) && (cInfo.Mobile != ""))
            {
                strcond += " and  c.Mobile = '" + cInfo.Mobile + "'";
            }
            if ((cInfo.ContactTel.Trim() != null) && (cInfo.ContactTel.Trim() != ""))
            {
                strcond += " and  c.ContactTel = '" + cInfo.ContactTel.Trim() + "'";
            }

            if ((cInfo.Age != null) && (cInfo.Age != 0))
            {
                strcond += " and DATEDIFF(year, BirthDate, '08/29/2018 10:56:27 ') > " + cInfo.Age + "";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.*, datediff(year," + " BirthDate" + ", '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + " ') as Age from " + dbName + ".dbo.Customer c  " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.CustomerFName ASC OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 Mobile = dr["Mobile"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 Title = dr["Title"].ToString().Trim(),
                                 Gender = dr["gender"].ToString().Trim(),
                                 Identification = dr["Identification_Number"].ToString().Trim(),
                                 BirthDate = dr["BirthDate"].ToString(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }

        public List<CustomerListReturn> CustomerCheck(CustomerInfo cInfo)
        {
            string strcond = "";


            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode ='" + cInfo.CustomerCode + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Customer c  " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }

        public int? CountCustomerListByCriteria(CustomerInfo cInfo)
        {

            string strcond = "";
            int? count = 0;


            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            if ((cInfo.Mobile != null) && (cInfo.Mobile != ""))
            {
                strcond += " and  c.Mobile = '" + cInfo.Mobile + "'";
            }

            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  c.MerchantCode = '" + cInfo.MerchantCode + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select count(c.Id) as countCustomer from " + dbName + ".dbo.Customer c " +
                                "left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +
                                "left join  " + dbName + ".dbo.Gender g on g.GenderCode = c.Gender " +
                                "left join  " + dbName + ".dbo.Occupation o on o.OccupationCode = c.Occupation " +
                                "left join  " + dbName + ".dbo.Lookup m on m.LookupCode = c.MaritalStatus and m.LookupType = 'MARITALSTATUS' " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 countCustomer = Convert.ToInt32(dr["countCustomer"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LCustomer.Count > 0)
            {
                count = LCustomer[0].countCustomer;
            }

            return count;
        }

        public int InsertCustomer(CustomerInfo cInfo)
        {

            int i = 0;
            string value_birthdate = "";

            if (cInfo.BirthDate != null)
            {
                value_birthdate = "CONVERT(DATETIME,'" + cInfo.BirthDate + "',103), ";
            }
            else { value_birthdate = "NULL,"; }
            if(cInfo.CustomerCode == null || cInfo.CustomerCode == "")
            {
                CustomerInfo ccInfo = new CustomerInfo();
                ccInfo.FlagDelete = "N";
                int? count = CountCustomerListByCriteriaMaster(ccInfo);
                string CustomerCode = count.ToString().PadLeft(5, '0');
                cInfo.CustomerCode = "C" + DateTime.Now.ToString("yyyyMMdd") + CustomerCode;
            }

            string strsql = "INSERT INTO " + dbName + ".dbo.Customer  " +
                "(CustomerCode,Title,CustomerFName,CustomerLName,ContactTel," +
                "HomePhone,Occupation,Mail,Gender,Mobile,Identification_Number," +
                //"BirthDate,
                "MaritalStatus,Income,CreateDate,CreateBy,UpdateDate,UpdateBy," +
                "FlagDelete,PointNum,PointRangeCode,MerchantCode,Username,Password)" +

                           "VALUES (" +
                          "'" + cInfo.CustomerCode + "'," +
                          "'" + cInfo.TitleCode + "'," +
                          "'" + cInfo.CustomerFName + "'," +
                          "'" + cInfo.CustomerLName + "'," +
                          "'" + cInfo.ContactTel + "'," +
                          "'" + cInfo.HomePhone + "'," +
                          "'" + cInfo.OccupationCode + "'," +
                          "'" + cInfo.Mail + "'," +
                          "'" + cInfo.Gender + "'," +
                          "'" + cInfo.Mobile + "'," +
                          "'" + cInfo.Identification + "'," +
                          //"CONVERT(DATETIME,'" + cInfo.BirthDate + "',103), " +
                          //"CONVERT(DATETIME,'" + "NULL" + "',103), " +
                          //value_birthdate +
                          "'" + cInfo.MaritalStatusCode + "'," +
                          "'" + cInfo.Income + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.UpdateBy + "'," +
                          "'" + cInfo.FlagDelete + "'," +
                          "'" + cInfo.PointNum + "'," +
                          "'" + cInfo.PointRangeCode + "'," +
                          "'" + cInfo.MerchantCode + "'," + 
                          "'" + cInfo.Username + "'," + 
                          "'" + cInfo.Password + "'" +
                           ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public String InsertCusfromOneApp(CustomerInfo cInfo)
        {
            int i = 0;

            int CustomerCode = getMaxCustomer(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());
            String genCustomerCode = "C" + (DateTime.Now.Year + 543).ToString() + DateTime.Now.ToString("MM") + String.Format("{0:00000000}", CustomerCode);

            string strsql = "INSERT INTO " + dbName + ".dbo.Customer  (CustomerCode,UniqueID,RunningNo,Title,CustomerFName,CustomerLName,ContactTel,HomePhone,Occupation,Mail,Gender,Mobile,Identification_Number,BirthDate,MaritalStatus,Income,Channel,Brand,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                           "VALUES (" +
                          "'" + genCustomerCode + "'," +
                          "'" + cInfo.UniqueID + "'," +
                          "'" + CustomerCode + "'," +
                          "'" + cInfo.TitleCode + "'," +
                          "'" + cInfo.CustomerFName + "'," +
                          "'" + cInfo.CustomerLName + "'," +
                          "'" + cInfo.ContactTel + "'," +
                          "'" + cInfo.HomePhone + "'," +
                          "'" + cInfo.OccupationCode + "'," +
                          "'" + cInfo.Mail + "'," +
                          "'" + cInfo.Gender + "'," +
                          "'" + cInfo.Mobile + "'," +
                          "'" + cInfo.Identification + "'," +
                          "'" + cInfo.BirthDate + "'," +
                          "'" + cInfo.MaritalStatusCode + "'," +
                          "'" + cInfo.Income + "'," +
                          "'" + cInfo.Channel + "'," +
                          "'" + cInfo.BrandNo + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.UpdateBy + "'," +
                          "'" + "N" + "'" +
                           ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);
            if (i > 0)
            {
                return genCustomerCode;
            }
            return "";
        }

        public int getMaxCustomer(String year, String month)
        {
            int maxCustomer = 1;

            DataTable dt = new DataTable();

            string strsql = @" select isnull(max(isnull(runningno,0)),0) + 1 max_no from " + dbName + @".dbo.Customer
                             where month(createdate) = " + month + " and year(createdate) = " + year + " and FlagDelete ='N' ";

            try
            {
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                if (dt.Rows.Count > 0)
                {
                    maxCustomer = (dt.Rows[0]["max_no"] != null) ? int.Parse(dt.Rows[0]["max_no"].ToString()) : 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return maxCustomer;
        }
        public int InsertCustomerofOMS(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.Customer  (CustomerCode,Title,CustomerFName,CustomerLName,Mail,Gender,ContactTel,Mobile,Identification_Number,BirthDate,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete,MerchantCode)" +
                           "VALUES (" +
                          "'" + cInfo.CustomerCode + "'," +
                          "'" + cInfo.TitleCode + "'," +
                          "'" + cInfo.CustomerFName + "'," +
                          "'" + cInfo.CustomerLName + "'," +
                          "'" + cInfo.Mail + "'," +
                          "'" + cInfo.Gender + "'," +
                          "'" + cInfo.ContactTel + "'," +
                          "'" + cInfo.Mobile + "'," +
                          "'" + cInfo.Identification + "'," +
                          "'" + cInfo.BirthDate + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.CreateBy + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + cInfo.UpdateBy + "'," +
                          "'" + cInfo.FlagDelete + "'," +
                          "'" + cInfo.MerchantCode + "'" +
                           ")";
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountCustomerPhone(CustomerInfo cInfo)
        {

            string strcond = "";
            int? count = 0;

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }

            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and c.PhoneNumber like '%" + cInfo.PhoneNumber + "%'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select count(c.PhoneId) as countCustomerPhone from " + dbName + ".dbo.CustomerPhone c " +
                               //"left join " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 countCustomerPhone = Convert.ToInt32(dr["countCustomerPhone"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LCustomer.Count > 0)
            {
                count = LCustomer[0].countCustomerPhone;
            }

            return count;
        }

        public int InsertCustomerPhoneByCustomerInfo(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.CustomerPhone  (CustomerCode,PhoneNumber,PhoneType,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + cInfo.CustomerCode + "'," +
                           "'" + cInfo.PhoneNumber + "'," +
                           "'" + cInfo.PhoneType + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + cInfo.UpdateBy + "'," +
                           "'" + cInfo.FlagDelete + "'" +
                            ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateCustomer(CustomerInfo cInfo)
        {

            int i = 0;
            string value_birthdate = "";

            if (cInfo.BirthDate != null)
            {
                value_birthdate = "CONVERT(DATETIME,'" + cInfo.BirthDate + "',103) ";
            }
            else { value_birthdate = "NULL"; }



            string strsql = " Update " + dbName + ".dbo.Customer set ";

                if (cInfo.CustomerCode != null && cInfo.CustomerCode != "")
                {
                    strsql += " CustomerCode = '" + cInfo.CustomerCode + "'," ;
                }
                if (cInfo.TitleCode != null && cInfo.TitleCode != "")
                {
                    strsql += " Title = '" + cInfo.TitleCode + "',";
                }
                if (cInfo.CustomerFName != null && cInfo.CustomerFName != "")
                {
                    strsql += " CustomerFName = '" + cInfo.CustomerFName + "',";
                }
                if (cInfo.CustomerLName != null && cInfo.CustomerLName != "")
                {
                    strsql += " CustomerLName = '" + cInfo.CustomerLName + "',";
                }
                if (cInfo.ContactTel != null && cInfo.ContactTel != "")
                {
                    strsql += " ContactTel = '" + cInfo.ContactTel + "',";
                }
                if (cInfo.OccupationCode != null && cInfo.OccupationCode != "")
                {
                    strsql += " Occupation = '" + cInfo.OccupationCode + "',";
                }
                if (cInfo.BirthDate != null && cInfo.BirthDate != "")
                {
                    strsql += " BirthDate = " + value_birthdate + ",";
                }
                if (cInfo.Gender != null && cInfo.Gender != "")
                {
                    strsql += " Gender = '" + cInfo.Gender + "',";
                }
                if (cInfo.MaritalStatusCode != null && cInfo.MaritalStatusCode != "")
                {
                    strsql += " MaritalStatus = '" + cInfo.MaritalStatusCode + "',";
                }
                if (cInfo.Income != null && cInfo.Income != 0)
                {
                    strsql += " Income = " + cInfo.Income + ",";
                }
                if (cInfo.Mail != null && cInfo.Mail != "")
                {
                    strsql += " Mail = '" + cInfo.Mail + "',";
                }
                if (cInfo.Mobile != null && cInfo.Mobile != "")
                {
                    strsql += " Mobile = '" + cInfo.Mobile + "',";
                }
                if (cInfo.Identification != null && cInfo.Identification != "")
                {
                    strsql += " Identification_Number = '" + cInfo.Identification + "',";
                }
                if (cInfo.HomePhone != null && cInfo.HomePhone != "")
                {
                    strsql += " HomePhone = '" + cInfo.HomePhone + "',";
                }
                if (cInfo.PointRangeCode != null && cInfo.PointRangeCode != "")
                {
                    strsql += " PointRangeCode = '" + cInfo.PointRangeCode + "',";
                }
                if (cInfo.PointNum != null && cInfo.PointNum != 0)
                {
                    strsql += " PointNum = " + cInfo.PointNum + ",";
                }
                if (cInfo.UpdateBy != null && cInfo.UpdateBy != "")
                {
                    strsql += " UpdateBy = '" + cInfo.UpdateBy + "',";
                }
                if (cInfo.Username != null && cInfo.Username != "")
                {
                    strsql += " Username = '" + cInfo.Username + "',";
                }
                if (cInfo.Password != null && cInfo.Password != "")
                {
                    strsql += " Password = '" + cInfo.Password + "',";
                }
            strsql += " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'"+
                " where CustomerCode ='" + cInfo.CustomerCode + "'";








            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateCustomerNoteProfile(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Customer set " +
                            " Note = '" + cInfo.Note + "'," +
                            " UpdateBy = '" + cInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where CustomerCode ='" + cInfo.CustomerCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateCustomerPhoneByCustomerInfo(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.CustomerPhone set " +
                            " PhoneNumber = '" + cInfo.PhoneNumber + "'," +
                            " PhoneType = '" + cInfo.PhoneType + "'," +

                            " UpdateBy = '" + cInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where CustomerCode ='" + cInfo.CustomerCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdateCustomerPhoneByCustomer(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Customer  set " +
                            " ContactTel = '" + cInfo.ContactTel + "'," +
                           
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where CustomerCode ='" + cInfo.CustomerCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteCustomer(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Customer set FlagDelete = 'Y' where Id in (" + cInfo.CusIdforDelete + ")";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<CustomerListReturn> AuthenCustomerByCriteria(CustomerInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.Mail != null) && (cInfo.Mail != ""))
            {
                strcond += " and  c.Mail = '" + cInfo.Mail + "'";
            }

            if ((cInfo.Password != null) && (cInfo.Password != ""))
            {
                strcond += " and  c.Password = '" + cInfo.Password + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.*,p.PhoneNumber,p.PhoneType  from " + dbName + ".dbo.Customer c left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),
                                 Gender = dr["Gender"].ToString().Trim(),
                                 Identification = dr["Identification_Number"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 PhoneNumber = dr["PhoneNumber"].ToString().Trim(),
                                 PhoneType = dr["PhoneType"].ToString().Trim(),
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

            return LCustomer;
        }
        public List<CustomerListReturn> CustomerCodeValidation(CustomerInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond = " and c.CustomerCode = '" + cInfo.CustomerCode.Trim() + "'";
            }
            //if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            //{
            //    strcond = " and c.CustomerFName like '%" + cInfo.CustomerFName.Trim() + "%'";
            //}

            DataTable dt = new DataTable();
            var LCustomer = new List<CustomerListReturn>();

            try
            {
                string strsql = "select c.*" +
                                " from " + dbName + ".dbo.Customer c where c.FlagDelete = 'N' " +
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }
        public List<CustomerListReturn> CheckUniqueIDofCustomer(String uniqueID)
        {
            string strcond = "";

            if ((uniqueID != "") && (uniqueID != null))
            {
                strcond = " and c.UniqueID = '" + uniqueID + "'";
            }

            DataTable dt = new DataTable();
            var LCustomer = new List<CustomerListReturn>();

            try
            {
                string strsql = "select c.*" +
                                " from " + dbName + ".dbo.Customer c where c.FlagDelete = 'N' " +
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 //CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 UniqueID = dr["UniqueID"].ToString(),
                                 CustomerCode = dr["CustomerCode"].ToString(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 //FlagDelete = dr["FlagDelete"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }
        public String UpdateCusfromOneApp(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Customer set " +
                            //" CustomerCode = '" + cInfo.CustomerCode + "'," +
                            //" UniqueID = '" + cInfo.UniqueID + "'," +
                            " Title = '" + cInfo.TitleCode + "'," +
                            " CustomerFName = '" + cInfo.CustomerFName + "'," +
                            " CustomerLName = '" + cInfo.CustomerLName + "'," +
                            " ContactTel = '" + cInfo.ContactTel + "'," +
                            " Occupation = '" + cInfo.OccupationCode + "'," +
                            " BirthDate = '" + cInfo.BirthDate + "'," +
                            " Gender = '" + cInfo.Gender + "'," +
                            " MaritalStatus = '" + cInfo.MaritalStatusCode + "'," +
                            " Income = '" + cInfo.Income + "'," +
                            " Mail = '" + cInfo.Mail + "'," +
                            " Mobile = '" + cInfo.Mobile + "'," +
                            " Identification_Number = '" + cInfo.Identification + "'," +
                            " HomePhone = '" + cInfo.HomePhone + "'," +
                            " Channel = '" + cInfo.Channel + "'," +
                            " Brand = '" + cInfo.BrandNo + "'," +
                            " UpdateBy = '" + cInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where UniqueID ='" + cInfo.UniqueID + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            if (i > 0)
            {
                return cInfo.UniqueID;
            }
            return "";
        }

        public List<CustomerListReturn> ListCustomerByCriteriaMaster(CustomerInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";
            string strsql = "";

            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode.Trim() + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName.Trim() + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName.Trim() + "%'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  c.MerchantCode = '" + cInfo.MerchantCode + "'";
            }

            if ((cInfo.PhoneNumber != null) && (cInfo.PhoneNumber != ""))
            {
                strcond += " and  c.Mobile = '" + cInfo.PhoneNumber + "'";
            }
            if ((cInfo.AgeFrom != null) && (cInfo.AgeFrom != 0) && (cInfo.AgeTo != null) && (cInfo.AgeTo != 0))
            {
                strcond += " and (DATEDIFF(year, c.BirthDate, GETDATE()) BETWEEN " + cInfo.AgeFrom + " AND " + cInfo.AgeTo + ")";
            }
            if ((cInfo.Gender != null) && (cInfo.Gender != "") && (cInfo.Gender != "-99"))
            {
                strcond += " and  c.Gender = '" + cInfo.Gender + "'";
            }
            if ((cInfo.MaritalStatusCode != null) && (cInfo.MaritalStatusCode != "") && (cInfo.MaritalStatusCode != "-99"))
            {
                strcond += " and  c.MaritalStatus = '" + cInfo.MaritalStatusCode + "'";
            }
            if ((cInfo.IncomeFrom != null) && (cInfo.IncomeFrom != 0) && (cInfo.IncomeTo != null) && (cInfo.IncomeTo != 0))
            {
                strcond += " and   (Income BETWEEN " + cInfo.IncomeFrom + " AND " + cInfo.IncomeTo + ")";
            }
            if ((cInfo.OccupationCode != null) && (cInfo.OccupationCode != "") && (cInfo.OccupationCode != "-99"))
            {
                strcond += " and  c.Occupation = '" + cInfo.OccupationCode + "'";
            }
            if ((cInfo.ContactTel != null) && (cInfo.ContactTel != ""))
            {
                strcond += " and  c.ContactTel = '" + cInfo.ContactTel.Trim() + "'";
            }
            if ((cInfo.rowOFFSet != null) && (cInfo.rowFetch != null) && (cInfo.rowFetch != 0))
            {
                stroffset += " OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                if ((cInfo.Currently != null) && (cInfo.Currently != ""))
                {
                    strsql = " select TOP 1 c.*, DATEDIFF(year, c.BirthDate, GETDATE()) AS Age,  g.GenderName, me.MerchantCode , m.LookupValue as MaritalStatusName, o.OccupationName from " + dbName + ".dbo.Customer c " +

                                "left join  " + dbName + ".dbo.Gender g on g.GenderCode = c.Gender " +
                                "left join  " + dbName + ".dbo.Merchant me on me.MerchantCode = c.MerchantCode " +
                                "left join  " + dbName + ".dbo.Occupation o on o.OccupationCode = c.Occupation " +
                                "left join  " + dbName + ".dbo.Lookup m on m.LookupCode = c.MaritalStatus and m.LookupType = 'MARITALSTATUS' " +
                                " where c.FlagDelete ='N' " + strcond;

                    strsql += " ORDER BY c.Id DESC " + stroffset;
                }
                else
                {
                    strsql = " select c.*, DATEDIFF(year, c.BirthDate, GETDATE()) AS Age,  g.GenderName, me.MerchantCode , m.LookupValue as MaritalStatusName, o.OccupationName from " + dbName + ".dbo.Customer c " +

                                "left join  " + dbName + ".dbo.Gender g on g.GenderCode = c.Gender " +
                                "left join  " + dbName + ".dbo.Merchant me on me.MerchantCode = c.MerchantCode " +
                                "left join  " + dbName + ".dbo.Occupation o on o.OccupationCode = c.Occupation " +
                                "left join  " + dbName + ".dbo.Lookup m on m.LookupCode = c.MaritalStatus and m.LookupType = 'MARITALSTATUS' " +
                                " where c.FlagDelete ='N' " + strcond;

                    strsql += " ORDER BY c.Id DESC " + stroffset;
                }
                    

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 CustomerTypeCode = dr["CustomerTypeCode"].ToString().Trim(),
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 Title = dr["Title"].ToString().Trim(),
                                 Gender = dr["Gender"].ToString().Trim(),
                                 GenderName = dr["GenderName"].ToString().Trim(),
                                 OccupationCode = dr["Occupation"].ToString().Trim(),
                                 OccupationName = dr["OccupationName"].ToString().Trim(),
                                 MaritalStatusCode = dr["MaritalStatus"].ToString().Trim(),
                                 MaritalStatusName = dr["MaritalStatusName"].ToString().Trim(),
                                 BirthDate = dr["BirthDate"].ToString().Trim(),
                                 Age = (dr["Age"].ToString() != "") ? Convert.ToInt32(dr["Age"]) : 0,
                                 Income = (dr["Income"].ToString() != "") ? Convert.ToInt32(dr["Income"]) : 0,
                                 Identification = dr["Identification_Number"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 HomePhone = dr["HomePhone"].ToString().Trim(),

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

            return LCustomer;
        }
        public int? CountCustomerListByCriteriaMaster(CustomerInfo cInfo)
        {

            string strcond = "";
            int? count = 0;


            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }

            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  c.MerchantCode = '" + cInfo.MerchantCode + "'";
            }

            if ((cInfo.CustomerTypeCode != null) && (cInfo.CustomerTypeCode != ""))
            {
                strcond += " and  c.CustomerTypeCode = '" + cInfo.CustomerTypeCode + "'";
            }

            if ((cInfo.Mobile != null) && (cInfo.Mobile != ""))
            {
                strcond += " and  c.Mobile = '" + cInfo.Mobile + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select count(c.Id) as countCustomer from " + dbName + ".dbo.Customer c " +

                                "left join  " + dbName + ".dbo.Gender g on g.GenderCode = c.Gender " +
                                "left join  " + dbName + ".dbo.Occupation o on o.OccupationCode = c.Occupation " +
                                "left join  " + dbName + ".dbo.Lookup m on m.LookupCode = c.MaritalStatus and m.LookupType = 'MARITALSTATUS' " +
                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 countCustomer = Convert.ToInt32(dr["countCustomer"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LCustomer.Count > 0)
            {
                count = LCustomer[0].countCustomer;
            }

            return count;
        }
        public int? UpdateCustomerTaxId(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Customer set " +
                            " TaxID = '" + cInfo.TaxId + "'," +
                            " UpdateBy = '" + cInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where CustomerCode ='" + cInfo.CustomerCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? CountCustomerListPagingByCriteria(CustomerInfo cInfo)
        {

            string strcond = "";
            int? count = 0;


            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName+ "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }
            if ((cInfo.ContactTel != null) && (cInfo.ContactTel != ""))
            {
                strcond += " and  c.ContactTel like '%" + cInfo.ContactTel + "%'";
            }
            if ((cInfo.Mail != null) && (cInfo.Mail != ""))
            {
                strcond += " and  c.Mail like '%" + cInfo.Mail + "%'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  c.MerchantCode = '" + cInfo.MerchantCode + "'";
            }
            if ((cInfo.PointRangeCode != null) && (cInfo.PointRangeCode != "") && (cInfo.PointRangeCode != "-99"))
            {
                strcond += " and  c.PointRangeCode = '" + cInfo.PointRangeCode + "'";
            }

            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select count(c.Id) as countCustomer from " + dbName + ".dbo.Customer c " +
                                " left join PointRange pr on pr.PointCode = c.PointRangeCode and pr.MerchantMapCode = '" + cInfo.MerchantCode + "'" +
                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 countCustomer = Convert.ToInt32(dr["countCustomer"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LCustomer.Count > 0)
            {
                count = LCustomer[0].countCustomer;
            }

            return count;
        }
        public List<CustomerListReturn> ListCustomerListPagingByCriteria(CustomerInfo cInfo)
        {
            string strcond = "";
            string stroffset = "";

            if ((cInfo.CustomerId != null) && (cInfo.CustomerId != 0))
            {
                strcond += " and  c.Id =" + cInfo.CustomerId;
            }

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode like '%" + cInfo.CustomerCode + "%'";
            }
            if ((cInfo.CustomerFName != null) && (cInfo.CustomerFName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + cInfo.CustomerFName + "%'";
            }

            if ((cInfo.CustomerLName != null) && (cInfo.CustomerLName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + cInfo.CustomerLName + "%'";
            }
            if ((cInfo.ContactTel != null) && (cInfo.ContactTel != ""))
            {
                strcond += " and  c.ContactTel like '%" + cInfo.ContactTel + "%'";
            }
            if ((cInfo.Mail != null) && (cInfo.Mail != ""))
            {
                strcond += " and  c.Mail like '%" + cInfo.Mail + "%'";
            }
            if ((cInfo.MerchantCode != null) && (cInfo.MerchantCode != ""))
            {
                strcond += " and  c.MerchantCode = '" + cInfo.MerchantCode + "'";
            }
            if ((cInfo.PointRangeCode != null) && (cInfo.PointRangeCode != "") && (cInfo.PointRangeCode != "-99"))
            {
                strcond += " and  c.PointRangeCode = '" + cInfo.PointRangeCode + "'";
            }
            if ((cInfo.rowOFFSet != null) && (cInfo.rowFetch != 0) && (cInfo.rowOFFSet != null) && (cInfo.rowFetch != 0))
            {
                stroffset += " OFFSET " + cInfo.rowOFFSet + " ROWS FETCH NEXT " + cInfo.rowFetch + " ROWS ONLY ";
            }
            var LCustomer = new List<CustomerListReturn>();
            DataTable dt = new DataTable();

            try
            {
                string strsql = " select c.*, datediff(year," + " BirthDate" + ", '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + " ') as Age , pr.PointName, " +
                                " (select min(prs.pointend) from PointRange prs where prs.PointEnd > c.PointNum and prs.MerchantMapCode = c.MerchantCode and prs.FlagDelete = 'N' ) - c.PointNum as NextPointNum, " +
                                " (select prp.pointname from PointRange prp " +
                                " where prp.PointSequence = (select min(pro.PointSequence) from PointRange pro where pro.PointEnd > c.PointNum and pro.MerchantMapCode = c.MerchantCode and pro.FlagDelete = 'N' )" +
                                " and prp.MerchantMapCode = c.MerchantCode and prp.FlagDelete = 'N') as NextPointName " +
                                " from " + dbName + ".dbo.Customer c " +
                                " left join PointRange pr on pr.PointCode = c.PointRangeCode and pr.MerchantMapCode = '" + cInfo.MerchantCode + "'" +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.CreateDate DESC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 Mail = dr["Mail"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 PointNum = (dr["PointNum"].ToString() != "") ? Convert.ToInt32(dr["PointNum"]) : 0,
                                 NextPointNum = (dr["NextPointNum"].ToString() != "") ? Convert.ToInt32(dr["NextPointNum"]) : 0,
                                 PointName = dr["PointName"].ToString(),
                                 NextPointName = dr["NextPointName"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomer;
        }

        public int? UpdateCustomerUserLogin(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Customer set " +
                            " Username = '" + cInfo.Username + "'," +
                            " Password = '" + cInfo.Password + "'," +
                            " UpdateBy = '" + cInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where CustomerCode ='" + cInfo.CustomerCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int? UpdateCustomerMail(CustomerInfo cInfo)
        {

            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Customer set " +
                            " Mail = '" + cInfo.Mail + "'," + 
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where CustomerCode ='" + cInfo.CustomerCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }





        public List<CustomerListReturn> EmailAuthenticationEcommerce(CustomerInfo cInfo)
        {
            string strcond = "";

            if ((cInfo.CustomerCode != null) && (cInfo.CustomerCode != ""))
            {
                strcond = " and c.CustomerCode = '" + cInfo.CustomerCode.Trim() + "'";
            }
            if ((cInfo.Email != null) && (cInfo.Email != ""))
            {
                strcond = " and c.Username = '" + cInfo.Email.Trim() + "'";
            }
            if ((cInfo.Password != null) && (cInfo.Password != ""))
            {
                strcond = " and c.Password = '" + cInfo.Password.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LCustomer = new List<CustomerListReturn>();

            try
            {
                string strsql = "select c.*" +
                                " from " + dbName + ".dbo.Customer c where c.FlagDelete = 'N' " +
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomer = (from DataRow dr in dt.Rows

                             select new CustomerListReturn()
                             {
                                 CustomerId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 CustomerCode = dr["CustomerCode"].ToString(),
                                 CustomerFName = dr["CustomerFName"].ToString().Trim(),
                                 CustomerLName = dr["CustomerLName"].ToString().Trim(),
                                 CustomerName = dr["CustomerFName"].ToString().Trim() + " " + dr["CustomerLName"].ToString().Trim(),
                                 TitleCode = dr["Title"].ToString().Trim(),
                                 OccupationCode = dr["Occupation"].ToString().Trim(),
                                 Gender = dr["Gender"].ToString().Trim(),
                                 MaritalStatusCode = dr["MaritalStatus"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 MerchantCode = dr["MerchantCode"].ToString().Trim(),
                                 Email = dr["Username"].ToString().Trim(),  // Need to send object change to Email because Need specific value of UI as "Email"
                                 Password = dr["Password"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim()
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

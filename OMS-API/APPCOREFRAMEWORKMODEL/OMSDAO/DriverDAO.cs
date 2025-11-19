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
    public class DriverDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<DriverListReturn> ListDriverByCriteria(DriverInfo dInfo)
        {
            string strcond = "";

            if ((dInfo.DriverId != null) && (dInfo.DriverId != 0))
            {
                strcond += " and  c.Id =" + dInfo.DriverId;
            }

            if ((dInfo.Driver_No != null) && (dInfo.Driver_No != ""))
            {
                strcond += " and  c.CustomerCode like '%" + dInfo.Driver_No + "%'";
            }
            if ((dInfo.FName != null) && (dInfo.FName != ""))
            {
                strcond += " and  c.CustomerFName like '%" + dInfo.FName + "%'";
            }

            if ((dInfo.LName != null) && (dInfo.LName != ""))
            {
                strcond += " and  c.CustomerLName like '%" + dInfo.LName + "%'";
            }

            DataTable dt = new DataTable();
            var LDriver = new List<DriverListReturn>();

            try
            {
                string strsql = " select c.*,p.PhoneNumber,p.PhoneType  from " + dbName + ".dbo.Customer c left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +

                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDriver = (from DataRow dr in dt.Rows

                             select new DriverListReturn()
                             {
                                 DriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Driver_No = dr["CustomerCode"].ToString().Trim(),
                                 TitleName = dr["CustomerFName"].ToString().Trim(),
                                 FName = dr["CustomerLName"].ToString().Trim(),
                                 LName = dr["CustomerTypeCode"].ToString().Trim(),
                                 Gender = dr["Gender"].ToString().Trim(),
                                 Mobile = dr["PhoneNumber"].ToString().Trim(),
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

            return LDriver;
        }

        public List<DriverListReturn> ListDriverDetailByCriteria(CustomerPhoneInfo dInfo)
        {
            string strcond = "";

            if ((dInfo.PhoneNumber != null) && (dInfo.PhoneNumber != ""))
            {
                strcond += " and  p.PhoneNumber ='" + dInfo.PhoneNumber + "'";
            }

            DataTable dt = new DataTable();
            var LDriver = new List<DriverListReturn>();

            try
            {
                string strsql = " select c.*,p.PhoneNumber,p.PhoneType  from " + dbName + ".dbo.Customer c left join  " + dbName + ".dbo.CustomerPhone p on c.CustomerCode = p.CustomerCode " +

                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDriver = (from DataRow dr in dt.Rows

                           select new DriverListReturn()
                           {
                               DriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               Driver_No = dr["CustomerCode"].ToString().Trim(),
                               FName = dr["CustomerFName"].ToString().Trim(),
                               LName = dr["CustomerLName"].ToString().Trim(),
                               Gender = dr["Gender"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
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

            return LDriver;
        }



        public List<DriverListReturn> ListDriverByCriteriaNoPaging(DriverInfo dInfo)
        {
            string strcond = "";

            if ((dInfo.DriverId != null) && (dInfo.DriverId != 0))
            {
                strcond += " and  c.Id =" + dInfo.DriverId;
            }

            if ((dInfo.Driver_No != null) && (dInfo.Driver_No != ""))
            {
                strcond += " and  c.Driver_No like '%" + dInfo.Driver_No + "%'";
            }
            

            if ((dInfo.FName != null) && (dInfo.FName != ""))
            {
                strcond += " and CONCAT(Fname,Lname) like '%" + dInfo.FName + "%'";
            }


            DataTable dt = new DataTable();
            var LDriver = new List<DriverListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Driver c  " +

                               " where c.FlagDelete ='N' " + strcond;    

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDriver = (from DataRow dr in dt.Rows

                           select new DriverListReturn()
                           {
                               DriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               Driver_No = dr["Driver_No"].ToString().Trim(),
                               TitleCode = dr["TitleCode"].ToString().Trim(),
                               FName = dr["FName"].ToString().Trim(),
                               LName = dr["LName"].ToString().Trim(),
                               Gender = dr["Gender"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
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

            return LDriver;
        }


        public List<DriverListReturn> ListDriverByCriteria_showgv(DriverInfo dInfo)
        {
            string strcond = "";

            if ((dInfo.DriverId != null) && (dInfo.DriverId != 0))
            {
                strcond += " and  c.Id =" + dInfo.DriverId;
            }

            if ((dInfo.Driver_No != null) && (dInfo.Driver_No != ""))
            {
                strcond += " and  c.Driver_No like '%" + dInfo.Driver_No + "%'";
            }
            if ((dInfo.FName != null) && (dInfo.FName != ""))
            {
                strcond += " and  c.FName like '%" + dInfo.FName + "%'";
            }

            if ((dInfo.LName != null) && (dInfo.LName != ""))
            {
                strcond += " and  c.LName like '%" + dInfo.LName + "%'";
            }


            DataTable dt = new DataTable();
            var LDriver = new List<DriverListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Driver c  " +

                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + dInfo.rowOFFSet + " ROWS FETCH NEXT " + dInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDriver = (from DataRow dr in dt.Rows

                           select new DriverListReturn()
                           {
                               DriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               Driver_No = dr["Driver_No"].ToString().Trim(),
                               TitleCode = dr["TitleCode"].ToString().Trim(),
                               FName = dr["FName"].ToString().Trim(),
                               LName = dr["LName"].ToString().Trim(),
                               Gender = dr["Gender"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
                               CreateBy = dr["CreateBy"].ToString(),
                               CreateDate = dr["CreateDate"].ToString(),
                               UpdateBy = dr["UpdateBy"].ToString(),
                               UpdateDate = dr["UpdateDate"].ToString(),
                               FlagDelete = dr["FlagDelete"].ToString(),
                               FullName = dr["FName"].ToString() +" "+ dr["LName"].ToString().Trim(),
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDriver;
        }

        public int? CountDriverListByCriteria(DriverInfo dInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((dInfo.DriverId != null) && (dInfo.DriverId != 0))
            {
                strcond += " and  c.Id =" + dInfo.DriverId;
            }

            if ((dInfo.Driver_No != null) && (dInfo.Driver_No != ""))
            {
                strcond += " and  c.Driver_No like '%" + dInfo.Driver_No + "%'";
            }
            if ((dInfo.FName != null) && (dInfo.FName != ""))
            {
                strcond += " and  c.Fname like '%" + dInfo.FName + "%'";
            }

            if ((dInfo.LName != null) && (dInfo.LName != ""))
            {
                strcond += " and  c.Lname like '%" + dInfo.LName + "%'";
            }

            DataTable dt = new DataTable();
            var LDriver = new List<DriverListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countCustomer from " + dbName + ".dbo.Driver c " +

                           " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDriver = (from DataRow dr in dt.Rows

                           select new DriverListReturn()
                           {
                               countDriver = Convert.ToInt32(dr["countCustomer"])
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LDriver.Count > 0)
            {
                count = LDriver[0].countDriver;
            }

            return count;
        }

        public int InsertDriver(DriverInfo dInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.Driver   (Driver_no, TitleCode, FName, LName, Gender,Mobile , CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete)" +
                            "VALUES (" +
                           "'" + dInfo.Driver_No + "'," +
                           "'" + dInfo.TitleCode + "'," +
                           "'" + dInfo.FName + "'," +
                           "'" + dInfo.LName + "'," +
                           "'" + dInfo.Gender + "'," +
                           "'" + dInfo.Mobile + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + dInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + dInfo.UpdateBy + "'," +
                           "'" + dInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? UpdateDriver(DriverInfo dInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Driver set " +
                            " Driver_No = '" + dInfo.Driver_No + "'," +
                            " FName = '" + dInfo.FName + "'," +
                            " LName = '" + dInfo.LName + "'," +
                            " Gender = '" + dInfo.Gender + "'," +
                            " TitleCode = '" + dInfo.TitleCode + "'," +
                            " Mobile = '" + dInfo.Mobile + "'," +
                             " UpdateBy = '" + dInfo.UpdateBy + "'," +
                           " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                           " where id ='" + dInfo.DriverId + "'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteDriver(DriverInfo dInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Driver set FlagDelete = 'Y' where Id in (" + dInfo.strDriverId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<DriverListReturn> DriverCheck(DriverInfo dInfo)
        {
            string strcond = "";

            if ((dInfo.Driver_No != null) && (dInfo.Driver_No != ""))
            {
                strcond += " and  c.Driver_No ='" + dInfo.Driver_No + "'";
            }

            DataTable dt = new DataTable();
            var LDriver = new List<DriverListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.Driver c  " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDriver = (from DataRow dr in dt.Rows

                           select new DriverListReturn()
                           {

                               DriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               Driver_No = dr["Driver_No"].ToString().Trim(),
                               FName = dr["FName"].ToString().Trim(),
                               LName = dr["LName"].ToString().Trim(),
                               Gender = dr["Gender"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
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

            return LDriver;
        }

        public List<DriverListReturn> ListDriverByCriteria_dll(DriverInfo dInfo)
        {
            string strcond = "";

            if ((dInfo.DriverId != null) && (dInfo.DriverId != 0))
            {
                strcond += " and  c.Id =" + dInfo.DriverId;
            }

            if ((dInfo.Driver_No != null) && (dInfo.Driver_No != ""))
            {
                strcond += " and  c.Driver_No like '%" + dInfo.Driver_No + "%'";
            }
            if ((dInfo.FName != null) && (dInfo.FName != ""))
            {
                strcond += " and  c.FName like '%" + dInfo.FName + "%'";
            }

            if ((dInfo.LName != null) && (dInfo.LName != ""))
            {
                strcond += " and  c.LName like '%" + dInfo.LName + "%'";
            }

            DataTable dt = new DataTable();
            var LDriver = new List<DriverListReturn>();

            try
            {
                string strsql = " select c.fname +SPACE(2)+c.lname as FullName,c.* from " + dbName + ".dbo.Driver c  " +

                               " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LDriver = (from DataRow dr in dt.Rows

                           select new DriverListReturn()
                           {
                               DriverId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               Driver_No = dr["Driver_No"].ToString().Trim(),
                               FName = dr["FName"].ToString().Trim(),
                               LName = dr["LName"].ToString().Trim(),
                               Gender = dr["Gender"].ToString().Trim(),
                               Mobile = dr["Mobile"].ToString().Trim(),
                               CreateBy = dr["CreateBy"].ToString(),
                               CreateDate = dr["CreateDate"].ToString(),
                               UpdateBy = dr["UpdateBy"].ToString(),
                               UpdateDate = dr["UpdateDate"].ToString(),
                               FlagDelete = dr["FlagDelete"].ToString(),
                               FullName = dr["FullName"].ToString(),
                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LDriver;
        }

    }

}

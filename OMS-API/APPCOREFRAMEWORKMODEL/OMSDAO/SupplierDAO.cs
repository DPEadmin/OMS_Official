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
    public class SupplierDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int UpdateSupplier(SupplierInfo sInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Supplier set " +
                            " SupplierCode = '" + sInfo.SupplierCode + "'," +
                            " SupplierName = '" + sInfo.SupplierName + "'," +
                            " TaxIdNo = '" + sInfo.TaxIdNo + "'," +
                            " Address = '" + sInfo.Address + "'," +
                            " SubdistrictCode = '" + sInfo.SubDistrictCode + "'," +
                            " DistrictCode = '" + sInfo.DistrictCode + "'," +
                            " ProvinceCode = '" + sInfo.ProvinceCode + "'," +
                            " ZipNo = '" + sInfo.ZipNo + "'," +
                            " PhoneNo = '" + sInfo.PhoneNumber + "'," +
                            " FaxNo = '" + sInfo.FaxNumber + "'," +
                            " Mail = '" + sInfo.Mail + "'," +
                            " Contactor = '" + sInfo.Contactor + "'," +
                            " UpdateBy = '" + sInfo.UpdateBy + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id =" + sInfo.SupplierId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteSupplier(SupplierInfo sInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Supplier set FlagDelete = 'Y' where Id in (" + sInfo.SupplierCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertSupplier(SupplierInfo sInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Supplier  (SupplierCode,SupplierName,Address,DistrictCode,SubDistrictCode,ProvinceCode,PhoneNo,Mail,FaxNo,ZipNo,TaxIdNo,Contactor,ActiveFlag,CreateDate,CreateBy,UpdateDate,UpdateBy,FlagDelete)" +
                            "VALUES (" +
                           "'" + sInfo.SupplierCode + "'," +
                           "'" + sInfo.SupplierName + "'," +
                           "'" + sInfo.Address + "'," +
                           "'" + sInfo.DistrictCode + "'," +
                           "'" + sInfo.SubDistrictCode + "'," +
                           "'" + sInfo.ProvinceCode + "'," +
                           "'" + sInfo.PhoneNumber + "'," +
                           "'" + sInfo.Mail + "'," +
                           "'" + sInfo.FaxNumber + "'," +
                           "'" + sInfo.ZipNo + "'," +
                           "'" + sInfo.TaxIdNo + "'," +
                           "'" + sInfo.Contactor + "'," +
                           "'" + sInfo.ActiveFlag + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + sInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + sInfo.UpdateBy + "'," +
                           "'" + sInfo.FlagDelete + "'" +
                            ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<SupplierListReturn> ListSupplierNopagingByCriteria(SupplierInfo sInfo)
        {
            string strcond = "";

            if ((sInfo.SupplierId != null) && (sInfo.SupplierId != 0))
            {
                strcond += " and  c.Id =" + sInfo.SupplierId;
            }

            if ((sInfo.SupplierCode != null) && (sInfo.SupplierCode != ""))
            {
                strcond += " and  c.SupplierCode like '%" + sInfo.SupplierCode + "%'";
            }

            if ((sInfo.SupplierCode_Validate != null) && (sInfo.SupplierCode_Validate != ""))
            {
                strcond += " and  c.SupplierCode = '" + sInfo.SupplierCode_Validate + "'";
            }

            if ((sInfo.SupplierName != null) && (sInfo.SupplierName != ""))
            {
                strcond += " and  c.SupplierName like '%" + sInfo.SupplierName + "%'";
            }

            DataTable dt = new DataTable();
            var LSupplier = new List<SupplierListReturn>();

            try
            {
                string strsql = " select c.*,s.SubDistrictName,d.DistrictName,p.ProvinceName from " + dbName + ".dbo.Supplier c " +
                                " left join SubDistrict s on c.SubDistrictCode = s.SubDistrictCode " +
                                " left join District d on c.DistrictCode = d.DistrictCode " +
                                " left join Province p on c.ProvinceCode = p.ProvinceCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id Desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSupplier = (from DataRow dr in dt.Rows

                             select new SupplierListReturn()
                             {
                                 SupplierId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                 SupplierName = dr["SupplierName"].ToString().Trim(),
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 DistrictName = dr["DistrictName"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                 PhoneNumber = dr["PhoneNo"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 ZipNo = dr["ZipNo"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 FaxNumber = dr["FaxNo"].ToString(),
                                 TaxIdNo = dr["TaxIdNo"].ToString(),
                                 Mail = dr["Mail"].ToString(),
                                 Contactor = dr["Contactor"].ToString()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSupplier;
        }

        public List<SupplierListReturn> ListSupplierByCriteria(SupplierInfo sInfo)
        {
            string strcond = "";

            if ((sInfo.SupplierId != null) && (sInfo.SupplierId != 0))
            {
                strcond += " and  c.Id =" + sInfo.SupplierId;
            }

            if ((sInfo.SupplierCode != null) && (sInfo.SupplierCode != ""))
            {
                strcond += " and  c.SupplierCode like '%" + sInfo.SupplierCode + "%'";
            }

            if ((sInfo.SupplierCode_Validate != null) && (sInfo.SupplierCode_Validate != ""))
            {
                strcond += " and  c.SupplierCode = '" + sInfo.SupplierCode_Validate + "'";
            }

            if ((sInfo.SupplierName != null) && (sInfo.SupplierName != ""))
            {
                strcond += " and  c.SupplierName like '%" + sInfo.SupplierName + "%'";
            }
            if ((sInfo.ActiveFlag != null) && (sInfo.ActiveFlag != ""))
            {
                strcond += " and  c.ActiveFlag like '" + sInfo.ActiveFlag + "'";
            }

            DataTable dt = new DataTable();
            var LSupplier = new List<SupplierListReturn>();

            try
            {
                string strsql = " select c.*,s.SubDistrictName,d.DistrictName,p.ProvinceName from " + dbName + ".dbo.Supplier c " +
                                " left join SubDistrict s on c.SubDistrictCode = s.SubDistrictCode " +
                                " left join District d on c.DistrictCode = d.DistrictCode " +
                                " left join Province p on c.ProvinceCode = p.ProvinceCode " +
                               " where c.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY c.Id DESC OFFSET " + sInfo.rowOFFSet + " ROWS FETCH NEXT " + sInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSupplier = (from DataRow dr in dt.Rows

                             select new SupplierListReturn()
                             {
                                 SupplierId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                 SupplierName = dr["SupplierName"].ToString().Trim(),
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 DistrictName = dr["DistrictName"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                 PhoneNumber = dr["PhoneNo"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 ZipNo = dr["ZipNo"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString(),
                                 CreateDate = dr["CreateDate"].ToString(),
                                 UpdateBy = dr["UpdateBy"].ToString(),
                                 UpdateDate = dr["UpdateDate"].ToString(),
                                 FlagDelete = dr["FlagDelete"].ToString(),
                                 FaxNumber = dr["FaxNo"].ToString(),
                                 TaxIdNo = dr["TaxIdNo"].ToString(),
                                 Mail = dr["Mail"].ToString(),
                                 Contactor = dr["Contactor"].ToString(),
                                 ActiveFlag = dr["ActiveFlag"].ToString(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSupplier;
        }

        public int? CountSupplierListByCriteria(SupplierInfo cInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((cInfo.SupplierId != null) && (cInfo.SupplierId != 0))
            {
                strcond += " and  c.Id =" + cInfo.SupplierId;
            }

            if ((cInfo.SupplierCode != null) && (cInfo.SupplierCode != ""))
            {
                strcond += " and  c.SupplierCode like '%" + cInfo.SupplierCode + "%'";
            }
            if ((cInfo.SupplierName != null) && (cInfo.SupplierName != ""))
            {
                strcond += " and  c.SupplierName like '%" + cInfo.SupplierName + "%'";
            }
            if ((cInfo.ActiveFlag != null) && (cInfo.ActiveFlag != ""))
            {
                strcond += " and  c.ActiveFlag = '" + cInfo.ActiveFlag + "'";
            }

            DataTable dt = new DataTable();
            var LSupplier = new List<SupplierListReturn>();


            try
            {
                string strsql = "select count(c.Id) as countSupplier from " + dbName + ".dbo.Supplier c " +
                                " left join SubDistrict s on c.SubDistrictCode = s.SubDistrictCode " +
                                " left join District d on c.DistrictCode = d.DistrictCode " +
                                " left join Province p on c.ProvinceCode = p.ProvinceCode " +
                                " where c.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSupplier = (from DataRow dr in dt.Rows

                             select new SupplierListReturn()
                             {
                                 countSupplier = Convert.ToInt32(dr["countSupplier"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LSupplier.Count > 0)
            {
                count = LSupplier[0].countSupplier;
            }

            return count;
        }

        public List<SupplierListReturn> SupplierCodeValidate(SupplierInfo sInfo)
        {
            string strcond = "";

            if ((sInfo.SupplierCode != null) && (sInfo.SupplierCode != ""))
            {
                strcond += " and s.SupplierCode = '" + sInfo.SupplierCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var Lsupplier = new List<SupplierListReturn>();

            try
            {
                string strsql = " select s.* from Supplier s" + " where s.flagdelete = 'N' " +
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                Lsupplier = (from DataRow dr in dt.Rows

                             select new SupplierListReturn()
                             {
                                 SupplierId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 SupplierCode = dr["SupplierCode"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Lsupplier;
        }

        public List<SupplierListReturn> ListPOMapSupplier(SupplierInfo sInfo)
        {
            string strcond = "";

            if ((sInfo.POCode != null) && (sInfo.POCode != ""))
            {
                strcond += " and  p.POCode like '%" + sInfo.POCode + "%'";
            }

            DataTable dt = new DataTable();
            var LSupplier = new List<SupplierListReturn>();

            try
            {
                string strsql = " SELECT p.POCode, s.SupplierName, s.TaxIdNo, s.Address, sd.SubDistrictName, d.DistrictName, pr.ProvinceName, s.ZipNo, s.Contactor, s.PhoneNo, s.FaxNo, s.Mail " +
                                " from " + dbName + ".dbo.PO AS p " +
                                " left join " + dbName + ".dbo.Supplier AS s ON s.SupplierCode = p.SupplierCode " +
                                " left join " + dbName + ".dbo.SubDistrict AS sd ON sd.SubDistrictCode = s.SubdistrictCode " +
                                " left join " + dbName + ".dbo.District AS d ON d.DistrictCode = s.DistrictCode " +
                                " left join " + dbName + ".dbo.Province AS pr ON pr.ProvinceCode = s.ProvinceCode " +
                                " where (p.FlagDelete = 'N') " + strcond;

                strsql += " ORDER BY p.POCode Desc";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LSupplier = (from DataRow dr in dt.Rows

                             select new SupplierListReturn()
                             {
                                 POCode = dr["POCode"].ToString().Trim(),
                                 SupplierName = dr["SupplierName"].ToString().Trim(),
                                 SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                 DistrictName = dr["DistrictName"].ToString().Trim(),
                                 ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                 PhoneNumber = dr["PhoneNo"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 ZipNo = dr["ZipNo"].ToString().Trim(),
                                 FaxNumber = dr["FaxNo"].ToString(),
                                 TaxIdNo = dr["TaxIdNo"].ToString(),
                                 Mail = dr["Mail"].ToString(),
                                 Contactor = dr["Contactor"].ToString(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LSupplier;
        }
    }
}

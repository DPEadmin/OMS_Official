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
    public class InventoryDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountInventoryListByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((iInfo.InventoryId != null) && (iInfo.InventoryId != 0))
            {
                strcond += " and  i.Id =" + iInfo.InventoryId;
            }
            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += " and  i.InventoryCode like '%" + iInfo.InventoryCode + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and  i.MerchantCode = '" + iInfo.MerchantCode + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();


            try
            {
                string strsql = "select count(i.Id) as countInventory from " + dbName + ".dbo.Inventory i " +

                           " where i.FlagDelete ='N' and i.InventoryCode <> '-99' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                             select new InventoryListReturn()
                             {
                                 countInventory = Convert.ToInt32(dr["countInventory"])
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LInventory.Count > 0)
            {
                count = LInventory[0].countInventory;
            }

            return count;
        }

        public List<InventoryListReturn> ListInventoryPagingByCriteria(InventoryInfo iInfo)

        {
            string strcond = "";
            string stroffset = "";

            if ((iInfo.InventoryId != null) && (iInfo.InventoryId != 0))
            {
                strcond += " and  i.Id =" + iInfo.InventoryId;
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and  i.MerchantCode = '" + iInfo.MerchantCode + "'";
            }
            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += " and  i.InventoryCode like '%" + iInfo.InventoryCode + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and  i.InventoryName like '%" + iInfo.InventoryName + "%'";
            }
            if ((iInfo.InvCenterFlag != null) && (iInfo.InvCenterFlag != "") && (iInfo.InvCenterFlag != "-99"))
            {
                strcond += " and  i.InvCenterFlag = '" + iInfo.InvCenterFlag + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = " select i.*,me.MerchantName " +
                            " from " + dbName + ".dbo.Inventory i " +
                            " left join " + dbName + ".dbo.Merchant AS me on i.MerchantCode = me.MerchantCode" +
                            " where i.FlagDelete ='N' and i.InventoryCode <> '-99' " +
                            strcond;

                strsql += " ORDER BY i.Id DESC OFFSET " + iInfo.rowOFFSet + " ROWS FETCH NEXT " + iInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString().Trim(),
                                  Province = dr["Province"].ToString().Trim(),
                                  District = dr["District"].ToString().Trim(),
                                  SubDistrict = dr["SubDistrict"].ToString().Trim(),
                                  Address = dr["Address"].ToString().Trim(),
                                  PostCode = dr["PostCode"].ToString().Trim(),
                                  ContactTel = dr["ContactTel"].ToString().Trim(),
                                  Fax = dr["Fax"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                  MerchantName = dr["MerchantName"].ToString().Trim(),
                                  InvCenterFlag = dr["InvCenterFlag"].ToString().Trim(),
                                  Lat = dr["Lat"].ToString().Trim(),
                                  Long = dr["Long"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> ListInventoryNoPagingByCriteria(InventoryInfo iInfo)

        {
            string strcond = "";

            if ((iInfo.InventoryId != null) && (iInfo.InventoryId != 0))
            {
                strcond += " and  i.Id =" + iInfo.InventoryId;
            }

            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += " and  i.InventoryCode like '%" + iInfo.InventoryCode + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and  i.InventoryName like '%" + iInfo.InventoryName + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and  i.MerchantCode like '%" + iInfo.MerchantCode + "%'";
            }
            if ((iInfo.CompanyCode != null) && (iInfo.CompanyCode != ""))
            {
                strcond += " and  i.CompanyCode like '%" + iInfo.CompanyCode + "%'";
            }
            if ((iInfo.AreaCode != null) && (iInfo.AreaCode != ""))
            {
                strcond += " and  i.AreaCode like '%" + iInfo.AreaCode + "%'";
            }


            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = " select i.* , p.ProvinceName, d.DistrictName, s.SubDistrictName " +
                            " from " + dbName + ".dbo.Inventory i LEFT OUTER JOIN " +
                            dbName + ".dbo.Province AS p ON p.ProvinceCode = i.Province LEFT OUTER JOIN " +
                            dbName + ".dbo.District AS d ON d.DistrictCode = i.District LEFT OUTER JOIN " +
                            dbName + ".dbo.SubDistrict AS s ON s.SubDistrictCode = i.SubDistrict " +
                            " where i.FlagDelete ='N' and i.InventoryCode <> '-99' " +
                            strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString().Trim(),
                                  Province = dr["Province"].ToString().Trim(),
                                  ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                  District = dr["District"].ToString().Trim(),
                                  DistrictName = dr["DistrictName"].ToString().Trim(),
                                  SubDistrict = dr["SubDistrict"].ToString().Trim(),
                                  SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                  Address = dr["Address"].ToString().Trim(),
                                  PostCode = dr["PostCode"].ToString().Trim(),
                                  ContactTel = dr["ContactTel"].ToString().Trim(),
                                  Fax = dr["Fax"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                  Lat = dr["Lat"].ToString().Trim(),
                                 Long = dr["Long"].ToString().Trim(),
                                 Polygon = dr["Polygon"].ToString().Trim(),
                                  CompanyCode= dr["CompanyCode"].ToString(),
                                  AreaCode = dr["AreaCode"].ToString()
                                  

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> ListInventorybyEmpNoPagingCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryId != null) && (iInfo.InventoryId != 0))
            {
                strcond += " and  i.Id =" + iInfo.InventoryId;
            }

            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += " and  i.InventoryCode like '%" + iInfo.InventoryCode + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and  i.InventoryName like '%" + iInfo.InventoryName + "%'";
            }
            if ((iInfo.EmpCode != null) && (iInfo.EmpCode != ""))
            {
                strcond += " and  ei.EmpCode like '%" + iInfo.EmpCode + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = " select i.id, i.InventoryCode, i.InventoryName, i.flagdelete from " + dbName + ".dbo.Inventory i " +
                                " left join EmpInventory ei ON ei.InventoryCode = i.InventoryCode where ei.flagdelete = 'N' and i.flagdelete = 'N' " +
                                strcond;

                strsql += " ORDER BY i.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                  InventoryName = dr["InventoryName"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim()
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> ListInventoryInfoPagingByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and i.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = " select i.* " +
                            " from " + dbName + ".dbo.Inventory i " +
                            " where i.FlagDelete ='N' " +
                            strcond;

                strsql += " ORDER BY i.Id DESC OFFSET " + iInfo.rowOFFSet + " ROWS FETCH NEXT " + iInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString().Trim(),
                                  Province = dr["Province"].ToString().Trim(),
                                  District = dr["District"].ToString().Trim(),
                                  SubDistrict = dr["SubDistrict"].ToString().Trim(),
                                  Address = dr["Address"].ToString().Trim(),
                                  PostCode = dr["PostCode"].ToString().Trim(),
                                  ContactTel = dr["Fax"].ToString().Trim(),
                                  Fax = dr["ContactTel"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim(),
                                  Lat = dr["Lat"].ToString().Trim(),
                                  Long = dr["Long"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public int InsertInventory(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = "insert into Inventory (InventoryCode, InventoryName,Lat,Long, MerchantCode, Address, Province, District, SubDistrict, PostCode, Fax, ContactTel, InvCenterFlag, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete) values (" +
                             "'" + iInfo.InventoryCode + "', " +
                             "'" + iInfo.InventoryName + "', " +
                               "'" + iInfo.Lat + "', " +
                             "'" + iInfo.Long + "', " +
                             "'" + iInfo.MerchantCode + "', " +
                             "'" + iInfo.Address + "', " +
                             "'" + iInfo.Province + "', " +
                             "'" + iInfo.District + "', " +
                             "'" + iInfo.SubDistrict + "', " +
                             "'" + iInfo.PostCode + "', " +
                             "'" + iInfo.Fax + "', " +
                             "'" + iInfo.ContactTel + "', " +
                             "'" + iInfo.InvCenterFlag + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.UpdateBy + "', " +
                             "'" + iInfo.FlagDelete + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteInventory(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Inventory set " +

                         " FlagDelete = 'Y'" +

                         " where Id in(" + iInfo.InventoryId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeleteInventoryList(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Inventory set " +

                         " FlagDelete = 'Y'" +

                         " where Id in(" + iInfo.InventoryCode + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInventory(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Inventory set " +

                            " InventoryCode = '" + iInfo.InventoryCode + "'," +
                            " InventoryName = '" + iInfo.InventoryName + "'," +
                              " Lat = '" + iInfo.Lat + "'," +
                            " Long = '" + iInfo.Long + "'," +
                            " Address = '" + iInfo.Address + "'," +
                            " Province = '" + iInfo.Province + "'," +
                            " District = '" + iInfo.District + "'," +
                            " SubDistrict = '" + iInfo.SubDistrict + "'," +
                            " PostCode = '" + iInfo.PostCode + "'," +
                            " ContactTel = '" + iInfo.ContactTel + "'," +
                            " Fax = '" + iInfo.Fax + "'," +
                            " InvCenterFlag = '" + iInfo.InvCenterFlag + "'," +
                            " CreateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'" +
                            " where Id =" + iInfo.InventoryId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateInventoryPolygon(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Inventory set " +

                              " Polygon = '" + iInfo.Polygon + "'," +

                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'" +
                            " where InventoryCode ='" + iInfo.InventoryCode+"'";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<InventoryListReturn> ListInventoryValidatePagingByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCodeValidate != null) && (iInfo.InventoryCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryCode like '" + iInfo.InventoryCodeValidate.Trim() + "%'" : " and i.InventoryCode like '" + iInfo.InventoryCodeValidate.Trim() + "'";
            }
            if ((iInfo.InventoryNameValidate != null) && (iInfo.InventoryNameValidate != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryName like '" + iInfo.InventoryNameValidate.Trim() + "%'" : " and i.InventoryName like '" + iInfo.InventoryNameValidate.Trim() + "'";
            }
            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'" : " and i.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'" : " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.FlagDelete != null) && (iInfo.FlagDelete != ""))
            {
                strcond += (strcond == "") ? " where  i.FlagDelete = '" + iInfo.FlagDelete.Trim() + "'" : " and i.FlagDelete = '" + iInfo.FlagDelete.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "select i.*" +
                                " from " + dbName + ".dbo.Inventory i " +
                                strcond;

              

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim()
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> GetListInventorybyCateriaNoPaging(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCodeValidate != null) && (iInfo.InventoryCodeValidate != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryCode like '" + iInfo.InventoryCodeValidate.Trim() + "%'" : " and i.InventoryCode like '" + iInfo.InventoryCodeValidate.Trim() + "'";
            }
            if ((iInfo.InventoryNameValidate != null) && (iInfo.InventoryNameValidate != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryName like '" + iInfo.InventoryNameValidate.Trim() + "%'" : " and i.InventoryName like '" + iInfo.InventoryNameValidate.Trim() + "'";
            }
            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'" : " and i.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += (strcond == "") ? " where  i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'" : " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "select i.*, p.ProvinceName, d.DistrictName, s.SubDistrictName " +
                                " from " + dbName + ".dbo.Inventory i left join " + dbName + ".dbo.Province p " +
                                " ON p.ProvinceCode = i.Province left join " + dbName + ".dbo.District d " +
                                " ON d.DistrictCode = i.District left join " + dbName + ".dbo.SubDistrict s " +
                                " ON s.SubDistrictCode = i.SubDistrict " +
                                strcond;



                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString().Trim(),
                                  Address = dr["Address"].ToString().Trim(),
                                  Province = dr["Province"].ToString().Trim(),
                                  ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                  District = dr["District"].ToString().Trim(),
                                  DistrictName = dr["DistrictName"].ToString().Trim(),
                                  SubDistrict = dr["SubDistrict"].ToString().Trim(),
                                  SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                  PostCode = dr["PostCode"].ToString().Trim(),
                                  ContactTel = dr["ContactTel"].ToString().Trim(),
                                  Fax = dr["Fax"].ToString().Trim(),
                                  FlagDelete = dr["FlagDelete"].ToString().Trim()
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> POMapInventoryNoPaging(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.POCode != "") && (iInfo.POCode != null))
            {
                strcond += " and p.POCode like '%" + iInfo.POCode.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "SELECT p.POCode, i.InventoryName, i.Address, s.SubDistrictName, d.DistrictName, pr.ProvinceName, i.PostCode, i.ContactTel, i.Fax " +
                                " from " + dbName + ".dbo.PO AS p LEFT OUTER JOIN " +
                                dbName + ".dbo.Inventory AS i ON i.InventoryCode = p.InventoryCode LEFT OUTER JOIN " +
                                dbName + ".dbo.SubDistrict AS s ON s.SubDistrictCode = i.SubDistrict LEFT OUTER JOIN " +
                                dbName + ".dbo.District AS d ON d.DistrictCode = i.District LEFT OUTER JOIN " +
                                dbName + ".dbo.Province AS pr ON pr.ProvinceCode = i.Province " +
                                " where i.FlagDelete ='N' " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  POCode = dr["POCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString().Trim(),
                                  Address = dr["Address"].ToString().Trim(),
                                  ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                  DistrictName = dr["DistrictName"].ToString().Trim(),
                                  SubDistrictName = dr["SubDistrictName"].ToString().Trim(),
                                  PostCode = dr["PostCode"].ToString().Trim(),
                                  ContactTel = dr["ContactTel"].ToString().Trim(),
                                  Fax = dr["Fax"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }





        public int UpdateInventorycenterflag(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Inventory set " +

                            " InvCenterFlag = '" + iInfo.InvCenterFlag + "'," +
                        
                            " where MerchantCode =" + iInfo.MerchantCode;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }



    }
}

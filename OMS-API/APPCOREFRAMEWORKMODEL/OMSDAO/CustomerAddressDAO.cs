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
    public class CustomerAddressDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int DeleteCustomerAddress(CustomerAddressInfo caInfo)
        {
            int i = 0;

            string strsql = "delete from CustomerAddressDetail" +
                            " where Id in (" + caInfo.CustomerAddressId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<CustomerAddressListReturn> ListAddressValidate(CustomerAddressInfo caInfo)
        {
            string strcond = "";

            if ((caInfo.CustomerCode != null) && (caInfo.CustomerCode != ""))
            {
                strcond += " and  c.CustomerCode = '" + caInfo.CustomerCode +"'";
            }

            if ((caInfo.Address != null) && (caInfo.Address != ""))
            {
                strcond += " and  c.Address like '%" + caInfo.Address + "%'";
            }
            if ((caInfo.Subdistrict != null) && (caInfo.Subdistrict != ""))
            {
                strcond += " and  c.Subdistrict like '%" + caInfo.Subdistrict + "%'";
            }

            if ((caInfo.District != null) && (caInfo.District != ""))
            {
                strcond += " and  c.District like '%" + caInfo.District + "%'";
            }

            if ((caInfo.Province != null) && (caInfo.Province != ""))
            {
                strcond += " and  c.Province = '" + caInfo.Province + "'";
            }

            if ((caInfo.ZipCode != null) && (caInfo.ZipCode != ""))
            {
                strcond += " and  c.ZipCode = '" + caInfo.ZipCode + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerAddress = new List<CustomerAddressListReturn>();

            try
            {
                string strsql = " select c.* from " + dbName + ".dbo.CustomerAddressDetail c " +
                               " where c.FlagActive ='Y' " + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerAddress = (from DataRow dr in dt.Rows

                             select new CustomerAddressListReturn()
                             {
                                 CustomerAddressId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 Address = dr["Address"].ToString().Trim(),
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerAddress;
        }

        public int InsertCustomerAddress(CustomerAddressInfo caInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO CustomerAddressDetail (address, subdistrict, district, province, zipcode, CustomerCode, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagActive, AddressType,FlagDefault, Address_Fullname, Address_Tel,Lat,Long) " +
                  "VALUES (" +
                  "'" + caInfo.Address + "'," +
                  "'" + caInfo.Subdistrict + "'," +
                  "'" + caInfo.District + "'," +
                  "'" + caInfo.Province + "'," +
                  "'" + caInfo.ZipCode + "'," +
                  "'" + caInfo.CustomerCode + "'," +
                  "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                  "'" + caInfo.CreateBy + "'," +
                  "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                  "'" + caInfo.UpdateBy + "'," +
                  "'" + caInfo.FlagActive + "'," + 
                  "'" + caInfo.AddressType + "'," +
                  "'" + caInfo.FlagDefault + "'," +
                  "'" + caInfo.Address_Fullname + "'," +
                  "'" + caInfo.Address_Tel + "'," +
                       "'" + caInfo.Lat + "'," +
                  "'" + caInfo.Long + "'" +
                  ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateFlagDefaultCustomerAddress(CustomerAddressInfo caInfo)
        {
            int i = 0;

            string strsql = "UPDATE CustomerAddressDetail  SET " +
                             " CustomerCode = '" + caInfo.CustomerCode + "'," +
                            " FlagDefault = 'N' "+" , "+
                           " where CustomerCode =" + caInfo.CustomerCode + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateCustomerAddress(CustomerAddressInfo caInfo)
        {
            int i = 0;

            string strsql = "UPDATE CustomerAddressDetail  SET " +
                            " address = '" + caInfo.Address + "'," +
                            " subdistrict = '" + caInfo.Subdistrict + "'," +
                            " district = '" + caInfo.District + "'," +
                            " province = '" + caInfo.Province + "'," +
                            " zipcode = '" + caInfo.ZipCode + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + caInfo.UpdateBy + "'," +
                            " FlagActive = '" + caInfo.FlagActive + "'," +
                            " AddressType = '" + caInfo.AddressType + "'," +
                             "FlagDefault ='" + caInfo.FlagDefault + "'," +
                  "Address_Fullname = '" + caInfo.Address_Fullname + "'," +
                                         "Lat ='" + caInfo.Lat + "'," +
                  "Long = '" + caInfo.Long + "'," +
                  "Address_Tel = '" + caInfo.Address_Tel + "'" +
                            " where Id =" + caInfo.CustomerAddressId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateCustomerAddressDateByAPI(CustomerAddressInfo caInfo)
        {
            int i = 0;
            string strcond = "";

            if ((caInfo.Address != null) && (caInfo.Address != ""))
            {
                strcond += " and  Address like '%" + caInfo.Address + "%'";
            }
            if ((caInfo.Subdistrict != null) && (caInfo.Subdistrict != ""))
            {
                strcond += " and  Subdistrict like '%" + caInfo.Subdistrict + "%'";
            }

            if ((caInfo.District != null) && (caInfo.District != ""))
            {
                strcond += " and  District like '%" + caInfo.District + "%'";
            }

            if ((caInfo.Province != null) && (caInfo.Province != ""))
            {
                strcond += " and  Province = '" + caInfo.Province + "'";
            }

            if ((caInfo.ZipCode != null) && (caInfo.ZipCode != ""))
            {
                strcond += " and  ZipCode = '" + caInfo.ZipCode + "'";
            }

            string strsql = "UPDATE CustomerAddressDetail  SET " +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where CustomerCode = '" + caInfo.CustomerCode + "'" + strcond;



            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateCustomerAddressLatLng(CustomerAddressInfo caInfo)
        {
            int i = 0;

            string strsql = "UPDATE CustomerAddressDetail  SET " +
                            " Lat = '" + caInfo.Lat + "'," +
                            " Long = '" + caInfo.Long + "'," +
                            " AreaCode = '" + caInfo.AreaCode + "'," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + caInfo.UpdateBy + "'" +
                            " where Id =" + caInfo.CustomerAddressId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int? CountCustomerAddressListByCriteria(CustomerAddressInfo caInfo)
        {
            string strcond = "";
            int? count = 0;
            if ((caInfo.CustomerAddressId != null) && (caInfo.CustomerAddressId != 0))
            {
                strcond += "and c.Id =" + caInfo.CustomerAddressId;
            }

            if ((caInfo.Address != null) && (caInfo.Address != ""))
            {
                strcond += "and c.Address like '%" + caInfo.Address + "%'";
            }
            if ((caInfo.Subdistrict != null) && (caInfo.Subdistrict != ""))
            {
                strcond += "and c.Subdistrict like '%" + caInfo.Subdistrict + "%'";
            }

            if ((caInfo.District != null) && (caInfo.District != ""))
            {
                strcond += "and c.District = '" + caInfo.District + "'";
            }
            if ((caInfo.Province != null) && (caInfo.Province != ""))
            {
                strcond += "and c.Province = '" + caInfo.Province + "'";
            }
            if ((caInfo.ZipCode != null) && (caInfo.ZipCode != ""))
            {
                strcond += "and c.ZipCode = '" + caInfo.ZipCode + "'";
            }
            if ((caInfo.CustomerCode != null) && (caInfo.CustomerCode != ""))
            {
                strcond += "and c.CustomerCode = '" + caInfo.CustomerCode + "'";
            }

            if ((caInfo.CustomerCode == null) && (caInfo.CustomerCode == ""))
            {
                strcond += "and c.CustomerCode = ''";
            }

            DataTable dt = new DataTable();
            var LCustomerAddress = new List<CustomerAddressListReturn>();

            try
            {
                string strsql = "select count(c.Id) as countCustomerAdress from " + dbName + ".dbo.CustomerAddressDetail c " +
                                  " left join Province p on c.province = p.ProvinceCode" +
                                " left join District d on c.district = d.DistrictCode" +
                                " left join SubDistrict s on c.SubDistrict = s.SubDistrictCode" +

                                 " where c.FlagActive = 'Y'" + strcond;


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerAddress = (from DataRow dr in dt.Rows

                                    select new CustomerAddressListReturn()
                                    {
                                        countCustomerAdress = Convert.ToInt32(dr["countCustomerAdress"])
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            if (LCustomerAddress.Count > 0)
            {
                count = LCustomerAddress[0].countCustomerAdress;
            }


            return count;
        }

        public List<CustomerAddressListReturn> ListCustomerAddressByCriteria(CustomerAddressInfo caInfo)
        {
            string strcond = "";

            if ((caInfo.CustomerAddressId != null) && (caInfo.CustomerAddressId != 0))
            {
                strcond += "and c.Id =" + caInfo.CustomerAddressId;
            }

            if ((caInfo.Address != null) && (caInfo.Address != ""))
            {
                strcond += "and c.Address like '%" + caInfo.Address + "%'";
            }
            if ((caInfo.Subdistrict != null) && (caInfo.Subdistrict != ""))
            {
                strcond += "and c.Subdistrict like '%" + caInfo.Subdistrict + "%'";
            }

            if ((caInfo.District != null) && (caInfo.District != ""))
            {
                strcond += "and c.District = '" + caInfo.District + "'";
            }
            if ((caInfo.Province != null) && (caInfo.Province != ""))
            {
                strcond += "and c.Province = '" + caInfo.Province + "'";
            }
            if ((caInfo.ZipCode != null) && (caInfo.ZipCode != ""))
            {
                strcond += "and c.ZipCode = '" + caInfo.ZipCode + "'";
            }
            if ((caInfo.CustomerCode != null) && (caInfo.CustomerCode != ""))
            {
                strcond += "and c.CustomerCode = '" + caInfo.CustomerCode + "'";
            }

            if ((caInfo.AddressType != null) && (caInfo.AddressType != ""))
            {
                strcond += "and c.AddressType = '" + caInfo.AddressType + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerAddress = new List<CustomerAddressListReturn>();

            try
            {
                string strsql = " select c.*,p.ProvinceName,s.SubDistrictName,d.DistrictName from " + dbName + ".dbo.CustomerAddressDetail c " +
                                " left join Province p on c.province = p.ProvinceCode" +
                                " left join District d on c.district = d.DistrictCode" +
                                " left join SubDistrict s on c.SubDistrict = s.SubDistrictCode" +
                                " where  c.FlagActive = 'Y'" + strcond + "order by FlagDefault DESC";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerAddress = (from DataRow dr in dt.Rows

                                    select new CustomerAddressListReturn()
                                    {
                                        CustomerAddressId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        Address = dr["address"].ToString().Trim(),
                                        AddressType = dr["AddressType"].ToString().Trim(),
                                        Subdistrict = dr["subdistrict"].ToString().Trim(),
                                        District = dr["district"].ToString().Trim(),
                                        Province = dr["province"].ToString().Trim(),
                                        ZipCode = dr["zipcode"].ToString().Trim(),
                                        DistrictName = dr["DistrictName"].ToString().Trim(),
                                        ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                        SubdistrictName = dr["SubdistrictName"].ToString().Trim(),
                                        CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                        CreateBy = dr["CreateBy"].ToString(),
                                        CreateDate = dr["CreateDate"].ToString(),
                                        UpdateBy = dr["UpdateBy"].ToString(),
                                        UpdateDate = dr["UpdateDate"].ToString(),
                                        FlagActive = dr["FlagActive"].ToString(),
                                        FlagDefault = dr["FlagDefault"].ToString(),
                                        Address_Fullname = dr["Address_Fullname"].ToString(),
                                        Address_Tel = dr["Address_Tel"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerAddress;
        }

        public int UpdateDateCustomerAddress(CustomerAddressInfo caInfo)
        {
            int i = 0;

            string strsql = "UPDATE CustomerAddressDetail  SET " +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where Id =" + caInfo.CustomerAddressId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<CustomerAddressListReturn> GetLatestUpdatedCustomerAddress(CustomerAddressInfo caInfo)
        {
            string strcond = "";

            if ((caInfo.CustomerAddressId != null) && (caInfo.CustomerAddressId != 0))
            {
                strcond += "and c.Id =" + caInfo.CustomerAddressId;
            }
            if ((caInfo.UniqueID != null) && (caInfo.UniqueID != ""))
            {
                strcond += "and cm.UniqueID = '" + caInfo.UniqueID + "'";
            }
            if ((caInfo.Address != null) && (caInfo.Address != ""))
            {
                strcond += "and c.Address like '%" + caInfo.Address + "%'";
            }
            if ((caInfo.Subdistrict != null) && (caInfo.Subdistrict != ""))
            {
                strcond += "and c.Subdistrict like '%" + caInfo.Subdistrict + "%'";
            }

            if ((caInfo.District != null) && (caInfo.District != ""))
            {
                strcond += "and c.District = '" + caInfo.District + "'";
            }
            if ((caInfo.Province != null) && (caInfo.Province != ""))
            {
                strcond += "and c.Province = '" + caInfo.Province + "'";
            }
            if ((caInfo.ZipCode != null) && (caInfo.ZipCode != ""))
            {
                strcond += "and c.ZipCode = '" + caInfo.ZipCode + "'";
            }
            if ((caInfo.CustomerCode != null) && (caInfo.CustomerCode != ""))
            {
                strcond += "and c.CustomerCode = '" + caInfo.CustomerCode + "'";
            }

            if ((caInfo.AddressType != null) && (caInfo.AddressType != ""))
            {
                strcond += "and c.AddressType = '" + caInfo.AddressType + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerAddress = new List<CustomerAddressListReturn>();

            try
            {
                string strsql = " select c.*,p.ProvinceName,s.SubDistrictName,d.DistrictName,cm.UniqueID from " + dbName + ".dbo.CustomerAddressDetail c " +
                                " left join Province p on c.province = p.ProvinceCode" +
                                " left join District d on c.district = d.DistrictCode" +
                                " left join SubDistrict s on c.SubDistrict = s.SubDistrictCode " +
                                " left join Customer cm on c.CustomerCode = cm.CustomerCode " +
                                " where  c.FlagActive = 'Y'" + strcond +"order by c.UpdateDate desc";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerAddress = (from DataRow dr in dt.Rows

                                    select new CustomerAddressListReturn()
                                    {
                                        CustomerAddressId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        Address = dr["address"].ToString().Trim(),
                                        AddressType = dr["AddressType"].ToString().Trim(),
                                        Subdistrict = dr["subdistrict"].ToString().Trim(),
                                        District = dr["district"].ToString().Trim(),
                                        Province = dr["province"].ToString().Trim(),
                                        ZipCode = dr["zipcode"].ToString().Trim(),
                                        DistrictName = dr["DistrictName"].ToString().Trim(),
                                        ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                        SubdistrictName = dr["SubdistrictName"].ToString().Trim(),
                                        CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                        UniqueID = dr["UniqueID"].ToString().Trim(),
                                        CreateBy = dr["CreateBy"].ToString(),
                                        CreateDate = dr["CreateDate"].ToString(),
                                        UpdateBy = dr["UpdateBy"].ToString(),
                                        UpdateDate = dr["UpdateDate"].ToString(),
                                        FlagActive = dr["FlagActive"].ToString(),
                                        Lat = dr["Lat"].ToString(),
                                        Long = dr["Long"].ToString(),
                                        AreaCode = dr["AreaCode"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerAddress;
        }

        public List<CustomerAddressListReturn> ListAllCustomerAddressListByCriteria(CustomerAddressInfo caInfo)
        {
            string strcond = "";

            if ((caInfo.CustomerAddressId != null) && (caInfo.CustomerAddressId != 0))
            {
                strcond += "and c.Id =" + caInfo.CustomerAddressId;
            }

            if ((caInfo.Address != null) && (caInfo.Address != ""))
            {
                strcond += "and c.Address like '%" + caInfo.Address + "%'";
            }
            if ((caInfo.Subdistrict != null) && (caInfo.Subdistrict != ""))
            {
                strcond += "and c.Subdistrict like '%" + caInfo.Subdistrict + "%'";
            }

            if ((caInfo.District != null) && (caInfo.District != ""))
            {
                strcond += "and c.District = '" + caInfo.District + "'";
            }
            if ((caInfo.Province != null) && (caInfo.Province != ""))
            {
                strcond += "and c.Province = '" + caInfo.Province + "'";
            }
            if ((caInfo.ZipCode != null) && (caInfo.ZipCode != ""))
            {
                strcond += "and c.ZipCode = '" + caInfo.ZipCode + "'";
            }
            if ((caInfo.CustomerCode != null) && (caInfo.CustomerCode != ""))
            {
                strcond += "and c.CustomerCode = '" + caInfo.CustomerCode + "'";
            }

            if ((caInfo.AddressType != null) && (caInfo.AddressType != ""))
            {
                strcond += "and c.AddressType = '" + caInfo.AddressType + "'";
            }

            DataTable dt = new DataTable();
            var LCustomerAddress = new List<CustomerAddressListReturn>();

            try
            {
                string strsql = " select c.*,p.ProvinceName,s.SubDistrictName,d.DistrictName, a.LookupValue as AddressTypeName, cs.LookupValue as FlagActiveName from " + dbName + ".dbo.CustomerAddressDetail c " +
                                " left join Province p on c.province = p.ProvinceCode" +
                                " left join District d on c.district = d.DistrictCode" +
                                " left join SubDistrict s on c.SubDistrict = s.SubDistrictCode " +
                                " left join Lookup a on a.LookupCode = c.AddressType and a.LookupType='ADDRESSTYPE' " +
                                " left join Lookup cs on cs.LookupCode = c.FlagActive and cs.LookupType='CUSADDRESSSTATUS' " +
                                " where 1 = 1 " + strcond;
                strsql += " ORDER BY c.Id DESC OFFSET " + caInfo.rowOFFSet + " ROWS FETCH NEXT " + caInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCustomerAddress = (from DataRow dr in dt.Rows

                                    select new CustomerAddressListReturn()
                                    {
                                        CustomerAddressId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        Address = dr["address"].ToString().Trim(),
                                        AddressType = dr["AddressType"].ToString().Trim(),
                                        AddressTypeName = dr["AddressTypeName"].ToString().Trim(),
                                        Subdistrict = dr["subdistrict"].ToString().Trim(),
                                        District = dr["district"].ToString().Trim(),
                                        Province = dr["province"].ToString().Trim(),
                                        ZipCode = dr["zipcode"].ToString().Trim(),
                                        DistrictName = dr["DistrictName"].ToString().Trim(),
                                        ProvinceName = dr["ProvinceName"].ToString().Trim(),
                                        SubdistrictName = dr["SubdistrictName"].ToString().Trim(),
                                        CustomerCode = dr["CustomerCode"].ToString().Trim(),
                                        CreateBy = dr["CreateBy"].ToString(),
                                        CreateDate = dr["CreateDate"].ToString(),
                                        UpdateBy = dr["UpdateBy"].ToString(),
                                        UpdateDate = dr["UpdateDate"].ToString(),
                                        FlagActive = dr["FlagActive"].ToString(),
                                        FlagActiveName = dr["FlagActiveName"].ToString(),
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerAddress;
        }

        public int UpdateCustomerAddressByEcommerce(CustomerAddressInfo caInfo)
        {
            int i = 0;
            string strcond = "";

            if ((caInfo.Address != null) && (caInfo.Address != ""))
            {
                strcond += " Address = '" + caInfo.Address + "',";
            }
            if ((caInfo.Subdistrict != null) && (caInfo.Subdistrict != ""))
            {
                strcond += " Subdistrict = '" + caInfo.Subdistrict + "',";
            }

            if ((caInfo.District != null) && (caInfo.District != ""))
            {
                strcond += " District = '" + caInfo.District + "',";
            }

            if ((caInfo.Province != null) && (caInfo.Province != ""))
            {
                strcond += " Province = '" + caInfo.Province + "',";
            }

            if ((caInfo.ZipCode != null) && (caInfo.ZipCode != ""))
            {
                strcond += " ZipCode = '" + caInfo.ZipCode + "',";
            }

            if ((caInfo.AddressType != null) && (caInfo.AddressType != ""))
            {
                strcond += " AddressType = '" + caInfo.AddressType + "',";
            }

            string strsql = "UPDATE CustomerAddressDetail  SET " +
                            strcond +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +                            
                            " where CustomerCode = '" + caInfo.CustomerCode + "'";

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
    }
}

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
    public class AddressDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<AddressListReturn> ListAddressByCriteria(AddressInfo aInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LAddress = new List<AddressListReturn>();

            try
            {
                string strsql = @" select a.id,p.ProvinceName Province,a.[address] ,d.DistrictName District,sd.SubDistrictName SubDistrict
                                          ,d.DistrictCode DistrictCode,sd.SubDistrictCode SubDistrictCode,p.ProvinceCode ProvinceCode,a.ZipCode " +
                                      " from " + dbName + ".[dbo].[CustomerAddressDetail] a " +
                                      " left join " + dbName + ".[dbo].[District] d on a.district = d.DistrictCode " +
                                      " left join " + dbName + ".[dbo].[SubDistrict] sd on a.SubDistrict = sd.SubDistrictCode" +
                                      " left join " + dbName + ".[dbo].[Province] p on a.Province = p.ProvinceCode " +

                                  " where a.FlagActive ='Y' and a.customercode = '" + aInfo.CustomerCode + "'";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LAddress = (from DataRow dr in dt.Rows

                             select new AddressListReturn()
                             {
                                 CustomerAddressId = Convert.ToInt32(dr["id"]),
                                 Province = dr["Province"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 District = dr["District"].ToString().Trim(),
                                 SubDistrict = dr["SubDistrict"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 ZipCode = dr["ZipCode"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LAddress;
        }
    }
}

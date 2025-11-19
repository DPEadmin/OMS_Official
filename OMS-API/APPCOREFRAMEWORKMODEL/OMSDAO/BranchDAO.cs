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
    public class BranchDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<BranchListReturn> ListAddressByCriteria(BranchInfo aInfo)
        {
            string strcond = "";

            if ((aInfo.BranchId != null) && (aInfo.BranchId != 0))
            {
                strcond += " and  b.Id =" + aInfo.BranchId;
            }

            if ((aInfo.CompanyCode != null) && (aInfo.CompanyCode != ""))
            {
                strcond += " and  b.CompanyCode = '" + aInfo.CompanyCode + "'";
            }

            if ((aInfo.AreaCode != null) && (aInfo.AreaCode != ""))
            {
                strcond += " and  b.AreaCode = '" + aInfo.AreaCode + "'";
            }

            DataTable dt = new DataTable();
            var LAddress = new List<BranchListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.Branch b " +
                                " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LAddress = (from DataRow dr in dt.Rows

                             select new BranchListReturn()
                             {
                                 BranchId = Convert.ToInt32(dr["id"]),
                                 BranchCode = dr["BranchCode"].ToString().Trim(),
                                 BranchType = dr["BranchType"].ToString().Trim(),
                                 BranchName = dr["BranchName"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ZipCode = dr["ZipCode"].ToString().Trim(),
                                 Lat = dr["Lat"].ToString().Trim(),
                                 Long = dr["Long"].ToString().Trim(),
                                 Distance = (dr["Distance"].ToString() != "") ? Convert.ToDouble(dr["Distance"]) : 0,
                                 OnlineStatus = dr["OnlineStatus"].ToString().Trim(),

                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LAddress;
        }


        public List<AreaListReturn> ListAreaByCriteria(AreaInfo aInfo)
        {
            string strcond = "";


            DataTable dt = new DataTable();
            var LArea = new List<AreaListReturn>();

            try
            {
                string strsql = " select * from " + dbName + ".dbo.Area a " +
                                " where 1=1 " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LArea = (from DataRow dr in dt.Rows

                            select new AreaListReturn()
                            {
                                AreaId = Convert.ToInt32(dr["id"]),
                                AreaCode = dr["AreaCode"].ToString().Trim(),
                                AreaName = dr["AreaName"].ToString().Trim(),
                                Polygon = dr["Polygon"].ToString().Trim(),

                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LArea;
        }

        public List<BranchListReturn> BranchListNoPagingCriteria(BranchInfo bInfo)
        {
            string strcond = "";

            if ((bInfo.BranchId != null) && (bInfo.BranchId != 0))
            {
                strcond += " and  b.Id =" + bInfo.BranchId;
            }

            if ((bInfo.BranchCode != null) && (bInfo.BranchCode != ""))
            {
                strcond += " and  b.BranchCode like '%" + bInfo.BranchCode + "%'";
            }
            if ((bInfo.BranchName != null) && (bInfo.BranchName != ""))
            {
                strcond += " and  b.BranchName like '%" + bInfo.BranchName + "%'";
            }

            DataTable dt = new DataTable();
            var LBranch = new List<BranchListReturn>();

            try
            {
                string strsql = " select b.* from " + dbName + ".dbo.Branch b " +
                                    " where b.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY b.Id DESC ";


                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LBranch = (from DataRow dr in dt.Rows

                             select new BranchListReturn()
                             {
                                 BranchId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                 BranchCode = dr["BranchCode"].ToString().Trim(),
                                 BranchName = dr["BranchName"].ToString().Trim(),
                                 TaxId = dr["TaxId"].ToString().Trim(),
                                 Address = dr["Address"].ToString().Trim(),
                                 SubDistrictCode = dr["SubDistrictCode"].ToString().Trim(),
                                 DistrictCode = dr["DistrictCode"].ToString().Trim(),
                                 ProvinceCode = dr["ProvinceCode"].ToString().Trim(),
                                 ZipCode = dr["ZipCode"].ToString().Trim(),
                                 ContactTel = dr["ContactTel"].ToString().Trim(),
                                 FaxNum = dr["FaxNum"].ToString().Trim(),
                                 Email = dr["Email"].ToString().Trim(),
                                 CreateDate = dr["CreateDate"].ToString().Trim(),
                                 CreateBy = dr["CreateBy"].ToString().Trim(),
                                 UpdateDate = dr["UpdateDate"].ToString().Trim(),
                                 UpdateBy = dr["UpdateBy"].ToString().Trim(),
                                 FlagDelete = dr["FlagDelete"].ToString().Trim()
                             }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LBranch;
        }


    }
}

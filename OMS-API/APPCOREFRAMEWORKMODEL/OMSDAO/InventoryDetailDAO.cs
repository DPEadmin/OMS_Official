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
    public class InventoryDetailDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int? CountInventoryDetailListByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and i.InventoryCode = '" + iInfo.InventoryCode.Trim() + "'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += " and p.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += " and p.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and p.MerchantCode like '%" + iInfo.MerchantCode.Trim() + "%'";
            }
            if ((iInfo.MerchantName != null) && (iInfo.MerchantName != ""))
            {
                strcond += " and m.MerchantName like '%" + iInfo.MerchantName.Trim() + "%'";
            }
            if (((iInfo.CreateDateFrom != "") && (iInfo.CreateDateFrom != null)) && ((iInfo.CreateDateTo != "") && (iInfo.CreateDateTo != null)))
            {
                strcond += " and  it.CreateDate BETWEEN CONVERT(VARCHAR, '" + iInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + iInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();


            try
            {
                string strsql = " SELECT count(ib.id) as countInventory FROM InventoryBalance AS ib " +
                                " LEFT OUTER JOIN Inventory AS i ON i.InventoryCode = ib.InventoryCode " +
                                " LEFT OUTER JOIN Product AS p ON p.ProductCode = ib.ProductCode AND p.FlagDelete = 'N' " +
                                " LEFT OUTER JOIN Supplier AS s ON s.SupplierCode = p.SupplierCode AND s.FlagDelete = 'N' " +
                                " WHERE (ib.FlagActive = 'Y') AND (i.FlagDelete = 'N') " + strcond;

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

        public List<InventoryListReturn> ListInventoryDetailInfoPagingByCriteria(InventoryInfo iInfo) 
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and i.InventoryCode = '" + iInfo.InventoryCode.Trim() + "'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += " and p.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += " and p.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and p.MerchantCode like '%" + iInfo.MerchantCode.Trim() + "%'";
            }
            if ((iInfo.MerchantName != null) && (iInfo.MerchantName != ""))
            {
                strcond += " and m.MerchantName like '%" + iInfo.MerchantName.Trim() + "%'";
            }
            if (((iInfo.CreateDateFrom != "") && (iInfo.CreateDateFrom != null)) && ((iInfo.CreateDateTo != "") && (iInfo.CreateDateTo != null)))
            {
                strcond += " and  it.CreateDate BETWEEN CONVERT(VARCHAR, '" + iInfo.CreateDateFrom + "', 103)  AND  DATEADD (day, DATEDIFF (day, 0, CONVERT (VARCHAR, '" + iInfo.CreateDateTo + "', 103)),'23:59:59')";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = " SELECT ib.Id AS InventoryBalanceId, ib.InventoryCode, i.InventoryName, ib.ProductCode, p.ProductName, ib.QTY, ib.Reserved, ib.Balance, ib.CreateDate, s.SupplierCode, s.SupplierName FROM InventoryBalance AS ib " + 
                                " LEFT OUTER JOIN Inventory AS i ON i.InventoryCode = ib.InventoryCode " + 
                                " LEFT OUTER JOIN Product AS p ON p.ProductCode = ib.ProductCode AND p.FlagDelete = 'N' " + 
                                " LEFT OUTER JOIN Supplier AS s ON s.SupplierCode = p.SupplierCode AND s.FlagDelete = 'N' " + 
                                " WHERE (ib.FlagActive = 'Y') AND (i.FlagDelete = 'N') " + strcond;

                       strsql += " ORDER BY ib.Id DESC OFFSET " + iInfo.rowOFFSet + " ROWS FETCH NEXT " + iInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryDetailId = (dr["InventoryBalanceId"].ToString() != "") ? Convert.ToInt32(dr["InventoryBalanceId"]) : 0,
                                  ProductCode = dr["ProductCode"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  SupplierCode = dr["SupplierCode"].ToString().Trim(),
                                  SupplierName = dr["SupplierName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  CreateDate = dr["CreateDate"].ToString()
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public int? CountListInventoryDetailByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += (strcond == "") ? " where  id.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'" : " and id.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += (strcond == "") ? " where  id.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'" : " and id.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += (strcond == "") ? " where  id.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'" : " and id.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += (strcond == "") ? " where  pd.ProductName like '%" + iInfo.ProductName.Trim() + "%'" : " and pd.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += (strcond == "") ? " where  pd.MerchantCode = '" + iInfo.MerchantCode.Trim() + "'" : " and pd.MerchantCode = '" + iInfo.MerchantCode.Trim() + "'";
            }
            if ((iInfo.MerchantName != null) && (iInfo.MerchantName != ""))
            {
                strcond += (strcond == "") ? " where  m.MerchantName like '%" + iInfo.MerchantName.Trim() + "%'" : " and m.MerchantName like '%" + iInfo.MerchantName.Trim() + "%'";
            }
            if ((iInfo.ProductCategoryCode != null) && (iInfo.ProductCategoryCode != "") && (iInfo.ProductCategoryCode != "-99"))
            {
                strcond += (strcond == "") ? " where  pd.ProductCategoryCode like '%" + iInfo.ProductCategoryCode.Trim() + "%'" : " and pd.ProductCategoryCode like '%" + iInfo.ProductCategoryCode.Trim() + "%'";
            }
            if ((iInfo.ProductBrandCode != null) && (iInfo.ProductBrandCode != "") && (iInfo.ProductBrandCode != "-99"))
            {
                strcond += (strcond == "") ? " where  pd.ProductBrandCode like '%" + iInfo.ProductBrandCode.Trim() + "%'" : " and pd.ProductBrandCode like '%" + iInfo.ProductBrandCode.Trim() + "%'";
            }
            if ((iInfo.FlagDelete != null) && (iInfo.ProductBrandCode != ""))
            {
                strcond += (strcond == "") ? " where  id.FlagDelete = '" + iInfo.FlagDelete.Trim() + "'" : " and id.FlagDelete = '" + iInfo.FlagDelete.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();


            try
            {
                string strsql = " SELECT count(id.id) as countInventoryDetail " +
                                " FROM " + dbName + ".dbo.InventoryDetail AS id " +
                                " LEFT JOIN Product AS pd ON pd.ProductCode = id.ProductCode AND pd.FlagDelete = 'N' " +
                                " LEFT JOIN ProductBrand AS pb ON pb.ProductBrandCode = pd.ProductBrandCode and pb.FlagDelete = 'N' " +
                                " LEFT JOIN ProductCategory AS pdcat ON pdcat.ProductCategoryCode = pd.ProductCategoryCode " +
                                " LEFT JOIN Supplier AS sp ON sp.SupplierCode = pd.SupplierCode " +
                                " LEFT JOIN Merchant AS m ON m.MerchantCode = pd.MerchantCode " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  countInventory = Convert.ToInt32(dr["countInventoryDetail"])
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

        public List<InventoryListReturn> ListInventoryDetailPagingByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += (strcond == "") ? " where  id.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'" : " and id.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += (strcond == "") ? " where  id.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'" : " and id.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += (strcond == "") ? " where  id.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'" : " and id.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += (strcond == "") ? " where  pd.ProductName like '%" + iInfo.ProductName.Trim() + "%'" : " and pd.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += (strcond == "") ? " where  pd.MerchantCode = '" + iInfo.MerchantCode.Trim() + "'" : " and pd.MerchantCode = '" + iInfo.MerchantCode.Trim() + "'";
            }
            if ((iInfo.MerchantName != null) && (iInfo.MerchantName != ""))
            {
                strcond += (strcond == "") ? " where  m.MerchantName like '%" + iInfo.MerchantName.Trim() + "%'" : " and m.MerchantName like '%" + iInfo.MerchantName.Trim() + "%'";
            }
            if ((iInfo.ProductCategoryCode != null) && (iInfo.ProductCategoryCode != "") && (iInfo.ProductCategoryCode != "-99"))
            {
                strcond += (strcond == "") ? " where  pd.ProductCategoryCode like '%" + iInfo.ProductCategoryCode.Trim() + "%'" : " and pd.ProductCategoryCode like '%" + iInfo.ProductCategoryCode.Trim() + "%'";
            }
            if ((iInfo.ProductBrandCode != null) && (iInfo.ProductBrandCode != "") && (iInfo.ProductBrandCode != "-99"))
            {
                strcond += (strcond == "") ? " where  pd.ProductBrandCode like '%" + iInfo.ProductBrandCode.Trim() + "%'" : " and pd.ProductBrandCode like '%" + iInfo.ProductBrandCode.Trim() + "%'";
            }
            if ((iInfo.FlagDelete != null) && (iInfo.ProductBrandCode != ""))
            {
                strcond += (strcond == "") ? " where  id.FlagDelete = '" + iInfo.FlagDelete.Trim() + "'" : " and id.FlagDelete = '" + iInfo.FlagDelete.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = " SELECT id.Id,id.UpdateDate,id.ProductCode,pd.MerchantCode,pd.ProductName,pd.ProductBrandCode,pb.ProductBrandName," +
                    "pdcat.ProductCategoryName,sp.SupplierName,m.MerchantName,id.POCode,id.SupplierCode," +
                    "id.Reserved,id.Balance,id.QTY,id.ReOrder,id.Currents,id.SafetyStock,id.price,id.pocode " +
                                " FROM " + dbName + ".dbo.InventoryDetail AS id " +
                                " LEFT JOIN Product AS pd ON pd.ProductCode = id.ProductCode AND pd.FlagDelete = 'N' " +
                                " LEFT JOIN ProductBrand AS pb ON pb.ProductBrandCode = pd.ProductBrandCode and pb.FlagDelete = 'N' " +
                                " LEFT JOIN ProductCategory AS pdcat ON pdcat.ProductCategoryCode = pd.ProductCategoryCode " +
                                " LEFT JOIN Supplier AS sp ON sp.SupplierCode = pd.SupplierCode " +
                                " LEFT JOIN Merchant AS m ON m.MerchantCode = pd.MerchantCode " + strcond;

                strsql += " ORDER BY id.Id DESC OFFSET " + iInfo.rowOFFSet + " ROWS FETCH NEXT " + iInfo.rowFetch + " ROWS ONLY";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  ProductCode = dr["ProductCode"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  
                                  SupplierName = dr["SupplierName"].ToString().Trim(),
                                  MerchantName = dr["MerchantName"].ToString().Trim(),
                                  ProductBrandCode = dr["ProductBrandCode"].ToString().Trim(),
                                  ProductBrandName = dr["ProductBrandName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  ReOrder = (dr["ReOrder"].ToString() != "") ? Convert.ToInt32(dr["ReOrder"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Current = (dr["Currents"].ToString() != "") ? Convert.ToInt32(dr["Currents"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  
                                  
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  ProductCategoryName = dr["ProductCategoryName"].ToString(),
                                  SafetyStock = (dr["SafetyStock"].ToString() != "") ? Convert.ToInt32(dr["SafetyStock"]) : 0,
                                  Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                  POCode = (dr["POCode"].ToString() != "") ? dr["POCode"].ToString() : "",
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> ListInventoryDetailInfoNoPagingByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and i.InventoryCode = '" + iInfo.InventoryCode.Trim() + "'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += " and p.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += " and p.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.ProductCodeList != null) && (iInfo.ProductCodeList != ""))
            {
                strcond += " and p.ProductCode in (" + iInfo.ProductCodeList.Trim() + ")";
            }
            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += " and i.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and i.MerchantCode like '%" + iInfo.MerchantCode.Trim() + "%'";
            }
            if ((iInfo.POCode != null) && (iInfo.POCode != ""))
            {
                strcond += " and i.MerchantCode = '" + iInfo.POCode.Trim() + "'";
            }
            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "select  i.InventoryCode, i.InventoryName,it.Id, i.MerchantCode, p.ProductCode, p.ProductName, it.QTY, it.Reserved, it.Currents, it.Balance, it.SafetyStock, it.POCode, it.UpdateDate, it.ReOrder, pb.ProductBrandName, pg.ProductCategoryName from " + dbName + ".dbo.InventoryDetail it left join" +
                                " Inventory i on it.InventoryCode = i.InventoryCode left join Product p ON p.ProductCode = it.ProductCode LEFT OUTER JOIN " +
                                " Merchant AS m ON m.MerchantCode = i.MerchantCode left join " + 
                                " Product prd on it.ProductCode = prd.ProductCode left join " +
                                " ProductBrand AS pb ON prd.ProductBrandCode = pb.ProductBrandCode left join" +
                                " ProductCategory AS pg ON prd.ProductCategoryCode = pg.ProductCategoryCode " +
                                " where it.FlagDelete ='N'  and p.ProductCode is not null  " + strcond;

                strsql += " ORDER BY it.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  POCode = dr["POCode"].ToString(),
                                  MerchantCode = dr["MerchantCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  ProductBrandName = dr["ProductBrandName"].ToString().Trim(),
                                  ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Current = (dr["Currents"].ToString() != "") ? Convert.ToInt32(dr["Currents"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  SafetyStock = (dr["SafetyStock"].ToString() != "") ? Convert.ToInt32(dr["SafetyStock"]) : 0,
                                  ReOrder = (dr["ReOrder"].ToString() != "") ? Convert.ToInt32(dr["ReOrder"]) : 0,
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> ListInventoryDetailStandardInfoNoPagingByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and i.InventoryCode = '" + iInfo.InventoryCode.Trim() + "'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += " and p.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += " and p.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.ProductCodeList != null) && (iInfo.ProductCodeList != ""))
            {
                strcond += " and p.ProductCode in (" + iInfo.ProductCodeList.Trim() + ")";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and i.MerchantCode like '%" + iInfo.MerchantCode.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "select  i.InventoryCode, i.InventoryName,it.Id, p.ProductCode, p.ProductName, it.QTY, it.Reserved, it.Balance from " + dbName + ".dbo.InventoryDetail it left join" +
                            " Inventory i on it.InventoryCode = i.InventoryCode left join Product p ON p.ProductCode = it.ProductCode" +
                            " left join PromotionDetailInfo pd ON pd.ProductCode = it.ProductCode" +
                           " where it.FlagDelete ='N' and pd.PromotionCode='STANDARD' " + strcond;

                strsql += " ORDER BY it.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryDetailListReturn> ListInventoryDetailValidateByCriteria(InventoryDetailInfo itdInfo)
        {
            string strcond = "";

            if ((itdInfo.ProductCodeValidate != null) && (itdInfo.ProductCodeValidate != ""))
            {
                strcond += " and it.ProductCode = '" + itdInfo.ProductCodeValidate.Trim() + "'";
            }
            if ((itdInfo.ProductNameValidate != null) && (itdInfo.ProductNameValidate != ""))
            {
                strcond += " and p.ProductName = '" + itdInfo.ProductNameValidate.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryDetailListReturn>();

            try
            {
                string strsql = "SELECT it.Id, p.ProductCode, p.ProductName, it.QTY, it.Reserved, it.Balance" +
                                " from " + dbName + ".dbo.Product p inner join " + dbName + ".dbo.InventoryDetail it" +
                                " on p.ProductCode = it.ProductCode" +
                                " where it.FlagDelete ='N' " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryDetailListReturn()
                              {
                                  ProductId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  ProductCode = dr["ProductCode"].ToString().Trim(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryListReturn> ListInventoryDetailGetFromTakeOrderRetail(InventoryInfo itdInfo)
        {
            string strcond = "";

            if ((itdInfo.InventoryCode != null) && (itdInfo.InventoryCode != ""))
            {
                strcond += " and it.InventoryCode = '" + itdInfo.InventoryCode.Trim() + "'";
            }
            if ((itdInfo.ProductCode != null) && (itdInfo.ProductCode != ""))
            {
                strcond += " and it.ProductCode = '" + itdInfo.ProductCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "SELECT it.Id, it.InventoryCode, it.ProductCode, it.QTY, it.Reserved, it.Currents, it.Balance" +
                                " from " + dbName + ".dbo.InventoryDetail it " +
                                " where it.FlagDelete ='N' " +
                                strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  ProductCode = dr["ProductCode"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Current = (dr["Currents"].ToString() != "") ? Convert.ToInt32(dr["Currents"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public int InsertInventoryDetail(InventoryDetailInfo iInfo)
        {
            int i = 0;

            string strsql = "insert into InventoryDetail (InventoryCode, ProductCode, Currents, Balance , QTY, Reserved, SafetyStock, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete,SupplierCode, POCode ) values (" +
                             "'" + iInfo.InventoryCode + "', " +
                             "'" + iInfo.ProductCode + "', " +
                             "'" + iInfo.Current + "', " +
                             "'" + iInfo.Balance + "', " +
                                   iInfo.QTY + ", " +
                                 +iInfo.Reserved + ", " +
                                 +iInfo.SafetyStock + ", " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.UpdateBy + "', " +
                             "'" + iInfo.FlagDelete + "'," +
                             "'" + iInfo.SupplierCode + "'," +
                             "'" + iInfo.POCode + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertInventoryDetailfromTakeOrderRetail(InventoryDetailInfo iInfo)
        {
            int i = 0;

            string strsql = "insert into InventoryDetail (InventoryCode, ProductCode, Currents, Balance, QTY, Reserved, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete) values (" +
                             "'" + iInfo.InventoryCode + "', " +
                             "'" + iInfo.ProductCode + "', " +
                                   iInfo.Current + ", " +
                                   iInfo.Balance + ", " +
                                   iInfo.QTY + ", " +
                                   iInfo.Reserved + ", " +
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

        public int InsertInventoryMapPO(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = "insert into " + dbName + ".dbo.InventoryMapPO (InventoryCode, POCode, CreateDate, CreateBy, UpdateDate, UpdateBy) values (" +
                             "'" + iInfo.InventoryCode + "', " +
                             "'" + iInfo.POCode + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.UpdateBy + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertInventoryDetailfromImport(List<inventorydetaillistData> iInfo)
        {
            List<String> lSQL = new List<string>();
            string strsql = "";
            int i = 0;
            var reserved = 0;

            foreach (var info in iInfo.ToList())
            {
                strsql = "insert into InventoryDetail (InventoryCode, ProductCode,Balance , QTY, Reserved, CreateDate, CreateBy, UpdateDate, UpdateBy, FlagDelete) values (" +
                             "'" + info.InventoryCode + "', " +
                             "'" + info.ProductCode + "', " +
                                   (info.QTY - reserved) + ", " +
                                   info.QTY + ", " +
                                   reserved + ", " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + info.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + info.UpdateBy + "', " +
                             "'" + info.FlagDelete + "')";
                lSQL.Add(strsql);
            }

            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            foreach (string strq in lSQL)
            {
                com.CommandText = strq;
                com.CommandType = System.Data.CommandType.Text;
                i = db.ExcuteBeginTransectionText(com);
            }
            return i;
        }

        public int DeleteInventoryDetail(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                         " FlagDelete = 'Y'" +

                         " where Id in(" + iInfo.InventoryDetailId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInventoryDetail(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                            " ProductCode = '" + iInfo.ProductCode + "'," +
                            " Currents = " + iInfo.Current + "," +
                            " Balance = " + iInfo.Balance + "," +
                            " QTY = " + iInfo.QTY + "," +
                            " Reserved = " + iInfo.Reserved + "," +
                            " SafetyStock = " + iInfo.SafetyStock + "," +
                            " POCode = '" + iInfo.POCode + "'," +
                            " SupplierCode = '" + iInfo.SupplierCode + "'," +

                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'" +
                            " where Id =" + iInfo.InventoryDetailId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInventoryDetailfromTakeOrderRetail(InventoryDetailInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                            " Balance = " + iInfo.Balance + "," +
                            " Currents = " + iInfo.Current + "," +
                            " QTY = " + iInfo.QTY + "," +
                            " Reserved = " + iInfo.Reserved + "," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'" +
                            " where (InventoryCode = '" + iInfo.InventoryCode + "') and (ProductCode = '" + iInfo.ProductCode + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateInventoryDetailfromEcommerce(InventoryInfo iInfo)
        {
            int i = 0;
            string strsql = "";
            List<InventoryListReturn> liInfo = new List<InventoryListReturn>();
            liInfo = ListInventoryDetailGetFromTakeOrderRetail(iInfo);
            if (liInfo.Count > 0)
            {
                int? reserved = iInfo.QTY + liInfo[0].Reserved;
                int? current = liInfo[0].QTY - reserved;
                int? balance = liInfo[0].Balance;

                strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                 " Balance = " + balance + "," +
                 " Currents = " + current + "," +
                 " Reserved = " + reserved + "," +
                 " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                 " UpdateBy = '" + iInfo.UpdateBy + "'" +
                 " where (InventoryCode = '" + iInfo.InventoryCode + "') and (ProductCode = '" + iInfo.ProductCode + "')";
            }




            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<InventoryDetailListReturn> ListProductShowAll_InventoryDetail(InventoryDetailInfo itInfo)
        {
            string strcond = "";

            if ((itInfo.InventoryCode != "") && (itInfo.InventoryCode != null))
            {
                strcond += " and it.InventoryCode = '" + itInfo.InventoryCode.Trim() + "'";
            }
            if ((itInfo.InventoryName != null) && (itInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + itInfo.InventoryName.Trim() + "%'";
            }
            if ((itInfo.ProductCode != null) && (itInfo.ProductCode != ""))
            {
                strcond += " and it.ProductCode like '%" + itInfo.ProductCode.Trim() + "%'";
            }
            if ((itInfo.ProductName != null) && (itInfo.ProductName != ""))
            {
                strcond += " and p.ProductName like '%" + itInfo.ProductName.Trim() + "%'";
            }
            DataTable dt = new DataTable();
            var LInventory = new List<InventoryDetailListReturn>();

            try
            {
                string strsql = "select it.ProductCode, p.ProductName from " + dbName + ".dbo.InventoryDetail it left join" +
                                " Inventory i on it.InventoryCode = i.InventoryCode left join Product p ON p.ProductCode = it.ProductCode" +
                                " where it.FlagDelete ='N' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryDetailListReturn()
                              {
                                  ProductCode = dr["ProductCode"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryDetailListReturn> ListInvenDetailInfoProductNoPagingByCriteria(InventoryDetailInfo iInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryDetailListReturn>();

            try
            {
                string strsql = "SELECT     id.InventoryCode, P.ProductCode, id.ProductCode AS IDPdcode, id.QTY, P.ProductName, id.Reserved, id.Balance FROM " + dbName + ".dbo.Product AS P " + 
                                " LEFT OUTER JOIN " + dbName + ".dbo.InventoryDetail AS id ON P.ProductCode = id.ProductCode " + 
                                " AND id.InventoryCode = '" + iInfo.InventoryCode + "' " + 
                                " WHERE (P.ProductCode IN(" + iInfo.ProductCode + "))";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryDetailListReturn()
                              {
                                  //InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  ProductCode = dr["ProductCode"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryDetailListReturn> ListInvenDetailExactNoPagingByCriteria(InventoryDetailInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and it.InventoryCode = '" + iInfo.InventoryCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryDetailListReturn>();

            try
            {
                string strsql = "select it.* from " + dbName + ".dbo.InventoryDetail it " +
                                " where it.FlagDelete ='N' " + strcond;

                strsql += " ORDER BY it.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryDetailListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryDetailListReturn> GetInventoryCodeList(String inventoryCode, String productCodeList)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryDetailListReturn>();

            try
            {
                string strsql = "select it.* from " + dbName + ".dbo.InventoryDetail it " +
                                " where (it.FlagDelete ='N') and (it.InventoryCode = '" + inventoryCode + "') and (it.ProductCode in (" + productCodeList + "))";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryDetailListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  ProductCode = dr["ProductCode"].ToString(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  CreateDate = dr["CreateDate"].ToString()
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryBalanceListReturn> ListInvenBalanceExactNoPagingByCriteria(InventoryDetailInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and it.InventoryCode = '" + iInfo.InventoryCode.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryBalanceListReturn>();

            try
            {
                string strsql = "select it.* from " + dbName + ".dbo.InventoryBalance it " +
                                " where it.FlagActive ='Y' " + strcond;

                strsql += " ORDER BY it.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryBalanceListReturn()
                              {
                                  InventoryBalanceId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryBalanceListReturn> ListInvenBalanceProductNoPagingByCriteria(InventoryDetailInfo iInfo)
        {
            string strcond = "";

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryBalanceListReturn>();

            try
            {
                string strsql = "SELECT     id.InventoryCode, P.ProductCode, id.ProductCode AS IDPdcode, id.QTY, P.ProductName, id.Reserved, id.Balance FROM " + dbName + ".dbo.Product AS P " +
                                " LEFT OUTER JOIN " + dbName + ".dbo.InventoryBalance AS id ON P.ProductCode = id.ProductCode " +
                                " AND id.InventoryCode = '" + iInfo.InventoryCode + "' " +
                                " WHERE (P.ProductCode IN(" + iInfo.ProductCode + "))";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryBalanceListReturn()
                              {
                                  
                                  ProductCode = dr["ProductCode"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public int InsertInventoryDetailfromUploadInvDetail(InventoryDetailInfo iInfo)
        {
            int i = 0;

            string strsql = "insert into InventoryDetail (InventoryCode, ProductCode, Currents, Balance, QTY, Reserved, SafetyStock, ReOrder, CreateDate, CreateBy, UpdateDate, UpdateBy,Price,Pocode, FlagDelete) values (" +
                             "'" + iInfo.InventoryCode + "', " +
                             "'" + iInfo.ProductCode + "', " +
                                   iInfo.Current + ", " +
                                   iInfo.Balance + ", " +
                                   iInfo.QTY + ", " +
                                   iInfo.Reserved + ", " +
                                   iInfo.SafetyStock + ", " +
                                   iInfo.ReOrder + ", " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.CreateBy + "', " +
                             "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "', " +
                             "'" + iInfo.UpdateBy + "', " +
                              "'" + iInfo.Price + "', " +
                              "'" + iInfo.POCode + "', " +
                             "'" + iInfo.FlagDelete + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInventoryDetailfromUploadFile(InventoryInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                            " ProductCode = '" + iInfo.ProductCode + "'," +
                            " Currents = " + iInfo.Current + "," +
                            " Balance = " + iInfo.Balance + "," +
                            " QTY = " + iInfo.QTY + "," +
                            " Reserved = " + iInfo.Reserved + "," +
                            " SafetyStock = " + iInfo.SafetyStock + "," +
                            " ReOrder = " + iInfo.ReOrder + "," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'," +
                             " Price = '" + iInfo.Price + "'" +
                            " where Id =" + iInfo.InventoryDetailId;


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public List<InventoryListReturn> ListInventoryDetailInfoNoPagingExportExcelByCriteria(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and i.InventoryCode = '" + iInfo.InventoryCode.Trim() + "'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += " and p.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += " and p.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.ProductCodeList != null) && (iInfo.ProductCodeList != ""))
            {
                strcond += " and p.ProductCode in (" + iInfo.ProductCodeList.Trim() + ")";
            }
            if ((iInfo.InventoryCode != null) && (iInfo.InventoryCode != ""))
            {
                strcond += " and i.InventoryCode like '%" + iInfo.InventoryCode.Trim() + "%'";
            }
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and i.MerchantCode like '%" + iInfo.MerchantCode.Trim() + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "select  i.InventoryCode, i.InventoryName,it.Id, i.MerchantCode,m.MerchantName, p.ProductCode, p.ProductName, it.QTY, it.Reserved, it.Currents, it.Balance, it.SafetyStock, it.POCode, it.UpdateDate, it.ReOrder, pb.ProductBrandName, pg.ProductCategoryName from " + dbName + ".dbo.InventoryDetail it left join" +
                                " Inventory i on it.InventoryCode = i.InventoryCode left join Product p ON p.ProductCode = it.ProductCode LEFT OUTER JOIN " +
                                " Merchant AS m ON m.MerchantCode = i.MerchantCode left join " +
                                " Product prd on it.ProductCode = prd.ProductCode left join " +
                                " ProductBrand AS pb ON prd.ProductBrandCode = pb.ProductBrandCode left join " +
                                " ProductCategory AS pg ON prd.ProductCategoryCode = pg.ProductCategoryCode " +
                                " where it.FlagDelete ='N' and prd.MerchantCode = '" + iInfo.MerchantCode.Trim() + "' " + strcond;

                strsql += " ORDER BY it.Id DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  POCode = dr["POCode"].ToString(),
                                  MerchantCode = dr["MerchantCode"].ToString(),
                                  MerchantName = dr["MerchantName"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  ProductBrandName = dr["ProductBrandName"].ToString().Trim(),
                                  ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Current = (dr["Currents"].ToString() != "") ? Convert.ToInt32(dr["Currents"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  SafetyStock = (dr["SafetyStock"].ToString() != "") ? Convert.ToInt32(dr["SafetyStock"]) : 0,
                                  ReOrder = (dr["ReOrder"].ToString() != "") ? Convert.ToInt32(dr["ReOrder"]) : 0,
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }
        public List<InventoryListReturn> ListInventoryDetailInfoNoPagingByCriteriaMoveStock(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.InventoryCode != "") && (iInfo.InventoryCode != null))
            {
                strcond += " and it.InventoryCode = '" + iInfo.InventoryCode + "'";
            }
            if ((iInfo.InventoryName != null) && (iInfo.InventoryName != ""))
            {
                strcond += " and i.InventoryName like '%" + iInfo.InventoryName.Trim() + "%'";
            }
            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += " and it.ProductCode like '%" + iInfo.ProductCode.Trim() + "%'";
            }
            if ((iInfo.ProductName != null) && (iInfo.ProductName != ""))
            {
                strcond += " and p.ProductName like '%" + iInfo.ProductName.Trim() + "%'";
            }
            if ((iInfo.ProductCodeList != null) && (iInfo.ProductCodeList != ""))
            {
                strcond += " and p.ProductCode in (" + iInfo.ProductCodeList.Trim() + ")";
            }
      
            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and p.MerchantCode like '%" + iInfo.MerchantCode + "%'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryListReturn>();

            try
            {
                string strsql = "SELECT it.Id, p.MerchantCode, it.InventoryCode, i.InventoryName, it.ProductCode, p.ProductName, it.QTY, it.Reserved, it.Currents, it.Balance, it.SafetyStock, it.POCode, it.UpdateDate, it.ReOrder, pb.ProductBrandName, pg.ProductCategoryName " + 
                                " FROM InventoryDetail AS it LEFT OUTER JOIN " + 
                                " Inventory AS i ON i.InventoryCode = it.InventoryCode AND i.FlagDelete = 'N' LEFT OUTER JOIN " + 
                                " Product AS p ON p.ProductCode = it.ProductCode AND p.FlagDelete = 'N' LEFT OUTER JOIN " + 
                                " ProductBrand AS pb ON pb.ProductBrandCode = p.ProductBrandCode AND pb.FlagDelete = 'N' LEFT OUTER JOIN " + 
                                " ProductCategory AS pg ON pg.ProductCategoryCode = p.ProductCategoryCode LEFT OUTER JOIN " + 
                                " Supplier AS sp ON sp.SupplierCode = p.SupplierCode LEFT OUTER JOIN " + 
                                " Merchant AS m ON m.MerchantCode = p.MerchantCode " +
                                " Where  it.FlagDelete = 'N' " + strcond;

                strsql += " ORDER BY it.ProductCode DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryListReturn()
                              {
                                  InventoryDetailId = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  POCode = dr["POCode"].ToString(),
                                  MerchantCode = dr["MerchantCode"].ToString(),
                                  InventoryName = dr["InventoryName"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  UpdateDate = dr["UpdateDate"].ToString(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  ProductBrandName = dr["ProductBrandName"].ToString().Trim(),
                                  ProductCategoryName = dr["ProductCategoryName"].ToString().Trim(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Current = (dr["Currents"].ToString() != "") ? Convert.ToInt32(dr["Currents"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                                  SafetyStock = (dr["SafetyStock"].ToString() != "") ? Convert.ToInt32(dr["SafetyStock"]) : 0,
                                  ReOrder = (dr["ReOrder"].ToString() != "") ? Convert.ToInt32(dr["ReOrder"]) : 0,
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }
        public int UpdateInventoryMove(InventoryListReturn oInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                      " QTY = QTY+"+oInfo.QTY+"" +


                      " where InventoryCode ='" + oInfo.InventoryCodeTo + "'" +
                      " and productcode ='"+oInfo.ProductCode+"'" +
                     
                      "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }
        public int UpdateInventoryMoveDelete(InventoryListReturn oInfo)
        {
            int i = 0;
            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                      " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                      " QTY = QTY-" + oInfo.QTY + "" +


                      " where InventoryCode ='" + oInfo.InventoryCode + "'" +
                      " and productcode ='" + oInfo.ProductCode + "'" +

                      "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInventoryDetailforMarketPlace(InventoryDetailInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                            " Balance = " + iInfo.Balance + "," +
                            " Currents = " + iInfo.Current + "," +
                            " QTY = " + iInfo.QTY + "," +
                            " Reserved = " + iInfo.Reserved + "," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'" +
                            " where (InventoryCode = '" + iInfo.InventoryCode + "') and (ProductCode = '" + iInfo.ProductCode + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateInvenDetailfromOrderChangeStatus(InventoryDetailInfo iInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.InventoryDetail set " +

                            " Balance = " + iInfo.Balance + "," +
                            " Reserved = " + iInfo.Reserved + "," +
                            " QTY = " + iInfo.QTY + "," +
                            " UpdateDate = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                            " UpdateBy = '" + iInfo.UpdateBy + "'" +
                            " where (InventoryCode = '" + iInfo.InventoryCode + "') and (ProductCode = '" + iInfo.ProductCode + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }






        public List<InventoryEcommerceReturn> findInventory_NearbyECommerce(InventoryInfo iInfo)
        {
            string strcond = "";

            if ((iInfo.ProductCode != null) && (iInfo.ProductCode != ""))
            {
                strcond += " and d.ProductCode in (" + iInfo.ProductCode.Trim() + ")";
            }

            if ((iInfo.MerchantCode != null) && (iInfo.MerchantCode != ""))
            {
                strcond += " and i.MerchantCode = '" + iInfo.MerchantCode.Trim() + "'";
            }

            if ((iInfo.Province != null) && (iInfo.Province != ""))
            {
                strcond += " and i.Province = '" + iInfo.Province.Trim() + "'";
            }

            DataTable dt = new DataTable();
            var LInventory = new List<InventoryEcommerceReturn>();

            try
            {
                string strsql = "SELECT i.InventoryCode, i.Province, d.ProductCode, d.QTY, d.Reserved, d.Currents, d.Balance from " + dbName + ".dbo.Inventory AS i " +
                                " left join " + dbName + ".dbo.InventoryDetail AS d ON d.InventoryCode = i.InventoryCode " +
                                " where (d.Balance > 0) " + strcond;

                strsql += " ORDER BY d.ProductCode DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryEcommerceReturn()
                              {
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  Province = dr["Province"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Current = (dr["Currents"].ToString() != "") ? Convert.ToInt32(dr["Currents"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LInventory;
        }

        public List<InventoryEcommerceReturn> GetNearestInventory(InventoryInfo iInfo)
        {
            DataTable dt = new DataTable();
            var LInventory = new List<InventoryEcommerceReturn>();

            try
            {               
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "FindInventoryNearbyEcommerce";

                SqlParameter param1 = new SqlParameter("MerchantCode", iInfo.MerchantCode);
                command.Parameters.Add(param1);
                SqlParameter param2 = new SqlParameter("Province", iInfo.Province);
                command.Parameters.Add(param2);
                SqlParameter param3 = new SqlParameter("ProductCode", iInfo.ProductCode);
                command.Parameters.Add(param3);
                SqlParameter param4 = new SqlParameter("CountProductCodeInitial", iInfo.CountProductCodeInitial);
                command.Parameters.Add(param4);

                dt = db.ExcuteBeginStoredProcedureText(command);

                LInventory = (from DataRow dr in dt.Rows

                              select new InventoryEcommerceReturn()
                              {
                                  InventoryCode = dr["InventoryCode"].ToString(),
                                  Province = dr["Province"].ToString(),
                                  ProductCode = dr["ProductCode"].ToString(),
                                  QTY = (dr["QTY"].ToString() != "") ? Convert.ToInt32(dr["QTY"]) : 0,
                                  Reserved = (dr["Reserved"].ToString() != "") ? Convert.ToInt32(dr["Reserved"]) : 0,
                                  Current = (dr["Currents"].ToString() != "") ? Convert.ToInt32(dr["Currents"]) : 0,
                                  Balance = (dr["Balance"].ToString() != "") ? Convert.ToInt32(dr["Balance"]) : 0,
                              }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return LInventory;
        }

    }
}

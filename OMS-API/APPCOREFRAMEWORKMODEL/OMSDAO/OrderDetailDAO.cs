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
    public class OrderDetailDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();
        public List<OrderDetailListReturn> ListOrderPaymentMapCustomerNopagingByCriteria(OrderDetailInfo odInfo)
        {
            string strcond = "";

            if ((odInfo.OrderCode != null) && (odInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + odInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + odInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderDetail = new List<OrderDetailListReturn>();

            try
            {
                string strsql =
                    " SELECT o.OrderCode,cus.CustomerFName,cus.CustomerLName,cus.CustomerCode,o.TransportPrice " +
                    ",op.Installment,op.InstallmentPrice,op.FirstInstallment,luci.LookupValue as CardIssuename,op.CardNo " +
                    ",luct.LookupValue as CardType,op.CVCNo,op.CardHolderName,op.CardExpMonth,op.CardExpYear,op.CitizenId,op.BirthDate " +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT JOIN Customer AS cus ON cus.CustomerCode = o.CustomerCode " +
                    " inner join OrderPayment as op on op.OrderCode = o.OrderCode " +
                    " left join Lookup AS luci ON op.CardIssuename = luci.LookupCode and luci.LookupType = 'BANK' " +
                    " left join Lookup AS luct ON op.CardIssuename = luct.LookupCode and luct.LookupType = 'CARDTYPE' " +
                    strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderDetail = (from DataRow dr in dt.Rows
                                select new OrderDetailListReturn()
                                {
                                    OrderCode = dr["OrderCode"].ToString(),
                                    CustomerCode = dr["CustomerCode"].ToString(),
                                    CustomerFName = dr["CustomerFName"].ToString(),
                                    CustomerLName = dr["CustomerLName"].ToString(),
                                    CustomerName = dr["CustomerFName"].ToString() + " " + dr["CustomerLName"].ToString(),
                                    TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToInt32(dr["TransportPrice"]) : 0,

                                    Installment = (dr["Installment"].ToString().Trim() != "-99") ? dr["Installment"].ToString().Trim() : "",
                                    InstallmentPrice = dr["InstallmentPrice"].ToString().Trim(),
                                    FirstInstallment = dr["FirstInstallment"].ToString().Trim(),
                                    CardIssuename = dr["CardIssuename"].ToString().Trim(),
                                    CardNo = dr["CardNo"].ToString().Trim(),
                                    CardType = dr["CardType"].ToString().Trim(),
                                    CVCNo = dr["CVCNo"].ToString().Trim(),
                                    CardHolderName = dr["CardHolderName"].ToString().Trim(),
                                    CardExpMonth = dr["CardExpMonth"].ToString().Trim(),
                                    CardExpYear = dr["CardExpMonth"].ToString().Trim()+"/"+dr["CardExpYear"].ToString().Trim(),
                                    CitizenId = dr["CitizenId"].ToString().Trim(),
                                    BirthDate = dr["BirthDate"].ToString().Trim(),
                                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderDetail;
        }
        public List<OrderDetailListReturn> ListOrderMapCustomerNopagingByCriteria(OrderDetailInfo odInfo)
        {
            string strcond = "";

            if ((odInfo.OrderCode != null) && (odInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + odInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + odInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderDetail = new List<OrderDetailListReturn>();

            try
            {
                string strsql =
                    " SELECT o.OrderCode,cus.CustomerFName,cus.CustomerLName,cus.CustomerCode,o.TransportPrice " +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT JOIN Customer AS cus ON cus.CustomerCode = o.CustomerCode " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderDetail = (from DataRow dr in dt.Rows
                                select new OrderDetailListReturn()
                                {
                                    OrderCode = dr["OrderCode"].ToString(),
                                    CustomerCode = dr["CustomerCode"].ToString(),
                                    CustomerFName = dr["CustomerFName"].ToString(),
                                    CustomerLName = dr["CustomerLName"].ToString(),
                                    CustomerName = dr["CustomerFName"].ToString() + " " + dr["CustomerLName"].ToString(),
                                    TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToInt32(dr["TransportPrice"]) : 0,
                                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderDetail;
        }

        public List<OrderDetailListReturn> ListGetCustomerPhoneNopagingByCriteria(OrderDetailInfo odInfo)
        {
            string strcond = "";

            if ((odInfo.CustomerCode != null) && (odInfo.CustomerCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  cp.CustomerCode like '%" + odInfo.CustomerCode + "%'" : strcond += " AND  cp.CustomerCode like '%" + odInfo.CustomerCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderDetail = new List<OrderDetailListReturn>();

            try
            {
                string strsql =
                    " SELECT cp.CustomerCode,cp.PhoneNumber " +
                    " FROM " + dbName + ".dbo.CustomerPhone AS cp " + strcond  +
                    "  order by cp.UpdateDate desc ";
                   

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderDetail = (from DataRow dr in dt.Rows
                                select new OrderDetailListReturn()
                                {
                                    CustomerCode = dr["CustomerCode"].ToString(),
                                    Phonenumber = dr["Phonenumber"].ToString(),
                                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderDetail;
        }

        public List<CustomerAddressListReturn> ListCustomerAddressDetailByCriteria(CustomerAddressInfo caInfo)
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
                                " where  c.FlagActive = 'Y'" + strcond;

                strsql += " order by c.UpdateDate ";

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
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerAddress;
        }

        public int? CountOrderDetailMapProductByCriteria(OrderDetailInfo odInfo)
        {
            string strcond = "";
            int? count = 0;

            if ((odInfo.OrderCode != null) && (odInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  od.OrderCode like '%" + odInfo.OrderCode + "%'" : strcond += " AND  od.OrderCode like '%" + odInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LVehicle = new List<OrderDetailListReturn>();


            try
            {
                string strsql =
                    " SELECT count(od.Id) as countOrder " +
                    " FROM " + dbName + ".dbo.orderDetail AS od " +
                    " LEFT JOIN Product AS pd ON pd.ProductCode = od.ProductCode " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LVehicle = (from DataRow dr in dt.Rows

                            select new OrderDetailListReturn()
                            {
                                countOrder = Convert.ToInt32(dr["countOrder"])
                            }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LVehicle.Count > 0)
            {
                count = LVehicle[0].countOrder;
            }

            return count;
        }

        public List<OrderDetailListReturn> ListOrderDetailMapProductNopagingByCriteria(OrderDetailInfo odInfo)
        {
            string strcond = "";

            if ((odInfo.OrderCode != null) && (odInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE od.OrderCode like '%" + odInfo.OrderCode + "%'" : strcond += "and  od.OrderCode like '%" + odInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderDetail = new List<OrderDetailListReturn>();

            try
            {
                string strsql =
                    " SELECT pd.productcode,pd.ProductName,od.Amount,od.Price,od.NetPrice,od.TotalPrice, od.InventoryCode, od.Promotioncode " +
                    " FROM " + dbName + ".dbo.orderDetail AS od " +
                    " LEFT JOIN Product AS pd ON pd.ProductCode = od.ProductCode  " + strcond + " AND  pd.productcode is not null ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderDetail = (from DataRow dr in dt.Rows
                                select new OrderDetailListReturn()
                                {
                                    ProductCode = dr["Productcode"].ToString(),
                                    ProductName = dr["ProductName"].ToString(),
                                    InventoryCode = dr["InventoryCode"].ToString(),
                                    PromotionCode = dr["PromotionCode"].ToString(),
                                    Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                    Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                    NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                                    TotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDouble(dr["TotalPrice"]) : 0,
                                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderDetail;
        }

        public List<OrderDetailListReturn> sumOrderDetail(OrderDetailInfo odInfo)
        {
            string strcond = "";

            if ((odInfo.OrderCode != null) && (odInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  o.OrderCode like '%" + odInfo.OrderCode + "%'" : strcond += " AND  o.OrderCode like '%" + odInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderDetail = new List<OrderDetailListReturn>();

            try
            {
                string strsql =
                    " SELECT SUM(od.Price) AS sumTotalPrice,SUM(od.Vat) AS sumVat " +
                    " FROM " + dbName + ".dbo.OrderInfo AS o " +
                    " LEFT JOIN OrderDetail AS od ON od.OrderCode = o.OrderCode " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderDetail = (from DataRow dr in dt.Rows
                                select new OrderDetailListReturn()
                                {
                                    sumTotalPrice = (dr["sumTotalPrice"].ToString() != "") ? Convert.ToDouble(dr["sumTotalPrice"]) : 0,
                                    sumVat = (dr["sumVat"].ToString() != "") ? Convert.ToDouble(dr["sumVat"]) : 0,

                                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderDetail;
        }
        public List<CustomerAddressListReturn> ListCustomerAddressDetailOrderByCriteria(CustomerAddressInfo caInfo)
        {
            string strcond = "";

         
            if ((caInfo.CustomerCode != null) && (caInfo.CustomerCode != ""))
            {
                strcond += "and o.CustomerCode = '" + caInfo.CustomerCode + "'";
            }

            if ((caInfo.AddressType != null) && (caInfo.AddressType != ""))
            {
                strcond += "and o.AddressType = '" + caInfo.AddressType + "'";
            }
            if ((caInfo.Ordercode != null) && (caInfo.Ordercode != ""))
            {
                strcond += "and o.Ordercode = '" + caInfo.Ordercode + "'";
            }
            DataTable dt = new DataTable();
            var LCustomerAddress = new List<CustomerAddressListReturn>();

            try
            {
                string strsql = " select o.Id, o.address, o.subdistrict, o.district, o.province, o.zipcode, o.CustomerCode, o.CreateDate,o.CreateBy, " +
                    "o.UpdateDate, o.UpdateBy,o.AddressType,  p.ProvinceName, s.SubDistrictName, d.DistrictName" +
                    " from " + dbName + ".dbo.OrderTransport o" +
                                " left join Province p on o.province = p.ProvinceCode" +
                                " left join District d on o.district = d.DistrictCode" +
                                " left join SubDistrict s on o.SubDistrict = s.SubDistrictCode" +
                                  " left outer join orderinfo oi on o.OrderCode = oi.OrderCode " +
                                " where  o.ordercode is not null " + strcond;

                strsql += " order by o.UpdateDate ";

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

                                       
                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCustomerAddress;
        }

        public List<orderdataInfo> ListOrderDetailNopagingbyOrderCode(orderdataInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += "and o.Ordercode = '" + oInfo.OrderCode + "'";
            }
            DataTable dt = new DataTable();
            var LOrderdata = new List<orderdataInfo>();

            try
            {
                string strsql = " select o.*, p.ProductName,ch.ChannelName, u.LookupValue AS UnitName " +
                                " from " + dbName + ".dbo.OrderDetail o" +
                                " left join " + dbName + ".dbo.Lookup AS u ON u.LookupCode = o.Unit AND u.LookupType = 'UNIT'" +
                                " left join " + dbName + ".dbo.Product AS p ON p.ProductCode = o.ProductCode" +
                                " left join " + dbName + ".dbo.Channel AS ch ON ch.ChannelCode = o.ChannelCode " +
                                " where 1=1 " + strcond;

                strsql += " order by o.UpdateDate ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderdata = (from DataRow dr in dt.Rows

                                    select new orderdataInfo()
                                    {
                                        OrderDetailID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                        FlagCombo = dr["FlagCombo"].ToString().Trim(),
                                        ProductCode = dr["ProductCode"].ToString().Trim(),
                                        ProductName = dr["ProductName"].ToString().Trim(),
                                        Unit = dr["Unit"].ToString().Trim(),
                                        UnitName = dr["UnitName"].ToString().Trim(),
                                        Price = (dr["Price"].ToString() != "") ? Convert.ToInt32(dr["Price"]) : 0,
                                        ProductPrice = (dr["ProductPrice"].ToString() != "") ? Convert.ToInt32(dr["ProductPrice"]) : 0,
                                        Amount = dr["Amount"].ToString().Trim(),
                                        DefaultAmount = dr["DefaultAmount"].ToString().Trim(),
                                        DiscountAmount = dr["DiscountAmount"].ToString().Trim(),
                                        DiscountPercent = dr["DiscountPercent"].ToString(),
                                        CampaignCode = dr["CampaignCode"].ToString(),
                                        PromotionCode = dr["PromotionCode"].ToString(),
                                        TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToInt32(dr["TransportPrice"]) : 0,
                                        SumPrice = (dr["SumPrice"].ToString() != "") ? Convert.ToInt32(dr["SumPrice"]) : 0,
                                        PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),
                                        ComboCode = dr["ComboCode"].ToString().Trim(),
                                        ComboName = dr["ComboName"].ToString().Trim(),
                                        runningNo = dr["RunningNo"].ToString().Trim(),
                                        LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                        LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                        NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToInt32(dr["NetPrice"]) : 0,
                                        Vat = (dr["Vat"].ToString() != "") ? Convert.ToInt32(dr["Vat"]) : 0,
                                        ParentProductCode = dr["ParentProductCode"].ToString().Trim(),
                                        ParentPromotionCode = dr["ParentPromotionCode"].ToString().Trim(),
                                        FreeShipping = dr["FreeShipping"].ToString().Trim(),
                                        FlagProSetHeader = dr["FlagProSetHeader"].ToString().Trim(),
                                        PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                        PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                        MOQFlag = dr["MOQFlag"].ToString().Trim(),
                                        MinimumQty = (dr["MinimumQty"].ToString() != "") ? Convert.ToInt32(dr["MinimumQty"]) : 0,
                                        ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountPercent"]) : 0,
                                        ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountAmount"]) : 0,
                                        FreeShippingBill = dr["FreeShippingBill"].ToString().Trim(),
                                        TradeFlag = dr["TradeFlag"].ToString().Trim(),
                                        InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                        CamcatCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                        ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                        ChannelName = dr["ChannelName"].ToString().Trim(),
                                        FlagMediaPlan = dr["FlagMediaPlan"].ToString().Trim(),

                                    }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderdata;
        }

        public List<orderdataInfo> ListOrderDetailNopagingbyOrderCode2(orderdataInfo oInfo)
        {
            string strcond = "";

            if ((oInfo.OrderCode != null) && (oInfo.OrderCode != ""))
            {
                strcond += "and o.Ordercode = '" + oInfo.OrderCode + "'";
            }
            DataTable dt = new DataTable();
            var LOrderdata = new List<orderdataInfo>();

            try
            {
                string strsql = " select o.*, p.ProductName,ch.ChannelName, u.LookupValue AS UnitName " +
                                " from " + dbName + ".dbo.OrderDetail o" +
                                " left join " + dbName + ".dbo.Lookup AS u ON u.LookupCode = o.Unit AND u.LookupType = 'UNIT'" +
                                " left join " + dbName + ".dbo.Product AS p ON p.ProductCode = o.ProductCode" +
                                " left join " + dbName + ".dbo.Channel AS ch ON ch.ChannelCode = o.ChannelCode " +
                                " where 1=1 " + strcond;

                strsql += " order by o.UpdateDate ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderdata = (from DataRow dr in dt.Rows

                              select new orderdataInfo()
                              {
                                  OrderDetailID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                                  FlagCombo = dr["FlagCombo"].ToString().Trim(),
                                  ProductCode = dr["ProductCode"].ToString().Trim(),
                                  ProductName = dr["ProductName"].ToString().Trim(),
                                  Unit = dr["Unit"].ToString().Trim(),
                                  UnitName = dr["UnitName"].ToString().Trim(),
                                  Price = (dr["ProductPrice"].ToString() != "") ? Convert.ToInt32(dr["ProductPrice"]) : 0,
                                  ProductPrice = (dr["ProductPrice"].ToString() != "") ? Convert.ToInt32(dr["ProductPrice"]) : 0,
                                  Amount = dr["Amount"].ToString().Trim(),
                                  DefaultAmount = dr["DefaultAmount"].ToString().Trim(),
                                  DiscountAmount = dr["DiscountAmount"].ToString().Trim(),
                                  DiscountPercent = dr["DiscountPercent"].ToString(),
                                  CampaignCode = dr["CampaignCode"].ToString(),
                                  PromotionCode = dr["PromotionCode"].ToString(),
                                  TransportPrice = (dr["TransportPrice"].ToString() != "") ? Convert.ToInt32(dr["TransportPrice"]) : 0,
                                  SumPrice = (dr["SumPrice"].ToString() != "") ? Convert.ToInt32(dr["SumPrice"]) : 0,
                                  PromotionDetailId = dr["PromotionDetailId"].ToString().Trim(),
                                  ComboCode = dr["ComboCode"].ToString().Trim(),
                                  ComboName = dr["ComboName"].ToString().Trim(),
                                  runningNo = dr["RunningNo"].ToString().Trim(),
                                  LockAmountFlag = dr["LockAmountFlag"].ToString().Trim(),
                                  LockCheckbox = dr["LockCheckbox"].ToString().Trim(),
                                  NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToInt32(dr["NetPrice"]) : 0,
                                  Vat = (dr["Vat"].ToString() != "") ? Convert.ToInt32(dr["Vat"]) : 0,
                                  ParentProductCode = dr["ParentProductCode"].ToString().Trim(),
                                  ParentPromotionCode = dr["ParentPromotionCode"].ToString().Trim(),
                                  FreeShipping = dr["FreeShipping"].ToString().Trim(),
                                  FlagProSetHeader = dr["FlagProSetHeader"].ToString().Trim(),
                                  PromotionTypeCode = dr["PromotionTypeCode"].ToString().Trim(),
                                  PromotionTypeName = dr["PromotionTypeName"].ToString().Trim(),
                                  MOQFlag = dr["MOQFlag"].ToString().Trim(),
                                  MinimumQty = (dr["MinimumQty"].ToString() != "") ? Convert.ToInt32(dr["MinimumQty"]) : 0,
                                  ProductDiscountPercent = (dr["ProductDiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountPercent"]) : 0,
                                  ProductDiscountAmount = (dr["ProductDiscountAmount"].ToString() != "") ? Convert.ToInt32(dr["ProductDiscountAmount"]) : 0,
                                  FreeShippingBill = dr["FreeShippingBill"].ToString().Trim(),
                                  TradeFlag = dr["TradeFlag"].ToString().Trim(),
                                  InventoryCode = dr["InventoryCode"].ToString().Trim(),
                                  CamcatCode = dr["CampaignCategoryCode"].ToString().Trim(),
                                  ChannelCode = dr["ChannelCode"].ToString().Trim(),
                                  ChannelName = dr["ChannelName"].ToString().Trim(),
                                  MediaPlanFlag = dr["FlagMediaPlan"].ToString().Trim(),

                              }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderdata;
        }

        public List<OrderDetailListReturn> ListOrderDetailMapProductAndPromotionNopagingByCriteria(OrderDetailInfo odInfo)
        {
            string strcond = "";

            if ((odInfo.OrderCode != null) && (odInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE     od.OrderCode like '%" + odInfo.OrderCode + "%'" : strcond += "    and  od.OrderCode like '%" + odInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderDetail = new List<OrderDetailListReturn>();

            try
            {
                string strsql =
                    " SELECT od.productcode,od.DiscountPercent, case when  pd.ProductName is null then  pro.PromotionName else pd.ProductName end as productname,od.Amount,od.ProductPrice,od.Price,od.NetPrice,od.TotalPrice, od.InventoryCode, od.Promotioncode " +
                    " ,od.FlagProSetHeader ,l.LookupValue as unitname" +
                    " FROM " + dbName + ".dbo.orderDetail AS od " +
                    " inner join Promotion pro on od.PromotionCode =pro.PromotionCode and pro.FlagDelete='N'" +
                    " LEFT JOIN Product AS pd ON pd.ProductCode = od.ProductCode " +
                    " left join Lookup l on l.LookupCode =pd.Unit and l.LookupType='UNIT' " + strcond;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderDetail = (from DataRow dr in dt.Rows
                                select new OrderDetailListReturn()
                                {
                                    ProductCode = dr["Productcode"].ToString(),
                                    ProductName = dr["ProductName"].ToString(),
                                    InventoryCode = dr["InventoryCode"].ToString(),
                                    PromotionCode = dr["PromotionCode"].ToString(),
                                    Amount = (dr["Amount"].ToString() != "") ? Convert.ToInt32(dr["Amount"]) : 0,
                                    Price = (dr["Price"].ToString() != "") ? Convert.ToDouble(dr["Price"]) : 0,
                                    NetPrice = (dr["NetPrice"].ToString() != "") ? Convert.ToDouble(dr["NetPrice"]) : 0,
                                    ProductPrice = (dr["ProductPrice"].ToString() != "") ? Convert.ToDouble(dr["ProductPrice"]) : 0,
                                    TotalPrice = (dr["TotalPrice"].ToString() != "") ? Convert.ToDouble(dr["TotalPrice"]) : 0,
                                    FlagProSetHeader = dr["FlagProSetHeader"].ToString(),
                                    UnitName = dr["unitname"].ToString(),
                                    DiscountPercent = (dr["DiscountPercent"].ToString() != "") ? Convert.ToInt32(dr["DiscountPercent"]) : 0,

                                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LOrderDetail;
        }
    }
}

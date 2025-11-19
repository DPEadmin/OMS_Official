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
    public class OutBoundDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<LeadManagementInfo> ListCallInNoPagingByCriteria(LeadManagementInfo CInfo)
        {
            string strcond = "";

            if ((CInfo.AssignEmpCode != null) && (CInfo.AssignEmpCode != ""))
            {
                strcond += " and  l.AssignEmpCode = '" + CInfo.AssignEmpCode + "'";
            }
            if ((CInfo.CustomerPhone != null) && (CInfo.CustomerPhone != ""))
            {
                strcond += " and  l.CustomerPhone like '%" + CInfo.CustomerPhone + "%'";
            }
            if ((CInfo.FULL_NAME_TH != null) && (CInfo.FULL_NAME_TH != ""))
            {
                strcond += " and  l.FULL_NAME_TH like '%" + CInfo.FULL_NAME_TH + "%'";
            }

            if ((CInfo.FIRSTNAME_TH != null) && (CInfo.FIRSTNAME_TH != ""))
            {
                strcond += " and  l.FIRSTNAME_TH like '%" + CInfo.FIRSTNAME_TH + "%'";
            }
            if ((CInfo.LASTNAME_TH != null) && (CInfo.LASTNAME_TH != ""))
            {
                strcond += " and  l.LASTNAME_TH like '%" + CInfo.LASTNAME_TH + "%'";
            }
            if ((CInfo.AssignFNAME != null) && (CInfo.AssignFNAME != ""))
            {
                strcond += " and  e.EmpFName_TH like '%" + CInfo.AssignFNAME + "%'";
            }
            if ((CInfo.AssignLNAME != null) && (CInfo.AssignLNAME != ""))
            {
                strcond += " and  e.EmpLName_TH like '%" + CInfo.AssignLNAME + "%'";
            }

            if ((CInfo.PREVIOUS_ORDER_DATE_FROM != null) && (CInfo.PREVIOUS_ORDER_DATE_FROM != "") && (CInfo.PREVIOUS_ORDER_DATE_TO != null) && (CInfo.PREVIOUS_ORDER_DATE_TO != ""))
            {
                strcond += " and  CONVERT(DATETIME, l.PREVIOUS_ORDER_DATE,3) BETWEEN CONVERT(DATETIME, '" + CInfo.PREVIOUS_ORDER_DATE_FROM + "',3) AND CONVERT(DATETIME,'" + CInfo.PREVIOUS_ORDER_DATE_TO + " 23:59:59:999',3)";
            }
            if ((CInfo.BRAND_NO != null) && (CInfo.BRAND_NO != "") && (CInfo.BRAND_NO != "-99"))
            {
                strcond += " and  l.BRAND_NO = '" + CInfo.BRAND_NO + "'";
            }
            if ((CInfo.Name != null) && (CInfo.Name != ""))
            {
                strcond += "and (l.firstname_th LIKE '%" + CInfo.Name + "%' or l.lastname_th LIKE '%" + CInfo.Name + "%')";
                // strcond += " and  l.Name like '%" + CInfo.Name + "%'";

            }
            if ((CInfo.Telephone != null) && (CInfo.Telephone != ""))
            {
                strcond += " and  l.Telephone like '%" + CInfo.Telephone + "%'";
            }
            if ((CInfo.Caryear != null) && (CInfo.Caryear != ""))
            {
                strcond += " and  l.Caryear like '%" + CInfo.Caryear + "%'";
            }
            if ((CInfo.Carmodel != null) && (CInfo.Carmodel != ""))
            {
                strcond += " and  l.Carmodel like '%" + CInfo.Carmodel + "%'";
            }
            if ((CInfo.ProductName != null) && (CInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + CInfo.ProductName + "%'";
            }
            if ((CInfo.PromotionName != null) && (CInfo.PromotionName != ""))
            {
                strcond += " and  pr.PromotionName like '%" + CInfo.PromotionName + "%'";
            }
            if ((CInfo.AssignEmpName != null) && (CInfo.AssignEmpName != ""))
            {
                strcond += " and  e.EmpFName_TH like '%" + CInfo.AssignEmpName + "%'";
                strcond += " or  e.EmpLName_TH like '%" + CInfo.AssignEmpName + "%'";
            }
            if ((CInfo.Status != null) && (CInfo.Status != ""))
            {
                strcond += " and  l.Status in (" + CInfo.Status + ")";
            }
            if ((CInfo.SeachStatus != null) && (CInfo.SeachStatus != "") && (CInfo.SeachStatus != "-99"))
            {
                strcond += " and  l.Status = '" + CInfo.SeachStatus + "'";
            }
            if ((CInfo.MERCHANT_CODE != null) && (CInfo.MERCHANT_CODE != ""))
            {
                strcond += " and  l.MERCHANT_CODE  = '" + CInfo.MERCHANT_CODE + "'";
            }
            if ((CInfo.CreateDateFrom != null) && (CInfo.CreateDateFrom != ""))
            {
                strcond += " AND l.CreateDate BETWEEN CONVERT(DATETIME, '" + CInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + CInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            DataTable dt = new DataTable();
            var LCallin = new List<LeadManagementInfo>();

            try
            {
                string strsql = " SELECT l.ID,l.firstname_th,l.lastname_th,l.full_name_th, l.CustomerCode, l.CustomerPhone, l.CreateDate,l.previous_sale_name,l.previous_product" +
                    ",l.MOBILE_1,l.MOBILE_2,l.MOBILE_3,l.MOBILE_4,l.MOBILE_5,l.MOBILE_6,phone_1,phone_2,phone_3" +
                    ",l.previous_order_date,l.previous_order_brand ,e.EmpFname_TH+' '+e.EmpLName_TH as AssignName, l.CAMPAIGN_ID, l.media_phone" +
                    ",l.Insurancetype, l.Caryear, l.Cartype, l.Carmodel, l.Carsubmodel, l.Insurancedate, l.Name, l.Telephone, l.Email, l.Productid, l.Promotion_Code, l.Status, p.ProductName, pr.PromotionName, l.LeadCode, l.CallStatus " +
                    ",l.RecontactbackDate, l.RecontactbactPeriodTime, lo.LookupValue AS RecontactbactPeriodTimeName, l.CusReason, l.CusReasonOther, l.TransactionTypeCode, tl.LookupValue AS TransactionTypeName " +
                    ",ll.LookupValue as LeadStatusName" +
                    " FROM " + dbName + ".dbo.LeadManagement AS l  " +
                    " inner join emp e on e.EmpCode = l.AssignEmpCode " +
                    " left join Product p on p.Id = l.Productid " +
                    " left join Promotion pr on pr.PromotionCode = l.Promotion_Code " +
                    " left join " + dbName + ".dbo.Lookup AS lo ON lo.LookupCode = l.RecontactbactPeriodTime AND lo.LookupType = 'RECONTACTBACKPERIODTIME'" +
                    " left join " + dbName + ".dbo.Lookup AS tl ON tl.LookupCode = l.TransactionTypeCode AND tl.LookupType = 'TRANSACTIONTYPE'" +
                    " left join " + dbName + ".dbo.Lookup AS ll ON ll.LookupCode = l.Status AND ll.LookupType = 'LEADSTATUS'" +
                                " where 1=1 and l.AssignEmpCode is not null and l.AssignEmpCode !=''  " + strcond;


              
                strsql += "  ORDER BY l.updatedate desc, l.full_name_th DESC OFFSET " + CInfo.rowOFFSet + " ROWS FETCH NEXT " + CInfo.rowFetch + " ROWS ONLY";
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows
                           
                           select new LeadManagementInfo()
                           {
                               LeadID = (dr["Id"].ToString() != "") ? Convert.ToInt32(dr["Id"]) : 0,
                               CreateDate = dr["CreateDate"].ToString().Trim(),
                               FULL_NAME_TH = dr["firstname_th"].ToString().Trim() + " " + dr["lastname_th"].ToString().Trim(),
                               CustomerPhone = dr["CustomerPhone"].ToString().Trim(),
                               CustomerCode = dr["CustomerCode"].ToString().Trim(),
                               PREVIOUS_ORDER_BRAND = dr["PREVIOUS_ORDER_BRAND"].ToString().Trim(),
                               PREVIOUS_PRODUCT = dr["PREVIOUS_PRODUCT"].ToString().Trim(),
                               PREVIOUS_ORDER_DATE = dr["PREVIOUS_ORDER_DATE"].ToString().Trim(),
                               PREVIOUS_SALE_NAME = dr["PREVIOUS_SALE_NAME"].ToString().Trim(),

                               MOBILE_1 = (dr["MOBILE_1"].ToString() != "") ? dr["MOBILE_1"].ToString() : "",
                               MOBILE_2 = (dr["MOBILE_2"].ToString() != "") ?  dr["MOBILE_2"].ToString() : "",
                               MOBILE_3 = (dr["MOBILE_3"].ToString() != "") ?  dr["MOBILE_3"].ToString() : "",
                               MOBILE_4 = (dr["MOBILE_4"].ToString() != "") ?  dr["MOBILE_4"].ToString() : "",
                               MOBILE_5 = (dr["MOBILE_5"].ToString() != "") ?  dr["MOBILE_5"].ToString() : "",
                               MOBILE_6 = (dr["MOBILE_6"].ToString() != "") ? dr["MOBILE_6"].ToString() : "",
                               PHONE_1 = (dr["phone_1"].ToString() != "") ? dr["phone_1"].ToString() : "",
                               PHONE_2 = (dr["phone_2"].ToString() != "") ? dr["phone_2"].ToString() : "",
                               PHONE_3 = (dr["phone_3"].ToString() != "") ? dr["phone_3"].ToString() : "",
                               AssignEmpName   = dr["AssignName"].ToString().Trim(),
                               CAMPAIGN_ID = dr["CAMPAIGN_ID"].ToString().Trim(),
                               MEDIA_PHONE = dr["media_phone"].ToString().Trim(),
                               Insurancetype = dr["Insurancetype"].ToString().Trim(),
                               Caryear = dr["Caryear"].ToString().Trim(),
                               Cartype = dr["Cartype"].ToString().Trim(),
                               Carmodel = dr["Carmodel"].ToString().Trim(),
                               Carsubmodel = dr["Carsubmodel"].ToString().Trim(),
                               Insurancedate = dr["Insurancedate"].ToString().Trim(),
                               Name = dr["Name"].ToString().Trim(),
                               Telephone = dr["Telephone"].ToString().Trim(),
                               Email = dr["Email"].ToString().Trim(),
                               Productid = (dr["Productid"].ToString() != "") ? Convert.ToInt32(dr["Productid"]) : 0,
                               ProductName = dr["ProductName"].ToString().Trim(),
                               Promotion_Code = dr["Promotion_Code"].ToString().Trim(),
                               PromotionName = dr["PromotionName"].ToString().Trim(),
                               Status = dr["Status"].ToString().Trim(),
                               LeadCode = dr["LeadCode"].ToString().Trim(),
                               CallStatus = dr["CallStatus"].ToString().Trim(),
                               RecontactbackDate = dr["RecontactbackDate"].ToString().Trim(),
                               RecontactbactPeriodTime = dr["RecontactbactPeriodTime"].ToString().Trim(),
                               RecontactbactPeriodTimeName = dr["RecontactbactPeriodTimeName"].ToString().Trim(),
                               CusReason = dr["CusReason"].ToString().Trim(),
                               CusReasonOther = dr["CusReasonOther"].ToString().Trim(),
                               TransactionTypeCode = dr["TransactionTypeCode"].ToString().Trim(),
                               TransactionTypeName = dr["TransactionTypeName"].ToString().Trim(),
                               LeadStatusName = dr["LeadStatusName"].ToString().Trim(),

                           }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LCallin;
        }
        public int? CountListCallInNoPagingByCriteria(LeadManagementInfo CInfo)
        {
            string strcond = "";
            int? count = 0;
            if ((CInfo.AssignEmpCode != null) && (CInfo.AssignEmpCode != ""))
            {
                strcond += " and  l.AssignEmpCode = '" + CInfo.AssignEmpCode + "'";
            }
            if ((CInfo.CustomerPhone != null) && (CInfo.CustomerPhone != ""))
            {
                strcond += " and  l.CustomerPhone like '%" + CInfo.CustomerPhone + "%'";
            }
       

            if ((CInfo.FULL_NAME_TH != null) && (CInfo.FULL_NAME_TH != ""))
            {
                strcond += " and  l.FULL_NAME_TH like '%" + CInfo.FULL_NAME_TH + "%'";
            }

            if ((CInfo.FIRSTNAME_TH != null) && (CInfo.FIRSTNAME_TH != ""))
            {
                strcond += " and  l.FIRSTNAME_TH like '%" + CInfo.FIRSTNAME_TH + "%'";
            }
            if ((CInfo.LASTNAME_TH != null) && (CInfo.LASTNAME_TH != ""))
            {
                strcond += " and  l.LASTNAME_TH like '%" + CInfo.LASTNAME_TH + "%'";
            }
            if ((CInfo.AssignFNAME != null) && (CInfo.AssignFNAME != ""))
            {
                strcond += " and  e.EmpFName_TH like '%" + CInfo.AssignFNAME + "%'";
            }
            if ((CInfo.AssignLNAME != null) && (CInfo.AssignLNAME != ""))
            {
                strcond += " and  e.EmpLName_TH like '%" + CInfo.AssignLNAME + "%'";
            }
            if ((CInfo.PREVIOUS_ORDER_DATE_FROM != null) && (CInfo.PREVIOUS_ORDER_DATE_FROM != "") && (CInfo.PREVIOUS_ORDER_DATE_TO != null) && (CInfo.PREVIOUS_ORDER_DATE_TO != ""))
            {
                strcond += " and  CONVERT(DATETIME, l.PREVIOUS_ORDER_DATE,3) BETWEEN CONVERT(DATETIME, '" + CInfo.PREVIOUS_ORDER_DATE_FROM + "',3) AND CONVERT(DATETIME,'" + CInfo.PREVIOUS_ORDER_DATE_TO + " 23:59:59:999',3)";
            }
            if ((CInfo.BRAND_NO != null) && (CInfo.BRAND_NO != "") && (CInfo.BRAND_NO != "-99"))
            {
                strcond += " and  l.BRAND_NO = '" + CInfo.BRAND_NO + "'";
            }
            if ((CInfo.Name != null) && (CInfo.Name != ""))
            {
                strcond += "and (l.firstname_th LIKE '%" + CInfo.Name + "%' or l.lastname_th LIKE '%" + CInfo.Name + "%')";
                // strcond += " and  l.Name like '%" + CInfo.Name + "%'";

            }
            if ((CInfo.Telephone != null) && (CInfo.Telephone != ""))
            {
                strcond += " and  l.Telephone like '%" + CInfo.Telephone + "%'";
            }
            if ((CInfo.Caryear != null) && (CInfo.Caryear != ""))
            {
                strcond += " and  l.Caryear like '%" + CInfo.Caryear + "%'";
            }
            if ((CInfo.Carmodel != null) && (CInfo.Carmodel != ""))
            {
                strcond += " and  l.Carmodel like '%" + CInfo.Carmodel + "%'";
            }
            if ((CInfo.ProductName != null) && (CInfo.ProductName != ""))
            {
                strcond += " and  p.ProductName like '%" + CInfo.ProductName + "%'";
            }
            if ((CInfo.PromotionName != null) && (CInfo.PromotionName != ""))
            {
                strcond += " and  pr.PromotionName like '%" + CInfo.PromotionName + "%'";
            }
            if ((CInfo.AssignEmpName != null) && (CInfo.AssignEmpName != ""))
            {
                strcond += " and  e.EmpFName_TH like '%" + CInfo.AssignEmpName + "%'";
                strcond += " or  e.EmpLName_TH like '%" + CInfo.AssignEmpName + "%'";
            }
            if ((CInfo.Status != null) && (CInfo.Status != ""))
            {
                strcond += " and  l.Status in (" + CInfo.Status + ")";
            }
            if ((CInfo.SeachStatus != null) && (CInfo.SeachStatus != "") && (CInfo.SeachStatus != "-99"))
            {
                strcond += " and  l.Status = '" + CInfo.SeachStatus + "'";
            }
            if ((CInfo.MERCHANT_CODE != null) && (CInfo.MERCHANT_CODE != ""))
            {
                strcond += " and  l.MERCHANT_CODE  = '" + CInfo.MERCHANT_CODE + "'";
            }
          
            if ((CInfo.CreateDateFrom != null) && (CInfo.CreateDateFrom != ""))
            {
                strcond += " AND l.CreateDate BETWEEN CONVERT(DATETIME, '" + CInfo.CreateDateFrom + "',103) AND CONVERT(DATETIME,'" + CInfo.CreateDateTo + " 23:59:59:999',103)";
            }
            DataTable dt = new DataTable();
            var LCallin = new List<LeadManagementInfo>();

            try
            {
                string strsql = " SELECT  count(l.Id) as countLead " +
    
                    " FROM   LeadManagement AS l  " +
                    " inner join emp e on e.EmpCode = l.AssignEmpCode " +
                    " left join Product p on p.Id = l.Productid " +
                    " left join Promotion pr on pr.PromotionCode = l.Promotion_Code " +
                    " left join " + dbName + ".dbo.Lookup AS lo ON lo.LookupCode = l.RecontactbactPeriodTime AND lo.LookupType = 'RECONTACTBACKPERIODTIME'" +
                    " left join " + dbName + ".dbo.Lookup AS tl ON tl.LookupCode = l.TransactionTypeCode AND tl.LookupType = 'TRANSACTIONTYPE'" +
                     " left join " + dbName + ".dbo.Lookup AS ll ON ll.LookupCode = l.Status AND ll.LookupType = 'LEADSTATUS'" +
                                " where 1=1 and l.AssignEmpCode is not null and l.AssignEmpCode !='' " + strcond;
               

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LCallin = (from DataRow dr in dt.Rows

                          select new LeadManagementInfo()
                          {
                              countLead = Convert.ToInt32(dr["countLead"])
                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


            if (LCallin.Count > 0)
            {
                count = LCallin[0].countLead;
            }

            return count;
        }
    }
}

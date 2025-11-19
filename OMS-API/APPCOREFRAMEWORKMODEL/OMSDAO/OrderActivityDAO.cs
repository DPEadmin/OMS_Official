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
    public class OrderActivityDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public List<OrderActivityInfoReturn> ListOrderActivityNopagingByCriteria(OrderActivityInfo odInfo)
        {
            string strcond = "";

            if ((odInfo.OrderCode != null) && (odInfo.OrderCode != ""))
            {
                strcond = strcond == "" ? strcond += " WHERE  oa.OrderCode like '%" + odInfo.OrderCode + "%'" : strcond += " AND  oa.OrderCode like '%" + odInfo.OrderCode + "%'";
            }

            DataTable dt = new DataTable();
            var LOrderDetail = new List<OrderActivityInfoReturn>();

            try
            {
                string strsql =
                    " SELECT oa.OrderCode,l1.LookupValue AS OrderStatusName,l2.LookupValue AS OrderStateName,emp.EmpFname_TH,emp.EmpLName_TH,oa.CreateDate,oa.NOTE " +
                    " FROM " + dbName + ".dbo.OrderActivity AS oa " +
                    " LEFT JOIN Lookup AS l1 ON l1.LookupCode = oa.Orderstatus AND l1.LookupType = 'ORDERSTATUS' " +
                    " LEFT JOIN Lookup AS l2 ON l2.LookupCode = oa.Orderstate AND l2.LookupType = 'ORDERSTATE' " +
                    " LEFT JOIN Emp AS emp ON emp.EmpCode = oa.CreateBy " +  strcond;

                strsql += " ORDER BY CreateDate DESC ";

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LOrderDetail = (from DataRow dr in dt.Rows
                                select new OrderActivityInfoReturn()
                                {
                                    OrderCode = dr["OrderCode"].ToString(),
                                    EmpFName = dr["EmpFname_TH"].ToString(),
                                    EmpLName = dr["EmpLName_TH"].ToString(),
                                    EmpName = dr["EmpFName_TH"].ToString() + " " + dr["EmpLName_TH"].ToString(),
                                    OrderStatusName = dr["OrderStatusName"].ToString(),
                                    OrderStateName = dr["OrderStateName"].ToString(),
                                    CreateDate = dr["CreateDate"].ToString(),
                                    Note = dr["NOTE"].ToString(),
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

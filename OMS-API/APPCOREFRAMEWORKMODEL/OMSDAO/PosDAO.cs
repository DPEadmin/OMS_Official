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
    public class PosDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        //getPOS_terminal
        public List<PosTerminalInfo> ListPosterminalPagingbyCriteria(PosTerminalInfo pInfo)
        {
            string strcond = "";
            string stroffset = "";
            if ((pInfo.Merchant_Code != null) && (pInfo.Merchant_Code != ""))
            {
                strcond += " and  p.Merchant_Code = '" + pInfo.Merchant_Code + "'";
            }
            if ((pInfo.Terminal_ID != null) && (pInfo.Terminal_ID != ""))
            {
                strcond += " and  p.Terminal_ID = '" + pInfo.Terminal_ID + "'";
            }

            
            DataTable dt = new DataTable();
            var LPoint = new List<PosTerminalInfo>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.Pos_Terminal p " +
                               " where p.FLAG_DELETE ='N' " + strcond;

                strsql += " ORDER BY p.Terminal_ID ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPoint = (from DataRow dr in dt.Rows

                          select new PosTerminalInfo()
                          {
                              ID = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                              Merchant_Code = dr["Merchant_Code"].ToString().Trim(),
                              Terminal_ID = dr["Terminal_ID"].ToString().Trim(),
                              CREATE_DATE = dr["CREATE_DATE"].ToString().Trim(),
                              UPDATE_DATE = dr["UPDATE_DATE"].ToString().Trim(),
                              UPDATE_BY = dr["UPDATE_BY"].ToString().Trim(),
                              FLAG_DELETE = dr["UPDATE_BY"].ToString().Trim()

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPoint;
        }



        public int InsertPosterminal(PosTerminalInfo pInfo)
        {

            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.Pos_Terminal (Merchant_Code,Terminal_ID,CREATE_DATE,CREATE_BY,UPDATE_DATE" +
                ",UPDATE_BY,FLAG_DELETE)" +
                           "VALUES (" +
                          "'" + pInfo.Merchant_Code + "'," +
                          "'" + pInfo.Terminal_ID + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.CREATE_BY + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.UPDATE_BY + "'," +                      
                          "'" + pInfo.FLAG_DELETE + "'" +
                           ")";
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }


        //UpdateDelete
        public int UpdatePosterminal(PosTerminalInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.Pos_Terminal set " +
                            " Merchant_Code = '" + pInfo.Merchant_Code + "'," +
                            " Terminal_ID = '" + pInfo.Terminal_ID + "'," +
                            " CREATE_DATE = '" + pInfo.CREATE_DATE + "'," +
                            " CREATE_BY = '" + pInfo.CREATE_BY + "'," +
                            " FLAG_DELETE = '" + pInfo.FLAG_DELETE + "'," +
                            " UPDATE_BY = '" + pInfo.UPDATE_BY + "'," +
                            " UPDATE_DATE = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where ID =" + pInfo.ID + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int DeletePosterminalRange(PosTerminalInfo pInfo)
        {
            int i = 0;

            string strsql = "Update " + dbName + ".dbo.Pos_Terminal set FLAG_DELETE = 'Y' where Id in (" + pInfo.ID + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }



        public List<PosAmountManagementInfo> ListPosAmountManagement(PosAmountManagementInfo pInfo)
        {
            string strcond = "";
            string stroffset = "";
            if ((pInfo.Merchant_Code != null) && (pInfo.Merchant_Code != ""))
            {
                strcond += " and  p.Merchant_Code = '" + pInfo.Merchant_Code + "'";
            }
            if ((pInfo.Terminal_ID != null) && (pInfo.Terminal_ID != ""))
            {
                strcond += " and  p.Terminal_ID = '" + pInfo.Terminal_ID + "'";
            }

            
            DataTable dt = new DataTable();
            var LPoint = new List<PosAmountManagementInfo>();

            try
            {
                string strsql = " select p.* from " + dbName + ".dbo.POS_Amount_Management p " +
                               " where p.FLAG_DELETE ='N' " + strcond;

                strsql += " ORDER BY p.Terminal_ID ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPoint = (from DataRow dr in dt.Rows

                          select new PosAmountManagementInfo()
                          {
                              ID = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                              Merchant_Code = dr["Merchant_Code"].ToString().Trim(),
                              Amount = (dr["Amount"].ToString() != "") ? Convert.ToSingle(dr["Amount"]) :0,
                              Type_Recive = dr["Type_Recive"].ToString().Trim(),
                              Type_Spend = dr["Type_Spend"].ToString().Trim(),
                              Terminal_ID = dr["Terminal_ID"].ToString().Trim(),
                              CREATE_DATE = dr["CREATE_DATE"].ToString().Trim(),
                              CREATE_BY = dr["CREATE_BY"].ToString().Trim(),
                              UPDATE_DATE = dr["UPDATE_DATE"].ToString().Trim(),
                              UPDATE_BY = dr["UPDATE_BY"].ToString().Trim(),
                              FLAG_DELETE = dr["UPDATE_BY"].ToString().Trim()

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPoint;
        }

        public int InsertPosAmountManagement(PosAmountManagementInfo pInfo)
        {

            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.POS_Amount_Management (Amount,Type_Recive,Type_Spend,Merchant_Code,Terminal_ID,CREATE_DATE,CREATE_BY,UPDATE_DATE" +
                ",UPDATE_BY,FLAG_DELETE)" +
                           "VALUES (" +
                            "'" + pInfo.Amount + "'," +
                             "'" + pInfo.Type_Recive + "'," +
                              "'" + pInfo.Type_Spend + "'," +
                          "'" + pInfo.Merchant_Code + "'," +
                          "'" + pInfo.Terminal_ID + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.CREATE_BY + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.UPDATE_BY + "'," +
                          "'" + pInfo.FLAG_DELETE + "'" +
                           ")";
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }


        public int UpdatePosAmountManagement(PosAmountManagementInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.POS_Amount_Management set " +

                            " Amount = '" + pInfo.Amount + "'," +
                            " Type_Recive = '" + pInfo.Type_Recive + "'," +
                            " Type_Spend = '" + pInfo.Type_Spend + "'," +
                            " Merchant_Code = '" + pInfo.Merchant_Code + "'," +
                            " Terminal_ID = '" + pInfo.Terminal_ID + "'," +
                            " CREATE_DATE = '" + pInfo.CREATE_DATE + "'," +
                            " CREATE_BY = '" + pInfo.CREATE_BY + "'," +
                            " FLAG_DELETE = '" + pInfo.FLAG_DELETE + "'," +
                            " UPDATE_BY = '" + pInfo.UPDATE_BY + "'," +
                            " UPDATE_DATE = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where ID =" + pInfo.ID + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }



        public List<PosopenSaleReturn> ListPosOpenSaleorder(PosopenSaleReturn pInfo)
        {
            string strcond = "";
            string stroffset = "";
            
            DataTable dt = new DataTable();
            var LPoint = new List<PosopenSaleReturn>();

            try
            {
                string strsql = " select * from POS_Open_Sale left join POS_Amount_Management on POS_Amount_Management.Terminal_ID = POS_Amount_Management.Terminal_ID  " +
                               " where  POS_Amount_Management.Terminal_ID = '" + pInfo.Terminal_ID + "'"  + strcond;

                strsql += " ORDER BY POS_Open_Sale.Terminal_ID ASC " + stroffset;

                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);
                LPoint = (from DataRow dr in dt.Rows

                          select new PosopenSaleReturn()
                          {
                              ID = (dr["ID"].ToString() != "") ? Convert.ToInt32(dr["ID"]) : 0,
                              Merchant_Code = dr["Merchant_Code"].ToString().Trim(),
                              Amount = (dr["Amount"].ToString() != "") ? Convert.ToSingle(dr["Amount"]) : 0,
                              Type_Recive = dr["Type_Recive"].ToString().Trim(),
                              Type_Spend = dr["Type_Spend"].ToString().Trim(),
                              Terminal_ID = dr["Terminal_ID"].ToString().Trim(),
                              CREATE_DATE = dr["CREATE_DATE"].ToString().Trim(),
                              CREATE_BY = dr["CREATE_BY"].ToString().Trim(),
                              UPDATE_DATE = dr["UPDATE_DATE"].ToString().Trim(),
                              UPDATE_BY = dr["UPDATE_BY"].ToString().Trim(),
                              FLAG_DELETE = dr["UPDATE_BY"].ToString().Trim(),
                              Amount_OPEN = (dr["Amount_OPEN"].ToString() != "") ? Convert.ToSingle(dr["Amount_OPEN"]) : 0,
                              Amount_CLOSE = (dr["Amount_CLOSE"].ToString() != "") ? Convert.ToSingle(dr["Amount_CLOSE"]) : 0,

                          }).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return LPoint;
        }


        public int UpdatePosOpenSaleId(PosopenSaleInfo pInfo)
        {
            int i = 0;

            string strsql = " Update " + dbName + ".dbo.POS_Amount_Management set " +

                         
                            " Merchant_Code = '" + pInfo.Merchant_Code + "'," +
                            " Amount_CLOSE = '" + pInfo.Amount_CLOSE + "'," +
                            " Amount_OPEN = '" + pInfo.Amount_OPEN + "'," +
                            " Terminal_ID = '" + pInfo.Terminal_ID + "'," +
                            " CREATE_DATE = '" + pInfo.CREATE_DATE + "'," +
                            " CREATE_BY = '" + pInfo.CREATE_BY + "'," +                           
                            " UPDATE_BY = '" + pInfo.UPDATE_BY + "'," +
                            " UPDATE_DATE = '" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'" +
                            " where ID =" + pInfo.ID + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }



        public int InsertPosOpenSaleId(PosopenSaleInfo pInfo)
        {

            int i = 0;

            string strsql = "INSERT INTO " + dbName + ".dbo.POS_Open_Sale (Merchant_Code,Amount_OPEN,Amount_CLOSE,Terminal_ID,CREATE_DATE,CREATE_BY,UPDATE_DATE" +
                ",UPDATE_BY)" +
                           "VALUES (" +
                            "'" + pInfo.Merchant_Code + "'," +
                             "'" + pInfo.Amount_OPEN + "'," +
                              "'" + pInfo.Amount_CLOSE + "'," +
                          "'" + pInfo.Terminal_ID + "'," +              
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.CREATE_BY + "'," +
                          "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                          "'" + pInfo.UPDATE_BY + "'," +
                        
                           ")";
            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

    }
}

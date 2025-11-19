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
    public class Transport3PartyDAO
    {
        public string GenThaipost(ThaiPostInfo oinfo)
        {
            string ordertracking = "";
            int maxOrder = 1;

            DataTable dt = new DataTable();

            string strsql = @"SELECT NEXT VALUE FOR  SeqThaipost AS OrderTrackingNo ";

            try
            {
                Database db = new Database(APPHELPPERS.Driver.ConntectionString());
                SqlCommand com = new SqlCommand();
                com.CommandText = strsql;
                com.CommandType = System.Data.CommandType.Text;
                dt = db.ExcuteDataReaderText(com);

                if (dt.Rows.Count > 0)
                {
                    maxOrder = (dt.Rows[0]["OrderTrackingNo"] != null) ? int.Parse(dt.Rows[0]["OrderTrackingNo"].ToString()) : 1;
                }
             
                switch (maxOrder.ToString().Length)
                {
                    case 1:
                        ordertracking = "00000" + maxOrder.ToString();
                        break;
                    case 2:
                        ordertracking = "0000" + maxOrder.ToString();
                        break;
                    case 3:
                        ordertracking = "000" + maxOrder.ToString();
                        break;
                    case 4:
                        ordertracking = "00" + maxOrder.ToString();
                        break;
                    case 5:
                        ordertracking = "0" + maxOrder.ToString();
                        break;
                    case 6:
                        ordertracking =  maxOrder.ToString();
                        break;
                    default:
                        ordertracking = "999999";
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            return ordertracking.ToString();
        }
    }
}

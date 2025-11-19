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
    public class LandmarkDAO
    {
        public string dbName = ConfigurationManager.AppSettings["dbName"].ToString();

        public int DeleteLandmark(LandmarkInfo lmInfo)
        {
            int i = 0;

            string strsql = "delete from Landmark" +
                            " where Id in (" + lmInfo.LandmarkId + ")";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int InsertLandmark(LandmarkInfo caInfo)
        {
            int i = 0;

            string strsql = "INSERT INTO Landmark  (LandmarkCode,LandmarkName,LandmarkDesc,CustomerAddressId,Lat,Long,CreateDate,CreateBy,UpdateDate,FlagDelete)" +
                            "VALUES (" +
                           "'" + caInfo.LandmarkCode + "'," +
                           "'" + caInfo.LandmarkName + "'," +
                           "'" + caInfo.LandmarkDesc + "'," +
                           "'" + caInfo.CustomerAddressId + "'," +
                           "'" + caInfo.Lat + "'," +
                           "'" + caInfo.Long + "'," +
                            "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + caInfo.CreateBy + "'," +
                           "'" + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") + "'," +
                           "'" + caInfo.FlagDelete + "')";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }

        public int UpdateLandmark(LandmarkInfo lmInfo)
        {
            int i = 0;

            string strsql = "UPDATE Landmark  SET " +
                            " LandmarkCode = '" + lmInfo.LandmarkCode + "'," +
                            " LandmarkName = '" + lmInfo.LandmarkName + "'," +
                            " LandmarkDesc = '" + lmInfo.LandmarkDesc + "'," +
                            " CustomerAddressId = " + lmInfo.CustomerAddressId + "," +
                            " Lat = '" + lmInfo.Lat + "'," +
                            " Long = '" + lmInfo.Long + "'," +
                            " UpdateBy = '" + lmInfo.UpdateBy + "'," +
                            " UpdateDate = '" + lmInfo.UpdateDate + "'," +
                            " where Id =" + lmInfo.LandmarkId + "";


            Database db = new Database(APPHELPPERS.Driver.ConntectionString());
            SqlCommand com = new SqlCommand();
            com.CommandText = strsql;
            com.CommandType = System.Data.CommandType.Text;
            i = db.ExcuteBeginTransectionText(com);

            return i;
        }


    }
}

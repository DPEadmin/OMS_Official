using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace APPHELPPERS
{
    public class Database
    {
        SqlTransaction _tran;
        SqlConnection _con;
        SqlDataAdapter _dav;
        string _sqlprovider;
        public Database(string _connectionString)
        {
            _sqlprovider = _connectionString;
            _con = new SqlConnection(_sqlprovider);
        }
        public void Commit()
        {
            _tran.Commit();
        }
        public void Rollback()
        {
            _tran.Rollback();
        }
        public int ExcuteBeginTransection(SqlCommand _com)
        {
            try
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                if (_tran != null) { _tran = _con.BeginTransaction(); }
                _com.Connection = _con;
                _com.Transaction = _tran;
                _com.CommandType = CommandType.StoredProcedure;
                return _com.ExecuteNonQuery();
              //  _con.Close();
            
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public List<T> ExcuteDataReader<T>(SqlCommand _com) where T:new()
        {
            try
            {
                DataTable dt = new DataTable();
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                _com.Connection = _con;
                _com.CommandType = CommandType.Text;
                _dav = new SqlDataAdapter(_com);
                _dav.Fill(dt);
                _con.Close();
                return MappingModel.ToList<T>(dt);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ExcuteDataSetReader(SqlCommand _com)
        {
            try
            {
                DataSet ds = new DataSet();
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                _com.Connection = _con;
                _com.CommandType = CommandType.StoredProcedure;
                _dav = new SqlDataAdapter(_com);
                _con.Close();
                _dav.Fill(ds);
                return ds;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void CopyDataToDatabase(DataTable dt,string tablename)
        {
            try
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                var _copy = new SqlBulkCopy(_con);
                _copy.DestinationTableName = tablename;
                _copy.WriteToServer(dt);
                _con.Close();
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        public DataTable ExcuteDataReaderText(SqlCommand _com)
        {
            try
            {
                DataTable dt = new DataTable();
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                _com.Connection = _con;
                _com.CommandType = CommandType.Text;
                _dav = new SqlDataAdapter(_com);
                _dav.Fill(dt);
                _con.Close();
                return dt;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ExcuteBeginTransectionText(SqlCommand _com)
        {
            int i ;
            try
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                if (_tran != null) { _tran = _con.BeginTransaction(); }
                _com.Connection = _con;
                _com.Transaction = _tran;
                _com.CommandType = CommandType.Text;
                i= _com.ExecuteNonQuery();
                _con.Close();
                return i;
               

            }
            catch (Exception ex)
            {
                throw ex;
             
            }
         
        }

        public int ExcuteBeginTransectionTextReturnID(SqlCommand _com)
        {
            int id;

            try
            {
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                if (_tran != null) { _tran = _con.BeginTransaction(); }
                _com.Connection = _con;
                _com.Transaction = _tran;
                _com.CommandType = CommandType.Text;
                //i = _com.ExecuteNonQuery();
                id = Convert.ToInt32(_com.ExecuteScalar());
                _con.Close();
                return id;


            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public DataTable ExcuteBeginStoredProcedureText(SqlCommand _com)
        {
            try
            {
                DataTable dt = new DataTable();
                if (_con.State == ConnectionState.Closed) { _con.Open(); }
                _com.Connection = _con;
                _com.CommandType = System.Data.CommandType.StoredProcedure;
                //_com.CommandType = CommandType.Text;
                _dav = new SqlDataAdapter(_com);
                _dav.Fill(dt);
                _con.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}

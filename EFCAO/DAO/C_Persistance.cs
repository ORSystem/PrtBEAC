using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EFCAO.DAO
{
    public class C_Persistance
    {
        #region ------------------------Variables membres------------------------
        /// <summary>
        /// Description résumée de DataConnection
        /// </summary>
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataAdapter _dataAdapter;
        private SqlDataReader _dataReader;
        private SqlTransaction _laTransaction;

        #endregion

        #region -----------------------------Constructeur-------------------------
        public C_Persistance(string cnx)
        {

            _connection = new SqlConnection();

            _connection.ConnectionString = cnx;
        }
        #endregion

        #region -----------------------------Methods-------------------------
        /// <summary>
        /// Apppel d'une procedure avec retour d'un Dataset
        /// </summary>
        /// <param name="name">Nom de la procédure</param>
        /// <returns>Dataset</returns>
        public DataSet CallProcedureDs(string name)
        {
            return CallDs(name, null, true);
        }

        /// <summary>
        /// Apppel d'une procedure avec retour d'un Dataset
        /// </summary>
        /// <param name="name">Nom de la procédure</param>
        /// <param name="param">Paramêtres de la procédure</param>
        /// <returns>Dataset</returns>
        public DataSet CallProcedureDs(string name, List<C_DataParam> param, List<C_DateDataParam> ParamDate = null)
        {
            return CallDs(name, param, true, ParamDate);
        }

        /// <summary>
        /// Appel d'une requete sql avec retour d'un Dataset
        /// </summary>
        /// <param name="name">Requète Sql</param>
        /// <returns>Dataset</returns>
        public DataSet CallSqlDs(string name)
        {
            return CallDs(name, null, false, null);
        }

        public DataSet CallSqlDs(string name, List<C_DataParam> param)
        {
            return CallDs(name, param, false);
        }

        /// <summary>
        /// Appel d'une procédure
        /// </summary>
        /// <param name="name">Nom de la procédure</param>
        /// <returns></returns>
        public bool CallProcedure(string name)
        {
            return CallSql(name, null, true);
        }

        /// <summary>
        /// Appel d'une procédure avec paramêtres
        /// </summary>
        /// <param name="name">Nom de la procédure</param>
        /// <param name="param">paramêtres de la procédure</param>
        /// <returns>Booléen</returns>
        public bool CallProcedure(string name, List<C_DataParam> param)
        {
            return CallSql(name, param, true);
        }


        /// <summary>
        /// Appel d'une requête
        /// </summary>
        /// <param name="name">Requète Sql</param></param>
        /// <returns>Booléen</returns>
        public bool CallSql(string name)
        {
            return CallSql(name, null, false);
        }


        public bool CallSql(string name, List<C_DataParam> param)
        {
            return CallSql(name, param, false);
        }

        #endregion

        #region -----------------A  function that excute a command sql (stored procedure) and return a Hashtable  -----------------
        public Hashtable CallDataReader(string name)
        {
            Hashtable ht = null;

            try
            {
                _connection.Open();
                _command = new SqlCommand(name, _connection);
                _command.CommandType = CommandType.Text;

                this._dataReader = _command.ExecuteReader();

                if (this._dataReader.FieldCount > 0)
                {
                    ht = new Hashtable();

                    while (this._dataReader.Read())
                    {
                        ht.Add(this._dataReader[0], this._dataReader[1]);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            catch (SystemException ex)
            {
                throw ex;
            }

            finally
            {
                Close();
            }


            return ht;
        }
        #endregion

        #region -----------------A  function that excute a command sql (stored procedure) and return a data set with parameter -----------------

        private DataSet CallDs(string name, List<C_DataParam> param, bool proc, List<C_DateDataParam> ParamDate = null)
        {
            try
            {
                DataSet ds = new DataSet();
                _connection.Open();
                _command = new SqlCommand(name, _connection);

                if (proc)
                    _command.CommandType = CommandType.StoredProcedure;
                else
                    _command.CommandType = CommandType.Text;

                if (param != null && param.Count > 0)
                {
                    foreach (C_DataParam p in param)
                    {
                        _command.Parameters.AddWithValue(p.Name, p.Value);
                    }

                    if (ParamDate != null)
                    {

                        foreach (C_DateDataParam p in ParamDate)
                        {
                            _command.Parameters.AddWithValue(p.Name, p.valueDate);
                        }
                    }
                }

                _dataAdapter = new SqlDataAdapter();
                _dataAdapter.SelectCommand = _command;

                _dataAdapter.Fill(ds);
                return ds;
            }

            catch (SqlException ex)
            {
                throw ex;
            }
            catch (SystemException ex)
            {
                throw ex;
            }

            finally
            {
                Close();
            }
        }

        #endregion

        #region -----------------A boolean function that excute a transaction Sql from a stored procedure -----------------

        private bool CallSql(string name, List<C_DataParam> param, bool proc)
        {
            bool bOk = false;

            try
            {
                _connection.Open();
                _laTransaction = _connection.BeginTransaction();
                _command = new SqlCommand(name, _connection);
                _command.Transaction = _laTransaction;
                if (proc)
                    _command.CommandType = CommandType.StoredProcedure;
                else
                    _command.CommandType = CommandType.Text;

                if (param != null && param.Count > 0)
                {
                    foreach (C_DataParam p in param)
                    {
                        _command.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }

                int y = _command.ExecuteNonQuery();

                _laTransaction.Commit();

                bOk = true;
            }


            catch (SqlException ex)
            {
                bOk = false;
                throw ex;
            }
            catch (SystemException ex)
            {

                bOk = false;
                throw ex;
            }

            finally
            {
                Close();
            }

            return bOk;
        }

        #endregion

        #region -----------------Close Command, Adapter, Reader, Connection-----------------

        public void Close()
        {
            if (_command != null)
                _command.Dispose();
            if (_dataAdapter != null)
                _dataAdapter.Dispose();
            if (_dataReader != null)
                _dataReader.Close();
            if (_connection != null && _connection.State == ConnectionState.Open)
                _connection.Close();


        }
        #endregion
    }
}
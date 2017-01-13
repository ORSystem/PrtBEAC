using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess;
using Oracle.ManagedDataAccess.Types;
using System.Linq;
using System.Web;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using EFCAO.EfcaoException;

namespace EFCAO.DAO
{
    public class C_PersistanceOracle : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        #region ------------------------Variables membres------------------------

        /// <summary>
        /// Description résumée de DataConnection
        /// </summary>

        private Oracle.ManagedDataAccess.Client.OracleConnection _connection;
        private Oracle.ManagedDataAccess.Client.OracleCommand _command;
        private Oracle.ManagedDataAccess.Client.OracleDataAdapter _dataAdapter;
        private Oracle.ManagedDataAccess.Client.OracleDataReader _dataReader;
        private Oracle.ManagedDataAccess.Client.OracleTransaction _laTransaction;

        #endregion

        #region -----------------------------Constructeur-------------------------

        public C_PersistanceOracle(string cnx)
        {

            _connection = new Oracle.ManagedDataAccess.Client.OracleConnection();

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
        public DataSet CallProcedureDs(string name, List<C_DataParam> param)
        {
            return CallDs(name, param, true);
        }

        /// <summary>
        /// Apppel d'une procedure avec retour d'un Dataset
        /// </summary>
        /// <param name="name">Nom de la procédure</param>
        /// <param name="param">Paramêtres de la procédure</param>
        /// <returns>Dataset</returns>
        public int CallProcedureReturningIntegerValue(string name, List<C_DataParam> param)
        {
            return CallProcedureReturningIntegerValue(name, param, true);
        }

        /// <summary>
        /// Apppel d'une procedure avec retour d'un Dataset
        /// </summary>
        /// <param name="name">Nom de la procédure</param>
        /// <param name="param">Paramêtres de la procédure</param>
        /// <returns>Dataset</returns>
        public bool CallProcedureReturningBooleanValue(string name, List<C_DataParam> param)
        {
            return CallProcedureReturningBooleanValue(name, param, true);
        }


        /// <summary>
        /// Appel d'une requete sql avec retour d'un Dataset
        /// </summary>
        /// <param name="name">Requète Sql</param>
        /// <returns>Dataset</returns>
        public DataSet CallSqlDs(string name)
        {
            return CallDs(name, null, false);
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

        #region ------------------------Dispose------------------------
        // Other functions go here...

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Close();
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        #endregion

        #region -----------------A  function that excute a command Oracle (stored procedure) and return a Hashtable -----------------
        public Hashtable CallDataReader(string name)
        {
            Hashtable ht = null;
            try
            {
                _connection.Open();
                _command = new Oracle.ManagedDataAccess.Client.OracleCommand(name, _connection);
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
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Erreur lors de la chargement de Data Reader";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallDataReader()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Erreur lors de la chargement de ades document";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallDataReader()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            finally
            {
                Close();
            }

            return ht;
        }
        #endregion

        #region -----------------A  function that excute a command Oracle (stored procedure) and return a data set with parameter -----------------
        /// <summary>
        /// A  function that excute a command Oracle (stored procedure) and return a data set with parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="proc"></param>
        /// <returns></returns>
        private DataSet CallDs(string name, List<C_DataParam> param, bool proc)
        {
            try
            {
                DataSet ds = new DataSet();
                _connection.Open();
                _command = new Oracle.ManagedDataAccess.Client.OracleCommand(name, _connection);

                if (proc)
                    _command.CommandType = CommandType.StoredProcedure;
                else
                    _command.CommandType = CommandType.Text;

                if (param != null && param.Count > 0)
                {
                    foreach (C_DataParam p in param)
                    {
                        if (p.Type == "VarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "NVarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "Char")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Char, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "DateTime")
                        {
                            //_command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = p.Value;
                            if (p.Value == null)
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = null;
                            }
                            else
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = Convert.ToDateTime(p.Value);
                            }
                        }
                        else if ((p.Type == "Number") || (p.Type == "Integer"))
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Int32).Value = p.Value;
                        }
                        else if ((p.Type == "DECIMAL"))
                        {
                            // Fro english Deciaml
                            //string x = p.Value;
                            //x = x.Replace(",", ".");
                            //_command.Parameters.Add(p.Name, OracleDbType.Decimal).Value = x;
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Decimal).Value = Convert.ToDecimal(p.Value);
                        }

                        else if (p.Type == "Cursor")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        }
                    }
                }
                _dataAdapter = new Oracle.ManagedDataAccess.Client.OracleDataAdapter();
                _dataAdapter.SelectCommand = _command;
                _dataAdapter.Fill(ds);
                return ds;
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error calling data set";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallDs()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error calling data set";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallDs()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            finally
            {
                Close();
            }
        }

        #endregion

        #region -----------------A  function that excute a command Oracle (stored procedure) and return integer value -----------------
        /// <summary>
        /// A  function that excute a command Oracle (stored procedure) and return integer value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="proc"></param>
        /// <returns></returns>
        private int CallProcedureReturningIntegerValue(string name, List<C_DataParam> param, bool proc)
        {
            int nRetour;
            try
            {
                DataSet ds = new DataSet();
                _connection.Open();
                _command = new Oracle.ManagedDataAccess.Client.OracleCommand(name, _connection);

                if (proc)
                    _command.CommandType = CommandType.StoredProcedure;
                else
                    _command.CommandType = CommandType.Text;

                if (param != null && param.Count > 0)
                {
                    foreach (C_DataParam p in param)
                    {
                        if (p.Type == "VarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "NVarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "Char")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Char, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "DateTime")
                        {
                            //_command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = p.Value;
                            if (p.Value == null)
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = null;
                            }
                            else
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = Convert.ToDateTime(p.Value);
                            }
                        }
                        else if ((p.Type == "Number") || (p.Type == "Integer"))
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Int32).Value = p.Value;
                        }
                        else if ((p.Type == "DECIMAL"))
                        {
                            // Fro english Deciaml
                            //string x = p.Value;
                            //x = x.Replace(",", ".");
                            //_command.Parameters.Add(p.Name, OracleDbType.Decimal).Value = x;
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Decimal).Value = Convert.ToDecimal(p.Value);
                        }

                        else if (p.Type == "Cursor")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        }
                    }
                }
                nRetour = _command.ExecuteNonQuery();
                return nRetour;
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error call procedure returning integer value";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallProcedureReturningIntegerValue()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error call procedure returning integer value";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallProcedureReturningIntegerValue()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            finally
            {
                Close();
            }
        }

        #endregion

        #region -----------------A  function that excute a command Oracle (stored procedure) and return a boolean value -----------------
        /// <summary>
        /// A  function that excute a command Oracle (stored procedure) and return a boolean value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="proc"></param>
        /// <returns></returns>
        private bool CallProcedureReturningBooleanValue(string name, List<C_DataParam> param, bool proc)
        {
            bool nRetour = false;
            try
            {
                DataSet ds = new DataSet();
                _connection.Open();
                _command = new Oracle.ManagedDataAccess.Client.OracleCommand(name, _connection);

                if (proc)
                    _command.CommandType = CommandType.StoredProcedure;
                else
                    _command.CommandType = CommandType.Text;

                if (param != null && param.Count > 0)
                {
                    foreach (C_DataParam p in param)
                    {
                        if (p.Type == "VarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "NVarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "Char")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Char, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "DateTime")
                        {
                            //_command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = p.Value;
                            if (p.Value == null)
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = null;
                            }
                            else
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = Convert.ToDateTime(p.Value);
                            }
                        }
                        else if ((p.Type == "Number") || (p.Type == "Integer"))
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Int32).Value = p.Value;
                        }
                        else if ((p.Type == "DECIMAL"))
                        {
                            // Fro english Deciaml
                            //string x = p.Value;
                            //x = x.Replace(",", ".");
                            //_command.Parameters.Add(p.Name, OracleDbType.Decimal).Value = x;
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Decimal).Value = Convert.ToDecimal(p.Value);
                        }
                        else if (p.Type == "Cursor")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        }
                    }
                }
                _command.ExecuteNonQuery();
                nRetour = true;
                return nRetour;
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error call procedure returning boolean value";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallProcedureReturningBooleanValue()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error call procedure returning boolean value";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallProcedureReturningBooleanValue()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            finally
            {
                Close();
            }
        }

        #endregion

        #region ----------------A boolean function that excute a transaction Oracle from a stored procedure -----------------
        /// <summary>
        /// A boolean function that excute a transaction Oracle from a stored procedure
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="proc"></param>
        /// <returns></returns>
        private bool CallProcedureWithTransaction(string name, List<C_DataParam> param, bool proc)
        {
            bool bOk = false;

            try
            {
                _connection.Open();
                _laTransaction = _connection.BeginTransaction();
                _command = new Oracle.ManagedDataAccess.Client.OracleCommand(name, _connection);
                _command.Transaction = _laTransaction;

                if (proc)
                    _command.CommandType = CommandType.StoredProcedure;
                else
                    _command.CommandType = CommandType.Text;

                if (param != null && param.Count > 0)
                {
                    foreach (C_DataParam p in param)
                    {
                        if (p.Type == "VarChar")
                        {
                            //_command.Parameters.AddWithValue(p.Name, p.Value); .NVarChar/Char DateTime
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "NVarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "Char")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Char, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "DateTime")
                        {
                            //_command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = p.Value;
                            if (p.Value == null)
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = null;
                            }
                            else
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = Convert.ToDateTime(p.Value);
                            }
                        }
                        else if ((p.Type == "Number") || (p.Type == "Integer"))
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Int32).Value = p.Value;
                        }
                        else if ((p.Type == "DECIMAL"))
                        {
                            // Fro english Deciaml
                            //string x = p.Value;
                            //x = x.Replace(",", ".");
                            //_command.Parameters.Add(p.Name, OracleDbType.Decimal).Value = x;
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Decimal).Value = Convert.ToDecimal(p.Value);
                        }

                        else if (p.Type == "Cursor")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        }
                    }
                }
                int y = _command.ExecuteNonQuery();
                _laTransaction.Commit();
                bOk = true;
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                bOk = false;
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error call procedure with transaction";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallProcedureWithTransaction()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();           
                throw;
            }
            catch (SystemException ex)
            {
                bOk = false;
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error call procedure with transaction";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallProcedureWithTransaction()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();              
                throw;
            }
            finally
            {
                Close();
            }
            return bOk;
        }

        #endregion

        #region -----------------A boolean function that excute a transaction Oracle from a stored procedure -----------------

        private bool CallSql(string name, List<C_DataParam> param, bool proc)
        {
            bool bOk = false;

            try
            {
                _connection.Open();
                _laTransaction = _connection.BeginTransaction();
                _command = new Oracle.ManagedDataAccess.Client.OracleCommand(name, _connection);
                _command.Transaction = _laTransaction;

                if (proc)
                    _command.CommandType = CommandType.StoredProcedure;
                else
                    _command.CommandType = CommandType.Text;

                if (param != null && param.Count > 0)
                {
                    foreach (C_DataParam p in param)
                    {
                        if (p.Type == "VarChar")
                        {
                            //_command.Parameters.AddWithValue(p.Name, p.Value); .NVarChar/Char DateTime
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "NVarChar")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.NVarchar2, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "Char")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Char, p.Size).Value = p.Value;
                        }
                        else if (p.Type == "DateTime")
                        {
                            //_command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = p.Value;
                            if (p.Value == null)
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = null;
                            }
                            else
                            {
                                _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Date).Value = Convert.ToDateTime(p.Value);
                            }
                        }
                        else if ((p.Type == "Number") || (p.Type == "Integer"))
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Int32).Value = p.Value;
                        }
                        else if ((p.Type == "DECIMAL"))
                        {
                            // Fro english Deciaml
                            //string x = p.Value;
                            //x = x.Replace(",", ".");
                            //_command.Parameters.Add(p.Name, OracleDbType.Decimal).Value = x;
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.Decimal).Value = Convert.ToDecimal(p.Value);
                        }

                        else if (p.Type == "Cursor")
                        {
                            _command.Parameters.Add(p.Name, Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        }
                    }
                }
                int y = _command.ExecuteNonQuery();
                _laTransaction.Commit();
                bOk = true;
            }
            catch (Oracle.ManagedDataAccess.Client.OracleException ex)
            {
                bOk = false;
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error calling oracle command";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallSql()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            catch (SystemException ex)
            {
                bOk = false;
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = "Error calling oracle command";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PersistanceOracle.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PersistanceOracle.CallSql()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw;
            }
            finally
            {
                Close();
            }
            return bOk;
        }

        #endregion

        #region ----------------- Close Oracle Command, Adapter, Reader, Connection -----------------

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
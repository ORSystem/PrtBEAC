using EFCAO.BLL.Collections;
using EFCAO.BLL.Entities;
using EFCAO.CommonFunctions;
using EFCAO.DAO;
using EFCAO.EfcaoException;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace EFCAO.DAL
{
    public class C_ConsomDal : IDisposable
    {
        private Oracle.ManagedDataAccess.Client.OracleConnection Connection;
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

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

        #region ------------------------Consommation --------------------

        #region -----------------------Get List Consommation ---------------------
        /// <summary>
        /// Get List Consommation
        /// </summary>
        /// <param name="C_ListGroupsRules"></param>
        /// <returns>TheListGroupsRules</returns>
        public object GetListConsommation(C_ListConsoms TheListConsommation, int UserID)
        {
            C_PersistanceOracle persistance = null;
            try
            {
                persistance = new C_PersistanceOracle(C_Functionglobal.getOracleConnection());
                string sql = "Get_Consommation_By_UserID.PS_Get_Consommation_By_UserID";

                List<C_DataParam> Param = new List<C_DataParam>();
                Param.Add(new C_DataParam("User_ID", UserID.ToString(), 0, "Number"));
                Param.Add(new C_DataParam("Cur_Consommation", "", 0, "Cursor"));
                DataSet ds = new DataSet();
                ds = persistance.CallProcedureDs(sql, Param);

                foreach (DataRow Dr in ds.Tables[0].Rows)
                {
                    C_Consom TheConsommation = new C_Consom();

                    if (Dr["Consumption_ID"] != DBNull.Value)
                    {
                        TheConsommation.Consumption_ID = Convert.ToInt32(Dr["Consumption_ID"].ToString());
                    }

                    if (Dr["Link_User_ID"] != DBNull.Value)
                    {
                        TheConsommation.Link_User_ID = Convert.ToInt32(Dr["Link_User_ID"].ToString());
                    }

                    if (Dr["Link_Prod_ID"] != DBNull.Value)
                    {
                        TheConsommation.Link_Prod_ID = Convert.ToInt32(Dr["Link_Prod_ID"].ToString());
                    }

                    if (Dr["Quantity"] != DBNull.Value)
                    {
                        TheConsommation.Quantity = Convert.ToInt32(Dr["Quantity"].ToString());
                    }

                    if (Dr["Unit_Price"] != DBNull.Value)
                    {
                        TheConsommation.Unit_Price = Convert.ToInt32(Dr["Unit_Price"].ToString());
                    }

                    if (Dr["Vat_Unit"] != DBNull.Value)
                    {
                        TheConsommation.Vat_Unit = Convert.ToInt32(Dr["Vat_Unit"].ToString());
                    }

                    if (Dr["Company_Key"] != DBNull.Value)
                    {
                        TheConsommation.Company_Key = Convert.ToInt32(Dr["Company_Key"].ToString());
                    }

                    if (Dr["Article_Name"] != DBNull.Value)
                    {
                        string DocName = Dr["Article_Name"].ToString();
                        TheConsommation.Article_Name = HttpUtility.HtmlDecode(DocName);
                    }


                    if (Dr["Date_Consumption"] != DBNull.Value)
                    {
                        TheConsommation.Date_Consumption = Convert.ToDateTime(Dr["Date_Consumption"].ToString());
                    }

                    TheListConsommation.Add(TheConsommation);
                }
                return TheListConsommation;
            }

            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao26");  // Erreur lors du chargement du la liste Consommation !
                Except.ErrorLevel = 2;
                Except.ErrorDetail = ex.ToString();
                Except.ErrorClass = "C_ConsomDal.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_ConsomDal.GetListConsommation()";
                Except.ErrorDate = DateTime.Now;
                Except.StoredProcedure = "PS_Get_Consommation_By_UserID";
                Except.InsertErroToLogFile();
                throw Except;
            }
        }
        #endregion

        #region ------------------------Insert Consommation --------------------
        /// <summary>
        /// Insert Consommation
        /// </summary>
        /// <param name="C_Consommation"></param>
        /// <returns> true / false</returns>
        public int InsertConsommation(C_Consom TheClientConsommation)
        {
            int nRetour = 0;
            try
            {
                Connection = new Oracle.ManagedDataAccess.Client.OracleConnection();

                Connection.ConnectionString = C_Functionglobal.getOracleConnection();
                //Connection.ConnectionString = FunctionGlobals.getOracleConnectionFromIniParameter();
                using (Oracle.ManagedDataAccess.Client.OracleCommand Cmd = new Oracle.ManagedDataAccess.Client.OracleCommand("Insert_Consommation.PS_Insert_Consommation", Connection))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Oracle.ManagedDataAccess.Client.OracleParameter parm1 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_Link_User_ID", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm1.Value = TheClientConsommation.Link_User_ID;
                    Cmd.Parameters.Add(parm1);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm2 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_Link_Prod_ID", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm2.Value = TheClientConsommation.Link_Prod_ID;
                    Cmd.Parameters.Add(parm2);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm3 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_Quantity", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm3.Value = TheClientConsommation.Quantity;
                    Cmd.Parameters.Add(parm3);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm4 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_Unit_Price", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm4.Value = TheClientConsommation.Unit_Price;
                    Cmd.Parameters.Add(parm4);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm5 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_Vat_Unit", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm5.Value = TheClientConsommation.Vat_Unit;
                    Cmd.Parameters.Add(parm5);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm6 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_Company_Key", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm6.Value = TheClientConsommation.Company_Key;
                    Cmd.Parameters.Add(parm6);



                    Oracle.ManagedDataAccess.Client.OracleParameter parm7 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_Article_Name", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2, 256);
                    parm7.Size = 256;
                    parm7.Value = TheClientConsommation.Article_Name;
                    Cmd.Parameters.Add(parm7);

                    //OracleParameter parm8 = new OracleParameter("I_Date_Consumption", OracleDbType.Date);
                    //parm8.Value = TheClientConsommation.Date_Consumption;
                    //Cmd.Parameters.Add(parm8);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm9 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_DOCPATH", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2, 256);
                    parm9.Value = TheClientConsommation.DOCPATH;
                    Cmd.Parameters.Add(parm9);

                    Oracle.ManagedDataAccess.Client.OracleParameter parmResult = new Oracle.ManagedDataAccess.Client.OracleParameter("I_InsertedConsumptionID", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    // SqlParameter parm9 = new SqlParameter("@ConsommationInserted", SqlDbType.VarChar);
                    //parm9.Size = 100;
                    parmResult.Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add(parmResult);
                    Connection.Open();
                    Cmd.ExecuteNonQuery();
                    Connection.Close();

                    nRetour = Convert.ToInt32(Cmd.Parameters["I_InsertedConsumptionID"].Value.ToString());
                    return nRetour;
                }
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao14"); // Erreur lors l’ajout de la consommation !. 
                Except.ErrorLevel = 2;
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_ConsomDal.cs";
                Except.ErrorNumber = 15;
                Except.ErrorMethod = "C_ConsomDal.InsertConsommation()";
                Except.ErrorDate = DateTime.Now;
                Except.StoredProcedure = "PS_Insert_Consommation";
                Except.InsertErroToLogFile();
                throw Except;
            }
        }

        #endregion

        #region -----------------------Get membership end life time ---------------------
        /// <summary>
        /// Get membership end life time
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public DateTime GetMspEndDate(int UserID)
        {
            DateTime nRetour;
            try
            {
                Connection = new Oracle.ManagedDataAccess.Client.OracleConnection();
                Connection.ConnectionString = C_Functionglobal.getOracleConnection();
                //Connection.ConnectionString = FunctionGlobals.getOracleConnectionFromIniParameter();
                using (Oracle.ManagedDataAccess.Client.OracleCommand Cmd = new Oracle.ManagedDataAccess.Client.OracleCommand("Ps_Get_Msp_Date_end", Connection))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Oracle.ManagedDataAccess.Client.OracleParameter parm1 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_UserID", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm1.Value = UserID;
                    Cmd.Parameters.Add(parm1);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm9 = new Oracle.ManagedDataAccess.Client.OracleParameter("return_value", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2, 200);
                    parm9.Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add(parm9);
                    Connection.Open();
                    Cmd.ExecuteNonQuery();
                    Connection.Close();

                    nRetour = Convert.ToDateTime(Cmd.Parameters["return_value"].Value.ToString());
                    return nRetour;
                }
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao28"); // Erreur lors de la vérification de la date de l'abonnement !
                Except.ErrorLevel = 2;
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_ConsomDal.cs";
                Except.ErrorNumber = 13;
                Except.ErrorMethod = "C_ConsomDal.GetMspEndDate();";
                Except.ErrorDate = DateTime.Now;
                Except.StoredProcedure = "Ps_Get_Msp_Date_end";
                throw Except;
            }
        }
        #endregion

        #region -----------------------Get membership remaining tokens ---------------------
        /// <summary>
        /// Get membership remaining tokens
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int GetMspRemainigTokens(int UserID)
        {
            int nRetour = 0;
            try
            {
                Connection = new Oracle.ManagedDataAccess.Client.OracleConnection();
                Connection.ConnectionString = C_Functionglobal.getOracleConnection();
                //Connection.ConnectionString = FunctionGlobals.getOracleConnectionFromIniParameter();
                using (Oracle.ManagedDataAccess.Client.OracleCommand Cmd = new Oracle.ManagedDataAccess.Client.OracleCommand("Ps_Get_Remainig_Tokens", Connection))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;

                    Oracle.ManagedDataAccess.Client.OracleParameter parm1 = new Oracle.ManagedDataAccess.Client.OracleParameter("I_UserID", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm1.Value = UserID;
                    Cmd.Parameters.Add(parm1);

                    Oracle.ManagedDataAccess.Client.OracleParameter parm9 = new Oracle.ManagedDataAccess.Client.OracleParameter("return_value", Oracle.ManagedDataAccess.Client.OracleDbType.Int32);
                    parm9.Direction = ParameterDirection.Output;
                    Cmd.Parameters.Add(parm9);
                    Connection.Open();
                    Cmd.ExecuteNonQuery();
                    Connection.Close();

                    nRetour = Convert.ToInt32(Cmd.Parameters["return_value"].Value.ToString());
                    return nRetour;
                }
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrDosInd28"); // Erreur lors de la vérification des jetons restants !
                Except.ErrorLevel = 2;
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_ConsomDal.cs";
                Except.ErrorNumber = 13;
                Except.ErrorMethod = "C_ConsomDal.GetMspRemainigTokens();";
                Except.ErrorDate = DateTime.Now;
                Except.StoredProcedure = "Ps_Get_Remainig_Tokens";
                throw Except;
            }
        }
        #endregion

        #endregion

        #region ----------------- Close -----------------

        public void Close()
        {
            Connection.Close();
            Connection.Dispose();
        }
        #endregion
    }
}
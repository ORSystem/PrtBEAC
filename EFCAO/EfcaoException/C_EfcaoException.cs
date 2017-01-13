using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EFCAO.EfcaoException
{
    public class C_EfcaoException : System.Exception
    {
        #region ------------------ Variables Membres -----------------------

        private string _ErrorModel;
        private short _ErrorNumber;
        private string _ErrorMessage;
        private string _ErrorDetail;
        private string _ErrorClass;
        private string _ErrorMethod;
        private short _ErrorLevel;
        private DateTime _ErrorDate;
        private string _StoredProcedure;


        #endregion

        #region ------------------ Accesseurs --------------------------
        public string ErrorModel
        {
            get { return _ErrorModel; }
            set { _ErrorModel = value; }
        }
        public short ErrorNumber
        {
            get { return _ErrorNumber; }
            set { _ErrorNumber = value; }
        }
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }
        public string ErrorDetail
        {
            get { return _ErrorDetail; }
            set { _ErrorDetail = value; }
        }
        public string ErrorClass
        {
            get { return _ErrorClass; }
            set { _ErrorClass = value; }
        }
        public string ErrorMethod
        {
            get { return _ErrorMethod; }
            set { _ErrorMethod = value; }
        }
        public short ErrorLevel
        {
            get { return _ErrorLevel; }
            set { _ErrorLevel = value; }
        }
        public DateTime ErrorDate
        {
            get { return _ErrorDate; }
            set { _ErrorDate = value; }
        }

        public string StoredProcedure
        {
            get { return _StoredProcedure; }
            set { _StoredProcedure = value; }
        }

        #endregion

        #region ------------------Construct ------------------
        /// <summary>
        /// Constructeur ProjectTrackerException
        /// </summary>       
        public C_EfcaoException()
        {
            ErrorModel = _ErrorModel;
            ErrorDate = _ErrorDate;
            ErrorNumber = _ErrorNumber;
            ErrorLevel = _ErrorLevel;
            ErrorMessage = _ErrorMessage;
            ErrorClass = _ErrorClass;
            ErrorMethod = _ErrorMethod;
            ErrorDetail = _ErrorDetail;
        }

        #endregion

        #region ------------------Methods ------------------
        public void InsertErroToLogFile()
        {
            try
            {
                using (StreamWriter ErrorData = new StreamWriter(HttpContext.Current.Server.MapPath("~/ErrorFile.log"), true))
                {
                    //PortailWebAnaDefi.exe;;2;09/10/2013 10:12:18;;2;1;AnadefiDal.GetListPays; Error Base Ades Constructor / GetListPays AnaDefiDal.cs;xxx;

                    string Error = this.ErrorModel + ";" + ";" + this.ErrorLevel + ";" + ErrorDate.ToString() + ";" + ";" + this.ErrorLevel + ";" + this.ErrorNumber.ToString() + ";" + ErrorMethod + ";" + this._ErrorDetail + ";" + ErrorClass;
                    ErrorData.WriteLine(Error); // Write the file.
                }
            }
            catch (SystemException ex)
            {
                string j = ex.ToString();
            }
        }
        #endregion
    }
}
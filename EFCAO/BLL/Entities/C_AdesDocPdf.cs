using EFCAO.DAL;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFCAO.BLL.Entities
{
    public class C_AdesDocPdf
    {
        #region -----------------------------Variables membres-----------------------------

        private string _PdfFile;
        private int _PdfID;
        private string _PdfLabel;
        private bool _PdfIsDefault;
        private string _PdfModel;
        private string _PdfSection;
        private string _PdfWeight;

        #endregion

        #region -----------------------------Accesseurs-----------------------------

        public string PdfFile
        {
            get { return _PdfFile; }
            set { _PdfFile = value; }
        }
        public int PdfID
        {
            get { return _PdfID; }
            set { _PdfID = value; }
        }
        public string PdfLabel
        {
            get { return _PdfLabel; }
            set { _PdfLabel = value; }
        }
        public bool PdfIsDefault
        {
            get { return _PdfIsDefault; }
            set { _PdfIsDefault = value; }
        }
        public string PdfModel
        {
            get { return _PdfModel; }
            set { _PdfModel = value; }
        }
        public string PdfSection
        {
            get { return _PdfSection; }
            set { _PdfSection = value; }
        }
        public string PdfWeight
        {
            get { return _PdfWeight; }
            set { _PdfWeight = value; }
        }

        #endregion

        #region ---------------------Documents Get Documents Private-----------------------
        /// <summary>
        /// Create Xml Doc
        /// </summary>
        /// <param name="companyKey"></param>
        /// <param name="balanceKeys"></param>
        /// <param name="DocumentsRef"></param>
        /// <return>True/False</return>
        public bool DocumentsGetDocumentsCreateXmlDoc(UInt64 companyKey, ulong[] balanceKeys, string[] DocumentsRef, string DocumentName)
        {
            bool result = false;
            try
            {

                C_PdfDal TheEfcaoPdfDal = new C_PdfDal();
                result = TheEfcaoPdfDal.DocumentsGetDocumentsCreateXmlDoc(companyKey, balanceKeys, DocumentsRef, DocumentName);
            }

            catch (C_EfcaoException)
            {
                throw;
            }

            catch (SystemException)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Create Pdf document
        /// </summary>
        /// <return>True/False</return>
        public bool DocumentsGetDocumentsCreatePdfAndAddContentFromXml(int UserID, string ProductName, string CompanyName)
        {
            bool result = false;
            C_PdfDal TheEfcaoPdfDal = new C_PdfDal();

            try
            {
                result = TheEfcaoPdfDal.DocumentsGetDocumentsCreatePdfAndAddContentFromXml(UserID, ProductName, CompanyName);
            }
            catch (C_EfcaoException)
            {
                throw;
            }

            catch (SystemException)
            {
                throw;
            }
            return result;
        }

        #endregion

        #region ---------------------Documents Get Documents Public-----------------------
        /// <summary>
        /// Create Xml Doc
        /// </summary>
        /// <param name="companyKey"></param>
        /// <param name="balanceKeys"></param>
        /// <param name="DocumentsRef"></param>
        /// <return>True/False</return>
        public string DocumentsGetDocumentsCreateXmlDocPublic(UInt64 companyKey, ulong[] balanceKeys, string[] DocumentsRef, string DocumentName)
        {
            //bool result = false;
            try
            {
                C_PdfDal TheEfcaoPdfDal = new C_PdfDal();
                return TheEfcaoPdfDal.DocumentsGetDocumentsCreateXmlDocPublic(companyKey, balanceKeys, DocumentsRef, DocumentName);
            }
            catch (C_EfcaoException)
            {
                throw;
            }
            catch (SystemException)
            {
                throw;
            }
        }

        /// <summary>
        /// Create Pdf document
        /// </summary>
        /// <return>True/False</return>
        public bool DocumentsGetDocumentsCreatePdfAndAddContentFromXmlPublic(int UserID, string ProductName, string CompanyName, string xmlPath)
        {
            bool result = false;
            C_PdfDal TheEfcaoPdfDal = new C_PdfDal();

            try
            {
                result = TheEfcaoPdfDal.DocumentsGetDocumentsCreatePdfAndAddContentFromXmlPublic(UserID, ProductName, CompanyName, xmlPath);
            }
            catch (C_EfcaoException)
            {
                throw;
            }
            catch (SystemException)
            {
                throw;
            }
            return result;
        }

        #endregion
    }
}
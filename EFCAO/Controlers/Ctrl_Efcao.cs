using EFCAO.BLL.Collections;
using EFCAO.BLL.Entities;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EFCAO.Controlers
{
    public class Ctrl_Efcao
    {
        #region -----------------------------Variable-----------------------------

            #region -----------------------Consommation--------------------
            private Ctrl_Consom _TheCtrlConsom;
            #endregion

            #region ---------------------Company Management------------------

            // The list to stock the serched compny resault
            private C_ListCompanies _TheListCompany;

            // Instance of Comapny to get the company property 
            private C_Company _TheSelectedCompany;

            // The Dictionary list to stock the Company Property
            private Dictionary<string, string> _CompanyProperty;

            #endregion

            #region ---------------------Company Balance---------------------
            C_ListExercises _TheListExercieses;
            C_ListExercicesModels _TheListExercicesModels;

            #endregion

            #region --------------------Company Params----------------------
            // The Dictionary list to stock the Company Sections
            private Dictionary<string, string> _DictionaryCompanySection;

            // DataTable represent Companay Model Detail from Sections
            private DataTable _DTableCompanyModelDetailParSectionSaisie;
            private DataTable _DTableCompanyModelDetailParSectionAnalyse;

            // DataTable represent the company model detail
            private DataTable _DTableCompanyModelDetailSaisie;
            private DataTable _DTableCompanyModelDetailAnalyse;

            #endregion         

            #region ----------------Product + Document-----------------
            C_Product _TheSelectedProductToView;

            #endregion


        #endregion

        #region -----------------------------Constructeur-------------------------


        public Ctrl_Efcao()
        {
            try
            {
                #region ---------------------Company Management------------------

                TheListCompany = new C_ListCompanies();
                TheSelectedCompany = new C_Company();
                CompanyProperty = new Dictionary<string, string>();
                #endregion

                #region -----------------------Consommation--------------------
                TheCtrlConsom = new Ctrl_Consom();
                #endregion

                #region ---------------------Company Balance---------------------
                TheListExercieses = new C_ListExercises();
                TheListExercicesModels = new C_ListExercicesModels();
                #endregion

                #region ----------------Product Document-----------------

                TheSelectedProductToView = new C_Product();

                #endregion

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
        #endregion

        #region -----------------------------Accesseurs---------------------------

            #region -------------------Company Management-------------------

            public C_Company TheSelectedCompany
            {
                get { return _TheSelectedCompany; }
                set { _TheSelectedCompany = value; }
            }

            public C_ListCompanies TheListCompany
            {
                get { return _TheListCompany; }
                set { _TheListCompany = value; }
            }

            public Dictionary<string, string> CompanyProperty
            {
                get { return _CompanyProperty; }
                set { _CompanyProperty = value; }
            }

            #endregion

            #region -----------------------Company Balance------------------

            public C_ListExercises TheListExercieses
            {
                get { return _TheListExercieses; }
                set { _TheListExercieses = value; }
            }

            public C_ListExercicesModels TheListExercicesModels
            {
                get { return _TheListExercicesModels; }
                set { _TheListExercicesModels = value; }
            }

            #endregion

            #region ----------------------Company Setings-------------------


            public Dictionary<string, string> DictionaryCompanySection
            {
                get { return _DictionaryCompanySection; }
                set { _DictionaryCompanySection = value; }
            }

            public DataTable DTableCompanyModelDetailSaisie
            {
                get { return _DTableCompanyModelDetailSaisie; }
                set { _DTableCompanyModelDetailSaisie = value; }
            }

            public DataTable DTableCompanyModelDetailAnalyse
            {
                get { return _DTableCompanyModelDetailAnalyse; }
                set { _DTableCompanyModelDetailAnalyse = value; }
            }

            public DataTable DTableCompanyModelDetailParSectionSaisie
            {
                get { return _DTableCompanyModelDetailParSectionSaisie; }
                set { _DTableCompanyModelDetailParSectionSaisie = value; }
            }

            public DataTable DTableCompanyModelDetailParSectionAnalyse
            {
                get { return _DTableCompanyModelDetailParSectionAnalyse; }
                set { _DTableCompanyModelDetailParSectionAnalyse = value; }
            }
         
            #endregion

            #region -----------------------Consommation--------------------

            public Ctrl_Consom TheCtrlConsom
            {
                get { return _TheCtrlConsom; }
                set { _TheCtrlConsom = value; }
            }
            #endregion

            #region ----------------Product Document-----------------

            public C_Product TheSelectedProductToView
            {
                get { return _TheSelectedProductToView; }
                set { _TheSelectedProductToView = value; }
            }         

            #endregion

        #endregion

        #region -------------------------Company Management----------------------

            #region -------------------------Searche Company----------------------
            /// <summary>
            /// Searche Company
            /// </summary>
            /// <param name="TxtRecherche"></param>
            /// <return>CompanyList</return>
            public object SearcheCompany(string TxtRecherche)
            {
                try
                {
                    return TheListCompany.SearcheCompany(TxtRecherche);
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
            #endregion

            #region -------------------------Get Company Property in a dictonary string----------------------
            /// <summary>
            /// Get Company Property in a dictonary string
            /// </summary>
            /// <param name="CompanyKey"></param>
            /// <return>Dictonary CompanyProperty</return>
            public Dictionary<string, string> GetCompanyProperty(UInt64 CompanyKey)
            {
                // new session Company Proprety
                CompanyProperty = new Dictionary<string, string>();

                // Fill the company property with data
                try
                {
                    return TheSelectedCompany.GetCompanyProperty(CompanyKey, CompanyProperty);
                    //return CustomerList.GetCompanyProperty(CompanyKey, CompanyProperty);

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
            #endregion

        #endregion

        #region -------------------------Company Params-------------------------
        /// <summary>
        /// Get company Model and the company model value
        /// assign result to CompanyModelSaisie
        /// </summary>
        /// <param name="CompanyModel"></param>
        /// <return>string CompanyModelSaisie</return>

        public string ModelGetLinks(string CompanyModel)
        {
            try
            {
                return TheSelectedCompany.CompanyModelSaisie = TheSelectedCompany.ModelGetLinks(CompanyModel);
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

        #endregion

        #region ------------------------Company Balance--------------------------
        /// <summary>
        /// Get company's balances list / exercies list
        /// Bind the data to the GridViewExercies
        /// </summary>
        /// <param name="CompanyKey"></param>
        /// <return>Object BSList</return>

        public object GetBalanceList(UInt64 CompanyKey)
        {
            try
            {
                TheListExercieses = new C_ListExercises();
                return TheListExercieses.GetBalanceList(CompanyKey, this.TheSelectedCompany.CompanyModelSaisie);
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
        /// Get company's balances list / exercies list
        /// Bind the data to the GridViewExercies
        /// </summary>
        /// <param name="CompanyKey"></param>
        /// <return>Object BSList</return>
        public object GetExercicesModelList(UInt64 CompanyKey)
        {
            try
            {
                TheListExercicesModels = new C_ListExercicesModels();
                return TheListExercicesModels.GetExercicesModelList(CompanyKey);
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
        #endregion

        #region ---------------------Company Document Private--------------------
        
        /// <summary>
        /// Create Xml Doc
        /// </summary>
        /// <param name="balanceKeys"></param>
        /// <param name="DocumentsRef"></param>
        /// <return>True/False</return>
        public bool DocumentsGetDocumentsCreateXmlDoc(ulong[] balanceKeys, string[] DocumentsRef, string DocumentName)
        {
            bool result = false;
            C_AdesDocPdf TheDocCreateXml = new C_AdesDocPdf();

            try
            {
                result = TheDocCreateXml.DocumentsGetDocumentsCreateXmlDoc(this.TheSelectedCompany.Key, balanceKeys, DocumentsRef, DocumentName);
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
            C_AdesDocPdf TheDocCreateXml = new C_AdesDocPdf();

            try
            {
                result = TheDocCreateXml.DocumentsGetDocumentsCreatePdfAndAddContentFromXml(UserID, ProductName, CompanyName);
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

        #region ---------------------Company Document Public-----------------------
        /// <summary>
        /// Create Xml Doc
        /// </summary>
        /// <param name="balanceKeys"></param>
        /// <param name="DocumentsRef"></param>
        /// <return>True/False</return>
        public string DocumentsGetDocumentsCreateXmlDocPublic(ulong[] balanceKeys, string[] DocumentsRef, string DocumentName)
        {
            //bool result = false;
            C_AdesDocPdf TheDocCreateXml = new C_AdesDocPdf();

            try
            {
                return TheDocCreateXml.DocumentsGetDocumentsCreateXmlDocPublic(this.TheSelectedCompany.Key, balanceKeys, DocumentsRef, DocumentName);
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
            C_AdesDocPdf TheDocCreateXml = new C_AdesDocPdf();

            try
            {
                result = TheDocCreateXml.DocumentsGetDocumentsCreatePdfAndAddContentFromXmlPublic(UserID, ProductName, CompanyName, xmlPath);
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Runtime.InteropServices;
using EFCAO.BLL.Entities;
using EFCAO.BLL.Collections;
using EFCAO.CompanyManagement;
using EFCAO.BalanceManagement;
using EFCAO.DocumentManagement;
using EFCAO.ParamsManagement;
using EFCAO.CommonFunctions;
using EFCAO.DAO;
using System.Drawing;
using EFCAO.EfcaoException;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Imaging;
using System.ServiceModel;
using Microsoft.Win32.SafeHandles;
namespace EFCAO.DAL
{
    public class C_EfcaoDal : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        #region ----------------------Variables membres---------------------

            #region ---------------------Param Management--------------------------------
            private ParamsManagementClient _Params = new ParamsManagementClient();

            #endregion

            #region ---------------------Companay Management----------------------
            private CompanyManagementClient TheCompany;

            #endregion

            #region ----------------------Company Balance-------------------------

            private BalanceManagementClient _BMClient;
            private List<AdesBalanceSummary> _BSList;
            // For Params
            private BalanceManagementClient ParamBalance;

            #endregion

            #region ------------------------Documents-----------------------
            private DocumentManagementClient _TheDocument;

            #endregion

        #endregion

        #region -------------------------Accesseurs-------------------------

            #region ---------------------Param Management--------------------------------
            private ParamsManagementClient Params
            {
                get { return _Params; }
                set { _Params = value; }
            }
            #endregion

            #region -------------------Company Balance--------------------
            public BalanceManagementClient BMClient
            {
                get { return _BMClient; }
                set { _BMClient = value; }
            }

            public List<AdesBalanceSummary> BSList
            {
                get { return _BSList; }
                set { _BSList = value; }
            }


            #endregion

            #region ------------------------Documents-----------------------
            public DocumentManagementClient TheDocument
            {
                get { return _TheDocument; }
                set { _TheDocument = value; }
            }
            #endregion

        #endregion

        #region ------------------------Constructeur------------------------
            public C_EfcaoDal()
            {


                #region -----------------------Company Management------------------

                TheCompany = new CompanyManagementClient();

                #endregion

                #region ---------------------Company Balance-----------------------

                BMClient = new BalanceManagementClient();
                BSList = new List<AdesBalanceSummary>();
                ParamBalance = new BalanceManagementClient();

                #endregion

                #region ---------------------Param Management--------------------------------
                Params = new ParamsManagementClient();
                #endregion

                #region ------------------------Documents-----------------------
                TheDocument = new DocumentManagementClient();

                #endregion
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

        #region ------------------------Search---------------------------
        /// <summary>
        /// Searche Company
        /// </summary>
        /// <param name="TxtRecherche"></param>
        /// <return>object</return>
        public object SearcheCompany(string TxtRecherche, C_ListCompanies TheListCompany)
        {
            C_ListCompanies NonOrderedListCompanies = new C_ListCompanies();
            try
            {
                string User = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                string pass = ConfigurationManager.AppSettings["PasseAdes"].ToString();


                AdesCompanySummary[] trouves = null;
                EFCAO.CompanyManagement.AuthHeader header = new CompanyManagement.AuthHeader() { User = User, Password = pass };

                trouves = TheCompany.CompanySearch(ref header, TxtRecherche);

                if (trouves != null)
                {

                    foreach (var t in trouves)
                    {
                        C_Company MonComapny = new C_Company();
                        MonComapny.Nom = t.name;
                        MonComapny.IdExterne = t.mainId;
                        MonComapny.Modele = t.model;
                        MonComapny.Key = t.key;
                        MonComapny.LockType = t.lockType;
                        NonOrderedListCompanies.Add(MonComapny);
                    }
                }

                EFCAO.ParamsManagement.AuthHeader header1 = new ParamsManagement.AuthHeader() { User = User, Password = pass };
                //DossierIndividuel.ParamsManagement.AdesReference[] MyParam = Params.ParamGetNames(ref header1, "MODELES", null);
                EFCAO.ParamsManagement.AdesReference[] MyParam = Params.ModelGetNames(ref header1, null);

                foreach (C_Company MyCompany in NonOrderedListCompanies)
                {
                    foreach (var param in MyParam)
                    {
                        if (param.code == MyCompany.Modele)
                        {
                            MyCompany.ModeleLabel = MyCompany.Modele + " - " + param.label;
                        }
                    }
                }

                // Cast the collection base to genric list and order ascending
                List<C_Company> ListComp = NonOrderedListCompanies.Cast<C_Company>().ToList();

                var query =
                  from C_Company c in ListComp
                  orderby c.Nom ascending
                  select c;

                foreach (var Comp in query)
                {
                    TheListCompany.Add(Comp);
                }

                return TheListCompany;
            }
            catch (FaultException ex)
            {
                if (ex.Message == "CannotConnectServer")
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("MesgEfcao19"); // Le Serveur Ades n’est pas démarré.
                    Except.ErrorDetail = C_Functionglobal.GetObjectLanguage("MesgEfcao19"); // Le Serveur Ades n’est pas démarré.
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 01;
                    Except.ErrorMethod = "C_EfcaoDal.SearcheCompany()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                else
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao8");  // Erreur Recherche Société !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 01;
                    Except.ErrorMethod = "C_EfcaoDal.SearcheCompany()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
            }
            catch (SystemException ex)
            {
                //string exc = ex.InnerException.Message;
                if (ex.InnerException.Message == "Impossible de se connecter au serveur distant")
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("MesgEfcao20"); // Le service web n’est pas connecté !.
                    Except.ErrorDetail = C_Functionglobal.GetObjectLanguage("MesgEfcao21"); // Le service web n’est pas connecté! / Impossible de se connecter au serveur distant
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 01;
                    Except.ErrorMethod = "C_EfcaoDal.SearcheCompany()";
                    Except.ErrorDate = DateTime.Now;
                    throw Except;
                }
                else
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao8");  // Erreur Recherche Société !. 
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 01;
                    Except.ErrorMethod = "C_EfcaoDal.SearcheCompany()";
                    Except.ErrorDate = DateTime.Now;
                    throw Except;
                }
            }
            finally
            {
                NonOrderedListCompanies = null;
            }
        }

        #endregion

        #region ---------------------Balance--------------------

            #region ---------------------Company Balance GetBalanceList---------------------
            /// <summary>
            /// Get company's balances list / exercies list
            /// Bind the data to the GridViewExercies
            /// </summary>
            /// <param name="CompanyKey"></param>
            /// <return>Object BSList</return>

            public object GetBalanceList(UInt64 CompanyKey, C_ListExercises TheListExercises, string CompanyModelSaisie)
            {
                BSList = new List<AdesBalanceSummary>();
                try
                {
                    string User = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                    string pass = ConfigurationManager.AppSettings["PasseAdes"].ToString();

                    EFCAO.BalanceManagement.AdesBalanceSummary[] summary =
                        new BalanceManagement.AdesBalanceSummary[50];

                    EFCAO.BalanceManagement.AuthHeader header = new BalanceManagement.AuthHeader() { User = User, Password = pass };
                    summary = BMClient.BalanceList(ref header, CompanyKey, null, null, null, 0, 0, false);

                    foreach (var B in summary)
                    {
                        if (B.model == CompanyModelSaisie)
                        {
                            BSList.Add(new BalanceManagement.AdesBalanceSummary()
                            {
                                key = B.key,
                                lockType = B.lockType,
                                date = B.date,
                                model = B.model,
                                duration = B.duration,
                                type = B.type,
                                currency = B.currency,
                                units = B.units,
                            });
                        }
                    }

                    BSList = BSList.OrderBy(x => Convert.ToDateTime(x.date)).ToList();
                    //BSList = BSList.OrderBy(x => Convert.ToDateTime(x.date).ToUniversalTime()).ToList();
                    //BSList.Reverse();

                    foreach (var BS in BSList)
                    {
                        C_Exercise TheExercise = new C_Exercise();
                        TheExercise.CleUnik = BS.key;
                        TheExercise.TypeExercice = BS.type;
                        TheExercise.Verrouille = BS.lockType;
                        TheExercise.DateExer = BS.date.Value.ToShortDateString();
                        TheExercise.DureeExer = BS.duration;
                        TheExercise.Model = BS.model;
                        TheExercise.Currency = BS.currency;
                        TheExercise.Unite = BS.units;
                        TheExercise.RowChecked = false;
                        TheListExercises.Add(TheExercise);
                    }
                    int numberOfExercies = TheListExercises.Count;
                    int i = 0;
                    foreach (C_Exercise TheExercise in TheListExercises)
                    {
                        i = i + 1;
                        if ((i == numberOfExercies - 2) || (i == numberOfExercies - 1) || (i == numberOfExercies))
                        {
                            TheExercise.RowChecked = true;
                        }
                    }

                    EFCAO.ParamsManagement.AuthHeader header1 = new ParamsManagement.AuthHeader() { User = User, Password = pass };
                    //DossierIndividuel.ParamsManagement.AdesReference[] MyParam = Params.ParamGetNames(ref header1, "MODELES", null);
                    EFCAO.ParamsManagement.AdesReference[] MyParam = Params.ModelGetNames(ref header1, null);

                    foreach (C_Exercise TheExercise in TheListExercises)
                    {
                        foreach (var param in MyParam)
                        {
                            if (param.code == TheExercise.Model)
                            {
                                TheExercise.Model = TheExercise.Model + " - " + param.label;
                            }
                        }
                    }
                    return TheListExercises;
                }
                catch (FaultException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao1");  // Erreur lors du chargement du balance liste !.  
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDalcs";
                    Except.ErrorNumber = 02;
                    Except.ErrorMethod = "C_EfcaoDal.GetBalanceList()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao1");  // Erreur lors du chargement du balance liste !. 
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 02;
                    Except.ErrorMethod = "C_EfcaoDal.GetBalanceList()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
               
            }
            #endregion

            #region ---------------------Company Balance GetExercicesModelList---------------------
            /// <summary>
            /// Get Exercices Model List DDListExerciesModels (Consoa - Conso / Soenta1 -Enta........)
            /// </summary>
            /// <param name="CompanyKey"></param>
            /// <param name="TheListExercicesModel"></param>
            /// <return>Object GetExercicesModelList</return>
            public object GetExercicesModelList(UInt64 CompanyKey, C_ListExercicesModels TheListExercicesModel)
            {
                try
                {
                    string User = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                    string pass = ConfigurationManager.AppSettings["PasseAdes"].ToString();

                    EFCAO.ParamsManagement.AdesProperty[] ResultedModelLinkList = new ParamsManagement.AdesProperty[50];
                    EFCAO.ParamsManagement.AuthHeader header = new ParamsManagement.AuthHeader() { User = User, Password = pass };
                    ResultedModelLinkList = Params.ModelGetLinks(ref header);

                    foreach (var ModelLink in ResultedModelLinkList)
                    {
                        C_ExercicesModel MonExercicesModel = new C_ExercicesModel();
                        MonExercicesModel.CompanyModelSaisie = ModelLink.value;
                        MonExercicesModel.CompanyModelAnalyse = ModelLink.code;
                        TheListExercicesModel.Add(MonExercicesModel);
                    }

                    return TheListExercicesModel;
                }
                catch (FaultException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao2"); // Erreur lors du chargement de la liste des modèles d’exercices !
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorNumber = 04;
                    Except.ErrorMethod = "C_EfcaoDal.GetExercicesModelList()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao2"); // Erreur lors du chargement de la liste des modèles d’exercices !
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorNumber = 04;
                    Except.ErrorMethod = "C_EfcaoDal.GetExercicesModelList()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }             
            }

            #endregion

        #endregion

        #region ---------------------Company Property--------------------
        /// Get Company Property in a dictonary string
        /// </summary>
        /// <param name="CompanyKey"></param>
        /// <return>Dictonary CompanyProperty</return>
        public Dictionary<string, string> GetCompanyProperty(UInt64 CompanyKey, Dictionary<string, string> CompanyProperty)
        {
            string User = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
            string pass = ConfigurationManager.AppSettings["PasseAdes"].ToString();
            // Fill the company property with data
            try
            {
                EFCAO.CompanyManagement.AuthHeader header = new CompanyManagement.AuthHeader() { User = User, Password = pass };
                EFCAO.CompanyManagement.AdesProperty[] props = new CompanyManagement.AdesProperty[50];
                props = TheCompany.CompanyGetProperties(ref header, CompanyKey, null);
                foreach (EFCAO.CompanyManagement.AdesProperty P in props)
                    CompanyProperty.Add(P.code, P.value);

            }
            catch (FaultException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao27"); // Erreur lors du chargement de la propriété de la société !
                Except.ErrorClass = "C_EfcaoDal.cs";
                Except.ErrorDetail = ex.Message;
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_EfcaoDal.GetCompanyProperty()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao27"); // Erreur lors du chargement de la propriété de la société !
                Except.ErrorClass = "C_EfcaoDal.cs";
                Except.ErrorDetail = ex.Message; ;
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_EfcaoDal.GetCompanyProperty()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }

            return CompanyProperty;
        }

        #endregion

        #region ---------------------Params--------------------

            #region ----------------------Model Get links--------------------
            /// <summary>
            /// Param Model get links
            /// </summary>
            /// <param name="CompanyModel"></param>
            /// <return>string CompanyModelSaisie</return>
            public string ParamsModelGetLinks(string CompanyModel)
            {
                string CompanyModelSaisie = "";
                try
                {
                    string User = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                    string pass = ConfigurationManager.AppSettings["PasseAdes"].ToString();

                    EFCAO.ParamsManagement.AdesProperty[] ResultedModelLinkList = new ParamsManagement.AdesProperty[50];
                    EFCAO.ParamsManagement.AuthHeader header = new ParamsManagement.AuthHeader() { User = User, Password = pass };
                    ResultedModelLinkList = Params.ModelGetLinks(ref header);

                    foreach (var ModelLink in ResultedModelLinkList)
                    {
                        string ExpectedResult = ModelLink.code;

                        if (CompanyModel == ExpectedResult)
                        {
                            CompanyModelSaisie = ModelLink.value;
                        }
                    }
                }
                catch (FaultException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao3");  // Erreur lors du chargement des liens ! 
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 04;
                    Except.ErrorMethod = "C_EfcaoDal.ParamsModelGetLinks()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao3");  // Erreur lors du chargement des liens !
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 04;
                    Except.ErrorMethod = "C_EfcaoDal.ParamsModelGetLinks()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
               
                return CompanyModelSaisie;
            }
            #endregion

        #endregion

        #region ---------------------Product + Documents Pdf-----------------------

            #region ------------------------Consultaion Get List Produit---------------------
            /// <summary>
            /// Get list produits
            /// </summary>
            /// <param name="C_ListProduits"></param>
            /// <returns>TheListProduit</returns>
            public object GetProductByProductID(C_Product TheProduct, int ProductID)
            {
                C_PersistanceOracle persistance = null;
                try
                {
                    persistance = new C_PersistanceOracle(C_Functionglobal.getOracleConnection());
                    string sql = "Get_Product_By_ProdID.PS_Get_Product_By_ProdID";

                    List<C_DataParam> Param = new List<C_DataParam>();
                    Param.Add(new C_DataParam("Product_ID", ProductID.ToString(), 0, "Number"));
                    Param.Add(new C_DataParam("Cur_Product", "", 0, "Cursor"));

                    DataSet ds = new DataSet();
                    ds = persistance.CallProcedureDs(sql, Param);

                    foreach (DataRow Dr in ds.Tables[0].Rows)
                    {

                        if (Dr["Prod_ID"] != DBNull.Value)
                        {
                            TheProduct.Prod_ID = Convert.ToInt32(Dr["Prod_ID"].ToString());
                        }

                        if (Dr["Prod_Name"] != DBNull.Value)
                            TheProduct.Prod_Name = Dr["Prod_Name"].ToString();

                        if (Dr["Prod_Characteristics"] != null)
                            TheProduct.Prod_Characteristics = Dr["Prod_Characteristics"].ToString();

                        if (Dr["Prod_Price"] != DBNull.Value)
                        {
                            TheProduct.Prod_Price = Convert.ToInt32(Dr["Prod_Price"].ToString());
                        }

                        if (Dr["Prod_State"] != DBNull.Value)
                        {
                            string state = Dr["Prod_State"].ToString();
                            if (state == "1")
                                TheProduct.Prod_State = true;
                            else
                                TheProduct.Prod_State = false;
                        }

                        if (Dr["Link_Type_ID"] != DBNull.Value)
                        {

                            TheProduct.Link_Type_ID = Convert.ToInt32(Dr["Link_Type_ID"].ToString());
                        }
                        if (Dr["Type_Name"] != DBNull.Value)
                        {
                            TheProduct.Type_Name = Dr["Type_Name"].ToString();
                        }

                        if (Dr["Prod_Url"] != DBNull.Value)
                        {
                            TheProduct.Prod_Url = Dr["Prod_Url"].ToString();
                        }

                        if (Dr["Prod_Reference"] != DBNull.Value)
                        {
                            TheProduct.Prod_Reference = Dr["Prod_Reference"].ToString();
                        }
                    }
                    return TheProduct;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao4"); // Erreur lors du chargement de la liste des produits !
                    Except.ErrorLevel = 2;
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 19;
                    Except.ErrorMethod = "C_EfcaoDal.GetProductByProductID()";
                    Except.ErrorDate = DateTime.Now;
                    Except.StoredProcedure = "PS_Get_Product_By_ProdID";
                    Except.InsertErroToLogFile();
                    throw Except;
                }
            }

            #endregion

            #region ------------------------Get List Document Saisie By Product ID---------------------
            /// <summary>
            /// Get List Document Saisie By Product ID
            /// </summary>
            /// <param name="C_ListProduits"></param>
            /// <returns>TheListProduit</returns>
            public object GetListDocumentSaisieByProductID(C_ListProdDocs TheListDocSaisiePdf, int ProductID, string model)
            {
                C_PersistanceOracle persistance = null;
                try
                {
                    persistance = new C_PersistanceOracle(C_Functionglobal.getOracleConnection());
                    string sql = "Get_Doc_Sais_By_Prod_ID.PS_Get_Doc_Sais_By_Prod_ID";


                    List<C_DataParam> Param = new List<C_DataParam>();
                    Param.Add(new C_DataParam("Ades_Model", model, 200, "VarChar"));
                    Param.Add(new C_DataParam("Prod_ID", ProductID.ToString(), 0, "Number"));
                    Param.Add(new C_DataParam("Cur_Doc", "", 0, "Cursor"));

                    DataSet ds = new DataSet();
                    ds = persistance.CallProcedureDs(sql, Param);

                    foreach (DataRow Dr in ds.Tables[0].Rows)
                    {
                        C_ProductDocuments TheProductDocuments = new C_ProductDocuments();

                        if (Dr["DOC_ID"] != DBNull.Value)
                            TheProductDocuments.Doc_ID = Convert.ToInt32(Dr["DOC_ID"].ToString());

                        if (Dr["LINK_PROD_ID"] != DBNull.Value)
                            TheProductDocuments.Link_Prod_ID = Convert.ToInt32(Dr["LINK_PROD_ID"].ToString());

                        if (Dr["DOC_NAME"] != DBNull.Value)
                        {
                            string DocName = Dr["DOC_NAME"].ToString();
                            TheProductDocuments.Doc_Name = HttpUtility.HtmlDecode(DocName);
                        }

                        if (Dr["DOC_CHARACTERISTICS"] != DBNull.Value)
                            TheProductDocuments.Doc_Characteristics = Dr["DOC_CHARACTERISTICS"].ToString();

                        if (Dr["ADES_DOC_ID"] != DBNull.Value)
                            TheProductDocuments.Ades_Doc_ID = Convert.ToInt32(Dr["ADES_DOC_ID"].ToString());


                        if (Dr["DOC_EDW"] != DBNull.Value)
                            TheProductDocuments.Doc_EDW = Dr["DOC_EDW"].ToString();

                        if (Dr["ADES_MODEL"] != DBNull.Value)
                            TheProductDocuments.Ades_Model = Dr["ADES_MODEL"].ToString();

                        if (Dr["DOC_TYPE"] != DBNull.Value)
                            TheProductDocuments.Doc_Type = Dr["DOC_TYPE"].ToString();

                        TheListDocSaisiePdf.Add(TheProductDocuments);
                    }
                    return TheListDocSaisiePdf;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao5");  // Erreur lors de la chargement des documents du produit Saisie !.
                    Except.ErrorLevel = 2;
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 19;
                    Except.ErrorMethod = "C_EfcaoDal.GetListDocumentSaisieByProductID()";
                    Except.ErrorDate = DateTime.Now;
                    Except.StoredProcedure = "Ps_GetListDocumentSaisieByProductID";
                    Except.InsertErroToLogFile();
                    throw Except;
                }
            }

            #endregion

            #region ------------------------Get List Document Analyse By Product ID---------------------
            /// <summary>
            /// Get List Document Analyse By Product ID
            /// </summary>
            /// <param name="C_ListProduits"></param>
            /// <returns>TheListProduit</returns>
            public object GetListDocumentAnalyseByProductID(C_ListProdDocs TheListDocAnalysePdf, int ProductID, string model)
            {
                C_PersistanceOracle persistance = null;
                try
                {
                    persistance = new C_PersistanceOracle(C_Functionglobal.getOracleConnection());
                    string sql = "Get_Doc_Analyse_By_Prod_ID.PS_Get_Doc_Analyse_By_Prod_ID";

                    List<C_DataParam> Param = new List<C_DataParam>();
                    Param.Add(new C_DataParam("Ades_Model", model, 200, "VarChar"));
                    Param.Add(new C_DataParam("Prod_ID", ProductID.ToString(), 0, "Number"));
                    Param.Add(new C_DataParam("Cur_Doc", "", 0, "Cursor"));

                    DataSet ds = new DataSet();
                    ds = persistance.CallProcedureDs(sql, Param);

                    foreach (DataRow Dr in ds.Tables[0].Rows)
                    {
                        C_ProductDocuments TheProductDocuments = new C_ProductDocuments();

                        if (Dr["DOC_ID"] != DBNull.Value)
                            TheProductDocuments.Doc_ID = Convert.ToInt32(Dr["DOC_ID"].ToString());

                        if (Dr["LINK_PROD_ID"] != DBNull.Value)
                            TheProductDocuments.Link_Prod_ID = Convert.ToInt32(Dr["LINK_PROD_ID"].ToString());

                        if (Dr["DOC_NAME"] != DBNull.Value)
                        {
                            string DocName = Dr["DOC_NAME"].ToString();
                            TheProductDocuments.Doc_Name = HttpUtility.HtmlDecode(DocName);
                        }

                        if (Dr["DOC_CHARACTERISTICS"] != DBNull.Value)
                            TheProductDocuments.Doc_Characteristics = Dr["DOC_CHARACTERISTICS"].ToString();

                        if (Dr["ADES_DOC_ID"] != DBNull.Value)
                            TheProductDocuments.Ades_Doc_ID = Convert.ToInt32(Dr["ADES_DOC_ID"].ToString());


                        if (Dr["DOC_EDW"] != DBNull.Value)
                            TheProductDocuments.Doc_EDW = Dr["DOC_EDW"].ToString();

                        if (Dr["ADES_MODEL"] != DBNull.Value)
                            TheProductDocuments.Ades_Model = Dr["ADES_MODEL"].ToString();

                        if (Dr["DOC_TYPE"] != DBNull.Value)
                            TheProductDocuments.Doc_Type = Dr["DOC_TYPE"].ToString();

                        TheListDocAnalysePdf.Add(TheProductDocuments);
                    }
                    return TheListDocAnalysePdf;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao6"); // Erreur lors de la chargement des documents du produit analyse !.
                    Except.ErrorLevel = 2;
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 19;
                    Except.ErrorMethod = "C_EfcaoDal.GetListDocumentAnalyseByProductID()";
                    Except.ErrorDate = DateTime.Now;
                    Except.StoredProcedure = "Ps_GetListDocumentAnalyseByProductID";
                    Except.InsertErroToLogFile();
                    throw Except;
                }
            }

            #endregion

        #endregion

        #region ---------------------Documents Ades Pdf-----------------------

            #region ---------------------Get List Ades Document--------------------
            /// <summary>
            /// Get List Document
            /// Assigne results to the CompanyModelDetailParSection Lsit
            /// </summary>
            /// <param name="Modelname"></param>
            /// <param name="type"></param>
            /// <returns>ListDocPdf</returns>
            public object GetListDocPdf(string ModelName, C_ListAdesDocsPdf ListDocPdf)
            {
                try
                {
                    string User = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                    string pass = ConfigurationManager.AppSettings["PasseAdes"].ToString();

                    EFCAO.DocumentManagement.AuthHeader header = new DocumentManagement.AuthHeader() { User = User, Password = pass };
                    EFCAO.DocumentManagement.AdesModelDocument[] DocumentList = TheDocument.DocumentGetList(ref header, ModelName, "");

                    if (DocumentList != null)
                    {
                        foreach (var Doc in DocumentList)
                        {
                            C_AdesDocPdf UnPdf = new C_AdesDocPdf();
                            UnPdf.PdfID = Doc.id;
                            UnPdf.PdfFile = Doc.file;
                            UnPdf.PdfLabel = Doc.label;
                            UnPdf.PdfModel = Doc.model;
                            int condition = Doc.isDefault;
                            if (condition == 0)
                            {
                                UnPdf.PdfIsDefault = false;
                            }
                            else
                            {
                                UnPdf.PdfIsDefault = true;
                            }

                            UnPdf.PdfSection = Doc.section;
                            UnPdf.PdfWeight = Doc.weight;
                            ListDocPdf.Add(UnPdf);
                        }
                    }

                    return ListDocPdf;
                }
                catch (FaultException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao7"); // Erreur lors du chargement des documents provenants d’Ades !
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal.cs";
                    Except.ErrorNumber = 10;
                    Except.ErrorMethod = "C_EfcaoDal.GetListDocPdf()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao7"); // Erreur lors du chargement des documents provenants d’Ades !
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_EfcaoDal";
                    Except.ErrorNumber = 10;
                    Except.ErrorMethod = "C_EfcaoDal.GetListDocPdf()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }              
            }
            #endregion

        #endregion

        #region ----------------- Close Web services -----------------

        public void Close()
        {
            if (_Params != null)
            {
                _Params.Close();
                _Params = null;
            }
            if (TheCompany != null)
            {
                TheCompany.Close();
                TheCompany = null;
            }
            if (_BMClient != null)
            {
                _BMClient.Close();
                _BMClient = null;
            }
            if (_BSList != null)
            {
                _BSList = null;
            }

            if (ParamBalance != null)
            {
                ParamBalance.Close();
                ParamBalance = null;
            }

            if (TheDocument != null)
            {
                TheDocument.Close();
                TheDocument = null;
            }

        }
        #endregion
    }
}
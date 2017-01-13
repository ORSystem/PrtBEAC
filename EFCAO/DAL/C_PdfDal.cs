using System;
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
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Runtime.InteropServices;
using EFCAO.BLL.Entities;
using EFCAO.BLL.Collections;
using EFCAO.CompanyManagement;
using EFCAO.BalanceManagement;
using EFCAO.DocumentManagement;
using EFCAO.CommonFunctions;
using System.Drawing;
using EFCAO.EfcaoException;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Imaging;
using System.ServiceModel;
using System.Security.AccessControl;
namespace EFCAO.DAL
{
    public class C_PdfDal
    {

        #region --------------------Variables membres-----------------------
        // Font Name default Arial
        string fontName;
        // Font size default 9
        float fontSize;
        //private CompanyManagementClient TheCompany;
        private DocumentManagementClient _TheDocument;
        private List<int> ListImage;

        #endregion

        #region -------------------------Accesseurs-------------------------

        public DocumentManagementClient TheDocument
        {
            get { return _TheDocument; }
            set { _TheDocument = value; }
        }
        #endregion

        #region ------------------------Constructeur------------------------
        public C_PdfDal()
        {
            TheDocument = new DocumentManagementClient();
            ListImage = new List<int>();
        }
        #endregion

        #region ---------------------Documents Get List Document-----------------------

        public object GetListDocPdf(string ModelName, C_ListAdesDocsPdf ListDocPdf)
        {

            try
            {
                string TheUser = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                string ThePass = ConfigurationManager.AppSettings["PasseAdes"].ToString();

                EFCAO.DocumentManagement.AuthHeader header = new DocumentManagement.AuthHeader() { User = TheUser, Password = ThePass };
                EFCAO.DocumentManagement.AdesModelDocument[] DocumentList = TheDocument.DocumentGetList(ref header, ModelName, "");

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
                return ListDocPdf;
            }
            catch (FaultException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao9"); // EDW : Erreur lors de la chargement de ADES EDW document liste !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetListDocPdf()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao9"); // EDW : Erreur lors de la chargement de ADES EDW document liste !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 10;
                Except.ErrorMethod = "C_PdfDal.GetListDocPdf()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
        }

        #endregion

        #region ---------------------Documents Public-----------------------

            #region ---------------------Documents Get Documents Create Xml Doc-----------------------
            /// <summary>
            /// Create Xml Doc
            /// </summary>
            /// <param name="companyKey"></param>
            /// <param name="balanceKeys"></param>
            /// <param name="DocumentsRef"></param>
            /// <return>True/False</return>
            public string DocumentsGetDocumentsCreateXmlDocPublic(UInt64 companyKey, ulong[] balanceKeys, string[] DocumentsRef, string DocName)
            {
                string pathXml = "";
                try
                {
                    string TheUser = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                    string ThePass = ConfigurationManager.AppSettings["PasseAdes"].ToString();

                    EFCAO.DocumentManagement.AuthHeader header = new DocumentManagement.AuthHeader() { User = TheUser, Password = ThePass };
                    string XmlText = TheDocument.DocumentGetDocument(ref header, companyKey, balanceKeys, DocumentsRef, "", "", "");

                    // New instance of the xml odcument class
                    XmlDocument XmlDoc = new XmlDocument();

                    // load the xml document from XmlText
                    XmlDoc.LoadXml(XmlText);

                    pathXml = Path.GetRandomFileName();
                    pathXml = Path.ChangeExtension(pathXml, ".xml");
                    pathXml = Path.Combine(Path.GetTempPath(), pathXml);
                    XmlDoc.Save(pathXml);  
                }
                catch (FaultException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao10"); // XML public: Erreur de création de document xml !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 04;
                    Except.ErrorMethod = "C_PdfDal.DocumentsGetDocumentsCreateXmlDoc()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (SystemException ex)
                {
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao10"); // XML public: Erreur de création de document xml !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 11;
                    Except.ErrorMethod = "C_PdfDal.DocumentsGetDocumentsCreateXmlDoc()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                return pathXml;
            }

            #endregion

            #region ----------------------------------Create the Pdf and add Content from xml -----------------------------
            /// <summary>
            /// Create Pdf document
            /// </summary>
            /// <return>True/False</return>
            public bool DocumentsGetDocumentsCreatePdfAndAddContentFromXmlPublic(int UserID, string ProductName, string CompanyName, string xmlPath)
            {
                CreatPdfImagesPublic(ProductName, xmlPath);

                bool result = false;
                // Declare a document Pdf
                Document documentPDf = null;

                // Declare a Pdf writer
                PdfWriter writer = null;
                try
                {

                    #region ----------Content Variable -------------------

                    //----------------------Xml---------------------------
                    // New instance of xml doc
                    XmlDocument xmlDoc = new XmlDocument();

                    // Get the path of xml document 
                    string root = HttpContext.Current.Server.MapPath("~");
                    
                    // Load Xml Document
                    xmlDoc.Load(xmlPath);

                    // Read Xml Document Nodes 
                    XmlNodeList PageNodes = xmlDoc.SelectNodes("//documents//EDWx/pages/page");

                    // -----------------------Page--------------------------------

                    // Declare The pdf document page size par default 210 * 297
                    float[] ThePageSize = { 210, 297 };

                    // Declare The pdf document page margin Par default 10
                    float[] PageMargin = { 10, 10, 10, 10 };

                    // ----------------------Table-----------------------------
                    // Declare a Pdf Table
                    //PdfPTable tablePdf = null;

                    // Declare the table Postion (Left, Top)
                    float[] tablePosition = { 0, 0 };

                    // Declare the table size (Width, Height) default 0 , 0
                    float[] tableSize = { 0, 0 };

                    // Declare Number of rows default 1
                    int numberOfRows = 0;

                    // Declare Number of columns default 1
                    int numberOfCol = 1;

                    int PageCount = 0;

                    #endregion

                    foreach (XmlNode Page in PageNodes)
                    {
                        // Get the page size
                        ThePageSize = GetPageSize(Page);
                        float width = ThePageSize[0];
                        float height = ThePageSize[1];
                        PageCount = PageCount + 1;

                        foreach (XmlNode theNode in Page.ChildNodes)
                        {
                            // ---------Get Page Margins-------------
                            if (theNode.Name == "margins")
                            {

                                if (PageCount == 1)
                                {
                                    // Get the page margin
                                    PageMargin = GetPageMargin(theNode);

                                    // initiate the pdf
                                    documentPDf = new Document(PageSize.A4, PageMargin[0], PageMargin[1], PageMargin[2], PageMargin[3]);

                                    if (width > height)
                                    {
                                        documentPDf.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                                    }

                                    // Get the Pdf path
                                    //string rootPdf = HttpContext.Current.Server.MapPath("~");

                                    string PathToPdf = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf"));
                                    
                                    // Get the Pdf path
                                    //string rootPdf = HttpContext.Current.Server.MapPath("~");
                                    string path1 = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf/Public"));

                                    // check if the user directory exist if not create it
                                    if (!Directory.Exists(path1))
                                    {
                                        Directory.CreateDirectory(path1);
                                    }

                                    // -------------------------------------------------------LocalHost-----------------------------------------------------------
                                    string path2 = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf/Public/" + ProductName));
                                   
                                    if (!Directory.Exists(path2))
                                    {
                                        Directory.CreateDirectory(path2);
                                    }

                                    string path3 = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf/Public/" + ProductName + "/" + CompanyName));

                                    if (!Directory.Exists(path3))
                                    {
                                        Directory.CreateDirectory(path3);
                                    }

                                    // Create a new writer to wrtie the document
                                    writer = PdfWriter.GetInstance(documentPDf, new FileStream(path3 + "/" + ProductName + "-" + CompanyName + ".pdf", FileMode.Create));
                                    // open the pdf document
                                    documentPDf.Open();
                                }
                                else
                                {
                                    if (width > height)
                                    {
                                        documentPDf.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                                    }
                                    else
                                    {
                                        documentPDf.SetPageSize(iTextSharp.text.PageSize.A4);
                                    }
                                    documentPDf.SetMargins(PageMargin[0], PageMargin[1], PageMargin[2], PageMargin[3]);
                                    documentPDf.NewPage();
                                }
                            }

                            // Get the xml Content
                            if (theNode.Name == "content")
                            {
                                foreach (XmlNode TableNode in theNode.ChildNodes)
                                {
                                    if (TableNode.Name == "table")
                                    {

                                        // ----------------- Get The number of rows------------------------
                                        numberOfRows = 1;
                                        if (GetAttributeValue(TableNode, "rows"))
                                        {
                                            numberOfRows = Convert.ToInt32(TableNode.Attributes["rows"].Value);
                                        }
                                        else
                                        {
                                            numberOfRows = 0;
                                        }
                                        //---------------- Get the number of columns--------------------
                                        numberOfCol = 1;
                                        if (GetAttributeValue(TableNode, "cols"))
                                        {
                                            numberOfCol = Convert.ToInt32(TableNode.Attributes["cols"].Value);
                                        }
                                        else
                                        {
                                            numberOfCol = 0;
                                        }

                                        // More than one row
                                        if ((numberOfRows >= 1) && (numberOfCol >= 1))
                                        {
                                            //float YyPos =0;

                                            foreach (XmlNode tableRow in TableNode.ChildNodes)
                                            {
                                                if (tableRow.Name == "row")
                                                {
                                                    foreach (XmlNode TheTableNode in tableRow.ChildNodes)
                                                    {
                                                        if (TheTableNode.Name == "table")
                                                        {
                                                            //------------------ Get the table size-------------------
                                                            GetTable(TheTableNode, ThePageSize, documentPDf, writer);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        else  // One Row only
                                        {
                                            GetTable(TableNode, ThePageSize, documentPDf, writer);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    documentPDf.Close();
                    writer.Close();
                    File.Delete(xmlPath);
                    result = true;
                }

                catch (iTextSharp.text.DocumentException ex)
                {
                    result = false;
                    documentPDf.Close();
                    documentPDf.Dispose();
                    writer.Close();
                    writer.Dispose();

                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao11"); // PDF public: Erreur de création de document PDf !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 10;
                    Except.ErrorMethod = "C_PdfDal.CreatePdfAndAddContentFromXml()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (C_EfcaoException)
                {
                    throw;
                }
                catch (SystemException ex)
                {
                    result = false;
                    documentPDf.Close();
                    documentPDf.Dispose();
                    writer.Close();
                    writer.Dispose();

                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao11"); // PDF public: Erreur de création de document PDf !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 10;
                    Except.ErrorMethod = "C_PdfDal.CreatePdfAndAddContentFromXml";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                return result;
            }
            #endregion

        #endregion

        #region ---------------------Documents Private-----------------------

            #region ---------------------Documents Get Documents Create Xml Doc-----------------------
            /// <summary>
            /// Create Xml Doc
            /// </summary>
            /// <param name="companyKey"></param>
            /// <param name="balanceKeys"></param>
            /// <param name="DocumentsRef"></param>
            /// <return>True/False</return>
            public bool DocumentsGetDocumentsCreateXmlDoc(UInt64 companyKey, ulong[] balanceKeys, string[] DocumentsRef, string DocName)
            {
                string pathXml = "";
                bool result = false;
                try
                {
                    string TheUser = ConfigurationManager.AppSettings["UtilisateurAdes"].ToString();
                    string ThePass = ConfigurationManager.AppSettings["PasseAdes"].ToString();


                    EFCAO.DocumentManagement.AuthHeader header = new DocumentManagement.AuthHeader() { User = TheUser, Password = ThePass };
                    string XmlText = TheDocument.DocumentGetDocument(ref header, companyKey, balanceKeys, DocumentsRef, "", "", "");
                    // New instance of the xml odcument class
                    XmlDocument XmlDoc = new XmlDocument();
                    // load the xml document from XmlText
                    XmlDoc.LoadXml(XmlText);

                    string root = HttpContext.Current.Server.MapPath("~");
                    pathXml = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/XML"));
                    //string path1 = Path.GetFullPath(Path.Combine(root + @"..\..\BeacBs/XML/"));
                    // ---------------------------------------------------------------------------------------------------------------------------

                    // -------------------------------------------------------Visula Studio-----------------------------------------------------------
                    //string path1 = Path.GetFullPath(Path.Combine(root + @"..\..\PortailBEAC-Portable/PortailBEAC/XML/"));
                    // ---------------------------------------------------------------------------------------------------------------------------
                  
                    // Get the xml path
                    pathXml = pathXml + "\\" + DocName + ".xml";
                    // Save the document Xml
                    XmlDoc.Save(pathXml);

                    result = true;
                }
                catch (FaultException ex)
                {
                    result = false;
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao12"); // XML : Erreur de création de document xml !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 04;
                    Except.ErrorMethod = "C_PdfDal.DocumentsGetDocumentsCreateXmlDoc()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (SystemException ex)
                {
                    result = false;
                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao12"); // XML : Erreur de création de document xml !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 11;
                    Except.ErrorMethod = "C_PdfDal.DocumentsGetDocumentsCreateXmlDoc()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                return result;
            }

            #endregion

            #region ----------------------------------Create the Pdf and add Content from xml -----------------------------
            /// <summary>
            /// Create Pdf document
            /// </summary>
            /// <return>True/False</return>
            public bool DocumentsGetDocumentsCreatePdfAndAddContentFromXml(int UserID, string ProductName, string CompanyName)
            {
                CreatPdfImages(ProductName);

                bool result = false;
                // Declare a document Pdf
                Document documentPDf = null;

                // Declare a Pdf writer
                PdfWriter writer = null;
                try
                {

                    #region ----------Content Variable -------------------

                    //----------------------Xml---------------------------
                    // New instance of xml doc
                    XmlDocument xmlDoc = new XmlDocument();

                    // Get the path of xml document 

                    string root = HttpContext.Current.Server.MapPath("~");
                    string pathXml = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/XML"));
                    //string pathXml = Path.GetFullPath(Path.Combine(root + @"..\..\BeacBs/XML/"));
                    // ---------------------------------------------------------------------------------------------------------------------------

                    // -------------------------------------------------------Visula Studio-----------------------------------------------------------
                    //string pathXml = Path.GetFullPath(Path.Combine(root + @"..\..\PortailBEAC-Portable/PortailBEAC/XML/"));
                    // ---------------------------------------------------------------------------------------------------------------------------


                    //string pathXml = HttpContext.Current.Server.MapPath("Xml");

                    // check if the user directory exist if not create it
                    //if (!Directory.Exists(pathXml))
                    //{
                    //    Directory.CreateDirectory(pathXml);
                    //}

                    // Load Xml Document
                    xmlDoc.Load(@pathXml + @"\" + ProductName + ".xml");

                    // Read Xml Document Nodes 
                    XmlNodeList PageNodes = xmlDoc.SelectNodes("//documents//EDWx/pages/page");

                    // -----------------------Page--------------------------------

                    // Declare The pdf document page size par default 210 * 297
                    float[] ThePageSize = { 210, 297 };

                    // Declare The pdf document page margin Par default 10
                    float[] PageMargin = { 10, 10, 10, 10 };

                    // ----------------------Table-----------------------------
                    // Declare a Pdf Table
                    //PdfPTable tablePdf = null;

                    // Declare the table Postion (Left, Top)
                    float[] tablePosition = { 0, 0 };

                    // Declare the table size (Width, Height) default 0 , 0
                    float[] tableSize = { 0, 0 };

                    // Declare Number of rows default 1
                    int numberOfRows = 0;

                    // Declare Number of columns default 1
                    int numberOfCol = 1;

                    int PageCount = 0;

                    #endregion

                    foreach (XmlNode Page in PageNodes)
                    {
                        // Get the page size
                        ThePageSize = GetPageSize(Page);
                        float width = ThePageSize[0];
                        float height = ThePageSize[1];
                        PageCount = PageCount + 1;


                        foreach (XmlNode theNode in Page.ChildNodes)
                        {
                            // ---------Get Page Margins-------------
                            if (theNode.Name == "margins")
                            {

                                if (PageCount == 1)
                                {
                                    // Get the page margin
                                    PageMargin = GetPageMargin(theNode);

                                    // initiate the pdf
                                    documentPDf = new Document(PageSize.A4, PageMargin[0], PageMargin[1], PageMargin[2], PageMargin[3]);

                                    if (width > height)
                                    {
                                        documentPDf.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                                    }

                                    // Get the Pdf path
                                    //string rootPdf = HttpContext.Current.Server.MapPath("~");

                                    string PathToPdf = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf"));
                                    

                                    // Get the Pdf path
                                    //string rootPdf = HttpContext.Current.Server.MapPath("~");
                                    string path1 = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf/" + UserID));
                                    //string path1 = Path.GetFullPath(Path.Combine(root + @"..\..\BeacBs/Pdf/" + UserID));
                                    // ---------------------------------------------------------------------------------------------------------------------------

                                    // -------------------------------------------------------Visula Studio-----------------------------------------------------------
                                    //string path1 = Path.GetFullPath(Path.Combine(root + @"..\..\PortailBEAC-Portable/PortailBEAC/Pdf/" + UserID));
                                    // ---------------------------------------------------------------------------------------------------------------------------

                                    // check if the user directory exist if not create it
                                    if (!Directory.Exists(path1))
                                    {
                                        Directory.CreateDirectory(path1);
                                    }

                                    // -------------------------------------------------------LocalHost-----------------------------------------------------------
                                    string path2 = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf/" + UserID + "/" + ProductName));
                                    //string path2 = Path.GetFullPath(Path.Combine(root + @"..\..\BeacBs/Pdf/" + UserID + "/" + DocName));
                                    // 

                                    // -------------------------------------------------------Visula Studio-----------------------------------------------------------
                                    //string path2 = Path.GetFullPath(Path.Combine(root + @"..\..\PortailBEAC-Portable/PortailBEAC/Pdf/" + UserID + "/" + DocName));
                                    // ---------------------------------------------------------------------------------------------------------------------------


                                    if (!Directory.Exists(path2))
                                    {
                                        Directory.CreateDirectory(path2);
                                    }

                                    string path3 = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf/" + UserID + "/" + ProductName + "/" + CompanyName));

                                    if (!Directory.Exists(path3))
                                    {
                                        Directory.CreateDirectory(path3);
                                    }

                                    // Create a new writer to wrtie the document
                                    writer = PdfWriter.GetInstance(documentPDf, new FileStream(path3 + "/" + ProductName + "-" + CompanyName + ".pdf", FileMode.Create));
                                    // open the pdf document
                                    documentPDf.Open();
                                }
                                else
                                {
                                    if (width > height)
                                    {
                                        documentPDf.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                                    }
                                    else
                                    {
                                        documentPDf.SetPageSize(iTextSharp.text.PageSize.A4);
                                    }
                                    documentPDf.SetMargins(PageMargin[0], PageMargin[1], PageMargin[2], PageMargin[3]);
                                    documentPDf.NewPage();
                                }

                            }

                            // Get the xml Content
                            if (theNode.Name == "content")
                            {
                                foreach (XmlNode TableNode in theNode.ChildNodes)
                                {
                                    if (TableNode.Name == "table")
                                    {

                                        // ----------------- Get The number of rows------------------------
                                        numberOfRows = 1;
                                        if (GetAttributeValue(TableNode, "rows"))
                                        {
                                            numberOfRows = Convert.ToInt32(TableNode.Attributes["rows"].Value);

                                        }
                                        else
                                        {
                                            numberOfRows = 0;
                                        }
                                        //---------------- Get the number of columns--------------------
                                        numberOfCol = 1;
                                        if (GetAttributeValue(TableNode, "cols"))
                                        {
                                            numberOfCol = Convert.ToInt32(TableNode.Attributes["cols"].Value);

                                        }
                                        else
                                        {
                                            numberOfCol = 0;
                                        }

                                        // More than one row
                                        if ((numberOfRows >= 1) && (numberOfCol >= 1))
                                        {
                                            //float YyPos =0;

                                            foreach (XmlNode tableRow in TableNode.ChildNodes)
                                            {
                                                if (tableRow.Name == "row")
                                                {
                                                    foreach (XmlNode TheTableNode in tableRow.ChildNodes)
                                                    {
                                                        if (TheTableNode.Name == "table")
                                                        {
                                                            //------------------ Get the table size-------------------
                                                            GetTable(TheTableNode, ThePageSize, documentPDf, writer);
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        else  // One Row only
                                        {
                                            GetTable(TableNode, ThePageSize, documentPDf, writer);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    documentPDf.Close();
                    writer.Close();
                    result = true;
                }
                catch (iTextSharp.text.DocumentException ex)
                {
                    result = false;
                    documentPDf.Close();
                    documentPDf.Dispose();
                    writer.Close();
                    writer.Dispose();

                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao13");  // PDF : Erreur de création de document PDf !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 10;
                    Except.ErrorMethod = "C_PdfDal.CreatePdfAndAddContentFromXml()";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }
                catch (C_EfcaoException)
                {
                    throw;
                }
                catch (SystemException ex)
                {
                    result = false;
                    documentPDf.Close();
                    documentPDf.Dispose();
                    writer.Close();
                    writer.Dispose();

                    C_EfcaoException Except = new C_EfcaoException();
                    Except.ErrorModel = "EFCAO.exe";
                    Except.ErrorLevel = 2;
                    Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao13");  // PDF : Erreur de création de document PDf !.
                    Except.ErrorDetail = ex.Message;
                    Except.ErrorClass = "C_PdfDal.cs";
                    Except.ErrorNumber = 10;
                    Except.ErrorMethod = "C_PdfDal.CreatePdfAndAddContentFromXml";
                    Except.ErrorDate = DateTime.Now;
                    Except.InsertErroToLogFile();
                    throw Except;
                }

                return result;
            }
            #endregion

        #endregion
        
        #region ---------------------------------Check if an Xml attribute exist ----------------------------
        /// <summary>
        /// Check if an attribute exist
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="AttrName"></param>
        /// <return>true/false</return>
        public static bool GetAttributeValue(XmlNode Node, string AttrName)
        {
            if (Node.Attributes.GetNamedItem(AttrName) != null)
            { return true; }
            else return false;
        }
        #endregion

        #region --------------------------------Get the Page Margin ---------------------------
        /// <summary>
        /// Genrate PageMArgin (Left, Right, Top, bottom)
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>float[]</return>
        private static float[] GetPageMargin(XmlNode Page)
        {
            float[] PageMargin = { 10, 10, 10, 10 };
            try
            {
                // Page left margin
                float pageMarginLeft = 0;
                // Page right margin
                float pageMarginRight = 0;
                // Page top margin
                float pageMarginTop = 0;
                // Page top margin
                float pageMarginBottom = 0;

                // ---------------Get Page Property------------  
                // Width
                // Page left margin
                if (GetAttributeValue(Page, "left"))
                {
                    pageMarginLeft = Single.Parse(Page.Attributes["left"].Value);
                }

                // Page right margin
                if (GetAttributeValue(Page, "right"))
                {
                    pageMarginRight = Single.Parse(Page.Attributes["right"].Value);
                }

                // Page top margin
                if (GetAttributeValue(Page, "top"))
                {
                    pageMarginTop = Single.Parse(Page.Attributes["top"].Value);
                }

                // Page bottom margin
                if (GetAttributeValue(Page, "bottom"))
                {
                    pageMarginBottom = Single.Parse(Page.Attributes["bottom"].Value);
                }

                PageMargin = new float[] { pageMarginLeft, pageMarginRight, pageMarginTop, pageMarginBottom };


            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao15"); // PDF : Erreur de lecture de page Marge";
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetPageMargin()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;

            }
            return PageMargin;

        }
        #endregion

        #region --------------------------------Get the Page Size ---------------------------
        /// <summary>
        /// Genrate PageSize
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>float[]</return>
        private static float[] GetPageSize(XmlNode Page)
        {
            float[] ThePageSize = { 210, 297 };
            try
            {
                // Page width Default value 210
                int pageWidth = 210;
                // Page width Default value 297
                int pageHieght = 297;

                // ---------------Get Page Property------------  
                // Width
                if (GetAttributeValue(Page, "width"))
                {
                    pageWidth = Convert.ToInt32(Page.Attributes["width"].Value);
                }

                // height
                if (GetAttributeValue(Page, "height"))
                {
                    pageHieght = Convert.ToInt32(Page.Attributes["height"].Value);
                }


                ThePageSize = new float[] { pageWidth, pageHieght };
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao16");  //  PDF : Erreur de lecture de la taille de la page !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetPageSize()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
            return ThePageSize;
        }
        #endregion

        #region --------------------------------Get the table from the xml node ----------------------------

        /// <summary>
        /// Get the table from the xml node
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>float[]</return>
        public void GetTable(XmlNode TableNode, float[] PageSize, Document TheDoc, PdfWriter TheWriter)
        {
            try
            {

                //DateTime debut = DateTime.Now;

                // Declare the table Postion (Left, Top)
                float[] tablePosition = { 0, 0 };

                // Declare the table size (Width, Height) default 0 , 0
                float[] tableSize = { 0, 0 };

                // Declare Number of rows default 1
                int numberOfRows = 1;

                // Declare Number of columns default 1
                int numberOfCol = 1;

                //---------------------Table Cell------------------------
                // Declare a pdf Table Cell
                PdfPCell tableCell = null;

                // Declare the table cell background color default = white
                string tableCellBackGroundColor = "#ffffff";

                // Declare table cell alignement default left
                string tableCellAlign = "Left";

                // Declare table cell padding default 0
                float[] tableCellPadding = { 0, 0, 0, 0 };

                // Declare table cell border default 0
                float[] tableCellBorder = new float[] { 0, 0, 0, 0 };

                // Declare table cell text default ""
                string tableCellText = "";

                //tableCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                Color MonColor = System.Drawing.Color.White;

                // Table Font Cell rotation
                int FontRotation = 0;

                // Table Image
                iTextSharp.text.Image TableImage = null;
                int NumeroImage = 0;

                //---------------------Table Cell Fonts------------------------
                // Declare table cell Fonts default null
                iTextSharp.text.Font tableCellFont = null;

                //------------------ Get the table postion----------------
                tablePosition = GetTablePostion(TableNode);

                //------------------ Get the table size--------------------
                tableSize = GetTableSize(TableNode);

                //------------------ Get the table alignement--------------
                tableCellAlign = "Left";
                if (GetAttributeValue(TableNode, "align"))
                {
                    tableCellAlign = TableNode.Attributes["align"].Value;
                }

                //------------------ Get the table Background color--------------
                tableCellBackGroundColor = "#ffffff";
                if (GetAttributeValue(TableNode, "background"))
                {
                    tableCellBackGroundColor = TableNode.Attributes["background"].Value;
                    MonColor = System.Drawing.ColorTranslator.FromHtml(tableCellBackGroundColor);
                }

                // ----------------- Get The number of rows------------------------
                numberOfRows = 1;
                if (GetAttributeValue(TableNode, "rows"))
                {
                    numberOfRows = Convert.ToInt32(TableNode.Attributes["rows"].Value);
                }

                //---------------- Get the number of columns--------------------
                numberOfCol = 1;
                if (GetAttributeValue(TableNode, "cols"))
                {
                    numberOfCol = Convert.ToInt32(TableNode.Attributes["cols"].Value);
                }

                // Get the image if there is one
                if (GetAttributeValue(TableNode, "idImage"))
                {
                    NumeroImage = Convert.ToInt32(TableNode.Attributes["idImage"].Value);
                }

                foreach (XmlNode tableProperty in TableNode.ChildNodes)
                {
                    // ------------------Get Table Padding---------------------
                    // Declare table cell border default 0

                    if (tableProperty.Name == "inner")
                    {
                        tableCellPadding = GetCellPadding(tableProperty);
                    }

                    // -------------Get Table border----------------------
                    if (tableProperty.Name == "bords")
                    {
                        tableCellBorder = GetCellBorders(tableProperty);
                    }

                    // -------------Get Table Font----------------------
                    if (tableProperty.Name == "font")
                    {
                        //-------------Font--------------------------
                        tableCellFont = GetFonts(tableProperty);
                    }

                    if (tableProperty.Name == "font")
                    {
                        FontRotation = GetFontsRotation(tableProperty);
                    }

                    // --------------Get table cell text--------------------
                    if (tableProperty.Name == "text")
                    {
                        tableCellText = tableProperty.InnerText;
                    }
                }

                // Creat a new table With the number of coulmn
                PdfPTable theTablePdf = new PdfPTable(1);
                // Assign Table width
                theTablePdf.TotalWidth = tableSize[0];
                // Fix the width
                theTablePdf.LockedWidth = true;

                // Check if the table has image 
                if (NumeroImage > 0)
                {
                    // Get the Ratio of TableWidth and height
                    float TWidth = tableSize[0] - (tableCellPadding[0] + tableCellPadding[1]);
                    float THeight = tableSize[1] - (tableCellPadding[2] + tableCellPadding[3]);
                    // Table Ratio
                    float RatioTableWh = TWidth / THeight;


                    // iTextSharp.text.Image TableImage
                    TableImage = GetCellImage(NumeroImage);
                    // Get Image width
                    float imgWidth = TableImage.ScaledWidth;
                    // Get Image Height
                    float imgHeight = TableImage.ScaledHeight;
                    // Image Ratio
                    float RatioImgWH = imgWidth / imgHeight;

                    float ImgHeight = 0;
                    float ImgWidht = 0;


                    if (RatioTableWh > RatioImgWH)
                    {
                        ImgWidht = THeight * RatioImgWH;
                        ImgHeight = THeight;
                    }

                    else
                    {
                        ImgWidht = THeight / RatioImgWH;
                        ImgHeight = THeight;
                    }

                    TableImage.ScaleAbsolute(ImgWidht, ImgHeight);

                    tableCell = new PdfPCell(TableImage);

                    // Add padding To cell
                    tableCell.PaddingLeft = tableCellPadding[0];
                    tableCell.PaddingRight = tableCellPadding[1];
                    tableCell.PaddingTop = tableCellPadding[2];
                    tableCell.PaddingBottom = tableCellPadding[3];


                    //Border
                    float l = Single.Parse("0");

                    if (tableCellBorder[0] == l)
                    {
                        tableCell.BorderWidthLeft = 0;
                    }
                    else
                    {
                        tableCell.BorderWidthLeft = tableCellBorder[0] / 2;
                    }

                    if (tableCellBorder[1] == l)
                    {
                        tableCell.BorderWidthRight = 0;
                    }
                    else
                    {
                        tableCell.BorderWidthRight = tableCellBorder[1] / 2;
                    }

                    if (tableCellBorder[2] == l)
                    {
                        tableCell.BorderWidthTop = 0;
                    }

                    else
                    {
                        tableCell.BorderWidthTop = tableCellBorder[2] / 2;
                    }

                    if (tableCellBorder[3] == l)
                    {
                        tableCell.BorderWidthBottom = 0;
                    }

                    else
                    {
                        tableCell.BorderWidthBottom = tableCellBorder[3] / 2;
                    }


                    // Align
                    if (tableCellAlign == "Left")
                    {
                        tableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    }

                    else if (tableCellAlign == "Center")
                    {
                        tableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else
                    {
                        tableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    theTablePdf.AddCell(tableCell);
                }

                else // no image Text simple
                {
                    // Check if there is a breake in the line
                    if (C_Functionglobal.ChecklineBreak(tableCellText))
                    {
                        // Get the text before the first break
                        string stBeforeBreak = C_Functionglobal.GetlineBeforeBreake(tableCellText);
                        // Create a new phrase with the stBeforeBreak string
                        Phrase PhResult = new Phrase(stBeforeBreak, tableCellFont);
                        // Get the text after break
                        string stAfterBreak = C_Functionglobal.GetlineAfterBreake(tableCellText);

                        for (int i = 0; i <= 5; i++)
                        {
                            if (C_Functionglobal.ChecklineBreak(stAfterBreak))
                            {
                                stBeforeBreak = C_Functionglobal.GetlineBeforeBreake(stAfterBreak);
                                Phrase Newline = new Phrase(Environment.NewLine);
                                PhResult.Add(Newline);
                                Phrase pph = new Phrase(stBeforeBreak, tableCellFont);
                                PhResult.Add(pph);


                                stAfterBreak = C_Functionglobal.GetlineAfterBreake(stAfterBreak);
                            }
                            else
                            {
                                Phrase ph1 = new Phrase(Environment.NewLine);
                                PhResult.Add(ph1);
                                Phrase ph2 = new Phrase(stAfterBreak, tableCellFont);
                                PhResult.Add(ph2);

                                break;
                            }
                        }
                        tableCell = new PdfPCell(PhResult);
                    }
                    else
                    {
                        tableCell = new PdfPCell(new Phrase(tableCellText, tableCellFont));
                    }


                    // Font rotation
                    if (FontRotation != 0)
                    {
                        tableCell.Rotation = FontRotation;
                    }

                    // Add Cell Height
                    tableCell.MinimumHeight = tableSize[1];

                    // Add padding To cell
                    tableCell.PaddingLeft = tableCellPadding[0];
                    tableCell.PaddingRight = tableCellPadding[1];
                    tableCell.PaddingTop = tableCellPadding[2];
                    tableCell.PaddingBottom = tableCellPadding[3];

                    float l = Single.Parse("0");

                    if (tableCellBorder[0] == l)
                    {
                        tableCell.BorderWidthLeft = 0;
                        //theTablePdf.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    }
                    else
                    {
                        tableCell.BorderWidthLeft = tableCellBorder[0] / 2;
                    }

                    if (tableCellBorder[1] == l)
                    {
                        tableCell.BorderWidthRight = 0;
                    }
                    else
                    {
                        tableCell.BorderWidthRight = tableCellBorder[1] / 2;
                    }

                    if (tableCellBorder[2] == l)
                    {
                        tableCell.BorderWidthTop = 0;
                    }

                    else
                    {
                        tableCell.BorderWidthTop = tableCellBorder[2] / 2;
                    }

                    if (tableCellBorder[3] == l)
                    {
                        tableCell.BorderWidthBottom = 0;
                    }

                    else
                    {
                        tableCell.BorderWidthBottom = tableCellBorder[3] / 2;
                    }

                    if (tableCellAlign == "Left")
                    {
                        tableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    }

                    else if (tableCellAlign == "Center")
                    {
                        tableCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    else
                    {
                        tableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    }

                    if (tableCellBackGroundColor != "#ffffff")
                    {
                        tableCell.BackgroundColor = new BaseColor(MonColor);
                    }

                    theTablePdf.AddCell(tableCell);
                }
                float x = Single.Parse("2,834645669291");

                float YPos = PageSize[1] * x;
                theTablePdf.WriteSelectedRows(0, -1, TheDoc.LeftMargin + tablePosition[0], YPos - tablePosition[1], TheWriter.DirectContent);

                //System.TimeSpan fin = DateTime.Now - debut;           
            }

            catch (iTextSharp.text.DocumentException ex)
            {

                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao17"); // PDF : Erreur de lecture du tableau !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetTable()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
            catch (C_EfcaoException)
            {
                throw;
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao17"); // PDF : Erreur de lecture du tableau !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetTable()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }  
        }

        #endregion

        #region --------------------------------Get the table Postion ---------------------------
        /// <summary>
        /// Genrate table Postion (Left, top)
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>float[]</return>
        private static float[] GetTablePostion(XmlNode TheTableNode)
        {
            float[] tablePosition = { 0, 0 };
            try
            {
                float x = Single.Parse("2,834645669291");
                // left Postion
                float tablePostionLeft = 0;

                //// Top Postion
                float tablePostionTop = 0;

                // Left Postion
                if (GetAttributeValue(TheTableNode, "left"))
                {
                    string s = TheTableNode.Attributes["left"].Value;
                    s = s.Replace(".", ",");

                    tablePostionLeft = Single.Parse(s);
                    tablePostionLeft = tablePostionLeft * x;
                }
                // top Psotion
                if (GetAttributeValue(TheTableNode, "top"))
                {
                    string s = TheTableNode.Attributes["top"].Value;
                    s = s.Replace(".", ",");
                    tablePostionTop = Single.Parse(s);
                    tablePostionTop = tablePostionTop * x;
                }

                tablePosition = new float[] { tablePostionLeft, tablePostionTop };

            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao18"); // PDF : Erreur de lecture de l'emplacement du tableau !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetTablePostion()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
            return tablePosition;
        }
        #endregion

        #region --------------------------------Get the table Size ---------------------------
        /// <summary>
        /// Genrate table size  (Widht, Height)
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>float[]</return>
        private static float[] GetTableSize(XmlNode TheTableNode)
        {
            float[] tableSize = { 0, 0 };
            try
            {
                float x = Single.Parse("2,834645669291");
                // width
                float tableWidth = 0;
                // Height
                float tableHeight = 0;

                if (GetAttributeValue(TheTableNode, "width"))
                {
                    string w = TheTableNode.Attributes["width"].Value;
                    w = w.Replace(".", ",");
                    tableWidth = Single.Parse(w);
                    tableWidth = C_Functionglobal.ConvertToPoint(tableWidth);
                    //tableWidth = tableWidth * x;
                }

                // Height
                if (GetAttributeValue(TheTableNode, "height"))
                {
                    string H = TheTableNode.Attributes["height"].Value;
                    H = H.Replace(".", ",");
                    tableHeight = Single.Parse(H);
                    tableHeight = C_Functionglobal.ConvertToPoint(tableHeight);
                    //tableHeight = tableHeight * x;
                }

                // ---------------Get Page Property------------  

                tableSize = new float[] { tableWidth, tableHeight };
            }

            catch (SystemException ex)
            {

                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao19"); // PDF : Erreur de lecture de la taille du tableau
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetTableSize()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;

            }
            return tableSize;
        }
        #endregion

        #region --------------------------------Get the table cell Padding ---------------------------
        /// <summary>
        /// Genrate Padding
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>ItextSharpFont</return>
        private static float[] GetCellPadding(XmlNode tableProperty)
        {
            float[] Padding = { 0, 0, 0, 0 };
            try
            {
                // ---------------Table Padding---------------------------
                // Left
                float cellPaddingLeft = 0;
                // Rgiht
                float cellPaddingRight = 0;
                // Top
                float cellPaddingTop = 0;
                // Bottom
                float cellPaddingBottom = 0;

                if (GetAttributeValue(tableProperty, "left"))
                {
                    string inn = tableProperty.Attributes["left"].Value;
                    inn = inn.Replace(".", ",");

                    cellPaddingLeft = Single.Parse(inn);
                    cellPaddingLeft = C_Functionglobal.ConvertToPoint(cellPaddingLeft);
                    cellPaddingLeft = cellPaddingLeft / 100;
                }

                if (GetAttributeValue(tableProperty, "right"))
                {
                    string inn = tableProperty.Attributes["right"].Value;
                    inn = inn.Replace(".", ",");

                    cellPaddingRight = Single.Parse(inn);

                    cellPaddingRight = C_Functionglobal.ConvertToPoint(cellPaddingRight);
                    cellPaddingRight = cellPaddingRight / 100;
                }


                if (GetAttributeValue(tableProperty, "top"))
                {
                    string inn = tableProperty.Attributes["top"].Value;
                    inn = inn.Replace(".", ",");

                    cellPaddingTop = Single.Parse(inn);
                    cellPaddingTop = C_Functionglobal.ConvertToPoint(cellPaddingTop);
                    cellPaddingTop = cellPaddingTop / 100;
                }


                if (GetAttributeValue(tableProperty, "bottom"))
                {
                    string inn = tableProperty.Attributes["bottom"].Value;
                    inn = inn.Replace(".", ",");

                    cellPaddingBottom = Single.Parse(inn);
                    cellPaddingBottom = C_Functionglobal.ConvertToPoint(cellPaddingBottom);
                    cellPaddingBottom = cellPaddingBottom / 100;
                }

                Padding = new float[] { cellPaddingLeft, cellPaddingRight, cellPaddingTop, cellPaddingBottom };

            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao20"); // PDF : Erreur de lecture de l'espacement intérieur des cellules de la table !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetCellPadding()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
            return Padding;
        }
        #endregion

        #region --------------------------------Get the table cell Borders ---------------------------
        /// <summary>
        /// Genrate Border
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>Borders</return>
        private static float[] GetCellBorders(XmlNode BodersNode)
        {
            float[] CellBorder = { 0, 0, 0, 0 };
            try
            {
                // -------------Table border---------------------
                // Left border
                float tableBorderLeft = 0;
                // right border
                float tableBorderRight = 0;
                // Bottom border
                float tableBorderBottom = 0;
                // Top border
                float tableBorderTop = 0;
                // Left 

                if (GetAttributeValue(BodersNode, "left"))
                {
                    string br = BodersNode.Attributes["left"].Value;
                    //br = br.Replace(".", ",");
                    if (C_Functionglobal.isInt(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else if (C_Functionglobal.isDecimal(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else
                    {
                        br = "1";
                    }

                    tableBorderLeft = Single.Parse(br);
                }

                // right 
                if (GetAttributeValue(BodersNode, "right"))
                {
                    string br = BodersNode.Attributes["right"].Value;
                    // br = br.Replace(".", ",");
                    if (C_Functionglobal.isInt(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else if (C_Functionglobal.isDecimal(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else
                    {
                        br = "1";
                    }
                    tableBorderRight = Single.Parse(br);
                }

                // Top
                if (GetAttributeValue(BodersNode, "top"))
                {
                    string br = BodersNode.Attributes["top"].Value;
                    //br = br.Replace(".", ",");
                    if (C_Functionglobal.isInt(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else if (C_Functionglobal.isDecimal(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else
                    {
                        br = "1";
                    }

                    tableBorderTop = Single.Parse(br);
                }

                // Bottom 
                if (GetAttributeValue(BodersNode, "bottom"))
                {
                    string br = BodersNode.Attributes["bottom"].Value;
                    if (C_Functionglobal.isInt(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else if (C_Functionglobal.isDecimal(br))
                    {
                        br = br.Replace(".", ",");
                    }
                    else
                    {
                        br = "1";
                    }

                    tableBorderBottom = Single.Parse(br);
                }

                CellBorder = new float[] { tableBorderLeft, tableBorderRight, tableBorderTop, tableBorderBottom };

            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao21");  // PDF : Erreur de lecture de la largeur des bordures de cellules
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetCellBorders()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }

            return CellBorder;
        }
        #endregion

        #region --------------------------------Get the table cell Image ---------------------------
        /// <summary>
        /// Genrate table Cell (Image)
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>float[]</return>
        private static iTextSharp.text.Image GetCellImage(int NumeroImage)
        {
            iTextSharp.text.Image MonImage = null;
            try
            {

                string root = HttpContext.Current.Server.MapPath("~");
                string pathPdfImage = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf"));
                //string pathPdfImage = Path.GetFullPath(Path.Combine(root + @"..\..\BeacBs/Pdf"));

                if (File.Exists(pathPdfImage + "/" + NumeroImage + ".jpg"))
                {
                    MonImage = iTextSharp.text.Image.GetInstance(pathPdfImage + "/" + NumeroImage + ".jpg");
                }
                else if (File.Exists(pathPdfImage + "/" + NumeroImage + ".gif"))
                {
                    MonImage = iTextSharp.text.Image.GetInstance(pathPdfImage + "/" + NumeroImage + ".gif");
                }

                else if (File.Exists(pathPdfImage + "/" + NumeroImage + ".bmp"))
                {
                    MonImage = iTextSharp.text.Image.GetInstance(pathPdfImage + "/" + NumeroImage + ".bmp");
                }

                else if (File.Exists(pathPdfImage + "/" + NumeroImage + ".png"))
                {
                    MonImage = iTextSharp.text.Image.GetInstance(pathPdfImage + "/" + NumeroImage + ".png");
                }

                else if (File.Exists(pathPdfImage + "/" + NumeroImage + ".Wmf"))
                {
                    MonImage = iTextSharp.text.Image.GetInstance(pathPdfImage + "/" + NumeroImage + ".Wmf");
                }

            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao22");  // PDF : Erreur de lecture de cellule image !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetCellImage()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
          
            return MonImage;
        }
        #endregion

        #region --------------------------------Get the table cell Font---------------------------
        /// <summary>
        /// Genrate Get Font
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>ItextSharpFont</return>
        public iTextSharp.text.Font GetFonts(XmlNode thefontNode)
        {
            // font weight default normal
            int fontWeight = 0;
            // font Decoration default normal = 0
            int fontDecoration = 0;
            // Underline
            int underline = 0;
            // Strikeout
            int fontStrikeOut = 0;
            string fontcolor = "#000000";
            Color MonColor = System.Drawing.Color.Black;

            iTextSharp.text.Font MonFont = FontFactory.GetFont("HELVETICA", 10);

            try
            {
                // Font name
                if (GetAttributeValue(thefontNode, "name"))
                {
                    fontName = thefontNode.Attributes["name"].Value;
                }
                // Font size
                if (GetAttributeValue(thefontNode, "size"))
                {
                    fontSize = Single.Parse(thefontNode.Attributes["size"].Value);
                    //double x = 2.5;
                    //float y = Convert.t(x);

                    //double pi = 1.5;
                    ////Console.WriteLine(pi);
                    //float fpi = (float)x;


                    //fontSize = fontSize - (3 / fpi);

                    //fontSize = fontSize - 
                }

                // font style
                if (GetAttributeValue(thefontNode, "bold"))
                {
                    fontWeight = Convert.ToInt32(thefontNode.Attributes["bold"].Value);
                }

                // font decoration
                if (GetAttributeValue(thefontNode, "italic"))
                {
                    fontDecoration = Convert.ToInt32(thefontNode.Attributes["italic"].Value);
                }

                // font Under line

                if (GetAttributeValue(thefontNode, "underline"))
                {
                    underline = Convert.ToInt32(thefontNode.Attributes["underline"].Value);
                }

                //------------------ Get the font color--------------

                if (GetAttributeValue(thefontNode, "color"))
                {
                    fontcolor = thefontNode.Attributes["color"].Value;
                    MonColor = System.Drawing.ColorTranslator.FromHtml(fontcolor);
                }

                //------------------ Get the font strikeout--------------
                if (GetAttributeValue(thefontNode, "strikeout"))
                {
                    fontStrikeOut = Convert.ToInt32(thefontNode.Attributes["strikeout"].Value);
                }


                fontName = fontName.ToLower();

                MonFont = FontFactory.GetFont(fontName, fontSize);

                if (MonFont.Familyname == "unknown")
                {
                    // Default font                
                    MonFont = FontFactory.GetFont("arial", fontSize);
                }


                if ((fontDecoration == 0) && (fontWeight == 0) && (underline == 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, new BaseColor(MonColor));
                }

                if ((fontDecoration > 0) && (fontWeight == 0) && (underline == 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.ITALIC, new BaseColor(MonColor));
                }

                if ((fontDecoration == 0) && (fontWeight > 0) && (underline == 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLD, new BaseColor(MonColor));
                }

                if ((fontDecoration == 0) && (fontWeight == 0) && (underline > 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.UNDERLINE, new BaseColor(MonColor));
                }

                if ((fontDecoration == 0) && (fontWeight == 0) && (underline == 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }

                if ((fontDecoration > 0) && (fontWeight > 0) && (underline == 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLDITALIC, new BaseColor(MonColor));
                }

                if ((fontDecoration > 0) && (fontWeight == 0) && (underline > 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.ITALIC | iTextSharp.text.Font.UNDERLINE, new BaseColor(MonColor));
                }

                if ((fontDecoration > 0) && (fontWeight == 0) && (underline == 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.ITALIC | iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }

                if ((fontDecoration == 0) && (fontWeight > 0) && (underline > 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE, new BaseColor(MonColor));
                }

                if ((fontDecoration == 0) && (fontWeight > 0) && (underline == 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }

                if ((fontDecoration == 0) && (fontWeight == 0) && (underline > 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }

                if ((fontDecoration > 0) && (fontWeight > 0) && (underline > 0) && (fontStrikeOut == 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLDITALIC | iTextSharp.text.Font.UNDERLINE, new BaseColor(MonColor));
                }

                if ((fontDecoration > 0) && (fontWeight > 0) && (underline == 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLDITALIC | iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }

                if ((fontDecoration > 0) && (fontWeight == 0) && (underline > 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.ITALIC | iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }

                if ((fontDecoration == 0) && (fontWeight > 0) && (underline > 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }


                if ((fontDecoration > 0) && (fontWeight > 0) && (underline > 0) && (fontStrikeOut > 0))
                {
                    MonFont = FontFactory.GetFont(fontName, fontSize, iTextSharp.text.Font.BOLDITALIC | iTextSharp.text.Font.UNDERLINE | iTextSharp.text.Font.STRIKETHRU, new BaseColor(MonColor));
                }
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao23"); // PDF : Erreur du lecture de Police de charactère !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetFonts()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;

            }
            return MonFont;
        }
        #endregion

        #region --------------------------------Get the table cell Font rotation---------------------------
        /// <summary>
        /// Font rotation
        /// </summary>
        /// <param name="XmlNode"></param>
        /// <return>ItextSharpFont</return>
        public static int GetFontsRotation(XmlNode thefontNode)
        {
            int FontRotation = 0;
            try
            {
                if (GetAttributeValue(thefontNode, "orient"))
                {
                    FontRotation = Convert.ToInt32(thefontNode.Attributes["orient"].Value);

                }
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao24");  // PDF : Erreur de lecture de rotation de fonte !.
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.GetFontsRotation()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;

            }
            return FontRotation;

        }
        #endregion

        #region --------------------------------Creat Pdf Images ---------------------------
        /// <summary>
        /// Genrate table Cell (Image)
        /// </summary>
        public void CreatPdfImages(string DocumentName)
        {
            string pathXml = "";
            string pathPdfImage = "";

            try
            {
                // New instance of xml doc
                XmlDocument xmlDoc = new XmlDocument();

                string root = HttpContext.Current.Server.MapPath("~");
                pathXml = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/XML/"));

                // Load Xml Document
                xmlDoc.Load(@pathXml + "\\" + DocumentName + ".xml");

                // Read Xml Document Nodes 
                XmlNodeList ImagesNodes = xmlDoc.SelectNodes("//documents//EDWx/images/image");

                foreach (XmlNode ImageNode in ImagesNodes)
                {
                    string imageID = "";
                    string imageType = "";
                    //string ImageString = "";
                    if (GetAttributeValue(ImageNode, "id"))
                    {
                        imageID = ImageNode.Attributes["id"].Value;
                    }
                    if (GetAttributeValue(ImageNode, "type"))
                    {
                        imageType = ImageNode.Attributes["type"].Value;
                    }

                    if (imageID.Length > 0 && imageType.Length > 0)
                    {
                        string ImageString = ImageNode.InnerText;
                        bool AddImgFlag = false;
                        bool CreateImage = false;

                        if (ListImage.Capacity == 0)
                        {
                            ListImage.Add(Convert.ToInt32(imageID));
                            CreateImage = true;
                        }

                        else
                        {
                            AddImgFlag = true;
                            foreach (int i in ListImage)
                            {
                                if (i.ToString() == imageID)
                                {
                                    AddImgFlag = false;
                                    break;
                                }
                            }

                            if (AddImgFlag)
                            {
                                ListImage.Add(Convert.ToInt32(imageID));
                                CreateImage = true;
                            }
                        }

                        if (CreateImage)
                        {
                            // Convert Base64 String to byte[]
                            byte[] imageBytes = Convert.FromBase64String(ImageString);
                            // Convert byte[] to Image
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);

                            pathPdfImage = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf"));

                            if (!Directory.Exists(pathPdfImage))
                            {
                                Directory.CreateDirectory(pathPdfImage);
                            }

                            System.Drawing.Image MyImage = System.Drawing.Image.FromStream(ms, true);


                            if (imageType.ToLower() == "jpg")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".jpg", ImageFormat.Jpeg);
                            }

                            else if (imageType.ToLower() == "gif")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".gif", ImageFormat.Gif);
                            }

                            else if (imageType.ToLower() == "bmp")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".bmp", ImageFormat.Bmp);
                            }

                            else if (imageType.ToLower() == "png")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".png", ImageFormat.Png);
                            }

                            else if (imageType.ToLower() == "Wmf")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".Wmf", ImageFormat.Wmf);
                            }

                            else
                            {
                                MyImage.Save(pathPdfImage + imageID + ".bmp", ImageFormat.Bmp);
                            }
                        }
                    }
                }
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao25") + "xml : " + pathXml + " pdf: " + pathPdfImage;
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.CreatPdfImages()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }          
        }
        #endregion

        #region --------------------------------Creat Pdf Images Public---------------------------
        /// <summary>
        /// Genrate table Cell (Image)
        /// </summary>
        public void CreatPdfImagesPublic(string DocumentName, string xmlPath)
        {

            string pathPdfImage = "";

            try
            {
                // New instance of xml doc
                XmlDocument xmlDoc = new XmlDocument();

                string root = HttpContext.Current.Server.MapPath("~");

                // Load Xml Document
                xmlDoc.Load(xmlPath);

                // Read Xml Document Nodes 
                XmlNodeList ImagesNodes = xmlDoc.SelectNodes("//documents//EDWx/images/image");


                foreach (XmlNode ImageNode in ImagesNodes)
                {
                    string imageID = "";
                    string imageType = "";
                    //string ImageString = "";
                    if (GetAttributeValue(ImageNode, "id"))
                    {
                        imageID = ImageNode.Attributes["id"].Value;
                    }
                    if (GetAttributeValue(ImageNode, "type"))
                    {
                        imageType = ImageNode.Attributes["type"].Value;
                    }

                    if (imageID.Length > 0 && imageType.Length > 0)
                    {
                        string ImageString = ImageNode.InnerText;
                        bool AddImgFlag = false;
                        bool CreateImage = false;

                        if (ListImage.Capacity == 0)
                        {
                            ListImage.Add(Convert.ToInt32(imageID));
                            CreateImage = true;
                        }

                        else
                        {
                            AddImgFlag = true;
                            foreach (int i in ListImage)
                            {
                                if (i.ToString() == imageID)
                                {
                                    AddImgFlag = false;
                                    break;
                                }
                            }

                            if (AddImgFlag)
                            {
                                ListImage.Add(Convert.ToInt32(imageID));
                                CreateImage = true;
                            }
                        }

                        if (CreateImage)
                        {
                            // Convert Base64 String to byte[]
                            byte[] imageBytes = Convert.FromBase64String(ImageString);
                            // Convert byte[] to Image
                            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                            ms.Write(imageBytes, 0, imageBytes.Length);
    
                            pathPdfImage = Path.GetFullPath(Path.Combine(root + @"..\..\" + ConfigurationManager.AppSettings["SiteName"].ToString() + "/Pdf"));

                            if (!Directory.Exists(pathPdfImage))
                            {
                                Directory.CreateDirectory(pathPdfImage);
                            }

                            System.Drawing.Image MyImage = System.Drawing.Image.FromStream(ms, true);

                            if (imageType.ToLower() == "jpg")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".jpg", ImageFormat.Jpeg);
                            }

                            else if (imageType.ToLower() == "gif")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".gif", ImageFormat.Gif);
                            }

                            else if (imageType.ToLower() == "bmp")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".bmp", ImageFormat.Bmp);
                            }

                            else if (imageType.ToLower() == "png")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".png", ImageFormat.Png);
                            }

                            else if (imageType.ToLower() == "Wmf")
                            {
                                MyImage.Save(pathPdfImage + "\\" + imageID + ".Wmf", ImageFormat.Wmf);
                            }

                            else
                            {
                                MyImage.Save(pathPdfImage + imageID + ".bmp", ImageFormat.Bmp);
                            }
                        }
                    }
                }
            }
            catch (SystemException ex)
            {
                C_EfcaoException Except = new C_EfcaoException();
                Except.ErrorModel = "EFCAO.exe";
                Except.ErrorLevel = 2;
                Except.ErrorMessage = C_Functionglobal.GetObjectLanguage("ErrEfcao25") + "xml : " + xmlPath + " pdf: " + pathPdfImage;
                Except.ErrorDetail = ex.Message;
                Except.ErrorClass = "C_PdfDal.cs";
                Except.ErrorNumber = 04;
                Except.ErrorMethod = "C_PdfDal.CreatPdfImages()";
                Except.ErrorDate = DateTime.Now;
                Except.InsertErroToLogFile();
                throw Except;
            }
        }
        #endregion      
    
    }
}
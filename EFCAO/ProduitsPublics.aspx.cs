using EFCAO.BLL.Collections;
using EFCAO.BLL.Entities;
using EFCAO.CommonFunctions;
using EFCAO.Controlers;
using EFCAO.EfcaoException;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EFCAO
{
    public partial class ProduitsPublics : System.Web.UI.Page
    {
        private Ctrl_Efcao TheCtrlEfcaoPublic;

        #region -----------------Load-----------------
        /// <summary>
        /// Page_Load
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    // Create PDF Font
                    C_Functionglobal.CreatePdfFont();

                    // crteate language

                    if (!(string.IsNullOrEmpty(Request.QueryString["Lang"])))
                    {
                        Session["SelectedCulture"] = Request.QueryString["Lang"].ToString();

                        // Get the language
                        GetObjectLanguage();
                    }

                    // Get the Product by ProductID
                    int ProductID = 0;
                    if (!(string.IsNullOrEmpty(Request.QueryString["ProduitID"])))
                    {
                        ProductID = Convert.ToInt32(Request.QueryString["ProduitID"]);
                    }

                    // Get the information Product
                    if (ProductID > 0)
                    {
                        TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                        TheCtrlEfcaoPublic.TheSelectedProductToView = new C_Product();
                        TheCtrlEfcaoPublic.TheSelectedProductToView.GetProductByProductID(ProductID);
                        LblEfcaoSubTitle.Text = TheCtrlEfcaoPublic.TheSelectedProductToView.Prod_Name;
                    }
                }
                //TBoxRecherche1.Focus();
            }
            catch (C_EfcaoException ex)
            {
                ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
            }
            catch (SystemException ex)
            {
                ViewAlert("Erreur", ex.Message, "DANGER");
            }
        }
        #endregion

        #region -----------------Language-----------------
        /// <summary>
        /// Language
        /// </summary>
        public void GetObjectLanguage()
        {
            try
            {
                string strCulture = Convert.ToString(Session["SelectedCulture"]);
                //create the culture based upon session culuture
                CultureInfo objCI = new CultureInfo(strCulture);
                Thread.CurrentThread.CurrentCulture = objCI;
                Thread.CurrentThread.CurrentUICulture = objCI;

                //Read the rsources files
                String strResourcesPath = Server.MapPath("~/bin");
                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("resource", strResourcesPath, null);

                //Title
                LblEfcaoTitle.Text = rm.GetString("LblEfcaoTitle");                         // Etats Financiers conforme aux originaux

                // Rechcerch
                LblSearchComp.Text = rm.GetString("LblSearchComp");               // Recherche de sociétés
                LkBttnSearchComp.ToolTip = rm.GetString("LkBttnSearchComp");              // Rechercher

                // Grid view title
                LblEfcaoCompTitle.Text = rm.GetString("LblEfcaoCompTitle");                 // Sociétés

                //Traduction Model error
                lblErrorTitle.Text = rm.GetString("lblErrorTitle");               // Erreur
                BttnErrorClose.InnerText = rm.GetString("BttnErrorClose");                  // Fermer

                //Traduction Model Loading
                LblLoadingTitle.Text = rm.GetString("LblLoadingTitle");                     // Chargement

                //Traduction Model Avertissement
                lblAvertisTitle.Text = rm.GetString("lblAvertisTitle");           // Avertissement
                BttnAvertisClose.InnerText = rm.GetString("BttnAvertisClose");              // Fermer

                //Traduction Model Success
                LblSuccessTitle.Text = rm.GetString("LblSuccessTitle");           // Succès
                BttnSuccessClose.InnerText = rm.GetString("BttnSuccessClose");              // Fermer


                //LblEfcaoExercies.Text = rm.GetString("LblEfcaoExercies");                 // Exercices :
                //LblEfcaoExerModel.Text = rm.GetString("LblEfcaoExerModel");               // Exercices Modèles
                LblEfcaoMod.Text = rm.GetString("LblEfcaoMod");                           // Modèles :
                LblEfcaoDocSaisie.Text = rm.GetString("LblEfcaoDocSaisie");               // Documents saisie
                LblEfcaoModeleSaisie.Text = rm.GetString("LblEfcaoModeleSaisie");         // , Modèle Saisie :
                LblEfcaoDocAnalyse.Text = rm.GetString("LblEfcaoDocAnalyse");             // Documents analyse
                LblEfcaoModeleAnalyse.Text = rm.GetString("LblEfcaoModeleAnalyse");       // , Modèle Analyse :

                //BttnEfcaoGetProdDoc.Text = rm.GetString("BttnEfcaoGetProdDoc");           // Obtenir les documents
                //BttnEfcaoSelectAll.Text = rm.GetString("BttnEfcaoSelectAll");             // Tout sélectionner
                //BttnEfcaoUnSelectAll.Text = rm.GetString("BttnEfcaoUnSelectAll");         // Tout desélectionner
                //BttnEfcaoOk.Text = rm.GetString("BttnEfcaoOk");                           // Ok
                //BttnEfcaoDownLoad.Text = rm.GetString("BttnEfcaoDownLoad");               // Télécharger
                LblCompExcericesTitle.Text = rm.GetString("LblCompExcericesTitle");                        // Liste des trois derniers exercices disponible
            }
            catch (SystemException)
            {
                ViewAlert("Erreur", "Erreur de Translation!", "DANGER");
            }
        }

        /// <summary>
        /// Get the slected language
        /// </summary>
        /// <param name=""></param>
        /// <return></return>
        public string GetObjectLanguageByResourceID(string ID)
        {
            string nRetour = "";
            try
            {
                string strCulture = Convert.ToString(Session["SelectedCulture"]);
                //create the culture based upon session culuture
                CultureInfo objCI = new CultureInfo(strCulture);
                Thread.CurrentThread.CurrentCulture = objCI;
                Thread.CurrentThread.CurrentUICulture = objCI;

                //Read the rsources files
                String strResourcesPath = Server.MapPath("~/bin");
                ResourceManager rm = ResourceManager.CreateFileBasedResourceManager("resource", strResourcesPath, null);

                nRetour = rm.GetString(ID);
            }
            catch (SystemException)
            {
                ViewAlert("Erreur", "Erreur de Translation!", "DANGER");
            }
            return nRetour;
        }
        #endregion

        #region -----------------Company Recheche-----------------

            #region -----------------Button Search Company_Click-----------------
            /// <summary>
            /// Btn Search Company_Click
            /// </summary>
            protected void LkBttnSearchComp_Click(object sender, EventArgs e)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {                  
                    // Change the value of * to % in case of SQL Server
                    // For Access DataBase leave the * And Replace % with *
                    //if (TBoxRecherche1.Value == "*" || TBoxRecherche1.Value == "")
                    //{
                    //    TBoxRecherche1.Value = "%";
                    //}

                    // Set the controler object to new object 
                    TheCtrlEfcaoPublic.TheSelectedCompany = new C_Company();
                    TheCtrlEfcaoPublic.TheListCompany = new C_ListCompanies();

                    SearchCompany();
                }

                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }

            #endregion

            #region -----------------Search Company-----------------
            /// <summary>
            /// Search Company
            /// </summary>
            public void SearchCompany()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    TheCtrlEfcaoPublic.SearcheCompany(TBoxRecherche.Text);
                    if (TheCtrlEfcaoPublic.TheListCompany.Count > 0)
                    {
                        BindSearchCompanyGridView();
                        PanelSearchCompGV.Visible = true;
                        LblEfcaoCompTitle.Text = "Sociétés ( " + TheCtrlEfcaoPublic.TheListCompany.Count + " trouvées)";
                    }
                    else
                    {
                        PanelSearchCompGV.Visible = false;
                        LblEfcaoCompTitle.Text = GetObjectLanguageByResourceID("LblEfcaoCompTitle");               // Sociétés

                        string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao04");  // La société recherchée n’existe pas                    
                        ViewAlert("Avertissement", ErrorMessage, "Warning");
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }

            #endregion

        #endregion

        #region -----------------GridView Company Methods -----------------

            #region -----------------Bind search company gridView -----------------
            /// <summary>
            /// Bind search company gridView
            /// </summary>
            public void BindSearchCompanyGridView()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    ResetCompanySearchGridView();                    
                    if (TheCtrlEfcaoPublic.TheListCompany.Count > 0)
                    {
                        SearchCompanyGridView.DataSource = TheCtrlEfcaoPublic.TheListCompany;
                        SearchCompanyGridView.DataBind();
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Search Company GridView Row Command -----------------
            /// <summary>
            /// Search Company GridView Row Command
            /// </summary>
            protected void SearchCompanyGridView_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];

                // Set the controler object to new object 
                TheCtrlEfcaoPublic.TheSelectedCompany = new C_Company();
                TheCtrlEfcaoPublic.TheListExercieses = new C_ListExercises();
                TheCtrlEfcaoPublic.TheListExercicesModels = new C_ListExercicesModels();

                try
                {
                    //SearchCompanyGridView.SelectedRowStyle.BackColor = Color.Black;  
                    if (e.CommandName.Equals("EditCompany"))
                    {
                        // Get the clicked row
                        GridViewRow row = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;

                        // get the value of the company key
                        int IDCompany;
                        Label CompanyID = (Label)SearchCompanyGridView.Rows[row.RowIndex].FindControl("CompanyID");
                        IDCompany = Convert.ToInt32(CompanyID.Text);

                        // Get the lockType
                        int lockType;
                        Label CompanylockType = (Label)SearchCompanyGridView.Rows[row.RowIndex].FindControl("CompanylockType");
                        lockType = Convert.ToInt32(CompanylockType.Text);
                        UInt64 CompanyKey = Convert.ToUInt64(IDCompany);

                        // get the value of the link button text
                        LinkButton btndetails = (LinkButton)e.CommandSource;

                        TheCtrlEfcaoPublic.TheSelectedCompany = TheCtrlEfcaoPublic.TheListCompany.FindCompany(CompanyKey);
                        TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse = TheCtrlEfcaoPublic.TheSelectedCompany.Modele;

                        //---------------------------------------------------------
                        // Get the model links
                        ModelGetLinks();

                        if (TheCtrlEfcaoPublic.TheListExercieses.Count == 0)
                        {
                            GetExercicesModelList();

                            //Get balance list
                            GetBalanceList();
                        }

                        LblModeleAnayseValue.Text = TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse;
                        LblModeleSaisieValue.Text = TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelSaisie;

                        LblMainExercicesCompName.Text = TheCtrlEfcaoPublic.TheSelectedCompany.Nom;
                        LblCompIdentifiantVal.Text = TheCtrlEfcaoPublic.TheSelectedCompany.IdExterne;

                        LblEfcaoDocSaisie.Text = GetObjectLanguageByResourceID("MesgEfcao02") + " " + TheCtrlEfcaoPublic.TheSelectedCompany.Nom;                // Documents saisie pour
                        LblEfcaoDocAnalyse.Text = GetObjectLanguageByResourceID("MesgEfcao03") + " " + TheCtrlEfcaoPublic.TheSelectedCompany.Nom;               // Documents analyse pour

                        GetCompanyProperty(CompanyKey);

                        // hide loading
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideLoading", "HideLoading();", true);

                        PanelCompAddress.Visible = true;
                        PanelSearchCompGV.Visible = false;
                        DivRecherch.Visible = false;
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Search Company GridView Row DataBound -----------------
            /// <summary>
            /// Search Company GridView Row DataBound
            /// This section is used to change the color of row on mouse over and mouse out
            /// </summary>
            protected void SearchCompanyGridView_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                try
                {
                    // change Color of the selectd row
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        e.Row.Attributes.Add("onmouseover", "self.MouseOverOldColor=this.style.backgroundColor;this.style.backgroundColor='yellow'");
                        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=self.MouseOverOldColor");
                    }
                    string GridHeaderCellText;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        GridHeaderCellText = GetObjectLanguageByResourceID("GridCompID");            // Clé Société
                        e.Row.Cells[0].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridCompLock");          // Type de verrouillage
                        e.Row.Cells[1].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridCompNom");           // Nom
                        e.Row.Cells[2].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridCompIdentifiant");   // Identifiant
                        e.Row.Cells[3].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridCompModels");        // Modèles
                        e.Row.Cells[4].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridCompAction");        // Action
                        e.Row.Cells[5].Text = GridHeaderCellText;
                    }
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Search Company GridView Page Index Changing -----------------
            /// <summary>
            /// Search Company GridView Page Index Changing
            /// </summary>
            protected void SearchCompanyGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    // Change the page index
                    BindSearchCompanyGridView();
                    SearchCompanyGridView.PageIndex = e.NewPageIndex;
                    SearchCompanyGridView.DataBind();
                }

                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region ---------------------Reset GridView Company-------------------------------
            /// <summary>
            /// Search Company GridView Page Index Changing
            /// </summary>
            public void ResetCompanySearchGridView()
            {
                // Reset Search Company GridView
                SearchCompanyGridView.DataSource = null;
                SearchCompanyGridView.DataBind();
            }
            #endregion

        #endregion

        #region ----------------------------------Company Params----------------------------------

            #region -----------------Model Links-----------------

            /// <summary>
            /// Get the Model List
            /// </summary>
            public void ModelGetLinks()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    // get Company model Saisie
                    string CompanyModelSaisie = TheCtrlEfcaoPublic.ModelGetLinks(TheCtrlEfcaoPublic.TheSelectedCompany.Modele);
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Company Property-----------------
            /// <summary>
            /// Bind the company property to the diffrent object
            /// </summary>
            /// <param name="CompanyKey"></param>
            public void GetCompanyProperty(UInt64 CompanyKey)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    TheCtrlEfcaoPublic.GetCompanyProperty(CompanyKey);

                    // Check if the company Property not null
                    if (TheCtrlEfcaoPublic.CompanyProperty != null)
                    {
                        foreach (var P in TheCtrlEfcaoPublic.CompanyProperty)
                        {
                            string Adresse;
                            if (TheCtrlEfcaoPublic.CompanyProperty.TryGetValue("Adresse1", out Adresse))
                            {
                                LblCompAddressVal.Text = Adresse;
                            }

                            string Adresse2;
                            if (TheCtrlEfcaoPublic.CompanyProperty.TryGetValue("Adresse2", out Adresse2))
                            {
                                LblCompAddressVal1.Text = Adresse2;
                            }

                            string CodePostal;
                            if (TheCtrlEfcaoPublic.CompanyProperty.TryGetValue("CodePostal", out CodePostal))
                            {
                                LblCompZipVal.Text = CodePostal;
                            }

                            string Ville;
                            if (TheCtrlEfcaoPublic.CompanyProperty.TryGetValue("Ville", out Ville))
                            {
                                LblCompRegionVal.Text = Ville;
                            }

                            string Pays;
                            if (TheCtrlEfcaoPublic.CompanyProperty.TryGetValue("Pays", out Pays))
                            {
                                LblCompRegionVal.Text = Pays;

                                //if (DDListPays.Items.Count == 0)
                                //{
                                //    DDListPays.DataSource = Le_Controleur.PaysList;
                                //    DDListPays.DataTextField = "Libelle";
                                //    DDListPays.DataValueField = "Code";
                                //    DDListPays.DataBind();
                                //    DDListPays.Attributes["style"] = "width: 170px; max-width: 170px";
                                //    foreach (System.Web.UI.WebControls.ListItem MonItem in DDListPays.Items)
                                //    {
                                //        if (MonItem.Text == Pays)
                                //        {
                                //            MonItem.Selected = true;
                                //            break;
                                //        }
                                //    }
                                //}
                            }

                            string Région;
                            if (TheCtrlEfcaoPublic.CompanyProperty.TryGetValue("Region", out Région))
                            {
                                LblCompCountryVal.Text = Région;
                                //if (DDListRégion.Items.Count == 0)
                                //{
                                //    DDListRégion.DataSource = Le_Controleur.RegionsList;
                                //    DDListRégion.DataTextField = "Libelle";
                                //    DDListRégion.DataValueField = "Code";
                                //    DDListRégion.DataBind();
                                //    DDListRégion.Attributes["style"] = "width: 170px; max-width: 170px";
                                //    foreach (System.Web.UI.WebControls.ListItem MonItem in DDListRégion.Items)
                                //    {
                                //        if (MonItem.Value == Région)
                                //        {
                                //            MonItem.Selected = true;
                                //            break;
                                //        }
                                //    }
                                //}
                            }
                        }
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

        #endregion

        #region ----------------------------------Company Balance  Exercies----------------------------------

            #region -----------------Get Balance List-----------------
            /// <summary>
            /// Get company's balances list
            /// Bind the data to the GridViewExercies
            /// </summary>
            public void GetBalanceList()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    ResetGridViewExercies();                                      
                    TheCtrlEfcaoPublic.GetBalanceList(TheCtrlEfcaoPublic.TheSelectedCompany.Key);
                    if (TheCtrlEfcaoPublic.TheListExercieses.Count > 0)
                    {
                        BindGridViewExercies();
                        PanelGridViewExceriesMain.Visible = true;
                        DivGridExercies.Visible = true;
                        DivGridExerciesTitle.Visible = true;
                        BttnGetDocImedit.Visible = true;
                    }
                    else
                    {
                        PanelGridViewExceriesMain.Visible = true;
                        DivGridExercies.Visible = false;
                        DivGridExerciesTitle.Visible = false;
                        BttnGetDocImedit.Visible = false;

                        lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao05");  // Aucun exercice la base de données.                     
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                        UpdatePanelAvertissement.Update();
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Get Exercices Model List-----------------
            /// <summary>
            /// Get Exercices Model List
            /// </summary>
            public void GetExercicesModelList()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {                   
                    // Bind the data to the GridViewExercies
                    TheCtrlEfcaoPublic.GetExercicesModelList(TheCtrlEfcaoPublic.TheSelectedCompany.Key);

                    DDListExerciesModels.DataSource = TheCtrlEfcaoPublic.TheListExercicesModels;
                    DDListExerciesModels.DataTextField = "CompanyModelAnalyse";
                    DDListExerciesModels.DataValueField = "CompanyModelSaisie";
                    DDListExerciesModels.DataBind();
                    DDListExerciesModels.ClearSelection();

                    foreach (System.Web.UI.WebControls.ListItem MonItem in DDListExerciesModels.Items)
                    {
                        if (MonItem.Text == TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse)
                        {
                            MonItem.Selected = true;
                            break;
                        }
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Dropdownlist Exercies Models Selected Index Changed-----------------
            /// <summary>
            /// Dropdownlist Exercies Models Selected Index Changed
            /// </summary>
            protected void DDListExerciesModels_SelectedIndexChanged(object sender, EventArgs e)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {                   
                    TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelSaisie = DDListExerciesModels.SelectedValue.ToString();
                    TheCtrlEfcaoPublic.TheSelectedCompany.Modele = DDListExerciesModels.SelectedItem.ToString();
                    TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse = DDListExerciesModels.SelectedItem.ToString();
                    LblModeleAnayseValue.Text = TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse;
                    LblModeleSaisieValue.Text = TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelSaisie;
                    TheCtrlEfcaoPublic.TheListExercieses = new C_ListExercises();
                    TheCtrlEfcaoPublic.TheListExercicesModels = new C_ListExercicesModels();
                    TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse = new C_ListProdDocs();
                    TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentSaisie = new C_ListProdDocs();

                    // Get balance list
                    GetBalanceList();
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

        #endregion

        #region -----------------GridView Exercies-----------------

            #region -----------------Bind GridView Exercies -----------------
            /// <summary>
            /// Bind GridView Exercies
            /// </summary>
            public void BindGridViewExercies()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    ResetGridViewExercies();                   
                    if (TheCtrlEfcaoPublic.TheListExercieses.Count > 0)
                    {
                        ExerciesGridView.DataSource = TheCtrlEfcaoPublic.TheListExercieses;
                        ExerciesGridView.DataBind();
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Exercies GridView Page Index Changing-----------------
            /// <summary>
            /// Exercies GridView Page Index Changing
            /// </summary>
            protected void ExerciesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {                 
                    ExerciesGridView.DataSource = TheCtrlEfcaoPublic.TheListExercieses;
                    ExerciesGridView.PageIndex = e.NewPageIndex;
                    ExerciesGridView.DataBind();
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Exercies GridView Row Command-----------------
            /// <summary>
            /// Exercies GridView Row Command
            /// </summary>
            protected void ExerciesGridView_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    if (e.CommandName.Equals("MyCustomCommand"))
                    {
                        // Get the clicked row
                        GridViewRow row = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;

                        Label LabelBalanceKey = (Label)ExerciesGridView.Rows[row.RowIndex].FindControl("ExerciesKey");
                        LinkButton LabelBalancedate = (LinkButton)ExerciesGridView.Rows[row.RowIndex].FindControl("LinkBtnDateBalance");

                        // Select The GridView CheckBoxes SelectedDatesCollection date
                        CheckBox DateCheckBox = (CheckBox)ExerciesGridView.Rows[row.RowIndex].FindControl("CheckDate");
                        DateCheckBox.Checked = true;
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Exercies GridView CheckBox CheckDate_CheckedChanged-----------------
            /// <summary>
            /// Exercies GridView CheckBox CheckDate_CheckedChanged
            /// </summary>
            protected void CheckDate_CheckedChanged(object sender, EventArgs e)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                TheCtrlEfcaoPublic.DTableCompanyModelDetailSaisie = new DataTable();
                TheCtrlEfcaoPublic.DTableCompanyModelDetailAnalyse = new DataTable();
                TheCtrlEfcaoPublic.DTableCompanyModelDetailParSectionAnalyse = new DataTable();
                TheCtrlEfcaoPublic.DTableCompanyModelDetailParSectionSaisie = new DataTable();
                try
                {                 
                    CheckBox DateCheckBox = (CheckBox)sender;
                    GridViewRow row = (GridViewRow)DateCheckBox.NamingContainer;

                    //GridViewRow row = ExerciesGridView.SelectedRow;
                    Label LabelBalanceKey = (Label)ExerciesGridView.Rows[row.RowIndex].FindControl("ExerciesKey");
                    C_Exercise theExercise = new C_Exercise();
                    if (DateCheckBox.Checked)
                    {
                        theExercise = TheCtrlEfcaoPublic.TheListExercieses.FindExercise(Convert.ToUInt32(LabelBalanceKey.Text));
                        theExercise.RowChecked = true;
                    }
                    else
                    {
                        theExercise = TheCtrlEfcaoPublic.TheListExercieses.FindExercise(Convert.ToUInt32(LabelBalanceKey.Text));
                        theExercise.RowChecked = false;
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------------------Reset GridView Exercies --------------------------
            /// <summary>
            /// Reset GridView Exercies
            /// </summary>
            public void ResetGridViewExercies()
            {
                // Reset GridView Exercies
                ExerciesGridView.DataSource = null;
                ExerciesGridView.DataBind();
            }

            #endregion

            #region -----------------Exercies GridView Row Data Bound -----------------
            /// <summary>
            /// GridView Exercies Row DataBound
            /// </summary>
            protected void ExerciesGridView_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                try
                {
                    string GridHeaderCellText;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesType");           // Type
                        e.Row.Cells[0].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesLock");           // Verrou
                        e.Row.Cells[1].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesDate");           // Date
                        e.Row.Cells[3].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesUnit");           // Unité
                        e.Row.Cells[4].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesCurrency");       // Devise
                        e.Row.Cells[5].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesDuration");       // Durée
                        e.Row.Cells[6].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesModels");         // Modèle
                        e.Row.Cells[7].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridExrciesKey");            // Clé
                        e.Row.Cells[8].Text = GridHeaderCellText;
                    }
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

        #endregion

        #region ----------------------------------GridView Product Documents Saisie----------------------------------

            #region -----------------Bind GridView product Douments Saisie -----------------
            /// <summary>
            /// Bind GridView product Douments Saisie
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            public void BindGridViewDoumentSaisie()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                ResetGridViesDocumentSaisie();
                GridViewDocumentSaisie.DataSource = TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentSaisie;
                GridViewDocumentSaisie.DataBind();
            }
            #endregion

            #region -----------------Reset GridView doument aaisie -----------------
            /// <summary>
            /// Reset GridView doument aaisie
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            public void ResetGridViesDocumentSaisie()
            {
                GridViewDocumentSaisie.DataSource = null;
                GridViewDocumentSaisie.DataBind();
            }
            #endregion

            #region -----------------GridView Document Saisie Page Index Changing-----------------
            /// <summary>
            /// GridView Document Saisie Page Index Changing
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void GridViewDocumentSaisie_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    // Change the page index
                    BindGridViewDoumentSaisie();
                    GridViewDocumentSaisie.PageIndex = e.NewPageIndex;
                    GridViewDocumentSaisie.DataBind();
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }

            #endregion

            #region -----------------GridView Document Saisie Row Data Bound -----------------
            /// <summary>
            /// GridView Document Saisie Row Data Bound
            /// </summary>
            protected void GridViewDocumentSaisie_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                try
                {
                    string GridHeaderCellText;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocSelect");           // Sélectionnez
                        e.Row.Cells[0].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocID");              // ID
                        e.Row.Cells[1].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocName");           // Nom du document
                        e.Row.Cells[2].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocEdw");           // Edw
                        e.Row.Cells[3].Text = GridHeaderCellText;
                    }
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

        #endregion

        #region ----------------------------------GridView Product Documents Analyse----------------------------------

            #region -----------------Bind GridView Doument Analyse -----------------
            /// <summary>
            /// Bind GridView Doument Analyse
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            public void BindGridViewDoumentAnalyse()
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                ResetGridViesDocumentAnalyse();
                GridViewDocumentAnalyse.DataSource = TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse;
                GridViewDocumentAnalyse.DataBind();
            }
            #endregion

            #region -----------------Reset GridView doument analyse -----------------
            /// <summary>
            /// Reset GridView doument analyse
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            public void ResetGridViesDocumentAnalyse()
            {
                GridViewDocumentAnalyse.DataSource = null;
                GridViewDocumentAnalyse.DataBind();
            }
            #endregion

            #region -----------------GridView Document Analyse Page Index Changing -----------------
            /// <summary>
            /// GridView Document Analyse Page Index Changing
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void GridViewDocumentAnalyse_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    // Change the page index
                    BindGridViewDoumentAnalyse();
                    GridViewDocumentAnalyse.PageIndex = e.NewPageIndex;
                    GridViewDocumentAnalyse.DataBind();
                    //BttnEfcaoOk.Focus();
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------GridView Document Analyse Row Data Bound -----------------
            /// <summary>
            /// GridView Document Analyse Row Data Bound
            /// </summary>
            protected void GridViewDocumentAnalyse_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                try
                {
                    string GridHeaderCellText;
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocSelect");           // Sélectionnez
                        e.Row.Cells[0].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocID");              // ID
                        e.Row.Cells[1].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocName");           // Nom du document
                        e.Row.Cells[2].Text = GridHeaderCellText;

                        GridHeaderCellText = GetObjectLanguageByResourceID("GridDocEdw");           // Edw
                        e.Row.Cells[3].Text = GridHeaderCellText;
                    }
                }

                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

        #endregion

        #region ----------------------------------Product Document Management----------------------------------

            #region -----------------Get List Document Saisie By Product ID-----------------
            /// <summary>
            /// Get List Document Saisie By Produc tID
            /// </summary>
            /// <param name="ProductID"></param>
            /// <return></return>
            public void GetListDocumentSaisieByProductID(string model)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];             
                try
                {
                    TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentSaisie.GetListDocumentSaisieByProductID(TheCtrlEfcaoPublic.TheSelectedProductToView.Prod_ID, model);

                    if (TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentSaisie.Count > 0)
                    {
                        BindGridViewDoumentSaisie();
                    }
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }

            }
            #endregion

            #region -----------------Get List Document Analyse By Product ID-----------------
            /// <summary>
            /// Get List Document Analyse By ProductID
            /// </summary>
            /// <param name="ProductID"></param>
            /// <return></return>
            public void GetListDocumentAnalyseByProductID(string model)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];               
                try
                {
                    TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse.GetListDocumentAnalyseByProductID(TheCtrlEfcaoPublic.TheSelectedProductToView.Prod_ID, model);

                    if (TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count > 0)
                    {
                        BindGridViewDoumentAnalyse();
                    }      
                }
                catch (C_EfcaoException ex)
                {
                    ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

        #endregion

        #region ----------------------------------Company Ades Document Management----------------------------------

            #region --------------------------xml Documents Create Xml Doc-------------------------
            /// <summary>
            /// xml Documents Create Xml Doc
            /// </summary>
            /// <param name="balanceKeys"></param>
            /// <param name="DocumentsRef"></param>
            public void DocumentsCreateXmlDoc(ulong[] balanceKeys, string[] DocumentsRef)
            {
                TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
                    int ProduitID = Convert.ToInt32(Request.QueryString["ProduitID"]);

                    // to create the xml file
                    string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcaoPublic.TheSelectedProductToView.Prod_Name);

                    // to create directory of the company name
                    string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcaoPublic.TheSelectedCompany.Nom);

                    // replace special char ("[;\\\\/:*?\"<>|&']") with _
                    if (C_Functionglobal.ValidateCompanyName(CompanyName))
                    {
                        CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                    }

                    try
                    {
                        string XmlPath = TheCtrlEfcaoPublic.DocumentsGetDocumentsCreateXmlDocPublic(balanceKeys, DocumentsRef, ProductName);
                        try
                        {
                            TheCtrlEfcaoPublic.DocumentsGetDocumentsCreatePdfAndAddContentFromXmlPublic(UserID, ProductName, CompanyName, XmlPath);
                        }
                        catch (C_EfcaoException)
                        {
                            throw;
                        }
                    }
                    catch (C_EfcaoException)
                    {
                        throw;
                    }                   
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

        #region -----------------Bttn Get document Imediatlly by default-----------------
        /// <summary>
        /// Bttn Get document Imediatlly by default-
        /// </summary>
        /// <param name=""></param>
        /// <return></return>
        protected void BttnGetDocImedit_Click(object sender, EventArgs e)
        {
            TheCtrlEfcaoPublic = (Ctrl_Efcao)Session["CtrlEfcao"];
            try
            {              
                //Get the number of selected exercises
                int NumberOFSlectedExercies = 0;

                foreach (C_Exercise MyExercise in TheCtrlEfcaoPublic.TheListExercieses)
                {
                    if (MyExercise.RowChecked)
                    {
                        NumberOFSlectedExercies = NumberOFSlectedExercies + 1;
                    }
                }

                if (NumberOFSlectedExercies > 5)
                {
                    //Max number of authorised selection is 5
                    lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao06"); // Le nombre maximum d’exercices à sélectionner est 5.; 
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                    UpdatePanelAvertissement.Update();
                }

                else
                {
                    TheCtrlEfcaoPublic.TheSelectedCompany.NumberOFSlectedExercises = NumberOFSlectedExercies;
                    TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelSaisie = DDListExerciesModels.SelectedValue.ToString();
                    TheCtrlEfcaoPublic.TheSelectedCompany.Modele = DDListExerciesModels.SelectedItem.ToString();
                    TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse = DDListExerciesModels.SelectedItem.ToString();

                    ResetGridViesDocumentAnalyse();
                    ResetGridViesDocumentSaisie();

                    //TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse = new C_ListProdDocs();
                    TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentSaisie = new C_ListProdDocs();

                    GetListDocumentAnalyseByProductID(TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse);

                    //foreach (C_ExercicesModel Xm in TheCtrlEfcaoPublic.TheListExercicesModels)
                    //{
                    //    GetListDocumentSaisieByProductID(Xm.CompanyModelSaisie);
                    //    GetListDocumentAnalyseByProductID(Xm.CompanyModelAnalyse);
                    //}

                    if (TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count == 0) 
                    {
                        lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao24");  // Aucun document de ce produit ne correspond aux modèles de cette société.
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                        UpdatePanelAvertissement.Update();
                    }
                    else
                    {
                        if (TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count > 0)
                        {
                            //int numberOFDocSaisie = TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentSaisie.Count;
                            int numberOFDocAnalyse = TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count;

                            string[] DocumentsRefAnalyse;
                            int i = 0;

                            try
                            {
                                // Create an array string that contains the Balance Keys
                                ulong[] balanceKeys = new ulong[TheCtrlEfcaoPublic.TheSelectedCompany.NumberOFSlectedExercises];

                                foreach (C_Exercise MyExercise in TheCtrlEfcaoPublic.TheListExercieses)
                                {
                                    if (MyExercise.RowChecked)
                                    {
                                        balanceKeys[i] = MyExercise.CleUnik;
                                        i = i + 1;
                                    }
                                }

                                // Create an array string that contains the reference of the Documents Analyse
                                DocumentsRefAnalyse = new string[numberOFDocAnalyse];

                                int q = 0;

                                //foreach (C_ProductDocuments theProductDocSaisie in TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentSaisie)
                                //{
                                //    string LabelWithId = TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelSaisie + "." + theProductDocSaisie.Ades_Doc_ID;
                                //    DocumentsRefSaisieAnalyse[q] = LabelWithId;
                                //    // Increment Q
                                //    q = q + 1;
                                //}

                                foreach (C_ProductDocuments theProductDocAnalyse in TheCtrlEfcaoPublic.TheSelectedProductToView.TheListProductsDocumentAnalyse)
                                {
                                    string LabelWithId = TheCtrlEfcaoPublic.TheSelectedCompany.CompanyModelAnalyse + "." + theProductDocAnalyse.Ades_Doc_ID;
                                    DocumentsRefAnalyse[q] = LabelWithId;
                                    // Increment Q
                                    q = q + 1;
                                }

                                if (numberOFDocAnalyse > 0)
                                {
                                    string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcaoPublic.TheSelectedProductToView.Prod_Name);
                                    string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcaoPublic.TheSelectedCompany.Nom);

                                    // replace special char ("[;\\\\/:*?\"<>|&']") with _
                                    if (C_Functionglobal.ValidateCompanyName(CompanyName))
                                    {
                                        CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                                    }

                                    string OrderedProductName = ProductName + "-" + CompanyName + ".pdf";

                                    try
                                    {
                                        DocumentsCreateXmlDoc(balanceKeys, DocumentsRefAnalyse);
                                        string docPath1 = "";

                                        // hide loading
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideLoading", "HideLoading();", true);
                                        try
                                        {
                                            string root = HttpContext.Current.Server.MapPath("~");

                                            string FolderName = ConfigurationManager.AppSettings["SiteName"].ToString();
                                            string RootName = @"..\..\wwwroot/" + FolderName + "/Pdf/Public/";

                                            docPath1 = Path.GetFullPath(Path.Combine(root, RootName + ProductName + "/" + CompanyName + "/" + ProductName + "-" + CompanyName + ".pdf"));
                                            // -------------------------------------------------------LocalHost-----------------------------------------------------------
                                            //string docPath = Path.GetFullPath(Path.Combine(root, @"..\..\wwwroot/BeacBs/Pdf/" + TheControler.LeControlerConsommation.TheConsommationToInsert.Link_User_ID + "/" + TheControler.TheSelectedProductToView.Prod_Name + "/" + FinalNameArticle + ".pdf"));

                                            string url1 = "Pdf.aspx";
                                            Session["DocumentEfcaoPath"] = docPath1;
                                            //Response.Write("<SCRIPT language=javascript>var pdf=window.open('" + url1 + "','PDF');</SCRIPT>");
                                            Response.Write("<SCRIPT language=javascript>var pdf=window.open('" + url1 + "','PDF', '_blank', 'height=150,width=200');</SCRIPT>");
                                        }
                                        catch (C_EfcaoException ex)
                                        {
                                            ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                                        }
                                        catch (SystemException ex)
                                        {
                                            ViewAlert("Erreur", ex.Message, "DANGER");
                                        }
                                    }
                                    catch (C_EfcaoException)
                                    {
                                        lblErrorMessage.Text = GetObjectLanguageByResourceID("MesgEfcao17");  // Erreur de création de document xml!.
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivErrorModal').modal();", true);
                                        UpdatePanelError.Update();
                                    }
                                }
                                else
                                {
                                    lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao13");  // Merci de sélectionner au moins une document.
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                                    UpdatePanelAvertissement.Update();
                                }

                            }
                            catch (C_EfcaoException ex)
                            {
                                ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
                            }
                            catch (SystemException ex)
                            {
                                ViewAlert("Erreur", ex.Message, "DANGER");
                            }
                        }
                    }
                }
            }
            catch (C_EfcaoException ex)
            {
                ViewAlert("Erreur", ex.ErrorMessage, "DANGER");
            }
            catch (SystemException ex)
            {
                ViewAlert("Erreur", ex.Message, "DANGER");
            }
        }
        #endregion

        #region -----------------Button New Search_Click-----------------
        /// <summary>
        /// Button New Search_Click
        /// </summary>
        /// <param name=""></param>
        /// <return></return>
        protected void BttnNewSearch_Click(object sender, EventArgs e)
        {
            PanelSearchCompGV.Visible = false;
            PanelGridViewExceriesMain.Visible = false;
            PanelCompAddress.Visible = false;
            DivRecherch.Visible = true;
        }

        #endregion

        #region -----------------View Alert / Error / Sucees-----------------
        /// <summary>
        /// View Alert / Error / Sucees
        /// </summary>      
        public void ViewAlert(string Title, string message, string Typealert)
        {
            Page page1 = HttpContext.Current.CurrentHandler as Page;
            page1.ClientScript.RegisterStartupScript(typeof(Page), "Test", "<script type='text/javascript'>messageAlert('" + Title + "', '" + message + "', 3000 , '" + Typealert + "');</script>");
        }


        public void ViewAlert1(string Title, string message, string Typealert)
        {
            //Page page1 = HttpContext.Current.CurrentHandler as Page;

            //ScriptManager.RegisterStartupScript(BttnEfcaoOk, this.GetType(), "JSCR", "<script type='text/javascript'>messageAlert('" + Title + "', '" + message + "', 3000 , '" + Typealert + "');</script>", false);
            //page1.ClientScript.RegisterStartupScript(typeof(Page), "Test", "<script type='text/javascript'>messageAlert('" + Title + "', '" + message + "', 3000 , '" + Typealert + "');</script>");
            //this.Page.ClientScript.RegisterClientScriptBlock(BttnEfcaoOk, "alertScript", "alert('hi')", true);


            //System.Web.UI.ClientScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertScript", "<script type='text/javascript'>messageAlert('" + Title + "', '" + message + "', 3000 , '" + Typealert + "');</script>", true);

            //System.Web.UI.ClientScriptManager.RegisterClientScriptBlock(
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Pop", "<script type='text/javascript'>messageAlert('" + Title + "', '" + message + "', 3000 , '" + Typealert + "');</script>", true);

            //ScriptManager.RegisterClientScriptBlock(BttnEfcaoOk, this.GetType(), "alert", "<script type='text/javascript'>messageAlert('" + Title + "', '" + message + "', 3000 , '" + Typealert + "');</script>", true);

            //ScriptManager.RegisterClientScriptBlock(BttnEfcaoOk, this.GetType(), "AlertMsg", "<script language='javascript'>CountCaddie('" + NItem + "');</script>", false);
        }

        #endregion
    }
}
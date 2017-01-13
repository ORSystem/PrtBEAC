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
    public partial class Default : System.Web.UI.Page
    {
        private Ctrl_Efcao TheCtrlEfcao;

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
                        TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                        TheCtrlEfcao.TheSelectedProductToView = new C_Product();
                        TheCtrlEfcao.TheSelectedProductToView.GetProductByProductID(ProductID);
                        LblEfcaoSubTitle.Text = TheCtrlEfcao.TheSelectedProductToView.Prod_Name;
                    }
                }
                GetListConsommation();
                //TBoxRecherche.Focus();
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
                LblSearchComp.Text = rm.GetString("LblSearchComp");                         // Recherche de sociétés
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

                BttnGetProdDoc.Text = rm.GetString("BttnGetProdDoc");                  // Obtenir les documents
                BttnSelectAll.Text = rm.GetString("BttnSelectAll");             // Tout sélectionner
                BttnUnSelectAll.Text = rm.GetString("BttnUnSelectAll");         // Tout desélectionner
                BttnOk.Text = rm.GetString("BttnOk");                           // Ok
                BttnDownLoad.Text = rm.GetString("BttnDownLoad");               // Télécharger
                BttnModeAvance.Text = rm.GetString("BttnModeAvance");                       // Mode avancé
                BttnGetDocImedit.Text = rm.GetString("BttnGetProdDoc");                     // Obtenir les documents
                BttnNewSearch.Text = rm.GetString("BttnNewSearch");                        // Nouvelle recherche

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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    // Change the value of * to % in case of SQL Server
                    // For Access DataBase leave the * And Replace % with *
                    //if (TBoxRecherche.Value == "*" || TBoxRecherche.Value == "")
                    //{
                    //    TBoxRecherche.Value = "%";
                    //}

                    // Set the controler object to new object 
                    TheCtrlEfcao.TheSelectedCompany = new C_Company();
                    TheCtrlEfcao.TheListCompany = new C_ListCompanies();
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    TheCtrlEfcao.SearcheCompany(TBoxRecherche.Text);
                    if (TheCtrlEfcao.TheListCompany.Count > 0)
                    {
                        BindSearchCompanyGridView();
                        PanelSearchCompGV.Visible = true;

                        LblEfcaoCompTitle.Text = "Sociétés ( " + TheCtrlEfcao.TheListCompany.Count + " trouvées)";
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    ResetCompanySearchGridView();
                    if (TheCtrlEfcao.TheListCompany.Count > 0)
                    {
                        SearchCompanyGridView.DataSource = TheCtrlEfcao.TheListCompany;
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];

                // Set the controler object to new object 
                TheCtrlEfcao.TheSelectedCompany = new C_Company();
                TheCtrlEfcao.TheListExercieses = new C_ListExercises();
                TheCtrlEfcao.TheListExercicesModels = new C_ListExercicesModels();
                LblConfirmOrder.Text = "";
                RowMesgOrder.Visible = false;

                try
                {
                    //SearchCompanyGridView.SelectedRowStyle.BackColor = Color.Black;  
                    if (e.CommandName.Equals("EditCompany"))
                    {
                        if (Request.QueryString.Count != 0)
                        {
                            DivRecherch.Visible = false;

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

                            // Get the Company Key  and assigne it to the controler company key
                            TheCtrlEfcao.TheSelectedCompany.Key = CompanyKey;
                          
                            TheCtrlEfcao.TheSelectedCompany = TheCtrlEfcao.TheListCompany.FindCompany(CompanyKey);
                            TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse = TheCtrlEfcao.TheSelectedCompany.Modele;

                            //---------------------------------------------------------
                            // Get the model links
                            ModelGetLinks();

                            if (TheCtrlEfcao.TheListExercieses.Count == 0)
                            {
                                GetExercicesModelList();

                                //Get balance list
                                GetBalanceList();
                            }

                            LblModeleAnayseValue.Text = TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse;
                            LblModeleSaisieValue.Text = TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie;

                            LblMainExercicesCompName.Text = TheCtrlEfcao.TheSelectedCompany.Nom;
                            LblCompIdentifiantVal.Text = TheCtrlEfcao.TheSelectedCompany.IdExterne;

                            LblAvanceExercicesCompName.Text = TheCtrlEfcao.TheSelectedCompany.Nom;
                            LblAvanceCompIdentifiantVal.Text = TheCtrlEfcao.TheSelectedCompany.IdExterne;

                            //LblEfcaoExercies.Text = GetObjectLanguageByResourceID("MesgEfcao01") + " " + TheControler.TheSelectedCompany.Nom;                 // Exercices modèles pour 
                            LblEfcaoDocSaisie.Text = GetObjectLanguageByResourceID("MesgEfcao02") + " " + TheCtrlEfcao.TheSelectedCompany.Nom;                // Documents saisie pour
                            LblEfcaoDocAnalyse.Text = GetObjectLanguageByResourceID("MesgEfcao03") + " " + TheCtrlEfcao.TheSelectedCompany.Nom;               // Documents analyse pour

                            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);

                            // insert command
                            TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert = new C_Consom();
                            if (Request.QueryString.Count != 0)
                            {
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID = UserID;
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_Prod_ID = TheCtrlEfcao.TheSelectedProductToView.Prod_ID;
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Quantity = 1;
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Unit_Price = TheCtrlEfcao.TheSelectedProductToView.Prod_Price;
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Vat_Unit = 0;
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Company_Key = Convert.ToInt32(CompanyKey);
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Date_Consumption = DateTime.Now;
                                TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Article_Name = TheCtrlEfcao.TheSelectedCompany.Nom;
                            }

                            BindGridViewExercies();
                            PanelSearchCompGV.Visible = false;
                            PanelGridViewExceriesMain.Visible = true;

                            GetCompanyProperty(CompanyKey);
                        }
                        else
                        {
                            string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao14"); // L’id de produit et utilisateur est manquant
                            ViewAlert("Avertissement", ErrorMessage, "Warning");
                            //ScriptManager.RegisterClientScriptBlock(UpdatePanelGridComp, this.GetType(), "AlertMsg", "<script type='text/javascript'>messageAlert('Avertissement', '" + ErrorMessage + "', 3000 , 'Warning');</script>", false);
                        }
                        // hide loading
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideLoading", "HideLoading();", true);
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
                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridCompID");            // Clé Société
                        e.Row.Cells[0].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridCompLock");          // Type de verrouillage
                        e.Row.Cells[1].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridCompNom");           // Nom
                        e.Row.Cells[2].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridCompIdentifiant");   // Identifiant
                        e.Row.Cells[3].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridCompModels");        // Modèles
                        e.Row.Cells[4].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridCompAction");        // Action
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    // get Company model Saisie
                    string CompanyModelSaisie = TheCtrlEfcao.ModelGetLinks(TheCtrlEfcao.TheSelectedCompany.Modele);
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

            #region -----------------Company Property-----------------
            /// <summary>
            /// Bind the company property to the diffrent object
            /// </summary>
            /// <param name="CompanyKey"></param>
            public void GetCompanyProperty(UInt64 CompanyKey)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    TheCtrlEfcao.GetCompanyProperty(CompanyKey);

                    // Check if the company Property not null
                    if (TheCtrlEfcao.CompanyProperty != null)
                    {
                        foreach (var P in TheCtrlEfcao.CompanyProperty)
                        {
                            string Adresse;
                            if (TheCtrlEfcao.CompanyProperty.TryGetValue("Adresse1", out Adresse))
                            {
                                LblCompAddressVal.Text = Adresse;
                                LblAvanceCompAddressVal.Text = Adresse;
                            }

                            string Adresse2;
                            if (TheCtrlEfcao.CompanyProperty.TryGetValue("Adresse2", out Adresse2))
                            {
                                LblCompAddressVal1.Text = Adresse2;
                            }

                            string CodePostal;
                            if (TheCtrlEfcao.CompanyProperty.TryGetValue("CodePostal", out CodePostal))
                            {
                                LblCompZipVal.Text = CodePostal;
                                LblAvanceCompZipVal.Text = CodePostal;
                            }

                            string Ville;
                            if (TheCtrlEfcao.CompanyProperty.TryGetValue("Ville", out Ville))
                            {
                                LblCompRegionVal.Text = Ville;
                                LblAvanceCompCityVal.Text = Ville;
                            }

                            string Pays;
                            if (TheCtrlEfcao.CompanyProperty.TryGetValue("Pays", out Pays))
                            {
                                LblCompRegionVal.Text = Pays;
                                LblAvanceCompCountryVal.Text = Pays;

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
                            if (TheCtrlEfcao.CompanyProperty.TryGetValue("Region", out Région))
                            {
                                LblCompCountryVal.Text = Région;
                                LblAvanceCompRegionVal.Text = Région;
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

        #region ----------------------------------Company Balance  Exercies----------------------------------

            #region -----------------Get Balance List-----------------
            /// <summary>
            /// Get company's balances list
            /// Bind the data to the GridViewExercies
            /// </summary>
            public void GetBalanceList()
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    ResetGridViewExercies();

                    // Bind the data to the GridViewExercies
                    TheCtrlEfcao.GetBalanceList(TheCtrlEfcao.TheSelectedCompany.Key);
                    if (TheCtrlEfcao.TheListExercieses.Count > 0)
                    {                     
                        BindGridViewExercies();
                        BttnGetProdDoc.Enabled = true;
                        BttnGetProdDoc.Visible = true;
                        PanelExerciesGridView.Visible = true;
                        UpdatePanelExerciesModel.Update();
                    }
                    else
                    {
                        BttnGetProdDoc.Enabled = false;
                        BttnGetProdDoc.Visible = false;
                        PanelExerciesGridView.Visible = false;
                        UpdatePanelExerciesModel.Update();

                        lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao05");  // Aucun exercice la base de données.                     
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModel').modal();", true);
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {                   
                    // Bind the data to the GridViewExercies
                    TheCtrlEfcao.GetExercicesModelList(TheCtrlEfcao.TheSelectedCompany.Key);

                    DDListExerciesModels.DataSource = TheCtrlEfcao.TheListExercicesModels;
                    DDListExerciesModels.DataTextField = "CompanyModelAnalyse";
                    DDListExerciesModels.DataValueField = "CompanyModelSaisie";
                    DDListExerciesModels.DataBind();
                    DDListExerciesModels.ClearSelection();

                    foreach (System.Web.UI.WebControls.ListItem MonItem in DDListExerciesModels.Items)
                    {
                        if (MonItem.Text == TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse)
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {                   
                    TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie = DDListExerciesModels.SelectedValue.ToString();
                    TheCtrlEfcao.TheSelectedCompany.Modele = DDListExerciesModels.SelectedItem.ToString();
                    TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse = DDListExerciesModels.SelectedItem.ToString();
                    LblModeleAnayseValue.Text = TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse;
                    LblModeleSaisieValue.Text = TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie;
                    TheCtrlEfcao.TheListExercieses = new C_ListExercises();
                    TheCtrlEfcao.TheListExercicesModels = new C_ListExercicesModels();
                    TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse = new C_ListProdDocs();
                    TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie = new C_ListProdDocs();
                    PanelDocSaisie.Visible = false;
                    PanelDocAnalyse.Visible = false;

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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    ResetGridViewExercies();
                    if (TheCtrlEfcao.TheListExercieses.Count > 0)
                    {
                        ExerciesGridViewAvance.DataSource = TheCtrlEfcao.TheListExercieses;
                        ExerciesGridViewAvance.DataBind();

                        GridViewExerciesMain.DataSource = TheCtrlEfcao.TheListExercieses;
                        GridViewExerciesMain.DataBind();
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
            protected void ExerciesGridViewAvance_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {               
                    ExerciesGridViewAvance.DataSource = TheCtrlEfcao.TheListExercieses;
                    ExerciesGridViewAvance.PageIndex = e.NewPageIndex;
                    ExerciesGridViewAvance.DataBind();
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
            protected void ExerciesGridViewAvance_RowCommand(object sender, GridViewCommandEventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    if (e.CommandName.Equals("MyCustomCommand"))
                    {
                        // Get the clicked row
                        GridViewRow row = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;

                        Label LabelBalanceKey = (Label)ExerciesGridViewAvance.Rows[row.RowIndex].FindControl("ExerciesKey");
                        LinkButton LabelBalancedate = (LinkButton)ExerciesGridViewAvance.Rows[row.RowIndex].FindControl("LinkBtnDateBalance");

                        // Select The GridView CheckBoxes SelectedDatesCollection date
                        CheckBox DateCheckBox = (CheckBox)ExerciesGridViewAvance.Rows[row.RowIndex].FindControl("CheckDate");
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                TheCtrlEfcao.DTableCompanyModelDetailSaisie = new DataTable();
                TheCtrlEfcao.DTableCompanyModelDetailAnalyse = new DataTable();
                TheCtrlEfcao.DTableCompanyModelDetailParSectionAnalyse = new DataTable();
                TheCtrlEfcao.DTableCompanyModelDetailParSectionSaisie = new DataTable();
                try
                {                   
                    CheckBox DateCheckBox = (CheckBox)sender;
                    GridViewRow row = (GridViewRow)DateCheckBox.NamingContainer;

                    //GridViewRow row = ExerciesGridView.SelectedRow;
                    Label LabelBalanceKey = (Label)ExerciesGridViewAvance.Rows[row.RowIndex].FindControl("ExerciesKey");
                    C_Exercise theExercise = new C_Exercise();
                    if (DateCheckBox.Checked)
                    {
                        theExercise = TheCtrlEfcao.TheListExercieses.FindExercise(Convert.ToUInt32(LabelBalanceKey.Text));
                        theExercise.RowChecked = true;
                    }
                    else
                    {
                        theExercise = TheCtrlEfcao.TheListExercieses.FindExercise(Convert.ToUInt32(LabelBalanceKey.Text));
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
                ExerciesGridViewAvance.DataSource = null;
                ExerciesGridViewAvance.DataBind();

                GridViewExerciesMain.DataSource = null;
                GridViewExerciesMain.DataBind();
            }

            #endregion

            #region -----------------Exercies GridView Row Data Bound -----------------
            /// <summary>
            /// GridView Exercies Row DataBound
            /// </summary>
            protected void ExerciesGridViewAvance_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    //else
                    //{
                    //    Label DateLabel = (Label)e.Row.FindControl("TheDateExer");

                    //    if (DateLabel != null)
                    //    {
                    //        string DateToConvert = DateLabel.Text;
                    //        DateToConvert = C_Functionglobal.GetTheDateLeftValue(DateToConvert);
                    //        DateLabel.Text = DateToConvert;
                    //    }
                    //}
                }            
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Exercies GridView Main Row Data Bound -----------------
            /// <summary>
            /// Exercies GridView Main Row Data Bound
            /// </summary>
            protected void GridViewExerciesMain_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    //else
                    //{
                    //    Label DateLabel = (Label)e.Row.FindControl("TheDateExer");

                    //    if (DateLabel != null)
                    //    {
                    //        string DateToConvert = DateLabel.Text;

                    //        DateToConvert = C_Functionglobal.GetTheDateLeftValue(DateToConvert);

                    //        DateLabel.Text = DateToConvert;
                    //    }
                    //}
                }
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region -----------------Exercies GridView Main Page Index Changing-----------------
            /// <summary>
            /// Exercies GridView Main Page Index Changing
            /// </summary>
            protected void GridViewExerciesMain_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                try
                {
                    TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                    GridViewExerciesMain.DataSource = TheCtrlEfcao.TheListExercieses;
                    GridViewExerciesMain.PageIndex = e.NewPageIndex;
                    GridViewExerciesMain.DataBind();
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie.Count > 0)
                    {
                        ResetGridViesDocumentSaisie();
                        TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie = new C_ListProdDocs();

                        foreach (C_ProductDocuments P in TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie)
                        {
                            TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie.Add(P);
                        }
                        GridViewDocumentSaisie.DataSource = TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie;
                        GridViewDocumentSaisie.DataBind();
                    }
                    //ResetGridViesDocumentSaisie();
                    //GridViewDocumentSaisie.DataSource = TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie;
                    //GridViewDocumentSaisie.DataBind();
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
                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocSelect");           // Sélectionnez
                        e.Row.Cells[0].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocID");              // ID
                        e.Row.Cells[1].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocName");           // Nom du document
                        e.Row.Cells[2].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocEdw");           // Edw
                        e.Row.Cells[3].Text = GridHeaderCellText;
                    }
                }
               
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region ---------------------------CheckBoxSaisie_CheckedChanged-------------------------
            /// <summary>
            /// CheckBoxSaisie_CheckedChanged
            /// </summary>
            protected void CheckBoxSaisie_CheckedChanged(object sender, EventArgs e)
            {
                // Change
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);

                    C_ProductDocuments ProdDoc = new C_ProductDocuments();

                    CheckBox CheckBoxSaisie = (CheckBox)GridViewDocumentSaisie.Rows[row.RowIndex].FindControl("CheckBoxSaisie");
                    string AdesDocID = row.Cells[1].Text;
                    int DocID;
                    DocID = Convert.ToInt32(AdesDocID);

                    if (CheckBoxSaisie.Checked)
                    {
                        ProdDoc = TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie.FindProductDocumentByProductDocID(DocID);
                        ProdDoc.Doc_Checked = true;
                        //BindGridViewDoumentSaisie();                   
                    }
                    else
                    {
                        ProdDoc = TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie.FindProductDocumentByProductDocID(DocID);
                        ProdDoc.Doc_Checked = false;
                        //BindGridViewDoumentSaisie();                      
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

        #region ----------------------------------GridView Product Documents Analyse----------------------------------

            #region -----------------Bind GridView Doument Analyse -----------------
            /// <summary>
            /// Bind GridView Doument Analyse
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            public void BindGridViewDoumentAnalyse()
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count > 0)
                    {
                        ResetGridViesDocumentAnalyse();
                        TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse = new C_ListProdDocs();

                        foreach (C_ProductDocuments P in TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse)
                        {
                            TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse.Add(P);
                        }
                        GridViewDocumentAnalyse.DataSource = TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse;
                        GridViewDocumentAnalyse.DataBind();
                    }
                    //ResetGridViesDocumentAnalyse();
                    //GridViewDocumentAnalyse.DataSource = TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse;
                    //GridViewDocumentAnalyse.DataBind();
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
                    BttnOk.Focus();
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
                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocSelect");           // Sélectionnez
                        e.Row.Cells[0].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocID");              // ID
                        e.Row.Cells[1].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocName");           // Nom du document
                        e.Row.Cells[2].Text = GridHeaderCellText;

                        GridHeaderCellText = C_Functionglobal.GetObjectLanguage("GridDocEdw");           // Edw
                        e.Row.Cells[3].Text = GridHeaderCellText;
                    }
                }
               
                catch (SystemException ex)
                {
                    ViewAlert("Erreur", ex.Message, "DANGER");
                }
            }
            #endregion

            #region ---------------------------CheckBoxAnalyse_CheckedChanged-------------------------
            /// <summary>
            /// CheckBoxAnalyse_CheckedChanged
            /// </summary>
            protected void CheckBoxAnalyse_CheckedChanged(object sender, EventArgs e)
            {
                // Change
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);



                    CheckBox CheckBoxAnalyse = (CheckBox)GridViewDocumentAnalyse.Rows[row.RowIndex].FindControl("CheckBoxAnalyse");

                    string AdesDocID = row.Cells[1].Text;
                    int DocID;
                    DocID = Convert.ToInt32(AdesDocID);

                    if (CheckBoxAnalyse.Checked)
                    {
                        C_ProductDocuments ProdDoc = new C_ProductDocuments();
                        ProdDoc = TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse.FindProductDocumentByProductDocID(DocID);
                        ProdDoc.Doc_Checked = true;
                        //BindGridViewDoumentAnalyse();
                    }
                    else
                    {
                        C_ProductDocuments ProdDoc = new C_ProductDocuments();
                        ProdDoc = TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse.FindProductDocumentByProductDocID(DocID);
                        ProdDoc.Doc_Checked = false;
                        //BindGridViewDoumentAnalyse();
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

        #region ----------------------------------Product Document Management----------------------------------

            #region -----------------Bttn Get document Imediatlly by default-----------------
            /// <summary>
            /// Bttn Get document Imediatlly by default-
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void BttnGetDocImedit_Click(object sender, EventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
                try
                {
                    string Ffc = "";
                    if (!(string.IsNullOrEmpty(Request.QueryString["Ffc"])))
                    {
                        Ffc = Request.QueryString["Ffc"].ToString();

                        if (Ffc == "MspD") // Date
                        {

                            DateTime MspEndDate = TheCtrlEfcao.TheCtrlConsom.GetMspEndDate(UserID);
                            DateTime TodayDate = DateTime.Now;

                            if (MspEndDate >= TodayDate)
                            {
                                ViewDocImedit();
                            }
                            else
                            {
                                string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao22");    // La date de votre abonnement n’est plus valable !               
                                ViewAlert("Avertissement", ErrorMessage, "Warning");
                            }                            
                        }
                        else if (Ffc == "Msp0") // no membership
                        {
                            ViewDocImedit();
                        }
                        else // Jeyons
                        {

                            int AvailabeJetons;
                            AvailabeJetons = TheCtrlEfcao.TheCtrlConsom.GetMspRemainigTokens(UserID);
                            if (AvailabeJetons >= TheCtrlEfcao.TheSelectedProductToView.Prod_Price)
                            {
                                ViewDocImedit();
                            }
                            else
                            {
                                string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao23");     // Votre abonnement ne possède pas assez de jetons !           
                                ViewAlert("Avertissement", ErrorMessage, "Warning");
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

            protected void ViewDocImedit()
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    //Get the number of selected exercises
                    int NumberOFSlectedExercies = 0;

                    foreach (C_Exercise MyExercise in TheCtrlEfcao.TheListExercieses)
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
                        btnCommandOk.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                        UpdatePanelAvertissement.Update();
                    }
                    else
                    {
                        TheCtrlEfcao.TheSelectedCompany.NumberOFSlectedExercises = NumberOFSlectedExercies;
                        TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie = DDListExerciesModels.SelectedValue.ToString();
                        TheCtrlEfcao.TheSelectedCompany.Modele = DDListExerciesModels.SelectedItem.ToString();
                        TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse = DDListExerciesModels.SelectedItem.ToString();

                        ResetGridViesDocumentAnalyse();
                        ResetGridViesDocumentSaisie();

                        TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse = new C_ListProdDocs();
                        TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie = new C_ListProdDocs();
                        TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie = new C_ListProdDocs();
                        TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse = new C_ListProdDocs();

                        GetListDocumentSaisieByProductID(TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie);
                        GetListDocumentAnalyseByProductID(TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse);

                        //foreach (C_ExercicesModel Xm in TheCtrlEfcao.TheListExercicesModels)
                        //{
                        //    GetListDocumentSaisieByProductID(Xm.CompanyModelSaisie);
                        //    GetListDocumentAnalyseByProductID(Xm.CompanyModelAnalyse);
                        //}

                        PanelDocAnalyse.Visible = false;
                        PanelDocSaisie.Visible = false;

                        if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count == 0) 
                        {
                            //
                            lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao24");  // Aucun document de ce produit ne correspond aux modèles de cette société.
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                            UpdatePanelAvertissement.Update();
                        }
                        else
                        {
                            PanelExerciesGridView.Visible = false;
                            int numberOFDocSaisie = TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie.Count;
                            int numberOFDocAnalyse = TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count;

                            if (!ProductHasBeenOrdered())
                            {
                                btnCommandOk.CommandName = "Imediat";
                                lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao15"); // Attention ! Si vous cliquez sur OK, la commande sera validée et facturée. ?";   
                                btnCommandOk.Visible = true;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                                UpdatePanelAvertissement.Update();
                            }
                            else
                            {

                                btnCommandOk.CommandName = "Imediat";
                                lblAvertisMessage.Text = GetObjectLanguageByResourceID("LblContinue"); // Continuer;      
                                btnCommandOk.Visible = true;
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                                UpdatePanelAvertissement.Update();
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

            #region -----------------Button Get Documents avance click-----------------
            /// <summary>
            /// Button Get Documents avance click
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void BttnGetDocAvance_Click(object sender, EventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {                   
                    //Get the number of selected exercises
                    int NumberOFSlectedExercies = 0;

                    foreach (C_Exercise MyExercise in TheCtrlEfcao.TheListExercieses)
                    {
                        if (MyExercise.RowChecked)
                        {
                            NumberOFSlectedExercies = NumberOFSlectedExercies + 1;
                        }
                    }

                    if (NumberOFSlectedExercies > 5)
                    {
                        //Max number of authorised selection is 5
                        lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao06"); // Le nombre maximum d’exercices à sélectionner est 5.
                        btnCommandOk.Visible = false;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                        UpdatePanelAvertissement.Update();
                    }
                    else
                    {
                        TheCtrlEfcao.TheSelectedCompany.NumberOFSlectedExercises = NumberOFSlectedExercies;
                        TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie = DDListExerciesModels.SelectedValue.ToString();
                        TheCtrlEfcao.TheSelectedCompany.Modele = DDListExerciesModels.SelectedItem.ToString();
                        TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse = DDListExerciesModels.SelectedItem.ToString();

                        ResetGridViesDocumentAnalyse();
                        ResetGridViesDocumentSaisie();

                        TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse = new C_ListProdDocs();
                        TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie = new C_ListProdDocs();

                        TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie = new C_ListProdDocs();
                        TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse = new C_ListProdDocs();

                        GetListDocumentSaisieByProductID(DDListExerciesModels.SelectedValue.ToString());
                        GetListDocumentAnalyseByProductID(DDListExerciesModels.SelectedItem.ToString());

                        if ((TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count == 0) && (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie.Count == 0))
                        {
                            PanelDocSaisie.Visible = false;
                            PanelDocAnalyse.Visible = false;
                            UpdatePanelExerciesModel.Update();
                            PanelExerciesGridView.Visible = true;

                            lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao09"); // Les listes des documents Saisie / Analyse Sont vide.
                            btnCommandOk.Visible = false;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                            UpdatePanelAvertissement.Update();
                        }
                        else
                        {
                            PanelExerciesGridView.Visible = false;

                            if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie.Count > 0)
                            {
                                PanelDocSaisie.Visible = true;
                                BttnSelectAll.Visible = true;
                                BttnUnSelectAll.Visible = true;
                                BttnOk.Visible = true;
                                UpdatePanelExerciesModel.Update();
                            }
                            else
                            {
                                PanelDocSaisie.Visible = false;
                                BttnSelectAll.Visible = false;
                                BttnUnSelectAll.Visible = false;
                                BttnOk.Visible = false;
                                UpdatePanelExerciesModel.Update();
                                btnCommandOk.Visible = false;
                            }

                            if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count > 0)
                            {
                                PanelDocAnalyse.Visible = true;
                                BttnSelectAll.Visible = true;
                                BttnUnSelectAll.Visible = true;
                                BttnOk.Visible = true;
                                UpdatePanelExerciesModel.Update();
                            }
                            else
                            {
                                PanelDocAnalyse.Visible = false;
                                BttnSelectAll.Visible = false;
                                BttnUnSelectAll.Visible = false;
                                BttnOk.Visible = false;
                                UpdatePanelExerciesModel.Update();                           
                                btnCommandOk.Visible = false;
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
                finally
                {
                    // hide loading
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideLoading", "HideLoading();", true);
                }
            }

            #endregion

            #region -----------------Get List Document Saisie By Product ID-----------------
            /// <summary>
            /// Get List Document Saisie By Produc tID
            /// </summary>
            /// <param name="ProductID"></param>
            /// <return></return>
            public void GetListDocumentSaisieByProductID(string model)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie.GetListDocumentSaisieByProductID(TheCtrlEfcao.TheSelectedProductToView.Prod_ID, model);

                    if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie.Count > 0)
                    {
                        BindGridViewDoumentSaisie();
                        PanelDocSaisie.Visible = true;
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    ////jamal
                    //TheControler.TheSelectedProductToView.ProductID = 29;
                    TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.GetListDocumentAnalyseByProductID(TheCtrlEfcao.TheSelectedProductToView.Prod_ID, model);

                    if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count > 0)
                    {
                        BindGridViewDoumentAnalyse();
                        PanelDocAnalyse.Visible = true;
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

        #region -----------------GridView Documents Analyse Saisie buttons  Ok / Annuler / Tout sélectionner / Tout desélectionner /-----------------

            #region -----------------Button Ok Click-----------------
            /// <summary>
            /// Bttn Ok click event
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void BttnOk_Click(object sender, EventArgs e)
            {
                int numberOFDocSaisie = 0;
                int numberOFDocAnalyse = 0;
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];

                try
                {                  
                    // --------------------Documents Saisie. -----------------------------
                    // Get the number of selected Documents Saisie
                    foreach (C_ProductDocuments theProductDocSaisie in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie)
                    {
                        if (theProductDocSaisie.Doc_Checked)
                        {
                            // Increment the number of numberOFDocSaisie
                            numberOFDocSaisie = numberOFDocSaisie + 1;
                        }
                    }

                    // --------------------Documents Analyse -----------------------------
                    // Get the number of selected Documents Analyse

                    foreach (C_ProductDocuments theProductDocAnalyse in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse)
                    {
                        if (theProductDocAnalyse.Doc_Checked)
                        {
                            // Increment the number of numberOFDocAnalyse
                            numberOFDocAnalyse = numberOFDocAnalyse + 1;
                        }
                    }

                    if ((numberOFDocSaisie > 0) || (numberOFDocAnalyse > 0))
                    {
                        if (!ProductHasBeenOrdered())
                        {
                            lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao15");  // Attention ! Si vous cliquez sur OK, la commande sera validée et facturée. ?
                            btnCommandOk.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                            UpdatePanelAvertissement.Update();
                        }
                        else
                        {   
                            lblAvertisMessage.Text = GetObjectLanguageByResourceID("LblContinue"); // Continuer;
                            btnCommandOk.Visible = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivAvertissementModal').modal();", true);
                            UpdatePanelAvertissement.Update();
                        }
                    }
                    else
                    {
                        lblAvertisMessage.Text = GetObjectLanguageByResourceID("MesgEfcao13");   // Merci de sélectionner au moins une document.
                        btnCommandOk.Visible = false;
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

            #region -----------------Button Command-----------------
            /// <summary>
            /// Bttn Command
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void btnCommandOk_Click(object sender, EventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                string CommandName = ((Button)sender).CommandName;
                try
                {                    
                    int numberOFDocSaisie = 0;
                    int numberOFDocAnalyse = 0;
                    string[] DocumentsRefSaisie;
                    string[] DocumentsRefAnalyse;
                    string[] DocumentsRefSaisieAnalyse;
                    int i = 0;
                    int j = 0;
                    int k = 0;

                    // Create an array string that contains the Balance Keys
                    ulong[] balanceKeys = new ulong[TheCtrlEfcao.TheSelectedCompany.NumberOFSlectedExercises];

                    foreach (C_Exercise MyExercise in TheCtrlEfcao.TheListExercieses)
                    {
                        if (MyExercise.RowChecked)
                        {
                            balanceKeys[i] = MyExercise.CleUnik;
                            i = i + 1;
                        }
                    }

                    if (CommandName == "Imediat") // Imediate only the document analysis for the company model by default
                    {
                        if (TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count > 0)
                        {
                            // numberOFDocSaisie = TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie.Count;
                            numberOFDocAnalyse = TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse.Count;

                            // Create an array string that contains the reference of the Documents Saisie, Analyse
                            DocumentsRefAnalyse = new string[numberOFDocAnalyse];
                            int q = 0;

                            //foreach (C_ProductDocuments theProductDocSaisie in TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentSaisie)
                            //{
                            //    string LabelWithId = TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie + "." + theProductDocSaisie.Ades_Doc_ID;
                            //    DocumentsRefAnalyse[q] = LabelWithId;
                            //    // Increment Q
                            //    q = q + 1;
                            //}

                            foreach (C_ProductDocuments theProductDocAnalyse in TheCtrlEfcao.TheSelectedProductToView.TheListProductsDocumentAnalyse)
                            {
                                string LabelWithId = TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse + "." + theProductDocAnalyse.Ades_Doc_ID;
                                DocumentsRefAnalyse[q] = LabelWithId;
                                // Increment Q
                                q = q + 1;
                            }

                            if (numberOFDocAnalyse > 0)
                            {
                                //bool DoInsertCommande = false;
                                string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedProductToView.Prod_Name);
                                string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedCompany.Nom);

                                // replace special char ("[;\\\\/:*?\"<>|&']") with _
                                if (C_Functionglobal.ValidateCompanyName(CompanyName))
                                {
                                    CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                                }

                                string OrderedProductName = ProductName + "-" + CompanyName + ".pdf";

                                //DoInsertCommande = CheckIfProductAllreadyOrdered();

                                if (!ProductHasBeenOrdered())
                                {
                                    try
                                    {
                                        DocumentsCreateXmlDoc(balanceKeys, DocumentsRefAnalyse);
                                        string docPath1 = "";

                                        try
                                        {
                                            // Insert Order
                                            InsertOrder();

                                            string NItem = GetListConsommation();
                                            ScriptManager.RegisterClientScriptBlock(BttnOk, this.GetType(), "AlertMsg", "<script language='javascript'>CountCaddie('" + NItem + "');</script>", false);

                                            BttnDownLoad.Visible = true;
                                            BttnDownLoad.Enabled = true;
                                            UpdatePanelSuccess.Update();

                                            string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao10") + " " + OrderedProductName + " " + GetObjectLanguageByResourceID("MesgEfcao11");  // Le Produit : + ... +  // a été enregistré, Le document Pdf a été créé.
                                            LblConfirmOrder.Text = ErrorMessage;
                                            RowMesgOrder.Visible = true;

                                            // hide loading
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "HideLoading", "HideLoading();", true);
                                            try
                                            {
                                                string root = HttpContext.Current.Server.MapPath("~");

                                                string FolderName = ConfigurationManager.AppSettings["SiteName"].ToString();
                                                string RootName = @"..\..\wwwroot/" + FolderName + "/PDf/";

                                                docPath1 = Path.GetFullPath(Path.Combine(root, RootName + TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + CompanyName + "/" + ProductName + "-" + CompanyName + ".pdf"));
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
                                            lblErrorMessage.Text = GetObjectLanguageByResourceID("MesgEfcao16"); // Erreur lors l’ajout de la consommation !.
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivErrorModal').modal();", true);
                                            UpdatePanelError.Update();
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
                                    DocumentsCreateXmlDoc(balanceKeys, DocumentsRefAnalyse);
                                    string docPath1 = "";

                                    BttnDownLoad.Visible = true;
                                    BttnDownLoad.Enabled = true;
                                    UpdatePanelSuccess.Update();

                                    string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao10") + " " + OrderedProductName + " " + GetObjectLanguageByResourceID("MesgEfcao18") + " " + GetObjectLanguageByResourceID("MesgEfcao12"); // Le Produit : + ... +  // le nouveau document Pdf a été créé.
                                    LblConfirmOrder.Text = ErrorMessage;
                                    RowMesgOrder.Visible = true;

                                    // hide loading
                                    try
                                    {
                                        string root = HttpContext.Current.Server.MapPath("~");

                                        string FolderName = ConfigurationManager.AppSettings["SiteName"].ToString();
                                        string RootName = @"..\..\wwwroot/" + FolderName + "/PDf/";

                                        docPath1 = Path.GetFullPath(Path.Combine(root, RootName + TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + CompanyName + "/" + ProductName + "-" + CompanyName + ".pdf"));
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
                            }
                        }
                    }
                    else // Avance document analysis / saisie for the selected company model
                    {
                        // --------------------Documents Saisie. -----------------------------
                        // Get the number of selected Documents Saisie

                        foreach (C_ProductDocuments theProductDocSaisie in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie)
                        {
                            // Get the checked rows 
                            if (theProductDocSaisie.Doc_Checked)
                            {
                                // Increment the number of numberOFDocSaisie
                                numberOFDocSaisie = numberOFDocSaisie + 1;
                            }
                        }


                        // Create an array string that contains the reference of the Documents Saisie
                        DocumentsRefSaisie = new string[numberOFDocSaisie];

                        foreach (C_ProductDocuments theProductDocSaisie in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie)
                        {
                            // Get the checked rows
                            if (theProductDocSaisie.Doc_Checked)
                            {
                                // Get the reference for each row selected
                                string LabelWithId = TheCtrlEfcao.TheSelectedCompany.CompanyModelSaisie + "." + theProductDocSaisie.Ades_Doc_ID;
                                // Assign values
                                DocumentsRefSaisie[j] = LabelWithId;
                                // Increment j
                                j = j + 1;
                            }
                        }



                        // --------------------Documents Analyse -----------------------------
                        // Get the number of selected Documents Analyse
                        foreach (C_ProductDocuments theProductDocAnalyse in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse)
                        {
                            if (theProductDocAnalyse.Doc_Checked)
                            {
                                // Increment the number of numberOFDocAnalyse
                                numberOFDocAnalyse = numberOFDocAnalyse + 1;
                            }
                        }
                       
                        // Create an array string that contains the reference of the Documents Analyse
                        DocumentsRefAnalyse = new string[numberOFDocAnalyse];
                        foreach (C_ProductDocuments theProductDocAnalyse in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse)
                        {
                            if (theProductDocAnalyse.Doc_Checked)
                            {
                                // Get the reference for each row selected
                                string LabelWithId = TheCtrlEfcao.TheSelectedCompany.CompanyModelAnalyse + "." + theProductDocAnalyse.Ades_Doc_ID;
                                // Assign values
                                DocumentsRefAnalyse[k] = LabelWithId;
                                // Increment K
                                k = k + 1;
                            }
                        }
                     

                        // --------------------Documents Analyse Saisie-----------------------------
                        // Create an array string that contains the reference of the Documents Saisie, Analyse
                        DocumentsRefSaisieAnalyse = new string[numberOFDocSaisie + numberOFDocAnalyse];
                        int q = 0;
                        foreach (var S in DocumentsRefSaisie)
                        {
                            DocumentsRefSaisieAnalyse[q] = S;
                            q = q + 1;
                        }

                        foreach (var A in DocumentsRefAnalyse)
                        {
                            DocumentsRefSaisieAnalyse[q] = A;
                            q = q + 1;
                        }

                      
                        if ((numberOFDocSaisie > 0) || (numberOFDocAnalyse > 0))
                        {
                            //bool DoInsertCommande = false;
                            string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedProductToView.Prod_Name);
                            string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedCompany.Nom);

                            // replace special char ("[;\\\\/:*?\"<>|&']") with _
                            if (C_Functionglobal.ValidateCompanyName(CompanyName))
                            {
                                CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                            }

                            string OrderedProductName = ProductName + "-" + CompanyName + ".pdf";

                            //DoInsertCommande = CheckIfProductAllreadyOrdered();

                            if (!ProductHasBeenOrdered())
                            {
                                try
                                {
                                    DocumentsCreateXmlDoc(balanceKeys, DocumentsRefSaisieAnalyse);
                                    try
                                    {
                                        // Insert Order
                                        InsertOrder();

                                        string NItem = GetListConsommation();
                                        ScriptManager.RegisterClientScriptBlock(BttnOk, this.GetType(), "AlertMsg", "<script language='javascript'>CountCaddie('" + NItem + "');</script>", false);

                                        BttnDownLoad.Visible = true;
                                        BttnDownLoad.Enabled = true;
                                        UpdatePanelSuccess.Update();

                                        string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao10") + " " + OrderedProductName + " " + GetObjectLanguageByResourceID("MesgEfcao11");  // Le Produit : + ... +  // a été enregistré, Le document Pdf a été créé.
                                        LblConfirmOrder.Text = ErrorMessage;
                                        RowMesgOrder.Visible = true;

                                        LblSuccessMessgae.Text = GetObjectLanguageByResourceID("MesgEfcao10") + " " + OrderedProductName + " " + GetObjectLanguageByResourceID("MesgEfcao11");  // Le Produit : + ... +  // a été enregistré, Le document Pdf a été créé.
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivSuccessModal').modal();", true);

                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UserInfo", "$('#DivExerciesModel').modal();", true);
                                        UpdatePanelExerciesModel.Update();
                                    }
                                    catch (C_EfcaoException)
                                    {
                                        lblErrorMessage.Text = GetObjectLanguageByResourceID("MesgEfcao16"); // Erreur lors l’ajout de la consommation !.
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivErrorModal').modal();", true);
                                        UpdatePanelError.Update();
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
                                try
                                {
                                    DocumentsCreateXmlDoc(balanceKeys, DocumentsRefSaisieAnalyse);

                                    BttnDownLoad.Visible = true;
                                    BttnDownLoad.Enabled = true;
                                    UpdatePanelSuccess.Update();

                                    string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao10") + " " + OrderedProductName + " " + GetObjectLanguageByResourceID("MesgEfcao18") + " " + GetObjectLanguageByResourceID("MesgEfcao12"); // Le Produit : + ... +  // le nouveau document Pdf a été créé.
                                    LblConfirmOrder.Text = ErrorMessage;
                                    RowMesgOrder.Visible = true;

                                    LblSuccessMessgae.Text = GetObjectLanguageByResourceID("MesgEfcao10") + " " + OrderedProductName + " " + GetObjectLanguageByResourceID("MesgEfcao18") + " " + GetObjectLanguageByResourceID("MesgEfcao12"); // Le Produit : + ... +  // le nouveau document Pdf a été créé.
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivSuccessModal').modal();", true);

                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UserInfo", "$('#DivExerciesModel').modal();", true);
                                    UpdatePanelExerciesModel.Update();
                                }
                                catch (C_EfcaoException)
                                {
                                    lblErrorMessage.Text = GetObjectLanguageByResourceID("MesgEfcao17");  // Erreur de création de document xml!.
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Dialog", "$('#DivErrorModal').modal();", true);
                                    UpdatePanelError.Update();
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

            #region -----------------Button Tout Deselectionner click event-----------------
            /// <summary>
            /// Button Tout Deselectionner click event
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void BttnUnSelectAll_Click(object sender, EventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    if (TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie.Count > 0)
                    {
                        foreach (C_ProductDocuments ThProdDocSaisie in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie)
                        {
                            ThProdDocSaisie.Doc_Checked = false;
                        }
                        BindGridViewDoumentSaisie();
                    }

                    if (TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse.Count > 0)
                    {
                        foreach (C_ProductDocuments ThProdDocAnalyse in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse)
                        {
                            ThProdDocAnalyse.Doc_Checked = false;
                        }
                        BindGridViewDoumentAnalyse();
                    }
                    //foreach (GridViewRow MyRow in GridViewDocumentSaisie.Rows)
                    //{
                    //    CheckBox CheckBoxSaisie = (CheckBox)GridViewDocumentSaisie.Rows[MyRow.RowIndex].FindControl("CheckBoxSaisie");
                    //    CheckBoxSaisie.Checked = false;
                    //}

                    //foreach (GridViewRow MyRow in GridViewDocumentAnalyse.Rows)
                    //{
                    //    CheckBox CheckBoxAnalyse = (CheckBox)GridViewDocumentAnalyse.Rows[MyRow.RowIndex].FindControl("CheckBoxAnalyse");
                    //    CheckBoxAnalyse.Checked = false;
                    //}
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

            #region -----------------Button Tout Selectionner click event-----------------
            /// <summary>
            /// Button Tout Selectionner click event
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void BttnSelectAll_Click(object sender, EventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    if (TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie.Count > 0)
                    {
                        foreach (C_ProductDocuments ThProdDoc in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocSaisie)
                        {
                            ThProdDoc.Doc_Checked = true;
                        }
                        BindGridViewDoumentSaisie();
                    }

                    if (TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse.Count > 0)
                    {
                        foreach (C_ProductDocuments ThProdDoc in TheCtrlEfcao.TheSelectedProductToView.TheListWorkDocAnalyse)
                        {
                            ThProdDoc.Doc_Checked = true;
                        }
                        BindGridViewDoumentAnalyse();
                    }
                    //foreach (GridViewRow MyRow in GridViewDocumentSaisie.Rows)
                    //{
                    //    CheckBox CheckBoxSaisie = (CheckBox)GridViewDocumentSaisie.Rows[MyRow.RowIndex].FindControl("CheckBoxSaisie");
                    //    CheckBoxSaisie.Checked = true;
                    //}

                    //foreach (GridViewRow MyRow in GridViewDocumentAnalyse.Rows)
                    //{
                    //    CheckBox CheckBoxAnalyse = (CheckBox)GridViewDocumentAnalyse.Rows[MyRow.RowIndex].FindControl("CheckBoxAnalyse");
                    //    CheckBoxAnalyse.Checked = true;
                    //}

                    BttnOk.Focus();
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

            #region -----------------Button OK focus -----------------
            /// <summary>
            /// Button OK focus 
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void FocusOKButton(object sender, EventArgs e)
            {
                BttnOk.Focus();
            }
            #endregion

            #region -----------------Bttn Telecharger Click-----------------
            /// <summary>
            /// Bttn Telecharger Click 
            /// </summary>
            /// <param name=""></param>
            /// <return></return>
            protected void BttnDownLoad_Click(object sender, EventArgs e)
            {
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"]; 
                try
                {                   
                    string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedProductToView.Prod_Name);
                    string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedCompany.Nom);

                    // replace special char ("[;\\\\/:*?\"<>|&']") with _
                    if (C_Functionglobal.ValidateCompanyName(CompanyName))
                    {
                        CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                    }
                    string root = HttpContext.Current.Server.MapPath("~");
                    string FolderName = ConfigurationManager.AppSettings["SiteName"].ToString();
                    string RootName = @"..\..\wwwroot/" + FolderName + "/PDf/";
                    string docPath1 = Path.GetFullPath(Path.Combine(root, RootName + TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + CompanyName + "/" + ProductName + "-" + CompanyName + ".pdf"));
                    //string docPath1 = Path.GetFullPath(Path.Combine(root, RootName + TheControler.LeControlerConsommation.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + FinalNameArticle + ".pdf"));

                    // -------------------------------------------------------LocalHost-----------------------------------------------------------
                    //string docPath = Path.GetFullPath(Path.Combine(root, @"..\..\wwwroot/BeacBs/Pdf/" + TheControler.LeControlerConsommation.TheConsommationToInsert.Link_User_ID + "/" + TheControler.TheSelectedProductToView.Prod_Name + "/" + FinalNameArticle + ".pdf"));

                    Response.ContentType = "Application/pdf";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + ProductName + "-" + CompanyName + ".pdf");
                    Response.TransmitFile(docPath1);
                    Response.End();
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
                TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                try
                {
                    int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
                    int ProduitID = Convert.ToInt32(Request.QueryString["ProduitID"]);

                    // to create the xml file
                    string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedProductToView.Prod_Name);

                    // to create directory of the company name
                    string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedCompany.Nom);

                    // replace special char ("[;\\\\/:*?\"<>|&']") with _
                    if (C_Functionglobal.ValidateCompanyName(CompanyName))
                    {
                        CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                    }

                    if (TheCtrlEfcao.DocumentsGetDocumentsCreateXmlDoc(balanceKeys, DocumentsRef, ProductName))
                    {
                        // Create the pdf document
                        TheCtrlEfcao.DocumentsGetDocumentsCreatePdfAndAddContentFromXml(UserID, ProductName, CompanyName);
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

        #region -----------------Button Mode Avance Click-----------------
        /// <summary>
        /// Button Mode Avance Click
        /// </summary>
        /// <param name=""></param>
        /// <return></return>
        protected void BttnModeAvance_Click(object sender, EventArgs e)
        {
            TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            try
            {
                string Ffc = "";
                if (!(string.IsNullOrEmpty(Request.QueryString["Ffc"])))
                {
                    Ffc = Request.QueryString["Ffc"].ToString();

                    if (Ffc == "MspD") // Date
                    {
                        DateTime MspEndDate = TheCtrlEfcao.TheCtrlConsom.GetMspEndDate(UserID);
                        DateTime TodayDate = DateTime.Now;

                        if (MspEndDate >= TodayDate)
                        {
                            ViewAvance();
                        }
                        else
                        {
                            string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao22");    // La date de votre abonnement n’est plus valable !               
                            ViewAlert("Avertissement", ErrorMessage, "Warning");
                        }
                    }
                    else if (Ffc == "Msp0") // no membership
                    {
                        ViewAvance();
                    }
                    else // Jeyons
                    {

                        int AvailabeJetons;
                        AvailabeJetons = TheCtrlEfcao.TheCtrlConsom.GetMspRemainigTokens(UserID);
                        if (AvailabeJetons >= TheCtrlEfcao.TheSelectedProductToView.Prod_Price)
                        {
                            ViewAvance();
                        }
                        else
                        {
                            string ErrorMessage = GetObjectLanguageByResourceID("MesgEfcao23");     // Votre abonnement ne possède pas assez de jetons !           
                            ViewAlert("Avertissement", ErrorMessage, "Warning");
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

        public void ViewAvance()
        {
            TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
            try
            {
                BttnGetProdDoc.Visible = true;
                DDListExerciesModels.Visible = true;
                ExerciesGridViewAvance.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "UserInfo", "$('#DivExerciesModel').modal();", true);
                UpdatePanelExerciesModel.Update();
                PanelDocSaisie.Visible = false;
                PanelDocAnalyse.Visible = false;
                PanelExerciesGridView.Visible = true;
                BttnDownLoad.Visible = false;
                BttnSelectAll.Visible = false;
                BttnUnSelectAll.Visible = false;
                BttnOk.Visible = false;
                btnCommandOk.CommandName = "Avance";
                PanelExerciesModel.Visible = true;
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
            DivRecherch.Visible = true;
            LblConfirmOrder.Text = "";
            RowMesgOrder.Visible = false;
        }

        #endregion

        #region -----------------Create check order-----------------

            #region ---------------------------------Check If Product is Allready ordered----------------------------------
            /// <summary>
            /// Check If Product is Allready ordered
            /// </summary>
            public bool ProductHasBeenOrdered()
            {               
                bool nRetour = false;
                try
                {
                    string root = HttpContext.Current.Server.MapPath("~");
                    string FolderName = ConfigurationManager.AppSettings["SiteName"].ToString();
                    string RootName = @"..\..\wwwroot/" + FolderName + "/PDf/";

                    string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedCompany.Nom);

                    // replace special char ("[;\\\\/:*?\"<>|&']") with _
                    if (C_Functionglobal.ValidateCompanyName(CompanyName))
                    {
                        CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                    }

                    string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedProductToView.Prod_Name);

                    string docPath1 = Path.GetFullPath(Path.Combine(root, RootName + TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + CompanyName + "/" + ProductName + "-" + CompanyName + ".pdf"));

                    //string docPath1 = Path.GetFullPath(Path.Combine(root, RootName + TheControler.LeControlerConsommation.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + FinalNameArticle + ".pdf"));

                    // -------------------------------------------------------LocalHost-----------------------------------------------------------
                    //string docPath = Path.GetFullPath(Path.Combine(root, @"..\..\wwwroot/BeacBs/Pdf/" + TheControler.LeControlerConsommation.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + FinalNameArticle + ".pdf"));
                    // ---------------------------------------------------------------------------------------------------------------------------

                    //LabelTest.Text = root + " -----------------" + docPath;
                    // -------------------------------------------------------Visula Studio----------------------------------------------------------
                    // string docPath = Path.GetFullPath(Path.Combine(root, @"..\..\PortailBEAC-Portable/PortailBEAC/Pdf/" + TheControler.LeControlerConsommation.TheConsommationToInsert.UserID + "/" + ProductName + "/" + FinalNameArticle + ".pdf"));
                    // ---------------------------------------------------------------------------------------------------------------------------

                    if (File.Exists(docPath1))
                    {
                        nRetour = true;
                    }
                    else
                    {
                        nRetour = false;
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
                return nRetour;
            }
            #endregion

            #region ---------------------------------Insert Order----------------------------------
            /// <summary>
            /// Insert Order
            /// </summary>
            public void InsertOrder()
            {                
                try
                {
                    string Url = "http://" + ConfigurationManager.AppSettings["AdresseIP"].ToString() + "/" + ConfigurationManager.AppSettings["SiteName"].ToString();

                    TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
                    // ------------------------remote url Local host-------------------------------------------
                    string remoteUrl = Url + "/Pages/FrontOffice/Facture.aspx";
                    //string remoteUrl = "http://192.168.0.104/BeacBs/Pages/FrontOffice/Facture.aspx";
                    // ----------------------------------------------------------------------------------------

                    // ------------------------remote url Visual studio-------------------------------------------
                    //string remoteUrl = "http://localhost:52602/Pages/FrontOffice/Facture.aspx";
                    // ----------------------------------------------------------------------------------------

                    string UserID = Convert.ToString(TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID);
                    string ProduitID = Convert.ToString(TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_Prod_ID);
                    string theCompanyKey = Convert.ToString(TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Company_Key);
                    string Prix = Convert.ToString(TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Unit_Price);
                    string root = HttpContext.Current.Server.MapPath("~");
                    string FolderName = ConfigurationManager.AppSettings["SiteName"].ToString();
                    string RootName = @"..\..\wwwroot/" + FolderName + "/PDf/";
                    string CompanyName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedCompany.Nom);

                    // replace special char ("[;\\\\/:*?\"<>|&']") with _
                    if (C_Functionglobal.ValidateCompanyName(CompanyName))
                    {
                        CompanyName = C_Functionglobal.ReplaceCompanyName(CompanyName);
                    }

                    string ProductName = HttpUtility.HtmlDecode(TheCtrlEfcao.TheSelectedProductToView.Prod_Name);
                    string docPath = Path.GetFullPath(Path.Combine(root, RootName + TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID + "/" + ProductName + "/" + CompanyName + "/" + ProductName + "-" + CompanyName + ".pdf"));

                    TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Article_Name = ProductName + "-" + CompanyName;
                    TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.DOCPATH = docPath;

                    try
                    {
                        int numberOfItem = TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.InsertConsommation();
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

        #region -----------------Consommation-----------------
        /// <summary>
        /// Get List Consommation
        /// </summary>
        /// <returns></returns>
        public string GetListConsommation()
        {
            TheCtrlEfcao = (Ctrl_Efcao)Session["CtrlEfcao"];
            string NumberOfItem = "";          
            C_ListConsoms TheListConsommations = new C_ListConsoms();
            try
            {
                TheListConsommations.GetListConsommation(TheCtrlEfcao.TheCtrlConsom.TheConsommationToInsert.Link_User_ID);

                if (TheListConsommations.Count == 1)
                {
                    NumberOfItem = TheListConsommations.Count.ToString() + " Item";
                }
                else if (TheListConsommations.Count > 1)
                {
                    NumberOfItem = TheListConsommations.Count.ToString() + " Items";
                }
                else
                {
                    NumberOfItem = "0 Item";
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

            return NumberOfItem;
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProduitsPublics.aspx.cs" Inherits="EFCAO.ProduitsPublics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="OR.system" />
    <meta name="author" content="Beac Portial" />
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <!-- Bootstrap 3.3.5 -->
    <link rel="stylesheet" href="AtlAdminStyle/bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link href="AtlAdminStyle/Others/4.4.0/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link href="AtlAdminStyle/Others/4.4.0/ionicons.min.css" rel="stylesheet" />
    <!-- Theme style -->
    <link rel="stylesheet" href="AtlAdminStyle/dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
         folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="AtlAdminStyle/dist/css/skins/_all-skins.min.css" />
    <!-- iCheck -->
    <link rel="stylesheet" href="AtlAdminStyle/plugins/iCheck/flat/blue.css" />
    <!-- Morris chart -->
    <link rel="stylesheet" href="AtlAdminStyle/plugins/morris/morris.css" />
    <!-- jvectormap -->
    <link rel="stylesheet" href="AtlAdminStyle/plugins/jvectormap/jquery-jvectormap-1.2.2.css"/>
    <!-- bootstrap wysihtml5 - text editor -->
    <link rel="stylesheet" href="AtlAdminStyle/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"/>

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link href="AtlAdminStyle/GridView.css" rel="stylesheet" />

    <!-- For the Dialog -->
    <script src="https://code.jquery.com/jquery-2.1.4.min.js"></script> 
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script> 
    <script src="https://cdnjs.cloudflare.com/ajax/libs/prettify/r298/run_prettify.min.js"></script> 
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap3-dialog/1.34.7/css/bootstrap-dialog.min.css" rel="stylesheet" type="text/css" /> 
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap3-dialog/1.34.7/js/bootstrap-dialog.min.js"></script> 
    <!-- / For the Dialog -->

    <script type="text/javascript">
        function messageAlert(title, message, duree, DialogType) {
            var messageAlert = new BootstrapDialog({
                buttons: [{
                    label: 'Fermer',
                    action: function (dialogItself) {
                        dialogItself.close();
                    }
                }]
            });
            messageAlert.setTitle(title);
            messageAlert.setMessage(message)
            if (DialogType == 'Warning') {
                messageAlert.setType(BootstrapDialog.TYPE_WARNING);
            }
            else if (DialogType == 'Success') {
                messageAlert.setType(BootstrapDialog.TYPE_SUCCESS);
            }

            else {
                messageAlert.setType(BootstrapDialog.TYPE_DANGER);
            }

            messageAlert.open();
            if (typeof (duree) == "undefined") {
                duree = 2000;
            }

            //setTimeout(function () {
            //    messageAlert.close();
            //}, duree);

            if (DialogType == 'Warning' || DialogType == 'Success') {
                setTimeout(function () {
                    messageAlert.close();
                }, duree);
            }
        }

    </script>

    <script>

        $('#DivExerciesModel').on('show.bs.modal', function (e) {
            if (!data) return e.preventDefault() // stops modal from being shown
        })

        $('#DivAvertissementModal').on('show.bs.modal', function (e) {
            if (!data) return e.preventDefault() // stops modal from being shown
        })

        $('#DivSuccessModal').on('show.bs.modal', function (e) {
            if (!data) return e.preventDefault() // stops modal from being shown
        })
        $('#DivErrorModal').on('show.bs.modal', function (e) {
            if (!data) return e.preventDefault() // stops modal from being shown
        })

        $('#DivLoadingModal').on('show.bs.modal', function (e) {
            if (!data) return e.preventDefault() // stops modal from being shown
            $("#progressbar1").css("width", "100%");
        })
    </script>

    <script type="text/javascript" >
        function HideLoading() {
            $('#DivLoadingModal').modal('hide');
        }
    </script>

    <script type="text/javascript" >
        function ViewLoading() {
            $('#DivAvertissementModal').modal('hide');
            $('#DivLoadingModal').modal('show');
        }
    </script>

     <style>
        .dropdownWidth {
            width:35%;
        }
    </style>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Etats financiers conforme aux originaux</title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="LkBttnSearchComp" defaultfocus="TBoxRecherche1">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- Content Header (Page header) -->
        <section class="content-header" >
          <h1 style="padding-bottom:10px">
             <asp:Label ID="LblEfcaoTitle" runat="server" Text="Etats Financiers conforme aux originaux"></asp:Label>                           
             <small><asp:Label ID="LblEfcaoSubTitle" runat="server"></asp:Label></small>
          </h1>
          <div class="navbar navbar-default" id="DivRecherch" runat="server">           
            <div class="navbar-form navbar-left">
                <div class="form-group">                                                    
                   <asp:Label ID="LblSearchComp" runat="server" Text="Recherche de sociétés :"></asp:Label>
                    <asp:TextBox ID="TBoxRecherche" runat="server" class="form-control" ValidationGroup="SearchComapnies"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVTBoxRecherche" runat="server" ErrorMessage="*" ControlToValidate="TBoxRecherche" ForeColor="Red" Font-Size="Small" ValidationGroup="SearchComapnies"></asp:RequiredFieldValidator>
                    <asp:LinkButton ID="LkBttnSearchComp" runat="server" CssClass="btn btn-social-icon btn-default btn-md" ValidationGroup="SearchComapnies" OnClick="LkBttnSearchComp_Click" ToolTip="Recherche" OnClientClick="if(Page_ClientValidate()) ViewLoading();"> <span aria-hidden="true" class="fa fa-search"></span></asp:LinkButton> 
                </div>
            </div>          
          </div>
        </section>

        <!-- Main content -->
        <section class="content">
        <asp:Panel ID="PanelSearchCompGV" runat="server" Visible="false"> 
        <div class="row">
            <!-- left column -->
            <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title"><asp:Label ID="LblEfcaoCompTitle" runat="server" Text="Sociétés"></asp:Label></h3>
                        </div><!-- /.box-header -->
                    <div class="box-body">                         
                        <asp:GridView ID="SearchCompanyGridView" runat="server" AutoGenerateColumns="False" PageSize="10" onrowcommand="SearchCompanyGridView_RowCommand" 
                            onpageindexchanging="SearchCompanyGridView_PageIndexChanging" AllowPaging="True" DataKeyNames="Key" onrowdatabound="SearchCompanyGridView_RowDataBound" CssClass="table table-bordered table-hover">
               
                                <rowstyle backcolor="#c5edfc" />
                                <alternatingrowstyle backcolor="white"  />
                                <Columns>
                                <asp:TemplateField Visible="false"  HeaderText="Company Key" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label id="CompanyID" runat ="server" text='<%# Eval("key")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField  Visible="false" HeaderText="lock Type" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label id="CompanylockType" runat ="server" text='<%# Eval("lockType")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Nom de la société" DataField="Nom" ReadOnly="true" ItemStyle-Width="40%"/>
                                <asp:BoundField HeaderText="Identifiant principal" DataField="IdExterne" ReadOnly="true" ItemStyle-Width="15%"/>
		                        <asp:BoundField HeaderText="Modèle" DataField="Modele" ReadOnly="true"  ItemStyle-Width="15%"/>
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="LkBttnCompEdit" runat="server" CssClass="btn btn-social-icon btn-primary btn-xs" ToolTip="Editer"  CommandName="EditCompany" OnClientClick="ViewLoading();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-edit"></span>
                                    </asp:LinkButton>                  
                                    </ItemTemplate> 
                                </asp:TemplateField>                
                            </Columns>
                            <SelectedRowStyle BackColor="blue" />
                            <PagerStyle CssClass="pagination-ys"/>
                               <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" FirstPageText="<<" LastPageText=">>" />
                        </asp:GridView>
                    </div><!-- /.box-body -->
                </div><!-- /.box -->
            </div>
          </div>
         </asp:Panel>

         <asp:Panel ID="PanelCompAddress" runat="server" visible="false">
              <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h4 class="box-title">
                                <asp:Label ID="LblMainExercicesCompName" runat="server"></asp:Label>
                                 <br />
                                 <small><asp:Label ID="LblCompIdentifiant" runat="server" Text="Identifiant :"></asp:Label> <asp:Label ID="LblCompIdentifiantVal" runat="server"></asp:Label></small>
                                 <br />
                                 <small><asp:Label ID="LblCompAddressVal" runat="server" ></asp:Label></small>
                                 <br />
                                 <small><asp:Label ID="LblCompAddressVal1" runat="server" ></asp:Label></small><br />
                                 <small>
                                 <asp:Label ID="LblCompZipVal" runat="server"></asp:Label>
                                 &nbsp;<asp:Label ID="LblCompCityVal" runat="server"></asp:Label>
                                 </small>
                                 <br />
                                <small><asp:Label ID="LblCompRegionVal" runat="server" ></asp:Label>  </small>
                                 <br />
                                <small><asp:Label ID="LblCompCountryVal" runat="server" ></asp:Label></small>
                            </h4>
                        </div>
                    </div>
                </div>
             </div>
          </asp:Panel>

          <asp:Panel ID="PanelGridViewExceriesMain" runat="server" visible="false">
            <div class="row">
            <!-- left column -->
            <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-primary">
                        
                         <div class="box-header with-border">
                            <h4 class="box-title">
                                <asp:Label ID="LblEfcaoMod" runat="server" Text="Modèles :"></asp:Label>
                            </h4>
                        </div>
                        <div class="box-body">                              
                            <asp:DropDownList ID="DDListExerciesModels" runat="server" AutoPostBack="True" CssClass="form-control dropdownWidth" onselectedindexchanged="DDListExerciesModels_SelectedIndexChanged">
                                </asp:DropDownList>
                        </div>
                        <div class="box-header with-border" id="DivGridExerciesTitle" runat="server">
                            <h4 class="box-title">
                                 <asp:Label ID="LblCompExcericesTitle" runat="server" Text="Liste des trois derniers exercices disponible"></asp:Label>
                            </h4>
                        </div><!-- /.box-header -->

                        <div class="box-body" id="DivGridExercies" runat="server">  
                          <asp:GridView ID="ExerciesGridView" runat="server" AutoGenerateColumns="false" Visible="true" PageSize="15" AllowPaging="true" 
                            onpageindexchanging="ExerciesGridView_PageIndexChanging" onrowcommand="ExerciesGridView_RowCommand" CssClass="table table-bordered table-hover" OnRowDataBound="ExerciesGridView_RowDataBound">
                            <rowstyle backcolor="#c5edfc" />
                                <alternatingrowstyle backcolor="white"  />
                                <Columns>
                                    <asp:BoundField HeaderText="Type" DataField="TypeExercice" ReadOnly="true"  ControlStyle-CssClass="texte"/>
                                    <asp:BoundField HeaderText="Verrou" DataField="Verrouille" ReadOnly="true"  ControlStyle-CssClass="texte"/>
                     
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="CheckDate" Checked='<%#Eval("RowChecked") %>' AutoPostBack="True" oncheckedchanged="CheckDate_CheckedChanged"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                  
                                    <asp:BoundField HeaderText="Date" DataField="DateExer" ReadOnly="true"  ControlStyle-CssClass="texte"/>
                                    <asp:BoundField HeaderText="Unité" DataField="Unite" ReadOnly="true"  ControlStyle-CssClass="texte"/>
                                    <asp:BoundField HeaderText="Devise" DataField="Currency" ControlStyle-CssClass="texte" ControlStyle-Width="40"/>
                                    <asp:BoundField HeaderText="Durée" DataField="DureeExer" ReadOnly="true"  ControlStyle-CssClass="texte"/>
		                            <asp:BoundField HeaderText="Modèle" DataField="Model" ReadOnly="true"  ControlStyle-CssClass="texte"/>
                                    <asp:TemplateField HeaderText="Key"  Visible="false">
                                    <ItemTemplate >
                                    <asp:Label ID="ExerciesKey" runat="server" Text='<%#Eval("CleUnik") %>'></asp:Label>      
                                    </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                            <PagerStyle Font-Names="Geneva, Tahoma, Verdana, sans-serif" Font-Size="16px" />
                        </asp:GridView>     
                    </div><!-- /.box-body -->
                </div><!-- /.box -->                       
            </div>
          </div>
          <div class="row">
            <!-- left column -->
            <div class="col-sm-12" style="text-align:right">
                <asp:Button ID="BttnGetDocImedit" runat="server" Text="Obtenir les documents" CssClass="btn btn-primary" OnClick="BttnGetDocImedit_Click" OnClientClick="ViewLoading();"/> 
                <asp:Button ID="BttnNewSearch" runat="server" Text="Nouvelle recherche" CssClass="btn btn-primary" OnClick="BttnNewSearch_Click"/>
               </div>           
          </div>
          </asp:Panel> 
          
          <asp:Panel ID="PanelDocSaisie" runat="server" Visible="false"> 
            <!-- Page Heading -->
            <div class="row" >
                <div class="col-lg-12">
                    <div class="h3">
                        Documents : 
                        <small>
                            <asp:Label ID="LblEfcaoDocSaisie" runat="server" Text="Documents saisie"></asp:Label> 
                            <asp:Label ID="LblEfcaoModeleSaisie" runat="server" Text=", Modèle Saisie :" ></asp:Label>
                            <asp:Label ID="LblModeleSaisieValue" runat="server" ></asp:Label>                                       
                        </small>
                    </div>                  
                </div>
            </div><!-- / Page Heading -->

            <div class="row" >
                <div class="col-lg-12">              
                        <asp:GridView ID="GridViewDocumentSaisie" runat="server" AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="GridViewDocumentSaisie_PageIndexChanging" PageSize="6" CssClass="table table-bordered table-hover" OnRowDataBound="GridViewDocumentSaisie_RowDataBound">
                        <rowstyle backcolor="#c5edfc" />
                        <alternatingrowstyle backcolor="white" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sélectionnez" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxSaisie" runat="server" AutoPostBack="true" Checked="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField ControlStyle-CssClass="texte" DataField="Ades_Doc_ID" HeaderText="ID" ReadOnly="true" />
                            <asp:BoundField ControlStyle-CssClass="texte" DataField="Doc_Name" HeaderText="Label" ReadOnly="true" />
                            <asp:BoundField DataField="Doc_EDW" HeaderText="Edw" ItemStyle-Width="35%" />
                        </Columns>
                        <PagerStyle Font-Names="Geneva, Tahoma, Verdana, sans-serif" Font-Size="16px" />
                    </asp:GridView>
                </div>                
            </div>
        </asp:Panel>

        <asp:Panel ID="PanelDocAnalyse" runat="server" Visible="false">
            <!-- Page Heading -->
            <div class="row" >
                <div class="col-lg-12">
                    <div class="h3">
                        Documents : 
                        <small>
                            <asp:Label ID="LblEfcaoDocAnalyse" runat="server" CssClass="TextShadow" Text="Documents analyse"></asp:Label> 
                            <asp:Label ID="LblEfcaoModeleAnalyse" runat="server" Text=", Modèle Analyse :" ></asp:Label>
                            <asp:Label ID="LblModeleAnayseValue" runat="server" ></asp:Label>
                        </small>
                    </div>                  
                </div>
            </div><!-- / Page Heading -->

            <div class="row">
                <div class="col-lg-12">
                    <asp:GridView ID="GridViewDocumentAnalyse" runat="server" AllowPaging="true" AutoGenerateColumns="false" CssClass="table table-bordered table-hover" OnPageIndexChanging="GridViewDocumentAnalyse_PageIndexChanging" PageSize="6" OnRowDataBound="GridViewDocumentAnalyse_RowDataBound">
                        <rowstyle backcolor="#c5edfc" />
                        <alternatingrowstyle backcolor="white" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sélectionnez" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBoxAnalyse" runat="server" AutoPostBack="true" Checked="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField ControlStyle-CssClass="texte" DataField="Ades_Doc_ID" HeaderText="ID" ReadOnly="true" />
                            <asp:BoundField ControlStyle-CssClass="texte" DataField="Doc_Name" HeaderText="Label" ReadOnly="true" />
                            <asp:BoundField DataField="Doc_EDW" HeaderText="Edw"/>
                        </Columns>
                        <PagerStyle Font-Names="Geneva, Tahoma, Verdana, sans-serif" Font-Size="16px" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>

        </section>

        <!-- Div Modal Delete record-->
        <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="DivAvertissementModal" style="z-index:1600;">
            <div class="modal-dialog mo">
                <asp:UpdatePanel ID="UpdatePanelAvertissement" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional"  >
                  <ContentTemplate>          
                    <div class="modal-content">
                        <div class="modal-header panel-heading bg-yellow" >
                        
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                            <div class="modal-title">
                                <asp:Label ID="lblAvertisTitle" runat="server" Text="Avertissement"></asp:Label>                                                                                             
                            </div>                         
                        </div>
                        <!-- modal-body --> 
                        <div class="modal-body">  
                             <asp:Label ID="lblAvertisMessage" runat="server"></asp:Label>                                    
                        </div><!-- modal-body --> 

                        <!-- modal-footer -->
                        <div class="modal-footer">                                         
                                <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true" id="BttnAvertisClose" runat="server">Close</button>
                        </div><!-- modal-footer -->
                    </div>
                  </ContentTemplate>
                    <Triggers>                                                                                     
                    </Triggers>
                </asp:UpdatePanel>    
                                                   
            </div>
        </div><!-- / Div Modal Delete record-->

        <!-- Div Modal Delete record-->
        <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="DivErrorModal" style="z-index:1600;">
            <div class="modal-dialog mo">
                <asp:UpdatePanel ID="UpdatePanelError" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional"  >
                  <ContentTemplate>          
                    <div class="modal-content">
                        <div class="modal-header panel-heading bg-red" >
                        
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                            <div class="modal-title">
                                <asp:Label ID="lblErrorTitle" runat="server" Text="Erreur"></asp:Label>                                                                                             
                            </div>                         
                        </div>
                        <!-- modal-body --> 
                        <div class="modal-body">  
                             <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>                                    
                        </div><!-- modal-body --> 

                        <!-- modal-footer -->
                        <div class="modal-footer">                                         
                                <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true" id="BttnErrorClose" runat="server">Close</button>
                        </div><!-- modal-footer -->
                    </div>
                  </ContentTemplate>
                    <Triggers>                                                                                     
                    </Triggers>
                </asp:UpdatePanel>    
                                                   
            </div>
        </div><!-- / Div Modal Delete record-->

        <!-- Div Modal Delete record-->
        <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="DivLoadingModal" style="z-index:1600;">
            <div class="modal-dialog mo">
                   
                    <div class="modal-content">
                        <div class="modal-header panel-heading bg-gray-light" >
                        
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                            <div class="modal-title">
                                <asp:Label ID="LblLoadingTitle" runat="server" Text="Loading...."></asp:Label>                                                                                             
                            </div>                         
                        </div>
                        <!-- modal-body --> 
                        <div class="modal-body">  
                                   <div class="progress progress-striped">
                                        <div class="progress-bar progress-bar-primary active" style="width: 100%" id="progressbar1">
                                            Processing...
                                        </div>
                                    </div>                             
                        </div><!-- modal-body --> 
                    </div>                       
            </div>
        </div><!-- / Div Modal Delete record-->

    <!-- Div Modal Delete record-->
    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="DivSuccessModal" style="z-index:1600;">
        <div class="modal-dialog mo">
            <asp:UpdatePanel ID="UpdatePanelSuccess" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional"  >
              <ContentTemplate>          
                <div class="modal-content">
                    <div class="modal-header panel-heading bg-green" >
                        
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>

                        <div class="modal-title">
                            <asp:Label ID="LblSuccessTitle" runat="server" Text="Success"></asp:Label>                                                                                             
                        </div>                         
                    </div>
                    <!-- modal-body --> 
                    <div class="modal-body">  
                            <asp:Label ID="LblSuccessMessgae" runat="server"></asp:Label>                                    
                    </div><!-- modal-body --> 

                    <!-- modal-footer -->
                    <div class="modal-footer">                                         
                            <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true" id="BttnSuccessClose" runat="server">Close</button>
                    </div><!-- modal-footer -->
                </div>
              </ContentTemplate>
                <Triggers>                                                                                     
                </Triggers>
            </asp:UpdatePanel>    
                                                   
        </div>
    </div><!-- / Div Modal Delete record-->

    <%--<!-- Div Membership Modif-->
    <div class="modal fade bs-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="DivExerciesModel" data-backdrop="static" >
        <div class="modal-dialog modal-lg">
            <asp:UpdatePanel ID="UpdatePanelExerciesModel" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional"  >
              <ContentTemplate>
                <div class="modal-content panel-default">
                
                <div class="modal-header panel-heading">                      
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <div class="modal-title">
                        <!-- Page Heading -->                                                                         
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <asp:Label ID="LblEfcaoExercies" runat="server"  CssClass="h4" Text="Exercices :"></asp:Label>
                                    <small><asp:Label ID="LblEfcaoExerModel" runat="server" Text="Exercices Modèles"></asp:Label></small>
                                </div>                       
                            </div>
                        </div>                                            
                        <!-- /Page Heading -->             
                    </div>                         
                </div>

                <!-- modal-body --> 
                <div class="modal-body"> 

                    <!-- panel User Info Modi-->
                    <asp:Panel ID="PanelExerciesModel" runat="server">
                        <!-- .row -->
                        <div class="row">
                            <div class="col-lg-1" style="text-align:right">
                                
                                                                    
                            </div>
                            <div class="col-lg-7">                                
                                
                            </div>
                            <div class="col-lg-4">
                                <asp:Button ID="BttnEfcaoGetProdDoc" runat="server" CssClass="btn btn-primary" OnClick="BttnGetListProdDocumentsSaisieAnalyse_Click" Text="Obtenir les documents" OnClientClick="ViewLoading();"/>
                            </div>
                        </div>
                    </asp:Panel>
                    <br />

                    <!-- panel User Info Modi-->
                    <asp:Panel ID="PanelExerciesGridView" runat="server" Visible="false">
                                              
                    </asp:Panel>


                    

                </div><!-- modal-body --> 
                 
                      
                <!-- modal-footer -->
                <div class="modal-footer">
                    <asp:Button ID="BttnEfcaoSelectAll" runat="server" Text="Tout sélectionner" onclick="BttnToutSelectionner_Click" CssClass="btn btn-primary" />                          
                    <asp:Button ID="BttnEfcaoUnSelectAll" runat="server" Text="Tout desélectionner" onclick="BttnToutDeSelectionner_Click" CssClass="btn btn-primary" />
                    <asp:Button ID="BttnEfcaoOk" runat="server" Text="Ok" onclick="BttnOk_Click" CssClass="btn btn-primary" OnClientClick="ViewLoading();"/>
                    <asp:Button ID="BttnEfcaoDownLoad" runat="server" Text="Télécharger" CssClass="btn btn-primary" Enabled="False" OnClick="BttnTelecharger_Click" Visible="False"/>

                    <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Close</button>
                </div><!-- modal-footer -->
            </div>
   
            </ContentTemplate>
                <Triggers>                                        
                   <asp:PostBackTrigger ControlID="BttnEfcaoDownLoad"/>                                         
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div><!-- / Div Membership Modif-->--%>


    <!-- jQuery 2.1.4 -->
    <script src="AtlAdminStyle/plugins/jQuery/jQuery-2.1.4.min.js"></script>
    <!-- jQuery UI 1.11.4 -->
    <script src="AtlAdminStyle/Others/1.11.4/jquery-ui.min.js"></script>
    <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
    <script>
        $.widget.bridge('uibutton', $.ui.button);
    </script>
    <!-- Bootstrap 3.3.5 -->
    <script src="AtlAdminStyle/bootstrap/js/bootstrap.min.js"></script>
    <!-- Morris.js charts -->
    <script src="AtlAdminStyle/Others/raphael/raphael-min.js"></script>
    <script src="AtlAdminStyle/plugins/morris/morris.min.js"></script>
    <!-- Sparkline -->
    <script src="AtlAdminStyle/plugins/sparkline/jquery.sparkline.min.js"></script>
    <!-- jvectormap -->
    <script src="AtlAdminStyle/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="AtlAdminStyle/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"></script>
    <!-- jQuery Knob Chart -->
    <script src="AtlAdminStyle/plugins/knob/jquery.knob.js"></script> 
    <script src="AtlAdminStyle/Others/raphael/moment.min.js"></script>
    <script src="AtlAdminStyle/plugins/daterangepicker/daterangepicker.js"></script>
    <!-- datepicker -->
    <script src="AtlAdminStyle/plugins/datepicker/bootstrap-datepicker.js"></script>
    <!-- Bootstrap WYSIHTML5 -->
    <script src="AtlAdminStyle/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"></script>
    <!-- Slimscroll -->
    <script src="AtlAdminStyle/plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <!-- AdminLTE App -->
    <script src="AtlAdminStyle/dist/js/app.min.js"></script>
    </form>
</body>
</html>

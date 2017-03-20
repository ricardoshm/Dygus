<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsereClientes.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InsereClientes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inserir Novo Cliente</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />


    <!-- Bootstrap -->
    <link rel="stylesheet" href="../css/bootstrap.min.css" />
    <!-- Bootstrap responsive -->
    <link rel="stylesheet" href="../css/bootstrap-responsive.min.css" />
    <!-- small charts plugin -->
    <link rel="stylesheet" href="../css/jquery.easy-pie-chart.css" />
    <!-- calendar plugin -->
    <link rel="stylesheet" href="../css/fullcalendar.css" />
    <!-- Calendar printable -->
    <link rel="stylesheet" href="../css/fullcalendar.print.css" media="print" />
    <!-- chosen plugin -->
    <link rel="stylesheet" href="../css/chosen.css" />
    <!-- CSS for Growl like notifications -->
    <link rel="stylesheet" href="../css/jquery.gritter.css" />
    <!-- Theme CSS -->
    <!--[if !IE]> -->
    <link rel="stylesheet" href="../css/style.css" />
    <!-- <![endif]-->
    <!--[if IE]>
	<link rel="stylesheet" href="../css/style_ie.css" />
	<![endif]-->

    <!-- jQuery -->
    <script src="../js/jquery.min.js"></script>
    <!-- smoother animations -->
    <script src="../js/jquery.easing.min.js"></script>
    <!-- Bootstrap -->
    <script src="../js/bootstrap.min.js"></script>
    <!-- small charts plugin -->
    <script src="../js/jquery.easy-pie-chart.min.js"></script>
    <!-- Charts plugin -->
    <script src="../js/jquery.flot.min.js"></script>
    <!-- Pie charts plugin -->
    <script src="../js/jquery.flot.pie.min.js"></script>
    <!-- Bar charts plugin -->
    <script src="../js/jquery.flot.bar.order.min.js"></script>
    <!-- Charts resizable plugin -->
    <script src="../js/jquery.flot.resize.min.js"></script>
    <!-- calendar plugin -->
    <script src="../js/fullcalendar.min.js"></script>
    <!-- chosen plugin -->
    <script src="../js/chosen.jquery.min.js"></script>
    <!-- Scrollable navigation -->
    <script src="../js/jquery.nicescroll.min.js"></script>
    <!-- Growl Like notifications -->
    <script src="../js/jquery.gritter.min.js"></script>

    <!-- Just for demonstration -->
    <script src="../js/demonstration.min.js"></script>
    <!-- Theme framework -->
    <script src="../js/eakroko.min.js"></script>
    <!-- Theme scripts -->
    <script src="../js/application.min.js"></script>
    <link rel="shortcut icon" href="./favicon.ico" />
    <link rel="apple-touch-icon-precomposed" href="../apple-touch-icon-precomposed.png" />

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style type="text/css">
        html, body, form
        {
            padding: 0;
            margin: 0;
            height: 100%;
            width: 100%;
        }

        body
        {
            font: normal 11px Arial, Verdana, Sans-serif;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Assembly="Telerik.Web.UI"
                    Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI"
                    Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
                <asp:ScriptReference Assembly="Telerik.Web.UI"
                    Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function GetRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow; //Will work in Moz in all cases, including clasic dialog
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow; //IE (and Moz az well)

                    return oWindow;
                }
                function CloseAndReload() {
                    var oWnd = GetRadWindow();
                    oWnd.BrowserWindow.location.reload();
                    oWnd.close();
                }

                function AdjustRadWidow() {
                    var oWindow = GetRadWindow();
                    setTimeout(function () { oWindow.autoSize(true); if ($telerik.isChrome || $telerik.isSafari) ChromeSafariFix(oWindow); }, 800);
                }

                //fix for Chrome/Safari due to absolute positioned popup not counted as part of the content page layout
                function ChromeSafariFix(oWindow) {
                    var iframe = oWindow.get_contentFrame();
                    var body = iframe.contentWindow.document.body;

                    setTimeout(function () {
                        var height = body.scrollHeight;
                        var width = body.scrollWidth;

                        var iframeBounds = $telerik.getBounds(iframe);
                        var heightDelta = height - iframeBounds.height;
                        var widthDelta = width - iframeBounds.width;

                        if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);
                        if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);
                        oWindow.center();

                    }, 310);
                }
            </script>
        </telerik:RadCodeBlock>
        <div id="content">
            <div class="container-fluid" id="content-area">
                <div class="row-fluid">
                    <div class="span12">
                        <div class="box">
                            <div class="box-body box-body-nopadding">
                                <div class="form-horizontal">
                                    <div class="control-group">
                                        <div class="alert alert-success" id="sucesso" runat="server" visible="false">
                                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                                            <label runat="server" id="sucessoMessage" visible="false"></label>
                                        </div>
                                        <div class="alert alert-error" id="erro" runat="server" visible="false">
                                            <button type="button" class="close" data-dismiss="alert">&times;</button>
                                            <label runat="server" id="errorMessage" visible="false"></label>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Código de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ForeColor="Green" Font-Bold="true" ID="tbcodcliente" ReadOnly="true" CssClass="input-xlarge"></telerik:RadTextBox>
                                            <span class="help-block">Este código será gerado automaticamente</span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Tipo de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadComboBox runat="server" ID="ddltipocliente" Width="285" Skin="MetroTouch" CssClass="input-xlarge"></telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Nome de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Nome Completo" CssClass="input-xlarge"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Morada de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbmorada" TextMode="MultiLine" placeholder="Morada" CssClass="input-xlarge"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Código Postal de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadMaskedTextBox Mask="####-###" Width="285" Skin="MetroTouch" runat="server" ID="tbcodpostal" placeholder="Código Postal" MaxLength="8" CssClass="input-xlarge"></telerik:RadMaskedTextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Localidade de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tblocalidade" placeholder="Localidade" CssClass="input-xlarge"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Contacto de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadNumericTextBox Type="Number" NumberFormat-DecimalDigits="0" Width="285" Skin="MetroTouch" runat="server" ID="tbcontacto" MaxLength="9" placeholder="Contacto telefónico" CssClass="input-xlarge"></telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Email de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbemail" placeholder="Email" CssClass="input-xlarge"></telerik:RadTextBox>
                                            <span class="help-block">Será gerada nova conta de cliente com este email</span>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">NIF de Cliente</label>
                                        <div class="controls">
                                            <telerik:RadNumericTextBox Type="Number" NumberFormat-DecimalDigits="0" Width="285" Skin="MetroTouch" runat="server" ID="tbnif" MaxLength="9" placeholder="NIF" CssClass="input-xlarge"></telerik:RadNumericTextBox>
                                        </div>
                                    </div>
                                    <div class="control-group">
                                        <label for="textfield" class="control-label">Observações</label>
                                        <div class="controls">
                                            <telerik:RadTextBox Height="85" Width="285" Skin="MetroTouch" runat="server" ID="tbobs" TextMode="MultiLine" placeholder="Observações" CssClass="input-xlarge"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                    <div class="form-actions">
                                        <asp:Button runat="server" ID="btnGrava" Text="Gravar" CssClass="button button-basic-blue" OnClick="btnGrava_Click" />
                                        <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="button button-basic" OnClick="btnLimpar_Click" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </form>
</body>
</html>

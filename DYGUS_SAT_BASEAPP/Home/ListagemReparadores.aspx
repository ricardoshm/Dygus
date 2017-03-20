<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListagemReparadores.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListagemReparadores" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listagem de Reparadores</title>
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
    <form id="form2" runat="server">
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
                </script>
            </telerik:RadCodeBlock>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="listagemReparadores">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="listagemReparadores" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadGrid AllowFilteringByColumn="true" runat="server" Skin="MetroTouch" ID="listagemReparadores" OnNeedDataSource="listagemReparadores_NeedDataSource" CellSpacing="0" GridLines="None" AllowPaging="True" Culture="pt-PT">
                <MasterTableView NoMasterRecordsText="Não existem reparadores registados" NoDetailRecordsText="Não existem reparadores registados" AutoGenerateColumns="False">
                    <RowIndicatorColumn CurrentFilterFunction="NoFilter" FilterListOptions="VaryByDataType"
                        Visible="False">
                        <HeaderStyle Width="20px" />
                    </RowIndicatorColumn>

                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" ShowFilterIcon="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <Columns>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MORADA" ReadOnly="True" HeaderText="Morada" SortExpression="MORADA" UniqueName="MORADA" FilterControlAltText="Filter MORADA column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODPOSTAL" ReadOnly="True" HeaderText="Código Postal" SortExpression="CODPOSTAL" UniqueName="CODPOSTAL" FilterControlAltText="Filter CODPOSTAL column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOCALIDADE" ReadOnly="True" HeaderText="Localidade" SortExpression="LOCALIDADE" UniqueName="LOCALIDADE" FilterControlAltText="Filter LOCALIDADE column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="TELEFONE" ReadOnly="True" HeaderText="Telefone" SortExpression="TELEFONE" UniqueName="TELEFONE" FilterControlAltText="Filter TELEFONE column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NIF" ReadOnly="True" HeaderText="Nif" SortExpression="NIF" UniqueName="NIF" DataType="System.Int32" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="OBSERVACOES" ReadOnly="True" HeaderText="Observações" SortExpression="OBSERVACOES" UniqueName="OBSERVACOES" FilterControlAltText="Filter OBSERVACOES column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" Visible="false" DataField="DATA_ULTIMA_MODIFICACAO" ReadOnly="True" HeaderText="DATA_ULTIMA_MODIFICACAO" SortExpression="DATA_ULTIMA_MODIFICACAO" UniqueName="DATA_ULTIMA_MODIFICACAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_ULTIMA_MODIFICACAO column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" Visible="false" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="DATA_REGISTO" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" Visible="false" DataField="USERID" ReadOnly="True" HeaderText="USERID" SortExpression="USERID" UniqueName="USERID" DataType="System.Guid" FilterControlAltText="Filter USERID column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" Visible="false" DataField="CONTA_ACTIVA" ReadOnly="True" HeaderText="CONTA_ACTIVA" SortExpression="CONTA_ACTIVA" UniqueName="CONTA_ACTIVA" FilterControlAltText="Filter CONTA_ACTIVA column"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <div style="margin-left: 5px;">
                <asp:Button runat="server" ID="btnCarrega" Text="Escolher Reparador" OnClientClick="CloseAndReload();" CssClass="button button-basic-blue" />
            </div>
    </form>
</body>
</html>

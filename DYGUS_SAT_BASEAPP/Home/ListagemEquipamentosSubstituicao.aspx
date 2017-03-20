<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListagemEquipamentosSubstituicao.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListagemEquipamentosSubstituicao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listagem de Equipamentos de Substituição</title>
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
        <div>
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
                    <telerik:AjaxSetting AjaxControlID="ListagemEquipamentos">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="ListagemEquipamentos" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadGrid AllowFilteringByColumn="true" runat="server" Skin="MetroTouch" ID="ListagemEquipamentos" OnNeedDataSource="ListagemEquipamentos_NeedDataSource" CellSpacing="0" GridLines="None">
                <MasterTableView NoMasterRecordsText="Não existem equipamentos de substituição registados" NoDetailRecordsText="Não existem equipamentos de substituição registados" AutoGenerateColumns="False">
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
                        <telerik:GridBoundColumn DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CODIGO" ReadOnly="True" HeaderText="CODIGO" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MARCA" ReadOnly="True" HeaderText="MARCA" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MODELO" ReadOnly="True" HeaderText="MODELO" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="IMEI" ReadOnly="True" HeaderText="IMEI" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                        <telerik:GridCheckBoxColumn DataField="BATERIA" ReadOnly="True" HeaderText="BATERIA" SortExpression="BATERIA" UniqueName="BATERIA" DataType="System.Boolean" FilterControlAltText="Filter BATERIA column"></telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="CARTAO_MEMORIA" ReadOnly="True" HeaderText="CARTAO_MEMORIA" SortExpression="CARTAO_MEMORIA" UniqueName="CARTAO_MEMORIA" DataType="System.Boolean" FilterControlAltText="Filter CARTAO_MEMORIA column"></telerik:GridCheckBoxColumn>
                        <telerik:GridCheckBoxColumn DataField="CARREGADOR" ReadOnly="True" HeaderText="CARREGADOR" SortExpression="CARREGADOR" UniqueName="CARREGADOR" DataType="System.Boolean" FilterControlAltText="Filter CARREGADOR column"></telerik:GridCheckBoxColumn>
                    </Columns>
                    <Columns>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <div style="margin-left: 5px;">
                <asp:Button runat="server" ID="btnCarrega" Text="Escolher Cliente" OnClientClick="CloseAndReload();" CssClass="button button-basic-blue" />
            </div>
    </form>
</body>
</html>

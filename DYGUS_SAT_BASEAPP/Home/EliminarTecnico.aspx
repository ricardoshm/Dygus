<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EliminarTecnico.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EliminarTecnico" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Técnico</a> <span class="divider">/</span></li>
            <li class="active">Eliminar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Eliminar Técnico</h1>
        </div>


        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Técnicos Registados</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" OnItemCommand="listagemtecnicosregistados_ItemCommand" ID="listagemtecnicosregistados" Skin="MetroTouch" OnNeedDataSource="listagemtecnicosregistados_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem técnicos registados!" NoDetailRecordsText="Não existem técnicos registados!" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridButtonColumn Text="Eliminar" CommandName="Delete" ButtonType="ImageButton"
                                        ConfirmText="Tem a certeza que deseja eliminar este técnico?" ConfirmDialogType="RadWindow"
                                        ConfirmTitle="Eliminar">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Display="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" ShowFilterIcon="false" DataField="COD_TECNICO" ReadOnly="True" HeaderText="Código" SortExpression="COD_TECNICO" UniqueName="COD_TECNICO" FilterControlAltText="Filter COD_TECNICO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" AutoPostBackOnFilter="false" ShowFilterIcon="false" ItemStyle-ForeColor="Blue" DataField="ACTIVO" ReadOnly="True" HeaderText="Conta Activa" SortExpression="ACTIVO" UniqueName="ACTIVO" FilterControlAltText="Filter ACTIVO column"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span12 -->
        </div>
        <!-- End .row-fluid -->
    </div>
</asp:Content>

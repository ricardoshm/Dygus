<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListagemGeralClientes.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListagemGeralClientes" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  


    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Relatórios</a> <span class="divider">/</span></li>
            <li class="active">Listagem Geral de Clientes</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Listagem Geral de Clientes</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Listagem Geral de Clientes</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" OnItemDataBound="listagemgeralClientes_ItemDataBound" ID="listagemgeralClientes" Skin="MetroTouch" OnNeedDataSource="listagemgeralClientes_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem clientes registados!" NoDetailRecordsText="Não existem clientes registados!" AutoGenerateColumns="False">
                                <Columns>
                                     <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="SELECTCLIENTE" FilterControlAltText="Filter SELECTCLIENTE column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn Visible="false" DataField="UserId" ReadOnly="True" HeaderText="UserId" SortExpression="UserId" UniqueName="UserId" DataType="System.Guid" FilterControlAltText="Filter UserId column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" ItemStyle-Font-Bold="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" ItemStyle-ForeColor="Green" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="CONTACTO" ReadOnly="True" HeaderText="Telefone" SortExpression="CONTACTO" UniqueName="CONTACTO" FilterControlAltText="Filter CONTACTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NIF" ReadOnly="True" HeaderText="Nif" SortExpression="NIF de Cliente" UniqueName="NIF" DataType="System.Int32" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="TIPO_CLIENTE" ReadOnly="True" HeaderText="Tipo" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" SortExpression="TIPO_CLIENTE" UniqueName="TIPO_CLIENTE" FilterControlAltText="Filter TIPO_CLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ESTADO" ReadOnly="True" HeaderText="Conta Activa" ItemStyle-ForeColor="Blue" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="iD Cliente" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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

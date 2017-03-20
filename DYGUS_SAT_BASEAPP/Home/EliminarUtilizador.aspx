<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EliminarUtilizador.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EliminarUtilizador" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   

    
    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Lojista</a> <span class="divider">/</span></li>
            <li class="active">Eliminar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Eliminar Lojista</h1>
        </div>
        

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Lojistas Registados</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                       <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemutilizadoresregistados" OnItemCommand="listagemutilizadoresregistados_ItemCommand" Skin="MetroTouch" OnNeedDataSource="listagemutilizadoresregistados_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="Não existem utilizadores registados!" NoDetailRecordsText="Não existem utilizadores registados!" AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridButtonColumn Text="Eliminar" CommandName="Delete" ButtonType="ImageButton"
                                                ConfirmText="Tem a certeza que deseja eliminar este UTILIZADOR?" ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Eliminar">
                                            </telerik:GridButtonColumn>

                                            <telerik:GridBoundColumn ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="COD_UTILIZADOR" ReadOnly="True" HeaderText="Código" SortExpression="COD_UTILIZADOR" UniqueName="COD_UTILIZADOR" FilterControlAltText="Filter COD_UTILIZADOR column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-ForeColor="Blue" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="ESTADO" ReadOnly="True" HeaderText="Conta Activa" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Display="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="iD Lojista" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                        </Columns>

                                        <Columns>
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

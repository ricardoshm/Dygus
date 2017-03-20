<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EliminarReparador.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EliminarReparador" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   

    

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Reparador</a> <span class="divider">/</span></li>
            <li class="active">Eliminar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Eliminar Reparador</h1>
        </div>
        

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Reparadores Registados</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                       <telerik:RadGrid Culture="pt-PT" runat="server" OnItemCommand="listagemreparadoresregistados_ItemCommand" ID="listagemreparadoresregistados" Skin="MetroTouch" OnNeedDataSource="listagemreparadoresregistados_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>

                                    <MasterTableView NoMasterRecordsText="Não existem reparadores externos registados!" NoDetailRecordsText="Não existem reparadores externos registados!" AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridButtonColumn Text="Eliminar Reparador" ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" CommandName="Delete" ButtonType="ImageButton"
                                                ConfirmText="Tem a certeza que deseja eliminar este reparador externo?" ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Eliminar">
                                            </telerik:GridButtonColumn>
                                            
                                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="CONTACTO" ReadOnly="True" HeaderText="Telefone" SortExpression="CONTACTO" UniqueName="CONTACTO" FilterControlAltText="Filter CONTACTO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="NIF" ReadOnly="True" HeaderText="Nif" SortExpression="NIF" UniqueName="NIF" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-ForeColor="Blue" AllowFiltering="false" AllowSorting="false" ShowSortIcon="false" ShowFilterIcon="false" DataField="CONTA_ACTIVA" ReadOnly="True" HeaderText="Conta Activa" SortExpression="CONTA_ACTIVA" UniqueName="CONTA_ACTIVA" FilterControlAltText="Filter CONTA_ACTIVA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Display="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="iD Reparador" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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

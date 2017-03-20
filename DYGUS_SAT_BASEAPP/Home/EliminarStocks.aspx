<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EliminarStocks.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EliminarStocks" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Artigo</a> <span class="divider">/</span></li>
            <li class="active">Eliminar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Eliminar Artigo</h1>
        </div>


        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Artigos Registados</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" OnItemCommand="listagemartigos_ItemCommand" ID="listagemartigos" Skin="MetroTouch" OnNeedDataSource="listagemartigos_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem artigos registados!" NoDetailRecordsText="Não existem artigos registados!" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridButtonColumn Text="Eliminar" CommandName="Delete" ButtonType="ImageButton"
                                        ConfirmText="Tem a certeza que deseja eliminar este artigo?" ConfirmDialogType="RadWindow"
                                        ConfirmTitle="Eliminar">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Display="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="DESCRICAO" ReadOnly="True" HeaderText="Descrição" SortExpression="DESCRICAO" UniqueName="DESCRICAO" FilterControlAltText="Filter DESCRICAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="VALOR_CUSTO" ReadOnly="True" HeaderText="Valor Custo" SortExpression="VALOR_CUSTO" UniqueName="VALOR_CUSTO" DataType="System.Double" DataFormatString="{0:F2} &#8364;" FilterControlAltText="Filter VALOR_CUSTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="VALOR_REVENDA" ReadOnly="True" HeaderText="Valor Revenda" SortExpression="VALOR_REVENDA" UniqueName="VALOR_REVENDA" DataType="System.Double" DataFormatString="{0:F2} &#8364;" FilterControlAltText="Filter VALOR_REVENDA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="VALOR_VENDA" ReadOnly="True" HeaderText="Valor Venda" SortExpression="VALOR_VENDA" UniqueName="VALOR_VENDA" DataType="System.Double" DataFormatString="{0:F2} &#8364;" FilterControlAltText="Filter VALOR_VENDA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="QTD" ReadOnly="True" HeaderText="Stock" SortExpression="QTD" UniqueName="QTD" DataType="System.Int32" FilterControlAltText="Filter QTD column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="ARMAZEM" ReadOnly="True" HeaderText="Armazém" SortExpression="ARMAZEM" UniqueName="ARMAZEM" FilterControlAltText="Filter ARMAZEM column"></telerik:GridBoundColumn>
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




<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListagemGeralOrcamentoCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListagemGeralOrcamentoCliente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Orçamentos</a> <span class="divider">/</span></li>
            <li class="active">Listar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Listagem de Orçamentos</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Orçamentos</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">

                        <telerik:RadGrid Culture="pt-PT" OnItemDataBound="listagemgeralors_ItemDataBound" runat="server" ID="listagemgeralors" Skin="MetroTouch" OnNeedDataSource="listagemgeralors_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem orçamentos!" NoDetailRecordsText="Não existem orçamentos!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Imprimir" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado ORÇ" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOMECLIENTE" ReadOnly="True" HeaderText="Nome Cliente" SortExpression="NOMECLIENTE" UniqueName="NOMECLIENTE" FilterControlAltText="Filter NOMECLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="Imei" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código ORÇ" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo OR" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" Visible="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID_ESTADO_OR" ReadOnly="True" HeaderText="ID_ESTADO_OR" SortExpression="ID_ESTADO_OR" UniqueName="ID_ESTADO_OR" DataType="System.Int32" FilterControlAltText="Filter ID_ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListarTecnicoOrdemReparacao.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListarTecnicoOrdemReparacao" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Ordens de Reparação</a> <span class="divider">/</span></li>
            <li class="active">Atribuir</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Atribuir Ordens de Reparação</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Ordens de Reparação</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemordensreparacaoregistadas" Skin="MetroTouch" OnNeedDataSource="listagemordensreparacaoregistadas_NeedDataSource" OnItemDataBound="listagemordensreparacaoregistadas_ItemDataBound" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação registadas!" NoDetailRecordsText="Não existem ordens de reparação registadas!" AutoGenerateColumns="False">
                                  <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Atribuir" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="EDITAR" FilterControlAltText="Filter EDITAR column"></telerik:GridHyperLinkColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGO" ReadOnly="True" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="IMEI" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-ForeColor="Green" DataField="DATA_REGISTO_OR" ReadOnly="True" HeaderText="Data OR" SortExpression="DATA_REGISTO_OR" UniqueName="DATA_REGISTO_OR" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-ForeColor="Red" DataField="DATA_PREVISTA_ENTREGA" ReadOnly="True" HeaderText="Data Prevista Entrega" SortExpression="DATA_PREVISTA_ENTREGA" UniqueName="DATA_PREVISTA_ENTREGA" DataType="System.DateTime" FilterControlAltText="Filter DATA_PREVISTA_ENTREGA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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



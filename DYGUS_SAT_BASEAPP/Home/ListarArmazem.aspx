<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListarArmazem.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListarArmazem" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">





    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Armazém</a> <span class="divider">/</span></li>
            <li class="active">Editar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Editar Armazém</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Armazéns Registados</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" OnItemDataBound="listagemarmazens_ItemDataBound" runat="server" ID="listagemarmazens" Skin="MetroTouch" OnNeedDataSource="listagemarmazens_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem armazéns registados!" NoDetailRecordsText="Não existem armazéns registados!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                  
                                     <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Editar" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="EDITARARMAZEM" FilterControlAltText="Filter EDITARARMAZEM column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn Display="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="OBSERVACOES" ReadOnly="True" HeaderText="Observações" SortExpression="OBSERVACOES" UniqueName="OBSERVACOES" FilterControlAltText="Filter OBSERVACOES column"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="false" />
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

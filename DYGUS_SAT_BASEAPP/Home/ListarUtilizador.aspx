<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListarUtilizador.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListarUtilizador" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Lojista</a> <span class="divider">/</span></li>
            <li class="active">Editar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Editar Lojista</h1>
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
                      <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemutilizadoresregistados" OnItemDataBound="listagemutilizadoresregistados_ItemDataBound" Skin="MetroTouch" OnNeedDataSource="listagemutilizadoresregistados_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="Não existem utilizadores registados!" NoDetailRecordsText="Não existem utilizadores registados!" AutoGenerateColumns="False">
                                         <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                        <Columns>
                                            <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Editar" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="EDITARUSER" FilterControlAltText="Filter EDITARUSER column"></telerik:GridHyperLinkColumn>
                                            
                                            <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="COD_UTILIZADOR" ReadOnly="True" HeaderText="Código" SortExpression="COD_UTILIZADOR" UniqueName="COD_UTILIZADOR" FilterControlAltText="Filter COD_UTILIZADOR column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn ItemStyle-ForeColor="Blue" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="ESTADO" ReadOnly="True" HeaderText="Conta Activa" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Display="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                        </Columns>

                                        <Columns>
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

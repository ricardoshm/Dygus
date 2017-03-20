<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListarLoja.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListarLoja" MaintainScrollPositionOnPostback="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
     <script type="text/javascript">
         function validationFailed(sender, eventArgs) {
             alert("A dimensão do ficheiro não pode ultrapassar 1MB");
         }

    </script>

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Loja</a> <span class="divider">/</span></li>
            <li class="active">Editar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Editar Loja</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Lojas Registadas</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" OnItemDataBound="listagemlojasregistadas_ItemDataBound" ID="listagemlojasregistadas" Skin="MetroTouch" OnNeedDataSource="listagemlojasregistadas_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem lojas registadas!" NoDetailRecordsText="Não existem lojas registadas!" AutoGenerateColumns="False">
                                 <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    
                                      <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Editar" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="EDITAR" FilterControlAltText="Filter EDITAR column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn Display="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="LOCALIDADE" ReadOnly="True" HeaderText="Localidade" SortExpression="LOCALIDADE" UniqueName="LOCALIDADE" FilterControlAltText="Filter LOCALIDADE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="CONTACTO_TEL" ReadOnly="True" HeaderText="Contacto" SortExpression="CONTACTO_TEL" UniqueName="CONTACTO_TEL" FilterControlAltText="Filter CONTACTO_TEL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="NIF" ReadOnly="True" HeaderText="NIF" SortExpression="NIF" UniqueName="NIF" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn  ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" AutoPostBackOnFilter="false" DataField="ESTADO" ReadOnly="True" HeaderText="ACTIVA" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
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

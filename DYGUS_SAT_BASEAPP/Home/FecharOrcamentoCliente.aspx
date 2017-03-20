<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="FecharOrcamentoCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.FecharOrcamentoCliente" %>

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

                        <telerik:RadGrid Culture="pt-PT" OnItemCommand="listagemgeralors_ItemCommand" runat="server" ID="listagemgeralors" Skin="MetroTouch" OnNeedDataSource="listagemgeralors_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
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
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-success" CommandName="CONVERT" ButtonType="PushButton" ConfirmDialogType="Classic" ItemStyle-Font-Bold="true" ConfirmTitle="Converter em OR" Text="Converter em OR" FilterControlAltText="Filter CONVERT column" UniqueName="CONVERT" ConfirmText="Tem a certeza que deseja convert este orçamento em ordem de reparação?">
                                        <HeaderStyle Width="15%" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-danger" CommandName="CANCELOR" ButtonType="PushButton" ConfirmDialogType="Classic" ItemStyle-Font-Bold="true" ConfirmTitle="Cancelar ORÇ" Text="Cancelar ORÇ" FilterControlAltText="Filter CANCELOR column" UniqueName="CANCELOR" ConfirmText="Tem a certeza que deseja cancelar? Este processo é irreversível!">
                                        <HeaderStyle Width="15%" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado ORÇ" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOMECLIENTE" ReadOnly="True" HeaderText="Nome Cliente" SortExpression="NOMECLIENTE" UniqueName="NOMECLIENTE" FilterControlAltText="Filter NOMECLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código ORÇ" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
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

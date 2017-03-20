<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="GerirORAdmin.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.GerirORAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Ordens de Reparação</a> <span class="divider">/</span></li>
            <li class="active">Gerir</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Gerir Ordens de Reparação</h1>
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
                        <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemORS" Skin="MetroTouch" OnNeedDataSource="listagemORS_NeedDataSource" OnItemCommand="listagemORS_ItemCommand" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
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
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-danger" CommandName="EDITAR" ButtonType="PushButton" Text="Gerir" FilterControlAltText="Filter EDITAR column" UniqueName="EDITAR">
                                        <HeaderStyle Width="10%" />
                                    </telerik:GridButtonColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOMECLIENTE" ReadOnly="True" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" HeaderText="Nome do Cliente" SortExpression="NOMECLIENTE" UniqueName="NOMECLIENTE" FilterControlAltText="Filter NOMECLIENTE column"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="IMEI" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ItemStyle-ForeColor="Red" ItemStyle-BackColor="LightBlue" ItemStyle-Font-Bold="true" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" Display="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-ForeColor="Green" DataField="DATA_REGISTO_OR" ReadOnly="True" HeaderText="Data Registo OR" SortExpression="DATA_REGISTO_OR" UniqueName="DATA_REGISTO_OR" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO_OR column"></telerik:GridBoundColumn>
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

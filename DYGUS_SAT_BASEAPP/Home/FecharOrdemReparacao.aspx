<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="FecharOrdemReparacao.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.FecharOrdemReparacao" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Ordens de Reparação</a> <span class="divider">/</span></li>
            <li class="active">Fechar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Fechar Ordens de Reparação</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Ordens de Reparação s/Equipamento de Substituição</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid OnItemDataBound="listagemgeralors_ItemDataBound" Culture="pt-PT" runat="server" ID="listagemgeralors" Skin="MetroTouch" OnItemCommand="listagemgeralors_ItemCommand" OnNeedDataSource="listagemgeralors_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação!" NoDetailRecordsText="Não existem ordens de reparação!" AutoGenerateColumns="False">

                                <Columns>
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-success " CommandName="FECHAR" ButtonType="PushButton" ConfirmDialogType="Classic" ConfirmTitle="Fecho OR" ItemStyle-Font-Bold="true" Text="Fechar OR" FilterControlAltText="Filter CLOSE column" UniqueName="CLOSE">
                                        <HeaderStyle Width="10%" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-danger " CommandName="NOTIFICARCLIENTE" ButtonType="PushButton" ConfirmDialogType="Classic" ItemStyle-Font-Bold="true" ConfirmTitle="Notificar" Text="Notificar" FilterControlAltText="Filter NOTIFICA column" UniqueName="NOTIFICA">
                                        <HeaderStyle Width="10%" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CLIENTENOTIFICADO" ReadOnly="True" HeaderText="Cliente Noficado" DataType="System.Boolean" SortExpression="CLIENTENOTIFICADO" UniqueName="CLIENTENOTIFICADO" FilterControlAltText="Filter CLIENTENOTIFICADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo OR" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOMECLIENTE" ReadOnly="True" HeaderText="Nome Cliente" SortExpression="NOMECLIENTE" UniqueName="NOMECLIENTE" FilterControlAltText="Filter NOMECLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_FINAL" DataFormatString="{0:F2} &#8364;" ReadOnly="True" HeaderText="Valor OR" SortExpression="VALOR_FINAL" UniqueName="VALOR_FINAL" DataType="System.Double" FilterControlAltText="Filter VALOR_FINAL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_CONCLUSAO" ReadOnly="True" HeaderText="Data Conclusão" SortExpression="DATA_CONCLUSAO" UniqueName="DATA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_CONCLUSAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
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

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Ordens de Reparação c/Equipamento de Substituição</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid OnItemDataBound="listagemorsequipsubst_ItemDataBound" Culture="pt-PT" runat="server" ID="listagemorsequipsubst" Skin="MetroTouch" OnItemCommand="listagemorsequipsubst_ItemCommand" OnNeedDataSource="listagemorsequipsubst_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação!" NoDetailRecordsText="Não existem ordens de reparação!" AutoGenerateColumns="False">

                                <Columns>
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-success " CommandName="FECHAR" ButtonType="PushButton" ConfirmDialogType="RadWindow" ItemStyle-Font-Bold="true" ConfirmText="O cliente devolveu o equipamento de substituição?" ConfirmTitle="Fecho OR" Text="Fechar OR" FilterControlAltText="Filter CLOSE column" UniqueName="CLOSE">
                                        <HeaderStyle Width="10%" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-danger " CommandName="NOTIFICARCLIENTE" ButtonType="PushButton" ConfirmDialogType="Classic" ItemStyle-Font-Bold="true" ConfirmTitle="Notificar" Text="Notificar" FilterControlAltText="Filter NOTIFICA column" UniqueName="NOTIFICA">
                                        <HeaderStyle Width="10%" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CLIENTENOTIFICADO" ReadOnly="True" HeaderText="Cliente Noficado" DataType="System.Boolean" SortExpression="CLIENTENOTIFICADO" UniqueName="CLIENTENOTIFICADO" FilterControlAltText="Filter CLIENTENOTIFICADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo OR" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOMECLIENTE" ReadOnly="True" HeaderText="Nome Cliente" SortExpression="NOMECLIENTE" UniqueName="NOMECLIENTE" FilterControlAltText="Filter NOMECLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_FINAL" DataFormatString="{0:F2} &#8364;" ReadOnly="True" HeaderText="Valor OR" SortExpression="VALOR_FINAL" UniqueName="VALOR_FINAL" DataType="System.Double" FilterControlAltText="Filter VALOR_FINAL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_CONCLUSAO" ReadOnly="True" HeaderText="Data Conclusão" SortExpression="DATA_CONCLUSAO" UniqueName="DATA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_CONCLUSAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="COD_EQUIP_SUBST" ReadOnly="True" HeaderText="Código Equip. Subst." SortExpression="COD_EQUIP_SUBST" UniqueName="COD_EQUIP_SUBST" FilterControlAltText="Filter COD_EQUIP_SUBST column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
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
    </div>
</asp:Content>

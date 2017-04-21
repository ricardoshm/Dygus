<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.Default" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="loja" runat="server"></div>
    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li class="active">Painel de Controlo</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-dashboard"></i>Painel de Controlo</h1>
        </div>

        <!-- End .row-fluid -->

        <div class="row-fluid" runat="server" id="atalhos">
            <div class="span12">
                <div class="widget plain">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Atalhos</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content center">
                        <div class="stats-buttons">
                            <ul class="unstyled">
                                <li><a href="MotorPesquisa.aspx" class="clearfix"><span class="icon green"><i class="icon24 i-file-8"></i></span><span class="number">PESQUISA</span> <span class="txt">NOVA</span> </a></li>
                                <li><a href="InserirOrdemReparacao.aspx" class="clearfix"><span class="icon blue"><i class="icon24 i-point-up"></i></span><span class="number">OR</span> <span class="txt">NOVA</span> </a></li>
                                <li><a href="InserirOrcamentoCliente.aspx" class="clearfix"><span class="icon blue"><i class="icon24 i-point-up"></i></span><span class="number">ORÇM</span> <span class="txt">NOVO</span> </a></li>
                                <li><a href="InserirLoja.aspx" class="clearfix"><span class="icon yellow"><i class="icon24 i-eye-2"></i></span><span class="number">LOJA</span> <span class="txt">NOVA</span> </a></li>
                                <li><a href="InserirCliente.aspx" class="clearfix"><span class="icon gray"><i class="icon24 i-envelop-2"></i></span><span class="number">CLIENTE</span> <span class="txt">NOVO</span> </a></li>
                                <li><a href="InserirTecnico.aspx" class="clearfix"><span class="icon"><i class="icon24 i-coin"></i></span><span class="number">TÉCNICO</span> <span class="txt">NOVO</span> </a></li>
                                <li><a href="InserirReparador.aspx" class="clearfix"><span class="icon"><i class="icon24 i-coin"></i></span><span class="number">REP. EXTERNO</span></a></li>
                                <li><a href="ContaCorrente.aspx" class="clearfix"><span class="icon green"><i class="icon24 i-user-plus"></i></span><span class="number">CONTA</span> <span class="txt">CORRENTE</span> </a></li>
                            </ul>
                        </div>
                        <!-- End .stats-buttons -->
                        <div class="clearfix"></div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>
        <div class="row-fluid" id="divalertas" runat="server">
            <div class="span6">
                <div class="widget plain" id="alertaPendente" runat="server">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Tarefas Pendentes</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="toDo">
                            <h4 class="period">Orçamentos Pendentes</h4>
                            <ul class="todo-list">
                                <asp:Literal runat="server" ID="orcamentoPendente"></asp:Literal>
                            </ul>
                        </div>
                        <div class="toDo">
                            <h4 class="period">Tarefas Pendentes</h4>
                            <ul class="todo-list">
                                <asp:Literal runat="server" ID="tarefaPendente"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <div class="span6">
                <div class="widget plain" id="divornotifica" runat="server">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Ordens de Reparação Concluídas p/Fechar</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="toDo">
                            <h4 class="period">Notificar Clientes</h4>
                            <ul class="todo-list">
                                <asp:Literal runat="server" ID="notificarClientes"></asp:Literal>
                            </ul>
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>

        <div class="row-fluid" id="divdados" runat="server">
            <div class="span12">
                <div class="widget plain">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Dados da Aplicação</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content center">
                        <div class="vital-stats">
                            <ul>
                                <li><a href="#">
                                    <div class="item">
                                        <div class="icon"><i class="i-calendar"></i></div>
                                        <span class="percent" id="Numclientes" runat="server"></span><span class="txt">Clientes</span>
                                    </div>
                                </a></li>
                                <li><a href="#">
                                    <div class="item">
                                        <div class="icon blue"><i class="i-basket"></i></div>
                                        <span class="percent" id="numOrdens" runat="server"></span><span class="txt">OR's</span>
                                    </div>
                                </a></li>
                                <li><a href="#">
                                    <div class="item">
                                        <div class="icon red"><i class="i-clipboard-4"></i></div>
                                        <span class="percent" id="numOrdensA" runat="server"></span><span class="txt">Atribuídas</span>
                                    </div>
                                </a></li>
                                <li><a href="#">
                                    <div class="item">
                                        <div class="icon green"><i class="i-download-2"></i></div>
                                        <span class="percent" id="NumReparadores" runat="server"></span><span class="txt">R. Externos</span>
                                    </div>
                                </a></li>
                                <li><a href="#">
                                    <div class="item">
                                        <div class="icon yellow"><i class="i-search-3"></i></div>
                                        <span class="percent" id="NumTecnicos" runat="server"></span><span class="txt">Técnicos</span>
                                    </div>
                                </a></li>
                                <li><a href="#">
                                    <div class="item">
                                        <div class="icon orange"><i class="i-temperature"></i></div>
                                        <span class="percent" id="NumUsers" runat="server"></span><span class="txt">Lojistas</span>
                                    </div>
                                </a></li>
                            </ul>
                        </div>
                        <!-- End .vital-stats -->
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>


        <div class="row-fluid" id="ortecnicos" runat="server">
            <div class="span12">
                <div class="widget plain">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Ordens de Reparação (Atribuídas a Técnicos)</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content center">
                        <telerik:RadGrid Culture="pt-PT" ClientSettings-AllowColumnsReorder="true" runat="server" ID="listaOrsTecnicos" OnNeedDataSource="listaOrsTecnicos_NeedDataSource" OnItemDataBound="listaOrsTecnicos_ItemDataBound" Skin="MetroTouch" AllowPaging="True" AllowSorting="True" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação registadas!" NoDetailRecordsText="Não existem ordens de reparação registadas!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="EDITAR" FilterControlAltText="Filter EDITAR column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" Display="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ItemStyle-ForeColor="Green" ReadOnly="True" HeaderText="Data de Registo" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado" ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" SortExpression="ESTADO" UniqueName="ESTADO" DataType="System.Int32" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_PREVISTA_CONCLUSAO" ReadOnly="True" HeaderText="Data Prevista Conclusão" ItemStyle-ForeColor="Red" SortExpression="DATA_PREVISTA_CONCLUSAO" UniqueName="DATA_PREVISTA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_PREVISTA_CONCLUSAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME_TEC" ReadOnly="True" HeaderText="Técnico" SortExpression="NOME_TEC" UniqueName="NOME_TEC" FilterControlAltText="Filter NOME_TEC column"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="false" />
                        </telerik:RadGrid>
                        <!-- End .vital-stats -->
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>

        <div class="row-fluid" id="orreparadores" runat="server">
            <div class="span12">
                <div class="widget plain">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Ordens de Reparação (Atribuídas a Reparadores Externos)</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content center">
                        <telerik:RadGrid ClientSettings-AllowColumnsReorder="true" Culture="pt-PT" runat="server" ID="listaOrsReparadores" OnNeedDataSource="listaOrsReparadores_NeedDataSource" OnItemDataBound="listaOrsReparadores_ItemDataBound" Skin="MetroTouch" AllowPaging="True" AllowSorting="True" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação registadas!" NoDetailRecordsText="Não existem ordens de reparação registadas!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="EDITAR" FilterControlAltText="Filter EDITAR column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" Display="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ItemStyle-ForeColor="Green" ReadOnly="True" HeaderText="Data de Registo" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado" SortExpression="ESTADO" ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" UniqueName="ESTADO" DataType="System.Int32" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_PREVISTA_CONCLUSAO" ItemStyle-ForeColor="Red" ReadOnly="True" HeaderText="Data Prevista Conclusão" SortExpression="DATA_PREVISTA_CONCLUSAO" UniqueName="DATA_PREVISTA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_PREVISTA_CONCLUSAO column"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME_REP" ReadOnly="True" HeaderText="Reparador" SortExpression="NOME_REP" UniqueName="NOME_REP" FilterControlAltText="Filter NOME_REP column"></telerik:GridBoundColumn>
                                </Columns>

                            </MasterTableView>
                            <GroupingSettings CaseSensitive="false" />
                        </telerik:RadGrid>

                        <!-- End .vital-stats -->
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>

        <div class="row-fluid" id="orclientes" runat="server">
            <div class="span12">
                <div class="widget plain">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Ordens de Reparação</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content center">
                        <telerik:RadGrid ClientSettings-AllowColumnsReorder="true" Culture="pt-PT" runat="server" ID="listaorclientes" OnNeedDataSource="listaorclientes_NeedDataSource" Skin="MetroTouch" AllowPaging="True" AllowSorting="True" OnItemDataBound="listaorclientes_ItemDataBound" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação registadas!" NoDetailRecordsText="Não existem ordens de reparação registadas!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="EDITAR" FilterControlAltText="Filter EDITAR column"></telerik:GridHyperLinkColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ItemStyle-ForeColor="Green" ReadOnly="True" HeaderText="Data de Registo" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME_CLIENTE" ReadOnly="True" HeaderText="Cliente" SortExpression="NOME_CLIENTE" UniqueName="NOME_CLIENTE" FilterControlAltText="Filter NOME_CLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="Imei" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado" SortExpression="ESTADO" ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" UniqueName="ESTADO" DataType="System.Int32" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_PREVISTA_CONCLUSAO" ItemStyle-ForeColor="Red" ReadOnly="True" HeaderText="Data Prevista Conclusão" SortExpression="DATA_PREVISTA_CONCLUSAO" UniqueName="DATA_PREVISTA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_PREVISTA_CONCLUSAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="false" />
                        </telerik:RadGrid>

                        <!-- End .vital-stats -->
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>

        <div class="row-fluid" id="orcclientes" runat="server">
            <div class="span12">
                <div class="widget plain">
                    <div class="widget-title">
                        <i class="icon20 i-mail-send"></i>
                        <h4>Orçamentos</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content center">
                        <telerik:RadGrid ClientSettings-AllowColumnsReorder="true" Culture="pt-PT" runat="server" ID="listaOrcamentos" OnNeedDataSource="listaOrcamentos_NeedDataSource" Skin="MetroTouch" AllowPaging="True" AllowSorting="True" OnItemDataBound="listaOrcamentos_ItemDataBound" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem orçamentos!" NoDetailRecordsText="Não existem orçamentos!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Gerir" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código ORÇ" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="USERID" ReadOnly="True" HeaderText="USERID" SortExpression="USERID" UniqueName="USERID" FilterControlAltText="Filter USERID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado ORÇ" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_OR" ReadOnly="True" HeaderText="Valor" DataFormatString="{0:F2} &#8364;" SortExpression="VALOR_OR" UniqueName="VALOR_OR" FilterControlAltText="Filter VALOR_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn AllowFiltering="false" Visible="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID_ESTADO_OR" ReadOnly="True" HeaderText="ID_ESTADO_OR" SortExpression="ID_ESTADO_OR" UniqueName="ID_ESTADO_OR" DataType="System.Int32" FilterControlAltText="Filter ID_ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                </Columns>

                            </MasterTableView>
                            <GroupingSettings CaseSensitive="false" />
                        </telerik:RadGrid>

                        <!-- End .vital-stats -->
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>

    </div>
    <!-- End .container-fluid -->
    <div class="row">
        <!-- Start .row -->
        <!-- Start .page-header -->
        <div class="col-lg-12 heading">
            <h1 class="page-header"><i class="im-screen"></i>Painel Controlo</h1>
            <!-- Start .bredcrumb -->
            <ul id="crumb" class="breadcrumb"></ul>
            <!-- End .breadcrumb -->
            <!-- Start .option-buttons -->

            <!-- End .option-buttons -->
        </div>
        <!-- End .page-header -->
    </div>
    <!-- End .row -->
    <div class="outlet">
        <!-- Start .outlet -->
        <!-- Page start here ( usual with .row ) -->
        <div class="row">
            <!-- Start .row -->
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="carousel-tile carousel vertical slide">
                    <div class="carousel-inner">
                        <div class="item active">
                            <div class="tile red">
                                <div class="tile-icon"><i class="br-cart s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">107</div>
                                    <h3>Orders</h3>
                                </div>
                            </div>
                        </div>
                        <div class="item">
                            <div class="tile orange">
                                <!-- tile start here -->
                                <div class="tile-icon"><i class="ec-cog s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">5</div>
                                    <h3>Settings changed</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Carousel -->
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="carousel-tile carousel slide">
                    <div class="carousel-inner">
                        <div class="item active">
                            <div class="tile blue">
                                <div class="tile-icon"><i class="st-chat s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">24</div>
                                    <h3>New Comments</h3>
                                </div>
                            </div>
                        </div>
                        <div class="item">
                            <div class="tile brown">
                                <!-- tile start here -->
                                <div class="tile-icon"><i class="ec-mail s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">17</div>
                                    <h3>New emails</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Carousel -->
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="carousel-tile carousel vertical slide">
                    <div class="carousel-inner">
                        <div class="item active">
                            <div class="tile green">
                                <div class="tile-icon"><i class="ec-users s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">325</div>
                                    <h3>New users</h3>
                                </div>
                            </div>
                        </div>
                        <div class="item">
                            <div class="tile purple">
                                <!-- tile start here -->
                                <div class="tile-icon"><i class="ec-search s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">2540</div>
                                    <h3>Searches</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Carousel -->
            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <div class="carousel-tile carousel slide">
                    <div class="carousel-inner">
                        <div class="item active">
                            <div class="tile teal">
                                <!-- tile start here -->
                                <div class="tile-icon"><i class="ec-images s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">45</div>
                                    <h3>New images</h3>
                                </div>
                            </div>
                        </div>
                        <div class="item">
                            <div class="tile magenta">
                                <!-- tile start here -->
                                <div class="tile-icon"><i class="ec-share s64"></i></div>
                                <div class="tile-content">
                                    <div class="number">3548</div>
                                    <h3>Posts shared</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Carousel -->
            </div>
        </div>
        <!-- End .row -->
        <div class="row">
            <!-- Start .row -->
            
            <!-- End col-lg-6 -->
            <div class="col-lg-6 col-md-6">
                <!-- Start col-lg-6 -->


                <div class="panel panel-default toggle panelClose panelRefresh">
                    <!-- Start .panel -->
                    <div class="panel-heading">
                        <h4 class="panel-title"><i class="fa-list"></i>Tarefas Pendentes</h4>
                    </div>
                    <div class="panel-body">
                        <div class="todo-widget">
                            <div class="todo-header">
                                <div class="todo-search">
                                    <div>
                                        <input class="form-control" name="search" placeholder="Search for todo ...">
                                    </div>
                                </div>
                                <div class="todo-add"><a href="#" class="btn btn-primary tip" title="Add new todo"><i class="im-plus"></i></a></div>
                            </div>
                            <h4 class="todo-period">Today</h4>
                            <ul class="todo-list" id="today">
                                <li class="todo-task-item">
                                    <label class="checkbox">
                                        <input type="checkbox">
                                    </label>
                                    <div class="todo-priority normal tip" title="Normal priority"><i class="im-radio-checked"></i></div>
                                    <span class="todo-category label label-primary">javascript</span>
                                    <div class="todo-task-text">Add scroll function to template</div>
                                    <button type="button" class="close todo-close">&times;</button>
                                </li>
                                <li class="todo-task-item">
                                    <label class="checkbox">
                                        <input type="checkbox">
                                    </label>
                                    <div class="todo-priority high tip" title="High priority"><i class="im-radio-checked"></i></div>
                                    <span class="todo-category label label-brown">less</span>
                                    <div class="todo-task-text">Fix main less file</div>
                                    <button type="button" class="close todo-close">&times;</button>
                                </li>
                                <li class="todo-task-item task-done">
                                    <label class="checkbox">
                                        <input type="checkbox" checked>
                                    </label>
                                    <div class="todo-priority high tip" title="High priority"><i class="im-radio-checked"></i></div>
                                    <span class="todo-category label label-info">html</span>
                                    <div class="todo-task-text">Change navigation structure</div>
                                    <button type="button" class="close todo-close">&times;</button>
                                </li>
                            </ul>
                            <h4 class="todo-period">Tomorrow</h4>
                            <ul class="todo-list" id="tomorrow">
                                <li class="todo-task-item">
                                    <label class="checkbox">
                                        <input type="checkbox">
                                    </label>
                                    <div class="todo-priority tip" title="Low priority"><i class="im-radio-checked"></i></div>
                                    <span class="todo-category label label-dark">css</span>
                                    <div class="todo-task-text">Create slide panel widget</div>
                                    <button type="button" class="close todo-close">&times;</button>
                                </li>
                                <li class="todo-task-item">
                                    <label class="checkbox">
                                        <input type="checkbox">
                                    </label>
                                    <div class="todo-priority medium tip" title="Medium priority"><i class="im-radio-checked"></i></div>
                                    <span class="todo-category label label-danger">php</span>
                                    <div class="todo-task-text">Edit the main controller</div>
                                    <button type="button" class="close todo-close">&times;</button>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <!-- End .panel -->
                <div class="panel panel-default plain">
                    <!-- Start .panel -->
                    <div class="panel-heading white-bg"></div>
                    <div class="panel-body p0">
                        <div id="calendar"></div>
                    </div>
                </div>
                <!-- End .panel -->
            </div>
            <!-- End col-lg-6 -->
        </div>
        <!-- End .row -->
        <!-- Page End here -->
    </div>
    <!-- End .outlet -->

</asp:Content>



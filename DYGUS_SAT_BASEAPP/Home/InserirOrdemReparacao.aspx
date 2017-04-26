<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirOrdemReparacao.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirOrdemReparacao" MaintainScrollPositionOnPostback="true" EnableEventValidation="false" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function openPesquisaCliente() {
                window.radopen(null, "ListagemClientes");
            }
        </script>



        <script type="text/javascript">
            function openInsereCliente() {
                window.radopen(null, "InsereClientes");
            }
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="ListagemClientes" runat="server" Width="1000px" Height="500px"
                Modal="true" Title="Pesquisar Cliente" Skin="MetroTouch" NavigateUrl="ListagemCodigosClientes.aspx">
            </telerik:RadWindow>
            <telerik:RadWindow ID="InsereClientes" runat="server" Width="1000px" Height="650px"
                Modal="true" Title="Inserir Cliente" Skin="MetroTouch" NavigateUrl="InsereClientes.aspx">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlmarca">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlmodelo" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="listagemEquipamentosSubstituicao">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="listagemEquipamentosSubstituicao"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Ordens de Reparação</a> <span class="divider">/</span></li>
            <li class="active">Inserir</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Inserir Ordem de Reparação</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <asp:Button runat="server" ID="btnRefresh" Text="Clique aqui para refazer a Ordem de Reparação (Refresh)" CssClass="btn btn-primary btn-large span12" OnClick="btnRefresh_Click" />
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>


        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Pesquisar Cliente/Histórico OR's</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="historicoCliente" runat="server">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Pesquisar Contacto Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="285" Skin="MetroTouch" runat="server" ID="tbPesquisaContactoCliente" placeholder="Telefone" MaxLength="9" CssClass="form-control" AutoPostBack="true" OnTextChanged="tbPesquisaContactoCliente_TextChanged"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Pesquisar Nome Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbPesquisaNomeCliente" placeholder="Nome" CssClass="form-control" AutoPostBack="true" OnTextChanged="tbPesquisaNomeCliente_TextChanged"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Dados do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadGrid OnSelectedIndexChanged="listagemgeralClientes_SelectedIndexChanged" ClientSettings-Scrolling-AllowScroll="true" Culture="pt-PT" runat="server" ID="listagemgeralClientes" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                        <GroupingSettings CaseSensitive="false" />
                                        <MasterTableView NoMasterRecordsText="Não existem clientes registados!" NoDetailRecordsText="Não existem clientes registados!" AutoGenerateColumns="False">
                                            <Columns>
                                                <telerik:GridButtonColumn Text="Escolher" CommandName="Select" ButtonCssClass="btn btn-success btn-full tip">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridButtonColumn Text="Desmarcar" CommandName="Deselect" ButtonCssClass="btn btn-danger btn-full tip">
                                                </telerik:GridButtonColumn>
                                                <telerik:GridBoundColumn Display="false" DataField="UserId" ReadOnly="True" HeaderText="UserId" SortExpression="UserId" UniqueName="UserId" DataType="System.Guid" FilterControlAltText="Filter UserId column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" ItemStyle-Font-Bold="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" ItemStyle-ForeColor="Green" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="CONTACTO" ReadOnly="True" HeaderText="Telefone" SortExpression="CONTACTO" UniqueName="CONTACTO" FilterControlAltText="Filter CONTACTO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NIF" ReadOnly="True" HeaderText="Nif" SortExpression="NIF de Cliente" UniqueName="NIF" DataType="System.Int32" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="TIPO_CLIENTE" ReadOnly="True" HeaderText="Tipo" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" SortExpression="TIPO_CLIENTE" UniqueName="TIPO_CLIENTE" FilterControlAltText="Filter TIPO_CLIENTE column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ESTADO" ReadOnly="True" HeaderText="Conta Activa" ItemStyle-ForeColor="Blue" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Display="false" AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="iD Cliente" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Display="false" AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="USERID" ReadOnly="True" HeaderText="USERID" SortExpression="USERID" UniqueName="USERID" DataType="System.Guid" FilterControlAltText="Filter USERID column"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Histórico OR's</label>
                                <div class="controls controls-row">
                                    <telerik:RadGrid ClientSettings-Scrolling-AllowScroll="true" Culture="pt-PT" OnItemDataBound="listagemORsLoja_ItemDataBound" runat="server" ID="listagemORsLoja" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True">
                                        <GroupingSettings CaseSensitive="false" />
                                        <ExportSettings>
                                            <Pdf PageWidth=""></Pdf>
                                        </ExportSettings>

                                        <ClientSettings>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                        </ClientSettings>

                                        <MasterTableView NoMasterRecordsText="Não existem ordens de reparação!" NoDetailRecordsText="Não existem ordens de reparação!" AutoGenerateColumns="False">

                                            <Columns>

                                                <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column"></telerik:GridHyperLinkColumn>

                                                <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo OR" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA_EQUIP_AVARIADO" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA_EQUIP_AVARIADO" UniqueName="MARCA_EQUIP_AVARIADO" FilterControlAltText="Filter MARCA_EQUIP_AVARIADO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO_EQUIP_AVARIADO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO_EQUIP_AVARIADO" UniqueName="MODELO_EQUIP_AVARIADO" FilterControlAltText="Filter MODELO_EQUIP_AVARIADO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_ULTIMA_MODIFICACAO" ReadOnly="True" HeaderText="Data Última Intervenção" SortExpression="DATA_ULTIMA_MODIFICACAO" UniqueName="DATA_ULTIMA_MODIFICACAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_ULTIMA_MODIFICACAO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_FINAL" DataFormatString="{0:F2} &#8364;" ReadOnly="True" HeaderText="Valor OR" SortExpression="VALOR_FINAL" UniqueName="VALOR_FINAL" DataType="System.Double" FilterControlAltText="Filter VALOR_FINAL column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_CONCLUSAO" ReadOnly="True" HeaderText="Data Conclusão" SortExpression="DATA_CONCLUSAO" UniqueName="DATA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_CONCLUSAO column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                            </Columns>

                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </div>
                    </div>


                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>



        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Clientes Frequentes</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="clienteFrequente" runat="server">
                        <div class="form-horizontal">
                            <telerik:RadGrid OnSelectedIndexChanged="listaClienteFrequente_SelectedIndexChanged" OnNeedDataSource="listaClienteFrequente_NeedDataSource" ClientSettings-Scrolling-AllowScroll="false" Culture="pt-PT" runat="server" ID="listaClienteFrequente" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView NoMasterRecordsText="Não existem clientes registados!" NoDetailRecordsText="Não existem clientes registados!" AutoGenerateColumns="False">
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Escolher" CommandName="Select" ButtonCssClass="btn btn-success btn-full tip">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn Text="Desmarcar" CommandName="Deselect" ButtonCssClass="btn btn-danger btn-full tip">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn Display="false" DataField="UserId" ReadOnly="True" HeaderText="UserId" SortExpression="UserId" UniqueName="UserId" DataType="System.Guid" FilterControlAltText="Filter UserId column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" ItemStyle-Font-Bold="true" Display="false" DataField="CODIGO" ReadOnly="True" HeaderText="Código" ItemStyle-ForeColor="Green" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="CONTACTO" ReadOnly="True" HeaderText="Telefone" SortExpression="CONTACTO" UniqueName="CONTACTO" FilterControlAltText="Filter CONTACTO column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NIF" ReadOnly="True" Display="false" HeaderText="Nif" SortExpression="NIF de Cliente" UniqueName="NIF" DataType="System.Int32" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="TIPO_CLIENTE" ReadOnly="True" Display="false" HeaderText="Tipo" ItemStyle-ForeColor="Red" ItemStyle-Font-Bold="true" SortExpression="TIPO_CLIENTE" UniqueName="TIPO_CLIENTE" FilterControlAltText="Filter TIPO_CLIENTE column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ESTADO" ReadOnly="True" Display="false" HeaderText="Conta Activa" ItemStyle-ForeColor="Blue" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>



                        </div>

                    </div>
                </div>
            </div>

        </div>
        <div class="row-fluid">
            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Novo Cliente</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="dadosNovoCliente" runat="server">
                        <div class="form-horizontal">
                            <div class="control-group">
                                <asp:HiddenField runat="server" ID="codClienteExistente" />
                                <label class="control-label" for="normal">Código do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox DisabledStyle-ForeColor="Green" Width="285" DisabledStyle-Font-Bold="true" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbcodcliente" CssClass="form-control" ReadOnly="true" placeholder="Código de Cliente"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group" id="tipocliente" runat="server">
                                <label class="control-label" for="normal">Tipo do Cliente</label>
                                <div class="controls controls-row">
                                    <%--<telerik:RadComboBox EnableLoadOnDemand="true" ID="ddltipoCliente" Width="285px" runat="server" CssClass="form-control" Skin="MetroTouch" AutoPostBack="false" />--%>
                                    <asp:RadioButtonList Width="300px" runat="server" ID="rbTipoCliente" RepeatDirection="Horizontal" Style="display: inline" CssClass="form-control"></asp:RadioButtonList>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Nome do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Nome Completo" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group" style="display: none">
                                <label class="control-label" for="normal">Morada do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbmorada" TextMode="MultiLine" placeholder="Morada" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group" style="display: none">
                                <label class="control-label" for="normal">Código Postal do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="####-###" Width="285" Skin="MetroTouch" runat="server" ID="tbcodpostal" placeholder="Código Postal" MaxLength="8" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group" style="display: none">
                                <label class="control-label" for="normal">Localidade do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tblocalidade" placeholder="Localidade" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Telefone do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="285" Skin="MetroTouch" runat="server" ID="tbcontacto" placeholder="Telefone" MaxLength="9" CssClass="form-control" OnTextChanged="tbcontacto_TextChanged"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Email do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbemail" placeholder="Email" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">NIF do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="#########" Width="285" Skin="MetroTouch" runat="server" ID="tbnif" placeholder="NIF" MaxLength="9" CssClass="form-control"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobs" TextMode="MultiLine" placeholder="Observações" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados do Equipamento Avariado</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Marca</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox runat="server" ID="tbmarca" Skin="MetroTouch" Width="285px"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Modelo</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox runat="server" ID="tbmodelo" Skin="MetroTouch" Width="285px"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">IMEI</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbimei" placeholder="IMEI" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Código de Segurança</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Skin="MetroTouch" Width="285" runat="server" ID="tbcodseguranca" placeholder="Código de Segurança" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Descrição Detalhada da Avaria</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox TextMode="MultiLine" Width="285" Skin="MetroTouch" runat="server" Height="85" ID="tbdescricaoProblemaEquipAvariado" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Data Prevista de Conclusão da Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadDatePicker Culture="pt-PT" ID="dataprevistareparacao" Skin="MetroTouch" runat="server" Width="285">
                                        <Calendar ID="Calendar2" runat="server">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="LightBlue"></telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Tipo de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox ID="ddltipoReparacao" Width="285px" runat="server" CssClass="form-control" Skin="MetroTouch"></telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Valor Previsto de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox Culture="pt-PT" Type="Currency" NumberFormat-DecimalDigits="0" ID="tbvalorprevistorep" Width="285px" runat="server" CssClass="form-control" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbObsNovoEquipAvariado" TextMode="MultiLine" placeholder="Observações" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>


        <div class="row-fluid">

            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Acessórios</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Bateria</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbbateriaequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>

                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Carregador</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbcarregadorequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>

                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Cartão SIM</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbcartaosimequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>


                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Bolsa</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbbolsaequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>

                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Cartão Memória</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbcartaomemoriaequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>


                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Caixa</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbcaixa" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>

                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Outros</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tboutrosequipavariado" TextMode="MultiLine" placeholder="Outros" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsequipavariado" TextMode="MultiLine" placeholder="Observações" CssClass="form-control"></telerik:RadTextBox>

                                </div>
                            </div>


                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>

            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Estado de Bloqueio Equipamento Avariado</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Operadora</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tboperadora" placeholder="Nome da Operadora" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsEstadoEquipamento" TextMode="MultiLine" placeholder="Observações" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>


                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>





        </div>
        <div class="row-fluid">

            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Trabalhos adicionais</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Reset Software</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbresetsoftware" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Actualização Software</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbactualizacaosoft" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Reparação Hardware</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbhardware" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Limpeza Geral</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbLimpeza" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Backup</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbbackupinfo" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbObsTrabalhosRealizar" TextMode="MultiLine" placeholder="Observações" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>




                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>

            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Garantia</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Tipo de Garantia</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox ID="ddltipogarantia" Width="285px" runat="server" CssClass="form-control" Skin="MetroTouch" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsGarantiaequipavariado" TextMode="MultiLine" placeholder="Observações" CssClass="form-control"></telerik:RadTextBox>
                                </div>
                            </div>

                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>

            <!-- End .span6 -->
        </div>
        <!-- End .row-fluid -->

        <div class="row-fluid">



            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Equipamento de Substituição</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="equipSubst" runat="server">
                        <asp:HiddenField ID="idEquipSubst" runat="server" />
                        <telerik:RadAjaxPanel runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                            <telerik:RadGrid OnItemDataBound="listagemEquipamentosSubstituicao_ItemDataBound" OnSelectedIndexChanged="listagemEquipamentosSubstituicao_SelectedIndexChanged" ClientSettings-Scrolling-AllowScroll="true" Culture="pt-PT" AllowMultiRowEdit="false" ID="listagemEquipamentosSubstituicao" OnNeedDataSource="listagemEquipamentosSubstituicao_NeedDataSource"
                                ShowStatusBar="true" runat="server" AllowPaging="True"
                                AllowSorting="True" AllowMultiRowSelection="false" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None">
                                <GroupingSettings CaseSensitive="false" />
                                <MasterTableView PageSize="50" NoMasterRecordsText="Não existem clientes registados!" NoDetailRecordsText="Não existem clientes registados!" AutoGenerateColumns="False">
                                    <Columns>
                                        <telerik:GridButtonColumn Text="Escolher" CommandName="Select" ButtonCssClass="btn btn-success btn-full tip">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn Text="Desmarcar" CommandName="Deselect" ButtonCssClass="btn btn-danger btn-full tip">
                                        </telerik:GridButtonColumn>
                                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" ItemStyle-BackColor="LightBlue"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="Imei" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR" DataFormatString="{0:F2} &#8364;" ReadOnly="True" HeaderText="Valor" SortExpression="VALOR" UniqueName="VALOR" DataType="System.Double" FilterControlAltText="Filter VALOR column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="BATERIA" ReadOnly="True" HeaderText="Bateria?" SortExpression="BATERIA" UniqueName="BATERIA" FilterControlAltText="Filter BATERIA column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="CARTAO_MEMORIA" ReadOnly="True" HeaderText="Cartão Memória?" SortExpression="CARTAO_MEMORIA" UniqueName="CARTAO_MEMORIA" FilterControlAltText="Filter CARTAO_MEMORIA column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="CARREGADOR" ReadOnly="True" HeaderText="Carregador?" SortExpression="CARREGADOR" UniqueName="CARREGADOR" FilterControlAltText="Filter CARREGADOR column"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" DataField="ID" ReadOnly="True" HeaderText="iD Equipamento" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <PagerStyle Mode="NumericPages"></PagerStyle>
                            </telerik:RadGrid>
                        </telerik:RadAjaxPanel>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>


            <!-- End .span6 -->
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados da Ordem de Reparação</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Código da Ordem de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" ReadOnly="true" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbcodordemreparacao" CssClass="span6"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group" style="display: none;">
                                <label class="control-label" for="normal">Data de Registo</label>
                                <div class="controls controls-row">
                                    <telerik:RadDatePicker Width="285" Enabled="false" Culture="pt-PT" ID="dataregisto" Skin="MetroTouch" runat="server" CssClass="span6">
                                        <Calendar runat="server" ID="calendar1">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="LightBlue"></telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="control-group" id="divLoja" runat="server">
                                <label class="control-label" for="normal">Loja</label>
                                <div class="controls controls-row">
                                    <%--<telerik:RadComboBox EnableLoadOnDemand="true" ID="ddlLoja" Width="285px" runat="server" CssClass="form-control" Skin="MetroTouch" AutoPostBack="true" OnSelectedIndexChanged="ddlLoja_SelectedIndexChanged" />--%>
                                    <asp:HiddenField ID="idMinhaLoja" runat="server" />
                                    <asp:RadioButtonList Width="400px" runat="server" ID="rbLojas" RepeatDirection="Horizontal" Style="display: inline" CssClass="form-control"></asp:RadioButtonList>
                                </div>
                            </div>
                            <br />
                            <div class="form-actions" id="sucesso" runat="server" visible="false">
                                <label runat="server" id="sucessoMessage" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro" runat="server" visible="false">
                                <label runat="server" id="errorMessage" visible="false" style="color: red;"></label>
                            </div>
                            <br />

                            <div class="form-actions">
                                <asp:Button runat="server" ID="btnGravar" Text="Gravar" CssClass="btn btn-primary btn-large" OnClick="btnGravar_Click" />
                                <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="btn btn-inverse btn-large" OnClick="btnLimpar_Click" />
                                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger btn-large" OnClick="btnCancelar_Click" />
                            </div>
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>
    </div>


    <!-- End #right-sidebar -->
    <!-- Start #content -->
    <div id="content">
        <!-- Start .content-wrapper -->
        <div class="content-wrapper">
            <div class="row">
                <!-- Start .row -->
                <!-- Start .page-header -->
                <div class="col-lg-12 heading">
                    <h1 class="page-header"><i class="im-screen"></i>Inserir OR</h1>
                    <!-- Start .bredcrumb -->
                    <ul id="crumb" class="breadcrumb">
                    </ul>
                </div>
                <!-- End .page-header -->
            </div>
            <!-- End .row -->
            <div class="outlet">
                <!-- Start .outlet -->
                <!-- Page start here ( usual with .row ) -->

                <div class="row">
                    <div class="col-lg-12">
                        <!-- col-lg-6 start here -->
                        <div class="panel panel-primary toggle">
                            <!-- Start .panel -->
                            <div class="panel-heading">
                                <h4 class="panel-title"><i class="im-wand"></i>Nova ordem de reparação</h4>
                            </div>
                            <div class="panel-body">
                                <div id="wizard2" class="form-horizontal form-wizard" role="form">
                                    <div class="msg"></div>
                                    <div class="wizard-steps"></div>
                                    <div class="step" id="first2">
                                        <span data-icon="ec-user" data-text="Personal information"></span>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">First Name</label>
                                            <div class="col-lg-9">
                                                <input class="form-control" name="firstname">
                                            </div>
                                        </div>
                                        <!-- End .control-group  -->
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Last Name</label>
                                            <div class="col-lg-9">
                                                <input class="form-control" name="lastname">
                                            </div>
                                        </div>
                                        <!-- End .control-group  -->
                                    </div>
                                    <div class="step" id="personal2">
                                        <span data-icon="fa-envelope" data-text="Contact information"></span>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Email</label>
                                            <div class="col-lg-9">
                                                <input class="form-control" name="email" type="email">
                                            </div>
                                        </div>
                                        <!-- End .control-group  -->
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Phone number</label>
                                            <div class="col-lg-9">
                                                <input class="form-control" name="phone">
                                            </div>
                                        </div>
                                        <!-- End .control-group  -->
                                    </div>
                                    <div class="step submit_step" id="account2">
                                        <span data-icon="ec-unlocked" data-text="Account information"></span>
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Username</label>
                                            <div class="col-lg-9">
                                                <input class="form-control" name="username">
                                            </div>
                                        </div>
                                        <!-- End .control-group  -->
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Password</label>
                                            <div class="col-lg-9">
                                                <input class="form-control" name="password" type="password">
                                            </div>
                                        </div>
                                        <!-- End .control-group  -->
                                        <div class="form-group">
                                            <label class="col-lg-3 control-label">Re-type password</label>
                                            <div class="col-lg-9">
                                                <input class="form-control" name="password_2" type="password">
                                            </div>
                                        </div>
                                        <!-- End .control-group  -->
                                    </div>
                                    <div class="wizard-actions">
                                        <button class="btn pull-left" type="reset"><i class="en-arrow-left5"></i>Voltar</button>
                                        <button class="btn pull-right" type="submit">Avançar<i class="en-arrow-right5"></i></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End .panel -->
                    </div>
                </div>
                <!-- Page End here -->
            </div>
            <!-- End .outlet -->
        </div>
        <!-- End .content-wrapper -->
        <div class="clearfix"></div>
    </div>
    <!-- End #content -->
    <!-- Javascripts -->
    <!-- Load pace first -->
    <script src="assets/plugins/core/pace/pace.min.js"></script>
    <!-- Important javascript libs(put in all pages) -->
    <script>window.jQuery || document.write('<script src="assets/js/libs/jquery-2.1.1.min.js">\x3C/script>')</script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script>window.jQuery || document.write('<script src="assets/js/libs/jquery-ui-1.10.4.min.js">\x3C/script>')</script>
    <!--[if lt IE 9]>
  <script type="text/javascript" src="assets/js/libs/excanvas.min.js"></script>
  <script type="text/javascript" src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
  <script type="text/javascript" src="assets/js/libs/respond.min.js"></script>
<![endif]-->
    <script src="assets/js/pages/wizard.js"></script>


</asp:Content>


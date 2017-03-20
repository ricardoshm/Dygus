<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirOrcamentoClienteOR.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirOrcamentoClienteOR" %>
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
            <li><a href="#">Orçamentos</a> <span class="divider">/</span></li>
            <li class="active">Inserir</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Inserir Orçamento de Cliente</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados do Orçamento</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Código do Orçamento</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" ReadOnly="true" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbcodOrcamento" CssClass="span6"></telerik:RadTextBox>
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
                            <div class="control-group">
                                <label class="control-label" for="normal">Descrição detalhada da avaria</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Height="80" TextMode="MultiLine" ReadOnly="true" Skin="MetroTouch" runat="server" ID="tbdescricaoavaria" CssClass="span6"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Valor Previsto de Reparação (apresentado ao cliente)</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox ReadOnly="true" Culture="pt-PT" Type="Currency" NumberFormat-DecimalDigits="0" ID="tbvalorprevistorepcliente" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Valor Previsto de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox Culture="pt-PT" Type="Currency" NumberFormat-DecimalDigits="0" ID="tbvalorprevistorep" ValidationGroup="valor" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="val1" runat="server" ControlToValidate="tbvalorprevistorep" ForeColor="Red" SetFocusOnError="true" ValidationGroup="valor"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-actions" id="sucesso" runat="server" visible="false">
                                <label runat="server" id="sucessoMessage" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro" runat="server" visible="false">
                                <label runat="server" id="errorMessage" visible="false" style="color: red;"></label>
                            </div>
                            <br />

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
                        <h4>Comentários Adicionais para Cliente</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label" for="normal">Email do cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbEmailCliente" CssClass="span6"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Comentário</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="700" Height="200" Skin="MetroTouch" runat="server" ID="tbComentariosOrcamento" TextMode="MultiLine" placeholder="Comentários" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Enviar email ao cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbEnviaEmail" OnCheckedChanged="rbEnviaEmail_CheckedChanged" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Selected="true" Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
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
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">
                           <div class="form-actions">
                                <asp:Button runat="server" ID="btnGravar" Text="Gravar" CssClass="btn btn-primary btn-large" OnClick="btnGravar_Click" ValidationGroup="valor" />
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
</asp:Content>

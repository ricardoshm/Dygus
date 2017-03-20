<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirOrdemReparacaoORCCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirOrdemReparacaoORCCliente" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


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
                                    <%--<telerik:RadComboBox EnableLoadOnDemand="true" ID="ddlLoja" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch" AutoPostBack="true" OnSelectedIndexChanged="ddlLoja_SelectedIndexChanged" />--%>
                                    <asp:HiddenField ID="idMinhaLoja" runat="server" />
                                    <%--<asp:RadioButtonList Width="400px" runat="server" ID="rbLojas" RepeatDirection="Horizontal" Style="display: inline" CssClass="span12"></asp:RadioButtonList>--%>
                                    <asp:Label runat="server" ID="nomeLoja" CssClass="span12"></asp:Label>
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

        <div class="row-fluid">
            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados do Cliente</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">
                            <div class="control-group">
                                <asp:HiddenField runat="server" ID="codClienteExistente" />
                                <label class="control-label" for="normal">Código do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox DisabledStyle-ForeColor="Green" Width="285" DisabledStyle-Font-Bold="true" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbcodcliente" CssClass="span12" ReadOnly="true" placeholder="Código de Cliente"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group" id="tipocliente" runat="server">
                                <label class="control-label" for="normal">Tipo do Cliente</label>
                                <div class="controls controls-row">
                                    <%--<telerik:RadComboBox EnableLoadOnDemand="true" ID="ddltipoCliente" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch" AutoPostBack="false" />--%>
                                    <%--<asp:RadioButtonList Width="300px" runat="server" ID="rbTipoCliente" RepeatDirection="Horizontal" Style="display: inline" CssClass="span12"></asp:RadioButtonList>--%>
                                    <asp:HiddenField ID="idTipoMeuCliente" runat="server" />
                                    <asp:Label runat="server" ID="lbTipoCliente" CssClass="span12"></asp:Label>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Nome do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Nome Completo" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group" style="display: none">
                                <label class="control-label" for="normal">Morada do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbmorada" TextMode="MultiLine" placeholder="Morada" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group" style="display: none">
                                <label class="control-label" for="normal">Código Postal do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="####-###" Width="285" Skin="MetroTouch" runat="server" ID="tbcodpostal" placeholder="Código Postal" MaxLength="8" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group" style="display: none">
                                <label class="control-label" for="normal">Localidade do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tblocalidade" placeholder="Localidade" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Telefone do Cliente</label>
                                <div class="controls controls-row">

                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="285" Skin="MetroTouch" runat="server" ID="tbcontacto" placeholder="Telefone" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Email do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbemail" placeholder="Email" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">NIF do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="#########" Width="285" Skin="MetroTouch" runat="server" ID="tbnif" placeholder="NIF" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobs" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbimei" placeholder="IMEI" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Código de Segurança</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Skin="MetroTouch" Width="285" runat="server" ID="tbcodseguranca" placeholder="Código de Segurança" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Descrição Detalhada da Avaria</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox TextMode="MultiLine" Width="285" Skin="MetroTouch" runat="server" Height="85" ID="tbdescricaoProblemaEquipAvariado" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadComboBox ID="ddltipoReparacao" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch"></telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Valor Previsto de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox Culture="pt-PT" Type="Currency" NumberFormat-DecimalDigits="0" ID="tbvalorprevistorep" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbObsNovoEquipAvariado" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tboutrosequipavariado" TextMode="MultiLine" placeholder="Outros" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsequipavariado" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>

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
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tboperadora" placeholder="Nome da Operadora" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsEstadoEquipamento" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbObsTrabalhosRealizar" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadComboBox ID="ddltipogarantia" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsGarantiaequipavariado" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                    <div class="widget-content">
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
                            <br />
                            <div class="form-actions" id="sucesso2" runat="server" visible="false">
                                <label runat="server" id="sucesso2message" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro2" runat="server" visible="false">
                                <label runat="server" id="erro2message" visible="false" style="color: red;"></label>
                            </div>
                            <br />

                            <div class="form-actions">
                                <asp:Button runat="server" ID="btnGravar2" Text="Gravar" CssClass="btn btn-primary btn-large" OnClick="btnGravar_Click" />
                                <asp:Button runat="server" ID="btnCancelar2" Text="Cancelar" CssClass="btn btn-danger btn-large" OnClick="btnCancelar_Click" />
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

<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="DetalheOR.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.DetalheOR" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Ordens de Reparação</a> <span class="divider">/</span></li>
            <li class="active">Detalhe</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Detalhe Ordem de Reparação</h1>
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
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbcodordemreparacao" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Data de Registo da Ordem de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbdataRegisto" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
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

                                <asp:Button runat="server" ID="btnVoltar" Text="Voltar" CssClass="btn btn-inverse btn-large" OnClick="btnVoltar_Click" />

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
                                <label class="control-label" for="normal">Código do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox DisabledStyle-ForeColor="Green" Width="285" DisabledStyle-Font-Bold="true" Skin="MetroTouch" ReadOnly="true" runat="server" Font-Bold="true" ForeColor="Green" ID="tbcodcliente" CssClass="span12" placeholder="Pesquisar Código de Cliente"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Nome do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Nome Completo" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Morada do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" runat="server" ID="tbmorada" TextMode="MultiLine" placeholder="Morada" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Código Postal do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox ReadOnly="true" Mask="####-###" Width="285" Skin="MetroTouch" runat="server" ID="tbcodpostal" placeholder="Código Postal" MaxLength="8" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Localidade do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" runat="server" ID="tblocalidade" placeholder="Localidade" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Telefone do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox ReadOnly="true" Mask="###-###-###" Width="285" Skin="MetroTouch" runat="server" ID="tbcontacto" placeholder="Telefone" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Email do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" runat="server" ID="tbemail" placeholder="Email" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">NIF do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox ReadOnly="true" Mask="#########" Width="285" Skin="MetroTouch" runat="server" ID="tbnif" placeholder="NIF" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobs" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Cliente Desde</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" runat="server" ID="tbdataRegistocliente" placeholder="Cliente Desde" CssClass="span12"></telerik:RadTextBox>
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
                            <asp:HiddenField runat="server" ID="idEquipAvariadoHD" />
                            <div class="control-group">
                                <label class="control-label" for="normal">Marca</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox runat="server" ID="tbmarca" Skin="MetroTouch" Width="285px" ReadOnly="true"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Modelo</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox runat="server" ReadOnly="true" ID="tbmodelo" Skin="MetroTouch" Width="285px"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">IMEI</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" ReadOnly="true" Skin="MetroTouch" runat="server" ID="tbimei" placeholder="IMEI" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Novo IMEI</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbnovoimei" placeholder="IMEI" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Código de Segurança</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Skin="MetroTouch" Width="285" runat="server" ID="tbcodseguranca" placeholder="Código de Segurança" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Descrição Detalhada da Avaria</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" TextMode="MultiLine" Width="285" Skin="MetroTouch" runat="server" Height="85" ID="tbdescricaoProblemaEquipAvariado" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Data Prevista de Conclusão da Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbdataprevistareparacao" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>

                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Tipo de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbtiming" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Valor Previsto de Reparação</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox Culture="pt-PT" Type="Currency" NumberFormat-DecimalDigits="0" ID="tbvalorprevisto" ReadOnly="true" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbObsNovoEquipAvariado" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadButton Enabled="false" ID="rbbateriaequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbcarregadorequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbcartaosimequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbbolsaequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbcartaomemoriaequipavariado" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbcaixa" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tboutrosequipavariado" TextMode="MultiLine" placeholder="Outros" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsequipavariado" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>

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
                                <label class="control-label" for="normal">Bloqueado à Operadora?</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton Enabled="false" ID="rbbloqueiooperadora" AutoPostBack="true" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Operadora</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" runat="server" ID="tboperadora" placeholder="Nome da Operadora" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsEstadoEquipamento" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadButton Enabled="false" ID="rbresetsoftware" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbactualizacaosoft" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbhardware" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbLimpeza" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadButton Enabled="false" ID="rbbackupinfo" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
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
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbObsTrabalhosRealizar" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                    <telerik:RadTextBox ID="tbtipoGarantia" Width="285px" runat="server" CssClass="span12" Skin="MetroTouch" ReadOnly="true" />
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox ReadOnly="true" Width="285" Skin="MetroTouch" Height="85" runat="server" ID="tbobsGarantiaequipavariado" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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

            <div class="span6" id="artigos" runat="server">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Artigos</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">
                            <div class="form-actions" id="resultadoArtigos" runat="server" visible="false">
                                <label runat="server" id="resultadoArtigosText" visible="false" style="color: red;"></label>
                            </div>
                            <div class="control-group">
                                <asp:HiddenField ID="idEquipSubst" runat="server" />
                                <asp:UpdatePanel runat="server" ID="udpadte1">
                                    <ContentTemplate>
                                        <telerik:RadGrid Culture="pt-PT" OnNeedDataSource="listagemartigosutilizados_NeedDataSource" runat="server" Skin="MetroTouch" ID="listagemartigosutilizados" AllowPaging="True" AllowSorting="True" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            </ClientSettings>
                                            <GroupingSettings CaseSensitive="false" />
                                            <ExportSettings>
                                                <Pdf PageWidth=""></Pdf>
                                            </ExportSettings>

                                            <MasterTableView NoMasterRecordsText="Não existem artigos registados!" NoDetailRecordsText="Não existem artigos registados!" AutoGenerateColumns="False">
                                                <Columns>
                                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" HeaderText="Código Artigo" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DESCRICAO" ReadOnly="True" HeaderText="Descrição" SortExpression="DESCRICAO" UniqueName="DESCRICAO" FilterControlAltText="Filter DESCRICAO column"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="QTD_ARTIGO" ReadOnly="True" HeaderText="Quantidade" SortExpression="QTD_ARTIGO" UniqueName="QTD_ARTIGO" FilterControlAltText="Filter QTD_ARTIGO column"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" Display="false" HeaderText="iD Artigo" SortExpression="ID" UniqueName="ID" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                                </Columns>

                                            </MasterTableView>

                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                        </div>
                    </div>
                </div>





            </div>


            <div class="span6">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Registo de Mão-de-Obra</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">
                            <div class="form-actions" id="resultadoMO" runat="server" visible="false">
                                <label runat="server" id="erroMO" visible="false" style="color: red;"></label>
                            </div>

                            <div class="control-group">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                    <ContentTemplate>
                                        <telerik:RadGrid Culture="pt-PT" OnNeedDataSource="listagemmoutilizada_NeedDataSource" runat="server" Skin="MetroTouch" ID="listagemmoutilizada" AllowPaging="True" AllowSorting="True" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                            </ClientSettings>
                                            <GroupingSettings CaseSensitive="false" />
                                            <ExportSettings>
                                                <Pdf PageWidth=""></Pdf>
                                            </ExportSettings>

                                            <MasterTableView NoMasterRecordsText="Não existe M.O. registada!" NoDetailRecordsText="Não existe M.O. registada!" AutoGenerateColumns="False">
                                                <Columns>

                                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" DataField="DESC_MAO_OBRA" ReadOnly="True" HeaderText="Descrição" SortExpression="DESC_MAO_OBRA" UniqueName="DESC_MAO_OBRA" FilterControlAltText="Filter DESC_MAO_OBRA column"></telerik:GridBoundColumn>
                                                    <telerik:GridNumericColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_MAO_OBRA" DataFormatString="{0:F2} &#8364;" ReadOnly="True" FooterStyle-Font-Bold="true" FooterStyle-ForeColor="Red" FooterStyle-BackColor="LightBlue" FooterText="Valor Final:" HeaderText="Valor M.O." SortExpression="VALOR_MAO_OBRA" UniqueName="VALOR_MAO_OBRA" DataType="System.Double" FilterControlAltText="Filter VALOR_MAO_OBRA column" Aggregate="Sum"></telerik:GridNumericColumn>
                                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" DataField="OBS_MAO_OBRA" ReadOnly="True" HeaderText="Observações" SortExpression="OBS_MAO_OBRA" UniqueName="OBS_MAO_OBRA" FilterControlAltText="Filter OBS_MAO_OBRA column"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="iD MO" SortExpression="ID" UniqueName="ID" FilterControlAltText="Filter ID column" DataType="System.Int32"></telerik:GridBoundColumn>
                                                </Columns>

                                            </MasterTableView>

                                        </telerik:RadGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>


                        </div>






                    </div>
                </div>



            </div>
        </div>
        <!-- End .widget-content -->

        <div class="row-fluid">

            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Estado da Ordem de Reparação</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">
                            <div class="control-group">
                                <label class="control-label" for="normal">Estado Actual da Ordem de Reparação</label>
                                <div class="controls controls-row">

                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbEstadoActualReparacao" CssClass="span12" ReadOnly="true" ForeColor="Green"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Nome do Técnico/Reparador</label>
                                <div class="controls controls-row">

                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbnometecnicoreparador" CssClass="span12" ReadOnly="true" ForeColor="Red"></telerik:RadTextBox>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>


            </div>
        </div>




        <!-- End .widget-content -->
    </div>
</asp:Content>


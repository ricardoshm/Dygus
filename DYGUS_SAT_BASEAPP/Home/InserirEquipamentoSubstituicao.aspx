<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirEquipamentoSubstituicao.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirEquipamentoSubstituicao" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Equipamento de Substituição</a> <span class="divider">/</span></li>
            <li class="active">Inserir</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Inserir Equipamento de Substituição</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados do Equipamento de Substituição</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Código do Equipamento</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" Font-Bold="true" ForeColor="Green" ID="tbcodequipamento" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Marca do Equipamento</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox runat="server" ID="tbmarca" placeholder="Marca" Width="285" Skin="MetroTouch"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Modelo do Equipamento</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox runat="server" ID="tbmodelo" placeholder="Modelo" Width="285" Skin="MetroTouch"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">IMEI do Equipamento</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox runat="server" ID="tbimei" placeholder="IMEI" Width="285" Skin="MetroTouch"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Tem Bateria?</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbbateria" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Selected="true" Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Tem Carregador?</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbcarregador" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Selected="true" Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Tem Cartão de Memória</label>
                                <div class="controls controls-row">
                                    <telerik:RadButton ID="rbcartaomem" AutoPostBack="false" runat="server" ToggleType="CheckBox" Skin="MetroTouch" ButtonType="LinkButton">
                                        <ToggleStates>
                                            <telerik:RadButtonToggleState Selected="true" Text="Sim" PrimaryIconCssClass="rbToggleCheckboxChecked" />
                                            <telerik:RadButtonToggleState Text="Não" PrimaryIconCssClass="rbToggleCheckbox" />
                                        </ToggleStates>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Valor do Equipamento</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox Width="285" Culture="pt-PT" Type="Currency" CssClass="span12" ID="tbvalorequip" runat="server" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Estado do Equipamento</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox Skin="MetroTouch" ID="ddlEstado" Width="400px" runat="server" CssClass="span12" />
                                </div>
                            </div>


                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Height="85" Width="285" Skin="MetroTouch" runat="server" ID="tbobs" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                                <asp:Button runat="server" ID="btnGrava" Text="Gravar" CssClass="btn btn-primary btn-large" OnClick="btnGrava_Click" />
                                <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="btn btn-inverse btn-large" OnClick="btnLimpar_Click" />
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
                        <h4>Lista de Equipamentos de Substituição Registados</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemequipamentos" Skin="MetroTouch" OnNeedDataSource="listagemequipamentos_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem equipamentos de substituição registados!" NoDetailRecordsText="Não existem equipamentos de substituição registados!" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridBoundColumn Display="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código de Equipamento" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
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


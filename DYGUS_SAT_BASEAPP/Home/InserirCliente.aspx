<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirCliente" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Cliente</a> <span class="divider">/</span></li>
            <li class="active">Inserir</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Inserir Cliente</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
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
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ForeColor="Green" Font-Bold="true" ID="tbcodcliente" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Tipo do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox runat="server" ID="ddltipocliente" Width="400" Skin="MetroTouch" CssClass="span12"></telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Nome do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Nome Completo" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Morada do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbmorada" TextMode="MultiLine" placeholder="Morada" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Código Postal do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="####-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcodpostal" placeholder="Código Postal" MaxLength="8" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Localidade do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tblocalidade" placeholder="Localidade" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Contacto Telefónico do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcontacto" placeholder="Contacto Telefónico" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Email do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbemail" placeholder="Email" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">NIF do Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="#########" Width="400" Skin="MetroTouch" runat="server" ID="tbnif" placeholder="NIF" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Height="85" Width="400" Skin="MetroTouch" runat="server" ID="tbobs" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
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
                        <h4>Lista de Clientes Registados</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemclientesregistados" Skin="MetroTouch" OnNeedDataSource="listagemclientesregistados_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem clientes registados!" NoDetailRecordsText="Não existem clientes registados!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridBoundColumn Display="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="CONTACTO" ReadOnly="True" HeaderText="Telefone" SortExpression="CONTACTO" UniqueName="CONTACTO" FilterControlAltText="Filter CONTACTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="NIF" ReadOnly="True" HeaderText="Nif" SortExpression="NIF" UniqueName="NIF" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" AllowSorting="false" ShowSortIcon="false" ShowFilterIcon="false" DataField="CONTA_ACTIVA" ReadOnly="True" HeaderText="Conta Activa" ItemStyle-ForeColor="Blue" SortExpression="CONTA_ACTIVA" UniqueName="CONTA_ACTIVA" FilterControlAltText="Filter CONTA_ACTIVA column"></telerik:GridBoundColumn>
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

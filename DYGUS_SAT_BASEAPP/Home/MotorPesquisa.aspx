<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="MotorPesquisa.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.MotorPesquisa" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Diversos</a> <span class="divider">/</span></li>
            <li class="active">Motor de Pesquisa</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Motor de Pesquisa</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Termos de Pesquisa</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Pesquisar Por</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox AppendDataBoundItems="true" ForeColor="Green" Font-Bold="true" ID="ddlpesquisa" Width="285px" runat="server" CssClass="input-xlarge" Skin="MetroTouch">
                                        <DefaultItem Text="Por favor seleccione..." />
                                        <Items>
                                            <telerik:RadComboBoxItem Text="Código O.R." />
                                            <telerik:RadComboBoxItem Text="Código Cliente" />
                                            <telerik:RadComboBoxItem Text="Nome Cliente" />
                                            <telerik:RadComboBoxItem Text="Contacto Cliente" />
                                            <telerik:RadComboBoxItem Text="Imei Equipamento Avariado" />
                                            <telerik:RadComboBoxItem Text="Novo Imei Equipamento Reparado" />
                                            <telerik:RadComboBoxItem Text="Marca Equipamento Avariado" />
                                            <telerik:RadComboBoxItem Text="Modelo Equipamento Avariado" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="focused">Termo de Pesquisa</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbpesquisa" placeholder="Texto a Pesquisar" CssClass="input-xlarge"></telerik:RadTextBox>
                                </div>
                            </div>


                            <br />
                            <div class="form-actions" id="sucesso" runat="server" style="display: none;">
                                <label runat="server" id="sucessoMessage" style="display: none; color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro" runat="server" style="display: none;">
                                <label runat="server" id="errorMessage" style="display: none; color: red;"></label>
                            </div>
                            <br />

                            <div class="form-actions">
                                <asp:Button runat="server" ID="btnPesquisa" Text="Pesquisar" CssClass="btn btn-primary btn-large" OnClick="btnPesquisa_Click" />
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
                        <h4>Resultados da Pesquisa</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid OnItemDataBound="listagemORCodigo_ItemDataBound" runat="server" ID="listagemORCodigo" Skin="MetroTouch" Culture="pt-PT" AllowSorting="True" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="Não existem resultados para apresentar." NoDetailRecordsText="Não existem resultados para apresentar.">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado" ItemStyle-ForeColor="Green" ItemStyle-BackColor="LightBlue" ItemStyle-Font-Bold="true" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ID_ESTADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data de Registo" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_ULTIMA_MODIFICACAO" ReadOnly="True" HeaderText="Data da Última Modificação" SortExpression="DATA_ULTIMA_MODIFICACAO" UniqueName="DATA_ULTIMA_MODIFICACAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_ULTIMA_MODIFICACAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="false" />
                        </telerik:RadGrid>
                        <telerik:RadGrid OnItemDataBound="listagemORCliente_ItemDataBound" runat="server" ID="listagemORCliente" Skin="MetroTouch" Culture="pt-PT" AllowSorting="True" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="Não existem resultados para apresentar." NoDetailRecordsText="Não existem resultados para apresentar.">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column"></telerik:GridHyperLinkColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MORADA" ReadOnly="True" HeaderText="Morada" SortExpression="MORADA" UniqueName="MORADA" FilterControlAltText="Filter MORADA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODPOSTAL" ReadOnly="True" HeaderText="Código Postal" SortExpression="CODPOSTAL" UniqueName="CODPOSTAL" FilterControlAltText="Filter CODPOSTAL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOCALIDADE" ReadOnly="True" HeaderText="Localidade" SortExpression="LOCALIDADE" UniqueName="LOCALIDADE" FilterControlAltText="Filter LOCALIDADE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="TELEFONE" ReadOnly="True" HeaderText="Telefone" SortExpression="TELEFONE" UniqueName="TELEFONE" FilterControlAltText="Filter TELEFONE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_ULTIMA_MODIFICACAO" ReadOnly="True" HeaderText="Data Última Modificação" SortExpression="DATA_ULTIMA_MODIFICACAO" UniqueName="DATA_ULTIMA_MODIFICACAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_ULTIMA_MODIFICACAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD Cliente" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                            <GroupingSettings CaseSensitive="false" />
                        </telerik:RadGrid>

                        <telerik:RadGrid OnItemDataBound="listagemOREquipAvariado_ItemDataBound" OnItemCommand="listagemOREquipAvariado_ItemCommand" runat="server" ID="listagemOREquipAvariado" Skin="MetroTouch" Culture="pt-PT" AllowSorting="True" CellSpacing="0" GridLines="None" AllowFilteringByColumn="True">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView AutoGenerateColumns="False" NoMasterRecordsText="Não existem resultados para apresentar." NoDetailRecordsText="Não existem resultados para apresentar.">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>

                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column">
                                        <HeaderStyle Width="10%" />
                                    </telerik:GridHyperLinkColumn>
                                    <telerik:GridButtonColumn ButtonCssClass="btn btn-warning" CommandName="ALTERAR" ButtonType="PushButton" ConfirmDialogType="Classic" ItemStyle-Font-Bold="true" ConfirmTitle="Editar OR" Text="Editar OR" FilterControlAltText="Filter ALTERAR column" UniqueName="ALTERAR">
                                        <HeaderStyle Width="10%" />
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="TELEFONE" ReadOnly="True" HeaderText="Telefone" SortExpression="TELEFONE" UniqueName="TELEFONE" FilterControlAltText="Filter TELEFONE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column" Display="false"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MORADA" ReadOnly="True" HeaderText="Morada" SortExpression="MORADA" UniqueName="MORADA" FilterControlAltText="Filter MORADA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODPOSTAL" ReadOnly="True" HeaderText="Código Postal" SortExpression="CODPOSTAL" UniqueName="CODPOSTAL" FilterControlAltText="Filter CODPOSTAL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOCALIDADE" ReadOnly="True" HeaderText="Localidade" SortExpression="LOCALIDADE" UniqueName="LOCALIDADE" Display="false" FilterControlAltText="Filter LOCALIDADE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO_OR" ReadOnly="True" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" HeaderText="Código OR" Display="false" SortExpression="CODIGO_OR" UniqueName="CODIGO_OR" FilterControlAltText="Filter CODIGO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="IMEI" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOVOIMEI" ReadOnly="True" HeaderText="Novo IMEI" SortExpression="NOVOIMEI" UniqueName="NOVOIMEI" Display="false" FilterControlAltText="Filter NOVOIMEI column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo" SortExpression="DATA_REGISTO" Display="false" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_ULTIMA_MODIFICACAO" ReadOnly="True" HeaderText="Data Última Modificação" Display="false" SortExpression="DATA_ULTIMA_MODIFICACAO" UniqueName="DATA_ULTIMA_MODIFICACAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_ULTIMA_MODIFICACAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="Loja" SortExpression="LOJA" UniqueName="LOJA" Display="false" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado" ItemStyle-ForeColor="Green" ItemStyle-BackColor="LightBlue" ItemStyle-Font-Bold="true" SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ID_ESTADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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



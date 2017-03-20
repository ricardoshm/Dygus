<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirLoja.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirLoja" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validationFailed(sender, eventArgs) {
            alert("A dimensão do ficheiro não pode ultrapassar 1MB");
        }

    </script>

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Loja</a> <span class="divider">/</span></li>
            <li class="active">Inserir</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Inserir Loja</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados da Loja</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Código da Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ForeColor="Green" Font-Bold="true" ID="tbcodloja" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="focused">Nome da Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Nome da Loja" CssClass="span12"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbnome" ID="validator1" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Morada da Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbmorada" TextMode="MultiLine" placeholder="Morada" CssClass="span12"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbmorada" ID="validator2" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="tooltip">Código Postal</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="####-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcodpostal" placeholder="Código Postal" MaxLength="8" CssClass="span12"></telerik:RadMaskedTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbcodpostal" ID="validator3" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="placeholder">Localidade</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tblocalidade" placeholder="Localidade" CssClass="span12"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tblocalidade" ID="validator4" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="readonly">Telefone</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcontactotel" placeholder="Contacto telefónico" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbcontactotel" ID="validator5" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="disabled">Fax</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcontactofax" placeholder="Contacto fax" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="maxlenght">NIF</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="#########" Width="400" Skin="MetroTouch" runat="server" ID="tbnif" placeholder="NIF" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbnif" ID="validator6" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Logotipo Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadAsyncUpload runat="server" MaxFileSize="1048576" TargetFolder="~/TempFiles" TemporaryFolder="~/TempFiles" MaxFileInputsCount="1" MultipleFileSelection="Disabled" OnClientValidationFailed="validationFailed" ID="uploadLogo" Skin="MetroTouch" Culture="pt-PT" AllowedFileExtensions="jpg,jpeg,gif,png">
                                        <Localization Select="Escolher" Remove="Remover" Cancel="Cancelar"></Localization>
                                    </telerik:RadAsyncUpload>

                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="maxlenght">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Height="85" Width="400" Skin="MetroTouch" runat="server" ID="tbobsloja" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <br />
                            <div class="form-actions" id="sucesso" runat="server" visible="false">
                                <label runat="server" id="sucessoMessage" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro" runat="server">
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
                        <h4>Lista de Lojas Registadas</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemlojasregistadas" Skin="MetroTouch" OnNeedDataSource="listagemlojasregistadas_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem lojas registadas!" NoDetailRecordsText="Não existem lojas registadas!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>
                                    <telerik:GridBoundColumn Visible="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="LOCALIDADE" ReadOnly="True" HeaderText="Localidade" SortExpression="LOCALIDADE" UniqueName="LOCALIDADE" FilterControlAltText="Filter LOCALIDADE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="CONTACTO_TEL" ReadOnly="True" HeaderText="Contacto" SortExpression="CONTACTO_TEL" UniqueName="CONTACTO_TEL" FilterControlAltText="Filter CONTACTO_TEL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ShowFilterIcon="false" AllowFiltering="true" AutoPostBackOnFilter="true" DataField="NIF" ReadOnly="True" HeaderText="NIF" SortExpression="NIF" UniqueName="NIF" FilterControlAltText="Filter NIF column"></telerik:GridBoundColumn>
                                    <telerik:GridImageColumn FilterControlAltText="Filter FOTO column" HeaderText="Logotipo"
                                        ImageHeight="61px" ImageWidth="150px" UniqueName="FOTO" DataImageUrlFields="FOTO"
                                        DataImageUrlFormatString="../onoff/{0}" AllowFiltering="False" ShowFilterIcon="False">
                                        <FooterStyle Width="150px" />
                                        <HeaderStyle Width="150px" />
                                        <ItemStyle Height="61px" Width="150px" />
                                    </telerik:GridImageColumn>
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

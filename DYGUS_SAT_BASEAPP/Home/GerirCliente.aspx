<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="GerirCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.GerirCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Administração</a> <span class="divider">/</span></li>
            <li class="active">Listagem Geral de Clientes</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Listagem Geral de Clientes</h1>
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Listagem Geral de Clientes</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="listaclientes" runat="server">
                        <telerik:RadGrid Culture="pt-PT" runat="server" OnItemCommand="listagemgeralClientes_ItemCommand" ID="listagemgeralClientes" Skin="MetroTouch" OnNeedDataSource="listagemgeralClientes_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem clientes registados!" NoDetailRecordsText="Não existem clientes registados!" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridButtonColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="ACTIVAR" ShowFilterIcon="false" AutoPostBackOnFilter="false" CommandName="SELECTCLIENTE" FilterControlAltText="Filter SELECTCLIENTE column"></telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Visible="false" DataField="UserId" ReadOnly="True" HeaderText="UserId" SortExpression="UserId" UniqueName="UserId" DataType="System.Guid" FilterControlAltText="Filter UserId column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="CONTACTO" ReadOnly="True" HeaderText="Telefone" SortExpression="CONTACTO" UniqueName="CONTACTO" FilterControlAltText="Filter CONTACTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="iD Cliente" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Listagem Geral de Parceiros</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="listaparceiros" runat="server">
                        <telerik:RadGrid Culture="pt-PT" ClientSettings-Scrolling-AllowScroll="true" runat="server" OnItemCommand="listaDataValidadeClientes_ItemCommand" ID="listaDataValidadeClientes" Skin="MetroTouch" OnNeedDataSource="listaDataValidadeClientes_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem parceiros registados!" NoDetailRecordsText="Não existem parceiros registados!" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridButtonColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="ACTUALIZAR" ShowFilterIcon="false" AutoPostBackOnFilter="false" CommandName="SELECTCLIENTE" FilterControlAltText="Filter SELECTCLIENTE column"></telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Visible="false" DataField="UserId" ReadOnly="True" HeaderText="UserId" SortExpression="UserId" UniqueName="UserId" DataType="System.Guid" FilterControlAltText="Filter UserId column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="CONTACTO" ReadOnly="True" HeaderText="Telefone" SortExpression="CONTACTO" UniqueName="CONTACTO" FilterControlAltText="Filter CONTACTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="DATA" ReadOnly="True" HeaderText="Data Validade" SortExpression="DATA" UniqueName="DATA" FilterControlAltText="Filter DATA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="iD Cliente" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Listagem Geral de Funcionários</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="listafuncionarios" runat="server">
                        <telerik:RadGrid Culture="pt-PT" ClientSettings-Scrolling-AllowScroll="true" runat="server" OnItemCommand="listaDataValidadeFuncionarios_ItemCommand" ID="listaDataValidadeFuncionarios" Skin="MetroTouch" OnNeedDataSource="listaDataValidadeFuncionarios_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView NoMasterRecordsText="Não existem funcionários registados!" NoDetailRecordsText="Não existem funcionários registados!" AutoGenerateColumns="False">
                                <Columns>
                                    <telerik:GridButtonColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="ACTUALIZAR" ShowFilterIcon="false" AutoPostBackOnFilter="false" CommandName="SELECTCLIENTE" FilterControlAltText="Filter SELECTCLIENTE column"></telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn Visible="false" DataField="UserId" ReadOnly="True" HeaderText="UserId" SortExpression="UserId" UniqueName="UserId" DataType="System.Guid" FilterControlAltText="Filter UserId column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="NOME" ReadOnly="True" HeaderText="Nome" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="EMAIL" ReadOnly="True" HeaderText="Email" SortExpression="EMAIL" UniqueName="EMAIL" FilterControlAltText="Filter EMAIL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="true" DataField="DATA" ReadOnly="True" HeaderText="Data Validade" SortExpression="DATA" UniqueName="DATA" FilterControlAltText="Filter DATA column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AutoPostBackOnFilter="true" ShowSortIcon="false" ShowFilterIcon="false" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="iD Cliente" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Reset Password</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content" id="Div1" runat="server">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Pesquisar p/Email</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbPesquisaEmail" placeholder="Email" CssClass="span12" AutoPostBack="true" OnTextChanged="tbPesquisaEmail_TextChanged"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <telerik:RadGrid OnItemCommand="ListaPesquisa_ItemCommand" ClientSettings-Scrolling-AllowScroll="true" Culture="pt-PT" runat="server" ID="ListaPesquisa" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="Não existem dados registados!" NoDetailRecordsText="Não existem dados registados!" AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridButtonColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="RESET PASSWORD" ShowFilterIcon="false" AutoPostBackOnFilter="false" CommandName="SELECTCLIENTE" FilterControlAltText="Filter SELECTCLIENTE column"></telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn ShowFilterIcon="false" DataField="UserId" ReadOnly="True" HeaderText="UserID" SortExpression="UserId" UniqueName="UserId" DataType="System.Guid" FilterControlAltText="Filter UserId column"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>

                        </div>

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

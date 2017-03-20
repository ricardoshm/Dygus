<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirOperadora.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirOperadora" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="page-header">
            <div class="pull-left">
                <h4><i class="icon-file-alt"></i>Operadoras | Inserir</h4>
            </div>
            <div class="pull-right">
                <ul class="bread">
                    <li><a href="../Default.aspx">Home</a><span class="divider">/</span></li>
                    <li><a href="ListarOperadora.aspx">Operadoras</a><span class="divider">/</span></li>
                    <li class='active'>Inserir</li>
                </ul>
            </div>
        </div>

        <div class="container-fluid" id="content-area">
            <div class="row-fluid">
                <div class="span12">
                    <div class="box">
                        <div class="box-head">
                            <i class="icon-list-ul"></i>
                            <span>Operadoras</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <div class="control-group">
                                    <div class="alert alert-success" id="sucesso" runat="server" visible="false">
                                        <button type="button" class="close" data-dismiss="alert">&times;</button>
                                        <label runat="server" id="sucessoMessage" visible="false"></label>
                                    </div>
                                    <div class="alert alert-error" id="erro" runat="server" visible="false">
                                        <button type="button" class="close" data-dismiss="alert">&times;</button>
                                        <label runat="server" id="errorMessage" visible="false"></label>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label for="textfield" class="control-label">Operadora</label>
                                    <div class="controls">
                                        <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tboperadora" placeholder="Designação da Operadora" CssClass="input-xlarge"></telerik:RadTextBox>
                                    </div>
                                </div>

                                <div class="form-actions">
                                    <asp:Button runat="server" ID="btnGravar" Text="Gravar" CssClass="button button-basic-blue" OnClick="btnGravar_Click" />
                                    <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="button button-basic" OnClick="btnLimpar_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <div class="box">
                        <div class="box-head">
                            <i class="icon-list-ul"></i>
                            <span>Operadoras</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemOperadoras" Skin="MetroTouch" OnNeedDataSource="listagemOperadoras_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="Não existem operadoras registadas!" NoDetailRecordsText="Não existem operadoras registadas!" AutoGenerateColumns="False">

                                        <Columns>
                                            <telerik:GridBoundColumn Visible="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="DESCRICAO" ReadOnly="True" HeaderText="Designação da Operadora" SortExpression="DESCRICAO" UniqueName="DESCRICAO" FilterControlAltText="Filter DESCRICAO column"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
</asp:Content>

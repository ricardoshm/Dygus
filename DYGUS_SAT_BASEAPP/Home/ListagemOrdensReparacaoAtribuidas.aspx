<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListagemOrdensReparacaoAtribuidas.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListagemOrdensReparacaoAtribuidas" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="page-header">
            <div class="pull-left">
                <h4><i class="icon-file-alt"></i>Ordens de Reparação - Gerir</h4>
            </div>
            <div class="pull-right">
                <ul class="bread">
                    <li><a href="Default.aspx">Home</a><span class="divider">/</span></li>
                    <li><a href="#">Ordens de Reparação</a><span class="divider">/</span></li>
                    <li class='active'>Gerir</li>
                </ul>
            </div>
        </div>

        <div class="container-fluid" id="content-area">


            <div class="row-fluid">
                <div class="span12">
                    <div class="box">
                        <div class="box-head">
                            <i class="icon-list-ul"></i>
                            <span>Lista de Ordens de Reparação Atribuídas (Técnicos)</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemordensreparacaoatribuidastecnicos" Skin="MetroTouch" OnNeedDataSource="listagemordensreparacaoatribuidastecnicos_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <ExportSettings>
                                        <Pdf PageWidth=""></Pdf>
                                    </ExportSettings>

                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>

                                    <MasterTableView NoMasterRecordsText="Não existem ordens de reparação atribuídas!" NoDetailRecordsText="Não existem ordens de reparação atribuídas!" AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado O.R." SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Visible="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGO" ReadOnly="True" HeaderText="OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Técnico" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="IMEI" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO_OR" ReadOnly="True" HeaderText="Data OR" SortExpression="DATA_REGISTO_OR" UniqueName="DATA_REGISTO_OR" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO_OR column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-ForeColor="Red" DataField="DATA_PREVISTA_ENTREGA" ReadOnly="True" HeaderText="Data Prevista Entrega" SortExpression="DATA_PREVISTA_ENTREGA" UniqueName="DATA_PREVISTA_ENTREGA" DataType="System.DateTime" FilterControlAltText="Filter DATA_PREVISTA_ENTREGA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome de Técnico" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
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
                            <span>Lista de Ordens de Reparação Atribuídas (Reparadores Externos)</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemordensreparacaoatribuidasreparadores" Skin="MetroTouch"  OnNeedDataSource="listagemordensreparacaoatribuidasreparadores_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <ExportSettings>
                                        <Pdf PageWidth=""></Pdf>
                                    </ExportSettings>

                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>

                                    <MasterTableView NoMasterRecordsText="Não existem ordens de reparação atribuídas!" NoDetailRecordsText="Não existem ordens de reparação atribuídas!" AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO" ReadOnly="True" HeaderText="Estado O.R." SortExpression="ESTADO" UniqueName="ESTADO" FilterControlAltText="Filter ESTADO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Visible="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGO" ReadOnly="True" HeaderText="OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Reparador" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="IMEI" ReadOnly="True" HeaderText="IMEI" SortExpression="IMEI" UniqueName="IMEI" FilterControlAltText="Filter IMEI column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO_OR" ReadOnly="True" HeaderText="Data OR" SortExpression="DATA_REGISTO_OR" UniqueName="DATA_REGISTO_OR" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO_OR column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" ItemStyle-ForeColor="Red" DataField="DATA_PREVISTA_ENTREGA" ReadOnly="True" HeaderText="Data Prevista Entrega" SortExpression="DATA_PREVISTA_ENTREGA" UniqueName="DATA_PREVISTA_ENTREGA" DataType="System.DateTime" FilterControlAltText="Filter DATA_PREVISTA_ENTREGA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="NOME" ReadOnly="True" HeaderText="Nome de Técnico" SortExpression="NOME" UniqueName="NOME" FilterControlAltText="Filter NOME column"></telerik:GridBoundColumn>
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

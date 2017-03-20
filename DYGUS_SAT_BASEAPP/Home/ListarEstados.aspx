<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListarEstados.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListarEstados" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="page-header">
            <div class="pull-left">
                <h4><i class="icon-file-alt"></i>Estados de Ordem de Reparação | Editar</h4>
            </div>
            <div class="pull-right">
                <ul class="bread">
                    <li><a href="Default.aspx">Home</a><span class="divider">/</span></li>
                    <li><a href="ListarEstados.aspx">Estados de Ordem de Reparação</a><span class="divider">/</span></li>
                    <li class='active'>Editar</li>
                </ul>
            </div>
        </div>

        <div class="container-fluid" id="content-area">
            <div class="row-fluid">
                <div class="span12">
                    <div class="box">
                        <div class="box-head">
                            <i class="icon-list-ul"></i>
                            <span>Lista de Estados Registados</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemestados" OnItemDataBound="listagemestados_ItemDataBound" Skin="MetroTouch" OnNeedDataSource="listagemestados_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="Não existem estados registados!" NoDetailRecordsText="Não existem estados registados!" AutoGenerateColumns="False">
                                        <Columns>
                                            <telerik:GridHyperLinkColumn ItemStyle-ForeColor="Green" ItemStyle-BackColor="LightBlue" ItemStyle-Font-Bold="true" Text="Editar" HeaderText="" UniqueName="EDITAR"
                                                Target="_parent" ShowFilterIcon="false" AllowFiltering="False">
                                            </telerik:GridHyperLinkColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="DESCRICAO" ReadOnly="True" HeaderText="Estado" SortExpression="DESCRICAO" UniqueName="DESCRICAO" FilterControlAltText="Filter DESCRICAO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="iD Estado" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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


<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EliminarMarca.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EliminarMarca" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="page-header">
            <div class="pull-left">
                <h4><i class="icon-file-alt"></i>Marcas | Eliminar</h4>
            </div>
            <div class="pull-right">
                <ul class="bread">
                    <li><a href="Default.aspx">Home</a><span class="divider">/</span></li>
                    <li><a href="ListarMarca.aspx">Marcas</a><span class="divider">/</span></li>
                    <li class='active'>Eliminar</li>
                </ul>
            </div>
        </div>

        <div class="container-fluid" id="content-area">
            <div class="row-fluid">
                <div class="span12">
                    <div class="box">
                        <div class="box-head">
                            <i class="icon-list-ul"></i>
                            <span>Lista de Marcas Registadas</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <telerik:RadGrid OnNeedDataSource="listagemmarcasregistadas_NeedDataSource" OnItemCommand="listagemmarcasregistadas_ItemCommand" Culture="pt-PT" runat="server" ID="listagemmarcasregistadas" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <ClientSettings>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                                    </ClientSettings>

                                    <MasterTableView NoMasterRecordsText="Não existem marcas registadas!" NoDetailRecordsText="Não existem marcas registadas!" AutoGenerateColumns="False">

                                        <Columns>
                                            <telerik:GridButtonColumn Text="Eliminar Marca" CommandName="Delete" ButtonType="ImageButton"
                                                ConfirmText="Tem a certeza que deseja eliminar esta marca?" ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Eliminar">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="false" Visible="true" DataField="ID" ReadOnly="True" HeaderText="iD Marca" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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


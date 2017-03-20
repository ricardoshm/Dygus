<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EliminarCondicaoGeral.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EliminarCondicaoGeral" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Condições Gerais</a> <span class="divider">/</span></li>
            <li class="active">Eliminar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Eliminar Condições Gerais</h1>
        </div>


        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Lista de Condições Gerais</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <telerik:RadGrid OnNeedDataSource="listagemcond_NeedDataSource" OnItemCommand="listagemcond_ItemCommand" Culture="pt-PT" runat="server" ID="listagemcond" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem Condições Gerais registadas!" NoDetailRecordsText="Não existem Condições Gerais registadas!" AutoGenerateColumns="False">

                                <Columns>
                                    <telerik:GridButtonColumn Text="Eliminar" CommandName="Delete" ButtonType="ImageButton"
                                        ConfirmText="Tem a certeza que deseja eliminar esta Condição Geral?" ConfirmDialogType="RadWindow"
                                        ConfirmTitle="Eliminar">
                                    </telerik:GridButtonColumn>
                                    <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="DESCRICAO" ReadOnly="True" HeaderText="Descrição" SortExpression="DESCRICAO" UniqueName="DESCRICAO" FilterControlAltText="Filter DESCRICAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" Visible="true" DataField="ID" ReadOnly="True" HeaderText="iD Condição" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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



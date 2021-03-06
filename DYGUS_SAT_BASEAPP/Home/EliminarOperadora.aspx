﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EliminarOperadora.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EliminarOperadora" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="content">
        <div class="page-header">
            <div class="pull-left">
                <h4><i class="icon-file-alt"></i>Operadoras | Eliminar</h4>
            </div>
            <div class="pull-right">
                <ul class="bread">
                    <li><a href="../Default.aspx">Home</a><span class="divider">/</span></li>
                    <li><a href="ListarOperadora.aspx">Operadoras</a><span class="divider">/</span></li>
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
                            <span>Operadoras</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemOperadoras" Skin="MetroTouch" OnNeedDataSource="listagemOperadoras_NeedDataSource" OnItemCommand="listagemOperadoras_ItemCommand" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="Não existem operadoras registadas!" NoDetailRecordsText="Não existem operadoras registadas!" AutoGenerateColumns="False">

                                        <Columns>
                                            <telerik:GridButtonColumn Text="Eliminar Operadora" CommandName="Delete" ButtonType="ImageButton"
                                                ConfirmText="Tem a certeza que deseja eliminar esta operadora?" ConfirmDialogType="RadWindow"
                                                ConfirmTitle="Eliminar">
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn AutoPostBackOnFilter="true" ShowFilterIcon="false" AllowFiltering="true" DataField="DESCRICAO" ReadOnly="True" HeaderText="Designação da Operadora" SortExpression="DESCRICAO" UniqueName="DESCRICAO" FilterControlAltText="Filter DESCRICAO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn Visible="true" AllowFiltering="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                            
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


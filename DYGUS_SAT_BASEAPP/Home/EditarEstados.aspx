<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EditarEstados.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EditarEstados" MaintainScrollPositionOnPostback="true" %>

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
                            <span>Estados de Ordem de Reparação</span>
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
                                    <label for="textfield" class="control-label">Estado</label>
                                    <div class="controls">
                                        <telerik:RadTextBox Width="285" Skin="MetroTouch" runat="server" ID="tbestado" placeholder="Estado da Reparação" CssClass="input-xlarge"></telerik:RadTextBox>
                                    </div>
                                </div>

                                <div class="form-actions">
                                    <asp:Button runat="server" ID="btnGrava" Text="Actualizar Estado" CssClass="button button-basic-blue" OnClick="btnGrava_Click" />
                                    <asp:Button runat="server" ID="btnLimpar" Text="Limpar Estado" CssClass="button button-basic" OnClick="btnLimpar_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

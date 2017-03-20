<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ListagemGeralClientesDetalhe.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ListagemGeralClientesDetalhe" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div id="content">
        

       
                                        

                                    
    </div>--%>


     <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Cliente</a> <span class="divider">/</span></li>
            <li class="active">Ficha</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Ficha de Cliente</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados do Cliente</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">
                            
                            <div class="control-group">
                                <label class="control-label" for="normal">Código de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ForeColor="Green" Font-Bold="true" ID="tbcodcliente" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                              <div class="control-group">
                                <label class="control-label" for="normal">Tipo de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" ReadOnly="true" runat="server" ID="tbtipoCliente" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Nome de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ForeColor="Red" Font-Bold="true" ID="tbnome" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Morada de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ReadOnly="true" ID="tbmorada" TextMode="MultiLine" Height="85" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Código Postal de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="####-###" Width="400" Skin="MetroTouch" runat="server" ReadOnly="true" ID="tbcodpostal" MaxLength="8" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Localidade de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ReadOnly="true" ID="tblocalidade" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Telefone de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox  Mask="###-###-###" Width="400" Skin="MetroTouch" runat="server" ReadOnly="true" ID="tbcontacto" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Email de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ReadOnly="true" ID="tbemail" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">NIF de Cliente</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox  Mask="#########" Width="400" Skin="MetroTouch" runat="server" ReadOnly="true" ID="tbnif" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Data da Última Intervenção</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ReadOnly="true" ID="tbdataultima" MaxLength="9" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>

                            <div class="control-group">
                                <label class="control-label" for="normal">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" Height="85" runat="server" ReadOnly="true" ID="tbobs" TextMode="MultiLine" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                                        
                           
                            <br />
                            <div class="form-actions" id="sucesso" runat="server" visible="false">
                                <label runat="server" id="sucessoMessage" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro" runat="server" visible="false">
                                <label runat="server" id="errorMessage" visible="false" style="color: red;"></label>
                            </div>
                            <br />

                            <div class="form-actions">
                                <asp:Button runat="server" ID="btnVoltar" Text="Voltar à página anterior" CssClass="btn btn-inverse btn-large" OnClick="btnVoltar_Click" />
                            </div>
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>

        
        <!-- End .row-fluid -->
    </div>
</asp:Content>

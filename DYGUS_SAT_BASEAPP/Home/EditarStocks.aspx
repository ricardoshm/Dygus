<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EditarStocks.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EditarStocks" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Artigo</a> <span class="divider">/</span></li>
            <li class="active">Editar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Editar Artigo</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados do Artigo</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">


                            <div class="control-group">
                                <label class="control-label" for="focused">Código do Artigo</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ForeColor="Green" Font-Bold="true" ID="tbcodartigo" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Descrição do Artigo</label>
                                <div class="controls controls-row">
                                    
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Descrição do Artigo" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Qtd. disponível</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox Culture="pt-PT" NumberFormat-DecimalDigits="0" Type="Number" Width="400px" ID="tbqtddisponivel" runat="server" CssClass="span12" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Qtd. mínima</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox Culture="pt-PT" NumberFormat-DecimalDigits="0" Type="Number" Width="400px" ID="tbqtdminima" runat="server" CssClass="span12" Skin="MetroTouch"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Valor Custo</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox NumberFormat-DecimalDigits="2" NumberFormat-AllowRounding="false" Culture="pt-PT" Width="400px" Type="Currency" CssClass="span12" ID="tbvalorcusto" Skin="MetroTouch" runat="server"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Valor Revenda</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox NumberFormat-DecimalDigits="2" NumberFormat-AllowRounding="false" Culture="pt-PT" Width="400px" Type="Currency" CssClass="span12" ID="tbvalorrevenda" Skin="MetroTouch" runat="server"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Valor Venda</label>
                                <div class="controls controls-row">
                                    <telerik:RadNumericTextBox NumberFormat-DecimalDigits="2" NumberFormat-AllowRounding="false" Culture="pt-PT" Width="400px" Type="Currency" CssClass="span12" ID="tbvalorvenda" Skin="MetroTouch" runat="server"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                            
                            <div class="control-group">
                                <label class="control-label" for="password">Armazém</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox ID="ddlarmazem" Width="400px" runat="server" CssClass="span12" Skin="MetroTouch" />
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

        
        <!-- End .row-fluid -->
    </div>
</asp:Content>



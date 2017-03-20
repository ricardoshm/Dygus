<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EditarCondicaoGeral.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EditarCondicaoGeral" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Condições Gerais</a> <span class="divider">/</span></li>
            <li class="active">Editar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Editar Condições Gerais</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados das Condições Gerais</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">


                            <div class="control-group">
                                <label class="control-label" for="focused">Condição Geral</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Height="285" TextMode="MultiLine" Width="400" Skin="MetroTouch" placeholder="Condição Geral" runat="server" ID="tbcond" CssClass="input-xlarge"></telerik:RadTextBox>
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
                                <asp:Button runat="server" ID="btnGravar" Text="Gravar" CssClass="btn btn-primary btn-large" OnClick="btnGravar_Click" />
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




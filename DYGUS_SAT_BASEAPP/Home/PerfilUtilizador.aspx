<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="PerfilUtilizador.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.PerfilUtilizador" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="container-fluid">
        <!-- Start .content-wrapper -->
        <div class="content-wrapper">
            <div class="row">
                <!-- Start .row -->
                <!-- Start .page-header -->
                <div class="col-lg-12 heading">
                    <h1 class="page-header"><i class="im-paragraph-justify"></i>Perfil de utilizador</h1>
                </div>
                <!-- End .page-header -->
            </div>
            <!-- End .row -->
            <div class="outlet">
                <!-- Start .outlet -->
                <!-- Page start here ( usual with .row ) -->
                <div class="row">
                    <!-- Start .row -->
                    <div class="col-lg-12">
                        <!-- Start col-lg-12 -->
                        <div class="panel panel-default toggle">
                            <!-- Start .panel -->
                            <div class="panel-heading">
                                <h3 class="panel-title">Definições</h3>
                            </div>
                            <div class="panel-body">
                                <div class="form-horizontal group-border hover-stripped" role="form">

                                    <div class="form-group">
                                        <label class="col-lg-2 col-md-2 col-sm-12 control-label">Nome de utilizador</label>
                                        <div class="col-lg-10 col-md-10">
                                            <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbusername" CssClass="form-control" ReadOnly="true"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-lg-2 col-md-2 col-sm-12 control-label">Palavra-passe antiga</label>
                                        <div class="col-lg-10 col-md-10">
                                            <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbpassantiga" TextMode="Password" CssClass="form-control"></telerik:RadTextBox>
                                            <asp:HiddenField ID="emailuser" runat="server" Value="" />
                                        </div>
                                    </div>
                                    <!-- End .form-group  -->
                                    <div class="form-group">
                                        <label class="col-lg-2 col-md-2 col-sm-12 control-label">Nova palavra-passe</label>
                                        <div class="col-lg-10 col-md-10">
                                            <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbnovapass" TextMode="Password" CssClass="form-control"></telerik:RadTextBox>
                                        </div>
                                    </div>
                                    <!-- End .form-group  -->
                                    <div class="form-group">
                                        <label class="col-lg-2 col-md-2 col-sm-12 control-label">Confirmação de palavra-passe</label>
                                        <div class="col-lg-10 col-md-10">
                                            <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbconfirmanovapass" TextMode="Password" CssClass="form-control"></telerik:RadTextBox>
                                        </div>
                                    </div>

                                    <br />
                                    <div class="form-group" id="sucesso" runat="server" visible="false">
                                        <div class="col-lg-10 col-md-10">
                                            <label runat="server" id="sucessoMessage" visible="false" style="color: green;"></label>
                                            <
                                        </div>
                                    </div>
                                    <div class="form-group" id="erro" runat="server" visible="false">
                                        <div class="col-lg-10 col-md-10">
                                            <label runat="server" id="errorMessage" visible="false" style="color: red;"></label>
                                        </div>
                                    </div>
                                    <br />

                                    <div class="form-group">
                                        <label class="col-lg-2 col-md-2 col-sm-12 control-label"></label>
                                        <div class="col-lg-10 col-md-10">
                                            <asp:Button runat="server" ID="btnGravar" Text="Gravar" CssClass="btn btn-primary btn-large" OnClick="btnGravar_Click" />
                                            <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="btn btn-inverse btn-large" OnClick="btnLimpar_Click" />
                                            <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger btn-large" OnClick="btnCancelar_Click" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- End .panel -->
                    </div>
                    <!-- End col-lg-12 -->
                </div>

            </div>
            <!-- End .outlet -->
        </div>
        <!-- End .content-wrapper -->
        <div class="clearfix"></div>
    </div>

</asp:Content>

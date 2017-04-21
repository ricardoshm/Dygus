<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Default" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Start #login -->
    <div id="login" class="animated bounceIn">
        <img id="logo" src="img/logo_dygus_sat.png" alt="sprFlat Logo" />
        <!-- Start .login-wrapper -->
        <div class="login-wrapper">
            <ul id="myTab" class="nav nav-tabs nav-justified bn">
                <li><a href="#log-in" data-toggle="tab">Login</a></li>
                <li><a href="#register" data-toggle="tab">Recuperação de conta</a></li>
            </ul>
            <div id="myTabContent" class="tab-content bn">
                <div class="tab-pane fade active in" id="log-in">
                    <div class="seperator">
                        <strong>
                            <label id="versao" runat="server"></label>
                        </strong>
                        <hr />
                    </div>
                    <div class="form-horizontal mt10" id="login-form" role="form">
                        <div class="form-group">
                            <div class="col-lg-12">
                                <asp:TextBox runat="server" ID="tbusername" CssClass="form-control left-icon" placeholder="Nome de Utilizador (Email)"></asp:TextBox>
                                <i class="ec-user s16 left-input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-12">
                                <asp:TextBox runat="server" ID="tbpassword" TextMode="Password" placeholder="Palavra-Passe" CssClass="form-control left-icon"></asp:TextBox>
                                <i class="ec-locked s16 left-input-icon"></i>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-12">
                                <label id="erro" runat="server" style="color: red; text-align: justify;" visible="false" class="span12"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-8">
                            </div>
                            <!-- col-lg-12 end here -->
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-4">
                                <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Entrar" CssClass="btn btn-success pull-right" />
                            </div>
                            <!-- col-lg-12 end here -->
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="register">
                    <div class="form-horizontal mt20" id="register-form" role="form">
                        <div class="form-group">
                            <div class="col-lg-12">
                                <!-- col-lg-12 start here -->
                                <asp:TextBox runat="server" ID="tbUsernameRecover" CssClass="form-control left-icon" placeholder="Nome de Utilizador (Email)"></asp:TextBox>
                                <i class="ec-mail s16 left-input-icon"></i>
                            </div>
                            <!-- col-lg-12 end here -->
                        </div>
                        <div class="form-group">
                            <div class="col-lg-12">
                                <label id="erroRecover" runat="server" style="color: red; text-align: justify;" visible="false" class="span12"></label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-lg-12">
                                <!-- col-lg-12 start here -->
                                <asp:Button runat="server" ID="btnRecover" OnClick="btnRecover_Click" Text="Recuperar conta" CssClass="btn btn-success pull-right" />
                            </div>
                            <!-- col-lg-12 end here -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- End #.login-wrapper -->
    </div>
    <!-- End #login -->
    <!-- Javascripts -->
    <!-- Load pace first -->
    <script src="assets/plugins/core/pace/pace.min.js"></script>
    <!-- Important javascript libs(put in all pages) -->
    <script>window.jQuery || document.write('<script src="assets/js/libs/jquery-2.1.1.min.js">\x3C/script>')</script>
    <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
    <script>window.jQuery || document.write('<script src="assets/js/libs/jquery-ui-1.10.4.min.js">\x3C/script>')</script>
    <!--[if lt IE 9]>
          <script type="text/javascript" src="assets/js/libs/excanvas.min.js"></script>
          <script type="text/javascript" src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
          <script type="text/javascript" src="assets/js/libs/respond.min.js"></script>
        <![endif]-->
    <!-- build:js assets/js/pages/login.js -->
    <!-- Bootstrap plugins -->
    <script src="assets/js/bootstrap/bootstrap.js"></script>
    <!-- Form plugins -->
    <script src="assets/plugins/forms/icheck/jquery.icheck.js"></script>
    <script src="assets/plugins/forms/validation/jquery.validate.js"></script>
    <script src="assets/plugins/forms/validation/additional-methods.min.js"></script>
    <!-- Init plugins olny for this page -->
    <script src="assets/js/pages/login.js"></script>
    <!-- endbuild -->
    <!-- Google Analytics:  -->

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Default" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div id="login">
            <div class="login-wrapper" data-active="log">
                <a class="brand" href="Default.aspx">
                    <img src="img/logo_dygus_sat.png" alt="Dygus :: SAT" /></a>
                <div id="log">
                    <div class="page-header">
                        <h3 class="center">Formulário de Login</h3>
                        <h4 class="center" id="versao" runat="server"></h4>
                    </div>
                    <div id="login-form" class="form-horizontal">
                        <div class="row-fluid">
                            <div class="control-group">
                                <div class="controls-row">
                                    <asp:TextBox runat="server" ID="tbusername" CssClass="span12" placeholder="Nome de Utilizador (Email)"></asp:TextBox>
                                </div>

                                <div class="controls-row">
                                    <asp:TextBox runat="server" ID="tbpassword" TextMode="Password" placeholder="Palavra-Passe" CssClass="span12"></asp:TextBox>
                                </div>
                            </div>
                            <!-- End .control-group -->
                            <div class="form-actions full">
                                <%--<label class="checkbox pull-left">
                                    <input type="checkbox" value="1" name="remember">
                                    <span class="pad-left5">Remember me ?</span>
                                </label>--%>
                                <label id="erro" runat="server" style="color: red; text-align: justify;" visible="false" class="span12"></label>
                                <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Text="Entrar" CssClass="btn btn-primary btn-large pull-right span5" />


                            </div>
                        </div>
                        <!-- End .row-fluid -->
                    </div>
                </div>
            </div>
            <div id="bar" data-active="log">
                <div class="btn-group btn-group-vertical">
                    <a id="A1" href="Default.aspx" class="btn tipR" title="Formulário de Login"><i class="icon16 i-key"></i></a>
                    <a id="A3" href="RecuperacaoConta.aspx" class="btn tipR" title="Recuperação de Conta"><i class="icon16 i-question"></i></a>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
    </div>
</asp:Content>

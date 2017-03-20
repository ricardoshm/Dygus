<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../js/pages/error-pages.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="errorContainer">
            <div class="page-header">
                <h1 class="center">! Erro !</h1>
            </div>
            <h2 class="center gap20" id="mensagemErro" runat="server"></h2>
            <div class="center gap-bottom5">
                <hr class="seperator">
                <a href="Default.aspx" class="btn"><i class="icon16 i-home-6 gap-left0"></i>Voltar à Página Inicial</a>
            </div>
        </div>
    </div>
</asp:Content>

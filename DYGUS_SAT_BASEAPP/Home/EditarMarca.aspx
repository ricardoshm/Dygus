<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EditarMarca.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EditarMarca" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function validaCampos() {

            var mensagem = "";

            if (document.getElementById('<%= tbmarca.ClientID %>').value == "")
                mensagem = "Campo Marca é de preenchimento obrigatório.<br/>";

            var mensagemfinal = mensagem;

            if (mensagemfinal != "") {
                document.getElementById('<%= errorMessage.ClientID %>').innerHTML = mensagemfinal;
                document.getElementById('<%= erro.ClientID %>').style.display = "block";
                document.getElementById('<%= errorMessage.ClientID %>').style.display = "block";
                return false;
            }
            else
                return true;
        }
    </script>
    <div id="content">
        <div class="page-header">
            <div class="pull-left">
                <h4><i class="icon-file-alt"></i>Marcas | Inserir</h4>
            </div>
            <div class="pull-right">
                <ul class="bread">
                    <li><a href="Default.aspx">Home</a><span class="divider">/</span></li>
                    <li><a href="ListarMarca.aspx">Marcas</a><span class="divider">/</span></li>
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
                            <span>Marcas</span>
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
                                    <label for="textfield" class="control-label">Marca</label>
                                    <div class="controls">
                                        <telerik:RadTextBox Width="285" Skin="MetroTouch" placeholder="Marca" runat="server" ID="tbmarca" CssClass="input-xlarge"></telerik:RadTextBox>
                                    </div>
                                </div>

                                <div class="form-actions">
                                    <asp:Button runat="server" ID="btnGravarMarca" Text="Actualizar Marca" CssClass="button button-basic-blue" OnClick="btnGravarMarca_Click" OnClientClick="return validaCampos();" />
                                    <asp:Button runat="server" ID="btnLimparMarca" Text="Limpar Marca" CssClass="button button-basic" OnClick="btnLimparMarca_Click" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

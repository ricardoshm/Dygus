<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="EditarLoja.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.EditarLoja" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function validationFailed(sender, eventArgs) {
            alert("A dimensão do ficheiro não pode ultrapassar 1MB");
        }

        function validaCampos() {

            var mensagem = "";
            var mensagem1 = "";
            var mensagem2 = "";
            var mensagem3 = "";
            var mensagem4 = "";
            var mensagem5 = "";
            var mensagem6 = "";
            var mensagem7 = "";

            if (document.getElementById('<%= tbcodloja.ClientID %>').value == "")
                mensagem = "Campo Código da Loja é de preenchimento obrigatório.<br/>";

            if (document.getElementById('<%= tbnome.ClientID %>').value == "")
                mensagem1 = "Campo Nome da Loja é de preenchimento obrigatório.<br/>";

            if (document.getElementById('<%= tbmorada.ClientID %>').value == "")
                mensagem2 = "Campo Morada da Loja é de preenchimento obrigatório.<br/>";

            if (document.getElementById('<%= tbcodpostal.ClientID %>').value == "")
                mensagem3 = "Campo Código Postal é de preenchimento obrigatório.<br/>";

            if (document.getElementById('<%= tblocalidade.ClientID %>').value == "")
                mensagem4 = "Campo Localidade da Loja é de preenchimento obrigatório.<br/>";

            if (document.getElementById('<%= tbcontactotel.ClientID %>').value == "")
                mensagem5 = "Campo Contacto Telefónico é de preenchimento obrigatório.<br/>";

            if (document.getElementById('<%= tbnif.ClientID %>').value == "")
                mensagem6 = "Campo NIF é de preenchimento obrigatório.<br/>";

            var nif = document.getElementById('<%= tbnif.ClientID %>').value

            if (nif != "") {
                if (IsValidNIF(nif) == false) {
                    mensagem7 = "O NIF introduzido é inválido!<br/>";
                }
            }

            var mensagemfinal = mensagem + mensagem1 + mensagem2 + mensagem3 + mensagem4 + mensagem5 + mensagem6 + mensagem7;

            if (mensagemfinal != "") {
                document.getElementById('<%= errorMessage.ClientID %>').innerHTML = mensagemfinal;
                document.getElementById('<%= erro.ClientID %>').style.display = "block";
                document.getElementById('<%= errorMessage.ClientID %>').style.display = "block";
                return false;
            }
            else
                return true;
        }

        function IsValidNIF(nif) {
            var c;
            var checkDigit = 0;
            if (nif != null && IsNumeric(nif) && nif.length == 9) {
                c = nif.charAt(0);
                if (c == '1' || c == '2' || c == '5' || c == '6' || c == '8' || c == '9') {
                    checkDigit = c * 9;
                    var i = 0;
                    for (i = 2; i <= 8; i++) {
                        checkDigit += nif.charAt(i - 1) * (10 - i);
                    }
                    checkDigit = 11 - (checkDigit % 11);
                    if (checkDigit >= 10)
                        checkDigit = 0;
                    if (checkDigit == nif.charAt(8)) {
                        return true;
                    }
                }
            }
            return false;
        }
    </script>
    <script type="text/javascript">
        function validationFailed(sender, eventArgs) {
            alert("A dimensão do ficheiro não pode ultrapassar 1MB");
        }

    </script>

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Loja</a> <span class="divider">/</span></li>
            <li class="active">Editar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Editar Loja</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Dados da Loja</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Código da Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ForeColor="Green" Font-Bold="true" ID="tbcodloja" ReadOnly="true" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="focused">Nome da Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbnome" placeholder="Nome da Loja" CssClass="span12"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbnome" ID="validator1" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Morada da Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tbmorada" TextMode="MultiLine" placeholder="Morada" CssClass="span12"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbmorada" ID="validator2" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="tooltip">Código Postal</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="####-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcodpostal" placeholder="Código Postal" MaxLength="8" CssClass="span12"></telerik:RadMaskedTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbcodpostal" ID="validator3" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="placeholder">Localidade</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Width="400" Skin="MetroTouch" runat="server" ID="tblocalidade" placeholder="Localidade" CssClass="span12"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tblocalidade" ID="validator4" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="readonly">Telefone</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcontactotel" placeholder="Contacto telefónico" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbcontactotel" ID="validator5" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="disabled">Fax</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="###-###-###" Width="400" Skin="MetroTouch" runat="server" ID="tbcontactofax" placeholder="Contacto fax" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="maxlenght">NIF</label>
                                <div class="controls controls-row">
                                    <telerik:RadMaskedTextBox Mask="#########" Width="400" Skin="MetroTouch" runat="server" ID="tbnif" placeholder="NIF" MaxLength="9" CssClass="span12"></telerik:RadMaskedTextBox>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbnif" ID="validator6" Text="Campo Obrigatório!" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Actual Logotipo Loja</label>
                                <div class="controls controls-row">
                                    <asp:Image runat="server" ID="logo" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="normal">Logotipo Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadAsyncUpload runat="server" MaxFileSize="1048576" TargetFolder="~/TempFiles" TemporaryFolder="~/TempFiles" MaxFileInputsCount="1" MultipleFileSelection="Disabled" OnClientValidationFailed="validationFailed" ID="uploadLogo" Skin="MetroTouch" Culture="pt-PT" AllowedFileExtensions="jpg,jpeg,gif,png">
                                        <Localization Select="Escolher" Remove="Remover" Cancel="Cancelar"></Localization>
                                    </telerik:RadAsyncUpload>

                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="maxlenght">Observações</label>
                                <div class="controls controls-row">
                                    <telerik:RadTextBox Height="85" Width="400" Skin="MetroTouch" runat="server" ID="tbobsloja" TextMode="MultiLine" placeholder="Observações" CssClass="span12"></telerik:RadTextBox>
                                </div>
                            </div>
                            <br />
                            <div class="form-actions" id="sucesso" runat="server" visible="false">
                                <label runat="server" id="sucessoMessage" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro" runat="server">
                                <label runat="server" id="errorMessage" visible="false" style="color: red;"></label>

                            </div>
                            <br />



                            <div class="form-actions">
                                <asp:Button runat="server" ID="btnGrava" Text="Gravar" CssClass="btn btn-primary btn-large" OnClick="btnGrava_Click" />
                                <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="btn btn-inverse btn-large" OnClick="btnLimpar_Click" />
                                <asp:Button runat="server" ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger btn-large" OnClick="btnCancelar_Click" />
                                <asp:Button runat="server" ID="btnReativarLoja" Text="Reactivar Loja" CssClass="btn btn-warning btn-large" OnClick="btnReativarLoja_Click" />
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

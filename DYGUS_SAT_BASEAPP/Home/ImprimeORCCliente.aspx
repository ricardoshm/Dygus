<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeORCCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ImprimeORCCliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dygus :: Support & AfterSales</title>
    <link rel="stylesheet" href="../cssfichacliente/style.css" />
    <link rel="license" href="http://www.opensource.org/licenses/mit-license/" />
    <script src="../jsfichacliente/script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <asp:Literal runat="server" ID="codOR"></asp:Literal>
            <address>
                <p style='font-weight: 600;'>
                    <asp:Literal ID="nomeLoja" runat="server"></asp:Literal>
                </p>
                <p>
                    <asp:Literal runat="server" ID="moradaLoja"></asp:Literal><br>
                    <asp:Literal runat="server" ID="localidadecodpostalloja"></asp:Literal>
                </p>
                <p>
                    T&nbsp;:&nbsp;<asp:Literal runat="server" ID="telefoneloja"></asp:Literal>
                </p>
            </address>
            <span>
                <asp:Literal runat="server" ID="logotipoLoja"></asp:Literal>
                <%--<img alt="" src="logo.png"><input type="file" accept="image/*">--%></span>
            <addresscliente>
                <p style='font-weight: 600;'>
                    <asp:Literal runat="server" ID="nomeClienteHeader"></asp:Literal><br>
                    </p>
                   <p> 
                    <asp:Literal runat="server" ID="moradaCliente"></asp:Literal><br>
                    <asp:Literal runat="server" ID="codpostallocalidade"></asp:Literal><br>
                    <br>
                    T&nbsp;:&nbsp;<asp:Literal runat="server" ID="telefonecliente"></asp:Literal><br>
                    E&nbsp;:&nbsp;<asp:Literal runat="server" ID="emailcliente"></asp:Literal><br>
                    <br>
                    NIF&nbsp;:&nbsp;
                    <asp:Literal runat="server" ID="nifcliente"></asp:Literal><br>
                </p>
            </addresscliente>
        </header>
        <article>
            <table class="meta">
                <tr>
                    <th><span>Orçamento</span></th>
                    <asp:Literal runat="server" ID="CodigoOr"></asp:Literal>

                    <th><span>Data de Registo</span></th>
                    <asp:Literal runat="server" ID="dataRegisto"></asp:Literal>
                </tr>
                <tr>
                    

                    <th><span>Equipamento Avariado</span></th>
                    <asp:Literal runat="server" ID="equipAvariado"></asp:Literal>

                    <th><span>IMEI</span></th>
                    <asp:Literal runat="server" ID="imeiequipAvariado"></asp:Literal>
                </tr>

                <tr>
                    <th><span>Valor Orçamento</span></th>
                    <asp:Literal runat="server" ID="valorprevisto"></asp:Literal>

                </tr>

            </table>
            <table class="inventory">
                <thead>
                    <tr style="width: 100%;">
                        <th style="width: 100%;"><span>Descrição Avaria</span></th>

                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <asp:Literal runat="server" ID="descricaoavaria"></asp:Literal>

                    </tr>
                </tbody>
            </table>


            <table class="inventory">
                <thead>
                    <tr style="width: 100%;">
                        <th style="width: 100%;"><span>Observações</span></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal runat="server" ID="obsOrcamento"></asp:Literal>
                </tbody>
            </table>

        </article>
        <aside>
            <h1><span>Condições Gerais de Reparação</span>&nbsp;<span id="numOr" runat="server"></span></h1>
            <div>
                <asp:Literal runat="server" ID="condgerais"></asp:Literal>
            </div>
            <br />
            <div>
                <p>
                    <input type="checkbox" />&nbsp;<asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Declaro que tomei conhecimento e aceito as condições gerais acima descritas."></asp:Label>
                </p>
                <br />
                <table class="inventory">
                    <thead>
                        <tr style="width: 100%;">
                            <th style="width: 100%;"><span>Descrição da Avaria do Equipamento</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <asp:Literal ID="descricaoAvariaEquipamento" runat="server"></asp:Literal>
                        </tr>
                    </tbody>
                </table>
                <table class="inventory">
                    <thead>
                        <tr style="width: 100%;">
                            <th style="width: 20%;"><span>Nome do Cliente</span></th>
                            <th style="width: 20%;"><span>Telefone de Contacto</span></th>
                            <th style="width: 20%;"><span>Data</span></th>
                            <th style="width: 40%;"><span>Assinatura</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <asp:Literal ID="nomeCliente" runat="server"></asp:Literal>
                            <asp:Literal ID="contactocliente" runat="server"></asp:Literal>
                            <asp:Literal ID="datahoje" runat="server"></asp:Literal>
                            <asp:Literal ID="assinatura" runat="server"></asp:Literal>
                        </tr>
                    </tbody>
                </table>
            </div>
        </aside>
    </form>
</body>
</html>

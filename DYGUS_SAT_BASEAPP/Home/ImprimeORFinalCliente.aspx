<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeORFinalCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ImprimeORFinalCliente" %>

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
            <h1>
                <asp:Literal runat="server" ID="codOR"></asp:Literal></h1>
            <address>
                <p style='font-weight: 600;'>
                    <asp:Literal ID="nomeLoja" runat="server"></asp:Literal>
                </p>
                <p>
                    <asp:Literal runat="server" ID="moradaLoja"></asp:Literal><br />
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
                    <asp:Literal runat="server" ID="nomeCliente"></asp:Literal><br />
                    </p>
                <p>
                    <asp:Literal runat="server" ID="moradaCliente"></asp:Literal><br />
                    <asp:Literal runat="server" ID="codpostallocalidade"></asp:Literal><br />
                    <br />
                    T&nbsp;:&nbsp;<asp:Literal runat="server" ID="telefonecliente"></asp:Literal><br />
                    E&nbsp;:&nbsp;<asp:Literal runat="server" ID="emailcliente"></asp:Literal><br />
                    <br />
                    NIF&nbsp;:&nbsp;
                    <asp:Literal runat="server" ID="nifcliente"></asp:Literal><br />
                </p>
            </addresscliente>
        </header>
        <article>
            <table class="meta">
                <tr>
                    <th><span>Ordem de Reparação</span></th>
                    <asp:Literal runat="server" ID="codOr2"></asp:Literal>

                    <th><span>Data de Registo</span></th>
                    <asp:Literal runat="server" ID="dataRegistoOR"></asp:Literal>
                </tr>
                <tr>
                    <th><span>Data Levantamento de Equipamento Avariado</span></th>
                    <asp:Literal runat="server" ID="dataLevantamentoEquipamento"></asp:Literal>

                    <th><span>Valor Final</span></th>
                    <asp:Literal runat="server" ID="valorfinal"></asp:Literal>
                </tr>
                <tr>
                    <th><span>Equipamento Avariado</span></th>
                    <asp:Literal runat="server" ID="equipAvariado"></asp:Literal>

                    <th><span>IMEI</span></th>
                    <asp:Literal runat="server" ID="imeiequipAvariado"></asp:Literal>
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
                        <th style="width: 10%;"><span>Bateria</span></th>
                        <th style="width: 10%;"><span>Carregador</span></th>
                        <th style="width: 10%;"><span>Cartão SIM</span></th>
                        <th style="width: 10%;"><span>Bolsa</span></th>
                        <th style="width: 10%;"><span>Cartão de Memória</span></th>
                        <th style="width: 10%;"><span>Caixa</span></th>
                        <th style="width: 10%;"><span>Outros</span></th>
                        <th style="width: 30%;"><span>Novo IMEI</span></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal runat="server" ID="bateria"></asp:Literal>
                    <asp:Literal runat="server" ID="carregador"></asp:Literal>
                    <asp:Literal runat="server" ID="cartaosim"></asp:Literal>
                    <asp:Literal runat="server" ID="bolsa"></asp:Literal>
                    <asp:Literal runat="server" ID="cartaom"></asp:Literal>
                    <asp:Literal runat="server" ID="caixa"></asp:Literal>
                    <asp:Literal runat="server" ID="outros"></asp:Literal>
                    <asp:Literal runat="server" ID="novoimei"></asp:Literal>
                </tbody>
            </table>
            <div id="tabelamo" runat="server">
                <table class="inventory">
                    <thead>
                        <tr style="width: 100%;">
                            <th style="width: 95%;"><span>Descrição dos trabalhos realizados</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Literal runat="server" ID="descricaotrabalhosrealizados"></asp:Literal>
                    </tbody>
                </table>
            </div>
            <div id="tabelaArtigos" runat="server">
                <table class="inventory">
                    <thead>
                        <tr style="width: 100%;">
                            <th style="width: 5%;"><span>Qtd.</span></th>
                            <th style="width: 95%;"><span>Descrição Artigos</span></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <asp:Literal runat="server" ID="descricaoartigos"></asp:Literal>
                        </tr>
                    </tbody>
                </table>
            </div>
            <table class="inventory" style="margin: 0 0 5em;">
                <thead>
                    <tr style="width: 100%;">
                        <th style="width: 100%;"><span>Observações adicionais</span></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal runat="server" ID="obsOrdensReparacao"></asp:Literal>
                </tbody>
            </table>
            <table class="balance">
                <tr>
                    <th><span style="font-weight: bolder;">Valor Final</span></th>
                    <asp:Literal runat="server" ID="valorfinal2"></asp:Literal>
                </tr>
            </table>
        </article>

    </form>
</body>
</html>

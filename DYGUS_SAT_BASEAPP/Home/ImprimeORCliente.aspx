<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeORCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ImprimeORCliente" %>

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
                    <asp:Literal runat="server" ID="nomeClienteHeader"></asp:Literal><br />
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
                    <asp:Literal runat="server" ID="CodigoOr"></asp:Literal>

                    <th><span>Data de Registo</span></th>
                    <asp:Literal runat="server" ID="dataRegisto"></asp:Literal>
                </tr>
                <tr>
                    <th><span>Data Prevista de Entrega</span></th>
                    <asp:Literal runat="server" ID="dataPrevistaEntrega"></asp:Literal>

                    <th><span>Equipamento de Substituição</span></th>
                    <asp:Literal runat="server" ID="equipSubst"></asp:Literal>
                </tr>
                <tr>
                    <th><span>Valor Previsto de Reparação</span></th>
                    <asp:Literal runat="server" ID="valorprevisto"></asp:Literal>

                    <th><span>Garantia do Equipamento Avariado</span></th>
                    <asp:Literal runat="server" ID="garantiaequipavariado"></asp:Literal>
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
                        <th style="width: 12%;"><span>Bateria</span></th>
                        <th style="width: 12%;"><span>Carregador</span></th>
                        <th style="width: 12%;"><span>Cartão SIM</span></th>
                        <th style="width: 12%;"><span>Bolsa</span></th>
                        <th style="width: 12%;"><span>Cartão de Memória</span></th>
                        <th style="width: 12%;"><span>Caixa</span></th>
                        <th style="width: 15.5%;"><span>Cód. Segurança</span></th>
                        <th style="width: 12%;"><span>Outros</span></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal runat="server" ID="bateria"></asp:Literal>
                    <asp:Literal runat="server" ID="carregador"></asp:Literal>
                    <asp:Literal runat="server" ID="cartaosim"></asp:Literal>
                    <asp:Literal runat="server" ID="bolsa"></asp:Literal>
                    <asp:Literal runat="server" ID="cartaom"></asp:Literal>
                    <asp:Literal runat="server" ID="caixa"></asp:Literal>
                    <asp:Literal runat="server" ID="codseg"></asp:Literal>
                    <asp:Literal runat="server" ID="outros"></asp:Literal>
                </tbody>
            </table>

            <table class="inventory" style="margin: 0 0 2em;">
                <thead>
                    <tr style="width: 100%;">
                        <th style="width: 100%;"><span>Observações</span></th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Literal runat="server" ID="obsOrdensReparacao"></asp:Literal>
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

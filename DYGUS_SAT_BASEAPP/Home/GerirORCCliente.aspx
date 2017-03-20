<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="GerirORCCliente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.GerirORCCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadCodeBlock runat="server" ID="code">
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";



                if (confirm("Tem a certeza que pretende rejeitar o orçamento? Este processo é irreversível!")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>
    </telerik:RadCodeBlock>
    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="#"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Orçamentos</a> <span class="divider">/</span></li>
            <li class="active">Gerir/Editar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-file"></i>Gerir/Editar Orçamentos</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-file"></i></div>
                        <h4>Orçamento <span class="invoice-num" id="codOrcamento" runat="server"></span></h4>
                        <%--<div class="w-right">
                            <button id="print-btn" onclick="window.print();" class="btn btn-danger btn-full tip" title="Print invoice" rel="widget-content"><i class="icon20 i-print-2 gap-right0"></i></button>
                        </div>--%>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content printArea">
                        <div class="row-fluid">
                            <div class="page-header">
                                <h2 class="center">
                                    <asp:Literal ID="logoLoja" runat="server"></asp:Literal></h2>
                            </div>
                            <div class="pad5">Orçamento: <strong class="red" id="numOrcamento" runat="server"></strong></div>
                            <div class="pad5">Registo: <strong class="red" id="dataOrcamento" runat="server"></strong></div>
                            <div class="pad5">Última Modificação: <strong class="red" id="dataUltModOrcamento" runat="server"></strong></div>
                            <div class="pad5">Estado: <strong class="red" id="estadoOrc" runat="server"></strong></div>
                            <div class="row-fluid">
                                <div class="span6 from">
                                    <div class="page-header">
                                        <h4>Loja:</h4>
                                    </div>
                                    <ul class="unstyled">
                                        <asp:Literal runat="server" ID="loja"></asp:Literal>
                                    </ul>
                                </div>
                                <div class="span6 to">
                                    <div class="page-header">
                                        <h4>Cliente:</h4>
                                    </div>
                                    <ul class="unstyled">
                                        <asp:Literal runat="server" ID="cliente"></asp:Literal>
                                    </ul>
                                </div>
                            </div>
                            <!-- End .row-fluid -->
                            <div class="row-fluid">
                                <div class="span12">
                                    <h3>Orçamento:</h3>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Descrição</th>
                                                <th>Valor Orçamentado</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Literal runat="server" ID="linhasOrc"></asp:Literal>


                                        </tbody>
                                    </table>
                                </div>
                                <!-- End .span12 -->
                            </div>
                            <div class="row-fluid">
                                <div class="span12">
                                    <!-- End .widget-title -->
                                    <div class="form-horizontal">
                                        <div class="control-group">
                                            <div class="controls-row">
                                                <br />
                                                <asp:Button runat="server" ID="btnAceitar" Text="Converter em OR" CssClass="btn btn-success span3" OnClick="btnAceitar_Click" />
                                                <asp:Button runat="server" ID="btnRejeitar" Text="Cancelar Orçamento" CssClass="btn btn-danger span3" OnClick="btnRejeitar_Click" OnClientClick="Confirm()" />
                                                <asp:Button runat="server" ID="btnConsultarOR" Text="Editar OR Associada" CssClass="btn btn-inverse span3" OnClick="btnConsultarOR_Click" Visible="false" />
                                                <asp:Button runat="server" ID="btImprimir" Text="Imprimir Orçamento" CssClass="btn btn-primary span3" OnClick="btImprimir_Click" OnClientClick="window.document.forms[0].target='_blank';"></asp:Button>
                                            </div>
                                        </div>
                                        <!-- End .control-group -->
                                    </div>
                                    <!-- End .widget-content -->
                                    <!-- End .widget -->
                                </div>
                            </div>
                            <!-- End .row-fluid -->

                            <!-- End .row-fluid -->
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span12 -->
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-cube"></i></div>
                        <h4>Comentários</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content ">
                        <div class="chat-layout">
                            <ul>
                                <asp:Literal runat="server" ID="comentarioCliente"></asp:Literal>
                                <asp:Literal runat="server" ID="comentarioAdmin"></asp:Literal>
                            </ul>
                            <div class="form-horizontal" id="formComentarioAdmin" runat="server">
                                <div class="control-group">
                                    <div class="controls-row">
                                        <telerik:RadTextBox Width="900" Height="85" Skin="MetroTouch" TextMode="MultiLine" runat="server" ID="tbComentario" placeholder="Comentário" CssClass="span12"></telerik:RadTextBox><br />
                                        <br />
                                        <asp:Button runat="server" ID="btnInserirComent" Text="Inserir Comentário" CssClass="btn btn-primary span3" OnClick="btnInserirComent_Click" />
                                    </div>
                                </div>
                                <!-- End .control-group -->
                            </div>
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
        </div>
        <!-- End .row-fluid -->
    </div>
    <!-- End .container-fluid -->


</asp:Content>

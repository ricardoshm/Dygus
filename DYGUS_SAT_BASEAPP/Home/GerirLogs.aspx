<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="GerirLogs.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.GerirLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <telerik:RadCodeBlock runat="server" ID="code">
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";



                if (confirm("Tem a certeza que pretende remover todos os logs da BD? Este processo é irreversível!")) {
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
            <li><a href="#">Logs</a> <span class="divider">/</span></li>
            <li class="active">Consultar</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-file"></i>Consultar LOGS</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-file"></i></div>
                        <h4>LOGS</h4>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content printArea">
                        <div class="row-fluid">

                            <div class="form-horizontal">

                                <div class="control-group">
                                    <label class="control-label" for="normal">Data de Inicio</label>
                                    <div class="controls controls-row">
                                        <telerik:RadDatePicker runat="server" Width="400" Skin="MetroTouch" ID="datainicio" DateInput-ValidationGroup="datas">
                                            <Calendar runat="server" ID="calendar2">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="Bisque"></telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório!" ValidationGroup="datas" ID="val1" runat="server" SetFocusOnError="true" ControlToValidate="datainicio" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label" for="focused">Data de Fim</label>
                                    <div class="controls controls-row">
                                        <telerik:RadDatePicker runat="server" Width="400" Skin="MetroTouch" ID="datafim" DateInput-ValidationGroup="datas">
                                            <Calendar runat="server" ID="calendar1">
                                                <SpecialDays>
                                                    <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="Bisque"></telerik:RadCalendarDay>
                                                </SpecialDays>
                                            </Calendar>
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório!" ValidationGroup="datas" ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" ControlToValidate="datafim" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <asp:Button runat="server" ID="btnPesquisa" ValidationGroup="datas" Text="Pesquisar" CssClass="btn btn-primary btn-large" OnClick="btnPesquisa_Click" />
                                    <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="btn btn-inverse btn-large" OnClick="btnLimpar_Click" />
                                    <asp:Button runat="server" ID="btnLimparLogs" Text="Remover Logs da BD" CssClass="btn btn-danger btn-large" OnClick="btnLimparLogs_Click" OnClientClick="Confirm()" />
                                </div>

                                <br />
                                <div class="form-actions" id="erro" runat="server" visible="false">
                                    <label runat="server" id="errorMessage" visible="false" style="color: red;"></label>
                                </div>
                                <br />
                            </div>
                            <div class="row-fluid">
                                <div class="span12">
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th style="width: 15%;">Data de Registo</th>
                                                <th style="width: 20%;">Utilizador</th>
                                                <th style="width: 15%;">Menu</th>
                                                <th style="width: 50%;">Descrição</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Literal runat="server" ID="linhasOrc"></asp:Literal>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- End .span12 -->
                            </div>
                        </div>
                        <!-- End .widget-content -->
                    </div>
                    <!-- End .widget -->
                </div>
                <!-- End .span12 -->
            </div>

            <!-- End .row-fluid -->
        </div>
    </div>
    <!-- End .container-fluid -->
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="InserirModelo.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.InserirModelo" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

        function validaCampos() {

            var mensagem = "";

            if (document.getElementById('<%= tbmodelo.ClientID %>').value == "")
                mensagem = "Campo Modelo é de preenchimento obrigatório.<br/>";

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
                <h4><i class="icon-file-alt"></i>Modelos | Inserir</h4>
            </div>
            <div class="pull-right">
                <ul class="bread">
                    <li><a href="Default.aspx">Home</a><span class="divider">/</span></li>
                    <li><a href="ListarModelo.aspx">Modelos</a><span class="divider">/</span></li>
                    <li class='active'>Inserir</li>
                </ul>
            </div>
        </div>

        <div class="container-fluid" id="content-area">
            <div class="row-fluid">
                <div class="span12">
                    <div class="box">
                        <div class="box-head">
                            <i class="icon-list-ul"></i>
                            <span>Modelos</span>
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
                                        <telerik:RadComboBox ID="ddlMarca" Width="285px" AutoPostBack="true" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged" runat="server" CssClass="input-xlarge" Skin="MetroTouch" />
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label for="textfield" class="control-label">Modelo</label>
                                    <div class="controls">
                                        <telerik:RadTextBox Width="285" Skin="MetroTouch" placeholder="Modelo" runat="server" ID="tbmodelo" CssClass="input-xlarge"></telerik:RadTextBox>
                                    </div>
                                </div>
                                <div class="form-actions">
                                    <asp:Button runat="server" ID="btnGravar" Text="Gravar Modelo" CssClass="button button-basic-blue" OnClick="btnGravar_Click" OnClientClick="return validaCampos();" />
                                    <asp:Button runat="server" ID="btnLimpar" Text="Limpar Modelo" CssClass="button button-basic" OnClick="btnLimpar_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row-fluid">
                <div class="span12">
                    <div class="box">
                        <div class="box-head">
                            <i class="icon-list-ul"></i>
                            <span>Lista de Modelos</span>
                        </div>
                        <div class="box-body box-body-nopadding">
                            <div class="form-horizontal">
                                <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemmodelosregistadas" Skin="MetroTouch" OnNeedDataSource="listagemmodelosregistadas_NeedDataSource" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True" AllowPaging="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <MasterTableView NoMasterRecordsText="Não existem modelos registados!" NoDetailRecordsText="Não existem modelos registados!" AutoGenerateColumns="False">

                                        <Columns>
                                            <telerik:GridBoundColumn Visible="false" DataField="ID" ReadOnly="True" HeaderText="ID" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="MODELO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO" UniqueName="MODELO" FilterControlAltText="Filter MODELO column"></telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="true" AutoPostBackOnFilter="true" ShowFilterIcon="false" DataField="MARCA" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA" UniqueName="MARCA" FilterControlAltText="Filter MARCA column"></telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
    </div>
</asp:Content>


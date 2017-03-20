<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="PesquisaGeralOrdensReparacao.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.PesquisaGeralOrdensReparacao" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Relatórios</a> <span class="divider">/</span></li>
            <li class="active">Pesquisa OR's</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Relatórios</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Pesquisa por data</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Data de Inicio</label>
                                <div class="controls controls-row">
                                    <telerik:RadDatePicker runat="server" Width="400" Skin="MetroTouch" ID="datainicio">
                                        <Calendar runat="server" ID="calendar2">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="Bisque"></telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="focused">Data de Fim</label>
                                <div class="controls controls-row">
                                    <telerik:RadDatePicker runat="server" Width="400" Skin="MetroTouch" ID="datafim">
                                        <Calendar runat="server" ID="calendar1">
                                            <SpecialDays>
                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-BackColor="Bisque"></telerik:RadCalendarDay>
                                            </SpecialDays>
                                        </Calendar>
                                    </telerik:RadDatePicker>
                                </div>
                            </div>



                            <br />
                            <div class="form-actions" id="sucesso" runat="server" visible="false">
                                <label runat="server" id="sucessoMessage" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erro" runat="server" visible="false">
                                <label runat="server" id="errorMessage" visible="false" style="color: red;"></label>
                            </div>
                            <br />

                            <div class="form-actions">
                                <asp:Button runat="server" ID="btnPesquisa" Text="Pesquisar" CssClass="btn btn-primary" OnClick="btnPesquisa_Click" />
                                <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="btn" OnClick="btnLimpar_Click" />

                            </div>
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Resultados da Pesquisa</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">


                        <telerik:RadGrid Culture="pt-PT" OnItemDataBound="listagemgeralors_ItemDataBound" runat="server" ID="listagemgeralors" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação!" NoDetailRecordsText="Não existem ordens de reparação!" AutoGenerateColumns="False">

                                <Columns>

                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column"></telerik:GridHyperLinkColumn>

                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo OR" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA_EQUIP_AVARIADO" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA_EQUIP_AVARIADO" UniqueName="MARCA_EQUIP_AVARIADO" FilterControlAltText="Filter MARCA_EQUIP_AVARIADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO_EQUIP_AVARIADO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO_EQUIP_AVARIADO" UniqueName="MODELO_EQUIP_AVARIADO" FilterControlAltText="Filter MODELO_EQUIP_AVARIADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_ULTIMA_MODIFICACAO" ReadOnly="True" HeaderText="Data Última Intervenção" SortExpression="DATA_ULTIMA_MODIFICACAO" UniqueName="DATA_ULTIMA_MODIFICACAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_ULTIMA_MODIFICACAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_FINAL" DataFormatString="{0:F2} &#8364;" ReadOnly="True" HeaderText="Valor OR" SortExpression="VALOR_FINAL" UniqueName="VALOR_FINAL" DataType="System.Double" FilterControlAltText="Filter VALOR_FINAL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_CONCLUSAO" ReadOnly="True" HeaderText="Data Conclusão" SortExpression="DATA_CONCLUSAO" UniqueName="DATA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_CONCLUSAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                </Columns>

                            </MasterTableView>
                        </telerik:RadGrid>

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
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Pesquisa por loja</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">
                        <div class="form-horizontal">

                            <div class="control-group">
                                <label class="control-label" for="normal">Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox ID="ddlloja" Width="400px" runat="server" CssClass="span12" Skin="MetroTouch" AutoPostBack="true" OnSelectedIndexChanged="ddlloja_SelectedIndexChanged" />
                                </div>
                            </div>




                            <br />
                            <div class="form-actions" id="sucessoLoja" runat="server" visible="false">
                                <label runat="server" id="sucessoMessageloja" visible="false" style="color: green;"></label>
                            </div>
                            <div class="form-actions" id="erroLoja" runat="server" visible="false">
                                <label runat="server" id="erromessageLoja" visible="false" style="color: red;"></label>
                            </div>
                            <br />


                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>

        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Resultados da Pesquisa</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">


                        <telerik:RadGrid Culture="pt-PT" OnItemDataBound="listagemORsLoja_ItemDataBound" runat="server" ID="listagemORsLoja" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação!" NoDetailRecordsText="Não existem ordens de reparação!" AutoGenerateColumns="False">

                                <Columns>

                                    <telerik:GridHyperLinkColumn ItemStyle-CssClass="btn btn-danger btn-full tip" ItemStyle-Font-Bold="true" Text="Detalhe" AllowSorting="False" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="false" UniqueName="PRINT" FilterControlAltText="Filter PRINT column"></telerik:GridHyperLinkColumn>

                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo OR" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARCA_EQUIP_AVARIADO" ReadOnly="True" HeaderText="Marca" SortExpression="MARCA_EQUIP_AVARIADO" UniqueName="MARCA_EQUIP_AVARIADO" FilterControlAltText="Filter MARCA_EQUIP_AVARIADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MODELO_EQUIP_AVARIADO" ReadOnly="True" HeaderText="Modelo" SortExpression="MODELO_EQUIP_AVARIADO" UniqueName="MODELO_EQUIP_AVARIADO" FilterControlAltText="Filter MODELO_EQUIP_AVARIADO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_ULTIMA_MODIFICACAO" ReadOnly="True" HeaderText="Data Última Intervenção" SortExpression="DATA_ULTIMA_MODIFICACAO" UniqueName="DATA_ULTIMA_MODIFICACAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_ULTIMA_MODIFICACAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_FINAL" DataFormatString="{0:F2} &#8364;" ReadOnly="True" HeaderText="Valor OR" SortExpression="VALOR_FINAL" UniqueName="VALOR_FINAL" DataType="System.Double" FilterControlAltText="Filter VALOR_FINAL column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_CONCLUSAO" ReadOnly="True" HeaderText="Data Conclusão" SortExpression="DATA_CONCLUSAO" UniqueName="DATA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_CONCLUSAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="false" AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
                                </Columns>

                            </MasterTableView>
                        </telerik:RadGrid>

                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span12 -->
        </div>
        <!-- End .row-fluid -->
    </div>

</asp:Content>

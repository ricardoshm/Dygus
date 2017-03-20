<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="PesquisaGeralOrcamentos.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.PesquisaGeralOrcamentos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Relatórios</a> <span class="divider">/</span></li>
            <li class="active">Pesquisa Orçamentos</li>
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


                        <telerik:RadGrid Culture="pt-PT"  runat="server" ID="listagemgeralors" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                             <MasterTableView NoMasterRecordsText="Não existem orçamentos!" NoDetailRecordsText="Não existem orçamentos!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>

                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código ORÇ" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo ORÇ" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" Visible="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID_ESTADO_OR" ReadOnly="True" HeaderText="ID_ESTADO_OR" SortExpression="ID_ESTADO_OR" UniqueName="ID_ESTADO_OR" DataType="System.Int32" FilterControlAltText="Filter ID_ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" Display="false" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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


                        <telerik:RadGrid Culture="pt-PT" runat="server" ID="listagemORsLoja" Skin="MetroTouch" AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings>
                                <Pdf PageWidth=""></Pdf>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                             <MasterTableView NoMasterRecordsText="Não existem orçamentos!" NoDetailRecordsText="Não existem orçamentos!" AutoGenerateColumns="False">
                                <PagerStyle FirstPageToolTip="Primeira Página" LastPageToolTip="Última Página" NextPagesToolTip="Proximas Páginas"
                                    NextPageToolTip="Proxima Página" PagerTextFormat="Change page: {4} &amp;nbsp;Página &lt;strong&gt;{0}&lt;/strong&gt; de &lt;strong&gt;{1}&lt;/strong&gt;, items &lt;strong&gt;{2}&lt;/strong&gt; até &lt;strong&gt;{3}&lt;/strong&gt; de &lt;strong&gt;{5}&lt;/strong&gt;."
                                    PageSizeLabelText="Tamanho da Página:" PrevPagesToolTip="Páginas Anteriores"
                                    PrevPageToolTip="Página Anterior" />
                                <Columns>

                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ReadOnly="True" HeaderText="Código ORÇ" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data Registo ORÇ" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGOCLIENTE" ReadOnly="True" HeaderText="Código Cliente" SortExpression="CODIGOCLIENTE" UniqueName="CODIGOCLIENTE" FilterControlAltText="Filter CODIGOCLIENTE column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" Visible="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID_ESTADO_OR" ReadOnly="True" HeaderText="ID_ESTADO_OR" SortExpression="ID_ESTADO_OR" UniqueName="ID_ESTADO_OR" DataType="System.Int32" FilterControlAltText="Filter ID_ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn ItemStyle-BackColor="LightBlue" ItemStyle-ForeColor="Green" ItemStyle-Font-Bold="true" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ESTADO_OR" ReadOnly="True" HeaderText="Estado OR" SortExpression="ESTADO_OR" UniqueName="ESTADO_OR" FilterControlAltText="Filter ESTADO_OR column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="ID" ReadOnly="True" HeaderText="iD OR" SortExpression="ID" Display="false" UniqueName="ID" DataType="System.Int32" FilterControlAltText="Filter ID column"></telerik:GridBoundColumn>
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

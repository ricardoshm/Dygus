<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="ContaCorrente.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.ContaCorrente" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .pdfButton {
            color: White;
            border: 0;
            height: 48px;
            background: url('../img/btnPdf.gif') no-repeat center;
            cursor: pointer;
        }
    </style>

    <telerik:RadCodeBlock runat="server" ID="code1">
        <script type="text/javascript">
            function requestStart(sender, args) {
                if (args.get_eventTarget().indexOf("DownloadPDF") > 0)
                    args.set_enableAjax(false);
            }
        </script>
        <script type="text/javascript">
            function PrintRadGrid() {
                var previewWnd = window.open('about:blank', '', '', false);
                var sh = '<%= ClientScript.GetWebResourceUrl(listagemgeralors.GetType(),String.Format("Telerik.Web.UI.Skins.{0}.Grid.{0}.css",listagemgeralors.Skin)) %>';
                var styleStr = "<html><head><link href = '" + sh + "' rel='stylesheet' type='text/css'></link></head>";
                var htmlcontent = styleStr + "<body>" + $find('<%= listagemgeralors.ClientID %>').get_element().outerHTML + "</body></html>";
                previewWnd.document.open();
                previewWnd.document.write(htmlcontent);
                previewWnd.document.close();
                previewWnd.print();

                $find("listagemgeralors").GridDataDiv.style.height = "100%";

            }
        </script>
    </telerik:RadCodeBlock>
    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="Default.aspx"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Relatórios</a> <span class="divider">/</span></li>
            <li class="active">Conta Corrente</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-list-4"></i>Conta Corrente</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Termos de Pesquisa</h4>
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
                            <div class="control-group">
                                <label class="control-label" for="password">Mensal</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox AppendDataBoundItems="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged" ID="ddlMes" Width="400px" runat="server" CssClass="span12" Skin="MetroTouch" AutoPostBack="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="0" Text="Por favor seleccione..." />
                                            <telerik:RadComboBoxItem Value="1" Text="Janeiro" />
                                            <telerik:RadComboBoxItem Value="2" Text="Fevereiro" />
                                            <telerik:RadComboBoxItem Value="3" Text="Março" />
                                            <telerik:RadComboBoxItem Value="4" Text="Abril" />
                                            <telerik:RadComboBoxItem Value="5" Text="Maio" />
                                            <telerik:RadComboBoxItem Value="6" Text="Junho" />
                                            <telerik:RadComboBoxItem Value="7" Text="Julho" />
                                            <telerik:RadComboBoxItem Value="8" Text="Agosto" />
                                            <telerik:RadComboBoxItem Value="9" Text="Setembro" />
                                            <telerik:RadComboBoxItem Value="10" Text="Outubro" />
                                            <telerik:RadComboBoxItem Value="11" Text="Novembro" />
                                            <telerik:RadComboBoxItem Value="12" Text="Dezembro" />
                                        </Items>
                                    </telerik:RadComboBox>

                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="password">Estado da OR</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox EnableLoadOnDemand="true" ID="ddlestado" Width="400px" runat="server" CssClass="span12" Skin="MetroTouch" AutoPostBack="false" />

                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="placeholder">Loja</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox EnableLoadOnDemand="true" ID="ddlloja" Width="400px" runat="server" CssClass="span12" Skin="MetroTouch" />
                                </div>
                            </div>
                            <div class="control-group">
                                <label class="control-label" for="tooltip">Técnico</label>
                                <div class="controls controls-row">
                                    <telerik:RadComboBox EnableLoadOnDemand="true" ID="ddlTecnico" Width="400px" runat="server" CssClass="span12" Skin="MetroTouch" AutoPostBack="false" />
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
                                <asp:Button runat="server" ID="btnPesquisa" Text="Pesquisar" CssClass="btn btn-primary btn-large" OnClick="btnPesquisa_Click" />
                                <asp:Button runat="server" ID="btnLimpar" Text="Limpar" CssClass="btn btn-inverse btn-large" OnClick="btnLimpar_Click" />
                                <asp:Button runat="server" ID="btnPrint" Text="Imprimir" CssClass="btn btn-danger btn-large" OnClick="btnPrint_Click" />
                                <asp:Button runat="server" ID="btnExport" Text="Exportar Excel" CssClass="btn btn-success btn-large" OnClick="btnExport_Click" />
                            </div>
                        </div>
                    </div>
                    <!-- End .widget-content -->
                </div>
                <!-- End .widget -->
            </div>
            <!-- End .span6 -->
        </div>

        <div class="row-fluid" runat="server" id="pesquisaloja">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-menu-6"></i></div>
                        <h4>Conta Corrente</h4>
                        <a href="#" class="minimize"></a>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content">

                        <telerik:RadGrid OnItemCreated="listagemgeralors_ItemCreated" OnItemCommand="listagemgeralors_ItemCommand" 
                            MasterTableView-CommandItemSettings-ShowExportToPdfButton="true" Culture="pt-PT" 
                            ShowFooter="true" runat="server" ID="listagemgeralors" Skin="MetroTouch" 
                            AllowFilteringByColumn="True" CellSpacing="0" GridLines="None" AllowSorting="True">
                            <GroupingSettings CaseSensitive="false" />
                            <ExportSettings IgnorePaging="true" OpenInNewWindow="true">
                                <Pdf PageHeight="210mm" PageWidth="297mm" PageTitle="SushiBar menu" DefaultFontFamily="Arial Unicode MS"
                                    PageBottomMargin="20mm" PageTopMargin="20mm" PageLeftMargin="20mm" PageRightMargin="20mm">
                                </Pdf>
                                <%--<Excel Format="Biff" AutoFitImages="true" FileExtension="Xlsx" />--%>
                            </ExportSettings>

                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>

                            <MasterTableView NoMasterRecordsText="Não existem ordens de reparação!" NoDetailRecordsText="Não existem ordens de reparação!" AutoGenerateColumns="False">
                                <CommandItemTemplate>
                                    <asp:Button ID="DownloadPDF" runat="server" Width="100%" CommandName="ExportToPdf"
                                        CssClass="pdfButton"></asp:Button>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="../img/btnPdf.gif" AlternateText="Sushi Bar"
                                        Width="100%"></asp:Image>
                                </CommandItemTemplate>
                                <Columns>


                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="CODIGO" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" ReadOnly="True" HeaderText="Código OR" SortExpression="CODIGO" UniqueName="CODIGO" FilterControlAltText="Filter CODIGO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_REGISTO" ReadOnly="True" HeaderText="Data de Registo" SortExpression="DATA_REGISTO" UniqueName="DATA_REGISTO" DataType="System.DateTime" FilterControlAltText="Filter DATA_REGISTO column"></telerik:GridBoundColumn>
                                    <telerik:GridNumericColumn FooterStyle-Font-Bold="true" FooterStyle-ForeColor="Red" FooterStyle-BackColor="LightBlue" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_CUSTO" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red" DataFormatString="{0:F2} &#8364;" ReadOnly="True" FooterText="Valor Custo:" HeaderText="Valor Custo" SortExpression="VALOR_CUSTO" UniqueName="VALOR_CUSTO" DataType="System.Double" FilterControlAltText="Filter VALOR_CUSTO column" Aggregate="Sum"></telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn FooterStyle-Font-Bold="true" FooterStyle-ForeColor="Red" FooterStyle-BackColor="LightBlue" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="VALOR_FINAL" DataFormatString="{0:F2} &#8364;" ReadOnly="True" FooterText="Valor Final:" HeaderText="Valor Final" SortExpression="VALOR_FINAL" UniqueName="VALOR_FINAL" DataType="System.Double" FilterControlAltText="Filter VALOR_FINAL column" Aggregate="Sum"></telerik:GridNumericColumn>
                                    <telerik:GridNumericColumn FooterStyle-Font-Bold="true" FooterStyle-ForeColor="Red" FooterStyle-BackColor="LightBlue" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="MARGEM" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Green" DataFormatString="{0:F2} &#8364;" ReadOnly="True" FooterText="Margem:" HeaderText="Margem" SortExpression="MARGEM" UniqueName="MARGEM" DataType="System.Double" FilterControlAltText="Filter MARGEM column" Aggregate="Sum"></telerik:GridNumericColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="DATA_CONCLUSAO" ReadOnly="True" HeaderText="Data Conclusão" SortExpression="DATA_CONCLUSAO" UniqueName="DATA_CONCLUSAO" DataType="System.DateTime" FilterControlAltText="Filter DATA_CONCLUSAO column"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" DataField="LOJA" ReadOnly="True" HeaderText="LOJA" SortExpression="Loja" UniqueName="LOJA" FilterControlAltText="Filter LOJA column"></telerik:GridBoundColumn>

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


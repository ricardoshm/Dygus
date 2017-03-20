<%@ Page Title="" Language="C#" MasterPageFile="~/Home/Home.Master" AutoEventWireup="true" CodeBehind="DetalheOrdemReparacao.aspx.cs" Inherits="DYGUS_SAT_BASEAPP.Home.DetalheOrdemReparacao" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="crumb">
        <ul class="breadcrumb">
            <li><a href="#"><i class="icon16 i-home-4"></i>Home</a> <span class="divider">/</span></li>
            <li><a href="#">Library</a> <span class="divider">/</span></li>
            <li class="active">Data</li>
        </ul>
    </div>
    <div class="container-fluid">
        <div id="heading" class="page-header">
            <h1><i class="icon20 i-file"></i>Invoice page</h1>
        </div>
        <div class="row-fluid">
            <div class="span12">
                <div class="widget">
                    <div class="widget-title">
                        <div class="icon"><i class="icon20 i-file"></i></div>
                        <h4>Ordem de Reparação nº <span class="invoice-num" id="numOr" runat="server"></span></h4>
                        <div class="w-right">
                            <button id="print-btn" class="btn btn-danger btn-full tip" title="Print invoice" rel="widget-content"><i class="icon20 i-print-2 gap-right0"></i></button>
                        </div>
                    </div>
                    <!-- End .widget-title -->
                    <div class="widget-content printArea">
                        <div class="row-fluid">
                            <div class="page-header">
                                <h2 class="center" id="logotipo" runat="server"></h2>
                            </div>
                            <div class="pad5">Ordem de Reparação nº <strong class="red" id="numOr2" runat="server"></strong></div>
                            <div class="pad5" id="dataregisto" runat="server"></div>
                            <div class="pad5" id="dataconclusao" runat="server"></div>
                            <div class="pad5 red" id="datalevantamento" runat="server"><strong></strong></div>
                            <div class="row-fluid">
                                <div class="span6 from">
                                    <div class="page-header">
                                        <h4>Loja:</h4>
                                    </div>
                                    <ul class="unstyled">
                                        <li><i class="icon16 i-arrow-right-3"></i>Dados Loja</li>
                                    </ul>
                                </div>
                                <div class="span6 to">
                                    <div class="page-header">
                                        <h4>Cliente:</h4>
                                    </div>
                                    <ul class="unstyled">
                                        <li><i class="icon16 i-arrow-right-3"></i>Dados Cliente</li>
                                    </ul>
                                </div>
                            </div>
                            <!-- End .row-fluid -->
                            <div class="row-fluid">
                                <div class="span12">
                                    <h3>Items:</h3>
                                    <table class="table table-bordered">
                                        <thead>
                                            <tr>
                                                <th>Quantity</th>
                                                <th>Description</th>
                                                <th>Unit price</th>
                                                <th>Discount</th>
                                                <th>Total</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td class="center">1</td>
                                                <td>Create logo for your firm</td>
                                                <td class="center">50$</td>
                                                <td class="center">10%</td>
                                                <td class="center">$45</td>
                                            </tr>
                                            <tr>
                                                <td class="center">2</td>
                                                <td>Make a corporate style for your site</td>
                                                <td class="center">1000$</td>
                                                <td class="center">0</td>
                                                <td class="center">$1000</td>
                                            </tr>
                                            <tr>
                                                <td class="center">3</td>
                                                <td>SEO optimization</td>
                                                <td class="center">200$</td>
                                                <td class="center">20%</td>
                                                <td class="center">$160</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- End .span12 -->
                            </div>
                            <!-- End .row-fluid -->
                            <div class="row-fluid">
                                <div class="span6">
                                    <div class="page-header">
                                        <h3>
                                        Payment method</h4> </div>
                                    <ul class="unstyled">
                                        <li>
                                            <h3>Payment method: <span class="red-smooth">SWIFT</span></h3>
                                        </li>
                                        <li><i class="icon16 i-arrow-right-3"></i>Bank account #</li>
                                        <li><i class="icon16 i-arrow-right-3"></i>SWIFT code</li>
                                        <li><i class="icon16 i-arrow-right-3"></i>IBAN</li>
                                        <li><i class="icon16 i-arrow-right-3"></i>Billing address</li>
                                        <li><i class="icon16 i-arrow-right-3"></i>Name</li>
                                        <li><i class="icon16 i-arrow-right-3"></i>Email: <strong class="red-smooth">some@info.com</strong></li>
                                    </ul>
                                </div>
                                <!-- End .span6 -->
                                <div class="span6">
                                    <div class="page-header">
                                        <h3>Amount Due</h3>
                                    </div>
                                    <table class="table table-bordered">
                                        <tbody>
                                            <tr>
                                                <th>Subtotal</th>
                                                <td class="center">1205$</td>
                                            </tr>
                                            <tr>
                                                <th>Tax</th>
                                                <td class="center">100$</td>
                                            </tr>
                                            <tr>
                                                <th>Shipping</th>
                                                <td class="center">0$</td>
                                            </tr>
                                            <tr>
                                                <th>Total</th>
                                                <td class="center">1305$</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- End .span6 -->
                            </div>
                            <!-- End .row-fluid -->
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
    <!-- End .container-fluid -->
</asp:Content>


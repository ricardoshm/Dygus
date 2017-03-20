using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListarOrcamentoCliente : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid userid = new Guid();
            Guid Role = new Guid();
            string UserName = "";

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    userid = item.UserId;
                }

                var utilizador = from util in DC.aspnet_Users
                                 where util.UserId == userid
                                 select util;

                foreach (var item in utilizador)
                {
                    UserName = item.UserName;
                }

                var p = from d in DC.aspnet_UsersInRoles
                        where d.UserId == userid
                        select d;

                foreach (var item in p)
                {
                    Role = item.RoleId;
                }

                var tipoutilizador = from d in DC.aspnet_Roles
                                     where d.RoleId == Role
                                     select d;

                string regra = "";

                foreach (var item in tipoutilizador)
                {
                    regra = item.RoleName;
                }

                if (regra == "Lojista" || regra == "Cliente")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);


            //(Page.Master.FindControl("or") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("orlist") as HtmlControl).Attributes.Add("class", "active");

        }

        protected void listagemOrcamentosAceites_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Guid userid = new Guid();
            Guid Role = new Guid();
            string UserName = "";

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    userid = item.UserId;
                }

                var utilizador = from util in DC.aspnet_Users
                                 where util.UserId == userid
                                 select util;

                foreach (var item in utilizador)
                {
                    UserName = item.UserName;
                }

                var p = from d in DC.aspnet_UsersInRoles
                        where d.UserId == userid
                        select d;

                foreach (var item in p)
                {
                    Role = item.RoleId;
                }

                var tipoutilizador = from d in DC.aspnet_Roles
                                     where d.RoleId == Role
                                     select d;

                string regra = "";

                foreach (var item in tipoutilizador)
                {
                    regra = item.RoleName;
                }

                if (regra == "Lojista" || regra == "Cliente")
                    Response.Redirect("Default.aspx", false);

                if (regra == "Administrador" || regra == "SuperAdmin")
                {
                    try
                    {
                        var carregaGrid = from ors in DC.Orcamentos
                                          join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                          //join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                          where ors.ID_ESTADO == 2
                                          orderby ors.ID descending
                                          select new
                                          {
                                              ID = ors.ID,
                                              CODIGO = ors.CODIGO,
                                              CODIGOCLIENTE = parceiro.CODIGO,
                                              MARCA = ors.Equipamento_Avariado.Marca.DESCRICAO,
                                              MODELO = ors.Equipamento_Avariado.Modelo.DESCRICAO,
                                              IMEI = ors.Equipamento_Avariado.IMEI,
                                              DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                              ESTADO = ors.Orcamentos_Estado.DESCRICAO,
                                              NOMECLIENTE = parceiro.NOME.ToString()
                                          };

                        listagemOrcamentosAceites.DataSourceID = "";
                        listagemOrcamentosAceites.DataSource = carregaGrid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }
                else
                {
                    try
                    {
                        var carregaGrid = from ors in DC.Orcamentos
                                          join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                          //join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                          where ors.ID_ESTADO == 3
                                          orderby ors.ID descending
                                          select new
                                          {
                                              ID = ors.ID,
                                              CODIGO = ors.CODIGO,
                                              CODIGOCLIENTE = parceiro.CODIGO,
                                              MARCA = ors.Equipamento_Avariado.Marca.DESCRICAO,
                                              MODELO = ors.Equipamento_Avariado.Modelo.DESCRICAO,
                                              IMEI = ors.Equipamento_Avariado.IMEI,
                                              DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                              ESTADO = ors.Orcamentos_Estado.DESCRICAO,
                                              NOMECLIENTE = parceiro.NOME.ToString()
                                          };

                        listagemOrcamentosAceites.DataSourceID = "";
                        listagemOrcamentosAceites.DataSource = carregaGrid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }
            }
            else
                Response.Redirect("~/Default.aspx", true);
        }

        protected void listagemOrcamentosAceites_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "GerirORCCliente.aspx?ID=" + val1;
            }
        }

        protected void listagemOrcamentosPendentes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Guid userid = new Guid();
            Guid Role = new Guid();
            string UserName = "";

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    userid = item.UserId;
                }

                var utilizador = from util in DC.aspnet_Users
                                 where util.UserId == userid
                                 select util;

                foreach (var item in utilizador)
                {
                    UserName = item.UserName;
                }

                var p = from d in DC.aspnet_UsersInRoles
                        where d.UserId == userid
                        select d;

                foreach (var item in p)
                {
                    Role = item.RoleId;
                }

                var tipoutilizador = from d in DC.aspnet_Roles
                                     where d.RoleId == Role
                                     select d;

                string regra = "";

                foreach (var item in tipoutilizador)
                {
                    regra = item.RoleName;
                }

                if (regra == "Lojista" || regra == "Cliente")
                    Response.Redirect("Default.aspx", false);

                if (regra == "Administrador" || regra == "SuperAdmin")
                {
                    try
                    {
                        var carregaGrid = from ors in DC.Orcamentos
                                          join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                          //join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                          where ors.ID_ESTADO == 1 
                                          orderby ors.ID descending
                                          select new
                                          {
                                              ID = ors.ID,
                                              CODIGO = ors.CODIGO,
                                              CODIGOCLIENTE = parceiro.CODIGO,
                                              MARCA = ors.Equipamento_Avariado.Marca.DESCRICAO,
                                              MODELO = ors.Equipamento_Avariado.Modelo.DESCRICAO,
                                              IMEI = ors.Equipamento_Avariado.IMEI,
                                              DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                              ESTADO = ors.Orcamentos_Estado.DESCRICAO,
                                              NOMECLIENTE = parceiro.NOME.ToString()
                                          };

                        listagemOrcamentosPendentes.DataSourceID = "";
                        listagemOrcamentosPendentes.DataSource = carregaGrid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }
                else
                {
                    try
                    {
                        var carregaGrid = from ors in DC.Orcamentos
                                          join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                          //join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                          where ors.ID_ESTADO == 1 || ors.ID_ESTADO == 2
                                          orderby ors.ID descending
                                          select new
                                          {
                                              ID = ors.ID,
                                              CODIGO = ors.CODIGO,
                                              CODIGOCLIENTE = parceiro.CODIGO,
                                              MARCA = ors.Equipamento_Avariado.Marca.DESCRICAO,
                                              MODELO = ors.Equipamento_Avariado.Modelo.DESCRICAO,
                                              IMEI = ors.Equipamento_Avariado.IMEI,
                                              DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                              ESTADO = ors.Orcamentos_Estado.DESCRICAO,
                                              NOMECLIENTE = parceiro.NOME.ToString()
                                          };

                        listagemOrcamentosPendentes.DataSourceID = "";
                        listagemOrcamentosPendentes.DataSource = carregaGrid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }
            }
            else
                Response.Redirect("~/Default.aspx", true);
        }

        protected void listagemOrcamentosPendentes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "GerirORCCliente.aspx?ID=" + val1;
            }
        }

        protected void listagemOrcamentosRejeitadosCancelados_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "GerirORCCliente.aspx?ID=" + val1;
            }
        }

        protected void listagemOrcamentosRejeitadosCancelados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Guid userid = new Guid();
            Guid Role = new Guid();
            string UserName = "";

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    userid = item.UserId;
                }

                var utilizador = from util in DC.aspnet_Users
                                 where util.UserId == userid
                                 select util;

                foreach (var item in utilizador)
                {
                    UserName = item.UserName;
                }

                var p = from d in DC.aspnet_UsersInRoles
                        where d.UserId == userid
                        select d;

                foreach (var item in p)
                {
                    Role = item.RoleId;
                }

                var tipoutilizador = from d in DC.aspnet_Roles
                                     where d.RoleId == Role
                                     select d;

                string regra = "";

                foreach (var item in tipoutilizador)
                {
                    regra = item.RoleName;
                }

                if (regra == "Lojista" || regra == "Cliente")
                    Response.Redirect("Default.aspx", false);

                if (regra == "Administrador" || regra == "SuperAdmin")
                {
                    try
                    {
                        var carregaGrid = from ors in DC.Orcamentos
                                          join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                          //join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                          where ors.ID_ESTADO == 3 || ors.ID_ESTADO == 4
                                          orderby ors.ID descending
                                          select new
                                          {
                                              ID = ors.ID,
                                              CODIGO = ors.CODIGO,
                                              CODIGOCLIENTE = parceiro.CODIGO,
                                              MARCA = ors.Equipamento_Avariado.Marca.DESCRICAO,
                                              MODELO = ors.Equipamento_Avariado.Modelo.DESCRICAO,
                                              IMEI = ors.Equipamento_Avariado.IMEI,
                                              DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                              ESTADO = ors.Orcamentos_Estado.DESCRICAO,
                                              NOMECLIENTE = parceiro.NOME.ToString()
                                          };

                        listagemOrcamentosRejeitadosCancelados.DataSourceID = "";
                        listagemOrcamentosRejeitadosCancelados.DataSource = carregaGrid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }
                else
                {
                    try
                    {
                        var carregaGrid = from ors in DC.Orcamentos
                                          join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                          //join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                          where ors.ID_ESTADO == 4 || ors.ID_ESTADO == 6
                                          orderby ors.ID descending
                                          select new
                                          {
                                              ID = ors.ID,
                                              CODIGO = ors.CODIGO,
                                              CODIGOCLIENTE = parceiro.CODIGO,
                                              MARCA = ors.Equipamento_Avariado.Marca.DESCRICAO,
                                              MODELO = ors.Equipamento_Avariado.Modelo.DESCRICAO,
                                              IMEI = ors.Equipamento_Avariado.IMEI,
                                              DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                              ESTADO = ors.Orcamentos_Estado.DESCRICAO,
                                              NOMECLIENTE = parceiro.NOME.ToString()
                                          };

                        listagemOrcamentosRejeitadosCancelados.DataSourceID = "";
                        listagemOrcamentosRejeitadosCancelados.DataSource = carregaGrid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }
            }
            else
                Response.Redirect("~/Default.aspx", true);
        }


    }
}
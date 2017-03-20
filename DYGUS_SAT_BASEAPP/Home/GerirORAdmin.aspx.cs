using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class GerirORAdmin : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid userid = new Guid();
            Guid Role = new Guid();
            string UserName = "";
            string EmailLogin = "";

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    userid = item.UserId;
                    EmailLogin = item.LoweredEmail;
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

                if (regra != "SuperAdmin")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);

        }

        protected void listagemORS_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
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


                    if (regra == "SuperAdmin")
                    {
                        if (e.CommandName == "EDITAR")
                        {

                            try
                            {
                                var item = (GridDataItem)e.Item;
                                var val1 = item["ID"].Text;
                                Response.Redirect("EditarOrdemReparacaoAdmin.aspx?ID=" + val1, false);
                            }

                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                        }

                    }
                }
                else
                    Response.Redirect("~/Default.aspx", true);

            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemORS_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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


                if (regra == "SuperAdmin")
                {
                    try
                    {
                        var carregaGrid = from ors in DC.Ordem_Reparacaos
                                          join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                          join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                          where ors.ID_ESTADO == 1 || ors.ID_ESTADO == 2 || ors.ID_ESTADO == 3 || ors.ID_ESTADO == 5
                                          orderby ors.ID descending
                                          select new
                                          {
                                              ID = ors.ID,
                                              CODIGO = ors.CODIGO,
                                              CODIGOCLIENTE = parceiro.CODIGO,
                                              MARCA = equip.Marca.DESCRICAO,
                                              MODELO = equip.Modelo.DESCRICAO,
                                              IMEI = equip.IMEI,
                                              DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                              DATA_PREVISTA_ENTREGA = ors.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString().ToString(),
                                              ESTADO = ors.Ordem_Reparacao_Estado.DESCRICAO,
                                              NOMECLIENTE = parceiro.NOME.ToString()
                                          };

                        listagemORS.DataSourceID = "";
                        listagemORS.DataSource = carregaGrid;
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }
                else
                    Response.Redirect("~/Default.aspx", true);
            }
            else
                Response.Redirect("~/Default.aspx", true);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListarUtilizador : Telerik.Web.UI.RadAjaxPage
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

                if (regra != "Administrador" && regra != "SuperAdmin")
                    Response.Redirect("Default.aspx", false);
                //(Page.Master.FindControl("utilizadores") as HtmlControl).Attributes.Add("class", "active");
                //(Page.Master.FindControl("utilizadoresedit") as HtmlControl).Attributes.Add("class", "active");
            }
        }

        protected void listagemutilizadoresregistados_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITARUSER"].Controls[0];
                hLink.NavigateUrl = "EditarUtilizador.aspx?id=" + val1;
            }
        }

        protected void listagemutilizadoresregistados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaUtilizadores = from utilizador in DC.Funcionarios
                                          where utilizador.ID_TIPO_FUNCIONARIO == 1
                                          join lojas in DC.Lojas on utilizador.ID_LOJA equals lojas.ID
                                          orderby utilizador.ID descending
                                          select new
                                          {
                                              ID = utilizador.ID,
                                              COD_UTILIZADOR = utilizador.CODIGO,
                                              NOME = utilizador.NOME,
                                              EMAIL = utilizador.EMAIL,
                                              LOJA = lojas.NOME,
                                              ESTADO = utilizador.ACTIVO.Value
                                          };

                listagemutilizadoresregistados.DataSourceID = "";
                listagemutilizadoresregistados.DataSource = carregaUtilizadores;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }
    }
}
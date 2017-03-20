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
    public partial class ListarReparador : Telerik.Web.UI.RadAjaxPage
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
            }
            //(Page.Master.FindControl("reparadores") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("reparadoresedit") as HtmlControl).Attributes.Add("class", "active");
        }

        protected void listagemreparadoresregistados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaClientes = from cliente in DC.Parceiros
                                      where cliente.ID_TIPO_PARCEIRO == 3 && cliente.ACTIVO == true
                                      select new
                                      {
                                          ID = cliente.ID,
                                          CODIGO = cliente.CODIGO,
                                          NOME = cliente.NOME,
                                          EMAIL = cliente.EMAIL,
                                          CONTACTO = cliente.TELEFONE,
                                          NIF = cliente.NIF,
                                          CONTA_ACTIVA = cliente.ACTIVO.Value
                                      };

                listagemreparadoresregistados.DataSourceID = "";
                listagemreparadoresregistados.DataSource = carregaClientes;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemreparadoresregistados_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITAR"].Controls[0];
                hLink.NavigateUrl = "EditarReparador.aspx?ID=" + val1;
            }
        }
    }
}
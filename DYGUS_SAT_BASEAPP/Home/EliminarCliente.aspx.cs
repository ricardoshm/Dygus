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
    public partial class EliminarCliente : Telerik.Web.UI.RadAjaxPage
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
            
        }

        protected void listagemclientesregistados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaClientes = from cliente in DC.Parceiros
                                      where (cliente.ID_TIPO_PARCEIRO == 1 || cliente.ID_TIPO_PARCEIRO == 2 || cliente.ID_TIPO_PARCEIRO == 4)
                                      select new
                                      {
                                          ID = cliente.ID,
                                          CODIGO = cliente.CODIGO,
                                          NOME = cliente.NOME,
                                          EMAIL = cliente.EMAIL,
                                          CONTACTO = cliente.TELEFONE,
                                          NIF = cliente.NIF,
                                          CONTA_ACTIVA = cliente.ACTIVO
                                      };

                listagemclientesregistados.DataSourceID = "";
                listagemclientesregistados.DataSource = carregaClientes;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void listagemclientesregistados_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    LINQ_DB.Parceiro CLIENTEELIMINA = new LINQ_DB.Parceiro();

                    var cliente = from c in DC.Parceiros
                                  where c.ID == Convert.ToInt32(id)
                                  select c;

                    foreach (var clienteeliminado in cliente)
                    {

                        LINQ_DB.aspnet_Membership MEMBER = new LINQ_DB.aspnet_Membership();

                        var membership = from m in DC.aspnet_Memberships
                                         where m.UserId == clienteeliminado.USERID.Value
                                         select m;

                        foreach (var membereliminado in membership)
                        {
                            membereliminado.IsApproved = false;
                            DC.SubmitChanges();
                        }

                        clienteeliminado.ACTIVO = false;
                        DC.SubmitChanges();
                    }

                    listagemclientesregistados.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
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
    public partial class EliminarTecnico : Telerik.Web.UI.RadAjaxPage
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

        protected void listagemtecnicosregistados_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    LINQ_DB.Funcionario TECNICOELIMINA = new LINQ_DB.Funcionario();

                    var tecnico = from t in DC.Funcionarios
                                  where t.ID == Convert.ToInt32(id)
                                  select t;

                    foreach (var clienteeliminado in tecnico)
                    {
                        clienteeliminado.ACTIVO = false;
                        DC.SubmitChanges();

                        LINQ_DB.aspnet_Membership MEMBER = new LINQ_DB.aspnet_Membership();

                        var membership = from m in DC.aspnet_Memberships
                                         where m.UserId == clienteeliminado.USERID
                                         select m;

                        foreach (var membereliminado in membership)
                        {
                            membereliminado.IsApproved = false;
                            DC.SubmitChanges();
                        }

                    }

                    listagemtecnicosregistados.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemtecnicosregistados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaTecnicos = from tecnico in DC.Funcionarios
                                      join loja in DC.Lojas on tecnico.ID_LOJA equals loja.ID
                                      where tecnico.ACTIVO == true
                                      select new
                                      {
                                          ID = tecnico.ID,
                                          COD_TECNICO = tecnico.CODIGO,
                                          NOME = tecnico.NOME,
                                          EMAIL = tecnico.EMAIL,
                                          LOJA = loja.NOME,
                                          ACTIVO = tecnico.ACTIVO.Value
                                      };

                listagemtecnicosregistados.DataSourceID = "";
                listagemtecnicosregistados.DataSource = carregaTecnicos;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
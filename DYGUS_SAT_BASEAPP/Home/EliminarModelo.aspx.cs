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
    public partial class EliminarModelo : Telerik.Web.UI.RadAjaxPage
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
            else
                Response.Redirect("~/Default.aspx", true);
           
        }

        protected void listagemmarcasregistadas_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    LINQ_DB.Modelo MODELOELIMINA = new LINQ_DB.Modelo();

                    var modelo = from c in DC.Modelos
                                 where c.ID == Convert.ToInt32(id)
                                 select c;

                    foreach (var modeloeliminada in modelo)
                    {
                        DC.Modelos.DeleteOnSubmit(modeloeliminada);
                        DC.SubmitChanges();
                    }

                    listagemmarcasregistadas.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemmarcasregistadas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregamarcasmodelos = from mm in DC.Modelos
                                           join marcas in DC.Marcas on mm.ID_MARCA equals marcas.ID
                                           select new
                                           {
                                               ID = mm.ID,
                                               MODELO = mm.DESCRICAO,
                                               MARCA = marcas.DESCRICAO
                                           };

                listagemmarcasregistadas.DataSourceID = "";
                listagemmarcasregistadas.DataSource = carregamarcasmodelos;

            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
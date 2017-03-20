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
    public partial class EliminarArmazem : Telerik.Web.UI.RadAjaxPage
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

        protected void listagemarmazens_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var armazem = from a in DC.Armazems
                              select new
                              {
                                  ID = a.ID,
                                  NOME = a.DESCRICAO,
                                  OBSERVACOES = a.OBSERVACOES
                              };

                listagemarmazens.DataSourceID = "";
                listagemarmazens.DataSource = armazem;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }

        protected void listagemarmazens_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    int artigos = 0;

                    var procuraArtigos = from art in DC.Artigos
                                         where art.ID_ARMAZEM == Convert.ToInt32(id)
                                         select art;

                    artigos = Enumerable.Count(procuraArtigos);

                    if (artigos > 0)
                        ShowAlertMessage("Este armazém ainda possui artigos registados! Deverá eliminar os mesmos em primeiro lugar.");

                    if (artigos == 0)
                    {

                        LINQ_DB.Armazem ARMAZEMELIMINA = new LINQ_DB.Armazem();

                        var armazem = from a in DC.Armazems
                                      where a.ID == Convert.ToInt32(id)
                                      select a;

                        foreach (var armazemeliminado in armazem)
                        {
                            armazemeliminado.ACTIVO = false;
                            DC.SubmitChanges();
                        }

                        listagemarmazens.Rebind();
                    }
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
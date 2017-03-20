using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class EditarEstados : Telerik.Web.UI.RadAjaxPage
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
            

            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarEstados.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
                carregaEstado();
        }

        protected void carregaEstado()
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarEstados.aspx", true);
                return;
            }

            var estado = from est in DC.Ordem_Reparacao_Estados
                         where est.ID == Convert.ToInt32(id)
                         select est;

            foreach (var item in estado)
            {
                tbestado.Text = item.DESCRICAO;
            }
        }

        protected void btnGrava_Click(object sender, EventArgs e)
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarEstados.aspx", true);
                return;
            }
            if (valida())
            {
                try
                {
                    var estado = from est in DC.Ordem_Reparacao_Estados
                                 where est.ID == Convert.ToInt32(id)
                                 select est;

                    LINQ_DB.Ordem_Reparacao_Estado ACTUALIZAESTADO = new LINQ_DB.Ordem_Reparacao_Estado();

                    ACTUALIZAESTADO = estado.First();

                    ACTUALIZAESTADO.DESCRICAO = tbestado.Text;
                    DC.SubmitChanges();
                    sucesso.Visible = sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Estado de Reparação actualizado com êxito";
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected bool valida()
        {
            string estado = "";
            estado = tbestado.Text;

            if (String.IsNullOrEmpty(estado))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Estado é de preenchimento obrigatório!";
                return false;
            }

            return true;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbestado.Text = "";
        }
    }
}
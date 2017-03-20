using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirEstados : Telerik.Web.UI.RadAjaxPage
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
            (Page.Master.FindControl("dados") as HtmlControl).Attributes.Add("class", "active");
            (Page.Master.FindControl("estadosinsert") as HtmlControl).Attributes.Add("class", "active");
        }

        protected void listagemestados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var pesquisa = from est in DC.Ordem_Reparacao_Estados
                               select est;

                listagemestados.DataSourceID = "";
                listagemestados.DataSource = pesquisa;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
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

        protected void btnGrava_Click(object sender, EventArgs e)
        {
            if (valida())
            {
                try
                {
                    LINQ_DB.Ordem_Reparacao_Estado NOVOESTADO = new LINQ_DB.Ordem_Reparacao_Estado();

                    NOVOESTADO.DESCRICAO = tbestado.Text;
                    DC.Ordem_Reparacao_Estados.InsertOnSubmit(NOVOESTADO);
                    DC.SubmitChanges();
                    sucesso.Visible = sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Estado de Reparação Inserido com êxito";
                    tbestado.Text = "";
                    tbestado.Focus();
                    listagemestados.Rebind();
                }
                catch (Exception ex)
                {
                   ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbestado.Text = "";
        }
    }
}
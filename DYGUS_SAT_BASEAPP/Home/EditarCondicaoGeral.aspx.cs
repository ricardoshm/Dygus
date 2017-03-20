using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class EditarCondicaoGeral : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
 Guid userid = new Guid();
            Guid Role = new Guid();
            string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           

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
                Response.Redirect("ListarCondicaoGeral.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
                carrega();
        }

        protected void carrega()
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarCondicaoGeral.aspx", true);
                return;
            }

            var procuraBD = from cond in DC.Condicoes_Gerais
                            where cond.ID == Convert.ToInt32(id)
                            select cond;

            foreach (var item in procuraBD)
            {
                tbcond.Text = item.DESCRICAO;
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarCondicaoGeral.aspx", true);
                return;
            }

            string novacondicao = "";
            novacondicao = tbcond.Text;

            try
            {
                var procuraBD = from cond in DC.Condicoes_Gerais
                                where cond.ID == Convert.ToInt32(id)
                                select cond;

                LINQ_DB.Condicoes_Gerai CONDICAO = new LINQ_DB.Condicoes_Gerai();
                CONDICAO = procuraBD.First();

                CONDICAO.DESCRICAO = tbcond.Text;
                DC.SubmitChanges();

                sucesso.Style.Add("display", "block");
                sucessoMessage.Style.Add("display", "block");
                sucessoMessage.InnerHtml = "Condição Geral actualizada com êxito";
                tbcond.Focus();
                SQLLog.registaLogBD(userid, DateTime.Now, "Editar cliente", "Foi editado a condição geral com o código: " + CONDICAO.ID.ToString() + ".", true);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbcond.Text = "";
        }
    }
}
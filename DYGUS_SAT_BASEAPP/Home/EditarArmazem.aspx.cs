using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class EditarArmazem : Telerik.Web.UI.RadAjaxPage
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
                Response.Redirect("ListarArmazem.aspx", true);
                return;
            }



            if (!Page.IsPostBack)
                carregaArmazem();

        }

        protected void carregaArmazem()
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarArmazem.aspx", true);
                return;
            }

            try
            {
                var armazem = from a in DC.Armazems
                              where a.ID == Convert.ToInt32(id)
                              select a;

                foreach (var item in armazem)
                {
                    tbnome.Text = item.DESCRICAO;
                    tbobs.Text = item.OBSERVACOES;
                }
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message); Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaCampos()
        {
            string nome = "";
            string obs = "";

            nome = tbnome.Text;
            obs = tbobs.Text;

            if (String.IsNullOrEmpty(nome))
            {
                erro.Visible = true;
                erro.InnerHtml = "O campo Nome do Armazém é obrigatório!";
                return false;
            }

            return true;
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
                Response.Redirect("ListarArmazem.aspx", true);
                return;
            }

            if (validaCampos())
            {
                try
                {
                    LINQ_DB.Armazem NOVOARMAZEM = new LINQ_DB.Armazem();

                    var armazem = from a in DC.Armazems
                                  where a.ID == Convert.ToInt32(id)
                                  select a;

                    NOVOARMAZEM = armazem.First();

                    NOVOARMAZEM.DESCRICAO = tbnome.Text;
                    NOVOARMAZEM.OBSERVACOES = tbobs.Text;

                    DC.SubmitChanges();
                    sucesso.Visible = sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Armazém actualizado com êxito!";
                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar armazém", "Foi editado o armazém com o nome: " + NOVOARMAZEM.DESCRICAO.ToString() + ".", true);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message); Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = tbobs.Text = "";
        }
    }
}
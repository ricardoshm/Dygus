using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class EditarTecnico : Telerik.Web.UI.RadAjaxPage
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
              

                string id = "";

                if (Request.QueryString["ID"] != null)
                {
                    id = Request.QueryString["ID"];
                }
                else
                {
                    Response.Redirect("ListarTecnico.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                    carregaTecnico();
            }
        }

        protected void carregaTecnico()
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarTecnico.aspx", true);
                return;
            }
            try
            {

                var consultatecnicos = from f in DC.Funcionarios
                                       join lojas in DC.Lojas on f.ID_LOJA equals lojas.ID
                                       where f.ID == Convert.ToInt32(id)
                                       select new
                                       {
                                           COD = f.CODIGO,
                                           LOJA = lojas.NOME,
                                           NOME = f.NOME,
                                           EMAIL = f.EMAIL,
                                           OBSERVACOES = f.OBSERVACOES
                                       };

                foreach (var item in consultatecnicos)
                {
                    tbcodtecnico.Text = item.COD;
                    tbloja.Text = item.LOJA.ToString();
                    tbnome.Text = item.NOME;
                    tbemail.Text = item.EMAIL;
                    tbobs.Text = item.OBSERVACOES;
                }
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaCampos()
        {
            string nome = "";

            nome = tbnome.Text;

            if (String.IsNullOrEmpty(nome))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Nome do Técnico é obrigatório!";
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
                Response.Redirect("ListarTecnico.aspx", true);
                return;
            }

            if (validaCampos())
            {
                try
                {
                    var consultatecnicos = from f in DC.Funcionarios
                                           where f.ID == Convert.ToInt32(id)
                                           select f;
                    LINQ_DB.Funcionario ACTUALIZATECNICO = new LINQ_DB.Funcionario();

                    ACTUALIZATECNICO = consultatecnicos.First();

                    ACTUALIZATECNICO.NOME = tbnome.Text;
                    ACTUALIZATECNICO.OBSERVACOES = tbobs.Text;

                    DC.SubmitChanges();
                    sucesso.Visible = true;
                    sucesso.InnerHtml = "Os dados relativos ao Técnico " + ACTUALIZATECNICO.CODIGO.ToString() + " foram actualizados com êxito!";

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = tbobs.Text = "";
            tbloja.Text = "";
        }
    }
}
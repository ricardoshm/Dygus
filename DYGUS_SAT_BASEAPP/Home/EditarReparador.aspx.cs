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
    public partial class EditarReparador : Telerik.Web.UI.RadAjaxPage
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
            

            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarReparador.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
                carregaReparador();
        }

        protected void carregaReparador()
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarReparador.aspx", true);
                return;
            }
            try
            {

                var consultatecnicos = from f in DC.Parceiros
                                       where f.ID == Convert.ToInt32(id)
                                       select f;

                foreach (var item in consultatecnicos)
                {
                    tbcodreparador.Text = item.CODIGO;
                    tbnome.Text = item.NOME;
                    tbmorada.Text = item.MORADA;
                    tbcodpostal.Text = item.CODPOSTAL;
                    tblocalidade.Text = item.LOCALIDADE;
                    tbcontacto.Text = item.TELEFONE;
                    tbemail.Text = item.EMAIL;
                    tbnif.Text = item.NIF.ToString();
                    tbobs.Text = item.OBSERVACOES;
                    ckbcontaactiva.SelectedToggleState.Selected = item.ACTIVO.Value;
                    ckbCliente.SelectedToggleState.Selected = item.CLIENTE.Value;
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
            string morada = "";
            string localidade = "";
            string contacto = "";

            string email = "";


            nome = tbnome.Text;
            morada = tbmorada.Text;
            localidade = tblocalidade.Text;
            contacto = tbcontacto.Text;

            email = tbemail.Text;




            if (String.IsNullOrEmpty(nome))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Nome de Reparador é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(morada))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Morada de Reparador é obrigatório!";
                return false;
            }


            if (String.IsNullOrEmpty(localidade))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Localidade de Reparador é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(contacto))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Contacto de Reparador é obrigatório!";
                return false;
            }



            if (String.IsNullOrEmpty(email))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Email de Reparador é obrigatório!";
                return false;
            }

            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (!rg.IsMatch(email))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O Email inserido é inválido!";
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
                Response.Redirect("ListarReparador.aspx", true);
                return;
            }

            if (validaCampos())
            {
                try
                {
                    var consultatecnicos = from f in DC.Parceiros
                                           where f.ID == Convert.ToInt32(id)
                                           select f;
                    LINQ_DB.Parceiro ACTUALIZAREPARADOR = new LINQ_DB.Parceiro();

                    ACTUALIZAREPARADOR = consultatecnicos.First();

                    ACTUALIZAREPARADOR.NOME = tbnome.Text;
                    ACTUALIZAREPARADOR.MORADA = tbmorada.Text;
                    ACTUALIZAREPARADOR.CODPOSTAL = tbcodpostal.Text;
                    ACTUALIZAREPARADOR.LOCALIDADE = tblocalidade.Text;
                    ACTUALIZAREPARADOR.TELEFONE = tbcontacto.Text;
                    ACTUALIZAREPARADOR.OBSERVACOES = tbobs.Text;
                    ACTUALIZAREPARADOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    ACTUALIZAREPARADOR.ACTIVO = ckbcontaactiva.SelectedToggleState.Selected;
                    ACTUALIZAREPARADOR.CLIENTE = ckbcontaactiva.SelectedToggleState.Selected;

                    DC.SubmitChanges();
                    sucesso.Visible = true;
                    sucesso.InnerHtml = "Os dados relativos ao Reparador Externo " + tbcodreparador.Text.ToString() + " foram actualizados com êxito!";

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
            tbnome.Text = tbmorada.Text = tbcodpostal.Text = tblocalidade.Text = tbcontacto.Text = tbobs.Text = "";
        }
    }
}
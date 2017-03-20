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
    public partial class EditarCliente : Telerik.Web.UI.RadAjaxPage
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
            

            int id = 0;

            if (Request.QueryString["ID"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
            {
                Response.Redirect("ListarCliente.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
                carregaCliente();
        }

        protected void carregaCliente()
        {
            try
            {
                int id = 0;

                if (Request.QueryString["ID"] != null)
                {
                    id = Convert.ToInt32(Request.QueryString["ID"]);
                }
                else
                {
                    Response.Redirect("ListarCliente.aspx", true);
                    return;
                }

                var consultaclientes = from f in DC.Parceiros
                                       join tipos in DC.Parceiro_Tipos on f.ID_TIPO_PARCEIRO equals tipos.ID
                                       where f.ID == id
                                       select new
                                       {
                                           COD = f.CODIGO,
                                           NOME = f.NOME,
                                           MORADA = f.MORADA,
                                           CODPOSTAL = f.CODPOSTAL,
                                           LOCALIDADE = f.LOCALIDADE,
                                           CONTACT = f.TELEFONE,
                                           EMAIL = f.EMAIL,
                                           NIF = f.NIF,
                                           OBSERVACOES = f.OBSERVACOES,
                                           CONTAACTIVA = f.ACTIVO.Value,
                                           TIPOCLIENTE = tipos.DESCRICAO
                                       };

                foreach (var item in consultaclientes)
                {
                    tbcodcliente.Text = item.COD;
                    tbnome.Text = item.NOME;
                    tbmorada.Text = item.MORADA;
                    tbcodpostal.Text = item.CODPOSTAL;
                    tblocalidade.Text = item.LOCALIDADE;
                    tbcontacto.Text = item.CONTACT;
                    tbemail.Text = item.EMAIL;
                    tbnif.Text = item.NIF.ToString();
                    tbobs.Text = item.OBSERVACOES;
                    ckbContaEstado.SelectedToggleState.Selected = item.CONTAACTIVA;
                    tbtipocliente.Text = item.TIPOCLIENTE;
                }
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = "";
            tbmorada.Text = "";
            tbcodpostal.Text = "";
            tblocalidade.Text = "";
            tbcontacto.Text = "";
            tbnif.Text = "";
            tbobs.Text = "";
            ckbContaEstado.ClearSelection();
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
                errorMessage.InnerHtml = "O campo Nome de Cliente é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(morada))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Morada de Cliente é obrigatório!";
                return false;
            }



            if (String.IsNullOrEmpty(localidade))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Localidade de Cliente é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(contacto))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Contacto de Cliente é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(email))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Email de Cliente é obrigatório!";
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

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            int id = 0;

            if (Request.QueryString["ID"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["ID"]);
            }
            else
            {
                Response.Redirect("ListarCliente.aspx", true);
                return;
            }


            if (validaCampos())
            {
                try
                {
                    var meuCliente = from m in DC.Parceiros
                                     where m.ID == id
                                     select m;

                    LINQ_DB.Parceiro cli = new LINQ_DB.Parceiro();
                    cli = meuCliente.First();

                    cli.NOME = tbnome.Text;
                    cli.MORADA = tbmorada.Text;
                    if (String.IsNullOrEmpty(tbcodpostal.Text))
                        cli.CODPOSTAL = "0000000";
                    else
                        cli.CODPOSTAL = tbcodpostal.Text;
                    cli.LOCALIDADE = tblocalidade.Text;
                    cli.TELEFONE = tbcontacto.Text;
                    if (!String.IsNullOrEmpty(tbnif.Text))
                        cli.NIF = Convert.ToInt32(tbnif.Text);
                    else
                        cli.NIF = 999999999;
                    if (String.IsNullOrEmpty(tbobs.Text))
                        cli.OBSERVACOES = "N/D";
                    else
                        cli.OBSERVACOES = tbobs.Text;
                    cli.ACTIVO = ckbContaEstado.SelectedToggleState.Selected;

                    DC.SubmitChanges();
                    sucesso.Visible = true;
                    sucesso.InnerHtml = "Os dados relativos ao Cliente " + cli.CODIGO.ToString() + " foram actualizados com êxito!";
                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar cliente", "Foi editado o cliente com o código: " + cli.CODIGO.ToString() + ".", true);
                }

                catch (Exception ex)
                {

                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }


    }
}
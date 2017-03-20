using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListagemGeralClientesDetalhe : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            //(Page.Master.FindControl("relatorios") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("relatoriosclientes") as HtmlControl).Attributes.Add("class", "active");

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
                Response.Redirect("ListagemGeralClientes.aspx", true);
                return;
            }

            try
            {
                var carrega = from client in DC.Parceiros
                              where client.ID == Convert.ToInt32(id)
                              select client;

                foreach (var item in carrega)
                {
                    tbcodcliente.Text = item.CODIGO;
                    tbtipoCliente.Text = item.Parceiro_Tipo.DESCRICAO;
                    tbnome.Text = item.NOME;
                    tbmorada.Text = item.MORADA;
                    tbcodpostal.Text = item.CODPOSTAL;
                    tblocalidade.Text = item.LOCALIDADE;
                    tbcontacto.Text = item.TELEFONE;
                    tbemail.Text = item.EMAIL;
                    tbnif.Text = item.NIF.ToString();
                    tbobs.Text = item.OBSERVACOES;
                    tbdataultima.Text = item.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListagemGeralClientes.aspx", true);
        }
    }
}
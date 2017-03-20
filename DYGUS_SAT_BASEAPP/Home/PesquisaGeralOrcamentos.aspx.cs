using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class PesquisaGeralOrcamentos : Telerik.Web.UI.RadAjaxPage
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
            //(Page.Master.FindControl("relatorios") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("relatoriosors") as HtmlControl).Attributes.Add("class", "active");

            if (!Page.IsPostBack)
                carregaLojas();
        }

        protected bool validaCampos()
        {
            if (datainicio.SelectedDate == null && datainicio.SelectedDate.GetValueOrDefault() == DateTime.MinValue)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Data de Inicio é de preenchimento obrigatório!";
                return false;
            }

            if (datafim.SelectedDate == null && datainicio.SelectedDate.GetValueOrDefault() == DateTime.MinValue)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Data de Fim é de preenchimento obrigatório!";
                return false;
            }

            return true;
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                try
                {
                    var carregaOrs = from ordem in DC.Orcamentos
                                     join parceiro in DC.Parceiros on ordem.USERID equals parceiro.USERID
                                     where (ordem.ID_ESTADO == 3 || ordem.ID_ESTADO == 4 || ordem.ID_ESTADO == 5 || ordem.ID_ESTADO == 6) && (ordem.DATA_REGISTO.Value >= datainicio.SelectedDate && ordem.DATA_REGISTO.Value < datafim.SelectedDate.Value.AddDays(1))
                                     orderby ordem.ID descending
                                     select new
                                     {
                                         ID = ordem.ID,
                                         CODIGO = ordem.CODIGO,
                                         DATA_REGISTO = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                         CODIGOCLIENTE = parceiro.CODIGO,
                                         ID_ESTADO_OR = ordem.ID_ESTADO.Value,
                                         ESTADO_OR = ordem.Orcamentos_Estado.DESCRICAO
                                     };

                    listagemgeralors.DataSourceID = "";
                    listagemgeralors.DataSource = carregaOrs;
                    listagemgeralors.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void carregaLojas()
        {
            try
            {
                var lojas = from c in DC.Lojas
                            select c;

                ddlloja.DataTextField = "NOME";
                ddlloja.DataValueField = "ID";
                ddlloja.DataSource = lojas;
                ddlloja.DataBind();
                ddlloja.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            datainicio.Clear();
            datafim.Clear();
        }

        protected bool validaCamposLoja()
        {
            if (ddlloja.SelectedItem.Text == "Por favor seleccione...")
            {
                erroLoja.Visible = erromessageLoja.Visible = true;
                erromessageLoja.InnerHtml = "O campo Loja é de preenchimento obrigatório!";
                return false;
            }
            return true;
        }

        protected void ddlloja_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (validaCamposLoja())
            {
                try
                {
                    var carregaOrs = from ordem in DC.Orcamentos
                                     join parceiro in DC.Parceiros on ordem.USERID equals parceiro.USERID
                                     where (ordem.ID_ESTADO == 3 || ordem.ID_ESTADO == 4 || ordem.ID_ESTADO == 5 || ordem.ID_ESTADO == 6) && ordem.ID_LOJA.Value == Convert.ToInt32(ddlloja.SelectedItem.Value)
                                     orderby ordem.ID descending
                                     select new
                                     {
                                         ID = ordem.ID,
                                         CODIGO = ordem.CODIGO,
                                         DATA_REGISTO = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                         CODIGOCLIENTE = parceiro.CODIGO,
                                         ID_ESTADO_OR = ordem.ID_ESTADO.Value,
                                         ESTADO_OR = ordem.Orcamentos_Estado.DESCRICAO
                                     };

                    listagemORsLoja.DataSourceID = "";
                    listagemORsLoja.DataSource = carregaOrs;
                    listagemORsLoja.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }


    }
}
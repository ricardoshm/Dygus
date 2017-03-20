using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirCondicaoGeral : Telerik.Web.UI.RadAjaxPage
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
            //(Page.Master.FindControl("dadoscond") as HtmlControl).Attributes.Add("class", "has-sub active");
            //(Page.Master.FindControl("condinsert") as HtmlControl).Attributes.Add("class", "active");
        }

        protected bool validaCampos()
        {
            if (String.IsNullOrEmpty(tbcond.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo condição geral é de preechimento obrigatório!";
                tbcond.Focus();
                return false;
            }
            return true;
        }
        
        protected void btnGravarMarca_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                try
                {
                    LINQ_DB.Condicoes_Gerai CONDICAO = new LINQ_DB.Condicoes_Gerai();

                    CONDICAO.DESCRICAO = tbcond.Text;

                    DC.Condicoes_Gerais.InsertOnSubmit(CONDICAO);
                    DC.SubmitChanges();

                    sucesso.Visible = sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Condição Geral inserida com êxito";
                    listagemcond.Rebind();
                    tbcond.Text = "";
                    tbcond.Focus();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }

        }

        protected void btnLimparMarca_Click(object sender, EventArgs e)
        {
            tbcond.Text = "";
        }

        protected void listagemcond_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregamarcas = from mm in DC.Condicoes_Gerais
                                    select new
                                    {
                                        ID = mm.ID,
                                        DESCRICAO = mm.DESCRICAO,
                                    };

                listagemcond.DataSourceID = "";
                listagemcond.DataSource = carregamarcas;

            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
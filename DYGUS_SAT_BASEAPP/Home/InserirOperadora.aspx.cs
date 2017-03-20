using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirOperadora : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Reparador"||regra=="Cliente")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);
            (Page.Master.FindControl("dados") as HtmlControl).Attributes.Add("class", "active");
            (Page.Master.FindControl("opinsert") as HtmlControl).Attributes.Add("class", "active");
        }

        protected void listagemOperadoras_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaoperadoras = from mm in DC.Operadoras
                                        orderby mm.DESCRICAO ascending
                                        select new
                                        {
                                            ID = mm.ID,
                                            DESCRICAO = mm.DESCRICAO
                                        };

                listagemOperadoras.DataSourceID = "";
                listagemOperadoras.DataSource = carregaoperadoras;

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tboperadora.Text))
            {
                try
                {
                    LINQ_DB.Operadora OPERA = new LINQ_DB.Operadora();

                    OPERA.DESCRICAO = tboperadora.Text;

                    DC.Operadoras.InsertOnSubmit(OPERA);
                    DC.SubmitChanges();

                    sucesso.Visible = sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Operadora inserida com êxito";
                    listagemOperadoras.Rebind();
                    tboperadora.Text = "";
                    tboperadora.Focus();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            else
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Operadora é de preenchimento obrigatório!";
                return;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tboperadora.Text = "";
        }
    }
}
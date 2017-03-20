using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirArmazem : Telerik.Web.UI.RadAjaxPage
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
            //(Page.Master.FindControl("stocks") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("stocksarmazeminsert") as HtmlControl).Attributes.Add("class", "active");
        }

        protected void listagemarmazens_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var armazem = from a in DC.Armazems
                              select new
                              {
                                  ID = a.ID,
                                  NOME = a.DESCRICAO,
                                  OBSERVACOES = a.OBSERVACOES
                              };

                listagemarmazens.DataSourceID = "";
                listagemarmazens.DataSource = armazem;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaCampos()
        {
            string nome = "";
            nome = tbnome.Text;

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
            if (validaCampos())
            {
                try
                {
                    LINQ_DB.Armazem NOVOARMAZEM = new LINQ_DB.Armazem();
                    NOVOARMAZEM.DESCRICAO = tbnome.Text;
                    if (!String.IsNullOrEmpty(tbobs.Text))
                        NOVOARMAZEM.OBSERVACOES = tbobs.Text;
                    else
                        NOVOARMAZEM.OBSERVACOES = "N/D";
                    NOVOARMAZEM.ACTIVO = true;
                    DC.Armazems.InsertOnSubmit(NOVOARMAZEM);
                    DC.SubmitChanges();
                    listagemarmazens.Rebind();
                    tbnome.Text = tbobs.Text = "";

                    sucesso.Visible = true;
                    sucesso.InnerHtml = "Armazém registado com êxito!";
                }
                catch (Exception ex)
                {

                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = tbobs.Text = "";
        }
    }
}
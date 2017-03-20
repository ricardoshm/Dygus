using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirMarca : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Reparador" || regra == "Cliente")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);
            (Page.Master.FindControl("dados") as HtmlControl).Attributes.Add("class", "active");
            (Page.Master.FindControl("marcasinsert") as HtmlControl).Attributes.Add("class", "active");
        }

        protected void listagemmarcas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregamarcas = from mm in DC.Marcas
                                    select new
                                    {
                                        ID = mm.ID,
                                        MARCA = mm.DESCRICAO,
                                    };

                listagemmarcas.DataSourceID = "";
                listagemmarcas.DataSource = carregamarcas;

            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnGravarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                LINQ_DB.Marca MARCA = new LINQ_DB.Marca();

                MARCA.DESCRICAO = tbmarca.Text;

                DC.Marcas.InsertOnSubmit(MARCA);
                DC.SubmitChanges();

                sucesso.Style.Add("display", "block");
                sucessoMessage.Style.Add("display", "block");
                sucessoMessage.InnerHtml = "Marca inserida com êxito";
                listagemmarcas.Rebind();
                tbmarca.Text = "";
                tbmarca.Focus();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void btnLimparMarca_Click(object sender, EventArgs e)
        {
            tbmarca.Text = "";
        }
    }
}
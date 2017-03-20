using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListagemArtigos : Telerik.Web.UI.RadAjaxPage
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
        }

        protected void listagemArtigos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var consulta = from artigo in DC.Artigos
                               select new
                               {
                                   ID = artigo.ID,
                                   CODIGO = artigo.CODIGO,
                                   DESCRICAO = artigo.DESCRICAO
                               };


                listagemArtigos.DataSourceID = "";
                listagemArtigos.DataSource = consulta;

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected List<string> returnedValuesArtigos = new List<string>();

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (Session["returnedValuesArtigos"] != null)
            {
                returnedValuesArtigos = (List<string>)Session["returnedValuesArtigos"];
            }
            GridDataItem dataItem = (GridDataItem)(sender as CheckBox).NamingContainer;

            string cod = dataItem["CODIGO"].Text;
            string descricao = dataItem["DESCRICAO"].Text;


            if (checkBox.Checked)
            {
                returnedValuesArtigos.Add(cod);
                returnedValuesArtigos.Add(descricao);
            }
            else
            {
                returnedValuesArtigos.Remove(cod);
                returnedValuesArtigos.Remove(descricao);
            }
            Session["returnedValuesArtigos"] = returnedValuesArtigos;
        }
    }
}
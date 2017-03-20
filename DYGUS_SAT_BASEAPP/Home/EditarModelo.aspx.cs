using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class EditarModelo : Telerik.Web.UI.RadAjaxPage
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
            

            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarModelos.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
                carregaModelo();
        }

        protected void carregaModelo()
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarModelos.aspx", true);
                return;
            }

            var modelos = from modelo in DC.Modelos
                          join marcas in DC.Marcas on modelo.ID_MARCA equals marcas.ID
                          where modelo.ID == Convert.ToInt32(id)
                          select new
                          {
                              MODELO = modelo.DESCRICAO,
                              MARCA = marcas.DESCRICAO,
                              MARCAID = marcas.ID
                          };

            foreach (var item in modelos)
            {
                tbmarca.Text = item.MARCA;
                tbmodelo.Text = item.MODELO;
                idMarca.Value = item.MARCAID.ToString();
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarModelos.aspx", true);
                return;
            }
            try
            {



                var modelos = from modelo in DC.Modelos
                              where modelo.ID == Convert.ToInt32(id)
                              select modelo;

                LINQ_DB.Modelo ACTUALIZAMODELO = new LINQ_DB.Modelo();

                ACTUALIZAMODELO = modelos.First();
                ACTUALIZAMODELO.ID_MARCA = Convert.ToInt32(idMarca.Value);
                ACTUALIZAMODELO.DESCRICAO = tbmodelo.Text;
                DC.SubmitChanges();

                sucesso.Style.Add("display", "block");
                sucessoMessage.Style.Add("display", "block");
                sucessoMessage.InnerHtml = "Modelo actualizado com êxito";

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbmodelo.Text = "";
            tbmodelo.Focus();
        }
    }
}
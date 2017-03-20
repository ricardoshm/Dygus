using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirModelo : Telerik.Web.UI.RadAjaxPage
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
            (Page.Master.FindControl("modelosinsert") as HtmlControl).Attributes.Add("class", "active");

            if (!Page.IsPostBack)
                carregaMarcas();

            tbmodelo.ReadOnly = true;
        }

        protected void carregaMarcas()
        {
            try
            {
                var marcas = from c in DC.Marcas
                             select c;

                ddlMarca.DataTextField = "DESCRICAO";
                ddlMarca.DataValueField = "ID";
                ddlMarca.DataSource = marcas;
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (ddlMarca.SelectedItem.Value != "Por favor seleccione...")
                tbmodelo.ReadOnly = false;
            else
                tbmodelo.ReadOnly = true;
        }

        protected void listagemmodelosregistadas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregamodelos = from mm in DC.Modelos
                                     join marcas in DC.Marcas on mm.ID_MARCA equals marcas.ID
                                     select new
                                     {
                                         ID = mm.ID,
                                         MODELO = mm.DESCRICAO,
                                         MARCA = marcas.DESCRICAO
                                     };

                listagemmodelosregistadas.DataSourceID = "";
                listagemmodelosregistadas.DataSource = carregamodelos;

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }


        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbmodelo.Text = "";
            ddlMarca.ClearSelection();
            tbmodelo.ReadOnly = true;
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                LINQ_DB.Modelo MODELO = new LINQ_DB.Modelo();

                MODELO.DESCRICAO = tbmodelo.Text;
                MODELO.ID_MARCA = Convert.ToInt32(ddlMarca.SelectedItem.Value);

                DC.Modelos.InsertOnSubmit(MODELO);
                DC.SubmitChanges();

                sucesso.Style.Add("display", "block");
                sucessoMessage.Style.Add("display", "block");
                sucessoMessage.InnerHtml = "Modelo inserido com êxito";
                listagemmodelosregistadas.Rebind();
                tbmodelo.Text = "";
                ddlMarca.ClearSelection();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
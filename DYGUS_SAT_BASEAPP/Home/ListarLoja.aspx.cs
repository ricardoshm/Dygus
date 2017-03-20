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
    public partial class ListarLoja : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            //(Page.Master.FindControl("config") as HtmlControl).Attributes.Add("class", "has-sub active");
            //(Page.Master.FindControl("configedit") as HtmlControl).Attributes.Add("class", "active");

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


        }



        protected void listagemlojasregistadas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaLojas = from loja in DC.Lojas
                                   orderby loja.ID ascending
                                   select new
                                   {
                                       ID = loja.ID,
                                       CODIGO = loja.CODIGO,
                                       NOME = loja.NOME,
                                       LOCALIDADE = loja.LOCALIDADE,
                                       CONTACTO_TEL = loja.TELEFONE,
                                       NIF = loja.NIF,
                                       ESTADO=loja.ACTIVO.Value
                                   };

                listagemlojasregistadas.DataSourceID = "";
                listagemlojasregistadas.DataSource = carregaLojas;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemlojasregistadas_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITAR"].Controls[0];
                hLink.NavigateUrl = "EditarLoja.aspx?id=" + val1;

                if (item["ESTADO"].Text == "True")
                    item["ESTADO"].Text = "Sim";
                if (item["ESTADO"].Text == "False")
                    item["ESTADO"].Text = "Não";

                

            }
        }


    }
}
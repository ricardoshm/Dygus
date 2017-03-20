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
    public partial class ListagemORFechadas : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            //(Page.Master.FindControl("or") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("orlistfechadas") as HtmlControl).Attributes.Add("class", "active");
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

        protected void listagemgeralors_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                 join parceiro in DC.Parceiros on ordem.USERID equals parceiro.USERID
                                 where ordem.ID_ESTADO == 6 || ordem.ID_ESTADO == 7
                                 orderby ordem.ID descending
                                 select new
                                 {
                                     ID = ordem.ID,
                                     CODIGO = ordem.CODIGO,
                                     DATA_REGISTO = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                     CODIGOCLIENTE = parceiro.CODIGO,
                                     ID_ESTADO_OR = ordem.ID_ESTADO.Value,
                                     ESTADO_OR = ordem.Ordem_Reparacao_Estado.DESCRICAO
                                 };

                listagemgeralors.DataSourceID = "";
                listagemgeralors.DataSource = carregaOrs;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemgeralors_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.Target = "_blank";
                hLink.NavigateUrl = "ImprimeORFinalCliente.aspx?ID=" + val1;
            }
        }

    }
}
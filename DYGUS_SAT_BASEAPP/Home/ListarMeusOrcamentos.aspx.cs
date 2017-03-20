using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListarMeusOrcamentos : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Cliente") { }
                else
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);

        }

        protected void listagemOrcamentos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                Guid val2 = new Guid(item["ID"].Text);
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "GerirMeuORC.aspx?ID=" + val1 + "&userid=" + val2;
            }
        }

        protected void listagemOrcamentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Guid userid = new Guid();

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    userid = item.UserId;
                }

                try
                {
                    var carregaGrid = from ors in DC.Orcamentos
                                      join parceiro in DC.Parceiros on ors.USERID equals parceiro.USERID
                                      join equip in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                      where ors.USERID.Value == userid
                                      orderby ors.ID descending
                                      select new
                                      {
                                          ID = ors.ID,
                                          CODIGO = ors.CODIGO,
                                          CODIGOCLIENTE = parceiro.CODIGO,
                                          MARCA = equip.Marca.DESCRICAO,
                                          MODELO = equip.Modelo.DESCRICAO,
                                          IMEI = equip.IMEI,
                                          DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                          ESTADO = ors.Orcamentos_Estado.DESCRICAO
                                      };

                    listagemOrcamentos.DataSourceID = "";
                    listagemOrcamentos.DataSource = carregaGrid;
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
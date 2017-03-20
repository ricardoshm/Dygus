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
    public partial class ListarEquipamentoSubstituicao : Telerik.Web.UI.RadAjaxPage
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

            //(Page.Master.FindControl("dados") as HtmlControl).Attributes.Add("class", "has-sub active");
            //(Page.Master.FindControl("eqsedit") as HtmlControl).Attributes.Add("class", "active");

        }

        protected void listagemequipamentos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var pesquisa = from equip in DC.Equipamentos_Substituicaos
                               join marcas in DC.Marcas on equip.ID_MARCA equals marcas.ID
                               join modelos in DC.Modelos on equip.ID_MODELO equals modelos.ID
                               join equipDisp in DC.Equipamentos_Substituicao_Disponibilidades on equip.ID_DISPONIBILIDADE equals equipDisp.ID
                               orderby equip.ID ascending
                               where equip.ACTIVO == true
                               select new
                               {
                                   ID = equip.ID,
                                   CODIGO = equip.CODIGO,
                                   MARCA = marcas.DESCRICAO,
                                   MODELO = modelos.DESCRICAO,
                                   ESTADO = equipDisp.DESCRICAO
                               };

                listagemequipamentos.DataSourceID = "";
                listagemequipamentos.DataSource = pesquisa;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemequipamentos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITAR"].Controls[0];
                hLink.NavigateUrl = "EditarEquipamentoSubstituicao.aspx?ID=" + val1;
            }
        }
    }
}
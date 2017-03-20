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
    public partial class EliminarEquipamentoSubstituicao : Telerik.Web.UI.RadAjaxPage
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
            
        }

        protected void listagemequipamentos_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    LINQ_DB.Equipamentos_Substituicao EQS = new LINQ_DB.Equipamentos_Substituicao();
                    var equipamento = from ee in DC.Equipamentos_Substituicaos
                                      where ee.ID == Convert.ToInt32(id)
                                      select ee;

                    foreach (var equipeliminada in equipamento)
                    {
                        equipeliminada.ACTIVO = false;
                        DC.SubmitChanges();
                    }

                    listagemequipamentos.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
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
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
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
    public partial class ListarTecnicoOrdemReparacao : Telerik.Web.UI.RadAjaxPage
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

        protected void listagemordensreparacaoregistadas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaGrid = from ors in DC.Ordem_Reparacaos
                                  join equipAvariado in DC.Equipamento_Avariados on ors.ID_EQUIPAMENTO_AVARIADO equals equipAvariado.ID
                                  join marcas in DC.Marcas on equipAvariado.ID_MARCA equals marcas.ID
                                  join modelos in DC.Modelos on equipAvariado.ID_MODELO equals modelos.ID
                                  join cli in DC.Parceiros on ors.USERID equals cli.USERID
                                  where ors.ATRIBUIDA == false && ors.ID_ESTADO == 1
                                  orderby ors.ID descending
                                  select new
                                  {
                                      ID = ors.ID,
                                      CODIGO = ors.CODIGO,
                                      CODIGOCLIENTE = cli.CODIGO,
                                      MARCA = marcas.DESCRICAO,
                                      MODELO = modelos.DESCRICAO,
                                      IMEI = equipAvariado.IMEI,
                                      DATA_REGISTO_OR = ors.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                      DATA_PREVISTA_ENTREGA = ors.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString().ToString()
                                  };

                listagemordensreparacaoregistadas.DataSourceID = "";
                listagemordensreparacaoregistadas.DataSource = carregaGrid;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemordensreparacaoregistadas_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITAR"].Controls[0];
                hLink.NavigateUrl = "GerirOrdemReparacao.aspx?ID=" + val1;
            }

        }
    }
}
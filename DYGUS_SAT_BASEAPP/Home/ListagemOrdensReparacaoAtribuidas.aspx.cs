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
    public partial class ListagemOrdensReparacaoAtribuidas : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            (Page.Master.FindControl("or") as HtmlControl).Attributes.Add("class", "active");
            (Page.Master.FindControl("orlistatrib") as HtmlControl).Attributes.Add("class", "active");
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

        protected void listagemordensreparacaoatribuidasreparadores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {

                var carregaGrid = from ors in DC.Ordem_Reparacao_Atribuicaos
                                  join fun in DC.Parceiros on ors.USERID equals fun.USERID
                                  join ordem in DC.Ordem_Reparacaos on ors.ID_ORDEM_REPARACAO equals ordem.ID
                                  join equip in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                  join cliente in DC.Parceiros on ors.USERID equals cliente.USERID
                                  where ordem.ATRIBUIDA == true
                                  orderby ors.ID descending
                                  select new
                                  {
                                      ID = ors.Ordem_Reparacao.ID,
                                      CODIGO = ors.Ordem_Reparacao.CODIGO,
                                      CODIGOCLIENTE = cliente.CODIGO,
                                      MARCA = equip.Marca.DESCRICAO,
                                      MODELO = equip.Modelo.DESCRICAO,
                                      IMEI = equip.IMEI,
                                      DATA_REGISTO_OR = ors.Ordem_Reparacao.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                      DATA_PREVISTA_ENTREGA = ors.Ordem_Reparacao.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString().ToString(),
                                      ESTADO = ors.Ordem_Reparacao.Ordem_Reparacao_Estado.DESCRICAO,
                                      NOME = fun.NOME
                                  };

                listagemordensreparacaoatribuidasreparadores.DataSourceID = "";
                listagemordensreparacaoatribuidasreparadores.DataSource = carregaGrid;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemordensreparacaoatribuidastecnicos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaGrid = from ors in DC.Ordem_Reparacao_Atribuicaos
                                  join fun in DC.Funcionarios on ors.USERID equals fun.USERID
                                  join ordem in DC.Ordem_Reparacaos on ors.ID_ORDEM_REPARACAO equals ordem.ID
                                  join equip in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                  join cliente in DC.Funcionarios on ors.USERID equals cliente.USERID
                                  where ordem.ATRIBUIDA == true
                                  orderby ors.ID descending
                                  select new
                                  {
                                      ID = ors.Ordem_Reparacao.ID,
                                      CODIGO = ors.Ordem_Reparacao.CODIGO,
                                      CODIGOCLIENTE = cliente.CODIGO,
                                      MARCA = equip.Marca.DESCRICAO,
                                      MODELO = equip.Modelo.DESCRICAO,
                                      IMEI = equip.IMEI,
                                      DATA_REGISTO_OR = ors.Ordem_Reparacao.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                      DATA_PREVISTA_ENTREGA = ors.Ordem_Reparacao.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString().ToString(),
                                      ESTADO = ors.Ordem_Reparacao.Ordem_Reparacao_Estado.DESCRICAO,
                                      NOME = fun.NOME
                                  };


                listagemordensreparacaoatribuidastecnicos.DataSourceID = "";
                listagemordensreparacaoatribuidastecnicos.DataSource = carregaGrid;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

    }
}
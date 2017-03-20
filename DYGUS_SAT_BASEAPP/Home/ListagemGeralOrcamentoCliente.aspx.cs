using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListagemGeralOrcamentoCliente : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Cliente")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);
        }

        protected void listagemgeralors_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                Guid userid = new Guid();
                Guid Role = new Guid();
                string UserName = "";
                int idLoja = 0;

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

                    if (regra == "Administrador" || regra == "SuperAdmin")
                    {

                        var carregaOrs = from ordem in DC.Orcamentos
                                         join parceiros in DC.Parceiros on ordem.USERID.Value equals parceiros.USERID.Value
                                         join equip in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                         where ordem.ID_ESTADO == 1 || ordem.ID_ESTADO == 2
                                         orderby ordem.ID descending
                                         select new
                                         {
                                             LOJA = ordem.Loja.NOME,
                                             ESTADO_OR = ordem.Orcamentos_Estado.DESCRICAO,
                                             NOMECLIENTE = parceiros.NOME,
                                             Imei = ordem.Equipamento_Avariado.IMEI,
                                             MARCA = equip.Marca.DESCRICAO,
                                             MODELO = equip.Modelo.DESCRICAO,
                                             CODIGO = ordem.CODIGO,
                                             DATA_REGISTO = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                             ID_ESTADO_OR = ordem.ID_ESTADO.Value,
                                             ID = ordem.ID
                                         };

                        listagemgeralors.DataSourceID = "";
                        listagemgeralors.DataSource = carregaOrs;
                    }

                    if (regra == "Lojista" || regra == "Tecnico")
                    {

                        var verificaLoja = from lojas in DC.Lojas
                                           join funcionarios in DC.Funcionarios on lojas.ID equals funcionarios.ID_LOJA
                                           where funcionarios.USERID == userid
                                           select lojas;

                        foreach (var item in verificaLoja)
                        {
                            idLoja = item.ID;
                        }

                        var carregaOrs = from ordem in DC.Orcamentos
                                         join parceiro in DC.Parceiros on ordem.USERID equals parceiro.USERID
                                         join equip in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                         where ordem.ID_LOJA == idLoja && (ordem.ID_ESTADO == 1 || ordem.ID_ESTADO == 2)
                                         orderby ordem.ID descending
                                         select new
                                         {
                                             LOJA = ordem.Loja.NOME,
                                             ESTADO_OR = ordem.Orcamentos_Estado.DESCRICAO,
                                             NOMECLIENTE = parceiro.NOME,
                                             Imei = ordem.Equipamento_Avariado.IMEI,
                                             MARCA = equip.Marca.DESCRICAO,
                                             MODELO = equip.Modelo.DESCRICAO,
                                             CODIGO = ordem.CODIGO,
                                             DATA_REGISTO = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                             ID_ESTADO_OR = ordem.ID_ESTADO.Value,
                                             ID = ordem.ID
                                         };

                        listagemgeralors.DataSourceID = "";
                        listagemgeralors.DataSource = carregaOrs;
                    }
                }
                else
                    Response.Redirect("~/Default.aspx", true);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
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
                hLink.NavigateUrl = "ImprimeORCCliente.aspx?ID=" + val1;
            }


        }

    }
}
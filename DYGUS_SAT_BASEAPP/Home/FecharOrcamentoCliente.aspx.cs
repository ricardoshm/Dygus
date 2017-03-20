using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class FecharOrcamentoCliente : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Administrador" || regra == "SuperAdmin") { }
                else
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
                                         where (ordem.ID_ESTADO == 1 || ordem.ID_ESTADO == 2 || ordem.ID_ESTADO == 3 || ordem.ID_ESTADO == 4 || ordem.ID_ESTADO == 5) && ordem.CONVERTIDO_OR == false
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
                                         where ordem.ID_LOJA == idLoja && (ordem.ID_ESTADO == 1 || ordem.ID_ESTADO == 2 || ordem.ID_ESTADO == 3 || ordem.ID_ESTADO == 4 || ordem.ID_ESTADO == 5)
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

        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }

        protected void listagemgeralors_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "CANCELOR")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    LINQ_DB.Orcamento update = new LINQ_DB.Orcamento();

                    var cancela = from c in DC.Orcamentos where c.ID == Convert.ToInt32(id) select c;
                    update = cancela.First();

                    update.ID_ESTADO = 6;
                    update.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    update.DATA_CONCLUSAO = DateTime.Now;
                    DC.SubmitChanges();
                    listagemgeralors.Rebind();
                    ShowAlertMessage("Orçamento cancelado com sucesso!");
                }

                if (e.CommandName == "CONVERT")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    int conta = (from o in DC.Orcamentos where o.ID == Convert.ToInt32(id) && o.CONVERTIDO_OR == false select o).Count();

                    if (conta > 0)
                        Response.Redirect("/onoff/home/InserirOrdemReparacaoORCCliente.aspx?id=" + id, false);
                    else
                    {
                        ShowAlertMessage("Orçamento já convertido em Ordem de Reparação!");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class Default : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Administrador" || regra == "SuperAdmin")
                {
                    atalhos.Style.Add("display", "block");
                    loja.Style.Add("display", "block");
                    ortecnicos.Style.Add("display", "block");
                    orreparadores.Style.Add("display", "block");
                    orclientes.Style.Add("display", "none");
                    orcclientes.Style.Add("display", "none");
                    divornotifica.Visible = true;
                    alertaPendente.Visible = true;

                    //ShowAlertMessage("DEVE RENOVAR A SUA LICENÇA QUE SE ENCONTRA A CADUCAR. POR FAVOR REGULARIZE A SUA SITUAÇÃO. TERÁ 2 DIAS PARA O FAZER A CONTAR A PARTIR DE 13.05.2013.");
                }

                if (regra == "Cliente")
                {
                    atalhos.Style.Add("display", "none");
                    loja.Style.Add("display", "none");
                    ortecnicos.Style.Add("display", "none");
                    orreparadores.Style.Add("display", "none");
                    orclientes.Style.Add("display", "block");
                    orcclientes.Style.Add("display", "block");
                    divornotifica.Visible = false;
                    alertaPendente.Visible = false;
                    divalertas.Visible = false;
                    divdados.Visible = false;

                }



                if (regra == "Lojista")
                {
                    atalhos.Visible = true;
                    divalertas.Visible = true;
                    divdados.Visible = false;
                    orreparadores.Visible = true;
                    orclientes.Visible = true;
                    ortecnicos.Visible = true;
                }

                if (regra == "Tecnico")
                {
                    atalhos.Visible = true;
                    divalertas.Visible = false;
                    divdados.Visible = false;
                    orreparadores.Visible = false;
                    orclientes.Visible = false;
                    ortecnicos.Visible = true;
                }

                if (regra == "Reparador")
                {
                    atalhos.Visible = false;
                    divalertas.Visible = false;
                    divdados.Visible = false;
                    orreparadores.Visible = true;
                    orclientes.Visible = false;
                    ortecnicos.Visible = false;
                }
            }
            else
                Response.Redirect("~/Default.aspx", true);

            string comando = "";

            if (Request.QueryString["comando"] != null)
                comando = Request.QueryString["comando"];

            if (comando == "resetpassword")
            {
            }


            if (!Page.IsPostBack)
            {
                contaDias();
                contaOrs();
                contaOrsA();
                contaClientes();
                contaUsers();
                contaTecnicos();
                contaReparadores();
                carregaOrcamentosPendentes();
            }
        }

        protected string GetRoleName(Guid useridComentario)
        {
            string resultado = "";
            try
            {
                var p = from d in DC.aspnet_UsersInRoles
                        where d.UserId == useridComentario
                        select d;

                foreach (var item in p)
                {
                    var pesqsuisa = from f in DC.aspnet_Roles
                                    where f.RoleId == item.RoleId
                                    select f;

                    foreach (var imt in pesqsuisa)
                    {
                        resultado = imt.RoleName;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

            return resultado;
        }

        protected void carregaOrcamentosPendentes()
        {
            try
            {
                Guid userid = new Guid();
                Guid Role = new Guid();
                string UserName = "";
                string role = "";

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

                        var pesquisaTodas = from o in DC.Orcamentos
                                            where o.ID_ESTADO == 1 || o.ID_ESTADO == 2 || o.ID_ESTADO == 3
                                            orderby o.DATA_ULTIMA_MODIFICACAO.Value descending
                                            select o;

                        foreach (var item in pesquisaTodas)
                        {
                            var verificaComentario = (from c in DC.Orcamentos_Comentarios
                                                      where c.ID_ORCAMENTO.Value == item.ID
                                                      orderby c.DATA_COMENTARIO.Value descending
                                                      select c).Take(1);

                            foreach (var cc in verificaComentario)
                            {
                                role = GetRoleName(cc.USERID_COMENTARIO.Value);

                                if (role == "Cliente")
                                {
                                    orcamentoPendente.Text += "<li class='task-item clearfix'>" +
                                                    "<a href='GerirORCCliente.aspx?id=" + item.ID.ToString() + "'><span class='label label-important category'>" + item.CODIGO.ToString() + " </span></a>" +
                                                    "<span class='task'>Orçamento <strong style='color:White; background-color:Red;'>NOVO COMENTÁRIO</strong></span></li>";
                                }
                                else
                                {
                                    orcamentoPendente.Text += "<li class='task-item clearfix'>" +
                                            "<a href='GerirORCCliente.aspx?id=" + item.ID.ToString() + "'><span class='label label-important category'>" + item.CODIGO.ToString() + " </span></a>" +
                                            "<span class='task'>Orçamento <strong style='color:White; background-color:Red;'>" + item.Orcamentos_Estado.DESCRICAO.ToString() + "</strong></span></li>";
                                }
                            }
                        }
                    }

                    if (regra == "Lojista" || regra == "Tecnico")
                    {

                        var procuraLoja = from l in DC.Funcionarios
                                          where l.USERID == userid
                                          select l;

                        foreach (var item in procuraLoja)
                        {
                            var pesquisaTodas = from o in DC.Orcamentos
                                                where o.ID_ESTADO == 1 || o.ID_ESTADO == 2 || o.ID_ESTADO == 3 && o.ID_LOJA.Value == item.ID_LOJA
                                                orderby o.DATA_ULTIMA_MODIFICACAO.Value descending
                                                select o;

                            foreach (var itemp in pesquisaTodas)
                            {
                                var verificaComentario = (from c in DC.Orcamentos_Comentarios
                                                          where c.ID_ORCAMENTO.Value == item.ID
                                                          orderby c.DATA_COMENTARIO.Value descending
                                                          select c).Take(1);
                                foreach (var cc in verificaComentario)
                                {
                                    role = GetRoleName(cc.USERID_COMENTARIO.Value);

                                    if (role == "Cliente")
                                    {
                                        orcamentoPendente.Text += "<li class='task-item clearfix'>" +
                                                        "<a href='GerirORCCliente.aspx?id=" + item.ID.ToString() + "'><span class='label label-important category'>" + item.CODIGO.ToString() + " </span></a>" +
                                                        "<span class='task'>Orçamento <strong style='color:White; background-color:Red;'>NOVO COMENTÁRIO</strong></span></li>";
                                    }
                                    else
                                    {
                                        orcamentoPendente.Text += "<li class='task-item clearfix'>" +
                                                "<a href='GerirORCCliente.aspx?id=" + item.ID.ToString() + "'><span class='label label-important category'>" + item.CODIGO.ToString() + " </span></a>" +
                                                 "<span class='task'>Verifique o estado do Orçamento </span></li>";
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void contaOrs()
        {
            try
            {
                int num = 0;

                var conta = (from o in DC.Ordem_Reparacaos
                             select o).Count();

                num = conta;
                numOrdens.InnerHtml = num.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void contaOrsA()
        {
            try
            {
                int num = 0;

                var conta = (from o in DC.Ordem_Reparacaos
                             where o.ATRIBUIDA == true
                             select o).Count();

                num = conta;
                numOrdensA.InnerHtml = num.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void contaClientes()
        {
            try
            {
                int num = 0;

                var conta = (from o in DC.Parceiros
                             where o.ID_TIPO_PARCEIRO == 1 || o.ID_TIPO_PARCEIRO == 2
                             select o).Count();

                num = conta;
                Numclientes.InnerHtml = num.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void contaUsers()
        {
            try
            {
                int num = 0;

                var conta = (from o in DC.Funcionarios
                             where o.ID_TIPO_FUNCIONARIO == 1
                             select o).Count();

                num = conta;
                NumUsers.InnerHtml = num.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void contaTecnicos()
        {
            try
            {
                int num = 0;

                var conta = (from o in DC.Funcionarios
                             where o.ID_TIPO_FUNCIONARIO == 2
                             select o).Count();

                num = conta;
                NumTecnicos.InnerHtml = num.ToString();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void contaReparadores()
        {
            try
            {
                int num = 0;

                var conta = (from o in DC.Parceiros
                             where o.ID_TIPO_PARCEIRO == 3 || o.ID_TIPO_PARCEIRO == 4
                             select o).Count();

                num = conta;
                NumReparadores.InnerHtml = num.ToString();
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

        protected void listaOrsReparadores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var orsReparadores = from atrib in DC.Ordem_Reparacao_Atribuicaos
                                     join reparador in DC.Parceiros on atrib.USERID equals reparador.USERID
                                     where atrib.Ordem_Reparacao.ID_ESTADO == 2 || atrib.Ordem_Reparacao.ID_ESTADO == 3 || atrib.Ordem_Reparacao.ID_ESTADO == 4 || atrib.Ordem_Reparacao.ID_ESTADO == 5
                                     select new
                                               {
                                                   ID = atrib.Ordem_Reparacao.ID,
                                                   CODIGO = atrib.Ordem_Reparacao.CODIGO,
                                                   DATA_REGISTO = atrib.Ordem_Reparacao.DATA_REGISTO.HasValue ? atrib.Ordem_Reparacao.DATA_REGISTO.Value.ToShortDateString().ToString() : null,
                                                   ESTADO = atrib.Ordem_Reparacao.Ordem_Reparacao_Estado.DESCRICAO,
                                                   DATA_PREVISTA_CONCLUSAO = atrib.Ordem_Reparacao.DATA_PREVISTA_CONCLUSAO.HasValue ? atrib.Ordem_Reparacao.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString() : null,
                                                   NOME_REP = reparador.NOME
                                               };

                listaOrsReparadores.DataSourceID = "";
                listaOrsReparadores.DataSource = orsReparadores;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void listaOrsReparadores_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITAR"].Controls[0];
                hLink.NavigateUrl = "DetalheOR.aspx?id=" + val1;
            }
        }

        protected void listaOrsTecnicos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var orsTecnicos = from atrib in DC.Ordem_Reparacao_Atribuicaos
                                  join tecnico in DC.Funcionarios on atrib.USERID equals tecnico.USERID
                                  where atrib.Ordem_Reparacao.ID_ESTADO == 2 || atrib.Ordem_Reparacao.ID_ESTADO == 3 || atrib.Ordem_Reparacao.ID_ESTADO == 4 || atrib.Ordem_Reparacao.ID_ESTADO == 5
                                  select new
                                           {
                                               ID = atrib.Ordem_Reparacao.ID,
                                               CODIGO = atrib.Ordem_Reparacao.CODIGO,
                                               DATA_REGISTO = atrib.Ordem_Reparacao.DATA_REGISTO.HasValue ? atrib.Ordem_Reparacao.DATA_REGISTO.Value.ToShortDateString().ToString() : null,
                                               NOME_TEC = tecnico.NOME,
                                               ESTADO = atrib.Ordem_Reparacao.Ordem_Reparacao_Estado.DESCRICAO,
                                               DATA_PREVISTA_CONCLUSAO = atrib.Ordem_Reparacao.DATA_PREVISTA_CONCLUSAO.HasValue ? atrib.Ordem_Reparacao.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString() : null
                                           };

                listaOrsTecnicos.DataSourceID = "";
                listaOrsTecnicos.DataSource = orsTecnicos;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaOrsTecnicos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITAR"].Controls[0];
                hLink.NavigateUrl = "DetalheOR.aspx?id=" + val1;
            }
        }

        protected void contaDias()
        {
            double totalDias = 0;
            const int maxTotalDias = 30;
            DateTime dataInicial;
            DateTime dataFinal;

            var pesquisa = from ordem in DC.Ordem_Reparacaos
                           where ordem.ID_ESTADO == 5
                           select ordem;

            foreach (var item in pesquisa)
            {
                dataInicial = item.DATA_REGISTO.Value;
                dataFinal = item.DATA_CONCLUSAO.Value;

                totalDias = (dataFinal - dataInicial).TotalDays;

                if (totalDias >= maxTotalDias)
                {
                    alertaPendente.Visible = true;
                    tarefaPendente.Text +=
                        "<li class='task-item clearfix'>" +
                        "<a href='DetalheOR.aspx?id=" + item.ID.ToString() + "'><span class='label label-important category'>" + item.CODIGO.ToString() + " </span></a>" +
                        "<span class='task'>Verifique o estado da OR </span></li>";
                }
            }

            var pesquisaOR = from or in DC.Ordem_Reparacaos
                             where or.ID_ESTADO == 5 && or.CLIENTE_NOTIFICADO == false
                             select or;

            foreach (var ornot in pesquisaOR)
            {

                divornotifica.Visible = true;
                notificarClientes.Text +=
                    "<li class='task-item clearfix'>" +
                        "<a href='FecharOrdemReparacao.aspx'><span class='label label-important category'>" + ornot.CODIGO.ToString() + " </span></a>" +
                        "<span class='task'>Ordem de Reparação para Fechar </span></li>";
            }
        }

        protected void listaorclientes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            Guid userid = new Guid();
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

            }
            else
                Response.Redirect("~/Default.aspx", true);

            try
            {
                var carregaORCliente = from ordem in DC.Ordem_Reparacaos
                                       join parceiro in DC.Parceiros on ordem.USERID equals parceiro.USERID
                                       where (parceiro.ID_TIPO_PARCEIRO == 1 || parceiro.ID_TIPO_PARCEIRO == 2 || parceiro.ID_TIPO_PARCEIRO == 4) && parceiro.USERID == userid
                                       select new
                                       {
                                           ID = ordem.ID,
                                           CODIGO = ordem.CODIGO,
                                           DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString().ToString() : null,
                                           NOME_CLIENTE = parceiro.NOME,
                                           ESTADO = ordem.Ordem_Reparacao_Estado.DESCRICAO,
                                           DATA_PREVISTA_CONCLUSAO = ordem.DATA_PREVISTA_CONCLUSAO.HasValue ? ordem.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString() : null,
                                           IMEI = ordem.Equipamento_Avariado.IMEI,
                                           MARCA = ordem.Equipamento_Avariado.Marca.DESCRICAO,
                                           MODELO = ordem.Equipamento_Avariado.Modelo.DESCRICAO
                                       };

                listaorclientes.DataSourceID = "";
                listaorclientes.DataSource = carregaORCliente;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaorclientes_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITAR"].Controls[0];
                hLink.NavigateUrl = "DetalheOR.aspx?ID=" + val1;
            }

        }

        protected void listaOrcamentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
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

                    var carregaOrs = from ordem in DC.Orcamentos
                                     join parceiros in DC.Parceiros on ordem.USERID.Value equals parceiros.USERID.Value
                                     join equip in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                     where ordem.USERID.Value == userid
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
                                         ID = ordem.ID,
                                         VALOR_OR = ordem.VALOR_ORCAMENTO.Value,
                                         USERID = ordem.USERID.Value
                                     };

                    listaOrcamentos.DataSourceID = "";
                    listaOrcamentos.DataSource = carregaOrs;
                }


            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaOrcamentos_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                Guid val2 = new Guid(item["USERID"].Text);
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "GerirMeuORC.aspx?ID=" + val1 + "&userid=" + val2;
            }
        }
    }
}
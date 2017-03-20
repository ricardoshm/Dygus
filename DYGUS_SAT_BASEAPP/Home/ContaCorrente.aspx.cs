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
    public partial class ContaCorrente : Telerik.Web.UI.RadAjaxPage
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


            if (!Page.IsPostBack)
            {
                carregaTecnicos();
                carregaEstados();
                carregaLoja();
            }

        }


        protected void carregaLoja()
        {
            try
            {
                var loja = from tec in DC.Lojas
                           where tec.ACTIVO == true
                           orderby tec.NOME ascending
                           select tec;

                ddlloja.DataSource = loja;
                ddlloja.DataTextField = "NOME";
                ddlloja.DataValueField = "ID";
                ddlloja.DataBind();
                ddlloja.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void carregaTecnicos()
        {
            try
            {
                var tecnicos = from tec in DC.Funcionarios
                               where tec.ACTIVO == true
                               where tec.ID_TIPO_FUNCIONARIO == 2
                               select tec;

                ddlTecnico.DataSource = tecnicos;
                ddlTecnico.DataTextField = "NOME";
                ddlTecnico.DataValueField = "ID";
                ddlTecnico.DataBind();
                ddlTecnico.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void carregaEstados()
        {
            try
            {
                var tecnicos = from tec in DC.Ordem_Reparacao_Estados
                               where tec.ID == 5 || tec.ID == 6
                               select tec;

                ddlestado.DataSource = tecnicos;
                ddlestado.DataTextField = "DESCRICAO";
                ddlestado.DataValueField = "ID";
                ddlestado.DataBind();
                ddlestado.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaCampos()
        {
            if (ddlMes.SelectedItem.Text == "Por favor seleccione...")
            {
                if (datainicio.SelectedDate == null || datainicio.SelectedDate.GetValueOrDefault() == DateTime.MinValue)
                {
                    erro.Visible = errorMessage.Visible = true;
                    errorMessage.InnerHtml = "O campo Data de Inicio é de preenchimento obrigatório!";
                    return false;
                }

                if (datafim.SelectedDate == null || datainicio.SelectedDate.GetValueOrDefault() == DateTime.MinValue)
                {
                    erro.Visible = errorMessage.Visible = true;
                    errorMessage.InnerHtml = "O campo Data de Fim é de preenchimento obrigatório!";
                    return false;
                }
            }
            else
                return true;

            if (ddlestado.SelectedItem.Text == "Por favor seleccione...")
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Estado da OR é de preenchimento obrigatório!";
                return false;
            }

            return true;
        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                if (ddlloja.SelectedItem.Text == "Por favor seleccione...")
                {
                    if (ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                    {
                        # region com estado e sem tecnico

                        if (ddlestado.SelectedItem.Text == "Reparação Concluída" && ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                        {
                            if (ddlMes.SelectedItem.Text == "Por favor seleccione...")
                            {
                                try
                                {

                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     where ordem.ID_ESTADO == 5 && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();

                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            else
                            {
                                #region Verificação de datas por mês

                                string dataMin = "";
                                string dataMax = "";
                                DateTime hoje = DateTime.Now;

                                if (ddlMes.SelectedItem.Value == "1")
                                {
                                    dataMin = hoje.Year.ToString() + "-01-01";
                                    dataMax = hoje.Year.ToString() + "-02-01";
                                }
                                if (ddlMes.SelectedItem.Value == "2")
                                {
                                    dataMin = hoje.Year.ToString() + "-02-01";
                                    dataMax = hoje.Year.ToString() + "-03-01";
                                }
                                if (ddlMes.SelectedItem.Value == "3")
                                {
                                    dataMin = hoje.Year.ToString() + "-03-01";
                                    dataMax = hoje.Year.ToString() + "-04-01";
                                }
                                if (ddlMes.SelectedItem.Value == "4")
                                {
                                    dataMin = hoje.Year.ToString() + "-04-01";
                                    dataMax = hoje.Year.ToString() + "-05-01";
                                }
                                if (ddlMes.SelectedItem.Value == "5")
                                {
                                    dataMin = hoje.Year.ToString() + "-05-01";
                                    dataMax = hoje.Year.ToString() + "-06-01";
                                }
                                if (ddlMes.SelectedItem.Value == "6")
                                {
                                    dataMin = hoje.Year.ToString() + "-06-01";
                                    dataMax = hoje.Year.ToString() + "-07-01";
                                }
                                if (ddlMes.SelectedItem.Value == "7")
                                {
                                    dataMin = hoje.Year.ToString() + "-07-01";
                                    dataMax = hoje.Year.ToString() + "-08-01";
                                }
                                if (ddlMes.SelectedItem.Value == "8")
                                {
                                    dataMin = hoje.Year.ToString() + "-08-01";
                                    dataMax = hoje.Year.ToString() + "-09-01";
                                }
                                if (ddlMes.SelectedItem.Value == "9")
                                {
                                    dataMin = hoje.Year.ToString() + "-09-01";
                                    dataMax = hoje.Year.ToString() + "-10-01";
                                }
                                if (ddlMes.SelectedItem.Value == "10")
                                {
                                    dataMin = hoje.Year.ToString() + "-10-01";
                                    dataMax = hoje.Year.ToString() + "-11-01";
                                }
                                if (ddlMes.SelectedItem.Value == "11")
                                {
                                    dataMin = hoje.Year.ToString() + "-11-01";
                                    dataMax = hoje.Year.ToString() + "-12-01";
                                }
                                if (ddlMes.SelectedItem.Value == "12")
                                {
                                    dataMin = hoje.Year.ToString() + "-12-01";
                                    dataMax = hoje.AddYears(1).Year.ToString() + "-01-01";
                                }

                                DateTime dtMin = Convert.ToDateTime(dataMin);
                                DateTime dtMax = Convert.ToDateTime(dataMax);

                                #endregion

                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     where ordem.ID_ESTADO == 5 && (ordem.DATA_CONCLUSAO.Value >= dtMin && ordem.DATA_CONCLUSAO.Value < dtMax)
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();

                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }

                        }


                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 where ordem.ID_ESTADO == 6 && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value <= datafim.SelectedDate)
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }
                    if (ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                    {
                        #region com estado e com tecnico

                        if (ddlestado.SelectedItem.Text == "Reparação Concluída" && ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                        {
                            if (ddlMes.SelectedItem.Text == "Por favor seleccione...")
                            {
                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                     join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                     where ordem.ID_ESTADO == 5 && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            else
                            {
                                #region Verificação de datas por mês

                                string dataMin = "";
                                string dataMax = "";
                                DateTime hoje = DateTime.Now;

                                if (ddlMes.SelectedItem.Value == "1")
                                {
                                    dataMin = hoje.Year.ToString() + "-01-01";
                                    dataMax = hoje.Year.ToString() + "-02-01";
                                }
                                if (ddlMes.SelectedItem.Value == "2")
                                {
                                    dataMin = hoje.Year.ToString() + "-02-01";
                                    dataMax = hoje.Year.ToString() + "-03-01";
                                }
                                if (ddlMes.SelectedItem.Value == "3")
                                {
                                    dataMin = hoje.Year.ToString() + "-03-01";
                                    dataMax = hoje.Year.ToString() + "-04-01";
                                }
                                if (ddlMes.SelectedItem.Value == "4")
                                {
                                    dataMin = hoje.Year.ToString() + "-04-01";
                                    dataMax = hoje.Year.ToString() + "-05-01";
                                }
                                if (ddlMes.SelectedItem.Value == "5")
                                {
                                    dataMin = hoje.Year.ToString() + "-05-01";
                                    dataMax = hoje.Year.ToString() + "-06-01";
                                }
                                if (ddlMes.SelectedItem.Value == "6")
                                {
                                    dataMin = hoje.Year.ToString() + "-06-01";
                                    dataMax = hoje.Year.ToString() + "-07-01";
                                }
                                if (ddlMes.SelectedItem.Value == "7")
                                {
                                    dataMin = hoje.Year.ToString() + "-07-01";
                                    dataMax = hoje.Year.ToString() + "-08-01";
                                }
                                if (ddlMes.SelectedItem.Value == "8")
                                {
                                    dataMin = hoje.Year.ToString() + "-08-01";
                                    dataMax = hoje.Year.ToString() + "-09-01";
                                }
                                if (ddlMes.SelectedItem.Value == "9")
                                {
                                    dataMin = hoje.Year.ToString() + "-09-01";
                                    dataMax = hoje.Year.ToString() + "-10-01";
                                }
                                if (ddlMes.SelectedItem.Value == "10")
                                {
                                    dataMin = hoje.Year.ToString() + "-10-01";
                                    dataMax = hoje.Year.ToString() + "-11-01";
                                }
                                if (ddlMes.SelectedItem.Value == "11")
                                {
                                    dataMin = hoje.Year.ToString() + "-11-01";
                                    dataMax = hoje.Year.ToString() + "-12-01";
                                }
                                if (ddlMes.SelectedItem.Value == "12")
                                {
                                    dataMin = hoje.Year.ToString() + "-12-01";
                                    dataMax = hoje.AddYears(1).Year.ToString() + "-01-01";
                                }

                                DateTime dtMin = Convert.ToDateTime(dataMin);
                                DateTime dtMax = Convert.ToDateTime(dataMax);

                                #endregion

                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                     join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                     where ordem.ID_ESTADO == 5 && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= dtMin && ordem.DATA_CONCLUSAO.Value < dtMax)
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }

                            return;
                        }
                    }

                    if (validaCampos())
                    {
                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.ID_ESTADO == 6 && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value <= datafim.SelectedDate)
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };




                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }

                    if (ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                    {
                        # region sem estado e sem tecnico

                        if (ddlestado.SelectedItem.Text == "Por favor seleccione..." && ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                        {
                            if (ddlMes.SelectedItem.Text == "Por favor seleccione...")
                            {
                                try
                                {

                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     where (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();

                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            else
                            {
                                #region Verificação de datas por mês

                                string dataMin = "";
                                string dataMax = "";
                                DateTime hoje = DateTime.Now;

                                if (ddlMes.SelectedItem.Value == "1")
                                {
                                    dataMin = hoje.Year.ToString() + "-01-01";
                                    dataMax = hoje.Year.ToString() + "-02-01";
                                }
                                if (ddlMes.SelectedItem.Value == "2")
                                {
                                    dataMin = hoje.Year.ToString() + "-02-01";
                                    dataMax = hoje.Year.ToString() + "-03-01";
                                }
                                if (ddlMes.SelectedItem.Value == "3")
                                {
                                    dataMin = hoje.Year.ToString() + "-03-01";
                                    dataMax = hoje.Year.ToString() + "-04-01";
                                }
                                if (ddlMes.SelectedItem.Value == "4")
                                {
                                    dataMin = hoje.Year.ToString() + "-04-01";
                                    dataMax = hoje.Year.ToString() + "-05-01";
                                }
                                if (ddlMes.SelectedItem.Value == "5")
                                {
                                    dataMin = hoje.Year.ToString() + "-05-01";
                                    dataMax = hoje.Year.ToString() + "-06-01";
                                }
                                if (ddlMes.SelectedItem.Value == "6")
                                {
                                    dataMin = hoje.Year.ToString() + "-06-01";
                                    dataMax = hoje.Year.ToString() + "-07-01";
                                }
                                if (ddlMes.SelectedItem.Value == "7")
                                {
                                    dataMin = hoje.Year.ToString() + "-07-01";
                                    dataMax = hoje.Year.ToString() + "-08-01";
                                }
                                if (ddlMes.SelectedItem.Value == "8")
                                {
                                    dataMin = hoje.Year.ToString() + "-08-01";
                                    dataMax = hoje.Year.ToString() + "-09-01";
                                }
                                if (ddlMes.SelectedItem.Value == "9")
                                {
                                    dataMin = hoje.Year.ToString() + "-09-01";
                                    dataMax = hoje.Year.ToString() + "-10-01";
                                }
                                if (ddlMes.SelectedItem.Value == "10")
                                {
                                    dataMin = hoje.Year.ToString() + "-10-01";
                                    dataMax = hoje.Year.ToString() + "-11-01";
                                }
                                if (ddlMes.SelectedItem.Value == "11")
                                {
                                    dataMin = hoje.Year.ToString() + "-11-01";
                                    dataMax = hoje.Year.ToString() + "-12-01";
                                }
                                if (ddlMes.SelectedItem.Value == "12")
                                {
                                    dataMin = hoje.Year.ToString() + "-12-01";
                                    dataMax = hoje.AddYears(1).Year.ToString() + "-01-01";
                                }

                                DateTime dtMin = Convert.ToDateTime(dataMin);
                                DateTime dtMax = Convert.ToDateTime(dataMax);

                                #endregion

                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     where ordem.ID_ESTADO == 5 && (ordem.DATA_CONCLUSAO.Value >= dtMin && ordem.DATA_CONCLUSAO.Value <= dtMax)
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();

                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }

                        }


                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 where ordem.ID_ESTADO == 6 && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value <= datafim.SelectedDate)
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }
                    if (ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                    {
                        #region sem estado e com tecnico

                        if (ddlestado.SelectedItem.Text == "Por favor seleccione..." && ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                        {
                            if (ddlMes.SelectedItem.Text == "Por favor seleccione...")
                            {
                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                     join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                     where funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            else
                            {
                                #region Verificação de datas por mês

                                string dataMin = "";
                                string dataMax = "";
                                DateTime hoje = DateTime.Now;

                                if (ddlMes.SelectedItem.Value == "1")
                                {
                                    dataMin = hoje.Year.ToString() + "-01-01";
                                    dataMax = hoje.Year.ToString() + "-02-01";
                                }
                                if (ddlMes.SelectedItem.Value == "2")
                                {
                                    dataMin = hoje.Year.ToString() + "-02-01";
                                    dataMax = hoje.Year.ToString() + "-03-01";
                                }
                                if (ddlMes.SelectedItem.Value == "3")
                                {
                                    dataMin = hoje.Year.ToString() + "-03-01";
                                    dataMax = hoje.Year.ToString() + "-04-01";
                                }
                                if (ddlMes.SelectedItem.Value == "4")
                                {
                                    dataMin = hoje.Year.ToString() + "-04-01";
                                    dataMax = hoje.Year.ToString() + "-05-01";
                                }
                                if (ddlMes.SelectedItem.Value == "5")
                                {
                                    dataMin = hoje.Year.ToString() + "-05-01";
                                    dataMax = hoje.Year.ToString() + "-06-01";
                                }
                                if (ddlMes.SelectedItem.Value == "6")
                                {
                                    dataMin = hoje.Year.ToString() + "-06-01";
                                    dataMax = hoje.Year.ToString() + "-07-01";
                                }
                                if (ddlMes.SelectedItem.Value == "7")
                                {
                                    dataMin = hoje.Year.ToString() + "-07-01";
                                    dataMax = hoje.Year.ToString() + "-08-01";
                                }
                                if (ddlMes.SelectedItem.Value == "8")
                                {
                                    dataMin = hoje.Year.ToString() + "-08-01";
                                    dataMax = hoje.Year.ToString() + "-09-01";
                                }
                                if (ddlMes.SelectedItem.Value == "9")
                                {
                                    dataMin = hoje.Year.ToString() + "-09-01";
                                    dataMax = hoje.Year.ToString() + "-10-01";
                                }
                                if (ddlMes.SelectedItem.Value == "10")
                                {
                                    dataMin = hoje.Year.ToString() + "-10-01";
                                    dataMax = hoje.Year.ToString() + "-11-01";
                                }
                                if (ddlMes.SelectedItem.Value == "11")
                                {
                                    dataMin = hoje.Year.ToString() + "-11-01";
                                    dataMax = hoje.Year.ToString() + "-12-01";
                                }
                                if (ddlMes.SelectedItem.Value == "12")
                                {
                                    dataMin = hoje.Year.ToString() + "-12-01";
                                    dataMax = hoje.AddYears(1).Year.ToString() + "-01-01";
                                }

                                DateTime dtMin = Convert.ToDateTime(dataMin);
                                DateTime dtMax = Convert.ToDateTime(dataMax);

                                #endregion
                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                     join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                     where funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= dtMin && ordem.DATA_CONCLUSAO.Value < dtMax)
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            return;
                        }
                    }

                    if (validaCampos())
                    {
                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.ID_ESTADO == 6 && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };




                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }
                }

                if (ddlloja.SelectedItem.Text != "Por favor seleccione...")
                {
                    if (ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                    {
                        #region com estado com tecnico e com loja

                        if (ddlestado.SelectedItem.Text == "Reparação Concluída" && ddlTecnico.SelectedItem.Text != "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            if (ddlMes.SelectedItem.Text == "Por favor seleccione...")
                            {
                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                     join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                     where ordem.ID_ESTADO == 5 && ordem.Loja.NOME == ddlloja.SelectedItem.Text && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            else
                            {
                                try
                                {
                                    var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                     join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                     join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                     where ordem.ID_ESTADO == 5 && ordem.Loja.NOME == ddlloja.SelectedItem.Text && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                     orderby ordem.ID descending
                                                     select new
                                                     {
                                                         CODIGO = ordem.CODIGO,
                                                         DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                         VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                         VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                         MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                         DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                         LOJA = ordem.Loja.NOME
                                                     };



                                    listagemgeralors.DataSourceID = "";
                                    listagemgeralors.DataSource = carregaOrs;
                                    listagemgeralors.DataBind();
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            return;
                        }


                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text != "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.ID_ESTADO == 6 && ordem.Loja.NOME == ddlloja.SelectedItem.Text && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }

                    if (ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                    {
                        #region com estado sem tecnico e com loja

                        if (ddlestado.SelectedItem.Text == "Reparação Concluída" && ddlTecnico.SelectedItem.Text == "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.ID_ESTADO == 5 && ordem.Loja.NOME == ddlloja.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }

                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text == "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.ID_ESTADO == 6 && ordem.Loja.NOME == ddlloja.SelectedItem.Text && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value <= datafim.SelectedDate)
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }

                    if (ddlTecnico.SelectedItem.Text != "Por favor seleccione...")
                    {
                        #region sem estado com tecnico e com loja

                        if (ddlestado.SelectedItem.Text == "Por favor seleccione..." && ddlTecnico.SelectedItem.Text != "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.Loja.NOME == ddlloja.SelectedItem.Text && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }


                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text != "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.ID_ESTADO == 6 && ordem.Loja.NOME == ddlloja.SelectedItem.Text && funcionario.NOME == ddlTecnico.SelectedItem.Text && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value <= datafim.SelectedDate)
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }

                    if (ddlTecnico.SelectedItem.Text == "Por favor seleccione...")
                    {
                        #region sem estado sem tecnico e com loja

                        if (ddlestado.SelectedItem.Text == "Por favor seleccione..." && ddlTecnico.SelectedItem.Text == "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.Loja.NOME == ddlloja.SelectedItem.Text && (ordem.DATA_CONCLUSAO.Value >= datainicio.SelectedDate && ordem.DATA_CONCLUSAO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }

                        if (ddlestado.SelectedItem.Text == "Fechada" && ddlTecnico.SelectedItem.Text == "Por favor seleccione..." && ddlloja.SelectedItem.Text != "Por favor seleccione...")
                        {
                            try
                            {
                                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                                 join atribuicao in DC.Ordem_Reparacao_Atribuicaos on ordem.ID equals atribuicao.ID_ORDEM_REPARACAO
                                                 join funcionario in DC.Funcionarios on atribuicao.USERID equals funcionario.USERID
                                                 where ordem.ID_ESTADO == 6 && ordem.Loja.NOME == ddlloja.SelectedItem.Text && (ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value >= datainicio.SelectedDate && ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value < datafim.SelectedDate.Value.AddDays(1))
                                                 orderby ordem.ID descending
                                                 select new
                                                 {
                                                     CODIGO = ordem.CODIGO,
                                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                                     VALOR_CUSTO = ordem.VALOR_CUSTO.Value,
                                                     MARGEM = ordem.VALOR_FINAL.Value - ordem.VALOR_CUSTO.Value,
                                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                                     LOJA = ordem.Loja.NOME
                                                 };



                                listagemgeralors.DataSourceID = "";
                                listagemgeralors.DataSource = carregaOrs;
                                listagemgeralors.DataBind();
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);
                                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                            }
                            return;
                        }
                        #endregion
                    }
                }
            }


        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            datainicio.Clear();
            datafim.Clear();
        }

        protected void listagemgeralors_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (isPdfExport)
                FormatGridItem(e.Item);
        }

        protected void listagemgeralors_ItemCommand(object sender, GridCommandEventArgs e)
        {
            isPdfExport = true;
            listagemgeralors.MasterTableView.ExportToPdf();
        }

        bool isPdfExport = false;

        protected void FormatGridItem(GridItem item)
        {
            item.Style["color"] = "#eeeeee";

            if (item is GridDataItem)
            {
                item.Style["vertical-align"] = "middle";
                item.Style["text-align"] = "center";
            }

            switch (item.ItemType) //Mimic RadGrid appearance for the exported PDF file
            {
                case GridItemType.Item:
                    item.Style["background-color"] = "#4F4F4F";
                    break;
                case GridItemType.AlternatingItem:
                    item.Style["background-color"] = "#494949";
                    break;
                case GridItemType.Header:
                    item.Style["background-color"] = "#2B2B2B";
                    break;
                case GridItemType.CommandItem:
                    item.Style["background-color"] = "#000000";
                    break;
            }

            if (item is GridCommandItem)
            {
                item.PrepareItemStyle();  //needed to span the image over the CommandItem cells
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            listagemgeralors.ClientSettings.Scrolling.AllowScroll = false;
            listagemgeralors.MasterTableView.Width = Unit.Percentage(100);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "myscript", "function pageLoad(){PrintRadGrid();}", true);
        }

        protected void ddlMes_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                if (ddlMes.SelectedItem.Text != "Por favor seleccione...")
                {
                    datainicio.Enabled = false;
                    datafim.Enabled = false;
                }
                else
                {
                    datainicio.Enabled = true;
                    datafim.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

       

        protected void btnExport_Click(object sender, EventArgs e)
        {
            
            //listagemgeralors.ExportSettings.Excel.Format = (GridExcelExportFormat)Enum.Parse(typeof(GridExcelExportFormat), "Xlsx");
            listagemgeralors.ExportSettings.IgnorePaging = true;
            listagemgeralors.ExportSettings.ExportOnlyData = true;
            listagemgeralors.ExportSettings.OpenInNewWindow = true;
            listagemgeralors.MasterTableView.ExportToExcel();
        }
    }
}
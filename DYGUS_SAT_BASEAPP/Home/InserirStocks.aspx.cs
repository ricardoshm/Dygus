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
    public partial class InserirStocks : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Cliente" || regra == "Reparador")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);

            //(Page.Master.FindControl("stocksartigos") as HtmlControl).Attributes.Add("class", "has-sub active");
            //(Page.Master.FindControl("stocksprodutoinsert") as HtmlControl).Attributes.Add("class", "active");


            if (!Page.IsPostBack)
            {
                int codNovoCliente = 0;

                try
                {
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "A"
                                 select conf;

                    LINQ_DB.Configuracao configuracao = new LINQ_DB.Configuracao();

                    configuracao = config.First();
                    codNovoCliente = Convert.ToInt32(configuracao.CODIGO) + 1;

                    string numeroCliente = "";

                    string prefixo = configuracao.INICIAL.ToString() + "-0000";

                    var confg = from confii in DC.Configuracaos
                                select confii;

                    foreach (var item in confg)
                    {
                        numeroCliente = prefixo + codNovoCliente.ToString();
                        tbcodartigo.Text = numeroCliente;
                    }

                    carregaArmazens();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }


            }


        }

        protected bool validaCampos()
        {
            string descricao = "";
            double valorcusto = 0;
            //double valorrevenda = 0;
            //double valorvenda = 0;

            descricao = tbnome.Text;
            valorcusto = Convert.ToDouble(tbvalorcusto.Value);

            if (String.IsNullOrEmpty(descricao))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Descrição de Artigo é obrigatório!";
                return false;
            }

            if (tbqtddisponivel.Value == null || tbqtddisponivel.Value <= 0)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Quantidade Disponível é obrigatório!";
                return false;
            }

            if (tbqtdminima.Value == null || tbqtdminima.Value <= 0)
            {
                tbqtdminima.Value = 0;
            }

            if (valorcusto == null || valorcusto <= 0)
            {
                valorcusto = 0;
            }

            if (tbvalorvenda.Value == null || tbvalorvenda.Value <= 0)
            {
                tbvalorvenda.Value = 0;
            }

            if (tbvalorrevenda.Value == null || tbvalorrevenda.Value <= 0)
            {
                tbvalorrevenda.Value = 0;
            }

            if (ddlarmazem.SelectedItem.Text == "Por favor seleccione...")
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Armazém é obrigatório!";
                return false;
            }
            erro.Visible = errorMessage.Visible = false;
            return true;
        }

        protected void btnGrava_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                try
                {
                    LINQ_DB.Artigo NOVOARTIGO = new LINQ_DB.Artigo();

                    NOVOARTIGO.CODIGO = tbcodartigo.Text;
                    NOVOARTIGO.DESCRICAO = tbnome.Text;
                    NOVOARTIGO.ID_ARMAZEM = Convert.ToInt32(ddlarmazem.SelectedItem.Value);
                    NOVOARTIGO.DATA_REGISTO = DateTime.Now;
                    NOVOARTIGO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    NOVOARTIGO.ACTIVO = true;

                    DC.Artigos.InsertOnSubmit(NOVOARTIGO);
                    DC.SubmitChanges();

                    LINQ_DB.Artigo_Stock ARTIGOEMSTOCK = new LINQ_DB.Artigo_Stock();

                    ARTIGOEMSTOCK.ID_ARTIGO = NOVOARTIGO.ID;
                    ARTIGOEMSTOCK.VALOR_CUSTO = Convert.ToDouble(tbvalorcusto.Value);
                    ARTIGOEMSTOCK.VALOR_REVENDA = Convert.ToDouble(tbvalorrevenda.Value);
                    ARTIGOEMSTOCK.VALOR_VENDA = Convert.ToDouble(tbvalorvenda.Value);
                    ARTIGOEMSTOCK.QTD_DISPONIVEL = Convert.ToInt32(tbqtddisponivel.Value);
                    ARTIGOEMSTOCK.QTD_MINIMA_STOCK = Convert.ToInt32(tbqtdminima.Value);

                    DC.Artigo_Stocks.InsertOnSubmit(ARTIGOEMSTOCK);
                    DC.SubmitChanges();

                    LINQ_DB.Configuracao NOVOCODIGOARTIGO = new LINQ_DB.Configuracao();
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "A"
                                 select conf;
                    NOVOCODIGOARTIGO = config.First();
                    NOVOCODIGOARTIGO.CODIGO = NOVOCODIGOARTIGO.CODIGO + 1;
                    DC.SubmitChanges();

                    listagemartigos.Rebind();
                    recalculaCodCliente();

                    sucesso.Visible = true;
                    sucesso.InnerHtml = "Artigo " + NOVOARTIGO.CODIGO.ToString() + " registado com êxito!";
                    limpaCampos();
                }
                catch (Exception ex)
                {

                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void recalculaCodCliente()
        {
            int codNovoCliente = 0;

            try
            {
                var config = from conf in DC.Configuracaos
                             where conf.INICIAL == "A"
                             select conf;

                LINQ_DB.Configuracao configuracao = new LINQ_DB.Configuracao();

                configuracao = config.First();
                codNovoCliente = Convert.ToInt32(configuracao.CODIGO) + 1;

                string numeroCliente = "";

                string prefixo = configuracao.INICIAL.ToString() + "-0000";

                var confg = from confii in DC.Configuracaos
                            select confii;

                foreach (var item in confg)
                {
                    numeroCliente = prefixo + codNovoCliente.ToString();
                    tbcodartigo.Text = numeroCliente;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void carregaArmazens()
        {
            try
            {
                var armazem = from a in DC.Armazems
                              select a;

                ddlarmazem.DataSource = armazem;
                ddlarmazem.DataTextField = "DESCRICAO";
                ddlarmazem.DataValueField = "ID";
                ddlarmazem.DataBind();
                ddlarmazem.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void limpaCampos()
        {
            tbnome.Text = "";
            tbqtddisponivel.Text = "";
            tbqtdminima.Text = "";
            tbvalorcusto.Text = "";
            tbvalorrevenda.Text = "";
            tbvalorvenda.Text = "";
            ddlarmazem.ClearSelection();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = "";
            tbqtddisponivel.Text = "";
            tbqtdminima.Text = "";
            tbvalorcusto.Text = "";
            tbvalorrevenda.Text = "";
            tbvalorvenda.Text = "";
            ddlarmazem.ClearSelection();
        }

        protected void listagemartigos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var consultaArtigos = from artigo in DC.Artigos
                                      join stock in DC.Artigo_Stocks on artigo.ID equals stock.ID_ARTIGO
                                      join armarzem in DC.Armazems on artigo.ID_ARMAZEM equals armarzem.ID
                                      select new
                                      {
                                          ID = artigo.ID,
                                          CODIGO = artigo.CODIGO,
                                          DESCRICAO = artigo.DESCRICAO,
                                          VALOR_CUSTO = stock.VALOR_CUSTO,
                                          VALOR_REVENDA = stock.VALOR_REVENDA,
                                          VALOR_VENDA = stock.VALOR_VENDA,
                                          ARMAZEM = armarzem.DESCRICAO,
                                          QTD = stock.QTD_DISPONIVEL
                                      };

                listagemartigos.DataSourceID = "";
                listagemartigos.DataSource = consultaArtigos;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
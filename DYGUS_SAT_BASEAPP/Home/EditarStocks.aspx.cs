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
    public partial class EditarStocks : Telerik.Web.UI.RadAjaxPage
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

            

            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarStocks.aspx", true);
                return;
            }

            if (!Page.IsPostBack)
            {
                carregaArtigo();
            }
        }



        protected bool validaCampos()
        {
            string descricao = "";

            descricao = tbnome.Text;


            if (String.IsNullOrEmpty(descricao))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Descrição de Artigo é obrigatório!";
                return false;
            }

            if (tbqtddisponivel.Value == null)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Quantidade Disponível é obrigatório!";
                return false;
            }

            if (tbvalorcusto.Value == null)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Valor de Custo é obrigatório!";
                return false;
            }

            if (tbvalorvenda.Value == null)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Valor de Venda é obrigatório!";
                return false;
            }

            if (tbvalorrevenda.Value == null)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Valor de Revenda é obrigatório!";
                return false;
            }


            return true;
        }

        protected void carregaArtigo()
        {
            try
            {
                string id = "";

                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                }
                else
                {
                    Response.Redirect("ListarStocks.aspx", true);
                    return;
                }

                var consultaartigos = from f in DC.Artigos
                                      join armazens in DC.Armazems on f.ID_ARMAZEM equals armazens.ID
                                      where f.ID == Convert.ToInt32(id)
                                      select new
                                      {
                                          COD = f.CODIGO,
                                          NOME = f.DESCRICAO,
                                          ARMAZEM = f.ID_ARMAZEM,
                                      };

                foreach (var item in consultaartigos)
                {
                    tbcodartigo.Text = item.COD;
                    tbnome.Text = item.NOME;

                    var armazens = from pa in DC.Armazems
                                   orderby pa.DESCRICAO ascending
                                   select pa;

                    foreach (var itemst in armazens)
                    {
                        RadComboBoxItem list = new RadComboBoxItem();
                        list.Text = itemst.DESCRICAO;
                        list.Value = itemst.ID.ToString();

                        ddlarmazem.Items.Add(list);
                    }
                    ddlarmazem.SelectedValue = item.ARMAZEM.Value.ToString();
                }

                var consultaStocks = from s in DC.Artigo_Stocks
                                     where s.ID_ARTIGO == Convert.ToInt32(id)
                                     select s;

                foreach (var item in consultaStocks)
                {
                    tbvalorcusto.Text = item.VALOR_CUSTO.Value.ToString();
                    tbvalorvenda.Text = item.VALOR_VENDA.Value.ToString();
                    tbvalorrevenda.Text = item.VALOR_REVENDA.Value.ToString();
                    tbqtddisponivel.Text = item.QTD_DISPONIVEL.ToString();
                    tbqtdminima.Text = item.QTD_MINIMA_STOCK.ToString();
                }

            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnGrava_Click(object sender, EventArgs e)
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarStocks.aspx", true);
                return;
            }


            if (validaCampos())
            {
                try
                {
                    var meuArtigo = from m in DC.Artigos
                                    where m.ID == Convert.ToInt32(id)
                                    select m;

                    LINQ_DB.Artigo NOVOARTIGO = new LINQ_DB.Artigo();

                    NOVOARTIGO = meuArtigo.First();

                    NOVOARTIGO.DESCRICAO = tbnome.Text;
                    NOVOARTIGO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;

                    DC.SubmitChanges();

                    var meuStock = from ms in DC.Artigo_Stocks
                                   where ms.ID_ARTIGO == Convert.ToInt32(id)
                                   select ms;

                    LINQ_DB.Artigo_Stock ARTIGOEMSTOCK = new LINQ_DB.Artigo_Stock();

                    ARTIGOEMSTOCK = meuStock.First();
                    ARTIGOEMSTOCK.ID_ARTIGO = NOVOARTIGO.ID;
                    ARTIGOEMSTOCK.VALOR_CUSTO = Convert.ToDouble(tbvalorcusto.Value);
                    ARTIGOEMSTOCK.VALOR_REVENDA = Convert.ToDouble(tbvalorrevenda.Value);
                    ARTIGOEMSTOCK.VALOR_VENDA = Convert.ToDouble(tbvalorvenda.Value);
                    ARTIGOEMSTOCK.QTD_DISPONIVEL = Convert.ToInt32(tbqtddisponivel.Value);
                    ARTIGOEMSTOCK.QTD_MINIMA_STOCK = Convert.ToInt32(tbqtdminima.Value);

                    DC.SubmitChanges();
                    sucesso.Visible = sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Artigo actualizado com êxito!";
                }
                catch (Exception ex)
                {

                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = "";
            tbqtddisponivel.Text = "";
            tbqtdminima.Text = "";
            tbvalorcusto.Text = "";
            tbvalorrevenda.Text = "";
            tbvalorvenda.Text = "";
        }
    }
}
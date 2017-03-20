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
    public partial class MotorPesquisa : Telerik.Web.UI.RadAjaxPage
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

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbpesquisa.Text))
                {
                    if (ddlpesquisa.SelectedItem.Text == "Código O.R.")
                    {
                        var carregaOR = from o in DC.Ordem_Reparacaos
                                        where o.CODIGO == tbpesquisa.Text
                                        select new
                                            {
                                                CODIGO = o.CODIGO,
                                                LOJA = o.Loja.NOME,
                                                ESTADO = o.Ordem_Reparacao_Estado.DESCRICAO,
                                                DATA_REGISTO = o.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                                DATA_ULTIMA_MODIFICACAO = o.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                                ID = o.ID
                                            };

                        listagemORCodigo.DataSourceID = "";
                        listagemORCodigo.DataSource = carregaOR;
                        listagemORCodigo.Rebind();

                    }

                    if (ddlpesquisa.SelectedItem.Text == "Código Cliente")
                    {
                        var carregaCli = from cli in DC.Parceiros
                                         where (cli.ID_TIPO_PARCEIRO == 1 || cli.ID_TIPO_PARCEIRO == 2 || cli.ID_TIPO_PARCEIRO == 4) && cli.CODIGO == tbpesquisa.Text
                                         select new
                                         {
                                             CODIGO = cli.CODIGO,
                                             NOME = cli.NOME,
                                             MORADA = cli.MORADA,
                                             CODPOSTAL = cli.CODPOSTAL,
                                             LOCALIDADE = cli.LOCALIDADE,
                                             TELEFONE = cli.TELEFONE,
                                             EMAIL = cli.EMAIL,
                                             DATA_REGISTO = cli.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                             DATA_ULTIMA_MODIFICACAO = cli.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                             ID = cli.ID
                                         };
                        listagemORCliente.DataSourceID = "";
                        listagemORCliente.DataSource = carregaCli;
                        listagemORCliente.Rebind();

                    }

                    if (ddlpesquisa.SelectedItem.Text == "Nome Cliente")
                    {
                        var carregaCliente = from cli in DC.Ordem_Reparacaos
                                             join parceiro in DC.Parceiros on cli.USERID equals parceiro.USERID
                                             join equip in DC.Equipamento_Avariados on cli.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                             where (parceiro.ID_TIPO_PARCEIRO == 1 || parceiro.ID_TIPO_PARCEIRO == 2 || parceiro.ID_TIPO_PARCEIRO == 4) && parceiro.NOME.Contains(tbpesquisa.Text)
                                             select new
                                             {

                                                 NOME = parceiro.NOME,
                                                 TELEFONE = parceiro.TELEFONE,
                                                 EMAIL = parceiro.EMAIL,
                                                 MORADA = parceiro.MORADA,
                                                 CODPOSTAL = parceiro.CODPOSTAL,
                                                 LOCALIDADE = parceiro.LOCALIDADE,
                                                 IMEI = equip.IMEI,
                                                 NOVOIMEI = equip.NOVO_IMEI,
                                                 MARCA = equip.Marca.DESCRICAO,
                                                 MODELO = equip.Modelo.DESCRICAO,
                                                 DATA_REGISTO = parceiro.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                                 DATA_ULTIMA_MODIFICACAO = parceiro.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                                 LOJA = cli.Loja.NOME,
                                                 ESTADO = cli.Ordem_Reparacao_Estado.DESCRICAO,
                                                 ID = cli.ID,
                                                 CODIGO_OR = cli.CODIGO,
                                                 ATRIBUIDA = cli.ATRIBUIDA.Value,
                                                 IDESTADO = cli.ID_ESTADO
                                             };


                        listagemOREquipAvariado.DataSourceID = "";
                        listagemOREquipAvariado.DataSource = carregaCliente;
                        listagemOREquipAvariado.Rebind();



                    }

                    if (ddlpesquisa.SelectedItem.Text == "Contacto Cliente")
                    {
                        var carregaCliente = from cli in DC.Ordem_Reparacaos
                                             join parceiro in DC.Parceiros on cli.USERID equals parceiro.USERID
                                             join equip in DC.Equipamento_Avariados on cli.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                             where (parceiro.ID_TIPO_PARCEIRO == 1 || parceiro.ID_TIPO_PARCEIRO == 2 || parceiro.ID_TIPO_PARCEIRO == 4) && parceiro.TELEFONE.Contains(tbpesquisa.Text)
                                             select new
                                             {

                                                 NOME = parceiro.NOME,
                                                 TELEFONE = parceiro.TELEFONE,
                                                 EMAIL = parceiro.EMAIL,
                                                 MORADA = parceiro.MORADA,
                                                 CODPOSTAL = parceiro.CODPOSTAL,
                                                 LOCALIDADE = parceiro.LOCALIDADE,
                                                 IMEI = equip.IMEI,
                                                 NOVOIMEI = equip.NOVO_IMEI,
                                                 MARCA = equip.Marca.DESCRICAO,
                                                 MODELO = equip.Modelo.DESCRICAO,
                                                 DATA_REGISTO = parceiro.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                                 DATA_ULTIMA_MODIFICACAO = parceiro.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                                 LOJA = cli.Loja.NOME,
                                                 ESTADO = cli.Ordem_Reparacao_Estado.DESCRICAO,
                                                 ID = cli.ID,
                                                 CODIGO_OR = cli.CODIGO,
                                                 ATRIBUIDA = cli.ATRIBUIDA.Value,
                                                 IDESTADO = cli.ID_ESTADO
                                             };


                        listagemOREquipAvariado.DataSourceID = "";
                        listagemOREquipAvariado.DataSource = carregaCliente;
                        listagemOREquipAvariado.Rebind();



                    }

                    if (ddlpesquisa.SelectedItem.Text == "Imei Equipamento Avariado")
                    {
                        var carregaCliente = from cli in DC.Ordem_Reparacaos
                                             join parceiro in DC.Parceiros on cli.USERID equals parceiro.USERID
                                             join equip in DC.Equipamento_Avariados on cli.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                             where (parceiro.ID_TIPO_PARCEIRO == 1 || parceiro.ID_TIPO_PARCEIRO == 2 || parceiro.ID_TIPO_PARCEIRO == 4) && equip.IMEI.Contains(tbpesquisa.Text)
                                             select new
                                             {

                                                 NOME = parceiro.NOME,
                                                 TELEFONE = parceiro.TELEFONE,
                                                 EMAIL = parceiro.EMAIL,
                                                 MORADA = parceiro.MORADA,
                                                 CODPOSTAL = parceiro.CODPOSTAL,
                                                 LOCALIDADE = parceiro.LOCALIDADE,
                                                 IMEI = equip.IMEI,
                                                 NOVOIMEI = equip.NOVO_IMEI,
                                                 MARCA = equip.Marca.DESCRICAO,
                                                 MODELO = equip.Modelo.DESCRICAO,
                                                 DATA_REGISTO = parceiro.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                                 DATA_ULTIMA_MODIFICACAO = parceiro.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                                 LOJA = cli.Loja.NOME,
                                                 ESTADO = cli.Ordem_Reparacao_Estado.DESCRICAO,
                                                 ID = cli.ID,
                                                 CODIGO_OR = cli.CODIGO,
                                                 ATRIBUIDA = cli.ATRIBUIDA.Value,
                                                 IDESTADO = cli.ID_ESTADO
                                             };



                        listagemOREquipAvariado.DataSourceID = "";
                        listagemOREquipAvariado.DataSource = carregaCliente;
                        listagemOREquipAvariado.Rebind();


                    }

                    if (ddlpesquisa.SelectedItem.Text == "Novo Imei Equipamento Reparado")
                    {
                        var carregaCliente = from cli in DC.Ordem_Reparacaos
                                             join parceiro in DC.Parceiros on cli.USERID equals parceiro.USERID
                                             join equip in DC.Equipamento_Avariados on cli.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                             where (parceiro.ID_TIPO_PARCEIRO == 1 || parceiro.ID_TIPO_PARCEIRO == 2 || parceiro.ID_TIPO_PARCEIRO == 4) && equip.NOVO_IMEI.Contains(tbpesquisa.Text)
                                             select new
                                             {

                                                 NOME = parceiro.NOME,
                                                 TELEFONE = parceiro.TELEFONE,
                                                 EMAIL = parceiro.EMAIL,
                                                 MORADA = parceiro.MORADA,
                                                 CODPOSTAL = parceiro.CODPOSTAL,
                                                 LOCALIDADE = parceiro.LOCALIDADE,
                                                 IMEI = equip.IMEI,
                                                 NOVOIMEI = equip.NOVO_IMEI,
                                                 MARCA = equip.Marca.DESCRICAO,
                                                 MODELO = equip.Modelo.DESCRICAO,
                                                 DATA_REGISTO = parceiro.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                                 DATA_ULTIMA_MODIFICACAO = parceiro.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                                 LOJA = cli.Loja.NOME,
                                                 ESTADO = cli.Ordem_Reparacao_Estado.DESCRICAO,
                                                 ID = cli.ID,
                                                 CODIGO_OR = cli.CODIGO,
                                                 ATRIBUIDA = cli.ATRIBUIDA.Value,
                                                 IDESTADO = cli.ID_ESTADO
                                             };



                        listagemOREquipAvariado.DataSourceID = "";
                        listagemOREquipAvariado.DataSource = carregaCliente;
                        listagemOREquipAvariado.Rebind();


                    }

                    if (ddlpesquisa.SelectedItem.Text == "Marca Equipamento Avariado")
                    {
                        var carregaCliente = from cli in DC.Ordem_Reparacaos
                                             join parceiro in DC.Parceiros on cli.USERID equals parceiro.USERID
                                             join equip in DC.Equipamento_Avariados on cli.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                             where (parceiro.ID_TIPO_PARCEIRO == 1 || parceiro.ID_TIPO_PARCEIRO == 2 || parceiro.ID_TIPO_PARCEIRO == 4) && equip.Marca.DESCRICAO.Contains(tbpesquisa.Text)
                                             select new
                                             {

                                                 NOME = parceiro.NOME,
                                                 TELEFONE = parceiro.TELEFONE,
                                                 EMAIL = parceiro.EMAIL,
                                                 MORADA = parceiro.MORADA,
                                                 CODPOSTAL = parceiro.CODPOSTAL,
                                                 LOCALIDADE = parceiro.LOCALIDADE,
                                                 IMEI = equip.IMEI,
                                                 NOVOIMEI = equip.NOVO_IMEI,
                                                 MARCA = equip.Marca.DESCRICAO,
                                                 MODELO = equip.Modelo.DESCRICAO,
                                                 DATA_REGISTO = parceiro.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                                 DATA_ULTIMA_MODIFICACAO = parceiro.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                                 LOJA = cli.Loja.NOME,
                                                 ESTADO = cli.Ordem_Reparacao_Estado.DESCRICAO,
                                                 ID = cli.ID,
                                                 CODIGO_OR = cli.CODIGO,
                                                 ATRIBUIDA = cli.ATRIBUIDA.Value,
                                                 IDESTADO = cli.ID_ESTADO
                                             };


                        listagemOREquipAvariado.DataSourceID = "";
                        listagemOREquipAvariado.DataSource = carregaCliente;
                        listagemOREquipAvariado.Rebind();



                    }

                    if (ddlpesquisa.SelectedItem.Text == "Modelo Equipamento Avariado")
                    {
                        var carregaCliente = from cli in DC.Ordem_Reparacaos
                                             join parceiro in DC.Parceiros on cli.USERID equals parceiro.USERID
                                             join equip in DC.Equipamento_Avariados on cli.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                             where (parceiro.ID_TIPO_PARCEIRO == 1 || parceiro.ID_TIPO_PARCEIRO == 2 || parceiro.ID_TIPO_PARCEIRO == 4) && equip.Modelo.DESCRICAO.Contains(tbpesquisa.Text)
                                             select new
                                             {

                                                 NOME = parceiro.NOME,
                                                 TELEFONE = parceiro.TELEFONE,
                                                 EMAIL = parceiro.EMAIL,
                                                 MORADA = parceiro.MORADA,
                                                 CODPOSTAL = parceiro.CODPOSTAL,
                                                 LOCALIDADE = parceiro.LOCALIDADE,
                                                 IMEI = equip.IMEI,
                                                 NOVOIMEI = equip.NOVO_IMEI,
                                                 MARCA = equip.Marca.DESCRICAO,
                                                 MODELO = equip.Modelo.DESCRICAO,
                                                 DATA_REGISTO = parceiro.DATA_REGISTO.Value.ToShortDateString().ToString(),
                                                 DATA_ULTIMA_MODIFICACAO = parceiro.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                                 LOJA = cli.Loja.NOME,
                                                 ESTADO = cli.Ordem_Reparacao_Estado.DESCRICAO,
                                                 ID = cli.ID,
                                                 CODIGO_OR = cli.CODIGO,
                                                 ATRIBUIDA = cli.ATRIBUIDA.Value,
                                                 IDESTADO = cli.ID_ESTADO
                                             };



                        listagemOREquipAvariado.DataSourceID = "";
                        listagemOREquipAvariado.DataSource = carregaCliente;
                        listagemOREquipAvariado.Rebind();


                    }


                }
                else
                {
                    erro.Style.Add("display", "block");
                    errorMessage.Style.Add("display", "block");
                    errorMessage.InnerHtml = "O campo Termo de Pesquisa é de preencchimento obrigatório!";
                    tbpesquisa.Focus();
                    return;
                }

            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbpesquisa.Text = "";
            ddlpesquisa.ClearSelection();
        }

        protected void listagemORCodigo_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "DetalheOR.aspx?ID=" + val1;
            }
        }

        protected void listagemORCliente_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "ListagemGeralClientesDetalhe.aspx?ID=" + val1;
            }
        }

        protected void listagemOREquipAvariado_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "DetalheOR.aspx?ID=" + val1;
            }
        }

        protected void listagemOREquipAvariado_ItemCommand(object sender, GridCommandEventArgs e)
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

                if (regra == "Lojista" || regra == "Cliente" || regra == "Reparador")
                    Response.Redirect("Default.aspx", false);

                if (regra == "Administrador" || regra == "SuperAdmin")
                {
                    try
                    {
                        if (e.CommandName == "ALTERAR")
                        {
                            var item = (GridDataItem)e.Item;
                            string val1 = item["CODIGO_OR"].Text;


                            LINQ_DB.Ordem_Reparacao_Atribuicao NOVAATRIBUICAOOR = new LINQ_DB.Ordem_Reparacao_Atribuicao();

                            var procura = from atr in DC.Ordem_Reparacaos
                                          where atr.CODIGO == val1
                                          select atr;

                            foreach (var itemOR in procura)
                            {
                                if (itemOR.ATRIBUIDA == false)
                                {
                                    NOVAATRIBUICAOOR.ID_ORDEM_REPARACAO = itemOR.ID;
                                    NOVAATRIBUICAOOR.USERID = userid;
                                    NOVAATRIBUICAOOR.DATA_REGISTO = DateTime.Now;
                                    NOVAATRIBUICAOOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    DC.Ordem_Reparacao_Atribuicaos.InsertOnSubmit(NOVAATRIBUICAOOR);
                                    DC.SubmitChanges();

                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var pesquisaOR = from ors in DC.Ordem_Reparacaos
                                                     where ors.CODIGO == val1
                                                     select ors;
                                    ACTUALIZAOR = pesquisaOR.First();

                                    ACTUALIZAOR.ID_ESTADO = 2;
                                    ACTUALIZAOR.ATRIBUIDA = true;
                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    DC.SubmitChanges();
                                    Response.Redirect("EditarOrdemReparacao.aspx?ID=" + itemOR.ID, false);
                                }
                                else
                                {
                                    if (itemOR.ID_ESTADO == 2 || itemOR.ID_ESTADO == 3 || itemOR.ID_ESTADO == 4)
                                        Response.Redirect("EditarOrdemReparacao.aspx?ID=" + itemOR.ID, false);
                                    else
                                        ShowAlertMessage("A ordem de reparação já se encontra concluída/fechada");
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

                if (regra == "Tecnico")
                {
                    try
                    {
                        if (e.CommandName == "ALTERAR")
                        {
                            var item = (GridDataItem)e.Item;
                            string val1 = item["CODIGO_OR"].Text;

                            LINQ_DB.Ordem_Reparacao_Atribuicao NOVAATRIBUICAOOR = new LINQ_DB.Ordem_Reparacao_Atribuicao();

                            var procura = from atr in DC.Ordem_Reparacaos
                                          where atr.CODIGO == val1
                                          select atr;

                            foreach (var itemORR in procura)
                            {
                                if (itemORR.ATRIBUIDA == false)
                                {

                                    NOVAATRIBUICAOOR.ID_ORDEM_REPARACAO = itemORR.ID;
                                    NOVAATRIBUICAOOR.USERID = userid;
                                    NOVAATRIBUICAOOR.DATA_REGISTO = DateTime.Now;
                                    NOVAATRIBUICAOOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    DC.Ordem_Reparacao_Atribuicaos.InsertOnSubmit(NOVAATRIBUICAOOR);
                                    DC.SubmitChanges();

                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var pesquisaOR = from ors in DC.Ordem_Reparacaos
                                                     where ors.CODIGO == val1
                                                     select ors;
                                    ACTUALIZAOR = pesquisaOR.First();

                                    ACTUALIZAOR.ID_ESTADO = 2;
                                    ACTUALIZAOR.ATRIBUIDA = true;
                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    DC.SubmitChanges();
                                    Response.Redirect("EditarOrdemReparacao.aspx?ID=" + itemORR.ID, false);

                                }
                                else
                                {
                                    if (itemORR.ID_ESTADO == 2 || itemORR.ID_ESTADO == 3 || itemORR.ID_ESTADO == 4)
                                        Response.Redirect("EditarOrdemReparacao.aspx?ID=" + itemORR.ID, false);
                                    else
                                        ShowAlertMessage("A ordem de reparação já se encontra concluída/fechada");
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
            }

            else
                Response.Redirect("~/Default.aspx", true);

        }

        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }
    }
}
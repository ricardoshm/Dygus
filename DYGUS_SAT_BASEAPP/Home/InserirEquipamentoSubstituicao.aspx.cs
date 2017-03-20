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
    public partial class InserirEquipamentoSubstituicao : Telerik.Web.UI.RadAjaxPage
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
            //(Page.Master.FindControl("dados") as HtmlControl).Attributes.Add("class", "has-sub active");
            //(Page.Master.FindControl("eqsinsert") as HtmlControl).Attributes.Add("class", "active");

            if (!Page.IsPostBack)
            {
                //carregarMarcas();
                carregarEstados();

                int codNovoCliente = 0;

                try
                {
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "ES"
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
                        tbcodequipamento.Text = numeroCliente;
                    }
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
                             where conf.INICIAL == "ES"
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
                    tbcodequipamento.Text = numeroCliente;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void carregarEstados()
        {
            try
            {
                var pesquisa = from marca in DC.Equipamentos_Substituicao_Disponibilidades
                               select marca;

                ddlEstado.DataTextField = "DESCRICAO";
                ddlEstado.DataValueField = "ID";
                ddlEstado.DataSource = pesquisa;
                ddlEstado.DataBind();
                ddlEstado.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        //protected void carregarMarcas()
        //{
        //    try
        //    {
        //        var pesquisa = from marca in DC.Marcas
        //                       select marca;

        //        ddlmarca.DataTextField = "DESCRICAO";
        //        ddlmarca.DataValueField = "ID";
        //        ddlmarca.DataSource = pesquisa;
        //        ddlmarca.DataBind();
        //        ddlmarca.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}


        protected bool validaCampos()
        {
            string imei = "";
            imei = tbimei.Text;

            if (String.IsNullOrEmpty(tbmarca.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Marca de Equipamento é obrigatório!";
                return true;
            }

            if (String.IsNullOrEmpty(tbmodelo.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Modelo de Equipamento é obrigatório!";
                return true;
            }

            if (String.IsNullOrEmpty(imei))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo IMEI de Equipamento é obrigatório!";
                return true;
            }

            if (tbvalorequip.Value == null)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Valor de Equipamento é obrigatório!";
                return true;
            }

            if (ddlEstado.SelectedItem.Text == "Por favor seleccione...")
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Estado de Equipamento é obrigatório!";
                return true;
            }

            return true;
        }


        protected void btnGrava_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                try
                {
                    int marcaexiste = 0;

                    var marcas = from mm in DC.Marcas
                                 where mm.DESCRICAO == tbmarca.Text
                                 select mm;

                    marcaexiste = Enumerable.Count(marcas);

                    int modeloexiste = 0;

                    var modelos = from m in DC.Modelos
                                  where m.DESCRICAO == tbmodelo.Text
                                  select m;

                    modeloexiste = Enumerable.Count(modelos);

                    LINQ_DB.Equipamentos_Substituicao NOVOEQUIPSUBST = new LINQ_DB.Equipamentos_Substituicao();

                    NOVOEQUIPSUBST.CODIGO = tbcodequipamento.Text;

                    if (marcaexiste == 0)
                    {
                        LINQ_DB.Marca NOVAMARCA = new LINQ_DB.Marca();
                        NOVAMARCA.DESCRICAO = tbmarca.Text;
                        DC.Marcas.InsertOnSubmit(NOVAMARCA);
                        DC.SubmitChanges();
                    }

                    if (modeloexiste == 0)
                    {
                        LINQ_DB.Modelo MODELO = new LINQ_DB.Modelo();

                        MODELO.DESCRICAO = tbmodelo.Text;

                        var procuramarca = from marcasexiste in DC.Marcas
                                           where marcasexiste.DESCRICAO == tbmarca.Text
                                           select marcasexiste;

                        foreach (var item in procuramarca)
                        {
                            MODELO.ID_MARCA = item.ID;
                        }
                        DC.Modelos.InsertOnSubmit(MODELO);
                        DC.SubmitChanges();

                        NOVOEQUIPSUBST.ID_MARCA = MODELO.ID_MARCA;
                        NOVOEQUIPSUBST.ID_MODELO = MODELO.ID;
                    }

                    if (modeloexiste == 1)
                    {
                        LINQ_DB.Modelo MODELO = new LINQ_DB.Modelo();

                        var modelosProcura = from mod in DC.Modelos
                                             where mod.DESCRICAO == tbmodelo.Text
                                             select mod;

                        foreach (var item in modelosProcura)
                        {
                            var procuramarca = from marcasexistentes in DC.Marcas
                                               where marcasexistentes.DESCRICAO == tbmarca.Text
                                               select marcasexistentes;

                            foreach (var itemmodelo in procuramarca)
                            {
                                NOVOEQUIPSUBST.ID_MARCA = itemmodelo.ID;
                            }
                            NOVOEQUIPSUBST.ID_MODELO = item.ID;
                        }
                    }

                    NOVOEQUIPSUBST.IMEI = tbimei.Text;
                    NOVOEQUIPSUBST.BATERIA = rbbateria.SelectedToggleState.Selected;
                    NOVOEQUIPSUBST.CARTAO_MEMORIA = rbcartaomem.SelectedToggleState.Selected;
                    NOVOEQUIPSUBST.CARREGADOR = rbcarregador.SelectedToggleState.Selected;
                    NOVOEQUIPSUBST.VALOR = Convert.ToDouble(tbvalorequip.Text);
                    NOVOEQUIPSUBST.DATA_REGISTO = DateTime.Now;
                    NOVOEQUIPSUBST.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    if (!String.IsNullOrEmpty(tbobs.Text))
                        NOVOEQUIPSUBST.OBSERVACAOES = tbobs.Text;
                    else
                        NOVOEQUIPSUBST.OBSERVACAOES = "N/D";
                    NOVOEQUIPSUBST.ID_DISPONIBILIDADE = Convert.ToInt32(ddlEstado.SelectedItem.Value);
                    NOVOEQUIPSUBST.ACTIVO = true;

                    DC.Equipamentos_Substituicaos.InsertOnSubmit(NOVOEQUIPSUBST);
                    DC.SubmitChanges();

                    LINQ_DB.Configuracao NOVOCODIGOEQUIPSUBST = new LINQ_DB.Configuracao();
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "ES"
                                 select conf;
                    NOVOCODIGOEQUIPSUBST = config.First();
                    NOVOCODIGOEQUIPSUBST.CODIGO = NOVOCODIGOEQUIPSUBST.CODIGO + 1;
                    DC.SubmitChanges();

                    listagemequipamentos.Rebind();
                    limpaCampos();
                    sucesso.Visible = sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Equipamento de Substituição " + NOVOEQUIPSUBST.CODIGO.ToString() + " registado com êxito!";
                    erro.Visible = errorMessage.Visible = false;
                    recalculaCodCliente();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void limpaCampos()
        {
            tbmarca.Text = "";
            tbmodelo.Text = "";
            tbimei.Text = tbobs.Text = "";
            ddlEstado.ClearSelection();
            tbvalorequip.Text = "";
            rbbateria.SelectedToggleState.Selected = false;
            rbcarregador.SelectedToggleState.Selected = false;
            rbcartaomem.SelectedToggleState.Selected = false;
            rbbateria.ClearSelection();
            rbcarregador.ClearSelection();
            rbcartaomem.ClearSelection();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbmarca.Text = "";
            tbmodelo.Text = "";
            tbimei.Text = tbobs.Text = "";
            ddlEstado.ClearSelection();
            tbvalorequip.Text = "";
            rbbateria.ClearSelection();
            rbcarregador.ClearSelection();
            rbcartaomem.ClearSelection();
        }

        protected void listagemequipamentos_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
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
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

    }
}
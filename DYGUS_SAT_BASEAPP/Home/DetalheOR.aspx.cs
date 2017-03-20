using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class DetalheOR : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        public static Guid userid = new Guid();
        public static Guid Role = new Guid();
        public static string UserName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
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

                if (regra == "Reparador")
                    Response.Redirect("Default.aspx", false);

                string id = "";

                if (Request.QueryString["ID"] != null)
                {
                    id = Request.QueryString["ID"];
                }
                else
                {
                    Response.Redirect("PesquisaGeralOrdensReparacao.aspx", true);
                    return;
                }

                if (!Page.IsPostBack)
                    carregaOR();

            }
            else
                Response.Redirect("~/Default.aspx", true);

        }

        protected void carregaOR()
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarTecnicoOrdemReparacao.aspx", true);
                return;
            }

            int conta = (from ordem in DC.Ordem_Reparacaos
                         where ordem.ID == Convert.ToInt32(id)
                         select ordem).Count();

            if (conta > 0)
            {
                var ordemRep = from ordem in DC.Ordem_Reparacaos
                               where ordem.ID == Convert.ToInt32(id)
                               select ordem;

                foreach (var item in ordemRep)
                {
                    tbcodordemreparacao.Text = item.CODIGO;
                    tbdataRegisto.Text = item.DATA_REGISTO.HasValue ? item.DATA_REGISTO.Value.ToShortDateString() : null;
                    tbEstadoActualReparacao.Text = item.Ordem_Reparacao_Estado.DESCRICAO;
                }

                var ordemRepCliente = from cliente in DC.Parceiros
                                      join ordem in DC.Ordem_Reparacaos on cliente.USERID equals ordem.USERID
                                      where ordem.ID == Convert.ToInt32(id) && ordem.USERID == cliente.USERID
                                      select cliente;

                foreach (var item in ordemRepCliente)
                {
                    tbcodcliente.Text = item.CODIGO;
                    tbnome.Text = item.NOME;
                    tbmorada.Text = item.MORADA;
                    tbcodpostal.Text = item.CODPOSTAL;
                    tblocalidade.Text = item.LOCALIDADE;
                    tbcontacto.Text = item.TELEFONE;
                    tbemail.Text = item.EMAIL;
                    tbnif.Text = item.NIF.ToString();
                    tbobs.Text = item.OBSERVACOES;
                    tbdataRegistocliente.Text = item.DATA_REGISTO.HasValue ? item.DATA_REGISTO.Value.ToShortDateString() : null;
                }

                var equipAvariado = from ordensRep in DC.Ordem_Reparacaos
                                    join equip in DC.Equipamento_Avariados on ordensRep.ID_EQUIPAMENTO_AVARIADO equals equip.ID
                                    where ordensRep.ID == Convert.ToInt32(id) && ordensRep.ID_EQUIPAMENTO_AVARIADO == equip.ID
                                    select ordensRep;

                foreach (var item in equipAvariado)
                {
                    tbmarca.Text = item.Equipamento_Avariado.Marca.DESCRICAO;
                    tbmodelo.Text = item.Equipamento_Avariado.Modelo.DESCRICAO;
                    tbimei.Text = item.Equipamento_Avariado.IMEI;
                    tbcodseguranca.Text = item.Equipamento_Avariado.COD_SEGURANCA.ToString();
                    tbdescricaoProblemaEquipAvariado.Text = item.DESCRICAO_DETALHADA_PROBLEMA;
                    tbtiming.Text = item.Equipamento_Avariado_TimingReparacao.DESCRICAO;
                    tbdataprevistareparacao.Text = item.DATA_PREVISTA_CONCLUSAO.HasValue ? item.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString() : null;
                    tbnovoimei.Text = item.Equipamento_Avariado.NOVO_IMEI;
                }

                var trabAdicional = from ordensRep in DC.Ordem_Reparacaos
                                    join trab in DC.Equipamento_Avariado_TrabalhosAdicionais on ordensRep.ID_TRABALHOS_ADICIONAIS equals trab.ID
                                    where ordensRep.ID == Convert.ToInt32(id) && ordensRep.ID_TRABALHOS_ADICIONAIS == trab.ID
                                    select ordensRep;

                foreach (var item in trabAdicional)
                {
                    rbresetsoftware.SelectedToggleState.Selected = item.Equipamento_Avariado_TrabalhosAdicionai.RESET_SOFTWARE.Value;
                    rbactualizacaosoft.SelectedToggleState.Selected = item.Equipamento_Avariado_TrabalhosAdicionai.ACTUALIZACAO_SOFTWARE.Value;
                    rbhardware.SelectedToggleState.Selected = item.Equipamento_Avariado_TrabalhosAdicionai.REPARACAO_HARDWARE.Value;
                    rbbackupinfo.SelectedToggleState.Selected = item.Equipamento_Avariado_TrabalhosAdicionai.BACKUP_INFORMACAO.Value;
                    rbLimpeza.SelectedToggleState.Selected = item.Equipamento_Avariado_TrabalhosAdicionai.LIMPEZA_GERAL.Value;
                    tbObsTrabalhosRealizar.Text = item.Equipamento_Avariado_TrabalhosAdicionai.OBSERVACOES;
                }

                var bloqueio = from ordensRep in DC.Ordem_Reparacaos
                               join bloq in DC.Equipamento_Avariado_Bloqueios on ordensRep.ID_ESTADO_BLOQUEIO equals bloq.ID
                               where ordensRep.ID == Convert.ToInt32(id) && ordensRep.ID_ESTADO_BLOQUEIO == bloq.ID
                               select ordensRep;

                foreach (var item in bloqueio)
                {
                    rbbloqueiooperadora.SelectedToggleState.Selected = item.Equipamento_Avariado_Bloqueio.BLOQUEADO.Value;
                    if (item.Equipamento_Avariado_Bloqueio.ID_OPERADORA != null)
                        tboperadora.Text = item.Equipamento_Avariado_Bloqueio.Operadora.DESCRICAO;
                    else
                        tboperadora.Text = "N/D";
                    tbobsEstadoEquipamento.Text = item.Equipamento_Avariado_Bloqueio.OBSERVACOES;
                }

                var acessorios = from ordensRep in DC.Ordem_Reparacaos
                                 join ac in DC.Equipamento_Avariado_Acessorios on ordensRep.ID_ACESSORIOS equals ac.ID
                                 where ordensRep.ID == Convert.ToInt32(id) && ordensRep.ID_ACESSORIOS == ac.ID
                                 select ordensRep;

                foreach (var item in acessorios)
                {
                    rbbateriaequipavariado.SelectedToggleState.Selected = item.Equipamento_Avariado_Acessorio.BATERIA.Value;
                    rbcartaosimequipavariado.SelectedToggleState.Selected = item.Equipamento_Avariado_Acessorio.CARTAO_SIM.Value;
                    rbbolsaequipavariado.SelectedToggleState.Selected = item.Equipamento_Avariado_Acessorio.BOLSA.Value;
                    rbcartaomemoriaequipavariado.SelectedToggleState.Selected = item.Equipamento_Avariado_Acessorio.CARTAO_MEM.Value;
                    rbcarregadorequipavariado.SelectedToggleState.Selected = item.Equipamento_Avariado_Acessorio.CARREGADOR.Value;
                    tboutrosequipavariado.Text = item.Equipamento_Avariado_Acessorio.OUTROS;
                    tbobsequipavariado.Text = item.Equipamento_Avariado_Acessorio.OBSERVACOES;
                    rbcaixa.SelectedToggleState.Selected = item.Equipamento_Avariado_Acessorio.CAIXA.Value;
                }

                var garantia = from ordensRep in DC.Ordem_Reparacaos
                               join gg in DC.Equipamento_Avariado_GarantiaTipos on ordensRep.ID_TIPO_GARANTIA equals gg.ID
                               where ordensRep.ID == Convert.ToInt32(id) && ordensRep.ID_TIPO_GARANTIA == gg.ID
                               select ordensRep;

                foreach (var item in garantia)
                {
                    if (item.Equipamento_Avariado_GarantiaTipo.ID_TIPO_GARANTIA != null)
                        tbtipoGarantia.Text = item.Equipamento_Avariado_GarantiaTipo.Equipamento_Avariado_Garantia.DESCRICAO;
                    else
                        tbtipoGarantia.Text = "N/D";
                    tbobsGarantiaequipavariado.Text = item.Equipamento_Avariado_GarantiaTipo.OBSERVACOES;
                }

                var procuraAtribuicao = from at in DC.Ordem_Reparacaos
                                        join atrib in DC.Ordem_Reparacao_Atribuicaos on at.ID equals atrib.ID_ORDEM_REPARACAO
                                        join parceiro in DC.Parceiros on atrib.USERID equals parceiro.USERID
                                        where at.ID == Convert.ToInt32(id)
                                        select new
                                        {
                                            NOME = parceiro.NOME
                                        };

                foreach (var item in procuraAtribuicao)
                {
                    tbnometecnicoreparador.Text = item.NOME;
                }

                var procuraAtribuicaoB = from at in DC.Ordem_Reparacaos
                                         join atrib in DC.Ordem_Reparacao_Atribuicaos on at.ID equals atrib.ID_ORDEM_REPARACAO
                                         join parceiro in DC.Funcionarios on atrib.USERID equals parceiro.USERID
                                         where at.ID == Convert.ToInt32(id)
                                         select new
                                         {
                                             NOME = parceiro.NOME
                                         };

                foreach (var item in procuraAtribuicaoB)
                {
                    tbnometecnicoreparador.Text = item.NOME;
                }

                if (tbnometecnicoreparador.Text == "")
                {
                    var procuraAtribuicaoC = from at in DC.Ordem_Reparacaos
                                             join atrib in DC.Ordem_Reparacao_Atribuicaos on at.ID equals atrib.ID_ORDEM_REPARACAO
                                             join admin in DC.aspnet_Memberships on atrib.USERID equals admin.UserId
                                             where at.ID == Convert.ToInt32(id)
                                             select new
                                             {
                                                 NOME = admin.LoweredEmail
                                             };

                    foreach (var item in procuraAtribuicaoC)
                    {
                        tbnometecnicoreparador.Text = item.NOME;
                    }
                }
            }
            else
                Response.Redirect("PesquisaGeralOrdensReparacao.aspx", false);
        }

        protected void listagemartigosutilizados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarOrdemReparacao.aspx", true);
                return;
            }

            try
            {
                var consultaArtigos = from artigo in DC.Ordem_Reparacao_Artigos
                                      join artigos in DC.Artigos on artigo.ID_ARTIGO equals artigos.ID
                                      where artigo.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                      select new
                                      {
                                          CODIGO = artigos.CODIGO,
                                          DESCRICAO = artigos.DESCRICAO,
                                          QTD_ARTIGO = artigo.QTD_ARTIGO,
                                      };

                listagemartigosutilizados.DataSourceID = "";
                listagemartigosutilizados.DataSource = consultaArtigos;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }

        protected void listagemmoutilizada_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarOrdemReparacao.aspx", true);
                return;
            }

            try
            {
                var consultaMO = from mo in DC.Ordem_Reparacao_MaoObras
                                 where mo.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                 select new
                                 {
                                     DESC_MAO_OBRA = mo.DESCRICAO,
                                     VALOR_MAO_OBRA = mo.VALOR.Value,
                                     OBS_MAO_OBRA = mo.OBSERVACOES
                                 };

                listagemmoutilizada.DataSourceID = "";
                listagemmoutilizada.DataSource = consultaMO;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }


        }
    }
}
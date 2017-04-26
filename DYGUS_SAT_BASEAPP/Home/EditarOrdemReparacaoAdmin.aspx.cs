using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class EditarOrdemReparacaoAdmin : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();
        protected System.Timers.Timer timer2 = new System.Timers.Timer();
        protected System.Timers.Timer timer3 = new System.Timers.Timer();
        protected System.Timers.Timer timer4 = new System.Timers.Timer();
        public int idOrcamento = 0;
        Guid userid = new Guid();
        string UserName = "";
        string emailUser = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Security.Principal.IPrincipal User = HttpContext.Current.User;



            if (User.Identity.IsAuthenticated == true)
            {
                try
                {
                    var us = from users in DC.aspnet_Memberships
                             where users.LoweredEmail == User.Identity.Name
                             select users;

                    foreach (var item in us)
                    {
                        userid = item.UserId;
                        emailUser = item.LoweredEmail;
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

                    Guid Role = new Guid();

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


                    if (regra != "SuperAdmin")
                    {
                        Response.Redirect("~/Default.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            else
                Response.Redirect("~/Default.aspx", true);



            if (Session["returnedValuesArtigos"] != null)
            {
                List<string> resultsArtigos = (List<string>)Session["returnedValuesArtigos"];
                tbcodartigo.Text += resultsArtigos[0];
                tbdescricaoArtigo.Text += resultsArtigos[1];
            }
            Session.Clear();


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


            if (!Page.IsPostBack)
            {
                carregaOR();
                carregaEstados();
            }

            var ordemRep = from ordem in DC.Ordem_Reparacaos
                           where ordem.ID == Convert.ToInt32(id)
                           select ordem;

            foreach (var item in ordemRep)
            {
                if (item.ID_ESTADO.Value < 4)
                    btnOrcamento.Visible = true;
                else
                    btnOrcamento.Visible = false;
            }

            var ordemRepOrc = from ordem in DC.Ordem_Reparacaos
                              join eq in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO.Value equals eq.ID
                              where ordem.ID == Convert.ToInt32(id)
                              select ordem;

            foreach (var item in ordemRep)
            {
                var Orc = from o in DC.Orcamentos
                          join eq in DC.Equipamento_Avariados on o.ID_EQUIPAMENTO_AVARIADO.Value equals eq.ID
                          select o;

                foreach (var itemo in Orc)
                {
                    if (itemo.ID > 0)
                    {
                        idOrcamento = itemo.ID;
                        btnConsultarOrcamento.Visible = true;
                    }
                    else
                        btnConsultarOrcamento.Visible = false;
                }

            }

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

            var ordemRep = from ordem in DC.Ordem_Reparacaos
                           where ordem.ID == Convert.ToInt32(id)
                           select ordem;

            foreach (var item in ordemRep)
            {
                tbcodordemreparacao.Text = item.CODIGO;
                tbdataRegisto.Text = item.DATA_REGISTO.HasValue ? item.DATA_REGISTO.Value.ToShortDateString() : null;
                tbEstadoActualReparacao.Text = item.Ordem_Reparacao_Estado.DESCRICAO;


                int conta = (from o in DC.Orcamentos
                             where o.ID_EQUIPAMENTO_AVARIADO.Value == item.ID_EQUIPAMENTO_AVARIADO.Value
                             select o).Count();
                if (conta > 0)
                {
                    valorOrcamento.Visible = true;
                    btnOrcamento.Visible = false;
                    btnOrcamento.Style.Add("display", "none");

                    var pesquisaorc = from o in DC.Orcamentos
                                      where o.ID_EQUIPAMENTO_AVARIADO.Value == item.ID_EQUIPAMENTO_AVARIADO.Value
                                      select o;

                    foreach (var itemorc in pesquisaorc)
                    {
                        tbvalororcamentado.Value = itemorc.VALOR_ORCAMENTO.Value;
                    }
                }
                else
                    valorOrcamento.Visible = false;
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
                idEquipAvariadoHD.Value = item.Equipamento_Avariado.ID.ToString();
                tbmarca.Text = item.Equipamento_Avariado.Marca.DESCRICAO;
                tbmodelo.Text = item.Equipamento_Avariado.Modelo.DESCRICAO;
                tbimei.Text = item.Equipamento_Avariado.IMEI;
                tbcodseguranca.Text = item.Equipamento_Avariado.COD_SEGURANCA.ToString();
                tbdescricaoProblemaEquipAvariado.Text = item.DESCRICAO_DETALHADA_PROBLEMA;
                tbtiming.Text = item.Equipamento_Avariado_TimingReparacao.DESCRICAO;
                tbvalorprevisto.Value = item.VALOR_PREVISTO_REPARACAO.Value;
                tbdataprevistareparacao.Text = item.DATA_PREVISTA_CONCLUSAO.HasValue ? item.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString() : null;
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
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            double somaValoresCustoArtigos = 0;
            double somaValoresVendaArtigos = 0;
            double somaValoresVendaMaoObra = 0;
            double valorFinalCustos = 0;
            double valorfinalVenda = 0;

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

            #region verificaperfil

            System.Security.Principal.IPrincipal User = HttpContext.Current.User;

            Guid userid = new Guid();
            string UserName = "";
            string emailUser = "";

            if (User.Identity.IsAuthenticated == true)
            {
                try
                {
                    var us = from users in DC.aspnet_Memberships
                             where users.LoweredEmail == User.Identity.Name
                             select users;

                    foreach (var item in us)
                    {
                        userid = item.UserId;
                        emailUser = item.LoweredEmail;
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

                    Guid Role = new Guid();

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

                    if (regra == "Tecnico" || regra == "Administrador" || regra == "SuperAdmin")
                    {


                        #region TECNICO

                        if (validaEstado())
                        {
                            #region REPARAÇÃO CONCLUIDA

                            if (ddlestado.SelectedItem.Text == "Reparação Concluída")
                            {
                                try
                                {
                                    if (!String.IsNullOrEmpty(tbnovoimei.Text))
                                    {
                                        LINQ_DB.Equipamento_Avariado ACTUALIZAEQUIP = new LINQ_DB.Equipamento_Avariado();

                                        var procuraEquipAva = from equip in DC.Equipamento_Avariados
                                                              where equip.ID == Convert.ToInt32(idEquipAvariadoHD.Value)
                                                              select equip;

                                        ACTUALIZAEQUIP = procuraEquipAva.First();
                                        ACTUALIZAEQUIP.NOVO_IMEI = tbnovoimei.Text;
                                        DC.SubmitChanges();
                                    }


                                    List<double> listaValoresArtigos = new List<double>();

                                    List<double> listaValoresMO = new List<double>();

                                    List<double> listaValoresCustoArtigo = new List<double>();

                                    List<double> listaValoresCustoManuais = new List<double>();

                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    ACTUALIZAOR.DATA_CONCLUSAO = DateTime.Now;

                                    var preco = from pp in DC.Ordem_Reparacao_Artigos
                                                where pp.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                                select pp;

                                    foreach (var item in preco)
                                    {
                                        var custo = from c in DC.Artigo_Stocks
                                                    where c.ID_ARTIGO == item.ID_ARTIGO
                                                    select c;

                                        foreach (var itemcusto in custo)
                                        {
                                            listaValoresCustoArtigo.Add(itemcusto.VALOR_CUSTO.Value);
                                            listaValoresArtigos.Add(item.VALOR_ARTIGO.Value * item.QTD_ARTIGO.Value);
                                        }
                                    }

                                    var precomo = from mm in DC.Ordem_Reparacao_MaoObras
                                                  where mm.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                                  select mm;

                                    foreach (var itemm in precomo)
                                    {
                                        listaValoresMO.Add(itemm.VALOR.Value);
                                    }


                                    somaValoresCustoArtigos = listaValoresCustoArtigo.Sum();
                                    valorFinalCustos = somaValoresCustoArtigos + Convert.ToDouble(tbvalorcustomanual.Value);
                                    ACTUALIZAOR.VALOR_CUSTO = valorFinalCustos;

                                    somaValoresVendaArtigos = listaValoresArtigos.Sum();
                                    somaValoresVendaMaoObra = listaValoresMO.Sum();
                                    valorfinalVenda = somaValoresVendaArtigos + somaValoresVendaMaoObra + Convert.ToDouble(tbvalorreparacaomanual.Value);
                                    ACTUALIZAOR.VALOR_FINAL = valorfinalVenda;

                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado REPARAÇÃO CONCLUIDA com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    //int idLoja = 0;
                                    //List<string> emailEnvio = new List<string>();

                                    //var verificaLojadaOR = from or in DC.Ordem_Reparacaos
                                    //                       join lojas in DC.Lojas on or.ID_LOJA equals lojas.ID
                                    //                       where or.ID == Convert.ToInt32(id)
                                    //                       select or;

                                    //foreach (var item in verificaLojadaOR)
                                    //{
                                    //    var pesquisaLojistas = from l in DC.Funcionarios
                                    //                           where l.ID_LOJA == item.ID_LOJA
                                    //                           select l;

                                    //    foreach (var mail in pesquisaLojistas)
                                    //    {
                                    //        emailEnvio.Add(mail.EMAIL.ToString() + ";");
                                    //    }
                                    //}

                                    //if (emailEnvio != null)
                                    //{
                                    //    Thread t = new Thread(() => EnviarEmail(emailEnvio.ToString()));
                                    //    t.Start();
                                    //    timer1.Interval = 5;
                                    //    timer1.Enabled = true;
                                    //    Thread tempo = new Thread(() => timer1.Start());
                                    //    tempo.Start();
                                    //}

                                    if (regra == "Tecnico")
                                        Response.Redirect("ListarOrdemReparacao.aspx", false);
                                    else
                                        Response.Redirect("FecharOrdemReparacao.aspx", false);


                                }
                                catch (Exception ex)
                                {

                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }

                            #endregion

                            #region AGUARDA STOCK
                            if (ddlestado.SelectedItem.Text == "Aguarda Stock")
                            {
                                try
                                {
                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado AGUARDA STOCK com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                                }
                                catch (Exception ex)
                                {

                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            #endregion

                            #region AGUARDA AUTORIZAÇÃO CLIENTE

                            if (ddlestado.SelectedItem.Text == "Aguarda Autorização do Cliente")
                            {
                                try
                                {
                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado AGUARDA AUTORIZAÇÃO DO CLIENTE (ORÇAMENTO) com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                                }
                                catch (Exception ex)
                                {

                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            #endregion

                            #region CANCELADA
                            if (ddlestado.SelectedItem.Text == "Cancelada")
                            {
                                try
                                {
                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado CANCELADA com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    Response.Redirect("ListagemGeralOrdensReparacao.aspx", false);
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            #endregion
                        }

                        #endregion
                    }

                    if (regra == "Reparador")
                    {
                        divvaloresmanuais.Visible = false;

                        #region REPARADOR

                        if (validaEstado())
                        {
                            #region REPARAÇÃO CONCLUÍDA

                            if (ddlestado.SelectedItem.Text == "Reparação Concluída")
                            {
                                try
                                {
                                    List<double> listaValoresMO = new List<double>();

                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    ACTUALIZAOR.DATA_CONCLUSAO = DateTime.Now;

                                    var precomo = from mm in DC.Ordem_Reparacao_MaoObras
                                                  where mm.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                                  select mm;

                                    foreach (var itemm in precomo)
                                    {
                                        listaValoresMO.Add(itemm.VALOR.Value);
                                    }

                                    somaValoresVendaMaoObra = listaValoresMO.Sum();
                                    valorfinalVenda = somaValoresVendaMaoObra;

                                    ACTUALIZAOR.VALOR_CUSTO = valorfinalVenda;
                                    ACTUALIZAOR.VALOR_FINAL = valorfinalVenda;
                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado REPARAÇÃO CONCLUIDA com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    //int idLoja = 0;
                                    //string emailEnvio = "";
                                    //Guid useridFunc = new Guid();

                                    //var verificaLoja = from loja in DC.Lojas
                                    //                   join ordem in DC.Ordem_Reparacaos on loja.ID equals ordem.ID_LOJA
                                    //                   where ordem.ID == Convert.ToInt32(id)
                                    //                   select loja;

                                    //foreach (var item in verificaLoja)
                                    //{
                                    //    idLoja = item.ID;
                                    //}

                                    //var verificaFunc = from or in DC.Ordem_Reparacaos
                                    //                   where or.ID == Convert.ToInt32(id)
                                    //                   select or;

                                    //foreach (var item in verificaFunc)
                                    //{
                                    //    useridFunc = item.USERID.Value;
                                    //}


                                    //var verificaEmail = from funcionario in DC.Funcionarios
                                    //                    where funcionario.USERID == useridFunc
                                    //                    select funcionario;

                                    //foreach (var item in verificaEmail)
                                    //{
                                    //    emailEnvio = item.EMAIL;
                                    //}

                                    //var verificaEmailOutraVez = from funcionario in DC.aspnet_Memberships
                                    //                            where funcionario.UserId == useridFunc
                                    //                            select funcionario;

                                    //foreach (var item in verificaEmailOutraVez)
                                    //{
                                    //    emailEnvio = item.Email;
                                    //}

                                    //Thread t = new Thread(() => EnviarEmail(emailEnvio.ToString()));
                                    //t.Start();
                                    //timer1.Interval = 5;
                                    //timer1.Enabled = true;
                                    //Thread tempo = new Thread(() => timer1.Start());
                                    //tempo.Start();

                                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                                }
                                catch (Exception ex)
                                {

                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }

                            #endregion

                            #region AGUARDA STOCK

                            if (ddlestado.SelectedItem.Text == "Aguarda Stock")
                            {
                                try
                                {
                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado AGUARDA STOCK com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                                }
                                catch (Exception ex)
                                {
                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            #endregion

                            #region AGUARDA AUTORIZAÇÃO DO CLIENTE

                            if (ddlestado.SelectedItem.Text == "Aguarda Autorização do Cliente")
                            {
                                try
                                {
                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado AGUARDA AUTORIZAÇÃO CLIENTE (ORÇAMENTO) com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                                }
                                catch (Exception ex)
                                {

                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }
                            #endregion

                            #region CANCELADA

                            if (ddlestado.SelectedItem.Text == "Cancelada")
                            {
                                try
                                {
                                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                                    var ordensRep = from ordem in DC.Ordem_Reparacaos
                                                    where ordem.ID == Convert.ToInt32(id)
                                                    select ordem;

                                    ACTUALIZAOR = ordensRep.First();

                                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    ACTUALIZAOR.ID_ESTADO = Convert.ToInt32(ddlestado.SelectedItem.Value);
                                    DC.SubmitChanges();
                                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi editada a ordem de reparação para o estado CANCELADA com o código: " + ACTUALIZAOR.CODIGO.ToString() + ".", true);
                                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                                }
                                catch (Exception ex)
                                {

                                    ErrorLog.WriteError(ex.Message);
                                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                }
                            }

                            #endregion
                        }
                        #endregion
                    }

                    if (regra == "Cliente" || regra == "Lojista")
                        Response.Redirect("~/Default.aspx", true);

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }

            #endregion
            }
        }

        public void EnviarEmail(List<string> email)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Dygus2017!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "DYGUS - SAT");

            msg.To.Add(new MailAddress(email.ToString()));

            string url = "";
            url = "http://www.dygus.com/onoff";
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Ordem de Reparação Concluída";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Utilizador,<br/><br/>" +
                "A plataforma DYGUS - SAT informa que a ordem de reparação " + tbcodordemreparacao.Text.ToString() + " se encontra concluída! <br/>" +
                "A partir deste momento, poderá dirigir-se à plataforma e notificar o cliente em: Fechar OR.<br/><br/>" +
                "Para saber mais, por favor, efectue login na sua àrea reservada em " + url.ToString() + " <br/><br/>" +
                "Muito obrigado pela sua colaboração.<br />" +
                "<img src=cid:companylogo alt=.  /><br /><br /><br/>" +
                "Por favor não responda a este email, a caixa postal emissora deste mail é exclusivamente para envio de mensagens.<br/>" +
                "Para qualquer assunto relacionado com o registo na plataforma por favor contacte-nos através do email <a href=mailto:" + mailContactoSuporte + ">ricardoshm@gmail.com</a>" +
                "</body>");

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(corpohtml, null, "text/html");
            htmlView.LinkedResources.Add(logo);
            msg.AlternateViews.Add(htmlView);

            try
            {
                client.Send(msg);
                SQLLog.registaLogBD(userid, DateTime.Now, "Editar ordem de reparação ADMIN", "Foi enviado um email para " + email.ToString() + " a informar que a ordem de reparação está concluida.", true);
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbcodartigo.Text = tbdescricaoArtigo.Text = tbqtdArtigo.Text = "";
            ddlestado.ClearSelection();
        }

        protected void carregaEstados()
        {
            try
            {
                var carrega = from est in DC.Ordem_Reparacao_Estados
                              where est.ID == 3 || est.ID == 4 || est.ID == 5 || est.ID == 7
                              orderby est.DESCRICAO ascending
                              select est;

                ddlestado.DataTextField = "DESCRICAO";
                ddlestado.DataValueField = "ID";
                ddlestado.DataSource = carrega;
                ddlestado.DataBind();
                ddlestado.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaCamposArtigos()
        {
            if (String.IsNullOrEmpty(tbcodartigo.Text))
            {
                resultadoArtigos.Style.Add("display", "block");
                resultadoArtigosText.Style.Add("display", "block");
                resultadoArtigosText.InnerHtml = "O campo Código do Artigo é de preenchimento obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(tbdescricaoArtigo.Text))
            {
                resultadoArtigos.Style.Add("display", "block");
                resultadoArtigosText.Style.Add("display", "block");
                resultadoArtigosText.InnerHtml = "O campo Descrição do Artigo é de preenchimento obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(tbqtdArtigo.Text))
            {
                resultadoArtigos.Style.Add("display", "block");
                resultadoArtigosText.Style.Add("display", "block");
                resultadoArtigosText.InnerHtml = "O campo Quantidade do Artigo é de preenchimento obrigatório!";
                return false;
            }

            if (tbvalorcustomanual.Value != null && tbvalorreparacaomanual.Value == null)
            {
                erroValorManual.Style.Add("display", "block");
                erroMessageValorManual.Style.Add("display", "block");
                erroMessageValorManual.InnerHtml = "O campo Valor de Reparação é de preenchimento obrigatório!";
                return false;
            }

            return true;
        }

        protected void listagemartigosutilizados_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
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
                                          ID = artigo.ID_ARTIGO,
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
            }

        }

        protected void btnInserirArtigo_Click(object sender, EventArgs e)
        {
            if (validaCamposArtigos())
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
                    double precofinal = 0;
                    int idArtigo = 0;

                    var verificacliente = from cliente in DC.Parceiros
                                          where cliente.CODIGO == tbcodcliente.Text
                                          select cliente;

                    foreach (var item in verificacliente)
                    {
                        if (item.ID_TIPO_PARCEIRO == 1)
                        {

                            var codigo = from art in DC.Artigos
                                         join preco in DC.Artigo_Stocks on art.ID equals preco.ID_ARTIGO
                                         where art.CODIGO == tbcodartigo.Text
                                         select new
                                         {
                                             ID = art.ID,
                                             PRECO = preco.VALOR_VENDA.Value
                                         };
                            foreach (var itemcod in codigo)
                            {
                                idArtigo = itemcod.ID;
                                precofinal = itemcod.PRECO;
                            }

                        }

                        if (item.ID_TIPO_PARCEIRO == 2 || item.ID_TIPO_PARCEIRO == 4)
                        {

                            var codigo = from art in DC.Artigos
                                         join preco in DC.Artigo_Stocks on art.ID equals preco.ID_ARTIGO
                                         where art.CODIGO == tbcodartigo.Text
                                         select new
                                         {
                                             ID = art.ID,
                                             PRECO = preco.VALOR_REVENDA.Value
                                         };
                            foreach (var itemcodd in codigo)
                            {
                                idArtigo = itemcodd.ID;
                                precofinal = itemcodd.PRECO * (Convert.ToInt32(tbqtdArtigo.Value));
                            }
                        }

                    }

                    LINQ_DB.Ordem_Reparacao_Artigo NOVOARTIGO = new LINQ_DB.Ordem_Reparacao_Artigo();
                    NOVOARTIGO.ID_ORDEM_REPARACAO = Convert.ToInt32(id);
                    NOVOARTIGO.ID_ARTIGO = idArtigo;
                    NOVOARTIGO.QTD_ARTIGO = Convert.ToInt32(tbqtdArtigo.Text);
                    NOVOARTIGO.VALOR_ARTIGO = precofinal;
                    NOVOARTIGO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    DC.Ordem_Reparacao_Artigos.InsertOnSubmit(NOVOARTIGO);
                    DC.SubmitChanges();
                    tbcodartigo.Text = tbdescricaoArtigo.Text = tbqtdArtigo.Text = "";
                    listagemartigosutilizados.Rebind();
                    Session.Clear();

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }

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

        protected void tbcodartigo_TextChanged(object sender, EventArgs e)
        {
            int contador = 0;

            try
            {
                var contaArtigos = from artigo in DC.Artigos
                                   where tbcodartigo.Text == artigo.CODIGO
                                   select artigo;

                contador = Enumerable.Count(contaArtigos);

                if (contador > 0)
                {
                    LINQ_DB.Artigo DADOSARTIGO = new LINQ_DB.Artigo();
                    var carregaDadosArtigo = from art in DC.Artigos
                                             where tbcodartigo.Text == art.CODIGO
                                             select art;

                    DADOSARTIGO = carregaDadosArtigo.First();
                    tbdescricaoArtigo.Text = DADOSARTIGO.DESCRICAO;
                }
                else
                {
                    ShowAlertMessage("Código de Artigo inválido ou inexistente! O campo de pesquisa será limpo!");
                    tbcodartigo.Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarOrdemReparacao.aspx", false);
        }

        protected bool validaEstado()
        {
            if (ddlestado.SelectedItem.Text == "Por favor seleccione...")
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "Deve actualizar o estado da ordem de reparação antes de gravar!";
                ddlestado.Focus();
                return false;
            }

            return true;
        }

        protected bool validaMO()
        {
            if (String.IsNullOrEmpty(tbdescmo.Text))
            {
                resultadoMO.Style.Add("display", "block");
                erroMO.InnerHtml = "O campo descrição de Mão-de-Obra é de preenchimento obrigatório!";
                return false;
            }
            if (tbvalormo.Value == null)
            {
                resultadoMO.Style.Add("display", "block");
                erroMO.InnerHtml = "O campo valor de Mão-de-Obra é de preenchimento obrigatório!";
                return false;
            }
            return true;
        }

        protected void btnRegistarMO_Click(object sender, EventArgs e)
        {
            if (validaMO())
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

                    LINQ_DB.Ordem_Reparacao_MaoObra NOVAMO = new LINQ_DB.Ordem_Reparacao_MaoObra();
                    NOVAMO.ID_ORDEM_REPARACAO = Convert.ToInt32(id);
                    NOVAMO.DESCRICAO = tbdescmo.Text;
                    NOVAMO.VALOR = tbvalormo.Value;
                    if (!String.IsNullOrEmpty(tbobsmo.Text))
                        NOVAMO.OBSERVACOES = tbobsmo.Text;
                    else
                        NOVAMO.OBSERVACOES = "N/D";
                    NOVAMO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    DC.Ordem_Reparacao_MaoObras.InsertOnSubmit(NOVAMO);
                    DC.SubmitChanges();
                    tbdescmo.Text = tbvalormo.Text = tbobsmo.Text = "";
                    listagemMO.Rebind();
                    Session.Clear();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void listagemMO_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
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
                var consultamo = from mo in DC.Ordem_Reparacao_MaoObras
                                 where mo.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                 select new
                                 {
                                     ID = mo.ID,
                                     DESC_MAO_OBRA = mo.DESCRICAO,
                                     VALOR_MAO_OBRA = mo.VALOR.Value,
                                     OBS_MAO_OBRA = mo.OBSERVACOES,
                                 };

                listagemMO.DataSourceID = "";
                listagemMO.DataSource = consultamo;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void listagemartigosutilizados_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    LINQ_DB.Ordem_Reparacao_Artigo ARTIGOELIMINA = new LINQ_DB.Ordem_Reparacao_Artigo();

                    var artigo = from t in DC.Ordem_Reparacao_Artigos
                                 where t.ID_ARTIGO == Convert.ToInt32(id)
                                 select t;

                    foreach (var clienteeliminado in artigo)
                    {
                        DC.Ordem_Reparacao_Artigos.DeleteOnSubmit(clienteeliminado);
                        DC.SubmitChanges();
                    }

                    listagemartigosutilizados.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemMO_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    LINQ_DB.Ordem_Reparacao_MaoObra MOELIMINA = new LINQ_DB.Ordem_Reparacao_MaoObra();

                    var artigo = from t in DC.Ordem_Reparacao_MaoObras
                                 where t.ID == Convert.ToInt32(id)
                                 select t;

                    foreach (var clienteeliminado in artigo)
                    {
                        DC.Ordem_Reparacao_MaoObras.DeleteOnSubmit(clienteeliminado);
                        DC.SubmitChanges();
                    }

                    listagemMO.Rebind();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnOrcamento_Click(object sender, EventArgs e)
        {
            //string codigoNovoOrc = "";
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


            #region verificaperfil

            System.Security.Principal.IPrincipal User = HttpContext.Current.User;

            Guid userid = new Guid();
            string UserName = "";
            string emailUser = "";

            if (User.Identity.IsAuthenticated == true)
            {
                try
                {
                    var us = from users in DC.aspnet_Memberships
                             where users.LoweredEmail == User.Identity.Name
                             select users;

                    foreach (var item in us)
                    {
                        userid = item.UserId;
                        emailUser = item.LoweredEmail;
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

                    Guid Role = new Guid();

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
                        divvaloresmanuais.Visible = false;

                    if (regra == "Cliente" || regra == "Lojista")
                        Response.Redirect("~/Default.aspx", true);

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }

                Response.Redirect("InserirOrcamentoClienteOR.aspx?or=" + id.ToString(), false);

            #endregion
            }


        }

        public void EnviarEmailOrcamentoCliente(string email)
        {

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Dygus2017!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "DYGUS - SAT");

            msg.To.Add(new MailAddress(email.ToString()));

            string url = "";
            url = "http://www.dygus.com/onoff/";
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Novo Orçamento Registado";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Cliente,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criado um novo orçamento na sua conta de cliente. Por favor clique <a href=" + url + ">aqui</a> para consultar.<br/><br/>" +
                "Muito obrigado pela sua colaboração.<br />" +
                "<img src=cid:companylogo alt=.  /><br /><br /><br/>" +
                "Por favor não responda a este email, a caixa postal emissora deste mail é exclusivamente para envio de mensagens.<br/>" +
                "Para qualquer assunto relacionado com o registo na plataforma por favor contacte-nos através do email <a href=mailto:" + mailContactoSuporte + ">ricardoshm@gmail.com</a>" +
                "</body>");

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(corpohtml, null, "text/html");
            htmlView.LinkedResources.Add(logo);
            msg.AlternateViews.Add(htmlView);

            try
            {
                client.Send(msg);
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnConsultarOrcamento_Click(object sender, EventArgs e)
        {
            if (idOrcamento > 0)
            {
                try
                {
                    Response.Redirect("GerirORCCliente?ID=" + idOrcamento + "", false);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }
    }
}
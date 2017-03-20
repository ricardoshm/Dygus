using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class GerirOrdemReparacao : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();

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
            

            if (Session["returnedValuesTecnicos"] != null)
            {
                List<string> resultsTecnicos = (List<string>)Session["returnedValuesTecnicos"];
                tbcodtecnico.Text += resultsTecnicos[1];
                tbnometecnico.Text += resultsTecnicos[3];
                tblojatecnico.Text += resultsTecnicos[2];
                tbemailtecnico.Text += resultsTecnicos[4];
                tbobstecnico.Text += resultsTecnicos[5];
            }

            if (Session["returnedValuesReparadores"] != null)
            {
                List<string> resultsReparadores = (List<string>)Session["returnedValuesReparadores"];
                tbcodreparador.Text += resultsReparadores[0];
                tbnomereparador.Text += resultsReparadores[2];
                tbmoradareparador.Text += resultsReparadores[3];
                tbcodpostalreparador.Text += resultsReparadores[4];
                tblocalidadereparador.Text += resultsReparadores[5];
                tbcontactoreparador.Text += resultsReparadores[6];
                tbemailreparador.Text += resultsReparadores[7];
                tbnifreparador.Text += resultsReparadores[8];
                tbobsreparador.Text += resultsReparadores[9];

            }
            Session.Clear();

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

            if (!Page.IsPostBack)
                carregaOR();
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
                tbdataRegisto.Text = item.DATA_REGISTO.Value.ToShortDateString();
                tbloja.Text = item.Loja.NOME;
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
                tbdataRegistocliente.Text = item.DATA_REGISTO.Value.ToShortDateString();
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
                tbvalorprevisto.Value = item.VALOR_PREVISTO_REPARACAO.Value;
                tbdataprevistareparacao.Text = item.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString();
                tbObsNovoEquipAvariado.Text = item.Equipamento_Avariado.OBSERVACOES.ToString();
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

            if (validaCampos())
            {
                string emailEnvio = "";

                try
                {
                    LINQ_DB.Ordem_Reparacao_Atribuicao NOVAATRIBUICAOOR = new LINQ_DB.Ordem_Reparacao_Atribuicao();
                    NOVAATRIBUICAOOR.ID_ORDEM_REPARACAO = Convert.ToInt32(id);

                    if (!String.IsNullOrEmpty(tbcodtecnico.Text) && String.IsNullOrEmpty(tbcodreparador.Text))
                    {
                        var user = from u in DC.Funcionarios
                                   where u.CODIGO == tbcodtecnico.Text
                                   select u;

                        foreach (var item in user)
                        {
                            NOVAATRIBUICAOOR.USERID = item.USERID;
                            emailEnvio = item.EMAIL;
                        }
                    }

                    if (!String.IsNullOrEmpty(tbcodreparador.Text) && String.IsNullOrEmpty(tbcodtecnico.Text))
                    {
                        var user = from u in DC.Parceiros
                                   where u.CODIGO == tbcodreparador.Text
                                   select u;

                        foreach (var item in user)
                        {
                            NOVAATRIBUICAOOR.USERID = item.USERID;
                            emailEnvio = item.EMAIL;
                        }
                    }
                    NOVAATRIBUICAOOR.DATA_REGISTO = DateTime.Now;
                    NOVAATRIBUICAOOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    DC.Ordem_Reparacao_Atribuicaos.InsertOnSubmit(NOVAATRIBUICAOOR);
                    DC.SubmitChanges();

                    LINQ_DB.Ordem_Reparacao ACTUALIZAOR = new LINQ_DB.Ordem_Reparacao();
                    var pesquisaOR = from ors in DC.Ordem_Reparacaos
                                     where ors.ID == Convert.ToInt32(id)
                                     select ors;
                    ACTUALIZAOR = pesquisaOR.First();

                    ACTUALIZAOR.ID_ESTADO = 2;
                    ACTUALIZAOR.ATRIBUIDA = true;
                    ACTUALIZAOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    DC.SubmitChanges();

                    string marca = "";
                    string modelo = "";
                    string avaria = "";
                    string obsavaria = "";

                    if (!String.IsNullOrEmpty(tbmarca.Text))
                        marca = tbmarca.Text;
                    else
                        marca = "N/D";
                    if (!String.IsNullOrEmpty(tbmodelo.Text))
                        modelo = tbmodelo.Text;
                    else
                        modelo = "N/D";
                    if (!String.IsNullOrEmpty(tbdescricaoProblemaEquipAvariado.Text))
                        avaria = tbdescricaoProblemaEquipAvariado.Text;
                    else
                        avaria = "N/D";
                    if (!String.IsNullOrEmpty(tbobsequipavariado.Text))
                        obsavaria = tbobsequipavariado.Text;
                    else
                        obsavaria = "N/D";

                    Thread t = new Thread(() => EnviarEmail(emailEnvio.ToString(), marca.ToString(), modelo.ToString(), avaria.ToString(), obsavaria.ToString()));
                    t.Start();
                    timer1.Interval = 5;
                    timer1.Enabled = true;
                    Thread tempo = new Thread(() => timer1.Start());
                    tempo.Start();

                    Response.Redirect("ListarTecnicoOrdemReparacao.aspx", false);

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        public void EnviarEmail(string email, string marca, string modelo, string avaria, string obsavaria)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Lisboa22!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "DYGUS - SAT");

            msg.To.Add(new MailAddress(email.ToString()));

            string url = "";
            url = "http://www.dygus.com/onoff";
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Nova Ordem de Reparação";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Utilizador,<br/><br/>" +
                "A plataforma DYGUS - SAT informa que existe uma nova ordem de reparação que lhe foi atribuída! <br/><br/><br/>" +
                "Marca do equipamento avariado: " + marca.ToString() + " <br/>" +
                "Modelo do equipamento avariado: " + modelo.ToString() + " <br/>" +
                "Descrição detalhada da avaria do equipamento avariado: " + avaria.ToString() + " <br/>" +
                "Observações do equipamento avariado: " + obsavaria.ToString() + " <br/>" +
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
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaCampos()
        {
            if (String.IsNullOrEmpty(tbcodreparador.Text) && String.IsNullOrEmpty(tbcodtecnico.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "Deve atribuir esta Ordem de Reparação pelo menos a um técnico ou a um reparador!";
                return false;
            }

            if (!String.IsNullOrEmpty(tbcodreparador.Text) && !String.IsNullOrEmpty(tbcodtecnico.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "Deve atribuir esta Ordem de Reparação apenas a um técnico ou a um reparador!";
                return false;
            }

            return true;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            Session.Clear();
            tbcodtecnico.Text = tbnometecnico.Text = tblojatecnico.Text = tbemailtecnico.Text = tbobstecnico.Text = "";
            tbcodreparador.Text = tbnomereparador.Text = tbmoradareparador.Text = tbcodpostalreparador.Text = tblocalidadereparador.Text = tbcontactoreparador.Text = tbemailreparador.Text = tbnifreparador.Text = tbobsreparador.Text = "";
        }

        protected void tbcodreparador_TextChanged(object sender, EventArgs e)
        {
            int contador = 0;

            try
            {
                var contaReparadores = from reparador in DC.Parceiros
                                       where ((reparador.ID_TIPO_PARCEIRO == 3 || reparador.ID_TIPO_PARCEIRO == 4) && tbcodreparador.Text == reparador.CODIGO) && reparador.ACTIVO == true
                                       select reparador;

                contador = Enumerable.Count(contaReparadores);

                if (contador > 0)
                {
                    LINQ_DB.Parceiro DADOSREPARADOR = new LINQ_DB.Parceiro();
                    var carregaDadosReparadores = from reparador in DC.Parceiros
                                                  where tbcodreparador.Text == reparador.CODIGO
                                                  select reparador;

                    DADOSREPARADOR = carregaDadosReparadores.First();
                    tbcodreparador.Text = DADOSREPARADOR.CODIGO;
                    tbnomereparador.Text = DADOSREPARADOR.NOME;
                    tbmoradareparador.Text = DADOSREPARADOR.MORADA;
                    tbcodpostalreparador.Text = DADOSREPARADOR.CODPOSTAL;
                    tblocalidadereparador.Text = DADOSREPARADOR.LOCALIDADE;
                    tbcontacto.Text = DADOSREPARADOR.TELEFONE;
                    tbemailreparador.Text = DADOSREPARADOR.EMAIL;
                    tbnifreparador.Text = DADOSREPARADOR.NIF.ToString();
                    tbobsreparador.Text = DADOSREPARADOR.OBSERVACOES;
                }
                else
                {
                    ShowAlertMessage("Código de Reparador inválido ou inexistente! O campo de pesquisa será limpo!");
                    tbcodreparador.Text = "";
                }
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }


        protected void tbcodtecnico_TextChanged(object sender, EventArgs e)
        {
            int contador = 0;

            try
            {
                var contaTecnicos = from tecnico in DC.Funcionarios
                                    where (tecnico.ID_TIPO_FUNCIONARIO == 2 && tbcodtecnico.Text == tecnico.CODIGO) && tecnico.ACTIVO == true
                                    select tecnico;

                contador = Enumerable.Count(contaTecnicos);

                if (contador > 0)
                {
                    LINQ_DB.Funcionario DADOSTECNICO = new LINQ_DB.Funcionario();
                    var carregaDadosFuncionarios = from funcionario in DC.Funcionarios
                                                   where tbcodtecnico.Text == funcionario.CODIGO
                                                   select funcionario;

                    DADOSTECNICO = carregaDadosFuncionarios.First();
                    tbcodtecnico.Text = DADOSTECNICO.CODIGO;
                    tbnometecnico.Text = DADOSTECNICO.NOME;
                    tblojatecnico.Text = DADOSTECNICO.Loja.NOME;
                    tbemailtecnico.Text = DADOSTECNICO.EMAIL;
                    tbobstecnico.Text = DADOSTECNICO.OBSERVACOES;
                }
                else
                {
                    ShowAlertMessage("Código de Técnico inválido ou inexistente! O campo de pesquisa será limpo!");
                    tbcodtecnico.Text = "";
                }
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

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarTecnicoOrdemReparacao.aspx", false);
        }




    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirOrcamentoClienteOR : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();
        protected System.Timers.Timer timer2 = new System.Timers.Timer();
        protected System.Timers.Timer timer3 = new System.Timers.Timer();
        protected System.Timers.Timer timer4 = new System.Timers.Timer();
        string resultado = "";
        string letras = "";
        int numeros = 0;
        string nOr = "";
        Guid userid = new Guid();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["or"] != null)
            {
                nOr = Request.QueryString["or"].ToString();


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


                if (dataregisto.SelectedDate == null)
                    dataregisto.SelectedDate = DateTime.Today;

                if (!Page.IsPostBack)
                {

                    try
                    {
                        carregaORC();
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);
                        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }

                }
            }
            else
            {
                Response.Redirect("Default.aspx", false);
            }
        }

        private int GeraRandomNumeros()
        {
            Random clsRan = new Random();
            int meuNumero = clsRan.Next(1, 9999);

            return meuNumero;
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();

        }

        protected void carregaORC()
        {
            try
            {
                letras = RandomString(2);
                numeros = GeraRandomNumeros();
                resultado = letras + "-" + numeros;


                int contaDuplicados = (from conf in DC.Configuracaos
                                       where conf.INICIAL == letras && conf.CODIGO == numeros
                                       select conf).Count();
                if (contaDuplicados > 0)
                {
                    while (contaDuplicados > 0)
                    {
                        letras = "";
                        numeros = 0;
                        resultado = "";

                        letras = RandomString(2);
                        numeros = GeraRandomNumeros();
                        resultado = letras + "-" + numeros;
                    }
                }
                else
                {
                    letras = "";
                    numeros = 0;
                    resultado = "";

                    letras = RandomString(2);
                    numeros = GeraRandomNumeros();
                    resultado = letras + "-" + numeros;
                }

                LINQ_DB.Configuracao configuracao = new LINQ_DB.Configuracao();

                var config = from conf in DC.Configuracaos
                             where conf.ID == 10
                             select conf;

                configuracao = config.First();
                configuracao.INICIAL = letras;
                configuracao.CODIGO = Convert.ToInt32(numeros);
                DC.SubmitChanges();


                tbcodOrcamento.Text = resultado.ToString();

                var pesquisa = from or in DC.Ordem_Reparacaos
                               where or.ID == Convert.ToInt32(nOr)
                               select or;

                foreach (var itemo in pesquisa)
                {
                    tbvalorprevistorepcliente.Value = itemo.VALOR_PREVISTO_REPARACAO.Value;
                    tbdescricaoavaria.Text = itemo.DESCRICAO_DETALHADA_PROBLEMA;
                }

                var emailCliente = from p in DC.Parceiros
                                   join orr in DC.Ordem_Reparacaos on p.USERID equals orr.USERID
                                   where orr.ID == Convert.ToInt32(nOr)
                                   select p;

                foreach (var item in emailCliente)
                {
                    tbEmailCliente.Text = item.EMAIL.ToString();
                }

                if (!String.IsNullOrEmpty(tbEmailCliente.Text) && tbEmailCliente.Text == "dygussat@dygus.com")
                {
                    ShowAlertMessage("ATENÇÃO! O email do cliente é inválido para envio!");
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

        public void EnviarEmailCliente(string email, Guid UserID, string pass)
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
            url = "http://www.dygus.com/onoff/VerificacaoConta.aspx?id=" + UserID;
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Registo de Conta de Cliente";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Cliente,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criada uma nova conta de cliente em seu nome com os seguintes dados de acesso:<br/><br/>" +
                "Nome de Utilizador: " + email.ToString() + ".<br/>" +
                "Palavra-Passe: " + pass.ToString() + ".<br/><br/>" +
                "Para concluir o seu registo, por favor clique: <a href=" + url + ">AQUI</a>.<br/><br/>" +
                "Recomendamos que, para sua segurança, altere a palavra-passe de acesso à plataforma.<br/><br/>" +
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
                SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Foi enviado um email para: " + email.ToString() + " informando da criação de nova conta de cliente", true);
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
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
                "<img src=cid:companylogo alt= /><br /><br /><br/>" +
                "Por favor não responda a este email, a caixa postal emissora deste mail é exclusivamente para envio de mensagens.<br/>" +
                "Para qualquer assunto relacionado com o registo na plataforma por favor contacte-nos através do email <a href=mailto:" + mailContactoSuporte + ">ricardoshm@gmail.com</a>" +
                "</body>");

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(corpohtml, null, "text/html");
            htmlView.LinkedResources.Add(logo);
            msg.AlternateViews.Add(htmlView);

            try
            {
                client.Send(msg);
                SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Foi enviado um email para: " + email.ToString() + " informando da criação de novo orçamento", true);
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(nOr))
                {
                    LINQ_DB.Ordem_Reparacao UPDATEOR = new LINQ_DB.Ordem_Reparacao();

                    var orcNum = from or in DC.Ordem_Reparacaos
                                 where or.ID == Convert.ToInt32(nOr)
                                 select or;

                    UPDATEOR = orcNum.First();
                    UPDATEOR.VALOR_PREVISTO_REPARACAO = tbvalorprevistorep.Value;
                    UPDATEOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    UPDATEOR.ID_ESTADO = 4;
                    DC.SubmitChanges();
                    SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Ordem de reparação " + UPDATEOR.CODIGO.ToString() + " actualizada para o estado: Aguarda Autorização do Cliente (Orçamento) ", true);

                    LINQ_DB.Orcamento UPDATE = new LINQ_DB.Orcamento();

                    UPDATE.CODIGO = tbcodOrcamento.Text;
                    UPDATE.USERID = UPDATEOR.USERID.Value;
                    UPDATE.ID_LOJA = UPDATEOR.ID_LOJA.Value;
                    UPDATE.CLIENTE_NOTIFICADO = false;
                    UPDATE.VALOR_ORCAMENTO = tbvalorprevistorep.Value;
                    UPDATE.ID_ESTADO = 1;
                    UPDATE.DESCRICAO_DETALHADA_AVARIA = UPDATEOR.DESCRICAO_DETALHADA_PROBLEMA;
                    UPDATE.ID_EQUIPAMENTO_AVARIADO = UPDATEOR.ID_EQUIPAMENTO_AVARIADO.Value;
                    UPDATE.ID_TRABALHOS_ADICIONAIS = UPDATEOR.ID_TRABALHOS_ADICIONAIS.Value;
                    UPDATE.DATA_REGISTO = DateTime.Now;
                    UPDATE.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    UPDATE.CONVERTIDO_OR = false;
                    UPDATE.ID_OR_ORIGEM = Convert.ToInt32(nOr);
                    DC.Orcamentos.InsertOnSubmit(UPDATE);
                    DC.SubmitChanges();
                    SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Novo orçamento registado com o código " + UPDATE.CODIGO.ToString(), true);

                    if (!String.IsNullOrEmpty(tbComentariosOrcamento.Text))
                    {
                        LINQ_DB.Orcamentos_Comentario NOVOCOMENTARIO = new LINQ_DB.Orcamentos_Comentario();

                        NOVOCOMENTARIO.USERID_COMENTARIO = userid;
                        NOVOCOMENTARIO.COMENTARIO = tbComentariosOrcamento.Text;
                        NOVOCOMENTARIO.ID_ORCAMENTO = UPDATE.ID;
                        NOVOCOMENTARIO.DATA_COMENTARIO = DateTime.Now;
                        DC.Orcamentos_Comentarios.InsertOnSubmit(NOVOCOMENTARIO);
                        DC.SubmitChanges();
                        SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Novo comentário registado no orçamento com o código " + UPDATE.CODIGO.ToString(), true);
                    }

                    string emailCliente = tbEmailCliente.Text;

                    if (String.IsNullOrEmpty(emailCliente))
                    {
                        var consultaemailCliente = from p in DC.Parceiros
                                                   where p.USERID.Value == UPDATEOR.USERID.Value
                                                   select p;

                        foreach (var item in consultaemailCliente)
                        {
                            emailCliente = item.EMAIL.ToString();
                            tbEmailCliente.Text = emailCliente;
                        }
                    }

                    var comparaEmail = from p in DC.Parceiros
                                       where p.USERID.Value == UPDATEOR.USERID.Value
                                       select p;

                    foreach (var item in comparaEmail)
                    {
                        if (tbEmailCliente.Text != item.EMAIL.ToString())
                            ActualizaEmailCliente(item.USERID.Value, tbEmailCliente.Text);
                    }

                    if (rbEnviaEmail.Checked)
                    {
                        if (!String.IsNullOrEmpty(emailCliente))
                        {
                            if (emailCliente != "dygussat@dygus.com")
                            {
                                Thread t = new Thread(() => EnviarEmailOrcamentoCliente(emailCliente.ToString()));
                                t.Start();
                                timer4.Interval = 5;
                                timer4.Enabled = true;
                                Thread tempo = new Thread(() => timer4.Start());
                                tempo.Start();

                                LINQ_DB.Orcamento UPDATEORC = new LINQ_DB.Orcamento();

                                var orc = from or in DC.Orcamentos
                                          where or.CODIGO == tbcodOrcamento.Text
                                          select or;

                                UPDATEORC = orc.First();
                                UPDATEOR.CLIENTE_NOTIFICADO = true;
                                UPDATEOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                DC.SubmitChanges();
                                SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Cliente notificado por email, Orçamento: " + UPDATEOR.CODIGO.ToString(), true);
                            }
                        }
                    }

                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                }
                else
                {
                    if (!String.IsNullOrEmpty(nOr))
                        Response.Redirect("EditarOrdemReparacao.aspx?ID=" + nOr, false);
                    else
                        Response.Redirect("Default.aspx", false);
                }

            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void limpaCampos()
        {
            dataregisto.SelectedDate = DateTime.Today;


        }

        public void ActualizaEstaOR(int idOr)
        {
            try
            {
                LINQ_DB.Ordem_Reparacao UPDATEESTADO = new LINQ_DB.Ordem_Reparacao();

                var pesquisaor = from p in DC.Ordem_Reparacaos
                                 where p.ID == idOr
                                 select p;

                UPDATEESTADO = pesquisaor.First();
                UPDATEESTADO.ID_ESTADO = 2;
                UPDATEESTADO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                DC.SubmitChanges();

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["or"] != null)
                {
                    nOr = Request.QueryString["or"].ToString();

                    ActualizaEstaOR(Convert.ToInt32(nOr));
                    SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Estado da ordem de reparação revertido para: Atribuida e em Reparação.", true);
                    Response.Redirect("ListarOrdemReparacao.aspx", false);
                }
                else
                    Response.Redirect("ListarOrcamentoCliente.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void ActualizaEmailCliente(Guid useridCliente, string emailCliente)
        {
            try
            {
                LINQ_DB.Parceiro emailParceiro = new LINQ_DB.Parceiro();

                var eParceiro = from or in DC.Parceiros
                                where or.USERID.Value == useridCliente
                                select or;

                emailParceiro = eParceiro.First();
                emailParceiro.EMAIL = emailCliente;
                emailParceiro.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                DC.SubmitChanges();
                SQLLog.registaLogBD(userid, DateTime.Now, "Inserir Orçamento", "Email do cliente: " + emailParceiro.EMAIL + " actualizado.", true);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
            }

        }

        protected void rbEnviaEmail_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbEnviaEmail.Checked)
                    if (tbEmailCliente.Text == "dygussat@dygus.com")
                    {
                        ShowAlertMessage("ATENÇÃO! O email do cliente é inválido para envio!");
                    }

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
            }
        }

    }
}
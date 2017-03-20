using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class GerirORCCliente : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();
        protected System.Timers.Timer timer2 = new System.Timers.Timer();
        string myid = "";
        int idOrdemRepAssociada = 0;
        Guid useridP = new Guid();
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
                    useridP = item.UserId;
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

                if (regra == "SuperAdmin")
                {

                }

                if (Request.QueryString["ID"] != null)
                {
                    myid = Request.QueryString["ID"];

                    var pesquisa = from pp in DC.Orcamentos
                                   where pp.ID == Convert.ToInt32(myid)
                                   select pp;

                    foreach (var item in pesquisa)
                    {
                        if (regra != "SuperAdmin")
                        {
                            if (item.ID_OR_ORIGEM != null && item.ID_OR_ORIGEM.Value > 0)
                                btnAceitar.Enabled = btnRejeitar.Enabled = false;
                            else
                                btnAceitar.Enabled = btnRejeitar.Enabled = true;
                        }
                        else
                        {
                            if (item.ID_OR_ORIGEM != null && item.ID_OR_ORIGEM.Value > 0)
                            {
                                if (item.ID_ESTADO <= 1)
                                {
                                    btnAceitar.Text = "Aprovar Orçamento";
                                    btnRejeitar.Text = "Rejeitar Orçamento";
                                    btnAceitar.Enabled = btnRejeitar.Enabled = true;
                                }
                                else
                                    modoLeitura();
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("ListagemGeralOrcamentoCliente.aspx", true);
                    return;
                }


            }
            else
                Response.Redirect("~/Default.aspx", true);




            if (!Page.IsPostBack)
            {
                carregaORC();
                carregaComentarios();
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

        protected void carregaComentarios()
        {
            try
            {

                string myRole = "";


                var pesquisaComent = from o in DC.Orcamentos_Comentarios
                                     where o.ID_ORCAMENTO == Convert.ToInt32(myid)
                                     orderby o.DATA_COMENTARIO.Value descending
                                     select o;

                foreach (var item in pesquisaComent)
                {
                    myRole = GetRoleName(item.USERID_COMENTARIO.Value);

                    if (myRole == "Cliente")
                    {
                        var pesquisaNome = from nome in DC.Parceiros
                                           where nome.USERID.Value == item.USERID_COMENTARIO.Value
                                           select nome;

                        foreach (var nomeP in pesquisaNome)
                        {
                            comentarioCliente.Text +=
                                "<li class='clearfix leftside'>" +
                                        "<div class='user'>" +
                                            "<div class='avatar'>" +
                                                "<img src='../images/no_avatar.jpg' alt='' />" +
                                            "</div>" +
                                            "<span class='ago' id='dataComentUser' runat='server'>" + item.DATA_COMENTARIO.Value.ToShortDateString() + "<br />" + item.DATA_COMENTARIO.Value.ToShortTimeString() + "</span>" +
                                        "</div>" +
                                        "<div class='message' style='width:90%; margin-left:85px;'><span class='name' id='nomeUser' runat='server'>" + nomeP.NOME + "</span><span class='txt' id='comentUser' runat='server'>" + item.COMENTARIO.ToString() + "</span></div>" +
                                    "</li>";
                        }
                    }

                    if (myRole == "Administrador" || myRole == "SuperAdmin")
                    {
                        var pesquisaNome = from nome in DC.Orcamentos
                                           where nome.ID == Convert.ToInt32(myid)
                                           select nome;

                        foreach (var nomeU in pesquisaNome)
                        {
                            comentarioAdmin.Text +=
                                        "<li class='clearfix rightside'>" +
                                                    "<div class='user'>" +
                                                        "<div class='avatar'>" +
                                                            "<img src='../images/no_avatar.jpg' alt='' />" +
                                                        "</div>" +
                                                        "<span class='ago' id='dataComentUser' runat='server'>" + item.DATA_COMENTARIO.Value.ToShortDateString() + "<br />" + item.DATA_COMENTARIO.Value.ToShortTimeString() + "</span>" +
                                                    "</div>" +
                                                    "<div class='message' style='width:90%;'><span class='name' id='nomeAdmin' runat='server'>" + nomeU.Loja.NOME.ToString() + "</span><span class='txt' id='comentAdmin' runat='server'>" + item.COMENTARIO.ToString() + "</span></div>" +
                                                "</li>";
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

        protected void btnInserirComent_Click(object sender, EventArgs e)
        {
            string perfil = "";

            try
            {
                Guid userid = new Guid();

                if (!string.IsNullOrEmpty(tbComentario.Text))
                {
                    if (Request.QueryString["ID"] != null)
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

                            LINQ_DB.Orcamentos_Comentario UPDATECOMENT = new LINQ_DB.Orcamentos_Comentario();

                            UPDATECOMENT.ID_ORCAMENTO = Convert.ToInt32(Request.QueryString["ID"]);
                            UPDATECOMENT.COMENTARIO = tbComentario.Text;
                            UPDATECOMENT.USERID_COMENTARIO = userid;
                            UPDATECOMENT.DATA_COMENTARIO = DateTime.Now;
                            DC.Orcamentos_Comentarios.InsertOnSubmit(UPDATECOMENT);
                            DC.SubmitChanges();

                            perfil = GetRoleName(userid);

                            if (perfil == "Administrador" || perfil == "SuperAdmin")
                                notificaCliente(UPDATECOMENT.ID_ORCAMENTO.Value);
                            if (perfil == "Cliente")
                                notificaAdmin(UPDATECOMENT.ID_ORCAMENTO.Value);
                        }
                    }
                    else
                        Response.Redirect("~/Default.aspx", false);
                    tbComentario.Text = "";
                    Response.Redirect("/onoff/home/GerirORCCliente.aspx?id=" + Request.QueryString["ID"], false);

                }
                else
                    ShowAlertMessage("O campo comentário é de preenchimento obrigatório!");
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void carregaORC()
        {
            try
            {
                string id = "";

                if (Request.QueryString["ID"] != null)
                {
                    id = Request.QueryString["ID"];

                    var pesquisaOrc = from or in DC.Orcamentos
                                      join parceiro in DC.Parceiros on or.USERID.Value equals parceiro.USERID.Value
                                      where or.ID == Convert.ToInt32(id)
                                      select new
                                      {
                                          
                                          NUMOR = or.CODIGO,
                                          LOJA = or.Loja.URL_FOTO,
                                          DATA_REGISTO = or.DATA_REGISTO.Value.ToShortDateString(),
                                          DATA_ULTIMA_MODIFICACAO = or.DATA_ULTIMA_MODIFICACAO.HasValue ? or.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString() : null,
                                          ESTADO = or.Orcamentos_Estado.DESCRICAO,
                                          IDESTADO = or.ID_ESTADO.Value,
                                          NOMECLIENTE = parceiro.NOME,
                                          CONTACTO = parceiro.TELEFONE,
                                          EMAIL = parceiro.EMAIL,
                                          NOMELOJA = or.Loja.NOME,
                                          MORADALOJA = or.Loja.MORADA + " " + or.Loja.CODPOSTAL + " " + or.Loja.LOCALIDADE,
                                          TELEFONELOJA = or.Loja.TELEFONE,
                                          DESCRICAO = or.DESCRICAO_DETALHADA_AVARIA,
                                          VALOR = or.VALOR_ORCAMENTO.Value,
                                          CONVERTIDO = or.CONVERTIDO_OR.Value
                                          
                                      };

                    foreach (var item in pesquisaOrc)
                    {
                        codOrcamento.InnerHtml = numOrcamento.InnerHtml = item.NUMOR.ToString();
                        logoLoja.Text += "<img src='../" + item.LOJA + "' />";
                        dataOrcamento.InnerHtml = item.DATA_REGISTO;
                        dataUltModOrcamento.InnerHtml = item.DATA_ULTIMA_MODIFICACAO;
                        estadoOrc.InnerHtml = item.ESTADO;
                        cliente.Text +=
                            "<li><i class='icon16 i-arrow-right-3'></i>Nome: <span class='red-smooth'><strong>" + item.NOMECLIENTE + "</strong></span></li>" +
                            "<li><i class='icon16 i-arrow-right-3'></i>Telefone: <span class='red-smooth'><strong>" + item.CONTACTO + "</strong></span></li>" +
                            "<li><i class='icon16 i-arrow-right-3'></i>Email: <span class='red-smooth'><strong>" + item.EMAIL + "</strong></span></li>";
                        loja.Text +=
                           "<li><i class='icon16 i-arrow-right-3'></i>Nome: <span class='red-smooth'><strong>" + item.NOMELOJA + "</strong></span></li>" +
                           "<li><i class='icon16 i-arrow-right-3'></i>Morada: <span class='red-smooth'><strong>" + item.MORADALOJA + "</strong></span></li>" +
                           "<li><i class='icon16 i-arrow-right-3'></i>Telefone: <span class='red-smooth'><strong>" + item.TELEFONELOJA + "</strong></span></li>";
                        linhasOrc.Text +=
                            "<tr><td class='center'>" + item.DESCRICAO + "</td><td class='center'>" + item.VALOR + " €</td></tr>";

                        if (item.IDESTADO == 3 || item.IDESTADO == 4 || item.IDESTADO == 5 || item.IDESTADO == 6)
                            modoLeitura();

                        if (item.CONVERTIDO == true)
                            btnAceitar.Text = "Gerir OR";

                    }

                    var pesquisaDetalhesOrC = from o in DC.Orcamentos
                                              where o.ID == Convert.ToInt32(id)
                                              select o;

                    foreach (var odc in pesquisaDetalhesOrC)
                    {
                        if (odc.ID_OR_ORIGEM != null && odc.ID_OR_ORIGEM.Value > 0)
                            btnConsultarOR.Visible = true;
                    }



                }
                else
                {
                    Response.Redirect("ListagemGeralOrcamentoCliente.aspx", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Log.ErrorException(ex);
                Log.Error("ERRO NO MÉTODO CARREGAORC EM GerirORCCliente.aspx");
                Log.Error(String.Format("Source: {0}{1}Message: {2}{3}{4}", ex.Source, Environment.NewLine, ex.Message, Environment.NewLine, ex.InnerException));
            }

        }

        protected void notificaCliente(int idOrcamento)
        {
            try
            {
                var pesquisaEmail = from i in DC.Orcamentos
                                    join p in DC.Parceiros on i.USERID.Value equals p.USERID.Value
                                    where i.ID == idOrcamento
                                    select new { EMAIL = p.EMAIL.ToLower().ToString(), CODIGO = i.CODIGO };

                foreach (var item in pesquisaEmail)
                {
                    Thread t = new Thread(() => EnviarEmailNotificaCliente(item.EMAIL, item.CODIGO));
                    t.Start();
                    timer1.Interval = 5;
                    timer1.Enabled = true;
                    Thread tempo = new Thread(() => timer1.Start());
                    tempo.Start();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void notificaAdmin(int idOrcamento)
        {
            try
            {
                var pesquisaEmail = from i in DC.Orcamentos
                                    join p in DC.aspnet_Memberships on i.USERID.Value equals p.UserId
                                    where i.ID == idOrcamento
                                    select new { EMAIL = p.LoweredEmail.ToString(), CODIGO = i.CODIGO };

                foreach (var item in pesquisaEmail)
                {
                    Thread t = new Thread(() => EnviarEmailNotificaAdmin(item.EMAIL, item.CODIGO));
                    t.Start();
                    timer2.Interval = 5;
                    timer2.Enabled = true;
                    Thread tempo = new Thread(() => timer2.Start());
                    tempo.Start();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        public void EnviarEmailNotificaCliente(string email, string codigo)
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
            url = "http://www.dygus.com/onoff/";
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Novo Comentário Registado";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Cliente,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criado um novo comentário no seu orçamento com o código " + codigo + " na sua conta de cliente. Por favor clique <a href=" + url + ">aqui</a> para consultar.<br/><br/>" +
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

        public void EnviarEmailNotificaAdmin(string email, string codigo)
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
            url = "http://www.dygus.com/onoff/";
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Novo Comentário Registado";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Administrador,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criado um novo comentário no orçamento com o código " + codigo + ". Por favor clique <a href=" + url + ">aqui</a> para consultar.<br/><br/>" +
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

        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }

        protected bool verificaExisteOR()
        {
            bool result = false;
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    myid = Request.QueryString["ID"];

                    var pesquisa = from p in DC.Orcamentos
                                   where p.ID == Convert.ToInt32(myid)
                                   select p;

                    foreach (var item in pesquisa)
                    {
                        if (item.ID_OR_ORIGEM != null && item.ID_OR_ORIGEM.Value > 0 && item.VALOR_ORCAMENTO.Value > 0)
                        {
                            ActualizaEstaOR(item.ID_OR_ORIGEM.Value, item.VALOR_ORCAMENTO.Value);
                            result = true;
                        }
                    }
                }
                else
                    Response.Redirect("GerirORCliente.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
            return result;
        }

        public void ActualizaEstaOR(int idOr, double valor)
        {
            try
            {
                LINQ_DB.Ordem_Reparacao UPDATEESTADO = new LINQ_DB.Ordem_Reparacao();

                var pesquisaor = from p in DC.Ordem_Reparacaos
                                 where p.ID == idOr
                                 select p;

                UPDATEESTADO = pesquisaor.First();
                UPDATEESTADO.ID_ESTADO = 2;
                UPDATEESTADO.VALOR_PREVISTO_REPARACAO = valor;
                UPDATEESTADO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                DC.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void aprovaOrc()
        {
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    myid = Request.QueryString["ID"];

                    LINQ_DB.Orcamento update = new LINQ_DB.Orcamento();

                    var pesquisa = from p in DC.Orcamentos
                                   where p.ID == Convert.ToInt32(myid)
                                   select p;

                    update = pesquisa.First();
                    update.CLIENTE_ACEITOU = true;
                    update.CLIENTE_REJEITOU = false;
                    update.ID_ESTADO = 2;
                    update.DATA_CONCLUSAO = DateTime.Now;
                    update.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    DC.SubmitChanges();

                    foreach (var item in pesquisa)
                    {
                        if (item.ID_OR_ORIGEM != null && item.ID_OR_ORIGEM.Value > 0)
                            Response.Redirect("EditarOrdemReparacao.aspx?id=" + item.ID_OR_ORIGEM.Value, false);
                    }

                }
                else
                    Response.Redirect("GerirORCliente.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnAceitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAceitar.Text == "Converter em OR")
                {
                    if (verificaExisteOR())
                        aprovaOrc();
                    else
                        criarNovaORFromOrc();
                }
                else
                    aprovaOrc();

            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void modoLeitura()
        {
            btnAceitar.Enabled = btnRejeitar.Enabled = btnInserirComent.Enabled = false;
            tbComentario.ReadOnly = true;
        }

        protected void btnRejeitar_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                try
                {
                    LINQ_DB.Orcamento UPDATE = new LINQ_DB.Orcamento();

                    var pesquisaORC = from o in DC.Orcamentos
                                      where o.ID == Convert.ToInt32(myid)
                                      select o;

                    UPDATE = pesquisaORC.First();
                    UPDATE.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    UPDATE.CLIENTE_ACEITOU = false;
                    UPDATE.CLIENTE_REJEITOU = true;
                    if (btnRejeitar.Text == "Cancelar Orçamento")
                    {
                        UPDATE.ID_ESTADO = 4;
                    }
                    else if (btnRejeitar.Text == "Rejeitar Orçamento")
                    {
                        UPDATE.ID_ESTADO = 3;
                    }

                    DC.SubmitChanges();

                    Response.Redirect("ListarOrcamentoCliente.aspx", false);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            else
                return;
        }

        protected void btImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ImprimeORCCliente.aspx?ID=" + myid, false);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        public void EnviarEmailClienteNovaOR(string email)
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
            url = "http://www.dygus.com/onoff/";
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Nova Ordem de Reparação";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Cliente,<br/><br/>" +
                "Foi criada uma nova ordem de reparação em seu nome.<br/><br/>" +
                "Para consultar o detalhe da mesma, por favor clique: <a href=" + url + ">AQUI</a>.<br/><br/>" +
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
                SQLLog.registaLogBD(useridP, DateTime.Now, "Inserir ordem de reparação", "Foi enviado um email para: " + email.ToString() + " para criação de nova ordem de reparação.", true);
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void criarNovaORFromOrc()
        {
            try
            {
                if (Request.QueryString["ID"] != null)
                {
                    myid = Request.QueryString["ID"];

                    var pesquisaDetalhesOrC = from o in DC.Orcamentos
                                              where o.ID == Convert.ToInt32(myid)
                                              select o;

                    foreach (var ORCDetalhe in pesquisaDetalhesOrC)
                    {

                        string codNovaOR = "";

                        var config = from conf in DC.Configuracaos
                                     where conf.ID == 1
                                     select conf;

                        foreach (var item in config)
                        {
                            codNovaOR = item.INICIAL + "-" + item.CODIGO;
                        }

                        LINQ_DB.Ordem_Reparacao NOVAORDEMREPARACAO = new LINQ_DB.Ordem_Reparacao();
                        NOVAORDEMREPARACAO.CODIGO = codNovaOR;
                        NOVAORDEMREPARACAO.USERID = ORCDetalhe.USERID;
                        NOVAORDEMREPARACAO.ID_LOJA = ORCDetalhe.ID_LOJA;
                        NOVAORDEMREPARACAO.ATRIBUIDA = false;
                        NOVAORDEMREPARACAO.CLIENTE_NOTIFICADO = false;
                        NOVAORDEMREPARACAO.VALOR_FINAL = 0;
                        NOVAORDEMREPARACAO.VALOR_CUSTO = 0;
                        NOVAORDEMREPARACAO.VALOR_PREVISTO_REPARACAO = ORCDetalhe.VALOR_ORCAMENTO.Value;
                        NOVAORDEMREPARACAO.VALOR_ADICIONAL_REPARACAO = 0;
                        NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = null;
                        NOVAORDEMREPARACAO.ID_EQUIPAMENTO_AVARIADO = ORCDetalhe.ID_EQUIPAMENTO_AVARIADO;
                        NOVAORDEMREPARACAO.ID_ESTADO = 1;
                        NOVAORDEMREPARACAO.DESCRICAO_DETALHADA_PROBLEMA = ORCDetalhe.DESCRICAO_DETALHADA_AVARIA;
                        NOVAORDEMREPARACAO.ID_TRABALHOS_ADICIONAIS = ORCDetalhe.ID_TRABALHOS_ADICIONAIS;

                        LINQ_DB.Equipamento_Avariado_Bloqueio NOVOESTADOBLOQUEIO = new LINQ_DB.Equipamento_Avariado_Bloqueio();
                        NOVOESTADOBLOQUEIO.BLOQUEADO = false;
                        NOVOESTADOBLOQUEIO.ID_OPERADORA = null;
                        NOVOESTADOBLOQUEIO.OBSERVACOES = "N/D";
                        DC.Equipamento_Avariado_Bloqueios.InsertOnSubmit(NOVOESTADOBLOQUEIO);
                        DC.SubmitChanges();

                        NOVAORDEMREPARACAO.ID_ESTADO_BLOQUEIO = NOVOESTADOBLOQUEIO.ID;

                        LINQ_DB.Equipamento_Avariado_Acessorio NOVOACESSORIO = new LINQ_DB.Equipamento_Avariado_Acessorio();
                        NOVOACESSORIO.BATERIA = false;
                        NOVOACESSORIO.CARREGADOR = false;
                        NOVOACESSORIO.CARTAO_MEM = false;
                        NOVOACESSORIO.BOLSA = false;
                        NOVOACESSORIO.CARTAO_SIM = false;
                        NOVOACESSORIO.CAIXA = false;
                        NOVOACESSORIO.OUTROS = "N/D";
                        NOVOACESSORIO.OBSERVACOES = "N/D";
                        DC.Equipamento_Avariado_Acessorios.InsertOnSubmit(NOVOACESSORIO);
                        DC.SubmitChanges();

                        NOVAORDEMREPARACAO.ID_ACESSORIOS = NOVOACESSORIO.ID;
                        NOVAORDEMREPARACAO.ID_TIMING_REPARACAO = 1;

                        LINQ_DB.Equipamento_Avariado_GarantiaTipo NOVOEQUIPAVARIADOGARANTIA = new LINQ_DB.Equipamento_Avariado_GarantiaTipo();
                        NOVOEQUIPAVARIADOGARANTIA.ID_TIPO_GARANTIA = null;
                        NOVOEQUIPAVARIADOGARANTIA.OBSERVACOES = "N/D";
                        DC.Equipamento_Avariado_GarantiaTipos.InsertOnSubmit(NOVOEQUIPAVARIADOGARANTIA);
                        DC.SubmitChanges();

                        NOVAORDEMREPARACAO.ID_TIPO_GARANTIA = NOVOEQUIPAVARIADOGARANTIA.ID;

                        NOVAORDEMREPARACAO.DATA_REGISTO = DateTime.Now;
                        NOVAORDEMREPARACAO.DATA_PREVISTA_CONCLUSAO = NOVAORDEMREPARACAO.DATA_REGISTO;
                        NOVAORDEMREPARACAO.DATA_CONCLUSAO = null;
                        NOVAORDEMREPARACAO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                        NOVAORDEMREPARACAO.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO = null;

                        DC.Ordem_Reparacaos.InsertOnSubmit(NOVAORDEMREPARACAO);
                        DC.SubmitChanges();
                        SQLLog.registaLogBD(useridP, DateTime.Now, "Inserir ordem de reparação", "Foi criada nova OR com o código: " + NOVAORDEMREPARACAO.CODIGO.ToString() + ".", true);

                        var parceiro = from p in DC.Parceiros
                                       where p.USERID.Value == NOVAORDEMREPARACAO.USERID
                                       select p;

                        foreach (var item in parceiro)
                        {
                            if (item.EMAIL != "dygussat@dygus.com")
                            {
                                Thread t = new Thread(() => EnviarEmailClienteNovaOR(item.EMAIL));
                                t.Start();
                                timer2.Interval = 5;
                                timer2.Enabled = true;
                                Thread tempo = new Thread(() => timer2.Start());
                                tempo.Start();
                            }
                        }


                        LINQ_DB.Orcamento update = new LINQ_DB.Orcamento();

                        var pesquisa = from p in DC.Orcamentos
                                       where p.ID == Convert.ToInt32(myid)
                                       select p;

                        update = pesquisa.First();
                        update.CLIENTE_ACEITOU = true;
                        update.CLIENTE_REJEITOU = false;
                        update.ID_ESTADO = 2;
                        update.DATA_CONCLUSAO = DateTime.Now;
                        update.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                        update.ID_OR_ORIGEM = NOVAORDEMREPARACAO.ID;
                        DC.SubmitChanges();

                        foreach (var item in pesquisa)
                        {
                            if (item.ID_OR_ORIGEM != null && item.ID_OR_ORIGEM.Value > 0)
                            {
                                ShowAlertMessage("Foi criada a nova ordem de reparação com o código " + NOVAORDEMREPARACAO.CODIGO.ToString() + "!");
                                Response.Redirect("EditarOrdemReparacao.aspx?id=" + item.ID_OR_ORIGEM.Value, false);
                            }
                            else
                            {
                                ShowAlertMessage("Foi criada a nova ordem de reparação com o código " + NOVAORDEMREPARACAO.CODIGO.ToString() + "!");
                                Response.Redirect("ListagemGeralOrdensReparacao.aspx", false);
                            }
                        }


                    }

                }
                else
                    Response.Redirect("GerirORCliente.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnConsultarOR_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ID"] != null)
            {
                myid = Request.QueryString["ID"];

                var pesquisaDetalhesOrC = from o in DC.Orcamentos
                                          where o.ID == Convert.ToInt32(myid)
                                          select o;

                foreach (var ORCDetalhe in pesquisaDetalhesOrC)
                {
                    if (ORCDetalhe.ID_OR_ORIGEM != null && ORCDetalhe.ID_OR_ORIGEM.Value > 0)
                        EditarORAssociada(ORCDetalhe.ID_OR_ORIGEM.Value);
                }
            }
        }

        protected void EditarORAssociada(int idOR)
        {
            if (idOR > 0)
            {
                Response.Redirect("EditarOrdemReparacao.aspx?id=" + idOR.ToString(), false);
            }
            else
            {
                ShowAlertMessage("Não existe qualquer ordem de reparação associada a este orçamento!");
            }
        }
    }
}
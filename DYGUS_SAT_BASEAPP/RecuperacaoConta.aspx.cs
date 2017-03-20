using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP
{
    public partial class RecuperacaoConta : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();

        protected void Page_Load(object sender, EventArgs e)
        {
            tbusername.Focus();
        }

        protected bool validateUserEmail(string email)
        {
            bool resultadofinal = false;

            try
            {
                int emailexiste = 0;

                var userResults = from u in DC.aspnet_Memberships
                                  where u.Email == email
                                  select u;
                emailexiste = Enumerable.Count(userResults);

                if (emailexiste == 1)
                {
                    resultadofinal = true;
                }
                else
                {
                    resultadofinal = false;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);

            }

            return resultadofinal;
        }

        private string GeraSenha()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random clsRan = new Random();
            Int32 tamanhoSenha = clsRan.Next(6, 18);

            string senha = "";
            for (Int32 i = 0; i <= tamanhoSenha; i++)
            {
                senha += guid.Substring(clsRan.Next(1, guid.Length), 1);
            }

            return senha;
        }

        public void EnviarEmailnovapassword(string email, string pass)
        {

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Lisboa22!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            string url = "";
            url = "onoff";
            string mail = "";
            mail = "mailto:ricardoshm@gmail.com";

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "Dygus :: Support & AfterSales");
            msg.To.Add(new MailAddress(email.ToString()));

            msg.Subject = "Dygus :: Support & AfterSales - Recuperação de Conta";
            msg.IsBodyHtml = true;


            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Utilizador, </br></br> Os seus dados de acesso à Plataforma de Assistência Técnica Dygus :: Support & AfterSales são: <br><br>" +
                "Nome de Utilizador: '" + email.ToString() + "'<br>" +
                "NOVA Palavra-Passe: '" + pass.ToString() + "' <br><br><br>" +
                "ATENÇÃO: Aconselhamos, por motivos de segurança, a alterar a password gerada automaticamente recebida neste email para uma password pessoal na sua área reservada. <br><br>" +
                "Pode aceder ao Painel de Administração usando a sua nova Password, clique <a href=" + url + ">aqui</a>.<br><br>" +
                "A caixa postal emissora deste mail é exclusivamente para envio de mensagens. Por favor não responda a este email. Para qualquer assunto relacionado com a Plataforma de Assistência Técnica Dygus :: Support & AfterSales, por favor contacte <a href=" + mail + "> ricardoshm@gmail.com<br /><br/>" +
                "<img src=cid:companylogo alt=.  />" +
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
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void btnRecover_Click(object sender, EventArgs e)
        {
            string username = tbusername.Text;
            erro.InnerHtml = "";

            if (String.IsNullOrEmpty(username))
            {
                erro.Visible = true;
                erro.InnerHtml = "O campo Nome de Utilizador é obrigatório!";
                tbusername.Focus();
            }
            else
            {
                try
                {
                    string user = "";

                    if (validateUserEmail(username) == true)
                    {
                        var userResults = from u in DC.aspnet_Memberships
                                          where u.Email == username
                                          select u;
                        foreach (var item in userResults)
                        {
                            user = item.aspnet_User.UserName;
                        }

                        MembershipUser mu = Membership.GetUser(user);

                        MembershipProvider provider = Membership.Provider;

                        string password = "";
                        password = GeraSenha();

                        string passwordencriptada = "";

                        passwordencriptada = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");

                        LINQ_DB.aspnet_Membership aspm = new LINQ_DB.aspnet_Membership();

                        aspm = userResults.First();

                        aspm.Password = passwordencriptada;
                        DC.SubmitChanges();

                        Thread t = new Thread(() => EnviarEmailnovapassword(username, password));
                        t.Start();
                        timer1.Interval = 5;
                        timer1.Enabled = true;
                        Thread tempo = new Thread(() => timer1.Start());
                        tempo.Start();

                        sucesso.Visible = true;
                        sucesso.InnerHtml = "Palavra-Passe enviada para o seu email de registo!";

                    }
                    else
                    {
                        erro.Visible = true;
                        erro.InnerHtml = "Nome de Utilizador inválido inválido!";
                    }

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }
    }
}
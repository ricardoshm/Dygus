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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InsereClientes : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Tecnico" || regra == "Reparador")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);

            if (!Page.IsPostBack)
            {
                int codNovoCliente = 0;

                try
                {
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "C"
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
                        tbcodcliente.Text = numeroCliente;
                    }

                    carregaTiposCliente();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected bool validaCampos()
        {
            string nome = "";
            string morada = "";
            string localidade = "";
            string contacto = "";

            nome = tbnome.Text;
            morada = tbmorada.Text;
            localidade = tblocalidade.Text;
            contacto = tbcontacto.Text;

            if (ddltipocliente.SelectedItem.Text == "Por favor seleccione...")
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Tipo de Cliente é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(nome))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Nome de Cliente é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(morada))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Morada de Cliente é obrigatório!";
                return false;
            }


            if (String.IsNullOrEmpty(localidade))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Localidade de Cliente é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(contacto))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Contacto de Cliente é obrigatório!";
                return false;
            }


            

            return true;
        }

        protected void recalculaCodCliente()
        {
            int codNovoCliente = 0;

            try
            {
                var config = from conf in DC.Configuracaos
                             where conf.INICIAL == "C"
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
                    tbcodcliente.Text = numeroCliente;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnGrava_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                try
                {
                    bool valido = new bool();

                    erro.Visible = false;
                    valido = true;

                    string email = "";
                    email = tbemail.Text;

                    string mensagemErro = "";

                    int emailExiste = 0;

                    var emailResults = from u in DC.aspnet_Memberships
                                       where u.Email == tbemail.Text.ToString()
                                       select u;

                    emailExiste = Enumerable.Count(emailResults);

                    if (emailExiste == 1)
                    {
                        valido = false;
                        mensagemErro = mensagemErro + "O endereço de email inserido já se encontra registado!";
                    }

                    int nomeExiste = 0;

                    var nomeResults = from u in DC.aspnet_Users
                                      where u.UserName == tbnome.Text.ToString()
                                      select u;

                    nomeExiste = Enumerable.Count(nomeResults);

                    if (nomeExiste == 1)
                    {
                        valido = false;
                        mensagemErro = mensagemErro + "O nome de cliente inserido já se encontra registado!";
                    }


                    if (valido == false)
                    {
                        erro.Visible = errorMessage.Visible = true;
                        errorMessage.InnerHtml = mensagemErro;
                    }

                    if (valido == true)
                    {
                        AdicionarCliente();
                    }

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void AdicionarCliente()
        {
            try
            {
                DateTime hoje = new DateTime();
                hoje = DateTime.Now;

                Guid aplid = new Guid();

                var app = from u in DC.aspnet_Applications
                          select u;

                foreach (var item in app)
                {
                    aplid = item.ApplicationId;
                }

                Guid roleId = new Guid();

                var role = from u in DC.aspnet_Roles
                           where u.RoleName == "Cliente"
                           select u;

                foreach (var item in role)
                {
                    roleId = item.RoleId;
                }

                Guid IDUser = Guid.NewGuid();

                LINQ_DB.aspnet_UsersInRole UserRole = new LINQ_DB.aspnet_UsersInRole();
                UserRole.RoleId = roleId;
                UserRole.UserId = IDUser;

                string password = "";
                password = GeraSenha();

                string passwordencriptada = "";

                passwordencriptada = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");

                string salt = "";
                salt = "CMqLUtNGYinaiK+46vS/Vw==";

                LINQ_DB.aspnet_Membership MEMBER = new LINQ_DB.aspnet_Membership();
                MEMBER.ApplicationId = aplid;
                MEMBER.CreateDate = hoje;
                if (!String.IsNullOrEmpty(tbemail.Text))
                    MEMBER.Email = tbemail.Text.ToString();
                else
                    MEMBER.Email = "dygussat@dygus.com";
                MEMBER.FailedPasswordAnswerAttemptCount = 0;
                MEMBER.FailedPasswordAttemptCount = 0;
                MEMBER.IsApproved = false;
                MEMBER.IsLockedOut = false;
                MEMBER.CreateDate = hoje;
                MEMBER.LastLoginDate = hoje;
                MEMBER.LastPasswordChangedDate = hoje;
                MEMBER.LastLockoutDate = hoje;
                MEMBER.FailedPasswordAnswerAttemptWindowStart = hoje;
                MEMBER.FailedPasswordAttemptWindowStart = hoje;
                if (!String.IsNullOrEmpty(tbemail.Text))
                    MEMBER.LoweredEmail = tbemail.Text.ToString().ToLower();
                else
                    MEMBER.LoweredEmail = "dygussat@dygus.com";
                MEMBER.Password = passwordencriptada;
                MEMBER.PasswordFormat = 1;
                MEMBER.PasswordSalt = salt;
                MEMBER.Comment = tbobs.Text;
                MEMBER.UserId = IDUser;

                LINQ_DB.aspnet_User USER = new LINQ_DB.aspnet_User();
                USER.IsAnonymous = true;
                USER.LastActivityDate = hoje;
                USER.UserName = tbnome.Text.ToString().Replace(" ", "");
                USER.LoweredUserName = tbnome.Text.ToString().ToLower().Replace(" ", "");
                USER.ApplicationId = aplid;
                USER.UserId = IDUser;
                USER.aspnet_UsersInRoles.Add(UserRole);
                USER.aspnet_Membership = MEMBER;

                LINQ_DB.Parceiro NOVOCLIENTE = new LINQ_DB.Parceiro();
                NOVOCLIENTE.aspnet_User = USER;
                NOVOCLIENTE.CODIGO = tbcodcliente.Text.ToString();
                NOVOCLIENTE.ID_TIPO_PARCEIRO = Convert.ToInt32(ddltipocliente.SelectedItem.Value);
                NOVOCLIENTE.DATA_REGISTO = DateTime.Now;
                NOVOCLIENTE.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                NOVOCLIENTE.DATA_VALIDADE_LOGIN = NOVOCLIENTE.DATA_REGISTO.Value.AddYears(1);
                NOVOCLIENTE.ACTIVO = false;
                NOVOCLIENTE.NOME = tbnome.Text.ToString();
                NOVOCLIENTE.MORADA = tbmorada.Text.ToString();
                if (String.IsNullOrEmpty(tbcodpostal.Text))
                    NOVOCLIENTE.CODPOSTAL = "0000000";
                else
                    NOVOCLIENTE.CODPOSTAL = tbcodpostal.Text.ToString();
                NOVOCLIENTE.LOCALIDADE = tblocalidade.Text.ToString();
                if (!String.IsNullOrEmpty(tbemail.Text))
                    NOVOCLIENTE.EMAIL = tbemail.Text.ToString();
                else
                    NOVOCLIENTE.EMAIL = "dygussat@dygus.com";
                NOVOCLIENTE.TELEFONE = tbcontacto.Text.ToString();
                if (String.IsNullOrEmpty(tbnif.Text))
                    NOVOCLIENTE.NIF = 999999999;
                else
                    NOVOCLIENTE.NIF = Convert.ToInt32(tbnif.Text);
                if (String.IsNullOrEmpty(tbobs.Text))
                    NOVOCLIENTE.OBSERVACOES = "N/D";
                else
                    NOVOCLIENTE.OBSERVACOES = tbobs.Text.ToString();
                NOVOCLIENTE.CLIENTE = true;

                DC.aspnet_Users.InsertOnSubmit(NOVOCLIENTE.aspnet_User);
                DC.SubmitChanges();

                LINQ_DB.Configuracao NOVOCODIGOCLIENTE = new LINQ_DB.Configuracao();
                var config = from conf in DC.Configuracaos
                             where conf.INICIAL == "C"
                             select conf;
                NOVOCODIGOCLIENTE = config.First();
                NOVOCODIGOCLIENTE.CODIGO = NOVOCODIGOCLIENTE.CODIGO + 1;


                if (NOVOCLIENTE.EMAIL != "dygussat@dygus.com")
                {
                    Thread t = new Thread(() => EnviarEmailCliente(MEMBER.Email, IDUser, password.ToString()));
                    t.Start();
                    timer1.Interval = 5;
                    timer1.Enabled = true;
                    Thread tempo = new Thread(() => timer1.Start());
                    tempo.Start();
                }

                if (NOVOCLIENTE.EMAIL == "dygussat@dygus.com")
                    NOVOCLIENTE.ACTIVO = true;
                DC.SubmitChanges();
                sucesso.Visible = sucessoMessage.Visible = true;
                sucessoMessage.InnerHtml = "Cliente " + NOVOCLIENTE.CODIGO.ToString() + " registado com êxito! Foi enviado um email para o cliente por forma a este concluir o registo.";
                limpaCampos();

                recalculaCodCliente();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        private static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        private string EncodePassword(byte passFormat, string passtext, string passwordSalt)
        {
            if (passFormat.Equals(0)) // passwordFormat="Clear" (0)         
                return passtext;
            else
            {
                byte[] bytePASS = Encoding.Unicode.GetBytes(passtext);
                byte[] byteSALT = Convert.FromBase64String(passwordSalt);
                byte[] byteRESULT = new byte[byteSALT.Length + bytePASS.Length + 1];
                System.Buffer.BlockCopy(byteSALT, 0, byteRESULT, 0, byteSALT.Length);
                System.Buffer.BlockCopy(bytePASS, 0, byteRESULT, byteSALT.Length, bytePASS.Length);
                if (passFormat.Equals(1)) // passwordFormat="Hashed" (1)         
                {
                    HashAlgorithm ha = HashAlgorithm.Create(Membership.HashAlgorithmType);
                    return (Convert.ToBase64String(ha.ComputeHash(byteRESULT)));
                }
                else // passwordFormat="Encrypted" (2)        
                {
                    HashAlgorithm ha = HashAlgorithm.Create(Membership.HashAlgorithmType);
                    return (Convert.ToBase64String(ha.ComputeHash(byteRESULT)));
                }
            }
        }

        public static string GenerateSalt()
        {  //generate salt
            byte[] data = new byte[0x10];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(pwd, salt);
            string hashedPwd =
                  FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "SHA1");
            hashedPwd = String.Concat(hashedPwd, salt);
            return hashedPwd;
        }

        protected void carregaTiposCliente()
        {
            try
            {
                var clientes = from c in DC.Parceiro_Tipos
                               where c.ID == 1 || c.ID == 2
                               select c;

                ddltipocliente.DataTextField = "DESCRICAO";
                ddltipocliente.DataValueField = "ID";
                ddltipocliente.DataSource = clientes;
                ddltipocliente.DataBind();
                ddltipocliente.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
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
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
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

        protected void limpaCampos()
        {
            tbnome.Text = "";
            tbmorada.Text = "";
            tbcodpostal.Text = "";
            tblocalidade.Text = "";
            tbcontacto.Text = "";
            tbemail.Text = "";
            tbnif.Text = "";
            tbobs.Text = "";
            ddltipocliente.ClearSelection();
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = "";
            tbmorada.Text = "";
            tbcodpostal.Text = "";
            tblocalidade.Text = "";
            tbcontacto.Text = "";
            tbemail.Text = "";
            tbnif.Text = "";
            tbobs.Text = "";
            ddltipocliente.ClearSelection();
        }
    }
}
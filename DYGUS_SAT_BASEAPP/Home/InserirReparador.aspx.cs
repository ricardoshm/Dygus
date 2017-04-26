using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
    public partial class InserirReparador : Telerik.Web.UI.RadAjaxPage
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

                if (regra != "Administrador" && regra != "SuperAdmin")
                    Response.Redirect("Default.aspx", false);
            }
            //(Page.Master.FindControl("reparadores") as HtmlControl).Attributes.Add("class", "has-sub active");
            //(Page.Master.FindControl("reparadoresinsert") as HtmlControl).Attributes.Add("class", "active");

            if (!Page.IsPostBack)
            {
                int codNovoCliente = 0;

                try
                {
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "R"
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
                        tbcodreparador.Text = numeroCliente;
                    }

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
            string email = "";

            nome = tbnome.Text;
            morada = tbmorada.Text;
            localidade = tblocalidade.Text;
            contacto = tbcontacto.Text;
            email = tbemail.Text;



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

            if (contacto == null)
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Contacto de Cliente é obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(email))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Email de Cliente é obrigatório!";
                return false;
            }

            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (!rg.IsMatch(email))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O Email inserido é inválido!";
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
                             where conf.INICIAL == "R"
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
                    tbcodreparador.Text = numeroCliente;
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
                        mensagemErro = mensagemErro + "O nome de reparador inserido já se encontra registado!";
                    }

                    if (valido == false)
                    {
                        erro.Visible = errorMessage.Visible = true;
                        errorMessage.InnerHtml = mensagemErro;
                    }

                    if (valido == true)
                    {
                        AdicionarReparador();
                    }

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void AdicionarReparador()
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
                           where u.RoleName == "Reparador"
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
                MEMBER.Email = tbemail.Text.ToString();
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
                MEMBER.LoweredEmail = tbemail.Text.ToString().ToLower();
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

                LINQ_DB.Parceiro NOVOREPARADOR = new LINQ_DB.Parceiro();
                NOVOREPARADOR.aspnet_User = USER;
                NOVOREPARADOR.CODIGO = tbcodreparador.Text.ToString();
                if (ckbCliente.SelectedToggleState.Selected == true)
                    NOVOREPARADOR.ID_TIPO_PARCEIRO = 4;
                else
                    NOVOREPARADOR.ID_TIPO_PARCEIRO = 3;
                NOVOREPARADOR.DATA_REGISTO = DateTime.Now;
                NOVOREPARADOR.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                NOVOREPARADOR.DATA_VALIDADE_LOGIN = NOVOREPARADOR.DATA_REGISTO.Value.AddYears(1);
                NOVOREPARADOR.ACTIVO = false;
                NOVOREPARADOR.NOME = tbnome.Text.ToString();
                NOVOREPARADOR.MORADA = tbmorada.Text.ToString();
                if (String.IsNullOrEmpty(tbcodpostal.Text))
                    NOVOREPARADOR.CODPOSTAL = "0000000";
                else
                    NOVOREPARADOR.CODPOSTAL = tbcodpostal.Text.ToString();
                NOVOREPARADOR.LOCALIDADE = tblocalidade.Text.ToString();
                NOVOREPARADOR.EMAIL = tbemail.Text.ToString();
                NOVOREPARADOR.TELEFONE = tbcontacto.Text.ToString();
                if (String.IsNullOrEmpty(tbnif.Text))
                    NOVOREPARADOR.NIF = 999999999;
                else
                    NOVOREPARADOR.NIF = Convert.ToInt32(tbnif.Text);
                if (String.IsNullOrEmpty(tbobs.Text))
                    NOVOREPARADOR.OBSERVACOES = "N/D";
                else
                    NOVOREPARADOR.OBSERVACOES = tbobs.Text.ToString();
                NOVOREPARADOR.CLIENTE = ckbCliente.SelectedToggleState.Selected;
                DC.aspnet_Users.InsertOnSubmit(NOVOREPARADOR.aspnet_User);
                DC.SubmitChanges();

                if (NOVOREPARADOR.CLIENTE == false)
                {
                    LINQ_DB.Configuracao NOVOCODIGOCLIENTE = new LINQ_DB.Configuracao();
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "R"
                                 select conf;
                    NOVOCODIGOCLIENTE = config.First();
                    NOVOCODIGOCLIENTE.CODIGO = NOVOCODIGOCLIENTE.CODIGO + 1;
                    DC.SubmitChanges();
                }

                if (NOVOREPARADOR.CLIENTE == true)
                {
                    LINQ_DB.Configuracao NOVOCODIGOCLIENTE = new LINQ_DB.Configuracao();
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "RC"
                                 select conf;
                    NOVOCODIGOCLIENTE = config.First();
                    NOVOCODIGOCLIENTE.CODIGO = NOVOCODIGOCLIENTE.CODIGO + 1;
                    DC.SubmitChanges();
                }

                Thread t = new Thread(() => EnviarEmailReparador(MEMBER.Email, IDUser, password.ToString()));
                t.Start();
                timer1.Interval = 10;
                timer1.Enabled = true;
                Thread tempo = new Thread(() => timer1.Start());
                tempo.Start();
                sucesso.Visible = sucessoMessage.Visible = true;
                sucessoMessage.InnerHtml = "Reparador Externo " + NOVOREPARADOR.CODIGO.ToString() + " registado com êxito! Foi enviado um email para o reparador por forma a este concluir o registo.";
                limpaCampos();
                listagemreparadoresregistados.Rebind();
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

        public void EnviarEmailReparador(string email, Guid UserID, string pass)
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

            msg.Subject = "DYGUS - SAT | Registo de Conta de Reparador";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Reparador,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criada uma nova conta de reparador externo em seu nome com os seguintes dados de acesso:<br/><br/>" +
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
        }

        protected void listagemreparadoresregistados_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaClientes = from cliente in DC.Parceiros
                                      where cliente.ID_TIPO_PARCEIRO == 3 || cliente.ID_TIPO_PARCEIRO == 4
                                      select new
                                      {
                                          ID = cliente.ID,
                                          CODIGO = cliente.CODIGO,
                                          NOME = cliente.NOME,
                                          EMAIL = cliente.EMAIL,
                                          CONTACTO = cliente.TELEFONE,
                                          NIF = cliente.NIF,
                                          CONTA_ACTIVA = cliente.ACTIVO.Value,
                                      };

                listagemreparadoresregistados.DataSourceID = "";
                listagemreparadoresregistados.DataSource = carregaClientes;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void ckbCliente_CheckedChanged(object sender, EventArgs e)
        {
            int codNovoCliente = 0;

            if (ckbCliente.SelectedToggleState.Selected == true)
            {
                try
                {
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "RC"
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
                        tbcodreparador.Text = numeroCliente;
                    }

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            if (ckbCliente.SelectedToggleState.Selected == false)
            {
                try
                {
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "R"
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
                        tbcodreparador.Text = numeroCliente;
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
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class InserirOrdemReparacao : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();
        protected System.Timers.Timer timer2 = new System.Timers.Timer();
        protected System.Timers.Timer timer3 = new System.Timers.Timer();
        string letras = "";
        int numeros = 0;
        string nOr = "";

        Guid useridPesquisaCliente = new Guid();
        Guid useridClienteFrequente = new Guid();
        string idEquipSubst = "";
        Guid useridP = new Guid();
        protected void Page_Load(object sender, EventArgs e)
        {
            //historicoCliente.Style.Add("display", "none");
            //dadosNovoCliente.Style.Add("display", "none");
            //equipSubst.Style.Add("display", "none");


            Guid Role = new Guid();
            string UserName = "";

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    useridP = item.UserId;
                }

                var utilizador = from util in DC.aspnet_Users
                                 where util.UserId == useridP
                                 select util;

                foreach (var item in utilizador)
                {
                    UserName = item.UserName;
                }

                var p = from d in DC.aspnet_UsersInRoles
                        where d.UserId == useridP
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

            if (Session["returnedValuesClientes"] != null)
            {
                try
                {
                    List<string> resultsClientes = (List<string>)Session["returnedValuesClientes"];
                    tbcodcliente.Text += resultsClientes[0];
                    tbnome.Text += resultsClientes[1];
                    tbmorada.Text += resultsClientes[2];
                    tbcodpostal.Text += resultsClientes[3];
                    tblocalidade.Text += resultsClientes[4];
                    tbcontacto.Text += resultsClientes[5];
                    tbemail.Text += resultsClientes[6];
                    tbnif.Text += resultsClientes[7];
                    tbobs.Text += resultsClientes[8];
                    codClienteExistente.Value = resultsClientes[0].ToString();

                    tipocliente.Visible = false;
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }

            Session.Clear();


            //if (rbbloqueiooperadora.SelectedToggleState.Selected)
            //{
            //    tboperadora.ReadOnly = false;
            //    tbobsEstadoEquipamento.Visible = true;
            //}
            //else
            //{
            //    tboperadora.ReadOnly = true;
            //    tbobsEstadoEquipamento.Visible = false;
            //}

            if (dataregisto.SelectedDate == null)
                dataregisto.SelectedDate = DateTime.Today;




            if (!Page.IsPostBack)
            {
                letras = RandomString(2);
                numeros = GeraRandomNumeros();
                nOr = letras + "-" + numeros;

                try
                {
                    int contaDuplicados = (from conf in DC.Configuracaos
                                           where conf.INICIAL == letras && conf.CODIGO == numeros
                                           select conf).Count();
                    if (contaDuplicados > 0)
                    {
                        while (contaDuplicados > 0)
                        {
                            letras = "";
                            numeros = 0;
                            nOr = "";

                            letras = RandomString(2);
                            numeros = GeraRandomNumeros();
                            nOr = letras + "-" + numeros;
                        }
                    }
                    else
                    {
                        letras = "";
                        numeros = 0;
                        nOr = "";

                        letras = RandomString(2);
                        numeros = GeraRandomNumeros();
                        nOr = letras + "-" + numeros;
                    }

                    LINQ_DB.Configuracao configuracao = new LINQ_DB.Configuracao();

                    var config = from conf in DC.Configuracaos
                                 where conf.ID == 1
                                 select conf;

                    configuracao = config.First();
                    configuracao.INICIAL = letras;
                    configuracao.CODIGO = Convert.ToInt32(numeros);
                    DC.SubmitChanges();


                    tbcodordemreparacao.Text = nOr.ToString();

                    //carregarMarcas();
                    carregaTiposGarantia();
                    //carregaOperadoras();
                    carregaTiposReparacao();
                    carregaLojas();
                    carregaTiposCliente();


                    int codNovoCliente = 0;

                    var configCliente = from confge in DC.Configuracaos
                                        where confge.INICIAL == "C"
                                        select confge;

                    LINQ_DB.Configuracao configuracaoCliente = new LINQ_DB.Configuracao();

                    configuracaoCliente = configCliente.First();
                    codNovoCliente = Convert.ToInt32(configuracaoCliente.CODIGO) + 1;

                    string numeroCliente = "";

                    string prefixoCliente = configuracaoCliente.INICIAL.ToString() + "-0000";

                    var confgg = from confiig in DC.Configuracaos
                                 select confiig;

                    foreach (var item in confgg)
                    {
                        numeroCliente = prefixoCliente + codNovoCliente.ToString();
                        tbcodcliente.Text = numeroCliente;
                    }


                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }

            }

        }

        protected string verificaClientesDuplicados()
        {
            string nome = "";
            string resultado = "";
            string resnome = "";
            string b = "";

            try
            {
                if (!String.IsNullOrEmpty(tbnome.Text))
                    nome = tbnome.Text.ToLower().ToString();

                int Contapesquisa = (from n in DC.aspnet_Users
                                     where n.UserName == nome.Replace(" ", "")
                                     select n).Count();

                if (Contapesquisa > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    char ch;
                    for (int i = 0; i < 5; i++)
                    {
                        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                        builder.Append(ch);
                    }
                    b = builder.ToString();
                    resnome = tbnome.Text.ToLower().Replace(" ", "").ToString();
                    resultado = resnome + b;
                }
                else
                    resultado = tbnome.Text.ToString();

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

            return resultado;
        }

        protected void carregaLojas()
        {
            try
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

                    if (regra == "Administrador" || regra == "SuperAdmin")
                    {
                        var carregaL = from op in DC.Lojas
                                       orderby op.ID ascending
                                       select op;

                        //ddlLoja.DataTextField = "NOME";
                        //ddlLoja.DataValueField = "ID";
                        //ddlLoja.DataSource = carregaL;
                        //ddlLoja.DataBind();
                        //ddlLoja.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));

                        rbLojas.DataTextField = "NOME";
                        rbLojas.DataValueField = "ID";
                        rbLojas.DataSource = carregaL;
                        rbLojas.DataBind();
                        //rbLojas.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
                    }
                    else
                    {
                        divLoja.Visible = false;
                    }
                }
                else
                    Response.Redirect("~/Default.aspx", true);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        //protected void carregaOperadoras()
        //{
        //    try
        //    {
        //        var carregaOP = from op in DC.Operadoras
        //                        orderby op.DESCRICAO ascending
        //                        select op;

        //        ddloperadora.DataTextField = "DESCRICAO";
        //        ddloperadora.DataValueField = "ID";
        //        ddloperadora.DataSource = carregaOP;
        //        ddloperadora.DataBind();
        //        ddloperadora.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}

        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }

        protected bool validaCamposEquipAvariado()
        {
            if (String.IsNullOrEmpty(tbmarca.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Marca do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(tbmodelo.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Modelo do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(tbimei.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo IMEI do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }
            Regex regex = new Regex(@"[0-9]");

            if (!String.IsNullOrEmpty(tbimei.Text) && !regex.IsMatch(tbimei.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo IMEI deve ser exclusivamente numérico!";
                return false;
            }


            if (String.IsNullOrEmpty(tbdescricaoProblemaEquipAvariado.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Descrição Detalhada do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }

            return true;
        }

        protected bool validaCamposCliente()
        {
            if (String.IsNullOrEmpty(tbnome.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Nome de Cliente é de preenchimento obrigatório!";
                tbnome.Focus();
                return false;
            }


            if (String.IsNullOrEmpty(tbcontacto.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Contacto de Cliente é de preenchimento obrigatório!";
                tbcontacto.Focus();
                return false;
            }

            if (rbTipoCliente.SelectedIndex != -1)
            { }
            else
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "Seleccione um tipo de cliente!";
                rbLojas.Focus();
                return false;

            }

            return true;
        }

        protected bool validaCamposOrdemReparacao()
        {
            if (String.IsNullOrEmpty(tbcodordemreparacao.Text))
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "O campo Código de Ordem de Reparação é de preenchimento obrigatório!";
                return false;
            }

            if (divLoja.Visible == true)
            {
                if (rbLojas.SelectedIndex != -1)
                { }
                else
                {
                    erro.Visible = errorMessage.Visible = true;
                    errorMessage.InnerHtml = "Seleccione uma loja por favor!";
                    rbLojas.Focus();
                    return false;

                }
            }

            return true;
        }

        public void EnviarEmailCliente(string email, Guid UserID, string pass)
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
                SQLLog.registaLogBD(useridP, DateTime.Now, "Inserir ordem de reparação", "Foi enviado um email para: " + email.ToString() + " para criação de nova conta de cliente.", true);
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

        private string GeraSenha()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random clsRan = new Random();
            Int32 tamanhoSenha = clsRan.Next(4, 6);

            string senha = "";
            for (Int32 i = 0; i <= tamanhoSenha; i++)
            {
                senha += guid.Substring(clsRan.Next(1, guid.Length), 1);
            }

            return senha;
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

        protected void btnGravar_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem selectedItem in listagemgeralClientes.SelectedItems)
            {
                useridPesquisaCliente = new Guid(selectedItem["UserId"].Text);
            }

            foreach (GridDataItem selectedItem in listaClienteFrequente.SelectedItems)
            {
                useridClienteFrequente = new Guid(selectedItem["UserId"].Text);
            }

            try
            {
                if (validaCamposCliente())
                {
                    Guid novouserCliente = new Guid();

                    int contactoexiste = 0;

                    var userResults = from u in DC.Parceiros
                                      where u.ID_TIPO_PARCEIRO != 3 && (u.TELEFONE == tbcontacto.Text)
                                      select u;

                    contactoexiste = Enumerable.Count(userResults);

                    if (contactoexiste == 0)
                    {
                        int mailexiste = 0;

                        var usermail = from u in DC.aspnet_Memberships
                                       where u.Email == tbemail.Text
                                       select u;

                        mailexiste = Enumerable.Count(usermail);

                        if (mailexiste == 0)
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

                            string nomeUser = verificaClientesDuplicados();

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
                            USER.UserName = nomeUser.ToString().Replace(" ", "");
                            USER.LoweredUserName = nomeUser.ToString().ToLower().Replace(" ", "");
                            USER.ApplicationId = aplid;
                            USER.UserId = IDUser;
                            USER.aspnet_UsersInRoles.Add(UserRole);
                            USER.aspnet_Membership = MEMBER;

                            LINQ_DB.Parceiro NOVOCLIENTE = new LINQ_DB.Parceiro();
                            NOVOCLIENTE.aspnet_User = USER;
                            NOVOCLIENTE.CODIGO = tbcodcliente.Text.ToString();
                            NOVOCLIENTE.ID_TIPO_PARCEIRO = Convert.ToInt32(rbTipoCliente.SelectedItem.Value);
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
                            var configura = from conf in DC.Configuracaos
                                            where conf.INICIAL == "C"
                                            select conf;

                            NOVOCODIGOCLIENTE = configura.First();
                            NOVOCODIGOCLIENTE.CODIGO = NOVOCODIGOCLIENTE.CODIGO + 1;


                            if (NOVOCLIENTE.EMAIL != "dygussat@dygus.com")
                            {
                                Thread t = new Thread(() => EnviarEmailCliente(MEMBER.Email, IDUser, password.ToString()));
                                t.Start();
                                timer3.Interval = 5;
                                timer3.Enabled = true;
                                Thread tempo = new Thread(() => timer3.Start());
                                tempo.Start();
                            }

                            if (NOVOCLIENTE.EMAIL == "dygussat@dygus.com")
                                NOVOCLIENTE.ACTIVO = true;

                            DC.SubmitChanges();

                            var procuraUserID = from n in DC.Parceiros
                                                where n.CODIGO == tbcodcliente.Text
                                                select n;

                            foreach (var item in procuraUserID)
                            {
                                novouserCliente = item.USERID.Value;
                            }

                            LINQ_DB.Equipamento_Avariado_TrabalhosAdicionai NOVOTRABALHOSADICIONAL = new LINQ_DB.Equipamento_Avariado_TrabalhosAdicionai();
                            NOVOTRABALHOSADICIONAL.RESET_SOFTWARE = rbresetsoftware.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.ACTUALIZACAO_SOFTWARE = rbactualizacaosoft.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.REPARACAO_HARDWARE = rbhardware.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.LIMPEZA_GERAL = rbLimpeza.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.BACKUP_INFORMACAO = rbbackupinfo.SelectedToggleState.Selected;
                            if (!String.IsNullOrEmpty(tbObsTrabalhosRealizar.Text))
                                NOVOTRABALHOSADICIONAL.OBSERVACOES = tbObsTrabalhosRealizar.Text;
                            else
                                NOVOTRABALHOSADICIONAL.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_TrabalhosAdicionais.InsertOnSubmit(NOVOTRABALHOSADICIONAL);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado_Acessorio NOVOACESSORIO = new LINQ_DB.Equipamento_Avariado_Acessorio();
                            NOVOACESSORIO.BATERIA = rbbateriaequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CARREGADOR = rbcarregadorequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CARTAO_MEM = rbcartaomemoriaequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.BOLSA = rbbolsaequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CARTAO_SIM = rbcartaosimequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CAIXA = rbcaixa.SelectedToggleState.Selected;
                            if (!String.IsNullOrEmpty(tboutrosequipavariado.Text))
                                NOVOACESSORIO.OUTROS = tboutrosequipavariado.Text;
                            else
                                NOVOACESSORIO.OUTROS = "N/D";
                            if (!String.IsNullOrEmpty(tbobsequipavariado.Text))
                                NOVOACESSORIO.OBSERVACOES = tbobsequipavariado.Text;
                            else
                                NOVOACESSORIO.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_Acessorios.InsertOnSubmit(NOVOACESSORIO);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado_Bloqueio NOVOESTADOBLOQUEIO = new LINQ_DB.Equipamento_Avariado_Bloqueio();

                            if (!String.IsNullOrEmpty(tboperadora.Text))
                            {
                                NOVOESTADOBLOQUEIO.BLOQUEADO = true;

                                int operadoraexiste = 0;

                                var operadoras = from m in DC.Operadoras
                                                 where m.DESCRICAO == tboperadora.Text
                                                 select m;

                                operadoraexiste = Enumerable.Count(operadoras);

                                if (operadoraexiste == 0)
                                {
                                    LINQ_DB.Operadora NOVAOPERADORA = new LINQ_DB.Operadora();

                                    NOVAOPERADORA.DESCRICAO = tbmodelo.Text;
                                    NOVAOPERADORA.ACTIVO = true;
                                    DC.Operadoras.InsertOnSubmit(NOVAOPERADORA);
                                    DC.SubmitChanges();

                                    NOVOESTADOBLOQUEIO.ID_OPERADORA = NOVAOPERADORA.ID;
                                }

                                if (operadoraexiste == 1)
                                {
                                    LINQ_DB.Operadora NOVAOPERADORA = new LINQ_DB.Operadora();

                                    var operadoraProcura = from mod in DC.Operadoras
                                                           where mod.DESCRICAO == tboperadora.Text
                                                           select mod;

                                    foreach (var item in operadoraProcura)
                                    {
                                        NOVOESTADOBLOQUEIO.ID_OPERADORA = item.ID;
                                    }
                                }

                            }
                            else
                                NOVOESTADOBLOQUEIO.BLOQUEADO = false;
                            NOVOESTADOBLOQUEIO.ID_OPERADORA = null;
                            if (!String.IsNullOrEmpty(tbobsEstadoEquipamento.Text))
                                NOVOESTADOBLOQUEIO.OBSERVACOES = tbobsEstadoEquipamento.Text;
                            else
                                NOVOESTADOBLOQUEIO.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_Bloqueios.InsertOnSubmit(NOVOESTADOBLOQUEIO);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado_GarantiaTipo NOVOEQUIPAVARIADOGARANTIA = new LINQ_DB.Equipamento_Avariado_GarantiaTipo();
                            if (ddltipogarantia.SelectedItem.Text != "Por favor seleccione...")
                                NOVOEQUIPAVARIADOGARANTIA.ID_TIPO_GARANTIA = Convert.ToInt32(ddltipogarantia.SelectedItem.Value);
                            else
                                NOVOEQUIPAVARIADOGARANTIA.ID_TIPO_GARANTIA = null;
                            if (!String.IsNullOrEmpty(tbobsGarantiaequipavariado.Text))
                                NOVOEQUIPAVARIADOGARANTIA.OBSERVACOES = tbobsGarantiaequipavariado.Text;
                            else
                                NOVOEQUIPAVARIADOGARANTIA.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_GarantiaTipos.InsertOnSubmit(NOVOEQUIPAVARIADOGARANTIA);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado NOVOEQUIPAVARIADO = new LINQ_DB.Equipamento_Avariado();

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

                                NOVOEQUIPAVARIADO.ID_MARCA = MODELO.ID_MARCA;
                                NOVOEQUIPAVARIADO.ID_MODELO = MODELO.ID;
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
                                        NOVOEQUIPAVARIADO.ID_MARCA = itemmodelo.ID;
                                    }
                                    NOVOEQUIPAVARIADO.ID_MODELO = item.ID;
                                }
                            }
                            NOVOEQUIPAVARIADO.IMEI = tbimei.Text;
                            NOVOEQUIPAVARIADO.NOVO_IMEI = "N/A";
                            if (!String.IsNullOrEmpty(tbcodseguranca.Text))
                                NOVOEQUIPAVARIADO.COD_SEGURANCA = tbcodseguranca.Text;
                            else
                                NOVOEQUIPAVARIADO.COD_SEGURANCA = "N/A";
                            if (!String.IsNullOrEmpty(tbObsNovoEquipAvariado.Text))
                                NOVOEQUIPAVARIADO.OBSERVACOES = tbObsNovoEquipAvariado.Text;
                            else
                                NOVOEQUIPAVARIADO.OBSERVACOES = "N/D";
                            NOVOEQUIPAVARIADO.DATA_REGISTO = DateTime.Now;
                            NOVOEQUIPAVARIADO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;

                            DC.Equipamento_Avariados.InsertOnSubmit(NOVOEQUIPAVARIADO);
                            DC.SubmitChanges();

                            LINQ_DB.Ordem_Reparacao NOVAORDEMREPARACAO = new LINQ_DB.Ordem_Reparacao();
                            NOVAORDEMREPARACAO.CODIGO = tbcodordemreparacao.Text;
                            NOVAORDEMREPARACAO.USERID = NOVOCLIENTE.USERID;

                            if (divLoja.Visible == true)
                                NOVAORDEMREPARACAO.ID_LOJA = Convert.ToInt32(rbLojas.SelectedItem.Value);
                            else
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

                                    if (regra == "Lojista" || regra == "Tecnico")
                                    {
                                        var procuraLoja = from l in DC.Funcionarios
                                                          where l.USERID == userid
                                                          select l;

                                        foreach (var item in procuraLoja)
                                        {
                                            NOVAORDEMREPARACAO.ID_LOJA = item.ID_LOJA;
                                        }
                                    }
                                }
                                else
                                    Response.Redirect("~/Default.aspx", true);
                            }
                            NOVAORDEMREPARACAO.ATRIBUIDA = false;
                            NOVAORDEMREPARACAO.CLIENTE_NOTIFICADO = false;
                            NOVAORDEMREPARACAO.VALOR_FINAL = 0;
                            NOVAORDEMREPARACAO.VALOR_CUSTO = 0;
                            if (tbvalorprevistorep.Value != null)
                                NOVAORDEMREPARACAO.VALOR_PREVISTO_REPARACAO = tbvalorprevistorep.Value;
                            else
                                NOVAORDEMREPARACAO.VALOR_PREVISTO_REPARACAO = 0;
                            NOVAORDEMREPARACAO.VALOR_ADICIONAL_REPARACAO = 0;

                            foreach (GridDataItem selectedItem in listagemEquipamentosSubstituicao.SelectedItems)
                            {
                                idEquipSubst = selectedItem["ID"].Text;
                            }
                            if (!String.IsNullOrEmpty(idEquipSubst))
                            {
                                NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = Convert.ToInt32(idEquipSubst);
                                LINQ_DB.Equipamentos_Substituicao NOVOEQUIPSUBST = new LINQ_DB.Equipamentos_Substituicao();
                                var verifica = from eqs in DC.Equipamentos_Substituicaos
                                               where eqs.ID == NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO
                                               select eqs;
                                NOVOEQUIPSUBST = verifica.First();
                                NOVOEQUIPSUBST.ID_DISPONIBILIDADE = 2;
                                NOVOEQUIPSUBST.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                DC.SubmitChanges();
                            }

                            else
                            {
                                NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = null;
                            }

                            NOVAORDEMREPARACAO.ID_EQUIPAMENTO_AVARIADO = NOVOEQUIPAVARIADO.ID;
                            NOVAORDEMREPARACAO.ID_ESTADO = 1;
                            NOVAORDEMREPARACAO.DESCRICAO_DETALHADA_PROBLEMA = tbdescricaoProblemaEquipAvariado.Text;
                            NOVAORDEMREPARACAO.ID_TRABALHOS_ADICIONAIS = NOVOTRABALHOSADICIONAL.ID;
                            NOVAORDEMREPARACAO.ID_ESTADO_BLOQUEIO = NOVOESTADOBLOQUEIO.ID;
                            NOVAORDEMREPARACAO.ID_ACESSORIOS = NOVOACESSORIO.ID;
                            if (ddltipoReparacao.SelectedItem.Text == "Por favor seleccione...")
                                NOVAORDEMREPARACAO.ID_TIMING_REPARACAO = 1;
                            else
                                NOVAORDEMREPARACAO.ID_TIMING_REPARACAO = Convert.ToInt32(ddltipoReparacao.SelectedItem.Value);
                            NOVAORDEMREPARACAO.ID_TIPO_GARANTIA = NOVOEQUIPAVARIADOGARANTIA.ID;
                            NOVAORDEMREPARACAO.DATA_REGISTO = DateTime.Now;
                            if (dataprevistareparacao.SelectedDate != null)
                                NOVAORDEMREPARACAO.DATA_PREVISTA_CONCLUSAO = dataprevistareparacao.SelectedDate;
                            else
                                NOVAORDEMREPARACAO.DATA_PREVISTA_CONCLUSAO = NOVAORDEMREPARACAO.DATA_REGISTO;
                            NOVAORDEMREPARACAO.DATA_CONCLUSAO = null;
                            NOVAORDEMREPARACAO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                            NOVAORDEMREPARACAO.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO = null;

                            DC.Ordem_Reparacaos.InsertOnSubmit(NOVAORDEMREPARACAO);
                            DC.SubmitChanges();
                            SQLLog.registaLogBD(useridP, DateTime.Now, "Inserir ordem de reparação", "Foi criada nova OR com o código: " + NOVAORDEMREPARACAO.CODIGO.ToString() + ".", true);
                            if (NOVOCLIENTE.EMAIL != "dygussat@dygus.com")
                            {
                                Thread t = new Thread(() => EnviarEmailClienteNovaOR(NOVOCLIENTE.EMAIL));
                                t.Start();
                                timer2.Interval = 5;
                                timer2.Enabled = true;
                                Thread tempo = new Thread(() => timer2.Start());
                                tempo.Start();
                            }

                            //recalculaCodCliente();
                            //sucesso.Visible = sucessoMessage.Visible = sucesso2.Visible = sucesso2message.Visible = true;
                            //sucessoMessage.InnerHtml = sucesso2message.InnerHtml = "Ordem de Reparação " + NOVAORDEMREPARACAO.CODIGO.ToString() + " registada com êxito!";
                            //erro.Visible = errorMessage.Visible =  false;
                            //limpaCampos();

                            Response.Redirect("ListagemGeralOrdensReparacao.aspx", false);

                        }
                        else
                        {
                            tbemail.Focus();
                            erro.Visible = errorMessage.Visible = true;
                            errorMessage.InnerHtml = "O email inserido já se encontra registado, por favor altere!";
                            return;
                        }
                    }
                    else
                    {
                        tbcontacto.Focus();
                        erro.Visible = errorMessage.Visible = true;
                        errorMessage.InnerHtml = "O nº de telefone registado já existe, por favor altere o contacto do cliente!";
                        return;
                    }

                }
                else
                {


                    if (useridPesquisaCliente.ToString() != "00000000-0000-0000-0000-000000000000")
                    {

                        var config = from conf in DC.Configuracaos
                                     where conf.ID == 1
                                     select conf;

                        foreach (var item in config)
                        {
                            tbcodordemreparacao.Text = item.INICIAL + "-" + item.CODIGO;
                        }


                        if (validaCamposOrdemReparacao() && validaCamposEquipAvariado())
                        {
                            LINQ_DB.Equipamento_Avariado_TrabalhosAdicionai NOVOTRABALHOSADICIONAL = new LINQ_DB.Equipamento_Avariado_TrabalhosAdicionai();
                            NOVOTRABALHOSADICIONAL.RESET_SOFTWARE = rbresetsoftware.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.ACTUALIZACAO_SOFTWARE = rbactualizacaosoft.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.REPARACAO_HARDWARE = rbhardware.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.LIMPEZA_GERAL = rbLimpeza.SelectedToggleState.Selected;
                            NOVOTRABALHOSADICIONAL.BACKUP_INFORMACAO = rbbackupinfo.SelectedToggleState.Selected;
                            if (!String.IsNullOrEmpty(tbObsTrabalhosRealizar.Text))
                                NOVOTRABALHOSADICIONAL.OBSERVACOES = tbObsTrabalhosRealizar.Text;
                            else
                                NOVOTRABALHOSADICIONAL.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_TrabalhosAdicionais.InsertOnSubmit(NOVOTRABALHOSADICIONAL);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado_Acessorio NOVOACESSORIO = new LINQ_DB.Equipamento_Avariado_Acessorio();
                            NOVOACESSORIO.BATERIA = rbbateriaequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CARREGADOR = rbcarregadorequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CARTAO_MEM = rbcartaomemoriaequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.BOLSA = rbbolsaequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CARTAO_SIM = rbcartaosimequipavariado.SelectedToggleState.Selected;
                            NOVOACESSORIO.CAIXA = rbcaixa.SelectedToggleState.Selected;
                            if (!String.IsNullOrEmpty(tboutrosequipavariado.Text))
                                NOVOACESSORIO.OUTROS = tboutrosequipavariado.Text;
                            else
                                NOVOACESSORIO.OUTROS = "N/D";
                            if (!String.IsNullOrEmpty(tbobsequipavariado.Text))
                                NOVOACESSORIO.OBSERVACOES = tbobsequipavariado.Text;
                            else
                                NOVOACESSORIO.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_Acessorios.InsertOnSubmit(NOVOACESSORIO);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado_Bloqueio NOVOESTADOBLOQUEIO = new LINQ_DB.Equipamento_Avariado_Bloqueio();

                            if (!String.IsNullOrEmpty(tboperadora.Text))
                            {
                                NOVOESTADOBLOQUEIO.BLOQUEADO = true;

                                int operadoraexiste = 0;

                                var operadoras = from m in DC.Operadoras
                                                 where m.DESCRICAO == tboperadora.Text
                                                 select m;

                                operadoraexiste = Enumerable.Count(operadoras);

                                if (operadoraexiste == 0)
                                {
                                    LINQ_DB.Operadora NOVAOPERADORA = new LINQ_DB.Operadora();

                                    NOVAOPERADORA.DESCRICAO = tbmodelo.Text;
                                    NOVAOPERADORA.ACTIVO = true;
                                    DC.Operadoras.InsertOnSubmit(NOVAOPERADORA);
                                    DC.SubmitChanges();

                                    NOVOESTADOBLOQUEIO.ID_OPERADORA = NOVAOPERADORA.ID;
                                }

                                if (operadoraexiste == 1)
                                {
                                    LINQ_DB.Operadora NOVAOPERADORA = new LINQ_DB.Operadora();

                                    var operadoraProcura = from mod in DC.Operadoras
                                                           where mod.DESCRICAO == tboperadora.Text
                                                           select mod;

                                    foreach (var item in operadoraProcura)
                                    {
                                        NOVOESTADOBLOQUEIO.ID_OPERADORA = item.ID;
                                    }
                                }

                            }
                            else
                                NOVOESTADOBLOQUEIO.BLOQUEADO = false;
                            NOVOESTADOBLOQUEIO.ID_OPERADORA = null;
                            if (!String.IsNullOrEmpty(tbobsEstadoEquipamento.Text))
                                NOVOESTADOBLOQUEIO.OBSERVACOES = tbobsEstadoEquipamento.Text;
                            else
                                NOVOESTADOBLOQUEIO.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_Bloqueios.InsertOnSubmit(NOVOESTADOBLOQUEIO);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado_GarantiaTipo NOVOEQUIPAVARIADOGARANTIA = new LINQ_DB.Equipamento_Avariado_GarantiaTipo();
                            if (ddltipogarantia.SelectedItem.Text != "Por favor seleccione...")
                                NOVOEQUIPAVARIADOGARANTIA.ID_TIPO_GARANTIA = Convert.ToInt32(ddltipogarantia.SelectedItem.Value);
                            else
                                NOVOEQUIPAVARIADOGARANTIA.ID_TIPO_GARANTIA = null;
                            if (!String.IsNullOrEmpty(tbobsGarantiaequipavariado.Text))
                                NOVOEQUIPAVARIADOGARANTIA.OBSERVACOES = tbobsGarantiaequipavariado.Text;
                            else
                                NOVOEQUIPAVARIADOGARANTIA.OBSERVACOES = "N/D";
                            DC.Equipamento_Avariado_GarantiaTipos.InsertOnSubmit(NOVOEQUIPAVARIADOGARANTIA);
                            DC.SubmitChanges();

                            LINQ_DB.Equipamento_Avariado NOVOEQUIPAVARIADO = new LINQ_DB.Equipamento_Avariado();

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

                                NOVOEQUIPAVARIADO.ID_MARCA = MODELO.ID_MARCA;
                                NOVOEQUIPAVARIADO.ID_MODELO = MODELO.ID;
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
                                        NOVOEQUIPAVARIADO.ID_MARCA = itemmodelo.ID;
                                    }
                                    NOVOEQUIPAVARIADO.ID_MODELO = item.ID;
                                }
                            }
                            NOVOEQUIPAVARIADO.IMEI = tbimei.Text;
                            NOVOEQUIPAVARIADO.NOVO_IMEI = "N/A";
                            if (!String.IsNullOrEmpty(tbcodseguranca.Text))
                                NOVOEQUIPAVARIADO.COD_SEGURANCA = tbcodseguranca.Text;
                            else
                                NOVOEQUIPAVARIADO.COD_SEGURANCA = "N/A";
                            if (!String.IsNullOrEmpty(tbObsNovoEquipAvariado.Text))
                                NOVOEQUIPAVARIADO.OBSERVACOES = tbObsNovoEquipAvariado.Text;
                            else
                                NOVOEQUIPAVARIADO.OBSERVACOES = "N/D";
                            NOVOEQUIPAVARIADO.DATA_REGISTO = DateTime.Now;
                            NOVOEQUIPAVARIADO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;

                            DC.Equipamento_Avariados.InsertOnSubmit(NOVOEQUIPAVARIADO);
                            DC.SubmitChanges();

                            LINQ_DB.Ordem_Reparacao NOVAORDEMREPARACAO = new LINQ_DB.Ordem_Reparacao();
                            NOVAORDEMREPARACAO.CODIGO = tbcodordemreparacao.Text;
                            NOVAORDEMREPARACAO.USERID = useridPesquisaCliente;

                            if (divLoja.Visible == true)
                                NOVAORDEMREPARACAO.ID_LOJA = Convert.ToInt32(rbLojas.SelectedItem.Value);
                            else
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

                                    if (regra == "Lojista" || regra == "Tecnico")
                                    {
                                        var procuraLoja = from l in DC.Funcionarios
                                                          where l.USERID == userid
                                                          select l;

                                        foreach (var item in procuraLoja)
                                        {
                                            NOVAORDEMREPARACAO.ID_LOJA = item.ID_LOJA;
                                        }
                                    }
                                }
                                else
                                    Response.Redirect("~/Default.aspx", true);
                            }
                            NOVAORDEMREPARACAO.ATRIBUIDA = false;
                            NOVAORDEMREPARACAO.CLIENTE_NOTIFICADO = false;
                            NOVAORDEMREPARACAO.VALOR_FINAL = 0;
                            NOVAORDEMREPARACAO.VALOR_CUSTO = 0;
                            if (tbvalorprevistorep.Value != null)
                                NOVAORDEMREPARACAO.VALOR_PREVISTO_REPARACAO = tbvalorprevistorep.Value;
                            else
                                NOVAORDEMREPARACAO.VALOR_PREVISTO_REPARACAO = 0;
                            NOVAORDEMREPARACAO.VALOR_ADICIONAL_REPARACAO = 0;

                            foreach (GridDataItem selectedItem in listagemEquipamentosSubstituicao.SelectedItems)
                            {
                                idEquipSubst = selectedItem["ID"].Text;
                            }
                            if (!String.IsNullOrEmpty(idEquipSubst))
                            {
                                NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = Convert.ToInt32(idEquipSubst);
                                LINQ_DB.Equipamentos_Substituicao NOVOEQUIPSUBST = new LINQ_DB.Equipamentos_Substituicao();
                                var verifica = from eqs in DC.Equipamentos_Substituicaos
                                               where eqs.ID == NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO
                                               select eqs;
                                NOVOEQUIPSUBST = verifica.First();
                                NOVOEQUIPSUBST.ID_DISPONIBILIDADE = 2;
                                NOVOEQUIPSUBST.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                DC.SubmitChanges();
                            }

                            else
                            {
                                NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = null;
                            }

                            NOVAORDEMREPARACAO.ID_EQUIPAMENTO_AVARIADO = NOVOEQUIPAVARIADO.ID;
                            NOVAORDEMREPARACAO.ID_ESTADO = 1;
                            NOVAORDEMREPARACAO.DESCRICAO_DETALHADA_PROBLEMA = tbdescricaoProblemaEquipAvariado.Text;
                            NOVAORDEMREPARACAO.ID_TRABALHOS_ADICIONAIS = NOVOTRABALHOSADICIONAL.ID;
                            NOVAORDEMREPARACAO.ID_ESTADO_BLOQUEIO = NOVOESTADOBLOQUEIO.ID;
                            NOVAORDEMREPARACAO.ID_ACESSORIOS = NOVOACESSORIO.ID;
                            if (ddltipoReparacao.SelectedItem.Text == "Por favor seleccione...")
                                NOVAORDEMREPARACAO.ID_TIMING_REPARACAO = 1;
                            else
                                NOVAORDEMREPARACAO.ID_TIMING_REPARACAO = Convert.ToInt32(ddltipoReparacao.SelectedItem.Value);
                            NOVAORDEMREPARACAO.ID_TIPO_GARANTIA = NOVOEQUIPAVARIADOGARANTIA.ID;
                            NOVAORDEMREPARACAO.DATA_REGISTO = DateTime.Now;
                            if (dataprevistareparacao.SelectedDate != null)
                                NOVAORDEMREPARACAO.DATA_PREVISTA_CONCLUSAO = dataprevistareparacao.SelectedDate;
                            else
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

                            Response.Redirect("ListagemGeralOrdensReparacao.aspx", false);

                            //recalculaCodCliente();
                            //sucesso.Visible = sucessoMessage.Visible = sucesso2.Visible = sucesso2message.Visible = true;
                            //sucessoMessage.InnerHtml = sucesso2message.InnerHtml = "Ordem de Reparação " + NOVAORDEMREPARACAO.CODIGO.ToString() + " registada com êxito!";
                            //erro.Visible = errorMessage.Visible =  false;
                            //limpaCampos();
                        }
                    }
                    else
                    {
                        if (useridClienteFrequente.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            var config = from conf in DC.Configuracaos
                                         where conf.ID == 1
                                         select conf;

                            foreach (var item in config)
                            {
                                tbcodordemreparacao.Text = item.INICIAL + "-" + item.CODIGO;
                            }


                            if (validaCamposOrdemReparacao() && validaCamposEquipAvariado())
                            {
                                LINQ_DB.Equipamento_Avariado_TrabalhosAdicionai NOVOTRABALHOSADICIONAL = new LINQ_DB.Equipamento_Avariado_TrabalhosAdicionai();
                                NOVOTRABALHOSADICIONAL.RESET_SOFTWARE = rbresetsoftware.SelectedToggleState.Selected;
                                NOVOTRABALHOSADICIONAL.ACTUALIZACAO_SOFTWARE = rbactualizacaosoft.SelectedToggleState.Selected;
                                NOVOTRABALHOSADICIONAL.REPARACAO_HARDWARE = rbhardware.SelectedToggleState.Selected;
                                NOVOTRABALHOSADICIONAL.LIMPEZA_GERAL = rbLimpeza.SelectedToggleState.Selected;
                                NOVOTRABALHOSADICIONAL.BACKUP_INFORMACAO = rbbackupinfo.SelectedToggleState.Selected;
                                if (!String.IsNullOrEmpty(tbObsTrabalhosRealizar.Text))
                                    NOVOTRABALHOSADICIONAL.OBSERVACOES = tbObsTrabalhosRealizar.Text;
                                else
                                    NOVOTRABALHOSADICIONAL.OBSERVACOES = "N/D";
                                DC.Equipamento_Avariado_TrabalhosAdicionais.InsertOnSubmit(NOVOTRABALHOSADICIONAL);
                                DC.SubmitChanges();

                                LINQ_DB.Equipamento_Avariado_Acessorio NOVOACESSORIO = new LINQ_DB.Equipamento_Avariado_Acessorio();
                                NOVOACESSORIO.BATERIA = rbbateriaequipavariado.SelectedToggleState.Selected;
                                NOVOACESSORIO.CARREGADOR = rbcarregadorequipavariado.SelectedToggleState.Selected;
                                NOVOACESSORIO.CARTAO_MEM = rbcartaomemoriaequipavariado.SelectedToggleState.Selected;
                                NOVOACESSORIO.BOLSA = rbbolsaequipavariado.SelectedToggleState.Selected;
                                NOVOACESSORIO.CARTAO_SIM = rbcartaosimequipavariado.SelectedToggleState.Selected;
                                NOVOACESSORIO.CAIXA = rbcaixa.SelectedToggleState.Selected;
                                if (!String.IsNullOrEmpty(tboutrosequipavariado.Text))
                                    NOVOACESSORIO.OUTROS = tboutrosequipavariado.Text;
                                else
                                    NOVOACESSORIO.OUTROS = "N/D";
                                if (!String.IsNullOrEmpty(tbobsequipavariado.Text))
                                    NOVOACESSORIO.OBSERVACOES = tbobsequipavariado.Text;
                                else
                                    NOVOACESSORIO.OBSERVACOES = "N/D";
                                DC.Equipamento_Avariado_Acessorios.InsertOnSubmit(NOVOACESSORIO);
                                DC.SubmitChanges();

                                LINQ_DB.Equipamento_Avariado_Bloqueio NOVOESTADOBLOQUEIO = new LINQ_DB.Equipamento_Avariado_Bloqueio();

                                if (!String.IsNullOrEmpty(tboperadora.Text))
                                {
                                    NOVOESTADOBLOQUEIO.BLOQUEADO = true;

                                    int operadoraexiste = 0;

                                    var operadoras = from m in DC.Operadoras
                                                     where m.DESCRICAO == tboperadora.Text
                                                     select m;

                                    operadoraexiste = Enumerable.Count(operadoras);

                                    if (operadoraexiste == 0)
                                    {
                                        LINQ_DB.Operadora NOVAOPERADORA = new LINQ_DB.Operadora();

                                        NOVAOPERADORA.DESCRICAO = tbmodelo.Text;
                                        NOVAOPERADORA.ACTIVO = true;
                                        DC.Operadoras.InsertOnSubmit(NOVAOPERADORA);
                                        DC.SubmitChanges();

                                        NOVOESTADOBLOQUEIO.ID_OPERADORA = NOVAOPERADORA.ID;
                                    }

                                    if (operadoraexiste == 1)
                                    {
                                        LINQ_DB.Operadora NOVAOPERADORA = new LINQ_DB.Operadora();

                                        var operadoraProcura = from mod in DC.Operadoras
                                                               where mod.DESCRICAO == tboperadora.Text
                                                               select mod;

                                        foreach (var item in operadoraProcura)
                                        {
                                            NOVOESTADOBLOQUEIO.ID_OPERADORA = item.ID;
                                        }
                                    }

                                }
                                else
                                    NOVOESTADOBLOQUEIO.BLOQUEADO = false;
                                NOVOESTADOBLOQUEIO.ID_OPERADORA = null;
                                if (!String.IsNullOrEmpty(tbobsEstadoEquipamento.Text))
                                    NOVOESTADOBLOQUEIO.OBSERVACOES = tbobsEstadoEquipamento.Text;
                                else
                                    NOVOESTADOBLOQUEIO.OBSERVACOES = "N/D";
                                DC.Equipamento_Avariado_Bloqueios.InsertOnSubmit(NOVOESTADOBLOQUEIO);
                                DC.SubmitChanges();

                                LINQ_DB.Equipamento_Avariado_GarantiaTipo NOVOEQUIPAVARIADOGARANTIA = new LINQ_DB.Equipamento_Avariado_GarantiaTipo();
                                if (ddltipogarantia.SelectedItem.Text != "Por favor seleccione...")
                                    NOVOEQUIPAVARIADOGARANTIA.ID_TIPO_GARANTIA = Convert.ToInt32(ddltipogarantia.SelectedItem.Value);
                                else
                                    NOVOEQUIPAVARIADOGARANTIA.ID_TIPO_GARANTIA = null;
                                if (!String.IsNullOrEmpty(tbobsGarantiaequipavariado.Text))
                                    NOVOEQUIPAVARIADOGARANTIA.OBSERVACOES = tbobsGarantiaequipavariado.Text;
                                else
                                    NOVOEQUIPAVARIADOGARANTIA.OBSERVACOES = "N/D";
                                DC.Equipamento_Avariado_GarantiaTipos.InsertOnSubmit(NOVOEQUIPAVARIADOGARANTIA);
                                DC.SubmitChanges();

                                LINQ_DB.Equipamento_Avariado NOVOEQUIPAVARIADO = new LINQ_DB.Equipamento_Avariado();

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

                                    NOVOEQUIPAVARIADO.ID_MARCA = MODELO.ID_MARCA;
                                    NOVOEQUIPAVARIADO.ID_MODELO = MODELO.ID;
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
                                            NOVOEQUIPAVARIADO.ID_MARCA = itemmodelo.ID;
                                        }
                                        NOVOEQUIPAVARIADO.ID_MODELO = item.ID;
                                    }
                                }
                                NOVOEQUIPAVARIADO.IMEI = tbimei.Text;
                                NOVOEQUIPAVARIADO.NOVO_IMEI = "N/A";
                                if (!String.IsNullOrEmpty(tbcodseguranca.Text))
                                    NOVOEQUIPAVARIADO.COD_SEGURANCA = tbcodseguranca.Text;
                                else
                                    NOVOEQUIPAVARIADO.COD_SEGURANCA = "N/A";
                                if (!String.IsNullOrEmpty(tbObsNovoEquipAvariado.Text))
                                    NOVOEQUIPAVARIADO.OBSERVACOES = tbObsNovoEquipAvariado.Text;
                                else
                                    NOVOEQUIPAVARIADO.OBSERVACOES = "N/D";
                                NOVOEQUIPAVARIADO.DATA_REGISTO = DateTime.Now;
                                NOVOEQUIPAVARIADO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;

                                DC.Equipamento_Avariados.InsertOnSubmit(NOVOEQUIPAVARIADO);
                                DC.SubmitChanges();

                                LINQ_DB.Ordem_Reparacao NOVAORDEMREPARACAO = new LINQ_DB.Ordem_Reparacao();
                                NOVAORDEMREPARACAO.CODIGO = tbcodordemreparacao.Text;
                                NOVAORDEMREPARACAO.USERID = useridClienteFrequente;

                                if (divLoja.Visible == true)
                                    NOVAORDEMREPARACAO.ID_LOJA = Convert.ToInt32(rbLojas.SelectedItem.Value);
                                else
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

                                        if (regra == "Lojista" || regra == "Tecnico")
                                        {
                                            var procuraLoja = from l in DC.Funcionarios
                                                              where l.USERID == userid
                                                              select l;

                                            foreach (var item in procuraLoja)
                                            {
                                                NOVAORDEMREPARACAO.ID_LOJA = item.ID_LOJA;
                                            }
                                        }
                                    }
                                    else
                                        Response.Redirect("~/Default.aspx", true);
                                }
                                NOVAORDEMREPARACAO.ATRIBUIDA = false;
                                NOVAORDEMREPARACAO.CLIENTE_NOTIFICADO = false;
                                NOVAORDEMREPARACAO.VALOR_FINAL = 0;
                                NOVAORDEMREPARACAO.VALOR_CUSTO = 0;
                                if (tbvalorprevistorep.Value != null)
                                    NOVAORDEMREPARACAO.VALOR_PREVISTO_REPARACAO = tbvalorprevistorep.Value;
                                else
                                    NOVAORDEMREPARACAO.VALOR_PREVISTO_REPARACAO = 0;
                                NOVAORDEMREPARACAO.VALOR_ADICIONAL_REPARACAO = 0;

                                foreach (GridDataItem selectedItem in listagemEquipamentosSubstituicao.SelectedItems)
                                {
                                    idEquipSubst = selectedItem["ID"].Text;
                                }

                                if (!String.IsNullOrEmpty(idEquipSubst))
                                {
                                    NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = Convert.ToInt32(idEquipSubst);
                                    LINQ_DB.Equipamentos_Substituicao NOVOEQUIPSUBST = new LINQ_DB.Equipamentos_Substituicao();
                                    var verifica = from eqs in DC.Equipamentos_Substituicaos
                                                   where eqs.ID == NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO
                                                   select eqs;
                                    NOVOEQUIPSUBST = verifica.First();
                                    NOVOEQUIPSUBST.ID_DISPONIBILIDADE = 2;
                                    NOVOEQUIPSUBST.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                    DC.SubmitChanges();
                                }

                                else
                                {
                                    NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = null;
                                }

                                NOVAORDEMREPARACAO.ID_EQUIPAMENTO_AVARIADO = NOVOEQUIPAVARIADO.ID;
                                NOVAORDEMREPARACAO.ID_ESTADO = 1;
                                NOVAORDEMREPARACAO.DESCRICAO_DETALHADA_PROBLEMA = tbdescricaoProblemaEquipAvariado.Text;
                                NOVAORDEMREPARACAO.ID_TRABALHOS_ADICIONAIS = NOVOTRABALHOSADICIONAL.ID;
                                NOVAORDEMREPARACAO.ID_ESTADO_BLOQUEIO = NOVOESTADOBLOQUEIO.ID;
                                NOVAORDEMREPARACAO.ID_ACESSORIOS = NOVOACESSORIO.ID;
                                if (ddltipoReparacao.SelectedItem.Text == "Por favor seleccione...")
                                    NOVAORDEMREPARACAO.ID_TIMING_REPARACAO = 1;
                                else
                                    NOVAORDEMREPARACAO.ID_TIMING_REPARACAO = Convert.ToInt32(ddltipoReparacao.SelectedItem.Value);
                                NOVAORDEMREPARACAO.ID_TIPO_GARANTIA = NOVOEQUIPAVARIADOGARANTIA.ID;
                                NOVAORDEMREPARACAO.DATA_REGISTO = DateTime.Now;
                                if (dataprevistareparacao.SelectedDate != null)
                                    NOVAORDEMREPARACAO.DATA_PREVISTA_CONCLUSAO = dataprevistareparacao.SelectedDate;
                                else
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

                                Response.Redirect("ListagemGeralOrdensReparacao.aspx", false);
                                //recalculaCodCliente();
                                //sucesso.Visible = sucessoMessage.Visible = sucesso2.Visible = sucesso2message.Visible = true;
                                //sucessoMessage.InnerHtml = sucesso2message.InnerHtml = "Ordem de Reparação " + NOVAORDEMREPARACAO.CODIGO.ToString() + " registada com êxito!";
                                //erro.Visible = errorMessage.Visible =  false;
                                //limpaCampos();
                            }
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
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        //protected void recalculaCodOR()
        //{
        //    int codNovoCliente = 0;

        //    try
        //    {
        //        var config = from conf in DC.Configuracaos
        //                     where conf.ID == "OR"
        //                     select conf;

        //        LINQ_DB.Configuracao configuracao = new LINQ_DB.Configuracao();

        //        configuracao = config.First();
        //        codNovoCliente = Convert.ToInt32(configuracao.CODIGO) + 1;

        //        string numeroCliente = "";

        //        string prefixo = configuracao.INICIAL.ToString() + "-0000";

        //        var confg = from confii in DC.Configuracaos
        //                    select confii;

        //        foreach (var item in confg)
        //        {
        //            numeroCliente = prefixo + codNovoCliente.ToString();
        //            tbcodordemreparacao.Text = numeroCliente;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}

        protected void limpaCampos()
        {
            dataregisto.SelectedDate = DateTime.Today;
            tbcodcliente.Text = tbnome.Text = tbmorada.Text = tbcodpostal.Text = tblocalidade.Text = tbcontacto.Text = tbemail.Text = tbnif.Text = tbobs.Text = "";
            tbmarca.Text = "";
            tbmodelo.Text = "";
            tbimei.Text = tbdescricaoProblemaEquipAvariado.Text = "";
            dataprevistareparacao.Clear();
            ddltipoReparacao.ClearSelection();
            tbObsNovoEquipAvariado.Text = "";
            rbresetsoftware.SelectedToggleState.Selected = false;
            rbactualizacaosoft.SelectedToggleState.Selected = false;
            rbhardware.SelectedToggleState.Selected = false;
            rbbackupinfo.SelectedToggleState.Selected = false;
            tbObsTrabalhosRealizar.Text = "";

            tboperadora.Text = "";
            tbobsEstadoEquipamento.Text = "";
            rbbateriaequipavariado.SelectedToggleState.Selected = false;
            rbcarregadorequipavariado.SelectedToggleState.Selected = false;
            rbcartaosimequipavariado.SelectedToggleState.Selected = false;
            rbbolsaequipavariado.SelectedToggleState.Selected = false;
            rbcartaomemoriaequipavariado.SelectedToggleState.Selected = false;
            rbcaixa.SelectedToggleState.Selected = false;
            tboutrosequipavariado.Text = tbobsequipavariado.Text = "";
            ddltipogarantia.ClearSelection();
            tbobsGarantiaequipavariado.Text = "";
            foreach (GridDataItem item in listagemEquipamentosSubstituicao.MasterTableView.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("ckbEquip");
                if (chk.Checked)
                {
                    chk.Checked = false;
                }
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            dataregisto.SelectedDate = DateTime.Today;
            tbcodcliente.Text = tbnome.Text = tbmorada.Text = tbcodpostal.Text = tblocalidade.Text = tbcontacto.Text = tbemail.Text = tbnif.Text = tbobs.Text = "";
            tbmarca.Text = "";
            tbmodelo.Text = "";
            tbimei.Text = tbdescricaoProblemaEquipAvariado.Text = "";
            dataprevistareparacao.Clear();
            ddltipoReparacao.ClearSelection();
            tbObsNovoEquipAvariado.Text = "";
            rbresetsoftware.SelectedToggleState.Selected = false;
            rbactualizacaosoft.SelectedToggleState.Selected = false;
            rbhardware.SelectedToggleState.Selected = false;
            rbbackupinfo.SelectedToggleState.Selected = false;
            tbObsTrabalhosRealizar.Text = "";
            tboperadora.Text = "";
            tbobsEstadoEquipamento.Text = "";
            rbbateriaequipavariado.SelectedToggleState.Selected = false;
            rbcarregadorequipavariado.SelectedToggleState.Selected = false;
            rbcartaosimequipavariado.SelectedToggleState.Selected = false;
            rbbolsaequipavariado.SelectedToggleState.Selected = false;
            rbcartaomemoriaequipavariado.SelectedToggleState.Selected = false;
            rbcaixa.SelectedToggleState.Selected = false;
            tboutrosequipavariado.Text = tbobsequipavariado.Text = "";
            ddltipogarantia.ClearSelection();
            tbobsGarantiaequipavariado.Text = "";
            foreach (GridDataItem item in listagemEquipamentosSubstituicao.MasterTableView.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("ckbEquip");
                if (chk.Checked)
                {
                    chk.Checked = false;
                }
            }
        }

        protected void carregaTiposGarantia()
        {
            try
            {
                var carregaGarantias = from g in DC.Equipamento_Avariado_Garantias
                                       orderby g.DESCRICAO ascending
                                       select g;

                ddltipogarantia.DataTextField = "DESCRICAO";
                ddltipogarantia.DataValueField = "ID";
                ddltipogarantia.DataSource = carregaGarantias;
                ddltipogarantia.DataBind();
                ddltipogarantia.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));

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
        //                       orderby marca.DESCRICAO ascending
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

        protected void listagemEquipamentosSubstituicao_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carrega = from equip in DC.Equipamentos_Substituicaos
                              where equip.ID_DISPONIBILIDADE == 1
                              orderby equip.ID ascending
                              where equip.ACTIVO == true
                              select new
                              {
                                  ID = equip.ID,
                                  CODIGO = equip.CODIGO,
                                  MARCA = equip.Marca.DESCRICAO,
                                  MODELO = equip.Modelo.DESCRICAO,
                                  IMEI = equip.IMEI,
                                  CARREGADOR = equip.CARREGADOR.Value,
                                  CARTAO_MEMORIA = equip.CARTAO_MEMORIA.Value,
                                  BATERIA = equip.BATERIA.Value,
                                  VALOR = equip.VALOR.Value
                              };

                listagemEquipamentosSubstituicao.DataSourceID = "";
                listagemEquipamentosSubstituicao.DataSource = carrega;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void carregaTiposReparacao()
        {
            try
            {
                var pesquisa = from marca in DC.Equipamento_Avariado_TimingReparacaos
                               orderby marca.DESCRICAO ascending
                               select marca;

                ddltipoReparacao.DataTextField = "DESCRICAO";
                ddltipoReparacao.DataValueField = "ID";
                ddltipoReparacao.DataSource = pesquisa;
                ddltipoReparacao.DataBind();
                ddltipoReparacao.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }

        //protected void ddlLoja_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    if (ddlLoja.SelectedItem.Text != "Por favor seleccione...")
        //        ddlLoja.Enabled = false;
        //}


        #region COMMENTED CODE
        //protected void tbcodcliente_TextChanged(object sender, EventArgs e)
        //{
        //    int contador = 0;

        //    try
        //    {
        //        var contaClientes = (from cliente in DC.Parceiros
        //                             where tbcodcliente.Text == cliente.CODIGO
        //                             select cliente).Count();

        //        contador = contaClientes;

        //        if (contador > 0)
        //        {
        //            LINQ_DB.Parceiro DADOSCLIENTE = new LINQ_DB.Parceiro();
        //            var carregaDadosClientes = from cliente in DC.Parceiros
        //                                       where tbcodcliente.Text == cliente.CODIGO
        //                                       select cliente;

        //            DADOSCLIENTE = carregaDadosClientes.First();
        //            tbnome.Text = DADOSCLIENTE.NOME.ToString();
        //            tbmorada.Text = DADOSCLIENTE.MORADA.ToString();
        //            tbcodpostal.Text = DADOSCLIENTE.CODPOSTAL.ToString();
        //            tblocalidade.Text = DADOSCLIENTE.LOCALIDADE.ToString();
        //            tbcontacto.Text = DADOSCLIENTE.TELEFONE.ToString();
        //            tbemail.Text = DADOSCLIENTE.EMAIL.ToString();
        //            tbnif.Text = DADOSCLIENTE.NIF.ToString();
        //            tbobs.Text = DADOSCLIENTE.OBSERVACOES.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}

        //protected void tbcontacto_TextChanged(object sender, EventArgs e)
        //{
        //    int contador = 0;

        //    try
        //    {
        //        var contaClientes = (from cliente in DC.Parceiros
        //                             where tbcontacto.Text == cliente.TELEFONE
        //                             select cliente).Count();

        //        contador = contaClientes;

        //        if (contador > 0)
        //        {
        //            LINQ_DB.Parceiro DADOSCLIENTE = new LINQ_DB.Parceiro();
        //            var carregaDadosClientes = from cliente in DC.Parceiros
        //                                       where tbcontacto.Text == cliente.TELEFONE
        //                                       select cliente;

        //            DADOSCLIENTE = carregaDadosClientes.First();
        //            tbcodcliente.Text = DADOSCLIENTE.CODIGO.ToString();
        //            tbnome.Text = DADOSCLIENTE.NOME.ToString();
        //            tbmorada.Text = DADOSCLIENTE.MORADA.ToString();
        //            tbcodpostal.Text = DADOSCLIENTE.CODPOSTAL.ToString();
        //            tblocalidade.Text = DADOSCLIENTE.LOCALIDADE.ToString();
        //            tbemail.Text = DADOSCLIENTE.EMAIL.ToString();
        //            tbnif.Text = DADOSCLIENTE.NIF.ToString();
        //            tbobs.Text = DADOSCLIENTE.OBSERVACOES.ToString();
        //        }
        //        else
        //        {
        //            ShowAlertMessage("Telefone inexistente! Deve registar um cliente novo!");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}

        //protected void tbemail_TextChanged(object sender, EventArgs e)
        //{
        //    int contador = 0;

        //    try
        //    {
        //        var contaClientes = (from cliente in DC.Parceiros
        //                             where tbemail.Text == cliente.EMAIL
        //                             select cliente).Count();

        //        contador = contaClientes;

        //        if (contador > 0)
        //        {
        //            LINQ_DB.Parceiro DADOSCLIENTE = new LINQ_DB.Parceiro();
        //            var carregaDadosClientes = from cliente in DC.Parceiros
        //                                       where tbemail.Text == cliente.EMAIL
        //                                       select cliente;

        //            DADOSCLIENTE = carregaDadosClientes.First();
        //            tbcodcliente.Text = DADOSCLIENTE.CODIGO.ToString();
        //            tbnome.Text = DADOSCLIENTE.NOME.ToString();
        //            tbmorada.Text = DADOSCLIENTE.MORADA.ToString();
        //            tbcodpostal.Text = DADOSCLIENTE.CODPOSTAL.ToString();
        //            tblocalidade.Text = DADOSCLIENTE.LOCALIDADE.ToString();
        //            tbcontacto.Text = DADOSCLIENTE.TELEFONE.ToString();
        //            tbnif.Text = DADOSCLIENTE.NIF.ToString();
        //            tbobs.Text = DADOSCLIENTE.OBSERVACOES.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}

        //protected void tbnif_TextChanged(object sender, EventArgs e)
        //{
        //    int contador = 0;

        //    try
        //    {
        //        var contaClientes = (from cliente in DC.Parceiros
        //                             where tbnif.Text == cliente.NIF.ToString()
        //                             select cliente).Count();

        //        contador = contaClientes;

        //        if (contador > 0)
        //        {
        //            LINQ_DB.Parceiro DADOSCLIENTE = new LINQ_DB.Parceiro();
        //            var carregaDadosClientes = from cliente in DC.Parceiros
        //                                       where tbnif.Text == cliente.NIF.ToString()
        //                                       select cliente;

        //            DADOSCLIENTE = carregaDadosClientes.First();
        //            tbcodcliente.Text = DADOSCLIENTE.CODIGO.ToString();
        //            tbnome.Text = DADOSCLIENTE.NOME.ToString();
        //            tbmorada.Text = DADOSCLIENTE.MORADA.ToString();
        //            tbcodpostal.Text = DADOSCLIENTE.CODPOSTAL.ToString();
        //            tblocalidade.Text = DADOSCLIENTE.LOCALIDADE.ToString();
        //            tbcontacto.Text = DADOSCLIENTE.TELEFONE.ToString();
        //            tbemail.Text = DADOSCLIENTE.EMAIL.ToString();
        //            tbobs.Text = DADOSCLIENTE.OBSERVACOES.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}

        //protected void tbnome_TextChanged(object sender, EventArgs e)
        //{
        //    int contador = 0;

        //    try
        //    {
        //        var contaClientes = (from cliente in DC.Parceiros
        //                             where tbnome.Text == cliente.NOME
        //                             select cliente).Count();

        //        contador = contaClientes;

        //        if (contador > 0)
        //        {
        //            LINQ_DB.Parceiro DADOSCLIENTE = new LINQ_DB.Parceiro();
        //            var carregaDadosClientes = from cliente in DC.Parceiros
        //                                       where tbnome.Text == cliente.NOME
        //                                       select cliente;

        //            DADOSCLIENTE = carregaDadosClientes.First();
        //            tbcodcliente.Text = DADOSCLIENTE.CODIGO.ToString();
        //            tbnif.Text = DADOSCLIENTE.NIF.ToString();
        //            tbmorada.Text = DADOSCLIENTE.MORADA.ToString();
        //            tbcodpostal.Text = DADOSCLIENTE.CODPOSTAL.ToString();
        //            tblocalidade.Text = DADOSCLIENTE.LOCALIDADE.ToString();
        //            tbcontacto.Text = DADOSCLIENTE.TELEFONE.ToString();
        //            tbemail.Text = DADOSCLIENTE.EMAIL.ToString();
        //            tbobs.Text = DADOSCLIENTE.OBSERVACOES.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }
        //}

        //protected void ddlLoja_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        //{
        //    try
        //    {
        //        Guid userid = new Guid();
        //        Guid Role = new Guid();
        //        string UserName = "";

        //        if (User.Identity.IsAuthenticated == true)
        //        {
        //            var us = from users in DC.aspnet_Memberships
        //                     where users.LoweredEmail == User.Identity.Name
        //                     select users;

        //            foreach (var item in us)
        //            {
        //                userid = item.UserId;
        //            }

        //            var utilizador = from util in DC.aspnet_Users
        //                             where util.UserId == userid
        //                             select util;

        //            foreach (var item in utilizador)
        //            {
        //                UserName = item.UserName;
        //            }

        //            var p = from d in DC.aspnet_UsersInRoles
        //                    where d.UserId == userid
        //                    select d;

        //            foreach (var item in p)
        //            {
        //                Role = item.RoleId;
        //            }

        //            var tipoutilizador = from d in DC.aspnet_Roles
        //                                 where d.RoleId == Role
        //                                 select d;

        //            string regra = "";

        //            foreach (var item in tipoutilizador)
        //            {
        //                regra = item.RoleName;
        //            }

        //            if (regra == "Administrador")
        //            {
        //                var carregaL = from op in DC.Lojas
        //                               orderby op.ID ascending
        //                               select op;

        //                ddlLoja.DataTextField = "NOME";
        //                ddlLoja.DataValueField = "ID";
        //                ddlLoja.DataSource = carregaL;
        //                ddlLoja.DataBind();
        //                ddlLoja.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));
        //            }
        //            else
        //            {
        //                divLoja.Visible = false;
        //            }
        //        }
        //        else
        //            Response.Redirect("~/Default.aspx", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog.WriteError(ex.Message);
        //        Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
        //    }

        //}
        #endregion

        protected void carregaTiposCliente()
        {
            try
            {
                var carregaT = from op in DC.Parceiro_Tipos
                               where op.ID != 3
                               orderby op.ID ascending
                               select op;

                //ddltipoCliente.DataTextField = "DESCRICAO";
                //ddltipoCliente.DataValueField = "ID";
                //ddltipoCliente.DataSource = carregaT;
                //ddltipoCliente.DataBind();
                //ddltipoCliente.Items.Insert(0, new RadComboBoxItem("Por favor seleccione..."));

                rbTipoCliente.DataTextField = "DESCRICAO";
                rbTipoCliente.DataValueField = "ID";
                rbTipoCliente.DataSource = carregaT;
                rbTipoCliente.DataBind();



            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        //protected void ddlLoja_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        //{
        //    int idLoja = 0;

        //    var loj = from loja in DC.Lojas
        //              where loja.ID == Convert.ToInt32(ddlLoja.SelectedItem.Value)
        //              select loja;

        //    foreach (var item in loj)
        //    {
        //        idLoja = item.ID;
        //        idMinhaLoja.Value = idLoja.ToString();
        //    }
        //    ddlLoja.Enabled = false;
        //}

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

        protected void tbPesquisaContactoCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var carrega = from cliente in DC.Parceiros
                              where (cliente.ID_TIPO_PARCEIRO == 1 || cliente.ID_TIPO_PARCEIRO == 2 || cliente.ID_TIPO_PARCEIRO == 4) && cliente.TELEFONE == tbPesquisaContactoCliente.Text
                              orderby cliente.CODIGO descending
                              select new
                              {
                                  UserId = cliente.USERID,
                                  CODIGO = cliente.CODIGO,
                                  NOME = cliente.NOME,
                                  EMAIL = cliente.EMAIL,
                                  CONTACTO = cliente.TELEFONE,
                                  NIF = cliente.NIF,
                                  TIPO_CLIENTE = cliente.Parceiro_Tipo.DESCRICAO,
                                  ESTADO = cliente.ACTIVO.Value,
                                  ID = cliente.ID
                              };

                listagemgeralClientes.DataSourceID = "";
                listagemgeralClientes.DataSource = carrega;
                listagemgeralClientes.DataBind();

                var pesquisaOrs = from ordem in DC.Ordem_Reparacaos
                                  join parceiros in DC.Parceiros on ordem.USERID equals parceiros.USERID
                                  where (parceiros.ID_TIPO_PARCEIRO == 1 || parceiros.ID_TIPO_PARCEIRO == 2 || parceiros.ID_TIPO_PARCEIRO == 4) && parceiros.TELEFONE == tbPesquisaContactoCliente.Text
                                  orderby ordem.ID descending
                                  select new
                                  {
                                      ID = ordem.ID,
                                      CODIGO = ordem.CODIGO,
                                      DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString().ToString() : null,
                                      CODIGOCLIENTE = parceiros.CODIGO,
                                      MARCA_EQUIP_AVARIADO = ordem.Equipamento_Avariado.Marca.DESCRICAO,
                                      MODELO_EQUIP_AVARIADO = ordem.Equipamento_Avariado.Modelo.DESCRICAO,
                                      DATA_ULTIMA_MODIFICACAO = ordem.DATA_ULTIMA_MODIFICACAO.HasValue ? ordem.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString() : null,
                                      ESTADO_OR = ordem.Ordem_Reparacao_Estado.DESCRICAO,
                                      VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                      DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null
                                  };

                listagemORsLoja.DataSourceID = "";
                listagemORsLoja.DataSource = pesquisaOrs;
                listagemORsLoja.DataBind();

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemORsLoja_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.Target = "_blank";
                hLink.NavigateUrl = "DetalheOR.aspx?ID=" + val1;
            }
        }

        protected void tbPesquisaNomeCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var carrega = from cliente in DC.Parceiros
                              where (cliente.ID_TIPO_PARCEIRO == 1 || cliente.ID_TIPO_PARCEIRO == 2 || cliente.ID_TIPO_PARCEIRO == 4) && cliente.NOME.Contains(tbPesquisaNomeCliente.Text)
                              orderby cliente.CODIGO descending
                              select new
                              {
                                  UserId = cliente.USERID,
                                  CODIGO = cliente.CODIGO,
                                  NOME = cliente.NOME,
                                  EMAIL = cliente.EMAIL,
                                  CONTACTO = cliente.TELEFONE,
                                  NIF = cliente.NIF,
                                  TIPO_CLIENTE = cliente.Parceiro_Tipo.DESCRICAO,
                                  ESTADO = cliente.ACTIVO.Value,
                                  ID = cliente.ID
                              };

                listagemgeralClientes.DataSourceID = "";
                listagemgeralClientes.DataSource = carrega;
                listagemgeralClientes.DataBind();

                var pesquisaOrs = from ordem in DC.Ordem_Reparacaos
                                  join parceiros in DC.Parceiros on ordem.USERID equals parceiros.USERID
                                  where (parceiros.ID_TIPO_PARCEIRO == 1 || parceiros.ID_TIPO_PARCEIRO == 2 || parceiros.ID_TIPO_PARCEIRO == 4) && parceiros.NOME.Contains(tbPesquisaNomeCliente.Text)
                                  orderby ordem.ID descending
                                  select new
                                  {
                                      ID = ordem.ID,
                                      CODIGO = ordem.CODIGO,
                                      DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString().ToString() : null,
                                      CODIGOCLIENTE = parceiros.CODIGO,
                                      MARCA_EQUIP_AVARIADO = ordem.Equipamento_Avariado.Marca.DESCRICAO,
                                      MODELO_EQUIP_AVARIADO = ordem.Equipamento_Avariado.Modelo.DESCRICAO,
                                      DATA_ULTIMA_MODIFICACAO = ordem.DATA_ULTIMA_MODIFICACAO.HasValue ? ordem.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString() : null,
                                      ESTADO_OR = ordem.Ordem_Reparacao_Estado.DESCRICAO,
                                      VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                      DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null
                                  };

                listagemORsLoja.DataSourceID = "";
                listagemORsLoja.DataSource = pesquisaOrs;
                listagemORsLoja.DataBind();

                //historicoCliente.Style.Add("display", "none");


            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaClienteFrequente_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carrega = (from cliente in DC.Parceiros
                               where (cliente.ID_TIPO_PARCEIRO == 1 || cliente.ID_TIPO_PARCEIRO == 2 || cliente.ID_TIPO_PARCEIRO == 4)
                               orderby cliente.DATA_ULTIMA_MODIFICACAO descending
                               select new
                               {
                                   UserId = cliente.USERID,
                                   CODIGO = cliente.CODIGO,
                                   NOME = cliente.NOME,
                                   EMAIL = cliente.EMAIL,
                                   CONTACTO = cliente.TELEFONE,
                                   NIF = cliente.NIF,
                                   TIPO_CLIENTE = cliente.Parceiro_Tipo.DESCRICAO,
                                   ESTADO = cliente.ACTIVO.Value,
                                   ID = cliente.ID,

                               }).Take(10);

                listaClienteFrequente.DataSourceID = "";
                listaClienteFrequente.DataSource = carrega;


            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }


        }

        protected void listagemEquipamentosSubstituicao_SelectedIndexChanged(object sender, EventArgs e)
        {
            //equipSubst.Style.Add("display", "none");
        }

        protected void listagemEquipamentosSubstituicao_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if (item["BATERIA"].Text == "True")
                    item["BATERIA"].Text = "Sim";
                if (item["BATERIA"].Text == "False")
                    item["BATERIA"].Text = "Não";

                if (item["CARTAO_MEMORIA"].Text == "True")
                    item["CARTAO_MEMORIA"].Text = "Sim";
                if (item["CARTAO_MEMORIA"].Text == "False")
                    item["CARTAO_MEMORIA"].Text = "Não";

                if (item["CARREGADOR"].Text == "True")
                    item["CARREGADOR"].Text = "Sim";
                if (item["CARREGADOR"].Text == "False")
                    item["CARREGADOR"].Text = "Não";
            }
        }

        protected void listaClienteFrequente_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem selectedItem in listaClienteFrequente.SelectedItems)
            {
                useridClienteFrequente = new Guid(selectedItem["UserId"].Text);
                historicoCliente.Style.Add("display", "none");
                clienteFrequente.Style.Add("display", "none");
                dadosNovoCliente.Style.Add("display", "none");
            }
        }

        protected void listagemgeralClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem selectedItem in listagemgeralClientes.SelectedItems)
            {
                useridPesquisaCliente = new Guid(selectedItem["UserId"].Text);
                historicoCliente.Style.Add("display", "none");
                clienteFrequente.Style.Add("display", "none");
                dadosNovoCliente.Style.Add("display", "none");
            }
        }

        protected void tbcontacto_TextChanged(object sender, EventArgs e)
        {
            string contacto = tbcontacto.Text;

            try
            {
                int pesquisaContactoCliente = (from p in DC.Parceiros
                                               where p.TELEFONE == contacto
                                               select p).Count();

                if (pesquisaContactoCliente > 1)
                    ShowAlertMessage("Existe mais do que um cliente com o mesmo número de telefone pesquisado!");
                else
                {
                    if (pesquisaContactoCliente == 1)
                    {
                        var pesquisa = from p in DC.Parceiros
                                       where p.TELEFONE == contacto
                                       select p;

                        foreach (var item in pesquisa)
                        {
                            tbcontacto.Text = item.CODIGO.ToString();
                            rbTipoCliente.SelectedValue = item.ID_TIPO_PARCEIRO.Value.ToString();
                            tbnome.Text = item.NOME.ToString();
                            tbcontacto.Text = item.TELEFONE.ToString();
                            tbemail.Text = item.EMAIL.ToString();
                            tbnif.Text = item.NIF.ToString();
                            tbobs.Text = item.OBSERVACOES.ToString();
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

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Home/InserirOrdemReparacao.aspx", false);
        }



    }
}
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
    public partial class InserirOrdemReparacaoORCCliente : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();
        protected System.Timers.Timer timer2 = new System.Timers.Timer();
        protected System.Timers.Timer timer3 = new System.Timers.Timer();
        string letras = "";
        int numeros = 0;
        string nOr = "";
        string idOrC = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.listagemEquipamentosSubstituicao.AllowMultiRowSelection = false;
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

                if (regra == "Reparador" || regra == "Cliente")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);



            if (Request.QueryString["id"] != null)
                idOrC = Request.QueryString["id"];
            else
                Response.Redirect("Default.aspx", false);




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
                int conta = (from o in DC.Orcamentos where o.ID == Convert.ToInt32(idOrC) && o.CONVERTIDO_OR == false select o).Count();
                if (conta > 0)
                    carregaORC();
                else
                    Response.Redirect("Default.aspx", false);
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
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo Marca do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(tbmodelo.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo Modelo do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(tbimei.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo IMEI do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }
            Regex regex = new Regex(@"[0-9]");

            if (!String.IsNullOrEmpty(tbimei.Text) && !regex.IsMatch(tbimei.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo IMEI deve ser exclusivamente numérico!";
                return false;
            }

            if (String.IsNullOrEmpty(tbdescricaoProblemaEquipAvariado.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo Descrição Detalhada do Equipamento Avariado é de preenchimento obrigatório!";
                return false;
            }

            return true;
        }

        protected bool validaCamposCliente()
        {
            if (String.IsNullOrEmpty(tbnome.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo Nome de Cliente é de preenchimento obrigatório!";
                tbnome.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(tbcontacto.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo Contacto de Cliente é de preenchimento obrigatório!";
                tbcontacto.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(tbcodcliente.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo Código de Cliente é de preenchimento obrigatório!";
                tbcodcliente.Focus();
                return false;
            }


            return true;
        }

        protected bool validaCamposOrdemReparacao()
        {
            if (String.IsNullOrEmpty(tbcodordemreparacao.Text))
            {
                erro.Visible = errorMessage.Visible = erro2.Visible = erro2message.Visible = true;
                errorMessage.InnerHtml = erro2message.InnerHtml = "O campo Código de Ordem de Reparação é de preenchimento obrigatório!";
                return false;
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
            try
            {
                var config = from conf in DC.Configuracaos
                             where conf.ID == 1
                             select conf;

                foreach (var item in config)
                {
                    tbcodordemreparacao.Text = item.INICIAL + "-" + item.CODIGO;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
            }

            Guid userCliente = new Guid();

            if (validaCamposOrdemReparacao() && validaCamposCliente() && validaCamposEquipAvariado())
            {
                try
                {
                    var procuraUserID = from n in DC.Parceiros
                                        where n.CODIGO == tbcodcliente.Text
                                        select n;

                    foreach (var item in procuraUserID)
                    {
                        userCliente = item.USERID.Value;
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
                    NOVAORDEMREPARACAO.USERID = userCliente;
                    NOVAORDEMREPARACAO.ID_LOJA = Convert.ToInt32(idMinhaLoja.Value);

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
                        idEquipSubst.Value = selectedItem["ID"].Text;
                        NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = Convert.ToInt32(idEquipSubst.Value);

                        LINQ_DB.Equipamentos_Substituicao NOVOEQUIPSUBST = new LINQ_DB.Equipamentos_Substituicao();
                        var verifica = from eqs in DC.Equipamentos_Substituicaos
                                       where eqs.ID == NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO
                                       select eqs;
                        NOVOEQUIPSUBST = verifica.First();
                        NOVOEQUIPSUBST.ID_DISPONIBILIDADE = 2;
                        NOVOEQUIPSUBST.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                        DC.SubmitChanges();
                    }
                    if (String.IsNullOrEmpty(idEquipSubst.Value))
                        NOVAORDEMREPARACAO.ID_EQUIPAMENTO_SUBSTITUICAO = null;

                    NOVAORDEMREPARACAO.ID_EQUIPAMENTO_AVARIADO = NOVOEQUIPAVARIADO.ID;
                    NOVAORDEMREPARACAO.ID_ESTADO = 1;
                    NOVAORDEMREPARACAO.DESCRICAO_DETALHADA_PROBLEMA = tbdescricaoProblemaEquipAvariado.Text;
                    NOVAORDEMREPARACAO.ID_TRABALHOS_ADICIONAIS = NOVOTRABALHOSADICIONAL.ID;
                    NOVAORDEMREPARACAO.ID_ESTADO_BLOQUEIO = NOVOESTADOBLOQUEIO.ID;
                    NOVAORDEMREPARACAO.ID_ACESSORIOS = NOVOACESSORIO.ID;
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

                    LINQ_DB.Orcamento UPDATE = new LINQ_DB.Orcamento();

                    var pesquisa = from o in DC.Orcamentos
                                   where o.ID == Convert.ToInt32(idOrC)
                                   select o;

                    UPDATE = pesquisa.First();
                    UPDATE.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    UPDATE.CONVERTIDO_OR = true;
                    DC.SubmitChanges();


                    Response.Redirect("ListagemGeralOrdensReparacao.aspx", false);

                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
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

        protected void listagemORsLoja_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["PRINT"].Controls[0];
                hLink.NavigateUrl = "DetalheOR.aspx?ID=" + val1;
            }
        }

        protected void carregaORC()
        {
            var pesquisaORC = from o in DC.Orcamentos
                              join p in DC.Parceiros on o.USERID.Value equals p.USERID.Value
                              join e in DC.Equipamento_Avariados on o.ID_EQUIPAMENTO_AVARIADO.Value equals e.ID
                              join t in DC.Equipamento_Avariado_TrabalhosAdicionais on o.ID_TRABALHOS_ADICIONAIS.Value equals t.ID
                              where o.ID == Convert.ToInt32(idOrC) && o.CONVERTIDO_OR == false
                              select new
                              {
                                  IDLOJA = o.ID_LOJA.Value,
                                  NOMELOJA = o.Loja.NOME,
                                  CODIGOCLIENTE = p.CODIGO,
                                  NOMECLIENTE = p.NOME,
                                  TELEFONECLIENTE = p.TELEFONE,
                                  EMAILCLIENTE = p.EMAIL,
                                  NIFCLIENTE = p.NIF,
                                  OBSCLIENTE = p.OBSERVACOES,
                                  TIPOCLIENTE = p.Parceiro_Tipo.DESCRICAO,
                                  IDTIPOCLIENTE = p.ID_TIPO_PARCEIRO.Value,
                                  MARCAEQUIP = e.Marca.DESCRICAO,
                                  MODELOEQUIP = e.Modelo.DESCRICAO,
                                  IMEI = e.IMEI,
                                  CODSEG = e.COD_SEGURANCA,
                                  AVARIA = o.DESCRICAO_DETALHADA_AVARIA,
                                  DATAPREVISTA = DateTime.Today,
                                  VALORPREV = o.VALOR_ORCAMENTO.Value,
                                  OBSEQUIP = e.OBSERVACOES,
                                  RESET = t.RESET_SOFTWARE.Value,
                                  ACTUALIZA = t.ACTUALIZACAO_SOFTWARE.Value,
                                  LIMPEZA = t.LIMPEZA_GERAL.Value,
                                  REPARA = t.REPARACAO_HARDWARE.Value,
                                  BACKUP = t.BACKUP_INFORMACAO.Value,
                                  OBSEAV = t.OBSERVACOES


                              };

            foreach (var item in pesquisaORC)
            {
                idMinhaLoja.Value = item.IDLOJA.ToString();
                nomeLoja.Text = item.NOMELOJA;
                tbcodcliente.Text = item.CODIGOCLIENTE;
                tbnome.Text = item.NOMECLIENTE;
                tbcontacto.Text = item.TELEFONECLIENTE;
                tbemail.Text = item.EMAILCLIENTE;
                tbnif.Text = item.NIFCLIENTE.Value.ToString();
                tbobs.Text = item.OBSCLIENTE;
                lbTipoCliente.Text = item.TIPOCLIENTE;
                idTipoMeuCliente.Value = item.IDTIPOCLIENTE.ToString();
                tbmarca.Text = item.MARCAEQUIP;
                tbmodelo.Text = item.MODELOEQUIP;
                tbimei.Text = item.IMEI;
                tbcodseguranca.Text = item.CODSEG;
                tbdescricaoProblemaEquipAvariado.Text = item.AVARIA;
                dataprevistareparacao.SelectedDate = item.DATAPREVISTA;
                tbvalorprevistorep.Value = item.VALORPREV;
                tbobsequipavariado.Text = item.OBSEQUIP;
                ddltipoReparacao.SelectedValue = "1";
                rbresetsoftware.SelectedToggleState.Selected = item.RESET;
                rbactualizacaosoft.SelectedToggleState.Selected = item.ACTUALIZA;
                rbLimpeza.SelectedToggleState.Selected = item.LIMPEZA;
                rbbackupinfo.SelectedToggleState.Selected = item.BACKUP;
                rbhardware.SelectedToggleState.Selected = item.REPARA;
                tbobsequipavariado.Text = item.OBSEAV;
            }

            setModoReadCliente();
            setModoReadEquipAvariado();
        }

        protected void setModoReadCliente()
        {
            tbnome.ReadOnly = true;
            tbcontacto.ReadOnly = true;
            tbemail.ReadOnly = true;
            tbnif.ReadOnly = true;
            tbobs.ReadOnly = true;
        }

        protected void setModoReadEquipAvariado()
        {
            tbmarca.ReadOnly = true;
            tbmodelo.ReadOnly = true;
            tbimei.ReadOnly = true;
            tbcodseguranca.ReadOnly = true;
            tbdescricaoProblemaEquipAvariado.ReadOnly = true;
            ddltipoReparacao.Enabled = false;
            tbObsNovoEquipAvariado.ReadOnly = true;


        }

        protected void listagemEquipamentosSubstituicao_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridDataItem selectedItem in listagemEquipamentosSubstituicao.SelectedItems)
            {
                idEquipSubst.Value = selectedItem["ID"].Text;
            }
        }

    }
}
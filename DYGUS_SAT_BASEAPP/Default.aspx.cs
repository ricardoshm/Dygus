using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP
{
    public partial class Default : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            tbusername.Focus();
            carregaVersao();
        }

       

        protected void carregaVersao()
        {
            try
            {
                var v = (from vv in DC.Versaos orderby vv.DATA descending select vv).Take(1);

                foreach (var item in v)
                {
                    versao.InnerHtml = "v." + item.VERSAO1.ToString();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
            }
        }

        private bool authenticated(string username, string password)
        {
            bool resultado_password = false;
            bool resultado_aprovacao = false;
            bool resultado_final = false;

            try
            {
                int emailexiste = 0;

                var userResults = from u in DC.aspnet_Memberships
                                  where u.Email == username
                                  select u;

                emailexiste = Enumerable.Count(userResults);

                if (emailexiste == 1)
                {
                    string pass = "";
                    Boolean aprovado = false;

                    var pas = from users in DC.aspnet_Memberships
                              where users.Email == username.ToString()
                              select users;

                    foreach (var item in pas)
                    {
                        pass = item.Password;
                        aprovado = item.IsApproved;
                    }
                    string hashedPass = pass;

                    string strUserInputtedHashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
                    if (strUserInputtedHashedPassword == hashedPass)
                        resultado_password = true;
                    else
                        resultado_password = false;
                    if (aprovado == true)
                        resultado_aprovacao = true;
                    else
                        resultado_aprovacao = false;
                }
                else
                {
                    int usernameExiste = 0;

                    var userNameResults = from u in DC.aspnet_Users
                                          where u.UserName == username
                                          select u;

                    usernameExiste = Enumerable.Count(userNameResults);

                    if (usernameExiste == 1)
                    {
                        string passN = "";
                        Boolean aprovadoN = false;

                        var pasN = from usersN in DC.aspnet_Users
                                   join m in DC.aspnet_Memberships on usersN.UserId equals m.UserId
                                   where usersN.UserName == username.ToString()
                                   select usersN;

                        foreach (var item in pasN)
                        {
                            passN = item.aspnet_Membership.Password;
                            aprovadoN = item.aspnet_Membership.IsApproved;
                        }
                        string hashedPass = passN;

                        string strUserInputtedHashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");
                        if (strUserInputtedHashedPassword == hashedPass)
                            resultado_password = true;
                        else
                            resultado_password = false;
                        if (aprovadoN == true)
                            resultado_aprovacao = true;
                        else
                            resultado_aprovacao = false;
                    }

                }


                if (resultado_password == true && resultado_aprovacao == true)
                    resultado_final = true;
                else
                    resultado_final = false;

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);

            }

            return resultado_final;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = tbusername.Text;
                string password = tbpassword.Text;
                erro.InnerHtml = "";

                List<string> lixo = new List<string>();

                lixo.Add("select");
                lixo.Add("drop");
                lixo.Add(";");
                lixo.Add("--");
                lixo.Add("insert");
                lixo.Add("delete");
                lixo.Add("_xp");

                foreach (var item in lixo)
                {
                    if (username.Contains(item))
                    {
                        username = username.Replace(item, "");
                    }
                }

                foreach (var item in lixo)
                {
                    if (password.Contains(item))
                    {
                        password = password.Replace(item, "");
                    }
                }


                if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password))
                {
                    erro.Visible = true;
                    erro.InnerHtml = "Todos os campos são obrigatórios!";
                }
                else
                {
                    if (String.IsNullOrEmpty(password))
                    {
                        erro.Visible = true;
                        erro.InnerHtml = "O campo Palavra-Passe é obrigatório!";
                        tbpassword.Focus();
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(username))
                        {

                            erro.Visible = true;
                            erro.InnerHtml = "O campo Nome de Utilizador é obrigatório!";
                            tbusername.Focus();
                        }
                    }
                }

                if (authenticated(username, password) == true)
                {
                    string nome = username;
                    string userData = "DYGUS_SAT";

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, nome, DateTime.Now, DateTime.Now.AddMinutes(30), true, userData, FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    Session.Add("Utilizador", nome);

                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    LINQ_DB.aspnet_Membership MEMBER = new LINQ_DB.aspnet_Membership();

                    Guid Userid = new Guid();
                    string emailLogin = "";

                    var m = from d in DC.aspnet_Memberships
                            where d.LoweredEmail == username
                            select d;

                    foreach (var item in m)
                    {
                        Userid = item.UserId;
                        emailLogin = item.LoweredEmail;
                    }

                    if (m.Count() > 0)
                    {
                        MEMBER = m.First();

                        MEMBER.LastLoginDate = DateTime.Now;
                        DC.SubmitChanges();
                    }
                    var p = from d in DC.aspnet_UsersInRoles
                            where d.UserId == Userid
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

                    if (regra == "SuperAdmin" || regra == "Administrador" || regra == "Cliente" || regra == "Reparador" || regra == "Tecnico" || regra == "Lojista")
                    {
                        if (validaRequisitosNovaInstalacao(emailLogin))
                        {
                            SQLLog.registaLogBD(Userid, DateTime.Now, "LOGIN NA APLICAÇÃO", "O utilizador com o userid: " + Userid + " efectuou login na aplicação com sucesso.", true);
                            Response.Redirect("/onoff/Home/Default.aspx", false);
                        }
                        else
                        {
                            SQLLog.registaLogBD(Userid, DateTime.Now, "LOGIN NA APLICAÇÃO", "O utilizador com o userid: " + Userid + " efectuou login na aplicação sem sucesso por ser necessário efectuar uma nova instalação.", true);
                            FormsAuthentication.SignOut();
                            string erroInstall = "Por favor contacte o administrador de sistema para efectuar uma nova instalação!";
                            Response.Redirect("/onoff/Home/ErrorPage.aspx?erro=" + erroInstall, false);

                        }
                    }
                    else
                    {
                        if (Request.QueryString["ReturnUrl"] != null)
                        {
                            Response.Redirect(FormsAuthentication.GetRedirectUrl(username, true));
                        }
                        else
                        {
                            Response.Redirect("~/Default.aspx", false);
                            //Response.Redirect("http://www.google.pt", false);
                        }

                    }

                }
                else
                {
                    erro.Visible = true;
                    erro.InnerHtml = "Nome do Utilizador ou Password estão errados. Tente novamente!";
                    tbpassword.Focus();
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("/onoff/Home/ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaRequisitosNovaInstalacao(string email)
        {
            if (email == "ricardoshm@gmail.com")
            {
                try
                {
                    int contaEntradas = (from i in DC.Configuracaos
                                         select i).Count();

                    if (contaEntradas <= 0)
                        return false;
                    else
                        return true;
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            return true;
        }
    }
}
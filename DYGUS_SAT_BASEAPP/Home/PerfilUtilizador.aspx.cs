using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class PerfilUtilizador : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
Guid userid = new Guid();
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Security.Principal.IPrincipal User = HttpContext.Current.User;

            
            string UserName = "";
            if (User.Identity.IsAuthenticated == true)
            {
            }
            else
                Response.Redirect("~/Default.aspx", true);
            if (!Page.IsPostBack)
            {

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
                        }

                        var utilizador = from util in DC.aspnet_Users
                                         where util.UserId == userid
                                         select util;


                        foreach (var item in utilizador)
                        {
                            UserName = item.UserName;
                        }

                        var utilizadorm = from util in DC.aspnet_Memberships
                                          where util.UserId == userid
                                          select util;


                        foreach (var item in utilizadorm)
                        {
                            emailuser.Value = item.Email;
                        }

                        tbusername.Text = UserName;
                        tbpassantiga.Focus();
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                    }
                }

                else
                {
                    Response.Redirect("~/Default.aspx", true);

                }
            }
        }


        protected void btnGravar_Click(object sender, EventArgs e)
        {
            if (Authenticated(emailuser.Value, tbpassantiga.Text) == true)
            {

                if (tbnovapass.Text != "" && tbconfirmanovapass.Text != "" && tbpassantiga.Text != "")
                {
                    if (tbnovapass.Text == tbconfirmanovapass.Text)
                    {
                        try
                        {
                            AlterarPassword();
                            DC.SubmitChanges();
                            SQLLog.registaLogBD(userid, DateTime.Now, "Perfil de Utilizador", "Alterada a password de utilizador", true);
                        }
                        catch (Exception ex)
                        {
                            ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                        }

                        sucesso.Visible = sucessoMessage.Visible = true;
                        sucessoMessage.InnerHtml = "Password alterada com sucesso!";
                        tbpassantiga.Focus();
                    }
                    else
                    {
                        erro.Visible = errorMessage.Visible = true;
                        errorMessage.InnerHtml = "Password e confirmação de password devem ser iguais!";
                        tbpassantiga.Focus();
                    }

                }
                else
                {
                    erro.Visible = errorMessage.Visible = true;
                    errorMessage.InnerHtml = "Todos os campos são de preenchimento obrigatório!";
                    tbpassantiga.Focus();
                }


            }
            else
            {
                erro.Visible = errorMessage.Visible = true;
                errorMessage.InnerHtml = "Password Actual incorrecta!";
                tbpassantiga.Focus();
            }
        }

        private void AlterarPassword()
        {
            try
            {
                if (tbnovapass.Text == tbconfirmanovapass.Text)
                {

                    string username = "";
                    string pass = "";


                    var userResults = from u in DC.aspnet_Memberships
                                      where u.Email == User.Identity.Name
                                      select u;
                    foreach (var item in userResults)
                    {
                        username = item.aspnet_User.UserName;
                        pass = item.Password;
                    }

                    string hashedpass = pass;

                    string strUserInputtedHashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(tbpassantiga.Text, "sha1");
                    if (strUserInputtedHashedPassword == hashedpass)
                    {

                        string password = "";
                        password = tbnovapass.Text;

                        string passwordencriptada = "";

                        passwordencriptada = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "sha1");

                        LINQ_DB.aspnet_Membership aspm = new LINQ_DB.aspnet_Membership();

                        aspm = userResults.First();
                        aspm.Password = passwordencriptada;
                        aspm.LastPasswordChangedDate = DateTime.Now;
                        DC.SubmitChanges();
                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }


        }

        private bool Authenticated(string email, string passw)
        {
            bool resultado_mail = false;
            bool resultado_aprovacao = false;
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

                    string pass = "";
                    Boolean aprovado = false;

                    var pas = from users in DC.aspnet_Memberships
                              where users.Email == email.ToString()
                              select users;

                    foreach (var item in pas)
                    {
                        pass = item.Password;
                        aprovado = item.IsApproved;
                    }

                    string hashedpass = pass;

                    string strUserInputtedHashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(passw, "sha1");
                    if (strUserInputtedHashedPassword == hashedpass)
                    {
                        resultado_mail = true;
                    }
                    else
                    {
                        resultado_mail = false;
                    }

                    if (aprovado == true)
                    {
                        resultado_aprovacao = true;
                    }
                    else
                    {
                        resultado_aprovacao = false;
                    }
                }
                if (resultado_mail == true && resultado_aprovacao == true)
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

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbpassantiga.Text = tbconfirmanovapass.Text = tbnovapass.Text = "";
            tbpassantiga.Focus();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", false);
        }
    }
}
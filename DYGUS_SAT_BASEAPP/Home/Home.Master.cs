using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class Home : System.Web.UI.MasterPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        string errovalidade = "";

        protected System.Timers.Timer tSuperAdmin = new System.Timers.Timer();
        protected System.Timers.Timer tAdmin = new System.Timers.Timer();
        protected System.Timers.Timer tCliente = new System.Timers.Timer();
        protected System.Timers.Timer tTecnico = new System.Timers.Timer();
        protected System.Timers.Timer tReparador = new System.Timers.Timer();
        protected System.Timers.Timer tLojista = new System.Timers.Timer();
        public Guid useridPublic = new Guid();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }

            System.Security.Principal.IPrincipal User = HttpContext.Current.User;

            Guid userid = new Guid();
            string UserName = "";
            string emailUser = "";

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
                        emailUser = item.LoweredEmail;
                    }

                    var utilizador = from util in DC.aspnet_Users
                                     where util.UserId == userid
                                     select util;


                    foreach (var item in utilizador)
                    {
                        UserName = item.UserName;
                    }

                    username.Value = UserName;
                    username2.Value = UserName;
                    mail.Value = emailUser;

                    var p = from d in DC.aspnet_UsersInRoles
                            where d.UserId == userid
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

                    if (regra == "Cliente")
                    {
                        var verificaValidade = from val in DC.Parceiros
                                               where val.USERID == userid
                                               select val;

                        foreach (var item in verificaValidade)
                        {
                            if (item.DATA_VALIDADE_LOGIN > DateTime.Now)
                            {
                                Thread t = new Thread(() => constroiMenuCliente());
                                t.Start();
                                tCliente.Interval = 5;
                                tCliente.Enabled = true;
                                Thread tempo = new Thread(() => tCliente.Start());
                                tempo.Start();
                            }
                            else
                            {
                                ShowAlertMessage("A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.");
                                errovalidade = "A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.";
                                FormsAuthentication.SignOut();
                                Response.Redirect("ErrorPage.aspx?erro=" + errovalidade, false);
                            }
                        }
                    }

                    if (regra == "Lojista")
                    {
                        var verificaValidade = from val in DC.Funcionarios
                                               where val.USERID == userid
                                               select val;

                        foreach (var item in verificaValidade)
                        {
                            if (item.DATA_VALIDADE_LOGIN > DateTime.Now)
                            {
                                Thread t = new Thread(() => constroiMenuLojista());
                                t.Start();
                                tLojista.Interval = 5;
                                tLojista.Enabled = true;
                                Thread tempo = new Thread(() => tLojista.Start());
                                tempo.Start();
                            }
                            else
                            {
                                ShowAlertMessage("A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.");
                                errovalidade = "A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.";
                                FormsAuthentication.SignOut();
                                Response.Redirect("ErrorPage.aspx?erro=" + errovalidade, false);
                            }
                        }
                    }

                    if (regra == "Tecnico")
                    {
                        var verificaValidade = from val in DC.Funcionarios
                                               where val.USERID == userid
                                               select val;

                        foreach (var item in verificaValidade)
                        {
                            if (item.DATA_VALIDADE_LOGIN > DateTime.Now)
                            {
                                Thread t = new Thread(() => constroiMenuTecnico());
                                t.Start();
                                tTecnico.Interval = 5;
                                tTecnico.Enabled = true;
                                Thread tempo = new Thread(() => tTecnico.Start());
                                tempo.Start();
                            }
                            else
                            {
                                ShowAlertMessage("A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.");
                                errovalidade = "A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.";
                                FormsAuthentication.SignOut();
                                Response.Redirect("ErrorPage.aspx?erro=" + errovalidade, false);
                            }
                        }
                    }

                    if (regra == "Reparador")
                    {
                        var verificaValidade = from val in DC.Parceiros
                                               where val.USERID == userid
                                               select val;

                        foreach (var item in verificaValidade)
                        {
                            if (item.DATA_VALIDADE_LOGIN > DateTime.Now)
                            {
                                Thread t = new Thread(() => constroiMenuReparador());
                                t.Start();
                                tReparador.Interval = 5;
                                tReparador.Enabled = true;
                                Thread tempo = new Thread(() => tReparador.Start());
                                tempo.Start();
                            }
                            else
                            {
                                ShowAlertMessage("A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.");
                                errovalidade = "A sua conta está caducada/suspensa/cancelada! Por favor contacte ricardoshm@gmail.com.";
                                FormsAuthentication.SignOut();
                                Response.Redirect("ErrorPage.aspx?erro=" + errovalidade, false);
                            }
                        }
                    }

                    if (regra == "Administrador")
                    {
                        Thread t = new Thread(() => constroiMenuAdmin());
                        t.Start();
                        tAdmin.Interval = 5;
                        tAdmin.Enabled = true;
                        Thread tempo = new Thread(() => tAdmin.Start());
                        tempo.Start();
                    }


                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            else
                Response.Redirect("~/Default.aspx", true);

            useridPublic = userid;
        }

        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }


        protected void constroiMenuAdmin()
        {
            menuAdmin.Style.Add("display", "none");
        }

        protected void constroiMenuCliente()
        {
            menuAdmin.Style.Add("display", "none");
            menuloja.Style.Add("display", "none");
            menuarmazem.Style.Add("display", "none");
            menutecnicos.Style.Add("display", "none");
            menureparadores.Style.Add("display", "none");
            menulojistas.Style.Add("display", "none");
            menuclientes.Style.Add("display", "none");
            menucondicoes.Style.Add("display", "none");
            menuartigos.Style.Add("display", "none");
            menuor.Style.Add("display", "none");
            menueqs.Style.Add("display", "none");
            menuorc.Style.Add("display", "none");
            menurelatorios.Style.Add("display", "none");
            //dashboard.Style.Add("display", "block");
            //config.Style.Add("display", "none");
            //dados.Style.Add("display", "none");
            //or.Style.Add("display", "none");
            //clientes.Style.Add("display", "none");
            //reparadores.Style.Add("display", "none");
            //utilizadores.Style.Add("display", "none");
            //tecnicos.Style.Add("display", "none");
            //stocks.Style.Add("display", "none");
            //relatorios.Style.Add("display", "none");
            //iconessuperiores.Style.Add("display", "none");
            //iconessuperiores.Visible = false;
        }

        protected void constroiMenuLojista()
        {
            menuAdmin.Style.Add("display", "none");
            menuloja.Style.Add("display", "none");
            menuarmazem.Style.Add("display", "none");
            menutecnicos.Style.Add("display", "none");
            menureparadores.Style.Add("display", "none");
            menulojistas.Style.Add("display", "none");
            menuclientes.Style.Add("display", "none");
            menucondicoes.Style.Add("display", "none");
            menurelatorios.Style.Add("display", "none");
            //iconessuperiores.Style.Add("display", "none");
            //iconessuperiores.Visible = false;
            //dashboard.Style.Add("display", "block");
            //config.Style.Add("display", "none");
            //dados.Style.Add("display", "block");
            //marcasedit.Style.Add("display", "none");
            ////marcasdelete.Style.Add("display", "none");
            //modelosedit.Style.Add("display", "none");
            ////modelosdelete.Style.Add("display", "none");
            //eqsedit.Style.Add("display", "none");
            //eqsdelete.Style.Add("display", "none");
            //opedit.Style.Add("display", "none");
            ////opdelete.Style.Add("display", "none");
            ////estadosrepair.Style.Add("display", "none");
            ////estadosrepair.Visible = false;
            ////estadosinsert.Style.Add("display", "none");
            ////estadosedit.Style.Add("display", "none");
            ////estadosdelete.Style.Add("display", "none");
            ////condgerais.Style.Add("display", "none");
            ////condgerais.Visible = false;
            //condedit.Style.Add("display", "none");
            //condinsert.Style.Add("display", "none");
            //conddelete.Style.Add("display", "none");
            //or.Style.Add("display", "block");
            //orinsert.Style.Add("display", "block");
            //ormanage.Style.Add("display", "none");
            ////orlistatrib.Style.Add("display", "block");
            //orlist.Style.Add("display", "none");
            //orfecho.Style.Add("display", "block");
            //orlistfechadas.Style.Add("display", "block");
            //orlistgeral.Style.Add("display", "block");
            //clientes.Style.Add("display", "block");
            //clientesedit.Style.Add("display", "none");
            //clientesdelete.Style.Add("display", "none");
            //reparadores.Style.Add("display", "none");
            //utilizadores.Style.Add("display", "none");
            //tecnicos.Style.Add("display", "none");
            //stocks.Style.Add("display", "block");
            ////armazemgeral.Visible = false;
            //stocksarmazeminsert.Style.Add("display", "none");
            //stocksarmazemedit.Style.Add("display", "none");
            //stocksarmazemdelete.Style.Add("display", "none");
            //stocksprodutoinsert.Style.Add("display", "block");
            //stocksprodutoedit.Style.Add("display", "none");
            //stocksprodutodelete.Style.Add("display", "none");
            //relatorios.Style.Add("display", "none");


        }

        protected void constroiMenuTecnico()
        {
            menuAdmin.Style.Add("display", "none");
            menuloja.Style.Add("display", "none");
            menuarmazem.Style.Add("display", "none");
            menutecnicos.Style.Add("display", "none");
            menureparadores.Style.Add("display", "none");
            menulojistas.Style.Add("display", "none");
            menuclientes.Style.Add("display", "none");
            menucondicoes.Style.Add("display", "none");
            menurelatorios.Style.Add("display", "none");
            //iconessuperiores.Style.Add("display", "none");
            //iconessuperiores.Visible = false;
            //dashboard.Style.Add("display", "block");
            //config.Style.Add("display", "none");
            //dados.Style.Add("display", "block");
            //marcasedit.Style.Add("display", "none");
            ////marcasdelete.Style.Add("display", "none");
            //modelosedit.Style.Add("display", "none");
            ////modelosdelete.Style.Add("display", "none");
            //eqsedit.Style.Add("display", "none");
            //eqsdelete.Style.Add("display", "none");
            //opedit.Style.Add("display", "none");
            ////opdelete.Style.Add("display", "none");
            ////estadosrepair.Style.Add("display", "none");
            ////estadosrepair.Visible = false;
            ////estadosinsert.Style.Add("display", "none");
            ////estadosedit.Style.Add("display", "none");
            ////estadosdelete.Style.Add("display", "none");
            //condgerais.Style.Add("display", "none");
            //condgerais.Visible = false;
            //condedit.Style.Add("display", "none");
            //condinsert.Style.Add("display", "none");
            //conddelete.Style.Add("display", "none");
            //or.Style.Add("display", "block");
            //orinsert.Style.Add("display", "block");
            //ormanage.Style.Add("display", "block");
            ////orlistatrib.Style.Add("display", "block");
            //orlist.Style.Add("display", "block");
            //orfecho.Style.Add("display", "block");
            //orlistfechadas.Style.Add("display", "block");
            //orlistgeral.Style.Add("display", "block");
            //clientes.Style.Add("display", "none");
            //reparadores.Style.Add("display", "none");
            //utilizadores.Style.Add("display", "none");
            //tecnicos.Style.Add("display", "none");
            //stocks.Style.Add("display", "block");
            ////armazemgeral.Visible = false;
            //stocksarmazeminsert.Style.Add("display", "none");
            //stocksarmazemedit.Style.Add("display", "none");
            //stocksarmazemdelete.Style.Add("display", "none");
            //stocksprodutoinsert.Style.Add("display", "block");
            //stocksprodutoedit.Style.Add("display", "none");
            //stocksprodutodelete.Style.Add("display", "none");
            //relatorios.Style.Add("display", "none");
        }

        protected void constroiMenuReparador()
        {
            menuAdmin.Style.Add("display", "none");
            menuloja.Style.Add("display", "none");
            menuarmazem.Style.Add("display", "none");
            menutecnicos.Style.Add("display", "none");
            menureparadores.Style.Add("display", "none");
            menulojistas.Style.Add("display", "none");
            menuclientes.Style.Add("display", "none");
            menucondicoes.Style.Add("display", "none");
            menuartigos.Style.Add("display", "none");
            menuor.Style.Add("display", "none");
            menueqs.Style.Add("display", "none");
            menuorc.Style.Add("display", "none");
            menurelatorios.Style.Add("display", "none");
            //dashboard.Style.Add("display", "block");
            //config.Style.Add("display", "none");
            //dados.Style.Add("display", "none");
            ////estadosrepair.Style.Add("display", "none");
            ////estadosrepair.Visible = false;
            ////estadosinsert.Style.Add("display", "none");
            ////estadosedit.Style.Add("display", "none");
            ////estadosdelete.Style.Add("display", "none");
            //condgerais.Style.Add("display", "none");
            //condgerais.Visible = false;
            //condedit.Style.Add("display", "none");
            //condinsert.Style.Add("display", "none");
            //conddelete.Style.Add("display", "none");
            //or.Style.Add("display", "block");
            //orinsert.Style.Add("display", "none");
            //ormanage.Style.Add("display", "none");
            ////orlistatrib.Style.Add("display", "none");
            //orlist.Style.Add("display", "block");
            //orfecho.Style.Add("display", "none");
            //orlistfechadas.Style.Add("display", "none");
            //orlistgeral.Style.Add("display", "none");
            //clientes.Style.Add("display", "none");
            //reparadores.Style.Add("display", "none");
            //stocks.Style.Add("display", "none");
            //utilizadores.Style.Add("display", "none");
            //tecnicos.Style.Add("display", "none");
            //relatorios.Style.Add("display", "none");
            //iconessuperiores.Style.Add("display", "none");
            //iconessuperiores.Visible = false;


        }

        protected void lgStatus_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            SQLLog.registaLogBD(useridPublic, DateTime.Now, "LOGOUT DA APLICAÇÃO", "O utilizador com o userid: " + useridPublic + " efectuou logout na aplicação com sucesso.", true);
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx", true);

        }
    }
}
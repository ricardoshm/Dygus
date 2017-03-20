using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class GerirLogs : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        Guid userid = new Guid();
        string UserName = "";
        string emailUser = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Security.Principal.IPrincipal User = HttpContext.Current.User;

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

                    if (regra != "SuperAdmin")
                        Response.Redirect("~/Default.aspx", true);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            else
                Response.Redirect("~/Default.aspx", true);


        }

        protected void btnPesquisa_Click(object sender, EventArgs e)
        {
            try
            {
                var carregaLog = from l in DC.Logs
                                 where l.DATA >= datainicio.SelectedDate && l.DATA < datafim.SelectedDate.Value.AddDays(1) && l.VISIVEL == true
                                 orderby l.DATA descending
                                 select new
                                 {
                                     DATA = l.DATA,
                                     UTILIZADOR = l.USERID,
                                     MENU = l.MENU,
                                     DESCRICAO = l.DESCRICAO
                                 };

                foreach (var item in carregaLog)
                {
                    linhasOrc.Text +=
     "<tr><td class='center'>" + item.DATA + "</td><td class='center'>" + getNomeUser(item.UTILIZADOR) + "</td><td class='center'>" + item.MENU + "</td><td class='center'>" + item.DESCRICAO + "</td></tr>";
                }

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }


        }

        public string getNomeUser(Guid userid)
        {
            string nomeUser = "";

            try
            {
                var pesquisanomefuncionario = from f in DC.Funcionarios
                                              where f.USERID.Value == userid
                                              select f;

                foreach (var item in pesquisanomefuncionario)
                {
                    nomeUser = item.NOME.ToString();
                }

                if (string.IsNullOrEmpty(nomeUser))
                {
                    var pesquisanomeparceiro = from p in DC.Parceiros
                                               where p.USERID.Value == userid
                                               select p;

                    foreach (var itemp in pesquisanomeparceiro)
                    {
                        nomeUser = itemp.NOME.ToString();
                    }
                }

                if (string.IsNullOrEmpty(nomeUser))
                {
                    var pesquisausername = from a in DC.aspnet_Memberships
                                           join u in DC.aspnet_Users on a.UserId equals u.UserId
                                           where a.UserId == userid
                                           select a;

                    foreach (var itemu in pesquisausername)
                    {
                        nomeUser = "(" + itemu.aspnet_User.UserName.ToString() + ")";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
            return nomeUser;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            datainicio.Clear();
            datafim.Clear();
            errorMessage.InnerHtml = "";
        }

        protected void btnLimparLogs_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                var ll = from l in DC.Logs
                         select l;

                foreach (var item in ll)
                {
                    DC.Logs.DeleteOnSubmit(item);
                    DC.SubmitChanges();
                    SQLLog.registaLogBD(userid, DateTime.Now, "LOGS", "Foram eliminados todos os logs da BD", true);
                }

                datafim.Clear();
                datainicio.Clear();
                linhasOrc.Text = "";
            }
        }
    }
}
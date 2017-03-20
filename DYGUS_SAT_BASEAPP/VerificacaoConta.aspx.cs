using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP
{
    public partial class VerificacaoConta : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                erro.Visible = false;
                sucesso.Visible = false;
                btnLogin.Visible = false;

                //if the id is in the query string
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {


                    //store the user id    
                    Guid userId = new Guid(Request.QueryString["ID"]);
                    //attempt to get the user's information    
                    MembershipUser user = Membership.GetUser(userId);
                    //check if the user exists

                    var email = from em in DC.aspnet_Memberships
                                where em.UserId == userId
                                select em;

                    foreach (var item in email)
                    {
                        tbusername.Text = item.Email;
                    }

                    if (user != null)
                    {
                        //check to make sure the user is not approved yet        
                        if (!user.IsApproved)
                        {
                            //approve the user            
                            user.IsApproved = true;
                            //update the account in the database 
                            try
                            {
                                Membership.UpdateUser(user);
                                sucesso.Visible = true;
                                btnLogin.Visible = true;

                                var userinrole = from users in DC.aspnet_UsersInRoles
                                                 where users.UserId == userId
                                                 select users;

                                foreach (var item in userinrole)
                                {
                                    if (item.aspnet_Role.RoleName == "Cliente" || item.aspnet_Role.RoleName == "Reparador")
                                    {
                                        LINQ_DB.Parceiro ACTUALIZAPARCEIRO = new LINQ_DB.Parceiro();

                                        var parceiro = from p in DC.Parceiros
                                                       where p.USERID == userId
                                                       select p;

                                        ACTUALIZAPARCEIRO = parceiro.First();
                                        ACTUALIZAPARCEIRO.ACTIVO = true;
                                        ACTUALIZAPARCEIRO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                        DC.SubmitChanges();
                                    }

                                    if (item.aspnet_Role.RoleName == "Tecnico" || item.aspnet_Role.RoleName == "Lojista")
                                    {
                                        LINQ_DB.Funcionario ACTUALIZAFUNCIONARIO = new LINQ_DB.Funcionario();

                                        var funcionario = from p in DC.Funcionarios
                                                       where p.USERID == userId
                                                       select p;

                                        ACTUALIZAFUNCIONARIO = funcionario.First();
                                        ACTUALIZAFUNCIONARIO.ACTIVO = true;
                                        ACTUALIZAFUNCIONARIO.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                                        DC.SubmitChanges();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                                btnLogin.Visible = false;
                                erro.Visible = true;
                            }

                        }
                        else
                        {
                            Response.Redirect("~/Default.aspx", false);
                        }
                    }
                }
                else
                {
                    erro.Visible = true;
                    btnLogin.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx", true);
        }
    }
}
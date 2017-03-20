using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class GerirCliente : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            //listaclientes.Style.Add("display", "none");
            //listaparceiros.Style.Add("display", "none");
            //listafuncionarios.Style.Add("display", "none");

            //(Page.Master.FindControl("relatorios") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("relatoriosclientes") as HtmlControl).Attributes.Add("class", "active");
            Guid userid = new Guid();
            Guid Role = new Guid();
            string UserName = "";
            string EmailLogin = "";

            if (User.Identity.IsAuthenticated == true)
            {
                var us = from users in DC.aspnet_Memberships
                         where users.LoweredEmail == User.Identity.Name
                         select users;

                foreach (var item in us)
                {
                    userid = item.UserId;
                    EmailLogin = item.LoweredEmail;
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

                if (regra != "SuperAdmin")
                    Response.Redirect("Default.aspx", false);

            }
            else
                Response.Redirect("~/Default.aspx", true);

        }


        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }

        protected void listagemgeralClientes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carrega = from cliente in DC.Parceiros
                              join member in DC.aspnet_Memberships on cliente.USERID.Value equals member.UserId
                              where member.IsApproved == false || cliente.ACTIVO == false
                              orderby cliente.ID descending
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
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message); Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemgeralClientes_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            Guid newU = new Guid();

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                String val = item["ID"].Text;

                var procuraG = from p1 in DC.Parceiros
                               where p1.ID == Convert.ToInt32(val)
                               select p1;

                foreach (var itemy in procuraG)
                {
                    newU = itemy.USERID.Value;
                }

                LINQ_DB.aspnet_Membership MEMBER = new LINQ_DB.aspnet_Membership();

                var procura = from p in DC.aspnet_Memberships
                              where p.UserId == newU
                              select p;

                MEMBER = procura.First();
                MEMBER.IsApproved = true;
                DC.SubmitChanges();

                LINQ_DB.Parceiro PARC = new LINQ_DB.Parceiro();

                var procurap = from pp in DC.Parceiros
                               where pp.USERID.Value == newU
                               select pp;

                PARC = procurap.First();
                PARC.ACTIVO = true;
                DC.SubmitChanges();

                ShowAlertMessage("Parceiro atualizado com sucesso!");

            }
        }

        protected void listagemgeralClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SELECTCLIENTE")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    var UserId = from a in DC.Parceiros
                                 where a.ID == Convert.ToInt32(id)
                                 select a;

                    foreach (var iteme in UserId)
                    {

                        LINQ_DB.aspnet_Membership MOELIMINA = new LINQ_DB.aspnet_Membership();

                        var a = from t in DC.aspnet_Memberships
                                where t.UserId == iteme.USERID.Value
                                select t;

                        foreach (var clienteeliminado in a)
                        {
                            MOELIMINA = a.First();
                            MOELIMINA.IsApproved = true;
                            DC.SubmitChanges();
                        }

                        LINQ_DB.Parceiro PELIMINA = new LINQ_DB.Parceiro();

                        var aa = from p1 in DC.Parceiros
                                 where p1.USERID.Value == iteme.USERID.Value
                                 select p1;

                        foreach (var peliminado in aa)
                        {
                            PELIMINA = aa.First();
                            PELIMINA.ACTIVO = true;
                            DC.SubmitChanges();
                        }
                    }
                    listagemgeralClientes.Rebind();
                    ShowAlertMessage("Cliente activado com sucesso!");

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaDataValidadeClientes_ItemCommand(object sender, GridCommandEventArgs e)
        {
            DateTime hoje = DateTime.Today;
            try
            {
                if (e.CommandName == "SELECTCLIENTE")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    var UserId = from a in DC.Parceiros
                                 where a.ID == Convert.ToInt32(id)
                                 select a;

                    foreach (var iteme in UserId)
                    {

                        LINQ_DB.Parceiro PELIMINA = new LINQ_DB.Parceiro();

                        var aa = from p1 in DC.Parceiros
                                 where p1.ID == Convert.ToInt32(id)
                                 select p1;

                        foreach (var peliminado in aa)
                        {
                            PELIMINA = aa.First();
                            PELIMINA.DATA_VALIDADE_LOGIN = hoje.AddYears(1);
                            DC.SubmitChanges();
                        }
                    }
                    listaDataValidadeClientes.Rebind();
                    ShowAlertMessage("Cliente actualizado com sucesso!");

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaDataValidadeClientes_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DateTime hoje = DateTime.Now;

            try
            {
                var carrega = from cliente in DC.Parceiros
                              join member in DC.aspnet_Memberships on cliente.USERID.Value equals member.UserId
                              where cliente.DATA_VALIDADE_LOGIN.Value <= hoje
                              orderby cliente.ID descending
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
                                  DATA = cliente.DATA_VALIDADE_LOGIN.Value.ToShortDateString()
                              };

                listaDataValidadeClientes.DataSourceID = "";
                listaDataValidadeClientes.DataSource = carrega;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message); Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaDataValidadeFuncionarios_ItemCommand(object sender, GridCommandEventArgs e)
        {
            DateTime hoje = DateTime.Today;
            try
            {
                if (e.CommandName == "SELECTCLIENTE")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;

                    var UserId = from a in DC.Funcionarios
                                 where a.ID == Convert.ToInt32(id)
                                 select a;

                    foreach (var iteme in UserId)
                    {

                        LINQ_DB.Funcionario PELIMINA = new LINQ_DB.Funcionario();

                        var aa = from p1 in DC.Funcionarios
                                 where p1.ID == Convert.ToInt32(id)
                                 select p1;

                        foreach (var peliminado in aa)
                        {
                            PELIMINA = aa.First();
                            PELIMINA.DATA_VALIDADE_LOGIN = hoje.AddYears(1);
                            DC.SubmitChanges();
                        }
                    }
                    listaDataValidadeFuncionarios.Rebind();
                    ShowAlertMessage("Cliente actualizado com sucesso!");

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listaDataValidadeFuncionarios_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DateTime hoje = DateTime.Now;

            try
            {
                var carrega = from cliente in DC.Funcionarios
                              join member in DC.aspnet_Memberships on cliente.USERID.Value equals member.UserId
                              where cliente.DATA_VALIDADE_LOGIN.Value <= hoje
                              orderby cliente.ID descending
                              select new
                              {
                                  UserId = cliente.USERID,
                                  CODIGO = cliente.CODIGO,
                                  NOME = cliente.NOME,
                                  EMAIL = cliente.EMAIL,
                                  ESTADO = cliente.ACTIVO.Value,
                                  ID = cliente.ID,
                                  DATA = cliente.DATA_VALIDADE_LOGIN.HasValue ? cliente.DATA_VALIDADE_LOGIN.Value.ToShortDateString() : null
                              };

                listaDataValidadeFuncionarios.DataSourceID = "";
                listaDataValidadeFuncionarios.DataSource = carrega;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message); Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void tbPesquisaEmail_TextChanged(object sender, EventArgs e)
        {
            string email = tbPesquisaEmail.Text;

            if (email != "ricardoshm@gmail.com")
            {
                try
                {
                    var carrega = from cliente in DC.aspnet_Memberships
                                  where cliente.Email == email
                                  select new
                                  {
                                      UserId = cliente.UserId,
                                      DATA = cliente.LastPasswordChangedDate.ToShortDateString()
                                  };

                    ListaPesquisa.DataSourceID = "";
                    ListaPesquisa.DataSource = carrega;
                    ListaPesquisa.DataBind();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void ListaPesquisa_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SELECTCLIENTE")
                {
                    var item = (GridDataItem)e.Item;
                    Guid id = new Guid(item["USERID"].Text);

                    LINQ_DB.aspnet_Membership UPDATE = new LINQ_DB.aspnet_Membership();

                    var pesquisa = from a in DC.aspnet_Memberships
                                   where a.UserId == id
                                   select a;

                    foreach (var iteme in pesquisa)
                    {
                        UPDATE = pesquisa.First();
                        UPDATE.Password = "7110EDA4D09E062AA5E4A390B0A572AC0D2C0220";
                        UPDATE.LastPasswordChangedDate = DateTime.Now;
                        DC.SubmitChanges();
                    }

                    ListaPesquisa.Rebind();
                    ShowAlertMessage("Actualizado com sucesso!");

                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }


    }
}
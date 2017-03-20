using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class GerirMeuORC : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        protected System.Timers.Timer timer1 = new System.Timers.Timer();
        protected System.Timers.Timer timer2 = new System.Timers.Timer();
        string myid = "";
        Guid myuserid = new Guid();

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

                if (regra == "Administrador" || regra == "SuperAdmin")
                { }

            }
            else
                Response.Redirect("~/Default.aspx", true);



            if (Request.QueryString["ID"] != null && Request.QueryString["userid"] != null)
            {
                myid = Request.QueryString["ID"];
                myuserid = new Guid(Request.QueryString["userid"].ToString());
            }
            else
                Response.Redirect("Default.aspx", false);

            if (!Page.IsPostBack)
            {
                carregaORC();
                carregaComentarios();
            }

        }

        protected string GetRoleName(Guid useridComentario)
        {
            string resultado = "";
            try
            {
                var p = from d in DC.aspnet_UsersInRoles
                        where d.UserId == useridComentario
                        select d;

                foreach (var item in p)
                {
                    var pesqsuisa = from f in DC.aspnet_Roles
                                    where f.RoleId == item.RoleId
                                    select f;

                    foreach (var imt in pesqsuisa)
                    {
                        resultado = imt.RoleName;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

            return resultado;
        }

        protected void carregaComentarios()
        {
            try
            {

                string myRole = "";


                var pesquisaComent = from o in DC.Orcamentos_Comentarios
                                     where o.ID_ORCAMENTO == Convert.ToInt32(myid)
                                     orderby o.DATA_COMENTARIO.Value descending
                                     select o;

                foreach (var item in pesquisaComent)
                {
                    myRole = GetRoleName(item.USERID_COMENTARIO.Value);

                    if (myRole == "Cliente")
                    {
                        var pesquisaNome = from nome in DC.Parceiros
                                           where nome.USERID.Value == item.USERID_COMENTARIO.Value
                                           select nome;

                        foreach (var nomeP in pesquisaNome)
                        {
                            comentarioCliente.Text +=
                                "<li class='clearfix leftside'>" +
                                        "<div class='user'>" +
                                            "<div class='avatar'>" +
                                                "<img src='../images/no_avatar.jpg' alt='' />" +
                                            "</div>" +
                                            "<span class='ago' id='dataComentUser' runat='server'>" + item.DATA_COMENTARIO.Value.ToShortDateString() + "<br />" + item.DATA_COMENTARIO.Value.ToShortTimeString() + "</span>" +
                                        "</div>" +
                                        "<div class='message' style='width:90%; margin-left:85px;'><span class='name' id='nomeUser' runat='server'>" + nomeP.NOME + "</span><span class='txt' id='comentUser' runat='server'>" + item.COMENTARIO.ToString() + "</span></div>" +
                                    "</li>";
                        }
                    }

                    if (myRole == "Administrador" || myRole == "SuperAdmin")
                    {
                        var pesquisaNome = from nome in DC.Orcamentos
                                           where nome.ID == Convert.ToInt32(myid)
                                           select nome;

                        foreach (var nomeU in pesquisaNome)
                        {
                            comentarioAdmin.Text +=
                                        "<li class='clearfix rightside'>" +
                                                    "<div class='user'>" +
                                                        "<div class='avatar'>" +
                                                            "<img src='../images/no_avatar.jpg' alt='' />" +
                                                        "</div>" +
                                                        "<span class='ago' id='dataComentUser' runat='server'>" + item.DATA_COMENTARIO.Value.ToShortDateString() + "<br />" + item.DATA_COMENTARIO.Value.ToShortTimeString() + "</span>" +
                                                    "</div>" +
                                                    "<div class='message' style='width:90%;'><span class='name' id='nomeAdmin' runat='server'>" + nomeU.Loja.NOME.ToString() + "</span><span class='txt' id='comentAdmin' runat='server'>" + item.COMENTARIO.ToString() + "</span></div>" +
                                                "</li>";
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

        protected void btnInserirComent_Click(object sender, EventArgs e)
        {
            string perfil = "";

            try
            {
                Guid userid = new Guid();

                if (!string.IsNullOrEmpty(tbComentario.Text))
                {
                    if (User.Identity.IsAuthenticated == true)
                    {
                        var us = from users in DC.aspnet_Memberships
                                 where users.LoweredEmail == User.Identity.Name
                                 select users;

                        foreach (var item in us)
                        {
                            userid = item.UserId;
                        }

                        LINQ_DB.Orcamentos_Comentario UPDATECOMENT = new LINQ_DB.Orcamentos_Comentario();

                        UPDATECOMENT.ID_ORCAMENTO = Convert.ToInt32(Request.QueryString["ID"]);
                        UPDATECOMENT.COMENTARIO = tbComentario.Text;
                        UPDATECOMENT.USERID_COMENTARIO = userid;
                        UPDATECOMENT.DATA_COMENTARIO = DateTime.Now;
                        DC.Orcamentos_Comentarios.InsertOnSubmit(UPDATECOMENT);
                        DC.SubmitChanges();

                        perfil = GetRoleName(userid);
                        Response.Redirect("Default.aspx", false);
                    }
                }
                else
                    ShowAlertMessage("O campo comentário é de preenchimento obrigatório!");
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected void carregaORC()
        {
            try
            {
                var pesquisaOrc = from or in DC.Orcamentos
                                  join parceiro in DC.Parceiros on or.USERID.Value equals parceiro.USERID.Value
                                  where or.ID == Convert.ToInt32(myid) && or.USERID.Value == myuserid
                                  select new
                                  {
                                      NUMOR = or.CODIGO,
                                      //LOJA = or.Loja.URL_FOTO,
                                      DATA_REGISTO = or.DATA_REGISTO.Value.ToShortDateString(),
                                      DATA_ULTIMA_MODIFICACAO = or.DATA_ULTIMA_MODIFICACAO.HasValue ? or.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString() : null,
                                      ESTADO = or.Orcamentos_Estado.DESCRICAO,
                                      IDESTADO = or.ID_ESTADO.Value,
                                      NOMECLIENTE = parceiro.NOME,
                                      CONTACTO = parceiro.TELEFONE,
                                      EMAIL = parceiro.EMAIL,
                                      NOMELOJA = or.Loja.NOME,
                                      MORADALOJA = or.Loja.MORADA + " " + or.Loja.CODPOSTAL + " " + or.Loja.LOCALIDADE,
                                      TELEFONELOJA = or.Loja.TELEFONE,
                                      DESCRICAO = or.DESCRICAO_DETALHADA_AVARIA,
                                      VALOR = or.VALOR_ORCAMENTO.Value
                                  };

                foreach (var item in pesquisaOrc)
                {
                    codOrcamento.InnerHtml = numOrcamento.InnerHtml = item.NUMOR.ToString();
                    //logoLoja.Text += "<img src='../" + item.LOJA + "' />";
                    dataOrcamento.InnerHtml = item.DATA_REGISTO;
                    dataUltModOrcamento.InnerHtml = item.DATA_ULTIMA_MODIFICACAO;
                    estadoOrc.InnerHtml = item.ESTADO;
                    cliente.Text +=
                        "<li><i class='icon16 i-arrow-right-3'></i>Nome: <span class='red-smooth'><strong>" + item.NOMECLIENTE + "</strong></span></li>" +
                        "<li><i class='icon16 i-arrow-right-3'></i>Telefone: <span class='red-smooth'><strong>" + item.CONTACTO + "</strong></span></li>" +
                        "<li><i class='icon16 i-arrow-right-3'></i>Email: <span class='red-smooth'><strong>" + item.EMAIL + "</strong></span></li>";
                    loja.Text +=
                       "<li><i class='icon16 i-arrow-right-3'></i>Nome: <span class='red-smooth'><strong>" + item.NOMELOJA + "</strong></span></li>" +
                       "<li><i class='icon16 i-arrow-right-3'></i>Morada: <span class='red-smooth'><strong>" + item.MORADALOJA + "</strong></span></li>" +
                       "<li><i class='icon16 i-arrow-right-3'></i>Telefone: <span class='red-smooth'><strong>" + item.TELEFONELOJA + "</strong></span></li>";
                    linhasOrc.Text +=
                        "<tr><td class='center'>" + item.DESCRICAO + "</td><td class='center'>" + item.VALOR + " €</td></tr>";

                    if (item.IDESTADO == 3 || item.IDESTADO == 4 || item.IDESTADO == 5 || item.IDESTADO == 6)
                        modoLeitura();
                }

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        public static void ShowAlertMessage(string error)
        {
            var page = HttpContext.Current.Handler as Page;
            if (page == null)
                return;
            error = error.Replace("'", "\'");
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + error + "');", true);
        }

        protected void btnAceitar_Click(object sender, EventArgs e)
        {
            try
            {
                LINQ_DB.Orcamento UPDATE = new LINQ_DB.Orcamento();

                var pesquisaORC = from o in DC.Orcamentos
                                  where o.ID == Convert.ToInt32(myid)
                                  select o;

                UPDATE = pesquisaORC.First();
                UPDATE.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                UPDATE.CLIENTE_ACEITOU = true;
                UPDATE.CLIENTE_REJEITOU = false;
                UPDATE.ID_ESTADO = 2;
                DC.SubmitChanges();

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void modoLeitura()
        {
            btnAceitar.Enabled = btnRejeitar.Enabled = btnInserirComent.Enabled = false;
            tbComentario.ReadOnly = true;
        }

        protected void btnRejeitar_Click(object sender, EventArgs e)
        {
            string confirmValue = Request.Form["confirm_value"];

            if (confirmValue == "Yes")
            {
                try
                {
                    LINQ_DB.Orcamento UPDATE = new LINQ_DB.Orcamento();

                    var pesquisaORC = from o in DC.Orcamentos
                                      where o.ID == Convert.ToInt32(myid)
                                      select o;

                    UPDATE = pesquisaORC.First();
                    UPDATE.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                    UPDATE.CLIENTE_ACEITOU = false;
                    UPDATE.CLIENTE_REJEITOU = true;
                    UPDATE.ID_ESTADO = 3;
                    DC.SubmitChanges();

                    Response.Redirect("Default.aspx", false);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            else
                return;
        }
    }
}
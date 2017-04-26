using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class FecharOrdemReparacao : Telerik.Web.UI.RadAjaxPage
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

                if (regra == "Reparador" || regra == "Cliente")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);

        }

        protected void listagemgeralors_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                 join parceiro in DC.Parceiros on ordem.USERID equals parceiro.USERID
                                 where ordem.ID_ESTADO == 5 && ordem.ID_EQUIPAMENTO_SUBSTITUICAO == null
                                 orderby ordem.ID descending
                                 select new
                                 {
                                     ID = ordem.ID,
                                     CODIGO = ordem.CODIGO,
                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue ? ordem.DATA_REGISTO.Value.ToShortDateString() : null,
                                     NOMECLIENTE = parceiro.NOME,
                                     CODIGOCLIENTE = parceiro.CODIGO,
                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue ? ordem.DATA_CONCLUSAO.Value.ToShortDateString() : null,
                                     ESTADO_OR = ordem.Ordem_Reparacao_Estado.DESCRICAO,
                                     CLIENTENOTIFICADO = ordem.CLIENTE_NOTIFICADO.Value
                                 };

                listagemgeralors.DataSourceID = "";
                listagemgeralors.DataSource = carregaOrs;

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

        protected void listagemgeralors_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "FECHAR")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;
                    var cod = item["CODIGOCLIENTE"].Text;

                    LINQ_DB.Ordem_Reparacao ORDEM = new LINQ_DB.Ordem_Reparacao();

                    var verificaCliente = from or in DC.Ordem_Reparacaos
                                          where or.ID == Convert.ToInt32(id)
                                          select or;

                    ORDEM = verificaCliente.First();
                    if (ORDEM.CLIENTE_NOTIFICADO == true)
                        Response.Redirect("FecharOrdemReparacaoDetalhe.aspx?ID=" + id, false);
                    else
                        ShowAlertMessage("O cliente " + cod.ToString() + " deve ser notificado antes de fechar a OR!");
                }

                if (e.CommandName == "NOTIFICARCLIENTE")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;
                    var cod = item["CODIGOCLIENTE"].Text;

                    LINQ_DB.Ordem_Reparacao ORDEM = new LINQ_DB.Ordem_Reparacao();

                    var verificaCliente = from or in DC.Ordem_Reparacaos
                                          where or.ID == Convert.ToInt32(id)
                                          select or;

                    ORDEM = verificaCliente.First();
                    if (ORDEM.CLIENTE_NOTIFICADO == false)
                    {
                        ORDEM.CLIENTE_NOTIFICADO = true;
                        ORDEM.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                        DC.SubmitChanges();
                        listagemgeralors.Rebind();
                        ShowAlertMessage("O cliente " + cod.ToString() + " foi notificado com êxito!");


                        var cliente = from cli in DC.Parceiros
                                      where cli.CODIGO == cod
                                      select cli;

                        foreach (var itemCliente in cliente)
                        {
                            if (itemCliente.EMAIL != "dygussat@dygus.com")
                            {
                                Thread t = new Thread(() => EnviarEmail(itemCliente.EMAIL.ToString()));
                                t.Start();
                                timer1.Interval = 5;
                                timer1.Enabled = true;
                                Thread tempo = new Thread(() => timer1.Start());
                                tempo.Start();
                            }
                        }
                    }
                    else
                        ShowAlertMessage("O cliente " + cod.ToString() + " já tinha sido notificado!");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        public void EnviarEmail(string email)
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
            url = "http://www.dygus.com/onoff";
            string mailContactoSuporte = "";
            mailContactoSuporte = "ricardoshm@gmail.com";

            msg.Subject = "DYGUS - SAT | Ordem de Reparação Concluída";
            msg.IsBodyHtml = true;

            string Urllogo = Server.MapPath("~\\img\\logo_dygus_sat.png");
            LinkedResource logo = new LinkedResource(Urllogo);
            logo.ContentId = "companylogo";

            string corpohtml = "";
            corpohtml = string.Format("<html><head></head><body> Estimado Cliente,<br/><br/>" +
                "A plataforma DYGUS - SAT informa que a sua ordem de reparação já se encontra concluída! <br/>" +
                "A partir deste momento, poderá dirigir-se à loja e levantar o seu equipamento.<br/><br/>" +
                "Para saber mais, por favor, efectue login na sua àrea reservada em " + url.ToString() + " <br/><br/>" +
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

        protected void listagemorsequipsubst_ItemCommand(object sender, GridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "FECHAR")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;
                    var cod = item["CODIGOCLIENTE"].Text;

                    LINQ_DB.Ordem_Reparacao ORDEM = new LINQ_DB.Ordem_Reparacao();

                    var verificaCliente = from or in DC.Ordem_Reparacaos
                                          where or.ID == Convert.ToInt32(id)
                                          select or;

                    ORDEM = verificaCliente.First();
                    if (ORDEM.CLIENTE_NOTIFICADO == true)
                        Response.Redirect("FecharOrdemReparacaoDetalheES.aspx?ID=" + id, false);
                    else
                        ShowAlertMessage("O cliente " + cod.ToString() + " deve ser notificado antes de fechar a OR!");

                }

                if (e.CommandName == "NOTIFICARCLIENTE")
                {
                    var item = (GridDataItem)e.Item;
                    var id = item["ID"].Text;
                    var cod = item["CODIGOCLIENTE"].Text;

                    LINQ_DB.Ordem_Reparacao ORDEM = new LINQ_DB.Ordem_Reparacao();

                    var verificaCliente = from or in DC.Ordem_Reparacaos
                                          where or.ID == Convert.ToInt32(id)
                                          select or;

                    ORDEM = verificaCliente.First();
                    if (ORDEM.CLIENTE_NOTIFICADO == false)
                    {
                        ORDEM.CLIENTE_NOTIFICADO = true;
                        ORDEM.DATA_ULTIMA_MODIFICACAO = DateTime.Now;
                        DC.SubmitChanges();
                        listagemorsequipsubst.Rebind();
                        ShowAlertMessage("O cliente " + cod.ToString() + " foi notificado com êxito!");


                        var cliente = from cli in DC.Parceiros
                                      where cli.CODIGO == cod
                                      select cli;

                        foreach (var itemCliente in cliente)
                        {
                            if (itemCliente.EMAIL != "dygussat@dygus.com")
                            {
                                Thread t = new Thread(() => EnviarEmail(itemCliente.EMAIL.ToString()));
                                t.Start();
                                timer1.Interval = 5;
                                timer1.Enabled = true;
                                Thread tempo = new Thread(() => timer1.Start());
                                tempo.Start();
                            }
                        }
                    }
                    else
                        ShowAlertMessage("O cliente " + cod.ToString() + " já tinha sido notificado!");
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemorsequipsubst_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaOrs = from ordem in DC.Ordem_Reparacaos
                                 join parceiro in DC.Parceiros on ordem.USERID equals parceiro.USERID
                                 join equip in DC.Equipamentos_Substituicaos on ordem.ID_EQUIPAMENTO_SUBSTITUICAO equals equip.ID
                                 where ordem.ID_ESTADO == 5 && ordem.ID_EQUIPAMENTO_SUBSTITUICAO != null
                                 orderby ordem.ID descending
                                 select new
                                 {
                                     ID = ordem.ID,
                                     CODIGO = ordem.CODIGO,
                                     DATA_REGISTO = ordem.DATA_REGISTO.HasValue?ordem.DATA_REGISTO.Value.ToShortDateString():null,
                                     NOMECLIENTE = parceiro.NOME,
                                     CODIGOCLIENTE = parceiro.CODIGO,
                                     VALOR_FINAL = ordem.VALOR_FINAL.Value,
                                     DATA_CONCLUSAO = ordem.DATA_CONCLUSAO.HasValue?ordem.DATA_CONCLUSAO.Value.ToShortDateString():null,
                                     COD_EQUIP_SUBST = ordem.Equipamentos_Substituicao.CODIGO,
                                     ESTADO_OR = ordem.Ordem_Reparacao_Estado.DESCRICAO,
                                     CLIENTENOTIFICADO = ordem.CLIENTE_NOTIFICADO.Value
                                 };

                listagemorsequipsubst.DataSourceID = "";
                listagemorsequipsubst.DataSource = carregaOrs;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemorsequipsubst_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if (item["CLIENTENOTIFICADO"].Text == "True")
                    item["CLIENTENOTIFICADO"].Text = "Sim";
                if (item["CLIENTENOTIFICADO"].Text == "False")
                    item["CLIENTENOTIFICADO"].Text = "Não";
            }
        }

        protected void listagemgeralors_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;

                if (item["CLIENTENOTIFICADO"].Text == "True")
                    item["CLIENTENOTIFICADO"].Text = "Sim";
                if (item["CLIENTENOTIFICADO"].Text == "False")
                    item["CLIENTENOTIFICADO"].Text = "Não";
            }
        }



    }
}
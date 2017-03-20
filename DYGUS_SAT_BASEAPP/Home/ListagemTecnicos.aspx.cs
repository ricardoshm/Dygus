using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListagemTecnicos : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

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

        protected void listagemTecnicos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaListaTecnicos = from tecnico in DC.Funcionarios
                                           where tecnico.ID_TIPO_FUNCIONARIO == 2 && tecnico.ACTIVO == true
                                           orderby tecnico.DATA_REGISTO descending
                                           select new
                                           {
                                               ID = tecnico.ID,
                                               COD_TECNICO = tecnico.CODIGO,
                                               ID_LOJA = tecnico.Loja.NOME,
                                               NOME = tecnico.NOME,
                                               EMAIL = tecnico.EMAIL,
                                               OBSERVACOES = tecnico.OBSERVACOES,
                                               USERID = tecnico.USERID,
                                               CONTA_ACTIVA = tecnico.ACTIVO.Value,
                                               DATA_ULTIMA_MODIFICACAO = tecnico.DATA_ULTIMA_MODIFICACAO.Value.ToShortDateString().ToString(),
                                               DATA_REGISTO = tecnico.DATA_REGISTO.Value.ToShortDateString().ToString()
                                           };

                listagemTecnicos.DataSourceID = "";
                listagemTecnicos.DataSource = carregaListaTecnicos;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected List<string> returnedValuesTecnicos = new List<string>();

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (Session["returnedValuesTecnicos"] != null)
            {
                returnedValuesTecnicos = (List<string>)Session["returnedValuesTecnicos"];
            }
            GridDataItem dataItem = (GridDataItem)(sender as CheckBox).NamingContainer;

            string id = dataItem["ID"].Text;
            string cod = dataItem["COD_TECNICO"].Text;
            string loja = dataItem["ID_LOJA"].Text;
            string nome = dataItem["NOME"].Text;
            string email = dataItem["EMAIL"].Text;
            string obs = dataItem["OBSERVACOES"].Text;
            string usreid = dataItem["USERID"].Text;
            string activo = dataItem["CONTA_ACTIVA"].Text;
            string dataultima = dataItem["DATA_ULTIMA_MODIFICACAO"].Text;
            string dataregisto = dataItem["DATA_REGISTO"].Text;


            if (checkBox.Checked)
            {
                returnedValuesTecnicos.Add(id);
                returnedValuesTecnicos.Add(cod);
                returnedValuesTecnicos.Add(loja);
                returnedValuesTecnicos.Add(nome);
                returnedValuesTecnicos.Add(email);
                returnedValuesTecnicos.Add(obs);
                returnedValuesTecnicos.Add(usreid);
                returnedValuesTecnicos.Add(activo);
                returnedValuesTecnicos.Add(dataultima);
                returnedValuesTecnicos.Add(dataregisto);
            }
            else
            {
                returnedValuesTecnicos.Remove(id);
                returnedValuesTecnicos.Remove(cod);
                returnedValuesTecnicos.Remove(loja);
                returnedValuesTecnicos.Remove(nome);
                returnedValuesTecnicos.Remove(email);
                returnedValuesTecnicos.Remove(obs);
                returnedValuesTecnicos.Remove(usreid);
                returnedValuesTecnicos.Remove(activo);
                returnedValuesTecnicos.Remove(dataultima);
                returnedValuesTecnicos.Remove(dataregisto);
            }
            Session["returnedValuesTecnicos"] = returnedValuesTecnicos;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListagemReparadores : Telerik.Web.UI.RadAjaxPage
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

        protected void listagemReparadores_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaListaReparadores = from reparador in DC.Parceiros
                                              where reparador.ACTIVO == true && (reparador.ID_TIPO_PARCEIRO == 3 || reparador.ID_TIPO_PARCEIRO == 4)
                                              orderby reparador.DATA_REGISTO descending
                                              select new
                                              {
                                                  NIF = reparador.NIF,
                                                  NOME = reparador.NOME,
                                                  CODIGO = reparador.CODIGO,
                                                  CODPOSTAL = reparador.CODPOSTAL,
                                                  MORADA = reparador.MORADA,
                                                  LOCALIDADE = reparador.LOCALIDADE,
                                                  DATA_ULTIMA_MODIFICACAO = reparador.DATA_ULTIMA_MODIFICACAO,
                                                  DATA_REGISTO = reparador.DATA_REGISTO,
                                                  USERID = reparador.USERID,
                                                  OBSERVACOES = reparador.OBSERVACOES,
                                                  EMAIL = reparador.EMAIL,
                                                  TELEFONE = reparador.TELEFONE,
                                                  CONTA_ACTIVA = reparador.ACTIVO.Value
                                              };

                listagemReparadores.DataSourceID = "";
                listagemReparadores.DataSource = carregaListaReparadores;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected List<string> returnedValuesReparadores = new List<string>();

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (Session["returnedValuesReparadores"] != null)
            {
                returnedValuesReparadores = (List<string>)Session["returnedValuesReparadores"];
            }
            GridDataItem dataItem = (GridDataItem)(sender as CheckBox).NamingContainer;

            string cod = dataItem["CODIGO"].Text;
            string nome = dataItem["NOME"].Text;
            string morada = dataItem["MORADA"].Text;
            string codpostal = dataItem["CODPOSTAL"].Text;
            string localidade = dataItem["LOCALIDADE"].Text;
            string telefone = dataItem["TELEFONE"].Text;
            string email = dataItem["EMAIL"].Text;
            string nif = dataItem["NIF"].Text;
            string obs = dataItem["OBSERVACOES"].Text;

            if (checkBox.Checked)
            {
                returnedValuesReparadores.Add(cod);
                returnedValuesReparadores.Add(nif);
                returnedValuesReparadores.Add(nome);
                returnedValuesReparadores.Add(morada);
                returnedValuesReparadores.Add(codpostal);
                returnedValuesReparadores.Add(localidade);
                returnedValuesReparadores.Add(telefone);
                returnedValuesReparadores.Add(email);
                returnedValuesReparadores.Add(nif);
                returnedValuesReparadores.Add(obs);
            }
            else
            {
                returnedValuesReparadores.Remove(cod);
                returnedValuesReparadores.Remove(nif);
                returnedValuesReparadores.Remove(nome);
                returnedValuesReparadores.Remove(morada);
                returnedValuesReparadores.Remove(codpostal);
                returnedValuesReparadores.Remove(localidade);
                returnedValuesReparadores.Remove(telefone);
                returnedValuesReparadores.Remove(email);
                returnedValuesReparadores.Remove(nif);
                returnedValuesReparadores.Remove(obs);
            }
            Session["returnedValuesReparadores"] = returnedValuesReparadores;
        }
    }
}
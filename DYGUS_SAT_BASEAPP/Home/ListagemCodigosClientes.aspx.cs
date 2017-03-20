using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListagemCodigosClientes : Telerik.Web.UI.RadAjaxPage
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

        protected void listagemClientes_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaListaClientes = from cliente in DC.Parceiros
                                           where cliente.ID_TIPO_PARCEIRO == 1 || cliente.ID_TIPO_PARCEIRO == 2 || cliente.ID_TIPO_PARCEIRO == 4
                                           orderby cliente.DATA_REGISTO descending
                                           select new
                                           {
                                               CODIGO = cliente.CODIGO,
                                               NOME = cliente.NOME,
                                               MORADA = cliente.MORADA,
                                               CODPOSTAL = cliente.CODPOSTAL,
                                               LOCALIDADE = cliente.LOCALIDADE,
                                               CONTACTO = cliente.TELEFONE,
                                               EMAIL = cliente.EMAIL,
                                               NIF = cliente.NIF,
                                               OBSERVACOES = cliente.OBSERVACOES
                                           };

                listagemClientes.DataSourceID = "";
                listagemClientes.DataSource = carregaListaClientes;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected List<string> returnedValuesClientes = new List<string>();

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (Session["returnedValuesClientes"] != null)
            {
                returnedValuesClientes = (List<string>)Session["returnedValuesClientes"];
            }
            GridDataItem dataItem = (GridDataItem)(sender as CheckBox).NamingContainer;

            string cod = dataItem["CODIGO"].Text;
            string nome = dataItem["NOME"].Text;
            string morada = dataItem["MORADA"].Text;
            string codPostal = dataItem["CODPOSTAL"].Text;
            string localidade = dataItem["LOCALIDADE"].Text;
            string contacto = dataItem["CONTACTO"].Text;
            string email = dataItem["EMAIL"].Text;
            string nif = dataItem["NIF"].Text;
            string obs = dataItem["OBSERVACOES"].Text;


            if (checkBox.Checked)
            {
                returnedValuesClientes.Add(cod);
                returnedValuesClientes.Add(nome);
                returnedValuesClientes.Add(morada);
                returnedValuesClientes.Add(codPostal);
                returnedValuesClientes.Add(localidade);
                returnedValuesClientes.Add(contacto);
                returnedValuesClientes.Add(email);
                returnedValuesClientes.Add(nif);
                returnedValuesClientes.Add(obs);
            }
            else
            {
                returnedValuesClientes.Remove(cod);
                returnedValuesClientes.Remove(nome);
                returnedValuesClientes.Remove(morada);
                returnedValuesClientes.Remove(codPostal);
                returnedValuesClientes.Remove(localidade);
                returnedValuesClientes.Remove(contacto);
                returnedValuesClientes.Remove(email);
                returnedValuesClientes.Remove(nif);
                returnedValuesClientes.Remove(obs);
            }
            Session["returnedValuesClientes"] = returnedValuesClientes;
        }
    }
}
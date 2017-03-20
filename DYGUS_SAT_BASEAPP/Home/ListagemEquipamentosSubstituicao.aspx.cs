using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListagemEquipamentosSubstituicao : Telerik.Web.UI.RadAjaxPage
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

        protected void ListagemEquipamentos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carrega = from equip in DC.Equipamentos_Substituicaos
                              where equip.ID_DISPONIBILIDADE == 1
                              select new
                              {
                                  ID = equip.ID,
                                  CODIGO = equip.CODIGO,
                                  MARCA = equip.ID_MARCA.Value.ToString(),
                                  MODELO = equip.ID_MODELO.Value.ToString(),
                                  IMEI = equip.IMEI,
                                  CARREGADOR = equip.CARREGADOR.Value,
                                  CARTAO_MEMORIA = equip.CARTAO_MEMORIA.Value,
                                  BATERIA = equip.BATERIA.Value
                              };

                ListagemEquipamentos.DataSourceID = "";
                ListagemEquipamentos.DataSource = carrega;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }

        }

        protected List<string> returnedValuesEquipamentos = new List<string>();

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (Session["returnedValuesEquipamentos"] != null)
            {
                returnedValuesEquipamentos = (List<string>)Session["returnedValuesEquipamentos"];
            }
            GridDataItem dataItem = (GridDataItem)(sender as CheckBox).NamingContainer;

            string cod = dataItem["CODIGO"].Text;
            string marca = dataItem["MARCA"].Text;
            string modelo = dataItem["MODELO"].Text;
            string imei = dataItem["IMEI"].Text;
            string carregador = dataItem["CARREGADOR"].Text;
            string cartao = dataItem["CARTAO_MEMORIA"].Text;
            string bateria = dataItem["BATERIA"].Text;


            if (checkBox.Checked)
            {
                returnedValuesEquipamentos.Add(cod);
                returnedValuesEquipamentos.Add(marca);
                returnedValuesEquipamentos.Add(modelo);
                returnedValuesEquipamentos.Add(imei);
                returnedValuesEquipamentos.Add(carregador);
                returnedValuesEquipamentos.Add(cartao);
                returnedValuesEquipamentos.Add(bateria);
            }
            else
            {
                returnedValuesEquipamentos.Remove(cod);
                returnedValuesEquipamentos.Remove(marca);
                returnedValuesEquipamentos.Remove(modelo);
                returnedValuesEquipamentos.Remove(imei);
                returnedValuesEquipamentos.Remove(carregador);
                returnedValuesEquipamentos.Remove(cartao);
                returnedValuesEquipamentos.Remove(bateria);
            }
            Session["returnedValuesEquipamentos"] = returnedValuesEquipamentos;
        }
    }
}
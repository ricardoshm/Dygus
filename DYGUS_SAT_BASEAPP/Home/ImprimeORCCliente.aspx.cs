using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ImprimeORCCliente : Telerik.Web.UI.RadAjaxPage
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
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListagemGeralOrcamentoCliente.aspx", true);
                return;
            }

            var procuraOR = from ordem in DC.Orcamentos
                            where ordem.ID == Convert.ToInt32(id)
                            select new
                            {
                                cod = ordem.CODIGO,
                                data = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                valor = ordem.VALOR_ORCAMENTO.Value,
                            };

            foreach (var item in procuraOR)
            {
                numOr.InnerHtml = item.cod;
                codOR.Text += "<h1>ORÇAMENTO  " + item.cod + "</h1>";
                CodigoOr.Text += "<td><span style='font-weight:600;'>" + item.cod + "</span></td>";
                dataRegisto.Text += "<td><span style='font-weight:600;'>" + item.data + "</span></td>";
                valorprevisto.Text += "<td><span style='font-weight:600;'>" + item.valor + "&nbsp;€</span></td>";
            }


            var carregaDadosCliente = from ordem in DC.Orcamentos
                                      join cliente in DC.Parceiros on ordem.USERID equals cliente.USERID
                                      where ordem.ID == Convert.ToInt32(id)
                                      select new
                                      {
                                          nome = cliente.NOME,
                                          morada = cliente.MORADA,
                                          codpostal = cliente.CODPOSTAL,
                                          localidade = cliente.LOCALIDADE,
                                          telefone = cliente.TELEFONE,
                                          email = cliente.EMAIL,
                                          nif = cliente.NIF,
                                          cod = cliente.CODIGO
                                      };

            foreach (var item in carregaDadosCliente)
            {
                nomeClienteHeader.Text += item.nome;
                moradaCliente.Text += item.morada;
                codpostallocalidade.Text += item.codpostal + "&nbsp;" + item.localidade;
                telefonecliente.Text += item.telefone;
                if (item.email != "dygussat@dygus.com")
                    emailcliente.Text += item.email;
                else
                    emailcliente.Text += "N/D";
                if (item.nif != 999999999)
                    nifcliente.Text += item.nif;
                else
                    nifcliente.Text += "N/D";
                nomeCliente.Text += "<td><span>" + item.nome + "</span></td>";
                contactocliente.Text += "<td><span>" + telefonecliente.Text + "</span></td>";
                datahoje.Text = "<td><span>" + DateTime.Today.ToShortDateString().ToString() + "</span></td>";
                assinatura.Text = "<td><span></span></td>";
            }

            var carregaLoja = from ordem in DC.Orcamentos
                              join lojas in DC.Lojas on ordem.ID_LOJA equals lojas.ID
                              where ordem.ID == Convert.ToInt32(id)
                              select new
                              {
                                  nome = lojas.NOME,
                                  morada = lojas.MORADA,
                                  codpostal = lojas.CODPOSTAL,
                                  localidade = lojas.LOCALIDADE,
                                  telefone = lojas.TELEFONE,
                                  nif = lojas.NIF,
                                  data = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                  logo = lojas.URL_FOTO.ToString()
                              };

            foreach (var item in carregaLoja)
            {
                nomeLoja.Text += item.nome;
                moradaLoja.Text += item.morada;
                localidadecodpostalloja.Text += item.codpostal + "&nbsp;" + item.localidade;
                telefoneloja.Text += item.telefone;
                logotipoLoja.Text += "<img alt='' src='" + item.logo + "'><input type='file' accept='image/*'>";

            }

            var equip = from ordem in DC.Orcamentos
                        join equips in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equips.ID
                        where ordem.ID == Convert.ToInt32(id)
                        select new
                        {
                            marca = equips.Marca.DESCRICAO,
                            modelo = equips.Modelo.DESCRICAO,
                            descricao = ordem.DESCRICAO_DETALHADA_AVARIA,
                            imei = equips.IMEI,
                            obs = equips.OBSERVACOES,
                        };

            foreach (var item in equip)
            {
                equipAvariado.Text += "<td><span style='font-weight:600;'>" + item.marca + "&nbsp;" + item.modelo + "</span></td>";
                imeiequipAvariado.Text += "<td><span style='font-weight:600;'>" + item.imei + "</span></td>";
                descricaoavaria.Text += "<td><span><p style='text-align:left;'>" + item.descricao + "</p></span></td>";
                descricaoAvariaEquipamento.Text += "<td><span><p style='text-align:left;font-size:x-small;'>" + item.descricao + "</p></span></td>";
                obsOrcamento.Text += "<td><span>" + item.obs + "</span></td>";
            }


            var condicoes = from cond in DC.Condicoes_Gerais
                            select cond;

            foreach (var item in condicoes)
            {
                condgerais.Text += "<p style='font-size:x-small;text-align:justify;'>" + item.ID + ".&nbsp;" + item.DESCRICAO + "</p>";
            }





        }
    }
}
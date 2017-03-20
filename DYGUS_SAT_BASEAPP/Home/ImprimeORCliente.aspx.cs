using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ImprimeORCliente : Telerik.Web.UI.RadAjaxPage
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
                Response.Redirect("ListagemGeralOrdensReparacao.aspx", true);
                return;
            }

            var procuraOR = from ordem in DC.Ordem_Reparacaos
                            where ordem.ID == Convert.ToInt32(id)
                            select new
                            {
                                cod = ordem.CODIGO,
                                data = ordem.DATA_REGISTO.Value.ToShortDateString(),
                                dataentrega = ordem.DATA_PREVISTA_CONCLUSAO.Value.ToShortDateString(),
                                sub = ordem.Equipamentos_Substituicao.CODIGO,
                                valor = ordem.VALOR_PREVISTO_REPARACAO.Value,
                                garantiaequipavariado = ordem.Equipamento_Avariado_GarantiaTipo.Equipamento_Avariado_Garantia.DESCRICAO
                            };

            foreach (var item in procuraOR)
            {
                numOr.InnerHtml = item.cod;
                codOR.Text += "<h1>ORDEM DE REPARAÇÃO  " + item.cod + "</h1>";
                CodigoOr.Text += "<td><span style='font-weight:600;'>" + item.cod + "</span></td>";
                dataRegisto.Text += "<td><span style='font-weight:600;'>" + item.data + "</span></td>";
                dataPrevistaEntrega.Text += "<td><span style='font-weight:600;'>" + item.dataentrega + "</span></td>";
                if (item.sub != null)
                    equipSubst.Text += "<td><span style='font-weight:600;'>" + item.sub + "</span></td>";
                else
                    equipSubst.Text += "<td><span style='font-weight:600;'>N/D</span></td>";
                if (item.valor == 0)
                    valorprevisto.Text += "<td><span style='font-weight:600;'>N/D</span></td>";
                else
                    valorprevisto.Text += "<td><span style='font-weight:600;'>" + item.valor + "&nbsp;€</span></td>";
                if (item.garantiaequipavariado != null)
                    garantiaequipavariado.Text += "<td><span style='font-weight:600;'>" + item.garantiaequipavariado + "</span></td>";
                else
                    garantiaequipavariado.Text += "<td><span style='font-weight:600;'>N/D</span></td>";
            }

            //var carregaLogo = from ordem in DC.Ordem_Reparacaos
            //                  join lojas in DC.Lojas on ordem.ID_LOJA equals lojas.ID
            //                  where ordem.ID == Convert.ToInt32(id)
            //                  select new
            //                  {
            //                      logo = lojas.URL_FOTO.ToString()
            //                  };

            //foreach (var item in carregaLogo)
            //{
            //    logoLoja.Text += "<span><img alt='' src=" + item.logo + "><input type='file' accept='image/*'></span>";
            //}

            var carregaDadosCliente = from ordem in DC.Ordem_Reparacaos
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

            var carregaLoja = from ordem in DC.Ordem_Reparacaos
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

            var equip = from ordem in DC.Ordem_Reparacaos
                        join equips in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equips.ID
                        join marcas in DC.Marcas on equips.ID_MARCA equals marcas.ID
                        join modelos in DC.Modelos on equips.ID_MODELO equals modelos.ID
                        where ordem.ID == Convert.ToInt32(id)
                        select new
                        {
                            marca = marcas.DESCRICAO,
                            modelo = modelos.DESCRICAO,
                            descricao = ordem.DESCRICAO_DETALHADA_PROBLEMA,
                            imei = equips.IMEI,
                            obs = equips.OBSERVACOES,
                        };

            foreach (var item in equip)
            {
                equipAvariado.Text += "<td><span style='font-weight:600;'>" + item.marca + "&nbsp;" + item.modelo + "</span></td>";
                imeiequipAvariado.Text += "<td><span style='font-weight:600;'>" + item.imei + "</span></td>";
                descricaoavaria.Text += "<td><span><p style='text-align:left;'>" + item.descricao + "</p></span></td>";
                descricaoAvariaEquipamento.Text += "<td><span><p style='font-size:x-small;text-align:left;'>" + item.descricao + "</p></span></td>";
                obsOrdensReparacao.Text += "<td><span>" + item.obs + "</span></td>";
            }


            var acessorios = from ordem in DC.Ordem_Reparacaos
                             join equips in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equips.ID
                             where ordem.ID == Convert.ToInt32(id)
                             select new
                             {
                                 bateria = ordem.Equipamento_Avariado_Acessorio.BATERIA.Value,
                                 carr = ordem.Equipamento_Avariado_Acessorio.CARREGADOR.Value,
                                 sim = ordem.Equipamento_Avariado_Acessorio.CARTAO_SIM.Value,
                                 bolsa = ordem.Equipamento_Avariado_Acessorio.BOLSA.Value,
                                 cartaom = ordem.Equipamento_Avariado_Acessorio.CARTAO_MEM.Value,
                                 caixa = ordem.Equipamento_Avariado_Acessorio.CAIXA.Value,
                                 outros = ordem.Equipamento_Avariado_Acessorio.OUTROS,
                                 codseg = ordem.Equipamento_Avariado.COD_SEGURANCA
                             };

            foreach (var item in acessorios)
            {
                if (item.bateria == true)
                    bateria.Text += "<td><span style='text-align:center;'>S</span></td>";
                else
                    bateria.Text += "<td><span>N</span></td>";

                if (item.carr == true)
                    carregador.Text += "<td><span>S</span></td>";
                else
                    carregador.Text += "<td><span>N</span></td>";

                if (item.sim == true)
                    cartaosim.Text += "<td><span>S</span></td>";
                else
                    cartaosim.Text += "<td><span>N</span></td>";

                if (item.bolsa == true)
                    bolsa.Text += "<td><span>S</span></td>";
                else
                    bolsa.Text += "<td><span>N</span></td>";

                if (item.cartaom == true)
                    cartaom.Text += "<td><span>S</span></td>";
                else
                    cartaom.Text += "<td><span>N</span></td>";
                if (item.caixa == true)
                    caixa.Text += "<td><span>S</span></td>";
                else
                    caixa.Text += "<td><span>N</span></td>";
                if (!String.IsNullOrEmpty(item.codseg))
                    codseg.Text += "<td><span>" + item.codseg.ToString() + "</span></td>";
                else
                    codseg.Text += "<td><span>N/A</span></td>";
                outros.Text += "<td><span>" + item.outros + "</span></td>";
            }

            var condicoes = from cond in DC.Condicoes_Gerais
                            select cond;

            foreach (var item in condicoes)
            {
                condgerais.Text += "<p style='font-size:xx-small;text-align:justify;'>" + item.ID + ".&nbsp;" + item.DESCRICAO + "</p>";
            }





        }
    }
}


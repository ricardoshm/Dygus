using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ImprimeORFinalCliente : Telerik.Web.UI.RadAjaxPage
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

            tabelaArtigos.Visible = false;
            tabelamo.Visible = false;

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

            try
            {

            
            var carregaHeader = from ordem in DC.Ordem_Reparacaos
                                where ordem.ID == Convert.ToInt32(id)
                                select ordem;

            foreach (var item in carregaHeader)
            {
                codOR.Text += "ORDEM DE REPARAÇÃO  " + item.CODIGO;
                nomeLoja.Text += item.Loja.NOME;
                moradaLoja.Text += item.Loja.MORADA;
                localidadecodpostalloja.Text += item.Loja.CODPOSTAL + "&nbsp;" + item.Loja.LOCALIDADE;
                telefoneloja.Text += item.Loja.TELEFONE;
                logotipoLoja.Text += "<img alt='' src='" + item.Loja.URL_FOTO + "'><input type='file' accept='image/*'>";
                codOr2.Text += "<td><span style='font-weight:600;'>" + item.CODIGO + "</span></td>";
                dataRegistoOR.Text += "<td><span style='font-weight:600;'>" + item.DATA_REGISTO.Value.ToShortDateString().ToString() + "</span></td>";
                dataLevantamentoEquipamento.Text += "<td><span style='font-weight:600;'>" + item.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value.ToShortDateString().ToString() + "</span></td>";
                valorfinal.Text += "<td><span style='font-weight:600;'>" + item.VALOR_FINAL.Value + "&nbsp;€</span></td>";
                valorfinal2.Text += "<td><span style='font-weight:600;'>" + item.VALOR_FINAL.Value + "&nbsp;€</span></td>";
            }

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
                nomeCliente.Text += item.nome;
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
                            obs = equips.OBSERVACOES
                        };

            foreach (var item in equip)
            {
                equipAvariado.Text += "<td><span style='font-weight:600;'>" + item.marca + "&nbsp;" + item.modelo + "</span></td>";
                imeiequipAvariado.Text += "<td><span style='font-weight:600;'>" + item.imei + "</span></td>";
                descricaoavaria.Text += "<td><span>" + item.descricao + "</span></td>";
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
                                 novoimei = ordem.Equipamento_Avariado.NOVO_IMEI
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

                outros.Text += "<td><span>" + item.outros + "</span></td>";

                novoimei.Text += "<td><span>" + item.novoimei + "</span></td>";
            }


            var valorFinal = from ordem in DC.Ordem_Reparacaos
                             where ordem.ID == Convert.ToInt32(id)
                             select ordem;

            foreach (var item in valorFinal)
            {
                //valorfinal.Text += item.VALOR_FINAL.Value + "&nbsp;€";
            }

            int maoobra = 0;
            int artigos = 0;

            var verificaMaoobra = from mo in DC.Ordem_Reparacao_MaoObras
                                  where mo.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                  select mo;

            maoobra = Enumerable.Count(verificaMaoobra);

            if (maoobra > 0)
            {
                tabelamo.Visible = true;
                var verificaMaoobrax = from mo in DC.Ordem_Reparacao_MaoObras
                                       where mo.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                       select mo;

                foreach (var item in verificaMaoobrax)
                {
                    descricaotrabalhosrealizados.Text +=
                        "<tr>" +
                        "<td><span>" + item.DESCRICAO.ToString() + "</span></td>" +
                        "</tr>";
                }
            }

            var verificaArtigos = from art in DC.Ordem_Reparacao_Artigos
                                  where art.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                  select art;

            artigos = Enumerable.Count(verificaArtigos);

            if (artigos > 0)
            {
                tabelaArtigos.Visible = true;
                var verificaArtigosx = from art in DC.Ordem_Reparacao_Artigos
                                       join artigo in DC.Artigos on art.ID_ARTIGO equals artigo.ID
                                       where art.ID_ORDEM_REPARACAO == Convert.ToInt32(id)
                                       select new
                                       {
                                           descricao = artigo.DESCRICAO,
                                           qtd = art.QTD_ARTIGO.Value
                                       };

                foreach (var item in verificaArtigosx)
                {

                    descricaoartigos.Text += "<tr>" +
                        "<td><span>" + item.qtd + "</span></td>" +
                        "<td><span>" + item.descricao + "</span></td>" +
                        "</tr>";
                }
            }

            //var procuraOR = from ordem in DC.Ordem_Reparacaos
            //                where ordem.ID == Convert.ToInt32(id)
            //                select new
            //                {
            //                    cod = ordem.CODIGO,
            //                    data = ordem.DATA_REGISTO.Value.ToShortDateString(),
            //                    datalevantamento = ordem.DATA_LEVANTAMENTO_EQUIPAMENTO_AVARIADO.Value.ToShortDateString(),
            //                };

            //foreach (var item in procuraOR)
            //{
            //    codOR.Text += "<h1>ORDEM DE REPARAÇÃO #" + item.cod + "</h1>";
            //    CodigoOr.Text += "<td><span style='font-weight:600;'>" + item.cod + "</span></td>";
            //    dataRegisto.Text += "<td><span style='font-weight:600;'>" + item.data + "</span></td>";
            //    datalevantamento.Text += "<td><span style='font-weight:600;'>" + item.datalevantamento + "</span></td>";

            //}

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

            //var carregaDadosCliente = from ordem in DC.Ordem_Reparacaos
            //                          join clientes in DC.Parceiros on ordem.USERID equals clientes.USERID
            //                          where ordem.ID == Convert.ToInt32(id)
            //                          select new
            //                          {
            //                              nome = clientes.NOME,
            //                              morada = clientes.MORADA,
            //                              codpostal = clientes.CODPOSTAL,
            //                              localidade = clientes.LOCALIDADE,
            //                              telefone = clientes.TELEFONE,
            //                              email = clientes.EMAIL,
            //                              nif = clientes.NIF,
            //                              cod = clientes.CODIGO
            //                          };

            //foreach (var item in carregaDadosCliente)
            //{
            //    dadosCliente.Text += "<address>" +
            //        "<p style='font-weight:600;'>" + item.cod + "</p>" +
            //        "<p style='font-weight:600;'>" + item.nome + "</p>" +
            //        "<p style='font-weight:600;'>" + item.morada + "&nbsp;" + item.codpostal + "&nbsp;" + item.localidade + "</p>" +
            //        "<p style='font-weight:600;'>" + item.telefone + "</p>" +
            //        "<p style='font-weight:600;'>" + item.email + "</p><br/>" +
            //        "<p style='font-weight:600;'>NIF: " + item.nif + "</p>" +
            //        "</address>";
            //}

            //var carregaLoja = from ordem in DC.Ordem_Reparacaos
            //                  join lojas in DC.Lojas on ordem.ID_LOJA equals lojas.ID
            //                  where ordem.ID == Convert.ToInt32(id)
            //                  select new
            //                  {
            //                      nome = lojas.NOME,
            //                      morada = lojas.MORADA,
            //                      codpostal = lojas.CODPOSTAL,
            //                      localidade = lojas.LOCALIDADE,
            //                      telefone = lojas.TELEFONE,
            //                      nif = lojas.NIF,
            //                      data = ordem.DATA_REGISTO.Value.ToShortDateString()
            //                  };

            //foreach (var item in carregaLoja)
            //{
            //    dadosLoja.Text += "<address>" +
            //    "<p>" + item.nome + "<br/>" + item.morada + "&nbsp;" + item.codpostal + "&nbsp;" + item.localidade + "<br>" + item.telefone + "<br/><br/>NIF: " + item.nif + "</p>" +
            //    "</address>";
            //}

            //var descricaoTrab = from ordem in DC.Ordem_Reparacaos
            //                    join artigo in DC.Ordem_Reparacao_Artigos on ordem.ID equals artigo.ID_ORDEM_REPARACAO
            //                    where ordem.ID == Convert.ToInt32(id)
            //                    select new
            //                    {
            //                        qtd = artigo.QTD_ARTIGO.Value,
            //                        desc = artigo.Artigo.DESCRICAO
            //                    };

            //foreach (var item in descricaoTrab)
            //{
            //    qtd.Text += "<td><span>" + item.qtd + "</span></td>";
            //    descricao.Text += "<td><span>" + item.desc + "</span></td>";
            //}

            //var descricaoMO = from ordem in DC.Ordem_Reparacaos
            //                  join artigo in DC.Ordem_Reparacao_MaoObras on ordem.ID equals artigo.ID_ORDEM_REPARACAO
            //                  where ordem.ID == Convert.ToInt32(id)
            //                  select new
            //                  {
            //                      desc = artigo.DESCRICAO
            //                  };

            //foreach (var item in descricaoMO)
            //{
            //    qtdmo.Text += "<span>1</span>";
            //    descricaomo.Text += "<span>" + item.desc + "</span>";
            //}

            //var valor = from v in DC.Ordem_Reparacaos
            //            where v.ID == Convert.ToInt32(id)
            //            select v;

            //foreach (var item in valor)
            //{
            //    valorfinal.Text += "<td><span>" + item.VALOR_FINAL.Value + "&nbsp;€</span></td>";
            //}

            //var equip = from ordem in DC.Ordem_Reparacaos
            //            join equips in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equips.ID
            //            join marcas in DC.Marcas on equips.ID_MARCA equals marcas.ID
            //            join modelos in DC.Modelos on equips.ID_MODELO equals modelos.ID
            //            where ordem.ID == Convert.ToInt32(id)
            //            select new
            //            {
            //                marca = marcas.DESCRICAO,
            //                modelo = modelos.DESCRICAO,
            //                descricao = ordem.DESCRICAO_DETALHADA_PROBLEMA,
            //                imei = equips.IMEI,
            //                novoimei = equips.NOVO_IMEI
            //            };

            //foreach (var item in equip)
            //{
            //    equipAvariado.Text += "<td><span>" + item.marca + "&nbsp;" + item.modelo + "</span></td>";
            //    imeiequipAvariado.Text += "<td><span>" + item.imei + "</span></td>";
            //    descricaoEquipAvariadoAvaria.Text += "<td><span>" + item.descricao + "</span></td>";
            //    qtdEquip.Text += "<td><span>1</span></td>";
            //    if (item.novoimei != null)
            //        novoimei.Text += "<td><span>" + item.novoimei + "</span></td>";
            //    else
            //        novoimei.Text += "<td><span>N/A</span></td>";
            //}


            //var acessorios = from ordem in DC.Ordem_Reparacaos
            //                 join equips in DC.Equipamento_Avariados on ordem.ID_EQUIPAMENTO_AVARIADO equals equips.ID
            //                 where ordem.ID == Convert.ToInt32(id)
            //                 select new
            //                 {
            //                     bateria = ordem.Equipamento_Avariado_Acessorio.BATERIA.Value,
            //                     carr = ordem.Equipamento_Avariado_Acessorio.CARREGADOR.Value,
            //                     sim = ordem.Equipamento_Avariado_Acessorio.CARTAO_SIM.Value,
            //                     bolsa = ordem.Equipamento_Avariado_Acessorio.BOLSA.Value,
            //                     cartaom = ordem.Equipamento_Avariado_Acessorio.CARTAO_MEM.Value,
            //                     caixa = ordem.Equipamento_Avariado_Acessorio.CAIXA.Value,
            //                     outros = ordem.Equipamento_Avariado_Acessorio.OUTROS
            //                 };

            //foreach (var item in acessorios)
            //{
            //    if (item.bateria == true)
            //        bateria.Text += "<td><span>S</span></td>";
            //    else
            //        bateria.Text += "<td><span>N</span></td>";

            //    if (item.carr == true)
            //        carregador.Text += "<td><span>S</span></td>";
            //    else
            //        carregador.Text += "<td><span>N</span></td>";

            //    if (item.sim == true)
            //        cartaosim.Text += "<td><span>S</span></td>";
            //    else
            //        cartaosim.Text += "<td><span>N</span></td>";

            //    if (item.bolsa == true)
            //        bolsa.Text += "<td><span>S</span></td>";
            //    else
            //        bolsa.Text += "<td><span>N</span></td>";

            //    if (item.cartaom == true)
            //        cartaom.Text += "<td><span>S</span></td>";
            //    else
            //        cartaom.Text += "<td><span>N</span></td>";

            //    if (item.caixa == true)
            //        caixa.Text += "<td><span>S</span></td>";
            //    else
            //        caixa.Text += "<td><span>N</span></td>";

            //    outros.Text += "<td><span>" + item.outros + "</span></td>";
            //}
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }
    }
}
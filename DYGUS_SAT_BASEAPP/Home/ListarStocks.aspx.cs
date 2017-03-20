﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ListarStocks : Telerik.Web.UI.RadAjaxPage
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

            //(Page.Master.FindControl("stocks") as HtmlControl).Attributes.Add("class", "active");
            //(Page.Master.FindControl("stocksprodutoedit") as HtmlControl).Attributes.Add("class", "active");
        }

        protected void listagemartigos_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var consultaArtigos = from artigo in DC.Artigos
                                      join stock in DC.Artigo_Stocks on artigo.ID equals stock.ID_ARTIGO
                                      join armarzem in DC.Armazems on artigo.ID_ARMAZEM equals armarzem.ID
                                      select new
                                      {
                                          ID = artigo.ID,
                                          CODIGO = artigo.CODIGO,
                                          DESCRICAO = artigo.DESCRICAO,
                                          VALOR_CUSTO = stock.VALOR_CUSTO,
                                          VALOR_REVENDA = stock.VALOR_REVENDA,
                                          VALOR_VENDA = stock.VALOR_VENDA,
                                          ARMAZEM = armarzem.DESCRICAO,
                                          QTD = stock.QTD_DISPONIVEL
                                      };

                listagemartigos.DataSourceID = "";
                listagemartigos.DataSource = consultaArtigos;
            }
            catch (Exception ex)
            {

                ErrorLog.WriteError(ex.Message);Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void listagemartigos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                string val1 = item["ID"].Text;
                HyperLink hLink = (HyperLink)item["EDITARARTIGO"].Controls[0];
                hLink.NavigateUrl = "EditarStocks.aspx?id=" + val1;
            }
        }
    }
}
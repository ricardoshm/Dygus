using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class ErrorPage : Telerik.Web.UI.RadAjaxPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["erro"] != null)
                mensagemErro.InnerHtml += Request.QueryString["erro"].ToString();
            else
                mensagemErro.InnerHtml = "";
        }
    }
}
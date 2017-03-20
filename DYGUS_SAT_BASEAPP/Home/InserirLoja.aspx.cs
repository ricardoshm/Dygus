using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace DYGUS_SAT_BASEAPP.Home
{
    public partial class InserirLoja : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();

        private List<Telerik.Web.UI.UploadedFileInfo> uploadedFiles = new List<Telerik.Web.UI.UploadedFileInfo>();
        public List<Telerik.Web.UI.UploadedFileInfo> UploadedFiles
        {
            get
            {
                return uploadedFiles;
            }
            set
            {
                uploadedFiles = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // (Page.Master.FindControl("config") as HtmlControl).Attributes.Add("class", "has-sub active");
            //(Page.Master.FindControl("configinsert") as HtmlControl).Attributes.Add("class", "active");

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

                if (regra != "Administrador" && regra != "SuperAdmin")
                    Response.Redirect("Default.aspx", false);
            }
            else
                Response.Redirect("~/Default.aspx", true);

            if (!Page.IsPostBack)
            {
                int codNovaLoja = 0;

                try
                {
                    var config = from conf in DC.Configuracaos
                                 where conf.INICIAL == "L"
                                 select conf;

                    LINQ_DB.Configuracao configuracao = new LINQ_DB.Configuracao();

                    configuracao = config.First();
                    codNovaLoja = Convert.ToInt32(configuracao.CODIGO) + 1;

                    string numeroLoja = "";

                    string prefixo = configuracao.INICIAL.ToString() + "-0000";

                    var confg = from confii in DC.Configuracaos
                                select confii;

                    foreach (var configItem in confg)
                    {
                        numeroLoja = prefixo + codNovaLoja.ToString();
                        tbcodloja.Text = numeroLoja;
                    }
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
        }

        protected void listagemlojasregistadas_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                var carregaLojas = from loja in DC.Lojas
                                   where loja.ACTIVO == true
                                   orderby loja.ID ascending
                                   select new
                                   {
                                       ID = loja.ID,
                                       CODIGO = loja.CODIGO,
                                       NOME = loja.NOME,
                                       LOCALIDADE = loja.LOCALIDADE,
                                       NIF = loja.NIF,
                                       CONTACTO_TEL = loja.TELEFONE,
                                       FOTO = loja.URL_FOTO
                                   };

                listagemlojasregistadas.DataSourceID = "";
                listagemlojasregistadas.DataSource = carregaLojas;
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }



        protected void btnGrava_Click(object sender, EventArgs e)
        {

            try
            {
                LINQ_DB.Loja NOVALOJA = new LINQ_DB.Loja();
                NOVALOJA.CODIGO = tbcodloja.Text;
                NOVALOJA.NOME = tbnome.Text;
                NOVALOJA.MORADA = tbmorada.Text;
                NOVALOJA.CODPOSTAL = tbcodpostal.Text;
                NOVALOJA.LOCALIDADE = tblocalidade.Text;
                NOVALOJA.TELEFONE = tbcontactotel.Text;
                if (tbcontactofax.Text != null)
                    NOVALOJA.FAX = tbcontactofax.Text;
                else
                    NOVALOJA.FAX = "N/D";
                NOVALOJA.NIF = Convert.ToInt32(tbnif.Text);
                NOVALOJA.ACTIVO = true;

                string pastaLogos = "";

                if (Directory.Exists(Server.MapPath("~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString())))
                    pastaLogos = Server.MapPath("~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString());
                else
                {
                    string thisDir = "";

                    thisDir = Server.MapPath("~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString());
                    System.IO.Directory.CreateDirectory(thisDir);
                    pastaLogos = Server.MapPath("~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString());
                }

                foreach (UploadedFile file in uploadLogo.UploadedFiles)
                {
                    //Get the temp file name
                    string tempFileName = System.IO.Path.Combine(Server.MapPath(uploadLogo.TemporaryFolder), file.FileName);

                    //Create the final file name based on the original file name
                    string finalFileName = System.IO.Path.Combine(pastaLogos, file.FileName);

                    if (File.Exists(finalFileName))
                    {
                        string fileTodelete;
                        fileTodelete = Server.MapPath("~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString() + "\\" + file.FileName);
                        File.Delete(fileTodelete);

                        Bitmap OriginalBM = new System.Drawing.Bitmap(tempFileName);

                        double sngRatio = Convert.ToDouble(OriginalBM.Width) / Convert.ToDouble(OriginalBM.Height);
                        double newWidth = 208;
                        double newHeight = newWidth / sngRatio;

                        if (newHeight > 61)
                        {
                            sngRatio = Convert.ToDouble(OriginalBM.Height) / Convert.ToDouble(OriginalBM.Width);
                            newHeight = 61;
                            newWidth = newHeight / sngRatio;
                        }

                        Size newSize = new Size(Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
                        Bitmap ResizedBM = new Bitmap(OriginalBM, newSize);

                        sngRatio = Convert.ToDouble(OriginalBM.Width) / Convert.ToDouble(OriginalBM.Height);

                        ResizedBM.Save(Server.MapPath(@"~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString() + "\\" + file.FileName));
                        ResizedBM.Save(Server.MapPath(@"~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString() + "\\" + file.FileName.Replace(" ", "")));
                        string clean = Regex.Replace(file.FileName, @"[^\x20-\x7e]", "a");
                        string resultado = Regex.Replace(clean, @"\s", "");

                        NOVALOJA.URL_FOTO = "../onoff/LogosLojas/" + NOVALOJA.CODIGO.ToString() + "/" + resultado;
                    }
                    else
                    {
                        Bitmap OriginalBM = new System.Drawing.Bitmap(tempFileName);

                        double sngRatio = Convert.ToDouble(OriginalBM.Width) / Convert.ToDouble(OriginalBM.Height);
                        double newWidth = 208;
                        double newHeight = newWidth / sngRatio;

                        if (newHeight > 61)
                        {
                            sngRatio = Convert.ToDouble(OriginalBM.Height) / Convert.ToDouble(OriginalBM.Width);
                            newHeight = 61;
                            newWidth = newHeight / sngRatio;
                        }

                        Size newSize = new Size(Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
                        Bitmap ResizedBM = new Bitmap(OriginalBM, newSize);

                        sngRatio = Convert.ToDouble(OriginalBM.Width) / Convert.ToDouble(OriginalBM.Height);

                        ResizedBM.Save(Server.MapPath(@"~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString() + "\\" + file.FileName));
                        ResizedBM.Save(Server.MapPath(@"~\\onoff\\LogosLojas\\" + NOVALOJA.CODIGO.ToString() + "\\" + file.FileName.Replace(" ", "")));
                        string clean = Regex.Replace(file.FileName, @"[^\x20-\x7e]", "a");
                        string resultado = Regex.Replace(clean, @"\s", "");

                        NOVALOJA.URL_FOTO = "../onoff/LogosLojas/" + NOVALOJA.CODIGO.ToString() + "/" + resultado;
                    }
                }
                if (!String.IsNullOrEmpty(tbobsloja.Text))
                    NOVALOJA.OBSERVACOES = tbobsloja.Text;
                else
                    NOVALOJA.OBSERVACOES = "N/D";
                DC.Lojas.InsertOnSubmit(NOVALOJA);
                DC.SubmitChanges();
            
                LINQ_DB.Configuracao NOVOCODIGOLOJA = new LINQ_DB.Configuracao();
                var config = from conf in DC.Configuracaos
                             where conf.INICIAL == "L"
                             select conf;
                NOVOCODIGOLOJA = config.First();
                NOVOCODIGOLOJA.CODIGO = NOVOCODIGOLOJA.CODIGO + 1;
                DC.SubmitChanges();

                listagemlojasregistadas.Rebind();
                recalculaCodCliente();

                sucesso.Style.Add("display", "block");
                sucessoMessage.Style.Add("display", "block");
                sucessoMessage.InnerHtml = "Loja " + NOVALOJA.CODIGO + " registada com êxito!";
                erro.Style.Add("display", "none");
                errorMessage.Style.Add("display", "none");
                errorMessage.InnerHtml = "";
                limpaCampos();

            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }


        }

        protected void recalculaCodCliente()
        {
            int codNovoCliente = 0;

            try
            {
                var config = from conf in DC.Configuracaos
                             where conf.INICIAL == "L"
                             select conf;

                LINQ_DB.Configuracao configuracao = new LINQ_DB.Configuracao();

                configuracao = config.First();
                codNovoCliente = Convert.ToInt32(configuracao.CODIGO) + 1;

                string numeroCliente = "";

                string prefixo = configuracao.INICIAL.ToString() + "-0000";

                var confg = from confii in DC.Configuracaos
                            select confii;

                foreach (var item in confg)
                {
                    numeroCliente = prefixo + codNovoCliente.ToString();
                    tbcodloja.Text = numeroCliente;
                }
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected void limpaCampos()
        {
            tbnome.Text = "";
            tbmorada.Text = "";
            tbcodpostal.Text = "";
            tblocalidade.Text = "";
            tbcontactotel.Text = "";
            tbcontactofax.Text = "";
            tbnif.Text = "";
            tbobsloja.Text = "";
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            tbnome.Text = "";
            tbmorada.Text = "";
            tbcodpostal.Text = "";
            tblocalidade.Text = "";
            tbcontactotel.Text = "";
            tbcontactofax.Text = "";
            tbnif.Text = "";
            tbobsloja.Text = "";
        }


    }
}
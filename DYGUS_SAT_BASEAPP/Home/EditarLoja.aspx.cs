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
    public partial class EditarLoja : Telerik.Web.UI.RadAjaxPage
    {
        LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
        Guid userid = new Guid();
        Guid Role = new Guid();
        string UserName = "";
        protected void Page_Load(object sender, EventArgs e)
        {



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

            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];

                var consultalojas = from f in DC.Lojas
                                    where f.ID == Convert.ToInt32(id)
                                    select f;

                foreach (var item in consultalojas)
                {
                    if (item.ACTIVO == false)
                    {
                        btnReativarLoja.Visible = true;
                        setModoRead(true);
                    }
                    else
                    {
                        btnReativarLoja.Visible = false;
                        setModoRead(false);
                    }
                }

                if (!Page.IsPostBack)
                    carregaLoja();
            }
            else
            {
                Response.Redirect("ListarLoja.aspx", true);
                return;
            }

        }

        protected void carregaLoja()
        {
            try
            {

                string id = "";

                if (Request.QueryString["ID"] != null)
                {
                    id = Request.QueryString["ID"];
                }
                else
                {
                    Response.Redirect("ListarLoja.aspx", true);
                    return;
                }

                var consultalojas = from f in DC.Lojas
                                    where f.ID == Convert.ToInt32(id)
                                    select f;

                foreach (var item in consultalojas)
                {
                    tbcodloja.Text = item.CODIGO;
                    tbnome.Text = item.NOME;
                    tbmorada.Text = item.MORADA;
                    tbcodpostal.Text = item.CODPOSTAL;
                    tblocalidade.Text = item.LOCALIDADE;
                    tbcontactotel.Text = item.TELEFONE;
                    tbcontactofax.Text = item.FAX;
                    tbnif.Text = item.NIF.ToString();
                    tbobsloja.Text = item.OBSERVACOES;
                    if (!String.IsNullOrEmpty(item.URL_FOTO.ToString()))
                        logo.ImageUrl = item.URL_FOTO.ToString();
                }
            }

            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message); Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
            }
        }

        protected bool validaCamposLoja()
        {
            string nome = "";
            string morada = "";
            string codpostal = "";
            string localidade = "";
            string telefone = "";
            string nif = "";

            nome = tbnome.Text;
            morada = tbmorada.Text;
            codpostal = tbcodpostal.Text;
            localidade = tblocalidade.Text;
            telefone = tbcontactotel.Text;
            nif = tbnif.Text;

            if (String.IsNullOrEmpty(nome))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo Nome da Loja é de preenchimento obrigatório!";
                return false;
            }


            if (String.IsNullOrEmpty(morada))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo Morada da Loja é de preenchimento obrigatório!";
                return false;
            }


            if (String.IsNullOrEmpty(codpostal))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo Código Postal da Loja é de preenchimento obrigatório!";
                return false;
            }


            if (String.IsNullOrEmpty(localidade))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo Localidade da Loja é de preenchimento obrigatório!";
                return false;
            }


            if (String.IsNullOrEmpty(telefone))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo Telefone da Loja é de preenchimento obrigatório!";
                return false;
            }

            if (String.IsNullOrEmpty(nif))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo NIF da Loja é de preenchimento obrigatório!";
                return false;
            }

            Regex numeric = new Regex(@"^[0-9]+$");

            if (!String.IsNullOrEmpty(telefone) && !numeric.IsMatch(telefone))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo Telefone da Loja deverá ser numérico!";
                return false;
            }

            if (!String.IsNullOrEmpty(tbcontactofax.Text) && !numeric.IsMatch(tbcontactofax.Text))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo Fax da Loja deverá ser numérico!";
                return false;
            }

            if (!String.IsNullOrEmpty(nif) && !numeric.IsMatch(nif))
            {
                erro.Style.Add("display", "block");
                errorMessage.Style.Add("display", "block");
                errorMessage.InnerHtml = "O campo NIF da Loja deverá ser numérico!";
                return false;
            }

            return true;


        }

        protected void btnGrava_Click(object sender, EventArgs e)
        {

            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            else
            {
                Response.Redirect("ListarLoja.aspx", true);
                return;
            }

            if (validaCamposLoja())
            {
                try
                {
                    var minhaLoja = from m in DC.Lojas
                                    where m.ID == Convert.ToInt32(id)
                                    select m;

                    LINQ_DB.Loja loj = new LINQ_DB.Loja();
                    loj = minhaLoja.First();

                    loj.CODIGO = tbcodloja.Text;
                    loj.NOME = tbnome.Text;
                    loj.MORADA = tbmorada.Text;
                    loj.CODPOSTAL = tbcodpostal.Text;
                    loj.LOCALIDADE = tblocalidade.Text;
                    loj.TELEFONE = tbcontactotel.Text;
                    loj.FAX = tbcontactofax.Text;
                    loj.NIF = Convert.ToInt32(tbnif.Text);

                    foreach (var item in minhaLoja)
                    {
                        loj.URL_FOTO = "";
                    }

                    string pastaLogos = "";

                    if (Directory.Exists(Server.MapPath("~\\LogosLojas\\" + loj.CODIGO.ToString())))
                        pastaLogos = Server.MapPath("~\\LogosLojas\\" + loj.CODIGO.ToString());
                    else
                    {
                        string thisDir = "";

                        thisDir = Server.MapPath("~\\LogosLojas\\" + loj.CODIGO.ToString());
                        System.IO.Directory.CreateDirectory(thisDir);
                        pastaLogos = Server.MapPath("~\\LogosLojas\\" + loj.CODIGO.ToString());
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
                            fileTodelete = Server.MapPath("~\\LogosLojas\\" + loj.CODIGO.ToString() + "\\" + file.FileName);
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

                            ResizedBM.Save(Server.MapPath(@"~\\LogosLojas\\" + loj.CODIGO.ToString() + "\\" + file.FileName));
                            ResizedBM.Save(Server.MapPath(@"~\\LogosLojas\\" + loj.CODIGO.ToString() + "\\" + file.FileName.Replace(" ", "")));
                            string clean = Regex.Replace(file.FileName, @"[^\x20-\x7e]", "a");
                            string resultado = Regex.Replace(clean, @"\s", "");

                            loj.URL_FOTO = "../LogosLojas/" + loj.CODIGO.ToString() + "/" + resultado;
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

                            ResizedBM.Save(Server.MapPath(@"~\\LogosLojas\\" + loj.CODIGO.ToString() + "\\" + file.FileName));
                            ResizedBM.Save(Server.MapPath(@"~\\LogosLojas\\" + loj.CODIGO.ToString() + "\\" + file.FileName.Replace(" ", "")));
                            string clean = Regex.Replace(file.FileName, @"[^\x20-\x7e]", "a");
                            string resultado = Regex.Replace(clean, @"\s", "");

                            loj.URL_FOTO = "../LogosLojas/" + loj.CODIGO.ToString() + "/" + resultado;
                        }
                    }

                    if (!String.IsNullOrEmpty(tbobsloja.Text))
                        loj.OBSERVACOES = tbobsloja.Text;
                    else
                        loj.OBSERVACOES = "N/D";

                    DC.SubmitChanges();
                    sucesso.Visible = true;
                    sucessoMessage.Visible = true;
                    sucessoMessage.InnerHtml = "Os dados relativos à Loja " + tbcodloja.Text.ToString() + " foram actualizados com êxito!";

                    SQLLog.registaLogBD(userid, DateTime.Now, "Editar loja", "Foi editada a loja com o código: " + loj.CODIGO.ToString() + ".", true);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }

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

        protected void btnReativarLoja_Click(object sender, EventArgs e)
        {
            string id = "";

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];

                try
                {
                    var minhaLoja = from m in DC.Lojas
                                    where m.ID == Convert.ToInt32(id)
                                    select m;

                    LINQ_DB.Loja loj = new LINQ_DB.Loja();
                    loj = minhaLoja.First();

                    loj.ACTIVO = true;
                    DC.SubmitChanges();
                    Response.Redirect("ListarLoja.aspx", false);
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteError(ex.Message);
                    Response.Redirect("ErrorPage.aspx?erro=" + ex.Message, false);
                }
            }
            else
            {
                Response.Redirect("ListarLoja.aspx", true);
                return;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarLoja.aspx", false);
        }

        protected void setModoRead(bool read)
        {
            if (read == true)
            {
                tbnome.ReadOnly = tbmorada.ReadOnly = tbcodpostal.ReadOnly = tblocalidade.ReadOnly = tbcontactotel.ReadOnly = tbcontactofax.ReadOnly = tbnif.ReadOnly = tbobsloja.ReadOnly = true;
                uploadLogo.Enabled = false;
                erro.Visible = true;
                errorMessage.Visible = true;
                errorMessage.InnerHtml = "Para poder editar os dados de uma loja inactiva, deve em primeiro lugar reactivar a mesma.";
            }
            else
            {
                tbnome.ReadOnly = tbmorada.ReadOnly = tbcodpostal.ReadOnly = tblocalidade.ReadOnly = tbcontactotel.ReadOnly = tbcontactofax.ReadOnly = tbnif.ReadOnly = tbobsloja.ReadOnly = false;
                uploadLogo.Enabled = true;
                erro.Visible = false;
                errorMessage.Visible = false;
                errorMessage.InnerHtml = "";
            }
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace EmailService
{
    public class ServiceEmail : IServiceEmail
    {
        public string EnviaEmailsRegistoCliente(SendInfoRegistoCliente sirc, MailInfoRegistoCliente mirc)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "DYGUS - SAT (onoff)");
            msg.To.Add(new MailAddress(sirc.EmailAddress.ToString()));
            msg.Subject = "DYGUS - SAT (onoff) | Registo de Conta de Cliente";
            msg.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mirc.ToString(), null, "text/html");
            htmlView.LinkedResources.Add(sirc.Logo);
            msg.AlternateViews.Add(htmlView);

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Dygus2017!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            try
            {
                client.SendAsync(msg, null);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public string EnviaEmailsRegistoTecnico(SendInfoRegistoTecnico sirt, MailInfoRegistoTecnico mirt)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "DYGUS - SAT (onoff)");
            msg.To.Add(new MailAddress(sirt.EmailAddress.ToString()));
            msg.Subject = "DYGUS - SAT (onoff) | Registo de Conta de Técnico";
            msg.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mirt.ToString(), null, "text/html");
            htmlView.LinkedResources.Add(sirt.Logo);
            msg.AlternateViews.Add(htmlView);

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Dygus2017!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            try
            {
                client.SendAsync(msg, null);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public string EnviaEmailsRegistoReparador(SendInfoRegistoReparador sirr, MailInfoRegistoReparador mirr)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "DYGUS - SAT (onoff)");
            msg.To.Add(new MailAddress(sirr.EmailAddress.ToString()));
            msg.Subject = "DYGUS - SAT (onoff) | Registo de Conta de Reparador";
            msg.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mirr.ToString(), null, "text/html");
            htmlView.LinkedResources.Add(sirr.Logo);
            msg.AlternateViews.Add(htmlView);

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Dygus2017!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            try
            {
                client.SendAsync(msg, null);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }

        public string EnviaEmailsRegistoLojista(SendInfoRegistoLojista sirl, MailInfoRegistoLojista mirl)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("dygussat@dygus.com", "DYGUS - SAT (onoff)");
            msg.To.Add(new MailAddress(sirl.EmailAddress.ToString()));
            msg.Subject = "DYGUS - SAT (onoff) | Registo de Conta de Utilizador";
            msg.IsBodyHtml = true;
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mirl.ToString(), null, "text/html");
            htmlView.LinkedResources.Add(sirl.Logo);
            msg.AlternateViews.Add(htmlView);

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = false;
            client.Host = "smtpout.europe.secureserver.net";

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("dygussat@dygus.com", "Dygus2017!#");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            try
            {
                client.SendAsync(msg, null);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }
    }
}
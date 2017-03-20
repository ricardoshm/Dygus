using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace EmailService
{
    [ServiceContract]
    public interface IServiceEmail
    {
        [OperationContract]
        string EnviaEmailsRegistoCliente(SendInfoRegistoCliente sirc, MailInfoRegistoCliente mirc);

        [OperationContract]
        string EnviaEmailsRegistoTecnico(SendInfoRegistoTecnico sirt, MailInfoRegistoTecnico mirt);

        [OperationContract]
        string EnviaEmailsRegistoReparador(SendInfoRegistoReparador sirr, MailInfoRegistoReparador mirr);

        [OperationContract]
        string EnviaEmailsRegistoLojista(SendInfoRegistoLojista sirl, MailInfoRegistoLojista mirl);

    }

    [DataContract]
    public class SendInfoRegistoCliente
    {
        [DataMember]
        public Guid UserId
        {
            get;
            set;
        }

        [DataMember]
        public string EmailAddress
        {
            get;
            set;
        }
        [DataMember]
        public string PasswordCliente
        {
            get;
            set;
        }

        public string URL
        {
            get
            {
                return String.Format("http://www.dygus.com/onoff/VerificacaoConta.aspx?id={0}", UserId);
            }
        }
        [DataMember]
        public string UrlLogo
        {
            set;
            get;
        }

        public LinkedResource Logo
        {
            get
            {
                return new LinkedResource(UrlLogo)
                {
                    ContentId = "companylogo",
                };
            }
        }
    }

    [DataContract]
    public class MailInfoRegistoCliente
    {
        [DataMember]
        public SendInfoRegistoCliente Sender
        {
            get;
            set;
        }

        private const string HTML = "<html><head></head><body> Estimado Cliente,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criada uma nova conta de cliente em seu nome com os seguintes dados de acesso:<br/><br/>" +
                "Nome de Utilizador: {1}.<br/>" +
                "Palavra-Passe: {2}.<br/><br/>" +
                "Para concluir o seu registo, por favor clique: <a href=\"{3}\">AQUI</a>.<br/><br/>" +
                "Recomendamos que, para sua segurança, altere a palavra-passe de acesso à plataforma.<br/><br/>" +
                "Muito obrigado pela sua colaboração.<br />" +
                "<img src=cid:companylogo alt=.  /><br /><br /><br/>" +
                "Por favor não responda a este email, a caixa postal emissora deste mail é exclusivamente para envio de mensagens.<br/>" +
                "Para qualquer assunto relacionado com o registo na plataforma por favor contacte-nos através do email ricardoshm@gmail.com." +
                "</body>";

        public override string ToString()
        {
            return String.Format(HTML, Sender.EmailAddress, Sender.PasswordCliente,
                Sender.URL);
        }
    }

    [DataContract]
    public class SendInfoRegistoTecnico
    {
        [DataMember]
        public Guid UserId
        {
            get;
            set;
        }

        [DataMember]
        public string EmailAddress
        {
            get;
            set;
        }
        [DataMember]
        public string PasswordTecnico
        {
            get;
            set;
        }

        public string URL
        {
            get
            {
                return String.Format("http://www.dygus.com/onoff/VerificacaoConta.aspx?id={0}", UserId);
            }
        }
        [DataMember]
        public string UrlLogo
        {
            set;
            get;
        }

        public LinkedResource Logo
        {
            get
            {
                return new LinkedResource(UrlLogo)
                {
                    ContentId = "companylogo",
                };
            }
        }
    }

    [DataContract]
    public class MailInfoRegistoTecnico
    {
        [DataMember]
        public SendInfoRegistoTecnico Sender
        {
            get;
            set;
        }

        private const string HTML = "<html><head></head><body> Estimado Técnico,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criada uma nova conta de técnico em seu nome com os seguintes dados de acesso:<br/><br/>" +
                "Nome de Utilizador: {1}.<br/>" +
                "Palavra-Passe: {2}.<br/><br/>" +
                "Para concluir o seu registo, por favor clique: <a href=\"{3}\">AQUI</a>.<br/><br/>" +
                "Recomendamos que, para sua segurança, altere a palavra-passe de acesso à plataforma.<br/><br/>" +
                "Muito obrigado pela sua colaboração.<br />" +
                "<img src=cid:companylogo alt=.  /><br /><br /><br/>" +
                "Por favor não responda a este email, a caixa postal emissora deste mail é exclusivamente para envio de mensagens.<br/>" +
                "Para qualquer assunto relacionado com o registo na plataforma por favor contacte-nos através do email ricardoshm@gmail.com." +
                "</body>";

        public override string ToString()
        {
            return String.Format(HTML, Sender.EmailAddress, Sender.PasswordTecnico,
                Sender.URL);
        }
    }

    [DataContract]
    public class SendInfoRegistoReparador
    {
        [DataMember]
        public Guid UserId
        {
            get;
            set;
        }

        [DataMember]
        public string EmailAddress
        {
            get;
            set;
        }
        [DataMember]
        public string PasswordReparador
        {
            get;
            set;
        }

        public string URL
        {
            get
            {
                return String.Format("http://www.dygus.com/onoff/VerificacaoConta.aspx?id={0}", UserId);
            }
        }
        [DataMember]
        public string UrlLogo
        {
            set;
            get;
        }

        public LinkedResource Logo
        {
            get
            {
                return new LinkedResource(UrlLogo)
                {
                    ContentId = "companylogo",
                };
            }
        }
    }

    [DataContract]
    public class MailInfoRegistoReparador
    {
        [DataMember]
        public SendInfoRegistoReparador Sender
        {
            get;
            set;
        }

        private const string HTML = "<html><head></head><body> Estimado Reparador,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criada uma nova conta de reparador em seu nome com os seguintes dados de acesso:<br/><br/>" +
                "Nome de Utilizador: {1}.<br/>" +
                "Palavra-Passe: {2}.<br/><br/>" +
                "Para concluir o seu registo, por favor clique: <a href=\"{3}\">AQUI</a>.<br/><br/>" +
                "Recomendamos que, para sua segurança, altere a palavra-passe de acesso à plataforma.<br/><br/>" +
                "Muito obrigado pela sua colaboração.<br />" +
                "<img src=cid:companylogo alt=.  /><br /><br /><br/>" +
                "Por favor não responda a este email, a caixa postal emissora deste mail é exclusivamente para envio de mensagens.<br/>" +
                "Para qualquer assunto relacionado com o registo na plataforma por favor contacte-nos através do email ricardoshm@gmail.com." +
                "</body>";

        public override string ToString()
        {
            return String.Format(HTML, Sender.EmailAddress, Sender.PasswordReparador,
                Sender.URL);
        }
    }

    [DataContract]
    public class SendInfoRegistoLojista
    {
        [DataMember]
        public Guid UserId
        {
            get;
            set;
        }

        [DataMember]
        public string EmailAddress
        {
            get;
            set;
        }
        [DataMember]
        public string Passwordlojista
        {
            get;
            set;
        }

        public string URL
        {
            get
            {
                return String.Format("http://www.dygus.com/onoff/VerificacaoConta.aspx?id={0}", UserId);
            }
        }
        [DataMember]
        public string UrlLogo
        {
            set;
            get;
        }

        public LinkedResource Logo
        {
            get
            {
                return new LinkedResource(UrlLogo)
                {
                    ContentId = "companylogo",
                };
            }
        }
    }

    [DataContract]
    public class MailInfoRegistoLojista
    {
        [DataMember]
        public SendInfoRegistoLojista Sender
        {
            get;
            set;
        }

        private const string HTML = "<html><head></head><body> Estimado Utilizador,<br/><br/>" +
                "Bem-Vindo à plataforma DYGUS - SAT! <br/>" +
                "Foi criada uma nova conta de utilizador em seu nome com os seguintes dados de acesso:<br/><br/>" +
                "Nome de Utilizador: {1}.<br/>" +
                "Palavra-Passe: {2}.<br/><br/>" +
                "Para concluir o seu registo, por favor clique: <a href=\"{3}\">AQUI</a>.<br/><br/>" +
                "Recomendamos que, para sua segurança, altere a palavra-passe de acesso à plataforma.<br/><br/>" +
                "Muito obrigado pela sua colaboração.<br />" +
                "<img src=cid:companylogo alt=.  /><br /><br /><br/>" +
                "Por favor não responda a este email, a caixa postal emissora deste mail é exclusivamente para envio de mensagens.<br/>" +
                "Para qualquer assunto relacionado com o registo na plataforma por favor contacte-nos através do email ricardoshm@gmail.com." +
                "</body>";

        public override string ToString()
        {
            return String.Format(HTML, Sender.EmailAddress, Sender.Passwordlojista,
                Sender.URL);
        }
    }

}

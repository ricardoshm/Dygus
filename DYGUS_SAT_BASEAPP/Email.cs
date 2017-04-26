using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DYGUS_SAT_BASEAPP
{
    public class Email
    {
        private string emailsender;
        public string SENDEREMAIL
        {
            get
            {
                return emailsender;
            }
            set
            {
                emailsender = value;
            }
        }
        private string emailhost { get; set; }
        public string EMAILHOST
        {
            get
            {
                return emailhost;
            }
            set
            {
                emailhost = value;
            }
        }
        private bool emailssl { get; set; }
        public bool EMAILSSL
        {
            get
            {
                return emailssl;
            }
            set
            {
                emailssl = value;
            }
        }
        private string emailalias { get; set; }
        public string EMAILALIAS
        {
            get
            {
                return emailalias;
            }
            set
            {
                emailalias = value;
            }
        }
        private string emailPassword = "Dygus2017!#";
        public string EMAILPASS
        {
            get
            {
                return emailPassword;
            }
            set
            {
                emailPassword = value;
            }
        }
    }
}
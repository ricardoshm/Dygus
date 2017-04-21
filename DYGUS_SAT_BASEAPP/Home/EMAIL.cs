using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DYGUS_SAT_BASEAPP.Home
{
    public class EMAIL
    {
        public string SENDEREMAIL { get; set; }
        public string SMTPHOST { get; set; }
        public string SENDEREMAILPASS { get; set; }
        public string SENDEREMAILALIAS { get; set; }
        public bool SENDEREMAILSSL { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DYGUS_SAT_BASEAPP
{
    public class SQLLog
    {
        public SQLLog()
        { }

        public static void registaLogBD(Guid userid, DateTime data, string menu, string descricao, bool visivel)
        {
            try
            {
                LINQ_DB.DBDataContext DC = new LINQ_DB.DBDataContext();
                LINQ_DB.Log NOVOREGISTO = new LINQ_DB.Log();

                NOVOREGISTO.USERID = userid;
                NOVOREGISTO.DATA = data;
                NOVOREGISTO.MENU = menu;
                NOVOREGISTO.DESCRICAO = descricao;
                NOVOREGISTO.VISIVEL = visivel;

                DC.Logs.InsertOnSubmit(NOVOREGISTO);
                DC.SubmitChanges();
            }
            catch (Exception ex)
            {
                ErrorLog.WriteError(ex.Message);
            }

        }
    }
}
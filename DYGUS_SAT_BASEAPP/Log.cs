using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Configuration;

namespace DYGUS_SAT_BASEAPP
{
    internal class Log
    {

        private static Logger Write { get; set; }

        internal static void Info(string message)
        {
            if (Write != null)
            {
                Write.Info(message);
                if (RunningConsole)
                {
                    Console.WriteLine(message);
                }
            }
        }
        internal static void Error(string message)
        {
            if (Write != null)
            {
                Write.Error(message);
                if (RunningConsole)
                {
                    Console.WriteLine(message);
                }
            }
        }
        internal static void ErrorException(Exception ex)
        {
            if (Write != null)
            {
                Write.Error(ex);
                if (RunningConsole)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static bool _RunningConsole;
        internal static bool RunningConsole
        {
            get { return _RunningConsole; }
            set
            {
                _RunningConsole = value;
                if (value)
                {
                    //InitializeLog();
                }
            }
        }
        internal static bool SaveLog = true;
        internal static string LogLocation = null;



        internal static void InitializeLog()
        {
            LoggingConfiguration config = new LoggingConfiguration();
            if (SaveLog)
            {
                FileTarget target = new FileTarget();
                target.FileName = "${basedir}\\logs\\${date:format=yyyy-MM-dd} DYGUS_SAT.log";
                target.Layout = "${date:format=HH\\:mm\\:ss}  ${level}  [${logger}];  ${message}";
                target.ArchiveAboveSize = 400;
                target.ArchiveNumbering = NLog.Targets.ArchiveNumberingMode.Rolling;
                target.ArchiveEvery = NLog.Targets.FileArchivePeriod.Month;
                config.AddTarget("Dygus FileManager", target);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, target));
            
            }
            //Log para a consola
            if (RunningConsole)
            {
                ColoredConsoleTarget tg = new ColoredConsoleTarget();
                tg.Layout = ConfigurationManager.AppSettings["Layout"].ToString();
                tg.Name = System.Reflection.Assembly.GetExecutingAssembly().FullName;
                config.AddTarget("Dygus FileManager", tg);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, tg));
            }
            LogManager.Configuration = config;
            if (Write == null)
                Write = LogManager.GetLogger("Dygus FileManager");
        }
    }
}
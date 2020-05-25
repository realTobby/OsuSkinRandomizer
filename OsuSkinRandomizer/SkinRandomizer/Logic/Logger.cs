using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinRandomizer.Logic
{
    public enum Severity
    {
        Information,
        Warning,
        Error
    }

    public class Logger
    {
        public void AddLoggerLine(string log, Severity severity)
        {
            System.IO.File.AppendAllText("log.txt",
                "[" + severity.ToString() + "] " +
                log + System.Environment.NewLine);
        }

    }
}

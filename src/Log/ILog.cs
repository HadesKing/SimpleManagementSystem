using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Log
{
    public interface ILog
    {

        void Write(String argMessage, Exception argException = null);

        void Error(String argMessage, Exception argException = null);

        void Warning(String argMessage, Exception argException = null);

        void Info(String argMessage, Exception argException = null);

        void Debug(String argMessage, Exception argException = null);

    }
}
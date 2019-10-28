using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Log
{
    public class NLogLogger : ILog
    {
        /// <summary>
        /// 日志类
        /// </summary>
        private NLog.Logger m_logger = null;
        private NLog.Logger m_ErrorLog = null;
        private NLog.Logger m_WarningLog = null;
        private NLog.Logger m_InfoLog = null;
        private NLog.Logger m_DebugLog = null;

        static NLogLogger()
        {
            //加载配置文件信息
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            LogManager.LoadConfiguration(Path.GetDirectoryName(path) + "\\nlog.config");
        }
        public NLogLogger()
        {
            //m_logger.Info("普通信息日志-----------");
            //m_logger.Debug("调试日志-----------");
            //m_logger.Error("错误日志-----------");
            //m_logger.Fatal("异常日志-----------");
            //m_logger.Warn("警告日志-----------");
            //m_logger.Trace("跟踪日志-----------");
            //m_logger.Log(NLog.LogLevel.Warn, "Log日志------------------");

            //m_Log.WriteError("错误日志-----------");
            //m_Log.WriteWarning("警告日志-----------");
            //m_Log.WriteInfo("普通信息日志-----------");
            //m_Log.WriteDebug("调试日志-----------");

            m_logger = LogManager.GetCurrentClassLogger();
            m_ErrorLog = LogManager.GetLogger("ErrorFile");
            m_WarningLog = LogManager.GetLogger("WarningFile");
            m_InfoLog = LogManager.GetLogger("InfoFile");
            m_DebugLog = LogManager.GetLogger("DebugFile");
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="argMessage">标题信息/简述</param>
        /// <param name="argException">错误</param>
        public void Write(string argMessage, Exception argException = null)
        {
            m_logger.Log(NLog.LogLevel.Info, argException, argMessage);
        }

        /// <summary>
        /// 写入Debug日志
        /// </summary>
        /// <param name="argMessage">标题信息/简述</param>
        /// <param name="argException">错误</param>
        public void Debug(string argMessage, Exception argException = null)
        {
            m_DebugLog.Debug(argException, argMessage);
        }

        /// <summary>
        /// 写入Error日志
        /// </summary>
        /// <param name="argMessage">标题信息/简述</param>
        /// <param name="argException">错误</param>
        public void Error(string argMessage, Exception argException = null)
        {
            m_ErrorLog.Error(argException, argMessage);
        }

        /// <summary>
        /// 写入Info日志
        /// </summary>
        /// <param name="argMessage">标题信息/简述</param>
        /// <param name="argException">错误</param>
        public void Info(string argMessage, Exception argException = null)
        {
            m_InfoLog.Info(argException, argMessage);
        }

        /// <summary>
        /// 写入Warning日志
        /// </summary>
        /// <param name="argMessage">标题信息/简述</param>
        /// <param name="argException">错误</param>
        public void Warning(string argMessage, Exception argException = null)
        {
            m_WarningLog.Warn(argException, argMessage);
        }

    }
}

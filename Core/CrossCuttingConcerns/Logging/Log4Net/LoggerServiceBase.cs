using log4net;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Core.CrossCuttingConcerns.Logging.Log4Net
{
    public class LoggerServiceBase
    {
        private readonly string LogConfigFileName = "log4net.config";
        private readonly ILog _log;
        public LoggerServiceBase(string name)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(File.OpenRead(LogConfigFileName));

            ILoggerRepository loggerRepository = LogManager
                .CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            log4net.Config.XmlConfigurator.Configure(loggerRepository, xmlDocument["log4net"]);
            _log = LogManager.GetLogger(loggerRepository.Name, name);
        }

        public bool IsInfoEnabled => _log.IsInfoEnabled;
        public bool IsDebugEnabled => _log.IsDebugEnabled;
        public bool IsWarnEnabled => _log.IsWarnEnabled;
        public bool IsFatalEnabled => _log.IsFatalEnabled;
        public bool IsErrorEnabled => _log.IsErrorEnabled;

        public void Info(object Message)
        {
            if (IsDebugEnabled)
                _log.Info(Message);
        }

        public void Debug(object Message)
        {
            if (IsDebugEnabled)
                _log.Debug(Message);
        }

        public void Warn(object Message)
        {
            if (IsDebugEnabled)
                _log.Warn(Message);
        }

        public void Fatal(object Message)
        {
            if (IsDebugEnabled)
                _log.Fatal(Message);
        }

        public void Error(object Message)
        {
            if (IsDebugEnabled)
                _log.Error(Message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace Asiana.UI.Services
{
    public class LogService : Asiana.UI.Services.ILogService
    {
        private Logger logger;

        public LogService(Logger logger)
        {
            this.logger = logger;
        }

        public void Log(string message)
        {
            logger.Info(message);
        }
    }
}
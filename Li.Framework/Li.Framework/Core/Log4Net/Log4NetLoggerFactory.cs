using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Li.Framework.Core.Log4Net
{
    /* ==============================================================================
     * 描述：Log4NetLoggerFactory
     * 创建人：李传刚 2017/7/20 13:59:35
     * ==============================================================================
     */
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        public Log4NetLoggerFactory(string configFile)
        {
            FileInfo file = new FileInfo(configFile);
            if (!file.Exists)
            {
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));
            }

            if (file.Exists)
            {
                XmlConfigurator.ConfigureAndWatch(file);
            }
            else
            {
                BasicConfigurator.Configure(new ConsoleAppender { Layout = new PatternLayout() });
            }
        }

        public ILog Create(string name)
        {
            return LogManager.GetLogger(name);
        }

        public ILog Create(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}

﻿using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telecom.TourismModels.MQModels;

//指定log4net使用的config文件来读取配置信息
[assembly: XmlConfigurator(ConfigFile = @"log4net.config", Watch = true)]
namespace DinJonYa.Aop.Models.LogHelper
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class Log4NetHelp
    {
        #region 变量定义
        //定义信息的二次处理
        public static event Action<string> OutputMessage;
        //ILog对象
        public static readonly ILog log = LogManager.GetLogger("Axon Logger");
        //记录异常日志数据库连接字符串
        private static string _ConnectionString = "";
        //信息模板
        private const string _ConversionPattern = @"%newline 
                           ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
                           %n【日志级别】：%-5level 
                           %n【记录时间】：%date 
                           %n【线 程 ID】: [%thread] 
                           %n【执行时间】: %r 毫秒
                           %n【出错文件】: %F
                           %n【出错行号】: %L
                           %n【出 错 类】：%logger property: [%property{NDC}]
                           %n【错误描述】：%message
                           %n【错误详情】：
                           ＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
                           %newline";
        #endregion

        #region 定义信息二次处理方式
        private static void HandMessage(object Msg)
        {
            if (OutputMessage != null)
            {
                OutputMessage(Msg.ToString());
            }
        }
        private static void HandMessage(object Msg, Exception ex)
        {
            if (OutputMessage != null)
            {
                OutputMessage(string.Format("{0}:{1}", Msg.ToString(), ex.ToString()));
            }
        }
        private static void HandMessage(string format, params object[] args)
        {
            if (OutputMessage != null)
            {
                OutputMessage(string.Format(format, args));
            }
        }
        #endregion

        #region 封装Log4net
        public static void Debug(object message)
        {
            HandMessage(message);
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        public static void Debug(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsDebugEnabled)
            {
                log.Debug(message, ex);
            }
        }
        public static void DebugFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsDebugEnabled)
            {
                log.DebugFormat(format, args);
            }
        }
        public static void Error(object message)
        {
            HandMessage(message);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        public static void Error(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }
        public static void ErrorFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsErrorEnabled)
            {
                log.ErrorFormat(format, args);
            }
        }
        public static void Fatal(object message)
        {
            HandMessage(message);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void Fatal(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message, ex);
            }
        }
        public static void FatalFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsFatalEnabled)
            {
                log.FatalFormat(format, args);
            }
        }
        public static void Info(object message)
        {
            HandMessage(message);
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }
        public static void Info(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsInfoEnabled)
            {
                log.Info(message, ex);
            }
        }
        public static void InfoFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsInfoEnabled)
            {
                log.InfoFormat(format, args);
            }
        }
        public static void Warn(object message)
        {
            HandMessage(message);
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        public static void Warn(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsWarnEnabled)
            {
                log.Warn(message, ex);
            }
        }
        public static void WarnFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsWarnEnabled)
            {
                log.WarnFormat(format, args);
            }
        }
        #endregion

        #region 手动加载配置
        public static void LoadADONetAppender()
        {
            Hierarchy hier = LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
            if (hier != null)
            {
                log4net.Appender.AdoNetAppender appender = new log4net.Appender.AdoNetAppender();
                appender.Name = "AdoNetAppender";
                appender.CommandType = CommandType.Text;
                appender.BufferSize = 1;
                appender.ConnectionType = "MySql.Data.MySqlClient.MySqlConnection, MySql.Data, Version=6.9.9.0, Culture=neutral, PublicKeyToken=c5687fc88969c44d";
                appender.ConnectionString = _ConnectionString;
                appender.CommandText = @"INSERT INTO Log ([Date],[Thread],[Level],[Logger],[Message],[Exception]) VALUES (@log_date, @thread, @log_level, @logger, @message, @exception)";
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@log_date", DbType = System.Data.DbType.DateTime, Layout = new log4net.Layout.RawTimeStampLayout() });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@thread", DbType = System.Data.DbType.String, Size = 255, Layout = new Layout2RawLayoutAdapter(new PatternLayout("%thread")) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@log_level", DbType = System.Data.DbType.String, Size = 50, Layout = new Layout2RawLayoutAdapter(new PatternLayout("%level")) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@logger", DbType = System.Data.DbType.String, Size = 255, Layout = new Layout2RawLayoutAdapter(new PatternLayout("%logger")) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@message", DbType = System.Data.DbType.String, Size = 4000, Layout = new Layout2RawLayoutAdapter(new PatternLayout("%message")) });
                appender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@exception", DbType = System.Data.DbType.String, Size = 4000, Layout = new Layout2RawLayoutAdapter(new ExceptionLayout()) });
                appender.ActivateOptions();
                BasicConfigurator.Configure(appender);
            }
        }

        public static void LoadFileAppender()
        {
            FileAppender appender = new FileAppender();
            appender.Name = "FileAppender";
            appender.File = "error.log";
            appender.AppendToFile = true;

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = _ConversionPattern;
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;

            //选择UTF8编码，确保中文不乱码。
            appender.Encoding = Encoding.UTF8;

            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);

        }

        public static void LoadRollingFileAppender()
        {
            RollingFileAppender appender = new RollingFileAppender();
            appender.AppendToFile = true;
            appender.Name = "RollingFileAppender";
            appender.DatePattern = "yyyy-MM-dd HH'时.log'";
            appender.File = "Logs/";
            appender.AppendToFile = true;
            appender.RollingStyle = RollingFileAppender.RollingMode.Composite;
            appender.MaximumFileSize = "500kb";
            appender.MaxSizeRollBackups = 10;
            appender.StaticLogFileName = false;


            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = _ConversionPattern;
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;

            //选择UTF8编码，确保中文不乱码。
            appender.Encoding = Encoding.UTF8;

            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);
        }

        public static void LoadConsoleAppender()
        {
            ConsoleAppender appender = new ConsoleAppender();
            appender.Name = "ConsoleAppender";

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = _ConversionPattern;
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;

            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);
        }

        public static void LoadTraceAppender()
        {
            TraceAppender appender = new TraceAppender();
            appender.Name = "TraceAppender";

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = _ConversionPattern;
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;

            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);
        }

        public static void LoadEventLogAppender()
        {
            EventLogAppender appender = new EventLogAppender();
            appender.Name = "EventLogAppender";

            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = _ConversionPattern;
            patternLayout.ActivateOptions();
            appender.Layout = patternLayout;

            appender.ActivateOptions();
            BasicConfigurator.Configure(appender);
        }
        #endregion

        #region 定义常规应用程序中未处理的异常信息记录方式
        public static void LoadUnhandledException()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler
             ((sender, e) =>
             {
                 log.Fatal("未处理的异常", e.ExceptionObject as Exception);
             });
        }
        #endregion

        #region 公开调用方法   反射调用
        public static int LogWriteFile(Loglevel level, MQLog_Model model)
        {
            try
            {
                Type t = typeof(Log4NetHelp);
                if (model.Exc==null)
                {
                    t.InvokeMember(level.ToString(), BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                                          null, null, new object[] { model.Message+" "+model.IpLocation });
                    return 0;

                }
                else
                {
                    t.InvokeMember(level.ToString(), BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                                      null, null, new object[] { model.Message, model.Exc });
                    return 0;
                }
            }
            catch (Exception exc)
            {
                Log4NetHelp.Error(exc.Message, exc);
                return 1;
            }
        }
        #endregion


        /// <summary>
        /// 取得当前源码的哪一行
        /// </summary>
        /// <returns></returns>
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 取当前源码的源文件名
        /// </summary>
        /// <returns></returns>
        public static string GetCurSourceFileName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileName();

        }
    }
}
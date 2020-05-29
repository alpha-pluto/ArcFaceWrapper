/*----------------------------------------------------------------
 *  
 * C# Wrapper for ArcSoft Face Engine SDK 2.2 C++ 
 * 2019-7-14  
 * daniel.zhang
 * zamen@126.com
 * 文件名：SimplifiedLogHelper.cs  
 * 文件功能描述：简单日志类
 * 
----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Afw.Core.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SimplifiedLogHelper
    {
        #region 日志有关

        private static string g_base_dir = AppDomain.CurrentDomain.BaseDirectory;

        private static object g_object = new object();

        /// <summary>
        /// 写入日志（带参数）
        /// </summary>
        /// <param name="inLogMessage">日志内容</param>
        /// <param name="_inLogType_">日志类型</param>
        /// <param name="_inTimeZone_">TIMEZONE</param>
        /// <param name="_inReqIPAddr_">请求端的IP</param>
        /// <param name="_inHttpRef_">来源页</param>
        /// <param name="_inUrl_">访问页</param>
        private static void WriteIntoLogFullFormat(
            string inLogMessage,
            string inReporter,
            string subPath = "common"
        )
        {
            lock (g_object)
            {
                var subpath = string.IsNullOrEmpty(subPath) ? "common" : subPath;
                var s = System.Text.RegularExpressions.Regex.Replace(DateTime.Now.ToString("yyyy-MM-dd-HH"), @"[^\d]", "");
                var short_date = System.Text.RegularExpressions.Regex.Replace(DateTime.Now.ToString("yyyy-MM-dd"), @"[^\d]", "");
                var ts = DateTime.Now;
                var subpathYear = ts.Year.ToString("0000");
                var subpathMonth = ts.Month.ToString("00");
                var subpathDay = ts.Day.ToString("00");
                StringBuilder sbLogContent = new StringBuilder();
                //string logFileName = string.Format(@"{0}log\{1}\{2}-log{3}.txt", g_base_dir, short_date, subpath, s);
                string logPath = $"{g_base_dir}log\\{subpathYear}\\{subpathMonth}\\{subpathDay}";
                string logFileName = $"{logPath}\\{subpath}-log{s}.txt";
                try
                {
                    if (!Directory.Exists(logPath))
                    {
                        Directory.CreateDirectory(logPath);
                    }
                    if (File.Exists(logFileName))
                    {
                        using (StreamWriter w = File.AppendText(logFileName))
                        {
                            sbLogContent.AppendLine("");
                            sbLogContent.AppendLine("**********************************");
                            sbLogContent.AppendLine("Date & time : {0}");
                            sbLogContent.AppendLine("Reporter: {1}");
                            sbLogContent.AppendLine("###### Detail Start ###### ");
                            sbLogContent.AppendLine("{2}");
                            sbLogContent.AppendLine("####### Detail End #######");
                            sbLogContent.AppendLine("**********************************");
                            sbLogContent.AppendLine("");

                            w.Write(sbLogContent.ToString(), DateTime.Now, inReporter, inLogMessage);
                            w.Flush();
                            // Close the writer and underlying file.
                            w.Close();
                        }

                    }
                    else
                    {
                        using (StreamWriter sw = File.CreateText(logFileName))
                        {
                            sbLogContent.AppendLine("");
                            sbLogContent.AppendLine("**********************************");
                            sbLogContent.AppendLine("Date & time : {0}");
                            sbLogContent.AppendLine("Reporter: {1}");
                            sbLogContent.AppendLine("###### Detail Start ###### ");
                            sbLogContent.AppendLine("{2}");
                            sbLogContent.AppendLine("####### Detail End #######");
                            sbLogContent.AppendLine("**********************************");
                            sbLogContent.AppendLine("");

                            sw.Write(sbLogContent.ToString(), DateTime.Now, inReporter, inLogMessage);
                            sw.Close();
                        }

                    }
                }
                catch (Exception err)
                {
                    using (StreamWriter w = File.AppendText(logFileName))
                    {
                        sbLogContent.AppendLine("");
                        sbLogContent.AppendLine("**********************************");
                        sbLogContent.AppendLine("Date & time : {0}");
                        sbLogContent.AppendLine("Reporter: {1}");
                        sbLogContent.AppendLine("###### Detail Start ###### ");
                        sbLogContent.AppendLine("{2}");
                        sbLogContent.AppendLine("####### Detail End #######");
                        sbLogContent.AppendLine("**********************************");
                        sbLogContent.AppendLine("");

                        w.Write(sbLogContent.ToString(), DateTime.Now, inReporter, inLogMessage);
                        w.WriteLine(err.ToString());
                        w.Flush();
                        // Close the writer and underlying file.
                        w.Close();
                    }
                }
            }
        }

        #region system log

        /// <summary>
        /// 写入系统日志（正常）
        /// </summary>
        /// <param name="inLogMessage"></param>
        public static void WriteIntoSystemLog(string inLogMessage)
        {
            WriteIntoLogFullFormat(inLogMessage, "SYSTEM");
        }

        public static void WriteIntoSystemLog(string subPath, string inLogMessage)
        {
            WriteIntoLogFullFormat(inLogMessage, "SYSTEM", subPath);
        }

        public static void WriteIntoSystemLogQueue(string inLogMessage)
        {
            WriteIntoLogFullFormat(inLogMessage, "SYSTEM", "Queue");
        }

        public static void WriteIntoSystemLog(string inLogMessage, Dictionary<string, string> dictObject)
        {
            StringBuilder sbDict = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in dictObject)
            {
                sbDict.AppendLine("");
                sbDict.AppendFormat("{ {0} => {1} }", kv.Key, kv.Value);
            }

            WriteIntoLogFullFormat(inLogMessage + sbDict.ToString(), "SYSTEM");
        }

        #endregion

        #endregion
    }
}

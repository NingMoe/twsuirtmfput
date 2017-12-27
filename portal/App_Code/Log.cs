using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Text;

/// <summary>
/// Log 的摘要说明
/// </summary>
 
    /// <summary>
    /// 日志文件名和文件的生成方式
    /// </summary>
    public enum LogTimeSpan
    {
        Year,
        Month,
        Day,
        None
    }

    public static class Log
    {       
        private static string _FilePath ="";
        private static LogTimeSpan _LogTimeSpan = LogTimeSpan.None;

        /// <summary>
        /// 日志文件的存放路径
        /// </summary>
        public static string FilePath
        {
            set
            {
                _FilePath = value;
                string path = _FilePath.Substring(0, _FilePath.LastIndexOf("\\"));
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            } 
        }

        public static LogTimeSpan LogTimeSpan
        {
            set
            {
                _LogTimeSpan = value;
            }
        }

        private static void WriteString(string errtype, string message)
        {
            WriteString("[" + errtype + "]" + DateTime.Now.ToString() + "\r\n" + message + "\r\n");
        }

        private static void WriteString(string errtype, string message, Exception exception)
        {
            WriteString("[" + errtype + "]" + DateTime.Now.ToString() + "\r\n" + exception.ToString() + "\r\n");

        }

        private static void WriteString(string value)
        {
            try
            {
                string strFileName = "";
                string strFileExtension = "";
                string strDirectory = "";
                strDirectory = _FilePath.Substring(0, _FilePath.LastIndexOf("\\"));
                strFileName = _FilePath.Substring(_FilePath.LastIndexOf("\\") + 1);
                if (strFileName.IndexOf(".") != -1)
                {
                    strFileExtension = strFileName.Substring(strFileName.LastIndexOf(".") + 1);
                    strFileName = strFileName.Substring(0, strFileName.LastIndexOf("."));
                }
                if (strFileName == "") strFileName = "log";

                switch (_LogTimeSpan)
                {
                    case LogTimeSpan.Day:
                        strFileName += Convert.ToString(DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day);
                        break;
                    case LogTimeSpan.Year:
                        strFileName += DateTime.Now.Year.ToString();
                        break;
                    default:
                        strFileName += Convert.ToString(DateTime.Now.Year * 100 + DateTime.Now.Month);
                        break;
                }

                if (strFileExtension != "")
                {
                    strFileName = strFileName + "." + strFileExtension;
                }
                else
                {
                    strFileName = strFileName + ".log";
                }

                StreamWriter sw = new System.IO.StreamWriter(File.AppendText(strDirectory + "\\" + strFileName).BaseStream, Encoding.UTF8);
                sw.WriteLine(value);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message">信息的内容</param>
        public static void Error(string message)
        {
            WriteString("Error", message);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="message">信息的内容</param>
        /// <param name="exception">异常来源</param>
        public static void Error(string message, Exception exception)
        {
            WriteString("Error", message, exception);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message">记录错误信息</param>
        public static void Warning(string message)
        {
            WriteString("Warning", message);
        }

        /// <summary>
        /// 记录警告信息
        /// </summary>
        /// <param name="message">记录错误信息</param>
        /// <param name="exception">异常来源</param>
        public static void Warning(string message, Exception exception)
        {
            WriteString("Warning", message, exception);
        }

        /// <summary>
        /// 记录提示信息
        /// </summary>
        /// <param name="message">记录错误信息</param>
        public static void Infomation(string message)
        {
            WriteString("Infomation", message);
        }

        /// <summary>
        /// 记录提示信息
        /// </summary>
        /// <param name="message">记录错误信息</param>
        /// <param name="exception">异常来源</param>
        public static void Infomation(string message, Exception exception)
        {
            WriteString("Infomation", message, exception);
        }

        /// <summary>
        /// 记录未知信息
        /// </summary>
        public static void Write(string message)
        {
            WriteString("Unknown", message);
        }

        /// <summary>
        /// 记录Hashtable的值
        /// </summary>
        /// <param name="hst"></param>
        //public static void Write(Hashtable hst)
        //{
        //    string strValue = "";
        //    IDictionaryEnumerator em = hst.GetEnumerator();
        //    while (em.MoveNext())
        //    {
        //        strValue += em.Key.ToString() + ":" + em.Value.ToString() + ";";
        //    }
        //    WriteString("Unknown", strValue);
        //}

        /// <summary>
        /// 记录未知信息
        /// </summary>
        /// <param name="message">记录错误信息</param>
        /// <param name="exception">异常来源</param>
        public static void Write(string message, Exception exception)
        { 
            WriteString("Unknown", message, exception);
        }

        /// <summary>
        /// 提取异常中的必要的信息。
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static string GetExceptionMessage(Exception exception)
        {
            if (exception == null) return "";
            string strexception = "";
            try
            {
                string strTemp = exception.StackTrace.Trim();
                string strTemp1 = exception.StackTrace.ToLower();
                
                strexception = exception.StackTrace;

            }
            catch (Exception ex)
            {
                return "";
            }
            finally
            {

            }
            return strexception;
        }

    } 
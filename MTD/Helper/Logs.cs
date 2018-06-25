using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace MTD.Helper
{
    public static class Logs
    {
        /// <summary>
        /// Xử lý log với tên log có sẵn
        /// </summary>
        /// <param name="logMessage"></param>
        public static void LogWrite(string logMessage)
        {
            try
            {
                using (StreamWriter w = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "\\" + "DB_Error_log.txt"))
                {
                    LogWrite(logMessage, w);
                }
            }
            catch
            {
            }
        }

        private static void LogWrite(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch
            {
            }
        }

        /// <summary>
        /// Xử lý lưu log với tên mới
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="fileName"></param>
        public static void LogWrite(string logMessage, string fileName)
        {
            try
            {
                using (StreamWriter w = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName))
                {
                    LogWrite(logMessage, w);
                }
            }
            catch
            {
            }
        }
    }
}

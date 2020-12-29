using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForms.HelloFrom;

namespace WindowsForms
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HelloWorld());
        }
        public static void WriteLog(string param1, string param2)
        {
            System.IO.File.AppendAllText(
            "ExceLog_Name", // 日志文bai件名
            string.Format("{0}\t{1}\t{2}", DateTime.Now, param1, param2), // 用制表符 \t 分隔字段
            Encoding.Default);
        }
    }
}

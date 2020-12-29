using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WindowsForms
{
    public static class FileHelper
    {
        /// <summary>
        /// 序列化指定类型的对象到指定的Xml文件
        /// </summary>
        /// <typeparam name="T">要序列化的对象类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="xmlFileName">保存对象数据的完整文件名</param>
        public static void SerializeXml<T>(T obj, string xmlFileName)
        {
            lock (xmlFileName)
            {
                try
                {
                    string dir = Path.GetDirectoryName(xmlFileName);       //获取文件路径
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    string xmlContent = SerializeObject<T>(obj);
                    FileHelper.WriteFile(xmlFileName, xmlContent, System.Text.Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }

        /// <summary>
        /// 把对象序列化为xml字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj)
        {
            if (obj != null)
            {
                StringWriter strWriter = new StringWriter();
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(strWriter, obj);
                return strWriter.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// 向指定文件写入内容
        /// </summary>
        /// <param name="path">要写入内容的文件完整路径</param>
        /// <param name="content">要写入的内容</param>
        /// <param name="encoding">编码格式</param>
        public static void WriteFile(string path, string content, System.Text.Encoding encoding)
        {
            try
            {
                object obj = new object();
                if (!System.IO.File.Exists(path))
                {
                    System.IO.FileStream fileStream = System.IO.File.Create(path);
                    fileStream.Close();
                }
                lock (obj)
                {
                    using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path, false, encoding))
                    {
                        streamWriter.WriteLine(content);
                        streamWriter.Close();
                        streamWriter.Dispose();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.Write(ex);

            }
        }
    }
}

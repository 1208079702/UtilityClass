using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitchEncode
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string path = Console.ReadLine();
                //string path2 = @"D:\utf.txt";
                //AnsiSwitchFile(path, path2);

                SwitchEncode(path,Encoding.Default, Encoding.UTF8);
                Console.WriteLine("转换完成");
            }
        }

        private static void SwitchEncode(string sourcePath,Encoding sourcEncoding,Encoding targetEncoding)
        {
            
            string source = File.ReadAllText(sourcePath);
            byte[] sourceBytes = sourcEncoding.GetBytes(source);
            byte[] targetBytes= Encoding.Convert(sourcEncoding, targetEncoding, sourceBytes);
            string target = targetEncoding.GetString(targetBytes);
            File.WriteAllText(sourcePath,target);
        }

        /// <summary>
        /// ANSI  转到  UTF-8
        /// </summary>
        /// <param name="path"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        private static bool AnsiSwitchFile(string path,string path2)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            else
            {
                using (StreamReader reader = new StreamReader(path,Encoding.Default,false))
                {
                    using (StreamWriter writer = new StreamWriter(path2, false,Encoding.UTF8))
                    {
                        writer.Write(reader.ReadToEnd());
                    }
                }
                FileInfo file = new FileInfo(path2);
                File.Delete(path);
                file.MoveTo(path);
                return true;
            }
        }
        /// <summary>
        /// Unicode 转到 UTF -8
        /// </summary>
        /// <param name="path"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        private static bool UnicodeSwitchFile(string path, string path2)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            else
            {
                using (StreamReader reader = new StreamReader(path,true))
                {
                    using (StreamWriter writer = new StreamWriter(path2, false, Encoding.UTF8))
                    {
                        writer.Write(reader.ReadToEnd());
                    }
                }
                FileInfo file = new FileInfo(path2);
                File.Delete(path);
                file.MoveTo(path);
                return true;
            }
        }
    }
}

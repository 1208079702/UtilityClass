﻿using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RenameTexture
{
    class Program
    {
        static void Main(string[] args)
        {

            string direcPath = @"D:\Desktop\常规项目\魔墙工程\嘉兴平湖2018.9.30\资料\照片 - 1";
            RenameTexture(direcPath, 1);
            Console.WriteLine("转换完成");
            Console.Read();
        }
        /// <summary>
        /// 重命名文件，使下标从零开始
        /// </summary>
        /// <param name="textureDire">路径</param>
        /// <param name="startIndex">源目录的开始下标</param>
        private static void RenameTexture(string textureDire, int startIndex)
        {
            DirectoryInfo info = new DirectoryInfo(textureDire);
            FileInfo[] files = info.GetFiles();
            StringBuilder builder = new StringBuilder();
            foreach (FileInfo file in files)
            {
                builder.Clear();
                string[] allstr = file.FullName.Split('\\');
                for (int i = 0; i < allstr.Length; i++)
                {
                    if (i == allstr.Length - 1)
                    {
                        Match mc = Regex.Match(allstr[i], "[0-9]+");
                        int index = Int32.Parse(mc.Value);
                        string substr = allstr[i].Substring(mc.Value.Length);
                        allstr[i] = (index - startIndex) + substr;
                        builder.Append(allstr[i]);
                    }
                    else
                    {
                        builder.Append(allstr[i]+'\\');
                    }
                }
                file.MoveTo(builder.ToString()); //重命名
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using LitJson;

namespace CreateJson
{
    class Program
    {
        static void Main(string[] args)
        {
            //string direcPath = @"D:\Desktop\常规项目\幻影成像\昆明市馆\程序\KMHYCX\Assets\StreamingAssets\Clip";
            //string jsonPath = @"D:\Desktop\常规项目\幻影成像\昆明市馆\程序\KMHYCX\Assets\StreamingAssets\Config\VideoJson.txt";
            //Write2Json(SetTheJsonObj(direcPath), jsonPath);

            //string KunmingdirecPath = @"D:\Desktop\常规项目\飞趣360\昆明市馆\资源\图片";
            //string KunmingjsonPath = @"D:\Desktop\常规项目\飞趣360\昆明市馆\资源\图片\TextureJson.json";
            //Write2Json(SetTheJsonObj(KunmingdirecPath), KunmingjsonPath);




            Console.WriteLine("转换完成");
            Console.Read();
        }
        ///// <summary>
        ///// 建德高铁新区馆
        ///// </summary>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //private static List<Detail> SetTheJsonObj(string path)
        //{
        //    List<Detail> details =new List<Detail>();
        //    DirectoryInfo info = new DirectoryInfo(path);
        //    FileInfo[] files = info.GetFiles();
        //    foreach (FileInfo file in files)
        //    {
        //        string[] allstr = file.FullName.Split('\\');
        //        string[] temp = allstr[allstr.Length - 1].Split('_');
        //        Detail detail = new Detail(int.Parse(temp[0]),temp[1].Split('.')[0]);
        //        details.Add(detail);
        //    }
        //    return details;
        //}

        /// <summary>
        /// 昆明馆
        /// </summary>
        /// <param name="path"></param>
        public static List<昆明馆.Detail> SetTheJsonObj(string path)
        {
            List<昆明馆.Detail> details = new List<昆明馆.Detail>();
            DirectoryInfo info = new DirectoryInfo(path);
            DirectoryInfo[] directs = info.GetDirectories();
            for (int i = 0; i < directs.Length; i++)
            {
                FileInfo[] files = directs[i].GetFiles();
                List<string> textureName =new List<string>();
                foreach (FileInfo file in files)
                {
                    textureName.Add(file.Name); 
                }
                昆明馆.Detail detail = new 昆明馆.Detail(i, directs[i].Name, textureName);
                details.Add(detail);
            }
            return details;
        }

        /// <summary>
        /// 将Json写入文档
        /// </summary>
        /// <param name="objJson">写入的内容</param>
        /// <param name="jsonpath">json路径</param>
        public static void Write2Json(object objJson, string jsonpath)
        {
            string jsonstr = JsonMapper.ToJson(objJson);
            // 将中文从Unicode 转换成 UTF-8
            jsonstr = UnicodeToString(jsonstr);         // 这一步也可以不进行（json网站可以完成）
            File.WriteAllText(jsonpath, jsonstr);
        }

        /// <summary>
        /// unicode编码转换为中文
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string UnicodeToString(string text)
        {
            MatchCollection mc = Regex.Matches(text, "\\\\u([\\w]{4})");
            if (mc != null && mc.Count > 0)
            {
                List<Match> matchs = new List<Match>(); // 保存已经转换好的就不用第二次转换了，节省性能
                foreach (Match m2 in mc)
                {
                    if (matchs.Contains(m2))
                        break;
                    matchs.Add(m2);
                    string v = m2.Value;
                    string word = v.Substring(2);
                    byte[] codes = new byte[2];
                    int code = Convert.ToInt32(word.Substring(0, 2), 16);
                    int code2 = Convert.ToInt32(word.Substring(2, 2), 16);
                    codes[0] = (byte)code2;
                    codes[1] = (byte)code;
                    text = text.Replace(v, Encoding.Unicode.GetString(codes)); // 从Unicode编码中提取字符串,.NET内置的会把中文字符串转换成UTF-8
                }
            }
            return text;
        }
    }
}

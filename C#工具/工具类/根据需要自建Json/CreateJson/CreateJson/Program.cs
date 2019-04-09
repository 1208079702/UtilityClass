using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CreateJson.武汉光谷;
using LitJson;
using Newtonsoft.Json;

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

            string jsonPath = @"D:\Desktop\IndustryDetail.json";

            Write2Json2(SetTheJsonObj2(), jsonPath);

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
                List<string> textureName = new List<string>();
                foreach (FileInfo file in files)
                {
                    textureName.Add(file.Name);
                }
                昆明馆.Detail detail = new 昆明馆.Detail(i, directs[i].Name, textureName);
                details.Add(detail);
            }
            return details;
        }


        public static List<武汉光谷.IndustryDetail> SetTheJsonObj2(/*string path*/)
        {
            Dictionary<int, Dictionary<int, Dictionary<int, int>>> Scores = new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();
            Dictionary<int, Dictionary<int, int>> companyScores = new Dictionary<int, Dictionary<int, int>>();
            Dictionary<int, int> companyTexScores = new Dictionary<int, int>();


            //Dictionary<string, IndustryDetail> industrys = new Dictionary<string, IndustryDetail>();
            List<武汉光谷.IndustryDetail> industrys = new List<IndustryDetail>();
            IndustryDetail industry = new IndustryDetail();
            industry.Id = 0;
            industry.Name = "光电子信息产业";
            industry.Introduction =
                "光谷是我国光通信产业的发源地，我国最大的光纤光缆、光器件研发和生产基地，我国最大的光通信技术研发基地和激光产业发展引领区，中国最大的中小尺寸显示面板生产和研发基地。光纤光缆的生产规模居全球第一，代表国家参与世界竞争的品牌。";
            industrys.Add(industry);
            //industrys.Add(industry);
            //industrys.Add(industry.Name,industry);
            //Dictionary<string,CompanyDetail> companys =new Dictionary<string, CompanyDetail>();
            Scores.Add(industry.Id, companyScores);


            List<CompanyDetail> companys = new List<CompanyDetail>();
            industry.CompanyDetails = companys;
            CompanyDetail company = new CompanyDetail();
            company.Id = 0;
            company.Company = "中国信息通信科技集团有限公司";
            company.CompanyIntroduction =
                "烽火科技集团是国际知名的信息通信网络产品与解决方案提供商。已跻身全球光通信最具竞争力企业十强，其中，光传输产品收入全球前五；宽带接入产品收入全球前四；光缆综合实力全球前四，连续八年位列中国光缆企业出口第一。";
            company.TextureDetails = new Dictionary<int, TextureDetail>();
            TextureDetail tex = new TextureDetail();
            tex.Id = 0;
            tex.Path = "0.jpg";
            tex.Introduction = "【高速光器件测试】中国信科集团（烽火科技）研发人员正在对光系统核心芯片进行检测";
            company.TextureDetails.Add(tex.Id, tex);

            companys.Add(company);
            //companys.Add(company);
            //companys.Add(company.Company,company);
            //companys.Add(company.Company+"2", company);
            companyScores.Add(company.Id, companyTexScores);
            companyTexScores.Add(tex.Id,100);

            Write2Json2(Scores, @"D:\Desktop\ScoreDetail.json");
            return industrys;
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

        public static void Write2Json2(object objJson, string jsonpath)
        {
            string jsonstr = JsonConvert.SerializeObject(objJson);
            File.WriteAllText(jsonpath, jsonstr);
        }
    }
}

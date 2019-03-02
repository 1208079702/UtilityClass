using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using LitJson;
using Newtonsoft.Json;

/*
 *  行包车间 0
 *  南行包车间 10000
 *  售票车间 20000
 *  上西车间 30000
 *  客运车间 40000
 *  虹桥车间 50000
     南客运车间 60000
     上南运转车间 70000
     运转车间 80000
     综合车间 90000
     机关科室 100000
     六十佳  110000
 */
namespace ConsoleApp3
{
    class Program
    {
        static List<Detail> lists = new List<Detail>();
        static List<Score> listscores = new List<Score>();
        static string path = @"D:\Desktop\常规项目\魔墙\05_上海站人才魔墙\资料\软件资料\0劳模\2017六十佳\附件1员工信息采集表.xls";
        static string texPath = @"D:\Desktop\常规项目\魔墙\05_上海站人才魔墙\资料\软件资料\0劳模\2017六十佳\六十佳照片";
        static string audioPath = @"D:\Desktop\六十佳录音\新建文件夹";
        static string department = "六十佳";
        static string jsondetail = @"D:\Desktop\" + department + "Detail.json";
        static string jdonscore = @"D:\Desktop\" + department + "Score.json";
        static int index = 110000;
        static string company = "上海火车站";

        static bool isimportment = true;
        //private static int inss =0;
        static void Main(string[] args)
        {
            //OpenExcel(path, texPath, audioPath);
            //Obj2jSON(jsondetail, lists);
            //Obj2jSON(jdonscore, listscores);
            //RemaneTex(@"D:\Desktop\资料\车间\虹桥\周五前党办照片\检1");
            //List<Score> list = new List<Score>();
            //for (int i = 100000; i < 100144; i++)
            //{
            //    Score sc = new Score();
            //    sc.Id = i;
            //    sc.LikeCount = 100;
            //    list.Add(sc);
            //}

            //string strjson = JsonMapper.ToJson(list);
            //File.WriteAllText(jdonscore, strjson);


            string excPath = @"D:\Desktop\Building.xlsx";
            string jsonPath = @"D:\Desktop\Data_Building.json";
            OpenXHExcel(excPath, jsonPath);

            Console.WriteLine("已完成");
            Console.Read();
        }
        //读取EXCEL的方法   (用范围区域读取数据)
        private static void OpenExcel(string excelPath, string texPath, string audioPath)
        {
            object missing = System.Reflection.Missing.Value;
            Application excel = new Application();//lauch excel application
            excel.Visible = false;
            excel.UserControl = true;
            // 以只读的形式打开EXCEL文件
            Workbook wb = excel.Application.Workbooks.Open(excelPath, missing, true, missing, missing, missing, missing, missing, missing, true, missing, missing, missing, missing, missing);
            //取得第一个工作薄
            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);
            //取得总记录行数   (包括标题列)
            int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
            Console.WriteLine("行数 " + rowsint);
            int columnsint = ws.UsedRange.CurrentRegion.Columns.Count;//得到列数
            for (int i = 3; i < rowsint + 1; i++)
            {
                Range rng1 = ws.Cells.get_Range("B" + i, "I" + i);   //item
                object[,] arryItem = (object[,])rng1.Value2;
                int row = arryItem.GetLength(0);    //1
                for (int j = 1; j < row + 1; j++)
                {
                    if (arryItem[j, 1] == null) break; //名字为空。就舍弃
                    string name = Regex.Replace((string)arryItem[j, 1], @"\s", ""); //将名字中间空格取消
                    //if (name == "周鑫")
                    //{
                    //    Console.WriteLine(name);
                    //}

                    List<string> texsName = FindTex(texPath, name);
                    if (texsName.Count == 0)
                        Console.WriteLine(name);

                    for (int k = 0; k < texsName.Count; k++)
                    {
                        Detail detail = new Detail();
                        detail.Id = index;

                        detail.Company = company;
                        detail.CompanyIntroduction = null;
                        detail.Department = department;
                        detail.DepartmentIntroduction = null;

                        detail.Name = (string)arryItem[j, 1];
                        //detail.Age = null;
                        if (arryItem[j, 2] == null)
                            detail.Age = null;
                        else
                            detail.Age = DateTime.FromOADate((double)arryItem[j, 2]).ToString("yyyy年 MM月");
                        if (arryItem[j, 3] == null)
                            detail.Group = null;
                        else
                            detail.Group = (string)arryItem[j, 3];
                        if (arryItem[j, 4] == null)
                            detail.Work = null;
                        else
                            detail.Work = (string)arryItem[j, 4];
                        if (arryItem[j, 5] == null)
                            detail.Politics = null;
                        else
                            detail.Politics = (string)arryItem[j, 5];

                        if (!string.IsNullOrEmpty((string)arryItem[j, 6]) /*&& !((string)arryItem[j, 6] == "无")*/)
                        {
                            detail.Honors = new List<string>();
                            string honors = (string)arryItem[j, 6];
                            string[] honorarr = honors.Split('\n');
                            for (int l = 0; l < honorarr.Length; l++)
                            {
                                detail.Honors.Add(honorarr[l]);
                            }
                        }
                        else
                        {
                            detail.Honors = null;
                        }
                        //detail.Introduction = null;
                        if (arryItem[j, 7] == null)
                            detail.Introduction = null;
                        else
                            detail.Introduction = (string)arryItem[j, 7];

                        detail.IsImportant = isimportment;

                        detail.TextureName = texsName[k];


                        if (arryItem[j, 8] != null)
                        {
                            if (detail.Name == "虞广生")
                            {
                                Console.WriteLine("");
                            }

                            string reult = FindAudio(audioPath, detail.Name);
                            detail.AudioName = reult;
                        }
                        else
                        {
                            detail.AudioName = null;
                        }

                        detail.VideoName = null;

                        detail.Standby1 = null;
                        detail.Standby2 = null;
                        detail.Standby3 = null;

                        Score scroe = new Score();
                        scroe.Id = index;
                        scroe.LikeCount = 100;
                        //Console.WriteLine(inss);
                        //inss++;
                        lists.Add(detail);
                        listscores.Add(scroe);
                        index++;
                    }
                }
            }
            //excel.Quit();
            //excel = null;
            //Process[] procs = Process.GetProcessesByName("WPS Spreadsheets(32位)");
            //foreach (Process pro in procs)
            //{
            //    pro.Kill();//没有更好的方法,只有杀掉进程
            //}
            //GC.Collect();
        }

        /// <summary>
        /// 找图片名字
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static List<string> FindTex(string path, string name)
        {
            List<string> texs = new List<string>();
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (FileInfo fileInfo in info.GetFiles())
            {
                if (fileInfo.Name.Contains(name))
                {
                    string[] nameArr = fileInfo.Name.Split('.');
                    nameArr[0] = nameArr[0].Trim();
                    if (nameArr[0].Length == name.Length)
                    {
                        texs.Add(fileInfo.Name);
                    }
                }
            }
            return texs;
        }

        private static string FindAudio(string path, string name)
        {
            if (path == null)
                return null;
            DirectoryInfo info = new DirectoryInfo(path);
            string result = null;
            foreach (FileInfo fileInfo in info.GetFiles())
            {
                if (fileInfo.Name.Contains(name))
                {
                    string[] nameArr = fileInfo.Name.Split('.');
                    result = nameArr[0] + ".wav";
                    break;
                }
            }

            return result;
        }

        private static void Obj2jSON(string jsonpath, object obj)
        {
            string json = JsonMapper.ToJson(obj);
            File.WriteAllText(jsonpath, json);
        }
        private static void RemaneTex(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            foreach (DirectoryInfo directoryInfo in info.GetDirectories())
            {
                for (int i = 0; i < directoryInfo.GetFiles().Length; i++)
                {
                    char[] c = new[] { '\\' };
                    string[] names = directoryInfo.GetFiles()[i].FullName.Split(c);
                    string newname = null;
                    for (int j = 0; j < names.Length - 2; j++)
                    {
                        newname += names[j] + "\\";
                    }
                    newname += directoryInfo.Name + 3 + ".jpg";
                    directoryInfo.GetFiles()[i].MoveTo(newname);
                }
            }
        }


        #region 徐汇变迁

        private static void OpenXHExcel(string excelPath, string jsonpath)
        {
            object missing = System.Reflection.Missing.Value;
            Application excel = new Application();//lauch excel application
            excel.Visible = false;
            excel.UserControl = true;
            // 以只读的形式打开EXCEL文件
            Workbook wb = excel.Application.Workbooks.Open(excelPath, missing, true, missing, missing, missing, missing, missing, missing, true, missing, missing, missing, missing, missing);
            //取得第一个工作薄
            Worksheet ws = (Worksheet)wb.Worksheets.get_Item(1);

            int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
            int columnsint = ws.UsedRange.CurrentRegion.Columns.Count;//得到列数
            Console.WriteLine("行数 " + rowsint);
            Console.WriteLine("列数 " + columnsint);

            List<Data_Building> list = new List<Data_Building>();
            for (int i = 2; i < rowsint + 1; i++)
            {
                Range rng1 = ws.Cells.get_Range("A" + i, "P" + i);   //item
                object[,] arryItem = (object[,])rng1.Value2;
                int row = arryItem.GetLength(0);    //1
                for (int j = 1; j < row + 1; j++)
                {
                    Data_Building build = new Data_Building();
                    list.Add(build);
                    build.Id = int.Parse(arryItem[j, 1].ToString());
                    build.Identification = arryItem[j, 2].ToString();
                    build.MinYears = int.Parse(arryItem[j, 3].ToString());
                    build.MaxYears = int.Parse(arryItem[j, 4].ToString());
                    build.Type = arryItem[j, 5] == null ? null : arryItem[j, 5].ToString();
                    build.Name = arryItem[j, 6].ToString();
                    build._IdentificationMap = arryItem[j, 7] == null ? "0" : arryItem[j, 7].ToString();
                    build.PictureLocationX = int.Parse(arryItem[j, 8] == null ? "0" : arryItem[j, 8].ToString());
                    build.PictureLocationY = int.Parse(arryItem[j, 9] == null ? "0" : arryItem[j, 9].ToString());
                    build.PopUpsPointX = int.Parse(arryItem[j, 10] == null ? "0" : arryItem[j, 10].ToString());
                    build.PopUpsPointY = int.Parse(arryItem[j, 11] == null ? "0" : arryItem[j, 11].ToString());
                    build.PopUpsImageSizeX = int.Parse(arryItem[j, 12] == null ? "0" : arryItem[j, 12].ToString());
                    build.PopUpsImageSizeY = int.Parse(arryItem[j, 13] == null ? "0" : arryItem[j, 13].ToString());
                    build._PopUpsImage = arryItem[j, 14] == null ? null : arryItem[j, 14].ToString();
                    build._InnerImaPath = new List<string>();
                    string[] paths = arryItem[j, 15].ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < paths.Length; k++)
                    {
                        build._InnerImaPath.Add(paths[k]);
                    }
                    build.InnerTxt = new List<string>();
                    string[] txts = arryItem[j, 16].ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < txts.Length; k++)
                    {
                        build.InnerTxt.Add(txts[k]);
                    }


                }
            }

            string str = JsonConvert.SerializeObject(list);
            File.WriteAllText(jsonpath, str);
        }

        #endregion

    }

}

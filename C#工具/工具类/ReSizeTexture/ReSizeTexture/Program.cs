using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace ReSizeTexture
{
    class Program
    {
        static void Main(string[] args)
        {
            //string srcPath =
            //    @"D:\Desktop\常规项目\魔墙\08_武汉光谷企业魔墙\MQ\Assets\Resources\Texture\Texture\光电子信息产业\中国信息通信科技集团有限公司\1、【高速光器件测试】中国信科集团（烽火科技）研发人员正在对光系统核心芯片进行检测-烽火集团提供.JPG";
            //string desFold = @"D:\Desktop\新建文件夹 (3)";
            //Image_Resize(srcPath, desFold, 8192);

            Direct_Resize(@"D:\Desktop\常规项目\魔墙\08_武汉光谷企业魔墙\MQ\Assets\Resources\Texture\Texture", @"D:\Desktop\Texture2",
                8192, @"D:\Desktop\TextureLog.txt");

            Console.WriteLine("完成");
            Console.Read();
        }
        /// <summary>
        /// 图片改变尺寸
        /// </summary>
        /// <param name="srcPath">图片原始位置</param>
        /// <param name="desFold">目标目录</param>
        /// <param name="maxSize"></param>
        public static void Image_Resize(string srcPath, string desFold, int maxSize, string logFilePath)
        {
            if (!Directory.Exists(desFold))
                Directory.CreateDirectory(desFold);
            if (string.IsNullOrEmpty(logFilePath))
            {
                Console.WriteLine("日志路径不存在 " + logFilePath);
                return;
            }
            // 使maxsize保持4的倍数
            maxSize = maxSize / 4 * 4;
            string fileName = srcPath.Split('\\').Reverse().First();
            try
            {
                Image img = Image.FromFile(srcPath);
                int width = 0;
                int height = 0;
                if (img.Width >= img.Height)
                {
                    width = Math.Max(1, img.Width > maxSize ? maxSize : img.Width / 4 * 4);
                    height = Math.Max(1, ((int)(1f * width / img.Width * img.Height)) / 4 * 4);
                }
                else
                {
                    height = Math.Max(1, img.Height > maxSize ? maxSize : img.Height / 4 * 4);
                    width = Math.Max(1, ((int)(1f * img.Width * height / img.Height)) / 4 * 4);
                }
                Image newImg = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(newImg);
                g.DrawImage(img, 0, 0, width, height);
                newImg.Save(Path.Combine(desFold, fileName));
                g.Dispose();
                img.Dispose();
                newImg.Dispose();
            }
            catch (Exception e)
            {
                if (!File.Exists(logFilePath))
                {
                    File.Create(logFilePath);
                }
                File.AppendAllText(logFilePath, "错误路径： " + srcPath + "\n\n");
            }
        }
        /// <summary>
        /// 对某一目录下的图片统一修改尺寸
        /// </summary>
        /// <param name="direPath"></param>
        /// <param name="desFold"></param>
        /// <param name="maxSize"></param>
        /// <param name="logFile"></param>
        public static void Direct_Resize(string direPath, string desFold, int maxSize,string logFile)
        {
            if (!Directory.Exists(direPath))
            {
                Console.WriteLine("原始目录不存在 " + direPath);
            }
            if (!Directory.Exists(desFold))
            {
                Directory.CreateDirectory(desFold);
            }
            DirectoryInfo info = new DirectoryInfo(direPath);
            int loadcount = 0;
            int allcount = 0;
            foreach (FileInfo fileInfo in info.GetFiles("*", SearchOption.AllDirectories))
            {
                if (!fileInfo.Name.Contains(".meta"))
                {
                    allcount++;
                }
            }
            foreach (FileInfo fileInfo in info.GetFiles("*", SearchOption.AllDirectories))
            {
                if (!fileInfo.Name.Contains(".meta"))
                {
                    int startIndex = info.FullName.Length;
                    int endIndex = fileInfo.FullName.LastIndexOf("\\");
                    int length = endIndex - startIndex + 1;
                    Image_Resize(fileInfo.FullName, desFold + fileInfo.FullName.Substring(info.FullName.Length, length), maxSize, logFile);
                    loadcount++;
                    Console.WriteLine("进度 " + loadcount + "/" + allcount);
                }
            }
        }
    }
}

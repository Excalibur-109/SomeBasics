using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace ConvertEncoding
{
    internal class Program
    {
        // F:\Projects\ExcaliburFramework\Excalibur\Assets
        static string[] filter = new string[] { ".cs", ".txt", ".xml", ".json", ".op" };
        static Encoding targetEncoding = Encoding.UTF8;
        const int TASK_HANDLE_NUM = 20;

        static void Main(string[] args)
        {
            Console.Title = "Encoding to utf-8 with bom";
            for (int i = 0; i < 3; ++i)
            {
                Console.WriteLine("转换前确定相关文件关闭！");
            }
            Console.WriteLine();
            Console.WriteLine("可转换文件类型：");
            for (int i = 0; i < filter.Length; ++i)
            {
                Console.Write(filter[i] + "  ");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("输入路径，并按Enter键开始查找：");
            string path = Console.ReadLine();

            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            List<string> fileList = new List<string>();
            for (int i = 0; i < files.Length; ++i)
            {
                string f = files[i];
                if (f.Contains(".meta")) { continue; }
                if (CanConvert(f))
                {
                    fileList.Add(f);
                    //using (StreamReader reader = new StreamReader(File.Open(f, FileMode.Open, FileAccess.Read)))
                    //{
                    //    if (reader.CurrentEncoding != targetEncoding)
                    //    {
                    //        fileList.Add(f);
                    //    }
                    //    reader.Dispose();
                    //}
                }
            }

            Console.WriteLine("正在查找...");

            if (fileList != null && fileList.Count > 0)
            {
                Console.WriteLine("找到文件：");
                for (int i = 0; i < fileList.Count; ++i)
                {
                    Console.WriteLine(fileList[i]);
                }
                ConsoleKeyInfo info;
                Console.WriteLine();
                Console.Write("转换这些文件(key -> y:do/anyelse:exit):");
                info = Console.ReadKey();
                if (info.Key == ConsoleKey.Y)
                {
                    Convert(fileList.ToArray());
                }
            }
            else
            {
                Console.WriteLine("未找到可转换文件, 按任意键退出...");
                Console.ReadKey();
            }
        }

        static bool CanConvert(string file)
        {
            for (int i = 0; i < filter.Length; ++i)
            {
                if (file.EndsWith(filter[i]))
                {
                    return true;    
                }
            }
            return false;
        }

        static async void Convert(string[] files)
        {
            int slice = files.Length / TASK_HANDLE_NUM;
            int remain = files.Length - slice * TASK_HANDLE_NUM;
            List<Task> taskes = new List<Task>();
            int from, to = 0;
            for (int i = 0; i < slice - 1; ++i)
            {
                from = i * TASK_HANDLE_NUM;
                to = from + TASK_HANDLE_NUM;
                List<string> list = new List<string>();
                for (int k = from; k < to; ++k)
                {
                    list.Add(files[k]);
                }
                taskes.Add(ConvertEncoding(list.ToArray()));
            }
            for (int i = to; i < to + remain; ++i)
            {
                List<string> list = new List<string>();
                list.Add(files[i]);
                taskes.Add(ConvertEncoding(list.ToArray()));
            }
            await Task.WhenAll(taskes);
            Console.WriteLine();
            Console.WriteLine("文件编码转换完成。");
            Console.WriteLine();
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }

        static async Task ConvertEncoding(string[] paths)
        {
            for (int i = 0; i < paths.Length; ++i)
            {
                string path = paths[i];
                string content = File.ReadAllText(path);
                using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Open, FileAccess.Write), targetEncoding))
                {
                    await writer.WriteAsync(content);
                }
            }
        }
    }
}

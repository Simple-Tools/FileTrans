using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTrans
{
    class Program
    {
        static void Main(string[] args)
        {
            var key = Console.ReadLine();
            if (key == "1")
            {
                FilesToBase64();
            }
            else
            {
                Base64FileToFile();
            }
        }

        private static void Base64FileToFile()
        {
            var files = Directory.GetFiles("./");
            foreach (String file in files)
            {
                if (file.EndsWith(".txt"))
                {
                    String fileString = Base64ToFile(file);
                    BackToFile(file, fileString);
                }
            }
        }

        private static void BackToFile(string file, string fileString)
        {
            using (var fs = new FileStream(file.Substring(0, file.Length - 4) + ".rar", FileMode.Create, FileAccess.Write))
            {
                fileString = fileString.Replace("!!!!!!AxCdwerqwv4324afdasfASf", "");
                byte[] bts = Convert.FromBase64String(fileString);
                fs.Write(bts, 0, bts.Length);
                fs.Flush();
            }
        }

        private static string Base64ToFile(string file)
        {
            String fileContent = string.Empty;
            using (FileStream filestream = new FileStream(file, FileMode.Open))
            {
                byte[] bts = new byte[filestream.Length];
                filestream.Read(bts, 0, bts.Length);
                for (int i = 0; i < bts.Length; i++)
                {
                    //bts[i] = Convert.ToByte((bts[i] >> 1) & 0xff);
                    bts[i] = Convert.ToByte((bts[i] - 99) & 0xff);
                }
                fileContent = Encoding.UTF8.GetString(bts);
                filestream.Close();
            }
            return fileContent;
        }

        private static void FilesToBase64()
        {
            var files = Directory.GetFiles("./");
            foreach (String file in files) {
                if (file.EndsWith(".rar") || file.EndsWith(".zip"))
                {
                    String fileString = FileToBase64(file);
                    SaveToFile(file, fileString);
                }
            }
        }

        private static void SaveToFile(String fileName, String content)
        {
            using (var fs = new FileStream(fileName.Substring(0,fileName.Length-4)+".txt", FileMode.Create, FileAccess.Write))
            {
                Byte[] bts = Encoding.UTF8.GetBytes(content);
                for (int i=0; i<bts.Length; i++) {
                    //bts[i] = Convert.ToByte((bts[i] << 1) & 0xff);
                    bts[i] = Convert.ToByte((bts[i] + 99) & 0xff);
                }
                fs.Write(bts, 0, bts.Length);
                fs.Flush();
            }
        }
        private static String FileToBase64(String file)
        {
            string base64Str = string.Empty;
            using (FileStream filestream = new FileStream(file, FileMode.Open))
            {
                byte[] bt = new byte[filestream.Length];
                filestream.Read(bt, 0, bt.Length);
                base64Str = Convert.ToBase64String(bt);
                filestream.Close();
            }
            base64Str = "!!!!!!AxCdwerqwv4324afdasfASf" + base64Str + "!!!!!!AxCdwerqwv4324afdasfASf";
            return base64Str;
        }
    }
}

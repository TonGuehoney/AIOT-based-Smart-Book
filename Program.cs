using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BookMessageSysTem
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
         
            //①调用人脸识别窗口
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new Form2());
            //Application.Run(new Form1());
            //Application.Run(new Form3());


            //ProcessStartInfo psi = new ProcessStartInfo();
            //psi.FileName = "cmd";
            ////psi.CreateNoWindow = false;

            //psi.CreateNoWindow = true;

            ////psi.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //psi.UseShellExecute = false;
            //psi.RedirectStandardInput = true;
            //psi.RedirectStandardOutput = true;
            //Process p = new Process();
            //p.StartInfo = psi;

            //p.Start();
            ////p.StandardInput.WriteLine($"cd {AppDomain.CurrentDomain.BaseDirectory}");
            ////p.StandardInput.WriteLine("exit");
            //p.StandardInput.WriteLine("conda activate face42");
            //p.StandardInput.WriteLine(@"python C:\Users\tongx\Desktop\FaceREC\UI.py");
            //p.StandardInput.WriteLine("exit");

            //p.StandardInput.AutoFlush = true;



















            /*

                                    //string str1 = File.ReadAllText(@"C:\Users\tongx\Desktop\AIoT-BookMSystem\Tongx.txt");
                        using (FileStream fsRead = new FileStream(@"C:\Users\tongx\Desktop\AIoT-BookMSystem\Tongx.txt", FileMode.OpenOrCreate, FileAccess.Read))
                        {
                            byte[] buffer = new byte[1024 * 1024 * 3];   //3M大小
                        //返回本次实际读取到的有效字节数
                            int r = fsRead.Read(buffer, 0, buffer.Length);
                        //将字节数组中的每一个元素按照给定的编码格式解码成字符串
                            string data = Encoding.UTF8.GetString(buffer, 0, r);
                            Console.WriteLine(data);                     

                            if (data.Length == 0)  //         C#线程 定时器 每隔多久 
                            {
                                Console.WriteLine("亲爱的用户：无法识别到你的信息！请您先录入您的信息！");
                            }
                            else if (data.Length == 9)
                            {
                                Console.WriteLine("亲爱的用户：无法识别到你的信息！请您先录入您的信息！");
                            }
                            else
                            {
                                Application.Run(new Form1());
                            }
                                        //识别控制台输出的id，再打开图书管理系统。
                        }

                       */




        }
    }
}

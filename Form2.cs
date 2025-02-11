using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using  System.Collections;
using  System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Timers;


namespace BookMessageSysTem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd";
            //psi.CreateNoWindow = false;

            psi.CreateNoWindow = true;

            //psi.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            psi.UseShellExecute = false;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardOutput = true;
            Process p = new Process();
            p.StartInfo = psi;

            p.Start();
            //p.StandardInput.WriteLine($"cd {AppDomain.CurrentDomain.BaseDirectory}");
            //p.StandardInput.WriteLine("exit");
            p.StandardInput.WriteLine("conda activate face42");
            p.StandardInput.WriteLine(@"python C:\Users\tongx\Desktop\FaceREC\UI.py");
            p.StandardInput.WriteLine("exit");

            p.StandardInput.AutoFlush = true;





            //string data = File.ReadAllText(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt");
            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Enabled = true;
            //timer.Interval = 1000; //执行间隔时间,单位为毫秒; 这里实际间隔为10分钟  
            //timer.Start();
            //timer.Elapsed += new System.Timers.ElapsedEventHandler(data);

            Thread thread = new Thread(new ThreadStart(Method1));
            thread.Start();
            void Method1()
            {
                while (true)
                {
                    Thread.Sleep(1000); //阻止设定时间
                    if (!System.IO.File.Exists(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt"))
                    {
                        continue;

                    }
                    string data = File.ReadAllText(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt");
                    if (data.Contains("unknown") == true)   //有known即 报错
                        {
                        Form3 a = new Form3();
                        a.ShowDialog();
                        if (File.Exists(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt"))
                            File.Delete(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt");
                    }
                    else
                    {
                        Form1 f = new Form1();
                        f.ShowDialog();
                        if (File.Exists(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt"))
                            File.Delete(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt");
                    }
                   
                }
            }

            //string data = File.ReadAllText(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt");


            //using (FileStream fsRead = new FileStream(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt", FileMode.OpenOrCreate, FileAccess.Read))
            //{
            //    byte[] buffer = new byte[1024 * 1024 * 3];   //3M大小
            //                                                 //返回本次实际读取到的有效字节数
            //    int r = fsRead.Read(buffer, 0, buffer.Length);
            //    //将字节数组中的每一个元素按照给定的编码格式解码成字符串
            //    string data = Encoding.UTF8.GetString(buffer, 0, r);
            //    Console.WriteLine(data);

            //    if (data.Length == 0)  //         C#线程 定时器 每隔多久 

            //    {
            //        Form3 a = new Form3();
            //        a.ShowDialog();
            //    }
            //    else if (data.Length == 9)
            //    {
            //        Form3 a = new Form3();
            //        a.ShowDialog();
            //    }
            //    else
            //    {
            //        Form1 f = new Form1();
            //        f.ShowDialog();
            //    }
            //    //识别控制台输出的id，再打开图书管理系统。  Form3可删除

            //}
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string userid = txtid.Text.ToString();
            string password = txtpassword.Text.ToString();

            //用户名或密码为空的情况，此处界面中要有label标签显示提示信息
            if (userid.Equals("") || password.Equals(""))
            {
                lblLoginError.Text = "用户名或密码不能为空";
            }
            //用户名或密码不为空的情况   
            else
            {
                string connectionString = "server=.;database=PEOPLE;integrated security=SSPI";
                SqlConnection SqlCon = new SqlConnection(connectionString); //数据库连接
                SqlCon.Open(); //打开数据库
                string sql = "Select * from peopletable where PEOPLEID='" + userid + "' and PEOPLEKEY='" + password + "'";//查找用户sql语句
                SqlCommand cmd = new SqlCommand(sql, SqlCon);
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr;
                sdr = cmd.ExecuteReader();
                if (sdr.Read())         //从结果中找到
                {
                    //信息验证成功，跳转到主界面frmLogin(自己所建立的界面，用textBox显示验证信息也可，自己灵活设置)，关闭登录界面；
                    //此三行代码常用于实现winform窗体之间的切换；其前两行是必须的
                    Form1 main = new Form1();
                    main.Show();
                    this.Hide();
                }
                //输入用户名和密码错误的情况
                else
                {
                    lblLoginError.Text = "用户名或密码错误,提示";
                    return;
                }
            }
            //Form1 f = new Form1();
            //f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 f = new Form5();
            f.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            f.Show();
            this.Hide();
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {
            txtpassword.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpassword.PasswordChar = '\0';   //显示输入
            }
            else
            {
                txtpassword.PasswordChar = '*';   //显示*
            }
        }
    }
}



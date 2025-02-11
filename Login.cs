using BookMessageSysTem.Dal;
using BookMessageSysTem.Model;
using BookMessageSysTem.Untils;
using ISO15693DLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookMessageSysTem
{
    public partial class Login : Form
    {

        //初始化一个Reader对象
        ISO15693DLL.Reader iso_Reader = new ISO15693DLL.Reader();
        //寻到的卡片数目
        private Int32 TagCount = 0;
        //寻到的卡片号
        private String[] TagNumber = null;
        public Login()
        {
            
        }

        public Login(Form1 form1)
        {
            InitializeComponent();
            Form1 = form1;
        }

       
        private void Login_Load(object sender, EventArgs e)
        {
            IsStop = true;
            Login.CheckForIllegalCrossThreadCalls = false;
            open();
           
        }



        private void open() {
           
            string strPort = "COM101";
            Byte value = iso_Reader.OpenSerialPort(strPort, Convert.ToInt32("115200"));
            if (value == 0x00)
            {

                MessageBox.Show(string.Format("打开串口:[{0}],成功!，波特率为:[{1}]", strPort, "115200"));
                Thread.Sleep(1000);
                xunKa();
            }
            else
            {
                MessageBox.Show(string.Format("打开串口[{0}],失败！", strPort));
            }
          }

        Thread th = null;
            private void xunKa()
            {
            /*Byte value;
            value = iso_Reader.Inventory(ModulateMethod.ASK, InventoryModel.Single, ref TagCount, ref TagNumber);
            if (value == 0x00)
            {
                MessageBox.Show(String.Format("寻得标签数量:{0}个，标签号为：{1}", TagCount, TagNumber[0]));
            Console.WriteLine(TagNumber[0]);
            Console.ReadLine();
            }
            else
            {
                MessageBox.Show("单卡寻找失败!未寻找到卡！");
            }*/
             th = new Thread(AutoRun);
            th.IsBackground = true;
            th.Start();
        }

        private List<string> _listTag = new List<string>();
       
        //定义变量用于控制自动寻卡循环是否继续进行
        private Boolean IsStop = true;
        //寻多卡的循环方法
        private void AutoRun()
        {
            Byte value;
            while (IsStop)
            {
                value = iso_Reader.Inventory(ModulateMethod.ASK, InventoryModel.Multiple, ref TagCount, ref TagNumber);
                if (value == 0)
                {
                 //   MessageBox.Show(String.Format("寻得标签数量:{0}个，标签号为：{1}", TagCount, TagNumber[0]));
                    Console.WriteLine(String.Format("寻得标签数量:{0}个，标签号为：{1}", TagCount, TagNumber[0]));
                    Console.ReadLine();
                    IsStop = false;
                    textBox1.Text = TagNumber[0];
                }
                else
                {
                    //ShowList("Inventory Lose!No TagNumber");
                    Console.WriteLine("自动寻卡失败!未发现卡片！");
                    
                }
            }
        }

        UserDal userDal = new UserDal();

        public Form1 Form1 { get; }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            User user = userDal.SelectUserByCardId(TagNumber[0]);
            if (user == null)
            {
                textBox1.Text = "";
                IsStop = true;
                return;
            }
            else {
                //存入全局变量
                IsStop = false;
                GlobalVariable.USRE = user;
                //this.Hide();
              
                this.Close();
                string str = user.Tag == "1" ? "管理员" : "学生";
                MessageBox.Show($"身份验证成功！\n亲爱的【{str}】：{user.CardID}，你好！ 正在为你跳转！", "消息对话框");

            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
           // th.Suspend();
            iso_Reader.CloseSerialPort();
            Form1.intoUserInfo();
        }
    }
}

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
using System.Data.SqlClient;

using System.Collections;

using System.Diagnostics;
using System.IO;

namespace BookMessageSysTem
{
    public partial class LendBook : Form
    {

        Reader iso_Reader = new Reader();
        //寻到的卡片数目
        private Int32 TagCount = 0;
        //寻到的卡片号
        private String[] TagNumber = null;
        //声明一个卡片集合用于存放不同卡号的卡片数据
        public List<string> _listTag = new List<string>();
        public LendBook()
        {
            LendBook.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //Load();
            UserInfo();
            open();
        }
        private void open()
        {

            string strPort = "COM101";
            Byte value = iso_Reader.OpenSerialPort(strPort, Convert.ToInt32("115200"));
            if (value == 0x00)
            {

                MessageBox.Show(string.Format("打开串口:[{0}],成功!，波特率为:[{1}]", strPort, "115200"));
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

            th = new Thread(AutoRun);
            th.IsBackground = true;
            th.Start();
        }

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
                    for (int i = 0; i < TagNumber.Length; i++)
                    {
                        Console.WriteLine(String.Format("卡片数:{0},卡号:{1}", TagCount, TagNumber[i]));
                        Console.ReadLine();
                        if (!_listTag.Contains(TagNumber[i]))
                        {
                            _listTag.Add(TagNumber[i]);
                            IntoLendBookInfo(TagNumber[i]);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("自动寻卡失败!未发现卡片！");
                }
            }
        }

        BookDal bookBal = new BookDal();
       
        public void IntoLendBookInfo(String str) {
            Book book = bookBal.SelectBookByCardId(str);
            if (book != null) {
                LendBooksTable lendBooksTable = new LendBooksTable((int.Parse(countNumber.Text) + 1), book, this);
                if (flowLayoutPanel1.InvokeRequired)
                {
                    flowLayoutPanel1.Invoke(new MethodInvoker(delegate
                    {
                        flowLayoutPanel1.Controls.Add(lendBooksTable);
                    }));
                }
                else
                {
                    flowLayoutPanel1.Controls.Add(lendBooksTable);
                }
                countNumber.Text = (int.Parse(countNumber.Text) + 1).ToString();
            }

        }




        //初始化用户信息
        private void UserInfo() {
            sno_label.Text = GlobalVariable.USRE.Sno;
            name_label.Text = GlobalVariable.USRE.Name;
            sex_label.Text = GlobalVariable.USRE.Sex;
            cname_label.Text = GlobalVariable.USRE.Cname;
            sfz.Text = GlobalVariable.USRE.IdentifyCardID;
        }


        public void setCountNumber(int number) {
            countNumber.Text = number.ToString();
            if (number == 0)
            {
                btnStartIn.Enabled = false;
                btnStartIn.BackColor = Color.Silver;
            }

            if( btnStartIn.Enabled == false && number > 0 ) {
                btnStartIn.Enabled = true; 
                btnStartIn.BackColor = Color.CornflowerBlue;
            }
        }
        public int getCountNumber() {
            return int.Parse(countNumber.Text);
        }

        public void removeLendBookTable(LendBooksTable lab) {
            flowLayoutPanel1.Controls.Remove(lab);
        }
        public void addLendBookTable()
        {
            LendBooksTable lendBooksTable = new LendBooksTable();
            flowLayoutPanel1.Controls.Add(lendBooksTable);
        }


        public void updateIndex(int index) {
            foreach (Control con in flowLayoutPanel1.Controls) {
                if (con.Controls.Find("label1", true)[0].Text != "") {
                    int aa = int.Parse(con.Controls.Find("label1", true)[0].Text);
                    if (aa > index)
                    {
                        con.Controls.Find("label1", true)[0].Text = (aa - 1).ToString();
                    }
                }
            }
          
        }

        public void Load() 
        {
            flowLayoutPanel1.Controls.Clear();
            int i = 0;
            for (; i < 5; i++) {
                Book book = new Book();
                book.BookName = "书名" + i;
               LendBooksTable lendBooksTable = new LendBooksTable(i+1,book,this);
                flowLayoutPanel1.Controls.Add(lendBooksTable);
            }
            setCountNumber(i);
            for (; i <= 10;i++) {
                LendBooksTable lendBooksTable = new LendBooksTable();
                flowLayoutPanel1.Controls.Add(lendBooksTable);
            }
           
        }

        OrderDal orderDal = new OrderDal();
         
        //点击确认阅读
        private void btnStartIn_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (Control con in flowLayoutPanel1.Controls) { 
                Order order = con.Tag as Order;
                if (order != null) {
                   order.LendTime = DateTime.Now.ToLocalTime().ToString();
                   int a = orderDal.Insert(order);
                    if (a > 0) 
                    { 
                        i++;
                    }
                }
            }
            if (i == 0)
            {
                MessageBox.Show("请放置借阅的书本！");
            }
            else
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
                            MessageBox.Show("身份认证错误！");
                            if (File.Exists(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt"))
                                File.Delete(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt");
                        }
                        else
                        {
                            MessageBox.Show("借阅成功！数量：" + i + "本！");
                            //flowLayoutPanel1.Controls.Clear();
                            this.Close();
                            if (File.Exists(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt"))
                                File.Delete(@"C:\Users\tongx\Desktop\AIoT Designer\AIoT-BookMSystem\Tongx.txt");
                        }
                    }
                }
            }
           
           
        }
        private void LendBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsStop = false;
            iso_Reader.CloseSerialPort();
        }

        private void LendBook_Load(object sender, EventArgs e)
        {

        }

        private void LendBook_Load_1(object sender, EventArgs e)
        {

        }
    }
}

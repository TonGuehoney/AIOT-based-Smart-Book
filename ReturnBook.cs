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
    public partial class ReturnBook : Form
    {

        Reader iso_Reader = new Reader();
        //寻到的卡片数目
        private Int32 TagCount = 0;
        //寻到的卡片号
        private String[] TagNumber = null;
        //声明一个卡片集合用于存放不同卡号的卡片数据
        public List<string> _listTag = new List<string>();
        Form1 _Form1;

        public ReturnBook() {
            InitializeComponent();
            LendBook.CheckForIllegalCrossThreadCalls = false;
            UserInfo();
        }
        public ReturnBook(Form1 form1)
        {
            InitializeComponent();
            _Form1 = form1;
            LendBook.CheckForIllegalCrossThreadCalls = false;
            UserInfo();
        }


        public void updateBooksInfo() { 
            _Form1.LoadAllBooks();
        }
        private void UserInfo()
        {
            sno_label.Text = GlobalVariable.USRE.Sno;
            name_label.Text = GlobalVariable.USRE.Name;
            sex_label.Text = GlobalVariable.USRE.Sex;
            cname_label.Text = GlobalVariable.USRE.Cname;
            sfz.Text = GlobalVariable.USRE.IdentifyCardID;
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
        OrderDal orderDal = new OrderDal();
        public void IntoLendBookInfo(String str)
        {
            Order order = orderDal.SelectOrderByBookCardId(str,GlobalVariable.USRE.CardID);
            if (order != null)
            {
                order.User = GlobalVariable.USRE;
                ReturnBookTable lendBooksTable = new ReturnBookTable((int.Parse(countNumber.Text) + 1), order, this);
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


        public void setCountNumber(int number)
        {
            countNumber.Text = number.ToString();
            if (number == 0)
            {
                btnStartIn.Enabled = false;
                btnStartIn.BackColor = Color.Silver;
            }

            if (btnStartIn.Enabled == false && number > 0)
            {
                btnStartIn.Enabled = true;
                btnStartIn.BackColor = Color.CornflowerBlue;
            }
        }
        public int getCountNumber()
        {
            return int.Parse(countNumber.Text);
        }

        public void removeLendBookTable(ReturnBookTable lab)
        {
            flowLayoutPanel1.Controls.Remove(lab);
        }

        public void updateIndex(int index)
        {
            foreach (Control con in flowLayoutPanel1.Controls)
            {
                if (con.Controls.Find("label1", true)[0].Text != "")
                {
                    int aa = int.Parse(con.Controls.Find("label1", true)[0].Text);
                    if (aa > index)
                    {
                        con.Controls.Find("label1", true)[0].Text = (aa - 1).ToString();
                    }
                }
            }

        }

        //窗口关闭
        private void ReturnBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            iso_Reader.CloseSerialPort();
            IsStop = false;
        }
        
        private void btnStartIn_Click(object sender, EventArgs e)
        {
           

        }
    }
}

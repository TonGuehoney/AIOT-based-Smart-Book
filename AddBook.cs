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
using BookMessageSysTem.Dal;
using BookMessageSysTem.Model;
using ISO15693DLL;

namespace BookMessageSysTem
{
    public partial class AddBook : Form
    {

        ISO15693DLL.Reader iso_Reader = new ISO15693DLL.Reader();
        //寻到的卡片数目
        private Int32 TagCount = 0;
        //寻到的卡片号
        private String[] TagNumber = null;
        BookDal bookDal = new BookDal();
        public AddBook()
        {
            InitializeComponent();
        }

        public AddBook(Form1 form1)
        {
            IsStop = true;
            InitializeComponent();
            Form1 = form1;
            open();
        }

        public Form1 Form1 { get; }

        private void open()
        {

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
                    Console.WriteLine(String.Format("寻得标签数量:{0}个，标签号为：{1}", TagCount, TagNumber[0]));
                    Console.ReadLine();
                    IsStop = false;
                    cmbCardID.Text = TagNumber[0];
                   /* th.Suspend();
                    iso_Reader.CloseSerialPort();*/
                }
                else
                {
                    Console.WriteLine("自动寻卡失败!未发现卡片！");
                }
            }
        }




        private void btnStartIn_Click(object sender, EventArgs e)
        {
            if (this.cmbCardID.Text == "")
            {
                MessageBox.Show("图书标签号不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (this.txtBookname.Text == "")
            {
                MessageBox.Show("图书名称不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.txtSeat.Text == "")
            {
                MessageBox.Show("图书位置不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.typeBook.Text == "")
            {
                MessageBox.Show("图书类型不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                Book boo = new Book();
                boo.BookID = cmbCardID.Text.Trim();    //将控件中的值赋给变量
                boo.BookName = txtBookname.Text.Trim();
                boo.BookSeat = txtSeat.Text.Trim();
                boo.BookStyle = typeBook.Text.Trim();
                boo.BookAuthor = bookAuthor.Text.Trim();

                if (bookDal.CheckBook(boo.BookID))
                {
                    MessageBox.Show("该图书已入库!");
                    this.Close();
                    return;
                }
                int i = bookDal.InsertBook(boo);                           //通过SqlHelper的对象调用Insert方法将用户信息存入数据库
                //= sh.IsSuccess();                //通过数据库中表的受影响行数判断登记信息是否成功
                if (i > 0)
                {
                    MessageBox.Show("入库成功!");
                    Form1.LoadAllBooks();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("数据库错误!入库失败!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsStop = true;
            cmbCardID.Text = "";
            txtBookname.Text = "";
            bookAuthor.Text = "";
            typeBook.Text = "";
            txtSeat.Text = "";
        }

        private void AddBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            iso_Reader.CloseSerialPort();
            IsStop = false;
        }

        private void AddBook_Load(object sender, EventArgs e)
        {

        }
    }
}

using BookMessageSysTem.Dal;
using BookMessageSysTem.Model;
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
    public partial class AddUser : Form
    {

        ISO15693DLL.Reader iso_Reader = new ISO15693DLL.Reader();
        //寻到的卡片数目
        private Int32 TagCount = 0;
        //寻到的卡片号
        private String[] TagNumber = null;
        public AddUser()
        {
            InitializeComponent();
            IsStop = true;
            open();
        }

        public AddUser(Form1 form1)
        {
            InitializeComponent();
            Form1 = form1;
            IsStop = true;
            open();
        }

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
                    txtAccessID.Text = TagNumber[0];
                }
                else
                {
                    Console.WriteLine("自动寻卡失败!未发现卡片！");
                }
            }
        }
        UserDal  userDal =  new UserDal();

        public Form1 Form1 { get; }

        //添加学生
        private void btnStartIn_Click(object sender, EventArgs e)
        {

            if (this.txtAccessID.Text == "")
            {
                MessageBox.Show("门禁卡号不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (this.txtStaffName.Text == "")
            {
                MessageBox.Show("学生姓名不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.cmbStaffSex.Text == "")
            {
                MessageBox.Show("学生性别不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (this.txtIDNO.Text == "")
            {
                MessageBox.Show("身份证不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (this.txtSno.Text == "")
            {
                MessageBox.Show("学生学号错误", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (this.txtCname.Text == "")
            {
                MessageBox.Show("学生班级不能为空", "消息对话框", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                User user = new User();
                user.CardID = txtAccessID.Text.Trim();    //将控件中的值赋给变量
                user.Name = txtStaffName.Text.Trim();
                user.Sex = cmbStaffSex.Text.Trim();
                user.IdentifyCardID = txtIDNO.Text.Trim();
                user.Sno = txtSno.Text.Trim();
                user.Cname = txtCname.Text.Trim();
                if (userDal.CheckEmployees(user.CardID))
                {
                    MessageBox.Show("该卡已登记!");
                    this.Close();
                    return;
                }
             int i =    userDal.Insert(user);                           //通过SqlHelper的对象调用Insert方法将用户信息存入数据库
                             //通过数据库中表的受影响行数判断登记信息是否成功
                if (i > 0)
                {
                    MessageBox.Show("登记信息成功!");
                    Form1.IntoFlowLayoutByUserInfo(userDal.SelectAllUser());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("数据库错误!登记信息失败!");
                }
            }

        }

        private void AddUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            iso_Reader.CloseSerialPort();
            IsStop = false;
        }

        private void AddUser_Load(object sender, EventArgs e)
        {

        }
    }
}

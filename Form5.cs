using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BookMessageSysTem
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userID = textsignid.Text.Trim();
            //连接数据库
            //设置连接字符串
            string constr = "server=.;database=PEOPLE;integrated security=SSPI";
            SqlConnection mycon = new SqlConnection(constr);                  //实例化连接对象
            mycon.Open();
            //新注册的用户是否存在
            SqlCommand checkCmd = mycon.CreateCommand();       //创建SQL命令执行对象
            string s = "select PEOPLEID from peopletable where PEOPLEID='" + userID + "'" ;
            checkCmd.CommandText = s;
            SqlDataAdapter check = new SqlDataAdapter();       //实例化数据适配器
            check.SelectCommand = checkCmd;                    //让适配器执行SELECT命令
            DataSet checkData = new DataSet();                 //实例化结果数据集
            int n = check.Fill(checkData, "register");         //将结果放入数据适配器，返回元祖个数
            if (n != 0)
            {
                lblsignerror.Text = "用户名存在";
                textsignid.Text = ""; textsignsfzid.Text = ""; textsignpsw.Text = "";
            }

            //确认密码
            else if (textensignpsw.Text != textsignpsw.Text)
            {
                lblsignerror.Text = "密码不一致！";
            }
            else if (textsignid.Text == ""|| textsignsfzid.Text == ""|| textsignpsw.Text == "" || textensignpsw.Text == "")
            {
                lblsignerror.Text = "请将信息填完整";
            }
            else
            {
                //插入数据
                string s1 = "insert into peopletable(PEOPLEID,PEOPLESFZ,PEOPLEKEY) values ('" + textsignid.Text + "','" + textsignsfzid.Text + "','" + textensignpsw.Text + "')";          //编写SQL命令

                SqlCommand mycom = new SqlCommand(s1, mycon);      //初始化命令
                mycom.ExecuteNonQuery();   //执行语句
                mycon.Close();             //关闭连接
                mycom = null;
                mycon.Dispose();           //释放对象s
                Form2 main = new Form2();
                main.Show();
                this.Hide();
                this.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //  this.Hide();//隐藏上一个界面
            Close();
            new Form2().ShowDialog();//调用窗体
                                     //  this.Dispose();//释放所有资源
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}

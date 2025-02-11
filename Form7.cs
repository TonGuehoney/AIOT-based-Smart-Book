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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //  this.Hide();//隐藏上一个界面
            Close();
            new Form2().ShowDialog();//调用窗体
                                     //  this.Dispose();//释放所有资源
        }

        private void btn_Downstep_Click(object sender, EventArgs e)
        {
            //先查找用户，之后核对身份证号，若一致则输出密码
            string userid = UID.Text;
            string idc = IDC.Text;
            string Sqlstr = "server=.;database=PEOPLE;integrated security=SSPI";
            SqlConnection conn = new SqlConnection(Sqlstr);
            conn.Open();
            SqlDataAdapter findid = new SqlDataAdapter("select*from peopletable where PEOPLEID='" + userid + "' and PEOPLESFZ='" + idc + "'", conn);
            DataSet FindId = new DataSet();
            findid.Fill(FindId);
            conn.Close();
            if (FindId.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("用户不存在！");
            }
            else
            {
                if (FindId.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("身份证号错误！");
                }
                else
                {
                    PWD.Text = FindId.Tables[0].Rows[0][1].ToString();
                }
            }
        }

        private void IDC_TextChanged(object sender, EventArgs e)
        {

            IDC.PasswordChar = '*';
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                IDC.PasswordChar = '\0';   //显示输入
            }
            else
            {
                IDC.PasswordChar = '*';   //显示*
            }
        }
    }
}


//        private void colorchange(object sender, MouseEventArgs e)
//        {
//            button3.BackColor = Color.Red;
//        }

//        private void colorback(object sender, EventArgs e)
//        {
//            button3.BackColor = Color.Transparent;
//        }

//        private void button3_Click(object sender, EventArgs e)
//        {
//            Application.Exit();
//        }
//    }
//}
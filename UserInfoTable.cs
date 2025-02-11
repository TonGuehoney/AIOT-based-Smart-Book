using BookMessageSysTem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookMessageSysTem
{
    public partial class UserInfoTable : UserControl
    {

        public string _index { get; }
        public User _User { get; }
        public UserInfoTable()
        {
            InitializeComponent();
        }

        public UserInfoTable(string v, User user)
        {
            InitializeComponent();
            _index = v;
            _User = user;
            into();
        }

        public void into() { 
            label1.Text = _index.ToString();
            label2.Text = _User.Sno.ToString();
            label3.Text = _User.Name.ToString();
            label4.Text = _User.IdentifyCardID.ToString();
            label5.Text = _User.Cname.ToString();
            label6.Text = _User.Tag.ToString() == "0" ? "普通用户":"管理员";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BookMessageSysTem.Dal;
using BookMessageSysTem.Model;
using BookMessageSysTem.Untils;
using System.Diagnostics;

namespace BookMessageSysTem
{
    public partial class Form1 : Form
    {

        User _user = null;
        BookDal bookDal = new BookDal();


        public Form1()
        {
            InitializeComponent();
            LoadAllBooks();
            intoUserInfo();
            this_Lab = label7;
        }

        //  写一个API 调用API 实现 人脸识别后 生成一个id  使得通过识别id成功,门禁自动打开，人脸识别不成功，门禁不动。
        //最后识别成功后进入图书管理系统进行借阅操作。
        /*
          ①可以这样来解决：
          当人脸识别录入的时候，
          生成用户id，然后把id打印在控制台，
          C#是可以读取到控制台输出的内容，
          这样就能把id和人员进行绑定。
          识别的时候流程是一样的。
          可以通过这种形式，当然还有其他形式。
         */
        /*
          ②还有一种解决方案：
          人脸识别系统成功识别之后，
          会在指定的目录下写一个文件，
          C#程序扫描（每隔100毫秒读取一次这个文件目录下的文件），
          这样也可以。
         */


        public void LoadAllBooks() {
            flowLayoutPanel4.Hide();
            flowLayoutPanel2.Show();
            panel6.Hide();
            panel4.Show();
            List<Book> list = bookDal.SelectAllBook();
            IntoFlowLayoutPanel_Main(list);
            ResCondition();
        }

        UserDal userDal = new UserDal();
        public void LoadAllUser()
        {
            flowLayoutPanel4.Show();
            flowLayoutPanel2.Hide();
            panel6.Show();
            panel4.Hide();
            List<User> list = userDal.SelectAllUser();
            IntoFlowLayoutByUserInfo(list);
            ResCondition();
        }

        public void intoUserInfo() {

            if (GlobalVariable.USRE == null)
            {
                button1.Show();
                flowLayoutPanel1.Hide();
                panel5.Hide();
                panel3.Hide();
            }
            else {
                flowLayoutPanel1.Show();
                if (GlobalVariable.USRE.Tag == "0")
                {
                    panel5.Hide();
                    panel3.Hide();
                }
                else {
                    panel5.Show();
                    panel3.Show();
                }
               
                button1.Hide();
                label3.Text = GlobalVariable.USRE.CardID;
                label2.Text = GlobalVariable.USRE.Tag == "1" ? "管理员：" : "普通用户";
            }

        }

        public void IntoFlowLayoutPanel_Main(List<Book> list) {
            CountNumber.Text = list.Count.ToString();
            flowLayoutPanel_Main.Controls.Clear();
            for (int i = 0; i < list.Count(); i++)
            {
                BookInfoTable b = new BookInfoTable((i+1).ToString(), list[i]);
                flowLayoutPanel_Main.Controls.Add(b);
            }
        }

        //初始化用户信息
        public void IntoFlowLayoutByUserInfo(List<User> list) {
            textBox1.Text = list.Count.ToString();
            flowLayoutPanel_Main.Controls.Clear();
            for (int i = 0; i < list.Count(); i++)
            {
                UserInfoTable b = new UserInfoTable((i + 1).ToString(), list[i]);
                flowLayoutPanel_Main.Controls.Add(b);
            }
        }

        //点击查询
        private void button2_Click(object sender, EventArgs e)
        {
            Book books = new Book();
            books.BookName = SearchBookName.Text;
            books.BookSeat = SearchZuoZhe.Text;
            books.BookStyle = SearchBookType.Text;
            if (books.BookName.Trim() == "" && books.BookSeat.Trim() == "" && books.BookStyle.Trim() == "")
            {
                return;
            }
            else {
                List<Book> list = bookDal.SelectAllBookByLike(books);
                IntoFlowLayoutPanel_Main(list);
            }
        }
        //点击重置
        private void button3_Click(object sender, EventArgs e)
        {
            LoadAllBooks();
        }

        public void ResCondition() {
            SearchBookName.Text = "";
            SearchZuoZhe.Text = "";
            SearchBookType.Text = "";
        }

        //添加图书
        private void label12_Click(object sender, EventArgs e)
        {
            LoadAllBooks();
            setCss(label10);
            AddBook addBook = new AddBook(this);
            addBook.ShowDialog();
            setCss(label12);
        }



        //点击借书
        private void label8_Click(object sender, EventArgs e)
        {
            LoadAllBooks();
            setCss(label8);
            if (GlobalVariable.USRE != null)
            {
                LendBook lendBook = new LendBook();
                lendBook.ShowDialog();
                LoadAllBooks();
            }
            else {
                MessageBox.Show("身份没有验证前无法进行借阅！");
            }
           
            setCss(label7);
        }

        //点击还书
        private void label9_Click(object sender, EventArgs e)
        {
            LoadAllBooks();
            setCss(label9);
            if (GlobalVariable.USRE != null)
            {
                ReturnBook returnBook = new ReturnBook(this);
                returnBook.ShowDialog();
            }
            else
            {
                MessageBox.Show("身份没有验证前无法进行还书！");
            }
            setCss(label7);
        }

        private void label29_Click(object sender, EventArgs e)
        {
            LoadAllUser();
            setCss(label27);
            AddUser addUser = new AddUser(this);
            addUser.ShowDialog();
            setCss(label29);

        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login(this);
            login.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            GlobalVariable.USRE = null;
            intoUserInfo();
        }

        Label this_Lab;
        private void setCss(Label lab)
        {

            this_Lab.BackColor = Color.FromArgb(204, 213, 240);
            this.this_Lab.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lab.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lab.BackColor = Color.FromArgb(247, 249, 254);
            this_Lab = lab;
        }

        private void label29_Click_1(object sender, EventArgs e)
        {
            LoadAllUser();
            setCss(label29);
        }

        private void label12_Click_1(object sender, EventArgs e)
        {
            LoadAllBooks();
            setCss(label12);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            LoadAllBooks();
            setCss(label7);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.Sno = textBox4.Text.Trim();
            user.Name = textBox3.Text.Trim();
            user.Cname = textBox2.Text.Trim();

            if (user.Sno != "" || user.Name != "" || user.Cname != "") {
                List<User> list = userDal.SelectAllUserByLike(user);
                IntoFlowLayoutByUserInfo(list);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            LoadAllUser();
            setCss(label29);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel_Main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

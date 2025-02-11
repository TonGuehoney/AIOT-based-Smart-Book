using BookMessageSysTem.Model;
using BookMessageSysTem.Untils;
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
    public partial class BookInfoTable : UserControl
    {

        public string _index { get; }
        public Book _Book { get; }
        public BookInfoTable()
        {
            
        }

        public BookInfoTable(string v, Book book)
        {
            InitializeComponent();
            _index = v;
            _Book = book;
            Into();
        }

        public void Into() {
            label1.Text = _index;
            label2.Text = _Book.BookName;
            label3.Text = _Book.BookAuthor;
            label4.Text = _Book.BookStyle;
            label5.Text = _Book.BookSeat;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GlobalVariable.USRE != null)
            {
                LendBook returnBook = new LendBook();
                returnBook.ShowDialog();
            }
            else
            {
                MessageBox.Show("身份没有验证前无法进行还书！");
            }
        }
    }
}

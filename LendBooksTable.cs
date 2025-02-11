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
    public partial class LendBooksTable : UserControl
    {
        public int _index { get; }
        public Book _Book { get; }
        public LendBook LendBook { get; }

        public LendBooksTable()
        {
            InitializeComponent();
            dateTimePicker1.Hide();
            button1.Hide();
        }

        public LendBooksTable(int v, Book book)
        {
            InitializeComponent();
            _index = v;
            _Book = book;
            into();
            this.Tag = order;
        }

        public LendBooksTable(int v, Book book, LendBook lendBook) : this(v, book)
        {
            LendBook = lendBook;
        }
        Order order = null;
        public void into() {
            label1.Text = _index.ToString();
            label2.Text = _Book.BookName;
            label3.Text = _Book.BookAuthor;
            order = new Order();
            order.Book = _Book;
            order.User = GlobalVariable.USRE;
            order.ReturnTime = dateTimePicker1.Value.ToString();
            this.Tag = order;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LendBook.setCountNumber(LendBook.getCountNumber() - 1);
            LendBook.updateIndex(int.Parse(label1.Text));
            LendBook.removeLendBookTable(this);
            
            for (int i = 0; i < LendBook._listTag.Count(); i++) {
                if (LendBook._listTag[i] == _Book.BookID)
                {
                    LendBook._listTag.Remove(LendBook._listTag[i]);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            order.ReturnTime = dateTimePicker1.Value.ToString();
            this.Tag = order;
        }
    }
}

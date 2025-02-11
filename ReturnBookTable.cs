using BookMessageSysTem.Dal;
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


    public partial class ReturnBookTable : UserControl
    {

        public int _index { get; }
        public Order _Order { get; }
        public Book _Book;
        public ReturnBook ReturnBook { get; }
        public ReturnBookTable()
        {
            InitializeComponent();
        }

        public ReturnBookTable(int v, Order order, ReturnBook returnBook)
        {
            InitializeComponent();
            _index = v;
            _Order = order;
            _Book = _Order.Book;
            ReturnBook = returnBook;
            into();
        }
        public void into()
        {
            label1.Text = _index.ToString();
            label2.Text = _Book.BookName;
            label3.Text = _Book.BookAuthor;
            label4.Text = _Order.LendTime;
            this.Tag = _Order;
        }


        OrderDal orderDal = new OrderDal();
        private void button1_Click(object sender, EventArgs e)
        {
            string str = DateTime.Now.ToLocalTime().ToString();
            int a =  orderDal.UpdateOrderState("0",_Order.Cid,str);
           
            if (a > 0) {
                MessageBox.Show($"归还成功！\n书名：<<{_Order.Book.BookName}>>\n归还日期：<<{str}>>");
                ReturnBook.setCountNumber(ReturnBook.getCountNumber() - 1);
                ReturnBook.updateIndex(int.Parse(label1.Text));
                ReturnBook.removeLendBookTable(this);
                ReturnBook.updateBooksInfo();
                for (int i = 0; i < ReturnBook._listTag.Count(); i++)
                {
                    if (ReturnBook._listTag[i] == _Book.BookID)
                    {
                        ReturnBook._listTag.Remove(ReturnBook._listTag[i]);
                    }
                }
                ReturnBook.updateBooksInfo();
            }
            
        }

       
    }
}

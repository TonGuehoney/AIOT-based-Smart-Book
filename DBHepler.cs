using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BookMessageSysTem
{
    public class DBHepler
    {
        public static string roomnum;
        public static string constr = "server=.;database=PEOPLE;integrated security=SSPI";
        //数据库连接对象
        public static SqlConnection conn = null;
        //初始化数据库连接
        public static void InitConnection()
        {
            //如果连接对象不存在，则创建连接
            if (conn == null)
            {
                conn = new SqlConnection(constr);
            }
            //如果连接对象关闭，则打开连接
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            //如果连接中断，则重启连接
            if (conn.State == ConnectionState.Broken)
            {
                conn.Close();
                conn.Open();
            }
        }
        //获得数据表集
        public static DataSet GetDataSet(string strSQL)
        {
            InitConnection();
            SqlCommand objMyCom = new SqlCommand(strSQL, conn);
            SqlDataAdapter da = new SqlDataAdapter(objMyCom);
            DataSet dt = new DataSet();
            da.Fill(dt);
            return dt;
        }
        //查询，获取DataReader
        public static SqlDataReader GetDataReader(string sqlstr)
        {
            InitConnection();
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            //CommandBehavior.CloseConnecetion 命令行为：当DataReader对象关闭时自动关闭占用的连接对象
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        //查询，获取DataTable
        public static DataTable GetDataTable(string sqlStr)
        {
            InitConnection();
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(sqlStr, conn);
            dap.Fill(dt);
            conn.Close();
            return dt;
        }

        //增删改
        public static bool ExecuteNonQuery(string sqlstr)
        {
            InitConnection();
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result > 0;
        }
        //执行集合函数
        public static object ExecuteScalar(string sqlstr)
        {
            InitConnection();
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            object result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
}

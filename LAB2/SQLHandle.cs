using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Project
{
    class SQLHandle
    {
        public static SqlConnection getConnection()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string str = config.GetConnectionString("MyConStr");
            return new SqlConnection(str);
        }

        public static DataTable getData(string cmd, SqlParameter[] parameter)
        {
            SqlCommand command = new SqlCommand(cmd, getConnection());
            if (parameter != null) command.Parameters.AddRange(parameter);
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            return dt;
        }

        public static int SQLExecute(string cmd, SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(cmd, getConnection());
            if (parameters != null) command.Parameters.AddRange(parameters);
            command.Connection.Open();
            int k = command.ExecuteNonQuery();
            command.Connection.Close();
            return k;
        }

        public static List<Student> ToStuList(DataTable dt)
        {
            List<Student> stuList = new List<Student>();
            foreach (DataRow row in dt.Rows)
            {

                stuList.Add(new Student(Convert.ToInt32(row[0]),
                    row[1].ToString(),
                    Convert.ToBoolean(row[2]),
                    Convert.ToDateTime(row[3]),
                    row[4].ToString(),
                    Convert.ToBoolean(row[5]),
                    Convert.ToSingle(row[6])
                    ));
            }
            return stuList;
        }

        public static List<Student> getAllStudent()
        {
            string cmd = "select * from Student";
            DataTable table = getData(cmd, null);
            return ToStuList(table);
        }

        public static List<Major> ToMajorList(DataTable dt)
        {
            List<Major> list = new List<Major>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Major(row[0].ToString(), row[1].ToString()));
            }
            return list;
        }

        public static List<Major> getAllMajor()
        {
            string cmd = "select * from Major";
            DataTable dt = getData(cmd, null);
            return ToMajorList(dt);
        }
    }
}

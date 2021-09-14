using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdoDemo
{
    class Connecting
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader dr;
        public static void CreateConnection()
        {
            string constr = "Data Source = DESKTOP-TMI53VD; Initial Catalog = Techretsu; Integrated Security = true; User ID = sa; Password=pass@123";
            con = new SqlConnection();
            con.ConnectionString = constr;
        }
        public static void InsertData(string name, string email, string contactno, int departmentid, string address)
        {
            con.Open();
            /*
            string query = "insert into Employee values ('" + name + "','" + email + "','" + contactno + "'," + departmentid + ",'" + address + "')";
            cmd = new SqlCommand(query, con);
            */
            string query = "insert into Employee values(@n,@e,@cn,@d,@addr)";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("n", name));
            cmd.Parameters.Add(new SqlParameter("e", email));
            cmd.Parameters.Add(new SqlParameter("cn", contactno));
            cmd.Parameters.Add(new SqlParameter("d", departmentid));
            cmd.Parameters.Add(new SqlParameter("addr", address));

            int r = cmd.ExecuteNonQuery();
            Console.WriteLine("{0} of rows affected", r);
            con.Close();
        }
        public static void DisplayData()
        {
            con.Open();
            string query = "Select * from Employee";
            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            Console.WriteLine("Name \t Email");
            while (dr.Read())
            {
                Console.WriteLine("{0} \t {1} ", dr["Name"], dr["Email"]);

            }
            dr.Close();
            string cmdcount = "Select count(*) from Employee";
            cmd = new SqlCommand(cmdcount, con);
            int count = (int)cmd.ExecuteScalar();
            Console.WriteLine("{0} Records in the table", count);

            con.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            string email;
            string contact;
            int dept;
            string address;

            Connecting.CreateConnection();

            Connecting.DisplayData();
            Console.Write("Enter Name: ");
            name = Console.ReadLine();
            Console.Write("Enter Email: ");
            email = Console.ReadLine();
            Console.Write("Enter Contact: ");
            contact = Console.ReadLine();
            Console.Write("Enter Department: ");
            dept = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Address: ");
            address = Console.ReadLine();
            Connecting.InsertData(name, email, contact, dept, address);
            Connecting.DisplayData();
            Console.ReadKey();
        }
    }
}

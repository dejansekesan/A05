using System.Data;
using System.Data.SqlClient;

namespace A05
{
    class Konekcija
    {
        public static SqlCommand GetCommand()
        {
            SqlConnection con = new SqlConnection("Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=produzeniboravak;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
    }
}
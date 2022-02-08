using System.Data.SqlClient;

namespace TesteWebMotors.DataBase
{
    public class DataBaseManager
    {
        public static SqlConnection GetConnection()
        {
            var sb = new SqlConnectionStringBuilder();
            sb["Connection Timeout"] = "3000";
            sb["Initial Catalog"] = "teste_webmotors";
            sb["Data Source"] = "localhost,1433";
            sb["User ID"] = "sa";
            sb["Password"] = @"10be15r575859FP";
            sb["Integrated Security"] = "False";
            sb["Encrypt"] = "False";

            return new SqlConnection(sb.ToString());
        }

    }
}

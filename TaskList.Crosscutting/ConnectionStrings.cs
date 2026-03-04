using System;
using System.Collections.Generic;
using System.Text;

namespace TaskList.Crosscutting
{
    public static class ConnectionStrings
    {
        public static string GetConnectionString()
        {
            return "Server=(localdb)\\MSSQLLocalDB;Database=master;Integrated Security=True;TrustServerCertificate=True;";
        }
    }
}

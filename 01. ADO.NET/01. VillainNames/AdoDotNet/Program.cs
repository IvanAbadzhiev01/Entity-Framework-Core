using AdoDotNet;
using System.Data.SqlClient;

const string __connectionString = "Server=LAPTOP-9CAVEGVR\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";

using SqlConnection dbCon = new SqlConnection(__connectionString);
await dbCon.OpenAsync();


using SqlCommand getCom = new SqlCommand(SqlQueries.__query, dbCon);

using SqlDataReader sqlDr = getCom.ExecuteReader();

while(await sqlDr.ReadAsync())
{
    Console.WriteLine($"{sqlDr["Name"]} - {sqlDr["TotalMinions"]}");
}



using _02.AddMinion;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Xml.Linq;

const string _connectionString = "Server=LAPTOP-9CAVEGVR\\SQLEXPRESS;Database=MinionsDB;Integrated Security=true";



using SqlConnection? dbCon = new SqlConnection(_connectionString);
try
{
    await dbCon.OpenAsync();

    string minionInfoRaw = Console.ReadLine();
    string valianInfoRaw = Console.ReadLine();

    string minionainfo = minionInfoRaw.Substring(minionInfoRaw.IndexOf(":") + 1).Trim();
    string valianName = valianInfoRaw.Substring(valianInfoRaw.IndexOf(":") + 1).Trim();

    await AddMinion(minionainfo, valianName, dbCon);

}
finally
{
    dbCon?.Dispose();
}

static async Task AddMinion(string minionainfo, string valianName, SqlConnection? dbCon)
{
    string[] minionData = minionainfo.Split(' ');
    string minionName = minionData[0];
    int minionAge = int.Parse(minionData[1]);
    string minionTown = minionData[2];
    #region Town
    SqlCommand cmdGetTownId = new SqlCommand(SqlQyery.GetTownByName, dbCon);
    cmdGetTownId.Parameters.AddWithValue("@townName", minionTown);

    object? townResult = await cmdGetTownId.ExecuteScalarAsync();
    int townId = -1;
    if (townResult is null)
    {
       using SqlCommand cmdAddTown = new SqlCommand(SqlQyery.AddTown, dbCon);
        cmdAddTown.Parameters.AddWithValue("@townName", minionTown);
        townId = Convert.ToInt32(await cmdAddTown.ExecuteScalarAsync());
        await Console.Out.WriteLineAsync($"Town {minionTown} was added to tehe database");
    }
    else
    {
        townId = (int)townResult;
    }



    #endregion

    #region Villan
    using SqlCommand cmdGetVillain = new SqlCommand(SqlQyery.GetVilainByName, dbCon);
    cmdGetVillain.Parameters.AddWithValue("@Name", valianName);
    var villainResult = await cmdGetVillain.ExecuteScalarAsync();

    int villainId = -1;
    if (villainResult is null)
    {
       using SqlCommand cmdInsertNewVallain = new SqlCommand(SqlQyery.AddVilain, dbCon);
        cmdInsertNewVallain.Parameters.AddWithValue("@villainName", valianName);
        villainId = Convert.ToInt32(await cmdInsertNewVallain.ExecuteScalarAsync());
        await Console.Out.WriteLineAsync($"Villain {valianName} was added to the database");
    }
    else
    {
        villainId = (int)villainResult;


    }
    #endregion

    #region Minion
   using SqlCommand cmdInsertMinion = new SqlCommand(SqlQyery.AddMinion, dbCon);
    cmdInsertMinion.Parameters.AddWithValue("@name", minionName);
    cmdInsertMinion.Parameters.AddWithValue("@age", minionAge);
    cmdInsertMinion.Parameters.AddWithValue("@townId", townId);
    await Console.Out.WriteLineAsync($"Minions {minionName} was adedd to database");
    int minionId = Convert.ToInt32(await cmdInsertMinion.ExecuteScalarAsync());

    using SqlCommand cmdInsertMV = new SqlCommand(SqlQyery.insertMv, dbCon);
    await cmdInsertMV.ExecuteNonQueryAsync();
    await Console.Out.WriteLineAsync($"Successfully Added {minionName} was added as servant to {valianName}");




    #endregion
}
// See https://aka.ms/new-console-template for more information
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Reflection.Metadata;

string jsonFile = "data.json";
var json = await File.ReadAllTextAsync(jsonFile);
var data = JsonConvert.DeserializeObject<MMProverbsModel>(json);

string connectionString = "Server=.;Database=Btach4MiniPOS;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
using var connection = new SqlConnection(connectionString);
await connection.OpenAsync();

string insertMMProverbsTitleSql = @"
            INSERT INTO Tbl_Mmproverbstitle (TitleId, TitleName)
            VALUES (@TitleId, @TitleName);";

foreach (var title in data.Tbl_MmProverbsTitle)
{
   await connection.ExecuteAsync(insertMMProverbsTitleSql, title);
}

string insertMMProverbsSql = @"
            INSERT INTO Tbl_Mmproverbs (TitleId, ProverbId, ProverbName, ProverbDesp)
            VALUES (@TitleId, @ProverbId, @ProverbName, @ProverbDesp);";

foreach (var mmproverb in data.Tbl_MmProverbs)
 {
    await connection.ExecuteAsync(insertMMProverbsSql, mmproverb);
 }


Console.WriteLine("Data inserted successfully!");
Console.ReadLine();

public class MMProverbsModel
{
    public Tbl_Mmproverbstitle[] Tbl_MmProverbsTitle { get; set; }
    public Tbl_Mmproverbs[] Tbl_MmProverbs { get; set; }
}

public class Tbl_Mmproverbstitle
{
    public int TitleId { get; set; }
    public string TitleName { get; set; }
}

public class Tbl_Mmproverbs
{
    public int TitleId { get; set; }
    public int ProverbId { get; set; }
    public string ProverbName { get; set; }
    public string ProverbDesp { get; set; }
}


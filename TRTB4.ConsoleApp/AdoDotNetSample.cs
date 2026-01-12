using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TRTB4.ConsoleApp;

internal class AdoDotNetSample
{
    private SqlConnectionStringBuilder sb;
    public AdoDotNetSample()
    {
        sb = new SqlConnectionStringBuilder();
        sb.DataSource = ".";
        sb.InitialCatalog = "Btach4MiniPOS";
        sb.UserID = "sa";
        sb.Password = "sasa@123";
        sb.TrustServerCertificate = true;
    }
    public void Read()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);


        connection.Open();

        SqlCommand cmd = new SqlCommand("select * from tbl_product", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        connection.Close();

        foreach (DataRow dr in dt.Rows)
        {
            string productName = Convert.ToString(dr["ProductName"]);
            decimal price = Convert.ToDecimal(dr["Price"]);
            Console.WriteLine("Product Name : " + productName);
            Console.WriteLine($"Price : {price.ToString("n0")})");
        }

    }

    public void Edit()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        SqlCommand cmd = new SqlCommand("select * from tbl_product where productid =1", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);

        connection.Close();

        if (dt.Rows.Count == 0)
            return;
        DataRow dr = dt.Rows[0];

        string productName = Convert.ToString(dr["ProductName"]);
        decimal price = Convert.ToDecimal(dr["Price"]);
        Console.WriteLine("Product Name : " + productName);
        Console.WriteLine($"Price : {price.ToString("n0")})");


    }

    public void Create()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        string query = @"Insert INTO [dbo].[Tbl_Product]
                        ([ProductName]
                        ,[Price]
                        ,[Quantity]
                        ,[IsDelete]
                        ,[CreatedDateTime])
                    VALUES
                       ('Pineapple',4000,30,0,GETDATE())
                        ";
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();

        connection.Close();
        string message = result > 0 ? "Saving successfully." : "Saving failed.";
        Console.WriteLine(message);



    }

    public void Update()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        string query = @"UPDATE [dbo].[Tbl_Product]
                        SET [ProductName] = 'Orange'
                            ,[Price] = 5000
                            ,[Quantity] = 30
                          ,[IsDelete] = 0
                      
                     WHERE [ProductId] = 1";
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();


        string message = result > 0 ? "Update successfully." : "Update failed.";
        Console.WriteLine(message);

        connection.Close();

    }
    public void Delete()
    {
        SqlConnection connection = new SqlConnection(sb.ConnectionString);

        connection.Open();

        string query = @"DELETE FROM [dbo].[Tbl_Product]
      
                       WHERE [ProductId] = 8";
        SqlCommand cmd = new SqlCommand(query, connection);
        int result = cmd.ExecuteNonQuery();


        string message = result > 0 ? "Deleted successfully." : "Deleted failed.";
        Console.WriteLine(message);

        connection.Close();
    }
}

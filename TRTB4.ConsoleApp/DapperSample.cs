using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace TRTB4.ConsoleApp;

internal class DapperSample
{
    private SqlConnectionStringBuilder sb;
    public DapperSample()
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
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();
            string query = "select * from tbl_product";
            List<ProductDto> lists = db.Query<ProductDto>(query).ToList(); // execute

            foreach (ProductDto item in lists)
            {
                Console.WriteLine($"Product Name - {item.ProductName}, Price - {item.Price}, Quantity - {item.Quantity}  ");

            }
        }


    }

    public void Edit()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();
            string query = "select * from tbl_product where productid =3";
            ProductDto item = db.Query<ProductDto>(query).FirstOrDefault()!; // execute

            if (item is null) return;

            Console.WriteLine(item.ProductId);
            Console.WriteLine(item.ProductName);
            Console.WriteLine(item.Price);
            Console.WriteLine(item.Quantity);


        }
    }
    public void Create()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();
            string query = @"Insert INTO [dbo].[Tbl_Product]
                        ([ProductName]
                        ,[Price]
                        ,[Quantity]
                        ,[IsDelete]
                        ,[CreatedDateTime])
                    VALUES
                       ('JackFruit',4000,10,0,GETDATE())
                        ";
            int result = db.Execute(query); // execute

            string message = result > 0 ? "Insert successfully!" : "Insert failed.";
            Console.WriteLine(message);


        }
    }

    public void Update()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();
            string query = @"UPDATE [dbo].[Tbl_Product]
                        SET [ProductName] = 'Banana'
                            ,[Price] = 1000
                            ,[Quantity] = 60
                          ,[IsDelete] = 0
                      
                     WHERE [ProductId] = 3";
            int result = db.Execute(query); // execute
            string message = result > 0 ? "Update successful." : "Update failed.";
            Console.WriteLine(message);
        }
    }
    public void Delete()
    {
        using (IDbConnection db = new SqlConnection(sb.ConnectionString))
        {
            db.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Product]
      
                       WHERE [ProductId] = 7";
            int result = db.Execute(query); // execute
            string message = result > 0 ? "Delete successful." : "Delete failed.";
            Console.WriteLine(message);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using TRTB4.ConsoleApp.Database.AppDbContextModels;

namespace TRTB4.ConsoleApp
{
    internal class EfCoreV2Sample
    {
        private readonly AppDbContext _db;
        public EfCoreV2Sample() 
        {
            _db = new AppDbContext();
        }

        public void Read()
        {
            List<TblProduct> lst = _db.TblProducts.ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.ProductId);
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.Price);
                Console.WriteLine(item.Quantity);
            }
            Console.WriteLine("-----------------***-------------------");

        }
        public void Create()
        {
            TblProduct product = new TblProduct()
            {
                ProductName = "Tomato",
                Price = 1000,
                Quantity = 15,
                IsDelete = false,
                CreatedDateTime = DateTime.Now
            };
            _db.TblProducts.Add(product);
            int result = _db.SaveChanges();
            string message = result > 0 ? "Saving successful!" : "Saving Failed!";

            Console.WriteLine(message);
            Console.WriteLine("-----------------***-------------------");

        }
        public void Edit()
        {
            var item = _db.TblProducts.Where(x => x.ProductId == 10).FirstOrDefault();
            if (item is null) return;

            Console.WriteLine(item.ProductId);
            Console.WriteLine(item.ProductName);
            Console.WriteLine(item.Price);
            Console.WriteLine(item.Quantity);

            Console.WriteLine("-----------------***-------------------");

        }
        public void Update()
        {
            var item = _db.TblProducts.Where(x => x.ProductId == 4).FirstOrDefault();
            if (item is null) return;
            item.ProductName = "Mango";
            int result = _db.SaveChanges();

            string message = result > 0 ? "Updating successful!" : "Updating Failed!";
            Console.WriteLine(message);
            Console.WriteLine("-----------------***-------------------");

        }
        public void Delete()
        {
            var item = _db.TblProducts.Where(x => x.ProductId == 3).FirstOrDefault();
            if (item is null) return;
            item.IsDelete = true;
            int result = _db.SaveChanges();
            string message = result > 0 ? "Deleting successful!" : "Deleting Failed!";
            Console.WriteLine(message);
            Console.WriteLine("-----------------***-------------------");

        }
    }
}

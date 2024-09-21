using System;
using System.Collections.Generic;
using System.Linq;
using PetStore.Models;

namespace PetStore.Logic
{
    public class ProductLogic : IProductLogic
    {
        public List<Product> _products;

        public ProductLogic()
        {
            _products = new List<Product>
            {
                new DogLeash
                {
                    Description = "A rope dog leash made from strong rope.",
                    LengthInches = 60,
                    Material = "Rope",
                    Name = "Rope Dog Leash",
                    Price = 21.00m,
                    Quantity = 0
                },
                new DryCatFood
                {
                    Quantity = 6,
                    Price = 25.59m,
                    Name = "Plain 'Ol Cat Food",
                    Description = "Basic cat food for a healthy life",
                    WeightPounds = 10,
                    KittenFood = false
                },
                new CatFood
                {
                    Quantity = 48,
                    Price = 12.99m,
                    Name = "Fancy Cat Food",
                    Description = "Food that is delicious and made from fine ingredients",
                    KittenFood = false
                }
            };
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public List<Product> GetAllProducts()
        {
            return _products;
        }

        public T GetProductByName<T>(string name) where T : Product
        {
            return _products.OfType<T>().FirstOrDefault(x => x.Name == name);
        }

        public List<string> GetOnlyInStockProducts()
        {
            return _products.Where(p => p.Quantity > 0).Select(p => p.Name).ToList();
        }

        public decimal GetTotalPriceOfInventory()
        {
            return _products.Where(p => p.Quantity > 0).Sum(p => p.Price * p.Quantity);
        }
    }
}

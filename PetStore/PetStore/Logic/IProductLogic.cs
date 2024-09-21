using System.Collections.Generic;
using PetStore.Models;  // Make sure the Product class is referenced correctly

namespace PetStore.Logic
{
    public interface IProductLogic
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();
        T GetProductByName<T>(string name) where T : Product;
        List<string> GetOnlyInStockProducts();
        decimal GetTotalPriceOfInventory();
    }
}

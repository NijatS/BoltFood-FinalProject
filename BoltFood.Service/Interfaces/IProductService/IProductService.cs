using BoltFood.Core.Enums.Product;
using BoltFood.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Interfaces.IProductService
{
    public interface IProductService:IService<Product>
    {
        public Task<string> CreateAsync(string name,ProductCategory category , double price,int RestaurantID);
        public Task<string> UpdateAsync(int id,string name,ProductCategory category, double price, int RestaurantID);
        public Task<string> RemoveAsync(int id);
    }
}

using BoltFood.Core.Enums.Restaurant;
using BoltFood.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Interfaces.IRestaurantService
{
    public interface IRestaurantService : IService<Restaurant>
    {
        public Task<string> CreateAsync(RestaurantCategory category,string name);
        public Task<string> UpdateAsync(int id,RestaurantCategory category,string name);
        public Task<string> RemoveAsync(int id);
    }
}

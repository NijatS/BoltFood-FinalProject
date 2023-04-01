using BoltFood.Core.Enums.Restaurant;
using BoltFood.Core.Models;
using BoltFood.Data.Repositories.RestaurantRepository;
using BoltFood.Service.Extentions;
using BoltFood.Service.Interfaces;
using BoltFood.Service.Interfaces.IRestaurantService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Implementations.Services
{
    public class RestaurantService : IRestaurantService
    {
        private RestaurantRepository _repository = new RestaurantRepository();
        public async Task<string> CreateAsync(RestaurantCategory category,string name)
        {
            if (!name.CheckName())
            {
                return "Again!!!";
            }
           Restaurant restaurant = new Restaurant(category,name);
           Console.ForegroundColor = ConsoleColor.Green;
           await _repository.AddAsync(restaurant);
            return "Successfully Added...";
        }
        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Restaurant> GetAsync(int id)
        {
            Restaurant restaurant = await _repository.GetAsync(x=>x.id == id);
            if(restaurant == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Helper.WriteSlowLine("Restaurant is not found");
                return null;
            }
            return restaurant;
        }
        public async Task<string> RemoveAsync(int id)
        {
            Restaurant restaurant =await GetAsync(id);
            if(restaurant == null)
            {
                return "Again!!!";
            }
            await _repository.RemoveAsync(restaurant);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully Removed...";
        }
        public async Task<string> UpdateAsync(int id,RestaurantCategory category,string name)
        {
            Restaurant restaurant = await GetAsync(id);
            if (restaurant == null)
            {
                return "Again!!!";
            }
            if (!name.CheckName())
            {
                return "Again!!!";
            }
            restaurant.Name = name;
            restaurant.category = category;
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully Updated...";
        }
    }
}

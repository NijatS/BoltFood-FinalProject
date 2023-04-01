using BoltFood.Core.Enums.Product;
using BoltFood.Core.Models;
using BoltFood.Data.Repositories.RestaurantRepository;
using BoltFood.Service.Extentions;
using BoltFood.Service.Interfaces.IProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Implementations.Services
{
    public class ProductService : IProductService
    {
        private RestaurantRepository _repository = new RestaurantRepository();
        public async Task<string> CreateAsync(string name, ProductCategory category, double price, int RestaurantID)
        {
            if (!name.CheckName())
            {
                return "Again!!!";
            }
            if (price <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Price must be more than 0$";
            }
            Restaurant restaurant = await _repository.GetAsync(x => x.id == RestaurantID);
            if (restaurant == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Restaurant is not found";
            }
            Product product = new Product(name, price, category, restaurant);

            restaurant.Products.Add(product);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Added Successfully";
        }
        public async Task<List<Product>> GetAllAsync()
        {
            List<Restaurant> restaurants = await _repository.GetAllAsync();
            List<Product> products = new List<Product>();
            foreach (Restaurant restaurant in restaurants)
            {
                products.AddRange(restaurant.Products);
            }
            return products;
        }
        public async Task<Product> GetAsync(int id)
        {
            List<Restaurant> restaurants = await _repository.GetAllAsync();


            foreach (Restaurant restaurant in restaurants)
            {
                Product product = restaurant.Products.Find(x => x.id == id);
                if (product != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    return product;
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Helper.WriteSlowLine("Product is not found");
            return null;
        }
        public async Task<string> RemoveAsync(int id)
        {
            List<Restaurant> restaurants = await _repository.GetAllAsync();
            foreach (Restaurant restaurant in restaurants)
            {
                Product product = restaurant.Products.Find(x => x.id == id);
                if (product != null)
                {
                    restaurant.Products.Remove(product);
                    Console.ForegroundColor = ConsoleColor.Green;
                    return "Removed Successfully";
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            return "Product is not found";
        }
        public async Task<string> UpdateAsync(int id, string name, ProductCategory category, double price, int RestaurantID)
        {
            if (!name.CheckName())
            {
                return "Again!!!";
            }
            List<Restaurant> restaurants = await _repository.GetAllAsync();
            foreach (Restaurant restaurant in restaurants)
            {
                Product product = restaurant.Products.Find(x => x.id == id);
                if (product != null)
                {
                    Restaurant newRestaurant = await _repository.GetAsync(x=> x.id == RestaurantID);
                    if(newRestaurant == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        return "New Restaurant not found";
                    }
                    product.Name = name;
                    product.category = category;
                    product.Price = price;
                    product.restaurant = newRestaurant;
                    Console.ForegroundColor = ConsoleColor.Green;
                    return "Updated Successfully";
                }
            }
            Console.ForegroundColor = ConsoleColor.Red;
            return "Product is not found";
        }
    }
}
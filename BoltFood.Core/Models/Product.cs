using BoltFood.Core.Enums.Product;
using BoltFood.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Core.Models
{
    public class Product:BaseModel
    {
        private static int _id;
        public double Price { get; set; }
        public ProductCategory category { get; set; }
        public Restaurant restaurant { get; set; }

        public Product(string name,double price, ProductCategory category,Restaurant restaurant):base(name) 
        {
            _id++;
            id = _id;
            Price = price;
            this.category = category;
            this.restaurant = restaurant;
        }

        public override string ToString()
        {
            return "ID:" + id + "  Name: " + Name + "  Category: " + category + "  Price: " + Price + "  RestaurantName: " + restaurant.Name;
        }

    }
}

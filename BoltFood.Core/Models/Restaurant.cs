using BoltFood.Core.Enums.Restaurant;
using BoltFood.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Core.Models
{
    public class Restaurant:BaseModel
    {
        private static int _id;
        public RestaurantCategory category { get; set; }
        public List<Product> Products { get; set; }
        public Restaurant(RestaurantCategory category, string name): base(name) {
            _id++;
            id = _id;
            Products = new List<Product>(); 
            this.category = category;
        }
        public override string ToString()
        {
            return "ID:" + id + "  Name: " +Name+ "  Category: " + category;
        }
    }
}

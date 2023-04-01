using BoltFood.Core.Models;
using BoltFood.Core.Repositories.RestaurantRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Data.Repositories.RestaurantRepository
{
    public class RestaurantRepository:Repository<Restaurant>,IRestaurantRepositories
    {
    }
}

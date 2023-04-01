using BoltFood.Core.Enums.User;
using BoltFood.Core.Models;
using BoltFood.Core.Models.Base;
using BoltFood.Core.Repositories;
using BoltFood.Core.Repositories.RestaurantRepositories;
using BoltFood.Core.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Data.Repositories.UserRepository
{
    public class UserRepository: Repository<User>, IUserRepositories
    {
        public UserRepository() {
            User user = new User("Nijat Soltanov", "admin", "admin", UserCategory.SuperAdmin); //SuperAdmin 
            Context.Add(user);
        }
    }
}

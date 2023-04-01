using BoltFood.Core.Enums.Restaurant;
using BoltFood.Core.Enums.User;
using BoltFood.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Interfaces.IUserService
{
    public interface IUserService:IService<User>
    {
        public Task<string> CreateAsync(string name,string username,string password,UserCategory category,User user);
        public Task<string> UpdateAsync(int id,string name, string username, string password, UserCategory category,User user);
        public Task<User> CheckUserAsync(string username, string password);
        public Task<string> RemoveAsync(int id,User user);
    }
}

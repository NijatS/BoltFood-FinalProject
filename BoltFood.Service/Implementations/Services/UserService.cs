using BoltFood.Core.Enums.User;
using BoltFood.Core.Models;
using BoltFood.Core.Repositories.UserRepositories;
using BoltFood.Data.Repositories.UserRepository;
using BoltFood.Service.Extentions;
using BoltFood.Service.Interfaces.IUserService;

namespace BoltFood.Service.Implementations.Services
{
    public class UserService : IUserService
    {
        private  IUserRepositories _userRepository = new UserRepository();
        public async Task<string> CreateAsync(string name, string username, string password, UserCategory category,User user1)
        {   
            List<User> users = await _userRepository.GetAllAsync();
            if (!name.CheckName() || !username.CheckUserName() || !password.passwordCheck())
            {
                return "Again!!!";
            }
            foreach (User u in users)
            {
                if(u.UserName == username)
                {
                    return "Username var Qaqa,elave eliye bilmersen!!!";
                }
            }
            User user = new User(name,username,password,category);
            if (user.category == UserCategory.Admin && user1.category == UserCategory.Admin)
            {
                if (user.category == UserCategory.Admin && user1.category == UserCategory.Admin)
            {
                return "Admin Qaqa admini add eliye bilmersen!!!";
            }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            await _userRepository.AddAsync(user);
            return "Successfully Added...";
        }
        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        public async Task<User> CheckUserAsync(string username,string password)
        {
            List<User> users = await _userRepository.GetAllAsync();
            foreach (User user in users)
            {
                if(user.UserName == username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
        public async Task<User> GetAsync(int id)
        {
            User user = await _userRepository.GetAsync(x => x.id == id);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Helper.WriteSlowLine("Staff is not founded");
                return null;
            }
            return user;
        }
        public async Task<string> RemoveAsync(int id,User user)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            User user1 = await GetAsync(id);
            if (user1 == null)
            {
                return "Again!!!";
            }
            if(user1.id == 1)
            {
                return "SuperAdmini sile bilmersen qaqa";
            }
            if (user.category == UserCategory.Admin && user1.category == UserCategory.Admin)
            {
                return "Admin Qaqa admini sile bilmersen!!!";
            }
            await _userRepository.RemoveAsync(user1);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully Removed...";
        }
        public async Task<string> UpdateAsync(int id, string name, string username, string password, UserCategory category,User user1)
        {
            List<User> users = await _userRepository.GetAllAsync();
            User user = await GetAsync(id);
            if (!name.CheckName() || !username.CheckUserName() || !password.passwordCheck() || user == null)
            {
                return "Again!!!";
            }
            if (user.category == UserCategory.Admin && user1.category == UserCategory.Admin)
            {
                return "Admin Qaqa admini update ede bilmersen!!!";
            }
            foreach (User u in users)
            {
                if (u.UserName == username)
                {
                    return "Username istifade olunub qaqa,bu addan istifade ede bilmersen!!!";
                }
            }
            if (user.id == 1)
            {
                return "SuperAdmini deyise bilmersen qaqa";
            }
            user.Name = name;
            user.UserName = username;
            user.Password = password;
            user.category = category;
            Console.ForegroundColor = ConsoleColor.Green;
            return "Successfully Updated...";
        }
    }
}

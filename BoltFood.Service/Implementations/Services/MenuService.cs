using BoltFood.Core.Enums.Product;
using BoltFood.Core.Enums.Restaurant;
using BoltFood.Core.Enums.User;
using BoltFood.Core.Models;
using BoltFood.Service.Extentions;
using BoltFood.Service.Interfaces.IMenuService;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Implementations.Services
{
    public class MenuService : IMenuService
    {
        public async Task ShowMenu()
        {
            Helper.WriteSlowLine("Welcome to Bolt Food App", ConsoleColor.Green);
            Thread.Sleep(1000);
            bool status = true;
            while (status)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                User user = await Login();
                if (user.category == UserCategory.SuperAdmin || user.category == UserCategory.Admin)
                {
                    while (status)
                    {
                        await Console.Out.WriteLineAsync("1.Restaurant Process\n" +
                            "2.Product Process\n" +
                            "3.Admin Menu\n" +
                            "q Log out");
                        await Console.Out.WriteAsync("Enter Step:");
                        string step = Console.ReadLine();
                        switch (step)
                        {
                            case "1":
                                Console.Clear();
                                await AdminMenuRestaurant(status);
                                status = true;
                                break;
                            case "2":
                                Console.Clear();
                                await AdminMenuProduct(status);
                                status = true;
                                break;
                            case "3":
                                Console.Clear();
                                await AdminMenu(user, status);
                                break;
                            case "q":
                                Console.Clear();
                                Helper.WriteSlowLine("Quited...", ConsoleColor.Cyan);
                                status = false; break;
                            default:
                                Console.Clear();
                                Helper.WriteSlowLine("Please enter correct step!!!",ConsoleColor.Red);
                                break;
                        }
                    }
                    status = true;
                }
                else if (user.category == UserCategory.User)
                {
                    while (status)
                    {
                        await Console.Out.WriteLineAsync("1.Restaurant Process\n" +
                            "2.Product Process\n" +
                            "q Log out");
                        await Console.Out.WriteAsync("Enter Step:");
                        string step = Console.ReadLine();
                        switch (step)
                        {
                            case "1":
                                Console.Clear();
                                await UserMenuRestaurant(status);
                                status = true;
                                break;
                            case "2":
                                Console.Clear();
                                await UserMenuProduct(status);
                                status = true;
                                break;
                            case "q":
                                Console.Clear();
                                Helper.WriteSlowLine("Quited...", ConsoleColor.Cyan);
                                status = false; break;
                            default:
                                Console.Clear();
                                Helper.WriteSlowLine("Please enter correct step!!!", ConsoleColor.Red);
                                break;
                        }
                    }
                    status = true;
                }
            }
        }
        private readonly RestaurantService _restaurantService = new RestaurantService();
        private readonly ProductService _productService = new ProductService();
        private readonly UserService _userService = new UserService();
        private async Task AddRestaurant()
        {

            Console.Write("Please Add Restaurant Name: ");
            string name = Console.ReadLine();
        Name:
            if (name == null)
            {
                Console.Write("Please Add Valid Name: ");
                name = Console.ReadLine();
                goto Name;
            }
            var Categories = Enum.GetValues(typeof(RestaurantCategory));
        Category:
            Console.Write("Restaurant Categories:\n");
            foreach (var category in Categories)
            {
                Console.WriteLine((int)category + "." + category);
            }
            Console.Write("Select of them by ID : ");
            int.TryParse(Console.ReadLine(), out int categoryId);
            try
            {
                Categories.GetValue(categoryId - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter correct Category...");
                Console.ForegroundColor = ConsoleColor.White;
                goto Category;
            }
            string message = await _restaurantService.CreateAsync((RestaurantCategory)categoryId, name);
            Helper.WriteSlowLine(message);
        }
        private async Task ShowAllRestaurant()
        {
            List<Restaurant> restaurants = await _restaurantService.GetAllAsync();
            if (!await CheckRestaurant())
            {
                return;
            }
            foreach (Restaurant restaurant in restaurants)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(restaurant);
            }
        }
        private async Task ShowRestaurantByID()
        {
            if (!await CheckRestaurant())
            {
                return;
            }
            Console.Write("Please enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);
            Restaurant restaurant = await _restaurantService.GetAsync(id);
            if (restaurant != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(restaurant);
            }
        }
        private async Task UpdateRestaurant()
        {
            if (!await CheckRestaurant()) { return; }
            Console.Write("Please enter Restaurant ID:");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Please Add new Restaurant Name: ");
            string name = Console.ReadLine();
        Name:
            if (name == null)
            {
                Console.Write("Please Add Valid Name: ");
                name = Console.ReadLine();
                goto Name;
            }
            var Categories = Enum.GetValues(typeof(RestaurantCategory));
        Category:
            Console.Write("Restaurant Categories:\n");
            foreach (var category in Categories)
            {
                Console.WriteLine((int)category + "." + category);
            }
            Console.Write("Select of them by ID : ");
            int.TryParse(Console.ReadLine(), out int categoryId);
            try
            {
                Categories.GetValue(categoryId - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter correct Category...");
                Console.ForegroundColor = ConsoleColor.White;
                goto Category;
            }
            string message = await _restaurantService.UpdateAsync(id, (RestaurantCategory)categoryId, name);
            Helper.WriteSlowLine(message);
        }
        private async Task RemoveRestaurant()
        {
            if (!await CheckRestaurant()) { return; }
            Console.Write("Please enter Restaurant ID:");
            int.TryParse(Console.ReadLine(), out int id);
            string message = await _restaurantService.RemoveAsync(id);
            Helper.WriteSlowLine(message);
        }
        private async Task AddProduct()
        {
        Begin:
            if (!await CheckRestaurant())
            {
                Helper.WriteSlowLine("You cannot add Product without Restaurant");
                Console.ForegroundColor = ConsoleColor.White;
                await AddRestaurant();
                goto Begin;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please Add Product Name: ");
            string name = Console.ReadLine();
        Name:
            if (name == null)
            {
                Console.Write("Please Add Valid Name: ");
                name = Console.ReadLine();
                goto Name;
            }
            var Categories = Enum.GetValues(typeof(ProductCategory));
        Category:
            Console.Write("Product Categories:\n");
            foreach (var category in Categories)
            {
                Console.WriteLine((int)category + "." + category);
            }
            Console.Write("Select of them by ID : ");
            int.TryParse(Console.ReadLine(), out int categoryId);
            try
            {
                Categories.GetValue(categoryId - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter correct Category...");
                Console.ForegroundColor = ConsoleColor.White;
                goto Category;
            }
        Price:
            await Console.Out.WriteAsync("Price: ");
            double.TryParse(Console.ReadLine(), out double price);
            if (!double.IsNormal(price))
            {
                Helper.WriteSlowLine("Price must be digit", ConsoleColor.Red);
                goto Price;
            }
            await Console.Out.WriteAsync("Restaurant ID: ");
            int.TryParse(Console.ReadLine(), out int RestaurantID);
            string message = await _productService.CreateAsync(name, (ProductCategory)categoryId, price, RestaurantID);
            Helper.WriteSlowLine(message);
        }
        private async Task ShowAllProduct()
        {
            if (!await CheckProduct())
            {
                return;
            }
            List<Product> products = await _productService.GetAllAsync();

            foreach (Product product in products)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(product);
            }
        }
        private async Task ShowProductByID()
        {
            if (!await CheckProduct())
            {
                return;
            }
            Console.Write("Please enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);
            Product product = await _productService.GetAsync(id);
            if (product != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(product);
            }
        }
        private async Task UpdateProduct()
        {
            if (!await CheckProduct())
            {
                return;
            }
            Console.Write("Please enter Product ID:");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Please Add new Product Name: ");
            string name = Console.ReadLine();
        Name:
            if (name == null)
            {
                Console.Write("Please Add Valid Name: ");
                name = Console.ReadLine();
                goto Name;
            }
            var Categories = Enum.GetValues(typeof(ProductCategory));
        Category:
            Console.Write("Product Categories:\n");
            foreach (var category in Categories)
            {
                Console.WriteLine((int)category + "." + category);
            }
            Console.Write("Select of them by ID : ");
            int.TryParse(Console.ReadLine(), out int categoryId);
            try
            {
                Categories.GetValue(categoryId - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter correct Category...");
                Console.ForegroundColor = ConsoleColor.White;
                goto Category;
            }
        Price:
            await Console.Out.WriteAsync("new Price: ");
            double.TryParse(Console.ReadLine(), out double price);
            if (!double.IsNormal(price))
            {
                Helper.WriteSlowLine("Price must be digit", ConsoleColor.Red);
                goto Price;
            }
            await Console.Out.WriteAsync("new Restaurant ID: ");
            int.TryParse(Console.ReadLine(), out int RestaurantID);
            string message = await _productService.UpdateAsync(id, name, (ProductCategory)categoryId, price, RestaurantID);
            Helper.WriteSlowLine(message);
        }
        private async Task RemoveProduct()
        {
            if (!await CheckProduct())
            {
                return;
            }
            Console.Write("Please enter Product ID:");
            int.TryParse(Console.ReadLine(), out int id);
            string message = await _productService.RemoveAsync(id);
            Helper.WriteSlowLine(message);
        }
        private async Task AddStaff(User user)
        {
            Console.Write("Please add Name: ");
            string name = Console.ReadLine();
            Console.Write("Please add Username: ");
            string username = Console.ReadLine();
            Console.Write("Please add Password: ");
            string password = Console.ReadLine();
            var Categories = Enum.GetValues(typeof(UserCategory));
        Category:
            Console.Write("User Categories:\n");
            foreach (var category in Categories)
            {
                Console.WriteLine((int)category + "." + category);
            }
            Console.Write("Select of them by ID : ");
            int.TryParse(Console.ReadLine(), out int categoryId);
            try
            {
                Categories.GetValue(categoryId - 1);
            }
            catch (Exception)
            {
                Console.Clear();
                Helper.WriteSlowLine("Enter correct Category...", ConsoleColor.Red);
                goto Category;
            }
            if ((UserCategory)categoryId == UserCategory.SuperAdmin)
            {
                Console.Clear();
                Helper.WriteSlowLine("SuperAdmin is unique", ConsoleColor.Red);
                goto Category;
            }
            string message = await _userService.CreateAsync(name, username, password, (UserCategory)categoryId, user);
            Console.Clear();
            Helper.WriteSlowLine(message);
        }
        private async Task ShowAllStaffs()
        {
            List<User> users = await _userService.GetAllAsync();
            if (!await CheckUser())
            {
                return;
            }
            foreach (User user in users)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(user);
            }
        }
        private async Task ShowStaffbyID()
        {
            if (!await CheckUser())
            {
                return;
            }
            Console.Write("Please enter ID: ");
            int.TryParse(Console.ReadLine(), out int id);
            User user = await _userService.GetAsync(id);
            if (user != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(user);
            }
        }
        private async Task UpdateStaff(User user)
        {
            if (!await CheckUser()) { return; }
            Console.Write("Please enter User ID:");
            int.TryParse(Console.ReadLine(), out int id);
            Console.Write("Please add new Name: ");
            string name = Console.ReadLine();
            Console.Write("Please add new Username: ");
            string username = Console.ReadLine();
            Console.Write("Please add new Password: ");
            string password = Console.ReadLine();
            var Categories = Enum.GetValues(typeof(UserCategory));
        Category:
            Console.Write("User Categories:\n");
            foreach (var category in Categories)
            {
                Console.WriteLine((int)category + "." + category);
            }
            Console.Write("Select of them by ID : ");
            int.TryParse(Console.ReadLine(), out int categoryId);
            try
            {
                Categories.GetValue(categoryId - 1);
            }
            catch (Exception)
            {
                Helper.WriteSlowLine("Enter correct Category...", ConsoleColor.Red);
                goto Category;
            }
            if ((UserCategory)categoryId == UserCategory.SuperAdmin)
            {
                Console.Clear();
                Helper.WriteSlowLine("SuperAdmin is unique", ConsoleColor.Red);
                goto Category;
            }
            string message = await _userService.UpdateAsync(id, name, username, password, (UserCategory)categoryId, user);
            Helper.WriteSlowLine(message);
        }
        private async Task RemoveStaff(User user)
        {
            if (!await CheckUser()) { return; }
            Console.Write("Please enter User ID:");
            int.TryParse(Console.ReadLine(), out int id);
            string message = await _userService.RemoveAsync(id, user);
            Helper.WriteSlowLine(message);
        }
        private async Task<bool> CheckRestaurant()
        {
            List<Restaurant> restaurants = await _restaurantService.GetAllAsync();
            if (restaurants.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Helper.WriteSlowLine("There aren`t any Restaurant.Please add Restaurant");
                return false;
            }
            return true;
        }
        private async Task<bool> CheckProduct()
        {
            List<Product> products = await _productService.GetAllAsync();
            if (products.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Helper.WriteSlowLine("There aren`t any Product.Please add Product");
                return false;
            }
            return true;
        }
        private async Task<bool> CheckUser()
        {
            List<User> users = await _userService.GetAllAsync();
            if (users.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Helper.WriteSlowLine("There aren`t any Staff.Please add User or Admin");
                return false;
            }
            return true;
        }
        private async Task AdminMenuProduct(bool status)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (status)
            {
                Console.ForegroundColor = ConsoleColor.White;
                await Console.Out.WriteLineAsync("1.Create Product\n" +
        "2.Show All Product\n" +
       "3.Get Product by ID\n" +
       "4.Update Product\n" +
       "5.Remove Product\n" +
       "q Quit Product Service");
                await Console.Out.WriteAsync("Enter Step:");
                string step = Console.ReadLine();
                switch (step)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await AddProduct();
                        break;
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowAllProduct();
                        break;
                    case "3":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowProductByID();
                        break;
                    case "4":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await UpdateProduct();
                        break;
                    case "5":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await RemoveProduct();
                        break;
                    case "q":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Helper.WriteSlowLine("Quited...");
                        Console.ForegroundColor = ConsoleColor.White;
                        status = false;
                        break;
                    default:
                        Console.Clear();
                        Helper.WriteSlowLine("Please enter correct step!!!", ConsoleColor.Red);
                        break;
                }
            }
        }
        private async Task AdminMenuRestaurant(bool status)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (status)
            {
                Console.ForegroundColor = ConsoleColor.White;
                await Console.Out.WriteLineAsync("1.Create Restaurant\n" +
        "2.Show All Restaurant\n" +
       "3.Get Restaurant by ID\n" +
       "4.Update Restaurant\n" +
       "5.Remove Restaurant\n" +
       "q Quit Restaurant Service");
                await Console.Out.WriteAsync("Enter Step:");
                string step = Console.ReadLine();
                switch (step)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await AddRestaurant();
                        break;
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowAllRestaurant();
                        break;
                    case "3":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowRestaurantByID();
                        break;
                    case "4":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await UpdateRestaurant();
                        break;
                    case "5":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await RemoveRestaurant();
                        break;
                    case "q":
                        Console.Clear();
                        Helper.WriteSlowLine("Quited...", ConsoleColor.Cyan);
                        status = false;
                        break;
                    default:
                        Console.Clear();
                        Helper.WriteSlowLine("Please enter correct step!!!", ConsoleColor.Red);
                        break;
                }
            }
        }
        private async Task AdminMenu(User user, bool status)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (status)
            {
                Console.ForegroundColor = ConsoleColor.White;
                await Console.Out.WriteLineAsync("1.Add Staff\n" +
        "2.Show All Staffs\n" +
       "3.Get Staff by ID\n" +
       "4.Update Staff\n" +
       "5.Remove Staff\n" +
       "6.Info\n" +
       "q Quit Staff Service");
                await Console.Out.WriteAsync("Enter Step:");
                string step = Console.ReadLine();
                switch (step)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await AddStaff(user);
                        break;
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowAllStaffs();
                        break;
                    case "3":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowStaffbyID();
                        break;
                    case "4":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await UpdateStaff(user);
                        break;
                    case "5":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await RemoveStaff(user);
                        break;
                    case "6":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await Info();
                        break;
                    case "q":
                        Console.Clear();
                        Helper.WriteSlowLine("Quited...", ConsoleColor.Cyan);
                        status = false;
                        break;
                    default:
                        Console.Clear();
                        Helper.WriteSlowLine("Please enter correct step!!!", ConsoleColor.Red);
                        break;
                }
            }

        }
        private async Task UserMenuProduct(bool status)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (status)
            {
                Console.ForegroundColor = ConsoleColor.White;
                await Console.Out.WriteLineAsync("1.Show All Product\n" +
       "2.Get Product by ID\n" +
       "q Quit");
                await Console.Out.WriteAsync("Enter Step:");
                string step = Console.ReadLine();
                switch (step)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowAllProduct();
                        break;
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowProductByID();
                        break;
                    case "q":
                        Console.Clear();
                        Helper.WriteSlowLine("Quited...", ConsoleColor.Cyan);
                        status = false;
                        break;
                    default:
                        Console.Clear();
                        Helper.WriteSlowLine("Please enter correct step!!!", ConsoleColor.Red);
                        break;
                }
            }
        }
        private async Task UserMenuRestaurant(bool status)
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (status)
            {
                Console.ForegroundColor = ConsoleColor.White;
                await Console.Out.WriteLineAsync("1.Show All Restaurant\n" +
       "2.Get Restaurant by ID\n" +
       "q Quit");
                await Console.Out.WriteAsync("Enter Step:");
                string step = Console.ReadLine();
                switch (step)
                {
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowAllRestaurant();
                        break;
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        await ShowRestaurantByID();
                        break;
                    case "q":
                        Console.Clear();
                        Helper.WriteSlowLine("Quited...", ConsoleColor.Cyan);
                        status = false;
                        break;
                    default:
                        Console.Clear();
                        Helper.WriteSlowLine("Please enter correct step!!!", ConsoleColor.Red);
                        break;
                }
            }
        }
        private static async Task Info()
        {
            Helper.WriteSlowLine("Username must contain @ and Password contain mimimum one upperCase and digit\n" +
                "Username is unique and it cannot be repeated\n" +
                "Admin cannot add,remove and update Admins and SuperAdmin,only Users\n" +
                "SuperAdmin can do everything and SuperAdmin is unique\n" +
                "Users can see only Get and GetAll functions,not Admin Menu", ConsoleColor.DarkCyan);
        }
        private async Task<User> Login()
        {
        Login:
            Helper.WriteSlow("Enter Username: ",ConsoleColor.White);
            string username = Console.ReadLine();
            Helper.WriteSlow("Enter Password: ",ConsoleColor.White);
            string password = Console.ReadLine();
            User user = await _userService.CheckUserAsync(username, password);
            if (user == null)
            {
                Helper.WriteSlowLine("Enter Correct Username and Password!!!", ConsoleColor.Red);
                Thread.Sleep(500);
                Console.Clear();
                goto Login;
            }
            Console.Clear();
            if (user.category == UserCategory.SuperAdmin)
            {
                Helper.WriteSlow("Logining successfully ", ConsoleColor.Magenta);
                Helper.WriteSlowLine("as SuperAdmin", ConsoleColor.Cyan);
            }
            else if (user.category == UserCategory.Admin)
            {
                Helper.WriteSlow("Logining successfully ", ConsoleColor.Magenta);
                Helper.WriteSlowLine("as Admin", ConsoleColor.Cyan);
            }
            else
            {
                Helper.WriteSlow("Logining successfully ", ConsoleColor.Magenta);
                Helper.WriteSlowLine("as User", ConsoleColor.Cyan);
            }
            return user;
        }
    }
}
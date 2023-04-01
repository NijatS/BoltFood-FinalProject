using BoltFood.Core.Enums.User;
using BoltFood.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Core.Models
{
    public class User:BaseModel
    {
        private static int _id;
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserCategory category { get; set; }

        public User(string name,string UserName,string Password,UserCategory category) : base(name)
        {
            _id++;
            id = _id;
            this.UserName = UserName;
            this.Password = Password;
            this.category = category;
        }
        public override string ToString()
        {
            if (id == 1)
            {
                return "ID:" + id + "  Name: " + Name + "  Category: " + category;
            }
            return "ID:" + id + "  Name: " + Name + "  UserName: " + UserName + "  Password: " + Password + "  Category: " + category;
        }
    }
}

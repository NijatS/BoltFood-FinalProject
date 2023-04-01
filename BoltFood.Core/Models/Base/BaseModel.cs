using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Core.Models.Base
{
    public class BaseModel
    {
        public int id;
        public string Name { get; set; }    

        public BaseModel(string name) { 
            Name = name;
        }
    }
}

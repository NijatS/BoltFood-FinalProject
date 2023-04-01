using BoltFood.Core.Enums.User;
using BoltFood.Core.Models;
using BoltFood.Core.Models.Base;
using BoltFood.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private static List<T> _context = new List<T>(); 
        public List<T> Context { get { return _context; } }
        public async Task AddAsync(T model)
        {
            Context.Add(model);
        }
        public async Task<T> GetAsync(Func<T, bool> expression)
        {
            T model = _context.FirstOrDefault(expression);
            return model;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return Context;
        }

        public async Task RemoveAsync(T model)
        {
            Context.Remove(model);
        }

        public async Task UpdateAsync(T model)
        {
            for(int i = 0 ; i < Context.Count; i++)
            {
                if (Context[i].id == model.id)
                {
                    Context[i] = model;
                    break;
                }
            }
        }
    }
}

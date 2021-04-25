using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Domain.Interfaces;
using EmployeeManager.Domain.Models;

namespace EmployeeManager.Database.Repository
{
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : BaseModel
    {
        private readonly Context context;

        public BaseRepository(Context context)
        {
            this.context = context;
        }

        public int Add(Entity entity)
        {
            context.Add(entity);

            return entity.Id;
        }

        public int Delete(Entity entity)
        {
            context.Remove(entity);

            return entity.Id;
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}

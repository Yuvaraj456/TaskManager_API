using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using task =  System.Threading.Tasks;
using TaskManager.DatabaseContext;
using TaskManager_Core.Domain.Entities;
using TaskManager_Core.Domain.RepositoryContracts;

namespace TaskManager_Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
               _db = db;
        }

        public async task.Task AddSkills(List<Skill> skills)
        {
            await _db.Skills.AddRangeAsync(skills);
            await _db.SaveChangesAsync();

            await task.Task.CompletedTask;

        }
    }
}

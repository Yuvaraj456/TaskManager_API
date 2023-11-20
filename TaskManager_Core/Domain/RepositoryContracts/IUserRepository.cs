using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager_Core.Domain.Entities;

namespace TaskManager_Core.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        public Task AddSkills(List<Skill> skills);

    }
}

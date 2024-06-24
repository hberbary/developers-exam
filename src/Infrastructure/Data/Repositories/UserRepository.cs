
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : Repository<UserEntity>
    {
        public UserRepository(SqlDbContext context) : base(context)
        {}

    }

}
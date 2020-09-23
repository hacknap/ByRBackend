using ByR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        private readonly DataContext context;
        public UserRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}

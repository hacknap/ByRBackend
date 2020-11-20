using ByR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public class RolUserRepository : GenericRepository<RoleUser>, IRolUser
    {
        private readonly DataContext context;

        public RolUserRepository(DataContext context) : base(context)
        {
            this.context = context;

        }
    }
}
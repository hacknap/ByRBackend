using ByR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IProperty
    {
        private readonly DataContext context;
        public PropertyRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}

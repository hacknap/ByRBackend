using ByR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public class GalleryRepository : GenericRepository<Gallery>, IGallery
    {
        private readonly DataContext context;

        public GalleryRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}

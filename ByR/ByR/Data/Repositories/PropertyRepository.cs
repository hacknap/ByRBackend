using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByR.Data.Repositories;

namespace ByR.Data.Repositories
{
    public class PropertyRepository : GenericRepository<Property>, IProperty
    {
        private readonly DataContext context;
        public PropertyRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<ActionResult<PageAndSortResponse<Property>>> GetProperties([FromQuery] PageAndSortRequest param, string id)
        {
            IEnumerable<Property> listProperty = null;
            
            
            listProperty = await context.Property.Where(p => p.User.Id == id || p.IsDelete.Equals(false) ).ToListAsync();
            
           
            if (param.Direction.ToLower() == "asc")
                listProperty = await context.Property.OrderBy(p => EF.Property<object>(p, param.Column)).ToListAsync();
            else if (param.Direction.ToLower() == "desc")
                listProperty = await context.Property.OrderByDescending(p => EF.Property<object>(p, param.Column)).ToListAsync();
            else
                listProperty = await context.Property.OrderBy(p => p.Id).ToListAsync();

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filter))
            {
                listProperty = listProperty.Where(ele => ele.Description.Contains(param.Filter));
            }
            total = listProperty.Count();
            listProperty = listProperty.Skip((param.Page - 1) * param.PageSize).Take(param.PageSize);

            var result = new PageAndSortResponse<Property>
            {
                Data = listProperty,
                TotalRows = total
            };

            return result;
        }
    }
}

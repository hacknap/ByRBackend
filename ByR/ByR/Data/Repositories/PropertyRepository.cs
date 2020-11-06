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

            if (param.Direction.ToLower() == "asc")
                listProperty = await context.Property.OrderBy(p => EF.Property<object>(p, param.Column))
                    .Where(p => p.IsDelete.Equals(false))
                    .Where(x => x.User.Id == id).ToListAsync();
            else if (param.Direction.ToLower() == "desc")
                listProperty = await context.Property.OrderByDescending(p => EF.Property<object>(p, param.Column))
                    .Where(p => p.IsDelete.Equals(false))
                    .Where(x => x.User.Id == id).ToListAsync();
            else
                listProperty = await context.Property.OrderBy(p => p.Id)
                    .Where(p => p.IsDelete.Equals(false))
                    .Where(x => x.User.Id == id).ToListAsync();

            int total = 0;
            if (!string.IsNullOrEmpty(param.Filter))
            {
                listProperty = listProperty.Where(ele => ele.Description.Contains(param.Filter)).Where(p => p.IsDelete.Equals(false))
                    .Where(x => x.User.Id == id);
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

        public List<Property> GetPropertyById(string id)
        {

            return context.Property.Include(p => p.User)
                                    .Where(p => p.Id.Equals(id)).ToList();
        }

        public List<Property> GetPropertyBySerch(string serch, decimal preciodesde, decimal preciohasta,
                                                    decimal tamaniodesde, decimal tamaniohasta, decimal nbanios, decimal ncuartos)
        {
            var model = context.Property.Include(p => p.User)
                                   .Where(p => p.Description.Contains(serch) ||
                                            p.Direction.Contains(serch) ||
                                            p.User.Name.Contains(serch) ||
                                            (p.Bathrooms > 0 && p.Bathrooms <= nbanios) ||
                                            (p.Bedrooms > 0 && p.Bedrooms <= ncuartos) ||
                                            (p.Price >= preciodesde && p.Price <= preciohasta )||
                                            (p.Size >= tamaniodesde && p.Size <= tamaniohasta)).ToList();

            return model;
        }

            

    }
}

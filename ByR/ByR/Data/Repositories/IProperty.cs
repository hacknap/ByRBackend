using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public interface IProperty : IGeneric<Property>
    {
        Task<ActionResult<PageAndSortResponse<Property>>> GetProperties([FromQuery] PageAndSortRequest param, string id);

        List<Property> GetPropertyById(string id);

        List<Property> GetPropertyBySerch(string serch);
    }
}

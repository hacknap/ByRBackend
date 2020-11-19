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
        IQueryable<Property> GetAllProperties();
        Task<ActionResult<PageAndSortResponse<Property>>> GetProperties([FromQuery] PageAndSortRequest param, string id);

        List<Property> GetPropertyById(string id);
        Task<Property> GetPropertyByPropertyId(string id);
        List<Property> GetPropertyBySerch(string serch, decimal preciodesde, decimal preciohasta, decimal tamaniodesde, decimal tamaniohasta, decimal nbanios, decimal ncuartos);
    }
}

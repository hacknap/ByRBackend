using ByR.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public interface IGallery : IGeneric<Gallery>
    {
        Task<ActionResult<List<Gallery>>> GetGalleryByPropertyId(string id);
    }
}

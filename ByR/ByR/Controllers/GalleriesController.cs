using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ByR.Data.Repositories;
using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ByR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GalleriesController : ControllerBase
    {
        private readonly IGallery _gallery;
        private readonly AppSettings _appSettings;
        public GalleriesController(IOptions<AppSettings> appSettings, IGallery gallery)
        {
            _appSettings = appSettings.Value;
            _gallery = gallery;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Gallery>>> GetGalleryByPropertyID(string id)
        {
            return await _gallery.GetGalleryByPropertyId(id);
        }
    }

    
}

﻿using ByR.Data.Repositories;
using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace ByR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly IProperty _properties;
        private readonly AppSettings _appSettings;
        private readonly IUser _users;
        private readonly IGallery _gallery;

        public PropertiesController(IProperty property, IOptions<AppSettings> appSettings, IUser user, IGallery gallery)
        {
            _properties = property;
            _appSettings = appSettings.Value;
            _users = user;
            _gallery = gallery;
        }

        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Property>>> GetProperties([FromQuery] PageAndSortRequest param, string id)
        {
            return await _properties.GetProperties(param, id);
        }

        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property property)
        {
            if (ModelState.IsValid)
            {
                property.IsDelete = false;
                property.Register = DateTime.UtcNow;
                var user = await _users.FindByIdAsync(property.UserIdPro);
                property.User = user;

                await _properties.CreateAsync(property);

                ////Generando un nombre unico para la imagen
                var path = string.Empty;
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";

                ////obteniendo el path local en el server
                path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/Propiedades",
                    file);

                byte[] imagebyte = System.Convert.FromBase64String(property.imageurl);


                ////guardando la imagen a partir de bytes[]
                using (var ms = new MemoryStream(imagebyte))
                {
                    var img = Image.FromStream(ms);
                    img.Save(path, ImageFormat.Jpeg);
                };



                ////este campo va la base de datos
                path = $"~/Propiedades/{file}";



                var Gallery = new Gallery
                {
                    ImageUrl = path,
                    IsDelete = false,                   
                    Property = property.Id,
                    Register = DateTime.UtcNow
                };

                await this._gallery.CreateAsync(Gallery);

            }
            else
            {
                return BadRequest();
            }
            return property;
        }
        [HttpPut]
        public async Task<ActionResult<Property>> PutProperty(Property property)
        {

            if (ModelState.IsValid)
            {
                property.User = await _users.FindByIdAsync(property.User.Id);
                await _properties.UpdateAsync(property);

            }
            else
            {
                return BadRequest();
            }

            return property;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Property>> DeleteProperty(string id)
        {
            var property = await _properties.FindByIdAsync(id);

            if (property == null)
            {
                return NotFound();
            }
            else
            {
                property.IsDelete = true;
                await _properties.UpdateAsync(property);
            }
            return property;
        }

        [HttpGet("{id}")]
        public Property GetPropertyById(string id)
        {
            return _properties.GetPropertyById(id);
        }




    }
}

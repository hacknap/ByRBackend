using ByR.Data.Repositories;
using ByR.Entities;
using ByR.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
        [Route("GetPropertyByUserBuyer/")]
        public ActionResult<PageAndSortResponse<Property>> GetPropertyByUserBuyer
            (string serch, double preciodesde, double? preciohasta, double? tamaniodesde, double? tamaniohasta, double? ncuartos, double? nbanios)
        {

            IQueryable<Property> propertyList;

            if (serch == null && preciohasta == 0 && tamaniohasta == 0 && nbanios == 0 && ncuartos == 0)
            {
                propertyList = this._properties.GetAllProperties();
            }

            else
            {
                propertyList= this._properties.GetAllProperties();

                if (serch != null)
                {
                    propertyList = propertyList.Where(p => p.Description.Contains(serch) ||
                                                      p.Direction.Contains(serch) ||
                                                      p.User.Name.Contains(serch) ||
                                                      p.User.Email.Contains(serch) );
                }
                if (preciohasta > 0)
                {
                    propertyList = propertyList.Where(p => p.Price >= Convert.ToDecimal(preciodesde) && p.Price <= Convert.ToDecimal(preciohasta));
                }
                if (tamaniohasta > 0)
                {
                    propertyList = propertyList.Where(p=> p.Size >= Convert.ToDecimal(tamaniodesde) && p.Size <= Convert.ToDecimal(tamaniohasta));
                }
                if (nbanios > 0)
                {
                    propertyList = propertyList.Where(p=> p.Bathrooms > 0 && p.Bathrooms <= Convert.ToDecimal(nbanios));
                }
                if (ncuartos > 0)
                {
                    propertyList = propertyList.Where(p => p.Bedrooms > 0 && p.Bedrooms <= Convert.ToDecimal(ncuartos));
                }
            }





            var response = new PageAndSortResponse<Property>
            {
                Data = propertyList,
                TotalRows = propertyList.Count()
            };

            return response;
        }

        [HttpGet]
        public async Task<ActionResult<PageAndSortResponse<Property>>> GetProperties([FromQuery] PageAndSortRequest param, string id)
        {
            return await _properties.GetProperties(param, id);
        }


        [HttpGet]
        [Route("GetPropertiesInit")]
        public async Task<ActionResult<PageAndSortResponse<Property>>> GetPropertiesInit()
        {
            var model = _properties.GetAllProperties();

            var response = new PageAndSortResponse<Property>()
            {
                Data = model,
                TotalRows = model.Count()
            };

            return response;
        }


        [HttpPost]
        [Route("PostSavedImage")]
        public async Task<ActionResult<Property>> PostSavedImage(Property property)
        {
            try
            {

                var _property = await this._properties.FindByIdAsync(property.Id);

                //////Generando un nombre unico para la imagen
                var path = string.Empty;
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";

                //////obteniendo el path local en el server
                path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/Propiedades",
                    file);

                byte[] imagebyte = System.Convert.FromBase64String(property.imageurl);


                //////guardando la imagen a partir de bytes[]
                using (var ms = new MemoryStream(imagebyte))
                {
                    var img = Image.FromStream(ms);
                    img.Save(path, ImageFormat.Jpeg);
                };



                //////este campo va la base de datos
                path = $"~/Propiedades/{file}";



                var Gallery = new Gallery
                {
                    ImageUrl = path,
                    imagen64 = property.imageurl,
                    IsDelete = false,
                    Property = property.Id,
                    Register = DateTime.UtcNow
                };

                await this._gallery.CreateAsync(Gallery);
                return _property;

            }
            catch (Exception)
            {

                throw;
            }



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
                property.imagen64portada = property.imageurl;
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

                property.imageurl = path;
                await _properties.CreateAsync(property);

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
                property.User = await _users.FindByIdAsync(property.UserIdPro);
                property.imagen64portada = property.imageurl;
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
        public async Task<ActionResult<PageAndSortResponse<Property>>> GetPropertyById(string id)
        {
            var property = _properties.GetPropertyById(id);

            var model = new PageAndSortResponse<Property>()
            {
                Data = property,
                TotalRows = property.Count()
            };

            return model;

        }

    }
}

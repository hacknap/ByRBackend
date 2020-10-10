﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByR.Entities;
using ByR.Data.Repositories;
using ByR.Helpers;
using Microsoft.Extensions.Options;
using System.IO;

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


                


                //var Gallery = new Gallery
                //{
                //    ImageUrl = path,
                //    IsDelete = false,                   
                //    Property = property.Id,
                //    Register = DateTime.UtcNow
                //};

                //await this._gallery.CreateAsync(Gallery);

            }
            else
            {
                return BadRequest();
            }
            return property;
        }
        [HttpPut]
        public async Task<ActionResult<Property>> PutProperty(Property property) {

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
        public async Task<ActionResult<Property>> DeleteProperty(string id){
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ByR.Entities;

namespace ByR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly DataContext _context;

        public PropertiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Properties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperty()
        {
            return await _context.Property.ToListAsync();
        }

        // GET: api/Properties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(string id)
        {
            var @property = await _context.Property.FindAsync(id);

            if (@property == null)
            {
                return NotFound();
            }

            return @property;
        }

        // PUT: api/Properties/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(string id, Property @property)
        {
            if (id != @property.Id)
            {
                return BadRequest();
            }

            _context.Entry(@property).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PropertyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Properties
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(Property @property)
        {
            _context.Property.Add(@property);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PropertyExists(@property.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProperty", new { id = @property.Id }, @property);
        }

        // DELETE: api/Properties/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Property>> DeleteProperty(string id)
        {
            var @property = await _context.Property.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }

            _context.Property.Remove(@property);
            await _context.SaveChangesAsync();

            return @property;
        }

        private bool PropertyExists(string id)
        {
            return _context.Property.Any(e => e.Id == id);
        }
    }
}

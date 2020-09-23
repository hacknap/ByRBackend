    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Entities
{
    [Table("Gallery")]
    public class Gallery
    {
        public string Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Register { get; set; }

        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }
        
        //llave foranea
        public Property Property { get; set; }
    }
}

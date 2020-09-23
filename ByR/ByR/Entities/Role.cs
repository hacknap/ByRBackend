using ByR.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Entities
{
    [Table("Role")]
    public class Role : IEntity
    {
        public string Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Register { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(150, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        public string Description { get; set; }

    }
}

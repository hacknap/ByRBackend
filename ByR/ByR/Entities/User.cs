using ByR.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Entities
{
    [Table("User")]
    public class User : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Register { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(150, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(150, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Display(Name = "Cedula de identidad")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(20, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        public string Ci { get; set; }
    }
}

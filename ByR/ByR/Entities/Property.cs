using ByR.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Entities
{
        
    public class Property : IEntity
    {
        public string Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Register { get; set; }

        [Display(Name = "Precio de propiedad")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [DisplayFormat(DataFormatString = "{0:#}")]
        [Column(TypeName = "decimal(5,0)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
                    
        [Display(Name = "Numero de habitaciones")]
        public int Bedrooms { get; set; }
    
        [Display(Name = "Numero de Baños")]
        public int Bathrooms { get; set; }

        [Display(Name = "Tamaño de la propiedad")]
        public decimal Size { get; set; }

        [Display(Name = "Direccion de la casa")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(250, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        public string Direction { get; set; }

        [Display(Name = "Estado de la propiedad")]
        public bool State { get; set; }

        [Display(Name = "Descripcion de la propiedad")]
        [Required(ErrorMessage = "Debe ingresar {0}")]
        [StringLength(400, ErrorMessage = "El campo no puede tener mas de {1} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Description  { get; set; }

        [Display(Name = "Latitud")]
        public string Latitude { get; set; }

        [Display(Name = "Longitud")]
        public string Longitude { get; set; }

        //Enums
        public TypeProperty TypeProperty { get; set; }
        public Category Category { get; set; }

        public User User { get; set; }


    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ByR.Data.Repositories
{
    public interface IEntity
    {
        string Id { get; set; }

        [Display(Name = "¿Suspendido?")]
        bool IsDelete { get; set; }

        [Display(Name = "Fecha Registro")]
        DateTime Register { get; set; }
    }
}
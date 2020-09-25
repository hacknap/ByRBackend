using ByR.Data.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Entities
{
    [Table("RoleUser")]
    public class RoleUser : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime Register { get; set; }  

        //llaves foraneas 
        public Role Role { get; set; }
        public User User { get; set; }
    
    }
}

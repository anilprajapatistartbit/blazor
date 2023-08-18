using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.DatabaseModel
{
    [Table("Login", Schema = "public")]
    public class Login
    {
        [Key]
       
        public string Id { get; set; }

        [Required]
       
        public string HashPassword { get; set; }

        [Required]
       
        public string SaltPassword { get; set; }

       
        public bool IsActive { get; set; }

        [Required]
        public string LoginType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

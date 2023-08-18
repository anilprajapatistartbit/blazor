using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.DatabaseModel
{
    [Table(name: "Patient", Schema = "public")]
    public class Patient
    {
        [Key]
        public string Id { get; set; }

        [Required]
       
        public string FirstName { get; set; }

        [Required]
       
        public string LastName { get; set; }

      
        public string Email { get; set; }

        
        public string PhoneNumber { get; set; }

       
        public string AlternatePhoneNumber { get; set; }

  
        public string AddressFirstLine { get; set; }

        public string? AddressSecondLine { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string ZipCode { get; set; }
    }
}

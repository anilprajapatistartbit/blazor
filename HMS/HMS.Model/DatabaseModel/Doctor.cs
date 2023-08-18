using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.DatabaseModel
{
    [Table(name: "Doctor", Schema = "public")]
    public  class Doctor
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Speciality { get; set; }
        public string Email { get; set; }
        public string Qualification { get; set; }
        public int? Experience { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public string AddressFirstLine { get; set; }
        public string? AddressSecondLine { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateTime? Dob { get; set; }
        
        public int DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.DatabaseModel
{
    [Table(name: "Department", Schema = "public")]
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

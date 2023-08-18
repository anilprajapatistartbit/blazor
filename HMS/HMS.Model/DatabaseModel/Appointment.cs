using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Model.DatabaseModel
{
    [Table(name: "Appointment", Schema = "public")]
    public class Appointment
    {
        [Key]
        
        public string Id { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }
        public DateTime? AppointmentTime { get; set; }

        public string? Status { get; set;}
        public string? AnalysisReport { get; set; }
        public string? DoctorPrescription { get; set; }
        public bool? ReVisit { get; set; }

        [Required]
        public string PatientId { get; set; }
        [Required]
        public string DoctorId { get; set;}

        [ForeignKey("PatientId")]
        public virtual Patient? Patient { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor? Doctor { get; set; }

    }
}

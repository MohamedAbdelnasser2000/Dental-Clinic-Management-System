namespace Dental_Clinic_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Appointment
    {
        [Key]
        public int App_Id { get; set; }

        public int? Pat_Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Exam_Date { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Reply_Date { get; set; }

        public string Reason { get; set; }

        public virtual Patient Patient { get; set; }
    }
}

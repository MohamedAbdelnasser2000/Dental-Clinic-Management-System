namespace Dental_Clinic_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Treatment")]
    public partial class Treatment
    {
        [Key]
        public int Treat_Id { get; set; }

        public string Treat_Name { get; set; }

        [StringLength(50)]
        public string Cost { get; set; }

        public string Description { get; set; }

        public int? Quantity { get; set; }
    }
}

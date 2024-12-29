namespace Dental_Clinic_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Prescription
    {
        [Key]
        public int Pre_Id { get; set; }

        public int? Pat_Id { get; set; }

        public string X_Ray { get; set; }

        public string Disease { get; set; }

        public string Medication { get; set; }

        public string Prescription_List { get; set; }

        public string Current_Health { get; set; }

        [StringLength(50)]
        public string Cost { get; set; }

        [StringLength(50)]
        public string Pay { get; set; }

        [StringLength(50)]
        public string Rest { get; set; }

        public virtual Patient Patient { get; set; }
    }
}

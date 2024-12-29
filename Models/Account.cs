namespace Dental_Clinic_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        public int S_Id { get; set; }



        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50)]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Confirmpassword does not match,type again ! ")]
        public string RePassword { get; set; }
    }
}

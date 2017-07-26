namespace FirstApplication_V2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employee
    {
        [Key]
        [StringLength(128)]
        public string EmployeeId { get; set; }


        [StringLength(200)]
        public string Name { get; set; }


        public DateTime CreateDate { get; set; }

        public DateTime EditDate { get; set; }
    }
}

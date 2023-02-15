using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.Models
{
    public class EmployeeAttandenceModel
    {
        public int EmpAtendenceId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        [Required(ErrorMessage = "Time  is required")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> Intime { get; set; }
        [Required(ErrorMessage = " This field  is required")]
        [DataType(DataType.Time)]
        public Nullable<System.TimeSpan> OutTime { get; set; }
         public string latitude { get; set; }
      
        public string longitude { get; set; }
      
        public string Duration { get; set; }            
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Date { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
    }
}
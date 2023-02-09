using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.Models
{
    public class EmployeeProjectMapping
    {
        public int Emp_Project_Mapping_Id { get; set; }
 
        public Nullable<int> Project_Id { get; set; }

    
        public Nullable<int> Employee_Id { get; set; }

        public virtual EmployeeCreateModel Employee { get; set; }

        public string ProjectName { get; set; }
        public string FirstName { get; set; }
        public virtual ProjectMasterModel ProjectMasterModel { get; set; }
    }
}
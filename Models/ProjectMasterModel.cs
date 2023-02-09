using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.Models
{
    public class ProjectMasterModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public Nullable<int> ProjectCode { get; set; }

    }
}
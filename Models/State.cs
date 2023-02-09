using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.Models
{
    public class State
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public State()
        {
            this.Employees = new HashSet<EmployeeCreateModel>();
        }

        public int StateId { get; set; }
        public string StateName { get; set; }
        public Nullable<int> CountryId { get; set; }

        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeCreateModel> Employees { get; set; }
    }
}
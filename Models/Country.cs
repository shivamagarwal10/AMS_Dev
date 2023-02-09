using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.Models
{
    public class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            this.States = new HashSet<State>();
            this.Employees = new HashSet<EmployeeCreateModel>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<State> States { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeCreateModel> Employees { get; set; }
    }
}
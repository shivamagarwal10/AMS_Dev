 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeAttendenceMangement.Models
{
    public class EmployeeCreateModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeCreateModel()
        {

            this.EmployeeAtendances = new HashSet<EmployeeAttandenceModel>();
        }
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Email id is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }

        
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        
        [DataType(DataType.EmailAddress)]
        public String AlternateEmailId { get ;set ;}


        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Numbers & Special characters are not allowed !!")]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Numbers & Special characters are not allowed !!")]
        public string LastName { get; set; }

       
        public string Gender { get; set; }
        [DataType(DataType.Date)]
      
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy }")]
        public Nullable<System.DateTime> DateofBirth { get; set; }
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^([0-9]{6})$", ErrorMessage = "Invalid PinCode ,PinCode required 6 digits and number only !!")]
        public string PinCode { get; set; }

        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Contact No ,Contact No required 10 digits only !!")]
        public string Contact_No { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Alternate No , AlterNate No required 10 digits only !!")]
        public string AlternateContact_No { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public Nullable<System.DateTime> Emp_Joining_Date { get; set; }

      
        public string Marital_status { get; set; }

        [DataType(DataType.Password)]
       
        public string Password { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> StateId { get; set; }
        public bool Is_admin { get; set; }
    
        [NotMapped]
        [DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}")]
        public string ConfirmPassword { get; set; }

        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    
        public virtual ICollection<EmployeeAttandenceModel> EmployeeAtendances { get; set; }
        public virtual State State { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }

        public string IsAttenadance { get; set; }


    }
}
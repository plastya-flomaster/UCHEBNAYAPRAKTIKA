//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UCHEBNAYAPRAKTIKA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResponsiblePerson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResponsiblePerson()
        {
            this.Zakupkas = new HashSet<Zakupka>();
        }
    
        public System.Guid ResponsiblePersonKey { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public System.Guid ClientKey { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AdditionalInfo { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakupka> Zakupkas { get; set; }

        public string FIO()
        {
            if(String.IsNullOrWhiteSpace(Patronymic)) return $"{FirstName} {LastName}";
            return $"{LastName} {FirstName} {Patronymic}";
        }
    }
}

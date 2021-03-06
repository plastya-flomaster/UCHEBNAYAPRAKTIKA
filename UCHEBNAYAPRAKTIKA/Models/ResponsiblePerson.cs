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
    using System.ComponentModel.DataAnnotations;

    public partial class ResponsiblePerson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResponsiblePerson()
        {
            this.Zakupkas = new HashSet<Zakupka>();
        }
        private string defPat = "�� �������";
        private string defNum = "�� ������";
        private string defE = "�� ������";
        private string defi = "�� ������o";
        private string fio = "�� �������";

        public System.Guid ResponsiblePersonKey { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic
        {
            get
            {
                return defPat;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    defPat = value;
                }
            }
        }
        public System.Guid ClientKey { get; set; }
        public string PhoneNumber {
            get
            {
                return defNum;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    defNum = value;
                }
            }
        }
        public string Email {
            get
            {
                return defE;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    defE = value;
                }
            }
        }
        public string AdditionalInfo
        {
            get
            {
                return defi;
            }
            set
            {
                if (!String.IsNullOrWhiteSpace(value))
                {
                    defi = value;
                }
            }
        }
        public bool Deleted { get; set; }
        public string FIO
            {
               get
            {
                return $"{this.LastName} {this.FirstName} {this.Patronymic}";
            }
        }
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakupka> Zakupkas { get; set; }

       
            
           
       
    }
}

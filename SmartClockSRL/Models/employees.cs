//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartClockSRL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class employees
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employees()
        {
            this.checkin = new HashSet<checkin>();
            this.checkout = new HashSet<checkout>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string address { get; set; }
        public Nullable<System.DateTime> birthdate { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public byte[] huella { get; set; }
        public Nullable<System.DateTime> admissionDate { get; set; }
        public Nullable<int> idPosition { get; set; }
        public byte[] images { get; set; }
        public string gender { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<checkin> checkin { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<checkout> checkout { get; set; }
        public virtual position position { get; set; }
    }
}

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
    
    public partial class users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Nullable<int> idRol { get; set; }
    
        public virtual roles roles { get; set; }
    }
}

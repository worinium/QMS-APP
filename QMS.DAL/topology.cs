//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QMS.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class topology
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public topology()
        {
            this.layers = new HashSet<layer>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int srid { get; set; }
        public double precision { get; set; }
        public bool hasz { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<layer> layers { get; set; }
    }
}
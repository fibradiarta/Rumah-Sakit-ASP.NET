//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebRumahSakit.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class JENIS_OBAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JENIS_OBAT()
        {
            this.OBATs = new HashSet<OBAT>();
        }
    
        public int JENIS_OBAT_ID { get; set; }
        public string NAMA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OBAT> OBATs { get; set; }
    }
}

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
    
    public partial class PEGAWAI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PEGAWAI()
        {
            this.RELASI_TRANSAKSI_PEGAWAI = new HashSet<RELASI_TRANSAKSI_PEGAWAI>();
        }
    
        public int PEGAWAI_ID { get; set; }
        public Nullable<int> ROLE_ID { get; set; }
        public string NAMA { get; set; }
        public string ALAMAT { get; set; }
        public string NO_TELP { get; set; }
        public Nullable<bool> JENIS_KELAMIN { get; set; }
        public Nullable<bool> STATUS { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
    
        public virtual ROLE ROLE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RELASI_TRANSAKSI_PEGAWAI> RELASI_TRANSAKSI_PEGAWAI { get; set; }
    }
}

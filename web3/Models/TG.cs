//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace web3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TG()
        {
            this.ThamGIas = new HashSet<ThamGIa>();
        }
    
        public int IDTG { get; set; }
        public string TenTG { get; set; }
        public string DiaChi { get; set; }
        public string TieuSu { get; set; }
        public string DienThoai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThamGIa> ThamGIas { get; set; }
    }
}

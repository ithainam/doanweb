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
    
    public partial class CTDH
    {
        public int IDDH { get; set; }
        public int IDSach { get; set; }
        public Nullable<int> Soluong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
    
        public virtual DH DH { get; set; }
        public virtual Sach Sach { get; set; }
    }
}
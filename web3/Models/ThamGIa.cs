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
    
    public partial class ThamGIa
    {
        public int IDSach { get; set; }
        public int IDTG { get; set; }
        public string vaitro { get; set; }
        public string vitri { get; set; }
    
        public virtual Sach Sach { get; set; }
        public virtual TG TG { get; set; }
    }
}

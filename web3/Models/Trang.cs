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
    using System.Linq;

    public partial class Trang
    {
        public int IDTrang { get; set; }
        public int IDChapter { get; set; }
        public string Image { get; set; }
        public int TSequence { get; set; }
    
        public virtual Chapter Chapter { get; set; }
        public static List<Trang> GetTrangs(int id)
        {
            QLSEntities db=new QLSEntities();
            return db.Trangs.Where(x => x.IDChapter == id).ToList();
        }
    }
}
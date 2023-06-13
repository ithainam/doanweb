using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web3.Models
{
    public class SuaTruyenViewModel
    {
        public int CurrentCSequence { get; set; }
        public int IDManga { get; set; }
        public string NameManga { get; set; }
        public int CSequence { get; set; }
        public int TotalChapters { get; set; }
    }
}
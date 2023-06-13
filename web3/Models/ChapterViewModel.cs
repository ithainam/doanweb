using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web3.Models
{
    public class ChapterViewModel
    {
        public int CurrentChapterID { get; set; }
        public int CurrentMangaID { get; set; }
        public int CurrentCSequence { get; set; }
        public int TotalChapters { get; set; }
        public List<Trang> Trangs { get; set; }
        public List<Chapter> Chapters { get; set; }
    }
}
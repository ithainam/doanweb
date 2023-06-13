using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using web3.Models;

namespace web3.Models
{
    public class GioHang
    {
        QLSEntities db = new QLSEntities();
        public int iMaSach { get; set; }
        public string sTenSach { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public int iCsequence { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public GioHang(int MaSach,int Csequence)
        {
            iMaSach = MaSach;
            Sach s = db.Saches.Single(n => n.IDSach == iMaSach);
            sTenSach = s.TenSach;
            sHinhAnh = s.AnhBia;
            dDonGia = double.Parse(s.Gia.ToString());
            iCsequence = Csequence;
            Chapter c = db.Chapters.Single(n => n.CSequence== iCsequence);
            iSoLuong = 1;

        }
    }
}
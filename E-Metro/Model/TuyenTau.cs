using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Metro.Model
{
    class TuyenTau
    {
        private String maTuyen;
        private String tenTuyen;
        private String maCongTy;
        private String gaDau;
        private String gaCuoi;
        private String loaiTuyen;
        private String gioBatDau;
        private String gioKetThuc;
        private int thoiGianChoTB;
        private String tinhTrang;
        private decimal giaVe;

        public String MaTuyen { get => maTuyen; set => maTuyen = value; }
        public String TenTuyen { get => tenTuyen; set => tenTuyen = value; }
        public String MaCongTy { get => maCongTy; set => maCongTy = value; }
        public String GaDau { get => gaDau; set => gaDau = value; }
        public String GaCuoi { get => gaCuoi; set => gaCuoi = value; }
        public String LoaiTuyen { get => loaiTuyen; set => loaiTuyen = value; }
        public String GioBatDau { get => gioBatDau; set => gioBatDau = value; }
        public String GioKetThuc { get => gioKetThuc; set => gioKetThuc = value; }
        public int ThoiGianChoTB { get => thoiGianChoTB; set => thoiGianChoTB = value; }
        public String TinhTrang { get => tinhTrang; set => tinhTrang = value; }
        public decimal GiaVe { get => giaVe; set => giaVe = value; }

        public TuyenTau ShallowCopy()
        {
            return (TuyenTau)this.MemberwiseClone();
        }
    }

    public enum LoaiTuyenEnum
    {
        Thuong = 0,
        TocHanh = 1,
    }

    public enum TinhTrangTuyenEnum
    {
        HoatDong = 0,
        KHoatDong = 1,
    }
}

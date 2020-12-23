using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Metro.Model
{
    class VeTau
    {
        private String maVe;
        private String maTuyen;
        private String loaiVe;
        private decimal giaVe;
        private String ngayMua;
        private String tinhTrang;

        public String Mave { get => maVe; set => maVe = value; }
        public String MaTuyen { get => maTuyen; set => maTuyen = value; }
        public String LoaiVe { get => loaiVe; set => loaiVe = value; }
        public decimal GiaVe { get => giaVe; set => giaVe = value; }
        public String NgayMua { get => ngayMua; set => ngayMua = value; }
        public String TinhTrang { get => tinhTrang; set => tinhTrang = value; }
    }

    public enum LoaiVeEnum
    {
        Thuong = 0,
        Thang = 1,
    }
}

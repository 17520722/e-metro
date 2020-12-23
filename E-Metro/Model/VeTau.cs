using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Metro.Model
{
    public class VeTau
    {
        private String maTuyen;
        private String giaVe;
        private String ngayMua;
        private String tinhTrang;
        private String loaiVe;
        public String maVe { get; set; }
        public String MaTuyen { get => maTuyen; set => maTuyen = value; }
        public String GiaVe { get => giaVe; set => giaVe = value; }
        public String NgayMua { get => ngayMua; set => ngayMua = value; }
        public String TinhTrang { get => tinhTrang; set => tinhTrang = value; }
        public String LoaiVe { get => loaiVe; set => loaiVe = value; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Metro.Model
{
    public class CongTy
    {
        private String maCongTy;
        private String tenCongTy;
        private String diaChiWeb;
        private String diaChiTruSo;
        private String sdt;

        public String MaCongTy { get => maCongTy; set => maCongTy = value; }
        public String TenCongTy { get => tenCongTy; set => tenCongTy = value; }
        public String DiaChiWeb { get => diaChiWeb; set => diaChiWeb = value; }
        public String DiaChiTruSo { get => diaChiTruSo; set => diaChiTruSo = value; }
        public String Sdt { get => sdt; set => sdt = value; }
    }
}

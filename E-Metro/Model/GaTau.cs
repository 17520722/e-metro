﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Metro.Model
{
    public class GaTau
    {
        private String maGaTau;
        private String tenGaTau;
        private String moTaViTri;
        private String trangThai;
        private String anhGaTau;

        public String MaGaTau { get => maGaTau; set => maGaTau = value; }
        public String TenGaTau { get => tenGaTau; set => tenGaTau = value; }
        public String MoTaViTri { get => moTaViTri; set => moTaViTri = value; }
        public String TrangThai { get => trangThai; set => trangThai = value; }
        public String AnhGaTau { get => anhGaTau; set => anhGaTau = value; }

        public GaTau ShallowCopy()
        {
            return (GaTau)this.MemberwiseClone();
        }
    }
    public enum TrangThaiEnum
    {
        HoatDong = 0,
        SuaChua = 1,
        KoHoatDong = 2,
    }
}

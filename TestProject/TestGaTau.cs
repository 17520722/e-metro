using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Metro.ViewModel;
using NUnit.Framework;
using E_Metro.Model;

namespace TestProject
{
    [TestFixture]
    class TestGaTau
    {
        GaTauViewModel gaTau;
        [SetUp]
        public void Setup()
        {
            gaTau = new GaTauViewModel();
        }
        [Test]
        public void TestGaTau1()
        {
            int result = gaTau.AddGaTau("gaTau001", "tenGa", "vitri", "binhThuong", "haha");
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void TestGaTau2()
        {
            int result = gaTau.AddGaTau("", "tenGa", "vitri", "binhThuong", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau3()
        {
            int result = gaTau.AddGaTau("gaTau001", "", "vitri", "binhThuong", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau4()
        {
            int result = gaTau.AddGaTau("gaTau001", "tenGa", "", "binhThuong", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau5()
        {
            int result = gaTau.AddGaTau("gaTau001", "tenGa", "vitri", "", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau6()
        {
            int result = gaTau.AddGaTau("gaTau001", "tenGa", "vitri", "binhThuong", "");
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void TestGaTau7()
        {
            int result = gaTau.AddGaTau("Mã ga tàu", "tenGa", "vitri", "binhThuong", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau8()
        {
            int result = gaTau.AddGaTau("gaTau001", "Tên ga tàu", "vitri", "binhThuong", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau9()
        {
            int result = gaTau.AddGaTau("gaTau001", "tenGa", "Mô Tả Vị Trí", "binhThuong", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau10()
        {
            int result = gaTau.AddGaTau("gaTau001", "tenGa", "vitri", "Trạng thái ga tàu", "haha");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestGaTau11()
        {
            int result = gaTau.AddGaTau("gaTau001", "tenGa", "vitri", "binhThuong", "Liên kết sơ đồ ga tàu");
            Assert.That(result, Is.EqualTo(0));
        }
        //Them Ga tau
        [Test]
        public void TestMySqlAddGaTau1()
        {
            GaTau gtau = new GaTau();
            gtau.MaGaTau = "gaTau001";
            gtau.TenGaTau = "Văn Thánh";
            gtau.MoTaViTri = "Quận 1";
            gtau.TrangThai = "binhThuong";
            gtau.AnhGaTau = "lienKet";
            int result = gaTau.MySqlAddGaTau(gtau);
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestMySqlAddGaTau2()
        {
            GaTau gtau = new GaTau();
            DateTime dateTime = DateTime.Now;
            gtau.MaGaTau = dateTime.ToString();
            gtau.TenGaTau = "Văn Thánh";
            gtau.MoTaViTri = "Quận 1";
            gtau.TrangThai = "binhThuong";
            gtau.AnhGaTau = "lienKet";
            int result = gaTau.MySqlAddGaTau(gtau);
            Assert.That(result, Is.EqualTo(1));
        }
    }
}

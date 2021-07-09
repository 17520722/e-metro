using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Metro.ViewModel;
using E_Metro.Model;
using NUnit.Framework;

namespace TestProject
{
    [TestFixture]
    public class TestCongTy
    {
        CongTyViewModel congTy;
        [SetUp]
        public void Setup()
        {
            congTy = new CongTyViewModel();
        }
        [Test]
        public void TestCongty1()
        {
            int result = congTy.AddCongTy("congty001", "ten", "diachiweb", "diachi", "0011");
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void TestCongty2()
        {
            int result = congTy.AddCongTy("", "ten", "diachiweb", "diachi", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty3()
        {
            int result = congTy.AddCongTy("congty001", "", "diachiweb", "diachi", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty4()
        {
            int result = congTy.AddCongTy("congty001", "ten", "", "diachi", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty5()
        {
            int result = congTy.AddCongTy("congty001", "ten", "diachiweb", "", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty6()
        {
            int result = congTy.AddCongTy("congty001", "ten", "diachiweb", "diachi", "");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty7()
        {
            int result = congTy.AddCongTy("Mã công ty", "ten", "diachiweb", "diachi", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty8()
        {
            int result = congTy.AddCongTy("congty001", "Tên công ty", "diachiweb", "diachi", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty9()
        {
            int result = congTy.AddCongTy("congty001", "ten", "Địa chỉ website", "diachi", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty10()
        {
            int result = congTy.AddCongTy("congty001", "ten", "diachiweb", "Địa chỉ trụ sở chính", "0011");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty11()
        {
            int result = congTy.AddCongTy("congty001", "ten", "diachiweb", "diachi", "Số điện thoại");
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestCongty12()
        {
            int result = congTy.AddCongTy("congty001", "ten", "diachiweb", "diachi", "sodienthoai");
            Assert.That(result, Is.EqualTo(0));
        }
        // MySQL them cong ty
        [Test]
        public void TestMySqlAddCongTy1()
        {
            CongTy cty = new CongTy();
            cty.MaCongTy = "congty001";
            cty.TenCongTy = "ten";
            cty.DiaChiWeb = "diachiweb";
            cty.DiaChiTruSo = "diachi";
            cty.Sdt = "0011";
            int result = congTy.MySqlAddCongTy(cty);
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestMySqlAddCongTy2()
        {
            CongTy cty = new CongTy();
            DateTime dateTime = DateTime.Now; 
            cty.MaCongTy = dateTime.ToString();
            cty.TenCongTy = "ten";
            cty.DiaChiWeb = "www.diachi.com";
            cty.DiaChiTruSo = "Vinh Long";
            cty.Sdt = "123123";
            int result = congTy.MySqlAddCongTy(cty);
            Assert.That(result, Is.EqualTo(1));
        }
        //Hàm tìm kiếm công ty
        [Test]
        public void TestSearchCongTy1()
        {
            String searchText = "";
            int result = congTy.SearchCongTy(searchText);
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestSearchCongTy2()
        {
            String searchText = "Bạn cần gì...";
            int result = congTy.SearchCongTy(searchText);
            Assert.That(result, Is.EqualTo(0));
        }
        [Test]
        public void TestSearchCongTy3()
        {
            String searchText = "Co";
            int result = congTy.SearchCongTy(searchText);
            Assert.That(result, Is.EqualTo(1));
        }
    }
}

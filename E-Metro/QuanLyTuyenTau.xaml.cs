using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace E_Metro
{
    /// <summary>
    /// Interaction logic for QuanLyTuyenTau.xaml
    /// </summary>
    public partial class QuanLyTuyenTau : Window
    {
        public QuanLyTuyenTau()
        {
            InitializeComponent();
        }

        private void maTuyenUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (maTuyenUpdate_txt.Text == "Mã tuyến")
                maTuyenUpdate_txt.Text = "";
        }

        private void maTuyen_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (maTuyen_txt.Text == "Mã tuyến")
                maTuyen_txt.Text = "";
        }

        private void maTuyen_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maTuyen_txt.Text))
                maTuyen_txt.Text = "Mã tuyến";
        }

        private void tenTuyen_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tenTuyen_txt.Text == "Tên tuyến")
                tenTuyen_txt.Text = "";
        }

        private void tenTuyen_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tenTuyen_txt.Text))
                tenTuyen_txt.Text = "Tên tuyến";
        }

        private void thoiGianCho_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (thoiGianCho_txt.Text == "Khoảng thời gian chờ")
                thoiGianCho_txt.Text = "";
        }

        private void thoiGianCho_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(thoiGianCho_txt.Text))
                thoiGianCho_txt.Text = "Khoảng thời gian chờ";
        }

        private void giaVe_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (giaVe_txt.Text == "Giá vé")
                giaVe_txt.Text = "";
        }

        private void giaVe_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(giaVe_txt.Text))
                giaVe_txt.Text = "Giá vé";
        }
        //***************************CAPNHAT******************************

        private void tenCongTyUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tenCongTyUpdate_txt.Text == "Tên công ty vận hành")
                tenCongTyUpdate_txt.Text = "";
        }

        private void tenCongTyUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tenCongTyUpdate_txt.Text))
                tenCongTyUpdate_txt.Text = "Tên công ty vận hành";
        }

        private void maTuyenUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maTuyenUpdate_txt.Text))
                maTuyenUpdate_txt.Text = "Mã tuyến";
        }

        private void tenTuyenUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tenTuyenUpdate_txt.Text == "Tên tuyến")
                tenTuyenUpdate_txt.Text = "";
        }

        private void tenTuyenUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tenTuyenUpdate_txt.Text))
                tenTuyenUpdate_txt.Text = "Tên tuyến";
        }

        private void gioBatDauUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (gioBatDauUpdate_txt.Text == "Giờ bắt đầu")
                gioBatDauUpdate_txt.Text = "";
        }

        private void gioBatDauUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gioBatDauUpdate_txt.Text))
                gioBatDauUpdate_txt.Text = "Giờ bắt đầu";
        }

        private void gioKetThucUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (gioKetThucUpdate_txt.Text == "Giờ kết thúc")
                gioKetThucUpdate_txt.Text = "";
        }

        private void gioKetThucUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gioKetThucUpdate_txt.Text))
                gioKetThucUpdate_txt.Text = "Giờ kết thúc";
        }

        private void thoiGianChoUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (thoiGianChoUpdate_txt.Text == "Khoảng thời gian chờ")
                thoiGianChoUpdate_txt.Text = "";
        }

        private void thoiGianChoUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(thoiGianChoUpdate_txt.Text))
                thoiGianChoUpdate_txt.Text = "Khoảng thời gian chờ";
        }

        private void giaVeUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (giaVeUpdate_txt.Text == "Giá vé")
                giaVeUpdate_txt.Text = "";
        }

        private void giaVeUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(giaVeUpdate_txt.Text))
                giaVeUpdate_txt.Text = "Giá vé";
        }

        private void gaDauUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (gaDauUpdate_txt.Text == "Ga đầu")
                gaDauUpdate_txt.Text = "";
        }

        private void gaDauUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gaDauUpdate_txt.Text))
                gaDauUpdate_txt.Text = "Ga đầu";
        }

        private void gaCuoiUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (gaCuoiUpdate_txt.Text == "Ga cuối")
                gaCuoiUpdate_txt.Text = "";
        }

        private void gaCuoiUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(gaCuoiUpdate_txt.Text))
                gaCuoiUpdate_txt.Text = "Ga cuối";
        }

        private void tinhTrangUpdate_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tinhTrangUpdate_txt.Text == "Tình trạng")
                tinhTrangUpdate_txt.Text = "";
        }

        private void tinhTrangUpdate_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tinhTrangUpdate_txt.Text))
                tinhTrangUpdate_txt.Text = "Tình trạng";
        }

        private void tenCongTy_cbx_MouseEnter(object sender, MouseEventArgs e)
        {
            tenCongTy_cbx.IsDropDownOpen = true;
        }

        private void tenCongTy_cbx_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //tenCongTy_cbx.IsDropDownOpen = true;
        }

        private void gaDau_cbx_MouseEnter(object sender, MouseEventArgs e)
        {
            gaDau_cbx.IsDropDownOpen = true;
        }

        private void gaCuoi_cbx_MouseEnter(object sender, MouseEventArgs e)
        {
            gaCuoi_cbx.IsDropDownOpen = true;
        }
    }
}

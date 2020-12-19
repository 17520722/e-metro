using E_Metro.Model;
using E_Metro.ViewModel;
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
            if (thoiGianCho_txt.Text == "Khoảng thời gian chờ (phút)")
                thoiGianCho_txt.Text = "";
        }

        private void thoiGianCho_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(thoiGianCho_txt.Text))
                thoiGianCho_txt.Text = "Khoảng thời gian chờ (phút)";
        }

        private void giaVe_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (giaVe_txt.Text == "Giá vé (vnđ)")
                giaVe_txt.Text = "";
        }

        private void giaVe_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(giaVe_txt.Text))
                giaVe_txt.Text = "Giá vé (vnđ)";
        }
        //***************************CAPNHAT******************************

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

        //*********************Xu ly ki tu so ***********************//

        private void thoiGianCho_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
        }

        private void giaVe_txt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
        }

        private bool AreAllValidNumericChars(string str)
        {
            foreach (char c in str)
            {
                if (!Char.IsNumber(c)) return false;
            }

            return true;
        }
        //*********************UPDATE***************************//
        private void handle_Update_Click(object sender, RoutedEventArgs e)
        {
            TuyenTau a = (TuyenTau)tuyenTau_dataGrid.SelectedItem;
            viewModel.CloneContentTuyen(a);

            update_tab.IsSelected = true;
        }

        //**********************Drop down text************************/

        private void gaDau_cbx_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gaDau_cbx.IsDropDownOpen = true;
        }

        private void gaCuoi_cbx_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gaCuoi_cbx.IsDropDownOpen = true;
        }

        private void gaDauUpdate_txt_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gaDauUpdate_cbx.IsDropDownOpen = true;
        }

        private void gaCuoiUpdate_txt_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gaCuoiUpdate_cbx.IsDropDownOpen = true;
        }

        private void tenCongTy_cbx_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tenCongTy_cbx.IsDropDownOpen = true;
        }

        private void tenCongTyUpdate_cbx_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tenCongTyUpdate_cbx.IsDropDownOpen = true;
        }

        private void tuyenTau_dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedTuyenTau = (TuyenTau)tuyenTau_dataGrid.SelectedItem;
        }
        //**********Search*************
        private void searchTuyenTau_txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void searchTuyenTau_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTuyenTau_txt.Text == "Bạn cần gì...")
                searchTuyenTau_txt.Text = "";
        }

        private void searchTuyenTau_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTuyenTau_txt.Text))
                searchTuyenTau_txt.Text = "Bạn cần gì...";
        }
    }
}

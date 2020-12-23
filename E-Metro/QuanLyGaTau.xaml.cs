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
    /// Interaction logic for QuanLyGaTau.xaml
    /// </summary>
    public partial class QuanLyGaTau : Window
    {
        public QuanLyGaTau()
        {
            InitializeComponent();
        }

        private void maGaTau_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (maGaTau_txt.Text == "Mã ga tàu")
                maGaTau_txt.Text = "";
        }

        private void maGaTau_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (maGaTau_txt.Text == "")
                maGaTau_txt.Text = "Mã ga tàu";
        }

        private void tenGaTau_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tenGaTau_txt.Text == "Tên ga tàu")
                tenGaTau_txt.Text = "";
        }

        private void tenGaTau_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tenGaTau_txt.Text == "")
                tenGaTau_txt.Text = "Tên ga tàu";
        }

        private void moTaViTri_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (moTaViTri_txt.Text == "Mô tả vị trí")
                moTaViTri_txt.Text = "";
        }

        private void moTaViTri_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (moTaViTri_txt.Text == "")
                moTaViTri_txt.Text = "Mô tả vị trí";
        }

        private void lienKetSoDoGaTau_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (lienKetSoDoGaTau_txt.Text == "Liên kết sơ đồ ga tàu")
                lienKetSoDoGaTau_txt.Text = "";
        }

        private void lienKetSoDoGaTau_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (lienKetSoDoGaTau_txt.Text == "")
                lienKetSoDoGaTau_txt.Text = "Liên kết sơ đồ ga tàu";
        }

        private void TrangThai_cbb_MouseUp(object sender, MouseButtonEventArgs e)
        {
            trangThai_cbb.IsDropDownOpen = true;
        }

        private void SearchGaTau_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchGaTau_txt.Text == "Bạn cần gì...")
                searchGaTau_txt.Text = "";
        }

        private void SearchGaTau_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchGaTau_txt.Text))
                searchGaTau_txt.Text = "Bạn cần gì...";
        }

        private void SearchGaTau_txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void SearchGaTau2_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchGaTau2_txt.Text == "Bạn cần gì...")
                searchGaTau2_txt.Text = "";
        }

        private void SearchGaTau2_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchGaTau2_txt.Text))
                searchGaTau2_txt.Text = "Bạn cần gì...";
        }

        private void SearchGaTau2_txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }

        private void SuaGaTau_dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            viewModel.UpdateGaTau(e, suaGaTau_dataGrid);
        }
    }
}

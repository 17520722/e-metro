using E_Metro.Model;
using E_Metro.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for QuanLyCongTy.xaml
    /// </summary>
    public partial class QuanLyCongTy : Window
    {
        public QuanLyCongTy()
        {
            InitializeComponent();
        }

        private void maCongTy_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (maCongTy_txt.Text == "Mã công ty")
                maCongTy_txt.Text = "";
        }

        private void maCongTy_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(maCongTy_txt.Text))
                maCongTy_txt.Text = "Mã công ty";
        }

        private void tenCongTy_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tenCongTy_txt.Text == "Tên công ty")
                tenCongTy_txt.Text = "";
        }

        private void tenCongTy_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tenCongTy_txt.Text))
                tenCongTy_txt.Text = "Tên công ty";
        }

        private void diaChiWeb_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (diaChiWeb_txt.Text == "Địa chỉ website")
                diaChiWeb_txt.Text = "";
        }

        private void diaChiWeb_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(diaChiWeb_txt.Text))
                diaChiWeb_txt.Text = "Địa chỉ website";
        }

        private void truSo_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (truSo_txt.Text == "Địa chỉ trụ sở chính")
                truSo_txt.Text = "";
        }

        private void truSo_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(truSo_txt.Text))
                truSo_txt.Text = "Địa chỉ trụ sở chính";
        }

        private void sdt_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sdt_txt.Text == "Số điện thoại")
                sdt_txt.Text = "";
        }

        private void sdt_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sdt_txt.Text))
                sdt_txt.Text = "Số điện thoại";
        }

        private void searchCongTy_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchCongTy_txt.Text == "Bạn cần gì...")
                searchCongTy_txt.Text = "";
        }

        private void searchCongTy_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchCongTy_txt.Text))
                searchCongTy_txt.Text = "Bạn cần gì...";
        }

        private void searchCongTy2_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchCongTy2_txt.Text == "Bạn cần gì...")
                searchCongTy2_txt.Text = "";
        }

        private void searchCongTy2_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchCongTy2_txt.Text))
                searchCongTy2_txt.Text = "Bạn cần gì...";
        }

        private void suaCongTy_dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var viewModel = new CongTyViewModel();       
            viewModel.UpdateCongTy(e, suaCongTy_dataGrid);

        }

        private void searchCongTy2_txt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
            }
        }
    }
}

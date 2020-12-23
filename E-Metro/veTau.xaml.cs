using E_Metro.Model;
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
    /// Interaction logic for veTau.xaml
    /// </summary>
    public partial class veTau : Window
    {
        public veTau()
        {
            InitializeComponent();
        }

        private void tenCongTy_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tenCongTy_txt.Text == "Tên công ty")
                tenCongTy_txt.Text = "";
        }

        private void tenCongTy_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tenCongTy_txt.Text))
                tenCongTy_txt.Text = "Tên công ty";
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

        private void soLuongVe_txt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (soLuongVe_txt.Text == "Số lượng")
                soLuongVe_txt.Text = "";
        }

        private void soLuongVe_txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(soLuongVe_txt.Text))
                soLuongVe_txt.Text = "Số lượng";
        }

        private void tuyenTau_cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TuyenTau tuyenTau = (TuyenTau)tuyenTau_cbx.SelectedItem;
            viewModel.LoadDataTicket(addVeTau_stp, tuyenTau);
        }

        private void tuyenTau_cbx_MouseUp(object sender, MouseButtonEventArgs e)
        {
            tuyenTau_cbx.IsDropDownOpen = true;
        }
    }
}

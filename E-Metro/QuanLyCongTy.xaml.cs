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
    /// Interaction logic for QuanLyCongTy.xaml
    /// </summary>
    public partial class QuanLyCongTy : Window
    {
        public QuanLyCongTy()
        {
            InitializeComponent();

            CongTy TNHH1 = new CongTy();

            TNHH1.congTyId = "001";
            TNHH1.tenCongTy = "Tranh Nhiem";
            TNHH1.diaChiWeb = "www.aaa.aa.b";
            TNHH1.truSo = "HCM";
            TNHH1.soDienThoai = "011122444";

            CongTy TNHH2 = new CongTy();

            TNHH2.congTyId = "001";
            TNHH2.tenCongTy = "AAAAAAAa";
            TNHH2.diaChiWeb = "www.goog.cab";
            TNHH2.truSo = "HCM";
            TNHH2.soDienThoai = "01201021210";

            congTy_dataGrid.Items.Add(TNHH1);
            congTy_dataGrid.Items.Add(TNHH2);

            suaCongTy_dataGrid.Items.Add(TNHH1);
            suaCongTy_dataGrid.Items.Add(TNHH2);
        }
        public class CongTy
        {
            public string congTyId { get; set; }
            public string tenCongTy { get; set; }
            public string diaChiWeb { get; set; }
            public string truSo { get; set; }
            public string soDienThoai { get; set; }

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
    }
}

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

        public class GaTau
        {
            public string gaTauId { get; set; }
            public string tenGaTau { get; set; }
            public string moTaViTri { get; set; }
            public string trangThai { get; set; }
            public string lienKetSoDo { get; set; }
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
    }
}

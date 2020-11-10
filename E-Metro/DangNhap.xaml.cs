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
    /// Interaction logic for DangNhap.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        public int re = 0;
        public DangNhap()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        public int Login()
        {
            if (username_txt.Text.Trim() == "luan" && password_txt.Password.Trim() == "123")
                return 1;
            return 0;
        }

        private void dangNhap_Click(object sender, RoutedEventArgs e)
        {
            re = Login();
            this.Close();
        }
        // place holder for taikhoan va matkhau

        private void username_GotFocus(object sender, RoutedEventArgs e)
        {
            if (username_txt.Text == "Tài khoản")
                username_txt.Text = "";
        }

        private void username_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username_txt.Text))
                username_txt.Text = "Tài khoản";
        }

        private void passwordv2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (password_txb.Text == "Mật khẩu")
                password_txb.Text = "";
        }

        private void passwordv2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password_txt.Password))
                password_txb.Text = "Mật khẩu";
        }
    }
}

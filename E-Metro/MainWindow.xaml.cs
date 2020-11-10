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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace E_Metro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DangNhap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            DangNhap dangnhap = new DangNhap();
            dangnhap.ShowDialog();
            if (dangnhap.re == 1)
            {
                border_dangNhap_btn.Visibility = Visibility.Hidden;
                border_dangXuat_btn.Visibility = Visibility.Visible;
            }            
            this.Show();
        }

        private void dangXuat_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

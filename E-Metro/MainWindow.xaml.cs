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

        private void quanLyCongTy_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            QuanLyCongTy quanLyCongTy = new QuanLyCongTy();          
            quanLyCongTy.ShowDialog();
            this.Show();
        }

        private void quanLyGaTau_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            QuanLyGaTau quanLyGaTau = new QuanLyGaTau();
            quanLyGaTau.ShowDialog();
            this.Show();
        }

        private void quanLyTuyenTau_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            QuanLyTuyenTau quanLyTuyenTau = new QuanLyTuyenTau();
            quanLyTuyenTau.ShowDialog();
            this.Show();
        }

        private void DockPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Grid_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Grid_PreviewMouseDown_2(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

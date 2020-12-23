using E_Metro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace E_Metro.ViewModel
{
    public class VeViewModel
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        private ObservableCollection<VeTau> listVeTau;
        public ObservableCollection<VeTau> ListVeTau { get => listVeTau; set => listVeTau = value; }



        public VeViewModel()
        {
            ListVeTau = new ObservableCollection<VeTau>();
            AddCommand = new RelayCommand<UIElementCollection>((p) => true, AddVeTau);
            
        }
        // processor
        public void AddVeTau(UIElementCollection p)
        {
            String maTuyen = "";
            String loaiVe = "";
            String tinhTrang = "";
            String giaVe="";
            String ngayMua=""  ;

            foreach (var item in p)
            {
                TextBox textB = item as TextBox;
                if (textB == null)
                    continue;
                switch (textB.Name)
                {
                    case "tuyenTau_txt":
                        maTuyen = textB.Text;
                        break;
                    case "loaiVe_txt":
                        loaiVe = textB.Text;
                        break;
                    case "giaVe_txt":
                        giaVe = textB.Text;
                        break;
                    case "tinhTrang_txt":
                        tinhTrang = textB.Text;
                        break;
                    case "ngayMua_txt":
                        ngayMua = textB.Text;
                        break;
                }
            }
          

           
           
            VeTau vt = new VeTau()
            {
                LoaiVe = loaiVe,
                MaTuyen = maTuyen,
                GiaVe = giaVe,
                TinhTrang = tinhTrang,
                NgayMua = ngayMua,
               
            };

            MySqlAddVeTau(vt);
        }
        public void MySqlAddVeTau(VeTau veTau)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "insert into CongTy(loaiVe, maTuyen,giaVe, ngayMua, tinhTrang) " +
                            "values ('" + veTau.LoaiVe + "', N'" + veTau.MaTuyen + "', '" + veTau.GiaVe + "','" +veTau.NgayMua + "', '" + veTau.TinhTrang + "', )";

                        var reader = command.ExecuteReader();

                        ListVeTau.Add(veTau);
                        MessageBox.Show("upload success") ;
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Connection Fail!");
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Console.WriteLine("Disconnection Success!");
                }
            }
        }
        public ICommand AddCommand { get; set; }
    }
}
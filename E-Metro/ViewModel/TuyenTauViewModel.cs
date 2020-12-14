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
using System.Windows.Input;

namespace E_Metro.ViewModel
{
    class TuyenTauViewModel
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
       
        private ObservableCollection<TuyenTau> listTuyenTau;
        public ObservableCollection<TuyenTau> ListTuyenTau { get => listTuyenTau; set => listTuyenTau = value; }

        private ObservableCollection<TinhTrangTuyenTau> listTinhTrang;
        public ObservableCollection<TinhTrangTuyenTau> ListTinhTrang { get => listTinhTrang; set => listTinhTrang = value; }

        private ObservableCollection<CongTy> listCongTy;
        public ObservableCollection<CongTy> ListCongTy { get => listCongTy; set => listCongTy = value; }

        private ObservableCollection<GaTau> listGaTau;
        public ObservableCollection<GaTau> ListGaTau { get => listGaTau; set => listGaTau = value; }

        private String trangThaiTuyen;
        public String TrangThaiTuyen { get => trangThaiTuyen; set => trangThaiTuyen = value; }

        public LoaiTuyenEnum loaiTuyen;

        public TuyenTauViewModel()
        {
            listTuyenTau = new ObservableCollection<TuyenTau>();
            listTinhTrang = new ObservableCollection<TinhTrangTuyenTau>();
            listCongTy = new ObservableCollection<CongTy>();
            listGaTau = new ObservableCollection<GaTau>();
            MySqlLoadTinhTrangTuyen();
            MySqlLoadCongTy();
            MySqlLoadGaTau();
            TrangThaiTuyen = ListTinhTrang[0].TinhTrang;
            loaiTuyen = LoaiTuyenEnum.Thuong;
            DoiTrangThaiCommand = new RelayCommand<UIElement>((p) => true, ChangeTinhTrangTuyen);
            DoiLoaiTuyenCommand = new RelayCommand<UIElement>((p) => true, ChangeLoaiTuyen);
        }

        private void AddTuyenTau(UIElementCollection p)
        {
            foreach (var item in p)
            {
                WrapPanel wp_pn = item as WrapPanel;
                foreach (var i in wp_pn.Children)
                {
                    TextBox textBox = i as TextBox;
                    if (textBox == null)
                        continue;
                    switch (textBox.Name)
                    {

                    }
                }
            }
        }

        public void ChangeTinhTrangTuyen(UIElement p)
        {
            TextBox text = p as TextBox;
            if (TrangThaiTuyen == ListTinhTrang[0].TinhTrang)
                text.Text = ListTinhTrang[1].TinhTrang;
            else
                text.Text = ListTinhTrang[0].TinhTrang;
            TrangThaiTuyen = text.Text;
        }

        public void ChangeLoaiTuyen(UIElement p)
        {
            TextBox text = p as TextBox;
            if (loaiTuyen == LoaiTuyenEnum.Thuong)
            {
                text.Text = "Loại tuyến: Tốc hành";
                loaiTuyen = LoaiTuyenEnum.TocHanh;
            }
            else
            {
                text.Text = "Loại tuyến: Thường";
                loaiTuyen = LoaiTuyenEnum.Thuong;
            }
        }

        public void MySqlLoadTinhTrangTuyen()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListTinhTrang.Clear();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from TinhTrangTuyenTau";

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            TinhTrangTuyenTau tinhTrang = new TinhTrangTuyenTau()
                            {
                                MaTTTuyenTau = reader["maTTTuyenTau"].ToString(),
                                TinhTrang = reader["tinhTrang"].ToString(),
                            };

                            ListTinhTrang.Add(tinhTrang);
                        }
                        Console.WriteLine("Load du lieu tinh trang tuyen tau thanh cong !!!!!!!!!!!!!!");
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

        public void MySqlLoadCongTy()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListCongTy.Clear();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from CongTy";

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            CongTy congTy = new CongTy()
                            {
                                MaCongTy = reader["maCongTy"].ToString(),
                                TenCongTy = reader["tenCongTy"].ToString(),
                                DiaChiWeb = reader["diaChiWeb"].ToString(),
                                DiaChiTruSo = reader["diaChiTruSo"].ToString(),
                                Sdt = reader["sdt"].ToString()
                            };

                            ListCongTy.Add(congTy);
                        }
                        Console.WriteLine("Load du lieu cong ty thanh cong !!!!!!!!!!!!!!");
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

        public void MySqlLoadGaTau()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListGaTau.Clear();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from GaTau";

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            GaTau gaTau = new GaTau()
                            {
                                MaGaTau = reader["maGaTau"].ToString(),
                                TenGaTau = reader["tenGaTau"].ToString(),
                                MoTaViTri = reader["moTaViTri"].ToString(),
                                TrangThai = reader["trangThai"].ToString(),
                                AnhGaTau = reader["anhGaTau"].ToString()
                            };

                            ListGaTau.Add(gaTau);
                        }
                        Console.WriteLine("Load du lieu ga tau thanh cong !!!!!!!!!!!!!!");
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

        public ICommand DoiTrangThaiCommand { get; set; }
        public ICommand DoiLoaiTuyenCommand { get; set; }
    }
}

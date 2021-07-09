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
    class VeTauViewModel
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        private ObservableCollection<VeTau> listVeTau;
        public ObservableCollection<VeTau> ListVeTau { get => listVeTau; set => listVeTau = value; }

        private ObservableCollection<TuyenTau> listTuyenTau;
        public ObservableCollection<TuyenTau> ListTuyenTau { get => listTuyenTau; set => listTuyenTau = value; }

        private ObservableCollection<CongTy> listCongTy;
        public ObservableCollection<CongTy> ListCongTy { get => listCongTy; set => listCongTy = value; }

        LoaiVeEnum loaiVeEnum;

        public VeTauViewModel()
        {
            ListVeTau = new ObservableCollection<VeTau>();
            ListTuyenTau = new ObservableCollection<TuyenTau>();
            ListCongTy = new ObservableCollection<CongTy>();
            MySqlLoadTuyenTau();
            MySqlLoadCongTy();
            loaiVeEnum = LoaiVeEnum.Thuong;
            AddCommand = new RelayCommand<UIElementCollection>((p) => true, AddVeTau);
            ChangeTypeOfTicketCommand = new RelayCommand<UIElement>((p) => true, ChangeTypeOfTicket);

        }
        // processor
        private void AddVeTau(UIElementCollection p)
        {
            String maTuyen = "";
            String loaiVe = "";
            String tinhTrang = "";
            decimal giaVe = 0;
            String ngayMua = "";
            int soLuong = 0;

            foreach (var item in p)
            {
                if (item is TextBox)
                {
                    TextBox textB = item as TextBox;
                    switch (textB.Name)
                    {
                        case "giaVe_txt":
                            if (string.IsNullOrWhiteSpace(textB.Text) || textB.Text == "Giá vé (vnđ)")
                                giaVe = 0;
                            else
                                giaVe = decimal.Parse(textB.Text);
                            break;
                        case "soLuongVe_txt":
                            if (string.IsNullOrWhiteSpace(textB.Text) || textB.Text == "Số lượng")
                                soLuong = 0;
                            else
                                soLuong = int.Parse(textB.Text);
                            break;
                    }
                }
                else if (item is StackPanel)
                {
                    StackPanel stack = item as StackPanel;
                    foreach (var i in stack.Children)
                    {
                        if (i is TextBox)
                        {
                            TextBox text = i as TextBox;
                            loaiVe = loaiVeEnum.ToString();
                        }
                    }
                }
                else if (item is ComboBox)
                {
                    ComboBox box = item as ComboBox;
                    if (box.SelectedItem == null) return;

                    TuyenTau tu = (TuyenTau)box.SelectedItem;
                    maTuyen = tu.MaTuyen;
                }
            }

            ngayMua = DateTime.Now.ToString("yyyy/MM/dd");

            if (string.IsNullOrEmpty(maTuyen) || string.IsNullOrEmpty(loaiVe) || string.IsNullOrEmpty(ngayMua) || giaVe <= 0 || soLuong <= 0)
            {
                return;
            }
            if (string.Compare(maTuyen, "Mã công ty") == 0 || string.Compare(loaiVe, "Tên công ty") == 0 || string.Compare(soLuong.ToString(), "Số lượng") == 0||
                string.Compare(ngayMua, "Địa chỉ website") == 0 || string.Compare(giaVe.ToString(), "Giá vé (vnđ)") == 0)
            {
                return;
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
        private void ChangeTypeOfTicket(UIElement p)
        {
            TextBox textBox = p as TextBox;
            if (loaiVeEnum == LoaiVeEnum.Thuong)
            {
                textBox.Text = "Loại vé: Tháng";
                loaiVeEnum = LoaiVeEnum.Thang;
            }
            else
            {
                textBox.Text = "Loại vé: Thường";
                loaiVeEnum = LoaiVeEnum.Thuong;
            }
        }
        
        public void LoadDataTicket(StackPanel stack, TuyenTau tuyenTau)
        {
            foreach (var item in stack.Children)
            {
                if (item is TextBox)
                {
                    TextBox text = item as TextBox;
                    switch (text.Name)
                    {
                        case "tenCongTy_txt":
                            foreach (var i in ListCongTy)
                            {
                                if (tuyenTau.MaCongTy == i.MaCongTy)
                                {
                                    text.Text = i.TenCongTy;
                                    break;
                                }
                            }
                            break;
                        case "giaVe_txt":
                            text.Text = tuyenTau.GiaVe.ToString();
                            break;
                    }
                }
            }
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
                        command.CommandText = "insert into VeTau(maTuyen, loaiVe, giaVe, ngayMua, tinhTrang) " +
                            "values ('" + veTau.MaTuyen + "', N'" + veTau.LoaiVe + "', '" + veTau.GiaVe + "','" + veTau.NgayMua + "','" + veTau.TinhTrang + "')";

                        var reader = command.ExecuteReader();

                        ListVeTau.Add(veTau);
                        MessageBox.Show("Đã in vé!");
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
        public void MySqlLoadTuyenTau()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListTuyenTau.Clear();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from TuyenTau";

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            TuyenTau tuyenTau = new TuyenTau()
                            {
                                MaTuyen = reader["maTuyen"].ToString(),
                                TenTuyen = reader["tenTuyen"].ToString(),
                                MaCongTy = reader["maCongTy"].ToString(),
                                GaDau = reader["gaDau"].ToString(),
                                GaCuoi = reader["gaCuoi"].ToString(),
                                LoaiTuyen = reader["loaiTuyen"].ToString(),
                                GioBatDau = reader["gioBatDau"].ToString(),
                                GioKetThuc = reader["gioKetThuc"].ToString(),
                                ThoiGianChoTB = int.Parse(reader["thoiGianChoTB"].ToString()),
                                TinhTrang = reader["tinhTrang"].ToString(),
                                GiaVe = decimal.Parse(reader["giaVe"].ToString()),
                            };

                            ListTuyenTau.Add(tuyenTau);
                        }
                        Console.WriteLine("Load du lieu tuyen tau thanh cong !!!!!!!!!!!!!!");
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Connection Fail!");
                    System.Windows.MessageBox.Show(ex.ToString());
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
                    System.Windows.MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Console.WriteLine("Disconnection Success!");
                }
            }
        }


        public ICommand AddCommand { get; set; }
        public ICommand ChangeTypeOfTicketCommand { get; set; }
    }
}

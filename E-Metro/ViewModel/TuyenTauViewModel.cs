using E_Metro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace E_Metro.ViewModel
{
    class TuyenTauViewModel
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private TuyenTau selectedTuyenTau { get; set; }
        public TuyenTau SelectedTuyenTau
        {
            get { return selectedTuyenTau; }
            set
            {
                selectedTuyenTau = value;
                if (selectedTuyenTau != value)
                {
                    OnPropertyChanged("SelectedTuyenTau");
                }
            }
        }

        private ObservableCollection<TuyenTau> listTuyenTau;
        public ObservableCollection<TuyenTau> ListTuyenTau { get => listTuyenTau; set => listTuyenTau = value; }

        private ObservableCollection<TuyenTau> listSearchTuyenTau;
        public ObservableCollection<TuyenTau> ListSearchTuyenTau
        {
            get => listSearchTuyenTau;
            set
            {
                if (listSearchTuyenTau == value) return;
                listSearchTuyenTau = value;
            }
        }

        private ObservableCollection<CongTy> listCongTy;
        public ObservableCollection<CongTy> ListCongTy { get => listCongTy; set => listCongTy = value; }

        private ObservableCollection<GaTau> listGaTau;
        public ObservableCollection<GaTau> ListGaTau { get => listGaTau; set => listGaTau = value; }

        public TinhTrangTuyenEnum tinhTrangTuyenEnum;

        public LoaiTuyenEnum loaiTuyenEnum;

        public TuyenTauViewModel()
        {
            listTuyenTau = new ObservableCollection<TuyenTau>();
            listSearchTuyenTau = new ObservableCollection<TuyenTau>();
            listCongTy = new ObservableCollection<CongTy>();
            listGaTau = new ObservableCollection<GaTau>();

            MySqlLoadCongTy();
            MySqlLoadGaTau();
            MySqlLoadDataTuyenTau();
            tinhTrangTuyenEnum = TinhTrangTuyenEnum.HoatDong;
            loaiTuyenEnum = LoaiTuyenEnum.Thuong;
            AddCommand = new RelayCommand<UIElementCollection>((p) => true, AddTuyenTau);
            UpdateCommand = new RelayCommand<UIElementCollection>((p) => true, UpdateTuyenTau);
            SearchCommand = new RelayCommand<UIElement>((p) => true, SearchTuyenTau);
            FillUpdateText = new RelayCommand<UIElementCollection>((p) => true, FillTextTuyenTau);
            DoiTrangThaiCommand = new RelayCommand<UIElement>((p) => true, ChangeTinhTrangTuyen);
            DoiLoaiTuyenCommand = new RelayCommand<UIElement>((p) => true, ChangeLoaiTuyen);
        }

        private TuyenTau HandleDataAdd(UIElementCollection p)
        {
            String maTuyen = "";
            String tenTuyen = "";
            String maCongTy = "";
            String gaDau = "";
            String gaCuoi = "";
            String loaiTuyen = "";
            String gioBatDau = "";
            String gioKetThuc = "";
            int thoiGianChoTB = 0;
            String tinhTrang = "";
            decimal giaVe = 0;

            foreach (var item in p)
            {
                if (!(item is WrapPanel)) continue;

                WrapPanel wp_pn = item as WrapPanel;
                foreach (var i in wp_pn.Children)
                {
                    if (i is ComboBox)
                    {
                        ComboBox comboBox = i as ComboBox;

                        if (comboBox.Text == null) continue;

                        var inputText = "";
                        if (comboBox.SelectedItem is CongTy)
                        {
                            CongTy ct = (CongTy)comboBox.SelectedItem;
                            inputText = ct.MaCongTy;
                        }
                        if (comboBox.SelectedItem is GaTau)
                        {
                            GaTau gt = (GaTau)comboBox.SelectedItem;
                            inputText = gt.MaGaTau;
                        }

                        switch (comboBox.Name)
                        {
                            case "tenCongTy_cbx":
                                maCongTy = inputText;
                                break;
                            case "gaDau_cbx":
                                gaDau = inputText;
                                break;
                            case "gaCuoi_cbx":
                                gaCuoi = inputText;
                                break;
                        }
                    }
                    else if (i is TimePicker)
                    {
                        TimePicker timePicker = i as TimePicker;
                        if (timePicker.Text == null)
                            continue;
                        switch (timePicker.Name)
                        {
                            case "gioBatDau_time":
                                gioBatDau = timePicker.Text;
                                break;
                            case "gioKetThuc_time":
                                gioKetThuc = timePicker.Text;
                                break;
                        }
                    }
                    else if (i is TextBox)
                    {
                        TextBox textBox = i as TextBox;
                        if (textBox == null)
                            continue;

                        switch (textBox.Name)
                        {
                            case "loaiTuyen_txt":
                                loaiTuyen = loaiTuyenEnum.ToString();
                                break;
                            case "maTuyen_txt":
                                maTuyen = textBox.Text;
                                break;
                            case "tenTuyen_txt":
                                tenTuyen = textBox.Text;
                                break;
                            case "thoiGianCho_txt":
                                if (textBox.Text == "Khoảng thời gian chờ (phút)")
                                    break;
                                thoiGianChoTB = int.Parse(textBox.Text);
                                break;
                            case "giaVe_txt":
                                if (textBox.Text == "Giá vé (vnđ)")
                                    break;
                                giaVe = decimal.Parse(textBox.Text);
                                break;
                            case "tinhTrang_txt":
                                tinhTrang = tinhTrangTuyenEnum.ToString();
                                break;
                        }
                    }
                    else continue;
                }
            }

            if (string.IsNullOrEmpty(maTuyen) || string.IsNullOrEmpty(tenTuyen) || string.IsNullOrEmpty(maCongTy) || string.IsNullOrEmpty(gioBatDau) ||
                string.IsNullOrEmpty(gioKetThuc) || string.IsNullOrEmpty(gaDau) || string.IsNullOrEmpty(gaCuoi) || string.IsNullOrEmpty(loaiTuyen) ||
                thoiGianChoTB <= 0 || string.IsNullOrEmpty(tinhTrang) || giaVe <= 0)
            {
                return null;
            }

            if (string.Compare(maTuyen, "Mã tuyến") == 0 || string.Compare(tenTuyen, "Tên tuyến") == 0 || string.Compare(maCongTy, "Tên công ty") == 0 || string.Compare(gioBatDau, "Giờ bắt đầu") == 0 ||
                string.Compare(gioKetThuc, "Giờ kết thúc") == 0 || string.Compare(gaDau, "Ga đầu") == 0 || string.Compare(gaCuoi, "Ga cuối") == 0)
            {
                return null;
            }

            TuyenTau tuyenTau = new TuyenTau()
            {
                MaTuyen = maTuyen,
                TenTuyen = tenTuyen,
                MaCongTy = maCongTy,
                GaDau = gaDau,
                GaCuoi = gaCuoi,
                LoaiTuyen = loaiTuyen,
                GioBatDau = gioBatDau,
                GioKetThuc = gioKetThuc,
                ThoiGianChoTB = thoiGianChoTB,
                TinhTrang = tinhTrang,
                GiaVe = giaVe,
            };
            return tuyenTau;
        }
        private TuyenTau HandleDataUpdate(UIElementCollection p)
        {
            String maTuyen = "";
            String tenTuyen = "";
            String maCongTy = "";
            String gaDau = "";
            String gaCuoi = "";
            String loaiTuyen = "";
            String gioBatDau = "";
            String gioKetThuc = "";
            int thoiGianChoTB = 0;
            String tinhTrang = "";
            decimal giaVe = 0;

            foreach (var item in p)
            {
                if (!(item is WrapPanel)) continue;

                WrapPanel wp_pn = item as WrapPanel;
                foreach (var i in wp_pn.Children)
                {
                    if (i is ComboBox)
                    {
                        ComboBox comboBox = i as ComboBox;

                        if (comboBox.Text == null) continue;

                        var inputText = "";
                        if (comboBox.SelectedItem is CongTy)
                        {
                            CongTy ct = (CongTy)comboBox.SelectedItem;
                            inputText = ct.MaCongTy;
                        }
                        if (comboBox.SelectedItem is GaTau)
                        {
                            GaTau gt = (GaTau)comboBox.SelectedItem;
                            inputText = gt.MaGaTau;
                        }

                        switch (comboBox.Name)
                        {
                            case "tenCongTyUpdate_cbx":
                                maCongTy = inputText;
                                break;
                            case "gaDauUpdate_cbx":
                                gaDau = inputText;
                                break;
                            case "gaCuoiUpdate_cbx":
                                gaCuoi = inputText;
                                break;
                        }
                    }
                    else if (i is TimePicker)
                    {
                        TimePicker timePicker = i as TimePicker;
                        if (timePicker.Text == null)
                            continue;
                        switch (timePicker.Name)
                        {
                            case "gioBatDauUpdate_time":
                                gioBatDau = timePicker.Text;
                                break;
                            case "gioKetThucUpdate_time":
                                gioKetThuc = timePicker.Text;
                                break;
                        }
                    }
                    else if (i is TextBox)
                    {
                        TextBox textBox = i as TextBox;
                        if (textBox == null)
                            continue;

                        switch (textBox.Name)
                        {
                            case "loaiTuyenUpdate_txt":
                                loaiTuyen = loaiTuyenEnum.ToString();
                                break;
                            case "maTuyenUpdate_txt":
                                maTuyen = textBox.Text;
                                break;
                            case "tenTuyenUpdate_txt":
                                tenTuyen = textBox.Text;
                                break;
                            case "thoiGianChoUpdate_txt":
                                if (textBox.Text == "Khoảng thời gian chờ (phút)")
                                    break;
                                thoiGianChoTB = int.Parse(textBox.Text);
                                break;
                            case "giaVeUpdate_txt":
                                if (textBox.Text == "Giá vé (vnđ)")
                                    break;
                                giaVe = decimal.Parse(textBox.Text);
                                break;
                            case "tinhTrangUpdate_txt":
                                tinhTrang = tinhTrangTuyenEnum.ToString();
                                break;
                        }
                    }
                    else continue;
                }
            }

            if (string.IsNullOrEmpty(maTuyen) || string.IsNullOrEmpty(tenTuyen) || string.IsNullOrEmpty(maCongTy) || string.IsNullOrEmpty(gioBatDau) ||
                string.IsNullOrEmpty(gioKetThuc) || string.IsNullOrEmpty(gaDau) || string.IsNullOrEmpty(gaCuoi) || string.IsNullOrEmpty(loaiTuyen) ||
                thoiGianChoTB <= 0 || string.IsNullOrEmpty(tinhTrang) || giaVe <= 0)
            {
                return null;
            }

            if (string.Compare(maTuyen, "Mã tuyến") == 0 || string.Compare(tenTuyen, "Tên tuyến") == 0 || string.Compare(maCongTy, "Tên công ty") == 0 || string.Compare(gioBatDau, "Giờ bắt đầu") == 0 ||
                string.Compare(gioKetThuc, "Giờ kết thúc") == 0 || string.Compare(gaDau, "Ga đầu") == 0 || string.Compare(gaCuoi, "Ga cuối") == 0)
            {
                return null;
            }

            TuyenTau tuyenTau = new TuyenTau()
            {
                MaTuyen = maTuyen,
                TenTuyen = tenTuyen,
                MaCongTy = maCongTy,
                GaDau = gaDau,
                GaCuoi = gaCuoi,
                LoaiTuyen = loaiTuyen,
                GioBatDau = gioBatDau,
                GioKetThuc = gioKetThuc,
                ThoiGianChoTB = thoiGianChoTB,
                TinhTrang = tinhTrang,
                GiaVe = giaVe,
            };
            return tuyenTau;
        }

        private void AddTuyenTau(UIElementCollection p)
        {
            TuyenTau tuyenTau = HandleDataAdd(p);
            if (tuyenTau != null)
                MySqlAddTuyenTau(tuyenTau);
        }

        private void UpdateTuyenTau(UIElementCollection p)
        {
            TuyenTau tuyenTau = HandleDataUpdate(p);
            if (tuyenTau != null)
            {
                MySqlUpdateTuyenTau(tuyenTau);
                MySqlLoadDataTuyenTau();
            }
        }

        private void SearchTuyenTau(UIElement p)
        {
            String searchTextTuyenTau = "";

            TextBox textB = p as TextBox;

            if (textB.Name == "searchTuyenTau_txt")
                searchTextTuyenTau = textB.Text;


            if (searchTextTuyenTau == "Bạn cần gì..." || string.IsNullOrEmpty(searchTextTuyenTau))
            {
                MySqlLoadDataTuyenTau();
            }
            else
            {
                MySqlSearchTuyenTau(searchTextTuyenTau);
            }
        }

        private void FillTextTuyenTau(UIElementCollection p)
        {
            foreach (var item in p)
            {
                if (!(item is WrapPanel)) continue;

                WrapPanel wp_pn = item as WrapPanel;
                foreach (var i in wp_pn.Children)
                {
                    if (i is ComboBox)
                    {
                        ComboBox comboBox = i as ComboBox;

                        switch (comboBox.Name)
                        {
                            case "tenCongTyUpdate_cbx":
                                foreach (var cty in ListCongTy)
                                {
                                    if (cty.MaCongTy == SelectedTuyenTau.MaCongTy)
                                        comboBox.SelectedItem = cty;
                                }
                                break;
                            case "gaDauUpdate_cbx":
                                foreach (var gta in ListGaTau)
                                {
                                    if (gta.MaGaTau == SelectedTuyenTau.GaDau)
                                        comboBox.SelectedItem = gta;
                                }
                                break;
                            case "gaCuoiUpdate_cbx":
                                foreach (var gta in ListGaTau)
                                {
                                    if (gta.MaGaTau == SelectedTuyenTau.GaCuoi)
                                        comboBox.SelectedItem = gta;
                                }
                                break;
                        }
                    }
                    else if (i is TimePicker)
                    {
                        TimePicker timePicker = i as TimePicker;

                        switch (timePicker.Name)
                        {
                            case "gioBatDauUpdate_time":
                                timePicker.Text = SelectedTuyenTau.GioBatDau;
                                break;
                            case "gioKetThucUpdate_time":
                                timePicker.Text = SelectedTuyenTau.GioKetThuc;
                                break;
                        }
                    }
                    else if (i is TextBox)
                    {
                        TextBox textBox = i as TextBox;

                        switch (textBox.Name)
                        {
                            case "loaiTuyenUpdate_txt":
                                ChangeLoaiTuyen(textBox);
                                break;
                            case "maTuyenUpdate_txt":
                                textBox.Text = SelectedTuyenTau.MaTuyen;
                                break;
                            case "tenTuyenUpdate_txt":
                                textBox.Text = SelectedTuyenTau.TenTuyen;
                                break;
                            case "thoiGianChoUpdate_txt":
                                textBox.Text = SelectedTuyenTau.ThoiGianChoTB.ToString();
                                break;
                            case "giaVeUpdate_txt":
                                textBox.Text = SelectedTuyenTau.GiaVe.ToString();
                                break;
                            case "tinhTrangUpdate_txt":
                                ChangeTinhTrangTuyen(textBox);
                                break;
                        }
                    }
                    else continue;
                }
            }
        }

        public void CloneContentTuyen(TuyenTau tuyenTau)
        {
            foreach (var item in ListTuyenTau)
            {
                if (item.MaTuyen == tuyenTau.MaTuyen)
                {
                    SelectedTuyenTau = item;
                }
            }
        }

        public void ChangeTinhTrangTuyen(UIElement p)
        {
            TextBox text = p as TextBox;
            if (tinhTrangTuyenEnum == TinhTrangTuyenEnum.HoatDong)
            {
                text.Text = "Tình trạng: Không hoạt động";
                tinhTrangTuyenEnum = TinhTrangTuyenEnum.KHoatDong;
            }
            else
            {
                text.Text = "Tình trạng: Hoạt động";
                tinhTrangTuyenEnum = TinhTrangTuyenEnum.HoatDong;
            }
        }

        public void ChangeLoaiTuyen(UIElement p)
        {
            TextBox text = p as TextBox;
            if (loaiTuyenEnum == LoaiTuyenEnum.Thuong)
            {
                text.Text = "Loại tuyến: Tốc hành";
                loaiTuyenEnum = LoaiTuyenEnum.TocHanh;
            }
            else
            {
                text.Text = "Loại tuyến: Thường";
                loaiTuyenEnum = LoaiTuyenEnum.Thuong;
            }
        }

        public TuyenTau LayNoiDungTuyen(TuyenTau tuyenGoc)
        {
            TuyenTau tuyenThayThe = tuyenGoc.ShallowCopy();
            foreach (var item in ListCongTy)
            {
                if (tuyenThayThe.MaCongTy == item.MaCongTy)
                    tuyenThayThe.MaCongTy = item.TenCongTy;
            }
            foreach (var item in ListGaTau)
            {
                if (tuyenThayThe.GaDau == item.MaGaTau)
                    tuyenThayThe.GaDau = item.TenGaTau;
                if (tuyenThayThe.GaCuoi == item.MaGaTau)
                    tuyenThayThe.GaCuoi = item.TenGaTau;
            }
            if (tuyenThayThe.LoaiTuyen == LoaiTuyenEnum.Thuong.ToString())
                tuyenThayThe.LoaiTuyen = "Thường";
            else tuyenThayThe.LoaiTuyen = "Tốc hành";

            if (tuyenThayThe.TinhTrang == TinhTrangTuyenEnum.HoatDong.ToString())
                tuyenThayThe.TinhTrang = "Hoạt động";
            else tuyenThayThe.TinhTrang = "Không hoạt động";

            return tuyenThayThe;
        }

        public void MySqlAddTuyenTau(TuyenTau tuyenTau)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "insert into TuyenTau values ('" + tuyenTau.MaTuyen + "', '" + tuyenTau.TenTuyen + "', '" + tuyenTau.MaCongTy +
                                              "', '" + tuyenTau.GaDau + "', '" + tuyenTau.GaCuoi + "', '" + tuyenTau.LoaiTuyen +
                                              "', '" + tuyenTau.GioBatDau + "', '" + tuyenTau.GioKetThuc + "'," + tuyenTau.ThoiGianChoTB + ", '" + tuyenTau.GiaVe + "', '" + tuyenTau.TinhTrang + "')";
                        var reader = command.ExecuteReader();

                        ListTuyenTau.Add(tuyenTau);
                        ListSearchTuyenTau.Add(LayNoiDungTuyen(tuyenTau));
                        Console.WriteLine("AAAAAA " + tuyenTau);

                        System.Windows.MessageBox.Show("Thêm mới tuyến tàu thành công!");
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

        public void MySqlUpdateTuyenTau(TuyenTau tuyenTau)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "update TuyenTau set tenTuyen = '" + tuyenTau.TenTuyen + "', maCongTy = '" + tuyenTau.MaCongTy + "', gaDau = '" + tuyenTau.GaDau + "', gaCuoi = '" + tuyenTau.GaCuoi + "'," +

                            "loaiTuyen = '" + tuyenTau.LoaiTuyen + "', gioBatDau = '" + tuyenTau.GioBatDau + "', gioKetThuc = '" + tuyenTau.GioKetThuc +

                            "', thoiGianChoTB = " + tuyenTau.ThoiGianChoTB + ", giaVe = " + tuyenTau.GiaVe + ", tinhTrang = '" + tuyenTau.TinhTrang + "' where maTuyen = '" + tuyenTau.MaTuyen + "'; ";

                        var reader = command.ExecuteReader();

                        System.Windows.MessageBox.Show("Cập nhật thông tin tuyến tàu thành công!");
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

        public void MySqlSearchTuyenTau(String searchText)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListSearchTuyenTau.Clear();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from TuyenTau where (maTuyen like '%" + searchText + "%' or tenTuyen like '%" + searchText +
                            "%' or maCongTy like '%" + searchText + "%' or gaDau like '%" + searchText + "%' or gaCuoi like '%" + searchText + "%'" +
                            " or loaiTuyen like '%" + searchText + "%' or tinhTrang like '%" + searchText + "%')";
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
                            ListSearchTuyenTau.Add(LayNoiDungTuyen(tuyenTau));
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

        public void MySqlLoadDataTuyenTau()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListTuyenTau.Clear();
                    ListSearchTuyenTau.Clear();
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

                            ListSearchTuyenTau.Add(LayNoiDungTuyen(tuyenTau));
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
                    System.Windows.MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Console.WriteLine("Disconnection Success!");
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand FillUpdateText { get; set; }
        public ICommand DoiTrangThaiCommand { get; set; }
        public ICommand DoiLoaiTuyenCommand { get; set; }
    }
}

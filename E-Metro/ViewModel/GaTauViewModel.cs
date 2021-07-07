using E_Metro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace E_Metro.ViewModel
{
    class GaTauViewModel
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        private ObservableCollection<GaTau> listGaTau;
        public ObservableCollection<GaTau> ListGaTau { get => listGaTau; set => listGaTau = value; }

        private ObservableCollection<GaTau> lisShowtGaTau;
        public ObservableCollection<GaTau> ListShowTau { get => lisShowtGaTau; set => lisShowtGaTau = value; }

        private ObservableCollection<TrangThaiGaTau> listTrangThai;
        public ObservableCollection<TrangThaiGaTau> ListTrangThai { get => listTrangThai; set => listTrangThai = value; }


        public GaTauViewModel()
        {
            listGaTau = new ObservableCollection<GaTau>();
            listTrangThai = new ObservableCollection<TrangThaiGaTau>();
            lisShowtGaTau = new ObservableCollection<GaTau>();
            MySqlTrangThai();
            MySqlLoadDataGaTau();
            AddCommand = new RelayCommand<UIElementCollection>((p) => true, AddGaTau);
            SearchCommand = new RelayCommand<UIElementCollection>((p) => true, SearchGaTau);
        }
        private void AddGaTau(UIElementCollection p)
        {
            String maGaTau = "";
            String tenGaTau = "";
            String moTaViTri = "";
            String trangThai = "";
            String lienKetSoDoGaTau = " ";
            foreach (var item in p)
            {
                if (item is TextBox)
                {
                    TextBox textB = item as TextBox;

                    switch (textB.Name)
                    {
                        case "maGaTau_txt":
                            maGaTau = textB.Text;
                            break;
                        case "tenGaTau_txt":
                            tenGaTau = textB.Text;
                            break;
                        case "moTaViTri_txt":
                            moTaViTri = textB.Text;
                            break;
                        case "lienKetSoDoGaTau_txt":
                            lienKetSoDoGaTau = textB.Text;
                            break;
                    }
                }
                else if (item is ComboBox)
                {
                    ComboBox comboB = item as ComboBox;
                    TrangThaiGaTau trThai = (TrangThaiGaTau)comboB.SelectedItem;
                    trangThai = trThai.MaTTGaTau;
                }
            }

            if (string.IsNullOrEmpty(maGaTau) || string.IsNullOrEmpty(tenGaTau) || string.IsNullOrEmpty(moTaViTri) ||
                string.IsNullOrEmpty(trangThai))
            {
                return;
            }
            if (string.Compare(maGaTau, "Mã ga tàu") == 0 || string.Compare(tenGaTau, "Tên ga tàu") == 0 ||
                string.Compare(moTaViTri, "Mô Tả Vị Trí") == 0 || string.Compare(trangThai, "Địa chỉ trụ sở chính") == 0)
            {
                return;
            }

            GaTau gaTau = new GaTau()
            {
                MaGaTau = maGaTau,
                TenGaTau = tenGaTau,
                MoTaViTri = moTaViTri,
                TrangThai = trangThai,
                AnhGaTau = lienKetSoDoGaTau,
            };

            MySqlAddGaTau(gaTau);
        }
        private void SearchGaTau(UIElementCollection p)
        {
            String searchTextGaTau = "";

            foreach (var item in p)
            {
                TextBox textB = item as TextBox;
                if (textB == null)
                    continue;
                switch (textB.Name)
                {
                    case "searchGaTau_txt":
                        searchTextGaTau = textB.Text;
                        break;
                    case "searchGaTau2_txt":
                        searchTextGaTau = textB.Text;
                        break;
                }
            }
            if (searchTextGaTau == "Bạn cần gì..." || string.IsNullOrEmpty(searchTextGaTau))
            {
                MySqlLoadDataGaTau();
            }
            else
            {
                MySqlSearchGaTau(searchTextGaTau);
            }
        }

        private GaTau ChangeTextGaTau(GaTau gaTau)
        {
            GaTau ga = gaTau.ShallowCopy();
            foreach (var item in ListTrangThai)
            {
                if (item.MaTTGaTau == ga.TrangThai)
                {
                    ga.TrangThai = item.TrangThai;
                    break;
                }     
            }
            return ga;
        }

        public void UpdateGaTau(DataGridCellEditEndingEventArgs e, DataGrid dataGrid)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                    if (bindingPath == "TenGaTau")
                    {
                        GaTau gaTau = (dataGrid.SelectedItem as GaTau);
                        gaTau.TenGaTau = (e.EditingElement as TextBox).Text;
                        MySqlUpdateGaTau(gaTau);
                    }
                    if (bindingPath == "MoTaViTri")
                    {
                        GaTau gaTau = (dataGrid.SelectedItem as GaTau);
                        gaTau.MoTaViTri = (e.EditingElement as TextBox).Text;
                        MySqlUpdateGaTau(gaTau);
                    }
                    if (bindingPath == "AnhGaTau")
                    {
                        GaTau gaTau = (dataGrid.SelectedItem as GaTau);
                        gaTau.AnhGaTau = (e.EditingElement as TextBox).Text;
                        MySqlUpdateGaTau(gaTau);
                    }
                }
            }
        }
        public void UpdateTrangThai(DataGrid dataGrid)
        {
            if (dataGrid.CurrentColumn.Header.ToString() == "Tình trạng")
            {
                GaTau gaTau = dataGrid.SelectedItem as GaTau;
                
                for (int i = 0; i < ListTrangThai.Count; i++)
                {
                    if (gaTau.TrangThai == ListTrangThai[i].TrangThai)
                    {
                        if (i - ListTrangThai.Count == -1)
                        {
                            string maTrangThai = "";
                            gaTau.TrangThai = ListTrangThai[0].TrangThai;
                            maTrangThai = ListTrangThai[0].MaTTGaTau;
                            MySqlUpdateTrangThai(maTrangThai, gaTau.MaGaTau);
                            break;
                        }
                        else if (i - ListTrangThai.Count < -1)
                        {
                            string maTrangThai = "";
                            gaTau.TrangThai = ListTrangThai[i + 1].TrangThai;
                            maTrangThai = ListTrangThai[i + 1].MaTTGaTau;
                            MySqlUpdateTrangThai(maTrangThai, gaTau.MaGaTau);
                            break;
                        }      
                    }
                }
            }
        }


        public void MySqlUpdateGaTau(GaTau gaTau)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "update GaTau set tenGaTau = '" + gaTau.TenGaTau + "', moTaViTri = '" + gaTau.MoTaViTri + "', anhGaTau = '" + gaTau.AnhGaTau + "' where maGaTau = '" + gaTau.MaGaTau + "'";

                        var reader = command.ExecuteReader();

                        Console.WriteLine("Cap Nhat thanh cong!");
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
        public void MySqlUpdateTrangThai(string trangThai, string maGaTau)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
       
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "update GaTau set trangThai = '" + trangThai + "' where maGaTau = '" + maGaTau+ "'";

                        var reader = command.ExecuteReader();

                        Console.WriteLine("Cap Nhat thanh cong!");
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
            MySqlLoadDataGaTau();
        }
        public void MySqlLoadDataGaTau()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListGaTau.Clear();
                    ListShowTau.Clear();
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
                                AnhGaTau = reader["anhGaTau"].ToString(),
                            };
                            ListGaTau.Add(gaTau);
                            ListShowTau.Add(ChangeTextGaTau(gaTau));
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
        public void MySqlAddGaTau(GaTau gaTau)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "insert into GaTau(maGaTau, tenGaTau, moTaViTri, trangThai, anhGaTau) " +
                            "values ('" + gaTau.MaGaTau + "', N'" + gaTau.TenGaTau + "', '" + gaTau.MoTaViTri + "', '" + gaTau.TrangThai + "', '" + gaTau.AnhGaTau + "')";

                        var reader = command.ExecuteReader();

                        ListGaTau.Add(gaTau);
                        ListShowTau.Add(gaTau);
                        System.Windows.MessageBox.Show("Thêm mới ga tàu thành công!");
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
        public void MySqlSearchGaTau(String searchText)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");
                    ListGaTau.Clear();
                    ListShowTau.Clear();
                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from GaTau where (maGaTau like '%" + searchText + "%' or tenGaTau like '%" + searchText +
                            "%' or moTaViTri like '%" + searchText + "%' or anhGaTau like '%" + searchText + "%')";
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            GaTau gaTau = new GaTau()
                            {
                                MaGaTau = reader["maGaTau"].ToString(),
                                TenGaTau = reader["tenGaTau"].ToString(),
                                MoTaViTri = reader["moTaViTri"].ToString(),
                                TrangThai = reader["trangThai"].ToString(),
                                AnhGaTau = reader["anhGaTau"].ToString(),
                            };
                            ListGaTau.Add(gaTau);
                            ListShowTau.Add(ChangeTextGaTau(gaTau));
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
        public void MySqlTrangThai()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from trangthaigatau";
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            TrangThaiGaTau trangThaiGaTau = new TrangThaiGaTau()
                            {
                                MaTTGaTau = reader["maTTGaTau"].ToString(),
                                TrangThai = reader["trangThai"].ToString(),
                            };
                            ListTrangThai.Add(trangThaiGaTau);
                        }
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
        public ICommand SearchCommand { get; set; }
    }
}

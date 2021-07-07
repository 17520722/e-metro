
using E_Metro.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;

namespace E_Metro.ViewModel
{
    public class CongTyViewModel
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        private ObservableCollection<CongTy> listCongTy;

        public ObservableCollection<CongTy> ListCongTy { get => listCongTy; set => listCongTy = value; }

        public CongTyViewModel()
        {
            listCongTy = new ObservableCollection<CongTy>();
            MySqlLoadDataCongTy();  
            AddCommand = new RelayCommand<UIElementCollection>((p) => true, AddCongTy);
            SearchCommand = new RelayCommand<UIElementCollection>((p) => true, SearchCongTy);
        }
        // processor
        private void AddCongTy(UIElementCollection p)
        {
            String maCongTy = "";
            String tenCongTy = "";
            String diaChiWeb = "";
            String diaChiTruSo = "";
            String sdt = "";

            foreach (var item in p)
            {
                TextBox textB = item as TextBox;
                if (textB == null)
                    continue;
                switch (textB.Name)
                {
                    case "maCongTy_txt":
                        maCongTy = textB.Text;
                        break;
                    case "tenCongTy_txt":
                        tenCongTy = textB.Text;
                        break;
                    case "diaChiWeb_txt":
                        diaChiWeb = textB.Text;
                        break;
                    case "truSo_txt":
                        diaChiTruSo = textB.Text;
                        break;
                    case "sdt_txt":
                        sdt = textB.Text;
                        break;
                }
            }

            if (string.IsNullOrEmpty(maCongTy) || string.IsNullOrEmpty(tenCongTy) || string.IsNullOrEmpty(diaChiWeb) ||
                string.IsNullOrEmpty(diaChiTruSo) || string.IsNullOrEmpty(sdt))
            {
                return;
            }
            if (string.Compare(maCongTy, "Mã công ty") == 0 || string.Compare(tenCongTy, "Tên công ty") == 0 ||
                string.Compare(diaChiWeb, "Địa chỉ website") == 0 || string.Compare(diaChiTruSo, "Địa chỉ trụ sở chính") == 0 ||
                string.Compare(sdt, "Số điện thoại") == 0)
            {
                return;
            }

            CongTy cty = new CongTy()
            {
                MaCongTy = maCongTy,
                TenCongTy = tenCongTy,
                DiaChiWeb = diaChiWeb,
                DiaChiTruSo = diaChiTruSo,
                Sdt = sdt
            };

            MySqlAddCongTy(cty);
        }         
        private void SearchCongTy(UIElementCollection p)
        {
            String searchTextCongTy = "";

            foreach (var item in p)
            {
                TextBox textB = item as TextBox;
                if (textB == null)
                    continue;
                switch (textB.Name)
                {
                    case "searchCongTy_txt":
                        searchTextCongTy = textB.Text;
                        break;
                    case "searchCongTy2_txt":
                        searchTextCongTy = textB.Text;
                        break;
                }
            }
            if (searchTextCongTy == "Bạn cần gì..." || string.IsNullOrEmpty(searchTextCongTy))
            {
                MySqlLoadDataCongTy();
            }
            else
            {
                MySqlSearchCongTy(searchTextCongTy);
            }
        }
        public void UpdateCongTy(DataGridCellEditEndingEventArgs e, DataGrid dataGrid)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var column = e.Column as DataGridBoundColumn;
                if (column != null)
                {
                    var bindingPath = (column.Binding as Binding).Path.Path;
                    if (bindingPath == "TenCongTy")
                    {
                        CongTy congTy = (dataGrid.SelectedItem as CongTy);
                        congTy.TenCongTy = (e.EditingElement as TextBox).Text;
                        MySqlUpdateCongTy(congTy);
                    }
                    if (bindingPath == "DiaChiWeb")
                    {
                        CongTy congTy = (dataGrid.SelectedItem as CongTy);
                        congTy.DiaChiWeb = (e.EditingElement as TextBox).Text;
                        MySqlUpdateCongTy(congTy);
                    }
                    if (bindingPath == "DiaChiTruSo")
                    {
                        CongTy congTy = (dataGrid.SelectedItem as CongTy);
                        congTy.DiaChiTruSo = (e.EditingElement as TextBox).Text;
                        MySqlUpdateCongTy(congTy);
                    }
                    if (bindingPath == "Sdt")
                    {
                        Regex regex = new Regex("[^0-9]+");
                        CongTy congTy = (dataGrid.SelectedItem as CongTy);
                        if (!regex.IsMatch(congTy.Sdt))
                        {
                            congTy.Sdt = (e.EditingElement as TextBox).Text;
                            MySqlUpdateCongTy(congTy);
                        }
                    }
                }
            }
        }

        public void MySqlLoadDataCongTy()
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
        public void MySqlAddCongTy(CongTy congTy)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "insert into CongTy(maCongTy, tenCongTy, diaChiWeb, diaChiTruSo, sdt) " +
                            "values ('" + congTy.MaCongTy + "', N'" + congTy.TenCongTy + "', '" + congTy.DiaChiWeb + "', '" + congTy.DiaChiTruSo + "', '" + congTy.Sdt + "')";

                        var reader = command.ExecuteReader();

                        ListCongTy.Add(congTy);
                        System.Windows.MessageBox.Show("Thêm mới công ty thành công!");
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
        public void MySqlUpdateCongTy(CongTy congTy)
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "update CongTy set tenCongTy = '" + congTy.TenCongTy + "', diaChiWeb = '" + congTy.DiaChiWeb + "', diaChiTruSo = '" + congTy.DiaChiTruSo + "', sdt = '" + congTy.Sdt + "' where maCongTy = '" + congTy.MaCongTy + "'";

                        var reader = command.ExecuteReader();

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
        public void MySqlSearchCongTy(String searchText)
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
                        command.CommandText = "select * from CongTy where (maCongTy like '%" + searchText + "%' or tenCongTy like '%" + searchText +
                            "%' or diaChiWeb like '%" + searchText + "%' or diaChiTruSo like '%" + searchText + "%' or sdt like '%" + searchText + "%')";
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
        public ICommand SearchCommand { get; set; }

    }
}

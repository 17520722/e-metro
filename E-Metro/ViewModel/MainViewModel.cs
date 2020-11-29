using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Windows;

namespace E_Metro.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        public MainViewModel()
        {
            using (con)
            {
                try
                {
                    con.Open();
                    Console.WriteLine("Connection Success!");

                    using (var command = con.CreateCommand())
                    {
                        command.CommandText = "select * from TrangThaiGaTau";

                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            String maTTGaTau = reader["maTTGaTau"].ToString();
                            String trangThai = reader["trangThai"].ToString();

                            Console.WriteLine("->->->->->->" + maTTGaTau + " + " + trangThai);
                        }
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
    }
}

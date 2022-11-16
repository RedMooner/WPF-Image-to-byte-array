using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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

namespace Image_Test_Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); // создаем диалоговое окно
            openFileDialog.ShowDialog(); // показываем
            byte[] image_bytes = File.ReadAllBytes(openFileDialog.FileName); // получаем байты выбранного файла

            string connectionString = "server=192.168.140.128;Trusted_Connection=No;DataBase=Test;User=s;PWD=s"; // строка подключения
            using (SqlConnection connection = new SqlConnection(connectionString)) // создаем подключение
            {
                connection.Open(); // откроем подключение
                SqlCommand command = new SqlCommand(); // создадим запрос
                command.Connection = connection; // дадим запросу подключение
                command.CommandText = @"INSERT INTO images VALUES (@ImageData)"; // пропишем запрос
                command.Parameters.Add("@ImageData", SqlDbType.Image, 1000000); 
                command.Parameters["@ImageData"].Value = image_bytes;// скалярной переменной ImageData присвоем массив байтов
                command.ExecuteNonQuery(); // запустим
            }
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            DataTable matcher_query = SqlModel.Select("SELECT * from Images"); // запрос
            image.Source = ByteImage.Convert(ByteImage.GetImageFromByteArray((byte[])matcher_query.Rows[matcher_query.Rows.Count - 1][0])); 
            // берем из запроса последнюю строку и ее массив байтов
        }
    }
}

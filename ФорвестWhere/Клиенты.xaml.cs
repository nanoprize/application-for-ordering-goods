using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace ФорвестWhere
{
    /// <summary>
    /// Логика взаимодействия для Клиенты.xaml
    /// </summary>
    public partial class Клиенты : Window
    {
        
        Entities entities = new Entities();
        public static Entities DataEntitiesKlients { get; set; }
        ObservableCollection<Клиенты_магазина> ListKlient;
        public Клиенты()
        {
            InitializeComponent();
            DataEntitiesKlients = new Entities();
            InitializeComponent();
            ListKlient = new ObservableCollection<Клиенты_магазина>();
        }
        byte[] data;
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            list_klientov.ItemsSource = entities.Клиенты_магазина.ToList();
        }
        
        private void list_klientov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var klient = list_klientov.SelectedItem as Клиенты_магазина;
            if (klient != null)
            {
                name_box.Text = klient.ФИО;
                
                number_box.Text = klient.номер;
                address_box.Text = klient.адрес;
                if (klient.фото!= null)
                {
                    data = klient.фото;
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(data);
                    image.EndInit();
                    photo.Source = image;
                }
                else
                {
                    data = null;
                }

            }
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            var save_all = list_klientov.SelectedItem as Клиенты_магазина;
            if (save_all == null)
            {
                save_all = new Клиенты_магазина();
                entities.Клиенты_магазина.Add(save_all);
            }
            save_all.ФИО = name_box.Text;
            
            save_all.номер = number_box.Text;
            save_all.адрес = address_box.Text;
            save_all.фото = data;
            entities.SaveChanges();
            list_klientov.ItemsSource = entities.Клиенты_магазина.ToList();
        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            name_box.Clear();
            
            number_box.Clear();
            address_box.Clear();
            list_klientov.SelectedIndex = -1;
            photo.Source = null;
            data = null;
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            var delete_usluga = list_klientov.SelectedItem as Клиенты_магазина;
            try
            {
                var exist_ = (from pb in entities.Ведение_заказов where pb.id_клиента == delete_usluga.Id_клиента select pb).First();
                // Запись найдена
                MessageBox.Show("Клиента удалить нельзя!\nОн оформил заказ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezult == MessageBoxResult.No)
                {
                    return;
                }

                var delete_tovar = list_klientov.SelectedItem as Клиенты_магазина;
                if (delete_tovar != null)
                {
                    entities.Клиенты_магазина.Remove(delete_tovar);
                    entities.SaveChanges();
                    name_box.Clear();
                    number_box.Clear();
                    address_box.Clear();
                    data = null;

                    ListKlient.Remove(delete_tovar);
                    list_klientov.ItemsSource = entities.Клиенты_магазина.ToList();

                    MessageBox.Show("Запись удалена", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void open_photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png|Все файлы (*.*)|*.*";
            if ((bool)file.ShowDialog())
            {
                try
                {
                    data = File.ReadAllBytes(file.FileName);
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = new MemoryStream(data);
                    image.EndInit();
                    photo.Source = image;
                }
                catch
                {
                    MessageBox.Show(">kfkfk", "dfds", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

  

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            list_klientov.ItemsSource = entities.Клиенты_магазина.ToList();
            list_klientov.SelectedIndex = -1;
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var raspisanie = entities.Клиенты_магазина.ToList();
            raspisanie = raspisanie.Where(p => p.ФИО.ToLower().Contains(search.Text.ToLower())).ToList();

            list_klientov.ItemsSource = raspisanie;
        }
    }
}


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
    /// Логика взаимодействия для Товары.xaml
    /// </summary>
    public partial class Товары : Window
    {
        Entities entities = new Entities();
        public static Entities DataEntitiesTovar{ get; set; }
        ObservableCollection<Товары_магазина> ListTovar;
        public Товары()
        {
            InitializeComponent();
            DataEntitiesTovar = new Entities();
            InitializeComponent();
            ListTovar = new ObservableCollection<Товары_магазина>();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            list_tovarov.ItemsSource = entities.Товары_магазина.ToList();
        }
        //private void UpdateListTovar()
        //{
        //    list_tovarov.ItemsSource = entities.Товары.ToList();
        //}
        byte[] data;
        private void list_tovarov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tovary = list_tovarov.SelectedItem as Товары_магазина;
            if (tovary != null)
            {
                name_box.Text = tovary.название;
                price_box.Text = tovary.цена;
                stock_box.Text = tovary.наличие;
                if (tovary.фото != null)
                {
                    data = tovary.фото;
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

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            var save_all = list_tovarov.SelectedItem as Товары_магазина;
            if (save_all == null)
            {
                save_all = new Товары_магазина();
                entities.Товары_магазина.Add(save_all);
            }
            save_all.название = name_box.Text;
            save_all.цена = price_box.Text;
            save_all.наличие = stock_box.Text;
            save_all.фото = data;
            entities.SaveChanges();
            list_tovarov.ItemsSource = entities.Товары_магазина.ToList();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            var delete_usluga = list_tovarov.SelectedItem as Товары_магазина;
            try
            {
                var exist_ = (from pb in entities.Ведение_заказов where pb.id_клиента == delete_usluga.Id_товара select pb).First();
                // Запись найдена
                MessageBox.Show("Нельзя удалить товар!\nОн оформлен клиентом.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                var rezult = MessageBox.Show("Вы действительно хотите удалить запись?",
                    "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezult == MessageBoxResult.No)
                {
                    return;
                }

                var delete_tovar = list_tovarov.SelectedItem as Товары_магазина;
                if (delete_tovar != null)
                {
                    entities.Товары_магазина.Remove(delete_tovar);
                    entities.SaveChanges();
                    name_box.Clear();
                    price_box.Clear();
                    stock_box.Clear();
                    data = null;
                    ListTovar.Remove(delete_tovar);
                    list_tovarov.ItemsSource = entities.Товары_магазина.ToList();

                    MessageBox.Show("Запись удалена", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Нет удаляемых объектов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            name_box.Clear();
            price_box.Clear();
            stock_box.Clear();
            list_tovarov.SelectedIndex = -1;
            photo.Source = null;
            data = null;
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            list_tovarov.ItemsSource = entities.Товары_магазина.ToList();
            list_tovarov.SelectedIndex = -1;
            

        }

     

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}



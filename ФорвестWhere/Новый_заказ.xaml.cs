using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Новый_заказ.xaml
    /// </summary>
    public partial class Новый_заказ : Window
    {
        Entities entities = new Entities();
        public Новый_заказ()
        {
            InitializeComponent();
            foreach (var tovar1 in entities.Товары_магазина)
                spisok_tovarov.Items.Add(tovar1);
            foreach (var klient1 in entities.Клиенты_магазина)
                spisok_fio.Items.Add(klient1);
          

        }

        private void make_zakaz_Click(object sender, RoutedEventArgs e)
        {
            var zakaz = spisok_fio.SelectedItem as Ведение_заказов;
           

            if (spisok_tovarov.SelectedIndex == -1 || spisok_fio.SelectedIndex == -1 ||  vid_dostavki.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (zakaz == null)
                {
                    zakaz = new Ведение_заказов();
                    entities.Ведение_заказов.Add(zakaz);
                    

                }


                zakaz.id_товара = (spisok_tovarov.SelectedItem as Товары_магазина).Id_товара;
                zakaz.id_клиента = (spisok_fio.SelectedItem as Клиенты_магазина).Id_клиента;
               
                zakaz.название_доставки = vid_dostavki.Text;
               


                entities.SaveChanges();
                MessageBox.Show("Заказ оформлен", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
                var window = new adminorsimple();
                window.Show();
                this.Close();
            }
        }

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new adminorsimple();
            window.Show();
            this.Close();
        }

      
    }
}
    

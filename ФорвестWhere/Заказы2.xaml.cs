using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Заказы2.xaml
    /// </summary>
    public partial class Заказы2 : Window
    {
        Entities entities = new Entities();
        public static Entities DataEntitiesKlient { get; set; }
        public static Entities DataEntitiesZakaz { get; set; }
        ObservableCollection<Ведение_заказов> ListZakazik;
      
        public Заказы2()
        {
            InitializeComponent();
            ListZakazik = new ObservableCollection<Ведение_заказов>();
        }

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new adminorsimple();
            window.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var zakaz = entities.Ведение_заказов;
            var query_zakaz = from Ведение_заказов in zakaz
                              orderby Ведение_заказов.название_доставки
                              select Ведение_заказов;
            foreach (Ведение_заказов pr in query_zakaz)
            {
                ListZakazik.Add(pr);
            }
            list_zakazov.ItemsSource = ListZakazik;
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            var student = entities.Ведение_заказов.ToList();
            list_zakazov.ItemsSource = student;
        }

        private void search_button_Click(object sender, RoutedEventArgs e)
        {
            DataEntitiesZakaz = new Entities();
            ListZakazik.Clear();
            if (search.Text == "")
            {
                MessageBox.Show("Введите номер комнаты");
            }
            else
            {
                int number = Convert.ToInt32(search.Text);

                var projivaniess = DataEntitiesZakaz.Ведение_заказов;
                var queryProj = from projivalchik in projivaniess
                                where projivalchik.id_клиента == number
                                select projivalchik;
                foreach (Ведение_заказов emp in queryProj)
                {
                    ListZakazik.Add(emp);
                }
                if (ListZakazik.Count > 0)
                {
                    list_zakazov.ItemsSource = ListZakazik;
                    //buttonFind.IsEnabled = true;
                    //buttonfindTitle.IsEnabled = false;
                }
                else
                    MessageBox.Show("Номер " + number + " еще не занят",
                    "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);


            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var raspisanie = entities.Ведение_заказов.ToList();
            raspisanie = raspisanie.Where(p => p.название_доставки.ToLower().Contains(search.Text.ToLower())).ToList();

            list_zakazov.ItemsSource = raspisanie;
        }
    }
}

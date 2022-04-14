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
    /// Логика взаимодействия для workZakazy.xaml
    /// </summary>
    public partial class workZakazy : Window
    {
        Entities entities = new Entities();
        public workZakazy()
        {
            InitializeComponent();
            list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
            foreach (var bilet in entities.Клиенты_магазина)
                fio.Items.Add(bilet);
            foreach (var klient in entities.Товары_магазина)
                tovar.Items.Add(klient);

        }

        private void list_zakazov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var zakaz = list_zakazov.SelectedItem as Ведение_заказов;
            if (zakaz != null)
            {
                //name_excursions.Text = zakaz.название;
                //name_bilet.Text = zakaz.цена;
                //name_klient.Text = zakaz.наличие;
              
                dostavka.Text = zakaz.название_доставки;
                price_dostavki.Text = Convert.ToString(zakaz.стоимость_доставки);
                sum.Text = Convert.ToString(zakaz.сумма_с_доставкой);

                fio.SelectedItem = (from vid in entities.Клиенты_магазина where vid.Id_клиента == zakaz.id_клиента select vid).Single<Клиенты_магазина>();
                tovar.SelectedItem = (from vid in entities.Товары_магазина where vid.Id_товара == zakaz.id_товара select vid).Single<Товары_магазина>();
               


            }
        }

        private void tovar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected_bilet = tovar.SelectedItem as Товары_магазина;
            if (selected_bilet != null)
            {
                price.Text = Convert.ToString(selected_bilet.цена);

            }
            else
            {
                price.Text = "";
            }
        }

        private void save_button_Click(object sender, RoutedEventArgs e)
        {
            var save_all = list_zakazov.SelectedItem as Ведение_заказов;
            if (fio.SelectedIndex == -1 || tovar.SelectedIndex == -1  || dostavka.Text == "" || price_dostavki.Text == "" || sum.Text == "")
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            if (save_all == null)
            {
                save_all = new Ведение_заказов();
                entities.Ведение_заказов.Add(save_all);
            }
            save_all.id_клиента = (fio.SelectedItem as Клиенты_магазина).Id_клиента;
            save_all.id_товара = (tovar.SelectedItem as Товары_магазина).Id_товара;
           


            save_all.название_доставки = dostavka.Text;
            save_all.стоимость_доставки = Convert.ToString(price_dostavki.Text);
            save_all.сумма_с_доставкой = Convert.ToString(sum.Text);

            entities.SaveChanges();
            MessageBox.Show("Заказ добавлен", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
            list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить данную запись?", "Предупреждение",
              MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes)
            {
                entities.Ведение_заказов.Remove(list_zakazov.SelectedItem as Ведение_заказов);
                try
                {
                    entities.SaveChanges();
                    MessageBox.Show("Запись успешно удалена", "Успех!");
                    list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void clear_button_Click(object sender, RoutedEventArgs e)
        {
            fio.SelectedIndex = -1;
            tovar.SelectedIndex = -1;

            price.Clear();
            dostavka.Clear();
            price_dostavki.Clear();
            sum.Clear();
            list_zakazov.SelectedIndex = -1;
        }

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Заказы1();
            window.Show();
            this.Close();
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
            list_zakazov.SelectedIndex = -1;
        }
    }
}

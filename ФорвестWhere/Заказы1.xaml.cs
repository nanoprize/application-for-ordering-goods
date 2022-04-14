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
    /// Логика взаимодействия для Заказы1.xaml
    /// </summary>
    public partial class Заказы1 : Window
    {
        Entities entities = new Entities();
        public static Entities DataEntitiesKlient { get; set; }
        public static Entities DataEntitiesZakaz{ get; set; }
    ObservableCollection<Ведение_заказов> ListZakaz;
        ObservableCollection<Клиенты_магазина> ListKlient;


        public Заказы1()
        {
            InitializeComponent();
            ListZakaz = new ObservableCollection<Ведение_заказов>();
            ListKlient = new ObservableCollection<Клиенты_магазина>();
            Filter.Items.Add("Все");
            Filter.Items.Add("Самовывоз");
            Filter.Items.Add("Доставка");
           
            Filter.SelectedIndex = 0;
            list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
        }



        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {


            var zakaz = entities.Ведение_заказов;
            var query_zakaz = from Ведение_заказов in zakaz
                              orderby Ведение_заказов.название_доставки
                              select Ведение_заказов;
            foreach (Ведение_заказов pr in query_zakaz)
            {
                ListZakaz.Add(pr);
            }
            list_zakazov.ItemsSource = ListZakaz;


        }



        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var tovarik = entities.Товары_магазина;
            var vedenie = list_zakazov.SelectedItem as Ведение_заказов;

            if (vedenie.название_доставки == "Самовывоз")
            {
                vedenie.стоимость_доставки = "0";
                list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
                entities.SaveChanges();
                list_zakazov.ItemsSource = ListZakaz;

            }

            else
            {
                vedenie.стоимость_доставки = "300";
                list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
                entities.SaveChanges();
                list_zakazov.ItemsSource = ListZakaz;
            }
        }

        private void list_zakazov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var zakazy = list_zakazov.SelectedItem as Ведение_заказов;
            if (zakazy != null)
            {


                lslsboc.Text = zakazy.стоимость_доставки;
                var b = cena_box.Text;
              

            }
        }

        private void itogo_button_Click(object sender, RoutedEventArgs e)
        {
            var zakazy = list_zakazov.SelectedItem as Ведение_заказов;
            if (zakazy != null)
            {
                var a = Convert.ToInt32(lslsboc.Text = zakazy.стоимость_доставки);
                var b = Convert.ToInt32(cena_box.Text);
                var c = a + b;
                itogo_pole.Text = Convert.ToString(c)+" руб.";

            }
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            var zakazy = list_zakazov.SelectedItem as Ведение_заказов;
            if (zakazy != null)
            {

                zakazy.сумма_с_доставкой = itogo_pole.Text;
                list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
                entities.SaveChanges();
                list_zakazov.ItemsSource = ListZakaz;
            }
        }

  

        private void Update()
        {
            var student = entities.Ведение_заказов.ToList();

            switch (Filter.SelectedItem)
            {
                case "Все":
                    break;
                case "Самовывоз":
                    student = student.Where(p => p.название_доставки == "Самовывоз").ToList();
                    break;
                case "Доставка":
                    student = student.Where(p => p.название_доставки == "Доставка").ToList();
                    break;
                
            }
            list_zakazov.ItemsSource = student;
        }

      
      

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void refresh_button_Click(object sender, RoutedEventArgs e)
        {
            list_zakazov.ItemsSource = entities.Ведение_заказов.ToList();
            list_zakazov.SelectedIndex = -1;
        }

        private void bd_zakazy_Click(object sender, RoutedEventArgs e)
        {
            var window = new workZakazy();
            window.Show();
            this.Close();
        }
    }
    }


    



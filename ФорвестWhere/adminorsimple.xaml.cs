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
    /// Логика взаимодействия для adminorsimple.xaml
    /// </summary>
    public partial class adminorsimple : Window
    {
        public adminorsimple()
        {
            InitializeComponent();
        }

        private void status_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Заказы2();
            window.Show();
            this.Close();
        }

        private void make_zakaz_button_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new Новый_заказ();
            window1.Show();
            this.Close();
        }

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new Авторизация();
            window1.Show();
            this.Close();
        }

        private void see_tovary_Click(object sender, RoutedEventArgs e)
        {
            var window = new seeTovary();
            window.Show();
            this.Close();
        }
    }
    }


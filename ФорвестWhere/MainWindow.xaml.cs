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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        
        public MainWindow()
        {
            InitializeComponent();
            
            
        }
       

        private void tovary_button_Click(object sender, RoutedEventArgs e)
        {
            var window = new Товары();
            window.Show();
            this.Close();
        }

        private void klienty_button_Click(object sender, RoutedEventArgs e)
        {
            var window2 = new Клиенты();
            window2.Show();
            this.Close();
        }

        private void zakazy_button_Click(object sender, RoutedEventArgs e)
        {
            var window3 = new Заказы1();
            window3.Show();
            this.Close();
        }

        

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window3 = new Авторизация();
            window3.Show();
            this.Close();
        }
    }
}

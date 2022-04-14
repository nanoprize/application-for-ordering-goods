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
    /// Логика взаимодействия для seeTovary.xaml
    /// </summary>
    public partial class seeTovary : Window
    {
        Entities entities = new Entities();
        public seeTovary()
        {
            InitializeComponent();
            list_tovarov.ItemsSource = entities.Товары_магазина.ToList();
        }

        private void nazad_button_Click(object sender, RoutedEventArgs e)
        {
            var window1 = new adminorsimple();
            window1.Show();
            this.Close();
        }
    }
}

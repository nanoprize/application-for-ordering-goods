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
    /// Логика взаимодействия для Авторизация.xaml
    /// </summary>
    public partial class Авторизация : Window
    {
        Entities entities = new Entities();
        public Авторизация()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            bool b = false;
            foreach (var user in entities.Пользователи)
            {
                if (loginBox.Text == "" || passwordBox.Password == "" || textCaptcha.Text == "")
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (loginBox.Text == "vera" && passwordBox.Password == "1111" && textCaptcha.Text == captchaBox.Text)
                {

                    MessageBox.Show("Капча введенна верно", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    var window = new MainWindow();
                    window.Show();
                    this.Close();
                    b = true;
                }

                else if (loginBox.Text == "maks" && passwordBox.Password == "2222" && textCaptcha.Text == captchaBox.Text)
                {

                    MessageBox.Show("Капча введенна верно", "Подтверждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    var window = new adminorsimple();
                    window.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Проверьте правильность введенных данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                break;
            }
           
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int num = rand.Next(6, 8);
            string capthca = "";
            int totl = 0;
            do
            {
                int chr = rand.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    capthca = capthca + (char)chr;
                    totl++;
                    if (totl == num)
                        break;
                    {

                    }
                }
            } while (true);
            textCaptcha.Text = capthca;
        }
    }
}

    


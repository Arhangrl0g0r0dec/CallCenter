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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CallCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        MainWindow main;
        public AutoPage(MainWindow window)
        {
            InitializeComponent();
            main = window;
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {//проверка логина и пароля на заполенность
            if (!String.IsNullOrEmpty(LoginBox.Text))
            {
                if (!String.IsNullOrEmpty(PassBox.Password))
                {//зпрос на вытягивание данных об операторах
                    IQueryable<Employee> employee = DBContext.GetContext().Employee.Where(p => p.Login == LoginBox.Text && p.Password == PassBox.Password);
                    if (employee.Count() != 0)
                    {//запрос на поиск оператора
                        main.listCab.ItemsSource = DBContext.GetContext().Employee.Where(p => p.Login == LoginBox.Text && p.Password == PassBox.Password).ToList();
                        main.listCab.SelectedIndex = 0;
                        main.FrameContainer.Navigate(new CompanesPage(this, main));
                    }//Обработка ошибки
                    else
                    {
                        MessageBox.Show("Введенные логин или пароль не верны!");
                    }
                }
                else
                {//Обработка ошибки
                    MessageBox.Show("Введите пароль!");
                }
            }
            else
            {
                MessageBox.Show("Введите логин!");
            }
        }
    }
}

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
    /// Логика взаимодействия для CompanesPage.xaml
    /// </summary>
    public partial class CompanesPage : Page
    {/// <summary>
    /// Булевы переменные для определения компании
    /// </summary>

        MainWindow main;
        AutoPage auto;
        //Конструктор включащий в себя MainWindow, AutoPage, метод SeeCompany()
        public CompanesPage(AutoPage autoPage, MainWindow window)
        {
            InitializeComponent();
            main = window;
            auto = autoPage;
            SeeCompany();
        }

        public void SeeCompany()
        {
            var IDCompany = (Employee)main.listCab.SelectedItem;

            var companys = DBContext.GetContext().IntercoCompanies.ToList(); //Создание листа с данными 

            if (companys.Count != 0)
            {
                companys = companys.Where(p => p.ID == IDCompany.ID).ToList();
                listCompany.ItemsSource = companys;
            }
            else
                MessageBox.Show("Список компаний данного сотрудника пуст!");

            if (!String.IsNullOrEmpty(txtCust.Text)) 
            {//Поиск компаний по TextBox
                companys = companys.Where(p => p.Title.Contains(txtCust.Text)).ToList();
                listCompany.ItemsSource = companys;
            }
        }

        private void listCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//выбор компании
            main.FrameContainer.Navigate(new ClientsPage(this, main, auto));
        }

        private void txtCust_TextChanged(object sender, TextChangedEventArgs e)
        {//вывод компании по поисковому запросу
            SeeCompany();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {//кнопка назад
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            main.Close();
        }
    }
}

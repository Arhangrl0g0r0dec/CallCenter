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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        AutoPage auto;
        MainWindow main;
        CompanesPage companes;
        public ClientsPage(CompanesPage companesPage, MainWindow window, AutoPage autoPage)
        {
            InitializeComponent();
            auto = autoPage;
            main = window;
            companes = companesPage;
            SeeClients();
        }

        public void SeeClients() 
        {
            var customers = DBContext.GetContext().Client.ToList();
            var comp = (IntercoCompanies)companes.listCompany.SelectedItem;

            customers = customers.Where(p => p.IntercoCompanies.ID == comp.ID).ToList();
            dataClients.ItemsSource = customers;

            if (!String.IsNullOrEmpty(TBPhone.Text))
            {
                customers = customers.Where(p => p.Phone.Contains(TBPhone.Text)).ToList();
                dataClients.ItemsSource = customers;
            }
        }

        private void btnSeeApply_Click(object sender, RoutedEventArgs e)
        {
            main.FrameContainer.Navigate(new ApplycationsPage(this, main));
        }

        private void btnApply_Click(object sender, RoutedEventArgs e)
        {
            main.FrameContainer.Navigate(new ApplicationPage(main, this));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            main.FrameContainer.Navigate(new CompanesPage(auto, main));
        }

        private void TBPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            SeeClients();
        }
    }
}

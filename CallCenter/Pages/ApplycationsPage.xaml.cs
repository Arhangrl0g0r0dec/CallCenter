using Gremlin.Net.Process.Traversal;
using Microsoft.VisualStudio.Services.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace CallCenter.Pages
{
    /// <summary>
    /// Логика взаимодействия для ApplycationsPage.xaml
    /// </summary>
    public partial class ApplycationsPage : Page
    {

        MainWindow window;
        ClientsPage clients;
        Client clientSelected;

        //конструктор включающий метод просмотра заявок
        public ApplycationsPage(ClientsPage clientPage, MainWindow main)
        {
            InitializeComponent();
            window = main;
            clients = clientPage;
            SeeAplications();
        }
        
        public void SeeAplications() 
        {
            clientSelected = (Client)clients.dataClients.SelectedItem; //Получение выделенного элемента

            if (clients.dataClients.SelectedItem != null)
            {
                var services = DBContext.GetContext().Services.ToList();

                var query = DBContext.GetContext().Applications.Where(p => p.Client.ID == clientSelected.ID).ToList(); 

                //заполнение данными DataGridApplication

                dataApplication.ItemsSource = query;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {//кнопка назад
            window.FrameContainer.Navigate(clients);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            List<Applications> app = dataApplication.SelectedItems.Cast<Applications>().ToList();
            try
            {
                if (dataApplication.SelectedItems != null)
                {
                    DBContext.GetContext().Applications.RemoveRange(app);
                    MessageBoxResult DialogResult = MessageBox.Show("Вы уверены что хотите удалить данные?", "Удалить", MessageBoxButton.YesNo);
                    switch (DialogResult)
                    {
                        case MessageBoxResult.Yes:
                            DBContext.GetContext().SaveChanges();

                            var services = DBContext.GetContext().Services.ToList();
                            var query = DBContext.GetContext().Applications.Where(p => p.Client.ID == clientSelected.ID).ToList();
                            dataApplication.ItemsSource = query;

                            MessageBox.Show("Заявка успешно удалена!", "Готово!");
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                }
                else
                    MessageBox.Show("Выберите заявку которую хотите удалить");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + ex.Message);
            }
            
        }
    }
}

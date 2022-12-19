using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для ApplicationPage.xaml
    /// </summary>
    public partial class ApplicationPage : Page
    {
        ClientsPage clients;
        MainWindow main;
        private Applications _curentApplications = new Applications();
        Services Serv;
        public ApplicationPage(MainWindow window, ClientsPage clientsPage)
        {
            InitializeComponent();

            clients = clientsPage;
            main = window;

            var service = DBContext.GetContext().Services.ToList();

            var applycation = DBContext.GetContext().Applications.ToList();
            int num = applycation.Count + 1;
            
            textNum.Text = Convert.ToString(num);

            comboService.ItemsSource = service;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            main.FrameContainer.Navigate(clients);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            main.FrameContainer.Navigate(new ApplycationsPage(clients, main));
        }

        private void comboService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var client = (Client)clients.dataClients.SelectedItem;

            var contract = from c in DBContext.GetContext().Contracts
                           where c.ClientID == client.ID
                           select c.OptionID;

            var NeedService = DBContext.GetContext().Services.Where(p => p.OptionID == contract.FirstOrDefault()).Select(p => p.Title).ToList();

            Serv = (Services)comboService.SelectedItem;
            _curentApplications.ServicesID = Serv.ID;
            _curentApplications.ClientID = client.ID;
            for(int i = 0; i < NeedService.Count; i++)
            {
                var option = DBContext.GetContext().Options.Where(p => p.ID == Serv.OptionID).Select(p => p.Title).ToList();
                textOption.Text = option[0];

                var price = DBContext.GetContext().Options.Where(p => p.ID == Serv.OptionID).Select(p => p.Price).ToList();
                
                if (Serv.Title != NeedService[i])
                {
                    textProblem.Text = "Данная заявка будет платной!";
                    textPrice.Text = $"{price[0]} руб.";
                }
                else
                {
                    textProblem.Text = "Данная заявка бесплатна.";
                    textPrice.Text = $"0 руб.";
                    break;
                }
            }
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (Serv.Title == null)
                stringBuilder.AppendLine("Вы не выбрали проблему!");
            else if (textOption.Text == null)
                stringBuilder.AppendLine("Не удалось определить необходиму услугу! Проверьте выбрана ли проблема! Если проблема выбрана, но услуга не выводится, тогда нажмите на кнопку \"справка\".");
            else
            {
                var client = (Client)clients.dataClients.SelectedItem;
                List<Applications> applications = DBContext.GetContext().Applications.Where(p => p.ClientID == client.ID).ToList();
                for (int i = 0; i <= applications.Count; i++)
                {
                    if (_curentApplications != applications[i])
                    {
                        AddAndSave();
                        break;
                    }
                    else 
                    {
                        var result = MessageBox.Show("В базе данных уже существует данная запись, вы уверены что хотите добавить её повторно?","Продолжить?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            AddAndSave();
                        }
                    }
                }
            }
        }

        private void AddAndSave() //Метод добавления и сохранения изменений в БД
        {
            _curentApplications.DateOfCompletion = DateTime.Now;
            DBContext.GetContext().Applications.Add(_curentApplications);
            try
            {
                DBContext.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка!" + ex);
            }
        }
    }
}

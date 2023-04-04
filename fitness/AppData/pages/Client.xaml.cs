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
using fitness.AppConnect;
using fitness.AppData.pages;
using static fitness.AppConnect.Connect;

namespace fitness.AppData.pages
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class ViewClient : Page
    {
        Client[] FindClient()
        {
            List<Client> client = modelodb.Client.ToList();
            if (txbSearch != null)
            {
                client = client.Where(x => (x.lName + " " + x.fName + " " + x.mName).ToLower().Contains(txbSearch.Text.ToLower())).ToList();

                if (cbOrder.SelectedIndex > 0)
                {
                    client = client.Where(x => x.Trainer1.lName + " " + x.Trainer1.fName + " " + x.Trainer1.mName == cbOrder.SelectedItem.ToString()).ToList();
                }

                if (cbFilter.SelectedIndex > 0)
                {
                    client = client.Where(x => x.Subscription1.name == cbFilter.SelectedItem.ToString()).ToList();
                }
            }

            return client.ToArray();
        }

        private void SetOrder()
        {
            cbOrder.Items.Add("Без фильтра");
            foreach (var item in modelodb.Trainer)
            {
                cbOrder.Items.Add(item.lName + " " + item.fName + " " + item.mName);
            }
            cbOrder.SelectedIndex = 0;
        }

        private void SetFilter()
        {
            cbFilter.Items.Add("Без фильтра");
            foreach (var item in modelodb.Subscription)
            {
                cbFilter.Items.Add(item.name);
            }
            cbFilter.SelectedIndex = 0;
        }

        public ViewClient()
        {
            InitializeComponent();
            lbView.ItemsSource = FindClient();
            SetFilter();
            SetOrder();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbView.ItemsSource = FindClient();
        }

        private void cbOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbView.ItemsSource = FindClient();
        }

        private void txbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lbView.ItemsSource = FindClient();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var clientObj = lbView.SelectedItems.Cast<Client>().ToList();
            try
            {
                modelodb.Client.RemoveRange(clientObj);
                modelodb.SaveChanges();
                MessageBox.Show("Клиент удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                lbView.ItemsSource = FindClient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new CreateClient());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            lbView.ItemsSource = FindClient();
        }

        private void btn_Trainer_click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new ViewTrainer());
        }

        private void btn_Report_click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new ViewReport());
        }
    }
}

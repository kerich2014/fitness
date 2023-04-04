using fitness.AppConnect;
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
using static fitness.AppConnect.Connect;

namespace fitness.AppData.pages
{
    /// <summary>
    /// Логика взаимодействия для Report.xaml
    /// </summary>
    public partial class ViewReport : Page
    {
        Client[] FindClient()
        {
            List<Client> client = modelodb.Client.ToList();
            if (dp_first != null && dp_second != null)
            {
                client = client.Where(x => x.dateStart >= dp_first.SelectedDate && x.dateStart <= dp_second.SelectedDate).ToList();
                int sum = 0;
                foreach (var item in client)
                {
                    sum += item.Subscription1.cost;
                }
                lb_result.Content = sum.ToString();
            }

            return client.ToArray();
        }
        public ViewReport()
        {
            InitializeComponent();
            lbView.ItemsSource = FindClient();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.GoBack();
        }

        private void dp_first_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lbView.ItemsSource = FindClient();
        }

        private void dp_second_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lbView.ItemsSource = FindClient();
        }
    }
}

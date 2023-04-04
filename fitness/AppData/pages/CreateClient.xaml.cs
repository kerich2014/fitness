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

namespace fitness.AppData.pages
{
    /// <summary>
    /// Логика взаимодействия для CreateClient.xaml
    /// </summary>
    public partial class CreateClient : Page
    {
        private Client _client = new Client();
        public CreateClient()
        {
            InitializeComponent();
            DataContext = _client;
            SetSubscription();
            SetTrainer();
        }

        private void SetSubscription()
        {
            cb_Subscription.Items.Add("Нет выбора");
            foreach (var item in Connect.modelodb.Subscription)
            {
                cb_Subscription.Items.Add(item.name);
            }
            cb_Subscription.SelectedIndex = 0;
        }

        private void SetTrainer()
        {
            cb_Trainer.Items.Add("Нет выбора");
            foreach (var item in Connect.modelodb.Trainer)
            {
                cb_Trainer.Items.Add(item.lName + " " + item.fName + " " + item.mName);
            }
            cb_Trainer.SelectedIndex = 0;
        }

        private void FindSubscription()
        {
            var client = Connect.modelodb.Client.FirstOrDefault(x => x.subscription == x.subscription);
            var subscription = Connect.modelodb.Subscription.FirstOrDefault(x => x.id == client.subscription);
            cb_Subscription.SelectedItem = subscription.name;
        }

        private void btn_Back_click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.GoBack();
        }

        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            _client.lName = txb_lName.Text;
            _client.fName = txb_fName.Text;
            _client.mName = txb_mName.Text;
            _client.email = txb_email.Text;
            _client.phone = txb_phone.Text;
            _client.subscription = cb_Subscription.SelectedIndex;
            _client.dateStart = dp_Start.SelectedDate.Value;
            _client.dateEnd = dp_End.SelectedDate.Value;
            _client.trainer = cb_Trainer.SelectedIndex;


            Entities.GetContext().Client.Add(_client);

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Клиент добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            AppFrame.mainFrame.GoBack();
        }
    }
}

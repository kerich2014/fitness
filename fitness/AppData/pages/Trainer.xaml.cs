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
    /// Логика взаимодействия для Trainer.xaml
    /// </summary>
    public partial class ViewTrainer : Page
    {
        Trainer[] FindTrainer()
        {
            List<Trainer> trainer = modelodb.Trainer.ToList();
            if (txbSearch != null)
            {
                trainer = trainer.Where(x => (x.lName + " " + x.fName + " " + x.mName).ToLower().Contains(txbSearch.Text.ToLower())).ToList();
            }

            return trainer.ToArray();
        }
        public ViewTrainer()
        {
            InitializeComponent();
            lbView.ItemsSource = FindTrainer();
        }

        private void txbSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            lbView.ItemsSource = FindTrainer();
        }

        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var trainerObj = lbView.SelectedItems.Cast<Trainer>().ToList();
            try
            {
                modelodb.Trainer.RemoveRange(trainerObj);
                modelodb.SaveChanges();
                MessageBox.Show("Клиент удален", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                lbView.ItemsSource = FindTrainer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.Navigate(new CreateTrainer());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
            lbView.ItemsSource = FindTrainer();
        }
    }
}

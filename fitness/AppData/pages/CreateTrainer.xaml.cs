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
    /// Логика взаимодействия для CreateTrainer.xaml
    /// </summary>
    public partial class CreateTrainer : Page
    {
        private Trainer _trainer = new Trainer();
        public CreateTrainer()
        {
            InitializeComponent();
            SetSpecialization();
        }

        private void SetSpecialization()
        {
            cb_Subscription.Items.Add("Нет выбора");
            foreach (var item in Connect.modelodb.Specialization)
            {
                cb_Subscription.Items.Add(item.name);
            }
            cb_Subscription.SelectedIndex = 0;
        }

        private void btn_Back_click(object sender, RoutedEventArgs e)
        {
            AppFrame.mainFrame.GoBack();
        }

        private void btn_Create_Click(object sender, RoutedEventArgs e)
        {
            _trainer.lName = txb_lName.Text;
            _trainer.fName = txb_fName.Text;
            _trainer.mName = txb_mName.Text;
            _trainer.email = txb_email.Text;
            _trainer.phone = txb_phone.Text;
            _trainer.specialization = cb_Subscription.SelectedIndex;


            Entities.GetContext().Trainer.Add(_trainer);

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Тренер добавлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            AppFrame.mainFrame.GoBack();
        }
    }
}

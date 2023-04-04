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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
            modelodb = new Entities();
        }

        private void btn_enter_Click(object sender, RoutedEventArgs e)
        {
            var userObj = modelodb.User.FirstOrDefault(x => x.login == txb_login.Text && x.password == psb_password.Password);
            if(userObj != null)
            {
                try
                {
                    AppFrame.mainFrame.Navigate(new ViewClient());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                txb_login.Text = "";
                psb_password.Password = "";
            }
        }
    }
}

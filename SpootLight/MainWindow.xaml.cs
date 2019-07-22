using SpootLight.Controllers;
using SpootLight.Popup;
using SpootLight.Views.Analyse;
using SpootLight.Views.PR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace SpootLight
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<string> user = new List<string>();
        AuthentificationController Ac = new AuthentificationController();
        public MainWindow()
        {
            InitializeComponent();
            DateSystem.Content = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void Forgetpass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OublieMotPasse o = new OublieMotPasse();
            o.Show();
            this.Close();
        }
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        private void Forgetpass_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            forgetpass.Foreground = Brushes.Red;
        }

        private void Forgetpass_MouseLeave(object sender, MouseEventArgs e)
        {
            forgetpass.Foreground = Brushes.Black;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Connexion_Click(object sender, RoutedEventArgs e)
        {
            if (Ac.CheckEmailFormat(SG_login_txt))
            {
                if (Ac.Connect_User(SG_login_txt, SG_pass_txt))
                {
                    this.Close();
                }
                else
                {
                    SG_login_txt.Text = "";
                    SG_pass_txt.Password = "";
                }
            }
        }
        public static void setUser(List<string> user)
        {
            MainWindow.user = user;
        }
        public static List<string> getUser()
        {
            return user;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SG_login_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Ac.CheckEmailFormat(SG_login_txt);
        }
        
    }
}

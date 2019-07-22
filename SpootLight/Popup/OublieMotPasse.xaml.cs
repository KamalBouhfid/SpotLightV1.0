using SpootLight.Controllers;
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
using System.Windows.Shapes;

namespace SpootLight.Popup
{
    /// <summary>
    /// Logique d'interaction pour OublieMotPasse.xaml
    /// </summary>
    public partial class OublieMotPasse : Window
    {
        public int BackUpCode=0;
        public string BackUpMail = "";
        AuthentificationController Ac = new AuthentificationController();
        public OublieMotPasse()
        {
            InitializeComponent();
            NbPassword.Content = "NB: Votre mot de passe doit être entre 8 et 15 Caractères \n (contient au moins un Caractère Majuscule et un nombre)";
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void RecupererMotpasse_Click(object sender, RoutedEventArgs e)
        {
            if (Ac.CheckEmailFormat(EmailRecupererTxt))
            {
                if (Ac.Checkemail(EmailRecupererTxt.Text))
                {
                    BackUpCode = Ac.sendEmailCodeConfirmation(EmailRecupererTxt.Text);
                    if (BackUpCode != 0)
                    {
                        BackUpMail = EmailRecupererTxt.Text;
                        Console.WriteLine("the code is :" + BackUpCode);
                        PinCodePanel.Visibility = Visibility.Visible;
                        EmailRecupererTxt.IsReadOnly = true;
                        RecupererMotpasse.Visibility = Visibility.Hidden;
                        RecupererPin.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("Adresse Email Invalide, Merci de vérifier !", "Connexion Erreur", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void RecupererPin_Click(object sender, RoutedEventArgs e)
        {
            if (Ac.CheckPinFormat(BackUpCodeTxt))
            {
                if (Convert.ToInt32(BackUpCodeTxt.Text) == BackUpCode)
                {
                    MessageBox.Show("Merci de créer Un nouveau Mot de passe !", "Pin Vérifiée !", MessageBoxButton.OK, MessageBoxImage.Information);
                    NewPassPanel.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Pin Invalide ! \n Merci de vérifier votre boite des E-mails !", "Pin Invalide !", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void ChangerPass_Click(object sender, RoutedEventArgs e)
        {
            if (Ac.CheckPasswordFormat(Newpass) && Ac.CheckPasswordFormat(NewpassVerification))
            {
                string Pass = Newpass.Password.ToString();
                string PassVerification = NewpassVerification.Password.ToString();
                if (Pass.Equals(PassVerification))
                {
                    AuthentificationController.UpdatePassword(BackUpMail, Pass);
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Les mots de passe n'est pas identique !", "Mot de passe Invalide !", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}

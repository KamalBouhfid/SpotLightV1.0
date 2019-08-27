using SpootLight.Controllers;
using SpootLight.Models;
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

namespace SpootLight.Views.AdminManagement
{
    /// <summary>
    /// Logique d'interaction pour Form_User.xaml
    /// </summary>
    public partial class Form_User : Window
    {
        FormController formcontroller = new FormController();
        public Form_User()
        {
            InitializeComponent();
        }
        private void ButtonMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            TextBox a = new TextBox();
            a.Text = PassTxt.Password;
            if (formcontroller.CheckNotNull(a))
            {
                USER_MODEL ctx = new USER_MODEL();
                UserSpot bac = new UserSpot();
                if (IDUser.Text.Equals(null) || IDUser.Text.Equals(""))
                {
                    Random IdVal = new Random();
                    IDUser.Text = IdVal.Next(40000).ToString();
                }
                bac.id = Convert.ToInt32(IDUser.Text);
                bac.firstname = FirstnameTxt.Text;
                bac.lastname = LastnameTxt.Text;
                bac.email = EmailTxt.Text;
                bac.pass = AuthentificationController.EncryptDecrypt(PassTxt.Password.ToString(), 128);

                ctx.InsertOrUpdate(bac);
                this.Close();
            }
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

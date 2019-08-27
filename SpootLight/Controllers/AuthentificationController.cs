using SpootLight.Models;
using SpootLight.Views.Analyse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpootLight.Controllers
{
    class AuthentificationController
    {
        public static SpotLightDBEntities1 crudctx = new SpotLightDBEntities1();
        private static List<string> user = new List<string>();
        private static Random r = new Random();
        private static string code;
        public bool Connect_User(TextBox SG_Login, PasswordBox SG_Pass)
        {
            var map = new Dictionary<string, string>();
            string MotDePasse = EncryptDecrypt(SG_Pass.Password.ToString(),128);
            var logedOne =
                crudctx.UserSpot.Where(c => c.email.Equals(SG_Login.Text) && c.pass.Equals(MotDePasse)).FirstOrDefault();
            if (logedOne != null)
            {
                user.Add(logedOne.id.ToString());
                user.Add(logedOne.firstname);
                user.Add(logedOne.lastname);
                user.Add(logedOne.email);
                user.Add(logedOne.GroupsUserSpot.name);
                Globals.setUser(null);
                Globals.setUser(user);
                setUser(user);
                OpenAnalyse openanalyse = new OpenAnalyse();
                openanalyse.setUser(user);
                openanalyse.Show();
                Console.WriteLine("check  !" + Globals.getUser()[1]);
                //Console.WriteLine("check 04 !" + Globals.GetTableName(typeof(PR_CURRENCY)));
                return true;
            } else
            {
                MessageBox.Show("Le login ou Mot de passe Invalide !","Connexion Erreur",MessageBoxButton.OK,MessageBoxImage.Error);
                return false;
            }
        }
        public void setUser(List<string> user)
        {
            //this.user = user;
        }
        public List<string> getUser()
        {
            return user;
        }
        public int sendEmailCodeConfirmation(string destinataire)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                var x = r.Next(42000);
                code = x.ToString("000000");
                mail.From = new MailAddress("kamalbouhfid@gmail.com");
                mail.To.Add(destinataire);
                mail.Subject = "Ce Email à été généré automatiquement !";
                mail.Body = "Bonjour Monsieur/Madame " +
                    "\n \n c'est ça votre code de confirmation :"+code;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("amedom45@gmail.com","Hello1231231230!");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                MessageBox.Show("Vérifiez votre Boite Email");  
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return Convert.ToInt32(code);
        }

        public static void UpdatePassword(string email, string pass)
        {

            var Updated = crudctx.UserSpot.Where(c => c.email.Equals(email)).FirstOrDefault();

            if (Updated != null)
            {
                Updated.pass = EncryptDecrypt(pass, 128);
                MessageBox.Show("Votre Mot de passe à été bien met à jour");
            }
            else
            {
                MessageBox.Show("Erreur Interne ");
            }

            crudctx.SaveChanges();
        }
        public bool Checkemail(string email)
        {

            var Updated = crudctx.UserSpot.Where(c => c.email.Equals(email)).FirstOrDefault();

            if (Updated != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string EncryptDecrypt(string szPlainText, int szEncryptionKey)
        {
            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            for (int iCount = 0; iCount < szPlainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ szEncryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }
        public bool CheckEmailFormat(TextBox Email)
        {
            Regex RX = new Regex(@"(?<email>\w+@\w+\.[a-z]{0,3})");
            Match match = RX.Match(Email.Text);
            if (match.Success)
            {
                Email.BorderBrush = Brushes.Green;
                Email.ToolTip = "Format Email valide !";
                return true;
            }
            else
            {
                Email.BorderBrush = Brushes.Red;
                Email.ToolTip = "Format Email invalide !";
                return false;
            }
        }
        public bool CheckPinFormat(TextBox Pin)
        {
            Regex RX = new Regex("^[0-9]{6}$");
            Match match = RX.Match(Pin.Text);
            if (match.Success)
            {
                Pin.BorderBrush = Brushes.Green;
                Pin.ToolTip = "Format Email valide !";
                return true;
            }
            else
            {
                Pin.BorderBrush = Brushes.Red;
                Pin.ToolTip = "Format Email invalide !";
                return false;
            }
        }
        public bool CheckPasswordFormat(PasswordBox Password)
        {
            Regex RX = new Regex("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^(.{8,15})$");
            Match match = RX.Match(Password.Password.ToString());
            if (match.Success)
            {
                Password.BorderBrush = Brushes.Green;
                Password.ToolTip = "Format Email valide !";
                return true;
            }
            else
            {
                Password.BorderBrush = Brushes.Red;
                Password.ToolTip = "Format Email invalide !";
                return false;
            }
        }
    }
}

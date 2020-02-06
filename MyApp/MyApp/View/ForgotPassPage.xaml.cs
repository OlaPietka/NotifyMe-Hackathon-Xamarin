using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyApp.Service;
using System;
using Xamarin.Essentials;
using MyApp.Models;
using System.Net.Mail;
using System.Net;

namespace MyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ForgotPassPage : ContentPage
	{
        UserRepository userRepo = new UserRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC
        PasswordRecoveryRepository passwordRecoveryRepo = new PasswordRecoveryRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC

        Random rd = new Random(); // <-------------------- DO ZMIENIENIA NA PRZYSZLOSC
        int? code; // <-------------------- DO ZMIENIENIA NA PRZYSZLOSC

        public ForgotPassPage()
		{
			InitializeComponent();

            foreach (PasswordRecovery p in passwordRecoveryRepo.GetAll()) // <-------------------- DO ZMIENIENIA NA PRZYSZLOSC. POTRZEBNE DLA ULATWIENIA
            {
                passwordRecoveryRepo.DeleteEmail(p.Id);
            }
		}

        private void SendEmail_Clicked(object sender, System.EventArgs e)
        {
            code = rd.Next(1000, 9999); // Wybiera randomowy numer do weryfikacji <--------------------- DO ZMIENIENIA NA PRZYSZLOSC

            if (userRepo.IsEmailExist(Email.Text)) // Sprawdza czy podany email istnieje w bazie uzytkownikow <--------------------------- DO ZMIENIENIA NA PRZYSZLOSC PRZY ZMIANIE BAZY DANYCH
            {
                if (!passwordRecoveryRepo.IsEmailExist(Email.Text)) // Sprawdza czy podany email przypadkiem juz nie istnieje w bazie odzyskiwania hasla <--------------------------- DO ZMIENIENIA NA PRZYSZLOSC PRZY ZMIANIE BAZY DANYCH
                {
                    PasswordRecovery entity = new PasswordRecovery() { Email = Email.Text, Code = code.ToString(), CurrentTime = DateTime.Now};

                    LabelEmail.Text = "Wyslalismy kod na podany email. Troche to kurwa zajmie okej?";

                    Send(code.ToString());

                    Preferences.Set("resetPass_Email", Email.Text); //dodaje referencje
                    passwordRecoveryRepo.InsertEmail(entity); // dodaje do bazy
                }else
                    LabelEmail.Text = "Na podany email zostal wyslany juz kod";
            }
            else
                LabelEmail.Text = "Email nie istnieje";
        }

        async void VerifyCode_Clicked(object sender, System.EventArgs e)
        {
            var emailRef = Preferences.Get("resetPass_Email", "");

            if (VerificationCode.Text == null || VerificationCode.Text == String.Empty || code == null || emailRef == String.Empty)
                LabelVerifyCode.Text = "Pierw wprowadz dane";
            else if (VerificationCode.Text.Equals(passwordRecoveryRepo.GetByEmail(emailRef).Code))
            {
                if(DateTime.Now.Subtract(passwordRecoveryRepo.GetByEmail(emailRef).CurrentTime).Minutes <= 5)
                    await Navigation.PushModalAsync(new CreateNewPasswordPage());
                else
                    LabelVerifyCode.Text = "Podany kod wygasl";
            }
            else
                LabelVerifyCode.Text = "Podany kod jest zły.";
        }

        public void Send(string code)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.EnableSsl = true;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new NetworkCredential("xamarinemailtest@gmail.com", "Bazilla11"); // < ---------------------------DO ZMIENIENIA NA PRZYSZLOSC

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("olapietka9@gmail.com"); // < ---------------------------DO ZMIENIENIA NA PRZYSZLOSC
            mail.To.Add("s19468@pjwstk.edu.pl"); // < ---------------------------DO ZMIENIENIA NA PRZYSZLOSC
            mail.Subject = "Twoja stara CHUJ CI W DUPE";
            mail.Body = code;

            SmtpServer.Send(mail);
        }
    }
}
using MyApp.Models;
using MyApp.Service;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateNewPasswordPage : ContentPage
	{
        UserRepository userRepo = new UserRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC
        PasswordRecoveryRepository passwordRecoveryRepo = new PasswordRecoveryRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC

        public CreateNewPasswordPage ()
		{
			InitializeComponent ();
		}

        private async void ChangePassword_Clicked(object sender, EventArgs e)
        {
            if (!IsPasswordCorrect() || !IsPasswordDuplicationCorrect())
            {
                if (!IsPasswordCorrect()) ErrorMessage(NewPassword, LabelNewPassword, "Hasło musi zawierać więcej niż 6 znaków.");
                else if (!IsPasswordCorrect()) ErrorMessage(NewRepeatPassword, LabelNewPassword, "Hasla sie nie zgadzaja.");
            }
            else
            {
                var emailRef = Preferences.Get("resetPass_Email", "");

                if (emailRef != String.Empty)
                {
                    User user = userRepo.GetUserByEmail(emailRef);  // < --------------------DO ZMIENIENIA NA PRZYSZLOSC PRZY ZMIANIE BAZY. + 3 LINIJKI NIZEJ

                    userRepo.UpdateUser("Password = \"" + NewPassword.Text + "\"", user.Id);

                    passwordRecoveryRepo.DeleteEmail(passwordRecoveryRepo.GetByEmail(emailRef).Id);

                    Preferences.Remove("resetPass_Email");

                    await Navigation.PushModalAsync(new MainPage());
                }
                else
                    Console.WriteLine("COS KURWA POSZLO NIE TAK"); //  <-------------------- DO ZMIENIENIA NA PRZYSZLOSC
            }
        }

        async void ErrorMessage(Custom.AppEntry field, Label label, string errorMessage) // Wypisuje blad
        {
            field.Text = string.Empty;
            field.BorderColor = Color.Red;

            label.Text = errorMessage;

            label.Opacity = 0;
            await label.FadeTo(1, 300);
        }

        bool IsPasswordCorrect() // Czy haslo ma wiecej liter niz 5 i czy nie jest puste
        {
            if (NewPassword.Text == null)
                return false;

            return NewPassword.Text.Length > 5;
        }

        bool IsPasswordDuplicationCorrect() // Czy pole nie jest puste oraz czy dwa hasla sa sobie rowne
        {
            if (NewPassword.Text == null || NewRepeatPassword.Text == null)
                return false;

            return NewPassword.Text.Equals(NewRepeatPassword.Text);
        }
    }
}
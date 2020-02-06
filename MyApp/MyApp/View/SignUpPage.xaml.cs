using MyApp.Models;
using MyApp.Service;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        UserRepository userRepo = new UserRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC
        const string path = "MyApp/MyApp.Android/Resources/drawable/";

        public SignUpPage()
        {
            InitializeComponent();
            userRepo.InsertUser(new User { Username = "admin", Password = "admin", Age = DateTime.Now, Email = "admin@admin.pl" }); // <------------------- DO USUNIECIA NA PRZYSZLOSC. POTRZEBNE DLA ULATWIENIA
        }

        async void Button_SignUp_Clicked(object sender, EventArgs e)
        {
            // Pierw sprawdza czy podane dane sa poprawne
            if (IsLoginCorrect()
                && IsPasswordCorrect()
                && IsPasswordDuplicationCorrect()
                && IsDateCorrect()
                && IsEmailCorrect())
            {
                // Jezeli dane sa poprawne, tworzy nowego uzytkownika 
                var newUser = new User() { Username = Username.Text, Password = Password.Text, Email = Email.Text, Age = DatePicker.Date, ProfileImage = new byte[16 * 1024], BannerImage = new byte[16 * 1024] };

                // Sprawdza czy podany uzytkownik juz istnieje w bazie
                if (!(userRepo.IsUsernameExist(newUser.Username) || userRepo.IsEmailExist(newUser.Email)))
                {
                    userRepo.InsertUser(newUser); // Dodaje do bazy <-------------------------- DO ZMIANY NA PRZYSZLOSC PRZY ZMIENIANIU BAZY

                    await Navigation.PushModalAsync(new MainPage());
                }
                else
                {
                    if (userRepo.IsUsernameExist(newUser.Username)) ErrorMessage(Username, LabelUsername, "Podana nazwa użytkownika jest już zajęta.");
                    if (userRepo.IsEmailExist(newUser.Email)) ErrorMessage(Email, LabelEmail, "Podany adres E-mail jest już powiązany z kontem.");
                }
            }
            else
            {
                if (!IsLoginCorrect()) ErrorMessage(Username, LabelUsername, "Nazwa użytkownika musi mieć conajmniej 3 znaki oraz nie może posiadać znaków szczególnych.");
                if (!IsPasswordCorrect()) ErrorMessage(Password, LabelPassword, "Hasło musi zawierać więcej niż 6 znaków.");
                else if (!IsPasswordDuplicationCorrect()) ErrorMessage(RepeatPassword, LabelRepeatPassword, "Podane hasła nie są identyczne.");
                if (!IsEmailCorrect()) ErrorMessage(Email, LabelEmail, "Podany adres E-mail jest nieprawidłowy.");
            }
        }

        async void ErrorMessage(Custom.AppEntry field, Label label, string errorMessage) // Wypisuje blad i tworzy animacje wibracji okna
        {
            field.Text = string.Empty;
            field.BorderColor = Color.Red;
            label.Text = errorMessage;

            char[] delimiters = new char[] { ' ', '\r', '\n' };
            int count = errorMessage.Length;

            if (count > 40)
                label.HeightRequest = 8 + 7 * ((count / 40) - 1);

            await ErrorAnimation(field, label);
        }

        async Task ErrorAnimation(Custom.AppEntry field, Label label) // Animacja wibracji okna wpisywania w przypadku bledu
        {
            if (label.Opacity == 0)
            {
                await Task.WhenAll(
                Vibration(field),
                label.FadeTo(1, 300));
            }
            else
                await Vibration(field);
        }

        async Task Vibration(Custom.AppEntry field) // Animacja wibracji 
        {
            const uint animateTime = 60;
            Easing easing = Easing.BounceOut;

            await field.TranslateTo(2, 0, animateTime, easing);
            await field.TranslateTo(-2, 0, animateTime, easing);
            await field.TranslateTo(1, 0, animateTime, easing);
            await field.TranslateTo(-1, 0, animateTime, easing);
            await field.TranslateTo(0, 0, animateTime, easing);
        }

        void Date_Selected(object sender, EventArgs e) // Jezeli ktos ma mniej niz 14 lat
        {
            if (!IsDateCorrect())
                LabelDatePicker.Text = "Nie spełniasz wymagań wiekowych.";
        }

        bool IsLoginCorrect() // Czy login ma miedzy 2 a 17 liter i nie jest puste
        {
            if (Username.Text == null)
                return false;

            return Username.Text.Length > 2 && Username.Text.Length < 17;
        }

        bool IsPasswordCorrect() // Czy haslo ma wiecej liter niz 5 i czy nie jest puste
        {
            if (Password.Text == null)
                return false;

            return Password.Text.Length > 5;
        }

        bool IsPasswordDuplicationCorrect() // Czy pole nie jest puste oraz czy dwa hasla sa sobie rowne
        {
            if (Password.Text == null || RepeatPassword.Text == null)
                return false;

            return Password.Text.Equals(RepeatPassword.Text);
        }

        bool IsDateCorrect() // Czy wiek jest wiekszy niz 14 lat
        {
            return 2019 - DatePicker.Date.Year > 14;
        }

        bool IsEmailCorrect() // Czy email nie jest pusty
        {
            if (Email.Text == null)
                return false;

            return Email.Text.Length > 0;
        }

        private byte[] ConvertToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
using MyApp.Service;
using System;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace MyApp.View
{
    public partial class MainPage : ContentPage
    {
        UserRepository userRepo = new UserRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC

        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_LogIn_Clicked(object sender, EventArgs e) // Po kliknieciu przycisku "Zaloguj sie"
        {
            if (!userRepo.IsUsernameExist(Login.Text)) // Patrzy czy dany login istnieje w bazie <---------------------- DO ZMIENIENIA PRZY ZMIANIE BAZY DANYCH
            {
                var action = await DisplayAlert("Błąd logowania", "Nieprawidłowe hasło lub nazwa użytkownika", "Zapomniałem/am hasła", "Wróć");  // Jezeli ktos kliknie wroc to action jest false (nic sie nie dzieje) jezeli zapomni hasla to action jest true

                if (action)
                    await Navigation.PushModalAsync(new ForgotPassPage());
            }
            else
            {
                Preferences.Set("id", userRepo.GetUserByUsername(Login.Text).Id.ToString()); // Po zalogowaniu zapisuje preferences <------------------- MOZLIWE DO ZMIANY 
                Preferences.Set("username", Login.Text);
                Preferences.Set("pass", Password.Text);

                await Navigation.PushModalAsync(new NavigationBar());
            }
        }

        async void Button_SignUp_Clicked(object sender, EventArgs e) // Po kliknieciu przycisku "Zarejestruj sie"
        {
            await Navigation.PushModalAsync(new SignUpPage());
        }

        async void Button_ForgotPass_Clicked(object sender, EventArgs e) // Po kliknieciu przycisku "Zapomnialem hasla"
        {
            await Navigation.PushModalAsync(new ForgotPassPage());
        }
    }
}

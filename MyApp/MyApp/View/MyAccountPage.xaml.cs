using MyApp.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System;
using System.IO;

namespace MyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MyAccountPage : ContentPage
	{
        UserRepository userRepo = new UserRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC
        string test;

        public MyAccountPage ()
		{
            InitializeComponent ();

            Username.Text = userRepo.GetUserById(Convert.ToInt32(Preferences.Get("id", ""))).Username; // Wyswietlanie loginu przez preferences z bazy <---------------------- DO ZMIANIENIA NA PRZYSZLOSC

            ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(userRepo.GetUserById(Convert.ToInt32(Preferences.Get("id", ""))).ProfileImage)); // Wyswitla zdj profilowe <---------------------- DO ZMIANIENIA NA PRZYSZLOSC
            BannerImage.Source = ImageSource.FromStream(() => new MemoryStream(userRepo.GetUserById(Convert.ToInt32(Preferences.Get("id", ""))).BannerImage)); // Wyswitla banner <---------------------- DO ZMIANIENIA NA PRZYSZLOSC

            var cmd = new Command(() => // Komenda przy przytrzymywaniu zdj profilowego
            {
                FacebookIcon.IsVisible = true;
                InstagramIcon.IsVisible = true;
                YoutubeIcon.IsVisible = true;
                TikTokIcon.IsVisible = true;
                TwiiterIcon.IsVisible = true;
            });

            ProfileButton.LongPressCommand = cmd;

            
        }


        private void ProfileButton_Released(object sender, EventArgs e) // Kiedy zdj porofilowe zostanie puszczone
        {
            FacebookIcon.IsVisible = false;
            InstagramIcon.IsVisible = false;
            YoutubeIcon.IsVisible = false;
            TikTokIcon.IsVisible = false;
            TwiiterIcon.IsVisible = false;
        }

        private async void Settings_Clicked(object sender, EventArgs e) // Kiedy przycisk settings na toolbarze zostanie klikniety
        {
            await Navigation.PushModalAsync(new ProfileSettingsPage());
        }
    }
}
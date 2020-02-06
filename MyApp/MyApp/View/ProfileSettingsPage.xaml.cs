using MyApp.Picture;
using MyApp.Service;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfileSettingsPage : ContentPage
	{
        UserRepository userRepo = new UserRepository(); // Dolaczenie bazy lokalnej <-------------------- DO ZMIENIENIA NA PRZYSZLOSC
        public byte[] profileImage = new byte[16 * 1024];
        public byte[] bannerImage = new byte[16 * 1024];
        bool profileChanged = false;
        bool bannerChanged = false;

        public ProfileSettingsPage ()
		{
			InitializeComponent ();

            Username.Text = userRepo.GetUserById(Convert.ToInt32(Preferences.Get("id", ""))).Username;
		}

        private async void ChangeBannerButton_Clicked(object sender, System.EventArgs e)
        {
            ChangeBannerButton.IsEnabled = false;
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
          
            if (stream != null)
            {
                bannerImage = ConvertToByteArray(stream);

                BannerImage.Source = ImageSource.FromStream(() => new MemoryStream(bannerImage));

                ChangeBannerButton.IsEnabled = true;

                bannerChanged = true;
            }
            else
                ChangeBannerButton.IsEnabled = true;
        }

        private async void ChangeProfileButton_Clicked(object sender, System.EventArgs e)
        {
            ChangeProfileButton.IsEnabled = false;
            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();

            if (stream != null)
            {
                profileImage = ConvertToByteArray(stream);

                ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(profileImage));

                ChangeProfileButton.IsEnabled = true;

                profileChanged = true;
            }
            else
                ChangeProfileButton.IsEnabled = true;
        }
        string test;
        private async void AcceptButton_Clicked(object sender, EventArgs e) //<----------- do zmiany na przyszlosc
        {
            int id = Convert.ToInt32(Preferences.Get("id", ""));

            if (profileChanged)
                userRepo.UpdateUser("ProfileImage = \"" + profileImage + "\"", id);
            if(bannerChanged)
                userRepo.UpdateUser("BannerImage = \"" + bannerImage + "\"", id);

            userRepo.UpdateUser("Username = \"" + Username.Text +"\"", id);

            Preferences.Set("username", Username.Text);
            await Navigation.PushModalAsync(new MyAccountPage());
           // await Navigation.PopModalAsync();
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
using MyApp.Data;
using MyApp.Models;
using MyApp.Service;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyApp.View
{
    public partial class App : Application
    {
        static UserDatabaseController userDatabase;
        UserRepository userRepo = new UserRepository();

        static PasswordRecoveryDatabaseController passwordRecoveryDatabase;
        PasswordRecoveryRepository passwordRecoveryDRepo = new PasswordRecoveryRepository();

        public App()
        {
            #if DEBUG
            LiveReload.Init();
            #endif
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.White
                ,
                BarTextColor = Color.Black

            };
        }

        protected override void OnStart()
        {
            //Preferences.Clear();

            string id = Preferences.Get("id", "");
            string username = Preferences.Get("username", "");
            string pass = Preferences.Get("pass", "");



            if (username == String.Empty || pass == String.Empty || id == String.Empty)
            {
                App.Current.MainPage = new MainPage();
            }
            else
            {
                if (userRepo.GetUserById(Convert.ToInt32(id)).Username.Equals(username) &&
                    userRepo.GetUserById(Convert.ToInt32(id)).Password.Equals(pass))
                {
                    App.Current.MainPage = new NavigationBar();
                }
                else
                {
                    // cos sie nie zgadza, blad sesji
                    Preferences.Clear();
                }
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                    userDatabase = new UserDatabaseController();

                return userDatabase;
            }
        }

        public static PasswordRecoveryDatabaseController PasswordRecoveryDatabase
        {
            get
            {
                if (passwordRecoveryDatabase == null)
                    passwordRecoveryDatabase = new PasswordRecoveryDatabaseController();

                return passwordRecoveryDatabase;
            }
        }
    }
}

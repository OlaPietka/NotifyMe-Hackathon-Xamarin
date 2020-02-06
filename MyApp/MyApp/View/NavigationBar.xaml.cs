using MyApp.Custom;
using Xamarin.Forms.Xaml;

namespace MyApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NavigationBar : AppTabbedPage
    {
		public NavigationBar ()
		{
			InitializeComponent ();
        }
    }
}
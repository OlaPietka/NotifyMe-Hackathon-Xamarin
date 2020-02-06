using MyApp.Custom;
using Xamarin.Forms;

namespace MyApp.Validation
{
    public class PasswordValidation : Behavior<AppEntry>
    {
        AppEntry control;
        string _placeHolder;
        Color _placeHolderColor;

        protected override void OnAttachedTo(AppEntry bindable)
        {
            bindable.TextChanged += Bindable_TextChanged;
            bindable.PropertyChanged += OnPropertyChanged;
            control = bindable;
            _placeHolder = bindable.Placeholder;
            _placeHolderColor = bindable.PlaceholderColor;
        }

        void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {

            var password = e.NewTextValue;

            var passwordEntry = sender as AppEntry;

            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (password.Length < 6)
                    passwordEntry.BorderColor = Color.FromRgb(234, 234, 234);
                else
                    passwordEntry.BorderColor = Color.Red;
            }
        }

        protected override void OnDetachingFrom(AppEntry bindable)
        {
            base.OnDetachingFrom(bindable);

            bindable.TextChanged -= Bindable_TextChanged;
        }

        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == AppEntry.BorderColorProperty.PropertyName && control != null)
            {


            }
        }
    }
}

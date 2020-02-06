using System;
using Android.Content;
using Xamarin.Forms;
using MyApp.Droid.Custom;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using MyApp.Custom;
using Android.Util;
using Android.Views;

[assembly: ExportRenderer(typeof(AppButton), typeof(MyButtonRenderer))]
namespace MyApp.Droid.Custom
{
    class MyButtonRenderer : ButtonRenderer
    {
        public MyButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            UpdateGradientBackground();

            var view = (AppButton) Element;

            if (view.LongPressEnabled && view.LongPressCommand != null)
            {
                this.Control.LongClick += (s, args) =>
                {
                    view.LongPressCommand.Execute(new object());
                };
            }
        }

        
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null) return;

            UpdateGradientBackground();
        }

        protected override void UpdateBackgroundColor()
        {
            base.UpdateBackgroundColor();

            UpdateGradientBackground();
        }

        private void UpdateGradientBackground()
        {
            var button = Element as AppButton;
            if (button != null && button.GradientEnabled)
            {
                int[] colors = new int[] { button.StartColor.ToAndroid(), button.EndColor.ToAndroid() };
                var gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);

                gradientDrawable.SetGradientType(GradientType.LinearGradient);
                gradientDrawable.SetShape(ShapeType.Rectangle);
                gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(button.FixedBorderRadius)));
                gradientDrawable.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
                this.Control.Background = gradientDrawable;
            }
        }
        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
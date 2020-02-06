﻿using System;
using Android.Content;
using Xamarin.Forms;
using MyApp.Droid.Custom;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using MyApp.Custom;
using Android.Util;

[assembly: ExportRenderer(typeof(AppEntry), typeof(MyEntryRenderer))]
namespace MyApp.Droid.Custom
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            UpdateEntry();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null) return;
            UpdateEntry();
        }

        void UpdateEntry()
        {
            var element = Element as AppEntry;

            int[] colors = new int[] { element.StartColor.ToAndroid(), element.EndColor.ToAndroid() };
            var gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.TopBottom, colors);

            gradientDrawable.SetGradientType(GradientType.LinearGradient);
            gradientDrawable.SetShape(ShapeType.Rectangle);
            gradientDrawable.SetCornerRadius(DpToPixels(this.Context, Convert.ToSingle(element.BorderRadius)));
            gradientDrawable.SetStroke((int)element.BorderWidth, element.BorderColor.ToAndroid());
            this.Control.Background = gradientDrawable;
        }

        public static float DpToPixels(Context context, float valueInDp)
        {
            DisplayMetrics metrics = context.Resources.DisplayMetrics;
            return TypedValue.ApplyDimension(ComplexUnitType.Dip, valueInDp, metrics);
        }
    }
}
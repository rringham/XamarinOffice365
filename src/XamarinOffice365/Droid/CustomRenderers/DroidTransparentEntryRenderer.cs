using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinOffice365.CustomRenderers;

[assembly: ExportRenderer (typeof (TransparentEntry), typeof (XamarinOffice365.Droid.CustomRenderers.DroidTransparentEntryRenderer))]

namespace XamarinOffice365.Droid.CustomRenderers
{
    public class DroidTransparentEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null) {
                this.Control.TextSize = 24;

                this.Control.SetTextColor(Android.Graphics.Color.White);
                this.Control.SetHintTextColor(Android.Graphics.Color.Argb(255, 160, 160, 160));

                SetPlaceholderText();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Placeholder") {
                SetPlaceholderText();
            }
        }            

        private void SetPlaceholderText()
        {
            if (Element == null)
            {
                return;
            }

            string placeholderText = (string)Element.GetValue(Entry.PlaceholderProperty);
            this.Control.Hint = placeholderText;
        }
    }
}
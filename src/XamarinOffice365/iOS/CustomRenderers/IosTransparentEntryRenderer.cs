using System.ComponentModel;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinOffice365.CustomRenderers;

[assembly: ExportRenderer (typeof (TransparentEntry), typeof (XamarinOffice365.iOS.CustomRenderers.IosTransparentEntryRenderer))]

namespace XamarinOffice365.iOS.CustomRenderers
{
    public class IosTransparentEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null) {                           
                Control.BorderStyle = UITextBorderStyle.None;
                Control.Font = UIFont.SystemFontOfSize(24);
                Control.TextColor = UIColor.White;

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

            var placeholderAttributes = new UIStringAttributes {
                ForegroundColor = UIColor.FromRGB(160/255.0f, 160/255.0f, 160/255.0f),
                Font = UIFont.SystemFontOfSize(24)
            };

            NSAttributedString placeholder = new NSAttributedString(placeholderText, placeholderAttributes);
            Control.AttributedPlaceholder = placeholder;
        }
    }
}
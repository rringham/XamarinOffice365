using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinOffice365.CustomRenderers;

[assembly: ExportRenderer(typeof(NoHighlightListView), typeof(XamarinOffice365.Droid.CustomRenderers.DroidNoHighlightListViewRenderer))]

namespace XamarinOffice365.Droid.CustomRenderers
{
    public class DroidNoHighlightListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null) {
                Control.SetSelector(Android.Resource.Color.Transparent);
                Control.CacheColorHint = Color.Transparent.ToAndroid();
            }
        }
    }
}
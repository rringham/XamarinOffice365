using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinOffice365.CustomRenderers;

[assembly: ExportRenderer(typeof(NoHighlightListViewCell), typeof(XamarinOffice365.iOS.CustomRenderers.IosNoHighlightListViewCellRenderer))]

namespace XamarinOffice365.iOS.CustomRenderers
{
    public class IosNoHighlightListViewCellRenderer : ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {            
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UIKit.UITableViewCellSelectionStyle.None;

            return cell;
        }
    }
}
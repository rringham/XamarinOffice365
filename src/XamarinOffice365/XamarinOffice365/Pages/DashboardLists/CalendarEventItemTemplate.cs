using XamarinOffice365.CustomRenderers;
using Xamarin.Forms;

namespace XamarinOffice365.Pages.DashboardLists
{
    public class CalendarEventItemTemplate : NoHighlightListViewCell
    {
        public CalendarEventItemTemplate()
        {
            Grid cellGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgba(255, 255, 255, 30),
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = 250 }
                }
            };

            var leftLayout = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.Center, Spacing = 15 };
            leftLayout.Padding = new Thickness(20, 20, 0, 20);

            var subjectLabel = new Label { TextColor = Color.White, FontSize = 20, VerticalOptions = LayoutOptions.Center };
            subjectLabel.SetBinding(Label.TextProperty, new Binding("Subject", BindingMode.Default));
            leftLayout.Children.Add(subjectLabel);

            var locationLabel = new Label { TextColor = Color.White, FontSize = 14, VerticalOptions = LayoutOptions.Center };
            locationLabel.SetBinding(Label.TextProperty, new Binding("Location.LocationName", BindingMode.Default));
            leftLayout.Children.Add(locationLabel);

            cellGrid.Children.Add(leftLayout, 0, 0);

            var rightLayout = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.Center, Spacing = 15 };
            rightLayout.Padding = new Thickness(20, 20, 0, 20);

            var timespanLabel = new Label { TextColor = Color.White, FontSize = 20, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
            timespanLabel.SetBinding(Label.TextProperty, new Binding("ShortTimeSpan", BindingMode.Default));
            rightLayout.Children.Add(timespanLabel);

            var dateLabel = new Label { TextColor = Color.White, FontSize = 14, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
            dateLabel.SetBinding(Label.TextProperty, new Binding("ShortDate", BindingMode.Default));
            rightLayout.Children.Add(dateLabel);

            cellGrid.Children.Add(rightLayout, 1, 0);

            View = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 4),
                Children = { cellGrid }
            };
        }
    }
}
using System;
using XamarinOffice365.CustomRenderers;
using Xamarin.Forms;

namespace XamarinOffice365.Pages.DashboardLists
{
    public class MessageItemTemplate : NoHighlightListViewCell
    {
        public MessageItemTemplate()
        {
            Grid cellGrid = new Grid
                {
                    BackgroundColor = Color.FromRgba(255, 255, 255, 30),
                    RowSpacing = 0,
                    ColumnSpacing = 0,
                    ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = 100 }
                        }
                    };

            var leftLayout = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.Center, Spacing = 15 };
            leftLayout.Padding = new Thickness(20, 20, 0, 20);

            var subjectLabel = new Label { TextColor = Color.White, FontSize = 20, VerticalOptions = LayoutOptions.Center };
            subjectLabel.SetBinding(Label.TextProperty, new Binding("Subject", BindingMode.Default));
            leftLayout.Children.Add(subjectLabel);

            var locationLabel = new Label { TextColor = Color.White, FontSize = 14, VerticalOptions = LayoutOptions.Center };
            locationLabel.SetBinding(Label.TextProperty, new Binding("From.FromDescription", BindingMode.Default));
            leftLayout.Children.Add(locationLabel);

            cellGrid.Children.Add(leftLayout, 0, 0);

            var rightLayout = new StackLayout { Orientation = StackOrientation.Vertical, VerticalOptions = LayoutOptions.Center, Spacing = 15 };
            rightLayout.Padding = new Thickness(20, 20, 0, 20);

            var unreadLabel = new Label { TextColor = Color.White, FontSize = 32, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
            unreadLabel.SetBinding(Label.TextProperty, new Binding("UnreadLabel", BindingMode.Default));
            rightLayout.Children.Add(unreadLabel);

            cellGrid.Children.Add(rightLayout, 1, 0);

            View = new StackLayout
            {
                Padding = new Thickness(0, 0, 0, 4),
                Children = { cellGrid }
            };
        }
    }
}
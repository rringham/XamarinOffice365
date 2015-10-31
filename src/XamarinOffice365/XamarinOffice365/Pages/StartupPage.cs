using System;
using Xamarin.Forms;

namespace XamarinOffice365.Pages
{
    public class StartupPage : ContentPage
    {
        public StartupPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);


            var grid = new Grid()
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
            };

            // Background.jpg: Macro Background Print 9 by Jason Weymouth Photography (https://www.flickr.com/photos/jason_weymouth/)
            // License: Attribution-ShareAlike 2.0 Generic (https://creativecommons.org/licenses/by-sa/2.0/)
            // Additional license information can be found in Background.jpg.license in root of repository.
            Image backgroundImage = new Image { Source = "Background.jpg", HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, Aspect = Aspect.AspectFill, Opacity = 1 };
            backgroundImage.SetValue(Grid.RowSpanProperty, 1);
            backgroundImage.SetValue(Grid.ColumnSpanProperty, 1);
            grid.Children.Add(backgroundImage);

            StackLayout mask = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.Black, Opacity = 0.1 };
            mask.SetValue(Grid.RowSpanProperty, 1);
            mask.SetValue(Grid.ColumnSpanProperty, 1);
            grid.Children.Add(mask);

            Content = grid;
        }
    }
}
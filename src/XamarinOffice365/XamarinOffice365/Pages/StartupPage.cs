using Xamarin.Forms;
using XamarinOffice365.CustomRenderers;

namespace XamarinOffice365.Pages
{
    public class StartupPage : ContentPage
    {
        public StartupPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            Grid grid = new Grid()
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

            ScrollView scrollView = new ScrollView
            {   
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Vertical
            };

            StackLayout loginLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 10
            };

            Label loginLabel = new Label { Text = "Login to Xamarin Office 365 Demo", TextColor = Color.White, FontSize = 24 };
            loginLayout.Children.Add(new StackLayout { Children = { loginLabel }, Padding = new Thickness(0,0,0,30) });

            TransparentEntry usernameEntry = new TransparentEntry { Placeholder = "Username" };
            loginLayout.Children.Add(usernameEntry);

            TransparentEntry passwordEntry = new TransparentEntry { Placeholder = "Password", IsPassword = true };
            loginLayout.Children.Add(passwordEntry);

            Button loginButton = new Button { Text = "Log In", FontSize = 18, TextColor = Color.White, BackgroundColor = Color.FromRgb(45, 180, 40), BorderColor = Color.White, BorderRadius = 5, BorderWidth = 1 };
            loginLayout.Children.Add(new StackLayout { Children = { loginButton }, Padding = new Thickness(0,30,0,0) });

            scrollView.Content = loginLayout;
            grid.Children.Add(scrollView);

            Content = grid;
        }
    }
}
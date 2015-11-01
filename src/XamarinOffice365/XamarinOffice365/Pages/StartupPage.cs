using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinOffice365.Interfaces;

namespace XamarinOffice365.Pages
{
    public class StartupPage : ContentPage
    {
        private string _adAccessToken;
        
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

            Button loginButton = new Button { Text = "Log In", FontSize = 18, TextColor = Color.White, BackgroundColor = Color.FromRgb(45, 180, 40), BorderColor = Color.White, BorderRadius = 5, BorderWidth = 1 };
            loginButton.Clicked += LoginButton_Clicked;
            loginLayout.Children.Add(loginButton);

            scrollView.Content = loginLayout;
            grid.Children.Add(scrollView);

            Label backgroundAttributionLabel = new Label { Text = "Macro Background Print 9 by Jason Weymouth Photography", TextColor = Color.White, FontSize = 12 };
            grid.Children.Add(new StackLayout { Children = { backgroundAttributionLabel }, HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.End, Padding = new Thickness(10,0,0,6) });

            Content = grid;
        }

        private async void LoginButton_Clicked (object sender, System.EventArgs e)
        {
            const string ClientId = "YOUR-CLIENT-ID-HERE";
            const string Authority = "https://login.windows.net/common";
            const string ReturnUri = "http://localhost/connect";
            const string GraphResourceUri = "https://graph.microsoft.com";

            var auth = DependencyService.Get<IAzureActiveDirectoryAuthenticator>();
            _adAccessToken = await auth.Authenticate(Authority, GraphResourceUri, ClientId, ReturnUri);

            var exchangeService = DependencyService.Get<IOffice365ExchangeService>();
            var messages = exchangeService.GetMessages(_adAccessToken);
        }

        private Task<string> GetAccessToken()
        {
            return Task.FromResult(_adAccessToken);
        }
    }
}
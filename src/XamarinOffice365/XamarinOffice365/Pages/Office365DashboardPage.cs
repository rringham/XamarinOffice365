using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinOffice365.Interfaces;
using XamarinOffice365.Interfaces.Responses.Calendar;
using XamarinOffice365.Interfaces.Responses.Messages;
using XamarinOffice365.CustomRenderers;
using XamarinOffice365.Pages.DashboardLists;

namespace XamarinOffice365.Pages
{
    public class Office365DashboardPage : ContentPage
    {
        private readonly string _adAccessToken;
        private ObservableCollection<CalendarEvent> _calendarEvents;
        private ObservableCollection<Message> _messages;

        private Grid _backgroundGrid;
        private Grid _loadingGrid;
        private Grid _contentGrid;
        private NoHighlightListView _calendarEventListView;
        private NoHighlightListView _messageListView;
        
        public Office365DashboardPage(string adAccessToken)
        {
            if (string.IsNullOrEmpty(adAccessToken)) {
                throw new InvalidOperationException("Missing a valid access token");
            }
            
            _adAccessToken = adAccessToken;
            _calendarEvents = new ObservableCollection<CalendarEvent>();
            _messages = new ObservableCollection<Message>();

            NavigationPage.SetHasNavigationBar(this, false);

            SetupBackgroundGrid();
            SetupLoadingGrid();
            SetupContentGrid();
            Content = _backgroundGrid;

            RefreshFromOffice365();
        }

        private void SetupBackgroundGrid()
        {
            _backgroundGrid = new Grid {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            };

            // Background.jpg: Macro Background Print 9 by Jason Weymouth Photography (https://www.flickr.com/photos/jason_weymouth/)
            // License: Attribution-ShareAlike 2.0 Generic (https://creativecommons.org/licenses/by-sa/2.0/)
            // Additional license information can be found in Background.jpg.license in root of repository.
            Image backgroundImage = new Image { Source = "Background.jpg", HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, Aspect = Aspect.AspectFill, Opacity = 1 };
            backgroundImage.SetValue(Grid.RowSpanProperty, 1);
            backgroundImage.SetValue(Grid.ColumnSpanProperty, 1);
            _backgroundGrid.Children.Add(backgroundImage);

            StackLayout mask = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.Black, Opacity = 0.1 };
            mask.SetValue(Grid.RowSpanProperty, 1);
            mask.SetValue(Grid.ColumnSpanProperty, 1);
            _backgroundGrid.Children.Add(mask);
        }

        private void SetupLoadingGrid()
        {            
            _loadingGrid = new Grid {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                }
            }; 

            StackLayout loadingLayout = new StackLayout { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
            loadingLayout.Children.Add(new ActivityIndicator { IsRunning = true, HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Color = Color.White });
            _loadingGrid.Children.Add(loadingLayout);

            _backgroundGrid.Children.Add(_loadingGrid, 0, 0);
        }

        private void SetupContentGrid()
        {            
            _contentGrid = new Grid {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = Device.OnPlatform(32,10,0) },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = Device.OnPlatform(200,220,0) },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = 40 },
                },
                IsVisible = false
            };

            _contentGrid.Children.Add(new StackLayout { Padding = new Thickness(20, 0, 0, 0), Children = { new Label { Text = "Upcoming Events", TextColor = Color.White, FontSize = 24, VerticalOptions = LayoutOptions.CenterAndExpand } } }, 0, 1);
            _contentGrid.Children.Add(new StackLayout { Padding = new Thickness(20, 0, 0, 0), Children = { new Label { Text = "Recent Messages", TextColor = Color.White, FontSize = 24, VerticalOptions = LayoutOptions.CenterAndExpand } } }, 0, 3);

            _calendarEventListView = new NoHighlightListView { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Color.Transparent };
            var calendarEventItemTemplate = new DataTemplate(() => new CalendarEventItemTemplate());
            calendarEventItemTemplate.CreateContent();
            _calendarEventListView.SeparatorVisibility = SeparatorVisibility.None;
            _calendarEventListView.ItemsSource = _calendarEvents;
            _calendarEventListView.ItemTemplate = calendarEventItemTemplate;
            _calendarEventListView.HasUnevenRows = true;
            _calendarEventListView.ItemTapped += (sender, e) => {                
                if (e.Item == null) {
                    return;
                }
                ((ListView)sender).SelectedItem = null;
            };
            _contentGrid.Children.Add(_calendarEventListView, 0, 2);

            _messageListView = new NoHighlightListView { HorizontalOptions = LayoutOptions.Fill, VerticalOptions = LayoutOptions.Fill, BackgroundColor = Color.Transparent };
            var messageItemTemplate = new DataTemplate(() => new MessageItemTemplate());
            messageItemTemplate.CreateContent();
            _messageListView.SeparatorVisibility = SeparatorVisibility.None;
            _messageListView.ItemsSource = _messages;
            _messageListView.ItemTemplate = messageItemTemplate;
            _messageListView.HasUnevenRows = true;
            _messageListView.ItemTapped += (sender, e) => {                
                if (e.Item == null) {
                    return;
                }
                ((ListView)sender).SelectedItem = null;
            };
            _contentGrid.Children.Add(_messageListView, 0, 4);

            Button refreshButton = new Button { Text = "Refresh", TextColor = Color.White, FontSize = 18, VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.EndAndExpand, BackgroundColor = Color.Transparent };
            refreshButton.Clicked += RefreshButton_Clicked;
            _contentGrid.Children.Add(new StackLayout { Orientation = StackOrientation.Horizontal, Children = { refreshButton }, Padding = new Thickness(0,0,10,0) }, 0, 5);

            _backgroundGrid.Children.Add(_contentGrid, 0, 0);
        }

        private void RefreshButton_Clicked (object sender, EventArgs e)
        {
            RefreshFromOffice365();
        }

        private void RefreshFromOffice365()
        {                
            _loadingGrid.IsVisible = true;
            _contentGrid.IsVisible = false;

            Task.Run(() => {
                var exchangeService = DependencyService.Get<IOffice365ExchangeService>();
                var calendarEvents = exchangeService.GetCalendarEvents(_adAccessToken);
                var messages = exchangeService.GetMessages(_adAccessToken);

                Device.BeginInvokeOnMainThread(() => {
                    _calendarEvents = new ObservableCollection<CalendarEvent>(calendarEvents);
                    _calendarEventListView.ItemsSource = _calendarEvents;

                    _messages = new ObservableCollection<Message>(messages);
                    _messageListView.ItemsSource = _messages;

                    _loadingGrid.IsVisible = false;
                    _contentGrid.IsVisible = true;
                });
            });
        }
    }
}
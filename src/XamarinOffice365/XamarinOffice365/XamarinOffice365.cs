using Xamarin.Forms;
using XamarinOffice365.Pages;

namespace XamarinOffice365
{
    public class App : Application
    {
        public App()
        {
            NavigationPage navigationPage = new NavigationPage(new StartupPage()) { BarTextColor = Color.White };
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
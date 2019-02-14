using detail_test.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace detail_test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {

        //public MainPage( int Progress, int subLevel, string ServerCon)
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
        }

        public async Task NavigateFromMenu(int id)
        {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        Detail = new NavigationPage(new ItemsPage( ));
                        break;
                    case (int)MenuItemType.About:
                        Detail = new NavigationPage(new AboutPage());
                        break;
                    case (int)MenuItemType.Settings:
                        Detail = new NavigationPage(new SettingsPage());
                        break;
                }
                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
        }
    }
}
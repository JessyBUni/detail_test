using System;
using System.Collections.Generic;

using Xamarin.Forms;
using detail_test.ViewModels;
namespace detail_test.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            string username = LoginViewModel.Username;
            //Binding Username = LoginViewModel.Username;
            InitializeComponent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using detail_test.Models;
using detail_test.Views;
using detail_test.ViewModels;
using detail_test.Services;

namespace detail_test.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {

            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            int currentProgress = LoginViewModel.ProgressPoint;
            if (item.ID <= currentProgress)
            {
                if (item.ID == currentProgress)
                {
                    if (LoginViewModel.SubscriptionLevel == 127)// user is paid
                    {
                        MockServer m = new MockServer(LoginViewModel.ServerConnection, out string SerCon);
                        int newprog = item.ID + 1;
                        m.updateUserProgress(SerCon, LoginViewModel.Username, newprog);
                        LoginViewModel.ProgressPoint = newprog;
                        BindingContext = viewModel = new ItemsViewModel();
                    }
                    else
                    {
                        viewModel.DisplayInvalidAccessPrompt += () => DisplayAlert("No Access",
                         "Invalid Selection, please upgrade your user subscription", "OK");
                        viewModel.DisplayInvalidAccessPrompt();
                    }
                }
            }
            else
            {
                viewModel.DisplayInvalidAccessPrompt += () => DisplayAlert("No Access",
                         "Invalid Selection, please view the previous chapters before trying to access this content", "OK");
                viewModel.DisplayInvalidAccessPrompt();
                return;
            }

            //if (subscription == 1 && item.ID != 1) { return; }*/

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            AcessItemsListView.SelectedItem = null;
        }

        /*async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }*/

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
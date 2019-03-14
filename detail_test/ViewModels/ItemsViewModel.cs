using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms;

using detail_test.Models;
using detail_test.Views;

namespace detail_test.ViewModels
{

    public class Grouping : ObservableCollection<Item>
    {
        public string Header { get; set; }
        //public Color Colour { get; set; }
    }
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Grouping> GroupedData { get; set; }
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Action DisplayInvalidAccessPrompt;

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            GroupedData = new ObservableCollection<Grouping>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                GroupedData.Clear();

                Grouping groupa = new Grouping();
                groupa.Header = "Accessible Content";
                //groupa.Colour = "{x:Static local:Framework.SecondaryColour}";
                //groupa.Colour = Framework.SecondaryColour;

                Grouping group = new Grouping();
                group.Header = "Locked Content";
                //group.Colour = "{x:Static local:Framework.Complementary1Colour}";
                //group.Colour = Framework.Complementary1Colour;
                int i = 0;
                int cutoff = 0;

                var items = await DataStore.GetItemsAsync(true);
                if (LoginViewModel.SubscriptionLevel == 127)//paid user
                {
                     cutoff = LoginViewModel.ProgressPoint;
                }
              
                foreach (var item in items)
                {
                    Items.Add(item);

                    if (i <= cutoff)
                    {
                        item.C = Framework.ContrastColour; 
                        //Itemsa.Add(item);
                        groupa.Add(item);
                    }
                    else
                    {
                        item.C = Framework.SecondaryColour;
                        //Itemsl.Add(item);
                        group.Add(item);
                    }
                    i++;
                }
                GroupedData.Add(groupa);
                GroupedData.Add(group);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
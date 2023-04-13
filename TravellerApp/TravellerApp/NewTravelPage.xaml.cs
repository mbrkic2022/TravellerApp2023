using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerApp.Logic;
using TravellerApp.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravellerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        Xamarin.Essentials.Location position;
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            position = await Geolocation.GetLocationAsync();
            var location = await LocationLogic.GetLocation(position.Latitude, position.Longitude);
            LocationListView.ItemsSource = location;
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedLocation = LocationListView.SelectedItem as Address;
                var firstCategory = selectedLocation.address;
                Post post = new Post()
                {
                    Experience = ExperienceEntry.Text,
                    Address = firstCategory.freeformAddress,
                    Country = firstCategory.country,
                    Municipality = firstCategory.municipality,
                    Latitude = position.Latitude,
                    Longitude = position.Longitude
                };
                SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
                conn.CreateTable<Post>();

                var rows = conn.Insert(post);
                conn.Close();
                if (rows > 0)
                {
                    await DisplayAlert("Success", "Experience successfully inserted!", "OK");
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", "Something went wrong", "OK");
            }
        }
    }
}
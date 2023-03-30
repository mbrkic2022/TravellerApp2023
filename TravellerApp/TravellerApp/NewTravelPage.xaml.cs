using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerApp.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

using Xamarin.Forms.Xaml;

namespace TravellerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        Position position;
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var locator = CrossGeolocator.Current;
            position = await locator.GetPositionAsync();
            var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
            LocationsListView.ItemsSource= placemarks;
        }
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedLocation = LocationsListView.SelectedItem as Placemark;
                Post post = new Post()
                {
                    Experience = ExperienceEntry.Text,
                    Address = selectedLocation.Thoroughfare,
                    Country = selectedLocation.CountryName,
                    Municipality = selectedLocation.Locality,
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
            catch(Exception ex)
            {
                await DisplayAlert("Error", "Something went wrong", "Ok");
            }
        }
    }
}
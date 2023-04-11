using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravellerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        IGeolocator locator = CrossGeolocator.Current;
        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetLocation();
        }

        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();
            if (status == PermissionStatus.Granted)
            {
                var location=await Geolocation.GetLocationAsync();
                locator.PositionChanged += Locator_PositionChanged;
                if (locator != null && locator.IsListening != true) await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100); // ako se lok. promijeni barem 100 m ili nismo updejtali 1 minutu, onda će se updejtati
                CenterMap(location.Latitude, location.Longitude);
                locationsMap.IsShowingUser = true;
            }
        }

        private void CenterMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude); // treba nam jer je to prvi param. za span
            MapSpan span = new MapSpan(center, 1, 1); // pola stupnja od centra na lijevo i pola na desno i isto tako gore/dolje
            locationsMap.MoveToRegion(span);

        }

        private void Locator_PositionChanged(object sender, PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            locator.StopListeningAsync();
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status== PermissionStatus.Granted) { return status; }
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}
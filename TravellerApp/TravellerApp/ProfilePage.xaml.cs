using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravellerApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravellerApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                var postTable = conn.Table<Post>().ToList();
                var countries = (from p in postTable 
                                 orderby p.Country 
                                 select p.Country).Distinct().ToList();
                //var countries2 = postTable.OrderBy(p => p.Country).Select(p => p.Country).Distinct().ToList();   
                postCountLabel.Text = postTable.Count().ToString();
                Dictionary<string, int> countriesCount = new Dictionary<string, int>();
                foreach (var country in countries)
                {
                    var count = (from post in postTable
                                 where post.Country == country
                                 select post).ToList().Count();
                    var count2 = postTable.Where(p=>p.Country == country).ToList().Count();
                    countriesCount[country] = count;
                }
                countriesListView.ItemsSource = countriesCount;
            }
        }
    }
}
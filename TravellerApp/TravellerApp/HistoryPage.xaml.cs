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
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                PostListView.ItemsSource = posts;
            }

        }

        private async void PostListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // var selecetedPost = sender as Post; if (selectedPost!=0) await Navigation.PushAsync(new PostDetailPage(selectedPost));
            await Navigation.PushAsync(new PostDetailPage(PostListView.SelectedItem as Post));
        }
    }
}
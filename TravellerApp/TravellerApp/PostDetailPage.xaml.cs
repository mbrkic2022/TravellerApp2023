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
    public partial class PostDetailPage : ContentPage
    {
        Post SelectedPost;
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            this.SelectedPost = selectedPost;
            ExperienceEntry.Text = selectedPost.Experience;
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                int rows = conn.Delete(SelectedPost);
                if (rows > 0)
                {
                    await DisplayAlert("Success", "Experience successfully deleted!", "OK");
                }
                await Navigation.PopAsync();
            }
        }

        private async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Post>();
                SelectedPost.Experience = ExperienceEntry.Text;
                int rows = conn.Update(SelectedPost);
                if (rows > 0)
                {
                    await DisplayAlert("Success", "Experience successfully updated!", "OK");
                }
                await Navigation.PopAsync();
            }
        }
    }
}
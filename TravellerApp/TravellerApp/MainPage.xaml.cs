using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravellerApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrWhiteSpace(EmailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrWhiteSpace(PasswordEntry.Text);
            if (!isEmailEmpty && !isPasswordEmpty)
            {
                await Navigation.PushAsync(new HomePage());
            }
            else 
            {
                await DisplayAlert("E-mail and password required", "Please enter both e-mail and password!", "OK");
            }
        }
    }
}
